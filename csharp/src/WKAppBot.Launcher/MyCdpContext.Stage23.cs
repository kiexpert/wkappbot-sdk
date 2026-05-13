// Stage 2 / Stage 3 placement corrections for post-launch Chrome windows.
//
// Architecture
// ------------
// TryMoveWebBotNearCaller (in MyCdpContext.cs) performs Stage 1 (immediate
// placement) synchronously. After Stage 1 returns, it invokes
// SpawnBackgroundPlacementWatcher() to fork a detached helper process that
// runs Stage 2 and Stage 3 asynchronously, so the user's CLI prompt returns
// immediately while the helper monitors Chrome over the next ~10s.
//
// Stage 2 (PAGE LOAD)
//   - Open CDP WebSocket against http://127.0.0.1:<port>/json/list
//   - Listen for Page.loadEventFired, max 10s timeout
//   - On fire/timeout, call TryValidateAndCorrectPlacement again to catch
//     drift caused by page rendering, content reflow, WM_DPICHANGED, etc.
//
// Stage 3 (DPI AWARE MATCH)
//   - Call GetDpiForWindow(chromeHwnd) for the live DPI of Chrome's monitor
//   - Compare against the caller's DPI captured at stage 1 entry
//   - If DPI mismatch detected, recompute target rect at Chrome's current
//     DPI and issue a corrective SetWindowPos
//
// Telemetry: every stage outcome is appended to wkappbot.hq/runtime/cdp-state.jsonl
// with "stage": 2 | 3 records so cdp-mon can audit timing and accuracy.

