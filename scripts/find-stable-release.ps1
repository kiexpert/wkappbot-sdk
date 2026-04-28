# find-stable-release.ps1
# Downloads build-launcher artifacts newest-first, runs smoke test on each,
# checks dev repo workflow health, files suggests on failure, and creates
# a GitHub Release for the first artifact that passes all checks.
#
# Usage:
#   pwsh -File scripts/find-stable-release.ps1 [-DevRepo org/private-repo] [-MaxRuns 6] [-DryRun]
#
# Secrets needed (GitHub repo settings):
#   WKAPPBOT_CORE_PAT    -- PAT with repo:read on dev repo (for workflow status check)
#   WKAPPBOT_CORE_REPO   -- private dev repo slug (e.g. "kiexpert/wkappbot-private")
#
# Suggest filing:
#   Requires D:\GitHub\WKAppBot\bin\wkappbot-core.exe on the local machine.
#   Suggests are filed to the dev repo's HQ suggest list.

param(
    [string]$DevRepo    = $env:WKAPPBOT_CORE_REPO,
    [string]$DevPAT     = $env:WKAPPBOT_CORE_PAT,
    [int]   $MaxRuns    = 6,
    [switch]$DryRun
)

Set-Location $PSScriptRoot\..

# Local fallback: if PAT not set but gh is authenticated, use its token.
# CI uses WKAPPBOT_CORE_PAT secret; locally, gh auth token covers it.
if (-not $DevPAT) {
    $ghToken = (gh auth token 2>$null)
    if ($ghToken) { $DevPAT = $ghToken; Write-Host "[local] Using gh auth token for dev repo access." }
}
if (-not $DevRepo) { $DevRepo = "kiexpert/WKAppBot" }
$repoRoot = $PWD.Path
$testDir  = "$repoRoot\.ci-test-tmp"
$devCore  = "D:\GitHub\WKAppBot\bin\wkappbot-core.exe"
$pwshExe  = if (Test-Path "C:\Program Files\PowerShell\7\pwsh.exe") {
                "C:\Program Files\PowerShell\7\pwsh.exe"
            } else {
                "powershell.exe"
            }

function Send-Suggest([string]$text, [string[]]$reqs) {
    if (!(Test-Path $devCore)) { Write-Host "[SUGGEST] dev core not found -- skipping"; return }
    $reqArgs = $reqs | ForEach-Object { "--requirement"; $_ }
    & $devCore suggest $text @reqArgs 2>&1 | Select-Object -Last 2
}

