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
public class drawbitmap5 : CPixieControl
{
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

    private int initflag = 1;

    private int _width = 200;

    private int _height = 115;

    private string mark = "仪表";

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(100f, 100f);

    private PointF pzero = new(5f, 99f);

    private PointF pmax = new(195f, 99f);

    private Color fontcolor = Color.Blue;

    private float value;

    [DisplayName("小数位")]
    [DHMICtrlProperty]
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
        }
    }

    [Description("仪表量程下限。")]
    [DisplayName("量程下限")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘主刻度线数量。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘主刻度之间副刻度线数量。")]
    [ReadOnly(false)]
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
    [DisplayName("绑定变量")]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
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
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘背景色。")]
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

    [Description("表盘刻度线颜色。")]
    [ReadOnly(false)]
    [DisplayName("表盘刻度线颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    public Color MarkColor
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

    [ReadOnly(false)]
    [DisplayName("表盘刻度值颜色")]
    [DHMICtrlProperty]
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("文本")]
    [Description("表盘显示文本。")]
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

    [ReadOnly(false)]
    [DisplayName("文本颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘显示的文本的颜色。")]
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
    [DisplayName("当前值")]
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
            if (this.value != value)
            {
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public drawbitmap5()
    {
    }

    protected drawbitmap5(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap5 info");
        }
        drawbitmap5 obj = new();
        FieldInfo[] fields = typeof(drawbitmap5).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap5))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap5), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap5))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap5), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap5) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap5 drawbitmap10 = (drawbitmap5)base.Copy();
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
        pcenter = new PointF(100 * _width / 200, 100 * _height / 115);
        pzero = new PointF(5 * _width / 200, 99 * _height / 115);
        pmax = new PointF(195 * _width / 200, 99 * _height / 115);
        Rectangle rect = new(_width / 200, _height / 115, 200 * _width / 200, 115 * _height / 115);
        SolidBrush brush = new(Bgcolor);
        g.FillRectangle(brush, rect);
        new SolidBrush(fontcolor);
        new SolidBrush(Markcolor);
        Pen pen = new(Markcolor);
        FontFamily family = new("Arial");
        Font font = new(family, 8 * _width / 200);
        int num = mainmarkcount * othermarkcount;
        List<PointF> list = new();
        List<float> list2 = new();
        for (int i = 1; i < num; i++)
        {
            if (90 + i * 180 / num > 90 && 90 + i * 180 / num <= 180)
            {
                float num2 = Convert.ToSingle(100.0 - 90.0 * Math.Cos(Math.PI / 2.0 + (double)i * Math.PI / (double)num - Math.PI / 2.0));
                float num3 = Convert.ToSingle(100.0 - 90.0 * Math.Sin(Math.PI / 2.0 + (double)i * Math.PI / (double)num - Math.PI / 2.0));
                g.DrawLine(pt2: new PointF(num2 * (float)_width / 200f, num3 * (float)_height / 115f), pen: pen, pt1: pcenter);
            }
            else if (90 + i * 180 / num > 180 && 90 + i * 180 / num <= 270)
            {
                float num4 = Convert.ToSingle(100.0 + 90.0 * Math.Sin(Math.PI / 2.0 + (double)i * Math.PI / (double)num - Math.PI));
                float num5 = Convert.ToSingle(100.0 - 90.0 * Math.Cos(Math.PI / 2.0 + (double)i * Math.PI / (double)num - Math.PI));
                g.DrawLine(pt2: new PointF(num4 * (float)_width / 200f, num5 * (float)_height / 115f), pen: pen, pt1: pcenter);
            }
        }
        for (int j = 1; j < mainmarkcount; j++)
        {
            if (90 + j * 180 / mainmarkcount > 90 && 90 + j * 180 / mainmarkcount <= 180)
            {
                float num6 = Convert.ToSingle(100.0 - 95.0 * Math.Cos(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI / 2.0));
                int num7 = Convert.ToInt32(100.0 - 85.0 * Math.Cos(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI / 2.0));
                float num8 = Convert.ToSingle(100.0 - 95.0 * Math.Sin(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI / 2.0));
                int num9 = Convert.ToInt32(100.0 - 85.0 * Math.Sin(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI / 2.0));
                int num10 = num7 - Convert.ToInt32((double)num7 * 0.05);
                int num11 = num9 + Convert.ToInt32((double)(100 - num9) * 0.05);
                PointF pointF = new(num6 * (float)_width / 200f, num8 * (float)_height / 115f);
                PointF item = new(Convert.ToSingle(num10) * (float)_width / 200f - (float)(_width / 20000), Convert.ToSingle(num11) * (float)_height / 115f);
                list.Add(item);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF);
            }
            else if (90 + j * 180 / mainmarkcount > 180 && 90 + j * 180 / mainmarkcount <= 270)
            {
                float num12 = Convert.ToSingle(100.0 + 95.0 * Math.Sin(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI));
                int num13 = Convert.ToInt32(100.0 + 85.0 * Math.Sin(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI));
                float num14 = Convert.ToSingle(100.0 - 95.0 * Math.Cos(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI));
                int num15 = Convert.ToInt32(100.0 - 85.0 * Math.Cos(Math.PI / 2.0 + (double)j * Math.PI / (double)mainmarkcount - Math.PI));
                int num16 = num13 - Convert.ToInt32((double)(num13 - 100) * 0.26);
                int num17 = num15;
                PointF pointF2 = new(num12 * (float)_width / 200f, num14 * (float)_height / 115f);
                PointF item2 = new(Convert.ToSingle(num16) * (float)_width / 200f - (float)(_width / 20000), Convert.ToSingle(num17) * (float)_height / 115f);
                list.Add(item2);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)j), pt2));
                g.DrawLine(pen, pcenter, pointF2);
            }
        }
        g.DrawLine(pen, pzero, pcenter);
        g.DrawLine(pen, pmax, pcenter);
        Rectangle rect2 = new(15 * _width / 200, 15 * _height / 115, 170 * _width / 200, 170 * _height / 115);
        g.FillPie(brush, rect2, 175f, 190f);
        g.FillPie(rect: new Rectangle(95 * _width / 200, 95 * _height / 115, 10 * _width / 200, 10 * _height / 115), brush: Brushes.Black, startAngle: 0f, sweepAngle: 360f);
        if (initflag == 1)
        {
            g.DrawLine(Pens.Blue, pcenter, pzero);
            initflag = 0;
        }
        new Rectangle(80 * _width / 200, 110 * _height / 115, 40 * _width / 200, 25 * _height / 115);
        new PointF(85 * _width / 200, 113 * _height / 115);
        SolidBrush brush2 = new(bqcolor);
        PointF point = new(10 * _width / 200, pzero.Y);
        PointF point2 = new(170 * _width / 200, pzero.Y);
        SolidBrush brush3 = new(txtcolor);
        g.DrawString(MinV.ToString(), font, brush3, point);
        g.DrawString(MaxV.ToString(), font, brush3, point2);
        for (int k = 0; k < list2.Count; k++)
        {
            g.DrawString(list2[k].ToString(), font, brush3, list[k]);
        }
        g.DrawString(point: new Point(90 * _width / 200, 60 * _height / 115), s: mark, font: font, brush: brush2);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 115;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(Convert.ToInt32((double)_width * 1.0), _height);
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
                    if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.5 && (Value - MinV) / (MaxV - MinV) >= 0f)
                    {
                        pffnow.X = (100f - Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI))) * (float)_width / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI))) * (float)_height / 115f;
                    }
                    else if ((Value - MinV) / (MaxV - MinV) <= 1f && (double)((Value - MinV) / (MaxV - MinV)) >= 0.5)
                    {
                        pffnow.X = (100f + Convert.ToSingle(90.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI - Math.PI / 2.0))) * (float)_width / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(90.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI - Math.PI / 2.0))) * (float)_height / 115f;
                    }
                    graphics.DrawLine(Pens.Blue, pcenter, pffnow);
                }
                else if (Value > MaxV)
                {
                    graphics.DrawLine(Pens.Blue, pcenter, pmax);
                }
                else if (Value < MinV)
                {
                    graphics.DrawLine(Pens.Blue, pcenter, pzero);
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
        BSet1 bSet = new()
        {
            VarName = varname,
            MaxValue = MaxV,
            MinValue = MinV,
            MainMarkCount = mainmarkcount,
            OtherMarkCount = othermarkcount,
            Bgcolor = Bgcolor,
            pt2 = pt2,
            BqColor = bqcolor,
            TxtColor = txtcolor,
            Mark = mark
        };
        bSet.TxtColor = txtcolor;
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            pt2 = bSet.pt2;
            mark = bSet.Mark;
            txtcolor = bSet.TxtColor;
            mainmarkcount = bSet.MainMarkCount;
            othermarkcount = bSet.OtherMarkCount;
            Bgcolor = bSet.Bgcolor;
            bqcolor = bSet.BqColor;
            txtcolor = bSet.TxtColor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC5 bSaveC = new()
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
            BSaveC5 bSaveC = (BSaveC5)formatter.Deserialize(stream);
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
