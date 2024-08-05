using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.Identity.Web;
using SmartHomeAutomation.API.Hubs;
using SmartHomeAutomation.API.Middleware;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Repositories;
using SmartHomeAutomation.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure app configuration
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
string cosmosDbConnectionString = builder.Configuration["CosmosDBConnectionString"];
string serviceBusConnectionString = builder.Configuration["ServiceBusConnectionString"];
string blobStorageConnectionString = builder.Configuration["BlobStorageConnectionString"];
string signalRHubUrl = builder.Configuration["SignalRHubUrl"];

builder.Services.AddSingleton(s => new CosmosClient(cosmosDbConnectionString));
builder.Services.AddSingleton(s => new ServiceBusClient(serviceBusConnectionString));
builder.Services.AddSingleton(a => new BlobServiceClient(blobStorageConnectionString));

builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IDeviceEventRepository, DeviceEventRepository>();
builder.Services.AddSingleton<IAutomationRuleRepository, AutomationRuleRepository>();
builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
builder.Services.AddSingleton<IMessageSender, MessageSender>();
builder.Services.AddSingleton<IEventHubTriggerHandler, EventHubTriggerHandler>();
builder.Services.AddSingleton<IRuleEvaluator, RuleEvaluator>();
builder.Services.AddSingleton<IAlertService, AlertService>();
builder.Services.AddSingleton<ISmartThermostatService, SmartThermostatService>();
builder.Services.AddSingleton<IReportRequestService, ReportRequestService>();
builder.Services.AddSingleton<ISmartThermostatReportService, SmartThermostatReportService>();
builder.Services.AddSingleton<IReportGenerator, ReportGenerator>();
builder.Services.AddSingleton<ISignalRNotificationService>(sp =>
    new SignalRNotificationService(builder.Configuration, sp.GetRequiredService<ILogger<SignalRNotificationService>>()));

builder.Services.AddSingleton<IReportNotificationService, ReportNotificationService>();
// Register actions
builder.Services.AddSingleton<IAutomationAction, SendAlertAction>();
builder.Services.AddSingleton<IAutomationAction, AdjustDeviceSettingsAction>();

builder.Services.AddSignalR();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

// Configure Azure AD authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DevicePolicy", policy => policy.RequireClaim("roles", "Device.ReadWrite"));
});

// Add controllers
builder.Services.AddControllers();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Use the custom exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Use the logging middlewareßß
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
