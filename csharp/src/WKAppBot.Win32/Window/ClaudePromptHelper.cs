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
    /// Strategy: SetForegroundWindow → Click prompt → Clipboard paste → Enter.
    /// Note: Electron contentEditable doesn't support WM_SETTEXT/WM_PASTE focusless,
    /// so we must bring the window to foreground + use SendInput clipboard paste.
    /// </summary>
    public bool TypeAndSubmit(PromptInfo prompt, string text)
    {
        // 1. Bring the Claude window to foreground (required for Electron input)
        Console.WriteLine($"  [PROMPT] Activating: {prompt.WindowTitle}");
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(300);

        // 2. Click the prompt area to focus the contentEditable div
        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(200);

        // 3. Clear existing text
        KeyboardInput.Hotkey(new[] { "ctrl", "a" });
        Thread.Sleep(50);
        KeyboardInput.PressKey("delete");
        Thread.Sleep(50);

        // 4. Paste via clipboard (fast, supports multiline + unicode)
        Console.WriteLine($"  [PROMPT] Pasting ({text.Length} chars)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(300);

        // 5. Submit with Enter
        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT] Submitted");

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
