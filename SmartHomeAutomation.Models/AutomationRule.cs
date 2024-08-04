namespace SmartHomeAutomation.Models;

/// <summary>
/// Represents an automation rule for a device.
/// </summary>
public class AutomationRule
{
    /// <summary>
    /// Unique identifier for the rule.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Identifier of the device the rule applies to.
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// Type of the rule (e.g., Temperature, Humidity).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Operator for the rule (e.g., GreaterThan, LessThan).
    /// </summary>
    public string Operator { get; set; }

    /// <summary>
    /// Threshold value for triggering the rule.
    /// </summary>
    public float Threshold { get; set; }

    /// <summary>
    /// Action to perform when the rule is triggered.
    /// </summary>
    public string Action { get; set; }
}