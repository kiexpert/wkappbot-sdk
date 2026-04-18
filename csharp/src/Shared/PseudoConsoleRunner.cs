using System.Runtime.InteropServices;

namespace WKAppBot.Shared;

/// <summary>
/// Launch a child process inside a Windows ConPTY (Pseudo Console). The child
/// believes it is attached to a real terminal -- cmd.exe tab completion, arrow
/// keys, F7/F8 history, colored output, and every other cooked-mode feature
/// works because Windows Console APIs see a genuine terminal on the other end.
///
/// We own both ends of the PTY via anonymous pipes:
///   - input-write  (ours)  ->  input-read   (child's stdin)
///   - output-read  (ours)  &lt;-  output-write (child's stdout+stderr merged)
///
/// Our reader thread copies output bytes verbatim to Console.Out (raw, so
/// VT100 escape sequences drive the real terminal). Our stdin translator
/// uses Console.ReadKey(intercept) to capture every keystroke and encodes it
/// as the appropriate UTF-8 / VT input sequence before writing into the input
/// pipe. On Win10 1809+ this gives a fully interactive shell that we can also
/// capture (tee to ToolOutputStore, Slack, etc) without the two goals fighting.
///
/// Single entry point: <see cref="Run"/>. Everything else is private P/Invoke
/// plumbing + one reader thread + one stdin translator thread. No global state.
/// </summary>
public static class PseudoConsoleRunner
{
    // ConPTY is Win10 1809 (build 17763). Probe kernel32 for CreatePseudoConsole
    // lazily so the type loads cleanly on older Windows; IsSupported reports the
    // result to callers that want a Process.Start fallback path.
    private static readonly Lazy<bool> _supported = new(() =>
    {
        try
        {
            var kernel = LoadLibraryW("kernel32.dll");
            if (kernel == IntPtr.Zero) return false;
            var proc = GetProcAddress(kernel, "CreatePseudoConsole");
            return proc != IntPtr.Zero;
        }
        catch { return false; }
    });

    public static bool IsSupported => _supported.Value;

