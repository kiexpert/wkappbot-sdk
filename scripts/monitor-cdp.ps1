# monitor-cdp.ps1 -- wkappbot Chrome/CDP session monitor
# Shows per-session: CWD, port, renderers, memory, age, target pos, actual pos, open tabs
#
# Usage: .\monitor-cdp.ps1          # one-shot
#        .\monitor-cdp.ps1 -f       # follow (refresh every 3s)
#        .\monitor-cdp.ps1 -f -i 5  # follow with custom interval

param(
    [switch]$f,
    [int]$i = 3
)

$HQ = "D:\GitHub\WKAppBot\bin\wkappbot.hq"

# ── Win32 window rect lookup ──────────────────────────────────────────────────
Add-Type -TypeDefinition @'
using System; using System.Runtime.InteropServices; using System.Text;
public static class WkWin32 {
    [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L,T,R,B; }
    [DllImport("user32.dll")] static extern bool EnumWindows(EnumWinProc cb, IntPtr lp);
    [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr h, out uint pid);
    [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr h);
    [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] static extern int  GetWindowTextLength(IntPtr h);
    [DllImport("user32.dll")] static extern int  GetWindowText(IntPtr h, StringBuilder s, int n);
    delegate bool EnumWinProc(IntPtr h, IntPtr lp);
    public static RECT? MainWindowRect(int targetPid) {
        RECT? found = null;
        EnumWindows((h, _) => {
            if (!IsWindowVisible(h)) return true;
            uint pid; GetWindowThreadProcessId(h, out pid);
            if (pid != (uint)targetPid) return true;
            if (GetWindowTextLength(h) < 3) return true;
            RECT r; GetWindowRect(h, out r);
            if ((r.R - r.L) < 100 || (r.B - r.T) < 100) return true;
            found = r; return false;
        }, IntPtr.Zero);
        return found;
    }
}
'@ -ErrorAction SilentlyContinue

# ── Build port -> CWD map from cdp_port_HASH.txt + SHA256(cwd) ───────────────
function Build-PortCwdMap {
    $map = @{}
    # Collect candidate CWDs: parent_window_geo files + direct git/CLAUDE.md project scan
    $cwds = @()
    Get-ChildItem "$HQ\runtime\parent_window_geo_*.json" -ErrorAction SilentlyContinue | ForEach-Object {
        try { $j = Get-Content $_.FullName -Raw | ConvertFrom-Json; if ($j.cwd) { $cwds += $j.cwd } } catch {}
    }
    # Scan known dev roots for project folders (has .git or CLAUDE.md)
    @('D:\GitHub', 'D:\Projects', 'C:\Users\kiexp\source') | Where-Object { Test-Path $_ } | ForEach-Object {
        Get-ChildItem $_ -Directory -ErrorAction SilentlyContinue | ForEach-Object {
            if ((Test-Path "$($_.FullName)\.git") -or (Test-Path "$($_.FullName)\CLAUDE.md")) {
                $cwds += $_.FullName
            }
        }
    }
    $cwds = $cwds | Select-Object -Unique

    # Load all cdp_port files: hash -> port
    $portFiles = @{}
    Get-ChildItem "$HQ\runtime\cdp_port_*.txt" -ErrorAction SilentlyContinue | ForEach-Object {
        $h = $_.BaseName -replace '^cdp_port_', ''
        $p = (Get-Content $_.FullName -Raw).Trim()
        $portFiles[$h] = $p
    }

    # Path 1: MD5 hash match  (ProjectRoot.Hash8 uses MD5, not SHA256)
    $md5 = [System.Security.Cryptography.MD5]::Create()
    foreach ($cwd in $cwds) {
        $bytes = [System.Text.Encoding]::UTF8.GetBytes($cwd.ToLowerInvariant())
        $hash  = $md5.ComputeHash($bytes)
        $hex   = [BitConverter]::ToString($hash) -replace '-', ''
        $h8    = $hex.Substring(0, 8).ToLower()
        if ($portFiles.ContainsKey($h8)) {
            $map[$portFiles[$h8]] = $cwd   # port string -> CWD
        }
    }

    # Path 2: DerivePort reverse lookup  (SHA256 big-endian -> 9300 + (n%174)*4)
    # Matches ports not covered by cdp_port files (most projects)
    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    foreach ($cwd in $cwds) {
        $norm  = [System.IO.Path]::GetFullPath($cwd).ToLowerInvariant()
        $bytes = [System.Text.Encoding]::UTF8.GetBytes($norm)
        $hash  = $sha256.ComputeHash($bytes)
        # ReadUInt32BigEndian from first 4 bytes
        $n = ([uint32]$hash[0] -shl 24) -bor ([uint32]$hash[1] -shl 16) -bor ([uint32]$hash[2] -shl 8) -bor [uint32]$hash[3]
        $base = 9300 + ($n % 174) * 4
        # 4-port block: base, base+1, base+2, base+3
        for ($p = $base; $p -le $base+3; $p++) {
            if (-not $map.ContainsKey("$p")) {
                $map["$p"] = $cwd
            }
        }
    }
    return $map
}

