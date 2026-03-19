using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// 레거시 Win32 컨트롤의 단축키(&amp; 프리픽스)를 감지하여 포커스리스로 활성화.
///
/// 동작 원리:
///   - Static/Label: "&amp;검색" → 'ㄱ' or "&amp;S" → 'S' — 다음 탭스톱 컨트롤이 실제 입력 타겟
///   - Button:       "&amp;확인" → BM_CLICK PostMessage (포커스 불필요)
///   - Edit:         EM_SETSEL(0,-1) → 캐럿 배치 (포커스 불필요)
///   - ComboBox:     CB_SETCURSEL 또는 WM_SETFOCUS 시도
///
/// 레거시 Win32는 WM_COMMAND/BM_CLICK이 포커스 무관하게 동작 → 완전 포커스리스 가능.
/// Tag: [LEGACY] [FOCUSLESS]
/// </summary>
public static class Win32ShortcutActivator
{
    private const uint BM_CLICK    = 0x00F5;
    private const uint EM_SETSEL   = 0x00B1;
    private const uint WM_COMMAND  = 0x0111;
    private const int  BN_CLICKED  = 0;

    // ── Shortcut extraction ──────────────────────────────────────

    /// <summary>
    /// Window text에서 &amp; 프리픽스 단축키 문자 추출.
    /// "&amp;저장" → 'S' (ASCII upper), "검색(&amp;S)" → 'S', "확인(&amp;O)" → 'O'
    /// </summary>
    public static char? ExtractShortcutChar(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;
        var idx = text.IndexOf('&');
        if (idx < 0 || idx + 1 >= text.Length) return null;
        var ch = text[idx + 1];
        if (ch == '&') return null; // && = literal &
        return char.ToUpperInvariant(ch);
    }

    /// <summary>
    /// hwnd의 window text에서 단축키 문자 추출.
    /// </summary>
    public static char? GetShortcutChar(IntPtr hwnd)
    {
        var sb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(hwnd, sb, sb.Capacity);
        return ExtractShortcutChar(sb.ToString());
    }

    // ── Shortcut map ─────────────────────────────────────────────

    /// <summary>
    /// 부모 창의 모든 자식 컨트롤에서 단축키 맵 빌드.
    /// 반환: char(대문자) → hwnd
    /// </summary>
    public static Dictionary<char, IntPtr> BuildShortcutMap(IntPtr parentHwnd)
    {
        var map = new Dictionary<char, IntPtr>();
        NativeMethods.EnumChildWindows(parentHwnd, (hwnd, _) =>
        {
            var ch = GetShortcutChar(hwnd);
            if (ch.HasValue && !map.ContainsKey(ch.Value))
                map[ch.Value] = hwnd;
            return true;
        }, IntPtr.Zero);
        return map;
    }

