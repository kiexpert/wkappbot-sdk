# wkdoctor check: harness AUDIT COMPLETENESS -- the anti-theater liveness check.
# (user 2026-06-17: "an UNTRACKED tool-call existing in the session file is ALONE a FAIL")
#
# Every tool call MUST leave an audit trail (ai-audit.log, written by the audit BeforeTool
# hook). If a session JSONL has a tool-call NEWER than the audit log's latest entry -- or more
# tool-calls than audit entries in the recent window -- then the hook did NOT fire for those
# calls = an UNTRACKED tool-call = a hole in enforcement = FAIL. This catches a DEAD/partial/
# bypassed hook that the proxy-canaries (which test the heart, not the live per-family main)
# emit false OKs for -- e.g. the 2026-06-17 gemini-main parse-death + SessionId runtime-death,
# both of which every canary reported "gemini PASS" while the live hook was dead.
#
# Runs EARLY (06). FAIL-OPEN on the check's OWN errors; an untracked call is a hard FAIL.

try {
    $auditLog = 'D:\GitHub\ai-audit.log'
    if (-not (Test-Path -LiteralPath $auditLog)) {
        Add-Check 'harness-audit-completeness' 'ok' 'n/a (ai-audit.log absent -- audit hook not wired yet)'
        Emit 'ok' 'harness-audit-completeness' 'n/a (no audit log)'
        return
    }

    # latest audit-entry timestamp (line prefix 'yyyy-MM-dd HH:mm:ss')
    $auditLines = @(Get-Content -LiteralPath $auditLog -Tail 400 -ErrorAction Stop)

    # FIELD-COMPLETENESS (user 2026-06-17): every NEW-format entry must carry ALL fields intact
    # (ts | slug | sess | tool | body); ANY missing field => FAIL. Format written by audit-log.ps1:
    #   'yyyy-MM-dd HH:mm:ss <slug> sess=<id> tp=<file> [<tool>]> <body>'. Legacy lines (no sess=)
    # are pre-upgrade and skipped. tp may be empty for families without a transcript path.
    $newFmt = '^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\s+(\S+)\s+sess=(\S*)\s+tp=(\S*)\s+\[([^\]]*)\]>\s*(.*)$'
    $fieldBad = @()
    foreach ($l in ($auditLines | Select-Object -Last 120)) {
        if ($l -notmatch 'sess=') { continue }   # legacy pre-upgrade entry -- skip
        if ($l -match $newFmt) {
            $miss = @()
            if (-not $Matches[2]) { $miss += 'slug' }
            if (-not $Matches[3]) { $miss += 'sess' }
            if (-not $Matches[5]) { $miss += 'tool' }
            if ([string]::IsNullOrWhiteSpace($Matches[6])) { $miss += 'body' }
            if ($miss.Count) { $fieldBad += ("'{0}...' MISSING [{1}]" -f $l.Substring(0,[math]::Min(55,$l.Length)), ($miss -join ',')) }
        } else {
            $fieldBad += ("MALFORMED '{0}...'" -f $l.Substring(0,[math]::Min(55,$l.Length)))
        }
    }
    if ($fieldBad.Count -gt 0) {
        $detail = 'AUDIT FIELD-INCOMPLETE -- {0} entr(ies) missing required field(s) (ts/slug/sess/tool/body) = the hook recorded a partial/corrupt trail: {1}' -f $fieldBad.Count, (($fieldBad | Select-Object -First 6) -join ' || ')
        Add-Check 'harness-audit-completeness' 'fail' $detail
        Emit 'fail' 'harness-audit-completeness' $detail
        return
    }
    $auditTimes = foreach ($l in $auditLines) {
        if ($l -match '^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})') {
            try { [datetime]::ParseExact($Matches[1], 'yyyy-MM-dd HH:mm:ss', $null) } catch {}
        }
    }
    $auditTimes = @($auditTimes | Sort-Object)
    if (-not $auditTimes.Count) {
        Add-Check 'harness-audit-completeness' 'ok' 'n/a (no parseable audit timestamps)'
        Emit 'ok' 'harness-audit-completeness' 'n/a (no audit timestamps)'
        return
    }
    $auditLatest = $auditTimes[-1]

    # newest active session JSONL (last 20 min) across all Claude projects
    $projRoot = Join-Path $env:USERPROFILE '.claude\projects'
    $now = Get-Date
    $offenders = @()
    if (Test-Path $projRoot) {
        $sessions = Get-ChildItem -Path $projRoot -Recurse -Filter '*.jsonl' -ErrorAction SilentlyContinue |
                    Where-Object { ($now - $_.LastWriteTime).TotalMinutes -le 20 }
        foreach ($s in $sessions) {
            # latest tool_use timestamp in this session
            $lines = Get-Content -LiteralPath $s.FullName -Tail 300 -ErrorAction SilentlyContinue
            $latestToolTs = $null
            foreach ($ln in $lines) {
                if ($ln -match '"type":"tool_use"' -and $ln -match '"timestamp":"([^"]+)"') {
                    try {
                        $t = [datetime]::Parse($Matches[1])
                        if (-not $latestToolTs -or $t -gt $latestToolTs) { $latestToolTs = $t }
                    } catch {}
                }
            }
            if ($latestToolTs) {
                # a tool-call NEWER than the audit log's latest entry (+120s tolerance for async)
                # = that call left NO audit trail = UNTRACKED.
                if ($latestToolTs -gt $auditLatest.AddSeconds(120)) {
                    $gap = [int]($latestToolTs - $auditLatest).TotalSeconds
                    $offenders += ('{0}: newest tool-call {1:HH:mm:ss} is {2}s AFTER the latest audit entry {3:HH:mm:ss} -- UNTRACKED' -f $s.Name, $latestToolTs, $gap, $auditLatest)
                }
            }
        }
    }

    if ($offenders.Count -gt 0) {
        $detail = 'UNTRACKED TOOL-CALL(S) -- {0} active session(s) have tool-calls with NO audit trail (the hook did NOT fire = enforcement hole): {1}' -f $offenders.Count, ($offenders -join ' || ')
        Add-Check 'harness-audit-completeness' 'fail' $detail
        Emit 'fail' 'harness-audit-completeness' $detail
    } else {
        Add-Check 'harness-audit-completeness' 'ok' "audit trail complete (latest audit entry $($auditLatest.ToString('HH:mm:ss')); no untracked tool-calls in active sessions)"
        Emit 'ok' 'harness-audit-completeness' "audit trail complete (no untracked tool-calls)"
    }
} catch {
    Add-Check 'harness-audit-completeness' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'harness-audit-completeness' 'n/a (check error, fail-open)'
}
