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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap : CPixieControl
{
    private int wth = 100;

    private int hgt = 30;

    private Color bgcolor = Color.Yellow;

    private Color timercolor = Color.Black;

    private Color fontcolor = Color.Green;

    private int initflag = 1;

    private readonly bool value;

    [NonSerialized]
    private Timer tm1;

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("边缘颜色。")]
    [DisplayName("边缘颜色")]
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

    [Category("杂项")]
    [DisplayName("背景色")]
    [DHMICtrlProperty]
    [Description("背景颜色。")]
    [ReadOnly(false)]
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

    [DisplayName("字体颜色")]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("字体颜色。")]
    [ReadOnly(false)]
    public Color NumberColor
    {
        get
        {
            return fontcolor;
        }
        set
        {
            fontcolor = value;
        }
    }

    public drawbitmap()
    {
    }

    protected drawbitmap(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap2 info");
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
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "bgcolor")
            {
                bgcolor = (Color)enumerator.Value;
            }
            else if (enumerator.Name == "fontcolor")
            {
                fontcolor = (Color)enumerator.Value;
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
        info.AddValue("fontcolor", fontcolor);
        info.AddValue("hgt", hgt);
        info.AddValue("initflag", initflag);
        info.AddValue("timercolor", timercolor);
        info.AddValue("value", value);
        info.AddValue("wth", wth);
    }

    public override CShape Copy()
    {
        drawbitmap drawbitmap3 = (drawbitmap)base.Copy();
        drawbitmap3.bgcolor = bgcolor;
        drawbitmap3.timercolor = timercolor;
        drawbitmap3.fontcolor = fontcolor;
        return drawbitmap3;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 0, wth, hgt);
        Rectangle rect2 = new(Convert.ToInt32((double)wth * 0.05), Convert.ToInt32((double)hgt * 0.05), Convert.ToInt32((double)wth * 0.9), Convert.ToInt32((double)hgt * 0.9));
        SolidBrush brush = new(bgcolor);
        SolidBrush brush2 = new(timercolor);
        SolidBrush brush3 = new(fontcolor);
        g.FillRectangle(brush, rect);
        g.FillRectangle(brush2, rect2);
        if (initflag == 1)
        {
            string s = "12:00:00";
            FontFamily family = new("Times New Roman");
            Font font = new(family, Convert.ToInt32((double)wth * 0.16));
            Point point = new(Convert.ToInt32((double)wth * 0.05), Convert.ToInt32((double)hgt * 0.05));
            g.DrawString(s, font, brush3, point);
        }
    }

    public override void RefreshControl()
    {
        try
        {
            if (Height == 0f || Width == 0f)
            {
                hgt = 60;
                wth = 200;
            }
            else
            {
                hgt = Convert.ToInt32(Math.Abs(Height));
                wth = Convert.ToInt32(Math.Abs(Width));
            }
            Bitmap bitmap = new(wth, hgt);
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new(fontcolor);
            initflag = 0;
            Paint(graphics);
            DateTime now = DateTime.Now;
            string text = ((now.Hour <= 9) ? ("0" + now.Hour) : now.Hour.ToString());
            string text2 = ((now.Minute <= 9) ? ("0" + now.Minute) : now.Minute.ToString());
            string text3 = ((now.Second <= 9) ? ("0" + now.Second) : now.Second.ToString());
            string s = text + ":" + text2 + ":" + text3;
            FontFamily family = new("Times New Roman");
            Font font = new(family, Convert.ToInt32((double)wth * 0.17));
            Point point = new(Convert.ToInt32((double)wth * 0.05), Convert.ToInt32((double)hgt * 0.05));
            graphics.DrawString(s, font, brush, point);
            FinishRefresh(bitmap);
        }
        catch
        {
        }
    }

    public override void ShowDialog()
    {
        TimerSet2 timerSet = new()
        {
            bgcolor = bgcolor,
            timercolor = timercolor,
            fontcolor = fontcolor
        };
        if (timerSet.ShowDialog() == DialogResult.OK)
        {
            bgcolor = timerSet.bgcolor;
            timercolor = timerSet.timercolor;
            fontcolor = timerSet.fontcolor;
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
        BSave bSave = new()
        {
            bgcolor = bgcolor,
            timercolor = timercolor,
            fontcolor = fontcolor
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
            BSave bSave = (BSave)formatter.Deserialize(stream);
            stream.Close();
            bgcolor = bSave.bgcolor;
            timercolor = bSave.timercolor;
            fontcolor = bSave.fontcolor;
            tm1 = new Timer();
            tm1.Tick += tm1_Tick;
            tm1.Interval = 1000;
            tm1.Enabled = true;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
