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
public class drawbitmap4 : CPixieControl
{
    private int pt2 = 1;

    private float MaxV = 300f;

    private float MinV;

    private int mainmarkcount = 10;

    private int othermarkcount = 2;

    private string varname = "";

    private Color Bgcolor = Color.White;

    private Color Markcolor = Color.Black;

    private Color ValueColor = Color.Blue;

    private Color txtcolor = Color.White;

    private int _width = 200;

    private int _height = 200;

    private int initflag = 1;

    private PointF pffnow = default(PointF);

    private PointF pcenter = new(100f, 100f);

    private PointF pzero = new(65f, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)));

    private PointF pmax = new(135f, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)));

    private PointF pmid = new(100f, 30f);

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

    [Description("仪表量程上限。")]
    [DisplayName("量程上限")]
    [Category("杂项")]
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

    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("控件绑定变量名。")]
    [DisplayName("绑定变量")]
    [Category("杂项")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("表盘背景色。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("表盘刻度线颜色。")]
    [DisplayName("表盘刻度线颜色")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("显示值颜色")]
    [Category("杂项")]
    [Description("表盘显示的绑定变量值的字体颜色。")]
    public Color ValueTextColor
    {
        get
        {
            return ValueColor;
        }
        set
        {
            ValueColor = value;
        }
    }

    [Description("表盘刻度值颜色。")]
    [ReadOnly(false)]
    [DisplayName("表盘刻度值颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [Description("仪表绑定变量的当前值。")]
    [ReadOnly(false)]
    [Category("杂项")]
    [DisplayName("当前值")]
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

    public drawbitmap4()
    {
    }

    protected drawbitmap4(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap4 info");
        }
        drawbitmap4 obj = new();
        FieldInfo[] fields = typeof(drawbitmap4).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap4))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap4))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap4) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap4 drawbitmap10 = (drawbitmap4)base.Copy();
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
        drawbitmap10.pt2 = pt2;
        drawbitmap10.ValueColor = ValueColor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        pcenter = new PointF(100 * _width / 200, 100 * _height / 200);
        pzero = new PointF(65 * _width / 200, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)) * (float)_height / 200f);
        pmax = new PointF(135 * _width / 200, Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0)) * (float)_height / 200f);
        pmid = new PointF(100 * _width / 200, 30 * _height / 200);
        new SolidBrush(fontcolor);
        new SolidBrush(Markcolor);
        Pen pen = new(Markcolor);
        FontFamily family = new("Arial");
        Font font = new(family, 10f);
        List<PointF> list = new();
        List<float> list2 = new();
        SolidBrush brush = new(Color.Black);
        SolidBrush brush2 = new(Bgcolor);
        Rectangle rect = new(5 * _width / 200, 5 * _height / 200, 190 * _width / 200, 190 * _height / 200);
        g.DrawPie(Pens.Black, rect, 0f, 360f);
        g.FillPie(brush, rect, 0f, 360f);
        Rectangle rect2 = new(30 * _width / 200, 30 * _height / 200, 140 * _width / 200, 140 * _height / 200);
        g.DrawPie(Pens.Black, rect2, 0f, 360f);
        g.FillPie(brush2, rect2, 0f, 360f);
        for (int i = 1; i < mainmarkcount; i++)
        {
            if (30 + i * 300 / mainmarkcount <= 90)
            {
                float num = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num2 = Convert.ToInt32(100.0 - 95.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                float num3 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num4 = Convert.ToInt32(100.0 + 95.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount));
                int num5 = num2 + 2;
                int num6 = num4 + Convert.ToInt32((double)(num2 - 100) * 0.1);
                PointF pointF = new(num * (float)_width / 200f, num3 * (float)_height / 200f);
                PointF item = new(Convert.ToSingle(num5) * (float)_width / 200f, Convert.ToSingle(num6) * (float)_height / 200f);
                list.Add(item);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(pen, pcenter, pointF);
            }
            else if (30 + i * 300 / mainmarkcount > 90 && 30 + i * 300 / mainmarkcount <= 180)
            {
                float num7 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num8 = Convert.ToInt32(100.0 - 95.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                float num9 = Convert.ToSingle(100.0 - 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num10 = Convert.ToInt32(100.0 - 95.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI / 2.0));
                int num11 = num8 - Convert.ToInt32((double)num8 * 0.05);
                int num12 = num10 + Convert.ToInt32((double)(100 - num10) * 0.05);
                PointF pointF2 = new(num7 * (float)_width / 200f, num9 * (float)_height / 200f);
                PointF item2 = new(Convert.ToSingle(num11) * (float)_width / 200f, Convert.ToSingle(num12) * (float)_height / 200f);
                list.Add(item2);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(pen, pcenter, pointF2);
            }
            else if (30 + i * 300 / mainmarkcount > 180 && 30 + i * 300 / mainmarkcount <= 270)
            {
                float num13 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num14 = Convert.ToInt32(100.0 + 95.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                float num15 = Convert.ToSingle(100.0 - 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num16 = Convert.ToInt32(100.0 - 95.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - Math.PI));
                int num17 = num14 - Convert.ToInt32((double)(num14 - 100) * 0.26);
                int num18 = num16;
                PointF pointF3 = new(num13 * (float)_width / 200f, num15 * (float)_height / 200f);
                PointF item3 = new(Convert.ToSingle(num17) * (float)_width / 200f, Convert.ToSingle(num18) * (float)_height / 200f);
                list.Add(item3);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(pen, pcenter, pointF3);
            }
            else if (30 + i * 300 / mainmarkcount > 270 && 30 + i * 300 / mainmarkcount <= 330)
            {
                float num19 = Convert.ToSingle(100.0 + 70.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num20 = Convert.ToInt32(100.0 + 95.0 * Math.Cos(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                float num21 = Convert.ToSingle(100.0 + 70.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num22 = Convert.ToInt32(100.0 + 95.0 * Math.Sin(Math.PI / 6.0 + (double)i * 5.235987755982989 / (double)mainmarkcount - 4.71238898038469));
                int num23 = num20 - Convert.ToInt32((double)(num20 - 100) * 0.24);
                int num24 = num22 - Convert.ToInt32((double)(num22 - 100) * 0.1);
                PointF pointF4 = new(num19 * (float)_width / 200f, num21 * (float)_height / 200f);
                num24 = num22 - Convert.ToInt32((double)(num22 - 100) * 0.2);
                PointF item4 = new(Convert.ToSingle(num23) * (float)_width / 200f, Convert.ToSingle(num24) * (float)_height / 200f);
                list.Add(item4);
                list2.Add((float)Math.Round(MinV + Convert.ToSingle((MaxV - MinV) / (float)mainmarkcount * (float)i), pt2));
                g.DrawLine(pen, pcenter, pointF4);
            }
        }
        g.DrawLine(pen, pzero, pcenter);
        g.DrawLine(pen, pmax, pcenter);
        Rectangle rect3 = new(40 * _width / 200, 40 * _height / 200, 120 * _width / 200, 120 * _height / 200);
        g.FillPie(brush2, rect3, 0f, 360f);
        g.FillPie(rect: new Rectangle(95 * _width / 200, 95 * _height / 200, 10 * _width / 200, 10 * _height / 200), brush: Brushes.Black, startAngle: 0f, sweepAngle: 360f);
        new Rectangle(80 * _width / 200, 110 * _height / 200, 40 * _width / 200, 25 * _height / 200);
        new PointF(85 * _width / 200, 113 * _height / 200);
        PointF point = new(57 * _width / 200, pzero.Y + (float)(3 * _height / 200));
        PointF point2 = new(133 * _width / 200, pzero.Y + (float)(3 * _height / 200));
        SolidBrush brush3 = new(txtcolor);
        g.DrawString(MinV.ToString(), font, brush3, point);
        g.DrawString(MaxV.ToString(), font, brush3, point2);
        for (int j = 0; j < list2.Count; j++)
        {
            g.DrawString(list2[j].ToString(), font, brush3, list[j]);
        }
        SolidBrush brush4 = new(ValueColor);
        Font font2 = new(family, 15 * _width / 200);
        PointF point3 = new(50 * _width / 200, 140 * _height / 200);
        if (initflag == 1)
        {
            g.DrawLine(Pens.Red, pcenter, pmid);
            initflag = 0;
            g.DrawString("#########", font2, brush4, point3);
        }
        else
        {
            double num25 = Math.Round(Value, pt2);
            g.DrawString(point: new PointF((50 + (30 - 4 * pt2)) * _width / 200, 140 * _height / 200), s: num25.ToString(), font: font2, brush: brush4);
        }
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
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
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
                        pffnow.X = (100f - Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)_width / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0))) * (float)_height / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.5 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.2)
                    {
                        pffnow.X = (100f - Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)_width / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI / 2.0))) * (float)_height / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) <= 0.8 && (double)((Value - MinV) / (MaxV - MinV)) >= 0.5)
                    {
                        pffnow.X = (100f + Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)_width / 200f;
                        pffnow.Y = (100f - Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - Math.PI))) * (float)_height / 200f;
                    }
                    else if ((double)((Value - MinV) / (MaxV - MinV)) > 0.8)
                    {
                        pffnow.X = (100f + Convert.ToSingle(70.0 * Math.Cos((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)_width / 200f;
                        pffnow.Y = (100f + Convert.ToSingle(70.0 * Math.Sin((double)((Value - MinV) / (MaxV - MinV)) * Math.PI * 5.0 / 3.0 + Math.PI / 6.0 - 4.71238898038469))) * (float)_height / 200f;
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
        BSet2 bSet = new()
        {
            Bgcolor = Bgcolor,
            ValueColor = ValueColor,
            TxtColor = txtcolor,
            MaxValue = MaxV,
            MinValue = MinV,
            pt2 = pt2,
            MainMarkCount = mainmarkcount
        };
        if (!string.IsNullOrEmpty(varname))
        {
            bSet.VarName = varname;
        }
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            pt2 = bSet.pt2;
            mainmarkcount = bSet.MainMarkCount;
            Bgcolor = bSet.Bgcolor;
            ValueColor = bSet.ValueColor;
            txtcolor = bSet.TxtColor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC4 bSaveC = new()
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
            BSaveC4 bSaveC = (BSaveC4)formatter.Deserialize(stream);
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
