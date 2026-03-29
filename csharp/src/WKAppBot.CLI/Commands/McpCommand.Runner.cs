using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WKAppBot.CLI;

internal partial class Program
{
    static (string output, int exitCode) RunCliCaptureWithCode(string command, string[] args, Action<string>? onOutputLine = null)
    {
        var capture = new LineCaptureWriter(onOutputLine);
        var prevOut = Console.Out;
        var prevErr = Console.Error;
        Console.SetOut(capture);
        Console.SetError(capture);

        int exitCode = 0;
        try
        {
            exitCode = command switch
            {
                "inspect" => InspectCommand(args),
                "a11y" => A11yCommand(args),
                "windows" => WindowsCommand(args),
                "web" => WebCommand(args),
                "ocr" => OcrCommand(args),
                "logcat" => LogcatCommand(args),
                "prompt-probe" => PromptProbeCommand(args),
                "claude-detect" => ClaudeDetectCommand(args),
                "find-prompts" => FindPromptsCommand(args),
                _ => -1
            };
        }
        catch (Exception ex)
        {
            capture.WriteLine($"Error: {ex.Message}");
            exitCode = 1;
        }
        finally
        {
            capture.FlushPartialLine();
            Console.SetOut(prevOut);
            Console.SetError(prevErr);
        }

        return (capture.GetAllText().Trim(), exitCode);
    }

    static (string output, int exitCode) RunCliCaptureWithCodeExternal(string command, string[] args, Action<string>? onOutputLine = null)
    {
        var exe = Environment.ProcessPath;
        if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
            return ($"Error: process path unavailable for out-of-proc run ({exe})", 1);

        // Propagate callerCwd to subprocess: set WorkingDirectory + env var
        // so EmitEyeTick in the child process uses the correct CWD for card display.
        var callerCwd = EyeCmdPipeServer.CallerCwd.Value;
        var psi = new ProcessStartInfo(exe)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
            WorkingDirectory = callerCwd ?? Path.GetDirectoryName(exe) ?? ".",
        };
        psi.ArgumentList.Add(command);
        foreach (var arg in args) psi.ArgumentList.Add(arg);

        var sb = new StringBuilder();
        object gate = new();
        int pushedPid = 0; // child PID — set after proc.Start()

        void PushLine(string line)
        {
            if (line == null) return;
            lock (gate) { sb.AppendLine(line); }
            // AsyncLocal 사이드채널: EmitToolProgress가 읽어서 progress 알림에 메트릭 포함
            _currentProgressMeta.Value = pushedPid > 0 ? SampleProcessMetrics(pushedPid) : null;
            onOutputLine?.Invoke(line);
            _currentProgressMeta.Value = null;
        }

