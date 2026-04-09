using System.Diagnostics;
using System.Text;

namespace WKAppBot.Launcher;

partial class Program
{

    /// <param name="fastExitTimeoutMs">If >0, kills Core if it doesn't exit within this time (ms) and returns exit 0.
    /// Used for fast-exit commands (help) where Core may hang 26s due to RotateOldLogs on W: drive.</param>
    /// <param name="stdoutRelayOnly">If true, redirect only stdout (not stdin/stderr) and relay it.
    /// Used for grap/grep with args — prevents MSYS2 PTY drain (~31s) from large stdout output.
    /// Core inherits stdin/stderr from Launcher (PTY) so interactive features still work.</param>
    /// <summary>
    /// Spawn Core with CreateNoWindow=true and stdin piped (no ConPTY inheritance).
    /// stdout/stderr are NOT redirected — Core writes directly to Launcher's terminal handles.
    /// This avoids ConPTY handle inheritance and unreliable pipe relay.
    /// </summary>
    static int RunCoreNoConPty(string[] args)
    {
        try
        {
            var dir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
            var core = ResolveCoreExe();
            if (!File.Exists(core)) { Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found at: {core}"); return 1; }

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = core,
                    UseShellExecute       = false,
                    CreateNoWindow        = true,  // no ConPTY handle inheritance
                    RedirectStandardInput = true,  // pipe stdin → Core holds no ConPTY stdin
                    RedirectStandardOutput = false, // Core writes directly to Launcher's stdout handle
                    RedirectStandardError  = false, // Core writes directly to Launcher's stderr handle
                }
            };
            // Strip MSYS2/Cygwin PTY env vars — AppHost deadlock prevention (same as fastExit path)
            foreach (var v in new[] { "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL",
                                      "ConEmuANSI", "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST",
                                      "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE" })
                proc.StartInfo.EnvironmentVariables.Remove(v);
            foreach (var a in args) proc.StartInfo.ArgumentList.Add(a);

            proc.Start();
            try { proc.StandardInput.Close(); } catch { } // EOF stdin immediately
            // Use WaitForExit(timeout) instead of blocking WaitForExit() — avoids potential
            // .NET internal async-stream-completion wait that can block 27-35s in AOT builds.
            if (!proc.WaitForExit(5000)) proc.Kill(entireProcessTree: false);
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] RunCoreNoConPty error: {ex.Message}");
            return 1;
        }
    }

    static int RunCore(string[] args, int fastExitTimeoutMs = 0, bool stdoutRelayOnly = false)
    {
        try
        {
            // wkappbot-core.exe lives next to wkappbot.exe in SDK/bin/
            var dir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
            var core = ResolveCoreExe();

            if (!File.Exists(core))
            {
                Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found at: {core}");
                Console.Error.WriteLine("[LAUNCHER] Run: dotnet publish WKAppBot.CLI to deploy core.");
                return 1;
            }

            // Parse --timeout N (seconds) — Launcher-level watchdog.
            // Works for ALL commands, including ones that don't implement their own timeout.
            // stdout/stderr inherited (no piping overhead); Launcher simply kills Core if exceeded.
            int timeoutSec  = 0;
            int timeoutExit = 2;
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "--timeout"      && int.TryParse(args[i + 1], out var t) && t > 0) timeoutSec  = t;
                if (args[i] == "--timeout-exit" && int.TryParse(args[i + 1], out var e))           timeoutExit = e;
            }

            bool useFastExit   = fastExitTimeoutMs > 0;
            bool useStdoutRelay = stdoutRelayOnly && !useFastExit;
            // For grap/grep one-shot: temp file relay avoids all pipe/ConPTY handle issues.
            // ConPTY (bash → Launcher → Core) swaps stdout/stderr handles in Core when UseShellExecute=false
            // redirects both streams; and CreateNoWindow=true prevents .NET 8 from initializing Console.
            // Solution: Core writes to a local temp file (WKAPPBOT_RELAY_FILE env var), Launcher reads after.
            // For --follow mode: run Core normally (no temp file, output goes to terminal, Ctrl+C to stop).
            bool followMode = useStdoutRelay && args.Any(a => a is "-f" or "--follow");
            string? relayFilePath = null;
            if (useStdoutRelay && !followMode)
            {
                // Use C:\Temp\ instead of GetTempPath() for reliable fast visibility.
                relayFilePath = System.IO.Directory.Exists(@"C:\Temp")
                    ? $@"C:\Temp\wkappbot-relay-{Environment.ProcessId}.txt"
                    : System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"wkappbot-relay-{Environment.ProcessId}.txt");
                // File will be created by Core; delete any stale file from previous run
                try { System.IO.File.Delete(relayFilePath); } catch { }
            }

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = core,
                    UseShellExecute = false,
                    // ALL 3 streams redirected + CreateNoWindow=true for ALL spawns.
                    // Root cause of bash/MSYS2 LPC deadlock: ConPTY slave handles inherited via
                    // stdin/stderr trigger .NET 8 AppHost console init (SetConsoleMode LPC → csrss.exe).
                    // Fix: redirect stdin+stderr so Core receives pipes (not ConPTY handles).
                    // Core detects piped stdin → Console.IsInputRedirected=true → skips interactive init.
                    // "\0UIT" (4 bytes, exact flush) = Core signaling Launcher to TerminateSelf immediately.
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    RedirectStandardInput  = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding  = System.Text.Encoding.UTF8,
                    // CreateNoWindow=true: prevents console window creation for ALL Core spawns.
                    // Together with all-streams-redirected, Core has zero ConPTY handles → no LPC init.
                    CreateNoWindow         = true,
                }
            };
            // File-based handshake: Core creates .ready BEFORE TerminateProcess (file visible immediately).
            // Launcher polls for .ready, reads relay file while Core is alive, then creates .ack.
            // Core waits for .ack (max 500ms), then calls TerminateProcess.
            // No named kernel objects — avoids ConPTY/session namespace issues.

            // Pass relay file path to Core via env var (one-shot only)
            if (relayFilePath != null)
                proc.StartInfo.EnvironmentVariables["WKAPPBOT_RELAY_FILE"] = relayFilePath;

            // Strip MSYS2/Cygwin PTY env vars for ALL Core spawns (not just fast-exit).
            // .NET 8 single-file AppHost deadlocks when TERM/MSYSTEM/MSYS/etc. are set,
            // regardless of CreateNoWindow mode. These vars tell the AppHost to reconcile
            // PTY handles at startup, causing an LPC deadlock before Main() is reached.
            foreach (var v in new[] { "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL",
                                      "ConEmuANSI", "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST",
                                      "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE" })
                proc.StartInfo.EnvironmentVariables.Remove(v);

            // Forward all args as-is
            foreach (var a in args)
                proc.StartInfo.ArgumentList.Add(a);


            _lDiagStep = "proc-starting";
            Prof("proc.Start()");
            // Detach Launcher from ConPTY console BEFORE spawning Core.
            // CREATE_NO_WINDOW still attaches Core to parent's ConPTY console session → LPC deadlock
            // in .NET 8 AppHost during console init (SetConsoleMode → csrss.exe LPC → never returns).
            // FreeConsole(): Launcher releases its console → child inherits nothing → no LPC deadlock.
            // Stdout/stderr file handles (ConPTY slave) remain valid for WriteFile after FreeConsole().
            // Make Launcher's stdout/stderr non-inheritable before spawning Core.
            // If bash's pipe write end is inheritable, Core inherits it → bash waits 30s for Core to die after Launcher exits.
            try { var h = GetStdHandle(-11); if (h != IntPtr.Zero && h != (IntPtr)(-1)) SetHandleInformation(h, 0x1, 0); } catch { }
            try { var h = GetStdHandle(-12); if (h != IntPtr.Zero && h != (IntPtr)(-1)) SetHandleInformation(h, 0x1, 0); } catch { }
            FreeConsole();
            proc.Start();
            // Close stdin immediately — Core doesn't read stdin interactively.
            // Stdin is always redirected (pipe) so Core never inherits a ConPTY stdin handle.
            try { proc.StandardInput.Close(); } catch { }
            _lDiagStep = $"proc-waitforexit(pid={proc.Id})";
            Prof($"proc.WaitForExit pid={proc.Id}");

            if (useFastExit)
            {
                // stdin already closed above.
                // Relay Core's stdout and stderr to Launcher's stdout/stderr in background.
                // Write bytes directly to the underlying stdout stream.
                // If terminal is CP949 (Korean CMD), wrap with TranscodeStream: UTF-8 → CP949 on the fly.
                var rawStdout = Console.OpenStandardOutput();
                var rawStderr = Console.OpenStandardError();
                Stream stdoutStream = _needsTranscode
                    ? new TranscodeStream(rawStdout, _consoleCodePage)
                    : rawStdout;
                var stderrStream = rawStderr; // stderr is ASCII diagnostics; no transcode needed
                var stdoutRelay = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new byte[4096];
                        int n;
                        while ((n = proc.StandardOutput.BaseStream.Read(buf, 0, buf.Length)) > 0)
                        {
                            stdoutStream.Write(buf, 0, n);
                            stdoutStream.Flush();
                        }
                        Console.Out.Flush();
                    }
                    catch { }
                });
                var stderrRelay = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new byte[4096];
                        int n;
                        while ((n = proc.StandardError.BaseStream.Read(buf, 0, buf.Length)) > 0)
                        {
                            stderrStream.Write(buf, 0, n);
                            stderrStream.Flush();
                        }
                    }
                    catch { }
                });
                // Core calls FastExit (TerminateProcess) after flushing help output.
                // If it doesn't exit within timeout, kill it (safety net for hangs).
                bool exited = proc.WaitForExit(fastExitTimeoutMs);
                _lDiagStep = exited ? "fast-exit-ok" : "fast-exit-timeout-kill";
                Prof(exited
                    ? $"fast-exit: core exited naturally pid={proc.Id}"
                    : $"fast-exit: timeout {fastExitTimeoutMs}ms — kill core pid={proc.Id}");
                if (!exited)
                    try { proc.Kill(entireProcessTree: true); } catch { }
                // Wait for relay to flush all output before Launcher exits
                System.Threading.Tasks.Task.WhenAll(stdoutRelay, stderrRelay).Wait(500);
                stdoutStream.Flush();
                rawStdout.Flush();
                _lDiagStep = "TerminateSelf";
                TerminateSelf(0);
                return 0; // unreachable
            }

            // Stdout-relay path (grap/grep with args):
            // Fast path: named pipe — Core closes pipe early, Launcher gets EOF quickly.
            // Fallback: relay file (WKAPPBOT_RELAY_FILE).
            // Follow mode: Core output goes directly to the terminal (Ctrl+C to stop).
            if (useStdoutRelay)
            {
                if (relayFilePath != null)
                {
                    // File-based fast path: poll for .ready (Core creates it BEFORE TerminateProcess).
                    // File created by live process → visible to GetFileAttributesW immediately.
                    var readyPath = relayFilePath + ".ready";
                    var ackPath   = relayFilePath + ".ack";
                    Prof($"relay: polling .ready path={readyPath}");
                    var readyDeadline = _sw.ElapsedMilliseconds + 5000;
                    bool readyFound = false;
                    while (_sw.ElapsedMilliseconds < readyDeadline)
                    {
                        if (GetFileAttributesW(readyPath) != 0xFFFFFFFF) { readyFound = true; break; }
                        System.Threading.Thread.Sleep(5);
                    }
                    Prof(readyFound ? $"relay: .ready found pid={proc.Id}" : $"relay: .ready timeout pid={proc.Id}");
                    if (readyFound)
                    {
                        try
                        {
                            var content = System.IO.File.ReadAllText(relayFilePath, System.Text.Encoding.UTF8);
                            Console.Out.Write(content);
                            Console.Out.Flush();
                            Prof($"relay: {content.Length} chars written");
                        }
                        catch (Exception ex) { Prof($"relay: read error: {ex.Message}"); }
                        finally
                        {
                            try { System.IO.File.WriteAllText(ackPath, "1"); } catch { } // signal Core: done
                            try { System.IO.File.Delete(relayFilePath); } catch { }
                            try { System.IO.File.Delete(readyPath); } catch { }
                            try { System.IO.File.Delete(ackPath); } catch { }
                        }
                        TerminateSelf(0);
                        return 0; // unreachable
                    }
                    Prof("relay: .ready timeout → killing Core, no output");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    try { System.IO.File.Delete(relayFilePath); } catch { }
                    try { System.IO.File.Delete(readyPath); } catch { }
                    Prof($"relay: done (timeout, {_sw.ElapsedMilliseconds}ms)");
                    return 0;
                }
                else
                {
                    // Follow mode: relay stdout+stderr in background, wait for Ctrl+C / natural exit.
                    Prof("relay-follow: Core running (Ctrl+C to stop)");
                    Stream fStdout = _needsTranscode
                        ? new TranscodeStream(Console.OpenStandardOutput(), _consoleCodePage)
                        : Console.OpenStandardOutput();
                    var fStderr = Console.OpenStandardError();
                    var fOut = System.Threading.Tasks.Task.Run(() => {
                        try { var b = new byte[4096]; int n;
                              while ((n = proc.StandardOutput.BaseStream.Read(b,0,b.Length)) > 0) { fStdout.Write(b,0,n); fStdout.Flush(); } } catch { }
                    });
                    var fErr = System.Threading.Tasks.Task.Run(() => {
                        try { var b = new byte[4096]; int n;
                              while ((n = proc.StandardError.BaseStream.Read(b,0,b.Length)) > 0) { fStderr.Write(b,0,n); fStderr.Flush(); } } catch { }
                    });
                    proc.WaitForExit();
                    System.Threading.Tasks.Task.WhenAll(fOut, fErr).Wait(500);
                    Prof($"relay-follow: Core exited code={proc.ExitCode}");
                    return proc.ExitCode;
                }
            }

            // Normal path: relay Core's stdout/stderr to Launcher's stdout/stderr in real-time.
            // Sentinel \0UIT on stderr (control signal) → TerminateSelf immediately.
            // Also buffer last 20 stderr lines + capture [LOG] path for fallback errors.jsonl.
            // Raw-byte read preserves sentinel detection; text accumulation handles line extraction.
            var stderrLastLines = new System.Collections.Generic.Queue<string>(); // ring buffer last 20 lines
            string? capturedLogPath = null;
            var stderrLock = new object();
            _ = System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var rawStderr = Console.OpenStandardError();
                    var sentinel = new byte[] { 0, (byte)'U', (byte)'I', (byte)'T' };
                    var buf = new byte[4096];
                    var lineAcc = new System.Text.StringBuilder();
                    int n;
                    while ((n = proc.StandardError.BaseStream.Read(buf, 0, buf.Length)) > 0)
                    {
                        // Sentinel check must happen on raw bytes before any encoding
                        if (n == 4 && buf[0] == sentinel[0] && buf[1] == sentinel[1]
                                   && buf[2] == sentinel[2] && buf[3] == sentinel[3])
                        {
                            Prof($"relay: \\0UIT sentinel (stderr) → TerminateSelf");
                            try { Console.Out.Flush(); CloseHandle(GetStdHandle(-11)); } catch { }
                            var ec = proc.WaitForExit(500) ? proc.ExitCode : 0;
                            TerminateSelf((uint)ec);
                            return;
                        }
                        // Relay to terminal immediately (real-time)
                        rawStderr.Write(buf, 0, n); rawStderr.Flush();
                        // Accumulate text for line extraction (last-lines ring buffer + [LOG] capture)
                        lineAcc.Append(System.Text.Encoding.UTF8.GetString(buf, 0, n));
                        var s = lineAcc.ToString();
                        var nl = s.LastIndexOf('\n');
                        if (nl >= 0)
                        {
                            var complete = s[..nl];
                            lineAcc.Clear(); lineAcc.Append(s[(nl + 1)..]);
                            foreach (var raw in complete.Split('\n'))
                            {
                                var line = raw.TrimEnd('\r');
                                lock (stderrLock)
                                {
                                    if (line.StartsWith("[LOG] ", StringComparison.Ordinal))
                                        capturedLogPath = line[6..].Trim();
                                    stderrLastLines.Enqueue(line);
                                    if (stderrLastLines.Count > 20) stderrLastLines.Dequeue();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER] stderr relay error: {ex.Message}"); }
            });
            Stream _stdout = _needsTranscode
                ? new TranscodeStream(Console.OpenStandardOutput(), _consoleCodePage)
                : Console.OpenStandardOutput();
            int effectiveTimeoutMs = timeoutSec > 0 ? timeoutSec * 1000 : 0;
            var relayBuf = new byte[65536];
            long stdoutBytesWritten = 0;
            while (true)
            {
                int n;
                try { n = proc.StandardOutput.BaseStream.Read(relayBuf, 0, relayBuf.Length); }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[LAUNCHER] stdout pipe error: {ex.GetType().Name}: {ex.Message}");
                    break;
                }
                if (n == 0) break; // EOF — Core stdout closed (normal or crash)

                // Timeout check (--timeout N flag)
                if (effectiveTimeoutMs > 0 && _sw.ElapsedMilliseconds > effectiveTimeoutMs)
                {
                    Console.Error.WriteLine($"[LAUNCHER] timeout {timeoutSec}s exceeded — killing core (pid={proc.Id})");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    try { _stdout.Flush(); } catch { }
                    TerminateSelf((uint)timeoutExit);
                    return timeoutExit; // unreachable
                }

                try { _stdout.Write(relayBuf, 0, n); _stdout.Flush(); stdoutBytesWritten += n; }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[LAUNCHER] stdout write error: {ex.GetType().Name}: {ex.Message}");
                    break;
                }
            }
            try { _stdout.Flush(); } catch { }
            // WaitForExit with timeout: stdout EOF doesn't guarantee Core exited (e.g. crash mid-output).
            if (!proc.HasExited && !proc.WaitForExit(5000))
            {
                Console.Error.WriteLine($"[LAUNCHER] Core still alive 5s after stdout EOF — killing (pid={proc.Id})");
                try { proc.Kill(entireProcessTree: false); } catch { }
                proc.WaitForExit(1000);
            }

            _lDiagStep = $"proc-exited(code={proc.ExitCode})";
            Prof($"proc exited code={proc.ExitCode}");
            int coreExitCode = proc.ExitCode;
            // Empty stdout + non-zero exit: Core failed silently (crash before output, or ErrorScope
            // swallowed everything). Warn so the caller isn't left with just a bare exit code.
            if (coreExitCode != 0 && stdoutBytesWritten == 0)
                Console.Error.WriteLine($"[LAUNCHER] Core exited {coreExitCode} with no stdout — check stderr log above");
            // Fallback errors.jsonl: write from Launcher when Core exits non-zero.
            // Core's TeeTextWriter normally handles this, but crashes before TeeWriter setup are silent.
            // Launcher's entry uses raw last-20 stderr lines — always a useful safety net.
            if (coreExitCode != 0)
            {
                try { AppendLauncherErrorRecord(coreExitCode, args, capturedLogPath, stderrLastLines, stderrLock); }
                catch { /* best-effort */ }
            }
            Console.Out.Flush();
            Console.Error.Flush();
            // TerminateSelf: Core's output already written (inherited handles); hard-kill Launcher immediately.
            TerminateSelf((uint)coreExitCode);
            return coreExitCode; // unreachable
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] Failed to run core: {ex.Message}");
            return 1;
        }
    }

    /// <summary>
    /// Fallback errors.jsonl writer — runs in Launcher when Core exits non-zero.
    /// Core's TeeTextWriter normally writes this, but if Core crashed before TeeWriter
    /// was set up, this is the only record. Uses last 20 stderr lines as evidence.
    /// </summary>
    static void AppendLauncherErrorRecord(int exitCode, string[] args, string? logPath,
        System.Collections.Generic.Queue<string> stderrLines, object lockObj)
    {
        // Determine logs directory: from captured [LOG] path, or default SDK/bin/wkappbot.hq/logs/
        string? logsDir = null;
        if (logPath != null)
            logsDir = Path.GetDirectoryName(logPath);
        if (string.IsNullOrEmpty(logsDir))
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
            logsDir = Path.Combine(exeDir, "wkappbot.hq", "logs");
        }
        Directory.CreateDirectory(logsDir);

        string[] lastLines;
        lock (lockObj) lastLines = stderrLines.ToArray();

        var cmd = string.Join(" ", args);
        if (cmd.Length > 300) cmd = cmd[..300];
        var ts = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        var lastSnippet = string.Join(" | ", lastLines.TakeLast(5).Select(l => l.Length > 150 ? l[..150] : l));
        // Escape for JSON
        lastSnippet = lastSnippet.Replace("\\", "\\\\").Replace("\"", "\\\"");
        var logField = logPath != null ? $",\"log\":\"{logPath.Replace("\\", "\\\\")}\"" : "";
        var record = $"{{\"ts\":\"{ts}\",\"exit\":{exitCode},\"source\":\"launcher\",\"cmd\":\"{cmd.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"{logField},\"fields\":{{\"_stderr\":\"{lastSnippet}\"}}}}";

        var errorsFile = Path.Combine(logsDir, "errors.jsonl");
        // Append atomically (best-effort)
        using var fs = new FileStream(errorsFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        sw.WriteLine(record);
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

}
