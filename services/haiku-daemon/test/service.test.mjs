import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mkdtempSync, writeFileSync, readFileSync, readdirSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { fakeClock } from '../lib/clock.mjs';
import { HaikuService } from '../lib/service.mjs';
import { LocalDirWire } from '../lib/wire.mjs';
import { LocalPublisher } from '../lib/publisher.mjs';
import { askProvider, slotCommand, loadProvider } from '../lib/answer.mjs';
import { runDaemon } from '../lib/runner.mjs';

const FAKE = fileURLToPath(new URL('./fake-claude.sh', import.meta.url));
const tmp = () => mkdtempSync(join(tmpdir(), 'hd-'));
const ks = (state) => { const f = join(tmp(), 'KS'); writeFileSync(f, `${state}\n`); return f; };
const drop = (dir, obj) => writeFileSync(join(dir, `${obj.id ?? obj.headers?.['X-Request-Id']}.json`), JSON.stringify(obj));

function setup({ killSwitch = 'closed', clock = fakeClock(Date.parse('2026-11-05T00:00:00Z')), ask } = {}) {
  const root = tmp();
  const pub = new LocalDirWire({ inbox: join(root, 'pub-in'), outbox: join(root, 'pub-out'), origin: 'public' });
  const int = new LocalDirWire({ inbox: join(root, 'int-in'), outbox: join(root, 'int-out'), origin: 'internal' });
  const svc = new HaikuService({ dir: join(root, 'state'), wires: [pub, int], killSwitchPath: ks(killSwitch), clock,
    ask: ask || ((p) => askProvider(p, { env: { ...process.env, CLAUDE_BIN: FAKE } })), runId: 'run-A', simulate: false });
  return { root, pub, int, svc, clock };
}
const out = (dir, id) => JSON.parse(readFileSync(join(dir, `${id}.json`), 'utf8'));

test('fake claude: provider slot runs haiku, text output, every tool disabled, token cap', async () => {
  const argsFile = join(tmp(), 'argv');
  const r = await askProvider('ping', { env: { ...process.env, CLAUDE_BIN: FAKE, FAKE_CLAUDE_ARGS: argsFile } });
  assert.equal(r.ok, true); assert.equal(r.text, 'fake-haiku: ping');
  assert.ok(r.first_byte_ms >= 0 && r.first_byte_ms <= r.total_ms);
  assert.match(readFileSync(argsFile, 'utf8'), /-p --model haiku --tools {2}--output-format text/);
  const cmd = slotCommand(loadProvider(), {});
  assert.equal(cmd.bin, 'claude');
  assert.equal(cmd.env.CLAUDE_CODE_MAX_OUTPUT_TOKENS, '600');
  assert.match(cmd.args[cmd.args.indexOf('--system-prompt') + 1], /Korean in-house assistant/);
});

test('provider slot: another family is a config change, not a code change', () => {
  const cfg = { ...loadProvider(), slot: { family: 'agy', model: 'flash-lite' },
    families: { agy: { bin: 'agy', args: ['--model', '{model}'], env: {} } } };
  assert.deepEqual(slotCommand(cfg, {}).args, ['--model', 'flash-lite']);
  assert.throws(() => slotCommand({ ...cfg, slot: { family: 'nope', model: 'x' } }, {}));
});

test('fake claude: a failing binary becomes an error answer, not a crash', async () => {
  const r = await askProvider('x', { env: { ...process.env, CLAUDE_BIN: FAKE, FAKE_CLAUDE_FAIL: '1' } });
  assert.equal(r.ok, false); assert.equal(r.error, 'exit 1');
});

test('simulation: occasional 429 and in-house latency', async () => {
  const clock = fakeClock(0);
  const { root, svc } = setup({ killSwitch: 'open', clock, ask: async () => ({ ok: true, text: 'x', first_byte_ms: 5, total_ms: 10 }) });
  svc.simulate = true; svc.cfg = { ...svc.cfg, simulate: { latency_ms: [300, 900], rate_429: 0.5 } };
  const rolls = [0.9, 0.5, 0.1]; svc.rng = () => rolls.shift();
  drop(join(root, 'pub-in'), { id: 'a', visitor: 'v1', prompt: 'q' });
  drop(join(root, 'pub-in'), { id: 'b', visitor: 'v2', prompt: 'q' });
  await svc.tick();
  const st = ['a', 'b'].map((id) => out(join(root, 'pub-out'), id));
  assert.equal(st.filter((x) => x.reason === 'simulated_429').length, 1);
  const ok = st.find((x) => x.status === 'ok');
  assert.equal(ok.latency_ms, 300 + Math.floor(0.5 * 600)); // rolls: a passes 429 (0.9), sleeps 0.5; b gets 429 (0.1)
});

