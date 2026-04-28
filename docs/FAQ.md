# Frequently Asked Questions

## Why is the core binary closed source?

The automation engine contains years of hard-won knowledge about UIA quirks, MFC workarounds, CDP edge cases, and HTS trading terminal behavior. Keeping it closed protects this investment while the launcher (the glue layer) is fully open and auditable.

## Does WKAppBot collect data?

The experience DB (`bin\wkappbot.hq\experience\`) caches UI element locations locally on your machine — nothing is sent to external servers. Logs stay in `bin\wkappbot.hq\logs\` locally.

Set `WKAPPBOT_NO_TELEMETRY=1` to disable all optional telemetry (currently none is sent; the flag is reserved for future use).

## What happens when my subscription expires?

Your license tier reverts to Free automatically on the next `wkappbot` startup after the 30-day window. All base automation keeps working — only CDP/Sudo features gate.

## Can I use WKAppBot in CI/CD pipelines?

Yes. Set `WKAPPBOT_WORKER=1` to suppress interactive output. License checks still apply — use a service GitHub account as the collaborator for CI contexts.

## Does it work on Windows Server?

Windows Server 2019+ is supported in headless mode. GUI automation (`a11y`) requires a desktop session (RDP or console). Eye daemon works over RDP.

## Can I automate web apps without a Slack bot?

Yes — Eye and Slack are optional. `wkappbot a11y` and `wkappbot ask` work standalone. Eye provides the persistent daemon mode and Slack integration.

## How do I automate an app that has no UIA support?

WKAppBot falls back automatically through 5 tiers: UIA → Vision Cache → OCR → Vision API → coordinates. Legacy MFC apps like HTS trading terminals are supported via Win32 PostMessage.

## Is Android automation supported?

Yes, via ADB: `wkappbot a11y find "adb://device/*element*"`. Requires `adb` in PATH and USB debugging enabled.

## How do I contribute?

See [CONTRIBUTING.md](../CONTRIBUTING.md). Launcher source is MIT — PRs welcome. Core is closed; contribute via skill authoring and handler YAML files instead.
