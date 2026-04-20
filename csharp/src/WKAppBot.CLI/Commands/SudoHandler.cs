// SudoHandler.cs -- Core-side --sudo request pipeline (v5.14.7).
//
// Design principle: hot-swap (file rename) and Eye spawn are SEPARATE concerns.
// This handler orchestrates them with a clean precondition-first ordering so that
// in the common case (admin Eye alive) we never touch the filesystem at all --
// preventing FSW cascades in other wkappbot processes.
//
// Flow:
//   1. Pre-check -- Ping admin Eye 100ms. Alive -> reuse, return true, done.
//   2. Need to spawn:
//      2a. Promote pending .new.exe (rename only, NEVER launches Eye).
//      2b. UAC + runas StartTracked (Core's request shows UAC dialog to user).
//      2c. Post-UAC 100ms handshake -- pre-booting admin Eye may have completed
//          during the UAC wait; if responsive, reuse it instead of our spawn.
//      2d. Poll up to 10s for our spawned admin Eye's pipe to appear.
//
// The separate LaunchElevatedEye remains in place for other callers (Readiness,
// elevation delegation). This handler is Core's focused --sudo entry.

using System.Diagnostics;
using WKAppBot.Win32.Input;

namespace WKAppBot.CLI;

internal static class SudoHandler
{
    /// <summary>
    /// Ensure an admin Eye is alive and responsive for a --sudo request.
    /// Returns true if we can proceed with ExecuteViaProxy on <see cref="ElevatedEyeClient"/>.
    /// Returns false if UAC was cancelled or spawn failed -- caller should fall through
    /// to non-admin execution with a FALLBACK marker.
    /// </summary>
    public static bool EnsureAdminForSudo(string reason = "--sudo request")
    {
        if (ElevationHelper.ShortCircuitIfUacFailed($"SudoHandler({reason})")) return false;
        PulseStep.Line("SudoHandler: pre-check Ping(100)");
        if (ElevatedEyeClient.Ping(100))
        {
            PulseStep.Line("SudoHandler: admin Eye alive -- reuse, no hot-swap, no spawn");
            // Print "reusing" line only when --sudo was typed explicitly by the user,
            // not when Launcher auto-inherited it. Auto-inherit sets WKAPPBOT_SUDO_AUTO=1.
            // This keeps `wkappbot chat --help` etc. quiet while still giving feedback
            // to intentional `wkappbot <cmd> --sudo` invocations.
            bool explicitSudo = Environment.GetEnvironmentVariable("WKAPPBOT_SUDO_AUTO") != "1";
            if (explicitSudo)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine("[SUDO] admin Eye alive -- reusing existing instance");
                Console.ResetColor();
            }
            return true;
        }

        // Pre-check missed. Distinguishing "admin Eye not running" (cold start, normal)
        // from "pipe existed but handshake failed" (regression) requires elapsed-time
        // knowledge which Ping() abstracts away. The Launcher's --sudo probe already
        // makes this distinction and emits the big banner + bug suggest if it was a
        // real handshake miss -- no need to duplicate here. Just log the pulse line
        // and proceed to spawn.
        PulseStep.Line("SudoHandler: admin Eye unresponsive -- need spawn path");

        // Step 2a: hot-swap pending .new.exe (file rename only, NO Eye launch)
        PromoteNewExeIfPending();

