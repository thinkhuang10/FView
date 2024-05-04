using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnScanEventHandler([MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);
