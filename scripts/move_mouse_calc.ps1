Add-Type @"
using System;
using System.Runtime.InteropServices;
using System.Text;

public class CalcMover2 {
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int cmd);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT r);
    [DllImport("user32.dll")] public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
    [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] public static extern int GetWindowTextW(IntPtr hWnd, StringBuilder sb, int max);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT { public int L; public int T; public int R; public int B; }

    public static string GetTitle(IntPtr h) {
        var sb = new StringBuilder(256);
        GetWindowTextW(h, sb, 256);
        return sb.ToString();
    }
}
"@

# Use appbot to find the calc window handle
$env:DOTNET_ROOT = "D:\$env:USERPROFILE\.dotnet"
$appbot = "D:\GitHub\AppBot\csharp\src\AppBot.CLI\bin\Release\net8.0-windows\appbot.exe"
$focusOutput = & $appbot focus --title "계산기" 2>&1 | Out-String

# Extract HWND from focus output
$match = [regex]::Match($focusOutput, "HWND\s*:\s*0x([0-9A-Fa-f]+)")
if (-not $match.Success) {
    Write-Host "Could not find calc HWND from appbot focus output"
    Write-Host $focusOutput
    exit 1
}

$hwndVal = [Convert]::ToInt64($match.Groups[1].Value, 16)
$h = [IntPtr]::new($hwndVal)
Write-Host "Calc HWND: 0x$($h.ToString('X8'))"

# Move and bring to front
[CalcMover2]::MoveWindow($h, 200, 100, 340, 520, $true) | Out-Null
Start-Sleep -Milliseconds 200
[CalcMover2]::ShowWindow($h, 5) | Out-Null
[CalcMover2]::SetForegroundWindow($h) | Out-Null
Start-Sleep -Milliseconds 500

$r = New-Object CalcMover2+RECT
[CalcMover2]::GetWindowRect($h, [ref]$r) | Out-Null
$cx = $r.L; $cy = $r.T
$cw = $r.R - $r.L; $ch = $r.B - $r.T
Write-Host "Calc at ($cx,$cy) ${cw}x${ch}"

# Verify it's in front
$fg = [CalcMover2]::GetForegroundWindow()
$fgTitle = [CalcMover2]::GetTitle($fg)
Write-Host "Foreground: 0x$($fg.ToString('X8')) '$fgTitle'"

# Button positions for 340x520 UWP calc
# Buttons area roughly starts at y=290, height ~46px each
# 4 columns at x offsets ~42, 127, 212, 297
$btnTop = $cy + 290
$btnH = 46
$c0 = $cx + 42
$c1 = $cx + 127
$c2 = $cx + 212
$c3 = $cx + 297

# Move to each position
function MoveTo($x, $y, $label) {
    [CalcMover2]::SetCursorPos($x, $y) | Out-Null
    Write-Host "  ($x,$y) $label"
    Start-Sleep -Milliseconds 500
}

Write-Host "Moving mouse..."
MoveTo ($cx+170) ($cy+180) "display"
MoveTo $c0 $btnTop "7"
MoveTo $c1 $btnTop "8"
MoveTo $c2 $btnTop "9"
MoveTo $c3 $btnTop "divide"
MoveTo $c0 ($btnTop+$btnH) "4"
MoveTo $c1 ($btnTop+$btnH) "5"
MoveTo $c2 ($btnTop+$btnH) "6"
MoveTo $c3 ($btnTop+$btnH) "multiply"
MoveTo $c0 ($btnTop+$btnH*2) "1"
MoveTo $c1 ($btnTop+$btnH*2) "2"
MoveTo $c2 ($btnTop+$btnH*2) "3"
MoveTo $c3 ($btnTop+$btnH*2) "minus"
MoveTo $c0 ($btnTop+$btnH*3) "negate"
MoveTo $c1 ($btnTop+$btnH*3) "0"
MoveTo $c2 ($btnTop+$btnH*3) "dot"
MoveTo $c3 ($btnTop+$btnH*3) "plus"
MoveTo $c3 ($btnTop+$btnH*4) "equals"
MoveTo ($cx+170) ($cy+15) "titlebar"
Write-Host "Done!"
