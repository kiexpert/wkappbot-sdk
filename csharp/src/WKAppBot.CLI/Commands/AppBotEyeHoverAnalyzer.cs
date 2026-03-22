// AppBotEyeHoverAnalyzer.cs — Mouse CCA Live Analysis (화면 MD 변환기)
// Background worker in Eye: 1s interval → mouse pos → UIA + CCA → Slack thread reply.
// Auto a11y hack on InputReadiness probe success.

using System.Text;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Accessibility;

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
                    Console.WriteLine($"[AUTO-HACK] Probe success → analyzing {processName}/{className} (0x{targetHwnd:X8})");

                    // Capture target parent region
                    NativeMethods.GetWindowRect(targetHwnd, out var wr);
                    int x = wr.Left, y = wr.Top, w = wr.Right - wr.Left, h = wr.Bottom - wr.Top;
                    if (w < 10 || h < 10) return;
                    if (w > 1200) w = 1200;
                    if (h > 800) h = 800;

                    using var bmp = new System.Drawing.Bitmap(w, h);
                    using (var g = System.Drawing.Graphics.FromImage(bmp))
                        g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(w, h));

                    // CCA analysis
                    var cca = new WKAppBot.Vision.ConnectedComponentAnalyzer();
                    var regions = cca.Analyze(bmp);

                    // Collect UIA leaves for fused matching
                    var uiaInfos = new List<WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo>();
                    try
                    {
                        using var uia = new UiaLocator();
                        var leaves = new List<(string text, int lx, int ly, int lw, int lh, int depth)>();
                        var rootEl = uia.GetElementAndInfoAtPoint(x + w / 2, y + h / 2).element;
                        if (rootEl?.Parent != null)
                            UiaLocator.CollectTextLeaves(rootEl.Parent, leaves, 0, 8);
                        foreach (var leaf in leaves)
                            uiaInfos.Add(new WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo
                            {
                                Name = leaf.text,
                                Bounds = new System.Drawing.Rectangle(leaf.lx - x, leaf.ly - y, leaf.lw, leaf.lh)
                            });
                    }
                    catch { }

                    // Fused match + save
                    if (uiaInfos.Count > 0 || regions.Count > 0)
                    {
                        var matchResults = WKAppBot.Vision.CcaUiaFusedMatcher.Match(regions, uiaInfos);
                        var summary = WKAppBot.Vision.CcaUiaFusedMatcher.Summarize(matchResults);
                        WKAppBot.Vision.CcaUiaFusedMatcher.SaveToExperienceDb(
                            Path.Combine(DataDir, "experience"), processName, className, matchResults);
                        Console.WriteLine($"[AUTO-HACK] Done: {summary}");
                    }
                }
                catch (Exception ex) { Console.Error.WriteLine($"[AUTO-HACK] Error: {ex.Message}"); }
                finally { _autoHackSemaphore.Release(); }
            });
        };
        Console.WriteLine("[AUTO-HACK] Subscribed to InputReadiness.OnProbeSuccess");
    }

    // ── Analyze-hack server process ──
    static System.Diagnostics.Process? _hackServerProcess;
    static StreamWriter? _hackServerStdin;
    static string _lastHackNodeKey = ""; // change detection: hwnd+elName+elAid

    /// <summary>
    /// Start mouse CCA worker using analyze-hack server process.
    /// Event-driven: only analyze when mouse UIA node changes.
    /// </summary>
    internal static void StartMouseCcaWorker(CancellationToken ct) =>
        Task.Run(() => MouseCcaServerLoop(ct), ct);

    static void EnsureHackServer()
    {
        if (_hackServerProcess is { HasExited: false }) return;
        try
        {
            var corePath = Environment.ProcessPath ?? "wkappbot";
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = corePath,
                Arguments = "analyze-hack --server",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
            };
            _hackServerProcess = System.Diagnostics.Process.Start(psi);
            if (_hackServerProcess != null)
            {
                _hackServerStdin = _hackServerProcess.StandardInput;
                Console.WriteLine($"[MOUSE-CCA] analyze-hack server started (PID={_hackServerProcess.Id})");
            }
        }
        catch (Exception ex) { Console.Error.WriteLine($"[MOUSE-CCA] Server spawn failed: {ex.Message}"); }
    }

    static async Task MouseCcaServerLoop(CancellationToken ct)
    {
        Console.WriteLine("[MOUSE-CCA] Event-driven analysis (analyze-hack server)");
        bool firstRun = true;

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(firstRun ? 3000 : 1000, ct); // first run: 3s delay for Eye init

                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;

                // Skip our own process
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                if ((int)pid == Environment.ProcessId) continue;

                // Change detection: only analyze when UIA node changes
                var nodeKey = $"{hwnd:X8}_{pt.X / 20}_{pt.Y / 20}"; // 20px grid
                if (!firstRun && nodeKey == _lastHackNodeKey) continue;
                _lastHackNodeKey = nodeKey;
                firstRun = false;

                // Send request to analyze-hack server process
                EnsureHackServer();
                if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null) continue;

                string serverResult;
                try
                {
                    var request = $"{{\"hwnd\":\"{hwnd:X8}\",\"x\":{pt.X},\"y\":{pt.Y}}}";
                    _hackServerStdin.WriteLine(request);
                    _hackServerStdin.Flush();

                    // Read response (one JSON line)
                    var responseLine = await Task.Run(() => _hackServerProcess.StandardOutput.ReadLine(), ct)
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
                    header.AppendLine($"🔍 **grap**: `{grapPath}`");
                    header.AppendLine($"**pos**: ({pt.X},{pt.Y}) | **win**: _{winTitleShort}_ ({captureRect})");
                    header.AppendLine($"**UIA**: `[{elType}]` \"{elName}\"");
                    header.AppendLine($"**CCA**: {totalSeg} seg — T={textCnt} I={iconCnt} S={sepCnt} C={contCnt}{tableInfo}");
                    if (!string.IsNullOrEmpty(fused))
                        header.AppendLine($"**match**: {fused}");

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

                // Post to Slack
                await UpdateMouseCcaSlack(serverResult);
                Console.WriteLine($"[MOUSE-CCA] {serverResult.Split('\n')[0]}");
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[MOUSE-CCA] Error: {ex.Message}"); }
        }

        // Cleanup server process
        try { _hackServerProcess?.Kill(); } catch { }
        Console.WriteLine("[MOUSE-CCA] Worker stopped");
    }

    // ── Keyboard Focus Chain analysis (separate Slack thread reply) ──
    static string? _focusChainReplyTs;
    static string _lastFocusChainResult = "";

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

                    var responseLine = await Task.Run(() => _hackServerProcess.StandardOutput.ReadLine(), ct)
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
                    sb.AppendLine($"⌨️ **grap**: `{grapPath}`");
                    sb.AppendLine($"**focus**: `[{focusType}]` \"{focusName}\"");
                    if (!string.IsNullOrEmpty(focusValue))
                        sb.AppendLine($"**value**: \"{(focusValue.Length > 50 ? focusValue[..50] + "…" : focusValue)}\"");
                    sb.AppendLine($"**win**: _{winShort}_");

                    // Parent chain
                    var chain = resp?["chain"] as System.Text.Json.Nodes.JsonArray;
                    if (chain?.Count > 0)
                    {
                        sb.AppendLine("**chain**:");
                        foreach (var p in chain)
                            sb.AppendLine($"  └ `[{p?["type"]}]` {p?["name"]}");
                    }

                    if (!string.IsNullOrEmpty(patterns))
                        sb.AppendLine($"**patterns**: {patterns}");

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
