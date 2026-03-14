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
    sealed class EyeParentCard
    {
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public string ParentTitle { get; set; } = "";
        public string LastTag { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public DateTime LastTsUtc { get; set; }
        public string Cwd { get; set; } = "";    // working directory for display
    }

    // ClaudeInstanceState + _instanceStates → moved to AppBotEyeClaudeStatusStreamer.cs

    sealed class PromptDiag
    {
        public long StatMs { get; set; }
        public long ReadMs { get; set; }
        public long ScanMs { get; set; }
        public long ParseMs { get; set; }
        public long NormMs { get; set; }
        public long CacheMs { get; set; }
        public string Source { get; set; } = "none";
        public DateTime FileWriteUtc { get; set; } = DateTime.MinValue; // session file mtime
    }

    static DateTime _lastTickActivityUtc = DateTime.MinValue;
    static DateTime _lastAiActivityUtc = DateTime.MinValue;
    static DateTime _lastAutoGogoUtc = DateTime.MinValue;
    static DateTime _lastKeepAwakeUtc = DateTime.MinValue;
    static string _lastPromptSource = "none";

    static System.Windows.Forms.Form? _screenBlankForm;

    static string _lastPromptSessionFile = "";
    static int _lastPromptLineIndex = -1;
    static int _lastContextPct = -1;  // context usage % for tick display
    static string _lastPromptPreviewCache = "";
    static long _lastPromptSessionLength = -1;
    static DateTime _lastPromptSessionWriteUtc = DateTime.MinValue;

    static string _lastEyeTickFile = "";
    static int _lastEyeTickLineIndex = -1;

    static string _lastPlanSessionFile = "";
    static int _lastPlanLineIndex = -1;
    static long _lastPlanSessionLength = -1;
    static DateTime _lastPlanSessionWriteUtc = DateTime.MinValue;
    static List<string> _lastPlanItemsCache = new();

    static EyeTick? _cachedLatestTick;
    static string _cachedPromptPreview = "";
    static DateTime _cachedPromptFileWriteUtc = DateTime.MinValue;
    static List<EyeParentCard> _cachedCards = new();

    // ── Dead card + health check ──
    static readonly HashSet<int> _reportedDeadPids = new();          // pids we've already alerted for
    static readonly Dictionary<int, string> _cardHealthCache = new(); // pid → "ok"/"slow"/"dead"
    static string? _eyeBotToken;    // set once Slack creds are loaded
    static string? _eyeChannel;
    static DateTime _lastForceFullLoadUtc = DateTime.MinValue;
    // Card cache: content + changedUtc per card, persisted to disk
    static string _cardCacheDir = "";
    static Dictionary<string, (string content, DateTime changedUtc)> _cardCache = new();
    static string _dirtyTickFile = "";
    static long _dirtyTickLength = -1;
    static DateTime _dirtyTickWriteUtc = DateTime.MinValue;
    static string _dirtyPromptFile = "";
    static long _dirtyPromptLength = -1;
    static DateTime _dirtyPromptWriteUtc = DateTime.MinValue;

    // ── FSW hybrid: event-driven dirty flags (set by FileSystemWatcher callbacks) ──
    static volatile bool _fswTickDirty;
    static volatile bool _fswPromptDirty;
    static volatile string? _fswPromptChangedFile; // last changed file name for filtering
    static volatile bool _fswExeDirty; // hot-swap: exe binary changed
    static FileSystemWatcher? _fswTick;
    static FileSystemWatcher? _fswPrompt;
    static FileSystemWatcher? _fswExe;
    static FileSystemWatcher? _fswExeNew; // hot-swap: .new.exe staged file

    // ── Memory tracking ──
    static long _prevWorkingSetMB;
    static long _peakWorkingSetMB;

    // Named mutex: signals GlobalMode Eye is running (no WPF window to detect otherwise)
    // LaunchAppBotEyeIfNeededCore checks this to prevent duplicate Eye spawns.
    static System.Threading.Mutex? _eyeRunMutex;

    // ── SupplementCardsFromPrompts: 1s cooldown after last scan ──
    // Per-hwnd cache in ClaudePromptHelper makes FindAllPrompts fast for known windows (no UIA rescan).
    // 1s cooldown avoids redundant EnumWindows calls in back-to-back ticks.
    static List<ClaudePromptHelper.PromptInfo>? _cachedAllPrompts;
    static DateTime _lastFindAllPromptsAt = DateTime.MinValue;

    // ── Eye IPC cache: updated each tick so eye tick IPC queries get instant response ──
    static string _cachedIpcSummary = "";
    static string _cachedIpcPromptPreview = "";
    static DateTime _cachedIpcUpdatedAt = DateTime.MinValue;

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs, bool elevated = false, int replacePid = 0)
    {
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        // ── Kill duplicate Eye processes ──
        // Sweep all wkappbot/wkappbot-core processes that have an Eye window or hold the Eye mutex.
        // This ensures only one Eye runs at a time regardless of how many were spawned.
        if (replacePid == 0)
        {
            int myPid = Environment.ProcessId;
            var names = new[] { "wkappbot", "wkappbot-core" };
            foreach (var name in names)
            foreach (var proc in Process.GetProcessesByName(name))
            {
                if (proc.Id == myPid) continue;
                bool isEye = false;
                try
                {
                    // Detect Eye: has "WK AppBot Eye" window owned by this PID
                    var sb2 = new System.Text.StringBuilder(256);
                    NativeMethods.EnumWindows((hwnd, _) =>
                    {
                        NativeMethods.GetWindowTextW(hwnd, sb2, sb2.Capacity);
                        if (sb2.ToString() == "WK AppBot Eye")
                        {
                            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                            if ((int)pid == proc.Id) isEye = true;
                        }
                        return !isEye;
                    }, IntPtr.Zero);
                }
                catch { }
                if (!isEye) continue;
                try
                {
                    proc.Kill();
                    proc.WaitForExit(3000);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Killed duplicate Eye (PID={proc.Id})");
                    Console.ResetColor();
                }
                catch { /* already exited or access denied */ }
                finally { proc.Dispose(); }
            }
        }
        else
        {
            // Hot-swap start: old Eye should self-exit (cts.Cancel), but if it goes zombie,
            // force-kill after 1 minute. Fire-and-forget — does not block startup.
            _ = Task.Run(async () =>
            {
                await Task.Delay(60_000);
                try
                {
                    using var old = Process.GetProcessById(replacePid);
                    if (!old.HasExited)
                    {
                        old.Kill();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[EYE:HOT-SWAP] Force-killed zombie old Eye (PID={replacePid}) after 1min grace");
                        Console.ResetColor();
                    }
                }
                catch { /* already exited — happy path */ }
            });
        }

        // Acquire named mutex — signals to other processes that GlobalMode Eye is running
        _eyeRunMutex = new System.Threading.Mutex(true, @"Global\WKAppBotEyeGlobal", out _);

        // Thread-local console routing: command threads → pipe StringWriter, Eye threads → real console
        Console.SetOut(new ThreadRoutingWriter(Console.Out));
        // Start command pipe server — Launcher delegates commands here (zero cold-start)
        EyeCmdPipeServer.StartServer();

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        // ★ Default: pure focusless mode — Eye will not steal foreground focus
        // AllowFocusSteal is temporarily enabled for handoff nudges only
        ClaudePromptHelper.AllowFocusSteal = false;
        Console.WriteLine("[EYE] Focusless mode (AllowFocusSteal=OFF by default)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // ── Eye pipe server (always: admin UIA proxy + eye tick IPC) ──
        _ = Task.Run(() => ElevatedEyeServer.ListenAsync(cts.Token));
        Console.WriteLine($"[EYE] Eye pipe server started (elevated={elevated})");

        if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
        if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;

        // ── Find Claude Desktop window (for plan approval UIA clicks) ──
        // Stored in a field so the getter closure below always returns the current value.
        // Re-fetched automatically when stale (Electron restart / window recreation).
        IntPtr claudeHwnd = FindClaudeDesktopWindow();
        if (claudeHwnd != IntPtr.Zero)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE] Found Claude Desktop (hwnd=0x{claudeHwnd:X8})");
            Console.ResetColor();
        }
        // Getter: re-detects if hwnd gone (Claude Desktop restarted or Electron recreated window).
        IntPtr GetCurrentClaudeHwnd()
        {
            if (claudeHwnd != IntPtr.Zero && !WKAppBot.Win32.Native.NativeMethods.IsWindow(claudeHwnd))
            {
                var fresh = FindClaudeDesktopWindow();
                if (fresh != claudeHwnd)
                {
                    claudeHwnd = fresh;
                    if (claudeHwnd != IntPtr.Zero)
                        Console.WriteLine($"[EYE] Claude Desktop re-detected (hwnd=0x{claudeHwnd:X8})");
                }
            }
            return claudeHwnd;
        }

        // ── Slack status streaming (per-instance state → AppBotEyeClaudeStatusStreamer.cs) ──
        var statusTsFile = Path.Combine(DataDir, "runtime", "status_streaming_ts.txt");

        // Previous status message ts — will be deleted after Slack connects
        string? previousStatusTs = null;
        try
        {
            if (File.Exists(statusTsFile))
            {
                var savedTs = File.ReadAllText(statusTsFile).Trim();
                if (!string.IsNullOrEmpty(savedTs))
                    previousStatusTs = savedTs;
            }
        }
        catch { }

        // ── Claude status tracking ──
        string? cachedClaudeStatusText = null;

        // ── Slack Socket Mode daemon (always ON) ──
        SlackSocketClient? slackClient = null;
        string? slackBotToken = null;
        string? slackChannel = null;
        var botUsername = BotUsername;

        try
        {
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (File.Exists(configPath))
            {
                var json = JsonNode.Parse(File.ReadAllText(configPath));
                var appToken = json?["app_token"]?.GetValue<string>();
                slackBotToken = json?["bot_token"]?.GetValue<string>();
                slackChannel = json?["channel"]?.GetValue<string>();
                _eyeBotToken = slackBotToken;
                _eyeChannel = slackChannel;

                if (!string.IsNullOrEmpty(appToken) && !string.IsNullOrEmpty(slackBotToken))
                {
                    slackClient = new SlackSocketClient();
                    slackClient.ConnectAsync(appToken, slackBotToken).GetAwaiter().GetResult();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[EYE] Slack Socket Mode connected (GlobalMode)");
                    Console.ResetColor();

                    // Startup: sweep ALL stale bot status/idle messages (not just saved ts)
                    // Fixes: hot-swap race leaves orphan idle messages in channel
                    {
                        int swept = 0;
                        try
                        {
                            using var http = new HttpClient();
                            http.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", slackBotToken);
                            var histUrl = $"https://slack.com/api/conversations.history?channel={slackChannel}&limit=20";
                            var histResp = http.GetStringAsync(histUrl).GetAwaiter().GetResult();
                            var histJson = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(histResp);
                            if (histJson?["ok"]?.GetValue<bool>() == true && histJson["messages"] is System.Text.Json.Nodes.JsonArray msgs)
                            {
                                foreach (var m in msgs)
                                {
                                    var text = m?["text"]?.GetValue<string>() ?? "";
                                    var botId = m?["bot_id"]?.GetValue<string>();
                                    var subtype = m?["subtype"]?.GetValue<string>();
                                    // Only delete bot messages that are status/idle patterns
                                    if (botId != null || subtype == "bot_message")
                                    {
                                        bool isStatus = text.StartsWith(":zzz:") || text.StartsWith(":gear:")
                                            || text.StartsWith(":clipboard:") || text.StartsWith(":memo:")
                                            || text.StartsWith(":warning:") || text.StartsWith(":robot_face:");
                                        if (isStatus)
                                        {
                                            var ts = m?["ts"]?.GetValue<string>();
                                            if (ts != null)
                                            {
                                                Task.Run(async () => await SlackDeleteMessageAsync(
                                                    slackBotToken!, slackChannel!, ts)).Wait(2000);
                                                swept++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                        if (swept > 0)
                            Console.WriteLine($"[EYE] Swept {swept} stale status message(s)");
                        try { File.WriteAllText(statusTsFile, ""); } catch { }
                        previousStatusTs = null;
                    }
                    string? startupTs = null;

                    // Set up event handlers (Slack → Claude prompt forwarding, plan/permission approval, status streaming)
                    SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                        GetCurrentClaudeHwnd, GetAnyPlanApprovalTs,
                        GetAnyPermissionTs, startupTs, botUsername,
                        GetAnyInstanceSlackStatusTs, () => ResetAllInstancesSlackStatus(statusTsFile));

                    // Block Kit button handler (plan approve/reject, permission buttons)
                    slackClient.OnBlockAction += (action) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[EYE][SLACK] Button: {action.ActionId}={action.Value} by {action.UserName}");
                        Console.ResetColor();

                        var thread = action.MessageTs ?? GetAnyPlanApprovalTs();

                        var cwHwnd = GetCurrentClaudeHwnd(); // re-detect if stale
                        if (action.ActionId == "plan_approve" && cwHwnd != IntPtr.Zero)
                        {
                            var approved = ClickApproveButton(cwHwnd);
                            var reply = approved
                                ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                                : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = approved
                                    ? $":white_check_mark: *플랜 승인됨* — by {action.UserName}"
                                    : ":warning: 승인 시도했으나 버튼을 찾지 못함";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                        else if (action.ActionId.StartsWith("perm_") && cwHwnd != IntPtr.Zero)
                        {
                            var buttonText = action.Value;
                            var clicked = ClickPermissionButton(cwHwnd, buttonText);
                            var reply = clicked
                                ? $":white_check_mark: \"{buttonText}\" 클릭 완료!"
                                : $":x: \"{buttonText}\" 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = clicked
                                    ? $":white_check_mark: *\"{buttonText}\" 처리됨* — by {action.UserName}"
                                    : $":warning: \"{buttonText}\" 버튼을 찾지 못함";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                        else if (action.ActionId == "plan_reject" && cwHwnd != IntPtr.Zero)
                        {
                            var feedbackOk = TypePlanFeedback(cwHwnd, "이 플랜을 거절합니다. 다시 검토해주세요.");
                            var reply = feedbackOk
                                ? ":no_entry_sign: 플랜 거절 피드백을 Claude에 전달했습니다."
                                : ":x: 피드백 입력란을 찾을 수 없습니다";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = $":no_entry_sign: *플랜 거절됨* — by {action.UserName}";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                    };
                }
                else
                {
                    Console.WriteLine("[EYE] Slack config missing tokens — Slack disabled");
                }
            }
            else
            {
                Console.WriteLine("[EYE] Slack config not found — Slack disabled");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] Slack init error: {ex.Message} — continuing without Slack");
        }

        // ── Startup: execute overdue schedules (PC reboot recovery) ──
        try
        {
            var overdueItems = ScheduleManager.GetDueItems();
            if (overdueItems.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SCHEDULE] {overdueItems.Count} overdue schedule(s) from before restart");
                Console.ResetColor();
                Thread.Sleep(5000);
                foreach (var item in overdueItems)
                {
                    ExecuteScheduleItem(item, slackBotToken, slackChannel);
                    Thread.Sleep(2000);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SCHEDULE] Startup recovery error: {ex.Message}");
        }

        // ── Hot-reload: track EXE timestamp ──
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        var exeStartTime = File.Exists(exePath) ? File.GetLastWriteTimeUtc(exePath) : DateTime.MinValue;
        bool hotReloadTriggered = false;

        var keepAwakeSw = Stopwatch.StartNew();
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
            WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
            WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
        _lastKeepAwakeUtc = DateTime.UtcNow;

        // ── FSW hybrid: event-driven file change detection ──
        InitFileWatchers();

        // ── Whisper Spectrum Ring (always-on mic → radial HUD overlay) ──
        var eyeStartTime = DateTime.UtcNow; // gate: auto-study allowed only after 10 min
        WhisperEngine? whisperEngine = null;
        WhisperRingHost? whisperRing = null;
        WhisperExperienceDb? whisperExp = null;
        try
        {
            whisperEngine = new WhisperEngine();
            if (whisperEngine.Start())
            {
                whisperRing = new WhisperRingHost();
                // Position: left of Eye window (Eye is at top-right corner)
                int ringX = Math.Max(0, posX - 190);
                int ringY = posY;
                whisperRing.Start(ringX, ringY);

                // Experience DB: token logging + STT auto-labeling
                whisperExp = new WhisperExperienceDb();
                whisperExp.StartLogging();
                bool sttOk = whisperExp.StartStt();

                // Auto-study: when _unknown/ reaches 10 files, run study in background
                // Gate: skip for first 10 min after Eye starts, then enforce 10-min minimum interval
                var expRef = whisperExp; // capture for closure
                DateTime lastStudyTime = DateTime.MinValue;
                whisperExp.OnAutoStudyNeeded += (count) =>
                {
                    var now = DateTime.UtcNow;
                    if ((now - eyeStartTime).TotalMinutes < 10)
                    {
                        Console.WriteLine($"[WHISPER] Auto-study deferred (Eye started {(now - eyeStartTime).TotalMinutes:F1} min ago, wait 10 min)");
                        expRef.NotifyAutoStudyDone();
                        return;
                    }
                    if ((now - lastStudyTime).TotalMinutes < 10)
                    {
                        Console.WriteLine($"[WHISPER] Auto-study deferred (last study {(now - lastStudyTime).TotalMinutes:F1} min ago, wait 10 min)");
                        expRef.NotifyAutoStudyDone();
                        return;
                    }
                    lastStudyTime = now;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"[WHISPER] Auto-study triggered: {count} files in _unknown/");
                            Console.ResetColor();
                            WhisperStudyCommand(["--batch", count.ToString()]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[WHISPER] Auto-study error: {ex.Message}");
                        }
                        finally
                        {
                            expRef.NotifyAutoStudyDone();
                        }
                    });
                };

                whisperEngine.OnFrame += (frame) =>
                {
                    if (whisperRing.IsAlive)
                    {
                        var (lastStt, lastSttTicks, lastSttMode, _) = whisperExp?.GetStatus() ?? (null, 0, "QUIET", 0);
                        long ageTicks = lastStt != null ? DateTime.UtcNow.Ticks - lastSttTicks : long.MaxValue;
                        int segFrames = whisperExp?.SegmentFrames ?? 0;
                        whisperRing.UpdateSpectrum(frame.Levels, frame.MaxLevel,
                            frame.Mode, frame.Token, frame.RecentTokens, lastStt, ageTicks, lastSttMode,
                            segFrames, frame.SoundCode, frame.VoiceLevels);
                    }
                    whisperExp?.LogFrame(frame);
                };

                // Mic PCM → parallel MP3 recording for Gemini STT
                // Align channel count FIRST so LameMP3FileWriter uses correct WaveFormat
                whisperExp?.SetMicChannels(whisperEngine.Channels);
                whisperEngine.OnMicData += (buf, len) => whisperExp?.WriteMicData(buf, len);

                // Mic segment ready → move to _unknown/ for batch Gemini STT (no real-time processing)
                whisperExp!.OnMicSegmentReady += (mp3Path) =>
                {
                    try
                    {
                        var unknownDir = Path.Combine(Path.GetDirectoryName(mp3Path)!, "..", "_unknown");
                        Directory.CreateDirectory(unknownDir);
                        var dest = Path.Combine(unknownDir, Path.GetFileName(mp3Path));
                        File.Move(mp3Path, dest);
                    }
                    catch { /* best effort */ }
                };

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[EYE] Whisper Ring started at ({ringX},{ringY})");
                Console.WriteLine($"[EYE] Whisper ExpDB: logging=ON stt={( sttOk ? "ON" : "OFF" )}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[EYE] Whisper Ring skipped (no microphone)");
                Console.ResetColor();
                whisperEngine.Dispose();
                whisperEngine = null;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[EYE] Whisper Ring init failed: {ex.Message}");
            Console.ResetColor();
            whisperEngine?.Dispose();
            whisperEngine = null;
        }

        // ── Context usage monitor (per-card) ──
        // Track warned sizes per CWD — only re-warn if size actually increased
        var contextWarnedSizes = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

        // ── Duplicate Eye self-close: Z-order check every ~10s ──
        // EnumWindows enumerates top-level windows top-to-bottom (Z-order).
        // First "WK AppBot Eye" window found = topmost. If that's not me → I'm behind → self-close.
        IntPtr myEyeHwnd = IntPtr.Zero;
        int duplicateCheckFrame = 0;

        int frameCount = 0;
        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            // ── Duplicate Eye check (every 100 frames ≈ 10s) ──
            if (++duplicateCheckFrame >= 100)
            {
                duplicateCheckFrame = 0;
                if (myEyeHwnd == IntPtr.Zero) myEyeHwnd = host.GetWindowHandle();
                if (myEyeHwnd != IntPtr.Zero)
                {
                    IntPtr firstEyeHwnd = IntPtr.Zero;
                    var sbDup = new System.Text.StringBuilder(256);
                    NativeMethods.EnumWindows((hwnd, _) =>
                    {
                        NativeMethods.GetWindowTextW(hwnd, sbDup, sbDup.Capacity);
                        if (sbDup.ToString() == "WK AppBot Eye")
                        {
                            firstEyeHwnd = hwnd; // first hit = topmost Eye in Z-order
                            return false; // stop enumeration
                        }
                        return true;
                    }, IntPtr.Zero);

                    if (firstEyeHwnd != IntPtr.Zero && firstEyeHwnd != myEyeHwnd)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[EYE] Not topmost Eye (top=0x{firstEyeHwnd:X} me=0x{myEyeHwnd:X}) — self-closing");
                        Console.ResetColor();
                        cts.Cancel();
                        break;
                    }
                }
            }

            // ── Core tick: read ticks + sessions ──
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags(forceFull);
            if (!TryRunOneGlobalTick(host, timeoutMs: 3000, forceFull, tickDirty, promptDirty))
            {
                if (frameCount < 3)
                {
                    Console.WriteLine($"[EYE] tick timeout (>3s) on frame {frameCount} — startup grace, continuing");
                }
                else
                {
                    Console.WriteLine("[EYE] tick timeout (>3s) - self terminate + restart");
                    hotReloadTriggered = true; // trigger self-restart via hot-swap path
                    break;
                }
            }

            // ── Hot-swap blue-green: first render OK → old Eye exits on its own (return 0) ──
            if (replacePid > 0 && frameCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[EYE:HOT-SWAP] First render OK — old Eye (PID={replacePid}) exiting on its own");
                Console.ResetColor();
                replacePid = 0;
                // Old Eye does return 0 right after Process.Start — 1s should be enough
                Thread.Sleep(1000);
                TryDeleteOldExes();
            }

            // ── Hot-swap cleanup: try delete .old.exe every ~10s (non-blocking fallback) ──
            if (frameCount % 100 == 50)
                TryDeleteOldExes();

            // ── Claude Desktop status detection (~every 5 sec) ──
            if (frameCount % 50 == 0)
            {
                cachedClaudeStatusText = RunClaudeStatusTick(
                    ref claudeHwnd, slackBotToken, slackChannel, botUsername,
                    slackClient, statusTsFile, contextWarnedSizes);
            }

            // ── Schedule executor (~every 10 seconds) ──
            if (frameCount % 100 == 50)
            {
                try
                {
                    var dueItems = ScheduleManager.GetDueItems();
                    foreach (var dueItem in dueItems)
                        ExecuteScheduleItem(dueItem, slackBotToken, slackChannel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SCHEDULE] Error: {ex.Message}");
                }
            }

            // ── Keep-awake ──
            if (keepAwakeSw.ElapsedMilliseconds >= 60_000)
            {
                keepAwakeSw.Restart();
                WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
                    WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
                    WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
                    WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
                _lastKeepAwakeUtc = DateTime.UtcNow;
                Console.WriteLine("[EYE] keep-awake tick");
            }

            // ── Slack reconnect watchdog (~every 5 min) ──
            if (slackClient != null && frameCount % 3000 == 2999)
            {
                if (slackClient.IsConnected && slackClient.MessageCount <= 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] No events received in 5+ minutes — attempting reconnect...");
                    Console.ResetColor();
                    try
                    {
                        slackClient.ReconnectAsync().GetAwaiter().GetResult();
                        Console.WriteLine("[EYE][SLACK] Reconnected successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[EYE][SLACK] Reconnect failed: {ex.Message}");
                    }
                }
            }

            // ── Hot-swap: FSW-driven instant detection + blue-green restart ──
            // FSW flag checked every frame (~100ms) — no 5s polling delay
            if (_fswExeDirty && exeStartTime != DateTime.MinValue)
            {
                _fswExeDirty = false; // consume flag
                try
                {
                    var exeDir = Path.GetDirectoryName(exePath) ?? "";
                    var exeName = Path.GetFileNameWithoutExtension(exePath);
                    var newExePath = Path.Combine(exeDir, $"{exeName}.new.exe");
                    // Timestamped old name — avoids collision with locked ghost-magnifier .old.exe
                    var oldExePath = Path.Combine(exeDir, $"{exeName}.old-{DateTime.Now:yyyyMMdd-HHmm}.exe");

                    if (File.Exists(newExePath))
                    {
                        // .new.exe staged — rename-swap (running exe CAN be renamed on Windows!)
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("[EYE:HOT-SWAP] .new.exe detected — rename-swap");
                        Console.ResetColor();
                        try
                        {
                            // No pre-delete needed — timestamped name is always unique
                            File.Move(exePath, oldExePath);    // running exe → .old-YYYYMMDD-HHmm.exe
                            File.Move(newExePath, exePath);     // .new.exe → wkappbot.exe
                            Console.WriteLine($"[EYE:HOT-SWAP] swap OK (.exe→{Path.GetFileName(oldExePath)}, .new→.exe)");
                            hotReloadTriggered = true;
                            break;
                        }
                        catch (Exception swapEx)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] swap failed ({swapEx.Message})");
                            // Rollback if partial (exe was moved but .new rename failed)
                            if (!File.Exists(exePath) && File.Exists(oldExePath))
                                try { File.Move(oldExePath, exePath); } catch { }
                        }
                    }
                    else if (File.GetLastWriteTimeUtc(exePath) != exeStartTime)
                    {
                        // Direct overwrite succeeded (exe wasn't locked somehow)
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("[EYE:HOT-SWAP] EXE timestamp changed — binary updated!");
                        Console.ResetColor();
                        hotReloadTriggered = true;
                        break;
                    }
                }
                catch { }
            }

            // ── Slack socket health check (~every 10 min = 6000 frames @ 100ms) ──
            if (frameCount % 6000 == 0 && frameCount > 0 && slackClient != null)
            {
                try
                {
                    var connAge = (DateTime.UtcNow - slackClient.LastConnectedUtc).TotalMinutes;
                    if (!slackClient.IsConnected || connAge >= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[EYE][SLACK] Health check: connected={slackClient.IsConnected} age={connAge:F0}m → force reconnect");
                        Console.ResetColor();
                        Task.Run(async () =>
                        {
                            try
                            {
                                await slackClient.ReconnectAsync();
                                Console.WriteLine("[EYE][SLACK] Periodic reconnect OK");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[EYE][SLACK] Periodic reconnect failed: {ex.Message}");
                            }
                        }).Wait(10000);
                    }
                    else
                    {
                        Console.WriteLine($"[EYE][SLACK] Health OK: connected={slackClient.IsConnected} age={connAge:F0}m msgs={slackClient.MessageCount} reconnects={slackClient.ReconnectCount}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EYE][SLACK] Health check error: {ex.Message}");
                }
            }

            // ── Periodic GC (~every 5 min = 3000 frames @ 100ms) ──
            if (frameCount % 3000 == 0 && frameCount > 0)
            {
                var beforeMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();
                GC.Collect(2, GCCollectionMode.Optimized);
                var afterMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[EYE][GC] Gen2 collect: {beforeMB}MB → {afterMB}MB (freed {beforeMB - afterMB}MB)");
                Console.ResetColor();
            }

            // ── Stats logging ──
            if (frameCount % 100 == 0 && frameCount > 0)
            {
                var slackInfo = slackClient != null
                    ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}, reconn={slackClient.ReconnectCount}"
                    : "";
                Console.WriteLine($"[EYE] frame #{frameCount} ({(slackClient != null ? "Socket+API" : "API-only")}{slackInfo})");
            }

            frameCount++;
            Thread.Sleep(Math.Max(100, intervalMs));
        }

        // ── Cleanup ──
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        // ── Cleanup Whisper Ring + ExpDB ──
        if (whisperEngine != null)
        {
            whisperExp?.Stop();
            whisperEngine.Dispose();
            whisperRing?.BeginFadeOut();
            Thread.Sleep(1200);
            whisperRing?.Dispose();
        }

        // ── Cleanup FSW watchers ──
        DisposeFileWatchers();

        if (slackClient != null)
        {
            try
            {
                // Shutdown — no Slack announcement (reduces channel spam on hot-reload restarts)
                slackClient.Dispose();
            }
            catch { }
        }

        // ── Hot-swap: launch new Eye FIRST (instant), then graceful cleanup ──
        if (hotReloadTriggered && File.Exists(exePath))
        {
            try
            {
                // Reconstruct original args + add --replace-pid for blue-green handoff
                var origArgs = Environment.GetCommandLineArgs().Skip(1).ToList();
                // Remove any previous --replace-pid
                for (int ri = origArgs.Count - 1; ri >= 0; ri--)
                    if (origArgs[ri] == "--replace-pid" && ri + 1 < origArgs.Count)
                    { origArgs.RemoveAt(ri + 1); origArgs.RemoveAt(ri); }
                origArgs.Add("--replace-pid");
                origArgs.Add(Environment.ProcessId.ToString());
                var argsStr = string.Join(" ", origArgs.Select(a =>
                    a.Contains(' ') ? $"\"{a}\"" : a));

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[EYE:HOT-SWAP] Launching new Eye: {Path.GetFileName(exePath)} {argsStr}");
                Console.ResetColor();

                var psi = new ProcessStartInfo
                {
                    FileName = exePath,  // always original path (rename-swap already done)
                    Arguments = argsStr,
                    UseShellExecute = false, // inherit admin token from parent
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE:HOT-SWAP] Re-launch failed: {ex.Message}");
            }
        }

        // ── Graceful shutdown ──
        cts.Cancel();

        Console.WriteLine("[EYE:HOT-SWAP] Old Eye shutting down");
        return 0;
    }

    static bool TryRunOneGlobalTick(AppBotEyeHost host, int timeoutMs, bool forceFull, bool tickDirty, bool promptDirty)
    {
        try
        {
            var t = Task.Run(() => RunOneGlobalTick(host, forceFull, tickDirty, promptDirty));
            return t.Wait(timeoutMs);
        }
        catch { return false; }
    }

    static void RunOneGlobalTick(AppBotEyeHost host, bool forceFull, bool tickDirty, bool promptDirty)
    {
        var swTotal = Stopwatch.StartNew();

        var swTick = Stopwatch.StartNew();
        long tickRead = 0, tickParse = 0;
        var latest = _cachedLatestTick;
        if (forceFull || tickDirty || latest == null)
        {
            latest = ReadLatestTick(out tickRead, out tickParse);
            _cachedLatestTick = latest;
            _cachedCards = ReadEyeCards(staleSeconds: 86400); // 24 hours
            SupplementCardsFromPrompts(_cachedCards);
            CheckAndReportDeadCards(_cachedCards);
        }
        swTick.Stop();

        var swPrompt = Stopwatch.StartNew();
        var promptDiag = new PromptDiag();
        var promptPreview = _cachedPromptPreview;
        if (forceFull || promptDirty || string.IsNullOrWhiteSpace(promptPreview))
        {
            promptPreview = ReadLatestOpenClawPromptPreview(promptDiag);
            _cachedPromptPreview = promptPreview;
            _cachedPromptFileWriteUtc = promptDiag.FileWriteUtc; // cache mtime for kro sort
        }
        else
        {
            promptDiag.Source = "sessions-cache";
            promptDiag.CacheMs = 1;
            promptDiag.FileWriteUtc = _cachedPromptFileWriteUtc; // restore cached mtime
        }
        swPrompt.Stop();

        var swSchedule = Stopwatch.StartNew();
        // placeholder for future schedule diagnostics
        swSchedule.Stop();

        if (latest != null)
        {
            _lastTickActivityUtc = DateTime.UtcNow;
            if ((latest.Status ?? "").Contains("step:", StringComparison.OrdinalIgnoreCase) ||
                (latest.Status ?? "").Contains("done", StringComparison.OrdinalIgnoreCase))
                _lastAiActivityUtc = DateTime.UtcNow;
        }
        if (!string.IsNullOrWhiteSpace(promptPreview))
            _lastAiActivityUtc = DateTime.UtcNow;

        _lastPromptSource = promptDiag.Source;

        var cards = _cachedCards;

        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        var eyeSummary = BuildEyeSummary(cards, latest, promptPreview, promptDiag.FileWriteUtc);
        host.UpdateAccessibilityText(eyeSummary);
        // Update IPC cache so eye tick one-shot gets instant response
        _cachedIpcSummary = eyeSummary;
        _cachedIpcPromptPreview = promptPreview ?? "";
        _cachedIpcUpdatedAt = DateTime.UtcNow;

        swTotal.Stop();

        var mode = forceFull ? "full-1s" : (tickDirty || promptDirty ? "dirty" : "idle");
        // Memory delta tracking
        var proc = Process.GetCurrentProcess();
        var wsMB = proc.WorkingSet64 / (1024 * 1024);
        var deltaMB = wsMB - _prevWorkingSetMB;
        if (wsMB > _peakWorkingSetMB) _peakWorkingSetMB = wsMB;
        _prevWorkingSetMB = wsMB;
        var memSuffix = deltaMB != 0 ? $" mem={wsMB}MB({(deltaMB >= 0 ? "+" : "")}{deltaMB}) peak={_peakWorkingSetMB}MB" : "";

        var ctxSuffix = _lastContextPct >= 0 ? $" ctx={_lastContextPct}%" : "";
        Console.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms{memSuffix}{ctxSuffix}");
        Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");

        var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
        var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
        var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
        var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
        Console.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

        var latestAge = -1.0;
        if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
            latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
        var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
        Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");
    }

    static bool ShouldForceFullLoad()
    {
        var now = DateTime.UtcNow;
        if (_lastForceFullLoadUtc == DateTime.MinValue || (now - _lastForceFullLoadUtc).TotalMilliseconds >= 1000)
        {
            _lastForceFullLoadUtc = now;
            return true;
        }
        return false;
    }

    /// <summary>
    /// FSW hybrid dirty check:
    /// - Fast path: consume volatile FSW flags (set by FileSystemWatcher callbacks, ~0ms)
    /// - Slow path: FileInfo triple-check (only on forceFull=true, every 1s safety net)
    /// This eliminates 100ms polling overhead while keeping reliability.
    /// </summary>
    static (bool tickDirty, bool promptDirty) CheckGlobalDirtyFlags(bool forceFull = false)
    {
        // ── Fast path: FSW event-driven flags (instant, no I/O) ──
        bool tickDirty = _fswTickDirty;
        bool promptDirty = _fswPromptDirty;
        var promptChangedFile = _fswPromptChangedFile;
        if (tickDirty) _fswTickDirty = false;    // consume flag
        if (promptDirty) _fswPromptDirty = false; // consume flag

        // Filter: skip prompt dirty if the changed file isn't the one we're tracking
        if (promptDirty && !string.IsNullOrEmpty(promptChangedFile) && !string.IsNullOrEmpty(_dirtyPromptFile))
        {
            var trackedName = Path.GetFileName(_dirtyPromptFile);
            if (!string.Equals(promptChangedFile, trackedName, StringComparison.OrdinalIgnoreCase))
            {
                promptDirty = false; // irrelevant file change — skip
            }
        }

        // ── Slow path: FileInfo poll (only on 1s safety-net intervals) ──
        // Catches edge cases: FSW buffer overflow, network drives, watcher init failure
        if (forceFull)
        {
            try
            {
                var tickPath = EyeTicksPath;
                if (File.Exists(tickPath))
                {
                    var fi = new FileInfo(tickPath);
                    if (_dirtyTickFile != tickPath || _dirtyTickLength != fi.Length || _dirtyTickWriteUtc != fi.LastWriteTimeUtc)
                    {
                        tickDirty = true;
                        _dirtyTickFile = tickPath;
                        _dirtyTickLength = fi.Length;
                        _dirtyTickWriteUtc = fi.LastWriteTimeUtc;
                    }
                }
            }
            catch { }

            try
            {
                var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
                if (Directory.Exists(sessionsDir))
                {
                    var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                        .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                        .FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(latestFile) && File.Exists(latestFile))
                    {
                        var fi = new FileInfo(latestFile);
                        if (_dirtyPromptFile != latestFile || _dirtyPromptLength != fi.Length || _dirtyPromptWriteUtc != fi.LastWriteTimeUtc)
                        {
                            promptDirty = true;
                            _dirtyPromptFile = latestFile;
                            _dirtyPromptLength = fi.Length;
                            _dirtyPromptWriteUtc = fi.LastWriteTimeUtc;
                        }
                    }
                }
            }
            catch { }
        }

        return (tickDirty, promptDirty);
    }

    /// <summary>
    /// FSW hybrid: create FileSystemWatchers for tick file + OpenClaw sessions dir.
    /// Events set volatile dirty flags → 100ms loop picks them up instantly (no FileInfo poll).
    /// 1s full-load safety net unchanged.
    /// </summary>
    static void InitFileWatchers()
    {
        // ── 1. Tick file watcher (eye_ticks.jsonl) ──
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
                Console.WriteLine($"[EYE][FSW] Tick watcher: {tickDir}/{tickFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Tick watcher init failed: {ex.Message}");
        }

        // ── 2. OpenClaw sessions watcher (*.jsonl) ──
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
                Console.WriteLine($"[EYE][FSW] Prompt watcher: {sessionsDir}/*.jsonl");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Prompt watcher init failed: {ex.Message}");
        }

        // ── 3. EXE file watcher (hot-swap: instant binary change detection) ──
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
                    Console.WriteLine("[EYE][FSW] .new.exe already present at startup — triggering hot-swap");
                    _fswExeDirty = true;
                }
                Console.WriteLine($"[EYE][FSW] Hot-swap watcher: {exeDir}/{exeFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Hot-swap watcher init failed: {ex.Message}");
        }
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
                    Console.WriteLine($"[EYE:HOT-SWAP] Cleaned up {Path.GetFileName(oldExe)}");
                }
                catch { } // still locked — 10s polling will retry
            }
        }
        catch { }
    }

    static void DisposeFileWatchers()
    {
        try { if (_fswTick != null) { _fswTick.EnableRaisingEvents = false; _fswTick.Dispose(); _fswTick = null; } } catch { }
        try { if (_fswPrompt != null) { _fswPrompt.EnableRaisingEvents = false; _fswPrompt.Dispose(); _fswPrompt = null; } } catch { }
        try { if (_fswExe != null) { _fswExe.EnableRaisingEvents = false; _fswExe.Dispose(); _fswExe = null; } } catch { }
        try { if (_fswExeNew != null) { _fswExeNew.EnableRaisingEvents = false; _fswExeNew.Dispose(); _fswExeNew = null; } } catch { }
        Console.WriteLine("[EYE][FSW] Watchers disposed");
    }

    static int EyeTickCommand(string[] args)
    {
        // ── Hard timeout: --timeout N kills the process if tick takes too long ──
        var timeoutSec = 0;
        for (int i = 0; i < args.Length - 1; i++)
            if (args[i] == "--timeout") int.TryParse(args[i + 1], out timeoutSec);
        System.Threading.Timer? killTimer = null;
        if (timeoutSec > 0)
        {
            killTimer = new System.Threading.Timer(_ =>
            {
                Console.WriteLine($"[EYE_TICK] hard timeout {timeoutSec}s exceeded — exiting");
                Console.Out.Flush();
                Environment.Exit(2);
            }, null, timeoutSec * 1000, System.Threading.Timeout.Infinite);
        }

        try
        {
            if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
            if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;
            var swTotal = Stopwatch.StartNew();

            // ── Fast path: query running Eye loop via IPC pipe ──
            // Eye loop maintains all caches in memory — IPC response is ~5ms vs ~600ms legacy scan.
            // Fallback to legacy only when Eye is not running or pipe query fails.
            var ipc = EyeIpcClient.QueryTickAsync(timeoutMs: 2000).GetAwaiter().GetResult();
            if (ipc != null)
            {
                Console.WriteLine($"[EYE] one-shot tick (IPC fast-path, cache age={ipc.CachedAgeMs}ms)");
                Console.WriteLine($"[EYE_TICK] ipc=ok total={swTotal.ElapsedMilliseconds}ms ctx={ipc.ContextPct}%");
                Console.WriteLine($"[EYE_TICK] hint promptLine={ipc.PromptLineHint} tickLine={ipc.TickLineHint}");
                Console.WriteLine($"[EYE_TICK] cards={ipc.CardCount} promptSource={ipc.PromptSource}");
                if (!string.IsNullOrWhiteSpace(ipc.Prompt))
                    Console.WriteLine($"[EYE_TICK] recent={ipc.Prompt}");
                foreach (var plan in ipc.Plans)
                    Console.WriteLine($"[EYE_PLAN] —:— {plan}");
                if (ipc.Plans.Length > 3) Console.WriteLine($"[EYE_PLAN] —:— 그 외 {ipc.Plans.Length - 3}건...");
                Console.WriteLine($"[EYE_GUARD] armed={(ipc.GuardArmed ? 1 : 0)} execIdle={ipc.ExecIdleSec:F0}s aiIdle={ipc.AiIdleSec:F0}s cooldown={ipc.CooldownSec:F0}s");
                Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(ipc.KeepAwakeAgeSec < 0 ? "n/a" : ipc.KeepAwakeAgeSec.ToString("F0") + "s")} promptSource={ipc.PromptSource} latestTickAge={(ipc.LatestTickAgeSec < 0 ? "n/a" : ipc.LatestTickAgeSec.ToString("F0") + "s")}");
                Console.WriteLine($"[EYE_TICK] ── card display ──");
                foreach (var line in ipc.Summary.Split('\n'))
                    Console.WriteLine($"[EYE_TICK] {line.TrimEnd('\r')}");
                Console.Out.Flush();
                // Slack forwarding handled by OnMessage → slack route worker (no HTTP poll on tick)
                Console.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                return 0;
            }

            // ── Eye not running: auto-launch + wait for pipe ──
            Console.WriteLine("[EYE] IPC query failed — launching Eye and waiting for pipe...");
            LaunchAppBotEyeIfNeeded();

            // Wait ~3s for Eye pipe to be ready (Eye startup ~1-2s), then fall back
            Thread.Sleep(3000);
            var ipcRetry = EyeIpcClient.QueryTickAsync(timeoutMs: 1000).GetAwaiter().GetResult();

            if (ipcRetry != null)
            {
                Console.WriteLine($"[EYE] one-shot tick (IPC after launch, cache age={ipcRetry.CachedAgeMs}ms)");
                Console.WriteLine($"[EYE_TICK] ipc=ok total={swTotal.ElapsedMilliseconds}ms ctx={ipcRetry.ContextPct}%");
                Console.WriteLine($"[EYE_TICK] hint promptLine={ipcRetry.PromptLineHint} tickLine={ipcRetry.TickLineHint}");
                Console.WriteLine($"[EYE_TICK] cards={ipcRetry.CardCount} promptSource={ipcRetry.PromptSource}");
                if (!string.IsNullOrWhiteSpace(ipcRetry.Prompt))
                    Console.WriteLine($"[EYE_TICK] recent={ipcRetry.Prompt}");
                foreach (var plan in ipcRetry.Plans)
                    Console.WriteLine($"[EYE_PLAN] —:— {plan}");
                Console.WriteLine($"[EYE_GUARD] armed={(ipcRetry.GuardArmed ? 1 : 0)} execIdle={ipcRetry.ExecIdleSec:F0}s aiIdle={ipcRetry.AiIdleSec:F0}s cooldown={ipcRetry.CooldownSec:F0}s");
                Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(ipcRetry.KeepAwakeAgeSec < 0 ? "n/a" : ipcRetry.KeepAwakeAgeSec.ToString("F0") + "s")} promptSource={ipcRetry.PromptSource} latestTickAge={(ipcRetry.LatestTickAgeSec < 0 ? "n/a" : ipcRetry.LatestTickAgeSec.ToString("F0") + "s")}");
                Console.WriteLine($"[EYE_TICK] ── card display ──");
                foreach (var line in ipcRetry.Summary.Split('\n'))
                    Console.WriteLine($"[EYE_TICK] {line.TrimEnd('\r')}");
                Console.Out.Flush();
                Console.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                return 0;
            }

            Console.WriteLine("[EYE] Eye launch or pipe timeout — falling back to legacy scan");

            // ── Phase 1: ReadLatestTick ──
            var swPhase = Stopwatch.StartNew();
            long tickRead = 0, tickParse = 0;
            var latest = ReadLatestTick(out tickRead, out tickParse);
            swPhase.Stop();
            var msReadTick = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] ReadLatestTick={msReadTick}ms (read={tickRead}ms,parse={tickParse}ms)");
            Console.Out.Flush();

            // ── Phase 2: ReadLatestOpenClawPromptPreview ──
            swPhase.Restart();
            var promptDiag = new PromptDiag();
            var prompt = ReadLatestOpenClawPromptPreview(promptDiag);
            swPhase.Stop();
            var msPrompt = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] PromptPreview={msPrompt}ms (stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms)");
            Console.Out.Flush();

            // ── Phase 3: ReadEyeCards + prompt-based discovery ──
            swPhase.Restart();
            var cards = ReadEyeCards(staleSeconds: 86400); // 24 hours
            SupplementCardsFromPrompts(cards);
            CheckAndReportDeadCards(cards);
            swPhase.Stop();
            var msCards = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] ReadEyeCards={msCards}ms (count={cards.Count})");
            Console.Out.Flush();
            _lastPromptSource = promptDiag.Source;

            // ── Phase 4: Context % check ──
            swPhase.Restart();
            try
            {
                var cpDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects");
                if (Directory.Exists(cpDir))
                {
                    var jsonls = Directory.EnumerateFiles(cpDir, "*.jsonl", SearchOption.AllDirectories)
                        .Select(f => { try { var fi = new FileInfo(f); fi.Refresh(); return fi; } catch { return null; } })
                        .Where(fi => fi != null && fi.Length > 0)
                        .OrderByDescending(fi => fi!.LastWriteTimeUtc)
                        .ToList();
                    if (jsonls.Count > 0)
                    {
                        var lf = jsonls[0]!;
                        _lastContextPct = (int)(lf.Length / (1024.0 * 1024.0) / 40.0 * 100);
                    }
                }
            }
            catch { }
            swPhase.Stop();
            var msCtx = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] ContextPct={msCtx}ms (ctx={_lastContextPct}%)");
            Console.Out.Flush();

            // ── Phase 5: ExtractRecentPlanItems ──
            swPhase.Restart();
            var plans = ExtractRecentPlanItems(maxItems: 3);
            swPhase.Stop();
            var msPlans = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] PlanItems={msPlans}ms (count={plans.Count})");
            Console.Out.Flush();

            // ── Phase 6: BuildEyeSummary ──
            swPhase.Restart();
            var summary = BuildEyeSummary(cards, latest, prompt, promptDiag.FileWriteUtc);
            swPhase.Stop();
            var msSummary = swPhase.ElapsedMilliseconds;
            Console.WriteLine($"[PROF] BuildSummary={msSummary}ms");
            Console.Out.Flush();

            // ── Print results ──
            var ctxInfo = _lastContextPct >= 0 ? $" ctx={_lastContextPct}%" : "";
            Console.WriteLine("[EYE] one-shot tick");
            Console.WriteLine($"[EYE_TICK] tick={msReadTick}ms prompt={msPrompt}ms cards={msCards}ms ctx={msCtx}ms plans={msPlans}ms summary={msSummary}ms total={swTotal.ElapsedMilliseconds}ms{ctxInfo}");
            Console.Out.Flush();
            Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");
            Console.WriteLine($"[EYE_TICK] cards={cards.Count} promptSource={promptDiag.Source}");
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.WriteLine($"[EYE_TICK] recent={prompt}");

            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    Console.WriteLine($"[EYE_PLAN] —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    Console.WriteLine($"[EYE_PLAN] —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
            var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
            var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
            var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
            Console.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

            var latestAge = -1.0;
            if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
                latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
            var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
            Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");

            Console.WriteLine($"[EYE_TICK] ── card display ──");
            foreach (var line in summary.Split('\n'))
                Console.WriteLine($"[EYE_TICK] {line.TrimEnd('\r')}");
            Console.Out.Flush();

            // Phase 7: Slack forwarding handled by OnMessage → slack route worker (no HTTP poll)
            Console.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] error={ex.Message}");
            Console.Out.Flush();
            return 1;
        }
        finally
        {
            killTimer?.Dispose();
        }
    }

    /// <summary>
    /// Build IPC response from cached state — called by EyeIpcServer on pipe connection.
    /// All fields read from static cache updated by RunOneGlobalTick; no UIA/file scan needed.
    /// </summary>
    public static EyeIpcTickResponse BuildIpcTickResponse()
    {
        var now = DateTime.UtcNow;
        var execIdle = (now - _lastTickActivityUtc).TotalSeconds;
        var aiIdle = (now - _lastAiActivityUtc).TotalSeconds;
        var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (now - _lastAutoGogoUtc).TotalSeconds;
        var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
        var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (now - _lastKeepAwakeUtc).TotalSeconds;
        var latestTickAge = -1.0;
        if (_cachedLatestTick != null && DateTime.TryParse(_cachedLatestTick.Ts, out var ts))
            latestTickAge = (now - ts.ToUniversalTime()).TotalSeconds;

        return new EyeIpcTickResponse
        {
            Summary = _cachedIpcSummary,
            CardCount = _cachedCards.Count,
            ContextPct = _lastContextPct,
            Plans = _lastPlanItemsCache.Take(3).ToArray(),
            PromptSource = _lastPromptSource ?? "unknown",
            Prompt = _cachedIpcPromptPreview,
            GuardArmed = armed,
            ExecIdleSec = execIdle,
            AiIdleSec = aiIdle,
            CooldownSec = cooldown,
            KeepAwakeAgeSec = keepAge,
            LatestTickAgeSec = latestTickAge,
            PromptLineHint = _lastPromptLineIndex,
            TickLineHint = _lastEyeTickLineIndex,
            CachedAgeMs = _cachedIpcUpdatedAt == DateTime.MinValue ? -1
                : (long)(now - _cachedIpcUpdatedAt).TotalMilliseconds,
        };
    }

    /// <summary>
    /// EyeTick one-shot: check Slack for new messages via conversations.history API
    /// and forward them to Claude prompt. Uses slack_last_ts.txt to track position.
    /// Also checks slack_inbox.jsonl as fallback (from standalone slack listen).
    /// </summary>
    static void EyeTickForwardSlackInbox()
    {
        try
        {
            var swSlack = Stopwatch.StartNew();

            // ── Slack Step 1: Load config ──
            var swStep = Stopwatch.StartNew();
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[EYE_TICK] slack=no_config");
                Console.Out.Flush();
                return;
            }

            var json = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(configPath));
            var botToken = json?["bot_token"]?.GetValue<string>();
            var channel = json?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
            {
                Console.WriteLine("[EYE_TICK] slack=missing_token_or_channel");
                Console.Out.Flush();
                return;
            }
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] LoadConfig={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 2: auth.test (get bot user ID) — HTTP call ──
            swStep.Restart();
            var botUserId = SlackGetBotUserId(botToken);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] auth.test={swStep.ElapsedMilliseconds}ms (botUserId={botUserId ?? "null"})");
            Console.Out.Flush();

            // ── Slack Step 3: LoadLastTs ──
            swStep.Restart();
            var lastTs = LoadLastTs(channel);
            var lastTsDouble = 0.0;
            if (!string.IsNullOrEmpty(lastTs))
                double.TryParse(lastTs, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out lastTsDouble);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] LoadLastTs={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 4: conversations.history — HTTP call ──
            swStep.Restart();
            var messages = SlackFetchHistoryAsync(botToken, channel, limit: 20)
                .GetAwaiter().GetResult();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] conversations.history={swStep.ElapsedMilliseconds}ms (msgs={messages.Count})");
            Console.Out.Flush();

            if (messages.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                Console.Out.Flush();
                return;
            }

            // ── Slack Step 5: Screen reader broadcast + UIA init ──
            swStep.Restart();
            WKAppBot.Win32.Native.ScreenReaderMode.Enable();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] ScreenReaderBroadcast={swStep.ElapsedMilliseconds}ms (enabled={WKAppBot.Win32.Native.ScreenReaderMode.IsEnabled})");
            Console.Out.Flush();

            swStep.Restart();
            using var helper = new ClaudePromptHelper();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] UIA3Automation_Init={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 6: Filter messages ──
            swStep.Restart();
            string? contextLine = null;
            var newMsgs = new List<(string user, string text, string ts)>();
            foreach (var msg in messages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";
                var msgTs = msg["ts"]?.GetValue<string>() ?? "";
                var subtype = msg["subtype"]?.GetValue<string>();

                if (!string.IsNullOrEmpty(subtype)) continue;
                if (string.IsNullOrWhiteSpace(text)) continue;

                double.TryParse(msgTs, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var tsDouble);

                if (msgTs == lastTs)
                {
                    var ctxClean = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                    var who = user == botUserId ? "클롣" : $"@{user}";
                    contextLine = $"[직전 대화] {who}: {ctxClean}";
                    continue;
                }

                if (lastTsDouble > 0 && tsDouble <= lastTsDouble) continue;
                if (user == botUserId) continue;

                newMsgs.Add((user, text, msgTs));
            }
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] FilterMessages={swStep.ElapsedMilliseconds}ms (new={newMsgs.Count})");
            Console.Out.Flush();

            if (newMsgs.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                // Still check thread replies even if no new channel messages
                swStep.Restart();
                EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] ThreadReplies={swStep.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                return;
            }

            newMsgs.Reverse();
            Console.WriteLine($"[EYE_TICK] slack={newMsgs.Count} new message(s) — forwarding to AI prompt...");
            Console.Out.Flush();

            // ── Slack Step 7: FindPrompt (UIA tree walk) ──
            swStep.Restart();
            var promptInfo = FindSlackPreferredPrompt(helper);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] FindPrompt={swStep.ElapsedMilliseconds}ms (found={promptInfo != null}, host={promptInfo?.HostType ?? "n/a"})");
            Console.Out.Flush();
            if (promptInfo == null)
            {
                Console.WriteLine("[EYE_TICK] WARNING: AI prompt not found — will retry next tick");
                Console.Out.Flush();
                return;
            }

            // ── Slack Step 8: Forward messages ──
            int forwarded = 0;
            string? latestTs = null;
            foreach (var (user, text, msgTs) in newMsgs)
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrWhiteSpace(cleanText)) continue;

                var contextPrefix = (contextLine != null && forwarded == 0) ? $"{contextLine}\n\n" : "";
                var promptText = $"{contextPrefix}{cleanText}\n\n{SlackReplySuffix(user, msgTs)}";

                swStep.Restart();
                var fresh = FindSlackPreferredPrompt(helper);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] Re-FindPrompt={swStep.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                if (fresh == null)
                {
                    Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping forward");
                    Console.Out.Flush();
                    break;
                }

                Console.WriteLine($"[EYE_TICK] [FORWARD] Slack @{user} → {fresh.HostType} prompt");
                Console.Out.Flush();
                swStep.Restart();
                var ok = helper.TypeAndSubmit(fresh, promptText);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] TypeAndSubmit={swStep.ElapsedMilliseconds}ms (ok={ok})");
                Console.Out.Flush();
                if (ok)
                {
                    forwarded++;
                    latestTs = msgTs;
                    Console.WriteLine($"[EYE_TICK] [DELIVERED] Slack @{user}: {cleanText}");

                    try
                    {
                        swStep.Restart();
                        var ackText = $"전달했습니다! (thread={msgTs})";
                        var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, msgTs, username: "앱봇아이")
                            .GetAwaiter().GetResult();
                        swStep.Stop();
                        Console.WriteLine($"[PROF:SLACK] AckSend={swStep.ElapsedMilliseconds}ms (ok={ackOk})");
                        Console.Out.Flush();
                        if (ackOk && ackTs != null)
                            SavePendingAck(msgTs, channel, ackTs);
                    }
                    catch { }

                    if (newMsgs.Count > 1)
                        Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine($"[EYE_TICK] WARNING: TypeAndSubmit failed for @{user}");
                    Console.Out.Flush();
                }
            }

            if (latestTs != null)
            {
                SaveLastTs(channel, latestTs);
                Console.WriteLine($"[EYE_TICK] slack forwarded={forwarded}/{newMsgs.Count} — lastTs updated");
            }

            // ── Thread reply detection ──
            swStep.Restart();
            EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] ThreadReplies={swStep.ElapsedMilliseconds}ms");
            Console.WriteLine($"[PROF:SLACK] SlackTotal={swSlack.ElapsedMilliseconds}ms");
            Console.Out.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] slack error: {ex.Message}");
            Console.Out.Flush();
        }
    }

    /// <summary>
    /// Check recent bot messages for new thread replies from users.
    /// Responds when: (1) parent is from bot (클롣) → any user reply, or (2) @mention in reply.
    /// </summary>
    static void EyeTickCheckThreadReplies(List<System.Text.Json.Nodes.JsonNode> channelMessages,
        string botToken, string channel, string? botUserId, ClaudePromptHelper helper)
    {
        try
        {
            // Find bot messages with replies (max 5 threads to check)
            var botThreads = new List<(string threadTs, string parentText)>();
            foreach (var msg in channelMessages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var botId = msg["bot_id"]?.GetValue<string>();
                var subtype = msg["subtype"]?.GetValue<string>();
                var replyCount = msg["reply_count"]?.GetValue<int>() ?? 0;
                var ts = msg["ts"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";

                if (replyCount <= 0) continue;

                // Skip threads older than 1 hour — prevents old message flood on Eye restart
                if (!string.IsNullOrEmpty(ts))
                {
                    if (double.TryParse(ts, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var tsEpoch))
                    {
                        var ageSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - tsEpoch;
                        if (ageSeconds > 3600)
                        {
                            // Silently skip — old thread, no need to scan replies
                            continue;
                        }
                    }
                }

                // Is this a bot message? (either user==botUserId or has bot_id or subtype==bot_message)
                bool isBotMsg = user == botUserId
                    || !string.IsNullOrEmpty(botId)
                    || subtype == "bot_message";
                if (!isBotMsg) continue;

                botThreads.Add((ts, text));
                if (botThreads.Count >= 5) break;
            }

            if (botThreads.Count == 0) return;

            int threadReplies = 0;
            foreach (var (threadTs, parentText) in botThreads)
            {
                // Load per-thread last reply ts
                var threadKey = $"{channel}_t_{threadTs}";
                var lastReplyTs = LoadLastTs(threadKey);
                double lastReplyTsDouble = 0;
                if (!string.IsNullOrEmpty(lastReplyTs))
                    double.TryParse(lastReplyTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out lastReplyTsDouble);

                // Fetch thread replies
                var replies = SlackFetchRepliesAsync(botToken, channel, threadTs, limit: 10)
                    .GetAwaiter().GetResult();

                string? latestReplyTs = null;
                foreach (var reply in replies)
                {
                    var rTs = reply["ts"]?.GetValue<string>() ?? "";
                    var rUser = reply["user"]?.GetValue<string>() ?? "";
                    var rText = reply["text"]?.GetValue<string>() ?? "";
                    var rBotId = reply["bot_id"]?.GetValue<string>();
                    var rSubtype = reply["subtype"]?.GetValue<string>();

                    // Skip parent message itself
                    if (rTs == threadTs) continue;
                    // Skip bot's own replies
                    if (rUser == botUserId || !string.IsNullOrEmpty(rBotId) || rSubtype == "bot_message") continue;
                    // Skip empty
                    if (string.IsNullOrWhiteSpace(rText)) continue;

                    // Skip already processed
                    double.TryParse(rTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var rTsDouble);
                    if (lastReplyTsDouble > 0 && rTsDouble <= lastReplyTsDouble) continue;

                    // Check: @mention tag → always respond
                    bool hasMention = !string.IsNullOrEmpty(botUserId) && rText.Contains($"<@{botUserId}>");

                    // For non-mention replies, parent must be from bot (already filtered above)
                    // Both cases pass — bot thread reply OR @mention

                    // Clean text
                    var cleanReply = System.Text.RegularExpressions.Regex.Replace(rText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (string.IsNullOrWhiteSpace(cleanReply)) continue;

                    // Build context: bot's original message (truncated)
                    var cleanParent = System.Text.RegularExpressions.Regex.Replace(parentText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (cleanParent.Length > 80) cleanParent = cleanParent[..80] + "…";

                    var promptText = $"[쓰레드 시작] {cleanParent}\n\n{cleanReply}\n\n{SlackReplySuffix(rUser, threadTs, "thread reply")}";

                    var fresh = FindSlackPreferredPrompt(helper);
                    if (fresh == null)
                    {
                        Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping thread reply forward");
                        return;
                    }

                    Console.WriteLine($"[EYE_TICK] [FORWARD] Thread @{rUser} → {fresh.HostType} prompt");
                    var ok = helper.TypeAndSubmit(fresh, promptText);
                    if (ok)
                    {
                        threadReplies++;
                        latestReplyTs = rTs;
                        Console.WriteLine($"[EYE_TICK] [DELIVERED] Thread @{rUser}: {cleanReply}");

                        // Send "전달했습니다" ack — deleted when slack reply is sent
                        try
                        {
                            var ackText = $"전달했습니다! (thread={threadTs})";
                            var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, threadTs, username: "앱봇아이")
                                .GetAwaiter().GetResult();
                            if (ackOk && ackTs != null)
                                SavePendingAck(threadTs, channel, ackTs);
                        }
                        catch { }

                        Thread.Sleep(2000);
                    }
                }

                // Update per-thread last reply ts
                if (latestReplyTs != null)
                    SaveLastTs(threadKey, latestReplyTs);
            }

            if (threadReplies > 0)
                Console.WriteLine($"[EYE_TICK] thread_replies={threadReplies} forwarded");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] thread check error: {ex.Message}");
        }
    }

    static string BuildEyeSummary(List<EyeParentCard> cards, EyeTick? latest, string prompt, DateTime kroFileWriteUtc = default)
    {
        var sb = new StringBuilder();

        // ── Action triplet (always on top — real-time accessibility probe) ──
        var (a11y, act, fallback) = ReadLatestActionTriplet();
        if (!string.IsNullOrWhiteSpace(a11y)) sb.AppendLine($"엑빌: {a11y}");
        if (!string.IsNullOrWhiteSpace(act)) sb.AppendLine($"액션: {act}");
        if (!string.IsNullOrWhiteSpace(fallback)) sb.AppendLine($"폴백: {fallback}");

        // ── Build KRO section text (rendered inline with cards by recency) ──
        // KRO sort time = session file mtime (= when kro last wrote to its session JSONL)
        // CardCacheGetTimestamp had a bug: content varied due to whitespace → always UtcNow
        // Now uses file mtime directly — more accurate indicator of kro activity
        string kroBlock = "";
        DateTime kroTsUtc = DateTime.MinValue;
        if (latest != null)
        {
            // Use session file mtime — reflects actual kro write activity
            kroTsUtc = kroFileWriteUtc != default ? kroFileWriteUtc
                : CardCacheGetTimestamp("kro", prompt ?? ""); // fallback if mtime not passed

            var kroSb = new StringBuilder();
            // KRO's home is ~/.openclaw/, not the CLI command's CWD
            var openClawDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw");
            var kroCwd = AbbreviateCwd(openClawDir);
            var (progress, done, next, block) = BuildKroStatus3(latest);
            kroSb.AppendLine($"크로 진행: {progress}" + (string.IsNullOrWhiteSpace(kroCwd) ? "" : $" [{kroCwd}]"));
            kroSb.AppendLine($"크로 완료: {done}");
            kroSb.AppendLine($"크로 예정: {next}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    kroSb.AppendLine($"- —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    kroSb.AppendLine($"- —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            if (!string.IsNullOrWhiteSpace(block))
                kroSb.AppendLine($"크로 이슈: {block}");

            // 크로 생각: OpenClaw session의 최신 assistant/user 텍스트
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var kroThought = prompt.Length > 120 ? prompt[..120] + "..." : prompt;
                kroSb.AppendLine($"크로 생각: {kroThought}");
            }
            kroBlock = kroSb.ToString().TrimEnd();
        }

        // ── Apply CardCache to 클롣 cards — timestamp reflects content change, not tick time ──
        // If tag/status hasn't changed, the card keeps its old timestamp (won't jump to top)
        foreach (var c in cards)
        {
            var clotContent = $"{c.LastTag}|{c.LastStatus}";
            var cwdAbbrev = AbbreviateCwd(c.Cwd);
            var clotKey = !string.IsNullOrEmpty(cwdAbbrev) ? $"clot_{cwdAbbrev}" : $"clot_pid{c.ParentPid}";
            var cachedTs = CardCacheGetTimestamp(clotKey, clotContent);
            if (cachedTs == DateTime.MinValue)
            {
                // First encounter — seed with tick timestamp (new card should appear at correct position)
                cachedTs = c.LastTsUtc;
                _cardCache[clotKey] = (clotContent, cachedTs);
                CardCacheSave(clotKey, clotContent, cachedTs);
            }
            c.LastTsUtc = cachedTs;
        }

        // ── Sort ALL cards (including KRO) by recency — newest on top ──
        // KRO = Claude Code itself (~/.openclaw/), cards = individual CLI commands (each with own CWD)
        // They share the same Claude Desktop host but are separate entities — no dedup needed
        // Hide KRO if its tick is older than 24h (stale — KRO not active)
        bool kroStale = kroTsUtc != DateTime.MinValue && (DateTime.UtcNow - kroTsUtc).TotalHours > 24;
        bool hasKro = !string.IsNullOrWhiteSpace(kroBlock) && !kroStale;
        var sortedCards = cards.OrderByDescending(x => x.LastTsUtc).Take(6).ToList();
        bool kroRendered = !hasKro;  // if no KRO or stale, mark as already rendered

        if (sortedCards.Count == 0 && !hasKro)
        {
            sb.AppendLine("(active cards: 0)");
        }
        else
        {
            // Interleave: render each item in time order (newest first)
            int cardIdx = 0;
            while (cardIdx < sortedCards.Count || !kroRendered)
            {
                bool renderKroNow = false;
                if (!kroRendered)
                {
                    if (cardIdx >= sortedCards.Count)
                        renderKroNow = true;
                    else if (kroTsUtc > sortedCards[cardIdx].LastTsUtc)
                        renderKroNow = true;
                }

                if (renderKroNow)
                {
                    sb.AppendLine(kroBlock);
                    sb.AppendLine("----");
                    kroRendered = true;
                }
                else if (cardIdx < sortedCards.Count)
                {
                    var c = sortedCards[cardIdx++];
                    var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                    var cwdTag = AbbreviateCwd(c.Cwd);
                    var ageText = age < 60 ? $"{age}초 전" : age < 3600 ? $"{age / 60}분 전" : $"{age / 3600}시간 전";

                    // Header: [CWD] or process info
                    var header = string.IsNullOrWhiteSpace(cwdTag)
                        ? (string.IsNullOrWhiteSpace(c.ParentTitle) ? $"{c.ParentName}:{c.ParentPid}" : c.ParentTitle)
                        : cwdTag;
                    sb.AppendLine($"[{header}] {c.ParentName}:{c.ParentPid}");
                    // Context % per card (CWD → session JSONL size → ctx%)
                    var ctxTag = "";
                    var (cardCtx, jsonlAge) = GetContextInfoForCwd(c.Cwd);
                    if (cardCtx >= 0) ctxTag = $" ctx={cardCtx}%";
                    // For prompt-discovered cards, use JSONL age instead of tick age
                    if (c.LastTag == "prompt-discovered" && jsonlAge != null)
                    {
                        var jAge = (int)jsonlAge.Value.TotalSeconds;
                        var jAgeText = jAge < 60 ? $"{jAge}초 전" : jAge < 3600 ? $"{jAge / 60}분 전" : $"{jAge / 3600}시간 전";
                        sb.AppendLine($"클롣 상태: 대기중 ({jAgeText}){ctxTag}");
                    }
                    else if (age > 30)
                    {
                        sb.AppendLine($"클롣 상태: 대기중 ({ageText}){ctxTag}");
                    }
                    else
                    {
                        sb.AppendLine($"클롣 작업: {c.LastTag}");
                        sb.AppendLine($"클롣 상태: {c.LastStatus} ({ageText}){ctxTag}");
                    }
                    // 클롣 생각: CWD별 Claude Code 세션에서 최신 assistant 텍스트
                    var clotThought = ReadClotThoughtForCwd(c.Cwd);
                    if (!string.IsNullOrWhiteSpace(clotThought))
                    {
                        var truncClot = clotThought.Length > 120 ? clotThought[..120] + "..." : clotThought;
                        sb.AppendLine($"클롣 생각: {truncClot}");
                    }
                    // No else — skip "클롣 생각:" if no text found
                    sb.AppendLine("----");
                }
                else
                {
                    break;
                }
            }
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Abbreviate a working directory path for compact display.
    /// Drive letter + first letter of each intermediate folder + "-" + leaf folder.
    /// e.g. "W:\GitHub\WKAppBot" → "WG-WKAppBot"
    ///      "W:\HTS-Project\Source\Main\MainLib" → "WHSM-MainLib"
    ///      "W:\VIGSOne" → "W-VIGSOne"
    /// </summary>
    static string AbbreviateCwd(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        var path = cwd.Replace('\\', '/').TrimEnd('/');
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return "";
        var drive = parts[0].TrimEnd(':').ToUpperInvariant();  // "W:" → "W"
        if (parts.Length <= 1) return drive;
        var leaf = parts[^1];
        // Middle folders → take first char of each as uppercase initial
        var initials = "";
        for (int i = 1; i < parts.Length - 1; i++)
            if (parts[i].Length > 0) initials += char.ToUpperInvariant(parts[i][0]);
        return $"{drive}{initials}-{leaf}";
    }

    /// <summary>
    /// File-based card cache: stores each card's content + last-changed timestamp.
    /// Content-change detection: timestamp only updates when content actually differs.
    /// Survives process restarts (one-shot eye tick, eye restart, etc.)
    /// Files: wkappbot.hq/runtime/card_cache/{cardKey}.json
    /// </summary>
    static string GetCardCacheDir()
    {
        if (string.IsNullOrEmpty(_cardCacheDir))
        {
            _cardCacheDir = Path.Combine(DataDir, "runtime", "card_cache");
            Directory.CreateDirectory(_cardCacheDir);
        }
        return _cardCacheDir;
    }

    /// <summary>
    /// Get the cached changedUtc for a card, updating if content changed.
    /// Returns DateTime.MinValue if card has never been seen.
    /// </summary>
    static DateTime CardCacheGetTimestamp(string cardKey, string currentContent)
    {
        // Memory cache first
        if (_cardCache.TryGetValue(cardKey, out var cached))
        {
            if (cached.content == currentContent)
                return cached.changedUtc;
            // Content changed — update
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Cold start — load from disk
        var diskEntry = CardCacheLoad(cardKey);
        if (diskEntry.HasValue)
        {
            if (diskEntry.Value.content == currentContent)
            {
                _cardCache[cardKey] = diskEntry.Value;
                return diskEntry.Value.changedUtc;
            }
            // Disk content differs from current — content changed
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Never seen — first encounter, use MinValue (card goes to bottom)
        _cardCache[cardKey] = (currentContent, DateTime.MinValue);
        CardCacheSave(cardKey, currentContent, DateTime.MinValue);
        return DateTime.MinValue;
    }

    static void CardCacheSave(string cardKey, string content, DateTime changedUtc)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            var json = JsonSerializer.Serialize(new { content, changedUtc = changedUtc.ToString("O") });
            var tmp = file + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, file, overwrite: true);
        }
        catch { }
    }

    static (string content, DateTime changedUtc)? CardCacheLoad(string cardKey)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            if (!File.Exists(file)) return null;
            var node = JsonNode.Parse(File.ReadAllText(file));
            var content = node?["content"]?.GetValue<string>() ?? "";
            var tsStr = node?["changedUtc"]?.GetValue<string>() ?? "";
            if (DateTime.TryParse(tsStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var ts))
                return (content, ts.ToUniversalTime());
            return (content, DateTime.MinValue);
        }
        catch { return null; }
    }

    static (int x, int y) GetRightmostMonitorAnchor(int width, int height)
    {
        try
        {
            var screens = Screen.AllScreens;
            if (screens != null && screens.Length > 0)
            {
                var rightMost = screens.OrderByDescending(s => s.Bounds.Right).First();
                var x = rightMost.Bounds.Right - width - 10;
                var y = rightMost.Bounds.Top + 10;
                return (x, y);
            }
        }
        catch { }

        return (1530, 110);
    }

    static EyeTick? ReadLatestTick(out long readMs, out long parseMs)
    {
        readMs = 0; parseMs = 0;
        var path = EyeTicksPath;
        if (!File.Exists(path)) return null;

        var swRead = Stopwatch.StartNew();
        var lines = ReadTailLinesShared(path, 64 * 1024);
        swRead.Stop();
        readMs = swRead.ElapsedMilliseconds;

        var swParse = Stopwatch.StartNew();
        var start = _lastEyeTickFile == path ? Math.Max(0, _lastEyeTickLineIndex - 1) : 0;
        for (int i = lines.Length - 1; i >= start; i--)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t != null)
                {
                    _lastEyeTickFile = path;
                    _lastEyeTickLineIndex = i;
                    swParse.Stop();
                    parseMs = swParse.ElapsedMilliseconds;
                    return t;
                }
            }
            catch { }
        }
        swParse.Stop();
        parseMs = swParse.ElapsedMilliseconds;
        return null;
    }

    static string ReadLatestOpenClawPromptPreview(PromptDiag diag)
    {
        var sw = Stopwatch.StartNew();
        var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
        if (!Directory.Exists(sessionsDir)) return "";

        var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
            .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
            .FirstOrDefault();
        diag.StatMs = sw.ElapsedMilliseconds;
        if (string.IsNullOrWhiteSpace(latestFile)) return "";

        var fi = new FileInfo(latestFile);
        diag.FileWriteUtc = fi.LastWriteTimeUtc; // expose session file mtime for kro sort
        if (latestFile == _lastPromptSessionFile && fi.Length == _lastPromptSessionLength && fi.LastWriteTimeUtc == _lastPromptSessionWriteUtc)
        {
            diag.CacheMs = 1;
            diag.Source = "sessions-cache";
            return _lastPromptPreviewCache;
        }

        string selected = "";
        int selectedLine = -1;
        var windows = new[] { 64 * 1024, 128 * 1024, 256 * 1024 };

        foreach (var tailBytes in windows)
        {
            sw.Restart();
            var lines = ReadTailLinesShared(latestFile, tailBytes);
            diag.ReadMs += sw.ElapsedMilliseconds;

            sw.Restart();
            string bestAssistant = "";
            string bestUser = "";
            int bestLine = -1;
            var start = latestFile == _lastPromptSessionFile ? Math.Max(0, _lastPromptLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("\"role\"", StringComparison.OrdinalIgnoreCase)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase) && !line.Contains("user", StringComparison.OrdinalIgnoreCase)) continue;

                var parseStart = Stopwatch.StartNew();
                if (TryExtractRoleAndText(line, out var role, out var text))
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;

                    var nsw = Stopwatch.StartNew();
                    text = NormalizePrompt(text);
                    nsw.Stop();
                    diag.NormMs += nsw.ElapsedMilliseconds;
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                    {
                        bestAssistant = text;
                        bestLine = i;
                        break;
                    }
                    if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                    {
                        bestUser = text;
                        if (bestLine < 0) bestLine = i;
                    }
                }
                else
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;
                }
            }
            diag.ScanMs += sw.ElapsedMilliseconds;

            selected = !string.IsNullOrWhiteSpace(bestAssistant) ? bestAssistant : bestUser;
            selectedLine = bestLine;
            if (!string.IsNullOrWhiteSpace(selected)) break;
        }

        sw.Restart();
        _lastPromptSessionFile = latestFile;
        _lastPromptLineIndex = selectedLine;
        _lastPromptPreviewCache = selected;
        _lastPromptSessionLength = fi.Length;
        _lastPromptSessionWriteUtc = fi.LastWriteTimeUtc;
        diag.CacheMs = sw.ElapsedMilliseconds;
        diag.Source = string.IsNullOrWhiteSpace(selected) ? "none" : "sessions";
        return selected;
    }

    /// <summary>
    /// Read "클롣 생각" from Claude Code session JSONL for a specific CWD.
    /// CWD → project dir name mapping: "W:\GitHub\WKAppBot" → "W--GitHub-WKAppBot"
    /// Uses ReadTailLinesShared (FileShare.ReadWrite, no file lock) — reads only last 64KB.
    /// Session files can be 35MB+ so tail-only reading is critical.
    /// Per-CWD cache to avoid re-reading unchanged files.
    /// </summary>
    static readonly Dictionary<string, (string file, string preview, long len, DateTime mtime)> _clotThoughtCache = new();

    /// <summary>Extract CWD from VS Code window title. Pattern: "... - FolderName - Visual Studio Code"</summary>
    static string? ExtractCwdFromVsCodeTitle(string title)
    {
        // VS Code title: "file.cs - WKAppBot - Visual Studio Code" → "WKAppBot"
        // Then try to find matching directory in common roots
        // Handle suffixes: "... - Visual Studio Code", "... - Visual Studio Code - Modified", etc.
        var vscIdx = title.IndexOf(" - Visual Studio Code", StringComparison.OrdinalIgnoreCase);
        if (vscIdx < 0) return null;
        var withoutSuffix = title[..vscIdx]; // "file.cs - WKAppBot"
        var lastDash = withoutSuffix.LastIndexOf(" - ", StringComparison.Ordinal);
        var folderName = lastDash >= 0 ? withoutSuffix[(lastDash + 3)..].Trim() : withoutSuffix.Trim();
        if (string.IsNullOrEmpty(folderName)) return null;

        // Search common code roots for a directory matching this name
        var searchRoots = new[] { @"W:\GitHub", @"W:\HTS_Project", @"C:\Users\edenc\projects" };
        foreach (var root in searchRoots)
        {
            var candidate = Path.Combine(root, folderName);
            if (Directory.Exists(candidate)) return candidate;
        }
        // Fallback: reverse-map from ~/.claude/projects/ dir names
        // e.g. "w--GitHub-WKAppBot" → ends with "-WKAppBot" → match "WKAppBot"
        // Then reverse: "w--GitHub-WKAppBot" → "w:\GitHub\WKAppBot"
        var cpDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects");
        if (Directory.Exists(cpDir))
        {
            var suffix2 = "-" + folderName.Replace('_', '-');
            foreach (var dir in Directory.GetDirectories(cpDir))
            {
                var dirName = Path.GetFileName(dir);
                if (dirName != null && dirName.EndsWith(suffix2, StringComparison.OrdinalIgnoreCase))
                {
                    // Reverse: "w--GitHub-WKAppBot" → "w:\GitHub\WKAppBot"
                    var reversed = dirName.Replace("--", ":\\", StringComparison.Ordinal)
                                          .Replace('-', '\\');
                    // Underscores were replaced with dashes in the mapping, can't perfectly reverse
                    if (Directory.Exists(reversed)) return reversed;
                    return reversed; // best effort — GetContextPctForCwd will re-map anyway
                }
            }
        }
        return null;
    }

    /// <summary>Get context usage % and JSONL age for a specific CWD's session.</summary>
    static (int pct, TimeSpan? age) GetContextInfoForCwd(string? cwd)
    {
        var (pct, age, _, _) = GetContextInfoForCwdEx(cwd);
        return (pct, age);
    }

    /// <summary>Extended version: also returns JSONL path and file size (for context monitor dedup)</summary>
    static (int pct, TimeSpan? age, string? jsonlPath, long fileSize) GetContextInfoForCwdEx(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return (-1, null, null, 0);
        try
        {
            var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
            var projDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects", projName);
            if (!Directory.Exists(projDir)) return (-1, null, null, 0);

            // Find most recently modified JSONL (= active session)
            string? latestFile = null;
            DateTime latestMtime = DateTime.MinValue;
            foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
            {
                try
                {
                    var mtime = File.GetLastWriteTimeUtc(jsonl);
                    if (mtime > latestMtime) { latestMtime = mtime; latestFile = jsonl; }
                }
                catch { }
            }
            if (latestFile == null) return (-1, null, null, 0);
            var size = new FileInfo(latestFile).Length;
            var pct = size > 0 ? (int)(size / (1024.0 * 1024.0) / 40.0 * 100) : -1;
            var age = DateTime.UtcNow - latestMtime;
            return (pct, age, latestFile, size);
        }
        catch { return (-1, null, null, 0); }
    }

    static string ReadClotThoughtForCwd(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        try
        {
            // CWD → project dir name: "W:\HTS_Project\..." → "w--HTS-Project-..."
            // Claude Code mapping: ':' → '-', '\' → '-', '/' → '-', '_' → '-'
            // Drive letter case varies (w-- or W--), Windows FS is case-insensitive so OK
            var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
            var projDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects", projName);
            if (!Directory.Exists(projDir)) return "";

            // Find most recently modified .jsonl in this project
            string? latestFile = null;
            DateTime latestMtime = DateTime.MinValue;
            foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
            {
                var mtime = File.GetLastWriteTimeUtc(jsonl);
                if (mtime > latestMtime) { latestMtime = mtime; latestFile = jsonl; }
            }
            if (latestFile == null) return "";

            // Cache check per CWD
            var fi = new FileInfo(latestFile);
            if (_clotThoughtCache.TryGetValue(cwd, out var cached) &&
                cached.file == latestFile && cached.len == fi.Length && cached.mtime == fi.LastWriteTimeUtc)
                return cached.preview;

            // Lightweight: 8KB first, 32KB fallback if needed
            string selected = "";
            foreach (var tailSize in new[] { 8 * 1024, 32 * 1024 })
            {
                var lines = ReadTailLinesShared(latestFile, tailSize);
                string bestAssistant = "";
                string bestUser = "";
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (!line.Contains("\"role\"", StringComparison.OrdinalIgnoreCase)) continue;

                    if (TryExtractRoleAndText(line, out var role, out var text))
                    {
                        text = NormalizePrompt(text);
                        if (string.IsNullOrWhiteSpace(text)) continue;

                        if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                        {
                            bestAssistant = text;
                            break;
                        }
                        if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                            bestUser = $"유저> {text}";
                    }
                }
                selected = !string.IsNullOrWhiteSpace(bestAssistant) ? bestAssistant : bestUser;
                if (!string.IsNullOrWhiteSpace(selected)) break;
            }
            _clotThoughtCache[cwd] = (latestFile, selected, fi.Length, fi.LastWriteTimeUtc);
            return selected;
        }
        catch { return ""; }
    }

    static bool TryExtractRoleAndText(string jsonLine, out string role, out string text)
    {
        role = "";
        text = "";
        try
        {
            using var doc = JsonDocument.Parse(jsonLine);
            var root = doc.RootElement;
            if (!root.TryGetProperty("message", out var msg)) return false;
            if (!msg.TryGetProperty("role", out var roleEl)) return false;
            role = roleEl.GetString() ?? "";
            if (!msg.TryGetProperty("content", out var content) || content.ValueKind != JsonValueKind.Array) return false;

            string? toolSummary = null; // fallback: tool_use abbreviated
            foreach (var c in content.EnumerateArray())
            {
                if (c.TryGetProperty("type", out var typeEl))
                {
                    var type = (typeEl.GetString() ?? "").ToLowerInvariant();
                    if (type is "text" or "output_text" or "input_text" or "summary_text")
                    {
                        if (c.TryGetProperty("text", out var txtEl))
                        {
                            text = txtEl.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                    // tool_use → abbreviated summary: "Bash: wkappbot ..." / "Read: file.cs"
                    if (type == "tool_use" && toolSummary == null && c.TryGetProperty("name", out var nameEl))
                    {
                        var toolName = nameEl.GetString() ?? "";
                        var brief = "";
                        if (c.TryGetProperty("input", out var inp) && inp.ValueKind == JsonValueKind.Object)
                        {
                            // Extract first useful field: command, file_path, pattern, etc.
                            foreach (var prop in inp.EnumerateObject())
                            {
                                if (prop.Value.ValueKind == JsonValueKind.String)
                                {
                                    var v = prop.Value.GetString() ?? "";
                                    if (v.Length > 60) v = v[..60] + "...";
                                    brief = v;
                                    break;
                                }
                            }
                        }
                        toolSummary = string.IsNullOrWhiteSpace(brief) ? $"🔧{toolName}" : $"🔧{toolName}: {brief}";
                    }
                }

                if (c.TryGetProperty("text", out var txtAny))
                {
                    text = txtAny.GetString() ?? "";
                    if (!string.IsNullOrWhiteSpace(text)) return true;
                }

                if (c.TryGetProperty("summary", out var summary) && summary.ValueKind == JsonValueKind.Array)
                {
                    foreach (var s in summary.EnumerateArray())
                    {
                        if (s.TryGetProperty("text", out var st))
                        {
                            text = st.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                }
            }
            // No text found — use tool_use summary as fallback
            if (toolSummary != null) { text = toolSummary; return true; }
            return false;
        }
        catch { return false; }
    }

    static readonly Regex _multiSpaceRx = new(@"\s{2,}", RegexOptions.Compiled);

    static string NormalizePrompt(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        // Collapse all whitespace (newlines, tabs, consecutive spaces) → single space
        var t = _multiSpaceRx.Replace(s, " ").Trim();
        if (t.Equals("NO_REPLY", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("telegram send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Equals("ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        // Filter eye tick profiling / diagnostic noise from "생각" display
        if (t.Contains("tick=") && t.Contains("ms(")) return "";
        if (t.StartsWith("[EYE_TICK]", StringComparison.Ordinal)) return "";
        if (t.StartsWith("[ACT]", StringComparison.Ordinal)) return "";
        if (t.Length > 200) t = t[..200] + "...";
        return t;
    }

    static string CompressPlanTitle(string s, int maxLen = 34)
    {
        var t = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (t.Length > maxLen) t = t[..maxLen] + "...";
        return t;
    }

    static List<string> ExtractPlanOutlineItems(string text, int maxItems)
    {
        var items = new List<string>();
        if (string.IsNullOrWhiteSpace(text)) return items;

        var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

        bool inBlock = false;
        for (int i = 0; i < lines.Count; i++)
        {
            var ln = lines[i];
            if (ln.Equals("[KRO_PLAN_BEGIN]", StringComparison.OrdinalIgnoreCase)) { inBlock = true; continue; }
            if (ln.Equals("[KRO_PLAN_END]", StringComparison.OrdinalIgnoreCase)) break;

            if (ln.StartsWith("PLAN:", StringComparison.OrdinalIgnoreCase))
            {
                var head = ln[5..].Trim();
                if (!string.IsNullOrWhiteSpace(head))
                {
                    // support single-line style: PLAN: title - item1 - item2 ...
                    var parts = head.Split(" - ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (parts.Length > 0)
                    {
                        items.Add(CompressPlanTitle(parts[0]));
                        for (int p = 1; p < parts.Length && items.Count < maxItems; p++)
                            items.Add(CompressPlanTitle(parts[p]));
                    }
                    else
                    {
                        items.Add(CompressPlanTitle(head));
                    }
                }
                inBlock = true;
                continue;
            }

            if (inBlock && (ln.StartsWith("-") || ln.StartsWith("•") || ln.StartsWith("1)") || ln.StartsWith("2)") || ln.StartsWith("3)")))
            {
                var body = ln.TrimStart('-', '•', ' ').Trim();
                if (!string.IsNullOrWhiteSpace(body)) items.Add(CompressPlanTitle(body));
                if (items.Count >= maxItems) break;
            }
        }

        return items.Take(maxItems).ToList();
    }

    static List<string> ExtractRecentPlanItems(int maxItems = 3)
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir)) return new List<string>();

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(latestFile)) return new List<string>();

            var fi = new FileInfo(latestFile);
            if (latestFile == _lastPlanSessionFile && fi.Length == _lastPlanSessionLength && fi.LastWriteTimeUtc == _lastPlanSessionWriteUtc)
                return _lastPlanItemsCache.Take(maxItems).ToList();

            var lines = ReadTailLinesShared(latestFile, 256 * 1024);
            var start = latestFile == _lastPlanSessionFile ? Math.Max(0, _lastPlanLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            var outItems = new List<string>();
            int foundLine = -1;
            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase)) continue;
                if (!TryExtractRoleAndText(line, out var role, out var text)) continue;
                if (!string.Equals(role, "assistant", StringComparison.OrdinalIgnoreCase)) continue;

                // 1) preferred: explicit plan-format outline
                var outline = ExtractPlanOutlineItems(text, maxItems);
                if (outline.Count > 0)
                {
                    // newest valid plan block wins: do not mix with older plans
                    outItems.Clear();
                    foreach (var it in outline.Take(maxItems))
                        outItems.Add(it);
                    foundLine = i;
                    break;
                }

                // 2) strict mode: no heuristic promotion
                // only explicit plan-format outputs are allowed for EYE_PLAN
                continue;
            }

            _lastPlanSessionFile = latestFile;
            _lastPlanLineIndex = foundLine;
            _lastPlanSessionLength = fi.Length;
            _lastPlanSessionWriteUtc = fi.LastWriteTimeUtc;
            _lastPlanItemsCache = outItems;

            return outItems.Take(maxItems).ToList();
        }
        catch
        {
            return new List<string>();
        }
    }

    static (string a11y, string act, string fallback) ReadLatestActionTriplet()
    {
        try
        {
            var logDir = Path.Combine(DataDir, "logs");
            if (!Directory.Exists(logDir)) return ("", "", "");

            var files = Directory.GetFiles(logDir, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(p => !Path.GetFileName(p).Contains(".eye."))
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .Take(3)
                .ToList();

            string a11y = "", act = "", fallback = "";
            foreach (var f in files)
            {
                var lines = ReadTailLinesShared(f, 32 * 1024);
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var ln = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(a11y) && ln.StartsWith("[A11Y]")) a11y = ln[6..].Trim();
                    if (string.IsNullOrWhiteSpace(act) && ln.StartsWith("[ACT]")) act = ln[5..].Trim();
                    if (string.IsNullOrWhiteSpace(fallback) && ln.StartsWith("[FALLBACK]")) fallback = ln[10..].Trim();
                    if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                        break;
                }
                if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                    break;
            }

            a11y = CompressPlanTitle(a11y, 46);
            act = CompressPlanTitle(act, 46);
            fallback = CompressPlanTitle(fallback, 46);
            return (a11y, act, fallback);
        }
        catch
        {
            return ("", "", "");
        }
    }

    static string[] ReadTailLinesShared(string path, int tailBytes)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var len = fs.Length;
            var start = Math.Max(0, len - tailBytes);
            fs.Seek(start, SeekOrigin.Begin);
            using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var txt = sr.ReadToEnd();
            if (start > 0)
            {
                var idx = txt.IndexOf('\n');
                if (idx >= 0 && idx + 1 < txt.Length)
                    txt = txt[(idx + 1)..];
            }
            return txt.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
        catch { return Array.Empty<string>(); }
    }

    // Commands to skip when building idle-after text (meta + communication noise)
    static readonly HashSet<string> _idleSkipCommands = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "slack", "tick" };

    /// <summary>
    /// Get age (seconds since last modification) of the most recent Claude Code session JSONL.
    /// Claude Code writes to ~/.claude/projects/{project}/*.jsonl during ALL activity
    /// (file reads, edits, builds, tool calls) — not just wkappbot commands.
    /// Returns 9999 if no session file found.
    /// </summary>
    static double GetClaudeCodeSessionAge()
    {
        try
        {
            var claudeDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects");
            if (!Directory.Exists(claudeDir)) return 9999;

            // Find most recently modified .jsonl across all projects
            DateTime latestUtc = DateTime.MinValue;
            foreach (var projDir in Directory.GetDirectories(claudeDir))
            {
                try
                {
                    foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
                    {
                        var mtime = File.GetLastWriteTimeUtc(jsonl);
                        if (mtime > latestUtc) latestUtc = mtime;
                    }
                }
                catch { }
            }

            if (latestUtc == DateTime.MinValue) return 9999;
            return (DateTime.UtcNow - latestUtc).TotalSeconds;
        }
        catch { return 9999; }
    }

    /// <summary>
    /// Read the last meaningful tick's tag from eye_ticks.jsonl.
    /// Skips meta tags AND communication commands (slack, eye) to find actual work.
    /// Returns like "publish/build" or "inspect" — shown as idle-after text.
    /// </summary>
    static string? GetLastTickTag()
    {
        try
        {
            if (!File.Exists(EyeTicksPath)) return null;
            var lines = ReadTailLinesShared(EyeTicksPath, 8192); // ~last 30 ticks (may need deeper scan)
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var t = System.Text.Json.JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t == null) continue;
                // Skip meta tags (eye, snapshot, validate, help)
                if (IsMetaTag(t.Tag)) continue;
                // Skip communication commands (slack send/reply, eye tick)
                if (_idleSkipCommands.Contains(t.Command ?? "")) continue;
                var tag = t.Tag ?? "";
                // Include command for context: "publish/build" or "run/click"
                if (!string.IsNullOrEmpty(t.Command) && !tag.StartsWith(t.Command))
                    return $"{t.Command}/{tag}";
                return tag;
            }
        }
        catch { }
        return null;
    }

    static (string progress, string done, string next, string block) BuildKroStatus3(EyeTick t)
    {
        var report = $"{t.Command}/{t.Tag}";
        var status = (t.Status ?? "").ToLowerInvariant();
        var aiRecent = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds <= 45;

        if (status == "start")
            return (report, "작업 시작", "응답 처리 중", "");

        if (status.StartsWith("step:"))
        {
            if (aiRecent)
                return (report, "최근 대화 수신", "응답 처리 중", "");
            return (report, "중간 단계", "상태 갱신 대기", "");
        }

        if (status.StartsWith("end:"))
        {
            var codeText = status[4..].Trim();
            if (int.TryParse(codeText, out var code) && code == 0)
            {
                if (aiRecent)
                    return ("응답 처리 중", "최근 대화 수신", "후속 입력 대기", "");
                return (report, "작업 완료", "다음 작업 대기", "");
            }
            return (report, "오류 종료", "에러 분석/폴백", $"종료코드 {codeText}");
        }

        if (aiRecent)
            return (report, "최근 대화 수신", "응답 처리 중", "");

        return (report, "상태 대기", "상태 갱신 대기", "");
    }

    // Meta tags: diagnostic/overhead commands that shouldn't hide meaningful work in cards
    // e.g. "eye tick" checks status, "snapshot" captures state — not the real work itself
    static readonly HashSet<string> _metaTags = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "snapshot", "eye tick", "validate", "help" };

    static bool IsMetaTag(string? tag) =>
        !string.IsNullOrWhiteSpace(tag) && _metaTags.Contains(tag!);

    /// <summary>
    /// Dead-card detector + WM_NULL health check.
    /// For each card:
    ///   1. If PID/HWND is gone → [DEAD_CARD] Slack alert + zombie kill attempt.
    ///   2. If PID exists but WM_NULL > 100ms → [SLOW_CARD] Slack alert + card marked "불량".
    /// Results cached in _cardHealthCache; dead pids cached in _reportedDeadPids to suppress repeats.
    /// </summary>
    static void CheckAndReportDeadCards(List<EyeParentCard> cards)
    {
        foreach (var card in cards.ToList())
        {
            var pid = card.ParentPid;
            if (pid <= 0) continue;

            // ── Step 1: is the process/window still alive? ──
            bool isPromptDiscovered = card.LastTag == "prompt-discovered";
            bool alive;
            IntPtr hwnd = IntPtr.Zero;
            if (isPromptDiscovered)
            {
                // ParentPid = HWND for prompt-discovered cards
                hwnd = (IntPtr)pid;
                alive = WKAppBot.Win32.Native.NativeMethods.IsWindow(hwnd);
            }
            else
            {
                try
                {
                    using var p = Process.GetProcessById(pid);
                    alive = !p.HasExited;
                    // Find any top-level HWND for this PID (for WM_NULL health check)
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((h, _) =>
                    {
                        WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(h, out uint wpid);
                        if (wpid == (uint)pid && hwnd == IntPtr.Zero) hwnd = h;
                        return hwnd == IntPtr.Zero; // stop once found
                    }, IntPtr.Zero);
                }
                catch { alive = false; }
            }

            if (!alive)
            {
                if (_reportedDeadPids.Add(pid))
                {
                    var cwdTag = AbbreviateCwd(card.Cwd);
                    var label = string.IsNullOrEmpty(cwdTag) ? $"{card.ParentName}:{pid}" : $"[{cwdTag}] {card.ParentName}:{pid}";
                    Console.WriteLine($"[DEAD_CARD] {label} — process gone");
                    _cardHealthCache[pid] = "dead";
                    // Zombie cleanup attempt (no-op if already gone)
                    try { Process.GetProcessById(pid).Kill(); } catch { }
                }
                cards.Remove(card);
                continue;
            }

            // ── Step 2: WM_NULL health check (0 = skip if no HWND found) ──
            if (hwnd == IntPtr.Zero) continue;
            if (_reportedDeadPids.Contains(pid)) continue;

            var sw = Stopwatch.StartNew();
            WKAppBot.Win32.Native.NativeMethods.SendMessageTimeoutW(
                hwnd, WKAppBot.Win32.Native.NativeMethods.WM_NULL,
                IntPtr.Zero, IntPtr.Zero,
                WKAppBot.Win32.Native.NativeMethods.SMTO_ABORTIFHUNG,
                100, out _);
            sw.Stop();

            // health% = max(0, 100 - responseMs): 1ms→99%, 99ms→1%, 100ms+→0%(불량)
            var responseMs = (int)sw.ElapsedMilliseconds;
            var healthPct = Math.Max(0, 100 - responseMs);
            var health = healthPct == 0 ? "불량" : (healthPct < 50 ? "느림" : "ok");

            var prevHealth = _cardHealthCache.GetValueOrDefault(pid, "ok");
            _cardHealthCache[pid] = health;

            if (health == "불량" && prevHealth != "불량")
            {
                var cwdTag = AbbreviateCwd(card.Cwd);
                var label = string.IsNullOrEmpty(cwdTag) ? $"{card.ParentName}:{pid}" : $"[{cwdTag}] {card.ParentName}:{pid}";
                Console.WriteLine($"[SLOW_CARD] {label} — WM_NULL={responseMs}ms (건강{healthPct}%)");
            }
            else if (health != "불량" && prevHealth == "불량")
            {
                Console.WriteLine($"[HEALTH] {card.ParentName}:{pid} recovered → {responseMs}ms (건강{healthPct}%)");
            }

            // Annotate card for display (show health% if not perfect)
            if (healthPct < 100)
                card.LastStatus = $"{card.LastStatus} [건강{healthPct}%]".Trim();
        }
    }

    /// <summary>Supplement tick-based cards with VS Code windows that have no tick (idle Claude Code sessions).</summary>
    static void SupplementCardsFromPrompts(List<EyeParentCard> cards)
    {
        try
        {
            var cardCwds = new HashSet<string>(cards.Select(c => c.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/')),
                StringComparer.OrdinalIgnoreCase);

            // 1s cooldown after last scan — FindAllPrompts is fast now (per-hwnd UIA cache in ClaudePromptHelper),
            // but EnumWindows + process name lookup still costs ~5-20ms per tick; limit to once/second.
            List<ClaudePromptHelper.PromptInfo> allPrompts;
            var now = DateTime.UtcNow;
            if (_cachedAllPrompts != null && (now - _lastFindAllPromptsAt).TotalMilliseconds < 1000)
            {
                allPrompts = _cachedAllPrompts;
            }
            else
            {
                var sw = Stopwatch.StartNew();
                using var discHelper = new ClaudePromptHelper();
                allPrompts = discHelper.FindAllPrompts();
                sw.Stop();
                _cachedAllPrompts = allPrompts;
                _lastFindAllPromptsAt = DateTime.UtcNow; // cooldown starts after scan completes
                if (sw.ElapsedMilliseconds > 50)
                    Console.WriteLine($"[EYE] FindAllPrompts scan: {sw.ElapsedMilliseconds}ms ({allPrompts.Count} prompts)");
            }
            foreach (var p in allPrompts)
            {
                if (p.HostType != "vscode-claudecode" && p.HostType != "Code") continue;
                var cwd = ExtractCwdFromVsCodeTitle(p.WindowTitle);
                if (string.IsNullOrEmpty(cwd)) continue;
                var cwdKey = cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                if (cardCwds.Contains(cwdKey)) continue;
                cards.Add(new EyeParentCard
                {
                    ParentPid = p.WindowHandle.ToInt32(),
                    ParentName = "Code",
                    ParentTitle = p.WindowTitle,
                    LastTag = "prompt-discovered",
                    LastStatus = "idle",
                    LastTsUtc = DateTime.UtcNow.AddMinutes(-1),
                    Cwd = cwd,
                });
                cardCwds.Add(cwdKey);
            }
        }
        catch { }
    }

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 86400)
    {
        // KEY = normalized CWD (folder-based grouping: same folder = one card, survives PID restart)
        var cards = new Dictionary<string, EyeParentCard>(StringComparer.OrdinalIgnoreCase);
        var path = EyeTicksPath;
        if (!File.Exists(path)) return cards.Values.ToList();

        // 2MB tail: enough for ~24h of ticks (each tick ~200 bytes, tick every ~10s = ~1.7MB/day)
        var lines = ReadTailLinesShared(path, 64 * 1024);
        var now = DateTime.UtcNow;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(line);
                if (t == null) continue;
                if (!DateTime.TryParse(t.Ts, out var tsLocal)) continue;
                var tsUtc = tsLocal.ToUniversalTime();
                var ppid = t.HostPid > 0 ? t.HostPid : (t.ParentPid > 0 ? t.ParentPid : t.Pid);
                var pname = !string.IsNullOrWhiteSpace(t.HostName) ? t.HostName : (string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName);
                var ptitle = t.HostTitle ?? "";

                // CWD-based key: normalize to lowercase forward-slash, trimmed
                var cwdRaw = (t.Cwd ?? "").Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                var cardKey = string.IsNullOrEmpty(cwdRaw) ? $"pid_{ppid}" : cwdRaw;

                // Always update timestamp with latest tick
                // But for tag/status: meta tags (eye, snapshot) don't overwrite meaningful work tags
                if (!cards.TryGetValue(cardKey, out var c))
                {
                    // First tick for this CWD — always accept
                    cards[cardKey] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        ParentTitle = ptitle,
                        LastTag = t.Tag,
                        LastStatus = t.Status,
                        LastTsUtc = tsUtc,
                        Cwd = t.Cwd ?? "",
                    };
                }
                else if (tsUtc > c.LastTsUtc)
                {
                    // Newer tick — always update timestamp and process info
                    c.LastTsUtc = tsUtc;
                    c.ParentPid = ppid; // latest PID for this CWD
                    c.ParentName = pname;
                    c.ParentTitle = ptitle;
                    if (!string.IsNullOrWhiteSpace(t.Cwd)) c.Cwd = t.Cwd;

                    // Only update tag/status if:
                    // 1. New tick is non-meta (real work always wins), OR
                    // 2. Existing tag is also meta (meta→meta is fine)
                    if (!IsMetaTag(t.Tag) || IsMetaTag(c.LastTag))
                    {
                        c.LastTag = t.Tag;
                        c.LastStatus = t.Status;
                    }
                }
            }
            catch { }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
    }

    /// <summary>
    /// Extract the CWD from a JSONL file by reading its first few lines.
    /// Returns null if CWD not found.
    /// </summary>
    static string? ExtractCwdFromJsonl(string jsonlPath)
    {
        try
        {
            using var sr = new StreamReader(jsonlPath);
            for (int i = 0; i < 20 && !sr.EndOfStream; i++)
            {
                var line = sr.ReadLine();
                if (line == null) break;
                // Look for "cwd":"W:\\GitHub\\WKAppBot" pattern
                var m = System.Text.RegularExpressions.Regex.Match(line, "\"cwd\":\"([^\"]+)\"");
                if (m.Success)
                {
                    return m.Groups[1].Value.Replace("\\\\", "\\");
                }
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Build a handoff prompt for a new chat session.
    /// Reads the tail of the current JSONL session to extract recent context,
    /// then constructs a continuation prompt in English (token-efficient) with Korean response instruction.
    /// </summary>
    static string BuildHandoffPrompt(string jsonlPath)
    {
        // Extract last few user/assistant messages from JSONL for context
        var recentMessages = new List<string>();
        try
        {
            // Read last ~50KB of the file (tail) to find recent conversation
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 50 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            // Skip partial first line
            if (fs.Position > 0) sr.ReadLine();

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    var type = node["type"]?.GetValue<string>();

                    // Capture human/assistant summary messages
                    if (role == "human" || role == "user")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 200) content = content[..200] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[USER] {content}");
                    }
                    else if (role == "assistant")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 300) content = content[..300] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[ASSISTANT] {content}");
                    }
                }
                catch { /* skip unparseable lines */ }
            }
        }
        catch (Exception ex)
        {
            recentMessages.Add($"(Failed to read session: {ex.Message})");
        }

        // Keep only the last ~10 messages for context
        if (recentMessages.Count > 10)
            recentMessages = recentMessages.Skip(recentMessages.Count - 10).ToList();

        var contextBlock = recentMessages.Count > 0
            ? string.Join("\n", recentMessages)
            : "(no recent messages extracted)";

        // Build handoff prompt (English for token efficiency, Korean response requested)
        var handoffPrompt = $@"This is an AUTO-RELAY from AppBotEye. The previous session hit 95% context limit and was automatically handed off.

