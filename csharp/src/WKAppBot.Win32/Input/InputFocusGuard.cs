using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Per-keystroke focus verification guard for SendInput-based input methods.
/// Detects interference (autocomplete dropdowns, dialogs, other apps) and
/// supports wait-and-retry strategy for reliable input delivery.
///
/// Design: "매 키스트로크 전 포커스 검증 → 간섭 감지 시 대기+재입력"
/// Tag: [GUARD]
/// </summary>
public class InputFocusGuard
{
    // ── Interference classification ──────────────────────────────
    public enum InterferenceType
    {
        None,                   // All clear — focus is on target
        AutocompleteDropdown,   // ListBox/ComboLBox stole focus (same process)
        SameProcessDialog,      // #32770 dialog appeared (same process)
        DifferentApp,           // Another process took foreground
        OverlayWindow,          // Transparent/layered window blocking
        FocusDrifted            // Focus moved to different control in same process
    }

    /// <summary>Result of a focus check.</summary>
    public record GuardResult(bool Ok, InterferenceType Type, string Detail)
    {
        public static readonly GuardResult Success = new(true, InterferenceType.None, "");
    }

    // ── State ────────────────────────────────────────────────────
    private readonly IntPtr _targetHwnd;     // The control that should have focus
    private readonly IntPtr _mainWindow;     // Main/top-level window for foreground ops
    private readonly int _maxRetries;
    private readonly uint _targetPid;
    private readonly uint _targetThreadId;
    private int _retryCount;
    private int _interferenceCount;          // Total interferences detected this session

    /// <summary>Number of interferences detected during this guard's lifetime.</summary>
    public int InterferenceCount => _interferenceCount;

    /// <summary>Number of full restarts performed.</summary>
    public int RetryCount => _retryCount;

    // ── Constructor ──────────────────────────────────────────────
    public InputFocusGuard(IntPtr targetHwnd, IntPtr mainWindow, int maxRetries = 3)
    {
        _targetHwnd = targetHwnd;
        _mainWindow = mainWindow;
        _maxRetries = maxRetries;
        _targetThreadId = NativeMethods.GetWindowThreadProcessId(targetHwnd, out _targetPid);
    }

    // ── Core API ─────────────────────────────────────────────────

    /// <summary>
    /// Quick focus check before a keystroke. Returns immediately.
    /// Use this in per-char loops for minimal overhead.
    /// </summary>
    public GuardResult CheckBeforeKeystroke()
    {
        // Level 1: Hardware focus check (most accurate)
        var gti = new NativeMethods.GUITHREADINFO
        {
            cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>()
        };

        if (NativeMethods.GetGUIThreadInfo(_targetThreadId, ref gti))
        {
            if (gti.hwndFocus == _targetHwnd)
                return GuardResult.Success;

            // Check if focused hwnd is a CHILD of targetHwnd.
            // CMaskEditEx (CWnd) contains a child Edit control — clicking it gives
            // focus to the child, not the parent. This is normal, not interference.
            if (gti.hwndFocus != IntPtr.Zero && IsChildOf(gti.hwndFocus, _targetHwnd))
                return GuardResult.Success;

            // Check if focused hwnd is a SIBLING under the same parent (same dialog/form).
            // MFC controls often share the same #32770 dialog parent — focus moving between
            // siblings in the same form is expected when clicking (e.g., Edit inside CMaskEditEx).
            if (gti.hwndFocus != IntPtr.Zero && IsSiblingOf(gti.hwndFocus, _targetHwnd))
                return GuardResult.Success;

            // Focus is elsewhere — classify
            return ClassifyInterference(gti.hwndFocus);
        }

        // GetGUIThreadInfo failed — fall back to app-level check
        return CheckAppLevel();
    }

    /// <summary>
    /// Check if childHwnd is a descendant of parentHwnd (up to 5 levels).
    /// </summary>
    private static bool IsChildOf(IntPtr childHwnd, IntPtr parentHwnd)
    {
        IntPtr current = childHwnd;
        for (int i = 0; i < 5; i++)
        {
            current = NativeMethods.GetParent(current);
            if (current == IntPtr.Zero) return false;
            if (current == parentHwnd) return true;
        }
        return false;
    }

    /// <summary>
    /// Check if two hwnds share the same immediate parent.
    /// MFC controls in the same #32770 dialog are siblings — focus moving
    /// between them during click is normal, not interference.
    /// Only returns true if both have valid parents and they match.
    /// </summary>
    private static bool IsSiblingOf(IntPtr hwnd1, IntPtr hwnd2)
    {
        var p1 = NativeMethods.GetParent(hwnd1);
        var p2 = NativeMethods.GetParent(hwnd2);
        return p1 != IntPtr.Zero && p1 == p2;
    }

