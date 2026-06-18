# wkdoctor check: wktasklist lineage / freshness / tombstone integrity.
# wktasklist is now a SHARED, load-bearing process-state source (feeds wkagent-name model
# detection, the emergency reaper, wkjobs). This runs its rigorous self-test as an ISOLATED
# subprocess so a regression in the snapshot logic is caught here, not in production.
# Suite: WKAppBot/bin/wktasklist.tests.ps1 (skill: wktasklist-cached-process-snapshot steps 5-8).

$tlTests = 'D:\GitHub\WKAppBot\bin\wktasklist.tests.ps1'
if (-not (Test-Path $tlTests)) {
    $alt = Join-Path $binDir 'wktasklist.tests.ps1'
    if (Test-Path $alt) { $tlTests = $alt }
}

if (-not (Test-Path $tlTests)) {
    Add-Check 'wktasklist lineage self-test' 'warn' "suite not found ($tlTests)"
    Emit 'warn' 'wktasklist lineage self-test' 'suite not found'
}
else {
    try {
        $psExe = Join-Path $env:WINDIR 'System32\WindowsPowerShell\v1.0\powershell.exe'
        $out = & $psExe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $tlTests 2>&1
        $rc = $LASTEXITCODE
        $summaryLine = $out | Select-String 'self-test:' | Select-Object -Last 1
        $summary = if ($summaryLine) { $summaryLine.ToString().Trim() } else { "rc=$rc" }
        if ($rc -eq 0) {
            Add-Check 'wktasklist lineage self-test' 'ok' $summary
            Emit 'ok' 'wktasklist lineage self-test' $summary
        }
        else {
            $fails = ($out | Select-String '\[FAIL\]' | ForEach-Object { $_.ToString().Trim() }) -join '; '
            $detail = if ($fails) { "$summary :: $fails" } else { "$summary (rc=$rc)" }
            Add-Check 'wktasklist lineage self-test' 'fail' $detail
            Emit 'fail' 'wktasklist lineage self-test' $detail
        }
    }
    catch {
        Add-Check 'wktasklist lineage self-test' 'fail' $_.Exception.Message
        Emit 'fail' 'wktasklist lineage self-test' $_.Exception.Message
    }
}
