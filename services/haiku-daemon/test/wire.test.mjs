// The signaler wire against a fake GitHub issue-comments API (ETag, POST, PATCH, 403 Retry-After).
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mkdtempSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { fakeClock } from '../lib/clock.mjs';
import { ReadPacer, WritePacer } from '../lib/pacer.mjs';
import { EtagPoller } from '../lib/etag.mjs';
import { GitHubIssueWire } from '../lib/ghwire.mjs';
import { HaikuService } from '../lib/service.mjs';
import { importKey, seal, open, parseNote, formatNote, parseLink, b64u } from '../lib/codec.mjs';

const KEY = b64u.enc(new Uint8Array(32).fill(7));
const LINK = `https://example.invalid/#relay=gh&proj=acme/signal&issue=5&session=s1&k=${KEY}`;
const T0 = Date.parse('2026-11-05T00:00:00Z');
const tmp = () => mkdtempSync(join(tmpdir(), 'hdw-'));

function fakeGitHub(clock) {
  const gh = { comments: [], etagN: 0, writes: [], reads: [], deny: null, nextId: 100 };
  const etag = () => `"e${gh.etagN}"`;
  const hdr = (o) => new Map(Object.entries(o));
  gh.fetch = async (url, init = {}) => {
    const m = init.method || 'GET';
    if (m === 'GET') {
      gh.reads.push(init.headers['If-None-Match'] || null);
      if (init.headers['If-None-Match'] === etag()) return { status: 304, ok: false, headers: hdr({ 'x-ratelimit-remaining': '4999', 'x-ratelimit-limit': '5000' }) };
      const body = gh.comments.map((c) => ({ ...c }));
      return { status: 200, ok: true, headers: hdr({ etag: etag(), 'x-ratelimit-remaining': '4998', 'x-ratelimit-limit': '5000' }), json: async () => body };
    }
    if (gh.deny) { const d = gh.deny; gh.deny = null; return { status: d, ok: false, headers: hdr({ 'retry-after': '30' }) }; }
    gh.writes.push({ m, url, t: clock.now() });
    const { body } = JSON.parse(init.body);
    if (m === 'POST') { const c = { id: gh.nextId++, body, updated_at: new Date(clock.now()).toISOString() }; gh.comments.push(c); gh.etagN++; return { status: 201, ok: true, headers: hdr({}), json: async () => c }; }
    const id = Number(url.split('/').pop());
    const c = gh.comments.find((x) => x.id === id); c.body = body; c.updated_at = new Date(clock.now()).toISOString(); gh.etagN++;
    return { status: 200, ok: true, headers: hdr({}), json: async () => c };
  };
  gh.guest = async (from, asks, tag = 'wk-signal-relay') => { // a visitor page's own note
    const key = await importKey(KEY);
    const ring = [];
    for (const [i, a] of asks.entries()) ring.push({ seq: i + 1, ct: await seal(key, { kind: 'ask', data: a }) });
    const body = formatNote({ v: 1, role: 'guest', from, ack: 0, next: null, ring }, tag);
    const c = gh.comments.find((x) => x.from === from);
    if (c) c.body = body; else gh.comments.push({ id: gh.nextId++, from, body, updated_at: new Date(clock.now()).toISOString() });
    gh.etagN++;
  };
  gh.hostAnswers = async () => {
    const key = await importKey(KEY);
    const host = gh.comments.map((c) => ({ c, n: parseNote(c.body) })).find((x) => x.n?.env.role === 'host');
    if (!host) return { tag: null, answers: [] };
    return { tag: host.n.tag, env: host.n.env, answers: await Promise.all(host.n.env.ring.map((r) => open(key, r.ct))) };
  };
  return gh;
}

function setup({ clock = fakeClock(T0), gh = fakeGitHub(clock), ks = 'open' } = {}) {
  const ksPath = join(tmp(), 'KS'); writeFileSync(ksPath, `${ks}\n`);
  const wire = new GitHubIssueWire({ link: LINK, token: 't', clock, fetchFn: gh.fetch,
    poller: new EtagPoller({ fetchFn: gh.fetch, pacer: new ReadPacer({ clock }) }) });
  const calls = [];
  const svc = new HaikuService({ dir: join(tmp(), 'state'), wires: [wire], killSwitchPath: ksPath, clock, simulate: false,
    writePacer: new WritePacer({ clock }), ask: async (p) => { calls.push(p); return { ok: true, text: `ans:${p}`, first_byte_ms: 1, total_ms: 2 }; } });
  return { clock, gh, wire, svc, calls };
}

test('codec: link parse, seal/open round trip, tag dialects', async () => {
  const l = parseLink(LINK);
  assert.deepEqual([l.provider, l.proj, l.issue, l.session], ['gh', 'acme/signal', 5, 's1']);
  const key = await importKey(KEY);
  assert.deepEqual(await open(key, await seal(key, { kind: 'ask', data: { text: 'hi' } })), { kind: 'ask', data: { text: 'hi' } });
  const env = { v: 1, role: 'guest', from: 'abcd1234', ack: 0, next: null, ring: [] };
  assert.equal(parseNote(formatNote(env)).tag, 'wksig-relay');
  assert.equal(parseNote(formatNote(env, 'wk-signal-relay')).tag, 'wk-signal-relay');
  assert.equal(parseNote('hello\n{}'), null);
  assert.equal(parseNote('Wksig-relay\n' + JSON.stringify(env)), null);
});

