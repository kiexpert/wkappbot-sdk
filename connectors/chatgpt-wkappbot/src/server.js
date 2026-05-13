const http = require('http');
const { createJob, getJob } = require('./store');

const port = Number(process.env.PORT || 8787);

function send(res, code, body) {
  res.writeHead(code, { 'content-type': 'application/json' });
  res.end(JSON.stringify(body));
}

const server = http.createServer((req, res) => {
  if (req.method === 'GET' && req.url === '/health') {
    return send(res, 200, { ok: true, service: 'wkappbot-chatgpt-relay' });
  }

  if (req.method === 'POST' && req.url === '/execute') {
    let body = '';

    req.on('data', chunk => {
      body += chunk;
    });

    req.on('end', () => {
      const parsed = body ? JSON.parse(body) : {};
      const job = createJob(parsed);
      send(res, 200, job);
    });

    return;
  }

  if (req.method === 'GET' && req.url.startsWith('/jobs/')) {
    const id = req.url.split('/').pop();
    const job = getJob(id);

    if (!job) {
      return send(res, 404, { error: 'job_not_found' });
    }

    return send(res, 200, job);
  }

  send(res, 404, { error: 'not_found' });
});

server.listen(port, () => {
  console.log(`wkappbot relay listening on ${port}`);
});
