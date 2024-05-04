using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("6E501CCA-D836-4156-B457-FEF07ABB25B4")]
[ComVisible(true)]
public class CDataGridView : DataGridView, IDCCEControl, IControlShape, IDBAnimation, ISupportHtml5
{
    private string id = "";

    private bool _html5_border;

    private bool _html5_HScroll;

    private bool Runing;

    public int ClickRowIndex;

    public int ClickColumnIndex;

    private readonly Dictionary<int, bool> isOrderByNumberColumnDict = new();

    private DataGridViewRow oldrow;

    private bool enableEdit;

    private int refreshTime;

    public bool dbmultoperation;

    public List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimations = new();

    public bool newtable;

    public bool ansyncnewtable;

    public string newtableSQL = "";

    public byte[] newtableOtherData;

    public List<int> newtableSafeRegion = new();

    public bool dbselect;

    public bool ansyncselect;

    public string dbselectSQL = "";

    public string dbselectTO = "";

    public byte[] dbselectOtherData;

    public List<int> dbselectSafeRegion = new();

    public bool dbinsert;

    public bool ansyncinsert;

    public string dbinsertSQL = "";

    public byte[] dbinsertOtherData;

    public List<int> dbinsertSafeRegion = new();

    public bool dbupdate;

    public bool ansyncupdate;

    public string dbupdateSQL = "";

    public byte[] dbupdateOtherData;

    public List<int> dbupdateSafeRegion = new();

    public bool dbdelete;

    public bool ansyncdelete;

    public string dbdeleteSQL = "";

    public byte[] dbdeleteOtherData;

    public List<int> dbdeleteSafeRegion = new();

    private int dBResult = -1;

