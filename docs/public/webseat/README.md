# webseat

One browser tab where an expensive spokesman model commands cheap sector models through an agent_cmd tool call, with no server.

- Roles: spokesman (anthropic, openai-compatible, openrouter, mock); sector (any openai-compatible endpoint, or mock-sector).
- Command-only mode (default on): the spokesman sees only file names and sector reports, never file text.
- Sign in with OpenRouter: the PKCE login stores the key in this browser only.
- Tools: list_files, read_file, write_file, edit_file, run_js (Web Worker, 5 s).
- Test: `node docs/public/webseat/selftest.js` prints `PASS 13/13` with mock models and no network.
- Security: keys stay in localStorage; only the endpoints you configure receive data.
