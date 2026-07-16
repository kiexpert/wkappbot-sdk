# wkdoctor check: eye-staleness -- detect (and, gated by -EmergencyKill, heal) a STUCK/STALE Eye.
#
# LAYER 3 of the Eye-staleness defense. Runs in PARALLEL with ROOT's LAYER 2 (Eye self-heal /
# self-retire on hot-swap failure). This layer is the OUTER safety net: if an Eye fails to
# hot-swap and keeps running stale/tainted code, this doctor plugin finds it and (only when
# EmergencyKill is explicitly requested) clears it via an OS-level kill so the mutex-guarded
# guardian (Global\WKAppBotEyeGuardian, single-flight -- see AppBotEyeCommands.cs) respawns a
# fresh Eye from the on-disk binary.
#
# PROTO FILE: the wkdoctor loader excludes '*-proto.ps1' from live runs
#   (Get-ChildItem $doctorDir -Filter '*.ps1' | Where-Object { $_.Name -notmatch '-proto\.ps1$' })
# so this file is inert until ROOT reviews it and promotes it to '55-stale-eye-heal.ps1'.
#
# DETECTION HEURISTIC (live-proven 2026-07-16) -- ALL THREE must hold:
#   1. StartTime of the wkappbot-core.exe process is OLDER than the deployed
#      wkappbot-core.exe binary's LastWriteTime (it started before the binary it should now be
#      running was deployed -- i.e. it never picked up the newer build via hot-swap).
#   2. Uptime > 1h (rules out a binary that was JUST deployed under an Eye that simply hasn't
#      had its next scheduled hot-swap tick yet -- give the normal hot-swap path time to work).
#   3. HandleCount > 200 (the full Eye daemon -- FSW watchers, pipe servers, overlay window,
#      Slack/MCP broker -- has a large handle footprint; this excludes the lightweight guardian
#      poll-loop process and short-lived detached MCP worker subprocesses, which also run
#      wkappbot-core.exe but are not the thing we want to touch).
#
# WHY OS taskkill, not `wkappbot taskkill`: a stale Eye is -- by construction -- running code
# older than the currently-deployed binary, so it is a "foreign-tree" process from the current
# session's point of view, and `wkappbot taskkill` REFUSES to act on a foreign-tree Eye. The
# only way to actually clear a stuck stale Eye is the raw OS kill: taskkill.exe /F /PID.
#
# WMI-STORM DISCIPLINE (wktasklist-intelligent-management): enumerate ONLY via
# `Get-Process -Name wkappbot-core`. NEVER Get-CimInstance/Get-WmiObject Win32_Process here.
#
# SANCTUARY: never act on a process that fails ANY of the 3 heuristic conditions above. The
# guardian and MCP workers are meant to be excluded by the Handles>200 gate (condition 3) since
# they are not the full Eye daemon. RESIDUAL RISK (flagged for ROOT review): Get-Process cannot
# read a process's command line without a WMI/CIM query, which is itself forbidden here by
# WMI-storm discipline -- so this proto cannot positively confirm "this pid is NOT the guardian"
# by command line the way `wkappbot taskkill`'s own classifier does. The 3-condition
# AND-heuristic is a best-effort proxy, not a certainty. If ROOT wants a harder guarantee before
# promoting this proto to live -EmergencyKill behavior, consider reading the guardian's own PID
# out of a marker it could optionally drop next to the eye pid log (wkappbot.hq/logs), or reuse
# the existing native (non-WMI) GetProcessCommandLine helper already used by
# cdp-open-reuse-existing-chrome-tab, exposed as a wkappbot CLI verb.
#
# FAIL-OPEN: every block wrapped in try/catch. A doctor plugin must never throw.

$eyeStalenessBinaryMtime = $null
$eyeStalenessBinaryOk = $false
try {
    $eyeStalenessCorePath = Join-Path $binDir 'wkappbot-core.exe'
    if (Test-Path $eyeStalenessCorePath -PathType Leaf) {
        $eyeStalenessBinaryMtime = (Get-Item $eyeStalenessCorePath -ErrorAction Stop).LastWriteTime
        $eyeStalenessBinaryOk = $true
    } else {
        Add-Check 'eye-staleness' 'warn' "wkappbot-core.exe not found at $eyeStalenessCorePath -- cannot compare"
        Emit '!' 'eye-staleness' "binary not found at $eyeStalenessCorePath"
    }
} catch {
    Add-Check 'eye-staleness' 'warn' "could not read deployed binary mtime: $($_.Exception.Message)"
    Emit '!' 'eye-staleness' "could not read deployed binary mtime: $($_.Exception.Message)"
}

