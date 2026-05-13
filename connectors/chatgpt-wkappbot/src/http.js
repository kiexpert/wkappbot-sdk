function json(res, code, body) {
  res.writeHead(code, {
    'content-type': 'application/json'
  });

  res.end(JSON.stringify(body));
}

module.exports = { json };
