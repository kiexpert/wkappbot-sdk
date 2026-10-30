# webseat

One browser tab where an expensive spokesman model commands cheap sector models through an agent_cmd tool call, with no server.

- Roles: spokesman (anthropic, openai-compatible, openrouter, gemini, mock); sector (any openai-compatible endpoint, or mock-sector).
- Command-only mode (default on): the spokesman sees only file names and sector reports, never file text.
- Login matrix, per spokesman provider:

  | provider | how you log in | key page |
  |---|---|---|
  | openrouter | Sign in with OpenRouter (OAuth PKCE), or paste a key | https://openrouter.ai/settings/keys |
  | anthropic | paste an API key | https://console.anthropic.com/settings/keys |
  | openai-compatible | paste an API key for the host | https://platform.openai.com/api-keys |
  | gemini | paste a Gemini API key (OpenAI-compatible endpoint) | https://aistudio.google.com/apikey |
  | local-cli | run `node webseat-bridge.mjs --cli claude` (or `codex`, `gemini`) on your machine, paste the key it prints | webseat-bridge.mjs |

  Only OpenRouter offers a public browser OAuth flow; the others take a pasted key. "check key" makes one authenticated GET (a models or key-info endpoint) and spends no tokens.
- Subscription login (local-cli): `webseat-bridge.mjs` (Node 18+, no dependencies) runs the official CLI you are already logged in to (`claude -p`, `codex exec`, `gemini -p`) and serves it at `http://127.0.0.1:8787/v1` in the OpenAI chat-completions shape. The CLI's own login token never leaves the CLI; the page only holds a one-time bridge key. It binds 127.0.0.1, keeps the CLI's own tools off, and allows only this site and localhost as origins. Pick `local-cli` for the spokesman, the sector, or both. Chrome and Edge reach localhost from the https page; Safari may refuse it.
- Session file: every turn autosaves to localStorage. Export writes a `.jsonl` file, one line per message in Claude Code shape (`type`, `message`, `uuid`, `parentUuid`, `timestamp`, `sessionId`; sector lines carry `isSidechain: true`), plus one `webseat-files` line with the virtual files. Import restores both the history and the files. API keys are never written to the export.
- Tools: list_files, read_file, write_file, edit_file, run_js (Web Worker, 5 s), skill_search, skill_read.
- Skill tools: `skill_search` and `skill_read` read the public wkappbot skill catalog that this site serves at `../skills/skills-data-full.js` (the same file the skill browser uses), so a spokesman or sector can look up a skill before it acts. They hold no file text, so command-only mode keeps them for the spokesman too.
- Test: `node docs/public/webseat/selftest.js` prints `PASS 34/34` with mock models, mocked endpoints and the real skill catalog file, no network. `node docs/public/webseat/bridge-selftest.mjs` checks the bridge's prompt, reply and CLI-argument shaping without running a CLI. CI runs both on ubuntu, windows and macos (`.github/workflows/webseat-qa.yml`).
- Security: keys stay in localStorage; only the endpoints you configure receive data.