if ($eyeStalenessBinaryOk) {
    $eyeStalenessCandidates = New-Object System.Collections.Generic.List[PSCustomObject]
    $eyeStalenessEnumOk = $true
    try {
        $eyeStalenessProcs = Get-Process -Name 'wkappbot-core' -ErrorAction SilentlyContinue
        if ($null -eq $eyeStalenessProcs) { $eyeStalenessProcs = @() }
        $eyeStalenessNow = Get-Date

        foreach ($p in @($eyeStalenessProcs)) {
            try {
                $start = $null
                try { $start = $p.StartTime } catch { $start = $null }
                if ($null -eq $start) { continue }  # access-denied / exited between enum and read -- skip, never guess

                $handles = 0
                try { $handles = [int]$p.HandleCount } catch { $handles = 0 }

                $uptimeHrs        = ($eyeStalenessNow - $start).TotalHours
                $isOlderThanBuild = $start -lt $eyeStalenessBinaryMtime
                $isLongRunning    = $uptimeHrs -gt 1
                $looksLikeFullEye = $handles -gt 200

                if ($isOlderThanBuild -and $isLongRunning -and $looksLikeFullEye) {
                    $eyeStalenessCandidates.Add([PSCustomObject]@{
                        Pid     = $p.Id
                        Start   = $start
                        Uptime  = [math]::Round($uptimeHrs, 1)
                        Handles = $handles
                    })
                }
            } catch { continue }  # never let one bad process entry abort the scan
        }
    } catch {
        $eyeStalenessEnumOk = $false
        Add-Check 'eye-staleness' 'warn' "Get-Process enumeration failed: $($_.Exception.Message)"
        Emit '!' 'eye-staleness' "Get-Process enumeration failed: $($_.Exception.Message)"
    }

    if ($eyeStalenessEnumOk) {
        if ($eyeStalenessCandidates.Count -eq 0) {
            Add-Check 'eye-staleness' 'ok' 'no stale Eye'
            Emit 'ok' 'eye-staleness' 'no stale Eye'
        } else {
            foreach ($c in $eyeStalenessCandidates) {
                $detail = "stale Eye pid=$($c.Pid) started $($c.Start) < binary $eyeStalenessBinaryMtime (uptime=$($c.Uptime)h handles=$($c.Handles))"
                Add-Check 'eye-staleness' 'warn' $detail
                Emit '!' 'eye-staleness' $detail

                if ($EmergencyKill) {
                    try {
                        # OS-level kill only -- `wkappbot taskkill` refuses a foreign-tree (stale) Eye.
                        $killProc = Start-Process -FilePath 'C:\Windows\System32\taskkill.exe' `
                            -ArgumentList @('/F', '/PID', "$($c.Pid)") `
                            -NoNewWindow -Wait -PassThru -ErrorAction Stop
                        if ($killProc -and $killProc.ExitCode -eq 0) {
                            Add-Check 'eye-staleness:heal' 'ok' "OS-killed stale Eye pid=$($c.Pid) (taskkill exit=0)"
                            Emit 'ok' 'eye-staleness:heal' "killed stale Eye pid=$($c.Pid) -- guardian (single-flight Global\WKAppBotEyeGuardian mutex) should respawn a fresh Eye from the on-disk binary"
                        } else {
                            $code = if ($killProc) { $killProc.ExitCode } else { 'null' }
                            Add-Check 'eye-staleness:heal' 'warn' "taskkill exit=$code for pid=$($c.Pid) -- kill may not have succeeded"
                            Emit '!' 'eye-staleness:heal' "taskkill exit=$code for pid=$($c.Pid)"
                        }
                    } catch {
                        Add-Check 'eye-staleness:heal' 'warn' "OS kill failed for pid=$($c.Pid): $($_.Exception.Message)"
                        Emit '!' 'eye-staleness:heal' "OS kill failed for pid=$($c.Pid): $($_.Exception.Message)"
                    }
                }
            }
        }
    }
}

# PHASE 4: PREVENT -- this check runs every wkdoctor session once promoted off -proto; a stale
# Eye that keeps failing hot-swap cannot silently persist across sessions.
Add-Check 'eye-staleness:guard' 'ok' 'stale-Eye check runs every session once promoted (currently PROTO, inert)'
Emit 'ok' 'eye-staleness:guard' 'proto -- promote to enable'
