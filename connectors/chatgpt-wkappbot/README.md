# ChatGPT WKAppBot Connector

Production-oriented relay and local agent for exposing WKAppBot to ChatGPT Actions.

## Architecture

ChatGPT -> relay -> local polling agent -> wkappbot

The local agent keeps an outbound polling connection to the relay.
The relay never directly connects to localhost.

## Relay

Start relay:

node src/server.js

Health check:

GET /health

Create execution job:

POST /execute

Poll next queued job:

POST /poll

Complete job:

POST /complete

Read job state:

GET /jobs/:id

## Local Agent

Start agent:

node src/agent.js

The agent polls the relay and executes wkappbot commands locally.

## Security

- bearer token support
- command allowlist
- outbound-only local agent
- no localhost exposure

## Docker

Build:

docker build -t wk-chat .

Run:

docker compose up
