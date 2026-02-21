using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.IO;

class Program
{
    [DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
    static extern int LoadTypeLib(string szFile, out ITypeLib ppTLib);

    static readonly string[] VtNames = {
        "void","null","short","int","float","double","CY","DateTime","string","IDispatch*",
        "SCODE","bool","VARIANT","IUnknown*","decimal","?15","sbyte","byte","ushort","uint",
        "long","ulong","int","uint","void","HRESULT","IntPtr","SAFEARRAY","CARRAY","USERDEFINED",
        "LPSTR","LPWSTR"
    };

    static string VtToString(ITypeInfo? context, TYPEDESC td)
    {
        short vt = td.vt;
        bool isPtr = (vt & 0x4000) != 0;   // VT_BYREF
        bool isArray = (vt & 0x2000) != 0;  // VT_ARRAY
        short baseVt = (short)(vt & 0x0FFF);

        if (baseVt == 26 && td.lpValue != IntPtr.Zero) // VT_PTR
        {
            var inner = Marshal.PtrToStructure<TYPEDESC>(td.lpValue);
            return VtToString(context, inner) + "*";
        }

        if (baseVt == 29 && context != null) // VT_USERDEFINED
        {
            try
            {
                int hRef = (int)td.lpValue;
                context.GetRefTypeInfo(hRef, out ITypeInfo refInfo);
                refInfo.GetDocumentation(-1, out string refName, out _, out _, out _);
                return refName ?? "USERDEFINED";
            }
            catch { return "USERDEFINED"; }
        }

        string name = baseVt < VtNames.Length ? VtNames[baseVt] : $"VT_{baseVt}";
        if (isArray) name += "[]";
        if (isPtr) name += "&";
        return name;
    }

    static void Main(string[] args)
    {
        string ocxPath = args.Length > 0 ? args[0] : @"C:\OpenAPI\KHOpenAPI.ocx";
        string outPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(ocxPath)!, "khopenapi_typelib.md");

        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine($"COM TypeLib Inspector (x{(IntPtr.Size * 8)})");
        Console.WriteLine($"Loading: {ocxPath}");

        int hr = LoadTypeLib(ocxPath, out ITypeLib typeLib);
        if (hr != 0)
        {
            Console.WriteLine($"FAILED: 0x{hr:X8}");
            return;
        }

        typeLib.GetDocumentation(-1, out string libName, out string libDoc, out _, out _);
        int count = typeLib.GetTypeInfoCount();

        var sb = new StringBuilder();
        sb.AppendLine($"# {libName} — COM TypeLib");
        sb.AppendLine($"Source: `{ocxPath}`");
        sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"TypeInfos: {count}");
        sb.AppendLine();

        Console.WriteLine($"Library: {libName} ({count} types)");
        Console.WriteLine(new string('=', 60));

        string[] kindNames = { "ENUM", "RECORD", "MODULE", "INTERFACE", "DISPATCH", "COCLASS", "ALIAS", "UNION" };

