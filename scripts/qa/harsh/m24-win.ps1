# m24-win.ps1 -- M24: Windows non-admin, HOME with a space and Korean characters, Windows PowerShell 5.1 only.
# Run by the windows-latest leg with `shell: powershell` (5.1). When the runner is an administrator, the outer
# call creates a standard local user, copies this file and the tgz to a stage folder, and re-runs the body as that
# user with -Inner. Otherwise the body runs as the runner and the result says admin=yes. Writes qa-result-m24-win.
# Written ASCII-only (Korean is built from [char] codes) so PowerShell 5.1 reads it correctly with no BOM.
# A leg that cannot create the user or start the inner run FAILS (exit 1): it never passes vacuously.
param(
  [switch]$Inner,
  [string]$TgzDir = '',
  [string]$Version = '',
  [string]$Out = ''
)
$ErrorActionPreference = 'Continue'
$HG_LEG = 'm24-win'
$rt = $env:RUNNER_TEMP
if (-not $rt) { $rt = $env:TEMP }
if (-not $TgzDir) { $TgzDir = Join-Path $rt 'tgz' }
if (-not $Out) { $Out = Join-Path $rt 'qa-result' }
New-Item -ItemType Directory -Force -Path $Out | Out-Null
$ResFile = Join-Path $Out ('qa-result-' + $HG_LEG)
$script:fails = 0

function Res([string]$k, [string]$v) {
  Add-Content -LiteralPath $ResFile -Value ('{0}={1}' -f $k, $v) -Encoding ASCII
  Write-Output ('RES {0}={1}' -f $k, $v)
}
function Check([string]$k, [bool]$ok) {
  if ($ok) { Res $k 'pass' } else { Res $k 'fail'; $script:fails++ }
}
function Test-Admin {
  $id = [Security.Principal.WindowsIdentity]::GetCurrent()
  return ([Security.Principal.WindowsPrincipal]$id).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}
function Stop-Red([string]$why) {
  Res 'outer_error' $why
  Res 'verdict' 'FAIL'
  Write-Output ('HARSH-LEG ' + $HG_LEG + ' fails=1 (' + $why + ')')
  exit 1
}

# ---- outer: when admin, run the body as a standard local user ----
if ((-not $Inner) -and (Test-Admin)) {
  Res 'runner_admin' 'yes'
  $user = 'wkqa'
  $pw = ([guid]::NewGuid().ToString('N')).Substring(0, 10) + 'Aa1!x'
  $sec = ConvertTo-SecureString -String $pw -AsPlainText -Force
  try {
    New-LocalUser -Name $user -Password $sec -PasswordNeverExpires -ErrorAction Stop | Out-Null
    Add-LocalGroupMember -Group 'Users' -Member $user -ErrorAction Stop
    Res 'user_created' 'yes'
  } catch {
    Stop-Red ('could not create the local user: ' + $_.Exception.Message)
  }
  $stage = Join-Path $env:PUBLIC ('wkqa-harsh-' + [guid]::NewGuid().ToString('N').Substring(0, 8))
  $stageTgz = Join-Path $stage 'tgz'
  New-Item -ItemType Directory -Force -Path $stageTgz | Out-Null
  Copy-Item -LiteralPath $PSCommandPath -Destination $stage
  Copy-Item -Path (Join-Path $TgzDir '*') -Destination $stageTgz -ErrorAction SilentlyContinue
  & icacls.exe $stage /grant ($user + ':(OI)(CI)M') > $null
  & icacls.exe $Out /grant ($user + ':(OI)(CI)M') > $null
  $cred = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $user, $sec
  $psExe = Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe'
  $argList = @('-NoProfile', '-ExecutionPolicy', 'Bypass', '-File', ('"' + (Join-Path $stage 'm24-win.ps1') + '"'),
    '-Inner', '-TgzDir', ('"' + $stageTgz + '"'), '-Version', ('"' + $Version + '"'), '-Out', ('"' + $Out + '"'))
  $p = $null
  try {
    $p = Start-Process -FilePath $psExe -ArgumentList $argList -Credential $cred -WorkingDirectory $stage `
      -LoadUserProfile -Wait -PassThru -RedirectStandardOutput (Join-Path $Out 'm24-inner.out.log') `
      -RedirectStandardError (Join-Path $Out 'm24-inner.err.log') -ErrorAction Stop
  } catch {
    Stop-Red ('could not start the inner run as the standard user: ' + $_.Exception.Message)
  }
  if (-not $p) { Stop-Red 'the inner run returned no process' }
  Res 'inner_exit' $p.ExitCode
  foreach ($lf in @('m24-inner.out.log', 'm24-inner.err.log')) {
    $lp = Join-Path $Out $lf
    if (Test-Path -LiteralPath $lp) { Get-Content -LiteralPath $lp -Tail 60 | ForEach-Object { Write-Output ('INNER| ' + $_) } }
  }
  exit $p.ExitCode
}

