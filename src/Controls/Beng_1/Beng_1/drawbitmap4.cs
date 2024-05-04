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

namespace Beng_1;

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

    private int _width = 100;

    private int _height = 100;

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
        Pen pen = new(Color.LightGray, 4f);
        Point[] points = new Point[3]
        {
            new(70 * _width / 117, 40 * _height / 120),
            new(30 * _width / 117, 100 * _height / 120),
            new(60 * _width / 117, 100 * _height / 120)
        };
        Point[] points2 = new Point[3]
        {
            new(70 * _width / 117, 40 * _height / 120),
            new(110 * _width / 117, 100 * _height / 120),
            new(80 * _width / 117, 100 * _height / 120)
        };
        Rectangle rect = new(30 * _width / 117, 40 * _height / 120, 40 * _width / 117, 60 * _height / 120);
        Rectangle rect2 = new(70 * _width / 117, 40 * _height / 120, 40 * _width / 117, 60 * _height / 120);
        LinearGradientBrush brush = new(rect, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect2, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPolygon(brush, points);
        g.FillPolygon(brush2, points2);
        g.DrawPolygon(Pens.Black, points);
        g.DrawPolygon(Pens.Black, points2);
        Rectangle rect3 = new(25 * _width / 117, 100 * _height / 120, 20 * _width / 117, 10 * _height / 120);
        Rectangle rect4 = new(45 * _width / 117, 100 * _height / 120, 20 * _width / 117, 10 * _height / 120);
        Rectangle rect5 = new(25 * _width / 117, 100 * _height / 120, 40 * _width / 117, 10 * _height / 120);
        LinearGradientBrush brush3 = new(rect3, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect4, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush3, rect3);
        g.FillRectangle(brush4, rect4);
        g.DrawLine(pen, 45 * _width / 117, 100 * _height / 120, 45 * _width / 117, 110 * _height / 120);
        g.DrawRectangle(Pens.Black, rect5);
        Rectangle rect6 = new(75 * _width / 117, 100 * _height / 120, 20 * _width / 117, 10 * _height / 120);
        Rectangle rect7 = new(95 * _width / 117, 100 * _height / 120, 20 * _width / 117, 10 * _height / 120);
        Rectangle rect8 = new(75 * _width / 117, 100 * _height / 120, 40 * _width / 117, 10 * _height / 120);
        LinearGradientBrush brush5 = new(rect6, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect7, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush5, rect6);
        g.FillRectangle(brush6, rect7);
        g.DrawLine(pen, 95 * _width / 117, 100 * _height / 120, 95 * _width / 117, 110 * _height / 120);
        g.DrawRectangle(Pens.Black, rect8);
        Rectangle rect9 = new(30 * _width / 117, 0, 80 * _width / 117, 80 * _height / 120);
        Rectangle rect10 = new(30 * _width / 117, 0, 40 * _width / 117, 80 * _height / 120);
        new Rectangle(70 * _width / 117, 0, 40 * _width / 117, 80 * _height / 120);
        LinearGradientBrush brush7 = new(rect10, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush8 = new(rect10, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPie(brush7, rect9, 90f, 180f);
        g.FillPie(brush8, rect9, 270f, 180f);
        g.DrawPie(Pens.Black, rect9, 270f, 360f);
        Rectangle rect11 = new(0, 0, 10 * _width / 117, 15 * _height / 120);
        Rectangle rect12 = new(0, 15 * _height / 120, 10 * _width / 117, 15 * _height / 120);
        Rectangle rect13 = new(0, 0, 10 * _width / 117, 30 * _height / 120);
        LinearGradientBrush brush9 = new(rect11, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect12, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect11);
        g.FillRectangle(brush10, rect12);
        Rectangle rect14 = new(10 * _width / 117, 5 * _height / 120, 40 * _width / 117, 10 * _height / 120);
        Rectangle rect15 = new(10 * _width / 117, 15 * _height / 120, 40 * _width / 117, 10 * _height / 120);
        LinearGradientBrush brush11 = new(rect11, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush12 = new(rect12, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush11, rect14);
        g.FillRectangle(brush12, rect15);
        g.DrawLine(pen, 0, 15 * _height / 120, 50 * _width / 117, 15 * _height / 120);
        g.DrawRectangle(Pens.Black, rect13);
        g.DrawLine(Pens.Black, 10 * _width / 117, 5 * _height / 120, 50 * _width / 117, 5 * _height / 120);
        g.DrawLine(Pens.Black, 10 * _width / 117, 25 * _height / 120, 50 * _width / 117, 25 * _height / 120);
        Rectangle rect16 = new(35 * _width / 117, 5 * _height / 120, 70 * _width / 117, 70 * _height / 120);
        g.FillPie(brush7, rect16, 90f, 180f);
        g.FillPie(brush8, rect16, 270f, 180f);
        g.DrawLine(pen, 70 * _width / 117, 0, 70 * _width / 117, 80 * _height / 120);
        g.FillRectangle(rect: new Rectangle(60 * _width / 117, 33 * _height / 120, 20 * _width / 117, 15 * _height / 120), brush: Brushes.Gray);
        if (initflag == 1)
        {
            Rectangle rect18 = new(65 * _width / 117, 35 * _height / 120, 10 * _width / 117, 10 * _height / 120);
            SolidBrush brush13 = new(offcolor);
            g.FillRectangle(brush13, rect18);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 100;
                _width = 100;
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
                Rectangle rect = new(65 * _width / 117, 35 * _height / 120, 10 * _width / 117, 10 * _height / 120);
                Paint(graphics);
                if (Value)
                {
                    graphics.FillRectangle(brush, rect);
                }
                else
                {
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
        BengSet1 bengSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            bengSet.varname = varname;
            bengSet.oncolor = oncolor;
            bengSet.offcolor = offcolor;
        }
        bengSet.viewevent += GetTable;
        bengSet.ckvarevent += CheckVar;
        if (bengSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + bengSet.varname + "]";
            oncolor = bengSet.oncolor;
            offcolor = bengSet.offcolor;
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
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
