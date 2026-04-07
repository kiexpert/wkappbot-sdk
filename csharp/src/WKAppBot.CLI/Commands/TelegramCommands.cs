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

        Console.Error.WriteLine($"[TELEGRAM] target window hint: {windowHint}");

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

        // Pick compose input edit: prefer placeholder name, then bottom-most wide edit.
        var edits = targetWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit)).ToList();
        var composeCandidates = edits
            .Where(e => e.BoundingRectangle.Width > 200)
            .OrderByDescending(e => e.BoundingRectangle.Bottom)
            .ThenByDescending(e => e.BoundingRectangle.Width)
            .ToList();

        var compose = composeCandidates.FirstOrDefault(e =>
            (e.Name ?? "").Contains("메시지 작성", StringComparison.OrdinalIgnoreCase))
            ?? composeCandidates.FirstOrDefault()
            ?? edits.LastOrDefault();

        if (compose == null)
            return Error("compose edit not found");

        var cbox = compose.BoundingRectangle;
        Console.Error.WriteLine($"[TELEGRAM] compose rect=({cbox.Left:0},{cbox.Top:0},{cbox.Width:0}x{cbox.Height:0}) name='{compose.Name}'");

        var before = ReadElementValue(compose);

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
            Console.Error.WriteLine($"[TELEGRAM] UIA SetValue failed: {ex.Message}");
        }

        if (!typed)
        {
            compose.Focus();
            Thread.Sleep(80);
            KeyboardInput.TypeText(text);
            typed = true;
            Console.WriteLine("[TELEGRAM] fallback type via SendInput OK");
        }

        var afterType = ReadElementValue(compose);
        var typedVerified = !string.Equals(Norm(afterType), Norm(before), StringComparison.Ordinal);
        Console.Error.WriteLine($"[TELEGRAM] typed-verify={(typedVerified ? "ok" : "weak")} before='{TrimForLog(before)}' after='{TrimForLog(afterType)}'");

        bool sent = false;

        // Accessibility-first send: invoke right-side nearby button, then verify input changed.
        try
        {
            var buttons = targetWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            var sendCandidates = buttons
                .Where(b => b.IsEnabled)
                .Select(b => new { Btn = b, R = b.BoundingRectangle })
                .Where(x => x.R.Left > cbox.Right - 30 && Math.Abs(((x.R.Top + x.R.Bottom) / 2.0) - ((cbox.Top + cbox.Bottom) / 2.0)) < 80)
                .OrderByDescending(x => x.R.Left)
                .Select(x => x.Btn)
                .ToList();

            foreach (var sendCandidate in sendCandidates)
            {
                if (!sendCandidate.Patterns.Invoke.TryGetPattern(out var inv))
                    continue;

                inv.Invoke();
                Console.WriteLine("[TELEGRAM] UIA Invoke candidate clicked");
                Thread.Sleep(180);

                var afterSend = ReadElementValue(compose);
                // Verification only when value can be observed. If not observable, keep trying and fallback to Enter.
                if (typedVerified && (string.IsNullOrWhiteSpace(afterSend) || !string.Equals(Norm(afterSend), Norm(afterType), StringComparison.Ordinal)))
                {
                    sent = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[TELEGRAM] UIA send invoke failed: {ex.Message}");
        }

        if (!sent && !typedVerified)
            Console.WriteLine("[TELEGRAM] value verification unavailable; using Enter fallback for reliable send");

        if (!sent && fallbackEnter)
        {
            compose.Focus();
            Thread.Sleep(80);
            KeyboardInput.PressKey("enter");
            Thread.Sleep(150);
            sent = true;
            Console.WriteLine("[TELEGRAM] fallback Enter send OK");
        }

        if (!sent)
            return Error("send failed (UIA send failed and fallback disabled)");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[TELEGRAM] sent: \"{text}\"");
        Console.ResetColor();
        return 0;
    }

    static string? ReadElementValue(FlaUI.Core.AutomationElements.AutomationElement el)
    {
        try
        {
            if (el.Patterns.Value.TryGetPattern(out var vp))
                return vp.Value;
        }
        catch { }

        try { return el.Name; } catch { }
        return null;
    }

    static string Norm(string? s) => (s ?? "").Trim().Replace("\r", "").Replace("\n", "");

    static string TrimForLog(string? s)
    {
        var t = Norm(s);
        if (t.Length > 40) t = t[..40] + "...";
        return t;
    }
}
