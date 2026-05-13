# Release

## Package

Create an archive from `connectors/chatgpt-wkappbot`.

## Validate

- run smoke workflow
- check `/health`
- check `/agents`
- submit `--version` job
- verify job completion

## Publish

Deploy relay to HTTPS host and install agents on target machines.
