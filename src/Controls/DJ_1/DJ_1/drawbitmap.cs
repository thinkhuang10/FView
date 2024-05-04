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

namespace DJ_1;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private bool value;

    private int _width = 200;

    private int _height = 180;

    [NonSerialized]
    private Timer tm1 = new();

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
        info.AddValue("initflag", initflag);
        info.AddValue("offcolor", offcolor);
        info.AddValue("oncolor", oncolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
    }

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap6 = (drawbitmap)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.LightGray, 4f);
        Rectangle rect = new(50 * _width / 155, 95 * _height / 140, 35 * _width / 155, 40 * _height / 140);
        Rectangle rect2 = new(85 * _width / 155, 95 * _height / 140, 35 * _width / 155, 40 * _height / 140);
        Rectangle rect3 = new(0, 0, 200 * _width / 155, 50 * _height / 140);
        Rectangle rect4 = new(0, 50 * _height / 140, 200 * _width / 155, 50 * _height / 140);
        LinearGradientBrush brush = new(rect3, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush2 = new(rect4, Color.LightGray, Color.Gray, 90f);
        LinearGradientBrush brush3 = new(rect3, Color.LightGray, Color.White, 90f);
        LinearGradientBrush brush4 = new(rect4, Color.White, Color.LightGray, 90f);
        LinearGradientBrush brush5 = new(rect, Color.Gray, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect2, Color.White, Color.Gray, LinearGradientMode.Horizontal);
        Rectangle rect5 = new(0, 45 * _height / 140, 25 * _width / 155, 5 * _height / 140);
        Rectangle rect6 = new(0, 50 * _height / 140, 25 * _width / 155, 5 * _height / 140);
        Rectangle rect7 = new(25 * _width / 155, 0, 10 * _width / 155, 50 * _height / 140);
        Rectangle rect8 = new(25 * _width / 155, 50 * _height / 140, 10 * _width / 155, 50 * _height / 140);
        Rectangle rect9 = new(25 * _width / 155, 0, 10 * _width / 155, 100 * _height / 140);
        Rectangle rect10 = new(35 * _width / 155, 5 * _height / 140, 100 * _width / 155, 45 * _height / 140);
        Rectangle rect11 = new(35 * _width / 155, 50 * _height / 140, 100 * _width / 155, 45 * _height / 140);
        Point[] points = new Point[4]
        {
            new(135 * _width / 155, 5 * _height / 140),
            new(135 * _width / 155, 50 * _height / 140),
            new(150 * _width / 155, 50 * _height / 140),
            new(150 * _width / 155, 20 * _height / 140)
        };
        Point[] points2 = new Point[4]
        {
            new(135 * _width / 155, 50 * _height / 140),
            new(135 * _width / 155, 95 * _height / 140),
            new(150 * _width / 155, 80 * _height / 140),
            new(150 * _width / 155, 50 * _height / 140)
        };
        Point[] points3 = new Point[4]
        {
            new(135 * _width / 155, 5 * _height / 140),
            new(135 * _width / 155, 95 * _height / 140),
            new(150 * _width / 155, 80 * _height / 140),
            new(150 * _width / 155, 20 * _height / 140)
        };
        Rectangle rect12 = new(57 * _width / 155, 95 * _height / 140, 28 * _width / 155, 10 * _height / 140);
        Rectangle rect13 = new(85 * _width / 155, 95 * _height / 140, 28 * _width / 155, 10 * _height / 140);
        Point[] points4 = new Point[4]
        {
            new(57 * _width / 155, 105 * _height / 140),
            new(85 * _width / 155, 105 * _height / 140),
            new(85 * _width / 155, 115 * _height / 140),
            new(50 * _width / 155, 115 * _height / 140)
        };
        Point[] points5 = new Point[4]
        {
            new(85 * _width / 155, 105 * _height / 140),
            new(85 * _width / 155, 115 * _height / 140),
            new(120 * _width / 155, 115 * _height / 140),
            new(113 * _width / 155, 105 * _height / 140)
        };
        Rectangle rect14 = new(50 * _width / 155, 115 * _height / 140, 35 * _width / 155, 10 * _height / 140);
        Rectangle rect15 = new(85 * _width / 155, 115 * _height / 140, 35 * _width / 155, 10 * _height / 140);
        Rectangle rect16 = new(50 * _width / 155, 115 * _height / 140, 70 * _width / 155, 10 * _height / 140);
        Rectangle rect17 = new(60 * _width / 155, 60 * _height / 140, 75 * _width / 155, 7 * _height / 140);
        Rectangle rect18 = new(60 * _width / 155, 80 * _height / 140, 75 * _width / 155, 3 * _height / 140);
        Point[] points6 = new Point[6]
        {
            new(60 * _width / 155, 20 * _height / 140),
            new(70 * _width / 155, 0),
            new(95 * _width / 155, 0),
            new(105 * _width / 155, 20 * _height / 140),
            new(95 * _width / 155, 40 * _height / 140),
            new(70 * _width / 155, 40 * _height / 140)
        };
        Rectangle rect19 = new(70 * _width / 155, 6 * _height / 140, 25 * _width / 155, 28 * _height / 140);
        g.FillRectangle(brush, rect5);
        g.FillRectangle(brush2, rect6);
        g.FillRectangle(brush, rect7);
        g.FillRectangle(brush2, rect8);
        g.FillRectangle(brush, rect10);
        g.FillRectangle(brush2, rect11);
        g.FillPolygon(brush3, points);
        g.FillPolygon(brush4, points2);
        g.DrawLine(pen, 0, 50 * _height / 140, 135 * _width / 155, 50 * _height / 140);
        g.DrawLine(new Pen(Color.White, 3f), 135 * _width / 155, 50 * _height / 140, 150 * _width / 155, 50 * _height / 140);
        g.FillRectangle(brush5, rect12);
        g.FillRectangle(brush6, rect13);
        g.FillRectangle(brush5, rect14);
        g.FillRectangle(brush6, rect15);
        g.DrawRectangle(Pens.Black, rect16);
        g.DrawLine(new Pen(Color.White, 3f), 85 * _width / 155, 95 * _height / 140, 85 * _width / 155, 125 * _height / 140);
        g.FillPolygon(Brushes.LightGray, points4);
        g.FillPolygon(Brushes.LightGray, points5);
        g.FillRectangle(Brushes.Gray, rect17);
        g.FillRectangle(Brushes.Gray, rect18);
        g.FillPolygon(Brushes.LightGray, points6);
        g.DrawPolygon(Pens.Black, points6);
        g.DrawRectangle(Pens.Black, rect9);
        g.DrawPolygon(Pens.Black, points3);
        if (initflag == 1)
        {
            SolidBrush brush7 = new(offcolor);
            g.FillRectangle(brush7, rect19);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 180;
                _width = 200;
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
                Rectangle rect = new(70 * _width / 155, 6 * _height / 140, 25 * _width / 155, 28 * _height / 140);
                Paint(graphics);
                if (Value)
                {
                    SolidBrush brush = new(oncolor);
                    graphics.FillRectangle(brush, rect);
                }
                else
                {
                    SolidBrush brush2 = new(offcolor);
                    graphics.FillRectangle(brush2, rect);
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
        DJSet1 dJSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            dJSet.varname = varname;
            dJSet.oncolor = oncolor;
            dJSet.offcolor = offcolor;
        }
        dJSet.viewevent += GetTable;
        dJSet.ckvarevent += CheckVar;
        if (dJSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + dJSet.varname + "]";
            oncolor = dJSet.oncolor;
            offcolor = dJSet.offcolor;
            NeedRefresh = true;
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            offcolor = offcolor,
            oncolor = oncolor,
            varname = varname
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
            BSave2 bSave = (BSave2)formatter.Deserialize(stream);
            stream.Close();
            oncolor = bSave.oncolor;
            offcolor = bSave.offcolor;
            varname = bSave.varname;
            tm1 = new Timer
            {
                Interval = 500
            };
            tm1.Tick += tm1_Tick;
            tm1.Enabled = true;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
