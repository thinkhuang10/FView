using System;
using System.Drawing;

namespace Button_1;

[Serializable]
public class Save_strbtn
{
    public string strtoshow;

    public Color bgcolor;

    public Color txtcolor;

    private readonly Font selectfont = new(new FontFamily("新宋体"), 10f);
}
