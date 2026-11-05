// File drop: one JSON object per request (req/<id>.json) and one per answer (ans/<id>.json).
// Writes are atomic (temp file + rename) so a reader never sees half an object.
import { mkdirSync, writeFileSync, renameSync, readFileSync, readdirSync, existsSync, unlinkSync } from 'node:fs';
import { join } from 'node:path';

const ID_RE = /^[A-Za-z0-9_-]{1,80}$/;
export const validId = (id) => typeof id === 'string' && ID_RE.test(id);

export function writeJsonAtomic(path, obj) {
  const tmp = `${path}.tmp-${process.pid}`;
  writeFileSync(tmp, JSON.stringify(obj, null, 2) + '\n');
  renameSync(tmp, path);
}

export class DropStore {
  constructor(dir) {
    this.dir = dir;
    this.reqDir = join(dir, 'req');
    this.ansDir = join(dir, 'ans');
    mkdirSync(this.reqDir, { recursive: true });
    mkdirSync(this.ansDir, { recursive: true });
  }
  putRequest(req) {
    if (!validId(req.id)) throw new Error(`bad request id: ${req.id}`);
    const p = join(this.reqDir, `${req.id}.json`);
    if (existsSync(p) || this.hasAnswer(req.id)) return false; // already seen
    writeJsonAtomic(p, req);
    return true;
  }
  pending() {
    return readdirSync(this.reqDir)
      .filter((f) => f.endsWith('.json'))
      .map((f) => JSON.parse(readFileSync(join(this.reqDir, f), 'utf8')))
      .filter((r) => !this.hasAnswer(r.id))
      .sort((a, b) => (a.received_at || 0) - (b.received_at || 0));
  }
  hasAnswer(id) { return existsSync(join(this.ansDir, `${id}.json`)); }
  putAnswer(ans) {
    if (!validId(ans.id)) throw new Error(`bad answer id: ${ans.id}`);
    writeJsonAtomic(join(this.ansDir, `${ans.id}.json`), ans);
  }
  readAnswer(id) { return JSON.parse(readFileSync(join(this.ansDir, `${id}.json`), 'utf8')); }
  dropRequest(id) { try { unlinkSync(join(this.reqDir, `${id}.json`)); } catch {} }
}
