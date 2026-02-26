using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: prompt-test — Test Claude prompt input (focusless vs focus-steal)
// Usage: wkappbot prompt-test ["text"] [--dry-run]
// --dry-run: type text but don't submit (no Enter key)
internal partial class Program
{
    static int PromptTestCommand(string[] args)
    {
        string text = "마이크 테스트 1 2 3";
        bool dryRun = false;

        // Parse args
        var textParts = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--dry-run" || args[i] == "--no-submit")
                dryRun = true;
            else
                textParts.Add(args[i]);
        }
        if (textParts.Count > 0)
            text = string.Join(" ", textParts);

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine($"[PROMPT-TEST] Finding Claude prompt...");

        using var helper = new ClaudePromptHelper();
        var prompt = helper.FindPrompt();
        if (prompt == null)
        {
            Console.WriteLine("[PROMPT-TEST] Claude prompt not found!");
            Console.WriteLine("[PROMPT-TEST] Is Claude Desktop or VS Code with Claude running?");
            return 1;
        }

        Console.WriteLine($"[PROMPT-TEST] Found: {prompt.HostType} \"{prompt.WindowTitle}\"");
        Console.WriteLine($"[PROMPT-TEST] Rect: ({prompt.PromptRect.X},{prompt.PromptRect.Y} {prompt.PromptRect.Width}x{prompt.PromptRect.Height})");
        Console.WriteLine($"[PROMPT-TEST] Text: \"{text}\"");
        Console.WriteLine($"[PROMPT-TEST] Mode: {(dryRun ? "DRY-RUN (no Enter)" : "SUBMIT (will press Enter)")}");
        Console.WriteLine();

        if (dryRun)
        {
            // Type without submitting — test text insertion only
            Console.WriteLine("[PROMPT-TEST] Dry-run: typing text without submit...");
            var result = helper.TypeWithoutSubmit(prompt, text);
            Console.WriteLine($"[PROMPT-TEST] Result: {(result ? "OK" : "FAILED")}");
        }
        else
        {
            // Full submit
            Console.WriteLine("[PROMPT-TEST] Submitting...");
            var result = helper.TypeAndSubmit(prompt, text);
            Console.WriteLine($"[PROMPT-TEST] Result: {(result ? "OK" : "FAILED")}");
        }

        Console.WriteLine("[PROMPT-TEST] Done!");
        return 0;
    }
}
