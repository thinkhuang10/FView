using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[TypeLibType(4304)]
[Guid("70841C78-067D-11D0-95D8-00A02463AB28")]
public interface IScriptError
{
    [DispId(201)]
    int Number
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(201)]
        get;
    }

    [DispId(202)]
    string Source
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(202)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(203)]
    string Description
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(203)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(204)]
    string HelpFile
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(204)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(205)]
    int HelpContext
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(205)]
        get;
    }

    [DispId(-517)]
    string Text
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(-517)]
        [return: MarshalAs(UnmanagedType.BStr)]
        get;
    }

    [DispId(206)]
    int Line
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(206)]
        get;
    }

    [DispId(-529)]
    int Column
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [DispId(-529)]
        get;
    }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(208)]
    void Clear();
}
