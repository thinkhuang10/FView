using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("70841C78-067D-11D0-95D8-00A02463AB28")]
[CoClass(typeof(ErrorClass))]
public interface Error : IScriptError
{
}
