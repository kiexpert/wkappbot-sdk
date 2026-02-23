# AppBot Eye Global Mode Notes

## Current default behavior
- `wkappbot eye` runs in **global mode by default**.
- Legacy per-parent mode is only entered with `--legacy`.

## Placement rule (updated)
- When `--pos` is not provided, Eye is auto-placed at the **top-right of the right-most monitor**.
- This keeps Eye visible on multi-monitor setups where Telegram/Claude are often on the far-right display.

## UI intent
- Eye is not just a process monitor.
- Top area should show a simple **Kro thought card**:
  - `크로 보고:`
  - `크로 다음:`
  - `크로 막힘:` (only when relevant)
- Parent cards are shown below in recent-tick order.

## Run examples
```powershell
wkappbot eye
wkappbot eye --size 380x280
wkappbot eye --size 380x280 --pos 4090,475
wkappbot eye --legacy --app "Telegram"
```
