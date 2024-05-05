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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class drawbitmap6 : CPixieControl
{
    private Color setcolor = Color.Red;

    private Color setbgcolor = Color.Gray;

    private string varname = "";

    private int initflag = 1;

    private int _width = 200;

    private int _height = 300;

    private bool opstflag;

    private bool value;

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("开关颜色")]
    [Category("杂项")]
    [Description("开关颜色。")]
    public Color MyControlColor
    {
        get
        {
            return setcolor;
        }
        set
        {
            setcolor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("背景颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("背景颜色。")]
    public Color MyControlbgColor
    {
        get
        {
            return setbgcolor;
        }
        set
        {
            setbgcolor = value;
        }
    }

    [Description("开关绑定变量。")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
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
    [DHMICtrlProperty]
    [DisplayName("是否取反")]
    [Description("是否取反。")]
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

    [ReadOnly(false)]
    [DisplayName("绑定变量当前值")]
    [Category("杂项")]
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
            else if (enumerator.Name == "initflag")
            {
                initflag = (int)enumerator.Value;
            }
            else if (enumerator.Name == "opstflag")
            {
                opstflag = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "setcolor")
            {
                setcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "value")
            {
                value = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "varname")
            {
                varname = (string)enumerator.Value;
            }
            else if (enumerator.Name == "setbgcolor")
            {
                setbgcolor = (Color)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_height", _height);
        info.AddValue("_width", _width);
        info.AddValue("initflag", initflag);
        info.AddValue("opstflag", opstflag);
        info.AddValue("setcolor", setcolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
        info.AddValue("setbgcolor", setbgcolor);
    }

    public override CShape Copy()
    {
        drawbitmap6 drawbitmap10 = (drawbitmap6)base.Copy();
        drawbitmap10.setcolor = setcolor;
        drawbitmap10.setbgcolor = setbgcolor;
        drawbitmap10.opstflag = opstflag;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(setbgcolor);
        SolidBrush brush2 = new(setcolor);
        Rectangle rect = new(0, 0, 200 * _width / 200, 200 * _height / 200);
        g.FillRectangle(brush, rect);
        g.FillPolygon(points: new Point[6]
        {
            new(70 * _width / 200, 48 * _height / 200),
            new(130 * _width / 200, 48 * _height / 200),
            new(160 * _width / 200, 100 * _height / 200),
            new(130 * _width / 200, 152 * _height / 200),
            new(70 * _width / 200, 152 * _height / 200),
            new(40 * _width / 200, 100 * _height / 200)
        }, brush: Brushes.Gray);
        g.DrawArc(rect: new Rectangle(55 * _width / 200, 55 * _height / 200, 90 * _width / 200, 90 * _height / 200), pen: Pens.Black, startAngle: 0f, sweepAngle: 360f);
        Rectangle rect3 = new(70 * _width / 200, 70 * _height / 200, 60 * _width / 200, 60 * _height / 200);
        g.FillPie(brush2, rect3, 0f, 360f);
        if (initflag == 1)
        {
            Rectangle rect4 = new(50 * _width / 200, 80 * _height / 200, 90 * _width / 200, 90 * _height / 200);
            g.DrawArc(Pens.Black, rect4, 0f, 360f);
            g.FillPie(brush2, rect4, 0f, 360f);
            g.FillPie(rect: new Rectangle(90 * _width / 200, 95 * _height / 200, 20 * _width / 200, 20 * _height / 200), brush: Brushes.White, startAngle: 0f, sweepAngle: 360f);
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
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                initflag = 0;
                SolidBrush brush = new(setcolor);
                Paint(graphics);
                if (opstflag)
                {
                    if (!Value)
                    {
                        Rectangle rect = new(50 * _width / 200, 30 * _height / 200, 90 * _width / 200, 90 * _height / 200);
                        graphics.FillPie(brush, rect, 0f, 360f);
                        graphics.DrawArc(Pens.Black, rect, 0f, 360f);
                        graphics.FillPie(rect: new Rectangle(90 * _width / 200, 70 * _height / 200, 20 * _width / 200, 20 * _height / 200), brush: Brushes.White, startAngle: 0f, sweepAngle: 360f);
                    }
                    else
                    {
                        Rectangle rect3 = new(50 * _width / 200, 80 * _height / 200, 90 * _width / 200, 90 * _height / 200);
                        graphics.DrawArc(Pens.Black, rect3, 0f, 360f);
                        graphics.FillPie(brush, rect3, 0f, 360f);
                        graphics.FillPie(rect: new Rectangle(90 * _width / 200, 95 * _height / 200, 20 * _width / 200, 20 * _height / 200), brush: Brushes.White, startAngle: 0f, sweepAngle: 360f);
                    }
                }
                else if (Value)
                {
                    Rectangle rect5 = new(50 * _width / 200, 30 * _height / 200, 90 * _width / 200, 90 * _height / 200);
                    graphics.FillPie(brush, rect5, 0f, 360f);
                    graphics.DrawArc(Pens.Black, rect5, 0f, 360f);
                    graphics.FillPie(rect: new Rectangle(90 * _width / 200, 70 * _height / 200, 20 * _width / 200, 20 * _height / 200), brush: Brushes.White, startAngle: 0f, sweepAngle: 360f);
                }
                else
                {
                    Rectangle rect7 = new(50 * _width / 200, 80 * _height / 200, 90 * _width / 200, 90 * _height / 200);
                    graphics.DrawArc(Pens.Black, rect7, 0f, 360f);
                    graphics.FillPie(brush, rect7, 0f, 360f);
                    graphics.FillPie(rect: new Rectangle(90 * _width / 200, 95 * _height / 200, 20 * _width / 200, 20 * _height / 200), brush: Brushes.White, startAngle: 0f, sweepAngle: 360f);
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
        KGSet1 kGSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.oppsiteflag = opstflag;
            kGSet.setcolor = setcolor;
            kGSet.varname = varname;
            kGSet.setbgcolor = setbgcolor;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.oppsiteflag;
            setcolor = kGSet.setcolor;
            setbgcolor = kGSet.setbgcolor;
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
