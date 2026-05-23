<#
.SYNOPSIS
    wkjobs - Show AI processes and their spawned child commands in this console session.
.DESCRIPTION
    Lists all AI processes (claude, codex, wkappbot, wkappbot-core) sharing the current
    console session, with CPU%, memory, thread count, idle/busy state, and command lines.
    A separate section shows non-AI child processes directly spawned by AI pids.
.PARAMETER Tree
    Show process hierarchy with branch characters.
.PARAMETER Watch
    Live-refresh mode (Ctrl+C to exit).
.PARAMETER All
    Include AI processes from all sessions, not just the current console session.
.PARAMETER RefreshSec
    Refresh interval in seconds for -Watch mode. Default: 2.
#>
param(
    [switch]$Tree,
    [switch]$Watch,
    [switch]$All,
    [int]$RefreshSec = 2
)

$AI_NAMES = @('claude', 'codex', 'wkappbot', 'wkappbot-core', 'wkappbot-core.new', 'wkappbot-eye', 'wka11y')

function Get-AiJobs {
    # Current console session id
    $mySession = (Get-Process -Id $PID).SessionId

    # All Win32 processes for CommandLine + PPID + CreationDate
    $allWmi = Get-CimInstance Win32_Process | Select-Object ProcessId, ParentProcessId, Name, CommandLine, CreationDate

    # CPU% via perf counter (snapshot; no sleep needed — already accumulated)
    $perfMap = @{}
    try {
        $perf = Get-CimInstance Win32_PerfFormattedData_PerfProc_Process |
                Select-Object IDProcess, PercentProcessorTime
        foreach ($p in $perf) { $perfMap[[int]$p.IDProcess] = [int]$p.PercentProcessorTime }
    } catch { }

    # Get-Process for WorkingSet + Threads + SessionId
    $gpMap = @{}
    Get-Process -ErrorAction SilentlyContinue | ForEach-Object {
        $gpMap[$_.Id] = $_
    }

    # Identify AI pids
    $wmiMap = @{}
    foreach ($w in $allWmi) { $wmiMap[[int]$w.ProcessId] = $w }

    $aiPids = [System.Collections.Generic.HashSet[int]]::new()
    foreach ($w in $allWmi) {
        $baseName = [System.IO.Path]::GetFileNameWithoutExtension($w.Name).ToLower()
        if ($AI_NAMES -contains $baseName) {
            $gp = $gpMap[[int]$w.ProcessId]
            if ($All -or ($gp -and $gp.SessionId -eq $mySession)) {
                $aiPids.Add([int]$w.ProcessId) | Out-Null
            }
        }
    }

    # Build AI rows
    $aiRows = @()
    foreach ($apid in ($aiPids | Sort-Object)) {
        $w   = $wmiMap[$apid]
        $gp  = $gpMap[$apid]
        if (-not $w) { continue }

        $cpu  = if ($perfMap.ContainsKey($apid)) { $perfMap[$apid] } else { 0 }
        $mem  = if ($gp) { [math]::Round($gp.WorkingSet64 / 1MB, 0) } else { 0 }
        $thr  = if ($gp) { $gp.Threads.Count } else { 0 }
        $sess = if ($gp) { $gp.SessionId } else { -1 }
        $cur  = ($sess -eq $mySession)
        $ppid = [int]$w.ParentProcessId

        $state = if ($cpu -gt 20) { 'BUSY' } elseif ($cpu -ge 1) { 'active' } else { 'idle' }

        $aiRows += [PSCustomObject]@{
            PID      = $apid
            PPID     = $ppid
            Name     = [System.IO.Path]::GetFileNameWithoutExtension($w.Name)
            CPU      = $cpu
            MemMB    = $mem
            Threads  = $thr
            State    = $state
            Current  = $cur
            Session  = $sess
            Cmd      = if ($w.CommandLine) { $w.CommandLine } else { $w.Name }
            Created  = $w.CreationDate
        }
    }

    # Build spawned (non-AI children of AI pids)
    $spawnedRows = @()
    $SPAWNED_SKIP = @('conhost', 'conhost.exe')
    foreach ($w in $allWmi) {
        $baseName = [System.IO.Path]::GetFileNameWithoutExtension($w.Name).ToLower()
        if ($AI_NAMES -contains $baseName) { continue }
        if ($SPAWNED_SKIP -contains $baseName) { continue }
        $wpid  = [int]$w.ProcessId
        $wppid = [int]$w.ParentProcessId
        if (-not $aiPids.Contains($wppid)) { continue }
        $gp  = $gpMap[$wpid]
        $cpu = if ($perfMap.ContainsKey($wpid)) { $perfMap[$wpid] } else { 0 }
        $mem = if ($gp) { [math]::Round($gp.WorkingSet64 / 1MB, 0) } else { 0 }
        $state = if ($cpu -gt 20) { 'BUSY' } elseif ($cpu -ge 1) { 'active' } else { 'idle' }
        $spawnedRows += [PSCustomObject]@{
            PID      = $wpid
            PPID     = $wppid
            Name     = [System.IO.Path]::GetFileNameWithoutExtension($w.Name)
            CPU      = $cpu
            MemMB    = $mem
            State    = $state
            Cmd      = if ($w.CommandLine) { $w.CommandLine } else { $w.Name }
            Created  = $w.CreationDate
        }
    }

    return $aiRows, $spawnedRows
}

