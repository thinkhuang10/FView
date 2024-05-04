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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private bool value;

    private int _width = 100;

    private int _height = 105;

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
        Rectangle rect = new(0, 0, 15 * _width / 120, 10 * _height / 120);
        Rectangle rect2 = new(15 * _width / 120, 0, 15 * _width / 120, 10 * _height / 120);
        Rectangle rect3 = new(0, 0, 30 * _width / 120, 10 * _height / 120);
        LinearGradientBrush brush = new(rect, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect2, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush, rect);
        g.FillRectangle(brush2, rect2);
        Rectangle rect4 = new(5 * _width / 120, 10 * _height / 120, 10 * _width / 120, 50 * _height / 120);
        Rectangle rect5 = new(15 * _width / 120, 10 * _height / 120, 10 * _width / 120, 50 * _height / 120);
        LinearGradientBrush brush3 = new(rect4, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect5, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush3, rect4);
        g.FillRectangle(brush4, rect5);
        g.DrawLine(pen, 15 * _width / 120, 0, 15 * _width / 120, 60 * _height / 120);
        g.DrawRectangle(Pens.Black, rect3);
        Point[] points = new Point[3]
        {
            new(40 * _width / 120, 75 * _height / 120),
            new(5 * _width / 120, 100 * _height / 120),
            new(40 * _width / 120, 100 * _height / 120)
        };
        Point[] points2 = new Point[3]
        {
            new(40 * _width / 120, 75 * _height / 120),
            new(75 * _width / 120, 100 * _height / 120),
            new(40 * _width / 120, 100 * _height / 120)
        };
        Rectangle rect6 = new(5 * _width / 120, 75 * _height / 120, 35 * _width / 120, 40 * _height / 120);
        Rectangle rect7 = new(40 * _width / 120, 75 * _height / 120, 35 * _width / 120, 40 * _height / 120);
        LinearGradientBrush brush5 = new(rect6, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect7, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPolygon(brush5, points);
        g.FillPolygon(brush5, points2);
        Rectangle rect8 = new(5 * _width / 120, 95 * _height / 120, 35 * _width / 120, 15 * _height / 120);
        Rectangle rect9 = new(40 * _width / 120, 95 * _height / 120, 35 * _width / 120, 15 * _height / 120);
        Rectangle rect10 = new(5 * _width / 120, 95 * _height / 120, 70 * _width / 120, 15 * _height / 120);
        g.FillRectangle(brush5, rect8);
        g.FillRectangle(brush6, rect9);
        g.DrawLine(pen, 40 * _width / 120, 75 * _height / 120, 40 * _width / 120, 110 * _height / 120);
        g.DrawRectangle(Pens.Black, rect10);
        Rectangle rect11 = new(5 * _width / 120, 25 * _height / 120, 70 * _width / 120, 70 * _height / 120);
        Rectangle rect12 = new(5 * _width / 120, 25 * _height / 120, 35 * _width / 120, 70 * _height / 120);
        Rectangle rect13 = new(40 * _width / 120, 25 * _height / 120, 35 * _width / 120, 70 * _height / 120);
        LinearGradientBrush brush7 = new(rect12, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush8 = new(rect13, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPie(brush7, rect11, 90f, 180f);
        g.FillPie(brush8, rect11, 270f, 180f);
        g.DrawLine(pen, 40 * _width / 120, 25 * _height / 120, 40 * _width / 120, 95 * _height / 120);
        Rectangle rect14 = new(20 * _width / 120, 50 * _height / 120, 40 * _width / 120, 10 * _height / 120);
        Rectangle rect15 = new(20 * _width / 120, 60 * _height / 120, 40 * _width / 120, 10 * _height / 120);
        LinearGradientBrush brush9 = new(rect14, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect15, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect14);
        g.FillRectangle(brush10, rect15);
        Rectangle rect16 = new(60 * _width / 120, 53 * _height / 120, 40 * _width / 120, 7 * _height / 120);
        Rectangle rect17 = new(60 * _width / 120, 60 * _height / 120, 40 * _width / 120, 7 * _height / 120);
        LinearGradientBrush brush11 = new(rect16, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush12 = new(rect17, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush11, rect16);
        g.FillRectangle(brush12, rect17);
        Rectangle rect18 = new(100 * _width / 120, 50 * _height / 120, 15 * _width / 120, 10 * _height / 120);
        Rectangle rect19 = new(100 * _width / 120, 60 * _height / 120, 15 * _width / 120, 10 * _height / 120);
        Rectangle rect20 = new(100 * _width / 120, 50 * _height / 120, 15 * _width / 120, 20 * _height / 120);
        LinearGradientBrush brush13 = new(rect18, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush14 = new(rect19, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush13, rect18);
        g.FillRectangle(brush14, rect19);
        g.DrawLine(pen, 20 * _width / 120, 60 * _height / 120, 115 * _width / 120, 60 * _height / 120);
        g.DrawRectangle(Pens.Black, rect20);
        if (initflag == 1)
        {
            Rectangle rect21 = new(30 * _width / 120, 55 * _height / 120, 20 * _width / 120, 10 * _height / 120);
            SolidBrush brush15 = new(offcolor);
            g.FillRectangle(brush15, rect21);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 105;
                _width = 100;
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
                Rectangle rect = new(30 * _width / 120, 55 * _height / 120, 20 * _width / 120, 10 * _height / 120);
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
        catch
        {
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
