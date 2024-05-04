using System;
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
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap7 : CPixieControl
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

    private Color markcolor = Color.Black;

    private int initflag = 1;

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(200f, 200f);

    private PointF pzero = new(110f, Convert.ToSingle(200.0 + 180.0 * Math.Cos(Math.PI / 6.0)));

    private PointF pmax = new(290f, Convert.ToSingle(200.0 + 180.0 * Math.Cos(Math.PI / 6.0)));

    private Color fontcolor = Color.Blue;

    private float value;

    [DisplayName("小数位")]
    [Category("杂项")]
    [Description("表盘刻度显示数值小数精确位数。")]
    [DHMICtrlProperty]
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

    [Category("杂项")]
    [Description("仪表量程上限。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("量程上限")]
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
    [ReadOnly(false)]
    [DHMICtrlProperty]
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
    [Description("表盘主刻度线数量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("主刻度数")]
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
    [Category("杂项")]
    [Description("表盘主刻度之间副刻度线数量。")]
    [DisplayName("副刻度数")]
    [DHMICtrlProperty]
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
    [Category("杂项")]
    [Description("控件绑定变量名。")]
    [DisplayName("绑定变量")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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
    [Category("杂项")]
    [Description("表盘背景色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DisplayName("正常范围颜色")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("当变量值处于配置的正常值范围内时指针指向的刻度的背景色。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("当变量值处于配置的警告值范围内时指针指向的刻度的背景色。")]
    [Category("杂项")]
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

    [ReadOnly(false)]
    [DisplayName("事故范围颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DHMICtrlProperty]
    [DisplayName("正常范围起始值")]
    [Category("杂项")]
    [Description("正常范围的起始值。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DisplayName("正常范围结束值")]
    [DHMICtrlProperty]
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

    [DisplayName("警告范围1起始值")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("警告范围的起始值。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DisplayName("警告范围2起始值")]
    [Description("警告范围的起始值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("警告范围1结束值")]
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

    [Description("警告范围的结束值。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("警告范围2结束值")]
    [Category("杂项")]
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
    [Description("事故范围的起始值。")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DHMICtrlProperty]
    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("事故范围2起始值")]
    [Description("事故范围的起始值。")]
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

    [Description("事故范围的结束值。")]
    [Category("杂项")]
    [DisplayName("事故范围1结束值")]
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

    [DisplayName("事故范围2结束值")]
    [Category("杂项")]
    [Description("事故范围的结束值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DisplayName("文本")]
    [Description("表盘上显示的文本。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Category("杂项")]
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

    [Description("表盘上刻度值颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [ReadOnly(false)]
    [Description("刻度线颜色。")]
    [DHMICtrlProperty]
    [DisplayName("刻度线颜色")]
    [Category("杂项")]
    public Color MarkLineColor
    {
        get
        {
            return markcolor;
        }
        set
        {
            markcolor = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("文本颜色")]
    [Description("表盘上显示的文本的字体颜色。")]
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

    [DisplayName("当前值")]
    [Description("仪表绑定变量的当前值。")]
    [ReadOnly(false)]
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
                if (!lowalertflag && this.LowAlert != null)
                {
                    this.LowAlert(this, null);
                    lowalertflag = true;
                }
            }
            else
            {
                lowalertflag = false;
            }
            if (Convert.ToSingle(value) > num2 && Convert.ToSingle(value) < num4)
            {
                if (!highalertflag && this.HighAlert != null)
                {
                    this.HighAlert(this, null);
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

    public drawbitmap7()
    {
    }

    protected drawbitmap7(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap7 info");
        }
        drawbitmap7 obj = new();
        FieldInfo[] fields = typeof(drawbitmap7).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap7))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap7))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap7) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap7 drawbitmap10 = (drawbitmap7)base.Copy();
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Arial");
        Font font = new(family, 5 * _width / 200);
        int num = mainmarkcount * othermarkcount;
        Rectangle rect = new(0, 0, 200 * _width / 200, 140 * _height / 140);
        SolidBrush brush = new(Bgcolor);
        SolidBrush brush2 = new(Errorcolor);
        SolidBrush brush3 = new(nmlcolor);
        SolidBrush brush4 = new(warncolor);
        SolidBrush brush5 = new(txtcolor);
        SolidBrush brush6 = new(Color.DarkGreen);
        Pen pen = new(markcolor);
        g.FillRectangle(brush, rect);
        new Rectangle(30 * _width / 200, 30 * _height / 140, 140 * _width / 200, 30 * _height / 140);
        Rectangle rect2 = new(Convert.ToInt32(30f + 140f * ((errorsta1 - MinV) / (MaxV - MinV))) * _width / 200, 30 * _height / 140, Convert.ToInt32(140f * ((errorend1 - errorsta1) / (MaxV - MinV))) * _width / 200, 30 * _height / 140);
        Rectangle rect3 = new(Convert.ToInt32(30f + 140f * ((warnsta1 - MinV) / (MaxV - MinV))) * _width / 200, 30 * _height / 140, Convert.ToInt32(140f * ((warnend1 - warnsta1) / (MaxV - MinV))) * _width / 200, 30 * _height / 140);
        Rectangle rect4 = new(Convert.ToInt32(30f + 140f * ((nmlsta - MinV) / (MaxV - MinV))) * _width / 200, 30 * _height / 140, Convert.ToInt32(140f * ((nmlend - nmlsta) / (MaxV - MinV))) * _width / 200, 30 * _height / 140);
        Rectangle rect5 = new(Convert.ToInt32(30f + 140f * ((warnsta2 - MinV) / (MaxV - MinV))) * _width / 200, 30 * _height / 140, Convert.ToInt32(140f * ((warnend2 - warnsta2) / (MaxV - MinV))) * _width / 200, 30 * _height / 140);
        Rectangle rect6 = new(Convert.ToInt32(30f + 140f * ((errorsta2 - MinV) / (MaxV - MinV))) * _width / 200, 30 * _height / 140, Convert.ToInt32(140f * ((errorend2 - errorsta2) / (MaxV - MinV))) * _width / 200, 30 * _height / 140);
        g.FillRectangle(brush2, rect2);
        g.FillRectangle(brush2, rect6);
        g.FillRectangle(brush4, rect3);
        g.FillRectangle(brush4, rect5);
        g.FillRectangle(brush3, rect4);
        for (int i = 0; i <= mainmarkcount; i++)
        {
            g.DrawLine(pen, (30 + i * 140 / mainmarkcount) * _width / 200, 30 * _height / 140, (30 + i * 140 / mainmarkcount) * _width / 200, 60 * _height / 140);
            g.DrawString(point: new PointF((30 + i * (140 / mainmarkcount) - 4) * _width / 200, 64 * _height / 140), s: Math.Round(Convert.ToSingle(MinV) + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2).ToString(), font: font, brush: brush5);
        }
        for (int j = 0; j <= num; j++)
        {
            g.DrawLine(pen, (30 + j * 140 / num) * _width / 200, 30 * _height / 140, (30 + j * 140 / num) * _width / 200, 44 * _height / 140);
        }
        Pen pen2 = new(Color.DarkGray, 3f);
        g.DrawLine(pen2, 30 * _width / 200, 100 * _height / 140, 170 * _width / 200, 100 * _height / 140);
        if (initflag == 1)
        {
            Point point2 = new(30 * _width / 200, 60 * _height / 140);
            Point point3 = new(26 * _width / 200, 96 * _height / 140);
            Point point4 = new(34 * _width / 200, 96 * _height / 140);
            g.FillPolygon(brush6, new PointF[3] { point2, point3, point4 });
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 140;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new(Color.DarkGreen);
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
                    Point point = new(Convert.ToInt32(30f + 140f * (Value - MinV) / (MaxV - MinV)) * _width / 200, 60 * _height / 140);
                    Point point2 = new(Convert.ToInt32(30f + 140f * (Value - MinV) / (MaxV - MinV)) * _width / 200 - 4 * _width / 200, 96 * _height / 140);
                    Point point3 = new(Convert.ToInt32(30f + 140f * (Value - MinV) / (MaxV - MinV)) * _width / 200 + 4 * _width / 200, 96 * _height / 140);
                    graphics.FillPolygon(brush, new PointF[3] { point, point2, point3 });
                }
                else if (Value < MinV)
                {
                    initflag = 0;
                    Paint(graphics);
                    Point point4 = new(30 * _width / 200, 60 * _height / 140);
                    Point point5 = new(26 * _width / 200, 96 * _height / 140);
                    Point point6 = new(34 * _width / 200, 96 * _height / 140);
                    graphics.FillPolygon(brush, new PointF[3] { point4, point5, point6 });
                }
                else if (Value > MaxV)
                {
                    initflag = 0;
                    Paint(graphics);
                    Point point7 = new(170 * _width / 200, 60 * _height / 140);
                    Point point8 = new(166 * _width / 200, 96 * _height / 140);
                    Point point9 = new(174 * _width / 200, 96 * _height / 140);
                    graphics.FillPolygon(brush, new PointF[3] { point7, point8, point9 });
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
        BSet bSet = new()
        {
            VarName = varname,
            MaxValue = MaxV,
            MinValue = MinV,
            MainMarkCount = mainmarkcount,
            OtherMarkCount = othermarkcount,
            Bgcolor = Bgcolor,
            pt2 = pt2,
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
            MarkColor = markcolor,
            BiaoqianColor = bqcolor,
            TxtColor = txtcolor
        };
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        bSet.lbHide = true;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            pt2 = bSet.pt2;
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
            Mark = bSet.Mark;
            bqcolor = bSet.BiaoqianColor;
            txtcolor = bSet.TxtColor;
            markcolor = bSet.MarkColor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC7 bSaveC = new()
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
            BSaveC7 bSaveC = (BSaveC7)formatter.Deserialize(stream);
            stream.Close();
            MaxV = bSaveC.MaxV;
            MinV = bSaveC.MinV;
            mainmarkcount = bSaveC.mainmarkcount;
            othermarkcount = bSaveC.othermarkcount;
            varname = bSaveC.varname;
            Bgcolor = bSaveC.Bgcolor;
            nmlcolor = bSaveC.nmlcolor;
            warncolor = bSaveC.warncolor;
            Errorcolor = bSaveC.Errorcolor;
            nmlsta = bSaveC.nmlsta;
            nmlend = bSaveC.nmlend;
            warnsta1 = bSaveC.warnsta1;
            warnsta2 = bSaveC.warnsta2;
            warnend1 = bSaveC.warnend1;
            warnend2 = bSaveC.warnend2;
            errorsta1 = bSaveC.errorsta1;
            errorsta2 = bSaveC.errorsta2;
            errorend1 = bSaveC.errorend1;
            errorend2 = bSaveC.errorend2;
            Mark = bSaveC.Mark;
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
