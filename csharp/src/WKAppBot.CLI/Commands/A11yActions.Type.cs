using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ConPTY terminals — WM_CHAR doesn't reach stdin, need clipboard paste or SendInput
    static readonly HashSet<string> ConPtyTerminalClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "CASCADIA_HOSTING_WINDOW_CLASS", // Windows Terminal (ConPTY)
        "PseudoConsoleWindow",           // ConPTY
        "VirtualConsoleClass",           // misc
    };

    // Classic console — WM_CHAR reaches stdin directly (focusless telepathy!)
    static readonly HashSet<string> ClassicConsoleClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "ConsoleWindowClass",            // Classic conhost.exe
    };

    // -- Type: UIA Value -> WM_CHAR -> LegacyIA SetValue --
    static bool A11yType(AutomationElement el, IntPtr hwnd, string text, bool hotkey = false)
    {
        // Tier 0: 포커스리스 컨트롤/메뉴 발동 (--hotkey 옵션 시에만 활성화)
        // - [파일/모두저장] 명시 경로 문법: 체이닝 가능, 잔여 text도 동일 규칙 재시도
        // - 레이블 매칭: text 전체가 컨트롤/메뉴 레이블과 정확히 일치할 때만 발동 (노이즈 방지)
        //   "저장" → "&저장" 버튼 발동 OK / "저장하기" → 매칭 없음, 이후 tiers 처리
        if (hotkey)
        {
            var elHwnd = GetElementHwnd(el);
            var parentHwnd = elHwnd != IntPtr.Zero
                ? NativeMethods.GetParent(elHwnd)
                : NativeMethods.GetParent(hwnd);
            if (parentHwnd != IntPtr.Zero)
            {
                // 프로세스명 + EXE 버전 추출 (경험 DB 키)
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                var procName = "";
                try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
                var exeVersion = HotkeyExperienceDb.GetExeVersion(pid); // null = access denied (elevated)

                // 경험 DB 조회 → Verify → 스태일이면 재스캔 후 재조회
                if (!string.IsNullOrEmpty(procName))
                {
                    var lookupPattern = text.Contains('/') ? text.Split('/')[^1] : text;

                    // 첫 접근 시 풀스캔 → DB 누적 (버전 불일치 시 Load가 자동 캐시 무효화)
                    if (!HotkeyExperienceDb.IsSessionScanned(procName))
                        A11yHotkeyScanner.ScanAndMerge(hwnd, el, procName, exeVersion);

                    var dbEntry = HotkeyExperienceDb.Match(procName, lookupPattern, exeVersion);
                    if (dbEntry != null)
                    {
                        // 발동 전 라이브 검증 (스태일 항목은 DB에서 제거)
                        if (!A11yHotkeyScanner.Verify(dbEntry, hwnd, parentHwnd, procName, exeVersion))
                        {
                            // 검증 실패 → 재스캔 후 재조회 (앱 업데이트 등)
                            Console.WriteLine($"[A11Y] type --hotkey — stale, rescanning '{procName}'...");
                            HotkeyExperienceDb.MarkSessionScanned(procName); // 무한루프 방지
                            A11yHotkeyScanner.ScanAndMerge(hwnd, el, procName, exeVersion);
                            dbEntry = HotkeyExperienceDb.Match(procName, lookupPattern, exeVersion);
                        }
                    }

                    if (dbEntry != null)
                    {
                        Console.WriteLine($"[A11Y] type --hotkey — DB '{dbEntry.Label}' ({dbEntry.Source}/{dbEntry.Method})");
                        return A11yHotkeyScanner.Dispatch(dbEntry, hwnd, parentHwnd);
                    }
                    Console.WriteLine($"[A11Y] type --hotkey — DB miss '{text}', falling to live scan");
                }

                List<(string Label, Func<bool> Activate)>? combinedMap = null;
                List<(string Label, Func<bool> Activate)> GetCombinedMap()
                {
                    var list = new List<(string Label, Func<bool> Activate)>();
                    foreach (var (label, ctrlHwnd) in Win32ShortcutActivator.BuildTextMap(parentHwnd))
                        list.Add((label, () => Win32ShortcutActivator.ActivateFocusless(ctrlHwnd, parentHwnd)));
                    foreach (var (label, itemId) in Win32ShortcutActivator.BuildMenuTextMapAllLangs(hwnd))
                        list.Add((label, () => Win32ShortcutActivator.DispatchMenuItem(hwnd, itemId)));
                    return list;
                }

                // ① 슬래시 포함 → 메뉴 경로 탐색 (경로 세그먼트별 grap 패턴 지원)
                // "파일/저장"  → 정확 경로  /  "파일/*저장*" → 와일드카드 세그먼트
                if (text.Contains('/'))
                {
                    var segments = text.Split('/');
                    var itemId = Win32ShortcutActivator.ResolveMenuPath(hwnd, segments);
                    if (itemId != 0)
                    {
                        Console.WriteLine($"[A11Y] type --hotkey — menu path '{text}' → WM_COMMAND 0x{itemId:X4}");
                        Win32ShortcutActivator.DispatchMenuItem(hwnd, itemId);
                        return true;
                    }
                    Console.Error.WriteLine($"[A11Y] type --hotkey — menu path '{text}' not found, aborting");
                    return false;
                }

                // ② grap 패턴으로 레이블 매칭 — 커버리지 최고 우선 선택
                // 커버리지 = 패턴길이 / 레이블길이 → 짧은 레이블이 더 구체적 매칭
                // "저장" 패턴: "저장"(100%) > "다른이름으로저장"(25%) → "저장" 선택
                {
                    combinedMap ??= GetCombinedMap();
                    var matcher = PatternMatcher.Create(text);
                    // 전체 매칭 수집 → 레이블 길이 오름차순 (커버리지 내림차순)
                    var matches = combinedMap
                        .Where(e => matcher.IsMatch(e.Label))
                        .OrderBy(e => e.Label.Length)
                        .ToList();
                    if (matches.Count == 0)
                    {
                        // CDP 없는 Electron/WPF 폴백: UIA AccessKey / AcceleratorKey 스캔
                        Console.WriteLine($"[A11Y] type --hotkey — no Win32 label match, trying UIA AccessKey scan");
                        var uiaMatcher = PatternMatcher.Create(text);
                        try
                        {
                            foreach (var desc in el.FindAllDescendants())
                            {
                                var name = desc.Properties.Name.ValueOrDefault ?? "";
                                if (!uiaMatcher.IsMatch(name)) continue;
                                var accessKey = desc.Properties.AccessKey.ValueOrDefault ?? "";
                                var accelKey  = desc.Properties.AcceleratorKey.ValueOrDefault ?? "";
                                var shortcut  = !string.IsNullOrEmpty(accessKey) ? accessKey
                                              : !string.IsNullOrEmpty(accelKey)  ? accelKey : null;
                                if (shortcut == null) continue;
                                Console.WriteLine($"[A11Y] type --hotkey — UIA '{name}' → '{shortcut}' → PostMessage");
                                return Win32ShortcutActivator.DispatchShortcutViaPostMessage(hwnd, shortcut);
                            }
                        }
                        catch { }
                        Console.Error.WriteLine($"[A11Y] type --hotkey — no label match for '{text}', aborting");
                        return false;
                    }
                    var (bestLabel, bestActivate) = matches[0];
                    if (matches.Count > 1)
                        Console.WriteLine($"[A11Y] type --hotkey — {matches.Count} matches, best coverage: '{bestLabel}' ({(double)text.Length / bestLabel.Length:P0})");
                    else
                        Console.WriteLine($"[A11Y] type --hotkey — matched '{bestLabel}'");
                    if (!bestActivate())
                    {
                        Console.Error.WriteLine("[A11Y] type --hotkey — dispatch failed, aborting");
                        return false;
                    }
                    return true;
                }
            }
            return false; // parentHwnd 없음
        }

        // Check terminal type for input strategy
        var winClass = WindowFinder.GetClassName(hwnd);
        bool isConPtyTerminal = ConPtyTerminalClasses.Contains(winClass);
        bool isClassicConsole = ClassicConsoleClasses.Contains(winClass);

        // Classic console: WM_CHAR reaches stdin directly — focusless telepathy!
        if (isClassicConsole)
        {
            foreach (char c in text)
                NativeMethods.PostMessageW(hwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
            Console.WriteLine($"[A11Y] type — console telepathy WM_CHAR ({text.Length} chars, focusless!)");
            return true;
        }

        // ConPTY terminal: clipboard paste (focusless — no SendInput needed)
        if (isConPtyTerminal)
        {
            try
            {
                var prevClip = ""; try { prevClip = System.Windows.Forms.Clipboard.GetText(); } catch { }
                System.Windows.Forms.Clipboard.SetText(text);
                // Ctrl+V via PostMessage (no focus steal)
                uint sc = NativeMethods.MapVirtualKeyW(0x56 /*V*/, 0);
                NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYDOWN, (IntPtr)0x11, IntPtr.Zero); // Ctrl down
                Thread.Sleep(10);
                NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYDOWN, (IntPtr)0x56, (IntPtr)(1u | (sc << 16))); // V down
                Thread.Sleep(10);
                NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYUP, (IntPtr)0x56, (IntPtr)(1u | (sc << 16) | (1u << 30) | (1u << 31)));
                NativeMethods.PostMessageW(hwnd, NativeMethods.WM_KEYUP, (IntPtr)0x11, (IntPtr)((1u << 30) | (1u << 31)));
                Thread.Sleep(50);
                try { if (!string.IsNullOrEmpty(prevClip)) System.Windows.Forms.Clipboard.SetText(prevClip); } catch { }
                Console.WriteLine($"[A11Y] type — ConPTY clipboard paste ({text.Length} chars, focusless!)");
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[A11Y] type — ConPTY clipboard paste failed: {ex.Message}, falling back to SendInput");
            }
        }

        if (!isConPtyTerminal)
        {
            // Tier 1: UIA Value.SetValue (focusless, replaces all — works for standard Edit)
            try
            {
                var vp = el.Patterns.Value;
                if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
                {
                    vp.Pattern.SetValue(text);
                    Console.WriteLine($"[A11Y] type — UIA Value.SetValue ({text.Length} chars)");
                    return true;
                }
            }
            catch { }

            var elHwnd = GetElementHwnd(el);
            bool isElectron = IsElectronWindow(hwnd);

            // Tier 2: LegacyIA SetValue — Electron 우선 (WM_CHAR은 Chromium renderer가 무시)
            // 일반 Win32는 WM_CHAR 후 LegacyIA로 넘어오지 않으므로 순서 문제 없음
            if (isElectron || elHwnd == IntPtr.Zero)
            {
                try
                {
                    var legacy = el.Patterns.LegacyIAccessible;
                    if (legacy.IsSupported)
                    {
                        legacy.Pattern.SetValue(text);
                        Console.WriteLine($"[A11Y] type — LegacyIA SetValue ({text.Length} chars){(isElectron ? " [Electron]" : "")}");
                        return true;
                    }
                }
                catch { }
            }

            // Tier 3: Win32 WM_CHAR — 네이티브 컨트롤 전용 (Electron 제외)
            // TranslateMessage가 WM_CHAR 자동생성하는 MFC 컨트롤(CMaskEditEx 등)에서 유일한 방법
            // Electron은 Tier 2(LegacyIA)로 처리됐으므로 여기 도달하면 네이티브 Win32
            if (!isElectron && elHwnd != IntPtr.Zero)
            {
                foreach (char c in text)
                    NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
                Console.WriteLine($"[A11Y] type — Win32 WM_CHAR ({text.Length} chars)");
                return true;
            }
        }
        // ConPTY clipboard paste failed — fall through to SendInput

        // Tier 4: SendKeys keystroke fallback (requires focus)
        try
        {
            Console.WriteLine("[A11Y] type — focusless failed, falling back to SendKeys (focus required)");
            NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
            Thread.Sleep(100);
            // Pass hwnd for per-token mid-input focus check+restore
            WKAppBot.Win32.Input.KeyboardInput.SendKeys(text, hwnd);
            Console.WriteLine($"[A11Y] type — SendKeys keystroke ({text.Length} chars)");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] type — SendKeys failed: {ex.Message}");
        }

        Console.Error.WriteLine("[A11Y] type — no input method available");
        return false;
    }

    // -- SetValue: UIA Value -> WM_SETTEXT -> LegacyIA --
    static bool A11ySetValue(AutomationElement el, IntPtr hwnd, string text)
    {
        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — UIA Value.SetValue");
                return true;
            }
        }
        catch { }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(elHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
            Console.WriteLine("[A11Y] set-value — Win32 WM_SETTEXT");
            return true;
        }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — LegacyIA SetValue");
                return true;
            }
        }
        catch { }

        Console.Error.WriteLine("[A11Y] set-value — no method available");
        return false;
    }

    // -- SetRange: UIA RangeValue --
    static bool A11ySetRange(AutomationElement el, double value)
    {
        var info = UiaLocator.GetRangeValueInfo(el);
        if (info != null)
            Console.WriteLine($"[A11Y] range: {info.Minimum}..{info.Maximum} (current={info.Value}, step={info.SmallChange})");

        if (UiaLocator.TrySetRangeValue(el, value))
        {
            Console.WriteLine($"[A11Y] set-range — UIA RangeValue = {value}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] set-range — RangeValue not supported on this element");
        return false;
    }
}
