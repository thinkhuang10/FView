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
public class drawbitmap3 : CPixieControl
{
    private bool highalertflag;

    private bool lowalertflag;

    private bool highalertflag2;

    private bool lowalertflag2;

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

    private Color colorvaluert1 = Color.Green;

    private string varname2 = "";

    private float maxval2 = 100f;

    private float minval2;

    private int mainmark2 = 5;

    private int othermark2 = 2;

    private int ptcount2 = 2;

    private Color color2 = Color.Green;

    private int dragzonexmin;

    private int dragzonexmax;

    private int dragzoneymin;

    private int dragzoneymax;

    private bool dragableflag;

    private int dragzonexmin2;

    private int dragzonexmax2;

    private int dragzoneymin2;

    private int dragzoneymax2;

    private bool dragableflag2;

    private Color colorvaluert2 = Color.Green;

    [NonSerialized]
    private float value;

    private float value2;

    [Description("控件绑定变量名。")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [DisplayName("绑定变量1")]
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

    [Category("杂项")]
    [DisplayName("量程上限1")]
    [DHMICtrlProperty]
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

    [DisplayName("量程下限1")]
    [DHMICtrlProperty]
    [Category("杂项")]
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

    [DisplayName("主刻度数1")]
    [Category("杂项")]
    [Description("表盘主刻度线数量。")]
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

    [DisplayName("副刻度数1")]
    [Category("杂项")]
    [Description("表盘副刻度线数量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DisplayName("小数位1")]
    [Category("杂项")]
    [Description("表盘显示数值小数精确位数。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
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

    [DisplayName("刻度盘背景色")]
    [Category("杂项")]
    [Description("刻度盘背景颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public Color BackColor11
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("YouBiao滑动时YouBiao下方液柱的填充色。")]
    [DisplayName("液柱填充色1")]
    public Color ColorFill1
    {
        get
        {
            return colorvaluert1;
        }
        set
        {
            colorvaluert1 = value;
        }
    }

    [Category("杂项")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量2")]
    [Description("控件绑定变量名。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string BindVar2
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
    [Category("杂项")]
    [Description("YouBiao量程上限。")]
    [DisplayName("量程上限2")]
    public float Maxvalue2
    {
        get
        {
            return maxval2;
        }
        set
        {
            maxval2 = value;
        }
    }

    [DisplayName("量程下限2")]
    [Description("YouBiao量程下限。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public float Minvalue2
    {
        get
        {
            return minval2;
        }
        set
        {
            minval2 = value;
        }
    }

    [Description("表盘主刻度线数量。")]
    [DisplayName("主刻度数2")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public int MainMarkCount2
    {
        get
        {
            return mainmark2;
        }
        set
        {
            mainmark2 = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("副刻度数2")]
    [Description("表盘副刻度线数量。")]
    [ReadOnly(false)]
    public int OtherMarkCount2
    {
        get
        {
            return othermark2;
        }
        set
        {
            othermark2 = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("小数位2")]
    [Description("表盘显示数值小数精确位数。")]
    public int DecimalPlacesCount2
    {
        get
        {
            return ptcount2;
        }
        set
        {
            ptcount2 = value;
        }
    }

    [Category("杂项")]
    [ReadOnly(false)]
    [DisplayName("刻度盘背景色2")]
    [Browsable(false)]
    [Description("刻度盘背景颜色。")]
    [DHMICtrlProperty]
    public Color BackColor2
    {
        get
        {
            return color2;
        }
        set
        {
            color2 = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("液柱填充色2")]
    [Category("杂项")]
    [Description("YouBiao滑动时YouBiao下方液柱的填充色。")]
    public Color ColorFill2
    {
        get
        {
            return colorvaluert2;
        }
        set
        {
            colorvaluert2 = value;
        }
    }

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

    public float Value2
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

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public event EventHandler HighAlert2;

    public event EventHandler LowAlert2;

    public drawbitmap3()
    {
    }

    protected drawbitmap3(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap3 info");
        }
        drawbitmap3 obj = new();
        FieldInfo[] fields = typeof(drawbitmap3).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap3))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap3))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap3), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap3) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap3 drawbitmap6 = (drawbitmap3)base.Copy();
        drawbitmap6.varname = varname;
        drawbitmap6.maxval = maxval;
        drawbitmap6.minval = minval;
        drawbitmap6.mainmark = mainmark;
        drawbitmap6.othermark = othermark;
        drawbitmap6.ptcount = ptcount;
        drawbitmap6.varname2 = varname2;
        drawbitmap6.maxval2 = maxval2;
        drawbitmap6.minval2 = minval2;
        drawbitmap6.mainmark2 = mainmark2;
        drawbitmap6.othermark2 = othermark2;
        drawbitmap6.ptcount2 = ptcount2;
        drawbitmap6.colorvaluert1 = colorvaluert1;
        drawbitmap6.colorvaluert2 = colorvaluert2;
        drawbitmap6.color1 = color1;
        drawbitmap6.color2 = color2;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(color1);
        SolidBrush brush2 = new(colorvaluert1);
        SolidBrush brush3 = new(colorvaluert2);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)_width * 0.06));
        g.FillRectangle(rect: new Rectangle(0, 0, _width, _height), brush: Brushes.LightGray);
        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.05), Convert.ToInt32((double)_height * 0.05), Convert.ToInt32((double)_width * 0.9), Convert.ToInt32((double)_height * 0.9));
        g.FillRectangle(brush, rect2);
        float num = Convert.ToSingle((double)_height * 0.6 / (double)mainmark);
        float num2 = Convert.ToSingle((double)_height * 0.6 / (double)mainmark2);
        float num3 = Convert.ToSingle((double)_height * 0.6 / (double)(mainmark * (othermark + 1)));
        float num4 = Convert.ToSingle((double)_height * 0.6 / (double)(mainmark2 * (othermark2 + 1)));
        Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.6));
        g.FillRectangle(brush2, rect3);
        g.DrawRectangle(Pens.White, rect3);
        Rectangle rect4 = new(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.6));
        g.FillRectangle(brush3, rect4);
        g.DrawRectangle(Pens.White, rect4);
        for (int i = 0; i <= mainmark; i++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.2), Convert.ToSingle((double)_height * 0.2 + (double)((float)i * num)), Convert.ToSingle((double)_width * 0.35), Convert.ToSingle((double)_height * 0.2 + (double)((float)i * num)));
        }
        for (int j = 0; j <= mainmark * (othermark + 1); j++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.275), Convert.ToSingle((double)_height * 0.2 + (double)((float)j * num3)), Convert.ToSingle((double)_width * 0.35), Convert.ToSingle((double)_height * 0.2 + (double)((float)j * num3)));
        }
        for (int k = 0; k <= mainmark2; k++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.65), Convert.ToSingle((double)_height * 0.2 + (double)((float)k * num2)), Convert.ToSingle((double)_width * 0.8), Convert.ToSingle((double)_height * 0.2 + (double)((float)k * num2)));
        }
        for (int l = 0; l <= mainmark2 * (othermark2 + 1); l++)
        {
            g.DrawLine(Pens.White, Convert.ToSingle((double)_width * 0.65), Convert.ToSingle((double)_height * 0.2 + (double)((float)l * num4)), Convert.ToSingle((double)_width * 0.725), Convert.ToSingle((double)_height * 0.2 + (double)((float)l * num4)));
        }
        List<string> list = new();
        List<Point> list2 = new();
        for (int m = 0; m <= mainmark; m++)
        {
            list.Add(Math.Round(minval + (float)m * ((maxval - minval) / (float)mainmark), ptcount).ToString());
        }
        for (int num5 = mainmark; num5 >= 0; num5--)
        {
            list2.Add(new Point(Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.2 + (double)((float)num5 * num) - (double)_height * 0.02)));
        }
        for (int n = 0; n <= mainmark; n++)
        {
            g.DrawString(list[n], font, Brushes.White, list2[n]);
        }
        List<string> list3 = new();
        List<Point> list4 = new();
        for (int num6 = 0; num6 <= mainmark2; num6++)
        {
            list3.Add(Math.Round(minval2 + (float)num6 * ((maxval2 - minval2) / (float)mainmark2), ptcount2).ToString());
        }
        for (int num7 = mainmark2; num7 >= 0; num7--)
        {
            list4.Add(new Point(Convert.ToInt32((double)_width * 0.81), Convert.ToInt32((double)_height * 0.2 + (double)((float)num7 * num2) - (double)_height * 0.02)));
        }
        for (int num8 = 0; num8 <= mainmark2; num8++)
        {
            g.DrawString(list3[num8], font, Brushes.White, list4[num8]);
        }
        Point point = new(Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.1));
        Point point2 = new(Convert.ToInt32((double)_width * 0.6), Convert.ToInt32((double)_height * 0.1));
        g.DrawString(" ", font, Brushes.White, point);
        g.DrawString(" ", font, Brushes.White, point2);
        if (initflag == 1)
        {
            Point[] points = new Point[3]
            {
                new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.8)),
                new(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.775)),
                new(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.825))
            };
            dragzonexmin = Convert.ToInt32((double)_width * 0.3);
            dragzonexmax = Convert.ToInt32((double)_width * 0.35);
            dragzoneymin = Convert.ToInt32((double)_height * 0.775);
            dragzoneymax = Convert.ToInt32((double)_height * 0.825);
            Point[] points2 = new Point[3]
            {
                new(Convert.ToInt32((double)_width * 0.65), Convert.ToInt32((double)_height * 0.8)),
                new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.775)),
                new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.825))
            };
            g.FillPolygon(Brushes.Green, points);
            g.FillPolygon(Brushes.Green, points2);
            dragzonexmin2 = Convert.ToInt32((double)_width * 0.65);
            dragzonexmax2 = Convert.ToInt32((double)_width * 0.7);
            dragzoneymin2 = Convert.ToInt32((double)_height * 0.775);
            dragzoneymax2 = Convert.ToInt32((double)_height * 0.825);
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        if (e.X > dragzonexmin && e.X < dragzonexmax && e.Y > dragzoneymin && e.Y < dragzoneymax)
        {
            dragableflag = true;
        }
        if (e.X > dragzonexmin2 && e.X < dragzonexmax2 && e.Y > dragzoneymin2 && e.Y < dragzoneymax2)
        {
            dragableflag2 = true;
        }
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        dragableflag = false;
        dragableflag2 = false;
    }

    public override void ManageMouseMove(MouseEventArgs e)
    {
        if (dragableflag)
        {
            float num = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.6) + (double)minval);
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
        if (dragableflag2)
        {
            float num2 = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval2 - minval2) / ((double)_height * 0.6) + (double)minval2);
            if (num2 > maxval2)
            {
                num2 = maxval2;
            }
            if (num2 < minval2)
            {
                num2 = minval2;
            }
            if (varname != "")
            {
                SetValue(varname2, num2);
            }
            else
            {
                Value2 = num2;
            }
            NeedRefresh = true;
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = ((!(varname != "")) ? null : GetValue(varname));
            object obj2 = ((!(varname2 != "")) ? null : GetValue(varname2));
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
            if (obj2 != null)
            {
                Value2 = Convert.ToSingle(obj2);
            }
            try
            {
                if (obj == null)
                {
                    obj = minval;
                }
                if (obj2 == null)
                {
                    obj2 = minval2;
                }
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
                float num2;
                if (Value2 > maxval2)
                {
                    if (!highalertflag2 && HighAlert2 != null)
                    {
                        HighAlert2(this, null);
                        highalertflag2 = true;
                    }
                    num2 = maxval2;
                }
                else if (Value2 < minval2)
                {
                    if (!lowalertflag2 && LowAlert2 != null)
                    {
                        LowAlert2(this, null);
                        lowalertflag2 = true;
                    }
                    num2 = minval2;
                }
                else
                {
                    highalertflag2 = false;
                    lowalertflag2 = false;
                    num2 = Value2;
                }
                FontFamily family = new("宋体");
                Font font = new(family, Convert.ToInt32((double)_width * 0.06));
                initflag = 0;
                Paint(graphics);
                Point[] points = new Point[3]
                {
                    new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval))),
                    new(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval))),
                    new(Convert.ToInt32((double)_width * 0.3), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval)))
                };
                dragzonexmin = Convert.ToInt32((double)_width * 0.3);
                dragzonexmax = Convert.ToInt32((double)_width * 0.35);
                dragzoneymin = Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval));
                dragzoneymax = Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval));
                Point[] points2 = new Point[3]
                {
                    new(Convert.ToInt32((double)_width * 0.65), Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(num2 - minval2) / (double)(maxval2 - minval2))),
                    new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.6 * (double)(num2 - minval2) / (double)(maxval2 - minval2))),
                    new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.6 * (double)(num2 - minval2) / (double)(maxval2 - minval2)))
                };
                dragzonexmin2 = Convert.ToInt32((double)_width * 0.65);
                dragzonexmax2 = Convert.ToInt32((double)_width * 0.7);
                dragzoneymin2 = Convert.ToInt32((double)_height * 0.775 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval));
                dragzoneymax2 = Convert.ToInt32((double)_height * 0.825 - (double)_height * 0.6 * (double)(num - minval) / (double)(maxval - minval));
                graphics.FillPolygon(Brushes.Green, points);
                graphics.FillPolygon(Brushes.Green, points2);
                Point point = new(Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.86));
                Point point2 = new(Convert.ToInt32((double)_width * 0.7), Convert.ToInt32((double)_height * 0.86));
                float num3 = ((obj == null) ? minval : ((float)Math.Round(Convert.ToSingle(obj), ptcount)));
                float num4 = ((obj2 == null) ? minval2 : ((float)Math.Round(Convert.ToSingle(obj2), ptcount)));
                graphics.DrawString(num3.ToString(), font, Brushes.Green, point);
                graphics.DrawString(num4.ToString(), font, Brushes.Green, point2);
                Rectangle rect = new(Convert.ToInt32((double)_width * 0.35), Convert.ToInt32((double)_height * 0.8 - (double)((num - minval) / (maxval - minval) * (float)_height) * 0.6), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)((num - minval) / (maxval - minval) * (float)_height) * 0.6));
                Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.55), Convert.ToInt32((double)_height * 0.8 - (double)((num2 - minval2) / (maxval2 - minval2) * (float)_height) * 0.6), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)((num2 - minval2) / (maxval2 - minval2) * (float)_height) * 0.6));
                SolidBrush brush = new(colorvaluert1);
                SolidBrush brush2 = new(colorvaluert2);
                graphics.FillRectangle(brush, rect);
                graphics.FillRectangle(brush2, rect2);
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
        YouBiao2 YouBiao = new();
        if (!string.IsNullOrEmpty(varname) && !string.IsNullOrEmpty(varname2))
        {
            YouBiao.colorvaluert1 = colorvaluert1;
            YouBiao.colorvaluert2 = colorvaluert2;
            YouBiao.maxval = maxval;
            YouBiao.varname = varname.Substring(1, varname.Length - 2);
            YouBiao.minval = minval;
            YouBiao.mainmark = mainmark;
            YouBiao.othermark = othermark;
            YouBiao.ptcount = ptcount;
            YouBiao.color1 = color1;
            YouBiao.maxval2 = maxval2;
            YouBiao.varname2 = varname2.Substring(1, varname2.Length - 2);
            YouBiao.minval2 = minval2;
            YouBiao.mainmark2 = mainmark2;
            YouBiao.othermark2 = othermark2;
            YouBiao.ptcount2 = ptcount2;
            YouBiao.color2 = color2;
        }
        YouBiao.viewevent += GetTable;
        YouBiao.ckvarevent += CheckVar;
        YouBiao.maxval = maxval;
        YouBiao.minval = minval;
        YouBiao.mainmark = mainmark;
        YouBiao.othermark = othermark;
        YouBiao.ptcount = ptcount;
        YouBiao.color1 = color1;
        YouBiao.maxval2 = maxval2;
        YouBiao.colorvaluert1 = colorvaluert1;
        YouBiao.colorvaluert2 = colorvaluert2;
        YouBiao.minval2 = minval2;
        YouBiao.mainmark2 = mainmark2;
        YouBiao.othermark2 = othermark2;
        YouBiao.ptcount2 = ptcount2;
        YouBiao.color2 = color2;
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
            minval = YouBiao.minval;
            mainmark = YouBiao.mainmark;
            othermark = YouBiao.othermark;
            ptcount = YouBiao.ptcount;
            color1 = YouBiao.color1;
            maxval2 = YouBiao.maxval2;
            colorvaluert1 = YouBiao.colorvaluert1;
            colorvaluert2 = YouBiao.colorvaluert2;
            if (YouBiao.varname2 != "")
            {
                varname2 = "[" + YouBiao.varname2 + "]";
            }
            else
            {
                varname2 = "";
            }
            minval2 = YouBiao.minval2;
            mainmark2 = YouBiao.mainmark2;
            othermark2 = YouBiao.othermark2;
            ptcount2 = YouBiao.ptcount2;
            color2 = YouBiao.color2;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC3 bSaveC = new()
        {
            varname = varname,
            maxval = maxval,
            minval = minval,
            mainmark = mainmark,
            othermark = othermark,
            ptcount = ptcount,
            color1 = color1,
            varname2 = varname2,
            maxval2 = maxval2,
            minval2 = minval2,
            mainmark2 = mainmark2,
            othermark2 = othermark2,
            ptcount2 = ptcount2,
            color2 = color2
        };
        formatter.Serialize(memoryStream, bSaveC);
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
            BSaveC3 bSaveC = (BSaveC3)formatter.Deserialize(stream);
            stream.Close();
            varname = bSaveC.varname;
            maxval = bSaveC.maxval;
            minval = bSaveC.minval;
            mainmark = bSaveC.mainmark;
            othermark = bSaveC.othermark;
            ptcount = bSaveC.ptcount;
            color1 = bSaveC.color1;
            varname2 = bSaveC.varname2;
            maxval2 = bSaveC.maxval2;
            minval2 = bSaveC.minval2;
            mainmark2 = bSaveC.mainmark2;
            othermark2 = bSaveC.othermark2;
            ptcount2 = bSaveC.ptcount2;
            color2 = bSaveC.color2;
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
