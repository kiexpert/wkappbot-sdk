# wktaskkill.ps1 -- smart taskkill: classify + auto-kill ZOMBIE only (UNKNOWN=PROTECTED by design)
# Usage: ./wktaskkill.ps1 /IM wkappbot-core.exe [extra args]
# Source: TaskkillCompatCommand.Classify.cs -- UNKNOWN defaults to PROTECTED (never auto-kill)
param([Parameter(ValueFromRemainingArguments)][string[]]$TkArgs)
$out = & wkappbot taskkill @TkArgs 2>&1
$out | ForEach-Object { Write-Host $_ }
# Parse the generated --force command from classify output (most reliable path)
$forceLine = $out | Where-Object { $_ -match "wkappbot taskkill --force ([\d,]+)\s+# all zombies" }
if ($forceLine -and $Matches[1]) {
    $pids = $Matches[1]
    Write-Host "[wktaskkill] auto-killing zombies: $pids"
    & wkappbot taskkill --force $pids 2>&1 | ForEach-Object { Write-Host $_ }
}