using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class SafeRegions
{
    public int id;

    public string regionName = "";

    public List<SafeRegions> SubSafeRegions = new();

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string RegionName
    {
        get
        {
            return regionName;
        }
        set
        {
            regionName = value;
        }
    }

    public override string ToString()
    {
        return RegionName;
    }
}
