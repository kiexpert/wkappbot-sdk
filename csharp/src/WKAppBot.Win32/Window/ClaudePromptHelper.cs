using System.Diagnostics;
using System.Drawing;
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
///   4. Click the prompt to focus, then SendInput text + Enter to submit
/// </summary>
public sealed class ClaudePromptHelper : IDisposable
{
    private readonly UIA3Automation _automation;

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
        foreach (var proc in Process.GetProcessesByName("claude"))
        {
            var result = FindClaudeDesktopPrompt(proc.Id);
            if (result != null) return result;
        }

        // Strategy 3: Enumerate all VS Code windows
        Console.WriteLine("  [PROMPT] Scanning VS Code windows...");
        foreach (var proc in Process.GetProcessesByName("Code"))
        {
            var result = FindVSCodePrompt(proc.Id);
            if (result != null) return result;
        }

        return null;
    }

    /// <summary>
    /// Type text into the Claude prompt and submit with Enter.
    /// Strategy (2-tier):
    ///   1. Focusless: UIA LegacyIAccessible.SetValue (no focus steal!)
    ///   2. Fallback: SetForegroundWindow → Click → Paste → Enter → Restore previous foreground
    /// Note: Electron contentEditable may not support UIA SetValue,
    /// so fallback saves/restores the previous foreground window to minimize disruption.
    /// </summary>
    public bool TypeAndSubmit(PromptInfo prompt, string text)
    {
        // === Strategy 1: Try fully focusless input ===
        if (TryFocuslessInput(prompt, text))
            return true;

        // === Strategy 2: Focus-stealing with auto-restore (minimal disruption) ===
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
    /// Try to input text into the Claude prompt WITHOUT stealing focus.
    /// Uses UIA LegacyIAccessible.SetValue if available on the contentEditable group.
    /// Returns true if text was successfully inserted AND submitted.
    /// </summary>
    private bool TryFocuslessInput(PromptInfo prompt, string text)
    {
        try
        {
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

            // NOTE: PostMessage WM_PASTE to Chrome_RenderWidgetHostHWND was tested (2026-02-26)
            // and does NOT work — Chrome renderer ignores WM_PASTE when window is not foreground.
            // LegacyIAccessible.SetValue also returns E_NOTIMPL for Electron contentEditable.
            // True focusless input is not possible for Electron/Chrome apps.
            // The fallback (TypeAndSubmit) uses focus-steal with auto-restore to minimize disruption.

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] Focusless error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Try to submit the prompt focuslessly: UIA Invoke on send button, or brief focus for Enter.
    /// </summary>
    private bool TryFocuslessSubmit(PromptInfo prompt, AutomationElement turnForm)
    {
        Thread.Sleep(100);

        // Look for a submit/send button in turn-form with Invoke pattern
        try
        {
            var allElements = turnForm.FindAllDescendants();
            foreach (var el in allElements)
            {
                if (el.ControlType != ControlType.Button) continue;
                var name = (el.Name ?? "").ToLowerInvariant();
                var aid = (el.AutomationId ?? "").ToLowerInvariant();
                if (name.Contains("중단") || name.Contains("stop") || name.Contains("cancel")) continue;
                if (name.Contains("send") || name.Contains("submit") || name.Contains("전송") ||
                    aid.Contains("send") || aid.Contains("submit"))
                {
                    if (el.Patterns.Invoke.IsSupported)
                    {
                        el.Patterns.Invoke.Pattern.Invoke();
                        Console.WriteLine($"  [PROMPT] Submitted via UIA Invoke on \"{el.Name}\" (fully focusless!)");
                        return true;
                    }
                }
            }
        }
        catch { }

        // No submit button — brief focus steal for Enter key only
        Console.WriteLine("  [PROMPT] Text set focuslessly! Brief focus for Enter key...");
        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(100);
        KeyboardInput.PressKey("enter");
        Thread.Sleep(100);
        if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
        {
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("  [PROMPT] Focus restored after Enter");
        }
        Console.WriteLine("  [PROMPT] Submitted (text=focusless, Enter=brief focus)");
        return true;
    }

    /// <summary>
    /// Type text into the prompt WITHOUT submitting (no Enter).
    /// For testing/dry-run: verify text insertion works.
    /// Tries focusless first, then focus-stealing with restore.
    /// </summary>
    public bool TypeWithoutSubmit(PromptInfo prompt, string text)
    {
        // Try focusless text insertion via LegacyIAccessible
        try
        {
            Console.WriteLine("  [PROMPT] TypeWithoutSubmit: trying focusless...");
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
        foreach (var hWnd in windows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                // Look for the turn-form group (the prompt container)
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm == null) continue;

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

                if (inputGroup == null) continue;

                var rect = inputGroup.BoundingRectangle;
                var title = WindowFinder.GetWindowText(hWnd);

                Console.WriteLine($"  [PROMPT] Found Claude Desktop prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                return new PromptInfo(
                    hWnd,
                    title,
                    "claude",
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    "claude-desktop"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] Error checking hWnd {hWnd}: {ex.Message}");
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

    public void Dispose()
    {
        _automation.Dispose();
    }
}
