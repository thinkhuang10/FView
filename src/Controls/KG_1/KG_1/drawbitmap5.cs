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
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap5 : CPixieControl
{
    private string varname = "";

    private string varname2 = "";

    private string titlestr = "仪表";

    private string onstr = "AUTO";

    private string offstr = "MAN";

    private string midstr = "OFF";

    private Color titlecolor = Color.Black;

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private Color midcolor = Color.Blue;

    private int initflag = 1;

    private int _width = 200;

    private int _height = 200;

    public bool opstflag;

    private bool value;

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DHMICtrlProperty]
    public string BindVar2
    {
        get
        {
            if (varname2.ToString().IndexOf('[') == -1)
            {
                return varname2;
            }
            return varname2.Substring(1, varname2.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname2 = "[" + value.ToString() + "]";
            }
            else
            {
                varname2 = value;
            }
        }
    }

    [DHMICtrlProperty]
    public string Title
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

    [DHMICtrlProperty]
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
    public string MidText
    {
        get
        {
            return midstr;
        }
        set
        {
            midstr = value;
        }
    }

    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
    public Color Midcolor
    {
        get
        {
            return midcolor;
        }
        set
        {
            midcolor = value;
        }
    }

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
        drawbitmap5 drawbitmap10 = (drawbitmap5)base.Copy();
        drawbitmap10.varname = varname;
        drawbitmap10.titlestr = titlestr;
        drawbitmap10.offstr = offstr;
        drawbitmap10.midstr = midstr;
        drawbitmap10.onstr = onstr;
        drawbitmap10.titlecolor = titlecolor;
        drawbitmap10.oncolor = oncolor;
        drawbitmap10.offcolor = offcolor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Times New Roman");
        Font font = new(family, 4 * _width / 200);
        g.FillRectangle(rect: new Rectangle(0, 0, 200 * _width / 200, 200 * _height / 200), brush: Brushes.LightGray);
        SolidBrush brush = new(oncolor);
        SolidBrush brush2 = new(offcolor);
        SolidBrush brush3 = new(midcolor);
        SolidBrush brush4 = new(titlecolor);
        Rectangle rect2 = new(0, 33 * _height / 200, 67 * _width / 200, 33 * _height / 200);
        Rectangle rect3 = new(134 * _width / 200, 33 * _height / 200, 66 * _width / 200, 33 * _height / 200);
        Rectangle rect4 = new(67 * _width / 200, 33 * _height / 200, 67 * _width / 200, 33 * _height / 200);
        g.FillRectangle(brush, rect2);
        g.FillRectangle(brush2, rect3);
        g.FillRectangle(brush3, rect4);
        g.DrawPie(rect: new Rectangle(50 * _width / 200, 90 * _height / 200, 100 * _width / 200, 100 * _height / 200), pen: Pens.Black, startAngle: 0f, sweepAngle: 360f);
        g.DrawLine(Pens.LightGray, 140 * _width / 200, 140 * _height / 200, 100 * _width / 200, 140 * _height / 200);
        if (initflag == 1)
        {
            PointF[] points = new PointF[4]
            {
                new(94 * _width / 200, 80 * _height / 200),
                new(107 * _width / 200, 80 * _height / 200),
                new(107 * _width / 200, 180 * _height / 200),
                new(94 * _width / 200, 180 * _height / 200)
            };
            g.DrawPolygon(Pens.White, points);
            g.FillPolygon(Brushes.LightGray, points);
        }
        PointF point = new(67 * _width / 200, _height / 200);
        PointF point2 = new(_width / 200, 60 * _height / 200);
        PointF point3 = new(140 * _width / 200, 37 * _height / 200);
        PointF point4 = new(21 * _width / 200, 37 * _height / 200);
        g.DrawString(titlestr, font, brush4, point);
        g.DrawString(onstr, font, Brushes.Black, point2);
        g.DrawString(offstr, font, Brushes.Black, point3);
        g.DrawString(midstr, font, Brushes.Black, point4);
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
            if (obj == null)
            {
                Paint(graphics);
            }
            else
            {
                try
                {
                    Value = Convert.ToBoolean(obj);
                    initflag = 0;
                    Paint(graphics);
                    if (Value)
                    {
                        PointF[] points = new PointF[4]
                        {
                            new(50 * _width / 200, 107 * _height / 200),
                            new(67 * _width / 200, 90 * _height / 200),
                            new(150 * _width / 200, 160 * _height / 200),
                            new(135 * _width / 200, 180 * _height / 200)
                        };
                        graphics.DrawPolygon(Pens.White, points);
                        graphics.FillPolygon(Brushes.LightGray, points);
                    }
                    else
                    {
                        PointF[] points2 = new PointF[4]
                        {
                            new(150 * _width / 200, 107 * _height / 200),
                            new(135 * _width / 200, 90 * _height / 200),
                            new(50 * _width / 200, 155 * _height / 200),
                            new(67 * _width / 200, 180 * _height / 200)
                        };
                        graphics.DrawPolygon(Pens.White, points2);
                        graphics.FillPolygon(Brushes.LightGray, points2);
                    }
                }
                catch
                {
                }
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
        KGSet6 kGSet = new();
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.varname = varname;
            kGSet.varname2 = varname2;
            kGSet.titlestr = titlestr;
            kGSet.offstr = offstr;
            kGSet.onstr = onstr;
            kGSet.midstr = midstr;
            kGSet.titlecolor = titlecolor;
            kGSet.oncolor = oncolor;
            kGSet.offcolor = offcolor;
            kGSet.midcolor = midcolor;
        }
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + kGSet.varname + "]";
            varname2 = kGSet.varname2;
            titlestr = kGSet.titlestr;
            onstr = kGSet.onstr;
            offstr = kGSet.offstr;
            midstr = kGSet.midstr;
            titlecolor = kGSet.titlecolor;
            oncolor = kGSet.oncolor;
            offcolor = kGSet.offcolor;
            midcolor = kGSet.midcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        KGSave kGSave = new()
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
            KGSave kGSave = (KGSave)formatter.Deserialize(stream);
            stream.Close();
            varname = kGSave.varname;
            titlestr = kGSave.titlestr;
            onstr = kGSave.onstr;
            offstr = kGSave.offstr;
            titlecolor = kGSave.titlecolor;
            oncolor = kGSave.oncolor;
            offcolor = kGSave.offcolor;
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
