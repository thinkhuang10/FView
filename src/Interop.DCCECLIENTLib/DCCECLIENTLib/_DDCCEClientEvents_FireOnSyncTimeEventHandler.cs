using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnSyncTimeEventHandler(int lYear, int lMonth, int lDay, int lHour, int lMinute, int lSecond, int lMilliSecond);
