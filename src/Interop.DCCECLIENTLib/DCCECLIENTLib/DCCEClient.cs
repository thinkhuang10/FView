using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[ComImport]
[Guid("6B1F0316-3D41-4406-A19A-203E5558999A")]
[CoClass(typeof(DCCEClientClass))]
public interface DCCEClient : _DDCCEClient, _DDCCEClientEvents_Event
{
}
