using CommonSnappableTypes;
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

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[Guid("1FBBA1D4-8539-42f1-8544-7D3D5C005713")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class CComboBox : ComboBox, IDCCEControl, IControlShape, IDBAnimation
{
    public List<object> tags = new();

    private bool Runing;

    private string _text = "";

    private string id = "";

    private string textVarBind = "";

    private string varBind = "";

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
