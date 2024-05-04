using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnDeviceStatusEventHandler(int lDeviceID, int lDeviceInterface, int lDeviceStatus);
