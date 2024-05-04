using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComEventInterface(typeof(_DDCCEClientEvents), typeof(_DDCCEClientEvents_EventProvider))]
[TypeLibType(16)]
[ComVisible(false)]
public interface _DDCCEClientEvents_Event
{
    event _DDCCEClientEvents_FireOnDataReadyEventHandler FireOnDataReady;

    event _DDCCEClientEvents_FireOnVariableAlarmEventHandler FireOnVariableAlarm;

    event _DDCCEClientEvents_FireOnVariableLagEventHandler FireOnVariableLag;

    event _DDCCEClientEvents_FireOnLoadOverEventHandler FireOnLoadOver;

    event _DDCCEClientEvents_FireOnDeviceStatusEventHandler FireOnDeviceStatus;

    event _DDCCEClientEvents_FireOnBehaviorEventHandler FireOnBehavior;

    event _DDCCEClientEvents_FireOnScanEventHandler FireOnScan;

    event _DDCCEClientEvents_FireOnScanBehaviorEventHandler FireOnScanBehavior;

    event _DDCCEClientEvents_FireOnSyncTimeEventHandler FireOnSyncTime;
}
