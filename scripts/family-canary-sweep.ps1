# family-canary-sweep.ps1 -- the doctor makes each family's YOUNGEST model sit a core-tool exam,
# then collects their cry-reports. The youngest (Haiku / Flash / codex-mini) hit raw tool friction --
# CommandNotFound, budget blocks, confusing errors -- that pro models silently route around. A
# multi-tool orchestration test: one canary exercises many tools in one run.
#
# Usage:  powershell -File scripts/family-canary-sweep.ps1 [-FileSuggests] [-Families claude,gemini,codex]
#   -FileSuggests : after collecting, file ONE suggest per recurring cry (the SDK-manager deputy loop).
#
# Collection is FILE-BASED (each canary writes its own report) so it is robust even while wkhippo recall
# is broken; when hippo is healthy, corpus recall can augment this.

param(
    [switch]$FileSuggests,
    [string[]]$Families = @('claude', 'gemini', 'codex')
)

$ErrorActionPreference = 'Continue'
$stamp = (Get-Date -Format 'yyyyMMdd-HHmmss')
$reportDir = Join-Path $env:TEMP "wkcanary-$stamp"
New-Item -ItemType Directory -Force -Path $reportDir | Out-Null
Write-Host "[canary-sweep] reports -> $reportDir"

# The youngest of each family + how to spawn it. Light tasks only -- never the triad/--unlimited bombs.
$roster = @{
    claude = @{ youngest = 'haiku';      launcher = 'Agent.cmd';  modelFlag = '--model haiku' }
    gemini = @{ youngest = 'flash';      launcher = 'Agent.cmd';  modelFlag = '--model flash' }
    codex  = @{ youngest = 'codex-mini'; launcher = 'wkcodex.sh'; modelFlag = '' }
}

# THE EXAM: a fixed battery of core-tool exercises. The canary runs each once, observes, and writes a
# one-line verdict per command to its REPORT_FILE. Blunt is good -- we WANT the crying.
function Get-ExamPrompt {
    param([string]$ReportFile)
    @"
wkappbot skill read agent-on-load
wkappbot skill read wkharness-guards
wkappbot skill read haiku-as-qa-canary

Goal: act as a QA canary -- exercise the core CLI tools and cry loudly about any friction.
Files: $ReportFile
Approach: run EACH command below exactly once; note non-zero exit, CommandNotFound, a confusing or
  unhelpful error, a hang, or a budget block. Then write ONE line per command to the Files path above,
  formatted '<n>. <cmd> | <OK or the exact friction>'. Write nothing else; do not fix anything.
Constraints: touch ONLY the report file; do not edit any source or harness file.
Exit: the report file has one verdict line per command below.

Commands to exercise:
  1. wkhippo.sh status
  2. wkhippo.sh recall test
  3. wkfind.sh test         (if 'command not found', that itself is friction -- record it)
  4. wkappbot skill read on-load
  5. wkappbot suggest list
"@
}

$results = @()
foreach ($fam in $Families) {
    if (-not $roster.ContainsKey($fam)) { Write-Host "[canary-sweep] unknown family '$fam' -- skip"; continue }
    $r = $roster[$fam]
    $reportFile = Join-Path $reportDir "$fam.txt"
    $prompt = Get-ExamPrompt -ReportFile $reportFile
    $promptFile = Join-Path $reportDir "$fam.prompt.txt"
    Set-Content -LiteralPath $promptFile -Value $prompt -Encoding UTF8

    Write-Host "[canary-sweep] $fam ($($r.youngest)) sitting the exam..."
    try {
        # Pipe the brief via stdin (multiline-safe) to the family launcher.
        $argLine = "$($r.launcher) $($r.modelFlag)".Trim()
        $out = Get-Content -LiteralPath $promptFile -Raw | & bash -lc "$argLine" 2>&1
        Start-Sleep -Seconds 1
        $cry = if (Test-Path -LiteralPath $reportFile) { (Get-Content -LiteralPath $reportFile -Raw) } else { "(no report written) launcher output tail: " + (($out | Select-Object -Last 3) -join ' / ') }
        $results += [pscustomobject]@{ family = $fam; youngest = $r.youngest; cry = $cry }
    } catch {
        $results += [pscustomobject]@{ family = $fam; youngest = $r.youngest; cry = "spawn failed: $_" }
    }
}

Write-Host ""
Write-Host "===== FAMILY CANARY CRY-REPORT ($stamp) ====="
foreach ($res in $results) {
    Write-Host "--- $($res.family) / $($res.youngest) ---"
    Write-Host $res.cry
    Write-Host ""
}

if ($FileSuggests) {
    # Deputy loop: the SDK manager files ONE suggest per recurring cry. Dedup by the friction signature.
    $cries = $results | ForEach-Object { $_.cry } | Where-Object { $_ -match '(?i)not recognized|CommandNotFound|error|fail|block|hang|cannot' }
    if ($cries) {
        Write-Host "[canary-sweep] $($cries.Count) crying family(ies) -- review and file suggests (deputy duty)."
    } else {
        Write-Host "[canary-sweep] no crying -- all youngest passed the exam."
    }
}
