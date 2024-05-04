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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap3 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int _width = 100;

    private int _height = 140;

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
        Pen pen = new(Color.LightGray, 4f);
        Rectangle rect = new(0, 0, 80 * _width / 97, 80 * _height / 120);
        Rectangle rect2 = new(0, 0, 80 * _width / 97, 40 * _height / 120);
        Rectangle rect3 = new(0, 40 * _height / 120, 80 * _width / 97, 40 * _height / 120);
        LinearGradientBrush brush = new(rect2, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush2 = new(rect3, Color.LightGray, Color.Gray, 90f);
        g.FillPie(brush, rect, 180f, 120f);
        g.FillPie(brush2, rect, 60f, 120f);
        g.DrawLine(pen, 40 * _width / 97, 40 * _height / 120, 0, 40 * _height / 120);
        Rectangle rect4 = new(5 * _width / 97, 5, 70 * _width / 97, 70 * _height / 120);
        Rectangle rect5 = new(5 * _width / 97, 5, 35 * _width / 97, 35 * _height / 120);
        Rectangle rect6 = new(5 * _width / 97, 40, 35 * _width / 97, 35 * _height / 120);
        LinearGradientBrush brush3 = new(rect5, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect6, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPie(brush3, rect4, 90f, 180f);
        g.FillPie(brush4, rect4, 60f, 30f);
        g.FillPie(brush4, rect4, 270f, 30f);
        g.DrawLine(pen, 40 * _width / 97, 5 * _height / 120, 40 * _width / 97, 75 * _height / 120);
        Point[] points = new Point[3]
        {
            new(40 * _width / 97, 40 * _height / 120),
            new(20 * _width / 97, 100 * _height / 120),
            new(40 * _width / 97, 100 * _height / 120)
        };
        Point[] points2 = new Point[3]
        {
            new(40 * _width / 97, 40 * _height / 120),
            new(60 * _width / 97, 100 * _height / 120),
            new(40 * _width / 97, 100 * _height / 120)
        };
        Rectangle rect7 = new(15 * _width / 97, 40 * _height / 120, 25 * _width / 97, 75 * _height / 120);
        Rectangle rect8 = new(40 * _width / 97, 40 * _height / 120, 25 * _width / 97, 75 * _height / 120);
        LinearGradientBrush brush5 = new(rect7, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect8, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPolygon(brush5, points);
        g.FillPolygon(brush6, points2);
        Rectangle rect9 = new(35 * _width / 97, 33 * _height / 120, 10 * _width / 97, 15 * _height / 120);
        Rectangle rect10 = new(35 * _width / 97, 33 * _height / 120, 5 * _width / 97, 15 * _height / 120);
        Rectangle rect11 = new(40 * _width / 97, 33 * _height / 120, 5 * _width / 97, 15 * _height / 120);
        LinearGradientBrush brush7 = new(rect10, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush8 = new(rect11, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPie(brush7, rect10, 90f, 180f);
        g.FillPie(brush8, rect11, 270f, 180f);
        g.FillPie(Brushes.Gray, rect9, 0f, 360f);
        Rectangle rect12 = new(15 * _width / 97, 100 * _height / 120, 25 * _width / 97, 15 * _height / 120);
        Rectangle rect13 = new(40 * _width / 97, 100 * _height / 120, 25 * _width / 97, 15 * _height / 120);
        Rectangle rect14 = new(15 * _width / 97, 100 * _height / 120, 50 * _width / 97, 15 * _height / 120);
        g.FillRectangle(brush5, rect12);
        g.FillRectangle(brush6, rect13);
        g.DrawLine(pen, 40 * _width / 97, 48 * _height / 120, 40 * _width / 97, 115 * _height / 120);
        g.DrawRectangle(Pens.Black, rect14);
        Rectangle rect15 = new(40 * _width / 97, 5 * _height / 120, 40 * _width / 97, 7 * _height / 120);
        Rectangle rect16 = new(40 * _width / 97, 12 * _height / 120, 40 * _width / 97, 8 * _height / 120);
        LinearGradientBrush brush9 = new(rect15, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect16, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect15);
        g.FillRectangle(brush10, rect16);
        Rectangle rect17 = new(80 * _width / 97, 0, 15 * _width / 97, 12 * _height / 120);
        Rectangle rect18 = new(80 * _width / 97, 12 * _height / 120, 15 * _width / 97, 13 * _height / 120);
        Rectangle rect19 = new(80 * _width / 97, 0, 15 * _width / 97, 25 * _height / 120);
        LinearGradientBrush brush11 = new(rect17, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush12 = new(rect18, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush11, rect17);
        g.FillRectangle(brush12, rect18);
        g.DrawLine(pen, 40 * _width / 97, 12 * _height / 120, 95 * _width / 97, 12 * _height / 120);
        g.DrawRectangle(Pens.Black, rect19);
        g.FillRectangle(rect: new Rectangle(30 * _width / 97, 85 * _height / 120, 20 * _width / 97, 15 * _height / 120), brush: Brushes.Gray);
        if (initflag == 1)
        {
            Rectangle rect21 = new(35 * _width / 97, 87 * _height / 120, 10 * _width / 97, 10 * _height / 120);
            SolidBrush brush13 = new(offcolor);
            g.FillRectangle(brush13, rect21);
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
                Rectangle rect = new(35 * _width / 97, 87 * _height / 120, 10 * _width / 97, 10 * _height / 120);
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
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
