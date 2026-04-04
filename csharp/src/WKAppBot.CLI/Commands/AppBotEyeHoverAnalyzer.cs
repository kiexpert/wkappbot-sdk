// AppBotEyeHoverAnalyzer.cs -- Mouse CCA Live Analysis
// Background worker in Eye: 1s interval ??mouse pos ??UIA + CCA ??Slack thread reply.
// Auto a11y hack on InputReadiness probe success.

using System.Text;
using System.Text.Json;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Input;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Mouse CCA live analysis (1s interval, change-based Slack thread reply) ──
    static string? _mouseCcaReplyTs;            // Slack thread ts for CCA result
    static string _lastMouseCcaResult = "";      // change detection

    // ── Auto a11y hack on InputReadiness (single worker, no parallel) ──
    static readonly SemaphoreSlim _autoHackSemaphore = new(1, 1);
    static readonly object _liveHackLock = new();
    static AppBotPipe.SpawnResult? _liveHackProcess;
    static int _liveHackRevision;
    static CancellationTokenSource? _liveHackDebounceCts;
    static string _pendingLiveHackGrap = "";
    static string _pendingLiveHackSource = "";
    static string _pendingLiveHackHeadline = "";

    static void AppendEyeAnalysisTrace(string kind, object payload)
    {
        try
        {
            var path = Path.Combine(GetEyeLogDir(), "eye-analysis-trace.jsonl");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            var line = JsonSerializer.Serialize(new
            {
                ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                kind,
                payload
            });
            File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
        }
        catch { }
    }

    /// <summary>Subscribe to InputReadiness.OnProbeSuccess for auto a11y hack.</summary>
    internal static void SetupAutoHackOnProbe()
    {
        AppendEyeAnalysisTrace("auto-hack-subscribed", new { source = "InputReadiness.OnProbeSuccess" });
        InputReadiness.OnProbeSuccess += (targetHwnd, processName, className) =>
        {
            AppendEyeAnalysisTrace("auto-hack-probe", new
            {
                hwnd = $"0x{targetHwnd.ToInt64():X}",
                processName,
                className
            });

            // Fire-and-forget, single worker (skip if already running)
            if (!_autoHackSemaphore.Wait(0))
            {
                AppendEyeAnalysisTrace("auto-hack-skip", new
                {
                    reason = "busy",
                    hwnd = $"0x{targetHwnd.ToInt64():X}",
                    processName,
                    className
                });
                return;
            }
            _ = Task.Run(() =>
            {
                try
                {
                    Console.WriteLine($"[AUTO-HACK] Probe success ??routing to analyze-hack server ({processName}/{className} 0x{targetHwnd:X8})");

                    // Route CCA+UIA analysis to analyze-hack server process (avoids loading Vision assembly in Eye)
                    EnsureHackServer();
                    if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null)
                    {
                        Console.Error.WriteLine("[AUTO-HACK] analyze-hack server not available");
                        AppendEyeAnalysisTrace("auto-hack-error", new
                        {
                            reason = "server-not-available",
                            hwnd = $"0x{targetHwnd.ToInt64():X}",
                            processName,
                            className
                        });
                        return;
                    }

                    var request = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        type = "auto-hack",
                        hwnd = targetHwnd.ToInt64(),
                        process = processName,
                        className = className
                    });

                    string? response;
                    lock (_hackServerStdin)
                    {
                        _hackServerStdin.WriteLine(request);
                        _hackServerStdin.Flush();
                        response = _hackServerProcess!.StdOut!.ReadLine();
                    }

                    if (!string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine($"[AUTO-HACK] Done: {response}");
                        AppendEyeAnalysisTrace("auto-hack-done", new
                        {
                            hwnd = $"0x{targetHwnd.ToInt64():X}",
                            processName,
                            className,
                            response
                        });
                    }
                    else
                    {
                        Console.WriteLine($"[AUTO-HACK] No response from server");
                        AppendEyeAnalysisTrace("auto-hack-empty", new
                        {
                            hwnd = $"0x{targetHwnd.ToInt64():X}",
                            processName,
                            className
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[AUTO-HACK] Error: {ex.Message}");
                    AppendEyeAnalysisTrace("auto-hack-error", new
                    {
                        hwnd = $"0x{targetHwnd.ToInt64():X}",
                        processName,
                        className,
                        error = ex.Message
                    });
                }
                finally { _autoHackSemaphore.Release(); }
            });
        };
        Console.WriteLine("[AUTO-HACK] Subscribed to InputReadiness.OnProbeSuccess (via analyze-hack server)");
    }

    // ── Analyze-hack server process ──
    static AppBotPipe.SpawnResult? _hackServerProcess;
    static StreamWriter? _hackServerStdin;
