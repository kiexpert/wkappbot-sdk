# wkdoctor check: bintool-param-first -- catch the split-break that killed wkhippo (2026-06-10).
# A PowerShell split that prepends module-loading ABOVE a script's param() block displaces param so it
# runs as a COMMAND at runtime ('The term param is not recognized') -- yet the file PARSES CLEAN, so a
# syntax check misses it. This catches it STATICALLY (no execution, safe every pass): a correctly-placed
# param() becomes the AST ParamBlock; a DISPLACED param() instead shows up as a CommandAst named 'param'.
# We flag any bin .ps1 whose AST contains a 'param' command -- that script is runtime-broken.
# FAIL-OPEN: parse/scan error on a file -> skip that file.

try {
    $root = @(
        'D:\GitHub\WKAppBot\bin',
        (Join-Path $env:USERPROFILE 'GitHub\WKAppBot\bin')
    ) | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1

    if (-not $root) {
        Add-Check 'bintool-param-first' 'ok' 'n/a (WKAppBot/bin not local)'
        Emit 'ok' 'bintool-param-first' 'n/a (bin not local)'
        return
    }

    $broken = New-Object 'System.Collections.Generic.List[string]'
    $scanned = 0
    # Top-level bin entry scripts only (where param() lives) + any *-lib split-output dirs -- NOT a full
    # recursive sweep of vendored/node scripts (that was 2349 parses/pass). Entry scripts are the risk.
    $files = @(Get-ChildItem -LiteralPath $root -Filter '*.ps1' -File -ErrorAction SilentlyContinue)
    $files += @(Get-ChildItem -LiteralPath $root -Directory -Filter '*-lib' -ErrorAction SilentlyContinue |
                ForEach-Object { Get-ChildItem -LiteralPath $_.FullName -Filter '*.ps1' -File -ErrorAction SilentlyContinue })
    foreach ($f in $files) {
        try {
            $tokens = $null; $perr = $null
            $ast = [System.Management.Automation.Language.Parser]::ParseFile($f.FullName, [ref]$tokens, [ref]$perr)
            if (-not $ast) { continue }
            $scanned++
            # A 'param' used as a COMMAND (not the keyword param block) == displaced param == runtime break.
            $bad = $ast.FindAll({
                param($n)
                ($n -is [System.Management.Automation.Language.CommandAst]) -and
                ($n.GetCommandName() -eq 'param')
            }, $true)
            if ($bad -and $bad.Count -gt 0) {
                $broken.Add("$($f.Name) (L$($bad[0].Extent.StartLineNumber): param runs as a command -- param() must be the FIRST statement)")
            }
        } catch { }
    }

    if ($broken.Count -eq 0) {
        Add-Check 'bintool-param-first' 'ok' "$scanned bin scripts: param() correctly first in all"
        Emit 'ok' 'bintool-param-first' "$scanned bin scripts OK -- no displaced param()"
    } else {
        $detail = "$($broken.Count)/$scanned bin script(s) RUNTIME-BROKEN (param displaced below module-loading -- parses clean, dies at runtime): " + ($broken -join ' | ') + "  FIX: move param() back to the FIRST statement (after the shebang/comment), emit module-loading BELOW it; then RUN the tool to confirm."
        Add-Check 'bintool-param-first' 'warn' $detail
        Emit '!' 'bintool-param-first' $detail
    }
} catch {
    Add-Check 'bintool-param-first' 'ok' "n/a (scan error, fail-open): $_"
    Emit 'ok' 'bintool-param-first' 'n/a (scan error, fail-open)'
}
