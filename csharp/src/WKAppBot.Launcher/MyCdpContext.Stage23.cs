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
// File layout (2026-05-16 split, ~400-line cap)
//   MyCdpContext.Stage23.cs           -- this file: watcher spawn/entry, Stage 2 flow
//   MyCdpContext.Stage3.cs            -- TryStage3DpiAwareMatch + DPI helpers
//   MyCdpContext.Stage23Ws.cs         -- CDP WebSocket helpers used by Stage 2
//   MyCdpContext.Stage23Telemetry.cs  -- AppendStage2Record / AppendStage3Record
//
// Telemetry: every stage outcome is appended to wkappbot.hq/runtime/cdp-state.jsonl
// with "stage": 2 | 3 records so cdp-mon can audit timing and accuracy.

using System.Text;
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
    /// then exits. Never crashes the parent -- all exceptions are swallowed.
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
            // Stage 2 timeout: 10s max (was 3s -- premature ws_receive_error spam when
            // ChatGPT/Gemini/Copilot pages took longer than 3s to fire Page.loadEventFired
            // on cold first launch or DPI heavy monitors). 10s matches the original
            // docstring intent at the top of this file and gives the foreground ASK
            // pipelines CDP socket enough headroom to finish injection before our
            // secondary watcher races it. Since the watcher is a fire and forget child
            // process, a 10s budget has no impact on user visible CLI latency.
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
                // not re-fire -- fall back to the 10s timer and re-validate
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

        // Auto-suggest GATE (silent-yield pattern, 2026-05-16):
        // ws_receive_error / ws_closed / no_ws_url / timeout are BENIGN when the
        // foreground ASK pipeline is the legitimate owner of this CDP port and
        // is actively pumping commands -- our secondary watcher socket races
        // against the foreground WS and intermittently fails on frame receive,
        // even though Stage 1 placement is correct. The previous behavior
        // spam-fired BUG-AUTO suggest on every transient race, flooding 30+
        // identical items per day across all projects (personal-docs,
        // wkappbot-sdk, WKAppBot). New rule:
        //
        //   1. If placementOk == true, the actual user-visible outcome is
        //      correct -- suppress the suggest regardless of trigger code.
        //      Stage 2 is purely a re-validation pass; placement OK means
        //      Stage 1 already landed Chrome where the user wanted it and
        //      the WS frame failure had no functional impact.
        //   2. If placementOk == false BUT the trigger is a benign CDP race
        //      (ws_receive_error / ws_closed / no_ws_url / ws_open_failed /
        //      cdp_enable_failed / timeout), still suppress -- we can't tell
        //      from a failed WS frame whether placement actually drifted,
        //      and the next Stage 1 placement pass on the user's next ask
        //      will fix it without spam.
        //   3. Only fire BUG-AUTO when placementOk == false AND we have a
        //      structural CDP error (cdp_error_frame) that suggests a real
        //      Chrome/CDP problem, not a transient socket race.
        //
        // See also: a11y-focus-steal-user-active-silent-yield skill -- same
        // pattern (silently yield when the foreground actor is legitimately
        // racing us; only escalate on true involuntary failures).
        bool isBenignRace =
            trigger == "ws_receive_error" || trigger == "ws_closed" ||
            trigger == "ws_open_failed"  || trigger == "no_ws_url" ||
            trigger == "cdp_enable_failed" || trigger == "timeout";

        bool shouldFireSuggest = !placementOk
            && !isBenignRace
            && (trigger.Contains("cdp_error_frame") || trigger.StartsWith("error:"));

        if (!shouldFireSuggest && (trigger.Contains("timeout") || trigger.Contains("cdp") || trigger.Contains("ws") || trigger.Contains("no_ws_url")))
        {
            Console.Error.WriteLine(
                $"[PLACEMENT:STAGE2] suppress BUG-AUTO (silent-yield): trigger={trigger} placementOk={placementOk} -- benign CDP race or placement still correct");
        }

        if (shouldFireSuggest)
        {
            var suggestMsg = $"BUG: Stage 2 ask anomaly (cmd={cmd}). trigger={trigger} elapsed={sw.ElapsedMilliseconds}ms. " +
                            $"Chrome placement or CDP communication failed. " +
                            $"Repro: wkappbot ask gpt 'test'";
            try
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "wkappbot",
                        Arguments = $"suggest \"{suggestMsg}\" --requirement \"wkappbot ask gpt 'test' => within 20s\" " +
                                   "--requirement \"wkappbot ask gemini 'test' => within 20s\" " +
                                   "--requirement \"wkappbot eye tick => healthy\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
                Console.Error.WriteLine($"[PLACEMENT:STAGE2] auto-suggest submitted for {trigger}");
            }
            catch { /* best-effort */ }
        }
    }

    // CDP WebSocket helpers (ResolveActivePageWsUrl, WaitForPageLoadEventFired)
    // live in MyCdpContext.Stage23Ws.cs. Stage 3 / DPI helpers
    // (TryStage3DpiAwareMatch, TryGetWindowDpiSafe, GetDpiForWindow P/Invoke)
    // live in MyCdpContext.Stage3.cs. Telemetry helpers
    // (AppendStage2Record / AppendStage3Record / RECT array writer) live in
    // MyCdpContext.Stage23Telemetry.cs.
}