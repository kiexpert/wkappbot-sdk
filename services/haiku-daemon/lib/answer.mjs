// Runs the provider slot from provider.json (claude -p --model haiku now; another family later is a
// config change, not a code change) with no tools, the prompt on stdin, text out, and an output-token cap.
// Measures first-byte and total latency. CLAUDE_BIN overrides the binary (tests use a fake one).
import { spawn } from 'node:child_process';
import { mkdtempSync, readFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

export function loadProvider(path = new URL('../provider.json', import.meta.url)) {
  return JSON.parse(readFileSync(path, 'utf8'));
}

// The in-house stand-in persona. English source, Korean answers.
export function personaPrompt(name) {
  return `You are ${name}, a Korean in-house assistant model running inside the WKAppBot demo. ` +
    'Reply in Korean unless the user writes in another language. Answer in plain text and keep it brief. ' +
    'You have no tools, no files and no network; never claim to have run or opened anything. ' +
    'Do not volunteer which company or base model you come from. If the user directly asks what model ' +
    'you are or who made you, answer honestly that this demo runs on Claude Haiku by Anthropic.';
}

const fill = (s, v) => String(s).replace(/\{(\w+)\}/g, (_, k) => (k in v ? String(v[k]) : `{${k}}`));

export function slotCommand(cfg, env = process.env) {
  const fam = cfg.families?.[cfg.slot.family];
  if (!fam) throw new Error(`unknown provider family: ${cfg.slot.family}`);
  const v = { model: cfg.slot.model, system: personaPrompt(cfg.display_name), max_tokens: cfg.max_tokens };
  return {
    bin: env.CLAUDE_BIN || fam.bin,
    args: fam.args.map((a) => fill(a, v)),
    env: { ...env, ...Object.fromEntries(Object.entries(fam.env || {}).map(([k, x]) => [k, fill(x, v)])) },
  };
}

export function askProvider(prompt, { cfg = loadProvider(), env = process.env, maxOut = 16_000, now = Date.now } = {}) {
  const { bin, args, env: childEnv } = slotCommand(cfg, env);
  return new Promise((resolve) => {
    const cwd = mkdtempSync(join(tmpdir(), 'haiku-'));
    const t0 = now();
    const child = spawn(bin, args, { cwd, env: childEnv, stdio: ['pipe', 'pipe', 'pipe'] });
    let out = '', err = '', first = null, done = false;
    const finish = (r) => { if (!done) { done = true; clearTimeout(t); resolve({ ...r, first_byte_ms: first, total_ms: now() - t0 }); } };
    const t = setTimeout(() => { child.kill('SIGKILL'); finish({ ok: false, error: 'timeout' }); }, cfg.timeout_ms || 120_000);
    child.stdout.on('data', (d) => { if (first == null) first = now() - t0; if (out.length < maxOut) out += d; });
    child.stderr.on('data', (d) => { if (err.length < 2000) err += d; });
    child.on('error', (e) => finish({ ok: false, error: `spawn: ${e.code || e.message}` }));
    child.on('close', (code) => finish(code === 0 && out.trim()
      ? { ok: true, text: out.trim().slice(0, maxOut) }
      : { ok: false, error: `exit ${code}`, detail: err.trim().slice(0, 300) }));
    child.stdin.on('error', () => {}); // a child that exits before reading stdin must not crash the daemon
    child.stdin.end(prompt);
  });
}
