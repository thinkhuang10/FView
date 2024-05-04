using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class CAuthorityInfo
{
    public string strName = "";

    public string strAuthority = "";

    public string srtPassword = "";

    public List<string> ltSafeRegion = new();
}
