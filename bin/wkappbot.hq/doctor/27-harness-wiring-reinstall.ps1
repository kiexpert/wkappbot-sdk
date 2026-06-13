# wkdoctor check: harness-wiring -- verify ~/.claude/settings.json BeforeTool/PreToolUse/PostToolUse hooks
# are wired to the wkappbot harness (DIRECT kih -- wkappbot-kih + wkharness.ps1 -- OR via the
# D:\GitHub relay shims that forward into kih). Both are valid, healthy wirings.
#
# AUTO-FIX is GUARDED behind $env:WKDOCTOR_FIX_WIRING -eq '1' and is NOT run by default.

try {
    $settingsPath = Join-Path $env:USERPROFILE '.claude\settings.json'
    if (-not (Test-Path -LiteralPath $settingsPath -PathType Leaf)) {
        Add-Check 'harness-wiring' 'ok' 'n/a (settings.json not found)'
        Emit 'ok' 'harness-wiring' 'n/a (settings.json not found)'
    } else {
        $json = Get-Content -LiteralPath $settingsPath -Raw -ErrorAction Stop

        # Two valid wiring styles, both healthy:
        #  (a) DIRECT kih:  settings.json references wkappbot-kih AND the harness scripts
        #  (b) RELAY shim:  settings.json references wkharness.ps1 / wkharness-post.ps1
        $refKih    = $json -match 'wkappbot-kih'
        $preWired  = $json -match 'wkharness\.ps1'
        $postWired = $json -match 'wkharness-post\.ps1'
        
        # Gemini CLI hook name check: BeforeTool is preferred over PreToolUse
        $hasBeforeHook = $json -match '"BeforeTool"'
        $hasPreHook    = $json -match '"PreToolUse"'

        if ($preWired -and $postWired -and ($hasBeforeHook -or $hasPreHook)) {
            $style = if ($refKih) { 'kih+relay' } else { 'relay' }
            $hookName = if ($hasBeforeHook) { 'BeforeTool' } else { 'PreToolUse' }
            Add-Check 'harness-wiring' 'ok' "settings.json $hookName+PostToolUse hooks wired ($style)"     
            Emit 'ok' 'harness-wiring' "settings.json $hookName+PostToolUse hooks wired ($style)"
        } else {
            $missing = @()
            if (-not $preWired)  { $missing += 'PreToolUse/BeforeTool(wkharness.ps1)' }
            if (-not $postWired) { $missing += 'PostToolUse(wkharness-post.ps1)' }
            if (-not $hasBeforeHook -and -not $hasPreHook) { $missing += 'hook event key' }
            
            $detail = "harness hooks missing from settings.json: $($missing -join ', ')"
            Add-Check 'harness-wiring' 'warn' $detail
            Emit '!' 'harness-wiring' $detail

            $installer = 'D:\GitHub\wkappbot-kih\install.ps1'
            if ($env:WKDOCTOR_FIX_WIRING -ne '1') {
                Emit '!' 'harness-wiring:fix' '[DRY-RUN] set WKDOCTOR_FIX_WIRING=1 to re-run kih/install.ps1'
            } elseif (Test-Path -LiteralPath $installer) {
                try {
                    & $installer *> $null
                    $json2 = Get-Content -LiteralPath $settingsPath -Raw -ErrorAction Stop
                    $ok1 = $json2 -match 'wkharness\.ps1'
                    $ok2 = $json2 -match 'wkharness-post\.ps1'
                    if ($ok1 -and $ok2) {
                        Add-Check 'harness-wiring' 'ok' 'reinstalled -- hooks now wired'
                        Emit 'ok' 'harness-wiring:fixed' 'harness-wiring reinstalled -- hooks now wired'    
                    } else {
                        Emit '!' 'harness-wiring:fix' 'reinstall ran but hooks still missing'
                    }
                } catch {
                    Emit '!' 'harness-wiring:fix' "installer failed: $($_.Exception.Message)"
                }
            }
        }
    }
} catch {
    Add-Check 'harness-wiring' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'harness-wiring' 'n/a (check error, fail-open)'
}
