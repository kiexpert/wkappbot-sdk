using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed class RunEntry(string id, string cmd, Process process, string? askControlPipeName = null)
    {
        public string Id { get; } = id;
        public string Cmd { get; } = cmd;
        public Process Process { get; } = process;
        public string? AskControlPipeName { get; } = askControlPipeName;
        public StringBuilder Buffer { get; } = new();
        public DateTime StartTime { get; } = DateTime.UtcNow;
        public Stopwatch Elapsed { get; } = Stopwatch.StartNew();

        public string GetMetrics()
        {
            try
            {
                if (Process.HasExited) return "";
                Process.Refresh();
                var memMb = Process.WorkingSet64 / 1024.0 / 1024.0;
                var cpuMs = (long)Process.TotalProcessorTime.TotalMilliseconds;
                var elapsedSec = Elapsed.Elapsed.TotalSeconds;
                // CPU% = total cpu ms used / (elapsed * logicalCores * 1000) * 100
                var cores = Environment.ProcessorCount;
                var cpuPct = elapsedSec > 0.5
                    ? Math.Round(cpuMs / (elapsedSec * cores * 10.0), 1)
                    : 0.0;
                return $"mem_mb: {memMb:F1}  cpu_pct: {cpuPct}  cpu_ms: {cpuMs}";
            }
            catch { return ""; }
        }
    }

    static readonly ConcurrentDictionary<string, RunEntry> _runRegistry =
        new(StringComparer.OrdinalIgnoreCase);
    static int _runSeq;

    static string GenerateRunId() =>
        $"r_{DateTime.Now:HHmmss}_{Interlocked.Increment(ref _runSeq):D3}";

    static string GenerateAskControlPipeName(string runId) =>
        $"wkappbot-ask-ctrl-{runId}".Replace(":", "_");

    // Well-known external interpreters/shells (no .exe suffix needed)
    static readonly HashSet<string> KnownExternalTools = new(StringComparer.OrdinalIgnoreCase)
    {
        "bash", "sh", "zsh", "fish",
        "powershell", "pwsh",
        "python", "python3", "py",
        "node", "node.exe",
        "java", "dotnet",
        "git", "curl", "wget",
    };

    // External binary: .exe/.bat/.cmd/.ps1, path separator, or known tool → run directly
    static bool IsExternalExe(string name) =>
        name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ||
        name.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) ||
        name.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase) ||
        name.EndsWith(".ps1", StringComparison.OrdinalIgnoreCase) ||
        name.Contains('\\') || name.Contains('/') ||
        KnownExternalTools.Contains(name);

    // Start a process in background; stdin/stdout/stderr all piped
    static (int exitCode, string stdout, string stderr) RunStart(string[] cmdArgv, string executedBy)
    {
        if (cmdArgv.Length == 0) return (2, "", "run start: empty command");
        var root = cmdArgv[0].ToLowerInvariant();
        if (root is "eye" or "mcp") return (2, "", $"run start: blocked: {root}");

        var runId = GenerateRunId();
        var askControlPipeName = root == "ask" ? GenerateAskControlPipeName(runId) : null;

        // External exe (cmd.exe, python.exe, etc.) → run directly
        // WKAppBot command (a11y, inspect, etc.) → run via wkappbot-core.exe
        string exe;
        string[] exeArgs;
        if (IsExternalExe(cmdArgv[0]))
        {
            exe = cmdArgv[0];
            exeArgs = cmdArgv[1..];
        }
        else
        {
            exe = Environment.ProcessPath ?? "wkappbot-core.exe";
            exeArgs = cmdArgv;
        }

        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = exe,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        foreach (var a in exeArgs) p.StartInfo.ArgumentList.Add(a);
        p.StartInfo.EnvironmentVariables["WKAPPBOT_LOOP_CALLER"] = executedBy;
        p.StartInfo.EnvironmentVariables["WKAPPBOT_RUN_ID"] = runId;
        if (!string.IsNullOrWhiteSpace(askControlPipeName))
            p.StartInfo.EnvironmentVariables["WKAPPBOT_ASK_CONTROL_PIPE"] = askControlPipeName;
        p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;

        var entry = new RunEntry(runId, string.Join(" ", cmdArgv), p, askControlPipeName);
        p.OutputDataReceived += (_, e) =>
        {
            if (e.Data != null) lock (entry.Buffer) entry.Buffer.AppendLine(e.Data);
        };
        p.ErrorDataReceived += (_, e) =>
        {
            if (e.Data != null) lock (entry.Buffer) entry.Buffer.AppendLine("[ERR] " + e.Data);
        };

        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();
        _runRegistry[runId] = entry;

        Console.Error.WriteLine($"[RUN] start run_id={runId}  pid={p.Id}  cmd={entry.Cmd}");
        var pipeLine = string.IsNullOrWhiteSpace(askControlPipeName) ? "" : $"\nask_control_pipe: {askControlPipeName}";
        return (0, $"run_id: {runId}\nstatus: running\npid: {p.Id}\ncmd: {entry.Cmd}{pipeLine}", "");
    }

    static bool TrySendAskControlPipe(RunEntry entry, string payload, out string error)
    {
        error = "";
        if (string.IsNullOrWhiteSpace(entry.AskControlPipeName))
            return false;

        try
        {
            using var client = new NamedPipeClientStream(".", entry.AskControlPipeName, PipeDirection.Out);
            client.Connect(500);
            using var writer = new StreamWriter(client, new UTF8Encoding(false)) { AutoFlush = true };
            writer.Write(payload);
            var preview = payload.Replace("\r", "\\r").Replace("\n", "\\n");
            Console.Error.WriteLine($"[RUN] ask-ctrl -> {entry.Id}  data={preview}");
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    // Inject stdin into a running process
    static (int exitCode, string stdout, string stderr) RunStdin(string runId, string stdin)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");
        if (entry.Process.HasExited)
            return (1, "", $"process already exited: {runId} (exit={entry.Process.ExitCode})");
        try
        {
            entry.Process.StandardInput.Write(stdin);
            entry.Process.StandardInput.Flush();
            var preview = stdin.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\u0003", "^C");
            Console.Error.WriteLine($"[RUN] stdin → {runId}  data={preview}");
            return (0, $"stdin → {runId} ({stdin.Length} chars: {preview})", "");
        }
        catch (Exception ex) { return (1, "", $"stdin inject failed: {ex.Message}"); }
    }

    static (int exitCode, string stdout, string stderr) RunAskControl(string runId, JsonObject payload)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");

        var text = payload.ToJsonString() + Environment.NewLine;
        if (TrySendAskControlPipe(entry, text, out var pipeError))
        {
            return (0, $"ask-control -> {runId} ({payload["action"]})", "");
        }

        if (!string.IsNullOrWhiteSpace(entry.AskControlPipeName) && !string.IsNullOrWhiteSpace(pipeError))
            Console.Error.WriteLine($"[RUN] ask-ctrl fallback -> stdin ({pipeError})");

        return RunStdin(runId, text);
    }

    static (int exitCode, string stdout, string stderr) RunQuestionCancel(string runId, string? questionId, string? reason = null)
    {
        var payload = new JsonObject
        {
            ["type"] = "ask-control",
            ["action"] = "cancel",
            ["run_id"] = runId
        };
        if (!string.IsNullOrWhiteSpace(questionId))
            payload["question_id"] = questionId;
        if (!string.IsNullOrWhiteSpace(reason))
            payload["reason"] = reason;
        return RunAskControl(runId, payload);
    }

    static (int exitCode, string stdout, string stderr) RunQuestionStatus(string runId, string? questionId = null)
    {
        var payload = new JsonObject
        {
            ["type"] = "ask-control",
            ["action"] = "status",
            ["run_id"] = runId
        };
        if (!string.IsNullOrWhiteSpace(questionId))
            payload["question_id"] = questionId;
        var inject = RunAskControl(runId, payload);
        if (inject.exitCode != 0)
            return inject;
        Thread.Sleep(250);
        return RunTail(runId);
    }

    static (int exitCode, string stdout, string stderr) RunQuestionList(string runId)
    {
        var payload = new JsonObject
        {
            ["type"] = "ask-control",
            ["action"] = "list",
            ["run_id"] = runId
        };
        var inject = RunAskControl(runId, payload);
        if (inject.exitCode != 0)
            return inject;
        Thread.Sleep(250);
        return RunTail(runId);
    }

    // Block until process exits or timeout
    static async Task<(int exitCode, string stdout, string stderr)> RunAwaitAsync(string runId, int timeoutSec)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(Math.Max(5, timeoutSec)));
            await entry.Process.WaitForExitAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            var partial = GetRunBuffer(entry);
            return (124, string.IsNullOrWhiteSpace(partial) ? "(no output)" : TrimForLoop(partial),
                $"timeout ({timeoutSec}s): {runId}");
        }
        _runRegistry.TryRemove(runId, out _);
        var output = GetRunBuffer(entry);
        return (entry.Process.ExitCode,
            string.IsNullOrWhiteSpace(output) ? "(no output)" : TrimForLoop(output), "");
    }

    // Kill the process
    static (int exitCode, string stdout, string stderr) RunCancel(string runId)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");
        try
        {
            if (!entry.Process.HasExited)
                entry.Process.Kill(entireProcessTree: true);
            _runRegistry.TryRemove(runId, out _);
            Console.Error.WriteLine($"[RUN] cancel run_id={runId}");
            return (0, $"cancelled: {runId}", "");
        }
        catch (Exception ex) { return (1, "", $"cancel failed: {ex.Message}"); }
    }

    // Status + live process metrics
    static (int exitCode, string stdout, string stderr) RunStatus(string runId)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");
        var elapsed = entry.Elapsed.Elapsed.TotalSeconds;
        var status = entry.Process.HasExited
            ? $"exited ({entry.Process.ExitCode})" : "running";
        var metrics = entry.GetMetrics();
        var sb = new StringBuilder();
        sb.AppendLine($"run_id: {runId}");
        sb.AppendLine($"status: {status}");
        sb.AppendLine($"elapsed: {elapsed:F1}s");
        sb.AppendLine($"pid: {entry.Process.Id}");
        sb.AppendLine($"cmd: {entry.Cmd}");
        if (!string.IsNullOrWhiteSpace(metrics)) sb.AppendLine(metrics);
        return (0, sb.ToString().Trim(), "");
    }

    // List all active runs with metrics
    static (int exitCode, string stdout, string stderr) RunList()
    {
        if (_runRegistry.IsEmpty) return (0, "no active runs", "");
        var sb = new StringBuilder();
        foreach (var (id, e) in _runRegistry)
        {
            var elapsed = e.Elapsed.Elapsed.TotalSeconds;
            var st = e.Process.HasExited ? $"exited({e.Process.ExitCode})" : "running";
            var metrics = e.GetMetrics();
            sb.Append($"{id}  {st}  {elapsed:F0}s  {e.Cmd}");
            if (!string.IsNullOrWhiteSpace(metrics)) sb.Append($"  [{metrics}]");
            sb.AppendLine();
        }
        return (0, sb.ToString().Trim(), "");
    }

    // Tail buffered stdout/stderr
    static (int exitCode, string stdout, string stderr) RunTail(string runId)
    {
        if (!_runRegistry.TryGetValue(runId, out var entry))
            return (1, "", $"run_id not found: {runId}");
        var metrics = entry.GetMetrics();
        var buf = GetRunBuffer(entry);
        var header = string.IsNullOrWhiteSpace(metrics) ? "" : $"[{metrics}]\n";
        return (0, header + (string.IsNullOrWhiteSpace(buf) ? "(no output yet)" : TrimForLoop(buf)), "");
    }

    static string GetRunBuffer(RunEntry e) { lock (e.Buffer) return e.Buffer.ToString(); }
}
