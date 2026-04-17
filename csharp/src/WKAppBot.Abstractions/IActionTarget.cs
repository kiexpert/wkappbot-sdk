namespace WKAppBot.Abstractions;

/// <summary>
/// Platform-agnostic abstraction for an automation target element.
/// Used by Action-Aware Readiness (AAR) to verify input readiness
/// before action execution across Windows UIA, Android ADB, and Chrome CDP.
///
/// Return value convention for EnsureActionReady:
///   null        -> blocked (hard fail)
///   == target   -> success (proceed with original target)
///   != target   -> retarget (e.g. popup found above main window)
/// </summary>
public interface IActionTarget
{
    // -- Identity --

    /// <summary>Human-readable name (UIA Name / Android content-desc or text)</summary>
    string DisplayName { get; }

    /// <summary>Programmatic ID (UIA AutomationId / Android resource-id / CSS selector)</summary>
    string? Identifier { get; }

    /// <summary>Element type (UIA ControlType / Android className / HTML tag)</summary>
    string? ClassName { get; }

    /// <summary>Platform backend: "UIA", "Win32", "ADB", "CDP"</summary>
    string BackendType { get; }

    /// <summary>Native platform handle for debugging and platform-specific calls.
    /// IntPtr (hwnd) for Windows, AndroidNode for ADB, string (nodeId) for CDP.</summary>
    object? NativeHandle { get; }

    // -- Bounds --

    /// <summary>Screen coordinates (Left, Top, Right, Bottom)</summary>
    (int Left, int Top, int Right, int Bottom) BoundingRect { get; }

    /// <summary>Center X (derived from BoundingRect)</summary>
    int CenterX => (BoundingRect.Left + BoundingRect.Right) / 2;

    /// <summary>Center Y (derived from BoundingRect)</summary>
    int CenterY => (BoundingRect.Top + BoundingRect.Bottom) / 2;

    /// <summary>Width (derived from BoundingRect)</summary>
    int Width => BoundingRect.Right - BoundingRect.Left;

    /// <summary>Height (derived from BoundingRect)</summary>
    int Height => BoundingRect.Bottom - BoundingRect.Top;

    // -- State --

    /// <summary>Has keyboard/input focus</summary>
    bool Focused { get; }

    /// <summary>Enabled for interaction</summary>
    bool Enabled { get; }

    /// <summary>In the live tree and not collapsed/hidden</summary>
    bool Visible { get; }

    /// <summary>Clipped by scroll container (in tree but not on screen)</summary>
    bool IsOffscreen { get; }

    /// <summary>Is a top-level window (for target resolution validation)</summary>
    bool IsWindow { get; }

    /// <summary>Window visual state: "normal", "minimized", "maximized" (window-level only)</summary>
    string? WindowState { get; }

    // -- Hierarchy --

    /// <summary>Parent element (null for root)</summary>
    IActionTarget? Parent { get; }

    /// <summary>Child elements</summary>
    IReadOnlyList<IActionTarget> Children { get; }
}

/// <summary>
/// Context for readiness pipeline -- carries platform-specific handles.
/// </summary>
public class ReadinessContext
{
    /// <summary>Android device serial</summary>
    public string? Serial { get; init; }

    /// <summary>Windows window handle</summary>
    public IntPtr Hwnd { get; init; }

    /// <summary>Platform client (AdbClient, CdpClient, etc.)</summary>
    public object? PlatformClient { get; init; }

    /// <summary>Max retarget depth for nested modals (default 3)</summary>
    public int MaxRetargetDepth { get; init; } = 3;

    /// <summary>Enable Z-order hit-test in Stage 2 (default true for click/tap)</summary>
    public bool RequireHitTest { get; init; } = true;

    /// <summary>Synchronous polling timeout in ms</summary>
    public int TimeoutMs { get; init; } = 5000;
}

/// <summary>
/// Action-Aware Readiness provider interface.
/// Platform-specific implementations handle the 4-stage pipeline.
/// </summary>
public interface IActionReadiness
{
    /// <summary>
    /// Verify readiness for the given action on the target element.
    /// Stage 0: Global (lock screen), Stage 1: Target resolution,
    /// Stage 2: Occlusion, Stage 3: Focus, Stage 4: Action-specific.
    /// </summary>
    /// <returns>null=blocked, ==target=success, !=target=retarget</returns>
    IActionTarget? Ensure(string action, IActionTarget target, ReadinessContext ctx);
}
