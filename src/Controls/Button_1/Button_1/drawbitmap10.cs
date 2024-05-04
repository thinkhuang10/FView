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

namespace Button_1;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
public class drawbitmap10 : CPixieControl
{
    private string strtoshow = "按钮";

    private Color bgcolor = Color.White;

    private Color txtcolor = Color.Black;

    private bool mousedownflag;

    private int width = 100;

    private int hight = 100;

    private Font selectfont = new(new FontFamily("新宋体"), 10f);

    [NonSerialized]
    private readonly Timer tm1 = new();

    [Description("按钮显示文本。")]
    [Category("杂项")]
    [DHMICtrlProperty]
    [DisplayName("文本")]
    [ReadOnly(false)]
    public string Text
    {
        get
        {
            return strtoshow;
        }
        set
        {
            strtoshow = value;
        }
    }

    [Category("杂项")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Description("按钮背景色。")]
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

    [DisplayName("文本颜色")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("按钮文本的颜色。")]
    public Color TextColor
    {
        get
        {
            return txtcolor;
        }
        set
        {
            txtcolor = value;
        }
    }

    public new event EventHandler Click;

    public drawbitmap10()
    {
    }

    protected drawbitmap10(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        drawbitmap10 obj = new();
        FieldInfo[] fields = typeof(drawbitmap10).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap10))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap10), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap10))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap10), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap10) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
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
        drawbitmap10 drawbitmap32 = (drawbitmap10)base.Copy();
        drawbitmap32.strtoshow = strtoshow;
        drawbitmap32.bgcolor = bgcolor;
        drawbitmap32.txtcolor = txtcolor;
        drawbitmap32.selectfont = selectfont;
        return drawbitmap32;
    }

    public void Paint(Graphics g)
    {
        Rectangle rect = new(0, 0, width, hight);
        SolidBrush brush = new(bgcolor);
        SolidBrush brush2 = new(txtcolor);
        g.FillRectangle(brush, rect);
        if (selectfont == null)
        {
            selectfont = SystemFonts.DefaultFont;
        }
        SizeF sizeF = g.MeasureString(strtoshow, selectfont);
        g.DrawString(point: new Point(Convert.ToInt32(((float)width - sizeF.Width) / 2f), Convert.ToInt32((double)((float)hight - sizeF.Height) * 1.11 / 2.0 + 1.0)), s: strtoshow, font: selectfont, brush: brush2);
        if (mousedownflag)
        {
            g.DrawLine(Pens.Black, 0, 0, width, 0);
            g.DrawLine(Pens.Black, 0, 0, 0, hight);
            g.DrawLine(Pens.White, width, hight, width, 0);
            g.DrawLine(Pens.White, width, hight, 0, hight);
        }
        else
        {
            g.DrawLine(Pens.White, 0, 0, width, 0);
            g.DrawLine(Pens.White, 0, 0, 0, hight);
            g.DrawLine(Pens.Black, width - 1, hight - 1, width - 1, 0);
            g.DrawLine(Pens.Black, width - 1, hight - 1, 0, hight - 1);
        }
    }

    public override void ManageMouseUp(MouseEventArgs e)
    {
        mousedownflag = false;
        NeedRefresh = true;
    }

    public override void ManageMouseMove(MouseEventArgs e)
    {
        base.ManageMouseMove(e);
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if (this.Click != null)
            {
                this.Click(this, null);
            }
            if (e.X < width && e.Y < hight)
            {
                mousedownflag = true;
                NeedRefresh = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public override void RefreshControl()
    {
        if (Height == 0f || Width == 0f)
        {
            hight = 100;
            width = 200;
        }
        else
        {
            hight = Convert.ToInt32(Math.Abs(Height));
            width = Convert.ToInt32(Math.Abs(Width));
        }
        if (width >= 0 && hight >= 0)
        {
            Bitmap bitmap = new(width, hight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Paint(graphics);
            FinishRefresh(bitmap);
        }
    }

    public override void ShowDialog()
    {
        StrBtnSET strBtnSET = new()
        {
            strtoshow = strtoshow,
            bgcolor = bgcolor,
            txtcolor = txtcolor,
            selectedfont = selectfont
        };
        if (strBtnSET.ShowDialog() == DialogResult.OK)
        {
            selectfont = strBtnSET.selectedfont;
            bgcolor = strBtnSET.bgcolor;
            txtcolor = strBtnSET.txtcolor;
            strtoshow = strBtnSET.strtoshow;
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        Save_strbtn save_strbtn = new()
        {
            strtoshow = strtoshow,
            bgcolor = bgcolor,
            txtcolor = txtcolor
        };
        formatter.Serialize(memoryStream, save_strbtn);
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
            Save_strbtn save_strbtn = (Save_strbtn)formatter.Deserialize(stream);
            stream.Close();
            strtoshow = save_strbtn.strtoshow;
            txtcolor = save_strbtn.txtcolor;
            bgcolor = save_strbtn.bgcolor;
        }
        catch { }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
