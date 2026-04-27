using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public sealed partial class InputReadiness
{
    // -- DetectBlocker: 방해꾼 빠른 감지 (~5ms) --------------------

    public BlockerInfo? DetectBlocker(IntPtr mainHwnd)
    {
        IntPtr blockerHwnd = IntPtr.Zero;

        NativeMethods.GetWindowThreadProcessId(mainHwnd, out uint targetPid);
        uint myPid = (uint)Environment.ProcessId;

        // Strategy 1: 전경 윈도우 체크 -- 같은 프로세스의 팝업/다이얼로그만 blocker
        // 같은 클래스의 최상위 윈도우(예: VS Code 다중창)는 blocker가 아님!
        var fg = NativeMethods.GetForegroundWindow();
        if (fg != mainHwnd && fg != IntPtr.Zero)
        {
            NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
            if (fgPid != myPid && fgPid == targetPid)
            {
                var fgClsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(fg, fgClsSb, 256);
                var mainClsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(mainHwnd, mainClsSb, 256);
                var fgCls = fgClsSb.ToString();
                var mainCls = mainClsSb.ToString();

                // 같은 최상위 클래스(Chrome_WidgetWin_1 등) -> 형제 윈도우, blocker 아님
                bool isSiblingWindow = fgCls == mainCls
                    && NativeMethods.GetAncestor(fg, NativeMethods.GA_ROOT) == fg;
                // mainHwnd의 자손(child/grandchild) -> 편집 영역 등, blocker 아님
                bool isDescendant = NativeMethods.GetAncestor(fg, NativeMethods.GA_ROOT) == mainHwnd
                    || NativeMethods.IsChild(mainHwnd, fg);
                // fg가 mainHwnd의 조상(parent/grandparent) -> blocker 아님 (child window 클릭시 parent가 fg인 경우)
                bool isAncestor = NativeMethods.IsChild(fg, mainHwnd);
                if (!isSiblingWindow && !isDescendant && !isAncestor)
                    blockerHwnd = fg;
            }
        }

        // Strategy 2: EnumWindows -- 같은 프로세스의 팝업/다이얼로그 탐색
        if (blockerHwnd == IntPtr.Zero)
        {
            var candidates = new List<IntPtr>();
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (hWnd == mainHwnd) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != targetPid) return true;

                var clsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(hWnd, clsSb, 256);
                var cls = clsSb.ToString();

                NativeMethods.GetWindowRect(hWnd, out var r);
                bool isDialog = cls == "#32770";
                bool isPopup = r.Width > 50 && r.Height > 30 && r.Width < 800 && r.Height < 600;

                // Empty-title non-dialog windows are tool/child windows, not real popup blockers
                var titleSb2 = new StringBuilder(256);
                NativeMethods.GetWindowTextW(hWnd, titleSb2, 256);
                bool hasTitle = titleSb2.Length > 0;

                if (isDialog || (isPopup && hasTitle))
                    candidates.Add(hWnd);

                return true;
            }, IntPtr.Zero);

            if (candidates.Count > 0)
                blockerHwnd = candidates[0];
        }

        if (blockerHwnd == IntPtr.Zero)
            return null;

        // 방해꾼 정보 수집
        var titleSb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(blockerHwnd, titleSb, 256);

        var classSb2 = new StringBuilder(256);
        NativeMethods.GetClassNameW(blockerHwnd, classSb2, 256);

        var classPath = BuildClassPath(blockerHwnd);

        NativeMethods.GetWindowThreadProcessId(blockerHwnd, out uint blockerPid);
        string procName = "";
        try { procName = Process.GetProcessById((int)blockerPid).ProcessName; } catch { }

        // Static 컨트롤에서 메시지 텍스트 읽기 (간단 버전)
        string? messageText = ReadStaticText(blockerHwnd);

        return new BlockerInfo(
            Handle: blockerHwnd,
            ClassName: classSb2.ToString(),
            ClassPath: classPath,
            Title: titleSb.ToString(),
            MessageText: messageText,
            ProcessId: blockerPid,
            ProcessName: procName
        );
    }

    // -- TryDismissBlocker: IBlockerHandler 위임 ------------------

    public (bool handled, bool shouldRetry) TryDismissBlocker(IntPtr mainHwnd, BlockerInfo blocker)
    {
        if (BlockerHandler == null)
            return (false, false);
        return BlockerHandler.TryHandle(mainHwnd, blocker);
    }

    // -- Private: BlockerInfo 빌드 --------------------------------

    private BlockerInfo? BuildBlockerInfo(IntPtr blockerHwnd)
    {
        if (blockerHwnd == IntPtr.Zero || !NativeMethods.IsWindow(blockerHwnd))
            return null;

        var titleSb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(blockerHwnd, titleSb, 256);

        var classSb = new StringBuilder(256);
        NativeMethods.GetClassNameW(blockerHwnd, classSb, 256);

        var classPath = BuildClassPath(blockerHwnd);

        NativeMethods.GetWindowThreadProcessId(blockerHwnd, out uint pid);
        string procName = "";
        try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }

        string? messageText = ReadStaticText(blockerHwnd);

        return new BlockerInfo(
            Handle: blockerHwnd,
            ClassName: classSb.ToString(),
            ClassPath: classPath,
            Title: titleSb.ToString(),
            MessageText: messageText,
            ProcessId: pid,
            ProcessName: procName
        );
    }

    // -- Private: Static 텍스트 읽기 (간단 버전) ----------------─

    private static string? ReadStaticText(IntPtr hDialog)
    {
        var texts = new List<string>();
        NativeMethods.EnumChildWindows(hDialog, (hChild, _) =>
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(hChild, sb, 256);
            if (sb.ToString() == "Static")
            {
                var textSb = new StringBuilder(512);
                NativeMethods.GetWindowTextW(hChild, textSb, 512);
                var t = textSb.ToString();
                if (!string.IsNullOrWhiteSpace(t))
                    texts.Add(t);
            }
            return true;
        }, IntPtr.Zero);

        return texts.Count > 0 ? string.Join(" | ", texts) : null;
    }
}
