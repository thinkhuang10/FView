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

namespace KG_1;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap : CPixieControl
{
    private Color setcolor = Color.Red;

    private Color setbgcolor = Color.Gray;

    private string varname = "";

    private int initflag = 1;

    private int _width = 200;

    private int _height = 300;

    private bool value;

    private bool opstflag;

    [DHMICtrlProperty]
    [DisplayName("开关颜色")]
    [Category("杂项")]
    [Description("开关颜色。")]
    [ReadOnly(false)]
    public Color MyControlColor
    {
        get
        {
            return setcolor;
        }
        set
        {
            setcolor = value;
        }
    }

    [DHMICtrlProperty]
    [Description("背景颜色。")]
    [ReadOnly(false)]
    [DisplayName("背景颜色")]
    [Category("杂项")]
    public Color MyControlBGColor
    {
        get
        {
            return setbgcolor;
        }
        set
        {
            setbgcolor = value;
        }
    }

    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("开关绑定变量。")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
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

    [DisplayName("是否取反")]
    [Category("杂项")]
    [Description("是否取反。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public bool IsGetOpesite
    {
        get
        {
            return opstflag;
        }
        set
        {
            opstflag = value;
        }
    }

    [Description("绑定变量当前值。")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("绑定变量当前值")]
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
            else if (enumerator.Name == "opstflag")
            {
                opstflag = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "setcolor")
            {
                setcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "value")
            {
                value = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "varname")
            {
                varname = (string)enumerator.Value;
            }
            else if (enumerator.Name == "setbgcolor")
            {
                setbgcolor = (Color)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("_height", _height);
        info.AddValue("_width", _width);
        info.AddValue("initflag", initflag);
        info.AddValue("opstflag", opstflag);
        info.AddValue("setcolor", setcolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
        info.AddValue("setbgcolor", setbgcolor);
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap10 = (drawbitmap)base.Copy();
        drawbitmap10.setcolor = setcolor;
        drawbitmap10.setbgcolor = setbgcolor;
        drawbitmap10.opstflag = opstflag;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(setbgcolor);
        SolidBrush brush2 = new(setcolor);
        Rectangle rect = new(0, 0, Convert.ToInt32(_width), Convert.ToInt32(_height));
        g.FillRectangle(brush, rect);
        g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.17), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.83));
        if (initflag == 1)
        {
            Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.64), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.2));
            g.DrawPie(Pens.Black, rect2, 0f, 360f);
            g.FillPie(brush2, rect2, 0f, 360f);
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(varname) && (float)e.X < Math.Abs(Width) && varname != "")
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
        }
        catch
        {
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname);
            if (Height == 0f || Width == 0f)
            {
                _height = 300;
                _width = 200;
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
                SolidBrush brush = new(setcolor);
                Paint(graphics);
                if (opstflag)
                {
                    if (!Value)
                    {
                        Rectangle rect = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.64), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.2));
                        graphics.DrawPie(Pens.Black, rect, 0f, 360f);
                        graphics.FillPie(brush, rect, 0f, 360f);
                    }
                    else
                    {
                        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.17), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.2));
                        graphics.DrawPie(Pens.Black, rect2, 0f, 360f);
                        graphics.FillPie(brush, rect2, 0f, 360f);
                    }
                }
                else if (Value)
                {
                    Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.64), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.2));
                    graphics.DrawPie(Pens.Black, rect3, 0f, 360f);
                    graphics.FillPie(brush, rect3, 0f, 360f);
                }
                else
                {
                    Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.17), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.2));
                    graphics.DrawPie(Pens.Black, rect4, 0f, 360f);
                    graphics.FillPie(brush, rect4, 0f, 360f);
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void ShowDialog()
    {
        KGSet1 kGSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.oppsiteflag = opstflag;
            kGSet.setcolor = setcolor;
            kGSet.varname = varname;
            kGSet.setbgcolor = setbgcolor;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.oppsiteflag;
            setcolor = kGSet.setcolor;
            setbgcolor = kGSet.setbgcolor;
            varname = "[" + kGSet.varname + "]";
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            setcolor = setcolor,
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
            BSave bSave = (BSave)formatter.Deserialize(stream);
            stream.Close();
            setcolor = bSave.setcolor;
            varname = bSave.varname;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
