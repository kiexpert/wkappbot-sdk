using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static bool A11yFind(AutomationElement root, IntPtr hwnd, int depth, bool printFocus = true, string[]? extraArgs = null,
        WKAppBot.Win32.Window.WindowInfo? originalHit = null, string? multiHeader = null, int totalCount = 1, bool isBest = false)
    {
        // Process-only result (windowless background process): show pid/proc/cmdline, skip hwnd logic.
        if (originalHit?.IsProcessOnly == true)
        {
            if (!string.IsNullOrEmpty(multiHeader)) Console.WriteLine(Ansi.Dim(multiHeader));
            var mark = totalCount > 1 ? (isBest ? $"BEST:{totalCount}" : $"AMB:{totalCount}") : "PROC";
            var cmdLine = originalHit.Title;
            var shortCmd = cmdLine.Length > 120 ? cmdLine[..120] + "…" : cmdLine;
            Console.WriteLine($"## {originalHit.ClassName} (pid={originalHit.ProcessId})");
            Console.WriteLine("```");
            Console.WriteLine($"  pid:{originalHit.ProcessId}  proc:{originalHit.ClassName}");
            Console.WriteLine($"  cmd:{shortCmd}");
            Console.WriteLine($"  [{mark}]");
            Console.WriteLine("```");
            Console.WriteLine();
            return true;
        }

        // -- SINGLE STA thread for ALL UIA work --
        // Using separate RunWithUiaTimeout() per call creates multiple STA threads that COM
        // serializes, causing 5×7s=35s delay. One batch thread runs all UIA sequentially.
        FocusedElementInfo? focInfo = null;
        string fullGrap = BuildCompactWinGrap(hwnd); // fallback: no #scope
        IntPtr focRootHwndBatch = IntPtr.Zero;
        string focGrapBatch = "";
        string leafTagBatch = "";

        var gti2 = new NativeMethods.GUITHREADINFO
            { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
        NativeMethods.GetGUIThreadInfo(0, ref gti2);
        var focRootHwndRaw = gti2.hwndFocus != IntPtr.Zero
            ? NativeMethods.GetAncestor(gti2.hwndFocus, NativeMethods.GA_ROOT)
            : IntPtr.Zero;
        if (focRootHwndRaw == IntPtr.Zero) focRootHwndRaw = gti2.hwndFocus;
        var hwndCapture = hwnd;
        var focRootCapture = focRootHwndRaw;

        // Skip deep UIA traversal for Electron/browser windows (VS Code, Claude, Codex etc.)
        // whose UIA trees are too deep to traverse within a useful timeout.
        NativeMethods.GetWindowThreadProcessId(hwndCapture, out var _capPid);
        var _capProc = WKAppBot.Win32.Window.WindowFinder.GetProcessNameCached2(_capPid);
        var _capCls  = WKAppBot.Win32.Window.WindowFinder.GetClassName(hwndCapture);
        bool skipUia = WKAppBot.Win32.Window.WindowFinder.IsElectronWindow(_capCls, _capProc)
            // MFC/Afx windows (nkrunlite, Heroes4 etc.) have minimal UIA trees;
            // batch always times out and abandoned STA threads delay exit.
            || _capCls.StartsWith("Afx:", StringComparison.OrdinalIgnoreCase)
            || _capCls.StartsWith("_NKHero", StringComparison.OrdinalIgnoreCase)
            || _capCls.Equals("#32770", StringComparison.OrdinalIgnoreCase); // Win32 dialog

        if (!skipUia)
        {
            RunAllUiaInOneBatch(() =>
            {
                // 1. focused element info (for FOCUS block)
                try { using var loc = new UiaLocator(); focInfo = loc.GetFocusedElementInfo(); } catch { }
                // 2. target grap with UIA scope (for TARGET #scope)
                fullGrap = AppendFocusPathCore_Public(BuildCompactWinGrap(hwndCapture), hwndCapture);
                // 3. focus root grap with UIA scope (for FOCUS block)
                var fr = focRootCapture != IntPtr.Zero ? focRootCapture : hwndCapture;
                focRootHwndBatch = focRootCapture;
                focGrapBatch = AppendFocusPathCore_Public(BuildCompactWinGrap(fr), fr);
                // 4. leaf tag (for TARGET block)
                try
                {
                    using var uia = new FlaUI.UIA3.UIA3Automation();
                    var rt = uia.FromHandle(hwndCapture);
                    if (rt != null)
                    {
                        var focused2 = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, rt, hwndCapture);
                        if (focused2 != null) leafTagBatch = FormatLeafTag(focused2);
                    }
                }
                catch { }
            }, timeoutMs: 2000);
        }
        else
        {
            focRootHwndBatch = focRootCapture;
            fullGrap = BuildCompactWinGrap(hwndCapture);
        }

        var compactGrap = BuildCompactWinGrap(hwnd);

        // -- FOCUS section --------------------------------------
        if (printFocus && focInfo != null)
        {
            var focGrap = !string.IsNullOrEmpty(focGrapBatch) ? focGrapBatch
                : BuildTargetGrap(focRootHwndBatch != IntPtr.Zero ? focRootHwndBatch : hwnd);
            var focRootHwnd = focRootHwndBatch != IntPtr.Zero ? focRootHwndBatch : hwnd;
            uint focPid = 0;
            string focProc = "";
            if (focRootHwnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(focRootHwnd, out focPid);
                try { using var fp = System.Diagnostics.Process.GetProcessById((int)focPid); focProc = fp.ProcessName; } catch { }
            }
            var focScope = focGrap.Contains('#') ? focGrap[focGrap.IndexOf('#')..] : "";
            var focTitle = focRootHwnd != IntPtr.Zero ? NativeMethods.GetWindowTextW(focRootHwnd) : "";
            var focCompact = BuildCompactWinGrap(focRootHwnd);
            var focGrapJson = BuildFindGrap(focRootHwnd, focPid, focProc, focCompact, null);
            var focPaste = QuoteGrapExpression($"{focGrapJson}{focScope}");
            var focSw = System.Diagnostics.Stopwatch.StartNew();
            string focVerify = "?";
            try
            {
                var focHits = WindowFinder.FindWindows(focCompact, false);
                focVerify = focHits.Any(v => v.Handle == focRootHwnd) ? "OK" : "MISS";
            }
            catch { }
            focSw.Stop();
            PrintFocusBlock(focPaste, focVerify, focSw.ElapsedMilliseconds);
            if (!string.IsNullOrWhiteSpace(focTitle))
                Console.Error.WriteLine(focTitle);
        }

        // Multi-match header: printed once before first TARGET (after FOCUS if present)
        if (!string.IsNullOrEmpty(multiHeader))
            Console.WriteLine(Ansi.Dim(multiHeader));

        // -- TARGET section ------------------------------------─
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string verifyMark = "?";
        var verifyHits = new List<WKAppBot.Win32.Window.WindowInfo>();
        // Skip verify for trivial grap -- WPF overlays (FocuslessWarning etc.) have no
        // meaningful identity fields, so FindWindows("{}") always returns MISS (false negative).
        bool trivialGrap = compactGrap is "{}" or "{ }";
        try
        {
            if (!trivialGrap)
            {
                verifyHits = WindowFinder.FindWindows(compactGrap, false);
                verifyMark = verifyHits.Any(v => v.Handle == hwnd)
                    ? (totalCount > 1 ? (isBest ? $"BEST:{totalCount}" : $"AMB:{totalCount}") : "OK")
                    : "MISS";
            }
        }
        catch { }
        sw.Stop();

        NativeMethods.GetWindowThreadProcessId(hwnd, out uint resolvedPid);
        string procName = "";
        try { using var proc = System.Diagnostics.Process.GetProcessById((int)resolvedPid); procName = proc.ProcessName; } catch { }

        var primaryHit = verifyHits.FirstOrDefault(v => v.Handle == hwnd);
        var scope = fullGrap.Contains('#') ? fullGrap[fullGrap.IndexOf('#')..] : "";
        var title = NativeMethods.GetWindowTextW(hwnd);
        if (title.Length > 90) title = title[..87] + "...";

        // Prefer originalHit for matched-field injection: verify re-searches by compact grap,
        // losing the original matched field (e.g. cmdLine:'chatgpt.com' for a VS Code window).
        var hitForGrap = originalHit ?? primaryHit;
        var grapJson = BuildFindGrap(hwnd, resolvedPid, procName, compactGrap, hitForGrap);
        var paste = QuoteGrapExpression($"{grapJson}{scope}");

        var titleHeading = !string.IsNullOrWhiteSpace(title) ? title : "TARGET";
        var matchNote = originalHit?.MatchedVia switch
        {
            null or "" or "context" or "proc" or "domain" or "url" or "file" or "cls" or "title" => "",
            "uia"       => $"  <- uia: {originalHit.MatchedSnippet}",
            "child-cmd" => "",  // proc/cmd already in grap -- no extra annotation needed
            _ => $"  <- {originalHit.MatchedVia}: {originalHit.MatchedSnippet}"
        } ?? "";

        var scoreStr = FormatFieldScores(hitForGrap);
        PrintTargetBlock(titleHeading, paste, "find", extraArgs, verifyMark, sw.ElapsedMilliseconds, matchNote, leafTagBatch, scoreStr);

        return true;
    }
}
