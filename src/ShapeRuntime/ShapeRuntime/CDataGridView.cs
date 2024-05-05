using CommonSnappableTypes;
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

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("6E501CCA-D836-4156-B457-FEF07ABB25B4")]
[ComVisible(true)]
public class CDataGridView : DataGridView, IDCCEControl, IControlShape, IDBAnimation
{
    private string id = "";

    private bool Runing;

    public int ClickRowIndex;

    public int ClickColumnIndex;

    private readonly Dictionary<int, bool> isOrderByNumberColumnDict = new();

    private DataGridViewRow oldrow;

    private bool enableEdit;

    private int refreshTime;

    public bool dbmultoperation;

    public List<DBAnimation.DBAnimation> DBAnimations = new();

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
            IDChanged?.Invoke(this, null);
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

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

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
                        cell.Value = GetValueEvent("[" + cell.Tag.ToString().Substring(6) + "]");
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
            string text = GetVarTableEvent("");
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
        OnCellClicked?.Invoke(sender, e);
    }

    private void CDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        ClickRowIndex = e.RowIndex;
        ClickColumnIndex = e.ColumnIndex;
        OnCellDoubleClicked?.Invoke(sender, e);
    }

    private void CDataGridView_CurrentCellChanged(object sender, EventArgs e)
    {
        try
        {
            if (oldrow == null || (oldrow != null && oldrow != base.CurrentCell.OwningRow))
            {
                oldrow = base.CurrentCell.OwningRow;
                SelectedRowChanged?.Invoke(sender, e);
            }
        }
        catch (Exception)
        {
        }
        SelectedCellChanged?.Invoke(sender, e);
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
            ColumnCount = base.ColumnCount
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

    public void FireDBOperationOK()
    {
        DBOperationOK?.Invoke(this, null);
    }

    public void FireDBOperationErr()
    {
        DBOperationErr?.Invoke(this, null);
    }
}