test('wire: asks in both dialects -> one coalesced host write in wksig-relay', async () => {
  const { gh, svc, calls } = setup();
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }], 'wk-signal-relay');
  await gh.guest('guest002', [{ visitor: 'v2', turn: 1, text: 'two' }], 'wksig-relay');
  await svc.tick();
  assert.deepEqual(calls.sort(), ['one', 'two']);
  assert.equal(gh.writes.length, 1, 'two answers, one write');
  assert.equal(gh.writes[0].m, 'POST');
  const h = await gh.hostAnswers();
  assert.equal(h.tag, 'wksig-relay'); assert.equal(h.env.role, 'host'); assert.equal(h.env.from.length, 8);
  assert.deepEqual(h.answers.map((a) => [a.kind, a.data.to, a.data.turn, a.data.text]).sort(),
    [['answer', 'guest001', 1, 'ans:one'], ['answer', 'guest002', 1, 'ans:two']]);
  assert.equal(svc.status.writes_per_turn, 0.5);
  assert.equal(svc.status.wire_writes_today, 1); assert.equal(svc.status.requests_today, 2);
});

test('wire: unchanged channel polls with If-None-Match and gets 304; later writes PATCH the same note', async () => {
  const { gh, svc, calls } = setup();
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }]);
  await svc.tick();
  await svc.tick(); // our own write changed the etag once
  const before = gh.reads.length;
  await svc.tick();
  assert.equal(gh.reads.length, before + 1);
  assert.ok(gh.reads.at(-1), 'conditional GET'); assert.equal(calls.length, 1);
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }, { visitor: 'v1', turn: 2, text: 'again' }]);
  await svc.tick();
  assert.deepEqual(calls, ['one', 'again']);
  assert.deepEqual(gh.writes.map((w) => w.m), ['POST', 'PATCH']);
  assert.equal(gh.comments.filter((c) => parseNote(c.body)?.env.role === 'host').length, 1);
});

test('wire: a restarted run adopts its note and does not re-answer', async () => {
  const clock = fakeClock(T0); const gh = fakeGitHub(clock);
  const a = setup({ clock, gh });
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }]);
  await a.svc.tick();
  const b = setup({ clock, gh });
  await b.svc.tick();
  assert.deepEqual(b.calls, []);
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }, { visitor: 'v1', turn: 2, text: 'new' }]);
  await b.svc.tick();
  assert.deepEqual(b.calls, ['new']);
  assert.deepEqual(gh.writes.map((w) => w.m), ['POST', 'PATCH']);
});

test('wire: cold start ignores turns on a guest note untouched for 10 min', async () => {
  const clock = fakeClock(T0); const gh = fakeGitHub(clock);
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'old' }]);
  clock.advance(11 * 60_000);
  const { svc, calls } = setup({ clock, gh });
  await svc.tick();
  assert.deepEqual(calls, []);
});

test('wire: kill switch closed -> refusal answered, claude never called', async () => {
  const { gh, svc, calls } = setup({ ks: 'closed' });
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }]);
  await svc.tick();
  assert.deepEqual(calls, []);
  const h = await gh.hostAnswers();
  assert.equal(h.answers[0].data.status, 'refused'); assert.equal(h.answers[0].data.reason, 'kill_switch_closed');
});

test('wire: 403 Retry-After keeps the answer queued and backs the write pacer off', async () => {
  const { gh, svc, clock } = setup();
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }]);
  gh.deny = 403;
  await svc.tick();
  assert.equal(gh.writes.length, 0);
  await svc.tick();
  assert.equal(gh.writes.length, 1);
  assert.ok(gh.writes[0].t - T0 >= 30_000, 'waited for Retry-After');
  assert.equal((await gh.hostAnswers()).answers[0].data.text, 'ans:one');
});

test('wire: daily cap counts wire writes and stops writing at the cap', async () => {
  const { gh, svc, calls } = setup();
  svc.cap = svc.status.cap = 2;
  for (let t = 1; t <= 4; t++) {
    await gh.guest('guest001', Array.from({ length: t }, (_, i) => ({ visitor: `v${i}`, turn: i + 1, text: `q${i + 1}` })));
    await svc.tick();
  }
  assert.equal(gh.writes.length, 2);
  assert.equal(svc.status.state, 'capped');
  assert.deepEqual(calls, ['q1', 'q2']);
});

test('wire: cadence 100/200/400 after a signal, 500 ms for 60 s, then 1200 ms; no burst on low budget', async () => {
  const { gh, wire, clock } = setup();
  await gh.guest('guest001', [{ visitor: 'v1', turn: 1, text: 'one' }]);
  await wire.poll();
  assert.deepEqual([1, 2, 3, 4].map(() => wire.nextDelayMs()), [100, 200, 400, 500]);
  clock.advance(61_000);
  assert.equal(wire.nextDelayMs(), 1200);
  wire.poller.last = { remaining: 500, limit: 5000 };
  wire.signal();
  assert.equal(wire.nextDelayMs(), 500);
});
