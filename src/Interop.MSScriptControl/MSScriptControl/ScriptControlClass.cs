using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace MSScriptControl;

[ComImport]
[TypeLibType(34)]
[ClassInterface(0.0)]
[Guid("0E59F1D5-1FBE-11D0-8FF2-00A0D10038BC")]
[ComSourceInterfaces("MSScriptControl.DScriptControlSource")]
public class ScriptControlClass : IScriptControl, ScriptControl//, DScriptControlSource_Event
{
    //public virtual extern event DScriptControlSource_ErrorEventHandler DScriptControlSource_Event_Error;

    //public virtual extern event DScriptControlSource_TimeoutEventHandler DScriptControlSource_Event_Timeout;

    //public virtual extern event DScriptControlSource_ErrorEventHandler Error
    //{
    //    add
    //    {

    //    }

    //    remove
    //    {

    //    }
    //}

    //event DScriptControlSource_TimeoutEventHandler DScriptControlSource_Event.Timeout
    //{
    //    add
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    remove
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //   add_DScriptControlSource_Event_Error

    //public event DScriptControlSource_ErrorEventHandler Error;
    //   //public virtual event DScriptControlSource_ErrorEventHandler DScriptControlSource_Event.Error;

    //   public virtual event DScriptControlSource_TimeoutEventHandler Timeout
    //{
    //       add; remove;
    //}



    [DispId(1500)]
    public virtual extern string Language
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
    public virtual extern ScriptControlStates State
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1501)]
        [TypeLibFunc(1024)]
        get;
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1501)]
        [TypeLibFunc(1024)]
        [param: In]
        set;
    }

    [DispId(1502)]
    public virtual extern int SitehWnd
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
    public virtual extern int Timeout
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
    public virtual extern bool AllowUI
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
    public virtual extern bool UseSafeSubset
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
    public virtual extern Modules Modules
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [TypeLibFunc(1024)]
        [DispId(1506)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [DispId(1507)]
    public virtual extern Error Error
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1507)]
        [TypeLibFunc(1024)]
        [return: MarshalAs(UnmanagedType.Interface)]
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
    [TypeLibFunc(64)]
    [DispId(-552)]
    public virtual extern void _AboutBox();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2500)]
    public virtual extern void AddObject([In][MarshalAs(UnmanagedType.BStr)] string Name, [In][MarshalAs(UnmanagedType.IDispatch)] object Object, [In] bool AddMembers = false);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2501)]
    public virtual extern void Reset();

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
