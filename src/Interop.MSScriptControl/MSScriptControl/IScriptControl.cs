using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("0E59F1D3-1FBE-11D0-8FF2-00A0D10038BC")]
[TypeLibType(4304)]
public interface IScriptControl
{
    [DispId(1500)]
    string Language
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1500)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1500)]
        [param: In]
        [param: MarshalAs(UnmanagedType.BStr)]
        set;
    }

    [DispId(1501)]
    ScriptControlStates State
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [TypeLibFunc(1024)]
        [DispId(1501)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1501)]
        [TypeLibFunc(1024)]
        [param: In]
        set;
    }

    [DispId(1502)]
    int SitehWnd
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [TypeLibFunc(1024)]
        [DispId(1502)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1502)]
        [TypeLibFunc(1024)]
        [param: In]
        set;
    }

    [DispId(1503)]
    public int Timeout
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1503)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1503)]
        [param: In]
        set;
    }

    [DispId(1504)]
    bool AllowUI
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1504)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1504)]
        [param: In]
        set;
    }

    [DispId(1505)]
    bool UseSafeSubset
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1505)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1505)]
        [param: In]
        set;
    }

    [DispId(1506)]
    Modules Modules
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1506)]
        [TypeLibFunc(1024)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [DispId(1507)]
    Error Error
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1507)]
        [TypeLibFunc(1024)]
        [return: MarshalAs(UnmanagedType.Interface)]
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
    [DispId(-552)]
    [TypeLibFunc(64)]
    void _AboutBox();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2500)]
    void AddObject([In][MarshalAs(UnmanagedType.BStr)] string Name, [In][MarshalAs(UnmanagedType.IDispatch)] object Object, [In] bool AddMembers = false);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2501)]
    void Reset();

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
