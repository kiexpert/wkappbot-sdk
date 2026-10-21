#!/usr/bin/env python3
"""Self-test for the QA suite itself: every check runs against fakes (hook, installer, CLI,
temp git repos), so the suite is verified without the other repos. Run: python3 qa/selftest.py
"""
import argparse
import json
import os
import shutil
import subprocess
import sys
import tempfile
import textwrap
import unittest
from pathlib import Path

HERE = Path(__file__).resolve().parent
sys.path[:0] = [str(HERE / "lib"), str(HERE / "checks")]

import hooks  # noqa: E402
import hygiene  # noqa: E402
import installer  # noqa: E402
import matrix  # noqa: E402
import static_xplat  # noqa: E402
from qa_common import FAIL, FIXTURES, PASS, SKIP, Ctx  # noqa: E402

PY = sys.executable

FAKE_HOOK = r'''
import json, re, sys
p = json.load(sys.stdin)
cmd = (p.get("tool_input") or {}).get("command", "")
deny = re.search(r"git push .*(--force|-f)\b.*\bmain\b", cmd) or re.search(r"\bcd\b[^&;]*&&\s*cd\b", cmd) \
    or re.search(r"core\.hooksPath\s+(\"\"|'')", cmd)
if deny:
    print(json.dumps({"hookSpecificOutput": {"hookEventName": "PreToolUse", "permissionDecision": "deny",
                                             "permissionDecisionReason": "qa fake"}}))
'''

FAKE_INSTALLER = r'''
import json, os, sys
home = os.environ.get("USERPROFILE") or os.environ["HOME"]
f = os.path.join(home, ".claude", "settings.json")
s = json.load(open(f)) if os.path.exists(f) else {}
mine = {"type": "command", "command": "python3 KIHROOT/hooks/guard.py"}
grp = s.setdefault("hooks", {}).setdefault("PreToolUse", [])
args = sys.argv[1:]
if "--dry-run" in args:
    print("would wire 1 hook"); sys.exit(0)
if "--uninstall" in args:
    for g in grp: g["hooks"] = [h for h in g["hooks"] if h != mine]
    s["hooks"]["PreToolUse"] = [g for g in grp if g["hooks"]]
elif DUPLICATE or not any(mine in g["hooks"] for g in grp):
    grp.append({"matcher": "Bash", "hooks": [mine]})
json.dump(s, open(f, "w"), indent=2)
'''

FAKE_CLI = r'''
import os, sys
a = sys.argv[1:]
def opt(n):
    return a[a.index(n) + 1]
if a[:1] == ["--help"]: print("usage: wkappbot skill|a11y|file"); sys.exit(0)
if a[:1] == ["--version"]: print("wkappbot 7.11.0-sdk"); sys.exit(0)
if a[:2] == ["skill", "read"] and a[2] == "qa-no-such-skill-xyz": print("not found", file=sys.stderr); sys.exit(1)
if a[:1] == ["skill"]: print("grap: UI element addressing"); sys.exit(0)
if a[:2] == ["suggest", "list"]: print("0 suggests"); sys.exit(0)
if a[:2] == ["file", "read"]:
    sys.stdout.buffer.write(open(opt("--path"), "rb").read()); sys.exit(0)
if a[:2] == ["file", "write"]:
    d = os.path.dirname(opt("--path"))
    if d: os.makedirs(d, exist_ok=True)
    open(opt("--path"), "wb").write(open(opt("--source-file"), "rb").read()); sys.exit(0)
print("unknown command", file=sys.stderr); sys.exit(2)
'''


def make_ctx(**kw):
    ns = argparse.Namespace(kih_root=None, wkappbot_root=None, cli=None, transcript=None,
                            allow_mutations=False, since=None, update_baseline=False)
    for k, v in kw.items():
        setattr(ns, k, v)
    return Ctx(ns)


def git(*a, cwd):
    subprocess.run(["git", *a], cwd=cwd, check=True, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)


