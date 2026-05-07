#!/usr/bin/env powershell
# ev-cdp-isolation.ps1 -- Evidence script for CDP port isolation + position fixes

param([switch]$Verbose)

$errors = 0
$pass   = 0

function Check($label, [scriptblock]$test) {
    try {
        $result = & $test
        if ($result) {
            Write-Host "PASS: $label" -ForegroundColor Green
            $script:pass++
        } else {
            Write-Host "FAIL: $label" -ForegroundColor Red
            $script:errors++
        }
    } catch {
        Write-Host "FAIL: $label -- $_" -ForegroundColor Red
        $script:errors++
    }
}

Write-Host "`n=== CDP Isolation Evidence Script ===" -ForegroundColor Cyan

# Clean state: kill wkappbot-managed Chromes and clear port registry
Write-Host "[SETUP] Clearing port registry files..." -ForegroundColor DarkGray
Get-ChildItem "D:/GitHub/WKAppBot/bin/wkappbot.hq/runtime","D:/GitHub/wkappbot-sdk/bin/wkappbot.hq/runtime" `
    -Filter "cdp_port_*.txt" -ErrorAction SilentlyContinue | Remove-Item -Force
# Remove stale geometry files too
Get-ChildItem "D:/GitHub/WKAppBot/bin/wkappbot.hq/runtime","D:/GitHub/wkappbot-sdk/bin/wkappbot.hq/runtime" `
    -Filter "chrome_geometry_*.json" -ErrorAction SilentlyContinue | Remove-Item -Force
Start-Sleep -Milliseconds 500

# Note: cdp commands route through Eye (long-running, may use older binary).
# Use wkappbot-core.exe directly for port tests to bypass Eye.
$core = if (Test-Path "D:/GitHub/WKAppBot/bin/wkappbot-core.new.exe") {
    "D:/GitHub/WKAppBot/bin/wkappbot-core.new.exe"
} else {
    "D:/GitHub/WKAppBot/bin/wkappbot-core.exe"
}
if ($Verbose) { Write-Host "  Core: $([IO.Path]::GetFileName($core))" }

# ------------------------------------------------------------------
# 1. Port derivation is in 9300-9999 range (not hardcoded 9222)
# ------------------------------------------------------------------
Check "cdp open returns port in 9300-9999 range" {
    $out = & $core cdp open "https://example.com" 2>&1 | Out-String
    if ($out -match 'OK \{[^}]*cdp:(\d+)') {
        $port = [int]$Matches[1]
        if ($Verbose) { Write-Host "  port=$port" }
        return $port -ge 9300 -and $port -le 9999
    }
    return $false
}

# ------------------------------------------------------------------
# 2. Same project gets same port on consecutive calls
# ------------------------------------------------------------------
Check "same project reuses same port" {
    $out1 = & $core cdp open "https://example.com" 2>&1 | Out-String
    $out2 = & $core cdp open "https://example.com" 2>&1 | Out-String
    $port1 = if ($out1 -match 'OK \{[^}]*cdp:(\d+)') { [int]$Matches[1] } else { 0 }
    $port2 = if ($out2 -match 'OK \{[^}]*cdp:(\d+)') { [int]$Matches[1] } else { 0 }
    if ($Verbose) { Write-Host "  port1=$port1 port2=$port2" }
    return $port1 -gt 0 -and $port1 -eq $port2
}

# ------------------------------------------------------------------
# 3. Port NOT 9222
# ------------------------------------------------------------------
Check "port is not legacy 9222" {
    $out = & $core cdp open "https://example.com" 2>&1 | Out-String
    if ($out -match 'OK \{[^}]*cdp:(\d+)') {
        return [int]$Matches[1] -ne 9222
    }
    return $false
}

# ------------------------------------------------------------------
# 4. Different projects derive different port blocks (no collision)
# ------------------------------------------------------------------
Check "different projects get different port blocks" {
    $pyScript = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), "cdp_port_check.py")
    Set-Content $pyScript @'
