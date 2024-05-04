using System;

namespace HMIClient;

public class VarForAlarmEventArgs : EventArgs
{
    public string id;

    public string AlarmTime;

    public int AlarmVar;

    public string VarValue;

    public int lAlarmType;

    public VarForAlarmEventArgs(string _ID, string _AlarmTime, int _AlarmVar, string _VarValue, int _AlarmType)
    {
        id = _ID;
        AlarmTime = _AlarmTime;
        AlarmVar = _AlarmVar;
        VarValue = _VarValue;
        lAlarmType = _AlarmType;
    }
}
