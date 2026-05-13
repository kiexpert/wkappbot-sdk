const jobs = new Map();

function createJob(input) {
  const id = String(Date.now()) + '-' + Math.random().toString(16).slice(2);
  const job = {
    id,
    state: 'queued',
    input,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
    result: null
  };
  jobs.set(id, job);
  return job;
}

function nextJob() {
  for (const job of jobs.values()) {
    if (job.state === 'queued') {
      job.state = 'running';
      job.updatedAt = new Date().toISOString();
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
  job.updatedAt = new Date().toISOString();
  return job;
}

function getJob(id) {
  return jobs.get(id) || null;
}

module.exports = { createJob, nextJob, completeJob, getJob };
