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

namespace KG_1;

[Serializable]
[Guid("B65CFCA2-49EA-40ee-8602-C7070FD93E59")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class drawbitmap7 : CPixieControl
{
    private Color blockcolor = Color.FromArgb(255, 255, 0, 0);

    private string varname1 = "";

    private string varname2 = "";

    private string varname3 = "";

    private string varname4 = "";

    private string txt1 = "label1";

    private string txt2 = "label2";

    private string txt3 = "label3";

    private string txt4 = "label4";

    private string ontxt = "ON";

    private string offtxt = "OFF";

    private int initflag = 1;

    private int _width = 200;

    private int _height = 300;

    private bool opstflag;

    [NonSerialized]
    private SolidBrush br;

    [NonSerialized]
    private bool value;

    private bool value2;

    private bool value3;

    private bool value4;

    [DHMICtrlProperty]
    [Category("杂项")]
    [ReadOnly(false)]
    [Description("滑动块颜色。")]
    [DisplayName("滑动块颜色")]
    public Color Blockcolor
    {
        get
        {
            return blockcolor;
        }
        set
        {
            blockcolor = value;
        }
    }

    [Category("杂项")]
    [Description("开关绑定变量。")]
    [ReadOnly(false)]
    [DisplayName("绑定变量1")]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [Description("开关绑定变量。")]
    [DisplayName("绑定变量2")]
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
    [Category("杂项")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Description("开关绑定变量。")]
    [DisplayName("绑定变量3")]
    [DHMICtrlProperty]
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

    [Description("开关绑定变量。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
    [Category("杂项")]
    [DisplayName("绑定变量4")]
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

    [DHMICtrlProperty]
    [Description("文本1。")]
    [ReadOnly(false)]
    [DisplayName("文本1")]
    [Category("杂项")]
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
    [Description("文本2。")]
    [DisplayName("文本2")]
    [ReadOnly(false)]
    [Category("杂项")]
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
    [Description("文本3。")]
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
    [DHMICtrlProperty]
    [DisplayName("文本4")]
    [Description("文本4。")]
    [ReadOnly(false)]
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

    [DisplayName("为真时文本")]
    [Category("杂项")]
    [Description("绑定变量为真时滑动块所在一侧显示的文本。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public string Ontext
    {
        get
        {
            return ontxt;
        }
        set
        {
            ontxt = value;
        }
    }

    [DisplayName("为假时文本")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [Category("杂项")]
    [Description("绑定变量为假时滑动块所在一侧显示的文本。")]
    public string Offtext
    {
        get
        {
            return offtxt;
        }
        set
        {
            offtxt = value;
        }
    }

    [ReadOnly(false)]
    [Description("绑定变量1当前值。")]
    [DisplayName("绑定变量当前值")]
    [Category("杂项")]
    public bool Value
    {
        get
        {
            return value;
        }
        set
        {
            if (this.value != value)
            {
                Value1Changed?.Invoke(this, null);
                NeedRefresh = true;
                this.value = value;
            }
        }
    }

    [Category("杂项")]
    [ReadOnly(false)]
    [Description("绑定变量2当前值。")]
    [DisplayName("绑定变量当前值2")]
    public bool Value2
    {
        get
        {
            return value2;
        }
        set
        {
            if (value2 != value)
            {
                Value2Changed?.Invoke(this, null);
                NeedRefresh = true;
                value2 = value;
            }
        }
    }

    [DisplayName("绑定变量当前值3")]
    [Category("杂项")]
    [Description("绑定变量3当前值。")]
    [ReadOnly(false)]
    public bool Value3
    {
        get
        {
            return value3;
        }
        set
        {
            if (value3 != value)
            {
                Value3Changed?.Invoke(this, null);
                NeedRefresh = true;
                value3 = value;
            }
        }
    }

    [DisplayName("绑定变量当前值4")]
    [Category("杂项")]
    [Description("绑定变量4当前值。")]
    [ReadOnly(false)]
    public bool Value4
    {
        get
        {
            return value4;
        }
        set
        {
            if (value4 != value)
            {
                Value4Changed?.Invoke(this, null);
                NeedRefresh = true;
                value4 = value;
            }
        }
    }

    public event EventHandler Value1Changed;

    public event EventHandler Value2Changed;

    public event EventHandler Value3Changed;

    public event EventHandler Value4Changed;

    public drawbitmap7()
    {
    }

    protected drawbitmap7(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("drawbitmap7 info");
        }
        drawbitmap7 obj = new();
        FieldInfo[] fields = typeof(drawbitmap7).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(drawbitmap7))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int j = 0; j < serializableMembers.Length; j++)
        {
            if (serializableMembers[j] is FieldInfo && serializableMembers[j].DeclaringType == typeof(drawbitmap7))
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
        MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(typeof(drawbitmap7), context);
        for (int i = 0; i < serializableMembers.Length; i++)
        {
            if (serializableMembers[i] is FieldInfo && serializableMembers[i].DeclaringType == typeof(drawbitmap7) && !Attribute.IsDefined(serializableMembers[i], typeof(NonSerializedAttribute)))
            {
                info.AddValue(serializableMembers[i].Name, ((FieldInfo)serializableMembers[i]).GetValue(this));
            }
        }
    }

    public override CShape Copy()
    {
        drawbitmap7 drawbitmap10 = (drawbitmap7)base.Copy();
        drawbitmap10.ontxt = ontxt;
        drawbitmap10.offtxt = offtxt;
        drawbitmap10.txt1 = txt1;
        drawbitmap10.txt2 = txt2;
        drawbitmap10.txt3 = txt3;
        drawbitmap10.txt4 = txt4;
        drawbitmap10.varname1 = varname1;
        drawbitmap10.varname2 = varname2;
        drawbitmap10.varname3 = varname3;
        drawbitmap10.varname4 = varname4;
        drawbitmap10.blockcolor = blockcolor;
        return drawbitmap10;
    }

    public void Paint(Graphics g)
    {
        br = new SolidBrush(blockcolor);
        List<string> list = new();
        list.Add(txt1);
        list.Add(txt2);
        list.Add(txt3);
        list.Add(txt4);
        FontFamily family = new("宋体");
        Font font = new(family, Convert.ToInt32((double)_width * 0.05));
        g.FillRectangle(rect: new Rectangle(0, 0, _width, _height), brush: Brushes.LightGray);
        for (int i = 0; i < 4; i++)
        {
            g.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.1 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.1)), s: list[i], font: font, brush: Brushes.Black);
            g.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.1 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.2)), s: ontxt, font: font, brush: Brushes.Black);
            g.DrawString(point: new Point(Convert.ToInt32((double)_width * 0.1 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.9)), s: offtxt, font: font, brush: Brushes.Black);
            g.FillRectangle(rect: new Rectangle(Convert.ToInt32((double)_width * 0.1 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.3), Convert.ToInt32((double)_width * 0.1), Convert.ToInt32((double)_height * 0.6)), brush: Brushes.White);
        }
        if (initflag == 1 && !isRunning)
        {
            for (int j = 0; j < 4; j++)
            {
                Rectangle rect3 = new(Convert.ToInt32((double)_width * 0.11 + (double)(j * _width) * 0.25), Convert.ToInt32((double)_height * 0.75), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.14));
                g.FillRectangle(br, rect3);
                g.DrawRectangle(Pens.Black, rect3);
            }
        }
    }

    public override void ManageMouseDown(MouseEventArgs e)
    {
        try
        {
            if ((double)e.X < (double)Width * 0.25)
            {
                if (Convert.ToBoolean(GetValue(varname1)))
                {
                    SetValue(varname1, false);
                }
                else
                {
                    SetValue(varname1, true);
                }
            }
            else if ((double)e.X > (double)Math.Abs(Width) * 0.25 && (double)e.X < (double)Math.Abs(Width) * 0.5)
            {
                if (Convert.ToBoolean(GetValue(varname2)))
                {
                    SetValue(varname2, false);
                }
                else
                {
                    SetValue(varname2, true);
                }
            }
            else if ((double)e.X > (double)Math.Abs(Width) * 0.5 && (double)e.X < (double)Math.Abs(Width) * 0.75)
            {
                if (Convert.ToBoolean(GetValue(varname3)))
                {
                    SetValue(varname3, false);
                }
                else
                {
                    SetValue(varname3, true);
                }
            }
            else if ((double)e.X > (double)Math.Abs(Width) * 0.75)
            {
                if (Convert.ToBoolean(GetValue(varname4)))
                {
                    SetValue(varname4, false);
                }
                else
                {
                    SetValue(varname4, true);
                }
            }
        }
        catch
        {
        }
    }

    public override void RefreshControl()
    {
        try
        {
            object obj = GetValue(varname1);
            object obj2 = GetValue(varname2);
            object obj3 = GetValue(varname3);
            object obj4 = GetValue(varname4);
            if (Height == 0f || Width == 0f)
            {
                _height = 200;
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
                Value = Convert.ToBoolean(obj);
            }
            if (obj2 != null)
            {
                Value2 = Convert.ToBoolean(obj2);
            }
            if (obj3 != null)
            {
                Value3 = Convert.ToBoolean(obj3);
            }
            if (obj4 != null)
            {
                Value4 = Convert.ToBoolean(obj4);
            }
            initflag = 0;
            try
            {
                if (opstflag)
                {
                    Value = !Value;
                    Value2 = !Value2;
                    Value3 = !Value3;
                    Value4 = !Value4;
                }
                Paint(graphics);
                List<bool> list = new();
                list.Add(Value);
                list.Add(Value2);
                list.Add(Value3);
                list.Add(Value4);
                for (int i = 0; i < 4; i++)
                {
                    if (list[i])
                    {
                        Rectangle rect = new(Convert.ToInt32((double)_width * 0.11 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.31), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.14));
                        graphics.FillRectangle(br, rect);
                        graphics.DrawRectangle(Pens.Black, rect);
                    }
                    else
                    {
                        Rectangle rect2 = new(Convert.ToInt32((double)_width * 0.11 + (double)(i * _width) * 0.25), Convert.ToInt32((double)_height * 0.75), Convert.ToInt32((double)_width * 0.08), Convert.ToInt32((double)_height * 0.14));
                        graphics.FillRectangle(br, rect2);
                        graphics.DrawRectangle(Pens.Black, rect2);
                    }
                }
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
        KGSet kGSet = new();
        if (!string.IsNullOrEmpty(varname1))
        {
            kGSet.opstflag = opstflag;
            kGSet.varname1 = varname1.Substring(1, varname1.Length - 2);
            kGSet.varname2 = varname2.Substring(1, varname1.Length - 2);
            kGSet.varname3 = varname3.Substring(1, varname1.Length - 2);
            kGSet.varname4 = varname4.Substring(1, varname1.Length - 2);
            kGSet.txt1 = txt1;
            kGSet.txt2 = txt2;
            kGSet.txt3 = txt3;
            kGSet.txt4 = txt4;
            kGSet.ontxt = ontxt;
            kGSet.offtxt = offtxt;
            kGSet.blockcolor = blockcolor;
        }
        kGSet.viewevent += GetTable;
        kGSet.ckvarevent += CheckVar;
        if (kGSet.ShowDialog() == DialogResult.OK)
        {
            opstflag = kGSet.opstflag;
            blockcolor = kGSet.blockcolor;
            txt1 = kGSet.txt1;
            txt2 = kGSet.txt2;
            txt3 = kGSet.txt3;
            txt4 = kGSet.txt4;
            ontxt = kGSet.ontxt;
            offtxt = kGSet.offtxt;
            varname1 = "[" + kGSet.varname1 + "]";
            varname2 = "[" + kGSet.varname2 + "]";
            varname3 = "[" + kGSet.varname3 + "]";
            varname4 = "[" + kGSet.varname4 + "]";
            NeedRefresh = true;
        }
    }

    public override byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        BSaveBtns bSaveBtns = new()
        {
            blockcolor = blockcolor,
            varname1 = varname1,
            varname2 = varname2,
            varname3 = varname3,
            varname4 = varname4,
            txt1 = txt1,
            txt2 = txt2,
            txt3 = txt3,
            txt4 = txt4,
            ontxt = ontxt,
            offtxt = offtxt
        };
        formatter.Serialize(memoryStream, bSaveBtns);
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
            BSaveBtns bSaveBtns = (BSaveBtns)formatter.Deserialize(stream);
            stream.Close();
            blockcolor = bSaveBtns.blockcolor;
            varname1 = bSaveBtns.varname1;
            varname2 = bSaveBtns.varname2;
            varname3 = bSaveBtns.varname3;
            varname4 = bSaveBtns.varname4;
            txt1 = bSaveBtns.txt1;
            txt2 = bSaveBtns.txt2;
            txt3 = bSaveBtns.txt3;
            txt4 = bSaveBtns.txt4;
            ontxt = bSaveBtns.ontxt;
            offtxt = bSaveBtns.offtxt;
        }
        catch
        {
        }
    }

    private void tm1_Tick(object sender, EventArgs e)
    {
        if (isRunning)
        {
            NeedRefresh = true;
        }
    }

    private string GetTable()
    {
        return GetVarNames();
    }
}
