# Daily Issue Worker

Use `connectors/chatgpt-wkappbot/cloudflare-worker-daily-issue.js` as a Cloudflare Worker.

Required secret/env:

- `GITHUB_TOKEN`

Optional env:

- `GITHUB_REPO`

Request:

```json
{
  "repo": "kiexpert/wkappbot-sdk",
  "label": "daily",
  "title": "Daily board"
}
```

The worker creates today's Daily issue if it does not already exist.
