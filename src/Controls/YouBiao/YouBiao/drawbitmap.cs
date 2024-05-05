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
public class drawbitmap : CPixieControl
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

    private int _height = 400;

    private int initflag = 1;

    private Color color1 = Color.Black;

    private Color colorvaluert = Color.Green;

    [NonSerialized]
    private int dragzonexmin;

    private int dragzonexmax;

    private int dragzoneymin;

    private int dragzoneymax;

    private bool dragableflag;

    private bool addbtndownflag;

    private bool subbtndownflag;

    private float value;

    private float changestep = 1f;

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量")]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
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
    [Description("YouBiao量程上限。")]
    [ReadOnly(false)]
    [DisplayName("量程上限")]
    [Category("杂项")]
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
    [Description("YouBiao量程下限。")]
    [DisplayName("量程下限")]
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

    [Description("表盘主刻度线数量。")]
    [Category("杂项")]
    [DisplayName("主刻度数")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("副刻度数")]
    [Category("杂项")]
    [Description("表盘副刻度线数量。")]
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

    [DisplayName("小数位")]
    [Description("表盘显示数值小数精确位数。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [ReadOnly(false)]
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
    [Description("YouBiao滑动时YouBiao下方液柱的填充色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DisplayName("单次增减值")]
    [Description("点击按钮控制绑定变量值时单次增减幅度。")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [DisplayName("YouBiao当前值")]
    [Category("杂项")]
    [Description("YouBiao当前显示数值。")]
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

    public drawbitmap()
    {
    }

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

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap6 = (drawbitmap)base.Copy();
        drawbitmap6.varname = varname;
        drawbitmap6.maxval = maxval;
        drawbitmap6.maxval = maxval;
        drawbitmap6.minval = minval;
        drawbitmap6.mainmark = mainmark;
        drawbitmap6.othermark = othermark;
        drawbitmap6.ptcount = ptcount;
        drawbitmap6.changestep = changestep;
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
        g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.05), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.1)), brush: Brushes.Blue);
        float num = Convert.ToSingle((double)_height * 0.5 / (double)mainmark);
        float num2 = Convert.ToSingle((double)_height * 0.5 / (double)(mainmark * (othermark + 1)));
        for (int i = 0; i <= mainmark; i++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.25), Convert.ToSingle((double)_height * 0.3 + (double)((float)i * num)), Convert.ToSingle((double)_width * 0.6), Convert.ToSingle((double)_height * 0.3 + (double)((float)i * num)));
        }
        for (int j = 0; j <= mainmark * (othermark + 1); j++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.3), Convert.ToSingle((double)_height * 0.3 + (double)((float)j * num2)), Convert.ToSingle((double)_width * 0.55), Convert.ToSingle((double)_height * 0.3 + (double)((float)j * num2)));
        }
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.375), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.5));
        g.FillRectangle(brush, rect3);
        List<string> list = new();
        List<Point> list2 = new();
        for (int k = 0; k <= mainmark; k++)
        {
            list.Add(Math.Round(minval + (float)k * ((maxval - minval) / (float)mainmark), ptcount).ToString());
        }
        for (int num3 = mainmark; num3 >= 0; num3--)
        {
            list2.Add(new Point(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.3 + (double)((float)num3 * num))));
        }
        for (int l = 0; l <= mainmark; l++)
        {
            g.DrawString(list[l], font, Brushes.Black, list2[l]);
        }
        new Rectangle(Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.075));
        g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.2));
        g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.275));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.2));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.275));
        if (addbtndownflag)
        {
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.2));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.275));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.2));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.275));
        }
        g.FillPolygon(points: new Point[3]
        {
            new(Convert.ToInt32((double)_width * 0.675), Convert.ToInt32((double)_height * 0.225)),
            new(Convert.ToInt32((double)_width * 0.635), Convert.ToInt32((double)_height * 0.25)),
            new(Convert.ToInt32((double)_width * 0.715), Convert.ToInt32((double)_height * 0.25))
        }, brush: Brushes.Green);
        new Rectangle(Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.075));
        g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.825));
        g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.9));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.825));
        g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.9));
        if (subbtndownflag)
        {
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.825));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.9));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.825));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.75), Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.9));
        }
        g.FillPolygon(points: new Point[3]
        {
            new(Convert.ToInt32((double)_width * 0.675), Convert.ToInt32((double)_height * 0.875)),
            new(Convert.ToInt32((double)_width * 0.635), Convert.ToInt32((double)_height * 0.85)),
            new(Convert.ToInt32((double)_width * 0.715), Convert.ToInt32((double)_height * 0.85))
        }, brush: Brushes.Green);
        Pen pen = new(Color.Black, Convert.ToInt32((double)_width * 0.015));
        g.DrawLine(pen, Convert.ToInt32((double)_width * 0.675), Convert.ToInt32((double)_height * 0.275), Convert.ToInt32((double)_width * 0.675), Convert.ToInt32((double)_height * 0.825));
        if (initflag == 1)
        {
            g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775), Convert.ToInt32((double)_width * 0.25), Convert.ToInt32((double)_height * 0.05)), brush: Brushes.LightGray);
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775), Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.825));
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.775));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.825));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.825), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.775));
            g.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.8), Convert.ToInt32((double)_width * 0.65), Convert.ToInt32((double)_height * 0.8));
            dragzonexmin = Convert.ToInt32((double)_width * 0.55);
            dragzonexmax = Convert.ToInt32((double)_width * 0.8);
            dragzoneymin = Convert.ToInt32((double)_height * 0.775);
            dragzoneymax = Convert.ToInt32((double)_height * 0.825);
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        if (!isRunning)
        {
            return;
        }
        if (e.X > dragzonexmin && e.X < dragzonexmax && e.Y > dragzoneymin && e.Y < dragzoneymax)
        {
            dragableflag = true;
        }
        if (e.X > Convert.ToInt32((double)_width * 0.6) && e.X < Convert.ToInt32((double)_width * 0.75) && e.Y > Convert.ToInt32((double)_height * 0.2) && e.Y < Convert.ToInt32((double)_height * 0.275))
        {
            if (varname != "")
            {
                if (Convert.ToSingle(GetValue(varname)) + changestep > maxval)
                {
                    SetValue(varname, maxval);
                }
                else
                {
                    SetValue(varname, Convert.ToSingle(GetValue(varname)) + changestep);
                }
            }
            else
            {
                Value += changestep;
            }
            addbtndownflag = true;
            NeedRefresh = true;
        }
        if (e.X <= Convert.ToInt32((double)_width * 0.6) || e.X >= Convert.ToInt32((double)_width * 0.75) || e.Y <= Convert.ToInt32((double)_height * 0.825) || e.Y >= Convert.ToInt32((double)_height * 0.9))
        {
            return;
        }
        if (varname != "")
        {
            if (Convert.ToSingle(GetValue(varname)) - changestep < minval)
            {
                SetValue(varname, minval);
            }
            else
            {
                SetValue(varname, Convert.ToSingle(GetValue(varname)) - changestep);
            }
        }
        else
        {
            Value -= changestep;
        }
        subbtndownflag = true;
        NeedRefresh = true;
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        dragableflag = false;
        addbtndownflag = false;
        subbtndownflag = false;
    }

    public override void ManageMouseMove(MouseEventArgs e)
    {
        if (dragableflag)
        {
            float num = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.5) + (double)minval);
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
                _height = 400;
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
                float num = Convert.ToSingle(0);
                SolidBrush brush = new(colorvaluert);
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
                initflag = 0;
                Paint(graphics);
                graphics.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.25), Convert.ToInt32((double)_height * 0.05)), brush: Brushes.LightGray);
                graphics.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)));
                graphics.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.8), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)));
                graphics.DrawLine(Pens.Black, Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.65), Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval)));
                FontFamily family = new("宋体");
                Font font = new(family, Convert.ToInt32((double)_width * 0.07));
                Point point = new(Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.08));
                Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.375), Convert.ToInt32((double)_height * 0.8 - (double)((num - minval) / (maxval - minval) * (float)_height) * 0.5), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)((num - minval) / (maxval - minval) * (float)_height) * 0.5));
                graphics.FillRectangle(brush, rect2);
                graphics.DrawString(((float)Math.Round(Convert.ToSingle(obj), ptcount)).ToString(), font, Brushes.White, point);
                dragzonexmin = Convert.ToInt32((double)_width * 0.55);
                dragzonexmax = Convert.ToInt32((double)_width * 0.8);
                dragzoneymin = Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval));
                dragzoneymax = Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.5 * (double)(num - minval) / (double)(maxval - minval));
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
            YouBiao.maxval = maxval;
            YouBiao.varname = varname.Substring(1, varname.Length - 2);
            YouBiao.minval = minval;
            YouBiao.mainmark = mainmark;
            YouBiao.othermark = othermark;
            YouBiao.ptcount = ptcount;
            YouBiao.changestep = changestep;
            YouBiao.color1 = color1;
            YouBiao.colorvaluert = colorvaluert;
        }
        YouBiao.viewevent += GetTable;
        YouBiao.ckvarevent += CheckVar;
        YouBiao.btnflag = true;
        if (YouBiao.ShowDialog() == DialogResult.OK)
        {
            maxval = YouBiao.maxval;
            colorvaluert = YouBiao.colorvaluert;
            varname = "[" + YouBiao.varname + "]";
            minval = YouBiao.minval;
            mainmark = YouBiao.mainmark;
            othermark = YouBiao.othermark;
            ptcount = YouBiao.ptcount;
            color1 = YouBiao.color1;
            changestep = YouBiao.changestep;
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
            color1 = color1,
            dragableflag = dragableflag
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
            dragableflag = bSave.dragableflag;
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
