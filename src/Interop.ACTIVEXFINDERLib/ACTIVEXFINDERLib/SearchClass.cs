using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ACTIVEXFINDERLib;

[ComImport]
[TypeLibType(2)]
[Guid("7E460AAE-E267-41A8-8ED6-4C403E4249CD")]
[ClassInterface(0.0)]
public class SearchClass : ISearch, Search
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void GetComponentInfo();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void Advise([In][MarshalAs(UnmanagedType.Interface)] ICallBack pCallBack);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void Unadvise();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void APILoadLibrary(out int hModule, [In][MarshalAs(UnmanagedType.BStr)] string LpFileName);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public virtual extern void APILoadBitmap(out int hBitmap, [In] int hInstance, int ResourceID);
}
