using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// HTS-specific interop: WM_COPYDATA form open, MDI child management.
/// Pattern ported from VIGSOne/tools/leak_test_repeat.ps1.
/// </summary>
public static class HtsInterop
{
    private const int COPYDATA_OPEN_FORM = 100;

    /// <summary>
    /// Open an HTS form via WM_COPYDATA (dwData=100).
    /// Path is encoded as system-default ANSI (CP949 on Korean Windows)
    /// to match the MBCS target application.
    /// </summary>
    public static bool OpenForm(IntPtr hMainWnd, string xmfFilePath)
    {
        // Encode as system default ANSI (CP949 on Korean Windows = matches MBCS app)
        byte[] pathBytes = Encoding.Default.GetBytes(xmfFilePath + "\0");

        IntPtr lpData = Marshal.AllocHGlobal(pathBytes.Length);
        try
        {
            Marshal.Copy(pathBytes, 0, lpData, pathBytes.Length);

            var cds = new COPYDATASTRUCT
            {
                dwData = new IntPtr(COPYDATA_OPEN_FORM),
                cbData = pathBytes.Length,
                lpData = lpData
            };

            NativeMethods.SendMessageW(hMainWnd, NativeMethods.WM_COPYDATA, IntPtr.Zero, ref cds);
            return true;
        }
        finally
        {
            Marshal.FreeHGlobal(lpData);
        }
    }

    /// <summary>
    /// Close the active MDI child window.
    /// </summary>
    public static bool CloseActiveChild(IntPtr hMainWnd)
    {
        var hChild = WindowFinder.GetActiveMDIChild(hMainWnd);
        if (hChild == IntPtr.Zero) return false;

        NativeMethods.SendMessageW(hChild, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        return true;
    }

    /// <summary>
    /// Run a stress test: open/close form N times.
    /// Reports progress via callback.
    /// </summary>
    public static void StressTest(
        IntPtr hMainWnd,
        string xmfFilePath,
        int iterations,
        int openDelayMs = 1000,
        int closeDelayMs = 1000,
        Action<int, string>? onProgress = null)
    {
        for (int i = 1; i <= iterations; i++)
        {
            // OPEN
            int beforeCount = WindowFinder.CountMDIChildren(hMainWnd);
            OpenForm(hMainWnd, xmfFilePath);
            Thread.Sleep(300);
            int afterCount = WindowFinder.CountMDIChildren(hMainWnd);

            string openStatus = afterCount > beforeCount
                ? $"OK (MDI: {beforeCount} -> {afterCount})"
                : $"SENT but no new child (MDI: {beforeCount} -> {afterCount})";
            onProgress?.Invoke(i, $"OPEN  {openStatus}");

            Thread.Sleep(Math.Max(0, openDelayMs - 300));

            // CLOSE
            bool closed = CloseActiveChild(hMainWnd);
            string closeStatus = closed ? "OK" : "SKIP (no active child)";
            onProgress?.Invoke(i, $"CLOSE {closeStatus}");

            Thread.Sleep(closeDelayMs);
        }
    }
}