class Base(unittest.TestCase):
    def setUp(self):
        self.tmp = Path(tempfile.mkdtemp(prefix="qa-selftest-"))
        self._env = dict(os.environ)
        for k in ("KIH_ROOT", "WKAPPBOT_ROOT", "WKAPPBOT_CLI", "TRANSCRIPT", "HOOK_SETTINGS", "KIH_INSTALLER"):
            os.environ.pop(k, None)
        os.environ["HOME"] = os.environ["USERPROFILE"] = str(self.tmp / "realhome")

    def tearDown(self):
        os.environ.clear()
        os.environ.update(self._env)
        shutil.rmtree(self.tmp, ignore_errors=True)


class StaticRules(Base):
    def scan(self, text):
        return [(h[1], h[2]) for h in static_xplat.scan_lines("x.ps1", textwrap.dedent(text).splitlines())]

    def test_drive_literal_flagged(self):
        self.assertEqual(self.scan('$p = "D:\\GitHub\\WKAppBot\\bin"'), [(1, "drive-literal")])
        self.assertEqual(self.scan("p = 'C:/Users/someone/x'"), [(1, "drive-literal")])

    def test_resolver_last_step_allowed(self):
        src = """
        $r = $env:WKAPPBOT_HOME
        if (-not $r) { $r = Join-Path (Split-Path $PSScriptRoot) 'WKAppBot' }
        if (-not (Test-Path $r)) { $r = 'D:/GitHub/WKAppBot' }
        """
        self.assertEqual(self.scan(src), [])

    def test_allow_marker(self):
        self.assertEqual(self.scan('$x = "D:/SDK/bin"  # qa:allow-path'), [])

    def test_userprofile_only_flagged_and_home_ok(self):
        self.assertEqual(self.scan("$c = Join-Path $env:USERPROFILE '.claude'"), [(1, "userprofile-only")])
        ok = "$h = $env:USERPROFILE\nif (-not $h) { $h = $env:HOME }\n$c = Join-Path $h '.claude'"
        self.assertEqual(self.scan(ok), [])

    def test_baseline_and_since(self):
        repo = self.tmp / "repo"
        repo.mkdir()
        git("init", "-q", cwd=repo)
        git("config", "user.email", "qa@example.invalid", cwd=repo)
        git("config", "user.name", "qa", cwd=repo)
        (repo / "a.py").write_text('OLD = "D:/old"\n', encoding="utf-8")
        git("add", ".", cwd=repo)
        git("commit", "-qm", "init", cwd=repo)
        name = "selftest-" + os.urandom(4).hex()
        try:
            r = static_xplat.check_repo(name, repo, update=True)[0]
            self.assertEqual(r.status, PASS)
            self.assertEqual(static_xplat.check_repo(name, repo)[0].status, PASS)
            (repo / "b.py").write_text('NEW = "D:/new"\n', encoding="utf-8")
            r = static_xplat.check_repo(name, repo)[0]
            self.assertEqual(r.status, FAIL)
            self.assertIn("b.py:1", r.cause)
            git("add", "b.py", cwd=repo)
            r = static_xplat.check_repo(name, repo, since="HEAD")[0]
            self.assertEqual(r.status, FAIL)
            self.assertNotIn("a.py", r.cause)
            self.assertEqual(static_xplat.check_repo(name, repo, since="no-such-ref")[0].status, SKIP)
        finally:
            static_xplat.baseline_path(name).unlink(missing_ok=True)


