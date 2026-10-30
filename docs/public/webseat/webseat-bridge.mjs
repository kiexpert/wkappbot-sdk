#!/usr/bin/env node
// webseat-bridge: lets the webseat page use YOUR OWN logged-in AI CLI (Claude Code, Codex, Gemini CLI)
// under your own subscription. The page talks to http://127.0.0.1:PORT/v1/chat/completions; this
// bridge runs the official CLI on your machine, so the CLI's login token never leaves the CLI.
//
//   node webseat-bridge.mjs --cli claude   [--model sonnet] [--port 8787]
//   node webseat-bridge.mjs --cli codex
//   node webseat-bridge.mjs --cli gemini
//
// It prints a one-time bridge key; paste it into the page's API key field with provider local-cli.
// Binds 127.0.0.1 only. No dependencies (Node 18+).
import http from 'node:http';
import { spawn } from 'node:child_process';
import { randomBytes } from 'node:crypto';
import { mkdtempSync, readFileSync, rmSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

const args = process.argv.slice(2);
const opt = (name, def) => {
  const i = args.indexOf('--' + name);
  return i >= 0 && i + 1 < args.length ? args[i + 1] : def;
};
const CLI = opt('cli', 'claude');
const PORT = Number(opt('port', '8787'));
const MODEL = opt('model', '');
const TIMEOUT_MS = Number(opt('timeout', '300')) * 1000;
const ORIGINS = opt('origin', 'https://kiexpert.github.io,http://localhost,http://127.0.0.1,null').split(',');
const KEY = opt('key', randomBytes(18).toString('base64url'));
const MAX_BODY = 4 * 1024 * 1024;

if (!['claude', 'codex', 'gemini'].includes(CLI)) {
  console.error('usage: node webseat-bridge.mjs --cli claude|codex|gemini [--model M] [--port 8787]');
  process.exit(2);
}

// ---- prompt: OpenAI messages + tools -> one text prompt that asks for a JSON reply ----
export const buildPrompt = (messages, tools) => {
  const lines = [];
  const sys = messages.filter(m => m.role === 'system').map(m => m.content).join('\n');
  if (sys) lines.push('SYSTEM INSTRUCTIONS:\n' + sys, '');
  if (tools && tools.length) {
    lines.push('TOOLS YOU MAY CALL (the caller runs them and returns the result):');
    for (const t of tools) {
      const f = t.function || t;
      lines.push('- ' + f.name + ': ' + (f.description || '') + ' params=' + JSON.stringify(f.parameters || {}));
    }
    lines.push('');
  }
  lines.push('CONVERSATION:');
  for (const m of messages) {
    if (m.role === 'system') continue;
    if (m.role === 'tool') { lines.push('[tool result ' + (m.tool_call_id || '') + ']\n' + m.content); continue; }
    let text = typeof m.content === 'string' ? m.content : '';
    if (m.tool_calls && m.tool_calls.length) {
      text += (text ? '\n' : '') + 'called: ' + JSON.stringify(m.tool_calls.map(c => ({ name: c.function.name, arguments: c.function.arguments })));
    }
    lines.push('[' + m.role + ']\n' + text);
  }
  lines.push('', 'REPLY FORMAT: answer with ONE JSON object and nothing else:',
    '{"content": "<text for the user>", "tool_calls": [{"name": "<tool>", "arguments": {...}}]}',
    'Use an empty tool_calls list when you are done. Do not use any tool of your own; only list calls.');
  return lines.join('\n');
};

// ---- reply: CLI text -> OpenAI chat.completion message ----
export const parseReply = (text) => {
  const s = String(text || '').trim();
  let obj = null;
  const a = s.indexOf('{'), b = s.lastIndexOf('}');
  if (a >= 0 && b > a) {
    try { obj = JSON.parse(s.slice(a, b + 1)); } catch (e) { obj = null; }
  }
  if (!obj || typeof obj !== 'object') return { role: 'assistant', content: s };
  const calls = Array.isArray(obj.tool_calls) ? obj.tool_calls.filter(c => c && c.name) : [];
  const msg = { role: 'assistant', content: typeof obj.content === 'string' ? obj.content : '' };
  if (calls.length) {
    msg.tool_calls = calls.map((c, i) => ({
      id: 'call_' + Date.now().toString(36) + '_' + i, type: 'function',
      function: { name: String(c.name), arguments: JSON.stringify(c.arguments || {}) }
    }));
  }
  return msg;
};

// ---- the official CLI command line; its own tools stay off, prompt goes on stdin ----
export const cliArgv = (cli, model, outFile) => {
  if (cli === 'claude') return ['claude', ['-p', '--restricted', '--strict-mcp-config', '--output-format', 'text', '--tools', ''].concat(model ? ['--model', model] : [])];
  if (cli === 'codex') return ['codex', ['exec', '--skip-git-repo-check', '--sandbox', 'read-only', '-o', outFile].concat(model ? ['-m', model] : [], ['-'])];
  return ['gemini', ['--approval-mode', 'plan', '-p', 'Follow the instructions given on stdin.'].concat(model ? ['-m', model] : [])];
};

// ---- run it in an empty scratch folder ----
const runCli = (prompt, model) => new Promise((resolve, reject) => {
  const dir = mkdtempSync(join(tmpdir(), 'webseat-'));
  const out = join(dir, 'last.txt');
  const [cmd, argv] = cliArgv(CLI, model || MODEL, out);
  const child = spawn(cmd, argv, { cwd: dir, shell: process.platform === 'win32', stdio: ['pipe', 'pipe', 'pipe'] });
  let stdout = '', stderr = '';
  const timer = setTimeout(() => { child.kill(); }, TIMEOUT_MS);
  child.stdout.on('data', d => { stdout += d; });
  child.stderr.on('data', d => { stderr += d; });
  child.on('error', e => { clearTimeout(timer); rmSync(dir, { recursive: true, force: true }); reject(e); });
  child.on('close', code => {
    clearTimeout(timer);
    let text = stdout;
    if (CLI === 'codex') { try { text = readFileSync(out, 'utf8'); } catch (e) { /* fall back to stdout */ } }
    rmSync(dir, { recursive: true, force: true });
    if (code !== 0 && !text.trim()) reject(new Error(cmd + ' exited ' + code + ': ' + stderr.slice(-400)));
    else resolve(text);
  });
  child.stdin.end(prompt);
});

// ---- http ----
const cors = (req, res) => {
  const o = req.headers.origin || '';
  if (ORIGINS.some(x => o === x || (x !== 'null' && o.startsWith(x + ':')))) res.setHeader('access-control-allow-origin', o);
  res.setHeader('access-control-allow-headers', 'content-type, authorization');
  res.setHeader('access-control-allow-methods', 'POST, GET, OPTIONS');
  res.setHeader('access-control-allow-private-network', 'true');
  res.setHeader('vary', 'origin');
};
const send = (res, code, obj) => { res.writeHead(code, { 'content-type': 'application/json' }); res.end(JSON.stringify(obj)); };

export const server = http.createServer((req, res) => {
  cors(req, res);
  if (req.method === 'OPTIONS') { res.writeHead(204); return res.end(); }
  if (req.headers.authorization !== 'Bearer ' + KEY) return send(res, 401, { error: { message: 'bad bridge key' } });
  if (req.method === 'GET' && req.url === '/v1/models') return send(res, 200, { data: [{ id: CLI, object: 'model' }] });
  if (req.method !== 'POST' || req.url !== '/v1/chat/completions') return send(res, 404, { error: { message: 'not found' } });
  let body = '';
  req.on('data', d => { body += d; if (body.length > MAX_BODY) req.destroy(); });
  req.on('end', async () => {
    let j;
    try { j = JSON.parse(body); } catch (e) { return send(res, 400, { error: { message: 'bad json' } }); }
    try {
      const text = await runCli(buildPrompt(j.messages || [], j.tools || []), j.model && j.model !== CLI ? j.model : '');
      const msg = parseReply(text);
      send(res, 200, { id: 'webseat-' + Date.now(), object: 'chat.completion', model: CLI,
        choices: [{ index: 0, message: msg, finish_reason: msg.tool_calls ? 'tool_calls' : 'stop' }] });
    } catch (e) {
      send(res, 502, { error: { message: String(e.message || e) } });
    }
  });
});

if (!process.env.WEBSEAT_BRIDGE_NO_LISTEN) {
  server.listen(PORT, '127.0.0.1', () => {
    console.log('webseat-bridge: cli=' + CLI + ' on http://127.0.0.1:' + PORT + '/v1');
    console.log('In webseat pick provider local-cli, base URL http://127.0.0.1:' + PORT + '/v1, API key:');
    console.log(KEY);
  });
}