## Instructions
1. Read CLAUDE.md and MEMORY.md first to understand the project
2. Continue the work from where the previous session left off
3. Reply in Korean (한국어로 답변해주세요)
4. Send a Slack message to let the user know you're continuing: `wkappbot slack send ""새 채팅에서 이어갑니다! (auto-relay) 🔄""`
5. Check recent Slack messages for any user instructions: look at eye tick data

## Recent conversation context from previous session:
```
{contextBlock}
```

## Session info
- Previous JSONL: {Path.GetFileName(jsonlPath)}
- Relay timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- Relay reason: context 95% limit reached

Please start by reading CLAUDE.md, then summarize what you understand about the pending work and continue.

(auto-relay by AppBotEye context monitor)";

        return handoffPrompt;
    }

    /// <summary>Overload: includes active card data + plan files for richer handoff.</summary>
    static string BuildHandoffPrompt(string jsonlPath, List<EyeParentCard> cards, double sizeMB, double limitMB)
    {
        var sb = new StringBuilder();
        sb.AppendLine("이전 세션이 컨텍스트 90%에 도달하여 자동 인수인계합니다.");
        sb.AppendLine("CLAUDE.md와 MEMORY.md를 먼저 읽고 이어서 작업해주세요.");
        sb.AppendLine();

        // Active cards = what clots were working on
        if (cards.Count > 0)
        {
            sb.AppendLine("## 활성 클롣 카드:");
            foreach (var c in cards.Take(5))
            {
                var cwd = AbbreviateCwd(c.Cwd);
                var info = !string.IsNullOrWhiteSpace(cwd) ? cwd : c.ParentName;
                sb.AppendLine($"- [{info}] {c.LastTag}: {c.LastStatus}");
            }
            sb.AppendLine();
        }

        // Plan files
        try
        {
            var plansDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "plans");
            if (Directory.Exists(plansDir))
            {
                var recentPlan = Directory.GetFiles(plansDir, "*.md")
                    .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
                    .FirstOrDefault();
                if (recentPlan != null)
                {
                    var age = (DateTime.UtcNow - File.GetLastWriteTimeUtc(recentPlan)).TotalHours;
                    if (age < 24)
                        sb.AppendLine($"## 미완료 플랜: {Path.GetFileName(recentPlan)} ({age:F0}시간 전)");
                }
            }
        }
        catch { }

        sb.AppendLine();
        sb.AppendLine($"세션: {Path.GetFileName(jsonlPath)} ({sizeMB:F1}/{limitMB}MB)");
        sb.AppendLine($"시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine();
        sb.AppendLine("⚡ 중요: 채팅방 제목을 참신하고 재밌게 지어주세요! 매번 '인수인계'같은 뻔한 제목 금지!");
        sb.AppendLine("슬랙으로 인수인계 완료 알림 보내주세요: wkappbot slack send '새 세션에서 이어갑니다 🔄'");

        return sb.ToString().Trim();
    }

    /// <summary>
    /// Build a handoff section to write into CLAUDE.md.
    /// Written in Korean so the user can also read it ("나도 보게 ㅋ").
    /// The next Claude session reads CLAUDE.md on startup and sees this section.
    /// </summary>
    static string BuildHandoffSection(string jsonlPath, string handoffPrompt)
    {
        // Extract recent context summary from JSONL
        var recentLines = new List<string>();
        try
        {
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 30 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            if (fs.Position > 0) sr.ReadLine(); // skip partial

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    if (role == "human" || role == "user")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **User**: {c}");
                    }
                    else if (role == "assistant")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **Claude**: {c}");
                    }
                }
                catch { }
            }
        }
        catch { recentLines.Add("  - (failed to read session)"); }

        // Keep last 8 messages
        if (recentLines.Count > 8)
            recentLines = recentLines.Skip(recentLines.Count - 8).ToList();

        var contextSummary = recentLines.Count > 0
            ? string.Join("\n", recentLines)
            : "  - (no recent messages)";

        return $@"
