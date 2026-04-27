// AppBotEyeGlobalMode.Reaper.cs -- class EyeZombieReaper (periodic stale wkappbot-core cleanup)
// Reaper loop is inline in EyeGlobalPollingLoop (closure-captured locals prevent extraction).
// This partial class holds constants + documents the watchdog audit for CoreSwapWatcher kill logic.
//
// Background: 53 wkappbot-core zombie processes accumulated over 24h after hot-swap events.
// CoreSwapWatcher quiet-swap watcher kills old core on .new.exe rename but something caused
// old cores to survive. Periodic reaper kills stale wkappbot-core workers (startup age > 3 min,
// not self PID) every 5 minutes to prevent accumulation post-swap cleanup failures.
namespace WKAppBot.CLI;

internal partial class Program
{
    // EyeZombieReaper tuning: how long after swap before cleanup kicks in
    private const int EyeReaperInitialDelayMinutes = 5;
    private const int EyeReaperIntervalMinutes = 5;
    // Grace period: processes started < 3 min ago are not killed (may be freshly swapped core)
    private const int EyeReaperGracePeriodMinutes = 3;
}
