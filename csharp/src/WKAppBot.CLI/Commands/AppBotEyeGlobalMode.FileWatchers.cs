using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static void InitFileWatchers()
    {
        // -- 1. Tick file watcher (eye_ticks.jsonl) --
        try
        {
            var tickPath = EyeTicksPath;
            var tickDir = Path.GetDirectoryName(tickPath);
            var tickFile = Path.GetFileName(tickPath);
            if (tickDir != null && Directory.Exists(tickDir))
            {
                _fswTick = new FileSystemWatcher(tickDir)
                {
                    Filter = tickFile,
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fswTick.Changed += (_, _) => _fswTickDirty = true;
                _fswTick.Created += (_, _) => _fswTickDirty = true;
                Console.Error.WriteLine($"[EYE][FSW] Tick watcher: {tickDir}/{tickFile}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE][FSW] Tick watcher init failed: {ex.Message}");
        }

        // -- 2. OpenClaw sessions watcher (*.jsonl) --
        try
        {
            var sessionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".openclaw", "agents", "main", "sessions");
            if (Directory.Exists(sessionsDir))
            {
                _fswPrompt = new FileSystemWatcher(sessionsDir)
                {
                    Filter = "*.jsonl",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                _fswPrompt.Changed += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                _fswPrompt.Created += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                _fswPrompt.Renamed += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                Console.Error.WriteLine($"[EYE][FSW] Prompt watcher: {sessionsDir}/*.jsonl");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE][FSW] Prompt watcher init failed: {ex.Message}");
        }

        // Claude Code JSONL: 1s polling only -- FSW removed (per-instance watermark sufficient)

        // -- 4. EXE file watcher (hot-swap: instant binary change detection) --
        try
        {
            var selfExe = Environment.ProcessPath ?? "";
            var exeDir = Path.GetDirectoryName(selfExe);
            var exeFile = Path.GetFileName(selfExe);
            if (exeDir != null && Directory.Exists(exeDir) && !string.IsNullOrEmpty(exeFile))
            {
                _fswExe = new FileSystemWatcher(exeDir)
                {
                    Filter = exeFile,
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.CreationTime,
                    EnableRaisingEvents = true
                };
                _fswExe.Changed += (_, _) => _fswExeDirty = true;
                _fswExe.Created += (_, _) => _fswExeDirty = true;
                // Also watch for .new.exe (staged swap)
                _fswExeNew = new FileSystemWatcher(exeDir)
                {
                    Filter = Path.GetFileNameWithoutExtension(exeFile) + ".new.exe",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fswExeNew.Changed += (_, _) => _fswExeDirty = true;
                _fswExeNew.Created += (_, _) => _fswExeDirty = true;
                // Startup: .new.exe may already exist (pre-dated swap from before restart)
                var newExeOnStart = Path.Combine(exeDir, Path.GetFileNameWithoutExtension(exeFile) + ".new.exe");
                if (File.Exists(newExeOnStart))
                {
                    Console.WriteLine("[EYE][FSW] .new.exe already present at startup -- triggering hot-swap");
                    _fswExeDirty = true;
                }
                Console.Error.WriteLine($"[EYE][FSW] Hot-swap watcher: {exeDir}/{exeFile}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE][FSW] Hot-swap watcher init failed: {ex.Message}");
        }

        // -- 4. 앱봇관리 log file watcher (temp dir) --
        // Eye/MCP가 wkappbot-*.log 생성 -> Eye가 감지해서 apbot-mcp WT 탭 자동 오픈
        try
        {
            var tempDir = Path.GetTempPath();
            // Eye 재시작 시 기존 로그 파일 처리:
            // - PID 살아있는 것 -> 탭 오픈
            // - 죽은 세션 로그 -> 삭제 (탭 폭발 방지)
            foreach (var f in Directory.GetFiles(tempDir, "wkappbot-*.log"))
            {
                var stem  = Path.GetFileNameWithoutExtension(f);
                var parts = stem.Split('-');
                // wkappbot-eye-{pid} or wkappbot-mcp-{pid} -> 마지막 파트가 숫자이면 PID
                if (int.TryParse(parts[^1], out var logPid))
                {
                    bool alive = false;
                    try { using var p = Process.GetProcessById(logPid); alive = !p.HasExited; } catch { }
                    if (!alive) { try { File.Delete(f); } catch { } continue; }
                }
                EyeOpenConsoleWtTab(f);
            }

            _fswMcp = new FileSystemWatcher(tempDir)
            {
                Filter                = "wkappbot-*.log",
                IncludeSubdirectories = false,
                NotifyFilter          = NotifyFilters.FileName | NotifyFilters.CreationTime,
                EnableRaisingEvents   = true
            };
            _fswMcp.Created += (_, e) =>
            {
                if (e.FullPath != null)
                    Task.Run(() => EyeOpenConsoleWtTab(e.FullPath));
            };
            Console.Error.WriteLine($"[EYE][FSW] Console watcher: {tempDir}wkappbot-*.log");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE][FSW] Console watcher init failed: {ex.Message}");
        }
    }

    /// <summary>
    /// 앱봇관리 로그 파일 생성 감지 시 apbot-mcp WT 탭 오픈.
    /// 파일명 규칙:
    ///   wkappbot-eye-{pid}.log              -> [앱봇관리] Eye
    ///   wkappbot-mcp-{session}.log          -> [앱봇관리] MCP
    ///   wkappbot-mcp-tool-{name}-{id}.log   -> [앱봇관리] {name} #{id}
    /// </summary>
    static void EyeOpenConsoleWtTab(string logFilePath)
    {
        // wt.exe 자동 탭 오픈 비활성화 -- 포커스 간섭 없이 필요할 때 수동으로 열어서 사용
        return;
#pragma warning disable CS0162 // unreachable (intentionally disabled, restore when re-enabling wt auto-tab)
        try
        {
            var fileName = Path.GetFileNameWithoutExtension(logFilePath);
            lock (_mcpTabsOpened)
            {
                if (!_mcpTabsOpened.Add(fileName)) return; // 이미 탭 열었음
            }

            string wtTitle;
            int watchPid = 0;
            if (fileName.StartsWith("wkappbot-eye-", StringComparison.Ordinal))
                wtTitle = "앱봇아이";
            else
            {
                // wkappbot-mcp-{sessionPid}.log -> 대화명 조회
                var sessionPart = fileName["wkappbot-mcp-".Length..];
                if (int.TryParse(sessionPart, out var sessionPid))
                {
                    wtTitle   = ResolveDisplayNameByPid(sessionPid);
                    watchPid  = sessionPid;
                }
                else wtTitle = "MCP";
            }

            // 프로세스 종료 감시 -- 킬당해도 로그에 찍힘
            if (watchPid > 0)
            {
                var logPath = logFilePath;
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var p = Process.GetProcessById(watchPid);
                        await p.WaitForExitAsync();
                        var msg = p.ExitCode == 0
                            ? "[MCP] Server stopped (stdin EOF)"
                            : $"[MCP] 강제 종료됨 (exit={p.ExitCode}) ㅋ";
                        await File.AppendAllTextAsync(logPath, msg + "\n");
                    }
                    catch { }
                });
            }

            // 이미 같은 로그파일을 감시 중인 powershell.exe가 있으면 탭 중복 방지
            if (IsAlreadyWatchingLog(logFilePath))
            {
                Console.Error.WriteLine($"[EYE][CONSOLE] 이미 탭 열려있음 (PS 감시중): {wtTitle}");
                return;
            }

            var logEsc = logFilePath.Replace("'", "''");
            var psCmd  = $"Get-Content -Wait -Path '{logEsc}'";
            // [FOCUS-GUARD] GuardedStart: wt.exe (Windows Terminal) 실행 -- 포커스 강탈 자동 감지+복원
            var wtProc = WKAppBot.Win32.Input.ProcessLaunchGuard.GuardedStart(new ProcessStartInfo
            {
                FileName        = "wt.exe",
                Arguments       = $"-w {McpWtWindowName} new-tab --title \"{wtTitle}\" -- powershell -NoProfile -NonInteractive -NoExit -Command \"{psCmd}\"",
                UseShellExecute = true,
            }, "AppBotEye:wt");
            // 첫 탭 or apbot-mcp 창 없을 때만 위치 고정
            ScheduleWtWindowPosition(900, wtProc?.Id ?? 0);
            Console.Error.WriteLine($"[EYE][CONSOLE] WT 탭 오픈: {wtTitle}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE][CONSOLE] WT 탭 오픈 실패: {ex.Message}");
        }
    }

    /// <summary>WMI로 powershell.exe 중 같은 로그파일을 Get-Content -Wait 중인 프로세스가 있는지 확인.</summary>
    static bool IsAlreadyWatchingLog(string logFilePath)
    {
        try
        {
            var normalized = logFilePath.Replace("\\", "/").ToLowerInvariant();
            using var searcher = new System.Management.ManagementObjectSearcher(
                "SELECT CommandLine FROM Win32_Process WHERE Name='powershell.exe'");
            foreach (System.Management.ManagementObject mo in searcher.Get())
            {
                var cmd = mo["CommandLine"]?.ToString() ?? "";
                if (cmd.Replace("\\", "/").IndexOf(normalized, StringComparison.OrdinalIgnoreCase) >= 0
                    && cmd.Contains("Get-Content", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }
        catch { }
        return false;
    }

    static void TryDeleteOldExes()
    {
        try
        {
            var myExe = Environment.ProcessPath ?? "";
            var exeDir = Path.GetDirectoryName(myExe) ?? "";
            var exeName = Path.GetFileNameWithoutExtension(myExe);
            // Delete all timestamped old exes: wkappbot.old-*.exe
            foreach (var oldExe in Directory.GetFiles(exeDir, $"{exeName}.old-*.exe"))
            {
                try
                {
                    File.Delete(oldExe);
                    Console.Error.WriteLine($"[EYE:HOT-SWAP] Cleaned up {Path.GetFileName(oldExe)}");
                }
                catch { } // still locked -- 10s polling will retry
            }
        }
        catch { }
    }

    static void DisposeFileWatchers()
    {
        try { if (_fswTick != null)   { _fswTick.EnableRaisingEvents   = false; _fswTick.Dispose();   _fswTick   = null; } } catch { }
        try { if (_fswPrompt != null) { _fswPrompt.EnableRaisingEvents = false; _fswPrompt.Dispose(); _fswPrompt = null; } } catch { }
        try { if (_fswExe != null)    { _fswExe.EnableRaisingEvents    = false; _fswExe.Dispose();    _fswExe    = null; } } catch { }
        try { if (_fswExeNew != null) { _fswExeNew.EnableRaisingEvents = false; _fswExeNew.Dispose(); _fswExeNew = null; } } catch { }
        try { if (_fswMcp != null)    { _fswMcp.EnableRaisingEvents    = false; _fswMcp.Dispose();    _fswMcp    = null; } } catch { }
        Console.WriteLine("[EYE][FSW] Watchers disposed");
    }


    // EyeTickCommand + BuildIpcTickResponse -> AppBotEyeTickCommand.cs

    // EyeTickForwardSlackInbox + EyeTickCheckThreadReplies -> AppBotEyeSlackMonitor.cs

    // BuildEyeSummary + AbbreviateCwd + CardCache + GetRightmostMonitorAnchor -> AppBotEyeCardBuilder.cs

    // ReadLatestTick + ReadLatestOpenClawPromptPreview + TryExtractRoleAndText + NormalizePrompt
    // CompressPlanTitle + ExtractPlanOutlineItems + ExtractRecentPlanItems
    // ReadLatestActionTriplet + ReadTailLinesShared -> AppBotEyeJsonlParser.cs

    // _idleSkipCommands + GetClaudeCodeSessionAge + GetLastTickTag + BuildKroStatus3
    // IsMetaTag + CheckAndReportDeadCards + SupplementCardsFromPrompts + ReadEyeCards -> AppBotEyeHealthCheck.cs

#pragma warning restore CS0162
}
