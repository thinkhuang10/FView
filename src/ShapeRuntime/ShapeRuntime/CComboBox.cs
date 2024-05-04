using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
[ComVisible(true)]
[Guid("1FBBA1D4-8539-42f1-8544-7D3D5C005713")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CComboBox : ComboBox, IDCCEControl, IControlShape, IDBAnimation, ISupportHtml5
{
    public List<object> tags = new();

    private bool Runing;

    private string _text = "";

    private string id = "";

    private string textVarBind = "";

    private string varBind = "";

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

    [Browsable(false)]
    public object SelectedTag
    {
        get
        {
            try
            {
                return GetItemTag(SelectedIndex);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }

    [Category("设计")]
    [DisplayName("ID")]
    [ReadOnly(false)]
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
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
        }
    }

    [Browsable(false)]
    public new Image BackgroundImage
    {
        get
        {
            return base.BackgroundImage;
        }
        set
        {
            base.BackgroundImage = value;
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
    public new ImageLayout BackgroundImageLayout
    {
        get
        {
            return base.BackgroundImageLayout;
        }
        set
        {
            base.BackgroundImageLayout = value;
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
    public string TextVarBind
    {
        get
        {
            return textVarBind;
        }
        set
        {
            textVarBind = value;
        }
    }

    [Browsable(false)]
    public string VarBind
    {
        get
        {
            return varBind;
        }
        set
        {
            varBind = value;
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
    public new ControlBindingsCollection DataBindings => base.DataBindings;

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
    public new int DropDownHeight
    {
        get
        {
            return base.DropDownHeight;
        }
        set
        {
            base.DropDownHeight = value;
        }
    }

    [Browsable(false)]
    public new int DropDownWidth
    {
        get
        {
            return base.DropDownWidth;
        }
        set
        {
            base.DropDownWidth = value;
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
    public new int MaxDropDownItems
    {
        get
        {
            return base.MaxDropDownItems;
        }
        set
        {
            base.MaxDropDownItems = value;
        }
    }

    [Browsable(false)]
    public new int MaxLength
    {
        get
        {
            return base.MaxLength;
        }
        set
        {
            base.MaxLength = value;
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
    public new AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get
        {
            return base.AutoCompleteCustomSource;
        }
        set
        {
            base.AutoCompleteCustomSource = value;
        }
    }

    [Browsable(false)]
    public new AutoCompleteMode AutoCompleteMode
    {
        get
        {
            return base.AutoCompleteMode;
        }
        set
        {
            base.AutoCompleteMode = value;
        }
    }

    [Browsable(false)]
    public new AutoCompleteSource AutoCompleteSource
    {
        get
        {
            return base.AutoCompleteSource;
        }
        set
        {
            base.AutoCompleteSource = value;
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler IDChanged;

    public event EventHandler DBOperationErr;

    public event EventHandler DBOperationOK;

    public Bitmap GetLogo()
    {
        return null;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            if (varBind != null && varBind != "")
            {
                SelectedIndex = Convert.ToInt32(this.GetValueEvent("[" + varBind + "]"));
            }
        }
        catch (Exception)
        {
        }
        try
        {
            if (textVarBind != null && textVarBind != "")
            {
                Text = Convert.ToString(this.GetValueEvent("[" + textVarBind + "]"));
            }
        }
        catch (Exception)
        {
        }
    }

    public CComboBox()
    {
        base.SelectedValueChanged += CComboBox_SelectedValueChanged;
        base.TextChanged += CComboBox_TextChanged;
        Text = "下拉框";
        ForeColor = Color.Black;
    }

    private void CComboBox_TextChanged(object sender, EventArgs e)
    {
        _text = Text;
    }

    private void CComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
        if (this.SelectedItemChanged != null)
        {
            this.SelectedItemChanged(sender, e);
        }
    }

    public byte[] Serialize()
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new();
            CComboBoxSaveItems cComboBoxSaveItems = new()
            {
                Text = _text,
                BackColor = base.BackColor,
                ForeColor = base.ForeColor,
                Font = base.Font,
                dropstyle = base.DropDownStyle,
                TabIndex = TabIndex,
                SelectedItem = (string)base.SelectedItem,
                items = new List<string>()
            };
            foreach (object item in base.Items)
            {
                cComboBoxSaveItems.items.Add(item.ToString());
            }
            cComboBoxSaveItems.hide = !base.Visible;
            cComboBoxSaveItems.disable = !base.Enabled;
            cComboBoxSaveItems.varBind = varBind;
            cComboBoxSaveItems.textVarBind = textVarBind;
            cComboBoxSaveItems.newtable = newtable;
            cComboBoxSaveItems.newtableSQL = newtableSQL;
            cComboBoxSaveItems.ansyncnewtable = ansyncnewtable;
            cComboBoxSaveItems.newtableOtherData = newtableOtherData;
            cComboBoxSaveItems.newtableSafeRegion = newtableSafeRegion;
            cComboBoxSaveItems.dbselect = dbselect;
            cComboBoxSaveItems.dbselectSQL = dbselectSQL;
            cComboBoxSaveItems.dbselectTO = dbselectTO;
            cComboBoxSaveItems.dbSelectAnsync = ansyncselect;
            cComboBoxSaveItems.dbselectSafeRegion = dbselectSafeRegion;
            cComboBoxSaveItems.dbselectOtherData = dbselectOtherData;
            cComboBoxSaveItems.dbinsert = dbinsert;
            cComboBoxSaveItems.dbinsertSQL = dbinsertSQL;
            cComboBoxSaveItems.dbInsertAnsync = ansyncinsert;
            cComboBoxSaveItems.dbinsertSafeRegion = dbinsertSafeRegion;
            cComboBoxSaveItems.dbinsertOtherData = dbinsertOtherData;
            cComboBoxSaveItems.dbupdate = dbupdate;
            cComboBoxSaveItems.dbupdateSQL = dbupdateSQL;
            cComboBoxSaveItems.dbUpdateAnsync = ansyncupdate;
            cComboBoxSaveItems.dbupdateSafeRegion = dbupdateSafeRegion;
            cComboBoxSaveItems.dbupdateOtherData = dbupdateOtherData;
            cComboBoxSaveItems.dbdelete = dbdelete;
            cComboBoxSaveItems.dbdeleteSQL = dbdeleteSQL;
            cComboBoxSaveItems.dbDeleteAnsync = ansyncdelete;
            cComboBoxSaveItems.dbdeleteSafeRegion = dbdeleteSafeRegion;
            cComboBoxSaveItems.dbdeleteOtherData = dbdeleteOtherData;
            cComboBoxSaveItems.dbmultoperation = dbmultoperation;
            cComboBoxSaveItems.DBAnimations = DBAnimations;
            formatter.Serialize(memoryStream, cComboBoxSaveItems);
            byte[] result = memoryStream.ToArray();
            memoryStream.Close();
            return result;
        }
        catch
        {
            return null;
        }
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(bytes);
            CComboBoxSaveItems cComboBoxSaveItems = (CComboBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            newtable = cComboBoxSaveItems.newtable;
            newtableSQL = cComboBoxSaveItems.newtableSQL;
            ansyncnewtable = cComboBoxSaveItems.ansyncnewtable;
            newtableOtherData = cComboBoxSaveItems.newtableOtherData;
            newtableSafeRegion = cComboBoxSaveItems.newtableSafeRegion;
            dbselect = cComboBoxSaveItems.dbselect;
            dbselectSQL = cComboBoxSaveItems.dbselectSQL;
            dbselectTO = cComboBoxSaveItems.dbselectTO;
            ansyncselect = cComboBoxSaveItems.dbSelectAnsync;
            dbselectSafeRegion = cComboBoxSaveItems.dbselectSafeRegion;
            dbselectOtherData = cComboBoxSaveItems.dbselectOtherData;
            dbinsert = cComboBoxSaveItems.dbinsert;
            dbinsertSQL = cComboBoxSaveItems.dbinsertSQL;
            ansyncinsert = cComboBoxSaveItems.dbInsertAnsync;
            dbinsertSafeRegion = cComboBoxSaveItems.dbinsertSafeRegion;
            dbinsertOtherData = cComboBoxSaveItems.dbinsertOtherData;
            dbupdate = cComboBoxSaveItems.dbupdate;
            dbupdateSQL = cComboBoxSaveItems.dbupdateSQL;
            ansyncupdate = cComboBoxSaveItems.dbUpdateAnsync;
            dbupdateSafeRegion = cComboBoxSaveItems.dbupdateSafeRegion;
            dbupdateOtherData = cComboBoxSaveItems.dbupdateOtherData;
            dbdelete = cComboBoxSaveItems.dbdelete;
            dbdeleteSQL = cComboBoxSaveItems.dbdeleteSQL;
            ansyncdelete = cComboBoxSaveItems.dbDeleteAnsync;
            dbdeleteSafeRegion = cComboBoxSaveItems.dbdeleteSafeRegion;
            dbdeleteOtherData = cComboBoxSaveItems.dbdeleteOtherData;
            dbmultoperation = cComboBoxSaveItems.dbmultoperation;
            DBAnimations = cComboBoxSaveItems.DBAnimations;
            Text = cComboBoxSaveItems.Text;
            base.BackColor = cComboBoxSaveItems.BackColor;
            base.ForeColor = cComboBoxSaveItems.ForeColor;
            base.Font = cComboBoxSaveItems.Font;
            base.SelectedItem = cComboBoxSaveItems.SelectedItem;
            TabIndex = cComboBoxSaveItems.TabIndex;
            if (cComboBoxSaveItems.dropstyle == null)
            {
                cComboBoxSaveItems.dropstyle = ComboBoxStyle.DropDown;
            }
            base.DropDownStyle = (ComboBoxStyle)cComboBoxSaveItems.dropstyle;
            if (base.DropDownStyle == ComboBoxStyle.Simple)
            {
                base.DropDownStyle = ComboBoxStyle.DropDown;
            }
            foreach (string item in cComboBoxSaveItems.items)
            {
                base.Items.Add(item);
                tags.Add(item);
            }
            base.Visible = !cComboBoxSaveItems.hide;
            base.Enabled = !cComboBoxSaveItems.disable;
            varBind = cComboBoxSaveItems.varBind;
            textVarBind = cComboBoxSaveItems.textVarBind;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }

    public object GetItem(int index)
    {
        try
        {
            return base.Items[index].ToString();
        }
        catch
        {
            return "";
        }
    }

    public object GetItemTag(int index)
    {
        try
        {
            return tags[index];
        }
        catch
        {
            return "";
        }
    }

    public bool InsertItem(int index, object obj)
    {
        try
        {
            base.Items.Insert(index, obj.ToString());
            tags.Insert(index, obj.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool InsertItemAndTag(int index, object obj, object tag)
    {
        try
        {
            base.Items.Insert(index, obj.ToString());
            tags.Insert(index, tag.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool AddItem(object obj)
    {
        try
        {
            base.Items.Add(obj.ToString());
            tags.Add(obj.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool AddItemAndTag(object obj, object tag)
    {
        try
        {
            base.Items.Add(obj.ToString());
            tags.Add(tag.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool RemoveItem(object obj)
    {
        try
        {
            tags.RemoveAt(base.Items.IndexOf(obj.ToString()));
            base.Items.Remove(obj.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ClearItems()
    {
        try
        {
            base.Items.Clear();
            tags.Clear();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Contains(object obj)
    {
        try
        {
            return base.Items.Contains(obj.ToString());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string makeHTML()
    {
        StringBuilder stringBuilder = new();
        string text = (base.Visible ? "visible" : "hidden");
        bool bold = Font.Bold;
        string text2 = ((!bold) ? "normal" : "bold");
        bool italic = Font.Italic;
        string text3 = ((!italic) ? "normal" : "Italic");
        bool underline = Font.Underline;
        string text4 = ((!underline) ? "none" : "underline");
        stringBuilder.AppendLine("<select  onchange=\"_onchange('" + id + "')\" id=\"" + id + "\" tabIndex=\"" + base.TabIndex + "\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL}; visibility:" + text + ";display:inline; position:absolute; left:" + base.Location.X + "px; top:" + base.Location.Y + "px;width:" + base.Width.ToString() + "px;height:" + base.Height.ToString() + "px;background-color:" + ColorTranslator.ToHtml(BackColor) + ";color:" + ColorTranslator.ToHtml(ForeColor) + ";font-Size:" + base.Font.Size + "pt; font-Style:" + text3 + "; font-family:" + Font.Name + "; font-Weight:" + text2 + ";text-decoration:" + text4 + ";\" width=\"" + base.Width.ToString() + "px\" height=\"" + base.Height.ToString() + "px\" value=\"" + Text.ToString() + "\" >");
        stringBuilder.AppendLine("</select>");
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
        string text = id + "_dom";
        stringBuilder.AppendLine(text + " = document.getElementById('" + id + "');");
        foreach (object item in base.Items)
        {
            stringBuilder.AppendLine("add_comboboxItem(\"" + id + "\",\"" + item.ToString() + "\")");
        }
        if (this.requestEventBindDict != null)
        {
            Dictionary<string, List<EventSetItem>> dictionary = this.requestEventBindDict();
            if (dictionary != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    switch (key)
                    {
                        case "Click":
                            stringBuilder.AppendLine(text + ".onclick = function (){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            break;
                        case "MouseEnter":
                            stringBuilder.AppendLine(text + ".onmouseenter = function (){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            break;
                        case "MouseLeave":
                            stringBuilder.AppendLine(text + ".onmouseout = function (){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            break;
                        case "DoubleClick":
                            stringBuilder.AppendLine(text + ".ondblclick = function (){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            break;
                        case "DBOperationOK":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "SelectedIndexChanged":
                            flag2 = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "DataSourceChanged":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "SelectedValueChanged":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "EnableChanged":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "VisibleChanged":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "SelectedItemChanged":
                            flag = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            stringBuilder.AppendLine(id + "_event_SelectedIndexChanged()");
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
            stringBuilder.AppendLine("function " + id + "_event_SelectedItemChanged(){");
            stringBuilder.AppendLine(id + "_event_SelectedIndexChanged()");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SelectedItemChanged\"," + id + "_event_SelectedItemChanged)");
        }
        if (!flag2)
        {
            stringBuilder.AppendLine("function " + id + "_event_SelectedIndexChanged(){");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"SelectedIndexChanged\"," + id + "_event_SelectedIndexChanged)");
        }
        string text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value,col) {set_Combobox_DataSource(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value,col) {set_Combobox_DataSourceTag(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Combobox_tag(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Tag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Combobox_tag(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Tag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return GetItemTag(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"GetItemTag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Combobox_Text(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Text\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Combobox_Text(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Text\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value,col) {");
        stringBuilder.AppendLine("add_comboboxItem(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("removeAll_comboboxIteml(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"ClearItems\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_SelectedItem(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_SelectedItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("get_SelectedItem(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_SelectedItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("RemoveItem(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"RemoveItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return GetItem(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"GetItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return HasItem(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"HasItem\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Combobox_DataSource(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_DataSource\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value,col) {");
        stringBuilder.AppendLine("set_Combobox_DataSource(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Combobox_DataSource\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return set_Backcolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_BackColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Backcolor(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_BackColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Backcolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_BColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Backcolor(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_BColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_X(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Left\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_X(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Left\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Y(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Top\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Y(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Top\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Width(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Width\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Width(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Width\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Height(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Height\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Height(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Height\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Visible(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Visible\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Visible(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Visible\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Enabled(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Enabled\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Enabled(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Enabled\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Tabindex(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_TabIndex\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Tabindex(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_TabIndex\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Font(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Font\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Font(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Font\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_textAlign(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_TextAlign\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_textAlign(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_TextAlign\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {set_Show(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Show\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {set_Hide(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Hide\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {set_Fire(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Fire\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(itemvalue,tagvalue) {ComboBox_AddItemAndTag(\"" + id + "\",itemvalue,tagvalue)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"AddItemAndTag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(index,itemvalue,tagvalue) {ComboBox_InsertItemAndTag(\"" + id + "\",index,itemvalue,tagvalue)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"InsertItemAndTag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "() {return get_Combobox_SQLText(\"" + id + "\");}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectText\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "() {return get_Combobox_tag(\"" + id + "\");}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectTag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Combobox_tag(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Tag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Combobox_tag(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Tag\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_Forecolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_FColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine(" return get_Forecolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_FColor\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_SelectedIndex(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_SelectedIndex\"," + text2 + ")");
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
