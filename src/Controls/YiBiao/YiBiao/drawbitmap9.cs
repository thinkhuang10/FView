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

namespace YiBiao;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap9 : CPixieControl
{
    private double d_ToBeFilledNum;

    private string varname = "";

    private int intcount = 4;

    private int i_DecimalPointCount = 2;

    private bool flag1;

    private bool colortransflag;

    private bool offtrans;

    private Color bgcolor = Color.Black;

    private Color oncolor = Color.FromArgb(255, 128, 255, 0);

    private Color offcolor = Color.FromArgb(255, 43, 65, 22);

    private Color offcolorbuf = Color.LightGray;

    private readonly int initflag = 1;

    private int weith = 150;

    private int hight = 50;

    [NonSerialized]
    private readonly bool value;

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("控件绑定变量名。")]
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

    [Description("数码管显示的整数位数。")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [DisplayName("整数位数")]
    [ReadOnly(false)]
    public int IntCount
    {
        get
        {
            return intcount;
        }
        set
        {
            intcount = value;
        }
    }

    [DisplayName("小位数")]
    [Category("杂项")]
    [Description("数码管显示的小数位数。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public int DecimalCount
    {
        get
        {
            return i_DecimalPointCount;
        }
        set
        {
            i_DecimalPointCount = value;
        }
    }

    [Description("数码管背景颜色。")]
    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Category("杂项")]
    [DisplayName("背景色")]
    public Color BackColor
    {
        get
        {
            return bgcolor;
        }
        set
        {
            bgcolor = value;
        }
    }

    [DisplayName("灯亮颜色")]
    [Description("数码管显示数字时数字部分的颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    public Color Oncolor
    {
        get
        {
            return oncolor;
        }
        set
        {
            oncolor = value;
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("灯灭颜色")]
    [Category("杂项")]
    [Description("数码管显示数字时未用于数字部分的颜色。")]
    public Color Offcolor
    {
        get
        {
            return offcolor;
        }
        set
        {
            offcolor = value;
            offcolorbuf = value;
        }
    }

    [Description("数码管显示的绑定变量的当前值。")]
    [DisplayName("当前值")]
    [ReadOnly(false)]
    [Category("杂项")]
    public double Value
    {
        get
        {
            return d_ToBeFilledNum;
        }
        set
        {
            d_ToBeFilledNum = value;
            NeedRefresh = true;
        }
    }

    public drawbitmap9()
    {
    }

    protected drawbitmap9(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap9 info");
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
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "bgcolor")
            {
                bgcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "colortransflag")
            {
                colortransflag = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "d_ToBeFilledNum")
            {
                d_ToBeFilledNum = (double)enumerator.Value;
            }
            else if (enumerator.Name == "flag1")
            {
                flag1 = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "hight")
            {
                hight = (int)enumerator.Value;
            }
            else if (enumerator.Name == "i_DecimalPointCount")
            {
                i_DecimalPointCount = (int)enumerator.Value;
            }
            else if (enumerator.Name == "initflag")
            {
                initflag = (int)enumerator.Value;
            }
            else if (enumerator.Name == "intcount")
            {
                intcount = (int)enumerator.Value;
            }
            else if (enumerator.Name == "offcolor")
            {
                offcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "offcolorbuf")
            {
                offcolorbuf = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "oncolor")
            {
                oncolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "value")
            {
                value = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "varname")
            {
                varname = (string)enumerator.Value;
            }
            else if (enumerator.Name == "weith")
            {
                weith = (int)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("bgcolor", bgcolor);
        info.AddValue("colortransflag", colortransflag);
        info.AddValue("offtrans", offtrans);
        info.AddValue("d_ToBeFilledNum", d_ToBeFilledNum);
        info.AddValue("flag1", flag1);
        info.AddValue("hight", hight);
        info.AddValue("i_DecimalPointCount", i_DecimalPointCount);
        info.AddValue("initflag", initflag);
        info.AddValue("intcount", intcount);
        info.AddValue("offcolor", offcolor);
        info.AddValue("offcolorbuf", offcolorbuf);
        info.AddValue("oncolor", oncolor);
        info.AddValue("value", value);
        info.AddValue("varname", varname);
        info.AddValue("weith", weith);
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
        drawbitmap9 drawbitmap10 = (drawbitmap9)base.Copy();
        drawbitmap10.varname = varname;
        drawbitmap10.oncolor = oncolor;
        drawbitmap10.offcolor = offcolor;
        drawbitmap10.intcount = intcount;
        drawbitmap10.i_DecimalPointCount = i_DecimalPointCount;
        drawbitmap10.bgcolor = bgcolor;
        drawbitmap10.flag1 = flag1;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        SolidBrush brush = new(bgcolor);
        Rectangle rect = new(0, 0, weith, hight);
        g.FillRectangle(brush, rect);
        SolidBrush brush2 = new(oncolor);
        SolidBrush brush3 = new(offcolor);
        string text3 = "F" + i_DecimalPointCount;
        d_ToBeFilledNum = Math.Round(d_ToBeFilledNum, i_DecimalPointCount);
        string text;
        string text2;
        if (d_ToBeFilledNum.ToString(text3).IndexOf(".") != -1)
        {
            int num = d_ToBeFilledNum.ToString(text3).IndexOf(".");
            text = d_ToBeFilledNum.ToString(text3).Substring(0, num);
            text2 = d_ToBeFilledNum.ToString(text3).Substring(num + 1, d_ToBeFilledNum.ToString(text3).Length - num - 1);
        }
        else
        {
            text = d_ToBeFilledNum.ToString(text3);
            text2 = "0000";
        }
        char[] array = ((!flag1) ? new char[intcount] : new char[intcount + 1]);
        char[] array2 = new char[i_DecimalPointCount];
        char[] array3 = text.ToCharArray(0, text.Length);
        int num2 = array3.Length - 1;
        int num3 = array.Length - 1;
        while (num2 >= 0 && num3 >= 0)
        {
            array[num3] = array3[num2];
            num2--;
            num3--;
        }
        char[] array4 = text2.ToCharArray(0, text2.Length);
        int num4 = 0;
        int num5 = 0;
        while (num4 < array4.Length && num5 < array2.Length)
        {
            array2[num5] = array4[num4];
            num4++;
            num5++;
        }
        int num6 = Convert.ToInt32((double)weith * 0.1);
        int num7 = ((!flag1) ? intcount : (intcount + 1));
        int num8 = i_DecimalPointCount;
        int num9 = Convert.ToInt32((double)weith * 0.8 / (double)(num7 + i_DecimalPointCount));
        int num10 = Convert.ToInt32(num9 / 6);
        int num11 = Convert.ToInt32(((double)hight * 0.8 - (double)(3 * num10)) / 2.0);
        for (int num12 = num7 - 1; num12 >= 0; num12--)
        {
            string text4;
            if (!flag1 || num12 != 0)
            {
                text4 = array[num12].ToString() switch
                {
                    "1" => "0010010",
                    "2" => "1011101",
                    "3" => "1011011",
                    "4" => "0111010",
                    "5" => "1101011",
                    "6" => "1101111",
                    "7" => "1010010",
                    "8" => "1111111",
                    "9" => "1111011",
                    "0" => "1110111",
                    _ => "0000000",
                };
            }
            else
            {
                object obj = GetValue(varname);
                double num13 = Convert.ToDouble(obj);
                text4 = ((!(num13 < 0.0)) ? "0000000" : "0001000");
            }
            int num14 = num6 + num12 * num9;
            Point[] points = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2))),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1)),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2))),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10))
            };
            if (text4.ToCharArray()[0].ToString() == "1")
            {
                g.FillPolygon(brush2, points);
            }
            else
            {
                g.FillPolygon(brush3, points);
            }
            Point[] points2 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num14), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num14), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2))
            };
            if (text4.ToCharArray()[1].ToString() == "1")
            {
                g.FillPolygon(brush2, points2);
            }
            else
            {
                g.FillPolygon(brush3, points2);
            }
            Point[] points3 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num14 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2))
            };
            if (text4.ToCharArray()[2].ToString() == "1")
            {
                g.FillPolygon(brush2, points3);
            }
            else
            {
                g.FillPolygon(brush3, points3);
            }
            Point[] points4 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10))
            };
            if (text4.ToCharArray()[3].ToString() == "1")
            {
                g.FillPolygon(brush2, points4);
            }
            else
            {
                g.FillPolygon(brush3, points4);
            }
            Point[] points5 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10))
            };
            if (text4.ToCharArray()[4].ToString() == "1")
            {
                g.FillPolygon(brush2, points5);
            }
            else
            {
                g.FillPolygon(brush3, points5);
            }
            Point[] points6 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num14 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10))
            };
            if (text4.ToCharArray()[5].ToString() == "1")
            {
                g.FillPolygon(brush2, points6);
            }
            else
            {
                g.FillPolygon(brush3, points6);
            }
            Point[] points7 = new Point[6]
            {
                new(Convert.ToInt32((double)num14 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num14 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num14 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10 + (double)num11 + (double)num10))
            };
            if (text4.ToCharArray()[6].ToString() == "1")
            {
                g.FillPolygon(brush2, points7);
            }
            else
            {
                g.FillPolygon(brush3, points7);
            }
        }
        if (i_DecimalPointCount != 0)
        {
            int x = Convert.ToInt32((double)weith * 0.1 + (double)(num7 * num9) - (double)num10 * 0.9);
            int y = Convert.ToInt32((double)hight * 0.1 + (double)(num11 * 2) + (double)num10 * 2.5);
            Rectangle rect2 = new(x, y, Convert.ToInt32(num10), Convert.ToInt32(num10));
            g.FillPie(brush2, rect2, 0f, 360f);
        }
        for (int i = 0; i < num8; i++)
        {
            string text5 = array2[i].ToString() switch
            {
                "1" => "0010010",
                "2" => "1011101",
                "3" => "1011011",
                "4" => "0111010",
                "5" => "1101011",
                "6" => "1101111",
                "7" => "1010010",
                "8" => "1111111",
                "9" => "1111011",
                "0" => "1110111",
                _ => "0000000",
            };
            int num15 = num6 + (i + num7) * num9;
            Point[] points8 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2))),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1)),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2))),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10))
            };
            if (text5.ToCharArray()[0].ToString() == "1")
            {
                g.FillPolygon(brush2, points8);
            }
            else
            {
                g.FillPolygon(brush3, points8);
            }
            Point[] points9 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num15), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num15), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2))
            };
            if (text5.ToCharArray()[1].ToString() == "1")
            {
                g.FillPolygon(brush2, points9);
            }
            else
            {
                g.FillPolygon(brush3, points9);
            }
            Point[] points10 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2)),
                new(Convert.ToInt32(num15 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2))
            };
            if (text5.ToCharArray()[2].ToString() == "1")
            {
                g.FillPolygon(brush2, points10);
            }
            else
            {
                g.FillPolygon(brush3, points10);
            }
            Point[] points11 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10))
            };
            if (text5.ToCharArray()[3].ToString() == "1")
            {
                g.FillPolygon(brush2, points11);
            }
            else
            {
                g.FillPolygon(brush3, points11);
            }
            Point[] points12 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10))
            };
            if (text5.ToCharArray()[4].ToString() == "1")
            {
                g.FillPolygon(brush2, points12);
            }
            else
            {
                g.FillPolygon(brush3, points12);
            }
            Point[] points13 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + num10 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)(4 * num10)), Convert.ToInt32((double)hight * 0.1 + (double)num10 * 1.5 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 - (double)num10 * 0.2 + (double)num11 + (double)num10)),
                new(Convert.ToInt32(num15 + 4 * num10), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num10 * 0.2 + (double)num11 + (double)num10))
            };
            if (text5.ToCharArray()[5].ToString() == "1")
            {
                g.FillPolygon(brush2, points13);
            }
            else
            {
                g.FillPolygon(brush3, points13);
            }
            Point[] points14 = new Point[6]
            {
                new(Convert.ToInt32((double)num15 + (double)num10 * 0.5 + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)num15 + (double)num10 * 4.5 - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)(num10 / 2) + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10 * 4) - (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10 + (double)num11 + (double)num10)),
                new(Convert.ToInt32((double)(num15 + num10) + (double)num10 * 0.2), Convert.ToInt32((double)hight * 0.1 + (double)num10 + (double)num11 + (double)num10 + (double)num11 + (double)num10))
            };
            if (text5.ToCharArray()[6].ToString() == "1")
            {
                g.FillPolygon(brush2, points14);
            }
            else
            {
                g.FillPolygon(brush3, points14);
            }
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
            if (Height == 0f || Width == 0f)
            {
                hight = 66;
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
            if (obj != null)
            {
                Value = Convert.ToSingle(obj);
            }
            try
            {
                Paint(graphics);
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
        ViewDataSet viewDataSet = new();
        if (!string.IsNullOrEmpty(varname))
        {
            viewDataSet.varname = varname.Substring(1, varname.Length - 2);
            viewDataSet.oncolor = oncolor;
            viewDataSet.offcolor = offcolor;
            viewDataSet.intcount = intcount;
            viewDataSet.dbcount = i_DecimalPointCount;
            viewDataSet.bgcolor = bgcolor;
            viewDataSet.flag1 = flag1;
            viewDataSet.colortranflag = colortransflag;
            viewDataSet.offcolortran = offtrans;
        }
        viewDataSet.viewevent += GetTable;
        viewDataSet.ckvarevent += CheckVar;
        if (viewDataSet.ShowDialog() == DialogResult.OK)
        {
            varname = "[" + viewDataSet.varname + "]";
            oncolor = viewDataSet.oncolor;
            offcolor = viewDataSet.offcolor;
            intcount = viewDataSet.intcount;
            i_DecimalPointCount = viewDataSet.dbcount;
            bgcolor = viewDataSet.bgcolor;
            flag1 = viewDataSet.flag1;
            colortransflag = viewDataSet.colortranflag;
            offtrans = viewDataSet.offcolortran;
            offcolorbuf = viewDataSet.offcolor;
            if (colortransflag)
            {
                bgcolor = Color.Transparent;
            }
            if (offtrans)
            {
                offcolor = Color.Transparent;
            }
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveC9 bSaveC = new()
        {
            varname = varname,
            intcount = intcount,
            dbcount = i_DecimalPointCount,
            oncolor = oncolor,
            offcolor = offcolor,
            bgcolor = bgcolor,
            flag1 = flag1,
            colortransflag = colortransflag,
            offtrans = offtrans
        };
        formatter.Serialize(memoryStream, bSaveC);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public override void Deserialize(byte[] data)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream(data);
        BSaveC9 bSaveC = (BSaveC9)formatter.Deserialize(stream);
        stream.Close();
        varname = bSaveC.varname;
        oncolor = bSaveC.oncolor;
        offcolor = bSaveC.offcolor;
        bgcolor = bSaveC.bgcolor;
        intcount = bSaveC.intcount;
        i_DecimalPointCount = bSaveC.dbcount;
        flag1 = bSaveC.flag1;
        colortransflag = bSaveC.colortransflag;
        offtrans = bSaveC.offtrans;
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