    /// <summary>
    /// Launch <paramref name="exe"/> (+optional <paramref name="args"/>) inside
    /// a ConPTY and block until it exits. Child stdout is mirrored to our
    /// Console in real time; bytes are also delivered to <paramref name="onOutput"/>
    /// so callers can tee to ToolOutputStore etc.
    /// </summary>
    /// <summary>
    /// Optional Enter-key interceptor. Called with the best-effort local line
    /// buffer whenever the user presses Enter. It must return EITHER null
    /// ("do not intercept -- forward Enter to the child unmodified") OR an
    /// <see cref="Action"/> that performs the intercept work ("I will consume
    /// this line"). The runner commits to intercept ONLY when a non-null
    /// Action is returned: it sends ESC to the child (clearing its line
    /// buffer so the typed text doesn't run as a shell command), pauses
    /// briefly so the ESC-erase bytes reach the terminal, invokes the Action
    /// (which typically prints its own output), then sends CR so the child
    /// draws a fresh prompt below. This two-phase decide/commit avoids the
    /// earlier bug where ESC was sent eagerly and dropped non-intercepted
    /// commands like "dir /w".
    ///
    /// The local buffer tracks printables + Backspace only; arrow keys /
    /// Home / End / Tab taint the line (complex edit) and Enter on a tainted
    /// line is always forwarded unmodified, so interactive line editing
    /// keeps working.
    /// </summary>
    public static int Run(
        string exe,
        string? args = null,
        string? cwd = null,
        Action<byte[], int>? onOutput = null,
        Func<string, Action?>? onLineReady = null,
        bool mirrorToTerminal = true)
    {
        if (!IsSupported)
            throw new PlatformNotSupportedException(
                "ConPTY requires Windows 10 1809 (build 17763) or later");

        IntPtr inputRead = IntPtr.Zero, inputWrite = IntPtr.Zero;
        IntPtr outputRead = IntPtr.Zero, outputWrite = IntPtr.Zero;
        IntPtr hPC = IntPtr.Zero;
        IntPtr attrList = IntPtr.Zero;
        PROCESS_INFORMATION pi = default;
        Thread? readerThread = null;
        Thread? inputThread = null;
        var cts = new CancellationTokenSource();

        try
        {
            // -- Anonymous pipes (inheritance flagged for child) --
            var sa = new SECURITY_ATTRIBUTES { nLength = Marshal.SizeOf<SECURITY_ATTRIBUTES>(), bInheritHandle = 1 };
            if (!CreatePipe(out inputRead, out inputWrite, ref sa, 0))
                throw new InvalidOperationException($"CreatePipe(input) failed: {Marshal.GetLastWin32Error()}");
            if (!CreatePipe(out outputRead, out outputWrite, ref sa, 0))
                throw new InvalidOperationException($"CreatePipe(output) failed: {Marshal.GetLastWin32Error()}");

            // -- Pseudo console --
            var size = GetTerminalSize();
            var hr = CreatePseudoConsole(size, inputRead, outputWrite, 0, out hPC);
            if (hr != 0)
                throw new InvalidOperationException($"CreatePseudoConsole failed: 0x{hr:X}");

            // Child inherits inputRead + outputWrite; parent keeps inputWrite + outputRead.
            // Close parent's copies of the child ends so EOF propagates cleanly on exit.
            CloseHandle(inputRead); inputRead = IntPtr.Zero;
            CloseHandle(outputWrite); outputWrite = IntPtr.Zero;

            // -- STARTUPINFOEX with PROC_THREAD_ATTRIBUTE_PSEUDOCONSOLE --
            var si = new STARTUPINFOEX();
            si.StartupInfo.cb = Marshal.SizeOf<STARTUPINFOEX>();

            var listSize = IntPtr.Zero;
            InitializeProcThreadAttributeList(IntPtr.Zero, 1, 0, ref listSize);
            attrList = Marshal.AllocHGlobal(listSize);
            if (!InitializeProcThreadAttributeList(attrList, 1, 0, ref listSize))
                throw new InvalidOperationException($"InitializeProcThreadAttributeList: {Marshal.GetLastWin32Error()}");

            if (!UpdateProcThreadAttribute(
                    attrList, 0, (IntPtr)PROC_THREAD_ATTRIBUTE_PSEUDOCONSOLE,
                    hPC, (IntPtr)IntPtr.Size, IntPtr.Zero, IntPtr.Zero))
                throw new InvalidOperationException($"UpdateProcThreadAttribute: {Marshal.GetLastWin32Error()}");

            si.lpAttributeList = attrList;

            // -- CreateProcess --
            var cmdline = string.IsNullOrEmpty(args) ? $"\"{exe}\"" : $"\"{exe}\" {args}";
            if (!CreateProcessW(
                    null, cmdline, IntPtr.Zero, IntPtr.Zero, false,
                    EXTENDED_STARTUPINFO_PRESENT, IntPtr.Zero,
                    string.IsNullOrEmpty(cwd) ? null : cwd,
                    ref si, out pi))
                throw new InvalidOperationException($"CreateProcessW: {Marshal.GetLastWin32Error()}");

            // Parent no longer needs its copies of child-end handles (already closed above).
            // Start reader + stdin translator threads.
            readerThread = new Thread(() => RelayOutput(outputRead, onOutput, mirrorToTerminal, cts.Token))
                { IsBackground = true, Name = "conpty-reader" };
            readerThread.Start();

            inputThread = new Thread(() => RelayInput(inputWrite, hPC, onLineReady, cts.Token))
                { IsBackground = true, Name = "conpty-stdin" };
            inputThread.Start();

            // -- Wait for child to exit --
            WaitForSingleObject(pi.hProcess, INFINITE);
            GetExitCodeProcess(pi.hProcess, out uint exit);
            return (int)exit;
        }
        finally
        {
            cts.Cancel();

            if (hPC != IntPtr.Zero) ClosePseudoConsole(hPC);
            if (inputWrite != IntPtr.Zero) CloseHandle(inputWrite);
            if (outputRead != IntPtr.Zero) CloseHandle(outputRead);
            if (inputRead != IntPtr.Zero) CloseHandle(inputRead);
            if (outputWrite != IntPtr.Zero) CloseHandle(outputWrite);

            if (pi.hProcess != IntPtr.Zero) CloseHandle(pi.hProcess);
            if (pi.hThread != IntPtr.Zero) CloseHandle(pi.hThread);

            if (attrList != IntPtr.Zero)
            {
                DeleteProcThreadAttributeList(attrList);
                Marshal.FreeHGlobal(attrList);
            }

            try { readerThread?.Join(500); } catch { }
            try { inputThread?.Join(500); } catch { }
            cts.Dispose();
        }
    }

