using System;
using System.Drawing;

namespace YiBiao;

[Serializable]
public class BSaveC9
{
    public string varname;

    public int intcount = 4;

    public int dbcount = 2;

    public bool flag1;

    public bool colortransflag;

    public bool offtrans;

    public Color bgcolor = Color.Black;

    public Color oncolor = Color.LightGreen;

    public Color offcolor = Color.DarkGreen;
}
