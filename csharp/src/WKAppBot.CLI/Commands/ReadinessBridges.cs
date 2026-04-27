using WKAppBot.Abstractions;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: InputReadiness bridge methods -- expose internal helpers to adapters.
// Tag: [READINESS]
internal partial class Program
{
    // -- 팩토리: InputReadiness 생성 + 모든 어댑터 연결 --

    /// <summary>
    /// Create a fully-wired InputReadiness instance with blocker handler, knowhow broadcaster, and zoom factory.
    /// </summary>
    internal static InputReadiness CreateInputReadiness(string? handlersDir = null)
    {
        handlersDir ??= Path.Combine(DataDir, "handlers");
        var handlerMgr = Directory.Exists(handlersDir) ? new DialogHandlerManager(handlersDir) : null;

        // [FOCUS-GUARD] 전역 ActiveGuardYieldCallback 설정 -- CheckActiveGuard 차단 시 팝업 표시
        // 앱 전역에 하나의 UserInputWaitAdapter 공유 (팝업 중복 방지)
        InputReadiness.ActiveGuardYieldCallback ??= new UserInputWaitAdapter();

        return new InputReadiness
        {
            BlockerHandler = new BlockerHandlerAdapter(handlerMgr),
            KnowhowBroadcaster = new KnowhowBroadcasterAdapter(),
            ZoomFactory = new ReadinessZoomAdapter(),
            UserInputWait = new UserInputWaitAdapter(),
            ElevationRequester = new ElevationRequesterAdapter(),
        };
    }

    // -- 팩토리: ActionReadiness (AAR) 생성 --

    /// <summary>
    /// Create an ActionReadiness (AAR) instance wrapping the given InputReadiness.
    /// </summary>
    internal static ActionReadiness CreateActionReadiness(InputReadiness readiness)
        => new(readiness);

    // -- Delegated-handler readiness gate --

