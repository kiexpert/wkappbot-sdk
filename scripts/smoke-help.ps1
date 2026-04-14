$ErrorActionPreference = 'Stop'
$repoBin = Join-Path $PWD 'bin'
if (!(Test-Path (Join-Path $repoBin 'wkappbot.exe'))) { throw 'bin/wkappbot.exe missing' }
$env:PATH = "$repoBin;$env:PATH"
$env:WKAPPBOT_WORKER = '1'
$helpCommands = @(
  @('--help'), @('help'), @('version'),
  @('skill','--help','--no-regression'), @('knowhow','--help','--no-regression'), @('schedule','--help','--no-regression'),
  @('file','--help','--no-regression'), @('file-read','--help','--no-regression'), @('file-grep','--help','--no-regression'), @('file-glob','--help','--no-regression'),
  @('web','--help','--no-regression'), @('ask','--help','--no-regression'), @('agent','--help','--no-regression'),
  @('a11y','--help','--no-regression'), @('inspect','--help','--no-regression'), @('find','--help','--no-regression'), @('windows','--help','--no-regression'),
  @('scan','--help','--no-regression'), @('capture','--help','--no-regression'), @('ocr','--help','--no-regression'), @('input','--help','--no-regression'),
  @('click','--help','--no-regression'), @('dismiss','--help','--no-regression'), @('dialog-click','--help','--no-regression'), @('watch','--help','--no-regression'),
  @('focus','--help','--no-regression'), @('snapshot','--help','--no-regression'), @('uia-test','--help','--no-regression'), @('validate','--help','--no-regression'),
  @('chart-analyze','--help','--no-regression'), @('tooltip-probe','--help','--no-regression'), @('whisper','--help','--no-regression'), @('slack','--help','--no-regression')
)
$fail = 0
function Run-Cmd([string[]]$cmd) {
  $pretty = ($cmd -join ' ')
  Write-Host "== TEST $pretty =="
  & wkappbot @cmd
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
