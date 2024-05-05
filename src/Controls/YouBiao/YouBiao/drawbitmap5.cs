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
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap5 : CPixieControl
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

    private Color colorvaluert = Color.Orange;

    [NonSerialized]
    private float value;

    [ReadOnly(false)]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
    [DisplayName("绑定变量")]
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

    [DisplayName("量程上限")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("YouBiao量程上限。")]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("量程下限")]
    [Category("杂项")]
    [Description("YouBiao量程下限。")]
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

    [Description("表盘主刻度线数量。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("主刻度数")]
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
    [DisplayName("副刻度数")]
    [Category("杂项")]
    [Description("表盘副刻度线数量。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [Description("表盘显示数值小数精确位数。")]
    [DHMICtrlProperty]
    [DisplayName("小数位")]
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
    [Description("刻度盘背景颜色。")]
    [ReadOnly(false)]
    [DisplayName("刻度盘背景色")]
    [Category("杂项")]
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

    [Description("YouBiao滑动时YouBiao下方液柱的填充色。")]
    [ReadOnly(false)]
    [DisplayName("液柱填充色")]
    [Category("杂项")]
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

    [Description("YouBiao当前显示数值。")]
    [DisplayName("YouBiao当前值")]
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

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public drawbitmap5()
    {
    }

    protected drawbitmap5(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap5 info");
        }
        drawbitmap5 obj = new();
        FieldInfo[] fields = typeof(drawbitmap5).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap5))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap5), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap5))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap5), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap5) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap5 drawbitmap6 = (drawbitmap5)base.Copy();
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
        SolidBrush brush = new(color1);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)_width * 0.04));
        g.FillRectangle(rect: new Rectangle(0, 0, _width, _height), brush: Brushes.LightGray);
        float num = Convert.ToSingle((double)_width * 0.8 / (double)mainmark);
        float num2 = Convert.ToSingle((double)_width * 0.8 / (double)(mainmark * (othermark + 1)));
        for (int i = 0; i <= mainmark; i++)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.1 + (double)((float)i * num)), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.1 + (double)((float)i * num)), Convert.ToInt32((double)_height * 0.55));
        }
        for (int j = 0; j <= mainmark * (othermark + 1); j++)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.1 + (double)((float)j * num2)), Convert.ToInt32((double)_height * 0.25), Convert.ToInt32((double)_width * 0.1 + (double)((float)j * num2)), Convert.ToInt32((double)_height * 0.5));
        }
        List<string> list = new();
        List<Point> list2 = new();
        for (int k = 0; k <= mainmark; k++)
        {
            list.Add((minval + (float)k * ((maxval - minval) / (float)mainmark)).ToString());
        }
        for (int l = 0; (float)l <= num; l++)
        {
            list2.Add(new Point(Convert.ToInt32((double)_width * 0.1 + (double)((float)l * num) - (double)_width * 0.02), Convert.ToInt32((double)_height * 0.6)));
        }
        for (int m = 0; m <= mainmark; m++)
        {
            g.DrawString(list[m], font, Brushes.Black, list2[m]);
        }
        Font font2 = new(family, Convert.ToInt32((double)_width * 0.07));
        g.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.05)), s: " ", font: font2, brush: Brushes.Black);
        g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.75), Convert.ToInt32((double)_width * 0.4), Convert.ToInt32((double)_height * 0.15)), brush: Brushes.Black);
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.15));
        g.FillRectangle(brush, rect3);
        if (initflag == 1)
        {
            g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.03), Convert.ToInt32((double)_height * 0.15)), brush: Brushes.Green);
            dragzonexmin = Convert.ToInt32((double)_width * 0.1);
            dragzonexmax = Convert.ToInt32((double)_width * 0.13);
            dragzoneymin = Convert.ToInt32((double)_height * 0.3);
            dragzoneymax = Convert.ToInt32((double)_height * 0.45);
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
        if (dragableflag)
        {
            float num = Convert.ToSingle(((double)e.X - (double)_width * 0.1) * (double)(maxval - minval) / ((double)_width * 0.8) + (double)minval);
            if (num > maxval)
            {
                num = maxval;
            }
            if (num < minval)
            {
                num = minval;
            }
            if (varname != "")
            {
                SetValue(varname, num);
            }
            else
            {
                Value = num;
            }
            NeedRefresh = true;
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                _height = 200;
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
                    if (!highalertflag && HighAlert != null)
                    {
                        HighAlert(this, null);
                        highalertflag = true;
                    }
                    num = maxval;
                }
                else if (Value < minval)
                {
                    if (!lowalertflag && LowAlert != null)
                    {
                        LowAlert(this, null);
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
                FontFamily family = new("宋体");
                Font font = new(family, Convert.ToInt32((double)_width * 0.06));
                initflag = 0;
                Paint(graphics);
                graphics.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.1 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.03), Convert.ToInt32((double)_height * 0.15)), brush: Brushes.Green);
                graphics.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.32), Convert.ToInt32((double)_height * 0.8)), s: ((float)Math.Round(Convert.ToSingle(obj), ptcount)).ToString(), font: font, brush: Brushes.White);
                Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_height * 0.15));
                SolidBrush brush = new(colorvaluert);
                graphics.FillRectangle(brush, rect2);
                dragzonexmin = Convert.ToInt32((double)_width * 0.1 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval));
                dragzonexmax = Convert.ToInt32((double)_width * 0.13 + (double)_width * 0.8 * (double)(num - minval) / (double)(maxval - minval));
                dragzoneymin = Convert.ToInt32((double)_height * 0.3);
                dragzoneymax = Convert.ToInt32((double)_height * 0.45);
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
            maxval = YouBiao.maxval;
            if (YouBiao.varname != "")
            {
                varname = "[" + YouBiao.varname + "]";
            }
            else
            {
                varname = "";
            }
            colorvaluert = YouBiao.colorvaluert;
            minval = YouBiao.minval;
            mainmark = YouBiao.mainmark;
            othermark = YouBiao.othermark;
            ptcount = YouBiao.ptcount;
            color1 = YouBiao.color1;
            NeedRefresh = true;
        }
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
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

    private string GetTable()
    {
        return GetVarNames();
    }
}
