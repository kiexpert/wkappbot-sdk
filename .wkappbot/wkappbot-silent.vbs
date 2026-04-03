On Error Resume Next
Set fso = CreateObject("Scripting.FileSystemObject")
Set f = fso.OpenTextFile("W:\GitHub\WKAppBot\.wkappbot\watchdog.log", 8, True)
f.WriteLine "[WATCHDOG] " & Now() & " loop start"
f.Close
Set fso = Nothing
Set ws = CreateObject("WScript.Shell")
ws.CurrentDirectory = "W:\GitHub\WKAppBot"
Do
    Dim fLog
    Set fLog = CreateObject("Scripting.FileSystemObject").OpenTextFile("W:\GitHub\WKAppBot\.wkappbot\watchdog.log", 8, True)
    fLog.WriteLine "[WATCHDOG] " & Now() & " spawn guardian"
    fLog.Close
    ws.Run """W:\SDK\bin\wkappbot-core.exe"" eye guardian --respawn-delay 10 --poll-ms 10000 --tick-timeout-ms 5000", 0, True
    Dim fLog2
    Set fLog2 = CreateObject("Scripting.FileSystemObject").OpenTextFile("W:\GitHub\WKAppBot\.wkappbot\watchdog.log", 8, True)
    fLog2.WriteLine "[WATCHDOG] " & Now() & " guardian exited, sleep 30s"
    fLog2.Close
    WScript.Sleep 30000
Loop
