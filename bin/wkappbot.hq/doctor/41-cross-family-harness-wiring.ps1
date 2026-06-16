# wkdoctor check: cross-family-harness-wiring -- CROSS-FAMILY harness hook validation
# Promoted from 38-family-harness-wiring-proto.ps1 (2026-06-17). Generalizes the 5 wiring
# invariants that 34-gemini-settings-bom.ps1 asserts for gemini to ALL families.
# Detects + reports harness hook regressions (missing hooks, wrong paths, broken settings)
# across claude (~/.claude/settings.json), gemini (~/.gemini/settings.json),
# codex (~/.codex/hooks.json), and agy (~/.gemini/antigravity-cli/settings.json).
#
# THE 5 INVARIANTS PER FAMILY (codified from manual-review 2026-06-16):
#   (1) PreHook targets the PER-FAMILY main wkharness-<family>-main.ps1
#       (codex routes via CodexPreToolUseAdapter.ps1 -> per-family main; that adapter is accepted)
#   (2) no reference to a retired stray hub (e.g. D:\GitHub\wkharness.ps1)
#   (3) no literal TAB in the hook command path (JSON \t-escape corruption)
#   (4) timeout in the family's correct UNIT + sane (gemini/codex/agy ms >= 3000; claude sec >= 5)
#   (5) PostHook exists and targets a per-family wkharness(-<family>)?-post.ps1
#       (codex routes via CodexPostToolUseAdapter.ps1; that adapter is accepted)
#
# FAIL-SAFE: read-only checks + soft-fail on errors (n/a, never crash doctor).
# Settings hook structure is { PreToolUse|BeforeTool: [ { id, hooks: [ { command, timeout } ] } ] }
# so detection MUST descend into the inner .hooks[] array (the proto bug: it filtered the OUTER
# {id, hooks} object whose .command is null, reporting every family's PreHook as "not found").

