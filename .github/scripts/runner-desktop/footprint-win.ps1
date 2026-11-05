# Installed-software footprint for Windows runner desktops: baseline | save <dir> | restore <dir>
# baseline (at boot, before the owner installs anything): records Program Files / ProgramData dirs, uninstall + Run
#   registry keys and services.  save: packs only what is NEW since baseline.  restore: replays it at the next boot.
# Not packed here: user profiles / AppData (Chrome travels separately via chrome.py), certificate stores, NPKI / *.pfx / *.p12 / *.key / *.der.
# Not replayable (reported as failed classes): kernel / file-system drivers, anything bound to the machine id.
param([string]$Cmd, [string]$Dir)
$ErrorActionPreference = 'Continue'
$base = Join-Path $env:RUNNER_TEMP 'desktop-baseline.json'
$roots = @($env:ProgramFiles, ${env:ProgramFiles(x86)}, $env:ProgramData) | Where-Object { $_ -and (Test-Path $_) }
$regRoots = 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall', 'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall',
            'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall'
$runKeys = 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run', 'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run',
           'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run'
$secretRx = '(?i)(\\NPKI\\|\\GPKI\\|\.(pfx|p12|key|der|pem)$|signPri|signCert|\\Cookies|\\User Data\\)'
function State {
  [pscustomobject]@{
    dirs = @($roots | ForEach-Object { Get-ChildItem -Directory -Force $_ -ErrorAction SilentlyContinue } | ForEach-Object FullName)
    uninst = @($regRoots | ForEach-Object { Get-ChildItem $_ -ErrorAction SilentlyContinue } | ForEach-Object Name)
    run = @($runKeys | ForEach-Object { $k = $_; $p = Get-ItemProperty $k -ErrorAction SilentlyContinue
            if ($p) { $p.PSObject.Properties | Where-Object { $_.Name -notlike 'PS*' } | ForEach-Object { "$k|$($_.Name)" } } })
    svc = @(Get-CimInstance Win32_Service | ForEach-Object Name) + @(Get-CimInstance Win32_SystemDriver | ForEach-Object Name)
  }
}
switch ($Cmd) {
  'baseline' { State | ConvertTo-Json -Depth 3 | Set-Content -Encoding UTF8 $base; "baseline recorded" }
  'save' {
    $b = Get-Content -Raw $base | ConvertFrom-Json; $n = State
    New-Item -ItemType Directory -Force (Join-Path $Dir 'files') | Out-Null
    $man = [ordered]@{ dirs = @(); reg = @(); run = @(); services = @(); skipped = @() }
    foreach ($d in ($n.dirs | Where-Object { $b.dirs -notcontains $_ })) {
      $rel = $d.Substring(0, 1) + ($d.Substring(2) -replace '[\\:]', '_')
      $dst = Join-Path $Dir "files\$rel"
      robocopy $d $dst /E /NFL /NDL /NJH /NJS /R:0 /W:0 /XF *.pfx *.p12 *.key *.der *.pem /XD NPKI GPKI 'User Data' | Out-Null
      Get-ChildItem -Recurse -File $dst -ErrorAction SilentlyContinue | Where-Object { $_.FullName -match $secretRx } | Remove-Item -Force
      $man.dirs += [pscustomobject]@{ path = $d; packed = "files\$rel" }
    }
    $i = 0
    foreach ($k in ($n.uninst | Where-Object { $b.uninst -notcontains $_ })) {
      $f = Join-Path $Dir ("reg{0}.reg" -f $i++); reg export $k $f /y | Out-Null; $man.reg += [pscustomobject]@{ key = $k; file = (Split-Path -Leaf $f) }
    }
    foreach ($r in ($n.run | Where-Object { $b.run -notcontains $_ })) {
      $k, $name = $r -split '\|', 2; $man.run += [pscustomobject]@{ key = $k; name = $name; value = (Get-ItemProperty $k).$name }
    }
    foreach ($s in ($n.svc | Where-Object { $b.svc -notcontains $_ })) {
      $c = Get-CimInstance Win32_Service -Filter "Name='$s'"
      if ($c) { $man.services += [pscustomobject]@{ name = $s; display = $c.DisplayName; bin = $c.PathName; start = $c.StartMode } }
      else { $man.skipped += [pscustomobject]@{ item = "driver:$s"; class = 'kernel/file-system driver (not replayable)' } }
    }
    $man | ConvertTo-Json -Depth 4 | Set-Content -Encoding UTF8 (Join-Path $Dir 'manifest.json')
    "footprint: $($man.dirs.Count) dirs, $($man.reg.Count) uninstall keys, $($man.run.Count) run entries, $($man.services.Count) services, $($man.skipped.Count) drivers skipped"
  }
  'restore' {
    $m = Get-Content -Raw (Join-Path $Dir 'manifest.json') | ConvertFrom-Json
    $ok = @(); $fail = @($m.skipped | ForEach-Object { "$($_.item): $($_.class)" })
    foreach ($d in $m.dirs) {
      robocopy (Join-Path $Dir $d.packed) $d.path /E /NFL /NDL /NJH /NJS /R:0 /W:0 | Out-Null
      if ($LASTEXITCODE -lt 8) { $ok += "dir:$($d.path)" } else { $fail += "dir:$($d.path): copy failed" }
    }
    foreach ($r in $m.reg) { reg import (Join-Path $Dir $r.file) 2>$null; if ($LASTEXITCODE -eq 0) { $ok += "reg:$($r.key)" } else { $fail += "reg:$($r.key): import failed" } }
    foreach ($r in $m.run) { New-ItemProperty -Path $r.key -Name $r.name -Value $r.value -Force | Out-Null; $ok += "run:$($r.name)" }
    foreach ($s in $m.services) {
      $st = @{ Auto = 'Automatic'; Manual = 'Manual'; Disabled = 'Disabled' }[$s.start]; if (-not $st) { $st = 'Manual' }
      try {
        if (-not (Get-Service $s.name -ErrorAction SilentlyContinue)) {
          New-Service -Name $s.name -BinaryPathName $s.bin -DisplayName $s.display -StartupType $st -ErrorAction Stop | Out-Null
        }
        if ($st -eq 'Automatic') { Start-Service $s.name -ErrorAction SilentlyContinue }
        $ok += "svc:$($s.name)"
      } catch { $fail += "svc:$($s.name): $($_.Exception.Message) (machine-bound or driver-dependent?)" }
    }
    foreach ($r in $m.run) { $exe = ($r.value -replace '^"([^"]+)".*$', '$1') -replace '^(\S+\.exe).*$', '$1'; if (Test-Path $exe) { Start-Process $exe -ErrorAction SilentlyContinue } }
    [pscustomobject]@{ restored = $ok; failed = $fail } | ConvertTo-Json -Depth 3 -Compress
  }
}
