// Where status.json lives. It doubles as the run lease: { run_id, updated_at } says which run owns the
// service and that it is alive. A successor takes over by writing its own run_id; the predecessor's next
// conditional write then fails (sha changed), it re-reads, sees the new owner and drains out.
import { readFileSync, existsSync } from 'node:fs';
import { join } from 'node:path';
import { writeJsonAtomic } from './store.mjs';

export class Conflict extends Error {}

export class LocalPublisher {
  constructor(dir) { this.path = join(dir, 'status.json'); this.ver = 0; this.seen = 0; }
  async read() {
    if (!existsSync(this.path)) return null;
    const o = JSON.parse(readFileSync(this.path, 'utf8'));
    this.seen = o._v || 0;
    return o;
  }
  async write(obj, { force = false } = {}) {
    const cur = existsSync(this.path) ? JSON.parse(readFileSync(this.path, 'utf8'))._v || 0 : 0;
    if (!force && cur !== this.seen) throw new Conflict('status changed under us');
    this.seen = cur + 1;
    writeJsonAtomic(this.path, { ...obj, _v: this.seen });
  }
}

// GitHub contents API on one branch of this repo. Reads use ETag (via EtagPoller), writes are
// conditional on the last sha we saw and go through the caller's write pacer.
export class GitHubPublisher {
  constructor({ repo, branch, path = 'status.json', token, poller, fetchFn = globalThis.fetch }) {
    Object.assign(this, { repo, branch, path, token, poller, fetchFn });
    this.url = `https://api.github.com/repos/${repo}/contents/${path}`;
    this.sha = null;
  }
  async read() {
    const r = await this.poller.get(`${this.url}?ref=${encodeURIComponent(this.branch)}`);
    if (!r.body) { this.sha = null; return null; }
    this.sha = r.body.sha;
    return JSON.parse(Buffer.from(r.body.content, 'base64').toString('utf8'));
  }
  async write(obj) {
    const body = { message: `status: ${obj.state} ${obj.run_id ?? ''}`.trim(), branch: this.branch,
      content: Buffer.from(JSON.stringify(obj, null, 2) + '\n').toString('base64') };
    if (this.sha) body.sha = this.sha;
    const res = await this.fetchFn(this.url, { method: 'PUT', body: JSON.stringify(body),
      headers: { Authorization: `Bearer ${this.token}`, Accept: 'application/vnd.github+json' } });
    if (res.status === 409 || res.status === 422) throw new Conflict(`status write ${res.status}`);
    if (!res.ok) throw new Error(`status write ${res.status}`);
    this.sha = (await res.json()).content?.sha ?? null;
  }
}