# ── 1. Check dev repo workflow health ──────────────────────────────────────
Write-Host "=== Dev repo workflow check ==="
$devWorkflowOk = $true
if ($DevRepo -and $DevPAT) {
    $savedToken    = $env:GH_TOKEN
    $env:GH_TOKEN  = $DevPAT
    $devRunsRaw    = gh run list --repo $DevRepo --workflow build.yml --limit 3 --json conclusion,displayTitle 2>$null
    $env:GH_TOKEN  = $savedToken   # restore so SDK run list below still has auth
    $devRuns = if ($devRunsRaw -and $devRunsRaw.TrimStart().StartsWith('[')) { $devRunsRaw | ConvertFrom-Json -ErrorAction SilentlyContinue } else { $null }

    if ($devRuns) {
        $lastConclusion = $devRuns[0].conclusion
        Write-Host "  Dev repo last build: $lastConclusion -- $($devRuns[0].displayTitle)"
        if ($lastConclusion -ne 'success') {
            $devWorkflowOk = $false
            Write-Host "  WARNING: dev repo build not green!"
            Send-Suggest "sdk-find-stable: dev repo build is not green ($lastConclusion). Blocking stable release candidate selection." `
                @("wkappbot skill list => wkappbot", "wkappbot file read README.md => WKAppBot", "wkappbot windows => exit 0")
        }
    } else {
        Write-Host "  Could not fetch dev repo status (no token or empty result)"
    }
} else {
    Write-Host "  DevRepo/DevPAT not set -- skipping dev workflow check"
}

# ── 2. Get recent successful SDK build runs ────────────────────────────────
Write-Host "`n=== Fetching recent build-launcher runs ==="
$rawRunsStr = gh run list --workflow build.yml --status success --limit $MaxRuns --json "databaseId,headSha,displayTitle,createdAt" 2>$null
$rawRuns = if ($rawRunsStr -and $rawRunsStr.TrimStart().StartsWith('[')) { $rawRunsStr | ConvertFrom-Json -ErrorAction SilentlyContinue } else { $null }
if (!$rawRuns) { Write-Error "No successful runs found (gh unauthenticated or no runs yet)."; exit 1 }

$stableRun = $null
$results   = @()

foreach ($run in $rawRuns) {
    $runId  = $run.databaseId
    $sha    = $run.headSha.Substring(0, 8)
    $title  = $run.displayTitle
    $date   = $run.createdAt.Substring(0, 10)
    $binDir = "$testDir\$runId"

    Write-Host "`n--- Run $runId ($date) sha=$sha`n    $title"

    # Download artifact
    New-Item -Force -ItemType Directory $binDir | Out-Null
    gh run download $runId --name "wkappbot-bin-$runId" --dir $binDir 2>&1 | Out-Null
    if (!(Test-Path "$binDir\wkappbot-core.exe")) {
        Write-Host "  SKIP: artifact expired or missing"
        $results += [pscustomobject]@{ runId=$runId; sha=$sha; title=$title; result="SKIP(expired)" }
        continue
    }

    # Seed HQ
    New-Item -Force -ItemType Directory "$binDir\wkappbot.hq\skills"   | Out-Null
    New-Item -Force -ItemType Directory "$binDir\wkappbot.hq\handlers" | Out-Null
    Copy-Item "skills\*"   "$binDir\wkappbot.hq\skills"   -Recurse -Force -ErrorAction SilentlyContinue
    Copy-Item "handlers\*" "$binDir\wkappbot.hq\handlers" -Recurse -Force -ErrorAction SilentlyContinue

    # Smoke test
    $savedPath = $env:PATH
    $env:PATH  = "$binDir;" + $env:PATH
    $env:WKAPPBOT_WORKER = "1"
    # -BasicOnly for fast release validation; run -Extended separately for deep checks
    $smokeLines = & $pwshExe -NoProfile -ExecutionPolicy Bypass -File "scripts\smoke-help.ps1" -BasicOnly 2>&1
    $code = $LASTEXITCODE
    $env:PATH = $savedPath

    $summary = ($smokeLines | Select-String "results:") | Select-Object -Last 1
    $status  = if ($code -eq 0) { "PASS" } else { "FAIL" }
    Write-Host "  Smoke: $status (exit=$code)  $summary"

    $results += [pscustomobject]@{ runId=$runId; sha=$sha; title=$title; result=$status; exit=$code }

    if ($code -ne 0) {
        $failLines = ($smokeLines | Select-String "FAIL:") -join "; "
        Send-Suggest "sdk-smoke-fail-run-${runId}: smoke test failed on artifact from run ${runId} (sha=${sha}). Failures: $failLines" `
            @("wkappbot skill list => wkappbot", "wkappbot file read README.md => WKAppBot", "wkappbot windows => exit 0")
        Write-Host "  Suggest filed for this failure."
    } elseif (!$stableRun) {
        $stableRun = @{ runId=$runId; sha=$sha; title=$title; binDir=$binDir }
        Write-Host "  *** STABLE CANDIDATE ***"
    }
}

# ── 3. Summary ────────────────────────────────────────────────────────────
Write-Host "`n========== SMOKE RESULTS =========="
$results | Format-Table runId, sha, result, title -AutoSize

if (!$stableRun) {
    Write-Host "No stable artifact found. Check suggests in dev appbot."
    exit 1
}

Write-Host "`nSTABLE: run=$($stableRun.runId) sha=$($stableRun.sha)"
Write-Host ">>> $($stableRun.title)"

if (!$devWorkflowOk) {
    Write-Host "WARNING: dev repo not green -- skipping release creation."
    exit 0
}

# ── 4. Create GitHub Release ───────────────────────────────────────────────
$version = & "$($stableRun.binDir)\wkappbot-core.exe" --version 2>$null
$version = $version -replace "wkappbot v", "" -replace "\s", ""
$tag     = "v${version}-sdk"

Write-Host "`n=== Creating release $tag ==="
if ($DryRun) { Write-Host "[DRY-RUN] Would create release $tag"; exit 0 }

# Bundle zip
$zip = "$repoRoot\wkappbot-$version.zip"
Compress-Archive -Path "$($stableRun.binDir)\wkappbot.exe",
                       "$($stableRun.binDir)\wkappbot-core.exe" `
                 -DestinationPath $zip -Force

# Tag at stable sha
git tag -f $tag $stableRun.sha 2>&1 | Out-Null
git push origin $tag --force 2>&1 | Out-Null

gh release create $tag $zip `
    --title "WKAppBot $tag" `
    --generate-notes `
    --verify-tag

Write-Host "Release created: https://github.com/$((gh repo view --json nameWithOwner -q .nameWithOwner))/releases/tag/$tag"
