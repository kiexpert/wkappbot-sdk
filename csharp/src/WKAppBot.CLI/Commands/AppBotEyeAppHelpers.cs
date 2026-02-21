// AppBotEyeAppHelpers.cs — App Mode helper methods for AppBotEye.
// Window finding, UIA element polling at cursor, element formatting, footer updates.
// Extracted from AppBotEyeCommands.cs for readability.

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Find target app window by title substring or process name.
    /// </summary>
    static IntPtr FindAppWindow(string? title, string? processName)
    {
        // Try title match first
        if (!string.IsNullOrEmpty(title))
        {
            var wins = WindowFinder.FindByTitle(title);
            if (wins.Count > 0)
                return wins[0].Handle;
        }

        // Try process name match
        if (!string.IsNullOrEmpty(processName))
        {
            foreach (var proc in Process.GetProcessesByName(processName))
            {
                if (proc.MainWindowHandle != IntPtr.Zero)
                    return proc.MainWindowHandle;
            }
        }

        return IntPtr.Zero;
    }

    /// <summary>
    /// Poll UIA element at cursor position within the target window.
    /// Strategy 1: cursor over target window → GetElementAtPointInWindow
    /// Strategy 2: cursor outside → UIA FocusedElement (if within target window)
    /// </summary>
    static string? PollAppElement(UiaLocator uia, IntPtr hWnd, ref string? lastElemKey)
    {
        try
        {
            // Check cursor position relative to target window
            GetCursorPos(out var cursor);
            GetWindowRect(hWnd, out var winRect);

            bool cursorOverWindow =
                cursor.X >= winRect.Left && cursor.X < winRect.Right &&
                cursor.Y >= winRect.Top && cursor.Y < winRect.Bottom;

            if (!cursorOverWindow) return null; // keep showing last element

            // Strategy: element at cursor within window's UIA tree
            var elemInfo = uia.GetElementAtPointInWindow(cursor.X, cursor.Y, hWnd);
            if (elemInfo == null) return null;

            // Change detection
            var elemKey = $"{elemInfo.ControlType}|{elemInfo.AutomationId}|{elemInfo.Name}|{elemInfo.BoundsX},{elemInfo.BoundsY}";
            if (elemKey == lastElemKey) return null; // no change
            lastElemKey = elemKey;

            return FormatAppElementInfo(elemInfo);
        }
        catch { return null; }
    }

    /// <summary>
    /// Format element info for AppBotEye text overlay display.
    /// Multi-line format designed for the green text area.
    /// </summary>
    static string FormatAppElementInfo(ElementAtPointInfo info)
    {
        var sb = new StringBuilder();

        // Line 1: [ControlType] "Name" aid="id"
        sb.Append($"[{info.ControlType}]");
        if (!string.IsNullOrEmpty(info.Name))
        {
            var displayName = info.Name.Length > 40 ? info.Name[..37] + "..." : info.Name;
            sb.Append($" \"{displayName}\"");
        }
        sb.AppendLine();

        // Line 2: AutomationId
        if (!string.IsNullOrEmpty(info.AutomationId))
            sb.AppendLine($"aid=\"{info.AutomationId}\"");

        // Line 3: Patterns
        if (info.Patterns.Count > 0)
            sb.AppendLine($"({string.Join(", ", info.Patterns)})");

        // Line 4: Value (if present)
        if (!string.IsNullOrEmpty(info.Value))
        {
            var displayVal = info.Value.Length > 50 ? info.Value[..47] + "..." : info.Value;
            sb.AppendLine($"Value: \"{displayVal}\"");
        }

        // Line 5: Bounds
        sb.Append($"Bounds: ({info.BoundsX},{info.BoundsY} {info.BoundsW}x{info.BoundsH})");

        return sb.ToString();
    }

    /// <summary>Update footer with app window info (title + class + PID).</summary>
    static void UpdateAppFooter(AppBotEyeHost host, IntPtr hWnd)
    {
        try
        {
            var title = GetAppWindowText(hWnd);
            var className = GetAppClassName(hWnd);
            GetWindowThreadProcessId(hWnd, out var pid);
            // Reuse UpdateInfo: first param shown in footer left, second as tooltip
            host.UpdateInfo($"{className} (PID:{pid})", title);
        }
        catch { }
    }

    /// <summary>Get window text via Win32 (safe wrapper).</summary>
    static string GetAppWindowText(IntPtr hWnd)
    {
        var sb = new StringBuilder(512);
        GetWindowTextW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    /// <summary>Get window class name via Win32 (safe wrapper).</summary>
    static string GetAppClassName(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        GetClassNameW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }
}
