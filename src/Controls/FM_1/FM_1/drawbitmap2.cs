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

namespace FM_1;

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

    private int _width = 50;

    private int _height = 80;

    private bool value;

    [NonSerialized]
    private SolidBrush br1 = new(Color.Red);

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

    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("关颜色")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [Description("指示阀门当前状态。")]
    [ReadOnly(false)]
    [DisplayName("当前值")]
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap9 = (drawbitmap2)base.Copy();
        drawbitmap9.oncolor = oncolor;
        drawbitmap9.offcolor = offcolor;
        drawbitmap9.varname = varname;
        drawbitmap9.br1 = br1;
        return drawbitmap9;
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        if (Convert.ToBoolean(GetValue(varname)))
        {
            SetValue(varname, false);
        }
        else
        {
            SetValue(varname, true);
        }
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.DarkBlue, 2f);
        Rectangle rect = new(15 * _width / 50, 0, 20 * _width / 50, 30 * _height / 60);
        g.DrawRectangle(pen, rect);
        g.DrawLine(pen, 25, 0, 25, 20);
        Rectangle rect2 = new(20 * _width / 50, 20 * _height / 60, 10 * _width / 50, 10 * _height / 60);
        g.FillRectangle(br1, rect2);
        g.DrawRectangle(Pens.Black, rect2);
        g.FillRectangle(rect: new Rectangle(0, 0, 50 * _width / 50, 15 * _height / 60), brush: br1);
        for (int i = 0; i < 49; i += 7)
        {
            g.DrawLine(Pens.Black, i * _width / 50, 0, i * _width / 50, 15 * _height / 60);
        }
        g.DrawLine(Pens.Black, 50 * _width / 50, 0, 50 * _width / 50, 15 * _height / 60);
        g.FillRectangle(rect: new Rectangle(22 * _width / 50, 30 * _height / 60, 6 * _width / 50, 15 * _height / 60), brush: br1);
        Rectangle rect5 = new(20 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
        g.DrawRectangle(Pens.Black, rect5);
        g.FillRectangle(br1, rect5);
        Rectangle rect6 = new(0, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
        Rectangle rect7 = new(40 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
        g.DrawRectangle(Pens.Black, rect6);
        g.DrawRectangle(Pens.Black, rect7);
        g.FillRectangle(br1, rect6);
        g.FillRectangle(br1, rect7);
        g.FillRectangle(rect: new Rectangle(10 * _width / 50, 47 * _height / 60, 30 * _width / 50, 10 * _height / 60), brush: br1);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 80;
                _width = 50;
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
                    SolidBrush brush = new(oncolor);
                    Pen pen = new(Color.DarkBlue, 2f);
                    Rectangle rect = new(15 * _width / 50, 0, 20 * _width / 50, 30 * _height / 60);
                    graphics.DrawRectangle(pen, rect);
                    graphics.DrawLine(pen, 25 * _width / 50, 0, 25 * _width / 50, 20 * _width / 50);
                    Rectangle rect2 = new(20 * _width / 50, 20 * _height / 60, 10 * _width / 50, 10 * _height / 60);
                    graphics.FillRectangle(brush, rect2);
                    graphics.DrawRectangle(Pens.Black, rect2);
                    Rectangle rect3 = new(0, 0, 50 * _width / 50, 15 * _height / 60);
                    graphics.FillRectangle(brush, rect3);
                    for (int i = 0; i < 49; i += 7)
                    {
                        graphics.DrawLine(Pens.Black, i * _width / 50, 0, i * _width / 50, 15 * _height / 60);
                    }
                    graphics.DrawLine(Pens.Black, 50 * _width / 50, 0, 50 * _width / 50, 15 * _height / 60);
                    Rectangle rect4 = new(22 * _width / 50, 30 * _height / 60, 6 * _width / 50, 15 * _height / 60);
                    graphics.FillRectangle(brush, rect4);
                    Rectangle rect5 = new(20 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    graphics.DrawRectangle(Pens.Black, rect5);
                    graphics.FillRectangle(brush, rect5);
                    Rectangle rect6 = new(0, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    Rectangle rect7 = new(40 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    graphics.DrawRectangle(Pens.Black, rect6);
                    graphics.DrawRectangle(Pens.Black, rect7);
                    graphics.FillRectangle(brush, rect6);
                    graphics.FillRectangle(brush, rect7);
                    Rectangle rect8 = new(10 * _width / 50, 47 * _height / 60, 30 * _width / 50, 10 * _height / 60);
                    graphics.FillRectangle(brush, rect8);
                }
                else
                {
                    SolidBrush brush2 = new(offcolor);
                    Pen pen2 = new(Color.DarkBlue, 2f);
                    Rectangle rect9 = new(15 * _width / 50, 0, 20 * _width / 50, 30 * _height / 60);
                    graphics.DrawRectangle(pen2, rect9);
                    graphics.DrawLine(pen2, 25 * _width / 50, 0, 25 * _width / 50, 20);
                    Rectangle rect10 = new(20 * _width / 50, 20 * _height / 60, 10 * _width / 50, 10 * _height / 60);
                    graphics.FillRectangle(brush2, rect10);
                    graphics.DrawRectangle(Pens.Black, rect10);
                    Rectangle rect11 = new(0, 0, 50 * _width / 50, 15 * _height / 60);
                    graphics.FillRectangle(brush2, rect11);
                    for (int j = 0; j < 49; j += 7)
                    {
                        graphics.DrawLine(Pens.Black, j * _width / 50, 0, j * _width / 50, 15 * _height / 60);
                    }
                    graphics.DrawLine(Pens.Black, 50 * _width / 50, 0, 50 * _width / 50, 15 * _height / 60);
                    Rectangle rect12 = new(22 * _width / 50, 30 * _height / 60, 6 * _width / 50, 15 * _height / 60);
                    graphics.FillRectangle(brush2, rect12);
                    Rectangle rect13 = new(20 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    graphics.DrawRectangle(Pens.Black, rect13);
                    graphics.FillRectangle(brush2, rect13);
                    Rectangle rect14 = new(0, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    Rectangle rect15 = new(40 * _width / 50, 45 * _height / 60, 10 * _width / 50, 15 * _height / 60);
                    graphics.DrawRectangle(Pens.Black, rect14);
                    graphics.DrawRectangle(Pens.Black, rect15);
                    graphics.FillRectangle(brush2, rect14);
                    graphics.FillRectangle(brush2, rect15);
                    Rectangle rect16 = new(10 * _width / 50, 47 * _height / 60, 30 * _width / 50, 10 * _height / 60);
                    graphics.FillRectangle(brush2, rect16);
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
        FMSet1 fMSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            fMSet.varname = varname;
            fMSet.oncolor = oncolor;
            fMSet.offcolor = offcolor;
        }
        fMSet.viewevent += GetTable;
        fMSet.ckvarevent += CheckVar;
        if (fMSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + fMSet.varname + "]";
            oncolor = fMSet.oncolor;
            offcolor = fMSet.offcolor;
            br1.Color = offcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            offcolor = offcolor,
            oncolor = oncolor,
            varname = varname
        };
        formatter.Serialize(memoryStream, bSave);
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
            br1 = new SolidBrush(Color.Red);
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
