// SPDX-License-Identifier: MIT
// AppBotPipe.cs -- shared between Launcher (NativeAOT) and Core.
// Linked via csproj: <Compile Include="..\..\Shared\AppBotPipe.cs" Link="Shared\AppBotPipe.cs" />
// No project dependencies -- pure kernel32 P/Invoke only.
//
// ALL process creation MUST go through this class:
//   Low-level:  AppBotPipe.CreateProcess(...)   -- raw CreateProcessW with null-CWD guard
//   High-level: AppBotPipe.Spawn(...)           -- Process.Start replacement, also uses CreateProcessW guard
//   .NET API:   AppBotPipe.StartTracked(psi)    -- Process.Start wrapper with CWD + focus guard
//
// Partial-class siblings (same static class, separate files):
//   AppBotPipe.Admin.cs    -- IsElevated (cached), AdminPing, AdminExecute, EnsureAdmin
//   AppBotPipe.HotSwap.cs  -- PromoteNewExeIfPending, TryRenameSwap
//
// Admin token inheritance: CreateProcessW without explicit token creation inherits
// the caller's primary token, which means an elevated parent spawns elevated
// children by default. All three entry points above rely on this. No special
// code is needed to "propagate admin" -- it is the Windows default.

using System.Runtime.InteropServices;

/// <summary>
/// Guarded CreateProcessW + high-level Spawn().
/// Null CWD guard: DETACHED_PROCESS with null lpCurrentDirectory defaults to C:\Windows\System32.
/// </summary>
internal static partial class AppBotPipe
{
    // Verbose: show CreateProcessW diagnostics only when explicitly requested.
    // Normal tool runs should stay quiet; failures still surface below.
    static readonly bool _verbose = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1"
                                 && Environment.GetEnvironmentVariable("WKAPPBOT_QUIET_FIND") != "1";

    // -- Structs ----------------------------------------------

    [StructLayout(LayoutKind.Sequential)]
    internal struct STARTUPINFOW
    {
        public int cb, _res0; // explicit padding for NativeAOT safety (int=4B -> IntPtr=8B on x64)
        public IntPtr lpReserved, lpDesktop, lpTitle;
        public uint dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute;
        public uint dwFlags; public ushort wShowWindow, cbReserved2; public IntPtr lpReserved2;
        public IntPtr hStdInput, hStdOutput, hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PROCESS_INFORMATION
    {
        public IntPtr hProcess, hThread; public uint dwProcessId, dwThreadId;
    }

    // -- Constants --------------------------------------------

    internal const uint DETACHED_PROCESS          = 0x00000008;
    internal const uint CREATE_NO_WINDOW          = 0x08000000;
    internal const uint CREATE_BREAKAWAY_FROM_JOB = 0x01000000;
    internal const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;
    internal const uint STARTF_USESTDHANDLES      = 0x100;
    internal const uint STARTF_USESHOWWINDOW      = 0x1;
    internal const uint HANDLE_FLAG_INHERIT        = 0x1;

    // -- P/Invoke --------------------------------------------─

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "CreateProcessW")]
    private static extern bool RawCreateProcessW(string? app, char[] cmd, IntPtr pa, IntPtr ta,
        bool inh, uint flags, IntPtr env, string? cwd,
        ref STARTUPINFOW si, out PROCESS_INFORMATION pi);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CreatePipe(out IntPtr hRead, out IntPtr hWrite, IntPtr sa, uint size);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetHandleInformation(IntPtr h, uint mask, uint flags);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr h);

    [DllImport("user32.dll")]
    private static extern nint GetForegroundWindow();

    // -- Focus Launch Callbacks ------------------------------─
    // Wired by WKAppBot.CLI at startup. null in Launcher (NativeAOT) -- no-op.
#pragma warning disable CS0649 // fields wired at runtime by WKAppBot.CLI, always null in NativeAOT Launcher
    /// <summary>Called before launching a known/declared focus-requiring exe. Returns true = approved.</summary>
    internal static Func<string, string, bool>? OnFocusApprovalRequired;

    /// <summary>Called after focusless launch to monitor if focus was stolen. (exePath, prevFgHwnd)</summary>
    internal static Action<string, nint>? OnPostLaunchFocusMonitor;

    /// <summary>Returns true if the exe is registered as a known focus-stealer.</summary>
    internal static Func<string, bool>? OnIsKnownFocusStealer;
