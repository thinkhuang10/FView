namespace AxDCCECLIENTLib;

public class _DDCCEClientEvents_FireOnVariableAlarmEvent
{
    public int lAlarmType;

    public int lID;

    public object pvarValue;

    public _DDCCEClientEvents_FireOnVariableAlarmEvent(int lAlarmType, int lID, object pvarValue)
    {
        this.lAlarmType = lAlarmType;
        this.lID = lID;
        this.pvarValue = pvarValue;
    }
}
