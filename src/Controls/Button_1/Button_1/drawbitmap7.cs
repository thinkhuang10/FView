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
using SetForms2;
using ShapeRuntime;

namespace Button_1;

[Serializable]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap7 : CPixieControl
{
    private string varname = "";

    private bool mousedownflag;

    private int pressflag;

    public bool toppstion = true;

    private float changestep = 1f;

    private int initflag = 1;

    private int weith = 200;

    private int hight = 70;

    private float maxval = 100f;

    private float minval;

    [NonSerialized]
    private Point txtpt;

    [NonSerialized]
    private FontFamily ff;

    [NonSerialized]
    private Font ft;

    [Category("杂项")]
    [Description("控件绑定变量。")]
    [ReadOnly(false)]
    [DisplayName("绑定变量")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("变化量")]
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

    [DHMICtrlProperty]
    [DisplayName("可控上限")]
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

    [Description("可以通过按钮使变量变化的下限。")]
    [DisplayName("可控下限")]
    [Category("杂项")]
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

    public drawbitmap7()
    {
    }

    protected drawbitmap7(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap7 info");
        }
        drawbitmap7 obj = new();
        FieldInfo[] fields = typeof(drawbitmap7).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap7))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap7))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap7) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
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
        drawbitmap7 drawbitmap32 = (drawbitmap7)base.Copy();
        drawbitmap32.varname = varname;
        drawbitmap32.changestep = changestep;
        drawbitmap32.maxval = maxval;
        drawbitmap32.minval = minval;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        ff = new FontFamily("宋体");
        ft = new Font(ff, Convert.ToInt32((double)weith * 0.06));
        g.FillRectangle(rect: new Rectangle(0, 0, weith, hight), brush: Brushes.LightGray);
        new Rectangle(Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.2), Convert.ToInt32((double)hight * 0.57));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
        g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
        g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
        g.DrawPolygon(points: new Point[3]
        {
            new(Convert.ToInt32((double)weith * 0.15), Convert.ToInt32((double)hight * 0.3)),
            new(Convert.ToInt32((double)weith * 0.08), Convert.ToInt32((double)hight * 0.45)),
            new(Convert.ToInt32((double)weith * 0.22), Convert.ToInt32((double)hight * 0.45))
        }, pen: Pens.White);
        g.DrawPolygon(points: new Point[3]
        {
            new(Convert.ToInt32((double)weith * 0.15), Convert.ToInt32((double)hight * 0.67)),
            new(Convert.ToInt32((double)weith * 0.08), Convert.ToInt32((double)hight * 0.52)),
            new(Convert.ToInt32((double)weith * 0.22), Convert.ToInt32((double)hight * 0.52))
        }, pen: Pens.White);
        g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)weith * 0.3), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.65), Convert.ToInt32((double)hight * 0.57)), brush: Brushes.Black);
        txtpt = new Point(Convert.ToInt32((double)weith * 0.33), Convert.ToInt32((double)hight * 0.35));
        g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
        g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
        g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.49), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.49));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.48), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.48));
        if (mousedownflag && pressflag == 1)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.48), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
            g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.48), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.48));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.2));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.2), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.48));
            pressflag = 0;
        }
        else if (mousedownflag && pressflag == 2)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.49));
            g.DrawLine(Pens.White, Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.77), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.49), Convert.ToInt32((double)weith * 0.25), Convert.ToInt32((double)hight * 0.49));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.49), Convert.ToInt32((double)weith * 0.05), Convert.ToInt32((double)hight * 0.77));
            pressflag = 0;
        }
        if (initflag == 1)
        {
            string s = "#####";
            g.DrawString(s, ft, Brushes.White, txtpt);
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
            Click?.Invoke(this, null);
            mousedownflag = true;
            if (string.IsNullOrEmpty(varname))
            {
                return;
            }
            if ((float)e.Y < Math.Abs(Height) * 0.5f && (float)e.Y > Math.Abs(Height) * 0.2f && (float)e.X > Math.Abs(Width) * 0.05f && (float)e.X < Math.Abs(Width) * 0.25f)
            {
                toppstion = true;
                pressflag = 1;
                NeedRefresh = true;
                if (Convert.ToSingle(GetValue(varname)) >= minval && Convert.ToSingle(GetValue(varname)) <= maxval && Convert.ToSingle(GetValue(varname)) + changestep >= minval && Convert.ToSingle(GetValue(varname)) + changestep <= maxval)
                {
                    SetValue(varname, Math.Round(Convert.ToSingle(GetValue(varname)) + changestep, 5, MidpointRounding.AwayFromZero));
                }
            }
            else if ((float)e.Y > Math.Abs(Height) * 0.5f && (float)e.Y < Math.Abs(Height) * 0.77f && (float)e.X > Math.Abs(Width) * 0.05f && (float)e.X < Math.Abs(Width) * 0.25f)
            {
                toppstion = false;
                pressflag = 2;
                NeedRefresh = true;
                if (Convert.ToSingle(GetValue(varname)) >= minval && Convert.ToSingle(GetValue(varname)) <= maxval && Convert.ToSingle(GetValue(varname)) - changestep >= minval && Convert.ToSingle(GetValue(varname)) - changestep <= maxval)
                {
                    SetValue(varname, Math.Round(Convert.ToSingle(GetValue(varname)) - changestep, 5, MidpointRounding.AwayFromZero));
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
            if (obj == null)
            {
                Paint(graphics);
            }
            else
            {
                initflag = 0;
                try
                {
                    Paint(graphics);
                    graphics.DrawString(Convert.ToSingle(obj).ToString(), ft, Brushes.White, txtpt);
                }
                catch
                {
                }
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
        BtnSet btnSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            btnSet.varname = varname;
            btnSet.changestep = changestep;
            btnSet.maxval = maxval;
            btnSet.minval = minval;
        }
        btnSet.viewevent += GetTable;
        btnSet.ckvarevent += CheckVar;
        if (btnSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + btnSet.varname + "]";
            changestep = btnSet.changestep;
            maxval = btnSet.maxval;
            minval = btnSet.minval;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC7 bSaveC = new()
        {
            varname = varname,
            changestep = changestep
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
            BSaveC7 bSaveC = (BSaveC7)formatter.Deserialize(stream);
            stream.Close();
            varname = bSaveC.varname;
            changestep = bSaveC.changestep;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
