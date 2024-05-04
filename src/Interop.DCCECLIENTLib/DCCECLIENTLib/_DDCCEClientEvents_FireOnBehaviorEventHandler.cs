using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnBehaviorEventHandler(int lBehaviorID, int lDeviceID, [MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);
