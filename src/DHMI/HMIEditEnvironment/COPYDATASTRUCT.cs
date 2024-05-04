using System;
using System.Runtime.InteropServices;

namespace HMIEditEnvironment;

public struct COPYDATASTRUCT
{
    public IntPtr dwData;

    public int cbData;

    [MarshalAs(UnmanagedType.LPStr)]
    public string lpData;
}
