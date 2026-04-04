using System.Text;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    private static string GetFocusRiskDumpPath()
    {
        var baseDir = Path.GetDirectoryName(Environment.ProcessPath ?? AppContext.BaseDirectory) ?? AppContext.BaseDirectory;
        var logDir = Path.Combine(baseDir, "wkappbot.hq", "logs");
        Directory.CreateDirectory(logDir);
        return Path.Combine(logDir, "cdp-focus-risk.jsonl");
    }

    private static bool IsFocusRiskMethod(string method)
    {
        return method is "Page.bringToFront" or "Target.activateTarget" or "Browser.setWindowBounds" or
               "Input.dispatchMouseEvent" or "Input.dispatchKeyEvent" or "Input.insertText" or "Runtime.evaluate";
    }

    private static bool IsFocusRiskExpression(string? expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) return false;
        string[] needles =
        [
            "window.focus(",
            ".focus(",
            ".click(",
            "showOpenFilePicker",
            "scrollIntoView(",
            "requestSubmit(",
            "dispatchEvent(new KeyboardEvent",
            "dispatchEvent(new MouseEvent"
        ];
        return needles.Any(n => expression.Contains(n, StringComparison.OrdinalIgnoreCase));
    }

    private static string TruncateForLog(string? text, int max = 240)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= max ? text : text[..max] + "...";
    }

    private static string SerializeParams(JsonObject? parameters)
    {
        try
        {
            if (parameters == null) return "";
            return TruncateForLog(parameters.ToJsonString());
        }
        catch
        {
            return "";
        }
    }

    private async Task LogFocusRiskAsync(
        string phase,
        string method,
        JsonObject? parameters,
        nint prevFg = 0,
        nint curFg = 0,
        string? note = null)
    {
        try
        {
            string expr = "";
            if (method == "Runtime.evaluate")
                expr = parameters?["expression"]?.GetValue<string>() ?? "";

            var payload = new JsonObject
            {
                ["ts"] = DateTimeOffset.Now.ToString("O"),
                ["phase"] = phase,
                ["method"] = method,
                ["targetId"] = TargetId ?? "",
                ["ws"] = WebSocketUrl ?? "",
                ["op"] = OperationContext ?? "",
                ["chromePid"] = ChromePid,
                ["chromeHwnd"] = $"0x{ChromeWindowHandle:X8}",
                ["prevFg"] = prevFg == 0 ? "" : $"0x{prevFg:X8}",
                ["curFg"] = curFg == 0 ? "" : $"0x{curFg:X8}",
                ["iconic"] = ChromeWindowHandle != 0 && IsIconic((IntPtr)ChromeWindowHandle),
                ["note"] = note ?? "",
                ["params"] = SerializeParams(parameters),
                ["expr"] = TruncateForLog(expr),
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

            await File.AppendAllTextAsync(GetFocusRiskDumpPath(), payload.ToJsonString() + Environment.NewLine, Encoding.UTF8);
            Console.Error.WriteLine($"[CDP:FOCUS-RISK] {phase} {method} op={OperationContext ?? "-"} note={note ?? "-"}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CDP:FOCUS-RISK] dump failed: {ex.Message}");
        }
    }

    private async Task MaybeLogFocusRiskBeforeAsync(string method, JsonObject? parameters, nint prevFg)
    {
        if (!IsFocusRiskMethod(method)) return;
        if (method == "Runtime.evaluate")
        {
            var expr = parameters?["expression"]?.GetValue<string>() ?? "";
            if (!IsFocusRiskExpression(expr)) return;
        }
        await LogFocusRiskAsync("before", method, parameters, prevFg: prevFg);
    }
}
