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
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("619A6A42-AA9F-4be9-A3B6-93EBA8B440AB")]
public class CDateTimePicker : DateTimePicker, IDCCEControl, IControlShape, ISupportHtml5
{
    private bool enablestate;

    private string id = "";

    [Browsable(false)]
    public bool isRuning { get; set; }

    [Browsable(false)]
    public bool Enablestate
    {
        get
        {
            return !enablestate;
        }
        set
        {
            enablestate = !value;
        }
    }

    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [DisplayName("ID")]
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
    public new Color CalendarForeColor
    {
        get
        {
            return base.CalendarForeColor;
        }
        set
        {
            base.CalendarForeColor = value;
        }
    }

    [Browsable(false)]
    public int Year
    {
        get
        {
            return base.Value.Year;
        }
        set
        {
            base.Value = new DateTime(value, base.Value.Month, base.Value.Day, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Month
    {
        get
        {
            return base.Value.Month;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, value, base.Value.Day, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Day
    {
        get
        {
            return base.Value.Day;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, value, base.Value.Hour, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Hour
    {
        get
        {
            return base.Value.Hour;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, value, base.Value.Minute, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Minute
    {
        get
        {
            return base.Value.Minute;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, base.Value.Hour, value, base.Value.Second);
        }
    }

    [Browsable(false)]
    public int Second
    {
        get
        {
            return base.Value.Second;
        }
        set
        {
            base.Value = new DateTime(base.Value.Year, base.Value.Month, base.Value.Day, base.Value.Hour, base.Value.Minute, value);
        }
    }

    [Browsable(false)]
    public new Font CalendarFont
    {
        get
        {
            return base.CalendarFont;
        }
        set
        {
            base.CalendarFont = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarMonthBackground
    {
        get
        {
            return base.CalendarMonthBackground;
        }
        set
        {
            base.CalendarMonthBackground = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTitleBackColor
    {
        get
        {
            return base.CalendarTitleBackColor;
        }
        set
        {
            base.CalendarTitleBackColor = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTitleForeColor
    {
        get
        {
            return base.CalendarTitleForeColor;
        }
        set
        {
            base.CalendarTitleForeColor = value;
        }
    }

    [Browsable(false)]
    public new Color CalendarTrailingForeColor
    {
        get
        {
            return base.CalendarTrailingForeColor;
        }
        set
        {
            base.CalendarTrailingForeColor = value;
        }
    }

    [Browsable(false)]
    public new LeftRightAlignment DropDownAlign
    {
        get
        {
            return base.DropDownAlign;
        }
        set
        {
            base.DropDownAlign = value;
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
    public new bool RightToLeftLayout
    {
        get
        {
            return base.RightToLeftLayout;
        }
        set
        {
            base.RightToLeftLayout = value;
        }
    }

    [Browsable(false)]
    public new bool ShowCheckBox
    {
        get
        {
            return base.ShowCheckBox;
        }
        set
        {
            base.ShowCheckBox = value;
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
    public string ReadOnly { get; set; }

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
    public new bool Checked
    {
        get
        {
            return base.Checked;
        }
        set
        {
            base.Checked = value;
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

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public new event EventHandler ValueChanged;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler IDChanged;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

    public Bitmap GetLogo()
    {
        return null;
    }

    public CDateTimePicker()
    {
        base.ValueChanged += CDateTimePicker_ValueChanged;
        base.MouseDown += CDateTimePicker_MouseDown;
        base.Format = DateTimePickerFormat.Custom;
        base.CustomFormat = "yyyy/MM/dd HH:mm:ss";
        ForeColor = Color.Black;
    }

    private void CDateTimePicker_MouseDown(object sender, MouseEventArgs e)
    {
        if (base.Format == DateTimePickerFormat.Short || base.Format == DateTimePickerFormat.Long)
        {
            SendMessage(new HandleRef(this, base.Handle), 260, 40, 0);
        }
    }

    private void CDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        if (this.ValueChanged != null)
        {
            this.ValueChanged(sender, e);
        }
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        CDateTimePickerSaveItems cDateTimePickerSaveItems = new()
        {
            Text = Text,
            BackColor = base.BackColor,
            ForeColor = base.ForeColor,
            Font = base.Font,
            TabIndex = TabIndex,
            hide = !base.Visible,
            disable = !base.Enabled,
            enablestate = enablestate
        };
        formatter.Serialize(memoryStream, cDateTimePickerSaveItems);
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
            CDateTimePickerSaveItems cDateTimePickerSaveItems = (CDateTimePickerSaveItems)formatter.Deserialize(stream);
            stream.Close();
            Text = cDateTimePickerSaveItems.Text;
            base.BackColor = cDateTimePickerSaveItems.BackColor;
            base.ForeColor = cDateTimePickerSaveItems.ForeColor;
            base.Font = cDateTimePickerSaveItems.Font;
            TabIndex = cDateTimePickerSaveItems.TabIndex;
            base.Visible = !cDateTimePickerSaveItems.hide;
            base.Enabled = !cDateTimePickerSaveItems.disable;
            enablestate = cDateTimePickerSaveItems.enablestate;
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
        bool flag = enablestate;
        string text = ((!flag) ? "" : " disabled=\"disabled\"");
        stringBuilder.Append("<div style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};position:absolute;width:" + base.Size.Width + "px;height:" + base.Size.Height + "px;top:" + base.Location.Y + "px;left:" + base.Location.X + "px\">");
        stringBuilder.Append("<input id=\"" + id + "\" type=\"text\"  " + text + " style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL}; position:absolute;width:" + base.Size.Width + "px;height:" + base.Size.Height + "px\"/>");
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
        string text = id + "_dom";
        stringBuilder.AppendLine(text + " = document.getElementById('" + id + "');");
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
                    }
                }
            }
        }
        stringBuilder.AppendLine("DatePicker_Init(\"" + id + "\")");
        string text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "() {return get_Text_SQLText(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"getSelectText\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {set_Text_DataSource(\"" + id + "\",value);}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value,col) {set_Text_DataSource(\"" + id + "\",value,col);}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"setSelectResult\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_DataPicker_ReadOnly(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_ReadOnly\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "() {");
        stringBuilder.AppendLine("return get_DataPicker_ReadOnly(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_ReadOnly\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_DataPicker_Text(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_Text\"," + text2 + ")");
        text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("return get_Text_Text(\"" + id + "\")}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"get_Text\"," + text2 + ")");
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
}
