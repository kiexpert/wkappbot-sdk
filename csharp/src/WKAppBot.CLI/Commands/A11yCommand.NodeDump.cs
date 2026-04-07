// A11yCommand.NodeDump.cs
// 입력위치확보 진입/해제 & 중간체크 이상 시 엑빌 노드 디테일 덤프.
// 대상 노드 → 조상 → 메인창까지 전부 출력 (포커스 강도 증거수집).
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── 액션 전/후 상태 비교를 위한 스냅샷 ────────────────────────────────
    private record struct NodeState(
        string?  Name,
        string?  Value,
        ToggleState?         Toggle,
        ExpandCollapseState? Expand,
        bool?    Selected,
        double?  Range,
        bool?    Enabled,
        bool?    Offscreen,
        bool?    Focused,
        System.Drawing.Rectangle? Rect
    );

    private static NodeState CaptureNodeState(AutomationElement el)
    {
        string? value = null;
        ToggleState? toggle = null;
        ExpandCollapseState? expand = null;
        bool? selected = null;
        double? range = null;
        bool? enabled = null, offscreen = null, focused = null;
        System.Drawing.Rectangle? rect = null;
        string? name = null;

        try { name    = el.Properties.Name.ValueOrDefault; } catch { }
        try { value   = el.Patterns.Value.PatternOrDefault?.Value?.ValueOrDefault; } catch { }
        try { toggle  = el.Patterns.Toggle.PatternOrDefault?.ToggleState?.ValueOrDefault; } catch { }
        try { expand  = el.Patterns.ExpandCollapse.PatternOrDefault?.ExpandCollapseState?.ValueOrDefault; } catch { }
        try { selected = el.Patterns.SelectionItem.PatternOrDefault?.IsSelected?.ValueOrDefault; } catch { }
        try { range   = el.Patterns.RangeValue.PatternOrDefault?.Value?.ValueOrDefault; } catch { }
        try { enabled  = el.Properties.IsEnabled.ValueOrDefault; } catch { }
        try { offscreen = el.Properties.IsOffscreen.ValueOrDefault; } catch { }
        try { focused  = el.Properties.HasKeyboardFocus.ValueOrDefault; } catch { }
        try { var r = el.Properties.BoundingRectangle.ValueOrDefault; rect = new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height); } catch { }

        return new NodeState(name, value, toggle, expand, selected, range, enabled, offscreen, focused, rect);
    }

    // ── 입력위치확보 진입 (BEFORE) ────────────────────────────────────────

    /// <summary>
    /// 액션 실행 전: 대상 노드 풀 덤프 + 조상 체인 메인창까지.
    /// 상태 스냅샷을 반환 → AFTER diff 비교용.
    /// </summary>
    private static NodeState PrintNodeBefore(AutomationElement el, IntPtr hwnd, string action)
    {
        const string sep = "──────────────────────────────────────────────────────────────────────";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(sep);
        Console.Error.WriteLine($"[NODE:BEFORE] action={action}");
        Console.ResetColor();
        PrintNodeDetail(el, hwnd, "  ");
        PrintAncestorChain(el, "  ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(sep);
        Console.ResetColor();
        return CaptureNodeState(el);
    }

    // ── 입력위치확보 해제 (AFTER) ─────────────────────────────────────────

    /// <summary>
    /// 액션 완료 후: 변경된 상태만 diff 출력 + 조상 체인 재확인.
    /// </summary>
    private static void PrintNodeAfter(AutomationElement el, NodeState before, string action, bool ok, long ms)
    {
        const string sep = "──────────────────────────────────────────────────────────────────────";
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(sep);
        Console.Error.WriteLine($"[NODE:AFTER]  action={action}  result={(ok ? "OK ✓" : "FAIL ✗")}  elapsed={ms}ms");
        Console.ResetColor();

        var after = CaptureNodeState(el);
        var changes = new List<string>();
        if (before.Name     != after.Name)     changes.Add($"name: \"{NdTrunc(before.Name,30)}\" → \"{NdTrunc(after.Name,30)}\"");
        if (before.Enabled  != after.Enabled)  changes.Add($"enabled: {NdB(before.Enabled)} → {NdB(after.Enabled)}");
        if (before.Offscreen!= after.Offscreen)changes.Add($"offscreen: {NdB(before.Offscreen)} → {NdB(after.Offscreen)}");
        if (before.Focused  != after.Focused)  changes.Add($"focused: {NdB(before.Focused)} → {NdB(after.Focused)}");
        if (before.Value    != after.Value)    changes.Add($"value: \"{NdTrunc(before.Value,30)}\" → \"{NdTrunc(after.Value,30)}\"");
        if (before.Toggle   != after.Toggle)   changes.Add($"toggle: {before.Toggle} → {after.Toggle}");
        if (before.Expand   != after.Expand)   changes.Add($"expand: {before.Expand} → {after.Expand}");
        if (before.Selected != after.Selected) changes.Add($"selected: {NdB(before.Selected)} → {NdB(after.Selected)}");
        if (before.Range    != after.Range)    changes.Add($"range: {before.Range:F2} → {after.Range:F2}");
        if (before.Rect     != after.Rect)     changes.Add($"rect: {NdRectStr(before.Rect)} → {NdRectStr(after.Rect)}");

        if (changes.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var c in changes) Console.WriteLine($"  ∆ {c}");
            Console.ResetColor();
        }
        else Console.WriteLine("  ∆ (no state change detected)");

        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(sep);
        Console.ResetColor();
    }

    // ── 중간체크 이상 발생 (MID-INPUT ABORT) ──────────────────────────────

    // 프롭 키: FocusSnapshot.Capture() 시 스탬프, PrintMidInputAbortNode에서 읽음
    internal const string FocusedCtlProp = "WKAppBot_FocusedCtl";

    /// <summary>
    /// 중간체크에서 의도치 않은 상황 발생 시: 액션 타겟 노드 + 현재 포커스 노드 + 조상 체인 덤프.
    /// 포커스 강탈 증거수집용.
    /// actionTarget/actionElHwnd: 클로저로 전달된 현재 실행 중인 액션의 타겟 (UIA 재스캔 없음).
    /// </summary>
    internal static void PrintMidInputAbortNode(
        string reason, string context, IntPtr intendedHwnd, UIA3Automation automation,
        AutomationElement? actionTarget = null, IntPtr actionElHwnd = default)
    {
        const string sep = "──────────────────────────────────────────────────────────────────────";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(sep);
        Console.Error.WriteLine($"[MID-INPUT:ABORT] reason={reason}  context={context}");
        Console.ResetColor();

        // Win32 수준: 포그라운드 윈도우 vs 의도한 핸들
        var fg = NativeMethods.GetForegroundWindow();
        if (fg != IntPtr.Zero)
        {
            var fgTitle = WindowFinder.GetWindowText(fg);
            var fgClass = WindowFinder.GetClassName(fg);
            NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
            string fgProc = "?"; try { fgProc = System.Diagnostics.Process.GetProcessById((int)fgPid).ProcessName; } catch { }
            bool same = intendedHwnd != IntPtr.Zero && fg == intendedHwnd;

            Console.WriteLine($"  foreground : 0x{fg.ToInt64():X8}  \"{NdTrunc(fgTitle,55)}\"");
            Console.WriteLine($"             class={fgClass}  proc={fgProc}({fgPid})");
            if (intendedHwnd != IntPtr.Zero)
            {
                Console.ForegroundColor = same ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"  intended   : 0x{intendedHwnd.ToInt64():X8}  {(same ? "✓ 포그라운드 일치" : "✗ 포커스 강탈 감지!")}");
                Console.ResetColor();
            }
        }

        // 프롭 캐시: FocusSnapshot.Capture() 시 스탬프된 포커스 컨트롤 hwnd 읽기 (Win32 only, 빠름)
        // → UIA automation.FocusedElement() 호출 전 먼저 확인
        var focusedCtlHwnd = IntPtr.Zero;
        if (intendedHwnd != IntPtr.Zero)
        {
            try { focusedCtlHwnd = NativeMethods.GetPropW(intendedHwnd, FocusedCtlProp); } catch { }
            if (focusedCtlHwnd != IntPtr.Zero)
            {
                var ctlTitle = WindowFinder.GetWindowText(focusedCtlHwnd);
                var ctlClass = WindowFinder.GetClassName(focusedCtlHwnd);
                Console.WriteLine($"  [PROP] focused ctl : 0x{focusedCtlHwnd.ToInt64():X8}  \"{NdTrunc(ctlTitle,50)}\"  class={ctlClass}");
            }
        }

        // 액션 타겟 노드 (클로저 캡처 — UIA 재스캔 없음)
        if (actionTarget != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  [action target node (closure — no UIA rescan)]");
            Console.ResetColor();
            try { PrintNodeDetail(actionTarget, actionElHwnd, "    "); PrintAncestorChain(actionTarget, "    "); }
            catch (Exception ex) { Console.WriteLine($"    (target node read error: {ex.Message})"); }
        }

        // UIA 수준: 현재 포커스된 노드 + 조상 체인 (프롭 캐시로 충분하면 스킵 가능)
        try
        {
            var focused = automation.FocusedElement();
            if (focused != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("  [currently focused UIA node + ancestor chain]");
                Console.ResetColor();
                PrintNodeDetail(focused, fg, "    ");
                PrintAncestorChain(focused, "    ");
            }
            else Console.WriteLine("  focused UIA node: (none)");
        }
        catch (Exception ex) { Console.WriteLine($"  UIA focus read error: {ex.Message}"); }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(sep);
        Console.ResetColor();
    }

    // ── 대상 노드 디테일 출력 ──────────────────────────────────────────────

    private static void PrintNodeDetail(AutomationElement el, IntPtr hwnd, string indent)
    {
        string ct = "?", name = "", aid = "", cls = "", localCt = "";
        bool? enabled = null, offscreen = null, focused = null, isContent = null, isCtrl = null, isPassword = null;
        System.Drawing.Rectangle? rect = null;
        int pid = 0; string procName = "?";

        try { ct      = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        try { name    = el.Properties.Name.ValueOrDefault ?? ""; } catch { }
        try { aid     = el.Properties.AutomationId.ValueOrDefault ?? ""; } catch { }
        try { cls     = el.Properties.ClassName.ValueOrDefault ?? ""; } catch { }
        try { localCt = el.Properties.LocalizedControlType.ValueOrDefault ?? ""; } catch { }
        try { enabled  = el.Properties.IsEnabled.ValueOrDefault; } catch { }
        try { offscreen = el.Properties.IsOffscreen.ValueOrDefault; } catch { }
        try { focused  = el.Properties.HasKeyboardFocus.ValueOrDefault; } catch { }
        try { isContent = el.Properties.IsContentElement.ValueOrDefault; } catch { }
        try { isCtrl   = el.Properties.IsControlElement.ValueOrDefault; } catch { }
        try { isPassword = el.Properties.IsPassword.ValueOrDefault; } catch { }
        try { var r = el.Properties.BoundingRectangle.ValueOrDefault; rect = new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height); } catch { }
        try { pid = el.Properties.ProcessId.ValueOrDefault; if (pid > 0) try { procName = System.Diagnostics.Process.GetProcessById(pid).ProcessName; } catch { } } catch { }

        // 패턴
        var pats = new List<string>();
        void TryPat(string n, Func<bool> f) { try { if (f()) pats.Add(n); } catch { } }
        TryPat("Invoke",         () => el.Patterns.Invoke.IsSupported);
        TryPat("Value",          () => el.Patterns.Value.IsSupported);
        TryPat("Toggle",         () => el.Patterns.Toggle.IsSupported);
        TryPat("ExpandCollapse", () => el.Patterns.ExpandCollapse.IsSupported);
        TryPat("SelectionItem",  () => el.Patterns.SelectionItem.IsSupported);
        TryPat("Selection",      () => el.Patterns.Selection.IsSupported);
        TryPat("RangeValue",     () => el.Patterns.RangeValue.IsSupported);
        TryPat("Scroll",         () => el.Patterns.Scroll.IsSupported);
        TryPat("Window",         () => el.Patterns.Window.IsSupported);
        TryPat("LegacyIA",       () => el.Patterns.LegacyIAccessible.IsSupported);
        TryPat("Text",           () => el.Patterns.Text.IsSupported);
        TryPat("Transform",      () => el.Patterns.Transform.IsSupported);

        // 현재 값
        var vals = new List<string>();
        try { var v = el.Patterns.Value.PatternOrDefault?.Value?.ValueOrDefault; if (v != null) vals.Add($"value=\"{NdTrunc(v,60)}\""); } catch { }
        try { var t = el.Patterns.Toggle.PatternOrDefault?.ToggleState?.ValueOrDefault; if (t.HasValue) vals.Add($"toggle={t}"); } catch { }
        try { var e = el.Patterns.ExpandCollapse.PatternOrDefault?.ExpandCollapseState?.ValueOrDefault; if (e.HasValue) vals.Add($"expand={e}"); } catch { }
        try { var s = el.Patterns.SelectionItem.PatternOrDefault?.IsSelected?.ValueOrDefault; if (s.HasValue) vals.Add($"selected={s}"); } catch { }
        try { var rp = el.Patterns.RangeValue.PatternOrDefault; if (rp != null) { vals.Add($"range={rp.Value?.ValueOrDefault:F2} [{rp.Minimum?.ValueOrDefault:F2}~{rp.Maximum?.ValueOrDefault:F2}]"); } } catch { }

        // 헤더
        Console.ForegroundColor = ConsoleColor.White;
        var nameDisplay = name.Length > 70 ? name[..67] + "..." : name;
        Console.WriteLine($"{indent}★ [{ct}]  \"{nameDisplay}\"  {(string.IsNullOrEmpty(aid) ? "" : $"aid=\"{aid}\"")}");
        Console.ResetColor();

        // 상세
        Console.WriteLine($"{indent}   type    : {ct}{(string.IsNullOrEmpty(localCt) || localCt == ct ? "" : $" ({localCt})")}  class={cls}");
        Console.WriteLine($"{indent}   state   : enabled={NdB(enabled)}  offscreen={NdB(offscreen)}  focused={NdB(focused)}  content={NdB(isContent)}  ctrl={NdB(isCtrl)}{(isPassword == true ? "  ⚠ PASSWORD" : "")}");
        if (rect.HasValue)
        {
            var r = rect.Value;
            Console.WriteLine($"{indent}   rect    : ({r.X},{r.Y}) {r.Width}×{r.Height}  center=({r.X + r.Width / 2},{r.Y + r.Height / 2})");
        }
        Console.WriteLine($"{indent}   process : pid={pid}  name={procName}  hwnd=0x{hwnd.ToInt64():X8}");
        if (pats.Count > 0) Console.WriteLine($"{indent}   patterns: {string.Join(" | ", pats)}");
        if (vals.Count > 0) Console.WriteLine($"{indent}   values  : {string.Join("  ", vals)}");
    }

    // ── 조상 체인: 타겟 → 메인창 ─────────────────────────────────────────

    private static void PrintAncestorChain(AutomationElement el, string indent, int maxDepth = 12)
    {
        var current = el;
        for (int level = 1; level <= maxDepth; level++)
        {
            AutomationElement? parent = null;
            try { parent = current.Parent; } catch { }
            if (parent == null) break;

            string pCt = "?", pName = "", pAid = "";
            bool? pEnabled = null;
            System.Drawing.Rectangle? pRect = null;
            IntPtr pHwnd = IntPtr.Zero;

            try { pCt   = parent.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            try { pName = parent.Properties.Name.ValueOrDefault ?? ""; } catch { }
            try { pAid  = parent.Properties.AutomationId.ValueOrDefault ?? ""; } catch { }
            try { pEnabled = parent.Properties.IsEnabled.ValueOrDefault; } catch { }
            try { var r = parent.Properties.BoundingRectangle.ValueOrDefault; pRect = new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height); } catch { }
            try { var h = parent.Properties.NativeWindowHandle.ValueOrDefault; pHwnd = h != 0 ? new IntPtr(h) : IntPtr.Zero; } catch { }

            var nameDisplay = pName.Length > 55 ? pName[..52] + "..." : pName;
            var aidInfo  = string.IsNullOrEmpty(pAid) ? "" : $"  aid=\"{pAid}\"";
            var rectInfo = pRect.HasValue ? $"  ({pRect.Value.X},{pRect.Value.Y}) {pRect.Value.Width}×{pRect.Value.Height}" : "";
            var hwndInfo = pHwnd != IntPtr.Zero ? $"  hwnd=0x{pHwnd.ToInt64():X8}" : "";
            var badState = (pEnabled == false ? "  ⚠disabled" : "") + (pRect.HasValue && pRect.Value.IsEmpty ? "  ⚠empty-rect" : "");

            Console.ForegroundColor = pCt == "Window" ? ConsoleColor.Gray : ConsoleColor.DarkGray;
            Console.WriteLine($"{indent}↑{level} [{pCt}]  \"{nameDisplay}\"{aidInfo}{rectInfo}{hwndInfo}{badState}");
            Console.ResetColor();

            // Window에 도달하면 중단 (메인창)
            if (pCt == "Window") break;

            current = parent;
        }
    }

    // ── 포맷 헬퍼 ─────────────────────────────────────────────────────────
    private static string NdB(bool? v) => v switch { true => "✓", false => "✗", null => "?" };
    private static string NdTrunc(string? s, int max) => s == null ? "(null)" : s.Length > max ? s[..max] + "…" : s;
    private static string NdRectStr(System.Drawing.Rectangle? r) =>
        r.HasValue ? $"({r.Value.X},{r.Value.Y}) {r.Value.Width}×{r.Value.Height}" : "(null)";
}
