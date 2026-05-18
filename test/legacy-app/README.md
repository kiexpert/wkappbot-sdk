# LegacyControlZoo

`LegacyControlZoo` is a Win32 accessibility fallback test target for WKAppBot. It mixes standard controls that expose UI Automation with legacy owner-drawn and paint-only HWND regions that intentionally return no `WM_GETOBJECT` accessibility provider.

Window title:

```text
LegacyControlZoo - WKAppBot Test
```

## Controls Covered

| Control | Purpose |
| --- | --- |
| MDI frame and child window | Classic MDI hierarchy for legacy HWND discovery and child-window grap drills. |
| Owner-drawn menu bar | Paint-only top strip with visible GDI text and no MSAA/UIA provider. |
| Owner-drawn toolbar | Icon-only custom HWND buttons; labels are painted nearby for OCR. |
| Single-line and multi-line EDIT | Standard Win32 controls that should exercise UIA tier 1. |
| Owner-drawn LISTBOX | Win32 listbox with painted items to test message fallback and OCR item recovery. |
| COMBOBOX | Standard combo box for normal Win32/UIA behavior. |
| Owner-drawn BUTTON | `BS_OWNERDRAW` button with custom GDI text and no useful accessible name. |
| STATIC labels | Standard labels providing stable visible OCR anchors. |
| STATUSCLASSNAME status bar | Common-control status bar with owner-drawn text parts. |
| TREEVIEW custom draw | Tree view with custom-drawn coloring to test standard HWND plus custom visual handling. |
| LISTVIEW report mode | Report view with columns and rows for normal common-control discovery. |
| Custom panel | Paint-only child HWND with no UIA provider; intended for OCR/Vision fallback. |

## Build

```cmd
build.cmd
```

The script uses CMake. It prefers the Visual Studio generator when available and falls back to MinGW Makefiles if `g++` is on `PATH`.
