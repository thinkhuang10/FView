using System;
using System.Drawing;

namespace YouBiao;

[Serializable]
public class BSave
{
    public string varname;

    public float maxval;

    public float minval;

    public int mainmark;

    public int othermark;

    public int ptcount;

    public Color color1 = Color.Black;

    public bool dragableflag;
}
