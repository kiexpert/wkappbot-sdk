using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>
    /// Right-click an element by CSS selector (context menu).
    /// Uses CDP Input.dispatchMouseEvent with button="right".
    /// </summary>
    public async Task RightClickAsync(string selector)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        var pointJson = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escapedSelector}}');
                if (!el) return JSON.stringify({ ok:false, reason:'NOT_FOUND' });
                el.scrollIntoView({ block:'center', inline:'center' });
                const r = el.getBoundingClientRect();
                if (!r || r.width <= 0 || r.height <= 0)
                    return JSON.stringify({ ok:false, reason:'NO_RECT' });
                const x = r.left + Math.min(r.width / 2, Math.max(1, r.width - 1));
                const y = r.top + Math.min(r.height / 2, Math.max(1, r.height - 1));
                return JSON.stringify({ ok:true, x, y });
            })()
            """);

        if (string.IsNullOrWhiteSpace(pointJson))
            throw new InvalidOperationException($"Failed to resolve click point: {selector}");

        var point = JsonNode.Parse(pointJson);
        var ok = point?["ok"]?.GetValue<bool>() ?? false;
        if (!ok)
        {
            var reason = point?["reason"]?.GetValue<string>();
            throw new InvalidOperationException(reason == "NOT_FOUND"
                ? $"Element not found: {selector}"
                : $"Element not clickable: {selector} ({reason})");
        }

        var x = point?["x"]?.GetValue<double>() ?? 0;
        var y = point?["y"]?.GetValue<double>() ?? 0;

        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseMoved", ["x"] = x, ["y"] = y, ["button"] = "none" });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mousePressed", ["x"] = x, ["y"] = y, ["button"] = "right", ["clickCount"] = 1 });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseReleased", ["x"] = x, ["y"] = y, ["button"] = "right", ["clickCount"] = 1 });
    }

    /// <summary>
    /// Scroll an element or the page by CSS selector.
    /// Uses Element.scrollBy for element scroll, window.scrollBy for page scroll.
    /// </summary>
    public async Task ScrollAsync(string? selector, string direction, string amount)
    {
        // Parse amount: "page" = viewport height, number = pixels, default = 300px
        var amountJs = amount?.ToLowerInvariant() switch
        {
            "page" => direction is "up" or "down"
                ? "(window.innerHeight * 0.85)"
                : "(window.innerWidth * 0.85)",
            null or "" => "300",
            _ => int.TryParse(amount, out var px) ? px.ToString() : "300"
        };

        var (dx, dy) = direction?.ToLowerInvariant() switch
        {
            "up" => ("0", $"-{amountJs}"),
            "down" => ("0", amountJs),
            "left" => ($"-{amountJs}", "0"),
            "right" => (amountJs, "0"),
            _ => ("0", amountJs) // default = down
        };

        if (string.IsNullOrEmpty(selector) || selector == "window" || selector == "page")
        {
            await EvalAsync($"window.scrollBy({dx}, {dy})");
        }
        else
        {
            var esc = selector.Replace("\\", "\\\\").Replace("'", "\\'");
            var js = $"(() => {{ const el = document.querySelector('{esc}'); if (!el) return 'NOT_FOUND'; el.scrollBy({dx}, {dy}); return 'OK'; }})()";
            var result = await EvalAsync(js);
            if (result == "NOT_FOUND")
                throw new InvalidOperationException($"Element not found: {selector}");
        }
    }

    /// <summary>
    /// Expand or collapse a details/disclosure element, or toggle aria-expanded.
    /// </summary>
    public async Task ExpandCollapseAsync(string selector, bool expand)
    {
        var esc = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var js = $$"""
            (() => {
                const el = document.querySelector('{{esc}}');
                if (!el) return 'NOT_FOUND';
                // <details> element
                if (el.tagName === 'DETAILS') {
                    el.open = {{(expand ? "true" : "false")}};
                    return 'OK';
                }
                // aria-expanded toggle
                const cur = el.getAttribute('aria-expanded');
                if (cur !== null) {
                    el.setAttribute('aria-expanded', '{{(expand ? "true" : "false")}}');
                    el.click();
                    return 'OK';
                }
                // Fallback: just click (many accordions toggle on click)
                el.click();
                return 'OK_CLICK';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ShowWindow")]
    private static extern bool ShowWindowNative(IntPtr hWnd, int nCmdShow);

    /// <summary>
    /// React SPA fallback click: calls __reactProps.onClick directly, bypassing isTrusted check.
    /// Returns true if a React handler was found and invoked; false if element has no React fiber.
    /// </summary>
    public async Task<bool> TryReactFiberClickAsync(string selector)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var result = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escaped}}');
                if (!el) return 'NOT_FOUND';
                const key = Object.keys(el).find(k => k.startsWith('__reactProps'));
                if (!key) return 'NO_FIBER';
                const props = el[key];
                if (typeof props?.onClick !== 'function') return 'NO_ONCLICK';
                const r = el.getBoundingClientRect();
                const cx = r.left + r.width / 2, cy = r.top + r.height / 2;
                props.onClick({ type:'click', target:el, currentTarget:el,
                    clientX:cx, clientY:cy, bubbles:true, cancelable:true,
                    preventDefault:()=>{}, stopPropagation:()=>{} });
                return 'OK';
            })()
            """);
        return result == "OK";
    }

    /// <summary>
    /// React SPA fallback setValue: uses nativeInputValueSetter + fiber onChange to update controlled inputs.
    /// Returns true if the React onChange handler was found and invoked.
    /// </summary>
    public async Task<bool> TryReactFiberSetValueAsync(string selector, string value)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var escapedValue = value.Replace("\\", "\\\\").Replace("'", "\\'");
        var result = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escaped}}');
                if (!el || (el.tagName !== 'INPUT' && el.tagName !== 'TEXTAREA')) return 'NOT_INPUT';
                const nativeSetter = Object.getOwnPropertyDescriptor(
                    el.tagName === 'TEXTAREA' ? HTMLTextAreaElement.prototype : HTMLInputElement.prototype,
                    'value')?.set;
                if (nativeSetter) nativeSetter.call(el, '{{escapedValue}}');
                else el.value = '{{escapedValue}}';
                const fiberKey = Object.keys(el).find(k => k.startsWith('__reactFiber'));
                if (fiberKey) {
                    const onChange = el[fiberKey]?.memoizedProps?.onChange;
                    if (typeof onChange === 'function') {
                        onChange({ target: el, currentTarget: el, bubbles: true });
                        return 'OK_FIBER';
                    }
                }
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
                return 'OK_EVENT';
            })()
            """);
        return result is "OK_FIBER" or "OK_EVENT";
    }
}

/// <summary>DOM 핫키 스캔 결과 엔트리.</summary>
public record CdpHotkeyEntry(
    string Label,
    string Accesskey,
    string Keyshortcuts,
    string Selector);
