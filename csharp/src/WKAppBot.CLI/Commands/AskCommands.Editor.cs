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
        // Use JSON serialization for safe JS string escaping (\r, \n, \', \", \, etc.)
        var jsStr = JsonSerializer.Serialize(text); // produces "\"...\""

        var focusless = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                var t = {{jsStr}};
                var p = el.querySelector('p');
                if (p) { p.textContent = t; }
                else { el.innerHTML = '<p>' + t.replace(/</g,'&lt;') + '</p>'; }
                el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:t}));
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        var result = await cdp.EvalAsync(focusless);
        if (result == "OK") return true;

        var js = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                var sel = window.getSelection();
                var range = document.createRange();
                range.selectNodeContents(el);
                range.collapse(false);
                sel.removeAllRanges();
                sel.addRange(range);
                document.execCommand('insertText', false, {{jsStr}});
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        result = await cdp.EvalAsync(js);
        return result == "OK";
    }

    static async Task ClearContentEditable(CdpClient cdp, string selector)
    {
        await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return;
                el.focus();
                document.execCommand('selectAll');
                document.execCommand('delete');
            })()
            """);
    }
}
