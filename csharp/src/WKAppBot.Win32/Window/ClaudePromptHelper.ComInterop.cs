using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    // ===============================================================
    // MSAA/IA2 COM Interop for focusless text insertion
    //
    // Discovery (2026-02-26):
    //   Chromium's contentEditable has ROLE_SYSTEM_GROUPING (not ROLE_TEXT),
    //   so IAccessibleEditableText is NOT available. However, direct MSAA vtable
    //   put_accValue() on the parent element (name="입력하세요") works!
    //
    //   Why it works when FlaUI's LegacyIAccessible.SetValue returns E_NOTIMPL:
    //   FlaUI goes through UIA->MSAA proxy bridge which adds validation.
    //   Direct MSAA COM vtable call bypasses the proxy entirely.
    //
    // MSAA tree structure (Claude Desktop prompt):
    //   grandparent: GROUPING, name=null
    //     +─ parent: GROUPING, name="입력하세요"  <- put_accValue target!
    //          +─ self: GROUPING, name=null       <- AccessibleObjectFromPoint hit
    //               +─ child: GROUPING or TEXT
    //               +─ child: WHITESPACE, name="\n"
    // ===============================================================

    [DllImport("oleacc.dll")]
    private static extern int AccessibleObjectFromPoint(
        Native.POINT pt,
        [MarshalAs(UnmanagedType.Interface)] out object ppacc,
        [MarshalAs(UnmanagedType.Struct)] out object pvarChild);

    // OLE IServiceProvider -- used to QueryService for IA2 from IAccessible
    [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IOleServiceProvider
    {
        [PreserveSig]
        int QueryService(ref Guid guidService, ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);
    }

    // IAccessibleEditableText -- the IA2 interface for text input
    // vtable order MUST match the IA2 IDL exactly!
    [ComImport, Guid("A59AA09A-7011-4b65-939D-32B1FB5547E3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleEditableText
    {
        [PreserveSig]
        int copyText(int startOffset, int endOffset);
        [PreserveSig]
        int deleteText(int startOffset, int endOffset);
        [PreserveSig]
        int insertText(int offset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int cutText(int startOffset, int endOffset);
        [PreserveSig]
        int pasteText(int offset);
        [PreserveSig]
        int replaceText(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int setAttributes(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string attributes);
    }

    // IAccessible via vtable -- for MSAA tree navigation (role probing + child access)
    // Chromium doesn't support IDispatch late binding -> must use vtable (InterfaceIsIUnknown)
    // Vtable order: IUnknown(3) + IDispatch(4) + IAccessible(21) = 28 slots
    [ComImport, Guid("618736E0-3C3D-11CF-810C-00AA00389B71"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleVtbl
    {
        // -- IDispatch (4 slots, never called -- consuming vtable positions) --
        void _QueryInfoCount_slot();
        void _GetTypeInfo_slot();
        void _GetIDsOfNames_slot();
        void _Invoke_slot();

        // -- IAccessible (21 methods) --
        [PreserveSig]
        int get_accParent([MarshalAs(UnmanagedType.IDispatch)] out object? ppdispParent);
        [PreserveSig]
        int get_accChildCount(out int pcountChildren);
        [PreserveSig]
        int get_accChild([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.IDispatch)] out object? ppdispChild);
        [PreserveSig]
        int get_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszName);
        [PreserveSig]
        int get_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszValue);
        [PreserveSig]
        int get_accDescription([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDescription);
        [PreserveSig]
        int get_accRole([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarRole);
        [PreserveSig]
        int get_accState([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarState);
        [PreserveSig]
        int get_accHelp([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszHelp);
        [PreserveSig]
        int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string? pszHelpFile,
            [MarshalAs(UnmanagedType.Struct)] object varChild, out int pidTopic);
        [PreserveSig]
        int get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszKeyboardShortcut);
        [PreserveSig]
        int get_accFocus([MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int get_accSelection([MarshalAs(UnmanagedType.Struct)] out object? pvarChildren);
        [PreserveSig]
        int get_accDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDefaultAction);
        [PreserveSig]
        int accSelect(int flagsSelect, [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight,
            [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accNavigate(int navDir, [MarshalAs(UnmanagedType.Struct)] object varStart,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarEndUpAt);
        [PreserveSig]
        int accHitTest(int xLeft, int yTop, [MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int accDoDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int put_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szName);
        [PreserveSig]
        int put_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szValue);
    }

    private static readonly Guid IID_IAccessible = new("618736E0-3C3D-11CF-810C-00AA00389B71");
    private static readonly Guid IID_IAccessible2 = new("E89F726E-C4F4-4c19-BB19-B647D7FA8478");
    private static readonly Guid IID_IAccessibleEditableText = new("A59AA09A-7011-4b65-939D-32B1FB5547E3");

    /// <summary>
    /// Try to insert text focuslessly using MSAA put_accValue (direct vtable).
    /// Priority 1: put_accValue on parent MSAA element (proven working on Claude Desktop)
    /// Priority 2: IAccessibleEditableText.insertText (IA2, not available for contentEditable)
    /// Strategy: AccessibleObjectFromPoint -> navigate to parent -> put_accValue
    /// </summary>
    private bool TryIA2InsertText(PromptInfo prompt, string text)
    {
        try
        {
            Console.WriteLine("  [PROMPT] Trying IA2 IAccessibleEditableText.insertText()...");

            var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
            var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
            var pt = new Native.POINT(centerX, centerY);

            int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? childId);
            if (hr != 0 || acc == null)
            {
                Console.WriteLine($"  [PROMPT] IA2: AccessibleObjectFromPoint failed (hr=0x{hr:X8})");
                return false;
            }

            var childIdStr = childId switch
            {
                int id => id == 0 ? "CHILDID_SELF" : $"child={id}",
                _ => $"type={childId?.GetType().Name ?? "null"}"
            };
            Console.WriteLine($"  [PROMPT] IA2: Got MSAA object at ({centerX},{centerY}), {childIdStr}");

            try
            {
                ProbeAccessibleInfo(acc, "  [PROMPT] IA2 [self]");

                // === Priority 1: Try put_accValue on parent (the "입력하세요" contentEditable) ===
                if (acc is IAccessibleVtbl selfForParent)
                {
                    selfForParent.get_accParent(out object? parentForValue);
                    if (parentForValue is IAccessibleVtbl parentVtbl)
                    {
                        string? parentName = null;
                        try { parentVtbl.get_accName(0, out parentName); } catch { }
                        Console.WriteLine($"  [PROMPT] IA2: Trying put_accValue on parent (\"{parentName}\")...");
                        try
                        {
                            int pvHr = parentVtbl.put_accValue(0, text);
                            if (pvHr == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue SUCCEEDED! ({text.Length} chars, focusless!)");
                                return true;
                            }
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue returned hr=0x{pvHr:X8}");
                        }
                        catch (Exception pvEx)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue error: {pvEx.Message}");
                        }

                        // Also try on self
                        try
                        {
                            int pvHr2 = selfForParent.put_accValue(0, text);
                            if (pvHr2 == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue on self SUCCEEDED! ({text.Length} chars)");
                                return true;
                            }
                        }
                        catch { }
                    }
                }

                // === Priority 2: Try IAccessibleEditableText ===
                if (TryInsertOnObject(acc, text, "self"))
                    return true;

                // Navigate to parent and try IAccessibleEditableText there
                if (acc is IAccessibleVtbl accV)
                {
                    try
                    {
                        accV.get_accParent(out object? parent);
                        if (parent != null)
                        {
                            ProbeAccessibleInfo(parent, "  [PROMPT] IA2 [parent]");
                            if (TryInsertOnObject(parent, text, "parent"))
                                return true;

                            if (parent is IAccessibleVtbl parentV)
                            {
                                try
                                {
                                    parentV.get_accParent(out object? grandparent);
                                    if (grandparent != null)
                                    {
                                        ProbeAccessibleInfo(grandparent, "  [PROMPT] IA2 [grandparent]");
                                        if (TryInsertOnObject(grandparent, text, "grandparent"))
                                            return true;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Parent navigation error: {ex.Message}");
                    }

                    try
                    {
                        accV.get_accChildCount(out int childCount);
                        if (childCount > 0)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: Probing {childCount} children...");
                            for (int i = 1; i <= Math.Min(childCount, 10); i++)
                            {
                                try
                                {
                                    int hr2 = accV.get_accChild(i, out object? child);
                                    if (hr2 == 0 && child != null)
                                    {
                                        ProbeAccessibleInfo(child, $"  [PROMPT] IA2 [child {i}]");
                                        if (TryInsertOnObject(child, text, $"child[{i}]"))
                                            return true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  [PROMPT] IA2 [child {i}]: get_accChild hr=0x{hr2:X8} (simple element or null)");
                                    }
                                }
                                catch (Exception cex)
                                {
                                    Console.WriteLine($"  [PROMPT] IA2 [child {i}]: error ({cex.Message})");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Child navigation error: {ex.Message}");
                    }
                }

                // Deep recursive search
                if (acc is IAccessibleVtbl selfV)
                {
                    selfV.get_accParent(out object? searchRoot);
                    if (searchRoot is IAccessibleVtbl rootV)
                    {
                        rootV.get_accParent(out object? gp);
                        if (gp != null) searchRoot = gp;
                    }
                    if (searchRoot != null)
                    {
                        Console.WriteLine("  [PROMPT] IA2: Deep recursive search for IAccessibleEditableText...");
                        if (TryInsertRecursive(searchRoot, text, 0, 5))
                            return true;
                    }
                }

                Console.WriteLine("  [PROMPT] IA2: No focusless insertion method found");
                return false;
            }
            finally
            {
                try { Marshal.ReleaseComObject(acc); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] IA2 error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Log MSAA role, name, childCount for diagnostics using vtable-bound IAccessible.
    /// </summary>
    private static void ProbeAccessibleInfo(object acc, string prefix)
    {
        if (acc is not IAccessibleVtbl v)
        {
            Console.WriteLine($"{prefix}: not IAccessibleVtbl (type={acc.GetType().Name})");
            return;
        }
        try
        {
            int childCount = 0;
            v.get_accChildCount(out childCount);
            object? role = null;
            try { v.get_accRole(0, out role); } catch { }
            string? name = null;
            try { v.get_accName(0, out name); } catch { }
            // MSAA role constants: 42=ROLE_SYSTEM_TEXT, 9=ROLE_SYSTEM_CLIENT,
            // 20=ROLE_SYSTEM_GROUPING, 15=ROLE_SYSTEM_DOCUMENT, 14=ROLE_SYSTEM_WINDOW
            var roleStr = role is int r ? $"role={r} (0x{r:X2})" : $"role={role}";
            Console.WriteLine($"{prefix}: {roleStr}, name=\"{name ?? "(null)"}\", children={childCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{prefix}: probe failed ({ex.Message})");
        }
    }

    /// <summary>
    /// Recursively walk the MSAA tree to find any element supporting IAccessibleEditableText.
    /// </summary>
    private bool TryInsertRecursive(object acc, string text, int depth, int maxDepth)
    {
        if (depth > maxDepth) return false;
        var indent = new string(' ', depth * 2);

        if (acc is IAccessibleVtbl v)
        {
            object? role = null;
            string? name = null;
            try { v.get_accRole(0, out role); } catch { }
            try { v.get_accName(0, out name); } catch { }
            var roleInt = role is int r ? r : -1;

            if (roleInt != 20)
                Console.WriteLine($"  [PROMPT] IA2 {indent}depth={depth}: role={roleInt} (0x{roleInt:X2}), name=\"{name ?? ""}\"");

            if (TryInsertOnObject(acc, text, $"depth{depth}"))
                return true;

            v.get_accChildCount(out int childCount);
            for (int i = 1; i <= Math.Min(childCount, 20); i++)
            {
                try
                {
                    int hr = v.get_accChild(i, out object? child);
                    if (hr == 0 && child != null)
                    {
                        if (TryInsertRecursive(child, text, depth + 1, maxDepth))
                            return true;
                    }
                }
                catch { }
            }
        }
        return false;
    }

    /// <summary>
    /// Try all approaches to get IAccessibleEditableText and call insertText on a COM object.
    /// Tries: Direct QI -> QueryService(IA2)->QI -> QueryService(IAccessibleEditableText)
    /// </summary>
    private bool TryInsertOnObject(object acc, string text, string label)
    {
        int hr;

        if (acc is IAccessibleEditableText directEdit)
        {
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: Direct QI succeeded! Calling insertText...");
            hr = directEdit.insertText(0, text);
            if (hr == 0)
            {
                Console.WriteLine($"  [PROMPT] IA2: insertText on {label} SUCCEEDED! ({text.Length} chars, fully focusless!)");
                return true;
            }
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
        }

        if (acc is IOleServiceProvider sp)
        {
            var guidIAcc = IID_IAccessible;
            var guidIA2 = IID_IAccessible2;
            var guidET = IID_IAccessibleEditableText;

            hr = sp.QueryService(ref guidIAcc, ref guidIA2, out object? ia2Obj);
            if (hr == 0 && ia2Obj != null)
            {
                if (ia2Obj is IAccessibleEditableText ia2Edit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: IA2->EditableText QI succeeded! Calling insertText...");
                    hr = ia2Edit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via IA2 on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                hr = sp.QueryService(ref guidIAcc, ref guidET, out object? etObj);
                if (hr == 0 && etObj is IAccessibleEditableText serviceEdit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: ServiceProvider->EditableText succeeded! Calling insertText...");
                    hr = serviceEdit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via SP on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                try { Marshal.ReleaseComObject(ia2Obj); } catch { }
            }
        }

        return false;
    }

    /// <summary>
    /// Write text to MSAA elements at point. Tries put_accValue on hit + ancestors (L0..L4),
    /// waits 5s so user can visually check if text appeared, then clears.
    /// </summary>
    public static void ProbeMsaaWrite(int absX, int absY, string text)
    {
        var pt = new Native.POINT(absX, absY);
        int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? _);
        if (hr != 0 || acc == null)
        {
            Console.WriteLine($"  [MSAA-WRITE] AccessibleObjectFromPoint failed hr=0x{hr:X8}");
            return;
        }
        try
        {
            object? current = acc;
            for (int level = 0; level <= 4 && current != null; level++)
            {
                if (current is not IAccessibleVtbl v) break;
                object? role = null; try { v.get_accRole(0, out role); } catch { }
                string? name = null; try { v.get_accName(0, out name); } catch { }
                int ri = role is int r ? r : -1;
                Console.Write($"    L{level}: role={ri}(0x{ri:X2}) name=\"{name ?? "(null)"}\" -> put_accValue...");
                try
                {
                    int pvHr = v.put_accValue(0, text);
                    Console.WriteLine($" hr=0x{pvHr:X8} {(pvHr == 0 ? "★ WRITTEN!" : "failed")}");
                    if (pvHr == 0)
                    {
                        Console.WriteLine($"  [MSAA-WRITE] Text written at L{level}! Waiting 5s for visual check...");
                        Thread.Sleep(5000);
                        string? readBack = null;
                        try { v.get_accValue(0, out readBack); } catch { }
                        Console.WriteLine($"  [MSAA-WRITE] Read back: \"{readBack ?? "(null)"}\"");
                        v.put_accValue(0, "");
                        Console.WriteLine($"  [MSAA-WRITE] Cleaned up.");
                        return;
                    }
                }
                catch (Exception ex) { Console.WriteLine($" {ex.GetType().Name}"); }
                try { v.get_accParent(out current); } catch { current = null; }
            }
            Console.WriteLine("  [MSAA-WRITE] No writable element found in L0..L4");
        }
        finally { try { Marshal.ReleaseComObject(acc); } catch { } }
    }

    /// <summary>
    /// Probe MSAA tree at absolute screen coordinates. Dumps role/name/children up to parent.
    /// Returns the MSAA role of the hit element, or -1 on failure.
    /// </summary>
    public static int ProbeMsaaAtPoint(int absX, int absY, int walkDepth = 6)
    {
        var pt = new Native.POINT(absX, absY);
        int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? childId);
        if (hr != 0 || acc == null)
        {
            Console.WriteLine($"  [MSAA-PROBE] AccessibleObjectFromPoint({absX},{absY}) failed hr=0x{hr:X8}");
            return -1;
        }

        var childIdStr = childId switch { int id => id == 0 ? "SELF" : $"child={id}", _ => $"type={childId?.GetType().Name}" };
        Console.WriteLine($"  [MSAA-PROBE] Hit at ({absX},{absY}) childId={childIdStr}");

        try
        {
            object? current = acc;
            for (int level = 0; level <= walkDepth && current != null; level++)
            {
                if (current is not IAccessibleVtbl v) { Console.WriteLine($"    L{level}: not IAccessibleVtbl"); break; }

                int childCount = 0; try { v.get_accChildCount(out childCount); } catch { }
                object? role = null; try { v.get_accRole(0, out role); } catch { }
                string? name = null; try { v.get_accName(0, out name); } catch { }
                string? value = null; try { v.get_accValue(0, out value); } catch { }
                string? defAction = null; try { v.get_accDefaultAction(0, out defAction); } catch { }

                int roleInt = role is int r ? r : -1;
                var nameStr = string.IsNullOrEmpty(name) ? "(null)" : (name.Length > 80 ? name[..77] + "..." : name);
                var valStr = string.IsNullOrEmpty(value) ? "" : $" val=\"{(value.Length > 40 ? value[..37] + "..." : value)}\"";
                var actStr = string.IsNullOrEmpty(defAction) ? "" : $" action=\"{defAction}\"";
                Console.WriteLine($"    L{level}: role={roleInt}(0x{roleInt:X2}) name=\"{nameStr}\" children={childCount}{valStr}{actStr}");

                if (level <= 2)
                {
                    try
                    {
                        int pvHr = v.put_accValue(0, "__PROBE_TEST__");
                        Console.WriteLine($"    L{level}: put_accValue -> hr=0x{pvHr:X8} {(pvHr == 0 ? "★ WRITABLE!" : "")}");
                        if (pvHr == 0)
                            v.put_accValue(0, "");
                    }
                    catch (Exception ex) { Console.WriteLine($"    L{level}: put_accValue -> {ex.GetType().Name}"); }
                }

                try { v.get_accParent(out current); } catch { current = null; }
            }

            if (acc is IAccessibleVtbl hitV)
            {
                hitV.get_accChildCount(out int cc);
                Console.WriteLine($"  [MSAA-PROBE] Children of hit ({cc}):");
                for (int i = 1; i <= Math.Min(cc, 15); i++)
                {
                    try
                    {
                        hitV.get_accChild(i, out object? child);
                        if (child is IAccessibleVtbl cv)
                        {
                            object? cr = null; try { cv.get_accRole(0, out cr); } catch { }
                            string? cn = null; try { cv.get_accName(0, out cn); } catch { }
                            int cri = cr is int crint ? crint : -1;
                            Console.WriteLine($"    child[{i}]: role={cri}(0x{cri:X2}) name=\"{cn ?? "(null)"}\"");
                        }
                    }
                    catch { }
                }
            }
            return acc is IAccessibleVtbl av ? (av.get_accRole(0, out object? ro) == 0 && ro is int ri ? ri : -1) : -1;
        }
        finally
        {
            try { Marshal.ReleaseComObject(acc); } catch { }
        }
    }
}
