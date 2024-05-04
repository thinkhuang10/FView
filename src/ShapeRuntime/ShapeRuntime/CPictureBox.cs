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

namespace ShapeRuntime;

[Serializable]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("DC8C74FC-902F-4360-B190-CF0F1A6782F7")]
[ComVisible(true)]
public class CPictureBox : PictureBox, IDCCEControl, IControlShape, ISupportHtml5
{
    private string id = "";

    [NonSerialized]
    private Image _img;

    private string ImageName = "";

    [Browsable(false)]
    public bool isRuning { get; set; }

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

    [Description("设定控件名称。")]
    [ReadOnly(false)]
    [Category("设计")]
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
            if (this.IDChanged != null)
            {
                this.IDChanged(this, null);
            }
        }
    }

    [Editor(typeof(VarTableImageManage), typeof(UITypeEditor))]
    [DisplayName("Image")]
    [Category("外观")]
    [Description("在控件中显示的图像。")]
    [ReadOnly(false)]
    [DHMINeedSerProperty]
    public new BitMapForIM Image
    {
        get
        {
            BitMapForIM bitMapForIM = new()
            {
                img = _img,
                ImgGUID = ImageName
            };
            return bitMapForIM;
        }
        set
        {
            if (value != null)
            {
                Image img2 = (base.Image = value.img);
                _img = img2;
                ImageName = value.ImgGUID;
            }
            else
            {
                Image img3 = (base.Image = null);
                _img = img3;
            }
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [Description("返回图片在控件中的布局")]
    [DisplayName("图片布局")]
    public PictureBoxSizeMode FillSizeMode
    {
        get
        {
            return SizeMode;
        }
        set
        {
            SizeMode = value;
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
    public new PictureBoxSizeMode SizeMode
    {
        get
        {
            return base.SizeMode;
        }
        set
        {
            base.SizeMode = value;
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
    public new Padding Padding
    {
        get
        {
            return base.Padding;
        }
        set
        {
            base.Padding = value;
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
    public new Image ErrorImage
    {
        get
        {
            return base.ErrorImage;
        }
        set
        {
            base.ErrorImage = value;
        }
    }

    [Browsable(false)]
    public new string ImageLocation
    {
        get
        {
            return base.ImageLocation;
        }
        set
        {
            base.ImageLocation = value;
        }
    }

    [Browsable(false)]
    public new Image InitialImage
    {
        get
        {
            return base.InitialImage;
        }
        set
        {
            base.InitialImage = value;
        }
    }

    [Browsable(false)]
    public new bool WaitOnLoad
    {
        get
        {
            return base.WaitOnLoad;
        }
        set
        {
            base.WaitOnLoad = value;
        }
    }

    public event GetValue GetValueEvent;

    public event SetValue SetValueEvent;

    public event GetDataBase GetDataBaseEvent;

    public event GetVarTable GetVarTableEvent;

    public event GetValue GetSystemItemEvent;

    public new event EventHandler Click;

    public event requestEventBindDictDele requestEventBindDict;

    public event requestPropertyBindDataDele requestPropertyBindData;

    public event EventHandler IDChanged;

    public Bitmap GetLogo()
    {
        return null;
    }

    public CPictureBox()
    {
        base.Click += CPictureBox_Click;
        BackColor = SystemColors.GrayText;
        ForeColor = Color.Black;
    }

    private void CPictureBox_Click(object sender, EventArgs e)
    {
        if (this.Click != null)
        {
            this.Click(sender, e);
        }
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        try
        {
            base.OnPaint(pe);
        }
        catch (Exception)
        {
            Image = null;
        }
    }

    public byte[] Serialize()
    {
        if (Image != null && Image.img != null && string.IsNullOrEmpty(ImageName))
        {
            ImageName = DHMIImageManage.SaveImage(Image.img);
        }
        using MemoryStream memoryStream = new();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(memoryStream, new CPictureBoxSaveItems
        {
            FillSizeMode = Convert.ToInt32(SizeMode),
            hide = !base.Visible,
            disable = !base.Enabled,
            ImageTag = ImageName
        });
        return memoryStream.ToArray();
    }

    public void DeSerialize(byte[] bytes)
    {
        try
        {
            CPictureBoxSaveItems cPictureBoxSaveItems = null;
            using (Stream serializationStream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                cPictureBoxSaveItems = (CPictureBoxSaveItems)formatter.Deserialize(serializationStream);
            }
            try
            {
                FillSizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), cPictureBoxSaveItems.FillSizeMode.ToString());
            }
            catch
            {
            }
            base.Visible = !cPictureBoxSaveItems.hide;
            base.Enabled = !cPictureBoxSaveItems.disable;
            ImageName = cPictureBoxSaveItems.ImageTag;
            if (!string.IsNullOrEmpty(ImageName))
            {
                Image = new BitMapForIM
                {
                    img = DHMIImageManage.LoadImage(ImageName),
                    ImgGUID = ImageName
                };
            }
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
        string text2 = ImageName != null && !(ImageName == "") ? "\"/Resources/" + ImageName + "\"" : "\"\"";
        stringBuilder.Append(string.Concat("<img id=\"", id, "\" src=", text2, "onclick=\"_onclick('", id, "');\" style=\"z-index:{Z_INDEX_REPLACE_BY_CCONTROL};visibility:", text, "; display:inline; position:absolute; left:", base.Location.X, "px; top:", base.Location.Y, "px;width:", base.Width.ToString(), "px;height:", base.Height.ToString(), "px;background-color:", ColorTranslator.ToHtml(BackColor), ";color:", ColorTranslator.ToHtml(ForeColor), ";font-Size:", base.Font.Size, "pt; font-Style:", base.Font.Style, "; font-Weight:", base.Font.OriginalFontName, ";\" width=\"", base.Width.ToString(), "px\" height=\"", base.Height.ToString(), "px\" ></img>"));
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
                        case "DBOperationOK":
                            stringBuilder.AppendLine("function " + id + "_event_" + key + "(){");
                            MakeEvent(stringBuilder, dictionary, key);
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
        string text2 = "_" + Guid.NewGuid().ToString().Replace("-", "");
        stringBuilder.AppendLine("function " + text2 + "(value) {");
        stringBuilder.AppendLine("set_CPictureBox_Draw(\"" + id + "\",value)}");
        stringBuilder.AppendLine("$(\"#" + id + "\").data(\"set_CPictureBox_Draw\"," + text2 + ")");
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
