using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// -- BlockerHandlerAdapter ----------------------------------------─

/// <summary>
/// IBlockerHandler implementation: wraps the existing TryHandleBlocker + DialogHandlerManager.
/// Tag: [READINESS]
/// </summary>
internal sealed class BlockerHandlerAdapter : IBlockerHandler
{
    private readonly DialogHandlerManager? _handlerMgr;

    public BlockerHandlerAdapter(DialogHandlerManager? handlerMgr)
    {
        _handlerMgr = handlerMgr;
    }

    public (bool handled, bool shouldRetry) TryHandle(IntPtr mainHwnd, BlockerInfo blocker)
    {
        // Delegate to Program.TryHandleBlocker (partial class)
        return Program.TryHandleBlockerViaReadiness(mainHwnd, _handlerMgr);
    }
}

// -- KnowhowBroadcasterAdapter ------------------------------------─

/// <summary>
/// IKnowhowBroadcaster implementation: guarantees one broadcast per key.
/// knowhow.md -> filename + first paragraph, knowhow-{action}.md -> filename only.
/// Tag: [READINESS]
/// </summary>
internal sealed class KnowhowBroadcasterAdapter : IKnowhowBroadcaster
{
    private readonly HashSet<string> _broadcastedKeys = new();

    public void Broadcast(IntPtr mainHwnd, string? formId, int? controlId, string? actionName)
    {
        var key = $"{mainHwnd:X}|{formId}|{controlId}|{actionName}";
        if (!_broadcastedKeys.Add(key))
            return; // Already broadcasted -- only once.

        Program.BroadcastKnowhowViaReadiness(mainHwnd, formId, actionName);
    }
}

// -- UserInputWaitAdapter ----------------------------------------─

/// <summary>
/// IUserInputWait implementation: waits for a focus yield via a WPF popup.
/// The owner is set to the target window and the popup uses a countdown confirm button.
/// Tag: [READINESS]
/// </summary>
internal sealed class UserInputWaitAdapter : IUserInputWait
{
    private readonly bool _noSound;
    public UserInputWaitAdapter(bool noSound = false) => _noSound = noSound;

    public UserYieldResult WaitForUserYield(IntPtr targetMainHwnd, uint userIdleMs, int timeoutSeconds,
                                             IntPtr positionHwnd = default)
    {
        if (ShouldAutoApproveAll())
        {
            var focusOk = targetMainHwnd == IntPtr.Zero ||
                NativeMethods.SmartSetForegroundWindow(targetMainHwnd);
            Console.WriteLine("  [READINESS] auto-approve-all: yield popup skipped");
            return new UserYieldResult(true, focusOk);
        }

        // Dot animation: print the popup label immediately, then one " ." per second.
        using var dotCts = new CancellationTokenSource();
        var dotThread = new Thread(() =>
        {
            Console.Write("  [승인팝업]");
            Console.Out.Flush();
            while (!dotCts.Token.WaitHandle.WaitOne(1000))
            {
                Console.Write(" .");
                Console.Out.Flush();
            }
        }) { IsBackground = true, Name = "YieldDotAnim" };

        var sw = System.Diagnostics.Stopwatch.StartNew();
        dotThread.Start();

        var result = new UserYieldResult(false, false);
        try
        {
            // CallerArgs: actual command (e.g. "ask gemini ...") when running inside the Eye pipe.
            // Fallback to process args when running standalone (non-Eye mode).
            var callerArgs = EyeCmdPipeServer.CallerArgs.Value;
            var actionArgs = callerArgs != null
                ? string.Join(" ", callerArgs)
                : string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
            // Capture the call stack -- WKAppBot frames only, skip internals.
            var stackFrames = new System.Diagnostics.StackTrace(fNeedFileInfo: false).GetFrames()
                .Select(f => f.GetMethod())
                .Where(m => m?.DeclaringType?.Namespace?.StartsWith("WKAppBot") == true)
                .Select(m => Program.CleanAsyncMethodName(m!.DeclaringType!.Name, m.Name))
                .Where(s => s != null && !s.StartsWith("UserInputWait") && !s.StartsWith("ReadinessAdapter"))
                .Distinct()
                .Take(6)
                .ToArray();
            var stackInfo = stackFrames.Length > 0 ? "\n[호출] " + string.Join(" <- ", stackFrames) : "";
            // Caller display name (who triggered this approval).
            var callerHwnd = EyeCmdPipeServer.CallerHwnd.Value;
            var callerName = callerHwnd.HasValue
                ? Program.GetPromptDisplayInfo(callerHwnd.Value).displayName
                : null;
            var callerInfo = callerName != null ? $"\n[요청자] {callerName}" : "";
            Console.Error.WriteLine($"[READINESS] cmd={actionArgs}{callerInfo}{stackInfo}");
            var (approved, focusAcquired, deniedByUser) = UserInputWaitOverlay.Show(targetMainHwnd, userIdleMs, timeoutSeconds,
                positionHwnd: positionHwnd, noSound: _noSound, actionInfo: actionArgs + callerInfo + stackInfo);
            if (deniedByUser)
                Console.WriteLine("[READINESS] User rejected the focus yield -- aborting");
            result = new UserYieldResult(approved, focusAcquired);
            return result;
        }
        finally
        {
            sw.Stop();
            dotCts.Cancel();
            dotThread.Join(200);
            var label = result.Approved ? "approved" : "cancelled";
            Console.WriteLine($" -> {label} ({sw.Elapsed.TotalSeconds:F1}s)");
            Console.Out.Flush();
        }
    }

