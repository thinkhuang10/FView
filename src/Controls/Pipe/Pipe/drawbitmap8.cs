using System;
using System.ComponentModel;
using System.Drawing;
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

namespace Pipe;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap8 : CPixieControl
{
    private Color setcolor = Color.LightGray;

    private int _width = 200;

    private int _height = 500;

    [Category("杂项")]
    [Description("管道颜色。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("管道颜色")]
    public Color PipeColor
    {
        get
        {
            return setcolor;
        }
        set
        {
            setcolor = value;
        }
    }

    public drawbitmap8()
    {
    }

    protected drawbitmap8(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap8 info");
        }
        drawbitmap8 obj = new();
        FieldInfo[] fields = typeof(drawbitmap8).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap8))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap8))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap8), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap8) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap8 drawbitmap9 = (drawbitmap8)base.Copy();
        drawbitmap9.setcolor = setcolor;
        return drawbitmap9;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 10 * _height / 100, 40 * _width / 40, 90 * _height / 100);
        Rectangle rect2 = new(0, 10 * _height / 100, 20 * _width / 40, 80 * _height / 100);
        Rectangle rect3 = new(20 * _width / 40, 10 * _height / 100, 20 * _width / 40, 80 * _height / 100);
        LinearGradientBrush brush = new(rect, setcolor, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect, Color.White, setcolor, LinearGradientMode.Horizontal);
        g.FillRectangle(brush, rect2);
        g.FillRectangle(brush2, rect3);
        Rectangle rect4 = new(0, 80 * _height / 100, 40 * _width / 40, 20 * _height / 100);
        g.FillPie(brush, rect4, 90f, 92f);
        g.FillPie(brush2, rect4, -2f, 92f);
        Rectangle rect5 = new(0, 0, 40 * _width / 40, 20 * _height / 100);
        g.FillPie(Brushes.DarkGray, rect5, 0f, 360f);
        g.DrawPie(Pens.DarkGray, rect5, 0f, 360f);
        Rectangle rect6 = new(5 * _width / 40, 3 * _height / 100, 30 * _width / 40, 14 * _height / 100);
        LinearGradientBrush brush3 = new(rect6, Color.DarkGray, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush4 = new(rect6, Color.White, Color.DarkGray, LinearGradientMode.Horizontal);
        g.FillPie(brush3, rect6, 90f, 180f);
        g.FillPie(brush4, rect6, 270f, 180f);
    }

    public override void RefreshControl()
    {
        if (Height == 0f || Width == 0f)
        {
            _height = 500;
            _width = 200;
        }
        else
        {
            _height = Convert.ToInt32(Math.Abs(Height));
            _width = Convert.ToInt32(Math.Abs(Width));
        }
        Bitmap bitmap = new(_width, _height);
        Graphics g = Graphics.FromImage(bitmap);
        Paint(g);
        FinishRefresh(bitmap);
    }

    private void tm1_Tick(object sender, EventArgs e)
    {
    }

    public override void ShowDialog()
    {
        PSet2 pSet = new()
        {
            setcolor = setcolor
        };
        if (pSet.ShowDialog() == DialogResult.OK)
        {
            setcolor = pSet.setcolor;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveP3C1 bSaveP3C = new()
        {
            setcolor = setcolor
        };
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
            BSaveP3C1 bSaveP3C = (BSaveP3C1)formatter.Deserialize(stream);
            stream.Close();
            setcolor = bSaveP3C.setcolor;
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
