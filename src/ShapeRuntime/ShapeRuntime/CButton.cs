using CommonSnappableTypes;
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

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("623CD0DF-1A5E-43c1-842F-7C90B36A2DE8")]
public class CButton : Button, IDCCEControl, IDBAnimation, IControlShape
{
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

    private bool Runing;

    private int dBResult = -1;

    private string id = "";

    [NonSerialized]
    private Image _img;

    private string imagepath = "";

    private string textVarBind = "";

    private string varBind = "";

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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
    [Browsable(false)]
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

    [Browsable(false)]
    [DHMIHideProperty]
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

    [DHMIHideProperty]
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

    [DisplayName("ID")]
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

    [DHMINeedSerProperty]
    [Category("外观")]
    [Description("将在控件上显示的图像。")]
    [ReadOnly(false)]
    [Editor(typeof(VarTableImageManage), typeof(UITypeEditor))]
    [DisplayName("Image")]
    public new BitMapForIM Image
    {
        get
        {
            BitMapForIM bitMapForIM = new()
            {
                img = _img,
                ImgGUID = imagepath
            };
            return bitMapForIM;
        }
        set
        {
            if (value != null)
            {
                Image img2 = (base.Image = value.img);
                _img = img2;
                imagepath = value.ImgGUID;
            }
            else
            {
                Image img3 = (base.Image = null);
                _img = img3;
            }
        }
    }

    [Browsable(false)]
    public new int ImageIndex
    {
        get
        {
            return base.ImageIndex;
        }
        set
        {
            base.ImageIndex = value;
        }
    }

    [Browsable(false)]
    public new string ImageKey
    {
        get
        {
            return base.ImageKey;
        }
        set
        {
            base.ImageKey = value;
        }
    }

    [Browsable(false)]
    public new ImageList ImageList
    {
        get
        {
            return base.ImageList;
        }
        set
        {
            base.ImageList = value;
        }
    }

    [Browsable(false)]
    public new TextImageRelation TextImageRelation
    {
        get
        {
            return base.TextImageRelation;
        }
        set
        {
            base.TextImageRelation = value;
        }
    }

    [Browsable(false)]
    public new bool UseMnemonic
    {
        get
        {
            return base.UseMnemonic;
        }
        set
        {
            base.UseMnemonic = value;
        }
    }

