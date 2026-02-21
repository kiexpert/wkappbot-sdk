// COM TypeLib Inspector — .NET Framework 4 / C# 5
// Compile: csc /platform:x86 ComInspect.cs
using System;
using System.Runtime.InteropServices;
using COMTypes = System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.IO;

class ComInspect
{
    [DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
    static extern int LoadTypeLib(string szFile, out COMTypes.ITypeLib ppTLib);

    static string VtToStr(COMTypes.ITypeInfo ctx, COMTypes.TYPEDESC td)
    {
        short vt = td.vt;
        short b = (short)(vt & 0xFFF);
        if (b == 26 && td.lpValue != IntPtr.Zero) // VT_PTR
        {
            var inner = (COMTypes.TYPEDESC)Marshal.PtrToStructure(td.lpValue, typeof(COMTypes.TYPEDESC));
            return VtToStr(ctx, inner) + "*";
        }
        if (b == 29 && ctx != null) // VT_USERDEFINED
        {
            try
            {
                int hr2 = (int)td.lpValue;
                COMTypes.ITypeInfo ri;
                ctx.GetRefTypeInfo(hr2, out ri);
                string rn, rd; int hc; string hf;
                ri.GetDocumentation(-1, out rn, out rd, out hc, out hf);
                return rn ?? "?";
            }
            catch { return "USERDEF"; }
        }
        string[] names = {"void","null","short","int","float","double","CY","DateTime",
            "string","IDispatch*","SCODE","bool","VARIANT","IUnknown*","decimal","?",
            "sbyte","byte","ushort","uint","long","ulong","int","uint","void","HRESULT",
            "IntPtr","SAFEARRAY","CARRAY","USERDEF","LPSTR","LPWSTR"};
        string n = b < names.Length ? names[b] : "VT_" + b;
        if ((vt & 0x2000) != 0) n += "[]";
        if ((vt & 0x4000) != 0) n += "&";
        return n;
    }

    static void Main(string[] args)
    {
        string ocx = args.Length > 0 ? args[0] : @"C:\OpenAPI\KHOpenAPI.ocx";
        string outFile = args.Length > 1 ? args[1] : @"C:\OpenAPI\khopenapi_typelib.md";

        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("COM TypeLib Inspector (x" + (IntPtr.Size * 8) + ")");
        Console.WriteLine("Loading: " + ocx);

        COMTypes.ITypeLib tl;
        int hr = LoadTypeLib(ocx, out tl);
        if (hr != 0) { Console.WriteLine("FAIL: 0x" + hr.ToString("X")); return; }

        string ln, ld; int lhc; string lhf;
        tl.GetDocumentation(-1, out ln, out ld, out lhc, out lhf);
        int cnt = tl.GetTypeInfoCount();

        var sb = new StringBuilder();
        sb.AppendLine("# " + ln + " — COM TypeLib");
        sb.AppendLine("Source: " + ocx);
        sb.AppendLine("Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("Types: " + cnt);
        sb.AppendLine();

        Console.WriteLine("Library: " + ln + " (" + cnt + " types)");
        Console.WriteLine(new string('=', 60));

        string[] kinds = {"ENUM","RECORD","MODULE","INTERFACE","DISPATCH","COCLASS","ALIAS","UNION"};

        for (int i = 0; i < cnt; i++)
        {
            COMTypes.ITypeInfo ti;
            tl.GetTypeInfo(i, out ti);
            string nm, dc; int hcc; string hff;
            tl.GetDocumentation(i, out nm, out dc, out hcc, out hff);
            COMTypes.TYPEKIND kk;
            tl.GetTypeInfoType(i, out kk);
            string ks = (int)kk < kinds.Length ? kinds[(int)kk] : "?" + (int)kk;

            Console.WriteLine();
            Console.WriteLine("[" + i + "] " + nm + " (" + ks + ")");
            sb.AppendLine("## " + nm + " (" + ks + ")");
            if (!string.IsNullOrEmpty(dc)) sb.AppendLine("*" + dc + "*");

            IntPtr pa;
            ti.GetTypeAttr(out pa);
            var attr = (COMTypes.TYPEATTR)Marshal.PtrToStructure(pa, typeof(COMTypes.TYPEATTR));
            sb.AppendLine("GUID: " + attr.guid);

            // Functions
            if (attr.cFuncs > 0)
            {
                sb.AppendLine();
                sb.AppendLine("### Functions (" + attr.cFuncs + ")");
                sb.AppendLine("```");
                for (int f = 0; f < attr.cFuncs; f++)
                {
                    IntPtr pfd;
                    ti.GetFuncDesc(f, out pfd);
                    var fd = (COMTypes.FUNCDESC)Marshal.PtrToStructure(pfd, typeof(COMTypes.FUNCDESC));

                    string[] ns = new string[fd.cParams + 2];
                    int nc;
                    ti.GetNames(fd.memid, ns, fd.cParams + 2, out nc);
                    string fn = ns[0] ?? "_" + f;

                    string inv = "";
                    if (fd.invkind == COMTypes.INVOKEKIND.INVOKE_PROPERTYGET) inv = "[get] ";
                    else if (fd.invkind == COMTypes.INVOKEKIND.INVOKE_PROPERTYPUT) inv = "[put] ";
                    else if (fd.invkind == COMTypes.INVOKEKIND.INVOKE_PROPERTYPUTREF) inv = "[putref] ";

                    string ret = VtToStr(ti, fd.elemdescFunc.tdesc);

                    var pl = new StringBuilder();
                    if (fd.cParams > 0 && fd.lprgelemdescParam != IntPtr.Zero)
                    {
                        int sz = Marshal.SizeOf(typeof(COMTypes.ELEMDESC));
                        for (int p = 0; p < fd.cParams; p++)
                        {
                            if (p > 0) pl.Append(", ");
                            var el = (COMTypes.ELEMDESC)Marshal.PtrToStructure(
                                new IntPtr(fd.lprgelemdescParam.ToInt64() + p * sz),
                                typeof(COMTypes.ELEMDESC));
                            string pt = VtToStr(ti, el.tdesc);
                            string pn = (p + 1 < nc) ? ns[p + 1] : "p" + p;
                            pl.Append(pt + " " + pn);
                        }
                    }

                    string sig = "  " + inv + ret + " " + fn + "(" + pl + ")";
                    Console.WriteLine(sig);
                    sb.AppendLine(sig);
                    ti.ReleaseFuncDesc(pfd);
                }
                sb.AppendLine("```");
            }

            // Vars
            if (attr.cVars > 0)
            {
                sb.AppendLine();
                sb.AppendLine("### Variables (" + attr.cVars + ")");
                sb.AppendLine("```");
                for (int v = 0; v < attr.cVars; v++)
                {
                    IntPtr pvd;
                    ti.GetVarDesc(v, out pvd);
                    var vd = (COMTypes.VARDESC)Marshal.PtrToStructure(pvd, typeof(COMTypes.VARDESC));
                    string[] vns = new string[1];
                    int vnc;
                    ti.GetNames(vd.memid, vns, 1, out vnc);
                    string vn = vns[0] ?? "v" + v;
                    string line = "  " + vn + " = 0x" + vd.memid.ToString("X");
                    Console.WriteLine(line);
                    sb.AppendLine(line);
                    ti.ReleaseVarDesc(pvd);
                }
                sb.AppendLine("```");
            }

            // Impl interfaces (COCLASS)
            if (kk == COMTypes.TYPEKIND.TKIND_COCLASS && attr.cImplTypes > 0)
            {
                sb.AppendLine();
                sb.AppendLine("### Implements (" + attr.cImplTypes + ")");
                for (int ii = 0; ii < attr.cImplTypes; ii++)
                {
                    int rt;
                    ti.GetRefTypeOfImplType(ii, out rt);
                    COMTypes.ITypeInfo ri;
                    ti.GetRefTypeInfo(rt, out ri);
                    string rn2, rd2; int hc2; string hf2;
                    ri.GetDocumentation(-1, out rn2, out rd2, out hc2, out hf2);
                    COMTypes.IMPLTYPEFLAGS fl;
                    ti.GetImplTypeFlags(ii, out fl);
                    string fs = "";
                    if ((fl & COMTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT) != 0) fs += "[default] ";
                    if ((fl & COMTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE) != 0) fs += "[source/event] ";
                    Console.WriteLine("  " + fs + rn2);
                    sb.AppendLine("- " + fs + "`" + rn2 + "`");
                }
            }

            ti.ReleaseTypeAttr(pa);
        }

        File.WriteAllText(outFile, sb.ToString(), Encoding.UTF8);
        Console.WriteLine();
        Console.WriteLine("Saved: " + outFile);
    }
}
