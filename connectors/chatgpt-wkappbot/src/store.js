const jobs = new Map();
const agents = new Map();
const { appendEvent } = require('./log');
const { publish } = require('./stream');

function now() {
  return new Date().toISOString();
}

function createJob(input) {
  const id = String(Date.now()) + '-' + Math.random().toString(16).slice(2);
  const job = {
    id,
    state: 'queued',
    input,
    createdAt: now(),
    updatedAt: now(),
    result: null
  };

  jobs.set(id, job);

  appendEvent('job_created', job);
  publish('job_created', job);

  return job;
}

function nextJob(agentId) {
  for (const job of jobs.values()) {
    if (job.state === 'queued') {
      job.state = 'running';
      job.agentId = agentId || null;
      job.updatedAt = now();

      const payload = {
        id: job.id,
        agentId: job.agentId
      };

      appendEvent('job_started', payload);
      publish('job_started', payload);

      return job;
    }
  }

  return null;
}

function completeJob(id, result) {
  const job = jobs.get(id);
  if (!job) return null;

  job.state = 'done';
  job.result = result;
  job.updatedAt = now();

  const payload = {
    id,
    ok: result && result.ok
  };

  appendEvent('job_completed', payload);
  publish('job_completed', payload);

  return job;
}

function getJob(id) {
  return jobs.get(id) || null;
}

function listJobs() {
  return [...jobs.values()];
}

function heartbeat(agentId, meta) {
  const id = agentId || 'default';

  const agent = {
    id,
    meta: meta || {},
    updatedAt: now()
  };

  agents.set(id, agent);

  appendEvent('agent_heartbeat', agent);
  publish('agent_heartbeat', agent);

  return agent;
}

function listAgents() {
  return [...agents.values()];
}

module.exports = {
  createJob,
  nextJob,
  completeJob,
  getJob,
  listJobs,
  heartbeat,
  listAgents
};
