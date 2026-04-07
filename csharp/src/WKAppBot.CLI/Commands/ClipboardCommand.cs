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

    [DllImport("user32.dll")] static extern uint RegisterClipboardFormatW([MarshalAs(UnmanagedType.LPWStr)] string lpszFormat);

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

    static int ClipboardWrite(string text, bool asHtml = false)
    {
        if (!OpenClipboard(IntPtr.Zero))
        {
            Console.Error.WriteLine("[CLIPBOARD] Failed to open clipboard");
            return 1;
        }

        try
        {
            EmptyClipboard();

            // CF_UNICODETEXT: always set plain text (strip HTML tags for plain text fallback)
            var plainText = asHtml ? System.Text.RegularExpressions.Regex.Replace(text, "<[^>]+>", "") : text;
            SetClipboardUnicode(plainText);

            // CF_HTML: rich paste for Gmail/Outlook/etc.
            if (asHtml)
            {
                var cfHtml = RegisterClipboardFormatW("HTML Format");
                if (cfHtml != 0)
                {
                    // CF_HTML requires a specific header format (UTF-8):
                    // Version:0.9\r\nStartHTML:XXXXX\r\nEndHTML:XXXXX\r\nStartFragment:XXXXX\r\nEndFragment:XXXXX\r\n
                    const string startFrag = "<!--StartFragment-->";
                    const string endFrag = "<!--EndFragment-->";
                    var header = "Version:0.9\r\nStartHTML:{0:D10}\r\nEndHTML:{1:D10}\r\nStartFragment:{2:D10}\r\nEndFragment:{3:D10}\r\n";
                    var htmlBody = $"<html><body>\r\n{startFrag}{text}{endFrag}\r\n</body></html>";

                    // Calculate offsets (header is variable-length due to number formatting)
                    var headerLen = string.Format(header, 0, 0, 0, 0).Length;
                    var startHtml = headerLen;
                    var startFragment = startHtml + htmlBody.IndexOf(startFrag, StringComparison.Ordinal) + startFrag.Length;
                    var endFragment = startHtml + htmlBody.IndexOf(endFrag, StringComparison.Ordinal);
                    var endHtml = startHtml + htmlBody.Length;

                    var cfHtmlStr = string.Format(header, startHtml, endHtml, startFragment, endFragment) + htmlBody;
                    var cfHtmlBytes = Encoding.UTF8.GetBytes(cfHtmlStr + "\0");

                    var hMemHtml = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)cfHtmlBytes.Length);
                    if (hMemHtml != IntPtr.Zero)
                    {
                        var ptrHtml = GlobalLock(hMemHtml);
                        if (ptrHtml != IntPtr.Zero)
                        {
                            Marshal.Copy(cfHtmlBytes, 0, ptrHtml, cfHtmlBytes.Length);
                            GlobalUnlock(hMemHtml);
                            if (SetClipboardData(cfHtml, hMemHtml) == IntPtr.Zero)
                                GlobalFree(hMemHtml);
                            else
                                Console.Error.WriteLine($"[CLIPBOARD] CF_HTML set ({cfHtmlBytes.Length} bytes)");
                        }
                        else
                            GlobalFree(hMemHtml);
                    }
                }
            }

            Console.Error.WriteLine($"[CLIPBOARD] wrote {text.Length} chars{(asHtml ? " (HTML+text)" : "")}");
            return 0;
        }
        finally
        {
            CloseClipboard();
        }
    }

    static void SetClipboardUnicode(string text)
    {
        var chars = text.ToCharArray();
        int byteCount = (chars.Length + 1) * 2;
        var hMem = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)byteCount);
        if (hMem == IntPtr.Zero) return;

        var ptr = GlobalLock(hMem);
        if (ptr == IntPtr.Zero) { GlobalFree(hMem); return; }

        Marshal.Copy(chars, 0, ptr, chars.Length);
        Marshal.WriteInt16(ptr, chars.Length * 2, 0);
        GlobalUnlock(hMem);

        if (SetClipboardData(CF_UNICODETEXT, hMem) == IntPtr.Zero)
            GlobalFree(hMem);
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

            Console.Error.WriteLine($"[CLIPBOARD] {resolved.Length} file(s) copied:");
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
