using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Slack Report ──

    // AsyncLocal: triad parent can pre-create a shared thread ts before spawning Task.Run children.
    // Each child Task inherits the value and posts to the same thread (no duplicate headers).
    internal static readonly System.Threading.AsyncLocal<string?> _slackSessionThreadTs = new();

    // Suppress Slack for internal sub-calls (e.g. whisper study ??AskAiForStudy).
    // Set to true before calling AskCommand internally to prevent channel noise.
    internal static readonly System.Threading.AsyncLocal<bool> _suppressSlackSession = new();

    // Per-page thread persistence: set to the sandbox key before calling EnsureSlackThread.
    // EnsureSlackThread uses this to look up / save a persistent thread per tab.
    // Triad parent sets _slackSessionThreadTs directly (bypasses per-page logic).
    internal static readonly System.Threading.AsyncLocal<string?> _askSandboxKey = new();

    // Current question ID for this ask execution (assigned by AssignNextQid).
    // 0 = no Q# (triad, internal sub-calls). Used to label Slack posts as [AI/Q#].
    internal static readonly System.Threading.AsyncLocal<int> _currentQid = new();

    // CDP client + host + editorSel for the current ask — set by each AI function so
    // SlackPostAnswerBlocks can inject re-education system messages.
    internal static readonly System.Threading.AsyncLocal<WKAppBot.WebBot.CdpClient?> _currentAskCdp = new();
    internal static readonly System.Threading.AsyncLocal<string?> _currentAskHost = new(); // "claude" | "gemini" | "chatgpt"
    internal static readonly System.Threading.AsyncLocal<string?> _currentAskEditorSel = new();

    // Persona posted flag ??triad posts persona once (first AI wins), others skip.
    static int _slackPersonaPostedFlag = 0; // Interlocked: 0=not posted, 1=posted

    // Per-session last-post tracker: sessionThreadTs ??(msgTs, username, text).
    // Used to detect "latest comment is mine" and append via chat.update instead of new post.
    // Tool ??markers update this too, so AI answers won't falsely append over tool messages.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string msgTs, string user, string text)>
        _slackThreadLastPost = new();
    const int SlackMaxAppendLength = 3800; // Slack message text limit (conservative)

    /// Post to thread and return the message ts (for later in-place updates). Null if no thread or error.
    /// Also updates _slackThreadLastPost so subsequent calls know the current "last poster".
    internal static string? SlackPostToThreadAndGetTs(string msg, string? username = null)
    {
        var threadTs = _slackSessionThreadTs.Value;
        if (threadTs == null) return null;
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;
            var uname = username ?? BotUsername;
            var (ok, ts) = SlackSendViaApi(botToken, channel, msg,
                threadTs: threadTs, username: uname).GetAwaiter().GetResult();
            if (ok && ts != null)
                _slackThreadLastPost[threadTs] = (ts, uname, msg);
            return ok ? ts : null;
        }
        catch { return null; }
    }

    /// Update a specific Slack message in-place by its ts. Noop if config unavailable.
    internal static void SlackUpdateThreadMessage(string msgTs, string text)
    {
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, msgTs, text).GetAwaiter().GetResult();
            if (!ok) Console.WriteLine($"[SLACK] SlackUpdateThreadMessage failed for ts={msgTs}");
        }
        catch { }
    }

    /// Post text to the current session's Slack thread. Noop if no active thread.
    /// If the latest thread message was posted by the same username, appends via chat.update
    /// instead of creating a new message (per user request: "if same author in thread, append instead of creating a new message").
    // Ratelimit guard: min 800ms between Slack posts (prevents Slack API 429)
    static long _lastSlackPostTicks;

    /// <summary>Active triad context for MD recording. Set by AskTriadParallel, cleared on exit.</summary>
    internal static TriadSharedContext? _activeTriadCtx;

    internal static void SlackPostToThread(string msg, string? username = null)
    {
        // Dual sink: always broadcast to local dashboard (even if Slack fails)
        DashboardBroadcaster.Emit("slack", msg, username ?? BotUsername, _slackSessionThreadTs.Value);

        // Live MD recording: append every slack message to MD transcript
        var mdUser = NormalizeMdSlackUsername(username ?? "system");
        var mdMsg = NormalizeMdSlackMessage(msg);
        if (ShouldRecordSlackMessageToMd(mdUser, mdMsg))
            _activeTriadCtx?.AppendMd($"> **{mdUser}** ({DateTime.Now:HH:mm}): {mdMsg}\n");

        var sessionTs = _slackSessionThreadTs.Value;
        if (sessionTs == null) return;
        // Debounce: wait if last post was <800ms ago
        var elapsed = (DateTime.UtcNow.Ticks - Interlocked.Read(ref _lastSlackPostTicks)) / TimeSpan.TicksPerMillisecond;
        if (elapsed < 800) System.Threading.Thread.Sleep(Math.Max(0, (int)(800 - elapsed)));
        Interlocked.Exchange(ref _lastSlackPostTicks, DateTime.UtcNow.Ticks);
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var uname = username ?? BotUsername;

            // ── Always merge bot replies: same author in thread -- edit with separator ──
            // No time limit ??bot replies always merge. Keeps thread compact + saves quota.
            if (_slackThreadLastPost.TryGetValue(sessionTs, out var last) && last.user == uname)
            {
                var iconEmoji = ExtractDebateIconEmoji(uname);
                var sepIcon = !string.IsNullOrEmpty(iconEmoji) ? $"{iconEmoji} " : "";
                var timeMark = SmartTimeMark(last.text);
                var combined = last.text + $"\n{sepIcon}*{uname}* | {timeMark} |\n" + msg;

                if (combined.Length <= SlackMaxAppendLength)
                {
                    var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, last.msgTs, combined).GetAwaiter().GetResult();
                    if (ok)
                    {
                        _slackThreadLastPost[sessionTs] = (last.msgTs, uname, combined);
                        return;
                    }
                }
                // Too long or update failed ??fall through to post new message
                // (message_limit fallback in SlackSendViaApi will trim if needed)
            }

            // Post new message (chunked for long content)
            const int chunkSize = 3000;
            string? newMsgTs = null;
            for (int pos = 0; pos < msg.Length; pos += chunkSize)
            {
                var chunk = msg[pos..Math.Min(pos + chunkSize, msg.Length)];
                // Extract emoji from username for icon_emoji (e.g. "🥇 GPT(SKEPTIC)" ??":fox:")
                var iconEmoji = ExtractDebateIconEmoji(uname);
                var (ok, ts) = SlackSendViaApiWithIcon(botToken, channel, chunk,
                    threadTs: sessionTs, username: uname, iconEmoji: iconEmoji).GetAwaiter().GetResult();
                if (ok && ts != null && pos == 0) newMsgTs = ts;
            }
            if (newMsgTs != null)
                _slackThreadLastPost[sessionTs] = (newMsgTs, uname, msg.Length > SlackMaxAppendLength ? msg[..SlackMaxAppendLength] : msg);
        }
        catch { }
    }

    /// <summary>
    /// Parse [An] answer blocks from AI response.
    /// Returns dict of qid → answer text. Empty if no [An] markers found.
    /// Format: "[A3] answer text..." — may span multiple lines until next [An] or EOF.
    /// </summary>
    internal static Dictionary<int, string> ParseAnswerBlocks(string response)
    {
        var result = new Dictionary<int, string>();
        if (string.IsNullOrWhiteSpace(response)) return result;

        // Find all [An] positions
        var rx = System.Text.RegularExpressions.Regex.Matches(
            response,
            @"\[A(\d+)\]",
            System.Text.RegularExpressions.RegexOptions.None);

        for (int i = 0; i < rx.Count; i++)
        {
            if (!int.TryParse(rx[i].Groups[1].Value, out var qid)) continue;
            var start = rx[i].Index + rx[i].Length;
            var end = i + 1 < rx.Count ? rx[i + 1].Index : response.Length;
            var block = response[start..end].Trim();
            if (!string.IsNullOrWhiteSpace(block))
                result[qid] = block;
        }
        return result;
    }

    /// <summary>
    /// Inject a re-education system message into the current AI tab and wait for the corrected response.
    /// Returns the new response text, or null if re-education failed or timed out.
    /// </summary>
    static async Task<string?> ReEducateAsync(WKAppBot.WebBot.CdpClient cdp, string host, string editorSel, int qid)
    {
        var sysMsg = $"[SYSTEM] Format error: your response must begin with [A{qid}] on its own line. " +
                     $"Restate your full answer starting with exactly \"[A{qid}]\" and nothing before it.";
        Console.WriteLine($"[REEDUCATE] Q{qid}: [A{qid}] 마커 없음 → 재교육 메시지 주입...");

        await cdp.ClearEditorAsync(editorSel);
        await Task.Delay(100);
        var inserted = await cdp.InsertContentEditableAsync(editorSel, sysMsg);
        if (!inserted)
        {
            Console.WriteLine($"[REEDUCATE] Q{qid}: 에디터 입력 실패");
            return null;
        }
        await Task.Delay(200);
        await cdp.SendPromptAsync(editorSel);

        // Wait for AI to start generating (stop button appears, up to 10s)
        var stopSels = WKAppBot.WebBot.CdpSelectorRegistry.Get(host, "stop");
        for (int w = 0; w < 20; w++)
        {
            await Task.Delay(500);
            var started = false;
            foreach (var s in stopSels)
            {
                var esc = s.Replace("'", "\\'");
                var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                if (vis == "1") { started = true; break; }
            }
            if (started) break;
        }

        // Wait for AI to finish generating (stop button disappears, up to 120s)
        for (int w = 0; w < 240; w++)
        {
            await Task.Delay(500);
            var still = false;
            foreach (var s in stopSels)
            {
                var esc = s.Replace("'", "\\'");
                var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                if (vis == "1") { still = true; break; }
            }
            if (!still) break;
        }

        // Read the latest AI response from DOM using registry selectors
        var msgSels = WKAppBot.WebBot.CdpSelectorRegistry.Get(host, "message");
        // Build JS that tries each selector and returns innerText of last matched element
        var selsJson = string.Join(",", msgSels.Select(s => $"'{WKAppBot.WebBot.CdpClient.Esc(s)}'"));
        var js = $"(()=>{{var sels=[{selsJson}];for(var i=0;i<sels.length;i++){{var els=document.querySelectorAll(sels[i]);if(els.length>0)return els[els.length-1].innerText||'';}}return '';}})()";

        var newResponse = await cdp.EvalAsync(js);
        if (string.IsNullOrWhiteSpace(newResponse))
        {
            Console.WriteLine($"[REEDUCATE] Q{qid}: 응답 DOM 읽기 실패");
            return null;
        }
        Console.WriteLine($"[REEDUCATE] Q{qid}: 재교육 응답 수신 ({newResponse.Length}자)");
        return newResponse;
    }

    /// <summary>
    /// Post per-question answer blocks to Slack as labeled replies.
    /// Called after receiving AI response when Q# was assigned.
    /// If response has no [An] markers, injects a re-education CDP message (max 1 retry).
    /// </summary>
    internal static void SlackPostAnswerBlocks(string response, string aiLabel)
    {
        var blocks = ParseAnswerBlocks(response);
        var qid = _currentQid.Value;

        // Re-education: Q# assigned but AI didn't use [An] format → inject correction
        if (blocks.Count == 0 && qid > 0)
        {
            var cdp = _currentAskCdp.Value;
            var host = _currentAskHost.Value;
            var editorSel = _currentAskEditorSel.Value;
            if (cdp != null && host != null && editorSel != null)
            {
                var retryResponse = Task.Run(() => ReEducateAsync(cdp, host, editorSel, qid)).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(retryResponse))
                {
                    blocks = ParseAnswerBlocks(retryResponse);
                    if (blocks.Count > 0)
                    {
                        Console.WriteLine($"[REEDUCATE] Q{qid}: 재교육 성공 — {blocks.Count}개 블록 파싱됨");
                        response = retryResponse;
                    }
                    else
                    {
                        Console.WriteLine($"[REEDUCATE] Q{qid}: 재교육 후에도 [An] 없음 — 스킵");
                    }
                }
            }
        }

        if (blocks.Count == 0) return;

        var threadTs = _slackSessionThreadTs.Value;
        if (string.IsNullOrEmpty(threadTs)) return;

        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            foreach (var (blockQid, answer) in blocks.OrderBy(kv => kv.Key))
            {
                var label = $"{aiLabel}/A{blockQid}";
                var truncated = answer.Length > 2800 ? answer[..2800] + "..." : answer;
                SlackSendViaApi(botToken, channel, $"*[{label}]* {truncated}",
                    threadTs: threadTs, username: GetSendReplyUsername())
                    .GetAwaiter().GetResult();
                Console.WriteLine($"[ASK] Slack A{blockQid} 전송 ({answer.Length}자)");
            }
        }
        catch { /* best effort */ }
    }

    static string NormalizeMdSlackUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return "system";
        return username.Replace("\r", " ").Replace("\n", " ").Trim();
    }

    static string NormalizeMdSlackMessage(string msg)
    {
        if (string.IsNullOrWhiteSpace(msg)) return msg;

        var text = msg.Replace("\r\n", "\n").Trim();
        var decoratedPrefix = System.Text.RegularExpressions.Regex.Match(
            text,
            @"^(?:\S+\s+)?\*\[[^\]]+\]\*:\s*",
            System.Text.RegularExpressions.RegexOptions.CultureInvariant);
        if (decoratedPrefix.Success)
            text = text[decoratedPrefix.Length..].TrimStart();

        var qMarker = text.IndexOf("[Q:", StringComparison.Ordinal);
        if (qMarker > 0)
            text = text[qMarker..];

        text = StripLeadingNoiseLines(text);
        return string.IsNullOrWhiteSpace(text) ? msg.Trim() : text;
    }

    static string StripLeadingNoiseLines(string text)
    {
        var lines = text.Split('\n').ToList();
        while (lines.Count > 0)
        {
            var first = lines[0].Trim();
            if (string.IsNullOrWhiteSpace(first))
            {
                lines.RemoveAt(0);
                continue;
            }
            if (first.StartsWith("[Q:", StringComparison.Ordinal) ||
                first.StartsWith("[G:", StringComparison.Ordinal))
                break;

            var alnumCount = first.Count(char.IsLetterOrDigit);
            var hasReplacement = first.Contains('\uFFFD');
            var hasBracketedSpeaker = System.Text.RegularExpressions.Regex.IsMatch(
                first,
                @"^\*\[[^\]]+\]\*:?$",
                System.Text.RegularExpressions.RegexOptions.CultureInvariant);
            var mostlySymbolic = first.Length <= 24 && alnumCount <= 6;
            var suspiciousPrefix = first.StartsWith("?:", StringComparison.Ordinal) ||
                                   first.StartsWith("?") ||
                                   hasBracketedSpeaker;

            if (hasReplacement || mostlySymbolic || suspiciousPrefix)
            {
                lines.RemoveAt(0);
                continue;
            }

            break;
        }

        return string.Join('\n', lines).TrimStart();
    }

    static bool ShouldRecordSlackMessageToMd(string username, string msg)
    {
        if (string.IsNullOrWhiteSpace(msg)) return false;

        // AI content itself is already recorded by AppendMdAiResponse().
        // Keep moderator/system control messages, but drop duplicate chunk relays.
        var isAiSpeaker =
            username.Contains("GPT(", StringComparison.OrdinalIgnoreCase) ||
            username.Contains("Gemini(", StringComparison.OrdinalIgnoreCase) ||
            username.Contains("Claude(", StringComparison.OrdinalIgnoreCase);
        if (isAiSpeaker)
        {
            if (msg.StartsWith("TITLE:", StringComparison.OrdinalIgnoreCase)) return false;
            if (msg.StartsWith("FILE_TITLE:", StringComparison.OrdinalIgnoreCase)) return false;
            if (msg.StartsWith("[CROSS ", StringComparison.Ordinal)) return false;
            if (msg.StartsWith("[MOD ", StringComparison.Ordinal)) return false;
        }

        return true;
    }

    /// <summary>Map Unicode emoji in username to Slack :shortcode: for icon_emoji.</summary>
    static string? ExtractDebateIconEmoji(string? username) => username switch
    {
        _ when username?.Contains("\u2696\uFE0F") == true => ":scales:",
        _ when username?.Contains("\U0001F98A") == true => ":fox_face:",
        _ when username?.Contains("\U0001F42C") == true => ":dolphin:",
        _ when username?.Contains("\U0001F419") == true => ":octopus:",
        _ when username?.Contains("\U0001F6A7") == true => ":construction:",
        _ => null,
    };

    /// <summary>SlackSendViaApi wrapper that injects icon_emoji for debate posts.</summary>
    static async Task<(bool ok, string? ts)> SlackSendViaApiWithIcon(
        string botToken, string channel, string text,
        string? threadTs = null, string? username = null, string? iconEmoji = null)
    {
        using var http = new HttpClient();
        var dict = new Dictionary<string, object> { ["channel"] = channel, ["text"] = text };
        if (!string.IsNullOrEmpty(threadTs)) dict["thread_ts"] = threadTs;
        if (!string.IsNullOrEmpty(username)) dict["username"] = username;
        if (!string.IsNullOrEmpty(iconEmoji)) dict["icon_emoji"] = iconEmoji;
        else
        {
            // Fall back to workspace custom icon
            var callerCwd = EyeCmdPipeServer.CallerCwd.Value;
            var icon = GetCustomSlackIcon(callerCwd);
            if (icon != null)
            {
                if (icon.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    dict["icon_url"] = icon;
                else
                    dict["icon_emoji"] = icon;
            }
        }
        var payload = System.Text.Json.JsonSerializer.Serialize(dict);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;
        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = System.Text.Json.JsonDocument.Parse(body);
        var ok = json.RootElement.GetProperty("ok").GetBoolean();
        var ts = json.RootElement.TryGetProperty("ts", out var tsProp) ? tsProp.GetString() : null;
        return (ok, ts);
    }

    /// <summary>
    /// Open a Slack thread for the current session if not already open.
    /// Returns the thread ts. Safe to call multiple times (idempotent per AsyncLocal context).
    /// </summary>
    internal static string? EnsureSlackThread(string label, string question)
    {
        if (_suppressSlackSession.Value) return null; // internal sub-call (e.g. whisper study)
        if (_slackSessionThreadTs.Value != null)
            return _slackSessionThreadTs.Value;   // already opened by triad parent

        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;

            var qTrunc = question.Length > 200 ? question[..200] + "..." : question;
            // Include Q# in label if assigned (e.g. "Claude/Q5")
            var qid = _currentQid.Value;
            var labelWithQ = qid > 0 ? $"{label}/Q{qid}" : label;

            // Per-page persistence: reuse existing thread for the same browser tab
            var sandboxKey = _askSandboxKey.Value;
            if (!string.IsNullOrEmpty(sandboxKey))
            {
                var existingTs = AskTargetRegistry.GetSlackThread(sandboxKey);
                if (!string.IsNullOrEmpty(existingTs))
                {
                    // Reuse existing thread: post new question as a reply separator
                    _slackSessionThreadTs.Value = existingTs;
                    var sep = $"─── *[{labelWithQ}]* ───\n{qTrunc}";
                    SlackSendViaApi(botToken, channel, sep,
                        threadTs: existingTs, username: GetSendReplyUsername())
                        .GetAwaiter().GetResult();
                    Console.WriteLine($"[ASK] Per-page Slack thread reused: {existingTs} ({sandboxKey})");
                    return existingTs;
                }
            }

            // No existing thread — create new top-level post
            var (ok, ts) = SlackSendViaApi(botToken, channel, $"*[{labelWithQ}]* {qTrunc}", username: GetSendReplyUsername())
                               .GetAwaiter().GetResult();
            if (ok && ts != null)
            {
                _slackSessionThreadTs.Value = ts;
                // Persist for per-page reuse
                if (!string.IsNullOrEmpty(sandboxKey))
                    AskTargetRegistry.SetSlackThread(sandboxKey, ts);
            }
            return ok ? ts : null;
        }
        catch { return null; }
    }

    // ── Agent stop flag (--terminate / --stop-agent) ──────────────────────────
    // Flag file: runtime/agent_stop_{sanitized_key}.flag
    // Loop checks this at each step; --terminate writes it; clear on loop exit.

    static string AgentStopFlagPath(string key)
    {
        var safe = key.Replace('+', '-').Replace('/', '_').Replace('\\', '_').Replace(' ', '_');
        return Path.Combine(DataDir, "runtime", $"agent_stop_{safe}.flag");
    }

    /// <summary>Check if a stop flag exists for the current async context's sandbox key.</summary>
    internal static bool AgentStopFlagExists()
    {
        var key = _askSandboxKey.Value;
        if (string.IsNullOrEmpty(key)) return false;
        try { return File.Exists(AgentStopFlagPath(key)); } catch { return false; }
    }

    internal static void AgentStopFlagCreate(string key)
    {
        try
        {
            Directory.CreateDirectory(Path.Combine(DataDir, "runtime"));
            File.WriteAllText(AgentStopFlagPath(key), DateTime.UtcNow.ToString("O"));
        }
        catch { /* best effort */ }
    }

    internal static void AgentStopFlagClear(string key)
    {
        try { File.Delete(AgentStopFlagPath(key)); } catch { /* best effort */ }
    }

    /// <summary>
    /// 훈수두기: inject msg into existing AI tab (CDP) + post to Slack thread.
    ///   triad/all → Slack-only (reads runtime/last_triad_ts.txt)
    ///   gemini/gpt/claude → CDP inject into tab + Slack thread
    ///   threadKey → agent mode (e.g. "claude-agent-myjob")
    /// Tab found → CDP inject (세션 유지, 초기화 없이 메시지 추가)
    /// Tab not found → Slack-only fallback (warn)
    /// </summary>
    static int AskIntercept(string ai, string msg, string? label = null, string? threadKey = null, bool interrupt = false)
    {
        var config = LoadSlackConfig();
        var botToken = config?["bot_token"]?.GetValue<string>();
        var channel  = config?["channel"]?.GetValue<string>();
        var threadLabel = label ?? ai;
        var mode = interrupt ? "인터럽트" : "훈수";

        // ── CDP injection (tab must exist) ─────────────────────────────────────
        bool cdpOk = false;
        if (ai is not ("triad" or "all"))
        {
            var regKey = threadKey ?? BuildSandboxKey("ask", ai);
            var entry = AskTargetRegistry.GetEntry(regKey);
            if (entry != null)
            {
                cdpOk = Task.Run(() => InjectIntoCdpTabAsync(entry.TargetId, ai, msg, interrupt)).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine($"[{mode}] 탭 없음 ({regKey}) — Slack 전송만 합니다.");
            }
        }

        // ── Slack post ─────────────────────────────────────────────────────────
        string? threadTs = null;
        if (!string.IsNullOrEmpty(threadKey))
        {
            threadTs = AskTargetRegistry.GetSlackThread(threadKey);
        }
        else if (ai is "triad" or "all")
        {
            var tsFile = Path.Combine(DataDir, "runtime", "last_triad_ts.txt");
            if (File.Exists(tsFile)) threadTs = File.ReadAllText(tsFile).Trim();
        }
        else
        {
            threadTs = AskTargetRegistry.GetSlackThread(threadKey ?? BuildSandboxKey("ask", ai));
        }

        if (string.IsNullOrEmpty(threadTs))
        {
            Console.WriteLine($"[{mode}] Slack 쓰레드 없음 ({threadLabel}). ask/agent 먼저 실행하세요.");
            return cdpOk ? 0 : 1;
        }

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine($"[{mode}] Slack config 없음 — CDP만 동작");
            return cdpOk ? 0 : 1;
        }

        var emoji = interrupt ? "⚡" : "👀";
        var cdpTag = cdpOk ? "💉 CDP+Slack" : "📨 Slack";
        var (ok, _) = SlackSendViaApi(botToken, channel, $"{emoji} *[{mode}]* {msg}",
            threadTs: threadTs, username: GetSendReplyUsername())
            .GetAwaiter().GetResult();

        Console.WriteLine(ok
            ? $"[{mode}] {cdpTag} 전달완료 → {threadLabel} thread={threadTs}"
            : $"[{mode}] Slack 전송 실패 (CDP: {(cdpOk ? "OK" : "SKIP")})");

        return (cdpOk || ok) ? 0 : 1;
    }

    /// <summary>
    /// Connect to an existing Chrome tab by targetId and inject msg into the AI editor.
    /// Waits up to 15s if AI is currently generating before injecting.
    /// Returns true on successful send.
    /// </summary>
    static async Task<bool> InjectIntoCdpTabAsync(string targetId, string ai, string msg, bool interrupt = false)
    {
        var host = ai switch { "claude" => "claude", "gemini" => "gemini", _ => "chatgpt" };
        var mode = interrupt ? "인터럽트" : "훈수";
        WKAppBot.WebBot.CdpClient? cdp = null;
        try
        {
            cdp = new WKAppBot.WebBot.CdpClient();
            await cdp.ConnectAsync(9222, timeoutMs: 3000);
            var switched = await cdp.SwitchToTargetAsync(targetId, 9222);
            if (!switched)
            {
                Console.WriteLine($"[{mode}] CDP: 탭 전환 실패 (targetId={targetId[..Math.Min(8, targetId.Length)]})");
                return false;
            }

            var stopSels = WKAppBot.WebBot.CdpSelectorRegistry.Get(host, "stop");

            if (interrupt)
            {
                // ⚡ CPU 인터럽트 방식: stop 버튼 클릭 → 즉시 inject
                foreach (var s in stopSels)
                {
                    var esc = s.Replace("'", "\\'");
                    var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                    if (vis == "1")
                    {
                        Console.WriteLine($"[{mode}] AI 응답 중단 → 즉시 주입");
                        await cdp.ClickStopButtonAsync();
                        await Task.Delay(300); // brief settle
                        break;
                    }
                }
            }
            else
            {
                // 👀 훈수 방식: AI 응답 완료 대기 (최대 15s)
                for (int w = 0; w < 30; w++)
                {
                    var generating = false;
                    foreach (var s in stopSels)
                    {
                        var esc = s.Replace("'", "\\'");
                        var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                        if (vis == "1") { generating = true; break; }
                    }
                    if (!generating) break;
                    if (w == 0) Console.WriteLine($"[{mode}] AI 응답 중 — 완료 대기 (최대 15s)...");
                    await Task.Delay(500);
                }
            }

            // Find editor
            var editorSels = WKAppBot.WebBot.CdpSelectorRegistry.Get(host, "editor");
            string? editorSel = null;
            foreach (var s in editorSels)
            {
                var esc = s.Replace("'", "\\'");
                var found = await cdp.EvalAsync($"!!document.querySelector('{esc}')");
                if (found == "true") { editorSel = s; break; }
            }
            if (editorSel == null)
            {
                Console.WriteLine($"[{mode}] CDP: 에디터 없음");
                return false;
            }

            // Clear any leftover text, insert message, send
            await cdp.ClearEditorAsync(editorSel);
            await Task.Delay(100);
            var inserted = await cdp.InsertContentEditableAsync(editorSel, msg);
            if (!inserted)
            {
                Console.WriteLine($"[{mode}] CDP: 텍스트 입력 실패");
                return false;
            }
            await Task.Delay(200);
            await cdp.SendPromptAsync(editorSel);
            Console.WriteLine($"[{mode}] CDP: {host} 탭에 주입 완료");

            if (interrupt)
            {
                // ⚡ Auto-resume: wait for AI response → inject "continue previous task"
                // Wait for AI to start generating (stop button appears, up to 10s)
                Console.WriteLine("[인터럽트] AI 응답 대기 중...");
                for (int w = 0; w < 20; w++)
                {
                    await Task.Delay(500);
                    var started = false;
                    foreach (var s in stopSels)
                    {
                        var esc = s.Replace("'", "\\'");
                        var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                        if (vis == "1") { started = true; break; }
                    }
                    if (started) break;
                }
                // Wait for AI to finish generating (stop button disappears, up to 60s)
                for (int w = 0; w < 120; w++)
                {
                    await Task.Delay(500);
                    var still = false;
                    foreach (var s in stopSels)
                    {
                        var esc = s.Replace("'", "\\'");
                        var vis = await cdp.EvalAsync($"(()=>{{var b=document.querySelector('{esc}');return b&&b.offsetParent!==null?'1':'0'}})()");
                        if (vis == "1") { still = true; break; }
                    }
                    if (!still) break;
                }
                // Auto-inject resume prompt
                await cdp.ClearEditorAsync(editorSel);
                await Task.Delay(100);
                await cdp.InsertContentEditableAsync(editorSel, "[INTERRUPT HANDLED] Continue with your previous task.");
                await Task.Delay(200);
                await cdp.SendPromptAsync(editorSel);
                Console.WriteLine("[인터럽트] 자동 복귀 완료 → AI가 이전 작업 재개");
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{mode}] CDP 오류: {ex.Message}");
            return false;
        }
        finally
        {
            cdp?.Dispose();
        }
    }

    /// <summary>
    /// --terminate / --stop-agent: write stop flag → loop exits cleanly at next step.
    /// Also clicks stop button via CDP if AI is currently generating.
    /// </summary>
    static int AskTerminate(string ai, string? threadKey = null)
    {
        var key = threadKey ?? BuildSandboxKey("ask", ai);
        AgentStopFlagCreate(key);
        Console.WriteLine($"[TERMINATE] 중단 플래그 생성 → {key}");

        // Also click stop button in CDP tab (immediate effect while loop waits)
        var entry = AskTargetRegistry.GetEntry(key);
        if (entry != null)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    var cdp = new WKAppBot.WebBot.CdpClient();
                    await cdp.ConnectAsync(9222, timeoutMs: 3000);
                    await cdp.SwitchToTargetAsync(entry.TargetId, 9222);
                    var result = await cdp.ClickStopButtonAsync();
                    Console.WriteLine($"[TERMINATE] CDP stop: {result}");
                    cdp.Dispose();
                }
                catch { /* best effort */ }
            });
        }

        // Post to Slack thread
        var threadTs = AskTargetRegistry.GetSlackThread(key);
        if (!string.IsNullOrEmpty(threadTs))
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
                SlackSendViaApi(botToken, channel, $"🛑 *[TERMINATE]* 에이전트 중단 요청됨",
                    threadTs: threadTs, username: GetSendReplyUsername()).GetAwaiter().GetResult();
        }
        return 0;
    }

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel  = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            // Open (or reuse) the session thread ??triad shares one thread, single AI gets its own
            var ts = EnsureSlackThread(aiName, question);
            if (ts == null) { Console.WriteLine("[ASK] Slack report: no thread ts"); return; }

            // Thread: answer in 3000-char chunks (no truncation ??split as needed)
            const int chunkSize = 3000;
            int pos = 0, part = 1;
            while (pos < answer.Length)
            {
                var chunk = answer[pos..Math.Min(pos + chunkSize, answer.Length)];
                SlackSendViaApi(botToken, channel, chunk, threadTs: ts, username: GetSendReplyUsername()).GetAwaiter().GetResult();
                pos += chunkSize;
                part++;
            }
            Console.WriteLine($"[ASK] Reported to Slack (thread {ts}, {part - 1} part(s))");
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "Slack report failed", ex);
        }
    }
}

