using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class CAuthoritySeiallize
{
    public Dictionary<string, CAuthorityInfo> dicAuthority = new();

    public bool bProjectStart;

    public bool bProjectEnd;
}
