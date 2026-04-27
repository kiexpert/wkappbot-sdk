using System.Diagnostics;
using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- [READINESS] Pre-action input readiness check ------------

    /// <summary>
    /// Lightweight readiness check before a11y action:
    ///   1. Minimized -> focusless restore (SW_SHOWNOACTIVATE)
    ///   2. Blocker detection (~5ms, QuickMode)
    /// Does NOT do full Probe -- keeps action latency minimal.
    /// </summary>
    private void EnsureInputReady(FlaUI.Core.AutomationElements.AutomationElement? element, string action)
    {
        var mainHwnd = _ctx.MainWindowHandle;
        if (mainHwnd == IntPtr.Zero) return;

        // Step 1: Minimized -> focusless restore
        if (NativeMethods.IsIconic(mainHwnd))
        {
            Log($"  [READINESS] Window minimized -- restoring (focusless)");
            NativeMethods.ShowWindow(mainHwnd, NativeMethods.SW_SHOWNOACTIVATE);
            Thread.Sleep(200); // wait for restore animation
        }

        // Step 2: Blocker detection (quick -- ~5ms)
        if (Readiness != null)
        {
            var blocker = Readiness.DetectBlocker(mainHwnd);
            if (blocker != null)
            {
                Log($"  [READINESS] Blocker: {blocker.ClassName} \"{blocker.Title}\" -- attempting dismiss");
                var (handled, _) = Readiness.BlockerHandler?.TryHandle(mainHwnd, blocker)
                                   ?? (false, false);
                if (!handled)
                    Log($"  [READINESS] Blocker not handled -- continuing anyway");
            }
        }
    }

    // -- [ZOOM] Overlay helper ------------------------------─

    /// <summary>
    /// Start zoom overlay for the located element (if factory is set).
    /// Sets _currentZoom field -- Execute() manages ShowPass/ShowFail/Dispose.
    /// </summary>
    private void BeginZoomForElement(FlaUI.Core.AutomationElements.AutomationElement? element, StepDefinition step)
    {
        if (CreateZoom == null || element == null) return;
        try
        {
            var rect = element.Properties.BoundingRectangle.ValueOrDefault;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            var hwnd = IntPtr.Zero;
            try { hwnd = element.Properties.NativeWindowHandle.ValueOrDefault; } catch { }

            var screenRect = new System.Drawing.Rectangle(
                (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            var formHandle = hwnd != IntPtr.Zero ? hwnd : _ctx.MainWindowHandle;
            var label = step.Target?.AutomationId ?? step.Target?.Name ?? step.Name;

            _currentZoom = CreateZoom(screenRect, formHandle, step.Action, label);
        }
        catch { /* zoom is non-critical -- never block automation */ }
    }

    // -- Smart Focus ("위치확보") ----------------------------─

    /// <summary>
    /// Ensure the target window has focus before SendInput.
    /// Called only when focusless path is not available.
    ///
    /// Flow:
    ///   1. Already focused? -> return (0ms)
    ///   2. Alert (beep + flash) -> wait alertDelay for user to switch back
    ///   3. Force focus (AttachThreadInput trick) -> retry loop
    ///   4. Timeout -> throw exception
    /// </summary>
    private void EnsureFocus()
    {
        if (!_ctx.FocusCheck) return;
        if (_ctx.MainWindowHandle == IntPtr.Zero) return;

        // Quick check -- most common case
        if (NativeMethods.IsWindowForeground(_ctx.MainWindowHandle))
            return;

        // Smart multi-phase recovery
        var (success, method) = WindowFinder.SmartBringToFront(
            _ctx.MainWindowHandle,
            timeoutSec: _ctx.FocusTimeout,
            retryDelaySec: _ctx.FocusRetryDelay,
            alertDelaySec: _ctx.FocusAlertDelay,
            consoleLock: _ctx.ConsoleLock);

        if (!success)
        {
            throw new InvalidOperationException(
                $"Focus recovery failed after {_ctx.FocusTimeout:F1}s -- " +
                "please switch to the test window or disable focus_check");
        }
    }

    /// <summary>
    /// Execute a step and return result.
    /// </summary>
    public StepResult Execute(StepDefinition step)
    {
        var sw = Stopwatch.StartNew();
        var result = new StepResult
        {
            StepName = step.Name,
            Action = step.Action
        };

        _currentZoom = null;
        try
        {
            switch (step.Action)
            {
                case "click":
                    result.LocatorMethod = DoClick(step, result);
                    break;

                case "double_click":
                    result.LocatorMethod = DoDoubleClick(step, result);
                    break;

                case "right_click":
                    result.LocatorMethod = DoRightClick(step, result);
                    break;

                case "type_text":
                    DoTypeText(step, result);
                    break;

                case "press_key":
                    DoPressKey(step, result);
                    break;

                case "hotkey":
                    DoHotkey(step, result);
                    break;

                case "wait":
                    DoWait(step, result);
                    break;

                case "assert":
                    DoAssert(step, result);
                    break;

                case "screenshot":
                    DoScreenshot(step, result);
                    break;

                case "scroll":
                    DoScroll(step, result);
                    break;

                // -- UIA Pattern actions (all focusless!) --------------
                case "toggle":
                    DoToggle(step, result);
                    break;

                case "expand":
                    DoExpandCollapse(step, result, expand: true);
                    break;

                case "collapse":
                    DoExpandCollapse(step, result, expand: false);
                    break;

                case "select":
                    DoSelect(step, result);
                    break;

                case "set_range":
                    DoSetRange(step, result);
                    break;

                case "window_close":
                    DoWindowAction(step, result, "close");
                    break;

                case "window_minimize":
                    DoWindowAction(step, result, "minimize");
                    break;

                case "window_maximize":
                    DoWindowAction(step, result, "maximize");
                    break;

                // -- Focusless UIA pattern actions (pure COM, no SendInput) --
                case "invoke":
                    DoInvoke(step, result);
                    break;

                case "set_value":
                    DoSetValue(step, result);
                    break;

                // -- Text extraction (UIA/OCR text readback) --
                case "read":
                    DoRead(step, result);
                    break;

                // -- External command runner (CreateProcessW via StartTracked guard) --
                case "shell":
                    DoShell(step, result);
                    break;

                default:
                    throw new InvalidOperationException($"Unknown action: {step.Action}");
            }

            if (result.Status == StepStatus.Pass || result.Status == 0)
                result.Status = StepStatus.Pass;

            // [ZOOM] Show success result
            _currentZoom?.ShowPass(result.ActionDetail ?? "done");
        }
        catch (Exception ex)
        {
            result.Status = StepStatus.Fail;
            result.Message = ex.Message;

            // [ZOOM] Show failure result
            _currentZoom?.ShowFail(ex.Message);
        }
        finally
        {
            _currentZoom?.Dispose();
            _currentZoom = null;
        }

        // -- Expect-Recovery gate (P1 competition response) --
        // After the action executes, poll for expected UI state.
        // If expect fails and recovery is defined, run recovery then retry.
        // All polling uses focusless UIA queries -- no SendInput, no focus steal.
        if (result.Status == StepStatus.Pass && step.Expect != null)
        {
            var expectResult = PollExpect(step.Expect, step.Target);
            if (!expectResult)
            {
                Log($"  [EXPECT] condition '{step.Expect.Condition}' NOT met after {step.Expect.Timeout}s");
                if (step.Recovery != null)
                {
                    for (int attempt = 0; attempt < step.Recovery.MaxRetries; attempt++)
                    {
                        Log($"  [RECOVERY] attempt {attempt + 1}/{step.Recovery.MaxRetries}: {step.Recovery.Action}");
                        ExecuteRecoveryAction(step.Recovery);

                        if (step.Recovery.Retry)
                        {
                            Log($"  [RECOVERY] retrying original action: {step.Action}");
                            var retryResult = Execute(new StepDefinition
                            {
                                Name = step.Name + $" (retry {attempt + 1})",
                                Action = step.Action,
                                Target = step.Target,
                                Params = step.Params,
                                Expect = step.Expect,
                            });
                            if (retryResult.Status == StepStatus.Pass)
                            {
                                result.Status = StepStatus.Pass;
                                result.Message = $"Recovered after {attempt + 1} attempt(s)";
                                Log($"  [RECOVERY] success on attempt {attempt + 1}");
                                break;
                            }
                        }
                        else
                        {
                            var recheck = PollExpect(step.Expect, step.Target);
                            if (recheck)
                            {
                                Log($"  [RECOVERY] expect met after recovery (no retry)");
                                break;
                            }
                        }

                        if (attempt == step.Recovery.MaxRetries - 1)
                        {
                            result.Status = StepStatus.Fail;
                            result.Message = $"Expect '{step.Expect.Condition}' failed after {step.Recovery.MaxRetries} recovery attempts";
                            Log($"  [RECOVERY] exhausted -- step fails");
                        }
                    }
                }
                else
                {
                    result.Status = StepStatus.Fail;
                    result.Message = $"Expect '{step.Expect.Condition}' not met (no recovery defined)";
                }
            }
            else
            {
                Log($"  [EXPECT] condition '{step.Expect.Condition}' met");
            }
        }

        sw.Stop();
        result.ElapsedMs = sw.Elapsed.TotalMilliseconds;
        return result;
    }

    /// <summary>
    /// Poll for expected UI state using focusless UIA queries only.
    /// Returns true if condition met within timeout.
    /// </summary>
    private bool PollExpect(ExpectDefinition expect, TargetDefinition? stepTarget)
    {
        var target = expect.Target ?? stepTarget;
        var deadline = Stopwatch.StartNew();
        var intervalMs = (int)(expect.Interval * 1000);

        while (deadline.Elapsed.TotalSeconds < expect.Timeout)
        {
            try
            {
                // Condition names are UIA-aligned: extensions of IUIAutomation properties/patterns.
                //   element_*       -> AutomationElement properties (IsOffscreen, IsEnabled, HasKeyboardFocus, ...)
                //   value_*         -> ValuePattern.Value operations (UIA ValuePattern)
                //   window_*        -> window-scope checks via WindowFinder
                //   toggle_*, selected, expanded, collapsed -> UIA pattern states
                bool met = expect.Condition switch
                {
                    // Element presence / state (UIA AutomationElement properties)
                    "element_visible"  => CheckElementVisible(target),   // !IsOffscreen
                    "element_enabled"  => CheckElementEnabled(target),   // IsEnabled
                    "element_absent"   => !CheckElementVisible(target),
                    "element_focused"  => CheckElementFocused(target),   // HasKeyboardFocus

                    // ValuePattern (UIA) -- the element's Value property
                    "value_contains"   => CheckValueContains(target, expect.Value ?? ""),
                    "value_equals"     => CheckValueEquals(target, expect.Value ?? ""),

                    // Window-scope (Win32 window enumeration)
                    "window_present"   => CheckWindowPresent(target),
                    "window_absent"    => !CheckWindowPresent(target),

                    // TogglePattern / SelectionItemPattern / ExpandCollapsePattern
                    "toggle_on"        => CheckToggleState(target, true),
                    "toggle_off"       => CheckToggleState(target, false),
                    "selected"         => CheckSelected(target),
                    "expanded"         => CheckExpandState(target, true),
                    "collapsed"        => CheckExpandState(target, false),

                    _ => throw new InvalidOperationException(
                        $"Unknown expect condition: {expect.Condition}. " +
                        "Valid: element_visible/enabled/absent/focused, value_contains/equals, " +
                        "window_present/absent, toggle_on/off, selected, expanded, collapsed.")
                };
                if (met) return true;
            }
            catch { /* element not found yet -- keep polling */ }

            Thread.Sleep(intervalMs);
        }
        return false;
    }

    /// <summary>Execute a recovery mini-step (no expect/recovery on recovery itself).</summary>
    private void ExecuteRecoveryAction(RecoveryDefinition recovery)
    {
        var recoveryStep = new StepDefinition
        {
            Name = "_recovery",
            Action = recovery.Action,
            Target = recovery.Target,
            Params = recovery.Params,
        };
        Execute(recoveryStep);
    }

}
