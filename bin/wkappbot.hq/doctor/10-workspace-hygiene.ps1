# Workspace Hygiene Check
# Detects locks and large files that slow down the agent

$repoRoot = Split-Path -Parent $binDir

# 1. Git Locks
$gitLock = Join-Path $repoRoot ".git/index.lock"
if (Test-Path $gitLock) {
    Add-Check "git-lock" "warn" "Found .git/index.lock -- may block git operations"
    Emit "!" "git-lock" "Found .git/index.lock"
} else {
    Add-Check "git-lock" "ok" "No stale git locks found"
}

# 2. Large Context Files
# Check current dir and depth 2 to avoid deep traversal but catch main files
$largeFiles = Get-ChildItem -Path $repoRoot -File -Recurse -Depth 2 -ErrorAction SilentlyContinue | Where-Object { $_.Length -gt 5MB }
if ($largeFiles) {
    $fileNames = ($largeFiles | Select-Object -First 3 | ForEach-Object { "$($_.Name) ($([int]($_.Length/1MB))MB)" }) -join ", "
    if ($largeFiles.Count -gt 3) { $fileNames += " ..." }
    Add-Check "large-files" "warn" "Found $($largeFiles.Count) large files (>5MB): $fileNames"
    Emit "!" "large-files" "Large files detected in workspace"
} else {
    Add-Check "large-files" "ok" "No excessively large files found"
}

# 3. Temp/Debug files
$staleFound = $false
foreach ($p in @("npm-debug.log", "yarn-error.log", "pip-log.txt")) {
    $f = Join-Path $repoRoot $p
    if (Test-Path $f) {
        Add-Check "stale-logs" "warn" "Found stale error log: $p"
        Emit "!" "stale-logs" "Stale error logs found: $p"
        $staleFound = $true
    }
}
if (-not $staleFound) {
    Add-Check "stale-logs" "ok" "No common stale logs found"
}
