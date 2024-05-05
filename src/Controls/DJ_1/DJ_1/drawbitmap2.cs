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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap2 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int mytimer = 1;

    private int _width = 200;

    private int _height = 140;

    [NonSerialized]
    private Timer tm1 = new();

    private bool value;

    [ReadOnly(false)]
    [Category("杂项")]
    [Description("控件绑定变量。")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
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

    [Description("阀门为开状态时显示的颜色。")]
    [DHMICtrlProperty]
    [DisplayName("开颜色")]
    [Category("杂项")]
    [ReadOnly(false)]
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

    [Description("阀门为关状态时显示的颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("关颜色")]
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

    [Description("指示阀门当前状态。")]
    [Category("杂项")]
    [DisplayName("当前值")]
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
                ValueChanged?.Invoke(this, null);
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public event EventHandler ValueChanged;

    public drawbitmap2()
    {
    }

    protected drawbitmap2(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap2 info");
        }
        drawbitmap2 obj = new();
        FieldInfo[] fields = typeof(drawbitmap2).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap2))
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
            else if (enumerator.Name == "mytimer")
            {
                mytimer = (int)enumerator.Value;
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
        info.AddValue("mytimer", mytimer);
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
        drawbitmap2 drawbitmap6 = (drawbitmap2)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        g.FillPie(rect: new Rectangle(0, 30 * _height / 150, 80 * _width / 215, 80 * _height / 150), brush: Brushes.SeaGreen, startAngle: 90f, sweepAngle: 180f);
        g.FillRectangle(rect: new Rectangle(30 * _width / 215, 30 * _height / 150, 120 * _width / 215, 80 * _height / 150), brush: Brushes.Gray);
        Rectangle rect3 = new(20 * _width / 215, 30 * _height / 150, 15 * _width / 215, 80 * _height / 150);
        g.DrawRectangle(Pens.Black, rect3);
        g.FillRectangle(Brushes.SeaGreen, rect3);
        Point[] points = new Point[4]
        {
            new(150 * _width / 215, 30 * _height / 150),
            new(150 * _width / 215, 70 * _height / 150),
            new(165 * _width / 215, 70 * _height / 150),
            new(165 * _width / 215, 45 * _height / 150)
        };
        Point[] points2 = new Point[4]
        {
            new(150 * _width / 215, 110 * _height / 150),
            new(150 * _width / 215, 70 * _height / 150),
            new(165 * _width / 215, 70 * _height / 150),
            new(165 * _width / 215, 95 * _height / 150)
        };
        Rectangle rect4 = new(150 * _width / 215, 30 * _height / 150, 50 * _width / 215, 40 * _height / 150);
        Rectangle rect5 = new(150 * _width / 215, 70 * _height / 150, 50 * _width / 215, 40 * _height / 150);
        LinearGradientBrush brush = new(rect4, Color.SeaGreen, Color.White, 90f);
        LinearGradientBrush brush2 = new(rect5, Color.White, Color.SeaGreen, 90f);
        g.FillPolygon(brush, points);
        g.FillPolygon(brush2, points2);
        g.DrawLine(new Pen(Color.White, 3f), 150 * _width / 215, 70 * _height / 150, 165 * _width / 215, 70 * _height / 150);
        g.FillRectangle(rect: new Rectangle(165 * _width / 215, 45 * _height / 150, 10 * _width / 215, 50 * _height / 150), brush: Brushes.DarkGreen);
        Rectangle rect7 = new(175 * _width / 215, 65 * _height / 150, 25 * _width / 215, 5 * _height / 150);
        Rectangle rect8 = new(175 * _width / 215, 70 * _height / 150, 25 * _width / 215, 5 * _height / 150);
        g.FillRectangle(brush, rect7);
        g.FillRectangle(brush2, rect8);
        g.DrawLine(new Pen(Color.White, 3f), 175 * _width / 215, 70 * _height / 150, 200 * _width / 215, 70 * _height / 150);
        g.FillPie(rect: new Rectangle(45 * _width / 215, 10 * _height / 150, 20 * _width / 215, 20 * _height / 150), brush: Brushes.SeaGreen, startAngle: 0f, sweepAngle: 360f);
        g.FillPie(rect: new Rectangle(50 * _width / 215, 15 * _height / 150, 10 * _width / 215, 10 * _height / 150), brush: Brushes.DarkGreen, startAngle: 0f, sweepAngle: 360f);
        Rectangle rect11 = new(80 * _width / 215, 0, 10 * _width / 215, 30 * _height / 150);
        Rectangle rect12 = new(90 * _width / 215, 0, 10 * _width / 215, 30 * _height / 150);
        LinearGradientBrush brush3 = new(rect11, Color.SeaGreen, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect11, Color.White, Color.SeaGreen, LinearGradientMode.Horizontal);
        g.FillRectangle(brush3, rect11);
        g.FillRectangle(brush4, rect12);
        g.DrawLine(new Pen(Color.White, 3f), 90 * _width / 215, 0, 90 * _width / 215, 30 * _height / 150);
        Rectangle rect13 = new(30 * _width / 215, 130 * _height / 150, 65 * _width / 215, 10 * _height / 150);
        Rectangle rect14 = new(95 * _width / 215, 130 * _height / 150, 65 * _width / 215, 10 * _height / 150);
        LinearGradientBrush brush5 = new(rect13, Color.SeaGreen, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect14, Color.White, Color.SeaGreen, LinearGradientMode.Horizontal);
        g.FillRectangle(brush5, rect13);
        g.FillRectangle(brush6, rect14);
        g.DrawLine(new Pen(Color.White, 3f), 95 * _width / 215, 130 * _height / 150, 95 * _width / 215, 140 * _height / 150);
        if (initflag == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                g.FillRectangle(rect: new Rectangle(35 * _width / 215, (30 + i * 10) * _height / 150, 115 * _width / 215, 5 * _height / 150), brush: Brushes.SeaGreen);
            }
            g.FillRectangle(rect: new Rectangle(90 * _width / 215, 100 * _height / 150, 30 * _width / 215, 30 * _height / 150), brush: Brushes.SeaGreen);
            Rectangle rect17 = new(98 * _width / 215, 108 * _height / 150, 14 * _width / 215, 14 * _height / 150);
            SolidBrush brush7 = new(offcolor);
            g.FillRectangle(brush7, rect17);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 140;
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
                if (Value)
                {
                    Paint(graphics);
                    int num = mytimer % 2;
                    for (int i = 0; i < 8; i++)
                    {
                        graphics.FillRectangle(rect: new Rectangle(35 * _width / 215, (30 + i * 10 + num * 5) * _height / 150, 115 * _width / 215, 5 * _height / 150), brush: Brushes.SeaGreen);
                    }
                    graphics.FillRectangle(rect: new Rectangle(90 * _width / 215, 100 * _height / 150, 30 * _width / 215, 30 * _height / 150), brush: Brushes.SeaGreen);
                    Rectangle rect3 = new(98 * _width / 215, 108 * _height / 150, 14 * _width / 215, 14 * _height / 150);
                    SolidBrush brush = new(oncolor);
                    graphics.FillRectangle(brush, rect3);
                    mytimer++;
                }
                else
                {
                    Paint(graphics);
                    for (int j = 0; j < 8; j++)
                    {
                        graphics.FillRectangle(rect: new Rectangle(35 * _width / 215, (30 + j * 10) * _height / 150, 115 * _width / 215, 5 * _height / 150), brush: Brushes.SeaGreen);
                    }
                    graphics.FillRectangle(rect: new Rectangle(90 * _width / 215, 100 * _height / 150, 30 * _width / 215, 30 * _height / 150), brush: Brushes.SeaGreen);
                    Rectangle rect6 = new(98 * _width / 215, 108 * _height / 150, 14 * _width / 215, 14 * _height / 150);
                    SolidBrush brush2 = new(offcolor);
                    graphics.FillRectangle(brush2, rect6);
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
                Interval = 200
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