try {
    # Define per-family configuration.
    #   PreMainPattern  : regex the PreHook command must match (per-family main, OR codex adapter)
    #   PostPattern     : regex the PostHook command must match (per-family post, OR codex adapter)
    $families = @(
        @{
            Name                = 'claude'
            SettingsPath        = Join-Path $env:USERPROFILE '.claude\settings.json'
            PreHookKey          = 'PreToolUse'
            PostHookKey         = 'PostToolUse'
            TimeoutUnit         = 'seconds'
            TimeoutMin          = 5
            PreMainPattern      = 'wkharness(-claude)?-main\.ps1'
            PostPattern         = 'wkharness(-claude)?-post\.ps1'
        },
        @{
            Name                = 'gemini'
            SettingsPath        = Join-Path $env:USERPROFILE '.gemini\settings.json'
            PreHookKey          = 'BeforeTool'
            PostHookKey         = 'AfterTool'
            TimeoutUnit         = 'milliseconds'
            TimeoutMin          = 3000
            PreMainPattern      = 'wkharness(-gemini)?-main\.ps1'
            PostPattern         = 'wkharness(-gemini)?-post\.ps1'
        },
        @{
            Name                = 'codex'
            SettingsPath        = Join-Path $env:USERPROFILE '.codex\hooks.json'
            PreHookKey          = 'PreToolUse'
            PostHookKey         = 'PostToolUse'
            # codex hooks.json is Claude-Code-style: timeout is in SECONDS (live values 30/10),
            # NOT ms (the proto's guess). Treat like claude to avoid a false fail-open warning.
            TimeoutUnit         = 'seconds'
            TimeoutMin          = 5
            # codex wires through adapters that re-dispatch into the per-family main/post.
            PreMainPattern      = 'wkharness(-codex)?-main\.ps1|CodexPreToolUseAdapter\.ps1'
            PostPattern         = 'wkharness(-codex)?-post\.ps1|CodexPostToolUseAdapter\.ps1'
        },
        @{
            Name                = 'agy'
            SettingsPath        = Join-Path $env:USERPROFILE '.gemini\antigravity-cli\settings.json'
            PreHookKey          = 'PreToolUse'
            PostHookKey         = 'PostToolUse'
            TimeoutUnit         = 'milliseconds'
            TimeoutMin          = 3000
            PreMainPattern      = 'wkharness(-agy)?-main\.ps1'
            PostPattern         = 'wkharness(-agy)?-post\.ps1'
        }
    )

    # Pull the inner hook entries (the {command,timeout,...} objects) for a given top-level key.
    # Settings shape: hooks.<Key> = @( { id/matcher, hooks: @( {command,...} ) } ).
    function Get-WkInnerHooks {
        param($SettingsHooks, [string]$Key)
        $inner = @()
        try {
            $top = $SettingsHooks.PSObject.Properties | Where-Object { $_.Name -eq $Key } | Select-Object -First 1
            if (-not $top) { return @() }
            foreach ($group in @($top.Value)) {
                if ($group.PSObject.Properties['hooks']) {
                    foreach ($h in @($group.hooks)) { $inner += $h }
                } elseif ($group.PSObject.Properties['command']) {
                    # tolerate a flat entry that already IS a hook
                    $inner += $group
                }
            }
        } catch {}
        return $inner
    }

    function Test-FamilyHarnessWiring {
        param([hashtable]$FamilyDef)
        $result = [ordered]@{
            Family          = $FamilyDef.Name
            Status          = 'ok'
            Issues          = @()
            PreHookFound    = $false
            PostHookFound   = $false
            PreHookCommand  = ''
            PostHookCommand = ''
        }

        if (-not (Test-Path $FamilyDef.SettingsPath -PathType Leaf)) {
            $result.Status = 'warn'
            $result.Issues += "settings file not found: $($FamilyDef.SettingsPath)"
            return $result
        }

        try {
            $content  = [IO.File]::ReadAllText($FamilyDef.SettingsPath, [Text.Encoding]::UTF8)
            $settings = ConvertFrom-Json -InputObject $content -ErrorAction Stop
        } catch {
            $result.Status = 'warn'
            $result.Issues += "settings parse error: $_"
            return $result
        }

        if (-not $settings.hooks) {
            $result.Status = 'warn'
            $result.Issues += "no 'hooks' object in settings"
            return $result
        }

        # PreHook: the inner entry whose command references wkharness OR the codex adapter.
        $preInner  = Get-WkInnerHooks -SettingsHooks $settings.hooks -Key $FamilyDef.PreHookKey
        $preHook   = $preInner | Where-Object { $_.command -and ($_.command -match 'wkharness' -or $_.command -match 'CodexPreToolUseAdapter') } | Select-Object -First 1
        $postInner = Get-WkInnerHooks -SettingsHooks $settings.hooks -Key $FamilyDef.PostHookKey
        $postHook  = $postInner | Where-Object { $_.command -and ($_.command -match 'wkharness-(\w+-)?post' -or $_.command -match 'CodexPostToolUseAdapter') } | Select-Object -First 1

        # INVARIANT 1-4 on the PreHook
        if ($preHook) {
            $result.PreHookFound   = $true
            $preCmd                = [string]$preHook.command
            $result.PreHookCommand = $preCmd

            if ($preCmd -notmatch $FamilyDef.PreMainPattern) {
                $result.Status = 'warn'
                $result.Issues += "INVARIANT 1: PreHook does not target $($FamilyDef.PreMainPattern); found: $preCmd"
            }
            # INVARIANT 2: retired stray hub D:\GitHub\wkharness.ps1 (NOT the -family- variants).
            if ($preCmd -match '(?i)github[\\/]+wkharness\.ps1(?![-\w])') {
                $result.Status = 'warn'
                $result.Issues += "INVARIANT 2: PreHook references retired stray D:\GitHub\wkharness.ps1 (deleted 2026-06-16); repoint to per-family main"
            }
            # INVARIANT 3: literal TAB = \t JSON-escape corruption in the path.
            if ($preCmd -match "`t") {
                $result.Status = 'warn'
                $result.Issues += "INVARIANT 3: PreHook command contains literal TAB (JSON corruption)"
            }
            # INVARIANT 4: timeout sane in the family's unit.
            if ($preHook.PSObject.Properties['timeout'] -and $preHook.timeout) {
                $t = [int]$preHook.timeout
                if ($t -lt $FamilyDef.TimeoutMin) {
                    $result.Status = 'warn'
                    $result.Issues += "INVARIANT 4: PreHook timeout $t $($FamilyDef.TimeoutUnit) too small (hook ~1.5s -> fail-open); min=$($FamilyDef.TimeoutMin)"
                }
            }
        } else {
            $result.Status = 'warn'
            $result.Issues += "PreHook ($($FamilyDef.PreHookKey)) with wkharness not found; harness DISABLED"
        }

        # INVARIANT 5 on the PostHook
        if ($postHook) {
            $result.PostHookFound   = $true
            $postCmd                = [string]$postHook.command
            $result.PostHookCommand = $postCmd
            if ($postCmd -notmatch $FamilyDef.PostPattern) {
                $result.Status = 'warn'
                $result.Issues += "INVARIANT 5: PostHook does not target $($FamilyDef.PostPattern); found: $postCmd"
            }
            # PostHook can ALSO carry the retired stray (agy currently does).
            if ($postCmd -match '(?i)github[\\/]+wkharness-post\.ps1(?![-\w])' -and $postCmd -notmatch '(?i)kih') {
                $result.Status = 'warn'
                $result.Issues += "INVARIANT 5: PostHook uses stray D:\GitHub\wkharness-post.ps1 (not the kih/per-family post)"
            }
        } else {
            $result.Status = 'warn'
            $result.Issues += "INVARIANT 5: PostHook ($($FamilyDef.PostHookKey)) not found; post-tool knowledge-ops/study-lock/nudges LOST"
        }

        return $result
    }

    $allResults = @()
    foreach ($fam in $families) { $allResults += (Test-FamilyHarnessWiring -FamilyDef $fam) }

    $okCount   = @($allResults | Where-Object { $_.Status -eq 'ok' }).Count
    $warnList  = @($allResults | Where-Object { $_.Status -ne 'ok' })
    $warnCount = $warnList.Count

    if ($warnCount -eq 0) {
        $summary = "All $($allResults.Count) families have valid harness wiring (5 invariants OK)."
        Add-Check 'family:harness-wiring' 'ok' $summary
        Emit 'ok' 'family:harness-wiring' $summary
    } else {
        $passNames = @($allResults | Where-Object { $_.Status -eq 'ok' } | ForEach-Object { $_.Family }) -join ','
        $summary = "$okCount families OK ($passNames); $warnCount families have wiring issues."
        Add-Check 'family:harness-wiring' 'warn' $summary
        Emit '!' 'family:harness-wiring' $summary
        foreach ($r in $warnList) {
            foreach ($issue in $r.Issues) {
                Add-Check "family:harness-wiring:$($r.Family)" 'warn' $issue
                Emit '!' "family:harness-wiring:$($r.Family)" $issue
            }
        }
    }

} catch {
    Add-Check 'family:harness-wiring' 'ok' "n/a (check error): $_"
    Emit 'ok' 'family:harness-wiring' 'error'
}
