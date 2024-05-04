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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap8 : CPixieControl
{
    private string varname = "";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private Color colorfill = Color.LightGray;

    private int _width;

    private int _height;

    private int initflag = 1;

    private bool value;

    [NonSerialized]
    private SolidBrush br1 = new(Color.Red);

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

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("阀门为关状态时显示的颜色。")]
    [ReadOnly(false)]
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

    public drawbitmap8()
    {
    }

    protected drawbitmap8(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap8 info");
        }
        drawbitmap8 obj = new();
        FieldInfo[] fields = typeof(drawbitmap8).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap8))
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
            else if (enumerator.Name == "colorfill")
            {
                colorfill = (Color)enumerator.Value;
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
        info.AddValue("colorfill", colorfill);
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
        drawbitmap8 drawbitmap9 = (drawbitmap8)base.Copy();
        drawbitmap9.oncolor = oncolor;
        drawbitmap9.offcolor = offcolor;
        drawbitmap9.varname = varname;
        drawbitmap9.br1 = br1;
        drawbitmap9.colorfill = colorfill;
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
        Rectangle rect = new(Convert.ToInt32((double)_width * 0.1), 0, Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.55));
        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.1), 0, Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.55));
        new Rectangle(Convert.ToInt32((double)_width * 0.5), 0, Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.55));
        LinearGradientBrush brush = new(rect2, colorfill, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect2, Color.White, colorfill, LinearGradientMode.Horizontal);
        g.FillPie(brush, rect, 180f, 90f);
        g.FillPie(brush2, rect, 270f, 90f);
        g.DrawLine(new Pen(Color.White, 3f), Convert.ToInt32((double)_width * 0.5), 0, Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.275));
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3));
        Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3));
        LinearGradientBrush brush3 = new(rect3, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect4, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush3, rect3);
        g.FillRectangle(brush4, rect4);
        Rectangle rect5 = new(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.55), Convert.ToInt32((double)_width * 0.2), Convert.ToInt32((double)_height * 0.35));
        Rectangle rect6 = new(Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.55), Convert.ToInt32((double)_width * 0.2), Convert.ToInt32((double)_height * 0.35));
        LinearGradientBrush brush5 = new(rect5, Color.Gray, Color.LightGray, LinearGradientMode.Horizontal);
        LinearGradientBrush brush6 = new(rect6, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal);
        g.FillRectangle(brush5, rect5);
        g.FillRectangle(brush6, rect6);
        g.DrawLine(new Pen(Color.LightGray, 3f), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.27), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.9));
        Rectangle rect7 = new(0, Convert.ToInt32((double)_height * 0.45), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.275));
        Rectangle rect8 = new(0, Convert.ToInt32((double)_height * 0.725), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.275));
        LinearGradientBrush brush7 = new(rect7, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush8 = new(rect8, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush7, rect7);
        g.FillRectangle(brush8, rect8);
        g.DrawLine(new Pen(Color.LightGray, 3f), 0, Convert.ToInt32((double)_height * 0.725), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.725));
        Rectangle rect9 = new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.45), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.275));
        Rectangle rect10 = new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.725), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.275));
        LinearGradientBrush brush9 = new(rect9, Color.Gray, Color.LightGray, 90f);
        LinearGradientBrush brush10 = new(rect10, Color.LightGray, Color.Gray, 90f);
        g.FillRectangle(brush9, rect9);
        g.FillRectangle(brush10, rect10);
        g.DrawLine(new Pen(Color.LightGray, 3f), Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.725), _width, Convert.ToInt32((double)_height * 0.725));
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
                    colorfill = oncolor;
                    Paint(graphics);
                }
                else
                {
                    colorfill = offcolor;
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
            colorfill = offcolor;
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
