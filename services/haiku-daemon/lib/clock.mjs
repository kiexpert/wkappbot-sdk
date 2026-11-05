// Injectable clock. Every timing decision in the daemon goes through one of these,
// so the pacers and the handoff timer can be unit-tested with a fake clock.
export const realClock = {
  now: () => Date.now(),
  sleep: (ms) => new Promise((r) => setTimeout(r, Math.max(0, ms))),
};

// Fake clock for tests: sleep() advances virtual time instantly and records each wait.
export function fakeClock(start = 0) {
  const c = {
    t: start,
    sleeps: [],
    now: () => c.t,
    sleep: async (ms) => {
      const d = Math.max(0, ms);
      c.sleeps.push(d);
      c.t += d;
    },
    advance: (ms) => { c.t += ms; },
  };
  return c;
}
