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

namespace YiBiao;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap8 : CPixieControl
{
    private float MaxV = 300f;

    private float MinV;

    private int mainmarkcount = 10;

    private string varname = "";

    private string Mark = "仪表";

    private Color bqcolor = Color.Blue;

    private Color fillcolor = Color.Black;

    private int initflag = 1;

    private Color fontcolor = Color.Blue;

    private int _width = 200;

    private int _height = 140;

    private float value;

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("量程上限")]
    [Category("杂项")]
    [Description("仪表量程上限。")]
    public float MaxValue
    {
        get
        {
            return MaxV;
        }
        set
        {
            MaxV = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("量程下限")]
    [Category("杂项")]
    [Description("仪表量程下限。")]
    public float MinValue
    {
        get
        {
            return MinV;
        }
        set
        {
            MinV = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("主刻度数")]
    [Description("表盘主刻度线数量。")]
    [Category("杂项")]
    public int Mainmarkcount
    {
        get
        {
            return mainmarkcount;
        }
        set
        {
            mainmarkcount = value;
        }
    }

    [Description("控件绑定变量名。")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
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

    [DisplayName("文本")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘显示的文本。")]
    public string Text
    {
        get
        {
            return Mark;
        }
        set
        {
            Mark = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("液柱填充色")]
    [Category("杂项")]
    [Description("绑定变量变化时，填充液柱的颜色。")]
    public Color Fillcolor
    {
        get
        {
            return fillcolor;
        }
        set
        {
            fillcolor = value;
        }
    }

    [DHMICtrlProperty]
    [Description("表盘上显示的文本的字体颜色。")]
    [ReadOnly(false)]
    [DisplayName("文本颜色")]
    [Category("杂项")]
    public Color Fontcolor
    {
        get
        {
            return bqcolor;
        }
        set
        {
            bqcolor = value;
        }
    }

    [Description("仪表绑定变量的当前值。")]
    [DisplayName("当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
    public float Value
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap8))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap8) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override CShape Copy()
    {
        drawbitmap8 drawbitmap10 = (drawbitmap8)base.Copy();
        drawbitmap10.MaxV = MaxV;
        drawbitmap10.MinV = MinV;
        drawbitmap10.mainmarkcount = mainmarkcount;
        drawbitmap10.varname = varname;
        drawbitmap10.Mark = Mark;
        drawbitmap10.initflag = initflag;
        drawbitmap10.fillcolor = fillcolor;
        drawbitmap10.bqcolor = bqcolor;
        drawbitmap10.fontcolor = fontcolor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        g.FillRectangle(rect: new Rectangle(0, 0, 100 * _width / 100, 70 * _height / 70), brush: Brushes.LightGray);
        FontFamily family = new("Arial");
        Font font = new(family, 7 * _width / 100);
        SolidBrush brush = new(fillcolor);
        SolidBrush brush2 = new(bqcolor);
        if (initflag == 1)
        {
            Rectangle rect2 = new(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70);
            g.DrawRectangle(Pens.Black, rect2);
            g.FillRectangle(brush, rect2);
        }
        else if (Value >= MinV && Value <= MaxV)
        {
            Rectangle rect3 = new(15 * _width / 100, 15 * _height / 70, Convert.ToInt32(70f * ((Value - MinV) / (MaxV - MinV))) * _width / 100, 30 * _height / 70);
            Rectangle rect4 = new(15 * _width / 100 + Convert.ToInt32(70f * ((Value - MinV) / (MaxV - MinV))) * _width / 100, 15 * _height / 70, 70 * _width / 100 - Convert.ToInt32(70f * ((Value - MinV) / (MaxV - MinV))) * _width / 100, 30 * _height / 70);
            g.DrawRectangle(rect: new Rectangle(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70), pen: Pens.Black);
            g.FillRectangle(brush, rect3);
            g.FillRectangle(Brushes.White, rect4);
        }
        else if (Value > MaxV)
        {
            Rectangle rect6 = new(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70);
            g.DrawRectangle(rect: new Rectangle(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70), pen: Pens.Black);
            g.FillRectangle(brush, rect6);
        }
        else if (Value < MinV)
        {
            Rectangle rect8 = new(15 * _width / 100, 15 * _height / 70, 0, 30 * _height / 70);
            Rectangle rect9 = new(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70);
            g.DrawRectangle(rect: new Rectangle(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70), pen: Pens.Black);
            g.FillRectangle(brush, rect8);
            g.FillRectangle(Brushes.White, rect9);
        }
        Pen pen = new(Color.LightGray, 10 / mainmarkcount);
        for (int i = 1; i < mainmarkcount; i++)
        {
            g.DrawLine(pen, (15 + i * (70 / mainmarkcount)) * _width / 100, 15 * _height / 70, (15 + i * (70 / mainmarkcount)) * _width / 100, 45 * _height / 70);
        }
        g.DrawRectangle(rect: new Rectangle(15 * _width / 100, 50 * _height / 70, 70 * _width / 100, 15 * _height / 70), pen: Pens.Black);
        g.DrawString(point: new PointF(37 * _width / 100, 53 * _height / 70), s: Mark.ToString(), font: font, brush: brush2);
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 140;
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
                Value = Convert.ToSingle(obj);
            }
            try
            {
                if (Value >= MinV && Value <= MaxV)
                {
                    initflag = 0;
                    Paint(graphics);
                }
                else if (Value < MinV)
                {
                    initflag = 0;
                    Paint(graphics);
                }
                else if (Value > MaxV)
                {
                    initflag = 0;
                    SolidBrush brush = new(fillcolor);
                    Rectangle rect = new(15 * _width / 100, 15 * _height / 70, 70 * _width / 100, 30 * _height / 70);
                    graphics.FillRectangle(brush, rect);
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
        BSet3 bSet = new()
        {
            VarName = varname,
            MaxValue = MaxV,
            MinValue = MinV,
            MainMarkCount = mainmarkcount,
            Mark = Mark,
            fillcolor = fillcolor,
            bqcolor = bqcolor
        };
        bSet.viewevent += GetTable;
        bSet.ckvarevent += CheckVar;
        if (bSet.ShowDialog() == DialogResult.OK)
        {
            varname = bSet.VarName;
            MaxV = bSet.MaxValue;
            MinV = bSet.MinValue;
            mainmarkcount = bSet.MainMarkCount;
            Mark = bSet.Mark;
            fillcolor = bSet.fillcolor;
            bqcolor = bSet.bqcolor;
            initflag = 1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC8 bSaveC = new()
        {
            MaxV = MaxV,
            MinV = MinV,
            mainmarkcount = mainmarkcount,
            varname = varname,
            Mark = Mark,
            initflag = initflag,
            fontcolor = fontcolor
        };
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public override void Deserialize(byte[] data)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream(data);
        BSaveC8 bSaveC = (BSaveC8)formatter.Deserialize(stream);
        stream.Close();
        MaxV = bSaveC.MaxV;
        MinV = bSaveC.MinV;
        mainmarkcount = bSaveC.mainmarkcount;
        varname = bSaveC.varname;
        Mark = bSaveC.Mark;
        initflag = bSaveC.initflag;
        fontcolor = bSaveC.fontcolor;
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
