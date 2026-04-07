using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// 앱 핫키 스캐너 + 검증기.
/// - ScanAndMerge: Win32 컨트롤 + 메뉴(다국어) + UIA AccessKey → DB 누적
/// - Verify: 발동 전 라이브 확인 (스태일이면 DB에서 제거)
/// - Dispatch: 검증된 엔트리 포커스리스 발동
/// </summary>
internal static class A11yHotkeyScanner
{
    // ── 풀스캔 + 누적 머지 ────────────────────────────────────────

    public static void ScanAndMerge(IntPtr hwnd, AutomationElement? el, string processName, string? exeVersion = null)
    {
        Console.Error.WriteLine($"[HOTKEY-DB] Scanning '{processName}' (fast)...");
        var newEntries = new List<HotkeyDbEntry>();

        // ① Win32 컨트롤 레이블 (빠름)
        var parentHwnd = GetParentHwnd(hwnd, el);
        if (parentHwnd != IntPtr.Zero)
        {
            foreach (var (label, ctrlHwnd) in Win32ShortcutActivator.BuildTextMap(parentHwnd))
            {
                var cls = GetClassName(ctrlHwnd);
                newEntries.Add(new HotkeyDbEntry(label, "win32_ctrl", "ctrl_activate", 0, null, cls));
            }
        }

        // ② 라이브 HMENU — 현재 표시 언어만 (빠름, ~1ms)
        foreach (var (label, itemId) in Win32ShortcutActivator.BuildMenuTextMapLive(hwnd))
            newEntries.Add(new HotkeyDbEntry(label, "win32_menu", "menu_cmd", itemId, null, null));

        // ③ UIA AccessKey / AcceleratorKey (빠름)
        if (el != null)
        {
            try
            {
                foreach (var desc in el.FindAllDescendants())
                {
                    var name      = desc.Properties.Name.ValueOrDefault ?? "";
                    var accessKey = desc.Properties.AccessKey.ValueOrDefault ?? "";
                    var accelKey  = desc.Properties.AcceleratorKey.ValueOrDefault ?? "";
                    if (string.IsNullOrWhiteSpace(name)) continue;
                    var shortcut = !string.IsNullOrEmpty(accessKey) ? accessKey
                                 : !string.IsNullOrEmpty(accelKey)  ? accelKey : null;
                    if (shortcut != null)
                        newEntries.Add(new HotkeyDbEntry(name, "uia", "shortcut", 0, shortcut, null));
                }
            }
            catch { }
        }

        int added = HotkeyExperienceDb.Merge(processName, newEntries, exeVersion);
        HotkeyExperienceDb.MarkSessionScanned(processName);
        Console.Error.WriteLine($"[HOTKEY-DB] Fast scan done — {newEntries.Count} found, {added} new merged");

        // ④ 백그라운드: 다국어 리소스 스캔 (LoadLibraryExW, 느림)
        // 메인 스레드 차단 없이 추가 언어팩 메뉴 항목 보충
        var bgHwnd = hwnd; var bgProc = processName; var bgVer = exeVersion;
        Task.Run(() =>
        {
            try
            {
                var bgEntries = Win32ShortcutActivator.BuildMenuResourceAllLangs(bgHwnd)
                    .Select(t => new HotkeyDbEntry(t.Label, "win32_menu", "menu_cmd", t.ItemId, null, null))
                    .ToList();
                var bgAdded = HotkeyExperienceDb.Merge(bgProc, bgEntries, bgVer);
                if (bgAdded > 0)
                    Console.Error.WriteLine($"[HOTKEY-DB] BG multi-lang scan — {bgAdded} additional entries merged");
            }
            catch (Exception ex) { Console.Error.WriteLine($"[HOTKEY-DB] BG scan error: {ex.Message}"); }
        });
    }

    // ── 발동 전 라이브 검증 ───────────────────────────────────────

