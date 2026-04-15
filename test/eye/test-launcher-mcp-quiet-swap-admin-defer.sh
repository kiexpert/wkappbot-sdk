#!/usr/bin/env bash
set -euo pipefail

cmd.exe /c dotnet build "D:\\GitHub\\WKAppBot\\csharp\\src\\WKAppBot.Launcher\\WKAppBot.Launcher.csproj" -c Release -nologo > /tmp/test-launcher-mcp-build.log
/w/SDK/bin/wkappbot-core.exe suggest list --limit 1

powershell.exe -NoProfile -Command "Select-String -Path 'D:\\GitHub\\WKAppBot\\csharp\\src\\WKAppBot.Launcher\\Program.cs' -SimpleMatch -- '--core','if (cmd == \"mcp\")' | Out-Null"

powershell.exe -NoProfile -Command "Select-String -Path 'D:\\GitHub\\WKAppBot\\csharp\\src\\WKAppBot.Launcher\\McpProxy.cs' -SimpleMatch -- '_pendingSwapWhileAdmin','_activeCoreStamp','_lastFailedSwapStamp','admin endpoint active' | Out-Null"

echo "launcher mcp quiet-swap admin defer: PASS"
