using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using CommonSnappableTypes;
using SetsForms;
using ShapeRuntime;

namespace Timer_1;

[Serializable]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap2 : CPixieControl
{
    private int wth = 200;

    private int hgt = 200;

    private Color bgcolor = Color.Blue;

    private Color timercolor = Color.White;

    private int fonttype = 1;

    private readonly int initflag = 1;

    private readonly bool value;

    [NonSerialized]
    private Timer tm1;

    [DHMICtrlProperty]
    [DisplayName("边缘颜色")]
    [Category("杂项")]
    [Description("边缘颜色。")]
    [ReadOnly(false)]
    public Color BorderColor
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

    [DHMICtrlProperty]
    [Description("背景颜色。")]
    [ReadOnly(false)]
    [DisplayName("背景色")]
    [Category("杂项")]
    public Color BackColor
    {
        get
        {
            return timercolor;
        }
        set
        {
            timercolor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("数字类型")]
    [DHMICtrlProperty]
    [Description("可以设置为阿拉伯数字或罗马数字。")]
    [Category("杂项")]
    public string NumberType
    {
        get
        {
            if (fonttype == 1)
            {
                return "阿拉伯数字";
            }
            return "罗马数字";
        }
        set
        {
            if (value.ToString() == "阿拉伯数字")
            {
                fonttype = 1;
            }
            else if (value.ToString() == "罗马数字")
            {
                fonttype = 2;
            }
            else
            {
                fonttype = 1;
            }
        }
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
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "bgcolor")
            {
                bgcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "fonttype")
            {
                fonttype = (int)enumerator.Value;
            }
            else if (enumerator.Name == "hgt")
            {
                hgt = (int)enumerator.Value;
            }
            else if (enumerator.Name == "initflag")
            {
                initflag = (int)enumerator.Value;
            }
            else if (enumerator.Name == "timercolor")
            {
                timercolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "value")
            {
                value = (bool)enumerator.Value;
            }
            else if (enumerator.Name == "wth")
            {
                wth = (int)enumerator.Value;
            }
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("bgcolor", bgcolor);
        info.AddValue("fonttype", fonttype);
        info.AddValue("hgt", hgt);
        info.AddValue("initflag", initflag);
        info.AddValue("timercolor", timercolor);
        info.AddValue("value", value);
        info.AddValue("wth", wth);
    }

    public drawbitmap2()
    {
    }

    public override CShape Copy()
    {
        drawbitmap2 drawbitmap3 = (drawbitmap2)base.Copy();
        drawbitmap3.bgcolor = bgcolor;
        drawbitmap3.timercolor = timercolor;
        drawbitmap3.fonttype = fonttype;
        return drawbitmap3;
    }

    public void Paint(Graphics g)
    {
        FontFamily family = new("Times New Roman");
        Font font = new(family, Convert.ToInt32((double)wth * 0.06));
        Point pt = new(Convert.ToInt32((double)wth * 0.5), Convert.ToInt32((double)hgt * 0.5));
        Rectangle rect = new(0, 0, wth, hgt);
        SolidBrush brush = new(bgcolor);
        SolidBrush brush2 = new(timercolor);
        g.FillPie(brush, rect, 0f, 360f);
        Rectangle rect2 = new(Convert.ToInt32((double)wth * 0.1), Convert.ToInt32((double)hgt * 0.1), Convert.ToInt32((double)wth * 0.8), Convert.ToInt32((double)hgt * 0.8));
        g.FillPie(brush2, rect2, 0f, 360f);
        for (int i = 0; i < 12; i++)
        {
            if (i * 30 > 0 && i * 30 < 90)
            {
                int x = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.4 * Math.Cos(Math.PI * (double)i * 30.0 / 180.0));
                int y = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.4 * Math.Sin(Math.PI * (double)i * 30.0 / 180.0));
                g.DrawLine(pt2: new Point(x, y), pen: Pens.Black, pt1: pt);
            }
            else if (i * 30 > 90 && i * 30 < 180)
            {
                int x2 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.4 * Math.Cos(Math.PI * (double)(i * 30 - 90) / 180.0));
                int y2 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.4 * Math.Sin(Math.PI * (double)(i * 30 - 90) / 180.0));
                g.DrawLine(pt2: new Point(x2, y2), pen: Pens.Black, pt1: pt);
            }
            else if (i * 30 > 180 && i * 30 < 270)
            {
                int x3 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.4 * Math.Cos(Math.PI * (double)(i * 30 - 180) / 180.0));
                int y3 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.4 * Math.Sin(Math.PI * (double)(i * 30 - 180) / 180.0));
                g.DrawLine(pt2: new Point(x3, y3), pen: Pens.Black, pt1: pt);
            }
            else if (i * 30 > 270 && i * 30 < 360)
            {
                int x4 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.4 * Math.Cos(Math.PI * (double)(i * 30 - 270) / 180.0));
                int y4 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.4 * Math.Sin(Math.PI * (double)(i * 30 - 270) / 180.0));
                g.DrawLine(pt2: new Point(x4, y4), pen: Pens.Black, pt1: pt);
            }
        }
        Rectangle rect3 = new(Convert.ToInt32((double)wth * 0.2), Convert.ToInt32((double)hgt * 0.2), Convert.ToInt32((double)wth * 0.6), Convert.ToInt32((double)hgt * 0.6));
        g.FillPie(brush2, rect3, 0f, 360f);
        g.FillPie(rect: new Rectangle(Convert.ToInt32((double)wth * 0.48), Convert.ToInt32((double)hgt * 0.48), Convert.ToInt32((double)wth * 0.04), Convert.ToInt32((double)hgt * 0.04)), brush: Brushes.Blue, startAngle: 0f, sweepAngle: 360f);
        Point point = new(Convert.ToInt32((double)wth * 0.8), Convert.ToInt32((double)hgt * 0.47));
        Point point2 = new(Convert.ToInt32((double)wth * 0.47), Convert.ToInt32((double)hgt * 0.8));
        Point point3 = new(Convert.ToInt32((double)wth * 0.11), Convert.ToInt32((double)hgt * 0.48));
        Point point4 = new(Convert.ToInt32((double)wth * 0.45), Convert.ToInt32((double)hgt * 0.12));
        if (fonttype == 1)
        {
            g.DrawString("3", font, Brushes.Black, point);
            g.DrawString("6", font, Brushes.Black, point2);
            g.DrawString("9", font, Brushes.Black, point3);
            g.DrawString("12", font, Brushes.Black, point4);
        }
        else
        {
            g.DrawString("III", font, Brushes.Black, point);
            g.DrawString("VI", font, Brushes.Black, point2);
            g.DrawString("IX", font, Brushes.Black, point3);
            g.DrawString("XII", font, Brushes.Black, point4);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            if (Height == 0f || Width == 0f)
            {
                hgt = 200;
                wth = 200;
            }
            else
            {
                hgt = Convert.ToInt32(Math.Abs(Height));
                wth = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(wth, hgt);
            Graphics graphics = Graphics.FromImage(bitmap);
            Point pt = new(Convert.ToInt32((double)wth * 0.5), Convert.ToInt32((double)hgt * 0.5));
            Paint(graphics);
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            double num = hour % 12;
            int minute = now.Minute;
            _ = minute / 60;
            num = (num * 60.0 + (double)minute) / 60.0;
            int second = now.Second;
            Pen pen = new(Color.Blue, 2f);
            Pen pen2 = new(Color.Blue, 2f);
            Pen pen3 = new(Color.Red, 2f);
            if (num >= 0.0 && num <= 3.0)
            {
                int x = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.3 * Math.Sin(num * Math.PI / 6.0));
                int y = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.3 * Math.Cos(num * Math.PI / 6.0));
                Point pt2 = new(x, y);
                graphics.DrawLine(pen, pt, pt2);
            }
            if (num > 3.0 && num <= 6.0)
            {
                int x2 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.3 * Math.Cos((num - 3.0) * Math.PI / 6.0));
                int y2 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.3 * Math.Sin((num - 3.0) * Math.PI / 6.0));
                Point pt3 = new(x2, y2);
                graphics.DrawLine(pen, pt, pt3);
            }
            if (num > 6.0 && num <= 9.0)
            {
                int x3 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.3 * Math.Sin((num - 6.0) * Math.PI / 6.0));
                int y3 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.3 * Math.Cos((num - 6.0) * Math.PI / 6.0));
                Point pt4 = new(x3, y3);
                graphics.DrawLine(pen, pt, pt4);
            }
            if (num > 9.0 && num <= 12.0)
            {
                int x4 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.3 * Math.Cos((num - 9.0) * Math.PI / 6.0));
                int y4 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.3 * Math.Sin((num - 9.0) * Math.PI / 6.0));
                Point pt5 = new(x4, y4);
                graphics.DrawLine(pen, pt, pt5);
            }
            if (minute >= 0 && minute <= 15)
            {
                int x5 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.37 * Math.Sin((double)minute * Math.PI / 30.0));
                int y5 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.37 * Math.Cos((double)minute * Math.PI / 30.0));
                Point pt6 = new(x5, y5);
                graphics.DrawLine(pen2, pt, pt6);
            }
            if (minute > 15 && minute <= 30)
            {
                int x6 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.37 * Math.Cos((double)(minute - 15) * Math.PI / 30.0));
                int y6 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.37 * Math.Sin((double)(minute - 15) * Math.PI / 30.0));
                Point pt7 = new(x6, y6);
                graphics.DrawLine(pen2, pt, pt7);
            }
            if (minute > 30 && minute <= 45)
            {
                int x7 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.37 * Math.Sin((double)(minute - 30) * Math.PI / 30.0));
                int y7 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.37 * Math.Cos((double)(minute - 30) * Math.PI / 30.0));
                Point pt8 = new(x7, y7);
                graphics.DrawLine(pen2, pt, pt8);
            }
            if (minute > 45 && minute <= 60)
            {
                int x8 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.37 * Math.Cos((double)(minute - 45) * Math.PI / 30.0));
                int y8 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.37 * Math.Sin((double)(minute - 45) * Math.PI / 30.0));
                Point pt9 = new(x8, y8);
                graphics.DrawLine(pen2, pt, pt9);
            }
            if (second >= 0 && second <= 15)
            {
                int x9 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.37 * Math.Sin((double)second * Math.PI / 30.0));
                int y9 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.37 * Math.Cos((double)second * Math.PI / 30.0));
                Point pt10 = new(x9, y9);
                graphics.DrawLine(pen3, pt, pt10);
            }
            if (second > 15 && second <= 30)
            {
                int x10 = Convert.ToInt32((double)wth * 0.5 + (double)wth * 0.37 * Math.Cos((double)(second - 15) * Math.PI / 30.0));
                int y10 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.37 * Math.Sin((double)(second - 15) * Math.PI / 30.0));
                Point pt11 = new(x10, y10);
                graphics.DrawLine(pen3, pt, pt11);
            }
            if (second > 30 && second <= 45)
            {
                int x11 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.37 * Math.Sin((double)(second - 30) * Math.PI / 30.0));
                int y11 = Convert.ToInt32((double)hgt * 0.5 + (double)hgt * 0.37 * Math.Cos((double)(second - 30) * Math.PI / 30.0));
                Point pt12 = new(x11, y11);
                graphics.DrawLine(pen3, pt, pt12);
            }
            if (second > 45 && second <= 60)
            {
                int x12 = Convert.ToInt32((double)wth * 0.5 - (double)wth * 0.37 * Math.Cos((double)(second - 45) * Math.PI / 30.0));
                int y12 = Convert.ToInt32((double)hgt * 0.5 - (double)hgt * 0.37 * Math.Sin((double)(second - 45) * Math.PI / 30.0));
                Point pt13 = new(x12, y12);
                graphics.DrawLine(pen3, pt, pt13);
            }
            FinishRefresh(bitmap);
        }
        catch
        {
        }
    }

    public override void ShowDialog()
    {
        TimerSet timerSet = new()
        {
            bgcolor = bgcolor,
            timercolor = timercolor,
            fonttype = fonttype
        };
        if (timerSet.ShowDialog() == DialogResult.OK)
        {
            bgcolor = timerSet.bgcolor;
            timercolor = timerSet.timercolor;
            fonttype = timerSet.fonttype;
            NeedRefresh = true;
        }
    }

    public void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSave2 bSave = new()
        {
            bgcolor = bgcolor,
            timercolor = timercolor,
            fonttype = fonttype
        };
        formatter.Serialize(memoryStream, bSave);
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
            bgcolor = bSave.bgcolor;
            timercolor = bSave.timercolor;
            fonttype = bSave.fonttype;
            tm1 = new Timer();
            tm1.Tick += tm1_Tick;
            tm1.Interval = 1000;
            tm1.Enabled = true;
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
