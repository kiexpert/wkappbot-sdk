#!/usr/bin/env node
// Haiku service daemon for AppBot Code. Usage:
//   node daemon.mjs --once  --dir D --inbox I [--outbox O]   one pull+answer pass, local wire (CI self-test)
//   node daemon.mjs --serve --dir D [--takeover]             run loop with lease + handoff (Actions runner)
// Environment (serve): GITHUB_TOKEN, GITHUB_REPOSITORY, STATUS_BRANCH (default svc/haiku-daemon),
//   GITHUB_RUN_ID, HANDOFF_WORKFLOW (file name to dispatch), KILL_SWITCH (path, default ./KILL_SWITCH),
//   INTERNAL_INBOX (optional local dir for internal requests), CLAUDE_BIN (default claude).
import { fileURLToPath } from 'node:url';
import { dirname, join, resolve } from 'node:path';
import { execFile } from 'node:child_process';
import { writeFileSync } from 'node:fs';
import { promisify } from 'node:util';
import { HaikuService } from './lib/service.mjs';
import { LocalDirWire } from './lib/wire.mjs';
import { LocalPublisher, GitHubPublisher } from './lib/publisher.mjs';
import { ReadPacer } from './lib/pacer.mjs';
import { EtagPoller } from './lib/etag.mjs';
import { runDaemon } from './lib/runner.mjs';

const here = dirname(fileURLToPath(import.meta.url));
const args = process.argv.slice(2);
const flag = (n) => args.includes(n);
const opt = (n, d) => { const i = args.indexOf(n); return i >= 0 ? args[i + 1] : d; };
const log = (m) => console.log(`${new Date().toISOString()} ${m}`);

const dir = resolve(opt('--dir', 'state'));
const killSwitchPath = resolve(process.env.KILL_SWITCH || join(here, 'KILL_SWITCH'));
const runId = process.env.GITHUB_RUN_ID || `local-${process.pid}`;

if (flag('--once')) {
  const wire = new LocalDirWire({ inbox: resolve(opt('--inbox', 'inbox')), outbox: opt('--outbox') && resolve(opt('--outbox')),
    origin: opt('--origin', 'internal') });
  const pub = new LocalPublisher(dir);
  const svc = new HaikuService({ dir, wires: [wire], killSwitchPath, runId, log,
    publishStatus: (s) => pub.write(s, { force: true }) });
  await svc.pull();
  await svc.drain();
  svc.status.state = 'stopped';
  await svc.flushStatus();
  console.log(JSON.stringify(svc.status.toJSON()));
} else if (flag('--serve')) {
  const token = process.env.GITHUB_TOKEN;
  const repo = process.env.GITHUB_REPOSITORY;
  const poller = new EtagPoller({ pacer: new ReadPacer(),
    headers: { Authorization: `Bearer ${token}`, Accept: 'application/vnd.github+json' } });
  const publisher = new GitHubPublisher({ repo, branch: process.env.STATUS_BRANCH || 'svc/haiku-daemon', token, poller });
  const wires = [];
  if (process.env.INTERNAL_INBOX) wires.push(new LocalDirWire({ inbox: resolve(process.env.INTERNAL_INBOX) }));
  // The signaler-wire adapter (relay.js dialects) is registered here once its transport is wired in.
  // The switch is re-read from main every 30 s (ETag, so unchanged = 304), so flipping it on main reaches
  // a live run without a redeploy. Any read error writes `closed` (fail closed).
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
  const svc = new HaikuService({ dir, wires, killSwitchPath, runId, log, preTick });
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
