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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap : CPixieControl
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

    private int _width;

    private int _height;

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
    [Category("杂项")]
    [Description("控件绑定变量。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [Description("罐体内液位到达最大填充比例时对应的变量值。")]
    [ReadOnly(false)]
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
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
    [DisplayName("液位最大填充")]
    [Category("杂项")]
    [Description("罐体内液位最大填充比例。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DisplayName("填充颜色")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [Description("罐体颜色。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("罐体颜色")]
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

    [ReadOnly(false)]
    [Description("当填充物未填满填充区域时，未填满部分显示的颜色。")]
    [DisplayName("填充背景色")]
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

    [DisplayName("当前值")]
    [Description("当前罐体显示的值。")]
    [Category("杂项")]
    [ReadOnly(false)]
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

    public override CShape Copy()
    {
        drawbitmap drawbitmap8 = (drawbitmap)base.Copy();
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
        Rectangle rect = new(10 * _width / 120, 30 * _height / 150, 50 * _width / 120, 100 * _height / 150);
        LinearGradientBrush brush = new(rect, setcolortype, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect, Color.White, setcolortype, LinearGradientMode.Horizontal);
        g.DrawRectangle(Pens.White, rect);
        g.FillRectangle(brush, rect);
        Rectangle rect2 = new(60 * _width / 120, 30 * _height / 150, 50 * _width / 120, 100 * _height / 150);
        g.DrawRectangle(Pens.White, rect2);
        g.FillRectangle(brush2, rect2);
        g.DrawLine(pen, 60 * _width / 120, 28 * _height / 150, 60 * _width / 120, 132 * _height / 150);
        Rectangle rect3 = new(10 * _width / 120, 11 * _height / 150, 100 * _width / 120, 40 * _height / 150);
        Rectangle rect4 = new(10 * _width / 120, 109 * _height / 150, 100 * _width / 120, 40 * _height / 150);
        g.FillPie(brush, rect3, 180f, 90f);
        g.FillPie(brush2, rect3, 270f, 90f);
        g.FillPie(brush2, rect4, 0f, 90f);
        g.FillPie(brush, rect4, 90f, 90f);
        g.DrawLine(pen, 60 * _width / 120, 30 * _height / 150, 60 * _width / 120, 11 * _height / 150);
        g.DrawLine(pen, 60 * _width / 120, 130 * _height / 150, 60 * _width / 120, 150 * _height / 150);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 250;
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
                SolidBrush brush2 = new(bgcolor);
                Paint(graphics);
                if (Value >= low && Value <= high)
                {
                    highalertflag = false;
                    lowalertflag = false;
                    Rectangle rect = new(50 * _width / 120, (120 - 80 * lowpst / 100 - Convert.ToInt32((float)(80 * (highpst - lowpst) / 100) * (Value - low) / (high - low))) * _height / 150, 20 * _width / 120, Convert.ToInt32((float)(80 * (highpst - lowpst) / 100) * (Value - low) / (high - low) * (float)_height / 150f));
                    Rectangle rect2 = new(50 * _width / 120, 40 * _height / 150, 20 * _width / 120, 80 * _height / 150);
                    graphics.FillRectangle(brush2, rect2);
                    graphics.FillRectangle(brush, rect);
                }
                else if (Value > high)
                {
                    if (!highalertflag && this.HighAlert != null)
                    {
                        this.HighAlert(this, null);
                        highalertflag = true;
                    }
                    Rectangle rect3 = new(50 * _width / 120, 40 * _height / 150, 20 * _width / 120, 80 * _height / 150);
                    graphics.FillRectangle(brush, rect3);
                }
                else if (Value < low)
                {
                    if (!lowalertflag && this.LowAlert != null)
                    {
                        this.LowAlert(this, null);
                        lowalertflag = true;
                    }
                    Rectangle rect4 = new(50 * _width / 120, 40 * _height / 150, 20 * _width / 120, 80 * _height / 150);
                    graphics.FillRectangle(brush2, rect4);
                }
                Rectangle rect5 = new(50 * _width / 120, 40 * _height / 150, 20 * _width / 120, 80 * (100 - highpst) / 100 * _height / 150);
                Rectangle rect6 = new(50 * _width / 120, (120 - 80 * lowpst / 100) * _height / 150, 20 * _width / 120, 80 * lowpst / 100 * _height / 150);
                graphics.FillRectangle(brush2, rect5);
                new Pen(bgcolor);
                graphics.FillRectangle(brush, rect6);
                Pen pen = new(colortype);
                graphics.DrawRectangle(pen, rect6);
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
            gSet.Bgcolor = bgcolor;
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
            bgcolor = gSet.Bgcolor;
            NeedRefresh = true;
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    private string GetTable()
    {
        return GetVarNames();
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        Save save = new()
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
        formatter.Serialize(memoryStream, save);
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
            Save save = (Save)formatter.Deserialize(stream);
            stream.Close();
            varname = save.varname;
            value = save.value;
            high = save.high;
            low = save.low;
            highpst = save.highpst;
            lowpst = save.lowpst;
            colortype = save.colortype;
            setcolortype = save.setcolortype;
        }
        catch { }
    }
}
