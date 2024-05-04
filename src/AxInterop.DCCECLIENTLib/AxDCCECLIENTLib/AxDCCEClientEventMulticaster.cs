using System.Runtime.InteropServices;
using DCCECLIENTLib;

namespace AxDCCECLIENTLib;

[ClassInterface(ClassInterfaceType.None)]
public class AxDCCEClientEventMulticaster : _DDCCEClientEvents
{
    private readonly AxDCCEClient parent;

    public AxDCCEClientEventMulticaster(AxDCCEClient parent)
    {
        this.parent = parent;
    }

    public virtual void FireOnDataReady(int lShakeInterval)
    {
        _DDCCEClientEvents_FireOnDataReadyEvent e = new(lShakeInterval);
        parent.RaiseOnFireOnDataReady(parent, e);
    }

    public virtual void FireOnVariableAlarm(int lAlarmType, int lID, ref object pvarValue)
    {
        _DDCCEClientEvents_FireOnVariableAlarmEvent dDCCEClientEvents_FireOnVariableAlarmEvent = new(lAlarmType, lID, pvarValue);
        parent.RaiseOnFireOnVariableAlarm(parent, dDCCEClientEvents_FireOnVariableAlarmEvent);
        pvarValue = dDCCEClientEvents_FireOnVariableAlarmEvent.pvarValue;
    }

    public virtual void FireOnVariableLag(int lID, int lDeviceID, ref object pvarValue)
    {
        _DDCCEClientEvents_FireOnVariableLagEvent dDCCEClientEvents_FireOnVariableLagEvent = new(lID, lDeviceID, pvarValue);
        parent.RaiseOnFireOnVariableLag(parent, dDCCEClientEvents_FireOnVariableLagEvent);
        pvarValue = dDCCEClientEvents_FireOnVariableLagEvent.pvarValue;
    }

    public virtual void FireOnLoadOver(int lLoadType, int lLoadSize)
    {
        _DDCCEClientEvents_FireOnLoadOverEvent e = new(lLoadType, lLoadSize);
        parent.RaiseOnFireOnLoadOver(parent, e);
    }

    public virtual void FireOnDeviceStatus(int lDeviceID, int lDeviceInterface, int lDeviceStatus)
    {
        _DDCCEClientEvents_FireOnDeviceStatusEvent e = new(lDeviceID, lDeviceInterface, lDeviceStatus);
        parent.RaiseOnFireOnDeviceStatus(parent, e);
    }

    public virtual void FireOnBehavior(int lBehaviorID, int lDeviceID, object varValue, int lResult)
    {
        _DDCCEClientEvents_FireOnBehaviorEvent e = new(lBehaviorID, lDeviceID, varValue, lResult);
        parent.RaiseOnFireOnBehavior(parent, e);
    }

    public virtual void FireOnScan(object varValue, int lResult)
    {
        _DDCCEClientEvents_FireOnScanEvent e = new(varValue, lResult);
        parent.RaiseOnFireOnScan(parent, e);
    }

    public virtual void FireOnScanBehavior(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, object varValue, int lResult)
    {
        _DDCCEClientEvents_FireOnScanBehaviorEvent dDCCEClientEvents_FireOnScanBehaviorEvent = new(lDevType, lInfType, lLevel, lBevID, pAddr, varValue, lResult);
        parent.RaiseOnFireOnScanBehavior(parent, dDCCEClientEvents_FireOnScanBehaviorEvent);
        pAddr = dDCCEClientEvents_FireOnScanBehaviorEvent.pAddr;
    }

    public virtual void FireOnSyncTime(int lYear, int lMonth, int lDay, int lHour, int lMinute, int lSecond, int lMilliSecond)
    {
        _DDCCEClientEvents_FireOnSyncTimeEvent e = new(lYear, lMonth, lDay, lHour, lMinute, lSecond, lMilliSecond);
        parent.RaiseOnFireOnSyncTime(parent, e);
    }
}
