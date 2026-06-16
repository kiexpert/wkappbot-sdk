# wkdoctor check: stray-hub retirement -- the D:\GitHub\wkharness.ps1 /
# wkharness-post.ps1 "redirect shims" are OBSOLETE as of 2026-06-17.
# Background: every family now wires DIRECTLY to its per-family kih main
# (wkharness-<family>-main.ps1 -Family <family>); the shim's only job was
# translating gemini native tool-names, which now lives inside the per-family
# mains (commit 678ca08). Re-creating the shim caused the family-bleed RE-REVERT
# (gemini/agy hooks pointed at a path with no -Family -> "Hook(s) failed").
# This module now DELETES any resurrected stray shim instead of repairing it.
# FAIL-OPEN: any error -> ok/na, never crash the doctor.

try {
    $strays = @('D:\GitHub\wkharness.ps1', 'D:\GitHub\wkharness-post.ps1')
    $removed = @()
    foreach ($p in $strays) {
        try {
            if (Test-Path -LiteralPath $p) {
                Remove-Item -LiteralPath $p -Force -ErrorAction Stop
                $removed += (Split-Path -Leaf $p)
            }
        } catch {
            Add-Check 'harness-hub-shim-retire' 'ok' "n/a $(Split-Path -Leaf $p) delete failed (fail-open): $_"
            Emit '!' 'harness-hub-shim-retire' "n/a $(Split-Path -Leaf $p) delete failed: $_"
        }
    }

    if ($removed.Count -gt 0) {
        Add-Check 'harness-hub-shim-retire' 'ok' "retired stray shim(s): $($removed -join ', ') (per-family direct wiring; shim obsolete since 678ca08)"
        Emit 'ok' 'harness-hub-shim-retire' "retired stray: $($removed -join ', ')"
    } else {
        Add-Check 'harness-hub-shim-retire' 'ok' 'no stray hub present (per-family direct wiring intact)'
        Emit '+' 'harness-hub-shim-retire' 'no stray hub (per-family direct wiring intact)'
    }
} catch {
    Add-Check 'harness-hub-shim-retire' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'harness-hub-shim-retire' 'n/a (check error, fail-open)'
}
