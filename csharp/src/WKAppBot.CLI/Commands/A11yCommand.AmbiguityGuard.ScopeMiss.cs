// A11yCommand.AmbiguityGuard.ScopeMiss.cs -- Layer 5+6: scope/UIA miss hints
// Extracted from A11yCommand.AmbiguityGuard.cs to keep files under 400 lines.

using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static partial class TargetAmbiguityGuard
    {
        // -- Layer 5: Missing #scope on element action --

        /// <summary>
        /// Element action (click/invoke/type) without #scope -> show root children + focused leaf.
        /// Returns true if guard triggered (action should abort).
        /// </summary>
        public static bool CheckMissingScope(AutomationElement root, IntPtr hwnd,
            string title, string action, bool isInteractiveAction, string? grap = null)
        {
            // Only guard interactive element actions (not close/minimize/etc.)
            if (!isInteractiveAction || action is "close" or "minimize" or "maximize" or "restore" or "focus" or "move" or "resize")
                return false;

            // Explicit hwnd:0x... in grap uniquely identifies the control -- skip scope guard.
            if (!string.IsNullOrEmpty(grap) &&
                System.Text.RegularExpressions.Regex.IsMatch(grap, @"hwnd\s*[:=]\s*0x[0-9A-Fa-f]+",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                return false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[A11Y] No #scope -- auto-switching to find. UIA elements in \"{title}\":");
            Console.ResetColor();
            try
            {
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                string proc = ""; try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
                var compact = Program.BuildCompactWinGrap(hwnd);
                var winGrapJson = BuildFindGrap(hwnd, pid, proc, compact, null);
                var winPaste = QuoteGrapExpression(winGrapJson);

                // Show focused element first (leaf -> parent chain)
                try
                {
                    using var focLoc = new UiaLocator();
                    var focInfo = focLoc.GetFocusedElementInfo();
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint winPid);
                    if (focInfo != null && focInfo.ProcessId == (int)winPid)
                    {
                        var fLabel = !string.IsNullOrEmpty(focInfo.AutomationId) ? focInfo.AutomationId : focInfo.Name;
                        if (fLabel.Length > 40) fLabel = fLabel[..40];
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  >> [FOCUS] {focInfo.ControlType}(\"{fLabel}\")");
                        if (focInfo.Patterns.Count > 0) Console.Write($" {string.Join(",", focInfo.Patterns)}");
                        Console.WriteLine();
                        foreach (var (pType, pName, _) in focInfo.ParentChain)
                        {
                            if (string.IsNullOrEmpty(pName)) continue;
                            Console.Write($"       <- {pType}(\"{pName}\")");
                            Console.WriteLine();
                        }
                        Console.ResetColor();
                        if (!string.IsNullOrEmpty(fLabel))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"     -> a11y {action} {winPaste}#*{fLabel}*\"");
                            Console.ResetColor();
                        }
                    }
                }
                catch { }

                // List root children
                var children = root.FindAllChildren();
                int shown = 0;
                foreach (var child in children)
                {
                    if (shown >= 20) { Console.WriteLine($"     ... (+{children.Length - shown} more -- use a11y find for full tree)"); break; }
                    var cType = "?"; try { cType = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                    var cName = child.Properties.Name.ValueOrDefault ?? "";
                    var cAid = child.Properties.AutomationId.ValueOrDefault ?? "";
                    if (cName.Length > 40) cName = cName[..40];
                    var cLabel = !string.IsNullOrEmpty(cAid) ? cAid : cName;
                    if (string.IsNullOrEmpty(cLabel)) { shown++; continue; }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"     {cType}(\"{cLabel}\") -> a11y {action} {winPaste}#*{cLabel}*\"");
                    Console.ResetColor();
                    shown++;
                }
            }
            catch { }
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint tipPid);
            string tipProc = ""; try { tipProc = System.Diagnostics.Process.GetProcessById((int)tipPid).ProcessName; } catch { }
            var tipCompact = Program.BuildCompactWinGrap(hwnd);
            var tipGrap = QuoteGrapExpression(BuildFindGrap(hwnd, tipPid, tipProc, tipCompact, null));
            Console.Error.WriteLine($"[A11Y] Tip: a11y find {tipGrap} --depth 5");
            return true;
        }

        // -- Layer 6: UIA scope element miss --

        /// <summary>
        /// UIA scope element not found -> list available children in the current root.
        /// </summary>
        public static void ShowUiaScopeMiss(AutomationElement root, IntPtr hwnd,
            string uiaPath, string action)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[A11Y] \"{uiaPath}\" not found -- available elements:");
            Console.ResetColor();
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            string proc = ""; try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            var compact = Program.BuildCompactWinGrap(hwnd);
            var winGrapJson = BuildFindGrap(hwnd, pid, proc, compact, null);
            var winPaste = QuoteGrapExpression(winGrapJson);
            try
            {
                var children = root.FindAllChildren();
                int shown = 0;
                foreach (var child in children)
                {
                    if (shown >= 15) { Console.WriteLine($"     ... (+{children.Length - shown} more)"); break; }
                    var cType = "?"; try { cType = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                    var cName = child.Properties.Name.ValueOrDefault ?? "";
                    var cAid = child.Properties.AutomationId.ValueOrDefault ?? "";
                    if (cName.Length > 40) cName = cName[..40];
                    var cLabel = !string.IsNullOrEmpty(cAid) ? cAid : cName;
                    if (string.IsNullOrEmpty(cLabel)) { shown++; continue; }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"     {cType}(\"{cLabel}\") -> a11y {action} {winPaste}#*{cLabel}*\"");
                    Console.ResetColor();
                    shown++;
                }
            }
            catch { }
            Console.Error.WriteLine($"[A11Y] Tip: a11y find {winPaste} --depth 5");
        }
    }
}