#pragma warning disable CS0414
    static string _lastHackNodeKey = ""; // change detection: hwnd+elName+elAid
#pragma warning restore CS0414

    /// <summary>
    /// Start unified analysis worker: mouse CCA + keyboard focus in ONE loop.
    /// Shares single analyze-hack server process, alternates requests.
    /// </summary>
    internal static void StartMouseCcaWorker(CancellationToken ct) =>
        Task.Run(() => UnifiedAnalysisLoop(ct), ct);

    static void EnsureHackServer()
    {
        if (_hackServerProcess is { HasExited: false }) return;
        try
        {
            var corePath = Environment.ProcessPath ?? "wkappbot";
            _hackServerProcess = AppBotPipe.Spawn(corePath, "analyze-hack --server",
                EyeCallerCwd.Length > 0 ? EyeCallerCwd : Environment.CurrentDirectory,
                redirectStdIn: true, redirectStdOut: true,
                env: new() { ["WKAPPBOT_WORKER"] = "1" }, caller: "EYE-HACK");
            if (_hackServerProcess != null)
            {
                _hackServerStdin = _hackServerProcess.StdIn;
                Console.WriteLine($"[MOUSE-CCA] analyze-hack server started (PID={_hackServerProcess.Pid})");
                AppendEyeAnalysisTrace("hack-server-started", new { pid = _hackServerProcess.Pid });
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[MOUSE-CCA] Server spawn failed: {ex.Message}");
            AppendEyeAnalysisTrace("hack-server-error", new { error = ex.Message });
        }
    }

    static string EscapeCmdArg(string arg) => arg.Replace("\"", "\\\"");

    static void TryLaunchLiveA11yHack(string grapPath, string reason, string headline)
    {
        if (string.IsNullOrWhiteSpace(grapPath))
            return;

        CancellationTokenSource? oldCts = null;
        lock (_liveHackLock)
        {
            _pendingLiveHackGrap = grapPath;
            _pendingLiveHackSource = reason;
            _pendingLiveHackHeadline = headline;
            _liveHackRevision++;
            oldCts = _liveHackDebounceCts;
            _liveHackDebounceCts = new CancellationTokenSource();
        }

        try { oldCts?.Cancel(); } catch { }
        try { oldCts?.Dispose(); } catch { }

        var debounceCts = _liveHackDebounceCts;
        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(1000, debounceCts.Token);
                string currentGrap;
                string currentSource;
                string currentHeadline;
                lock (_liveHackLock)
                {
                    if (debounceCts != _liveHackDebounceCts)
                    {
                        AppendEyeAnalysisTrace("auto-hack-skip", new
                        {
                            reason = "debounce-cancelled",
                            grapPath,
                            source = reason
                        });
                        return;
                    }
                    currentGrap = _pendingLiveHackGrap;
                    currentSource = _pendingLiveHackSource;
                    currentHeadline = _pendingLiveHackHeadline;
                    if (_liveHackProcess is { HasExited: false })
                    {
                        try { _liveHackProcess.Kill(); } catch { }
                        try { _liveHackProcess.Dispose(); } catch { }
                        _liveHackProcess = null;
                    }
                }

                var corePath = Environment.ProcessPath ?? "wkappbot";
                var cwd = EyeCallerCwd.Length > 0 ? EyeCallerCwd : Environment.CurrentDirectory;
                var spawn = AppBotPipe.Spawn(corePath, $"a11y hack \"{EscapeCmdArg(currentGrap)}\"",
                    cwd, showNoActivate: true, caller: "EYE-A11Y");
                if (spawn == null)
                {
                    AppendEyeAnalysisTrace("auto-hack-error", new
                    {
                        reason = "live-spawn-failed",
                        grapPath = currentGrap,
                        source = currentSource
                    });
                    return;
                }

                lock (_liveHackLock)
                {
                    if (debounceCts != _liveHackDebounceCts)
                    {
                        try { spawn.Kill(); } catch { }
                        try { spawn.Dispose(); } catch { }
                        return;
                    }
                    _liveHackProcess = spawn;
                }

                Console.WriteLine($"[AUTO-HACK] Live overlay spawned (pid={spawn.Pid}) {currentSource}: {currentGrap}");
                AppendEyeAnalysisTrace("auto-hack-live", new
                {
                    pid = spawn.Pid,
                    grapPath = currentGrap,
                    source = currentSource,
                    headline = currentHeadline
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                AppendEyeAnalysisTrace("auto-hack-error", new
                {
                    reason = "live-launch-error",
                    grapPath,
                    source = reason,
                    error = ex.Message
                });
            }
            finally { }
        });
    }

    static async Task UnifiedAnalysisLoop(CancellationToken ct)
    {
        Console.WriteLine("[ANALYSIS] Unified mouse+focus loop (analyze-hack server)");
        AppendEyeAnalysisTrace("analysis-loop-started", new { worker = "mouse+focus" });
        bool firstRun = true;

        // Wait for Eye startup Slack message
        for (int wait = 0; wait < 30 && _eyeStatusTs == null && !ct.IsCancellationRequested; wait++)
            await Task.Delay(1000, ct);

        // Set up reply #2 and #3: reuse if existing CCABot reply, else create placeholder
        if (_eyeStatusTs != null && !string.IsNullOrEmpty(_eyeBotToken) && !string.IsNullOrEmpty(_eyeChannel))
        {
            try
            {
                var replies = await SlackGetThreadRepliesAsync(_eyeBotToken, _eyeChannel, _eyeStatusTs);
                var children = replies?
                    .Where(r => r?["ts"]?.GetValue<string>() != _eyeStatusTs)
                    .ToList();

                // Reply #2 (mouse CCA)
                var r2 = children?.Count > 1 ? children[1] : null;
                if (r2?["username"]?.GetValue<string>() == "CCABot")
                    _mouseCcaReplyTs = r2["ts"]?.GetValue<string>();
                else
                {
                    var (ok1, ts1) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, "Analyzing...",
                        threadTs: _eyeStatusTs, username: "CCABot");
                    if (ok1 && ts1 != null) _mouseCcaReplyTs = ts1;
                }

                // Reply #3 (focus chain)
                var r3 = children?.Count > 2 ? children[2] : null;
                if (r3?["username"]?.GetValue<string>() == "CCABot")
                    _focusChainReplyTs = r3["ts"]?.GetValue<string>();
                else
                {
                    var (ok2, ts2) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, "Analyzing focus chain...",
                        threadTs: _eyeStatusTs, username: "CCABot");
                    if (ok2 && ts2 != null) _focusChainReplyTs = ts2;
                }

                Console.WriteLine($"[ANALYSIS] Replies ready: mouse={_mouseCcaReplyTs != null} focus={_focusChainReplyTs != null}");
            }
            catch { }
        }

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(firstRun ? 2000 : 100, ct);

                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                var nodeKey = hwnd != IntPtr.Zero ? $"{hwnd:X8}:{pt.X},{pt.Y}" : "";
                var idleMs = NativeMethods.GetUserIdleMs();
                bool doMouse = hwnd != IntPtr.Zero && idleMs >= 1000 && !firstRun;
                firstRun = false;

                // Ensure server is running
                EnsureHackServer();
                if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null) continue;

                // ── Mouse CCA (only if node changed) ──
                if (doMouse)
                {
                AppendEyeAnalysisTrace("mouse-analyze", new
                {
                    hwnd = $"0x{hwnd.ToInt64():X}",
                    x = pt.X,
                    y = pt.Y,
                    nodeKey
                });

                string serverResult;
                string grapPath = "";
                try
                {
                    var request = $"{{\"hwnd\":\"{hwnd:X8}\",\"x\":{pt.X},\"y\":{pt.Y}}}";
                    _hackServerStdin.WriteLine(request);
                    _hackServerStdin.Flush();

                    // Read response (one JSON line)
                    var responseLine = await Task.Run(() => _hackServerProcess.StdOut!.ReadLine(), ct)
                        .WaitAsync(TimeSpan.FromSeconds(10), ct);
                    if (string.IsNullOrEmpty(responseLine)) continue;                    // Parse server response
                    var resp = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(responseLine);
                    grapPath = resp?["grapPath"]?.GetValue<string>() ?? "";
                    var elName = resp?["elName"]?.GetValue<string>() ?? "";
                    var elType = resp?["elType"]?.GetValue<string>() ?? "?";
                    var elValue = resp?["elValue"]?.GetValue<string>() ?? "";
                    var elBounds = resp?["elBounds"]?.GetValue<string>() ?? "";
                    var elPatterns = resp?["elPatterns"]?.GetValue<string>() ?? "";
                    var winTitle = resp?["winTitle"]?.GetValue<string>() ?? "";
                    var captureRect = resp?["captureRect"]?.GetValue<string>() ?? "";
                    var visualMd = resp?["visualMd"]?.GetValue<string>() ?? "";
                    var fused = resp?["fused"]?.GetValue<string>() ?? "";
                    var dynId = resp?["dynId"]?.GetValue<string>() ?? "";
                    var ccaNode = resp?["cca"];
                    int totalSeg = ccaNode?["total"]?.GetValue<int>() ?? 0;
                    int textCnt = ccaNode?["text"]?.GetValue<int>() ?? 0;
                    int iconCnt = ccaNode?["icon"]?.GetValue<int>() ?? 0;
                    int sepCnt = ccaNode?["separator"]?.GetValue<int>() ?? 0;
                    int contCnt = ccaNode?["container"]?.GetValue<int>() ?? 0;
                    var tableStr = ccaNode?["table"]?.GetValue<string>() ?? "";
                    var tableInfo = !string.IsNullOrEmpty(tableStr) ? $" tbl={tableStr}" : "";

                    var winTitleShort = winTitle.Length > 40 ? winTitle[..40] + ".." : winTitle;
                    if (elName.Length > 30) elName = elName[..30] + "..";
                    var snippetFileName = $"{winTitleShort}_{elType}_{elName}";
                    if (!string.IsNullOrEmpty(dynId)) snippetFileName += $"_{dynId}";

                    // Build Slack message
                    var header = new StringBuilder();
                    header.AppendLine($"Mouse: {grapPath}");
                    header.AppendLine($"pos: ({pt.X},{pt.Y}) | win: {winTitleShort} ({captureRect})");
                    header.AppendLine($"UIA: [{elType}] \"{elName}\"");
                    if (!string.IsNullOrEmpty(elBounds))
                        header.AppendLine($"bounds: {elBounds}");
                    if (!string.IsNullOrEmpty(elValue))
                        header.AppendLine($"value: \"{elValue}\"");
                    if (!string.IsNullOrEmpty(elPatterns))
                        header.AppendLine($"patterns: {elPatterns}");
                    header.AppendLine($"CCA: {totalSeg} seg | T={textCnt} I={iconCnt} S={sepCnt} C={contCnt}{tableInfo}");
                    if (!string.IsNullOrEmpty(fused))
                        header.AppendLine($"match: {fused}");

                    serverResult = header.ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(visualMd) && visualMd.Trim().Length > 0)
                        serverResult += $"\n`\n{visualMd}\n`";
                }
                catch (TimeoutException) { continue; }
                catch (Exception ex) { Console.Error.WriteLine($"[MOUSE-CCA] Server error: {ex.Message}"); continue; }

                // Change detection on result
                if (serverResult == _lastMouseCcaResult) continue;
                _lastMouseCcaResult = serverResult;

                // Post mouse to Slack
                await UpdateMouseCcaSlack(serverResult);
                NativeMethods.GetCursorPos(out var postPt);
                var postHwnd = NativeMethods.WindowFromPoint(postPt);
                var postNodeKey = postHwnd != IntPtr.Zero ? $"{postHwnd:X8}:{postPt.X},{postPt.Y}" : "";
                if (postNodeKey != nodeKey)
                {
                    AppendEyeAnalysisTrace("mouse-cancel", new
                    {
                        reason = "cursor-moved",
                        before = nodeKey,
                        after = postNodeKey
                    });
                    continue;
                }

                AppendEyeAnalysisTrace("mouse-result", new
                {
                    hwnd = $"0x{hwnd.ToInt64():X}",
                    x = pt.X,
                    y = pt.Y,
                    headline = serverResult.Split('\n')[0]
                });
                Console.WriteLine($"[ANALYSIS] mouse: {serverResult.Split('\n')[0]}");
                TryLaunchLiveA11yHack(grapPath, "mouse", serverResult.Split('\n')[0]);
                } // end if (doMouse)

                // ── Keyboard focus (always, regardless of mouse change) ──
                try
                {
                    _hackServerStdin!.WriteLine("{\"type\":\"focus\"}");
                    _hackServerStdin.Flush();
                    var focusLine = await Task.Run(() => _hackServerProcess!.StdOut!.ReadLine(), ct)
                        .WaitAsync(TimeSpan.FromSeconds(5), ct);
                    if (!string.IsNullOrEmpty(focusLine))
                    {
                        var fr = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(focusLine);
                        if (fr?["error"] == null)
                        {
                            var fName = fr?["name"]?.GetValue<string>() ?? "";
                            var fType = fr?["type"]?.GetValue<string>() ?? "?";
                            var fValue = fr?["value"]?.GetValue<string>() ?? "";
                            var fGrap = fr?["grapPath"]?.GetValue<string>() ?? "";
                            var fWin = fr?["winTitle"]?.GetValue<string>() ?? "";
                            var fPatterns = fr?["patterns"]?.GetValue<string>() ?? "";
                                var fWinShort = fWin.Length > 30 ? fWin[..30] + ".." : fWin;
                                var fsb = new StringBuilder();
                                fsb.AppendLine($"Keyboard: `{fGrap}`");
                                fsb.AppendLine($"focus: [{fType}] \"{fName}\"");
                                if (!string.IsNullOrEmpty(fValue))
                                    fsb.AppendLine($"value: \"{(fValue)}\"");
                                fsb.AppendLine($"win: {fWinShort}");
                                var chain = fr?["chain"] as System.Text.Json.Nodes.JsonArray;
                                if (chain?.Count > 0)
                                {
                                    fsb.AppendLine("chain:");
                                    foreach (var p in chain)
                                        fsb.AppendLine($"  ??[{p?["type"]}] {p?["name"]}");
                                }
                                if (!string.IsNullOrEmpty(fPatterns))
                                    fsb.AppendLine($"patterns: {fPatterns}");
                                var focusResult = fsb.ToString().TrimEnd();
                                if (focusResult != _lastFocusChainResult)
                                {
                                    _lastFocusChainResult = focusResult;
                                    if (_focusChainReplyTs != null)
                                        await SlackUpdateMessageAsync(_eyeBotToken!, _eyeChannel!, _focusChainReplyTs, focusResult);
                                    else if (_eyeStatusTs != null)
                                    {
                                        var (fok, fts) = await SlackSendViaApi(_eyeBotToken!, _eyeChannel!, focusResult,
                                            threadTs: _eyeStatusTs, username: "CCABot");
                                        if (fok && fts != null) _focusChainReplyTs = fts;
                                    }
                                    AppendEyeAnalysisTrace("focus-result", new
                                    {
                                        headline = focusResult.Split('\n')[0],
                                        replyTs = _focusChainReplyTs
                                    });
                                    Console.WriteLine($"[ANALYSIS] focus: {focusResult.Split('\n')[0]}");
                                    TryLaunchLiveA11yHack(fGrap, "focus", focusResult.Split('\n')[0]);
                                }
                        }
                    }
                }
                catch { }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[ANALYSIS] Error: {ex.Message}"); }
        }

        // Cleanup server process
        try { _hackServerProcess?.Kill(); } catch { }
        Console.WriteLine("[MOUSE-CCA] Worker stopped");
    }

    // ── Keyboard Focus Chain analysis (separate Slack thread reply) ──
    static string? _focusChainReplyTs;
    static string _lastFocusChainResult = "";

    /// <summary>
    /// Restore reply ts values from a recovered Eye status thread (Eye restart reuse).
    /// Called from AppBotEyeGlobalMode when _eyeStatusTs is reused from previous session.
    /// </summary>
    internal static void RestoreHoverReplyTs(string? mouseTs, string? focusTs)
    {
        if (mouseTs != null) { _mouseCcaReplyTs = mouseTs; Console.WriteLine($"[EYE] Restored mouse CCA reply ts={mouseTs}"); }
        if (focusTs != null) { _focusChainReplyTs = focusTs; Console.WriteLine($"[EYE] Restored focus chain reply ts={focusTs}"); }
    }

    /// <summary>
    /// Start keyboard focus chain analysis worker.
    /// Every 1s: UIA focused element ??parent chain ??root window ??Slack thread reply.
    /// </summary>
    internal static void StartFocusChainWorker(CancellationToken ct) =>
        Task.Run(() => FocusChainLoop(ct), ct);

    static async Task FocusChainLoop(CancellationToken ct)
    {
        Console.WriteLine("[FOCUS-CHAIN] Keyboard focus (via analyze-hack server)");
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, ct);

                // Send focus request to shared analyze-hack server
                EnsureHackServer();
                if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null) continue;

                string chainResult;
                try
                {
                    _hackServerStdin.WriteLine("{\"type\":\"focus\"}");
                    _hackServerStdin.Flush();

                    var responseLine = await Task.Run(() => _hackServerProcess.StdOut!.ReadLine(), ct)
                        .WaitAsync(TimeSpan.FromSeconds(5), ct);
                    if (string.IsNullOrEmpty(responseLine)) continue;

                    var resp = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(responseLine);
                    if (resp?["error"] != null) continue;

                                        var focusName = resp?["name"]?.GetValue<string>() ?? "";
                    var focusType = resp?["type"]?.GetValue<string>() ?? "?";
                    var focusValue = resp?["value"]?.GetValue<string>() ?? "";
                    var grapPath = resp?["grapPath"]?.GetValue<string>() ?? "";
                    var winTitle = resp?["winTitle"]?.GetValue<string>() ?? "";
                    var patterns = resp?["patterns"]?.GetValue<string>() ?? "";
                    var inspectText = resp?["inspectText"]?.GetValue<string>() ?? "";
                    var bounds = resp?["bounds"]?.GetValue<string>() ?? "";
                    var selectionRects = resp?["selectionRects"] as System.Text.Json.Nodes.JsonArray;

                    var winShort = winTitle.Length > 30 ? winTitle[..30] + ".." : winTitle;
                    var sb = new StringBuilder();
                    sb.AppendLine($"win: {winShort}");
                    sb.AppendLine($"Keyboard: {grapPath}");
                    if (!string.IsNullOrEmpty(bounds))
                        sb.AppendLine($"bounds: {bounds}");
                    if (selectionRects?.Count > 0)
                        sb.AppendLine($"selection: {string.Join(" | ", selectionRects.Select(x => x?.ToString()).Where(x => !string.IsNullOrEmpty(x))!)}");

                    if (!string.IsNullOrWhiteSpace(inspectText))
                    {
                        sb.AppendLine("`");
                        sb.AppendLine(inspectText.TrimEnd());
                        sb.AppendLine("`");
                    }
                    else
                    {
                        sb.AppendLine($"focus: [{focusType}] \"{focusName}\"");
                        if (!string.IsNullOrEmpty(focusValue))
                            sb.AppendLine($"value: \"{focusValue}\"");

                        var chain = resp?["chain"] as System.Text.Json.Nodes.JsonArray;
                        if (chain?.Count > 0)
                        {
                            sb.AppendLine("chain:");
                            foreach (var p in chain)
                                sb.AppendLine($"  - [{p?["type"]}] {p?["name"]}");
                        }

                        if (!string.IsNullOrEmpty(patterns))
                            sb.AppendLine($"patterns: {patterns}");
                    }

                    chainResult = sb.ToString().TrimEnd();
                }
                catch (TimeoutException) { continue; }
                catch { continue; }

                // Change detection
                if (chainResult == _lastFocusChainResult) continue;
                _lastFocusChainResult = chainResult;

                // Post/edit as simple message (no file upload)
                if (_eyeStatusTs != null && !string.IsNullOrEmpty(_eyeBotToken) && !string.IsNullOrEmpty(_eyeChannel))
                {
                    try
                    {
                        if (_focusChainReplyTs != null)
                            await SlackUpdateMessageAsync(_eyeBotToken, _eyeChannel, _focusChainReplyTs, chainResult);
                        else
                        {
                            var (ok2, ts2) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, chainResult,
                                threadTs: _eyeStatusTs, username: "CCABot");
                            if (ok2 && ts2 != null) _focusChainReplyTs = ts2;
                        }
                    }
                    catch { }
                }

                Console.WriteLine($"[FOCUS-CHAIN] {chainResult.Split('\n')[0]}");
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[FOCUS-CHAIN] Error: {ex.Message}"); }
        }
        Console.WriteLine("[FOCUS-CHAIN] Worker stopped");
    }

    // Track uploaded snippet message ts for delete→reupload cycle
#pragma warning disable CS0169
    static string? _mouseCcaSnippetMsgTs;
    static string? _focusChainSnippetMsgTs;
#pragma warning restore CS0169

    /// <summary>Post/edit mouse CCA result (already formatted with code block).</summary>
    static async Task UpdateMouseCcaSlack(string text)
    {
        if (_eyeStatusTs == null || string.IsNullOrEmpty(_eyeBotToken) || string.IsNullOrEmpty(_eyeChannel))
            return;
        // Truncate to Slack limit
        if (text.Length > 3900) text = text[..3900] + "\n...";
        try
        {
            if (_mouseCcaReplyTs != null)
                await SlackUpdateMessageAsync(_eyeBotToken, _eyeChannel, _mouseCcaReplyTs, text);
            else
            {
                var (ok, ts) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, text,
                    threadTs: _eyeStatusTs, username: "CCABot");
                if (ok && ts != null) _mouseCcaReplyTs = ts;
            }
        }
        catch { }
    }
}

