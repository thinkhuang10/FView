using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[DefaultMember("Name")]
[Guid("0E59F1DC-1FBE-11D0-8FF2-00A0D10038BC")]
[ClassInterface(0.0)]
public class ModuleClass : IScriptModule, Module
{
    [DispId(0)]
    public virtual extern string Name
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(1000)]
    public virtual extern object CodeObject
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1000)]
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
    }

    [DispId(1001)]
    public virtual extern Procedures Procedures
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1001)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2000)]
    public virtual extern void AddCode([In][MarshalAs(UnmanagedType.BStr)] string Code);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2001)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern object Eval([In][MarshalAs(UnmanagedType.BStr)] string Expression);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2002)]
    public virtual extern void ExecuteStatement([In][MarshalAs(UnmanagedType.BStr)] string Statement);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2003)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern object Run([In][MarshalAs(UnmanagedType.BStr)] string ProcedureName, [In][MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] ref object[] Parameters);
}
