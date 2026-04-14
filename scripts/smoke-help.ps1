$ErrorActionPreference = 'Stop'
$project = 'csharp/src/WKAppBot.CLI'
$commands = @(
  @('--help'), @('help'), @('version'),
  @('a11y','--help'), @('run','--help'), @('find','--help'), @('inspect','--help'), @('windows','--help'),
  @('ocr','--help'), @('capture','--help'), @('scan','--help'), @('ask','--help'), @('agent','--help'),
  @('model','--help'), @('logcat','--help'), @('eye','--help'), @('slack','--help'), @('web','--help'),
  @('file','--help'), @('code','--help'), @('vscode','--help'), @('file-edit','--help'), @('file-open','--help'),
  @('file-read','--help'), @('file-grep','--help'), @('file-glob','--help'), @('mcp','--help'), @('do','--help'),
  @('input','--help'), @('click','--help'), @('dismiss','--help'), @('win-click','--help'), @('dialog-click','--help'),
  @('tab-select','--help'), @('cond-add','--help'), @('focus','--help'), @('watch','--help'), @('snapshot','--help'),
  @('readiness','--help'), @('uia-test','--help'), @('form-dump','--help'), @('toolbar-ocr','--help'),
  @('titlebar','--help'), @('validate','--help'), @('chart-analyze','--help'), @('hts-stress','--help'),
  @('tooltip-probe','--help'), @('speak','--help'), @('whisper','--help'), @('newchat','--help'),
  @('analyze-hack','--help'), @('screensaver','--help'), @('whisper-ring','--help'), @('prompt','--help'),
  @('schedule','--help'), @('dashboard','--help'), @('knowhow','--help'), @('skill','--help'), @('win-move','--help'),
  @('screen','--help'), @('clipboard','--help'), @('claude-usage','--help'), @('enc-test','--help'),
  @('suggest','--help'), @('gc','--help'), @('hotswap','--help'), @('zoom-demo','--help'), @('tick','--help'),
  @('kiwoom','--help'), @('com','--help'), @('telegram','--help'), @('stock-scan','--help'), @('tree-select','--help'),
  @('prompt-test','--help'), @('prompt-probe','--help'), @('claude-detect','--help'), @('find-prompts','--help'),
  @('msaa-probe','--help')
)
$fail = 0
foreach ($cmd in $commands) {
  $pretty = ($cmd -join ' ')
  Write-Host "== HELP $pretty =="
  & dotnet run --project $project -- @cmd
  if ($LASTEXITCODE -ne 0) {
    Write-Host "FAILED: $pretty"
    $fail = 1
  }
}
if ($fail -ne 0) { throw 'help smoke failed' }
Write-Host 'All help smoke checks passed.'
