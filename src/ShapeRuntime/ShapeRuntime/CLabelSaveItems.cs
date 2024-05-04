using System;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class CLabelSaveItems
{
    public string Text;

    public Font Font;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public bool hide;

    public bool disable;

    public ContentAlignment align;

    public string textVarBind;
}
