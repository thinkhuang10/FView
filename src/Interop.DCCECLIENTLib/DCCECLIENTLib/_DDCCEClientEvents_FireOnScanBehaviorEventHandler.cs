using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComVisible(false)]
public delegate void _DDCCEClientEvents_FireOnScanBehaviorEventHandler(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, [MarshalAs(UnmanagedType.Struct)] object varValue, int lResult);
