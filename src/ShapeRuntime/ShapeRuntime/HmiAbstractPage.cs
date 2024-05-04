using System.Xml.Serialization;

namespace ShapeRuntime;

public abstract class HmiAbstractPage
{
    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlAttribute("Text")]
    public string Text { get; set; }

    [XmlIgnore]
    public HmiPageGroup Parent { get; set; }
}
