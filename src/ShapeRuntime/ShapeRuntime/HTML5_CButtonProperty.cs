using System;
using System.ComponentModel;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CButtonProperty
{
    [NonSerialized]
    public CButton theButton;

    [ReadOnly(false)]
    [DisplayName("名称")]
    [Category("设计")]
    [Description("设定控件名称。")]
    public string ID
    {
        get
        {
            return theButton.ID;
        }
        set
        {
            theButton.ID = value;
        }
    }

    [Description("设置控件中字体的样式")]
    [DisplayName("字体设置")]
    [Category("外观")]
    [ReadOnly(false)]
    public Font Font
    {
        get
        {
            return theButton.Font;
        }
        set
        {
            theButton.Font = value;
        }
    }

    [DisplayName("字体颜色")]
    [Category("外观")]
    [Description("设置或获取控件的字体颜色")]
    [ReadOnly(false)]
    public Color ForeColor
    {
        get
        {
            return theButton.ForeColor;
        }
        set
        {
            theButton.ForeColor = value;
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
            return theButton.BackColor;
        }
        set
        {
            theButton.BackColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("按钮大小")]
    [Category("布局")]
    [Description("设置或获取控件的大小（宽和高）")]
    public Size Size
    {
        get
        {
            return theButton.Size;
        }
        set
        {
            theButton.Size = value;
        }
    }

    [Category("布局")]
    [DisplayName("按钮位置")]
    [ReadOnly(false)]
    [Description("设置或获取控件的位置（横纵坐标）")]
    public Point Location
    {
        get
        {
            return theButton.Location;
        }
        set
        {
            theButton.Location = value;
        }
    }

    [DisplayName("按钮文本内容")]
    [ReadOnly(false)]
    [Category("数据")]
    [Description("设置或获取控件的文本值")]
    public string Text
    {
        get
        {
            return theButton.Text;
        }
        set
        {
            theButton.Text = value;
        }
    }

    [Description("获取或设置一个值，该值指示控件是否可以对用户交互作出响应")]
    [DisplayName("文本对齐方式")]
    [Category("数据")]
    [ReadOnly(false)]
    public ContentAlignment TextAlign
    {
        get
        {
            return theButton.TextAlign;
        }
        set
        {
            theButton.TextAlign = value;
        }
    }

    [DisplayName("按钮可见性")]
    [Category("行为")]
    [Description("确定该控件是可见还是隐藏")]
    [ReadOnly(false)]
    public bool Visible
    {
        get
        {
            return theButton.Visible;
        }
        set
        {
            theButton.Visible = value;
        }
    }

    [Description("获取或设置一个值，该值指示控件是否可以对用户交互作出响应")]
    [Category("行为")]
    [DisplayName("按钮是否启用")]
    [ReadOnly(false)]
    public bool Enabled
    {
        get
        {
            return theButton.Enabled;
        }
        set
        {
            theButton.Enabled = value;
        }
    }

    [Description("获取或设置一个值，该值指示控件是否可以对用户交互作出响应")]
    [Category("行为")]
    [DisplayName("Tab键顺序")]
    [ReadOnly(false)]
    public int TabIndex
    {
        get
        {
            return theButton.TabIndex;
        }
        set
        {
            theButton.TabIndex = value;
        }
    }

    public HTML5_CButtonProperty()
    {
    }

    public HTML5_CButtonProperty(CButton _theButton)
    {
        theButton = _theButton;
    }
}