    // --- Output relay ----------------------------------------------------------

    private static void RelayOutput(IntPtr outputRead, Action<byte[], int>? cb, bool mirror, CancellationToken ct)
    {
        var buf = new byte[4096];
        Stream? stdout = mirror ? Console.OpenStandardOutput() : null;
        try
        {
            while (!ct.IsCancellationRequested)
            {
                if (!ReadFile(outputRead, buf, (uint)buf.Length, out uint read, IntPtr.Zero) || read == 0)
                    break;
                try { cb?.Invoke(buf, (int)read); } catch { }
                try
                {
                    stdout?.Write(buf, 0, (int)read);
                    stdout?.Flush();
                }
                catch { break; }
            }
        }
        catch { /* pipe closed on child exit */ }
    }

    // --- Input relay (Console.ReadKey -> VT input sequences) ------------------

    private static void RelayInput(
        IntPtr inputWrite,
        IntPtr hPC,
        Func<string, Action?>? onLineReady,
        CancellationToken ct)
    {
        // Local line buffer: shadows what the user has typed since the last
        // Enter (as far as we can track). Printable chars append, Backspace
        // pops, Enter triggers the interceptor. Arrow keys / Home / End / Tab
        // set `complexEdit` and we stop trusting the buffer for this line --
        // interactive editing via cmd.exe's own line editor keeps working,
        // we just skip the interceptor on Enter.
        var buffer = new System.Text.StringBuilder();
        bool complexEdit = false;

        try
        {
            while (!ct.IsCancellationRequested)
            {
                if (!Console.KeyAvailable) { Thread.Sleep(20); continue; }
                var key = Console.ReadKey(intercept: true);

                // -- Buffer accounting BEFORE we forward the key to cmd.exe --
                if (key.Key == ConsoleKey.Enter)
                {
                    if (!complexEdit && onLineReady != null)
                    {
                        var line = buffer.ToString();
                        Action? dispatch = null;
                        try { dispatch = onLineReady(line); } catch { dispatch = null; }
                        if (dispatch != null)
                        {
                            // Phase 1 -- ESC clears the child's line buffer
                            // AND visually erases what the user typed.
                            var esc = new byte[] { 0x1B };
                            if (!WriteFile(inputWrite, esc, 1, out _, IntPtr.Zero))
                                break;
                            // Small pause so ESC-erase VT bytes reach the
                            // terminal BEFORE the dispatch writes its output;
                            // otherwise the erase codes interleave with it.
                            Thread.Sleep(30);
                            // Phase 2 -- run the intercept work.
                            try { dispatch(); }
                            catch { /* dispatch logged its own error */ }
                            // Phase 3 -- single CR to drive the child to emit
                            // its own "\r\n + fresh prompt" below the dispatch.
                            var cr = new byte[] { (byte)'\r' };
                            if (!WriteFile(inputWrite, cr, 1, out _, IntPtr.Zero))
                                break;
                            buffer.Clear();
                            complexEdit = false;
                            continue;
                        }
                        // dispatch == null -> NOT intercepted, fall through to
                        // the normal Enter forwarding below. Critically, we
                        // have NOT sent ESC yet, so the child still has the
                        // user's typed text in its line buffer and runs it
                        // normally (e.g. "dir /w" keeps working).
                    }
                    buffer.Clear();
                    complexEdit = false;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (buffer.Length > 0) buffer.Length--;
                }
                else if (IsComplexEditKey(key.Key))
                {
                    complexEdit = true;
                }
                else if (key.KeyChar != '\0' && !char.IsControl(key.KeyChar))
                {
                    buffer.Append(key.KeyChar);
                }
                // else: other control keys (Esc, Ctrl+X etc) -- forward but
                // don't touch the buffer. If the user clears the cmd.exe line
                // manually (Esc), our buffer will be stale, but the next
                // complex-edit or fresh Enter resets it anyway.

                var seq = EncodeKeyToVt(key);
                if (seq == null || seq.Length == 0) continue;
                if (!WriteFile(inputWrite, seq, (uint)seq.Length, out _, IntPtr.Zero))
                    break;
            }
        }
        catch { /* best effort */ }
    }