    static bool ShouldAutoApproveAll()
    {
        // Global approval policy is persisted in bin/wkappbot.hq/profiles/approval-policy.json.
        return ApprovalPolicy.AutoApproveAll;
    }
}

// -- ElevationRequesterAdapter ------------------------------------

/// <summary>
/// IElevationRequester implementation: prefer the Elevated Eye proxy, otherwise use UAC runas.
/// 1. If the Elevated Eye proxy is alive, delegate the command over the pipe.
/// 2. Otherwise, try to start Elevated Eye and then delegate through the proxy.
/// 3. If UAC is cancelled, return false and continue with focusless methods.
/// Tag: [READINESS]
/// </summary>
internal sealed class ElevationRequesterAdapter : IElevationRequester
{
    public bool RequestElevation(string targetProcessName, uint targetPid)
    {
        // MCP mode: never launch processes -- proxy only, or signal Launcher.
        if (Program.IsMcpMode || Program.RunningInEye)
        {
            Console.Error.WriteLine($"[ELEVATION] MCP/Eye mode: cannot launch admin process for {targetProcessName} (pid={targetPid})");
            if (ElevatedEyeClient.IsAvailable())
                return DelegateViaProxy();
            if (Program.IsMcpMode) Program.McpElevationRequired = true;
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [ELEVATION] {targetProcessName} (PID {targetPid}) is elevated -- requesting admin rights...");
        Console.ResetColor();

        // Strategy 1: Use the existing elevated Eye proxy.
        if (ElevatedEyeClient.IsAvailable())
        {
            Console.WriteLine("  [ELEVATION] Elevated Eye proxy found -- delegating command");
            return DelegateViaProxy();
        }

        // Strategy 2: Launch elevated Eye, then delegate.
        Console.WriteLine("  [ELEVATION] No elevated Eye -- launching admin Eye proxy...");
        if (ElevationHelper.LaunchElevatedEye("Readiness: UIPI-blocked target, proxy fallback"))
        {
            return DelegateViaProxy();
        }

        // Strategy 3: Fallback -- relaunch self as admin (legacy).
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";
            var rawArgs = Environment.GetCommandLineArgs();
            var args = string.Join(" ", rawArgs.Skip(1).Select(a =>
                a.Contains(' ') || a.Contains('"') ? $"\"{a.Replace("\"", "\\\"")}\"" : a));

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args,
                UseShellExecute = true,
                Verb = "runas",
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  [ELEVATION] Fallback: relaunching as admin: {Path.GetFileName(exePath)} {args}");
            Console.ResetColor();

            var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "READINESS");
            if (proc != null)
            {
                proc.WaitForExit();
                Environment.Exit(proc.ExitCode);
                return true;
            }
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 1223) // ERROR_CANCELLED
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ELEVATION] UAC denied -- continuing with focusless methods only");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [ELEVATION] Relaunch failed: {ex.Message}");
            Console.ResetColor();
        }

        return false;
    }

    /// <summary>Delegate current command to elevated Eye proxy and exit.</summary>
    private bool DelegateViaProxy()
    {
        var rawArgs = Environment.GetCommandLineArgs();
        // args[0] = exe path, args[1] = command, args[2..] = rest
        if (rawArgs.Length < 2) return false;

        var command = rawArgs[1];
        var args = rawArgs.Skip(2).ToArray();

        var exitCode = ElevatedEyeClient.ExecuteViaProxy(command, args);
        if (exitCode >= 0)
        {
            Environment.Exit(exitCode);
            return true; // unreachable after Exit
        }

        Console.WriteLine("  [ELEVATION] Proxy delegation failed -- falling back to runas");
        return false;
    }
}

// -- ReadinessZoomAdapter ------------------------------------------

/// <summary>
/// IReadinessZoom implementation: wraps ClickZoomHelper and reuses the previous zoom helper when possible.
/// Tag: [READINESS] [ZOOM]
/// </summary>
internal sealed class ReadinessZoomAdapter : IReadinessZoom
{
    private ClickZoomHelper? _lastZoom;

    public IActionZoom? Begin(Rectangle screenRect, IntPtr formHandle,
                              string action, string label)
    {
        // Reuse the previous zoom helper if it is still alive.
        if (_lastZoom != null)
        {
            try
            {
                _lastZoom.UpdateStatus($"[READINESS] {action}: {label}");
                return new ClickZoomAdapter(_lastZoom);
            }
            catch
            {
                _lastZoom = null; // Dead zoom helper -- create a new one.
            }
        }

        // Create a new helper.
        _lastZoom = ClickZoomHelper.BeginFromRect(screenRect, formHandle,
                                                   "readiness", label);
        if (_lastZoom == null) return null;
        return new ClickZoomAdapter(_lastZoom);
    }
}

internal static partial class Program
{
    /// <summary>
    /// Strip async state machine noise from stack frame names.
    /// "&lt;AskClaude&gt;d__409" + "MoveNext" -> "AskClaude"
    /// </summary>
    internal static string? CleanAsyncMethodName(string typeName, string methodName)
    {
        if (methodName == "MoveNext")
        {
            var m = System.Text.RegularExpressions.Regex.Match(typeName, @"<([A-Za-z][A-Za-z0-9_]*)>");
            return m.Success ? m.Groups[1].Value : null;
        }
        if (methodName.StartsWith('<') || typeName.StartsWith('<')) return null;
        return methodName;
    }
}
