# Cloudflare Worker Trigger Setup

## Required Secrets

Set these as Worker environment variables or secrets:

- `GITHUB_TOKEN`
- `GITHUB_REPO`
- `GITHUB_REF`

Example:

```text
GITHUB_REPO=kiexpert/wkappbot-sdk
GITHUB_REF=main
```

## Worker Entry

Use:

- `cloudflare-worker-trigger.js`

## Request Example

```http
POST /run
content-type: application/json

{
  "minutes": 25,
  "query": "hello wkappbot"
}
```
