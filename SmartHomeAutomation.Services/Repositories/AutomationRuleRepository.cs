using Microsoft.Azure.Cosmos;
using SmartHomeAutomation.Models;
using Microsoft.Extensions.Caching.Memory;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Repositories;

/// <summary>
/// Repository for managing automation rules.
/// </summary>
public class AutomationRuleRepository : IAutomationRuleRepository
{
    private readonly Container _rulesContainer;
    private readonly IMemoryCache _cache;
    private readonly string _cacheKeyPrefix = "AutomationRules_";

    
    /// <summary>
    /// Initializes a new instance of the <see cref="AutomationRuleRepository"/> class.
    /// </summary>
    /// <param name="cosmosClient">Cosmos client instance.</param>
    public AutomationRuleRepository(CosmosClient cosmosClient, IMemoryCache cache)
    {
        _rulesContainer = cosmosClient.GetContainer("device-data-document-db", "AutomationRules");
        _cache = cache;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AutomationRule>> GetRulesForDeviceAsync(string deviceId)
    {
        string cacheKey = _cacheKeyPrefix + deviceId;
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<AutomationRule> rules))
        {
            var ruleList = new List<AutomationRule>();
            var query = new QueryDefinition("SELECT * FROM c WHERE c.deviceId = @deviceId")
                .WithParameter("@deviceId", deviceId);

            var iterator = _rulesContainer.GetItemQueryIterator<AutomationRule>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                ruleList.AddRange(response);
            }
            rules = ruleList;
            _cache.Set(cacheKey, rules, TimeSpan.FromMinutes(10));
        }
        return rules;
    }
}