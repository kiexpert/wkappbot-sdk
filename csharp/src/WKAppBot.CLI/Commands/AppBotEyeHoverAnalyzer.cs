// AppBotEyeHoverAnalyzer.cs — Mouse CCA Live Analysis (화면 MD 변환기)
// Background worker in Eye: 1s interval → mouse pos → UIA + CCA → Slack thread reply.
// Auto a11y hack on InputReadiness probe success.

using System.Text;
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

    /// <summary>Subscribe to InputReadiness.OnProbeSuccess for auto a11y hack.</summary>
    internal static void SetupAutoHackOnProbe()
    {
        InputReadiness.OnProbeSuccess += (targetHwnd, processName, className) =>
        {
            // Fire-and-forget, single worker (skip if already running)
            if (!_autoHackSemaphore.Wait(0)) return;
            _ = Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine($"[AUTO-HACK] Probe success → routing to analyze-hack server ({processName}/{className} 0x{targetHwnd:X8})");

                    // Route CCA+UIA analysis to analyze-hack server process (avoids loading Vision assembly in Eye)
                    EnsureHackServer();
                    if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null)
                    {
                        Console.Error.WriteLine("[AUTO-HACK] analyze-hack server not available");
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
                        Console.WriteLine($"[AUTO-HACK] Done: {response}");
                    else
                        Console.WriteLine($"[AUTO-HACK] No response from server");
                }
                catch (Exception ex) { Console.Error.WriteLine($"[AUTO-HACK] Error: {ex.Message}"); }
                finally { _autoHackSemaphore.Release(); }
            });
        };
        Console.WriteLine("[AUTO-HACK] Subscribed to InputReadiness.OnProbeSuccess (via analyze-hack server)");
    }

    // ── Analyze-hack server process ──
    static AppBotPipe.SpawnResult? _hackServerProcess;
    static StreamWriter? _hackServerStdin;
    static string _lastHackNodeKey = ""; // change detection: hwnd+elName+elAid

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
            }
        }
        catch (Exception ex) { Console.Error.WriteLine($"[MOUSE-CCA] Server spawn failed: {ex.Message}"); }
    }

    static async Task UnifiedAnalysisLoop(CancellationToken ct)
    {
        Console.WriteLine("[ANALYSIS] Unified mouse+focus loop (analyze-hack server)");
        bool firstRun = true;

        // Wait for Eye startup Slack message
        for (int wait = 0; wait < 30 && _eyeStatusTs == null && !ct.IsCancellationRequested; wait++)
            await Task.Delay(1000, ct);

        // Set up reply #2 and #3: reuse if existing 앱봇아이 reply, else create placeholder
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
                if (r2?["username"]?.GetValue<string>() == "앱봇아이")
                    _mouseCcaReplyTs = r2["ts"]?.GetValue<string>();
                else
                {
                    var (ok1, ts1) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, "🔍 분석 중...",
                        threadTs: _eyeStatusTs, username: "앱봇아이");
                    if (ok1 && ts1 != null) _mouseCcaReplyTs = ts1;
                }

                // Reply #3 (focus chain)
                var r3 = children?.Count > 2 ? children[2] : null;
                if (r3?["username"]?.GetValue<string>() == "앱봇아이")
                    _focusChainReplyTs = r3["ts"]?.GetValue<string>();
                else
                {
                    var (ok2, ts2) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, "⌨️ 분석 중...",
                        threadTs: _eyeStatusTs, username: "앱봇아이");
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
                await Task.Delay(firstRun ? 2000 : 1000, ct);

                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                bool doMouse = false;

                if (hwnd != IntPtr.Zero)
                {
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                    if ((int)pid != Environment.ProcessId)
                    {
                        var nodeKey = $"{hwnd:X8}_{pt.X / 20}_{pt.Y / 20}";
                        if (firstRun || nodeKey != _lastHackNodeKey)
                        {
                            _lastHackNodeKey = nodeKey;
                            doMouse = true;
                        }
                    }
                }
                firstRun = false;

                // Ensure server is running
                EnsureHackServer();
                if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null) continue;

                // ── Mouse CCA (only if node changed) ──
                if (doMouse)
                {

                string serverResult;
                try
                {
                    var request = $"{{\"hwnd\":\"{hwnd:X8}\",\"x\":{pt.X},\"y\":{pt.Y}}}";
                    _hackServerStdin.WriteLine(request);
                    _hackServerStdin.Flush();

                    // Read response (one JSON line)
                    var responseLine = await Task.Run(() => _hackServerProcess.StdOut!.ReadLine(), ct)
                        .WaitAsync(TimeSpan.FromSeconds(10), ct);
                    if (string.IsNullOrEmpty(responseLine)) continue;

                    // Parse server response
                    var resp = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(responseLine);
                    var grapPath = resp?["grapPath"]?.GetValue<string>() ?? "";
                    var elName = resp?["elName"]?.GetValue<string>() ?? "";
                    var elType = resp?["elType"]?.GetValue<string>() ?? "";
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

                    var winTitleShort = winTitle.Length > 40 ? winTitle[..40] + "…" : winTitle;
                    if (elName.Length > 30) elName = elName[..30] + "…";
                    var snippetFileName = $"{winTitleShort}_{elType}_{elName}";
                    if (!string.IsNullOrEmpty(dynId)) snippetFileName += $"_{dynId}";

                    // Build Slack message
                    var header = new StringBuilder();
                    header.AppendLine($"🖱️ Mouse: `{grapPath}`");
                    header.AppendLine($"pos: ({pt.X},{pt.Y}) | win: {winTitleShort} ({captureRect})");
                    header.AppendLine($"UIA: [{elType}] \"{elName}\"");
                    header.AppendLine($"CCA: {totalSeg} seg — T={textCnt} I={iconCnt} S={sepCnt} C={contCnt}{tableInfo}");
                    if (!string.IsNullOrEmpty(fused))
                        header.AppendLine($"match: {fused}");

                    serverResult = header.ToString().TrimEnd();
                    // Add tree as code block
                    if (!string.IsNullOrEmpty(visualMd) && visualMd.Trim().Length > 0)
                        serverResult += $"\n```\n{visualMd}\n```";
                }
                catch (TimeoutException) { continue; }
                catch (Exception ex) { Console.Error.WriteLine($"[MOUSE-CCA] Server error: {ex.Message}"); continue; }

                // Change detection on result
                if (serverResult == _lastMouseCcaResult) continue;
                _lastMouseCcaResult = serverResult;

                // Post mouse to Slack
                await UpdateMouseCcaSlack(serverResult);
                Console.WriteLine($"[ANALYSIS] mouse: {serverResult.Split('\n')[0]}");
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
                            var fPid = fr?["pid"]?.GetValue<int>() ?? 0;
                            if (fPid != Environment.ProcessId)
                            {
                                var fWinShort = fWin.Length > 30 ? fWin[..30] + "…" : fWin;
                                var fsb = new StringBuilder();
                                fsb.AppendLine($"⌨️ Keyboard: `{fGrap}`");
                                fsb.AppendLine($"focus: [{fType}] \"{fName}\"");
                                if (!string.IsNullOrEmpty(fValue))
                                    fsb.AppendLine($"value: \"{(fValue)}\"");
                                fsb.AppendLine($"win: {fWinShort}");
                                var chain = fr?["chain"] as System.Text.Json.Nodes.JsonArray;
                                if (chain?.Count > 0)
                                {
                                    fsb.AppendLine("chain:");
                                    foreach (var p in chain)
                                        fsb.AppendLine($"  └ [{p?["type"]}] {p?["name"]}");
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
                                            threadTs: _eyeStatusTs, username: "앱봇아이");
                                        if (fok && fts != null) _focusChainReplyTs = fts;
                                    }
                                    Console.WriteLine($"[ANALYSIS] focus: {focusResult.Split('\n')[0]}");
                                }
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
    /// Every 1s: UIA focused element → parent chain → root window → Slack thread reply.
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
                    var focusPid = resp?["pid"]?.GetValue<int>() ?? 0;

                    // Skip our own process
                    if (focusPid == Environment.ProcessId) continue;

                    var winShort = winTitle.Length > 30 ? winTitle[..30] + "…" : winTitle;
                    var sb = new StringBuilder();
                    sb.AppendLine($"⌨️ Keyboard: `{grapPath}`");
                    sb.AppendLine($"focus: [{focusType}] \"{focusName}\"");
                    if (!string.IsNullOrEmpty(focusValue))
                        sb.AppendLine($"value: \"{(focusValue)}\"");
                    sb.AppendLine($"win: {winShort}");

                    // Parent chain
                    var chain = resp?["chain"] as System.Text.Json.Nodes.JsonArray;
                    if (chain?.Count > 0)
                    {
                        sb.AppendLine("chain:");
                        foreach (var p in chain)
                            sb.AppendLine($"  └ [{p?["type"]}] {p?["name"]}");
                    }

                    if (!string.IsNullOrEmpty(patterns))
                        sb.AppendLine($"patterns: {patterns}");

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
                                threadTs: _eyeStatusTs, username: "앱봇아이");
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
    static string? _mouseCcaSnippetMsgTs;
    static string? _focusChainSnippetMsgTs;

    /// <summary>Post/edit mouse CCA result (already formatted with code block).</summary>
    static async Task UpdateMouseCcaSlack(string text)
    {
        if (_eyeStatusTs == null || string.IsNullOrEmpty(_eyeBotToken) || string.IsNullOrEmpty(_eyeChannel))
            return;
        // Truncate to Slack limit
        if (text.Length > 3900) text = text[..3900] + "\n…";
        try
        {
            if (_mouseCcaReplyTs != null)
                await SlackUpdateMessageAsync(_eyeBotToken, _eyeChannel, _mouseCcaReplyTs, text);
            else
            {
                var (ok, ts) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, text,
                    threadTs: _eyeStatusTs, username: "앱봇아이");
                if (ok && ts != null) _mouseCcaReplyTs = ts;
            }
        }
        catch { }
    }
}
