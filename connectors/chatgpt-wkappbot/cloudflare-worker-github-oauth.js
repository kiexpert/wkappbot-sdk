export default {
  async fetch(request, env) {
    const url = new URL(request.url);

    if (url.pathname === '/login') {
      const state = crypto.randomUUID();
      const redirect = new URL('https://github.com/login/oauth/authorize');
      redirect.searchParams.set('client_id', env.GITHUB_CLIENT_ID);
      redirect.searchParams.set('redirect_uri', env.OAUTH_CALLBACK_URL || `${url.origin}/callback`);
      redirect.searchParams.set('scope', 'read:user');
      redirect.searchParams.set('state', state);
      return new Response(null, {
        status: 302,
        headers: {
          location: redirect.toString(),
          'set-cookie': `wk_oauth_state=${state}; HttpOnly; Secure; SameSite=Lax; Path=/; Max-Age=600`
        }
      });
    }

    if (url.pathname === '/callback') {
      const code = url.searchParams.get('code');
      if (!code) return text('missing_code', 400);

      const tokenRes = await fetch('https://github.com/login/oauth/access_token', {
        method: 'POST',
        headers: {
          accept: 'application/json',
          'content-type': 'application/json'
        },
        body: JSON.stringify({
          client_id: env.GITHUB_CLIENT_ID,
          client_secret: env.GITHUB_CLIENT_SECRET,
          code,
          redirect_uri: env.OAUTH_CALLBACK_URL || `${url.origin}/callback`
        })
      });

      const token = await tokenRes.json();
      if (!token.access_token) return text('token_exchange_failed', 500);

      const meRes = await fetch('https://api.github.com/user', {
        headers: {
          authorization: `Bearer ${token.access_token}`,
          accept: 'application/vnd.github+json',
          'user-agent': 'wkappbot-board-oauth'
        }
      });
      const me = await meRes.json();
      const login = me.login || 'github-user';

      const target = new URL(env.BOARD_URL || 'https://kiexpert.github.io/wkappbot-sdk/board.html');
      target.searchParams.set('user', login);
      return new Response(null, {
        status: 302,
        headers: {
          location: target.toString(),
          'set-cookie': `wk_github_login=${encodeURIComponent(login)}; Secure; SameSite=Lax; Path=/; Max-Age=2592000`
        }
      });
    }

    if (url.pathname === '/me') {
      const cookie = request.headers.get('cookie') || '';
      const match = cookie.match(/(?:^|; )wk_github_login=([^;]+)/);
      return json({ login: match ? decodeURIComponent(match[1]) : null });
    }

    return text('not_found', 404);
  }
};

function json(value, status = 200) {
  return new Response(JSON.stringify(value, null, 2), {
    status,
    headers: {
      'content-type': 'application/json',
      'access-control-allow-origin': '*'
    }
  });
}

function text(value, status = 200) {
  return new Response(value, { status });
}
