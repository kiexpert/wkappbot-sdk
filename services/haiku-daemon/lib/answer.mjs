// Runs `claude -p --model haiku` with every tool disabled and the prompt on stdin. Text out only.
// CLAUDE_BIN points tests at a fake binary. The child runs in an empty scratch directory.
import { spawn } from 'node:child_process';
import { mkdtempSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

export const SYSTEM_PROMPT =
  'You are the AppBot Code demo assistant. Answer in plain text, briefly. You have no tools, ' +
  'no file access and no network. Do not claim to have run anything.';

export function claudeArgs(model = 'haiku') {
  return ['-p', '--model', model, '--tools', '', '--output-format', 'text',
    '--no-session-persistence', '--strict-mcp-config', '--system-prompt', SYSTEM_PROMPT];
}

export function askClaude(prompt, { bin = process.env.CLAUDE_BIN || 'claude', model = 'haiku',
  timeoutMs = 120_000, maxOut = 16_000, env = process.env } = {}) {
  return new Promise((resolve) => {
    const cwd = mkdtempSync(join(tmpdir(), 'haiku-'));
    const child = spawn(bin, claudeArgs(model), { cwd, env, stdio: ['pipe', 'pipe', 'pipe'] });
    let out = '', err = '', done = false;
    const finish = (r) => { if (!done) { done = true; clearTimeout(t); resolve(r); } };
    const t = setTimeout(() => { child.kill('SIGKILL'); finish({ ok: false, error: 'timeout' }); }, timeoutMs);
    child.stdout.on('data', (d) => { if (out.length < maxOut) out += d; });
    child.stderr.on('data', (d) => { if (err.length < 2000) err += d; });
    child.on('error', (e) => finish({ ok: false, error: `spawn: ${e.code || e.message}` }));
    child.on('close', (code) => finish(code === 0 && out.trim()
      ? { ok: true, text: out.trim().slice(0, maxOut) }
      : { ok: false, error: `exit ${code}`, detail: err.trim().slice(0, 300) }));
    child.stdin.end(prompt);
  });
}
