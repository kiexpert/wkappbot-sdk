"""kih installer: --dry-run changes nothing, install twice is idempotent, --uninstall restores.

Runs inside a throwaway HOME/USERPROFILE seeded with a user settings.json that carries a
user-owned hook and key; the installer must keep both and --uninstall must restore the seed.
Hooks may land in any Claude settings scope, so all four are watched:
  <HOME>/.claude/settings.json, <HOME>/.claude/settings.local.json,
  <KIH_ROOT>/.claude/settings.json, <KIH_ROOT>/.claude/settings.local.json
The installer NEVER runs against the real checkout: it may resolve its own root from its
script location (install-linux.sh does), so KIH_ROOT is copied (git-tracked files + .claude/)
into the sandbox and the copy's installer runs there. The real KIH_ROOT is hashed before and
after; any change is a FAIL row.
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

USER_HOOK = "echo qa-user-hook"
SEED = {
    "model": "qa-keep",
    "hooks": {"PreToolUse": [{"matcher": "Bash", "hooks": [{"type": "command", "command": USER_HOOK}]}]},
}
CANDIDATES = {
    "windows": ["install.ps1", "install-windows.ps1"],
    "macos": ["install-macos.sh", "install-linux.sh"],
    "linux": ["install-linux.sh"],
}
NAMES = ["dry-run changes nothing", "install #1", "install #2 idempotent", "user settings preserved",
         "uninstall restores seed", "real KIH_ROOT untouched"]


def find_installer(ctx):
    if os.environ.get("KIH_INSTALLER"):
        p = Path(os.environ["KIH_INSTALLER"]).resolve()
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


def snapshot(paths):
    """Hash of every file under the given dirs/files (missing ones count as absent)."""
    h = hashlib.sha256()
    for root in paths:
        root = Path(root)
        items = sorted(root.rglob("*")) if root.is_dir() else [root]
        for p in items:
            h.update(str(p).replace("\\", "/").encode())
            if p.is_symlink():
                h.update(b"L" + os.readlink(p).encode())
            elif p.is_file():
                h.update(p.read_bytes())
            else:
                h.update(b"-")
    return h.hexdigest()


def hook_cmds(settings):
    out = []
    for groups in (settings.get("hooks") or {}).values():
        for g in groups or []:
            out += [h.get("command", "") for h in g.get("hooks", [])]
    return out


def read_json(p):
    try:
        return json.loads(p.read_text(encoding="utf-8")) if p.is_file() else {}
    except ValueError:
        return {"_unparseable": True}


def copy_checkout(src, dst):
    """git-tracked files (+ untracked non-ignored) and .claude/ of src into dst; no .git."""
    rc, out, _, _ = run(["git", "-C", str(src), "ls-files", "-z", "--cached", "--others", "--exclude-standard"])
    rels = [r for r in out.split("\0") if r] if rc == 0 else \
        [str(p.relative_to(src)) for p in Path(src).rglob("*") if p.is_file() and ".git" not in p.parts]
    claude = Path(src) / ".claude"
    if claude.is_dir():
        rels += [str(p.relative_to(src)) for p in claude.rglob("*") if p.is_file()]
    for rel in set(rels):
        s, d = Path(src) / rel, Path(dst) / rel
        if s.is_file() or s.is_symlink():
            d.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(s, d, follow_symlinks=False)


def run_checks(ctx):
    area = "installer"
    if ctx.kih_root is None:
        return [Result(area, n, SKIP, "KIH_ROOT not found") for n in NAMES]
    real_inst = find_installer(ctx)
    if real_inst is None:
        return [Result(area, n, SKIP, f"no installer for {ctx.platform} in KIH_ROOT") for n in NAMES]
    real_claude = [ctx.kih_root / ".claude" / "settings.json", ctx.kih_root / ".claude" / "settings.local.json"]
    real_before = snapshot(real_claude)
    sb = Path(tempfile.mkdtemp(prefix="qa-install-"))
    kih = sb / "kih"
    copy_checkout(ctx.kih_root, kih)
    try:
        inst = kih / real_inst.relative_to(ctx.kih_root)
    except ValueError:  # $KIH_INSTALLER outside the checkout: copy it next to the copy's root
        inst = kih / real_inst.name
        shutil.copy2(real_inst, inst)
    home = sb / "home"
    seed_file = home / ".claude" / "settings.json"
    seed_file.parent.mkdir(parents=True)
    seed_file.write_text(json.dumps(SEED, indent=2), encoding="utf-8")
    proj = [kih / ".claude" / "settings.json", kih / ".claude" / "settings.local.json"]
    watched = [seed_file, home / ".claude" / "settings.local.json"] + proj
    env = {"HOME": str(home), "USERPROFILE": str(home), "XDG_CONFIG_HOME": str(home / ".config"),
           "XDG_DATA_HOME": str(home / ".local/share"), "KIH_ROOT": str(kih)}
    if ctx.wk_root:
        env["WKAPPBOT_HOME"] = str(ctx.wk_root)
    out = []

    def step(*extra):
        return run(argv_for(inst, *extra), cwd=str(kih), env=env, timeout=max(ctx.timeout, 180))

    def all_cmds():
        return [c for p in watched for c in hook_cmds(read_json(p))]

    def raw():
        return [p.read_bytes() if p.is_file() else None for p in watched]

    def where(cmds):
        return ", ".join(sorted({str(p.relative_to(sb) if sb in p.parents else p.name) for p in watched
                                 if set(hook_cmds(read_json(p))) & set(cmds)}))

    try:
        before = snapshot([home] + proj)
        rc, so, se, ms = step("--dry-run")
        if rc != 0:
            out.append(Result(area, NAMES[0], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif snapshot([home] + proj) != before:
            out.append(Result(area, NAMES[0], FAIL, inst.name, ms, "--dry-run modified HOME or the copy's .claude/"))
        else:
            out.append(Result(area, NAMES[0], PASS, inst.name, ms))

        rc, so, se, ms = step()
        raw1 = raw()
        mine = sorted({c for c in all_cmds() if c and c != USER_HOOK})
        if rc != 0:
            out.append(Result(area, NAMES[1], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif not mine:
            out.append(Result(area, NAMES[1], FAIL, inst.name, ms, "no harness hooks in any watched settings file"))
        else:
            out.append(Result(area, NAMES[1], PASS, f"{len(mine)} hook command(s) in {where(mine)}", ms))

        rc, so, se, ms = step()
        cmds = all_cmds()
        dups = len(cmds) - len(set(cmds))
        if rc != 0:
            out.append(Result(area, NAMES[2], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif raw() != raw1:
            out.append(Result(area, NAMES[2], FAIL, inst.name, ms,
                              f"settings changed on 2nd run ({dups} duplicate hook command(s))"))
        else:
            out.append(Result(area, NAMES[2], PASS, "watched settings byte-identical", ms))

        s2 = read_json(seed_file)
        kept = s2.get("model") == "qa-keep" and USER_HOOK in hook_cmds(s2)
        out.append(Result(area, NAMES[3], PASS if kept else FAIL, "model key + user hook",
                          cause="" if kept else "installer dropped user-owned settings"))

        rc, so, se, ms = step("--uninstall")
        left = [c for c in all_cmds() if c in mine]
        if rc != 0:
            out.append(Result(area, NAMES[4], FAIL, inst.name, ms, f"rc={rc}: {first_line(se, so)}"))
        elif left:
            out.append(Result(area, NAMES[4], FAIL, inst.name, ms,
                              f"{len(left)} harness hook(s) left in {where(left)}: {left[0][:100]}"))
        elif read_json(seed_file) != SEED:
            out.append(Result(area, NAMES[4], FAIL, inst.name, ms, "HOME settings.json differs from the seed"))
        else:
            out.append(Result(area, NAMES[4], PASS, "no harness hooks left, HOME settings.json == seed", ms))
    finally:
        shutil.rmtree(sb, ignore_errors=True)
    touched = snapshot(real_claude) != real_before
    out.append(Result(area, "real KIH_ROOT untouched", FAIL if touched else PASS, ".claude/settings*.json",
                      cause="the installer run changed the REAL checkout's .claude settings" if touched else ""))
    return out
