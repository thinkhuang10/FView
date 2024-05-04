using System;

namespace HMIEditEnvironment;

public class FindResultEventArgs : EventArgs
{
    public FindResult Result { get; private set; }

    public FindResultEventArgs(FindResult result)
    {
        Result = result;
    }
}
