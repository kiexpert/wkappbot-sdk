using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Physical keyboard input via SendInput.
/// Supports VK codes, scan codes, and Unicode text.
/// </summary>
public static class KeyboardInput
{
    /// <summary>
    /// Full focus state snapshot: OS foreground window + keyboard focus control within it.
    /// Captured via GetForegroundWindow + GetGUIThreadInfo(hwndFocus).
    /// Use Capture() before an operation, Restore() after to undo any focus theft.
    /// </summary>
    public readonly struct FocusSnapshot
    {
        public IntPtr  Foreground    { get; init; }  // OS foreground window (SetForegroundWindow target)
        public IntPtr  FocusedCtl   { get; init; }  // keyboard-focused control inside Foreground
        public uint    FgThreadId   { get; init; }  // thread owning Foreground (for AttachThreadInput)
        public uint    ImeConversion { get; init; } // IME conversion mode (0=English, 1=Korean/Hangul ...)
        public uint    ImeSentence  { get; init; }  // IME sentence mode
        public bool    ImeValid     { get; init; }  // true if IME state was captured
        public string? ImeComposing { get; init; }  // 조합중 문자열 (e.g. "하" mid-automata), null=없음

        private const uint GCS_COMPSTR = 0x0008;
        private const uint SCS_SETSTR  = 0x0002;

        public bool IsEmpty => Foreground == IntPtr.Zero;

        /// <summary>Capture current focus state (foreground + focused control + IME mode + 조합중).</summary>
        public static FocusSnapshot Capture()
        {
            var fg = NativeMethods.GetForegroundWindow();
            if (fg == IntPtr.Zero) return default;
            uint tid = NativeMethods.GetWindowThreadProcessId(fg, out _);
            var gti = new NativeMethods.GUITHREADINFO { cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            NativeMethods.GetGUIThreadInfo(tid, ref gti);

            var imeWnd = gti.hwndFocus != IntPtr.Zero ? gti.hwndFocus : fg;
            var hIMC = NativeMethods.ImmGetContext(imeWnd);
            bool imeValid = false;
            uint imeConv = 0, imeSent = 0;
            string? imeComposing = null;
            if (hIMC != IntPtr.Zero)
            {
                imeValid = NativeMethods.ImmGetConversionStatus(hIMC, out imeConv, out imeSent);
                // 조합중 문자열 캡처 (GCS_COMPSTR)
                var compLen = NativeMethods.ImmGetCompositionStringW(hIMC, GCS_COMPSTR, null, 0);
                if (compLen > 0)
                {
                    var buf = new char[compLen / 2];
                    NativeMethods.ImmGetCompositionStringW(hIMC, GCS_COMPSTR, buf, (uint)compLen);
                    imeComposing = new string(buf);
                }
                NativeMethods.ImmReleaseContext(imeWnd, hIMC);
            }

            var snapshot = new FocusSnapshot
            {
                Foreground = fg, FocusedCtl = gti.hwndFocus, FgThreadId = tid,
                ImeConversion = imeConv, ImeSentence = imeSent, ImeValid = imeValid,
                ImeComposing = imeComposing
            };

            // 프롭 캐시: 포커스 컨트롤 hwnd를 포그라운드 윈도우에 스탬프
            // -> MidInputCheck abort 시 UIA 없이 빠르게 읽기 가능 (cross-invocation 증거보존)
            if (fg != IntPtr.Zero && gti.hwndFocus != IntPtr.Zero)
            {
                try { NativeMethods.SetPropW(fg, "WKAppBot_FocusedCtl", gti.hwndFocus); } catch { }
            }

            return snapshot;
        }

        /// <summary>
        /// Restore OS foreground AND keyboard focus control AND IME mode AND 조합중 상태.
        /// 1) SetForegroundWindow -- activates the window
        /// 2) AttachThreadInput + SetFocus -- restores the focused control inside it
        /// 3) ImmSetConversionStatus -- restores Korean/CJK input mode (한/영)
        /// 4) ImmSetCompositionString -- re-injects mid-automata composition string
        /// Returns true if foreground was actually different (= focus had drifted).
        /// </summary>
        public bool Restore()
        {
            if (IsEmpty) return false;
            bool drifted = NativeMethods.GetForegroundWindow() != Foreground;

            // Step 1: restore foreground window (raw -- this is a restore, not new acquisition)
            NativeMethods.SetForegroundWindowRaw(Foreground);

            // Step 2: restore keyboard focus control via AttachThreadInput
            if (FocusedCtl != IntPtr.Zero)
            {
                uint ourTid = NativeMethods.GetCurrentThreadId();
                NativeMethods.AttachThreadInput(ourTid, FgThreadId, true);
                try { NativeMethods.SetFocusRaw(FocusedCtl); } // Raw: this is a RESTORE, not new acquisition
                finally { NativeMethods.AttachThreadInput(ourTid, FgThreadId, false); }
            }

            // Step 3+4: restore IME state (한/영 모드 + 조합중 문자열)
            if (ImeValid)
            {
                var imeWnd = FocusedCtl != IntPtr.Zero ? FocusedCtl : Foreground;
                var hIMC = NativeMethods.ImmGetContext(imeWnd);
                if (hIMC != IntPtr.Zero)
                {
                    NativeMethods.ImmSetConversionStatus(hIMC, ImeConversion, ImeSentence);
                    // 조합중이던 글자를 composition 버퍼에 다시 밀어넣기
                    if (!string.IsNullOrEmpty(ImeComposing))
                        NativeMethods.ImmSetCompositionStringW(hIMC, SCS_SETSTR,
                            ImeComposing, (uint)(ImeComposing.Length * 2), IntPtr.Zero, 0);
                    NativeMethods.ImmReleaseContext(imeWnd, hIMC);
                }
            }

            return drifted;
        }
    }

    // -- 중간체크 abort 콜백 (CLI 레이어에서 설정 -> UIA 노드 덤프) --------------
    /// <summary>
    /// CLI 레이어에서 설정하는 콜백. MidInputCheck/MidInputFocusCheck 이상 감지 시 호출됨.
    /// 인자: (reason, context, intendedHwnd) -- CLI 레이어가 UIA automation으로 포커스 노드 덤프.
    /// </summary>
    public static Action<string, string, IntPtr>? OnMidInputAbort { get; set; }

    // -- IME injection (focusless text input via IMM32) --------------------─
    private const uint SCS_SETSTR = 0x0002;
    private const uint GCS_COMPSTR_IME = 0x0008;
    private const uint NI_COMPOSITIONSTR = 0x0015;
    private const uint CPS_COMPLETE = 0x0001;

    /// <summary>
    /// Type text into target window via IME composition injection.
    /// Focusless: uses AttachThreadInput to access target's IME context.
    /// Returns true if IME accepted the text.
    /// </summary>
    private const uint CPS_CANCEL = 0x0004;

    public static bool TypeViaIme(IntPtr hwnd, string text)
    {
        if (hwnd == IntPtr.Zero || string.IsNullOrEmpty(text)) return false;

        uint targetTid = NativeMethods.GetWindowThreadProcessId(hwnd, out _);
        uint ourTid = NativeMethods.GetCurrentThreadId();

        bool attached = NativeMethods.AttachThreadInput(ourTid, targetTid, true);
        try
        {
            var gti = new NativeMethods.GUITHREADINFO { cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            NativeMethods.GetGUIThreadInfo(targetTid, ref gti);
            var imeWnd = gti.hwndFocus != IntPtr.Zero ? gti.hwndFocus : hwnd;

            var hIMC = NativeMethods.ImmGetContext(imeWnd);
            if (hIMC == IntPtr.Zero) return false;

            try
            {
                // Backup: save existing IME conversion state + any in-progress composition
                NativeMethods.ImmGetConversionStatus(hIMC, out uint savedConv, out uint savedSent);
                var prevCompLen = NativeMethods.ImmGetCompositionStringW(hIMC, GCS_COMPSTR_IME, null, 0);
                string? prevComp = null;
                if (prevCompLen > 0)
                {
                    var buf = new char[prevCompLen / 2];
                    NativeMethods.ImmGetCompositionStringW(hIMC, GCS_COMPSTR_IME, buf, (uint)prevCompLen);
                    prevComp = new string(buf);
                }

                // Inject: set composition + complete
                bool ok = NativeMethods.ImmSetCompositionStringW(hIMC, SCS_SETSTR,
                    text, (uint)(text.Length * 2), IntPtr.Zero, 0);
                if (!ok)
                {
                    // Restore previous composition if injection failed
                    if (!string.IsNullOrEmpty(prevComp))
                        NativeMethods.ImmSetCompositionStringW(hIMC, SCS_SETSTR,
                            prevComp, (uint)(prevComp.Length * 2), IntPtr.Zero, 0);
                    return false;
                }

                NativeMethods.ImmNotifyIME(hIMC, NI_COMPOSITIONSTR, CPS_COMPLETE, 0);

                // Verify: composition buffer should be empty after commit
                Thread.Sleep(50);
                var afterLen = NativeMethods.ImmGetCompositionStringW(hIMC, GCS_COMPSTR_IME, null, 0);
                if (afterLen > 0)
                {
                    // Composition stuck -- cancel and report failure
                    NativeMethods.ImmNotifyIME(hIMC, NI_COMPOSITIONSTR, CPS_CANCEL, 0);
                    // Restore previous state
                    NativeMethods.ImmSetConversionStatus(hIMC, savedConv, savedSent);
                    if (!string.IsNullOrEmpty(prevComp))
                        NativeMethods.ImmSetCompositionStringW(hIMC, SCS_SETSTR,
                            prevComp, (uint)(prevComp.Length * 2), IntPtr.Zero, 0);
                    return false;
                }

                // Restore IME mode (한/영) -- injection may have changed it
                NativeMethods.ImmSetConversionStatus(hIMC, savedConv, savedSent);
                return true;
            }
            finally
            {
                NativeMethods.ImmReleaseContext(imeWnd, hIMC);
            }
        }
        finally
        {
            if (attached) NativeMethods.AttachThreadInput(ourTid, targetTid, false);
        }
    }

    // -- Global keyboard input lock --------------------------------------------
    // Cross-process named Mutex: "먼저 잡은 넘이 우선권" (first grabber wins).
    // While any wkappbot session is sending keystrokes, others skip SmartSetForegroundWindow.
    private const string InputLockName = "Global\\WKAppBot_KeyboardInputLock";
    [ThreadStatic] private static Mutex? _inputLock; // per-thread: each typing thread holds its own

    /// <summary>
    /// Acquire the global keyboard input lock (cross-process named Mutex).
    /// Returns true if acquired (this session now owns typing priority).
    /// Other sessions will detect the lock via IsInputLockedByOther() and yield focus.
    /// timeoutMs=0 -> immediate try only (non-blocking).
    /// </summary>
    public static bool AcquireInputLock(int timeoutMs = 0)
    {
        try
        {
            var m = new Mutex(false, InputLockName);
            if (m.WaitOne(timeoutMs))
            {
                _inputLock = m;
                return true;
            }
            m.Dispose();
            return false;
        }
        catch { return false; }
    }

    /// <summary>Release the global keyboard input lock held by this thread.</summary>
    public static void ReleaseInputLock()
    {
        try { _inputLock?.ReleaseMutex(); _inputLock?.Dispose(); }
        catch { }
        finally { _inputLock = null; }
    }

    /// <summary>
    /// Returns true if ANOTHER process currently holds the keyboard input lock.
    /// Fast (~0.1ms): tries WaitOne(0) to probe mutex state.
    /// If this thread holds the lock (_inputLock != null), returns false (own session).
    /// </summary>
    public static bool IsInputLockedByOther()
    {
        if (_inputLock != null) return false; // we hold it -- not "other"
        try
        {
            using var probe = new Mutex(false, InputLockName);
            if (probe.WaitOne(0))
            {
                probe.ReleaseMutex();
                return false; // no one holds it
            }
            return true; // someone else holds it
        }
        catch { return false; }
    }

    /// <summary>
    /// Context bag passed to TypeText/SendKeys for mid-input focus check + user yield.
    /// intendedHwnd: the window that should hold focus during input.
    /// UserInputWait: when set, pauses input and shows yield overlay on user activity.
    /// </summary>
    public sealed class TypeInputContext
    {
        public IntPtr IntendedHwnd { get; init; }
        public IUserInputWait? UserInputWait { get; init; }  // null = no yield popup
    }

    /// <summary>
    /// Mid-input focus check: verifies the OS foreground window matches the intended target,
    /// then fully restores both the foreground window AND the keyboard focus control inside it.
    /// Called per-keystroke during SendInput-based typing.
    /// intendedHwnd=Zero -> skip check.
    /// snapshot: pre-captured state for keyboard-focus control restoration.
    /// </summary>
    public static bool MidInputFocusCheck(IntPtr intendedHwnd, string context,
        FocusSnapshot snapshot = default, Action<string>? onWarning = null)
    {
        if (intendedHwnd == IntPtr.Zero) return false;
        var cur = NativeMethods.GetForegroundWindow();
        if (cur == intendedHwnd) return false;

        var msg = $"[FOCUS] ! mid-input drift @ {context}: intended={intendedHwnd:X8} now={cur:X8} -- restoring";
        onWarning?.Invoke(msg);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(msg);
        Console.ResetColor();

        if (!snapshot.IsEmpty)
        {
            // Full restore: foreground + keyboard focus control
            snapshot.Restore();
            Console.WriteLine($"[FOCUS] keyboard focus restored -> ctl={snapshot.FocusedCtl:X8}");
        }
        else
        {
            NativeMethods.SetForegroundWindowRaw(intendedHwnd); // restore -- no guard needed
        }
        Thread.Sleep(30); // brief delay for focus to settle before next key
        return true;
    }

    /// <summary>
    /// Unified mid-input liveness check -- call inside typing loops between each keystroke/token.
    /// Replaces the separate CheckUserActivity + MidInputFocusCheck call pattern.
    ///
    /// Checks (in order):
    ///   1. Competing input lock -- another wkappbot process holds Global keyboard lock -> abort
    ///   2. User activity -> yield popup if TypeInputContext.UserInputWait is configured
    ///   3. Focus drift -> restore foreground + keyboard focus control if drifted
    ///
    /// Returns false if input should be aborted (competing lock or user cancelled).
    /// </summary>
    public static bool MidInputCheck(
        string context,
        IntPtr intendedHwnd,
        ref uint lastInputBaseline,
        TypeInputContext? ctx = null,
        FocusSnapshot snapshot = default)
    {
        // 1. Competing a11y node check (cross-process named mutex)
        if (IsInputLockedByOther())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[IDLE\u26a0 LOCK-CONFLICT:{context}] another wkappbot holds input lock -- aborting input");
            Console.ResetColor();
            PrintMidInputAbortWin32("LOCK-CONFLICT", intendedHwnd);
            OnMidInputAbort?.Invoke("LOCK-CONFLICT", context, intendedHwnd);
            return false;
        }

        // 2. User activity check (yield popup if configured)
        if (!CheckUserActivity(ref lastInputBaseline, ctx))
        {
            PrintMidInputAbortWin32("USER-ACTIVITY", intendedHwnd);
            OnMidInputAbort?.Invoke("USER-ACTIVITY", context, intendedHwnd);
            return false;
        }

        // 3. Focus drift -> abort immediately + stamp stealing window for next-run yield popup
        if (MidInputFocusCheck(intendedHwnd, context, snapshot))
        {
            // Stamp the ROOT window (not just intendedHwnd child control!) so
            // InputReadiness.Probe() -- which checks mainHwnd (root) -- finds the prop.
            if (intendedHwnd != IntPtr.Zero)
            {
                try
                {
                    var rootHwnd = NativeMethods.GetAncestor(intendedHwnd, NativeMethods.GA_ROOT);
                    if (rootHwnd == IntPtr.Zero) rootHwnd = intendedHwnd;
                    NativeMethods.SetPropW(rootHwnd, "WKAppBot_FocusStealer-midInput", (IntPtr)1);
                } catch { }
            }
            PrintMidInputAbortWin32("FOCUS-DRIFT", intendedHwnd);
            OnMidInputAbort?.Invoke("FOCUS-DRIFT", context, intendedHwnd);
            return false;  // abort -- next retry will require user approval via yield popup
        }

        return true;
    }

    /// <summary>Win32 수준 포그라운드 상태 출력 -- UIA 없이 키보드 레이어에서 즉시 출력.</summary>
    private static void PrintMidInputAbortWin32(string reason, IntPtr intendedHwnd)
    {
        try
        {
            var fg = NativeMethods.GetForegroundWindow();
            if (fg == IntPtr.Zero) return;
            var fgTitle = WindowFinder.GetWindowText(fg);
            var fgClass = WindowFinder.GetClassName(fg);
            NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
            string fgProc = "?"; try { fgProc = System.Diagnostics.Process.GetProcessById((int)fgPid).ProcessName; } catch { }
            bool same = intendedHwnd != IntPtr.Zero && fg == intendedHwnd;

            Console.WriteLine($"  [WIN32] foreground : 0x{fg.ToInt64():X8}  \"{(fgTitle.Length > 55 ? fgTitle[..52] + "..." : fgTitle)}\"");
            Console.WriteLine($"          class={fgClass}  proc={fgProc}({fgPid})");
            if (intendedHwnd != IntPtr.Zero)
            {
                Console.ForegroundColor = same ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"  [WIN32] intended   : 0x{intendedHwnd.ToInt64():X8}  {(same ? "v 일치" : "X 포커스 강탈!")}");
                Console.ResetColor();
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>
    /// Check for user input activity during keystroke sending.
    /// Compares current GetLastInputInfo tick against baseline.
    /// If activity detected AND UserInputWait provided: shows yield overlay and blocks until approved.
    /// Returns true if input can continue (approved or no yield configured).
    /// Returns false if user cancelled (abort typing).
    /// </summary>
    public static bool CheckUserActivity(ref uint lastInputBaseline, TypeInputContext? ctx)
    {
        if (ctx?.UserInputWait == null) return true; // no yield configured -- always continue

        var lii = new NativeMethods.LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<NativeMethods.LASTINPUTINFO>() };
        if (!NativeMethods.GetLastInputInfo(ref lii)) return true;

        if (lii.dwTime == lastInputBaseline) return true; // no new input -- continue

        // User activity detected during input!
        lastInputBaseline = lii.dwTime; // update baseline so we don't re-trigger immediately
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[FOCUS] 유저 입력 감지 -- 포커스 양보 팝업");
        Console.ResetColor();

        var yieldResult = ctx.UserInputWait.WaitForUserYield(
            ctx.IntendedHwnd, userIdleMs: 0, timeoutSeconds: 30);

        if (!yieldResult.Approved)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[FOCUS] 유저 취소 -- 키스트로크 중단");
            Console.ResetColor();
            return false;
        }

        // After approval: restore focus + keyboard focus control
        Console.WriteLine($"[FOCUS] 승인됨 -- 포커스 복원 후 재개");
        var snapshot = FocusSnapshot.Capture();
        if (ctx.IntendedHwnd != IntPtr.Zero)
        {
            snapshot = new FocusSnapshot
            {
                Foreground = ctx.IntendedHwnd,
                FocusedCtl = snapshot.FocusedCtl,
                FgThreadId = NativeMethods.GetWindowThreadProcessId(ctx.IntendedHwnd, out _)
            };
            snapshot.Restore();
        }
        Thread.Sleep(50); // let focus settle before resuming keys
        return true;
    }

    /// <summary>
    /// Type a text string using Unicode input events.
    /// Works with any language (Korean, etc.) without VK mapping.
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// BLOCKED by CheckActiveGuard if user is actively typing.
    /// intendedHwnd: when non-zero, checks focus per-character and restores on drift.
    /// </summary>
    public static void TypeText(string text, IntPtr intendedHwnd = default, TypeInputContext? ctx = null)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard TypeText)");
        InputReadiness.AssertReadiness("KeyboardInput.TypeText");
        if (!InputReadiness.CheckActiveGuard("SendInput:TypeText")) return;
        var effectiveHwnd = ctx?.IntendedHwnd ?? intendedHwnd;
        // Acquire global input lock -- first grabber wins, others yield SmartSetForegroundWindow
        bool lockAcquired = AcquireInputLock();
        var modSnap = ModifierSnapshot.CaptureAndRelease();
        try
        {
        // Capture full focus snapshot (foreground + keyboard focus control) once before loop
        var snapshot = effectiveHwnd != IntPtr.Zero ? FocusSnapshot.Capture() : default;
        // Capture last-input baseline for user activity detection
        var lii = new NativeMethods.LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<NativeMethods.LASTINPUTINFO>() };
        NativeMethods.GetLastInputInfo(ref lii);
        uint lastInputBaseline = lii.dwTime;

