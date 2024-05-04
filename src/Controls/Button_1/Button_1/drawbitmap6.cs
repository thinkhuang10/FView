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
public class drawbitmap6 : CPixieControl
{
    private string varname = "";

    private Color txtcolor = Color.Black;

    private string txt = "Check Box";

    private int weith = 200;

    private int hight = 50;

    [NonSerialized]
    private bool value;

    [DisplayName("绑定变量")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
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

    [DisplayName("文本颜色")]
    [ReadOnly(false)]
    [Description("控件显示的文本的颜色。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    public Color Textcolor
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("文本")]
    [Category("杂项")]
    [Description("控件显示文本。")]
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

    [Description("按钮绑定变量当前值。")]
    [ReadOnly(false)]
    [Category("杂项")]
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
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    public new event EventHandler Click;

    public drawbitmap6()
    {
    }

    protected drawbitmap6(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap6 obj = new();
        FieldInfo[] fields = typeof(drawbitmap6).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap6))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap6), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap6))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap6), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap6) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap6 drawbitmap32 = (drawbitmap6)base.Copy();
        drawbitmap32.varname = varname;
        drawbitmap32.txtcolor = txtcolor;
        drawbitmap32.txt = txt;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(txtcolor);
        Rectangle rect = new(Convert.ToInt32((double)weith * 0.03), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.1), Convert.ToInt32((double)hight * 0.5));
        Pen pen = new(Color.Black, 3f);
        g.DrawRectangle(pen, rect);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)hight * 0.5));
        g.DrawString(point: new Point(Convert.ToInt32((double)weith * 0.15), Convert.ToInt32((double)hight * 0.15)), s: txt, font: font, brush: brush);
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (this.Click != null)
            {
                this.Click(this, null);
            }
            if (!string.IsNullOrEmpty(varname) && e.X < weith)
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
            if (Height == 0f || Width == 0f)
            {
                hight = 50;
                weith = 200;
            }
            else
            {
                hight = Convert.ToInt32(Math.Abs(Height));
                weith = Convert.ToInt32(Math.Abs(Width));
            }
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            Bitmap bitmap = new(weith, hight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                if (Value)
                {
                    Paint(graphics);
                    Pen pen = new(Color.Red, 2f);
                    graphics.DrawLine(pen, Convert.ToInt32((double)weith * 0.02), Convert.ToInt32((double)hight * 0.34), Convert.ToInt32((double)weith * 0.09), Convert.ToInt32((double)hight * 0.66));
                    graphics.DrawLine(pen, Convert.ToInt32((double)weith * 0.09), Convert.ToInt32((double)hight * 0.66), Convert.ToInt32((double)weith * 0.21), Convert.ToInt32((double)hight * 0.2));
                }
                else
                {
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
        BtnSet3 btnSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            btnSet.varname = varname;
            btnSet.txtcolor = txtcolor;
            btnSet.txt = txt;
        }
        btnSet.viewevent += GetTable;
        btnSet.ckvarevent += CheckVar;
        if (btnSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + btnSet.varname + "]";
            txt = btnSet.txt;
            txtcolor = btnSet.txtcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC6 bSaveC = new()
        {
            varname = varname,
            txt = txt,
            txtcolor = txtcolor
        };
        formatter.Serialize(memoryStream, bSaveC);
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
            BSaveC6 bSaveC = (BSaveC6)formatter.Deserialize(stream);
            stream.Close();
            varname = bSaveC.varname;
            txtcolor = bSaveC.txtcolor;
            txt = bSaveC.txt;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
