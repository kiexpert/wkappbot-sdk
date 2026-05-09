# monitor-cdp.ps1 -- wkappbot Chrome/CDP session monitor
# Shows per-session: CWD, port, procs/renderers, memory, age,
#                    target pos, actual pos, position drift, open tabs
#
# Usage: .\monitor-cdp.ps1              # one-shot
#        .\monitor-cdp.ps1 -f           # follow (refresh every 3s)
#        .\monitor-cdp.ps1 -f -i 5      # follow, 5s interval
#        .\monitor-cdp.ps1 -tabs        # show tab list per session
#        .\monitor-cdp.ps1 -killIdle 10 # kill Chrome idle >10 min (no wkappbot cmd)

param(
    [switch]$f,
    [int]$i = 3,
    [switch]$tabs,
    [switch]$nofix,          # skip auto-kill of duplicate IME daemons
    [int]$killIdle = 0       # kill Chrome sessions idle > N minutes (0 = disabled)
)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$OutputEncoding = [System.Text.Encoding]::UTF8

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
    [DllImport("user32.dll")] static extern int  GetWindowText(IntPtr h, StringBuilder s, int n);
    [DllImport("user32.dll")] public  static extern bool IsWindow(IntPtr h);
    delegate bool EnumWinProc(IntPtr h, IntPtr lp);
    public static string GetTitle(IntPtr h) {
        int n = GetWindowTextLength(h); if (n <= 0) return "";
        var sb = new StringBuilder(n + 2); GetWindowText(h, sb, n + 2); return sb.ToString();
    }
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
                    $titles = $pages  # keep full objects for URL + wsUrl
                }
            }
        } catch {}
    }
    if ($samples.Count -eq 0) { return @{ LatAvg=-1; LatMin=-1; LatMax=-1; Pages=@() } }
    $avg = [int](($samples | Measure-Object -Average).Average)
    $min = ($samples | Measure-Object -Minimum).Minimum
    $max = ($samples | Measure-Object -Maximum).Maximum
    return @{ LatAvg=$avg; LatMin=$min; LatMax=$max; Pages=$titles }
}

# ── CDP WebSocket: tab health + idle + alert detection ───────────────────────
# Returns @{ Hidden=bool; AgeSec=int; Alert=bool } or $null on failure.
#
# Alert detection: JS alert blocks Runtime.evaluate. Strategy:
#   1. Connect WebSocket (200ms) -- succeeds even with alert open.
#   2. Send Runtime.evaluate (300ms tight timeout).
#   3. If connect OK but receive times out -> alert blocking -> Alert=$true.
#   4. If connect fails -> tab crashed/gone -> $null.
function Get-TabIdle([string]$wsUrl) {
    if (-not $wsUrl) { return $null }
    $ws = $null
    try {
        $ws  = New-Object System.Net.WebSockets.ClientWebSocket
        $uri = [Uri]($wsUrl -replace '^ws://localhost', 'ws://127.0.0.1')
        $cts = New-Object System.Threading.CancellationTokenSource(200)
        $ws.ConnectAsync($uri, $cts.Token).Wait(200) | Out-Null
        if ($ws.State -ne 'Open') { return $null }  # tab gone / crashed

        $expr = 'document.hidden+","+Math.round((Date.now()-performance.timing.navigationStart)/1000)'
        $msg  = "{`"id`":1,`"method`":`"Runtime.evaluate`",`"params`":{`"expression`":`"$expr`"}}"
        $buf  = [System.Text.Encoding]::UTF8.GetBytes($msg)
        $sendCts = New-Object System.Threading.CancellationTokenSource(100)
        $ws.SendAsync([System.ArraySegment[byte]]$buf,
            [System.Net.WebSockets.WebSocketMessageType]::Text, $true, $sendCts.Token).Wait(100) | Out-Null

        # Short receive timeout: alert blocks evaluate, so timeout = alert present
        $rbuf    = New-Object byte[] 4096
        $recvCts = New-Object System.Threading.CancellationTokenSource(300)
        try {
            $result = $ws.ReceiveAsync([System.ArraySegment[byte]]$rbuf, $recvCts.Token).Result
            $json   = [System.Text.Encoding]::UTF8.GetString($rbuf, 0, $result.Count) | ConvertFrom-Json
            $val    = $json.result.result.value -split ','
            return @{ Hidden=($val[0] -eq 'true'); AgeSec=[int]$val[1]; Alert=$false }
        } catch {
            # Receive timed out: WebSocket connected but eval blocked -> ALERT
            return @{ Hidden=$false; AgeSec=-1; Alert=$true }
        }
    } catch { return $null }
    finally {
        if ($ws) {
            if ($ws.State -eq 'Open') { try { $ws.CloseAsync('NormalClosure','','').Wait(100) } catch {} }
            $ws.Dispose()
        }
    }
}

