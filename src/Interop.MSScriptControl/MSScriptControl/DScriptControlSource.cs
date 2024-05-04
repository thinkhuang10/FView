using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[TypeLibType(4112)]
[InterfaceType(2)]
[Guid("8B167D60-8605-11D0-ABCB-00A0C90FFFC0")]
public interface DScriptControlSource
{
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(3000)]
    void Error();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(3001)]
    void Timeout();
}
