using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Vision;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: shared helpers for control discovery, dialog content, notice handling, interactive learning
internal partial class Program
{
    /// <summary>
    /// Find MFC custom combo-like controls: AfxWnd parent with an enabled Edit child,
    /// small size, positioned above the target button. Direct Win32 tree walk.
    /// </summary>
    /// <summary>Flat enumeration of all child windows (for finding dropdown ListBox etc).</summary>
    private static void FindAllChildrenFlat(IntPtr hParent, List<WindowInfo> results, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            results.Add(child);
            FindAllChildrenFlat(child.Handle, results, depth + 1, maxDepth);
        }
    }

    private static void FindMfcCombos(IntPtr hParent, RECT buttonRect, List<WindowInfo> results, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            if (!child.IsVisible) continue;

            // Check if this AfxWnd looks like a combo container
            if (child.ClassName.StartsWith("Afx") &&
                child.Rect.Height >= 20 && child.Rect.Height <= 35 &&
                child.Rect.Width >= 50 && child.Rect.Width <= 200 &&
                child.Rect.Top >= buttonRect.Top - 80 &&
                child.Rect.Top <= buttonRect.Top)
            {
                // Check for Edit child that is enabled
                var grandChildren = WindowFinder.GetChildrenZOrder(child.Handle);
                var edit = grandChildren.FirstOrDefault(c => c.ClassName == "Edit" && NativeMethods.IsWindowEnabled(c.Handle));
                if (edit != null)
                {
                    // Read current text
                    var sb = new StringBuilder(256);
                    NativeMethods.SendMessageW(edit.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, sb);
                    var currentText = sb.ToString();

                    // Skip account number (has dash like "5229-9737") and stock code (cid=32760)
                    if (currentText.Contains("-") && currentText.Length >= 8) goto recurse;
                    if (edit.ControlId == 32760) goto recurse;

                    // Skip disabled containers
                    if (!NativeMethods.IsWindowEnabled(child.Handle)) goto recurse;

                    results.Add(child);
                    continue; // don't recurse into combos
                }
            }

            recurse:
            FindMfcCombos(child.Handle, buttonRect, results, depth + 1, maxDepth);
        }
    }

    private static void DumpFormTree(IntPtr hParent, int level, int maxDepth)
    {
        if (level > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        var indent = new string(' ', level * 2);

        foreach (var child in children)
        {
            // Read text via WM_GETTEXT
            var sb = new StringBuilder(512);
            NativeMethods.SendMessageW(child.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)512, sb);
            var text = sb.ToString();
            var textDisplay = string.IsNullOrEmpty(text) ? "" : $" \"{text}\"";

            var vis = child.IsVisible ? "" : " [H]";
            var en = NativeMethods.IsWindowEnabled(child.Handle) ? "" : " [disabled]";

            // Color by class
            var color = child.ClassName switch
            {
                "Button" => ConsoleColor.Yellow,
                "ComboBox" => ConsoleColor.Blue,
                "Edit" => ConsoleColor.Green,
                "Static" => ConsoleColor.DarkGray,
                "ListBox" => ConsoleColor.Cyan,
                _ when child.ClassName.StartsWith("Afx") => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };

            Console.ForegroundColor = color;
            Console.Write($"{indent}[{child.ClassName}]");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" cid={child.ControlId}");
            if (!string.IsNullOrEmpty(textDisplay))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(textDisplay);
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {child.Rect.Width}x{child.Rect.Height} @({child.Rect.Left},{child.Rect.Top}){vis}{en}");
            Console.WriteLine();
            Console.ResetColor();

            DumpFormTree(child.Handle, level + 1, maxDepth);
        }
    }

    /// <summary>
    /// Recursively find all controls inside a window (buttons, combos, edits, etc).
    /// Buttons are found at leaf level; ComboBox is NOT recursed into (it has internal Edit+ListBox).
    /// </summary>
    private static void FindControlsRecursive(
        IntPtr hParent, string path, int depth, int maxDepth,
        List<(WindowInfo Info, int Depth, string Path)> results)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            var childPath = string.IsNullOrEmpty(path) ? child.ClassName : $"{path}>{child.ClassName}";

            // Record interesting control types
            if (child.ClassName is "Button" or "ComboBox" or "Edit" or "Static" or "ListBox"
                or "SysListView32" or "SysTreeView32")
            {
                results.Add((child, depth, childPath));
            }

            // Also record MFC owner-drawn buttons (AfxWnd with small size that look like buttons)
            if (child.ClassName.StartsWith("Afx") && child.Rect.Width > 10 && child.Rect.Height > 10
                && child.Rect.Width < 200 && child.Rect.Height < 60 && child.IsVisible)
            {
                // Check if it has button-like text (from GetWindowText)
                if (!string.IsNullOrEmpty(child.Title))
                    results.Add((child, depth, childPath));
            }

            // Don't recurse into ComboBox (has internal Edit+ListBox children)
            if (child.ClassName != "ComboBox")
            {
                FindControlsRecursive(child.Handle, childPath, depth + 1, maxDepth, results);
            }
        }
    }

    /// <summary>
    /// Read and display contents of a dialog/popup window.
    /// Uses Win32 GetWindowText first, falls back to OCR for owner-drawn content.
    /// </summary>
    /// <summary>
    /// Build class hierarchy path by walking GetParent chain.
    /// Returns "GrandparentClass/ParentClass/MyClass" (up to 5 levels, stops at desktop/null).
    /// </summary>
    private static string BuildClassPath(IntPtr hWnd)
    {
        var parts = new List<string>();
        var current = hWnd;
        for (int i = 0; i < 5 && current != IntPtr.Zero; i++)
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(current, sb, 256);
            var cls = sb.ToString();
            if (string.IsNullOrEmpty(cls)) break;
            parts.Add(cls);
            current = NativeMethods.GetParent(current);
        }
        parts.Reverse(); // root -> leaf order
        return string.Join("/", parts);
    }

    // -- Notice content reading + importance classification --------─

    private enum NoticeImportance { Normal, Critical }

    /// <summary>
    /// Critical keywords: notices containing these should NOT be auto-dismissed.
    /// Trading-critical events that require user attention.
    /// </summary>
    private static readonly string[] CriticalKeywords = {
        "긴급", "장애", "거래중지", "거래정지", "시스템장애", "서버장애",
        "점검시간변경", "임시점검", "긴급점검",
        "증거금률변경", "위탁증거금", "반대매매",
        "상장폐지", "매매거래정지", "투자경고",
        "해킹", "보안", "개인정보유출"
    };

    /// <summary>
    /// Read notice content from an MDI child window via OCR.
    /// Returns the full text content, or null if unreadable.
    /// </summary>
    private static string? ReadNoticeContent(IntPtr hWnd, string formTitle)
    {
        try
        {
            // Try WM_GETTEXT on child controls first
            var children = new List<WindowInfo>();
            FindAllChildrenFlat(hWnd, children, 0, 4);

            var texts = new List<string>();
            foreach (var child in children.Where(c => c.IsVisible))
            {
                var text = child.Title;
                if (string.IsNullOrEmpty(text))
                    text = GetWmGetText(child.Handle);
                if (!string.IsNullOrWhiteSpace(text) && text.Length > 2)
                    texts.Add(text);
            }

            // If we got reasonable text from WM_GETTEXT, use it
            if (texts.Count > 0)
            {
                var combined = string.Join(" ", texts);
                if (combined.Length > 10) return combined;
            }

            // Fallback: OCR the window
            using var bmp = ScreenCapture.CaptureWindow(hWnd);
            if (bmp.Width < 30 || bmp.Height < 30) return null;

            var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
            var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
            using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.3f);
            var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();

            if (!string.IsNullOrWhiteSpace(result.FullText))
                return result.FullText;

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Classify notice importance based on critical keywords.
    /// </summary>
    private static NoticeImportance ClassifyNoticeImportance(string text)
    {
        foreach (var kw in CriticalKeywords)
        {
            if (text.Contains(kw, StringComparison.OrdinalIgnoreCase))
                return NoticeImportance.Critical;
        }
        return NoticeImportance.Normal;
    }

    /// <summary>
    /// Print notice text in a readable format (indented, max 5 lines).
    /// </summary>
    private static void PrintNoticeText(string text)
    {
        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int maxLines = 8;
        for (int i = 0; i < Math.Min(lines.Length, maxLines); i++)
        {
            var line = lines[i].Trim();
            if (line.Length > 120) line = line[..117] + "...";
            Console.WriteLine($"    | {line}");
        }
        if (lines.Length > maxLines)
            Console.WriteLine($"    | ... ({lines.Length - maxLines} more lines)");
    }

    /// <summary>
    /// Get text from a window using WM_GETTEXT (works for some owner-drawn controls).
    /// </summary>
    private static string GetWmGetText(IntPtr hWnd)
    {
        var len = (int)NativeMethods.SendMessageW(hWnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
        if (len <= 0) return "";
        var sb = new StringBuilder(len + 2);
        NativeMethods.SendMessageW(hWnd, NativeMethods.WM_GETTEXT, (IntPtr)(len + 1), sb);
        return sb.ToString();
    }

    /// <summary>
    /// Determine if a child window looks like a button.
    /// Standard "Button" class + small owner-drawn AfxWnd110 controls in MFC dialogs.
    /// </summary>
    private static bool IsButtonLike(WindowInfo c)
    {
        // Standard Win32 Button class (includes BS_OWNERDRAW variants)
        if (c.ClassName == "Button") return true;

        // MFC owner-drawn: small clickable AfxWnd110 controls
        // Typical button: 50~120px wide, 18~30px tall
        if (c.ClassName.StartsWith("AfxWnd") || c.ClassName.StartsWith("Afx:"))
        {
            if (c.Rect.Width >= 30 && c.Rect.Width <= 200 &&
                c.Rect.Height >= 15 && c.Rect.Height <= 40)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Extract Static/Label text from a dialog (for handler message_contains matching).
    /// Does NOT print to console -- use ReadDialogContents for that.
    /// </summary>
    private static string GetDialogMessageText(IntPtr hDialog)
    {
        var texts = new List<string>();
        var children = WindowFinder.GetChildrenZOrder(hDialog);

        foreach (var child in children)
        {
            if (child.ClassName != "Static") continue;
            var text = WindowFinder.GetWindowText(child.Handle);
            if (!string.IsNullOrWhiteSpace(text))
                texts.Add(text);
        }

        // Also check nested dialogs
        foreach (var child in children)
        {
            if (child.ClassName == "#32770" || child.ClassName.StartsWith("Afx"))
            {
                foreach (var sc in WindowFinder.GetChildrenZOrder(child.Handle))
                {
                    if (sc.ClassName != "Static") continue;
                    var text = WindowFinder.GetWindowText(sc.Handle);
                    if (!string.IsNullOrWhiteSpace(text))
                        texts.Add(text);
                }
            }
        }

        // Fallback: OCR if no Static text found (owner-drawn dialogs)
        if (texts.Count == 0)
        {
            try
            {
                using var bmp = ScreenCapture.CaptureWindow(hDialog);
                if (bmp.Width > 20 && bmp.Height > 20)
                {
                    var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                    var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                    using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.5f);
                    var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                    if (result.Words.Count > 0)
                        texts.Add(string.Join(" ", result.Words.Select(w => w.Text)));
                }
            }
            catch { /* OCR not critical */ }
        }

        return string.Join(" ", texts);
    }

    /// <summary>
    /// Interactive button learning for unknown dialogs.
    /// Shows buttons, asks user to hover mouse over desired button for 1+ second,
    /// then clicks it and auto-generates a handler YAML.
    /// Returns: (clicked, shouldRetry, buttonText) -- same semantics as TryHandleBlocker.
    /// </summary>
    private static (bool clicked, bool shouldRetry) InteractiveButtonLearn(
        IntPtr hDialog, string windowTitle, string classPath, string className,
        string processName, string handlersDir)
    {
        // Find all buttons in the dialog (including MFC owner-drawn)
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(hDialog, allChildren, 0, 3);
        var buttons = allChildren.Where(c => c.IsVisible && c.Rect.Width > 10 && IsButtonLike(c))
            .OrderBy(b => b.Rect.Left).ToList();

        if (buttons.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[BLOCK] No buttons found in dialog -- cannot learn.");
            Console.ResetColor();
            return (false, false);
        }

        // Show available buttons
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[BLOCK] Unknown dialog -- interactive learning mode!");
        Console.WriteLine("[BLOCK] Hover your mouse over the button you want, and hold for 1 second.");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            var txt = !string.IsNullOrEmpty(b.Title) ? $"\"{b.Title}\"" : "(owner-drawn)";
            Console.WriteLine($"  [{i}] {txt} {b.Rect.Width}x{b.Rect.Height} @({b.Rect.Left},{b.Rect.Top})");
        }
        Console.ResetColor();

        // Alert user with beep + flash
        NativeMethods.MessageBeep(0x00000030); // MB_ICONEXCLAMATION
        NativeMethods.FlashWindow(hDialog, true);

        // Poll mouse position: detect hover over a button for 1+ second
        const int pollIntervalMs = 100;
        const int hoverThresholdMs = 1000;
        const int totalTimeoutMs = 15000;

        IntPtr hoveredBtn = IntPtr.Zero;
        int hoverDurationMs = 0;
        int elapsedMs = 0;

        while (elapsedMs < totalTimeoutMs)
        {
            Thread.Sleep(pollIntervalMs);
            elapsedMs += pollIntervalMs;

            // Check if dialog is still alive
            if (!NativeMethods.IsWindow(hDialog) || !NativeMethods.IsWindowVisible(hDialog))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[BLOCK] Dialog disappeared during learning.");
                Console.ResetColor();
                return (false, false);
            }

            NativeMethods.GetCursorPos(out var cursorPos);

            // Check which button the cursor is over
            IntPtr currentBtn = IntPtr.Zero;
            foreach (var btn in buttons)
            {
                NativeMethods.GetWindowRect(btn.Handle, out var rect);
                if (cursorPos.X >= rect.Left && cursorPos.X <= rect.Right &&
                    cursorPos.Y >= rect.Top && cursorPos.Y <= rect.Bottom)
                {
                    currentBtn = btn.Handle;
                    break;
                }
            }

            if (currentBtn != IntPtr.Zero && currentBtn == hoveredBtn)
            {
                hoverDurationMs += pollIntervalMs;

                // Show progress
                if (hoverDurationMs % 500 == 0 && hoverDurationMs < hoverThresholdMs)
                {
                    var btnInfo = buttons.First(b => b.Handle == currentBtn);
                    var btnText = !string.IsNullOrEmpty(btnInfo.Title) ? $"\"{btnInfo.Title}\"" : "(owner-drawn)";
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"\r[BLOCK] Hovering over {btnText}... {hoverDurationMs}ms / {hoverThresholdMs}ms");
                    Console.ResetColor();
                }

                if (hoverDurationMs >= hoverThresholdMs)
                {
                    // Button selected!
                    var selected = buttons.First(b => b.Handle == currentBtn);
                    var selectedText = !string.IsNullOrEmpty(selected.Title) ? selected.Title : null;
                    var displayText = selectedText ?? "(owner-drawn)";

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\r[BLOCK] Button selected: \"{displayText}\" -- clicking now!          ");
                    Console.ResetColor();

                    // Click the button
                    bool clicked = SmartClickButton(selected.Handle, hDialog);
                    if (clicked)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[BLOCK] Button clicked successfully.");
                        Console.ResetColor();

                        // Auto-generate handler YAML with the selected button
                        var handlerPath = GenerateLearnedHandler(
                            handlersDir, windowTitle, classPath, className, processName,
                            selectedText, buttons.IndexOf(selected));

                        if (handlerPath != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"[BLOCK] Handler learned and saved: {Path.GetFileName(handlerPath)}");
                            Console.WriteLine("[BLOCK] Next time this dialog appears, it will be handled automatically!");
                            Console.ResetColor();
                        }

                        return (true, true); // clicked + retry
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[BLOCK] Click failed.");
                        Console.ResetColor();
                        return (false, false);
                    }
                }
            }
            else
            {
                // Cursor moved to different button or outside
                hoveredBtn = currentBtn;
                hoverDurationMs = currentBtn != IntPtr.Zero ? pollIntervalMs : 0;
            }
        }

        // Timeout -- no button selected
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\r[BLOCK] No button selected within {totalTimeoutMs / 1000}s -- giving up.          ");
        Console.ResetColor();

        // Still generate sample handler for manual editing
        var samplePath = DialogHandlerManager.GenerateSampleHandler(
            handlersDir, windowTitle, classPath, className, processName);
        if (samplePath != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[BLOCK] Sample handler created: {Path.GetFileName(samplePath)}");
            Console.WriteLine("[BLOCK] Edit this file to configure auto-handling, then re-run.");
            Console.ResetColor();
        }

        return (false, false);
    }

    /// <summary>
    /// Generate a complete handler YAML from interactive learning.
    /// The button selected by the user becomes the action target.
    /// </summary>
    private static string? GenerateLearnedHandler(
        string handlersDir, string windowTitle, string classPath, string className,
        string processName, string? buttonText, int buttonIndex)
    {
        // Build keyword from classPath leaf or title
        var keyword = !string.IsNullOrWhiteSpace(windowTitle)
            ? windowTitle.Trim()
            : className; // fallback to class name

        // Sanitize for filename
        keyword = string.Join("", keyword.Split(Path.GetInvalidFileNameChars())).Trim();
        if (keyword.Length > 30) keyword = keyword[..30];
        if (string.IsNullOrEmpty(keyword)) keyword = className;

        try
        {
            Directory.CreateDirectory(handlersDir);
            var filePath = Path.Combine(handlersDir, $"{keyword}.yaml");

            // Don't overwrite existing handler
            if (File.Exists(filePath))
            {
                // Append _learned suffix
                filePath = Path.Combine(handlersDir, $"{keyword}_learned.yaml");
                if (File.Exists(filePath)) return null;
            }

            var buttonSection = buttonText != null
                ? $"  button_text: \"{buttonText}\"   # learned from user hover"
                : $"  button_index: {buttonIndex}     # learned from user hover (owner-drawn, no text)";

            var yaml = $@"# Auto-learned handler for: ""{windowTitle}""
# Class hierarchy: {classPath}
# Generated by interactive mouse-hover learning.

match:
  class: ""{className}""
  process: ""{processName}""

action: click_button

params:
{buttonSection}
  wait_after: 5000
  retry: true
";
            File.WriteAllText(filePath, yaml);
            return filePath;
        }
        catch
        {
            return null;
        }
    }

    private static void ReadDialogContents(IntPtr hDialog)
    {
        var children = WindowFinder.GetChildrenZOrder(hDialog);
        bool foundText = false;

        // Pass 1: Try Win32 GetWindowText on children
        foreach (var child in children)
        {
            var text = WindowFinder.GetWindowText(child.Handle);
            if (string.IsNullOrWhiteSpace(text)) continue;

            if (child.ClassName == "Static")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("         message: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\"{text}\"");
                Console.ResetColor();
                foundText = true;
            }
            else if (child.ClassName == "Button")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"         button: [{text}]");
                Console.ResetColor();
            }
        }

        // Recurse one level for nested dialogs
        foreach (var child in children)
        {
            if (child.ClassName == "#32770" || child.ClassName.StartsWith("Afx"))
            {
                var subChildren = WindowFinder.GetChildrenZOrder(child.Handle);
                foreach (var sc in subChildren)
                {
                    var text = WindowFinder.GetWindowText(sc.Handle);
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    if (sc.ClassName == "Static")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("         message: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\"{text}\"");
                        Console.ResetColor();
                        foundText = true;
                    }
                    else if (sc.ClassName == "Button")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"         button: [{text}]");
                        Console.ResetColor();
                    }
                }
            }
        }

        // Pass 2: If no Static text found, try OCR on the dialog window
        if (!foundText)
        {
            try
            {
                using var bmp = ScreenCapture.CaptureWindow(hDialog);
                if (bmp.Width > 20 && bmp.Height > 20)
                {
                    var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                    var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                    using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.5f);

                    var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                    if (result.Words.Count > 0)
                    {
                        // Filter out button text, keep dialog message text only
                        var buttonTexts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var child in children)
                        {
                            var bt = WindowFinder.GetWindowText(child.Handle);
                            if (!string.IsNullOrWhiteSpace(bt)) buttonTexts.Add(bt.Trim());
                        }

                        var messageWords = result.Words
                            .Where(w => !buttonTexts.Contains(w.Text.Trim()))
                            .Select(w => w.Text)
                            .ToList();

                        if (messageWords.Count > 0)
                        {
                            var ocrMsg = string.Join(" ", messageWords);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("         OCR: ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\"{ocrMsg}\"");
                            Console.ResetColor();
                        }
                    }
                }
            }
            catch { /* OCR not critical */ }
        }
    }
}