# ---- body: runs as -Inner (standard user) or as the runner when not an administrator ----
$adminTxt = 'no'
if (Test-Admin) { $adminTxt = 'yes' }
Res 'leg' $HG_LEG
Res 'identity_admin' $adminTxt
Res 'ps_version' $PSVersionTable.PSVersion.ToString()
Check 'powershell_5_1_only' ($PSVersionTable.PSVersion.Major -eq 5)

# Git Bash off PATH: the harness must not lean on bash here
$bashTxt = 'no'
if (Get-Command bash.exe -ErrorAction SilentlyContinue) { $bashTxt = 'yes' }
Res 'bash_on_path' $bashTxt
$env:Path = (($env:Path -split ';') | Where-Object { $_ -and ($_ -notmatch '\\Git\\') }) -join ';'

# HOME and USERPROFILE with a space and a Korean syllable (U+C9C0 is the syllable jip)
$base = $env:TEMP
if (-not $base) { $base = $rt }
$homeDir = Join-Path $base ('wk home ' + [char]0xC9C0 + ' qa')
New-Item -ItemType Directory -Force -Path $homeDir | Out-Null
$env:HOME = $homeDir
$env:USERPROFILE = $homeDir
$env:npm_config_prefix = Join-Path $homeDir 'npm-prefix'
$env:npm_config_cache = Join-Path $homeDir 'npm-cache'
New-Item -ItemType Directory -Force -Path $env:npm_config_prefix | Out-Null
$env:Path = $env:npm_config_prefix + ';' + $env:Path
$spaceKorean = ($homeDir.Contains(' ') -and ($homeDir.IndexOf([char]0xC9C0) -ge 0))
Check 'home_space_and_korean' $spaceKorean

# package: packed tgz when present, else the registry
$tgz = Get-ChildItem -LiteralPath $TgzDir -Filter '*.tgz' -ErrorAction SilentlyContinue | Select-Object -First 1
$srcKind = 'registry'
$src = 'wkappbot-code'
if ($Version) { $src = 'wkappbot-code@' + $Version }
if ($tgz) { $srcKind = 'tgz'; $src = $tgz.FullName }
Res 'install_source' $srcKind

$npmOut = & npm.cmd i -g $src 2>&1
$npmRc = $LASTEXITCODE
$npmOut | Out-File -FilePath (Join-Path $Out 'm24-npm.log') -Encoding UTF8
Res 'npm_global_rc' $npmRc
Check 'npm_global_install' ($npmRc -eq 0)

$installOut = & wkappbot-code.cmd install 2>&1
$installRc = $LASTEXITCODE
$installOut | Out-File -FilePath (Join-Path $Out 'm24-install.log') -Encoding UTF8
Res 'install_rc' $installRc
Check 'wkappbot_code_install' ($installRc -eq 0)
@($installOut) | Select-Object -Last 25 | ForEach-Object { Write-Output ('INSTALL| ' + $_) }