    /// <summary>
    /// DB 엔트리가 현재 창 상태에서 여전히 유효한지 확인.
    /// 스태일이면 DB에서 제거하고 false 반환.
    /// shortcut 타입은 항상 유효 (키 조합은 불변).
    /// </summary>
    public static bool Verify(HotkeyDbEntry entry, IntPtr hwnd, IntPtr parentHwnd, string processName, string? exeVersion = null)
    {
        bool valid = entry.Method switch
        {
            "menu_cmd"      => VerifyMenuCmd(hwnd, entry.ItemId),
            "ctrl_activate" => VerifyCtrl(parentHwnd, entry.Label),
            "shortcut"      => true, // 키 조합은 항상 유효
            _               => false
        };

        if (!valid)
        {
            Console.Error.WriteLine($"[HOTKEY-DB] Stale entry '{entry.Label}' ({entry.Method}) — removing");
            HotkeyExperienceDb.RemoveStale(processName, [entry.Label], exeVersion);
        }
        return valid;
    }

    static bool VerifyMenuCmd(IntPtr hwnd, uint itemId)
    {
        // 메뉴 트리에서 itemId가 아직 존재하는지 확인
        var hMenu = NativeMethods.GetMenu(hwnd);
        if (hMenu == IntPtr.Zero) return false;
        return FindMenuItemId(hMenu, itemId);
    }

    static bool FindMenuItemId(IntPtr hMenu, uint targetId)
    {
        int count = NativeMethods.GetMenuItemCount(hMenu);
        for (int i = 0; i < count; i++)
        {
            var sub = NativeMethods.GetSubMenu(hMenu, i);
            if (sub != IntPtr.Zero) { if (FindMenuItemId(sub, targetId)) return true; continue; }
            if (NativeMethods.GetMenuItemID(hMenu, i) == targetId) return true;
        }
        return false;
    }

    static bool VerifyCtrl(IntPtr parentHwnd, string label)
    {
        if (parentHwnd == IntPtr.Zero) return false;
        var textMap = Win32ShortcutActivator.BuildTextMap(parentHwnd);
        return textMap.Any(t => t.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
    }

    // ── 발동 ─────────────────────────────────────────────────────

    public static bool Dispatch(HotkeyDbEntry entry, IntPtr hwnd, IntPtr parentHwnd)
    {
        switch (entry.Method)
        {
            case "menu_cmd":
                Console.WriteLine($"  [HOTKEY-DB] menu_cmd 0x{entry.ItemId:X4} '{entry.Label}'");
                return Win32ShortcutActivator.DispatchMenuItem(hwnd, entry.ItemId);

            case "ctrl_activate":
                Console.WriteLine($"  [HOTKEY-DB] ctrl_activate '{entry.Label}'");
                var textMap = Win32ShortcutActivator.BuildTextMap(parentHwnd);
                var ctrl = textMap.FirstOrDefault(t =>
                    t.Label.Equals(entry.Label, StringComparison.OrdinalIgnoreCase));
                if (ctrl.Hwnd == IntPtr.Zero)
                { Console.Error.WriteLine($"  [HOTKEY-DB] ctrl not found: '{entry.Label}'"); return false; }
                return Win32ShortcutActivator.ActivateFocusless(ctrl.Hwnd, parentHwnd);

            case "shortcut":
                Console.WriteLine($"  [HOTKEY-DB] shortcut '{entry.Shortcut}' for '{entry.Label}'");
                return Win32ShortcutActivator.DispatchShortcutViaPostMessage(hwnd, entry.Shortcut ?? "");

            default:
                Console.Error.WriteLine($"  [HOTKEY-DB] Unknown method '{entry.Method}'");
                return false;
        }
    }

    // ── Helpers ───────────────────────────────────────────────────

    internal static IntPtr GetParentHwnd(IntPtr hwnd, AutomationElement? el)
    {
        if (el != null)
        {
            try
            {
                var elHwnd = el.Properties.NativeWindowHandle.ValueOrDefault;
                if (elHwnd != IntPtr.Zero) return NativeMethods.GetParent(elHwnd);
            }
            catch { }
        }
        return NativeMethods.GetParent(hwnd);
    }

    static string GetClassName(IntPtr hwnd)
    {
        var sb = new System.Text.StringBuilder(128);
        NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
        return sb.ToString();
    }
}
