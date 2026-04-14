$ErrorActionPreference = 'Stop'
$project = 'csharp/src/WKAppBot.CLI'
$helpCommands = @(
  @('--help'), @('help'), @('version'),
  @('skill','--help'), @('knowhow','--help'), @('schedule','--help'),
  @('file','--help'), @('file-read','--help'), @('file-grep','--help'), @('file-glob','--help'),
  @('web','--help'), @('ask','--help'), @('agent','--help'), @('mcp','--help'),
  @('a11y','--help'), @('inspect','--help'), @('find','--help'), @('windows','--help'),
  @('scan','--help'), @('capture','--help'), @('ocr','--help'), @('input','--help'),
  @('click','--help'), @('dismiss','--help'), @('dialog-click','--help'), @('watch','--help'),
  @('focus','--help'), @('snapshot','--help'), @('uia-test','--help'), @('validate','--help'),
  @('chart-analyze','--help'), @('tooltip-probe','--help'), @('whisper','--help'), @('slack','--help')
)
$fail = 0
function Run-Cmd([string[]]$cmd) {
  $pretty = ($cmd -join ' ')
  Write-Host "== TEST $pretty =="
  & dotnet run --project $project -- @cmd
  if ($LASTEXITCODE -ne 0) {
    Write-Host "FAILED: $pretty"
    $script:fail = 1
  }
}
foreach ($cmd in $helpCommands) { Run-Cmd $cmd }
Run-Cmd @('skill','list')
Run-Cmd @('schedule','list')
$smokeDir = Join-Path $PWD 'smoke-temp'
New-Item -ItemType Directory -Force -Path $smokeDir | Out-Null
$sampleFile = Join-Path $smokeDir 'sample.txt'
Set-Content -Path $sampleFile -Value @('alpha','beta','skill smoke') -Encoding UTF8
Run-Cmd @('file','read',$sampleFile)
Run-Cmd @('file','grep','skill',$sampleFile)
Run-Cmd @('file','glob','**/*.txt','--path',$smokeDir)
if ($fail -ne 0) { throw 'help/basic smoke failed' }
Write-Host 'All help/basic smoke checks passed.'
