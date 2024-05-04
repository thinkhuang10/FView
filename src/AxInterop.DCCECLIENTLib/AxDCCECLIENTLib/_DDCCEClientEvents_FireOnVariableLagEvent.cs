namespace AxDCCECLIENTLib;

public class _DDCCEClientEvents_FireOnVariableLagEvent
{
    public int lID;

    public int lDeviceID;

    public object pvarValue;

    public _DDCCEClientEvents_FireOnVariableLagEvent(int lID, int lDeviceID, object pvarValue)
    {
        this.lID = lID;
        this.lDeviceID = lDeviceID;
        this.pvarValue = pvarValue;
    }
}
