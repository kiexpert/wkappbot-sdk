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
        bool staged = false;
        int retry = 3;

        // Parse args
        var textParts = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--dry-run" || args[i] == "--no-submit")
                dryRun = true;
            else if (args[i] == "--staged")
                staged = true;
            else if (args[i] == "--retry" && i + 1 < args.Length && int.TryParse(args[i + 1], out var r))
            {
                retry = Math.Max(1, Math.Min(10, r));
                i++;
            }
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
        Console.WriteLine($"[PROMPT-TEST] Mode: {(staged ? $"STAGED (retry={retry})" : dryRun ? "DRY-RUN (no Enter)" : "SUBMIT (will press Enter)")}");
        Console.WriteLine();

        if (staged)
        {
            for (int attempt = 1; attempt <= retry; attempt++)
            {
                Console.WriteLine($"[PROMPT-TEST][STAGED] attempt {attempt}/{retry}");

                var typed = helper.TypeWithoutSubmit(prompt, text);
                if (!typed)
                {
                    Console.WriteLine("[WARN] stage=type result=failed");
                    continue;
                }

                var before = helper.ProbeSubmitState(prompt);
                Console.WriteLine($"[PROMPT-TEST][STAGED] before turnForm={before.TurnFormFound} submit={before.SubmitFound} enabled={before.SubmitEnabled} name=\"{before.SubmitName}\"");
                var isCodexPrompt = string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
                if ((!before.TurnFormFound || !before.SubmitFound || !before.SubmitEnabled) && !isCodexPrompt)
                {
                    Console.WriteLine("[WARN] stage=pre-submit-check result=failed");
                    continue;
                }
                if (!before.TurnFormFound || !before.SubmitFound || !before.SubmitEnabled)
                    Console.WriteLine("[WARN] stage=pre-submit-check limited for codex-desktop");

                var prevAllowFocusSteal = ClaudePromptHelper.AllowFocusSteal;
                try
                {
                    ClaudePromptHelper.AllowFocusSteal = attempt > 1; // temporary focus allowed on retry
                    var submitted = helper.SubmitExistingInput(prompt);
                    var accepted = submitted && (isCodexPrompt || helper.VerifySubmitAccepted(prompt, 2000));
                    var after = helper.ProbeSubmitState(prompt);

                    Console.WriteLine($"[PROMPT-TEST][STAGED] submit submitted={submitted} accepted={accepted}");
                    Console.WriteLine($"[PROMPT-TEST][STAGED] after turnForm={after.TurnFormFound} submit={after.SubmitFound} enabled={after.SubmitEnabled} name=\"{after.SubmitName}\"");

                    if (accepted)
                    {
                        Console.WriteLine("[PROMPT-TEST][STAGED] Result: OK");
                        Console.WriteLine("[PROMPT-TEST] Done!");
                        return 0;
                    }

                    Console.WriteLine("[WARN] stage=post-submit-check result=failed");
                }
                finally
                {
                    ClaudePromptHelper.AllowFocusSteal = prevAllowFocusSteal;
                }
            }

            Console.WriteLine("[PROMPT-TEST][STAGED] Result: FAILED");
            return 2;
        }

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
