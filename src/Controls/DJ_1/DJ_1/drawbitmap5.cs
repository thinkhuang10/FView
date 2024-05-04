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
public class drawbitmap5 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int mytimer = 1;

    private int _width = 200;

    private int _height = 200;

    [NonSerialized]
    private Timer tm1 = new();

    private bool value;

    [DisplayName("绑定变量")]
    [ReadOnly(false)]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("控件绑定变量。")]
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
    [Category("杂项")]
    [DisplayName("开颜色")]
    [ReadOnly(false)]
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

    [Description("阀门为关状态时显示的颜色。")]
    [Category("杂项")]
    [DisplayName("关颜色")]
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

    [DisplayName("当前值")]
    [Category("杂项")]
    [Description("指示阀门当前状态。")]
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

    public drawbitmap5()
    {
    }

    protected drawbitmap5(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap5 info");
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
        drawbitmap5 drawbitmap6 = (drawbitmap5)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 30 * _height / 200, 60 * _width / 200, 80 * _height / 200);
        Rectangle rect2 = new(0, 30 * _height / 200, 150 * _width / 200, 40 * _height / 200);
        Rectangle rect3 = new(0, 70 * _height / 200, 150 * _width / 200, 40 * _height / 200);
        LinearGradientBrush brush = new(rect2, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush2 = new(rect3, Color.LightGray, Color.Gray, 90f);
        g.FillPie(brush, rect, 180f, 90f);
        g.FillPie(brush2, rect, 90f, 90f);
        Rectangle rect4 = new(29 * _width / 200, 30 * _height / 200, 101 * _width / 200, 40 * _height / 200);
        Rectangle rect5 = new(29 * _width / 200, 70 * _height / 200, 101 * _width / 200, 40 * _height / 200);
        g.FillRectangle(brush, rect4);
        g.FillRectangle(brush2, rect5);
        Rectangle rect6 = new(130 * _width / 200, 35 * _height / 200, 20 * _width / 200, 35 * _height / 200);
        Rectangle rect7 = new(130 * _width / 200, 70 * _height / 200, 20 * _width / 200, 35 * _height / 200);
        LinearGradientBrush brush3 = new(rect6, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush4 = new(rect7, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush3, rect6);
        g.FillRectangle(brush4, rect7);
        Rectangle rect8 = new(150 * _width / 200, 45 * _height / 200, 15 * _width / 200, 25 * _height / 200);
        Rectangle rect9 = new(150 * _width / 200, 70 * _height / 200, 15 * _width / 200, 25 * _height / 200);
        LinearGradientBrush brush5 = new(rect8, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush6 = new(rect9, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush5, rect8);
        g.FillRectangle(brush6, rect9);
        Rectangle rect10 = new(165 * _width / 200, 63 * _height / 200, 35 * _width / 200, 7 * _height / 200);
        Rectangle rect11 = new(165 * _width / 200, 70 * _height / 200, 35 * _width / 200, 7 * _height / 200);
        LinearGradientBrush brush7 = new(rect10, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush8 = new(rect11, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush7, rect10);
        g.FillRectangle(brush8, rect11);
        Rectangle rect12 = new(30 * _width / 200, 37 * _height / 200, 80 * _width / 200, 7 * _height / 200);
        Rectangle rect13 = new(30 * _width / 200, 44 * _height / 200, 80 * _width / 200, 7 * _height / 200);
        LinearGradientBrush brush9 = new(rect12, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect13, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect12);
        g.FillRectangle(brush10, rect13);
        g.DrawLine(new Pen(Color.LightGray, 3f), 30 * _width / 200, 44 * _height / 200, 110 * _width / 200, 44 * _height / 200);
        Rectangle rect14 = new(30 * _width / 200, 90 * _height / 200, 80 * _width / 200, 5 * _height / 200);
        Rectangle rect15 = new(30 * _width / 200, 95 * _height / 200, 80 * _width / 200, 5 * _height / 200);
        LinearGradientBrush brush11 = new(rect14, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush12 = new(rect15, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush11, rect14);
        g.FillRectangle(brush12, rect15);
        g.DrawLine(new Pen(Color.LightGray, 3f), 30 * _width / 200, 95 * _height / 200, 110 * _width / 200, 95 * _height / 200);
        Rectangle rect16 = new(130 * _width / 200, 30 * _height / 200, 10 * _width / 200, 10 * _height / 200);
        Rectangle rect17 = new(130 * _width / 200, 53 * _height / 200, 10 * _width / 200, 10 * _height / 200);
        Rectangle rect18 = new(130 * _width / 200, 77 * _height / 200, 10 * _width / 200, 10 * _height / 200);
        Rectangle rect19 = new(130 * _width / 200, 100 * _height / 200, 10 * _width / 200, 10 * _height / 200);
        g.FillRectangle(Brushes.DarkGray, rect16);
        g.FillRectangle(Brushes.DarkGray, rect17);
        g.FillRectangle(Brushes.DarkGray, rect18);
        g.FillRectangle(Brushes.DarkGray, rect19);
        g.FillPolygon(points: new Point[6]
        {
            new(50 * _width / 200, 20 * _height / 200),
            new(60 * _width / 200, 0),
            new(70 * _width / 200, 0),
            new(80 * _width / 200, 20 * _height / 200),
            new(70 * _width / 200, 40 * _height / 200),
            new(60 * _width / 200, 40 * _height / 200)
        }, brush: Brushes.LightGray);
        g.DrawLine(new Pen(Color.LightGray, 3f), 0, 70 * _height / 200, 200 * _width / 200, 70 * _height / 200);
        Rectangle rect20 = new(90 * _width / 200, 30 * _height / 200, 40 * _width / 200, 170 * _height / 200);
        new Rectangle(90 * _width / 200, 30 * _height / 200, 40 * _width / 200, 30 * _height / 200);
        Rectangle rect21 = new(90 * _width / 200, 60 * _height / 200, 40 * _width / 200, 30 * _height / 200);
        Rectangle rect22 = new(90 * _width / 200, 90 * _height / 200, 40 * _width / 200, 30 * _height / 200);
        g.FillRectangle(Brushes.LightGray, rect20);
        g.FillRectangle(Brushes.Gray, rect21);
        g.FillRectangle(Brushes.DarkGray, rect22);
        g.FillPie(rect: new Rectangle(95 * _width / 200, 140 * _height / 200, 30 * _width / 200, 30 * _height / 200), brush: Brushes.Gray, startAngle: 0f, sweepAngle: 360f);
        Pen pen = new(Color.LightGray, 5f);
        Rectangle rect24 = new(102 * _width / 200, 147 * _height / 200, 16 * _width / 200, 16 * _height / 200);
        g.DrawArc(pen, rect24, 0f, 360f);
        g.DrawRectangle(rect: new Rectangle(95 * _width / 200, 185 * _height / 200, 30 * _width / 200, 15 * _height / 200), pen: Pens.Black);
        if (initflag == 1)
        {
            Rectangle rect26 = new(58 * _width / 200, 8 * _height / 200, 14 * _width / 200, 24 * _height / 200);
            SolidBrush brush13 = new(offcolor);
            g.FillRectangle(brush13, rect26);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Width == 0f || Height == 0f)
            {
                _width = 200;
                _height = 200;
            }
            else
            {
                _width = Convert.ToInt32(Math.Abs(Width));
                _height = Convert.ToInt32(Math.Abs(Height));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new(oncolor);
            SolidBrush brush2 = new(offcolor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                initflag = 0;
                Paint(graphics);
                if (Value)
                {
                    Rectangle rect = new(58 * _width / 200, 8 * _height / 200, 14 * _width / 200, 24 * _height / 200);
                    graphics.FillRectangle(brush, rect);
                }
                else
                {
                    Rectangle rect2 = new(58 * _width / 200, 8 * _height / 200, 14 * _width / 200, 24 * _height / 200);
                    graphics.FillRectangle(brush2, rect2);
                }
                mytimer++;
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
