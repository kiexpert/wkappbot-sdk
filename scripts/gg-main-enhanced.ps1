# gg-main-enhanced.ps1: SDK Product Manager Health Check + All Issues Detection
# Detects all problems found in past month + immediate alerts
# Run: powershell -File scripts/gg-main-enhanced.ps1

$ErrorActionPreference = "SilentlyContinue"
$CRITICAL_ALERTS = @()
$WARNINGS = @()

Write-Host "=== gg-main ENHANCED: Full Problem Detection ===" -ForegroundColor Green
Write-Host "[$(Get-Date)]"
Write-Host ""

# ============================================================================
# ISSUE #1: Chrome Multiplication (>5 processes)
# ============================================================================
Write-Host "==[ ISSUE #1 ] Chrome Multiplication Detection ==" -ForegroundColor Cyan
$CHROME_COUNT = @(Get-Process chrome -ErrorAction SilentlyContinue).Count
Write-Host "Chrome processes: $CHROME_COUNT"
if ($CHROME_COUNT -gt 20) {
  Write-Host "  🔴 CRITICAL: $CHROME_COUNT Chrome processes (>20) -- System in danger" -ForegroundColor Red
  $CRITICAL_ALERTS += "Chrome multiplication CRITICAL ($CHROME_COUNT > 20)"
} elseif ($CHROME_COUNT -gt 5) {
  Write-Host "  ⚠️  WARNING: $CHROME_COUNT Chrome processes (>5) -- ask gpt/cdp open multiplication" -ForegroundColor Yellow
  $WARNINGS += "Chrome multiplication ($CHROME_COUNT > 5)"
}
Write-Host ""

# ============================================================================
# ISSUE #2: Eye IPC Lag (wkappbot-core >3)
# ============================================================================
Write-Host "==[ ISSUE #2 ] Eye IPC Lag Detection ==" -ForegroundColor Cyan
$CORE_COUNT = @(Get-Process wkappbot-core -ErrorAction SilentlyContinue).Count
Write-Host "wkappbot-core processes: $CORE_COUNT"
if ($CORE_COUNT -gt 8) {
  Write-Host "  🔴 CRITICAL: $CORE_COUNT processes (>8) -- System lag imminent" -ForegroundColor Red
  $CRITICAL_ALERTS += "Eye lag CRITICAL ($CORE_COUNT > 8)"
} elseif ($CORE_COUNT -gt 3) {
  Write-Host "  ⚠️  WARNING: $CORE_COUNT processes (>3) -- suggest/ask timeout risk" -ForegroundColor Yellow
  $WARNINGS += "Eye lag warning ($CORE_COUNT > 3)"
}
Write-Host ""

# ============================================================================
# ISSUE #3: Off-screen Chrome Placement (x < -100)
# ============================================================================
Write-Host "==[ ISSUE #3 ] Off-Screen Chrome Detection ==" -ForegroundColor Cyan
$chrome_procs = Get-Process chrome -ErrorAction SilentlyContinue
$off_screen_found = $false
if ($chrome_procs) {
  foreach ($proc in $chrome_procs) {
    $hwnd = $proc.MainWindowHandle
    if ($hwnd -ne 0) {
      $rect = New-Object 'Win32.WinAPI+RECT' -ErrorAction SilentlyContinue
      if ($rect -and ([Win32.WinAPI]::GetWindowRect($hwnd, [ref]$rect))) {
        if ($rect.Left -lt -100 -or $rect.Top -lt -100) {
          Write-Host "  🔴 CRITICAL: Chrome at ($($rect.Left), $($rect.Top)) -- off-screen" -ForegroundColor Red
          $CRITICAL_ALERTS += "Off-screen Chrome detected ($($rect.Left), $($rect.Top))"
          $off_screen_found = $true
        }
      }
    }
  }
}
if (-not $off_screen_found) {
  Write-Host "  ✓ No off-screen Chrome detected" -ForegroundColor Green
}
Write-Host ""

# ============================================================================
# ISSUE #4: Zombie Process Accumulation
# ============================================================================
Write-Host "==[ ISSUE #4 ] Zombie Process Detection ==" -ForegroundColor Cyan
$total_procs = @(Get-Process wkappbot*, chrome -ErrorAction SilentlyContinue).Count
Write-Host "Total wkappbot/Chrome processes: $total_procs"
if ($total_procs -gt 50) {
  Write-Host "  🔴 CRITICAL: $total_procs processes (>50) -- zombie accumulation" -ForegroundColor Red
  $CRITICAL_ALERTS += "Zombie accumulation CRITICAL ($total_procs > 50)"
} elseif ($total_procs -gt 30) {
  Write-Host "  ⚠️  WARNING: $total_procs processes (>30) -- monitor closely" -ForegroundColor Yellow
  $WARNINGS += "Zombie accumulation risk ($total_procs > 30)"
}
Write-Host ""

