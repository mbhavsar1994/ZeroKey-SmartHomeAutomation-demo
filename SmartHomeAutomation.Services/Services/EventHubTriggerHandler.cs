using System.Text;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

    /// <summary>
    /// Handles events triggered from Event Hub.
    /// </summary>
    public class EventHubTriggerHandler : IEventHubTriggerHandler
    {
        private readonly ILogger<EventHubTriggerHandler> _logger;
        private readonly IDeviceEventRepository _deviceEventRepository;
        private readonly IRuleEvaluator _ruleEvaluator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubTriggerHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="deviceEventRepository">Device event repository instance.</param>
        /// <param name="ruleEvaluator">Rule evaluator instance.</param>
        public EventHubTriggerHandler(ILogger<EventHubTriggerHandler> logger, 
            IDeviceEventRepository deviceEventRepository, 
            IRuleEvaluator ruleEvaluator)
        {
            _logger = logger;
            _deviceEventRepository = deviceEventRepository;
            _ruleEvaluator = ruleEvaluator;
        }

        /// <inheritdoc/>
        public async Task HandleEventsAsync(EventData[] events)
        {
            foreach (var eventData in events)
            {
                try
                {
                    var messageString = Encoding.UTF8.GetString(eventData.Body.ToArray());
                    _logger.LogInformation($"Event data: {messageString}");

                    var deviceData = JObject.Parse(messageString);
                    var deviceEvent = new DeviceEvent
                    {
                        Id = Guid.NewGuid().ToString(),
                        DeviceId = deviceData["deviceId"].Value<string>(),
                        Temperature = deviceData["temperature"].Value<float>(),
                        Humidity = deviceData["humidity"].Value<float>(),
                        Timestamp = DateTime.UtcNow
                    };

                    await _deviceEventRepository.StoreEventDataAsync(deviceEvent);
                    await _ruleEvaluator.EvaluateAndExecuteRulesAsync(deviceEvent.DeviceId, deviceEvent.Temperature, deviceEvent.Humidity);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred: {ex.Message}");
                }
            }
        }
    }