using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using FlaUI.Core.AutomationElements;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // Dialog policy button patterns -- one set per window_close dialog_policy value.
    // Matched case-insensitively against UIA Button.Name after NormalizeUiText
    // (strips '&' accelerator and whitespace).
    private static readonly string[] DontSavePatterns =
    [
        "저장 안 함",
        "저장안함",
        "Don't Save",
        "Dont Save",
        "Don't save",
        "Dont save",
        "&Don't Save",
        "No",
        "Discard",
    ];

    private static readonly string[] SavePatterns =
    [
        "저장",
        "저장(&S)",
        "Save",
        "&Save",
        "Yes",
        "&Yes",
        "예",
        "예(&Y)",
    ];

    private static readonly string[] CancelPatterns =
    [
        "취소",
        "취소(&C)",
        "Cancel",
        "&Cancel",
    ];

    private static readonly string[] SaveDialogTitleHints =
    [
        "저장",
        "save",
        "Save",
        "메모장",
        "Notepad",
    ];

    /// <summary>
    /// Normalize a raw dialog_policy string (case-insensitive, trims whitespace and hyphens).
    /// Returns canonical "dont_save" | "save" | "cancel" | "ask_user". Null/empty -> "dont_save" (legacy default).
    /// </summary>
    private static string NormalizeDialogPolicy(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "dont_save";
        var k = raw.Trim().ToLowerInvariant().Replace("-", "_").Replace(" ", "_");
        return k switch
        {
            "save" or "keep" or "yes" => "save",
            "dont_save" or "discard" or "no" => "dont_save",
            "cancel" or "abort" => "cancel",
            "ask_user" or "ask" or "report" => "ask_user",
            _ => k,
        };
    }

    /// <summary>
    /// Select the button-name patterns that match the requested policy.
    /// Returns null for "ask_user" (skip auto-dismiss).
    /// </summary>
    private static string[]? PatternsForPolicy(string policy) => policy switch
    {
        "save" => SavePatterns,
        "dont_save" => DontSavePatterns,
        "cancel" => CancelPatterns,
        _ => null, // ask_user or unknown -> let DetectModalDialogAfterClose surface buttons
    };

    /// <summary>
    /// Close, minimize, or maximize a window. Focusless via UIA Window pattern.
    /// For close: if a modal (save prompt etc.) blocks the close, try to dismiss
    /// it; if it can't be dismissed, fail the step with a descriptive error.
    /// </summary>
    private void DoWindowAction(StepDefinition step, StepResult result, string action)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for window_{action}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        bool success = action switch
        {
            "close" => UiaLocator.TryWindowClose(element),
            "minimize" => UiaLocator.TrySetWindowState(element,
                FlaUI.Core.Definitions.WindowVisualState.Minimized),
            "maximize" => UiaLocator.TrySetWindowState(element,
                FlaUI.Core.Definitions.WindowVisualState.Maximized),
            _ => false
        };

        var viaLabel = success ? $"{method}, focusless" : null;

        // Fallback for close: Alt+F4 (needs focus, SendInput)
        if (!success && action == "close")
        {
            EnsureFocus();
            KeyboardInput.Hotkey(new[] { "alt", "F4" });
            success = true;
            viaLabel = "Alt+F4 fallback";
        }

        if (!success)
            throw new InvalidOperationException($"Window {action} failed for {elemDesc}");

        // Close post-action: poll up to 500ms for target to disappear.
        // Early-out the moment the window is gone -- no need to probe for modals.
        if (action == "close")
        {
            var targetHwnd = element.Properties.NativeWindowHandle.ValueOrDefault;
            bool stillAlive = true;
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(100);
                if (targetHwnd == IntPtr.Zero || !NativeMethods.IsWindow(targetHwnd))
                {
                    stillAlive = false;
                    break;
                }
            }

            if (!stillAlive)
            {
                result.ActionDetail = $"Window close {elemDesc} ({viaLabel})";
                Log($"  Window close via {viaLabel}");
                return;
            }

            var policy = NormalizeDialogPolicy(step.DialogPolicy);
            var patterns = PatternsForPolicy(policy);

            if (patterns != null &&
                TryDismissSavePromptAfterClose(element, patterns, out var dismissDetail))
            {
                result.ActionDetail = $"Window close {elemDesc} ({viaLabel}) + [{policy}] {dismissDetail}";
                Log($"  [CLOSE-GUARD] policy={policy} {dismissDetail}");
                return;
            }

            // Target still alive + no matching dismiss button -> dump focused node grap
            // (paste-ready), then fail. Gives operator/AI a concrete next-step target.
            var focusDump = BuildFocusedNodeReport(targetHwnd);
            var dialogInfo = DetectModalDialogAfterClose(element);

            result.Status = StepStatus.Fail;
            var baseMsg = policy == "ask_user"
                ? "[dialog_policy=ask_user] window still alive after close"
                : $"[dialog_policy={policy}] no matching button -- window still alive after close";
            result.Message = focusDump != null
                ? $"{baseMsg}\n{focusDump}\n{dialogInfo ?? ""}".TrimEnd()
                : $"{baseMsg} -- {dialogInfo ?? "(no focused node found)"}";

            Log($"  [CLOSE-GUARD] policy={policy} -- FAILED, focus-node dump follows");
            if (focusDump != null) Log(focusDump);
            return;
        }

        result.ActionDetail = $"Window {action} {elemDesc} ({viaLabel})";
        Log($"  Window {action} via {viaLabel}");
    }

    /// <summary>
    /// After a close action, try to find a save prompt and invoke the button whose
    /// name matches one of <paramref name="patterns"/>. Returns true if a button was found and clicked.
    /// </summary>
    private bool TryDismissSavePromptAfterClose(
        AutomationElement closedElement, string[] patterns, out string? detail)
    {
        detail = null;

        try
        {
            var hwnd = closedElement.Properties.NativeWindowHandle.ValueOrDefault;
            if (hwnd == IntPtr.Zero) return false;

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint ownerPid);
            if (ownerPid == 0) return false;

            Thread.Sleep(200);

            foreach (var dialogHwnd in FindLikelySaveDialogs(ownerPid))
            {
                var dialogRoot = _uia.Automation.FromHandle(dialogHwnd);
                if (dialogRoot == null) continue;

                if (TryInvokeDismissButton(dialogRoot, patterns, out var buttonName))
                {
                    detail = $"save prompt dismissed via [{buttonName}]";
                    Thread.Sleep(250);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Log($"  [CLOSE-GUARD] save prompt handler error: {ex.Message}");
        }

        return false;
    }

    /// <summary>
    /// Find likely save prompt windows belonging to the same process.
    /// </summary>
    private List<IntPtr> FindLikelySaveDialogs(uint ownerPid)
    {
        var results = new List<IntPtr>();

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd))
                return true;

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != ownerPid)
                return true;

            var title = WindowFinder.GetWindowText(hWnd);
            var className = WindowFinder.GetClassName(hWnd);

            bool looksLikeDialog = className == "#32770"
                || SaveDialogTitleHints.Any(hint =>
                    title.Contains(hint, StringComparison.OrdinalIgnoreCase));

            if (looksLikeDialog)
                results.Add(hWnd);

            return true;
        }, IntPtr.Zero);

        return results;
    }

    /// <summary>
    /// Recursively scan a dialog for a button whose normalized name matches any of
    /// <paramref name="patterns"/> and invoke it.
    /// </summary>
    private bool TryInvokeDismissButton(
        AutomationElement dialogRoot, string[] patterns, out string? buttonName)
    {
        buttonName = null;

        try
        {
            var walker = dialogRoot.Automation.TreeWalkerFactory.GetControlViewWalker();
            var queue = new Queue<AutomationElement>();
            queue.Enqueue(dialogRoot);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                try
                {
                    var controlType = current.Properties.ControlType.ValueOrDefault;
                    var name = current.Properties.Name.ValueOrDefault ?? "";

                    if (controlType == FlaUI.Core.Definitions.ControlType.Button)
                    {
                        var normalized = NormalizeUiText(name);
                        if (patterns.Any(pattern =>
                            string.Equals(NormalizeUiText(pattern), normalized, StringComparison.OrdinalIgnoreCase)))
                        {
                            var tagPath = _uia.GetAbsoluteTagPath(current);
                            var resolved = !string.IsNullOrEmpty(tagPath)
                                ? GrapHelper.FindUiaScope(dialogRoot, tagPath, maxDepth: 15) ?? current
                                : current;

                            if (UiaLocator.TryInvoke(resolved))
                            {
                                buttonName = string.IsNullOrEmpty(tagPath) ? name : $"{name} [{tagPath}]";
                                return true;
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    var child = walker.GetFirstChild(current);
                    while (child != null)
                    {
                        queue.Enqueue(child);
                        try { child = walker.GetNextSibling(child); }
                        catch { break; }
                    }
                }
                catch { }
            }
        }
        catch { }

        return false;
    }

    /// <summary>
    /// Normalize UI text so accelerator markers and spaces do not block matching.
    /// </summary>
    private static string NormalizeUiText(string value)
        => new string(value.Where(ch => ch != '&' && !char.IsWhiteSpace(ch)).ToArray());

}
