---
id: antigravity-cli-agy-integration-guide
app: personal-docs
description: "Operational guide for Google's Antigravity CLI (agy) within the Hongik Harness environment."
---

> **Refresh**: `wkappbot skill read antigravity-cli-agy-integration-guide --if-newer` — v1.0 (2026-06-07)

# Antigravity CLI (agy) Integration & Harness Wiring

## Steps

1. 1. INSTALLATION: installed via official Go binary installer at %LOCALAPPDATA%\agy\bin\agy.exe.
2. 2. HARNESS WIRING: configured via wkharness-agy-install.ps1; settings.json in .agents hooks into modular guards.
3. 3. WRAPPER USAGE: Use 'wkagy.sh' for daily budget guarding and API key auto-loading. Supports --stream and --yolo flags.
4. 4. AGENT INTEGRATION: Integrated into Agent.cmd as a first-class model tier. Usage: Agent.cmd --model agy 'prompt'.
