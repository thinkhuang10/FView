using System;

namespace ShapeRuntime.DBAnimation;

[Serializable]
public class DBAnimation
{
    public string Type = "";

    public bool enable;

    public override string ToString()
    {
        if (Type != "")
        {
            return Type;
        }
        return base.ToString();
    }
}
