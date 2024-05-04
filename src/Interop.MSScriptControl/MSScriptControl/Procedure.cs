using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[CoClass(typeof(ProcedureClass))]
[Guid("70841C73-067D-11D0-95D8-00A02463AB28")]
public interface Procedure : IScriptProcedure
{
}
