#Requires -Version 5.1
<#
.SYNOPSIS
    wkfind - Comprehensive problem-solving and code discovery tool

.DESCRIPTION
    Multi-angle search and problem-solving framework combining:
    - Git history analysis
    - Pattern matching (grep, glob)
    - Web search
    - Code reading and cross-reference
    - Process/environment analysis
    - Agent-based exploration

.PARAMETER Problem
    Problem description or symptom to investigate

.PARAMETER SearchDepth
    shallow (5min), medium (30min), or deep (60min)

.PARAMETER IncludeWeb
    Include web search in results

.PARAMETER Auto
    Auto-execute searches without confirmation

.EXAMPLE
    wkfind "Chrome window position mismatch" --depth medium --include-web
    wkfind "off-screen caller window" --depth deep --auto

.NOTES
    Uses multiple search strategies in parallel:
    1. Git history (commits, diffs, blame)
    2. Code pattern matching (grep, glob)
    3. Cross-reference analysis
    4. Web/external knowledge search
    5. Environment & process analysis
#>

param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string]$Problem,

    [Parameter(Mandatory = $false)]
    [ValidateSet("shallow", "medium", "deep")]
    [string]$SearchDepth = "medium",

    [switch]$IncludeWeb,
    [switch]$Auto
)

$ErrorActionPreference = "Continue"

