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
public class drawbitmap6 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private Color colorfill = Color.Gray;

    private int _width = 50;

    private int _height = 80;

    private int initflag = 1;

    private bool value;

    [NonSerialized]
    private SolidBrush br1 = new(Color.Red);

    [ReadOnly(false)]
    [Description("控件绑定变量。")]
    [DisplayName("绑定变量")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DHMICtrlProperty]
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

    [ReadOnly(false)]
    [DisplayName("开颜色")]
    [Description("阀门为开状态时显示的颜色。")]
    [Category("杂项")]
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

    [Category("杂项")]
    [DisplayName("关颜色")]
    [Description("阀门为关状态时显示的颜色。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [Description("指示阀门当前状态。")]
    [DisplayName("当前值")]
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
                ValueChanged?.Invoke(this, null);
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public event EventHandler ValueChanged;

    public drawbitmap6()
    {
    }

    protected drawbitmap6(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap6 info");
        }
        drawbitmap6 obj = new();
        FieldInfo[] fields = typeof(drawbitmap6).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap6))
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
            else if (enumerator.Name == "colorfill")
            {
                colorfill = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "initflag")
            {
                initflag = (int)enumerator.Value;
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
        info.AddValue("colorfill", colorfill);
        info.AddValue("initflag", initflag);
        info.AddValue("offcolor", offcolor);
        info.AddValue("oncolor", oncolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override CShape Copy()
    {
        drawbitmap6 drawbitmap9 = (drawbitmap6)base.Copy();
        drawbitmap9.oncolor = oncolor;
        drawbitmap9.offcolor = offcolor;
        drawbitmap9.varname = varname;
        drawbitmap9.br1 = br1;
        drawbitmap9.colorfill = colorfill;
        return drawbitmap9;
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

    public void Paint(Graphics g)
    {
        if (initflag == 1)
        {
            colorfill = offcolor;
        }
        SolidBrush brush = new(colorfill);
        Rectangle rect = new(5 * _width / 50, 0, 40 * _width / 50, 15 * _height / 55);
        g.FillRectangle(brush, rect);
        g.DrawRectangle(Pens.Black, rect);
        Rectangle rect2 = new(20 * _width / 50, 15 * _height / 55, 5 * _width / 50, 30 * _height / 55);
        Rectangle rect3 = new(25 * _width / 50, 15 * _height / 55, 5 * _width / 50, 30 * _height / 55);
        LinearGradientBrush brush2 = new(rect2, colorfill, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush3 = new(rect3, Color.White, colorfill, LinearGradientMode.Horizontal);
        g.FillRectangle(brush2, rect2);
        g.FillRectangle(brush3, rect3);
        g.DrawLine(new Pen(Color.White, 3f), 25 * _width / 50, 15 * _height / 55, 25 * _width / 50, 45 * _height / 55);
        Point[] points = new Point[5]
        {
            new(0, 45 * _height / 55),
            new(0, 35 * _height / 55),
            new(25 * _width / 50, 45 * _height / 55),
            new(50 * _width / 50, 35 * _height / 55),
            new(50 * _width / 50, 45 * _height / 55)
        };
        Point[] points2 = new Point[5]
        {
            new(0, 45 * _height / 55),
            new(0, 55 * _height / 55),
            new(25 * _width / 50, 45 * _height / 55),
            new(50 * _width / 50, 55 * _height / 55),
            new(50 * _width / 50, 45 * _height / 55)
        };
        Rectangle rect4 = new(0, 35 * _height / 55, 50 * _width / 50, 10 * _height / 55);
        Rectangle rect5 = new(0, 35 * _height / 55, 50 * _width / 50, 10 * _height / 55);
        LinearGradientBrush brush4 = new(rect4, colorfill, Color.White, 90f);
        LinearGradientBrush brush5 = new(rect5, Color.White, colorfill, 90f);
        g.FillPolygon(brush4, points);
        g.FillPolygon(brush5, points2);
        g.DrawLine(new Pen(Color.White, 3f), 0, 45 * _height / 55, 50 * _width / 50, 45 * _height / 55);
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
                initflag = 0;
                if (Value)
                {
                    colorfill = oncolor;
                    Paint(graphics);
                }
                else
                {
                    colorfill = offcolor;
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