    // Keys that mutate the cmd.exe line buffer in ways our naive append/pop
    // shadow can't follow. When we see any of these, mark the current line as
    // "complex edit" and let Enter pass through without interception.
    private static bool IsComplexEditKey(ConsoleKey k) => k switch
    {
        ConsoleKey.LeftArrow or ConsoleKey.RightArrow or
        ConsoleKey.UpArrow or ConsoleKey.DownArrow or
        ConsoleKey.Home or ConsoleKey.End or
        ConsoleKey.Tab or ConsoleKey.Delete or
        ConsoleKey.PageUp or ConsoleKey.PageDown or
        ConsoleKey.F1 or ConsoleKey.F2 or ConsoleKey.F3 or ConsoleKey.F4 or
        ConsoleKey.F5 or ConsoleKey.F6 or ConsoleKey.F7 or ConsoleKey.F8 => true,
        _ => false,
    };

    /// <summary>
    /// Translate a <see cref="ConsoleKeyInfo"/> into the UTF-8 / VT input bytes
    /// that a PTY-attached child expects. Covers the common keys; exotic
    /// combinations (e.g. Alt+F3) fall through unhandled which is fine for MVP.
    /// </summary>
    private static byte[]? EncodeKeyToVt(ConsoleKeyInfo key)
    {
        // VT arrows etc -- ESC [ X
        string? csi = key.Key switch
        {
            ConsoleKey.UpArrow    => "\x1b[A",
            ConsoleKey.DownArrow  => "\x1b[B",
            ConsoleKey.RightArrow => "\x1b[C",
            ConsoleKey.LeftArrow  => "\x1b[D",
            ConsoleKey.Home       => "\x1b[H",
            ConsoleKey.End        => "\x1b[F",
            ConsoleKey.Delete     => "\x1b[3~",
            ConsoleKey.PageUp     => "\x1b[5~",
            ConsoleKey.PageDown   => "\x1b[6~",
            ConsoleKey.F1         => "\x1bOP",
            ConsoleKey.F2         => "\x1bOQ",
            ConsoleKey.F3         => "\x1bOR",
            ConsoleKey.F4         => "\x1bOS",
            ConsoleKey.F5         => "\x1b[15~",
            ConsoleKey.F6         => "\x1b[17~",
            ConsoleKey.F7         => "\x1b[18~",
            ConsoleKey.F8         => "\x1b[19~",
            _ => null,
        };
        if (csi != null) return System.Text.Encoding.ASCII.GetBytes(csi);

        // Printable / control characters -- emit KeyChar as UTF-8.
        // Enter -> CR (\r) which PTYs translate to newline per line-ending mode.
        // Backspace -> DEL (0x7F), matching Unix convention that ConPTY honors.
        if (key.Key == ConsoleKey.Enter) return new byte[] { (byte)'\r' };
        if (key.Key == ConsoleKey.Backspace) return new byte[] { 0x7F };
        if (key.Key == ConsoleKey.Tab) return new byte[] { (byte)'\t' };
        if (key.Key == ConsoleKey.Escape) return new byte[] { 0x1b };

        if (key.KeyChar == '\0') return null;
        return System.Text.Encoding.UTF8.GetBytes(new[] { key.KeyChar });
    }

    // --- Terminal size -------------------------------------------------------

    private static COORD GetTerminalSize()
    {
        try
        {
            var h = GetStdHandle(STD_OUTPUT_HANDLE);
            if (h != IntPtr.Zero && h != INVALID_HANDLE_VALUE
                && GetConsoleScreenBufferInfo(h, out var info))
            {
                return new COORD
                {
                    X = (short)(info.srWindow.Right - info.srWindow.Left + 1),
                    Y = (short)(info.srWindow.Bottom - info.srWindow.Top + 1),
                };
            }
        }
        catch { }
        return new COORD { X = 120, Y = 30 }; // sensible default
    }

