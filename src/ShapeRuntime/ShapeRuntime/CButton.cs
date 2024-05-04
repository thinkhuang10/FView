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
[ComVisible(true)]
[Guid("623CD0DF-1A5E-43c1-842F-7C90B36A2DE8")]
public class CButton : Button, IDCCEControl, IDBAnimation, IControlShape, ISupportHtml5
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
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
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

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

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
                this.SetValueEvent("[" + varBind + "]", !Convert.ToBoolean(this.GetValueEvent("[" + varBind + "]")));
            }
        }
        catch
        {
        }
        if (this.Click != null)
        {
            this.Click(this, e);
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

    public void Fire()
    {
        if (this.Click != null)
        {
            this.Click(this, null);
        }
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
        string text5 = Convert.ToString(base.TextAlign) switch
        {
            "TopLeft" => "Left",
            "MiddleLeft" => "Left",
            "BottomLeft" => "Left",
            "TopRight" => "Right",
            "MiddleRight" => "Right",
            "BottomRight" => "Right",
            "TopCenter" => "Center",
            "MiddleCenter" => "Center",
            "BottomCenter" => "Center",
            _ => "Center",
        };
        stringBuilder.Append("<input type=\"button\" id=\"" + id + "\" onclick=\"_onclick('" + id + "');\" tabIndex=\"" + base.TabIndex + "\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};visibility:" + text + ";text-align:" + text5 + "; display:inline; position:absolute;left:" + base.Location.X + "px; top:" + base.Location.Y + "px;width:" + base.Width.ToString() + "px;height:" + base.Height.ToString() + "px;background-color:" + ColorTranslator.ToHtml(BackColor) + ";color:" + ColorTranslator.ToHtml(ForeColor) + ";font-Size:" + base.Font.Size + "pt; font-Style:" + text3 + "; font-family:" + Font.Name + "; font-Weight:" + text2 + ";text-decoration:" + text4 + ";\" width=\"" + base.Width.ToString() + "px\" height=\"" + base.Height.ToString() + "px\" value=\"" + Text.ToString() + "\" ></input>");
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
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            MakeScriptOfDBOperation(stringBuilder);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            flag = true;
                            break;
                        case "MouseEnter":
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseLeave":
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "DBOperationOK":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            stringBuilder.AppendLine("var resultline = $(\"#" + id + "\").data(\"DBOKResult\")");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"DBOKResult\",\"\")");
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "DBOperationErr":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                    }
                }
            }
        }
        if (!flag && (dbinsert || dbselect || dbdelete || dbupdate))
        {
            stringBuilder.AppendLine("function " + id + "_event_Click(){");
            MakeScriptOfDBOperation(stringBuilder);
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Click\"," + id + "_event_Click)");
        }
        stringBuilder.AppendLine("//>!以下是--" + id + "--的Data区实现");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"DBOKResult\",\"\")");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"_ControlTag\",\"\")");
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value,col) {set_Button_DataSource(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value,col) {set_Button_DataSourceTag(\"" + id + "\",value,col);}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_tag(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Tag\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_tag(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Tag\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_tag(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectTag\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Enabled(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Enabled\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Enabled(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Enabled\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Button_DataSource(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Button_DataSource\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_X(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Left\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_X(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Left\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Y(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Top\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Y(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Top\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Width(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Width\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Width(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Width\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Height(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Height\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Height(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Height\"," + text + ")");
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
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_textAlign(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_TextAlign\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_textAlign(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_TextAlign\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Backcolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_BackColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Backcolor(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_BackColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Backcolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_BColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Backcolor(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_BColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {return get_Button_SQLText(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectText\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {return get_Button_Text(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Text\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {return set_Button_Text(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Text\"," + text + ")");
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
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Forecolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_FColor\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine(" return get_Forecolor(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_FColor\"," + text + ")");
        return stringBuilder.ToString();
    }

    private void MakeEvent(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
    {
        try
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
                    if (text2.Contains("eval") && text2.StartsWith("eval"))
                    {
                        sb.AppendLine(text2);
                    }
                    else
                    {
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
                            sb.AppendLine("\t\t\t\tparent.VarOperation.SetValueByName(\"[" + item.ToObject.Key + "]\"," + text2 + ")");
                        }
                        else
                        {
                            sb.AppendLine("\t\t\t\t" + text2);
                        }
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
                        sb.AppendLine("\t\t\t\tinputData+=\"<InputItem Id=\\\"" + key + "\\\" Type=\\\"\"+(typeof parent.VarOperation.GetValueByName(\"" + key + "\"))+\"\\\">\"+parent.VarOperation.GetValueByName(\"" + key + "\")+\"</InputItem>\";");
                    }
                    sb.AppendLine("\t\t\t\tinputData+=\"</Input>\";");
                    sb.AppendLine("\t\t\t\tvar callsl = new parent.ServerLogic();");
                    sb.AppendLine("\t\t\t\tcallsl.ExcuteServerLogic(\"" + serverLogicRequest.Id + "\", inputData);");
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
                        sb.AppendLine(string.Concat("\t\t\t\tparent.VarOperation.SetValueByName(\"[", item.ToObject, "]\",parent.GetPage(\"", array3[0], "\")(\"#", array3[1], "\").data(\"", array3[2], "\")(", stringBuilder2.ToString(), "));"));
                    }
                    sb.AppendLine("\t\t\t}");
                }
            }
            sb.AppendLine("\t\t\tbreak labelFinish;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
        }
        catch
        {
            MessageBox.Show("按钮:" + id + "事件" + eventName.ToString());
        }
    }

    private void MakeScriptOfDBOperation(StringBuilder sb)
    {
        if (dbselect)
        {
            sb.AppendLine("$.ajax({");
            if (Ansyncselect)
            {
                sb.AppendLine("async: true,");
            }
            else
            {
                sb.AppendLine("async: false,");
            }
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/SelectToTable\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + DbselectSQL.Replace("\r", "\\r").Replace("\n", "\\n") + "\")),");
            sb.AppendLine("success: function(result) {");
            string[] array = dbselectTO.Split(',');
            new List<string>();
            new List<string>();
            new Dictionary<string, string>();
            int num = -1;
            string[] array2 = array;
            foreach (string text in array2)
            {
                num++;
                if (text.Contains("."))
                {
                    string[] array3 = text.Substring(1, text.Length - 2).Split('.');
                    if (array3.Length == 2)
                    {
                        sb.AppendLine("parent.GetPage(\"" + array3[0] + "\")(\"#" + array3[1] + "\").data(\"setSelectResult\")(result," + num + ");");
                    }
                    else if (array3.Length == 3)
                    {
                        sb.AppendLine("parent.GetPage(\"" + array3[0] + "\")(\"#" + array3[1] + "\").data(\"setSelectResultTag\")(result," + num + ");");
                    }
                }
                else if (!(text == "{权限管理字段}") && text.Contains("["))
                {
                    string text2 = text.Substring(1, text.Length - 2);
                    sb.AppendLine("//内部变量 bugzhang");
                    sb.AppendLine("if($(result).find(\"Table Row col\").eq(" + num + ").text() == \"\" || $(result).find(\"Table Row col\").eq(" + num + ").text() == \" \" )");
                    sb.AppendLine("{");
                    sb.AppendLine("    parent.VarOperation.SetValueByName(\"" + text2 + "\", \"\");");
                    sb.AppendLine("}");
                    sb.AppendLine("else");
                    sb.AppendLine("{");
                    sb.AppendLine("    parent.VarOperation.SetValueByName(\"" + text2 + "\", $(result).find(\"Table Row col\").eq(" + num + ").text());");
                    sb.AppendLine("}");
                }
            }
            sb.AppendLine("_onDBOperationOK(\"" + id + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + id + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbupdate)
        {
            sb.AppendLine("$.ajax({");
            if (Ansyncupdate)
            {
                sb.AppendLine("async: true,");
            }
            else
            {
                sb.AppendLine("async: false,");
            }
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/ExecuteSQL\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbupdateSQL.Replace(Environment.NewLine, "\\r\\n") + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("$(\"#" + id + "\").data(\"DBOKResult\",result)");
            sb.AppendLine("_onDBOperationOK(\"" + id + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + id + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbdelete)
        {
            sb.AppendLine("$.ajax({");
            if (Ansyncdelete)
            {
                sb.AppendLine("async: true,");
            }
            else
            {
                sb.AppendLine("async: false,");
            }
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/ExecuteSQL\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbdeleteSQL.Replace(Environment.NewLine, "\\r\\n") + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("$(\"#" + id + "\").data(\"DBOKResult\",result)");
            sb.AppendLine("_onDBOperationOK(\"" + id + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + id + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
        if (dbinsert)
        {
            sb.AppendLine("$.ajax({");
            if (Ansyncinsert)
            {
                sb.AppendLine("async: true,");
            }
            else
            {
                sb.AppendLine("async: false,");
            }
            sb.AppendLine("type:\"POST\",");
            sb.AppendLine("url:\"DBOperation.asmx/ExecuteSQL\",");
            sb.AppendLine("data:\"sqlcmd=\"+encodeURIComponent(parent.CommonReplaceSQLValue(\"" + dbinsertSQL.Replace(Environment.NewLine, "\\r\\n") + "\")),");
            sb.AppendLine("success: function(result) {");
            sb.AppendLine("$(\"#" + id + "\").data(\"DBOKResult\",result)");
            sb.AppendLine("_onDBOperationOK(\"" + id + "\");");
            sb.AppendLine("},");
            sb.AppendLine("error:function(result){");
            sb.AppendLine("_onDBOperationErr(\"" + id + "\");");
            sb.AppendLine("}");
            sb.AppendLine("});");
        }
    }
}
