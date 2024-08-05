using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Cosmos;
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

builder.Services.AddSingleton(s => new CosmosClient(cosmosDbConnectionString));
builder.Services.AddSingleton(s => new ServiceBusClient(serviceBusConnectionString));


builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IDeviceEventRepository, DeviceEventRepository>();
builder.Services.AddSingleton<IAutomationRuleRepository, AutomationRuleRepository>();
builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
builder.Services.AddSingleton<IMessageSender, MessageSender>();
builder.Services.AddSingleton<IMessageReceiver, MessageReceiver>();
builder.Services.AddSingleton<IEventHubTriggerHandler, EventHubTriggerHandler>();
builder.Services.AddSingleton<IRuleEvaluator, RuleEvaluator>();
builder.Services.AddSingleton<IAlertService, AlertService>();
builder.Services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddSingleton<IUserProfileService, UserProfileService>();

// Register actions
builder.Services.AddSingleton<IAutomationAction, SendAlertAction>();
builder.Services.AddSingleton<IAutomationAction, AdjustDeviceSettingsAction>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

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
app.UseAuthorization();

// Use the logging middlewareßß
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.Run();
