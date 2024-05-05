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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class drawbitmap3 : CPixieControl
{
    private Color truecolor = Color.Green;

    private Color falsecolor = Color.Red;

    private Color truecolorbuf = Color.Green;

    private Color falsecolorbuf = Color.Red;

    private int spdtype = 1;

    private string varname1 = "";

    private string varname2 = "";

    private int lighttype = 1;

    private int initflag = 1;

    private int _width = 220;

    private int _height = 220;

    private int tickcount;

    private int i = 1000;

    [NonSerialized]
    private Timer tm1;

    private bool value;

    private bool value2;

    [DisplayName("为真时颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("绑定变量为真时，报警灯的颜色。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [DisplayName("为假时颜色")]
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

    [ReadOnly(false)]
    [Category("杂项")]
    [DHMICtrlProperty]
    [Description("可以设置1至3之间的整数，为1时最慢，为3时最快。")]
    [DisplayName("闪烁频率")]
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
    [DisplayName("报警变量")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("报警灯绑定的变量，用于控制报警灯的颜色。")]
    [Category("杂项")]
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

    [Description("当配置报警灯允许闪烁时，用于控制报警灯是否闪烁，不为零时报警灯闪烁。")]
    [DisplayName("闪烁控制变量")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
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
    [DHMICtrlProperty]
    [DisplayName("是否闪烁")]
    [ReadOnly(false)]
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

    [DisplayName("报警变量当前值")]
    [ReadOnly(false)]
    [Description("报警变量当前值。")]
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
                ValueChanged?.Invoke(this, null);
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    [Description("报警变量当前值。")]
    [DisplayName("闪烁控制变量当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public void tm1_Tick(object sender, EventArgs e)
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
                NeedRefresh = true;
            }
            else
            {
                truecolor = Color.Transparent;
                falsecolor = Color.Transparent;
                NeedRefresh = true;
            }
            tickcount++;
        }
        if (!Value2)
        {
            truecolor = truecolorbuf;
            falsecolor = falsecolorbuf;
        }
    }

    public override CShape Copy()
    {
        drawbitmap3 drawbitmap7 = (drawbitmap3)base.Copy();
        drawbitmap7.truecolor = truecolor;
        drawbitmap7.falsecolor = falsecolor;
        drawbitmap7.varname1 = varname1;
        drawbitmap7.varname2 = varname2;
        drawbitmap7.spdtype = spdtype;
        drawbitmap7.lighttype = lighttype;
        return drawbitmap7;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 0, 200 * _width / 220, 200 * _height / 220);
        Rectangle rect2 = new(20 * _width / 220, 20 * _height / 220, 200 * _width / 220, 200 * _height / 220);
        g.FillPie(Brushes.Black, rect, 0f, 360f);
        g.FillPie(Brushes.Black, rect2, 0f, 360f);
        if (initflag == 1)
        {
            Rectangle rect3 = new(40 * _width / 220, 40 * _height / 220, 160 * _width / 220, 160 * _height / 220);
            SolidBrush brush = new(falsecolor);
            g.FillPie(brush, rect3, 0f, 360f);
            g.DrawArc(Pens.Gray, rect3, 0f, 360f);
            g.FillRectangle(rect: new Rectangle(68 * _width / 220, 68 * _height / 220, 20 * _width / 220, 20 * _height / 220), brush: Brushes.White);
            Rectangle rect5 = new(60 * _width / 220, 60 * _height / 220, 120 * _width / 220, 120 * _height / 220);
            Pen pen = new(Color.White, 5 * _width / 220);
            g.DrawArc(pen, rect5, 200f, 45f);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname1);
            if (Height == 0f || Width == 0f)
            {
                _height = 220;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
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
                    Rectangle rect = new(40 * _width / 220, 40 * _height / 220, 160 * _width / 220, 160 * _height / 220);
                    SolidBrush brush = new(truecolor);
                    graphics.FillPie(brush, rect, 0f, 360f);
                    graphics.DrawArc(Pens.Gray, rect, 0f, 360f);
                    graphics.FillRectangle(rect: new Rectangle(68 * _width / 220, 68 * _height / 220, 20 * _width / 220, 20 * _height / 220), brush: Brushes.White);
                    Rectangle rect3 = new(60 * _width / 220, 60 * _height / 220, 120 * _width / 220, 120 * _height / 220);
                    Pen pen = new(Color.White, 5 * _width / 220);
                    graphics.DrawArc(pen, rect3, 200f, 45f);
                }
                else
                {
                    Rectangle rect4 = new(40 * _width / 220, 40 * _height / 220, 160 * _width / 220, 160 * _height / 220);
                    SolidBrush brush2 = new(falsecolor);
                    graphics.FillPie(brush2, rect4, 0f, 360f);
                    graphics.DrawArc(Pens.Gray, rect4, 0f, 360f);
                    graphics.FillRectangle(rect: new Rectangle(68 * _width / 220, 68 * _height / 220, 20 * _width / 220, 20 * _height / 220), brush: Brushes.White);
                    Rectangle rect6 = new(60 * _width / 220, 60 * _height / 220, 120 * _width / 220, 120 * _height / 220);
                    Pen pen2 = new(Color.White, 5 * _width / 220);
                    graphics.DrawArc(pen2, rect6, 200f, 45f);
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
        Light_11 light_ = new();
        if (!string.IsNullOrEmpty(varname1))
        {
            light_.varname1 = varname1;
            light_.varname2 = varname2;
            light_.truecolor = truecolor;
            light_.falsecolor = falsecolor;
            light_.spdtype = spdtype;
            light_.lighttype = lighttype;
        }
        light_.viewevent += GetTable;
        light_.ckvarevent += CheckVar;
        if (light_.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        varname1 = "[" + light_.varname1 + "]";
        varname2 = "[" + light_.varname2 + "]";
        truecolor = light_.truecolor;
        falsecolor = light_.falsecolor;
        lighttype = light_.lighttype;
        spdtype = light_.spdtype;
        truecolorbuf = truecolor;
        falsecolorbuf = falsecolor;
        if (light_.lighttype == 1)
        {
            switch (light_.spdtype)
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
            tm1 = new Timer();
            tm1.Tick += tm1_Tick;
            tm1.Interval = bSave.i;
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