    /// <summary>
    /// Verify focus and attempt to recover if lost.
    /// Blocks up to maxWaitMs polling every 50ms.
    /// Returns true if focus was restored, false if gave up.
    /// </summary>
    public bool EnsureBeforeKeystroke(int maxWaitMs = 2000)
    {
        var result = CheckBeforeKeystroke();
        if (result.Ok) return true;

        _interferenceCount++;
        Console.WriteLine($"[GUARD] Interference detected: {result.Type} — {result.Detail}");

        // Try to clear the interference
        if (TryClearInterference(result.Type, result.Detail, maxWaitMs))
        {
            Console.WriteLine($"[GUARD] Interference cleared, focus restored");
            return true;
        }

        Console.WriteLine($"[GUARD] Could not restore focus within {maxWaitMs}ms");
        return false;
    }

    /// <summary>
    /// Check if we can do another full restart. Increments retry counter.
    /// Returns false if maxRetries exceeded.
    /// </summary>
    public bool CanRetry()
    {
        if (_retryCount >= _maxRetries)
        {
            Console.WriteLine($"[GUARD] Max retries ({_maxRetries}) exceeded — aborting");
            return false;
        }
        _retryCount++;
        Console.WriteLine($"[GUARD] Restart attempt {_retryCount}/{_maxRetries}");
        return true;
    }

    /// <summary>
    /// Prepare for input restart: Home → Shift+End → Delete to clear existing text.
    /// Uses PostMessage to avoid focus dependency for the clear operation.
    /// </summary>
    public void ClearFieldForRestart()
    {
        // Home key via PostMessage
        NativeMethods.PostMessageW(_targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x24 /*VK_HOME*/, IntPtr.Zero);
        Thread.Sleep(30);
        NativeMethods.PostMessageW(_targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x24, IntPtr.Zero);
        Thread.Sleep(30);

        // Shift+End via PostMessage (select all)
        // Shift down
        NativeMethods.PostMessageW(_targetHwnd, 0x0100, (IntPtr)0x10 /*VK_SHIFT*/, IntPtr.Zero);
        Thread.Sleep(10);
        // End
        NativeMethods.PostMessageW(_targetHwnd, 0x0100, (IntPtr)0x23 /*VK_END*/, IntPtr.Zero);
        Thread.Sleep(30);
        NativeMethods.PostMessageW(_targetHwnd, 0x0101, (IntPtr)0x23, IntPtr.Zero);
        Thread.Sleep(10);
        // Shift up
        NativeMethods.PostMessageW(_targetHwnd, 0x0101, (IntPtr)0x10, IntPtr.Zero);
        Thread.Sleep(30);

        // Delete
        NativeMethods.PostMessageW(_targetHwnd, 0x0100, (IntPtr)0x2E /*VK_DELETE*/, IntPtr.Zero);
        Thread.Sleep(30);
        NativeMethods.PostMessageW(_targetHwnd, 0x0101, (IntPtr)0x2E, IntPtr.Zero);
        Thread.Sleep(50);
    }

    /// <summary>
    /// Re-establish focus on target hwnd for SendInput methods.
    /// Returns true if focus was successfully set.
    /// </summary>
    public bool RefocusTarget()
    {
        // Step 1: Ensure our main window is foreground
        if (!NativeMethods.IsWindowForeground(_mainWindow))
        {
            NativeMethods.SmartSetForegroundWindow(_mainWindow);
            Thread.Sleep(100);
        }

        // Step 2: Set focus on target control
        uint ourThread = NativeMethods.GetCurrentThreadId();
        bool attached = false;
        if (_targetThreadId != ourThread)
        {
            attached = NativeMethods.AttachThreadInput(ourThread, _targetThreadId, true);
        }

        try
        {
            NativeMethods.SetFocus(_targetHwnd);
            Thread.Sleep(50);
        }
        finally
        {
            if (attached)
                NativeMethods.AttachThreadInput(ourThread, _targetThreadId, false);
        }

        // Verify
        var check = CheckBeforeKeystroke();
        return check.Ok;
    }

    // ── Internal detection ───────────────────────────────────────

