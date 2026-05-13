const fs = require('fs');
const path = require('path');
const http = require('http');
const {
  createJob,
  getJob,
  nextJob,
  completeJob,
  listJobs,
  heartbeat,
  listAgents
} = require('./store');
const {
  createSession,
  appendSession,
  getSession,
  listSessions
} = require('./session');
const { isAllowed } = require('./auth');
const { addClient, removeClient } = require('./stream');
const { snapshot } = require('./metrics');

const port = Number(process.env.PORT || 8787);
const maxBodyBytes = Number(process.env.WK_MAX_BODY_BYTES || 1024 * 1024);

function send(res, code, body) {
  res.writeHead(code, { 'content-type': 'application/json' });
  res.end(JSON.stringify(body));
}

function sendFile(res, filePath, type) {
  fs.readFile(filePath, (err, data) => {
    if (err) return send(res, 404, { error: 'not_found' });
    res.writeHead(200, { 'content-type': type });
    res.end(data);
  });
}

function readJson(req, done) {
  let body = '';
  let tooLarge = false;

  req.on('data', chunk => {
    body += chunk;
    if (Buffer.byteLength(body) > maxBodyBytes) {
      tooLarge = true;
      req.destroy();
    }
  });

  req.on('end', () => {
    if (tooLarge) return done(new Error('body_too_large'));
    try {
      done(null, body ? JSON.parse(body) : {});
    } catch (err) {
      done(err);
    }
  });

  req.on('error', err => done(err));
}

const server = http.createServer((req, res) => {
  if (!isAllowed(req)) return send(res, 401, { error: 'unauthorized' });

  if (req.method === 'GET' && (req.url === '/' || req.url === '/dashboard')) {
    return sendFile(res, path.join(__dirname, '..', 'dashboard.html'), 'text/html; charset=utf-8');
  }

  if (req.method === 'GET' && req.url === '/events') {
    addClient(res);
    req.on('close', () => removeClient(res));
    return;
  }

  if (req.method === 'GET' && req.url === '/health') {
    return send(res, 200, { ok: true, service: 'wkappbot-chatgpt-relay' });
  }

  if (req.method === 'GET' && req.url === '/metrics') {
    return send(res, 200, snapshot());
  }

  if (req.method === 'GET' && req.url === '/jobs') {
    return send(res, 200, { jobs: listJobs() });
  }

  if (req.method === 'GET' && req.url === '/agents') {
    return send(res, 200, { agents: listAgents() });
  }

  if (req.method === 'GET' && req.url === '/sessions') {
    return send(res, 200, { sessions: listSessions() });
  }

  if (req.method === 'POST' && req.url === '/sessions') {
    return readJson(req, (err, parsed) => {
      if (err) return send(res, 400, { error: err.message === 'body_too_large' ? 'body_too_large' : 'bad_json' });
      const item = createSession(parsed);
      send(res, 200, item);
    });
  }

  if (req.method === 'POST' && req.url.startsWith('/sessions/')) {
    return readJson(req, (err, parsed) => {
      if (err) return send(res, 400, { error: err.message === 'body_too_large' ? 'body_too_large' : 'bad_json' });
      const id = req.url.split('/').pop();
      const item = appendSession(id, parsed || {});
      if (!item) return send(res, 404, { error: 'session_not_found' });
      send(res, 200, item);
    });
  }

  if (req.method === 'POST' && req.url === '/heartbeat') {
    return readJson(req, (err, parsed) => {
      if (err) return send(res, 400, { error: err.message === 'body_too_large' ? 'body_too_large' : 'bad_json' });
      const item = heartbeat(parsed.agentId, parsed.meta);
      send(res, 200, item);
    });
  }

  if (req.method === 'POST' && req.url === '/execute') {
    return readJson(req, (err, parsed) => {
      if (err) return send(res, 400, { error: err.message === 'body_too_large' ? 'body_too_large' : 'bad_json' });
      const job = createJob(parsed);
      send(res, 200, job);
    });
  }

  if (req.method === 'POST' && req.url === '/poll') {
    return readJson(req, (_err, parsed) => {
      const job = nextJob(parsed && parsed.agentId);
      send(res, 200, { job });
    });
  }

  if (req.method === 'POST' && req.url === '/complete') {
    return readJson(req, (err, parsed) => {
      if (err) return send(res, 400, { error: err.message === 'body_too_large' ? 'body_too_large' : 'bad_json' });
      const job = completeJob(parsed.id, parsed.result || null);
      if (!job) return send(res, 404, { error: 'job_not_found' });
      send(res, 200, job);
    });
  }

  if (req.method === 'GET' && req.url.startsWith('/jobs/')) {
    const id = req.url.split('/').pop();
    const job = getJob(id);
    if (!job) return send(res, 404, { error: 'job_not_found' });
    return send(res, 200, job);
  }

  if (req.method === 'GET' && req.url.startsWith('/session/')) {
    const id = req.url.split('/').pop();
    const item = getSession(id);
    if (!item) return send(res, 404, { error: 'session_not_found' });
    return send(res, 200, item);
  }

  send(res, 404, { error: 'not_found' });
});

server.listen(port, () => {
  console.log(`wkappbot relay listening on ${port}`);
});
