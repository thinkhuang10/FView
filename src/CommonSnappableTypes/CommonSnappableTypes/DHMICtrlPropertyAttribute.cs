using System;

namespace CommonSnappableTypes;

public class DHMICtrlPropertyAttribute : Attribute
{
    private readonly string showName;

    private readonly string name;

    public DHMICtrlPropertyAttribute()
    {
    }

    public DHMICtrlPropertyAttribute(string showName, string name)
    {
        this.showName = showName;
        this.name = name;
    }
}
