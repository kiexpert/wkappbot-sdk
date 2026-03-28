using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── 선배 클롣 경험 노하우 자동 방송 ──
    // 1. knowhow.md (앱 특성 개요) — 있으면 방송, 없으면 경로 안내
    // 2. knowhow-{action}.md (액션별) — 있으면 방송, 없으면 경로 안내
    // 3. knowhow-failed-actions.md — 실패 시 방송 or 안내
    static void BroadcastActionKnowhow(IntPtr hwnd, string action, bool success)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = "unknown";
            var className = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            try
            {
                var buf = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(hwnd, buf, 256);
                className = buf.ToString();
            }
            catch { }

            var safeProc = SanitizePathTokenForExp(procName);
            var safeClass = SanitizePathTokenForExp(className);
            var expDir = Path.Combine(DataDir, "experience", safeProc, safeClass);

            // 1. knowhow.md (앱 일반 특성)
            var generalPath = Path.Combine(DataDir, "experience", safeProc, "knowhow.md");
            if (File.Exists(generalPath))
                ShowKnowhowBroadcast(generalPath, "KNOWHOW:OS");
            else
                ShowKnowhowHint(generalPath, "Record app-wide automation traits (MFC/WPF/Electron quirks etc.)");

            // 2. knowhow-{action}.md (per action)
            var actionPath = Path.Combine(expDir, $"knowhow-{action}.md");
            if (File.Exists(actionPath))
                ShowKnowhowBroadcast(actionPath, "KNOWHOW:OS");
            else
                ShowKnowhowHint(actionPath, $"Record knowhow for {action}: success patterns, pitfalls, failures");

            // 3. On failure: knowhow-failed-actions.md
            if (!success)
            {
                var failedPath = Path.Combine(expDir, "knowhow-failed-actions.md");
                if (File.Exists(failedPath))
                    ShowKnowhowBroadcast(failedPath, "KNOWHOW:OS");
                else
                    ShowKnowhowHint(failedPath, "Record this failure: root cause and workaround");
            }
        }
        catch { /* best effort */ }
    }

    static void ShowKnowhowHint(string path, string hint)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"  [KNOWHOW:OS] 💌 No knowhow yet — {hint}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("     → Your expertise here saves future Claudes hours of trial-and-error. 🙏");
        Console.WriteLine("     → Please write what you just learned about this app/action:");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"        a11y file-write \"{path}\" --text \"## Knowhow\\n- your note here\"");
        Console.ResetColor();
    }

    // ── FocusStealer Knowhow Auto-Record ──
    // Called via ActionApi.OnFocusStealer when a nominally-focusless UIA action steals focus.
    // Appends a dated entry to knowhow-{action}.md for both:
    //   (window) experience/{proc}/{class}/knowhow-{action}.md
    //   (app)    experience/{proc}/knowhow.md   (if not yet mentioned)
    internal static void AppendFocusStealerKnowhow(IntPtr rootHwnd, string action)
    {
        try
        {
            // Resolve process name + class name from the target hwnd
            NativeMethods.GetWindowThreadProcessId(rootHwnd, out uint pid);
            var procName = "unknown";
            var className = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            try
            {
                var buf = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(rootHwnd, buf, 256);
                className = buf.ToString();
            }
            catch { }

            var safeProc  = SanitizePathTokenForExp(procName);
            var safeClass = SanitizePathTokenForExp(className);
            var expDir    = Path.Combine(DataDir, "experience", safeProc, safeClass);
            Directory.CreateDirectory(expDir);

            var date    = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var note    = $"\n## FocusStealer @ {date}\n" +
                          $"- Action `{action}` on [{className}] ({procName}, hwnd=0x{rootHwnd:X}) stole focus despite nominally being focusless.\n" +
                          $"- Next invocation will force yield popup (Win32 prop `WKAppBot_FocusStealer-{action}` stamped).\n" +
                          $"- If this is always the case, prefer wrapping with `--yield` or switching to SendInput approach.\n";

            // Window-class node: knowhow-{action}.md
            var actionKnowhowPath = Path.Combine(expDir, $"knowhow-{action}.md");
            File.AppendAllText(actionKnowhowPath, note);

            // App-level node: experience/{proc}/knowhow.md — add section only once per run
            var appKnowhowPath = Path.Combine(DataDir, "experience", safeProc, "knowhow.md");
            var appNote = $"\n## FocusStealer ({action}) @ {date}\n" +
                          $"- [{className}] `{action}` steals focus. See: {actionKnowhowPath}\n";
            File.AppendAllText(appKnowhowPath, appNote);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"  [FOCUSSTEALER] Knowhow recorded → {actionKnowhowPath}");
            Console.ResetColor();
        }
        catch { /* best effort */ }
    }

    static bool A11yNotYet(string action)
    {
        Console.Error.WriteLine($"[A11Y] ERROR: '{action}' is not a valid action");
        Console.Error.WriteLine("[A11Y] Window: close, minimize, maximize, restore, move, resize, focus");
        Console.Error.WriteLine("[A11Y] Element: read, find, highlight, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range");
        Console.Error.WriteLine("[A11Y] Async: wait, eval");
        return false;
    }
}
