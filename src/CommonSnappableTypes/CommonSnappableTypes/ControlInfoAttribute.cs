using System;

namespace CommonSnappableTypes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ControlInfoAttribute : Attribute
{
    private string controlname;

    public string Controlname
    {
        get
        {
            return controlname;
        }
        set
        {
            controlname = value;
        }
    }
}
