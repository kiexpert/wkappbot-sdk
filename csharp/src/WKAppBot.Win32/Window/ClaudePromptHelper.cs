using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.UIA3;

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
        string HostType // "claude-desktop" | "vscode" | "unknown"
    );

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
}
