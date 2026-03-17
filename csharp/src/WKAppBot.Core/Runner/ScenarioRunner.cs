using System.Diagnostics;
using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Orchestrates scenario execution: launch app, run steps, teardown.
/// Background watcher runs passively, outputting [WATCH] lines interleaved
/// with [RUN] step execution — like a shell background process with foreground prompt.
/// </summary>
public sealed class ScenarioRunner
{
    private readonly bool _verbose;
    private readonly bool _watch;
    private readonly int _watchIntervalMs;
    private object? _consoleLock;

    /// <summary>
    /// [ZOOM] Optional zoom overlay factory. Set by CLI layer.
    /// Passed through to ActionExecutor.CreateZoom for per-step visual feedback.
    /// </summary>
    public Func<System.Drawing.Rectangle, IntPtr, string, string, IActionZoom?>? ZoomFactory { get; set; }

    /// <summary>
    /// [READINESS] Optional InputReadiness instance. Set by CLI layer.
    /// Passed through to ActionExecutor.Readiness for pre-action blocker detection.
    /// </summary>
    public InputReadiness? ReadinessInstance { get; set; }

    public ScenarioRunner(bool verbose = false, bool watch = true, int watchIntervalMs = 200)
    {
        _verbose = verbose;
        _watch = watch;
        _watchIntervalMs = watchIntervalMs;
    }

    /// <summary>
    /// Run a complete scenario and return results.
    /// </summary>
    public RunResult Run(ScenarioDocument doc)
    {
        var ctx = new RuntimeContext
        {
            StartTime = DateTime.Now,
            // Smart Focus config
            FocusCheck = doc.Config.FocusCheck,
            FocusTimeout = doc.Config.FocusTimeout,
            FocusRetryDelay = doc.Config.FocusRetryDelay,
            FocusAlertDelay = doc.Config.FocusAlertDelay,
            PreferFocusless = doc.Config.PreferFocusless,
            // Vision config
            VisionEnabled = doc.Config.VisionEnabled,
            VisionCacheDir = doc.Config.VisionCacheDir,
            VisionCacheTtlDays = doc.Config.VisionCacheTtlDays,
            VisionConfidenceThreshold = doc.Config.VisionConfidenceThreshold,
            VisionModel = doc.Config.VisionModel,
            OcrPreview = doc.Config.OcrPreview,
        };

        // Copy variables
        foreach (var (k, v) in doc.Variables)
            ctx.Variables[k] = v;

        var result = new RunResult
        {
            ScenarioName = doc.Scenario.Name,
            StartTime = ctx.StartTime
        };

        BackgroundWatcher? watcher = null;
        VisionCache? visionCache = null;
        VisionAnalyzer? visionAnalyzer = null;
        SimpleOcrAnalyzer? simpleOcr = null;
        OcrSegmentCache? segmentCache = null;

        try
        {
            // 1. Launch app
            LogRun($"\n=== {doc.Scenario.Name} ===");
            LogRun($"Launching: {doc.App.Launch}");

            LaunchApp(doc.App, ctx);
            LogRun($"Window found: 0x{ctx.MainWindowHandle:X8} \"{ctx.AppTitle}\"");

            // 2. Initialize Vision components BEFORE watcher (OCR needed by watcher)
            if (ctx.VisionEnabled || ctx.OcrPreview)
            {
                try
                {
                    visionCache = new VisionCache(ctx.VisionCacheDir, ctx.VisionCacheTtlDays);
                    segmentCache = new OcrSegmentCache(ctx.VisionCacheDir);

                    // Simple OCR first (free, fast, offline)
                    try
                    {
                        var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                        var primaryOcrLang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                        simpleOcr = new SimpleOcrAnalyzer(
                            primaryLanguage: primaryOcrLang,
                            confidenceThreshold: ctx.VisionConfidenceThreshold);
                        LogRun($"Simple OCR enabled: lang={primaryOcrLang} (available: {string.Join(", ", ocrLangs)})");
                    }
                    catch (Exception ex)
                    {
                        LogRun($"Simple OCR init failed: {ex.Message} — OCR tier skipped");
                        simpleOcr = null;
                    }

                    // Claude Vision API (expensive fallback — only if vision_enabled AND API key available)
                    if (ctx.VisionEnabled)
                    {
                        try
                        {
                            visionAnalyzer = new VisionAnalyzer(
                                model: ctx.VisionModel,
                                confidenceThreshold: ctx.VisionConfidenceThreshold);
                            LogRun($"Vision API enabled: model={ctx.VisionModel}");
                        }
                        catch (Exception ex)
                        {
                            LogRun($"Vision API not available: {ex.Message} — Claude fallback disabled");
                            visionAnalyzer = null;
                        }
                    }

                    var (totalEntries, _, avgRate) = visionCache.GetStats();
                    LogRun($"Vision cache: {totalEntries} entries (avg success={avgRate:P0})");
                }
                catch (Exception ex)
                {
                    LogRun($"Vision init failed: {ex.Message} — continuing without Vision");
                    visionCache = null;
                    visionAnalyzer = null;
                    simpleOcr = null;
                    ctx.VisionEnabled = false;
                }
            }

            // 3. Start background watcher (passive mode, with OCR if available)
            if (_watch)
            {
                watcher = new BackgroundWatcher(_watchIntervalMs, ctx: ctx, ocr: simpleOcr);
                _consoleLock = watcher.ConsoleLock;
                ctx.ConsoleLock = _consoleLock;  // Share lock with ActionExecutor for [FOCUS] output
                watcher.Start();

                lock (_consoleLock)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[WATCH] ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    var ocrTag = simpleOcr != null ? " + OCR" : "";
                    Console.WriteLine($"Started — tracking test action points (mouse fallback{ocrTag})");
                    Console.ResetColor();
                }
            }

            // 4. Run steps
            using var executor = new ActionExecutor(ctx, _verbose, visionCache, visionAnalyzer, simpleOcr, segmentCache);
            executor.CreateZoom = ZoomFactory; // [ZOOM] Pass through CLI layer factory
            executor.Readiness = ReadinessInstance; // [READINESS] Pre-action blocker/minimize check

            for (int i = 0; i < doc.Steps.Count; i++)
            {
                var step = doc.Steps[i];

                // ── Step header: separator + step name ─────────────
                lock (_consoleLock ?? new object())
                {
                    Console.Write("\r" + new string(' ', SafeWindowWidth() - 1) + "\r");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"─── [{i + 1}/{doc.Steps.Count}] {step.Name} " + new string('─', Math.Max(0, SafeWindowWidth() - 20 - step.Name.Length)));
                    Console.ResetColor();
                }

                // ── Pre-nudge: current state before action ─────────
                watcher?.Nudge();

                // ── Execute the action ─────────────────────────────
                StepResult stepResult = ExecuteWithRetry(executor, step, doc.Config);

                // ── Show what was done ─────────────────────────────
                lock (_consoleLock ?? new object())
                {
                    Console.Write("\r" + new string(' ', SafeWindowWidth() - 1) + "\r");

                    if (!string.IsNullOrEmpty(stepResult.ActionDetail))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[RUN] ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(stepResult.ActionDetail);
                    }
                }

