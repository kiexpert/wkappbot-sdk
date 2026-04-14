param(
  [string]$Before,
  [string]$After,
  [string[]]$Extensions = @('ps1','py','sh')
)
$ErrorActionPreference = 'Stop'
function Get-ScriptFiles([string]$BeforeSha,[string]$AfterSha,[string[]]$Exts) {
  if ([string]::IsNullOrWhiteSpace($AfterSha)) { throw 'After SHA is required.' }
  $pattern = '\.(' + (($Exts | ForEach-Object { [regex]::Escape($_.TrimStart('.')) }) -join '|') + ')$'
  $commits = @()
  if (-not [string]::IsNullOrWhiteSpace($BeforeSha) -and $BeforeSha -notmatch '^0+$') {
    $commits = @(git rev-list "$BeforeSha..$AfterSha")
  }
  if (!$commits) { $commits = @($AfterSha) }
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
  [pscustomobject]@{
    selectedCommit = $selectedCommit
    files = @($files)
  }
}
if ([string]::IsNullOrWhiteSpace($After)) {
  Write-Host 'Usage: pwsh -File scripts/get-latest-script-files.ps1 -Before <sha> -After <sha>'
  $head = (git rev-parse HEAD).Trim()
  $beforeHead = ''
  try { $beforeHead = (git rev-parse HEAD~1 2>$null).Trim() } catch { $beforeHead = '' }
  $self = Get-ScriptFiles -BeforeSha $beforeHead -AfterSha $head -Exts $Extensions
  Write-Host "Self-test OK. selectedCommit=$($self.selectedCommit) files=$(@($self.files).Count)"
  $self | ConvertTo-Json -Depth 3 -Compress
  exit 0
}
$result = Get-ScriptFiles -BeforeSha $Before -AfterSha $After -Exts $Extensions
$result | ConvertTo-Json -Depth 3 -Compress
