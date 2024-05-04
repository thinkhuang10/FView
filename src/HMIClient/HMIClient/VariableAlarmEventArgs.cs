using System;

namespace HMIClient;

public class VariableAlarmEventArgs : EventArgs
{
    public int lAlarmType;

    public int lID;

    public object pvarValue;

    public VariableAlarmEventArgs(int _lAlarmType, int _lID, object _pvarValue)
    {
        lAlarmType = _lAlarmType;
        lID = _lID;
        pvarValue = _pvarValue;
    }
}
