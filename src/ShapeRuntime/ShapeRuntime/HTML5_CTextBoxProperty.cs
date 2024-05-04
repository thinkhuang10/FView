using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CTextBoxProperty
{
    [NonSerialized]
    public CTextBox theTextBox;

    [Category("设计")]
    [Description("设定控件名称。")]
    [DisplayName("名称")]
    [ReadOnly(false)]
    public string ID
    {
        get
        {
            return theTextBox.ID;
        }
        set
        {
            theTextBox.ID = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("只读")]
    [Description("设定是否只读。")]
    public bool ReadOnly
    {
        get
        {
            return theTextBox.ReadOnly;
        }
        set
        {
            theTextBox.ReadOnly = value;
        }
    }

    [DisplayName("控件是否多行")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("设置一个值，该值指示此控件是否为多行TextBox 控件。注：在控件多行与系统密码设置不能同时为True。")]
    public bool Multiline
    {
        get
        {
            return theTextBox.Multiline;
        }
        set
        {
            theTextBox.Multiline = value;
        }
    }

    [DisplayName("是否采用系统密码设置")]
    [Description("设置一个值，该值指示TextBox 控件中的文本是否应该以默认的密码字符显示。注：在控件多行与系统密码设置不能同时为True。")]
    [Category("行为")]
    [ReadOnly(false)]
    public bool UseSystemPasswordChar
    {
        get
        {
            return theTextBox.UseSystemPasswordChar;
        }
        set
        {
            theTextBox.UseSystemPasswordChar = value;
        }
    }

    [Description("设置控件中字体的样式")]
    [ReadOnly(false)]
    [Category("外观")]
    [DisplayName("字体设置")]
    public Font Font
    {
        get
        {
            return theTextBox.Font;
        }
        set
        {
            theTextBox.Font = value;
        }
    }

    [ReadOnly(false)]
    [Description("设置或获取控件的字体颜色")]
    [DisplayName("字体颜色")]
    [Category("外观")]
    public Color ForeColor
    {
        get
        {
            return theTextBox.ForeColor;
        }
        set
        {
            theTextBox.ForeColor = value;
        }
    }

    [Category("外观")]
    [ReadOnly(false)]
    [Description("设置或获取控件的背景色")]
    [DisplayName("背景色")]
    public Color BackColor
    {
        get
        {
            return theTextBox.BackColor;
        }
        set
        {
            theTextBox.BackColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件位置")]
    [Category("布局")]
    [Description("设置控件的位置（横纵坐标）")]
    public Point Location
    {
        get
        {
            return theTextBox.Location;
        }
        set
        {
            theTextBox.Location = value;
        }
    }

    [Category("布局")]
    [DisplayName("控件大小")]
    [ReadOnly(false)]
    [Description("设置控件的大小（横纵坐标）")]
    public Size Size
    {
        get
        {
            return theTextBox.Size;
        }
        set
        {
            theTextBox.Size = value;
        }
    }

    [DisplayName("文本对齐方式")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("设置文本框的文本对齐方式")]
    public HorizontalAlignment TextAlign
    {
        get
        {
            return theTextBox.TextAlign;
        }
        set
        {
            theTextBox.TextAlign = value;
        }
    }

    [DisplayName("显示文本")]
    [ReadOnly(false)]
    [Category("数据")]
    [Description("设置或获取控件的文本值")]
    public string Text
    {
        get
        {
            return theTextBox.Text;
        }
        set
        {
            theTextBox.Text = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件选中当前值")]
    [Category("行为")]
    [Description("设置一个值，该值指示控件中当前选定的文本")]
    public string SelectedText
    {
        get
        {
            return theTextBox.SelectedText;
        }
        set
        {
            theTextBox.SelectedText = value;
        }
    }

    [DisplayName("文本框是否可见")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("设置一个值，该值指示是否显示该控件")]
    public bool Visible
    {
        get
        {
            return theTextBox.Visible;
        }
        set
        {
            theTextBox.Visible = value;
        }
    }

    [DisplayName("Tab顺序索引")]
    [Description("设置Tab键顺序索引值")]
    [ReadOnly(false)]
    [Category("行为")]
    public int TabIndex
    {
        get
        {
            return theTextBox.TabIndex;
        }
        set
        {
            theTextBox.TabIndex = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("文本最大长度")]
    [Category("行为")]
    [Description("设置文本框中显示文本的最大长度")]
    public int StrLen
    {
        get
        {
            return theTextBox.MaxLength;
        }
        set
        {
            theTextBox.MaxLength = value;
        }
    }

    public HTML5_CTextBoxProperty()
    {
    }

    public HTML5_CTextBoxProperty(CTextBox _theTextBox)
    {
        theTextBox = _theTextBox;
    }
}
