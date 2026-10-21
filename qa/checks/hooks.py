"""kih harness hook fixtures: PreToolUse allow/deny, PostToolUse, Stop, per-call latency.

Hooks are discovered from Claude Code settings files (standard "hooks" block), merged:
  $HOOK_SETTINGS, $KIH_ROOT/.claude/settings.json, $KIH_ROOT/.claude/settings.local.json,
  <USERPROFILE|HOME>/.claude/settings.json
Each fixture is fed to every matching hook command on stdin, as Claude Code does.
Decision: rc 2, permissionDecision deny/ask, decision block/deny, or continue:false = deny.
"""
import json
import os
import re
import tempfile
import uuid
from pathlib import Path

from qa_common import FAIL, FIXTURES, PASS, SKIP, Result, first_line, home_dir, load_json, run


def settings_files(ctx):
    files = []
    if os.environ.get("HOOK_SETTINGS"):
        files.append(Path(os.environ["HOOK_SETTINGS"]))
    if ctx.kih_root:
        files += [ctx.kih_root / ".claude" / "settings.json", ctx.kih_root / ".claude" / "settings.local.json"]
    files.append(Path(home_dir()) / ".claude" / "settings.json")
    return [f for f in files if f.is_file()]


def discover(files):
    """{event: [(matcher, command)]} merged and de-duplicated."""
    hooks, seen = {}, set()
    for f in files:
        try:
            data = load_json(f)
        except (OSError, ValueError):
            continue
        for event, groups in (data.get("hooks") or {}).items():
            for g in groups or []:
                for h in g.get("hooks", []):
                    cmd = h.get("command")
                    k = (event, g.get("matcher", ""), cmd)
                    if h.get("type", "command") == "command" and cmd and k not in seen:
                        seen.add(k)
                        hooks.setdefault(event, []).append((g.get("matcher", ""), cmd))
    return hooks


def matches(matcher, tool):
    if not matcher or matcher == "*" or tool is None:
        return True
    try:
        return re.fullmatch(matcher, tool) is not None
    except re.error:
        return matcher == tool


def decision(rc, stdout):
    if rc == 2:
        return "deny"
    for line in reversed([l for l in stdout.strip().splitlines() if l.strip()]):
        try:
            o = json.loads(line)
        except ValueError:
            continue
        if not isinstance(o, dict):
            continue
        hso = o.get("hookSpecificOutput") or {}
        if hso.get("permissionDecision") in ("deny", "ask") or o.get("decision") in ("block", "deny") \
                or o.get("continue") is False:
            return "deny"
        break
    return "allow"


def short(cmd):
    m = re.findall(r"[\w.-]+\.(?:ps1|sh|py|cmd|js)", cmd)
    return m[-1] if m else cmd[:50]


def expected_for(fx, plat):
    e = fx["expect"]
    return e.get(plat, e.get("default")) if isinstance(e, dict) else e


def run_checks(ctx):
    area = "harness-hooks"
    fixtures = load_json(FIXTURES / "hooks-fixtures.json")["fixtures"]
    if ctx.kih_root is None:
        return [Result(area, f["name"], SKIP, "KIH_ROOT not found") for f in fixtures]
    files = settings_files(ctx)
    hooks = discover(files)
    if not hooks:
        return [Result(area, f["name"], SKIP, "no hooks wired (run the kih installer)") for f in fixtures]
    tdir = tempfile.mkdtemp(prefix="qa-hooks-")
    transcript = ctx.transcript or str(Path(tdir) / "empty.jsonl")
    if not ctx.transcript:
        Path(transcript).write_text("", encoding="utf-8")
    sid = "qa-" + uuid.uuid4().hex[:12]
    env = {"CLAUDE_PROJECT_DIR": str(ctx.kih_root), "QA_HARNESS_FIXTURE": "1"}
    out, latency = [], {}
    for fx in fixtures:
        ev, tool = fx["event"], fx.get("tool_name")
        payload = {"session_id": sid, "transcript_path": transcript, "cwd": str(ctx.kih_root),
                   "hook_event_name": ev, "permission_mode": "default"}
        if tool:
            payload["tool_name"] = tool
            payload["tool_input"] = fx.get("tool_input", {})
        if ev == "PostToolUse":
            payload["tool_response"] = fx.get("tool_response", {"stdout": "", "stderr": "", "interrupted": False})
        if ev in ("Stop", "SubagentStop"):
            payload["stop_hook_active"] = False
        sel = [c for m, c in hooks.get(ev, []) if matches(m, tool)]
        if not sel:
            out.append(Result(area, fx["name"], SKIP, f"no {ev} hook matches {tool or ''}".strip()))
            continue
        want = expected_for(fx, ctx.platform)
        decisions, crashes, total = [], [], 0
        for cmd in sel:
            rc, so, se, ms = run(cmd, cwd=str(ctx.kih_root), env=env, shell=True,
                                 stdin=json.dumps(payload).encode("utf-8"), timeout=ctx.timeout)
            total += ms
            latency.setdefault(cmd, []).append(ms)
            d = decision(rc, so)
            decisions.append(d)
            if rc not in (0, 2):
                crashes.append(f"{short(cmd)} rc={rc}: {first_line(se, so)}")
        got = "deny" if "deny" in decisions else "allow"
        detail = f"{len(sel)} hook(s), {total} ms, got {got}"
        if crashes:
            out.append(Result(area, fx["name"], FAIL, detail, total, "hook crashed: " + crashes[0]))
        elif want not in (None, "any") and got != want:
            cause = f"want {want}, got {got}"
            if fx.get("known_bug"):
                cause += f" | known bug: {fx['known_bug']}"
            out.append(Result(area, fx["name"], FAIL, detail, total, cause))
        else:
            out.append(Result(area, fx["name"], PASS, detail, total))
    for cmd, mss in latency.items():
        mx = max(mss)
        name = f"latency {short(cmd)}"
        detail = f"max {mx} ms over {len(mss)} call(s), budget {ctx.hook_budget_ms} ms"
        if mx > ctx.hook_budget_ms:
            out.append(Result(area, name, FAIL, detail, mx, f"{mx} ms > {ctx.hook_budget_ms} ms (1 s mandate)"))
        else:
            out.append(Result(area, name, PASS, detail, mx))
    out.append(Result(area, "settings sources", PASS, "; ".join(str(f) for f in files)))
    return out
