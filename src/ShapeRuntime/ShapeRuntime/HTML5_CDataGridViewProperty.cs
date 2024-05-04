using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class HTML5_CDataGridViewProperty
{
    [NonSerialized]
    public CDataGridView theDataGirdView;

    [ReadOnly(false)]
    [DisplayName("名称")]
    [Category("设计")]
    [Description("设定控件名称。")]
    public string ID
    {
        get
        {
            return theDataGirdView.ID;
        }
        set
        {
            theDataGirdView.ID = value;
        }
    }

    [Description("设置数据视图包含的列数。")]
    [Category("数据")]
    [DisplayName("设置列数")]
    [ReadOnly(false)]
    public int SetColumnCount
    {
        get
        {
            return theDataGirdView.SetColumnCount;
        }
        set
        {
            theDataGirdView.SetColumnCount = value;
        }
    }

    [DisplayName("设置行数")]
    [Category("数据")]
    [Description("设置数据视图包含的行数。")]
    [ReadOnly(false)]
    public int SetRowCount
    {
        get
        {
            return theDataGirdView.SetRowCount;
        }
        set
        {
            theDataGirdView.SetRowCount = value;
        }
    }

    [Category("设计")]
    [DisplayName("IIS发布外部边框")]
    [Description("IIS发布外部边框")]
    [ReadOnly(false)]
    public bool HTML5_Border
    {
        get
        {
            return theDataGirdView.HTML5_Border;
        }
        set
        {
            theDataGirdView.HTML5_Border = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("水平滚动条")]
    [Category("设计")]
    [Description("是否设置水平滚动条")]
    public bool HTML5_HScroll
    {
        get
        {
            return theDataGirdView.HTML5_HScroll;
        }
        set
        {
            theDataGirdView.HTML5_HScroll = value;
        }
    }

    [Category("外观")]
    [DisplayName("字体设置")]
    [ReadOnly(false)]
    [Description("设置控件中字体的样式")]
    public Font Font
    {
        get
        {
            return theDataGirdView.Font;
        }
        set
        {
            theDataGirdView.Font = value;
        }
    }

    [DisplayName("控件位置")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("控件的位置")]
    public Point Location
    {
        get
        {
            return theDataGirdView.Location;
        }
        set
        {
            theDataGirdView.Location = value;
        }
    }

    [DisplayName("控件大小")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("控件的布局")]
    public Size Size
    {
        get
        {
            return theDataGirdView.Size;
        }
        set
        {
            theDataGirdView.Size = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("控件背景颜色")]
    [Category("外观")]
    [Description("设置控件背景颜色。")]
    public Color BackgroundColor
    {
        get
        {
            return theDataGirdView.BackgroundColor;
        }
        set
        {
            theDataGirdView.BackgroundColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("单元格边框样式")]
    [Category("外观")]
    [Description("单元格边框样式。")]
    public DataGridViewCellBorderStyle CellBorderStyle
    {
        get
        {
            return theDataGirdView.CellBorderStyle;
        }
        set
        {
            theDataGirdView.CellBorderStyle = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("是否显示标题行")]
    [Category("外观")]
    [Description("是否显示标题行")]
    public bool ColumnHeadersVisible
    {
        get
        {
            return theDataGirdView.ColumnHeadersVisible;
        }
        set
        {
            theDataGirdView.ColumnHeadersVisible = value;
        }
    }

    [Category("外观")]
    [DisplayName("控件表格颜色")]
    [ReadOnly(false)]
    [Description("控件中表格的颜色")]
    public Color GridColor
    {
        get
        {
            return theDataGirdView.GridColor;
        }
        set
        {
            theDataGirdView.GridColor = value;
        }
    }

    [DisplayName("允许用户改变列宽")]
    [Category("设计")]
    [Description("是否允许用户改变列宽。")]
    [ReadOnly(false)]
    public bool AllowUserToResizeColumns
    {
        get
        {
            return theDataGirdView.AllowUserToResizeColumns;
        }
        set
        {
            theDataGirdView.AllowUserToResizeColumns = value;
        }
    }

    [DisplayName("允许用户改变行宽")]
    [Category("设计")]
    [Description("是否允许用户改变行宽。")]
    [ReadOnly(false)]
    public bool AllowUserToResizeRows
    {
        get
        {
            return theDataGirdView.AllowUserToResizeRows;
        }
        set
        {
            theDataGirdView.AllowUserToResizeRows = value;
        }
    }

    [Description("是否允许用户多行选取。")]
    [Category("设计")]
    [ReadOnly(false)]
    [DisplayName("多行选取")]
    public bool MultiSelect
    {
        get
        {
            return theDataGirdView.MultiSelect;
        }
        set
        {
            theDataGirdView.MultiSelect = value;
        }
    }

    [Category("行为")]
    [Description("控件的Tab键顺序")]
    [ReadOnly(false)]
    [DisplayName("Tab键顺序")]
    public int TabIndex
    {
        get
        {
            return theDataGirdView.TabIndex;
        }
        set
        {
            theDataGirdView.TabIndex = value;
        }
    }

    [DisplayName("控件是否可用")]
    [Category("行为")]
    [ReadOnly(false)]
    [Description("控件是否可用")]
    public bool Enabled
    {
        get
        {
            return theDataGirdView.Enabled;
        }
        set
        {
            theDataGirdView.Enabled = value;
        }
    }

    [ReadOnly(false)]
    [Category("行为")]
    [DisplayName("控件可见性")]
    [Description("控件是可见的还是隐藏的")]
    public bool Visible
    {
        get
        {
            return theDataGirdView.Visible;
        }
        set
        {
            theDataGirdView.Visible = value;
        }
    }

    public HTML5_CDataGridViewProperty()
    {
    }

    public HTML5_CDataGridViewProperty(CDataGridView _theDataGirdView)
    {
        theDataGirdView = _theDataGirdView;
    }
}
