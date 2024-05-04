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
public class drawbitmap2 : CPixieControl
{
    private Color bgcolor = Color.LightGray;

    private Color txtcolor = Color.Black;

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private string varname = "";

    private string mark = "ON";

    private int initflag = 1;

    private int _width = 200;

    private int _height = 180;

    private bool opstflag;

    private bool value;

    [DHMICtrlProperty]
    public Color BackColor
    {
        get
        {
            return bgcolor;
        }
        set
        {
            bgcolor = value;
        }
    }

    [DHMICtrlProperty]
    public Color TextColor
    {
        get
        {
            return txtcolor;
        }
        set
        {
            txtcolor = value;
        }
    }

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

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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
    public string Mark
    {
        get
        {
            return mark;
        }
        set
        {
            mark = value;
        }
    }

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
            else if (enumerator.Name == "bgcolor")
            {
                bgcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "initflag")
            {
                initflag = (int)enumerator.Value;
            }
            else if (enumerator.Name == "mark")
            {
                mark = (string)enumerator.Value;
            }
            else if (enumerator.Name == "offcolor")
            {
                offcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "oncolor")
            {
                oncolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "opstflag")
            {
                opstflag = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "txtcolor")
            {
                txtcolor = (Color)enumerator.Value;
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
        info.AddValue("bgcolor", bgcolor);
        info.AddValue("initflag", initflag);
        info.AddValue("mark", mark);
        info.AddValue("offcolor", offcolor);
        info.AddValue("oncolor", oncolor);
        info.AddValue("opstflag", opstflag);
        info.AddValue("txtcolor", txtcolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
    }

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap10 = (drawbitmap2)base.Copy();
        drawbitmap10.bgcolor = bgcolor;
        drawbitmap10.txtcolor = txtcolor;
        drawbitmap10.oncolor = oncolor;
        drawbitmap10.offcolor = offcolor;
        drawbitmap10.varname = varname;
        drawbitmap10.mark = mark;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Times New Roman");
        Font font = new(family, 15f);
        Rectangle rect = new(0, 0, 200 * _width / 200, 270 * _height / 270);
        SolidBrush brush = new(bgcolor);
        SolidBrush brush2 = new(oncolor);
        new SolidBrush(offcolor);
        SolidBrush brush3 = new(txtcolor);
        g.FillRectangle(brush, rect);
        g.DrawRectangle(rect: new Rectangle(15 * _width / 200, 15 * _height / 270, 170 * _width / 200, 120 * _height / 270), pen: Pens.Black);
        Rectangle rect3 = new(22 * _width / 200, 22 * _height / 270, 156 * _width / 200, 106 * _height / 270);
        if (initflag == 1)
        {
            g.FillRectangle(brush2, rect3);
        }
        g.DrawString(point: new PointF(70 * _width / 200, 200 * _height / 270), s: mark, font: font, brush: brush3);
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(varname) && (float)e.X < Math.Abs(Width) && (float)e.Y < Math.Abs(Height))
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
                _height = 270;
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
            if (obj == null)
            {
                Paint(graphics);
            }
            else
            {
                try
                {
                    Value = Convert.ToBoolean(obj);
                    initflag = 0;
                    new SolidBrush(bgcolor);
                    Paint(graphics);
                    if (opstflag)
                    {
                        if (!Value)
                        {
                            Rectangle rect = new(22 * _width / 200, 22 * _height / 270, 156 * _width / 200, 106 * _height / 270);
                            graphics.FillRectangle(brush, rect);
                        }
                        else
                        {
                            Rectangle rect2 = new(22 * _width / 200, 22 * _height / 270, 156 * _width / 200, 106 * _height / 270);
                            graphics.FillRectangle(brush2, rect2);
                        }
                    }
                    else if (Value)
                    {
                        Rectangle rect3 = new(22 * _width / 200, 22 * _height / 270, 156 * _width / 200, 106 * _height / 270);
                        graphics.FillRectangle(brush, rect3);
                    }
                    else
                    {
                        Rectangle rect4 = new(22 * _width / 200, 22 * _height / 270, 156 * _width / 200, 106 * _height / 270);
                        graphics.FillRectangle(brush2, rect4);
                    }
                }
                catch
                {
                }
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
        KGSet3 kGSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.opstflag = opstflag;
            kGSet.Bgcolor = bgcolor;
            kGSet.varname = varname;
            kGSet.Oncolor = oncolor;
            kGSet.Offcolor = offcolor;
            kGSet.txtstr = mark;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.opstflag;
            bgcolor = kGSet.Bgcolor;
            varname = kGSet.varname;
            mark = kGSet.txtstr;
            oncolor = kGSet.Oncolor;
            offcolor = kGSet.Offcolor;
            txtcolor = kGSet.Txtcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            bgcolor = bgcolor,
            txtcolor = txtcolor,
            offcolor = offcolor,
            oncolor = oncolor,
            mark = mark,
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
            bgcolor = bSave.bgcolor;
            txtcolor = bSave.txtcolor;
            oncolor = bSave.oncolor;
            offcolor = bSave.offcolor;
            mark = bSave.mark;
            varname = bSave.varname;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