    [Browsable(false)]
    public new bool UseVisualStyleBackColor
    {
        get
        {
            return base.UseVisualStyleBackColor;
        }
        set
        {
            base.UseVisualStyleBackColor = value;
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
    public new FlatButtonAppearance FlatAppearance => base.FlatAppearance;

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
            Text = "#Bind:" + textVarBind;
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
    public new bool AutoEllipsis
    {
        get
        {
            return base.AutoEllipsis;
        }
        set
        {
            base.AutoEllipsis = value;
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
    public new DialogResult DialogResult
    {
        get
        {
            return base.DialogResult;
        }
        set
        {
            base.DialogResult = value;
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
    public new bool UseCompatibleTextRendering
    {
        get
        {
            return base.UseCompatibleTextRendering;
        }
        set
        {
            base.UseCompatibleTextRendering = value;
        }
    }

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public new event EventHandler Click;

    public event EventHandler DBOperationErr;

    public event EventHandler DBOperationOK;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    protected override void OnPaint(PaintEventArgs pevent)
    {
        try
        {
            base.OnPaint(pevent);
        }
        catch (Exception)
        {
            Image = null;
        }
    }

    public Bitmap GetLogo()
    {
        return null;
    }

    public CButton()
    {
        base.Click += CButton_Click;
        Text = "按钮";
        ForeColor = Color.Black;
    }

    private void CButton_Click(object sender, EventArgs e)
    {
        if (!Runing)
        {
            return;
        }
        try
        {
            if (varBind != null && varBind != "")
            {
                SetValueEvent("[" + varBind + "]", !Convert.ToBoolean(GetValueEvent("[" + varBind + "]")));
            }
        }
        catch
        {
        }
        Click?.Invoke(this, e);
    }

    public void FireDBOperationOK()
    {
        DBOperationOK?.Invoke(this, null);
    }

    public void FireDBOperationErr()
    {
        DBOperationErr?.Invoke(this, null);
    }

    public void Fire()
    {
        Click?.Invoke(this, null);
    }

    public byte[] Serialize()
    {
        if (Image != null && Image.img != null && string.IsNullOrEmpty(imagepath))
        {
            imagepath = DHMIImageManage.SaveImage(Image.img);
        }
        using MemoryStream memoryStream = new();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(memoryStream, new CButtonSaveItems
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            TabIndex = base.TabIndex,
            Font = base.Font,
            hide = !base.Visible,
            disable = !base.Enabled,
            varBind = varBind,
            textVarBind = textVarBind,
            ImagePath = imagepath,
            newtable = newtable,
            newtableSQL = newtableSQL,
            ansyncnewtable = ansyncnewtable,
            newtableOtherData = newtableOtherData,
            newtableSafeRegion = newtableSafeRegion,
            dbselect = dbselect,
            dbselectSQL = dbselectSQL,
            dbselectTO = dbselectTO,
            dbSelectAnsync = ansyncselect,
            dbselectSafeRegion = dbselectSafeRegion,
            dbselectOtherData = dbselectOtherData,
            dbinsert = dbinsert,
            dbinsertSQL = dbinsertSQL,
            dbInsertAnsync = ansyncinsert,
            dbinsertSafeRegion = dbinsertSafeRegion,
            dbinsertOtherData = dbinsertOtherData,
            dbupdate = dbupdate,
            dbupdateSQL = dbupdateSQL,
            dbUpdateAnsync = ansyncupdate,
            dbupdateSafeRegion = dbupdateSafeRegion,
            dbupdateOtherData = dbupdateOtherData,
            dbdelete = dbdelete,
            dbdeleteSQL = dbdeleteSQL,
            dbDeleteAnsync = ansyncdelete,
            dbdeleteSafeRegion = dbdeleteSafeRegion,
            dbdeleteOtherData = dbdeleteOtherData,
            dbmultoperation = dbmultoperation,
            DBAnimations = DBAnimations
        });
        return memoryStream.ToArray();
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            CButtonSaveItems cButtonSaveItems = null;
            using (Stream serializationStream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                cButtonSaveItems = (CButtonSaveItems)formatter.Deserialize(serializationStream);
            }
            Text = cButtonSaveItems.Text;
            base.BackColor = cButtonSaveItems.BackColor;
            base.ForeColor = cButtonSaveItems.ForeColor;
            base.Font = cButtonSaveItems.Font;
            base.TabIndex = cButtonSaveItems.TabIndex;
            base.Visible = !cButtonSaveItems.hide;
            base.Enabled = !cButtonSaveItems.disable;
            varBind = cButtonSaveItems.varBind;
            textVarBind = cButtonSaveItems.textVarBind;
            imagepath = cButtonSaveItems.ImagePath;
            if (!string.IsNullOrEmpty(imagepath))
            {
                Image = new BitMapForIM
                {
                    img = DHMIImageManage.LoadImage(imagepath),
                    ImgGUID = imagepath
                };
            }
            newtable = cButtonSaveItems.newtable;
            newtableSQL = cButtonSaveItems.newtableSQL;
            ansyncnewtable = cButtonSaveItems.ansyncnewtable;
            newtableOtherData = cButtonSaveItems.newtableOtherData;
            newtableSafeRegion = cButtonSaveItems.newtableSafeRegion;
            dbselect = cButtonSaveItems.dbselect;
            dbselectSQL = cButtonSaveItems.dbselectSQL;
            dbselectTO = cButtonSaveItems.dbselectTO;
            ansyncselect = cButtonSaveItems.dbSelectAnsync;
            dbselectSafeRegion = cButtonSaveItems.dbselectSafeRegion;
            dbselectOtherData = cButtonSaveItems.dbselectOtherData;
            dbinsert = cButtonSaveItems.dbinsert;
            dbinsertSQL = cButtonSaveItems.dbinsertSQL;
            ansyncinsert = cButtonSaveItems.dbInsertAnsync;
            dbinsertSafeRegion = cButtonSaveItems.dbinsertSafeRegion;
            dbinsertOtherData = cButtonSaveItems.dbinsertOtherData;
            dbupdate = cButtonSaveItems.dbupdate;
            dbupdateSQL = cButtonSaveItems.dbupdateSQL;
            ansyncupdate = cButtonSaveItems.dbUpdateAnsync;
            dbupdateSafeRegion = cButtonSaveItems.dbupdateSafeRegion;
            dbupdateOtherData = cButtonSaveItems.dbupdateOtherData;
            dbdelete = cButtonSaveItems.dbdelete;
            dbdeleteSQL = cButtonSaveItems.dbdeleteSQL;
            ansyncdelete = cButtonSaveItems.dbDeleteAnsync;
            dbdeleteSafeRegion = cButtonSaveItems.dbdeleteSafeRegion;
            dbdeleteOtherData = cButtonSaveItems.dbdeleteOtherData;
            dbmultoperation = cButtonSaveItems.dbmultoperation;
            DBAnimations = cButtonSaveItems.DBAnimations;
        }
        catch
        {
        }
    }

    public void Stop()
    {
    }
}
