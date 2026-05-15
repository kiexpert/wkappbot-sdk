# Runbook

## Start relay

```bash
node src/server.js
```

## Start agent

```bash
node src/agent.js
```

## Submit job

```bash
curl -s -X POST http://127.0.0.1:8787/execute -H 'content-type: application/json' -d '{"args":["--version"]}'
```

## Watch events

```bash
curl -N http://127.0.0.1:8787/events
```
