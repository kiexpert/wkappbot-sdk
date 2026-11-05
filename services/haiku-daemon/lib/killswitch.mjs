// The kill switch is a file. Its first non-comment word is `closed` or `open`.
// Anything else -- missing file, unreadable, empty, a typo -- reads as CLOSED (fail closed).
// Closed means: no public request reaches claude. Internal requests (from the runner's own local
// inbox, never from the wire) are still answered, so CI can prove the path without opening it.
import { readFileSync } from 'node:fs';

export function readKillSwitch(path) {
  let text = '';
  try { text = readFileSync(path, 'utf8'); } catch { return 'closed'; }
  for (const raw of text.split(/\r?\n/)) {
    const line = raw.trim();
    if (!line || line.startsWith('#')) continue;
    return line.split(/\s+/)[0].toLowerCase() === 'open' ? 'open' : 'closed';
  }
  return 'closed';
}

export function allowedBySwitch(state, origin) {
  return origin === 'internal' || state === 'open';
}
