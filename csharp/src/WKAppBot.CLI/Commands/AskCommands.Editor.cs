using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    static async Task<bool> InsertTextContentEditable(CdpClient cdp, string selector, string text)
    {
        // Delegates to CdpClient.InsertContentEditableAsync (3-tier: innerHTML → execCommand → Input.insertText)
        return await cdp.InsertContentEditableAsync(selector, text);
    }

    /// <summary>
    /// Tier 2 focusless insert: DOM.focus (Chrome-internal, no OS SetForegroundWindow) +
    /// execCommand/textContent via Runtime.callFunctionOn.
    /// Called when InsertTextContentEditable (Tier 1) fails.
    /// </summary>
    static async Task<bool> InsertTextViaDomFocus(CdpClient cdp, string selector, string text)
    {
        try
        {
            // Step 1: resolve nodeId via CDP DOM domain
            var docResult = await cdp.SendAsync("DOM.getDocument", new JsonObject { ["depth"] = 0 });
            var rootId = docResult?["root"]?["nodeId"]?.GetValue<int>() ?? 0;
            if (rootId == 0) return false;

            var qResult = await cdp.SendAsync("DOM.querySelector",
                new JsonObject { ["nodeId"] = rootId, ["selector"] = selector });
            var nodeId = qResult?["nodeId"]?.GetValue<int>() ?? 0;
            if (nodeId == 0) return false;

            // Step 2: DOM.focus — Chrome-internal focus, does NOT trigger OS SetForegroundWindow
            await cdp.SendAsync("DOM.focus", new JsonObject { ["nodeId"] = nodeId });
            await Task.Delay(50);

            // Step 3: insert via Runtime.evaluate (same process as Tier 1 but after DOM.focus)
            var jsStr = JsonSerializer.Serialize(text);
            var result = await cdp.EvalAsync($$"""
                (() => {
                    var el = document.querySelector('{{selector}}');
                    if (!el) return 'NOT_FOUND';
                    var t = {{jsStr}};
                    // execCommand first (respects contenteditable selection model)
                    var sel = window.getSelection();
                    var range = document.createRange();
                    range.selectNodeContents(el);
                    range.deleteContents();
                    sel.removeAllRanges();
                    sel.addRange(range);
                    if (document.execCommand('insertText', false, t)) {
                        el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:t}));
                        return el.textContent.length > 0 ? 'OK' : 'EMPTY';
                    }
                    // Fallback: direct DOM mutation + synthetic events (React-compatible)
                    var p = el.querySelector('p');
                    if (p) { p.textContent = t; } else { el.textContent = t; }
                    el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:t}));
                    el.dispatchEvent(new Event('change', {bubbles:true}));
                    return el.textContent.length > 0 ? 'OK' : 'EMPTY';
                })()
                """);
            Console.WriteLine($"[ASK] Tier2-DomFocus: {result}");
            return result == "OK";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Tier2-DomFocus failed: {ex.Message}");
            return false;
        }
    }

    static async Task ClearContentEditable(CdpClient cdp, string selector)
    {
        await cdp.ClearEditorAsync(selector);
    }
}
