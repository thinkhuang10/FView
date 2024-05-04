using System;
using System.Collections.Generic;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class DBDeleteAnimation : DBAnimation
{
    public bool ansync;

    public string dbdeleteSQL = "";

    public byte[] dbdeleteOtherData;

    public List<int> dbdeleteSafeRegion = new();
}
