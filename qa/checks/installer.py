"""kih installer: --dry-run changes nothing, install twice is idempotent, --uninstall restores.

Runs inside a throwaway HOME/USERPROFILE seeded with a user settings.json that carries a
user-owned hook and key; the installer must keep both and --uninstall must restore the seed.
Installer: $KIH_INSTALLER, else per platform under KIH_ROOT:
  windows: install.ps1, install-windows.ps1     macos: install-macos.sh, install-linux.sh
  linux:   install-linux.sh
"""
import hashlib
import json
import os
import shutil
import tempfile
from pathlib import Path

from qa_common import FAIL, PASS, SKIP, Result, first_line, run

SEED = {
    "model": "qa-keep",
    "hooks": {"PreToolUse": [{"matcher": "Bash", "hooks": [{"type": "command", "command": "echo qa-user-hook"}]}]},
}
CANDIDATES = {
    "windows": ["install.ps1", "install-windows.ps1"],
    "macos": ["install-macos.sh", "install-linux.sh"],
    "linux": ["install-linux.sh"],
}


def find_installer(ctx):
    if os.environ.get("KIH_INSTALLER"):
        p = Path(os.environ["KIH_INSTALLER"])
        return p if p.is_file() else None
    for n in CANDIDATES[ctx.platform]:
        p = ctx.kih_root / n
        if p.is_file():
            return p
    return None


def argv_for(inst, *extra):
    if inst.suffix.lower() == ".ps1":
        return ["pwsh", "-NoProfile", "-NonInteractive", "-File", str(inst), *extra]
    return ["bash", str(inst), *extra]


def snapshot(root):
    h = hashlib.sha256()
    for p in sorted(Path(root).rglob("*")):
        rel = str(p.relative_to(root)).replace("\\", "/")
        h.update(rel.encode())
        if p.is_symlink():
            h.update(b"L" + os.readlink(p).encode())
        elif p.is_file():
            h.update(p.read_bytes())
    return h.hexdigest()


def hook_cmds(settings):
    out = []
    for groups in (settings.get("hooks") or {}).values():
        for g in groups or []:
            out += [h.get("command", "") for h in g.get("hooks", [])]
    return out


def run_checks(ctx):
    area = "installer"
    names = ["dry-run changes nothing", "install #1", "install #2 idempotent", "user settings preserved",
             "uninstall restores seed"]
    if ctx.kih_root is None:
        return [Result(area, n, SKIP, "KIH_ROOT not found") for n in names]
    inst = find_installer(ctx)
    if inst is None:
        return [Result(area, n, SKIP, f"no installer for {ctx.platform} in KIH_ROOT") for n in names]
    sb = Path(tempfile.mkdtemp(prefix="qa-install-"))
    home = sb / "home"
    sfile = home / ".claude" / "settings.json"
    sfile.parent.mkdir(parents=True)
    sfile.write_text(json.dumps(SEED, indent=2), encoding="utf-8")
    env = {"HOME": str(home), "USERPROFILE": str(home), "XDG_CONFIG_HOME": str(home / ".config"),
           "XDG_DATA_HOME": str(home / ".local/share"), "KIH_ROOT": str(ctx.kih_root)}
    if ctx.wk_root:
        env["WKAPPBOT_HOME"] = str(ctx.wk_root)
    out = []
    kih = str(ctx.kih_root).replace("\\", "/")

    def step(*extra):
        return run(argv_for(inst, *extra), cwd=str(ctx.kih_root), env=env, timeout=max(ctx.timeout, 180))

    def load():
        return json.loads(sfile.read_text(encoding="utf-8")) if sfile.is_file() else {}

    try:
        before = snapshot(home)
        rc, so, se, ms = step("--dry-run")
        after = snapshot(home)
        if rc != 0:
            out.append(Result(area, names[0], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif before != after:
            out.append(Result(area, names[0], FAIL, inst.name, ms, "--dry-run modified the sandbox HOME"))
        else:
            out.append(Result(area, names[0], PASS, inst.name, ms))

        rc, so, se, ms = step()
        s1, raw1 = load(), sfile.read_bytes() if sfile.is_file() else b""
        mine = [c for c in hook_cmds(s1) if c and c != "echo qa-user-hook"]
        if rc != 0:
            out.append(Result(area, names[1], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif not mine:
            out.append(Result(area, names[1], FAIL, inst.name, ms, "no harness hooks written to settings.json"))
        else:
            out.append(Result(area, names[1], PASS, f"{len(mine)} hook command(s) wired", ms))

        rc, so, se, ms = step()
        s2, raw2 = load(), sfile.read_bytes() if sfile.is_file() else b""
        cmds = hook_cmds(s2)
        dups = len(cmds) - len(set(cmds))
        if rc != 0:
            out.append(Result(area, names[2], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif raw1 != raw2:
            cause = f"settings.json changed on 2nd run ({dups} duplicate hook command(s))"
            out.append(Result(area, names[2], FAIL, inst.name, ms, cause))
        else:
            out.append(Result(area, names[2], PASS, "settings.json byte-identical", ms))

        kept = s2.get("model") == "qa-keep" and "echo qa-user-hook" in cmds
        out.append(Result(area, names[3], PASS if kept else FAIL, "model key + user hook",
                          cause="" if kept else "installer dropped user-owned settings"))

        rc, so, se, ms = step("--uninstall")
        s3 = load()
        left = [c for c in hook_cmds(s3) if kih in c.replace("\\", "/") or c in mine]
        if rc != 0:
            out.append(Result(area, names[4], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif left:
            out.append(Result(area, names[4], FAIL, inst.name, ms, f"{len(left)} harness hook(s) left: {left[0][:100]}"))
        elif s3 != SEED:
            out.append(Result(area, names[4], FAIL, inst.name, ms, "settings.json differs from the pre-install seed"))
        else:
            out.append(Result(area, names[4], PASS, "settings.json == seed", ms))
    finally:
        shutil.rmtree(sb, ignore_errors=True)
    return out
