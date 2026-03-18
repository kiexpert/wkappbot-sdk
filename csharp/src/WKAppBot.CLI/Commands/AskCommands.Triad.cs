using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

/// <summary>
/// Shared context for parallel triad runs (Gemini + GPT + Claude).
/// Thread-safe: written from concurrent Task.Run contexts.
///
/// Dual-storage: in-memory (fast) + JSONL files (crash-safe).
/// Files layout: {DataDir}/triad/{sessionId}/{ai}.jsonl
///   Each line: {"ts":"...","ai":"Gemini","msg":"..."}
/// On recovery, BuildRecoveryContext merges in-memory + file logs — survives process restart.
/// </summary>
internal sealed class TriadSharedContext
{
    private readonly string _question;
    private readonly string? _sessionDir;  // null = in-memory only (non-triad use)

    // Per-AI step log: initial answer + each loop step report
    private readonly ConcurrentDictionary<string, List<string>> _stepLogs =
        new(StringComparer.OrdinalIgnoreCase);

    // Per-AI file-write lock (multiple threads write to same AI file concurrently in recovery retries)
    private readonly ConcurrentDictionary<string, object> _fileLocks =
        new(StringComparer.OrdinalIgnoreCase);

    public string? SessionDir => _sessionDir;

    public TriadSharedContext(string question, string? sessionDir = null)
    {
        _question = question;
        _sessionDir = sessionDir;

        if (sessionDir != null)
        {
            Directory.CreateDirectory(sessionDir);
            // Write session manifest so we can identify this session later
            var manifest = Path.Combine(sessionDir, "session.json");
            File.WriteAllText(manifest, JsonSerializer.Serialize(new
            {
                question,
                started = DateTime.UtcNow.ToString("o"),
            }));
        }
    }

    /// <summary>
    /// Record a step/answer summary for an AI.
    /// Called on initial answer and each loop step report.
    /// Appends to in-memory list AND JSONL file atomically per AI.
    /// </summary>
    public void LogStep(string ai, string summary)
    {
        var snippet = summary.Length > 600 ? summary[..600] + "…" : summary;

        // In-memory
        var steps = _stepLogs.GetOrAdd(ai, _ => new List<string>());
        lock (steps) steps.Add(snippet);

        // File (crash-safe)
        if (_sessionDir != null)
        {
            var fileLock = _fileLocks.GetOrAdd(ai, _ => new object());
            lock (fileLock)
            {
                try
                {
                    var path = Path.Combine(_sessionDir, $"{ai.ToLowerInvariant()}.jsonl");
                    var line = JsonSerializer.Serialize(new
                    {
                        ts  = DateTime.UtcNow.ToString("o"),
                        ai,
                        msg = snippet
                    });
                    File.AppendAllText(path, line + "\n");
                }
                catch { /* never crash AI pipeline over log write failure */ }
            }
        }
    }

    /// <summary>
    /// Build a recovery context string for the failing AI.
    /// Merges in-memory logs + file logs (file wins if in-memory is empty — e.g. after restart).
    /// </summary>
    public string BuildRecoveryContext(string failedAi)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== TRIAD RECOVERY CONTEXT ===");
        sb.AppendLine($"Original question: {_question}");
        sb.AppendLine();

        // Merge in-memory + file logs for each non-failing AI
        var allAis = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Gemini", "ChatGPT", "Claude" };
        allAis.Remove(failedAi);

        bool anyContext = false;
        foreach (var ai in allAis)
        {
            var entries = LoadMergedSteps(ai);
            if (entries.Count == 0) continue;
            anyContext = true;
            sb.AppendLine($"[{ai}] ({entries.Count} entries):");
            foreach (var s in entries.TakeLast(4))
                sb.AppendLine($"  • {s}");
            sb.AppendLine();
        }

        if (!anyContext)
            sb.AppendLine("(No context collected yet from other AIs — starting fresh.)");

