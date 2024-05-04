using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("70841C73-067D-11D0-95D8-00A02463AB28")]
[DefaultMember("Name")]
[TypeLibType(4304)]
public interface IScriptProcedure
{
    [DispId(0)]
    string Name
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(100)]
    int NumArgs
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(100)]
        get;
    }

    [DispId(101)]
    bool HasReturnValue
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(101)]
        get;
    }
}
