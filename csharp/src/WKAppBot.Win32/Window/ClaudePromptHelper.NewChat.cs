using System.Drawing;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Fallback input for new-chat landing page (no turn-form yet).
    /// This starts a new conversation, after which turn-form becomes available.
    /// Returns true if text was submitted.
    /// </summary>
    public bool TryNewChatInput(IntPtr claudeHwnd, string text)
    {
        if (claudeHwnd == IntPtr.Zero || !NativeMethods.IsWindow(claudeHwnd)) return false;

        Console.WriteLine("  [PROMPT] NEW-CHAT FALLBACK: attempting paste on Claude Desktop...");

        // ★ Dump UIA tree at this point for analysis (always, even if fallback succeeds)
        try
        {
            var root = _automation.FromHandle(claudeHwnd);
            if (root != null)
            {
                var dumpSb = new System.Text.StringBuilder();
                var title = WindowFinder.GetWindowText(claudeHwnd);
                dumpSb.AppendLine($"=== New-Chat UIA Dump: 0x{claudeHwnd:X} \"{title}\" @ {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
                DumpUiaTree(root, dumpSb, 0, maxDepth: 10);
                var dumpDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".claude", "projects");
                try { Directory.CreateDirectory(dumpDir); } catch { }
                var dumpPath = Path.Combine(dumpDir, $"newchat_uia_dump_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                File.WriteAllText(dumpPath, dumpSb.ToString());
                Console.WriteLine($"  [PROMPT] UIA dump saved: {dumpPath}");
            }
        }
        catch (Exception dumpEx) { Console.WriteLine($"  [PROMPT] UIA dump failed: {dumpEx.Message}"); }

        // First, try to find turn-form with retries (page might still be loading)
        for (int retry = 0; retry < 3; retry++)
        {
            if (retry > 0)
            {
                Console.WriteLine($"  [PROMPT] Retry {retry}/3: waiting 1s for turn-form...");
                Thread.Sleep(1000);
            }
            try
            {
                var root = _automation.FromHandle(claudeHwnd);
                if (root != null)
                {
                    var turnForm = root.FindFirstDescendant(
                        new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                    if (turnForm != null)
                    {
                        Console.WriteLine($"  [PROMPT] turn-form appeared after retry {retry}!");
                        var inputGroup = turnForm.FindFirstChild(
                            new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
                        if (inputGroup == null)
                        {
                            var children = turnForm.FindAllChildren();
                            inputGroup = children.FirstOrDefault(c =>
                                c.ControlType == ControlType.Group && c.BoundingRectangle.Width > 100);
                        }
                        if (inputGroup != null)
                        {
                            var rect = inputGroup.BoundingRectangle;
                            var title = WindowFinder.GetWindowText(claudeHwnd);
                            var prompt = new PromptInfo(claudeHwnd, title, "claude",
                                new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), "claude-desktop");
                            return TypeAndSubmit(prompt, text);
                        }
                    }
                }
            }
            catch { }
        }

        // turn-form still not found after retries → brute-force paste (no click!)
        // ★ New-chat landing page already has cursor focused in input — clicking risks UNFOCUSING it!
        Console.WriteLine("  [PROMPT] turn-form not found after 3 retries. Brute-force paste (no click)...");

        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(claudeHwnd);
        Thread.Sleep(300);

        // Paste text directly — cursor is already in the input field
        SetClipboardText(text);
        Thread.Sleep(100);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(300);

        // Submit with Enter
        KeyboardInput.PressKey("enter");
        Thread.Sleep(200);
        Console.WriteLine("  [PROMPT] NEW-CHAT: text pasted + Enter pressed (brute-force)");

        // Restore focus
        if (prevFg != IntPtr.Zero && prevFg != claudeHwnd)
        {
            Thread.Sleep(500);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("  [PROMPT] Focus restored after new-chat input");
        }

        return true;
    }

    /// <summary>
    /// Open a new chat in Claude Desktop via UIA sidebar "새 대화" button (fully Focusless!).
    /// Flow: Toggle sidebar open → Invoke "새 대화" Hyperlink → verify new JSONL → Toggle sidebar closed.
    /// Falls back to Ctrl+N (focus steal) if UIA approach fails.
    /// Verification: checks that a new JSONL session file was created (= new chat confirmed).
    /// </summary>
    public bool OpenNewChat(PromptInfo? currentPrompt = null)
    {
        try
        {
            if (currentPrompt != null)
            {
                if (string.Equals(currentPrompt.HostType, HostCodexDesktop, StringComparison.OrdinalIgnoreCase))
                    return OpenCodexDesktopNewChat(currentPrompt);
            }

            // Find Claude Desktop window
            IntPtr claudeHwnd;
            FlaUI.Core.AutomationElements.AutomationElement? claudeRoot = null;
            if (currentPrompt != null)
            {
                claudeHwnd = currentPrompt.WindowHandle;
            }
            else
            {
                var found = FindPrompt();
                if (found == null)
                {
                    Console.WriteLine("  [PROMPT] OpenNewChat: Claude Desktop window not found");
                    return false;
                }
                if (string.Equals(found.HostType, HostCodexDesktop, StringComparison.OrdinalIgnoreCase))
                    return OpenCodexDesktopNewChat(found);
                claudeHwnd = found.WindowHandle;
            }

            // Snapshot current JSONL for change detection
            var (beforePath, beforeCreation) = GetLatestJsonlInfo();
            Console.WriteLine($"  [PROMPT] OpenNewChat: Before JSONL = {Path.GetFileName(beforePath ?? "(none)")} created={beforeCreation:HH:mm:ss}");

            // === Strategy 1: UIA Focusless — sidebar toggle + "새 대화" button ===
            bool uiaTriggered = false;
            try
            {
                using var automation = new FlaUI.UIA3.UIA3Automation();
                claudeRoot = automation.FromHandle(claudeHwnd);
                if (claudeRoot != null)
                {
                    // Step 1: Find and click "사이드바 열기" toggle button
                    var sidebarBtn = claudeRoot.FindAllDescendants()
                        .FirstOrDefault(e => e.ControlType == ControlType.Button &&
                                            (e.Name ?? "").Contains("사이드바"));
                    if (sidebarBtn != null && sidebarBtn.Patterns.Toggle.IsSupported)
                    {
                        var toggleState = sidebarBtn.Patterns.Toggle.Pattern.ToggleState;
                        Console.WriteLine($"  [PROMPT] OpenNewChat: Sidebar toggle found (state={toggleState})");

                        // Open sidebar if closed
                        if (toggleState == FlaUI.Core.Definitions.ToggleState.Off)
                        {
                            sidebarBtn.Patterns.Toggle.Pattern.Toggle();
                            Console.WriteLine("  [PROMPT] OpenNewChat: Sidebar opened (focusless!)");
                            Thread.Sleep(500); // Wait for sidebar animation
                        }

                        // Step 2: Find "새 대화" hyperlink in sidebar and Invoke
                        var newChatLink = claudeRoot.FindAllDescendants()
                            .FirstOrDefault(e => e.ControlType == ControlType.Hyperlink &&
                                                (e.Name ?? "").Contains("새 대화"));
                        if (newChatLink != null && newChatLink.Patterns.Invoke.IsSupported)
                        {
                            newChatLink.Patterns.Invoke.Pattern.Invoke();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("  [PROMPT] OpenNewChat: '새 대화' clicked (fully focusless!)");
                            Console.ResetColor();
                            uiaTriggered = true;
                        }
                        else
                        {
                            Console.WriteLine("  [PROMPT] OpenNewChat: '새 대화' hyperlink not found in sidebar");
                        }

                        // Step 3: Close sidebar back (cleanup)
                        Thread.Sleep(300);
                        // Re-find sidebar button (DOM may have changed)
                        sidebarBtn = claudeRoot.FindAllDescendants()
                            .FirstOrDefault(e => e.ControlType == ControlType.Button &&
                                                (e.Name ?? "").Contains("사이드바"));
                        if (sidebarBtn?.Patterns.Toggle.IsSupported == true)
                        {
                            var newState = sidebarBtn.Patterns.Toggle.Pattern.ToggleState;
                            if (newState == FlaUI.Core.Definitions.ToggleState.On)
                            {
                                sidebarBtn.Patterns.Toggle.Pattern.Toggle();
                                Console.WriteLine("  [PROMPT] OpenNewChat: Sidebar closed back (focusless!)");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] OpenNewChat: UIA strategy failed: {ex.Message}");
            }

            // === Strategy 2: Ctrl+N fallback (requires focus steal) ===
            if (!uiaTriggered)
            {
                Console.WriteLine("  [PROMPT] OpenNewChat: Falling back to Ctrl+N (focus steal)...");
                var prevForeground = NativeMethods.GetForegroundWindow();
                NativeMethods.SmartSetForegroundWindow(claudeHwnd);
                Thread.Sleep(300);
                KeyboardInput.Hotkey(new[] { "ctrl", "n" });
                Console.WriteLine("  [PROMPT] OpenNewChat: Sent Ctrl+N");
                Thread.Sleep(300);
                if (prevForeground != IntPtr.Zero && prevForeground != claudeHwnd)
                    NativeMethods.SmartSetForegroundWindow(prevForeground);
            }

            // Wait and verify: new JSONL file should appear (= new session)
            // Detection: new file path OR same path but newer CreationTime
            // Minimum size: new JSONL should have some initial content (>100 bytes)
            const long MinNewJsonlBytes = 100;
            bool newChatConfirmed = false;
            for (int retry = 0; retry < 15; retry++)  // 15 retries × 500ms = 7.5s max
            {
                Thread.Sleep(500);
                var (afterPath, afterCreation) = GetLatestJsonlInfo();
                if (afterPath == null) continue;

                // Check 1: different file path = definitely new chat
                // Check 2: same path but newer creation time = recycled name (unlikely but safe)
                bool isNewFile = afterPath != beforePath ||
                                 (afterCreation > beforeCreation && (afterCreation - beforeCreation).TotalSeconds > 2);

                if (isNewFile)
                {
                    var afterSize = 0L;
                    try { afterSize = new FileInfo(afterPath).Length; } catch { }

                    if (afterSize >= MinNewJsonlBytes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  [PROMPT] OpenNewChat: New JSONL confirmed! {Path.GetFileName(afterPath)} ({afterSize}B, created={afterCreation:HH:mm:ss})");
                        Console.ResetColor();
                        newChatConfirmed = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"  [PROMPT] OpenNewChat: New JSONL found but too small ({afterSize}B < {MinNewJsonlBytes}B), waiting...");
                    }
                }
            }

            if (!newChatConfirmed)
            {
                // Fallback: check if prompt exists (less reliable but better than nothing)
                var newPrompt = FindPrompt();
                if (newPrompt != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  [PROMPT] OpenNewChat: No new JSONL confirmed, but prompt found (assuming OK)");
                    Console.ResetColor();
                    return true;
                }
                Console.WriteLine("  [PROMPT] OpenNewChat: FAILED — no new JSONL and no prompt found");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] OpenNewChat error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Get the most recently created JSONL file in ~/.claude/projects/
    /// Returns (path, creationTime) for new chat detection.
    /// Uses CreationTimeUtc to distinguish genuinely new files from merely updated ones.
    /// </summary>
    internal static (string? path, DateTime creationUtc) GetLatestJsonlInfo()
    {
        try
        {
            var claudeProjectsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "projects");
            if (!Directory.Exists(claudeProjectsDir)) return (null, DateTime.MinValue);

            var latest = Directory.EnumerateFiles(claudeProjectsDir, "*.jsonl", SearchOption.AllDirectories)
                .Select(f => { try { return new FileInfo(f); } catch { return null; } })
                .Where(fi => fi != null)
                .OrderByDescending(fi => fi!.CreationTimeUtc)
                .FirstOrDefault();

            if (latest == null) return (null, DateTime.MinValue);
            return (latest.FullName, latest.CreationTimeUtc);
        }
        catch { return (null, DateTime.MinValue); }
    }

    /// <summary>Backward compat wrapper — returns just the path.</summary>
    private static string? GetLatestJsonlPath()
        => GetLatestJsonlInfo().path;

    private bool OpenCodexDesktopNewChat(PromptInfo prompt)
    {
        try
        {
            Console.WriteLine("  [PROMPT] OpenNewChat: Codex desktop via Ctrl+N");
            var prevForeground = NativeMethods.GetForegroundWindow();
            NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
            Thread.Sleep(250);
            KeyboardInput.Hotkey(new[] { "ctrl", "n" });
            Thread.Sleep(700);

            NativeMethods.GetWindowThreadProcessId(prompt.WindowHandle, out uint pid);
            var refreshed = pid != 0 ? FindCodexDesktopPromptByMarker((int)pid) : null;
            var ok = refreshed != null;
            Console.WriteLine(ok
                ? "  [PROMPT] OpenNewChat: Codex prompt still discoverable after Ctrl+N"
                : "  [PROMPT] OpenNewChat: Codex prompt not rediscovered after Ctrl+N");

            if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
            {
                Thread.Sleep(150);
                NativeMethods.SmartSetForegroundWindow(prevForeground);
            }

            return ok;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] OpenNewChat Codex error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Complete auto-relay: open new chat and type handoff prompt immediately.
    /// NOTE: Prefer Schedule-based relay (OpenNewChat + ScheduleManager.Add) for reliability.
    /// This method is kept as a manual/fallback option.
    /// </summary>
    public bool AutoRelay(string handoffPrompt)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[RELAY] Starting auto-relay to new chat...");
        Console.ResetColor();

        // 1. Open new chat
        if (!OpenNewChat())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to open new chat!");
            Console.ResetColor();
            return false;
        }

        // 2. Wait a bit for new chat to stabilize
        Thread.Sleep(1000);

        // 3. Find prompt in the new chat
        var newPrompt = FindPrompt();
        if (newPrompt == null)
        {
            // Retry
            Thread.Sleep(2000);
            newPrompt = FindPrompt();
        }
        if (newPrompt == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to find prompt in new chat!");
            Console.ResetColor();
            return false;
        }

        // 4. Type handoff prompt
        var ok = TypeAndSubmit(newPrompt, handoffPrompt);
        if (ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[RELAY] ✅ Auto-relay complete! New chat started with handoff prompt.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to type handoff prompt!");
            Console.ResetColor();
        }
        return ok;
    }
}