# ── CWD -> active session name via Eye log [CMD] lines ────────────────────────
# Eye logs: [CMD] name=클롣[DG-wkappbot-sdk] cmd=... cwd=D:\GitHub\wkappbot-sdk
#           [CMD-MCP] name=... cmd=... cwd=... hwnd=0x...
# This is the ground truth: exact CWD + session name, logged per command.
# Newest log wins per CWD. Works even after Eye has died (logs persist on disk).
function Build-CwdCallerMap {
    $map     = @{}  # normalized-cwd -> session name
    $lastCmd = @{}  # normalized-cwd -> [datetime] last cmd time
    # Eye log lines: "[HH:mm:ss] [CMD] name=... cmd=... cwd=..."
    $reCmd = [regex]'(?:\[(\d{2}:\d{2}:\d{2})\] )?\[CMD(?:-MCP)?\] name=(\S+).*? cwd=(\S+)'

    Get-ChildItem "$HQ\logs\eye.pid=*.log" -ErrorAction SilentlyContinue |
        Sort-Object LastWriteTime -Descending | ForEach-Object {
        $logDate = $_.LastWriteTime.Date
        try {
            $tail = Get-Content $_.FullName -Tail 3000 -Encoding UTF8 -ErrorAction SilentlyContinue
            # Forward scan: $lastTs tracks the nearest preceding timestamp for each CMD line.
            # CMD lines have no [HH:mm:ss] prefix; surrounding NOTIFY/EYE lines do.
            # Each CMD overwrites temp entries so the LAST occurrence per CWD (newest) wins.
            $lastTs  = $null
            $tmpMap  = @{}
            $tmpTime = @{}
            for ($li = 0; $li -lt $tail.Count; $li++) {
                $line = $tail[$li]
                if ($line -match '^\[(\d{2}:\d{2}:\d{2})\]') { $lastTs = $Matches[1] }
                $m = $reCmd.Match($line)
                if ($m.Success) {
                    $nc = $m.Groups[3].Value.Trim().ToLowerInvariant()
                    $tmpMap[$nc] = $m.Groups[2].Value
                    if ($lastTs) {
                        try {
                            $t = [datetime]::ParseExact($lastTs, 'HH:mm:ss', $null)
                            $tmpTime[$nc] = $logDate.Add($t.TimeOfDay)
                        } catch {}
                    }
                }
            }
            # Merge into global maps; files are newest-first so first-seen wins per CWD
            foreach ($nc in $tmpMap.Keys) {
                if (-not $map.ContainsKey($nc))     { $map[$nc]     = $tmpMap[$nc] }
                if (-not $lastCmd.ContainsKey($nc) -and $tmpTime.ContainsKey($nc)) {
                    $lastCmd[$nc] = $tmpTime[$nc]
                }
            }
        } catch {}
    }
    return @{ Map=$map; LastCmd=$lastCmd }
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
    $portCwdMap      = Build-PortCwdMap
    $callerResult    = Build-CwdCallerMap
    $cwdCallerMap    = $callerResult.Map
    $cwdLastCmdMap   = $callerResult.LastCmd

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
        $ageMin    = $age.TotalMinutes
        $ageStr    = if    ($age.TotalHours   -ge 1) { '{0:F1}h' -f $age.TotalHours   }
                     elseif ($age.TotalMinutes -ge 1) { '{0:F0}m' -f $age.TotalMinutes }
                     else                             { '{0:F0}s' -f $age.TotalSeconds  }

        $cmd       = $main.CommandLine
        $tgtPos    = if ($cmd -match '--window-position=(-?\d+),(-?\d+)') { "$($Matches[1]),$($Matches[2])" } else { '?' }
        $tgtSize   = if ($cmd -match '--window-size=(\d+),(\d+)')         { "$($Matches[1])x$($Matches[2])" } else { '?' }
        $startUrl  = if ($cmd -match '"(https?://[^"]+)"$')               { $Matches[1] }
                     elseif ($cmd -match "(https?://\S+)$")               { $Matches[1] } else { '' }

        # LAUNCHED-BY: walk parent chain to find the wkappbot command.
        # 1. Chrome parent alive + specific command in cmdline -> use that.
        # 2. Parent alive but is Eye server (\beye\b) or dead -> check per-command logs.
        # 3. Log file wkappbot-core.exe.out-*.CMD.pid=PID.log -> extract CMD.
        # 4. Fallback: infer from Chrome launch URL.
        $launchCmd = '(unknown)'

        # Step 1: check alive parent cmdline for specific ask/cdp subcommand
        $parentPid = $main.ParentProcessId
        $parentProc = Get-CimInstance Win32_Process -Filter "ProcessId=$parentPid" -ErrorAction SilentlyContinue
        if ($parentProc -and $parentProc.Name -like '*wkappbot*') {
            $cl = $parentProc.CommandLine
            if     ($cl -match '\bask.gpt\b|\bask-gpt\b')         { $launchCmd = 'ask gpt' }
            elseif ($cl -match '\bask.gemini\b|\bask-gemini\b')   { $launchCmd = 'ask gemini' }
            elseif ($cl -match '\bask.claude\b|\bask-claude\b')   { $launchCmd = 'ask claude' }
            elseif ($cl -match '\bask.triad\b|\bask-triad\b')     { $launchCmd = 'ask triad' }
            elseif ($cl -match '\bcdp.open\b|\bweb.open\b')       { $launchCmd = 'cdp open' }
            # If it's just Eye server, fall through to log/URL search
        }

        # Step 2: search per-command log files by parent PID chain (dead parents too)
        if ($launchCmd -eq '(unknown)') {
            $checkPids = @($parentPid)
            if ($parentProc) { $checkPids += $parentProc.ParentProcessId }
            foreach ($pid2 in $checkPids) {
                # Only match wkappbot-core command logs, not eye.diag or eye.pid files
                $logFile = Get-ChildItem "$HQ\logs" -Filter "wkappbot-core.exe.out-*.pid=$pid2.log" -ErrorAction SilentlyContinue |
                           Sort-Object LastWriteTime -Descending | Select-Object -First 1
                if ($logFile) {
                    # Extract command from filename: wkappbot-core.exe.out-YYYYMMDD_HHMMSS.CMD.pid=PID.log
                    $cmdFromLog = $logFile.BaseName -replace '^wkappbot-core\.exe\.out-\d+_\d+\.(.+)\.pid=\d+$','$1'
                    if ($cmdFromLog -notmatch 'eye$') {  # skip generic 'eye' command logs
                        $launchCmd = $cmdFromLog -replace '-',' '
                        break
                    }
                }
            }
        }

        # Step 3: fallback -- infer from Chrome launch URL (always reliable)
        if ($launchCmd -eq '(unknown)' -and $startUrl -ne '') {
            $launchCmd = if     ($startUrl -like '*chatgpt.com*')     { 'ask gpt' }
                         elseif ($startUrl -like '*gemini.google*')   { 'ask gemini' }
                         elseif ($startUrl -like '*claude.ai*')       { 'ask claude' }
                         elseif ($startUrl -like '*koreainvestment*') { 'cdp open hts' }
                         elseif ($startUrl -like '*smilegate*')       { 'cdp open smilegate' }
                         elseif ($startUrl -like '*careers.*')        { 'cdp open careers' }
                         else { 'cdp open ' + ($startUrl -replace '^https?://([^/?#]+).*','$1') }
        }

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
        $latAvg   = $cdp.LatAvg
        $latMin   = $cdp.LatMin
        $latMax   = $cdp.LatMax
        $pages    = @($cdp.Pages)   # full /json page objects: title, url, webSocketDebuggerUrl

        $cwd         = if ($portCwdMap.ContainsKey($port)) { $portCwdMap[$port] } else { '(unknown)' }
        $proj        = Split-Path $cwd -Leaf
        $normCwd     = $cwd.ToLowerInvariant()
        $caller      = if ($cwdCallerMap.ContainsKey($normCwd)) { $cwdCallerMap[$normCwd] } else { '' }
        $lastCmdTime = if ($cwdLastCmdMap.ContainsKey($normCwd)) { $cwdLastCmdMap[$normCwd] } else { $null }
        $idleMin     = if ($null -ne $lastCmdTime) { [math]::Round(((Get-Date) - $lastCmdTime).TotalMinutes, 1) } else { $null }

        [PSCustomObject]@{
            Port        = [int]$port
            MainPID     = $main.ProcessId
            Procs       = $procs.Count
            Renderers   = $rCount
            MemMB       = $totalMB
            Age         = $ageStr
            AgeMin      = $ageMin
            TgtPos      = $tgtPos
            TgtSize     = $tgtSize
            ActPos      = $actPos
            ActSize     = $actSize
            Drift       = $drift
            Offscreen   = $offscreen
            LatAvg      = $latAvg
            LatMin      = $latMin
            LatMax      = $latMax
            Pages       = $pages
            TabCount    = $pages.Count
            StartUrl    = $startUrl
            LaunchCmd   = $launchCmd
            CWD         = $cwd
            Proj        = $proj
            Caller      = $caller
            LastCmdTime = $lastCmdTime
            IdleMin     = $idleMin
        }
    }
}

