using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[ClassInterface(0.0)]
[Guid("0E59F1DE-1FBE-11D0-8FF2-00A0D10038BC")]
public class ErrorClass : IScriptError, Error
{
    [DispId(201)]
    public virtual extern int Number
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(201)]
        get;
    }

    [DispId(202)]
    public virtual extern string Source
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(202)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(203)]
    public virtual extern string Description
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(203)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(204)]
    public virtual extern string HelpFile
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(204)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(205)]
    public virtual extern int HelpContext
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(205)]
        get;
    }

    [DispId(-517)]
    public virtual extern string Text
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(-517)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(206)]
    public virtual extern int Line
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(206)]
        get;
    }

    [DispId(-529)]
    public virtual extern int Column
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(-529)]
        get;
    }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(208)]
    public virtual extern void Clear();
}
