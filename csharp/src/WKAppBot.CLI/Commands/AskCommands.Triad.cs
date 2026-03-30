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

    // ── Real-time cross-prompting: streaming chunks shared between AIs ──
    // When AI-A produces a chunk, other AIs get it as context.
    private readonly ConcurrentDictionary<string, string> _latestChunks = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, int> _chunkVersions = new(StringComparer.OrdinalIgnoreCase);

    // Per-AI cross-prompt injection queue: chunks from peers waiting to be injected
    private readonly ConcurrentDictionary<string, ConcurrentQueue<(string fromAi, string text)>> _crossPromptQueues =
        new(StringComparer.OrdinalIgnoreCase);

    // Per-AI CdpClient reference for direct injection
    internal readonly ConcurrentDictionary<string, WKAppBot.WebBot.CdpClient> _cdpClients =
        new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Register a CdpClient for an AI (needed for cross-prompt injection).</summary>
    public void RegisterCdp(string ai, WKAppBot.WebBot.CdpClient cdp) => _cdpClients[ai] = cdp;

    /// <summary>When true, moderator intervenes (STANCE check, format enforcement). Off during R0.</summary>
    public bool ModeratorEnabled { get; set; } = false;

    /// <summary>The original user question — used by moderator redirect when debaters go off-topic.</summary>
    public string OriginalQuestion => _question;

    // ── Live MD minutes: real-time debate transcript (APPEND mode) ──
    private string? _mdPath;
    private readonly object _mdLock = new();
    private bool _mdHeaderWritten;
    private string _mdQuestion = "";

    /// <summary>Initialize live MD output. Call once after construction.</summary>
    public void InitLiveMinutes(string question, string? mdPath = null)
    {
        _mdQuestion = question;
        // Default path: triad session dir / 삼두 정반합 회의록 - {short question}.md
        if (mdPath == null)
        {
            var shortQ = question.Length > 40 ? question[..40].Trim() + "..." : question;
            // Sanitize filename
            foreach (var c in Path.GetInvalidFileNameChars()) shortQ = shortQ.Replace(c, '_');
            shortQ = shortQ.Replace("[", "").Replace("]", "").Replace("G:", "").Trim();
            // Put in CWD/.wkappbot/ask/ folder (consistent with single ask MD output)
            // Prefer CallerCwd (original invoker's working directory), then git root, then fallback
            var cwd = EyeCmdPipeServer.CallerCwd.Value;
            if (string.IsNullOrEmpty(cwd) || !Directory.Exists(Path.Combine(cwd, ".git")))
            {
                // Walk up from exe dir to find git root (most likely the project working directory)
                var probe = Path.GetDirectoryName(Environment.ProcessPath) ?? Environment.CurrentDirectory;
                for (int i = 0; i < 10 && !string.IsNullOrEmpty(probe); i++)
                {
                    if (Directory.Exists(Path.Combine(probe, ".git"))) { cwd = probe; break; }
                    probe = Path.GetDirectoryName(probe);
                }
                if (string.IsNullOrEmpty(cwd)) cwd = Environment.CurrentDirectory;
            }
            var askDir = Path.Combine(cwd, ".wkappbot", "ask");
            Directory.CreateDirectory(askDir);
            var ts = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var slug = Program.BuildSlug(shortQ, 40);
            mdPath = Path.Combine(askDir, $"{ts}-triad-{slug}.md");
        }
        _mdPath = mdPath;
        if (_mdPath != null)
        {
            // Write header once
            lock (_mdLock)
            {
                File.WriteAllText(_mdPath, $"# Triad Debate\n\n> **Question**: {_mdQuestion}\n> **Started**: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n---\n\n", Encoding.UTF8);
                _mdHeaderWritten = true;
            }
        }
    }

    /// <summary>Path to the live MD file (null if not initialized).</summary>
    public string? MdPath => _mdPath;

    /// <summary>Append a line to the live MD (thread-safe). Used by all Slack relay points.</summary>
    public void AppendMd(string line)
    {
        if (_mdPath == null) return;
        lock (_mdLock)
        {
            try { File.AppendAllText(_mdPath, line + "\n", Encoding.UTF8); }
            catch { }
        }
    }

    /// <summary>Append a formatted section to MD (AI response with optional DEBATE_JSON parsing).</summary>
    public void AppendMdAiResponse(string ai, string newText)
    {
        if (string.IsNullOrWhiteSpace(newText)) return;
        var emojiMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            { ["gpt"] = "🤖", ["gemini"] = "💎", ["claude"] = "🧠" };
        var emoji = emojiMap.GetValueOrDefault(ai, "🔮");
        AppendMd($"\n### {emoji} {ai.ToUpperInvariant()} ({DateTime.Now:HH:mm})\n\n{newText.Trim()}\n");
    }

    // ── EEP: Evidence Escalation Protocol — track claims per AI per round ──
    internal readonly ConcurrentDictionary<string, List<string>> _priorClaims = new(StringComparer.OrdinalIgnoreCase);
    internal readonly ConcurrentDictionary<string, int> _restatementCount = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Store claims for restatement detection in next round.</summary>
    public void StoreClaims(string ai, List<TriadDebateLoop.Claim> claims)
        => _priorClaims[ai] = claims.Select(c => c.Text).ToList();

    /// <summary>Check if a claim is a restatement (>70% keyword overlap with prior).</summary>
    public bool IsRestatement(string ai, string claimText)
    {
        if (!_priorClaims.TryGetValue(ai, out var prior)) return false;
        var curTokens = TriadDebateLoop.Tokenize(claimText);
        return prior.Any(p =>
        {
            var prevTokens = TriadDebateLoop.Tokenize(p);
            if (prevTokens.Count == 0) return false;
            var overlap = curTokens.Intersect(prevTokens).Count();
            return (double)overlap / prevTokens.Count >= 0.7;
        });
    }

    /// <summary>Increment restatement counter. Returns new count.</summary>
    public int IncrementRestatement(string ai)
        => _restatementCount.AddOrUpdate(ai, 1, (_, v) => v + 1);

    // ── Tool Discovery Sharing (큐 기반 — 경합 방지) ──
    private readonly ConcurrentDictionary<string, ConcurrentQueue<string>> _toolDiscoveryQueues =
        new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Push a tool discovery to all peer AIs' queues (not to source AI).</summary>
    public void PushDiscovery(string sourceAi, string summary)
    {
        foreach (var ai in _cdpClients.Keys)
        {
            if (ai.Equals(sourceAi, StringComparison.OrdinalIgnoreCase)) continue;
            var queue = _toolDiscoveryQueues.GetOrAdd(ai, _ => new ConcurrentQueue<string>());
            queue.Enqueue($"[TOOL from {sourceAi}] {summary}");
        }
    }

    /// <summary>Drain pending tool discoveries for a target AI. Call when AI is idle (between responses).</summary>
    public List<string> DrainDiscoveries(string targetAi)
    {
        var result = new List<string>();
        if (_toolDiscoveryQueues.TryGetValue(targetAi, out var queue))
            while (queue.TryDequeue(out var item))
                result.Add(item);
        return result;
    }

    /// <summary>Get the latest chunk for an AI (direct access, not peer-filtered).</summary>
    public string? GetLatestChunk(string ai) => _latestChunks.GetValueOrDefault(ai);

    /// <summary>DM: inject message to ONE AI only (no broadcast to peers). For warnings/corrections.</summary>
    public void InjectToSingle(string targetAi, string message)
    {
        if (!_cdpClients.TryGetValue(targetAi, out var cdp)) return;
        var editorSel = targetAi.ToLowerInvariant() switch
        {
            "gemini" => ".ql-editor",
            "gpt" or "chatgpt" => "#prompt-textarea",
            "claude" => "div.tiptap.ProseMirror",
            _ => null
        };
        if (editorSel == null) return;
        _ = Task.Run(async () =>
        {
            try
            {
                await cdp.InsertContentEditableAsync(editorSel, message);
                Console.WriteLine($"[MOD:DM] → {targetAi}: {message[..Math.Min(60, message.Length)]}");
            }
            catch { }
        });
    }

    /// <summary>Update latest response chunk + broadcast to Slack + queue for other AIs.</summary>
    public void UpdateChunk(string ai, string chunk)
    {
        var prev = _latestChunks.GetValueOrDefault(ai, "");
        _latestChunks[ai] = chunk;
        _chunkVersions.AddOrUpdate(ai, 1, (_, v) => v + 1);

        // Live MD: append new content (not full overwrite)
        var mdDelta = chunk.Length > prev.Length ? chunk[prev.Length..].Trim() : "";
        if (mdDelta.Length > 30)
            AppendMdAiResponse(ai, mdDelta);

        // First meaningful chunk → assign emoji (speed-based: first responder = 🦊)
        if (prev.Length == 0 && chunk.Length > 20)
            Program.AssignEmojiOnFinish(ai);

        // First chunk: check for STANCE compliance (only when moderator active, not R0)
        if (ModeratorEnabled && prev.Length == 0 && chunk.Length > 50)
        {
            var stance = TriadDebateLoop.ParseStance(chunk);
            if (stance != null)
            {
                // Post STANCE to Slack
                Program.SlackPostToThread($"📊 *[{Program.AiDisplayName(ai)} STANCE]* {stance} (sum={stance.Sum})", Program.AiDisplayName(ai));
            }
            if (stance == null)
            {
                // Moderator: queue STANCE demand — will be sent AFTER response completes
                _ = Task.Run(async () =>
                {
                    try
                    {
                        if (_cdpClients.TryGetValue(ai, out var selfCdp))
                        {
                            var editorSel = ai.ToLowerInvariant() switch
                            {
                                "gemini" => ".ql-editor",
                                "gpt" or "chatgpt" => "#prompt-textarea",
                                "claude" => "div.tiptap.ProseMirror",
                                _ => null
                            };
                            if (editorSel != null)
                            {
                                // Wait for response to finish (streaming ends)
                                for (int w = 0; w < 30; w++)
                                {
                                    await Task.Delay(2000);
                                    try
                                    {
                                        var streaming = await selfCdp.IsStreamingAsync(ai == "gpt" ? "chatgpt" : ai);
                                        if (!streaming) break;
                                    }
                                    catch { break; }
                                }
                                await Task.Delay(1000); // settle

                                var demand = "[MODERATOR DM] Your response was REJECTED — missing mandatory [STANCE]. Reply with ONLY this format:\n[STANCE N=? R=? C=? E=? D=?] (sum must equal 9)\nExample: [STANCE N=2 R=3 C=1 E=2 D=1]\nThen restate your key claim in one sentence.";
                                await selfCdp.InsertContentEditableAsync(editorSel, demand);
                                await Task.Delay(500);
                                await selfCdp.SendPromptAsync(editorSel); // auto-send!
                                Console.WriteLine($"[MOD] {ai}: STANCE missing → reject + re-request SENT");
                                Program.SlackPostToThread($"⚠️ *[MOD→{ai}]* STANCE missing! Response rejected, re-requested.", "🦉 Moderator");
                            }
                        }
                    }
                    catch (Exception ex) { Console.Error.WriteLine($"[MOD] {ai}: error: {ex.Message}"); }
                });
            }
        }

        // Only broadcast meaningful new content (delta > 100 chars)
        var delta = chunk.Length - prev.Length;
        if (delta < 100) return;

        var newText = chunk[prev.Length..].Trim();
        if (newText.Length < 50) return;

        // Post chunk to Slack immediately
        var slackSnippet = newText.Length > 200 ? newText[..200] + "..." : newText;
        Program.SlackPostToThread($"💬 *[{Program.AiDisplayName(ai)}]*: {slackSnippet}", Program.AiDisplayName(ai));

        // Inject cross-prompt into other AIs' editors immediately (editor is free during streaming)
        var snippet = newText.Length > 300 ? newText[..300] + "..." : newText;
        foreach (var kv in _cdpClients)
        {
            if (kv.Key.Equals(ai, StringComparison.OrdinalIgnoreCase)) continue;
            var peerCdp = kv.Value;
            var peerAi = kv.Key;
            var queue = _crossPromptQueues.GetOrAdd(peerAi, _ => new ConcurrentQueue<(string, string)>());
            queue.Enqueue((ai, snippet));

            // Cross-prompt always ON (streaming peer answers = natural nudge)
            // Pre-type into peer's editor + post to Slack
            _ = Task.Run(async () =>
            {
                try
                {
                    var editorSel = peerAi.ToLowerInvariant() switch
                    {
                        "gemini" => ".ql-editor",
                        "gpt" or "chatgpt" => "#prompt-textarea",
                        "claude" => "div.tiptap.ProseMirror",
                        _ => null
                    };
                    if (editorSel == null) return;

                    var crossText = $"[{ai} says]: {snippet}\nYour brief reaction? (ENGLISH ONLY)";
                    await peerCdp.InsertContentEditableAsync(editorSel, crossText);
                    Console.WriteLine($"[CROSS] {ai}→{peerAi}: pre-typed ({snippet.Length} chars)");

                    // Post delivery result to Slack
                    Program.SlackPostToThread($"🔀 *[{Program.AiDisplayName(ai)}→{peerAi} ✅]*", Program.AiDisplayName(ai));
                }
                catch { /* editor may not be ready — non-fatal */ }
            });
        }
    }

    /// <summary>Dequeue pending cross-prompts for an AI (call when AI is idle/between responses).</summary>
    public List<(string fromAi, string text)> DequeueCrossPrompts(string ai)
    {
        var result = new List<(string, string)>();
        if (_crossPromptQueues.TryGetValue(ai, out var queue))
            while (queue.TryDequeue(out var item)) result.Add(item);
        return result;
    }

    /// <summary>Get chunks from OTHER AIs (for cross-prompting). Returns only chunks newer than lastSeen versions.</summary>
    public List<(string ai, string chunk)> GetPeerChunks(string selfAi, ConcurrentDictionary<string, int> lastSeen)
    {
        var result = new List<(string ai, string chunk)>();
        foreach (var kv in _latestChunks)
        {
            if (kv.Key.Equals(selfAi, StringComparison.OrdinalIgnoreCase)) continue;
            var ver = _chunkVersions.GetValueOrDefault(kv.Key, 0);
            var seen = lastSeen.GetValueOrDefault(kv.Key, 0);
            if (ver > seen && kv.Value.Length > 50) // only meaningful chunks
            {
                result.Add((kv.Key, kv.Value));
                lastSeen[kv.Key] = ver;
            }
        }
        return result;
    }

    /// <summary>Check if an AI has finished its response (chunk stops updating).</summary>
    public bool IsAiDone(string ai) => _stepLogs.TryGetValue(ai, out var steps) && steps.Count > 0;

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

            SlackPostToThread($"🔄 *[복구]* `{AiDisplayName(ai)}` 실패 감지 — 컨텍스트 재주입 후 재시작 중...", AiDisplayName(ai));

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
                    ? $"✅ *[복구 성공]* `{AiDisplayName(ai)}` 응답 완료"
                    : $"❌ *[복구 실패]* `{AiDisplayName(ai)}` — 두 번째 시도도 실패";
                SlackPostToThread(status, AiDisplayName(ai));
            }
        }

        return result;
    }
}
