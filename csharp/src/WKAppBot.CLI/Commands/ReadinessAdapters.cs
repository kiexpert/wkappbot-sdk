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

    // User-activity reset window (seconds). Any detected input within the popup
    // bumps the countdown back up to this value before auto-approval fires.
    private const int ResetSeconds = 30;

    public UserYieldResult WaitForUserYield(IntPtr targetMainHwnd, uint userIdleMs, int timeoutSeconds,
                                             IntPtr positionHwnd = default)
    {
        // Auto-approve timeout: scales with user activity.
        // - User idle (>= 15s): 3s timeout → auto-approve (away from keyboard, proceed quickly)
        // - User active (< 15s): 30s reset → every detected input resets timer to 30s;
        //   only auto-approves after 30s of inactivity within the popup display.
        // Auto-approve timeout scaling (only when no PendingAction -- i.e. not a type/input action).
        // When PendingAction is set the user MUST explicitly confirm (O button) -- no auto-timeout.
        // When PendingAction is set (type/input), always use full timeout -- explicit O required.
        const uint ActiveThresholdMs = 15000;
        if (CalloutInputWindow.PendingAction != null)
        {
            timeoutSeconds = ResetSeconds; // 30s -- user MUST press O
            Console.Error.WriteLine($"[CALLOUT] 타입명령 승인 대기 ({timeoutSeconds}s) -- O 버튼 필요");
        }
        else if (ShouldAutoApproveAll())
            timeoutSeconds = userIdleMs >= ActiveThresholdMs ? 3 : ResetSeconds;


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

            // Build targetRect: prefer PendingTargetRect (exact UIA element rect, e.g. Message input)
            // over positionHwnd/window rect so the callout tail points to the actual input element.
            Rectangle targetRect = CalloutInputWindow.PendingTargetRect;
            if (targetRect.IsEmpty)
            {
                var anchorHwnd = positionHwnd != IntPtr.Zero ? positionHwnd : targetMainHwnd;
                if (anchorHwnd != IntPtr.Zero && NativeMethods.GetWindowRect(anchorHwnd, out var rc))
                    targetRect = new Rectangle(rc.Left, rc.Top, rc.Width, rc.Height);
            }

            // Body of the callout: action info + caller + stack (content the user should see).
            // If A11yCommand set PendingContent for type/set-value, prefer that (shows the
            // actual text about to be typed instead of the raw CLI args string).
            var calloutContent = CalloutInputWindow.PendingContent ?? actionArgs;

            Console.WriteLine($"[CALLOUT] 승인창 표시 → O 버튼 눌러주세요 (timeout={timeoutSeconds}s)");
            Console.Out.Flush();
            var (approved, focusAcquired, deniedByUser) = CalloutInputOverlay.ShowForReadiness(
                calloutContent, targetRect, timeoutSeconds,
                targetHwnd: targetMainHwnd, resetSeconds: ResetSeconds);
            if (deniedByUser)
                Console.WriteLine("[CALLOUT] ✗ 사용자가 거부했습니다");
            else if (!approved)
                Console.WriteLine("[CALLOUT] ✗ 타임아웃 (승인 없음)");
            else
                Console.WriteLine("[CALLOUT] ✓ 승인됨");
            Console.Out.Flush();
            result = new UserYieldResult(approved, focusAcquired);
            if (approved)
            {
                // 1. Initial delay: let the O-button click event fully process (300ms).
                Thread.Sleep(300);

                // 2. Idle gate: don't type while user is still active (mouse move, keyboard, etc.).
                //    Any input within 500ms resets the wait. Cap at 30s total.
                const uint IdleThresholdMs = 500;
                const int MaxWaitMs = 30_000;
                var gateStart = System.Diagnostics.Stopwatch.StartNew();
                while (NativeMethods.GetUserIdleMs() < IdleThresholdMs
                       && gateStart.ElapsedMilliseconds < MaxWaitMs)
                {
                    Thread.Sleep(50);
                }
                if (gateStart.ElapsedMilliseconds >= 200)
                    Console.Error.WriteLine($"[READINESS] idle gate: waited {gateStart.ElapsedMilliseconds}ms for user to settle");
            }
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

        // UAC already failed above -> don't pop another dialog for the same
        // failure mode. Strategy 3 is the legacy runas-relaunch fallback; we
        // skip it when the guard says UAC is reproducibly broken this session.
        if (ElevationHelper.ShortCircuitIfUacFailed("ReadinessStrategy3-runas-relaunch"))
            return false;

        // Strategy 3: Fallback -- relaunch self as admin (legacy).
        try
        {
            // Same env sanitize as SudoHandler/LaunchElevatedEye before runas.
            ElevationHelper.SanitizeEnvForElevatedSpawn();
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
