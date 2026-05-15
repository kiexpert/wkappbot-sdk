export default {
  async fetch(request, env) {
    if (request.method === 'OPTIONS') {
      return cors(new Response(null, { status: 204 }));
    }

    if (request.method !== 'POST') {
      return cors(new Response('method_not_allowed', { status: 405 }));
    }

    const body = await request.json().catch(() => ({}));

    if (body.website) {
      return cors(json({ ok: false, error: 'bot_rejected' }, 400));
    }

    if (env.BOARD_SECRET && body.secret !== env.BOARD_SECRET) {
      return cors(json({ ok: false, error: 'bad_secret' }, 403));
    }

    const repo = env.GITHUB_REPO || 'kiexpert/wkappbot-sdk';
    const issue = body.issue || env.BOARD_ISSUE || '21';
    const text = String(body.body || '').trim();
    const guest = String(body.guestName || 'Guest').trim().slice(0, 80);

    if (!text) return cors(json({ ok: false, error: 'empty_body' }, 400));
    if (text.length > 4000) return cors(json({ ok: false, error: 'body_too_long' }, 400));

    const commentBody = `**Guest:** ${guest}\n\n${text}`;

    const response = await fetch(`https://api.github.com/repos/${repo}/issues/${issue}/comments`, {
      method: 'POST',
      headers: {
        'authorization': `Bearer ${env.GITHUB_TOKEN}`,
        'accept': 'application/vnd.github+json',
        'content-type': 'application/json',
        'user-agent': 'wkappbot-board-worker'
      },
      body: JSON.stringify({ body: commentBody })
    });

    const result = await response.json().catch(() => ({}));
    return cors(json({ ok: response.ok, status: response.status, result }, response.ok ? 200 : 500));
  }
};

function json(value, status) {
  return new Response(JSON.stringify(value, null, 2), {
    status,
    headers: { 'content-type': 'application/json' }
  });
}

function cors(response) {
  response.headers.set('access-control-allow-origin', '*');
  response.headers.set('access-control-allow-methods', 'POST, OPTIONS');
  response.headers.set('access-control-allow-headers', 'content-type');
  return response;
}
