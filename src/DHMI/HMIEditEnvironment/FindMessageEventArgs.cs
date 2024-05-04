using System;

namespace HMIEditEnvironment;

public class FindMessageEventArgs : EventArgs
{
    public string Message { get; private set; }

    public FindMessageEventArgs(string message)
    {
        Message = message;
    }
}
