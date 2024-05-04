using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using CommonSnappableTypes;
using SetsForms;
using ShapeRuntime;

namespace KG_1;

[Serializable]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap8 : CPixieControl
{
    private Color setcolor = Color.Red;

    private string varname = "";

    public int initflag = 1;

    private int _width = 200;

    private int _height = 100;

    private int step1 = 100;

    private bool opstflag;

    private bool value;

    [Description("开关绑定变量。")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
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

    [DisplayName("是否取反")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("是否取反。")]
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

    [DisplayName("绑定变量当前值")]
    [Description("绑定变量当前值。")]
    [ReadOnly(false)]
    [Category("杂项")]
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

    public drawbitmap8()
    {
    }

    protected drawbitmap8(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap8 info");
        }
        drawbitmap8 obj = new();
        FieldInfo[] fields = typeof(drawbitmap8).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap8))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap8))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap8) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap8 drawbitmap10 = (drawbitmap8)base.Copy();
        drawbitmap10.setcolor = setcolor;
        drawbitmap10.opstflag = opstflag;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        g.FillRectangle(rect: new Rectangle(0, 0, Convert.ToInt32((double)_width * 0.95), Convert.ToInt32((double)_height * 0.95)), brush: Brushes.Gray);
        Point[] points = new Point[4]
        {
            new(Convert.ToInt32(step1), Convert.ToInt32(0)),
            new(Convert.ToInt32(step1), Convert.ToInt32((double)_height * 0.95)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.05), Convert.ToInt32(_height)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.05), Convert.ToInt32((double)_height * 0.05))
        };
        g.DrawPolygon(Pens.Black, points);
        g.FillPolygon(Brushes.Pink, points);
        Point[] points2 = new Point[4]
        {
            new(Convert.ToInt32(step1), Convert.ToInt32(0)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.45), Convert.ToInt32(0)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.5), Convert.ToInt32((double)_height * 0.05)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.05), Convert.ToInt32((double)_height * 0.05))
        };
        g.DrawPolygon(Pens.Black, points2);
        g.FillPolygon(Brushes.DarkRed, points2);
        Point[] points3 = new Point[4]
        {
            new(Convert.ToInt32((double)step1 + (double)_width * 0.05), Convert.ToInt32((double)_height * 0.05)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.5), Convert.ToInt32((double)_height * 0.05)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.5), Convert.ToInt32(_height)),
            new(Convert.ToInt32((double)step1 + (double)_width * 0.05), Convert.ToInt32(_height))
        };
        g.DrawPolygon(Pens.Black, points3);
        g.FillPolygon(Brushes.DarkRed, points3);
        for (int i = 0; i < 3; i++)
        {
            Point[] points4 = new Point[4]
            {
                new(Convert.ToInt32((double)step1 + (double)_width * 0.07 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.07)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.12 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.12)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.12 + (double)i * 0.15 * (double)_width), Convert.ToInt32(_height)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.07 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.95))
            };
            if (i != 0)
            {
                g.DrawPolygon(Pens.Black, points4);
                g.FillPolygon(Brushes.Pink, points4);
            }
            else
            {
                g.FillPolygon(Brushes.Black, points4);
            }
            Point[] points5 = new Point[4]
            {
                new(Convert.ToInt32((double)step1 + (double)_width * 0.07 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.07)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.12 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.12)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.2 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.12)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.15 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.07))
            };
            g.FillPolygon(Brushes.DarkRed, points5);
            g.DrawPolygon(Pens.Black, points5);
            Point[] points6 = new Point[4]
            {
                new(Convert.ToInt32((double)step1 + (double)_width * 0.12 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.12)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.2 + (double)i * 0.15 * (double)_width), Convert.ToInt32((double)_height * 0.12)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.2 + (double)i * 0.15 * (double)_width), Convert.ToInt32(_height)),
                new(Convert.ToInt32((double)step1 + (double)_width * 0.12 + (double)i * 0.15 * (double)_width), Convert.ToInt32(_height))
            };
            g.FillPolygon(Brushes.DarkRed, points6);
            g.DrawPolygon(Pens.Black, points6);
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(varname) && (float)e.X < Math.Abs(Width))
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
                _height = 100;
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
                new SolidBrush(setcolor);
                if (opstflag)
                {
                    if (!Value)
                    {
                        step1 = 0;
                        Paint(graphics);
                    }
                    else
                    {
                        step1 = Convert.ToInt32((double)_width * 0.5);
                        Paint(graphics);
                    }
                }
                else if (Value)
                {
                    step1 = 0;
                    Paint(graphics);
                }
                else
                {
                    step1 = Convert.ToInt32((double)_width * 0.5);
                    Paint(graphics);
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
        KGSet7 kGSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.varname = varname;
            kGSet.opstflag = opstflag;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.opstflag;
            varname = "[" + kGSet.varname + "]";
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            setcolor = setcolor,
            varname = varname
        };
        formatter.Serialize(memoryStream, bSave);
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
            BSave bSave = (BSave)formatter.Deserialize(stream);
            stream.Close();
            setcolor = bSave.setcolor;
            varname = bSave.varname;
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
