using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WKAppBot.Core.Scenario;

/// <summary>
/// Loads and validates YAML scenario files.
/// </summary>
public static class ScenarioParser
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    /// <summary>
    /// Load a scenario from a YAML file.
    /// </summary>
    public static ScenarioDocument Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new ScenarioParseException($"File not found: {filePath}");

        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        if (ext is not (".yaml" or ".yml"))
            throw new ScenarioParseException($"Unsupported file extension: {ext} (expected .yaml or .yml)");

        string yaml;
        try
        {
            yaml = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            throw new ScenarioParseException($"Cannot read file: {ex.Message}", ex);
        }

        return Parse(yaml);
    }

    /// <summary>
    /// Parse a scenario from a YAML string.
    /// </summary>
    public static ScenarioDocument Parse(string yaml)
    {
        ScenarioDocument doc;
        try
        {
            doc = Deserializer.Deserialize<ScenarioDocument>(yaml)
                  ?? throw new ScenarioParseException("Empty YAML document");
        }
        catch (YamlDotNet.Core.YamlException ex)
        {
            throw new ScenarioParseException($"YAML syntax error: {ex.Message}", ex);
        }

        Validate(doc);
        return doc;
    }

    /// <summary>
    /// Validate without throwing (returns success/failure).
    /// </summary>
    public static bool TryValidate(string filePath, out string? error)
    {
        try
        {
            Load(filePath);
            error = null;
            return true;
        }
        catch (ScenarioParseException ex)
        {
            error = ex.Message;
            return false;
        }
    }

    private static void Validate(ScenarioDocument doc)
    {
        if (string.IsNullOrWhiteSpace(doc.Scenario.Name))
            throw new ScenarioParseException("scenario.name is required");

        if (string.IsNullOrWhiteSpace(doc.App.Launch))
            throw new ScenarioParseException("app.launch is required");

        if (doc.Steps == null || doc.Steps.Count == 0)
            throw new ScenarioParseException("At least one step is required");

        for (int i = 0; i < doc.Steps.Count; i++)
        {
            var step = doc.Steps[i];
            if (string.IsNullOrWhiteSpace(step.Name))
                throw new ScenarioParseException($"steps[{i}].name is required");
            if (string.IsNullOrWhiteSpace(step.Action))
                throw new ScenarioParseException($"steps[{i}].action is required");

            ValidateAction(step, i);
        }
    }

    private static void ValidateAction(StepDefinition step, int index)
    {
        switch (step.Action)
        {
            case "type_text":
                if (string.IsNullOrEmpty(step.Params?.Text))
                    throw new ScenarioParseException($"steps[{index}]: type_text requires params.text");
                break;

            case "press_key":
                if (string.IsNullOrEmpty(step.Params?.Key))
                    throw new ScenarioParseException($"steps[{index}]: press_key requires params.key");
                break;

            case "hotkey":
                if (step.Params?.Keys == null || step.Params.Keys.Count == 0)
                    throw new ScenarioParseException($"steps[{index}]: hotkey requires params.keys");
                break;

            case "assert":
                if (string.IsNullOrEmpty(step.Params?.Type))
                    throw new ScenarioParseException($"steps[{index}]: assert requires params.type");
                if (step.Params.Expected == null)
                    throw new ScenarioParseException($"steps[{index}]: assert requires params.expected");
                break;

            case "click":
            case "double_click":
            case "right_click":
                // Target required but validated at runtime
                break;

            case "wait":
                // seconds optional (default 1.0)
                break;

            case "screenshot":
                // filename optional
                break;

            case "scroll":
                // direction and amount optional
                break;

            // UIA pattern actions (all focusless — no SendInput needed)
            case "toggle":
            case "expand":
            case "collapse":
            case "select":
            case "set_range":
            case "invoke":
            case "set_value":
            case "window_close":
            case "window_minimize":
            case "window_maximize":
                break;

            default:
                throw new ScenarioParseException(
                    $"steps[{index}]: unknown action '{step.Action}'. " +
                    "Valid: click, double_click, right_click, type_text, press_key, hotkey, " +
                    "wait, screenshot, assert, scroll, toggle, expand, collapse, select, " +
                    "set_range, invoke, set_value, window_close, window_minimize, window_maximize");
        }
    }
}

public class ScenarioParseException : Exception
{
    public ScenarioParseException(string message) : base(message) { }
    public ScenarioParseException(string message, Exception inner) : base(message, inner) { }
}
