using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class GlobalScriptClass
{
    public string Name = "";

    public List<string> Params = new();

    public string Script;

    public string ResultScript;

    public override string ToString()
    {
        return Name;
    }
}
