using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShapeRuntime;

public class HmiPageGroup : HmiAbstractPage
{
    private readonly List<HmiAbstractPage> _list = new();

    [XmlArrayItem("PageGroup", typeof(HmiPageGroup))]
    [XmlArray("Items")]
    [XmlArrayItem("Page", typeof(HmiPage))]
    public List<HmiAbstractPage> Children => _list;
}
