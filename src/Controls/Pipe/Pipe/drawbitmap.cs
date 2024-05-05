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

namespace Pipe;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap : CPixieControl
{
    private string varname1 = "";

    private string varname2 = "";

    private Color PipeColor = Color.LightGray;

    private Color LColor = Color.Blue;

    private int SPDFlag = 1;

    private int TypeFlag = 1;

    private float divwth = 10f;

    private float LHigh = 10f;

    private float LWth = 30f;

    private int initflag = 1;

    private float steplth = 4f;

    private float steplthbuffer = 4f;

    private int addnum;

    private int retimer;

    private int stepdivcount = 15;

    private int _width = 200;

    private int _height = 40;

    [NonSerialized]
    private Timer tm1;

    private bool value1;

    private bool value2;

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("流向变量")]
    [Category("杂项")]
    [Description("用于控制管道内水流的流向。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string VarDirection
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

    [DHMICtrlProperty]
    [DisplayName("流动使能变量")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("用于控制管道内水流是否流动的变量。")]
    [ReadOnly(false)]
    public string VarEnabled
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

    [Description("管道颜色。")]
    [DisplayName("管道颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public Color Pipecolor
    {
        get
        {
            return PipeColor;
        }
        set
        {
            PipeColor = value;
        }
    }

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("流动块的颜色。")]
    [DisplayName("流动块颜色")]
    [ReadOnly(false)]
    public Color StockColor
    {
        get
        {
            return LColor;
        }
        set
        {
            LColor = value;
        }
    }

    [DisplayName("流动速度")]
    [Category("杂项")]
    [Description("可以设置1到5之间的整数，1为最慢，5为最快。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public int SpeedLevel
    {
        get
        {
            return SPDFlag;
        }
        set
        {
            SPDFlag = value;
            switch (SPDFlag)
            {
                case 1:
                    steplth = (SpaceWidth + StockWidth) / 15f;
                    stepdivcount = 15;
                    break;
                case 2:
                    steplth = (SpaceWidth + StockWidth) / 12f;
                    stepdivcount = 12;
                    break;
                case 3:
                    steplth = (SpaceWidth + StockWidth) / 9f;
                    stepdivcount = 9;
                    break;
                case 4:
                    steplth = (SpaceWidth + StockWidth) / 6f;
                    stepdivcount = 6;
                    break;
                case 5:
                    steplth = (SpaceWidth + StockWidth) / 4f;
                    stepdivcount = 3;
                    break;
            }
            steplthbuffer = steplth;
        }
    }

    [Description("流动块之间的间距。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("流动块间距")]
    public float SpaceWidth
    {
        get
        {
            return divwth;
        }
        set
        {
            divwth = value;
            switch (SPDFlag)
            {
                case 1:
                    steplth = (divwth + StockWidth) / 15f;
                    stepdivcount = 15;
                    break;
                case 2:
                    steplth = (divwth + StockWidth) / 12f;
                    stepdivcount = 12;
                    break;
                case 3:
                    steplth = (divwth + StockWidth) / 9f;
                    stepdivcount = 9;
                    break;
                case 4:
                    steplth = (divwth + StockWidth) / 6f;
                    stepdivcount = 6;
                    break;
                case 5:
                    steplth = (divwth + StockWidth) / 4f;
                    stepdivcount = 3;
                    break;
            }
            steplthbuffer = steplth;
        }
    }

    [DisplayName("流动块高度")]
    [DHMICtrlProperty]
    [Description("流动块的高度。")]
    [Category("杂项")]
    [ReadOnly(false)]
    public float StockHight
    {
        get
        {
            return LHigh;
        }
        set
        {
            LHigh = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("流动块宽度")]
    [ReadOnly(false)]
    [Description("流动块的宽度。")]
    public float StockWidth
    {
        get
        {
            return LWth;
        }
        set
        {
            LWth = value;
            switch (SPDFlag)
            {
                case 1:
                    steplth = (SpaceWidth + LWth) / 15f;
                    stepdivcount = 15;
                    break;
                case 2:
                    steplth = (SpaceWidth + LWth) / 12f;
                    stepdivcount = 12;
                    break;
                case 3:
                    steplth = (SpaceWidth + LWth) / 9f;
                    stepdivcount = 9;
                    break;
                case 4:
                    steplth = (SpaceWidth + LWth) / 6f;
                    stepdivcount = 6;
                    break;
                case 5:
                    steplth = (SpaceWidth + LWth) / 4f;
                    stepdivcount = 3;
                    break;
            }
            steplthbuffer = steplth;
        }
    }

    [DisplayName("流向变量当前值")]
    [ReadOnly(false)]
    [Description("流向变量当前值。")]
    [Category("杂项")]
    public bool Value1
    {
        get
        {
            return value1;
        }
        set
        {
            if (value1 != value)
            {
                FlowDirectionChanged?.Invoke(this, null);
                value1 = value;
            }
        }
    }

    [DisplayName("流动使能变量当前值")]
    [Category("杂项")]
    [Description("流动使能变量当前值。")]
    [ReadOnly(false)]
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
                IsFlowChanged?.Invoke(this, null);
                value2 = value;
            }
        }
    }

    public event EventHandler FlowDirectionChanged;

    public event EventHandler IsFlowChanged;

    protected drawbitmap(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap info");
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

    public drawbitmap()
    {
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap9 = (drawbitmap)base.Copy();
        drawbitmap9.varname1 = varname1;
        drawbitmap9.varname2 = varname2;
        drawbitmap9.PipeColor = PipeColor;
        drawbitmap9.LColor = LColor;
        drawbitmap9.SPDFlag = SPDFlag;
        drawbitmap9.TypeFlag = (TypeFlag = 1);
        drawbitmap9.divwth = divwth;
        drawbitmap9.LHigh = LHigh;
        drawbitmap9.LWth = LWth;
        return drawbitmap9;
    }

    public void Paint(Graphics g)
    {
        if (TypeFlag == 1)
        {
            Rectangle rect = new(0, 0, 200 * _width / 200, 40 * _height / 40);
            Rectangle rect2 = new(0, 0, 200 * _width / 200, 20 * _height / 40);
            Rectangle rect3 = new(0, 20 * _height / 40, 200 * _width / 200, 20 * _height / 40);
            LinearGradientBrush brush = new(rect, PipeColor, Color.White, 90f);
            LinearGradientBrush brush2 = new(rect, Color.White, PipeColor, 90f);
            SolidBrush brush3 = new(LColor);
            g.FillRectangle(brush, rect2);
            g.FillRectangle(brush2, rect3);
            if (initflag == 1)
            {
                for (int i = 0; i <= Convert.ToInt32(200f / (divwth + LWth)); i++)
                {
                    Rectangle rect4 = new(Convert.ToInt32((float)i * (divwth + LWth)) * _width / 200, Convert.ToInt32(20f - LHigh / 2f) * _height / 40, Convert.ToInt32(LWth) * _width / 200, Convert.ToInt32(LHigh) * _height / 40);
                    g.FillRectangle(brush3, rect4);
                }
            }
        }
        else
        {
            if (TypeFlag != 2)
            {
                return;
            }
            Rectangle rect5 = new(0, 0, 200 * _width / 200, 40);
            Rectangle rect6 = new(0, 0, 100 * _width / 200, 40);
            Rectangle rect7 = new(0, 20, 100 * _width / 200, 40);
            LinearGradientBrush brush4 = new(rect5, PipeColor, Color.White, LinearGradientMode.Horizontal);
            LinearGradientBrush brush5 = new(rect5, Color.White, PipeColor, LinearGradientMode.Horizontal);
            SolidBrush brush6 = new(LColor);
            g.FillRectangle(brush4, rect6);
            g.FillRectangle(brush5, rect7);
            if (initflag == 1)
            {
                for (int j = 0; j <= Convert.ToInt32(200f / (divwth + LHigh)); j++)
                {
                    Rectangle rect8 = new(Convert.ToInt32(100f - LWth / 2f) * _width / 200, Convert.ToInt32((float)j * (divwth + LHigh)) * _height / 40, Convert.ToInt32(LWth) * _width / 200, Convert.ToInt32(LHigh) * _height / 40);
                    g.FillRectangle(brush6, rect8);
                }
            }
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname1) ? null : GetValue(varname1));
            object obj2 = (string.IsNullOrEmpty(varname2) ? null : GetValue(varname2));
            if (Height == 0f || Width == 0f)
            {
                _height = 40;
                _width = 200;
            }
            else
            {
                _height = Convert.ToInt32(Math.Abs(Height));
                _width = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(_width, _height);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new(LColor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (obj != null)
            {
                Value1 = Convert.ToBoolean(obj);
            }
            if (obj2 != null)
            {
                Value2 = Convert.ToBoolean(obj2);
            }
            initflag = 0;
            try
            {
                if (Value2)
                {
                    steplth = steplthbuffer;
                }
                else
                {
                    steplth = 0f;
                }
                if (TypeFlag == 1)
                {
                    Paint(graphics);
                    if (Value1)
                    {
                        for (int i = -1; i <= Convert.ToInt32(200f / (divwth + LWth)); i++)
                        {
                            retimer = addnum % (stepdivcount - 1);
                            Rectangle rect = new(Convert.ToInt32((float)i * (divwth + LWth) + (float)retimer * steplth) * _width / 200, Convert.ToInt32(20f - LHigh / 2f) * _height / 40, Convert.ToInt32(LWth) * _width / 200, Convert.ToInt32(LHigh) * _height / 40);
                            graphics.FillRectangle(brush, rect);
                        }
                        addnum++;
                    }
                    else
                    {
                        for (int j = 0; j <= Convert.ToInt32(200f / (divwth + LWth)) + 1; j++)
                        {
                            retimer = addnum % (stepdivcount - 1);
                            Rectangle rect2 = new(Convert.ToInt32((float)j * (divwth + LWth) - (float)retimer * steplth) * _width / 200, Convert.ToInt32(20f - LHigh / 2f) * _height / 40, Convert.ToInt32(LWth) * _width / 200, Convert.ToInt32(LHigh) * _height / 40);
                            graphics.FillRectangle(brush, rect2);
                        }
                        addnum++;
                    }
                }
                else
                {
                    Paint(graphics);
                    for (int k = 0; k <= Convert.ToInt32(200f / (divwth + LHigh)); k++)
                    {
                        retimer = addnum % (stepdivcount - 1);
                        Rectangle rect3 = new(Convert.ToInt32(100f - LWth / 2f), Convert.ToInt32((float)k * (divwth + LHigh) + (float)retimer * steplth), Convert.ToInt32(LWth), Convert.ToInt32(LHigh));
                        graphics.FillRectangle(brush, rect3);
                    }
                    addnum++;
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

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override void ShowDialog()
    {
        PSet1 pSet = new();
        if (varname1 != "")
        {
            pSet.varname1 = varname1;
            pSet.varname2 = varname2;
            pSet.TypeFlag = TypeFlag;
            pSet.SPDFlag = SPDFlag;
            pSet.LHigh = LHigh;
            pSet.LWth = LWth;
            pSet.PipeColor = PipeColor;
            pSet.LColor = LColor;
            pSet.divwth = divwth;
            pSet.TypeFlag = TypeFlag;
            pSet.SPDFlag = SPDFlag;
        }
        pSet.viewevent += GetTable;
        pSet.ckvarevent += CheckVar;
        if (pSet.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        varname1 = pSet.varname1;
        varname2 = pSet.varname2;
        TypeFlag = pSet.TypeFlag;
        SPDFlag = pSet.SPDFlag;
        LHigh = pSet.LHigh;
        LWth = pSet.LWth;
        PipeColor = pSet.PipeColor;
        LColor = pSet.LColor;
        divwth = pSet.divwth;
        SPDFlag = pSet.SPDFlag;
        initflag = 1;
        if (pSet.TypeFlag == 1)
        {
            switch (SPDFlag)
            {
                case 1:
                    steplth = (pSet.divwth + pSet.LWth) / 15f;
                    stepdivcount = 15;
                    break;
                case 2:
                    steplth = (pSet.divwth + pSet.LWth) / 12f;
                    stepdivcount = 12;
                    break;
                case 3:
                    steplth = (pSet.divwth + pSet.LWth) / 9f;
                    stepdivcount = 9;
                    break;
                case 4:
                    steplth = (pSet.divwth + pSet.LWth) / 6f;
                    stepdivcount = 6;
                    break;
                case 5:
                    steplth = (pSet.divwth + pSet.LWth) / 4f;
                    stepdivcount = 3;
                    break;
            }
        }
        steplthbuffer = steplth;
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
        BSaveP1 bSaveP = new()
        {
            varname1 = varname1,
            varname2 = varname2,
            PipeColor = PipeColor,
            LColor = LColor,
            SPDFlag = SPDFlag,
            TypeFlag = TypeFlag,
            divwth = divwth,
            LHigh = LHigh,
            LWth = LWth
        };
        formatter.Serialize(memoryStream, bSaveP);
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
            BSaveP1 bSaveP = (BSaveP1)formatter.Deserialize(stream);
            stream.Close();
            varname1 = bSaveP.varname1;
            varname2 = bSaveP.varname2;
            PipeColor = bSaveP.PipeColor;
            LColor = bSaveP.LColor;
            SPDFlag = bSaveP.SPDFlag;
            TypeFlag = bSaveP.TypeFlag;
            divwth = bSaveP.divwth;
            LHigh = bSaveP.LHigh;
            LWth = bSaveP.LWth;
            tm1 = new Timer();
            tm1.Tick += tm1_Tick;
            tm1.Interval = 180;
            tm1.Enabled = true;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