        int charIndex = 0;
        foreach (char ch in text)
        {
            // Unified mid-input check: competing lock + user activity + focus drift
            if (!MidInputCheck($"TypeText[{charIndex}]", effectiveHwnd, ref lastInputBaseline, ctx, snapshot)) return;
            charIndex++;

            // \r (0x0D) → VK_RETURN: real Enter key for chat/form submission.
            // \n (0x0A) → Unicode LF: line break within multi-line inputs (e.g. code editor).
            // Callers use \r at the end to submit, \n for mid-text line breaks.
            if (ch == '\r')
            {
                KeyDown(0x0D); Thread.Sleep(10); KeyUp(0x0D);
                Thread.Sleep(10);
                continue;
            }

            var inputs = new INPUT[2];

            // Key down
            inputs[0].type = INPUT.INPUT_KEYBOARD;
            inputs[0].u.ki.wVk = 0;
            inputs[0].u.ki.wScan = ch;
            inputs[0].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE;

            // Key up
            inputs[1].type = INPUT.INPUT_KEYBOARD;
            inputs[1].u.ki.wVk = 0;
            inputs[1].u.ki.wScan = ch;
            inputs[1].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE | KeyFlags.KEYEVENTF_KEYUP;

            NativeMethods.SendInput(2, inputs, Marshal.SizeOf<INPUT>());
            Thread.Sleep(10); // small delay for stability
        }
        } finally { modSnap.Restore(); if (lockAcquired) ReleaseInputLock(); }
    }

    /// <summary>
    /// Type text by posting WM_CHAR messages to target window's message queue.
    /// Cross-process capable -- messages go directly to the target window's queue,
    /// bypassing UIPI issues that affect SendInput.
    ///
    /// Best for: MFC CMaskEdit, owner-drawn inputs, admin-to-user process input.
    /// Gemini recommendation: WM_CHAR + focus + 10-30ms delay per character.
    /// </summary>
    public static bool TypeTextViaWmChar(IntPtr hWnd, string text, int charDelayMs = 20)
    {
        if (hWnd == IntPtr.Zero || string.IsNullOrEmpty(text))
            return false;
        if (!InputReadiness.CheckActiveGuard("PostMessage:WmChar")) return false;

        foreach (char ch in text)
        {
            // WM_CHAR lParam: repeat=1, scan=0, extended=0, context=0, previous=0, transition=0
            NativeMethods.PostMessageW(hWnd, NativeMethods.WM_CHAR, (IntPtr)ch, IntPtr.Zero);
            if (charDelayMs > 0)
                Thread.Sleep(charDelayMs);
        }
        return true;
    }

    /// <summary>
    /// Set text on an Edit control via WM_SETTEXT.
    /// Replaces entire content. Works cross-process without SendInput.
    ///
    /// Best for: Standard Edit controls, RichEdit, some MFC derivatives.
    /// Not suitable for owner-drawn controls that don't process WM_SETTEXT.
    /// </summary>
    public static bool TypeTextViaWmSetText(IntPtr hWnd, string text)
    {
        if (hWnd == IntPtr.Zero) return false;
        if (!InputReadiness.CheckActiveGuard("SendMessage:WmSetText")) return false;

        // WM_SETTEXT = 0x000C, uses string overload (marshalled by P/Invoke)
        NativeMethods.SendMessageW(hWnd, 0x000C, IntPtr.Zero, text);
        return true;
    }

    /// <summary>
    /// Append text to an Edit control via EM_REPLACESEL.
    /// Inserts at current cursor position without replacing entire content.
    ///
    /// Best for: Inserting text at cursor in Edit/RichEdit controls.
    /// </summary>
    public static bool TypeTextViaEmReplaceSel(IntPtr hWnd, string text)
    {
        if (hWnd == IntPtr.Zero) return false;
        if (!InputReadiness.CheckActiveGuard("SendMessage:EmReplaceSel")) return false;

        // EM_REPLACESEL = 0x00C2, wParam=1 (can undo), string overload
        NativeMethods.SendMessageW(hWnd, 0x00C2, (IntPtr)1, text);
        return true;
    }

    /// <summary>
    /// Press and release a single key by name.
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// BLOCKED by CheckActiveGuard if user is actively typing.
    /// </summary>
    public static void PressKey(string keyName)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard PressKey)");
        InputReadiness.AssertReadiness("KeyboardInput.PressKey");
        if (!InputReadiness.CheckActiveGuard("SendInput:PressKey")) return;
        ushort vk = NameToVk(keyName);
        KeyDown(vk);
        Thread.Sleep(30);
        KeyUp(vk);
    }

    /// <summary>
    /// Press a hotkey combination (e.g., ["ctrl", "s"]).
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// BLOCKED by CheckActiveGuard if user is actively typing.
    /// </summary>
    public static void Hotkey(IReadOnlyList<string> keys)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard Hotkey)");
        InputReadiness.AssertReadiness("KeyboardInput.Hotkey");
        if (!InputReadiness.CheckActiveGuard("SendInput:Hotkey")) return;
        // Press modifiers first, then the final key
        var vks = keys.Select(NameToVk).ToList();

        // Press all
        foreach (var vk in vks)
        {
            KeyDown(vk);
            Thread.Sleep(20);
        }

        // Release in reverse order
        Thread.Sleep(30);
        for (int i = vks.Count - 1; i >= 0; i--)
        {
            KeyUp(vks[i]);
            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// Press a key down (no release).
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// </summary>
    public static void KeyDown(ushort vk)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard KeyDown)");
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = 0;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Release a key.
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// </summary>
    public static void KeyUp(ushort vk)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard KeyUp)");
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = KeyFlags.KEYEVENTF_KEYUP;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Send a sequence of key actions with +/- modifier notation.
    /// Tokens (space-separated):
    ///   +Shift   -> KeyDown (push to held stack)
    ///   -Shift   -> KeyUp (pop from stack)
    ///   F5       -> PressKey (down+up)
    ///   hello    -> per-char keystroke via VkKeyScanW (auto Shift wrap)
    /// Unreleased modifiers auto-released in LIFO order at end.
    /// BLOCKED by NativeHookFocusless (SendInput).
    /// </summary>
    public static void SendKeys(string sequence, IntPtr intendedHwnd = default, TypeInputContext? ctx = null)
    {
        NativeHookFocusless.AssertAllowed("SendInput(keyboard SendKeys)");
        InputReadiness.AssertReadiness("KeyboardInput.SendKeys");
        if (!InputReadiness.CheckActiveGuard("SendInput:SendKeys")) return;
        var effectiveHwnd = ctx?.IntendedHwnd ?? intendedHwnd;
        // Acquire global input lock -- first grabber wins, others yield SmartSetForegroundWindow
        bool lockAcquired = AcquireInputLock();
        var modSnap = ModifierSnapshot.CaptureAndRelease();
        try
        {
        // Capture full focus snapshot once -- restored per-token if focus drifts
        var snapshot = effectiveHwnd != IntPtr.Zero ? FocusSnapshot.Capture() : default;
        // Capture last-input baseline for user activity detection
        var lii = new NativeMethods.LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<NativeMethods.LASTINPUTINFO>() };
        NativeMethods.GetLastInputInfo(ref lii);
        uint lastInputBaseline = lii.dwTime;

        var heldStack = new Stack<ushort>();
        var tokens = sequence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            // Unified mid-input check: competing lock + user activity + focus drift
            if (!MidInputCheck($"SendKeys:{token}", effectiveHwnd, ref lastInputBaseline, ctx, snapshot)) return;

            if (token.StartsWith('+') && token.Length > 1)
            {
                // +Key -> hold down
                var vk = NameToVk(token[1..]);
                KeyDown(vk);
                heldStack.Push(vk);
                Thread.Sleep(20);
            }
            else if (token.StartsWith('-') && token.Length > 1)
            {
                // -Key -> release
                var vk = NameToVk(token[1..]);
                KeyUp(vk);
                // Remove from stack if present
                var temp = new Stack<ushort>();
                while (heldStack.Count > 0)
                {
                    var h = heldStack.Pop();
                    if (h != vk) temp.Push(h);
                }
                while (temp.Count > 0) heldStack.Push(temp.Pop());
                Thread.Sleep(20);
            }
            else
            {
                // Try as known key name first (F5, Enter, etc.)
                try
                {
                    var vk = NameToVk(token);
                    KeyDown(vk);
                    Thread.Sleep(30);
                    KeyUp(vk);
                    Thread.Sleep(20);
                }
                catch (ArgumentException)
                {
                    // Not a known key -> type as character sequence via VkKeyScanW
                    foreach (char ch in token)
                    {
                        short vkScan = NativeMethods.VkKeyScanW(ch);
                        if (vkScan == -1) continue; // unmappable char
                        byte vk = (byte)(vkScan & 0xFF);
                        bool needShift = (vkScan >> 8 & 1) != 0;
                        bool needCtrl = (vkScan >> 8 & 2) != 0;
                        bool needAlt = (vkScan >> 8 & 4) != 0;

                        if (needCtrl) KeyDown(0x11);
                        if (needAlt) KeyDown(0x12);
                        if (needShift) KeyDown(0x10);
                        KeyDown(vk); Thread.Sleep(20); KeyUp(vk);
                        if (needShift) KeyUp(0x10);
                        if (needAlt) KeyUp(0x12);
                        if (needCtrl) KeyUp(0x11);
                        Thread.Sleep(20);
                    }
                }
            }
        }

        // Release any remaining held keys (LIFO)
        while (heldStack.Count > 0)
        {
            KeyUp(heldStack.Pop());
            Thread.Sleep(20);
        }
        } finally { modSnap.Restore(); if (lockAcquired) ReleaseInputLock(); }
    }

    /// <summary>
    /// Map key name to virtual key code.
    /// </summary>
    public static ushort NameToVk(string name) => name.ToLowerInvariant() switch
    {
        // Modifiers
        "ctrl" or "control" => 0x11,    // VK_CONTROL
        "alt" => 0x12,                  // VK_MENU
        "shift" => 0x10,               // VK_SHIFT
        "win" or "windows" => 0x5B,    // VK_LWIN

        // Navigation
        "enter" or "return" => 0x0D,
        "tab" => 0x09,
        "escape" or "esc" => 0x1B,
        "space" => 0x20,
        "backspace" or "back" => 0x08,
        "delete" or "del" => 0x2E,
        "insert" or "ins" => 0x2D,
        "home" => 0x24,
        "end" => 0x23,
        "pageup" or "pgup" => 0x21,
        "pagedown" or "pgdn" => 0x22,

        // Arrows
        "up" => 0x26,
        "down" => 0x28,
        "left" => 0x25,
        "right" => 0x27,

        // Function keys
        "f1" => 0x70, "f2" => 0x71, "f3" => 0x72, "f4" => 0x73,
        "f5" => 0x74, "f6" => 0x75, "f7" => 0x76, "f8" => 0x77,
        "f9" => 0x78, "f10" => 0x79, "f11" => 0x7A, "f12" => 0x7B,

        // Letters (A-Z = 0x41-0x5A)
        var s when s.Length == 1 && char.IsLetter(s[0])
            => (ushort)(char.ToUpper(s[0])),

        // Numbers (0-9 = 0x30-0x39)
        var s when s.Length == 1 && char.IsDigit(s[0])
            => (ushort)s[0],

        // Misc
        "+" or "plus" => 0xBB,
        "-" or "minus" => 0xBD,
        "." or "period" => 0xBE,
        "," or "comma" => 0xBC,

        _ => throw new ArgumentException($"Unknown key name: '{name}'")
    };
}

