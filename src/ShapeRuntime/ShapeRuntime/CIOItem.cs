using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class CIOItem
{
    public bool IsLeaf;

    public string name = "";

    public string MsgID = "";

    public string id = "";

    public string type = "";

    public string tag = "";

    public List<CIOItem> subitem = new();
}
