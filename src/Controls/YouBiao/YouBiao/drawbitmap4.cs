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
public class drawbitmap4 : CPixieControl
{
    private bool highalertflag;

    private bool lowalertflag;

    private bool highalertflag2;

    private bool lowalertflag2;

    private bool highalertflag3;

    private bool lowalertflag3;

    private bool highalertflag4;

    private bool lowalertflag4;

    private bool highalertflag5;

    private bool lowalertflag5;

    private int initflag = 1;

    private string varname1 = "";

    private string varname2 = "";

    private string varname3 = "";

    private string varname4 = "";

    private string varname5 = "";

    private string txt1 = "";

    private string txt2 = "";

    private string txt3 = "";

    private string txt4 = "";

    private string txt5 = "";

    private Color bgcolor1 = Color.Black;

    private Color bgcolor2 = Color.Black;

    private Color bgcolor3 = Color.Black;

    private Color bgcolor4 = Color.Black;

    private Color bgcolor5 = Color.Black;

    private Color valuertcolor1 = Color.Orange;

    private Color valuertcolor2 = Color.Orange;

    private Color valuertcolor3 = Color.Orange;

    private Color valuertcolor4 = Color.Orange;

    private Color valuertcolor5 = Color.Orange;

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

    private int dragzonexmin3;

    private int dragzonexmax3;

    private int dragzoneymin3;

    private int dragzoneymax3;

    private bool dragableflag3;

    private int dragzonexmin4;

    private int dragzonexmax4;

    private int dragzoneymin4;

    private int dragzoneymax4;

    private bool dragableflag4;

    private int dragzonexmin5;

    private int dragzonexmax5;

    private int dragzoneymin5;

    private int dragzoneymax5;

    private bool dragableflag5;

    private int _width = 200;

    private int _height = 200;

    private int mainmark = 5;

    private int othermark = 2;

    private float maxval = 100f;

    private float minval;

    private int addbtndownflag;

    private int subbtndownflag;

    private float changestep = 1f;

    [NonSerialized]
    private float value;

    private float value2;

    private float value3;

    private float value4;

    private float value5;

