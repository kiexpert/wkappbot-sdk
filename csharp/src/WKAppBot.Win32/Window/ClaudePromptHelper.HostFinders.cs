using System.Diagnostics;
using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Find prompt in Claude Desktop (Electron) window.
    /// UIA path: Window -> ... -> Document "Claude" aid="RootWebArea" -> Group aid="turn-form" -> Group "Type here"
    /// </summary>
    private PromptInfo? FindClaudeDesktopPrompt(int processId)
    {
        // Find all visible top-level windows from this PID
        var windows = FindWindowsByProcessId(processId);
        Console.WriteLine($"  [PROMPT] PID={processId}: {windows.Count} visible window(s)");
        foreach (var hWnd in windows)
        {
            var wTitle = WindowFinder.GetWindowText(hWnd);
            var wClass = WindowFinder.GetClassName(hWnd);
            NativeMethods.GetWindowRect(hWnd, out var wRect);
            var wSize = $"{wRect.Right - wRect.Left}x{wRect.Bottom - wRect.Top}";
            Console.WriteLine($"  [PROMPT]   hwnd=0x{hWnd:X} \"{wTitle}\" class={wClass} {wSize}");
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null)
                {
                    Console.WriteLine($"  [PROMPT]     UIA.FromHandle=null (no UIA tree)");
                    continue;
                }

                // Look for the turn-form group (the prompt container)
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm == null)
                {
                    // Log what we DID find at the top level (for diagnosis)
                    try
                    {
                        var topAids = new List<string>();
                        var topChildren = root.FindAllChildren();
                        foreach (var child in topChildren)
                        {
                            try
                            {
                                var aid = child.AutomationId;
                                var ct = child.ControlType;
                                topAids.Add($"{ct}(aid={aid})");
                                // Check one level deeper for RootWebArea or named elements
                                foreach (var gc in child.FindAllChildren())
                                {
                                    try
                                    {
                                        var gcAid = gc.AutomationId;
                                        if (!string.IsNullOrEmpty(gcAid))
                                            topAids.Add($"  {gc.ControlType}(aid={gcAid})");
                                    }
                                    catch { }
                                }
                            }
                            catch { }
                            if (topAids.Count > 20) { topAids.Add("...(truncated)"); break; }
                        }
                        Console.WriteLine($"  [PROMPT]     turn-form NOT found. UIA tree: {string.Join(", ", topAids)}");
                    }
                    catch (Exception diagEx)
                    {
                        Console.WriteLine($"  [PROMPT]     turn-form NOT found (UIA tree read error: {diagEx.Message})");
                    }
                    continue;
                }

                Console.WriteLine($"  [PROMPT]     turn-form FOUND at ({turnForm.BoundingRectangle})");

                // Found the prompt container! Get the input group
                // It's a child Group with a placeholder-like name.
                var inputGroup = turnForm.FindFirstChild(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.ControlType,
                        ControlType.Group));

                if (inputGroup == null)
                {
                    // Try finding by contentEditable behavior -- the first child Group
                    var children = turnForm.FindAllChildren();
                    inputGroup = children.FirstOrDefault(c =>
                        c.ControlType == ControlType.Group &&
                        c.BoundingRectangle.Width > 100);
                }

                if (inputGroup == null)
                {
                    // Log turn-form children for diagnosis
                    try
                    {
                        var tfChildren = turnForm.FindAllChildren();
                        var tfInfo = string.Join(", ", tfChildren.Select(c =>
                            $"{c.ControlType}(\"{c.Name}\",{c.BoundingRectangle.Width}x{c.BoundingRectangle.Height})"));
                        Console.WriteLine($"  [PROMPT]     inputGroup NOT found! turn-form children: {tfInfo}");
                    }
                    catch { Console.WriteLine($"  [PROMPT]     inputGroup NOT found (children read error)"); }
                    continue;
                }

                var rect = inputGroup.BoundingRectangle;

                Console.WriteLine($"  [PROMPT] Found Claude Desktop prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                return new PromptInfo(
                    hWnd,
                    wTitle,
                    "claude",
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    "claude-desktop"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT]     Error: {ex.Message}");
            }
        }
        return null;
    }

    /// <summary>
    /// Find prompt in VS Code with Claude Code extension.
    /// The Claude Code panel in VS Code may have different UIA structure.
    /// </summary>
    private PromptInfo? FindVSCodePrompt(int processId, string? callerCwd = null)
    {
        var windows = FindWindowsByProcessId(processId);

        // If callerCwd is known, prefer the window whose title contains the folder name.
        // VS Code window title format: "filename - FolderName - Visual Studio Code"
        if (!string.IsNullOrEmpty(callerCwd))
        {
            var folderName = System.IO.Path.GetFileName(callerCwd.TrimEnd('\\', '/'));
            if (!string.IsNullOrEmpty(folderName))
            {
                var preferred = windows.FirstOrDefault(h =>
                    WindowFinder.GetWindowText(h).Contains(folderName, StringComparison.OrdinalIgnoreCase));
                if (preferred != IntPtr.Zero)
                {
                    // Move matching window to front so it's checked first
                    windows.Remove(preferred);
                    windows.Insert(0, preferred);
                }
            }
        }
        foreach (var hWnd in windows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                // VS Code Claude Code panel -- look for turn-form or similar
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm != null)
                {
                    var inputGroup = turnForm.FindFirstChild(
                        new PropertyCondition(
                            _automation.PropertyLibrary.Element.ControlType,
                            ControlType.Group));

                    if (inputGroup != null)
                    {
                        var rect = inputGroup.BoundingRectangle;
                        var title = WindowFinder.GetWindowText(hWnd);

                        Console.WriteLine($"  [PROMPT] Found VS Code Claude prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                        return new PromptInfo(
                            hWnd,
                            title,
                            "Code",
                            new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                            ClassifyVsCodeHostType(title)
                        );
                    }
                }
            }
            catch { }
        }

        foreach (var hWnd in windows)
        {
            var codexPrompt = TryFindFocusedVsCodeCodexPrompt(hWnd);
            if (codexPrompt != null) return codexPrompt;
        }

        // Fallback: VS Code Claude Code extension (no turn-form, native panel)
        // Detect by window title containing "Visual Studio Code"
        foreach (var hWnd in windows)
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) continue;
            var title = WindowFinder.GetWindowText(hWnd);
            if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) continue;
            if (IsLikelyVsCodeCodexWindowTitle(title)) continue;
            if (ShouldSkipVsCodePromptFallbackWindow(title)) continue;

            // Use full window rect (will use keyboard shortcut to focus input)
            NativeMethods.GetWindowRect(hWnd, out var wr);
            var rect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
            var hostType = ClassifyVsCodeHostType(title);

            Console.WriteLine($"  [PROMPT] Found VS Code Claude host ({hostType}): \"{title}\" at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

            return new PromptInfo(hWnd, title, "Code", rect, hostType);
        }

        return null;
    }

    /// <summary>
    /// Get ancestor process chain: current -> parent -> grandparent -> ...
    /// Stops when parent is not accessible or reaches PID 0/1.
    /// </summary>
    /// <summary>
    /// Find prompt in Codex desktop app window.
    /// Codex composer is not exposed as a writable Edit control in UIA, so we detect
    /// stable placeholder/marker text and use window-level focusless WM_CHAR injection.
    /// </summary>
    private PromptInfo? FindCodexDesktopPromptByMarker(int processId)
    {
        var windows = GetCodexCandidateWindows(processId);
        foreach (var hWnd in windows)
        {
            try
            {
                var title = WindowFinder.GetWindowText(hWnd);
                if (string.IsNullOrWhiteSpace(title) ||
                    !title.Contains("Codex", StringComparison.OrdinalIgnoreCase))
                    continue;

                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                NativeMethods.GetWindowRect(hWnd, out var wr0);
                var winRect0 = Rectangle.FromLTRB(wr0.Left, wr0.Top, wr0.Right, wr0.Bottom);
                var messageInputRect = TryResolvePromptRectFromMessageInput(root, winRect0);
                if (messageInputRect != Rectangle.Empty)
                {
                    Console.WriteLine($"  [PROMPT] Found Codex prompt by Message input: ({messageInputRect.X},{messageInputRect.Y} {messageInputRect.Width}x{messageInputRect.Height})");
                    return new PromptInfo(hWnd, title, "codex", messageInputRect, HostCodexDesktop);
                }

                // If Codex permission/approval dialog is open, accept it first so
                // prompt routing does not get stuck on the blocker UI.
                if (TryDismissCodexApprovalDialog(root))
                {
                    Thread.Sleep(250);
                    root = _automation.FromHandle(hWnd);
                    if (root == null) continue;
                }

                // Fast-path: ElementFromPoint probes at bottom of window -- prompt bar is always near bottom.
                // O(1) vs O(N) for FindAllDescendants. Walk parent chain ≤4 levels to validate.
                AutomationElement? fastMarker = null;
                try
                {
                    NativeMethods.GetWindowRect(hWnd, out var fastWr);
                    var midX = (fastWr.Left + fastWr.Right) / 2;
                    // Probe 3 anchor points: bottom-center, bottom-left quarter, bottom-right quarter
                    var probes = new[] {
                        new System.Drawing.Point(midX, fastWr.Bottom - 60),
                        new System.Drawing.Point(fastWr.Left + (fastWr.Right - fastWr.Left) / 4, fastWr.Bottom - 60),
                        new System.Drawing.Point(fastWr.Right - (fastWr.Right - fastWr.Left) / 4, fastWr.Bottom - 60),
                    };
                    foreach (var pt in probes)
                    {
                        var el = _automation.FromPoint(pt);
                        if (el == null) continue;
                        // Walk up ≤4 levels looking for Codex marker
                        var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
                        var cur = el;
                        for (int i = 0; i < 4 && cur != null; i++)
                        {
                            var n = cur.Properties.Name.ValueOrDefault ?? "";
                            if (!string.IsNullOrWhiteSpace(n) && n.Length <= 120 &&
                                (n.Contains("follow-up", StringComparison.OrdinalIgnoreCase) ||
                                 n.Contains("message Codex", StringComparison.OrdinalIgnoreCase) ||
                                 n.Contains("What are we coding next", StringComparison.OrdinalIgnoreCase) ||
                                 n.Contains("Terminal input", StringComparison.OrdinalIgnoreCase) ||
                                 n.Contains("후속 변경", StringComparison.OrdinalIgnoreCase)))
                            { fastMarker = cur; break; }
                            cur = walker.GetParent(cur);
                        }
                        if (fastMarker != null) break;
                    }
                }
                catch { }

                if (fastMarker != null)
                {
                    var fastMr = fastMarker.BoundingRectangle;
                    NativeMethods.GetWindowRect(hWnd, out var fastWr2);
                    var fastWinRect = new Rectangle(fastWr2.Left, fastWr2.Top, fastWr2.Width, fastWr2.Height);
                    var fastX = Math.Max(fastWinRect.Left + 4, fastMr.X - 16);
                    var fastY = Math.Max(fastWinRect.Top + 4, fastMr.Y - 6);
                    var fastW = Math.Max(360, Math.Min(fastWinRect.Right - fastX - 4, fastMr.Width + 32));
                    var fastH = Math.Max(28, fastMr.Height + 12);
                    Console.WriteLine($"  [PROMPT] Codex fast-probe hit: \"{fastMarker.Properties.Name.ValueOrDefault}\" ({fastW}x{fastH})");
                    return new PromptInfo(hWnd, title, "codex", new Rectangle(fastX, fastY, fastW, fastH), HostCodexDesktop);
                }

                // Fallback: full tree scan (slow -- FindAllDescendants on entire Codex tree).
                var markers = root.FindAllDescendants()
                    .Where(e =>
                    {
                        try
                        {
                            var n = e.Name ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(n)) return false;
                            if (n.Length > 120 || n.Contains('\n') || n.Contains('\r')) return false;
                            var ct = e.ControlType;
                            if (ct != ControlType.Button && ct != ControlType.Text && ct != ControlType.Edit)
                                return false;
                            if (n.Contains("Ask follow-up changes", StringComparison.OrdinalIgnoreCase) ||
                                n.Contains("follow-up", StringComparison.OrdinalIgnoreCase) ||
                                n.Contains("message Codex", StringComparison.OrdinalIgnoreCase) ||
                                n.Contains("What are we coding next", StringComparison.OrdinalIgnoreCase) ||
                                n.Contains("Terminal input", StringComparison.OrdinalIgnoreCase))
                                return true;
                            return n.Contains("후속 변경 사항을 부탁하세요", StringComparison.OrdinalIgnoreCase) ||
                                   n.Contains("여기가 프롬프트", StringComparison.OrdinalIgnoreCase);
                        }
                        catch { return false; }
                    })
                    .ToList();

                if (markers.Count == 0)
                    continue;

                // Prefer marker closest to bottom (actual composer placeholder zone).
                var marker = markers
                    .OrderByDescending(m => m.BoundingRectangle.Bottom)
                    .First();
                var mr = marker.BoundingRectangle;

                NativeMethods.GetWindowRect(hWnd, out var wr);
                var windowRect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);

                // Expand marker rect to a practical input target band.
                var x = Math.Max(windowRect.Left + 4, mr.X - 16);
                var y = Math.Max(windowRect.Top + 4, mr.Y - 6);
                var w = Math.Max(360, Math.Min(windowRect.Right - x - 4, mr.Width + 32));
                var h = Math.Max(28, mr.Height + 12);
                var promptRect = new Rectangle(x, y, w, h);

                Console.WriteLine($"  [PROMPT] Found Codex prompt marker: \"{marker.Name}\" at ({promptRect.X},{promptRect.Y} {promptRect.Width}x{promptRect.Height})");
                return new PromptInfo(hWnd, title, "codex", promptRect, HostCodexDesktop);
            }
            catch { }
        }
        return null;
    }

    private bool TryDismissCodexApprovalDialog(AutomationElement root)
    {
        try
        {
            var all = root.FindAllDescendants();
            if (all.Length == 0) return false;

            bool hasApprovalContext = all.Any(e =>
            {
                try
                {
                    var n = (e.Name ?? string.Empty).ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(n)) return false;
                    return ApprovalPolicy.ContainsApprovalContext(n);
                }
                catch { return false; }
            });
            if (!hasApprovalContext) return false;

            var buttons = all
                .Where(e =>
                {
                    try
                    {
                        if (e.ControlType != ControlType.Button) return false;
                        if (!e.IsEnabled) return false;
                        var n = (e.Name ?? string.Empty).ToLowerInvariant();
                        if (string.IsNullOrWhiteSpace(n)) return false;
                        return ApprovalPolicy.ContainsApprovalButtonText(n);
                    }
                    catch { return false; }
                })
                .OrderByDescending(b => b.BoundingRectangle.Bottom)
                .ToList();

            foreach (var btn in buttons)
            {
                try
                {
                    if (!btn.Patterns.Invoke.IsSupported) continue;
                    btn.Patterns.Invoke.Pattern.Invoke();
                    Console.WriteLine($"  [PROMPT:CODEX] approval dialog accepted via \"{btn.Name}\"");
                    return true;
                }
                catch { }
            }
        }
        catch { }

        return false;
    }

    internal PromptInfo? TryFindFocusedVsCodeCodexPrompt(IntPtr hWnd)
    {
        if (!NativeMethods.IsWindowVisible(hWnd)) return null;
        var title = WindowFinder.GetWindowText(hWnd);
        if (!IsLikelyVsCodeCodexWindowTitle(title)) return null;

        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return null;

            NativeMethods.GetWindowRect(hWnd, out var wr);
            var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
            var messageInputRect = TryResolvePromptRectFromMessageInput(root, winRect);
            if (messageInputRect != Rectangle.Empty)
            {
                if (!ContainsCurrentCursor(messageInputRect))
                {
                    Console.WriteLine($"  [PROMPT] Reject VS Code Codex Message input (cursor outside): ({messageInputRect.X},{messageInputRect.Y} {messageInputRect.Width}x{messageInputRect.Height}) \"{title}\"");
                    return null;
                }
                Console.WriteLine($"  [PROMPT] Found VS Code Codex prompt by Message input: ({messageInputRect.X},{messageInputRect.Y} {messageInputRect.Width}x{messageInputRect.Height}) \"{title}\"");
                return new PromptInfo(hWnd, title, "Code", messageInputRect, HostVsCodeCodex);
            }

            var focused = GrapHelper.FindFocusedLeaf(_automation, root, hWnd);
            if (focused == null) return null;

            var promptRect = TryResolveVsCodeCodexPromptRectFromFocus(hWnd, focused);
            if (promptRect == Rectangle.Empty) return null;
            if (!ContainsCurrentCursor(promptRect))
            {
                Console.WriteLine($"  [PROMPT] Reject VS Code Codex focus rect (cursor outside): ({promptRect.X},{promptRect.Y} {promptRect.Width}x{promptRect.Height}) \"{title}\"");
                return null;
            }

            Console.WriteLine($"  [PROMPT] Found VS Code Codex prompt by focus: ({promptRect.X},{promptRect.Y} {promptRect.Width}x{promptRect.Height}) \"{title}\"");
            return new PromptInfo(hWnd, title, "Code", promptRect, HostVsCodeCodex);
        }
        catch
        {
            return null;
        }
    }

    private Rectangle TryResolvePromptRectFromMessageInput(AutomationElement root, Rectangle winRect)
    {
        try
        {
            var editEl = root.FindFirst(TreeScope.Descendants, new AndCondition(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Edit),
                new PropertyCondition(_automation.PropertyLibrary.Element.Name, "Message input")));
            if (editEl == null) return Rectangle.Empty;

            var br = editEl.BoundingRectangle;
            var rect = new Rectangle(br.X, br.Y, br.Width, br.Height);
            if (rect.IsEmpty || rect.Width < 120 || rect.Height < 18)
                return Rectangle.Empty;

            var padX = Math.Clamp(rect.Width / 12, 16, 64);
            var padY = Math.Clamp(rect.Height / 3, 8, 28);
            var expanded = Rectangle.FromLTRB(
                Math.Max(winRect.Left + 4, rect.Left - padX),
                Math.Max(winRect.Top + 4, rect.Top - padY),
                Math.Min(winRect.Right - 4, rect.Right + padX),
                Math.Min(winRect.Bottom - 4, rect.Bottom + padY));

            return expanded.Width >= 140 && expanded.Height >= 24 ? expanded : rect;
        }
        catch
        {
            return Rectangle.Empty;
        }
    }

    private static bool ContainsCurrentCursor(Rectangle rect)
    {
        if (rect.IsEmpty || rect.Width <= 0 || rect.Height <= 0)
            return false;

        try
        {
            if (!NativeMethods.TryGetCurrentCursorRect(out var cursorRect))
                return false;
            return rect.Contains(cursorRect);
        }
        catch
        {
            return false;
        }
    }

    private Rectangle TryResolveVsCodeCodexPromptRectFromFocus(IntPtr hWnd, AutomationElement focused)
    {
        NativeMethods.GetWindowRect(hWnd, out var wr);
        var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        if (winRect.IsEmpty) return Rectangle.Empty;

        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root != null)
            {
                var markerRect = TryResolveVsCodeCodexPromptRectFromMarker(root, focused, winRect);
                if (markerRect != Rectangle.Empty)
                    return markerRect;
            }
        }
        catch { }

        var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
        var candidates = new List<(Rectangle Rect, int Score, int Area)>();
        var cur = focused;

        for (int depth = 0; depth < 10 && cur != null; depth++)
        {
            Rectangle rect;
            try
            {
                var br = cur.BoundingRectangle;
                rect = new Rectangle(br.X, br.Y, br.Width, br.Height);
            }
            catch
            {
                rect = Rectangle.Empty;
            }

            if (!rect.IsEmpty &&
                rect.Width >= 220 &&
                rect.Height >= 28 &&
                rect.Height <= 180 &&
                rect.Left >= winRect.Left &&
                rect.Top >= winRect.Top &&
                rect.Right <= winRect.Right + 4 &&
                rect.Bottom <= winRect.Bottom + 4)
            {
                var score = 0;
                if (rect.Bottom >= winRect.Bottom - Math.Min(320, Math.Max(140, winRect.Height / 3))) score += 8;
                if (rect.Top >= winRect.Top + winRect.Height / 4) score += 2;
                if (rect.Width >= Math.Min(360, Math.Max(260, winRect.Width / 4))) score += 4;
                if (rect.Height is >= 32 and <= 120) score += 4;
                if (ElementSupportsTextOrValue(cur)) score += 3;
                if (depth <= 1) score += 2;
                else if (depth <= 3) score += 1;

                candidates.Add((rect, score, rect.Width * rect.Height));
            }

            try { cur = walker.GetParent(cur); }
            catch { break; }
        }

        var best = candidates
            .OrderByDescending(c => c.Score)
            .ThenBy(c => c.Rect.Height)
            .ThenBy(c => c.Area)
            .FirstOrDefault();

        if (best.Rect == Rectangle.Empty || best.Score < 10)
            return Rectangle.Empty;

        var expanded = Rectangle.FromLTRB(
            Math.Max(winRect.Left + 4, best.Rect.Left - 10),
            Math.Max(winRect.Top + 4, best.Rect.Top - 6),
            Math.Min(winRect.Right - 4, best.Rect.Right + 10),
            Math.Min(winRect.Bottom - 4, best.Rect.Bottom + 6));

        return expanded.Width >= 220 && expanded.Height >= 28 ? expanded : best.Rect;
    }

    private Rectangle TryResolveVsCodeCodexPromptRectFromMarker(
        AutomationElement root,
        AutomationElement focused,
        Rectangle winRect)
    {
        var markers = root.FindAllDescendants()
            .Where(e =>
            {
                try
                {
                    var name = (e.Name ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(name) || name.Length > 160) return false;
                    var lower = name.ToLowerInvariant();
                    return lower.Contains("ask follow-up") ||
                           lower.Contains("follow-up changes") ||
                           lower.Contains("message codex") ||
                           lower.Contains("what are we coding next") ||
                           lower.Contains("ask for") ||
                           lower.Contains("ask");
                }
                catch { return false; }
            })
            .Select(e =>
            {
                try
                {
                    var br = e.BoundingRectangle;
                    var rect = new Rectangle(br.X, br.Y, br.Width, br.Height);
                    return (el: e, rect);
                }
                catch
                {
                    return (el: e, rect: Rectangle.Empty);
                }
            })
            .Where(x => !x.rect.IsEmpty && x.rect.Width >= 120 && x.rect.Height >= 16)
            .ToList();

        if (markers.Count == 0)
            return Rectangle.Empty;

        Rectangle focusRect;
        try
        {
            var fr = focused.BoundingRectangle;
            focusRect = new Rectangle(fr.X, fr.Y, fr.Width, fr.Height);
        }
        catch
        {
            focusRect = Rectangle.Empty;
        }

        var best = markers
            .Select(x =>
            {
                var score = 0;
                if (x.rect.Bottom >= winRect.Bottom - Math.Min(320, Math.Max(140, winRect.Height / 3))) score += 10;
                if (x.rect.Left >= winRect.Left + winRect.Width / 2) score += 4;
                if (!focusRect.IsEmpty && Math.Abs(x.rect.Top - focusRect.Top) <= 48) score += 8;
                if (!focusRect.IsEmpty && Math.Abs(x.rect.Bottom - focusRect.Bottom) <= 64) score += 8;
                if (x.rect.Width >= 180) score += 2;
                return (x.rect, score);
            })
            .OrderByDescending(x => x.score)
            .ThenByDescending(x => x.rect.Bottom)
            .FirstOrDefault();

        if (best.rect == Rectangle.Empty || best.score < 10)
            return Rectangle.Empty;

        var rect = Rectangle.FromLTRB(
            Math.Max(winRect.Left + 4, best.rect.Left - 14),
            Math.Max(winRect.Top + 4, best.rect.Top - 12),
            Math.Min(winRect.Right - 4, Math.Max(best.rect.Right + 120, best.rect.Left + 320)),
            Math.Min(winRect.Bottom - 4, best.rect.Bottom + 18));

        return rect.Width >= 220 && rect.Height >= 28 ? rect : Rectangle.Empty;
    }

    private static bool ElementSupportsTextOrValue(AutomationElement el)
    {
        try
        {
            if (el.Patterns.Text.IsSupported) return true;
        }
        catch { }

        try
        {
            if (el.Patterns.Value.IsSupported) return true;
        }
        catch { }

        return false;
    }

    private static List<IntPtr> GetCodexCandidateWindows(int processId)
    {
        var ordered = new List<IntPtr>();
        var seen = new HashSet<IntPtr>();

        try
        {
            var proc = Process.GetProcessById(processId);
            var main = proc.MainWindowHandle;
            if (main != IntPtr.Zero &&
                NativeMethods.IsWindow(main) &&
                NativeMethods.IsWindowVisible(main) &&
                WindowFinder.GetClassName(main).Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase))
            {
                ordered.Add(main);
                seen.Add(main);
            }
        }
        catch { }

        foreach (var hWnd in FindWindowsByProcessId(processId))
        {
            if (!seen.Add(hWnd)) continue;
            if (!NativeMethods.IsWindowVisible(hWnd)) continue;
            var cls = WindowFinder.GetClassName(hWnd);
            if (!cls.Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase)) continue;
            var title = WindowFinder.GetWindowText(hWnd);
            if (title.Contains("DevTools", StringComparison.OrdinalIgnoreCase)) continue;
            ordered.Add(hWnd);
        }

        return ordered;
    }

    /// <summary>
    /// Try to find a VS Code Claude Code turn-form in a single window handle.
    /// Returns PromptInfo if found, null otherwise.
    /// Extracted so Strategy 3 in Finder can pool all windows and sort globally by callerCwd.
    /// </summary>
    internal PromptInfo? TryFindTurnFormInVSCodeWindow(IntPtr hWnd)
    {
        if (!NativeMethods.IsWindowVisible(hWnd)) return null;
        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return null;

            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm == null) return null;

            var inputGroup = turnForm.FindFirstChild(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
            if (inputGroup == null)
            {
                var children = turnForm.FindAllChildren();
                inputGroup = children.FirstOrDefault(c =>
                    c.ControlType == ControlType.Group && c.BoundingRectangle.Width > 100);
            }
            if (inputGroup == null) return null;

            var rect = inputGroup.BoundingRectangle;
            var title = WindowFinder.GetWindowText(hWnd);
            Console.WriteLine($"  [PROMPT] Found VS Code Claude prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height}) \"{title}\"");
            return new PromptInfo(hWnd, title, "Code",
                new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), ClassifyVsCodeHostType(title));
        }
        catch { return null; }
    }

    // Legacy private alias kept for merge safety with older branches.
    private bool TypeAndSubmitCodexDesktop(PromptInfo prompt, string text) => SubmitCodexDesktopPrompt(prompt, text);

    // Legacy private alias kept for merge safety with older branches.
    private bool TryPostMessageText(PromptInfo prompt, string text, bool submit) =>
        TryPostMessageTextToChromiumRenderer(prompt, text, submit);

    // Legacy private alias kept for merge safety with older branches.
    private PromptInfo? FindCodexPrompt(int processId) => FindCodexDesktopPromptByMarker(processId);

    private static List<(int Pid, string Name)> GetAncestorProcesses()
    {
        var result = new List<(int, string)>();
        try
        {
            var current = Process.GetCurrentProcess();
            var pid = current.Id;

            // Walk up the tree
            for (int i = 0; i < 10; i++) // max 10 levels
            {
                var parentPid = GetParentProcessId(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    result.Add((parent.Id, parent.ProcessName));
                    pid = parent.Id;
                }
                catch
                {
                    break;
                }
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Get parent process ID using NtQueryInformationProcess or WMI fallback.
    /// </summary>
    private static int GetParentProcessId(int pid)
    {
        try
        {
            // Use WMI-like approach via Process
            // On Windows, Process has no direct ParentId. Use handle + NtQueryInformationProcess.
            // Simpler: use PerformanceCounter or WMI
            // Simplest portable approach: read /proc on WSL or use dotnet API
            var proc = Process.GetProcessById(pid);

            // .NET 8+ approach - try reflection for ParentId (not available in all frameworks)
            // Fallback: use command-line tool
            var psi = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = $"process where processid={pid} get parentprocessid /value",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var wmic = AppBotPipe.Start(psi);
            if (wmic == null) return -1;
            var output = wmic.StandardOutput.ReadToEnd();
            wmic.WaitForExit(3000);

            // Parse "ParentProcessId=12345\r\n"
            var match = System.Text.RegularExpressions.Regex.Match(output, @"ParentProcessId=(\d+)");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);
        }
        catch { }
        return -1;
    }

    /// <summary>
    /// Find all visible top-level windows belonging to a process.
    /// </summary>
    private static List<IntPtr> FindWindowsByProcessId(int processId)
    {
        var result = new List<IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != (uint)processId) return true;

            // Root-only filter: skip child windows and owned popups.
            // VS Code (Electron) creates many Chrome_WidgetWin_1 per process --
            // tab renderers, sidebars, etc. GetAncestor(GA_ROOT) ensures we pick
            // the true top-level frame, not a renderer child.
            var root = NativeMethods.GetAncestor(hWnd, 2); // GA_ROOT = 2
            if (root != hWnd) return true;

            // Reject zero-size or tiny floating windows (e.g. tooltip overlays)
            NativeMethods.GetWindowRect(hWnd, out var wr);
            if ((wr.Right - wr.Left) < 200 || (wr.Bottom - wr.Top) < 100) return true;

            result.Add(hWnd);
            return true;
        }, IntPtr.Zero);

        // Sort by area descending -- main window is almost always the largest
        result.Sort((a, b) =>
        {
            NativeMethods.GetWindowRect(a, out var ra);
            NativeMethods.GetWindowRect(b, out var rb);
            int areaA = (ra.Right - ra.Left) * (ra.Bottom - ra.Top);
            int areaB = (rb.Right - rb.Left) * (rb.Bottom - rb.Top);
            return areaB.CompareTo(areaA); // descending
        });

        return result;
    }
}
