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

namespace Light_1;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap6 : CPixieControl
{
    private Color truecolor = Color.Green;

    private Color falsecolor = Color.Red;

    private Color truecolorbuf = Color.Green;

    private Color falsecolorbuf = Color.Red;

    private Color txtcolor = Color.Black;

    private string truetxt = "真";

    private string falsetxt = "假";

    private int spdtype = 1;

    private string varname1 = "";

    private string varname2 = "";

    private int lighttype = 1;

    private int btwth = 100;

    private int bthight = 80;

    private int initflag = 1;

    private int tickcount;

    private int i = 1000;

    [NonSerialized]
    private Timer tm1;

    private bool value;

    private bool value2;

    [DHMICtrlProperty]
    [Description("绑定变量为真时，报警灯的颜色。")]
    [ReadOnly(false)]
    [DisplayName("为真时颜色")]
    [Category("杂项")]
    public Color Truecolor
    {
        get
        {
            return truecolor;
        }
        set
        {
            truecolor = value;
            truecolorbuf = value;
        }
    }

    [DisplayName("为假时颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("绑定变量为假时，报警灯的颜色。")]
    [ReadOnly(false)]
    public Color Falsecolor
    {
        get
        {
            return falsecolor;
        }
        set
        {
            falsecolor = value;
            falsecolorbuf = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("文本颜色")]
    [Category("杂项")]
    [Description("报警灯显示的文本的颜色。")]
    [ReadOnly(false)]
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

    [Category("杂项")]
    [DisplayName("为真时文本")]
    [Description("报警灯绑定变量为真时显示的文本。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string Truetext
    {
        get
        {
            return truetxt;
        }
        set
        {
            truetxt = value;
        }
    }

    [Description("报警灯绑定变量为假时显示的文本。")]
    [DisplayName("为假时文本")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public string Falsetext
    {
        get
        {
            return falsetxt;
        }
        set
        {
            falsetxt = value;
        }
    }

    [DisplayName("闪烁频率")]
    [Category("杂项")]
    [Description("可以设置1至3之间的整数，为1时最慢，为3时最快。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public int SpeedLevel
    {
        get
        {
            return spdtype;
        }
        set
        {
            spdtype = value;
            switch (spdtype)
            {
                case 1:
                    i = 1000;
                    break;
                case 2:
                    i = 600;
                    break;
                case 3:
                    i = 200;
                    break;
                default:
                    spdtype = 1;
                    i = 1000;
                    break;
            }
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("报警灯绑定的变量，用于控制报警灯的颜色。")]
    [DisplayName("报警变量")]
    public string BindVar
    {
        get
        {
            if (varname1.ToString().IndexOf('[') == -1)
            {
                return varname1;
            }
            return varname1.Substring(1, varname1.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname1 = "[" + value.ToString() + "]";
            }
            else
            {
                varname1 = value;
            }
        }
    }

    [DisplayName("闪烁控制变量")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("当配置报警灯允许闪烁时，用于控制报警灯是否闪烁，不为零时报警灯闪烁。")]
    [ReadOnly(false)]
    public string FlashVar
    {
        get
        {
            if (varname2.ToString().IndexOf('[') == -1)
            {
                return varname2;
            }
            return varname2.Substring(1, varname2.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname2 = "[" + value.ToString() + "]";
            }
            else
            {
                varname2 = value;
            }
        }
    }

    [Category("杂项")]
    [DisplayName("是否闪烁")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("控制是否允许报警灯闪烁，为1时允许闪烁，为0时不允许闪烁。")]
    public int IsFlash
    {
        get
        {
            return lighttype;
        }
        set
        {
            lighttype = value;
        }
    }

    [Description("报警变量当前值。")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("报警变量当前值")]
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

    [Category("杂项")]
    [Description("报警变量当前值。")]
    [ReadOnly(false)]
    [DisplayName("闪烁控制变量当前值")]
    public bool Value2
    {
        get
        {
            return value2;
        }
        set
        {
            if (value2 != value)
            {
                NeedRefresh = true;
                value2 = value;
            }
        }
    }

    public event EventHandler ValueChanged;

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

    public void tm1_Tick(object sender, EventArgs e)
    {
        try
        {
            if (!isRunning)
            {
                return;
            }
            NeedRefresh = true;
            object obj = (string.IsNullOrEmpty(varname2) ? null : GetValue(varname2));
            if (obj != null)
            {
                Value2 = Convert.ToBoolean(obj);
            }
            if (lighttype == 1 && Value2)
            {
                if (tickcount % 2 == 0)
                {
                    truecolor = truecolorbuf;
                    falsecolor = falsecolorbuf;
                }
                else
                {
                    truecolor = Color.Transparent;
                    falsecolor = Color.Transparent;
                }
                tickcount++;
            }
            if (!Value2)
            {
                truecolor = truecolorbuf;
                falsecolor = falsecolorbuf;
            }
        }
        catch
        {
        }
    }

    public override CShape Copy()
    {
        drawbitmap6 drawbitmap7 = (drawbitmap6)base.Copy();
        drawbitmap7.truecolor = truecolor;
        drawbitmap7.falsecolor = falsecolor;
        drawbitmap7.varname1 = varname1;
        drawbitmap7.varname2 = varname2;
        drawbitmap7.spdtype = spdtype;
        drawbitmap7.lighttype = lighttype;
        drawbitmap7.txtcolor = txtcolor;
        drawbitmap7.truetxt = truetxt;
        drawbitmap7.falsetxt = falsetxt;
        return drawbitmap7;
    }

    public void Paint(Graphics g)
    {
        g.FillRectangle(rect: new Rectangle(0, 0, btwth, bthight), brush: Brushes.LightGray);
        g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)btwth * 0.1), Convert.ToInt32((double)bthight * 0.1), Convert.ToInt32((double)btwth * 0.8), Convert.ToInt32((double)bthight * 0.8)), brush: Brushes.White);
        if (initflag == 1)
        {
            Rectangle rect3 = new(Convert.ToInt32((double)btwth * 0.15), Convert.ToInt32((double)bthight * 0.15), Convert.ToInt32((double)btwth * 0.7), Convert.ToInt32((double)bthight * 0.7));
            SolidBrush brush = new(falsecolor);
            g.FillRectangle(brush, rect3);
            g.DrawRectangle(Pens.Gray, rect3);
            Point point = new(Convert.ToInt32((double)btwth * 0.5 - (double)(falsetxt.Length * btwth) * 0.05), Convert.ToInt32((double)bthight * 0.45));
            FontFamily family = new("宋体");
            Font font = new(family, Convert.ToInt32((double)btwth * 0.06));
            SolidBrush brush2 = new(txtcolor);
            g.DrawString(falsetxt, font, brush2, point);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            if (Height == 0f || Width == 0f)
            {
                bthight = 160;
                btwth = 200;
            }
            else
            {
                bthight = Convert.ToInt32(Math.Abs(Height));
                btwth = Convert.ToInt32(Math.Abs(Width));
            }
            object obj = GetValue(varname1);
            Bitmap bitmap = new(btwth, bthight);
            Graphics graphics = Graphics.FromImage(bitmap);
            new SolidBrush(truecolor);
            new SolidBrush(falsecolor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                initflag = 0;
                Paint(graphics);
                if (Value)
                {
                    Rectangle rect = new(Convert.ToInt32((double)btwth * 0.15), Convert.ToInt32((double)bthight * 0.15), Convert.ToInt32((double)btwth * 0.7), Convert.ToInt32((double)bthight * 0.7));
                    SolidBrush brush = new(truecolor);
                    graphics.FillRectangle(brush, rect);
                    graphics.DrawRectangle(Pens.Gray, rect);
                    Point point = new(Convert.ToInt32((double)btwth * 0.5 - (double)(truetxt.Length * btwth) * 0.05), Convert.ToInt32((double)bthight * 0.45));
                    FontFamily family = new("宋体");
                    Font font = new(family, Convert.ToInt32((double)btwth * 0.06));
                    SolidBrush brush2 = new(txtcolor);
                    graphics.DrawString(truetxt, font, brush2, point);
                }
                else
                {
                    Rectangle rect2 = new(Convert.ToInt32((double)btwth * 0.15), Convert.ToInt32((double)bthight * 0.15), Convert.ToInt32((double)btwth * 0.7), Convert.ToInt32((double)bthight * 0.7));
                    SolidBrush brush3 = new(falsecolor);
                    graphics.FillRectangle(brush3, rect2);
                    graphics.DrawRectangle(Pens.Gray, rect2);
                    Point point2 = new(Convert.ToInt32((double)btwth * 0.5 - (double)(falsetxt.Length * btwth) * 0.05), Convert.ToInt32((double)bthight * 0.45));
                    FontFamily family2 = new("宋体");
                    Font font2 = new(family2, Convert.ToInt32((double)btwth * 0.06));
                    SolidBrush brush4 = new(txtcolor);
                    graphics.DrawString(falsetxt, font2, brush4, point2);
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
        LSet2 lSet = new();
        if (!string.IsNullOrEmpty(varname1))
        {
            lSet.varname1 = varname1;
            lSet.varname2 = varname2;
            lSet.truecolor = truecolor;
            lSet.falsecolor = falsecolor;
            lSet.spdtype = spdtype;
            lSet.lighttype = lighttype;
            lSet.txtcolor = txtcolor;
            lSet.truetxt = truetxt;
            lSet.falsetxt = falsetxt;
        }
        lSet.viewevent += GetTable;
        lSet.ckvarevent += CheckVar;
        if (lSet.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        varname1 = "[" + lSet.varname1 + "]";
        varname2 = "[" + lSet.varname2 + "]";
        truecolor = lSet.truecolor;
        falsecolor = lSet.falsecolor;
        lighttype = lSet.lighttype;
        truecolorbuf = truecolor;
        falsecolorbuf = falsecolor;
        spdtype = lSet.spdtype;
        txtcolor = lSet.txtcolor;
        truetxt = lSet.truetxt;
        falsetxt = lSet.falsetxt;
        if (lSet.lighttype == 1)
        {
            switch (lSet.spdtype)
            {
                case 1:
                    i = 1000;
                    break;
                case 2:
                    i = 700;
                    break;
                case 3:
                    i = 400;
                    break;
            }
        }
        NeedRefresh = true;
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            truecolor = truecolor,
            falsecolor = falsecolor,
            spdtype = spdtype,
            varname1 = varname1,
            varname2 = varname2,
            lighttype = lighttype,
            i = i
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
            truecolor = bSave.truecolor;
            falsecolor = bSave.falsecolor;
            spdtype = bSave.spdtype;
            varname1 = bSave.varname1;
            varname2 = bSave.varname2;
            lighttype = bSave.lighttype;
            tm1 = new Timer
            {
                Interval = bSave.i
            };
            tm1.Tick += tm1_Tick;
            tm1.Enabled = true;
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
