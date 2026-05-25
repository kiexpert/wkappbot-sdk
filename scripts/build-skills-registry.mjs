import { promises as fs } from 'node:fs';
import path from 'node:path';

const roots = [
  'skills',
  'bin/wkappbot.hq/skills'
];

const outFile = 'docs/skills.json';
const repo = process.env.SKILL_REPO || 'kiexpert/wkappbot-sdk';

async function exists(p) {
  try { await fs.stat(p); return true; } catch { return false; }
}

async function walk(dir) {
  const out = [];
  if (!(await exists(dir))) return out;
  for (const ent of await fs.readdir(dir, { withFileTypes: true })) {
    const full = path.join(dir, ent.name);
    if (ent.isDirectory()) out.push(...await walk(full));
    else if (ent.isFile() && ent.name.endsWith('.skill.json')) out.push(full);
  }
  return out;
}

function asArray(v) {
  if (!v) return [];
  return Array.isArray(v) ? v : [v];
}

function pick(obj, names, fallback = '') {
  for (const n of names) if (obj && obj[n]) return obj[n];
  return fallback;
}

function normalizeSkill(file, raw, sourceRoot) {
  const id = pick(raw, ['id', 'name', 'skill', 'slug'], path.basename(file, '.skill.json'));
  const title = pick(raw, ['title', 'displayName', 'name'], id);
  const desc = pick(raw, ['description', 'desc', 'summary'], '');
  const tags = asArray(pick(raw, ['tags', 'categories'], []));
  const command = pick(raw, ['command', 'cmd', 'run'], `wkappbot skill run ${id}`);
  const status = sourceRoot.includes('bin/wkappbot.hq') ? 'installed' : 'public';
  const sourcePath = file.replaceAll(path.sep, '/');

  return {
    id,
    title,
    desc,
    tags,
    repo,
    command,
    status,
    sourcePath,
    source: `https://github.com/${repo}/blob/main/${sourcePath}`,
    board: `board/?repo=${encodeURIComponent(repo)}&title=${encodeURIComponent(title)}&label=daily`
  };
}

async function main() {
  const skills = [];
  for (const root of roots) {
    const files = await walk(root);
    for (const file of files) {
      try {
        const content = await fs.readFile(file, 'utf8');
        const raw = JSON.parse(content.replace(/^﻿/, ''));  // strip UTF-8 BOM
        skills.push(normalizeSkill(file, raw, root));
      } catch (err) {
        console.warn(`skip ${file}: ${err.message}`);
      }
    }
  }

  const unique = new Map();
  for (const s of skills) unique.set(`${s.status}:${s.id}`, s);
  const result = [...unique.values()].sort((a, b) => a.title.localeCompare(b.title));

  await fs.mkdir(path.dirname(outFile), { recursive: true });
  await fs.writeFile(outFile, JSON.stringify(result, null, 2) + '\n');
  console.log(`wrote ${outFile} with ${result.length} skill(s)`);
}

main().catch(err => {
  console.error(err);
  process.exit(1);
});
