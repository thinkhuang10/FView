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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap2 : CPixieControl
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

    private int initflag = 1;

    private int drawwidth = 200;

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(10f, 190f);

    private PointF pzero = new(190f, 190f);

    private Color fontcolor = Color.Blue;

    private float value;

    [Description("表盘刻度显示数值小数精确位数。")]
    [DisplayName("小数位")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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
    [DisplayName("量程上限")]
    [Category("杂项")]
    [Description("仪表量程上限。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DisplayName("量程下限")]
    [DHMICtrlProperty]
    [Description("仪表量程下限。")]
    [ReadOnly(false)]
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

    [DisplayName("主刻度数")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [Category("杂项")]
    [Description("表盘主刻度之间副刻度线数量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("控件绑定变量名。")]
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
    [Description("表盘背景色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [ReadOnly(false)]
    [Description("当变量值处于配置的正常值范围内时指针指向的刻度的背景色。")]
    [DisplayName("正常范围颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
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
    [Description("当变量值处于配置的警告值范围内时指针指向的刻度的背景色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("警告范围颜色")]
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

    [Category("杂项")]
    [DisplayName("事故范围颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("当变量值处于配置的事故值范围内时指针指向的刻度的背景色。")]
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

    [Category("杂项")]
    [Description("正常范围的起始值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [Description("正常范围的结束值。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("正常范围结束值")]
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
    [Category("杂项")]
    [Description("警告范围的起始值。")]
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

    [DHMICtrlProperty]
    [DisplayName("警告范围1结束值")]
    [Category("杂项")]
    [Description("警告范围的结束值。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [DisplayName("警告范围2结束值")]
    [Category("杂项")]
    [Description("警告范围的结束值。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DisplayName("事故范围1起始值")]
    [DHMICtrlProperty]
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

    [DisplayName("事故范围2起始值")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("事故范围的起始值。")]
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

    [Category("杂项")]
    [DisplayName("事故范围1结束值")]
    [Description("事故范围的结束值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("事故范围2结束值")]
    [Description("事故范围的结束值。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [DisplayName("文本")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘上显示的文本。")]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘上刻度值颜色。")]
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

    [DisplayName("文本颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘上显示的文本的字体颜色。")]
    [ReadOnly(false)]
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

    [Description("仪表绑定变量的当前值。")]
    [ReadOnly(false)]
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

    public drawbitmap2()
    {
    }

    protected drawbitmap2(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap2 obj = new();
        FieldInfo[] fields = typeof(drawbitmap2).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap2))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap2), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap2))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap2), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap2) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap2 drawbitmap10 = (drawbitmap2)base.Copy();
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
        drawbitmap10.fontcolor = fontcolor;
        drawbitmap10.txtcolor = txtcolor;
        drawbitmap10.bqcolor = bqcolor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        pcenter = new PointF(10 * drawwidth / 200, 190 * drawwidth / 200);
        pzero = new PointF(190 * drawwidth / 200, 190 * drawwidth / 200);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        List<PointF> list = new();
        List<float> list2 = new();
        new SolidBrush(fontcolor);
        SolidBrush brush = new(Errorcolor);
        SolidBrush brush2 = new(warncolor);
        SolidBrush brush3 = new(nmlcolor);
        SolidBrush brush4 = new(Bgcolor);
        FontFamily family = new("Arial");
        Font font = new(family, 8 * drawwidth / 200);
        PointF pointF = new(10 * drawwidth / 200, 10 * drawwidth / 200);
        g.DrawLine(Pens.Black, pcenter, pzero);
        g.DrawLine(Pens.Black, pcenter, pointF);
        Rectangle rect = new(-170 * drawwidth / 200, 10 * drawwidth / 200, 360 * drawwidth / 200, 360 * drawwidth / 200);
        Rectangle rect2 = new(-160 * drawwidth / 200, 20 * drawwidth / 200, 340 * drawwidth / 200, 340 * drawwidth / 200);
        g.DrawPie(Pens.Black, rect, 270f, 90f);
        g.DrawPie(Pens.Black, rect2, 270f, 90f);
        g.FillPie(brush, rect, 270f + 90f * (MaxV - errorend1) / (MaxV - MinV), 90f * (MaxV - errorsta1) / (MaxV - MinV) + 270f - (270f + 90f * (MaxV - errorend1) / (MaxV - MinV)));
        g.FillPie(brush2, rect, 270f + 90f * (MaxV - warnend1) / (MaxV - MinV), 90f * (MaxV - warnsta1) / (MaxV - MinV) + 270f - (270f + 90f * (MaxV - warnend1) / (MaxV - MinV)));
        g.FillPie(brush3, rect, 270f + 90f * (MaxV - nmlend) / (MaxV - MinV), 90f * (MaxV - nmlsta) / (MaxV - MinV) + 270f - (270f + 90f * (MaxV - nmlend) / (MaxV - MinV)));
        g.FillPie(brush2, rect, 270f + 90f * (MaxV - warnend2) / (MaxV - MinV), 90f * (MaxV - warnsta2) / (MaxV - MinV) + 270f - (270f + 90f * (MaxV - warnend2) / (MaxV - MinV)));
        g.FillPie(brush, rect, 270f + 90f * (MaxV - errorend2) / (MaxV - MinV), 90f * (MaxV - errorsta2) / (MaxV - MinV) + 270f - (270f + 90f * (MaxV - errorend2) / (MaxV - MinV)));
        for (int num = mainmarkcount - 1; num >= 1; num--)
        {
            float num2 = Convert.ToSingle(10.0 + 180.0 * Math.Sin((double)num * (Math.PI / 2.0) / (double)mainmarkcount));
            int num3 = Convert.ToInt32(10.0 + 160.0 * Math.Sin((double)num * (Math.PI / 2.0) / (double)mainmarkcount));
            float num4 = Convert.ToSingle(190.0 - 180.0 * Math.Cos((double)num * (Math.PI / 2.0) / (double)mainmarkcount));
            int num5 = Convert.ToInt32(190.0 - 160.0 * Math.Cos((double)num * (Math.PI / 2.0) / (double)mainmarkcount));
            int num6 = num3 - Convert.ToInt32((double)(num3 - 10) * 0.08);
            int num7 = num5;
            PointF pointF2 = new(num2 * (float)drawwidth / 200f, num4 * (float)drawwidth / 200f);
            PointF item = new(Convert.ToSingle(num6) * (float)drawwidth / 200f, Convert.ToSingle(num7) * (float)drawwidth / 200f);
            list.Add(item);
            list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)(mainmarkcount - num)), pt2));
            g.DrawLine(Pens.Black, pcenter, pointF2);
        }
        g.FillPie(brush4, rect2, 270f, 90f);
        g.DrawPie(Pens.Black, rect2, 270f, 90f);
        if (initflag == 1)
        {
            g.DrawLine(Pens.Red, pcenter, pzero);
            initflag = 0;
        }
        new Rectangle(80 * drawwidth / 200, 110 * drawwidth / 200, 40 * drawwidth / 200, 25 * drawwidth / 200);
        PointF point = new(100 * drawwidth / 200, 100 * drawwidth / 200);
        SolidBrush brush5 = new(bqcolor);
        g.DrawString(Mark, font, brush5, point);
        PointF point2 = new(165 * drawwidth / 200, 178 * drawwidth / 200);
        PointF point3 = new(10 * drawwidth / 200, 30 * drawwidth / 200);
        SolidBrush brush6 = new(txtcolor);
        g.DrawString(MinV.ToString(), font, brush6, point2);
        g.DrawString(MaxV.ToString(), font, brush6, point3);
        for (int i = 0; i < list2.Count; i++)
        {
            g.DrawString(list2[i].ToString(), font, brush6, list[i]);
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
                if (Value >= MinV && Value <= MaxV)
                {
                    initflag = 0;
                    Paint(graphics);
                    pffnow.X = (10f + Convert.ToSingle(180.0 * Math.Sin(Math.PI / 2.0 * (double)(MaxV - Value) / (double)(MaxV - MinV)))) * (float)drawwidth / 200f;
                    pffnow.Y = (190f - Convert.ToSingle(180.0 * Math.Cos(Math.PI / 2.0 * (double)(MaxV - Value) / (double)(MaxV - MinV)))) * (float)drawwidth / 200f;
                    graphics.DrawLine(Pens.Red, pcenter, pffnow);
                }
                else if (Value > MaxV)
                {
                    initflag = 0;
                    Paint(graphics);
                    graphics.DrawLine(pt2: new PointF(10 * drawwidth / 200, 10 * drawwidth / 200), pen: Pens.Red, pt1: pcenter);
                }
                else if (Value < MinV)
                {
                    initflag = 0;
                    Paint(graphics);
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
            bSet.pt2 = pt2;
            bSet.errorsta2 = errorsta2;
            bSet.errorend1 = errorend1;
            bSet.errorend2 = errorend2;
            bSet.Mark = Mark;
            bSet.BiaoqianColor = bqcolor;
            bSet.TxtColor = txtcolor;
        }
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        bSet.markHide = true;
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
            pt2 = bSet.pt2;
            warnsta1 = bSet.warnsta1;
            warnsta2 = bSet.warnsta2;
            warnend1 = bSet.warnend1;
            warnend2 = bSet.warnend2;
            errorsta1 = bSet.errorsta1;
            errorsta2 = bSet.errorsta2;
            errorend1 = bSet.errorend1;
            errorend2 = bSet.errorend2;
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
        SaveC2 saveC = new()
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
        formatter.Serialize(memoryStream, saveC);
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
            SaveC2 saveC = (SaveC2)formatter.Deserialize(stream);
            stream.Close();
            MaxV = saveC.MaxV;
            MinV = saveC.MinV;
            mainmarkcount = saveC.mainmarkcount;
            othermarkcount = saveC.othermarkcount;
            varname = saveC.varname;
            Bgcolor = saveC.Bgcolor;
            nmlcolor = saveC.nmlcolor;
            warncolor = saveC.warncolor;
            Errorcolor = saveC.Errorcolor;
            nmlsta = saveC.nmlsta;
            nmlend = saveC.nmlend;
            warnsta1 = saveC.warnsta1;
            warnsta2 = saveC.warnsta2;
            warnend1 = saveC.warnend1;
            warnend2 = saveC.warnend2;
            errorsta1 = saveC.errorsta1;
            errorsta2 = saveC.errorsta2;
            errorend1 = saveC.errorend1;
            errorend2 = saveC.errorend2;
            Mark = saveC.Mark;
            initflag = saveC.initflag;
            pffnow = saveC.pffnow;
            pcenter = saveC.pcenter;
            pzero = saveC.pzero;
            fontcolor = saveC.fontcolor;
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
