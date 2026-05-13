# ChatGPT Action Setup

Use `api-schema.yaml` as the Action schema.

Recommended deployment:

1. Run relay on a public HTTPS host.
2. Set `WKAPPBOT_TOKEN` on both relay and local agent.
3. Start one or more local agents from machines that have WKAppBot installed.
4. Configure ChatGPT Action authentication as bearer token.

Required public routes:

- `GET /health`
- `POST /execute`
- `GET /jobs/{id}`

Optional operator routes:

- `GET /jobs`
- `GET /agents`
- `GET /events`
