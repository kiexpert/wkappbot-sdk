const roles = {
  reader: ['health', 'jobs', 'agents', 'sessions'],
  operator: ['health', 'jobs', 'agents', 'sessions', 'execute'],
  agent: ['health', 'poll', 'complete', 'heartbeat']
};

function can(role, action) {
  const allowed = roles[role || 'operator'] || [];
  return allowed.includes(action);
}

module.exports = { can };
