using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using CommonSnappableTypes;
using SetsForms;
using ShapeRuntime;

namespace YiBiao;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap : CPixieControl
{
    private bool highalertflag;

    private bool lowalertflag;

    private int pt2 = 1;

    private float MaxV = 300f;

    private float MinV;

    private int mainmarkcount = 10;

    private int othermarkcount = 2;

    private string varname = "";

    private Color Bgcolor = Color.LightGray;

    private Color nmlcolor = Color.Green;

    private Color warncolor = Color.Yellow;

    private Color Errorcolor = Color.Red;

    private float nmlsta = 120f;

    private float nmlend = 180f;

    private float warnsta1 = 60f;

    private float warnsta2 = 180f;

    private float warnend1 = 120f;

    private float warnend2 = 240f;

    private float errorsta1;

    private float errorsta2 = 240f;

    private float errorend1 = 60f;

    private float errorend2 = 300f;

    private int _width = 200;

    private int _height = 200;

    private string Mark = "仪表";

    private Color bqcolor = Color.Black;

    private Color txtcolor = Color.Black;

    private int initflag;

    private int drawwidth = 200;

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(100f, 100f);

    private PointF pzero = new(55f, Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0)));

    private PointF pmax = new(145f, Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0)));

    private Color fontcolor = Color.Blue;

    private float value;

    [DHMICtrlProperty]
    [DisplayName("小数位")]
    [Category("杂项")]
    [Description("表盘刻度显示数值小数精确位数。")]
    [ReadOnly(false)]
    public int DicemalPlacesCount
    {
        get
        {
            return pt2;
        }
        set
        {
            pt2 = value;
        }
    }

    [DHMICtrlProperty]
    [Description("仪表量程上限。")]
    [ReadOnly(false)]
    [DisplayName("量程上限")]
    [Category("杂项")]
    public float MaxValue
    {
        get
        {
            return MaxV;
        }
        set
        {
            MaxV = value;
            if (nmlend > value)
            {
                nmlend = value;
            }
            if (warnend1 > value)
            {
                warnend1 = value;
            }
            if (warnend2 > value)
            {
                warnend2 = value;
            }
            if (errorend1 > value)
            {
                errorend1 = value;
            }
            if (errorend2 > value)
            {
                errorend2 = value;
            }
        }
    }

    [ReadOnly(false)]
    [DisplayName("量程下限")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("仪表量程下限。")]
    public float MinValue
    {
        get
        {
            return MinV;
        }
        set
        {
            MinV = value;
            if (nmlsta < value)
            {
                nmlsta = value;
            }
            if (warnsta1 < value)
            {
                warnsta1 = value;
            }
            if (warnsta2 < value)
            {
                warnsta2 = value;
            }
            if (errorsta1 < value)
            {
                errorsta1 = value;
            }
            if (errorsta2 < value)
            {
                errorsta2 = value;
            }
        }
    }

    [Category("杂项")]
    [DisplayName("主刻度数")]
    [Description("表盘主刻度线数量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public int Mainmarkcount
    {
        get
        {
            return mainmarkcount;
        }
        set
        {
            mainmarkcount = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("副刻度数")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘主刻度之间副刻度线数量。")]
    public int Othermarkcount
    {
        get
        {
            return othermarkcount;
        }
        set
        {
            othermarkcount = value;
        }
    }

    [Description("控件绑定变量名。")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
    [Category("杂项")]
    public string BindVar
    {
        get
        {
            if (varname.ToString().IndexOf('[') == -1)
            {
                return varname;
            }
            return varname.Substring(1, varname.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname = "[" + value.ToString() + "]";
            }
            else
            {
                varname = value;
            }
        }
    }

    [Description("表盘背景色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("表盘背景色")]
    public Color BackColor
    {
        get
        {
            return Bgcolor;
        }
        set
        {
            Bgcolor = value;
        }
    }

    [Description("当变量值处于配置的正常值范围内时指针指向的刻度的背景色。")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("正常范围颜色")]
    public Color FineColor
    {
        get
        {
            return nmlcolor;
        }
        set
        {
            nmlcolor = value;
        }
    }

    [Category("杂项")]
    [DisplayName("警告范围颜色")]
    [Description("当变量值处于配置的警告值范围内时指针指向的刻度的背景色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public Color WarnColor
    {
        get
        {
            return warncolor;
        }
        set
        {
            warncolor = value;
        }
    }

    [DisplayName("事故范围颜色")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("当变量值处于配置的事故值范围内时指针指向的刻度的背景色。")]
    [ReadOnly(false)]
    public Color ErrorColor
    {
        get
        {
            return Errorcolor;
        }
        set
        {
            Errorcolor = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("正常范围的起始值。")]
    [Category("杂项")]
    [DisplayName("正常范围起始值")]
    public float FineStart
    {
        get
        {
            return nmlsta;
        }
        set
        {
            nmlsta = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("正常范围结束值")]
    [Category("杂项")]
    [Description("正常范围的结束值。")]
    [ReadOnly(false)]
    public float FineEnd
    {
        get
        {
            return nmlend;
        }
        set
        {
            nmlend = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("警告范围的起始值。")]
    [Category("杂项")]
    [DisplayName("警告范围1起始值")]
    public float Warnstart1
    {
        get
        {
            return warnsta1;
        }
        set
        {
            warnsta1 = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("警告范围2起始值")]
    [Category("杂项")]
    [Description("警告范围的起始值。")]
    [ReadOnly(false)]
    public float Warnstart2
    {
        get
        {
            return warnsta2;
        }
        set
        {
            warnsta2 = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("警告范围1结束值")]
    [Category("杂项")]
    [Description("警告范围的结束值。")]
    [DHMICtrlProperty]
    public float Warnend1
    {
        get
        {
            return warnend1;
        }
        set
        {
            warnend1 = value;
        }
    }

    [DisplayName("警告范围2结束值")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("警告范围的结束值。")]
    public float Warnend2
    {
        get
        {
            return warnend2;
        }
        set
        {
            warnend2 = value;
        }
    }

    [DisplayName("事故范围1起始值")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("事故范围的起始值。")]
    [ReadOnly(false)]
    public float Errorstart1
    {
        get
        {
            return errorsta1;
        }
        set
        {
            errorsta1 = value;
        }
    }

    [Description("事故范围的起始值。")]
    [DisplayName("事故范围2起始值")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    public float Errorstart2
    {
        get
        {
            return errorsta2;
        }
        set
        {
            errorsta2 = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("事故范围1结束值")]
    [Category("杂项")]
    [Description("事故范围的结束值。")]
    [ReadOnly(false)]
    public float Errorend1
    {
        get
        {
            return errorend1;
        }
        set
        {
            errorend1 = value;
        }
    }

    [ReadOnly(false)]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("事故范围的结束值。")]
    [DisplayName("事故范围2结束值")]
    public float Errorend2
    {
        get
        {
            return errorend2;
        }
        set
        {
            errorend2 = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("文本")]
    [Description("表盘上显示的文本。")]
    [ReadOnly(false)]
    public string Text
    {
        get
        {
            return Mark;
        }
        set
        {
            Mark = value;
        }
    }

    [Category("杂项")]
    [Description("表盘上刻度值颜色。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("刻度值颜色")]
    public Color MarkTextColor
    {
        get
        {
            return txtcolor;
        }
        set
        {
            txtcolor = value;
        }
    }

    [DHMICtrlProperty]
    [Description("表盘上显示的文本的字体颜色。")]
    [DisplayName("文本颜色")]
    [ReadOnly(false)]
    [Category("杂项")]
    public Color Fontcolor
    {
        get
        {
            return bqcolor;
        }
        set
        {
            bqcolor = value;
        }
    }

    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("当前值")]
    [Description("仪表绑定变量的当前值。")]
    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value == value)
            {
                return;
            }

            float num;
            float num2;
            if (errorsta1 < errorsta2)
            {
                num = errorsta1;
                num2 = errorsta2;
            }
            else
            {
                num = errorsta2;
                num2 = errorsta1;
            }

            float num3;
            float num4;
            if (errorend1 < errorend2)
            {
                num3 = errorend1;
                num4 = errorend2;
            }
            else
            {
                num3 = errorend2;
                num4 = errorend1;
            }
            if (Convert.ToSingle(value) > num && Convert.ToSingle(value) < num3)
            {
                if (!lowalertflag && LowAlert != null)
                {
                    LowAlert(this, null);
                    lowalertflag = true;
                }
            }
            else
            {
                lowalertflag = false;
            }
            if (Convert.ToSingle(value) > num2 && Convert.ToSingle(value) < num4)
            {
                if (!highalertflag && HighAlert != null)
                {
                    HighAlert(this, null);
                    highalertflag = true;
                }
            }
            else
            {
                highalertflag = false;
            }
            NeedRefresh = true;
            this.value = value;
        }
    }

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public drawbitmap()
    {
    }

    protected drawbitmap(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap obj = new();
        FieldInfo[] fields = typeof(drawbitmap).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap))
            {
                FieldInfo fieldInfo2 = (FieldInfo)serializableMembers[j];
                if (!Attribute.IsDefined(serializableMembers[j], typeof(NonSerializedAttribute)))
                {
                    fieldInfo2.SetValue(this, info.GetValue(fieldInfo2.Name, fieldInfo2.FieldType));
                }
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap10 = (drawbitmap)base.Copy();
        drawbitmap10.MaxV = MaxV;
        drawbitmap10.MinV = MinV;
        drawbitmap10.mainmarkcount = mainmarkcount;
        drawbitmap10.othermarkcount = othermarkcount;
        drawbitmap10.varname = varname;
        drawbitmap10.Bgcolor = Bgcolor;
        drawbitmap10.nmlcolor = nmlcolor;
        drawbitmap10.warncolor = warncolor;
        drawbitmap10.Errorcolor = Errorcolor;
        drawbitmap10.nmlsta = nmlsta;
        drawbitmap10.nmlend = nmlend;
        drawbitmap10.warnsta1 = warnsta1;
        drawbitmap10.warnsta2 = warnsta2;
        drawbitmap10.warnend1 = warnend1;
        drawbitmap10.warnend2 = warnend2;
        drawbitmap10.errorsta1 = errorsta1;
        drawbitmap10.errorsta2 = errorsta2;
        drawbitmap10.errorend1 = errorend1;
        drawbitmap10.errorend2 = errorend2;
        drawbitmap10.Mark = Mark;
        drawbitmap10.initflag = initflag;
        drawbitmap10.pffnow = pffnow;
        drawbitmap10.pcenter = pcenter;
        drawbitmap10.pzero = pzero;
        drawbitmap10.txtcolor = txtcolor;
        drawbitmap10.bqcolor = bqcolor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        pcenter = new PointF(100 * drawwidth / 200, 100 * drawwidth / 200);
        pzero = new PointF(55 * drawwidth / 200, Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0)) * (float)drawwidth / 200f);
        pmax = new PointF(145 * drawwidth / 200, Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0)) * (float)drawwidth / 200f);
        SolidBrush brush = new(Bgcolor);
        new SolidBrush(fontcolor);
        SolidBrush brush2 = new(Errorcolor);
        SolidBrush brush3 = new(warncolor);
        SolidBrush brush4 = new(nmlcolor);
        FontFamily family = new("Arial");
        Font font = new(family, 10 * drawwidth / 200);
        int num = mainmarkcount * othermarkcount;
        List<PointF> list = new();
        List<float> list2 = new();
        Rectangle rect = new(10 * drawwidth / 200, 10 * drawwidth / 200, 180 * drawwidth / 200, 180 * drawwidth / 200);
        g.DrawPie(Pens.Black, rect, 120f, 300f);
        g.FillPie(brush2, rect, 120f + 300f * (errorsta1 - MinV) / (MaxV - MinV), 300f * (errorend1 - MinV) / (MaxV - MinV) + 120f - (120f + 300f * (errorsta1 - MinV) / (MaxV - MinV)));
        g.FillPie(brush3, rect, 120f + 300f * (warnsta1 - MinV) / (MaxV - MinV), 300f * (warnend1 - MinV) / (MaxV - MinV) + 120f - (120f + 300f * (warnsta1 - MinV) / (MaxV - MinV)));
        g.FillPie(brush4, rect, 120f + 300f * (nmlsta - MinV) / (MaxV - MinV), 300f * (nmlend - MinV) / (MaxV - MinV) + 120f - (120f + 300f * (nmlsta - MinV) / (MaxV - MinV)));
        g.FillPie(brush3, rect, 120f + 300f * (warnsta2 - MinV) / (MaxV - MinV), 300f * (warnend2 - MinV) / (MaxV - MinV) + 120f - (120f + 300f * (warnsta2 - MinV) / (MaxV - MinV)));
        g.FillPie(brush2, rect, 120f + 300f * (errorsta2 - MinV) / (MaxV - MinV), 300f * (errorend2 - MinV) / (MaxV - MinV) + 120f - (120f + 300f * (errorsta2 - MinV) / (MaxV - MinV)));
        for (int i = 1; i < mainmarkcount; i++)
        {
            if (30 + i * 300 / mainmarkcount <= 90)
            {
                float num2 = Convert.ToSingle(100.0 - 90.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num3 = Convert.ToInt32(100.0 - 80.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                float num4 = Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num5 = Convert.ToInt32(100.0 + 80.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num6 = num3 + 3;
                int num7 = num5 + Convert.ToInt32((double)(num3 - 100) * 0.1);
                PointF pointF = new(num2 * (float)drawwidth / 200f, num4 * (float)drawwidth / 200f);
                PointF item = new(Convert.ToSingle(num6) * (float)drawwidth / 200f, Convert.ToSingle(num7) * (float)drawwidth / 200f);
                list.Add(item);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(Pens.Black, pcenter, pointF);
            }
            else if (30 + i * 300 / mainmarkcount > 90 && 30 + i * 300 / mainmarkcount <= 180)
            {
                float num8 = Convert.ToSingle(100.0 - 90.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num9 = Convert.ToInt32(100.0 - 80.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                float num10 = Convert.ToSingle(100.0 - 90.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num11 = Convert.ToInt32(100.0 - 80.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num12 = num9 - Convert.ToInt32((double)num9 * 0.05);
                int num13 = num11 + Convert.ToInt32((double)(100 - num11) * 0.05);
                PointF pointF2 = new(num8 * (float)drawwidth / 200f, num10 * (float)drawwidth / 200f);
                PointF item2 = new(Convert.ToSingle(num12) * (float)drawwidth / 200f, Convert.ToSingle(num13) * (float)drawwidth / 200f);
                list.Add(item2);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(Pens.Black, pcenter, pointF2);
            }
            else if (30 + i * 300 / mainmarkcount > 180 && 30 + i * 300 / mainmarkcount <= 270)
            {
                float num14 = Convert.ToSingle(100.0 + 90.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num15 = Convert.ToInt32(100.0 + 80.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                float num16 = Convert.ToSingle(100.0 - 90.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num17 = Convert.ToInt32(100.0 - 80.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num18 = num15 - Convert.ToInt32((double)(num15 - 100) * 0.3);
                int num19 = num17;
                PointF pointF3 = new(num14 * (float)drawwidth / 200f, num16 * (float)drawwidth / 200f);
                PointF item3 = new(Convert.ToSingle(num18) * (float)drawwidth / 200f, Convert.ToSingle(num19) * (float)drawwidth / 200f);
                list.Add(item3);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(Pens.Black, pcenter, pointF3);
            }
            else if (30 + i * 300 / mainmarkcount > 270 && 30 + i * 300 / mainmarkcount <= 330)
            {
                float num20 = Convert.ToSingle(100.0 + 90.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num21 = Convert.ToInt32(100.0 + 80.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                float num22 = Convert.ToSingle(100.0 + 90.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num23 = Convert.ToInt32(100.0 + 80.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num24 = num21 - Convert.ToInt32((double)(num21 - 100) * 0.33);
                int num25 = num23 - Convert.ToInt32((double)(num23 - 100) * 0.2);
                PointF pointF4 = new(num20 * (float)drawwidth / 200f, num22 * (float)drawwidth / 200f);
                PointF item4 = new(Convert.ToSingle(num24) * (float)drawwidth / 200f, Convert.ToSingle(num25) * (float)drawwidth / 200f);
                list.Add(item4);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(Pens.Black, pcenter, pointF4);
            }
        }
        for (int j = 1; j < num; j++)
        {
            if (30 + j * 300 / num <= 90)
            {
                float num26 = Convert.ToSingle(100.0 - 85.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num));
                float num27 = Convert.ToSingle(100.0 + 85.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num));
                g.DrawLine(pt2: new PointF(num26 * (float)drawwidth / 200f, num27 * (float)drawwidth / 200f), pen: Pens.Black, pt1: pcenter);
            }
            else if (30 + j * 300 / num > 90 && 30 + j * 300 / num <= 180)
            {
                float num28 = Convert.ToSingle(100.0 - 85.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - Math.PI / 2.0));
                float num29 = Convert.ToSingle(100.0 - 85.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - Math.PI / 2.0));
                g.DrawLine(pt2: new PointF(num28 * (float)drawwidth / 200f, num29 * (float)drawwidth / 200f), pen: Pens.Black, pt1: pcenter);
            }
            else if (30 + j * 300 / num > 180 && 30 + j * 300 / num <= 270)
            {
                float num30 = Convert.ToSingle(100.0 + 85.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - Math.PI));
                float num31 = Convert.ToSingle(100.0 - 85.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - Math.PI));
                g.DrawLine(pt2: new PointF(num30 * (float)drawwidth / 200f, num31 * (float)drawwidth / 200f), pen: Pens.Black, pt1: pcenter);
            }
            else if (30 + j * 300 / num > 270 && 30 + j * 300 / num <= 330)
            {
                float num32 = Convert.ToSingle(100.0 + 85.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - 4.71238898038469));
                float num33 = Convert.ToSingle(100.0 + 85.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)num - 4.71238898038469));
                g.DrawLine(pt2: new PointF(num32 * (float)drawwidth / 200f, num33 * (float)drawwidth / 200f), pen: Pens.Black, pt1: pcenter);
            }
        }
        Rectangle rect2 = new(20 * drawwidth / 200, 20 * drawwidth / 200, 160 * drawwidth / 200, 160 * drawwidth / 200);
        g.DrawPie(Pens.Black, rect2, 0f, 360f);
        g.FillPie(brush, rect2, 0f, 360f);
        g.FillPie(rect: new Rectangle(95 * drawwidth / 200, 95 * drawwidth / 200, 10 * drawwidth / 200, 10 * drawwidth / 200), brush: Brushes.Black, startAngle: 0f, sweepAngle: 360f);
        if (initflag == 1)
        {
            g.DrawLine(Pens.Red, pcenter, pzero);
            initflag = 0;
        }
        new Rectangle(80 * drawwidth / 200, 110 * drawwidth / 200, 40 * drawwidth / 200, 25 * drawwidth / 200);
        PointF point = new(85 * drawwidth / 200, 113 * drawwidth / 200);
        SolidBrush brush5 = new(bqcolor);
        g.DrawString(Mark, font, brush5, point);
        PointF point2 = new(63 * drawwidth / 200, pzero.Y - (float)(22 * drawwidth / 200));
        PointF point3 = new(120 * drawwidth / 200, pzero.Y - (float)(22 * drawwidth / 200));
        SolidBrush brush6 = new(txtcolor);
        g.DrawString(MinV.ToString(), font, brush6, point2);
        g.DrawString(MaxV.ToString(), font, brush6, point3);
        for (int k = 0; k < list2.Count; k++)
        {
            g.DrawString(list2[k].ToString(), font, brush6, list[k]);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = (_width = 200);
                drawwidth = 200;
            }
            else
            {
                _width = Convert.ToInt32(Math.Abs(Width));
                _height = Convert.ToInt32(Math.Abs(Height));
                if (_width < _height)
                {
                    drawwidth = _width;
                }
                else
                {
                    drawwidth = _height;
                }
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToSingle(obj);
            }
            try
            {
                initflag = 0;
                Paint(graphics);
                if (Value <= MaxV && Value >= MinV)
                {
                    if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.2)
                    {
                        pffnow.X = (100f - Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.5 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.2)
                    {
                        pffnow.X = (100f - Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.8 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.5)
                    {
                        pffnow.X = (100f + Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) > 0.8)
                    {
                        pffnow.X = (100f + Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)drawwidth / 200f;
                    }
                    graphics.DrawLine(Pens.Red, pcenter, pffnow);
                }
                else if (Value > MaxV)
                {
                    graphics.DrawLine(Pens.Red, pcenter, pmax);
                }
                else if (Value < MinV)
                {
                    graphics.DrawLine(Pens.Red, pcenter, pzero);
                }
            }
            catch
            {
            }
            FinishRefresh(bitmap);
        }
        catch
        {
        }
    }

    public override void ShowDialog()
    {
        BSet bSet = new();
        if (varname != "")
        {
            bSet.VarName = varname;
            bSet.MaxValue = MaxV;
            bSet.MinValue = MinV;
            bSet.MainMarkCount = mainmarkcount;
            bSet.OtherMarkCount = othermarkcount;
            bSet.Bgcolor = Bgcolor;
            bSet.nmlcolor = nmlcolor;
            bSet.warncolor = warncolor;
            bSet.Errorcolor = Errorcolor;
            bSet.nmlsta = nmlsta;
            bSet.nmlend = nmlend;
            bSet.warnsta1 = warnsta1;
            bSet.warnsta2 = warnsta2;
            bSet.warnend1 = warnend1;
            bSet.warnend2 = warnend2;
            bSet.errorsta1 = errorsta1;
            bSet.errorsta2 = errorsta2;
            bSet.errorend1 = errorend1;
            bSet.errorend2 = errorend2;
            bSet.Mark = Mark;
            bSet.pt2 = pt2;
            bSet.BiaoqianColor = bqcolor;
            bSet.TxtColor = txtcolor;
        }
        bSet.OtherMarkCount = Othermarkcount;
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            mainmarkcount = bSet.MainMarkCount;
            othermarkcount = bSet.OtherMarkCount;
            Bgcolor = bSet.Bgcolor;
            nmlcolor = bSet.nmlcolor;
            warncolor = bSet.warncolor;
            Errorcolor = bSet.Errorcolor;
            nmlsta = bSet.nmlsta;
            nmlend = bSet.nmlend;
            warnsta1 = bSet.warnsta1;
            warnsta2 = bSet.warnsta2;
            warnend1 = bSet.warnend1;
            warnend2 = bSet.warnend2;
            errorsta1 = bSet.errorsta1;
            errorsta2 = bSet.errorsta2;
            errorend1 = bSet.errorend1;
            errorend2 = bSet.errorend2;
            pt2 = bSet.pt2;
            Mark = bSet.Mark;
            bqcolor = bSet.BiaoqianColor;
            txtcolor = bSet.TxtColor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            MaxV = MaxV,
            MinV = MinV,
            mainmarkcount = mainmarkcount,
            othermarkcount = othermarkcount,
            varname = varname,
            Bgcolor = Bgcolor,
            nmlcolor = nmlcolor,
            warncolor = warncolor,
            Errorcolor = Errorcolor,
            nmlsta = nmlsta,
            nmlend = nmlend,
            warnsta1 = warnsta1,
            warnsta2 = warnsta2,
            warnend1 = warnend1,
            warnend2 = warnend2,
            errorsta1 = errorsta1,
            errorsta2 = errorsta2,
            errorend1 = errorend1,
            errorend2 = errorend2,
            Mark = Mark,
            initflag = initflag,
            pffnow = pffnow,
            pcenter = pcenter,
            pzero = pzero,
            fontcolor = fontcolor
        };
        formatter.Serialize(memoryStream, bSave);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public override void Deserialize(byte[] data)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream(data);
        BSave bSave = (BSave)formatter.Deserialize(stream);
        stream.Close();
        MaxV = bSave.MaxV;
        MinV = bSave.MinV;
        mainmarkcount = bSave.mainmarkcount;
        othermarkcount = bSave.othermarkcount;
        varname = bSave.varname;
        Bgcolor = bSave.Bgcolor;
        nmlcolor = bSave.nmlcolor;
        warncolor = bSave.warncolor;
        Errorcolor = bSave.Errorcolor;
        nmlsta = bSave.nmlsta;
        nmlend = bSave.nmlend;
        warnsta1 = bSave.warnsta1;
        warnsta2 = bSave.warnsta2;
        warnend1 = bSave.warnend1;
        warnend2 = bSave.warnend2;
        errorsta1 = bSave.errorsta1;
        errorsta2 = bSave.errorsta2;
        errorend1 = bSave.errorend1;
        errorend2 = bSave.errorend2;
        Mark = bSave.Mark;
        initflag = bSave.initflag;
        pffnow = bSave.pffnow;
        pcenter = bSave.pcenter;
        pzero = bSave.pzero;
        fontcolor = bSave.fontcolor;
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
