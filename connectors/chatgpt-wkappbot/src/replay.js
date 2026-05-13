const { readEvents } = require('./log');

function rebuildState() {
  const jobs = new Map();
  const agents = new Map();

  for (const event of readEvents()) {
    if (event.type === 'job_created') {
      jobs.set(event.payload.id, event.payload);
    }

    if (event.type === 'job_started') {
      const job = jobs.get(event.payload.id);
      if (job) {
        job.state = 'running';
        job.agentId = event.payload.agentId || null;
      }
    }

    if (event.type === 'job_completed') {
      const job = jobs.get(event.payload.id);
      if (job) {
        job.state = 'done';
      }
    }

    if (event.type === 'agent_heartbeat') {
      agents.set(event.payload.id, event.payload);
    }
  }

  return {
    jobs: [...jobs.values()],
    agents: [...agents.values()]
  };
}

module.exports = { rebuildState };
