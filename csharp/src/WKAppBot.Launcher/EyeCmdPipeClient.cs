using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Launcher;

[JsonSerializable(typeof(string[]))]
internal partial class LauncherJsonContext : JsonSerializerContext { }

/// <summary>Connects to Eye's command pipe and delegates execution (zero cold-start).</summary>
internal static class EyeCmdPipeClient
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";

    /// <summary>
    /// Try to delegate args to the running Eye process.
    /// Returns true + sets exitCode if delegation succeeded.
    /// Returns false only if Eye is not running/busy (caller should fall through to RunCore).
    ///
    /// timeoutMs: if >0, close the pipe after this many ms (enforces Launcher-level timeout
    /// even for Eye in-process commands that don't implement their own timeout).
    /// firstOutputTimeoutMs: if >0, fall back to Core if Eye produces no output within this time.
    ///   Use for fast commands (file edit/read/grep) where Eye stall -> Core is safer than waiting.
    ///   Returns false so caller runs Core; pipe is closed (Eye gets BrokenPipe and skips command).
    /// </summary>
    public static bool TryDelegate(string[] args, out int exitCode, int timeoutMs = 0, int timeoutExitCode = 2, int firstOutputTimeoutMs = 0)
    {
        exitCode = 0;
        bool connected = false;
        var pipe = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
        // Eye pipe sends UTF-8 text. We decode it to Unicode strings via StreamReader,
        // then re-encode to terminal CP via WideCharToMultiByte (NativeAOT-safe, no managed code pages).
        // Encoding.GetEncoding(949) is NOT used -- NativeAOT trims code page support; silent UTF-8
        // fallback caused garbled Korean in CMD. Win32 WideCharToMultiByte has no such limitation.
        var rawStdout = Console.OpenStandardOutput();
        var cp = Program._consoleCodePage;
        var lineEnd = "\r\n"u8.ToArray(); // Windows line ending as bytes
        Action<string> writeLine = cp > 0 && cp != 65001
            ? line => WriteLineW(rawStdout, line, cp, lineEnd)
            : line => { var b = Encoding.UTF8.GetBytes(line + "\r\n"); rawStdout.Write(b, 0, b.Length); rawStdout.Flush(); };
        try
        {
            pipe.Connect(300); // 300ms: Eye either answers instantly or isn't running
            connected = true;

            var w = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };
            var r = new StreamReader(pipe, leaveOpen: true);

            // Prepend CWD + caller-terminal HWND hint so Eye can resolve caller window directly.
            // ResolveCallerTerminalHwnd() walks the ancestor process chain and returns the nearest
            // ancestor with a visible non-minimized window. Foreground/focus is ignored -- the IDE
            // is the AppBot caller, not whichever window happens to have focus.
            var fgHwnd = ResolveCallerTerminalHwnd();
            var hwndPrefix = fgHwnd != IntPtr.Zero ? $"__hwnd:0x{fgHwnd.ToInt64():X8}" : null;

            // Also resolve the ConPTY (PseudoConsoleWindow) HWND of THIS terminal tab.
            // Distinct from __hwnd: the placement HWND is the visible IDE window; the ConPTY
            // HWND is the hidden console window of the specific tab that ran wkappbot, needed
            // for future input injection into that exact tab.
            IntPtr conHwnd = GetConsoleWindow();
            if (conHwnd == IntPtr.Zero)
                conHwnd = FindAncestorPseudoConsoleWindow();
            var conHwndPrefix = conHwnd != IntPtr.Zero ? $"__conhwnd:0x{conHwnd.ToInt64():X8}" : null;

            // Pass the Launcher-validated Chrome placement target (set by
            // MyCdpContext.Placement.TryMoveWebBotNearCaller) through to Eye
            // so Core's ComputePlacementNearCaller honours the same target
            // the Launcher already used for its SetWindowPos. Without this,
            // Eye-delegated commands re-derive the target inside Core from
            // the caller HWND, which can diverge from the Launcher's value
            // (e.g. when ResolveCallerHwnd in Core re-walks the process tree
            // and picks a different ancestor with off-screen coordinates).
            var chromeTargetEnv = Environment.GetEnvironmentVariable("WKAPPBOT_CHROME_TARGET");
            var targetPrefix = !string.IsNullOrWhiteSpace(chromeTargetEnv)
                ? $"__target:{chromeTargetEnv}"
                : null;

            var prefixList = new List<string> { $"__cwd:{Environment.CurrentDirectory}" };
            if (hwndPrefix != null) prefixList.Add(hwndPrefix);
            if (conHwndPrefix != null) prefixList.Add(conHwndPrefix);
            if (targetPrefix != null) prefixList.Add(targetPrefix);
            var payload = prefixList.Concat(args).ToArray();
            w.WriteLine(JsonSerializer.Serialize(payload, LauncherJsonContext.Default.StringArray));

            // First-output timeout: both LAUNCH JSON and [CMD] must arrive within N ms.
            // Normal Eye: LAUNCH JSON + [CMD] come back-to-back in <10ms.
            // If either is missing within timeout -> Eye is stalled -> Core fallback.
            string? peekedLine = null;
            string? peekedLine2 = null;
            if (firstOutputTimeoutMs > 0)
            {
                var firstReadTask = Task.Run(() => r.ReadLine());
                if (!firstReadTask.Wait(firstOutputTimeoutMs))
                {
                    try { pipe.Close(); } catch { }
                    return false; // no LAUNCH JSON -> Core fallback
                }
                peekedLine = firstReadTask.Result;

                // Second line ([CMD]) must also arrive within the same window
                var secondReadTask = Task.Run(() => r.ReadLine());
                if (!secondReadTask.Wait(firstOutputTimeoutMs))
                {
                    // LAUNCH JSON came but [CMD] didn't -> Eye stuck after dispatch
                    try { pipe.Close(); } catch { }
                    Console.Error.WriteLine("[PIPE] no [CMD] within timeout -- falling back to Core");
                    return false;
                }
                peekedLine2 = secondReadTask.Result;
            }

            // Timeout: fire timer closes pipe -> unblocks ReadLine with IOException.
            // Eye continues executing in background; Launcher returns timeout exit code.
            bool timedOut = false;
            Timer? timeoutTimer = null;
            if (timeoutMs > 0)
            {
                timeoutTimer = new Timer(_ =>
                {
                    timedOut = true;
                    try { pipe.Close(); } catch { }
                }, null, timeoutMs, Timeout.Infinite);
            }

            bool gotEndMarker = false;
            int outputLines = 0; // non-LAUNCH lines written to stdout
            try
            {
                // Drain: process peeked lines first (if first-output timeout was used), then loop.
                IEnumerable<string?> Lines()
                {
                    if (peekedLine != null) yield return peekedLine;
                    if (peekedLine2 != null) yield return peekedLine2;
                    string? l;
                    while ((l = r.ReadLine()) != null) yield return l;
                }

                foreach (var line in Lines())
                {
                    if (line == null) break;
                    if (line.StartsWith(EndMarker))
                    {
                        int.TryParse(line.AsSpan(EndMarker.Length).Trim(), out exitCode);
                        gotEndMarker = true;
                        break;
                    }
                    writeLine(line);
                    // LAUNCH JSON is a sentinel header -- don't count as real output
                    if (!line.StartsWith("{\"_\":\"LAUNCH\"") && !line.StartsWith("{\"_\": \"LAUNCH\""))
                        outputLines++;
                }
                if (timedOut) exitCode = timeoutExitCode;
            }
            catch (Exception) when (timedOut)
            {
                exitCode = timeoutExitCode;
            }
            finally
            {
                timeoutTimer?.Dispose();
            }

            // Incomplete pipe guard: pipe closed without EndMarker
            //   Case A: only LAUNCH JSON came (or nothing) -> Core fallback (silent crash/kill)
            //   Case B: some real output came + no EndMarker -> error but no re-run (partial output already sent)
            if (!timedOut && !gotEndMarker)
            {
                if (outputLines == 0) // only LAUNCH JSON or truly empty
                {
                    Console.Error.WriteLine("[PIPE] incomplete -- no output after LAUNCH, falling back to Core");
                    return false; // Launcher will re-run via Core
                }
                else // partial real output was already written
                {
                    Console.Error.WriteLine("[PIPE] incomplete -- pipe closed without EndMarker (partial output)");
                    exitCode = -1;
                }
            }

            return true;
        }
        catch
        {
            return connected; // false = Eye not running; true = connected but read failed
        }
        finally
        {
            try { pipe.Dispose(); } catch { }
        }
    }

    [DllImport("user32.dll")] static extern bool IsWindow(IntPtr hWnd);
    [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] static extern IntPtr GetAncestor(IntPtr hWnd, uint flags);
    [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint pid);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetClassNameW(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
    [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();

    [StructLayout(LayoutKind.Sequential)]
    struct RECT { public int Left, Top, Right, Bottom; }

    delegate bool EnumWindowsProcDelegate(IntPtr hWnd, IntPtr lParam);
    [DllImport("user32.dll")]
    static extern bool EnumWindows(EnumWindowsProcDelegate proc, IntPtr lParam);

    [DllImport("ntdll.dll")]
    static extern int NtQueryInformationProcess(IntPtr handle, int infoClass, byte[] info, int infoLen, ref int retLen);

    /// <summary>
    /// Resolve the hwnd of the caller window where the user invoked wkappbot.
    ///
    /// STANDARD APPBOT WINDOW = the window of the NEAREST ANCESTOR PROCESS in the wkappbot
    /// process chain that owns a visible, non-minimized window. Foreground/focus window is
    /// COMPLETELY IRRELEVANT. GetForegroundWindow() is ALWAYS wrong for this purpose.
    ///
    /// Algorithm: Walk OUR ancestor process chain (Launcher -> shell -> host) up to 8 levels.
    /// For each ancestor PID, EnumWindows and return the FIRST visible, non-minimized,
    /// titled top-level window. First hit on the CLOSEST ancestor wins. No class restriction --
    /// VS Code, Claude Code, any IDE, any shell host that launched wkappbot is accepted.
    /// Spec: "앱봇 부모프로세스 중 가장 근접한 창을 가진 프로세스를 찾으면 그게 AppBot 호출창"
    ///
    /// Returns IntPtr.Zero if no ancestor owns a usable window. NO foreground fallback.
    /// </summary>
    internal static IntPtr ResolveCallerTerminalHwnd()
    {
        // Walk Launcher's parent process chain in order (immediate parent first).
        var ancestorPids = new List<uint>();
        try
        {
            int pid = Environment.ProcessId;
            for (int depth = 0; depth < 8 && pid > 0; depth++)
            {
                var parent = GetParentPid(pid);
                if (parent <= 0 || parent == pid) break;
                ancestorPids.Add((uint)parent);
                pid = parent;
            }
        }
        catch { }

        // Walk ancestor PIDs in order; for each, EnumWindows looking for a usable window.
        // First hit on the closest ancestor wins.
        //
        // PREFERENCE ORDER (per ancestor, closest first):
        //   1. A VISIBLE, root, non-degenerate window -- the real terminal window we
        //      want for Chrome placement (Windows Terminal CASCADIA host, VS Code,
        //      Claude IDE, etc.). Return it immediately.
        //   2. A hidden window with a non-zero rect (ConPTY PseudoConsoleWindow) --
        //      kept only as a LAST-RESORT fallback if NO ancestor in the whole chain
        //      owns a visible window.
        //
        // Why visible-first matters: ConPTY (PseudoConsoleWindow) windows live INSIDE
        // the AppBot's own process (claude.exe etc.), are hidden, sit at a degenerate
        // (0,0,16,16) rect, and are NOT child windows of the terminal host
        // (GetParent==0, GetAncestor(GA_ROOT)==self -- verified). They carry no usable
        // placement coordinates and cannot be walked up to a visible parent. The OLD
        // code returned the first match per ancestor, so the hidden ConPTY of the
        // nearest AppBot ancestor short-circuited the walk before it reached the
        // visible Windows Terminal further up the chain. Downstream
        // ResolveValidCallerWindow then rejected the hidden window and fell back to a
        // GLOBAL-largest-window heuristic -- which picks the wrong terminal when more
        // than one terminal is open. Preferring the visible ancestor window here makes
        // resolution ancestry-driven and correct regardless of terminal count.
        IntPtr hiddenFallback = IntPtr.Zero;
        foreach (var targetPid in ancestorPids)
        {
            // GA_ROOTOWNER PRE-PASS (verified 2026-05-24): A Windows Terminal tab spawns a
            // VISIBLE PseudoConsoleWindow (rect [0,0,0,0]) inside its ConPTY host process.
            // That PCW's GA_ROOTOWNER (flag=3) is the exact CASCADIA tab window that owns it.
            // This is the only reliable way to map a ConPTY back to its specific WT tab when
            // the direct launcher->WT ancestry is broken by dead intermediate PIDs -- the
            // PCW's host process IS in our ancestor chain, so this resolves the correct tab.
            // GA_ROOT (flag=2) is useless here (returns self for both WT-managed and
            // AllocConsole'd PCWs). Vis=False PCWs [0,0,16,16] are AllocConsole'd and their
            // GA_ROOTOWNER == self -- the IsWindowVisible gate skips those. Runs FIRST per
            // ancestor (inner-to-outer) so the closest ConPTY tab wins.
            IntPtr cascadiaMatch = IntPtr.Zero;
            IntPtr pcwForLog = IntPtr.Zero;
            EnumWindows((hWnd, _) =>
            {
                GetWindowThreadProcessId(hWnd, out uint wpid);
                if (wpid != targetPid) return true; // not this ancestor
                if (!IsWindow(hWnd) || !IsWindowVisible(hWnd)) return true;
                var cls = new System.Text.StringBuilder(256);
                GetClassNameW(hWnd, cls, 256);
                if (cls.ToString() != "PseudoConsoleWindow") return true;
                IntPtr rootOwner = GetAncestor(hWnd, 3 /* GA_ROOTOWNER */);
                if (rootOwner == IntPtr.Zero || rootOwner == hWnd) return true; // self == useless
                var ocls = new System.Text.StringBuilder(256);
                GetClassNameW(rootOwner, ocls, 256);
                if (!ocls.ToString().StartsWith("CASCADIA", StringComparison.Ordinal)) return true;
                cascadiaMatch = rootOwner;
                pcwForLog = hWnd;
                return false; // found the WT tab for this ancestor -- stop scanning
            }, IntPtr.Zero);
            if (cascadiaMatch != IntPtr.Zero)
            {
                Console.Error.WriteLine($"[CALLER:HWND] via GA_ROOTOWNER PCW=0x{pcwForLog.ToInt64():X} CASCADIA=0x{cascadiaMatch.ToInt64():X}");
                return cascadiaMatch;
            }

            IntPtr visibleMatch = IntPtr.Zero;
            IntPtr hiddenMatch = IntPtr.Zero;
            EnumWindows((hWnd, _) =>
            {
                GetWindowThreadProcessId(hWnd, out uint wpid);
                if (wpid != targetPid) return true; // not this ancestor
                if (!IsWindow(hWnd)) return true;
                if (!GetWindowRect(hWnd, out RECT rect)) return true;
                bool degenerate = rect.Right - rect.Left <= 0 && rect.Bottom - rect.Top <= 0;
                if (IsWindowVisible(hWnd))
                {
                    // Visible windows must be root (filters owned popups / tool windows)
                    // and have a non-degenerate rect to be a real placement anchor.
                    if (GetAncestor(hWnd, 2 /* GA_ROOT */) != hWnd) return true;
                    if (degenerate) return true;
                    visibleMatch = hWnd;
                    return false; // best candidate for this ancestor -- stop scanning
                }
                // Hidden window (e.g. ConPTY PseudoConsoleWindow): remember only as a
                // last-resort fallback. Reject fully-zero rects -- they carry nothing.
                if (!degenerate && hiddenMatch == IntPtr.Zero)
                    hiddenMatch = hWnd;
                return true;
            }, IntPtr.Zero);

            // A visible window on the closest ancestor wins outright.
            if (visibleMatch != IntPtr.Zero) return visibleMatch;
            // Otherwise keep the FIRST (closest-ancestor) hidden window seen, but keep
            // walking -- a further ancestor may still own a visible terminal window.
            if (hiddenFallback == IntPtr.Zero && hiddenMatch != IntPtr.Zero)
                hiddenFallback = hiddenMatch;
        }

        // No ancestor in our own chain owns a visible window.
        // SECOND ATTEMPT: walk the ConPTY's owning process parent chain.
        // ConPTY (PseudoConsoleWindow) is owned by a ConPTY host process (conhost/openconsole).
        // That host's parent IS the visible terminal (WindowsTerminal). This route survives
        // dead intermediate launcher-chain PIDs because the ConPTY host is still alive.
        // GetConsoleWindow() returns it directly; hiddenFallback is the enum-found one.
        IntPtr conptyHwnd = GetConsoleWindow();
        if (conptyHwnd == IntPtr.Zero) conptyHwnd = hiddenFallback;
        if (conptyHwnd != IntPtr.Zero)
        {
            GetWindowThreadProcessId(conptyHwnd, out uint conptyPid);
            if (conptyPid > 0)
            {
                var conptyChain = new List<uint>();
                int cPid = (int)conptyPid;
                for (int depth = 0; depth < 8 && cPid > 0; depth++)
                {
                    var parent = GetParentPid(cPid);
                    if (parent <= 0 || parent == cPid) break;
                    conptyChain.Add((uint)parent);
                    cPid = parent;
                }
                foreach (var targetPid in conptyChain)
                {
                    IntPtr visibleMatch = IntPtr.Zero;
                    EnumWindows((hWnd, _) =>
                    {
                        GetWindowThreadProcessId(hWnd, out uint wpid);
                        if (wpid != targetPid) return true;
                        if (!IsWindow(hWnd) || !IsWindowVisible(hWnd)) return true;
                        if (GetAncestor(hWnd, 2 /* GA_ROOT */) != hWnd) return true;
                        if (!GetWindowRect(hWnd, out RECT rect)) return true;
                        if (rect.Right - rect.Left <= 0 && rect.Bottom - rect.Top <= 0) return true;
                        visibleMatch = hWnd;
                        return false;
                    }, IntPtr.Zero);
                    if (visibleMatch != IntPtr.Zero)
                    {
                        Console.Error.WriteLine($"[CALLER:HWND] via ConPTY owner chain 0x{visibleMatch.ToInt64():X} (conpty=0x{conptyHwnd.ToInt64():X})");
                        return visibleMatch;
                    }
                }
            }
        }

        // Truly nothing. Do NOT use GetForegroundWindow -- that is the user's focused
        // window (someone else's app), never the caller. Return Zero.
        return IntPtr.Zero;
    }

    /// <summary>
    /// Walk ancestor PIDs looking for a window whose class name is "PseudoConsoleWindow".
    /// Used when GetConsoleWindow() returns 0 (e.g. launcher running detached or under a host
    /// that owns the ConPTY rather than wkappbot itself). No visibility/title filter --
    /// ConPTY windows are intentionally hidden and have no title.
    /// Returns IntPtr.Zero if no ancestor owns a ConPTY window.
    /// </summary>
    static IntPtr FindAncestorPseudoConsoleWindow()
    {
        var ancestorPids = new List<uint>();
        try
        {
            int pid = Environment.ProcessId;
            for (int depth = 0; depth < 8 && pid > 0; depth++)
            {
                var parent = GetParentPid(pid);
                if (parent <= 0 || parent == pid) break;
                ancestorPids.Add((uint)parent);
                pid = parent;
            }
        }
        catch { }

        foreach (var targetPid in ancestorPids)
        {
            IntPtr match = IntPtr.Zero;
            EnumWindows((hWnd, _) =>
            {
                GetWindowThreadProcessId(hWnd, out uint wpid);
                if (wpid != targetPid) return true; // not this ancestor
                var cls = new System.Text.StringBuilder(256);
                GetClassNameW(hWnd, cls, 256);
                if (cls.ToString() != "PseudoConsoleWindow") return true;
                match = hWnd;
                return false;
            }, IntPtr.Zero);
            if (match != IntPtr.Zero) return match;
        }
        return IntPtr.Zero;
    }

    static int GetParentPid(int pid)
    {
        try
        {
            var handle = System.Diagnostics.Process.GetProcessById(pid).Handle;
            var pbi = new byte[48]; // PROCESS_BASIC_INFORMATION
            int retLen = 0;
            NtQueryInformationProcess(handle, 0, pbi, pbi.Length, ref retLen);
            return (int)BitConverter.ToInt64(pbi, 40); // InheritedFromUniqueProcessId at offset 40 (64-bit: ExitStatus+pad+Peb+AffinityMask+BasePriority+pad+UniqueProcessId = 40)
        }
        catch { return 0; }
    }

    // CharSet.Unicode: prevents char[] from being marshaled as ANSI bytes.
    // Without it, Korean chars become '?' before Win32 sees them.
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WideCharToMultiByte")]
    static extern int WideCharToMultiByte(int codePage, uint dwFlags,
        char[] lpWideCharStr, int cchWideChar,
        byte[] lpMultiByteStr, int cbMultiByte,
        nint lpDefaultChar, nint lpUsedDefaultChar);

    static readonly byte[] _wcBuf = new byte[65536];
    static readonly char[] _charBuf = new char[32768];

    static void WriteLineW(Stream stdout, string line, int codePage, byte[] lineEnd)
    {
        // Encode string to target code page via Win32 WideCharToMultiByte.
        // Works in NativeAOT -- no managed Encoding stack required.
        int len = line.Length;
        if (len > _charBuf.Length) len = _charBuf.Length;
        line.CopyTo(0, _charBuf, 0, len);
        int n = WideCharToMultiByte(codePage, 0, _charBuf, len, _wcBuf, _wcBuf.Length, 0, 0);
        if (n > 0) stdout.Write(_wcBuf, 0, n);
        stdout.Write(lineEnd, 0, lineEnd.Length);
        stdout.Flush();
    }
}
