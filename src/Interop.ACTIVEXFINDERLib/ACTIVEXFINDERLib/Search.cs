using System.Runtime.InteropServices;

namespace ACTIVEXFINDERLib;

[ComImport]
[CoClass(typeof(SearchClass))]
[Guid("9479487B-491D-4DC4-A639-5A1C75DF8468")]
public interface Search : ISearch
{
}
