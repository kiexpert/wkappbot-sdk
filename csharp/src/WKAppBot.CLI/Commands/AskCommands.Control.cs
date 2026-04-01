using System.Collections.Concurrent;
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

    static void EnsureAskControlPumpStarted()
    {
        var runId = Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID");
        if (string.IsNullOrWhiteSpace(runId))
            return;

        lock (_askControlPumpGate)
        {
            if (_askControlPumpTask != null)
                return;

            _askControlPumpTask = Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        string? line;
                        try { line = Console.ReadLine(); }
                        catch { break; }

                        if (line == null)
                            break;

                        if (!TryParseAskControlCommand(line, runId, out var cmd) || cmd == null)
                            continue;

                        _askControlQueue.Enqueue(cmd);
                        Console.WriteLine($"[ASK:CTRL] queued action={cmd.Action} q={cmd.QuestionId ?? "*"} reason={cmd.Reason ?? "USER_STOP"}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ASK:CTRL] reader stopped: {ex.Message}");
                }
            });
        }
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
            if (!string.Equals(effectiveAction, "cancel", StringComparison.OrdinalIgnoreCase))
                return false;

            var targetRunId = root.TryGetProperty("run_id", out var runEl) ? runEl.GetString() : null;
            if (!string.IsNullOrWhiteSpace(targetRunId) &&
                !string.Equals(targetRunId, currentRunId, StringComparison.OrdinalIgnoreCase))
                return false;

            cmd = new AskControlCommand
            {
                Action = "cancel",
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
            if (!string.Equals(cmd.Action, "cancel", StringComparison.OrdinalIgnoreCase))
                continue;

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

        return cancelled;
    }
}
