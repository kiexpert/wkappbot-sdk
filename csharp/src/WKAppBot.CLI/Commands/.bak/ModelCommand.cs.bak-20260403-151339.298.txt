// ModelCommand.cs — Active fallback model configuration.
//
// When Claude Code hits a rate limit or server error, Eye's fallback spawns
// the model specified here. Defaults to "gemini".
//
// `wkappbot model set sonnet|opus|haiku` also writes ~/.claude/settings.json
// so Claude Code's prompt window switches to that Claude model immediately.
//
// Commands:
//   wkappbot model set gemini|gpt|claude|sonnet|opus|haiku
//   wkappbot model get
//
// Config: wkappbot.hq/active-model.json  +  ~/.claude/settings.json (Claude models only)

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    static string ActiveModelConfigPath => Path.Combine(DataDir, "active-model.json");

    // ~/.claude/settings.json — Claude Code reads this for model selection
    static string ClaudeSettingsPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".claude", "settings.json");

    // Map friendly names → Claude Code model short names (what settings.json accepts)
    static readonly Dictionary<string, string> ClaudeModelAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        ["sonnet"]  = "sonnet",
        ["opus"]    = "opus",
        ["haiku"]   = "haiku",
        ["claude"]  = "sonnet",   // "claude" → default claude sonnet
        ["claude-sonnet"] = "sonnet",
        ["claude-opus"]   = "opus",
        ["claude-haiku"]  = "haiku",
    };

    // External AI models handled by Eye fallback (not Claude Code native)
    static readonly HashSet<string> ExternalModels = new(StringComparer.OrdinalIgnoreCase)
        { "gemini", "gpt", "chatgpt" };

    static string GetActiveFallbackModel()
    {
        try
        {
            if (!File.Exists(ActiveModelConfigPath)) return "gemini";
            var node = JsonNode.Parse(File.ReadAllText(ActiveModelConfigPath));
            return node?["model"]?.GetValue<string>() ?? "gemini";
        }
        catch { return "gemini"; }
    }

    static string? GetCurrentClaudeModel()
    {
        try
        {
            if (!File.Exists(ClaudeSettingsPath)) return null;
            var node = JsonNode.Parse(File.ReadAllText(ClaudeSettingsPath));
            return node?["model"]?.GetValue<string>();
        }
        catch { return null; }
    }

    /// <summary>
    /// Read the model used in the last Claude Code assistant turn from the active session JSONL.
    /// Returns e.g. "claude-sonnet-4-6".
    /// </summary>
    static string? GetCurrentSessionModel(string? cwd = null)
    {
        try
        {
            var (_, _, jsonlPath, _) = GetContextInfoForCwdEx(cwd ?? Environment.CurrentDirectory);
            if (string.IsNullOrEmpty(jsonlPath) || !File.Exists(jsonlPath)) return null;
            var lines = ReadLastLines(jsonlPath, 30);
            foreach (var line in Enumerable.Reverse(lines))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var node = JsonNode.Parse(line);
                if (node?["type"]?.GetValue<string>() != "assistant") continue;
                var model = node["message"]?["model"]?.GetValue<string>();
                if (!string.IsNullOrEmpty(model)) return model;
            }
        }
        catch { }
        return null;
    }

    static void SetActiveFallbackModel(string model)
    {
        var json = new JsonObject { ["model"] = model, ["updatedAt"] = DateTime.UtcNow.ToString("O") };
        File.WriteAllText(ActiveModelConfigPath, json.ToJsonString());
    }

    /// <summary>
    /// Write model into ~/.claude/settings.json — Claude Code picks this up on next prompt.
    /// Preserves existing keys.
    /// </summary>
    static bool SetClaudeCodeModel(string claudeModelShort)
    {
        try
        {
            JsonObject settings;
            if (File.Exists(ClaudeSettingsPath))
            {
                var raw = File.ReadAllText(ClaudeSettingsPath, Encoding.UTF8);
                settings = JsonNode.Parse(raw)?.AsObject() ?? new JsonObject();
            }
            else
            {
                settings = new JsonObject();
            }
            settings["model"] = claudeModelShort;
            var tmp = ClaudeSettingsPath + ".tmp";
            File.WriteAllText(tmp, settings.ToJsonString(new JsonSerializerOptions { WriteIndented = true }), Encoding.UTF8);
            File.Move(tmp, ClaudeSettingsPath, overwrite: true);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[MODEL] Failed to write Claude settings: {ex.Message}");
            return false;
        }
    }

    static int ModelCommand(string[] args)
    {
        if (args.Length == 0) return ModelUsage();

        var sub = args[0].ToLowerInvariant();

        // Called by ConfigChange hook — reads ~/.claude/settings.json model and syncs active-model.json
        if (sub == "sync-from-settings")
        {
            var claudeModel = GetCurrentClaudeModel();
            if (string.IsNullOrEmpty(claudeModel)) return 0;
            // Only sync Claude models (sonnet/opus/haiku) — external models managed separately
            if (ClaudeModelAliases.ContainsKey(claudeModel))
            {
                SetActiveFallbackModel("gemini"); // primary is claude → fallback stays gemini
                Console.WriteLine($"[MODEL] Synced from Claude Code settings: {claudeModel} (fallback=gemini)");
            }
            return 0;
        }

        if (sub == "get" || sub == "status")
        {
            var fallback = GetActiveFallbackModel();
            var claudeSettings = GetCurrentClaudeModel();
            var sessionModel = GetCurrentSessionModel();
            Console.WriteLine($"[MODEL] Fallback model    : {fallback}");
            if (claudeSettings != null)
                Console.WriteLine($"[MODEL] Claude Code (cfg) : {claudeSettings}");
            if (sessionModel != null)
                Console.WriteLine($"[MODEL] Session (last turn): {sessionModel}");
            return 0;
        }

        if (sub == "set" && args.Length >= 2)
        {
            var model = args[1].ToLowerInvariant();
            if (model == "chatgpt") model = "gpt";

            if (ExternalModels.Contains(model))
            {
                // External AI → only update fallback config
                SetActiveFallbackModel(model);
                Console.WriteLine($"[MODEL] Fallback model set to: {model}");
                Console.WriteLine($"[MODEL] Eye will use '{model}' on next Claude error/rate-limit.");
                return 0;
            }

            if (ClaudeModelAliases.TryGetValue(model, out var claudeShort))
            {
                // Claude model → update fallback config + Claude Code settings.json
                SetActiveFallbackModel("gemini"); // reset fallback to gemini (claude is primary)
                var ok = SetClaudeCodeModel(claudeShort);
                Console.WriteLine($"[MODEL] Claude Code model → {claudeShort}" + (ok ? "" : " (settings write failed)"));
                Console.WriteLine($"[MODEL] Fallback model reset to: gemini");
                Console.WriteLine($"[MODEL] Restart Claude Code tab to apply new model.");
                return 0;
            }

            return Error($"Unknown model: {model} (use gemini, gpt, sonnet, opus, haiku)");
        }

        return ModelUsage();
    }

    static int ModelUsage()
    {
        Console.WriteLine(@"Usage: wkappbot model <command>

Commands:
  model set gemini|gpt          Set external fallback model (Eye auto-spawns on Claude error)
  model set sonnet|opus|haiku   Switch Claude Code model (writes ~/.claude/settings.json)
  model get                     Show current fallback + Claude Code model

Claude models apply on next Claude Code prompt restart.
External models (gemini/gpt) are spawned automatically on Claude error/rate-limit.");
        return 1;
    }
}
