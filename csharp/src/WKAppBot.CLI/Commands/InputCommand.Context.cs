using System.Text;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Result of a single input method attempt in the 12-tier fallback chain.
    /// </summary>
    enum InputResult
    {
        /// <summary>Method succeeded — stop the chain.</summary>
        Success,
        /// <summary>Method not applicable or failed softly — try next method.</summary>
        Continue,
        /// <summary>Fatal error (window closed, target gone) — stop the chain immediately.</summary>
        Abort,
    }

    /// <summary>
    /// Shared mutable state for the 12-tier input fallback chain.
    /// Passed by reference to each TryMethodN() — methods can read and modify shared state.
    ///
    /// Design decisions (Triad debate 2026-03-28):
    /// - Mutable class (not record): OCR/bitmap/hwnd mutation across methods needs shared visibility
    /// - Single object (not split): No variable declared in one method is used by a later method
    /// - No strategy pattern: 12-tier order = empirical domain knowledge, hardcoded
    /// </summary>
    sealed class InputContext
    {
        // ── Immutable inputs (set once during setup) ──
        public required string Title { get; init; }
        public required string Text { get; init; }
        public required string TargetFormId { get; init; }
        public required int TargetCid { get; init; }
        public required bool SendEnter { get; init; }
        public required int? MethodOnly { get; init; }
        public required int ClickXOffset { get; init; }

        // ── Resolved targets (set during setup, read-only in methods) ──
        public required WindowInfo Win { get; init; }
        public required IntPtr MdiClient { get; init; }
        public required WindowInfo TargetForm { get; init; }
        public required IntPtr TargetHwnd { get; init; }
        public required RECT CtlRect { get; init; }
        public required ExperienceDb? ExpDb { get; init; }
        public required InputReadinessReport ReadinessReport { get; init; }

        // ── Helper delegates (closures from setup, used by multiple methods) ──
        public required Func<string, string> OcrDebugPath { get; init; }
        public required Func<string, bool> ConfirmByOcrContains { get; init; }
        public required Func<IntPtr, IntPtr, byte[]?> CaptureControlPng { get; init; }
        public required Func<IntPtr, IntPtr, byte[]?> CaptureControlFast { get; init; }
        public required Func<string, string, int, bool> FuzzyDigitMatch { get; init; }

        // ── Abort flag (any method can set to stop the chain) ──
        public bool AbortChain { get; set; }

        /// <summary>Check if a specific method should run (respects --method N filter).</summary>
        public bool ShouldTry(int methodNumber)
            => MethodOnly == null || MethodOnly == methodNumber;

        /// <summary>Check if a named method should run (for Method A11Y which has no number).</summary>
        public bool ShouldTryA11y()
            => MethodOnly == null || MethodOnly == 0; // 0 = A11Y pseudo-number
    }
}