test('kill switch closed: public request refused, internal answered, one object each', async () => {
  const { root, svc } = setup();
  drop(join(root, 'pub-in'), { id: 'p1', visitor: 'v1', prompt: 'hello' });
  drop(join(root, 'int-in'), { headers: { 'X-Request-Id': 'i1', 'X-Visitor-Id': 'ci' }, body: { prompt: 'pong?' } });
  await svc.tick();
  assert.equal(out(join(root, 'pub-out'), 'p1').status, 'refused');
  const i1 = out(join(root, 'int-out'), 'i1');
  assert.equal(i1.status, 'ok'); assert.equal(i1.text, 'fake-haiku: pong?'); assert.equal(i1.dialect, 'headers');
  assert.deepEqual(readdirSync(join(root, 'state', 'ans')).sort(), ['i1.json', 'p1.json']);
  assert.deepEqual(readdirSync(join(root, 'state', 'req')), [], 'answered requests are dropped');
  const s = svc.status.toJSON();
  assert.equal(s.kill_switch, 'closed'); assert.equal(s.answered_count, 1); assert.equal(s.refused_count, 1);
  assert.equal(s.queue, 0); assert.ok(s.last_answer_at);
  assert.ok(!JSON.stringify(s).includes('v1'), 'status never carries a visitor id');
});

test('kill switch open: per-visitor limit applies to public requests', async () => {
  const { root, svc } = setup({ killSwitch: 'open' });
  for (let i = 0; i < 5; i++) drop(join(root, 'pub-in'), { id: `p${i}`, visitor: 'v1', prompt: `q${i}`, });
  drop(join(root, 'pub-in'), { id: 'other', visitor: 'v2', prompt: 'q' });
  await svc.tick();
  const st = [0, 1, 2, 3, 4].map((i) => out(join(root, 'pub-out'), `p${i}`).status);
  assert.equal(st.filter((x) => x === 'ok').length, 3);
  assert.equal(st.filter((x) => x === 'rate_limited').length, 2);
  assert.equal(out(join(root, 'pub-out'), 'other').status, 'ok');
});

test('duplicate request id is answered once', async () => {
  const { root, svc } = setup();
  drop(join(root, 'int-in'), { id: 'd1', prompt: 'a' });
  await svc.tick();
  drop(join(root, 'int-in'), { id: 'd1', prompt: 'a' });
  let calls = 0; svc.ask = async () => { calls++; return { ok: true, text: 'x' }; };
  await svc.tick();
  assert.equal(calls, 0);
});

test('handoff: dispatch at handoffAfter, successor takes the lease, predecessor drains out', async () => {
  const clock = fakeClock(Date.parse('2026-11-05T00:00:00Z'));
  const { root, svc } = setup({ clock, ask: async () => ({ ok: true, text: 'x' }) });
  const pubA = new LocalPublisher(root);
  let dispatchedAt = null;
  const opts = { tickMs: 1000, statusEveryMs: 10_000, idleStatusEveryMs: 10_000, handoffAfterMs: 60_000, maxRunMs: 600_000 };
  const successor = async () => {
    dispatchedAt = clock.now();
    const pubB = new LocalPublisher(root);
    const svcB = setup({ clock }).svc; svcB.status.run_id = 'run-B';
    await pubB.read();
    await pubB.write(svcB.status.touch().toJSON(), { force: true }); // B claims the lease
  };
  const end = await runDaemon({ service: svc, publisher: pubA, dispatchSuccessor: successor, clock, opts });
  assert.equal(end, 'handed_off');
  assert.equal(dispatchedAt - Date.parse('2026-11-05T00:00:00Z'), 60_000);
  assert.equal(svc.status.state, 'handed_off');
  assert.equal(JSON.parse(readFileSync(join(root, 'status.json'), 'utf8')).run_id, 'run-B');
});

test('lease: a second run without --takeover stands by while the first is live', async () => {
  const clock = fakeClock(Date.parse('2026-11-05T00:00:00Z'));
  const { root } = setup({ clock });
  const live = setup({ clock }).svc; live.status.state = 'running';
  await new LocalPublisher(root).write(live.status.touch().toJSON(), { force: true });
  const second = setup({ clock }).svc; second.status.run_id = 'run-C';
  assert.equal(await runDaemon({ service: second, publisher: new LocalPublisher(root), clock }), 'standby');
});

test('no successor: run stops at maxRunMs with state stopped', async () => {
  const clock = fakeClock(Date.parse('2026-11-05T00:00:00Z'));
  const { root, svc } = setup({ clock });
  const opts = { tickMs: 5000, handoffAfterMs: 20_000, maxRunMs: 40_000 };
  assert.equal(await runDaemon({ service: svc, publisher: new LocalPublisher(root), clock, opts }), 'stopped');
  assert.equal(JSON.parse(readFileSync(join(root, 'status.json'), 'utf8')).state, 'stopped');
});
