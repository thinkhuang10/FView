using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnVariableLagEventHandler(int lID, int lDeviceID, [MarshalAs(UnmanagedType.Struct)] ref object pvarValue);