                // ── Post-nudge: state after action ─────────────────
                watcher?.Nudge();

                // ── Result line ────────────────────────────────────
                ctx.StepResults.Add(stepResult);

                // ── ActionState IPC: share step info with AppBotEye ──
                WriteActionState(doc, step, stepResult, ctx, i, doc.Steps.Count);
                var statusStr = stepResult.Status == StepStatus.Pass ? "PASS" : "FAIL";
                var color = stepResult.Status == StepStatus.Pass ? ConsoleColor.Green : ConsoleColor.Red;

                lock (_consoleLock ?? new object())
                {
                    Console.Write("\r" + new string(' ', SafeWindowWidth() - 1) + "\r");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[RUN] ");
                    WriteColored($"→ {statusStr}", color);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" ({stepResult.ElapsedMs:F0}ms)");
                    if (!string.IsNullOrEmpty(stepResult.Message))
                        Console.Write($" {stepResult.Message}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                if (stepResult.Status == StepStatus.Fail && !doc.Config.ContinueOnError)
                {
                    LogRun("  Stopping: continue_on_error=false");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            LogRun($"\nFATAL: {ex.Message}");
            result.Error = ex.Message;
        }
        finally
        {
            // Stop watcher before teardown
            watcher?.Stop(printSummary: true);

            // Dispose vision components
            simpleOcr?.Dispose();
            visionAnalyzer?.Dispose();
            visionCache?.Dispose();

            // 5. Teardown
            if (doc.Teardown != null && doc.Teardown.Count > 0)
            {
                LogRun("\n--- Teardown ---");
                using var teardownExec = new ActionExecutor(ctx, _verbose);
                foreach (var step in doc.Teardown)
                {
                    try
                    {
                        LogRun($"  {step.Name}");
                        teardownExec.Execute(step);
                    }
                    catch (Exception ex)
                    {
                        LogRun($"  Teardown error: {ex.Message}");
                    }
                }
            }

            ctx.EndTime = DateTime.Now;
            watcher?.Dispose();
        }

        // Compile results
        result.EndTime = ctx.EndTime;
        result.Steps = ctx.StepResults;
        result.TotalSteps = doc.Steps.Count;
        result.PassedSteps = ctx.StepResults.Count(r => r.Status == StepStatus.Pass);
        result.FailedSteps = ctx.StepResults.Count(r => r.Status == StepStatus.Fail);
        result.Duration = ctx.EndTime - ctx.StartTime;

        // Summary
        LogRun($"\n=== Results: {result.PassedSteps}/{result.TotalSteps} PASS ===");
        var summaryColor = result.FailedSteps == 0 ? ConsoleColor.Green : ConsoleColor.Red;
        lock (_consoleLock ?? new object())
        {
            WriteColored(
                $"\n{doc.Scenario.Name}: {result.PassedSteps}/{result.TotalSteps} PASS " +
                $"in {result.Duration.TotalSeconds:F1}s\n",
                summaryColor);
        }

        return result;
    }

