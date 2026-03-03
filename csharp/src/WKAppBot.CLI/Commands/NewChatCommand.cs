using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Input;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace WKAppBot.CLI;

// partial class: newchat command — open new chat in Claude Desktop OR VS Code Claude Code
internal partial class Program
{
    /// <summary>
    /// wkappbot newchat "prompt text"
    /// wkappbot newchat --file prompt.txt
    /// wkappbot newchat --file prompt.txt --vscode   (force VS Code)
    /// wkappbot newchat --file prompt.txt --desktop   (force Claude Desktop)
    ///
    /// Auto-detection priority:
    ///   1. VS Code with Claude Code extension (UIA Invoke "New Chat" — focusless!)
    ///   2. Claude Desktop (sidebar toggle + "새 대화" — focusless!)
    ///
    /// VS Code flow:
    ///   1. Find VS Code window (Chrome_WidgetWin_1, process=Code)
    ///   2. UIA recursive walk → find "New Chat (Ctrl+N)" button → Invoke (focusless!)
    ///   3. Wait for new chat to load
    ///   4. Input prompt: Escape (focus input) → clipboard paste → Enter (needs focus)
    ///
    /// Claude Desktop flow:
    ///   1. Find Claude Desktop window (process=claude, class=Chrome_WidgetWin_1)
    ///   2. Toggle sidebar open → Invoke "새 대화" → Toggle sidebar closed (all focusless!)
    ///   3. Wait for new chat page to load
    ///   4. Insert text via MSAA put_accValue + submit (focusless!)
    /// </summary>
    const int NewChatMaxRetries = 3;

