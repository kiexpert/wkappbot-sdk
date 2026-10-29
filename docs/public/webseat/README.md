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

  Only OpenRouter offers a public browser OAuth flow; the others take a pasted key. "check key" makes one authenticated GET (a models or key-info endpoint) and spends no tokens.
- Session file: every turn autosaves to localStorage. Export writes a `.jsonl` file, one line per message in Claude Code shape (`type`, `message`, `uuid`, `parentUuid`, `timestamp`, `sessionId`; sector lines carry `isSidechain: true`), plus one `webseat-files` line with the virtual files. Import restores both the history and the files. API keys are never written to the export.
- Tools: list_files, read_file, write_file, edit_file, run_js (Web Worker, 5 s).
- Test: `node docs/public/webseat/selftest.js` prints `PASS 29/29` with mock models and mocked endpoints, no network. CI runs it on ubuntu, windows and macos (`.github/workflows/webseat-qa.yml`).
- Security: keys stay in localStorage; only the endpoints you configure receive data.
