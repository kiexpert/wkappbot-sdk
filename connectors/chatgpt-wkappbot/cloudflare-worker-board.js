export default {
  async fetch(request, env) {
    if (request.method !== 'POST') {
      return new Response('method_not_allowed', { status: 405 });
    }

    const body = await request.json().catch(() => ({}));
    const repo = env.GITHUB_REPO || 'kiexpert/wkappbot-sdk';
    const issue = body.issue || env.BOARD_ISSUE || '21';
    const text = String(body.body || '').trim();

    if (!text) {
      return new Response(JSON.stringify({ ok: false, error: 'empty_body' }), {
        status: 400,
        headers: { 'content-type': 'application/json' }
      });
    }

    const response = await fetch(`https://api.github.com/repos/${repo}/issues/${issue}/comments`, {
      method: 'POST',
      headers: {
        'authorization': `Bearer ${env.GITHUB_TOKEN}`,
        'accept': 'application/vnd.github+json',
        'content-type': 'application/json',
        'user-agent': 'wkappbot-board-worker'
      },
      body: JSON.stringify({ body: text })
    });

    const result = await response.json().catch(() => ({}));

    return new Response(JSON.stringify({
      ok: response.ok,
      status: response.status,
      result
    }, null, 2), {
      status: response.ok ? 200 : 500,
      headers: {
        'content-type': 'application/json',
        'access-control-allow-origin': '*'
      }
    });
  }
};
