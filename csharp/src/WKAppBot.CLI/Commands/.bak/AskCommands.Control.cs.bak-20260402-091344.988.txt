using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed class AskControlCommand
    {
        public string Action { get; init; } = "";
        public string? RunId { get; init; }
        public string? QuestionId { get; init; }
        public string? PageKey { get; init; }
        public string? Reason { get; init; }
    }

    static readonly ConcurrentQueue<AskControlCommand> _askControlQueue = new();
    static readonly object _askControlPumpGate = new();
    static Task? _askControlPumpTask;
    static string? _askControlPipeName;

    static void EnsureAskControlPumpStarted()
    {
        var runId = Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID");
        if (string.IsNullOrWhiteSpace(runId))
            return;
        _askControlPipeName ??= Environment.GetEnvironmentVariable("WKAPPBOT_ASK_CONTROL_PIPE");

        lock (_askControlPumpGate)
        {
            if (_askControlPumpTask != null)
                return;

            _askControlPumpTask = Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(_askControlPipeName))
                    {
                        RunAskControlPipeLoop(runId, _askControlPipeName);
                        return;
                    }

                    while (true)
                    {
                        string? line;
                        try { line = Console.ReadLine(); }
                        catch { break; }

                        if (line == null)
                            break;

                        EnqueueAskControlLine(line, runId);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ASK:CTRL] reader stopped: {ex.Message}");
                }
            });
        }
    }

    static void RunAskControlPipeLoop(string runId, string pipeName)
    {
        while (true)
        {
            try
            {
                using var server = new NamedPipeServerStream(
                    pipeName,
                    PipeDirection.In,
                    1,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous);
                server.WaitForConnection();

                using var reader = new StreamReader(server);
                string? line;
                while ((line = reader.ReadLine()) != null)
                    EnqueueAskControlLine(line, runId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK:CTRL] pipe stopped: {ex.Message}");
                Thread.Sleep(250);
            }
        }
    }

    static void EnqueueAskControlLine(string line, string runId)
    {
        if (!TryParseAskControlCommand(line, runId, out var cmd) || cmd == null)
            return;

        _askControlQueue.Enqueue(cmd);
        Console.WriteLine($"[ASK:CTRL] queued action={cmd.Action} q={cmd.QuestionId ?? "*"} reason={cmd.Reason ?? "USER_STOP"}");
    }

    static bool TryParseAskControlCommand(string line, string currentRunId, out AskControlCommand? cmd)
    {
        cmd = null;
        if (string.IsNullOrWhiteSpace(line))
            return false;

        try
        {
            using var doc = JsonDocument.Parse(line);
            var root = doc.RootElement;
            var type = root.TryGetProperty("type", out var typeEl) ? typeEl.GetString() : null;
            var kind = root.TryGetProperty("kind", out var kindEl) ? kindEl.GetString() : null;
            var action = root.TryGetProperty("action", out var actionEl) ? actionEl.GetString() : null;
            var op = root.TryGetProperty("op", out var opEl) ? opEl.GetString() : null;

            var isAskControl =
                string.Equals(type, "ask-control", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(kind, "ask-control", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(op, "cancel_question", StringComparison.OrdinalIgnoreCase);
            if (!isAskControl)
                return false;

            var effectiveAction = action ?? (string.Equals(op, "cancel_question", StringComparison.OrdinalIgnoreCase) ? "cancel" : null);
            if (!string.Equals(effectiveAction, "cancel", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(effectiveAction, "status", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(effectiveAction, "list", StringComparison.OrdinalIgnoreCase))
                return false;

            var targetRunId = root.TryGetProperty("run_id", out var runEl) ? runEl.GetString() : null;
            if (!string.IsNullOrWhiteSpace(targetRunId) &&
                !string.Equals(targetRunId, currentRunId, StringComparison.OrdinalIgnoreCase))
                return false;

            cmd = new AskControlCommand
            {
                Action = effectiveAction ?? "cancel",
                RunId = targetRunId ?? currentRunId,
                QuestionId = root.TryGetProperty("question_id", out var qidEl) ? qidEl.GetString() : null,
                PageKey = root.TryGetProperty("page_key", out var pageEl) ? pageEl.GetString() : null,
                Reason = root.TryGetProperty("reason", out var reasonEl) ? reasonEl.GetString() : null,
            };
            return true;
        }
        catch
        {
            return false;
        }
    }

    static async Task<bool> TryHandleAskControlAsync(AskSession askSession)
    {
        EnsureAskControlPumpStarted();

        var cancelled = false;
        while (_askControlQueue.TryDequeue(out var cmd))
        {
            if (string.Equals(cmd.Action, "cancel", StringComparison.OrdinalIgnoreCase))
            {
                bool ok;
                if (string.IsNullOrWhiteSpace(cmd.QuestionId) && string.IsNullOrWhiteSpace(cmd.PageKey))
                {
                    ok = await askSession.CancelCurrentQuestionAsync(cmd.Reason ?? "USER_STOP");
                }
                else
                {
                    ok = await askSession.CancelQuestionAsync(
                        pageKey: cmd.PageKey,
                        questionId: cmd.QuestionId,
                        runId: cmd.RunId,
                        reason: cmd.Reason ?? "USER_STOP");
                }

                Console.WriteLine(ok
                    ? $"[ASK:CTRL] cancel applied q={cmd.QuestionId ?? "*"}"
                    : $"[ASK:CTRL] cancel miss q={cmd.QuestionId ?? "*"}");

                cancelled |= ok;
            }
            else if (string.Equals(cmd.Action, "status", StringComparison.OrdinalIgnoreCase))
            {
                var key = askSession.ResolveQuestionKey(cmd.PageKey, cmd.QuestionId, cmd.RunId);
                Console.WriteLine($"[ASK:QSTATUS] {askSession.SnapshotQuestion(key).ToJsonString()}");
            }
            else if (string.Equals(cmd.Action, "list", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[ASK:QLIST] {askSession.SnapshotQuestions().ToJsonString()}");
            }
        }

        return cancelled;
    }
}
