using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int TelegramCommand(string[] args)
    {
        if (args.Length == 0) return TelegramUsage();

        var sub = args[0].ToLowerInvariant();
        var rest = args.Skip(1).ToArray();

        return sub switch
        {
            "send" => TelegramSend(rest),
            _ => TelegramUsage()
        };
    }

    static int TelegramUsage()
    {
        Console.WriteLine(@"Usage: wkappbot telegram send ""text"" [--window WkQuant] [--no-fallback-enter]

Options:
  --window <title>       Window title contains text (default: WkQuant)
  --no-fallback-enter    UIA 전송 실패 시 Enter 폴백 비활성화

Examples:
  wkappbot telegram send ""분석완료!""
  wkappbot telegram send ""테스트"" --window WkQuant
");
        return 1;
    }

    static int TelegramSend(string[] rest)
    {
        if (rest.Length == 0)
            return Error("Usage: wkappbot telegram send \"text\" [--window WkQuant] [--no-fallback-enter]");

        string windowHint = "WkQuant";
        bool fallbackEnter = true;

        var textParts = new List<string>();
        for (int i = 0; i < rest.Length; i++)
        {
            if (rest[i] == "--window" && i + 1 < rest.Length)
            {
                windowHint = rest[++i];
            }
            else if (rest[i] == "--no-fallback-enter")
            {
                fallbackEnter = false;
            }
            else
            {
                textParts.Add(rest[i]);
            }
        }

        var text = string.Join(" ", textParts).Trim();
        if (string.IsNullOrWhiteSpace(text))
            return Error("text is empty");

        Console.WriteLine($"[TELEGRAM] target window hint: {windowHint}");

        using var automation = new UIA3Automation();
        var desktop = automation.GetDesktop();

        var windows = desktop.FindAllChildren(cf => cf.ByControlType(ControlType.Window));
        var targetWindow = windows.FirstOrDefault(w =>
            !string.IsNullOrWhiteSpace(w.Name) &&
            w.Name.Contains(windowHint, StringComparison.OrdinalIgnoreCase));

        if (targetWindow == null)
            return Error($"Telegram window not found (hint: {windowHint})");

        var hwnd = targetWindow.Properties.NativeWindowHandle.TryGetValue(out var nwh) ? (IntPtr)nwh : IntPtr.Zero;
        if (hwnd != IntPtr.Zero)
            NativeMethods.SmartSetForegroundWindow(hwnd);

        Thread.Sleep(150);

        // Pick compose input edit: prefer name containing 메시지 작성
        var edits = targetWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit));
        var compose = edits.FirstOrDefault(e =>
            (e.Name ?? "").Contains("메시지 작성", StringComparison.OrdinalIgnoreCase))
            ?? edits.LastOrDefault();

        if (compose == null)
            return Error("compose edit not found");

        bool typed = false;
        try
        {
            if (compose.Patterns.Value.TryGetPattern(out var vp))
            {
                vp.SetValue(text);
                typed = true;
                Console.WriteLine("[TELEGRAM] UIA ValuePattern.SetValue OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TELEGRAM] UIA SetValue failed: {ex.Message}");
        }

        if (!typed)
        {
            // Minimal fallback for typing (still not sending yet)
            compose.Focus();
            Thread.Sleep(80);
            KeyboardInput.TypeText(text);
            typed = true;
            Console.WriteLine("[TELEGRAM] fallback type via SendInput OK");
        }

        // Accessibility-first send: try invoke a nearby button on the right of compose box.
        bool sent = false;
        try
        {
            var cbox = compose.BoundingRectangle;
            var buttons = targetWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));

            var sendCandidate = buttons
                .Where(b => b.IsEnabled)
                .Select(b => new { Btn = b, R = b.BoundingRectangle })
                .Where(x => x.R.Left > cbox.Right - 20 && Math.Abs(((x.R.Top + x.R.Bottom) / 2.0) - ((cbox.Top + cbox.Bottom) / 2.0)) < 70)
                .OrderBy(x => x.R.Left)
                .Select(x => x.Btn)
                .FirstOrDefault();

            if (sendCandidate != null && sendCandidate.Patterns.Invoke.TryGetPattern(out var inv))
            {
                inv.Invoke();
                sent = true;
                Console.WriteLine("[TELEGRAM] UIA Invoke send button OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TELEGRAM] UIA send invoke failed: {ex.Message}");
        }

        if (!sent && fallbackEnter)
        {
            compose.Focus();
            Thread.Sleep(80);
            KeyboardInput.PressKey("enter");
            sent = true;
            Console.WriteLine("[TELEGRAM] fallback Enter send OK");
        }

        if (!sent)
            return Error("send failed (UIA send failed and fallback disabled)");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[TELEGRAM] sent: \"{text}\"");
        Console.ResetColor();
        return 0;
    }
}
