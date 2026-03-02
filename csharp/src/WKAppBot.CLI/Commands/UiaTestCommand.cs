using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// MFC-SAFE UIA pattern exerciser — tests focusless operations without poisoning COM.
    ///
    /// KEY LESSONS (paid for with many restarts):
    /// 1. GetSupportedPatterns() on MFC Scroll elements → COM 0x80040201 → permanent hang
    /// 2. Certain Tab selections can ALSO trigger COM 0x80040201
    /// 3. Once COM is poisoned, ALL UIA calls on that window fail
    /// 4. VS debugger catching the exception makes it worse (freezes UI thread)
    /// 5. Button Invoke should be tested BEFORE tabs to get results before COM dies
    /// 6. Win32 WM_GETTEXT works even after COM is poisoned (separate API)
    /// </summary>
    static int UiaTestCommand(string[] args)
    {
        if (args.Length == 0) { Console.WriteLine("Usage: wkappbot uia-test <title> [--form id] [--invoke name]"); return 1; }

        var formId = GetArgValue(args, "--form") ?? "";
        var invokeTarget = GetArgValue(args, "--invoke") ?? "";

        // Resolve grap: "window/child#uiaScope" — '/' and '#' are equivalent separators
        UIA3Automation automation;
        AutomationElement root;
        IntPtr mainHwnd;
        try
        {
            automation = new UIA3Automation();
            automation.ConnectionTimeout = TimeSpan.FromSeconds(10);
            automation.TransactionTimeout = TimeSpan.FromSeconds(10);

            var resolved = GrapHelper.ResolveFullGrap(args[0], automation);
            if (resolved == null) { Console.WriteLine("Failed to resolve grap pattern."); return 1; }
            if (resolved.Value.error != null) { Console.WriteLine(resolved.Value.error); return 1; }

            mainHwnd = resolved.Value.hwnd;
            root = resolved.Value.root;
            Console.WriteLine($"Target: \"{WindowFinder.GetWindowText(mainHwnd)}\" (0x{mainHwnd:X})");

            // Quick invoke mode — use resolved root (already scope-narrowed)
            if (!string.IsNullOrEmpty(invokeTarget))
                return QuickInvoke(mainHwnd, invokeTarget, root);

            // Find MDI child if form-id specified (additional narrowing)
            if (!string.IsNullOrEmpty(formId))
            {
                var formHwnd = FindFormHwnd(mainHwnd, formId);
                if (formHwnd != mainHwnd)
                {
                    root = automation.FromHandle(formHwnd);
                    mainHwnd = formHwnd;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UIA init failed (window hung?): {Trunc(ex.Message, 80)}");
            return 1;
        }

        int pass = 0, fail = 0, skip = 0;
        var results = new List<(string test, string result, string detail)>();
        bool comDead = false;

        void Test(string name, Func<string> action)
        {
            try
            {
                var detail = action();
                if (detail.StartsWith("SKIP")) { skip++; results.Add((name, "SKIP", detail)); Console.WriteLine($"  [SKIP] {name} — {detail}"); }
                else { pass++; results.Add((name, "PASS", detail)); Console.WriteLine($"  [PASS] {name} — {detail}"); }
            }
            catch (Exception ex)
            {
                fail++;
                var msg = Trunc(ex.Message, 100);
                results.Add((name, "FAIL", msg));
                Console.WriteLine($"  [FAIL] {name} — {msg}");
                if (ex.Message.Contains("0x80040201")) comDead = true;
            }
        }

        // ═══ Phase 1: Safe element count ═══
        Console.WriteLine("\n═══ Phase 1: MFC-Safe Element Count ═══");
        int totalElements = 0, btnCount = 0;
        try
        {
            totalElements = root.FindAllDescendants().Length;
            var tabCount = root.FindAllDescendants(x => x.ByControlType(ControlType.Tab)).Length;
            btnCount = root.FindAllDescendants(x => x.ByControlType(ControlType.Button)).Length;
            var tiCount = root.FindAllDescendants(x => x.ByControlType(ControlType.TabItem)).Length;
            Console.WriteLine($"  Total: {totalElements}  Tab: {tabCount}  TabItem: {tiCount}  Button: {btnCount}");
        }
        catch (Exception ex) { Console.WriteLine($"  ERR: {Trunc(ex.Message, 80)}"); }

        // ═══ Phase 2: Button Invoke — RUN FIRST (before tabs can poison COM) ═══
        Console.WriteLine("\n═══ Phase 2: Button Invoke — Focusless ═══");
        if (btnCount > 0 && !comDead) try
        {
            var buttons = root.FindAllDescendants(x => x.ByControlType(ControlType.Button));
            Console.WriteLine($"  Found {buttons.Length} Button elements:");
            foreach (var btn in buttons.Take(20))
            {
                if (comDead) break;
                try
                {
                    var aid = btn.Properties.AutomationId.ValueOrDefault ?? "?";
                    var name = btn.Properties.Name.ValueOrDefault ?? "";
                    var rect = btn.BoundingRectangle;
                    Console.WriteLine($"    aid={aid} name='{Trunc(name, 25)}' {rect.Width}x{rect.Height}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("0x80040201")) { comDead = true; Console.WriteLine("    ⚠ COM poisoned!"); }
                }
            }

            if (!comDead)
            {
                // Test Invoke on first 3 small buttons
                var testTargets = buttons.Where(b =>
                {
                    try { return b.BoundingRectangle.Width <= 30; } catch { return false; }
                }).Take(3).ToArray();

                Console.WriteLine($"\n  Testing Invoke on {testTargets.Length} small buttons:");
                foreach (var btn in testTargets)
                {
                    if (comDead) break;
                    var aid = btn.Properties.AutomationId.ValueOrDefault ?? "?";
                    Test($"Invoke btn[{aid}]", () =>
                    {
                        var ok = ActionApi.Invoke(btn, mainHwnd, $"btn[{aid}]");
                        if (!ok) throw new Exception("TryInvoke returned false");
                        return "focusless=true";
                    });
                    Thread.Sleep(400);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Phase 2 ERR: {Trunc(ex.Message, 80)}");
            if (ex.Message.Contains("0x80040201")) comDead = true;
        }

        // ═══ Phase 3: Tab SelectionItem ═══
        Console.WriteLine("\n═══ Phase 3: SelectionItem (Tab Switch) — Focusless ═══");
        if (comDead) Console.WriteLine("  SKIPPED — COM already dead!");
        else try
        {
            var tabs = root.FindAllDescendants(x => x.ByControlType(ControlType.Tab));
            Console.WriteLine($"  Found {tabs.Length} Tab controls");

            foreach (var tab in tabs)
            {
                if (comDead) break;
                var tabAid = tab.Properties.AutomationId.ValueOrDefault ?? "?";
                var tabItems = tab.FindAllChildren(x => x.ByControlType(ControlType.TabItem));
                Console.WriteLine($"  Tab[{tabAid}]: {tabItems.Length} items [{string.Join(", ", tabItems.Select(t => t.Name))}]");
                if (tabItems.Length < 2) continue;

                var original = tabItems.FirstOrDefault(t =>
                {
                    try { return t.Patterns.SelectionItem.PatternOrDefault?.IsSelected.Value == true; }
                    catch { return false; }
                });
                var origName = original?.Name ?? tabItems[0].Name;

                foreach (var ti in tabItems)
                {
                    if (comDead) break;
                    if (ti.Name == origName) continue;

                    Test($"Tab[{tabAid}] '{ti.Name}'", () =>
                    {
                        var ok = ActionApi.Select(ti, mainHwnd, $"Tab '{ti.Name}'");
                        if (!ok) throw new Exception("TrySelect returned false");
                        Thread.Sleep(300);
                        return "focusless=true";
                    });
                }

                // Restore
                if (original != null && !comDead)
                {
                    Test($"Tab[{tabAid}] Restore '{origName}'", () =>
                    {
                        UiaLocator.TrySelect(original);
                        return "restored";
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Phase 3 ERR: {Trunc(ex.Message, 80)}");
            if (ex.Message.Contains("0x80040201")) comDead = true;
        }

        // ═══ Phase 4: Scroll — DO NOT TOUCH ═══
        Console.WriteLine("\n═══ Phase 4: Scroll — TOXIC ON MFC ═══");
        Console.WriteLine("  SKIPPED — COM 0x80040201 → permanent hang! GetSupportedPatterns도 위험!");

        // ═══ Phase 5: Win32 WM_GETTEXT (works even with dead COM) ═══
        Console.WriteLine("\n═══ Phase 5: Win32 WM_GETTEXT — Focusless ═══");
        try
        {
            var childHwnds = new List<IntPtr>();
            NativeMethods.EnumChildWindows(mainHwnd, (hwnd, _) => { childHwnds.Add(hwnd); return true; }, IntPtr.Zero);
            Console.WriteLine($"  Win32 children: {childHwnds.Count}");

            var textFound = 0;
            foreach (var hwnd in childHwnds)
            {
                try
                {
                    var text = NativeMethods.GetWindowTextW(hwnd);
                    if (string.IsNullOrEmpty(text) || text.Length <= 1) continue;
                    textFound++;
                    if (textFound <= 20)
                    {
                        var cid = NativeMethods.GetDlgCtrlID(hwnd);
                        var cls = WindowFinder.GetClassName(hwnd);
                        Console.WriteLine($"    [{cls}] cid={cid} text='{Trunc(text, 40)}'");
                    }
                }
                catch { }
            }
            Console.WriteLine($"  Total with text: {textFound}");
        }
        catch (Exception ex) { Console.WriteLine($"  Phase 5 ERR: {Trunc(ex.Message, 80)}"); }

        // ═══ Phase 6: LegacyIA on Tabs (if COM alive) ═══
        Console.WriteLine("\n═══ Phase 6: LegacyIAccessible — Focusless ═══");
        if (comDead) Console.WriteLine("  SKIPPED — COM dead");
        else try
        {
            var tabs = root.FindAllDescendants(x => x.ByControlType(ControlType.Tab));
            foreach (var tab in tabs)
            {
                var aid = tab.Properties.AutomationId.ValueOrDefault ?? "?";
                try
                {
                    var la = tab.Patterns.LegacyIAccessible.PatternOrDefault;
                    if (la != null)
                        Console.WriteLine($"  Tab[{aid}] role={la.Role.Value} defAction='{la.DefaultAction.ValueOrDefault}'");
                }
                catch { }
            }
            var tis = root.FindAllDescendants(x => x.ByControlType(ControlType.TabItem)).Take(8);
            foreach (var ti in tis)
            {
                try
                {
                    var la = ti.Patterns.LegacyIAccessible.PatternOrDefault;
                    if (la != null)
                        Console.WriteLine($"    '{ti.Name}' role={la.Role.Value} defAction='{la.DefaultAction.ValueOrDefault}'");
                }
                catch { }
            }
        }
        catch (Exception ex) { Console.WriteLine($"  Phase 6 ERR: {Trunc(ex.Message, 80)}"); }

        // ═══ Phase 7: MDI info ═══
        Console.WriteLine("\n═══ Phase 7: MDI Info ═══");
        if (comDead) Console.WriteLine("  (COM dead — using Win32 only)");
        try
        {
            var hMdi = NativeMethods.FindWindowExW(mainHwnd, IntPtr.Zero, "MDIClient", null);
            if (hMdi != IntPtr.Zero)
            {
                var mdiChildren = WindowFinder.GetChildren(hMdi);
                Console.WriteLine($"  MDI children (Win32): {mdiChildren.Count}");
                foreach (var mc in mdiChildren.Take(5))
                    Console.WriteLine($"    [{mc.Handle:X}] '{Trunc(mc.Title, 40)}'");
            }
        }
        catch (Exception ex) { Console.WriteLine($"  Phase 7 ERR: {Trunc(ex.Message, 80)}"); }

        // ═══ Summary ═══
        Console.WriteLine($"\n═══ Summary ═══");
        Console.WriteLine($"  PASS: {pass}  FAIL: {fail}  SKIP: {skip}  Total: {pass + fail + skip}");
        if (comDead) Console.WriteLine("  ⚠ COM session was poisoned during testing — some phases skipped");

        if (fail > 0)
        {
            Console.WriteLine("\n  Failed tests:");
            foreach (var (test, _, detail) in results.Where(r => r.result == "FAIL"))
                Console.WriteLine($"    ✗ {test}: {detail}");
        }

        automation.Dispose();
        return 0;
    }

    static int QuickInvoke(IntPtr hwnd, string target, AutomationElement? preResolvedRoot = null)
    {
        using var a = new UIA3Automation();
        a.ConnectionTimeout = TimeSpan.FromSeconds(10);
        a.TransactionTimeout = TimeSpan.FromSeconds(10);
        var root = preResolvedRoot ?? a.FromHandle(hwnd);
        // Recursive walk (like inspect) to find element by name — FindAllDescendants misses Electron subtrees
        AutomationElement? btn = null;
        var candidates = new List<(string type, string name)>();
        void Walk(AutomationElement el, int depth)
        {
            if (depth > 15 || btn != null) return;
            try
            {
                var name = el.Properties.Name.ValueOrDefault ?? "";
                if (name.Equals(target, StringComparison.OrdinalIgnoreCase)
                    || (name.Length < 200 && name.Contains(target, StringComparison.OrdinalIgnoreCase)))
                {
                    bool hasInvoke = false;
                    try { hasInvoke = el.Patterns.Invoke.IsSupported; } catch { }
                    string ct = "?";
                    try { ct = el.ControlType.ToString(); } catch { }
                    if (hasInvoke)
                    {
                        btn = el;
                        return;
                    }
                    // Also check LegacyIAccessible default action as fallback (Electron Hyperlinks)
                    if (!hasInvoke && ct.Contains("Hyperlink", StringComparison.OrdinalIgnoreCase))
                    {
                        btn = el; // Hyperlinks are invokable via LegacyIA
                        return;
                    }
                    candidates.Add((ct, name.Length > 80 ? name[..80] + "..." : name));
                }
            }
            catch { }
            try { foreach (var child in el.FindAllChildren()) Walk(child, depth + 1); } catch { }
        }
        Walk(root, 0);
        if (btn == null)
        {
            Console.WriteLine($"Invokable '{target}' not found. Matches without Invoke pattern:");
            foreach (var (t, n) in candidates.Take(10)) Console.WriteLine($"  [{t}] \"{n}\"");
            return 1;
        }
        // Invoke with auto-zoom via ActionApi
        var ok = ActionApi.Invoke(btn, hwnd, target);
        Console.WriteLine(ok ? $"[PASS] Invoked '{btn.Properties.Name.ValueOrDefault}' (focusless!)" : $"[FAIL] TryInvoke false");
        return ok ? 0 : 1;
    }

    static IntPtr FindFormHwnd(IntPtr mainHwnd, string formId)
    {
        if (string.IsNullOrEmpty(formId)) return mainHwnd;
        var hMdi = NativeMethods.FindWindowExW(mainHwnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero) return mainHwnd;
        foreach (var c in WindowFinder.GetChildren(hMdi))
        {
            if (c.Title.Contains($"[{formId}]"))
            {
                Console.WriteLine($"Form: {c.Title} (0x{c.Handle:X})");
                return c.Handle;
            }
        }
        Console.WriteLine($"Form [{formId}] not found, using main window");
        return mainHwnd;
    }

    static string Trunc(string s, int maxLen) =>
        s.Length <= maxLen ? s : s[..maxLen] + "...";
}
