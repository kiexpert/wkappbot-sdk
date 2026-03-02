using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;

namespace WKAppBot.CLI;

// partial class: newchat command — open new Claude Desktop chat and submit prompt (all focusless)
internal partial class Program
{
    /// <summary>
    /// wkappbot newchat "prompt text"
    /// wkappbot newchat --file prompt.txt
    ///
    /// Flow (all focusless via UIA):
    ///   1. Find Claude Desktop window (process=claude, class=Chrome_WidgetWin_1)
    ///   2. Toggle sidebar open (Toggle pattern on sidebar button)
    ///   3. Invoke "새 대화" (Hyperlink, recursive walk)
    ///   4. Toggle sidebar closed
    ///   5. Wait for new chat page to load
    ///   6. Insert text via MSAA put_accValue + submit (ClaudePromptHelper)
    /// </summary>
    /// <summary>Max retries per step. Electron can lag heavily at 90%+ context.</summary>
    const int NewChatMaxRetries = 3;

    static int NewChatCommand(string[] args)
    {
        // ── Parse args ──
        string? text = null;
        var filePath = GetArgValue(args, "--file");

        if (!string.IsNullOrEmpty(filePath))
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ERROR] File not found: {filePath}");
                return 1;
            }
            text = File.ReadAllText(filePath).Trim();
        }
        else if (args.Length > 0 && !args[0].StartsWith("--"))
        {
            text = args[0];
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Usage: wkappbot newchat \"prompt text\"");
            Console.WriteLine("       wkappbot newchat --file prompt.txt");
            return 1;
        }

        Console.WriteLine($"[NEWCHAT] Prompt: {(text.Length > 80 ? text[..77] + "..." : text)} ({text.Length} chars)");

        // ── Step 1: Find Claude Desktop window ──
        var claudeHwnd = FindClaudeDesktopWindow();
        if (claudeHwnd == IntPtr.Zero)
        {
            Console.WriteLine("[ERROR] Claude Desktop window not found");
            return 1;
        }
        Console.WriteLine($"[NEWCHAT] Found Claude: hwnd=0x{claudeHwnd:X}");

        // ── Step 2: Open sidebar (retry with backoff — Electron lag at 90%+ context) ──
        bool sidebarOpened = false;
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT] Opening sidebar... (attempt {attempt}/{NewChatMaxRetries})");
            if (ToggleSidebar(claudeHwnd, open: true))
            {
                sidebarOpened = true;
                break;
            }
            Console.WriteLine($"  [SIDEBAR] Failed, waiting {attempt}s before retry...");
            Thread.Sleep(attempt * 1000);
        }
        if (!sidebarOpened)
        {
            Console.WriteLine("[ERROR] Failed to toggle sidebar after retries");
            return 1;
        }
        Thread.Sleep(1000); // extra wait for sidebar animation under lag

        // ── Step 3: Invoke "새 대화" (retry — UIA walk can timeout under heavy load) ──
        bool invoked = false;
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT] Invoking '새 대화'... (attempt {attempt}/{NewChatMaxRetries})");
            if (QuickInvoke(claudeHwnd, "새 대화") == 0)
            {
                invoked = true;
                break;
            }
            Console.WriteLine($"  [INVOKE] Failed, waiting {attempt * 2}s before retry...");
            Thread.Sleep(attempt * 2000);
        }
        if (!invoked)
        {
            Console.WriteLine("[ERROR] Failed to invoke '새 대화' after retries");
            ToggleSidebar(claudeHwnd, open: false);
            return 1;
        }
        Thread.Sleep(1000);

        // ── Step 4: Close sidebar (focusless Toggle) ──
        ToggleSidebar(claudeHwnd, open: false);

        // ── Step 5: Wait for new chat page to load (longer for laggy Electron) ──
        Console.WriteLine("[NEWCHAT] Waiting for new chat page...");
        Thread.Sleep(3000);

        // ── Step 6: Submit prompt via ClaudePromptHelper (retry) ──
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT] Submitting prompt... (attempt {attempt}/{NewChatMaxRetries})");
            using var helper = new ClaudePromptHelper();
            ClaudePromptHelper.ForceFocusless = true;

            if (helper.TryNewChatInput(claudeHwnd, text))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[NEWCHAT] SUCCESS — prompt submitted to new chat!");
                Console.ResetColor();
                return 0;
            }

            if (attempt < NewChatMaxRetries)
            {
                Console.WriteLine($"  [PROMPT] Failed, waiting {attempt * 2}s before retry...");
                Thread.Sleep(attempt * 2000);
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[NEWCHAT] FAILED — could not submit prompt after retries");
        Console.ResetColor();
        return 1;
    }

    // FindClaudeDesktopWindow is defined in AppBotEyeClaudeDetector.cs (same partial class)

    /// <summary>
    /// Toggle Claude Desktop sidebar using UIA Toggle pattern at (60,22) relative to window.
    /// Returns true if toggle was performed.
    /// </summary>
    static bool ToggleSidebar(IntPtr hwnd, bool open)
    {
        try
        {
            using var a = new UIA3Automation();
            a.ConnectionTimeout = TimeSpan.FromSeconds(5);
            a.TransactionTimeout = TimeSpan.FromSeconds(5);
            var root = a.FromHandle(hwnd);
            if (root == null) return false;

            NativeMethods.GetWindowRect(hwnd, out var rect);
            int absX = rect.Left + 60;
            int absY = rect.Top + 22;

            var el = root.Automation.FromPoint(new System.Drawing.Point(absX, absY));
            if (el == null) return false;

            // Check current state
            bool hasToggle = false;
            try { hasToggle = el.Patterns.Toggle.IsSupported; } catch { }

            if (hasToggle)
            {
                try
                {
                    var state = el.Patterns.Toggle.Pattern.ToggleState.Value;
                    bool isOpen = state == FlaUI.Core.Definitions.ToggleState.On;

                    if ((open && !isOpen) || (!open && isOpen))
                    {
                        el.Patterns.Toggle.Pattern.Toggle();
                        Console.WriteLine($"  [SIDEBAR] Toggled {(open ? "open" : "closed")} (focusless)");
                    }
                    else
                    {
                        Console.WriteLine($"  [SIDEBAR] Already {(open ? "open" : "closed")}");
                    }
                    return true;
                }
                catch { }
            }

            // Fallback: just toggle without state check
            try
            {
                el.Patterns.Toggle.Pattern.Toggle();
                return true;
            }
            catch { return false; }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [SIDEBAR] Error: {ex.Message}");
            return false;
        }
    }
}
