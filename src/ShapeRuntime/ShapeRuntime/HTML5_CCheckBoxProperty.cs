using System;
using System.ComponentModel;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CCheckBoxProperty
{
    [NonSerialized]
    public CCheckBox theCheckBox;

    [Category("设计")]
    [DisplayName("名称")]
    [ReadOnly(false)]
    [Description("设定控件名称。")]
    public string ID
    {
        get
        {
            return theCheckBox.ID;
        }
        set
        {
            theCheckBox.ID = value;
        }
    }

    [DisplayName("控件使能性")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("用于设置控件的使能性")]
    public bool Enabled
    {
        get
        {
            return theCheckBox.Enabled;
        }
        set
        {
            theCheckBox.Enabled = value;
        }
    }

    [Category("外观")]
    [ReadOnly(false)]
    [DisplayName("字体设置")]
    [Description("设置控件中字体的样式")]
    public Font Font
    {
        get
        {
            return theCheckBox.Font;
        }
        set
        {
            theCheckBox.Font = value;
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
            return theCheckBox.ForeColor;
        }
        set
        {
            theCheckBox.ForeColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("按钮背景色")]
    [Category("外观")]
    [Description("设置或获取控件的背景色")]
    public Color BackColor
    {
        get
        {
            return theCheckBox.BackColor;
        }
        set
        {
            theCheckBox.BackColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("复选框是否为选中")]
    [Category("外观")]
    [Description("设置或获取控件的选中状态")]
    public bool Checked
    {
        get
        {
            return theCheckBox.Checked;
        }
        set
        {
            theCheckBox.Checked = value;
        }
    }

    [Category("外观")]
    [DisplayName("复选框文本内容")]
    [ReadOnly(false)]
    [Description("设置或获取控件的文本")]
    public string Text
    {
        get
        {
            return theCheckBox.Text;
        }
        set
        {
            theCheckBox.Text = value;
        }
    }

    [DisplayName("复选框位置")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("设置或获取控件的布局")]
    public Point Location
    {
        get
        {
            return theCheckBox.Location;
        }
        set
        {
            theCheckBox.Location = value;
        }
    }

    [Category("布局")]
    [ReadOnly(false)]
    [DisplayName("复选框大小")]
    [Description("设置或获取控件的大小")]
    public Size Size
    {
        get
        {
            return theCheckBox.Size;
        }
        set
        {
            theCheckBox.Size = value;
        }
    }

    [Category("行为")]
    [DisplayName("Tab键顺序")]
    [ReadOnly(false)]
    [Description("设置或获取控件的Tab键顺序")]
    public int TabIndex
    {
        get
        {
            return theCheckBox.TabIndex;
        }
        set
        {
            theCheckBox.TabIndex = value;
        }
    }

    [DisplayName("控件可见性")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("设置或获取控件的可见性")]
    public bool Visible
    {
        get
        {
            return theCheckBox.Visible;
        }
        set
        {
            theCheckBox.Visible = value;
        }
    }

    public HTML5_CCheckBoxProperty()
    {
    }

    public HTML5_CCheckBoxProperty(CCheckBox _theCheckBox)
    {
        theCheckBox = _theCheckBox;
    }
}
