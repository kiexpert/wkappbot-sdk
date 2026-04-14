param(
  [string]$Before,
  [string]$After,
  [string[]]$Extensions = @('ps1','py','sh')
)
$ErrorActionPreference = 'Stop'
if ([string]::IsNullOrWhiteSpace($After)) { throw 'After SHA is required.' }
$pattern = '\.(' + (($Extensions | ForEach-Object { [regex]::Escape($_.TrimStart('.')) }) -join '|') + ')$'
$commits = @()
if (-not [string]::IsNullOrWhiteSpace($Before) -and $Before -notmatch '^0+$') {
  $commits = @(git rev-list "$Before..$After")
}
if (!$commits) { $commits = @($After) }
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
$result = [pscustomobject]@{
  selectedCommit = $selectedCommit
  files = @($files)
}
$result | ConvertTo-Json -Depth 3 -Compress
