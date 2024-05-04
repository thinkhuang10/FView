using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class ParaIO
{
    public bool isPara;

    public int ID;

    public List<int> RBev = new();

    public List<int> WBev = new();

    public string type = "";

    public string name = "";
}
