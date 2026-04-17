using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Loads dialog handler YAML files from a directory.
/// Filename (without extension) = primary keyword trigger.
/// Matches window title/class/process against loaded handlers.
/// </summary>
public sealed class DialogHandlerManager
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    private readonly List<(string keyword, string filePath, DialogHandlerConfig config)> _handlers = new();

    /// <summary>Number of loaded handlers.</summary>
    public int Count => _handlers.Count;

    /// <summary>The handlers directory path.</summary>
    public string HandlersDir { get; }

    /// <summary>
    /// Load all *.yaml files from the handlers directory.
    /// Filename (minus extension) becomes the keyword trigger.
    /// </summary>
    public DialogHandlerManager(string handlersDir)
    {
        HandlersDir = handlersDir;
        if (!Directory.Exists(handlersDir)) return;

        foreach (var file in Directory.GetFiles(handlersDir, "*.yaml")
                     .Concat(Directory.GetFiles(handlersDir, "*.yml")))
        {
            try
            {
                var yaml = File.ReadAllText(file);
                var config = Deserializer.Deserialize<DialogHandlerConfig>(yaml);
                if (config == null) continue;

                // Filename without extension = keyword trigger
                var keyword = Path.GetFileNameWithoutExtension(file);
                _handlers.Add((keyword, file, config));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[BLOCK] Handler load error: {Path.GetFileName(file)} -- {ex.Message}");
                Console.ResetColor();
            }
        }

        // Sort by specificity: more match conditions = higher priority (checked first)
        // This ensures specific handlers (title_contains + class + process) match before generic ones
        _handlers.Sort((a, b) => Specificity(b.config).CompareTo(Specificity(a.config)));
    }

    /// <summary>
    /// Calculate handler specificity score: more match conditions = higher priority.
    /// </summary>
    private static int Specificity(DialogHandlerConfig cfg)
    {
        if (cfg.Match == null) return 0;
        int score = 0;
        if (cfg.Match.TitleContains != null) score += 10; // title is most specific
        if (cfg.Match.MessageContains != null) score += 5;
        if (cfg.Match.Class != null) score += 2;
        if (cfg.Match.Process != null) score += 1;
        return score;
    }

    /// <summary>
    /// Find a matching handler for the given window.
    /// Step 1: filename keyword must appear in match base (classPath + title, whitespace removed).
    ///         classPath = "GrandparentClass/ParentClass/MyClass" hierarchy from GetParent walk.
    ///         This allows matching by class hierarchy for empty-title dialogs.
    /// Step 2: match section fields (title_contains, class, process, message_contains) must all pass.
    /// </summary>
    public DialogHandlerConfig? FindHandler(
        string windowTitle, string classPath, string className, string processName,
        string? messageText = null)
    {
        // Match base: classPath + "/" + title with whitespace removed (keep / as hierarchy separator)
        // e.g. classPath="_NKHeroMainClass/#32770" + title="접속 끊김"
        //      -> "_NKHeroMainClass/#32770/접속끊김"
        // Empty title: "_NKHeroMainClass/#32770" + title="" -> "_NKHeroMainClass/#32770/"
        var matchBase = NormalizeForMatch(classPath + "/" + windowTitle);

        foreach (var (keyword, _, config) in _handlers)
        {
            // Step 1: normalized keyword in match base?
            var normalizedKeyword = NormalizeForMatch(keyword);
            if (!matchBase.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase))
                continue;

            // Step 2: precise match conditions (supports GitHub-style glob patterns)
            var m = config.Match;
            if (m != null)
            {
                // title: glob pattern -> full match; literal -> contains
                if (m.TitleContains != null)
                {
                    if (PatternMatcher.IsPattern(m.TitleContains))
                    {
                        if (!PatternMatcher.Create(m.TitleContains).IsMatch(windowTitle))
                            continue;
                    }
                    else if (!windowTitle.Contains(m.TitleContains, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                // class: path glob (/ or **) -> match vs classPath; standard glob -> match vs className; literal -> exact vs className
                if (m.Class != null)
                {
                    if (PatternMatcher.IsPathGlob(m.Class))
                    {
                        if (!PatternMatcher.CreatePathGlob(m.Class).IsMatch(classPath))
                            continue;
                    }
                    else if (PatternMatcher.IsPattern(m.Class))
                    {
                        if (!PatternMatcher.Create(m.Class).IsMatch(className))
                            continue;
                    }
                    else if (!string.Equals(m.Class, className, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                // process: glob pattern -> full match; literal -> exact
                if (m.Process != null)
                {
                    if (PatternMatcher.IsPattern(m.Process))
                    {
                        if (!PatternMatcher.Create(m.Process).IsMatch(processName))
                            continue;
                    }
                    else if (!string.Equals(m.Process, processName, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                // message: glob pattern -> full match; literal -> contains
                if (m.MessageContains != null && messageText != null)
                {
                    if (PatternMatcher.IsPattern(m.MessageContains))
                    {
                        if (!PatternMatcher.Create(m.MessageContains).IsMatch(messageText))
                            continue;
                    }
                    else if (!messageText.Contains(m.MessageContains, StringComparison.OrdinalIgnoreCase))
                        continue;
                }
            }

            return config;
        }

        return null;
    }

    /// <summary>
    /// Get a summary string of loaded handlers.
    /// </summary>
    public override string ToString()
    {
        if (_handlers.Count == 0) return "No dialog handlers loaded";
        var names = _handlers.Select(h => h.keyword);
        return $"{_handlers.Count} handler(s): [{string.Join(", ", names)}]";
    }

    /// <summary>
    /// Generate a sample handler YAML file for an unhandled dialog.
    /// Extracts a keyword from the window title and creates a template.
    /// Returns the generated file path, or null if generation failed.
    /// </summary>
    public static string? GenerateSampleHandler(
        string handlersDir, string windowTitle, string classPath, string className, string processName)
    {
        // Build keyword from title or class hierarchy for filename
        var keyword = ExtractKeyword(windowTitle);
        if (string.IsNullOrEmpty(keyword))
        {
            // Empty title -- use leaf class name as keyword
            keyword = ExtractKeyword(className);
        }
        if (string.IsNullOrEmpty(keyword)) return null;

        try
        {
            Directory.CreateDirectory(handlersDir);
            var filePath = Path.Combine(handlersDir, $"{keyword}.yaml");

            // Don't overwrite existing handler
            if (File.Exists(filePath)) return null;

            var yaml = $@"# Auto-generated handler template for: ""{windowTitle}""
# Class hierarchy: {classPath}
# Edit this file to configure automatic dialog handling.
# Filename ""{keyword}"" triggers when class path or title contains this keyword.

match:
  # title_contains: ""{windowTitle}""
  class: ""{className}""
  process: ""{processName}""
  # message_contains: """"   # match dialog message text (Static controls)

# Action options: click_button, dismiss, report
action: report    # <-- change to click_button or dismiss

params:
  button_index: 0       # 0 = first button (e.g. OK/Confirm)
  # button_text: """"   # alternative: match by button text
  wait_after: 1000      # ms to wait after handling
  retry: true           # retry the original action
";
            File.WriteAllText(filePath, yaml);
            return filePath;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Normalize a string for keyword matching: remove whitespace only.
    /// Keeps "/" as class hierarchy separator.
    /// e.g. "_NKHeroMainClass/#32770" + "/" + "접속 끊김" -> "_NKHeroMainClass/#32770/접속끊김"
    /// </summary>
    private static string NormalizeForMatch(string s)
        => s.Replace(" ", "");

    /// <summary>
    /// Extract a meaningful keyword from window title.
    /// Tries to find the most specific non-generic part.
    /// </summary>
    private static string? ExtractKeyword(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return null;

        // Sanitize for filename: remove invalid chars
        var sanitized = string.Join("", title.Split(Path.GetInvalidFileNameChars()));
        sanitized = sanitized.Trim();

        // If short enough, use as-is
        if (sanitized.Length > 0 && sanitized.Length <= 20) return sanitized;

        // For long titles, take first segment (split by common delimiters)
        var parts = sanitized.Split(new[] { ' ', '-', ':', '|', '/', '\\' },
            StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            // Use first non-trivial part (>= 2 chars)
            var part = parts.FirstOrDefault(p => p.Length >= 2) ?? parts[0];
            return part.Length <= 20 ? part : part[..20];
        }

        return sanitized.Length <= 20 ? sanitized : sanitized[..20];
    }
}
