# Smart Home Automation System - Smart Thermostat

## Features

- **Device State Synchronization**: Ensures the current state of the thermostat (temperature, mode) is consistent across all user interfaces and backend systems.
- **Automation Rules Synchronization**: Ensures that any automation rules related to the thermostat are consistent across devices.

## Architecture

1. **Smart Thermostat**: Sends state changes and data to Azure IoT Hub.
2. **Azure IoT Hub**: Receives messages from the thermostat and triggers Azure Functions.
3. **Azure Functions**: Processes the messages, updates the central database, and publishes updates to Azure Service Bus.
4. **Azure Service Bus**: Decouples services and ensures reliable communication of state changes.
5. **Azure Cosmos DB**: Stores the device state and automation rules.


## Setup

### Step 1: Azure Resources

1. **Azure IoT Hub**
2. **Azure Service Bus**
3. **Azure Cosmos DB**
4. **Azure Event Hubs**

### Step 2: Configure Azure Resources

1. **Azure IoT Hub**: Register your Smart Thermostat device.
2. **Azure Service Bus**: Create a namespace and queue/topic.
3. **Azure Cosmos DB**: Create a database and collections for device states, user profiles, and automation rules.
4. **Azure Event Hubs**: Create a namespace and event hub for historical data.

### Step 3: Clone the Repository

```bash
git clone https://github.com/your-username/smart-home-automation.git
cd smart-home-automation


### Update local.settings.json with your Azure resource connection strings for Azure functions

{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "azureWebJobsStorage_connection_url",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "eventHubConnectionString": "event-hub-connection-string",
        "eventHubName": "hub-keyone",
        "CosmosDBConnectionString": "cosmos-db-connection-string",
        "ServiceBusConnectionString": "service-bus-connection-string",
        "AutomationRuleTriggerTopic": "automation-rule-trigger",
        "BlobStorageConnectionString": "blob_storage_connection_string",
        "ReportRequestQueue": "report-requests",
        "ReportBlobContainer": "energy-consumption-reports",
        "SignalRHubUrl": "signalr_hub_url"
    }
}

### update appsettings.json with Azure resource ids/connectionstring for API

{
  "CosmosDBConnectionString": "cosmos-db-connection-string",
  "ServiceBusConnectionString": "service-bus-connection-string",
  "ReportRequestQueue": "report-requests",
  "SignalRHubUrl": "signalr_hub_url",
  "ReportBlobContainer": "energy-consumption-reports",
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "demozerokey.onmicrosoft.com",
    "TenantId": "tenant-id",
    "ClientId": "client-id",
    "CallbackPath": "/signin-oidc",
    "ClientSecret": "client-secret"
  },
  "ApplicationInsights": {
    "InstrumentationKey": "application-insights-instrumentation-key"
  }
}
```
