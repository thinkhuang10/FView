using System;
using System.Drawing;

namespace CommonSnappableTypes;

[Serializable]
public class BitMapForIM
{
    public string ImgGUID;

    public Image img;

    public override string ToString()
    {
        if (img == null)
        {
            return "";
        }
        return "W:" + img.Width + " H:" + img.Height;
    }
}
