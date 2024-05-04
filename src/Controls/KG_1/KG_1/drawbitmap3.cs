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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap3 : CPixieControl
{
    private Color setcolor = Color.Red;

    private Color setbgcolor = Color.Gray;

    private string varname = "";

    private string mark = "MM";

    private int _width = 200;

    private int _height = 200;

    private int initflag = 1;

    private bool opstflag;

    private bool value;

    [DisplayName("开关颜色")]
    [DHMICtrlProperty]
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

    [ReadOnly(false)]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("背景颜色。")]
    [DisplayName("背景颜色")]
    public Color MyControlbgColor
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
    [Description("开关绑定变量。")]
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

    [Description("开关显示文本。")]
    [DisplayName("文本")]
    [ReadOnly(false)]
    [Category("杂项")]
    [DHMICtrlProperty]
    public string Text
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

    [DHMICtrlProperty]
    [Description("是否取反。")]
    [DisplayName("是否取反")]
    [Category("杂项")]
    [ReadOnly(false)]
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

    [DisplayName("绑定变量当前值")]
    [Category("杂项")]
    [Description("绑定变量当前值。")]
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

    public drawbitmap3()
    {
    }

    protected drawbitmap3(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap3))
            {
                FieldInfo fieldInfo2 = (FieldInfo)serializableMembers[j];
                if (!Attribute.IsDefined(serializableMembers[j], typeof(NonSerializedAttribute)))
                {
                    fieldInfo2.SetValue(this, info.GetValue(fieldInfo2.Name, fieldInfo2.FieldType));
                }
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap3) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap3 drawbitmap10 = (drawbitmap3)base.Copy();
        drawbitmap10.setcolor = setcolor;
        drawbitmap10.opstflag = opstflag;
        drawbitmap10.setbgcolor = setbgcolor;
        drawbitmap10.mark = mark;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Arial");
        SolidBrush brush = new(setbgcolor);
        Font font = new(family, Convert.ToInt32((double)_width * 0.06));
        Rectangle rect = new(0, 0, Convert.ToInt32(_width), Convert.ToInt32(_height));
        g.FillRectangle(brush, rect);
        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.2), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.6));
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.24), Convert.ToInt32((double)_height * 0.24), Convert.ToInt32((double)_width * 0.52), Convert.ToInt32((double)_height * 0.52));
        Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.28), Convert.ToInt32((double)_height * 0.28), Convert.ToInt32((double)_width * 0.44), Convert.ToInt32((double)_height * 0.44));
        Rectangle rect5 = new(Convert.ToInt32((double)_width * 0.36), Convert.ToInt32((double)_height * 0.36), Convert.ToInt32((double)_width * 0.28), Convert.ToInt32((double)_height * 0.28));
        g.DrawArc(Pens.Black, rect2, 0f, 360f);
        g.DrawArc(Pens.White, rect3, 0f, 360f);
        g.DrawArc(Pens.Black, rect5, 0f, 360f);
        g.FillPie(Brushes.LightGray, rect4, 0f, 360f);
        if (initflag == 1)
        {
            Rectangle rect6 = new(Convert.ToInt32((double)_width * 0.46), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.36));
            SolidBrush brush2 = new(setcolor);
            g.DrawRectangle(Pens.Black, rect6);
            g.FillRectangle(brush2, rect6);
            g.DrawLine(Pens.Gray, Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.4));
        }
        PointF point = new(Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.8));
        PointF point2 = new(1f, Convert.ToInt32((double)_height * 0.46));
        PointF point3 = new(Convert.ToInt32((double)_width * 0.44), 1f);
        g.DrawString(mark, font, Brushes.Black, point);
        g.DrawString("ON", font, Brushes.Blue, point2);
        g.DrawString("OFF", font, Brushes.Blue, point3);
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
                _height = 200;
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
                    if (Value)
                    {
                        Rectangle rect = new(Convert.ToInt32((double)_width * 0.46), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.36));
                        graphics.DrawRectangle(Pens.Black, rect);
                        graphics.FillRectangle(brush, rect);
                        graphics.DrawLine(Pens.Gray, Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.4));
                    }
                    else
                    {
                        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.32), Convert.ToInt32((double)_height * 0.46), Convert.ToInt32((double)_width * 0.36), Convert.ToInt32((double)_height * 0.1));
                        graphics.DrawRectangle(Pens.Black, rect2);
                        graphics.FillRectangle(brush, rect2);
                        graphics.DrawLine(Pens.Gray, Convert.ToInt32((double)_width * 0.32), Convert.ToInt32((double)_height * 0.5), Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.5));
                    }
                }
                else if (!Value)
                {
                    Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.46), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.36));
                    graphics.DrawRectangle(Pens.Black, rect3);
                    graphics.FillRectangle(brush, rect3);
                    graphics.DrawLine(Pens.Gray, Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.32), Convert.ToInt32((double)_width * 0.5), Convert.ToInt32((double)_height * 0.4));
                }
                else
                {
                    Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.32), Convert.ToInt32((double)_height * 0.46), Convert.ToInt32((double)_width * 0.36), Convert.ToInt32((double)_height * 0.1));
                    graphics.DrawRectangle(Pens.Black, rect4);
                    graphics.FillRectangle(brush, rect4);
                    graphics.DrawLine(Pens.Gray, Convert.ToInt32((double)_width * 0.32), Convert.ToInt32((double)_height * 0.5), Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.5));
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
        KGSet2 kGSet = new();
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (!string.IsNullOrEmpty(varname))
        {
            kGSet.opstflag = opstflag;
            kGSet.setcolor = setcolor;
            kGSet.setbgcolor = setbgcolor;
            kGSet.varname = varname.Substring(1, varname.Length - 2);
            kGSet.mark = mark;
        }
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.opstflag;
            setcolor = kGSet.setcolor;
            setbgcolor = kGSet.setbgcolor;
            varname = "[" + kGSet.varname + "]";
            mark = kGSet.mark;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            setcolor = setcolor
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
            BSave bSave = (BSave)formatter.Deserialize(stream);
            stream.Close();
            setcolor = bSave.setcolor;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
