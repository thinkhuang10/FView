using System;
using System.Collections.Generic;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class DBInsertAnimation : DBAnimation
{
    public bool ansync;

    public string dbinsertSQL = "";

    public byte[] dbinsertOtherData;

    public List<int> dbinsertSafeRegion = new();
}
