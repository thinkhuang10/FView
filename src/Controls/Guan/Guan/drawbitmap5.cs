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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap5 : CPixieControl
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

    private int _height = 245;

    [ReadOnly(false)]
    [Category("杂项")]
    [Description("控件绑定变量。")]
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

    [ReadOnly(false)]
    [Category("杂项")]
    [Description("罐体内液位到达最大填充比例时对应的变量值。")]
    [DHMICtrlProperty]
    [DisplayName("液位满对应值")]
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

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("液位空对应值")]
    [Category("杂项")]
    [Description("罐体内液位到达最小填充比例时对应的变量值。")]
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

    [DisplayName("液位最大填充")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("罐体内液位最大填充比例。")]
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("罐体内填充物显示颜色。")]
    [DisplayName("填充颜色")]
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
    [Description("罐体颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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
    [DisplayName("填充背景色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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
    [Description("当前罐体显示的值。")]
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

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public drawbitmap5()
    {
    }

    protected drawbitmap5(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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
        drawbitmap5 drawbitmap8 = (drawbitmap5)base.Copy();
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
        Pen pen = new(Color.White, 4f);
        Rectangle rect = new(30 * _width / 170, 30 * _height / 210, 50 * _width / 170, 100 * _height / 210);
        LinearGradientBrush brush = new(rect, setcolortype, Color.White, LinearGradientMode.Horizontal);
        g.DrawRectangle(Pens.White, rect);
        g.FillRectangle(brush, rect);
        Rectangle rect2 = new(80 * _width / 170, 30 * _height / 210, 50 * _width / 170, 100 * _height / 210);
        g.DrawRectangle(Pens.White, rect2);
        LinearGradientBrush brush2 = new(rect, Color.White, setcolortype, LinearGradientMode.Horizontal);
        g.FillRectangle(brush2, rect2);
        g.DrawLine(pen, 80 * _width / 170, 30 * _height / 210, 80 * _width / 170, 130 * _height / 210);
        g.FillPolygon(points: new PointF[6]
        {
            new(30 * _width / 170, 130 * _height / 210),
            new(10 * _width / 170, 200 * _height / 210),
            new(5 * _width / 170, 205 * _height / 210),
            new(5 * _width / 170, 210 * _height / 210),
            new(20 * _width / 170, 200 * _height / 210),
            new(40 * _width / 170, 130 * _height / 210)
        }, brush: Brushes.Gray);
        g.FillPolygon(points: new PointF[6]
        {
            new(130 * _width / 170, 130 * _height / 210),
            new(150 * _width / 170, 200 * _height / 210),
            new(155 * _width / 170, 205 * _height / 210),
            new(155 * _width / 170, 210 * _height / 210),
            new(140 * _width / 170, 200 * _height / 210),
            new(120 * _width / 170, 130 * _height / 210)
        }, brush: Brushes.Gray);
        g.FillPolygon(points: new PointF[6]
        {
            new(80 * _width / 170, 130 * _height / 210),
            new(25 * _width / 170, 160 * _height / 210),
            new(25 * _width / 170, 170 * _height / 210),
            new(80 * _width / 170, 140 * _height / 210),
            new(135 * _width / 170, 170 * _height / 210),
            new(135 * _width / 170, 160 * _height / 210)
        }, brush: Brushes.Gray);
        Rectangle rect3 = new(30 * _width / 170, 11 * _height / 210, 100 * _width / 170, 40 * _height / 210);
        Rectangle rect4 = new(30 * _width / 170, 109 * _height / 210, 100 * _width / 170, 40 * _height / 210);
        g.FillPie(brush, rect3, 180f, 90f);
        g.FillPie(brush2, rect3, 270f, 90f);
        g.FillPie(brush2, rect4, 0f, 90f);
        g.FillPie(brush, rect4, 90f, 90f);
        g.FillRectangle(rect: new Rectangle(25 * _width / 170, 158 * _height / 210, 110 * _width / 170, 30 * _height / 210), brush: Brushes.Blue);
        g.DrawLine(pen, 80 * _width / 170, 30 * _height / 210, 80 * _width / 170, 10 * _height / 210);
        g.DrawLine(pen, 80 * _width / 170, 130 * _height / 210, 80 * _width / 170, 150 * _height / 210);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 245;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            FontFamily family = new("Arial");
            Font font = new(family, 10 * _height / 210);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToSingle(obj);
            }
            try
            {
                Paint(graphics);
                SolidBrush brush = new(colortype);
                SolidBrush brush2 = new(ContentBackColor);
                if (Value >= low && Value <= high)
                {
                    highalertflag = false;
                    lowalertflag = false;
                    PointF point = new(52 * _width / 170, 166 * _height / 210);
                    Rectangle rect = new(90 * _width / 170, (120 - 80 * lowpst / 100 - Convert.ToInt32((float)(80 * (highpst - lowpst) / 100) * (Value - low) / (high - low))) * _height / 210, 20 * _width / 170, Convert.ToInt32((float)(80 * (highpst - lowpst) / 100) * (Value - low) / (high - low)) * _height / 210);
                    Rectangle rect2 = new(90 * _width / 170, 40 * _height / 210, 20 * _width / 170, 80 * _height / 210);
                    graphics.FillRectangle(brush2, rect2);
                    graphics.FillRectangle(brush, rect);
                    graphics.FillRectangle(rect: new Rectangle(33 * _width / 170, 166 * _height / 210, 94 * _width / 170, 14 * _height / 210), brush: Brushes.White);
                    graphics.DrawString(Value.ToString(), font, Brushes.Black, point);
                }
                else if (Value > high)
                {
                    if (!highalertflag && HighAlert != null)
                    {
                        HighAlert(this, null);
                        highalertflag = true;
                    }
                    PointF point2 = new(52 * _width / 170, 166 * _height / 210);
                    Rectangle rect4 = new(90 * _width / 170, 40 * _height / 210, 20 * _width / 170, 80 * _height / 210);
                    graphics.FillRectangle(brush, rect4);
                    graphics.FillRectangle(rect: new Rectangle(33 * _width / 170, 166 * _height / 210, 94 * _width / 170, 14 * _height / 210), brush: Brushes.White);
                    graphics.DrawString(Value.ToString(), font, Brushes.Black, point2);
                }
                else if (Value < low)
                {
                    if (!lowalertflag && LowAlert != null)
                    {
                        LowAlert(this, null);
                        lowalertflag = true;
                    }
                    PointF point3 = new(52 * _width / 170, 166 * _height / 210);
                    Rectangle rect6 = new(90 * _width / 170, 40 * _height / 210, 20 * _width / 170, 80 * _height / 210);
                    graphics.FillRectangle(brush2, rect6);
                    graphics.FillRectangle(rect: new Rectangle(33 * _width / 170, 166 * _height / 210, 94 * _width / 170, 14 * _height / 210), brush: Brushes.White);
                    graphics.DrawString(Value.ToString(), font, Brushes.Black, point3);
                }
                Rectangle rect8 = new(90 * _width / 170, 40 * _height / 210, 20 * _width / 170, 80 * (100 - highpst) / 100 * _height / 210);
                Rectangle rect9 = new(90 * _width / 170, (120 - 80 * lowpst / 100) * _height / 210, 20 * _width / 170, 80 * lowpst / 100 * _height / 210);
                graphics.FillRectangle(brush2, rect8);
                Pen pen = new(ContentBackColor);
                graphics.DrawRectangle(pen, rect8);
                graphics.FillRectangle(brush, rect9);
                Pen pen2 = new(colortype);
                graphics.DrawRectangle(pen2, rect9);
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
        if (!string.IsNullOrEmpty(varname.ToString()))
        {
            gSet.varname = varname;
            gSet.highpersnt = highpst;
            gSet.high = high;
            gSet.low = low;
            gSet.lowpersnt = lowpst;
            gSet.colortype = colortype;
            gSet.setcolortype = setcolortype;
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

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        SaveC5 saveC = new()
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
            SaveC5 saveC = (SaveC5)formatter.Deserialize(stream);
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
        catch
        {
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
