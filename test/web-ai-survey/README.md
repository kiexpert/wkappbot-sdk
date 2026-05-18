# Free Web AI -- CDP Automation Survey

Probes whether a set of free public web-AI services can be driven by `wkappbot cdp` without manual login.

## What it does

For each candidate URL the script runs this sequence:

1. `wkappbot cdp open <url>` -- launches/reuses project Chrome, parses `cdp:PORT` from output.
2. Wait 3 s for the page to settle.
3. Eval-JS probe for an input surface: `textarea, [contenteditable=true], [role=textbox], input[type=text|search]`.
4. If an input is found, best-effort inject the literal text `say: SURVEY_OK` via property-descriptor setter + bubbling `input` event (React/Vue compatible). **The prompt is not submitted** -- this is a feasibility check, not an actual ask.
5. Sniff the body text (first 4 KB) for login-wall keywords: `sign in`, `log in`, `create account`, `sign up`, `login`, `signin`.
6. Classify:
   - `CDP_OK`         -- input present, no login wall detected.
   - `LOGIN_REQUIRED` -- login keywords present (with or without input).
   - `NO_INPUT`       -- no input surface and no login wall (probably skeleton / SPA not loaded / bot block).
   - `ERROR`          -- `cdp open` failed (no port in output / timeout).

## Sites (19)

`chatgpt.com`, `gemini.google.com`, `grok.com`, `duck.ai`, `perplexity.ai`,
`copilot.microsoft.com`, `chat.deepseek.com`, `chat.mistral.ai`, `huggingface.co/chat`,
`you.com`, `groq.com`, `meta.ai`, `kimi.ai`, `qwen.ai`, `pi.ai`, `phind.com`,
`venice.ai`, `aifreeforever.com`, `cohere.com/chat`.

## Run

```powershell
powershell -File D:/GitHub/wkappbot-sdk/test/web-ai-survey/test-free-web-ai-cdp.ps1
```

Per-site `cdp open` timeout: 30 s local, 45 s CI. Each eval-JS probe: 15 s. Total worst case ~25 min for the full 19-site sweep.

## Output

Stdout:

```
[RESULT] chatgpt.com               LOGIN_REQUIRED   port=9300 input=yes wall=yes
[RESULT] perplexity.ai             CDP_OK           port=9300 inject=ok input=yes
...
[SUMMARY] 7/19 CDP_OK without login
[SUMMARY] LOGIN_REQUIRED=10  NO_INPUT=1  ERROR=1
```

Files written under `bin/wkappbot.hq/logs/web-ai-survey/`:

- `<site>-open.log`  -- raw `cdp open` output (port + hwnd).
- `<site>-input.log` -- input-detection eval-JS result.
- `<site>-login.log` -- login-wall sniff result.
- `<site>-read.log`  -- first 200 chars of body (only when input was found).
- `summary.json`     -- machine-readable result list + counts.

## Exit codes

- `0` -- any site classified (typical case, even with all `LOGIN_REQUIRED`).
- `1` -- every site returned `ERROR` (Chrome/CDP itself is broken; re-run health check).

## Limitations

- The script does not solve CAPTCHA / Cloudflare-Turnstile / login flows. A site behind one is reported `LOGIN_REQUIRED` even if the underlying chat is technically reachable after login.
- Some SPAs render the input lazily; a 3 s wait will miss the slowest. Re-run individually with longer waits if a `NO_INPUT` result looks wrong.
- Injection is best-effort. Some sites (e.g. Lexical-based editors) need a different keystroke path -- `inject=err` in the detail line marks those.
- Per CLAUDE.md CDP discipline: the script uses only the project's SHA256-derived CDP port that `cdp open` returns. It never snoops other Chrome instances.