        // Step 2b-2c-2d: UAC + handshake + poll
        return SpawnAndAwaitAdmin(reason);
    }

    /// <summary>
    /// Rename-only hot-swap: if wkappbot-core.new.exe is pending, promote it to
    /// wkappbot-core.exe so the admin Eye we are about to spawn runs the latest
    /// code. This is the ONLY thing this function does -- it never spawns any
    /// process. Windows permits renaming a running exe; deletion is disallowed,
    /// which is why we use rename + backup timestamp.
    /// </summary>
    static void PromoteNewExeIfPending()
    {
        try
        {
            var exePath = Environment.ProcessPath ?? "";
            var exeDir = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(exeDir)) return;

            var newExePath = Path.Combine(exeDir, "wkappbot-core.new.exe");
            if (!File.Exists(newExePath)) return;

            var backupPath = Path.Combine(exeDir,
                $"wkappbot-core.old-sudo-{DateTime.Now:yyyyMMdd-HHmmss}.exe");

            Console.Error.WriteLine("[SUDO:HOT-SWAP] pending .new.exe detected -- promoting before spawn");
            try { File.Move(exePath, backupPath); }
            catch { /* already renamed by parallel promoter -- non-fatal */ }
            File.Move(newExePath, exePath);
            Console.Error.WriteLine("[SUDO:HOT-SWAP] promoted -- next admin Eye will run latest core");
        }
        catch (Exception ex)
        {
            // Hot-swap is best-effort. If it fails, continue with current exe.
            Console.Error.WriteLine($"[SUDO:HOT-SWAP] skipped: {ex.Message}");
        }
    }

    /// <summary>
    /// Spawn admin Eye via runas + UAC. Immediately after (regardless of UAC
    /// outcome) do a 100ms handshake in case a pre-booting admin Eye happened
    /// to finish during the UAC dialog wait. If UAC approved and handshake
    /// still unresponsive, poll the admin pipe up to 10s.
    /// </summary>
    static bool SpawnAndAwaitAdmin(string reason)
    {
        // Fix TMP/TEMP before runas: ShellExecute inherits caller's env, so Git
        // Bash "/tmp" would break .NET bundle extract on the admin child and
        // crash it with STATUS_DLL_INIT_FAILED before Main() runs.
        ElevationHelper.SanitizeEnvForElevatedSpawn();

        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var psi = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = "eye --elevated",
            UseShellExecute = true,
            Verb = "runas",
            WindowStyle = ProcessWindowStyle.Hidden,
        };

        // Audible + visual cue BEFORE UAC so user knows why it appeared
        PlayElevationMelody();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine($"[SUDO] ▼ Requesting admin rights: {reason}");
        var trail = PulseStep.GetRecentTrail(5);
        if (trail.Count > 0)
        {
            Console.Error.WriteLine("[SUDO] -- recent step trail --");
            foreach (var line in trail) Console.Error.WriteLine($"[SUDO]   {line}");
            Console.Error.WriteLine("[SUDO] ----------------------");
        }
        Console.ResetColor();

        Process? proc = null;
        bool uacApproved = false;
        System.ComponentModel.Win32Exception? uacEx = null;
        try
        {
            proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "SUDO-SPAWN");
            uacApproved = proc != null;
            PulseStep.Line($"SudoHandler: UAC result approved={uacApproved}");
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            uacEx = ex;
            if (unchecked((uint)ex.NativeErrorCode) == 0xC0000142u)
                PulseStep.Line("SudoHandler: admin Eye spawn failed (STATUS_DLL_INIT_FAILED)");
            else
                PulseStep.Line($"SudoHandler: UAC cancelled err={ex.NativeErrorCode}");
        }

        // Prominent UAC outcome marker -- not just PulseStep ring buffer
        Console.ForegroundColor = uacApproved ? ConsoleColor.Green : ConsoleColor.DarkYellow;
        Console.Error.WriteLine($"[SUDO:UAC] approved={(uacApproved ? "YES" : "NO")}"
            + (uacEx != null ? $" (err={uacEx.NativeErrorCode})" : ""));
        Console.ResetColor();

        // Post-UAC 100ms handshake -- pre-booting admin Eye may have finished
        // during the UAC dialog wait.
        if (ElevatedEyeClient.Ping(100))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine("[SUDO] admin Eye responsive post-UAC -- reusing (skipping our spawn)");
            Console.ResetColor();
            VerifyAndReportAdminIntegrity();
            return true;
        }

        if (!uacApproved)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var err = uacEx?.NativeErrorCode;
            var suffix = err.HasValue && unchecked((uint)err.Value) == 0xC0000142u
                ? "spawn failed (STATUS_DLL_INIT_FAILED)"
                : $"UAC cancelled (err={err})";
            Console.Error.WriteLine($"[SUDO] {suffix} -- no admin Eye available");
            Console.ResetColor();
            ElevationHelper.MarkUacFailure("SudoHandler", suffix);
            return false;
        }

        // UAC approved + our spawn in progress -- wait until the admin Eye becomes ready.
        // Once the sudo path has been taken, do not give the caller a fallback window.
        // BUT: if the spawned admin process dies (STATUS_DLL_INIT_FAILED 0xC0000142 is a
        // known crash during admin-mode .NET initialization), stop waiting -- otherwise
        // we hang forever. Liveness check happens every loop tick.
        int tick = 0;
        while (true)
        {
            Thread.Sleep(250);
            tick++;
            if (ElevatedEyeClient.Ping(100))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine("[SUDO] admin Eye pipe up -- handshake OK");
                Console.ResetColor();
                VerifyAndReportAdminIntegrity();
                return true;
            }

            // Liveness: if the spawned admin Eye process died, no amount of waiting helps.
            // Surface the exit code so the operator can see STATUS_DLL_INIT_FAILED etc.
            if (proc != null)
            {
                try
                {
                    if (proc.HasExited)
                    {
                        var exit = proc.ExitCode;
                        var hex = unchecked((uint)exit).ToString("X8");
                        var label = unchecked((uint)exit) == 0xC0000142u
                            ? "STATUS_DLL_INIT_FAILED (admin .NET init crash)"
                            : $"exit=0x{hex}";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine($"[SUDO] admin Eye process died before pipe came up -- {label}");
                        Console.ResetColor();
                        PulseStep.Line($"SudoHandler: admin Eye died exit=0x{hex} after {tick * 250}ms");
                        ElevationHelper.MarkUacFailure("SudoHandler", label);
                        return false;
                    }
                }
                catch { /* process handle race -- keep polling */ }
            }

            // Emit a progress breadcrumb every 4 seconds so a genuinely-slow admin startup
            // does not look like a silent hang.
            if (tick % 16 == 0)
                Console.Error.WriteLine($"[SUDO] still waiting for admin Eye pipe ({tick * 250 / 1000}s)...");
        }
    }

    /// <summary>
    /// After admin Eye becomes responsive, verify its integrity level is actually High
    /// (admin) and emit a prominent marker. If the "admin" Eye is somehow running as
    /// medium integrity (e.g. pipe squatter, misconfigured runas), the user sees the
    /// discrepancy immediately instead of assuming elevation succeeded.
    /// </summary>
    static void VerifyAndReportAdminIntegrity()
    {
        try
        {
            // Find wkappbot-core processes. The admin one has High/System integrity.
            uint highestIL = 0;
            uint highestPid = 0;
            foreach (var p in System.Diagnostics.Process.GetProcessesByName("wkappbot-core"))
            {
                try
                {
                    var il = WKAppBot.Win32.Native.NativeMethods.GetProcessIntegrityLevel((uint)p.Id);
                    if (il > highestIL) { highestIL = il; highestPid = (uint)p.Id; }
                }
                catch { }
                finally { p.Dispose(); }
            }
            // 0x1000=Low, 0x2000=Medium (user), 0x3000=High (admin), 0x4000=System
            var label = highestIL switch
            {
                >= 0x4000 => "System",
                >= 0x3000 => "High (admin)",
                >= 0x2000 => "Medium (user)",
                _ => $"Low/Unknown (0x{highestIL:X})"
            };
            var color = highestIL >= 0x3000 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = color;
            Console.Error.WriteLine($"[SUDO:VERIFY] highest wkappbot-core integrity level: {label} pid={highestPid}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUDO:VERIFY] integrity check skipped: {ex.Message}");
        }
    }

    /// <summary>Fire-and-forget speak: alerts user before UAC dialog appears.</summary>
    static void PlayElevationMelody()
    {
        _ = Task.Run(() =>
        {
            try
            {
                AppBotPipe.Spawn(
                    Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? "", "wkappbot.exe"),
                    "speak 관리자 권한이 필요합니다",
                    Environment.CurrentDirectory, caller: "SUDO-SPEAK");
            }
            catch { }
        });
    }
}
