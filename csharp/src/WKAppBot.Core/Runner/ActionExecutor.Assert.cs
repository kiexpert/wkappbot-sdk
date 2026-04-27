using System.Diagnostics;
using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Wait --------------------------------------------------─

    private void DoWait(StepDefinition step, StepResult result)
    {
        var seconds = step.Params?.Seconds ?? 1.0;
        Thread.Sleep((int)(seconds * 1000));
        result.ActionDetail = $"Wait {seconds}s";
        Log($"  Waited {seconds}s");
    }

    // -- Assert ------------------------------------------------─

    private void DoAssert(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for assert");

        // Record action point at assert target element
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var elemDesc = $"{step.Target?.AutomationId ?? step.Target?.Name ?? "?"}";
            _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "assert", elemDesc);
        }

        var actualText = UiaLocator.GetText(element) ?? "";
        var expected = _ctx.Resolve(step.Params!.Expected);
        var assertType = step.Params.Type!;

        Log($"  Assert {assertType}: actual=\"{actualText}\" expected=\"{expected}\"");

        // Assert types use the same vocabulary as ExpectDefinition conditions --
        // value_* operate on UIA ValuePattern.Value, consistent across assert + expect.
        bool pass = assertType switch
        {
            "value_contains"    => actualText.Contains(expected, StringComparison.OrdinalIgnoreCase),
            "value_equals"      => actualText.Equals(expected, StringComparison.OrdinalIgnoreCase),
            "value_starts_with" => actualText.StartsWith(expected, StringComparison.OrdinalIgnoreCase),
            "value_not_empty"   => !string.IsNullOrWhiteSpace(actualText),
            _ => throw new InvalidOperationException(
                $"Unknown assert type: {assertType}. " +
                "Valid: value_contains, value_equals, value_starts_with, value_not_empty.")
        };

        result.Status = pass ? StepStatus.Pass : StepStatus.Fail;
        result.Message = pass
            ? $"OK: \"{actualText}\" {assertType} \"{expected}\""
            : $"FAIL: \"{actualText}\" does not {assertType} \"{expected}\"";
        result.LocatorMethod = method;
        result.ActionDetail = $"Assert {assertType} \"{expected}\" on {step.Target?.AutomationId ?? step.Target?.Name ?? "?"}";
        if (pass) result.ActionDetail += $" -> \"{actualText}\"";
    }

    // -- Read --------------------------------------------------─

    /// <summary>
    /// Extract text from a UIA target (ValuePattern.Value first, then TextPattern.DocumentRange)
    /// and optionally store it as a scenario variable for later ${StoreAs} substitution.
    /// Mirrors the read path used by A11yCommand.Read / DoAssert -- same UiaLocator.GetText helper,
    /// so MFC edit controls, Win32 statics, and standard UIA elements all return consistent text.
    /// </summary>
    private void DoRead(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException(
                $"Cannot locate element for read: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");

        // Record action point at the target element (same policy as DoAssert)
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var elemDesc = $"{step.Target?.AutomationId ?? step.Target?.Name ?? "?"}";
            _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "read", elemDesc);
        }

        var text = UiaLocator.GetText(element) ?? "";

        // Optional: stash under ${StoreAs} so subsequent steps can reference it
        var storeAs = step.Params?.StoreAs;
        if (!string.IsNullOrWhiteSpace(storeAs))
        {
            _ctx.Variables[storeAs!] = text;
        }

        // Echo the value to stdout -- matches `wkappbot a11y read` behavior so scenario
        // logs carry the same visible trace as an interactive a11y read call.
        Console.WriteLine(text);

        result.Status = StepStatus.Pass;
        result.LocatorMethod = method;
        var targetLabel = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        result.ActionDetail = storeAs != null
            ? $"Read {targetLabel} -> ${{{storeAs}}}=\"{Truncate(text, 80)}\" ({method})"
            : $"Read {targetLabel} -> \"{Truncate(text, 80)}\" ({method})";
        result.Message = text;
        Log($"  Read {targetLabel}: \"{Truncate(text, 200)}\"" +
            (storeAs != null ? $" -> ${{{storeAs}}}" : ""));
    }

    private static string Truncate(string s, int max)
    {
        if (string.IsNullOrEmpty(s)) return "";
        return s.Length <= max ? s : s.Substring(0, max) + "...";
    }

    // -- Shell -------------------------------------------------─

    /// <summary>
    /// Run an external command. Hidden window, CreateNoWindow=true, stdout+stderr captured.
    /// Enforces a timeout (default 30s) to avoid hangs in CI scenarios.
    /// Variables ${foo} in command/args/working_dir are resolved via RuntimeContext.
    ///
    /// Focusless / non-interactive by construction:
    ///   UseShellExecute=false + RedirectStandardOutput=true + CreateNoWindow=true
    /// matches the policy AppBotPipe.StartTracked enforces on the Shared-layer spawns.
    /// Core doesn't link Shared, so we instantiate Process directly but honor the same contract.
    /// </summary>
    private void DoShell(StepDefinition step, StepResult result)
    {
        var cmd = _ctx.Resolve(step.Params?.Command);
        if (string.IsNullOrWhiteSpace(cmd))
            throw new InvalidOperationException("shell: params.command is required");

        var args = _ctx.Resolve(step.Params?.Args ?? "");
        var cwd = !string.IsNullOrWhiteSpace(step.Params?.WorkingDir)
            ? _ctx.Resolve(step.Params!.WorkingDir)
            : Environment.CurrentDirectory;
        var timeoutMs = (int)((step.Params?.TimeoutSec ?? 30.0) * 1000);
        var expectedExit = step.Params?.ExitCode ?? 0;

        var psi = new ProcessStartInfo
        {
            FileName = cmd,
            Arguments = args,
            WorkingDirectory = cwd,
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = System.Text.Encoding.UTF8,
            StandardErrorEncoding = System.Text.Encoding.UTF8,
        };

        Log($"  Shell: {cmd} {args}  (cwd={cwd}, timeout={timeoutMs}ms)");

        string stdout, stderr;
        int exitCode;
        using (var proc = Process.Start(psi))
        {
            if (proc == null)
                throw new InvalidOperationException($"shell: failed to start process: {cmd}");

            // Read streams async; WaitForExit(timeout) to avoid deadlock on full pipe.
            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();

            if (!proc.WaitForExit(timeoutMs))
            {
                try { proc.Kill(entireProcessTree: true); } catch { }
                throw new InvalidOperationException(
                    $"shell: timed out after {timeoutMs}ms: {cmd} {args}");
            }

            // Ensure async stream readers complete after exit.
            proc.WaitForExit();
            stdout = stdoutTask.Result ?? "";
            stderr = stderrTask.Result ?? "";
            exitCode = proc.ExitCode;
        }

        var storeAs = step.Params?.StoreAs;
        if (!string.IsNullOrWhiteSpace(storeAs))
        {
            // Trim trailing newline -- scenario variables rarely want them
            _ctx.Variables[storeAs!] = stdout.TrimEnd('\r', '\n');
        }

        if (exitCode != expectedExit)
        {
            result.Status = StepStatus.Fail;
            result.Message = $"shell: exit {exitCode} (expected {expectedExit})\nstderr: {Truncate(stderr, 500)}";
            result.ActionDetail = $"Shell {cmd} {args} -> exit {exitCode}";
            throw new InvalidOperationException(result.Message);
        }

        // Echo stdout so scenario log mirrors interactive shell behavior
        if (!string.IsNullOrEmpty(stdout))
            Console.Write(stdout);

        result.Status = StepStatus.Pass;
        result.Message = stdout;
        result.ActionDetail = storeAs != null
            ? $"Shell {cmd} {args} -> ${{{storeAs}}}=\"{Truncate(stdout.TrimEnd(), 80)}\" (exit {exitCode})"
            : $"Shell {cmd} {args} -> exit {exitCode}";
    }
}
