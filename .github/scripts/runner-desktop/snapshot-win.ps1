# Windows half of snapshot.sh: idle | save <apps.json> | restore <apps.json>
param([string]$Cmd, [string]$Path)
Add-Type @"
using System; using System.Runtime.InteropServices;
public static class WkSnap {
  [StructLayout(LayoutKind.Sequential)] public struct LII { public uint cbSize; public uint dwTime; }
  [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L, T, R, B; }
  [DllImport("user32.dll")] public static extern bool GetLastInputInfo(ref LII p);
  [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
  [DllImport("user32.dll")] public static extern bool MoveWindow(IntPtr h, int x, int y, int w, int hh, bool rp);
  public static uint Idle() { var l = new LII(); l.cbSize = 8; GetLastInputInfo(ref l); return ((uint)Environment.TickCount - l.dwTime) / 1000; }
}
"@
$skip = 'chrome','explorer','cloudflared','python','powershell','pwsh','bash','Runner.Worker','Runner.Listener','tvnserver','conhost','WindowsTerminal','ShellExperienceHost','SearchHost','TextInputHost','ApplicationFrameHost','SystemSettings'
switch ($Cmd) {
  'idle' { [WkSnap]::Idle() }
  'save' {
    $apps = Get-Process | Where-Object { $_.MainWindowHandle -ne 0 -and $_.Path -and ($skip -notcontains $_.ProcessName) } | ForEach-Object {
      $r = New-Object WkSnap+RECT; [void][WkSnap]::GetWindowRect($_.MainWindowHandle, [ref]$r)
      [pscustomobject]@{ name = $_.ProcessName; path = $_.Path; x = $r.L; y = $r.T; w = $r.R - $r.L; h = $r.B - $r.T }
    }
    ConvertTo-Json @($apps) | Set-Content -Encoding UTF8 $Path
  }
  'restore' {
    $n = 0
    foreach ($a in (Get-Content -Raw $Path | ConvertFrom-Json)) {
      if (-not (Test-Path $a.path)) { continue }
      $p = Start-Process -FilePath $a.path -PassThru
      for ($i = 0; $i -lt 20 -and $p.MainWindowHandle -eq 0; $i++) { Start-Sleep -Milliseconds 500; $p.Refresh() }
      if ($p.MainWindowHandle -ne 0 -and $a.w -gt 0) { [void][WkSnap]::MoveWindow($p.MainWindowHandle, $a.x, $a.y, $a.w, $a.h, $true) }
      $n++
    }
    $n
  }
}
