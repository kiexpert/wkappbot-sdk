using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

internal partial class Program
{
    const string LoopCallBegin = "[APPBOT_TOOL_CALL_BEGIN]";
    const string LoopCallEnd = "[APPBOT_TOOL_CALL_END]";
    const string ClaudeUsageUrl = "https://claude.ai/settings/usage";
    static readonly string LoopPersonaStateVersion = ComputeLoopPersonaStateHash();

    static string ComputeLoopPersonaStateHash()
    {
        const string seed =
            "loop-persona|mcp-schema|ask_gemini(argv:string[])|json-block-markers-v1";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        return Convert.ToHexString(bytes)[..12];
    }

    static string FormatClaudeLimitResponse(string rawText)
    {
        var clean = ExtractClaudeLimitExcerpt(rawText);
        return
            "[CLAUDE_LIMIT] Claude 메시지 한도에 도달했습니다.\n" +
            $"사용량 확인: {ClaudeUsageUrl}\n\n" +
            clean;
    }

    static bool IsClaudeLimitResponse(string? text)
        => !string.IsNullOrWhiteSpace(text)
           && text.StartsWith("[CLAUDE_LIMIT]", StringComparison.Ordinal);

    static string ExtractClaudeLimitExcerpt(string? rawText)
    {
        var raw = (rawText ?? "").Replace("\r", "");
        if (string.IsNullOrWhiteSpace(raw))
            return "한도 안내 문구를 찾지 못했습니다.";

        var normalized = System.Text.RegularExpressions.Regex.Replace(raw, @"\s+", " ").Trim();
        var rxPatterns = new[]
        {
            @"사용량\s*한도\s*도달[^.!\n]*",
            @"Claude\s*메시지\s*한도에\s*도달했습니다[^.!\n]*",
            @"제한이\s*[^.!\n]*재설정[^.!\n]*",
            @"usage\s*limit[^.!\n]*",
            @"rate\s*limit[^.!\n]*",
            @"limit\s*reached[^.!\n]*"
        };
        var rxHits = new List<string>();
        foreach (var p in rxPatterns)
        {
            var m = System.Text.RegularExpressions.Regex.Match(
                normalized,
                p,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (m.Success)
                rxHits.Add(m.Value.Trim());
            if (rxHits.Count >= 3)
                break;
        }
        if (rxHits.Count > 0)
            return string.Join("\n", rxHits.Distinct().Take(3));

        var keys = new[]
        {
            "사용량 한도",
            "메시지 한도",
            "한도에 도달",
            "제한이",
            "재설정",
            "usage limit",
            "rate limit",
            "limit reached",
            "reset"
        };

        var lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var picked = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (keys.Any(k => line.Contains(k, StringComparison.OrdinalIgnoreCase)))
            {
                picked.Add(line);
                if (i + 1 < lines.Length && picked.Count < 3)
                    picked.Add(lines[i + 1]);
                if (picked.Count >= 3)
                    break;
            }
        }

        if (picked.Count == 0)
        {
            var fallback = raw.Trim();
            if (fallback.Length > 260)
                fallback = fallback[..260] + "...";
            return fallback;
        }

        var compact = string.Join("\n", picked.Distinct().Take(3)).Trim();
        if (compact.Length > 320)
            compact = compact[..320] + "...";
        return compact;
    }

    static string BuildAskPersona(bool loopMode, bool triadMode, int maxSteps, int retry, string? modelHint = null)
    {
        if (!loopMode && string.IsNullOrWhiteSpace(modelHint))
            return AskPersona.Trim();

        var sb = new StringBuilder();
        sb.Append(AskPersona.Trim());
        sb.Append(' ');
        if (!string.IsNullOrWhiteSpace(modelHint))
            sb.Append($"Preferred remote model/version hint: {modelHint}. ");
        if (!loopMode)
            return sb.ToString();

        sb.Append("LOOP MODE ENABLED. You can request host execution via a strict JSON block. ");
        sb.Append($"Max loop steps: {Math.Max(1, maxSteps)}. ");
        sb.Append($"Per-step retry budget: {Math.Max(0, retry)}. ");
        if (triadMode)
            sb.Append("Use TRIAD planning: Observation -> Action -> Verification. ");
        sb.Append("Available host MCP-style capabilities include UIA automation (a11y/find/click/type/wait), web automation, shell command execution wrappers, screenshots, file operations, and reporting commands. ");
        sb.Append("Tool schema available in this session: ");
        sb.Append("tool ask_gemini(args) where args is JSON object with required field argv:string[]; ");
        sb.Append("argv must start with [\"ask\",\"gemini\",<question>] and may include \"--timeout\",<seconds>. ");
        sb.Append("Do not claim missing schema; this schema is authoritative for this session. ");
        sb.Append("When you need execution, output exactly one block with no extra keys: ");
        sb.Append(LoopCallBegin);
        sb.Append("{\"argv\":[\"a11y\",\"find\",\"*Chrome*\"]}");
        sb.Append(LoopCallEnd);
        sb.Append(" After tool_result, continue or finish with normal answer.");
        return sb.ToString();
    }

