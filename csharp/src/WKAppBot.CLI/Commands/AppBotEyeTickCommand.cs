using System.Diagnostics;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int EyeTickCommand(string[] args)
    {
        // -- Hard timeout: --timeout N kills the process if tick takes too long --
        var timeoutSec = 0;
        for (int i = 0; i < args.Length - 1; i++)
            if (args[i] == "--timeout") int.TryParse(args[i + 1], out timeoutSec);
        System.Threading.Timer? killTimer = null;
        if (timeoutSec > 0)
        {
            killTimer = new System.Threading.Timer(_ =>
            {
                Console.Error.WriteLine($"[EYE_TICK] hard timeout {timeoutSec}s exceeded -- exiting");
                Console.Out.Flush();
                Environment.Exit(2);
            }, null, timeoutSec * 1000, System.Threading.Timeout.Infinite);
        }

        // NOTE: Previously auto-launched Eye via LaunchAppBotEyeIfNeeded() here, but that
        // spawns a fresh Eye daemon whose "[EYE] Guardian startup run-key: armed" +
        // "Schedulerless guardian mode active" lines appeared on our stdout/stderr,
        // polluting the tick output and making the command look hung. tick is a
        // READ-ONLY diagnostic. If Eye is dead, just say so -- the next normal command
        // (any a11y/inspect/etc. via Launcher) will auto-spawn Eye through the regular
        // fast-exit path in Program.cs. Tick never performs recovery.

        PulseStep.Init("eye-tick");
        try
        {
            if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
            if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;
            var swTotal = Stopwatch.StartNew();

            PulseStep.Mark("ipc-query");
            // -- Fast path: query running Eye loop via IPC pipe --
            // Eye loop maintains all caches in memory -- IPC response is ~5ms vs ~600ms legacy scan.
            // Fallback to legacy only when Eye is not running or pipe query fails.
            // RunningInEye=true: already inside Eye process -- call BuildIpcTickResponse() directly (no pipe round-trip).
            var ipc = Program.RunningInEye
                ? BuildIpcTickResponse()
                : EyeIpcClient.QueryTickAsync(timeoutMs: 100).GetAwaiter().GetResult();
            if (ipc != null)
            {
                // Diag to stderr (debug only)
                Console.Error.WriteLine($"[EYE_TICK] ipc={swTotal.ElapsedMilliseconds}ms ctx={ipc.ContextPct}% cards={ipc.CardCount} armed={(ipc.GuardArmed?1:0)}");

                // Staleness probe -- warn only when swap is pending
                try
                {
                    var liveExe = Path.Combine(AppContext.BaseDirectory, "wkappbot-core.exe");
                    var newExe  = Path.Combine(AppContext.BaseDirectory, "wkappbot-core.new.exe");
                    if (File.Exists(liveExe) && File.Exists(newExe))
                    {
                        var lagMin = (new FileInfo(newExe).LastWriteTimeUtc - new FileInfo(liveExe).LastWriteTimeUtc).TotalMinutes;
                        if (lagMin >= 1)
                            Console.Error.WriteLine($"[EYE_EXE] STALE: hot-swap pending (lag={lagMin:F0}m)");
                    }
                }
                catch { }

                // Card summary is the main user-facing output
                var ipcSummary = ipc.Summary ?? "";
                if (!string.IsNullOrWhiteSpace(ipcSummary))
                {
                    foreach (var line in ipcSummary.Split('\n'))
                        Console.WriteLine(line.TrimEnd('\r'));
                    Console.Out.Flush();
                    PulseStep.Mark("ipc-done");
                    return 0;
                }

                // IPC alive but summary empty (Eye warming up post-swap) -- fast card scan fallback
                Console.Error.WriteLine("[EYE_TICK] ipc-summary-empty -- fast card scan fallback");
                PulseStep.Mark("ipc-empty-fallback");
                var swFallback = Stopwatch.StartNew();
                var fbCards = ReadEyeCards(staleSeconds: 86400);
                var fbTick = ReadLatestTick(out _, out _);
                var fbSummary = BuildEyeSummary(fbCards, fbTick, "");
                Console.Error.WriteLine($"[EYE_TICK] fallback={swFallback.ElapsedMilliseconds}ms cards={fbCards.Count}");
                if (string.IsNullOrWhiteSpace(fbSummary))
                    Console.WriteLine("(no summary yet -- Eye initializing)");
                else
                    foreach (var line in fbSummary.Split('\n'))
                        Console.WriteLine(line.TrimEnd('\r'));
                Console.Out.Flush();
                PulseStep.Mark("ipc-done");
                return 0;
            }

            // -- Eye not running or refused: fall back to legacy scan --
            // Eye는 간접 런칭만! tick에서 직접 spawn하지 않음.
            // 다음 일반 명령(inspect, a11y 등) 실행 시 자동 spawn됨.
            PulseStep.Mark("ipc-failed-legacy");
            Console.Error.WriteLine("[EYE] IPC query failed -- falling back to legacy scan");

            // -- Phase 1: ReadLatestTick --
            var swPhase = Stopwatch.StartNew();
            long tickRead = 0, tickParse = 0;
            var latest = ReadLatestTick(out tickRead, out tickParse);
            swPhase.Stop();
            var msReadTick = swPhase.ElapsedMilliseconds;
            Console.Error.WriteLine($"[PROF] ReadLatestTick={msReadTick}ms (read={tickRead}ms,parse={tickParse}ms)");

            // -- Phase 2: ReadLatestOpenClawPromptPreview --
            swPhase.Restart();
            var promptDiag = new PromptDiag();
            var prompt = ReadLatestOpenClawPromptPreview(promptDiag);
            swPhase.Stop();
            var msPrompt = swPhase.ElapsedMilliseconds;
            Console.Error.WriteLine($"[PROF] PromptPreview={msPrompt}ms (stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms)");

            // -- Phase 3: ReadEyeCards + prompt-based discovery --
            swPhase.Restart();
            var cards = ReadEyeCards(staleSeconds: 86400); // 24 hours
            SupplementCardsFromPrompts(cards);
            CheckAndReportDeadCards(cards);
            swPhase.Stop();
            var msCards = swPhase.ElapsedMilliseconds;
            _lastPromptSource = promptDiag.Source;

            // -- Phase 4: Context % check --
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
                        _lastContextPct = (int)(jsonls[0]!.Length / (1024.0 * 1024.0) / 20.0 * 100);
                }
            }
            catch { }
            swPhase.Stop();

            // -- Phase 5+6: Plans + Summary --
            var plans = ExtractRecentPlanItems(maxItems: 3);
            var summary = BuildEyeSummary(cards, latest, prompt, promptDiag.FileWriteUtc);

            // Single diag line to stderr (not shown on success in interactive terminal)
            var ctxInfo = _lastContextPct >= 0 ? $" ctx={_lastContextPct}%" : "";
            Console.Error.WriteLine($"[EYE_TICK] legacy={swTotal.ElapsedMilliseconds}ms cards={cards.Count}{ctxInfo}");

            // Card summary -- clean stdout, no separator header
            if (string.IsNullOrWhiteSpace(summary))
                Console.WriteLine("(no summary yet)");
            else
                foreach (var line in summary.Split('\n'))
                    Console.WriteLine(line.TrimEnd('\r'));
            Console.Out.Flush();

            // Watchdog + route retry (background concern, errors to stderr only)
            try
            {
                RouteRetryQueue.EnsureEyeRunning();
                var retryItems = RouteRetryQueue.GetDueItems();
                if (retryItems.Count > 0)
                {
                    Console.Error.WriteLine($"[EYE_TICK] Route retry flush: {retryItems.Count} item(s)");
                    foreach (var item in retryItems)
                        SlackRouteCommand([item]);
                }
                RouteRetryQueue.ScheduleRetryTask();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE_TICK] Watchdog error: {ex.Message}");
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE_TICK] error={ex.Message}");
            Console.Out.Flush();
            return 1;
        }
        finally
        {
            killTimer?.Dispose();

            // tick is READ-ONLY. Recovery (Eye spawn, watchdog re-arm) used to run here,
            // but its output -- including the two "[EYE] Guardian startup run-key: armed" /
            // "Schedulerless guardian mode active" lines -- leaked onto the user's stdout
            // via the Eye pipe's TeeTextWriter, making the command output noisy on every
            // invocation. Recovery is the Watchdog/Task Scheduler's job, not tick's.
            // Keep the pulse marker for telemetry but take no recovery action.
            PulseStep.Mark("eye-check-skipped");
            PulseStep.Done();
        }
    }

    /// <summary>
    /// Build IPC response from cached state -- called by EyeIpcServer on pipe connection.
    /// Trigger EnsureVisible() on the running Eye host -- called by IPC tick handler
    /// so `wkappbot eye tick` also heals opacity/topmost/position.
    /// </summary>
    public static void EnsureEyeVisible() => _globalEyeHost?.EnsureVisible();

    /// <summary>
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
}
