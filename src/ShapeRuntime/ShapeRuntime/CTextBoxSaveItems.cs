using System;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class CTextBoxSaveItems
{
    public string Text;

    public Font Font;

    public Color BackColor;

    public Color ForeColor = Color.Black;

    public bool IsPassword;

    public bool IsMultiline;

    public int TabIndex;

    public int ScrollStyle;

    public bool ReadOnly;

    public bool hide;

    public bool disable;

    public string textVarBind;
}
