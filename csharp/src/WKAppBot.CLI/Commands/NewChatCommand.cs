using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Accessibility;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;

namespace WKAppBot.CLI;

// partial class: newchat command — /clear + prompt injection for VS Code Claude Code
internal partial class Program
{
    /// <summary>
    /// wkappbot newchat "prompt text"
    /// wkappbot newchat --file prompt.txt
    ///
    /// VS Code Claude Code flow (a11y pipeline):
    ///   1. Find VS Code window (CWD match preferred)
    ///   2. UIA: find [Edit] "Message input" via grap #scope (depth 25)
    ///   3. Value.SetValue("/clear") → submit via focus + Enter
    ///   4. Wait 3 seconds for clear to complete
    ///   5. Value.SetValue(prompt) → submit via focus + Enter
    ///   6. Restore previous foreground window
    /// </summary>

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
        else
        {
            // First non-flag arg is the prompt text
            text = args.FirstOrDefault(a => !a.StartsWith("--"));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Usage: wkappbot newchat \"prompt text\"");
            Console.WriteLine("       wkappbot newchat --file prompt.txt");
            return 1;
        }

        Console.WriteLine($"[NEWCHAT] Prompt: {(text.Length > 80 ? text[..77] + "..." : text)} ({text.Length} chars)");

        // ── Find VS Code window ──
        var vsHwnd = FindVSCodeWindowForNewChat();
        if (vsHwnd == IntPtr.Zero)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] VS Code window not found");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine($"[NEWCHAT] Target: VS Code hwnd=0x{vsHwnd:X}");

        // ── Find [Edit] "Message input" via UIA ──
        using var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(10);
        automation.TransactionTimeout = TimeSpan.FromSeconds(10);
        var root = automation.FromHandle(vsHwnd);
        if (root == null) return Error("[NEWCHAT] UIA root not found");

        // Search specifically for [Edit] "Message input" — must filter by ControlType
        // because chat text may contain "Message input" as substring in Text elements
        var editEl = GrapHelper.WalkTree(root, maxDepth: 25, el =>
        {
            try
            {
                if (el.Properties.ControlType.ValueOrDefault != ControlType.Edit) return false;
                var name = el.Properties.Name.ValueOrDefault ?? "";
                return name == "Message input";
            }
            catch { return false; }
        });
        if (editEl == null) return Error("[NEWCHAT] [Edit] 'Message input' not found — Claude Code panel open?");

        Console.WriteLine($"[NEWCHAT] Found: [Edit] \"{editEl.Name}\" at ({editEl.BoundingRectangle.X},{editEl.BoundingRectangle.Y})");

        var prevFg = NativeMethods.GetForegroundWindow();

        // ── Step 1: /clear + Enter ──
        if (!SetValueAndSubmit(editEl, vsHwnd, "/clear"))
            return Error("[NEWCHAT] Failed to send /clear");
        Console.WriteLine("[NEWCHAT] /clear submitted — waiting 3s for reset...");
        Thread.Sleep(3000);

        // ── Step 2: Re-find edit (DOM may have changed after /clear) ──
        root = automation.FromHandle(vsHwnd);
        editEl = root != null ? GrapHelper.WalkTree(root, maxDepth: 25, el =>
        {
            try
            {
                if (el.Properties.ControlType.ValueOrDefault != ControlType.Edit) return false;
                return (el.Properties.Name.ValueOrDefault ?? "") == "Message input";
            }
            catch { return false; }
        }) : null;
        if (editEl == null) return Error("[NEWCHAT] Edit element lost after /clear");

        // ── Step 3: Paste prompt + submit ──
        if (!SetValueAndSubmit(editEl, vsHwnd, text))
            return Error("[NEWCHAT] Failed to send prompt");

        // ── Step 4: Restore focus ──
        if (prevFg != IntPtr.Zero && prevFg != vsHwnd)
        {
            Thread.Sleep(500);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("[NEWCHAT] Focus restored");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[NEWCHAT] SUCCESS — /clear + prompt submitted!");
        Console.ResetColor();
        return 0;
    }

    /// <summary>
    /// Set text via UIA Value pattern, then submit with Enter key.
    /// For Chromium webview Edit: SetValue sets the text, then we need
    /// focus + Enter to trigger submission.
    /// </summary>
    static bool SetValueAndSubmit(FlaUI.Core.AutomationElements.AutomationElement editEl, IntPtr hwnd, string text)
    {
        // Set text via UIA Value pattern (focusless!)
        try
        {
            var vp = editEl.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine($"[NEWCHAT] Value.SetValue OK ({text.Length} chars)");
            }
            else
            {
                Console.WriteLine("[NEWCHAT] Value pattern not writable, trying clipboard...");
                NativeMethods.SmartSetForegroundWindow(hwnd);
                Thread.Sleep(200);
                ClaudePromptHelper.SetClipboardTextPublic(text);
                KeyboardInput.Hotkey(new[] { "ctrl", "v" });
                Thread.Sleep(300);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[NEWCHAT] SetValue failed: {ex.Message}");
            return false;
        }

        // Submit: focus element + Enter
        // (Enter requires focus — acceptable for deliberate newchat operation)
        try
        {
            editEl.Focus();
            Thread.Sleep(100);
        }
        catch
        {
            // Focus failed — bring window to foreground as fallback
            NativeMethods.SmartSetForegroundWindow(hwnd);
            Thread.Sleep(300);
        }

        KeyboardInput.PressKey("enter");
        Thread.Sleep(200);
        return true;
    }

    // ═══════════════════════════════════════════════════════════════════
    //  VS Code window finder
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
            Console.WriteLine($"[NEWCHAT] CWD match: \"{match.title}\"");
            return match.hWnd;
        }

        // Any VS Code
        if (candidates.Count > 0)
        {
            Console.WriteLine($"[NEWCHAT] No CWD match, using: \"{candidates[0].title}\"");
            return candidates[0].hWnd;
        }

        return IntPtr.Zero;
    }
}
