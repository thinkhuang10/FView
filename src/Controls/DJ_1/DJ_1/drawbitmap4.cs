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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap4 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int mytimer = 1;

    private int _width = 200;

    private int _height = 130;

    [NonSerialized]
    private Timer tm1 = new();

    private bool value;

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

    [Description("阀门为开状态时显示的颜色。")]
    [DisplayName("开颜色")]
    [DHMICtrlProperty]
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("关颜色")]
    [Description("阀门为关状态时显示的颜色。")]
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
    [DisplayName("当前值")]
    [Category("杂项")]
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
        drawbitmap4 drawbitmap6 = (drawbitmap4)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        g.FillRectangle(rect: new Rectangle(70 * _width / 230, 40 * _height / 150, 60 * _width / 230, 95 * _height / 150), brush: Brushes.Black);
        Rectangle rect2 = new(10 * _width / 230, 45 * _height / 150, 60 * _width / 230, 20 * _height / 150);
        Rectangle rect3 = new(10 * _width / 230, 65 * _height / 150, 60 * _width / 230, 20 * _height / 150);
        LinearGradientBrush brush = new(rect2, Color.DarkBlue, Color.LightBlue, 90f);
        LinearGradientBrush brush2 = new(rect3, Color.LightBlue, Color.DarkBlue, 90f);
        g.FillRectangle(brush, rect2);
        g.FillRectangle(brush2, rect3);
        Rectangle rect4 = new(70 * _width / 230, 25 * _height / 150, 20 * _width / 230, 40 * _height / 150);
        Rectangle rect5 = new(70 * _width / 230, 65 * _height / 150, 20 * _width / 230, 40 * _height / 150);
        LinearGradientBrush brush3 = new(rect4, Color.DarkBlue, Color.LightBlue, 90f);
        LinearGradientBrush brush4 = new(rect5, Color.LightBlue, Color.DarkBlue, 90f);
        g.FillRectangle(brush3, rect4);
        g.FillRectangle(brush4, rect5);
        g.DrawLine(new Pen(Color.LightBlue, 3f), 10 * _width / 230, 65 * _height / 150, 90 * _width / 230, 65 * _height / 150);
        Rectangle rect6 = new(120 * _width / 230, 0, 20 * _width / 230, 30 * _height / 150);
        Rectangle rect7 = new(140 * _width / 230, 0, 20 * _width / 230, 30 * _height / 150);
        LinearGradientBrush brush5 = new(rect6, Color.DarkBlue, Color.LightBlue, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect7, Color.LightBlue, Color.DarkBlue, LinearGradientMode.Horizontal);
        g.FillRectangle(brush5, rect6);
        g.FillRectangle(brush6, rect7);
        g.DrawLine(new Pen(Color.LightBlue, 3f), 140 * _width / 230, 0, 140 * _width / 230, 30 * _height / 150);
        g.DrawLine(Pens.Black, 121 * _width / 230, 20 * _height / 150, 159 * _width / 230, 20 * _height / 150);
        Rectangle rect8 = new(100 * _width / 230, 30 * _height / 150, 80 * _width / 230, 40 * _height / 150);
        Rectangle rect9 = new(100 * _width / 230, 70 * _height / 150, 80 * _width / 230, 40 * _height / 150);
        LinearGradientBrush brush7 = new(rect8, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush8 = new(rect9, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush7, rect8);
        g.FillRectangle(brush8, rect9);
        g.DrawRectangle(rect: new Rectangle(110 * _width / 230, 32 * _height / 150, 60 * _width / 230, 20 * _height / 150), pen: Pens.Black);
        Rectangle rect11 = new(180 * _width / 230, 20 * _height / 150, 30 * _width / 230, 50 * _height / 150);
        Rectangle rect12 = new(180 * _width / 230, 70 * _height / 150, 30 * _width / 230, 50 * _height / 150);
        Rectangle rect13 = new(190 * _width / 230, 20 * _height / 150, 10 * _width / 230, 100 * _height / 150);
        LinearGradientBrush brush9 = new(rect11, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect12, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect11);
        g.FillRectangle(brush10, rect12);
        g.DrawLine(new Pen(Color.LightGray, 3f), 100 * _width / 230, 70 * _height / 150, 210 * _width / 230, 70 * _height / 150);
        g.FillRectangle(Brushes.Black, rect13);
        Point[] points = new Point[4]
        {
            new(210 * _width / 230, 30 * _height / 150),
            new(210 * _width / 230, 70 * _height / 150),
            new(225 * _width / 230, 70 * _height / 150),
            new(225 * _width / 230, 45 * _height / 150)
        };
        Point[] points2 = new Point[4]
        {
            new(210 * _width / 230, 110 * _height / 150),
            new(210 * _width / 230, 70 * _height / 150),
            new(225 * _width / 230, 70 * _height / 150),
            new(225 * _width / 230, 95 * _height / 150)
        };
        Rectangle rect14 = new(210 * _width / 230, 30 * _height / 150, 15 * _width / 230, 40 * _height / 150);
        Rectangle rect15 = new(210 * _width / 230, 70 * _height / 150, 15 * _width / 230, 40 * _height / 150);
        LinearGradientBrush brush11 = new(rect14, Color.LightGray, Color.White, 90f);
        LinearGradientBrush brush12 = new(rect15, Color.White, Color.LightGray, 90f);
        g.FillPolygon(brush11, points);
        g.FillPolygon(brush12, points2);
        g.DrawLine(new Pen(Color.White, 3f), 210 * _width / 230, 70 * _height / 150, 225 * _width / 230, 70 * _height / 150);
        Rectangle rect16 = new(0, 50 * _height / 150, 80 * _width / 230, 80 * _height / 150);
        Rectangle rect17 = new(15 * _width / 230, 65 * _height / 150, 50 * _width / 230, 50 * _height / 150);
        Rectangle rect18 = new(30 * _width / 230, 80 * _height / 150, 20 * _width / 230, 20 * _height / 150);
        g.FillPie(Brushes.DarkBlue, rect16, 0f, 360f);
        g.FillPie(Brushes.DarkBlue, rect17, 0f, 360f);
        g.DrawArc(Pens.Black, rect18, 0f, 360f);
        Rectangle rect19 = new(50 * _width / 230, 130 * _height / 150, 50 * _width / 230, 15 * _height / 150);
        Rectangle rect20 = new(100 * _width / 230, 130 * _height / 150, 50 * _width / 230, 15 * _height / 150);
        LinearGradientBrush brush13 = new(rect19, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush14 = new(rect20, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush13, rect19);
        g.FillRectangle(brush14, rect20);
        g.DrawLine(new Pen(Color.LightGray, 3f), 100 * _width / 230, 130 * _height / 150, 100 * _width / 230, 140 * _height / 150);
        g.FillRectangle(rect: new Rectangle(120 * _width / 230, 70 * _height / 150, 40 * _width / 230, 25 * _height / 150), brush: Brushes.Gray);
        if (initflag == 1)
        {
            Rectangle rect22 = new(125 * _width / 230, 72 * _height / 150, 30 * _width / 230, 21 * _height / 150);
            SolidBrush brush15 = new(offcolor);
            g.FillRectangle(brush15, rect22);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 130;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
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
                    Rectangle rect = new(125 * _width / 230, 72 * _height / 150, 30 * _width / 230, 21 * _height / 150);
                    graphics.FillRectangle(brush, rect);
                }
                else
                {
                    Rectangle rect2 = new(125 * _width / 230, 72 * _height / 150, 30 * _width / 230, 21 * _height / 150);
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
