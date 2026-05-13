function statusSnapshot(parts) {
  return {
    ok: true,
    service: 'wkappbot-chatgpt-relay',
    time: new Date().toISOString(),
    parts: parts || {}
  };
}

module.exports = { statusSnapshot };
