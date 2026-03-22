// WhisperRingStandaloneCommand.cs — Standalone Whisper Ring process
// Usage: wkappbot whisper-ring [x] [y]
// Self-contained: WhisperEngine + WhisperRing + ExpDB in own process.
// Eye spawns this → WPF + audio model memory isolated from Eye.

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

        Console.WriteLine($"[WHISPER-RING] Standalone process starting at ({ringX},{ringY})...");
        WhisperEngine? engine = null;
        WhisperRingHost? ring = null;
        WhisperExperienceDb? exp = null;
        try
        {
            engine = new WhisperEngine();
            if (!engine.Start())
            {
                Console.WriteLine("[WHISPER-RING] No microphone — exiting");
                engine.Dispose();
                return 0;
            }

            ring = new WhisperRingHost();
            ring.Start(ringX, ringY);

            exp = new WhisperExperienceDb();
            exp.StartLogging();
            bool sttOk = exp.StartStt();

            // Auto-study
            DateTime lastStudyTime = DateTime.MinValue;
            var startTime = DateTime.UtcNow;
            exp.OnAutoStudyNeeded += (count) =>
            {
                var now = DateTime.UtcNow;
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

            engine.OnFrame += (frame) =>
            {
                if (ring.IsAlive)
                {
                    var (lastStt, lastSttTicks, lastSttMode, _) = exp?.GetStatus() ?? (null, 0, "QUIET", 0);
                    long ageTicks = lastStt != null ? DateTime.UtcNow.Ticks - lastSttTicks : long.MaxValue;
                    int segFrames = exp?.SegmentFrames ?? 0;
                    ring.UpdateSpectrum(frame.Levels, frame.MaxLevel,
                        frame.Mode, frame.Token, frame.RecentTokens, lastStt, ageTicks, lastSttMode,
                        segFrames, frame.SoundCode, frame.VoiceLevels);
                }
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

            Console.WriteLine($"[WHISPER-RING] Started (stt={sttOk})");

            // Keep alive — exit when ring closes OR parent dies
            int ppid = 0;
            int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out ppid);
            int parentCheck = 0;
            while (ring.IsAlive)
            {
                Thread.Sleep(100);
                if (ppid > 0 && ++parentCheck % 50 == 0)
                {
                    try { System.Diagnostics.Process.GetProcessById(ppid); }
                    catch { Console.WriteLine("[WHISPER-RING] Parent gone — exiting"); break; }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[WHISPER-RING] Error: {ex.Message}");
        }
        finally
        {
            exp?.Stop();
            engine?.Dispose();
            ring?.BeginFadeOut();
            Thread.Sleep(1000);
            ring?.Dispose();
        }

        Console.WriteLine("[WHISPER-RING] Exiting");
        return 0;
    }
}
