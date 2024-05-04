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
public class drawbitmap5 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private int initflag = 1;

    private bool value;

    private int _width = 100;

    private int _height = 100;

    [Category("杂项")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
    [Description("控件绑定变量。")]
    [ReadOnly(false)]
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
        drawbitmap5 drawbitmap6 = (drawbitmap5)base.Copy();
        drawbitmap6.oncolor = oncolor;
        drawbitmap6.offcolor = offcolor;
        drawbitmap6.varname = varname;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.LightGray, 4f);
        Rectangle rect = new(0, 0, 15 * _width / 87, 10 * _height / 133);
        Rectangle rect2 = new(15 * _width / 87, 0, 15 * _width / 87, 10 * _height / 133);
        Rectangle rect3 = new(0, 0, 30 * _width / 87, 10 * _height / 133);
        LinearGradientBrush brush = new(rect, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect2, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush, rect);
        g.FillRectangle(brush2, rect2);
        Rectangle rect4 = new(5 * _width / 87, 10 * _height / 133, 10 * _width / 87, 60 * _height / 133);
        Rectangle rect5 = new(15 * _width / 87, 10 * _height / 133, 10 * _width / 87, 60 * _height / 133);
        LinearGradientBrush brush3 = new(rect4, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect5, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush3, rect4);
        g.FillRectangle(brush4, rect5);
        g.DrawLine(pen, 15 * _width / 87, 0, 15 * _width / 87, 50 * _height / 133);
        g.DrawRectangle(Pens.Black, rect3);
        Rectangle rect6 = new(5 * _width / 87, 30 * _height / 133, 80 * _width / 87, 80 * _height / 133);
        SolidBrush brush5 = new(offcolor);
        SolidBrush brush6 = new(oncolor);
        if (initflag == 1)
        {
            g.FillPie(brush5, rect6, 0f, 360f);
        }
        else
        {
            g.FillPie(brush6, rect6, 0f, 360f);
        }
        Rectangle rect7 = new(35 * _width / 87, 60 * _height / 133, 10 * _width / 87, 60 * _height / 133);
        new Rectangle(45 * _width / 87, 60 * _height / 133, 10 * _width / 87, 60 * _height / 133);
        LinearGradientBrush brush7 = new(rect7, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush8 = new(rect7, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        Rectangle rect8 = new(35 * _width / 87, 60 * _height / 133, 20 * _width / 87, 20 * _height / 133);
        g.FillPie(brush7, rect8, 180f, 90f);
        g.FillPie(brush8, rect8, 270f, 90f);
        Rectangle rect9 = new(35 * _width / 87, 68 * _height / 133, 10 * _width / 87, 52 * _height / 133);
        Rectangle rect10 = new(45 * _width / 87, 68 * _height / 133, 10 * _width / 87, 52 * _height / 133);
        g.FillRectangle(brush7, rect9);
        g.FillRectangle(brush8, rect10);
        Rectangle rect11 = new(30 * _width / 87, 120 * _height / 133, 15 * _width / 87, 10 * _height / 133);
        Rectangle rect12 = new(45 * _width / 87, 120 * _height / 133, 15 * _width / 87, 10 * _height / 133);
        Rectangle rect13 = new(30 * _width / 87, 120 * _height / 133, 30 * _width / 87, 10 * _height / 133);
        LinearGradientBrush brush9 = new(rect11, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush10 = new(rect12, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush9, rect11);
        g.FillRectangle(brush10, rect12);
        g.DrawLine(pen, 45 * _width / 87, 60 * _height / 133, 45 * _width / 87, 130 * _height / 133);
        g.DrawRectangle(Pens.Black, rect13);
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
            new SolidBrush(oncolor);
            new SolidBrush(offcolor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                new Rectangle(65 * _width / 87, 35 * _height / 133, 10 * _width / 87, 10 * _height / 133);
                if (Value)
                {
                    initflag = 0;
                    Paint(graphics);
                }
                else
                {
                    initflag = 1;
                    Paint(graphics);
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