class HookFixtures(Base):
    def kih(self, hook_src):
        kih = self.tmp / "kih"
        (kih / ".claude").mkdir(parents=True)
        (kih / "guard.py").write_text(hook_src, encoding="utf-8")
        cmd = f'"{PY}" "{kih / "guard.py"}"'
        settings = {"hooks": {
            "PreToolUse": [{"matcher": "Bash|Read", "hooks": [{"type": "command", "command": cmd}]}],
            "PostToolUse": [{"matcher": "Bash", "hooks": [{"type": "command", "command": cmd}]}],
            "Stop": [{"hooks": [{"type": "command", "command": cmd}]}]}}
        (kih / ".claude" / "settings.json").write_text(json.dumps(settings), encoding="utf-8")
        return kih

    def test_fake_harness_passes_all(self):
        res = hooks.run_checks(make_ctx(kih_root=str(self.kih(FAKE_HOOK))))
        bad = [(r.name, r.cause) for r in res if r.status != PASS]
        self.assertEqual(bad, [], bad)

    def test_allow_all_harness_fails_deny_rows(self):
        res = hooks.run_checks(make_ctx(kih_root=str(self.kih("import sys; sys.stdin.read()"))))
        failed = {r.name for r in res if r.status == FAIL}
        self.assertIn("PreToolUse Bash force-push main -> deny", failed)
        self.assertIn("PreToolUse Bash cd-chain -> deny", failed)
        self.assertNotIn("PreToolUse Bash plain ls -> allow", failed)

    def test_crash_and_latency(self):
        os.environ["QA_HOOK_BUDGET_MS"] = "1"
        res = hooks.run_checks(make_ctx(kih_root=str(self.kih("import sys, time; time.sleep(0.05); sys.exit(3)"))))
        self.assertTrue(any("hook crashed" in r.cause for r in res))
        self.assertTrue(any(r.name.startswith("latency") and r.status == FAIL for r in res))

    def test_rc2_is_deny(self):
        self.assertEqual(hooks.decision(2, ""), "deny")
        self.assertEqual(hooks.decision(0, '{"decision": "block"}'), "deny")
        self.assertEqual(hooks.decision(0, "plain text"), "allow")


class Installer(Base):
    def kih(self, duplicate):
        kih = self.tmp / "kih"
        kih.mkdir()
        src = FAKE_INSTALLER.replace("KIHROOT", str(kih).replace("\\", "/")).replace("DUPLICATE", str(duplicate))
        (kih / "fake_install.py").write_text(src, encoding="utf-8")
        sh = kih / "install-fake.sh"
        sh.write_text(f'#!/usr/bin/env bash\nexec "{Path(PY).as_posix()}" "{(kih / "fake_install.py").as_posix()}" "$@"\n',
                      encoding="utf-8")
        os.environ["KIH_INSTALLER"] = str(sh)
        return kih

    @unittest.skipUnless(shutil.which("bash"), "bash needed")
    def test_good_installer(self):
        res = installer.run_checks(make_ctx(kih_root=str(self.kih(False))))
        self.assertEqual([(r.name, r.cause) for r in res if r.status != PASS], [])

    @unittest.skipUnless(shutil.which("bash"), "bash needed")
    def test_non_idempotent_installer(self):
        res = {r.name: r for r in installer.run_checks(make_ctx(kih_root=str(self.kih(True))))}
        self.assertEqual(res["install #2 idempotent"].status, FAIL)
        self.assertIn("duplicate", res["install #2 idempotent"].cause)


class Matrix(Base):
    def test_cli_and_encoding_against_fake(self):
        fake = self.tmp / "fake_cli.py"
        fake.write_text(FAKE_CLI, encoding="utf-8")
        ctx = make_ctx()
        for f in ("cli-matrix.json", "encoding-matrix.json"):
            res = matrix.run_matrix(ctx, "cli", str(fake), FIXTURES / f, tool_prefix=[PY])
            self.assertEqual([(r.name, r.cause) for r in res if r.status == FAIL], [], f)
        self.assertTrue(all(r.status == SKIP for r in res if "skill edit" in r.name))

    def test_known_bug_in_cause(self):
        fake = self.tmp / "fake_cli.py"
        fake.write_text(FAKE_CLI.replace("if d: os.makedirs(d, exist_ok=True)", "pass"), encoding="utf-8")
        res = matrix.run_matrix(make_ctx(), "cli", str(fake), FIXTURES / "cli-matrix.json", tool_prefix=[PY])
        r = [x for x in res if x.name == "file write creates parent dirs"][0]
        self.assertEqual(r.status, FAIL)
        self.assertIn("known bug", r.cause)

    def test_missing_tool_skips(self):
        res = matrix.run_matrix(make_ctx(), "wkhippo", None, FIXTURES / "wkhippo-matrix.json")
        self.assertTrue(res and all(r.status == SKIP for r in res))