        sb.AppendLine($"You ({failedAi}) encountered a failure. Resume the original task using the above context.");
        sb.AppendLine("=== END RECOVERY CONTEXT ===");
        return sb.ToString();
    }

    // Merge in-memory list with file-based JSONL (deduplicated by message content)
    private List<string> LoadMergedSteps(string ai)
    {
        // Start with in-memory
        var result = new List<string>();
        if (_stepLogs.TryGetValue(ai, out var mem))
            lock (mem) result.AddRange(mem);

        // Supplement from file if available and in-memory is sparse
        if (_sessionDir != null && result.Count == 0)
        {
            try
            {
                var path = Path.Combine(_sessionDir, $"{ai.ToLowerInvariant()}.jsonl");
                if (File.Exists(path))
                {
                    foreach (var line in File.ReadAllLines(path))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var node = JsonNode.Parse(line);
                        var msg = node?["msg"]?.GetValue<string>();
                        if (msg != null) result.Add(msg);
                    }
                }
            }
            catch { }
        }

        return result;
    }
}

internal partial class Program
{
    // Active triad session dir for the current Eye/CLI invocation (set in AskTriadParallel)
    static volatile string? _triadLastSessionDir;

    /// <summary>
    /// Calls one AI in triad mode, then retries once with recovery context if it fails.
    /// Must be called from inside a Task.Run that already inherited the unified Slack thread ts.
    /// linePrefix is applied here so both the initial run and recovery share the same prefix.
    /// Pass linePrefix:null to the AI functions so they inherit rather than override this prefix.
    /// </summary>
    static int RunTriadAiWithRecovery(
        string ai, string question, int timeoutSec,
        List<string>? attachFiles, bool newSession, bool loopMode, int loopMaxSteps,
        int loopRetry, int loopMaxParallel, string? modelHint, bool noWait,
        TriadSharedContext ctx, string linePrefix)
    {
        using var _pfx = ApplyOutputPrefix(linePrefix);

        int result = ai switch
        {
            "gemini" => AskGemini(question, true, timeoutSec, newTab: false, attachFiles,
                newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                triadMode: true, modelHint, noWait, targetTagOverride: null,
                linePrefix: null, triadCtx: ctx),
            "gpt" => AskChatGpt(question, true, timeoutSec, newTab: false, attachFiles,
                newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                triadMode: true, modelHint, noWait, targetTagOverride: null,
                linePrefix: null, triadCtx: ctx),
            "claude" => AskClaude(question, true, timeoutSec, newTab: false,
                newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                triadMode: true, modelHint, noWait, targetTagOverride: null,
                linePrefix: null, triadCtx: ctx),
            _ => 1
        };

        if (result != 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[TRIAD:{ai}] ⚠ Failed — attempting recovery with shared context");
            Console.ResetColor();

            SlackPostToThread($"🔄 *[복구]* `{ai}` 실패 감지 — 컨텍스트 재주입 후 재시작 중...", ai);

            var recoveryCtx = ctx.BuildRecoveryContext(ai);
            var recoveryQuestion = recoveryCtx + "\n\nResume task:\n" + question;

            // Brief pause to let tab settle before recovery
            Task.Delay(2000).GetAwaiter().GetResult();

            result = ai switch
            {
                "gemini" => AskGemini(recoveryQuestion, true, timeoutSec, newTab: false,
                    attachFiles, newSession: true, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                    triadMode: true, modelHint, noWait, targetTagOverride: null,
                    linePrefix: null, triadCtx: ctx),
                "gpt" => AskChatGpt(recoveryQuestion, true, timeoutSec, newTab: false,
                    attachFiles, newSession: true, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                    triadMode: true, modelHint, noWait, targetTagOverride: null,
                    linePrefix: null, triadCtx: ctx),
                "claude" => AskClaude(recoveryQuestion, true, timeoutSec, newTab: false,
                    newSession: true, loopMode, loopMaxSteps, loopRetry, loopMaxParallel,
                    triadMode: true, modelHint, noWait, targetTagOverride: null,
                    linePrefix: null, triadCtx: ctx),
                _ => 1
            };

            {
                var status = result == 0
                    ? $"✅ *[복구 성공]* `{ai}` 응답 완료"
                    : $"❌ *[복구 실패]* `{ai}` — 두 번째 시도도 실패";
                SlackPostToThread(status, ai);
            }
        }

        return result;
    }
}
