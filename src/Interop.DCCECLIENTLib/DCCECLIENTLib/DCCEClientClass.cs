using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComImport]
[ComSourceInterfaces("DCCECLIENTLib._DDCCEClientEvents\0\0")]
[TypeLibType(34)]
[ClassInterface(0.0)]
[Guid("6301125A-4B83-402E-9361-4829C6591B31")]
public class DCCEClientClass : _DDCCEClient, DCCEClient, _DDCCEClientEvents_Event
{
    public virtual extern event _DDCCEClientEvents_FireOnDataReadyEventHandler FireOnDataReady;

    public virtual extern event _DDCCEClientEvents_FireOnVariableAlarmEventHandler FireOnVariableAlarm;

    public virtual extern event _DDCCEClientEvents_FireOnVariableLagEventHandler FireOnVariableLag;

    public virtual extern event _DDCCEClientEvents_FireOnLoadOverEventHandler FireOnLoadOver;

    public virtual extern event _DDCCEClientEvents_FireOnDeviceStatusEventHandler FireOnDeviceStatus;

    public virtual extern event _DDCCEClientEvents_FireOnBehaviorEventHandler FireOnBehavior;

    public virtual extern event _DDCCEClientEvents_FireOnScanEventHandler FireOnScan;

    public virtual extern event _DDCCEClientEvents_FireOnScanBehaviorEventHandler FireOnScanBehavior;

    public virtual extern event _DDCCEClientEvents_FireOnSyncTimeEventHandler FireOnSyncTime;

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(1)]
    public virtual extern int DataInit(int lShakeInterval);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(2)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern object Read(int lID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(3)]
    public virtual extern int Write(int lID, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(4)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern object ReadEx(int lStartID, int lEndID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(5)]
    public virtual extern int WriteEx(int lStartID, int lWriteCount, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(6)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public virtual extern object Execute(int lMethordType, [MarshalAs(UnmanagedType.Struct)] object varValue, int lDeviceID, int lStartID, int lRWCount);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(7)]
    public virtual extern int ExecBehavior(int lBehaviorID, [MarshalAs(UnmanagedType.Struct)] object varValue, int lDeviceID);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(8)]
    public virtual extern int ExecScanBehavior(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, [MarshalAs(UnmanagedType.Struct)] object varValue);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(9)]
    public virtual extern int SyncTime();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [DispId(10)]
    public virtual extern int ChangeDeviceCommAddr(int lDeviceID, int lDeviceAddrNew);
}
