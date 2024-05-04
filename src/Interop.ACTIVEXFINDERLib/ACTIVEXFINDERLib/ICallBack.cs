using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ACTIVEXFINDERLib;

[ComImport]
[Guid("7E659BB1-FB79-4188-9661-65CA22B6A3E6")]
[InterfaceType(1)]
public interface ICallBack
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Result([In][MarshalAs(UnmanagedType.BStr)] string strControlName, [In][MarshalAs(UnmanagedType.BStr)] string strCLSID, [In][MarshalAs(UnmanagedType.BStr)] string strPath, [In][MarshalAs(UnmanagedType.BStr)] string strBitmapPath, [In][MarshalAs(UnmanagedType.BStr)] string strVersion);
}