    [DisplayName("名称")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [Category("设计")]
    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
        }
    }

    [ReadOnly(false)]
    [Category("设计")]
    [DisplayName("整行选取模式")]
    [Description("数据是否是整行选取模式。")]
    public bool FullRowSelect
    {
        get
        {
            return SelectionMode == DataGridViewSelectionMode.FullRowSelect;
        }
        set
        {
            if (value)
            {
                SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            else
            {
                SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public CDataGridView Instance => this;

    [ReadOnly(false)]
    [DisplayName("单元格内容的位置")]
    [Category("设计")]
    [Description("设置单元格内容的位置。\r\n NotSet = 0,   未设定对齐方式。\r\n TopLeft = 1,内容与单元格的顶部垂直对齐，与单元格的左侧水平对齐。\r\n TopCenter = 2,内容与单元格的顶部垂直对齐，与单元格的中间水平对齐。\r\n TopRight = 4,内容与单元格的顶部垂直对齐，与单元格的右侧水平对齐。\r\n MiddleLeft = 16,内容与单元格的中间垂直对齐，与单元格的左侧水平对齐。\r\n MiddleCenter = 32,内容与单元格的垂直和水平中心对齐。\r\n MiddleRight = 64,内容与单元格的中间垂直对齐，与单元格的右侧水平对齐。\r\n BottomLeft = 256,内容与单元格的底部垂直对齐，与单元格的左侧水平对齐。\r\n BottomCenter = 512,内容与单元格的底部垂直对齐，与单元格的中间水平对齐。\r\n BottomRight = 1024, 内容与单元格的底部垂直对齐，与单元格的右侧水平对齐。")]
    public DataGridViewContentAlignment Alignment
    {
        get
        {
            return RowTemplate.DefaultCellStyle.Alignment;
        }
        set
        {
            RowTemplate.DefaultCellStyle.Alignment = value;
        }
    }

    [DisplayName("IIS发布外部边框")]
    [Description("IIS发布外部边框")]
    [ReadOnly(false)]
    [Category("设计")]
    public bool HTML5_Border
    {
        get
        {
            return _html5_border;
        }
        set
        {
            _html5_border = value;
        }
    }

    [Description("是否设置水平滚动条")]
    [ReadOnly(false)]
    [DisplayName("IIS发布水平滚动条")]
    [Category("设计")]
    public bool HTML5_HScroll
    {
        get
        {
            return _html5_HScroll;
        }
        set
        {
            _html5_HScroll = value;
        }
    }

    [Category("设计")]
    [Description("数据是否是列填充模式。")]
    [DisplayName("列填充模式")]
    [ReadOnly(false)]
    public bool FullFill
    {
        get
        {
            return base.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill;
        }
        set
        {
            if (value)
            {
                base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
        }
    }

    [Browsable(false)]
    public bool isRuning
    {
        get
        {
            return Runing;
        }
        set
        {
            Runing = value;
        }
    }

    [Browsable(false)]
    public int BColor
    {
        get
        {
            return base.BackgroundColor.ToArgb();
        }
        set
        {
            base.BackgroundColor = Color.FromArgb(value);
        }
    }

    [Browsable(false)]
    public int FColor
    {
        get
        {
            return base.ForeColor.ToArgb();
        }
        set
        {
            base.ForeColor = Color.FromArgb(value);
        }
    }

    [Category("外观")]
    [DisplayName("控件字体")]
    [Description("控件中字体的样式")]
    [ReadOnly(false)]
    public Font TextFont
    {
        get
        {
            return DefaultCellStyle.Font;
        }
        set
        {
            DefaultCellStyle.Font = value;
        }
    }

    [Category("布局")]
    [DisplayName("位置")]
    [ReadOnly(false)]
    [Description("控件左上角相对于其容器左上角的坐标")]
    public new Point Location
    {
        get
        {
            return base.Location;
        }
        set
        {
            base.Location = value;
        }
    }

    [DisplayName("滚动条类型")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("要为DataGridView控件显示的滚动条的类型")]
    public new ScrollBars ScrollBars
    {
        get
        {
            return base.ScrollBars;
        }
        set
        {
            base.ScrollBars = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("大小")]
    [Description("控件的大小（以像素为单位）")]
    [Category("布局")]
    public new Size Size
    {
        get
        {
            return base.Size;
        }
        set
        {
            base.Size = value;
        }
    }

    [DisplayName("列自动调整模式")]
    [ReadOnly(false)]
    [Category("布局")]
    [Description("确定可见列的自动调整大小模式")]
    public new DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
        get
        {
            return base.AutoSizeColumnsMode;
        }
        set
        {
            base.AutoSizeColumnsMode = value;
        }
    }

    [ReadOnly(false)]
    [Description("确定可见行的自动调整大小模式")]
    [Category("布局")]
    [DisplayName("行自动调整模式")]
    public new DataGridViewAutoSizeRowsMode AutoSizeRowsMode
    {
        get
        {
            return base.AutoSizeRowsMode;
        }
        set
        {
            base.AutoSizeRowsMode = value;
        }
    }

    [Browsable(false)]
    public new AnchorStyles Anchor
    {
        get
        {
            return base.Anchor;
        }
        set
        {
            base.Anchor = value;
        }
    }

    [Browsable(false)]
    public new DockStyle Dock
    {
        get
        {
            return base.Dock;
        }
        set
        {
            base.Dock = value;
        }
    }

    [Browsable(false)]
    public new Padding Margin
    {
        get
        {
            return base.Margin;
        }
        set
        {
            base.Margin = value;
        }
    }

    [Browsable(false)]
    public new Size MaximumSize
    {
        get
        {
            return base.MaximumSize;
        }
        set
        {
            base.MaximumSize = value;
        }
    }

    [Browsable(false)]
    public new Size MinimumSize
    {
        get
        {
            return base.MinimumSize;
        }
        set
        {
            base.MinimumSize = value;
        }
    }

    [Browsable(false)]
    public new int RowHeadersWidth
    {
        get
        {
            return base.RowHeadersWidth;
        }
        set
        {
            base.RowHeadersWidth = value;
        }
    }

    [Browsable(false)]
    public new bool CausesValidation
    {
        get
        {
            return base.CausesValidation;
        }
        set
        {
            base.CausesValidation = value;
        }
    }

    [Browsable(false)]
    public new string AccessibleName
    {
        get
        {
            return base.AccessibleName;
        }
        set
        {
            base.AccessibleName = value;
        }
    }

    [Browsable(false)]
    public new AccessibleRole AccessibleRole
    {
        get
        {
            return base.AccessibleRole;
        }
        set
        {
            base.AccessibleRole = value;
        }
    }

    [Browsable(false)]
    public new string AccessibleDescription
    {
        get
        {
            return base.AccessibleDescription;
        }
        set
        {
            base.AccessibleDescription = value;
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [Description("DataGridView的背景色")]
    [DisplayName("背景色")]
    public new Color BackgroundColor
    {
        get
        {
            return base.BackgroundColor;
        }
        set
        {
            base.BackgroundColor = value;
        }
    }

    [Category("外观")]
    [DisplayName("边框样式")]
    [Description("DataGridView的边框样式")]
    [ReadOnly(false)]
    public new BorderStyle BorderStyle
    {
        get
        {
            return base.BorderStyle;
        }
        set
        {
            base.BorderStyle = value;
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [DisplayName("单元格边框样式")]
    [Description("DataGridView的单元格边框样式")]
    public new DataGridViewCellBorderStyle CellBorderStyle
    {
        get
        {
            return base.CellBorderStyle;
        }
        set
        {
            base.CellBorderStyle = value;
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [DisplayName("列标题高度")]
    [Description("列标题行的高度(以像素为单位)")]
    public new int ColumnHeadersHeight
    {
        get
        {
            return base.ColumnHeadersHeight;
        }
        set
        {
            base.ColumnHeadersHeight = value;
        }
    }

    [Category("外观")]
    [Description("指示是否显示列标题行")]
    [ReadOnly(false)]
    [DisplayName("列标题可见性")]
    public new bool ColumnHeadersVisible
    {
        get
        {
            return base.ColumnHeadersVisible;
        }
        set
        {
            base.ColumnHeadersVisible = value;
        }
    }

    [DisplayName("光标")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("指针移动过该控件时显示的光标")]
    public new Cursor Cursor
    {
        get
        {
            return base.Cursor;
        }
        set
        {
            base.Cursor = value;
        }
    }

    [Description("指示在为应用程序启用视觉样式的情况下，行标题和列标题是否使用用户当前主题的视觉样式")]
    [DisplayName("启用行列标题视觉样式")]
    [Category("外观")]
    [ReadOnly(false)]
    public new bool EnableHeadersVisualStyles
    {
        get
        {
            return base.EnableHeadersVisualStyles;
        }
        set
        {
            base.EnableHeadersVisualStyles = value;
        }
    }

    [DisplayName("行标题可见性")]
    [Description("指示是否显示包含行标题的列")]
    [Category("外观")]
    [ReadOnly(false)]
    public new bool RowHeadersVisible
    {
        get
        {
            return base.RowHeadersVisible;
        }
        set
        {
            base.RowHeadersVisible = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewCellStyle RowsDefaultCellStyle
    {
        get
        {
            return base.RowsDefaultCellStyle;
        }
        set
        {
            base.RowsDefaultCellStyle = value;
        }
    }

    [Browsable(false)]
    public new bool UseWaitCursor
    {
        get
        {
            return base.UseWaitCursor;
        }
        set
        {
            base.UseWaitCursor = value;
        }
    }

    [Browsable(false)]
    public new bool ShowRowErrors
    {
        get
        {
            return base.ShowRowErrors;
        }
        set
        {
            base.ShowRowErrors = value;
        }
    }

    [Browsable(false)]
    public new bool ShowEditingIcon
    {
        get
        {
            return base.ShowEditingIcon;
        }
        set
        {
            base.ShowEditingIcon = value;
        }
    }

    [Browsable(false)]
    public new bool ShowCellToolTips
    {
        get
        {
            return base.ShowCellToolTips;
        }
        set
        {
            base.ShowCellToolTips = value;
        }
    }

    [Browsable(false)]
    public new bool ShowCellErrors
    {
        get
        {
            return base.ShowCellErrors;
        }
        set
        {
            base.ShowCellErrors = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewRow RowTemplate
    {
        get
        {
            return base.RowTemplate;
        }
        set
        {
            base.RowTemplate = value;
        }
    }

    [Browsable(false)]
    public new Color GridColor
    {
        get
        {
            return base.GridColor;
        }
        set
        {
            base.GridColor = value;
        }
    }

    [Browsable(false)]
    public new RightToLeft RightToLeft
    {
        get
        {
            return base.RightToLeft;
        }
        set
        {
            base.RightToLeft = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewCellStyle DefaultCellStyle
    {
        get
        {
            return base.DefaultCellStyle;
        }
        set
        {
            base.DefaultCellStyle = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewHeaderBorderStyle RowHeadersBorderStyle
    {
        get
        {
            return base.RowHeadersBorderStyle;
        }
        set
        {
            base.RowHeadersBorderStyle = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewCellStyle RowHeadersDefaultCellStyle
    {
        get
        {
            return base.RowHeadersDefaultCellStyle;
        }
        set
        {
            base.RowHeadersDefaultCellStyle = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle
    {
        get
        {
            return base.ColumnHeadersBorderStyle;
        }
        set
        {
            base.ColumnHeadersBorderStyle = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewCellStyle ColumnHeadersDefaultCellStyle
    {
        get
        {
            return base.ColumnHeadersDefaultCellStyle;
        }
        set
        {
            base.ColumnHeadersDefaultCellStyle = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewCellStyle AlternatingRowsDefaultCellStyle
    {
        get
        {
            return base.AlternatingRowsDefaultCellStyle;
        }
        set
        {
            base.AlternatingRowsDefaultCellStyle = value;
        }
    }

    [Browsable(false)]
    public int SetColumnCount
    {
        get
        {
            return base.ColumnCount;
        }
        set
        {
            if (base.ColumnCount < value)
            {
                for (int i = base.ColumnCount; i < value; i++)
                {
                    Columns.Add("", "");
                }
            }
            else if (base.ColumnCount > value)
            {
                for (int num = base.ColumnCount; num > value; num--)
                {
                    Columns.RemoveAt(Columns.Count - 1);
                }
            }
        }
    }

    [Browsable(false)]
    public int SetRowCount
    {
        get
        {
            return base.RowCount;
        }
        set
        {
            if (base.RowCount < value)
            {
                for (int i = base.RowCount; i < value; i++)
                {
                    base.Rows.Add();
                }
            }
            else if (base.RowCount > value)
            {
                for (int num = base.RowCount; num > value; num--)
                {
                    base.Rows.RemoveAt(base.Rows.Count - 1);
                }
            }
        }
    }

    [Browsable(false)]
    public string SetBindValue
    {
        get
        {
            if (SelectedCell.StartsWith("#Bind:"))
            {
                return SelectedCell.Substring(6);
            }
            return "";
        }
        set
        {
            if (value != "")
            {
                SelectedCell = "#Bind:" + value;
            }
            else
            {
                SelectedCell = "";
            }
        }
    }

    [Browsable(false)]
    public string SelectedCell
    {
        get
        {
            try
            {
                if (base.SelectedCells.Count == 0)
                {
                    return "";
                }
                return base.SelectedCells[0].Value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        set
        {
            try
            {
                base.SelectedCells[0].Value = value;
            }
            catch (Exception)
            {
            }
        }
    }

    [Browsable(false)]
    public bool EnableEdit
    {
        get
        {
            return enableEdit;
        }
        set
        {
            enableEdit = value;
        }
    }

    [Browsable(false)]
    public string SetColumnName
    {
        get
        {
            try
            {
                return base.SelectedCells[0].OwningColumn.HeaderText;
            }
            catch (Exception)
            {
            }
            return "";
        }
        set
        {
            try
            {
                base.SelectedCells[0].OwningColumn.HeaderText = value;
            }
            catch
            {
            }
        }
    }

    [Browsable(false)]
    public int SetRefreshTime
    {
        get
        {
            return refreshTime;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            refreshTime = value;
        }
    }

    [Browsable(false)]
    public new object Tag
    {
        get
        {
            return base.Tag;
        }
        set
        {
            base.Tag = value;
        }
    }

    [Browsable(false)]
    public new object DataSource
    {
        get
        {
            return base.DataSource;
        }
        set
        {
            base.DataSource = value;
        }
    }

    [Browsable(false)]
    public new string DataMember
    {
        get
        {
            return base.DataMember;
        }
        set
        {
            base.DataMember = value;
        }
    }

    [Browsable(false)]
    public new ControlBindingsCollection DataBindings => base.DataBindings;

    [DisplayName("多行选择")]
    [ReadOnly(false)]
    [Category("行为")]
    [Description("指示用户一次是否可以选择DataGridView的多个单元格、行或列")]
    public new bool MultiSelect
    {
        get
        {
            return base.MultiSelect;
        }
        set
        {
            base.MultiSelect = value;
        }
    }

    [DisplayName("可见性")]
    [Description("DataGridView的背景色")]
    [ReadOnly(false)]
    [Category("行为")]
    public new bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
        }
    }

    [Browsable(false)]
    public new bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
    {
        get
        {
            return base.RowHeadersWidthSizeMode;
        }
        set
        {
            base.RowHeadersWidthSizeMode = value;
        }
    }

    [Browsable(false)]
    public new bool ReadOnly
    {
        get
        {
            return base.ReadOnly;
        }
        set
        {
            base.ReadOnly = value;
        }
    }

    [Browsable(false)]
    public new bool AllowDrop
    {
        get
        {
            return base.AllowDrop;
        }
        set
        {
            base.AllowDrop = value;
        }
    }

    [Browsable(false)]
    public new bool AllowUserToAddRows
    {
        get
        {
            return base.AllowUserToAddRows;
        }
        set
        {
            base.AllowUserToAddRows = value;
        }
    }

    [Browsable(false)]
    public new bool AllowUserToDeleteRows
    {
        get
        {
            return base.AllowUserToDeleteRows;
        }
        set
        {
            base.AllowUserToDeleteRows = value;
        }
    }

    [Browsable(false)]
    public new bool AllowUserToOrderColumns
    {
        get
        {
            return base.AllowUserToOrderColumns;
        }
        set
        {
            base.AllowUserToOrderColumns = value;
        }
    }

    [Browsable(false)]
    public new bool AllowUserToResizeRows
    {
        get
        {
            return base.AllowUserToResizeRows;
        }
        set
        {
            base.AllowUserToResizeRows = value;
        }
    }

    [Browsable(false)]
    public new bool AllowUserToResizeColumns
    {
        get
        {
            return base.AllowUserToResizeColumns;
        }
        set
        {
            base.AllowUserToResizeColumns = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewClipboardCopyMode ClipboardCopyMode
    {
        get
        {
            return base.ClipboardCopyMode;
        }
        set
        {
            base.ClipboardCopyMode = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
    {
        get
        {
            return base.ColumnHeadersHeightSizeMode;
        }
        set
        {
            base.ColumnHeadersHeightSizeMode = value;
        }
    }

    [Browsable(false)]
    public new ContextMenuStrip ContextMenuStrip
    {
        get
        {
            return base.ContextMenuStrip;
        }
        set
        {
            base.ContextMenuStrip = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewEditMode EditMode
    {
        get
        {
            return base.EditMode;
        }
        set
        {
            base.EditMode = value;
        }
    }

    [Browsable(false)]
    public new bool VirtualMode
    {
        get
        {
            return base.VirtualMode;
        }
        set
        {
            base.VirtualMode = value;
        }
    }

    [Browsable(false)]
    public new int TabIndex
    {
        get
        {
            return base.TabIndex;
        }
        set
        {
            base.TabIndex = value;
        }
    }

    [Browsable(false)]
    public new bool TabStop
    {
        get
        {
            return base.TabStop;
        }
        set
        {
            base.TabStop = value;
        }
    }

    [Browsable(false)]
    public new bool StandardTab
    {
        get
        {
            return base.StandardTab;
        }
        set
        {
            base.StandardTab = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewSelectionMode SelectionMode
    {
        get
        {
            return base.SelectionMode;
        }
        set
        {
            base.SelectionMode = value;
        }
    }

    [Browsable(false)]
    public new ImeMode ImeMode
    {
        get
        {
            return base.ImeMode;
        }
        set
        {
            base.ImeMode = value;
        }
    }

    [Browsable(false)]
    public new DataGridViewColumnCollection Columns => base.Columns;

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbmultoperation
    {
        get
        {
            return dbmultoperation;
        }
        set
        {
            dbmultoperation = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimationList
    {
        get
        {
            return DBAnimations;
        }
        set
        {
            DBAnimations = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Newtable
    {
        get
        {
            return newtable;
        }
        set
        {
            newtable = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Ansyncnewtable
    {
        get
        {
            return ansyncnewtable;
        }
        set
        {
            ansyncnewtable = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string NewtableSQL
    {
        get
        {
            return newtableSQL;
        }
        set
        {
            newtableSQL = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public byte[] NewtableOtherData
    {
        get
        {
            return newtableOtherData;
        }
        set
        {
            newtableOtherData = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public List<int> NewtableSafeRegion
    {
        get
        {
            return newtableSafeRegion;
        }
        set
        {
            newtableSafeRegion = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbselect
    {
        get
        {
            return dbselect;
        }
        set
        {
            dbselect = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncselect
    {
        get
        {
            return ansyncselect;
        }
        set
        {
            ansyncselect = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbselectSQL
    {
        get
        {
            return dbselectSQL;
        }
        set
        {
            dbselectSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string DbselectTO
    {
        get
        {
            return dbselectTO;
        }
        set
        {
            dbselectTO = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public byte[] DbselectOtherData
    {
        get
        {
            return dbselectOtherData;
        }
        set
        {
            dbselectOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<int> DbselectSafeRegion
    {
        get
        {
            return dbselectSafeRegion;
        }
        set
        {
            dbselectSafeRegion = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbinsert
    {
        get
        {
            return dbinsert;
        }
        set
        {
            dbinsert = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Ansyncinsert
    {
        get
        {
            return ansyncinsert;
        }
        set
        {
            ansyncinsert = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string DbinsertSQL
    {
        get
        {
            return dbinsertSQL;
        }
        set
        {
            dbinsertSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] DbinsertOtherData
    {
        get
        {
            return dbinsertOtherData;
        }
        set
        {
            dbinsertOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<int> DbinsertSafeRegion
    {
        get
        {
            return dbinsertSafeRegion;
        }
        set
        {
            dbinsertSafeRegion = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbupdate
    {
        get
        {
            return dbupdate;
        }
        set
        {
            dbupdate = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncupdate
    {
        get
        {
            return ansyncupdate;
        }
        set
        {
            ansyncupdate = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbupdateSQL
    {
        get
        {
            return dbupdateSQL;
        }
        set
        {
            dbupdateSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] DbupdateOtherData
    {
        get
        {
            return dbupdateOtherData;
        }
        set
        {
            dbupdateOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<int> DbupdateSafeRegion
    {
        get
        {
            return dbupdateSafeRegion;
        }
        set
        {
            dbupdateSafeRegion = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbdelete
    {
        get
        {
            return dbdelete;
        }
        set
        {
            dbdelete = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Ansyncdelete
    {
        get
        {
            return ansyncdelete;
        }
        set
        {
            ansyncdelete = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbdeleteSQL
    {
        get
        {
            return dbdeleteSQL;
        }
        set
        {
            dbdeleteSQL = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public byte[] DbdeleteOtherData
    {
        get
        {
            return dbdeleteOtherData;
        }
        set
        {
            dbdeleteOtherData = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public List<int> DbdeleteSafeRegion
    {
        get
        {
            return dbdeleteSafeRegion;
        }
        set
        {
            dbdeleteSafeRegion = value;
        }
    }

    [Browsable(false)]
    public int DBResult
    {
        get
        {
            return dBResult;
        }
        set
        {
            dBResult = value;
        }
    }

    public event EventHandler IDChanged;

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler SelectedRowChanged;

    public event EventHandler SelectedCellChanged;

    public event EventHandler OnCellDoubleClicked;

    public event EventHandler OnCellClicked;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler DBOperationErr;

    public event EventHandler DBOperationOK;

    public event EventHandler CheckClick;

    public Bitmap GetLogo()
    {
        return null;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            foreach (DataGridViewRow item in (IEnumerable)base.Rows)
            {
                foreach (DataGridViewCell cell in item.Cells)
                {
                    if (cell.Tag.ToString().StartsWith("#Bind:"))
                    {
                        cell.Value = this.GetValueEvent("[" + cell.Tag.ToString().Substring(6) + "]");
                    }
                }
            }
        }
        catch
        {
        }
    }

    private bool InitCurrentCulture()
    {
        try
        {
            if (Thread.CurrentThread.CurrentCulture.Name != "zh-CN")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("zh-CN");
            }
            return true;
        }
        catch
        {
            MessageBox.Show("设置当前控件运行环境出现异常！", "提示");
            return false;
        }
    }

    public CDataGridView()
    {
        ForeColor = Color.Black;
        RowHeadersVisible = false;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        base.CurrentCellChanged += CDataGridView_CurrentCellChanged;
        base.CellDoubleClick += CDataGridView_CellDoubleClick;
        base.CellClick += CDataGridView_CellClick;
        base.CellMouseClick += CDataGridView_CellMouseClick;
        base.SortCompare += CDataGridView_SortCompare;
        InitCurrentCulture();
    }

    public void SetColumnSortbyNum(int columnIndex)
    {
        if (!isOrderByNumberColumnDict.ContainsKey(columnIndex))
        {
            isOrderByNumberColumnDict.Add(columnIndex, value: true);
        }
        else
        {
            isOrderByNumberColumnDict[columnIndex] = true;
        }
    }

    public void SetColumnSortbyString(int columnIndex)
    {
        if (!isOrderByNumberColumnDict.ContainsKey(columnIndex))
        {
            isOrderByNumberColumnDict.Add(columnIndex, value: false);
        }
        else
        {
            isOrderByNumberColumnDict[columnIndex] = false;
        }
    }

    public void ExcelOut()
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "数据导出文件|*.csv|任意文件|*.*"
        };
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        try
        {
            StringBuilder stringBuilder = new();
            foreach (DataGridViewColumn column in Columns)
            {
                stringBuilder.Append(column.HeaderText + ",");
            }
            if (Columns.Count != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            stringBuilder.AppendLine();
            foreach (DataGridViewRow item in (IEnumerable)base.Rows)
            {
                foreach (DataGridViewCell cell in item.Cells)
                {
                    stringBuilder.Append(((cell.Value != null) ? cell.Value.ToString() : "") + ",");
                }
                if (item.Cells.Count != 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                stringBuilder.AppendLine();
            }
            File.WriteAllText(saveFileDialog.FileName, stringBuilder.ToString(), Encoding.GetEncoding("gb2312"));
        }
        catch (Exception ex)
        {
            MessageBox.Show("数据导出失败.\r\n" + ex.Message);
        }
    }

    private void CDataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
    {
        try
        {
            if (isOrderByNumberColumnDict.ContainsKey(e.Column.Index) && isOrderByNumberColumnDict[e.Column.Index])
            {
                e.SortResult = ((Convert.ToDouble((e.CellValue1 == null) ? ((object)0) : e.CellValue1) - Convert.ToDouble((e.CellValue2 == null) ? ((object)0) : e.CellValue2) > 0.0) ? 1 : ((Convert.ToDouble((e.CellValue1 == null) ? ((object)0) : e.CellValue1) - Convert.ToDouble((e.CellValue2 == null) ? ((object)0) : e.CellValue2) < 0.0) ? (-1) : 0));
            }
            else
            {
                e.SortResult = string.Compare(Convert.ToString((e.CellValue1 == null) ? "" : e.CellValue1), Convert.ToString((e.CellValue2 == null) ? "" : e.CellValue2));
            }
        }
        catch
        {
            e.SortResult = string.Compare(Convert.ToString((e.CellValue1 == null) ? "" : e.CellValue1), Convert.ToString((e.CellValue2 == null) ? "" : e.CellValue2));
        }
        e.Handled = true;
    }

    private void CDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (!isRuning && e.Button == MouseButtons.Right)
        {
            string text = this.GetVarTableEvent("");
            if (text != "")
            {
                base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "#Bind:" + text;
            }
        }
    }

    private void CDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        ClickRowIndex = e.RowIndex;
        ClickColumnIndex = e.ColumnIndex;
        if (this.OnCellClicked != null)
        {
            this.OnCellClicked(sender, e);
        }
    }

    private void CDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        ClickRowIndex = e.RowIndex;
        ClickColumnIndex = e.ColumnIndex;
        if (this.OnCellDoubleClicked != null)
        {
            this.OnCellDoubleClicked(sender, e);
        }
    }

    private void CDataGridView_CurrentCellChanged(object sender, EventArgs e)
    {
        try
        {
            if (oldrow == null || (oldrow != null && oldrow != base.CurrentCell.OwningRow))
            {
                oldrow = base.CurrentCell.OwningRow;
                if (this.SelectedRowChanged != null)
                {
                    this.SelectedRowChanged(sender, e);
                }
            }
        }
        catch (Exception)
        {
        }
        if (this.SelectedCellChanged != null)
        {
            this.SelectedCellChanged(sender, e);
        }
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CDataGridViewSaveItems cDataGridViewSaveItems = new()
        {
            BackColor = base.BackgroundColor,
            ForeColor = base.ForeColor,
            TabIndex = TabIndex,
            fullRowSelect = FullRowSelect,
            multiSelect = MultiSelect,
            data = new DataTable(),
            font = TextFont,
            fullfill = FullFill,
            alignment = Alignment,
            CellBorderStyle = base.CellBorderStyle,
            AllowUserToResizeRows = base.AllowUserToResizeRows,
            AllowUserToResizeColumns = base.AllowUserToResizeColumns,
            HideHead = !base.ColumnHeadersVisible,
            GridLineColor = base.GridColor,
            RowCount = base.RowCount,
            ColumnCount = base.ColumnCount,
            html5_border = HTML5_Border,
            html5_HScroll = HTML5_HScroll
        };
        foreach (DataGridViewColumn column in Columns)
        {
            cDataGridViewSaveItems.data.Columns.Add(column.HeaderText).ExtendedProperties.Add("Width", column.Width);
        }
        foreach (DataGridViewRow item in (IEnumerable)base.Rows)
        {
            DataRow dataRow = cDataGridViewSaveItems.data.NewRow();
            for (int i = 0; i < item.Cells.Count; i++)
            {
                if (item.Cells[i].Value == null)
                {
                    dataRow[i] = "";
                }
                else
                {
                    dataRow[i] = item.Cells[i].Value.ToString();
                }
            }
            cDataGridViewSaveItems.data.Rows.Add(dataRow);
        }
        cDataGridViewSaveItems.hide = !Visible;
        cDataGridViewSaveItems.disable = !Enabled;
        cDataGridViewSaveItems.refreshTime = refreshTime;
        cDataGridViewSaveItems.enableEdit = enableEdit;
        cDataGridViewSaveItems.newtable = newtable;
        cDataGridViewSaveItems.newtableSQL = newtableSQL;
        cDataGridViewSaveItems.ansyncnewtable = ansyncnewtable;
        cDataGridViewSaveItems.newtableOtherData = newtableOtherData;
        cDataGridViewSaveItems.newtableSafeRegion = newtableSafeRegion;
        cDataGridViewSaveItems.dbselect = dbselect;
        cDataGridViewSaveItems.dbselectSQL = dbselectSQL;
        cDataGridViewSaveItems.dbselectTO = dbselectTO;
        cDataGridViewSaveItems.dbSelectAnsync = ansyncselect;
        cDataGridViewSaveItems.dbselectSafeRegion = dbselectSafeRegion;
        cDataGridViewSaveItems.dbselectOtherData = dbselectOtherData;
        cDataGridViewSaveItems.dbinsert = dbinsert;
        cDataGridViewSaveItems.dbinsertSQL = dbinsertSQL;
        cDataGridViewSaveItems.dbInsertAnsync = ansyncinsert;
        cDataGridViewSaveItems.dbinsertSafeRegion = dbinsertSafeRegion;
        cDataGridViewSaveItems.dbinsertOtherData = dbinsertOtherData;
        cDataGridViewSaveItems.dbupdate = dbupdate;
        cDataGridViewSaveItems.dbupdateSQL = dbupdateSQL;
        cDataGridViewSaveItems.dbUpdateAnsync = ansyncupdate;
        cDataGridViewSaveItems.dbupdateSafeRegion = dbupdateSafeRegion;
        cDataGridViewSaveItems.dbupdateOtherData = dbupdateOtherData;
        cDataGridViewSaveItems.dbdelete = dbdelete;
        cDataGridViewSaveItems.dbdeleteSQL = dbdeleteSQL;
        cDataGridViewSaveItems.dbDeleteAnsync = ansyncdelete;
        cDataGridViewSaveItems.dbdeleteSafeRegion = dbdeleteSafeRegion;
        cDataGridViewSaveItems.dbdeleteOtherData = dbdeleteOtherData;
        cDataGridViewSaveItems.dbmultoperation = dbmultoperation;
        cDataGridViewSaveItems.DBAnimations = DBAnimations;
        cDataGridViewSaveItems.CellFormat = "G";
        formatter.Serialize(memoryStream, cDataGridViewSaveItems);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(bytes);
            CDataGridViewSaveItems cDataGridViewSaveItems = (CDataGridViewSaveItems)formatter.Deserialize(stream);
            stream.Close();
            newtable = cDataGridViewSaveItems.newtable;
            newtableSQL = cDataGridViewSaveItems.newtableSQL;
            ansyncnewtable = cDataGridViewSaveItems.ansyncnewtable;
            newtableOtherData = cDataGridViewSaveItems.newtableOtherData;
            newtableSafeRegion = cDataGridViewSaveItems.newtableSafeRegion;
            dbselect = cDataGridViewSaveItems.dbselect;
            dbselectSQL = cDataGridViewSaveItems.dbselectSQL;
            dbselectTO = cDataGridViewSaveItems.dbselectTO;
            ansyncselect = cDataGridViewSaveItems.dbSelectAnsync;
            dbselectSafeRegion = cDataGridViewSaveItems.dbselectSafeRegion;
            dbselectOtherData = cDataGridViewSaveItems.dbselectOtherData;
            dbinsert = cDataGridViewSaveItems.dbinsert;
            dbinsertSQL = cDataGridViewSaveItems.dbinsertSQL;
            ansyncinsert = cDataGridViewSaveItems.dbInsertAnsync;
            dbinsertSafeRegion = cDataGridViewSaveItems.dbinsertSafeRegion;
            dbinsertOtherData = cDataGridViewSaveItems.dbinsertOtherData;
            dbupdate = cDataGridViewSaveItems.dbupdate;
            dbupdateSQL = cDataGridViewSaveItems.dbupdateSQL;
            ansyncupdate = cDataGridViewSaveItems.dbUpdateAnsync;
            dbupdateSafeRegion = cDataGridViewSaveItems.dbupdateSafeRegion;
            dbupdateOtherData = cDataGridViewSaveItems.dbupdateOtherData;
            dbdelete = cDataGridViewSaveItems.dbdelete;
            dbdeleteSQL = cDataGridViewSaveItems.dbdeleteSQL;
            ansyncdelete = cDataGridViewSaveItems.dbDeleteAnsync;
            dbdeleteSafeRegion = cDataGridViewSaveItems.dbdeleteSafeRegion;
            dbdeleteOtherData = cDataGridViewSaveItems.dbdeleteOtherData;
            dbmultoperation = cDataGridViewSaveItems.dbmultoperation;
            DBAnimations = cDataGridViewSaveItems.DBAnimations;
            DefaultCellStyle.Format = cDataGridViewSaveItems.CellFormat;
            HTML5_HScroll = cDataGridViewSaveItems.html5_HScroll;
            HTML5_Border = cDataGridViewSaveItems.html5_border;
            Alignment = cDataGridViewSaveItems.alignment;
            FullFill = cDataGridViewSaveItems.fullfill;
            TabIndex = cDataGridViewSaveItems.TabIndex;
            base.BackgroundColor = cDataGridViewSaveItems.BackColor;
            base.ForeColor = cDataGridViewSaveItems.ForeColor;
            FullRowSelect = cDataGridViewSaveItems.fullRowSelect;
            MultiSelect = cDataGridViewSaveItems.multiSelect;
            if (cDataGridViewSaveItems.font != null)
            {
                TextFont = cDataGridViewSaveItems.font;
            }
            base.CellBorderStyle = cDataGridViewSaveItems.CellBorderStyle;
            base.AllowUserToResizeRows = cDataGridViewSaveItems.AllowUserToResizeRows;
            base.AllowUserToResizeColumns = cDataGridViewSaveItems.AllowUserToResizeColumns;
            base.ColumnHeadersVisible = !cDataGridViewSaveItems.HideHead;
            base.GridColor = cDataGridViewSaveItems.GridLineColor;
            Visible = !cDataGridViewSaveItems.hide;
            Enabled = !cDataGridViewSaveItems.disable;
            refreshTime = cDataGridViewSaveItems.refreshTime;
            enableEdit = cDataGridViewSaveItems.enableEdit;
            _ = base.ColumnCount;
            base.ColumnCount = cDataGridViewSaveItems.ColumnCount;
            base.RowCount = cDataGridViewSaveItems.RowCount;
            for (int i = 0; i < cDataGridViewSaveItems.data.Columns.Count; i++)
            {
                DataColumn dataColumn = cDataGridViewSaveItems.data.Columns[i];
                Columns[i].HeaderText = dataColumn.ColumnName;
                if (dataColumn.ExtendedProperties.ContainsKey("Width"))
                {
                    Columns[i].Width = Convert.ToInt32(dataColumn.ExtendedProperties["Width"]);
                }
            }
            for (int j = 0; j < cDataGridViewSaveItems.data.Rows.Count; j++)
            {
                DataRow dataRow = cDataGridViewSaveItems.data.Rows[j];
                for (int k = 0; k < cDataGridViewSaveItems.data.Columns.Count; k++)
                {
                    base.Rows[j].Cells[k].Value = dataRow[k];
                }
            }
            foreach (DataGridViewRow item in (IEnumerable)base.Rows)
            {
                foreach (DataGridViewCell cell in item.Cells)
                {
                    cell.Tag = cell.Value;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }

    public string GetRowTag(int i)
    {
        try
        {
            return base.Rows[i].Tag.ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string GetValue(int i, int j)
    {
        try
        {
            if (base.Rows[i].Cells[j].Value != null)
            {
                return base.Rows[i].Cells[j].Value.ToString();
            }
            return "";
        }
        catch (InvalidOperationException)
        {
            return "";
        }
        catch
        {
            return "";
        }
    }

    public object GetValueObject(int i, int j)
    {
        try
        {
            return base.Rows[i].Cells[j].Value;
        }
        catch (InvalidOperationException)
        {
            return "";
        }
        catch (Exception)
        {
            return "";
        }
    }

    public float GetCellFontSize(int i, int j)
    {
        try
        {
            return base.Rows[i].Cells[j].Style.Font.Size;
        }
        catch (Exception)
        {
            return -1f;
        }
    }

    public bool SetCellFontSize(int i, int j, float size)
    {
        try
        {
            base.Rows[i].Cells[j].Style.Font = new Font(base.Rows[i].Cells[j].Style.Font.FontFamily, size);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetCellColor(int i, int j, int backcolor, int forecolor)
    {
        try
        {
            Color backColor = Color.FromArgb(backcolor);
            base.Rows[i].Cells[j].Style.BackColor = backColor;
            backColor = Color.FromArgb(forecolor);
            base.Rows[i].Cells[j].Style.ForeColor = backColor;
            _ = base.Rows[i].Cells[j];
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool dgv_color(int column, string _a, string _b, string Acolor, string Bcolor)
    {
        try
        {
            Columns[column].Visible = false;
            for (int i = 0; i < base.Rows.Count; i++)
            {
                if (base[column, i].Value.ToString() == _a)
                {
                    Color color = GetColor(Acolor);
                    base.Rows[i].DefaultCellStyle.ForeColor = color;
                }
                else if (base[column, i].Value.ToString() == _b)
                {
                    Color color2 = GetColor(Bcolor);
                    base.Rows[i].DefaultCellStyle.ForeColor = color2;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private Color GetColor(string _a)
    {
        if ("红" == _a)
        {
            return Color.Red;
        }
        if ("蓝" == _a)
        {
            return Color.Blue;
        }
        if ("黑" == _a)
        {
            return Color.Black;
        }
        if ("黄" == _a)
        {
            return Color.Yellow;
        }
        if ("紫" == _a)
        {
            return Color.Purple;
        }
        return Color.Black;
    }

    public bool SetRowColorA(int i, int backcolor, int forecolor)
    {
        return false;
    }

    public bool SetRowColor(int i, int backcolor, int forecolor)
    {
        try
        {
            Color backColor = Color.FromArgb(backcolor);
            base.Rows[i].DefaultCellStyle.BackColor = backColor;
            backColor = Color.FromArgb(forecolor);
            base.Rows[i].DefaultCellStyle.ForeColor = backColor;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetSelectedCell(int i, int j)
    {
        if (base.RowCount > i && base.ColumnCount > j)
        {
            base.CurrentCell = base.Rows[i].Cells[j];
            return true;
        }
        return false;
    }

    public bool SetValueObject(int i, int j, object value)
    {
        try
        {
            base.Rows[i].Cells[j].Value = value;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetValue(int i, int j, string value)
    {
        try
        {
            base.Rows[i].Cells[j].Value = value;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public int InsertRow(int row)
    {
        try
        {
            base.Rows.Insert(row, 1);
            return row;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int AddRow()
    {
        try
        {
            return base.Rows.Add();
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public void AddRowAt(int rownum)
    {
    }

    public int GetRowCount()
    {
        try
        {
            return base.Rows.Count;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool RemoveRow(int i)
    {
        try
        {
            base.Rows.RemoveAt(i);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool RemoveCulRows()
    {
        try
        {
            List<int> list = new();
            foreach (DataGridViewCell selectedCell in base.SelectedCells)
            {
                if (!list.Contains(selectedCell.RowIndex))
                {
                    list.Add(selectedCell.RowIndex);
                }
            }
            foreach (int item in list)
            {
                base.Rows.RemoveAt(item);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int AddColumn(string str)
    {
        try
        {
            return Columns.Add(Guid.NewGuid().ToString(), str);
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool RemoveColumn(int i)
    {
        try
        {
            Columns.RemoveAt(i);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetColumnVisible(int i, bool visible)
    {
        try
        {
            Columns[i].Visible = visible;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetColumnWidth(int i, int width)
    {
        try
        {
            Columns[i].Width = width;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void AllowResizeRows(bool fix)
    {
        AllowUserToResizeRows = fix;
    }

    public void AllowResizeColumns(bool fix)
    {
        AllowUserToResizeColumns = fix;
    }

    public int GetColumnCount()
    {
        try
        {
            return Columns.Count;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int GetSelectRowCount()
    {
        try
        {
            return base.SelectedRows.Count;
        }
        catch
        {
            return -1;
        }
    }

    public int GetSelectRowIndex(int i)
    {
        try
        {
            return base.SelectedRows[i].Index;
        }
        catch
        {
            return -1;
        }
    }

    public int GetCulRow()
    {
        try
        {
            return base.SelectedCells[0].RowIndex;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int GetCulColumn()
    {
        try
        {
            return base.SelectedCells[0].ColumnIndex;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool SetColumnReadOnly(int icolumn, bool IsReadOnly)
    {
        try
        {
            Columns[icolumn].ReadOnly = IsReadOnly;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void SetColumnColor(int i, int forecolor, int backcolor)
    {
    }

    public bool SetSelectedRow(int i, string str)
    {
        try
        {
            base.SelectedRows[0].Cells[i].Value = str;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SetCellColor(int i, int j, int color)
    {
        try
        {
            Color backColor = Color.FromArgb(color);
            base.Rows[i].Cells[j].Style.BackColor = backColor;
            _ = base.Rows[i].Cells[j];
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void AddColumnCheckbox(int columnnum, string columnName)
    {
    }

    public void SetAllEdit()
    {
    }

    public void SetColEdit(int col)
    {
    }

    public void SetCellEdit(int row, int col)
    {
    }

    public void SetCellUnderLine(int row, int col)
    {
    }

    public void SetCheckCellEnable(int row, int col, bool isenable)
    {
    }

    public string makeHTML()
    {
        bool bold = TextFont.Bold;
        string text2 = ((!bold) ? "normal" : "bold");
        bool italic = TextFont.Italic;
        string text3 = ((!italic) ? "normal" : "Italic");
        bool underline = TextFont.Underline;
        string text4 = ((!underline) ? "none" : "underline");
        string text = !HTML5_Border ? "" : ";border-style:solid;border-width:1px";
        string text5 = HTML5_HScroll ? "" : "overflow-x:hidden;";
        StringBuilder stringBuilder = new();
        string text6 = (Visible ? "visible" : "hidden");
        stringBuilder.Append("<div  tabIndex=\"" + base.TabIndex + "\" style=\" z-index:{Z_INDEX_REPLACE_BY_CCONTROL};visibility:" + text6 + ";background-color:" + ColorTranslator.ToHtml(BackgroundColor) + text + ";position:absolute;font-Size:" + TextFont.Size + "pt; font-Style:" + base.Font.Name + "; width:" + base.Width.ToString() + "px;Height:" + base.Height.ToString() + "px;overflow:scroll;" + text5 + "top:" + Location.Y + "px;left:" + Location.X + "px;\">");
        stringBuilder.Append("<table class=\"datagirdview\" id=\"" + id + "\"  width=\"100%\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};background-color:" + ColorTranslator.ToHtml(BackgroundColor) + ";font-Size:" + TextFont.Size + "pt; font-Style:" + text3 + "; font-family:" + TextFont.Name + "; font-Weight:" + text2 + ";text-decoration:" + text4 + ";table-layout:fixed;border-collapse:collapse; border:solid thin black\">");
        if (base.RowCount != 0 && base.ColumnCount != 0)
        {
            for (int i = 0; i < base.RowCount; i++)
            {
                stringBuilder.Append("<tr>");
                for (int j = 0; j < base.ColumnCount; j++)
                {
                    stringBuilder.Append("<td>---</td>");
                }
                stringBuilder.Append("</tr>");
            }
        }
        stringBuilder.Append("</table>");
        stringBuilder.Append("</div>");
        return stringBuilder.ToString();
    }

    public string makeCycleScript()
    {
        StringBuilder stringBuilder = new();
        if (this.requestPropertyBindData != null)
        {
            DataTable dataTable = this.requestPropertyBindData();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    stringBuilder.AppendLine("parent.VarOperation.SetValueByName(\"[\"+pagename+\"." + id + "." + row["PropertyName"].ToString() + "]\",parent.VarOperation.GetValueByName(\"[" + row["Bind"].ToString() + "]\"));");
                }
            }
        }
        return stringBuilder.ToString();
    }

    public string makeStyle()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("");
        return stringBuilder.ToString();
    }

    public string makeScript()
    {
        StringBuilder stringBuilder = new();
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        if (this.requestEventBindDict != null)
        {
            Dictionary<string, List<EventSetItem>> dictionary = this.requestEventBindDict();
            if (dictionary != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    switch (key)
                    {
                        case "DBOperationErr":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "DBOperationOK":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "CellValueChanged":
                            flag4 = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(obj,changeRowIndextemp,ChangeColIndex){");
                            stringBuilder.AppendLine("ChangeRowIndex = parseInt(changeRowIndextemp) -1;");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "Click":
                            flag3 = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(obj,SelectedRowIndextemp,SelectedColIndex){");
                            stringBuilder.AppendLine("SelectedRowIndex = parseInt(SelectedRowIndextemp) -1;");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseEnter":
                            flag = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(obj){");
                            stringBuilder.AppendLine("var trs = $(\"#" + id + " tr\");");
                            stringBuilder.AppendLine("for(var selectrowindextemp=0;selectrowindextemp<trs.size();selectrowindextemp++)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("if(trs[selectrowindextemp]==obj)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor ='DFEBF2'");
                            stringBuilder.AppendLine("var SelectedRowIndex = selectrowindextemp - 1;");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("else{");
                            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor ='';");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseLeave":
                            flag2 = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(obj){");
                            stringBuilder.AppendLine("var trs = $(\"#" + id + " tr\");");
                            stringBuilder.AppendLine("for(var selectrowindextemp=0;selectrowindextemp<trs.size();selectrowindextemp++)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("if(trs[selectrowindextemp]==obj)");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor =''");
                            stringBuilder.AppendLine("var SelectedRowIndex = selectrowindextemp - 1;");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "CheckClick":
                            flag5 = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(rowindex,colindex){");
                            stringBuilder.AppendLine("var SelectedCheckRowIndex = rowindex - 1;");
                            stringBuilder.AppendLine("var SelectedCheckColIndex = colindex;");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                    }
                }
            }
        }
        if (!flag)
        {
            stringBuilder.AppendLine("function " + id + "_event_MouseEnter(obj){");
            stringBuilder.AppendLine("var trs = $(\"#" + id + " tr\");");
            stringBuilder.AppendLine("for(var selectrowindextemp=0;selectrowindextemp<trs.size();selectrowindextemp++)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("if(trs[selectrowindextemp]==obj)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor ='DFEBF2'");
            stringBuilder.AppendLine("var SelectedRowIndex = selectrowindextemp - 1;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("else{");
            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor ='';");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"MouseEnter\"," + id + "_event_MouseEnter)");
        }
        if (!flag2)
        {
            stringBuilder.AppendLine("function " + id + "_event_MouseLeave(obj){");
            stringBuilder.AppendLine("var trs = $(\"#" + id + " tr\");");
            stringBuilder.AppendLine("for(var selectrowindextemp=0;selectrowindextemp<trs.size();selectrowindextemp++)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("if(trs[selectrowindextemp]==obj)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor =''");
            stringBuilder.AppendLine("var SelectedRowIndex = selectrowindextemp - 1;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"MouseLeave\"," + id + "_event_MouseLeave)");
        }
        if (!flag3)
        {
            stringBuilder.AppendLine("function " + id + "_event_Click(obj){");
            stringBuilder.AppendLine("var trs = $(\"#" + id + " tr\");");
            stringBuilder.AppendLine("for(var selectrowindextemp=0;selectrowindextemp<trs.size();selectrowindextemp++)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("if(trs[selectrowindextemp]==obj)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("trs[selectrowindextemp].style.backgroundColor ='DFEBF2'");
            stringBuilder.AppendLine("var SelectedRowIndex = selectrowindextemp - 1;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("else{trs[selectrowindextemp].style.backgroundColor =''}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("if (event.stopPropagation) {event.stopPropagation();}else if (window.event) {window.event.cancelBubble = true;} ");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Click\"," + id + "_event_Click)");
        }
        if (!flag4)
        {
            stringBuilder.AppendLine("function " + id + "_event_CellValueChanged(obj){");
            stringBuilder.AppendLine("//> 这时change没有代码");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"CellValueChanged\"," + id + "_event_CellValueChanged)");
        }
        if (!flag5)
        {
            stringBuilder.AppendLine("function " + id + "_event_CheckClick(obj){");
            stringBuilder.AppendLine("//> 这时checkclick没有代码");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"CheckClick\"," + id + "_event_CheckClick)");
        }
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_DataGridView_DataSource(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_DataSource\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value,col) {set_DataGridView_DataSource(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(row,col,isable) {DataGirdView_SetCheckCellEnable(\"" + id + "\",row,col,isable)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetCheckCellEnable\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(column,visible) {datagirdview_SetColVisible(\"" + id + "\",column,visible)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetColumnVisible\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(columnnum, width) {datagirdview_setcolumnwidth(\"" + id + "\",columnnum, width)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetColumnWidth\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(rownum, color, backcolor) {datagirdview_setrowcolor(\"" + id + "\",rownum, color, backcolor)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetRowColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(rownum, color, backcolor) {datagirdview_SetRowColorA(\"" + id + "\",rownum, color, backcolor)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetRowColorA\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(rowNum,colNum) {return get_DataGirdView_CellText(\"" + id + "\",rowNum,colNum)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"GetValue\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "( rowNum, colNum, Value) {set_DataGirdView_CellText(\"" + id + "\", rowNum, colNum, Value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetValue\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {return get_DataGirdView_RowCount(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"GetRowCount\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {return get_DataGirdView_ColumnCount(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"GetColumnCount\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() { datagirdview_addrow(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddRow\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(rowIndex) {datagirdview_RemoveRowAt(\"" + id + "\",rowIndex)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"RemoveRow\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(colIndex) {datagirdview_RemoveCol(\"" + id + "\",colIndex)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"RemoveColumn\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(colnum,columnName) {DataGirdView_AddColumnCheckbox(\"" + id + "\",colnum,columnName)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddColumnCheckbox\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(index) {DataGirdView_SetStaticColor(\"" + id + "\",index)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetStaticColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {DataGirdView_BindClickAll(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetAllEdit\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(col) {DataGirdView_BindClickColumn(\"" + id + "\",col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetColEdit\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(row,col) {DataGirdView_BindClickCell(\"" + id + "\",row,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetCellEdit\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(row,col,backcolor,forecolor) {datagirdview_SetCellColor(\"" + id + "\",row,col,backcolor,forecolor)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetCellColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(colName) {datagirdview_AddCol(\"" + id + "\",colName)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddColumn\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(colNum,backcolor,forecolor) {datagirdview_setcolumncolor(\"" + id + "\",colNum,backcolor,forecolor)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetColumnColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {DataGirdView_EndEdit()}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"EndEdit\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(row,col) {DataGirdView_CellUnderLine(\"" + id + "\",row,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SetCellUnderLine\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(row) {datagirdview_AddRowAt(\"" + id + "\",row)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddRowAt\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Show(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Show\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Hide(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Hide\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Fire(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Fire\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(left) {datagirdview_SetLeft(\"" + id + "\",left)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Left\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(top) {datagirdview_SetTop(\"" + id + "\",top)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Top\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(width) {datagirdview_SetWidth(\"" + id + "\",width)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Width\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(width) {datagirdview_SetHeight(\"" + id + "\",width)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Height\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Visible(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Visible\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Visible(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Visible\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Tabindex(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_TabIndex\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Tabindex(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_TabIndex\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Font(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Font\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Font(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Font\"," + text + ")");
        return stringBuilder.ToString();
    }

    private void MakeEvent(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
    {
        int num = 0;
        try
        {
            num = eventBindDict[eventName].Count;
        }
        catch (Exception)
        {
        }
        if (num == 0)
        {
            return;
        }
        int num2 = 0;
        sb.AppendLine("\tvar step=\"0\";");
        sb.AppendLine("\tlabelFinish:");
        sb.AppendLine("\twhile(true)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tswitch(step) {");
        sb.AppendLine("\t\tcase \"0\":");
        Regex regex = new("\\[.*?\\]");
        Regex regex2 = new("(\\b\\w+)\\.(\\b\\w+)\\.(\\b\\w+)\\((.*)\\)");
        foreach (EventSetItem item in eventBindDict[eventName])
        {
            string text = item.Condition;
            if (text == null)
            {
                text = "true";
            }
            else
            {
                List<string[]> replaceFunction = new();
                CShape.GetReplaceJSFunStr(regex2, text, ref replaceFunction);
                foreach (string[] item2 in replaceFunction)
                {
                    text = text.Replace(item2[0], "parent.GetPage(\"" + item2[1] + "\")(\"#" + item2[2] + "\").data(\"" + item2[3] + "\")(" + item2[4] + ")");
                }
                text = text.Replace("System.", "parent.");
                List<string> list = new();
                foreach (Match item3 in regex.Matches(text))
                {
                    if (!list.Contains(item3.Value))
                    {
                        list.Add(item3.Value);
                        text = text.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
                    }
                }
                list.Clear();
            }
            if (item.OperationType == "定义标签")
            {
                sb.AppendLine("\t\tcase \"" + item.FromObject + "\":");
            }
            else if (item.OperationType == "跳转标签")
            {
                sb.AppendLine("\t\tcase \"" + num2++ + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("\t\t\t\tstep=\"" + item.FromObject + "\";");
                sb.AppendLine("\t\t\t\tbreak;");
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "变量赋值")
            {
                int num3 = ++num2;
                sb.AppendLine("\t\tcase \"" + num3 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                string text2 = item.FromObject;
                List<string[]> replaceFunction2 = new();
                CShape.GetReplaceJSFunStr(regex2, text2, ref replaceFunction2);
                foreach (string[] item4 in replaceFunction2)
                {
                    text2 = text2.Replace(item4[0], "parent.GetPage(\"" + item4[1] + "\")(\"#" + item4[2] + "\").data(\"" + item4[3] + "\")(" + item4[4] + ")");
                }
                text2 = text2.Replace("System.", "parent.");
                List<string> list2 = new();
                foreach (Match item5 in regex.Matches(text2))
                {
                    if (!list2.Contains(item5.Value))
                    {
                        list2.Add(item5.Value);
                        text2 = text2.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
                    }
                }
                list2.Clear();
                if (item.ToObject.Key != "")
                {
                    sb.AppendLine("parent.VarOperation.SetValueByName(\"[" + item.ToObject.Key + "]\"," + text2 + ")");
                }
                else
                {
                    sb.AppendLine("\t\t\t\t" + text2);
                }
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "属性赋值")
            {
                int num4 = ++num2;
                sb.AppendLine("\t\tcase \"" + num4 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                string text3 = item.FromObject;
                List<string[]> replaceFunction3 = new();
                CShape.GetReplaceJSFunStr(regex2, text3, ref replaceFunction3);
                foreach (string[] item6 in replaceFunction3)
                {
                    text3 = text3.Replace(item6[0], "parent.GetPage(\"" + item6[1] + "\")(\"#" + item6[2] + "\").data(\"" + item6[3] + "\")(" + item6[4] + ")");
                }
                text3 = text3.Replace("System.", "parent.");
                List<string> list3 = new();
                foreach (Match item7 in regex.Matches(text3))
                {
                    if (!list3.Contains(item7.Value))
                    {
                        list3.Add(item7.Value);
                        text3 = text3.Replace(item7.Value, "parent.VarOperation.GetValueByName(\"" + item7.Value + "\")");
                    }
                }
                list3.Clear();
                string[] array = item.ToObject.Key.Split('.');
                sb.AppendLine("\t\t\t\tparent.GetPage(\"" + array[0] + "\")(\"#" + array[1] + "\").data(\"set_" + array[2] + "\")(" + text3 + ");");
                sb.AppendLine("\t\t\t}");
            }
            else if (item.OperationType == "服务器逻辑")
            {
                ServerLogicRequest serverLogicRequest = item.Tag as ServerLogicRequest;
                serverLogicRequest.Id = Guid.NewGuid().ToString();
                Operation.ServerLogicDict.Add(serverLogicRequest.Id, serverLogicRequest);
                int num5 = ++num2;
                sb.AppendLine("\t\tcase \"" + num5 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("var inputData=\"<Input>\";");
                foreach (string key in serverLogicRequest.InputDict.Keys)
                {
                    sb.AppendLine("inputData+=\"<InputItem Id=\\\"" + key + "\\\" Type=\\\"\"+(typeof parent.VarOperation.GetValueByName(\"" + key + "\"))+\"\\\">\"+parent.VarOperation.GetValueByName(\"" + key + "\")+\"</InputItem>\";");
                }
                sb.AppendLine("inputData+=\"</Input>\";");
                sb.AppendLine("var callsl = new parent.ServerLogic();");
                sb.AppendLine("callsl.ExcuteServerLogic(\"" + serverLogicRequest.Id + "\", inputData);");
                sb.AppendLine("\t\t\t}");
            }
            else
            {
                if (!(item.OperationType == "方法调用"))
                {
                    continue;
                }
                int num6 = ++num2;
                sb.AppendLine("\t\tcase \"" + num6 + "\":");
                sb.AppendLine("\t\t\tif(" + text + ")");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("\t\t\t");
                if (item.ToObject.Key == "")
                {
                    string[] array2 = item.FromObject.Split('.');
                    StringBuilder stringBuilder = new();
                    foreach (KVPart<string, string> para in item.Paras)
                    {
                        string text4 = para.Key.Replace("System.", "parent.");
                        List<string> list4 = new();
                        foreach (Match item8 in regex.Matches(text4))
                        {
                            if (!list4.Contains(item8.Value))
                            {
                                list4.Add(item8.Value);
                                text4 = text4.Replace(item8.Value, "parent.VarOperation.GetValueByName(\"" + item8.Value + "\")");
                            }
                        }
                        list4.Clear();
                        stringBuilder.Append("," + text4);
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(0, 1);
                    }
                    sb.AppendLine("parent.GetPage(\"" + array2[0] + "\")(\"#" + array2[1] + "\").data(\"" + array2[2] + "\")(" + stringBuilder.ToString() + ");");
                }
                else
                {
                    string[] array3 = item.FromObject.Split('.');
                    StringBuilder stringBuilder2 = new();
                    foreach (KVPart<string, string> para2 in item.Paras)
                    {
                        string text5 = para2.Key.Replace("System.", "parent.");
                        List<string> list5 = new();
                        foreach (Match item9 in regex.Matches(text5))
                        {
                            if (!list5.Contains(item9.Value))
                            {
                                list5.Add(item9.Value);
                                text5 = text5.Replace(item9.Value, "parent.VarOperation.GetValueByName(\"" + item9.Value + "\")");
                            }
                        }
                        list5.Clear();
                        stringBuilder2.Append("," + text5);
                    }
                    if (stringBuilder2.Length > 0)
                    {
                        stringBuilder2.Remove(0, 1);
                    }
                    sb.AppendLine(string.Concat("parent.VarOperation.SetValueByName(\"[", item.ToObject, "]\",parent.GetPage(\"", array3[0], "\")(\"#", array3[1], "\").data(\"", array3[2], "\")(", stringBuilder2.ToString(), "));"));
                }
                sb.AppendLine("\t\t\t}");
            }
        }
        sb.AppendLine("\t\t\tbreak labelFinish;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
    }

    private void InitializeComponent()
    {
        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        base.SuspendLayout();
        this.RowTemplate.Height = 23;
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        base.ResumeLayout(false);
    }

    public void FireDBOperationOK()
    {
        if (this.DBOperationOK != null)
        {
            this.DBOperationOK(this, null);
        }
    }

    public void FireDBOperationErr()
    {
        if (this.DBOperationErr != null)
        {
            this.DBOperationErr(this, null);
        }
    }
}
