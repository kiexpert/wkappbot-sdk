$ErrorActionPreference = 'Stop'
$Extensions = @('ps1','py','sh')
$FallbackRecentCount = 100
$head = (git rev-parse HEAD).Trim()
if ([string]::IsNullOrWhiteSpace($head)) { throw 'HEAD SHA is required.' }
$pattern = '\.(' + (($Extensions | ForEach-Object { [regex]::Escape($_.TrimStart('.')) }) -join '|') + ')$'
$commits = @(git rev-list --max-count $FallbackRecentCount $head)
if (!$commits) { $commits = @($head) }
$selectedCommit = $null
$files = @()
foreach ($commit in $commits) {
  $candidateFiles = @(git show --name-only --pretty='' $commit | Where-Object { $_ -match $pattern } | Sort-Object -Unique)
  if ($candidateFiles.Count -gt 0) {
    $selectedCommit = $commit
    $files = $candidateFiles
    break
  }
}
$result = [pscustomobject]@{ selectedCommit = $selectedCommit; files = @($files) }
$result | ConvertTo-Json -Depth 3 -Compress
