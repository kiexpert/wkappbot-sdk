const sessions = new Map();

function createSession(meta) {
  const id = String(Date.now()) + '-' + Math.random().toString(16).slice(2);
  const item = {
    id,
    meta: meta || {},
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    history: []
  };
  sessions.set(id, item);
  return item;
}

function appendSession(id, event) {
  const item = sessions.get(id);
  if (!item) return null;
  item.history.push({
    time: new Date().toISOString(),
    event
  });
  item.updatedAt = new Date().toISOString();
  return item;
}

function getSession(id) {
  return sessions.get(id) || null;
}

function listSessions() {
  return [...sessions.values()];
}

module.exports = { createSession, appendSession, getSession, listSessions };
