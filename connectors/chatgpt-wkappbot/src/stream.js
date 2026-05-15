const clients = new Set();

function addClient(res) {
  clients.add(res);
  res.writeHead(200, {
    'content-type': 'text/event-stream',
    'cache-control': 'no-cache',
    'connection': 'keep-alive'
  });
  res.write('\n');
}

function removeClient(res) {
  clients.delete(res);
}

function publish(type, payload) {
  const row = `event: ${type}\ndata: ${JSON.stringify(payload || {})}\n\n`;
  for (const res of clients) {
    try { res.write(row); } catch (_) { clients.delete(res); }
  }
}

module.exports = { addClient, removeClient, publish };
