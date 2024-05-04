using System.Runtime.InteropServices;

namespace MSScriptControl;

[ComImport]
[Guid("70841C71-067D-11D0-95D8-00A02463AB28")]
[CoClass(typeof(ProceduresClass))]
public interface Procedures : IScriptProcedureCollection
{
}
