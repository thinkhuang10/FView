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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap2 : CPixieControl
{
    private bool mousedownflag;

    private bool toppstion = true;

    private string varname = "";

    private Color CtrlColor = Color.Blue;

    private float changestep = 1f;

    public int initflag = 1;

    private int weith = 100;

    private int hight = 200;

    private float maxval = 100f;

    private float minval;

    [NonSerialized]
    private readonly Timer tm1 = new();

    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
    [Description("控件绑定变量。")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Description("按钮颜色。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Category("杂项")]
    [DisplayName("按钮颜色")]
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

    [DHMICtrlProperty]
    [DisplayName("变化量")]
    [Category("杂项")]
    [Description("点击按钮时绑定变量单次增减量。")]
    [ReadOnly(false)]
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

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("可控下限")]
    [Category("杂项")]
    [Description("可以通过按钮使变量变化的下限。")]
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

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap32 = (drawbitmap2)base.Copy();
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
        Pen pen2 = new(Color.Black, 1f);
        Pen pen3 = new(CtrlColor, Convert.ToInt32((double)weith * 0.1));
        Rectangle rect = new(0, 0, weith, hight / 2);
        g.FillRectangle(Brushes.Gray, rect);
        Rectangle rect2 = new(0, hight / 2, weith, hight / 2);
        g.FillRectangle(Brushes.Gray, rect2);
        g.DrawLine(pen3, Convert.ToInt32((double)weith * 0.15), Convert.ToInt32((double)hight * 0.25), Convert.ToInt32((double)weith * 0.85), Convert.ToInt32((double)hight * 0.25));
        g.DrawLine(pen3, Convert.ToInt32((double)weith * 0.15), Convert.ToInt32((double)hight * 0.75), Convert.ToInt32((double)weith * 0.85), Convert.ToInt32((double)hight * 0.75));
        g.DrawLine(pen3, Convert.ToInt32((double)weith * 0.5), Convert.ToInt32((double)hight * 0.075), Convert.ToInt32((double)weith * 0.5), Convert.ToInt32((double)hight * 0.425));
        g.DrawRectangle(Pens.LightGray, rect);
        g.DrawRectangle(Pens.LightGray, rect2);
        g.DrawLine(pen, 1, 1, weith, 1);
        g.DrawLine(pen, 1, 1, 1, hight);
        g.DrawLine(pen2, weith, hight, weith, 1);
        g.DrawLine(pen2, weith, hight, 1, hight);
        g.DrawLine(pen2, 1, hight / 2, weith, hight / 2);
        g.DrawLine(pen, 1, hight / 2 + 2, weith, hight / 2 + 2);
        if (mousedownflag && toppstion)
        {
            g.DrawLine(pen, 1, hight / 2, weith, hight / 2);
            g.DrawLine(pen, weith - 1, 1, weith - 1, hight / 2);
            g.DrawLine(pen2, 1, 1, 1, hight / 2);
            g.DrawLine(pen2, 1, 1, weith, 1);
        }
        else if (mousedownflag && !toppstion)
        {
            g.DrawLine(pen, 1, hight - 1, weith, hight - 1);
            g.DrawLine(pen, weith - 1, hight / 2, weith - 1, hight);
            g.DrawLine(pen2, 1, hight / 2, 1, hight);
            g.DrawLine(pen2, 1, hight / 2 + 2, weith, hight / 2 + 2);
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
            if ((float)e.Y < Math.Abs(Height) * 0.5f)
            {
                toppstion = true;
            }
            else
            {
                toppstion = false;
            }
            if (!string.IsNullOrEmpty(varname))
            {
                if ((float)e.Y < Math.Abs(Height) * 0.5f)
                {
                    toppstion = true;
                    object value = GetValue(varname);
                    float num = Convert.ToSingle(value);
                    if (num >= minval && num <= maxval && num + changestep >= minval && num + changestep <= maxval)
                    {
                        SetValue(varname, Math.Round(num + changestep, 5, MidpointRounding.AwayFromZero));
                    }
                }
                else
                {
                    toppstion = false;
                    object value2 = GetValue(varname);
                    float num2 = Convert.ToSingle(value2);
                    if (num2 >= minval && num2 <= maxval && num2 - changestep >= minval && num2 - changestep <= maxval)
                    {
                        SetValue(varname, Math.Round(num2 - changestep, 5, MidpointRounding.AwayFromZero));
                    }
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
                hight = 400;
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
            minval = btnSet.minval;
            maxval = btnSet.maxval;
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
        catch
        {
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