# ============================================================================
# ISSUE #5: Memory Excessive (>2GB per session)
# ============================================================================
Write-Host "==[ ISSUE #5 ] Memory Usage Detection ==" -ForegroundColor Cyan
$chrome_mem = @(Get-Process chrome -ErrorAction SilentlyContinue | Measure-Object -Property WorkingSet -Sum).Sum / 1GB
$core_mem = @(Get-Process wkappbot-core -ErrorAction SilentlyContinue | Measure-Object -Property WorkingSet -Sum).Sum / 1GB
$total_mem = $chrome_mem + $core_mem
Write-Host "Chrome memory: $([math]::Round($chrome_mem, 2)) GB"
Write-Host "Core memory: $([math]::Round($core_mem, 2)) GB"
Write-Host "Total: $([math]::Round($total_mem, 2)) GB"
if ($total_mem -gt 4) {
  Write-Host "  🔴 CRITICAL: $([math]::Round($total_mem, 2)) GB (>4) -- OOM imminent" -ForegroundColor Red
  $CRITICAL_ALERTS += "Memory CRITICAL (>4GB)"
} elseif ($total_mem -gt 2) {
  Write-Host "  ⚠️  WARNING: $([math]::Round($total_mem, 2)) GB (>2) -- high memory" -ForegroundColor Yellow
  $WARNINGS += "High memory usage (>2GB)"
}
Write-Host ""

# ============================================================================
# ISSUE #6: Suggest Resolve / Ask GPT Timeouts
# ============================================================================
Write-Host "==[ ISSUE #6 ] IPC Timeout Risk Detection ==" -ForegroundColor Cyan
if ($CORE_COUNT -gt 3 -or $CHROME_COUNT -gt 5) {
  Write-Host "  🔴 ALERT: IPC lag detected -- suggest resolve/ask gpt likely to timeout" -ForegroundColor Red
  $CRITICAL_ALERTS += "IPC timeout risk: suggest/ask commands will hang"
} else {
  Write-Host "  ✓ IPC healthy, suggest/ask commands should work" -ForegroundColor Green
}
Write-Host ""

# ============================================================================
# ISSUE #7: Stale BUG-AUTO Suggests
# ============================================================================
Write-Host "==[ ISSUE #7 ] Stale BUG-AUTO Detection ==" -ForegroundColor Cyan
if ((Get-Command wkappbot -ErrorAction SilentlyContinue)) {
  try {
    $suggests = wkappbot suggest list 2>$null | Select-String "BUG-AUTO" | Measure-Object | Select-Object -ExpandProperty Count
    if ($suggests -gt 5) {
      Write-Host "  ⚠️  WARNING: $suggests stale BUG-AUTO entries (>5) -- needs triage" -ForegroundColor Yellow
      $WARNINGS += "Stale BUG-AUTO backlog ($suggests > 5)"
    } else {
      Write-Host "  ✓ BUG-AUTO count normal ($suggests)" -ForegroundColor Green
    }
  } catch {
    Write-Host "  ⚠️  Cannot read suggest list (IPC lag?)" -ForegroundColor Yellow
  }
} else {
  Write-Host "  [SKIP] wkappbot not available" -ForegroundColor Gray
}
Write-Host ""

# ============================================================================
# ISSUE #8: System Lag Indicators
# ============================================================================
Write-Host "==[ ISSUE #8 ] System Lag Indicators ==" -ForegroundColor Cyan
$disk_queue = Get-Counter '\PhysicalDisk(_Total)\% Disk Time' -ErrorAction SilentlyContinue | Select-Object -ExpandProperty CounterSamples | Select-Object -ExpandProperty CookedValue
if ($disk_queue -gt 80) {
  Write-Host "  🔴 CRITICAL: Disk I/O $([math]::Round($disk_queue)) % (>80) -- system lag" -ForegroundColor Red
  $CRITICAL_ALERTS += "Disk I/O high (>80%)"
} else {
  Write-Host "  ✓ Disk I/O normal ($([math]::Round($disk_queue)) %)" -ForegroundColor Green
}
Write-Host ""

# ============================================================================
# FINAL ALERT SUMMARY
# ============================================================================
Write-Host "=== ALERT SUMMARY ===" -ForegroundColor Cyan
if ($CRITICAL_ALERTS.Count -gt 0) {
  Write-Host "🔴 CRITICAL ALERTS ($($CRITICAL_ALERTS.Count)):" -ForegroundColor Red
  foreach ($alert in $CRITICAL_ALERTS) {
    Write-Host "  • $alert" -ForegroundColor Red
  }
  Write-Host ""
}

if ($WARNINGS.Count -gt 0) {
  Write-Host "⚠️  WARNINGS ($($WARNINGS.Count)):" -ForegroundColor Yellow
  foreach ($warning in $WARNINGS) {
    Write-Host "  • $warning" -ForegroundColor Yellow
  }
  Write-Host ""
}

if ($CRITICAL_ALERTS.Count -eq 0 -and $WARNINGS.Count -eq 0) {
  Write-Host "✅ ALL SYSTEMS GREEN - No issues detected" -ForegroundColor Green
  Write-Host ""
  exit 0
} elseif ($CRITICAL_ALERTS.Count -gt 0) {
  Write-Host "Status: 🔴 CRITICAL - Immediate action required" -ForegroundColor Red
  exit 2
} else {
  Write-Host "Status: ⚠️  WARNING - Monitor closely" -ForegroundColor Yellow
  exit 1
}
