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
        // ── Mutex guard: prevent concurrent/duplicate newchat runs ──
        var lockFile = Path.Combine(Path.GetTempPath(), "wkappbot_newchat.lock");
        try
        {
            var lockAge = File.Exists(lockFile) ? DateTime.UtcNow - File.GetLastWriteTimeUtc(lockFile) : TimeSpan.MaxValue;
            if (lockAge.TotalSeconds < 30)
            {
                Console.WriteLine($"[NEWCHAT] SKIPPED — another newchat ran {lockAge.TotalSeconds:F0}s ago (cooldown 30s)");
                return 0;
            }
            File.WriteAllText(lockFile, $"{DateTime.UtcNow:O} pid={Environment.ProcessId}");
        }
        catch { /* best-effort lock */ }

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
            Console.WriteLine("       wkappbot newchat ... --no-policy   (skip EmbeddedInitialPrompt)");
            return 1;
        }

        // ── Append EmbeddedInitialPrompt + regenerate policy file ──
        // Handoff prompt goes first (인수인계 우선), policy appended after.
        // Strip "Startup Confirmation" section — newchat expects handoff response, not "Policy loaded. Ready."
        if (!args.Contains("--no-policy"))
        {
            var policy = AgentPolicy.EmbeddedInitialPrompt;
            const string startupSection = "━━ Startup Confirmation ━━";
            var cutIdx = policy.IndexOf(startupSection, StringComparison.Ordinal);
            if (cutIdx >= 0)
                policy = policy[..cutIdx].TrimEnd();
            policy += """

━━ Session Start (Handoff Mode) ━━
You are receiving a relay handoff from the previous session.

Begin your response with EXACTLY this line (Korean, no changes):
"바통을 이어받았습니다. 이전 작업과 운영 규칙을 숙지하고 중단 없이 달려가겠습니다. 🔄"

Then immediately:
1. Read CLAUDE.md + memory/MEMORY.md
2. Send Slack: wkappbot slack send "바통 인수 완료! 이전 작업 이어갑니다 🔄"
3. Summarize what you understand is pending (Korean, bullet points)
4. Continue work without waiting for confirmation
""";

            text = text + "\n\n---\n" + policy;
            AgentPolicy.RegeneratePolicyFile();
            Console.WriteLine($"[NEWCHAT] Policy injected ({policy.Length} chars) + file regenerated");
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

        // Guard: if input field already has real user input, abort to avoid overwriting.
        // Placeholder text detection:
        //   1. Color: if TextPattern ForegroundColor is NOT white-ish → placeholder (gray) → skip
        //   2. Length: >30 chars or contains newline → real user input → abort
        try
        {
            var existing = editEl.Patterns.Value.IsSupported
                ? editEl.Patterns.Value.Pattern.Value.Value ?? ""
                : "";
            if (existing.Length > 0)
            {
                // Placeholder detection: HelpText often contains the placeholder string in web inputs.
                // If Value == HelpText, it's a placeholder (not real user input).
                bool isPlaceholder = false;
                try
                {
                    var helpText = editEl.Properties.HelpText.ValueOrDefault ?? "";
                    if (!string.IsNullOrEmpty(helpText))
                    {
                        isPlaceholder = string.Equals(existing.Trim(), helpText.Trim(), StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine($"[NEWCHAT] HelpText: \"{helpText}\" → {(isPlaceholder ? "IS placeholder" : "different from value")}");
                    }
                }
                catch { }

                Console.WriteLine($"[NEWCHAT] Input field value: \"{existing.Replace("\n","\\n")}\" ({existing.Length} chars){(isPlaceholder ? " [placeholder]" : "")}");

                if (!isPlaceholder && (existing.Length > 30 || existing.Contains('\n')))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[NEWCHAT] Input field has pending text ({existing.Length} chars) — aborting to avoid overwriting user input");
                    Console.ResetColor();
                    return Error("[NEWCHAT] Input not empty — user may be typing");
                }
            }
        }
        catch { /* ignore — proceed if value check fails */ }

        var prevFg = NativeMethods.GetForegroundWindow();

        // Probe readiness before any keyboard input (AssertReadiness now throws if skipped)
        var readiness = CreateInputReadiness();
        readiness.Probe(new WKAppBot.Win32.Input.InputReadinessRequest
        {
            TargetHwnd = vsHwnd,
            IntendedAction = "key",
            AutoApproveYield = true,
            SkipKnowhow = true,
        });

        // ── Step 1: /clear via keyboard (slash command menu needs keystroke input!) ──
        Console.WriteLine("[NEWCHAT] Using keyboard input for slash command (v2)");
        if (!TypeSlashCommandAndSubmit(editEl, vsHwnd, "/clear"))
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
    /// Type a slash command via keyboard input so the autocomplete menu appears,
    /// then press Enter to select it. SetValue bypasses the menu!
    /// </summary>
    static bool TypeSlashCommandAndSubmit(FlaUI.Core.AutomationElements.AutomationElement editEl, IntPtr hwnd, string command)
    {
        // newchat is an intentional automation command — bypass readiness assertion
        WKAppBot.Win32.Input.InputReadiness.ReadinessCalled = true;
        try
        {
            // Focus the edit element first
            try { editEl.Focus(); Thread.Sleep(100); }
            catch
            {
                NativeMethods.SmartSetForegroundWindow(hwnd);
                Thread.Sleep(300);
            }

            // Clear any existing text
            KeyboardInput.Hotkey(new[] { "ctrl", "a" });
            Thread.Sleep(50);
            KeyboardInput.PressKey("delete");
            Thread.Sleep(100);

            // Type each character via SendInput so the slash command menu triggers
            foreach (var ch in command)
            {
                KeyboardInput.TypeText(ch.ToString());
                Thread.Sleep(50); // delay for menu to react per keystroke
            }
            Console.WriteLine($"[NEWCHAT] Typed '{command}' via keyboard");

            // Wait for autocomplete menu to appear and settle
            Thread.Sleep(800);

            // Press Enter once to select the slash command from menu
            KeyboardInput.PressKey("enter");
            Thread.Sleep(200);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[NEWCHAT] TypeSlashCommand failed: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Set text via clipboard paste, then submit with Enter key.
    /// UIA Value.SetValue doesn't fire DOM input events in Chromium webview
    /// — clipboard paste (Ctrl+V) reliably triggers React/Svelte state updates.
    /// </summary>
    static bool SetValueAndSubmit(FlaUI.Core.AutomationElements.AutomationElement editEl, IntPtr hwnd, string text)
    {
        WKAppBot.Win32.Input.InputReadiness.ReadinessCalled = true;
        try
        {
            // Focus the edit element (required for SendInput)
            try { editEl.Focus(); Thread.Sleep(100); }
            catch
            {
                NativeMethods.SmartSetForegroundWindow(hwnd);
                Thread.Sleep(300);
            }

            // Clear any existing text
            KeyboardInput.Hotkey(new[] { "ctrl", "a" });
            Thread.Sleep(50);
            KeyboardInput.PressKey("delete");
            Thread.Sleep(100);

            // Paste via clipboard (fires DOM input/change events in Chromium!)
            ClaudePromptHelper.SetClipboardTextPublic(text);
            Thread.Sleep(50);
            KeyboardInput.Hotkey(new[] { "ctrl", "v" });
            Console.WriteLine($"[NEWCHAT] Clipboard paste OK ({text.Length} chars)");
            Thread.Sleep(500); // let React/Svelte process the input event

            // Submit with Enter
            KeyboardInput.PressKey("enter");
            Thread.Sleep(200);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[NEWCHAT] SetValueAndSubmit failed: {ex.Message}");
            return false;
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    //  VS Code window finder
    // ═══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Find VS Code window for new chat.
    /// Priority 1: ancestor VS Code process (wkappbot was spawned from its terminal → exact match).
    /// Priority 2: CWD folder match in title — only if exactly ONE window matches (ambiguous = error).
    /// Never falls back to "any VS Code" — wrong window is worse than no window.
    /// </summary>
    static IntPtr FindVSCodeWindowForNewChat()
    {
        // ── Collect all VS Code main windows ──
        var candidates = new List<(IntPtr hWnd, uint pid, string title)>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            if (WindowFinder.GetClassName(hWnd) != "Chrome_WidgetWin_1") return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string procName = "?";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            if (!procName.Equals("Code", StringComparison.OrdinalIgnoreCase)) return true;
            var title = WindowFinder.GetWindowText(hWnd);
            if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) return true;
            candidates.Add((hWnd, pid, title));
            return true;
        }, IntPtr.Zero);

        if (candidates.Count == 0) return IntPtr.Zero;

        // ── Priority 1: ancestor VS Code PID ──
        // Walk parent process chain: wkappbot → shell → ... → Code.exe
        var ancestorCodePids = new HashSet<uint>();
        try
        {
            int checkPid = System.Diagnostics.Process.GetCurrentProcess().Id;
            for (int depth = 0; depth < 10 && checkPid > 0; depth++)
            {
                int parentPid = 0;
                string parentName = "";
                try
                {
                    using var searcher = new System.Management.ManagementObjectSearcher(
                        $"SELECT ParentProcessId, Name FROM Win32_Process WHERE ProcessId={checkPid}");
                    foreach (System.Management.ManagementObject mo in searcher.Get())
                    {
                        parentPid = Convert.ToInt32(mo["ParentProcessId"]);
                        parentName = mo["Name"]?.ToString() ?? "";
                        break;
                    }
                }
                catch { break; }
                if (parentPid == 0) break;
                if (parentName.Equals("Code.exe", StringComparison.OrdinalIgnoreCase))
                    ancestorCodePids.Add((uint)parentPid);
                checkPid = parentPid;
            }
        }
        catch { }

        if (ancestorCodePids.Count > 0)
        {
            var ancestorMatch = candidates.Where(c => ancestorCodePids.Contains(c.pid)).ToList();
            if (ancestorMatch.Count == 1)
            {
                Console.WriteLine($"[NEWCHAT] Ancestor match (PID={ancestorMatch[0].pid}): \"{ancestorMatch[0].title}\"");
                return ancestorMatch[0].hWnd;
            }
            if (ancestorMatch.Count > 1)
            {
                // Multiple windows for same Code process (split editors) — use CWD to disambiguate
                Console.WriteLine($"[NEWCHAT] {ancestorMatch.Count} ancestor windows — disambiguating by CWD...");
                candidates = ancestorMatch; // narrow scope for CWD check below
            }
        }

        // ── Priority 2: CWD folder match — ONLY within ancestor set if known ──
        // Never match a window outside the ancestor set (would target wrong VS Code instance).
        var cwd = Environment.CurrentDirectory;
        var cwdFolder = Path.GetFileName(cwd.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        if (!string.IsNullOrEmpty(cwdFolder))
        {
            // Match " - {folder} - " pattern (VS Code title format) — avoids partial substring hits
            var cwdPattern = $" - {cwdFolder} - ";
            var cwdMatches = candidates
                .Where(c => c.title.Contains(cwdPattern, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Guard: if ancestor PIDs known but match is outside ancestor set → wrong window → fail
            if (cwdMatches.Count == 1 && ancestorCodePids.Count > 0 && !ancestorCodePids.Contains(cwdMatches[0].pid))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[NEWCHAT] ERROR: CWD match \"{cwdFolder}\" → \"{cwdMatches[0].title}\" but PID={cwdMatches[0].pid} is NOT an ancestor — refusing to target wrong window.");
                Console.ResetColor();
                return IntPtr.Zero;
            }

            if (cwdMatches.Count == 1)
            {
                Console.WriteLine($"[NEWCHAT] CWD match \"{cwdFolder}\": \"{cwdMatches[0].title}\"");
                return cwdMatches[0].hWnd;
            }
            if (cwdMatches.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[NEWCHAT] ERROR: {cwdMatches.Count} VS Code windows match CWD \"{cwdFolder}\" — ambiguous, aborting.");
                Console.WriteLine("  Use a more specific title or close duplicate windows.");
                foreach (var m in cwdMatches) Console.WriteLine($"    hwnd=0x{m.hWnd:X} \"{m.title}\"");
                Console.ResetColor();
                return IntPtr.Zero;
            }
        }

        // ── No match → error (never guess) ──
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[NEWCHAT] ERROR: No VS Code window matches CWD \"{cwdFolder}\" (ancestor PID lookup also failed).");
        Console.WriteLine("  Available VS Code windows:");
        foreach (var c in candidates) Console.WriteLine($"    hwnd=0x{c.hWnd:X} pid={c.pid} \"{c.title}\"");
        Console.ResetColor();
        return IntPtr.Zero;
    }
}