    static async Task<bool> HasLoopPersonaStateAsync(CdpClient cdp, string provider)
    {
        var key = $"wkappbot.loop.persona.{provider}";
        var script =
            "(() => {" +
            "try {" +
            $"var k = '{key}';" +
            $"if (sessionStorage.getItem(k) === '{LoopPersonaStateVersion}') return '1';" +
            $"if (localStorage.getItem(k) === '{LoopPersonaStateVersion}') return '1';" +
            $"if (window[k] === '{LoopPersonaStateVersion}') return '1';" +
            "} catch (e) {}" +
            "return '0';" +
            "})()";
        var result = await cdp.EvalAsync(script) ?? "0";
        return result == "1";
    }

    static async Task SetLoopPersonaStateAsync(CdpClient cdp, string provider)
    {
        var key = $"wkappbot.loop.persona.{provider}";
        var script =
            "(() => {" +
            "try {" +
            $"var k = '{key}';" +
            $"sessionStorage.setItem(k, '{LoopPersonaStateVersion}');" +
            $"localStorage.setItem(k, '{LoopPersonaStateVersion}');" +
            $"window[k] = '{LoopPersonaStateVersion}';" +
            "} catch (e) {}" +
            "return 'OK';" +
            "})()";
        await cdp.EvalAsync(script);
    }

    static async Task<(bool ok, string? text)> RunChatGptLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (!TryParseLoopToolCall(answer, out var argv, out var parseError))
            {
                if (!string.IsNullOrWhiteSpace(parseError))
                    Console.WriteLine($"[ASK:LOOP] Parse skipped: {parseError}");
                return (true, answer);
            }

            Console.WriteLine($"[ASK:LOOP] Step {step}/{steps}: {string.Join(" ", argv)}");
            var exec = await ExecuteLoopCommandAsync(argv, timeoutSec, retries, "gpt");

            var toolResultPrompt =
                "TOOL_RESULT (host executed your request):\n" +
                "executed_by: gpt\n" +
                $"exit_code: {exec.exitCode}\n" +
                $"stdout:\n{exec.stdout}\n" +
                $"stderr:\n{exec.stderr}\n" +
                "If more actions are required, emit the next APPBOT_TOOL_CALL block. Otherwise provide final answer.";

            var (ok, next) = await ChatGptSendAndWait(cdp, toolResultPrompt, timeoutSec);
            if (!ok || string.IsNullOrWhiteSpace(next))
            {
                Console.WriteLine("[ASK:LOOP] ChatGPT follow-up timed out. Returning last stable answer.");
                return (true, answer);
            }

            answer = next;
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    static async Task<(bool ok, string? text)> RunGeminiLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (!TryParseLoopToolCall(answer, out var argv, out _))
                return (true, answer);

            Console.WriteLine($"[ASK:LOOP] Gemini step {step}/{steps}: {string.Join(" ", argv)}");
            var exec = await ExecuteLoopCommandAsync(argv, timeoutSec, retries, "gemini");

            var prompt =
                "TOOL_RESULT (host executed your request):\n" +
                "executed_by: gemini\n" +
                $"exit_code: {exec.exitCode}\n" +
                $"stdout:\n{exec.stdout}\n" +
                $"stderr:\n{exec.stderr}\n" +
                "If more actions are required, emit the next APPBOT_TOOL_CALL block. Otherwise provide final answer.";

            var (ok, next) = await GeminiSendAndWaitAsync(cdp, prompt, timeoutSec);
            if (!ok || string.IsNullOrWhiteSpace(next))
            {
                Console.WriteLine("[ASK:LOOP] Gemini follow-up timed out. Returning last stable answer.");
                return (true, answer);
            }
            answer = next;
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    static async Task<(bool ok, string? text)> RunClaudeLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (!TryParseLoopToolCall(answer, out var argv, out _))
                return (true, answer);

            Console.WriteLine($"[ASK:LOOP] Claude step {step}/{steps}: {string.Join(" ", argv)}");
            var exec = await ExecuteLoopCommandAsync(argv, timeoutSec, retries, "claude");

