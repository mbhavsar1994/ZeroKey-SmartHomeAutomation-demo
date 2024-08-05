using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Repositories;
using SmartHomeAutomation.Services.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        string cosmosDbConnectionString = context.Configuration["CosmosDBConnectionString"];
        string serviceBusConnectionString = context.Configuration["ServiceBusConnectionString"];
        string blobStorageConnectionString = context.Configuration["BlobStorageConnectionString"];


        services.AddSingleton(s => new CosmosClient(cosmosDbConnectionString));
        services.AddSingleton(s => new ServiceBusClient(serviceBusConnectionString));
        services.AddSingleton(a => new BlobServiceClient(blobStorageConnectionString));


        services.AddSingleton<IDeviceRepository, DeviceRepository>();
        services.AddSingleton<IDeviceEventRepository, DeviceEventRepository>();
        services.AddSingleton<IAutomationRuleRepository, AutomationRuleRepository>();
        services.AddSingleton<IFileStorageService, FileStorageService>();
        services.AddSingleton<IMessageSender, MessageSender>();
        services.AddSingleton<IMessageReceiver, MessageReceiver>();
        services.AddSingleton<IEventHubTriggerHandler, EventHubTriggerHandler>();
        services.AddSingleton<IRuleEvaluator, RuleEvaluator>();
        services.AddSingleton<IAlertService, AlertService>();

        // Register actions
        services.AddSingleton<IAutomationAction, SendAlertAction>();
        services.AddSingleton<IAutomationAction, AdjustDeviceSettingsAction>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddApplicationInsights();
    })
    .Build();

host.Run();