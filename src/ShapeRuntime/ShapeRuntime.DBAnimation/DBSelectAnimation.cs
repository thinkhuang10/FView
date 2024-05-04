using System;
using System.Collections.Generic;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class DBSelectAnimation : DBAnimation
{
    public bool ansync;

    public string dbselectSQL = "";

    public string dbselectTO = "";

    public byte[] dbselectOtherData;

    public List<int> dbselectSafeRegion = new();
}
