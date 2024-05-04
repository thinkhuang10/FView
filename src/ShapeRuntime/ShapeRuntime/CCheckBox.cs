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

namespace ShapeRuntime;

[Serializable]
[Guid("FD36F835-8BE3-4219-A143-7F6CEEEFA3E3")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class CCheckBox : CheckBox, IDCCEControl, IControlShape, ISupportHtml5
{
    private bool Runing;

    private bool userclick;

    private string id = "";

    private string textVarBind = "";

    private string varBind = "";

    private bool enablestate;

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

    [DisplayName("ID")]
    [Category("设计")]
    [Description("设定控件名称。")]
    [ReadOnly(false)]
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
    public new CheckState CheckState
    {
        get
        {
            return base.CheckState;
        }
        set
        {
            base.CheckState = value;
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
    [DisplayName("外形设置")]
    [Category("外观")]
    [Description("设置控件中字体的样式")]
    [ReadOnly(false)]
    public new FlatButtonAppearance FlatAppearance
    {
        get
        {
            return base.FlatAppearance;
        }
        set
        {
            base.FlatAppearance.BorderColor = value.BorderColor;
            base.FlatAppearance.BorderSize = value.BorderSize;
            base.FlatAppearance.MouseDownBackColor = value.MouseDownBackColor;
            base.FlatAppearance.MouseOverBackColor = value.MouseOverBackColor;
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

    public event EventHandler CheckedChange;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler IDChanged;

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
                base.Checked = Convert.ToBoolean(this.GetValueEvent("[" + varBind + "]"));
            }
        }
        catch (Exception)
        {
        }
        try
        {
            if (textVarBind != null && textVarBind != "")
            {
                base.Text = Convert.ToString(this.GetValueEvent("[" + textVarBind + "]"));
            }
        }
        catch (Exception)
        {
        }
    }

    public CCheckBox()
    {
        base.Click += CCheckBox_Click;
        base.CheckedChanged += CCheckBox_CheckedChanged;
        Text = "多选框";
        ForeColor = Color.Black;
    }

    private void CCheckBox_Click(object sender, EventArgs e)
    {
        if (Runing && varBind != null && varBind != "")
        {
            userclick = true;
            this.SetValueEvent("[" + varBind + "]", base.Checked);
            base.Checked = !base.Checked;
            userclick = false;
        }
    }

    private void CCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (this.CheckedChange != null && !userclick)
        {
            this.CheckedChange(sender, e);
        }
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CCheckBoxSaveItems cCheckBoxSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            Checked = base.Checked,
            TabIndex = TabIndex,
            VarBind = varBind,
            hide = !base.Visible,
            disable = !base.Enabled,
            Enabled = base.Enabled,
            textVarBind = textVarBind
        };
        formatter.Serialize(memoryStream, cCheckBoxSaveItems);
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
            CCheckBoxSaveItems cCheckBoxSaveItems = (CCheckBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cCheckBoxSaveItems.Text;
            base.BackColor = cCheckBoxSaveItems.BackColor;
            base.ForeColor = cCheckBoxSaveItems.ForeColor;
            base.Font = cCheckBoxSaveItems.Font;
            base.Checked = cCheckBoxSaveItems.Checked;
            TabIndex = cCheckBoxSaveItems.TabIndex;
            VarBind = cCheckBoxSaveItems.VarBind;
            base.Visible = !cCheckBoxSaveItems.hide;
            enablestate = cCheckBoxSaveItems.Enabled;
            textVarBind = cCheckBoxSaveItems.textVarBind;
        }
        catch (Exception)
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
        string text2 = Convert.ToString(base.TextAlign) switch
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
        bool flag = enablestate;
        string text3 = ((!flag) ? "" : " disabled=\"disabled\"");
        if (!base.Checked)
        {
            stringBuilder.Append(string.Concat("<div tabIndex=\"", base.TabIndex, "\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};visibility:", text, "; text-align:", text2, "; display:inline; position:absolute; left:", base.Location.X, "px; top:", base.Location.Y, "px; width:", base.Width.ToString(), "px; height:", base.Height.ToString(), "px; background-color:", ColorTranslator.ToHtml(BackColor), "; color:", ColorTranslator.ToHtml(ForeColor), "; font-Size:", base.Font.Size, "pt; font-Style:", base.Font.Style, "; font-family:", base.Font.OriginalFontName, ";\" width=\"", base.Width.ToString(), "px\" height=\"", base.Height.ToString(), "px\"><input id=\"", id, "\" ", text3, " onclick=\"_onclick('", id, "');\" onchange=\"_oncheckedchanged('", id, "');\" type=\"checkbox\"><label for=", id, ">", Text.ToString(), "</label></div>"));
        }
        else
        {
            stringBuilder.Append(string.Concat("<div tabIndex=\"", base.TabIndex, "\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};visibility:", text, "; text-align:", text2, "; display:inline; position:absolute; left:", base.Location.X, "px; top:", base.Location.Y, "px; width:", base.Width.ToString(), "px; height:", base.Height.ToString(), "px; background-color:", ColorTranslator.ToHtml(BackColor), "; color:", ColorTranslator.ToHtml(ForeColor), "; font-Size:", base.Font.Size, "pt; font-Style:", base.Font.Style, "; font-family:", base.Font.OriginalFontName, ";\" width=\"", base.Width.ToString(), "px\" height=\"", base.Height.ToString(), "px\"><input  id=\"", id, "\" ", text3, "  onclick=\"_onclick('", id, "');\" onchange=\"_oncheckedchanged('", id, "');\" checked=\"checked\" type=\"checkbox\"><label for=", id, ">", Text.ToString(), "</label></div>"));
        }
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
                            flag2 = true;
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            makeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "CheckedChanged":
                            flag = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            makeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseEnter":
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            makeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseLeave":
                            stringBuilder.AppendLine("$(\"#" + id + "\").addClass(\"handup\");");
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            makeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                    }
                }
            }
        }
        if (!flag)
        {
            stringBuilder.AppendLine("function " + id + "_event_CheckedChanged(obj){");
            stringBuilder.AppendLine("//> 这里是 checkchanged 的空实现");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"CheckedChanged\"," + id + "_event_CheckedChanged)");
        }
        if (!flag2)
        {
            stringBuilder.AppendLine("function " + id + "_event_Click(obj){");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Click\"," + id + "_event_Click)");
        }
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Checkbox_Selected(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Checked\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Checkbox_Selected(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Checked\"," + text + ")");
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
        stringBuilder.AppendLine("function " + text + "(value) {set_Show(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Show\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Hide(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Hide\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Fire(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Fire\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "() {return get_Button_SQLText(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectText\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value,col) { set_Checkbox_DataSource(\"" + id + "\",value,col)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) { set_Checkbox_DataSource(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
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

    private void makeEvent(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
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

    private void CheckStatedChanged(StringBuilder sb, Dictionary<string, List<EventSetItem>> eventBindDict, string eventName)
    {
        sb.AppendLine("var " + id + "_temp=\"\";");
        if (base.Checked)
        {
            sb.AppendLine(id + "_temp=true;");
        }
        else
        {
            sb.AppendLine(id + "_temp=false;");
        }
        sb.AppendLine("//复选框Checked状态改变事件");
        sb.AppendLine("   ");
        sb.AppendLine("function " + id + "_oncheckedchanged() {");
        sb.AppendLine(" if(" + id + "_temp!=get_Checkbox_Selected(\"" + id + "\"))");
        sb.AppendLine("{");
        sb.AppendLine(id + "_event_CheckedChanged(\"" + id + "\");");
        sb.AppendLine(id + "_temp=get_Checkbox_Selected(\"" + id + "\");");
        sb.AppendLine("}");
        sb.AppendLine("}");
        sb.AppendLine(id + "_oncheckedchanged();");
    }
}
