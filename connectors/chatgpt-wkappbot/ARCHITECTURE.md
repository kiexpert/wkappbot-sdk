# Architecture

## Flow

ChatGPT Action -> relay -> queue -> local agent -> wkappbot

## Components

- relay server
- polling agents
- event stream
- replay recovery
- session store
- dashboard

## Security

- bearer token
- outbound-only agents
- command allowlist