import hashlib, struct, sys
path = sys.argv[1].lower().replace('/', chr(92)).rstrip(chr(92))
h = hashlib.sha256(path.encode('utf-8')).digest()
n = struct.unpack('>I', h[:4])[0]
print(9300 + (n % 174) * 4)
'@ -Encoding UTF8

    $projects = @('D:/GitHub/WKAppBot','D:/GitHub/wkappbot-sdk','D:/GitHub/WkAutoQuant','D:/GitHub/personal-docs') |
                Where-Object { Test-Path $_ }

    $blocks = @{}; $collision = $false
    foreach ($proj in $projects) {
        $raw = python3 $pyScript $proj 2>$null | Where-Object { $_ -match '^\d+$' } | Select-Object -First 1
        $port = [int]([string]$raw).Trim()
        $block = $port -band 0xFFFC
        if ($blocks.ContainsKey($block)) { $collision = $true }
        $blocks[$block] = $proj
        if ($Verbose) { Write-Host "  $([IO.Path]::GetFileName($proj)) -> port $port (block $block)" }
    }
    Remove-Item $pyScript -ErrorAction SilentlyContinue
    return -not $collision
}

# ------------------------------------------------------------------
# 5. Renderer parent-PID walk for CDP port is in WindowFinder
# ------------------------------------------------------------------
Check "renderer CDP port walk: parent PID code present in WindowFinder" {
    $src = Get-Content "D:/GitHub/WKAppBot/csharp/src/WKAppBot.Win32/Window/WindowFinder.cs" -Raw
    return ($src -match 'type=renderer') -and ($src -match 'GetParentProcessId')
}

# ------------------------------------------------------------------
# 6. No GeometryFilePath in active source (removed)
# ------------------------------------------------------------------
Check "GeometryFilePath removed from WebBot source" {
    $src = Get-Content "D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/CdpClient.Window2.cs" -Raw
    return $src -notmatch 'GeometryFilePath'
}

# ------------------------------------------------------------------
# 7. No chrome_geometry files on disk
# ------------------------------------------------------------------
Check "no chrome_geometry_*.json files on disk" {
    $dirs = @(
        "D:/GitHub/WKAppBot/bin/wkappbot.hq/runtime",
        "D:/GitHub/wkappbot-sdk/bin/wkappbot.hq/runtime"
    ) | Where-Object { Test-Path $_ }
    $files = $dirs | ForEach-Object { Get-ChildItem $_ -Filter "chrome_geometry_*.json" -ErrorAction SilentlyContinue }
    if ($Verbose -and $files) { $files | ForEach-Object { Write-Host "  $_" } }
    return @($files).Count -eq 0
}

# ------------------------------------------------------------------
# 8. DerivePort uses SHA256 (not GetHashCode)
# ------------------------------------------------------------------
Check "DerivePort uses SHA256 not GetHashCode" {
    $src = Get-Content "D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot/ChromeLauncher.cs" -Raw
    return ($src -match 'SHA256\.HashData') -and ($src -notmatch '\.GetHashCode\(\).*% 700')
}

# ------------------------------------------------------------------
# 9. No hardcoded 9222 in active CDP call sites
# ------------------------------------------------------------------
Check "no hardcoded 9222 in CDP call sites" {
    $files = @(
        "D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/WebCommands.cs",
        "D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Entry.Cdp.cs",
        "D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AskCommands.Slack.cs"
    )
    $found = $false
    foreach ($f in $files) {
        $src = Get-Content $f -Raw -ErrorAction SilentlyContinue
        if ($src -match 'ConnectAsync\(9222|DetectCdpPort\(9222|port\s*=\s*9222') {
            if ($Verbose) { Write-Host "  FOUND 9222 in $([IO.Path]::GetFileName($f))" }
            $found = $true
        }
    }
    return -not $found
}

# ------------------------------------------------------------------
Write-Host ""
Write-Host "=== Results: $pass passed, $errors failed ===" -ForegroundColor $(if ($errors -eq 0) {'Green'} else {'Yellow'})
exit $errors
