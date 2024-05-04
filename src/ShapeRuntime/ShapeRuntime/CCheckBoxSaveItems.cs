using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace ShapeRuntime;

[Serializable]
public class CCheckBoxSaveItems
{
    public string Text;

    public Font Font;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public bool Checked;

    public int TabIndex;

    public bool hide;

    public bool disable;

    public bool Enabled;

    [OptionalField]
    public string VarBind;

    public string textVarBind;
}