    private StepResult ExecuteWithRetry(ActionExecutor executor, StepDefinition step, ScenarioConfig config)
    {
        StepResult? lastResult = null;

        for (int attempt = 0; attempt <= config.RetryCount; attempt++)
        {
            if (attempt > 0)
            {
                LogRun($"  Retry {attempt}/{config.RetryCount}...");
                Thread.Sleep((int)(config.RetryDelay * 1000));
            }

            lastResult = executor.Execute(step);
            lastResult.RetryCount = attempt;

            if (lastResult.Status == StepStatus.Pass)
                return lastResult;
        }

        return lastResult!;
    }

    private void LaunchApp(AppConfig app, RuntimeContext ctx)
    {
        // Start the process — split "cmd.exe /c echo ok" into FileName + Arguments
        var launchParts = app.Launch.Split(' ', 2, StringSplitOptions.TrimEntries);
        var psi = new ProcessStartInfo
        {
            FileName = launchParts[0],
            Arguments = launchParts.Length > 1 ? launchParts[1] : "",
            UseShellExecute = true
        };
        var proc = Process.Start(psi)
            ?? throw new InvalidOperationException($"Failed to start: {app.Launch}");

        // Wait for the main window
        var timeout = app.WaitForWindow?.Timeout ?? 10.0;
        var sw = Stopwatch.StartNew();

        while (sw.Elapsed.TotalSeconds < timeout)
        {
            Thread.Sleep(300);
            proc.Refresh();

            // Strategy 1: Check the launched process's own window
            if (proc.MainWindowHandle != IntPtr.Zero)
            {
                if (app.WaitForWindow?.TitleContains != null)
                {
                    var title = WindowFinder.GetWindowText(proc.MainWindowHandle);
                    if (title.Contains(app.WaitForWindow.TitleContains, StringComparison.OrdinalIgnoreCase))
                    {
                        ctx.MainWindowHandle = proc.MainWindowHandle;
                        ctx.AppTitle = title;
                        WindowFinder.BringToFront(ctx.MainWindowHandle);
                        Thread.Sleep(500);
                        return;
                    }
                }
                else
                {
                    ctx.MainWindowHandle = proc.MainWindowHandle;
                    ctx.AppTitle = WindowFinder.GetWindowText(proc.MainWindowHandle);
                    WindowFinder.BringToFront(ctx.MainWindowHandle);
                    Thread.Sleep(500);
                    return;
                }
            }

            // Strategy 2: Search ALL visible windows by title (for UWP / ApplicationFrameHost apps)
            // UWP apps like Calculator run under ApplicationFrameHost, so proc.MainWindowHandle
            // will be IntPtr.Zero. We need to search all top-level windows by title instead.
            if (app.WaitForWindow?.TitleContains != null)
            {
                var windows = WindowFinder.FindByTitle(app.WaitForWindow.TitleContains);
                if (windows.Count > 0)
                {
                    ctx.MainWindowHandle = windows[0].Handle;
                    ctx.AppTitle = windows[0].Title;
                    if (_verbose)
                        LogRun($"  Found via title search: 0x{ctx.MainWindowHandle:X8} \"{ctx.AppTitle}\"");
                    WindowFinder.BringToFront(ctx.MainWindowHandle);
                    Thread.Sleep(500);
                    return;
                }
            }

            // Strategy 3: Search by class name
            if (app.WaitForWindow?.ClassName != null)
            {
                var windows = WindowFinder.FindByClassName(app.WaitForWindow.ClassName);
                if (windows.Count > 0)
                {
                    ctx.MainWindowHandle = windows[0].Handle;
                    ctx.AppTitle = windows[0].Title;
                    WindowFinder.BringToFront(ctx.MainWindowHandle);
                    Thread.Sleep(500);
                    return;
                }
            }
        }

        throw new TimeoutException(
            $"Window not found within {timeout}s for {app.Launch}");
    }