# ── Get open tab titles via CDP /json ─────────────────────────────────────────
function Get-CdpTabs([int]$port) {
    try {
        $r = Invoke-RestMethod "http://localhost:$port/json" -TimeoutSec 1 -ErrorAction Stop
        return $r | Where-Object { $_.type -eq 'page' } | ForEach-Object { $_.title }
    } catch { return @() }
}

# ── Main session enumeration ──────────────────────────────────────────────────
function Get-WkCdpSessions {
    $portCwdMap = Build-PortCwdMap

    $all = Get-CimInstance Win32_Process -Filter "Name = 'chrome.exe'" |
           Select-Object ProcessId, ParentProcessId, CreationDate, WorkingSetSize, CommandLine

    # Browser processes: have --remote-debugging-port and wkappbot profile path
    $browsers = $all | Where-Object {
        $_.CommandLine -match '--remote-debugging-port=(\d+)' -and
        $_.CommandLine -match 'wkappbot\.hq[/\\]chrome-profiles'
    }

    # Group by port (one row per CDP session)
    $byPort = @{}
    foreach ($b in $browsers) {
        $port = if ($b.CommandLine -match '--remote-debugging-port=(\d+)') { $Matches[1] } else { '0' }
        if (-not $byPort.ContainsKey($port)) { $byPort[$port] = @() }
        $byPort[$port] += $b
    }

    foreach ($port in ($byPort.Keys | Sort-Object { [int]$_ })) {
        $procs = $byPort[$port]

        # Main process = has --window-position in cmdline (browser process)
        $main = $procs | Where-Object { $_.CommandLine -match '--window-position=' } | Select-Object -First 1
        if (-not $main) { $main = $procs | Sort-Object WorkingSetSize -Descending | Select-Object -First 1 }

        # Renderers = child processes of main
        $rCount = ($all | Where-Object { $_.ParentProcessId -eq $main.ProcessId } | Measure-Object).Count

        # Total memory (all procs with this port)
        $totalBytes = ($procs | Measure-Object -Property WorkingSetSize -Sum).Sum

        # Age from oldest process in group
        $oldest = ($procs | Sort-Object CreationDate | Select-Object -First 1).CreationDate
        $age = (Get-Date) - $oldest
        $ageStr = if    ($age.TotalHours -ge 1)   { '{0:F1}h' -f $age.TotalHours   }
                  elseif ($age.TotalMinutes -ge 1) { '{0:F0}m' -f $age.TotalMinutes }
                  else                             { '{0:F0}s' -f $age.TotalSeconds  }

        # Target position/size from cmdline args
        $tgtPos  = if ($main.CommandLine -match '--window-position=(-?\d+),(-?\d+)') { "$($Matches[1]),$($Matches[2])" } else { '?' }
        $tgtSize = if ($main.CommandLine -match '--window-size=(\d+),(\d+)')         { "$($Matches[1])x$($Matches[2])" } else { '?' }

        # Actual window rect (PowerShell unwraps Nullable<T> automatically)
        $rect = [WkWin32]::MainWindowRect([int]$main.ProcessId)
        $actPos  = if ($null -ne $rect) { "$($rect.L),$($rect.T)" } else { '?' }
        $actSize = if ($null -ne $rect) { "$(($rect.R - $rect.L))x$(($rect.B - $rect.T))" } else { '?' }

        # Drift (target vs actual position)
        $drift = '?'
        if ($tgtPos -ne '?' -and $actPos -ne '?' -and $actPos -notmatch '^,') {
            $tp = $tgtPos -split ','; $ap = $actPos -split ','
            $dx = [int]$ap[0] - [int]$tp[0]; $dy = [int]$ap[1] - [int]$tp[1]
            $drift = if ($dx -eq 0 -and $dy -eq 0) { 'OK' } else { "dx=$dx dy=$dy" }
        }

        # Tabs
        $tabs    = Get-CdpTabs ([int]$port)
        $tabStr  = if ($tabs.Count -eq 0) { '(no tabs)' }
                   elseif ($tabs.Count -eq 1) { $tabs[0] -replace '.{40}$', '...' }
                   else { "$($tabs.Count) tabs: $($tabs[0] -replace '.{30}$','...')" }

        # CWD
        $cwd = if ($portCwdMap.ContainsKey($port)) { $portCwdMap[$port] } else { '(unknown)' }

        [PSCustomObject]@{
            Port     = [int]$port
            MainPID  = $main.ProcessId
            Procs    = $procs.Count
            Renderers= $rCount
            MemMB    = [math]::Round($totalBytes / 1MB)
            Age      = $ageStr
            TgtPos   = $tgtPos
            ActPos   = $actPos
            Drift    = $drift
            Tabs     = $tabStr
            CWD      = $cwd
        }
    }
}

