using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComVisible(false)]
[TypeLibType(16)]
[ComEventInterface(typeof(DScriptControlSource), typeof(DScriptControlSource_EventProvider))]
public interface DScriptControlSource_Event
{
    event DScriptControlSource_ErrorEventHandler Error;

    event DScriptControlSource_TimeoutEventHandler Timeout;
}