function Get-StateColor($row) {
    $n = $row.Name.ToLower()
    if ($row.State -eq 'BUSY') {
        if ($n -like 'claude*')              { return 'Green' }
        if ($n -like 'codex*')              { return 'Yellow' }
        if ($n -like 'wkappbot-core.new*')  { return 'Magenta' }
        return 'Cyan'
    }
    if ($n -like 'claude*')                 { return 'Cyan' }
    if ($n -like 'codex*')                  { return 'DarkYellow' }
    if ($n -like 'wkappbot-core.new*')      { return 'DarkMagenta' }
    if ($n -like 'wkappbot-core*')          { return 'DarkGray' }
    return 'White'
}

function Truncate($s, $maxLen) {
    if (-not $s) { return '' }
    if ($s.Length -le $maxLen) { return $s }
    return $s.Substring(0, $maxLen - 3) + '...'
}

function Show-Jobs {
    $aiRows, $spawnedRows = Get-AiJobs

    $w = $Host.UI.RawUI.WindowSize.Width
    $cmdWidth = [math]::Max(40, $w - 62)

    # Header
    Write-Host ''
    Write-Host ('  AI PROCESSES  [session=' + (Get-Process -Id $PID).SessionId + ']  ' + (Get-Date -Format 'HH:mm:ss')) -ForegroundColor White
    Write-Host ('  {0,-6} {1,-22} {2,5} {3,7} {4,4} {5,-7} {6}' -f 'PID','NAME','CPU%','MEM(MB)','THR','STATE','COMMAND') -ForegroundColor DarkCyan
    Write-Host ('  ' + ('-' * ([math]::Min($w - 4, 100)))) -ForegroundColor DarkGray

    if ($aiRows.Count -eq 0) {
        Write-Host '  (no AI processes found)' -ForegroundColor DarkGray
    }

    if ($Tree) {
        # Build parent->children map for AI pids
        $aiPidSet = [System.Collections.Generic.HashSet[int]]($aiRows | ForEach-Object { $_.PID })
        $children = @{}
        foreach ($r in $aiRows) {
            if (-not $children.ContainsKey($r.PPID)) { $children[$r.PPID] = @() }
            $children[$r.PPID] += $r
        }
        # Roots = AI pids whose PPID is not in aiPidSet
        $roots = $aiRows | Where-Object { -not $aiPidSet.Contains($_.PPID) } | Sort-Object PID

        function Print-Tree($rows, $prefix) {
            for ($i = 0; $i -lt $rows.Count; $i++) {
                $r = $rows[$i]
                $isLast = ($i -eq $rows.Count - 1)
                $branch = if ($isLast) { '`-- ' } else { '+-- ' }
                $cur = if ($r.Current) { '*' } else { ' ' }
                $color = Get-StateColor $r
                $label = '{0}{1}{2,-18} {3,5}% {4,7}MB {5,4}t {6,-7}' -f $cur, $branch, $r.Name, $r.CPU, $r.MemMB, $r.Threads, $r.State
                $cmd   = Truncate $r.Cmd ($cmdWidth - $prefix.Length - 4)
                Write-Host ('  ' + $prefix + $label + ' ' + $cmd) -ForegroundColor $color
                $sub = if ($children.ContainsKey($r.PID)) { $children[$r.PID] | Sort-Object PID } else { @() }
                if ($sub.Count -gt 0) {
                    $newPrefix = $prefix + (if ($isLast) { '    ' } else { '|   ' })
                    Print-Tree $sub $newPrefix
                }
            }
        }
        Print-Tree $roots ''
    } else {
        foreach ($r in ($aiRows | Sort-Object PID)) {
            $cur   = if ($r.Current) { '*' } else { ' ' }
            $color = Get-StateColor $r
            $cmd   = Truncate $r.Cmd $cmdWidth
            $line  = '  {0}{1,-6} {2,-22} {3,5} {4,7} {5,4} {6,-7} {7}' -f $cur, $r.PID, $r.Name, $r.CPU, $r.MemMB, $r.Threads, $r.State, $cmd
            Write-Host $line -ForegroundColor $color
        }
    }

    # Spawned section
    Write-Host ''
    Write-Host '  SPAWNED BY AI' -ForegroundColor White
    Write-Host ('  {0,-6} {1,-22} {2,-6} {3,5} {4,7} {5,-7} {6}' -f 'PPID','NAME','PID','CPU%','MEM(MB)','STATE','COMMAND') -ForegroundColor DarkCyan
    Write-Host ('  ' + ('-' * ([math]::Min($w - 4, 100)))) -ForegroundColor DarkGray

    if ($spawnedRows.Count -eq 0) {
        Write-Host '  (none)' -ForegroundColor DarkGray
    } else {
        foreach ($r in ($spawnedRows | Sort-Object PPID, PID)) {
            $color = if ($r.State -eq 'BUSY') { 'Yellow' } elseif ($r.State -eq 'active') { 'White' } else { 'DarkGray' }
            $cmd   = Truncate $r.Cmd $cmdWidth
            $line  = '  {0,-6} {1,-22} {2,-6} {3,5} {4,7} {5,-7} {6}' -f $r.PPID, $r.Name, $r.PID, $r.CPU, $r.MemMB, $r.State, $cmd
            Write-Host $line -ForegroundColor $color
        }
    }
    Write-Host ''
}

if ($Watch) {
    while ($true) {
        Clear-Host
        Show-Jobs
        Write-Host "  [Watch mode - Ctrl+C to exit, refresh: ${RefreshSec}s]" -ForegroundColor DarkGray
        Start-Sleep -Seconds $RefreshSec
    }
} else {
    Show-Jobs
}

exit 0
