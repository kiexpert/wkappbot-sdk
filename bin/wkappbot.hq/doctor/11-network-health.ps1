# Network Health Check
# Checks latency to AI API endpoints

function Test-Endpoint {
    param($Name, $Url)
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    try {
        # Some endpoints reject HEAD. Use GET but limit response size.
        $res = Invoke-WebRequest -Uri $Url -Method Get -TimeoutSec 5 -ErrorAction Stop -UseBasicParsing -MaximumRedirection 0
        $sw.Stop()
        Add-Check "latency-$Name" "ok" "$($sw.ElapsedMilliseconds)ms"
        Emit "ok" "latency-$Name" "$($sw.ElapsedMilliseconds)ms"
    } catch {
        $sw.Stop()
        # If we got a 4xx or 5xx, it still means the server is reachable
        if ($_.Exception.Message -match "401|403|404|405|400") {
             Add-Check "latency-$Name" "ok" "$($sw.ElapsedMilliseconds)ms (Auth/Method required)"
             Emit "ok" "latency-$Name" "$($sw.ElapsedMilliseconds)ms"
        } else {
             Add-Check "latency-$Name" "warn" "Timeout/Error after $($sw.ElapsedMilliseconds)ms: $($_.Exception.Message)"
             Emit "!" "latency-$Name" "Failed/Slow ($($sw.ElapsedMilliseconds)ms)"
        }
    }
}

Test-Endpoint "google-ai" "https://generativelanguage.googleapis.com"
Test-Endpoint "anthropic" "https://api.anthropic.com"
Test-Endpoint "openai"    "https://api.openai.com"
