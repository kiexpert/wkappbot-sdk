# monitor-cdp.ps1 -- wkappbot Chrome/CDP session monitor
# Shows per-session: CWD, port, procs/renderers, memory, age,
#                    target pos, actual pos, position drift, open tabs
#
# Usage: .\monitor-cdp.ps1              # one-shot
#        .\monitor-cdp.ps1 -f           # follow (refresh every 3s)
#        .\monitor-cdp.ps1 -f -i 5      # follow, 5s interval
#        .\monitor-cdp.ps1 -tabs        # show tab list per session

param(
    [switch]$f,
    [int]$i = 3,
    [switch]$tabs
)

$HQ = "D:\GitHub\WKAppBot\bin\wkappbot.hq"

# ── Win32: actual window rect from PID ───────────────────────────────────────
Add-Type -TypeDefinition @'
using System; using System.Runtime.InteropServices; using System.Text;
public static class WkWin32 {
    [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L,T,R,B; }
    [DllImport("user32.dll")] static extern bool EnumWindows(EnumWinProc cb, IntPtr lp);
    [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr h, out uint pid);
    [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr h);
    [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] static extern int  GetWindowTextLength(IntPtr h);
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

# ── CDP tab fetch (Chrome 110+ requires Host:localhost header) ────────────────
Add-Type -AssemblyName System.Net.Http -ErrorAction SilentlyContinue
$global:_WkCdpClient = $null

# Returns hashtable: LatAvg/LatMin/LatMax (ms, -1=dead), Titles (string[])
# Pings CDP /json 3 times with 100ms timeout; reports avg/min/max.
# Use 127.0.0.1: on Win11, localhost may resolve to IPv6 ::1 but Chrome binds IPv4 only.
function Get-CdpInfo([int]$port, [int]$pings = 3) {
    if (-not $global:_WkCdpClient) {
        $global:_WkCdpClient = New-Object System.Net.Http.HttpClient
        $global:_WkCdpClient.Timeout = [TimeSpan]::FromMilliseconds(100)
    }
    $samples = @(); $titles = @()
    for ($n = 0; $n -lt $pings; $n++) {
        try {
            $req = New-Object System.Net.Http.HttpRequestMessage(
                [System.Net.Http.HttpMethod]::Get, "http://127.0.0.1:$port/json")
            $req.Headers.Host = "localhost"
            $sw = [System.Diagnostics.Stopwatch]::StartNew()
            $resp = $global:_WkCdpClient.SendAsync($req).Result
            $sw.Stop()
            if ($resp.IsSuccessStatusCode) {
                $samples += [int]$sw.ElapsedMilliseconds
                if ($n -eq 0) {
                    $body  = $resp.Content.ReadAsStringAsync().Result
                    $pages = @(($body | ConvertFrom-Json) | Where-Object { $_.type -eq 'page' })
                    $titles = $pages | Select-Object -ExpandProperty title
                }
            }
        } catch {}
    }
    if ($samples.Count -eq 0) { return @{ LatAvg=-1; LatMin=-1; LatMax=-1; Titles=@() } }
    $avg = [int](($samples | Measure-Object -Average).Average)
    $min = ($samples | Measure-Object -Minimum).Minimum
    $max = ($samples | Measure-Object -Maximum).Maximum
    return @{ LatAvg=$avg; LatMin=$min; LatMax=$max; Titles=$titles }
}

# ── Port -> CWD map: MD5 (cdp_port files) + SHA256 DerivePort reverse ────────
function Build-PortCwdMap {
    $map = @{}

    # Candidate CWDs: runtime geo files + git/.CLAUDE.md project scan
    $cwds = @()
    Get-ChildItem "$HQ\runtime\parent_window_geo_*.json" -ErrorAction SilentlyContinue | ForEach-Object {
        try { $j = Get-Content $_.FullName -Raw | ConvertFrom-Json; if ($j.cwd) { $cwds += $j.cwd } } catch {}
    }
    @('D:\GitHub', 'D:\Projects') | Where-Object { Test-Path $_ } | ForEach-Object {
        Get-ChildItem $_ -Directory -ErrorAction SilentlyContinue | ForEach-Object {
            if ((Test-Path "$($_.FullName)\.git") -or (Test-Path "$($_.FullName)\CLAUDE.md")) {
                $cwds += $_.FullName
            }
        }
    }
    $cwds = $cwds | Select-Object -Unique

    # cdp_port_HASH.txt files: MD5(cwd.lower())[0:8] -> port
    $portFiles = @{}
    Get-ChildItem "$HQ\runtime\cdp_port_*.txt" -ErrorAction SilentlyContinue | ForEach-Object {
        $h = $_.BaseName -replace '^cdp_port_', ''
        $p = (Get-Content $_.FullName -Raw).Trim()
        $portFiles[$h] = $p
    }

    $md5    = [System.Security.Cryptography.MD5]::Create()
    $sha256 = [System.Security.Cryptography.SHA256]::Create()

    foreach ($cwd in $cwds) {
        $lower = $cwd.ToLowerInvariant()
        $norm  = [System.IO.Path]::GetFullPath($cwd).ToLowerInvariant()

        # Path 1: MD5 hash -> cdp_port file (ProjectRoot.Hash8 uses MD5)
        $md5h = [BitConverter]::ToString($md5.ComputeHash(
                    [System.Text.Encoding]::UTF8.GetBytes($lower))) -replace '-',''
        $h8   = $md5h.Substring(0,8).ToLower()
        if ($portFiles.ContainsKey($h8) -and -not $map.ContainsKey($portFiles[$h8])) {
            $map[$portFiles[$h8]] = $cwd
        }

        # Path 2: SHA256 DerivePort reverse  (9300 + (ReadUInt32BE(sha256) % 174) * 4)
        $sha  = $sha256.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($norm))
        $n    = ([uint32]$sha[0] -shl 24) -bor ([uint32]$sha[1] -shl 16) `
               -bor ([uint32]$sha[2] -shl 8) -bor [uint32]$sha[3]
        $base = 9300 + ($n % 174) * 4
        for ($p = $base; $p -le $base+3; $p++) {
            if (-not $map.ContainsKey("$p")) { $map["$p"] = $cwd }
        }
    }
    return $map
}

# ── Main enumeration ──────────────────────────────────────────────────────────
function Get-WkCdpSessions {
    $portCwdMap = Build-PortCwdMap

    $all = Get-CimInstance Win32_Process -Filter "Name = 'chrome.exe'" |
           Select-Object ProcessId, ParentProcessId, CreationDate, WorkingSetSize, CommandLine

    $browsers = $all | Where-Object {
        $_.CommandLine -match '--remote-debugging-port=(\d+)' -and
        $_.CommandLine -match 'wkappbot\.hq[/\\]chrome-profiles'
    }

    $byPort = @{}
    foreach ($b in $browsers) {
        $p = if ($b.CommandLine -match '--remote-debugging-port=(\d+)') { $Matches[1] } else { '0' }
        if (-not $byPort[$p]) { $byPort[$p] = @() }
        $byPort[$p] += $b
    }

    foreach ($port in ($byPort.Keys | Sort-Object { [int]$_ })) {
        $procs = $byPort[$port]

        $main = ($procs | Where-Object { $_.CommandLine -match '--window-position=' } |
                 Select-Object -First 1)
        if (-not $main) { $main = $procs | Sort-Object WorkingSetSize -Descending | Select-Object -First 1 }

        $rCount    = ($all | Where-Object { $_.ParentProcessId -eq $main.ProcessId } | Measure-Object).Count
        $totalMB   = [math]::Round(($procs | Measure-Object WorkingSetSize -Sum).Sum / 1MB)

        $oldest    = ($procs | Sort-Object CreationDate | Select-Object -First 1).CreationDate
        $age       = (Get-Date) - $oldest
        $ageStr    = if    ($age.TotalHours   -ge 1) { '{0:F1}h' -f $age.TotalHours   }
                     elseif ($age.TotalMinutes -ge 1) { '{0:F0}m' -f $age.TotalMinutes }
                     else                             { '{0:F0}s' -f $age.TotalSeconds  }

        $cmd       = $main.CommandLine
        $tgtPos    = if ($cmd -match '--window-position=(-?\d+),(-?\d+)') { "$($Matches[1]),$($Matches[2])" } else { '?' }
        $tgtSize   = if ($cmd -match '--window-size=(\d+),(\d+)')         { "$($Matches[1])x$($Matches[2])" } else { '?' }
        $startUrl  = if ($cmd -match '"(https?://[^"]+)"$')               { $Matches[1] }
                     elseif ($cmd -match "(https?://\S+)$")               { $Matches[1] } else { '' }

        $rect      = [WkWin32]::MainWindowRect([int]$main.ProcessId)
        $actPos    = if ($null -ne $rect) { "$($rect.L),$($rect.T)" }     else { '(min)' }
        $actSize   = if ($null -ne $rect) { "$($rect.R-$rect.L)x$($rect.B-$rect.T)" } else { '?' }

        # Drift
        $drift = 'n/a'
        $offscreen = $false
        if ($tgtPos -ne '?' -and $actPos -ne '(min)') {
            $tp = $tgtPos -split ','; $ap = $actPos -split ','
            try {
                $dx = [int]$ap[0] - [int]$tp[0]; $dy = [int]$ap[1] - [int]$tp[1]
                $drift = if ($dx -eq 0 -and $dy -eq 0) { 'OK' } else { "d$dx,$dy" }
            } catch {}
        }
        if ($tgtPos -match '^-\d{3,}') { $offscreen = $true }  # x < -100 = off-screen target

        $cdp      = Get-CdpInfo ([int]$port)
        $latAvg   = $cdp.LatAvg    # ms avg; -1 = dead/timeout
        $latMin   = $cdp.LatMin
        $latMax   = $cdp.LatMax
        $tabList  = @($cdp.Titles)
        $tabCount = $tabList.Count

        $cwd  = if ($portCwdMap.ContainsKey($port)) { $portCwdMap[$port] } else { '(unknown)' }
        $proj = Split-Path $cwd -Leaf

        [PSCustomObject]@{
            Port      = [int]$port
            MainPID   = $main.ProcessId
            Procs     = $procs.Count
            Renderers = $rCount
            MemMB     = $totalMB
            Age       = $ageStr
            TgtPos    = $tgtPos
            TgtSize   = $tgtSize
            ActPos    = $actPos
            ActSize   = $actSize
            Drift     = $drift
            Offscreen = $offscreen
            LatAvg    = $latAvg
            LatMin    = $latMin
            LatMax    = $latMax
            TabCount  = $tabCount
            TabList   = $tabList
            StartUrl  = $startUrl
            CWD       = $cwd
            Proj      = $proj
        }
    }
}

# ── Display ───────────────────────────────────────────────────────────────────
function Show-Once {
    $sessions = @(Get-WkCdpSessions)
    if (-not $sessions -or $sessions.Count -eq 0) {
        Write-Host '  (no wkappbot Chrome sessions running)' -ForegroundColor DarkGray
        return
    }

    $hdr = '{0,-6} {1,-7} {2,5} {3,6} {4,5}  {5,-13} {6,-13} {7,-10} {8,4}  {9,-14} {10}' -f `
           'PORT','PID','P/R','MEM(M)','AGE','TGT-POS','ACT-POS','DRIFT','TABS','LAT(avg/mn/mx)','PROJECT (CWD)'
    Write-Host $hdr -ForegroundColor Cyan
    Write-Host ('-' * 120) -ForegroundColor DarkGray

    foreach ($s in $sessions) {
        $pr       = "$($s.Procs)/$($s.Renderers)"

        # Latency string: "12/8/31" or "DEAD"
        $latStr   = if ($s.LatAvg -lt 0) { 'DEAD' }
                    else { "$($s.LatAvg)/$($s.LatMin)/$($s.LatMax)" }
        $latColor = if ($s.LatAvg -lt 0)   { 'Red'    }
                    elseif ($s.LatAvg -gt 50) { 'Yellow' }
                    else                    { 'Green'  }

        $memColor = if ($s.MemMB -gt 2000) { 'Red' } elseif ($s.MemMB -gt 1000) { 'Yellow' } else { 'White' }
        $rowColor = if ($s.Offscreen)                       { 'Red'    }
                    elseif ($s.Drift -notin @('OK','n/a'))  { 'Yellow' }
                    else                                    { 'White'  }

        $pre  = '{0,-6} {1,-7} {2,5} ' -f $s.Port, $s.MainPID, $pr
        $mem  = '{0,6} ' -f $s.MemMB
        $mid  = '{0,5}  {1,-13} {2,-13} {3,-10} {4,4}  ' -f `
                $s.Age, $s.TgtPos, $s.ActPos, $s.Drift, $s.TabCount
        $lat  = '{0,-14} ' -f $latStr
        $tail = "$($s.Proj)  ($($s.CWD))"

        if ($rowColor -ne 'White') {
            Write-Host ($pre + $mem + $mid + $lat + $tail) -ForegroundColor $rowColor
        } else {
            Write-Host $pre  -NoNewline
            Write-Host $mem  -NoNewline -ForegroundColor $memColor
            Write-Host $mid  -NoNewline
            Write-Host $lat  -NoNewline -ForegroundColor $latColor
            Write-Host $tail
        }

        # Tab list (if -tabs flag or any tabs)
        if ($tabs -and $s.TabList.Count -gt 0) {
            foreach ($t in $s.TabList) {
                Write-Host ("         > $t") -ForegroundColor DarkGray
            }
        } elseif ($s.TabCount -gt 0 -and $s.StartUrl) {
            Write-Host ("         > $($s.StartUrl)") -ForegroundColor DarkGray
        }
    }

    $totMem = ($sessions | Measure-Object MemMB     -Sum).Sum
    $totRen = ($sessions | Measure-Object Renderers -Sum).Sum
    $totPro = ($sessions | Measure-Object Procs     -Sum).Sum
    $totTab = ($sessions | Measure-Object TabCount  -Sum).Sum
    Write-Host ''
    $summaryColor = if ($sessions | Where-Object Offscreen) { 'Red' } else { 'Yellow' }
    Write-Host ("  {0} session(s)  {1} procs / {2} renderers  {3} tabs  {4} MB total" -f `
        $sessions.Count, $totPro, $totRen, $totTab, $totMem) -ForegroundColor $summaryColor
    if ($sessions | Where-Object Offscreen) {
        Write-Host "  [!] Off-screen session(s) detected (TGT-POS x < -100)" -ForegroundColor Red
    }
}

if ($f) {
    while ($true) {
        Clear-Host
        Write-Host ("[CDP Monitor] {0}  (Ctrl+C to stop)" -f (Get-Date -Format 'HH:mm:ss')) -ForegroundColor Green
        Show-Once
        Start-Sleep -Seconds $i
    }
} else {
    Show-Once
}
