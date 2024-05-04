using System.Xml.Serialization;

namespace ShapeRuntime;

public class HmiPageFile
{
    [XmlAttribute("Name")]
    public string PageName;

    [XmlAttribute("File")]
    public string FileName;
}
