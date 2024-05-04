namespace AxDCCECLIENTLib;

public class _DDCCEClientEvents_FireOnBehaviorEvent
{
    public int lBehaviorID;

    public int lDeviceID;

    public object varValue;

    public int lResult;

    public _DDCCEClientEvents_FireOnBehaviorEvent(int lBehaviorID, int lDeviceID, object varValue, int lResult)
    {
        this.lBehaviorID = lBehaviorID;
        this.lDeviceID = lDeviceID;
        this.varValue = varValue;
        this.lResult = lResult;
    }
}
