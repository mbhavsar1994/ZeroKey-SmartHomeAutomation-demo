using Microsoft.Azure.Cosmos;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Repositories;

/// <summary>
/// Repository for managing automation rules.
/// </summary>
public class AutomationRuleRepository : IAutomationRuleRepository
{
    private readonly Container _rulesContainer;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutomationRuleRepository"/> class.
    /// </summary>
    /// <param name="cosmosClient">Cosmos client instance.</param>
    public AutomationRuleRepository(CosmosClient cosmosClient)
    {
        _rulesContainer = cosmosClient.GetContainer("device-data-document-db", "AutomationRules");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AutomationRule>> GetRulesForDeviceAsync(string deviceId)
    {
        var rules = new List<AutomationRule>();
        var query = new QueryDefinition("SELECT * FROM c WHERE c.deviceId = @deviceId")
            .WithParameter("@deviceId", deviceId);

        var iterator = _rulesContainer.GetItemQueryIterator<AutomationRule>(query);

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            rules.AddRange(response);
        }

        return rules;
    }
}