using System;

namespace HMIClient;

public class DeviceAlarmEventArgs : EventArgs
{
    public int _lDevID;

    public int _lState;

    public DeviceAlarmEventArgs(int _lDevID, int _lState)
    {
        this._lDevID = _lDevID;
        this._lState = _lState;
    }
}