function Show-Once {
    $sessions = @(Get-WkCdpSessions)
    if (-not $sessions -or $sessions.Count -eq 0) {
        Write-Host '  (no wkappbot Chrome sessions running)' -ForegroundColor DarkGray
        return
    }

    Write-Host ('{0,-6} {1,-7} {2,3}/{3,3} {4,6} {5,5}  {6,-13} {7,-13} {8,-12}  {9}' -f `
        'PORT','PID','P','R','MEM(M)','AGE','TGT-POS','ACT-POS','DRIFT','CWD') -ForegroundColor Cyan
    Write-Host ('-' * 100) -ForegroundColor DarkGray

    foreach ($s in $sessions) {
        $driftColor = if ($s.Drift -eq 'OK' -or $s.Drift -eq '?') { 'White' } else { 'Red' }
        $line = '{0,-6} {1,-7} {2,3}/{3,3} {4,6} {5,5}  {6,-13} {7,-13} {8,-12}  {9}' -f `
            $s.Port, $s.MainPID, $s.Procs, $s.Renderers, $s.MemMB, $s.Age,
            $s.TgtPos, $s.ActPos, $s.Drift, $s.CWD
        Write-Host $line -ForegroundColor $driftColor
        if ($s.Tabs -ne '(no tabs)') {
            Write-Host ("         tabs: $($s.Tabs)") -ForegroundColor DarkGray
        }
    }

    $totMem = ($sessions | Measure-Object -Property MemMB      -Sum).Sum
    $totRen = ($sessions | Measure-Object -Property Renderers  -Sum).Sum
    $totPro = ($sessions | Measure-Object -Property Procs      -Sum).Sum
    Write-Host ''
    Write-Host ("  {0} session(s)  {1} procs / {2} renderers  {3} MB total" -f `
        $sessions.Count, $totPro, $totRen, $totMem) -ForegroundColor Yellow
}

if ($f) {
    while ($true) {
        Clear-Host
        Write-Host ("[CDP Monitor] {0}" -f (Get-Date -Format 'HH:mm:ss')) -ForegroundColor Green
        Show-Once
        Start-Sleep -Seconds $i
    }
} else {
    Show-Once
}
