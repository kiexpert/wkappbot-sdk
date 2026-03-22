using System.Diagnostics;
using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Find the Claude Code prompt input by walking the process tree.
    /// Strategy: current process → parent → grandparent ... → find Electron/VSCode main window → UIA search.
    /// </summary>
    public PromptInfo? FindPrompt(string? callerCwd = null)
    {
        // Strategy 1: Walk up process tree from current wkappbot.exe
        var ancestors = GetAncestorProcesses();
        foreach (var (pid, name) in ancestors)
        {
            Console.WriteLine($"  [PROMPT] Checking ancestor: {name} (PID={pid})");

            if (name.Equals("claude", StringComparison.OrdinalIgnoreCase))
            {
                // Claude Desktop (Electron app)
                var result = FindClaudeDesktopPrompt(pid);
                if (result != null) return result;
            }
            else if (name.Equals("code", StringComparison.OrdinalIgnoreCase) ||
                     name.Equals("code - insiders", StringComparison.OrdinalIgnoreCase))
            {
                // VS Code with Claude Code extension
                var result = FindVSCodePrompt(pid, callerCwd);
                if (result != null) return result;
            }
            else if (name.Equals("codex", StringComparison.OrdinalIgnoreCase))
            {
                // Codex desktop app (marker based prompt detection).
                var result = FindCodexDesktopPromptByMarker(pid);
                if (result != null) return result;
            }
        }

        // Strategy 2: Enumerate all visible windows from claude.exe processes
        Console.WriteLine("  [PROMPT] Ancestor walk failed, scanning all claude.exe windows...");
        var claudeProcs = Process.GetProcessesByName("claude");
        Console.WriteLine($"  [PROMPT] Process.GetProcessesByName(\"claude\"): {claudeProcs.Length} process(es)");
        foreach (var proc in claudeProcs)
        {
            Console.WriteLine($"  [PROMPT]   claude PID={proc.Id}");
            var result = FindClaudeDesktopPrompt(proc.Id);
            if (result != null) return result;
        }

        // Prefer Codex before VS Code when both are open.
        Console.WriteLine("  [PROMPT] Scanning Codex windows...");
        var codexProcs = Process.GetProcessesByName("codex");
        Console.WriteLine($"  [PROMPT] Process.GetProcessesByName(\"codex\"): {codexProcs.Length} process(es)");
        foreach (var proc in codexProcs)
        {
            var result = FindCodexDesktopPromptByMarker(proc.Id);
            if (result != null) return result;
        }

        // Strategy 3: Enumerate all VS Code windows — pool ALL windows first, then sort by callerCwd.
        // Important: multiple Code.exe processes exist (one per window). Iterating process-by-process
        // means callerCwd preference only applies within each process, not globally.
        // Pooling all windows and sorting globally ensures the right folder window wins.
        Console.WriteLine("  [PROMPT] Scanning VS Code windows...");
        var codeProcs = Process.GetProcessesByName("Code");
        Console.WriteLine($"  [PROMPT] Process.GetProcessesByName(\"Code\"): {codeProcs.Length} process(es)");
        {
            var allCodeWindows = new List<IntPtr>();
            foreach (var proc in codeProcs)
                allCodeWindows.AddRange(FindWindowsByProcessId(proc.Id));

            if (!string.IsNullOrEmpty(callerCwd))
            {
                var folderName = System.IO.Path.GetFileName(callerCwd.TrimEnd('\\', '/'));
                if (!string.IsNullOrEmpty(folderName))
                {
                    // Sort: matching folder title first, then by area descending
                    allCodeWindows = allCodeWindows
                        .OrderByDescending(h => WindowFinder.GetWindowText(h)
                            .Contains(folderName, StringComparison.OrdinalIgnoreCase) ? 1 : 0)
                        .ToList();
                    Console.WriteLine($"  [PROMPT]   callerCwd folder=\"{folderName}\" applied to {allCodeWindows.Count} window(s)");
                }
            }

            foreach (var hWnd in allCodeWindows)
            {
                var result = TryFindTurnFormInVSCodeWindow(hWnd);
                if (result != null) return result;
            }
        }

        // Strategy 4: Brute-force — scan ALL Chrome_WidgetWin_1 windows for turn-form
        Console.WriteLine("  [PROMPT] Brute-force: scanning all Chrome_WidgetWin_1 windows...");
        var allElectronWindows = new List<IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls == "Chrome_WidgetWin_1")
            {
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string procName = "?";
                try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                Console.WriteLine($"  [PROMPT]   Chrome_WidgetWin_1: 0x{hWnd:X} \"{title}\" proc={procName} pid={pid}");
                allElectronWindows.Add(hWnd);
            }
            return true;
        }, IntPtr.Zero);

        // Try each Chrome_WidgetWin_1 window for turn-form
        foreach (var hWnd in allElectronWindows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                if (turnForm == null) continue;

                var inputGroup = turnForm.FindFirstChild(
                    new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
                if (inputGroup == null)
                {
                    var children = turnForm.FindAllChildren();
                    inputGroup = children.FirstOrDefault(c =>
                        c.ControlType == ControlType.Group && c.BoundingRectangle.Width > 100);
                }
                if (inputGroup == null) continue;

                var rect = inputGroup.BoundingRectangle;
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string pName = "?";
                try { pName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                Console.WriteLine($"  [PROMPT] BRUTE-FORCE FOUND: turn-form in \"{title}\" proc={pName} pid={pid}");
                return new PromptInfo(hWnd, title, pName, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    pName.Equals("claude", StringComparison.OrdinalIgnoreCase) ? "claude-desktop" : "unknown");
            }
            catch { }
        }

        Console.WriteLine("  [PROMPT] ALL strategies exhausted — no prompt found");
        return null;
    }

    /// <summary>
    /// Find ALL Claude Code prompt inputs across all windows.
    /// Unlike FindPrompt (which returns first match), this returns every available prompt.
    /// Used for broadcast messages (e.g., ping → all Claude instances respond).
    /// </summary>
    private static DateTime _lastFullScan = DateTime.MinValue;
    private static List<PromptInfo> _cachedPromptResults = new();

    public List<PromptInfo> FindAllPrompts(bool includeHidden = false)
    {
        // Fast path: return cached results if valid (all hwnds alive + <2s old)
        if (_cachedPromptResults.Count > 0
            && (DateTime.UtcNow - _lastFullScan).TotalSeconds < 2.0
            && _cachedPromptResults.All(p => NativeMethods.IsWindow(p.WindowHandle)))
        {
            return _cachedPromptResults;
        }

        var results = new List<PromptInfo>();
        var seen = new HashSet<IntPtr>();
        var scannedCodexPids = new HashSet<uint>();

        // Purge dead windows from cache (window closed/crashed since last scan)
        var deadHwnds = _turnFormCache.Keys.Where(h => !NativeMethods.IsWindow(h)).ToList();
        foreach (var h in deadHwnds) _turnFormCache.Remove(h);

        // Scan all Chrome_WidgetWin_1 windows (covers Electron + VS Code)
        // Cache pid→procName: each Electron/VS Code process has many Chrome_WidgetWin_1 windows,
        // so Process.GetProcessById() would be called N times for the same pid without caching.
        var procNameCache = new Dictionary<uint, string>();
        var allWindows = new List<(IntPtr hWnd, string title, string procName, uint pid)>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!includeHidden && !NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls == "Chrome_WidgetWin_1")
            {
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (!procNameCache.TryGetValue(pid, out var procName))
                {
                    try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { procName = "?"; }
                    procNameCache[pid] = procName!;
                }
                allWindows.Add((hWnd, title, procName!, pid));
            }
            return true;
        }, IntPtr.Zero);

        foreach (var (hWnd, title, procName, pid) in allWindows)
        {
            if (seen.Contains(hWnd)) continue;

            // Claude Desktop: look for turn-form (each window = separate instance)
            if (procName.Equals("claude", StringComparison.OrdinalIgnoreCase))
            {
                var pi = FindTurnFormInWindow(hWnd, title, procName);
                if (pi != null) { results.Add(pi); seen.Add(hWnd); }
                continue;
            }

            // Codex desktop: prompt marker based detection
            if (procName.Equals("codex", StringComparison.OrdinalIgnoreCase))
            {
                if (!scannedCodexPids.Add(pid))
                    continue;

                var pi = FindCodexDesktopPromptByMarker((int)pid);
                if (pi != null)
                {
                    results.Add(pi);
                    _turnFormCache[pi.WindowHandle] = pi; // expose to GetAllCachedPrompts() for status streaming
                    seen.Add(pi.WindowHandle);
                }
                continue;
            }

            // VS Code: title check first to skip background/helper windows (they have no "Visual Studio Code" suffix).
            // Only main editor windows carry the " - Visual Studio Code" suffix — saves UIA traversal on dozens of helper hwnd's.
            if (procName.Equals("Code", StringComparison.OrdinalIgnoreCase))
            {
                if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                    continue; // skip Code.exe utility/helper windows quickly

                // Try turn-form first (Claude.ai loaded in VS Code webview)
                var pi = FindTurnFormInWindow(hWnd, title, procName);
                if (pi != null) { results.Add(pi); seen.Add(hWnd); continue; }

                // Native Claude Code extension (no turn-form)
                // Each VS Code window gets its own entry — SmartSetForegroundWindow(hwnd) targets
                // the specific window, and foreground verification in TypeAndSubmit prevents
                // wrong-window delivery. No per-process dedup needed.
                NativeMethods.GetWindowRect(hWnd, out var wr);
                var rect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
                var vsCodeEntry = new PromptInfo(hWnd, title, "Code", rect, "vscode-claudecode");
                results.Add(vsCodeEntry);
                _turnFormCache[hWnd] = vsCodeEntry; // expose to GetAllCachedPrompts() for status streaming
                seen.Add(hWnd);
            }
        }

        var scanMs = (DateTime.UtcNow - _lastFullScan).TotalMilliseconds;
        _lastFullScan = DateTime.UtcNow;
        _cachedPromptResults = results;
        Console.WriteLine($"  [PROMPT] FindAllPrompts: {results.Count} prompt(s) found ({scanMs:F0}ms)");
        foreach (var r in results)
        {
            var shortTitle = r.WindowTitle.Length > 60 ? r.WindowTitle[..57] + "..." : r.WindowTitle;
            Console.WriteLine($"    [{r.HostType}] 0x{r.WindowHandle:X} \"{shortTitle}\"");
        }
        return results;
    }

    /// <summary>
    /// Find the Claude Code prompt for a SPECIFIC project CWD.
    /// Matches VS Code by window title, Claude Desktop by UIA text content.
    /// Returns null if no matching prompt found (caller should fall back to Slack).
    /// </summary>
    public PromptInfo? FindPromptForCwd(string targetCwd)
    {
        var cwdFolder = Path.GetFileName(targetCwd); // e.g., "WKAppBot"
        if (string.IsNullOrEmpty(cwdFolder)) return null;
        Console.WriteLine($"  [PROMPT-CWD] Searching for prompt matching CWD: {targetCwd} (key: {cwdFolder})");

        var allWindows = new List<(IntPtr hWnd, string title, string procName)>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            var cls = WindowFinder.GetClassName(hWnd);
            if (cls == "Chrome_WidgetWin_1")
            {
                var title = WindowFinder.GetWindowText(hWnd);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                string procName = "?";
                try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }
                allWindows.Add((hWnd, title, procName));
            }
            return true;
        }, IntPtr.Zero);

        // Priority 1: VS Code — match by window title containing folder name
        foreach (var (hWnd, title, procName) in allWindows)
        {
            if (!procName.Equals("Code", StringComparison.OrdinalIgnoreCase)) continue;
            if (!title.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase)) continue;
            Console.WriteLine($"  [PROMPT-CWD] VS Code title match: \"{title}\"");
            // Try turn-form first; fallback to vscode-claudecode (native extension has no turn-form)
            var pi = FindTurnFormInWindow(hWnd, title, procName);
            if (pi != null) return pi;
            // Native Claude Code extension: no turn-form, use window directly
            if (title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
            {
                NativeMethods.GetWindowRect(hWnd, out var wr);
                var rect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
                Console.WriteLine($"  [PROMPT-CWD] VS Code native Claude Code extension found");
                return new PromptInfo(hWnd, title, "Code", rect, "vscode-claudecode");
            }
        }

        // Priority 2: Claude Desktop — search UIA for CWD text at bottom bar
        foreach (var (hWnd, title, procName) in allWindows)
        {
            if (!procName.Equals("claude", StringComparison.OrdinalIgnoreCase)) continue;
            Console.WriteLine($"  [PROMPT-CWD] Checking Claude Desktop window: \"{title}\"");
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;
                // Search for any element (Button/Text) whose Name matches the CWD path
                // Claude Desktop Code tab shows CWD as a Button at the bottom bar
                var allElements = root.FindAllDescendants();
                bool cwdFound = false;
                foreach (var el in allElements)
                {
                    try
                    {
                        var name = el.Name;
                        if (string.IsNullOrEmpty(name) || name.Length > 500) continue;
                        if (name.Equals(targetCwd, StringComparison.OrdinalIgnoreCase)
                            || name.EndsWith("\\" + cwdFolder, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"  [PROMPT-CWD] CWD element found in Claude Desktop: [{el.ControlType}] \"{name}\"");
                            cwdFound = true;
                            break;
                        }
                    }
                    catch { }
                }
                if (!cwdFound) continue;
                var pi = FindTurnFormInWindow(hWnd, title, procName);
                if (pi != null) return pi;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT-CWD] Claude Desktop UIA error: {ex.Message}");
            }
        }

        Console.WriteLine($"  [PROMPT-CWD] No matching prompt found for CWD: {targetCwd}");
        return null;
    }

    /// <summary>
    /// Find turn-form prompt input in a specific window handle.
    /// Shared helper for FindPrompt and FindPromptForCwd.
    /// Results are cached per hwnd — UIA scan only on first encounter.
    /// </summary>
    private PromptInfo? FindTurnFormInWindow(IntPtr hWnd, string title, string procName)
    {
        // Positive cache hit: validate hwnd still alive (Electron can recreate windows during updates/navigation).
        if (_turnFormCache.TryGetValue(hWnd, out var cached))
        {
            if (!NativeMethods.IsWindow(hWnd)) { _turnFormCache.Remove(hWnd); } // stale → fall through to rescan
            else return cached.WindowTitle == title ? cached : cached with { WindowTitle = title };
        }

        // Negative cache hit: skip scan for TTL window (avoids re-scanning non-AI windows repeatedly).
        if (_negativeCache.TryGetValue(hWnd, out var expiry) && DateTime.UtcNow < expiry)
            return null;

        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return null;

            AutomationElement? turnForm = null;

            // Fast-path A: GetFocusedElement — if user is typing in this window's prompt,
            // walk up the focus chain (≤6 levels) to find turn-form. O(depth) not O(tree).
            try
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint targetPid);
                var focused = _automation.FocusedElement();
                if (focused != null && focused.Properties.ProcessId.ValueOrDefault == (int)targetPid)
                {
                    var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
                    var el = focused;
                    for (int i = 0; i < 6 && el != null; i++)
                    {
                        if ((el.Properties.AutomationId.ValueOrDefault ?? "") == "turn-form")
                        { turnForm = el; break; }
                        el = walker.GetParent(el);
                    }
                }
            }
            catch { }

            // Fast-path B: Document → direct child search (1-2 levels, avoids deep conversation tree).
            if (turnForm == null)
            {
                try
                {
                    var doc = root.FindFirstDescendant(
                        new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Document));
                    if (doc != null)
                        turnForm = doc.FindFirstChild(
                            new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                }
                catch { }
            }

            // Fallback: full tree scan (only runs once per hwnd due to positive cache on success).
            if (turnForm == null)
                turnForm = root.FindFirstDescendant(
                    new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));

            if (turnForm == null)
            {
                // Cache negative result — suppress re-scan for 2s.
                _negativeCache[hWnd] = DateTime.UtcNow + NegativeCacheTtl;
                return null;
            }

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
            var hostType = procName.Equals("claude", StringComparison.OrdinalIgnoreCase) ? "claude-desktop" : "vscode";
            var result = new PromptInfo(hWnd, title, procName,
                new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), hostType);
            _turnFormCache[hWnd] = result; // cache for all future ticks
            Console.WriteLine($"  [PROMPT-CWD] turn-form found+cached in \"{title}\" ({hostType}) hwnd=0x{hWnd:X}");
            return result;
        }
        catch { return null; }
    }

    /// <summary>
    /// Comprehensive diagnostic for prompt search failure.
    /// Returns a multi-line string with all relevant details for debugging.
    /// Call this AFTER FindPrompt() returns null to capture the full picture.
    /// </summary>
    public string DiagnosePromptSearch()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("=== Prompt Search Diagnosis ===");

        // 1. Process ancestry
        try
        {
            var ancestors = GetAncestorProcesses();
            sb.AppendLine($"Process ancestry ({ancestors.Count}):");
            foreach (var (pid, name) in ancestors)
                sb.AppendLine($"  {name} (PID={pid})");
        }
        catch (Exception ex) { sb.AppendLine($"Ancestry error: {ex.Message}"); }

        // 2. All claude.exe processes and their windows
        try
        {
            var claudeProcs = Process.GetProcessesByName("claude");
            sb.AppendLine($"claude.exe processes: {claudeProcs.Length}");
            foreach (var proc in claudeProcs)
            {
                var windows = FindWindowsByProcessId(proc.Id);
                sb.AppendLine($"  PID={proc.Id}: {windows.Count} visible window(s)");
                foreach (var hWnd in windows)
                {
                    var title = WindowFinder.GetWindowText(hWnd);
                    var cls = WindowFinder.GetClassName(hWnd);
                    NativeMethods.GetWindowRect(hWnd, out var r);
                    sb.AppendLine($"    0x{hWnd:X} \"{title}\" class={cls} {r.Right - r.Left}x{r.Bottom - r.Top}");

                    // Check UIA for turn-form
                    try
                    {
                        var root = _automation.FromHandle(hWnd);
                        if (root == null) { sb.AppendLine("      UIA: null"); continue; }

                        var tf = root.FindFirstDescendant(
                            new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                        if (tf != null)
                        {
                            sb.AppendLine($"      turn-form: FOUND at {tf.BoundingRectangle}");
                            // Check children
                            var children = tf.FindAllChildren();
                            sb.AppendLine($"      turn-form children ({children.Length}):");
                            foreach (var c in children)
                            {
                                try { sb.AppendLine($"        {c.ControlType} \"{c.Name}\" {c.BoundingRectangle.Width}x{c.BoundingRectangle.Height} enabled={c.IsEnabled}"); }
                                catch { sb.AppendLine($"        (read error)"); }
                            }
                        }
                        else
                        {
                            sb.AppendLine("      turn-form: NOT FOUND");
                            // Deep UIA dump → saved to file for analysis
                            try
                            {
                                var deepSb = new System.Text.StringBuilder();
                                deepSb.AppendLine($"=== Deep UIA Dump: 0x{hWnd:X} \"{title}\" ===");
                                DumpUiaTree(root, deepSb, 0, maxDepth: 10);
                                var dumpPath = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    ".claude", "projects", "prompt_uia_dump_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
                                try { Directory.CreateDirectory(Path.GetDirectoryName(dumpPath)!); } catch { }
                                File.WriteAllText(dumpPath, deepSb.ToString());
                                sb.AppendLine($"      UIA deep dump saved: {dumpPath}");
                                // Also include top 20 lines in the summary
                                var lines = deepSb.ToString().Split('\n');
                                foreach (var line in lines.Take(20))
                                    sb.AppendLine($"      {line.TrimEnd()}");
                                if (lines.Length > 20) sb.AppendLine($"      ...(+{lines.Length - 20} more lines in dump file)");
                            }
                            catch (Exception dumpEx) { sb.AppendLine($"      (UIA dump error: {dumpEx.Message})"); }
                        }
                    }
                    catch (Exception uiaEx) { sb.AppendLine($"      UIA error: {uiaEx.Message}"); }
                }
            }
        }
        catch (Exception ex) { sb.AppendLine($"Claude process scan error: {ex.Message}"); }

        // 3. Foreground window info
        try
        {
            var fg = NativeMethods.GetForegroundWindow();
            if (fg != IntPtr.Zero)
            {
                var fgTitle = WindowFinder.GetWindowText(fg);
                var fgClass = WindowFinder.GetClassName(fg);
                NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
                sb.AppendLine($"Foreground: 0x{fg:X} \"{fgTitle}\" class={fgClass} pid={fgPid}");
            }
            else sb.AppendLine("Foreground: none");
        }
        catch { }

        return sb.ToString();
    }

    /// <summary>Recursive UIA tree dump for diagnostics.</summary>
    private static void DumpUiaTree(AutomationElement el, System.Text.StringBuilder sb, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var indent = new string(' ', depth * 2);
        // Read element properties individually (each can fail independently)
        string name = "?", aid = "?", ct = "?", rect = "?";
        try { name = (el.Name ?? "").Replace("\n", " ").Replace("\r", ""); if (name.Length > 60) name = name.Substring(0, 60) + "..."; } catch { }
        try { aid = el.AutomationId ?? ""; } catch { }
        try { ct = el.ControlType.ToString(); } catch { }
        try { var r = el.BoundingRectangle; rect = $"{r.Width}x{r.Height} at {r.X},{r.Y}"; } catch { }
        sb.AppendLine($"{indent}[{ct}] aid=\"{aid}\" name=\"{name}\" ({rect})");

        // Always try to enumerate children even if parent properties failed
        try
        {
            foreach (var child in el.FindAllChildren())
                DumpUiaTree(child, sb, depth + 1, maxDepth);
        }
        catch { }
    }

}
