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

namespace Guan;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap4 : CPixieControl
{
    private bool highalertflag;

    private bool lowalertflag;

    private float value;

    private string varname = "";

    private float high = 100f;

    private float low;

    private int highpst = 100;

    private int lowpst;

    private Color colortype = Color.Green;

    private Color setcolortype = Color.Gray;

    private Color bgcolor = Color.White;

    private int _width = 200;

    private int _height = 233;

    [DisplayName("绑定变量")]
    [Description("控件绑定变量。")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Description("罐体内液位到达最大填充比例时对应的变量值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("液位满对应值")]
    [Category("杂项")]
    public float HighValue
    {
        get
        {
            return high;
        }
        set
        {
            high = value;
        }
    }

    [Category("杂项")]
    [Description("罐体内液位到达最小填充比例时对应的变量值。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("液位空对应值")]
    public float LowValue
    {
        get
        {
            return low;
        }
        set
        {
            low = value;
        }
    }

    [Description("罐体内液位最大填充比例。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("液位最大填充")]
    public int HighFill
    {
        get
        {
            return highpst;
        }
        set
        {
            highpst = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("液位最小填充")]
    [Category("杂项")]
    [Description("罐体内液位最小填充比例。")]
    public int LowFill
    {
        get
        {
            return lowpst;
        }
        set
        {
            lowpst = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("填充颜色")]
    [Category("杂项")]
    [Description("罐体内填充物显示颜色。")]
    public Color ContentColor
    {
        get
        {
            return colortype;
        }
        set
        {
            colortype = value;
        }
    }

    [DisplayName("罐体颜色")]
    [Category("杂项")]
    [Description("罐体颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public Color ControlColor
    {
        get
        {
            return setcolortype;
        }
        set
        {
            setcolortype = value;
        }
    }

    [Description("当填充物未填满填充区域时，未填满部分显示的颜色。")]
    [Category("杂项")]
    [DisplayName("填充背景色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public Color ContentBackColor
    {
        get
        {
            return bgcolor;
        }
        set
        {
            bgcolor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("当前值")]
    [Category("杂项")]
    [Description("当前罐体显示的值。")]
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

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public drawbitmap4()
    {
    }

    protected drawbitmap4(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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

    public override CShape Copy()
    {
        drawbitmap4 drawbitmap8 = (drawbitmap4)base.Copy();
        drawbitmap8.varname = varname;
        drawbitmap8.value = value;
        drawbitmap8.high = high;
        drawbitmap8.highpst = highpst;
        drawbitmap8.low = low;
        drawbitmap8.lowpst = lowpst;
        drawbitmap8.colortype = colortype;
        drawbitmap8.setcolortype = setcolortype;
        drawbitmap8.bgcolor = bgcolor;
        return drawbitmap8;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(setcolortype);
        PointF[] points = new PointF[16]
        {
            new(10 * _width / 120, 30 * _height / 140),
            new(15 * _width / 120, 40 * _height / 140),
            new(20 * _width / 120, 43 * _height / 140),
            new(25 * _width / 120, 48 * _height / 140),
            new(28 * _width / 120, 55 * _height / 140),
            new(33 * _width / 120, 60 * _height / 140),
            new(40 * _width / 120, 70 * _height / 140),
            new(45 * _width / 120, 80 * _height / 140),
            new(42 * _width / 120, 88 * _height / 140),
            new(39 * _width / 120, 95 * _height / 140),
            new(35 * _width / 120, 100 * _height / 140),
            new(25 * _width / 120, 105 * _height / 140),
            new(15 * _width / 120, 115 * _height / 140),
            new(10 * _width / 120, 130 * _height / 140),
            new(110 * _width / 120, 130 * _height / 140),
            new(110 * _width / 120, 30 * _height / 140)
        };
        g.DrawPolygon(Pens.Gray, points);
        g.FillPolygon(brush, points);
        Size size = new(30 * _width / 120, 12 * _height / 140);
        Size size2 = new(20 * _width / 120, 12 * _height / 140);
        Size size3 = new(35 * _width / 120, 12 * _height / 140);
        Size size4 = new(15 * _width / 120, 12 * _height / 140);
        Size size5 = new(25 * _width / 120, 4 * _height / 140);
        Rectangle[] array = new Rectangle[24];
        Point location = new(25 * _width / 120, 30 * _height / 140);
        ref Rectangle reference = ref array[0];
        reference = new Rectangle(location, size);
        location = new Point(55 * _width / 120, 30 * _height / 140);
        ref Rectangle reference2 = ref array[1];
        reference2 = new Rectangle(location, size2);
        location = new Point(75 * _width / 120, 30 * _height / 140);
        ref Rectangle reference3 = ref array[2];
        reference3 = new Rectangle(location, size3);
        location = new Point(30 * _width / 120, 42 * _height / 140);
        ref Rectangle reference4 = ref array[3];
        reference4 = new Rectangle(location, size3);
        location = new Point(65 * _width / 120, 42 * _height / 140);
        ref Rectangle reference5 = ref array[4];
        reference5 = new Rectangle(location, size2);
        location = new Point(85 * _width / 120, 42 * _height / 140);
        ref Rectangle reference6 = ref array[5];
        reference6 = new Rectangle(location, size2);
        location = new Point(40 * _width / 120, 54 * _height / 140);
        ref Rectangle reference7 = ref array[6];
        reference7 = new Rectangle(location, size2);
        location = new Point(75 * _width / 120, 54 * _height / 140);
        ref Rectangle reference8 = ref array[7];
        reference8 = new Rectangle(location, size2);
        location = new Point(95 * _width / 120, 54 * _height / 140);
        ref Rectangle reference9 = ref array[8];
        reference9 = new Rectangle(location, size4);
        location = new Point(45 * _width / 120, 66 * _height / 140);
        ref Rectangle reference10 = ref array[9];
        reference10 = new Rectangle(location, size3);
        location = new Point(75 * _width / 120, 66 * _height / 140);
        ref Rectangle reference11 = ref array[10];
        reference11 = new Rectangle(location, size4);
        g.DrawRectangle(Pens.Black, 90 * _width / 120, 66 * _height / 140, 20 * _width / 120, 12 * _height / 140);
        location = new Point(55 * _width / 120, 78 * _height / 140);
        ref Rectangle reference12 = ref array[11];
        reference12 = new Rectangle(location, size2);
        location = new Point(75 * _width / 120, 78 * _height / 140);
        ref Rectangle reference13 = ref array[12];
        reference13 = new Rectangle(location, size);
        location = new Point(45 * _width / 120, 90 * _height / 140);
        ref Rectangle reference14 = ref array[13];
        reference14 = new Rectangle(location, size);
        location = new Point(75 * _width / 120, 90 * _height / 140);
        ref Rectangle reference15 = ref array[14];
        reference15 = new Rectangle(location, size4);
        location = new Point(90 * _width / 120, 90 * _height / 140);
        ref Rectangle reference16 = ref array[15];
        reference16 = new Rectangle(location, size2);
        location = new Point(40 * _width / 120, 102 * _height / 140);
        ref Rectangle reference17 = ref array[16];
        reference17 = new Rectangle(location, size2);
        location = new Point(60 * _width / 120, 102 * _height / 140);
        ref Rectangle reference18 = ref array[17];
        reference18 = new Rectangle(location, size);
        location = new Point(35 * _width / 120, 114 * _height / 140);
        ref Rectangle reference19 = ref array[18];
        reference19 = new Rectangle(location, size4);
        location = new Point(50 * _width / 120, 114 * _height / 140);
        ref Rectangle reference20 = ref array[19];
        reference20 = new Rectangle(location, size);
        location = new Point(80 * _width / 120, 114 * _height / 140);
        ref Rectangle reference21 = ref array[20];
        reference21 = new Rectangle(location, size2);
        location = new Point(20 * _width / 120, 126 * _height / 140);
        ref Rectangle reference22 = ref array[21];
        reference22 = new Rectangle(location, size5);
        location = new Point(45 * _width / 120, 126 * _height / 140);
        ref Rectangle reference23 = ref array[22];
        reference23 = new Rectangle(location, size5);
        location = new Point(70 * _width / 120, 126 * _height / 140);
        ref Rectangle reference24 = ref array[23];
        reference24 = new Rectangle(location, size5);
        g.DrawRectangle(Pens.Black, 95 * _width / 120, 126 * _height / 140, 15 * _width / 120, 4 * _height / 140);
        g.DrawRectangles(Pens.Black, array);
        Rectangle rect = new(10 * _width / 120, 10 * _height / 140, 100 * _width / 120, 40 * _height / 140);
        g.DrawPie(Pens.Black, rect, 180f, 180f);
        g.FillPie(brush, rect, 180f, 180f);
        g.DrawLine(Pens.Black, 21 * _width / 120, 130 * _height / 140, 21 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 32 * _width / 120, 130 * _height / 140, 32 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 43 * _width / 120, 130 * _height / 140, 43 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 54 * _width / 120, 130 * _height / 140, 54 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 65 * _width / 120, 130 * _height / 140, 65 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 76 * _width / 120, 130 * _height / 140, 76 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 87 * _width / 120, 130 * _height / 140, 87 * _width / 120, 140 * _height / 140);
        g.DrawLine(Pens.Black, 99 * _width / 120, 130 * _height / 140, 99 * _width / 120, 140 * _height / 140);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 233;
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
                SolidBrush brush = new(colortype);
                SolidBrush brush2 = new(ContentBackColor);
                if (Value >= low && Value <= high)
                {
                    highalertflag = false;
                    lowalertflag = false;
                    Rectangle rect = new(10 * _width / 120, (130 - 100 * lowpst / 100 - Convert.ToInt32((float)(100 * (highpst - lowpst) / 100) * (Value - low) / (high - low))) * _height / 140, 95 * _width / 120, Convert.ToInt32((float)(100 * (highpst - lowpst) / 100) * (Value - low) / (high - low)) * _height / 140);
                    Rectangle rect2 = new(10 * _width / 120, 30 * _height / 140, 95 * _width / 120, 100 * _height / 140);
                    graphics.DrawRectangle(Pens.White, rect2);
                    graphics.FillRectangle(brush2, rect2);
                    graphics.DrawRectangle(Pens.White, rect);
                    graphics.FillRectangle(brush, rect);
                }
                else if (Value > high)
                {
                    if (!highalertflag && HighAlert != null)
                    {
                        HighAlert(this, null);
                        highalertflag = true;
                    }
                    Rectangle rect3 = new(10 * _width / 120, 30 * _height / 140, 95 * _width / 120, 100 * _height / 140);
                    graphics.FillRectangle(brush, rect3);
                }
                else if (Value < low)
                {
                    if (!lowalertflag && LowAlert != null)
                    {
                        LowAlert(this, null);
                        lowalertflag = true;
                    }
                    Rectangle rect4 = new(10 * _width / 120, 30 * _height / 140, 95 * _width / 120, 100 * _height / 140);
                    graphics.FillRectangle(brush2, rect4);
                }
                Rectangle rect5 = new(10 * _width / 120, 30 * _height / 140, 95 * _width / 120, 100 * (100 - highpst) / 100 * _height / 140);
                Rectangle rect6 = new(10 * _width / 120, (130 - 100 * lowpst / 100) * _height / 140, 95 * _width / 120, 100 * lowpst / 100 * _height / 140);
                graphics.FillRectangle(brush2, rect5);
                graphics.FillRectangle(brush, rect6);
                Pen pen = new(colortype);
                graphics.DrawLine(pen, 11 * _width / 120, (130 - 100 * lowpst / 100) * _height / 140, 105 * _width / 120, (130 - 100 * lowpst / 100) * _height / 140);
                Paint(graphics);
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void ShowDialog()
    {
        GSet gSet = new();
        gSet.viewevent += GetTable;
        gSet.ckvarevent += CheckVar;
        gSet.setcolortype = setcolortype;
        if (!string.IsNullOrEmpty(varname.ToString()))
        {
            gSet.varname = varname;
            gSet.highpersnt = highpst;
            gSet.high = high;
            gSet.low = low;
            gSet.lowpersnt = lowpst;
            gSet.colortype = colortype;
            gSet.Bgcolor = ContentBackColor;
        }
        if (gSet.ShowDialog() == DialogResult.OK)
        {
            varname = gSet.varname;
            colortype = gSet.colortype;
            setcolortype = gSet.setcolortype;
            high = gSet.high;
            highpst = gSet.highpersnt;
            low = gSet.low;
            lowpst = gSet.lowpersnt;
            ContentBackColor = gSet.Bgcolor;
            NeedRefresh = true;
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        SaveC4 saveC = new()
        {
            varname = varname,
            value = value,
            high = high,
            low = low,
            highpst = highpst,
            lowpst = lowpst,
            colortype = colortype,
            setcolortype = setcolortype
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
            SaveC4 saveC = (SaveC4)formatter.Deserialize(stream);
            stream.Close();
            varname = saveC.varname;
            value = saveC.value;
            high = saveC.high;
            low = saveC.low;
            highpst = saveC.highpst;
            lowpst = saveC.lowpst;
            colortype = saveC.colortype;
            setcolortype = saveC.setcolortype;
        }
        catch { }
    }
}
