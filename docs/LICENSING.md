# Licensing

## Launcher (open source)

The launcher binary (`wkappbot.exe`) and its source code (`csharp/src/WKAppBot.Launcher/`) are released under the **MIT License**. See [LICENSE](../LICENSE).

You may freely use, modify, and distribute the launcher.

## Core binary (closed source)

`wkappbot-core.exe` is proprietary software. The binary is provided free for personal use under the following terms:

- **Free personal use**: unlimited
- **Commercial use**: requires a paid subscription (see [SUBSCRIBE.md](../SUBSCRIBE.md))
- **Redistribution**: not permitted without written consent

The core binary is downloaded automatically by `build.cmd` from the latest GitHub Release.

## Third-party components

The launcher uses the following open-source packages:

| Package | License |
|---------|---------|
| .NET 8 Runtime | MIT |

Run `dotnet list package --include-transitive` for the full dependency list.

## Feature tiers

| Tier | License | Price |
|------|---------|-------|
| Free | Personal use | Free |
| CDP | Commercial subscription | ₩100,000/mo |
| Sudo | Commercial subscription | ₩500,000/mo |

See [PRICING.md](../PRICING.md) for the full feature comparison.

## License validation

License is validated via GitHub collaborator permission on `kiexpert/wkappbot-sdk`.
No license file needs to be downloaded — the binary checks your GitHub account status directly.

Offline grace period: 7 days (cached from last successful check).
