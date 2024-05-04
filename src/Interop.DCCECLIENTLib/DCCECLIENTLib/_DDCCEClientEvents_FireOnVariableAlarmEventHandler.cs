using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnVariableAlarmEventHandler(int lAlarmType, int lID, [MarshalAs(UnmanagedType.Struct)] ref object pvarValue);