class Hygiene(Base):
    def repo(self):
        repo = self.tmp / "r"
        repo.mkdir()
        git("init", "-q", cwd=repo)
        return repo

    def test_githooks_mode(self):
        repo = self.repo()
        (repo / ".githooks").mkdir()
        (repo / ".githooks" / "pre-push").write_text("#!/bin/sh\n", encoding="utf-8")
        git("add", ".", cwd=repo)
        git("update-index", "--chmod=-x", ".githooks/pre-push", cwd=repo)
        self.assertEqual(hygiene.githooks_exec(repo).status, FAIL)
        git("update-index", "--chmod=+x", ".githooks/pre-push", cwd=repo)
        self.assertEqual(hygiene.githooks_exec(repo).status, PASS)

    def test_autocommit_trim(self):
        root = self.tmp / "wk"
        root.mkdir()
        f = root / "AutoCommit.cs"
        f.write_text("var l = line.Trim();\nvar path = l.Substring(3);\n", encoding="utf-8")
        self.assertEqual(hygiene.autocommit_trim(root).status, FAIL)
        f.write_text("var l = line.TrimEnd('\\r');\nvar path = l.Substring(3);\n", encoding="utf-8")
        self.assertEqual(hygiene.autocommit_trim(root).status, PASS)

    def test_personal_paths(self):
        repo = self.repo()
        user = "qa" + "person"
        (repo / "a.md").write_text("see C:/Users/test/x and /home/runner/w\n", encoding="utf-8")
        git("add", ".", cwd=repo)
        self.assertEqual(hygiene.personal_paths(repo, "r").status, PASS)
        (repo / "b.sh").write_text(f"X=C:\\\\Users\\\\{user}\\\\bin\n", encoding="utf-8")
        git("add", ".", cwd=repo)
        r = hygiene.personal_paths(repo, "r")
        self.assertEqual(r.status, FAIL)
        self.assertIn("q*****", r.detail)
        self.assertNotIn(user, r.detail + r.cause)

    def test_version_sync_drift(self):
        root = self.tmp / "sdk"
        (root / "csharp").mkdir(parents=True)
        (root / "CLAUDE.md").write_text("# WKAppBot v1.2.0-sdk - x\n", encoding="utf-8")
        (root / "CHANGELOG.md").write_text("# C\n\n## [1.3.0-sdk] - d\n", encoding="utf-8")
        (root / "VERSIONING.md").write_text("## Current version\n\n`v1.3.0-sdk` x\n", encoding="utf-8")
        (root / "README.md").write_text("### v1.3.0-sdk highlights (x)\n", encoding="utf-8")
        (root / "csharp" / "Directory.Build.props").write_text(
            "<WKAppBotBaseVersion>1.3</WKAppBotBaseVersion>", encoding="utf-8")
        r = hygiene.version_sync(root)
        self.assertEqual(r.status, FAIL)
        self.assertIn("CLAUDE.md header=1.2.0", r.cause)


class Runner(Base):
    def test_runner_table_and_exit(self):
        out = self.tmp / "results"
        p = subprocess.run([PY, str(HERE / "run-qa.py"), "--areas", "hooks,installer", "--results-dir", str(out),
                            "--kih-root", str(self.tmp / "nope")], stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
        txt = p.stdout.decode("utf-8", "replace")
        self.assertEqual(p.returncode, 0, txt)
        self.assertIn("PER-PLATFORM TABLE", txt)
        self.assertIn("not run here ->", txt)
        self.assertTrue(any(out.glob("*.json")))

    def test_runner_nonzero_on_fail(self):
        kih = self.tmp / "kih"
        (kih / ".githooks").mkdir(parents=True)
        git("init", "-q", cwd=kih)
        (kih / ".githooks" / "pre-push").write_text("#!/bin/sh\n", encoding="utf-8")
        git("add", ".", cwd=kih)
        git("update-index", "--chmod=-x", ".githooks/pre-push", cwd=kih)
        p = subprocess.run([PY, str(HERE / "run-qa.py"), "--areas", "hygiene", "--no-save", "--kih-root", str(kih)],
                           stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
        txt = p.stdout.decode("utf-8", "replace")
        self.assertEqual(p.returncode, 1, txt)
        self.assertIn("FAIL [hygiene] kih .githooks executable in git", txt)


if __name__ == "__main__":
    unittest.main(verbosity=2)
