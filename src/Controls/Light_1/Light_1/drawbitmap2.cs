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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap2 : CPixieControl
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

    private int _width = 200;

    private int _height = 135;

    private int tickcount;

    private int i = 1000;

    [NonSerialized]
    private Timer tm1;

    private bool value;

    private bool value2;

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("为真时颜色")]
    [Category("杂项")]
    [Description("绑定变量为真时，报警灯的颜色。")]
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
    [DisplayName("闪烁频率")]
    [Category("杂项")]
    [Description("可以设置1至3之间的整数，为1时最慢，为3时最快。")]
    [ReadOnly(false)]
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

    [Description("报警灯绑定的变量，用于控制报警灯的颜色。")]
    [DisplayName("报警变量")]
    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Category("杂项")]
    [DisplayName("闪烁控制变量")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("当配置报警灯允许闪烁时，用于控制报警灯是否闪烁，不为零时报警灯闪烁。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DHMICtrlProperty]
    [DisplayName("是否闪烁")]
    [Category("杂项")]
    [Description("控制是否允许报警灯闪烁，为1时允许闪烁，为0时不允许闪烁。")]
    [ReadOnly(false)]
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
    [DisplayName("报警变量当前值")]
    [ReadOnly(false)]
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
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, null);
                }
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    [Description("报警变量当前值。")]
    [ReadOnly(false)]
    [Category("杂项")]
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

    public drawbitmap2()
    {
    }

    protected drawbitmap2(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap2), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap2))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap2), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap2) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        catch
        {
        }
    }

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap7 = (drawbitmap2)base.Copy();
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
        Rectangle rect = new(0, 0, 200 * _width / 200, 135 * _height / 135);
        Rectangle rect2 = new(20 * _width / 200, 15 * _height / 135, 160 * _width / 200, 105 * _height / 135);
        g.FillRectangle(Brushes.Black, rect);
        if (initflag == 1)
        {
            SolidBrush brush = new(falsecolor);
            g.FillRectangle(brush, rect2);
            g.DrawRectangle(Pens.Gray, rect2);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname1);
            if (Height == 0f || Width == 0f)
            {
                _height = 135;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new(truecolor);
            SolidBrush brush2 = new(falsecolor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value = Convert.ToBoolean(obj);
            }
            try
            {
                initflag = 0;
                Rectangle rect = new(20 * _width / 200, 15 * _height / 135, 160 * _width / 200, 105 * _height / 135);
                Paint(graphics);
                if (Value)
                {
                    graphics.FillRectangle(brush, rect);
                    graphics.DrawRectangle(Pens.Gray, rect);
                }
                else
                {
                    graphics.FillRectangle(brush2, rect);
                    graphics.DrawRectangle(Pens.Gray, rect);
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
                    i = 600;
                    break;
                case 3:
                    i = 200;
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
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
