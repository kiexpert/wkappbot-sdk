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

    // ── form_uia experience DB fallback ──
    // Looks up Dy-tagged elements from UIA scan saved by hack-hover.
    // Returns true=success, false=fail, null=not found (caller tries next tier).

    // File-level cache: formPath → (mtime, FormExperience)
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (DateTime mtime, WKAppBot.Win32.Window.FormExperience form)>
        _formUiaFileCache = new(StringComparer.OrdinalIgnoreCase);

    // Last-hit cache: (hwnd, uiaPath) → ControlExperience (invalidated on hwnd rect change)
    static (IntPtr hwnd, string uiaPath, System.Drawing.Rectangle rect, WKAppBot.Win32.Window.ControlExperience ctrl)
        _formUiaLastHit;

    static bool? TryFormUiaExpDbAction(IntPtr hwnd, string uiaPath, string action)
    {
        try
        {
            // ── Last-hit fast path ──
            var lh = _formUiaLastHit;
            if (lh.hwnd == hwnd && lh.uiaPath == uiaPath && lh.ctrl != null)
            {
                NativeMethods.GetWindowRect(hwnd, out var wrFast);
                var rectFast = new System.Drawing.Rectangle(wrFast.Left, wrFast.Top, wrFast.Right - wrFast.Left, wrFast.Bottom - wrFast.Top);
                if (rectFast == lh.rect)
                {
                    int winW2 = rectFast.Width, winH2 = rectFast.Height;
                    int cx2 = wrFast.Left + (int)(lh.ctrl.RelativeX * winW2) + lh.ctrl.Width / 2;
                    int cy2 = wrFast.Top  + (int)(lh.ctrl.RelativeY * winH2) + lh.ctrl.Height / 2;
                    var lhLabel = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(
                        lh.ctrl.ClassName ?? "Node", lh.ctrl.Role, lh.ctrl.ControlId, isDynamic: true);
                    Console.WriteLine($"[A11Y] form_uia cache: \"{uiaPath}\" → {lhLabel} ({cx2},{cy2})");
                    return DispatchFormUiaAction(action, lhLabel, lh.ctrl, cx2, cy2);
                }
            }

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            string procName;
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName.ToLowerInvariant(); }
            catch { return null; }

            var clsBuf = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
            var winCls = clsBuf.ToString();
            if (string.IsNullOrEmpty(winCls)) return null;

            var formPath = Path.Combine(DataDir, "profiles", $"{procName}_exp",
                $"form_uia_{winCls.Replace(" ", "_")}.json");
            if (!File.Exists(formPath)) return null;

            // ── File cache (mtime-invalidated) ──
            WKAppBot.Win32.Window.FormExperience? form = null;
            try
            {
                var mtime = File.GetLastWriteTimeUtc(formPath);
                if (_formUiaFileCache.TryGetValue(formPath, out var cached) && cached.mtime == mtime)
                {
                    form = cached.form;
                }
                else
                {
                    form = System.Text.Json.JsonSerializer.Deserialize<WKAppBot.Win32.Window.FormExperience>(
                        File.ReadAllText(formPath));
                    if (form != null) _formUiaFileCache[formPath] = (mtime, form);
                }
            }
            catch { return null; }
            if (form == null || form.Controls.Count == 0) return null;

            // Build a PatternMatcher for the requested grap
            bool isWild = uiaPath.Contains('*') || uiaPath.Contains('?');
            var matcher = isWild
                ? WKAppBot.Win32.Accessibility.PatternMatcher.Create(uiaPath)
                : null;

            WKAppBot.Win32.Window.ControlExperience? best = null;
            foreach (var ctrl in form.Controls)
            {
                var dyTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(
                    ctrl.ClassName ?? "Node", ctrl.Role, ctrl.ControlId, isDynamic: true);
                bool hit = matcher != null
                    ? matcher.IsMatch(dyTag)
                    : dyTag.Contains(uiaPath, StringComparison.OrdinalIgnoreCase);
                if (hit) { best = ctrl; break; }
            }
            if (best == null) return null;

            NativeMethods.GetWindowRect(hwnd, out var wr);
            int winW = wr.Right - wr.Left;
            int winH = wr.Bottom - wr.Top;
            int cx = wr.Left + (int)(best.RelativeX * winW) + best.Width / 2;
            int cy = wr.Top  + (int)(best.RelativeY * winH) + best.Height / 2;

            // Store last-hit for next call
            _formUiaLastHit = (hwnd, uiaPath,
                new System.Drawing.Rectangle(wr.Left, wr.Top, winW, winH), best);

            var dyLabel = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(
                best.ClassName ?? "Node", best.Role, best.ControlId, isDynamic: true);
            Console.WriteLine($"[A11Y] form_uia hit: \"{uiaPath}\" → {dyLabel} ({cx},{cy})");
            return DispatchFormUiaAction(action, dyLabel, best, cx, cy);
        }
        catch { return null; }
    }

    static bool DispatchFormUiaAction(string action, string label,
        WKAppBot.Win32.Window.ControlExperience ctrl, int cx, int cy)
    {
        if (action is "click" or "invoke")
        {
            WKAppBot.Win32.Input.MouseInput.Click(cx, cy);
            return true;
        }
        if (action is "find" or "inspect" or "read")
        {
            Console.WriteLine($"[A11Y] form_uia element: {label} rel=({ctrl.RelativeX:F3},{ctrl.RelativeY:F3}) size={ctrl.Width}x{ctrl.Height} screen=({cx},{cy})");
            return true;
        }
        Console.WriteLine($"[A11Y] form_uia: action '{action}' not supported for coordinate-only element");
        return false;
    }
}
