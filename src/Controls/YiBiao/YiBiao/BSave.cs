using System;
using System.Drawing;

namespace YiBiao;

[Serializable]
public class BSave
{
    public float MaxV;

    public float MinV;

    public int mainmarkcount;

    public int othermarkcount;

    public string varname;

    public Color Bgcolor;

    public Color nmlcolor;

    public Color warncolor;

    public Color Errorcolor;

    public float nmlsta;

    public float nmlend;

    public float warnsta1;

    public float warnsta2;

    public float warnend1;

    public float warnend2;

    public float errorsta1;

    public float errorsta2;

    public float errorend1;

    public float errorend2;

    public string Mark;

    public int initflag;

    public PointF pffnow;

    public PointF pcenter;

    public PointF pzero;

    public Color fontcolor;
}
