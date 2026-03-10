using System.Runtime.InteropServices;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int ClipboardCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  wkappbot clipboard read");
            Console.WriteLine("      Read clipboard text with automatic CP949→UTF-8 conversion.");
            Console.WriteLine("      Detects encoding: if CF_UNICODETEXT available, use it;");
            Console.WriteLine("      otherwise read CF_TEXT and convert from CP949.");
            return 0;
        }

        var sub = args[0].ToLowerInvariant();

        if (sub == "read")
            return ClipboardRead();

        return Error($"Unknown clipboard subcommand: {sub}");
    }

    [DllImport("user32.dll")] static extern bool OpenClipboard(IntPtr hWnd);
    [DllImport("user32.dll")] static extern bool CloseClipboard();
    [DllImport("user32.dll")] static extern bool EmptyClipboard();
    [DllImport("user32.dll")] static extern IntPtr GetClipboardData(uint uFormat);
    [DllImport("user32.dll")] static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
    [DllImport("user32.dll")] static extern bool IsClipboardFormatAvailable(uint format);
    [DllImport("kernel32.dll")] static extern IntPtr GlobalLock(IntPtr hMem);
    [DllImport("kernel32.dll")] static extern bool GlobalUnlock(IntPtr hMem);
    [DllImport("kernel32.dll")] static extern int GlobalSize(IntPtr hMem);
    [DllImport("kernel32.dll")] static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);
    [DllImport("kernel32.dll")] static extern bool GlobalFree(IntPtr hMem);

    const uint CF_TEXT = 1;
    const uint CF_UNICODETEXT = 13;
    const uint CF_HDROP = 15;
    const uint GMEM_MOVEABLE = 0x0002;

    static int ClipboardRead()
    {
        if (!OpenClipboard(IntPtr.Zero))
        {
            Console.Error.WriteLine("[CLIPBOARD] Failed to open clipboard");
            return 1;
        }

        try
        {
            // Prefer CF_UNICODETEXT (already UTF-16, no conversion needed)
            if (IsClipboardFormatAvailable(CF_UNICODETEXT))
            {
                var hMem = GetClipboardData(CF_UNICODETEXT);
                if (hMem != IntPtr.Zero)
                {
                    var ptr = GlobalLock(hMem);
                    if (ptr != IntPtr.Zero)
                    {
                        try
                        {
                            string text = Marshal.PtrToStringUni(ptr) ?? "";
                            Console.Write(text);
                            return 0;
                        }
                        finally { GlobalUnlock(hMem); }
                    }
                }
            }

            // Fallback: CF_TEXT (ANSI — likely CP949 on Korean Windows)
            if (IsClipboardFormatAvailable(CF_TEXT))
            {
                var hMem = GetClipboardData(CF_TEXT);
                if (hMem != IntPtr.Zero)
                {
                    var ptr = GlobalLock(hMem);
                    if (ptr != IntPtr.Zero)
                    {
                        try
                        {
                            int size = GlobalSize(hMem);
                            var bytes = new byte[size];
                            Marshal.Copy(ptr, bytes, 0, size);

                            // Trim null terminator
                            int end = Array.IndexOf(bytes, (byte)0);
                            if (end >= 0) bytes = bytes[..end];

                            // Try UTF-8 first, then CP949
                            string text;
                            if (IsValidUtf8(bytes))
                            {
                                text = Encoding.UTF8.GetString(bytes);
                            }
                            else
                            {
                                // CP949 (Korean Windows ANSI codepage)
                                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                                var cp949 = Encoding.GetEncoding(949);
                                text = cp949.GetString(bytes);
                            }

                            Console.Write(text);
                            return 0;
                        }
                        finally { GlobalUnlock(hMem); }
                    }
                }
            }

            Console.Error.WriteLine("[CLIPBOARD] No text data on clipboard");
            return 1;
        }
        finally
        {
            CloseClipboard();
        }
    }

    static bool IsValidUtf8(byte[] bytes)
    {
        int i = 0;
        while (i < bytes.Length)
        {
            byte b = bytes[i];
            int seqLen;
            if (b <= 0x7F) { i++; continue; }
            else if ((b & 0xE0) == 0xC0) seqLen = 2;
            else if ((b & 0xF0) == 0xE0) seqLen = 3;
            else if ((b & 0xF8) == 0xF0) seqLen = 4;
            else return false; // invalid lead byte

            if (i + seqLen > bytes.Length) return false;
            for (int j = 1; j < seqLen; j++)
            {
                if ((bytes[i + j] & 0xC0) != 0x80) return false;
            }
            i += seqLen;
        }
        return i > 0 && bytes.Any(b => b > 0x7F); // must have non-ASCII to be "detected" as UTF-8
    }

    static int ClipboardWrite(string text)
    {
        if (!OpenClipboard(IntPtr.Zero))
        {
            Console.Error.WriteLine("[CLIPBOARD] Failed to open clipboard");
            return 1;
        }

        try
        {
            EmptyClipboard();

            // CF_UNICODETEXT: UTF-16LE null-terminated
            var chars = text.ToCharArray();
            int byteCount = (chars.Length + 1) * 2; // +1 for null terminator
            var hMem = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)byteCount);
            if (hMem == IntPtr.Zero)
            {
                Console.Error.WriteLine("[CLIPBOARD] GlobalAlloc failed");
                return 1;
            }

            var ptr = GlobalLock(hMem);
            if (ptr == IntPtr.Zero)
            {
                GlobalFree(hMem);
                Console.Error.WriteLine("[CLIPBOARD] GlobalLock failed");
                return 1;
            }

            Marshal.Copy(chars, 0, ptr, chars.Length);
            Marshal.WriteInt16(ptr, chars.Length * 2, 0); // null terminator
            GlobalUnlock(hMem);

            if (SetClipboardData(CF_UNICODETEXT, hMem) == IntPtr.Zero)
            {
                GlobalFree(hMem);
                Console.Error.WriteLine("[CLIPBOARD] SetClipboardData failed");
                return 1;
            }

            // hMem now owned by clipboard — do NOT free
            Console.WriteLine($"[CLIPBOARD] wrote {text.Length} chars");
            return 0;
        }
        finally
        {
            CloseClipboard();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct DROPFILES
    {
        public uint pFiles;  // offset to file list
        public int pt_x;
        public int pt_y;
        public bool fNC;
        public bool fWide;   // true = Unicode
    }

    static int ClipboardWriteFiles(string[] filePaths)
    {
        // Resolve to absolute paths
        var resolved = filePaths.Select(f => Path.GetFullPath(f)).ToArray();
        foreach (var f in resolved)
        {
            if (!File.Exists(f) && !Directory.Exists(f))
            {
                Console.Error.WriteLine($"[CLIPBOARD] not found: {f}");
                return 1;
            }
        }

        if (!OpenClipboard(IntPtr.Zero))
        {
            Console.Error.WriteLine("[CLIPBOARD] Failed to open clipboard");
            return 1;
        }

        try
        {
            EmptyClipboard();

            // CF_HDROP format: DROPFILES header + double-null-terminated Unicode file list
            int headerSize = Marshal.SizeOf<DROPFILES>();
            // Ensure offset is aligned (typically 20 bytes)
            int dataOffset = headerSize;

            // Build file list: each path null-terminated, extra null at end
            var fileListBuilder = new StringBuilder();
            foreach (var f in resolved)
            {
                fileListBuilder.Append(f);
                fileListBuilder.Append('\0');
            }
            fileListBuilder.Append('\0'); // double-null terminator

            var fileList = fileListBuilder.ToString();
            int totalSize = dataOffset + fileList.Length * 2;

            var hMem = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)totalSize);
            if (hMem == IntPtr.Zero)
            {
                Console.Error.WriteLine("[CLIPBOARD] GlobalAlloc failed");
                return 1;
            }

            var ptr = GlobalLock(hMem);
            if (ptr == IntPtr.Zero)
            {
                GlobalFree(hMem);
                Console.Error.WriteLine("[CLIPBOARD] GlobalLock failed");
                return 1;
            }

            // Write DROPFILES header
            var df = new DROPFILES
            {
                pFiles = (uint)dataOffset,
                fWide = true // Unicode
            };
            Marshal.StructureToPtr(df, ptr, false);

            // Write file list
            Marshal.Copy(fileList.ToCharArray(), 0, ptr + dataOffset, fileList.Length);
            GlobalUnlock(hMem);

            if (SetClipboardData(CF_HDROP, hMem) == IntPtr.Zero)
            {
                GlobalFree(hMem);
                Console.Error.WriteLine("[CLIPBOARD] SetClipboardData CF_HDROP failed");
                return 1;
            }

            Console.WriteLine($"[CLIPBOARD] {resolved.Length} file(s) copied:");
            foreach (var f in resolved)
                Console.WriteLine($"  {f}");
            return 0;
        }
        finally
        {
            CloseClipboard();
        }
    }
}
