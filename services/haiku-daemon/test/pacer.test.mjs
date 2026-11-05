import { test } from 'node:test';
import assert from 'node:assert/strict';
import { fakeClock } from '../lib/clock.mjs';
import { ReadPacer, WritePacer } from '../lib/pacer.mjs';

test('read pacer: 4 calls in a second go out with no wait', async () => {
  const c = fakeClock(1000); const p = new ReadPacer({ clock: c });
  for (let i = 0; i < 4; i++) await p.acquire();
  assert.equal(c.now(), 1000);
  assert.deepEqual(c.sleeps, []);
});

test('read pacer: past 4/s every call keeps a 500 ms gap', async () => {
  const c = fakeClock(0); const p = new ReadPacer({ clock: c });
  const t = [];
  for (let i = 0; i < 8; i++) t.push(await p.acquire());
  assert.deepEqual(t.slice(0, 4), [0, 0, 0, 0]);
  for (let i = 4; i < t.length; i++) assert.ok(t[i] - t[i - 1] >= 500, `gap ${i}: ${t[i] - t[i - 1]}`);
  assert.equal(t[4], 500);
});

test('read pacer: after the window drains, bursts are free again', async () => {
  const c = fakeClock(0); const p = new ReadPacer({ clock: c });
  for (let i = 0; i < 6; i++) await p.acquire();
  c.advance(5000);
  const before = c.sleeps.length;
  for (let i = 0; i < 4; i++) await p.acquire();
  assert.equal(c.sleeps.length, before);
});

test('read pacer: slow callers (2/s) are never delayed', async () => {
  const c = fakeClock(0); const p = new ReadPacer({ clock: c });
  for (let i = 0; i < 20; i++) { await p.acquire(); c.advance(500); }
  assert.deepEqual(c.sleeps, []);
});

test('write pacer: never more than perMinute writes in any 60 s window', async () => {
  const c = fakeClock(0); const p = new WritePacer({ clock: c, perMinute: 60 });
  const t = [];
  for (let i = 0; i < 200; i++) t.push(await p.acquire());
  for (let i = 60; i < t.length; i++) assert.ok(t[i] - t[i - 60] >= 60_000, `window at ${i}`);
  assert.equal(t[59], 0); assert.equal(t[60], 60_000);
});

test('write pacer: refuses a limit at or over 80/min', () => {
  assert.throws(() => new WritePacer({ perMinute: 80 }));
});

test('read pacer: a sustained hammer settles at 2 per second', async () => {
  const c = fakeClock(0); const p = new ReadPacer({ clock: c });
  for (let i = 0; i < 44; i++) await p.acquire();
  assert.equal(c.now(), 20_000); // 4 free, then 40 calls 500 ms apart
});
