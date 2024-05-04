using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[ClassInterface(0.0)]
[Guid("0E59F1DB-1FBE-11D0-8FF2-00A0D10038BC")]
public class ProceduresClass : IScriptProcedureCollection, Procedures, IEnumerable
{
    [DispId(0)]
    public virtual extern Procedure this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [DispId(1)]
    public virtual extern int Count
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1)]
        get;
    }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(-4)]
    [TypeLibFunc(64)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public virtual extern IEnumerator GetEnumerator();
}
