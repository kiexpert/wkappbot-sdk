using System.Text.Json.Nodes;
using WKAppBot.Abstractions;

namespace WKAppBot.WebBot;

/// <summary>
/// IActionTarget wrapper for a Chrome DevTools Protocol (CDP) web element.
/// Created via <see cref="QueryAsync"/> which evaluates element properties
/// in a single JS call: tag, id, class, text, enabled, visible, bounds.
///
/// BoundingRect is in screen coordinates (viewport-relative + window offset).
///
/// Tag: [AAR]
/// </summary>
public sealed class CdpActionTarget : IActionTarget
{
    // -- Identity --
    public string DisplayName { get; init; } = "";
    public string? Identifier { get; init; }
    public string? ClassName { get; init; }
    public string BackendType => "CDP";
    public object? NativeHandle { get; init; }  // CSS selector string

    // -- Bounds (screen coords) --
    public (int Left, int Top, int Right, int Bottom) BoundingRect { get; init; }

    // -- State --
    public bool Focused { get; init; }
    public bool Enabled { get; init; } = true;
    public bool Visible { get; init; } = true;
    public bool IsOffscreen { get; init; }
    public bool IsWindow => false;
    public string? WindowState => null;

    // -- Hierarchy (flat -- CDP doesn't expose tree walking cheaply) --
    public IActionTarget? Parent => null;
    public IReadOnlyList<IActionTarget> Children { get; } = Array.Empty<IActionTarget>();

    // -- Extra CDP properties (for action-specific checks) --

    /// <summary>True if the element has readonly or contenteditable=false.</summary>
    public bool IsReadOnly { get; init; }

    /// <summary>HTML tag name (e.g., "INPUT", "BUTTON", "DIV").</summary>
    public string? TagName { get; init; }

    /// <summary>Input type attribute (e.g., "text", "checkbox", "submit").</summary>
    public string? InputType { get; init; }

    /// <summary>
    /// Query a CDP element's properties in one JS eval and wrap as IActionTarget.
    /// Returns null if element not found.
    /// </summary>
    /// <param name="cdp">Connected CDP client</param>
    /// <param name="cssSelector">CSS selector for the target element</param>
    /// <param name="windowOffsetX">Window content area X offset (screen coords)</param>
    /// <param name="windowOffsetY">Window content area Y offset (screen coords)</param>
    public static CdpActionTarget? Query(CdpClient cdp, string cssSelector,
        int windowOffsetX = 0, int windowOffsetY = 0)
    {
        var escaped = cssSelector.Replace("\\", "\\\\").Replace("'", "\\'");

        // Single JS call to get all properties + bounding rect
        var js = $@"(() => {{
            var el = document.querySelector('{escaped}');
            if (!el) return 'NOT_FOUND';
            var r = el.getBoundingClientRect();
            var cs = window.getComputedStyle(el);
            return JSON.stringify({{
                tag: el.tagName || '',
                id: el.id || '',
                cls: el.className || '',
                text: (el.textContent || '').substring(0, 200).trim(),
                value: el.value || '',
                type: el.type || '',
                aria: el.getAttribute('aria-label') || '',
                disabled: el.disabled === true || el.getAttribute('aria-disabled') === 'true',
                readOnly: el.readOnly === true || el.contentEditable === 'false',
                hidden: cs.display === 'none' || cs.visibility === 'hidden' || (r.width === 0 && r.height === 0),
                focused: document.activeElement === el,
                left: Math.round(r.left),
                top: Math.round(r.top),
                right: Math.round(r.right),
                bottom: Math.round(r.bottom)
            }});
        }})()";

        try
        {
            var result = cdp.EvalAsync(js).GetAwaiter().GetResult();
            if (result == null || result == "NOT_FOUND")
                return null;

            var json = JsonNode.Parse(result);
            if (json == null) return null;

            var tag = json["tag"]?.GetValue<string>() ?? "";
            var id = json["id"]?.GetValue<string>() ?? "";
            var cls = json["cls"]?.GetValue<string>() ?? "";
            var text = json["text"]?.GetValue<string>() ?? "";
            var aria = json["aria"]?.GetValue<string>() ?? "";
            var inputType = json["type"]?.GetValue<string>() ?? "";
            var disabled = json["disabled"]?.GetValue<bool>() ?? false;
            var readOnly = json["readOnly"]?.GetValue<bool>() ?? false;
            var hidden = json["hidden"]?.GetValue<bool>() ?? false;
            var focused = json["focused"]?.GetValue<bool>() ?? false;
            var left = json["left"]?.GetValue<int>() ?? 0;
            var top = json["top"]?.GetValue<int>() ?? 0;
            var right = json["right"]?.GetValue<int>() ?? 0;
            var bottom = json["bottom"]?.GetValue<int>() ?? 0;

            // Build display name: aria-label > text > id > tag
            var displayName = !string.IsNullOrEmpty(aria) ? aria
                : !string.IsNullOrEmpty(text) ? (text.Length > 50 ? text[..50] + "..." : text)
                : !string.IsNullOrEmpty(id) ? $"#{id}"
                : tag.ToLowerInvariant();

            return new CdpActionTarget
            {
                DisplayName = displayName,
                Identifier = cssSelector,
                ClassName = tag,
                NativeHandle = cssSelector,
                BoundingRect = (left + windowOffsetX, top + windowOffsetY,
                                right + windowOffsetX, bottom + windowOffsetY),
                Focused = focused,
                Enabled = !disabled,
                Visible = !hidden,
                IsOffscreen = false,
                IsReadOnly = readOnly,
                TagName = tag,
                InputType = inputType,
            };
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get the Chrome window's content area offset for viewport->screen conversion.
    /// Uses CDP Browser.getWindowBounds if available, otherwise returns (0, 0).
    /// </summary>
    public static (int X, int Y) GetWindowContentOffset(CdpClient cdp)
    {
        try
        {
            // devicePixelRatio-aware offset via JS
            var js = "JSON.stringify({ x: Math.round(window.screenX + (window.outerWidth - window.innerWidth) / 2 * window.devicePixelRatio), " +
                     "y: Math.round(window.screenY + (window.outerHeight - window.innerHeight) * window.devicePixelRatio) })";
            var result = cdp.EvalAsync(js).GetAwaiter().GetResult();
            if (result != null)
            {
                var json = JsonNode.Parse(result);
                if (json != null)
                    return (json["x"]?.GetValue<int>() ?? 0, json["y"]?.GetValue<int>() ?? 0);
            }
        }
        catch { /* best effort */ }
        return (0, 0);
    }
}
