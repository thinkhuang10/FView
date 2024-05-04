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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap31 : CPixieControl
{
    private string varname = "";

    private bool mousedownflag;

    private Color CtrlColor = Color.Blue;

    private float changestep = 1f;

    public int initflag = 1;

    private int weith = 100;

    private int hight = 100;

    private float maxval = 100f;

    private float minval;

    [NonSerialized]
    private readonly Timer tm1 = new();

    [DHMICtrlProperty]
    [Description("控件绑定变量。")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
    [Category("杂项")]
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

    [DisplayName("按钮颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("按钮颜色。")]
    [ReadOnly(false)]
    public Color ControlColor
    {
        get
        {
            return CtrlColor;
        }
        set
        {
            CtrlColor = value;
        }
    }

    [DisplayName("变化量")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("点击按钮时绑定变量单次增减量。")]
    public float ChangeStepValue
    {
        get
        {
            return changestep;
        }
        set
        {
            changestep = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("可控上限")]
    [Category("杂项")]
    [Description("可以通过按钮使变量变化的上限。")]
    public float Maxvalue
    {
        get
        {
            return maxval;
        }
        set
        {
            maxval = value;
        }
    }

    [DisplayName("可控下限")]
    [Description("可以通过按钮使变量变化的下限。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public float Minvalue
    {
        get
        {
            return minval;
        }
        set
        {
            minval = value;
        }
    }

    public new event EventHandler Click;

    public drawbitmap31()
    {
    }

    protected drawbitmap31(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap31 obj = new();
        FieldInfo[] fields = typeof(drawbitmap31).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap31))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap31), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap31))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap31), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap31) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap31 drawbitmap32 = (drawbitmap31)base.Copy();
        drawbitmap32.varname = varname;
        drawbitmap32.CtrlColor = CtrlColor;
        drawbitmap32.changestep = changestep;
        drawbitmap32.maxval = maxval;
        drawbitmap32.minval = minval;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.White, 1f);
        Pen pen2 = new(Color.DarkGray, 1f);
        SolidBrush brush = new(CtrlColor);
        g.FillRectangle(rect: new Rectangle(0, 0, weith, hight), brush: Brushes.Gray);
        g.FillPolygon(brush, new PointF[3]
        {
            new Point(Convert.ToInt32((double)weith * 0.1), Convert.ToInt32(hight / 10)),
            new Point(Convert.ToInt32((double)weith * 0.9), Convert.ToInt32(hight / 10)),
            new Point(Convert.ToInt32(weith / 2), Convert.ToInt32((double)hight * 0.9))
        });
        g.DrawLine(pen, 1, 1, 1, hight);
        g.DrawLine(pen, 1, 1, weith, 1);
        g.DrawLine(pen2, weith, hight, weith, 1);
        g.DrawLine(pen2, weith, hight, 1, hight);
        if (mousedownflag)
        {
            g.DrawLine(pen2, 1, 1, 1, hight);
            g.DrawLine(pen2, 1, 1, weith, 1);
            g.DrawLine(pen, weith - 1, hight, weith - 1, 1);
            g.DrawLine(pen, weith, hight - 1, 1, hight - 1);
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
            if (!string.IsNullOrEmpty(varname))
            {
                object value = GetValue(varname);
                float num = Convert.ToSingle(value);
                if (num >= minval && num <= maxval && num + changestep >= minval && num + changestep <= maxval)
                {
                    SetValue(varname, Math.Round(num + changestep, 5, MidpointRounding.AwayFromZero));
                }
            }
            NeedRefresh = true;
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
                hight = 200;
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
            FinishRefresh(bitmap);
        }
        catch
        {
        }
    }

    public override void ShowDialog()
    {
        BtnSet btnSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            btnSet.Varname = varname;
            btnSet.CtrlColor = CtrlColor;
            btnSet.ChangeStep = changestep;
            btnSet.maxval = maxval;
            btnSet.minval = minval;
        }
        btnSet.CtrlColor = CtrlColor;
        btnSet.viewevent += GetTable;
        btnSet.ckvarevent += CheckVar;
        if (btnSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + btnSet.Varname + "]";
            CtrlColor = btnSet.CtrlColor;
            changestep = btnSet.ChangeStep;
            maxval = btnSet.maxval;
            minval = btnSet.minval;
            NeedRefresh = true;
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            varname = varname,
            changestep = changestep,
            ctrlcolor = CtrlColor
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
            BSave2 bSave = (BSave2)formatter.Deserialize(stream);
            stream.Close();
            varname = bSave.varname;
            changestep = bSave.changestep;
            CtrlColor = bSave.ctrlcolor;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