    [Description("控件绑定变量名。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DisplayName("绑定变量1")]
    [Category("杂项")]
    public string BindVar1
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
    [DisplayName("绑定变量2")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
    [ReadOnly(false)]
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

    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [DHMICtrlProperty]
    [DisplayName("绑定变量3")]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
    [ReadOnly(false)]
    public string BindVar3
    {
        get
        {
            if (varname3.ToString().IndexOf('[') == -1)
            {
                return varname3;
            }
            return varname3.Substring(1, varname3.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname3 = "[" + value.ToString() + "]";
            }
            else
            {
                varname3 = value;
            }
        }
    }

    [Category("杂项")]
    [DisplayName("绑定变量4")]
    [Description("控件绑定变量名。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    public string BindVar4
    {
        get
        {
            if (varname4.ToString().IndexOf('[') == -1)
            {
                return varname4;
            }
            return varname4.Substring(1, varname4.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname4 = "[" + value.ToString() + "]";
            }
            else
            {
                varname4 = value;
            }
        }
    }

    [Description("控件绑定变量名。")]
    [Category("杂项")]
    [DisplayName("绑定变量5")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    public string BindVar5
    {
        get
        {
            if (varname5.ToString().IndexOf('[') == -1)
            {
                return varname5;
            }
            return varname5.Substring(1, varname5.Length - 2);
        }
        set
        {
            if (value.ToString().IndexOf('[') == -1)
            {
                varname5 = "[" + value.ToString() + "]";
            }
            else
            {
                varname5 = value;
            }
        }
    }

    [DisplayName("文本1")]
    [Category("杂项")]
    [Description("刻度盘下方的文本。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string Text1
    {
        get
        {
            return txt1;
        }
        set
        {
            txt1 = value;
        }
    }

    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("刻度盘下方的文本。")]
    [ReadOnly(false)]
    [DisplayName("文本2")]
    public string Text2
    {
        get
        {
            return txt2;
        }
        set
        {
            txt2 = value;
        }
    }

    [Category("杂项")]
    [DisplayName("文本3")]
    [Description("刻度盘下方的文本。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string Text3
    {
        get
        {
            return txt3;
        }
        set
        {
            txt3 = value;
        }
    }

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("文本4")]
    [Description("刻度盘下方的文本。")]
    public string Text4
    {
        get
        {
            return txt4;
        }
        set
        {
            txt4 = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("文本5")]
    [Category("杂项")]
    [Description("刻度盘下方的文本。")]
    public string Text5
    {
        get
        {
            return txt5;
        }
        set
        {
            txt5 = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("刻度盘背景色1")]
    [Description("刻度盘背景颜色。")]
    [ReadOnly(false)]
    public Color Bgcolor1
    {
        get
        {
            return bgcolor1;
        }
        set
        {
            bgcolor1 = value;
        }
    }

    [Description("刻度盘背景颜色。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("刻度盘背景色2")]
    [Category("杂项")]
    public Color Bgcolor2
    {
        get
        {
            return bgcolor2;
        }
        set
        {
            bgcolor2 = value;
        }
    }

    [DisplayName("刻度盘背景色3")]
    [Description("刻度盘背景颜色。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [ReadOnly(false)]
    public Color Bgcolor3
    {
        get
        {
            return bgcolor3;
        }
        set
        {
            bgcolor3 = value;
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("刻度盘背景颜色。")]
    [DisplayName("刻度盘背景色4")]
    [Category("杂项")]
    public Color Bgcolor4
    {
        get
        {
            return bgcolor4;
        }
        set
        {
            bgcolor4 = value;
        }
    }

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("刻度盘背景色5")]
    [Description("刻度盘背景颜色。")]
    [ReadOnly(false)]
    public Color Bgcolor5
    {
        get
        {
            return bgcolor5;
        }
        set
        {
            bgcolor5 = value;
        }
    }

    [Category("杂项")]
    [DisplayName("刻度盘液柱颜色1")]
    [DHMICtrlProperty]
    [Description("YouBiao滑动时YouBiao下方液柱的颜色。")]
    [ReadOnly(false)]
    public Color ColorFill1
    {
        get
        {
            return valuertcolor1;
        }
        set
        {
            valuertcolor1 = value;
        }
    }

    [DisplayName("刻度盘液柱颜色2")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("YouBiao滑动时YouBiao下方液柱的颜色。")]
    [ReadOnly(false)]
    public Color ColorFill2
    {
        get
        {
            return valuertcolor2;
        }
        set
        {
            valuertcolor2 = value;
        }
    }

    [DisplayName("刻度盘液柱颜色3")]
    [Description("YouBiao滑动时YouBiao下方液柱的颜色。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Category("杂项")]
    public Color ColorFill3
    {
        get
        {
            return valuertcolor3;
        }
        set
        {
            valuertcolor3 = value;
        }
    }

    [DHMICtrlProperty]
    [Description("YouBiao滑动时YouBiao下方液柱的颜色。")]
    [DisplayName("刻度盘液柱颜色4")]
    [ReadOnly(false)]
    [Category("杂项")]
    public Color ColorFill4
    {
        get
        {
            return valuertcolor4;
        }
        set
        {
            valuertcolor4 = value;
        }
    }

    [Description("YouBiao滑动时YouBiao下方液柱的颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("刻度盘液柱颜色5")]
    public Color ColorFill5
    {
        get
        {
            return valuertcolor5;
        }
        set
        {
            valuertcolor5 = value;
        }
    }

    [ReadOnly(false)]
    [Category("杂项")]
    [Description("刻度盘主刻度线数。")]
    [DisplayName("刻度盘主刻度数")]
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

    [Category("杂项")]
    [DisplayName("刻度盘副刻度数")]
    [Description("刻度盘副刻度线数。")]
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

    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("量程上限")]
    [Description("刻度盘量程上限。")]
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

    [DisplayName("量程下限")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("刻度盘量程下限。")]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("单次增减值")]
    [Category("杂项")]
    [Description("点击按钮控制绑定变量值时单次增减幅度。")]
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

    [DisplayName("绑定变量1当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
    [Description("绑定变量1当前值。")]
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

    [Category("杂项")]
    [Description("绑定变量2当前值。")]
    [ReadOnly(false)]
    [DisplayName("绑定变量2当前值")]
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

    [Category("杂项")]
    [DisplayName("绑定变量3当前值")]
    [ReadOnly(false)]
    [Description("绑定变量3当前值。")]
    public float Value3
    {
        get
        {
            return value3;
        }
        set
        {
            if (value3 != value)
            {
                NeedRefresh = true;
                value3 = value;
            }
        }
    }

    [Category("杂项")]
    [Description("绑定变量4当前值。")]
    [ReadOnly(false)]
    [DisplayName("绑定变量4当前值")]
    public float Value4
    {
        get
        {
            return value4;
        }
        set
        {
            if (value4 != value)
            {
                NeedRefresh = true;
                value4 = value;
            }
        }
    }

    [Description("绑定变量5当前值。")]
    [DisplayName("绑定变量5当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
    public float Value5
    {
        get
        {
            return value5;
        }
        set
        {
            if (value5 != value)
            {
                NeedRefresh = true;
                value5 = value;
            }
        }
    }

    public event EventHandler HighAlert;

    public event EventHandler LowAlert;

    public event EventHandler HighAlert2;

    public event EventHandler LowAlert2;

    public event EventHandler HighAlert3;

    public event EventHandler LowAlert3;

    public event EventHandler HighAlert4;

    public event EventHandler LowAlert4;

    public event EventHandler HighAlert5;

    public event EventHandler LowAlert5;

    public drawbitmap4()
    {
    }

    protected drawbitmap4(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap4 info");
        }
        drawbitmap4 obj = new();
        FieldInfo[] fields = typeof(drawbitmap4).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap4))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap4))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap4), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap4) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap4 drawbitmap6 = (drawbitmap4)base.Copy();
        drawbitmap6.varname1 = varname1;
        drawbitmap6.txt1 = txt1;
        drawbitmap6.bgcolor1 = bgcolor1;
        drawbitmap6.valuertcolor1 = valuertcolor1;
        drawbitmap6.varname2 = varname2;
        drawbitmap6.txt2 = txt2;
        drawbitmap6.bgcolor2 = bgcolor2;
        drawbitmap6.valuertcolor2 = valuertcolor2;
        drawbitmap6.varname3 = varname3;
        drawbitmap6.txt3 = txt3;
        drawbitmap6.bgcolor3 = bgcolor3;
        drawbitmap6.valuertcolor3 = valuertcolor3;
        drawbitmap6.varname4 = varname4;
        drawbitmap6.txt4 = txt4;
        drawbitmap6.bgcolor4 = bgcolor4;
        drawbitmap6.valuertcolor4 = valuertcolor4;
        drawbitmap6.varname5 = varname5;
        drawbitmap6.txt5 = txt5;
        drawbitmap6.bgcolor5 = bgcolor5;
        drawbitmap6.valuertcolor5 = valuertcolor5;
        drawbitmap6.mainmark = mainmark;
        drawbitmap6.othermark = othermark;
        drawbitmap6.changestep = changestep;
        return drawbitmap6;
    }

    public void Paint(Graphics g)
    {
        List<SolidBrush> list = new();
        SolidBrush item = new(valuertcolor1);
        list.Add(item);
        SolidBrush item2 = new(valuertcolor2);
        list.Add(item2);
        SolidBrush item3 = new(valuertcolor3);
        list.Add(item3);
        SolidBrush item4 = new(valuertcolor4);
        list.Add(item4);
        SolidBrush item5 = new(valuertcolor5);
        list.Add(item5);
        List<SolidBrush> list2 = new();
        SolidBrush item6 = new(bgcolor1);
        list2.Add(item6);
        SolidBrush item7 = new(bgcolor2);
        list2.Add(item7);
        SolidBrush item8 = new(bgcolor3);
        list2.Add(item8);
        SolidBrush item9 = new(bgcolor4);
        list2.Add(item9);
        SolidBrush item10 = new(bgcolor5);
        list2.Add(item10);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)_width * 0.05));
        g.FillRectangle(rect: new Rectangle(0, 0, _width, _height), brush: Brushes.LightGray);
        float num = Convert.ToSingle((double)_height * 0.6 / (double)mainmark);
        float num2 = Convert.ToSingle((double)_height * 0.6 / (double)(mainmark * (othermark + 1)));
        for (int i = 0; i <= mainmark; i++)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.2 + (double)((float)i * num)), Convert.ToInt32((double)_width * 0.9), Convert.ToInt32((double)_height * 0.2 + (double)((float)i * num)));
        }
        for (int j = 0; j <= mainmark * (othermark + 1); j++)
        {
            g.DrawLine(Pens.White, Convert.ToInt32((double)_width * 0.15), Convert.ToInt32((double)_height * 0.2 + (double)((float)j * num2)), Convert.ToInt32((double)_width * 0.85), Convert.ToInt32((double)_height * 0.2 + (double)((float)j * num2)));
        }
        List<string> list3 = new();
        List<Point> list4 = new();
        List<Point> list5 = new();
        for (int k = 0; k <= mainmark; k++)
        {
            list3.Add(Math.Round(minval + (float)k * ((maxval - minval) / (float)mainmark), 2).ToString());
        }
        for (int num3 = mainmark; num3 >= 0; num3--)
        {
            list4.Add(new Point(0, Convert.ToInt32((double)_height * 0.2 + (double)((float)num3 * num) - (double)_height * 0.02)));
        }
        for (int num4 = mainmark; num4 >= 0; num4--)
        {
            list5.Add(new Point(Convert.ToInt32((double)_width * 0.91), Convert.ToInt32((double)_height * 0.2 + (double)((float)num4 * num) - (double)_height * 0.02)));
        }
        for (int l = 0; l <= mainmark; l++)
        {
            g.DrawString(list3[l], font, Brushes.White, list4[l]);
            g.DrawString(list3[l], font, Brushes.White, list5[l]);
        }
        for (int m = 0; m < 5; m++)
        {
            int num5 = Convert.ToInt32((double)_width * 0.15 + (double)(m * _width) * 0.14);
            g.FillRectangle(rect: new Rectangle(num5, Convert.ToInt32((double)_height * 0.02), Convert.ToInt32((double)_width * 0.12), Convert.ToInt32((double)_height * 0.08)), brush: Brushes.Black);
            g.FillRectangle(rect: new Rectangle(num5 + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.2), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.6)), brush: list2[m]);
            g.FillRectangle(rect: new Rectangle(num5, Convert.ToInt32((double)_height * 0.9), Convert.ToInt32((double)_width * 0.12), Convert.ToInt32((double)_height * 0.08)), brush: list2[m]);
            Rectangle rect5 = new(num5 + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.12), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.08));
            g.FillRectangle(Brushes.LightGray, rect5);
            g.DrawRectangle(Pens.Black, rect5);
            if (m + 1 == addbtndownflag)
            {
                g.FillRectangle(Brushes.Orange, rect5);
            }
            Point[] points = new Point[3]
            {
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.06)), Convert.ToInt32((double)_height * 0.14)),
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.03)), Convert.ToInt32((double)_height * 0.18)),
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.09)), Convert.ToInt32((double)_height * 0.18))
            };
            Rectangle rect6 = new(num5 + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.81), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.08));
            g.FillRectangle(Brushes.LightGray, rect6);
            g.DrawRectangle(Pens.Black, rect6);
            if (m + 1 == subbtndownflag)
            {
                g.FillRectangle(Brushes.Orange, rect6);
            }
            Point[] points2 = new Point[3]
            {
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.06)), Convert.ToInt32((double)_height * 0.87)),
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.03)), Convert.ToInt32((double)_height * 0.83)),
                new(Convert.ToInt32(num5 + Convert.ToInt32((double)_width * 0.09)), Convert.ToInt32((double)_height * 0.83))
            };
            g.FillPolygon(Brushes.Green, points);
            g.FillPolygon(Brushes.Green, points2);
        }
        List<string> list6 = new();
        list6.Add(txt1);
        list6.Add(txt2);
        list6.Add(txt3);
        list6.Add(txt4);
        list6.Add(txt5);
        List<Point> list7 = new();
        for (int n = 0; n < 5; n++)
        {
            list7.Add(new Point(Convert.ToInt32((double)_width * 0.16 + (double)(n * _width) * 0.14), Convert.ToInt32((double)_height * 0.93)));
        }
        for (int num6 = 0; num6 < 5; num6++)
        {
            g.DrawString(list6[num6], font, list[num6], list7[num6]);
        }
        List<SolidBrush> list8 = new();
        list8.Add(new SolidBrush(valuertcolor1));
        list8.Add(new SolidBrush(valuertcolor2));
        list8.Add(new SolidBrush(valuertcolor3));
        list8.Add(new SolidBrush(valuertcolor4));
        list8.Add(new SolidBrush(valuertcolor5));
        if (initflag == 1)
        {
            for (int num7 = 0; num7 < 5; num7++)
            {
                int num8 = Convert.ToInt32((double)_width * 0.15 + (double)(num7 * _width) * 0.14);
                g.FillRectangle(rect: new Rectangle(num8 + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.78), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.02)), brush: Brushes.Green);
            }
            dragzonexmin = Convert.ToInt32((double)_width * 0.15);
            dragzonexmin2 = Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.14);
            dragzonexmin3 = Convert.ToInt32((double)_width * 0.15 + (double)(2 * _width) * 0.14);
            dragzonexmin4 = Convert.ToInt32((double)_width * 0.15 + (double)(3 * _width) * 0.14);
            dragzonexmin5 = Convert.ToInt32((double)_width * 0.15 + (double)(4 * _width) * 0.14);
            dragzonexmax = Convert.ToInt32((double)_width * 0.23);
            dragzonexmax2 = Convert.ToInt32((double)_width * 0.23 + (double)_width * 0.14);
            dragzonexmax3 = Convert.ToInt32((double)_width * 0.23 + (double)(2 * _width) * 0.14);
            dragzonexmax4 = Convert.ToInt32((double)_width * 0.23 + (double)(3 * _width) * 0.14);
            dragzonexmax5 = Convert.ToInt32((double)_width * 0.23 + (double)(4 * _width) * 0.14);
            dragzoneymin = (dragzoneymin2 = (dragzoneymin3 = (dragzoneymin4 = (dragzoneymin5 = Convert.ToInt32((double)_height * 0.78)))));
            dragzoneymax = (dragzoneymax2 = (dragzoneymax3 = (dragzoneymax4 = (dragzoneymax5 = Convert.ToInt32((double)_height * 0.8)))));
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
        if (e.X > dragzonexmin3 && e.X < dragzonexmax3 && e.Y > dragzoneymin3 && e.Y < dragzoneymax3)
        {
            dragableflag3 = true;
        }
        if (e.X > dragzonexmin4 && e.X < dragzonexmax4 && e.Y > dragzoneymin4 && e.Y < dragzoneymax4)
        {
            dragableflag4 = true;
        }
        if (e.X > dragzonexmin5 && e.X < dragzonexmax5 && e.Y > dragzoneymin5 && e.Y < dragzoneymax5)
        {
            dragableflag5 = true;
        }
        for (int i = 0; i < 5; i++)
        {
            int num = Convert.ToInt32((double)_width * 0.15 + (double)(i * _width) * 0.14);
            if (e.X > num + Convert.ToInt32((double)_width * 0.02) && e.X < num + Convert.ToInt32((double)_width * 0.1) && e.Y > Convert.ToInt32((double)_height * 0.12) && e.Y < Convert.ToInt32((double)_height * 0.2))
            {
                switch (i)
                {
                    case 0:
                        if (varname1 != "")
                        {
                            if (Convert.ToSingle(GetValue(varname1)) + changestep > maxval)
                            {
                                SetValue(varname1, maxval);
                            }
                            else
                            {
                                SetValue(varname1, Convert.ToSingle(GetValue(varname1)) + changestep);
                            }
                        }
                        else
                        {
                            Value += changestep;
                        }
                        break;
                    case 1:
                        if (varname2 != "")
                        {
                            if (Convert.ToSingle(GetValue(varname2)) + changestep > maxval)
                            {
                                SetValue(varname2, maxval);
                            }
                            else
                            {
                                SetValue(varname2, Convert.ToSingle(GetValue(varname2)) + changestep);
                            }
                        }
                        else
                        {
                            Value2 += changestep;
                        }
                        break;
                    case 2:
                        if (varname3 != "")
                        {
                            if (Convert.ToSingle(GetValue(varname3)) + changestep > maxval)
                            {
                                SetValue(varname3, maxval);
                            }
                            else
                            {
                                SetValue(varname3, Convert.ToSingle(GetValue(varname3)) + changestep);
                            }
                        }
                        else
                        {
                            Value3 += changestep;
                        }
                        break;
                    case 3:
                        if (varname4 != "")
                        {
                            if (Convert.ToSingle(GetValue(varname4)) + changestep > maxval)
                            {
                                SetValue(varname4, maxval);
                            }
                            else
                            {
                                SetValue(varname4, Convert.ToSingle(GetValue(varname4)) + changestep);
                            }
                        }
                        else
                        {
                            Value4 += changestep;
                        }
                        break;
                    case 4:
                        if (varname5 != "")
                        {
                            if (Convert.ToSingle(GetValue(varname5)) + changestep > maxval)
                            {
                                SetValue(varname5, maxval);
                            }
                            else
                            {
                                SetValue(varname5, Convert.ToSingle(GetValue(varname5)) + changestep);
                            }
                        }
                        else
                        {
                            Value5 += changestep;
                        }
                        break;
                }
                addbtndownflag = i + 1;
                NeedRefresh = true;
            }
            if (e.X <= num + Convert.ToInt32((double)_width * 0.02) || e.X >= num + Convert.ToInt32((double)_width * 0.1) || e.Y <= Convert.ToInt32((double)_height * 0.81) || e.Y >= Convert.ToInt32((double)_height * 0.89))
            {
                continue;
            }
            switch (i)
            {
                case 0:
                    if (varname1 != "")
                    {
                        if (Convert.ToSingle(GetValue(varname1)) - changestep < minval)
                        {
                            SetValue(varname1, minval);
                        }
                        else
                        {
                            SetValue(varname1, Convert.ToSingle(GetValue(varname1)) - changestep);
                        }
                    }
                    else
                    {
                        Value -= changestep;
                    }
                    break;
                case 1:
                    if (varname2 != "")
                    {
                        if (Convert.ToSingle(GetValue(varname2)) - changestep < minval)
                        {
                            SetValue(varname2, minval);
                        }
                        else
                        {
                            SetValue(varname2, Convert.ToSingle(GetValue(varname2)) - changestep);
                        }
                    }
                    else
                    {
                        Value2 -= changestep;
                    }
                    break;
                case 2:
                    if (varname3 != "")
                    {
                        if (Convert.ToSingle(GetValue(varname3)) - changestep < minval)
                        {
                            SetValue(varname3, minval);
                        }
                        else
                        {
                            SetValue(varname3, Convert.ToSingle(GetValue(varname3)) - changestep);
                        }
                    }
                    else
                    {
                        Value3 -= changestep;
                    }
                    break;
                case 3:
                    if (varname4 != "")
                    {
                        if (Convert.ToSingle(GetValue(varname4)) - changestep < minval)
                        {
                            SetValue(varname4, minval);
                        }
                        else
                        {
                            SetValue(varname4, Convert.ToSingle(GetValue(varname4)) - changestep);
                        }
                    }
                    else
                    {
                        Value4 -= changestep;
                    }
                    break;
                case 4:
                    if (varname5 != "")
                    {
                        if (Convert.ToSingle(GetValue(varname5)) - changestep < minval)
                        {
                            SetValue(varname5, minval);
                        }
                        else
                        {
                            SetValue(varname5, Convert.ToSingle(GetValue(varname5)) - changestep);
                        }
                    }
                    else
                    {
                        Value5 -= changestep;
                    }
                    break;
            }
            subbtndownflag = i + 1;
            NeedRefresh = true;
        }
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        dragableflag = false;
        dragableflag2 = false;
        dragableflag3 = false;
        dragableflag4 = false;
        dragableflag5 = false;
        addbtndownflag = 0;
        subbtndownflag = 0;
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
            if (varname1 != "")
            {
                SetValue(varname1, num);
            }
            else
            {
                Value = num;
            }
            NeedRefresh = true;
        }
        if (dragableflag2)
        {
            float num2 = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.6) + (double)minval);
            if (num2 > maxval)
            {
                num2 = maxval;
            }
            if (num2 < minval)
            {
                num2 = minval;
            }
            if (varname2 != "")
            {
                SetValue(varname2, num2);
            }
            else
            {
                Value = num2;
            }
            NeedRefresh = true;
        }
        if (dragableflag3)
        {
            float num3 = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.6) + (double)minval);
            if (num3 > maxval)
            {
                num3 = maxval;
            }
            if (num3 < minval)
            {
                num3 = minval;
            }
            if (varname3 != "")
            {
                SetValue(varname3, num3);
            }
            else
            {
                Value = num3;
            }
            NeedRefresh = true;
        }
        if (dragableflag4)
        {
            float num4 = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.6) + (double)minval);
            if (num4 > maxval)
            {
                num4 = maxval;
            }
            if (num4 < minval)
            {
                num4 = minval;
            }
            if (varname4 != "")
            {
                SetValue(varname4, num4);
            }
            else
            {
                Value = num4;
            }
            NeedRefresh = true;
        }
        if (dragableflag5)
        {
            float num5 = Convert.ToSingle(((double)_height * 0.8 - (double)e.Y) * (double)(maxval - minval) / ((double)_height * 0.6) + (double)minval);
            if (num5 > maxval)
            {
                num5 = maxval;
            }
            if (num5 < minval)
            {
                num5 = minval;
            }
            if (varname5 != "")
            {
                SetValue(varname5, num5);
            }
            else
            {
                Value = num5;
            }
            NeedRefresh = true;
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = ((!(varname1 != "")) ? null : GetValue(varname1));
            object obj2 = ((!(varname2 != "")) ? null : GetValue(varname2));
            object obj3 = ((!(varname3 != "")) ? null : GetValue(varname3));
            object obj4 = ((!(varname4 != "")) ? null : GetValue(varname4));
            object obj5 = ((!(varname5 != "")) ? null : GetValue(varname5));
            if (Height == 0f || Width == 0f)
            {
                _height = 400;
                _width = 400;
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
            if (obj3 != null)
            {
                Value3 = Convert.ToSingle(obj3);
            }
            if (obj4 != null)
            {
                Value4 = Convert.ToSingle(obj4);
            }
            if (obj5 != null)
            {
                Value5 = Convert.ToSingle(obj5);
            }
            initflag = 0;
            float[] array = new float[5];
            array[0] = (array[1] = (array[2] = (array[3] = (array[4] = minval))));
            try
            {
                if (Value > maxval)
                {
                    if (!highalertflag && HighAlert != null)
                    {
                        HighAlert(this, null);
                        highalertflag = true;
                    }
                    array[0] = maxval;
                }
                else if (Value < minval)
                {
                    if (!lowalertflag && LowAlert != null)
                    {
                        LowAlert(this, null);
                        lowalertflag = true;
                    }
                    array[0] = minval;
                }
                else
                {
                    highalertflag = false;
                    lowalertflag = false;
                    array[0] = Value;
                }
                if (Value2 > maxval)
                {
                    if (!highalertflag2 && HighAlert2 != null)
                    {
                        HighAlert2(this, null);
                        highalertflag2 = true;
                    }
                    array[1] = maxval;
                }
                else if (Value2 < minval)
                {
                    if (!lowalertflag2 && LowAlert2 != null)
                    {
                        LowAlert2(this, null);
                        lowalertflag2 = true;
                    }
                    array[1] = minval;
                }
                else
                {
                    highalertflag2 = false;
                    lowalertflag2 = false;
                    array[1] = Value2;
                }
                if (Value3 > maxval)
                {
                    if (!highalertflag3 && HighAlert3 != null)
                    {
                        HighAlert3(this, null);
                        highalertflag3 = true;
                    }
                    array[2] = maxval;
                }
                else if (Value3 < minval)
                {
                    if (!lowalertflag3 && LowAlert3 != null)
                    {
                        LowAlert3(this, null);
                        lowalertflag3 = true;
                    }
                    array[2] = minval;
                }
                else
                {
                    highalertflag3 = false;
                    lowalertflag3 = false;
                    array[2] = Value3;
                }
                if (Value4 > maxval)
                {
                    if (!highalertflag4 && HighAlert4 != null)
                    {
                        HighAlert4(this, null);
                        highalertflag4 = true;
                    }
                    array[3] = maxval;
                }
                else if (Value4 < minval)
                {
                    if (!lowalertflag4 && LowAlert4 != null)
                    {
                        LowAlert4(this, null);
                        lowalertflag4 = true;
                    }
                    array[3] = minval;
                }
                else
                {
                    highalertflag4 = false;
                    lowalertflag4 = false;
                    array[3] = Value4;
                }
                if (Value5 > maxval)
                {
                    if (!highalertflag5 && HighAlert5 != null)
                    {
                        HighAlert5(this, null);
                        highalertflag5 = true;
                    }
                    array[4] = maxval;
                }
                else if (Value5 < minval)
                {
                    if (!lowalertflag5 && LowAlert5 != null)
                    {
                        LowAlert5(this, null);
                        lowalertflag5 = true;
                    }
                    array[4] = minval;
                }
                else
                {
                    highalertflag5 = false;
                    lowalertflag5 = false;
                    array[4] = Value5;
                }
                Paint(graphics);
                List<SolidBrush> list = new();
                new List<SolidBrush>();
                list.Add(new SolidBrush(valuertcolor1));
                list.Add(new SolidBrush(valuertcolor2));
                list.Add(new SolidBrush(valuertcolor3));
                list.Add(new SolidBrush(valuertcolor4));
                list.Add(new SolidBrush(valuertcolor5));
                for (int i = 0; i < 5; i++)
                {
                    int num = Convert.ToInt32((double)_width * 0.15 + (double)(i * _width) * 0.14);
                    graphics.FillRectangle(rect: new Rectangle(num + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[i] - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.02)), brush: Brushes.Green);
                    graphics.FillRectangle(rect: new Rectangle(num + Convert.ToInt32((double)_width * 0.02), Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[i] - minval) / (double)(maxval - minval)), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.6 * (double)(array[i] - minval) / (double)(maxval - minval))), brush: list[i]);
                }
                dragzonexmin = Convert.ToInt32((double)_width * 0.15);
                dragzonexmin2 = Convert.ToInt32((double)_width * 0.15 + (double)_width * 0.14);
                dragzonexmin3 = Convert.ToInt32((double)_width * 0.15 + (double)(2 * _width) * 0.14);
                dragzonexmin4 = Convert.ToInt32((double)_width * 0.15 + (double)(3 * _width) * 0.14);
                dragzonexmin5 = Convert.ToInt32((double)_width * 0.15 + (double)(4 * _width) * 0.14);
                dragzonexmax = Convert.ToInt32((double)_width * 0.23);
                dragzonexmax2 = Convert.ToInt32((double)_width * 0.23 + (double)_width * 0.14);
                dragzonexmax3 = Convert.ToInt32((double)_width * 0.23 + (double)(2 * _width) * 0.14);
                dragzonexmax4 = Convert.ToInt32((double)_width * 0.23 + (double)(3 * _width) * 0.14);
                dragzonexmax5 = Convert.ToInt32((double)_width * 0.23 + (double)(4 * _width) * 0.14);
                dragzoneymin = Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[0] - minval) / (double)(maxval - minval));
                dragzoneymin2 = Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[1] - minval) / (double)(maxval - minval));
                dragzoneymin3 = Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[2] - minval) / (double)(maxval - minval));
                dragzoneymin4 = Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[3] - minval) / (double)(maxval - minval));
                dragzoneymin5 = Convert.ToInt32((double)_height * 0.78 - (double)_height * 0.6 * (double)(array[4] - minval) / (double)(maxval - minval));
                dragzoneymax = Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[0] - minval) / (double)(maxval - minval));
                dragzoneymax2 = Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[1] - minval) / (double)(maxval - minval));
                dragzoneymax3 = Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[2] - minval) / (double)(maxval - minval));
                dragzoneymax4 = Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[3] - minval) / (double)(maxval - minval));
                dragzoneymax5 = Convert.ToInt32((double)_height * 0.8 - (double)_height * 0.6 * (double)(array[4] - minval) / (double)(maxval - minval));
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
        YouBiao3 YouBiao = new();
        if (!string.IsNullOrEmpty(varname1))
        {
            YouBiao.varname1 = varname1.Substring(1, varname1.Length - 2);
        }
        if (!string.IsNullOrEmpty(varname2))
        {
            YouBiao.varname2 = varname2.Substring(1, varname2.Length - 2);
        }
        if (!string.IsNullOrEmpty(varname3))
        {
            YouBiao.varname3 = varname3.Substring(1, varname3.Length - 2);
        }
        if (!string.IsNullOrEmpty(varname4))
        {
            YouBiao.varname4 = varname4.Substring(1, varname4.Length - 2);
        }
        if (!string.IsNullOrEmpty(varname5))
        {
            YouBiao.varname5 = varname5.Substring(1, varname5.Length - 2);
        }
        YouBiao.changestep = changestep;
        YouBiao.mainmark = mainmark;
        YouBiao.othermark = othermark;
        YouBiao.maxval = maxval;
        YouBiao.minval = minval;
        YouBiao.txt1 = txt1;
        YouBiao.txt2 = txt2;
        YouBiao.txt3 = txt3;
        YouBiao.txt4 = txt4;
        YouBiao.txt5 = txt5;
        YouBiao.txtcolor1 = valuertcolor1;
        YouBiao.txtcolor2 = valuertcolor2;
        YouBiao.txtcolor3 = valuertcolor3;
        YouBiao.txtcolor4 = valuertcolor4;
        YouBiao.txtcolor5 = valuertcolor5;
        YouBiao.bgcolor1 = bgcolor1;
        YouBiao.bgcolor2 = bgcolor2;
        YouBiao.bgcolor3 = bgcolor3;
        YouBiao.bgcolor4 = bgcolor4;
        YouBiao.bgcolor5 = bgcolor5;
        YouBiao.viewevent += GetTable;
        YouBiao.ckvarevent += CheckVar;
        if (YouBiao.ShowDialog() == DialogResult.OK)
        {
            if (YouBiao.varname1 != "")
            {
                varname1 = "[" + YouBiao.varname1 + "]";
            }
            else
            {
                varname1 = "";
            }
            if (YouBiao.varname2 != "")
            {
                varname2 = "[" + YouBiao.varname2 + "]";
            }
            else
            {
                varname2 = "";
            }
            if (YouBiao.varname3 != "")
            {
                varname3 = "[" + YouBiao.varname3 + "]";
            }
            else
            {
                varname3 = "";
            }
            if (YouBiao.varname4 != "")
            {
                varname4 = "[" + YouBiao.varname4 + "]";
            }
            else
            {
                varname4 = "";
            }
            if (YouBiao.varname5 != "")
            {
                varname5 = "[" + YouBiao.varname5 + "]";
            }
            else
            {
                varname5 = "";
            }
            txt1 = YouBiao.txt1;
            txt2 = YouBiao.txt2;
            txt3 = YouBiao.txt3;
            txt4 = YouBiao.txt4;
            txt5 = YouBiao.txt5;
            mainmark = YouBiao.mainmark;
            othermark = YouBiao.othermark;
            maxval = YouBiao.maxval;
            minval = YouBiao.minval;
            valuertcolor1 = YouBiao.txtcolor1;
            valuertcolor2 = YouBiao.txtcolor2;
            valuertcolor3 = YouBiao.txtcolor3;
            valuertcolor4 = YouBiao.txtcolor4;
            valuertcolor5 = YouBiao.txtcolor5;
            bgcolor1 = YouBiao.bgcolor1;
            bgcolor2 = YouBiao.bgcolor2;
            bgcolor3 = YouBiao.bgcolor3;
            bgcolor4 = YouBiao.bgcolor4;
            bgcolor5 = YouBiao.bgcolor5;
            changestep = YouBiao.changestep;
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
        BSaveC4 bSaveC = new()
        {
            varname1 = varname1,
            varname2 = varname2,
            varname3 = varname3,
            varname4 = varname4,
            varname5 = varname5,
            txt1 = txt1,
            txt2 = txt2,
            txt3 = txt3,
            txt4 = txt4,
            txt5 = txt5,
            txtcolor1 = valuertcolor1,
            txtcolor2 = valuertcolor2,
            txtcolor3 = valuertcolor3,
            txtcolor4 = valuertcolor4,
            txtcolor5 = valuertcolor5,
            bgcolor1 = bgcolor1,
            bgcolor2 = bgcolor2,
            bgcolor3 = bgcolor3,
            bgcolor4 = bgcolor4,
            bgcolor5 = bgcolor5,
            mainmark = mainmark,
            othermark = othermark,
            maxval = maxval,
            minval = minval
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
            BSaveC4 bSaveC = (BSaveC4)formatter.Deserialize(stream);
            stream.Close();
            varname1 = bSaveC.varname1;
            varname2 = bSaveC.varname2;
            varname3 = bSaveC.varname3;
            varname4 = bSaveC.varname4;
            varname5 = bSaveC.varname5;
            txt1 = bSaveC.txt1;
            txt2 = bSaveC.txt2;
            txt3 = bSaveC.txt3;
            txt4 = bSaveC.txt4;
            txt5 = bSaveC.txt5;
            valuertcolor1 = bSaveC.txtcolor1;
            valuertcolor2 = bSaveC.txtcolor2;
            valuertcolor3 = bSaveC.txtcolor3;
            valuertcolor4 = bSaveC.txtcolor4;
            valuertcolor5 = bSaveC.txtcolor5;
            bgcolor1 = bSaveC.bgcolor1;
            bgcolor2 = bSaveC.bgcolor2;
            bgcolor3 = bSaveC.bgcolor3;
            bgcolor4 = bSaveC.bgcolor4;
            bgcolor5 = bSaveC.bgcolor5;
            mainmark = bSaveC.mainmark;
            othermark = bSaveC.othermark;
            maxval = bSaveC.maxval;
            minval = bSaveC.minval;
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
