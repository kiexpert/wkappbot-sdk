using System.Diagnostics;
using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: prompt-probe
// Probe prompt candidates with chat-name list and print UIA/EXBuild-style hints.
internal partial class Program
{
    sealed record AuthorHint(
        string Raw,
        string HostHint,   // codex | claude | any
        string? WorkspaceTag
    );

    sealed record PromptProbeCandidate(
        IntPtr Hwnd,
        string Title,
        string ClassName,
        int Pid,
        string ProcessName,
        bool Visible,
        bool Iconic,
        Rectangle Rect,
        int Score,
        string Reason,
        bool HostMatch,
        bool CwdMatch
    );

    sealed record InputProbeCertainty(bool IsCertain, string Reason);

    static int PromptProbeCommand(string[] args)
    {
        if (args.Any(a => a is "--help" or "-h"))
        {
            Console.WriteLine("Usage: wkappbot prompt-probe [names...] [--names a,b,c] [--depth N] [--limit N] [--all]");
            Console.WriteLine("  names: thread starter + recent reply authors (recommended input set)");
            Console.WriteLine("  --names: comma/semicolon separated names");
            Console.WriteLine("  --depth: UIA tree depth per candidate (default: 3)");
            Console.WriteLine("  --limit: candidate window count (default: 12)");
            Console.WriteLine("  --all: include hidden windows");
            Console.WriteLine("  input round-trip probe is always ON (1-char add/remove, non-submit)");
            return 0;
        }

        var includeHidden = args.Contains("--all");
        var depth = ParseIntFlag(args, "--depth", 3, 1, 8);
        var limit = ParseIntFlag(args, "--limit", 12, 1, 100);
        var names = ParseNameArgs(args);
        var hints = BuildAuthorHints(names);
        var hostTarget = ResolveHostTarget(hints);
        var cwdFolder = Path.GetFileName(Environment.CurrentDirectory) ?? "";
        var workspaceTags = hints
            .Select(h => h.WorkspaceTag)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Cast<string>()
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        Console.WriteLine("[PROMPT-PROBE] === Input Names ===");
        if (names.Count == 0)
        {
            Console.WriteLine("  (none)");
            Console.WriteLine("  tip: pass thread starter + last 7 reply authors via --names");
        }
        else
        {
            for (int i = 0; i < names.Count; i++)
                Console.WriteLine($"  [{i + 1}] {names[i]}");
        }

        Console.WriteLine();
        Console.WriteLine("[PROMPT-PROBE] === Author Pattern Analysis ===");
        Console.WriteLine($"  target-host={hostTarget}");
        Console.WriteLine($"  cwd-folder={cwdFolder}");
        if (workspaceTags.Count == 0) Console.WriteLine("  workspace-tags=(none)");
        else Console.WriteLine($"  workspace-tags={string.Join(", ", workspaceTags)}");
        foreach (var h in hints)
            Console.WriteLine($"  author=\"{h.Raw}\" -> host={h.HostHint} workspace={h.WorkspaceTag ?? "(none)"}");

        using var helper = new ClaudePromptHelper();
        var allPrompts = helper.FindAllPrompts();

        Console.WriteLine();
        Console.WriteLine("[PROMPT-PROBE] === FindAllPrompts() ===");
        if (allPrompts.Count == 0)
        {
            Console.WriteLine("  prompt=none");
        }
        else
        {
            foreach (var p in allPrompts)
            {
                var st = helper.ProbeSubmitState(p);
                Console.WriteLine($"  prompt host={p.HostType} hwnd=0x{p.WindowHandle:X} pid={TryGetPid(p.WindowHandle)}");
                Console.WriteLine($"    title=\"{TrimForLog(p.WindowTitle, 120)}\"");
                Console.WriteLine($"    rect=({p.PromptRect.X},{p.PromptRect.Y},{p.PromptRect.Width},{p.PromptRect.Height})");
                Console.WriteLine($"    submit turnForm={st.TurnFormFound} found={st.SubmitFound} enabled={st.SubmitEnabled} name=\"{TrimForLog(st.SubmitName, 80)}\"");
            }
        }

        var promptByHwnd = allPrompts
            .GroupBy(p => p.WindowHandle)
            .ToDictionary(g => g.Key, g => g.First());

        var candidates = CollectPromptProbeCandidates(
            names,
            includeHidden,
            promptByHwnd,
            limit,
            hostTarget,
            cwdFolder,
            workspaceTags);

        Console.WriteLine();
        Console.WriteLine($"[PROMPT-PROBE] === Candidate Windows ({candidates.Count}) ===");
        using var automation = new UIA3Automation();
        int idx = 0;
        foreach (var c in candidates)
        {
            idx++;
            Console.WriteLine($"[{idx}] hwnd=0x{c.Hwnd:X} score={c.Score} proc={c.ProcessName} pid={c.Pid}");
            Console.WriteLine($"    title=\"{TrimForLog(c.Title, 120)}\"");
            Console.WriteLine($"    class={c.ClassName} visible={c.Visible} iconic={c.Iconic}");
            Console.WriteLine($"    rect=({c.Rect.X},{c.Rect.Y},{c.Rect.Width},{c.Rect.Height})");
            Console.WriteLine($"    reason={c.Reason}");
            Console.WriteLine($"    match host={c.HostMatch} cwd={c.CwdMatch}");

            if (promptByHwnd.TryGetValue(c.Hwnd, out var known))
            {
                Console.WriteLine($"    mapped-prompt host={known.HostType}");
            }

            try
            {
                var root = automation.FromHandle(c.Hwnd);
                if (root == null)
                {
                    Console.WriteLine("    uia=unavailable");
                    continue;
                }

                Console.WriteLine("    stage.window-state begin");
                var okWindow = DumpWindowRuntimeState(c.Hwnd);
                Console.WriteLine($"    stage.window-state ok={okWindow}");

                var knownPrompt = promptByHwnd.TryGetValue(c.Hwnd, out var kp) ? kp : null;
                Console.WriteLine("    stage.prompt-snapshot begin");
                var okSnapshot = DumpPromptRuntimeSnapshot(helper, root, knownPrompt);
                Console.WriteLine($"    stage.prompt-snapshot ok={okSnapshot}");
                if (knownPrompt != null)
                {
                    var certainty = EvaluateInputProbeCertainty(helper, root, knownPrompt);
                    Console.WriteLine($"    stage.input-roundtrip certainty={certainty.IsCertain} reason=\"{TrimForLog(certainty.Reason, 140)}\"");
                    if (certainty.IsCertain)
                    {
                        Console.WriteLine("    stage.input-roundtrip begin");
                        var okRoundTrip = RunPromptInputRoundTripProbe(
                            helper,
                            root,
                            knownPrompt,
                            "☆",
                            focuslessOnly: false,
                            mode: "auto-1char");
                        Console.WriteLine($"    stage.input-roundtrip ok={okRoundTrip}");
                    }
                    else
                    {
                        Console.WriteLine("    stage.input-roundtrip skipped=true");
                    }
                }

                Console.WriteLine("    stage.uia-summary begin");
                var okUia = DumpProbeUiaSummary(root, names, depth);
                Console.WriteLine($"    stage.uia-summary ok={okUia}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    uia-error={TrimForLog(ex.Message, 120)}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("[PROMPT-PROBE] Done.");
        return 0;
    }

    static bool DumpWindowRuntimeState(IntPtr hWnd)
    {
        try
        {
            var fg = NativeMethods.GetForegroundWindow();
            var isFg = fg == hWnd;
            var enabled = NativeMethods.IsWindowEnabled(hWnd);
            var visible = NativeMethods.IsWindowVisible(hWnd);
            var iconic = NativeMethods.IsIconic(hWnd);

            var wp = new NativeMethods.WINDOWPLACEMENT { length = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.WINDOWPLACEMENT>() };
            var hasWp = NativeMethods.GetWindowPlacement(hWnd, ref wp);

            var props = NativeMethods.EnumWindowProps(hWnd);
            var wkProps = props
                .Where(p => p.name.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) ||
                            p.name.Contains("Slack", StringComparison.OrdinalIgnoreCase) ||
                            p.name.Contains("Codex", StringComparison.OrdinalIgnoreCase))
                .Take(6)
                .ToList();

            Console.WriteLine($"    win.state foreground={isFg} enabled={enabled} visible={visible} iconic={iconic} showCmd={(hasWp ? wp.showCmd : -1)}");
            Console.WriteLine($"    win.props total={props.Count} wk-related={wkProps.Count}");
            foreach (var p in wkProps)
                Console.WriteLine($"      - prop {p.name}={p.value}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"    win.state error={TrimForLog(ex.Message, 120)}");
            return false;
        }
    }

    static bool DumpPromptRuntimeSnapshot(ClaudePromptHelper helper, AutomationElement root, ClaudePromptHelper.PromptInfo? knownPrompt)
    {
        try
        {
            var target = knownPrompt;
            if (target == null)
            {
                var rect = root.BoundingRectangle;
                target = new ClaudePromptHelper.PromptInfo(
                    root.FrameworkAutomationElement.NativeWindowHandle,
                    root.Name ?? "",
                    "",
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    "unknown");
            }

            var submit = helper.ProbeSubmitState(target);
            Console.WriteLine($"    submit.state turnForm={submit.TurnFormFound} found={submit.SubmitFound} enabled={submit.SubmitEnabled} name=\"{TrimForLog(submit.SubmitName, 80)}\"");

            var promptText = ExtractCurrentPromptDraft(root, target.PromptRect);
            Console.WriteLine($"    prompt.current=\"{TrimForLog(promptText, 200)}\"");

            var statusLines = ExtractRecentAiStatusHints(root, target.PromptRect);
            if (statusLines.Count == 0)
            {
                Console.WriteLine("    ai.status=(no obvious status text)");
            }
            else
            {
                Console.WriteLine($"    ai.status candidates={statusLines.Count}");
                foreach (var s in statusLines)
                    Console.WriteLine($"      - {TrimForLog(s, 160)}");
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"    prompt.snapshot error={TrimForLog(ex.Message, 120)}");
            return false;
        }
    }

    static string ExtractCurrentPromptDraft(AutomationElement root, Rectangle promptRect)
    {
        var candidates = new List<(double dist, string txt)>();
        var cap = 2200;
        var scanned = 0;

        var queue = new Queue<AutomationElement>();
        queue.Enqueue(root);

        var centerX = promptRect.Width > 0 ? promptRect.Left + promptRect.Width / 2.0 : root.BoundingRectangle.Left + root.BoundingRectangle.Width / 2.0;
        var centerY = promptRect.Height > 0 ? promptRect.Top + promptRect.Height / 2.0 : root.BoundingRectangle.Top + root.BoundingRectangle.Height / 2.0;

        while (queue.Count > 0 && scanned < cap)
        {
            var el = queue.Dequeue();
            scanned++;
            AutomationElement[] children;
            try { children = el.FindAllChildren(); }
            catch { continue; }
            foreach (var child in children)
            {
                queue.Enqueue(child);
                string text = TryReadElementText(child);
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (text.Length > 600) continue;
                var r = child.BoundingRectangle;
                if (r.Width <= 0 || r.Height <= 0) continue;

                var nodeName = (child.Name ?? "").ToLowerInvariant();
                var aid = (child.AutomationId ?? "").ToLowerInvariant();
                var ctrl = child.ControlType;
                var promptLike = aid.Contains("turn-form") || nodeName.Contains("input") || nodeName.Contains("prompt") ||
                                 nodeName.Contains("입력") || ctrl == ControlType.Edit || ctrl == ControlType.Document;
                if (!promptLike) continue;

                var dx = (r.Left + r.Width / 2.0) - centerX;
                var dy = (r.Top + r.Height / 2.0) - centerY;
                var dist = Math.Sqrt(dx * dx + dy * dy);
                candidates.Add((dist, text.Trim()));
            }
        }

        if (candidates.Count == 0) return "";
        return candidates
            .OrderBy(x => x.dist)
            .ThenByDescending(x => x.txt.Length)
            .Select(x => x.txt)
            .FirstOrDefault() ?? "";
    }

    static List<string> ExtractRecentAiStatusHints(AutomationElement root, Rectangle promptRect)
    {
        var outLines = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var cap = 2400;
        var scanned = 0;
        var queue = new Queue<AutomationElement>();
        queue.Enqueue(root);

        var keywords = new[]
        {
            "thinking","analyzing","stream","stop","retry","failed","error","limit","usage","tokens",
            "생각","분석","중지","재시도","실패","오류","제한","사용량","전달했습니다","online","offline"
        };

        while (queue.Count > 0 && scanned < cap && outLines.Count < 8)
        {
            var el = queue.Dequeue();
            scanned++;
            AutomationElement[] children;
            try { children = el.FindAllChildren(); }
            catch { continue; }
            foreach (var c in children)
            {
                queue.Enqueue(c);

                var txt = TryReadElementText(c);
                if (string.IsNullOrWhiteSpace(txt)) continue;
                txt = txt.Trim();
                if (txt.Length < 2 || txt.Length > 200) continue;

                var lowered = txt.ToLowerInvariant();
                var hit = keywords.Any(k => lowered.Contains(k, StringComparison.OrdinalIgnoreCase));
                if (!hit) continue;

                var r = c.BoundingRectangle;
                if (promptRect.Width > 0 && r.Top >= promptRect.Top - 5)
                {
                    // Prefer status lines above prompt input area (assistant side).
                    continue;
                }

                if (seen.Add(txt))
                    outLines.Add(txt);
                if (outLines.Count >= 8) break;
            }
        }

        return outLines;
    }

    static string TryReadElementText(AutomationElement el)
    {
        try
        {
            var vp = el.Patterns.Value.PatternOrDefault;
            if (vp != null)
            {
                var v = vp.Value;
                if (!string.IsNullOrWhiteSpace(v)) return v;
            }
        }
        catch { }

        try
        {
            var tp = el.Patterns.Text.PatternOrDefault;
            if (tp != null)
            {
                var t = tp.DocumentRange.GetText(240);
                if (!string.IsNullOrWhiteSpace(t)) return t;
            }
        }
        catch { }

        try
        {
            if (!string.IsNullOrWhiteSpace(el.Name)) return el.Name;
        }
        catch { }

        return "";
    }

    static bool RunPromptInputRoundTripProbe(
        ClaudePromptHelper helper,
        AutomationElement root,
        ClaudePromptHelper.PromptInfo prompt,
        string probeText,
        bool focuslessOnly,
        string mode)
    {
        try
        {
            var before = ExtractCurrentPromptDraft(root, prompt.PromptRect);
            var beforeNorm = (before ?? "").Trim();
            var writeText = mode == "auto-1char"
                ? beforeNorm + probeText
                : probeText;

            var writeOk = focuslessOnly
                ? helper.TryTypeWithoutSubmitFocusless(prompt, writeText)
                : TypeWithReadinessBridge(helper, prompt, writeText, "write");
            Thread.Sleep(180);

            string afterInsert;
            try
            {
                var r2 = root.Automation.FromHandle(prompt.WindowHandle);
                afterInsert = r2 != null ? ExtractCurrentPromptDraft(r2, prompt.PromptRect) : "";
            }
            catch
            {
                afterInsert = "";
            }

            var insertObserved =
                !string.IsNullOrWhiteSpace(afterInsert) &&
                afterInsert.Contains(writeText, StringComparison.Ordinal);

            var restoreOk = focuslessOnly
                ? helper.TryTypeWithoutSubmitFocusless(prompt, beforeNorm)
                : TypeWithReadinessBridge(helper, prompt, beforeNorm, "restore");
            Thread.Sleep(180);

            string afterRestore;
            try
            {
                var r3 = root.Automation.FromHandle(prompt.WindowHandle);
                afterRestore = r3 != null ? ExtractCurrentPromptDraft(r3, prompt.PromptRect) : "";
            }
            catch
            {
                afterRestore = "";
            }

            var restored =
                string.Equals((afterRestore ?? "").Trim(), beforeNorm, StringComparison.Ordinal);

            Console.WriteLine(
                $"    input.verify mode={mode} focuslessOnly={focuslessOnly} writeOk={writeOk} insertObserved={insertObserved} restoreOk={restoreOk} restored={restored}");
            Console.WriteLine($"    input.probe-text=\"{TrimForLog(writeText, 120)}\"");
            Console.WriteLine($"    input.before=\"{TrimForLog(beforeNorm, 120)}\"");
            Console.WriteLine($"    input.after-insert=\"{TrimForLog(afterInsert, 120)}\"");
            Console.WriteLine($"    input.after-restore=\"{TrimForLog(afterRestore, 120)}\"");
            return writeOk && insertObserved && restoreOk && restored;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"    input.verify error={TrimForLog(ex.Message, 120)}");
            return false;
        }
    }

    static bool TypeWithReadinessBridge(
        ClaudePromptHelper helper,
        ClaudePromptHelper.PromptInfo prompt,
        string text,
        string phase)
    {
        // Main: focusless text set first.
        var mainOk = helper.TryTypeWithoutSubmitFocusless(prompt, text);
        Console.WriteLine($"    input.{phase}.main-focusless ok={mainOk}");
        if (mainOk) return true;

        // Bridge: standard a11y/input-readiness protection stage.
        var readiness = CreateInputReadiness();
        var report = readiness.Probe(new InputReadinessRequest
        {
            TargetHwnd = prompt.WindowHandle,
            IntendedAction = "key",
            AutoApproveYield = true,
            SkipKnowhow = true,
        });

        if (report.FormIconic)
        {
            var wp = new NativeMethods.WINDOWPLACEMENT
            {
                length = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.WINDOWPLACEMENT>()
            };
            if (NativeMethods.GetWindowPlacement(prompt.WindowHandle, ref wp))
            {
                wp.showCmd = NativeMethods.SW_SHOWNOACTIVATE;
                NativeMethods.SetWindowPlacement(prompt.WindowHandle, ref wp);
                Thread.Sleep(180);
                report = readiness.Probe(new InputReadinessRequest
                {
                    TargetHwnd = prompt.WindowHandle,
                    IntendedAction = "key",
                    AutoApproveYield = true,
                    SkipKnowhow = true,
                });
            }
        }

        var bridgeOk =
            report.ActiveBlocker == null &&
            !report.ElevationMismatch &&
            report.FormVisible &&
            report.FormEnabled &&
            !report.FormIconic &&
            (!report.UserYieldRequested || report.UserYieldConfirmed);

        Console.WriteLine($"    input.{phase}.bridge ok={bridgeOk} visible={report.FormVisible} enabled={report.FormEnabled} iconic={report.FormIconic} elevationMismatch={report.ElevationMismatch} blocker={(report.ActiveBlocker != null)}");

        if (!bridgeOk)
        {
            Console.WriteLine($"    input.{phase}.fallback skipped=true (bridge not ready)");
            return false;
        }

        // Fallback: legacy helper fallback (focus-steal path may run, with readiness already applied).
        var fallbackOk = helper.TypeWithoutSubmit(prompt, text);
        Console.WriteLine($"    input.{phase}.fallback ok={fallbackOk}");
        return fallbackOk;
    }

    static InputProbeCertainty EvaluateInputProbeCertainty(
        ClaudePromptHelper helper,
        AutomationElement root,
        ClaudePromptHelper.PromptInfo prompt)
    {
        try
        {
            var st = helper.ProbeSubmitState(prompt);
            var host = (prompt.HostType ?? "").ToLowerInvariant();
            var draft = ExtractCurrentPromptDraft(root, prompt.PromptRect);
            var rectOk = prompt.PromptRect.Width >= 120 && prompt.PromptRect.Height >= 20;

            if (host == "claude-desktop")
            {
                // Respect legacy Claude path: turn-form + submit detection is authoritative.
                var ok = st.TurnFormFound && st.SubmitFound;
                return new InputProbeCertainty(ok, ok
                    ? "claude turn-form+submit detected"
                    : $"claude prompt uncertain (turnForm={st.TurnFormFound}, submit={st.SubmitFound})");
            }

            if (host == "codex-desktop")
            {
                // Codex currently relies on marker-style prompt detection.
                var markerHit =
                    draft.Contains("Terminal input", StringComparison.OrdinalIgnoreCase) ||
                    draft.Contains("prompt", StringComparison.OrdinalIgnoreCase) ||
                    draft.Contains("input", StringComparison.OrdinalIgnoreCase);
                var ok = rectOk && markerHit;
                return new InputProbeCertainty(ok, ok
                    ? "codex marker prompt detected"
                    : $"codex prompt uncertain (rectOk={rectOk}, markerHit={markerHit})");
            }

            if (host == "vscode-claudecode")
            {
                // Avoid accidental typing in normal editor panes.
                return new InputProbeCertainty(false, "vscode-claudecode skipped for safety (possible normal editor)");
            }

            return new InputProbeCertainty(false, $"unsupported hostType={prompt.HostType}");
        }
        catch (Exception ex)
        {
            return new InputProbeCertainty(false, $"certainty check error: {ex.Message}");
        }
    }

    static bool DumpProbeUiaSummary(AutomationElement root, List<string> names, int depth)
    {
        try
        {
            var cond = new PropertyCondition(root.Automation.PropertyLibrary.Element.AutomationId, "turn-form");
            var turnForm = root.FindFirstDescendant(cond);
            Console.WriteLine($"    uia.turn-form={(turnForm != null)}");

            var hits = new List<string>();
            var queue = new Queue<(AutomationElement el, int d)>();
            queue.Enqueue((root, 0));
            int scanned = 0;
            int cap = 1800;

            while (queue.Count > 0 && scanned < cap)
            {
                var (el, d) = queue.Dequeue();
                scanned++;
                if (d >= depth) continue;

                AutomationElement[] children;
                try { children = el.FindAllChildren(); }
                catch { continue; }

                foreach (var child in children)
                {
                    queue.Enqueue((child, d + 1));
                    try
                    {
                        var name = child.Name ?? "";
                        var aid = child.AutomationId ?? "";
                        var ctrl = child.ControlType.ToString();
                        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(aid))
                            continue;

                        if (IsPromptishNode(name, aid, names))
                        {
                            var r = child.BoundingRectangle;
                            hits.Add($"[{ctrl}] name=\"{TrimForLog(name, 70)}\" aid=\"{TrimForLog(aid, 40)}\" rect=({r.X},{r.Y},{r.Width},{r.Height})");
                            if (hits.Count >= 10) break;
                        }
                    }
                    catch { }
                }

                if (hits.Count >= 10) break;
            }

            Console.WriteLine($"    uia.scanned={scanned} depth={depth}");
            if (hits.Count == 0)
            {
                Console.WriteLine("    uia.hits=none");
                return scanned > 0;
            }
            Console.WriteLine($"    uia.hits={hits.Count}");
            foreach (var h in hits)
                Console.WriteLine($"      - {h}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"    uia.summary error={TrimForLog(ex.Message, 120)}");
            return false;
        }
    }

    static bool IsPromptishNode(string name, string aid, List<string> names)
    {
        static bool Has(string src, string token) =>
            src.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;

        if (Has(aid, "turn-form")) return true;
        if (Has(name, "prompt")) return true;
        if (Has(name, "message")) return true;
        if (Has(name, "input")) return true;
        if (Has(name, "입력")) return true;
        if (Has(name, "질문")) return true;
        if (Has(name, "send")) return true;
        if (Has(name, "전송")) return true;

        foreach (var n in names)
        {
            if (string.IsNullOrWhiteSpace(n) || n.Length < 2) continue;
            if (Has(name, n)) return true;
        }
        return false;
    }

    static List<PromptProbeCandidate> CollectPromptProbeCandidates(
        List<string> names,
        bool includeHidden,
        IReadOnlyDictionary<IntPtr, ClaudePromptHelper.PromptInfo> promptByHwnd,
        int limit,
        string hostTarget,
        string cwdFolder,
        List<string> workspaceTags)
    {
        var list = new List<PromptProbeCandidate>();
        var namesNorm = names
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            var visible = NativeMethods.IsWindowVisible(hWnd);
            if (!includeHidden && !visible) return true;

            var title = WindowFinder.GetWindowText(hWnd) ?? "";
            var className = WindowFinder.GetClassName(hWnd) ?? "";
            var iconic = NativeMethods.IsIconic(hWnd);
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pidRaw);
            var pid = (int)pidRaw;
            var proc = TryGetProcessName(pid);
            NativeMethods.GetWindowRect(hWnd, out var wr);
            var rect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);

            var score = 0;
            var reasons = new List<string>();
            var hostMatch = hostTarget == "any";
            var cwdMatch = false;

            if (promptByHwnd.ContainsKey(hWnd))
            {
                score += 100;
                reasons.Add("mapped-by-findallprompts");
            }
            if (proc.Equals("codex", StringComparison.OrdinalIgnoreCase) ||
                proc.Equals("claude", StringComparison.OrdinalIgnoreCase) ||
                proc.Equals("code", StringComparison.OrdinalIgnoreCase))
            {
                score += 20;
                reasons.Add($"proc={proc}");
            }
            if (hostTarget == "codex" && proc.Equals("codex", StringComparison.OrdinalIgnoreCase))
            {
                score += 30;
                hostMatch = true;
                reasons.Add("host-match:codex");
            }
            else if (hostTarget == "claude" &&
                     (proc.Equals("claude", StringComparison.OrdinalIgnoreCase) ||
                      proc.Equals("code", StringComparison.OrdinalIgnoreCase)))
            {
                score += 30;
                hostMatch = true;
                reasons.Add("host-match:claude");
            }
            if (className.Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase))
            {
                score += 10;
                reasons.Add("class=Chrome_WidgetWin_1");
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                if (title.Contains("Codex", StringComparison.OrdinalIgnoreCase) ||
                    title.Contains("Claude", StringComparison.OrdinalIgnoreCase) ||
                    title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                {
                    score += 8;
                    reasons.Add("title=ai-host");
                }
                if (!string.IsNullOrWhiteSpace(cwdFolder) &&
                    title.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase))
                {
                    score += 25;
                    cwdMatch = true;
                    reasons.Add($"cwd-hit:{cwdFolder}");
                }
                foreach (var tag in workspaceTags)
                {
                    if (string.IsNullOrWhiteSpace(tag)) continue;
                    if (!title.Contains(tag, StringComparison.OrdinalIgnoreCase)) continue;
                    score += 18;
                    cwdMatch = true;
                    reasons.Add($"tag-hit:{tag}");
                }
            }

