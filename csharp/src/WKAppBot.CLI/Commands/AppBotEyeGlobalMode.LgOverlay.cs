using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// LG Smart Assistant / LG overlay topmost screen-cover detection and neutralization.
// Extracted from AppBotEyeGlobalMode.cs to keep the root partial under the 400-line cap.
internal partial class Program
{
    static readonly HashSet<string> _knownLgOverlayProcesses = new(StringComparer.OrdinalIgnoreCase)
    {
        "LGDisplayExtension",
        "LGHotkeyExtension",
        "LGSmartAssistantExtension",
        "LGSmartAssistant",
        "LGLivelyWallpaper",
        "LG PC Care",
    };
    static DateTime _lastLgOverlayGuardUtc = DateTime.MinValue;
    static nint _lastLgOverlayHwnd;

    static bool TryGuardLgOverlay(string logPrefix, string? slackBotToken = null, string? slackChannel = null)
    {
        try
        {
            if (_lastLgOverlayGuardUtc != DateTime.MinValue &&
                (DateTime.UtcNow - _lastLgOverlayGuardUtc).TotalSeconds < 5)
                return false;

            var lgHwnd = FindLgOverlayWindow();
            if (lgHwnd == IntPtr.Zero) return false;

            _lastLgOverlayGuardUtc = DateTime.UtcNow;
            _lastLgOverlayHwnd = lgHwnd;

            var fgBuf = new StringBuilder(256);
            NativeMethods.GetWindowTextW(NativeMethods.GetForegroundWindow(), fgBuf, fgBuf.Capacity);
            var fgTitle = fgBuf.ToString();

            NativeMethods.GetWindowThreadProcessId(lgHwnd, out uint lgPid);
            string procName = "";
            try { procName = lgPid > 0 ? Process.GetProcessById((int)lgPid).ProcessName : ""; } catch { }

            Console.WriteLine($"{logPrefix} LG overlay topmost! pid={lgPid} proc={procName} fg=\"{fgTitle}\"");

            ApplyLgOverlayNeutralize(lgHwnd, logPrefix);

            NativeMethods.PostMessageW(lgHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine($"{logPrefix} -> WM_CLOSE sent");

            Thread.Sleep(500);
            string result;
            if (NativeMethods.IsWindow(lgHwnd))
            {
                Console.WriteLine($"{logPrefix} WM_CLOSE ignored -> applying shrink fallback");
                ApplyLgOverlayFallbackLayout(lgHwnd, logPrefix);
                Thread.Sleep(250);
                result = "shrunk";

                if (NativeMethods.IsWindow(lgHwnd))
                {
                    result = "kill";
                    Console.WriteLine($"{logPrefix} shrink fallback insufficient -> killing process");
                }

                if (lgPid > 0)
                {
                    try
                    {
                        if (result == "kill")
                        {
                            Process.GetProcessById((int)lgPid).Kill();
                            Console.WriteLine($"{logPrefix} Kill OK (pid={lgPid})");
                        }
                    }
                    catch (Exception kex)
                    {
                        result = $"kill-failed: {kex.Message}";
                        Console.WriteLine($"{logPrefix} Kill failed: {kex.Message}");
                    }
                }
            }
            else
            {
                result = "closed";
                Console.WriteLine($"{logPrefix} overlay closed OK");
            }

            if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
            {
                var alertMsg = $":warning: *{(string.IsNullOrEmpty(procName) ? "LG overlay" : procName)}* screen-cover detected -> {result}\n포그라운드: `{fgTitle}`";
                Task.Run(async () =>
                {
                    try { await SlackSendViaApi(slackBotToken, slackChannel, alertMsg, username: "앱봇아이"); }
                    catch { }
                });
            }

            return true;
        }
        catch { return false; }
    }

    static IntPtr FindLgOverlayWindow()
    {
        IntPtr best = IntPtr.Zero;
        long bestArea = 0;
        var virtualScreen = SystemInformation.VirtualScreen;

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            try
            {
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;

                int exStyle = NativeMethods.GetWindowLongW(hWnd, -20);
                if ((exStyle & 0x8) == 0) return true; // WS_EX_TOPMOST

                NativeMethods.GetWindowRect(hWnd, out var rc);
                int w = Math.Max(0, rc.Right - rc.Left);
                int h = Math.Max(0, rc.Bottom - rc.Top);
                if (w < 600 || h < 300) return true; // ignore tiny LG helper popups

                long area = (long)w * h;
                long minCoverArea = (long)virtualScreen.Width * virtualScreen.Height / 3;
                if (area < minCoverArea) return true; // only large screen-cover candidates

                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid == 0) return true;

                string procName;
                try { procName = Process.GetProcessById((int)pid).ProcessName; }
                catch { return true; }
                if (!_knownLgOverlayProcesses.Contains(procName)) return true;

                if (area > bestArea)
                {
                    bestArea = area;
                    best = hWnd;
                }
            }
            catch { }
            return true;
        }, IntPtr.Zero);

        return best;
    }

    static void ApplyLgOverlayNeutralize(IntPtr hWnd, string logPrefix)
    {
        var exStyle = NativeMethods.GetWindowLongW(hWnd, -20);
        var neutralExStyle = exStyle | NativeMethods.WS_EX_LAYERED | NativeMethods.WS_EX_TRANSPARENT;
        NativeMethods.SetWindowLongW(hWnd, -20, neutralExStyle);
        NativeMethods.SetLayeredWindowAttributes(hWnd, 0, 0, NativeMethods.LWA_ALPHA);
        Console.WriteLine($"{logPrefix} -> transparent + click-through");
    }

    static void ApplyLgOverlayFallbackLayout(IntPtr hWnd, string logPrefix)
    {
        try
        {
            NativeMethods.ShowWindow(hWnd, NativeMethods.SW_SHOWMINNOACTIVE);
            Console.WriteLine($"{logPrefix} -> minimize(no-activate)");
            if (!NativeMethods.IsIconic(hWnd))
            {
                NativeMethods.ShowWindow(hWnd, 6); // SW_MINIMIZE
                Console.WriteLine($"{logPrefix} -> force iconic minimize");
            }
        }
        catch { }

        try
        {
            NativeMethods.SetWindowPos(
                hWnd,
                new IntPtr(-2), // HWND_NOTOPMOST
                -32000, -32000, 1, 1,
                0x0010); // SWP_NOACTIVATE
            Console.WriteLine($"{logPrefix} -> offscreen shrink");
        }
        catch { }
    }
}