    /// <summary>
    /// Shared Probe() gate for a11y actions delegated out of the main A11yCommand loop
    /// (hack / hack-hover / hack-input / inspect / screenshot / ocr / kill).
    /// Runs the full input-position-guard pipeline (magnifier + blocker detect + yield popup)
    /// once the target hwnd is known. Even focusless actions must call this -- the magnifier
    /// ("돋보기") is a first-class UX contract, not just a focus-steal guard.
    /// Safe to call when hwnd == IntPtr.Zero (no-op).
    /// </summary>
    internal static void EnsureA11yReadiness(IntPtr hwnd, string action)
    {
        if (hwnd == IntPtr.Zero) return;
        if (InputReadiness.ReadinessCalled) return; // already probed by caller
        try
        {
            var readiness = CreateInputReadiness();
            readiness.Probe(new InputReadinessRequest
            {
                TargetHwnd = hwnd,
                IntendedAction = action,
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[READINESS] {action} probe failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Wrap any a11y command body in `using var _ = new FocusStealSentinel("a11y-click")`
    /// to catch focus steals that happen during the command. On Dispose, if the foreground
    /// window changed, it is restored and a bug report is filed via AutoBugReport.
    /// Find is exempt -- pass skip=true for the find action.
    /// </summary>
    internal readonly struct FocusStealSentinel : IDisposable
    {
        private readonly IntPtr _prevFg;
        private readonly IntPtr _prevFocusCtl; // keyboard focus control inside _prevFg
        private readonly string _action;
        private readonly bool _skip;

        public FocusStealSentinel(string action, bool skip = false)
        {
            _action = action;
            _skip = skip;
            if (skip)
            {
                _prevFg = IntPtr.Zero;
                _prevFocusCtl = IntPtr.Zero;
            }
            else
            {
                _prevFg = NativeMethods.GetForegroundWindow();
                // Capture keyboard-focus control too. Distinct from _prevFg for
                // shells like Windows Terminal where the outer window hosts a
                // tab bar + a separate content control -- ForceForegroundWindow
                // alone lands on the outer window but the caret stays lost
                // unless we also SetFocus on the original inner control.
                _prevFocusCtl = NativeMethods.GetKeyboardFocusHwnd();
            }
        }

        public void Dispose() => DetectAndRecover(phase: "end");

        /// <summary>
        /// Mid-action check. Call from inside long-running loops (like typing 1 char at a
        /// time) to catch focus theft the moment it happens instead of waiting for command
        /// exit. Returns true if theft was detected+recovered so the caller can bail out.
        /// </summary>
        public bool Checkpoint() => DetectAndRecover(phase: "mid");

        /// <summary>
        /// Read-only theft check -- returns true if the foreground window changed since
        /// entry, but does NOT restore focus and does NOT file an auto bug report.
        /// Use when the focus change may be a legitimate user action (e.g. user interacting
        /// with a save-dialog raised by WM_CLOSE during kill) and we want to de-escalate
        /// WITHOUT ripping focus back from the user.
        /// </summary>
        public bool Peek()
        {
            if (_skip || _prevFg == IntPtr.Zero) return false;
            try { return NativeMethods.GetForegroundWindow() != _prevFg; }
            catch { return false; }
        }

        private bool DetectAndRecover(string phase)
        {
            if (_skip || _prevFg == IntPtr.Zero) return false;
            try
            {
                var curFg = NativeMethods.GetForegroundWindow();
                var curFocus = NativeMethods.GetKeyboardFocusHwnd();

                // Three cases:
                //   (1) outer window changed  -> full restore + bug report
                //   (2) outer same, focus ctl changed -> inner focus slid (Windows
                //       Terminal tab bar etc.) -- restore focus ctl, report softly
                //   (3) nothing changed -> no-op
                bool outerStolen = curFg != _prevFg;
                bool innerSlid   = !outerStolen
                                && _prevFocusCtl != IntPtr.Zero
                                && curFocus != _prevFocusCtl;
                if (!outerStolen && !innerSlid) return false;

                string curTitle = "";
                try { curTitle = WindowFinder.GetWindowText(outerStolen ? curFg : _prevFg); } catch { }
                if (curTitle.Length > 40) curTitle = curTitle[..40] + "...";

                // User-active guard applies to BOTH outer steal and inner slide.
                // Shared threshold with AskCommands.Focus.cs RestoreFocusWithRetryAsync
                // (1500ms) -- a11y-focus-steal-user-active-silent-yield skill v1.3.
                // When user is actively typing/clicking, treat foreground/focus change
                // as an intentional user action. Silent yield: no restore, no
                // AutoBugReport. Only involuntary steals (idle >= 1500ms) get reported.
                var idleMs = NativeMethods.GetUserIdleMs();
                if (idleMs < 1500)
                {
                    Console.Error.WriteLine(outerStolen
                        ? $"[FOCUS-STEAL:{phase}] {_action}: was=0x{_prevFg.ToInt64():X8} " +
                          $"now=0x{curFg.ToInt64():X8} \"{curTitle}\" -- user active ({idleMs}ms), yielding"
                        : $"[FOCUS-STEAL:{phase}] {_action} inner-slide in \"{curTitle}\": " +
                          $"focusCtl was=0x{_prevFocusCtl.ToInt64():X8} now=0x{curFocus.ToInt64():X8} " +
                          $"-- user active ({idleMs}ms), yielding");
                    // Silent return -- user made a choice, no bug report.
                    return true;
                }

                // If _prevFg no longer exists (e.g. a11y close destroyed it), focus shift is expected.
                if (outerStolen && !NativeMethods.IsWindow(_prevFg))
                {
                    Console.Error.WriteLine(
                        $"[FOCUS-STEAL:{phase}] {_action}: was=0x{_prevFg.ToInt64():X8} (gone) " +
                        $"now=0x{curFg.ToInt64():X8} \"{curTitle}\" -- window closed, skip restore");
                    return false;
                }
                if (outerStolen)
                {
                    Console.Error.WriteLine(
                        $"[FOCUS-STEAL:{phase}] {_action}: was=0x{_prevFg.ToInt64():X8} " +
                        $"now=0x{curFg.ToInt64():X8} \"{curTitle}\" -- restoring");
                    NativeMethods.ForceForegroundWindow(_prevFg);
                }
                else
                {
                    // Inner-slide: before restoring the caret, re-verify focusCtl
                    // actually mismatched. Short spurious GetKeyboardFocusHwnd
                    // flickers (e.g. during CDP attach or UIA scan) can report a
                    // stale value; a 50ms re-read debounces that race so we don't
                    // report a bug for a focus that's already back on its own.
                    Thread.Sleep(50);
                    var reverifyFocus = NativeMethods.GetKeyboardFocusHwnd();
                    if (reverifyFocus == _prevFocusCtl) return false;
                    Console.Error.WriteLine(
                        $"[FOCUS-STEAL:{phase}] {_action} inner-slide in \"{curTitle}\": " +
                        $"focusCtl was=0x{_prevFocusCtl.ToInt64():X8} now=0x{reverifyFocus.ToInt64():X8} -- restoring caret");
                }

                // Always re-apply inner SetFocus via AttachThreadInput so the
                // keyboard caret lands back on the control that had it before.
                // Covers Windows Terminal's "focus landed on tab bar" case where
                // ForceForegroundWindow alone is insufficient.
                if (_prevFocusCtl != IntPtr.Zero)
                {
                    RestoreInnerFocus(_prevFocusCtl);
                }

                AutoBugReport(
                    outerStolen
                        ? $"FOCUS-STEAL {phase} during a11y {_action}: was=0x{_prevFg.ToInt64():X8} now=0x{curFg.ToInt64():X8} \"{curTitle}\""
                        : $"FOCUS-INNER-SLIDE {phase} during a11y {_action} in \"{curTitle}\": focusCtl was=0x{_prevFocusCtl.ToInt64():X8} now=0x{curFocus.ToInt64():X8}");
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// AttachThreadInput to the target control's GUI thread so SetFocus
        /// takes effect across thread boundaries, then detach. Mirrors the
        /// pattern used by FocusSnapshot.Restore in KeyboardInput.
        /// </summary>
        private static void RestoreInnerFocus(IntPtr ctlHwnd)
        {
            try
            {
                var ourTid = (uint)Environment.CurrentManagedThreadId;
                ourTid = NativeMethods.GetCurrentThreadId();
                var targetTid = NativeMethods.GetWindowThreadProcessId(ctlHwnd, out _);
                if (targetTid == 0) return;
                if (targetTid == ourTid) { NativeMethods.SetFocusRaw(ctlHwnd); return; }
                bool attached = NativeMethods.AttachThreadInput(ourTid, targetTid, true);
                try { NativeMethods.SetFocusRaw(ctlHwnd); }
                finally { if (attached) NativeMethods.AttachThreadInput(ourTid, targetTid, false); }
            }
            catch { /* best effort */ }
        }
    }

    /// <summary>
    /// Resolve a grap pattern to exactly one window for non-find a11y actions.
    /// Enforces the two shared contracts:
    ///   1. Ambiguity -> fall back to find-style candidate list + error (no silent first-pick).
    ///   2. Single match -> run EnsureA11yReadiness (magnifier / blocker / yield popup).
    /// Returns the selected WindowInfo or null when the caller should exit 1.
    /// Intended for delegated handlers that do not pass through the main A11yCommand loop:
    ///   inspect / screenshot / ocr / capture-style commands with a single-target grap.
    /// </summary>
    internal static WindowInfo? ResolveA11yTarget(string grap, string action)
    {
        var windows = WindowFinder.FindWindows(grap);
        if (windows.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[A11Y] {action}: no window matching \"{grap}\"");
            Console.ResetColor();
            return null;
        }
        if (windows.Count > 1)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine(
                $"[A11Y] {action} AMBIGUOUS -- \"{grap}\" matched {windows.Count} windows. " +
                "Narrow via #scope / hwnd: / a more specific pattern:");
            Console.ResetColor();
            foreach (var w in windows.Take(20))
                Console.Error.WriteLine($"  0x{w.Handle.ToInt64():X8}  \"{w.Title}\"");
            if (windows.Count > 20)
                Console.Error.WriteLine($"  ... and {windows.Count - 20} more");
            return null;
        }
        var target = windows[0];
        EnsureA11yReadiness(target.Handle, action);
        // Copy-paste-ready TARGET grap line (stdout) -- parallel with main A11yCommand dispatcher.
        NativeMethods.GetWindowThreadProcessId(target.Handle, out uint rPid);
        string rProc = ""; try { rProc = System.Diagnostics.Process.GetProcessById((int)rPid).ProcessName; } catch { }
        Console.WriteLine($"# TARGET {AppendFocusPath(BuildStableGrap(target.Handle, rProc), target.Handle)}");
        return target;
    }

    // -- 공용 입력위치확보: iconic zoom + focusless restore + blocker dismiss --

    /// <summary>
    /// Shared input readiness pipeline for any window: iconic zoom -> focusless restore -> blocker dismiss.
    /// Used by A11yCommand, AskCommands, and any command that needs a window ready for interaction.
    /// Returns true if the window was restored from iconic state.
    /// </summary>
    internal static bool EnsureWindowReady(IntPtr hwnd, string actionLabel, string title, InputReadiness? readiness = null)
    {
        bool wasIconic = false;

        // Step 1: Iconic -> focusless restore with zoom
        if (NativeMethods.IsIconic(hwnd))
        {
            wasIconic = true;
            Console.Error.WriteLine($"[A11Y] 0x{hwnd.ToInt64():X} \"{title}\" minimized -- restoring (focusless)");
            using var iconicZoom = ClickZoomHelper.BeginForIconic(hwnd, actionLabel, $"\"{title}\"");
            var prevFg = NativeMethods.GetForegroundWindow();
            NativeMethods.ShowWindow(hwnd, 9); // SW_RESTORE
            if (prevFg != IntPtr.Zero && prevFg != hwnd)
                NativeMethods.ForceForegroundWindow(prevFg); // AttachThreadInput restore after SW_RESTORE
            Thread.Sleep(300);
            iconicZoom?.UpdateImage();
            iconicZoom?.ShowPass("restored");
            Thread.Sleep(600);
        }

        // Step 2: Blocker detection + chain dismiss (핸들러 발동 시 연쇄 체크)
        if (readiness != null)
        {
            const int maxChain = 5;
            for (int chain = 0; chain < maxChain; chain++)
            {
                var blocker = readiness.DetectBlocker(hwnd);
                if (blocker == null) break;

                Console.WriteLine(chain == 0
                    ? $"[A11Y] blocker: {blocker.ClassName} \"{blocker.Title}\" -- dismissing"
                    : $"[A11Y] chain blocker #{chain + 1}: {blocker.ClassName} \"{blocker.Title}\" -- dismissing");
                var (handled, _) = readiness.BlockerHandler?.TryHandle(hwnd, blocker) ?? (false, false);
                if (!handled)
                {
                    Console.Error.WriteLine($"[A11Y] blocker persists: {blocker.ClassName} \"{blocker.Title}\"");
                    break;
                }
                Thread.Sleep(1000); // 연쇄 다이얼로그 출현 대기
            }
        }

        return wasIconic;
    }

    // -- Bridge: TryHandleBlocker -> adapter --

    /// <summary>
    /// Bridge for BlockerHandlerAdapter: delegates to existing TryHandleBlocker.
    /// </summary>
    internal static (bool handled, bool shouldRetry) TryHandleBlockerViaReadiness(
        IntPtr mainHwnd, DialogHandlerManager? handlerMgr)
    {
        return TryHandleBlocker(mainHwnd, handlerMgr);
    }

    // -- Bridge: Knowhow broadcast -> adapter --

    /// <summary>
    /// Bridge for KnowhowBroadcasterAdapter: delegates to BroadcastInspectKnowhow.
    /// Resolves profile matching and form ID from window handle.
    /// </summary>
    internal static void BroadcastKnowhowViaReadiness(IntPtr mainHwnd, string? formId, string? actionName)
    {
        try
        {
            var className = WindowFinder.GetClassName(mainHwnd);
            var title = WindowFinder.GetWindowText(mainHwnd);
            BroadcastInspectKnowhow(mainHwnd, className, formId, title);

        }
        catch { }
    }

    /// <summary>
    /// Create/update action-method-result knowhow file.
    /// Filename: knowhow-{action}-{method}-{success|fail}.md
    /// Called by InputCommand (and potentially other commands) after each method attempt.
    /// </summary>
    internal static void WriteActionMethodKnowhow(
        IntPtr mainHwnd, string? formId, string action, string method,
        bool success, string? notes = null)
    {
        if (string.IsNullOrEmpty(formId)) return;
        try
        {
            var className = WindowFinder.GetClassName(mainHwnd);
            NativeMethods.GetWindowThreadProcessId(mainHwnd, out uint pid);
            var procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            var profileStore = new ProfileStore();
            var profileMatch = profileStore.FindByMatch(className, procName);
            if (profileMatch == null) return;

            var a11yDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
            var formDir = Path.Combine(a11yDir, $"form_{formId}");
            Directory.CreateDirectory(formDir);

            var result = success ? "success" : "fail";
            var khPath = Path.Combine(formDir, $"knowhow-{action}-{method}-{result}.md");

            // Append if exists, create if not
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var entry = $"- [{timestamp}] {(notes ?? "(auto-recorded)")}";

            if (File.Exists(khPath))
            {
                File.AppendAllText(khPath, entry + "\n");
            }
            else
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"## [{formId}] {action}/{method} -> {result}");
                sb.AppendLine(entry);
                File.WriteAllText(khPath, sb.ToString());
            }
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Shorten MFC Afx class names for readable knowhow filenames.
    /// "Afx:009C0000:8:00010005:00000000:00000000" -> "Afx"
    /// "AfxWnd140" -> "AfxWnd"
    /// "Edit" -> "Edit" (unchanged)
    /// "#32770" -> "Dlg32770"
    /// </summary>
    internal static string ShortenClassName(string className)
    {
        if (string.IsNullOrEmpty(className)) return "unknown";

        // Afx:HEX:HEX:... -> "Afx" (MFC auto-registered class)
        if (className.StartsWith("Afx:", StringComparison.OrdinalIgnoreCase) ||
            className.StartsWith("Afx_", StringComparison.OrdinalIgnoreCase))
            return "Afx";

        // AfxWnd140, AfxWnd110s -> "AfxWnd"
        if (className.StartsWith("AfxWnd", StringComparison.OrdinalIgnoreCase))
            return "AfxWnd";

        // #32770 -> "Dlg32770"
        if (className.StartsWith("#"))
            return "Dlg" + className.Substring(1);

        // Remove version numbers: "ToolbarWindow32" -> "ToolbarWindow"
        // But keep short names as-is: "Edit", "Button", "Static"
        var sanitized = className.Replace(":", "_").Replace(" ", "");

        // Truncate to 20 chars max
        if (sanitized.Length > 20) sanitized = sanitized.Substring(0, 20);

        return sanitized;
    }
}
