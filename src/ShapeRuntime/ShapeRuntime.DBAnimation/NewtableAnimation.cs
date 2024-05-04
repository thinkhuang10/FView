using System;
using System.Collections.Generic;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class NewtableAnimation : DBAnimation
{
    public bool ansync;

    public string newtableSQL = "";

    public byte[] newtableOtherData;

    public List<int> newtableSafeRegion = new();
}
