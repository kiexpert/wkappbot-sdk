# Troubleshooting

## `wkappbot: command not found`

`bin\` is not in your PATH. Fix:
```powershell
[Environment]::SetEnvironmentVariable('PATH',
  "$env:USERPROFILE\Documents\wkappbot\bin;$([Environment]::GetEnvironmentVariable('PATH','User'))",
  'User')
```
Restart the terminal.

## License shows Free after payment

You must **accept the GitHub collaborator invitation** before the license activates.
Check: <https://github.com/notifications> or your email.

After accepting:
```bash
wkappbot license status   # should show CDP or Sudo
```

## Eye won't start / `eye tick` fails

Check the log:
```bash
wkappbot file glob "**/*.log" --path bin\wkappbot.hq\logs
wkappbot logcat error --past 10m
```

Common causes:
- Another Eye already running (`wkappbot windows *eye*`)
- Port conflict on named pipe — check with `wkappbot eye tick --timeout 3`

## Build fails: `vswhere.exe not recognized`

Install Visual Studio Build Tools 2022 (needed for AOT launcher build).
The `build.cmd` auto-prepends the VS Installer path — if missing, install from:
<https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022>

## CDP commands blocked (overlay appears)

CDP features require **CDP tier** or above. Check your license:
```bash
wkappbot license status
```
Free tier users see a dismissible overlay after 1 minute of CDP use.

## `wkappbot-core.exe` missing after build

Run `build.cmd --download-only` to re-download the pre-built core.
If download fails, `build.cmd` falls back to source build (requires .NET SDK 8+).

## Korean text garbled in terminal

Set the terminal code page:
```bat
chcp 65001
```
Or use Windows Terminal which handles UTF-8 natively.

## Collect diagnostics for a bug report

```bash
wkappbot --version
wkappbot eye tick --timeout 5
wkappbot logcat error --past 30m
```
Include this output when filing a [bug report](https://github.com/kiexpert/wkappbot-sdk/issues/new?template=bug_report.md).