    static int NewChatCommand(string[] args)
    {
        // ── Parse args ──
        string? text = null;
        var filePath = GetArgValue(args, "--file");
        bool forceVSCode = args.Contains("--vscode");
        bool forceDesktop = args.Contains("--desktop");

        if (!string.IsNullOrEmpty(filePath))
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ERROR] File not found: {filePath}");
                return 1;
            }
            text = File.ReadAllText(filePath).Trim();
        }
        else
        {
            // First non-flag arg is the prompt text
            text = args.FirstOrDefault(a => !a.StartsWith("--"));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Usage: wkappbot newchat \"prompt text\"");
            Console.WriteLine("       wkappbot newchat --file prompt.txt");
            Console.WriteLine("       --vscode   Force VS Code target");
            Console.WriteLine("       --desktop  Force Claude Desktop target");
            return 1;
        }

        Console.WriteLine($"[NEWCHAT] Prompt: {(text.Length > 80 ? text[..77] + "..." : text)} ({text.Length} chars)");

        // ── Auto-detect target ──
        if (!forceDesktop)
        {
            // Try VS Code first
            var vsHwnd = FindVSCodeWindowForNewChat();
            if (vsHwnd != IntPtr.Zero)
            {
                Console.WriteLine($"[NEWCHAT] Target: VS Code hwnd=0x{vsHwnd:X}");
                return NewChatVSCode(vsHwnd, text);
            }
            if (forceVSCode)
            {
                Console.WriteLine("[ERROR] VS Code window not found (--vscode forced)");
                return 1;
            }
        }

        if (!forceVSCode)
        {
            // Fall back to Claude Desktop
            var claudeHwnd = FindClaudeDesktopWindow();
            if (claudeHwnd != IntPtr.Zero)
            {
                Console.WriteLine($"[NEWCHAT] Target: Claude Desktop hwnd=0x{claudeHwnd:X}");
                return NewChatClaudeDesktop(claudeHwnd, text);
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ERROR] No VS Code or Claude Desktop window found");
        Console.ResetColor();
        return 1;
    }

    // ═══════════════════════════════════════════════════════════════════
    //  VS Code Claude Code — New Chat via UIA Invoke (focusless!)
    // ═══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Find VS Code window suitable for new chat.
    /// Priority: window with current CWD folder in title, then any VS Code.
    /// </summary>
    static IntPtr FindVSCodeWindowForNewChat()
    {
        var cwd = Environment.CurrentDirectory;
        var cwdFolder = Path.GetFileName(cwd) ?? "";
        var candidates = new List<(IntPtr hWnd, string title, bool cwdMatch)>();

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls != "Chrome_WidgetWin_1") return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string procName = "?";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            if (!procName.Equals("Code", StringComparison.OrdinalIgnoreCase)) return true;

            var title = WindowFinder.GetWindowText(hWnd);
            if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) return true;

            bool cwdMatch = !string.IsNullOrEmpty(cwdFolder) &&
                title.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase);
            candidates.Add((hWnd, title, cwdMatch));
            return true;
        }, IntPtr.Zero);

        // Prefer CWD match
        var match = candidates.FirstOrDefault(c => c.cwdMatch);
        if (match.hWnd != IntPtr.Zero)
        {
            Console.WriteLine($"[NEWCHAT:VSCODE] CWD match: \"{match.title}\"");
            return match.hWnd;
        }

        // Any VS Code
        if (candidates.Count > 0)
        {
            Console.WriteLine($"[NEWCHAT:VSCODE] No CWD match, using: \"{candidates[0].title}\"");
            return candidates[0].hWnd;
        }

        return IntPtr.Zero;
    }

    /// <summary>
    /// Open new chat in VS Code Claude Code:
    ///   1. SetForegroundWindow → bring VS Code to front
    ///   2. Ctrl+Esc → focus Claude Code input area (extension keybinding)
    ///   3. Ctrl+N → open new chat (extension keybinding when panel focused)
    ///   4. Wait for new chat to load
    ///   5. Paste prompt → Enter → restore focus
    ///
    /// Note: VS Code Claude Code UIA tree is too deep (10+ levels) for reliable
    /// button discovery. Keyboard shortcuts are more reliable for all steps.
    /// </summary>
    static int NewChatVSCode(IntPtr hwnd, string text)
    {
        var prevFg = NativeMethods.GetForegroundWindow();

        // ── Step 1: Bring VS Code to foreground ──
        Console.WriteLine("[NEWCHAT:VSCODE] Activating VS Code...");
        NativeMethods.SmartSetForegroundWindow(hwnd);
        Thread.Sleep(300);

        // ── Step 2: Focus Claude Code input (Ctrl+Esc) ──
        Console.WriteLine("[NEWCHAT:VSCODE] Focusing Claude Code input (Ctrl+Esc)...");
        KeyboardInput.Hotkey(new[] { "ctrl", "escape" });
        Thread.Sleep(500);

        // ── Step 3: New Chat (Ctrl+N) ──
        Console.WriteLine("[NEWCHAT:VSCODE] Opening new chat (Ctrl+N)...");
        KeyboardInput.Hotkey(new[] { "ctrl", "n" });
        Thread.Sleep(1500); // Wait for new chat to initialize

        // ── Step 4: Paste prompt + submit ──
        Console.WriteLine($"[NEWCHAT:VSCODE] Pasting prompt ({text.Length} chars)...");
        ClaudePromptHelper.SetClipboardTextPublic(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(300);

        KeyboardInput.PressKey("enter");
        Console.WriteLine("[NEWCHAT:VSCODE] Submitted (Enter)");

        // ── Step 5: Restore previous foreground ──
        if (prevFg != IntPtr.Zero && prevFg != hwnd)
        {
            Thread.Sleep(500);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("[NEWCHAT:VSCODE] Focus restored");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[NEWCHAT:VSCODE] SUCCESS — prompt submitted to new chat!");
        Console.ResetColor();
        return 0;
    }

    // ═══════════════════════════════════════════════════════════════════
    //  Claude Desktop — New Chat via sidebar toggle + "새 대화" invoke
    // ═══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Open new chat in Claude Desktop (existing flow, all focusless).
    /// </summary>
    static int NewChatClaudeDesktop(IntPtr claudeHwnd, string text)
    {
        // ── Step 2: Open sidebar (retry with backoff — Electron lag at 90%+ context) ──
        bool sidebarOpened = false;
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT:DESKTOP] Opening sidebar... (attempt {attempt}/{NewChatMaxRetries})");
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
        Thread.Sleep(1000);

        // ── Step 3: Invoke "새 대화" ──
        bool invoked = false;
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT:DESKTOP] Invoking '새 대화'... (attempt {attempt}/{NewChatMaxRetries})");
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

        // ── Step 4: Close sidebar ──
        ToggleSidebar(claudeHwnd, open: false);

        // ── Step 5: Wait for new chat page ──
        Console.WriteLine("[NEWCHAT:DESKTOP] Waiting for new chat page...");
        Thread.Sleep(3000);

        // ── Step 6: Submit prompt ──
        for (int attempt = 1; attempt <= NewChatMaxRetries; attempt++)
        {
            Console.WriteLine($"[NEWCHAT:DESKTOP] Submitting prompt... (attempt {attempt}/{NewChatMaxRetries})");
            using var helper = new ClaudePromptHelper();
            ClaudePromptHelper.AllowFocusSteal = false;

            if (helper.TryNewChatInput(claudeHwnd, text))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[NEWCHAT:DESKTOP] SUCCESS — prompt submitted to new chat!");
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
        Console.WriteLine("[NEWCHAT:DESKTOP] FAILED — could not submit prompt after retries");
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
