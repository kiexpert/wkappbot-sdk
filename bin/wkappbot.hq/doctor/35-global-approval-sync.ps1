# wk-approval-sync.ps1
# Synchronizes tool approvals between family-specific local files and global policies.
# Families: Claude (settings.json), Gemini (auto-saved.toml)

$ClaudeLocal = Join-Path $env:USERPROFILE '.claude\settings.local.json'
$ClaudeGlobal = Join-Path $env:USERPROFILE '.claude\settings.json'
$GeminiPolicy = Join-Path $env:USERPROFILE '.gemini\policies\auto-saved.toml'

function Sync-ClaudeApprovals {
    if (-not (Test-Path $ClaudeLocal)) { return }
    if (-not (Test-Path $ClaudeGlobal)) { return }

    $localMtime = (Get-Item $ClaudeLocal).LastWriteTime
    $globalMtime = (Get-Item $ClaudeGlobal).LastWriteTime

    if ($localMtime -le $globalMtime) { return }

    Write-Host "[sync:claude] New approvals detected in settings.local.json. Syncing..." -ForegroundColor Cyan
    try {
        $l = Get-Content $ClaudeLocal -Raw | ConvertFrom-Json
        $g = Get-Content $ClaudeGlobal -Raw | ConvertFrom-Json

        if ($l.permissions -and $l.permissions.allow) {
            $localAllow = @($l.permissions.allow)
            $globalAllow = if ($g.permissions -and $g.permissions.allow) { @($g.permissions.allow) } else { @() }
            
            $newItems = $localAllow | Where-Object { $globalAllow -notcontains $_ }
            if ($newItems) {
                $g.permissions.allow = @($globalAllow + $newItems) | Select-Object -Unique
                $g | ConvertTo-Json -Depth 10 | Set-Content $ClaudeGlobal -Encoding UTF8
                Write-Host "[sync:claude] Added $($newItems.Count) item(s) to global settings.json" -ForegroundColor Green
                
                # Also update Gemini policy if it's a command approval
                Sync-GeminiFromClaude -NewItems $newItems
            }
        }
    } catch {
        Write-Error "[sync:claude] Failed to sync: $_"
    }
}

function Sync-GeminiFromClaude {
    param([string[]]$NewItems)
    if (-not $NewItems) { return }
    if (-not (Test-Path $GeminiPolicy)) { return }

    Write-Host "[sync:gemini] Propagating Claude approvals to Gemini policy..." -ForegroundColor Cyan
    $policyContent = Get-Content $GeminiPolicy -Raw
    $addedCount = 0

    foreach ($item in $NewItems) {
        # Convert command(git) -> git
        if ($item -match '^command\((.+)\)$') {
            $cmd = $Matches[1]
            if ($policyContent -notmatch "commandPrefix = \[ `"$cmd`\" \]") {
                $rule = @"

[[rule]]
decision = "allow"
priority = 950
toolName = "run_shell_command"
commandPrefix = [ "$cmd" ]
modes = [ "default", "autoEdit", "yolo" ]
"@
                $policyContent += $rule
                $addedCount++
            }
        }
    }

    if ($addedCount -gt 0) {
        [IO.File]::WriteAllText($GeminiPolicy, $policyContent, [System.Text.Encoding]::UTF8)
        Write-Host "[sync:gemini] Added $addedCount rule(s) to auto-saved.toml" -ForegroundColor Green
    }
}

# Run sync
Sync-ClaudeApprovals