        async Task PumpAsync(StreamReader reader)
        {
            while (true)
            {
                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null) break;
                PushLine(line);
            }
        }

        try
        {
            using var proc = Process.Start(psi);
            if (proc == null) return ("Error: failed to start child process", 1);

            pushedPid = proc.Id; // PushLine이 메트릭 샘플링에 사용
            var outTask = PumpAsync(proc.StandardOutput);
            var errTask = PumpAsync(proc.StandardError);

            using var watchdogCts = new CancellationTokenSource();
            var watchdogTask = McpToolWaitWatchdog(proc.Id, command, onOutputLine, watchdogCts.Token);

            proc.WaitForExit();
            watchdogCts.Cancel();
            try { watchdogTask.Wait(500); } catch { }
            Task.WaitAll(outTask, errTask);

            return (sb.ToString().Trim(), proc.ExitCode);
        }
        catch (Exception ex)
        {
            return ($"Error: out-of-proc execution failed: {ex.Message}", 1);
        }
    }

    /// <summary>
    /// MCP 툴 프로세스 블로킹 대기 감지 워치독.
    /// ProcessThread.ThreadState/WaitReason API 사용 (NtQuerySystemInformation 불필요).
    /// 모든 스레드가 Wait 상태일 때 MCP 콜러에 즉시 통보 + 콘솔 출력 + Slack 알림.
    ///
    /// WaitReason별 태그:
    ///   UserRequest  → [WAIT_INPUT]  (stdin/콘솔 읽기 대기 — e.g. cmd.exe 무인수 실행)
    ///   LpcReply     → [WAIT_IPC]    (IPC 대기)
    ///   Executive    → [WAIT_KERNEL] (커널 객체 대기)
    ///   기타         → [WAIT_OTHER]
    /// </summary>
    static async Task McpToolWaitWatchdog(int pid, string cmdLabel, Action<string>? onOutputLine, CancellationToken cancel)
    {
        var reported = new HashSet<string>(StringComparer.Ordinal);
        // 프로세스 시작 직후 안정화 대기
        try { await Task.Delay(1000, cancel).ConfigureAwait(false); } catch (OperationCanceledException) { return; }

        bool isBlocking = false;
        while (!cancel.IsCancellationRequested)
        {
            try
            {
                using var p = Process.GetProcessById(pid);
                if (p.HasExited) break;

                var threads = p.Threads.Cast<ProcessThread>().ToList();
                if (threads.Count == 0) break;

                var waitReasons = new List<ThreadWaitReason>();
                foreach (var t in threads)
                {
                    try
                    {
                        if (t.ThreadState == System.Diagnostics.ThreadState.Wait)
                            waitReasons.Add(t.WaitReason);
                    }
                    catch { }
                }

                if (waitReasons.Count > 0 && waitReasons.Count >= threads.Count - 1)
                {
                    isBlocking = true;
                    var dominant = waitReasons
                        .GroupBy(r => r)
                        .OrderByDescending(g => g.Count())
                        .First().Key;

                    var (tag, statusVal, desc) = dominant switch
                    {
                        ThreadWaitReason.UserRequest    => ("[WAIT_INPUT]", "wait_input",  "stdin/콘솔 입력 대기 (cmd.exe 등)"),
                        ThreadWaitReason.LpcReply       => ("[WAIT_IPC]",   "wait_ipc",   "IPC 응답 대기"),
                        ThreadWaitReason.LpcReceive     => ("[WAIT_IPC]",   "wait_ipc",   "IPC 수신 대기"),
                        ThreadWaitReason.Executive      => ("[WAIT_LOCK]",  "wait_lock",  "동기화 객체 대기 (mutex/event/semaphore)"),
                        ThreadWaitReason.Suspended      => ("[WAIT_SUSP]",  "suspended",  "일시정지됨"),
                        ThreadWaitReason.PageIn         => ("[WAIT_IO]",    "wait_io",    "디스크 I/O 대기"),
                        ThreadWaitReason.ExecutionDelay => ("[WAIT_SLEEP]", "sleeping",   "sleep/delay 대기"),
                        _                               => ("[WAIT_OTHER]", "waiting",    $"대기중 ({dominant})")
                    };

                    if (reported.Add(tag))
                    {
                        var msg = $"{tag} 툴 프로세스 블로킹! {desc} — pid={pid} cmd={cmdLabel}";
                        Console.Error.WriteLine($"[MCP] ⚠ {msg}");
                        onOutputLine?.Invoke($"\x00{statusVal}\x00⚠ {msg}");
                    }
                }
                else
                {
                    if (isBlocking)
                    {
                        // 블로킹 해제 → 재개 알림 + 재알림 허용
                        isBlocking = false;
                        reported.Clear();
                        onOutputLine?.Invoke($"\x00running\x00[RESUMED] 툴 프로세스 재개됨 — pid={pid}");
                    }
                }
            }
            catch (ArgumentException) { break; }
            catch { }

            // 블로킹 중: 50ms 거의 실시간 / 정상: 500ms
            var delay = isBlocking ? 50 : 500;
            try { await Task.Delay(delay, cancel).ConfigureAwait(false); } catch (OperationCanceledException) { break; }
        }
    }

    static string RunCliCapture(string command, string[] args)
    {
        var (output, _) = RunCliCaptureWithCode(command, args);
        return output;
    }

    static string RunCliCaptureScreenshot(JsonObject args)
    {
        var grap = args["grap"]?.GetValue<string>();
        var tempFile = Path.Combine(Path.GetTempPath(), $"mcp_screenshot_{Guid.NewGuid():N}.png");

        try
        {
            var cliArgs = new List<string>();
            if (!string.IsNullOrEmpty(grap)) cliArgs.Add(grap);
            cliArgs.Add("-o");
            cliArgs.Add(tempFile);

            var sw = new StringWriter();
            var prevOut = Console.Out;
            Console.SetOut(sw);
            try { CaptureCommand(cliArgs.ToArray()); }
            finally { Console.SetOut(prevOut); }

            if (File.Exists(tempFile))
            {
                var bytes = File.ReadAllBytes(tempFile);
                return $"Screenshot saved ({bytes.Length} bytes). Base64 PNG:\n{Convert.ToBase64String(bytes)}";
            }
            return "Screenshot failed: no output file.";
        }
        finally
        {
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }

    // ────────────────────────────────────────────────────────────────
    // MCP 앱봇관리 콘솔 호스트
    // ────────────────────────────────────────────────────────────────

    // 콘솔 호스트 고정 위치: 맨오른쪽 모니터 우측경계 - 1610, 모니터 상측 + 10, 800x600
    const int McpWtOffsetFromRight = 1610;
    const int McpWtOffsetFromTop   = 10;
    const int McpWtWidth           = 800;
    const int McpWtHeight          = 600;

    // ── P/Invoke: 모니터 위치 + WT 창 찾기 ──────────────────────────
    [DllImport("ntdll.dll")]
    static extern int NtQueryInformationProcess(
        IntPtr hProcess, int processInfoClass,
        ref McpProcessBasicInfo pbi, int size, out int returnLen);

    [StructLayout(LayoutKind.Sequential)]
    struct McpProcessBasicInfo
    {
        IntPtr ExitStatus, PebBase, AffinityMask, BasePriority, UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    [DllImport("user32.dll")]
    static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
        McpMonitorEnumProc lpfnEnum, IntPtr dwData);
    delegate bool McpMonitorEnumProc(IntPtr hMon, IntPtr hdcMon, ref McpRect lprcMon, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern bool GetMonitorInfoW(IntPtr hMon, ref McpMonitorInfo lpmi);

    [StructLayout(LayoutKind.Sequential)]
    struct McpRect { public int left, top, right, bottom; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct McpMonitorInfo
    {
        public uint  cbSize;
        public McpRect rcMonitor;
        public McpRect rcWork;
        public uint  dwFlags;
    }

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int x, int y, int cx, int cy, uint uFlags);
    const uint SWP_NOACTIVATE = 0x0010;
    const uint SWP_NOZORDER   = 0x0004;

    // WT 메인 창 클래스 (GetClassNameW는 AppBotEyeCommands.cs에 이미 선언됨)
    const string WtWindowClass    = "CASCADIA_HOSTING_WINDOW_CLASS";
    // MCP 전용 WT 창 이름 (Eye 창과 완전 분리)
    const string McpWtWindowName  = "apbot-mcp";

    static int McpGetParentProcessId()
    {
        try
        {
            var pbi = new McpProcessBasicInfo();
            var status = NtQueryInformationProcess(
                Process.GetCurrentProcess().Handle, 0,
                ref pbi, Marshal.SizeOf<McpProcessBasicInfo>(), out _);
            return status == 0 ? (int)pbi.InheritedFromUniqueProcessId : -1;
        }
        catch { return -1; }
    }

    // EnumDisplayMonitors callback — delegate with ref parameter can't be a lambda
    static McpRect? _monitorEnumBest;
    static bool MonitorEnumCallback(IntPtr hMon, IntPtr hdcMon, ref McpRect lprc, IntPtr dwData)
    {
        var mi = new McpMonitorInfo { cbSize = (uint)Marshal.SizeOf<McpMonitorInfo>() };
        if (GetMonitorInfoW(hMon, ref mi))
        {
            if (_monitorEnumBest == null || mi.rcMonitor.right > _monitorEnumBest.Value.right)
                _monitorEnumBest = mi.rcMonitor;
        }
        return true;
    }

    /// <summary>맨오른쪽 모니터의 rcMonitor 반환. 없으면 null.</summary>
    static McpRect? GetRightmostMonitorRect()
    {
        _monitorEnumBest = null;
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumCallback, IntPtr.Zero);
        return _monitorEnumBest;
    }

    /// <summary>콘솔 호스트 고정 위치 (X,Y) 계산.</summary>
    static (int x, int y) GetMcpWtTargetPos()
    {
        var mon = GetRightmostMonitorRect();
        if (mon == null) return (0, 0);
        return (mon.Value.right - McpWtOffsetFromRight, mon.Value.top + McpWtOffsetFromTop);
    }

    /// <summary>
    /// WT 창 찾기 (CASCADIA_HOSTING_WINDOW_CLASS) → SetWindowPos(SWP_NOACTIVATE).
    /// 딜레이 후 비동기 실행 — 창이 뜨기 전에 호출하면 실패하므로.
    /// wtPid > 0 이면 해당 프로세스 소유 창만 이동 (Eye WT 창 오이동 방지).
    /// </summary>
    static void ScheduleWtWindowPosition(int delayMs = 900, int wtPid = 0)
    {
        var (targetX, targetY) = GetMcpWtTargetPos();
        _ = Task.Delay(delayMs).ContinueWith(_ =>
        {
            try
            {
                var sb   = new System.Text.StringBuilder(256);
                IntPtr found = IntPtr.Zero;

                // Pass 1: PID 기반 (새로 생성된 WT 창 정확히 매칭)
                if (wtPid > 0)
                {
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((hWnd, _) =>
                    {
                        sb.Clear(); GetClassNameW(hWnd, sb, 256);
                        if (sb.ToString() == WtWindowClass)
                        {
                            GetWindowThreadProcessId(hWnd, out int wPid);
                            if (wPid == wtPid) { found = hWnd; return false; }
                        }
                        return true;
                    }, IntPtr.Zero);
                }

                // Pass 2: fallback — wt.exe가 기존 창에 탭 추가 후 즉시 종료 시 PID 없음
                // → 임의의 WT 창 중 apbot-mcp 창(이미 존재)에 위치 적용
                if (found == IntPtr.Zero)
                {
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((hWnd, _) =>
                    {
                        sb.Clear(); GetClassNameW(hWnd, sb, 256);
                        if (sb.ToString() == WtWindowClass) { found = hWnd; return false; }
                        return true;
                    }, IntPtr.Zero);
                }

                if (found != IntPtr.Zero)
                    SetWindowPos(found, IntPtr.Zero, targetX, targetY, McpWtWidth, McpWtHeight,
                        SWP_NOACTIVATE | SWP_NOZORDER);
            }
            catch { }
        });
    }

    /// <summary>
    /// MCP 시작 시 앱봇관리 콘솔 탭 설정.
    /// - stderr → TeeTextWriter → log 파일 (FileShare.ReadWrite)
    /// - WT 탭으로 열기 (hot-swap 시 lock file로 중복 방지)
    /// - 열린 후 고정 위치로 이동 (맨오른쪽 모니터 기준)
    /// - --no-wt 플래그로 비활성화 가능
    /// </summary>
    static void TrySetupMcpConsoleHost(string[] args)
    {
        if (args.Contains("--no-wt")) return;
        try
        {
            var parentPid = McpGetParentProcessId();
            var sessionKey = parentPid > 0 ? parentPid.ToString() : Environment.ProcessId.ToString();
            var tmpDir   = Path.GetTempPath();
            var lockFile = Path.Combine(tmpDir, $"wkappbot-mcp-wt-{sessionKey}.lock");
            var logFile  = Path.Combine(tmpDir, $"wkappbot-mcp-{sessionKey}.log");

            // Tee Console.Error → real stderr + log file (항상, hot-swap 포함)
            var tee = new TeeTextWriter(Console.Error, logFile, moveToOldOnDispose: false);
            Console.SetError(tee);

            if (!File.Exists(lockFile))
            {
                File.WriteAllText(lockFile, sessionKey);
                // Eye FSW가 logFile 생성을 감지해서 WT 탭 자동 오픈
                Console.Error.WriteLine($"[MCP] 콘솔 로그 시작 → {logFile} (Eye가 WT 탭 오픈)");
            }
            else
            {
                Console.Error.WriteLine($"[MCP] hot-swap — 기존 콘솔 탭 재사용 (session={sessionKey})");
            }
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[MCP] 콘솔 호스트 설정 실패: {ex.Message}"); } catch { }
        }
    }

}
