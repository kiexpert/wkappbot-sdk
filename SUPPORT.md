# Support

## Community support (free)

- **GitHub Issues** — bug reports, feature requests, license questions
  Use the [issue templates](.github/ISSUE_TEMPLATE/) to get a faster response.
- **GitHub Discussions** — general questions, show and tell, ideas

## Paid support

For CDP/Sudo/Enterprise subscribers:
- Email: kiexpert@kivilab.co.kr
- Response within 24 hours on business days

## Before filing an issue

Run diagnostics and include the output:
```bash
wkappbot --version
wkappbot eye tick --timeout 5
```

## Common issues

| Symptom | First step |
|---------|-----------|
| `wkappbot: command not found` | Add `bin\` to PATH — see [README](README.md#installation) |
| License shows Free after payment | Accept the GitHub collaborator invitation in your email |
| Eye won't start | Check `bin\wkappbot.hq\logs\` for errors |
| Build fails (vswhere) | Install Visual Studio Build Tools 2022 |
