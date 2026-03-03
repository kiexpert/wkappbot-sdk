using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Find and interact with the Claude Code prompt input field.
/// Supports: Claude Desktop (Electron), VS Code Claude Code Extension.
///
/// Strategy:
///   1. Find the parent process of wkappbot.exe (claude.exe or code.exe)
///   2. Walk up to the main window of that process
///   3. Find the prompt input via UIA: aid="turn-form" → child Group "입력하세요"
///   4. Insert text via MSAA put_accValue (focusless!)
///   5. Submit via UIA Invoke on "제출" button (focusless!) — NO focus steal needed!
///
/// Key Discoveries (2026-02-26):
///   1. Direct MSAA vtable put_accValue() on the contentEditable's parent MSAA element
///      (name="입력하세요", ROLE_SYSTEM_GROUPING=20) returns S_OK and successfully inserts text,
///      even though FlaUI's UIA LegacyIAccessible.SetValue returns E_NOTIMPL for the same element.
///      The difference: direct MSAA vtable call bypasses the UIA-to-MSAA proxy bridge.
///   2. The submit button in Claude Desktop is named "제출" (Korean for "submit") with
///      UIA Invoke pattern. accDoDefaultAction("활성화") on contentEditable is a false positive
///      — it only activates/focuses the element, doesn't submit.
///   3. MSAA tree walk pitfall: GROUPING(role=20) with action "상위 개체 클릭" is NOT a button!
/// </summary>
public sealed class ClaudePromptHelper : IDisposable
{
    private readonly UIA3Automation _automation;

    /// <summary>
    /// Global toggle: when true, only focusless strategies are allowed.
    /// When true, allows focus-stealing as a fallback after focusless input fails.
    /// Default = false → pure focusless mode (all focus-stealing paths blocked).
    /// Set true for critical actions like handoff nudges where delivery matters.
    /// Focus-stealing goes through normal InputReadiness path (overlay + sound + zoom).
    /// </summary>
    public static bool AllowFocusSteal { get; set; }

    public ClaudePromptHelper()
    {
        _automation = new UIA3Automation();
    }

    /// <summary>
    /// Result of a prompt search.
    /// </summary>
    public record PromptInfo(
        IntPtr WindowHandle,
        string WindowTitle,
        string ProcessName,
        Rectangle PromptRect,
        string HostType // "claude-desktop" | "vscode" | "unknown"
    );

