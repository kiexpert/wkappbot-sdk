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
    [switch]$Kill,       # Kill zombie processes (safe ones only; prompts unless -Force)
    [switch]$Force,      # Skip confirmation in -Kill mode
    [int]$ZombieMin = 30, # Minutes idle before considered zombie
    [int]$RefreshSec = 2
)

$AI_NAMES = @('claude', 'codex', 'wkappbot', 'wkappbot-core', 'wkappbot-core.new', 'wkappbot-eye', 'wka11y')
function Format-Age([datetime]$created) {
    $span = (Get-Date) - $created
    if ($span.TotalHours -ge 1) { return '{0}h{1:D2}m' -f [int]$span.TotalHours, $span.Minutes }
    return '{0}m' -f [int]$span.TotalMinutes
}

# Safe-to-kill: wkappbot helper cmds (skill/suggest/--version) that are idle+old
# NEVER kill: wkappbot-core.*, wkchat, claude, codex, eye, chat, guardian, mcp
function Test-SafeToKill($r) {
    $name = $r.Name.ToLower()
    if ($name -ne 'wkappbot') { return $false }         # only wkappbot.exe helpers
    $cmd = if ($r.Cmd) { $r.Cmd.ToLower() } else { '' }
    if ($cmd -match '\bchat\b|\beye\b|\bguardian\b|\bmcp\b|\bspeak\b') { return $false }
    if ($cmd -match '\bskill\b|\bsuggest\b|\bask\b|\b--version\b|\bfile\b|\bwindows\b|\blogcat\b') { return $true }
    return $false
}
# --- Native CWD reader via NtQueryInformationProcess ---
if (-not ([System.Management.Automation.PSTypeName]'WkJobsNative').Type) {
    Add-Type @"
using System;
using System.Runtime.InteropServices;
using System.Text;
public class WkJobsNative {
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_BASIC_INFORMATION {
        public IntPtr Reserved1; public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0; public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId; public IntPtr Reserved3;
    }
    [DllImport("ntdll.dll")]
    public static extern int NtQueryInformationProcess(IntPtr h, int pic, ref PROCESS_BASIC_INFORMATION pbi, uint cb, out uint r);
    [DllImport("kernel32.dll")] public static extern IntPtr OpenProcess(uint access, bool inh, int pid);
    [DllImport("kernel32.dll")] public static extern bool ReadProcessMemory(IntPtr h, IntPtr addr, byte[] buf, int n, out IntPtr read);
    [DllImport("kernel32.dll")] public static extern bool CloseHandle(IntPtr h);
    public static string GetCwd(int pid) {
        const uint VM_READ = 0x10; const uint QUERY_INFO = 0x400;
        IntPtr hProc = IntPtr.Zero;
        try {
            hProc = OpenProcess(VM_READ | QUERY_INFO, false, pid);
            if (hProc == IntPtr.Zero) return "";
            var pbi = new PROCESS_BASIC_INFORMATION(); uint retLen;
            if (NtQueryInformationProcess(hProc, 0, ref pbi, (uint)Marshal.SizeOf(pbi), out retLen) != 0) return "";
            byte[] peb = new byte[0x28]; IntPtr br;
            if (!ReadProcessMemory(hProc, pbi.PebBaseAddress, peb, peb.Length, out br)) return "";
            IntPtr ppAddr = (IntPtr)BitConverter.ToInt64(peb, 0x20);
            byte[] pp = new byte[0x60];
            if (!ReadProcessMemory(hProc, ppAddr, pp, pp.Length, out br)) return "";
            // CurrentDirectory.DosPath UNICODE_STRING at offset 0x38: Length@0x38, Buffer@0x40
            int cwdLen = BitConverter.ToInt16(pp, 0x38);
            if (cwdLen <= 0 || cwdLen > 520) return "";
            IntPtr cwdBuf = (IntPtr)BitConverter.ToInt64(pp, 0x40);
            byte[] cwdBytes = new byte[cwdLen];
            if (!ReadProcessMemory(hProc, cwdBuf, cwdBytes, cwdLen, out br)) return "";
            return Encoding.Unicode.GetString(cwdBytes).TrimEnd('\\', '\0');
        } catch { return ""; }
        finally { if (hProc != IntPtr.Zero) CloseHandle(hProc); }
    }
}
"@
}

