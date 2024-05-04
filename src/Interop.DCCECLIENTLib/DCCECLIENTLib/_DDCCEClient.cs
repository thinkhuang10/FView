using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComImport]
[TypeLibType(4112)]
[Guid("6B1F0316-3D41-4406-A19A-203E5558999A")]
[InterfaceType(2)]
public interface _DDCCEClient
{
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(1)]
    int DataInit(int lShakeInterval);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object Read(int lID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(3)]
    int Write(int lID, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(4)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object ReadEx(int lStartID, int lEndID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(5)]
    int WriteEx(int lStartID, int lWriteCount, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(6)]
    [return: MarshalAs(UnmanagedType.Struct)]
    object Execute(int lMethordType, [MarshalAs(UnmanagedType.Struct)] object varValue, int lDeviceID, int lStartID, int lRWCount);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(7)]
    int ExecBehavior(int lBehaviorID, [MarshalAs(UnmanagedType.Struct)] object varValue, int lDeviceID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(8)]
    int ExecScanBehavior(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(9)]
    int SyncTime();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(10)]
    int ChangeDeviceCommAddr(int lDeviceID, int lDeviceAddrNew);
}
