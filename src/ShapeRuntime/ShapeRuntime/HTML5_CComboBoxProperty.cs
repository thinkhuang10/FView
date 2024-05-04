using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CComboBoxProperty
{
    [NonSerialized]
    public CComboBox theComboBox;

    [Category("设计")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [DisplayName("名称")]
    public string ID
    {
        get
        {
            return theComboBox.ID;
        }
        set
        {
            theComboBox.ID = value;
        }
    }

    [DisplayName("字体设置")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("设置控件中字体的样式")]
    public Font Font
    {
        get
        {
            return theComboBox.Font;
        }
        set
        {
            theComboBox.Font = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("字体颜色")]
    [Category("外观")]
    [Description("设置或获取控件的字体颜色")]
    public Color ForeColor
    {
        get
        {
            return theComboBox.ForeColor;
        }
        set
        {
            theComboBox.ForeColor = value;
        }
    }

    [Category("外观")]
    [DisplayName("按钮背景色")]
    [Description("设置或获取控件的背景色")]
    [ReadOnly(false)]
    public Color BackColor
    {
        get
        {
            return theComboBox.BackColor;
        }
        set
        {
            theComboBox.BackColor = value;
        }
    }

    [Category("布局")]
    [DisplayName("控件位置")]
    [ReadOnly(false)]
    [Description("设置或获取控件的位置")]
    public Point Location
    {
        get
        {
            return theComboBox.Location;
        }
        set
        {
            theComboBox.Location = value;
        }
    }

    [DisplayName("控件大小")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("设置或获取控件的大小")]
    public Size Size
    {
        get
        {
            return theComboBox.Size;
        }
        set
        {
            theComboBox.Size = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件样式")]
    [Category("外观")]
    [Description("设置或获取控件的样式。\r\n Simple = 0,文本部分可编辑。列表部分总可见。\r\n DropDown = 1,文本部分可编辑。用户必须单击箭头按钮来显示列表部分。这是默认样式。\r\n DropDownList = 2,用户不能直接编辑文本部分。用户必须单击箭头按钮来显示列表部分。")]
    public ComboBoxStyle DropDownStyle
    {
        get
        {
            return theComboBox.DropDownStyle;
        }
        set
        {
            theComboBox.DropDownStyle = value;
        }
    }

    [Description("设置或获取控件的文本值")]
    [Category("数据")]
    [DisplayName("显示文本")]
    [ReadOnly(false)]
    public string Text
    {
        get
        {
            return theComboBox.Text;
        }
        set
        {
            theComboBox.Text = value;
        }
    }

    [DisplayName("Tab顺序索引")]
    [Category("行为")]
    [Description("设置Tab键顺序索引值")]
    [ReadOnly(false)]
    public int TabIndex
    {
        get
        {
            return theComboBox.TabIndex;
        }
        set
        {
            theComboBox.TabIndex = value;
        }
    }

    [Category("行为")]
    [DisplayName("控件是否可用")]
    [Description("设置或获取控件是否可用")]
    [ReadOnly(false)]
    public bool Enabled
    {
        get
        {
            return theComboBox.Enabled;
        }
        set
        {
            theComboBox.Enabled = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件是否可见")]
    [Category("行为")]
    [Description("设置或获取控件的可见性")]
    public bool Visible
    {
        get
        {
            return theComboBox.Visible;
        }
        set
        {
            theComboBox.Visible = value;
        }
    }

    public HTML5_CComboBoxProperty()
    {
    }

    public HTML5_CComboBoxProperty(CComboBox _theComboBox)
    {
        theComboBox = _theComboBox;
    }
}
