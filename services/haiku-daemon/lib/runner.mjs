// The run loop around HaikuService: lease, periodic status, overlap handoff before the 6 h runner cap.
//
// Timeline of one run (defaults): t=0 claim lease -> serve -> t=handoffAfterMs (5h15m) dispatch a
// successor -> successor claims the lease -> this run drains its queue and exits as `handed_off`.
// If no successor shows up by maxRunMs (5h45m) the run stops anyway; the cron watchdog restarts the service.
import { Conflict } from './publisher.mjs';
import { realClock } from './clock.mjs';

export const DEFAULTS = {
  tickMs: 2000, statusEveryMs: 60_000, idleStatusEveryMs: 300_000, staleMs: 15 * 60_000,
  handoffAfterMs: (5 * 60 + 15) * 60_000, maxRunMs: (5 * 60 + 45) * 60_000,
};

export async function runDaemon({ service, publisher, dispatchSuccessor = async () => {}, clock = realClock,
  takeover = false, log = () => {}, opts = {} }) {
  const o = { ...DEFAULTS, ...opts };
  const me = service.status.run_id;
  const start = clock.now();

  // 1. Lease: do not start a second live owner unless we were dispatched as its successor.
  const cur = await publisher.read().catch(() => null);
  const fresh = cur && clock.now() - Date.parse(cur.updated_at) < o.staleMs && cur.state !== 'stopped'
    && cur.state !== 'handed_off';
  if (fresh && cur.run_id !== me && !takeover) { log(`[LEASE] run ${cur.run_id} is live; standing by`); return 'standby'; }
  await publisher.write(service.status.touch().toJSON(), { force: true });

  let lastStatus = clock.now(), lastAnswered = 0, dispatched = false;
  for (;;) {
    const elapsed = clock.now() - start;
    if (!dispatched && elapsed >= o.handoffAfterMs) {
      dispatched = true;
      service.status.handoff_at = new Date(clock.now()).toISOString();
      log('[HANDOFF] dispatching successor');
      await dispatchSuccessor().catch((e) => log(`[HANDOFF] dispatch failed: ${e.message}`));
    }
    if (elapsed >= o.maxRunMs) return finish('stopped');

    await service.tick();

    const changed = service.status.answered_count + service.status.refused_count !== lastAnswered;
    const due = clock.now() - lastStatus >= (changed ? o.statusEveryMs : o.idleStatusEveryMs);
    if (due || (dispatched && clock.now() - lastStatus >= o.statusEveryMs)) {
      try {
        if (dispatched) { // during overlap, look for the successor before writing
          const s = await publisher.read();
          if (s && s.run_id !== me) return handedOff();
        }
        await service.writePacer.acquire();
        await publisher.write(service.status.touch().toJSON());
      } catch (e) {
        if (!(e instanceof Conflict)) throw e;
        const s = await publisher.read();
        if (s && s.run_id !== me) return handedOff();
      }
      lastStatus = clock.now();
      lastAnswered = service.status.answered_count + service.status.refused_count;
    }
    await clock.sleep(o.tickMs);
  }

  async function handedOff() {
    log('[HANDOFF] successor holds the lease; draining');
    service.accepting = false;
    service.status.state = 'draining';
    await service.drain();
    service.status.state = 'handed_off';
    return 'handed_off';
  }
  async function finish(state) {
    service.status.state = state;
    await publisher.write(service.status.touch().toJSON()).catch(() => {});
    return state;
  }
}
