using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("0E59F1DA-1FBE-11D0-8FF2-00A0D10038BC")]
[ClassInterface(0.0)]
[DefaultMember("Name")]
public class ProcedureClass : IScriptProcedure, Procedure
{
    [DispId(0)]
    public virtual extern string Name
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(0)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(100)]
    public virtual extern int NumArgs
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(100)]
        get;
    }

    [DispId(101)]
    public virtual extern bool HasReturnValue
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(101)]
        get;
    }
}
