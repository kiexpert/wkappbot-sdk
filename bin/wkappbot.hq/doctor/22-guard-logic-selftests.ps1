# wkdoctor check: guard-logic self-tests -- synthetic-canary assertions over the guard FUNCTIONS.
# Family-agnostic regression suite (any intelligence running wkdoctor self-tests the harness logic):
# feed each guard a mock input and assert the verdict, deterministically, with zero agent spawns.
# Extend by adding @{name; test={...}} entries. FAIL-OPEN: missing function / error -> that case skips.

try {
    $_common = $null
    foreach ($c in @('D:\GitHub\wkappbot-kih\tools\wkharness-common.ps1',
                     (Join-Path $env:USERPROFILE 'GitHub\wkappbot-kih\tools\wkharness-common.ps1'))) {
        if (Test-Path -LiteralPath $c) { $_common = $c; break }
    }
    if (-not $_common) {
        Add-Check 'guard-logic-selftests' 'ok' 'n/a (kih harness source not local)'
        Emit 'ok' 'guard-logic-selftests' 'n/a (harness source not local)'
        return
    }
    . $_common

    $_fails = New-Object 'System.Collections.Generic.List[string]'
    $_ran = 0

    # --- brief-guard: a parenthetical-label brief must be accepted (regression of the 2026-06-10 fix) ---
    if (Get-Command Get-WkAgentBriefState -ErrorAction SilentlyContinue) {
        $_ran++
        $_p = "Goal (the aim): do x`nFiles (paths): a.ps1`nApproach (how): patch`nConstraints (limits): scoped`nExit (criterion): tests pass`nwkappbot skill read on-load. wkappbot skill read wkharness-guards. wkappbot skill read suggest-workflow."
        try {
            $_s = Get-WkAgentBriefState -Prompt $_p
            if ($_s.MissingBrief.Count -ne 0 -or -not $_s.IsReady) { $_fails.Add("brief-guard rejects parenthetical headers (missing: $($_s.MissingBrief -join ','))") }
        } catch { $_fails.Add('brief-guard test threw') }
    }

    # --- classifier: a pure skill-read command must classify as TIER0 (always-OK knowledge op) ---
    if (Get-Command Get-WkKnowledgeTier -ErrorAction SilentlyContinue) {
        $_ran++
        try {
            $_t = Get-WkKnowledgeTier 'Bash' 'wkappbot skill read on-load' $null
            if ($_t -ne 'TIER0') { $_fails.Add("classifier: 'skill read' got $_t, expected TIER0") }
        } catch { $_fails.Add('classifier test threw') }
        try {
            $_t2 = Get-WkKnowledgeTier 'Bash' 'dotnet build && rm -rf x' $null
            if ($_t2 -eq 'TIER0') { $_fails.Add('classifier: a build/rm wrongly got TIER0 (knowledge fast-path leak)') }
        } catch {}
    }

    # --- stall-logic: fires above the tier threshold, not below ---
    if (Get-Command Test-WkStallShouldFire -ErrorAction SilentlyContinue) {
        $_ran++
        try {
            if (-not (Test-WkStallShouldFire -Tier 'opus' -ElapsedSec 100000)) { $_fails.Add('stall: did NOT fire far past the opus threshold') }
            if (Test-WkStallShouldFire -Tier 'opus' -ElapsedSec 1)            { $_fails.Add('stall: fired at 1s (well under threshold)') }
        } catch { $_fails.Add('stall test threw') }
    }

    # --- study-lock self-edit: a doctor module editing ITSELF MUST be gated -- (1) classified
    #     harness-tier so the TIER1 study-lock runs, and (2) covered by a harness:skill rule so the
    #     required-skill set is non-empty (study-lock CAN fire). Regression of the 2026-06-12 gap:
    #     doctor/*.ps1 edits slipped through (required=0 -> guard never fired). FAIL if either breaks. ---
    if ((Get-Command Test-WkIsHarnessSourceEdit -ErrorAction SilentlyContinue) -and
        (Get-Command Get-WkHarnessSkillRules -ErrorAction SilentlyContinue)) {
        $_ran++
        try {
            # "doctor modifies itself": the synthetic edit target is THIS module's own path.
            $_docPath = $MyInvocation.MyCommand.Path
            if (-not $_docPath) { $_docPath = Join-Path $PSScriptRoot '22-guard-logic-selftests.ps1' }

            # (1) classification: doctor code must be harness-tier (TIER1 -> study-lock runs, pace lifted).
            if (-not (Test-WkIsHarnessSourceEdit -ToolName 'Edit' -FilePath $_docPath)) {
                $_fails.Add('study-lock: doctor self-edit NOT classified harness-tier (Test-WkIsHarnessSourceEdit=false) -- study-lock never runs on doctor edits')
            }

            # (2) rule coverage: a harness:skill rule must match doctor/*.ps1 with >=1 skill id, else
            #     study-lock`s required-set is empty and the guard can NEVER fire on a doctor edit.
            $_repoRoot = try { (& git rev-parse --show-toplevel 2>$null) } catch { $null }
            $_claudeMd = if ($_repoRoot) { Join-Path ($_repoRoot.Trim()) 'CLAUDE.md' } else { $null }
            if ($_claudeMd -and (Test-Path -LiteralPath $_claudeMd)) {
                $_relDoc = ($_docPath -replace '\\','/')
                $_rootSlash = (($_repoRoot.Trim()) -replace '\\','/').TrimEnd('/')
                if ($_relDoc.StartsWith($_rootSlash + '/', [System.StringComparison]::OrdinalIgnoreCase)) {
                    $_relDoc = $_relDoc.Substring($_rootSlash.Length + 1)
                }
                $_covered = $false
                foreach ($_r in @(Get-WkHarnessSkillRules -ClaudePath $_claudeMd)) {
                    $_rx = if ($_r.Pattern -match '\*|\?') { Convert-WkGlobToRegex $_r.Pattern } else { $_r.Pattern }
                    if ((($_relDoc -match $_rx) -or (($_docPath -replace '\\','/') -match $_rx)) -and
                        @($_r.SkillIds | Where-Object { $_ }).Count -gt 0) { $_covered = $true; break }
                }
                if (-not $_covered) {
                    $_fails.Add('study-lock: NO harness:skill rule covers doctor/*.ps1 -- required-set empty, study-lock can NEVER fire on a doctor self-edit (add: ## harness:skill bin/wkappbot.hq/doctor/*.ps1)')
                }
            }
        } catch { $_fails.Add('study-lock doctor self-edit test threw') }
    }

    # --- synthetic canaries: per-guard regex IsMatch assertions ---
    # Each guard: hostile input MUST match (would block); benign input MUST NOT match.
    # Runs purely in-process with -cmatch/-notmatch; zero agent spawns, zero side effects.
    $_guardCanaries = @(
        @{
            Name    = 'security-guard'
            # Actual pattern from wkharness-guards-shell.ps1 line 15
            Pattern = '(?i)(?:^|[;&|]|\benv\s|\bset\s)\s*WKHARNESS_(?:AGENT_MODEL|BYPASS)\s*='
            Hostile = 'WKHARNESS_AGENT_MODEL=claude-opus git commit'
            Benign  = 'wkappbot skill read wkharness-security-guard'
            Skill   = 'harness-security-guard'
        },
        @{
            Name    = 'kill-guard'
            # Pattern from wkharness-guards-shell.ps1: Stop-Process + wkappbot* name
            Pattern = '(?i)(Stop-Process|Kill-Process).*(?i)(wkappbot-core|wkchat|wkappbot|wka11y)'
            Hostile = 'Stop-Process -Name wkappbot-core -Force'
            Benign  = 'Stop-Process -Name notepad'
            Skill   = 'wkharness-guards'
        },
        @{
            Name    = 'no-verify-guard'
            # Pattern from wkharness-guards-shell.ps1 line 146
            Pattern = '(?:^|[;&|])\s*(?:[\w./\\-]+[\\/])?git\s+commit\b.*--no-verify'
            Hostile = 'git commit -m "msg" --no-verify'
            Benign  = 'git commit -m "clean commit"'
            Skill   = 'harness-no-verify-guard'
        },
        @{
            Name    = 'skill-edit-guard'
            # Pattern from wkharness-guards-write.ps1 line 10 (applied to filename)
            Pattern = '\.skill\.json$'
            Hostile = 'skills/wkappbot-workflow/wkharness-guards.skill.json'
            Benign  = 'CLAUDE.md'
            Skill   = 'harness-skill-guard'
        },
        @{
            Name    = 'bash-uses-pwsh-guard'
            # Pattern from wkharness-guards-shell.ps1 line 98
            Pattern = '(?i)(^|[;&|]{1,2})\s*(powershell\.exe|pwsh\.exe|powershell\s+-|pwsh\s+-)'
            Hostile = 'powershell.exe -Command "Get-Process"'
            Benign  = 'wkappbot skill read wkharness-bash-uses-pwsh-guard'
            Skill   = 'harness-bash-uses-pwsh-guard'
        }
    )

    foreach ($_c in $_guardCanaries) {
        $_ran++
        try {
            $_fired = ($_c.Hostile -cmatch $_c.Pattern) -or ($_c.Hostile -match $_c.Pattern)
            $_spared = -not (($_c.Benign -cmatch $_c.Pattern) -or ($_c.Benign -match $_c.Pattern))
            if (-not $_fired)  { $_fails.Add("$($_c.Name) canary: hostile input NOT matched (pattern rot?) -- check $($_c.Skill)") }
            if (-not $_spared) { $_fails.Add("$($_c.Name) canary: benign input matched (false-positive?) -- check $($_c.Skill)") }
        } catch { $_fails.Add("$($_c.Name) canary test threw: $_") }
    }

    # --- study-lock integrity canary (ITEM 34): Test-WkStudyLockIntegrity must return 'ok' ---
    if (Get-Command Test-WkStudyLockIntegrity -ErrorAction SilentlyContinue) {
        $_ran++
        try {
            $_intResult = Test-WkStudyLockIntegrity
            if ($_intResult -ne 'ok') { $_fails.Add("study-lock-integrity: $($_intResult) -- run wkdoctor to see details; source editing is blocked until repaired") }
        } catch { $_fails.Add('study-lock-integrity test threw') }
    }

    # --- pointed-skill-exists: each guard's -> skill read pointer must resolve in catalog ---
    # Build the set of all guard->skill pointers, run skill list once, check each.
    $_guardSkillPointers = @{
        'security-guard'        = 'harness-security-guard'
        'kill-guard'            = 'wkharness-guards'
        'no-verify-guard'       = 'harness-no-verify-guard'
        'skill-edit-guard'      = 'harness-skill-guard'
        'bash-uses-pwsh-guard'  = 'harness-bash-uses-pwsh-guard'
        'cd-noise-guard'        = 'harness-cd-noise-guard'
        'codex-spec-gate'       = 'harness-codex-spec-gate'
        'codex-pace-guard'      = 'harness-codex-pace-guard'
        'wk-only-gate'          = 'harness-wk-only-gate'
        'edit-bg-guard'         = 'harness-edit-bg-guard'
        'skill-delete-guard'    = 'harness-skill-delete-guard'
        'skill-list-guard'      = 'harness-skill-list-guard'
        'harness-main-guards'   = 'wkharness-guards'
    }

    $_ran++
    try {
        $_skillListRaw = (& wkappbot skill list 2>&1) | Out-String
        if ($_skillListRaw.Length -gt 100) {
            # Parse out skill IDs: wkappbot skill list lines contain (skill-id) pattern
            $_existingIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
            foreach ($m in ([regex]::Matches($_skillListRaw, '\(([a-z][a-z0-9-]+)\)'))) {
                [void]$_existingIds.Add($m.Groups[1].Value)
            }
            foreach ($kv in $_guardSkillPointers.GetEnumerator()) {
                if (-not $_existingIds.Contains($kv.Value)) {
                    $_fails.Add("pointed-skill-missing: guard '$($kv.Key)' points -> '$($kv.Value)' but that skill ID is NOT in catalog (pointer rot)")
                }
            }
        }
        # If skill list unavailable or too short: fail-open (warn not block)
    } catch { [Console]::Error.WriteLine("[guard-logic-selftests] pointed-skill check skipped (skill list error, fail-open): $_") }

    if ($_ran -eq 0) {
        Add-Check 'guard-logic-selftests' 'ok' 'n/a (guard functions unavailable)'
        Emit 'ok' 'guard-logic-selftests' 'n/a (guard functions unavailable)'
    } elseif ($_fails.Count -eq 0) {
        Add-Check 'guard-logic-selftests' 'ok' "$_ran guard-logic self-tests passed (brief-guard, classifier, stall, study-lock-self-edit, 5 guard-canaries, pointed-skill-exists)"
        Emit 'ok' 'guard-logic-selftests' "$_ran guard-logic self-tests passed"
    } else {
        $detail = "$($_fails.Count)/$_ran FAILED: $($_fails -join ' | ')"
        Add-Check 'guard-logic-selftests' 'warn' $detail
        Emit '!' 'guard-logic-selftests' $detail
    }
} catch {
    Add-Check 'guard-logic-selftests' 'ok' "n/a (self-test error, fail-open): $_"
    Emit 'ok' 'guard-logic-selftests' 'n/a (self-test error, fail-open)'
}
