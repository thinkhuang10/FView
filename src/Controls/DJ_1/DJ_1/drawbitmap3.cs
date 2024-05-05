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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap3 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int mytimer = 1;

    private int _width = 200;

    private int _height = 108;

    [NonSerialized]
    private Timer tm1 = new();

    private bool value;

    [Category("杂项")]
    [DisplayName("绑定变量")]
    [Description("控件绑定变量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [DisplayName("开颜色")]
    [Category("杂项")]
    [Description("阀门为开状态时显示的颜色。")]
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

    [ReadOnly(false)]
    [DisplayName("关颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DisplayName("当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
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

    public drawbitmap3()
    {
    }

    protected drawbitmap3(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap3 info");
        }
        drawbitmap3 obj = new();
        FieldInfo[] fields = typeof(drawbitmap3).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap3))
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
        drawbitmap3 drawbitmap6 = (drawbitmap3)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        g.FillPie(rect: new Rectangle(0, 5 * _height / 115, 80 * _width / 210, 80 * _height / 115), brush: Brushes.Gray, startAngle: 90f, sweepAngle: 180f);
        Rectangle rect2 = new(30 * _width / 210, 5 * _height / 115, 120 * _width / 210, 40 * _height / 115);
        Rectangle rect3 = new(30 * _width / 210, 45 * _height / 115, 120 * _width / 210, 40 * _height / 115);
        LinearGradientBrush brush = new(rect2, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush2 = new(rect2, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush, rect2);
        g.FillRectangle(brush2, rect3);
        g.DrawLine(new Pen(Color.LightGray, 3f), 30 * _width / 210, 45 * _height / 115, 150 * _width / 210, 45 * _height / 115);
        g.FillRectangle(rect: new Rectangle(55 * _width / 210, 22 * _height / 115, 70 * _width / 210, 46 * _height / 115), brush: Brushes.Blue);
        Point[] points = new Point[4]
        {
            new(150 * _width / 210, 5 * _height / 115),
            new(150 * _width / 210, 45 * _height / 115),
            new(165 * _width / 210, 45 * _height / 115),
            new(165 * _width / 210, 20 * _height / 115)
        };
        Point[] points2 = new Point[4]
        {
            new(165 * _width / 210, 45 * _height / 115),
            new(150 * _width / 210, 45 * _height / 115),
            new(150 * _width / 210, 85 * _height / 115),
            new(165 * _width / 210, 70 * _height / 115)
        };
        Rectangle rect5 = new(150 * _width / 210, 5 * _height / 115, 40 * _width / 210, 40 * _height / 115);
        new Rectangle(150 * _width / 210, 5 * _height / 115, 40 * _width / 210, 40 * _height / 115);
        LinearGradientBrush brush3 = new(rect5, Color.LightGray, Color.White, 90f);
        LinearGradientBrush brush4 = new(rect5, Color.White, Color.LightGray, 90f);
        g.FillPolygon(brush3, points);
        g.FillPolygon(brush4, points2);
        g.DrawLine(new Pen(Color.White, 3f), 150 * _width / 210, 45 * _height / 115, 165 * _width / 210, 45 * _height / 115);
        g.FillRectangle(rect: new Rectangle(165 * _width / 210, 20 * _height / 115, 10 * _width / 210, 50 * _height / 115), brush: Brushes.Gray);
        g.FillRectangle(rect: new Rectangle(175 * _width / 210, 40 * _height / 115, 25 * _width / 210, 10 * _height / 115), brush: Brushes.LightGray);
        g.FillRectangle(rect: new Rectangle(70 * _width / 210, 85 * _height / 115, 40 * _width / 210, 15 * _height / 115), brush: Brushes.Gray);
        Rectangle rect9 = new(40 * _width / 210, 100 * _height / 115, 50 * _width / 210, 10 * _height / 115);
        Rectangle rect10 = new(90 * _width / 210, 100 * _height / 115, 50 * _width / 210, 10 * _height / 115);
        LinearGradientBrush brush5 = new(rect9, Color.Gray, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect10, Color.White, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush5, rect9);
        g.FillRectangle(brush6, rect10);
        g.DrawLine(new Pen(Color.White, 3f), 90 * _width / 210, 100 * _height / 115, 90 * _width / 210, 110 * _height / 115);
        Rectangle rect11 = new(35 * _width / 210, 5 * _height / 115, 115 * _width / 210, 8 * _height / 115);
        Rectangle rect12 = new(35 * _width / 210, 13 * _height / 115, 115 * _width / 210, 8 * _height / 115);
        Rectangle rect13 = new(35 * _width / 210, 77 * _height / 115, 115 * _width / 210, 8 * _height / 115);
        Rectangle rect14 = new(35 * _width / 210, 69 * _height / 115, 115 * _width / 210, 8 * _height / 115);
        g.DrawRectangle(Pens.Black, rect11);
        g.DrawRectangle(Pens.Black, rect12);
        g.DrawRectangle(Pens.Black, rect13);
        g.DrawRectangle(Pens.Black, rect14);
        if (initflag == 1)
        {
            Rectangle rect15 = new(35 * _width / 210, 28 * _height / 115, 115 * _width / 210, 7 * _height / 115);
            Rectangle rect16 = new(35 * _width / 210, 42 * _height / 115, 115 * _width / 210, 7 * _height / 115);
            Rectangle rect17 = new(35 * _width / 210, 55 * _height / 115, 115 * _width / 210, 7 * _height / 115);
            g.DrawRectangle(Pens.Black, rect15);
            g.FillRectangle(Brushes.White, rect15);
            g.DrawRectangle(Pens.Black, rect16);
            g.FillRectangle(Brushes.White, rect16);
            g.DrawRectangle(Pens.Black, rect17);
            g.FillRectangle(Brushes.White, rect17);
            Rectangle rect18 = new(90 * _width / 210, 30 * _height / 115, 30 * _width / 210, 25 * _height / 115);
            Rectangle rect19 = new(95 * _width / 210, 33 * _height / 115, 20 * _width / 210, 19 * _height / 115);
            g.FillRectangle(Brushes.Gray, rect18);
            SolidBrush brush7 = new(offcolor);
            g.FillRectangle(brush7, rect19);
            Rectangle rect20 = new(90 * _width / 210, 0, 30 * _width / 210, 15 * _height / 115);
            Rectangle rect21 = new(90 * _width / 210, 15 * _height / 115, 30 * _width / 210, 15 * _height / 115);
            LinearGradientBrush brush8 = new(rect20, Color.Blue, Color.LightBlue, 90f);
            LinearGradientBrush brush9 = new(rect20, Color.LightBlue, Color.Blue, 90f);
            g.FillRectangle(brush8, rect20);
            g.FillRectangle(brush9, rect21);
            g.DrawLine(new Pen(Color.LightBlue, 3f), 90 * _width / 210, 15 * _height / 115, 120 * _width / 210, 15 * _height / 115);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 108;
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
                if (Value)
                {
                    int num = mytimer % 2;
                    Paint(graphics);
                    Rectangle rect = new(35 * _width / 210, (21 + num * 7) * _height / 115, 115 * _width / 210, 7 * _height / 115);
                    Rectangle rect2 = new(35 * _width / 210, (35 + num * 7) * _height / 115, 115 * _width / 210, 7 * _height / 115);
                    Rectangle rect3 = new(35 * _width / 210, (49 + num * 7) * _height / 115, 115 * _width / 210, 7 * _height / 115);
                    if (num == 0)
                    {
                        Rectangle rect4 = new(35 * _width / 210, 63 * _height / 115, 115 * _width / 210, 7 * _height / 115);
                        graphics.DrawRectangle(Pens.Black, rect4);
                        graphics.FillRectangle(Brushes.White, rect4);
                    }
                    graphics.DrawRectangle(Pens.Black, rect);
                    graphics.FillRectangle(Brushes.White, rect);
                    graphics.DrawRectangle(Pens.Black, rect2);
                    graphics.FillRectangle(Brushes.White, rect2);
                    graphics.DrawRectangle(Pens.Black, rect3);
                    graphics.FillRectangle(Brushes.White, rect3);
                    Rectangle rect5 = new(90 * _width / 210, 30 * _height / 115, 30 * _width / 210, 25 * _height / 115);
                    Rectangle rect6 = new(95 * _width / 210, 33 * _height / 115, 20 * _width / 210, 19 * _height / 115);
                    graphics.FillRectangle(Brushes.Gray, rect5);
                    Rectangle rect7 = new(90 * _width / 210, 0, 30 * _width / 210, 15 * _height / 115);
                    Rectangle rect8 = new(90 * _width / 210, 15 * _height / 115, 30 * _width / 210, 15 * _height / 115);
                    LinearGradientBrush brush3 = new(rect7, Color.Blue, Color.LightBlue, 90f);
                    LinearGradientBrush brush4 = new(rect7, Color.LightBlue, Color.Blue, 90f);
                    graphics.FillRectangle(brush3, rect7);
                    graphics.FillRectangle(brush4, rect8);
                    graphics.DrawLine(Pens.LightBlue, 90 * _width / 210, 15 * _height / 115, 120 * _width / 210, 15 * _height / 115);
                    graphics.FillRectangle(brush, rect6);
                    mytimer++;
                }
                else
                {
                    _ = mytimer % 2;
                    Paint(graphics);
                    Rectangle rect9 = new(35 * _width / 210 * _height / 115, 21, 115 * _width / 210, 7 * _height / 115);
                    Rectangle rect10 = new(35 * _width / 210 * _height / 115, 35, 115 * _width / 210, 7 * _height / 115);
                    Rectangle rect11 = new(35 * _width / 210 * _height / 115, 49, 115 * _width / 210, 7 * _height / 115);
                    Rectangle rect12 = new(35 * _width / 210 * _height / 115, 63, 115 * _width / 210, 7 * _height / 115);
                    graphics.DrawRectangle(Pens.Black, rect12);
                    graphics.FillRectangle(Brushes.White, rect12);
                    graphics.DrawRectangle(Pens.Black, rect9);
                    graphics.FillRectangle(Brushes.White, rect9);
                    graphics.DrawRectangle(Pens.Black, rect10);
                    graphics.FillRectangle(Brushes.White, rect10);
                    graphics.DrawRectangle(Pens.Black, rect11);
                    graphics.FillRectangle(Brushes.White, rect11);
                    Rectangle rect13 = new(90 * _width / 210, 30 * _height / 115, 30 * _width / 210, 25 * _height / 115);
                    Rectangle rect14 = new(95 * _width / 210, 33 * _height / 115, 20 * _width / 210, 19 * _height / 115);
                    graphics.FillRectangle(Brushes.Gray, rect13);
                    Rectangle rect15 = new(90 * _width / 210, 0, 30 * _width / 210, 15 * _height / 115);
                    Rectangle rect16 = new(90 * _width / 210, 15 * _height / 115, 30 * _width / 210, 15 * _height / 115);
                    LinearGradientBrush brush5 = new(rect15, Color.Blue, Color.LightBlue, 90f);
                    LinearGradientBrush brush6 = new(rect15, Color.LightBlue, Color.Blue, 90f);
                    graphics.FillRectangle(brush5, rect15);
                    graphics.FillRectangle(brush6, rect16);
                    graphics.DrawLine(Pens.LightBlue, 90 * _width / 210, 15 * _height / 115, 120 * _width / 210, 15 * _height / 115);
                    graphics.FillRectangle(brush2, rect14);
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
