using System;

namespace ShapeRuntime;

[Serializable]
public class ProjectIO
{
    public bool isOPC;

    public string OPChostname = "";

    public string OPCservername = "";

    public string OPCvarname = "";

    public bool OPCreadfromcache;

    public int ID;

    public string name = "";

    public string type = "";

    public string tag = "";

    public string description = "";

    public string access = "";

    public string emluator = "";

    public string max = "";

    public string min = "";

    public string T = "";

    public string delay = "";

    public string GroupName = "内部变量";

    public bool History;

    public static int StaticID;

    public ProjectIO()
    {
        StaticID++;
        ID = StaticID;
    }

    public ProjectIO(bool empty)
    {
    }

    public ProjectIO Copy()
    {
        ProjectIO projectIO = new(empty: true)
        {
            ID = ID,
            name = name,
            type = type,
            tag = tag,
            description = description,
            access = access,
            emluator = emluator,
            max = max,
            min = min,
            T = T,
            delay = delay,
            GroupName = GroupName,
            History = History
        };
        return projectIO;
    }
}
