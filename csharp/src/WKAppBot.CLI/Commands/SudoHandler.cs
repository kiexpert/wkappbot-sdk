// SudoHandler.cs — Core-side --sudo request pipeline (v5.14.7).
//
// Design principle: hot-swap (file rename) and Eye spawn are SEPARATE concerns.
// This handler orchestrates them with a clean precondition-first ordering so that
// in the common case (admin Eye alive) we never touch the filesystem at all —
// preventing FSW cascades in other wkappbot processes.
//
// Flow:
//   1. Pre-check — Ping admin Eye 100ms. Alive → reuse, return true, done.
//   2. Need to spawn:
//      2a. Promote pending .new.exe (rename only, NEVER launches Eye).
//      2b. UAC + runas StartTracked (Core's request shows UAC dialog to user).
//      2c. Post-UAC 100ms handshake — pre-booting admin Eye may have completed
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
    /// Returns false if UAC was cancelled or spawn failed — caller should fall through
    /// to non-admin execution with a FALLBACK marker.
    /// </summary>
    public static bool EnsureAdminForSudo(string reason = "--sudo request")
    {
        PulseStep.Line("SudoHandler: pre-check Ping(100)");
        if (ElevatedEyeClient.Ping(100))
        {
            PulseStep.Line("SudoHandler: admin Eye alive — reuse, no hot-swap, no spawn");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine("[SUDO] admin Eye alive — reusing existing instance");
            Console.ResetColor();
            return true;
        }

        PulseStep.Line("SudoHandler: admin Eye unresponsive — need spawn path");

        // Step 2a: hot-swap pending .new.exe (file rename only, NO Eye launch)
        PromoteNewExeIfPending();

        // Step 2b-2c-2d: UAC + handshake + poll
        return SpawnAndAwaitAdmin(reason);
    }

    /// <summary>
    /// Rename-only hot-swap: if wkappbot-core.new.exe is pending, promote it to
    /// wkappbot-core.exe so the admin Eye we are about to spawn runs the latest
    /// code. This is the ONLY thing this function does — it never spawns any
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

            Console.Error.WriteLine("[SUDO:HOT-SWAP] pending .new.exe detected — promoting before spawn");
            try { File.Move(exePath, backupPath); }
            catch { /* already renamed by parallel promoter — non-fatal */ }
            File.Move(newExePath, exePath);
            Console.Error.WriteLine("[SUDO:HOT-SWAP] promoted — next admin Eye will run latest core");
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
            Console.Error.WriteLine("[SUDO] ── recent step trail ──");
            foreach (var line in trail) Console.Error.WriteLine($"[SUDO]   {line}");
            Console.Error.WriteLine("[SUDO] ──────────────────────");
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
            PulseStep.Line($"SudoHandler: UAC cancelled err={ex.NativeErrorCode}");
        }

        // Prominent UAC outcome marker — not just PulseStep ring buffer
        Console.ForegroundColor = uacApproved ? ConsoleColor.Green : ConsoleColor.DarkYellow;
        Console.Error.WriteLine($"[SUDO:UAC] approved={(uacApproved ? "YES" : "NO")}"
            + (uacEx != null ? $" (err={uacEx.NativeErrorCode})" : ""));
        Console.ResetColor();

        // Post-UAC 100ms handshake — pre-booting admin Eye may have finished
        // during the UAC dialog wait.
        if (ElevatedEyeClient.Ping(100))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine("[SUDO] admin Eye responsive post-UAC — reusing (skipping our spawn)");
            Console.ResetColor();
            VerifyAndReportAdminIntegrity();
            return true;
        }

        if (!uacApproved)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[SUDO] UAC cancelled (err={uacEx?.NativeErrorCode}) — no admin Eye available");
            Console.ResetColor();
            return false;
        }

        // UAC approved + our spawn in progress — poll for pipe readiness up to 10s
        for (int i = 0; i < 40; i++)
        {
            Thread.Sleep(250);
            if (ElevatedEyeClient.Ping(100))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine("[SUDO] admin Eye pipe up — handshake OK");
                Console.ResetColor();
                VerifyAndReportAdminIntegrity();
                return true;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine("[SUDO] admin Eye did not respond within 10s");
        Console.ResetColor();
        return false;
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