            var prompt =
                "TOOL_RESULT (host executed your request):\n" +
                "executed_by: claude\n" +
                $"exit_code: {exec.exitCode}\n" +
                $"stdout:\n{exec.stdout}\n" +
                $"stderr:\n{exec.stderr}\n" +
                "If more actions are required, emit the next APPBOT_TOOL_CALL block. Otherwise provide final answer.";

            var (ok, next) = await ClaudeSendAndWaitAsync(cdp, prompt, timeoutSec);
            if (!ok || string.IsNullOrWhiteSpace(next))
            {
                Console.WriteLine("[ASK:LOOP] Claude follow-up timed out. Returning last stable answer.");
                return (true, answer);
            }
            answer = next;
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    static async Task<(bool ok, string? text)> ClaudeSendAndWaitAsync(CdpClient cdp, string message, int timeoutSec)
    {
        var editorSel = await WaitForClaudeEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        var baseCountStr = await cdp.EvalAsync("document.querySelectorAll('[data-is-streaming]').length.toString()") ?? "0";
        int baseCount = int.TryParse(baseCountStr, out var bc) ? bc : 0;

        using var loopLock = ChromeTabLock.Acquire("Claude/loop");
        if (loopLock == null)
            return (false, null);

        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextClaudeProseMirror(cdp, editorSel, message);
        if (!inserted)
            return (false, null);

        await Task.Delay(350);
        var clickResult = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('[data-testid="chat-input-grid-area"] button[type="submit"]')
                       || document.querySelector('[data-testid="chat-input"] button[type="submit"]')
                       || document.querySelector('button[aria-label="Send Message"]')
                       || document.querySelector('button[aria-label*="Send"]');
                if (!btn || btn.disabled) return 'NO_BTN';
                btn.click();
                return 'CLICKED';
            })()
            """) ?? "NO_BTN";

        if (clickResult != "CLICKED")
        {
            await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
            await Task.Delay(80);
            await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
            {
                ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
            {
                ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
        }

        loopLock.Release("sent");

        var sw = Stopwatch.StartNew();
        string? last = null;
        int stable = 0;
        while (sw.Elapsed.TotalSeconds < Math.Max(20, timeoutSec))
        {
            await Task.Delay(1500);
            var limitText = await cdp.EvalAsync("""
                (() => {
                    var t = (document.body?.innerText || '');
                    var keys = ['usage limit', 'rate limit', 'too many requests', '요청이 너무 많', '사용량 한도'];
                    for (var i = 0; i < keys.length; i++) {
                        if (t.toLowerCase().includes(keys[i])) return t.substring(0, 800);
                    }
                    return '';
                })()
                """) ?? "";
            if (!string.IsNullOrWhiteSpace(limitText))
                return (true, FormatClaudeLimitResponse(limitText));

            var poll = await cdp.EvalAsync(
                "(() => {" +
                "var msgs = document.querySelectorAll('[data-is-streaming]');" +
                $"if (msgs.length <= {baseCount}) return JSON.stringify({{state:'WAIT',text:''}});" +
                "var last = msgs[msgs.length - 1];" +
                "var state = last.getAttribute('data-is-streaming') === 'true' ? 'STREAMING' : 'DONE';" +
                "var text = (last.textContent || '').trim();" +
                "return JSON.stringify({state:state,text:text});" +
                "})()") ?? "{}";
            try
            {
                using var doc = JsonDocument.Parse(poll);
                var state = doc.RootElement.TryGetProperty("state", out var s) ? s.GetString() ?? "WAIT" : "WAIT";
                var text = doc.RootElement.TryGetProperty("text", out var t) ? t.GetString() ?? "" : "";
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                if (text == last)
                {
                    stable++;
                    if (stable >= 1 && state != "STREAMING")
                        return (true, text);
                }
                else
                {
                    stable = 0;
                    last = text;
                }
            }
            catch
            {
                // ignore transient parse/DOM errors during polling
            }
        }

        return (!string.IsNullOrWhiteSpace(last), last);
    }

    static async Task<(bool ok, string? text)> GeminiSendAndWaitAsync(CdpClient cdp, string message, int timeoutSec)
    {
        var editorSel = await WaitForEditorA11y(cdp,
            ".ql-editor",
            "[role='textbox'][contenteditable='true']",
            "div[contenteditable='true']",
            "div.ql-editor",
            "rich-textarea [contenteditable]",
            ".input-area [contenteditable]");
        if (editorSel == null)
            return (false, null);

        using var loopLock = ChromeTabLock.Acquire("Gemini/loop");
        if (loopLock == null)
            return (false, null);

        var baseCountStr = await cdp.EvalAsync(
            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
        int baseCount = int.TryParse(baseCountStr, out var b) ? b : 0;

        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        if (!inserted)
            return (false, null);
        await Task.Delay(300);

        var sendResult = "PENDING";
        for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
        {
            var remaining = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
            if (remaining == "0" && sendAttempt > 0)
            {
                sendResult = $"SENT(attempt={sendAttempt})";
                break;
            }

            if (sendAttempt > 0)
            {
                var curResponses = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                if (curResponses != baseCountStr)
                {
                    sendResult = $"RESPONSE_STARTED(attempt={sendAttempt})";
                    break;
                }
            }

            var clickResult = await cdp.EvalAsync("""
                (() => {
                    var btn = document.querySelector('button[aria-label*="Send"]')
                           || document.querySelector('button[aria-label="Send message"]')
                           || document.querySelector('button.send-button');
                    if (!btn || btn.disabled) return 'NO_BUTTON';
                    btn.click();
                    return 'CLICKED';
                })()
                """) ?? "NO_BUTTON";

            if (clickResult != "CLICKED")
            {
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                    ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                });
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                    ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                });
            }

            await Task.Delay(500);
        }
        if (sendResult == "PENDING")
            sendResult = "FORCED(5x)";
        Console.WriteLine($"[ASK:LOOP] Gemini send={sendResult}");
        loopLock.Release("sent");

        var sw = Stopwatch.StartNew();
        string? last = null;
        int blankCount = 0;
        int stable = 0;
        while (sw.Elapsed.TotalSeconds < Math.Max(20, timeoutSec))
        {
            await Task.Delay(2000);
            var text = await cdp.EvalAsync(
                "(() => {" +
                "if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100) return '\\x01BLANK';" +
                "var r = document.querySelectorAll('model-response');" +
                "if (r.length === 0) r = document.querySelectorAll('[role=\"article\"]');" +
                $"if (r.length <= {baseCount}) return '';" +
                "return (r[r.length-1].textContent || '').trim();" +
                "})()") ?? "";
            if (text == "\x01BLANK")
            {
                blankCount++;
                if (blankCount >= 3)
                    break;
                continue;
            }
            blankCount = 0;
            if (string.IsNullOrWhiteSpace(text))
                continue;

            var streaming = await cdp.EvalAsync("""
                (() => {
                    var stop = document.querySelector('button[aria-label="Stop response"], button[aria-label="Stop"]');
                    if (stop) return '1';
                    var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                    return mat ? '1' : '0';
                })()
                """) ?? "0";

            if (text == last)
            {
                stable++;
                if (stable >= 1 && streaming != "1")
                    return (true, text);
            }
            else
            {
                stable = 0;
                last = text;
            }
        }

        return (!string.IsNullOrWhiteSpace(last), last);
    }

    static bool TryParseLoopToolCall(string text, out string[] argv, out string? error)
    {
        argv = Array.Empty<string>();
        error = null;
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var b = text.IndexOf(LoopCallBegin, StringComparison.Ordinal);
        if (b < 0) return false;
        var e = text.IndexOf(LoopCallEnd, b + LoopCallBegin.Length, StringComparison.Ordinal);
        string payload;
        if (e < 0)
        {
            var tail = text[(b + LoopCallBegin.Length)..].Trim();
            if (!TryExtractFirstJsonObject(tail, out payload))
            {
                error = "loop call begin found but end marker missing";
                return false;
            }
        }
        else
        {
            payload = text[(b + LoopCallBegin.Length)..e].Trim();
        }
        try
        {
            using var doc = JsonDocument.Parse(payload);
            if (!doc.RootElement.TryGetProperty("argv", out var argvEl) || argvEl.ValueKind != JsonValueKind.Array)
            {
                error = "argv array missing";
                return false;
            }

            var parts = new List<string>();
            foreach (var el in argvEl.EnumerateArray())
            {
                if (el.ValueKind == JsonValueKind.String)
                    parts.Add(el.GetString() ?? "");
            }

            if (parts.Count == 0 || string.IsNullOrWhiteSpace(parts[0]))
            {
                error = "argv is empty";
                return false;
            }

            argv = parts.ToArray();
            return true;
        }
        catch (Exception ex)
        {
            if (TryParseRelaxedLoopPayload(payload, out argv))
                return true;
            error = ex.Message;
            return false;
        }
    }

    static bool TryExtractFirstJsonObject(string text, out string json)
    {
        json = "";
        if (string.IsNullOrWhiteSpace(text))
            return false;

        int start = text.IndexOf('{');
        if (start < 0)
            return false;

        bool inString = false;
        bool esc = false;
        int depth = 0;
        for (int i = start; i < text.Length; i++)
        {
            char c = text[i];
            if (inString)
            {
                if (esc) { esc = false; continue; }
                if (c == '\\') { esc = true; continue; }
                if (c == '"') inString = false;
                continue;
            }

            if (c == '"') { inString = true; continue; }
            if (c == '{') depth++;
            else if (c == '}')
            {
                depth--;
                if (depth == 0)
                {
                    json = text[start..(i + 1)];
                    return true;
                }
            }
        }

        return false;
    }

    static bool TryParseRelaxedLoopPayload(string payload, out string[] argv)
    {
        argv = Array.Empty<string>();
        var normalized = payload.Trim();
        if (string.IsNullOrWhiteSpace(normalized))
            return false;

        // Gemini may return {argv:[help]} without JSON quotes. Accept a minimal relaxed shape.
        var m = System.Text.RegularExpressions.Regex.Match(
            normalized,
            @"argv\s*:\s*\[(?<list>[^\]]*)\]",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        if (!m.Success)
            return false;

        var list = m.Groups["list"].Value;
        var parts = list
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s => s.Trim().Trim('"', '\''))
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
        if (parts.Length == 0)
            return false;

        argv = parts;
        return true;
    }

    static async Task<(int exitCode, string stdout, string stderr)> ExecuteLoopCommandAsync(
        string[] argv, int timeoutSec, int retry, string executedBy)
    {
        if (argv.Length == 0)
            return (2, "", "empty argv");

        var root = argv[0].ToLowerInvariant();
        if (root is "eye" or "mcp")
            return (2, "", $"blocked command in loop mode: {root}");
        if (root == "ask")
        {
            if (argv.Length < 2)
                return (2, "", "blocked ask command in loop mode: provider missing");
            var provider = argv[1].ToLowerInvariant();
            if (provider != "gemini")
                return (2, "", $"blocked ask provider in loop mode: {provider}");
            if (argv.Any(a => string.Equals(a, "--loop", StringComparison.OrdinalIgnoreCase)))
                return (2, "", "blocked nested --loop in ask gemini");
        }

        var exe = Environment.ProcessPath ?? "wkappbot-core.exe";
        for (int attempt = 0; attempt <= retry; attempt++)
        {
            try
            {
                using var p = new Process();
                p.StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                foreach (var a in argv)
                    p.StartInfo.ArgumentList.Add(a);
                p.StartInfo.EnvironmentVariables["WKAPPBOT_LOOP_CALLER"] = executedBy;

                p.Start();
                var outTask = p.StandardOutput.ReadToEndAsync();
                var errTask = p.StandardError.ReadToEndAsync();

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Math.Max(10, timeoutSec)));
                await p.WaitForExitAsync(cts.Token);

                var stdout = TrimForLoop(await outTask);
                var stderr = TrimForLoop(await errTask);
                return (p.ExitCode, stdout, stderr);
            }
            catch (OperationCanceledException)
            {
                if (attempt >= retry) return (124, "", "command timeout");
            }
            catch (Exception ex)
            {
                if (attempt >= retry) return (1, "", ex.Message);
            }
        }

        return (1, "", "unknown loop execution error");
    }

    static string TrimForLoop(string s)
    {
        const int max = 8000;
        if (string.IsNullOrEmpty(s)) return "";

        // Strip verbose full-answer marker blocks (noisy in TOOL_RESULT context)
        s = StripFullAnswerBlocks(s);

        return s.Length <= max ? s : s[..max] + "\n... (truncated)";
    }

    static string StripFullAnswerBlocks(string s)
    {
        const string begin = "[ASK_FULL_ANSWER_BEGIN]";
        const string end   = "[ASK_FULL_ANSWER_END]";
        var sb = new StringBuilder();
        int pos = 0;
        while (pos < s.Length)
        {
            var b = s.IndexOf(begin, pos, StringComparison.Ordinal);
            if (b < 0) { sb.Append(s[pos..]); break; }
            sb.Append(s[pos..b]);
            var e = s.IndexOf(end, b + begin.Length, StringComparison.Ordinal);
            if (e < 0) { sb.Append(s[b..]); break; } // no closing — keep as-is
            int contentLen = e - (b + begin.Length);
            sb.Append($"[ASK_ANSWER: {contentLen}bytes]");
            pos = e + end.Length;
        }
        return sb.ToString();
    }
}
