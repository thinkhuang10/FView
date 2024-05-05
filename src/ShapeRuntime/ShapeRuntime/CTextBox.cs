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
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("C8CAC342-CBBC-4e5f-885D-B8FEBA72980C")]
[ComVisible(true)]
public class CTextBox : TextBox, IDCCEControl, IControlShape
{
    private bool Runing;

    private string id = "";

    private string textVarBind = "";

    [Browsable(false)]
    public int ScrollStyle
    {
        get
        {
            return Convert.ToInt32(base.ScrollBars);
        }
        set
        {
            base.ScrollBars = (ScrollBars)value;
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

    [ReadOnly(false)]
    [Category("设计")]
    [Description("设定控件名称")]
    [DisplayName("ID")]
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
            base.Text = "#Bind:" + textVarBind;
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
    public new bool HideSelection
    {
        get
        {
            return base.HideSelection;
        }
        set
        {
            base.HideSelection = value;
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
    public new bool ShortcutsEnabled
    {
        get
        {
            return base.ShortcutsEnabled;
        }
        set
        {
            base.ShortcutsEnabled = value;
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

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public event EventHandler TextChange;

    public event requestEventBindDictDele RequestEventBindDict;

    public event requestPropertyBindDataDele RequestPropertyBindData;

    public event EventHandler IDChanged;

    private void CTextBox_TextChanged(object sender, EventArgs e)
    {
        TextChange?.Invoke(sender, e);
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
        ChangeEventScript(stringBuilder);
        bool flag = false;
        if (RequestEventBindDict != null)
        {
            Dictionary<string, List<EventSetItem>> dictionary = RequestEventBindDict();
            if (dictionary != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    switch (key)
                    {
                        case "Click":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "TextChange":
                            flag = true;
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseEnter":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "MouseLeave":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                        case "DoubleClick":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
                            stringBuilder.AppendLine("}");
                            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"" + key + "\"," + id + "_event_" + key + ")");
                            break;
                    }
                }
            }
            if (!flag)
            {
                stringBuilder.AppendLine("var " + id + "_temp=\"\";");
                if (Text == "")
                {
                    stringBuilder.AppendLine("_temp=\"\"");
                }
                else
                {
                    stringBuilder.AppendLine(id + "_temp=\"" + Text.ToString() + "\";");
                }
                stringBuilder.AppendLine("//文本change事件");
                stringBuilder.AppendLine("function " + id + "_onTextChangeClick() {");
                stringBuilder.AppendLine(" if(" + id + "_temp!=get_Text_Text(\"" + id + "\"))");
                stringBuilder.AppendLine("\t{");
                stringBuilder.AppendLine("\t\t_onText_Textchange(\"" + id + "\");");
                stringBuilder.AppendLine(id + "_temp=get_Text_Text(\"" + id + "\");");
                stringBuilder.AppendLine("\t}");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine(id + "_onTextChangeClick();");
                stringBuilder.AppendLine("function " + id + "_event_TextChange(){");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("$(\"#" + id + "\").data(\"TextChange\"," + id + "_event_TextChange)");
            }
        }
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"_ControlTag\",\"\")");
        string text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Text_Clearall(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Clear\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Text_ReadOnly(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_ReadOnly\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Text_ReadOnly(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_ReadOnly\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Text_Text(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Text\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Text_Text(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Text\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Text_password(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_UseSystemPasswordChar\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Text_password(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_UseSystemPasswordChar\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("set_Text_maxLength(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_MaxLength\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {");
        stringBuilder.AppendLine("return get_Text_maxLength(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_MaxLength\"," + text + ")");
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
        stringBuilder.AppendLine("function " + text + "() {return get_Text_SQLText(\"" + id + "\");}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectText\"," + text + ")");
        if (base.Multiline)
        {
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value) {set_Textarea_DataSource(\"" + id + "\",value);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value,col) {set_Textarea_DataSource(\"" + id + "\",value,col);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value) {set_Textarea_DataSourceTag(\"" + id + "\",value);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text + ")");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value,col) {set_Textarea_DataSourceTag(\"" + id + "\",value,col);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text + ")");
        }
        else
        {
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value) {set_Text_DataSource(\"" + id + "\",value);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "(value,col) {set_Text_DataSource(\"" + id + "\",value,col);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text + ")");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "_Tag(value) {set_Text_DataSourceTag(\"" + id + "\",value);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text + "_Tag)");
            text = "_" + Guid.NewGuid().ToString().Replace("-", "");
            stringBuilder.AppendLine("function " + text + "_Tag(value,col) {set_Text_DataSourceTag(\"" + id + "\",value,col);}");
            stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResultTag\"," + text + "_Tag)");
        }
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Show(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Show\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Hide(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Hide\"," + text + ")");
        text = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text + "(value) {set_Fire(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"Fire\"," + text + ")");
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

    public Bitmap GetLogo()
    {
        return null;
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CTextBoxSaveItems cTextBoxSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            IsPassword = base.UseSystemPasswordChar,
            IsMultiline = base.Multiline,
            TabIndex = base.TabIndex,
            ScrollStyle = ScrollStyle,
            ReadOnly = base.ReadOnly,
            hide = !base.Visible,
            disable = !base.Enabled,
            textVarBind = textVarBind
        };
        formatter.Serialize(memoryStream, cTextBoxSaveItems);
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
            CTextBoxSaveItems cTextBoxSaveItems = (CTextBoxSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cTextBoxSaveItems.Text;
            base.BackColor = cTextBoxSaveItems.BackColor;
            base.ForeColor = cTextBoxSaveItems.ForeColor;
            base.Font = cTextBoxSaveItems.Font;
            base.UseSystemPasswordChar = cTextBoxSaveItems.IsPassword;
            base.Multiline = cTextBoxSaveItems.IsMultiline;
            base.TabIndex = cTextBoxSaveItems.TabIndex;
            base.ReadOnly = cTextBoxSaveItems.ReadOnly;
            ScrollStyle = cTextBoxSaveItems.ScrollStyle;
            base.Visible = !cTextBoxSaveItems.hide;
            base.Enabled = !cTextBoxSaveItems.disable;
            textVarBind = cTextBoxSaveItems.textVarBind;
        }
        catch (Exception)
        {
        }
    }

    public void Stop()
    {
    }

    private void ChangeEventScript(StringBuilder sb)
    {
        sb.AppendLine("var " + id + "_temp=\"\";");
        if (Text == "")
        {
            sb.AppendLine("_temp=\"\"");
        }
        else
        {
            sb.AppendLine(id + "_temp=\"" + Text.ToString() + "\";");
        }
        sb.AppendLine("//文本change事件");
        sb.AppendLine("function " + id + "_onTextChangeClick() {");
        sb.AppendLine(" if(" + id + "_temp!=get_Text_Text(\"" + id + "\"))");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\t_onText_Textchange(\"" + id + "\");");
        sb.AppendLine(id + "_temp=get_Text_Text(\"" + id + "\");");
        sb.AppendLine("\t}");
        sb.AppendLine("}");
        sb.AppendLine(id + "_onTextChangeClick();");
    }
}
