using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;

if (args.Length < 2)
{
    Console.WriteLine("Usage: InputProbe <window-title-substring> <text> [--cx N --cy N]");
    return 1;
}

var titleSub = args[0];
var text = args[1];
var clickX = GetInt("--cx", 120);
var clickY = GetInt("--cy", 120);

var target = ResolveTargetWindow(titleSub);
if (target == IntPtr.Zero)
{
    Console.WriteLine($"Target window not found: {titleSub}");
    return 2;
}
Console.WriteLine($"[TARGET] hwnd=0x{target.ToInt64():X} title='{GetWindowTitle(target)}'");

Native.ShowWindow(target, 9);
Native.SetForegroundWindow(target);
Thread.Sleep(120);

Native.GetWindowRect(target, out var rc);
var x = rc.Left + clickX;
var y = rc.Top + clickY;

// blocker detection: top window at input point must belong to target chain
var hit = Native.WindowFromPoint(new Native.POINT { X = x, Y = y });
var hitRoot = Native.GetAncestor(hit, 2);
var targetRoot = Native.GetAncestor(target, 2);
if (hit != IntPtr.Zero && hitRoot != IntPtr.Zero && hitRoot != targetRoot)
{
    var cls = GetClassName(hitRoot);
    var t = GetWindowTitle(hitRoot);
    Console.WriteLine($"[BLOCKER] top-window mismatch at ({x},{y}) cls={cls} title='{t}'");
    return 3;
}

Native.SetCursorPos(x, y);
Thread.Sleep(40);
MouseClick();
Thread.Sleep(80);

try { Interaction.AppActivate(GetWindowTitle(target)); } catch { }
Thread.Sleep(80);

// Path A: SendKeys (human-like)
var ws = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell")!);
ws!.GetType().InvokeMember("SendKeys", System.Reflection.BindingFlags.InvokeMethod, null, ws, new object[] { "^a" });
Thread.Sleep(60);
ws.GetType().InvokeMember("SendKeys", System.Reflection.BindingFlags.InvokeMethod, null, ws, new object[] { "{DEL}" });
Thread.Sleep(60);
ws.GetType().InvokeMember("SendKeys", System.Reflection.BindingFlags.InvokeMethod, null, ws, new object[] { text });
Thread.Sleep(120);

// Verify via known text controls (Edit / RichEdit*)
var got = "";
var candidates = FindChildTextControls(target);
foreach (var h in candidates)
{
    var t = GetWindowText(h);
    if (!string.IsNullOrWhiteSpace(t) && t.Contains(text, StringComparison.OrdinalIgnoreCase))
    {
        got = t;
        break;
    }
}

if (string.IsNullOrWhiteSpace(got))
{
    // Path B fallback: direct WM_SETTEXT to first known text control
    var first = candidates.FirstOrDefault();
    if (first != IntPtr.Zero)
    {
        Native.SendMessageW(first, 0x000C, IntPtr.Zero, text); // WM_SETTEXT
        Thread.Sleep(60);
        got = GetWindowText(first);
    }
}

var ok = !string.IsNullOrWhiteSpace(got) && got.Contains(text, StringComparison.OrdinalIgnoreCase);
Console.WriteLine($"InputProbe done: hwnd=0x{target.ToInt64():X} text='{text}' click=({x},{y}) verified={(ok ? "ok" : "fail")}");
return ok ? 0 : 4;

int GetInt(string key, int def)
{
    var idx = Array.IndexOf(args, key);
    if (idx >= 0 && idx + 1 < args.Length && int.TryParse(args[idx + 1], out var v)) return v;
    return def;
}

static string GetWindowTitle(IntPtr h)
{
    var sb = new StringBuilder(512);
    Native.GetWindowTextW(h, sb, sb.Capacity);
    return sb.ToString();
}

static string GetClassName(IntPtr h)
{
    var sb = new StringBuilder(256);
    Native.GetClassNameW(h, sb, sb.Capacity);
    return sb.ToString();
}

static string GetWindowText(IntPtr h)
{
    var sb = new StringBuilder(4096);
    Native.GetWindowTextW(h, sb, sb.Capacity);
    return sb.ToString();
}

static IntPtr FindForegroundNotepad()
{
    var fg = Native.GetForegroundWindow();
    if (fg == IntPtr.Zero) return IntPtr.Zero;
    var root = Native.GetAncestor(fg, 2);
    if (root == IntPtr.Zero) root = fg;
    var cls = GetClassName(root);
    return string.Equals(cls, "Notepad", StringComparison.OrdinalIgnoreCase) ? root : IntPtr.Zero;
}

static List<IntPtr> FindChildTextControls(IntPtr root)
{
    var list = new List<IntPtr>();
    Native.EnumChildWindows(root, (h, l) =>
    {
        var cls = GetClassName(h);
        if (string.Equals(cls, "Edit", StringComparison.OrdinalIgnoreCase) ||
            cls.Contains("RichEdit", StringComparison.OrdinalIgnoreCase) ||
            cls.Contains("TextBox", StringComparison.OrdinalIgnoreCase))
        {
            list.Add(h);
        }
        return true;
    }, IntPtr.Zero);
    return list;
}

static IntPtr ResolveTargetWindow(string titleSub)
{
    // 1) explicit title intent wins, even if another notepad is foreground
    IntPtr topMostNotepad = IntPtr.Zero;
    IntPtr titleMatched = IntPtr.Zero;
    Native.EnumWindows((h, l) =>
    {
        if (!Native.IsWindowVisible(h)) return true;
        var cls = GetClassName(h);
        if (!string.Equals(cls, "Notepad", StringComparison.OrdinalIgnoreCase)) return true;

        var t = GetWindowTitle(h);
        if (topMostNotepad == IntPtr.Zero) topMostNotepad = h;
        if (!string.IsNullOrWhiteSpace(titleSub) && !string.IsNullOrWhiteSpace(t) &&
            t.Contains(titleSub, StringComparison.OrdinalIgnoreCase))
        {
            titleMatched = h;
            return false; // first match in z-order
        }
        return true;
    }, IntPtr.Zero);

    if (titleMatched != IntPtr.Zero) return titleMatched;
    return topMostNotepad;
}

static void MouseClick()
{
    Native.mouse_event(0x0002, 0, 0, 0, UIntPtr.Zero);
    Native.mouse_event(0x0004, 0, 0, 0, UIntPtr.Zero);
}

static class Native
{
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")] public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(POINT Point);
    [DllImport("user32.dll")] public static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern IntPtr SendMessageW(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);
    [DllImport("user32.dll")] public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT { public int X, Y; }
}
