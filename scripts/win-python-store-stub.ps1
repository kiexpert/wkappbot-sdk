# Store python stub first, every real python hidden: a fresh Windows 11 where the only "python" is the
# App execution alias (owner 2026-11-09: the harness install failed with code 49 there).
# - python.exe / python3.exe in %LOCALAPPDATA%\Microsoft\WindowsApps (the image's own stub if present,
#   else a stand-in with the same rc 9009 and the Store message);
# - real pythons hidden: PATH entries (process, machine, user), the py launcher, the tool cache, PEP 514 keys;
# - the job PATH (GITHUB_ENV) and the user PATH end with WindowsApps, as on a real machine;
# - fails if python3 -c "print(3)" still prints 3 (the leg would not test the stub).
# Writes "stub=<image|built> rc=<n>" to $env:RUNNER_TEMP\stub.txt. Windows PowerShell 5.1 (Add-Type exe output).
$ErrorActionPreference = 'Stop'
$wa = Join-Path $env:LOCALAPPDATA 'Microsoft\WindowsApps'
New-Item -ItemType Directory -Force $wa | Out-Null
$kind = 'built'; if (Test-Path (Join-Path $wa 'python.exe')) { $kind = 'image' }
if ($kind -eq 'built') {
  $src = 'using System; public static class StoreStub { public static int Main(string[] a) { Console.Error.WriteLine("Python was not found; run without arguments to install from the Microsoft Store, or disable this shortcut from Settings > Apps > Advanced app settings > App execution aliases."); return 9009; } }'
  foreach ($n in 'python.exe', 'python3.exe') { Add-Type -TypeDefinition $src -OutputType ConsoleApplication -OutputAssembly (Join-Path $wa $n) }
}
foreach ($f in 'C:\Windows\py.exe', 'C:\Windows\pyw.exe') { if (Test-Path $f) { Rename-Item $f ((Split-Path $f -Leaf) + '.hidden') } }
foreach ($d in 'C:\hostedtoolcache\windows\Python', 'C:\Python') { if (Test-Path $d) { Rename-Item $d ((Split-Path $d -Leaf) + '.hidden') } }
foreach ($k in 'HKLM:\SOFTWARE\Python', 'HKCU:\SOFTWARE\Python', 'HKLM:\SOFTWARE\WOW6432Node\Python') { if (Test-Path $k) { Remove-Item $k -Recurse -Force } }
$isPy = { param($e) $e -match '(?i)\\python|\\pyenv|\\pipx|WindowsApps' }
$keep = ($env:PATH -split ';') | Where-Object { $_ -and -not (& $isPy $_) }
$mach = ([Environment]::GetEnvironmentVariable('PATH', 'Machine') -split ';') | Where-Object { $_ -and -not (& $isPy $_) }
[Environment]::SetEnvironmentVariable('PATH', ($mach -join ';'), 'Machine')
$user = ([Environment]::GetEnvironmentVariable('PATH', 'User') -split ';') | Where-Object { $_ -and -not (& $isPy $_) }
[Environment]::SetEnvironmentVariable('PATH', ((@($user) + $wa) -join ';'), 'User')
$new = (@($keep) + $wa) -join ';'
"PATH=$new" | Out-File -Append -Encoding utf8 $env:GITHUB_ENV
$env:PATH = $new
$o = (& python3 -c "print(3)" 2>&1 | Out-String).Trim(); $rc = $LASTEXITCODE
Write-Host "stub=$kind at $wa; python3 -c print(3) -> rc=$rc '$o'"
if ($o -eq '3') { Write-Error 'a real python is still reachable: the leg would not test the stub' }
Set-Content -Encoding ascii (Join-Path $env:RUNNER_TEMP 'stub.txt') "stub=$kind rc=$rc"
