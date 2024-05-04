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

namespace Button_1;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap4 : CPixieControl
{
    private string varname = "";

    private bool mousedownflag;

    private string txt = "开关";

    private Color oncolor = Color.Green;

    private Color offcolor = Color.Red;

    private Color topainedcolor = Color.Red;

    private int btntype = 3;

    public int initflag = 1;

    private int weith = 130;

    private int hight = 50;

    [NonSerialized]
    private bool value;

    [Description("控件绑定变量。")]
    [Category("杂项")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
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

    [Description("按钮显示文本。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("文本")]
    [ReadOnly(false)]
    public string Text
    {
        get
        {
            return txt;
        }
        set
        {
            txt = value;
        }
    }

    [DisplayName("开颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("按钮绑定变量为真时显示的颜色。")]
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

    [DisplayName("关颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("按钮绑定变量为假时显示的颜色。")]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("可设为只开、只关和切换三种类型。")]
    [DisplayName("按钮类型")]
    public string ButtonType
    {
        get
        {
            if (btntype == 1)
            {
                return "只开";
            }
            if (btntype == 1)
            {
                return "只关";
            }
            if (btntype == 1)
            {
                return "切换";
            }
            return "切换";
        }
        set
        {
            if (value.ToString() == "只开")
            {
                btntype = 1;
            }
            else if (value.ToString() == "只关")
            {
                btntype = 2;
            }
            else if (value.ToString() == "切换")
            {
                btntype = 3;
            }
            else
            {
                btntype = 3;
            }
        }
    }

    [ReadOnly(false)]
    [Description("按钮绑定变量当前值。")]
    [DisplayName("当前值")]
    [Category("杂项")]
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
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public new event EventHandler Click;

    public drawbitmap4()
    {
    }

    protected drawbitmap4(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap4))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap4) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override CShape Copy()
    {
        drawbitmap4 drawbitmap32 = (drawbitmap4)base.Copy();
        drawbitmap32.varname = varname;
        drawbitmap32.oncolor = oncolor;
        drawbitmap32.offcolor = offcolor;
        drawbitmap32.btntype = btntype;
        drawbitmap32.txt = txt;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.White, 1f);
        Pen pen2 = new(Color.Black, 1f);
        g.FillRectangle(rect: new Rectangle(0, 0, weith, hight), brush: Brushes.Gray);
        SolidBrush brush = new(topainedcolor);
        Rectangle rect2 = new(Convert.ToInt32((double)weith * 0.64), Convert.ToInt32((double)hight * 0.05), Convert.ToInt32((double)weith * 0.33), Convert.ToInt32((double)hight * 0.9));
        g.FillRectangle(brush, rect2);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)weith * 0.1));
        g.DrawString(txt, font, Brushes.Black, Convert.ToSingle((double)weith * 0.07), Convert.ToSingle((double)hight * 0.33));
        g.DrawLine(pen, Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.05), Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.95));
        g.DrawLine(pen, Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.05), Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.05));
        g.DrawLine(pen2, Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.95), Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.05));
        g.DrawLine(pen2, Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.95), Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.95));
        if (mousedownflag)
        {
            g.DrawLine(pen2, Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.05), Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.95));
            g.DrawLine(pen2, Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.05), Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.05));
            g.DrawLine(pen, Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.95), Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.05));
            g.DrawLine(pen, Convert.ToSingle((double)weith * 0.97), Convert.ToSingle((double)hight * 0.95), Convert.ToSingle((double)weith * 0.64), Convert.ToSingle((double)hight * 0.95));
        }
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        mousedownflag = false;
        NeedRefresh = true;
    }

    public override void ManageMouseMove(MouseEventArgs e)
    {
        base.ManageMouseMove(e);
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (this.Click != null)
            {
                this.Click(this, null);
            }
            mousedownflag = true;
            if (!string.IsNullOrEmpty(varname) && (double)e.X > (double)Math.Abs(Width) * 0.64 && (double)e.X < (double)Math.Abs(Width) * 0.97 && (double)e.Y > (double)Math.Abs(Height) * 0.05 && (double)e.Y < (double)Math.Abs(Height) * 0.95)
            {
                if (btntype == 1)
                {
                    SetValue(varname, true);
                }
                else if (btntype == 2)
                {
                    SetValue(varname, false);
                }
                else if (Convert.ToBoolean(GetValue(varname)))
                {
                    SetValue(varname, false);
                }
                else
                {
                    SetValue(varname, true);
                }
            }
            if ((double)e.X > (double)Math.Abs(Width) * 0.64 && (double)e.X < (double)Math.Abs(Width) * 0.97 && (double)e.Y > (double)Math.Abs(Height) * 0.05 && (double)e.Y < (double)Math.Abs(Height) * 0.95)
            {
                NeedRefresh = true;
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
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                hight = 80;
                weith = 200;
            }
            else
            {
                hight = Convert.ToInt32(Math.Abs(Height));
                weith = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(weith, hight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Paint(graphics);
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                if (Value)
                {
                    topainedcolor = oncolor;
                    Paint(graphics);
                }
                else
                {
                    topainedcolor = offcolor;
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void ShowDialog()
    {
        BtnSet2 btnSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            btnSet.varname = varname;
            btnSet.oncolor = oncolor;
            btnSet.offcolor = offcolor;
            btnSet.btntype = btntype;
            btnSet.txt = txt;
        }
        btnSet.viewevent += GetTable;
        btnSet.ckvarevent += CheckVar;
        if (btnSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + btnSet.varname + "]";
            oncolor = btnSet.oncolor;
            offcolor = btnSet.offcolor;
            btntype = btnSet.btntype;
            txt = btnSet.txt;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            varname = varname,
            oncolor = oncolor,
            offcolor = offcolor,
            txt = txt,
            btntype = btntype
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
            varname = bSave.varname;
            oncolor = bSave.oncolor;
            offcolor = bSave.offcolor;
            txt = bSave.txt;
            btntype = bSave.btntype;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