            foreach (var n in namesNorm)
            {
                if (n.Length < 2) continue;
                if (title.Contains(n, StringComparison.OrdinalIgnoreCase))
                {
                    score += 7;
                    reasons.Add($"name-hit:{n}");
                }
            }

            if (score <= 0) return true;

            list.Add(new PromptProbeCandidate(
                hWnd,
                title,
                className,
                pid,
                proc,
                visible,
                iconic,
                rect,
                score,
                string.Join(", ", reasons.Distinct(StringComparer.OrdinalIgnoreCase)),
                hostMatch,
                cwdMatch
            ));
            return true;
        }, IntPtr.Zero);

        return list
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Visible)
            .ThenBy(x => x.Iconic)
            .Take(limit)
            .ToList();
    }

    static List<string> ParseNameArgs(string[] args)
    {
        var list = new List<string>();

        var rawNames = GetArgValue(args, "--names");
        if (!string.IsNullOrWhiteSpace(rawNames))
        {
            list.AddRange(rawNames.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a == "--names" && i + 1 < args.Length) { i++; continue; }
            if (a == "--depth" && i + 1 < args.Length) { i++; continue; }
            if (a == "--limit" && i + 1 < args.Length) { i++; continue; }
            if (a == "--all" || a.StartsWith("--", StringComparison.Ordinal)) continue;
            list.Add(a);
        }

        return list
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    static List<AuthorHint> BuildAuthorHints(List<string> names)
    {
        var list = new List<AuthorHint>();
        foreach (var raw in names)
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;
            var s = raw.Trim();
            var lower = s.ToLowerInvariant();

            string hostHint = "any";
            if (lower.Contains("codex") || lower.Contains("코뎃") || lower.Contains("코덱"))
                hostHint = "codex";
            else if (lower.Contains("claude") || lower.Contains("클롣") || lower.Contains("클봇"))
                hostHint = "claude";

            string? workspaceTag = null;
            var lb = s.IndexOf('[');
            var rb = s.IndexOf(']');
            if (lb >= 0 && rb > lb)
            {
                workspaceTag = s[(lb + 1)..rb].Trim();
            }

            list.Add(new AuthorHint(s, hostHint, workspaceTag));
        }
        return list;
    }

    static string ResolveHostTarget(List<AuthorHint> hints)
    {
        var hasCodex = hints.Any(h => h.HostHint == "codex");
        var hasClaude = hints.Any(h => h.HostHint == "claude");
        if (hasCodex && !hasClaude) return "codex";
        if (!hasCodex && hasClaude) return "claude";
        return "any";
    }

    static int ParseIntFlag(string[] args, string flag, int fallback, int min, int max)
    {
        var s = GetArgValue(args, flag);
        if (!int.TryParse(s, out var v)) return fallback;
        if (v < min) return min;
        if (v > max) return max;
        return v;
    }

    static string TryGetProcessName(int pid)
    {
        try
        {
            return Process.GetProcessById(pid).ProcessName;
        }
        catch
        {
            return "?";
        }
    }

    static int TryGetPid(IntPtr hWnd)
    {
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pidRaw);
        return (int)pidRaw;
    }

    static string TrimForLog(string? s, int max)
    {
        if (string.IsNullOrEmpty(s)) return "";
        var v = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (v.Length <= max) return v;
        return v[..max] + "...";
    }
}
