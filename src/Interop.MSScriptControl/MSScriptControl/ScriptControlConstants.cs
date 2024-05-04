using System.Runtime.InteropServices;

namespace MSScriptControl;

[Guid("AE311340-082C-11D0-95DE-00A02463AB28")]
public abstract class ScriptControlConstants
{
    [MarshalAs(UnmanagedType.LPStr)]
    public const string GlobalModule = "Global";

    public const int NoTimeout = -1;
}
