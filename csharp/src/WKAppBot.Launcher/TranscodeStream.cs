using System.Runtime.InteropServices;
using System.Text;

namespace WKAppBot.Launcher;

/// <summary>
/// Wraps an output Stream, transcoding UTF-8 input bytes -> target code page on the fly.
/// Uses Win32 WideCharToMultiByte -- works in NativeAOT without CodePagesEncodingProvider.
/// Handles split multi-byte sequences correctly via a stateful UTF-8 Decoder.
/// </summary>
internal sealed class TranscodeStream : Stream
{
    private readonly Stream _inner;
    private readonly Decoder _utf8Dec;
    private readonly int _codePage;
    private readonly char[] _charBuf = new char[4096];
    private readonly byte[] _byteBuf = new byte[8192];

    // CharSet.Unicode + explicit EntryPoint: prevents char[] from being marshaled as ANSI bytes.
    // Without CharSet.Unicode, each char is ANSI-marshaled -> Korean chars become '?' before Win32 sees them.
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WideCharToMultiByte")]
    static extern int WideCharToMultiByte(int codePage, uint dwFlags,
        char[] lpWideCharStr, int cchWideChar,
        byte[] lpMultiByteStr, int cbMultiByte,
        nint lpDefaultChar, nint lpUsedDefaultChar);

    public TranscodeStream(Stream inner, int codePage)
    {
        _inner = inner;
        _utf8Dec = System.Text.Encoding.UTF8.GetDecoder();
        _codePage = codePage;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        // Decode UTF-8 -> chars (stateful: handles sequences split across Write calls)
        int charCount = _utf8Dec.GetChars(buffer, offset, count, _charBuf, 0);
        if (charCount == 0) return;
        // Re-encode chars -> target CP via Win32 (no managed encoding stack needed)
        int byteCount = WideCharToMultiByte(_codePage, 0, _charBuf, charCount, _byteBuf, _byteBuf.Length, 0, 0);
        if (byteCount > 0)
            _inner.Write(_byteBuf, 0, byteCount);
    }

    public override void Flush() => _inner.Flush();
    protected override void Dispose(bool disposing) { if (disposing) _inner.Dispose(); base.Dispose(disposing); }

    public override bool CanRead  => false;
    public override bool CanSeek  => false;
    public override bool CanWrite => true;
    public override long Length   => throw new NotSupportedException();
    public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
    public override int  Read(byte[] buffer, int offset, int count)    => throw new NotSupportedException();
    public override long Seek(long offset, SeekOrigin origin)          => throw new NotSupportedException();
    public override void SetLength(long value)                         => throw new NotSupportedException();
}