    private GuardResult ClassifyInterference(IntPtr actualFocusHwnd)
    {
        if (actualFocusHwnd == IntPtr.Zero)
        {
            // No hwnd has focus in target thread — check app level
            return CheckAppLevel();
        }

        // Get class name of the interfering window
        var sb = new StringBuilder(256);
        NativeMethods.GetClassNameW(actualFocusHwnd, sb, 256);
        string className = sb.ToString();

        // Check if it's an autocomplete dropdown
        if (className.Equals("ListBox", StringComparison.OrdinalIgnoreCase) ||
            className.Equals("ComboLBox", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("AutoComplete", StringComparison.OrdinalIgnoreCase))
        {
            return new GuardResult(false, InterferenceType.AutocompleteDropdown,
                $"class={className} hwnd=0x{actualFocusHwnd:X}");
        }

        // Check if it's a dialog
        if (className == "#32770")
        {
            var titleSb = new StringBuilder(256);
            NativeMethods.GetWindowTextW(actualFocusHwnd, titleSb, 256);
            return new GuardResult(false, InterferenceType.SameProcessDialog,
                $"dialog title=\"{titleSb}\" hwnd=0x{actualFocusHwnd:X}");
        }

        // Check if it's in the same process
        NativeMethods.GetWindowThreadProcessId(actualFocusHwnd, out uint focusPid);
        if (focusPid == _targetPid)
        {
            return new GuardResult(false, InterferenceType.FocusDrifted,
                $"class={className} hwnd=0x{actualFocusHwnd:X}");
        }

        // Different process entirely
        return new GuardResult(false, InterferenceType.DifferentApp,
            $"pid={focusPid} class={className}");
    }

    private GuardResult CheckAppLevel()
    {
        IntPtr fg = NativeMethods.GetForegroundWindow();
        if (fg == IntPtr.Zero)
            return new GuardResult(false, InterferenceType.DifferentApp, "no foreground window");

        NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
        if (fgPid != _targetPid)
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(fg, sb, 256);
            return new GuardResult(false, InterferenceType.DifferentApp,
                $"pid={fgPid} class={sb}");
        }

        // Same process but focus info unavailable
        return new GuardResult(false, InterferenceType.FocusDrifted,
            "same process, focus info unavailable");
    }

    // ── Interference clearing ────────────────────────────────────

    private bool TryClearInterference(InterferenceType type, string detail, int maxWaitMs)
    {
        int elapsed = 0;
        const int pollInterval = 50;

        switch (type)
        {
            case InterferenceType.AutocompleteDropdown:
                // Send Escape to dismiss the dropdown
                Console.WriteLine("[GUARD] Dismissing autocomplete dropdown via VK_ESCAPE");
                NativeMethods.PostMessageW(_targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x1B /*VK_ESCAPE*/, IntPtr.Zero);
                Thread.Sleep(30);
                NativeMethods.PostMessageW(_targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x1B, IntPtr.Zero);
                Thread.Sleep(100);

                // Wait for focus to return
                while (elapsed < maxWaitMs)
                {
                    if (CheckBeforeKeystroke().Ok) return true;
                    Thread.Sleep(pollInterval);
                    elapsed += pollInterval;
                }
                // Last resort: re-focus
                return RefocusTarget();

            case InterferenceType.SameProcessDialog:
                Console.WriteLine($"[GUARD] Dialog detected: {detail}");
                // Wait for dialog to close (user or DialogHandler may handle it)
                while (elapsed < maxWaitMs)
                {
                    if (CheckBeforeKeystroke().Ok) return true;
                    Thread.Sleep(pollInterval);
                    elapsed += pollInterval;
                }
                return false; // Can't auto-dismiss dialogs safely

            case InterferenceType.DifferentApp:
                Console.WriteLine($"[GUARD] Different app has foreground: {detail}");
                // Try to reclaim foreground
                NativeMethods.SmartSetForegroundWindow(_mainWindow);
                Thread.Sleep(200);

                // Then refocus target
                while (elapsed < maxWaitMs)
                {
                    if (RefocusTarget()) return true;
                    Thread.Sleep(pollInterval);
                    elapsed += pollInterval;
                }
                return false;

            case InterferenceType.FocusDrifted:
                Console.WriteLine($"[GUARD] Focus drifted: {detail}");
                return RefocusTarget();

            case InterferenceType.OverlayWindow:
                Console.WriteLine($"[GUARD] Overlay window detected: {detail}");
                // Can't auto-resolve overlays — just wait
                while (elapsed < maxWaitMs)
                {
                    if (CheckBeforeKeystroke().Ok) return true;
                    Thread.Sleep(pollInterval);
                    elapsed += pollInterval;
                }
                return false;

            default:
                return false;
        }
    }
}