# ── IME daemon health check + auto-kill duplicates ────────────────────────────
function Watch-ImeDaemon {
    $daemons = @(Get-CimInstance Win32_Process -Filter "Name='wkappbot-core.exe'" |
                 Where-Object { $_.CommandLine -like '*ime-relay-daemon*' })
    if ($daemons.Count -eq 0) {
        Write-Host "[IME] WARNING: no relay daemon running -- Han/Yeong toggle will not work" -ForegroundColor Yellow
        return
    }
    if ($daemons.Count -eq 1) {
        Write-Host "[IME] relay daemon OK  PID=$($daemons[0].ProcessId)  age=$([int]((Get-Date)-$daemons[0].CreationDate).TotalMinutes)m" -ForegroundColor Green
        return
    }
    # 2+ daemons: keep newest, kill the rest immediately
    Write-Host "[IME] $($daemons.Count) relay daemons detected -- killing extras (Korean toggle conflict)" -ForegroundColor Red
    $sorted = $daemons | Sort-Object CreationDate -Descending
    $keep   = $sorted[0]
    $kills  = $sorted[1..($sorted.Count-1)]
    foreach ($d in $kills) {
        try {
            $age = [int]((Get-Date)-$d.CreationDate).TotalMinutes
            Stop-Process -Id $d.ProcessId -Force -ErrorAction Stop
            Write-Host "  [KILLED] PID=$($d.ProcessId) age=${age}m (stale duplicate)" -ForegroundColor Red
        } catch { Write-Host "  [KILL FAIL] PID=$($d.ProcessId): $_" -ForegroundColor Yellow }
    }
    Write-Host "  [KEPT]   PID=$($keep.ProcessId) age=$([int]((Get-Date)-$keep.CreationDate).TotalMinutes)m (newest)" -ForegroundColor Green
}

