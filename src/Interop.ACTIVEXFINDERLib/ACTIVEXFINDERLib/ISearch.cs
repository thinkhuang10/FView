using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ACTIVEXFINDERLib;

[ComImport]
[Guid("9479487B-491D-4DC4-A639-5A1C75DF8468")]
[InterfaceType(1)]
public interface ISearch
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetComponentInfo();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Advise([In][MarshalAs(UnmanagedType.Interface)] ICallBack pCallBack);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Unadvise();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void APILoadLibrary(out int hModule, [In][MarshalAs(UnmanagedType.BStr)] string LpFileName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void APILoadBitmap(out int hBitmap, [In] int hInstance, int ResourceID);
}
