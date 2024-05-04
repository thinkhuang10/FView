using System;
using System.Collections.Generic;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class DBUpdateAnimation : DBAnimation
{
    public bool ansync;

    public string dbupdateSQL = "";

    public byte[] dbupdateOtherData;

    public List<int> dbupdateSafeRegion = new();
}
