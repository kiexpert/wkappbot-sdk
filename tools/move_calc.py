import ctypes
import ctypes.wintypes
import time
import sys

user32 = ctypes.windll.user32

# === Win32 Unicode APIs ===
user32.FindWindowW.argtypes = [ctypes.c_wchar_p, ctypes.c_wchar_p]
user32.FindWindowW.restype = ctypes.wintypes.HWND
user32.SetCursorPos.argtypes = [ctypes.c_int, ctypes.c_int]
user32.MoveWindow.argtypes = [ctypes.wintypes.HWND, ctypes.c_int, ctypes.c_int, ctypes.c_int, ctypes.c_int, ctypes.c_bool]
user32.SetForegroundWindow.argtypes = [ctypes.wintypes.HWND]
user32.ShowWindow.argtypes = [ctypes.wintypes.HWND, ctypes.c_int]
user32.GetWindowTextW.argtypes = [ctypes.wintypes.HWND, ctypes.c_wchar_p, ctypes.c_int]
user32.IsWindowVisible.argtypes = [ctypes.wintypes.HWND]

WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, ctypes.wintypes.HWND, ctypes.wintypes.LPARAM)
user32.EnumWindows.argtypes = [WNDENUMPROC, ctypes.wintypes.LPARAM]

class RECT(ctypes.Structure):
    _fields_ = [("left", ctypes.c_long), ("top", ctypes.c_long),
                ("right", ctypes.c_long), ("bottom", ctypes.c_long)]

# Find calculator by title (Unicode!)
calc_hwnd = None

def enum_cb(hwnd, lparam):
    global calc_hwnd
    if not user32.IsWindowVisible(hwnd):
        return True
    buf = ctypes.create_unicode_buffer(256)
    user32.GetWindowTextW(hwnd, buf, 256)
    title = buf.value
    if "\uacc4\uc0b0\uae30" in title or "Calculator" in title:
        calc_hwnd = hwnd
        return False
    return True

user32.EnumWindows(WNDENUMPROC(enum_cb), 0)

if not calc_hwnd:
    sys.stdout.write("Calculator not found!\n")
    sys.exit(1)

sys.stdout.write("Calc HWND: 0x%08X\n" % calc_hwnd)

# Move and bring to front
user32.MoveWindow(calc_hwnd, 200, 100, 340, 520, True)
time.sleep(0.3)
user32.ShowWindow(calc_hwnd, 9)  # SW_RESTORE
user32.SetForegroundWindow(calc_hwnd)
time.sleep(0.8)

# Get rect
r = RECT()
user32.GetWindowRect(calc_hwnd, ctypes.byref(r))
cx, cy = r.left, r.top
cw, ch = r.right - r.left, r.bottom - r.top
sys.stdout.write("Calc at (%d,%d) %dx%d\n" % (cx, cy, cw, ch))

# Verify foreground
fg = user32.GetForegroundWindow()
buf = ctypes.create_unicode_buffer(256)
user32.GetWindowTextW(fg, buf, 256)
sys.stdout.write("Foreground: 0x%08X\n" % fg)

# Button layout for 340x520 UWP calc
btn_top = cy + 290
btn_h = 46
cols = [cx + 42, cx + 127, cx + 212, cx + 297]

moves = [
    (cx + 170, cy + 180, "display"),
    (cols[0], btn_top, "7"), (cols[1], btn_top, "8"),
    (cols[2], btn_top, "9"), (cols[3], btn_top, "divide"),
    (cols[0], btn_top + btn_h, "4"), (cols[1], btn_top + btn_h, "5"),
    (cols[2], btn_top + btn_h, "6"), (cols[3], btn_top + btn_h, "multiply"),
    (cols[0], btn_top + btn_h*2, "1"), (cols[1], btn_top + btn_h*2, "2"),
    (cols[2], btn_top + btn_h*2, "3"), (cols[3], btn_top + btn_h*2, "minus"),
    (cols[0], btn_top + btn_h*3, "negate"), (cols[1], btn_top + btn_h*3, "0"),
    (cols[2], btn_top + btn_h*3, "dot"), (cols[3], btn_top + btn_h*3, "plus"),
    (cols[3], btn_top + btn_h*4, "equals"),
    (cx + 170, cy + 15, "titlebar"),
]

sys.stdout.write("Moving over %d positions...\n" % len(moves))
for x, y, label in moves:
    user32.SetCursorPos(int(x), int(y))
    sys.stdout.write("  (%d,%d) %s\n" % (x, y, label))
    time.sleep(0.5)

sys.stdout.write("Done!\n")