# ── Display ───────────────────────────────────────────────────────────────────
function Show-Once {
    if (-not $nofix) { Watch-ImeDaemon }
    Write-Host ''
    $sessions = @(Get-WkCdpSessions)
    if (-not $sessions -or $sessions.Count -eq 0) {
        Write-Host '  (no wkappbot Chrome sessions running)' -ForegroundColor DarkGray
        return
    }

    # ── Build global URL duplicate map ───────────────────────────────────────
    $urlCount = @{}
    foreach ($s in $sessions) {
        foreach ($p in $s.Pages) {
            if ($p.url -and $p.url -notmatch '^chrome') {
                $k = $p.url -replace '\?.*',''   # ignore query string for dup detection
                if ($urlCount.ContainsKey($k)) { $urlCount[$k]++ } else { $urlCount[$k] = 1 }
            }
        }
    }

    $hdr = '{0,-6} {1,-7} {2,5} {3,6} {4,5}  {5,-13} {6,-13} {7,-10} {8,4}  {9,-14} {10,-14} {11}' -f `
           'PORT','PID','P/R','MEM(M)','AGE','TGT-POS','ACT-POS','DRIFT','TABS','LAT(avg/mn/mx)','LAUNCHED-BY','PROJECT (CWD)'
    Write-Host $hdr -ForegroundColor Cyan
    Write-Host ('-' * 135) -ForegroundColor DarkGray

    foreach ($s in $sessions) {
        $pr       = "$($s.Procs)/$($s.Renderers)"

        $latStr   = if ($s.LatAvg -lt 0) { 'DEAD' }
                    else { "$($s.LatAvg)/$($s.LatMin)/$($s.LatMax)" }
        $latColor = if ($s.LatAvg -lt 0)     { 'Red'    }
                    elseif ($s.LatAvg -gt 50) { 'Yellow' }
                    else                      { 'Green'  }

        $memColor = if ($s.MemMB -gt 2000) { 'Red' } elseif ($s.MemMB -gt 1000) { 'Yellow' } else { 'White' }
        $rowColor = if ($s.Offscreen)                      { 'Red'    }
                    elseif ($s.Drift -notin @('OK','n/a')) { 'Yellow' }
                    else                                   { 'White'  }

        $pre  = '{0,-6} {1,-7} {2,5} ' -f $s.Port, $s.MainPID, $pr
        $mem  = '{0,6} ' -f $s.MemMB
        $mid  = '{0,5}  {1,-13} {2,-13} {3,-10} {4,4}  ' -f `
                $s.Age, $s.TgtPos, $s.ActPos, $s.Drift, $s.TabCount
        $lat  = '{0,-14} ' -f $latStr
        $launch = '{0,-14} ' -f ($s.LaunchCmd.Substring(0, [Math]::Min(14, $s.LaunchCmd.Length)))
        $callerShort = if ($s.Caller) { $t = $s.Caller -replace '^\W+',''; "  [<< $($t.Substring(0,[Math]::Min(35,$t.Length)))]" } else { '' }
        $tail = "$($s.Proj)  ($($s.CWD))$callerShort"

        if ($rowColor -ne 'White') {
            Write-Host ($pre + $mem + $mid + $lat + $launch + $tail) -ForegroundColor $rowColor
        } else {
            Write-Host $pre    -NoNewline; Write-Host $mem    -NoNewline -ForegroundColor $memColor
            Write-Host $mid    -NoNewline; Write-Host $lat    -NoNewline -ForegroundColor $latColor
            Write-Host $launch -NoNewline -ForegroundColor DarkCyan
            Write-Host $tail
        }

        # ── Per-tab detail ────────────────────────────────────────────────────
        $showTabs = $tabs -or $s.TabCount -gt 0
        if ($showTabs -and $s.Pages.Count -gt 0) {
            foreach ($pg in $s.Pages) {
                $urlBase = $pg.url -replace '\?.*',''
                $isDup   = ($urlCount.ContainsKey($urlBase) -and $urlCount[$urlBase] -gt 1)
                $dupTag  = if ($isDup) { ' [DUP]' } else { '' }

                # Alert detection + idle time via WebSocket
                # Alert check: always run (fast 300ms timeout). Idle: only with -tabs flag.
                $idleTag  = ''
                $alertTag = ''
                if ($pg.webSocketDebuggerUrl) {
                    $wsTarget = $pg.webSocketDebuggerUrl -replace 'localhost','127.0.0.1'
                    $idle = Get-TabIdle $wsTarget
                    if ($null -eq $idle) {
                        # connect failed -- tab gone/crashed
                    } elseif ($idle.Alert) {
                        $alertTag = ' [ALERT]'
                    } elseif ($tabs) {
                        $vis    = if ($idle.Hidden) { 'bg' } else { 'fg' }
                        $ageSec = $idle.AgeSec
                        $ageDisp = if ($ageSec -gt 3600) { '{0:F1}h' -f ($ageSec/3600) }
                                   elseif ($ageSec -gt 60) { '{0}m' -f [int]($ageSec/60) }
                                   else { "${ageSec}s" }
                        $idleTag = "  [$vis age:$ageDisp]"
                    }
                }

                $title    = if ($pg.title) { $pg.title } else { $pg.url }
                $tabLine  = "         > $title$dupTag$alertTag$idleTag"
                $tabColor = if ($alertTag)  { 'Red'    }
                            elseif ($isDup) { 'Yellow' }
                            else            { 'DarkGray' }
                Write-Host $tabLine -ForegroundColor $tabColor
            }
        } elseif (-not $tabs -and $s.TabCount -gt 0 -and $s.StartUrl) {
            Write-Host "         > $($s.StartUrl)" -ForegroundColor DarkGray
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

    # ── Anomaly report ────────────────────────────────────────────────────────
    $warns = @()

    # Too many Chrome sessions total
    if ($sessions.Count -gt 5)  { $warns += "[WARN] $($sessions.Count) Chrome sessions running (>5) -- sandbox-miss accumulation likely" }

    foreach ($s in $sessions) {
        $p = $s.Port
        # Off-screen target
        if ($s.Offscreen) { $warns += "[CRIT] Port $p  off-screen TGT-POS $($s.TgtPos) -- Chrome will drift or be invisible" }
        # Dead CDP
        if ($s.LatAvg -lt 0) { $warns += "[CRIT] Port $p  LAT=DEAD -- Chrome not responding to CDP" }
        # High latency
        if ($s.LatAvg -gt 80) { $warns += "[WARN] Port $p  LAT avg $($s.LatAvg)ms (>80) -- Chrome slow/overloaded" }
        # Memory
        if ($s.MemMB -gt 2000) { $warns += "[CRIT] Port $p  MEM $($s.MemMB)MB (>2GB) -- kill and restart" }
        elseif ($s.MemMB -gt 1000) { $warns += "[WARN] Port $p  MEM $($s.MemMB)MB (>1GB)" }
        # Tab overload
        if ($s.TabCount -gt 6) { $warns += "[WARN] Port $p  $($s.TabCount) tabs (>6) -- sandbox-miss accumulation" }
        # Unknown CWD
        if ($s.CWD -eq '(unknown)') { $warns += "[INFO] Port $p  CWD unknown -- port-to-CWD mapping gap" }
        # Old session
        $ageH = [math]::Round(([int]($s.Age -replace '[^0-9.]','') * $(if ($s.Age -match 'h') {1} elseif ($s.Age -match 'm') {1/60} else {1/3600})), 1)
        if ($s.Age -match 'h' -and [float]($s.Age -replace 'h','') -gt 10) {
            $warns += "[WARN] Port $p  age $($s.Age) (>10h) -- consider recycling"
        }
        # Position drift
        if ($s.Drift -notin @('OK','n/a') -and $s.Drift -match 'd') {
            $nums = [regex]::Matches($s.Drift, '-?\d+')
            if ($nums.Count -ge 2) {
                $dx = [math]::Abs([int]$nums[0].Value); $dy = [math]::Abs([int]$nums[1].Value)
                if ($dx -gt 200 -or $dy -gt 200) {
                    $warns += "[WARN] Port $p  position drift $($s.Drift) (>200px)"
                }
            }
        }
        # Alert-blocked tabs
        $alertTabs = @($s.Pages | Where-Object { $_.webSocketDebuggerUrl } | ForEach-Object {
            $idle = Get-TabIdle ($_.webSocketDebuggerUrl -replace 'localhost','127.0.0.1')
            if ($idle -and $idle.Alert) { $_ }
        })
        if ($alertTabs.Count -gt 0) {
            $warns += "[CRIT] Port $p  $($alertTabs.Count) tab(s) blocked by JS alert -- next ask will timeout"
        }
        # Dup tabs
        $dupCount = @($s.Pages | Where-Object {
            $k = ($_.url -replace '\?.*','')
            ($urlCount.ContainsKey($k) -and $urlCount[$k] -gt 1)
        }).Count
        if ($dupCount -gt 2) { $warns += "[WARN] Port $p  $dupCount duplicate URL tabs" }

        # Idle Chrome detection: warn if no wkappbot cmd in >10min (always shown)
        # If -killIdle N specified, auto-kill sessions idle > N min
        $idleThresholdWarn = 10
        $im = $s.IdleMin
        if ($null -ne $im -and $im -ge $idleThresholdWarn) {
            $tsStr = if ($s.LastCmdTime) { $s.LastCmdTime.ToString('HH:mm:ss') } else { '?' }
            $killCmd = "wkappbot taskkill /F /PID $($s.MainPID)"
            $warns += "[WARN] Port $p  idle ${im}min (last wkappbot cmd: $tsStr) -- Kill: $killCmd"
        }
        if ($killIdle -gt 0 -and $null -ne $im -and $im -ge $killIdle) {
            try {
                & wkappbot taskkill /F /PID $s.MainPID
                $warns += "[KILLED] Port $p  PID=$($s.MainPID) idle ${im}min (>= threshold ${killIdle}min)"
            } catch {
                $warns += "[KILL-FAIL] Port $p  PID=$($s.MainPID): $_"
            }
        }
    }

    if ($warns.Count -gt 0) {
        Write-Host ''
        Write-Host ('=' * 72) -ForegroundColor DarkGray
        Write-Host "  ANOMALY REPORT" -ForegroundColor Cyan
        Write-Host ('=' * 72) -ForegroundColor DarkGray
        foreach ($w in $warns) {
            $col = if ($w -match '^\[CRIT\]') { 'Red' } elseif ($w -match '^\[WARN\]') { 'Yellow' } else { 'DarkGray' }
            Write-Host "  $w" -ForegroundColor $col
        }
        Write-Host ('=' * 72) -ForegroundColor DarkGray
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
