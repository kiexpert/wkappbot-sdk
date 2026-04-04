using System.Text;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    private CancellationTokenSource? _minimizeDumpCts;
    private string? _pendingMinimizeReason;
    private DateTimeOffset? _pendingMinimizeAtUtc;

    private static string GetMinimizeDumpPath()
    {
        var baseDir = Path.GetDirectoryName(Environment.ProcessPath ?? AppContext.BaseDirectory) ?? AppContext.BaseDirectory;
        var logDir = Path.Combine(baseDir, "wkappbot.hq", "logs");
        Directory.CreateDirectory(logDir);
        return Path.Combine(logDir, "cdp-minimize-watchdog.jsonl");
    }

    private void ScheduleMinimizeDump(string reason, IntPtr hwnd, int delayMs = 5000)
    {
        CancelMinimizeDump("reschedule");
        if (hwnd == IntPtr.Zero) return;

        var cts = new CancellationTokenSource();
        _minimizeDumpCts = cts;
        _pendingMinimizeReason = reason;
        _pendingMinimizeAtUtc = DateTimeOffset.UtcNow;

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(delayMs, cts.Token);
                if (cts.IsCancellationRequested) return;
                if (!IsWindow(hwnd) || !IsIconic(hwnd)) return;

                var payload = new JsonObject
                {
                    ["ts"] = DateTimeOffset.Now.ToString("O"),
                    ["reason"] = reason,
                    ["delayMs"] = delayMs,
                    ["hwnd"] = $"0x{hwnd.ToInt64():X8}",
                    ["targetId"] = TargetId ?? "",
                    ["ws"] = WebSocketUrl ?? "",
                    ["op"] = OperationContext ?? "",
                    ["chromePid"] = ChromePid,
                    ["pendingSinceUtc"] = _pendingMinimizeAtUtc?.ToString("O") ?? "",
                };

                try
                {
                    payload["pageUrl"] = await EvalAsync("location.href") ?? "";
                    payload["pageTitle"] = await EvalAsync("document.title") ?? "";
                }
                catch
                {
                    payload["pageUrl"] = "";
                    payload["pageTitle"] = "";
                }

                await File.AppendAllTextAsync(GetMinimizeDumpPath(), payload.ToJsonString() + Environment.NewLine, Encoding.UTF8, CancellationToken.None);
                Console.Error.WriteLine($"[CDP:MINIMIZE] dump persisted after {delayMs}ms ({reason}) hwnd={hwnd:X8}");

                // Force-restore: if Chrome is STILL minimized after dump delay, something forgot to restore.
                // SW_SHOWNOACTIVATE=4: visible without stealing focus from user's active window.
                if (IsWindow(hwnd) && IsIconic(hwnd))
                {
                    ShowWindowNative(hwnd, 4); // SW_SHOWNOACTIVATE
                    Console.Error.WriteLine($"[CDP:MINIMIZE] force-restored hwnd={hwnd:X8} (was stuck minimized)");
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CDP:MINIMIZE] dump failed: {ex.Message}");
            }
        });
    }

    private void CancelMinimizeDump(string restoreReason)
    {
        var cts = _minimizeDumpCts;
        _minimizeDumpCts = null;
        if (cts == null) return;
        try { cts.Cancel(); } catch { }
        cts.Dispose();
        if (!string.IsNullOrEmpty(_pendingMinimizeReason))
            Console.WriteLine($"[CDP:MINIMIZE] dump cancelled ({restoreReason}) prev={_pendingMinimizeReason}");
        _pendingMinimizeReason = null;
        _pendingMinimizeAtUtc = null;
    }
}