#pragma warning restore CS0649

    // -- Low-level: guarded CreateProcessW --------------------

    /// <summary>
    /// Guarded CreateProcessW -- blocks null/empty CWD to prevent system32 zombie processes.
    /// </summary>
    internal static bool CreateProcess(string? app, char[] cmd, IntPtr pa, IntPtr ta,
        bool inh, uint flags, IntPtr env, string? cwd,
        ref STARTUPINFOW si, out PROCESS_INFORMATION pi,
        out int lastError, string caller = "PROC")
    {
        lastError = 0;
        var cmdStr = new string(cmd).TrimEnd('\0');

        // -- FOCUSLESS GUARD ------------------------------------─
        // wShowWindow > 0 without SW_HIDE = potential focus steal
        // Exception: SW_SHOWNOACTIVATE(4) is safe -- shows window without stealing focus
        if ((si.dwFlags & STARTF_USESHOWWINDOW) != 0 && si.wShowWindow > 0 && si.wShowWindow != 4)
        {
            try { Console.Error.WriteLine($"[{caller}:BUG] CreateProcessW BLOCKED -- wShowWindow={si.wShowWindow} violates focusless! cmd={Trunc(cmdStr, 60)}"); } catch { }
            pi = default;
            return false;
        }

        // -- NULL CWD GUARD --------------------------------------
        if (string.IsNullOrEmpty(cwd))
        {
            try { Console.Error.WriteLine($"[{caller}:BUG] CreateProcessW BLOCKED -- null CWD! cmd={Trunc(cmdStr, 60)}"); } catch { }
            pi = default;
            return false;
        }
        if (_verbose) try { Console.Error.WriteLine($"[{caller}] CreateProcessW cwd=\"{cwd}\" cmd={Trunc(cmdStr, 80)} flags=0x{flags:X}"); } catch { }
        var ok = RawCreateProcessW(app, cmd, pa, ta, inh, flags, env, cwd, ref si, out pi);
        // Save error BEFORE any Console/IO calls that would reset Win32 last error
        lastError = ok ? 0 : Marshal.GetLastWin32Error();
        try
        {
            if (ok && _verbose)
            {
                Console.Error.WriteLine($"[{caller}] CreateProcessW \u2192 pid={pi.dwProcessId}");
            }
            else if (!ok)
            {
                bool retriableBreakawayDenied = lastError == 5 && (flags & CREATE_BREAKAWAY_FROM_JOB) != 0;
                if (retriableBreakawayDenied)
                    Console.Error.WriteLine($"[{caller}:WARN] CreateProcessW breakaway denied err=5 -- retry without breakaway");
                else
                    Console.Error.WriteLine($"[{caller}] CreateProcessW FAILED err={lastError}");
            }
        }
        catch { }
        return ok;
    }

    // -- High-level: Spawn() -- Process.Start replacement ----─

    /// <summary>
    /// Spawn result -- process handle + optional redirected streams.
    /// Dispose closes all handles. Use WaitForExit/ExitCode as needed.
    /// </summary>
    internal sealed class SpawnResult : IDisposable
    {
        public int Pid;
        public IntPtr Handle;
        public System.IO.StreamWriter? StdIn;
        public System.IO.StreamReader? StdOut;
        public System.IO.StreamReader? StdErr;
        private IntPtr _hStdInWrite, _hStdOutRead, _hStdErrRead;

        internal SpawnResult(int pid, IntPtr handle,
            IntPtr hStdInWrite, IntPtr hStdOutRead, IntPtr hStdErrRead)
        {
            Pid = pid; Handle = handle;
            _hStdInWrite = hStdInWrite; _hStdOutRead = hStdOutRead; _hStdErrRead = hStdErrRead;
            if (hStdInWrite != IntPtr.Zero)
                StdIn = new System.IO.StreamWriter(
                    new System.IO.FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdInWrite, true), System.IO.FileAccess.Write),
                    new System.Text.UTF8Encoding(false)) { AutoFlush = true };
            if (hStdOutRead != IntPtr.Zero)
                StdOut = new System.IO.StreamReader(
                    new System.IO.FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdOutRead, true), System.IO.FileAccess.Read),
                    System.Text.Encoding.UTF8);
            if (hStdErrRead != IntPtr.Zero)
                StdErr = new System.IO.StreamReader(
                    new System.IO.FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdErrRead, true), System.IO.FileAccess.Read),
                    System.Text.Encoding.UTF8);
        }

        public bool HasExited { get { if (Handle == IntPtr.Zero) return true; uint code; GetExitCodeProcess(Handle, out code); return code != 259; } }
        public int ExitCode { get { uint code; GetExitCodeProcess(Handle, out code); return (int)code; } }
        public bool WaitForExit(int ms) => WaitForSingleObject(Handle, (uint)ms) == 0;
        public void Kill() { if (Handle != IntPtr.Zero) TerminateProcess(Handle, 1); }

        public void Dispose()
        {
            StdIn?.Dispose(); StdOut?.Dispose(); StdErr?.Dispose();
            if (Handle != IntPtr.Zero) { CloseHandle(Handle); Handle = IntPtr.Zero; }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    }

    /// <summary>
    /// High-level process spawn -- replaces Process.Start, goes through CreateProcessW guard.
    /// CWD is REQUIRED -- null/empty CWD is blocked (same as CreateProcess guard).
    /// env: optional environment variables to set in child (set before CreateProcess, restored after).
    /// </summary>
    internal static SpawnResult? Spawn(string exe, string args, string cwd,
        bool redirectStdIn = false, bool redirectStdOut = false, bool redirectStdErr = false,
        System.Collections.Generic.Dictionary<string, string>? env = null,
        bool requiresFocus = false,
        bool showNoActivate = false,
        string caller = "PROC")
    {
        // -- Focus Launch Guard ----------------------------------─
        var exeName = Path.GetFileName(exe);
        bool needsApproval = requiresFocus || (OnIsKnownFocusStealer?.Invoke(exeName) == true);
        if (needsApproval)
        {
            if (requiresFocus)
            {
                OnPostLaunchFocusMonitor?.Invoke(exe, 0); // register immediately (prevFg=0 = force-register signal)
            }
            if (OnFocusApprovalRequired?.Invoke(exeName, caller) == false)
            {
                try { Console.Error.WriteLine($"[{caller}] Spawn BLOCKED -- focus approval denied for {exeName}"); } catch { }
                return null;
            }
        }
        nint prevFg = needsApproval ? 0 : GetForegroundWindow();

        // Save + set env vars (inherited via CreateProcess), restore after
        System.Collections.Generic.Dictionary<string, string?>? savedEnv = null;
        if (env != null && env.Count > 0)
        {
            savedEnv = new();
            foreach (var kv in env)
            {
                savedEnv[kv.Key] = Environment.GetEnvironmentVariable(kv.Key);
                Environment.SetEnvironmentVariable(kv.Key, kv.Value);
            }
        }

        var cmdLine = $"\"{exe}\" {args}\0".ToCharArray();
        // showNoActivate=true -> SW_SHOWNOACTIVATE(4): visible but no focus steal (WPF overlays)
        // showNoActivate=false -> SW_HIDE(0): invisible background process
        var si = new STARTUPINFOW
        {
            cb = Marshal.SizeOf<STARTUPINFOW>(),
            dwFlags = STARTF_USESHOWWINDOW,
            wShowWindow = showNoActivate ? (ushort)4 : (ushort)0, // 4=SW_SHOWNOACTIVATE, 0=SW_HIDE
        };

        IntPtr hStdInRead = IntPtr.Zero, hStdInWrite = IntPtr.Zero;
        IntPtr hStdOutRead = IntPtr.Zero, hStdOutWrite = IntPtr.Zero;
        IntPtr hStdErrRead = IntPtr.Zero, hStdErrWrite = IntPtr.Zero;

        bool needPipes = redirectStdIn || redirectStdOut || redirectStdErr;
        if (needPipes)
        {
            si.dwFlags |= STARTF_USESTDHANDLES;
            if (redirectStdIn)
            {
                CreatePipe(out hStdInRead, out hStdInWrite, IntPtr.Zero, 0);
                SetHandleInformation(hStdInRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
                si.hStdInput = hStdInRead;
            }
            if (redirectStdOut)
            {
                CreatePipe(out hStdOutRead, out hStdOutWrite, IntPtr.Zero, 0);
                SetHandleInformation(hStdOutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
                si.hStdOutput = hStdOutWrite;
            }
            if (redirectStdErr)
            {
                CreatePipe(out hStdErrRead, out hStdErrWrite, IntPtr.Zero, 0);
                SetHandleInformation(hStdErrWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
                si.hStdError = hStdErrWrite;
            }
        }

        bool ok = CreateProcess(null, cmdLine, IntPtr.Zero, IntPtr.Zero,
            needPipes, // bInheritHandles only when pipes are used
            CREATE_NO_WINDOW | CREATE_BREAKAWAY_FROM_JOB, IntPtr.Zero, cwd,
            ref si, out var pi, out var cpErr, caller);

        // err=5 (ACCESS_DENIED): job object may not allow breakaway -- retry without that flag
        if (!ok && cpErr == 5)
            ok = CreateProcess(null, cmdLine, IntPtr.Zero, IntPtr.Zero,
                needPipes, CREATE_NO_WINDOW, IntPtr.Zero, cwd,
                ref si, out pi, out _, caller + "-no-breakaway");

        // Restore parent env (don't pollute caller process)
        if (savedEnv != null)
            foreach (var kv in savedEnv)
                Environment.SetEnvironmentVariable(kv.Key, kv.Value);

        // Close child-side pipe handles in parent
        if (hStdInRead  != IntPtr.Zero) CloseHandle(hStdInRead);
        if (hStdOutWrite != IntPtr.Zero) CloseHandle(hStdOutWrite);
        if (hStdErrWrite != IntPtr.Zero) CloseHandle(hStdErrWrite);

        if (!ok)
        {
            if (hStdInWrite  != IntPtr.Zero) CloseHandle(hStdInWrite);
            if (hStdOutRead  != IntPtr.Zero) CloseHandle(hStdOutRead);
            if (hStdErrRead  != IntPtr.Zero) CloseHandle(hStdErrRead);
            return null;
        }
        CloseHandle(pi.hThread);
        if (!needsApproval && prevFg != 0)
            OnPostLaunchFocusMonitor?.Invoke(exe, prevFg);
        return new SpawnResult(
            (int)pi.dwProcessId, pi.hProcess,
            hStdInWrite, hStdOutRead, hStdErrRead);
    }

    // -- StartTracked: Process.Start with CWD enforcement ------─
    /// <summary>
    /// Process.Start with mandatory CWD -- prevents system32 default.
    /// Use for cases that need .NET Process (async StreamReader, UseShellExecute=true/runas, CreateNoWindow=false).
    /// All other cases should use Spawn() which routes through CreateProcessW guard.
    /// </summary>
    internal static System.Diagnostics.Process? StartTracked(
        System.Diagnostics.ProcessStartInfo psi, string cwd,
        string caller = "PROC",
        bool requiresFocus = false)
    {
        if (string.IsNullOrEmpty(cwd))
        {
            try { Console.Error.WriteLine($"[{caller}:BUG] StartTracked BLOCKED -- null CWD! exe={psi.FileName}"); } catch { }
            return null;
        }
        psi.WorkingDirectory = cwd;

        // -- Focus Launch Guard ----------------------------------─
        var exeName = Path.GetFileName(psi.FileName);
        bool needsApproval = requiresFocus || (OnIsKnownFocusStealer?.Invoke(exeName) == true);
        if (needsApproval)
        {
            if (requiresFocus)
                OnPostLaunchFocusMonitor?.Invoke(psi.FileName, 0); // register immediately
            if (OnFocusApprovalRequired?.Invoke(exeName, caller) == false)
            {
                try { Console.Error.WriteLine($"[{caller}] StartTracked BLOCKED -- focus approval denied for {exeName}"); } catch { }
                return null;
            }
        }
        nint prevFg = needsApproval ? 0 : GetForegroundWindow();

        // -- FOCUSLESS GUARD --------------------------------------
        // UseShellExecute=true is allowed in three shapes:
        //   (1) Verb="runas"  -- UAC elevation, OS-mandated
        //   (2) WindowStyle=Minimized -- visible in taskbar but won't steal
        //       foreground (ShellExecuteEx respects SW_SHOWMINNOACTIVE). Used
        //       by ChromeLauncher to bring Chrome up for CDP without stealing
        //       focus from the active app.
        //   (3) WindowStyle=Hidden -- not shown at all.
        // Anything else (Normal/Maximized with UseShellExecute) would pop a
        // visible, focus-stealing window and is blocked.
        if (psi.UseShellExecute
            && string.IsNullOrEmpty(psi.Verb)
            && psi.WindowStyle != System.Diagnostics.ProcessWindowStyle.Minimized
            && psi.WindowStyle != System.Diagnostics.ProcessWindowStyle.Hidden)
        {
            try { Console.Error.WriteLine($"[{caller}:BUG] StartTracked -- UseShellExecute=true without Verb + WindowStyle={psi.WindowStyle} BLOCKED (focusless violation). Set WindowStyle=Minimized/Hidden, use Spawn(), or UseShellExecute=false. exe={psi.FileName}"); } catch { }
            return null;
        }

        // -- FOCUSLESS RE-ADJUSTMENT ------------------------------
        // Ignore any caller-supplied focus-requesting options -- always enforce focusless.
        // WindowStyle=Normal/Maximized -> Hidden; CreateNoWindow -> true
        if (!psi.UseShellExecute)
        {
            if (!psi.CreateNoWindow)
            {
                try { Console.Error.WriteLine($"[{caller}:WARN] StartTracked -- enforcing CreateNoWindow=true (was false) exe={psi.FileName}"); } catch { }
                psi.CreateNoWindow = true;
            }
            if (psi.WindowStyle != System.Diagnostics.ProcessWindowStyle.Hidden)
            {
                try { Console.Error.WriteLine($"[{caller}:WARN] StartTracked -- enforcing WindowStyle=Hidden (was {psi.WindowStyle}) exe={psi.FileName}"); } catch { }
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
        }

        try { Console.Error.WriteLine($"[{caller}] StartTracked cwd=\"{cwd}\" exe={psi.FileName} shell={psi.UseShellExecute} verb={psi.Verb} nowin={psi.CreateNoWindow}"); } catch { }
        var proc = System.Diagnostics.Process.Start(psi);
        if (!needsApproval && prevFg != 0)
            OnPostLaunchFocusMonitor?.Invoke(psi.FileName, prevFg);
        return proc;
    }

    /// <summary>
    /// Convenience wrapper: `AppBotPipe.Start(psi)` -- same as `StartTracked(psi, Env.CWD, "LEGACY")`.
    /// Use for mechanical migration of raw `Process.Start(psi)` call sites; prefer the
    /// explicit StartTracked(..., cwd, caller) form for new code.
    /// </summary>
    internal static System.Diagnostics.Process? Start(System.Diagnostics.ProcessStartInfo psi, string caller = "LEGACY")
    {
        var cwd = string.IsNullOrEmpty(psi.WorkingDirectory) ? Environment.CurrentDirectory : psi.WorkingDirectory;
        return StartTracked(psi, cwd, caller);
    }

    // -- SpawnMcp: DETACHED_PROCESS + stdin/stdout/stderr pipes --
    /// <summary>
    /// Spawn MCP subprocess with DETACHED_PROCESS flag and full pipe I/O.
    /// cwd is required -- prevents system32 default.
    /// envBlock: pre-built Unicode env block (IntPtr.Zero = inherit parent env).
    /// </summary>
    internal static bool SpawnMcp(string exe, string cwd, IntPtr envBlock,
        out int pid, out System.IO.Stream? stdinStream,
        out System.IO.Stream? stdoutStream, out System.IO.Stream? stderrStream)
    {
        pid = 0; stdinStream = stdoutStream = stderrStream = null;

        // Pipes: child-side handles inherit, parent-side do not
        CreatePipe(out var hStdinRead, out var hStdinWrite, IntPtr.Zero, 0);
        SetHandleInformation(hStdinRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        CreatePipe(out var hStdoutRead, out var hStdoutWrite, IntPtr.Zero, 0);
        SetHandleInformation(hStdoutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        CreatePipe(out var hStderrRead, out var hStderrWrite, IntPtr.Zero, 0);
        SetHandleInformation(hStderrWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

        var si = new STARTUPINFOW { cb = Marshal.SizeOf<STARTUPINFOW>(), dwFlags = STARTF_USESTDHANDLES,
            hStdInput = hStdinRead, hStdOutput = hStdoutWrite, hStdError = hStderrWrite };

        var cmdLine = ($"\"{exe}\" mcp" + '\0').ToCharArray();
        bool ok = CreateProcess(null, cmdLine, IntPtr.Zero, IntPtr.Zero, true,
            DETACHED_PROCESS | CREATE_UNICODE_ENVIRONMENT, envBlock, cwd, ref si, out var pi, out _, "MCP-SPAWN");

        // Close child-side handles in parent regardless of outcome
        CloseHandle(hStdinRead); CloseHandle(hStdoutWrite); CloseHandle(hStderrWrite);

        if (!ok) { CloseHandle(hStdinWrite); CloseHandle(hStdoutRead); CloseHandle(hStderrRead); return false; }

        pid = (int)pi.dwProcessId;
        CloseHandle(pi.hThread);
        stdinStream  = new System.IO.FileStream(
            new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdinWrite,  true), System.IO.FileAccess.Write);
        stdoutStream = new System.IO.FileStream(
            new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdoutRead,  true), System.IO.FileAccess.Read);
        stderrStream = new System.IO.FileStream(
            new Microsoft.Win32.SafeHandles.SafeFileHandle(hStderrRead,  true), System.IO.FileAccess.Read);
        return true;
    }

    private static string Trunc(string s, int max) => s.Length > max ? s[..(max - 3)] + "..." : s;
}