using System.Text.Json;

namespace WKAppBot.WebBot;

/// <summary>
/// Runtime-configurable CSS selector registry for AI chat sites.
/// Loads from JSON file (wkappbot.hq/cdp_selectors.json) with hardcoded defaults.
/// Allows hot-patching DOM selectors when AI sites change layout without code changes.
/// </summary>
public static class CdpSelectorRegistry
{
    static readonly string _jsonPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "..", "SDK", "bin", "wkappbot.hq", "cdp_selectors.json");

    static Dictionary<string, Dictionary<string, string[]>>? _cache;
    static DateTime _cacheTime;

    /// <summary>Default selectors per host per purpose. Used when JSON not found or entry missing.</summary>
    static readonly Dictionary<string, Dictionary<string, string[]>> Defaults = new(StringComparer.OrdinalIgnoreCase)
    {
        ["chatgpt"] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["editor"] = ["#prompt-textarea", "textarea", "[contenteditable='true']", "[role='textbox']"],
            ["send"] = ["button[data-testid='send-button']", "button[aria-label*='Send']", "button[aria-label*='보내기']"],
            ["stop"] = ["button[data-testid='stop-button']", "button[aria-label='Stop generating']", "button[aria-label='스트리밍 중지']"],
            ["message"] = ["[data-message-author-role='assistant']", "article"],
            ["upload"] = ["button[aria-label*='Upload']", "button[aria-label*='파일 업로드']"],
        },
        ["gemini"] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["editor"] = [".ql-editor", "[role='textbox'][contenteditable='true']", "div[contenteditable='true']", "rich-textarea [contenteditable]", ".input-area [contenteditable]"],
            ["send"] = ["button[aria-label='Send message']", "button[aria-label*='Send']"],
            ["stop"] = ["button[aria-label*='Stop']", "button[aria-label*='중지']"],
            ["message"] = ["model-response", "[role='article']"],
            ["dialog"] = ["mat-dialog-container", "[role='dialog']", "[role='alertdialog']"],
        },
        ["claude"] = new(StringComparer.OrdinalIgnoreCase)
        {
            ["editor"] = ["div.tiptap.ProseMirror[contenteditable='true']", "div.tiptap.ProseMirror", ".ProseMirror[contenteditable='true']", "[contenteditable='true']", "[data-testid='composer-input']", "textarea"],
            ["send"] = ["button[aria-label*='Send']", "button[aria-label*='보내기']"],
            ["stop"] = ["button[aria-label*='Stop']"],
            ["message"] = ["[data-testid='user-message']", "[data-is-streaming]", "[data-testid='assistant-message']"],
        },
    };

    /// <summary>
    /// Get CSS selectors for a given host and purpose.
    /// Falls back to defaults if JSON config missing.
    /// </summary>
    /// <param name="host">AI host: "chatgpt", "gemini", "claude"</param>
    /// <param name="purpose">Selector purpose: "editor", "send", "stop", "message", "dialog", "upload"</param>
    /// <returns>Array of CSS selectors in priority order (first match wins)</returns>
    public static string[] Get(string host, string purpose)
    {
        var registry = LoadRegistry();
        if (registry.TryGetValue(host, out var hostMap) && hostMap.TryGetValue(purpose, out var selectors))
            return selectors;

        // Fallback to defaults
        if (Defaults.TryGetValue(host, out var defMap) && defMap.TryGetValue(purpose, out var defSelectors))
            return defSelectors;

        return [];
    }

    /// <summary>
    /// Get the first matching CSS selector from the registry.
    /// Tries each selector in order via JS querySelector, returns first non-null.
    /// </summary>
    public static string GetFirst(string host, string purpose)
    {
        var selectors = Get(host, purpose);
        return selectors.Length > 0 ? selectors[0] : "";
    }

    /// <summary>
    /// Build a JS expression that tries each selector in order and returns the first match.
    /// E.g.: document.querySelector('sel1') || document.querySelector('sel2') || null
    /// </summary>
    public static string BuildFallbackJs(string host, string purpose)
    {
        var selectors = Get(host, purpose);
        if (selectors.Length == 0) return "null";
        return string.Join(" || ", selectors.Select(s => $"document.querySelector('{CdpClient.Esc(s)}')"));
    }

    static Dictionary<string, Dictionary<string, string[]>> LoadRegistry()
    {
        // Cache for 30s to avoid frequent file reads
        if (_cache != null && (DateTime.UtcNow - _cacheTime).TotalSeconds < 30)
            return _cache;

        try
        {
            if (File.Exists(_jsonPath))
            {
                var json = File.ReadAllText(_jsonPath);
                var loaded = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string[]>>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (loaded != null)
                {
                    _cache = loaded;
                    _cacheTime = DateTime.UtcNow;
                    return loaded;
                }
            }
        }
        catch { /* fall through to defaults */ }

        _cache = Defaults;
        _cacheTime = DateTime.UtcNow;
        return Defaults;
    }

    /// <summary>Export defaults to JSON file for editing.</summary>
    public static void ExportDefaults()
    {
        var dir = Path.GetDirectoryName(_jsonPath);
        if (dir != null) Directory.CreateDirectory(dir);
        var json = JsonSerializer.Serialize(Defaults, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_jsonPath, json);
    }
}
