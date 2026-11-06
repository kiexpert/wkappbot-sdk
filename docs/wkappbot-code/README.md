# Wisdom Kernel AppBot Code (`wkappbot-code`)

> **An AI coding agent with a wisdom reflex body, wired to an a11y-linked harness and A11Y QA.**
>
> Skills are its memory. Reflex guards refuse a bad tool call *before* it runs, and every refusal names the skill that fixes it.
> The lesson from each refusal is written back into a skill, so the same mistake costs less the next time.
> And it tests the way a person does: **A11Y QA** clicks and reads the real app through its accessibility tree, so a passing test means the screen works, not that a mock passed.

```bash
npm i -g wkappbot-code && wkappbot-code install && wkappbot-code doctor
```

[![npm](https://img.shields.io/npm/v/wkappbot-code?label=npm%20wkappbot-code)](https://www.npmjs.com/package/wkappbot-code)
Published from this repository's [`npm-publish-wkappbot-code`](../../.github/workflows/npm-publish-wkappbot-code.yml) workflow, with npm provenance.

---

## 한국어 요약

**Wisdom Kernel AppBot Code(`wkappbot-code`)는 AI 코딩 에이전트(OpenCode)에 "지혜 반사체"를 입힌 개발자용 배포판입니다.**

- **지혜 반사체:** 스킬이 기억 역할을 합니다. 잘못된 도구 호출은 실행되기 *전에* 반사 가드가 거부하고, 거부할 때마다 해결 방법이 담긴 스킬 이름을 알려 줍니다. 거부에서 얻은 교훈은 다시 스킬에 새겨집니다.
- **a11y 연동 하네스:** Windows UIA, Chrome CDP, macOS AX 같은 접근성 트리로 실제 데스크톱·웹 앱을 다룹니다. 컨트롤을 **이름으로** 찾고, 화면 좌표를 쓰지 않으며, 창이 화면 밖에 있어도 동작하고, 사용자의 창 크기를 바꾸지 않습니다.
- **A11Y QA:** 사람이 하듯 실제 앱을 접근성 트리로 클릭하고 읽는 QA입니다. 그래서 테스트 통과가 곧 실제 화면이 동작한다는 뜻입니다.
- **설치:** `npm i -g wkappbot-code` → `wkappbot-code install` → `wkappbot-code doctor`
- **플랫폼:** a11y·CDP 구동은 현재 Windows에서 전부 지원합니다. Linux·macOS에서는 에이전트, 하네스, 스킬(`skill`/`file`/`suggest`)이 동작하고, a11y 구동은 준비 중입니다(아래 표 참고).

---

## (a) Why

**Models drift.** The same agent that followed a rule an hour ago skips it today, and it reports success either way. A rule written in a prompt is a *claim* the model makes about itself. A guard that only trusts the claim passes the very run it should stop.

So the guards here gate on the **falsifier**, not on the claim:

- A commit is accepted when the check it names has actually run in this session, not when the model says it ran one.
- A "done" report is accepted when the file, test or run id it names exists, not when the sentence says *done*.
- A binary or skill bundle is accepted when its bytes match the published `SHA256SUMS`, not when the download "succeeded".

A refusal is not a dead end. It names the skill that holds the fix, so the agent reads it and continues.

## (b) The wisdom reflex body

```
            ┌──────────────────────── lesson engraved back (like / heal) ─────────────────────┐
            ▼                                                                                  │
  skills (memory) ──read──▶  agent plans a tool call ──▶  REFLEX GUARD (pre)  ──allow──▶ tool runs ──▶ result
                                                              │ refuse (exit 2)                         │
                                                              ▼                                         ▼
                                                  "blocked: read skill <id>"                 STOP GATE checks the
                                                              │                              report against evidence
                                                              └──────── agent reads the skill, retries ─┘
```

| Part | What it does |
|---|---|
| **Skills** | Versioned, searchable know-how (`wkappbot skill search <topic>`, `wkappbot skill read <id>`). The public set ships with the install. |
| **Intent lock** | A command that touches guarded ground is refused until the agent has looked up the matching skill first. |
| **Reflex guard** | The OpenCode plugin `plugin/wisdom-kernel.js` maps `tool.execute.before` onto the harness `pre` hook. Exit 2 refuses the call, with the hook's reason as the message. |
| **Stop gate** | At the end of a turn, the report is checked against what actually happened, and an unproven claim is sent back. |
| **Like / heal loop** | A reflex that carried real work gets a *like* and rises; a stale one sinks and is healed into a reference tier. Lessons are written back as short steps of the form "do X when Y". |

## (c) A11Y QA — the harness drives real apps by name

**A11Y QA** means testing the app the way a person uses it: through the same accessibility tree a screen reader sees. The a11y-linked harness:

- finds controls by **name and role** (Windows UIA/MSAA, Chrome CDP + DOM a11y, macOS AX), never by screen coordinates;
- works on windows that are **off-screen or behind** others: it is *focusless* and never steals the keyboard;
- **never resizes or moves the user's windows** to make a test pass;
- falls back UIA → Win32 messages → OCR/vision only when an app exposes no tree (owner-drawn MFC, games).

A passing A11Y QA run is a working screen, not a mocked one.

**Try it** (Windows, with the [SDK installed](../INSTALL.md)):

```bash
wkappbot a11y read "*Notepad*"                 # read every named control of a Notepad window
wkappbot a11y find "*Notepad*"                 # resolve it; prints a copy-paste '# TARGET "hwnd:0x..."' line
wkappbot a11y read "Chrome#example.com"        # a browser tab: CDP first, UIA fallback
```

**How the harness proves itself.** Each guard is proven by what it **refuses**, not by what it allows:

- A guard refuses by exit code, not by politeness: when the harness `pre` hook exits 2, the plugin throws and OpenCode never runs the tool call, and the hook's reason is shown to the agent. `wkappbot-code doctor` shows whether the plugin is wired and which harness it calls (no harness found = the plugin lets every call through, and doctor says so).
- Downloads are refused on a **sha256 mismatch**: the core binary is accepted only when it matches its `SHA256SUMS` line, and a skill bundle with the wrong hash installs nothing (`sha256 MISMATCH ... no skill was copied`).
- The **public-safety scan** fails this repository's build on any private word, personal email or owner path, and the npm package is masked and re-scanned before every publish: `bash scripts/public-safety-scan.sh skills` → `PUBLIC-SAFETY HITS 0`.
- The macOS build and its portable tests run on `macos-latest` CI ([`macos-test.yml`](../../.github/workflows/macos-test.yml)).

## (d) Install

```bash
npm i -g wkappbot-code          # Node 18+; also installs the command alias wk-appbot-code
wkappbot-code install           # lays down every part below (idempotent; safe to re-run)
wkappbot-code doctor            # one PASS / WARN / FAIL / SKIP row per part
wkappbot-code --version
```

`install` takes `--no-npm`, `--no-bin`, `--no-harness`, `--no-skills`, `--web-harness`, `--config-dir DIR`. An existing `~/.config/opencode/opencode.json` is backed up (`opencode.json.bak-<stamp>`) and only the plugin entry is merged in; every other key you set stays. A config that is not plain JSON is left untouched, and `wkappbot-code.json` is written beside it for you to merge by hand.

**Coming next (not in 0.3.0):** `wkappbot-code update` (sha-checked self-update) and `wkappbot-code config` (detect, edit or generate the minimum OpenCode settings). Until then, re-run `npm i -g wkappbot-code@latest && wkappbot-code install` to update.

**Pick a model:** `opencode run -m provider/model "prompt"` for one headless turn, or `/models` inside the TUI. Keys stay in your environment; nothing is stored in git.

## (e) What gets installed

| Part | What | Verified by |
|---|---|---|
| **opencode** | upstream OpenCode, pinned (`opencode-ai@1.19.5`), via `npm -g` | npm |
| **plugin** | `plugin/wisdom-kernel.js` plus its `opencode.json` plugin entry | install log |
| **core binary** | the `wkappbot` core for your platform, from this repository's latest release asset `wkappbot-<platform>`, placed at `~/.local/share/wkappbot/wkappbot` and linked as `wkappbot` | its line in the release `SHA256SUMS`; mismatch → refused |
| **harness** | the scrubbed web harness to `~/.wk-harness` (only when no local harness checkout is found) | sha256 of the bundle |
| **skills** | the latest public skills, into the core's `wkappbot.hq/skills` (read-only copy); your own edits go to `~/.wkappbot/private-skills`, a local git repo with no remote | sha256 of the bundle |

## (f) Platform matrix

| | Windows 10/11 x64 | macOS (arm64, x64) | Linux x64 |
|---|---|---|---|
| `npm i -g wkappbot-code`, agent + reflex plugin | ✅ | ✅ | ✅ |
| core binary via `wkappbot-code install` | use the [SDK installer](../INSTALL.md) | ✅ `osx-arm64` / `osx-x64` | ✅ `linux-x64` |
| harness + public skills | ✅ | ✅ | ✅ |
| `wkappbot skill` / `file` / `suggest` | ✅ | ✅ | ✅ |
| `wkappbot a11y` (read/find/click/type/...) — A11Y QA | ✅ full (UIA/Win32/OCR) | 🚧 planned (macOS AX backend under CI test, not yet a CLI verb) | 🚧 planned |
| `wkappbot cdp` / browser a11y | ✅ | 🚧 planned | 🚧 planned |

✅ live today · 🚧 planned. The matrix is updated with each release; a row is marked ✅ only once its CI job passes.

## (g) Uninstall

```bash
npm uninstall -g wkappbot-code opencode-ai
rm -rf ~/.local/share/wkappbot ~/.local/bin/wkappbot ~/.wk-harness   # core binary, its link, web harness
# optional: your own skill edits
rm -rf ~/.wkappbot/private-skills
```

Then remove the `wisdom-kernel` entry from the `plugin` list in `~/.config/opencode/opencode.json` (or restore the `opencode.json.bak-<stamp>` that `install` saved).

---

**Floor.** Signing, submitting and paying stay human-only, whatever the agent is asked to do.

*Maintainer note:* the registry publish runs only with the `NPM_TOKEN` Actions secret; without it the workflow logs `NPM_TOKEN not set -- registry publish skipped` and stays green.
