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

// partial class: wkappbot ask <ai> "question" ??one-command AI Q&A via WebBot
internal partial class Program
{
    // ???? CDP InputReadiness: ensure Chrome window is ready for CDP actions ????
    // Detects blockers, restores from minimized (focusless), guards focus theft.

    /// <summary>
    /// Ensure Chrome window is ready for a CDP action. Focusless approach:
    /// 1. DetectBlocker (~5ms) ??check for blocking popups
    /// 2. Minimized restore (SW_SHOWNOACTIVATE ??no focus steal)
    /// 3. Zoom overlay on target element (visual feedback)
    /// 4. Focus theft guard: snapshot + restore after action
    /// Returns (ready, prevForeground, zoom) ??caller disposes zoom after action.
    /// </summary>
    static async Task<List<string>> DetectAndDownloadImages(
        CdpClient cdp, HashSet<string> knownImageUrls, string aiName, string? responseSelector = null)
    {
        var saved = new List<string>();
        try
        {
            // Find all images in the latest response that we haven't seen yet
            var selectorBase = responseSelector ?? (aiName == "gemini"
                ? "model-response:last-of-type img, [role='article']:last-of-type img"
                : "[data-message-author-role='assistant']:last-of-type img, article:last-of-type img");

            var imgJson = await cdp.EvalAsync($$"""
                (() => {
                    var imgs = document.querySelectorAll("{{selectorBase}}");
                    var result = [];
                    var allImgs = document.querySelectorAll('img');
                    for (var img of imgs) {
                        var src = img.src || img.getAttribute('src') || '';
                        if (!src || src.startsWith('data:image/svg')) continue;
                        // Skip tiny icons/avatars (< 50px)
                        if (img.naturalWidth > 0 && img.naturalWidth < 50) continue;
                        // Find global index for reliable re-lookup
                        var idx = -1;
                        for (var j = 0; j < allImgs.length; j++) { if (allImgs[j] === img) { idx = j; break; } }
                        result.push({src: src, w: img.naturalWidth || 0, h: img.naturalHeight || 0, idx: idx});
                    }
                    return JSON.stringify(result);
                })()
                """) ?? "[]";

            var imgs = JsonDocument.Parse(imgJson).RootElement;
            foreach (var img in imgs.EnumerateArray())
            {
                var src = img.GetProperty("src").GetString() ?? "";
                var imgIdx = img.TryGetProperty("idx", out var idxEl) ? idxEl.GetInt32() : -1;
                if (string.IsNullOrEmpty(src) || knownImageUrls.Contains(src)) continue;
                knownImageUrls.Add(src);

                // Download image
                var outputDir = Path.Combine(
                    Environment.GetEnvironmentVariable("WKAPPBOT_HQ") ?? @"W:\SDK\bin\wkappbot.hq",
                    "output");
                Directory.CreateDirectory(outputDir);
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var idx = saved.Count + 1;
                var w = img.GetProperty("w").GetInt32();
                var h = img.GetProperty("h").GetInt32();
                var fileName = $"{aiName}_gen_{timestamp}_{idx}.png";
                var filePath = Path.Combine(outputDir, fileName);

                try
                {
                    byte[]? bytes = null;
                    // Use global index for reliable re-lookup (avoids URL matching issues)
                    string findImgJs;
                    if (imgIdx >= 0)
                    {
                        findImgJs = $"document.querySelectorAll('img')[{imgIdx}]";
                    }
                    else
                    {
                        var srcSnippet = src.Replace("'", "\\'");
                        if (srcSnippet.Length > 60) srcSnippet = srcSnippet.Substring(0, 60);
                        findImgJs = $"Array.from(document.querySelectorAll('img')).find(i => i.src.includes('{srcSnippet}'))";
                    }

                    // ???? Tier 1: Canvas rendering ??reload with crossOrigin to avoid taint ????
                    var canvasB64 = await cdp.EvalAsync($$"""
                        (async () => {
                            try {
                                var origImg = {{findImgJs}};
                                if (!origImg) return 'ERR:no_img';
                                // Try direct canvas first (same-origin images)
                                try {
                                    if (origImg.complete && origImg.naturalWidth > 0) {
                                        var c = document.createElement('canvas');
                                        c.width = origImg.naturalWidth; c.height = origImg.naturalHeight;
                                        c.getContext('2d').drawImage(origImg, 0, 0);
                                        var d = c.toDataURL('image/png');
                                        return d.substring(d.indexOf(',') + 1);
                                    }
                                } catch(e) { /* tainted ??fall through to crossOrigin reload */ }
                                // Reload with crossOrigin='anonymous' to avoid taint
                                var src = origImg.src;
                                if (!src.startsWith('data:') && !src.startsWith('blob:')) {
                                    var newImg = new Image();
                                    newImg.crossOrigin = 'anonymous';
                                    var loaded = await new Promise((resolve) => {
                                        newImg.onload = () => resolve(true);
                                        newImg.onerror = () => resolve(false);
                                        setTimeout(() => resolve(false), 5000);
                                        newImg.src = src;
                                    });
                                    if (loaded && newImg.naturalWidth > 0) {
                                        var c = document.createElement('canvas');
                                        c.width = newImg.naturalWidth; c.height = newImg.naturalHeight;
                                        c.getContext('2d').drawImage(newImg, 0, 0);
                                        var d = c.toDataURL('image/png');
                                        return d.substring(d.indexOf(',') + 1);
                                    }
                                }
                                // data: or blob: direct canvas with wait
                                if (!origImg.complete) await new Promise(r => { origImg.onload = r; setTimeout(r, 3000); });
                                if (!origImg.naturalWidth) return 'ERR:no_natural';
                                var c2 = document.createElement('canvas');
                                c2.width = origImg.naturalWidth; c2.height = origImg.naturalHeight;
                                c2.getContext('2d').drawImage(origImg, 0, 0);
                                var d2 = c2.toDataURL('image/png');
                                return d2.substring(d2.indexOf(',') + 1);
                            } catch(e) { return 'ERR:' + e.message; }
                        })()
                        """, awaitPromise: true) ?? "ERR:null";
                    if (!canvasB64.StartsWith("ERR:") && canvasB64.Length > 100)
                    {
                        bytes = Convert.FromBase64String(canvasB64);
                        Console.WriteLine($"[ASK] Image captured via canvas ({w}x{h})");
                    }
                    else if (canvasB64.StartsWith("ERR:"))
                    {
                        Console.WriteLine($"[ASK] Canvas failed: {canvasB64}");
                    }

                    // ???? Tier 2: data: URL direct extract ????
                    if (bytes == null && src.StartsWith("data:"))
                    {
                        var commaIdx = src.IndexOf(',');
                        if (commaIdx > 0)
                            bytes = Convert.FromBase64String(src.Substring(commaIdx + 1));
                    }

                    // ???? Tier 3: fetch with credentials (https: URLs) ????
                    if (bytes == null && src.StartsWith("http"))
                    {
                        var fetchSrcEsc = src.Replace("\\", "\\\\").Replace("'", "\\'");
                        var fetchB64 = await cdp.EvalAsync($$"""
                            (async () => {
                                try {
                                    var resp = await fetch('{{fetchSrcEsc}}', {credentials: 'include'});
                                    if (!resp.ok) return 'ERR:' + resp.status;
                                    var blob = await resp.blob();
                                    if (blob.size < 100) return 'ERR:empty';
                                    return await new Promise((resolve) => {
                                        var reader = new FileReader();
                                        reader.onloadend = () => {
                                            var r = reader.result || '';
                                            var i = r.indexOf(',');
                                            resolve(i > 0 ? r.substring(i + 1) : '');
                                        };
                                        reader.readAsDataURL(blob);
                                    });
                                } catch(e) { return 'ERR:' + e.message; }
                            })()
                            """, awaitPromise: true) ?? "ERR:null";
                        if (!fetchB64.StartsWith("ERR:") && fetchB64.Length > 100)
                            bytes = Convert.FromBase64String(fetchB64);
                    }

                    // ???? Tier 4: CDP Page.captureScreenshot with proper timing ????
                    if (bytes == null)
                    {
                        try
                        {
                            // Bring tab to front for accurate screenshot (ignore errors)
                            try { await cdp.SendAsync("Page.bringToFront"); } catch { }

                            // Scroll image into view, wait 2 frames for reflow, measure rect
                            var rectJson = await cdp.EvalAsync($$"""
                                (async () => {
                                    var img = {{findImgJs}};
                                    if (!img) return '';
                                    img.scrollIntoView({block:'center', behavior:'instant'});
                                    // Wait 2 animation frames for scroll + reflow
                                    await new Promise(r => requestAnimationFrame(() => requestAnimationFrame(r)));
                                    var r = img.getBoundingClientRect();
                                    var dpr = window.devicePixelRatio || 1;
                                    if (r.width < 10 || r.height < 10) return '';
                                    // Clamp to viewport
                                    var x = Math.max(0, r.x), y = Math.max(0, r.y);
                                    var w = Math.min(r.width, window.innerWidth - x);
                                    var h = Math.min(r.height, window.innerHeight - y);
                                    if (w < 10 || h < 10) return '';
                                    return JSON.stringify({x:x, y:y, w:w, h:h, dpr:dpr});
                                })()
                                """, awaitPromise: true) ?? "";
                            if (!string.IsNullOrEmpty(rectJson))
                            {
                                var rect = JsonDocument.Parse(rectJson).RootElement;
                                var rx = rect.GetProperty("x").GetDouble();
                                var ry = rect.GetProperty("y").GetDouble();
                                var rw = rect.GetProperty("w").GetDouble();
                                var rh = rect.GetProperty("h").GetDouble();
                                var dpr = rect.TryGetProperty("dpr", out var dprEl) ? dprEl.GetDouble() : 1.0;

                                Console.WriteLine($"[ASK] Screenshot clip: x={rx:F0} y={ry:F0} w={rw:F0} h={rh:F0} dpr={dpr}");
                                var ssResult = await cdp.SendAsync("Page.captureScreenshot", new JsonObject
                                {
                                    ["format"] = "png",
                                    ["clip"] = new JsonObject
                                    {
                                        ["x"] = rx, ["y"] = ry,
                                        ["width"] = rw, ["height"] = rh,
                                        ["scale"] = 1
                                    }
                                });
                                var ssB64 = (ssResult as System.Text.Json.Nodes.JsonObject)?["data"]?.GetValue<string>();
                                if (!string.IsNullOrEmpty(ssB64))
                                {
                                    bytes = Convert.FromBase64String(ssB64);
                                    w = (int)(rw * dpr); h = (int)(rh * dpr);
                                    Console.WriteLine($"[ASK] Image captured via screenshot ({w}x{h}, dpr={dpr:F1})");
                                }
                            }
                        }
                        catch (Exception ssEx)
                        {
                            Console.WriteLine($"[ASK] Screenshot fallback failed: {ssEx.GetType().Name}: {ssEx.Message}");
                            Console.WriteLine($"[ASK] Screenshot stack: {ssEx.StackTrace?.Split('\n').FirstOrDefault()?.Trim()}");
                        }
                    }

                    if (bytes != null && bytes.Length > 100)
                    {
                        // Detect actual format from magic bytes for extension
                        var ext = ".png";
                        if (bytes.Length > 3 && bytes[0] == 0xFF && bytes[1] == 0xD8) ext = ".jpg";
                        else if (bytes.Length > 4 && bytes[0] == 0x52 && bytes[1] == 0x49) ext = ".webp";
                        fileName = $"{aiName}_gen_{timestamp}_{idx}{ext}";
                        filePath = Path.Combine(outputDir, fileName);

                        File.WriteAllBytes(filePath, bytes);
                        saved.Add(filePath);
                        Console.WriteLine();
                        Console.WriteLine($"[image:{fileName} ({w}x{h}, {bytes.Length / 1024}KB)]");
                        Console.Out.Flush();
                    }
                    else
                    {
                        Console.WriteLine($"[ASK] Image download empty/small for: {src.Substring(0, Math.Min(80, src.Length))}");
                    }
                }
                catch (Exception ex)
                {
                    LogWarning("ASK", "Image save failed", ex);
                }
            }
        }
        catch (Exception ex)
        {
            // Non-fatal ??image detection is best-effort
            if (!ex.Message.Contains("JSON")) // suppress parse noise
                LogWarning("ASK", "Image detect error", ex);
        }
        return saved;
    }
}