    // --- P/Invoke ------------------------------------------------------------

    private const int PROC_THREAD_ATTRIBUTE_PSEUDOCONSOLE = 0x00020016;
    private const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;
    private const int STD_OUTPUT_HANDLE = -11;
    private static readonly IntPtr INVALID_HANDLE_VALUE = new(-1);
    private const uint INFINITE = 0xFFFFFFFF;

    [StructLayout(LayoutKind.Sequential)]
    private struct SECURITY_ATTRIBUTES { public int nLength; public IntPtr lpSecurityDescriptor; public int bInheritHandle; }

    [StructLayout(LayoutKind.Sequential)]
    private struct COORD { public short X; public short Y; }

    [StructLayout(LayoutKind.Sequential)]
    private struct SMALL_RECT { public short Left, Top, Right, Bottom; }

    [StructLayout(LayoutKind.Sequential)]
    private struct CONSOLE_SCREEN_BUFFER_INFO
    {
        public COORD dwSize;
        public COORD dwCursorPosition;
        public short wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFO
    {
        public int cb;
        public string? lpReserved;
        public string? lpDesktop;
        public string? lpTitle;
        public int dwX, dwY, dwXSize, dwYSize;
        public int dwXCountChars, dwYCountChars, dwFillAttribute, dwFlags;
        public short wShowWindow, cbReserved2;
        public IntPtr lpReserved2, hStdInput, hStdOutput, hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFOEX
    {
        public STARTUPINFO StartupInfo;
        public IntPtr lpAttributeList;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_INFORMATION { public IntPtr hProcess, hThread; public int dwProcessId, dwThreadId; }

    // LoadLibraryW takes LPCWSTR -- must marshal the name as Unicode. Without
    // CharSet.Unicode the default (Ansi) would push "kernel32.dll" as ANSI bytes
    // into a wide-char parameter; LoadLibraryW then reads "k\0e\0..." as
    // "k"/garbage and returns NULL, making IsSupported permanently false. That's
    // exactly why ConPTY silently wasn't engaging.
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)] private static extern IntPtr LoadLibraryW(string name);
    // GetProcAddress takes LPCSTR (ANSI) -- default marshalling is correct here.
    [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetProcAddress(IntPtr h, string name);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool CloseHandle(IntPtr h);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool CreatePipe(out IntPtr hRead, out IntPtr hWrite, ref SECURITY_ATTRIBUTES sa, int nSize);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool ReadFile(IntPtr h, byte[] buf, uint toRead, out uint read, IntPtr overlapped);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool WriteFile(IntPtr h, byte[] buf, uint toWrite, out uint written, IntPtr overlapped);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool GetConsoleScreenBufferInfo(IntPtr h, out CONSOLE_SCREEN_BUFFER_INFO info);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern int CreatePseudoConsole(COORD size, IntPtr hInput, IntPtr hOutput, uint dwFlags, out IntPtr phPC);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern int ResizePseudoConsole(IntPtr hPC, COORD size);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern void ClosePseudoConsole(IntPtr hPC);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool InitializeProcThreadAttributeList(IntPtr lpAttributeList, int dwAttributeCount, int dwFlags, ref IntPtr lpSize);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool UpdateProcThreadAttribute(IntPtr lpAttributeList, uint dwFlags, IntPtr Attribute, IntPtr lpValue, IntPtr cbSize, IntPtr lpPreviousValue, IntPtr lpReturnSize);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern void DeleteProcThreadAttributeList(IntPtr lpAttributeList);
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CreateProcessW(
        string? lpApplicationName, string lpCommandLine, IntPtr lpProcAttrs, IntPtr lpThreadAttrs,
        bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string? lpCurrentDirectory,
        ref STARTUPINFOEX si, out PROCESS_INFORMATION pi);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern uint WaitForSingleObject(IntPtr h, uint ms);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool GetExitCodeProcess(IntPtr h, out uint code);
}
