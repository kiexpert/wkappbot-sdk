export default {
  async fetch(request, env) {
    if (request.method === 'OPTIONS') return cors(new Response(null, { status: 204 }));
    if (request.method !== 'POST') return cors(json({ ok: false, error: 'method_not_allowed' }, 405));

    const body = await request.json().catch(() => ({}));
    const repo = String(body.repo || env.GITHUB_REPO || '').trim();
    const label = String(body.label || 'daily').trim();
    const titlePrefix = String(body.title || 'Daily board').trim();
    const day = String(body.day || new Date().toISOString().slice(0, 10));

    if (!repo || !repo.includes('/')) return cors(json({ ok: false, error: 'bad_repo' }, 400));

    const [owner, name] = repo.split('/');
    const title = `${titlePrefix} - ${day}`;

    const search = await fetch(`https://api.github.com/search/issues?q=${encodeURIComponent(`repo:${repo} is:issue in:title "${title}"`)}`, {
      headers: githubHeaders(env)
    });
    const found = await search.json().catch(() => ({}));
    if (found.items && found.items[0]) {
      return cors(json({ ok: true, created: false, issue: found.items[0] }));
    }

    const created = await fetch(`https://api.github.com/repos/${owner}/${name}/issues`, {
      method: 'POST',
      headers: githubHeaders(env),
      body: JSON.stringify({
        title,
        labels: [label],
        body: `# ${title}\n\n## Smoke Test\n\nWaiting for automated smoke result...\n\n## Notes\n\nUse comments as replies.`
      })
    });

    const issue = await created.json().catch(() => ({}));
    return cors(json({ ok: created.ok, created: created.ok, status: created.status, issue }, created.ok ? 200 : 500));
  }
};

function githubHeaders(env) {
  return {
    authorization: `Bearer ${env.GITHUB_TOKEN}`,
    accept: 'application/vnd.github+json',
    'content-type': 'application/json',
    'user-agent': 'wkappbot-daily-issue-worker'
  };
}

function json(value, status = 200) {
  return new Response(JSON.stringify(value, null, 2), { status, headers: { 'content-type': 'application/json' } });
}

function cors(response) {
  response.headers.set('access-control-allow-origin', '*');
  response.headers.set('access-control-allow-methods', 'POST, OPTIONS');
  response.headers.set('access-control-allow-headers', 'content-type');
  return response;
}