## 🔄 Handoff — {DateTime.Now:yyyy-MM-dd HH:mm}

> **Previous session hit 95% context limit and was auto-relayed by AppBotEye.**
> Read this section, continue the work, then DELETE this section when done.

### Previous Session
- **JSONL**: `{Path.GetFileName(jsonlPath)}`
- **Relay time**: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- **Reason**: context 95% limit (auto-relay by AppBotEye)

### Recent Conversation
{contextSummary}

### TODO for new session
1. Read CLAUDE.md + MEMORY.md to understand project state
2. Run `wkappbot slack send ""New chat continuing! (auto-relay) 🔄""` to notify user
3. Check recent Slack messages for user instructions
4. Continue work from where previous session left off
5. **DELETE this handoff section** after processing

";
    }

    /// <summary>
    /// Check if CLAUDE.md contains a handoff section (auto-relay precondition).
    /// </summary>
    static bool HasHandoffSectionInClaudeMd()
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## \U0001f504 Handoff";
        try
        {
            if (!File.Exists(claudeMdPath)) return false;
            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);
            return content.Contains(handoffMarker, StringComparison.Ordinal);
        }
        catch { return false; }
    }

    /// <summary>
    /// Write handoff section to CLAUDE.md (at the end, before roadmap section).
    /// If a previous handoff section exists, replace it.
    /// CLAUDE.md path: W:/GitHub/WKAppBot/CLAUDE.md
    /// </summary>
    static void WriteHandoffToClaudeMd(string handoffSection)
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## 🔄 Handoff";
        const string handoffEndMarker = "## "; // next section starts

        try
        {
            if (!File.Exists(claudeMdPath))
            {
                Console.WriteLine("[EYE] CLAUDE.md not found! Cannot write handoff.");
                return;
            }

            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);

            // Remove existing handoff section if present
            var handoffIdx = content.IndexOf(handoffMarker, StringComparison.Ordinal);
            if (handoffIdx >= 0)
            {
                // Find end of handoff section (next ## heading or end of file)
                var afterHandoff = content.IndexOf("\n" + handoffEndMarker, handoffIdx + handoffMarker.Length, StringComparison.Ordinal);
                if (afterHandoff >= 0)
                    content = content[..handoffIdx] + content[(afterHandoff + 1)..];
                else
                    content = content[..handoffIdx]; // handoff was at end
            }

            // Append handoff section at the end
            content = content.TrimEnd() + "\n" + handoffSection;

            // Atomic write
            var tmpPath = claudeMdPath + ".tmp";
            File.WriteAllText(tmpPath, content, Encoding.UTF8);
            File.Move(tmpPath, claudeMdPath, overwrite: true);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[EYE] ✅ CLAUDE.md 인수인계 섹션 작성 완료 ({handoffSection.Length} chars)");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] CLAUDE.md 인수인계 작성 실패: {ex.Message}");
        }
    }
}
