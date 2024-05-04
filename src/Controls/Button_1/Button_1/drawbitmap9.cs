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
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap9 : CPixieControl
{
    private string varname = "";

    private bool mousedownflag;

    private bool toppstion = true;

    private Color CtrlColor = Color.Blue;

    private float changestep = 1f;

    public int initflag = 1;

    private int weith = 200;

    private int hight = 200;

    private float maxval = 100f;

    private float minval;

    [NonSerialized]
    private readonly Timer tm1 = new();

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量。")]
    [DisplayName("绑定变量")]
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
    [DisplayName("按钮颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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
    [Description("点击按钮时绑定变量单次增减量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DisplayName("可控上限")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("可以通过按钮使变量变化的上限。")]
    [ReadOnly(false)]
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
    [Category("杂项")]
    [Description("可以通过按钮使变量变化的下限。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    public drawbitmap9()
    {
    }

    protected drawbitmap9(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap9 obj = new();
        FieldInfo[] fields = typeof(drawbitmap9).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap9))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap9), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap9))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap9), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap9) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap9 drawbitmap32 = (drawbitmap9)base.Copy();
        drawbitmap32.varname = varname;
        drawbitmap32.CtrlColor = CtrlColor;
        drawbitmap32.changestep = changestep;
        drawbitmap32.maxval = maxval;
        drawbitmap32.minval = minval;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(CtrlColor);
        Rectangle rect = new(0, 0, 200 * weith / 200, 200 * hight / 200);
        new Rectangle(10 * weith / 200, 10 * hight / 200, 180 * weith / 200, 180 * hight / 200);
        new Rectangle(15 * weith / 200, 15 * hight / 200, 170 * weith / 200, 80 * hight / 200);
        new Rectangle(15 * weith / 200, 105 * hight / 200, 170 * weith / 200, 80 * hight / 200);
        g.FillRectangle(Brushes.LightGray, rect);
        g.DrawLine(Pens.DarkGray, 10 * weith / 200, 10 * hight / 200, 190 * weith / 200, 10 * hight / 200);
        g.DrawLine(Pens.DarkGray, 10 * weith / 200, 10 * hight / 200, 10 * weith / 200, 190 * hight / 200);
        g.DrawLine(Pens.White, 190 * weith / 200, 190 * hight / 200, 190 * weith / 200, 10 * hight / 200);
        g.DrawLine(Pens.White, 190 * weith / 200, 190 * hight / 200, 10 * weith / 200, 190 * hight / 200);
        Point[] points = new Point[3]
        {
            new(100 * weith / 200, 25 * hight / 200),
            new(25 * weith / 200, 85 * hight / 200),
            new(175 * weith / 200, 85 * hight / 200)
        };
        Point[] points2 = new Point[3]
        {
            new(100 * weith / 200, 175 * hight / 200),
            new(25 * weith / 200, 115 * hight / 200),
            new(175 * weith / 200, 115 * hight / 200)
        };
        g.FillPolygon(brush, points);
        g.FillPolygon(brush, points2);
        g.DrawLine(Pens.White, 10 * weith / 200, 10 * hight / 200, 190 * weith / 200, 10 * hight / 200);
        g.DrawLine(Pens.White, 10 * weith / 200, 10 * hight / 200, 10 * weith / 200, 190 * hight / 200);
        g.DrawLine(Pens.DarkGray, 190 * weith / 200, 190 * hight / 200, 190 * weith / 200, 10 * hight / 200);
        g.DrawLine(Pens.DarkGray, 190 * weith / 200, 190 * hight / 200, 10 * weith / 200, 190 * hight / 200);
        g.DrawLine(Pens.White, 190 * weith / 200, 102 * hight / 200, 10 * weith / 200, 102 * hight / 200);
        g.DrawLine(Pens.DarkGray, 190 * weith / 200, 100 * hight / 200, 10 * weith / 200, 100 * hight / 200);
        if (mousedownflag && toppstion)
        {
            g.DrawLine(Pens.White, 190 * weith / 200, 100 * hight / 200, 10 * weith / 200, 100 * hight / 200);
            g.DrawLine(Pens.White, 190 * weith / 200, 100 * hight / 200, 190 * weith / 200, 10 * hight / 200);
            g.DrawLine(Pens.DarkGray, 10 * weith / 200, 10 * hight / 200, 190 * weith / 200, 10 * hight / 200);
            g.DrawLine(Pens.DarkGray, 10 * weith / 200, 10 * hight / 200, 10 * weith / 200, 100 * hight / 200);
        }
        else if (mousedownflag && !toppstion)
        {
            g.DrawLine(Pens.White, 190 * weith / 200, 190 * hight / 200, 190 * weith / 200, 102 * hight / 200);
            g.DrawLine(Pens.White, 190 * weith / 200, 190 * hight / 200, 10 * weith / 200, 190 * hight / 200);
            g.DrawLine(Pens.DarkGray, 10 * weith / 200, 102 * hight / 200, 10 * weith / 200, 190 * hight / 200);
            g.DrawLine(Pens.DarkGray, 10 * weith / 200, 102 * hight / 200, 190 * weith / 200, 102 * hight / 200);
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
            if (string.IsNullOrEmpty(varname))
            {
                return;
            }
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
