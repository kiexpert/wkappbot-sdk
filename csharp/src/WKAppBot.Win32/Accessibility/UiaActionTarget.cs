using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WKAppBot.Abstractions;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// IActionTarget adapter for Windows UIA AutomationElement.
/// Wraps FlaUI AutomationElement with COM-safe property access.
/// All property reads are try-catch guarded (COM can throw at any time).
/// </summary>
public class UiaActionTarget : IActionTarget
{
    private readonly AutomationElement _element;

    public UiaActionTarget(AutomationElement element)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>Underlying FlaUI element for platform-specific operations</summary>
    public AutomationElement Element => _element;

    // ── Identity ──

    public string DisplayName => SafeGet(() => _element.Properties.Name.ValueOrDefault) ?? "";

    public string? Identifier => SafeGet(() => _element.Properties.AutomationId.ValueOrDefault);

    public string? ClassName
    {
        get
        {
            try
            {
                var ct = _element.Properties.ControlType.ValueOrDefault;
                return ct.ToString();
            }
            catch
            {
                return SafeGet(() => _element.Properties.ClassName.ValueOrDefault);
            }
        }
    }

    public string BackendType => "UIA";

    public object? NativeHandle
    {
        get
        {
            var hwnd = SafeGet(() => _element.Properties.NativeWindowHandle.ValueOrDefault);
            return hwnd != default ? hwnd : null;
        }
    }

    // ── Bounds ──

    public (int Left, int Top, int Right, int Bottom) BoundingRect
    {
        get
        {
            var r = SafeGet(() => _element.Properties.BoundingRectangle.ValueOrDefault);
            if (r.Width <= 0 && r.Height <= 0)
                return (0, 0, 0, 0);
            return ((int)r.Left, (int)r.Top, (int)r.Right, (int)r.Bottom);
        }
    }

    // ── State ──

    public bool Focused => SafeGet(() => _element.Properties.HasKeyboardFocus.ValueOrDefault);

    public bool Enabled => SafeGet(() => _element.Properties.IsEnabled.ValueOrDefault, true);

    public bool Visible
    {
        get
        {
            // Not offscreen AND has non-zero bounds
            var offscreen = SafeGet(() => _element.Properties.IsOffscreen.ValueOrDefault);
            if (offscreen) return false;
            var r = BoundingRect;
            return r.Right > r.Left && r.Bottom > r.Top;
        }
    }

    public bool IsOffscreen => SafeGet(() => _element.Properties.IsOffscreen.ValueOrDefault);

    public bool IsWindow
    {
        get
        {
            var ct = SafeGet(() => _element.Properties.ControlType.ValueOrDefault);
            if (ct == ControlType.Window) return true;
            // Also check if it has a window handle and is top-level
            var hwnd = SafeGet(() => _element.Properties.NativeWindowHandle.ValueOrDefault);
            return hwnd != default && Parent == null;
        }
    }

    public string? WindowState
    {
        get
        {
            try
            {
                if (!_element.Patterns.Window.IsSupported) return null;
                var state = _element.Patterns.Window.Pattern.WindowVisualState.ValueOrDefault;
                return state switch
                {
                    WindowVisualState.Normal => "normal",
                    WindowVisualState.Minimized => "minimized",
                    WindowVisualState.Maximized => "maximized",
                    _ => null
                };
            }
            catch { return null; }
        }
    }

    // ── Hierarchy ──

    public IActionTarget? Parent
    {
        get
        {
            try
            {
                var parent = _element.Parent;
                return parent != null ? new UiaActionTarget(parent) : null;
            }
            catch { return null; }
        }
    }

    public IReadOnlyList<IActionTarget> Children
    {
        get
        {
            try
            {
                var kids = _element.FindAllChildren();
                return kids.Select(c => (IActionTarget)new UiaActionTarget(c)).ToList();
            }
            catch { return []; }
        }
    }

    // ── Equality (reference-based for retarget comparison) ──

    public override bool Equals(object? obj)
    {
        if (obj is UiaActionTarget other)
        {
            // Compare by native handle if available, otherwise by RuntimeId
            var h1 = SafeGet(() => _element.Properties.NativeWindowHandle.ValueOrDefault);
            var h2 = SafeGet(() => other._element.Properties.NativeWindowHandle.ValueOrDefault);
            if (h1 != default && h2 != default)
                return h1 == h2;

            // Fallback: BoundingRect + Name + AutomationId
            return BoundingRect == other.BoundingRect
                && DisplayName == other.DisplayName
                && Identifier == other.Identifier;
        }
        return ReferenceEquals(this, obj);
    }

    public override int GetHashCode()
    {
        var hwnd = SafeGet(() => _element.Properties.NativeWindowHandle.ValueOrDefault);
        return hwnd != default ? hwnd.GetHashCode() : HashCode.Combine(DisplayName, Identifier);
    }

    public override string ToString()
        => $"[UIA] {ClassName} \"{DisplayName}\" (aid={Identifier})";

    // ── Safe COM access ──

    private static T? SafeGet<T>(Func<T> getter, T? fallback = default)
    {
        try { return getter(); }
        catch { return fallback; }
    }
}
