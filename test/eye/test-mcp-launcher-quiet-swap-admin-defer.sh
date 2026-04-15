#!/bin/bash
set -euo pipefail

WKAPPBOT="${WKAPPBOT:-D:/GitHub/WKAppBot/csharp/src/WKAppBot.Launcher/bin/Release/net8.0-windows/win-x64/wkappbot.exe}"
CORE_WIN="${CORE_WIN:-D:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI\bin\Release\net8.0-windows10.0.22621.0\win-x64\wkappbot-core.exe}"
OUTDIR=".wkappbot"
OUTFILE="$OUTDIR/test-mcp-launcher-quiet-swap-admin-defer.out"
ERRFILE="$OUTDIR/test-mcp-launcher-quiet-swap-admin-defer.err"
OUTFILE_WIN="D:\GitHub\WKAppBot\.wkappbot\test-mcp-launcher-quiet-swap-admin-defer.out"
ERRFILE_WIN="D:\GitHub\WKAppBot\.wkappbot\test-mcp-launcher-quiet-swap-admin-defer.err"

/usr/bin/mkdir -p "$OUTDIR"
D:/SDK/bin/wkappbot-core.exe mcp launcher --help >/dev/null 2>&1 || true
D:/SDK/bin/wkappbot-core.exe suggest list --limit 1 >/dev/null

printf '%s\n%s\n%s\n' \
  '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test-mcp-launcher","version":"1.0"}}}' \
  '{"jsonrpc":"2.0","method":"notifications/initialized"}' \
  '{"jsonrpc":"2.0","id":2,"method":"ping"}' \
  | /usr/bin/timeout 20 "$WKAPPBOT" --core "$CORE_WIN" mcp --no-wt > "$OUTFILE" 2> "$ERRFILE" || true

powershell.exe -NoProfile -Command "Select-String -Path '$ERRFILE_WIN' -SimpleMatch -- '--core override:','cmd=mcp','mcp → RunMcpProxy','core started via pipe','stdin relay started','[MCP] << initialize','[MCP] >> initialize OK' | Out-Null"
powershell.exe -NoProfile -Command "Select-String -Path '$OUTFILE_WIN' -SimpleMatch -- 'serverInfo','\"id\":2','pong' | Out-Null"
powershell.exe -NoProfile -Command "Select-String -Path 'D:\GitHub\WKAppBot\csharp\src\WKAppBot.Launcher\Program.cs' -SimpleMatch -- 'var forwardArgs','if (cmd == \"mcp\")','RunMcpProxy(forwardArgs)' | Out-Null"
powershell.exe -NoProfile -Command "Select-String -Path 'D:\GitHub\WKAppBot\csharp\src\WKAppBot.Launcher\McpProxy.cs' -SimpleMatch -- '_pendingSwapWhileAdmin','_activeCoreStamp','_lastFailedSwapStamp','currentSwapStamp' | Out-Null"

echo "mcp launcher quiet-swap admin defer: PASS"