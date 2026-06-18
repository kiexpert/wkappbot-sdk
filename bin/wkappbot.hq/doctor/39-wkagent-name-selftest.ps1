# wkdoctor check: wkagent-name resolver RIGOROUS self-test (complements 18-accuracy).
# Module 18 compares the live reported model vs the authoritative session model (a soft sanity
# compare). THIS runs the resolver's own 18-assertion suite as an ISOLATED subprocess, exercising:
#   - env-var identity is IGNORED (no spoofable WKHARNESS_AGENT_MODEL backdoor),
#   - version-strip units, CreationDate parsing, CIM-failure -> UNKNOWN,
#   - LINEAGE FRESHNESS: the procMap now routes through Get-WkTaskList -EnsureChainFor, so the
#     snapshot is guaranteed to cover this process's PARENT chain (the fix for codex -> 'opus').
# FAIL-OPEN: resolver absent / unreadable => warn, never crash the doctor.
# Resolver under test: D:\GitHub\wkappbot-kih\tools\wkharness-agent-name.ps1 (-Test)

$CHECK = 'wkagent-name:selftest'
$candidates = @(
    'D:\GitHub\wkappbot-kih\tools\wkharness-agent-name.ps1'
)
$cmd = Get-Command 'wkagent-name.ps1' -ErrorAction SilentlyContinue
if ($cmd -and $cmd.Source) { $candidates += $cmd.Source }

$resolver = $candidates | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1

if (-not $resolver) {
    Add-Check $CHECK 'warn' "resolver not found (tried: $($candidates -join ', '))"
    Emit 'warn' $CHECK 'resolver not found'
    return
}

try {
    $psExe = Join-Path $env:WINDIR 'System32\WindowsPowerShell\v1.0\powershell.exe'
    $out = & $psExe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $resolver -Test 2>&1
    $rc = $LASTEXITCODE
    $summaryLine = $out | Select-String '\[\d+/\d+ passed\]' | Select-Object -Last 1
    $summary = if ($summaryLine) { $summaryLine.ToString().Trim() } else { "rc=$rc" }
    # surface the lineage-freshness result explicitly -- it is the codex->'opus' regression guard
    $chainLine = $out | Select-String 'procmap-covers-parent-chain' | Select-Object -Last 1
    $chain = if ($chainLine) { '; ' + $chainLine.ToString().Trim() } else { '' }

    if ($rc -eq 0) {
        Add-Check $CHECK 'ok' "$summary$chain"
        Emit 'ok' $CHECK "$summary$chain"
    } else {
        $fails = ($out | Select-String 'FAIL' | ForEach-Object { $_.ToString().Trim() }) -join '; '
        $detail = if ($fails) { "$summary :: $fails" } else { "$summary (rc=$rc)" }
        Add-Check $CHECK 'fail' $detail
        Emit 'fail' $CHECK $detail
    }
} catch {
    Add-Check $CHECK 'fail' $_.Exception.Message
    Emit 'fail' $CHECK $_.Exception.Message
}
