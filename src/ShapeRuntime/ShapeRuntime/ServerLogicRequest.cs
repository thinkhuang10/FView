using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class ServerLogicRequest
{
    public string Id = Guid.NewGuid().ToString();

    public Dictionary<string, object> InputDict;

    public List<ServerLogicItem> LogicItemList;

    public List<string> OutputList;

    public bool Ansync;
}
