using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[DefaultMember("Name")]
[TypeLibType(4304)]
[Guid("70841C70-067D-11D0-95D8-00A02463AB28")]
public interface IScriptModule
{
    [DispId(0)]
    string Name
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(1000)]
    object CodeObject
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1000)]
        [return: MarshalAs(UnmanagedType.IDispatch)]
        get;
    }

    [DispId(1001)]
    Procedures Procedures
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1001)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2000)]
    void AddCode([In][MarshalAs(UnmanagedType.BStr)] string Code);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2001)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object Eval([In][MarshalAs(UnmanagedType.BStr)] string Expression);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2002)]
    void ExecuteStatement([In][MarshalAs(UnmanagedType.BStr)] string Statement);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2003)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object Run([In][MarshalAs(UnmanagedType.BStr)] string ProcedureName, [In][MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] ref object[] Parameters);
}
