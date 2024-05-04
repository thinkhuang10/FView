using System;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class CPictureBoxSaveItems
{
    public Image Image;

    public int FillSizeMode;

    public bool hide;

    public bool disable;

    public string ImageTag;
}
