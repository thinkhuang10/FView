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
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap2 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private int _width = 100;

    private int _height = 90;

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
        drawbitmap2 drawbitmap6 = (drawbitmap2)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.LightGray, 4f);
        Rectangle rect = new(0, 20 * _height / 130, 10 * _width / 130, 20 * _height / 130);
        Rectangle rect2 = new(0, 40 * _height / 130, 10 * _width / 130, 20 * _height / 130);
        Rectangle rect3 = new(0, 20 * _height / 130, 10 * _width / 130, 40 * _height / 130);
        LinearGradientBrush brush = new(rect, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush2 = new(rect2, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush, rect);
        g.FillRectangle(brush2, rect2);
        Rectangle rect4 = new(10 * _width / 130, 25 * _height / 130, 30 * _width / 130, 15 * _height / 130);
        Rectangle rect5 = new(10 * _width / 130, 40 * _height / 130, 30 * _width / 130, 15 * _height / 130);
        LinearGradientBrush brush3 = new(rect4, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush4 = new(rect5, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush3, rect4);
        g.FillRectangle(brush4, rect5);
        g.DrawLine(pen, 0, 40 * _height / 130, 40 * _width / 130, 40 * _height / 130);
        g.DrawRectangle(Pens.Black, rect3);
        Point[] points = new Point[3]
        {
            new(60 * _width / 130, 60 * _height / 130),
            new(30 * _width / 130, 100 * _height / 130),
            new(60 * _width / 130, 100 * _height / 130)
        };
        Point[] points2 = new Point[3]
        {
            new(60 * _width / 130, 60 * _height / 130),
            new(90 * _width / 130, 100 * _height / 130),
            new(60 * _width / 130, 100 * _height / 130)
        };
        Rectangle rect6 = new(25 * _width / 130, 60 * _height / 130, 35 * _width / 130, 55 * _height / 130);
        Rectangle rect7 = new(60 * _width / 130, 60 * _height / 130, 35 * _width / 130, 55 * _height / 130);
        LinearGradientBrush brush5 = new(rect6, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect7, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPolygon(brush5, points);
        g.FillPolygon(brush6, points2);
        Rectangle rect8 = new(25 * _width / 130, 100 * _height / 130, 35 * _width / 130, 15 * _height / 130);
        Rectangle rect9 = new(60 * _width / 130, 100 * _height / 130, 35 * _width / 130, 15 * _height / 130);
        Rectangle rect10 = new(25 * _width / 130, 100 * _height / 130, 70 * _width / 130, 15 * _height / 130);
        g.FillRectangle(brush5, rect8);
        g.FillRectangle(brush6, rect9);
        g.DrawLine(pen, 60 * _width / 130, 60 * _height / 130, 60 * _width / 130, 115 * _height / 130);
        g.DrawRectangle(Pens.Black, rect10);
        Rectangle rect11 = new(25 * _width / 130, 5 * _height / 130, 70 * _width / 130, 70 * _height / 130);
        Rectangle rect12 = new(25 * _width / 130, 5 * _height / 130, 35 * _width / 130, 70 * _height / 130);
        Rectangle rect13 = new(60 * _width / 130, 5 * _height / 130, 35 * _width / 130, 70 * _height / 130);
        LinearGradientBrush brush7 = new(rect12, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush8 = new(rect13, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillPie(brush7, rect11, 90f, 180f);
        g.FillPie(brush8, rect11, 270f, 180f);
        Rectangle rect14 = new(70 * _width / 130, 7 * _height / 130, 40 * _width / 130, 10 * _height / 130);
        Rectangle rect15 = new(70 * _width / 130, 17 * _height / 130, 40 * _width / 130, 10 * _height / 130);
        LinearGradientBrush brush9 = new(rect14, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect15, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect14);
        g.FillRectangle(brush10, rect15);
        Rectangle rect16 = new(110 * _width / 130, 2 * _height / 130, 15 * _width / 130, 15 * _height / 130);
        Rectangle rect17 = new(110 * _width / 130, 17 * _height / 130, 15 * _width / 130, 15 * _height / 130);
        Rectangle rect18 = new(110 * _width / 130, 2 * _height / 130, 15 * _width / 130, 30 * _height / 130);
        LinearGradientBrush brush11 = new(rect16, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush12 = new(rect17, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush11, rect16);
        g.FillRectangle(brush12, rect17);
        g.DrawLine(pen, 70 * _width / 130, 17 * _height / 130, 125 * _width / 130, 17 * _height / 130);
        g.DrawRectangle(Pens.Black, rect18);
        Rectangle rect19 = new(30 * _width / 130, 10 * _height / 130, 60 * _width / 130, 60 * _height / 130);
        g.FillPie(brush7, rect19, 90f, 180f);
        g.FillPie(brush8, rect19, 270f, 180f);
        g.DrawLine(pen, 60 * _width / 130, 5 * _height / 130, 60 * _width / 130, 75 * _height / 130);
        Rectangle rect20 = new(45 * _width / 130, 25 * _height / 130, 30 * _width / 130, 15 * _height / 130);
        Rectangle rect21 = new(45 * _width / 130, 40 * _height / 130, 30 * _width / 130, 15 * _height / 130);
        Rectangle rect22 = new(50 * _width / 130, 30 * _height / 130, 20 * _width / 130, 20 * _height / 130);
        LinearGradientBrush brush13 = new(rect20, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush14 = new(rect21, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush13, rect20);
        g.FillRectangle(brush14, rect21);
        g.DrawLine(pen, 45 * _width / 130, 40 * _height / 130, 75 * _width / 130, 40 * _height / 130);
        if (initflag == 1)
        {
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
                _height = 90;
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
                Rectangle rect = new(50 * _width / 130, 30 * _height / 130, 20 * _width / 130, 20 * _height / 130);
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
        catch
        {
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
