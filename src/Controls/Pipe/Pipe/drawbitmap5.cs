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
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap5 : CPixieControl
{
    private Color setcolor = Color.LightGray;

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [DisplayName("管道颜色")]
    [Category("杂项")]
    [Description("管道颜色。")]
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
        drawbitmap5 drawbitmap9 = (drawbitmap5)base.Copy();
        drawbitmap9.setcolor = setcolor;
        return drawbitmap9;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 60, 100, 40);
        new Rectangle(0, 60, 100, 20);
        new Rectangle(0, 80, 100, 20);
        LinearGradientBrush brush = new(rect, setcolor, Color.White, LinearGradientMode.Horizontal);
        LinearGradientBrush brush2 = new(rect, Color.White, setcolor, LinearGradientMode.Horizontal);
        Point[] points = new Point[4]
        {
            new(80, 80),
            new(60, 60),
            new(0, 60),
            new(0, 80)
        };
        Point[] points2 = new Point[4]
        {
            new(80, 80),
            new(100, 100),
            new(0, 100),
            new(0, 80)
        };
        g.FillPolygon(brush, points);
        g.FillPolygon(brush2, points2);
        Rectangle rect2 = new(60, 0, 40, 100);
        LinearGradientBrush brush3 = new(rect2, setcolor, Color.White, 90f);
        LinearGradientBrush brush4 = new(rect2, Color.White, setcolor, 90f);
        Point[] points3 = new Point[4]
        {
            new(80, 80),
            new(60, 60),
            new(0, 60),
            new(0, 80)
        };
        Point[] points4 = new Point[4]
        {
            new(80, 80),
            new(100, 100),
            new(0, 100),
            new(0, 80)
        };
        g.FillPolygon(brush3, points3);
        g.FillPolygon(brush4, points4);
    }

    public override void RefreshControl()
    {
        Bitmap bitmap = new(100, 100);
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
        BSaveP1C1 bSaveP1C = new()
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
            BSaveP1C1 bSaveP1C = (BSaveP1C1)formatter.Deserialize(stream);
            stream.Close();
            setcolor = bSaveP1C.setcolor;
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
