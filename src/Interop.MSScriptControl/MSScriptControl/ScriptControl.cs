using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("0E59F1D3-1FBE-11D0-8FF2-00A0D10038BC")]
[CoClass(typeof(ScriptControlClass))]
public interface ScriptControl : IScriptControl
{
}
