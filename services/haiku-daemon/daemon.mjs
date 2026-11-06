#!/usr/bin/env node
// Haiku service daemon for WKAppBot Code. Usage:
//   node daemon.mjs --once  --dir D --inbox I [--outbox O] [--origin internal|public]   one pass, local wire (CI)
//   node daemon.mjs --serve --dir D [--takeover]   run loop with lease + handoff (Actions runner)
// Environment (serve): GITHUB_TOKEN, GITHUB_REPOSITORY, STATUS_BRANCH (default svc/haiku-daemon), GITHUB_RUN_ID,
//   RUNNER_NAME, HANDOFF_WORKFLOW, KILL_SWITCH (default ./KILL_SWITCH), INTERNAL_INBOX (optional),
//   HAIKU_RELAY_LINK (signaler share link; unset = no public wire), HAIKU_WIRE_TOKEN (token for that repo),
//   HAIKU_SIM=0 (turn off simulated latency / 429), CLAUDE_BIN (fake binary for tests).
import { fileURLToPath } from 'node:url';
import { dirname, join, resolve } from 'node:path';
import { execFile } from 'node:child_process';
import { writeFileSync } from 'node:fs';
import { promisify } from 'node:util';
import { HaikuService } from './lib/service.mjs';
import { LocalDirWire } from './lib/wire.mjs';
import { GitHubIssueWire } from './lib/ghwire.mjs';
import { LocalPublisher, GitHubPublisher } from './lib/publisher.mjs';
import { ReadPacer } from './lib/pacer.mjs';
import { EtagPoller } from './lib/etag.mjs';
import { runDaemon } from './lib/runner.mjs';

const here = dirname(fileURLToPath(import.meta.url));
const args = process.argv.slice(2);
const flag = (n) => args.includes(n);
const opt = (n, d) => { const i = args.indexOf(n); return i >= 0 ? args[i + 1] : d; };
const log = (m) => console.log(`${new Date().toISOString()} ${m}`);
const gh = (token) => ({ Authorization: `Bearer ${token}`, Accept: 'application/vnd.github+json' });

const dir = resolve(opt('--dir', 'state'));
const killSwitchPath = resolve(process.env.KILL_SWITCH || join(here, 'KILL_SWITCH'));
const runId = process.env.GITHUB_RUN_ID || `local-${process.pid}`;
const runnerId = process.env.RUNNER_NAME || null;
const simulate = process.env.HAIKU_SIM !== '0';

if (flag('--once')) {
  const wire = new LocalDirWire({ inbox: resolve(opt('--inbox', 'inbox')), outbox: opt('--outbox') && resolve(opt('--outbox')),
    origin: opt('--origin', 'internal') });
  const pub = new LocalPublisher(dir);
  const svc = new HaikuService({ dir, wires: [wire], killSwitchPath, runId, runnerId, log, simulate,
    publishStatus: (s) => pub.write(s, { force: true }) });
  await svc.pull();
  await svc.drain();
  svc.status.state = 'stopped';
  await svc.flushStatus();
  console.log(JSON.stringify(svc.status.toJSON()));
} else if (flag('--serve')) {
  const token = process.env.GITHUB_TOKEN;
  const repo = process.env.GITHUB_REPOSITORY;
  const readPacer = new ReadPacer(); // shared by every read this run makes
  const poller = new EtagPoller({ pacer: readPacer, headers: gh(token) });
  const publisher = new GitHubPublisher({ repo, branch: process.env.STATUS_BRANCH || 'svc/haiku-daemon', token, poller });
  const wires = [];
  if (process.env.INTERNAL_INBOX) wires.push(new LocalDirWire({ inbox: resolve(process.env.INTERNAL_INBOX) }));
  if (process.env.HAIKU_RELAY_LINK) {
    const wt = process.env.HAIKU_WIRE_TOKEN;
    const wire = new GitHubIssueWire({ link: process.env.HAIKU_RELAY_LINK, token: wt, log,
      poller: new EtagPoller({ pacer: readPacer, headers: gh(wt) }) });
    await wire.init();
    await probe(wire, new EtagPoller({ pacer: readPacer, headers: gh(wt) }));
    wires.push(wire);
  } else log('[WIRE] HAIKU_RELAY_LINK is not set: no public wire this run');

  // The switch is re-read from main every 30 s (ETag, so unchanged = 304); flipping it on main reaches a live
  // run without a redeploy. Any read error writes `closed` (fail closed).
  const ksUrl = `https://api.github.com/repos/${repo}/contents/services/haiku-daemon/KILL_SWITCH?ref=main`;
  let ksAt = 0;
  const preTick = async () => {
    if (Date.now() - ksAt < 30_000) return;
    ksAt = Date.now();
    try {
      const r = await poller.get(ksUrl);
      writeFileSync(killSwitchPath, r.body ? Buffer.from(r.body.content, 'base64').toString('utf8') : 'closed\n');
    } catch (e) { log(`[KILL] refresh failed (${e.message}); closing`); writeFileSync(killSwitchPath, 'closed\n'); }
  };
  const svc = new HaikuService({ dir, wires, killSwitchPath, runId, runnerId, log, preTick, simulate });
  const wf = process.env.HANDOFF_WORKFLOW;
  const dispatchSuccessor = async () => {
    if (!wf) return;
    await promisify(execFile)('gh', ['workflow', 'run', wf, '--repo', repo, '--ref', process.env.GITHUB_REF_NAME || 'main',
      '-f', 'takeover=true']);
  };
  const end = await runDaemon({ service: svc, publisher, dispatchSuccessor, takeover: flag('--takeover'), log });
  log(`[END] ${end}`);
} else {
  console.error('usage: daemon.mjs --once|--serve [options]');
  process.exit(2);
}

// Measures, on the live channel, whether a conditional re-read of the comment list returns 304 and leaves
// x-ratelimit-remaining unchanged (a free poll). Logged only; the channel address is never printed.
// It uses its own poller, so the wire's ETag cache and unread asks are untouched.
async function probe(wire, p) {
  try {
    const url = `${wire.api}/${wire.cfg.issue}/comments?per_page=100`;
    await p.get(url);
    const a = p.last;
    await p.get(url);
    const b = p.last;
    log(`[PROBE] comment list: first=${a?.status} second=${b?.status} remaining ${a?.remaining}->${b?.remaining} ` +
      `(304 free: ${b?.status === 304 && a?.remaining === b?.remaining})`);
  } catch (e) { log(`[PROBE] failed: ${e.message}`); }
}
