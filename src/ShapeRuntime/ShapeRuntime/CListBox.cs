using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("E5B12A5E-D966-4b84-9941-A0B593895469")]
[ComVisible(true)]
public class CListBox : ListBox, IDCCEControl, IControlShape, IDBAnimation
{
    private bool Runing;

    private string id = "";

    private int refreshTime;

    public List<object> tags = new();

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

    [Category("设计")]
    [ReadOnly(false)]
    [DisplayName("名称")]
    [Description("设定控件名称。")]
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

    [Category("外观")]
    [DisplayName("边框类型")]
    [ReadOnly(false)]
    [Description("控制在ListBox周围绘制的边框的类型")]
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

    [DisplayName("背景色")]
    [ReadOnly(false)]
    [Category("外观")]
    [Description("组件的背景色")]
    public new Color BackColor
    {
        get
        {
            return base.BackColor;
        }
        set
        {
            base.BackColor = value;
        }
    }

    [Category("外观")]
    [ReadOnly(false)]
    [DisplayName("光标")]
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

    [ReadOnly(false)]
    [Description("设置控件中字体的样式")]
    [DisplayName("字体设置")]
    [Category("外观")]
    public new Font Font
    {
        get
        {
            return base.Font;
        }
        set
        {
            base.Font = value;
        }
    }

