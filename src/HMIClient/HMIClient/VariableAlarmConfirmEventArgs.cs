using System;

namespace HMIClient;

public class VariableAlarmConfirmEventArgs : EventArgs
{
    public string id;

    public string confirmTime;

    public string confirmUser;

    public VariableAlarmConfirmEventArgs(string id, string confirmTime, string confirmUser)
    {
        this.id = id;
        this.confirmTime = confirmTime;
        this.confirmUser = confirmUser;
    }
}
