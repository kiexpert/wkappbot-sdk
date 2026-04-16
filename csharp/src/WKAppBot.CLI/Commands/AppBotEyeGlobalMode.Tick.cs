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
        // Amber border when admin Eye proxy is reachable — NOT when current process is
        // elevated. User Eye itself is never elevated; what matters is whether a separate
        // admin Eye is serving the wkappbot_elevated pipe so --sudo commands can route
        // to it. IsElevated() here was a long-standing bug that kept the border blue
        // forever regardless of admin session state.
        host.SetElevatedBorder(ElevatedEyeClient.Ping(100));
        PulseStep.Mark("tick-host-info");
        var eyeSummary = BuildEyeSummary(cards, latest, promptPreview, promptDiag.FileWriteUtc);
        Console.WriteLine(string.IsNullOrWhiteSpace(eyeSummary)
            ? "[EYE_RENDER] summary=empty"
            : $"[EYE_RENDER] summary-len={eyeSummary.Length}");

        // CCA live analysis removed from Eye (v4.8) — now in analyze-hack server process.
        // Eye only does read-only operations: card summary, UIA status, context %.
        // No Bitmap/CCA/FlaUI in Eye → memory savings ~500MB+.

        host.UpdateAccessibilityText(eyeSummary);
        PulseStep.Mark("tick-host-text");
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
        Console.Error.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms{memSuffix}{ctxSuffix}");
        Console.Error.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");

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

        var (qPending, qProc) = GetSlackQueueStats();
        if (qPending > 0 || qProc > 0)
            Console.Error.WriteLine($"[EYE_QUEUE] pending={qPending} processing={qProc}{(_slackRetiring ? " retiring" : "")}");

        TryGuardLgOverlay("[EYE_TICK][GUARD]");

        // ── Claude error → Gemini fallback (1s check) ──
        CheckClaudeSessionsForErrors();
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
}
