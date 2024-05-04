using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShapeRuntime;

public class HmiProject
{
    [XmlElement("PageGroup")]
    public HmiPageGroup PageGroup;

    [XmlArray("PageFiles")]
    [XmlArrayItem("PageFile")]
    public List<HmiPageFile> PageFiles;
}
