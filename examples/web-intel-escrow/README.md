# WKAppBot Web Intelligence Escrow Demo

This example turns WKAppBot into a living public proof system.

It demonstrates the workflow:

1. Collect public web-intelligence topics.
2. Produce a safe public summary.
3. Store sensitive payloads in a private repository.
4. Leave auditable evidence in a public GitHub Issue.

The public repository works as the showroom. The private repository works as the secure brain store.

## What this demo proves

WKAppBot is not just a chatbot wrapper. It can run an auditable workflow:

```text
search -> summarize -> decide -> escrow -> private commit -> public evidence
```

## Public evidence

Each run creates or updates a daily issue with:

- workflow timestamp
- checked topics
- public insights
- escrow counts
- private payload status, without exposing the payload
- evidence checklist

## Private payload

If `APPBOT_PRIVATE_REPO` and `APPBOT_PRIVATE_REPO_TOKEN` are configured, the workflow commits detailed JSON to the private repository.

Recommended private repo:

```text
kiexpert/wkappbot-private
```

No secrets, raw prompts, private source text, or detailed scoring should be printed to public logs.

## Demo positioning

```text
Talk is cheap. Actions logs are evidence.
```

WKAppBot can turn AI/web intelligence into reproducible, timestamped, reviewable automation evidence.