# seven symbols in the installed harness
$harness = Join-Path $homeDir '.wk-harness'
$names = @('normKo', 'skill-reinforce-nudge', 'web-result-skill-edit', 'WK_QUEUED_GATE',
  'Get-WkQueuedInterimGate', 'wkhippo_reflex_noise', 'wkhippo_reflex_stop_refused')
$files = @(Get-ChildItem -LiteralPath $harness -Recurse -File -ErrorAction SilentlyContinue)
$present = 0
$missing = @()
foreach ($n in $names) {
  $hit = $false
  foreach ($f in $files) {
    if (Select-String -LiteralPath $f.FullName -SimpleMatch -Pattern $n -Quiet) { $hit = $true; break }
  }
  if ($hit) { $present++ } else { $missing += $n }
}
$missTxt = 'none'
if ($missing.Count -gt 0) { $missTxt = $missing -join ',' }
Res 'symbols_present' $present
Res 'symbols_missing' $missTxt
Check 'symbols_7' ($present -eq 7)

# both --help exit 0
& wkappbot.cmd --help > $null 2>&1
$a = $LASTEXITCODE
& wkhippo.cmd --help > $null 2>&1
$b = $LASTEXITCODE
Res 'help_wkappbot_rc' $a
Res 'help_wkhippo_rc' $b
Check 'help_both' (($a -eq 0) -and ($b -eq 0))

# one Stop event through the Stop command settings.json names for the harness
$settingsPath = Join-Path $homeDir '.claude\settings.json'
$cmdLine = ''
if (Test-Path -LiteralPath $settingsPath) {
  $cfg = Get-Content -LiteralPath $settingsPath -Raw -Encoding UTF8 | ConvertFrom-Json
  if ($cfg.hooks -and $cfg.hooks.Stop) {
    foreach ($m in @($cfg.hooks.Stop)) {
      foreach ($h in @($m.hooks)) {
        if ((-not $cmdLine) -and ([string]$h.command -match 'wkharness')) { $cmdLine = [string]$h.command }
      }
    }
  }
}
if (-not $cmdLine) {
  Res 'stop_hook_found' 'no'
  Check 'stop_roundtrip' $false
} else {
  Res 'stop_hook_found' 'yes'
  $json = '{"hook_event_name":"Stop","session_id":"qa-harsh","cwd":"C:/","stop_hook_active":false,"last_assistant_message":"qa ping"}'
  $psi = New-Object System.Diagnostics.ProcessStartInfo
  $psi.FileName = 'cmd.exe'
  $psi.Arguments = '/d /s /c "' + $cmdLine + '"'
  $psi.UseShellExecute = $false
  $psi.RedirectStandardInput = $true
  $psi.RedirectStandardOutput = $true
  $psi.RedirectStandardError = $true
  $pr = [System.Diagnostics.Process]::Start($psi)
  $pr.StandardInput.Write($json)
  $pr.StandardInput.Close()
  $so = $pr.StandardOutput.ReadToEnd()
  $se = $pr.StandardError.ReadToEnd()
  $pr.WaitForExit()
  $rc = $pr.ExitCode
  $txt = ($so + $se).Trim()
  Res 'stop_hook_rc' $rc
  $textTxt = 'no'
  if ($txt.Length -gt 0) { $textTxt = 'yes' }
  Res 'stop_hook_text' $textTxt
  $ok = (($rc -eq 0) -or (($rc -eq 2) -and ($txt.Length -gt 0)))
  Check 'stop_roundtrip' $ok
}

$verdict = 'PASS'
if ($script:fails -gt 0) { $verdict = 'FAIL' }
Res 'verdict' $verdict
Write-Output ('HARSH-LEG ' + $HG_LEG + ' fails=' + $script:fails)
exit $script:fails
