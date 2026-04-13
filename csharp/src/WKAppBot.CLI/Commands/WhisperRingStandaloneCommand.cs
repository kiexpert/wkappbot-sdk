// WhisperRingStandaloneCommand.cs — Standalone Whisper Ring process
// Usage: wkappbot whisper-ring [x] [y]
// Self-contained: WhisperEngine + WhisperRing + ExpDB in own process.

namespace WKAppBot.CLI;

internal partial class Program
{
    static int WhisperRingStandaloneCommand(string[] args)
    {
        int ringX = 0, ringY = 0;
        if (args.Length >= 2)
        {
            int.TryParse(args[0], out ringX);
            int.TryParse(args[1], out ringY);
        }

        int ppid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out ppid);

        Console.Error.WriteLine($"[WHISPER-RING] Standalone (parent={ppid}, pos=({ringX},{ringY}))");

        // Run on dedicated STA thread with WPF message pump
        var thread = new Thread(() => RunWhisperRingSta(ringX, ringY, ppid));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Name = "WhisperRing-Main-STA";
        thread.Start();
        thread.Join(); // block until done

        Console.WriteLine("[WHISPER-RING] Exiting");
        return 0;
    }

    static void RunWhisperRingSta(int ringX, int ringY, int ppid)
    {
        WhisperEngine? engine = null;
        WhisperExperienceDb? exp = null;
        WhisperRingWindow? window = null;
        try
        {
            engine = new WhisperEngine();
            if (!engine.Start())
            {
                Console.WriteLine("[WHISPER-RING] No microphone — exiting");
                engine.Dispose();
                return;
            }

            // Create WPF window directly on this STA thread
            window = new WhisperRingWindow();
            window.Left = ringX;
            window.Top = ringY;

            exp = new WhisperExperienceDb();
            exp.StartLogging();
            bool sttOk = exp.StartStt();

            // Auto-study (only when user idle >= 1 hour)
            DateTime lastStudyTime = DateTime.MinValue;
            var startTime = DateTime.UtcNow;
            exp.OnAutoStudyNeeded += (count) =>
            {
                var now = DateTime.UtcNow;
                var userIdleMs = WKAppBot.Win32.Native.NativeMethods.GetUserIdleMs();
                if (userIdleMs < 3600_000) // 1 hour idle required
                { exp.NotifyAutoStudyDone(); return; }
                if ((now - startTime).TotalMinutes < 2 || (now - lastStudyTime).TotalMinutes < 2)
                { exp.NotifyAutoStudyDone(); return; }
                lastStudyTime = now;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try { WhisperStudyCommand(["--batch", count.ToString()]); }
                    catch { }
                    finally { exp.NotifyAutoStudyDone(); }
                });
            };

            // OnFrame → dispatch to WPF thread
            var dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            int frameCount = 0;
            engine.OnFrame += (frame) =>
            {
                if (++frameCount % 100 == 0) // every ~10s
                    Console.Error.WriteLine($"[WHISPER-RING] frame={frameCount} mode={frame.Mode} t={DateTime.Now:HH:mm:ss}");
                dispatcher.BeginInvoke(() =>
                {
                    var (lastStt, lastSttTicks, lastSttMode, _) = exp?.GetStatus() ?? (null, 0, "QUIET", 0);
                    long ageTicks = lastStt != null ? DateTime.UtcNow.Ticks - lastSttTicks : long.MaxValue;
                    int segFrames = exp?.SegmentFrames ?? 0;
                    window.UpdateSpectrum(frame.Levels, frame.MaxLevel,
                        frame.Mode, frame.Token, frame.RecentTokens, lastStt, ageTicks, lastSttMode,
                        segFrames, frame.SoundCode, frame.VoiceLevels);
                    window.EnsureTopmost();
                });
                exp?.LogFrame(frame);
            };

            exp?.SetMicChannels(engine.Channels);
            engine.OnMicData += (buf, len) => exp?.WriteMicData(buf, len);

            exp!.OnMicSegmentReady += (mp3Path) =>
            {
                try
                {
                    var unknownDir = Path.Combine(Path.GetDirectoryName(mp3Path)!, "..", "_unknown");
                    Directory.CreateDirectory(unknownDir);
                    File.Move(mp3Path, Path.Combine(unknownDir, Path.GetFileName(mp3Path)));
                }
                catch { }
            };

            // Parent PID watchdog — timer on dispatcher
            if (ppid > 0)
            {
                var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                timer.Tick += (s, e) =>
                {
                    try { System.Diagnostics.Process.GetProcessById(ppid); }
                    catch
                    {
                        Console.WriteLine("[WHISPER-RING] Parent gone — closing");
                        window.Close();
                    }
                };
                timer.Start();
            }

            Console.Error.WriteLine($"[WHISPER-RING] Started (stt={sttOk})");
            window.Show();
            System.Windows.Threading.Dispatcher.Run(); // WPF message pump
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[WHISPER-RING] Error: {ex.Message}");
        }
        finally
        {
            exp?.Stop();
            engine?.Dispose();
        }
    }
}
