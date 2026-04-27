using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using FlaUI.Core.AutomationElements;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    /// <summary>
    /// Build a multi-line, paste-ready focus-node report for a window that refused to close.
    /// Shape mirrors a11y ambiguity-guard output so operators/AI can copy the suggested grap.
    /// Returns null if no focused element belongs to the target's process.
    /// </summary>
    private static string? BuildFocusedNodeReport(IntPtr targetHwnd)
    {
        try
        {
            if (targetHwnd == IntPtr.Zero) return null;

            using var focLoc = new UiaLocator();
            var foc = focLoc.GetFocusedElementInfo();
            if (foc == null) return null;

            NativeMethods.GetWindowThreadProcessId(targetHwnd, out uint winPid);
            if (foc.ProcessId != (int)winPid) return null; // focus is in a different process

            var label = !string.IsNullOrEmpty(foc.AutomationId) ? foc.AutomationId : foc.Name;
            if (label.Length > 40) label = label[..40];

            var sb = new System.Text.StringBuilder();
            sb.Append($"[CLOSE-GUARD] FOCUSED: {foc.ControlType}(\"{label}\")");
            if (foc.Patterns.Count > 0) sb.Append($" [{string.Join(",", foc.Patterns)}]");
            sb.AppendLine();
            foreach (var (pType, pName, _) in foc.ParentChain)
            {
                if (string.IsNullOrEmpty(pName)) continue;
                sb.AppendLine($"         <- {pType}(\"{pName}\")");
            }
            if (!string.IsNullOrEmpty(label) && !string.IsNullOrEmpty(foc.WindowTitle))
            {
                sb.Append($"[CLOSE-GUARD] FIND: a11y find \"*{foc.WindowTitle}*#*{label}*\"");
            }
            return sb.ToString().TrimEnd();
        }
        catch { return null; }
    }

    /// <summary>
    /// After window_close, check if the process is still alive (modal dialog blocking close).
    /// If alive, scan for dialog UIA children (buttons) and return a descriptive error
    /// so the caller/AI can decide which button to invoke.
    /// Returns null if process exited cleanly; error string if dialog detected.
    /// </summary>
    private string? DetectModalDialogAfterClose(AutomationElement closedElement)
    {
        try
        {
            // Check if the element's window still exists
            var hwnd = closedElement.Properties.NativeWindowHandle.ValueOrDefault;
            if (hwnd == IntPtr.Zero) return null;
            if (!NativeMethods.IsWindow(hwnd)) return null; // window gone -- clean close

            // Window still present -> likely a modal dialog appeared
            // Walk UIA children to find dialog + its buttons
            var buttons = new List<string>();
            try
            {
                var walker = closedElement.Automation.TreeWalkerFactory.GetControlViewWalker();
                void ScanForButtons(AutomationElement parent, int depth)
                {
                    if (depth > 4) return;
                    var child = walker.GetFirstChild(parent);
                    while (child != null)
                    {
                        try
                        {
                            var ct = child.Properties.ControlType.ValueOrDefault;
                            var name = child.Properties.Name.ValueOrDefault ?? "";
                            if (ct == FlaUI.Core.Definitions.ControlType.Button && !string.IsNullOrEmpty(name))
                                buttons.Add(name);
                            ScanForButtons(child, depth + 1);
                        }
                        catch { }
                        try { child = walker.GetNextSibling(child); } catch { break; }
                    }
                }
                ScanForButtons(closedElement, 0);
            }
            catch { }

            if (buttons.Count > 0)
            {
                var btnList = string.Join("] [", buttons);
                return $"Modal dialog blocking close -- buttons: [{btnList}]. Use: a11y invoke \"*{buttons[0]}*\" to dismiss.";
            }
            return "Window still alive after close -- possible modal dialog (no buttons found via UIA)";
        }
        catch { return null; }
    }
}
