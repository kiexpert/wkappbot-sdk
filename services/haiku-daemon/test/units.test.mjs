import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mkdtempSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { fakeClock } from '../lib/clock.mjs';
import { ReadPacer } from '../lib/pacer.mjs';
import { EtagPoller } from '../lib/etag.mjs';
import { VisitorLimiter } from '../lib/ratelimit.mjs';
import { readKillSwitch } from '../lib/killswitch.mjs';
import { normalize } from '../lib/wire.mjs';

const tmp = () => mkdtempSync(join(tmpdir(), 'hd-'));

test('etag: second GET sends If-None-Match and a 304 returns the cached body', async () => {
  const seen = [];
  const fetchFn = async (url, { headers }) => {
    seen.push(headers['If-None-Match'] || null);
    if (headers['If-None-Match'] === '"v1"') return { status: 304, ok: false, headers: new Map() };
    return { status: 200, ok: true, headers: new Map([['etag', '"v1"']]), json: async () => ({ a: 1 }) };
  };
  const c = fakeClock(0);
  const p = new EtagPoller({ fetchFn, pacer: new ReadPacer({ clock: c }) });
  const r1 = await p.get('u'); const r2 = await p.get('u');
  assert.deepEqual(seen, [null, '"v1"']);
  assert.equal(r1.changed, true); assert.equal(r2.status, 304); assert.equal(r2.changed, false);
  assert.deepEqual(r2.body, { a: 1 }); assert.equal(p.stats.notModified, 1);
});

test('visitor limiter: fixed window per visitor', () => {
  const c = fakeClock(0); const l = new VisitorLimiter({ clock: c, max: 3, windowMs: 600_000 });
  for (let i = 0; i < 3; i++) assert.equal(l.check('v1').ok, true);
  const no = l.check('v1');
  assert.equal(no.ok, false); assert.equal(no.retryAfterMs, 600_000);
  assert.equal(l.check('v2').ok, true, 'other visitors unaffected');
  c.advance(600_000);
  assert.equal(l.check('v1').ok, true, 'next window resets');
});

test('kill switch: fails closed', () => {
  const d = tmp(); const f = join(d, 'KS');
  assert.equal(readKillSwitch(join(d, 'missing')), 'closed');
  writeFileSync(f, ''); assert.equal(readKillSwitch(f), 'closed');
  writeFileSync(f, '# c\nopne\n'); assert.equal(readKillSwitch(f), 'closed');
  writeFileSync(f, '# c\n  OPEN  \n'); assert.equal(readKillSwitch(f), 'open');
});

test('kill switch: the committed file is closed', () => {
  assert.equal(readKillSwitch(new URL('../KILL_SWITCH', import.meta.url).pathname), 'closed');
});

test('normalize: both header dialects give the same request', () => {
  const a = normalize({ headers: { 'X-Request-Id': 'r1', 'X-Visitor-Id': 'v' }, body: { prompt: 'hi' } });
  const b = normalize({ id: 'r1', visitor: 'v', prompt: 'hi' });
  assert.deepEqual({ ...a, dialect: 0 }, { ...b, dialect: 0 });
  assert.equal(a.dialect, 'headers'); assert.equal(b.dialect, 'flat');
  assert.equal(normalize({ id: 'x' }), null);
  assert.equal(normalize('junk'), null);
  assert.equal(normalize({ id: 'r', prompt: 'x'.repeat(9000) }).prompt.length, 4000);
});
