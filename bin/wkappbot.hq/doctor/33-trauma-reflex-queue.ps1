# wkdoctor check: trauma-reflex Opus-review queue health.
# Reads the auto-generated queue ($USERPROFILE/.claude/trauma-reflex-opus-review-queue.jsonl),
# counts pending entries, and reports their age:
#   ok   -- no pending entries, or all pending entries are fresh (<=1h old)
#   warn -- oldest pending entry is >1h old
#   crit -- oldest pending entry is >24h old (reflex knowledge becoming stale)
# Zero spawns. FAIL-OPEN: any error -> ok (telemetry must never block wkdoctor).
#
# harness:skill bin/wkappbot.hq/doctor/23-trauma-reflex-queue.ps1
# wkappbot skill read wkdoctor-system-physician    # Usage: doctor module pattern
# wkappbot skill read incident-trauma-reflex-ref   # Impl: what the queue drafts become
# wkappbot skill read wkharness-guards             # Task: guard-block context

try {
    $_trQueuePath = Join-Path $env:USERPROFILE '.claude\trauma-reflex-opus-review-queue.jsonl'

    if (-not (Test-Path -LiteralPath $_trQueuePath)) {
        Add-Check 'trauma-reflex-queue' 'ok' 'no queue file (no RISING guard blocks recorded yet)'
        Emit 'ok' 'trauma-reflex-queue' 'no queue file'
        return
    }

    $_trPending   = 0
    $_trOldestSec = 0
    $_trNow       = [DateTimeOffset]::UtcNow

    try {
        foreach ($_trLine in [System.IO.File]::ReadLines($_trQueuePath)) {
            if ([string]::IsNullOrWhiteSpace($_trLine)) { continue }
            try {
                $_trEntry = $_trLine | ConvertFrom-Json -ErrorAction SilentlyContinue
                if (-not $_trEntry) { continue }
                if ([string]$_trEntry.status -ne 'pending') { continue }
                $_trPending++
                if ($_trEntry.ts) {
                    try {
                        $_trTs  = [DateTimeOffset]::Parse([string]$_trEntry.ts)
                        $_trAge = ($_trNow - $_trTs).TotalSeconds
                        if ($_trAge -gt $_trOldestSec) { $_trOldestSec = $_trAge }
                    } catch {}
                }
            } catch {}
        }
    } catch {}

    if ($_trPending -eq 0) {
        Add-Check 'trauma-reflex-queue' 'ok' 'no pending entries'
        Emit 'ok' 'trauma-reflex-queue' 'no pending entries'
        return
    }

    $_trOldestH = [math]::Round($_trOldestSec / 3600.0, 1)
    $_trDetail  = "$_trPending pending reflex draft(s); oldest ~${_trOldestH}h old -- Opus should evaluate and publish"

    if ($_trOldestSec -gt 86400) {
        # Oldest entry is >24h: crit
        Add-Check 'trauma-reflex-queue' 'crit' $_trDetail
        Emit '!' 'trauma-reflex-queue' $_trDetail
    } elseif ($_trOldestSec -gt 3600) {
        # Oldest entry is >1h: warn
        Add-Check 'trauma-reflex-queue' 'warn' $_trDetail
        Emit '!' 'trauma-reflex-queue' $_trDetail
    } else {
        # All entries are fresh (<=1h): ok
        Add-Check 'trauma-reflex-queue' 'ok' $_trDetail
        Emit 'ok' 'trauma-reflex-queue' $_trDetail
    }
} catch {
    # FAIL-OPEN: any unexpected error -> ok so wkdoctor is never blocked
    Add-Check 'trauma-reflex-queue' 'ok' "n/a (trauma-reflex-queue check error, fail-open): $_"
    Emit 'ok' 'trauma-reflex-queue' 'n/a (error, fail-open)'
}
