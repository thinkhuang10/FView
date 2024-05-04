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

namespace KG_1;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap4 : CPixieControl
{
    private string varname = "";

    private string titlestr = "仪表";

    private string onstr = "ON";

    private string offstr = "OFF";

    private Color titlecolor = Color.Black;

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int _width = 200;

    private int _height = 220;

    private bool opstflag;

    private int initflag = 1;

    private bool value;

    [Description("开关绑定变量。")]
    [DisplayName("绑定变量")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Category("杂项")]
    [Description("显示文本。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("显示文本")]
    public string MyTitle
    {
        get
        {
            return titlestr;
        }
        set
        {
            titlestr = value;
        }
    }

    [DHMICtrlProperty]
    [Description("绑定变量为真时开关指向一侧显示的文本。")]
    [DisplayName("开文本")]
    [ReadOnly(false)]
    [Category("杂项")]
    public string OnText
    {
        get
        {
            return onstr;
        }
        set
        {
            onstr = value;
        }
    }

    [Description("绑定变量为假时开关指向一侧显示的文本。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("关文本")]
    public string OffText
    {
        get
        {
            return offstr;
        }
        set
        {
            offstr = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("显示文本颜色")]
    [Category("杂项")]
    [Description("显示文本颜色。")]
    [ReadOnly(false)]
    public Color Titlecolor
    {
        get
        {
            return titlecolor;
        }
        set
        {
            titlecolor = value;
        }
    }

    [DisplayName("开颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("绑定变量为真时开关指向一侧显示的颜色。")]
    public Color Oncolor
    {
        get
        {
            return oncolor;
        }
        set
        {
            oncolor = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("关颜色")]
    [Category("杂项")]
    [Description("绑定变量为假时开关指向一侧显示的颜色。")]
    public Color Offcolor
    {
        get
        {
            return offcolor;
        }
        set
        {
            offcolor = value;
        }
    }

    [DisplayName("是否取反")]
    [Description("是否取反。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public bool IsGetOpesite
    {
        get
        {
            return opstflag;
        }
        set
        {
            opstflag = value;
        }
    }

    [Description("绑定变量当前值。")]
    [Category("杂项")]
    [DisplayName("绑定变量当前值")]
    [ReadOnly(false)]
    public bool Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, null);
                }
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public event EventHandler ValueChanged;

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

    public override CShape Copy()
    {
        drawbitmap4 drawbitmap10 = (drawbitmap4)base.Copy();
        drawbitmap10.varname = varname;
        drawbitmap10.titlestr = titlestr;
        drawbitmap10.offstr = offstr;
        drawbitmap10.onstr = onstr;
        drawbitmap10.titlecolor = titlecolor;
        drawbitmap10.oncolor = oncolor;
        drawbitmap10.offcolor = offcolor;
        drawbitmap10.opstflag = opstflag;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Arial");
        Font font = new(family, 10 * _width / 200);
        g.FillRectangle(rect: new Rectangle(0, 0, 200 * _width / 200, 220 * _height / 220), brush: Brushes.LightGray);
        SolidBrush brush = new(oncolor);
        SolidBrush brush2 = new(offcolor);
        SolidBrush brush3 = new(titlecolor);
        Rectangle rect2 = new(0, 35 * _height / 220, 67 * _width / 200, 35 * _height / 220);
        Rectangle rect3 = new(135 * _width / 200, 35 * _height / 220, 67 * _width / 200, 35 * _height / 220);
        g.FillRectangle(brush, rect2);
        g.FillRectangle(brush2, rect3);
        g.DrawArc(rect: new Rectangle(50 * _width / 200, 90 * _height / 220, 100 * _width / 200, 100 * _height / 220), pen: Pens.Black, startAngle: 0f, sweepAngle: 360f);
        g.DrawLine(Pens.LightGray, 146 * _width / 200, 142 * _height / 220, 100 * _width / 200, 142 * _height / 220);
        if (initflag == 1)
        {
            PointF[] points = new PointF[4]
            {
                new(50 * _width / 200, 110 * _height / 220),
                new(67 * _width / 200, 90 * _height / 220),
                new(150 * _width / 200, 176 * _height / 220),
                new(135 * _width / 200, 195 * _height / 220)
            };
            g.DrawPolygon(Pens.White, points);
            g.FillPolygon(Brushes.LightGray, points);
        }
        PointF point = new(67 * _width / 200, 8 * _height / 220);
        PointF point2 = new(15 * _width / 200, 40 * _height / 220);
        PointF point3 = new(140 * _width / 200, 40 * _height / 220);
        g.DrawString(titlestr, font, brush3, point);
        g.DrawString(onstr, font, Brushes.Black, point2);
        g.DrawString(offstr, font, Brushes.Black, point3);
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(varname) && (float)e.X < Math.Abs(Width) && (float)e.Y < Math.Abs(Height))
            {
                if (Convert.ToBoolean(GetValue(varname)))
                {
                    SetValue(varname, false);
                }
                else
                {
                    SetValue(varname, true);
                }
            }
        }
        catch
        {
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname);
            if (Height == 0f || Width == 0f)
            {
                _height = 220;
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
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                initflag = 0;
                Paint(graphics);
                if (opstflag)
                {
                    if (!Value)
                    {
                        PointF[] points = new PointF[4]
                        {
                            new(50 * _width / 200, 110 * _height / 220),
                            new(67 * _width / 200, 90 * _height / 220),
                            new(150 * _width / 200, 176 * _height / 220),
                            new(135 * _width / 200, 195 * _height / 220)
                        };
                        graphics.DrawPolygon(Pens.White, points);
                        graphics.FillPolygon(Brushes.LightGray, points);
                    }
                    else
                    {
                        PointF[] points2 = new PointF[4]
                        {
                            new(150 * _width / 200, 110 * _height / 220),
                            new(140 * _width / 200, 90 * _height / 220),
                            new(50 * _width / 200, 170 * _height / 220),
                            new(67 * _width / 200, 195 * _height / 220)
                        };
                        graphics.DrawPolygon(Pens.White, points2);
                        graphics.FillPolygon(Brushes.LightGray, points2);
                    }
                }
                else if (Value)
                {
                    PointF[] points3 = new PointF[4]
                    {
                        new(50 * _width / 200, 110 * _height / 220),
                        new(67 * _width / 200, 90 * _height / 220),
                        new(150 * _width / 200, 176 * _height / 220),
                        new(135 * _width / 200, 195 * _height / 220)
                    };
                    graphics.DrawPolygon(Pens.White, points3);
                    graphics.FillPolygon(Brushes.LightGray, points3);
                }
                else
                {
                    PointF[] points4 = new PointF[4]
                    {
                        new(150 * _width / 200, 110 * _height / 220),
                        new(140 * _width / 200, 90 * _height / 220),
                        new(50 * _width / 200, 170 * _height / 220),
                        new(67 * _width / 200, 195 * _height / 220)
                    };
                    graphics.DrawPolygon(Pens.White, points4);
                    graphics.FillPolygon(Brushes.LightGray, points4);
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void ShowDialog()
    {
        KGSet4 kGSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.opstflag = opstflag;
            kGSet.varname = varname;
            kGSet.titlestr = titlestr;
            kGSet.onstr = onstr;
            kGSet.offstr = offstr;
            kGSet.oncolor = oncolor;
            kGSet.offcolor = offcolor;
            kGSet.titlecolor = titlecolor;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.opstflag;
            varname = "[" + kGSet.varname + "]";
            titlestr = kGSet.titlestr;
            onstr = kGSet.onstr;
            offstr = kGSet.offstr;
            titlecolor = kGSet.titlecolor;
            oncolor = kGSet.oncolor;
            offcolor = kGSet.offcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave3 bSave = new()
        {
            varname = varname,
            titlestr = titlestr,
            onstr = onstr,
            offstr = offstr,
            titlecolor = titlecolor,
            oncolor = oncolor,
            offcolor = offcolor
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
            BSave3 bSave = (BSave3)formatter.Deserialize(stream);
            stream.Close();
            varname = bSave.varname;
            titlestr = bSave.titlestr;
            onstr = bSave.onstr;
            offstr = bSave.offstr;
            titlecolor = bSave.titlecolor;
            oncolor = bSave.oncolor;
            offcolor = bSave.offcolor;
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
