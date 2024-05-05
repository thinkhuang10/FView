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
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap4 : CPixieControl
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

    [DisplayName("绑定变量")]
    [Description("控件绑定变量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
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
    [DisplayName("开颜色")]
    [Category("杂项")]
    [Description("阀门为开状态时显示的颜色。")]
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
    [DisplayName("关颜色")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
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
        drawbitmap4 drawbitmap9 = (drawbitmap4)base.Copy();
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
        if (initflag == 1)
        {
            Rectangle rect = new(15 * _width / 70, 0, 40 * _width / 70, 10 * _height / 80);
            SolidBrush brush = new(offcolor);
            g.FillRectangle(brush, rect);
        }
        Rectangle rect2 = new(30 * _width / 70, 10 * _height / 80, 10 * _width / 70, 20 * _height / 80);
        g.DrawRectangle(Pens.Black, rect2);
        g.FillRectangle(Brushes.Gray, rect2);
        Rectangle rect3 = new(0, 27 * _height / 80, 10 * _width / 70, 50 * _height / 80);
        g.DrawRectangle(Pens.Black, rect3);
        g.FillRectangle(Brushes.Gray, rect3);
        Rectangle rect4 = new(10 * _width / 70, 35 * _height / 80, 50 * _width / 70, 35 * _height / 80);
        g.DrawRectangle(Pens.Black, rect4);
        g.FillRectangle(Brushes.Gray, rect4);
        Rectangle rect5 = new(60 * _width / 70, 27 * _height / 80, 10 * _width / 70, 50 * _height / 80);
        g.DrawRectangle(Pens.Black, rect5);
        g.FillRectangle(Brushes.Gray, rect5);
        Rectangle rect6 = new(20 * _width / 70, 30 * _height / 80, 30 * _width / 70, 10 * _height / 80);
        g.DrawRectangle(Pens.Black, rect6);
        g.FillRectangle(Brushes.Gray, rect6);
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
                    Rectangle rect = new(15 * _width / 70, 0, 40 * _width / 70, 10 * _height / 80);
                    SolidBrush brush = new(oncolor);
                    graphics.FillRectangle(brush, rect);
                    Paint(graphics);
                }
                else
                {
                    Rectangle rect2 = new(15 * _width / 70, 0, 40 * _width / 70, 10 * _height / 80);
                    SolidBrush brush2 = new(offcolor);
                    graphics.FillRectangle(brush2, rect2);
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
