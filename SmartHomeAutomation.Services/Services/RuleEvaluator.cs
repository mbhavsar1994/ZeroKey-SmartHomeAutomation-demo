using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;
namespace SmartHomeAutomation.Services.Services;

    /// <summary>
    /// Evaluates and executes automation rules.
    /// </summary>
    public class RuleEvaluator : IRuleEvaluator
    {
        private readonly ILogger<RuleEvaluator> _logger;
        private readonly IAutomationRuleRepository _ruleRepository;
        private readonly IMessageSender _messageSender;
        private readonly string _automationRuleTriggerTopicName;
        private readonly Dictionary<string, IAutomationAction> _actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEvaluator"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="ruleRepository">Rule repository instance.</param>
        /// <param name="messageSender">Message sender instance.</param>
        /// <param name="configuration">Configuration instance.</param>
        /// <param name="actions">Collection of automation actions.</param>
        public RuleEvaluator(ILogger<RuleEvaluator> logger, 
            IAutomationRuleRepository ruleRepository, 
            IMessageSender messageSender, 
            IConfiguration configuration,
            IEnumerable<IAutomationAction> actions)
        {
            _logger = logger;
            _ruleRepository = ruleRepository;
            _messageSender = messageSender;
            _automationRuleTriggerTopicName = configuration["AutomationRuleTriggerTopic"];
            _actions = actions.ToDictionary(a => a.GetType().Name, a => a);
        }

        /// <inheritdoc/>
        public async Task EvaluateAndExecuteRulesAsync(string deviceId, float temperature, float humidity)
        {
            var rules = await _ruleRepository.GetRulesForDeviceAsync(deviceId);
            bool rulesFound = false;

            foreach (var rule in rules)
            {
                rulesFound = true;
                bool isTriggered = EvaluateRule(rule, temperature, humidity);

                if (isTriggered)
                {
                    await _messageSender.SendMessageAsync(_automationRuleTriggerTopicName, $"Triggered for device: {deviceId}, Temperature: {temperature}, Humidity: {humidity}");
                    if (_actions.ContainsKey(rule.Action))
                    {
                        _actions[rule.Action].ExecuteAsync(rule.DeviceId, temperature, humidity).GetAwaiter().GetResult();
                    }
                }
            }

            if (!rulesFound)
            {
                _logger.LogInformation($"No automation rules found for device: {deviceId}");
            }
        }

        private bool EvaluateRule(AutomationRule rule, float temperature, float humidity)
        {
            if (rule.Type == "Temperature")
            {
                return (rule.Operator == "GreaterThan" && temperature > rule.Threshold) ||
                       (rule.Operator == "LessThan" && temperature < rule.Threshold);
            }
            else if (rule.Type == "Humidity")
            {
                return (rule.Operator == "GreaterThan" && humidity > rule.Threshold) ||
                       (rule.Operator == "LessThan" && humidity < rule.Threshold);
            }

            return false;
        }
    }