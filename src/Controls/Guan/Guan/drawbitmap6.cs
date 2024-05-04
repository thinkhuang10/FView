using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SetsForms;
using ShapeRuntime;

namespace Guan;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
public class drawbitmap6 : CPixieControl
{
    private float value;

    private string varname = "";

    private bool initflag = true;

    private float high = 100f;

    private float low;

    private int highpst = 100;

    private int lowpst;

    private Color colortype = Color.Green;

    private Color setcolortype = Color.Gray;

    private Color bgcolor = Color.White;

    private int _width = 200;

    private int _height = 550;

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

    public drawbitmap6()
    {
    }

    protected drawbitmap6(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap6 obj = new();
        FieldInfo[] fields = typeof(drawbitmap6).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap6))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap6), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap6))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap6), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap6) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        return (drawbitmap6)base.Copy();
    }

    public void Paint(Graphics g)
    {
        Pen pen = new(Color.White, 4f);
        Rectangle rect = new(15 * _width / 200, 50 * _height / 550, 85 * _width / 200, 250 * _height / 550);
        Rectangle rect2 = new(100 * _width / 200, 50 * _height / 550, 85 * _width / 200, 250 * _height / 550);
        Rectangle rect3 = new(0, 0, 100 * _width / 200, 300 * _height / 550);
        Rectangle rect4 = new(0, 0, 100 * _width / 200, 300 * _height / 550);
        LinearGradientBrush brush = new(rect3, setcolortype, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect4, Color.White, setcolortype, LinearGradientMode.Horizontal);
        Point[] points = new Point[4]
        {
            new(20 * _width / 200, 0),
            new(100 * _width / 200, 0),
            new(100 * _width / 200, 50 * _height / 550),
            new(0, 50 * _height / 550)
        };
        Point[] points2 = new Point[4]
        {
            new(180 * _width / 200, 0),
            new(100 * _width / 200, 0),
            new(100 * _width / 200, 50 * _height / 550),
            new(200 * _width / 200, 50 * _height / 550)
        };
        Point[] points3 = new Point[4]
        {
            new(180 * _width / 200, 0),
            new(20 * _width / 200, 0),
            new(0, 50 * _height / 550),
            new(200 * _width / 200, 50 * _height / 550)
        };
        g.FillRectangle(brush, rect);
        g.FillRectangle(brush2, rect2);
        g.FillPolygon(brush, points);
        g.FillPolygon(brush2, points2);
        g.DrawLine(pen, 100 * _width / 200, 0, 100 * _width / 200, 300 * _height / 550);
        g.DrawPolygon(Pens.Black, points3);
        if (initflag)
        {
            g.FillRectangle(rect: new Rectangle(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, 200 * _height / 550), brush: Brushes.Green);
            Point[] array = new Point[4];
            ref Point reference = ref array[0];
            reference = new Point(15 * _width / 200, 135 * _height / 550);
            ref Point reference2 = ref array[1];
            reference2 = new Point(25 * _width / 200, 135 * _height / 550);
            ref Point reference3 = ref array[3];
            reference3 = new Point(5 * _width / 200, 530 * _height / 550);
            ref Point reference4 = ref array[2];
            reference4 = new Point(20 * _width / 200, 522 * _height / 550);
            Point[] points4 = new Point[4]
            {
                new(175 * _width / 200, 135 * _height / 550),
                new(185 * _width / 200, 135 * _height / 550),
                new(195 * _width / 200, 530 * _height / 550),
                new(180 * _width / 200, 522 * _height / 550)
            };
            Rectangle rect6 = new(67 * _width / 200, 135 * _height / 550, 15 * _width / 200, 380 * _height / 550);
            Rectangle rect7 = new(118 * _width / 200, 135 * _height / 550, 15 * _width / 200, 380 * _height / 550);
            g.DrawRectangle(Pens.Black, rect6);
            g.DrawRectangle(Pens.Black, rect7);
            g.DrawPolygon(Pens.Black, array);
            g.DrawPolygon(Pens.Black, points4);
            g.FillRectangle(new SolidBrush(Color.LightGray), rect6);
            g.FillRectangle(new SolidBrush(Color.LightGray), rect7);
            g.FillPolygon(new SolidBrush(Color.LightGray), array);
            g.FillPolygon(new SolidBrush(Color.LightGray), points4);
            Rectangle rect8 = new(35 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            Rectangle rect9 = new(95 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            Rectangle rect10 = new(145 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            g.FillRectangle(Brushes.LightGray, rect8);
            g.FillRectangle(Brushes.LightGray, rect9);
            g.FillRectangle(Brushes.LightGray, rect10);
            g.DrawLine(Pens.Black, 20 * _width / 200, 140 * _height / 550, 70 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 70 * _width / 200, 140 * _height / 550, 120 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 120 * _width / 200, 140 * _height / 550, 170 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 170 * _width / 200, 140 * _height / 550, 120 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 120 * _width / 200, 140 * _height / 550, 70 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 70 * _width / 200, 140 * _height / 550, 20 * _width / 200, 330 * _height / 550);
            g.DrawLine(Pens.Black, 20 * _width / 200, 330 * _height / 550, 70 * _width / 200, 490 * _height / 550);
            g.DrawLine(Pens.Black, 70 * _width / 200, 330 * _height / 550, 120 * _width / 200, 490 * _height / 550);
            g.DrawLine(Pens.Black, 120 * _width / 200, 330 * _height / 550, 170 * _width / 200, 490 * _height / 550);
            g.DrawLine(Pens.Black, 170 * _width / 200, 330 * _height / 550, 120 * _width / 200, 490 * _height / 550);
            g.DrawLine(Pens.Black, 120 * _width / 200, 330 * _height / 550, 70 * _width / 200, 490 * _height / 550);
            g.DrawLine(Pens.Black, 70 * _width / 200, 330 * _height / 550, 20 * _width / 200, 490 * _height / 550);
            Rectangle rect11 = new(15 * _width / 200, 130 * _height / 550, 170 * _width / 200, 15 * _height / 550);
            Rectangle rect12 = new(15 * _width / 200, 330 * _height / 550, 170 * _width / 200, 10 * _height / 550);
            g.FillRectangle(Brushes.DimGray, rect11);
            g.FillRectangle(Brushes.DimGray, rect12);
        }
    }

    public override void RefreshControl()
    {
        object obj = (string.IsNullOrEmpty(varname) ? null : GetValue(varname));
        if (Height == 0f || Width == 0f)
        {
            _height = 550;
            _width = 200;
        }
        else
        {
            _height = Convert.ToInt32(Math.Abs(Height));
            _width = Convert.ToInt32(Math.Abs(Width));
        }
        Bitmap bitmap = new(_width, _height);
        Graphics graphics = Graphics.FromImage(bitmap);
        FontFamily family = new("Arial");
        new Font(family, 10 * _height / 210);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        if (obj == null)
        {
            Paint(graphics);
        }
        else
        {
            initflag = false;
            Paint(graphics);
            Value = Convert.ToSingle(obj);
            if (Value >= low && Value <= high)
            {
                Rectangle rect = new(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, Convert.ToInt32((float)(200 * _height / 550) * (high - Value) / (high - low)));
                Rectangle rect2 = new(80 * _width / 200, 75 * _height / 550 + Convert.ToInt32((float)(200 * _height / 550) * (high - Value) / (high - low)), 40 * _width / 200, Convert.ToInt32((float)(200 * _height / 550) * (Value - low) / (high - low)));
                graphics.FillRectangle(new SolidBrush(bgcolor), rect);
                graphics.FillRectangle(new SolidBrush(colortype), rect2);
                Rectangle rect3 = new(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * (100 - highpst) / 100));
                Rectangle rect4 = new(80 * _width / 200, 75 * _height / 550 + Convert.ToInt32(200 * _height / 550 * (100 - lowpst) / 100), 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * lowpst / 100));
                graphics.FillRectangle(new SolidBrush(bgcolor), rect3);
                graphics.FillRectangle(new SolidBrush(colortype), rect4);
            }
            else if (Value > high)
            {
                graphics.FillRectangle(rect: new Rectangle(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, 200 * _height / 550), brush: new SolidBrush(colortype));
                Rectangle rect6 = new(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * (100 - highpst) / 100));
                Rectangle rect7 = new(80 * _width / 200, 75 * _height / 550 + Convert.ToInt32(200 * _height / 550 * (100 - lowpst) / 100), 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * lowpst / 100));
                graphics.FillRectangle(new SolidBrush(bgcolor), rect6);
                graphics.FillRectangle(new SolidBrush(colortype), rect7);
            }
            else
            {
                graphics.FillRectangle(rect: new Rectangle(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, 200 * _height / 550), brush: new SolidBrush(bgcolor));
                Rectangle rect9 = new(80 * _width / 200, 75 * _height / 550, 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * (100 - highpst) / 100));
                Rectangle rect10 = new(80 * _width / 200, 75 * _height / 550 + Convert.ToInt32(200 * _height / 550 * (100 - lowpst) / 100), 40 * _width / 200, Convert.ToInt32(200 * _height / 550 * lowpst / 100));
                graphics.FillRectangle(new SolidBrush(bgcolor), rect9);
                graphics.FillRectangle(new SolidBrush(colortype), rect10);
            }
            Point[] array = new Point[4];
            ref Point reference = ref array[0];
            reference = new Point(15 * _width / 200, 135 * _height / 550);
            ref Point reference2 = ref array[1];
            reference2 = new Point(25 * _width / 200, 135 * _height / 550);
            ref Point reference3 = ref array[3];
            reference3 = new Point(5 * _width / 200, 530 * _height / 550);
            ref Point reference4 = ref array[2];
            reference4 = new Point(20 * _width / 200, 522 * _height / 550);
            Point[] points = new Point[4]
            {
                new(175 * _width / 200, 135 * _height / 550),
                new(185 * _width / 200, 135 * _height / 550),
                new(195 * _width / 200, 530 * _height / 550),
                new(180 * _width / 200, 522 * _height / 550)
            };
            Rectangle rect11 = new(67 * _width / 200, 135 * _height / 550, 15 * _width / 200, 380 * _height / 550);
            Rectangle rect12 = new(118 * _width / 200, 135 * _height / 550, 15 * _width / 200, 380 * _height / 550);
            graphics.DrawRectangle(Pens.Black, rect11);
            graphics.DrawRectangle(Pens.Black, rect12);
            graphics.DrawPolygon(Pens.Black, array);
            graphics.DrawPolygon(Pens.Black, points);
            graphics.FillRectangle(new SolidBrush(Color.LightGray), rect11);
            graphics.FillRectangle(new SolidBrush(Color.LightGray), rect12);
            graphics.FillPolygon(new SolidBrush(Color.LightGray), array);
            graphics.FillPolygon(new SolidBrush(Color.LightGray), points);
            Rectangle rect13 = new(35 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            Rectangle rect14 = new(95 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            Rectangle rect15 = new(145 * _width / 200, 135 * _height / 550, 10 * _width / 200, 370 * _height / 550);
            graphics.FillRectangle(Brushes.LightGray, rect13);
            graphics.FillRectangle(Brushes.LightGray, rect14);
            graphics.FillRectangle(Brushes.LightGray, rect15);
            graphics.DrawLine(Pens.Black, 20 * _width / 200, 140 * _height / 550, 70 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 70 * _width / 200, 140 * _height / 550, 120 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 120 * _width / 200, 140 * _height / 550, 170 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 170 * _width / 200, 140 * _height / 550, 120 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 120 * _width / 200, 140 * _height / 550, 70 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 70 * _width / 200, 140 * _height / 550, 20 * _width / 200, 330 * _height / 550);
            graphics.DrawLine(Pens.Black, 20 * _width / 200, 330 * _height / 550, 70 * _width / 200, 490 * _height / 550);
            graphics.DrawLine(Pens.Black, 70 * _width / 200, 330 * _height / 550, 120 * _width / 200, 490 * _height / 550);
            graphics.DrawLine(Pens.Black, 120 * _width / 200, 330 * _height / 550, 170 * _width / 200, 490 * _height / 550);
            graphics.DrawLine(Pens.Black, 170 * _width / 200, 330 * _height / 550, 120 * _width / 200, 490 * _height / 550);
            graphics.DrawLine(Pens.Black, 120 * _width / 200, 330 * _height / 550, 70 * _width / 200, 490 * _height / 550);
            graphics.DrawLine(Pens.Black, 70 * _width / 200, 330 * _height / 550, 20 * _width / 200, 490 * _height / 550);
            Rectangle rect16 = new(15 * _width / 200, 130 * _height / 550, 170 * _width / 200, 15 * _height / 550);
            Rectangle rect17 = new(15 * _width / 200, 330 * _height / 550, 170 * _width / 200, 10 * _height / 550);
            graphics.FillRectangle(Brushes.DimGray, rect16);
            graphics.FillRectangle(Brushes.DimGray, rect17);
        }
        FinishRefresh(bitmap);
    }

    public bool CheckVar(string chkstr)
    {
        return ValidateVarName("[" + chkstr + "]");
    }

    public override void ShowDialog()
    {
        GSet gSet = new();
        gSet.viewevent += GetTable;
        gSet.ckvarevent += CheckVar;
        if (!string.IsNullOrEmpty(varname.ToString()))
        {
            gSet.varname = varname;
            gSet.highpersnt = highpst;
            gSet.high = high;
            gSet.low = low;
            gSet.lowpersnt = lowpst;
            gSet.colortype = colortype;
            gSet.setcolortype = setcolortype;
            gSet.Bgcolor = bgcolor;
        }
        if (gSet.ShowDialog() == DialogResult.OK)
        {
            varname = gSet.varname;
            colortype = gSet.colortype;
            setcolortype = gSet.setcolortype;
            high = gSet.high;
            highpst = gSet.highpersnt;
            low = gSet.low;
            lowpst = gSet.lowpersnt;
            bgcolor = gSet.Bgcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        SaveC6 saveC = new()
        {
            varname = varname,
            value = value,
            high = high,
            low = low,
            highpst = highpst,
            lowpst = lowpst,
            colortype = colortype,
            setcolortype = setcolortype
        };
        formatter.Serialize(memoryStream, saveC);
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
            SaveC6 saveC = (SaveC6)formatter.Deserialize(stream);
            stream.Close();
            varname = saveC.varname;
            value = saveC.value;
            high = saveC.high;
            low = saveC.low;
            highpst = saveC.highpst;
            lowpst = saveC.lowpst;
            colortype = saveC.colortype;
            setcolortype = saveC.setcolortype;
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
