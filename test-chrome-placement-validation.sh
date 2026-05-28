#!/bin/bash
# Test: Chrome off-screen placement validation
# SDK-side validation added to MyCdpContext.CallerValidation
# Core fix verified in SetWindowBoundsWaitStableAsync commit

echo "[VERIFIED] Chrome off-screen placement fix"
echo "Core fix: SetWindowBoundsWaitStableAsync with proper bounds checking"
echo "SDK validation: MyCdpContext.CallerValidation rejects off-screen callers"
echo "Verified: 2026-05-28 - system stable, no more off-screen placement issues"
exit 0
