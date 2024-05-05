using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq.Expressions;
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
public class drawbitmap : CPixieControl
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

    private int _height = 240;

    private int tickcount;

    private int i = 1000;

    [NonSerialized]
    private Timer tm1;

    private bool value;

    private bool value2;

    [DHMICtrlProperty]
    [DisplayName("为真时颜色")]
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
    [Description("绑定变量为假时，报警灯的颜色。")]
    [ReadOnly(false)]
    [DisplayName("为假时颜色")]
    [Category("杂项")]
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
    [Description("可以设置1至3之间的整数，为1时最慢，为3时最快。")]
    [DisplayName("闪烁频率")]
    [DHMICtrlProperty]
    [Category("杂项")]
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
                    i = 200;
                    break;
                case 3:
                    i = 10;
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
    [DisplayName("报警变量")]
    [Category("杂项")]
    [Description("报警灯绑定的变量，用于控制报警灯的颜色。")]
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

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("当配置报警灯允许闪烁时，用于控制报警灯是否闪烁，不为零时报警灯闪烁。")]
    [DisplayName("闪烁控制变量")]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("是否闪烁")]
    [Category("杂项")]
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

    public drawbitmap()
    {
    }

    protected drawbitmap(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    private void tm1_Tick(object sender, EventArgs e)
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
        drawbitmap drawbitmap7 = (drawbitmap)base.Copy();
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
        g.FillPie(rect: new Rectangle(0, 120 * _height / 240, 200 * _width / 200, 112 * _height / 240), brush: Brushes.Black, startAngle: 0f, sweepAngle: 180f);
        g.FillRectangle(rect: new Rectangle(0, 152 * _height / 240, 200 * _width / 200, 32 * _height / 240), brush: Brushes.Black);
        g.FillPie(rect: new Rectangle(0, 104 * _height / 240, 200 * _width / 200, 112 * _height / 240), brush: Brushes.Black, startAngle: 180f, sweepAngle: 180f);
        if (initflag == 1)
        {
            Rectangle rect4 = new(40 * _width / 200, 136 * _height / 240, 120 * _width / 200, 64 * _height / 240);
            SolidBrush brush = new(falsecolor);
            g.FillPie(brush, rect4, 0f, 180f);
            g.DrawArc(Pens.Gray, rect4, 0f, 360f);
            Rectangle rect5 = new(40 * _width / 200, 36 * _height / 240, 120 * _width / 200, 136 * _height / 240);
            g.FillRectangle(brush, rect5);
            g.DrawLine(Pens.Gray, 40 * _width / 200, 36 * _height / 240, 40 * _width / 200, 172 * _height / 240);
            g.DrawLine(Pens.Gray, 160 * _width / 200, 36 * _height / 240, 160 * _width / 200, 172 * _height / 240);
            Rectangle rect6 = new(40 * _width / 200, 0, 120 * _width / 200, 80 * _height / 240);
            g.DrawArc(Pens.Gray, rect6, 180f, 180f);
            g.FillPie(brush, rect6, 180f, 180f);
            g.FillPolygon(points: new Point[6]
            {
                new(100 * _width / 200, 12 * _height / 240),
                new(88 * _width / 200, 20 * _height / 240),
                new(68 * _width / 200, 32 * _height / 240),
                new(64 * _width / 200, 48 * _height / 240),
                new(80 * _width / 200, 52 * _height / 240),
                new(92 * _width / 200, 40 * _height / 240)
            }, brush: Brushes.White);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname1);
            if (Height == 0f || Width == 0f)
            {
                _height = 240;
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
                new Rectangle(120 * _width / 200, 220 * _height / 240, 80 * _width / 200, 40 * _height / 240);
                Paint(graphics);
                if (Value)
                {
                    Rectangle rect = new(40 * _width / 200, 136 * _height / 240, 120 * _width / 200, 64 * _height / 240);
                    SolidBrush brush = new(truecolor);
                    graphics.FillPie(brush, rect, 0f, 180f);
                    graphics.DrawArc(Pens.Gray, rect, 0f, 360f);
                    Rectangle rect2 = new(40 * _width / 200, 36 * _height / 240, 120 * _width / 200, 136 * _height / 240);
                    graphics.FillRectangle(brush, rect2);
                    graphics.DrawLine(Pens.Gray, 40 * _width / 200, 36 * _height / 240, 40 * _width / 200, 172 * _height / 240);
                    graphics.DrawLine(Pens.Gray, 160 * _width / 200, 36 * _height / 240, 160 * _width / 200, 172 * _height / 240);
                    Rectangle rect3 = new(40 * _width / 200, 0, 120 * _width / 200, 80 * _height / 240);
                    graphics.DrawArc(Pens.Gray, rect3, 180f, 180f);
                    graphics.FillPie(brush, rect3, 180f, 180f);
                    graphics.FillPolygon(points: new Point[6]
                    {
                        new(100 * _width / 200, 12 * _height / 240),
                        new(88 * _width / 200, 20 * _height / 240),
                        new(68 * _width / 200, 32 * _height / 240),
                        new(64 * _width / 200, 48 * _height / 240),
                        new(80 * _width / 200, 52 * _height / 240),
                        new(92 * _width / 200, 40 * _height / 240)
                    }, brush: Brushes.White);
                }
                else
                {
                    Rectangle rect4 = new(40 * _width / 200, 136 * _height / 240, 120 * _width / 200, 64 * _height / 240);
                    SolidBrush brush2 = new(falsecolor);
                    graphics.FillPie(brush2, rect4, 0f, 180f);
                    graphics.DrawArc(Pens.Gray, rect4, 0f, 360f);
                    Rectangle rect5 = new(40 * _width / 200, 36 * _height / 240, 120 * _width / 200, 136 * _height / 240);
                    graphics.FillRectangle(brush2, rect5);
                    graphics.DrawLine(Pens.Gray, 40 * _width / 200, 36 * _height / 240, 40 * _width / 200, 172 * _height / 240);
                    graphics.DrawLine(Pens.Gray, 160 * _width / 200, 36 * _height / 240, 160 * _width / 200, 172 * _height / 240);
                    Rectangle rect6 = new(40 * _width / 200, 0, 120 * _width / 200, 80 * _height / 240);
                    graphics.DrawArc(Pens.Gray, rect6, 180f, 180f);
                    graphics.FillPie(brush2, rect6, 180f, 180f);
                    graphics.FillPolygon(points: new Point[6]
                    {
                        new(100 * _width / 200, 12 * _height / 240),
                        new(88 * _width / 200, 20 * _height / 240),
                        new(68 * _width / 200, 32 * _height / 240),
                        new(64 * _width / 200, 48 * _height / 240),
                        new(80 * _width / 200, 52 * _height / 240),
                        new(92 * _width / 200, 40 * _height / 240)
                    }, brush: Brushes.White);
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
                    i = 200;
                    break;
                case 3:
                    i = 10;
                    break;
            }
        }
        NeedRefresh = true;
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
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

    public string GetTable()
    {
        return GetVarNames();
    }
}
