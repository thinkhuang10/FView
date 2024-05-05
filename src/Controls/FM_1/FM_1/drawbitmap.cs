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

namespace FM_1;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int _width = 50;

    private int _height = 80;

    private bool value;

    [NonSerialized]
    private SolidBrush br1 = new(Color.Red);

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
    [Description("阀门为开状态时显示的颜色。")]
    [ReadOnly(false)]
    [DisplayName("开颜色")]
    [Category("杂项")]
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
    [DisplayName("关颜色")]
    [Category("杂项")]
    [Description("阀门为关状态时显示的颜色。")]
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

    [Category("杂项")]
    [DisplayName("当前值")]
    [ReadOnly(false)]
    [Description("指示阀门当前状态。")]
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
                ValueChanged?.Invoke(this, null);
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public event EventHandler ValueChanged;

    public drawbitmap()
    {
    }

    protected drawbitmap(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap info");
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
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "_height")
            {
                _height = (int)enumerator.Value;
            }
            else if (enumerator.Name == "_width")
            {
                _width = (int)enumerator.Value;
            }
            else if (enumerator.Name == "offcolor")
            {
                offcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "oncolor")
            {
                oncolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "value")
            {
                value = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "varname")
            {
                varname = (string)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_height", _height);
        info.AddValue("_width", _width);
        info.AddValue("offcolor", offcolor);
        info.AddValue("oncolor", oncolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap9 = (drawbitmap)base.Copy();
        drawbitmap9.oncolor = oncolor;
        drawbitmap9.offcolor = offcolor;
        drawbitmap9.varname = varname;
        drawbitmap9.br1 = br1;
        return drawbitmap9;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 0, 15 * _width / 58, 15 * _height / 100);
        g.DrawArc(Pens.Black, rect, 0f, 360f);
        g.FillPie(br1, rect, 0f, 360f);
        Rectangle rect2 = new(0, 15 * _height / 100, 15 * _width / 58, 70 * _height / 100);
        g.DrawRectangle(Pens.Black, rect2);
        g.FillRectangle(br1, rect2);
        Rectangle rect3 = new(0, 85 * _height / 100, 15 * _width / 58, 15 * _height / 100);
        g.DrawArc(Pens.Black, rect3, 0f, 360f);
        g.FillPie(br1, rect3, 0f, 360f);
        Rectangle rect4 = new(15 * _width / 58, 45 * _height / 100, 30 * _width / 58, 10 * _height / 100);
        g.DrawRectangle(Pens.Black, rect4);
        g.FillRectangle(br1, rect4);
        Point[] points = new Point[5]
        {
            new(35 * _width / 58, 0),
            new(55 * _width / 58, 0),
            new(45 * _width / 58, 50 * _height / 100),
            new(35 * _width / 58, 100 * _height / 100),
            new(55 * _width / 58, 100 * _height / 100)
        };
        g.DrawPolygon(Pens.Black, points);
        g.FillPolygon(br1, points);
        Rectangle rect5 = new(40 * _width / 58, 40 * _height / 100, 10 * _width / 58, 20 * _height / 100);
        g.DrawArc(Pens.Black, rect5, 0f, 360f);
        g.FillPie(br1, rect5, 0f, 360f);
    }

    public override void ManageMouseDown(MouseEventArgs e)
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

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 80;
                _width = 50;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            new SolidBrush(oncolor);
            new SolidBrush(offcolor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                if (Value)
                {
                    SolidBrush brush = new(oncolor);
                    Rectangle rect = new(0, 0, 15 * _width / 58, 15 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect, 0f, 360f);
                    graphics.FillPie(brush, rect, 0f, 360f);
                    Rectangle rect2 = new(0, 15 * _height / 100, 15 * _width / 58, 70 * _height / 100);
                    graphics.DrawRectangle(Pens.Black, rect2);
                    graphics.FillRectangle(brush, rect2);
                    Rectangle rect3 = new(0, 85 * _height / 100, 15 * _width / 58, 15 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect3, 0f, 360f);
                    graphics.FillPie(brush, rect3, 0f, 360f);
                    Rectangle rect4 = new(15 * _width / 58, 45 * _height / 100, 30 * _width / 58, 10 * _height / 100);
                    graphics.DrawRectangle(Pens.Black, rect4);
                    graphics.FillRectangle(brush, rect4);
                    Point[] points = new Point[5]
                    {
                        new(35 * _width / 58, 0),
                        new(55 * _width / 58, 0),
                        new(45 * _width / 58, 50 * _height / 100),
                        new(35 * _width / 58, 100 * _height / 100),
                        new(55 * _width / 58, 100 * _height / 100)
                    };
                    graphics.DrawPolygon(Pens.Black, points);
                    graphics.FillPolygon(brush, points);
                    Rectangle rect5 = new(40 * _width / 58, 40 * _height / 100, 10 * _width / 58, 20 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect5, 0f, 360f);
                    graphics.FillPie(brush, rect5, 0f, 360f);
                }
                else
                {
                    SolidBrush brush2 = new(offcolor);
                    Rectangle rect6 = new(0, 0, 15 * _width / 58, 15 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect6, 0f, 360f);
                    graphics.FillPie(brush2, rect6, 0f, 360f);
                    Rectangle rect7 = new(0, 15 * _height / 100, 15 * _width / 58, 70 * _height / 100);
                    graphics.DrawRectangle(Pens.Black, rect7);
                    graphics.FillRectangle(brush2, rect7);
                    Rectangle rect8 = new(0, 85 * _height / 100, 15 * _width / 58, 15 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect8, 0f, 360f);
                    graphics.FillPie(brush2, rect8, 0f, 360f);
                    Rectangle rect9 = new(15 * _width / 58, 45 * _height / 100, 30 * _width / 58, 10 * _height / 100);
                    graphics.DrawRectangle(Pens.Black, rect9);
                    graphics.FillRectangle(brush2, rect9);
                    Point[] points2 = new Point[5]
                    {
                        new(35 * _width / 58, 0),
                        new(55 * _width / 58, 0),
                        new(45 * _width / 58, 50 * _height / 100),
                        new(35 * _width / 58, 100 * _height / 100),
                        new(55 * _width / 58, 100 * _height / 100)
                    };
                    graphics.DrawPolygon(Pens.Black, points2);
                    graphics.FillPolygon(brush2, points2);
                    Rectangle rect10 = new(40 * _width / 58, 40 * _height / 100, 10 * _width / 58, 20 * _height / 100);
                    graphics.DrawArc(Pens.Black, rect10, 0f, 360f);
                    graphics.FillPie(brush2, rect10, 0f, 360f);
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
        FMSet1 fMSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            fMSet.varname = varname;
            fMSet.oncolor = oncolor;
            fMSet.offcolor = offcolor;
        }
        fMSet.viewevent += GetTable;
        fMSet.ckvarevent += CheckVar;
        if (fMSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + fMSet.varname + "]";
            oncolor = fMSet.oncolor;
            offcolor = fMSet.offcolor;
            br1.Color = offcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            offcolor = offcolor,
            oncolor = oncolor,
            varname = varname
        };
        formatter.Serialize(memoryStream, bSave);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void Deserialize(byte[] data)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(data);
            BSave2 bSave = (BSave2)formatter.Deserialize(stream);
            stream.Close();
            oncolor = bSave.oncolor;
            offcolor = bSave.offcolor;
            varname = bSave.varname;
            br1 = new SolidBrush(Color.Red);
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