    [Category("外观")]
    [ReadOnly(false)]
    [DisplayName("文本色")]
    [Description("设置或获取控件的文本色")]
    public new Color ForeColor
    {
        get
        {
            return base.ForeColor;
        }
        set
        {
            base.ForeColor = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("显示方式")]
    [Category("外观")]
    [Description("组件是否应该从右向左进行绘制")]
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
    public int BColor
    {
        get
        {
            return base.BackColor.ToArgb();
        }
        set
        {
            base.BackColor = Color.FromArgb(value);
        }
    }

    [Category("布局")]
    [Description("控件左上角相对于其容器左上角的坐标")]
    [DisplayName("位置")]
    [ReadOnly(false)]
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

    [ReadOnly(false)]
    [DisplayName("大小")]
    [Category("布局")]
    [Description("控件的大小（以像素为单位）")]
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

    [Browsable(false)]
    public new string FormatString
    {
        get
        {
            return base.FormatString;
        }
        set
        {
            base.FormatString = value;
        }
    }

    [Browsable(false)]
    public new bool FormattingEnabled
    {
        get
        {
            return base.FormattingEnabled;
        }
        set
        {
            base.FormattingEnabled = value;
        }
    }

    [Description("组合框中的项")]
    [DisplayName("添加项")]
    [Category("数据")]
    [ReadOnly(false)]
    public new ObjectCollection Items => base.Items;

    [ReadOnly(false)]
    [DisplayName("刷新周期")]
    [Category("数据")]
    [Description("设置单元格内绑定变量刷新周期。")]
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

    [DisplayName("项内容")]
    [ReadOnly(false)]
    [Category("数据")]
    [Description("设置项所显示的内容。")]
    public string SelectedCell
    {
        get
        {
            if (null == base.SelectedItem)
                return string.Empty;

            return base.SelectedItem.ToString();
        }
        set
        {
            try
            {
                Items[SelectedIndex] = value;
            }
            catch (Exception)
            {
            }
        }
    }

    [ReadOnly(false)]
    [DisplayName("列表框选中行数")]
    [Category("数据")]
    [Description("获取列表框的被选中行数总数")]
    public int SelectedCount => base.SelectedItems.Count;

    [Description("获取列表框的使用行数总数")]
    [ReadOnly(false)]
    [DisplayName("列表框总行数")]
    [Category("数据")]
    public int Count => Items.Count;

    [ReadOnly(false)]
    [Category("数据")]
    [Description("绑定用于项显示值的变量。")]
    [DisplayName("变量绑定")]
    [Editor(typeof(VarTableUITypeEditor), typeof(UITypeEditor))]
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
    public new ControlBindingsCollection DataBindings => base.DataBindings;

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
    public new string DisplayMember
    {
        get
        {
            return base.DisplayMember;
        }
        set
        {
            base.DisplayMember = value;
        }
    }

    [Browsable(false)]
    public new string ValueMember
    {
        get
        {
            return base.ValueMember;
        }
        set
        {
            base.ValueMember = value;
        }
    }

    [DisplayName("项数")]
    [ReadOnly(false)]
    [Browsable(false)]
    [Category("数据")]
    [Description("设置列表框包含项行数。")]
    public int SetRowCount
    {
        get
        {
            return Items.Count;
        }
        set
        {
            if (Items.Count < value)
            {
                for (int i = Items.Count; i < value; i++)
                {
                    int num = i + 1;
                    Items.Add("(Item" + num + ")");
                }
            }
            else if (Items.Count > value)
            {
                for (int num2 = Items.Count; num2 > value; num2--)
                {
                    Items.RemoveAt(Items.Count - 1);
                }
            }
        }
    }

    [DisplayName("排序")]
    [Description("指定是否对组合框的列表部分中的项进行排序")]
    [ReadOnly(false)]
    [Category("行为")]
    public new bool Sorted
    {
        get
        {
            return base.Sorted;
        }
        set
        {
            base.Sorted = value;
        }
    }

    [Description("确定该控件是可见还是隐藏")]
    [ReadOnly(false)]
    [Category("行为")]
    [DisplayName("可见性")]
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

    [Description("用于设置控件的使能性")]
    [ReadOnly(false)]
    [DisplayName("启用控件")]
    [Category("行为")]
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

    [DisplayName("是否使能水平滚动条")]
    [Description("指示ListBox是否超出ListBox右边缘的项显示水平滚动条")]
    [ReadOnly(false)]
    [Category("行为")]
    public new bool HorizontalScrollbar
    {
        get
        {
            return base.HorizontalScrollbar;
        }
        set
        {
            base.HorizontalScrollbar = value;
        }
    }

    [ReadOnly(false)]
    [DisplayName("是否使能多列")]
    [Category("行为")]
    [Description("指示值是否应在列中水平显示")]
    public new bool MultiColumn
    {
        get
        {
            return base.MultiColumn;
        }
        set
        {
            base.MultiColumn = value;
        }
    }

    [ReadOnly(false)]
    [Category("行为")]
    [Description("指示值是否应在列中水平显示")]
    [DisplayName("是否使能多列")]
    public new bool ScrollAlwaysVisible
    {
        get
        {
            return base.ScrollAlwaysVisible;
        }
        set
        {
            base.ScrollAlwaysVisible = value;
        }
    }

    [Browsable(false)]
    public new SelectionMode SelectionMode
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
    public new int ItemHeight
    {
        get
        {
            return base.ItemHeight;
        }
        set
        {
            base.ItemHeight = value;
        }
    }

    [Browsable(false)]
    public new bool IntegralHeight
    {
        get
        {
            return base.IntegralHeight;
        }
        set
        {
            base.IntegralHeight = value;
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
    public new int HorizontalExtent
    {
        get
        {
            return base.HorizontalExtent;
        }
        set
        {
            base.HorizontalExtent = value;
        }
    }

    [Browsable(false)]
    public new int ColumnWidth
    {
        get
        {
            return base.ColumnWidth;
        }
        set
        {
            base.ColumnWidth = value;
        }
    }

    [Browsable(false)]
    public new DrawMode DrawMode
    {
        get
        {
            return base.DrawMode;
        }
        set
        {
            base.DrawMode = value;
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
    public new bool UseTabStops
    {
        get
        {
            return base.UseTabStops;
        }
        set
        {
            base.UseTabStops = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler SelectedItemChanged;

    public new event EventHandler Click;

    public new event EventHandler DoubleClick;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    public event EventHandler DBOperationErr;

    public event EventHandler DBOperationOK;

    public object GetItem(int i)
    {
        if (Count > i)
        {
            return Items[i];
        }
        return null;
    }

    public object GetItemTag(int i)
    {
        if (Count > i)
        {
            return tags[i];
        }
        return null;
    }

    public object GetSelectedItem(int i)
    {
        if (SelectedCount > i)
        {
            return base.SelectedItems[i];
        }
        return null;
    }

    public object GetSelectedItemTag(int i)
    {
        if (SelectedCount > i)
        {
            return tags[base.SelectedIndices[i]];
        }
        return null;
    }

    public Bitmap GetLogo()
    {
        return null;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i].ToString().StartsWith("#Bind:"))
                {
                    Items[i] = GetValueEvent("[" + tags[i].ToString().Substring(6) + "]");
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public CListBox()
    {
        IntegralHeight = false;
        base.SelectedIndexChanged += CListBox_SelectedItemChanged;
        base.DoubleClick += CListBox_DoubleClick;
        base.Click += CListBox_Click;
        ForeColor = Color.Black;
    }

    private void CListBox_Click(object sender, EventArgs e)
    {
        Click?.Invoke(sender, e);
    }

    private void CListBox_DoubleClick(object sender, EventArgs e)
    {
        DoubleClick?.Invoke(sender, e);
    }

    private void CListBox_SelectedItemChanged(object sender, EventArgs e)
    {
        SelectedItemChanged?.Invoke(sender, e);
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CListBoxSaveItems cListBoxSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            TabIndex = TabIndex,
            SelectedItem = (string)base.SelectedItem,
            items = new List<string>()
        };
        foreach (object item in Items)
        {
            cListBoxSaveItems.items.Add(item.ToString());
        }
        cListBoxSaveItems.hide = !Visible;
        cListBoxSaveItems.disable = !Enabled;
        cListBoxSaveItems.refreshTime = refreshTime;
        cListBoxSaveItems.newtable = newtable;
        cListBoxSaveItems.newtableSQL = newtableSQL;
        cListBoxSaveItems.ansyncnewtable = ansyncnewtable;
        cListBoxSaveItems.newtableOtherData = newtableOtherData;
        cListBoxSaveItems.newtableSafeRegion = newtableSafeRegion;
        cListBoxSaveItems.dbselect = dbselect;
        cListBoxSaveItems.dbselectSQL = dbselectSQL;
        cListBoxSaveItems.dbselectTO = dbselectTO;
        cListBoxSaveItems.dbSelectAnsync = ansyncselect;
        cListBoxSaveItems.dbselectSafeRegion = dbselectSafeRegion;
        cListBoxSaveItems.dbselectOtherData = dbselectOtherData;
        cListBoxSaveItems.dbinsert = dbinsert;
        cListBoxSaveItems.dbinsertSQL = dbinsertSQL;
        cListBoxSaveItems.dbInsertAnsync = ansyncinsert;
        cListBoxSaveItems.dbinsertSafeRegion = dbinsertSafeRegion;
        cListBoxSaveItems.dbinsertOtherData = dbinsertOtherData;
        cListBoxSaveItems.dbupdate = dbupdate;
        cListBoxSaveItems.dbupdateSQL = dbupdateSQL;
        cListBoxSaveItems.dbUpdateAnsync = ansyncupdate;
        cListBoxSaveItems.dbupdateSafeRegion = dbupdateSafeRegion;
        cListBoxSaveItems.dbupdateOtherData = dbupdateOtherData;
        cListBoxSaveItems.dbdelete = dbdelete;
        cListBoxSaveItems.dbdeleteSQL = dbdeleteSQL;
        cListBoxSaveItems.dbDeleteAnsync = ansyncdelete;
        cListBoxSaveItems.dbdeleteSafeRegion = dbdeleteSafeRegion;
        cListBoxSaveItems.dbdeleteOtherData = dbdeleteOtherData;
        cListBoxSaveItems.dbmultoperation = dbmultoperation;
        cListBoxSaveItems.DBAnimations = DBAnimations;
        formatter.Serialize(memoryStream, cListBoxSaveItems);
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
            CListBoxSaveItems cListBoxSaveItems = (CListBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            newtable = cListBoxSaveItems.newtable;
            newtableSQL = cListBoxSaveItems.newtableSQL;
            ansyncnewtable = cListBoxSaveItems.ansyncnewtable;
            newtableOtherData = cListBoxSaveItems.newtableOtherData;
            newtableSafeRegion = cListBoxSaveItems.newtableSafeRegion;
            dbselect = cListBoxSaveItems.dbselect;
            dbselectSQL = cListBoxSaveItems.dbselectSQL;
            dbselectTO = cListBoxSaveItems.dbselectTO;
            ansyncselect = cListBoxSaveItems.dbSelectAnsync;
            dbselectSafeRegion = cListBoxSaveItems.dbselectSafeRegion;
            dbselectOtherData = cListBoxSaveItems.dbselectOtherData;
            dbinsert = cListBoxSaveItems.dbinsert;
            dbinsertSQL = cListBoxSaveItems.dbinsertSQL;
            ansyncinsert = cListBoxSaveItems.dbInsertAnsync;
            dbinsertSafeRegion = cListBoxSaveItems.dbinsertSafeRegion;
            dbinsertOtherData = cListBoxSaveItems.dbinsertOtherData;
            dbupdate = cListBoxSaveItems.dbupdate;
            dbupdateSQL = cListBoxSaveItems.dbupdateSQL;
            ansyncupdate = cListBoxSaveItems.dbUpdateAnsync;
            dbupdateSafeRegion = cListBoxSaveItems.dbupdateSafeRegion;
            dbupdateOtherData = cListBoxSaveItems.dbupdateOtherData;
            dbdelete = cListBoxSaveItems.dbdelete;
            dbdeleteSQL = cListBoxSaveItems.dbdeleteSQL;
            ansyncdelete = cListBoxSaveItems.dbDeleteAnsync;
            dbdeleteSafeRegion = cListBoxSaveItems.dbdeleteSafeRegion;
            dbdeleteOtherData = cListBoxSaveItems.dbdeleteOtherData;
            dbmultoperation = cListBoxSaveItems.dbmultoperation;
            DBAnimations = cListBoxSaveItems.DBAnimations;
            Text = cListBoxSaveItems.Text;
            base.BackColor = cListBoxSaveItems.BackColor;
            base.ForeColor = cListBoxSaveItems.ForeColor;
            base.Font = cListBoxSaveItems.Font;
            base.SelectedItem = cListBoxSaveItems.SelectedItem;
            TabIndex = cListBoxSaveItems.TabIndex;
            Visible = !cListBoxSaveItems.hide;
            Enabled = !cListBoxSaveItems.disable;
            for (int i = 0; i < cListBoxSaveItems.items.Count; i++)
            {
                object value = cListBoxSaveItems.items[i];
                Items[i] = value;
            }
            refreshTime = cListBoxSaveItems.refreshTime;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }

    public bool AddItem(object obj)
    {
        try
        {
            Items.Add(obj.ToString());
            tags.Add(obj.ToString());
            return true;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public bool AddItemAndTag(object obj, object tag)
    {
        try
        {
            Items.Add(obj.ToString());
            tags.Add(tag.ToString());
            return true;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public bool RemoveItem(object obj)
    {
        try
        {
            if (Items.Contains(obj.ToString()))
            {
                tags.RemoveAt(Items.IndexOf(obj.ToString()));
                Items.Remove(obj.ToString());
            }
            return true;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public bool ClearItems()
    {
        try
        {
            Items.Clear();
            tags.Clear();
            return true;
        }
        catch (Exception)
        {
        }
        return false;
    }

    public bool Contains(object obj)
    {
        try
        {
            return Items.Contains(obj.ToString());
        }
        catch (Exception)
        {
        }
        return false;
    }

    public bool HasItem(string item)
    {
        return Items.Contains(item);
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