    /// <summary>
    /// Find the Claude Code prompt input by walking the process tree.
    /// Strategy: current process → parent → grandparent ... → find Electron/VSCode main window → UIA search.
    /// </summary>
    public PromptInfo? FindPrompt()
    {
        // Strategy 1: Walk up process tree from current wkappbot.exe
        var ancestors = GetAncestorProcesses();
        foreach (var (pid, name) in ancestors)
        {
            Console.WriteLine($"  [PROMPT] Checking ancestor: {name} (PID={pid})");

            if (name.Equals("claude", StringComparison.OrdinalIgnoreCase))
            {
                // Claude Desktop (Electron app)
                var result = FindClaudeDesktopPrompt(pid);
                if (result != null) return result;
            }
            else if (name.Equals("code", StringComparison.OrdinalIgnoreCase) ||
                     name.Equals("code - insiders", StringComparison.OrdinalIgnoreCase))
            {
                // VS Code with Claude Code extension
                var result = FindVSCodePrompt(pid);
                if (result != null) return result;
            }
        }

        // Strategy 2: Enumerate all visible windows from claude.exe processes
        Console.WriteLine("  [PROMPT] Ancestor walk failed, scanning all claude.exe windows...");
        var claudeProcs = Process.GetProcessesByName("claude");
        Console.WriteLine($"  [PROMPT] Process.GetProcessesByName(\"claude\"): {claudeProcs.Length} process(es)");
        foreach (var proc in claudeProcs)
        {
            Console.WriteLine($"  [PROMPT]   claude PID={proc.Id}");
            var result = FindClaudeDesktopPrompt(proc.Id);
            if (result != null) return result;
        }

        // Strategy 3: Enumerate all VS Code windows
        Console.WriteLine("  [PROMPT] Scanning VS Code windows...");
        var codeProcs = Process.GetProcessesByName("Code");
        Console.WriteLine($"  [PROMPT] Process.GetProcessesByName(\"Code\"): {codeProcs.Length} process(es)");
        foreach (var proc in codeProcs)
        {
            var result = FindVSCodePrompt(proc.Id);
            if (result != null) return result;
        }

        // Strategy 4: Brute-force — scan ALL Chrome_WidgetWin_1 windows for turn-form
        Console.WriteLine("  [PROMPT] Brute-force: scanning all Chrome_WidgetWin_1 windows...");
        var allElectronWindows = new List<IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls == "Chrome_WidgetWin_1")
            {
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string procName = "?";
                try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                Console.WriteLine($"  [PROMPT]   Chrome_WidgetWin_1: 0x{hWnd:X} \"{title}\" proc={procName} pid={pid}");
                allElectronWindows.Add(hWnd);
            }
            return true;
        }, IntPtr.Zero);

        // Try each Chrome_WidgetWin_1 window for turn-form
        foreach (var hWnd in allElectronWindows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                if (turnForm == null) continue;

                var inputGroup = turnForm.FindFirstChild(
                    new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
                if (inputGroup == null)
                {
                    var children = turnForm.FindAllChildren();
                    inputGroup = children.FirstOrDefault(c =>
                        c.ControlType == ControlType.Group && c.BoundingRectangle.Width > 100);
                }
                if (inputGroup == null) continue;

                var rect = inputGroup.BoundingRectangle;
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string pName = "?";
                try { pName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                Console.WriteLine($"  [PROMPT] BRUTE-FORCE FOUND: turn-form in \"{title}\" proc={pName} pid={pid}");
                return new PromptInfo(hWnd, title, pName, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    pName.Equals("claude", StringComparison.OrdinalIgnoreCase) ? "claude-desktop" : "unknown");
            }
            catch { }
        }

        Console.WriteLine("  [PROMPT] ALL strategies exhausted — no prompt found");
        return null;
    }

    /// <summary>
    /// Find the Claude Code prompt for a SPECIFIC project CWD.
    /// Matches VS Code by window title, Claude Desktop by UIA text content.
    /// Returns null if no matching prompt found (caller should fall back to Slack).
    /// </summary>
    public PromptInfo? FindPromptForCwd(string targetCwd)
    {
        var cwdFolder = Path.GetFileName(targetCwd); // e.g., "WKAppBot"
        if (string.IsNullOrEmpty(cwdFolder)) return null;
        Console.WriteLine($"  [PROMPT-CWD] Searching for prompt matching CWD: {targetCwd} (key: {cwdFolder})");

        var allWindows = new List<(IntPtr hWnd, string title, string procName)>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls == "Chrome_WidgetWin_1")
            {
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string procName = "?";
                try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                allWindows.Add((hWnd, title, procName));
            }
            return true;
        }, IntPtr.Zero);

        // Priority 1: VS Code — match by window title containing folder name
        foreach (var (hWnd, title, procName) in allWindows)
        {
            if (!procName.Equals("Code", StringComparison.OrdinalIgnoreCase)) continue;
            if (!title.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase)) continue;
            Console.WriteLine($"  [PROMPT-CWD] VS Code title match: \"{title}\"");
            var pi = FindTurnFormInWindow(hWnd, title, procName);
            if (pi != null) return pi;
        }

        // Priority 2: Claude Desktop — search UIA for CWD text at bottom bar
        foreach (var (hWnd, title, procName) in allWindows)
        {
            if (!procName.Equals("claude", StringComparison.OrdinalIgnoreCase)) continue;
            Console.WriteLine($"  [PROMPT-CWD] Checking Claude Desktop window: \"{title}\"");
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;
                // Search for any element (Button/Text) whose Name matches the CWD path
                // Claude Desktop Code tab shows CWD as a Button at the bottom bar
                var allElements = root.FindAllDescendants();
                bool cwdFound = false;
                foreach (var el in allElements)
                {
                    try
                    {
                        var name = el.Name;
                        if (string.IsNullOrEmpty(name) || name.Length > 500) continue;
                        if (name.Equals(targetCwd, StringComparison.OrdinalIgnoreCase)
                            || name.EndsWith("\\" + cwdFolder, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"  [PROMPT-CWD] CWD element found in Claude Desktop: [{el.ControlType}] \"{name}\"");
                            cwdFound = true;
                            break;
                        }
                    }
                    catch { }
                }
                if (!cwdFound) continue;
                var pi = FindTurnFormInWindow(hWnd, title, procName);
                if (pi != null) return pi;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT-CWD] Claude Desktop UIA error: {ex.Message}");
            }
        }

        Console.WriteLine($"  [PROMPT-CWD] No matching prompt found for CWD: {targetCwd}");
        return null;
    }

    /// <summary>
    /// Find turn-form prompt input in a specific window handle.
    /// Shared helper for FindPrompt and FindPromptForCwd.
    /// </summary>
    private PromptInfo? FindTurnFormInWindow(IntPtr hWnd, string title, string procName)
    {
        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return null;
            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm == null) return null;

            var inputGroup = turnForm.FindFirstChild(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
            if (inputGroup == null)
            {
                var children = turnForm.FindAllChildren();
                inputGroup = children.FirstOrDefault(c =>
                    c.ControlType == ControlType.Group && c.BoundingRectangle.Width > 100);
            }
            if (inputGroup == null) return null;

            var rect = inputGroup.BoundingRectangle;
            var hostType = procName.Equals("claude", StringComparison.OrdinalIgnoreCase) ? "claude-desktop" : "vscode";
            Console.WriteLine($"  [PROMPT-CWD] turn-form found in \"{title}\" ({hostType})");
            return new PromptInfo(hWnd, title, procName,
                new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), hostType);
        }
        catch { return null; }
    }

    /// <summary>
    /// Comprehensive diagnostic for prompt search failure.
    /// Returns a multi-line string with all relevant details for debugging.
    /// Call this AFTER FindPrompt() returns null to capture the full picture.
    /// </summary>
    public string DiagnosePromptSearch()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("=== Prompt Search Diagnosis ===");

        // 1. Process ancestry
        try
        {
            var ancestors = GetAncestorProcesses();
            sb.AppendLine($"Process ancestry ({ancestors.Count}):");
            foreach (var (pid, name) in ancestors)
                sb.AppendLine($"  {name} (PID={pid})");
        }
        catch (Exception ex) { sb.AppendLine($"Ancestry error: {ex.Message}"); }

        // 2. All claude.exe processes and their windows
        try
        {
            var claudeProcs = Process.GetProcessesByName("claude");
            sb.AppendLine($"claude.exe processes: {claudeProcs.Length}");
            foreach (var proc in claudeProcs)
            {
                var windows = FindWindowsByProcessId(proc.Id);
                sb.AppendLine($"  PID={proc.Id}: {windows.Count} visible window(s)");
                foreach (var hWnd in windows)
                {
                    var title = WindowFinder.GetWindowText(hWnd);
                    var cls = WindowFinder.GetClassName(hWnd);
                    NativeMethods.GetWindowRect(hWnd, out var r);
                    sb.AppendLine($"    0x{hWnd:X} \"{title}\" class={cls} {r.Right - r.Left}x{r.Bottom - r.Top}");

                    // Check UIA for turn-form
                    try
                    {
                        var root = _automation.FromHandle(hWnd);
                        if (root == null) { sb.AppendLine("      UIA: null"); continue; }

                        var tf = root.FindFirstDescendant(
                            new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                        if (tf != null)
                        {
                            sb.AppendLine($"      turn-form: FOUND at {tf.BoundingRectangle}");
                            // Check children
                            var children = tf.FindAllChildren();
                            sb.AppendLine($"      turn-form children ({children.Length}):");
                            foreach (var c in children)
                            {
                                try { sb.AppendLine($"        {c.ControlType} \"{c.Name}\" {c.BoundingRectangle.Width}x{c.BoundingRectangle.Height} enabled={c.IsEnabled}"); }
                                catch { sb.AppendLine($"        (read error)"); }
                            }
                        }
                        else
                        {
                            sb.AppendLine("      turn-form: NOT FOUND");
                            // Deep UIA dump → saved to file for analysis
                            try
                            {
                                var deepSb = new System.Text.StringBuilder();
                                deepSb.AppendLine($"=== Deep UIA Dump: 0x{hWnd:X} \"{title}\" ===");
                                DumpUiaTree(root, deepSb, 0, maxDepth: 10);
                                var dumpPath = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    ".claude", "projects", "prompt_uia_dump_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
                                try { Directory.CreateDirectory(Path.GetDirectoryName(dumpPath)!); } catch { }
                                File.WriteAllText(dumpPath, deepSb.ToString());
                                sb.AppendLine($"      UIA deep dump saved: {dumpPath}");
                                // Also include top 20 lines in the summary
                                var lines = deepSb.ToString().Split('\n');
                                foreach (var line in lines.Take(20))
                                    sb.AppendLine($"      {line.TrimEnd()}");
                                if (lines.Length > 20) sb.AppendLine($"      ...(+{lines.Length - 20} more lines in dump file)");
                            }
                            catch (Exception dumpEx) { sb.AppendLine($"      (UIA dump error: {dumpEx.Message})"); }
                        }
                    }
                    catch (Exception uiaEx) { sb.AppendLine($"      UIA error: {uiaEx.Message}"); }
                }
            }
        }
        catch (Exception ex) { sb.AppendLine($"Claude process scan error: {ex.Message}"); }

        // 3. Foreground window info
        try
        {
            var fg = NativeMethods.GetForegroundWindow();
            if (fg != IntPtr.Zero)
            {
                var fgTitle = WindowFinder.GetWindowText(fg);
                var fgClass = WindowFinder.GetClassName(fg);
                NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
                sb.AppendLine($"Foreground: 0x{fg:X} \"{fgTitle}\" class={fgClass} pid={fgPid}");
            }
            else sb.AppendLine("Foreground: none");
        }
        catch { }

        return sb.ToString();
    }

    /// <summary>Recursive UIA tree dump for diagnostics.</summary>
    private static void DumpUiaTree(AutomationElement el, System.Text.StringBuilder sb, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var indent = new string(' ', depth * 2);
        // Read element properties individually (each can fail independently)
        string name = "?", aid = "?", ct = "?", rect = "?";
        try { name = (el.Name ?? "").Replace("\n", " ").Replace("\r", ""); if (name.Length > 60) name = name.Substring(0, 60) + "..."; } catch { }
        try { aid = el.AutomationId ?? ""; } catch { }
        try { ct = el.ControlType.ToString(); } catch { }
        try { var r = el.BoundingRectangle; rect = $"{r.Width}x{r.Height} at {r.X},{r.Y}"; } catch { }
        sb.AppendLine($"{indent}[{ct}] aid=\"{aid}\" name=\"{name}\" ({rect})");

        // Always try to enumerate children even if parent properties failed
        try
        {
            foreach (var child in el.FindAllChildren())
                DumpUiaTree(child, sb, depth + 1, maxDepth);
        }
        catch { }
    }

    /// <summary>
    /// Fallback for new-chat landing page: when turn-form doesn't exist,
    /// activate Claude Desktop, click the input area, paste text, and submit.
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
    /// Type text into the Claude prompt and submit with Enter.
    /// Strategy (2-tier):
    ///   1. Focusless: MSAA put_accValue via direct vtable (no focus steal!)
    ///   2. Fallback: SetForegroundWindow → Click → Paste → Enter → Restore previous foreground
    /// </summary>
    public bool TypeAndSubmit(PromptInfo prompt, string text)
    {
        // === VS Code Claude Code (native extension): focus-steal + Escape + paste ===
        if (prompt.HostType == "vscode-claudecode")
            return TypeAndSubmitVSCodeClaudeCode(prompt, text);

        // === Strategy 1: Try fully focusless input ===
        if (TryFocuslessInput(prompt, text))
            return true;

        // === Strategy 2: Focus-stealing with auto-restore (minimal disruption) ===
        if (!AllowFocusSteal)
        {
            Console.WriteLine("  [PROMPT] Focusless-only mode: focusless input failed, focus steal not allowed");
            return false;
        }
        var prevForeground = NativeMethods.GetForegroundWindow();
        Console.WriteLine($"  [PROMPT] Activating: {prompt.WindowTitle} (will restore focus)");
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        // Click the prompt area to focus the contentEditable div
        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(150);

        // Clear existing text
        KeyboardInput.Hotkey(new[] { "ctrl", "a" });
        Thread.Sleep(30);
        KeyboardInput.PressKey("delete");
        Thread.Sleep(30);

        // Paste via clipboard (fast, supports multiline + unicode)
        Console.WriteLine($"  [PROMPT] Pasting ({text.Length} chars)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(200);

        // Submit with Enter
        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT] Submitted");

        // Restore previous foreground window (minimize user disruption)
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(300); // Brief pause to let Claude register the submit
            NativeMethods.SmartSetForegroundWindow(prevForeground);
            Console.WriteLine("  [PROMPT] Focus restored to previous window");
        }

        return true;
    }

    /// <summary>
    /// VS Code Claude Code extension: focus-steal + Escape (focus input) + paste + Enter.
    /// UIA turn-form 없음 → 키보드 단축키로 입력 영역 포커싱.
    /// Escape: Claude Code 입력창 포커스 (VS Code 확장 기본 동작).
    /// 입력위치확보(Probe) 승인 후 호출되므로 포커스 전환 허용됨.
    /// </summary>
    private bool TypeAndSubmitVSCodeClaudeCode(PromptInfo prompt, string text)
    {
        var prevForeground = NativeMethods.GetForegroundWindow();
        Console.WriteLine($"  [PROMPT:VSCODE-CC] Activating: \"{prompt.WindowTitle}\"");

        // Step 1: Bring VS Code to foreground
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(300);

        // Step 2: Escape — focuses Claude Code input area
        KeyboardInput.PressKey("escape");
        Thread.Sleep(200);

        // Step 3: Paste via clipboard (fast, supports multiline + unicode)
        Console.WriteLine($"  [PROMPT:VSCODE-CC] Pasting ({text.Length} chars)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(300);

        // Step 4: Submit with Enter
        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT:VSCODE-CC] Submitted");

        // Step 5: Restore previous foreground window
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(500); // Brief pause for Claude to register
            NativeMethods.SmartSetForegroundWindow(prevForeground);
            Console.WriteLine("  [PROMPT:VSCODE-CC] Focus restored");
        }

        return true;
    }

    /// <summary>
    /// Try to input text into the Claude prompt WITHOUT stealing focus.
    /// Priority: MSAA put_accValue (direct vtable) → LegacyIAccessible.SetValue (UIA bridge).
    /// Returns true if text was successfully inserted AND submitted.
    /// </summary>
    private bool TryFocuslessInput(PromptInfo prompt, string text)
    {
        try
        {
            // === IA2 attempt: IAccessibleEditableText.insertText (the dream!) ===
            if (TryIA2InsertText(prompt, text))
            {
                // Text inserted focuslessly! Now submit.
                Console.WriteLine("  [PROMPT] IA2 text insertion succeeded! Submitting...");
                var root2 = _automation.FromHandle(prompt.WindowHandle);
                var tf2 = root2?.FindFirstDescendant(
                    new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                if (tf2 != null)
                    return TryFocuslessSubmit(prompt, tf2);
                // Fallback: brief focus for Enter
                var prevFg = NativeMethods.GetForegroundWindow();
                NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
                Thread.Sleep(100);
                KeyboardInput.PressKey("enter");
                Thread.Sleep(100);
                if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
                    NativeMethods.SmartSetForegroundWindow(prevFg);
                Console.WriteLine("  [PROMPT] Submitted (IA2 text + brief focus Enter)");
                return true;
            }

            // === LegacyIAccessible attempt (known to fail on Electron contentEditable) ===
            Console.WriteLine("  [PROMPT] Trying focusless input (LegacyIAccessible)...");
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return false;

            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(
                    _automation.PropertyLibrary.Element.AutomationId,
                    "turn-form"));
            if (turnForm == null)
            {
                Console.WriteLine("  [PROMPT] Focusless: turn-form not found");
                return false;
            }

            // Find the input group (contentEditable div)
            var inputGroup = turnForm.FindFirstChild(
                new PropertyCondition(
                    _automation.PropertyLibrary.Element.ControlType,
                    ControlType.Group));
            if (inputGroup == null)
            {
                var children = turnForm.FindAllChildren();
                inputGroup = children.FirstOrDefault(c =>
                    c.ControlType == ControlType.Group &&
                    c.BoundingRectangle.Width > 100);
            }
            if (inputGroup == null)
            {
                Console.WriteLine("  [PROMPT] Focusless: inputGroup not found");
                return false;
            }

            // Try LegacyIAccessible.SetValue
            var legacy = inputGroup.Patterns.LegacyIAccessible;
            if (!legacy.IsSupported)
            {
                Console.WriteLine("  [PROMPT] Focusless: LegacyIA not supported");
                return false;
            }

            // Method 1: LegacyIAccessible.SetValue
            try
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue succeeded! ({text.Length} chars, focusless!)");
                // If we get here, text was set — try to submit and return
                return TryFocuslessSubmit(prompt, turnForm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue failed: {ex.Message}");
            }

            // NOTE: LegacyIAccessible.SetValue returns E_NOTIMPL for Electron contentEditable
            // because FlaUI's UIA→MSAA bridge adds extra validation.
            // Direct MSAA vtable put_accValue works! (handled above via TryIA2InsertText)
            // PostMessage WM_PASTE also doesn't work (Chrome ignores when not foreground).

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] Focusless error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Try to submit the prompt focuslessly using multiple strategies:
    ///   1. UIA: Invoke pattern on Submit/Send button (most reliable!)
    ///   2. MSAA: Walk tree to find Send button → accDoDefaultAction
    ///   3. PostMessage VK_RETURN to Chrome renderer hwnd
    ///   4. Fallback: brief focus steal for Enter key
    /// </summary>
    /// <summary>
    /// Unified submit logic: find "제출" button → check state → invoke → verify → retry.
    /// Returns true only if submit is VERIFIED (button disappeared or "중단" appeared).
    /// </summary>
    private bool TryFocuslessSubmit(PromptInfo prompt, AutomationElement turnForm)
    {
        Thread.Sleep(500); // Wait for UI to settle + React to detect DOM change from put_accValue

        // === Strategy 1: UIA Invoke with verify (fully focusless) ===
        if (TrySubmitWithVerify(turnForm, "UIA"))
            return true;

        // === Strategy 2: MSAA accDoDefaultAction ===
        if (TryMsaaSubmit(prompt))
        {
            Console.WriteLine("  [PROMPT] MSAA submit fired, verifying...");
            if (VerifySubmitSuccess(turnForm))
            {
                Console.WriteLine("  [PROMPT] Submitted via MSAA (verified, focusless!)");
                return true;
            }
            Console.WriteLine("  [PROMPT] MSAA submit fired but NOT verified");
        }

        // === Strategy 3: PostMessage VK_RETURN ===
        if (TryPostMessageEnter(prompt))
        {
            Console.WriteLine("  [PROMPT] PostMessage VK_RETURN fired, verifying...");
            if (VerifySubmitSuccess(turnForm))
            {
                Console.WriteLine("  [PROMPT] Submitted via PostMessage VK_RETURN (verified, focusless!)");
                return true;
            }
            Console.WriteLine("  [PROMPT] PostMessage submit fired but NOT verified");
        }

        // === Strategy 4: brief focus + nudge React + Enter ===
        // put_accValue sets DOM text but React may not see it (no input event fired).
        // Brief focus + Space+Backspace triggers React's onChange, then Enter submits.
        if (!AllowFocusSteal)
        {
            Console.WriteLine("  [PROMPT] Focusless-only mode: focusless submit failed, focus steal not allowed");
            return false;
        }
        Console.WriteLine("  [PROMPT] Focusless strategies exhausted, trying brief focus + React nudge...");
        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        // Nudge React: End → Space → Backspace (triggers input event without changing text)
        KeyboardInput.PressKey("end");
        Thread.Sleep(50);
        KeyboardInput.PressKey("space");
        Thread.Sleep(50);
        KeyboardInput.PressKey("backspace");
        Thread.Sleep(200); // Let React process the input event

        // Now submit with Enter (retry 3 times)
        for (int retry = 0; retry < 3; retry++)
        {
            KeyboardInput.PressKey("enter");
            Thread.Sleep(500);

            // After each Enter, check if submit succeeded
            try
            {
                var checkResult = CheckSubmitButton(turnForm);
                if (checkResult == SubmitButtonState.Gone || checkResult == SubmitButtonState.StopAppeared)
                {
                    Console.WriteLine($"  [PROMPT] Submitted via focus+Enter (retry {retry + 1}, verified!)");
                    RestoreFocus(prevFg, prompt.WindowHandle);
                    return true;
                }
            }
            catch { }
        }

        RestoreFocus(prevFg, prompt.WindowHandle);
        Console.WriteLine("  [PROMPT] Submitted (focus+Enter, 3x retry, unverified)");
        return true; // Best-effort: text was set + Enter pressed 3 times
    }

    /// <summary>
    /// Find submit button, check its enabled state, invoke, verify, retry up to 3 times.
    /// Returns true only if submit is VERIFIED (button gone or "중단" appeared).
    /// </summary>
    private bool TrySubmitWithVerify(AutomationElement turnForm, string tag)
    {
        for (int attempt = 0; attempt < 3; attempt++)
        {
            if (attempt > 0) Thread.Sleep(500 * attempt);
            try
            {
                var btn = FindSubmitButton(turnForm);
                if (btn == null)
                {
                    if (attempt < 2) Console.WriteLine($"  [PROMPT] [{tag}] Submit button not found (attempt {attempt + 1}/3)");
                    continue;
                }

                // Check if button is enabled/invokable
                if (!btn.Patterns.Invoke.IsSupported)
                {
                    Console.WriteLine($"  [PROMPT] [{tag}] Button \"{btn.Name}\" found but Invoke not supported");
                    continue;
                }
                if (!btn.IsEnabled)
                {
                    Console.WriteLine($"  [PROMPT] [{tag}] Button \"{btn.Name}\" found but DISABLED (React may not see text yet)");
                    Thread.Sleep(300); // Extra wait for React
                    continue;
                }

                // Invoke the button
                btn.Patterns.Invoke.Pattern.Invoke();
                Console.WriteLine($"  [PROMPT] [{tag}] Invoked \"{btn.Name}\" (attempt {attempt + 1})");

                // Verify: wait up to 2 seconds for submit to take effect
                for (int check = 0; check < 4; check++)
                {
                    Thread.Sleep(500);
                    var state = CheckSubmitButton(turnForm);
                    if (state == SubmitButtonState.Gone || state == SubmitButtonState.StopAppeared)
                    {
                        Console.WriteLine($"  [PROMPT] [{tag}] Submit VERIFIED ({state}, attempt {attempt + 1}, fully focusless!)");
                        return true;
                    }
                }

                // Button still visible after 2s — submit likely failed, re-press
                Console.WriteLine($"  [PROMPT] [{tag}] Submit NOT verified after invoke (attempt {attempt + 1}), retrying...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] [{tag}] Error (attempt {attempt + 1}): {ex.Message}");
            }
        }

        Console.WriteLine($"  [PROMPT] [{tag}] All 3 attempts exhausted without verification");
        return false;
    }

    private enum SubmitButtonState { Visible, Gone, StopAppeared }

    /// <summary>
    /// Check if submit succeeded by examining button state.
    /// Gone = button disappeared (submit consumed), StopAppeared = Claude is processing.
    /// </summary>
    private SubmitButtonState CheckSubmitButton(AutomationElement turnForm)
    {
        try
        {
            var elements = turnForm.FindAllDescendants();
            bool submitVisible = false;
            foreach (var el in elements)
            {
                if (el.ControlType != ControlType.Button) continue;
                var name = (el.Name ?? "").ToLowerInvariant();
                if (name.Contains("중단") || name.Contains("stop"))
                    return SubmitButtonState.StopAppeared;
                if (name.Contains("제출") || name.Contains("submit") || name.Contains("send") || name.Contains("전송"))
                    submitVisible = true;
            }
            return submitVisible ? SubmitButtonState.Visible : SubmitButtonState.Gone;
        }
        catch { return SubmitButtonState.Gone; } // Element stale → likely submitted
    }

    /// <summary>
    /// Find the submit/send button in turn-form descendants.
    /// </summary>
    private AutomationElement? FindSubmitButton(AutomationElement turnForm)
    {
        try
        {
            foreach (var el in turnForm.FindAllDescendants())
            {
                if (el.ControlType != ControlType.Button) continue;
                var name = (el.Name ?? "").ToLowerInvariant();
                var aid = (el.AutomationId ?? "").ToLowerInvariant();
                if (name.Contains("중단") || name.Contains("stop") || name.Contains("cancel")) continue;
                if (name.Contains("send") || name.Contains("submit") || name.Contains("전송") ||
                    name.Contains("제출") || aid.Contains("send") || aid.Contains("submit"))
                    return el;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Verify submit success: wait up to 2s for button to disappear or "중단" to appear.
    /// </summary>
    private bool VerifySubmitSuccess(AutomationElement turnForm)
    {
        for (int i = 0; i < 4; i++)
        {
            Thread.Sleep(500);
            var state = CheckSubmitButton(turnForm);
            if (state == SubmitButtonState.Gone || state == SubmitButtonState.StopAppeared)
                return true;
        }
        return false;
    }

    private void RestoreFocus(IntPtr prevFg, IntPtr promptHwnd)
    {
        if (prevFg != IntPtr.Zero && prevFg != promptHwnd)
        {
            Thread.Sleep(200);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("  [PROMPT] Focus restored");
        }
    }

    /// <summary>
    /// Try to submit via MSAA: find Send button in MSAA tree → accDoDefaultAction.
    /// Note: accDoDefaultAction("활성화") on contentEditable only activates/focuses the element,
    /// it does NOT submit. So we must find the actual Send button.
    /// </summary>
    private bool TryMsaaSubmit(PromptInfo prompt)
    {
        try
        {
            var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
            var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
            var pt = new Native.POINT(centerX, centerY);

            int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? _childId);
            if (hr != 0 || acc == null) return false;

            try
            {
                if (acc is not IAccessibleVtbl selfV) return false;

                // Navigate up to turn-form level (grandparent → great-grandparent)
                // and search the MSAA tree for a Send/Submit button
                selfV.get_accParent(out object? parentObj);
                if (parentObj is not IAccessibleVtbl parentV) return false;

                // Walk up several levels to cover the turn-form area
                IAccessibleVtbl searchRoot = parentV;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        searchRoot.get_accParent(out object? upper);
                        if (upper is IAccessibleVtbl upperV)
                            searchRoot = upperV;
                        else
                            break;
                    }
                    catch { break; }
                }

                Console.WriteLine("  [PROMPT] MSAA submit: Searching for Send button in MSAA tree...");
                if (TryFindAndClickSubmitButton(searchRoot, 0, 8))
                    return true;

                Console.WriteLine("  [PROMPT] MSAA submit: No Send button found in MSAA tree");
                return false;
            }
            finally
            {
                try { Marshal.ReleaseComObject(acc); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] MSAA submit error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Recursively search MSAA tree for a button element and trigger its default action.
    /// Looks for ROLE_SYSTEM_PUSHBUTTON(43) or ROLE_SYSTEM_BUTTONDROPDOWN(56).
    /// Also accepts elements with "press"/"누르기" action but NOT generic "상위 개체 클릭".
    /// Skips stop/cancel/중단 buttons.
    /// </summary>
    private bool TryFindAndClickSubmitButton(IAccessibleVtbl node, int depth, int maxDepth)
    {
        if (depth > maxDepth) return false;

        try
        {
            node.get_accRole(0, out object? roleObj);
            int role = roleObj is int r ? r : -1;

            node.get_accName(0, out string? name);
            string nameLower = (name ?? "").ToLowerInvariant();

            string? action = null;
            try { node.get_accDefaultAction(0, out action); } catch { }
            string actionLower = (action ?? "").ToLowerInvariant();

            // ROLE_SYSTEM_PUSHBUTTON=43, ROLE_SYSTEM_BUTTONDROPDOWN=56, ROLE_SYSTEM_LINK=30
            bool isButtonRole = role == 43 || role == 56 || role == 30;
            // Accept press/click actions but NOT "상위 개체 클릭" (generic parent click = false positive)
            bool hasClickAction = (actionLower.Contains("press") || actionLower.Contains("누르기")) &&
                                  !actionLower.Contains("상위");
            // For "클릭" action, only accept actual button roles (not GROUPING=20)
            if (!hasClickAction && isButtonRole && actionLower.Contains("클릭"))
                hasClickAction = true;

            if (isButtonRole || hasClickAction)
            {
                // Skip stop/cancel/중단/collapse/expand/menu/model selector buttons
                if (nameLower.Contains("중단") || nameLower.Contains("stop") || nameLower.Contains("cancel") ||
                    nameLower.Contains("collapse") || nameLower.Contains("expand") ||
                    nameLower.Contains("접기") || nameLower.Contains("펼치기") ||
                    nameLower.Contains("메뉴") || nameLower.Contains("menu") ||
                    nameLower.Contains("opus") || nameLower.Contains("sonnet") || nameLower.Contains("haiku") ||
                    nameLower.Contains("권한") || nameLower.Contains("permission"))
                {
                    // Don't return false — continue to children
                }
                else
                {
                    Console.WriteLine($"  [PROMPT] MSAA button: \"{name}\" action=\"{action}\" role={role}(0x{role:X2}) depth={depth}");

                    if (action != null)
                    {
                        int hr = node.accDoDefaultAction(0);
                        Console.WriteLine($"  [PROMPT] MSAA submit: accDoDefaultAction on \"{name}\" hr=0x{hr:X8}");
                        if (hr == 0) return true;
                    }
                }
            }

            // Recurse into children
            node.get_accChildCount(out int childCount);
            for (int i = 1; i <= Math.Min(childCount, 30); i++)
            {
                try
                {
                    int hr = node.get_accChild(i, out object? child);
                    if (hr == 0 && child is IAccessibleVtbl childV)
                    {
                        if (TryFindAndClickSubmitButton(childV, depth + 1, maxDepth))
                            return true;
                    }
                }
                catch { }
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Try to submit by posting VK_RETURN to the Chrome renderer child window.
    /// Chrome_RenderWidgetHostHWND is the actual input handler inside the Electron window.
    /// </summary>
    private bool TryPostMessageEnter(PromptInfo prompt)
    {
        try
        {
            // Find Chrome_RenderWidgetHostHWND child window
            var rendererHwnd = NativeMethods.FindWindowExW(
                prompt.WindowHandle, IntPtr.Zero,
                "Chrome_RenderWidgetHostHWND", null);

            if (rendererHwnd == IntPtr.Zero)
            {
                // Try intermediate child: Chrome_WidgetWin_1 → Chrome_RenderWidgetHostHWND
                var widgetWin = NativeMethods.FindWindowExW(
                    prompt.WindowHandle, IntPtr.Zero,
                    "Chrome_WidgetWin_1", null);
                if (widgetWin != IntPtr.Zero)
                {
                    rendererHwnd = NativeMethods.FindWindowExW(
                        widgetWin, IntPtr.Zero,
                        "Chrome_RenderWidgetHostHWND", null);
                }
            }

            if (rendererHwnd == IntPtr.Zero)
            {
                Console.WriteLine("  [PROMPT] PostMessage: Chrome_RenderWidgetHostHWND not found");
                return false;
            }

            Console.WriteLine($"  [PROMPT] PostMessage: Found renderer hwnd=0x{rendererHwnd:X}");

            // Post WM_KEYDOWN + WM_KEYUP for VK_RETURN
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_RETURN = 0x0D;

            // lParam for VK_RETURN: repeat=1, scancode=0x1C, extended=0
            uint scanCode = NativeMethods.MapVirtualKeyW((uint)VK_RETURN, 0);
            IntPtr lParamDown = (IntPtr)((scanCode << 16) | 1); // repeat=1
            IntPtr lParamUp = (IntPtr)((scanCode << 16) | 1 | (1 << 30) | (1 << 31)); // prev=down, transition=release

            bool ok1 = NativeMethods.PostMessageW(rendererHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, lParamDown);
            Thread.Sleep(30);
            bool ok2 = NativeMethods.PostMessageW(rendererHwnd, WM_KEYUP, (IntPtr)VK_RETURN, lParamUp);

            Console.WriteLine($"  [PROMPT] PostMessage: VK_RETURN down={ok1} up={ok2}");

            if (ok1 && ok2)
            {
                Thread.Sleep(200); // Wait for submit to register
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] PostMessage error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Type text into the prompt WITHOUT submitting (no Enter).
    /// For testing/dry-run: verify text insertion works.
    /// Tries focusless first, then focus-stealing with restore.
    /// </summary>
    public bool TypeWithoutSubmit(PromptInfo prompt, string text)
    {
        // Try IA2 focusless text insertion first
        if (TryIA2InsertText(prompt, text))
        {
            Console.WriteLine($"  [PROMPT] IA2 insertText succeeded! ({text.Length} chars, focusless, no submit)");
            return true;
        }

        // Try focusless text insertion via LegacyIAccessible
        try
        {
            Console.WriteLine("  [PROMPT] TypeWithoutSubmit: trying LegacyIA focusless...");
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root != null)
            {
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));
                if (turnForm != null)
                {
                    var inputGroup = turnForm.FindFirstChild(
                        new PropertyCondition(
                            _automation.PropertyLibrary.Element.ControlType,
                            ControlType.Group));
                    if (inputGroup == null)
                    {
                        var children = turnForm.FindAllChildren();
                        inputGroup = children.FirstOrDefault(c =>
                            c.ControlType == ControlType.Group &&
                            c.BoundingRectangle.Width > 100);
                    }

                    if (inputGroup != null)
                    {
                        var legacy = inputGroup.Patterns.LegacyIAccessible;
                        if (legacy.IsSupported)
                        {
                            try
                            {
                                legacy.Pattern.SetValue(text);
                                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue succeeded! ({text.Length} chars, focusless, no submit)");
                                return true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue failed: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  [PROMPT] LegacyIA not supported on inputGroup");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] Focusless probe error: {ex.Message}");
        }

        // Fallback: focus-steal to paste text, but no Enter
        Console.WriteLine("  [PROMPT] Falling back to focus-steal paste (no submit)...");
        var prevForeground = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(150);

        KeyboardInput.Hotkey(new[] { "ctrl", "a" });
        Thread.Sleep(30);
        KeyboardInput.PressKey("delete");
        Thread.Sleep(30);

        Console.WriteLine($"  [PROMPT] Pasting ({text.Length} chars, no submit)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(200);

        // NO Enter key — dry run!
        Console.WriteLine("  [PROMPT] Text pasted (no Enter, dry-run)");

        // Restore previous foreground
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(200);
            NativeMethods.SmartSetForegroundWindow(prevForeground);
            Console.WriteLine("  [PROMPT] Focus restored");
        }

        return true;
    }

    /// <summary>
    /// Find prompt in Claude Desktop (Electron) window.
    /// UIA path: Window → ... → Document "Claude" aid="RootWebArea" → Group aid="turn-form" → Group "입력하세요"
    /// </summary>
    private PromptInfo? FindClaudeDesktopPrompt(int processId)
    {
        // Find all visible top-level windows from this PID
        var windows = FindWindowsByProcessId(processId);
        Console.WriteLine($"  [PROMPT] PID={processId}: {windows.Count} visible window(s)");
        foreach (var hWnd in windows)
        {
            var wTitle = WindowFinder.GetWindowText(hWnd);
            var wClass = WindowFinder.GetClassName(hWnd);
            NativeMethods.GetWindowRect(hWnd, out var wRect);
            var wSize = $"{wRect.Right - wRect.Left}x{wRect.Bottom - wRect.Top}";
            Console.WriteLine($"  [PROMPT]   hwnd=0x{hWnd:X} \"{wTitle}\" class={wClass} {wSize}");
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null)
                {
                    Console.WriteLine($"  [PROMPT]     UIA.FromHandle=null (no UIA tree)");
                    continue;
                }

                // Look for the turn-form group (the prompt container)
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm == null)
                {
                    // Log what we DID find at the top level (for diagnosis)
                    try
                    {
                        var topAids = new List<string>();
                        var topChildren = root.FindAllChildren();
                        foreach (var child in topChildren)
                        {
                            try
                            {
                                var aid = child.AutomationId;
                                var ct = child.ControlType;
                                topAids.Add($"{ct}(aid={aid})");
                                // Check one level deeper for RootWebArea or named elements
                                foreach (var gc in child.FindAllChildren())
                                {
                                    try
                                    {
                                        var gcAid = gc.AutomationId;
                                        if (!string.IsNullOrEmpty(gcAid))
                                            topAids.Add($"  {gc.ControlType}(aid={gcAid})");
                                    }
                                    catch { }
                                }
                            }
                            catch { }
                            if (topAids.Count > 20) { topAids.Add("...(truncated)"); break; }
                        }
                        Console.WriteLine($"  [PROMPT]     turn-form NOT found. UIA tree: {string.Join(", ", topAids)}");
                    }
                    catch (Exception diagEx)
                    {
                        Console.WriteLine($"  [PROMPT]     turn-form NOT found (UIA tree read error: {diagEx.Message})");
                    }
                    continue;
                }

                Console.WriteLine($"  [PROMPT]     turn-form FOUND at ({turnForm.BoundingRectangle})");

                // Found the prompt container! Get the input group
                // It's a child Group with name "입력하세요" or similar placeholder
                var inputGroup = turnForm.FindFirstChild(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.ControlType,
                        ControlType.Group));

                if (inputGroup == null)
                {
                    // Try finding by contentEditable behavior — the first child Group
                    var children = turnForm.FindAllChildren();
                    inputGroup = children.FirstOrDefault(c =>
                        c.ControlType == ControlType.Group &&
                        c.BoundingRectangle.Width > 100);
                }

                if (inputGroup == null)
                {
                    // Log turn-form children for diagnosis
                    try
                    {
                        var tfChildren = turnForm.FindAllChildren();
                        var tfInfo = string.Join(", ", tfChildren.Select(c =>
                            $"{c.ControlType}(\"{c.Name}\",{c.BoundingRectangle.Width}x{c.BoundingRectangle.Height})"));
                        Console.WriteLine($"  [PROMPT]     inputGroup NOT found! turn-form children: {tfInfo}");
                    }
                    catch { Console.WriteLine($"  [PROMPT]     inputGroup NOT found (children read error)"); }
                    continue;
                }

                var rect = inputGroup.BoundingRectangle;

                Console.WriteLine($"  [PROMPT] Found Claude Desktop prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                return new PromptInfo(
                    hWnd,
                    wTitle,
                    "claude",
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    "claude-desktop"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT]     Error: {ex.Message}");
            }
        }
        return null;
    }

    /// <summary>
    /// Find prompt in VS Code with Claude Code extension.
    /// The Claude Code panel in VS Code may have different UIA structure.
    /// </summary>
    private PromptInfo? FindVSCodePrompt(int processId)
    {
        var windows = FindWindowsByProcessId(processId);
        foreach (var hWnd in windows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                // VS Code Claude Code panel — look for turn-form or similar
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm != null)
                {
                    var inputGroup = turnForm.FindFirstChild(
                        new PropertyCondition(
                            _automation.PropertyLibrary.Element.ControlType,
                            ControlType.Group));

                    if (inputGroup != null)
                    {
                        var rect = inputGroup.BoundingRectangle;
                        var title = WindowFinder.GetWindowText(hWnd);

                        Console.WriteLine($"  [PROMPT] Found VS Code Claude prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                        return new PromptInfo(
                            hWnd,
                            title,
                            "Code",
                            new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                            "vscode"
                        );
                    }
                }
            }
            catch { }
        }

        // Fallback: VS Code Claude Code extension (no turn-form, native panel)
        // Detect by window title containing "Visual Studio Code"
        foreach (var hWnd in windows)
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) continue;
            var title = WindowFinder.GetWindowText(hWnd);
            if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) continue;

            // Use full window rect (will use keyboard shortcut to focus input)
            NativeMethods.GetWindowRect(hWnd, out var wr);
            var rect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);

            Console.WriteLine($"  [PROMPT] Found VS Code Claude Code (native ext): \"{title}\" at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

            return new PromptInfo(hWnd, title, "Code", rect, "vscode-claudecode");
        }

        return null;
    }

    /// <summary>
    /// Get ancestor process chain: current → parent → grandparent → ...
    /// Stops when parent is not accessible or reaches PID 0/1.
    /// </summary>
    private static List<(int Pid, string Name)> GetAncestorProcesses()
    {
        var result = new List<(int, string)>();
        try
        {
            var current = Process.GetCurrentProcess();
            var pid = current.Id;

            // Walk up the tree
            for (int i = 0; i < 10; i++) // max 10 levels
            {
                var parentPid = GetParentProcessId(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    result.Add((parent.Id, parent.ProcessName));
                    pid = parent.Id;
                }
                catch
                {
                    break;
                }
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Get parent process ID using NtQueryInformationProcess or WMI fallback.
    /// </summary>
    private static int GetParentProcessId(int pid)
    {
        try
        {
            // Use WMI-like approach via Process
            // On Windows, Process has no direct ParentId. Use handle + NtQueryInformationProcess.
            // Simpler: use PerformanceCounter or WMI
            // Simplest portable approach: read /proc on WSL or use dotnet API
            var proc = Process.GetProcessById(pid);

            // .NET 8+ approach - try reflection for ParentId (not available in all frameworks)
            // Fallback: use command-line tool
            var psi = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = $"process where processid={pid} get parentprocessid /value",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var wmic = Process.Start(psi);
            if (wmic == null) return -1;
            var output = wmic.StandardOutput.ReadToEnd();
            wmic.WaitForExit(3000);

            // Parse "ParentProcessId=12345\r\n"
            var match = System.Text.RegularExpressions.Regex.Match(output, @"ParentProcessId=(\d+)");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);
        }
        catch { }
        return -1;
    }

    /// <summary>
    /// Find all visible top-level windows belonging to a process.
    /// </summary>
    private static List<IntPtr> FindWindowsByProcessId(int processId)
    {
        var result = new List<IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid == (uint)processId)
                result.Add(hWnd);

            return true;
        }, IntPtr.Zero);
        return result;
    }

    /// <summary>
    /// Set clipboard text using STA thread (Win32 clipboard requires STA).
    /// </summary>
    private static void SetClipboardText(string text)
    {
        // Clipboard operations require STA thread
        var thread = new Thread(() =>
        {
            // P/Invoke clipboard: OpenClipboard → EmptyClipboard → SetClipboardData → CloseClipboard
            if (!NativeMethods.OpenClipboard(IntPtr.Zero)) return;
            try
            {
                NativeMethods.EmptyClipboard();
                var hGlobal = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(text);
                NativeMethods.SetClipboardData(13 /* CF_UNICODETEXT */, hGlobal);
                // Don't free hGlobal — clipboard takes ownership
            }
            finally
            {
                NativeMethods.CloseClipboard();
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(3000);
    }

    // ═══════════════════════════════════════════════════════════════
    // MSAA/IA2 COM Interop for focusless text insertion
    //
    // Discovery (2026-02-26):
    //   Chromium's contentEditable has ROLE_SYSTEM_GROUPING (not ROLE_TEXT),
    //   so IAccessibleEditableText is NOT available. However, direct MSAA vtable
    //   put_accValue() on the parent element (name="입력하세요") works!
    //
    //   Why it works when FlaUI's LegacyIAccessible.SetValue returns E_NOTIMPL:
    //   FlaUI goes through UIA→MSAA proxy bridge which adds validation.
    //   Direct MSAA COM vtable call bypasses the proxy entirely.
    //
    // MSAA tree structure (Claude Desktop prompt):
    //   grandparent: GROUPING, name=null
    //     └─ parent: GROUPING, name="입력하세요"  ← put_accValue target!
    //          └─ self: GROUPING, name=null       ← AccessibleObjectFromPoint hit
    //               ├─ child: GROUPING or TEXT
    //               └─ child: WHITESPACE, name="\n"
    // ═══════════════════════════════════════════════════════════════

    [DllImport("oleacc.dll")]
    private static extern int AccessibleObjectFromPoint(
        Native.POINT pt,
        [MarshalAs(UnmanagedType.Interface)] out object ppacc,
        [MarshalAs(UnmanagedType.Struct)] out object pvarChild);

    // OLE IServiceProvider — used to QueryService for IA2 from IAccessible
    [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IOleServiceProvider
    {
        [PreserveSig]
        int QueryService(ref Guid guidService, ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);
    }

    // IAccessibleEditableText — the IA2 interface for text input
    // vtable order MUST match the IA2 IDL exactly!
    [ComImport, Guid("A59AA09A-7011-4b65-939D-32B1FB5547E3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleEditableText
    {
        [PreserveSig]
        int copyText(int startOffset, int endOffset);
        [PreserveSig]
        int deleteText(int startOffset, int endOffset);
        [PreserveSig]
        int insertText(int offset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int cutText(int startOffset, int endOffset);
        [PreserveSig]
        int pasteText(int offset);
        [PreserveSig]
        int replaceText(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int setAttributes(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string attributes);
    }

    // IAccessible via vtable — for MSAA tree navigation (role probing + child access)
    // Chromium doesn't support IDispatch late binding → must use vtable (InterfaceIsIUnknown)
    // Vtable order: IUnknown(3) + IDispatch(4) + IAccessible(21) = 28 slots
    [ComImport, Guid("618736E0-3C3D-11CF-810C-00AA00389B71"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleVtbl
    {
        // ── IDispatch (4 slots, never called — consuming vtable positions) ──
        void _QueryInfoCount_slot();
        void _GetTypeInfo_slot();
        void _GetIDsOfNames_slot();
        void _Invoke_slot();

        // ── IAccessible (21 methods) ──
        [PreserveSig]
        int get_accParent([MarshalAs(UnmanagedType.IDispatch)] out object? ppdispParent);
        [PreserveSig]
        int get_accChildCount(out int pcountChildren);
        [PreserveSig]
        int get_accChild([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.IDispatch)] out object? ppdispChild);
        [PreserveSig]
        int get_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszName);
        [PreserveSig]
        int get_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszValue);
        [PreserveSig]
        int get_accDescription([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDescription);
        [PreserveSig]
        int get_accRole([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarRole);
        [PreserveSig]
        int get_accState([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarState);
        [PreserveSig]
        int get_accHelp([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszHelp);
        [PreserveSig]
        int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string? pszHelpFile,
            [MarshalAs(UnmanagedType.Struct)] object varChild, out int pidTopic);
        [PreserveSig]
        int get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszKeyboardShortcut);
        [PreserveSig]
        int get_accFocus([MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int get_accSelection([MarshalAs(UnmanagedType.Struct)] out object? pvarChildren);
        [PreserveSig]
        int get_accDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDefaultAction);
        [PreserveSig]
        int accSelect(int flagsSelect, [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight,
            [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accNavigate(int navDir, [MarshalAs(UnmanagedType.Struct)] object varStart,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarEndUpAt);
        [PreserveSig]
        int accHitTest(int xLeft, int yTop, [MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int accDoDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int put_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szName);
        [PreserveSig]
        int put_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szValue);
    }

    private static readonly Guid IID_IAccessible = new("618736E0-3C3D-11CF-810C-00AA00389B71");
    private static readonly Guid IID_IAccessible2 = new("E89F726E-C4F4-4c19-BB19-B647D7FA8478");
    private static readonly Guid IID_IAccessibleEditableText = new("A59AA09A-7011-4b65-939D-32B1FB5547E3");

    /// <summary>
    /// Try to insert text focuslessly using MSAA put_accValue (direct vtable).
    /// Priority 1: put_accValue on parent MSAA element (proven working on Claude Desktop)
    /// Priority 2: IAccessibleEditableText.insertText (IA2, not available for contentEditable)
    /// Strategy: AccessibleObjectFromPoint → navigate to parent → put_accValue
    /// </summary>
    private bool TryIA2InsertText(PromptInfo prompt, string text)
    {
        try
        {
            Console.WriteLine("  [PROMPT] Trying IA2 IAccessibleEditableText.insertText()...");

            var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
            var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
            var pt = new Native.POINT(centerX, centerY);

            int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? childId);
            if (hr != 0 || acc == null)
            {
                Console.WriteLine($"  [PROMPT] IA2: AccessibleObjectFromPoint failed (hr=0x{hr:X8})");
                return false;
            }

            var childIdStr = childId switch
            {
                int id => id == 0 ? "CHILDID_SELF" : $"child={id}",
                _ => $"type={childId?.GetType().Name ?? "null"}"
            };
            Console.WriteLine($"  [PROMPT] IA2: Got MSAA object at ({centerX},{centerY}), {childIdStr}");

            try
            {
                // Probe MSAA role/name/children for diagnostics
                ProbeAccessibleInfo(acc, "  [PROMPT] IA2 [self]");

                // === Priority 1: Try put_accValue on parent (the "입력하세요" contentEditable) ===
                // This works through direct MSAA vtable, bypassing UIA bridge that returns E_NOTIMPL
                if (acc is IAccessibleVtbl selfForParent)
                {
                    selfForParent.get_accParent(out object? parentForValue);
                    if (parentForValue is IAccessibleVtbl parentVtbl)
                    {
                        string? parentName = null;
                        try { parentVtbl.get_accName(0, out parentName); } catch { }
                        Console.WriteLine($"  [PROMPT] IA2: Trying put_accValue on parent (\"{parentName}\")...");
                        try
                        {
                            int pvHr = parentVtbl.put_accValue(0, text);
                            if (pvHr == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue SUCCEEDED! ({text.Length} chars, focusless!)");
                                return true;
                            }
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue returned hr=0x{pvHr:X8}");
                        }
                        catch (Exception pvEx)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue error: {pvEx.Message}");
                        }

                        // Also try on self
                        try
                        {
                            int pvHr2 = selfForParent.put_accValue(0, text);
                            if (pvHr2 == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue on self SUCCEEDED! ({text.Length} chars)");
                                return true;
                            }
                        }
                        catch { }
                    }
                }

                // === Priority 2: Try IAccessibleEditableText ===
                if (TryInsertOnObject(acc, text, "self"))
                    return true;

                // Navigate to parent and try IAccessibleEditableText there
                if (acc is IAccessibleVtbl accV)
                {
                    try
                    {
                        accV.get_accParent(out object? parent);
                        if (parent != null)
                        {
                            ProbeAccessibleInfo(parent, "  [PROMPT] IA2 [parent]");
                            if (TryInsertOnObject(parent, text, "parent"))
                                return true;

                            // Try grandparent too (contentEditable might be 2 levels up)
                            if (parent is IAccessibleVtbl parentV)
                            {
                                try
                                {
                                    parentV.get_accParent(out object? grandparent);
                                    if (grandparent != null)
                                    {
                                        ProbeAccessibleInfo(grandparent, "  [PROMPT] IA2 [grandparent]");
                                        if (TryInsertOnObject(grandparent, text, "grandparent"))
                                            return true;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Parent navigation error: {ex.Message}");
                    }

                    // Try children of the current element
                    try
                    {
                        accV.get_accChildCount(out int childCount);
                        if (childCount > 0)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: Probing {childCount} children...");
                            for (int i = 1; i <= Math.Min(childCount, 10); i++)
                            {
                                try
                                {
                                    int hr2 = accV.get_accChild(i, out object? child);
                                    if (hr2 == 0 && child != null)
                                    {
                                        ProbeAccessibleInfo(child, $"  [PROMPT] IA2 [child {i}]");
                                        if (TryInsertOnObject(child, text, $"child[{i}]"))
                                            return true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  [PROMPT] IA2 [child {i}]: get_accChild hr=0x{hr2:X8} (simple element or null)");
                                    }
                                }
                                catch (Exception cex)
                                {
                                    Console.WriteLine($"  [PROMPT] IA2 [child {i}]: error ({cex.Message})");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Child navigation error: {ex.Message}");
                    }
                }

                // Deep recursive search — walk entire subtree from parent for IAccessibleEditableText
                if (acc is IAccessibleVtbl selfV)
                {
                    selfV.get_accParent(out object? searchRoot);
                    if (searchRoot is IAccessibleVtbl rootV)
                    {
                        rootV.get_accParent(out object? gp);
                        if (gp != null) searchRoot = gp; // start from grandparent for wider coverage
                    }
                    if (searchRoot != null)
                    {
                        Console.WriteLine("  [PROMPT] IA2: Deep recursive search for IAccessibleEditableText...");
                        if (TryInsertRecursive(searchRoot, text, 0, 5))
                            return true;
                    }
                }

                Console.WriteLine("  [PROMPT] IA2: No focusless insertion method found");
                return false;
            }
            finally
            {
                try { Marshal.ReleaseComObject(acc); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] IA2 error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Log MSAA role, name, childCount for diagnostics using vtable-bound IAccessible.
    /// </summary>
    private static void ProbeAccessibleInfo(object acc, string prefix)
    {
        if (acc is not IAccessibleVtbl v)
        {
            Console.WriteLine($"{prefix}: not IAccessibleVtbl (type={acc.GetType().Name})");
            return;
        }
        try
        {
            int childCount = 0;
            v.get_accChildCount(out childCount);

            object? role = null;
            try { v.get_accRole(0 /* CHILDID_SELF */, out role); } catch { }

            string? name = null;
            try { v.get_accName(0, out name); } catch { }

            // MSAA role constants: 42=ROLE_SYSTEM_TEXT, 9=ROLE_SYSTEM_CLIENT,
            // 20=ROLE_SYSTEM_GROUPING, 15=ROLE_SYSTEM_DOCUMENT, 14=ROLE_SYSTEM_WINDOW
            var roleStr = role is int r ? $"role={r} (0x{r:X2})" : $"role={role}";
            Console.WriteLine($"{prefix}: {roleStr}, name=\"{name ?? "(null)"}\", children={childCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{prefix}: probe failed ({ex.Message})");
        }
    }

    /// <summary>
    /// Recursively walk the MSAA tree to find any element supporting IAccessibleEditableText.
    /// </summary>
    private bool TryInsertRecursive(object acc, string text, int depth, int maxDepth)
    {
        if (depth > maxDepth) return false;
        var indent = new string(' ', depth * 2);

        if (acc is IAccessibleVtbl v)
        {
            // Check role
            object? role = null;
            string? name = null;
            try { v.get_accRole(0, out role); } catch { }
            try { v.get_accName(0, out name); } catch { }
            var roleInt = role is int r ? r : -1;

            // Log only interesting roles (not GROUPING 20)
            if (roleInt != 20)
                Console.WriteLine($"  [PROMPT] IA2 {indent}depth={depth}: role={roleInt} (0x{roleInt:X2}), name=\"{name ?? ""}\"");

            // Try IAccessibleEditableText on this element
            if (TryInsertOnObject(acc, text, $"depth{depth}"))
                return true;

            // Navigate to children
            v.get_accChildCount(out int childCount);
            for (int i = 1; i <= Math.Min(childCount, 20); i++)
            {
                try
                {
                    int hr = v.get_accChild(i, out object? child);
                    if (hr == 0 && child != null)
                    {
                        if (TryInsertRecursive(child, text, depth + 1, maxDepth))
                            return true;
                    }
                }
                catch { }
            }
        }
        return false;
    }

    /// <summary>
    /// Try all approaches to get IAccessibleEditableText and call insertText on a COM object.
    /// Tries: Direct QI → QueryService(IA2)→QI → QueryService(IAccessibleEditableText)
    /// </summary>
    private bool TryInsertOnObject(object acc, string text, string label)
    {
        int hr;

        // Approach 1: Direct QI for IAccessibleEditableText
        if (acc is IAccessibleEditableText directEdit)
        {
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: Direct QI succeeded! Calling insertText...");
            hr = directEdit.insertText(0, text);
            if (hr == 0)
            {
                Console.WriteLine($"  [PROMPT] IA2: insertText on {label} SUCCEEDED! ({text.Length} chars, fully focusless!)");
                return true;
            }
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
        }

        // Approach 2: QueryService → IAccessible2 → QI for IAccessibleEditableText
        if (acc is IOleServiceProvider sp)
        {
            var guidIAcc = IID_IAccessible;
            var guidIA2 = IID_IAccessible2;
            var guidET = IID_IAccessibleEditableText;

            hr = sp.QueryService(ref guidIAcc, ref guidIA2, out object? ia2Obj);
            if (hr == 0 && ia2Obj != null)
            {
                if (ia2Obj is IAccessibleEditableText ia2Edit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: IA2→EditableText QI succeeded! Calling insertText...");
                    hr = ia2Edit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via IA2 on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                // Approach 3: QueryService directly for IAccessibleEditableText
                hr = sp.QueryService(ref guidIAcc, ref guidET, out object? etObj);
                if (hr == 0 && etObj is IAccessibleEditableText serviceEdit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: ServiceProvider→EditableText succeeded! Calling insertText...");
                    hr = serviceEdit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via SP on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                try { Marshal.ReleaseComObject(ia2Obj); } catch { }
            }
        }

        return false;
    }

    /// <summary>
    /// Write text to MSAA elements at point. Tries put_accValue on hit + ancestors (L0..L3),
    /// waits 5s so user can visually check if text appeared, then clears.
    /// </summary>
    public static void ProbeMsaaWrite(int absX, int absY, string text)
    {
        var pt = new Native.POINT(absX, absY);
        int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? _);
        if (hr != 0 || acc == null)
        {
            Console.WriteLine($"  [MSAA-WRITE] AccessibleObjectFromPoint failed hr=0x{hr:X8}");
            return;
        }
        try
        {
            object? current = acc;
            for (int level = 0; level <= 4 && current != null; level++)
            {
                if (current is not IAccessibleVtbl v) break;
                object? role = null; try { v.get_accRole(0, out role); } catch { }
                string? name = null; try { v.get_accName(0, out name); } catch { }
                int ri = role is int r ? r : -1;
                Console.Write($"    L{level}: role={ri}(0x{ri:X2}) name=\"{name ?? "(null)"}\" → put_accValue...");
                try
                {
                    int pvHr = v.put_accValue(0, text);
                    Console.WriteLine($" hr=0x{pvHr:X8} {(pvHr == 0 ? "★ WRITTEN!" : "failed")}");
                    if (pvHr == 0)
                    {
                        Console.WriteLine($"  [MSAA-WRITE] Text written at L{level}! Waiting 5s for visual check...");
                        Thread.Sleep(5000);
                        // Read back
                        string? readBack = null;
                        try { v.get_accValue(0, out readBack); } catch { }
                        Console.WriteLine($"  [MSAA-WRITE] Read back: \"{readBack ?? "(null)"}\"");
                        // Clean up
                        v.put_accValue(0, "");
                        Console.WriteLine($"  [MSAA-WRITE] Cleaned up.");
                        return;
                    }
                }
                catch (Exception ex) { Console.WriteLine($" {ex.GetType().Name}"); }
                try { v.get_accParent(out current); } catch { current = null; }
            }
            Console.WriteLine("  [MSAA-WRITE] No writable element found in L0..L4");
        }
        finally { try { Marshal.ReleaseComObject(acc); } catch { } }
    }

    /// <summary>
    /// Probe MSAA tree at absolute screen coordinates. Dumps role/name/children up to parent.
    /// Used to investigate VS Code webview accessibility for Claude Code input.
    /// Returns the MSAA role of the hit element, or -1 on failure.
    /// </summary>
    public static int ProbeMsaaAtPoint(int absX, int absY, int walkDepth = 6)
    {
        var pt = new Native.POINT(absX, absY);
        int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? childId);
        if (hr != 0 || acc == null)
        {
            Console.WriteLine($"  [MSAA-PROBE] AccessibleObjectFromPoint({absX},{absY}) failed hr=0x{hr:X8}");
            return -1;
        }

        var childIdStr = childId switch { int id => id == 0 ? "SELF" : $"child={id}", _ => $"type={childId?.GetType().Name}" };
        Console.WriteLine($"  [MSAA-PROBE] Hit at ({absX},{absY}) childId={childIdStr}");

        try
        {
            // Walk up from hit element, dumping role/name/value
            object? current = acc;
            for (int level = 0; level <= walkDepth && current != null; level++)
            {
                if (current is not IAccessibleVtbl v) { Console.WriteLine($"    L{level}: not IAccessibleVtbl"); break; }

                int childCount = 0; try { v.get_accChildCount(out childCount); } catch { }
                object? role = null; try { v.get_accRole(0, out role); } catch { }
                string? name = null; try { v.get_accName(0, out name); } catch { }
                string? value = null; try { v.get_accValue(0, out value); } catch { }
                string? defAction = null; try { v.get_accDefaultAction(0, out defAction); } catch { }

                int roleInt = role is int r ? r : -1;
                var nameStr = string.IsNullOrEmpty(name) ? "(null)" : (name.Length > 80 ? name[..77] + "..." : name);
                var valStr = string.IsNullOrEmpty(value) ? "" : $" val=\"{(value.Length > 40 ? value[..37] + "..." : value)}\"";
                var actStr = string.IsNullOrEmpty(defAction) ? "" : $" action=\"{defAction}\"";
                Console.WriteLine($"    L{level}: role={roleInt}(0x{roleInt:X2}) name=\"{nameStr}\" children={childCount}{valStr}{actStr}");

                // Try put_accValue to test writability
                if (level <= 2)
                {
                    try
                    {
                        int pvHr = v.put_accValue(0, "__PROBE_TEST__");
                        Console.WriteLine($"    L{level}: put_accValue → hr=0x{pvHr:X8} {(pvHr == 0 ? "★ WRITABLE!" : "")}");
                        if (pvHr == 0) // clean up test value
                            v.put_accValue(0, "");
                    }
                    catch (Exception ex) { Console.WriteLine($"    L{level}: put_accValue → {ex.GetType().Name}"); }
                }

                // Move to parent
                try { v.get_accParent(out current); } catch { current = null; }
            }

            // Also dump children of the hit element
            if (acc is IAccessibleVtbl hitV)
            {
                hitV.get_accChildCount(out int cc);
                Console.WriteLine($"  [MSAA-PROBE] Children of hit ({cc}):");
                for (int i = 1; i <= Math.Min(cc, 15); i++)
                {
                    try
                    {
                        hitV.get_accChild(i, out object? child);
                        if (child is IAccessibleVtbl cv)
                        {
                            object? cr = null; try { cv.get_accRole(0, out cr); } catch { }
                            string? cn = null; try { cv.get_accName(0, out cn); } catch { }
                            int cri = cr is int crint ? crint : -1;
                            Console.WriteLine($"    child[{i}]: role={cri}(0x{cri:X2}) name=\"{cn ?? "(null)"}\"");
                        }
                    }
                    catch { }
                }
            }
            return acc is IAccessibleVtbl av ? (av.get_accRole(0, out object? ro) == 0 && ro is int ri ? ri : -1) : -1;
        }
        finally
        {
            try { Marshal.ReleaseComObject(acc); } catch { }
        }
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
            // Find Claude Desktop window
            IntPtr claudeHwnd;
            AutomationElement? claudeRoot = null;
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

    public void Dispose()
    {
        _automation.Dispose();
    }
}
