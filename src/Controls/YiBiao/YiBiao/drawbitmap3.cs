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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap3 : CPixieControl
{
    private int drawwidth = 200;

    private int pt2 = 1;

    private float MaxV = 300f;

    private float MinV;

    private int mainmarkcount = 10;

    private int othermarkcount = 2;

    private string varname = "";

    private Color Bgcolor = Color.White;

    private Color Markcolor = Color.Black;

    private Color bqcolor = Color.Black;

    private Color txtcolor = Color.Black;

    private string mark = "仪表";

    private int initflag = 1;

    private int _width = 200;

    private int _height = 200;

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(100f, 100f);

    private PointF pzero = new(65f, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)));

    private PointF pmax = new(135f, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)));

    private Color fontcolor = Color.Blue;

    private float value;

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘刻度显示数值小数精确位数。")]
    [ReadOnly(false)]
    [DisplayName("小数位")]
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

    [Description("仪表量程上限。")]
    [Category("杂项")]
    [DisplayName("量程上限")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public float MaxValue
    {
        get
        {
            return MaxV;
        }
        set
        {
            MaxV = value;
        }
    }

    [DisplayName("量程下限")]
    [Category("杂项")]
    [Description("仪表量程下限。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public float MinValue
    {
        get
        {
            return MinV;
        }
        set
        {
            MinV = value;
        }
    }

    [DisplayName("主刻度数")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("表盘主刻度线数量。")]
    [ReadOnly(false)]
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

    [Description("表盘主刻度之间副刻度线数量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("副刻度数")]
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

    [ReadOnly(false)]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
    [DisplayName("绑定变量")]
    [DHMICtrlProperty]
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

    [DisplayName("表盘背景色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘背景色。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [DisplayName("表盘刻度线颜色")]
    [Category("杂项")]
    [Description("表盘刻度线颜色。")]
    [ReadOnly(false)]
    public Color MarkLineColor
    {
        get
        {
            return Markcolor;
        }
        set
        {
            Markcolor = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("表盘刻度值颜色")]
    [Category("杂项")]
    [Description("表盘刻度值颜色。")]
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
    [ReadOnly(false)]
    [DisplayName("文本")]
    [Category("杂项")]
    [Description("表盘上显示的文本值。")]
    public string Text
    {
        get
        {
            return mark;
        }
        set
        {
            mark = value;
        }
    }

    [Description("表盘上显示的文本的颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("文本颜色")]
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

    [ReadOnly(false)]
    [Description("仪表绑定变量的当前值。")]
    [DisplayName("当前值")]
    [Category("杂项")]
    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public drawbitmap3()
    {
    }

    protected drawbitmap3(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap3 obj = new();
        FieldInfo[] fields = typeof(drawbitmap3).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap3))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap3))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap3) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap3 drawbitmap10 = (drawbitmap3)base.Copy();
        drawbitmap10.MaxV = MaxV;
        drawbitmap10.MinV = MinV;
        drawbitmap10.mainmarkcount = mainmarkcount;
        drawbitmap10.othermarkcount = othermarkcount;
        drawbitmap10.varname = varname;
        drawbitmap10.Bgcolor = Bgcolor;
        drawbitmap10.initflag = initflag;
        drawbitmap10.pffnow = pffnow;
        drawbitmap10.pcenter = pcenter;
        drawbitmap10.pzero = pzero;
        drawbitmap10.fontcolor = fontcolor;
        drawbitmap10.Markcolor = Markcolor;
        drawbitmap10.txtcolor = txtcolor;
        drawbitmap10.bqcolor = bqcolor;
        drawbitmap10.mark = mark;
        drawbitmap10.pt2 = pt2;
        return drawbitmap10;
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public void Paint(Graphics g)
    {
        pcenter = new PointF(100 * drawwidth / 200, 100 * drawwidth / 200);
        pzero = new PointF(65 * drawwidth / 200, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)) * (float)drawwidth / 200f);
        pmax = new PointF(135 * drawwidth / 200, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)) * (float)drawwidth / 200f);
        Rectangle rect = new(drawwidth / 200, drawwidth / 200, 199 * drawwidth / 200, 199 * drawwidth / 200);
        SolidBrush brush = new(Color.Transparent);
        g.FillRectangle(brush, rect);
        new SolidBrush(fontcolor);
        new SolidBrush(Markcolor);
        Pen pen = new(Markcolor);
        FontFamily family = new("Arial");
        Font font = new(family, 10 * drawwidth / 200);
        int num = mainmarkcount * othermarkcount;
        List<PointF> list = new();
        List<float> list2 = new();
        SolidBrush brush2 = new(Color.Gray);
        SolidBrush brush3 = new(Bgcolor);
        Rectangle rect2 = new(5 * drawwidth / 200, 5 * drawwidth / 200, 190 * drawwidth / 200, 190 * drawwidth / 200);
        g.DrawPie(Pens.Black, rect2, 0f, 360f);
        g.FillPie(brush2, rect2, 0f, 360f);
        Rectangle rect3 = new(30 * drawwidth / 200, 30 * drawwidth / 200, 140 * drawwidth / 200, 140 * drawwidth / 200);
        g.DrawPie(Pens.Black, rect3, 0f, 360f);
        g.FillPie(brush3, rect3, 0f, 360f);
        for (int i = 1; i < num; i++)
        {
            if (30 + i * 300 / num <= 90)
            {
                float num2 = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num));
                float num3 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num));
                g.DrawLine(pt2: new PointF(num2 * (float)drawwidth / 200f, num3 * (float)drawwidth / 200f), pen: pen, pt1: pcenter);
            }
            else if (30 + i * 300 / num > 90 && 30 + i * 300 / num <= 180)
            {
                float num4 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - Math.PI / 2.0));
                float num5 = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - Math.PI / 2.0));
                g.DrawLine(pt2: new PointF(num4 * (float)drawwidth / 200f, num5 * (float)drawwidth / 200f), pen: pen, pt1: pcenter);
            }
            else if (30 + i * 300 / num > 180 && 30 + i * 300 / num <= 270)
            {
                float num6 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - Math.PI));
                float num7 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - Math.PI));
                g.DrawLine(pt2: new PointF(num6 * (float)drawwidth / 200f, num7 * (float)drawwidth / 200f), pen: pen, pt1: pcenter);
            }
            else if (30 + i * 300 / num > 270 && 30 + i * 300 / num <= 330)
            {
                float num8 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - 4.71238898038469));
                float num9 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)num - 4.71238898038469));
                g.DrawLine(pt2: new PointF(num8 * (float)drawwidth / 200f, num9 * (float)drawwidth / 200f), pen: pen, pt1: pcenter);
            }
        }
        Rectangle rect4 = new(35 * drawwidth / 200, 35 * drawwidth / 200, 130 * drawwidth / 200, 130 * drawwidth / 200);
        g.FillPie(brush3, rect4, 0f, 360f);
        for (int j = 1; j < mainmarkcount; j++)
        {
            if (30 + j * 300 / mainmarkcount <= 90)
            {
                float num10 = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount));
                int num11 = Convert.ToInt32(100.0 - 95.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount));
                float num12 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount));
                int num13 = Convert.ToInt32(100.0 + 95.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount));
                int num14 = num11 + 2;
                int num15 = num13 + Convert.ToInt32((double)(num11 - 100) * 0.1);
                PointF pointF = new(num10 * (float)drawwidth / 200f, num12 * (float)drawwidth / 200f);
                PointF item = new(Convert.ToSingle(num14) * (float)drawwidth / 200f, Convert.ToSingle(num15) * (float)drawwidth / 200f);
                list.Add(item);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF);
            }
            else if (30 + j * 300 / mainmarkcount > 90 && 30 + j * 300 / mainmarkcount <= 180)
            {
                float num16 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num17 = Convert.ToInt32(100.0 - 95.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                float num18 = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num19 = Convert.ToInt32(100.0 - 95.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num20 = num17 - Convert.ToInt32((double)num17 * 0.05);
                int num21 = num19 + Convert.ToInt32((double)(100 - num19) * 0.05);
                PointF pointF2 = new(num16 * (float)drawwidth / 200f, num18 * (float)drawwidth / 200f);
                PointF item2 = new(Convert.ToSingle(num20) * (float)drawwidth / 200f, Convert.ToSingle(num21) * (float)drawwidth / 200f);
                list.Add(item2);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF2);
            }
            else if (30 + j * 300 / mainmarkcount > 180 && 30 + j * 300 / mainmarkcount <= 270)
            {
                float num22 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num23 = Convert.ToInt32(100.0 + 95.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                float num24 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num25 = Convert.ToInt32(100.0 - 95.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num26 = num23 - Convert.ToInt32((double)(num23 - 100) * 0.26);
                int num27 = num25;
                PointF pointF3 = new(num22 * (float)drawwidth / 200f, num24 * (float)drawwidth / 200f);
                PointF item3 = new(Convert.ToSingle(num26) * (float)drawwidth / 200f, Convert.ToSingle(num27) * (float)drawwidth / 200f);
                list.Add(item3);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF3);
            }
            else if (30 + j * 300 / mainmarkcount > 270 && 30 + j * 300 / mainmarkcount <= 330)
            {
                float num28 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num29 = Convert.ToInt32(100.0 + 95.0 * Math.Cos(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                float num30 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num31 = Convert.ToInt32(100.0 + 95.0 * Math.Sin(Math.PI / 6.0 + (double)j * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num32 = num29 - Convert.ToInt32((double)(num29 - 100) * 0.24);
                int num33 = num31 - Convert.ToInt32((double)(num31 - 100) * 0.1);
                PointF pointF4 = new(num28 * (float)drawwidth / 200f, num30 * (float)drawwidth / 200f);
                num33 = num31 - Convert.ToInt32((double)(num31 - 100) * 0.2);
                PointF item4 = new(Convert.ToSingle(num32) * (float)drawwidth / 200f, Convert.ToSingle(num33) * (float)drawwidth / 200f);
                list.Add(item4);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF4);
            }
        }
        g.DrawLine(pen, pzero, pcenter);
        g.DrawLine(pen, pmax, pcenter);
        Rectangle rect5 = new(40 * drawwidth / 200, 40 * drawwidth / 200, 120 * drawwidth / 200, 120 * drawwidth / 200);
        g.FillPie(brush3, rect5, 0f, 360f);
        g.FillPie(rect: new Rectangle(95 * drawwidth / 200, 95 * drawwidth / 200, 10 * drawwidth / 200, 10 * drawwidth / 200), brush: Brushes.Black, startAngle: 0f, sweepAngle: 360f);
        if (initflag == 1)
        {
            g.DrawLine(Pens.Red, pcenter, pzero);
            initflag = 0;
        }
        new Rectangle(80 * drawwidth / 200, 110 * drawwidth / 200, 40 * drawwidth / 200, 25 * drawwidth / 200);
        new PointF(85 * drawwidth / 200, 113 * drawwidth / 200);
        SolidBrush brush4 = new(bqcolor);
        PointF point = new(57 * drawwidth / 200, pzero.Y + (float)(3 * drawwidth / 200));
        PointF point2 = new(133 * drawwidth / 200, pzero.Y + (float)(3 * drawwidth / 200));
        SolidBrush brush5 = new(txtcolor);
        g.DrawString(MinV.ToString(), font, brush5, point);
        g.DrawString(MaxV.ToString(), font, brush5, point2);
        for (int k = 0; k < list2.Count; k++)
        {
            g.DrawString(list2[k].ToString(), font, brush5, list[k]);
        }
        g.DrawString(point: new Point(90 * drawwidth / 200, 120 * drawwidth / 200), s: mark, font: font, brush: brush4);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 200;
                _width = 200;
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
            Bitmap bitmap = new(drawwidth, drawwidth);
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
                        pffnow.X = (100f - Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.5 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.2)
                    {
                        pffnow.X = (100f - Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.8 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.5)
                    {
                        pffnow.X = (100f + Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)drawwidth / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) > 0.8)
                    {
                        pffnow.X = (100f + Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)drawwidth / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)drawwidth / 200f;
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
        BSet1 bSet = new();
        if (varname != "")
        {
            bSet.VarName = varname;
            bSet.MaxValue = MaxV;
            bSet.MinValue = MinV;
            bSet.pt2 = pt2;
            bSet.MainMarkCount = mainmarkcount;
            bSet.OtherMarkCount = othermarkcount;
            bSet.Bgcolor = Bgcolor;
            bSet.BqColor = bqcolor;
            bSet.TxtColor = txtcolor;
            bSet.Mark = mark;
        }
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        bSet.OtherMarkCount = othermarkcount;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            mainmarkcount = bSet.MainMarkCount;
            othermarkcount = bSet.OtherMarkCount;
            Bgcolor = bSet.Bgcolor;
            bqcolor = bSet.BqColor;
            mark = bSet.Mark;
            pt2 = bSet.pt2;
            txtcolor = bSet.TxtColor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC3 bSaveC = new()
        {
            MaxV = MaxV,
            MinV = MinV,
            mainmarkcount = mainmarkcount,
            othermarkcount = othermarkcount,
            varname = varname,
            Bgcolor = Bgcolor,
            initflag = initflag,
            pffnow = pffnow,
            pcenter = pcenter,
            pzero = pzero,
            fontcolor = fontcolor
        };
        formatter.Serialize(memoryStream, bSaveC);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public override void Deserialize(byte[] data)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(data);
            BSaveC3 bSaveC = (BSaveC3)formatter.Deserialize(stream);
            stream.Close();
            MaxV = bSaveC.MaxV;
            MinV = bSaveC.MinV;
            mainmarkcount = bSaveC.mainmarkcount;
            othermarkcount = bSaveC.othermarkcount;
            varname = bSaveC.varname;
            Bgcolor = bSaveC.Bgcolor;
            initflag = bSaveC.initflag;
            pffnow = bSaveC.pffnow;
            pcenter = bSaveC.pcenter;
            pzero = bSaveC.pzero;
            fontcolor = bSaveC.fontcolor;
        }
        catch
        {
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
