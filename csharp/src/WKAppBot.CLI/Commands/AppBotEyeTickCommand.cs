using System.Diagnostics;

namespace WKAppBot.CLI;

internal partial class Program
{
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

            // Phase 8: Route retry flush — Eye-independent safety net (called by Task Scheduler one-shot)
            try
            {
                var retryItems = RouteRetryQueue.GetDueItems();
                if (retryItems.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[EYE_TICK] Route retry flush: {retryItems.Count} item(s)");
                    Console.ResetColor();
                    foreach (var item in retryItems)
                        SlackRouteCommand([item]);
                }
                // Re-schedule next one-shot for any remaining items in queue
                RouteRetryQueue.ScheduleNextTask();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE_TICK] Route retry error: {ex.Message}");
            }

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
}
