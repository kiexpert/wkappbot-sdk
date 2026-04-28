# Installation Guide

## Requirements

- Windows 10 22H2+ or Windows 11
- [Git](https://git-scm.com/download/win)
- [GitHub CLI (`gh`)](https://cli.github.com/) — needed for paid tiers; free tier works without it

## Option A — Clone and build (recommended)

```bat
git clone https://github.com/kiexpert/wkappbot-sdk %USERPROFILE%\Documents\wkappbot
cd %USERPROFILE%\Documents\wkappbot
build.cmd
```

`build.cmd` downloads the pre-built core binary and builds the launcher from source.
Requires .NET SDK 8+ — install from <https://dot.net>.

## Option B — Download release zip

1. Go to [Releases](https://github.com/kiexpert/wkappbot-sdk/releases/latest)
2. Download `wkappbot-X.Y.Z.zip`
3. Extract to `%USERPROFILE%\Documents\wkappbot`

## Add to PATH

```powershell
[Environment]::SetEnvironmentVariable(
  'PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User'
)
```

Restart your terminal, then verify:

```bash
wkappbot --version
wkappbot skill list
```

## Activate a paid license (CDP / Sudo)

```bash
gh auth login        # one-time GitHub authentication
wkappbot license status
```

Then follow [SUBSCRIBE.md](../SUBSCRIBE.md).
After accepting the collaborator invite, run `wkappbot license status` again — your tier activates immediately.

## Updating

```bat
cd %USERPROFILE%\Documents\wkappbot
git pull
build.cmd
```

Eye auto-detects the new `wkappbot-core.new.exe` and hot-swaps with zero downtime.
