// The signaler wire on GitHub: one issue per channel, each peer owns ONE comment (its note).
// The daemon is the HOST peer. Guests (visitors' pages) post kind "ask" {visitor, turn, text}; the daemon
// answers with kind "answer" {to, visitor, turn, text, latency_ms, status} in its own note's ring.
// Reads:  GET  /repos/{proj}/issues/{n}/comments?per_page=100 with If-None-Match (304 = free).
// Writes: POST /repos/{proj}/issues/{n}/comments once, then PATCH /repos/{proj}/issues/comments/{id}.
// All answers produced in one tick are coalesced into one PATCH (one write per visitor turn or fewer).
import { createHash } from 'node:crypto';
import { importKey, seal, open, parseNote, formatNote, parseLink } from './codec.mjs';
import { realClock } from './clock.mjs';

export class RetryAfter extends Error {
  constructor(status, sec) { super(`wire write ${status}, retry after ${sec}s`); this.sec = sec; }
}

const CADENCE = { burst: [100, 200, 400], activeMs: 500, activeForMs: 60_000, idleMs: 1200, minBudget: 0.2 };

export class GitHubIssueWire {
  constructor({ link, token, poller, fetchFn = globalThis.fetch, clock = realClock, ringMax = 24, log = () => {} }) {
    this.cfg = parseLink(link);
    Object.assign(this, { token, poller, fetchFn, clock, ringMax, log });
    this.origin = 'public';
    this.api = `https://api.github.com/repos/${this.cfg.proj}/issues`;
    // Stable host peer id per channel, so a restarted or successor run finds and keeps editing the same note.
    this.me = BigInt('0x' + createHash('sha256').update(`${this.cfg.session}|host`).digest('hex').slice(0, 12))
      .toString(36).padStart(8, '0').slice(-8);
    this.noteId = null; this.seq = 0; this.ring = []; this.queue = [];
    this.lastSeq = new Map(); this.ack = 0; this.lastSignalAt = -Infinity; this.burstLeft = [];
    this.answered = new Set(); // "<from>-<turn>" already in our own ring (restart / successor dedupe)
    this.cold = true; this.staleMs = 10 * 60_000;
  }
  async init() { this.key = await importKey(this.cfg.k); }

  async poll() {
    if (!this.key) await this.init();
    const r = await this.poller.get(`${this.api}/${this.cfg.issue}/comments?per_page=100`);
    if (!r.changed || !Array.isArray(r.body)) return [];
    const notes = r.body.map((c) => ({ c, note: parseNote(c.body) })).filter((x) => x.note);
    // Pass 1: adopt our own note (from an earlier run) BEFORE reading guests, so its answers dedupe.
    for (const { c, note: { env } } of notes) {
      if (env.from !== this.me || this.noteId != null) continue;
      this.noteId = c.id; this.ring = env.ring.slice(-this.ringMax); this.ack = env.ack || 0;
      this.seq = Math.max(0, ...env.ring.map((x) => x.seq || 0));
      for (const it of this.ring) {
        try { const m = await open(this.key, it.ct); if (m?.kind === 'answer') this.answered.add(`${m.data.to}-${m.data.turn}`); } catch {}
      }
    }
    const out = [];
    for (const { c, note: { env } } of notes) {
      if (env.from === this.me) continue;
      if (env.role !== 'guest') continue;
      // On a cold start, a guest note untouched for 10 min holds only old turns: mark them seen, answer none.
      const stale = this.cold && c.updated_at && this.clock.now() - Date.parse(c.updated_at) > this.staleMs;
      const seen = this.lastSeq.get(env.from) || 0;
      for (const item of env.ring) {
        if (!(item.seq > seen)) continue;
        this.lastSeq.set(env.from, Math.max(this.lastSeq.get(env.from) || 0, item.seq));
        this.ack = Math.max(this.ack, item.seq);
        let msg;
        try { msg = await open(this.key, item.ct); } catch { continue; }
        if (msg?.kind !== 'ask' || typeof msg.data?.text !== 'string') continue;
        const turn = String(msg.data.turn ?? item.seq).replace(/[^A-Za-z0-9_-]/g, '').slice(0, 20);
        if (stale || this.answered.has(`${env.from}-${msg.data.turn ?? item.seq}`)) continue;
        out.push({ origin: this.origin, raw: { id: `${env.from}-${turn}`.slice(0, 80), visitor: msg.data.visitor || env.from,
          prompt: msg.data.text, meta: { from: env.from, turn: msg.data.turn ?? item.seq } } });
      }
    }
    this.cold = false;
    if (out.length) this.signal();
    return out;
  }

  signal() {
    this.lastSignalAt = this.clock.now();
    const b = this.poller.last;
    const lowBudget = b?.remaining != null && b?.limit ? b.remaining / b.limit < CADENCE.minBudget : false;
    this.burstLeft = lowBudget ? [] : [...CADENCE.burst];
  }

  // After a signal: 100/200/400 ms, then 500 ms for 60 s, then 1200 ms idle.
  nextDelayMs() {
    if (this.burstLeft.length) return this.burstLeft.shift();
    return this.clock.now() - this.lastSignalAt < CADENCE.activeForMs ? CADENCE.activeMs : CADENCE.idleMs;
  }

  async publishAnswer(ans) {
    if (!ans.meta?.from) return; // not a wire request
    const { from, turn } = ans.meta;
    this.queue.push({ kind: 'answer', data: { to: from, visitor: ans.visitor, turn, status: ans.status,
      text: ans.text ?? null, reason: ans.reason ?? null, retry_after_ms: ans.retry_after_ms ?? null, latency_ms: ans.latency_ms ?? null } });
  }

  pendingWrites() { return this.queue.length > 0; }

  // One write carrying every queued answer. Returns the number of answers it carried (0 = no write).
  async flush() {
    if (!this.queue.length) return 0;
    if (!this.key) await this.init();
    const batch = this.queue.slice();
    const ring = [...this.ring];
    let seq = this.seq;
    for (const m of batch) ring.push({ seq: ++seq, ct: await seal(this.key, m) });
    const env = { v: 1, role: 'host', from: this.me, ack: this.ack, next: null, ring: ring.slice(-this.ringMax) };
    const body = JSON.stringify({ body: formatNote(env) });
    const headers = { Authorization: `Bearer ${this.token}`, Accept: 'application/vnd.github+json', 'Content-Type': 'application/json' };
    const url = this.noteId ? `${this.api}/comments/${this.noteId}` : `${this.api}/${this.cfg.issue}/comments`;
    const res = await this.fetchFn(url, { method: this.noteId ? 'PATCH' : 'POST', headers, body });
    if (res.status === 403 || res.status === 429) throw new RetryAfter(res.status, Number(res.headers?.get?.('retry-after')) || 60);
    if (!res.ok) throw new Error(`wire write ${res.status}`);
    if (!this.noteId) this.noteId = (await res.json()).id;
    this.ring = env.ring; this.seq = seq;
    this.queue.splice(0, batch.length);
    return batch.length;
  }
}
