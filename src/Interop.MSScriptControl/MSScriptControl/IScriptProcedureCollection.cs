using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[TypeLibType(4304)]
[Guid("70841C71-067D-11D0-95D8-00A02463AB28")]
public interface IScriptProcedureCollection : IEnumerable
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [TypeLibFunc(64)]
    [DispId(-4)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    new IEnumerator GetEnumerator();

    [DispId(0)]
    Procedure this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.Interface)]
        get;
    }

    [DispId(1)]
    int Count
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(1)]
        get;
    }
}
