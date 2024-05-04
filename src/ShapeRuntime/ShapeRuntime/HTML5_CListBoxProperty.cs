using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CListBoxProperty
{
    [NonSerialized]
    public CListBox theListBox;

    [DisplayName("名称")]
    [Category("设计")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
    public string ID
    {
        get
        {
            return theListBox.ID;
        }
        set
        {
            theListBox.ID = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("项数")]
    [Category("数据")]
    [Description("设置列表框包含项行数。")]
    public int SetRowCount
    {
        get
        {
            return theListBox.SetRowCount;
        }
        set
        {
            theListBox.SetRowCount = value;
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
            return theListBox.Font;
        }
        set
        {
            theListBox.Font = value;
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
            return theListBox.ForeColor;
        }
        set
        {
            theListBox.ForeColor = value;
        }
    }

    [Category("外观")]
    [DisplayName("背景色")]
    [Description("设置或获取控件的背景色")]
    [ReadOnly(false)]
    public Color BackColor
    {
        get
        {
            return theListBox.BackColor;
        }
        set
        {
            theListBox.BackColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("边框设置")]
    [Category("外观")]
    [Description("控件是否应该带有边框")]
    public BorderStyle BorderStyle
    {
        get
        {
            return theListBox.BorderStyle;
        }
        set
        {
            theListBox.BorderStyle = value;
        }
    }

    [Category("行为")]
    [DisplayName("列表框列宽")]
    [ReadOnly(false)]
    [Description("设置或获取列表框宽度")]
    public int ColumnWidth
    {
        get
        {
            return theListBox.ColumnWidth;
        }
        set
        {
            theListBox.ColumnWidth = value;
        }
    }

    [DisplayName("列表框是否多行")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("设置一个值，该值指示此控件是否为多行TextBox 控件")]
    public bool MultiColumn
    {
        get
        {
            return theListBox.MultiColumn;
        }
        set
        {
            theListBox.MultiColumn = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("列表框选择模式")]
    [Category("行为")]
    [Description("指示列表框将是单选，多选还是不可选择")]
    public SelectionMode SelectionMode
    {
        get
        {
            return theListBox.SelectionMode;
        }
        set
        {
            theListBox.SelectionMode = value;
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
            return theListBox.TabIndex;
        }
        set
        {
            theListBox.TabIndex = value;
        }
    }

    [Description("设置一个值，该值指示是否显示该控件")]
    [Category("行为")]
    [DisplayName("列表框可见性")]
    [ReadOnly(false)]
    public bool Visible
    {
        get
        {
            return theListBox.Visible;
        }
        set
        {
            theListBox.Visible = value;
        }
    }

    [Description("设置控件的位置（横纵坐标）")]
    [Category("布局")]
    [DisplayName("列表框位置")]
    [ReadOnly(false)]
    public Point Location
    {
        get
        {
            return theListBox.Location;
        }
        set
        {
            theListBox.Location = value;
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
            return theListBox.Size;
        }
        set
        {
            theListBox.Size = value;
        }
    }

    [ReadOnly(false)]
    [Category("数据")]
    [Description("设置控件的项目、条款、 消息、情报等的一则, 一条")]
    [DisplayName("列表框中的项")]
    public ListBox.ObjectCollection Items => theListBox.Items;

    public HTML5_CListBoxProperty()
    {
    }

    public HTML5_CListBoxProperty(CListBox _theListBox)
    {
        theListBox = _theListBox;
    }
}
