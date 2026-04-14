// IocpPipeRelay.cs — IOCP-based pipe relay for Core process stdout/stderr multiplexing.
// Named pipes with FILE_FLAG_OVERLAPPED → IOCP → GetQueuedCompletionStatus loop.
// Exit detection: Core writes exit-file (WKAPPBOT_LAUNCHER_PID) → Launcher polls every 50ms.
// Named pipe IOCP completions are delayed ~30s by .NET 8 DLL-detach handle cleanup,
// so file-based sentinel is the primary exit mechanism.

using System.Text;
using STARTUPINFOW = AppBotPipe.STARTUPINFOW;
using PROCESS_INFORMATION = AppBotPipe.PROCESS_INFORMATION;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Spawn Core with DETACHED_PROCESS + IOCP pipe relay.
    /// DETACHED_PROCESS prevents console LPC init (avoids bash/ConPTY deadlock).
    /// IOCP multiplexes stdout + stderr asynchronously.
    /// Exit detection via file sentinel (WKAPPBOT_LAUNCHER_PID).
    /// Falls back to RunCore() if detached spawn fails.
    /// </summary>
    static int RunCoreDetachedNormal(string[] args, bool showStderr = false,
        System.Collections.Generic.List<(long ms, string msg)>? stderrBuf = null)
    {
        Prof("IOCP: RunCoreDetachedNormal enter");
        var dir  = System.IO.Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = ResolveCoreExe();
        Prof($"IOCP: core={core} exists={System.IO.File.Exists(core)}");
        if (!System.IO.File.Exists(core)) return RunCore(args); // fallback

        // Parse --timeout/--timeout-exit (same as RunCore)
        int timeoutSec = 0, timeoutExit = 2;
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--timeout"      && int.TryParse(args[i + 1], out var t) && t > 0) timeoutSec  = t;
            if (args[i] == "--timeout-exit" && int.TryParse(args[i + 1], out var e))           timeoutExit = e;
        }

        // Build env: current process env minus MSYS2/PTY vars + WKAPPBOT_EXIT_FILE
        var exitDir = System.IO.Directory.Exists(@"C:\Temp") ? @"C:\Temp" : System.IO.Path.GetTempPath();
        var exitFilePath = System.IO.Path.Combine(exitDir, $"wkappbot-exit-{Environment.ProcessId}");

        var strip = new System.Collections.Generic.HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
        {
            "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL", "ConEmuANSI",
            "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST", "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE",
        };
        var envSb = new StringBuilder();
        foreach (System.Collections.DictionaryEntry kv in Environment.GetEnvironmentVariables())
        {
            var k = kv.Key?.ToString() ?? "";
            if (strip.Contains(k) || k == "WKAPPBOT_EXIT_FILE") continue;
            envSb.Append(k).Append('=').Append(kv.Value?.ToString() ?? "").Append('\0');
        }
        // Pass exact exit-file path — avoids PID mismatch (.NET 8 single-file inner process PID ≠ host PID)
        envSb.Append("WKAPPBOT_EXIT_FILE=").Append(exitFilePath).Append('\0');
        envSb.Append('\0');
        Prof($"IOCP: env WKAPPBOT_EXIT_FILE={exitFilePath}");
        var envBytes = Encoding.Unicode.GetBytes(envSb.ToString());
        var envPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(envBytes.Length);
        System.Runtime.InteropServices.Marshal.Copy(envBytes, 0, envPtr, envBytes.Length);

        // ALSO set on current process — inherited even if custom env block is used
        Environment.SetEnvironmentVariable("WKAPPBOT_EXIT_FILE", exitFilePath);

        try
        {
            // Exit code 99 = hot-swap restart: Core swapped binary and wants Launcher to re-spawn.
            // Loop keeps same Launcher process → same terminal/console → no new window.
            while (true)
            {
                int code = RunCoreDetachedIocp(core, args, envPtr, exitFilePath, timeoutSec, timeoutExit, showStderr, stderrBuf);
                if (code != 99) return code;
                Prof("IOCP: exit code 99 — hot-swap restart, re-spawning Core");
                // Re-resolve core path (binary was swapped on disk)
                core = ResolveCoreExe();
                if (!System.IO.File.Exists(core)) { Prof("IOCP: new core not found, abort"); return 1; }
                try { System.IO.File.Delete(exitFilePath); } catch { }
            }
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(envPtr);
        }
    }

    static int RunCoreDetachedIocp(string core, string[] args, IntPtr envPtr, string exitFilePath, int timeoutSec, int timeoutExit,
        bool showStderr = false, System.Collections.Generic.List<(long ms, string msg)>? stderrBuf = null)
    {
        var pid = Environment.ProcessId;

        // 1. Create named pipes with FILE_FLAG_OVERLAPPED
        var pipeOutName = $@"\\.\pipe\wkappbot-out-{pid}";
        var pipeErrName = $@"\\.\pipe\wkappbot-err-{pid}";

        var hPipeOut = CreateNamedPipeW(pipeOutName,
            PIPE_ACCESS_INBOUND | FILE_FLAG_OVERLAPPED, 0 /*PIPE_TYPE_BYTE|PIPE_WAIT*/,
            1, 65536, 65536, 0, IntPtr.Zero);
        var hPipeErr = CreateNamedPipeW(pipeErrName,
            PIPE_ACCESS_INBOUND | FILE_FLAG_OVERLAPPED, 0,
            1, 65536, 65536, 0, IntPtr.Zero);

        if (hPipeOut == INVALID_HANDLE || hPipeErr == INVALID_HANDLE)
        {
            Prof($"IOCP: CreateNamedPipe failed err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()} → RunCore fallback");
            if (hPipeOut != INVALID_HANDLE) CloseHandle(hPipeOut);
            if (hPipeErr != INVALID_HANDLE) CloseHandle(hPipeErr);
            return RunCore(args);
        }
        Prof("IOCP: named pipes created");

        // 2. Create IOCP + associate BOTH pipes BEFORE ConnectNamedPipe
        const ulong KEY_STDOUT = 1, KEY_STDERR = 2;
        var hIocp = CreateIoCompletionPort(INVALID_HANDLE, IntPtr.Zero, UIntPtr.Zero, 1);
        CreateIoCompletionPort(hPipeOut, hIocp, new UIntPtr(KEY_STDOUT), 0);
        CreateIoCompletionPort(hPipeErr, hIocp, new UIntPtr(KEY_STDERR), 0);
        Prof("IOCP: port created + pipes associated");

        // 3. ConnectNamedPipe (overlapped) — server enters "listening"
        var ovCOut = new System.Threading.NativeOverlapped();
        var ovCErr = new System.Threading.NativeOverlapped();
        ConnectNamedPipe(hPipeOut, ref ovCOut);
        ConnectNamedPipe(hPipeErr, ref ovCErr);
        Prof("IOCP: ConnectNamedPipe issued");

        // 4. CreateFileW — client connects to named pipes
        var hClientOut = CreateFileW(pipeOutName, GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        var hClientErr = CreateFileW(pipeErrName, GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        if (hClientOut == INVALID_HANDLE || hClientErr == INVALID_HANDLE)
        {
            Prof($"IOCP: CreateFileW client failed err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()} → RunCore fallback");
            CloseHandle(hPipeOut); CloseHandle(hPipeErr); CloseHandle(hIocp);
            if (hClientOut != INVALID_HANDLE) CloseHandle(hClientOut);
            if (hClientErr != INVALID_HANDLE) CloseHandle(hClientErr);
            return RunCore(args);
        }
        SetHandleInformation(hClientOut, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        SetHandleInformation(hClientErr, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        Prof("IOCP: client handles created");

        // Drain 2 connect completions
        for (int c = 0; c < 2; c++)
            GetQueuedCompletionStatus(hIocp, out _, out _, out _, 5000);
        Prof("IOCP: connect completions drained");

        // 5. Create named event for Core→Launcher exit signaling
        var exitEventName = $"wkappbot-exit-{pid}";
        var hExitEvent = CreateEventW(IntPtr.Zero, true /*manual-reset*/, false /*initial=nonsignaled*/, exitEventName);
        Prof($"IOCP: exit event created name={exitEventName} handle={hExitEvent}");

        // 5b. Spawn Core with client handles as stdout/stderr
        // Append --exit-event as hidden arg (Core opens event by name and signals it)
        var cmd = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmd.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        cmd.Append(" \"--exit-event\" \"").Append(exitEventName).Append('"');
        var cmdArr = (cmd.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW
        {
            cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>(),
            dwFlags = STARTF_USESTDHANDLES,
            hStdInput  = INVALID_HANDLE,
            hStdOutput = hClientOut,
            hStdError  = hClientErr,
        };
        // NOTE: Use IntPtr.Zero (inherit parent env) instead of custom envBlock.
        // Environment.SetEnvironmentVariable("WKAPPBOT_EXIT_FILE") was called before this point.
        // Custom envBlock was failing to deliver env vars to .NET 8 single-file Core.
        bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, true,
            DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB,
            IntPtr.Zero, Environment.CurrentDirectory, ref si, out var pi);
        if (!ok && System.Runtime.InteropServices.Marshal.GetLastWin32Error() == 5)
        {
            Prof("IOCP: CreateProcess err=5 with breakaway — retrying without breakaway");
            ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, true,
                DETACHED_PROCESS,
                IntPtr.Zero, Environment.CurrentDirectory, ref si, out pi);
        }

        // Close client-side handles in parent (child holds them → EOF on child exit)
        CloseHandle(hClientOut);
        CloseHandle(hClientErr);

        if (!ok)
        {
            Prof($"IOCP: CreateProcess failed err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()} → RunCore fallback");
            CloseHandle(hPipeOut); CloseHandle(hPipeErr); CloseHandle(hIocp);
            return RunCore(args);
        }
        CloseHandle(pi.hThread);
        Prof($"IOCP: Core spawned pid={pi.dwProcessId}");

        // 6. IOCP read loop + process handle wait
        // Strategy: IOCP thread relays pipe output. Main thread waits on process handle.
        // When Core exits, WaitForSingleObject signals immediately (kernel object, no FS delay).
        // Then drain remaining pipe data with short IOCP timeout.
        var bufOut = new byte[65536];
        var bufErr = new byte[4096];
        var ovROut = new System.Threading.NativeOverlapped();
        var ovRErr = new System.Threading.NativeOverlapped();
        ReadFileOv(hPipeOut, bufOut, (uint)bufOut.Length, out _, ref ovROut);
        ReadFileOv(hPipeErr, bufErr, (uint)bufErr.Length, out _, ref ovRErr);

        var rawStdout = Console.OpenStandardOutput();
        var _stderr = Console.OpenStandardError();
        // Stdout: wrap with TranscodeStream when terminal is non-UTF-8 (e.g. CP949 CMD).
        // TranscodeStream uses a stateful Decoder — correctly handles multi-byte sequences
        // split across IOCP read boundaries (unlike one-shot GetString/GetBytes).
        Stream _stdout = _needsTranscode
            ? new TranscodeStream(rawStdout, _consoleCodePage)
            : rawStdout;
        Action<byte[], int> writeStdout = (buf, len) => { _stdout.Write(buf, 0, len); _stdout.Flush(); };
        // Stderr strategy: passthrough (--stderr) or buffer-only (default)
        Action<byte[], int> writeStderr = showStderr
            ? (buf, len) => { _stderr.Write(buf, 0, len); _stderr.Flush(); }
            : (buf, len) => { if (stderrBuf != null) lock (stderrBuf) stderrBuf.Add((_sw.ElapsedMilliseconds, System.Text.Encoding.UTF8.GetString(buf, 0, len).TrimEnd())); };
        int effectiveTimeoutMs = timeoutSec > 0 ? timeoutSec * 1000 : 0;

        // IOCP relay in background thread
        int stopFlag = 0;
        var iocpThread = new System.Threading.Thread(() =>
        {
            int eof = 0;
            while (eof < 2 && System.Threading.Interlocked.CompareExchange(ref stopFlag, 0, 0) == 0)
            {
                bool got = GetQueuedCompletionStatus(hIocp, out uint bytes, out var key, out var pOv, 200);
                if (!got) { if (pOv != IntPtr.Zero) eof++; continue; }
                if (bytes == 0) { eof++; continue; }
                ulong k = (ulong)key;
                if (k == KEY_STDOUT)
                {
                    try { writeStdout(bufOut, (int)bytes); } catch { }
                    ovROut = new System.Threading.NativeOverlapped();
                    ReadFileOv(hPipeOut, bufOut, (uint)bufOut.Length, out _, ref ovROut);
                }
                else if (k == KEY_STDERR)
                {
                    try { writeStderr(bufErr, (int)bytes); } catch { }
                    ovRErr = new System.Threading.NativeOverlapped();
                    ReadFileOv(hPipeErr, bufErr, (uint)bufErr.Length, out _, ref ovRErr);
                }
            }
        }) { IsBackground = true, Name = "IOCP-Relay" };
        iocpThread.Start();

        // Main thread: poll event + process handle (diagnostic)
        // Both WaitForSingleObject(event, INFINITE) and file-based approaches showed 30s delay.
        // Poll both to see if the event state is signaled before 30s.
        uint waitResult = 0x102;
        long _diagNext2 = 2000;
        while (true)
        {
            uint evtState = WaitForSingleObject(hExitEvent, 0);
            if (evtState == 0) { waitResult = 0; Prof($"IOCP: exit event signaled (poll)! t={_sw.ElapsedMilliseconds}ms"); break; }
            uint procState = WaitForSingleObject(pi.hProcess, 0);
            if (procState == 0) { waitResult = 0; Prof($"IOCP: process exited (poll)! t={_sw.ElapsedMilliseconds}ms"); break; }
            if (_sw.ElapsedMilliseconds >= _diagNext2)
            {
                Prof($"IOCP: poll t={_sw.ElapsedMilliseconds}ms evt={evtState} proc={procState} iocp={iocpThread.IsAlive}");
                _diagNext2 = _sw.ElapsedMilliseconds + 2000;
            }
            if (effectiveTimeoutMs > 0 && _sw.ElapsedMilliseconds > effectiveTimeoutMs)
            {
                Console.Error.WriteLine($"[LAUNCHER] timeout {timeoutSec}s — exiting");
                try { _stdout.Flush(); } catch { }
                CloseHandle(hExitEvent); CloseHandle(hPipeOut); CloseHandle(hPipeErr); CloseHandle(hIocp); CloseHandle(pi.hProcess);
                TerminateSelf((uint)timeoutExit);
                return timeoutExit;
            }
            System.Threading.Thread.Sleep(50);
        }
        if (waitResult == 0) Prof($"IOCP: signaled at t={_sw.ElapsedMilliseconds}ms");

        // Event signaled — drain remaining pipe data (buffered output may still be in transit)
        System.Threading.Interlocked.Exchange(ref stopFlag, 1);
        iocpThread.Join(2000);

        // Get exit code: process handle may show STILL_ACTIVE (259) since Core hasn't fully exited
        uint exitCode = 0;
        GetExitCodeProcess(pi.hProcess, out exitCode);
        if (exitCode == 259) exitCode = 0; // STILL_ACTIVE → treat as success
        // Try exit-file for exact exit code from Main
        try { if (System.IO.File.Exists(exitFilePath)) { uint.TryParse(System.IO.File.ReadAllText(exitFilePath).Trim(), out var fc); exitCode = fc; } } catch { }
        try { System.IO.File.Delete(exitFilePath); } catch { }
        CloseHandle(hExitEvent);

        try { _stdout.Flush(); } catch { }
        // Kill Core if still alive — prevents zombie accumulation (30s DLL detach delay)
        try { TerminateProcess(pi.hProcess, exitCode != 0 ? (uint)exitCode : 0); } catch { }
        CloseHandle(hPipeOut); CloseHandle(hPipeErr); CloseHandle(hIocp); CloseHandle(pi.hProcess);
        Prof($"IOCP: done exitCode={exitCode}");
        return (int)exitCode;
    }
}
