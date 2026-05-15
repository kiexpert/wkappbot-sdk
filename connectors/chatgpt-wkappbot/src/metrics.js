const metrics = {
  jobsCreated: 0,
  jobsCompleted: 0,
  jobsFailed: 0,
  heartbeats: 0
};

function mark(name) {
  metrics[name] = (metrics[name] || 0) + 1;
}

function snapshot() {
  return {
    ...metrics,
    time: new Date().toISOString()
  };
}

module.exports = { mark, snapshot };
