using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComImport]
[InterfaceType(2)]
[Guid("B9747318-F601-466D-9B18-8B353A103176")]
[TypeLibType(4096)]
public interface _DDCCEClientEvents
{
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(1)]
    void FireOnDataReady(int lShakeInterval);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2)]
    void FireOnVariableAlarm(int lAlarmType, int lID, [MarshalAs(UnmanagedType.Struct)] ref object pvarValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(3)]
    void FireOnVariableLag(int lID, int lDeviceID, [MarshalAs(UnmanagedType.Struct)] ref object pvarValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(4)]
    void FireOnLoadOver(int lLoadType, int lLoadSize);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(5)]
    void FireOnDeviceStatus(int lDeviceID, int lDeviceInterface, int lDeviceStatus);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(6)]
    void FireOnBehavior(int lBehaviorID, int lDeviceID, [MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(7)]
    void FireOnScan([MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(8)]
    void FireOnScanBehavior(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, [MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(9)]
    void FireOnSyncTime(int lYear, int lMonth, int lDay, int lHour, int lMinute, int lSecond, int lMilliSecond);
}
