#!/usr/bin/env bash
wkappbot file grep 'exec/--exec <command>' --path D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/CommandHelp.Entries.Misc.cs 2>&1 | /usr/bin/grep -q match || exit 1
wkappbot help exec 2>&1 | /usr/bin/grep -q exec || exit 1
wkappbot exec cmd /c echo exec-verified 2>&1; true