    /// <summary>
    /// 부모 창의 모든 자식 컨트롤에서 레이블 텍스트(& 제거) → hwnd 맵 빌드.
    /// "&저장" → "저장", "검색(&S)" → "검색(S)"
    /// 가장 긴 키부터 매칭해야 하므로 길이 내림차순 정렬된 리스트로 반환.
    /// </summary>
    public static List<(string Label, IntPtr Hwnd)> BuildTextMap(IntPtr parentHwnd)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var list = new List<(string Label, IntPtr Hwnd)>();
        NativeMethods.EnumChildWindows(parentHwnd, (hwnd, _) =>
        {
            var raw = GetWindowText(hwnd);
            if (string.IsNullOrWhiteSpace(raw)) return true;
            var label = StripShortcutMarkers(raw);
            if (!string.IsNullOrWhiteSpace(label) && seen.Add(label))
                list.Add((label, hwnd));
            return true;
        }, IntPtr.Zero);
        // 긴 레이블 우선 매칭 (예: "저장하기" > "저장")
        list.Sort((a, b) => b.Label.Length.CompareTo(a.Label.Length));
        return list;
    }

    /// <summary>
    /// Win32 창 텍스트에서 &amp; 단축키 마커 제거.
    /// "&저장" → "저장", "검색(&S)" → "검색(S)", "&amp;&amp;" → "&"
    /// </summary>
    public static string StripShortcutMarkers(string text)
    {
        if (!text.Contains('&')) return text;
        var sb = new StringBuilder(text.Length);
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '&')
            {
                if (i + 1 < text.Length && text[i + 1] == '&')
                { sb.Append('&'); i++; } // && → literal &
                // else: single & → skip (shortcut marker, hidden in display)
            }
            else
                sb.Append(text[i]);
        }
        return sb.ToString();
    }

    // ── Control type detection ────────────────────────────────────

    private static string GetClassName(IntPtr hwnd)
    {
        var sb = new StringBuilder(128);
        NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
        return sb.ToString();
    }

    private static bool IsButton(string cls) =>
        cls.Equals("Button", StringComparison.OrdinalIgnoreCase);

    private static bool IsEdit(string cls) =>
        cls.Equals("Edit", StringComparison.OrdinalIgnoreCase);

    private static bool IsStatic(string cls) =>
        cls.Equals("Static", StringComparison.OrdinalIgnoreCase);

    private static bool IsComboBox(string cls) =>
        cls.Equals("ComboBox", StringComparison.OrdinalIgnoreCase);

    // ── Linked control (Static label → next tabstop) ─────────────

    /// <summary>
    /// Static 라벨의 &amp; 단축키가 가리키는 실제 입력 컨트롤 찾기.
    /// Z-order에서 labelHwnd 다음 WS_TABSTOP 형제 컨트롤 반환.
    /// </summary>
    public static IntPtr FindLinkedControl(IntPtr labelHwnd)
    {
        var cur = NativeMethods.GetWindow(labelHwnd, NativeMethods.GW_HWNDNEXT);
        while (cur != IntPtr.Zero)
        {
            if (NativeMethods.IsWindowVisible(cur))
            {
                var style = NativeMethods.GetWindowLongW(cur, NativeMethods.GWL_STYLE);
                if ((style & NativeMethods.WS_TABSTOP) != 0)
                    return cur;
            }
            cur = NativeMethods.GetWindow(cur, NativeMethods.GW_HWNDNEXT);
        }
        return IntPtr.Zero;
    }

    // ── Focusless activation ──────────────────────────────────────

    /// <summary>
    /// 컨트롤 타입에 따라 포커스리스 활성화.
    /// Button  → BM_CLICK PostMessage
    /// Edit    → EM_SETSEL(0,-1) (캐럿 끝으로)
    /// Static  → FindLinkedControl → 재귀 호출
    /// </summary>
    public static bool ActivateFocusless(IntPtr ctrlHwnd, IntPtr parentHwnd = default)
    {
        if (ctrlHwnd == IntPtr.Zero) return false;
        if (!NativeMethods.IsWindowVisible(ctrlHwnd)) return false;
        if ((NativeMethods.GetWindowLongW(ctrlHwnd, NativeMethods.GWL_STYLE) & NativeMethods.WS_DISABLED) != 0)
            return false;

        var cls = GetClassName(ctrlHwnd);

        if (IsButton(cls))
        {
            NativeMethods.PostMessageW(ctrlHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine($"  [WIN32-SC] BM_CLICK → 0x{ctrlHwnd:X8} \"{GetWindowText(ctrlHwnd)}\"");
            return true;
        }

        if (IsEdit(cls))
        {
            // EM_SETSEL(0, -1): 전체 선택 + 캐럿 배치 (포커스 불필요)
            NativeMethods.PostMessageW(ctrlHwnd, EM_SETSEL, IntPtr.Zero, (IntPtr)(-1));
            Console.WriteLine($"  [WIN32-SC] EM_SETSEL(0,-1) → Edit 0x{ctrlHwnd:X8}");
            return true;
        }

        if (IsStatic(cls))
        {
            // 라벨 → 다음 탭스톱 컨트롤이 실제 타겟
            var linked = FindLinkedControl(ctrlHwnd);
            if (linked != IntPtr.Zero)
            {
                Console.WriteLine($"  [WIN32-SC] Static label → linked 0x{linked:X8}");
                return ActivateFocusless(linked, parentHwnd);
            }
            return false;
        }

        if (IsComboBox(cls))
        {
            // WM_COMMAND EN_SETFOCUS 대신 parent에 CBN_SETFOCUS notify
            if (parentHwnd != IntPtr.Zero)
            {
                int ctrlId = NativeMethods.GetDlgCtrlID(ctrlHwnd);
                var wParam = (IntPtr)((1 << 16) | (ctrlId & 0xFFFF)); // CBN_SETFOCUS=1
                NativeMethods.PostMessageW(parentHwnd, WM_COMMAND, wParam, ctrlHwnd);
            }
            Console.WriteLine($"  [WIN32-SC] ComboBox focus notify → 0x{ctrlHwnd:X8}");
            return true;
        }

        Console.WriteLine($"  [WIN32-SC] Unknown class '{cls}' — skip");
        return false;
    }

    /// <summary>
    /// 부모 창에서 단축키 문자로 컨트롤 찾아 포커스리스 활성화.
    /// </summary>
    public static bool ActivateByShortcut(IntPtr parentHwnd, char shortcutChar)
    {
        var upper = char.ToUpperInvariant(shortcutChar);
        var map = BuildShortcutMap(parentHwnd);
        if (!map.TryGetValue(upper, out var ctrlHwnd))
        {
            Console.WriteLine($"  [WIN32-SC] Shortcut '{upper}' not found in 0x{parentHwnd:X8}");
            return false;
        }
        Console.WriteLine($"  [WIN32-SC] Shortcut '{upper}' → 0x{ctrlHwnd:X8}");
        return ActivateFocusless(ctrlHwnd, parentHwnd);
    }

    /// <summary>
    /// InputReadiness.EnsureInputPosition 콜백용.
    /// 타겟 컨트롤을 포커스리스로 활성화하고, Edit이면 캐럿 배치까지 보장.
    /// 사용: EnsureInputPosition = () => Win32ShortcutActivator.EnsureInputFocusless(targetHwnd)
    /// </summary>
    public static bool EnsureInputFocusless(IntPtr targetHwnd)
    {
        var parent = NativeMethods.GetParent(targetHwnd);
        return ActivateFocusless(targetHwnd, parent);
    }

    // ── Menu dispatch ─────────────────────────────────────────────

    /// <summary>
    /// 창의 메뉴바를 재귀 탐색하여 레이블 텍스트(& 제거) → 메뉴 아이템 ID 맵 빌드.
    /// 팝업 서브메뉴는 재귀 탐색, 실제 명령 아이템만 수집.
    /// WM_COMMAND(wParam=itemId) 발동용.
    /// 길이 내림차순 정렬.
    /// </summary>
    public static List<(string Label, uint ItemId)> BuildMenuTextMap(IntPtr hwnd)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var list = new List<(string Label, uint ItemId)>();
        var hMenu = NativeMethods.GetMenu(hwnd);
        if (hMenu != IntPtr.Zero)
            CollectMenuItems(hMenu, list, seen);
        list.Sort((a, b) => b.Label.Length.CompareTo(a.Label.Length));
        return list;
    }

    private static void CollectMenuItems(IntPtr hMenu, List<(string Label, uint ItemId)> list, HashSet<string> seen)
    {
        int count = NativeMethods.GetMenuItemCount(hMenu);
        for (int i = 0; i < count; i++)
        {
            var sub = NativeMethods.GetSubMenu(hMenu, i);
            if (sub != IntPtr.Zero)
            {
                // 팝업 서브메뉴 → 재귀
                CollectMenuItems(sub, list, seen);
                continue;
            }
            uint id = NativeMethods.GetMenuItemID(hMenu, i);
            if (id == 0 || id == 0xFFFFFFFF) continue; // separator / popup
            var sb = new StringBuilder(256);
            if (NativeMethods.GetMenuStringW(hMenu, (uint)i, sb, sb.Capacity, NativeMethods.MF_BYPOSITION) <= 0) continue;
            var label = ExtractMenuLabel(sb.ToString());
            if (!string.IsNullOrWhiteSpace(label) && seen.Add(label))
                list.Add((label, id));
        }
    }

    // ── Multi-language menu resource scan ────────────────────────

    /// <summary>
    /// 라이브 HMENU(현재 표시 언어)만 수집 — 빠름 (~1ms).
    /// ScanAndMerge 동기 단계에서 사용. 다국어 리소스는 BuildMenuResourceAllLangs로 별도 수집.
    /// </summary>
    public static List<(string Label, uint ItemId)> BuildMenuTextMapLive(IntPtr hwnd)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var list = new List<(string Label, uint ItemId)>();
        var hMenu = NativeMethods.GetMenu(hwnd);
        if (hMenu != IntPtr.Zero) CollectMenuItems(hMenu, list, seen);
        list.Sort((a, b) => b.Label.Length.CompareTo(a.Label.Length));
        return list;
    }

    /// <summary>
    /// 앱 모듈 RT_MENU 리소스(모든 LANGID) 파싱 — 느림 (LoadLibraryExW).
    /// ScanAndMerge 백그라운드 단계에서 사용.
    /// </summary>
    public static List<(string Label, uint ItemId)> BuildMenuResourceAllLangs(IntPtr hwnd)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var list = new List<(string Label, uint ItemId)>();

        var sb = new StringBuilder(512);
        if (NativeMethods.GetWindowModuleFileNameW(hwnd, sb, (uint)sb.Capacity) == 0) return list;
        var modulePath = sb.ToString();
        if (string.IsNullOrEmpty(modulePath)) return list;

        var hMod = NativeMethods.LoadLibraryExW(modulePath, IntPtr.Zero,
            NativeMethods.LOAD_LIBRARY_AS_DATAFILE | NativeMethods.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
        if (hMod == IntPtr.Zero) return list;

        try
        {
            var resNames = new List<IntPtr>();
            NativeMethods.EnumResourceNamesW(hMod, NativeMethods.RT_MENU,
                (_, _, name, _) => { resNames.Add(name); return true; }, IntPtr.Zero);

            foreach (var resName in resNames)
            {
                NativeMethods.EnumResourceLanguagesW(hMod, NativeMethods.RT_MENU, resName,
                    (h, type, name, langId, _) =>
                    {
                        var hRes = NativeMethods.FindResourceExW(h, type, name, langId);
                        if (hRes == IntPtr.Zero) return true;
                        var hData = NativeMethods.LoadResource(h, hRes);
                        if (hData == IntPtr.Zero) return true;
                        var ptr = NativeMethods.LockResource(hData);
                        if (ptr != IntPtr.Zero)
                            ParseMenuResource(ptr, list, seen);
                        return true;
                    }, IntPtr.Zero);
            }
        }
        finally { NativeMethods.FreeLibrary(hMod); }

        list.Sort((a, b) => b.Label.Length.CompareTo(a.Label.Length));
        return list;
    }

    /// <summary>
    /// 라이브 HMENU(현재 언어) + 앱 모듈 RT_MENU 리소스(모든 LANGID) 통합.
    /// 시스템에 설치된 모든 언어팩의 메뉴 문자열을 수집하여 언어 무관 매칭 지원.
    /// </summary>
    public static List<(string Label, uint ItemId)> BuildMenuTextMapAllLangs(IntPtr hwnd)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var list = new List<(string Label, uint ItemId)>();

        // 1. 라이브 HMENU — 현재 표시 언어
        var hMenu = NativeMethods.GetMenu(hwnd);
        if (hMenu != IntPtr.Zero) CollectMenuItems(hMenu, list, seen);

        // 2. 모듈 리소스 — 모든 LANGID의 RT_MENU 파싱
        var sb = new StringBuilder(512);
        if (NativeMethods.GetWindowModuleFileNameW(hwnd, sb, (uint)sb.Capacity) == 0) goto Done;
        var modulePath = sb.ToString();
        if (string.IsNullOrEmpty(modulePath)) goto Done;

        var hMod = NativeMethods.LoadLibraryExW(modulePath, IntPtr.Zero,
            NativeMethods.LOAD_LIBRARY_AS_DATAFILE | NativeMethods.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
        if (hMod == IntPtr.Zero) goto Done;

        try
        {
            var resNames = new List<IntPtr>();
            NativeMethods.EnumResourceNamesW(hMod, NativeMethods.RT_MENU,
                (_, _, name, _) => { resNames.Add(name); return true; }, IntPtr.Zero);

            foreach (var resName in resNames)
            {
                NativeMethods.EnumResourceLanguagesW(hMod, NativeMethods.RT_MENU, resName,
                    (h, type, name, langId, _) =>
                    {
                        var hRes = NativeMethods.FindResourceExW(h, type, name, langId);
                        if (hRes == IntPtr.Zero) return true;
                        var hData = NativeMethods.LoadResource(h, hRes);
                        if (hData == IntPtr.Zero) return true;
                        var ptr = NativeMethods.LockResource(hData);
                        if (ptr != IntPtr.Zero)
                            ParseMenuResource(ptr, list, seen);
                        return true;
                    }, IntPtr.Zero);
            }
        }
        finally { NativeMethods.FreeLibrary(hMod); }

        Done:
        list.Sort((a, b) => b.Label.Length.CompareTo(a.Label.Length));
        return list;
    }

    // RT_MENU 바이너리 파싱 — MENUTEMPLATE(v0) + MENUEX_TEMPLATE(v1) 지원
    private static void ParseMenuResource(IntPtr ptr, List<(string Label, uint ItemId)> list, HashSet<string> seen)
    {
        try
        {
            int pos = 0;
            ushort version = (ushort)Marshal.ReadInt16(ptr, pos); pos += 2;
            ushort cbOffset = (ushort)Marshal.ReadInt16(ptr, pos); pos += 2;
            if (version == 1) // MENUEX_TEMPLATE
            {
                pos += cbOffset; // dwHelpId 등 헤더 확장 스킵
                ParseMenuExItems(ptr, ref pos, list, seen);
            }
            else // MENUTEMPLATE (v0) — MFC/레거시
            {
                ParseMenuItems(ptr, ref pos, list, seen);
            }
        }
        catch { /* 파싱 오류는 무시 — 이미 라이브 HMENU로 수집됨 */ }
    }

    // MENUTEMPLATE 아이템 파싱
    private static void ParseMenuItems(IntPtr ptr, ref int pos, List<(string Label, uint ItemId)> list, HashSet<string> seen)
    {
        while (true)
        {
            ushort flags = (ushort)Marshal.ReadInt16(ptr, pos); pos += 2;
            bool isPopup = (flags & 0x0010) != 0;
            bool isEnd   = (flags & 0x0080) != 0;
            uint id = 0;
            if (!isPopup) { id = (uint)(ushort)Marshal.ReadInt16(ptr, pos); pos += 2; }
            var label = ReadMenuWChar(ptr, ref pos);
            if (!string.IsNullOrWhiteSpace(label))
            {
                var stripped = StripShortcutMarkers(label).Trim();
                bool isSep = (flags & 0x0800) != 0 || stripped == "-";
                if (!isSep && !isPopup && id != 0 && seen.Add(stripped))
                    list.Add((stripped, id));
            }
            if (isPopup) ParseMenuItems(ptr, ref pos, list, seen);
            if (isEnd) break;
        }
    }

    // MENUEX_TEMPLATE 아이템 파싱
    private static void ParseMenuExItems(IntPtr ptr, ref int pos, List<(string Label, uint ItemId)> list, HashSet<string> seen)
    {
        while (true)
        {
            uint dwType  = (uint)Marshal.ReadInt32(ptr, pos); pos += 4;
            /*state*/     pos += 4;
            uint uId     = (uint)Marshal.ReadInt32(ptr, pos); pos += 4;
            ushort wFlags = (ushort)Marshal.ReadInt16(ptr, pos); pos += 2;
            bool isPopup = (wFlags & 0x01) != 0;
            bool isEnd   = (wFlags & 0x80) != 0;
            var label = ReadMenuWChar(ptr, ref pos);
            pos = (pos + 3) & ~3; // DWORD 정렬
            if (!string.IsNullOrWhiteSpace(label))
            {
                var stripped = StripShortcutMarkers(label).Trim();
                bool isSep = (dwType & 0x00000800u) != 0; // MFT_SEPARATOR
                if (!isSep && !isPopup && uId != 0 && seen.Add(stripped))
                    list.Add((stripped, uId));
            }
            if (isPopup) { pos += 4; ParseMenuExItems(ptr, ref pos, list, seen); } // skip dwHelpId
            if (isEnd) break;
        }
    }

    private static string ReadMenuWChar(IntPtr ptr, ref int pos)
    {
        var chars = new System.Text.StringBuilder();
        while (true)
        {
            char c = (char)(ushort)Marshal.ReadInt16(ptr, pos); pos += 2;
            if (c == '\0') break;
            chars.Append(c);
        }
        return chars.ToString();
    }

    // ── Menu path resolution ──────────────────────────────────────

    /// <summary>
    /// 메뉴 경로로 아이템 ID 탐색. 각 세그먼트는 PatternMatcher grap 패턴 지원.
    /// path = ["파일", "모두저장"]  → 정확 경로
    /// path = ["파일", "*저장*"]    → 와일드카드 (glob)
    /// path = ["*", "regex:저장.*"] → 정규식
    /// 반환: 0 = 미발견
    /// </summary>
    public static uint ResolveMenuPath(IntPtr hwnd, string[] path)
    {
        if (path.Length == 0) return 0;
        var hMenu = NativeMethods.GetMenu(hwnd);
        if (hMenu == IntPtr.Zero) return 0;
        return WalkMenuPath(hMenu, path, 0);
    }

    private static uint WalkMenuPath(IntPtr hMenu, string[] path, int depth)
    {
        var target = path[depth].Trim();
        bool isLast = depth == path.Length - 1;
        var matcher = PatternMatcher.Create(target); // grap 패턴: Contains / glob / regex
        int count = NativeMethods.GetMenuItemCount(hMenu);
        var sb = new StringBuilder(256);
        for (int i = 0; i < count; i++)
        {
            sb.Clear();
            if (NativeMethods.GetMenuStringW(hMenu, (uint)i, sb, sb.Capacity, NativeMethods.MF_BYPOSITION) <= 0) continue;
            var label = ExtractMenuLabel(sb.ToString());
            if (!matcher.IsMatch(label)) continue;
            if (isLast)
            {
                uint id = NativeMethods.GetMenuItemID(hMenu, i);
                return (id == 0 || id == 0xFFFFFFFF) ? 0 : id;
            }
            else
            {
                var sub = NativeMethods.GetSubMenu(hMenu, i);
                if (sub == IntPtr.Zero) return 0;
                return WalkMenuPath(sub, path, depth + 1);
            }
        }
        return 0;
    }

    private static string ExtractMenuLabel(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return string.Empty;
        // 탭 구분 뒤 단축키 힌트 제거: "저장(&S)\tCtrl+S" → "저장(&S)"
        var tab = raw.IndexOf('\t');
        if (tab >= 0) raw = raw[..tab];
        return StripShortcutMarkers(raw).Trim();
    }

    /// <summary>
    /// 메뉴 아이템 ID로 WM_COMMAND 포커스리스 발동.
    /// </summary>
    public static bool DispatchMenuItem(IntPtr hwnd, uint itemId)
    {
        NativeMethods.PostMessageW(hwnd, WM_COMMAND, (IntPtr)itemId, IntPtr.Zero);
        Console.WriteLine($"  [WIN32-MENU] WM_COMMAND itemId=0x{itemId:X4} → 0x{hwnd:X8}");
        return true;
    }

    // ── Shortcut PostMessage dispatch (focusless) ─────────────────

    /// <summary>
    /// "Alt+S", "Ctrl+Shift+F5", "s" 등 단축키 문자열을 파싱하여
    /// WM_KEYDOWN / WM_SYSKEYDOWN PostMessage로 포커스리스 디스패치.
    /// </summary>
    public static bool DispatchShortcutViaPostMessage(IntPtr hwnd, string shortcut)
    {
        if (string.IsNullOrWhiteSpace(shortcut)) return false;
        var parts = shortcut.Split('+');
        var keyStr = parts[^1].Trim();
        bool hasAlt   = parts.Any(p => p.Trim().Equals("Alt",     StringComparison.OrdinalIgnoreCase));
        bool hasCtrl  = parts.Any(p => p.Trim().Equals("Ctrl",    StringComparison.OrdinalIgnoreCase)
                                    || p.Trim().Equals("Control", StringComparison.OrdinalIgnoreCase));
        bool hasShift = parts.Any(p => p.Trim().Equals("Shift",   StringComparison.OrdinalIgnoreCase));

        uint vk = ParseVirtualKey(keyStr);
        if (vk == 0) { Console.WriteLine($"  [WIN32-KEY] Unknown key '{keyStr}'"); return false; }

        uint scan   = NativeMethods.MapVirtualKeyW(vk, 0); // VK→scan code
        uint dnLp   = 0x00000001u | (scan << 16);          // repeat=1, scan code
        if (hasAlt) dnLp |= 1u << 29;                      // context bit = Alt held
        uint upLp   = dnLp | (1u << 30) | (1u << 31);     // prev-down + transition

        uint dnMsg  = hasAlt ? NativeMethods.WM_SYSKEYDOWN : NativeMethods.WM_KEYDOWN;
        uint upMsg  = hasAlt ? NativeMethods.WM_SYSKEYUP   : NativeMethods.WM_KEYUP;

        if (hasCtrl)  NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYDOWN,    (IntPtr)NativeMethods.VK_CONTROL, (IntPtr)0x001D0001u);
        if (hasShift) NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYDOWN,    (IntPtr)NativeMethods.VK_SHIFT,   (IntPtr)0x002A0001u);
        NativeMethods.PostMessageW(hwnd, dnMsg, (IntPtr)vk, (IntPtr)dnLp);
        NativeMethods.PostMessageW(hwnd, upMsg, (IntPtr)vk, (IntPtr)upLp);
        if (hasShift) NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYUP,      (IntPtr)NativeMethods.VK_SHIFT,   unchecked((IntPtr)0xC02A0001u));
        if (hasCtrl)  NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYUP,      (IntPtr)NativeMethods.VK_CONTROL, unchecked((IntPtr)0xC01D0001u));

        Console.WriteLine($"  [WIN32-KEY] '{shortcut}' → vk=0x{vk:X2} scan={scan} mods=({(hasCtrl?"Ctrl ":"")}{(hasAlt?"Alt ":"")}{(hasShift?"Shift":"")})");
        return true;
    }

    private static uint ParseVirtualKey(string key) => key.ToUpperInvariant() switch
    {
        var k when k.Length == 1 && char.IsAsciiLetterOrDigit(k[0]) => (uint)char.ToUpperInvariant(k[0]),
        "ENTER" or "RETURN" => 0x0D,
        "ESCAPE" or "ESC"   => 0x1B,
        "TAB"               => 0x09,
        "SPACE"             => 0x20,
        "BACK" or "BACKSPACE" => 0x08,
        "DELETE" or "DEL"   => 0x2E,
        "INSERT" or "INS"   => 0x2D,
        "HOME"              => 0x24,
        "END"               => 0x23,
        "PAGEUP"  or "PGUP" => 0x21,
        "PAGEDOWN" or "PGDN"=> 0x22,
        "LEFT"  => 0x25, "UP"    => 0x26,
        "RIGHT" => 0x27, "DOWN"  => 0x28,
        "F1"  => 0x70, "F2"  => 0x71, "F3"  => 0x72, "F4"  => 0x73,
        "F5"  => 0x74, "F6"  => 0x75, "F7"  => 0x76, "F8"  => 0x77,
        "F9"  => 0x78, "F10" => 0x79, "F11" => 0x7A, "F12" => 0x7B,
        _ => 0
    };

    // ── Helpers ───────────────────────────────────────────────────

    private static string GetWindowText(IntPtr hwnd)
    {
        var sb = new StringBuilder(128);
        NativeMethods.GetWindowTextW(hwnd, sb, sb.Capacity);
        return sb.ToString();
    }
}
