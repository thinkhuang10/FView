using System.Xml.Serialization;

namespace ShapeRuntime;

public class HmiPage : HmiAbstractPage
{
    [XmlIgnore]
    public DataFile DataFile { get; set; }
}