using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Hidden subcommand marker. When wkappbot.exe is invoked with this as
    /// argv[0], Main() routes to <see cref="RunBackgroundPlacementWatcher"/>
    /// instead of normal command dispatch. The argv layout is:
    ///   __bg-place-chrome <hwndHex> <cdpPort> <L> <T> <R> <B> <cmd>
    /// Underscored to make it obvious to log readers that this is internal.
    /// </summary>
    internal const string BgPlaceChromeArg = "__bg-place-chrome";

    /// <summary>
    /// Fork a detached wkappbot.exe child that runs Stage 2 (page-load wait)
    /// and Stage 3 (DPI-aware match) against the just-placed Chrome window.
    /// Returns immediately so the parent Launcher can exit and unblock the
    /// caller's terminal prompt.
    ///
    /// Best-effort: any failure (no exe path, CreateProcess error, etc.) is
    /// swallowed and Stage 1's placement remains the final result.
    /// </summary>
    internal static void SpawnBackgroundPlacementWatcher(
        IntPtr chromeHwnd,
        RECT stage1TargetRect,
        string cmd)
    {
        try
        {
            // Resolve CDP port for this project (same hash as cdp open assigns).
            var port = GetExpectedCdpPort();
            if (!port.HasValue)
            {
                Console.Error.WriteLine("[PLACEMENT:STAGE23] no CDP port resolved -- skip background watcher");
                return;
            }

            var exe = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exe) || !File.Exists(exe))
            {
                Console.Error.WriteLine("[PLACEMENT:STAGE23] no exe path -- skip background watcher");
                return;
            }

            // Build argv for the detached child. We invoke ourselves so the
            // helper has the same DPI awareness, the same telemetry path,
            // and the same shared helpers without needing a separate binary.
            var args = new[]
            {
                BgPlaceChromeArg,
                $"0x{chromeHwnd.ToInt64():X}",
                port.Value.ToString(),
                stage1TargetRect.Left.ToString(),
                stage1TargetRect.Top.ToString(),
                stage1TargetRect.Right.ToString(),
                stage1TargetRect.Bottom.ToString(),
                cmd ?? "",
            };

            // Use CreateProcessW with DETACHED_PROCESS so the child has no
            // console (no bash ConPTY entanglement) and CREATE_BREAKAWAY_FROM_JOB
            // so bash doesn't wait for it. envBlock=IntPtr.Zero -> inherit
            // current environment (we just need WKAPPBOT_* and PATH to work).
            var cmdLine = new StringBuilder($"\"{exe.Replace("\"", "\\\"")}\"");
            foreach (var a in args)
                cmdLine.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
            var cmdArr = (cmdLine.ToString() + "\0").ToCharArray();
            var si = new AppBotPipe.STARTUPINFOW { cb = System.Runtime.InteropServices.Marshal.SizeOf<AppBotPipe.STARTUPINFOW>() };

            bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, false,
                DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB | CREATE_UNICODE_ENVIRONMENT,
                IntPtr.Zero, Environment.CurrentDirectory, ref si, out var pi);

            if (!ok)
            {
                var err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                Console.Error.WriteLine($"[PLACEMENT:STAGE23] CreateProcessW failed gle={err} -- skip background watcher");
                return;
            }
            CloseHandle(pi.hThread);
            CloseHandle(pi.hProcess);

            Console.Error.WriteLine($"[PLACEMENT:STAGE23] spawned background watcher pid={pi.dwProcessId} hwnd=0x{chromeHwnd.ToInt64():X} port={port.Value}");
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[PLACEMENT:STAGE23] spawn failed: {ex.GetType().Name}: {ex.Message}"); } catch { }
        }
    }

    /// <summary>
    /// Entry point for the detached child process started by
    /// <see cref="SpawnBackgroundPlacementWatcher"/>. Runs Stage 2 and Stage 3,
    /// then exits. Never crashes the parent — all exceptions are swallowed.
    ///
    /// argv layout (passed via Main()):
    ///   [0] __bg-place-chrome  (already stripped by caller)
    ///   [1] 0xHEXHWND
    ///   [2] cdpPort
    ///   [3..6] L T R B  (stage 1 target rect)
    ///   [7] originating cmd ("cdp"|"ask"|...) for logging
    /// </summary>
    internal static int RunBackgroundPlacementWatcher(string[] args)
    {
        try
        {
            if (args.Length < 7)
            {
                Console.Error.WriteLine($"[PLACEMENT:WATCHER] argc={args.Length}, expected 7+ -- abort");
                return 1;
            }

            // args[0] is __bg-place-chrome, already consumed by Main routing.
            // The Launcher passes the full argv array including the marker,
            // so the offset starts at 1 here.
            var hwndStr  = args[1];
            var portStr  = args[2];
            var lStr     = args[3];
            var tStr     = args[4];
            var rStr     = args[5];
            var bStr     = args[6];
            var fromCmd  = args.Length > 7 ? args[7] : "";

            if (!TryParseHexOrDec(hwndStr, out long hwndVal)
                || !int.TryParse(portStr, out int cdpPort)
                || !int.TryParse(lStr, out int tL)
                || !int.TryParse(tStr, out int tT)
                || !int.TryParse(rStr, out int tR)
                || !int.TryParse(bStr, out int tB))
            {
                Console.Error.WriteLine("[PLACEMENT:WATCHER] argv parse failed -- abort");
                return 1;
            }

            var chromeHwnd = new IntPtr(hwndVal);
            var stage1Target = new RECT { Left = tL, Top = tT, Right = tR, Bottom = tB };

            Console.Error.WriteLine($"[PLACEMENT:WATCHER] pid={Environment.ProcessId} hwnd=0x{hwndVal:X} port={cdpPort} cmd={fromCmd}");

            // Capture the caller-DPI (from the parent Launcher's already-set
            // PerMonitorV2 awareness + the Chrome window's current DPI). This
            // is used as the Stage 3 baseline.
            uint callerDpi = TryGetWindowDpiSafe(chromeHwnd);

            // Stage 2: wait for the page to fire its load event (or 10s timeout),
            // then re-validate Chrome's rect against stage1Target.
            TryStage2PageLoadWait(chromeHwnd, cdpPort, stage1Target, fromCmd).GetAwaiter().GetResult();

            // Stage 3: query final DPI context, recompute target if drift
            // detected, issue corrective SetWindowPos.
            TryStage3DpiAwareMatch(chromeHwnd, stage1Target, callerDpi, fromCmd);

            return 0;
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[PLACEMENT:WATCHER] fatal: {ex.GetType().Name}: {ex.Message}"); } catch { }
            return 1;
        }
    }

    static bool TryParseHexOrDec(string s, out long value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(s)) return false;
        var t = s.Trim();
        if (t.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            return long.TryParse(t.AsSpan(2), System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.InvariantCulture, out value);
        return long.TryParse(t, out value);
    }

    /// <summary>
    /// Stage 2: subscribe to the active tab's Page.loadEventFired over CDP and
    /// re-validate placement once the page finishes loading (or after 10s).
    ///
    /// Why: between Stage 1's SetWindowPos and the page actually rendering,
    /// content can drive WM_DPICHANGED (monitor change via page-driven move),
    /// or WPF/Electron-style host shells can resize the window to match
    /// content. Catching the post-load rect ensures the user's final view is
    /// where we put it.
    /// </summary>
    internal static async Task TryStage2PageLoadWait(IntPtr chromeHwnd, int cdpPort, RECT stage1Target, string cmd)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string trigger = "timeout";
        bool placementOk = false;
        int attempts = 0;
        RECT final = default;

        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            // Step A: resolve the active page's webSocketDebuggerUrl via the
            // standard CDP discovery endpoint. Chrome listens on 127.0.0.1
            // and serves JSON describing each open tab.
            string? wsUrl = await ResolveActivePageWsUrl(cdpPort, cts.Token).ConfigureAwait(false);
            if (string.IsNullOrEmpty(wsUrl))
            {
                trigger = "no_ws_url";
                Console.Error.WriteLine($"[PLACEMENT:STAGE2] could not resolve CDP ws url for port={cdpPort}");
            }
            else
            {
                // Step B: open WebSocket, enable Page domain, listen for
                // Page.loadEventFired. If page already loaded, Chrome will
                // not re-fire — fall back to the 10s timer and re-validate
                // anyway (still useful for catching post-Stage-1 drift).
                trigger = await WaitForPageLoadEventFired(wsUrl!, cts.Token).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            trigger = "timeout";
        }
        catch (Exception ex)
        {
            trigger = "error:" + ex.GetType().Name;
            Console.Error.WriteLine($"[PLACEMENT:STAGE2] error during ws listen: {ex.Message}");
        }

        sw.Stop();

        // Step C: re-validate placement now that the page is fully loaded
        // (or we gave up waiting). Same triple-validate path Stage 1 used.
        try
        {
            var result = TryValidateAndCorrectPlacement(chromeHwnd, stage1Target, maxAttempts: 3);
            placementOk = result.success;
            final = result.finalRect;
            attempts = result.attemptCount;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PLACEMENT:STAGE2] revalidate threw: {ex.Message}");
        }

        AppendStage2Record(trigger, sw.ElapsedMilliseconds, stage1Target, final, placementOk, attempts, cmd);
        Console.Error.WriteLine(
            $"[PLACEMENT:STAGE2] trigger={trigger} elapsed={sw.ElapsedMilliseconds}ms ok={placementOk} attempts={attempts} "
            + $"final=(L={final.Left},T={final.Top},R={final.Right},B={final.Bottom})");
    }

    /// <summary>
    /// Stage 3: re-check Chrome's DPI context post-load and correct any rect
    /// mismatch caused by an intervening WM_DPICHANGED. If the caller-DPI
    /// captured at watcher entry differs from Chrome's current DPI by more
    /// than the threshold (e.g. caller=96, Chrome=144), the target rect is
    /// recomputed in the new DPI context and SetWindowPos re-issued.
    ///
    /// Logical-to-physical / physical-to-logical: since the launcher process
    /// is PerMonitorV2-aware, both rects are already in physical pixels for
    /// their respective monitors. The "rescale" therefore means scaling the
    /// stage1 width/height by (currentDpi / callerDpi) so the visible size
    /// stays consistent across the migration.
    /// </summary>
    internal static void TryStage3DpiAwareMatch(IntPtr chromeHwnd, RECT stage1Target, uint callerDpi, string cmd)
    {
        const int DeltaThreshold = 8;             // px — minimum drift to bother correcting
        const uint MinDpi = 72, MaxDpi = 480;     // sanity bounds, anything outside means a bad read
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;

        try
        {
            if (!IsWindow(chromeHwnd))
            {
                AppendStage3Record(0, 0, false, false, false, stage1Target, default, "hwnd_invalid", cmd);
                Console.Error.WriteLine("[PLACEMENT:STAGE3] hwnd no longer valid -- skip");
                return;
            }

            uint currentDpi = TryGetWindowDpiSafe(chromeHwnd);
            if (currentDpi < MinDpi || currentDpi > MaxDpi) currentDpi = 96;
            if (callerDpi  < MinDpi || callerDpi  > MaxDpi) callerDpi  = 96;

            double currentScale = currentDpi / 96.0;
            double callerScale  = callerDpi  / 96.0;
            bool dpiMatch = currentDpi == callerDpi;

            // Read Chrome's actual rect now (after Stage 2 stabilized things).
            RECT actual = default;
            if (!TryGetWindowRectLTRB(chromeHwnd, out actual))
            {
                AppendStage3Record(callerDpi, currentDpi, dpiMatch, false, false, stage1Target, default, "rect_unavailable", cmd);
                Console.Error.WriteLine("[PLACEMENT:STAGE3] could not read rect -- skip");
                return;
            }

            // If DPIs match and the rect is close to target, nothing to do.
            int dX = Math.Abs(actual.Left - stage1Target.Left);
            int dY = Math.Abs(actual.Top  - stage1Target.Top);
            int dW = Math.Abs(actual.Width  - stage1Target.Width);
            int dH = Math.Abs(actual.Height - stage1Target.Height);

            if (dpiMatch && dX < DeltaThreshold && dY < DeltaThreshold && dW < DeltaThreshold && dH < DeltaThreshold)
            {
                AppendStage3Record(callerDpi, currentDpi, true, false, true, stage1Target, actual, "match_confirmed", cmd);
                Console.Error.WriteLine(
                    $"[PLACEMENT:STAGE3] DPI match confirmed (caller={callerDpi}/{callerScale * 100:F0}% chrome={currentDpi}/{currentScale * 100:F0}%), no correction needed");
                return;
            }

            // Recompute target in the current DPI context. The visible size
            // we want is whatever Stage 1 targeted at the caller's DPI; if
            // Chrome ended up on a higher-DPI monitor, scale up accordingly
            // so the user sees the same physical size on the new display.
            int newW = stage1Target.Width;
            int newH = stage1Target.Height;
            int newL = stage1Target.Left;
            int newT = stage1Target.Top;

            if (!dpiMatch && callerDpi > 0)
            {
                double r = (double)currentDpi / (double)callerDpi;
                newW = (int)Math.Round(stage1Target.Width  * r);
                newH = (int)Math.Round(stage1Target.Height * r);
            }

            // Clamp the recomputed rect to Chrome's current monitor work
            // area so we don't push the window off-screen on the corrected
            // attempt.
            int cX = actual.Left + Math.Max(1, actual.Width) / 2;
            int cY = actual.Top  + Math.Max(1, actual.Height) / 2;
            if (TryGetWorkArea(cX, cY, out var workArea))
            {
                if (newW > workArea.Right - workArea.Left) newW = workArea.Right - workArea.Left;
                if (newH > workArea.Bottom - workArea.Top) newH = workArea.Bottom - workArea.Top;
                if (newL < workArea.Left)              newL = workArea.Left;
                if (newT < workArea.Top)               newT = workArea.Top;
                if (newL + newW > workArea.Right)      newL = workArea.Right - newW;
                if (newT + newH > workArea.Bottom)     newT = workArea.Bottom - newH;
            }

            var corrected = new RECT { Left = newL, Top = newT, Right = newL + newW, Bottom = newT + newH };
            int correctedDx = corrected.Left - actual.Left;
            int correctedDy = corrected.Top  - actual.Top;

            SetWindowPos(chromeHwnd, IntPtr.Zero, newL, newT, newW, newH, SWP_NOZORDER | SWP_NOACTIVATE);
            Console.Error.WriteLine(
                $"[PLACEMENT:STAGE3] DPI mismatch detected at caller={callerScale * 100:F0}% chrome={currentScale * 100:F0}%, "
                + $"correcting by [dX={correctedDx},dY={correctedDy}] new=(L={newL},T={newT},W={newW},H={newH})");

            AppendStage3Record(callerDpi, currentDpi, dpiMatch, true, true, corrected, actual, "corrected", cmd);
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[PLACEMENT:STAGE3] fatal: {ex.GetType().Name}: {ex.Message}"); } catch { }
        }
    }

    // ---- CDP WebSocket helpers (stage 2) -------------------------------------------------------

    /// <summary>
    /// HTTP GET http://127.0.0.1:&lt;port&gt;/json/list and return the
    /// webSocketDebuggerUrl of the first page (type=="page") tab. Returns
    /// null on any error so the caller can fall back to the timeout path.
    /// </summary>
    static async Task<string?> ResolveActivePageWsUrl(int cdpPort, CancellationToken ct)
    {
        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var url = $"http://127.0.0.1:{cdpPort}/json/list";
            var resp = await http.GetAsync(url, ct).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode) return null;
            var body = await resp.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            using var doc = JsonDocument.Parse(body);
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                if (el.TryGetProperty("type", out var typeProp)
                    && typeProp.GetString() == "page"
                    && el.TryGetProperty("webSocketDebuggerUrl", out var wsProp))
                {
                    return wsProp.GetString();
                }
            }
        }
        catch { /* fall through to null */ }
        return null;
    }

    /// <summary>
    /// Open the given CDP WebSocket, enable Page domain, then wait for the
    /// next Page.loadEventFired event. Returns a short trigger code:
    ///   "page_load_event" -- the event fired
    ///   "ws_closed"       -- socket closed before any event
    ///   "timeout"         -- ct fired first (caller-imposed budget)
    ///   "ws_open_failed"  -- could not connect
    /// </summary>
    static async Task<string> WaitForPageLoadEventFired(string wsUrl, CancellationToken ct)
    {
        ClientWebSocket? ws = null;
        try
        {
            ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri(wsUrl), ct).ConfigureAwait(false);
            }
            catch
            {
                return "ws_open_failed";
            }

            // Enable Page domain so Page.* events get delivered. Some Chrome
            // versions don't deliver events without explicit enable.
            var enableMsg = "{\"id\":1,\"method\":\"Page.enable\"}";
            var enableBytes = Encoding.UTF8.GetBytes(enableMsg);
            await ws.SendAsync(enableBytes, WebSocketMessageType.Text, true, ct).ConfigureAwait(false);

            // Read frames until Page.loadEventFired, ws close, or cancel.
            var buf = new byte[8192];
            var sb = new StringBuilder();
            while (!ct.IsCancellationRequested && ws.State == WebSocketState.Open)
            {
                sb.Clear();
                WebSocketReceiveResult res;
                do
                {
                    res = await ws.ReceiveAsync(buf, ct).ConfigureAwait(false);
                    if (res.MessageType == WebSocketMessageType.Close) return "ws_closed";
                    sb.Append(Encoding.UTF8.GetString(buf, 0, res.Count));
                } while (!res.EndOfMessage);

                var msg = sb.ToString();
                // Cheap substring probe -- CDP frames are JSON and the method
                // string is uniquely identifiable. Avoids parsing every frame
                // (Chrome can be chatty with Page.frameNavigated, etc.).
                if (msg.Contains("\"Page.loadEventFired\""))
                    return "page_load_event";
            }
        }
        catch (OperationCanceledException)
        {
            return "timeout";
        }
        catch { /* fall through */ }
        finally
        {
            try { ws?.Dispose(); } catch { }
        }
        return ct.IsCancellationRequested ? "timeout" : "ws_closed";
    }

    // ---- DPI helpers (stage 3) -----------------------------------------------------------------

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern uint GetDpiForWindow(IntPtr hwnd);

    /// <summary>
    /// GetDpiForWindow wrapped against older Win10 builds that may not export
    /// it. Falls back to 96 (100% scale) on any failure so Stage 3 still has
    /// a non-zero baseline to reason about.
    /// </summary>
    static uint TryGetWindowDpiSafe(IntPtr hwnd)
    {
        try
        {
            if (hwnd == IntPtr.Zero) return 96;
            uint dpi = GetDpiForWindow(hwnd);
            return dpi == 0 ? 96 : dpi;
        }
        catch
        {
            return 96;
        }
    }

    // Telemetry helpers (AppendStage2Record / AppendStage3Record / RECT array
    // writer) live in MyCdpContext.Stage23Telemetry.cs to keep this file
    // focused on the actual stage 2/3 flow.
}
