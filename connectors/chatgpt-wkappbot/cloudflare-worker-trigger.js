export default {
  async fetch(request, env) {
    if (request.method !== 'POST') {
      return new Response('method_not_allowed', { status: 405 });
    }

    const body = await request.json().catch(() => ({}));

    const payload = {
      ref: env.GITHUB_REF || 'main',
      inputs: {
        minutes: String(body.minutes || '25'),
        query: String(body.query || '')
      }
    };

    const response = await fetch(
      `https://api.github.com/repos/${env.GITHUB_REPO}/actions/workflows/chatgpt-connector-ephemeral-relay.yml/dispatches`,
      {
        method: 'POST',
        headers: {
          'authorization': `Bearer ${env.GITHUB_TOKEN}`,
          'accept': 'application/vnd.github+json',
          'content-type': 'application/json'
        },
        body: JSON.stringify(payload)
      }
    );

    return new Response(JSON.stringify({
      ok: response.ok,
      status: response.status,
      payload
    }, null, 2), {
      status: response.ok ? 200 : 500,
      headers: {
        'content-type': 'application/json'
      }
    });
  }
};
