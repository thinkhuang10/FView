using System;
using System.ComponentModel;
using System.Drawing;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CLabelProperty
{
    [NonSerialized]
    public CLabel theLabel;

    [DisplayName("名称")]
    [Category("设计")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
    public string ID
    {
        get
        {
            return theLabel.ID;
        }
        set
        {
            theLabel.ID = value;
        }
    }

    [DisplayName("显示文本")]
    [Category("数据")]
    [Description("设置或获取控件的文本值")]
    [ReadOnly(false)]
    public string Text
    {
        get
        {
            return theLabel.Text;
        }
        set
        {
            theLabel.Text = value;
        }
    }

    [Description("设置控件中字体的样式")]
    [Category("外观")]
    [DisplayName("字体设置")]
    [ReadOnly(false)]
    public Font Font
    {
        get
        {
            return theLabel.Font;
        }
        set
        {
            theLabel.Font = value;
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
            return theLabel.ForeColor;
        }
        set
        {
            theLabel.ForeColor = value;
        }
    }

    [Category("外观")]
    [DisplayName("字体背景色")]
    [Description("设置或获取控件的背景色")]
    [ReadOnly(false)]
    public Color BackColor
    {
        get
        {
            return theLabel.BackColor;
        }
        set
        {
            theLabel.BackColor = value;
        }
    }

    [Description("设置控件的位置（横纵坐标）")]
    [Category("布局")]
    [DisplayName("控件位置")]
    [ReadOnly(false)]
    public Point Location
    {
        get
        {
            return theLabel.Location;
        }
        set
        {
            theLabel.Location = value;
        }
    }

    [DisplayName("控件大小")]
    [Category("布局")]
    [Description("设置控件的大小（横纵坐标）")]
    [ReadOnly(false)]
    public Size Size
    {
        get
        {
            return theLabel.Size;
        }
        set
        {
            theLabel.Size = value;
        }
    }

    [Category("外观")]
    [DisplayName("标签对齐方式")]
    [Description("设置标签的文本对齐方式")]
    [ReadOnly(false)]
    public ContentAlignment TextAlign
    {
        get
        {
            return theLabel.TextAlign;
        }
        set
        {
            theLabel.TextAlign = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件是否可见")]
    [Category("行为")]
    [Description("设置一个值，该值指示是否显示该控件")]
    public bool Visible
    {
        get
        {
            return theLabel.Visible;
        }
        set
        {
            theLabel.Visible = value;
        }
    }

    [Category("行为")]
    [DisplayName("控件使能性")]
    [ReadOnly(false)]
    [Description("设置控件的点击使能性。")]
    public bool Enabled
    {
        get
        {
            return theLabel.Enabled;
        }
        set
        {
            theLabel.Enabled = value;
        }
    }

    [DisplayName("Tab顺序索引")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("设置Tab键顺序索引值")]
    public int TabIndex
    {
        get
        {
            return theLabel.TabIndex;
        }
        set
        {
            theLabel.TabIndex = value;
        }
    }

    public HTML5_CLabelProperty()
    {
    }

    public HTML5_CLabelProperty(CLabel _theLabel)
    {
        theLabel = _theLabel;
    }
}
