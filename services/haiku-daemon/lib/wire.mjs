// Wire adapters. An adapter has: poll() -> [{ raw, origin }], publishAnswer(answer), and optionally
// close(). normalize() turns any accepted header dialect into one request shape:
//   { id, visitor, prompt, dialect }
// The signaler wire (relay.js) adapter plugs in here; LocalDirWire is the internal inbox and the test double.
import { readdirSync, readFileSync, mkdirSync, unlinkSync } from 'node:fs';
import { join } from 'node:path';
import { writeJsonAtomic } from './store.mjs';

const MAX_PROMPT = 4000;

export function normalize(raw) {
  if (!raw || typeof raw !== 'object') return null;
  let id, visitor, prompt, dialect;
  const h = raw.headers && typeof raw.headers === 'object' ? lowerKeys(raw.headers) : null;
  if (h) { // header dialect: HTTP-style header map beside a body
    dialect = 'headers';
    id = h['x-request-id'] ?? h['x-wk-id'] ?? raw.id;
    visitor = h['x-visitor-id'] ?? h['x-wk-visitor'] ?? raw.visitor;
    prompt = raw.body?.prompt ?? raw.prompt ?? (typeof raw.body === 'string' ? raw.body : undefined);
  } else { // flat dialect: fields at the top level
    dialect = 'flat';
    ({ id, visitor, prompt } = raw);
  }
  if (typeof id !== 'string' || typeof prompt !== 'string' || !prompt.trim()) return null;
  const out = { id, visitor: String(visitor || 'anonymous').slice(0, 80), prompt: prompt.slice(0, MAX_PROMPT), dialect };
  if (raw.meta && typeof raw.meta === 'object') out.meta = raw.meta; // wire routing (peer id, turn)
  return out;
}

const lowerKeys = (o) => Object.fromEntries(Object.entries(o).map(([k, v]) => [k.toLowerCase(), v]));

export class LocalDirWire {
  constructor({ inbox, outbox, origin = 'internal' }) {
    Object.assign(this, { inbox, outbox, origin });
    mkdirSync(inbox, { recursive: true });
    if (outbox) mkdirSync(outbox, { recursive: true });
  }
  async poll() {
    const out = [];
    for (const f of readdirSync(this.inbox).filter((n) => n.endsWith('.json'))) {
      const p = join(this.inbox, f);
      try { out.push({ raw: JSON.parse(readFileSync(p, 'utf8')), origin: this.origin }); } catch {}
      try { unlinkSync(p); } catch {}
    }
    return out;
  }
  async publishAnswer(ans) {
    if (this.outbox) writeJsonAtomic(join(this.outbox, `${ans.id}.json`), ans);
  }
}
