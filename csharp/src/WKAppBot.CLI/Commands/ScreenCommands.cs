using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Fullscreen black overlay form — WS_EX_NOACTIVATE from birth (CreateParams).
/// Never steals focus, never appears in taskbar, never triggers lock screen feel.
/// </summary>
internal sealed class ScreenBlankForm : System.Windows.Forms.Form
{
    protected override System.Windows.Forms.CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x08000000 | 0x00000080; // WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW
            return cp;
        }
    }

    public ScreenBlankForm()
    {
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        BackColor = System.Drawing.Color.Black;
        TopMost = true;
        ShowInTaskbar = false;
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;

        // Cover all monitors (virtual screen)
        int vx = NativeMethods.GetSystemMetrics(76); // SM_XVIRTUALSCREEN
        int vy = NativeMethods.GetSystemMetrics(77); // SM_YVIRTUALSCREEN
        int vw = NativeMethods.GetSystemMetrics(78); // SM_CXVIRTUALSCREEN
        int vh = NativeMethods.GetSystemMetrics(79); // SM_CYVIRTUALSCREEN
        Bounds = new System.Drawing.Rectangle(vx, vy, vw, vh);
    }
}

internal partial class Program
{
    // _screenBlankForm is declared in AppBotEyeGlobalMode.cs (shared partial class)

    static int ScreenCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  wkappbot screen off [--duration N]");
            Console.WriteLine("      Black out all monitors with fullscreen overlay (no power API, no sleep).");
            Console.WriteLine("      --duration N: auto-restore after N seconds (default: 60s safety net)");
            Console.WriteLine("  wkappbot screen on");
            Console.WriteLine("      Remove black overlay immediately.");
            return 0;
        }

        var sub = args[0].ToLowerInvariant();

        if (sub == "on")
        {
            if (_screenBlankForm != null)
            {
                CloseScreenBlank();
                Console.WriteLine("[SCREEN] blank removed");
            }
            else
            {
                Console.WriteLine("[SCREEN] no blank active");
            }
            return 0;
        }

        if (sub != "off")
            return Error($"Unknown screen subcommand: {sub}");

        // Parse --duration N (default 60s safety net — prevents accidental permanent blank)
        int durationSec = 60;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].Equals("--duration", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                int.TryParse(args[++i], out durationSec);
                if (durationSec <= 0) durationSec = 60;
            }
        }

        if (_screenBlankForm != null)
        {
            Console.WriteLine("[SCREEN] blank already active");
            return 0;
        }

        // Create fullscreen black overlay — NO focus steal, NO power API
        var ready = new ManualResetEventSlim(false);
        var thread = new Thread(() =>
        {
            var form = new ScreenBlankForm();

            form.FormClosed += (_, _) => { _screenBlankForm = null; };
            _screenBlankForm = form;

            // Snapshot cursor position — compare later to distinguish mouse vs keyboard
            NativeMethods.GetCursorPos(out var startCursor);
            var startEnv = Environment.TickCount;
            string wakeReason = "timeout";

            // Poll every 200ms: if user input detected OR duration expired → close
            var poll = new System.Windows.Forms.Timer { Interval = 200 };
            poll.Tick += (_, _) =>
            {
                uint idleMs = NativeMethods.GetUserIdleMs();
                bool userInput = idleMs < 500;
                bool expired = (Environment.TickCount - startEnv) >= durationSec * 1000;
                if (userInput || expired)
                {
                    poll.Stop();
                    if (userInput)
                    {
                        NativeMethods.GetCursorPos(out var nowCursor);
                        bool mouseMoved = Math.Abs(nowCursor.X - startCursor.X) > 5 ||
                                          Math.Abs(nowCursor.Y - startCursor.Y) > 5;
                        wakeReason = mouseMoved ? "mouse" : "keyboard";
                    }
                    form.Tag = wakeReason; // pass to FormClosed
                    form.Close();
                }
            };
            // Start polling after grace period (CLI input + Enter keystroke needs time to age out)
            var grace = new System.Windows.Forms.Timer { Interval = 3000 };
            grace.Tick += (_, _) => { grace.Stop(); poll.Start(); };
            grace.Start();

            form.Shown += (_, _) => ready.Set();
            form.Show(); // Show without activation (WS_EX_NOACTIVATE handles this)
            System.Windows.Forms.Application.Run(form);
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = false; // keep process alive until form closes
        thread.Name = "ScreenBlank";
        thread.Start();

        // Wait for form to appear
        ready.Wait(3000);
        Console.WriteLine($"[SCREEN] blank ON — all monitors blacked out (auto-off in {durationSec}s, or move mouse/key to restore)");

        // Wait for form to close (process must stay alive for the overlay thread)
        thread.Join();
        Console.WriteLine("[SCREEN] blank OFF — restored");

        return 0;
    }

    static void CloseScreenBlank()
    {
        var form = _screenBlankForm;
        if (form != null && !form.IsDisposed)
        {
            form.BeginInvoke(() => form.Close());
        }
        _screenBlankForm = null;
    }

    static void StartEyeBackground()
    {
        try
        {
            var exe = Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
                return;

            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = "eye --size 380x280",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process.Start(psi);
        }
        catch { }
    }

    static IntPtr FindEyeWindow()
    {
        IntPtr found = IntPtr.Zero;
        EnumWindows((hWnd, _) =>
        {
            if (!IsWindowVisible(hWnd)) return true;
            var sb = new StringBuilder(512);
            GetWindowTextW(hWnd, sb, sb.Capacity);
            var t = sb.ToString();
            if (t.StartsWith("WK AppBot Global Eye", StringComparison.OrdinalIgnoreCase) ||
                t.StartsWith("WK AppBot Eye", StringComparison.OrdinalIgnoreCase))
            {
                found = hWnd;
                return false;
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }

    static bool TryCaptureWindow(IntPtr hWnd, string outPath, out bool nearBlack, out double brightRatio)
    {
        nearBlack = true;
        brightRatio = 0;
        try
        {
            if (!GetWindowRect(hWnd, out MV_RECT rc)) return false;
            int w = Math.Max(1, rc.Right - rc.Left);
            int h = Math.Max(1, rc.Bottom - rc.Top);

            using var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(rc.Left, rc.Top, 0, 0, new Size(w, h));

            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
            bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);

            int sample = 0;
            int bright = 0;
            int step = Math.Max(1, Math.Min(w, h) / 40);
            for (int y = 0; y < h; y += step)
            {
                for (int x = 0; x < w; x += step)
                {
                    var c = bmp.GetPixel(x, y);
                    sample++;
                    if ((c.R + c.G + c.B) >= 45) bright++;
                }
            }

            brightRatio = sample > 0 ? (double)bright / sample : 0;
            nearBlack = brightRatio < 0.01;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