    /// <summary>
    /// Output a [RUN] tagged line, thread-safe with background watcher.
    /// </summary>
    private void LogRun(string msg)
    {
        lock (_consoleLock ?? new object())
        {
            // Clear any [WATCH] \r line before printing
            Console.Write("\r" + new string(' ', SafeWindowWidth() - 1) + "\r");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[RUN]   ");
            Console.ResetColor();
            Console.WriteLine(msg);
        }
    }

    private static void WriteColored(string msg, ConsoleColor color)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(msg);
        Console.ForegroundColor = prev;
    }

    private static int SafeWindowWidth()
    {
        try { return Console.WindowWidth; } catch { return 120; }
    }

    /// <summary>
    /// Write ActionState IPC file for AppBotEye to read.
    /// Best-effort: never blocks the main scenario flow.
    /// </summary>
    private static void WriteActionState(
        ScenarioDocument doc, StepDefinition step, StepResult stepResult,
        RuntimeContext ctx, int stepIndex, int totalSteps)
    {
        try
        {
            var state = new ActionState
            {
                Source = "scenario",
                WindowTitle = ctx.AppTitle,
                ProcessName = null, // TODO: extract from ctx if available

                // UIA element info from the last action point
                ActionName = step.Action,
                ActionDetail = stepResult.ActionDetail,
                StepName = step.Name,
                Status = stepResult.Status switch
                {
                    StepStatus.Pass => "pass",
                    StepStatus.Fail => "fail",
                    StepStatus.Skip => "skip",
                    _ => "error"
                },
                LocatorMethod = stepResult.LocatorMethod,

                // Scenario progress
                ScenarioName = doc.Scenario.Name,
                Progress = $"{stepIndex + 1}/{totalSteps}"
            };

            // Extract UIA info from target definition
            if (step.Target != null)
            {
                if (!string.IsNullOrEmpty(step.Target.AutomationId))
                    state.AutomationId = step.Target.AutomationId;
                if (!string.IsNullOrEmpty(step.Target.Name))
                    state.ElementName = step.Target.Name;
                if (!string.IsNullOrEmpty(step.Target.ControlType))
                    state.ControlType = $"[{step.Target.ControlType}]";
                // Use automation_id as element name fallback
                if (string.IsNullOrEmpty(state.ElementName) && !string.IsNullOrEmpty(step.Target.AutomationId))
                    state.ElementName = step.Target.AutomationId;
            }

            // Enrich from action point if available (has runtime UIA info)
            var ap = ctx.LastActionPoint;
            if (ap != null)
            {
                state.ElementName ??= ap.ElementDescription;
            }

            ActionState.Write(state);
        }
        catch
        {
            // Best-effort: never crash the scenario
        }
    }
}

/// <summary>
/// Final result of a scenario run.
/// </summary>
public sealed class RunResult
{
    public string ScenarioName { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int TotalSteps { get; set; }
    public int PassedSteps { get; set; }
    public int FailedSteps { get; set; }
    public List<StepResult> Steps { get; set; } = new();
    public string? Error { get; set; }

    public bool IsSuccess => FailedSteps == 0 && Error == null;
}
