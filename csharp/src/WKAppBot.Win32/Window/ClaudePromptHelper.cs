using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.UIA3;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Find and interact with the Claude Code prompt input field.
/// Supports: Claude Desktop (Electron), VS Code Claude Code Extension, Codex Desktop.
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
public sealed partial class ClaudePromptHelper : IDisposable
{
    private readonly UIA3Automation _automation;
    private const string HostClaudeDesktop = "claude-desktop";
    private const string HostVsCodeClaudeCode = "vscode-claudecode";
    private const string HostVsCodeCodex = "vscode-codex";
    private const string HostCodexDesktop = "codex-desktop";

    /// <summary>
    /// Global toggle: when true, only focusless strategies are allowed.
    /// When true, allows focus-stealing as a fallback after focusless input fails.
    /// Default = false → pure focusless mode (all focus-stealing paths blocked).
    /// Set true for critical actions like handoff nudges where delivery matters.
    /// Focus-stealing goes through normal InputReadiness path (overlay + sound + zoom).
    /// </summary>
    public static bool AllowFocusSteal { get; set; }

    // Per-hwnd turn-form cache: once found, reuse until window is gone.
    // UIA tree scan (FindFirstDescendant) reads all conversation content — very slow for long sessions.
    // Cache key = hwnd, invalidated when IsWindow() returns false or hwnd disappears from EnumWindows.
    // Only positive finds are cached (null = "not found yet, retry next time").
    private static readonly Dictionary<IntPtr, PromptInfo> _turnFormCache = new();

    // Negative cache: hwnd → expiry. Suppresses repeated UIA scans for windows confirmed to have no turn-form.
    // TTL = 1s (asymmetric: negative shorter than positive to recover quickly if prompt appears).
    private static readonly Dictionary<IntPtr, DateTime> _negativeCache = new();
    private static readonly TimeSpan NegativeCacheTtl = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Return all currently cached prompt infos (windows already confirmed to have turn-form).
    /// Instant — no UIA scan. Used by Eye loop for per-instance status streaming.
    /// </summary>
    public static IReadOnlyList<PromptInfo> GetAllCachedPrompts()
        => _turnFormCache.Values.ToList();

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
        string HostType, // "claude-desktop" | "vscode" | "unknown"
        bool Visible = true,
        bool Usable = true,
        string? OccludedBy = null
    );

    public static bool IsCodexHostType(string? hostType) =>
        !string.IsNullOrWhiteSpace(hostType) &&
        hostType.Contains("codex", StringComparison.OrdinalIgnoreCase);

    public static bool IsVsCodeHostType(string? hostType) =>
        string.Equals(hostType, HostVsCodeClaudeCode, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(hostType, HostVsCodeCodex, StringComparison.OrdinalIgnoreCase);

    public static bool ShouldSkipVsCodePromptFallbackWindow(string? title) =>
        !string.IsNullOrWhiteSpace(title) &&
        title.Contains("Codex Diff", StringComparison.OrdinalIgnoreCase);

    public static bool IsLikelyVsCodeCodexWindowTitle(string? title)
    {
        if (string.IsNullOrWhiteSpace(title)) return false;
        if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) return false;
        return title.Contains("Codex", StringComparison.OrdinalIgnoreCase);
    }

    public static string ClassifyVsCodeHostType(string? title) =>
        IsLikelyVsCodeCodexWindowTitle(title) ? HostVsCodeCodex : HostVsCodeClaudeCode;

    public record SubmitState(
        bool TurnFormFound,
        bool SubmitFound,
        bool SubmitEnabled,
        string SubmitName
    );

    public void Dispose()
    {
        _automation.Dispose();
    }

    static PromptInfo DecoratePromptInfo(PromptInfo pi)
    {
        try
        {
            var windowVisible = NativeMethods.IsWindowVisible(pi.WindowHandle);
            if (!NativeMethods.GetWindowRect(pi.WindowHandle, out var wr))
                return pi with { Visible = windowVisible, Usable = false, OccludedBy = "window-rect-missing" };

            var windowRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
            var promptRect = pi.PromptRect;
            var hasArea = promptRect.Width > 4 && promptRect.Height > 4;
            var insideWindow = hasArea && windowRect.IntersectsWith(promptRect);
            var promptVisible = windowVisible && hasArea && insideWindow;

            string? occludedBy = null;
            if (promptVisible)
            {
                var cx = promptRect.Left + (promptRect.Width / 2);
                var cy = promptRect.Top + (promptRect.Height / 2);
                var pt = new POINT { X = cx, Y = cy };
                var leaf = NativeMethods.WindowFromPoint(pt);
                if (leaf != IntPtr.Zero)
                {
                    var top = NativeMethods.GetAncestor(leaf, NativeMethods.GA_ROOT);
                    if (top == IntPtr.Zero) top = leaf;
                    if (top != IntPtr.Zero && top != pi.WindowHandle)
                    {
                        var cls = WindowFinder.GetClassName(top);
                        var title = WindowFinder.GetWindowText(top);
                        occludedBy = $"0x{top:X} {cls} \"{TrimForLog(title, 80)}\"";
                    }
                }
            }

            var usable = promptVisible && string.IsNullOrEmpty(occludedBy);
            return pi with
            {
                Visible = promptVisible,
                Usable = usable,
                OccludedBy = occludedBy
            };
        }
        catch
        {
            return pi with { Visible = false, Usable = false, OccludedBy = "decorate-error" };
        }
    }

    internal static PromptInfo? RequireUsablePrompt(PromptInfo? pi)
    {
        if (pi == null) return null;
        var decorated = DecoratePromptInfo(pi);
        return decorated.Usable ? decorated : null;
    }

    static string TrimForLog(string? text, int max)
    {
        if (string.IsNullOrEmpty(text)) return "";
        var s = text.Replace("\r", " ").Replace("\n", " ");
        return s.Length > max ? s[..max] + "..." : s;
    }
}
