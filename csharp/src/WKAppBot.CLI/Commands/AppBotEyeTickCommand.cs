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
                // Diagnostic/meta lines go to stderr -- buffered by Launcher and shown only
                // on error exit (or with --stderr flag). Cards are the user-facing result
                // so they stay on stdout. Pipe mode currently drops stderr entirely; the
                // Launcher smart-stderr handler is a separate enhancement to plumb it.
                Console.Error.WriteLine($"[EYE] one-shot tick (IPC fast-path, cache age={ipc.CachedAgeMs}ms)");
                Console.Error.WriteLine($"[EYE_TICK] ipc=ok total={swTotal.ElapsedMilliseconds}ms ctx={ipc.ContextPct}%");
                Console.Error.WriteLine($"[EYE_TICK] hint promptLine={ipc.PromptLineHint} tickLine={ipc.TickLineHint}");
                Console.Error.WriteLine($"[EYE_TICK] cards={ipc.CardCount} promptSource={ipc.PromptSource}");
                if (!string.IsNullOrWhiteSpace(ipc.Prompt))
                    Console.Error.WriteLine($"[EYE_TICK] recent={ipc.Prompt}");
                foreach (var plan in ipc.Plans)
                    Console.Error.WriteLine($"[EYE_PLAN] -> {plan}");
                if (ipc.Plans.Length > 3) Console.Error.WriteLine($"[EYE_PLAN] -> +{ipc.Plans.Length - 3} more...");
                Console.Error.WriteLine($"[EYE_GUARD] armed={(ipc.GuardArmed ? 1 : 0)} execIdle={ipc.ExecIdleSec:F0}s aiIdle={ipc.AiIdleSec:F0}s cooldown={ipc.CooldownSec:F0}s");
                Console.Error.WriteLine($"[EYE_LOOP] keepAwakeAge={(ipc.KeepAwakeAgeSec < 0 ? "n/a" : ipc.KeepAwakeAgeSec.ToString("F0") + "s")} promptSource={ipc.PromptSource} latestTickAge={(ipc.LatestTickAgeSec < 0 ? "n/a" : ipc.LatestTickAgeSec.ToString("F0") + "s")}");
                // "-- card display --" separator: stdout because it precedes the card content.
                Console.WriteLine("-- card display --");
                foreach (var line in ipc.Summary.Split('\n'))
                    Console.WriteLine(line.TrimEnd('\r'));
                Console.Out.Flush();
                // Slack forwarding handled by OnMessage -> slack route worker (no HTTP poll on tick)
                PulseStep.Mark("ipc-done");
                Console.Out.Flush();
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
            Console.Error.WriteLine($"[PROF] ReadEyeCards={msCards}ms (count={cards.Count})");
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
                    {
                        var lf = jsonls[0]!;
                        _lastContextPct = (int)(lf.Length / (1024.0 * 1024.0) / 20.0 * 100);
                    }
                }
            }
            catch { }
            swPhase.Stop();
            var msCtx = swPhase.ElapsedMilliseconds;
            Console.Error.WriteLine($"[PROF] ContextPct={msCtx}ms (ctx={_lastContextPct}%)");

            // -- Phase 5: ExtractRecentPlanItems --
            swPhase.Restart();
            var plans = ExtractRecentPlanItems(maxItems: 3);
            swPhase.Stop();
            var msPlans = swPhase.ElapsedMilliseconds;
            Console.Error.WriteLine($"[PROF] PlanItems={msPlans}ms (count={plans.Count})");

            // -- Phase 6: BuildEyeSummary --
            swPhase.Restart();
            var summary = BuildEyeSummary(cards, latest, prompt, promptDiag.FileWriteUtc);
            swPhase.Stop();
            var msSummary = swPhase.ElapsedMilliseconds;
            Console.Error.WriteLine($"[PROF] BuildSummary={msSummary}ms");

            // -- Print results --
            var ctxInfo = _lastContextPct >= 0 ? $" ctx={_lastContextPct}%" : "";
            Console.Error.WriteLine("[EYE] one-shot tick");
            Console.Error.WriteLine($"[EYE_TICK] tick={msReadTick}ms prompt={msPrompt}ms cards={msCards}ms ctx={msCtx}ms plans={msPlans}ms summary={msSummary}ms total={swTotal.ElapsedMilliseconds}ms{ctxInfo}");
            Console.Error.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");
            Console.Error.WriteLine($"[EYE_TICK] cards={cards.Count} promptSource={promptDiag.Source}");
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.Error.WriteLine($"[EYE_TICK] recent={prompt}");

            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    Console.Error.WriteLine($"[EYE_PLAN] -> {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    Console.Error.WriteLine($"[EYE_PLAN] -> +{_lastPlanItemsCache.Count - plans.Count} more...");
            }

            var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
            var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
            var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
            var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
            Console.Error.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

            var latestAge = -1.0;
            if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
                latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
            var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
            Console.Error.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");

            Console.WriteLine("-- card display --");
            foreach (var line in summary.Split('\n'))
                Console.WriteLine(line.TrimEnd('\r'));
            Console.Out.Flush();

            // Phase 7: Slack forwarding handled by OnMessage -> slack route worker (no HTTP poll)

            // Phase 8: Eye watchdog + route retry flush (called by Task Scheduler every 10 min)
            try
            {
                // Respawn Eye daemon if it was killed -- watchdog core behavior
                RouteRetryQueue.EnsureEyeRunning();

                // Flush due retry items
                var retryItems = RouteRetryQueue.GetDueItems();
                if (retryItems.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Error.WriteLine($"[EYE_TICK] Route retry flush: {retryItems.Count} item(s)");
                    Console.ResetColor();
                    foreach (var item in retryItems)
                        SlackRouteCommand([item]);
                }

                // Sync precise one-shot for remaining items (if any)
                RouteRetryQueue.ScheduleRetryTask();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE_TICK] Watchdog/retry error: {ex.Message}");
            }

            Console.Error.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
            Console.Out.Flush();

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
