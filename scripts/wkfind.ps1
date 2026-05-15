param(
    [Parameter(Mandatory = $true, Position = 0, ValueFromRemainingArguments = $true)]
    [string[]]$Keywords,
    [Parameter(Mandatory = $false)]
    [ValidateSet("shallow", "medium", "deep")]
    [string]$SearchDepth = "medium",
    [switch]$IncludeWeb,
    [int]$MaxResults = 20
)

$ErrorActionPreference = "Continue"

Write-Host ""
Write-Host "==== wkfind Multi-Keyword Discovery Tool ===="
Write-Host ""

$Keywords = @($Keywords | Where-Object { $_ } | ForEach-Object { $_.Trim() } | Where-Object { $_.Length -gt 0 })

if ($Keywords.Count -eq 0) {
    Write-Host "Usage: wkfind <keyword1> [<keyword2> ...] [--depth shallow|medium|deep] [--include-web]"
    Write-Host ""
    Write-Host "Examples:"
    Write-Host "  wkfind caller window validation"
    Write-Host "  wkfind off-screen position --depth deep"
    Write-Host "  wkfind GetForegroundWindow GetParentProcessId --include-web"
    exit 1
}

Write-Host "Keywords: $($Keywords -join ' + ')"
Write-Host "Depth: $SearchDepth`n"

foreach ($kw in $Keywords) {
    Write-Host "===== KEYWORD: $kw ====="
    Write-Host ""

    # Git log
    Write-Host "[1] Git Log Search: $kw"
    git log --oneline --all -S $kw 2>$null | Select-Object -First 10

    Write-Host ""
    # Git grep
    Write-Host "[2] Git Grep: $kw"
    git grep -i -n "$kw" 2>$null | Select-Object -First 10

    Write-Host ""
    # C# grep
    Write-Host "[3] C# Source Files: $kw"
    Get-ChildItem -Path "d:\GitHub\wkappbot-sdk\csharp\src" -Include "*.cs" -Recurse -ErrorAction SilentlyContinue 2>$null |
        Select-String -Pattern $kw -List 2>$null |
        ForEach-Object { "$($_.Filename)" } |
        Select-Object -First 5

    Write-Host ""
}

Write-Host ""
Write-Host "==== ENVIRONMENT & RECENT COMMITS ===="
Write-Host ""
Write-Host "Recent 10 commits:"
git log --oneline -10

Write-Host ""
if ($IncludeWeb) {
    Write-Host "==== WEB SEARCH TERMS ===="
    foreach ($kw in $Keywords) {
        Write-Host "  * $kw C#: https://www.google.com/search?q=$([System.Web.HttpUtility]::UrlEncode("$kw C#"))"
        Write-Host "  * $kw Windows: https://www.google.com/search?q=$([System.Web.HttpUtility]::UrlEncode("$kw Windows"))"
    }
    Write-Host ""
}

Write-Host "NEXT STEPS:"
Write-Host "  1. Review code patterns above"
Write-Host "  2. Read files: Program.cs, MyCdpContext.cs, EyeCmdPipeClient.cs, CoreRunner.cs"
Write-Host "  3. git log -p <file> for detailed history"
Write-Host "  4. wkappbot skill read wkfind-comprehensive-problem-solving"
Write-Host ""