function Get-ProcessCwd([int]$procId) {
    try { [WkJobsNative]::GetCwd($procId) } catch { "" }
}

function Get-TerminalHost([int]$procId, [hashtable]$wmiMap, [hashtable]$gpMap) {
    # Walk parent chain: skip wkappbot*/conhost, return first "interesting" terminal ancestor
    $SKIP = @('wkappbot','wkappbot-core','wkappbot-core.new','wkappbot-eye','claude','codex','conhost','wka11y')
    $visited = [System.Collections.Generic.HashSet[int]]::new()
    $cur = $procId
    for ($i = 0; $i -lt 12; $i++) {
        if (-not $visited.Add($cur)) { break }
        $w = $wmiMap[$cur]; if (-not $w) { break }
        $ppid = [int]$w.ParentProcessId
        if ($ppid -le 4) { break }
        $pw = $wmiMap[$ppid]; if (-not $pw) { break }
        $pname = [System.IO.Path]::GetFileNameWithoutExtension($pw.Name).ToLower()
        if (-not ($SKIP -contains $pname)) {
            # Found interesting ancestor — also check if WindowsTerminal is in remaining chain
            $termTitle = ""
            $pgp = $gpMap[$ppid]
            if ($pgp -and $pgp.MainWindowTitle) { $termTitle = " [" + $pgp.MainWindowTitle.Substring(0,[Math]::Min(35,$pgp.MainWindowTitle.Length)) + "]" }
            return ($pw.Name.TrimEnd('exe').TrimEnd('.') + "(${ppid})" + $termTitle)
        }
        $cur = $ppid
    }
    return ""
}

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

    # Build wmiMap + children map for subtree traversal
    $wmiMap = @{}
    foreach ($w in $allWmi) { $wmiMap[[int]$w.ProcessId] = $w }
    $childMap = @{}
    foreach ($w in $allWmi) {
        $pp = [int]$w.ParentProcessId
        if (-not $childMap.ContainsKey($pp)) { $childMap[$pp] = [System.Collections.Generic.List[int]]::new() }
        $childMap[$pp].Add([int]$w.ProcessId)
    }

    # Find my session root: walk $PID up to first terminal/wkchat ancestor
    $mySubtree = $null
    if (-not $All) {
        $TERM_NAMES = @('wkchat','cmd','pwsh','windowsterminal','wt')
        # Start from parent of $PID (skip self -- wkjobs is a powershell, not the terminal)
        $w0 = $wmiMap[$PID]
        $cur = if ($w0) { [int]$w0.ParentProcessId } else { $PID }
        $sessionRoot = $cur
        for ($i = 0; $i -lt 15; $i++) {
            $w = $wmiMap[$cur]; if (-not $w) { break }
            $nm = [System.IO.Path]::GetFileNameWithoutExtension($w.Name).ToLower()
            if ($TERM_NAMES -contains $nm) { $sessionRoot = $cur; break }
            $par = [int]$w.ParentProcessId; if ($par -le 4) { break }
            $cur = $par; $sessionRoot = $cur
        }
        # BFS to collect all descendants of sessionRoot
        $mySubtree = [System.Collections.Generic.HashSet[int]]::new()
        $q = [System.Collections.Generic.Queue[int]]::new()
        $q.Enqueue($sessionRoot)
        while ($q.Count -gt 0) {
            $n = $q.Dequeue()
            if ($mySubtree.Add($n) -and $childMap.ContainsKey($n)) {
                foreach ($ch in $childMap[$n]) { $q.Enqueue($ch) }
            }
        }
    }

    # Identify AI pids: filter by subtree (default) or session (--All)
    $aiPids = [System.Collections.Generic.HashSet[int]]::new()
    foreach ($w in $allWmi) {
        $baseName = [System.IO.Path]::GetFileNameWithoutExtension($w.Name).ToLower()
        if ($AI_NAMES -contains $baseName) {
            $wpid = [int]$w.ProcessId
            $inScope = if ($All) {
                $gp = $gpMap[$wpid]; $gp -and $gp.SessionId -eq $mySession
            } else {
                $mySubtree.Contains($wpid)
            }
            if ($inScope) { $aiPids.Add($wpid) | Out-Null }
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
            Cwd      = (Get-ProcessCwd $apid)
            Terminal = (Get-TerminalHost $apid $wmiMap $gpMap)
            Age      = if ($w.CreationDate) { Format-Age $w.CreationDate } else { "?" }
            AgeMin   = if ($w.CreationDate) { [int]((Get-Date) - $w.CreationDate).TotalMinutes } else { 0 }
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
            Cwd      = (Get-ProcessCwd $apid)
            Terminal = (Get-TerminalHost $apid $wmiMap $gpMap)
            Age      = if ($w.CreationDate) { Format-Age $w.CreationDate } else { "?" }
            AgeMin   = if ($w.CreationDate) { [int]((Get-Date) - $w.CreationDate).TotalMinutes } else { 0 }
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
    Write-Host ('  {0,-6} {1,-22} {2,5} {3,7} {4,4} {5,-7} {6,6}  {7}' -f 'PID','NAME','CPU%','MEM(MB)','THR','STATE','AGE','CWD  (CMD/CON below)') -ForegroundColor DarkCyan
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
            $isZombie  = ($r.State -eq 'idle' -and $r.AgeMin -ge $ZombieMin)
            $isSafe    = $isZombie -and (Test-SafeToKill $r)
            $color = if ($isSafe)    { 'Red' }
                     elseif ($isZombie) { 'DarkRed' }
                     else { Get-StateColor $r }
            $ageStr = $r.Age
            $zombie = if ($isSafe) { '[ZOMBIE]' } elseif ($isZombie) { '[old]' } else { '' }
            $line  = '  {0}{1,-6} {2,-22} {3,5} {4,7} {5,4} {6,-7} {7,6}{8}' -f $cur, $r.PID, $r.Name, $r.CPU, $r.MemMB, $r.Threads, $r.State, $ageStr, $zombie
            Write-Host $line -ForegroundColor $color -NoNewline
            if ($r.Cwd)      { Write-Host ('  ' + $r.Cwd) -ForegroundColor DarkCyan -NoNewline }
            Write-Host ""
            $cmdColor = if ($isSafe) { 'DarkRed' } else { 'DarkGray' }
            if ($r.Cmd)      { Write-Host ('         CMD: ' + $r.Cmd) -ForegroundColor $cmdColor }
            if ($r.Terminal) { Write-Host ('         CON: ' + $r.Terminal) -ForegroundColor DarkYellow }
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


function Invoke-KillZombies {
    $aiRows, $null2 = Get-AiJobs
    $targets = $aiRows | Where-Object { $_.State -eq 'idle' -and $_.AgeMin -ge $ZombieMin -and (Test-SafeToKill $_) }
    if ($targets.Count -eq 0) {
        Write-Host "  No zombie processes to kill (threshold: ${ZombieMin}m)." -ForegroundColor Green
        return
    }
    Write-Host ""
    Write-Host "  ZOMBIE CANDIDATES (idle >= ${ZombieMin}m, safe wkappbot helpers):" -ForegroundColor Red
    Write-Host ("  {0,-6} {1,-22} {2,6}  {3}" -f "PID","NAME","AGE","CMD") -ForegroundColor DarkCyan
    Write-Host ("  " + "-"*80) -ForegroundColor DarkGray
    foreach ($r in ($targets | Sort-Object AgeMin -Descending)) {
        $cmd = if ($r.Cmd) { $r.Cmd.Substring(0, [Math]::Min(70, $r.Cmd.Length)) } else { "" }
        Write-Host ("  {0,-6} {1,-22} {2,6}  {3}" -f $r.PID, $r.Name, $r.Age, $cmd) -ForegroundColor Red
    }
    Write-Host ""
    if (-not $Force) {
        $ans = Read-Host "  Kill $($targets.Count) process(es)? [y/N]"
        if ($ans -notmatch '^[yY]') { Write-Host "  Cancelled." -ForegroundColor DarkGray; return }
    }
    $pids = ($targets | ForEach-Object { $_.PID }) -join ','
    & wkappbot taskkill --force $pids.Split(',')
}
if ($Kill) {
    Invoke-KillZombies
} elseif ($Watch) {
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
