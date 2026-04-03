using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

internal partial class Program
{
    static async Task<(bool ok, string? text)> RunChatGptLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry, int maxParallel = 7,
        Action<string, string?>? onReport = null, TriadSharedContext? triadCtx = null, List<string>? toolLogOut = null)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (AgentStopFlagExists()) { Console.WriteLine("[AGENT] 🛑 --terminate 신호 → 루프 종료"); return (true, answer + "\n[AGENT] Terminated."); }
            var toolCalls = ParseAllLoopToolCalls(answer);
            if (toolCalls.Count == 0) return (true, answer);

            var requested = toolCalls.Count;
            if (toolCalls.Count > maxParallel)
            {
                Console.WriteLine($"[ASK:LOOP] GPT requested {requested} — clamped to {maxParallel}");
                toolCalls = toolCalls.Take(maxParallel).ToList();
            }

            Console.WriteLine($"[ASK:LOOP] GPT step {step}/{steps}: {toolCalls.Count}/{requested} tool call(s) in parallel");
            onReport?.Invoke($"*Step {step}/{steps}* — {toolCalls.Count} tool(s): {string.Join(", ", toolCalls.Select(tc => tc.IsStdinInject ? $"stdin→{tc.RunId}" : string.Join(" ", tc.Argv!.Take(2))))}", $"ChatGPT:step{step}");
            var idToCmd = toolCalls.ToDictionary(tc => tc.Id, tc => tc.IsStdinInject ? $"stdin → {tc.RunId}" : string.Join(" ", tc.Argv!));
            // Post ⏳ placeholder per tool — will be updated in-place when result arrives
            var idToStartTs = onReport != null ? toolCalls.ToDictionary(
                tc => tc.Id,
                tc => SlackPostToThreadAndGetTs($"⏳ `{idToCmd[tc.Id]}`", $"ChatGPT:{tc.Id}") ?? "") : null;
            var execTasks = toolCalls.Select(tc =>
            {
                if (tc.IsStdinInject)
                {
                    Console.WriteLine($"[ASK:LOOP]   {tc.Id}: stdin → {tc.RunId}");
                    var rs = RunStdin(tc.RunId!, tc.Stdin!);
                    var r = new ExecResult(rs.exitCode, rs.stdout, rs.stderr, 0);
                    return Task.FromResult((tc.Id, r));
                }
                Console.WriteLine($"[ASK:LOOP]   {tc.Id}: {string.Join(" ", tc.Argv!)}");
                return ExecuteLoopCommandAsync(tc.Argv!, timeoutSec, retries, "gpt")
                    .ContinueWith(t => (tc.Id, t.Result));
            }).ToList();

            var startTsMapGpt = idToStartTs?.Where(kv => !string.IsNullOrEmpty(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var (flushOk, flushAnswer) = await RunParallelWithFlushAsync(
                execTasks, "gpt", msg => ChatGptSendAndWait(cdp, msg, timeoutSec), answer, idToCmd, onReport, step, startTsMapGpt, triadCtx, toolLogOut);
            if (!flushOk) return (true, answer);
            answer = flushAnswer;
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    static async Task<(bool ok, string? text)> RunGeminiLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry, int maxParallel = 7,
        Action<string, string?>? onReport = null, TriadSharedContext? triadCtx = null, List<string>? toolLogOut = null)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (AgentStopFlagExists()) { Console.WriteLine("[AGENT] 🛑 --terminate 신호 → 루프 종료"); return (true, answer + "\n[AGENT] Terminated."); }
            var toolCalls = ParseAllLoopToolCalls(answer);
            if (toolCalls.Count == 0)
                return (true, answer);

            var requested = toolCalls.Count;
            if (toolCalls.Count > maxParallel)
            {
                Console.WriteLine($"[ASK:LOOP] Gemini requested {requested} calls — clamped to {maxParallel} (--max-parallel)");
                toolCalls = toolCalls.Take(maxParallel).ToList();
            }

            Console.WriteLine($"[ASK:LOOP] Gemini step {step}/{steps}: {toolCalls.Count}/{requested} tool call(s) in parallel");
            onReport?.Invoke($"*Step {step}/{steps}* — {toolCalls.Count} tool(s): {string.Join(", ", toolCalls.Select(tc => tc.IsStdinInject ? $"stdin→{tc.RunId}" : string.Join(" ", tc.Argv!.Take(2))))}", $"Gemini:step{step}");
            var idToCmd = toolCalls.ToDictionary(tc => tc.Id, tc => tc.IsStdinInject ? $"stdin → {tc.RunId}" : string.Join(" ", tc.Argv!));
            var idToStartTsGemini = onReport != null ? toolCalls.ToDictionary(
                tc => tc.Id,
                tc => SlackPostToThreadAndGetTs($"⏳ `{idToCmd[tc.Id]}`", $"Gemini:{tc.Id}") ?? "") : null;
            var execTasks = toolCalls.Select(tc =>
            {
                if (tc.IsStdinInject)
                {
                    Console.WriteLine($"[ASK:LOOP]   {tc.Id}: stdin → {tc.RunId}");
                    var rs = RunStdin(tc.RunId!, tc.Stdin!);
                    var r = new ExecResult(rs.exitCode, rs.stdout, rs.stderr, 0);
                    return Task.FromResult((tc.Id, r));
                }
                Console.WriteLine($"[ASK:LOOP]   {tc.Id}: {string.Join(" ", tc.Argv!)}");
                return ExecuteLoopCommandAsync(tc.Argv!, timeoutSec, retries, "gemini")
                    .ContinueWith(t => (tc.Id, t.Result));
            }).ToList();

            var startTsMapGemini = idToStartTsGemini?.Where(kv => !string.IsNullOrEmpty(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var (flushOk, flushAnswer) = await RunParallelWithFlushAsync(
                execTasks, "gemini", msg => GeminiSendAndWaitAsync(cdp, msg, timeoutSec), answer, idToCmd, onReport, step, startTsMapGemini, triadCtx, toolLogOut);
            if (!flushOk) return (true, answer);
            answer = flushAnswer;

            // Inject Gemini response into Claude session JSONL if requested
            TryInjectGeminiResponseToSession(step, answer);
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    static async Task<(bool ok, string? text)> RunClaudeLoopAsync(
        CdpClient cdp, string initialAnswer, int timeoutSec, int maxSteps, int retry, int maxParallel = 7,
        Action<string, string?>? onReport = null, TriadSharedContext? triadCtx = null, List<string>? toolLogOut = null)
    {
        var answer = initialAnswer;
        var steps = Math.Max(1, maxSteps);
        var retries = Math.Max(0, retry);

        for (int step = 1; step <= steps; step++)
        {
            if (AgentStopFlagExists()) { Console.WriteLine("[AGENT] 🛑 --terminate 신호 → 루프 종료"); return (true, answer + "\n[AGENT] Terminated."); }
            var toolCalls = ParseAllLoopToolCalls(answer);
            if (toolCalls.Count == 0) return (true, answer);

            var requested = toolCalls.Count;
            if (toolCalls.Count > maxParallel)
            {
                Console.WriteLine($"[ASK:LOOP] Claude requested {requested} — clamped to {maxParallel}");
                toolCalls = toolCalls.Take(maxParallel).ToList();
            }

            Console.WriteLine($"[ASK:LOOP] Claude step {step}/{steps}: {toolCalls.Count}/{requested} tool call(s) in parallel");
            onReport?.Invoke($"*Step {step}/{steps}* — {toolCalls.Count} tool(s): {string.Join(", ", toolCalls.Select(tc => tc.IsStdinInject ? $"stdin→{tc.RunId}" : string.Join(" ", tc.Argv!.Take(2))))}", $"Claude:step{step}");
            var idToCmd = toolCalls.ToDictionary(tc => tc.Id, tc => tc.IsStdinInject ? $"stdin → {tc.RunId}" : string.Join(" ", tc.Argv!));
            var idToStartTsClaude = onReport != null ? toolCalls.ToDictionary(
                tc => tc.Id,
                tc => SlackPostToThreadAndGetTs($"⏳ `{idToCmd[tc.Id]}`", $"Claude:{tc.Id}") ?? "") : null;
            var execTasks = toolCalls.Select(tc =>
            {
                if (tc.IsStdinInject)
                {
                    Console.WriteLine($"[ASK:LOOP]   {tc.Id}: stdin → {tc.RunId}");
                    var rs = RunStdin(tc.RunId!, tc.Stdin!);
                    var r = new ExecResult(rs.exitCode, rs.stdout, rs.stderr, 0);
                    return Task.FromResult((tc.Id, r));
                }
                Console.WriteLine($"[ASK:LOOP]   {tc.Id}: {string.Join(" ", tc.Argv!)}");
                return ExecuteLoopCommandAsync(tc.Argv!, timeoutSec, retries, "claude")
                    .ContinueWith(t => (tc.Id, t.Result));
            }).ToList();

            var startTsMapClaude = idToStartTsClaude?.Where(kv => !string.IsNullOrEmpty(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            var (flushOk, flushAnswer) = await RunParallelWithFlushAsync(
                execTasks, "claude", msg => ClaudeSendAndWaitAsync(cdp, msg, timeoutSec), answer, idToCmd, onReport, step, startTsMapClaude, triadCtx, toolLogOut);
            if (!flushOk) return (true, answer);
            answer = flushAnswer;
        }

        return (true, answer + "\n[ASK:LOOP] Max steps reached.");
    }

    // Parallel tool execution with 1-second flush batching.
    // Uses Task.WhenAny to collect completions as they arrive.
    // Sends intermediate TOOL_RESULT to AI when 1s has elapsed since last prompt.
    // Final flush (all done) sends complete results and parses AI's next response.
    static async Task<(bool ok, string answer)> RunParallelWithFlushAsync(
        List<Task<(string id, ExecResult result)>> execTasks,
        string provider,
        Func<string, Task<(bool ok, string? text)>> sendAndWait,
        string currentAnswer,
        IReadOnlyDictionary<string, string>? idToCmd = null,
        Action<string, string?>? onReport = null,
        int step = 0,
        IReadOnlyDictionary<string, string>? idToStartTs = null,
        TriadSharedContext? triadCtx = null,
        List<string>? toolLogOut = null)
    {
        var pending = execTasks.ToList();
        // Map task → id so we can show status=running for incomplete tasks
        var taskIds = execTasks.ToDictionary(t => t, t => t.IsCompleted ? t.Result.id : "???");
        var collected = new List<(string id, ExecResult result)>();
        var lastPromptTime = DateTime.UtcNow;
        string answer = currentAnswer;

        while (pending.Count > 0)
        {
            var completed = await Task.WhenAny(pending);
            pending.Remove(completed);
            var r = completed.GetAwaiter().GetResult();
            taskIds[completed] = r.id; // update id now that it's resolved
            collected.Add(r);

            // Cross-share tool call + result via discovery queue (경합 방지)
            {
                var cmdStr = idToCmd?.GetValueOrDefault(r.id, r.id) ?? r.id;
                var outputSnippet = r.result.Output.Length > 500 ? r.result.Output[..500] + "..." : r.result.Output;
                var toolMsg = $"{cmdStr} → exit={r.result.ExitCode}\n{outputSnippet}";
                if (triadCtx != null)
                {
                    triadCtx.PushDiscovery(provider, toolMsg);
                    triadCtx.UpdateChunk(provider, $"[TOOL] {toolMsg}");
                    Console.WriteLine($"[CROSS:TOOL] {provider} → queued for peers: {cmdStr} (exit={r.result.ExitCode}, {r.result.Output.Length}ch)");
                    SlackPostToThread($"🔧 *[{provider} tool→peers]* `{cmdStr}` exit={r.result.ExitCode}, {r.result.Output.Length}ch", "🦉 Moderator");
                }
                // Log tool results to MD: edit = full output, others = one line
                if (toolLogOut != null)
                {
                    var status = r.result.ExitCode == 0 ? "ok" : $"exit={r.result.ExitCode}";
                    if (cmdStr.Contains("edit", StringComparison.OrdinalIgnoreCase))
                    {
                        var snippet = r.result.Output.Length > 300 ? r.result.Output[..300] + "..." : r.result.Output;
                        toolLogOut.Add($"`{cmdStr}` → {status}\n```\n{snippet}\n```");
                    }
                    else
                    {
                        toolLogOut.Add($"`{cmdStr}` → {status}");
                    }
                }
            }

            // Echo tool output to console with "{id}> " prefix + metrics
            {
                var (echoId, exec) = r;
                var combined = exec.Output;
                if (!string.IsNullOrWhiteSpace(combined))
                    foreach (var line in combined.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                        Console.WriteLine($"  {echoId}> {line.TrimEnd()}");
                // Metrics line: tab-separated for easy parsing/grep
                Console.WriteLine($"  {echoId}>\t exit={exec.ExitCode} bytes={exec.TotalBytes} ms={exec.ElapsedMs} @{DateTime.Now:HH:mm:ss.fff}");
            }

            bool allDone   = pending.Count == 0;
            bool flushDue  = (DateTime.UtcNow - lastPromptTime).TotalSeconds >= 1.0;
            if (!allDone && !flushDue) continue;

            // Build TOOL_RESULT in MCP JSON-RPC 2.0 format
            var sb = new StringBuilder();
            sb.AppendLine($"TOOL_RESULTS [MCP+APSP executed_by={provider} flushed={collected.Count} still_running={pending.Count}]");
            var responses = new JsonArray();
            foreach (var (id, exec) in collected)
            {
                responses.Add(new JsonObject
                {
                    ["jsonrpc"] = "2.0",
                    ["id"] = id,
                    ["result"] = new JsonObject
                    {
                        ["content"] = new JsonArray { new JsonObject { ["type"] = "text", ["text"] = exec.Output.Trim() } },
                        ["isError"] = exec.ExitCode != 0,
                        // _meta: standard MCP extension — metrics for observability and AI reasoning
                        ["_meta"] = new JsonObject
                        {
                            ["exit"]        = exec.ExitCode,
                            ["bytes"]       = exec.TotalBytes,
                            ["elapsed_ms"]  = exec.ElapsedMs,
                            ["flushed_at"]  = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                            ["provider"]    = provider
                        }
                    }
                });
            }
            sb.AppendLine(responses.ToJsonString());
            // MCP notifications/progress for each still-running call — emitted after the results array
            foreach (var t in pending)
            {
                var tid = taskIds.TryGetValue(t, out var v) ? v : "tc_?";
                sb.AppendLine(new JsonObject
                {
                    ["jsonrpc"] = "2.0",
                    ["method"] = "notifications/progress",
                    ["params"] = new JsonObject
                    {
                        ["progressToken"] = tid,
                        ["progress"] = 0,
                        ["status"] = "running",
                        ["data"] = "still executing — partial results above, remaining results arriving each second"
                    }
                }.ToJsonString());
            }
            if (!allDone)
            {
                sb.AppendLine();
                sb.AppendLine($"⚠ {pending.Count} tool call(s) still running (notifications/progress above). Do NOT respond or emit new TOOL_CALLs — wait for next TOOL_RESULTS flush.");
            }
            else
                sb.Append("All tool calls complete. Emit next TOOL_CALL block(s) if more actions needed, otherwise give final answer.");

            Console.WriteLine($"[ASK:LOOP] Flushing {collected.Count} result(s) to {provider} (pending={pending.Count})");

            // Log tool results to Slack thread — each tool as separate post with AI:toolId:stepN username
            if (onReport != null)
            {
                var label = LoopProviderLabel(provider);
                foreach (var (id, exec) in collected)
                {
                    var cmd = idToCmd != null && idToCmd.TryGetValue(id, out var c) ? c : id;
                    var icon = exec.ExitCode == 0 ? "✅" : "❌";
                    var toolMsg = new StringBuilder();
                    // Header: icon + command
                    toolMsg.AppendLine($"{icon} `{cmd}`");
                    // Metrics line: all available metrics
                    var elapsedSec = exec.ElapsedMs / 1000.0;
                    var stdoutB = exec.Stdout.Length;
                    var stderrB = exec.Stderr.Length;
                    var bytesStr = stderrB > 0
                        ? $"stdout={stdoutB}B stderr={stderrB}B"
                        : $"{stdoutB}B";
                    var stepStr = step > 0 ? $" · step={step}" : "";
                    toolMsg.AppendLine($"> `exit={exec.ExitCode}` · `{elapsedSec:F1}s` · {bytesStr}{stepStr} · `{id}`");
                    // Output block (stdout)
                    if (!string.IsNullOrWhiteSpace(exec.Stdout))
                    {
                        var cleaned = StripVerboseToolLines(exec.Stdout);
                        var snippet = cleaned.Length > 500 ? cleaned[..500] + "…" : cleaned;
                        if (!string.IsNullOrWhiteSpace(snippet))
                            toolMsg.AppendLine($"```\n{snippet}\n```");
                    }
                    // Stderr block (if present)
                    if (!string.IsNullOrWhiteSpace(exec.Stderr))
                    {
                        var errSnippet = exec.Stderr.Length > 200 ? exec.Stderr[..200] + "…" : exec.Stderr;
                        toolMsg.AppendLine($"⚠ stderr:\n```\n{errSnippet}\n```");
                    }
                    var toolMsgText = toolMsg.ToString().TrimEnd();
                    // If we have a dedicated start-message ts → update in-place (no new message spam)
                    if (idToStartTs != null && idToStartTs.TryGetValue(id, out var startTs))
                        SlackUpdateThreadMessage(startTs, toolMsgText);
                    else
                        onReport(toolMsgText, step > 0 ? $"{label}:{id}:step{step}" : $"{label}:{id}");
                }
                if (pending.Count > 0)
                    onReport($"⏳ {pending.Count} still running…", $"{label}:step{step}");
            }

            collected.Clear();
            lastPromptTime = DateTime.UtcNow;

            var (ok, next) = await sendAndWait(sb.ToString());
            if (!ok || string.IsNullOrWhiteSpace(next))
            {
                Console.WriteLine($"[ASK:LOOP] {provider} follow-up timed out after flush.");
                onReport?.Invoke($"❌ *timeout* — {provider} didn't respond after tool results flush", $"{LoopProviderLabel(provider)}:error");
                return (true, answer);
            }
            answer = next!;

            // Log AI response to Slack thread — collapse blank lines (incl. whitespace-only lines)
            var slackAnswer = NormalizeBlankLines(answer);
            onReport?.Invoke(slackAnswer.Length > 2000 ? slackAnswer[..2000] + "…" : slackAnswer, LoopProviderLabel(provider));
        }

        return (true, answer);
    }

    // TryInjectGeminiResponseToSession → AppBotEyeClaudeFallback.cs
}
