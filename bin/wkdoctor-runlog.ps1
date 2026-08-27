# wkdoctor-runlog-proto.ps1 -- Logging module for wkdoctor runs
# Provides functions to capture, strip ANSI codes, and persist wkdoctor output

# Module-level path to the log file (set by Initialize-WkdoctorLog)
$script:wkdoctorLogPath = $null

# BOM-free UTF-8 encoding (shared across all write operations)
$script:utf8NoBom = New-Object System.Text.UTF8Encoding $false

# Initialize logging for this run
function Initialize-WkdoctorLog {
    $logDir = Join-Path $env:USERPROFILE '.claude\wkharness'
    if (-not (Test-Path $logDir)) {
        New-Item -ItemType Directory -Path $logDir -Force | Out-Null
    }

    $script:wkdoctorLogPath = Join-Path $logDir 'wkdoctor-last-run.log'

    # Truncate the log file and write the header
    $timestamp = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
    $header = "=== wkdoctor run at $timestamp ===" + [System.Environment]::NewLine

    try {
        [System.IO.File]::WriteAllText($script:wkdoctorLogPath, $header, $script:utf8NoBom)
    } catch {
        Write-Error "Failed to initialize log at $($script:wkdoctorLogPath): $_" -ErrorAction SilentlyContinue
    }
}

# Add a line to the log file (immediate write-through, no buffer)
function Add-WkdoctorLogLine {
    param([string]$Line)

    if (-not $script:wkdoctorLogPath) {
        return
    }

    # Strip ANSI codes before writing
    $cleanLine = Remove-AnsiCodes $Line

    # Append to file immediately
    try {
        [System.IO.File]::AppendAllText($script:wkdoctorLogPath, $cleanLine + [System.Environment]::NewLine, $script:utf8NoBom)
    } catch {
        Write-Error "Failed to write log line: $_" -ErrorAction SilentlyContinue
    }
}

# Strip ANSI color codes from a string
function Remove-AnsiCodes {
    param([string]$Text)
    # Remove all ANSI escape sequences: \x1b\[[0-9;]*m
    return $Text -replace '\x1b\[[0-9;]*m', ''
}

# Finalize and append the summary to the log file
function Finalize-WkdoctorLog {
    param([int]$Pass = 0, [int]$Fail = 0, [int]$Warn = 0)

    if (-not $script:wkdoctorLogPath) {
        return
    }

    $logDir = Join-Path $env:USERPROFILE '.claude\wkharness'
    $historyPath = Join-Path $logDir 'wkdoctor-history.log'

    # The caller already logs the summary + note lines through Add-WkdoctorLogLine on the
    # human-readable path, so appending them again here duplicated the summary at the tail of
    # every log (observed live 2026-08-27 on a real 51-check run). Finalize owns history
    # rotation only. On a -Json run the caller emits no summary line, which is correct: the
    # counts are in the JSON payload and the per-check lines are already in the log.

    # Append to history file with rotation
    try {
        $historyEntry = @(
            (Get-Content -Path $script:wkdoctorLogPath -Raw -ErrorAction SilentlyContinue)
            "---"
            ""
        ) -join [System.Environment]::NewLine

        [System.IO.File]::AppendAllText($historyPath, $historyEntry, $script:utf8NoBom)

        # Rotate history: keep last 100 run entries (separated by "---")
        $historyContent = Get-Content -Path $historyPath -Raw -ErrorAction SilentlyContinue
        if ($historyContent) {
            $entries = @($historyContent -split '---\s*')
            if ($entries.Count -gt 100) {
                # Keep last 100 entries
                $trimmed = @($entries[-100..-1]) -join "---`n"
                [System.IO.File]::WriteAllText($historyPath, $trimmed, $script:utf8NoBom)
            }
        }
    } catch {
        Write-Error "Failed to update history log: $_" -ErrorAction SilentlyContinue
    }
}

# Read and display the last run log
function Show-WkdoctorLastRun {
    $logDir = Join-Path $env:USERPROFILE '.claude\wkharness'
    $lastRunPath = Join-Path $logDir 'wkdoctor-last-run.log'

    if (Test-Path $lastRunPath) {
        $utf8NoBom = New-Object System.Text.UTF8Encoding $false
        [System.IO.File]::ReadAllText($lastRunPath, $utf8NoBom)
    } else {
        Write-Host "No previous wkdoctor run found at $lastRunPath" -ForegroundColor Yellow
    }
}