/// <summary>
/// Snapshot modifier key state (Shift/Ctrl/Alt/Win) at entry, release all held,
/// then restore original state after typing to prevent sticky-key contamination.
/// </summary>
internal readonly struct ModifierSnapshot
{
    private static readonly byte[] _modVks = [0x10, 0x11, 0x12, 0x5B, 0x5C]; // Shift,Ctrl,Alt,LWin,RWin

    private readonly bool[] _wasDown;

    private ModifierSnapshot(bool[] wasDown) { _wasDown = wasDown; }

    /// <summary>Capture current state and immediately release any pressed modifiers.</summary>
    public static ModifierSnapshot CaptureAndRelease()
    {
        var wasDown = new bool[_modVks.Length];
        var inputs  = new List<INPUT>();
        for (int i = 0; i < _modVks.Length; i++)
        {
            wasDown[i] = (NativeMethods.GetAsyncKeyState(_modVks[i]) & 0x8000) != 0;
            if (wasDown[i])
            {
                var inp = new INPUT { type = INPUT.INPUT_KEYBOARD };
                inp.u.ki.wVk = _modVks[i];
                inp.u.ki.dwFlags = KeyFlags.KEYEVENTF_KEYUP;
                inputs.Add(inp);
            }
        }
        if (inputs.Count > 0)
        {
            var arr = inputs.ToArray();
            NativeMethods.SendInput((uint)arr.Length, arr, Marshal.SizeOf<INPUT>());
            Thread.Sleep(10);
        }
        return new ModifierSnapshot(wasDown);
    }

    /// <summary>Restore modifiers that were held before the type operation.</summary>
    public void Restore()
    {
        if (_wasDown == null) return;
        var inputs = new List<INPUT>();
        for (int i = 0; i < _modVks.Length; i++)
        {
            if (!_wasDown[i]) continue;
            var inp = new INPUT { type = INPUT.INPUT_KEYBOARD };
            inp.u.ki.wVk = _modVks[i];
            inp.u.ki.dwFlags = 0; // key down
            inputs.Add(inp);
        }
        if (inputs.Count > 0)
        {
            var arr = inputs.ToArray();
            NativeMethods.SendInput((uint)arr.Length, arr, Marshal.SizeOf<INPUT>());
        }
    }
}