# Color output helpers
function Write-Section {
    param([string]$Title)
    Write-Host "`n" -NoNewline
    Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
    Write-Host "║ $Title" -ForegroundColor Cyan
    Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Result {
    param([string]$Title, [array]$Items)
    Write-Host "  $Title" -ForegroundColor Green
    $Items | ForEach-Object {
        Write-Host "    • $_"
    }
}

function Write-Search {
    param([string]$Query)
    Write-Host "  🔍 $Query" -ForegroundColor Yellow
}

# Search toolbox
$SearchTools = @{
    "Git Log" = {
        Write-Search "git log --oneline --all -S '$Problem' | head -20"
        $result = @()
        try {
            $matches = git log --oneline --all -S $Problem 2>$null | Select-Object -First 20
            $matches | ForEach-Object {
                $result += $_
            }
        } catch {}
        return $result
    }

    "Git Grep" = {
        Write-Search "git grep -i -n '$Problem' | head -30"
        $result = @()
        try {
            $terms = $Problem.Split() | Where-Object { $_.Length -gt 3 }
            $terms | ForEach-Object {
                $matches = git grep -i -n $_ 2>$null | Select-Object -First 15
                $result += $matches
            }
        } catch {}
        return $result | Select-Object -Unique
    }

    "File Grep (*.cs)" = {
        Write-Search "Searching C# files for pattern match"
        $result = @()
        try {
            $terms = $Problem.Split() | Where-Object { $_.Length -gt 3 }
            $terms | ForEach-Object {
                $matches = Get-ChildItem -Path "d:\GitHub\wkappbot-sdk" -Include "*.cs" -Recurse -ErrorAction SilentlyContinue |
                    Select-String -Pattern $_ -List |
                    ForEach-Object { "$($_.Path):$($_.LineNumber): $($_.Line.Trim())" } |
                    Select-Object -First 10
                $result += $matches
            }
        } catch {}
        return $result
    }

    "Recent Commits" = {
        Write-Search "git log --oneline -30"
        $result = @()
        try {
            $result = git log --oneline -30 2>$null
        } catch {}
        return $result
    }

    "Blame Search" = {
        Write-Search "git blame analysis for matching patterns"
        $result = @()
        try {
            $blameFiles = Get-ChildItem -Path "d:\GitHub\wkappbot-sdk\csharp\src" -Include "*.cs" -Recurse -ErrorAction SilentlyContinue |
                ForEach-Object {
                    $blame = git blame "$($_.FullName)" 2>$null | Where-Object { $_ -match $Problem } | Select-Object -First 3
                    if ($blame) {
                        "$($_.Name): $blame"
                    }
                }
            $result = $blameFiles
        } catch {}
        return $result
    }

    "Environment Variables" = {
        Write-Search "Checking related environment variables"
        $result = @()
        $envVars = @("WKAPPBOT_CALLER_HWND", "WKAPPBOT_PROFILE", "PATH")
        $envVars | ForEach-Object {
            $val = [System.Environment]::GetEnvironmentVariable($_)
            if ($val) {
                $result += "$($_): $val"
            }
        }
        return $result
    }

    "Process Analysis" = {
        Write-Search "Get-Process | Where-Object { ... }"
        $result = @()
        try {
            $procs = Get-Process | Where-Object { $_.ProcessName -match "wkappbot|chrome|code" }
            $procs | ForEach-Object {
                $result += "$($_.Name) (PID: $($_.Id), Memory: $($_.WorkingSet/1MB)MB)"
            }
        } catch {}
        return $result
    }
}

# Phase-based searches
$SearchPhases = @{
    "Phase 1: Problem Definition" = @(
        "Recent Commits",
        "Git Log"
    )

    "Phase 2: Multi-Angle Search" = @(
        "Git Grep",
        "File Grep (*.cs)",
        "Blame Search",
        "Environment Variables",
        "Process Analysis"
    )

    "Phase 3: Reference & Patterns" = @(
        "Blame Search",
        "Git Log"
    )
}

# ========================================
# Main Execution
# ========================================

Write-Host ""
Write-Host "╭─────────────────────────────────────╮" -ForegroundColor Magenta
Write-Host "│  wkfind Problem-Solving Tool        │" -ForegroundColor Magenta
Write-Host "│  Comprehensive Code Discovery       │" -ForegroundColor Magenta
Write-Host "╰─────────────────────────────────────╯" -ForegroundColor Magenta

Write-Section "PROBLEM ANALYSIS"
Write-Host "  Problem: $Problem"
Write-Host "  Depth: $SearchDepth"
Write-Host "  Include Web Search: $IncludeWeb"
Write-Host ""

# Determine which phases to run based on depth
$phasesToRun = switch ($SearchDepth) {
    "shallow" { @("Phase 1: Problem Definition") }
    "medium" { @("Phase 1: Problem Definition", "Phase 2: Multi-Angle Search") }
    "deep" { @("Phase 1: Problem Definition", "Phase 2: Multi-Angle Search", "Phase 3: Reference & Patterns") }
}

# Execute searches
foreach ($phase in $phasesToRun) {
    Write-Section $phase

    $tools = $SearchPhases[$phase]
    foreach ($tool in $tools) {
        Write-Host "  Executing: $tool"
        $searchFunc = $SearchTools[$tool]
        if ($searchFunc) {
            $results = & $searchFunc
            if ($results -and $results.Count -gt 0) {
                Write-Result "Results ($($results.Count) items)" $results
            } else {
                Write-Host "    (no matches)" -ForegroundColor Gray
            }
        }
    }
}

# Web search if requested
if ($IncludeWeb) {
    Write-Section "PHASE 4: WEB/EXTERNAL KNOWLEDGE"
    Write-Host "  Google Search Terms:"

    $searchTerms = @(
        "Windows $($Problem -split ' ' | Select-Object -First 2) C#",
        "GetProcessById MainWindowHandle",
        "Process parent chain HWND"
    )

    foreach ($term in $searchTerms) {
        Write-Host "    • $term"
        Write-Host "      → https://www.google.com/search?q=$([System.Web.HttpUtility]::UrlEncode($term))" -ForegroundColor Cyan
    }
}

# Summary
Write-Section "INVESTIGATION SUMMARY"
Write-Host "  Files to investigate first:"
Write-Host "    • D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\Program.cs"
Write-Host "    • D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\MyCdpContext.cs"
Write-Host "    • D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\EyeCmdPipeClient.cs"
Write-Host "    • D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\CoreRunner.cs"

Write-Host ""
Write-Host "  Next Steps:"
Write-Host "    1. Read the most relevant files (wkfind suggests above)"
Write-Host "    2. Cross-reference with git history (when was this last touched?)"
Write-Host "    3. Trace data flow (source → forwarding → consumption)"
Write-Host "    4. Build & test changes"
Write-Host "    5. Verify with persistence/logs (cdp-state.jsonl, etc.)"

Write-Host ""
Write-Host "  For deeper analysis, use:"
Write-Host "    wkappbot agent ask-triad 'problem summary here'"
Write-Host "    wkappbot skill read wkfind-comprehensive-problem-solving"

Write-Host ""
