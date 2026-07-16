# wkdoctor check: eye-staleness -- DETECT (warn-only) a STUCK/STALE Eye.
#
# LAYER 3 of the Eye-staleness defense. Runs in PARALLEL with ROOT's LAYER 2 (Eye self-heal /
# self-retire on hot-swap failure). This layer is the OUTER safety net: if an Eye fails to
# hot-swap and keeps running stale/tainted code, this doctor plugin FINDS it and WARNS -- it
# does NOT kill it. Clearing a stale Eye is a human/sanctuary-gated action via `wkappbot taskkill`.
#
# STATUS: LIVE. DETECT+WARN ONLY.
#
# INCIDENT 2026-07-16 (suggest 1784193313): this plugin previously ran a raw OS-level
# `taskkill.exe /F /PID` on every process matching the heuristic below whenever invoked under
# `wkdoctor -EmergencyKill`. HandleCount>200 is only a PROXY for "this is the full Eye daemon" --
# it CANNOT positively exclude a session core, a guardian process, or an MCP worker belonging to
# a DIFFERENT live session. The proxy over-matched and the raw OS kill destroyed the cores/
# terminals of 4 unrelated live sessions (GitHub-w, GitHub-p, codex). The raw-taskkill-under-
# EmergencyKill path has been REMOVED PERMANENTLY. `-EmergencyKill` auto-fires at 60% CPU/RAM,
# which made this a live auto-hazard -- exactly what `wkappbot taskkill` (sanctuary-aware,
# foreign-tree-refusing) exists to prevent. Routing around it with raw taskkill was the bug.
# Any future re-introduction of an automatic kill path here MUST first replace the HandleCount
# proxy with a POSITIVE command-line-based Eye classifier (see RESIDUAL GAP below) and route the
# kill through `wkappbot taskkill`, never a raw OS taskkill.
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
# REMEDIATION: this plugin never kills. When a stale Eye is detected it emits an operator
# remediation line pointing at `wkappbot taskkill --force <pid>` -- the sanctuary-aware tool
# that refuses to act on a non-Eye or foreign-tree session core, the exact protection a raw OS
# taskkill lacks.
#
# WMI-STORM DISCIPLINE (wktasklist-intelligent-management): enumerate ONLY via
# `Get-Process -Name wkappbot-core`. NEVER Get-CimInstance/Get-WmiObject Win32_Process here.
#
# RESIDUAL GAP (flagged for ROOT review): Get-Process cannot read a process's command line
# without a WMI/CIM query, which is itself forbidden here by WMI-storm discipline -- so this
# heuristic cannot positively confirm "this pid is NOT the guardian / NOT another session's
# core" the way `wkappbot taskkill`'s own classifier does. The 3-condition AND-heuristic is a
# best-effort proxy, not a certainty, which is precisely why it must stay detect+warn-only until
# a positive command-line-based classifier exists (candidate: a native GetProcessCommandLine CLI
# verb, already used by cdp-open-reuse-existing-chrome-tab, or a guardian-PID marker file).
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

                $remediation = "stale Eye pid=$($c.Pid) -- auto-kill DISABLED (over-killed other sessions 2026-07-16). Clear it safely via: wkappbot taskkill --force $($c.Pid) (sanctuary-aware; refuses non-Eye/foreign session cores)."
                Add-Check 'eye-staleness:heal' 'warn' $remediation
                Emit '!' 'eye-staleness:heal' $remediation
            }
        }
    }
}

# PHASE 4: PREVENT -- this check runs every wkdoctor session (LIVE); a stale Eye that keeps
# failing hot-swap cannot silently persist across sessions. DETECT+WARN ONLY: auto-kill was
# intentionally removed 2026-07-16 (suggest 1784193313) pending a positive command-line-based
# Eye classifier -- HandleCount>200 was an unsafe proxy that over-matched other live sessions'
# cores. Clearing a detected stale Eye is now a human/sanctuary-gated action via
# `wkappbot taskkill --force <pid>`.
Add-Check 'eye-staleness:guard' 'ok' 'stale-Eye check runs every session (LIVE, detect+warn only -- auto-kill removed 2026-07-16, suggest 1784193313)'
Emit 'ok' 'eye-staleness:guard' 'live -- detect+warn only, no auto-kill'
