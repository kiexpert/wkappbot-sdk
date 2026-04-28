$ErrorActionPreference = 'Stop'
$Extensions = @('ps1','py','sh')
# Exclude GUI/integration scripts requiring a running display or browser
$ExcludePathPrefixes = @('experience/')
# Exclude scripts that require live GitHub auth, network artifacts, or external CI context
$ExcludeFiles = @('scripts/find-stable-release.ps1')
$FallbackRecentCount = 100
$head = (git rev-parse HEAD).Trim()
if ([string]::IsNullOrWhiteSpace($head)) { throw 'HEAD SHA is required.' }
$pattern = '\.(' + (($Extensions | ForEach-Object { [regex]::Escape($_.TrimStart('.')) }) -join '|') + ')$'
$commits = @(git rev-list --max-count $FallbackRecentCount $head)
if (!$commits) { $commits = @($head) }
$selectedCommit = $null
$files = @()
foreach ($commit in $commits) {
  $candidateFiles = @(git show --name-only --pretty='' $commit |
    Where-Object { $_ -match $pattern } |
    Where-Object { $f = $_; -not ($ExcludePathPrefixes | Where-Object { $f -like "$_*" }) } |
    Where-Object { $f = $_; -not ($ExcludeFiles       | Where-Object { $f -eq $_ }) } |
    Sort-Object -Unique)
  if ($candidateFiles.Count -gt 0) {
    $selectedCommit = $commit
    $files = $candidateFiles
    break
  }
}
$result = [pscustomobject]@{ selectedCommit = $selectedCommit; files = @($files) }
$result | ConvertTo-Json -Depth 3 -Compress