        for (int i = 0; i < count; i++)
        {
            typeLib.GetTypeInfo(i, out ITypeInfo typeInfo);
            typeLib.GetDocumentation(i, out string name, out string doc, out _, out _);
            typeLib.GetTypeInfoType(i, out TYPEKIND kind);

            string kindStr = (int)kind < kindNames.Length ? kindNames[(int)kind] : $"?{(int)kind}";

            Console.WriteLine($"\n[{i}] {name} ({kindStr})");
            sb.AppendLine($"## {name} ({kindStr})");
            if (!string.IsNullOrEmpty(doc)) sb.AppendLine($"*{doc}*");

            typeInfo.GetTypeAttr(out IntPtr pAttr);
            var attr = Marshal.PtrToStructure<TYPEATTR>(pAttr);

            sb.AppendLine($"GUID: `{attr.guid}`");

            // --- Functions ---
            if (attr.cFuncs > 0)
            {
                sb.AppendLine($"\n### Functions ({attr.cFuncs})");
                sb.AppendLine("```");

                for (int f = 0; f < attr.cFuncs; f++)
                {
                    typeInfo.GetFuncDesc(f, out IntPtr pFd);
                    var fd = Marshal.PtrToStructure<FUNCDESC>(pFd);

                    string[] names = new string[fd.cParams + 2];
                    typeInfo.GetNames(fd.memid, names, fd.cParams + 2, out int nc);

                    string funcName = names[0] ?? $"_{f}";
                    string invStr = fd.invkind switch
                    {
                        INVOKEKIND.INVOKE_FUNC => "",
                        INVOKEKIND.INVOKE_PROPERTYGET => "[get] ",
                        INVOKEKIND.INVOKE_PROPERTYPUT => "[put] ",
                        INVOKEKIND.INVOKE_PROPERTYPUTREF => "[putref] ",
                        _ => $"[{fd.invkind}] "
                    };

                    string retType = VtToString(typeInfo, fd.elemdescFunc.tdesc);

                    // Params
                    var pList = new StringBuilder();
                    if (fd.cParams > 0 && fd.lprgelemdescParam != IntPtr.Zero)
                    {
                        int sz = Marshal.SizeOf<ELEMDESC>();
                        for (int p = 0; p < fd.cParams; p++)
                        {
                            if (p > 0) pList.Append(", ");
                            var elem = Marshal.PtrToStructure<ELEMDESC>(fd.lprgelemdescParam + p * sz);
                            string pType = VtToString(typeInfo, elem.tdesc);
                            string pName = (p + 1 < nc) ? names[p + 1] : $"p{p}";
                            pList.Append($"{pType} {pName}");
                        }
                    }

                    string sig = $"  {invStr}{retType} {funcName}({pList})";
                    Console.WriteLine(sig);
                    sb.AppendLine(sig);

                    typeInfo.ReleaseFuncDesc(pFd);
                }
                sb.AppendLine("```");
            }

            // --- Variables ---
            if (attr.cVars > 0)
            {
                sb.AppendLine($"\n### Variables ({attr.cVars})");
                sb.AppendLine("```");
                for (int v = 0; v < attr.cVars; v++)
                {
                    typeInfo.GetVarDesc(v, out IntPtr pVd);
                    var vd = Marshal.PtrToStructure<VARDESC>(pVd);
                    string[] vNames = new string[1];
                    typeInfo.GetNames(vd.memid, vNames, 1, out _);
                    string vName = vNames[0] ?? $"var_{v}";
                    string line = $"  {vName} = 0x{vd.memid:X}";
                    Console.WriteLine(line);
                    sb.AppendLine(line);
                    typeInfo.ReleaseVarDesc(pVd);
                }
                sb.AppendLine("```");
            }

            // --- Implemented interfaces (COCLASS) ---
            if (kind == TYPEKIND.TKIND_COCLASS && attr.cImplTypes > 0)
            {
                sb.AppendLine($"\n### Implements ({attr.cImplTypes})");
                for (int impl = 0; impl < attr.cImplTypes; impl++)
                {
                    typeInfo.GetRefTypeOfImplType(impl, out int refType);
                    typeInfo.GetRefTypeInfo(refType, out ITypeInfo refInfo);
                    refInfo.GetDocumentation(-1, out string refName, out _, out _, out _);
                    typeInfo.GetImplTypeFlags(impl, out IMPLTYPEFLAGS flags);

                    string f = "";
                    if (flags.HasFlag(IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT)) f += "[default] ";
                    if (flags.HasFlag(IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE)) f += "[source/event] ";

                    Console.WriteLine($"  {f}{refName}");
                    sb.AppendLine($"- {f}`{refName}`");
                }
            }

            typeInfo.ReleaseTypeAttr(pAttr);
        }

        File.WriteAllText(outPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"\n{'='}\nSaved: {outPath}");
    }
}
