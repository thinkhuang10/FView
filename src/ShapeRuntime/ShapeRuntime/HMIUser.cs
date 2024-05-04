using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class HMIUser
{
    public long id;

    public string name = "";

    public int type;

    public string password = "";

    public List<int> Regions = new();

    public int time = -1;
}
