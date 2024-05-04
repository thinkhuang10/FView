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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap9 : CPixieControl
{
    private Color setcolor = Color.Red;

    private string varname = "";

    public int initflag = 1;

    private int _width = 200;

    private int _height = 90;

    private bool valflag;

    private bool opstflag;

    private bool value;

    [Description("开关绑定变量。")]
    [DisplayName("绑定变量")]
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("绑定变量当前值")]
    [Description("绑定变量当前值。")]
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

    public drawbitmap9()
    {
    }

    protected drawbitmap9(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap9 obj = new();
        FieldInfo[] fields = typeof(drawbitmap9).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap9))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap9), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap9))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap9), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap9) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap9 drawbitmap10 = (drawbitmap9)base.Copy();
        drawbitmap10.setcolor = setcolor;
        drawbitmap10.opstflag = opstflag;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        g.FillRectangle(rect: new Rectangle(0, 0, _width, Convert.ToInt32((double)_height * 0.8)), brush: Brushes.Black);
        if (valflag)
        {
            Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.02), Convert.ToInt32((double)_width * 0.48), Convert.ToInt32((double)_height * 0.76));
            g.FillRectangle(Brushes.DarkGray, rect2);
            g.DrawRectangle(Pens.Black, rect2);
            Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.07), Convert.ToInt32((double)_height * 0.07), Convert.ToInt32((double)_width * 0.38), Convert.ToInt32((double)_height * 0.66));
            g.FillRectangle(Brushes.Green, rect3);
            g.DrawRectangle(Pens.Black, rect3);
            Point[] points = new Point[4]
            {
                new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.02)),
                new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.78)),
                new(Convert.ToInt32((double)_width * 0.87), Convert.ToInt32((double)_height * 0.98)),
                new(Convert.ToInt32((double)_width * 0.87), Convert.ToInt32((double)_height * 0.22))
            };
            g.FillPolygon(Brushes.Gray, points);
            g.DrawPolygon(Pens.Black, points);
            Point[] points2 = new Point[6]
            {
                new(Convert.ToInt32((double)_width * 0.87), Convert.ToInt32((double)_height * 0.22)),
                new(Convert.ToInt32((double)_width * 0.87), Convert.ToInt32((double)_height * 0.98)),
                new(Convert.ToInt32((double)_width * 0.9), Convert.ToInt32((double)_height * 0.93)),
                new(Convert.ToInt32((double)_width * 0.9), Convert.ToInt32((double)_height * 0.43)),
                new(Convert.ToInt32((double)_width * 0.95), Convert.ToInt32((double)_height * 0.4)),
                new(Convert.ToInt32((double)_width * 0.95), Convert.ToInt32((double)_height * 0.12))
            };
            g.FillPolygon(Brushes.DarkGray, points2);
            g.DrawPolygon(Pens.Black, points2);
        }
        else
        {
            Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.02), Convert.ToInt32((double)_width * 0.48), Convert.ToInt32((double)_height * 0.76));
            g.FillRectangle(Brushes.DarkGray, rect4);
            g.DrawRectangle(Pens.Black, rect4);
            Rectangle rect5 = new(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.07), Convert.ToInt32((double)_width * 0.38), Convert.ToInt32((double)_height * 0.66));
            g.FillRectangle(Brushes.Red, rect5);
            g.DrawRectangle(Pens.Black, rect5);
            Point[] points3 = new Point[4]
            {
                new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.02)),
                new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.78)),
                new(Convert.ToInt32((double)_width * 0.13), Convert.ToInt32((double)_height * 0.98)),
                new(Convert.ToInt32((double)_width * 0.13), Convert.ToInt32((double)_height * 0.22))
            };
            g.FillPolygon(Brushes.Gray, points3);
            g.DrawPolygon(Pens.Black, points3);
            Point[] points4 = new Point[6]
            {
                new(Convert.ToInt32((double)_width * 0.13), Convert.ToInt32((double)_height * 0.22)),
                new(Convert.ToInt32((double)_width * 0.13), Convert.ToInt32((double)_height * 0.98)),
                new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.93)),
                new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.43)),
                new(Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.4)),
                new(Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.12))
            };
            g.FillPolygon(Brushes.DarkGray, points4);
            g.DrawPolygon(Pens.Black, points4);
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
                _height = 90;
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
                        valflag = true;
                        Paint(graphics);
                    }
                    else
                    {
                        valflag = false;
                        Paint(graphics);
                    }
                }
                else if (Value)
                {
                    valflag = true;
                    Paint(graphics);
                }
                else
                {
                    valflag = false;
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
            kGSet.opstflag = opstflag;
            kGSet.varname = varname;
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
            setcolor = setcolor
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
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
