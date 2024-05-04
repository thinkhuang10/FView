using System;
using System.Collections.Generic;
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

namespace YouBiao;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap2 : CPixieControl
{
    private bool highalertflag;

    private bool lowalertflag;

    private string varname = "";

    private float maxval = 100f;

    private float minval;

    private int mainmark = 5;

    private int othermark = 2;

    private int ptcount = 2;

    private int _width = 200;

    private int _height = 100;

    private int initflag = 1;

    private int dragzonexmin;

    private int dragzonexmax;

    private int dragzoneymin;

    private int dragzoneymax;

    private bool dragableflag;

    private Color color1 = Color.Black;

    private Color colorvaluert = Color.Green;

    [NonSerialized]
    private float value;

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
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

    [DHMICtrlProperty]
    [DisplayName("量程上限")]
    [Category("杂项")]
    [Description("YouBiao量程上限。")]
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

    [Category("杂项")]
    [DisplayName("量程下限")]
    [DHMICtrlProperty]
    [Description("YouBiao量程下限。")]
    [ReadOnly(false)]
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

    [DisplayName("主刻度数")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("表盘主刻度线数量。")]
    [ReadOnly(false)]
    public int MainMarkCount
    {
        get
        {
            return mainmark;
        }
        set
        {
            mainmark = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("副刻度数")]
    [Description("表盘副刻度线数量。")]
    [Category("杂项")]
    public int OtherMarkCount
    {
        get
        {
            return othermark;
        }
        set
        {
            othermark = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("小数位")]
    [Description("表盘显示数值小数精确位数。")]
    [ReadOnly(false)]
    [Category("杂项")]
    public int DecimalPlacesCount
    {
        get
        {
            return ptcount;
        }
        set
        {
            ptcount = value;
        }
    }

    [DHMICtrlProperty]
    [DisplayName("刻度盘背景色")]
    [Category("杂项")]
    [Description("刻度盘背景颜色。")]
    [ReadOnly(false)]
    public Color BackColor
    {
        get
        {
            return color1;
        }
        set
        {
            color1 = value;
        }
    }

    [DisplayName("液柱填充色")]
    [Category("杂项")]
    [Description("YouBiao滑动时YouBiao下方液柱的填充色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public Color ColorFill
    {
        get
        {
            return colorvaluert;
        }
        set
        {
            colorvaluert = value;
        }
    }

    [ReadOnly(false)]
    [Description("YouBiao当前显示数值。")]
    [DisplayName("YouBiao当前值")]
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

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public drawbitmap2()
    {
    }

    protected drawbitmap2(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap2 info");
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

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap6 = (drawbitmap2)base.Copy();
        drawbitmap6.varname = varname;
        drawbitmap6.maxval = maxval;
        drawbitmap6.minval = minval;
        drawbitmap6.mainmark = mainmark;
        drawbitmap6.othermark = othermark;
        drawbitmap6.ptcount = ptcount;
        drawbitmap6.color1 = color1;
        drawbitmap6.colorvaluert = colorvaluert;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)_width * 0.04));
        g.FillRectangle(rect: new Rectangle(0, 0, _width, _height), brush: Brushes.LightGray);
        g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.1), Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.18)), brush: Brushes.Black);
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.35), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.15));
        SolidBrush brush = new(color1);
        g.FillRectangle(brush, rect3);
        Pen pen = new(Color.Black, Convert.ToInt32((double)_height * 0.06));
        g.DrawLine(pen, Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.9), Convert.ToInt32((double)_height * 0.9));
        float num = Convert.ToSingle((double)_width * 0.8 / (double)mainmark);
        float num2 = Convert.ToSingle((double)_width * 0.8 / (double)(mainmark * (othermark + 1)));
        for (int i = 0; i <= mainmark; i++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.1 + (double)((float)i * num)), Convert.ToSingle((double)_height * 0.65), Convert.ToSingle((double)_width * 0.1 + (double)((float)i * num)), Convert.ToSingle((double)_height * 0.85));
        }
        for (int j = 0; j <= mainmark * (othermark + 1); j++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.1 + (double)((float)j * num2)), Convert.ToSingle((double)_height * 0.75), Convert.ToSingle((double)_width * 0.1 + (double)((float)j * num2)), Convert.ToSingle((double)_height * 0.85));
        }
        List<string> list = new();
        List<Point> list2 = new();
        for (int k = 0; k <= mainmark; k++)
        {
            list.Add(Math.Round(minval + (float)k * ((maxval - minval) / (float)mainmark), ptcount).ToString());
        }
        for (int l = 0; l <= mainmark; l++)
        {
            list2.Add(new Point(Convert.ToInt32((double)_width * 0.1 + (double)((float)l * num) - (double)_width * 0.02), Convert.ToInt32((double)_height * 0.53)));
        }
        for (int m = 0; m <= mainmark; m++)
        {
            g.DrawString(list[m], font, Brushes.Black, list2[m]);
        }
        if (initflag == 1)
        {
            g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.12)), brush: Brushes.LightGray);
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.97));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.85));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.97), Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.97));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.97), Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.85));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.9));
            dragzonexmin = Convert.ToInt32((double)_width * 0.05);
            dragzonexmax = Convert.ToInt32((double)_width * 0.15);
            dragzoneymin = Convert.ToInt32((double)_height * 0.85);
            dragzoneymax = Convert.ToInt32((double)_height * 0.97);
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        if (e.X > dragzonexmin && e.X < dragzonexmax && e.Y > dragzoneymin && e.Y < dragzoneymax)
        {
            dragableflag = true;
        }
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        dragableflag = false;
    }

    public override void ManageMouseMove(MouseEventArgs e)
    {
        if (!dragableflag)
        {
            return;
        }
        float num = Convert.ToSingle(((double)e.X - (double)_width * 0.1) * (double)(maxval - minval) / ((double)_width * 0.8) + (double)minval);
        if (varname != "")
        {
            if (num > maxval)
            {
                num = maxval;
            }
            if (num < minval)
            {
                num = minval;
            }
            SetValue(varname, num);
        }
        else
        {
            Value = num;
        }
        NeedRefresh = true;
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 100;
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
                float num;
                if (Value > maxval)
                {
                    if (!highalertflag && this.HighAlert != null)
                    {
                        this.HighAlert(this, null);
                        highalertflag = true;
                    }
                    num = maxval;
                }
                else if (Value < minval)
                {
                    if (!lowalertflag && this.LowAlert != null)
                    {
                        this.LowAlert(this, null);
                        lowalertflag = true;
                    }
                    num = minval;
                }
                else
                {
                    highalertflag = false;
                    lowalertflag = false;
                    num = Value;
                }
                initflag = 0;
                Paint(graphics);
                graphics.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.12)), brush: Brushes.LightGray);
                graphics.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.97));
                graphics.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.97), Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.97));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.97), Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.1 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.85), Convert.ToInt32((double)_width * 0.1 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.9));
                FontFamily family = new("宋体");
                Font font = new(family, Convert.ToInt32((double)_width * 0.04));
                graphics.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.16)), s: ((float)Math.Round(Convert.ToSingle(obj), ptcount)).ToString(), font: font, brush: Brushes.White);
                Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.35), Convert.ToInt32((double)((num - minval) / (maxval - minval) * (float)_width) * 0.8), Convert.ToInt32((double)_height * 0.15));
                SolidBrush brush = new(colorvaluert);
                graphics.FillRectangle(brush, rect2);
                dragzonexmin = Convert.ToInt32((double)_width * 0.05 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval));
                dragzonexmax = Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval));
                dragzoneymin = Convert.ToInt32((double)_height * 0.85);
                dragzoneymax = Convert.ToInt32((double)_height * 0.97);
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
        SetForms2.YouBiao YouBiao = new();
        if (!string.IsNullOrEmpty(varname))
        {
            YouBiao.colorvaluert = colorvaluert;
            YouBiao.maxval = maxval;
            YouBiao.varname = varname.Substring(1, varname.Length - 2);
            YouBiao.minval = minval;
            YouBiao.mainmark = mainmark;
            YouBiao.othermark = othermark;
            YouBiao.ptcount = ptcount;
            YouBiao.color1 = color1;
        }
        YouBiao.viewevent += GetTable;
        YouBiao.ckvarevent += CheckVar;
        if (YouBiao.ShowDialog() == DialogResult.OK)
        {
            colorvaluert = YouBiao.colorvaluert;
            maxval = YouBiao.maxval;
            varname = "[" + YouBiao.varname + "]";
            minval = YouBiao.minval;
            mainmark = YouBiao.mainmark;
            othermark = YouBiao.othermark;
            ptcount = YouBiao.ptcount;
            color1 = YouBiao.color1;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave bSave = new()
        {
            varname = varname,
            maxval = maxval,
            minval = minval,
            mainmark = mainmark,
            othermark = othermark,
            ptcount = ptcount,
            color1 = color1
        };
        formatter.Serialize(memoryStream, bSave);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override void Deserialize(byte[] data)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(data);
            BSave bSave = (BSave)formatter.Deserialize(stream);
            stream.Close();
            varname = bSave.varname;
            maxval = bSave.maxval;
            minval = bSave.minval;
            mainmark = bSave.mainmark;
            othermark = bSave.othermark;
            ptcount = bSave.ptcount;
            color1 = bSave.color1;
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
