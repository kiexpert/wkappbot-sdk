# wkdoctor check: git-integrity -- verify git metadata health for key repos.
# Detects: HEAD/config missing while objects intact (partial corruption), or full corruption.
# Safe auto-fix: recreate .git/HEAD = 'ref: refs/heads/main' if missing and objects exist.
# Does NOT fabricate config (could cause commits to wrong identity). FAIL-OPEN.

try {
    $repos = @('D:\GitHub\wkappbot-sdk', 'D:\GitHub\wkappbot-kih')
    $issues = @(); $healthy = @()
    foreach ($repo in $repos) {
        if (-not (Test-Path -LiteralPath $repo)) { continue }
        try {
            $result = & git -C $repo rev-parse --show-toplevel 2>&1
            if ($LASTEXITCODE -eq 0) {
                $healthy += $repo
            } else {
                $gitDir = Join-Path $repo '.git'
                $objDir = Join-Path $gitDir 'objects'
                $hasObjects = (Test-Path -LiteralPath $objDir) -and ((Get-ChildItem -LiteralPath $objDir -Recurse -File -ErrorAction SilentlyContinue | Select-Object -First 1) -ne $null)
                if ($hasObjects) {
                    Emit '!' 'git-integrity' "$repo git metadata damaged but objects intact -- recoverable"
                    $issues += $repo
                    $headFile = Join-Path $gitDir 'HEAD'
                    if (-not (Test-Path -LiteralPath $headFile)) {
                        try {
                            [IO.File]::WriteAllText($headFile, "ref: refs/heads/main`n", (New-Object System.Text.UTF8Encoding($false)))
                            Emit 'ok' 'git-integrity:fixed' "$repo .git/HEAD recreated (ref: refs/heads/main)"
                            $result2 = & git -C $repo rev-parse --show-toplevel 2>&1
                            if ($LASTEXITCODE -eq 0) {
                                Emit 'ok' 'git-integrity:verified' "$repo git now functional after HEAD recreate"
                                $issues = $issues | Where-Object { $_ -ne $repo }
                                $healthy += $repo
                            }
                        } catch {
                            Emit '!' 'git-integrity:fix' "$repo HEAD recreate failed: $_"
                        }
                    } else {
                        Emit '!' 'git-integrity' "$repo .git/HEAD exists but git still fails -- manual recovery needed"
                    }
                } else {
                    Emit '!' 'git-integrity' "$repo .git damaged with no objects -- cannot auto-recover, reclone needed"
                    $issues += $repo
                }
            }
        } catch {
            Emit 'ok' 'git-integrity' "n/a ($repo check error, fail-open): $_"
        }
    }
    if ($issues.Count -eq 0) {
        $detail = if ($healthy.Count -gt 0) { "all repos healthy: $($healthy -join ', ')" } else { 'no repos present on this machine' }
        Add-Check 'git-integrity' 'ok' $detail
        Emit 'ok' 'git-integrity' $detail
    } else {
        Add-Check 'git-integrity' 'warn' "$($issues.Count) repo(s) with git integrity issues: $($issues -join ', ')"
    }
} catch {
    Add-Check 'git-integrity' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'git-integrity' 'n/a (check error, fail-open)'
}
