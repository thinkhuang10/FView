using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class EventBindForm : Form
{
    public List<int> SafeRegion = new();

    private IEventBind selectST;

    private bool drty;

    private Dictionary<string, string> EventCon;

    private readonly List<KVPart<string, List<EventSetItem>>> saveitems = new();

    private IContainer components;

    private ComboBox comboBox1;

    private Label label1;

    private ListView listView3;

    private ColumnHeader columnHeader5;

    private ColumnHeader columnHeader6;

    private Button button1;

    private Button button2;

    private Button button4;

    private Button button5;

    private Button button6;

    private Button button7;

    private Button button8;

    private Button button9;

    private Button button11;

    private Button button12;

    private Button button10;

    private ColumnHeader columnHeader1;

    private ToolTip toolTip1;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 配置控件常用属性PToolStripMenuItem;

    public IEventBind SelectST
    {
        get
        {
            return selectST;
        }
        set
        {
            selectST = value;
        }
    }

    public EventBindForm(IEventBind item)
    {
        InitializeComponent();
        selectST = item;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        EventSetProperty eventSetProperty = new();
        if (eventSetProperty.ShowDialog() == DialogResult.OK)
        {
            drty = true;
            EventSetItem result = eventSetProperty.Result;
            result.EventName = comboBox1.SelectedText;
            ListViewItem listViewItem = new()
            {
                Text = result.OperationType,
                Tag = result
            };
            listViewItem.SubItems.Add(result.Condition);
            listViewItem.SubItems.Add(result.ToObject.Key + "=[" + result.FromObject + "]");
            listView3.Items.Add(listViewItem);
            SaveTheEvent();
        }
    }

    private void SaveTheEvent()
    {
        if (comboBox1.SelectedItem == null)
        {
            return;
        }
        List<EventSetItem> list = new();
        foreach (ListViewItem item in listView3.Items)
        {
            list.Add((EventSetItem)item.Tag);
        }
        ((KVPart<string, List<EventSetItem>>)comboBox1.SelectedItem).Value = list;
    }

    private void EventBindForm_Load(object sender, EventArgs e)
    {
        EventCon = new Dictionary<string, string>
        {
            { "OnPageShow", "页面显示" },
            { "OnPageHide", "页面隐藏" },
            { "OnPageRun", "页面运行" },
            { "页面显示", "OnPageShow" },
            { "页面隐藏", "OnPageHide" },
            { "页面运行", "OnPageRun" }
        };
        object obj = new();
        DataTable dataTable = null;
        Type type;
        if (selectST is CControl)
        {
            obj = (selectST as CControl)._c;
            type = obj.GetType();
            dataTable = CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name];
        }
        else
        {
            obj = selectST;
            type = obj.GetType();
        }
        EventInfo[] array = type.GetEvents();
        List<string> list = new();
        bool flag = true;
        if (dataTable == null)
        {
            flag = false;
        }
        else
        {
            DataRow[] array2 = dataTable.Select("Type='事件'");
            for (int i = 0; i < array2.Length; i++)
            {
                if (Convert.ToBoolean(array2[i]["Visible"]))
                {
                    list.Add(array2[i]["Name"].ToString());
                }
            }
        }
        Dictionary<string, List<EventSetItem>> dictionary = new();
        try
        {
            BinaryFormatter binaryFormatter = new();
            using MemoryStream memoryStream = new();
            binaryFormatter.Serialize(memoryStream, selectST.EventBindDict);
            using MemoryStream serializationStream = new(memoryStream.ToArray());
            dictionary = (Dictionary<string, List<EventSetItem>>)binaryFormatter.UnsafeDeserialize(serializationStream, null);
        }
        catch
        {
        }
        EventInfo[] array3 = array;
        foreach (EventInfo eventInfo in array3)
        {
            KVPart<string, List<EventSetItem>> item = (dictionary.ContainsKey(eventInfo.Name) ? ((!EventCon.ContainsKey(eventInfo.Name)) ? new KVPart<string, List<EventSetItem>>(eventInfo.Name, dictionary[eventInfo.Name]) : new KVPart<string, List<EventSetItem>>(EventCon[eventInfo.Name], dictionary[eventInfo.Name])) : ((!EventCon.ContainsKey(eventInfo.Name)) ? new KVPart<string, List<EventSetItem>>(eventInfo.Name, new List<EventSetItem>()) : new KVPart<string, List<EventSetItem>>(EventCon[eventInfo.Name], new List<EventSetItem>())));
            saveitems.Add(item);
            if (!flag || list.Contains(eventInfo.Name))
            {
                comboBox1.Items.Add(item);
            }
        }
        if (comboBox1.Items.Count > 0)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void RefreshBindView()
    {
        listView3.Items.Clear();
        List<EventSetItem> value = ((KVPart<string, List<EventSetItem>>)comboBox1.SelectedItem).Value;
        foreach (EventSetItem item in value)
        {
            AddEventSetItem(item);
        }
    }

    private void AddEventSetItem(EventSetItem item)
    {
        if (item.OperationType == "属性赋值")
        {
            ListViewItem listViewItem = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem.SubItems.Add(item.Condition);
            listViewItem.SubItems.Add(item.ToObject.Key + "=[" + item.FromObject + "]");
            listView3.Items.Add(listViewItem);
        }
        else if (item.OperationType == "变量赋值")
        {
            ListViewItem listViewItem2 = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem2.SubItems.Add(item.Condition);
            listViewItem2.SubItems.Add(item.ToObject.Key + "=[" + item.FromObject + "]");
            listView3.Items.Add(listViewItem2);
        }
        else if (item.OperationType == "方法调用")
        {
            string text = "";
            foreach (KVPart<string, string> para in item.Paras)
            {
                text = text + "[" + para.Key + "],";
            }
            if (text.Length != 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            string text2 = item.ToObject.Key;
            if (text2.Length != 0)
            {
                text2 += "=";
            }
            ListViewItem listViewItem3 = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem3.SubItems.Add(item.Condition);
            listViewItem3.SubItems.Add(text2 + item.FromObject + "(" + text + ")");
            listView3.Items.Add(listViewItem3);
        }
        else if (item.OperationType == "定义标签")
        {
            ListViewItem listViewItem4 = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem4.SubItems.Add(item.Condition);
            listViewItem4.SubItems.Add(item.FromObject);
            listView3.Items.Add(listViewItem4);
        }
        else if (item.OperationType == "跳转标签")
        {
            ListViewItem listViewItem5 = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem5.SubItems.Add(item.Condition);
            listViewItem5.SubItems.Add("goto -> " + item.FromObject);
            listView3.Items.Add(listViewItem5);
        }
        else if (item.OperationType == "服务器逻辑")
        {
            ListViewItem listViewItem6 = new()
            {
                Text = item.OperationType,
                Tag = item
            };
            listViewItem6.SubItems.Add(item.Condition);
            listViewItem6.SubItems.Add(item.FromObject);
            listView3.Items.Add(listViewItem6);
        }
        SaveTheEvent();
    }

    private void button9_Click(object sender, EventArgs e)
    {
        EventSetVar eventSetVar = new();
        if (eventSetVar.ShowDialog() == DialogResult.OK)
        {
            drty = true;
            EventSetItem result = eventSetVar.Result;
            result.EventName = comboBox1.SelectedText;
            ListViewItem listViewItem = new()
            {
                Text = result.OperationType,
                Tag = result
            };
            listViewItem.SubItems.Add(result.Condition);
            listViewItem.SubItems.Add("[" + result.ToObject.Key + "]=[" + result.FromObject + "]");
            listView3.Items.Add(listViewItem);
            SaveTheEvent();
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        EventSetFunction eventSetFunction = new();
        if (eventSetFunction.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        drty = true;
        EventSetItem result = eventSetFunction.Result;
        result.EventName = comboBox1.SelectedText;
        string text = "";
        foreach (KVPart<string, string> para in result.Paras)
        {
            text = text + "[" + para.Key + "],";
        }
        if (text.Length != 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        string text2 = result.ToObject.Key;
        if (text2.Length != 0)
        {
            text2 += "=";
        }
        ListViewItem listViewItem = new()
        {
            Text = result.OperationType,
            Tag = result
        };
        listViewItem.SubItems.Add(result.Condition);
        listViewItem.SubItems.Add(text2 + result.FromObject + "(" + text + ")");
        listView3.Items.Add(listViewItem);
        SaveTheEvent();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        drty = false;
        SaveTheEvent();
        Dictionary<string, List<EventSetItem>> dictionary = new();
        foreach (KVPart<string, List<EventSetItem>> saveitem in saveitems)
        {
            if (saveitem is KVPart<string, List<EventSetItem>>)
            {
                KVPart<string, List<EventSetItem>> kVPart = saveitem as KVPart<string, List<EventSetItem>>;
                if (EventCon.ContainsKey(kVPart.key))
                {
                    dictionary.Add(EventCon[kVPart.Key], kVPart.Value);
                }
                else
                {
                    dictionary.Add(kVPart.key, kVPart.Value);
                }
            }
        }
        selectST.EventBindDict = dictionary;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button8_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void button5_Click(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count != 0)
        {
            drty = true;
            ListViewItem listViewItem = listView3.SelectedItems[0];
            int index = listViewItem.Index;
            listView3.Items.Remove(listViewItem);
            listView3.Items.Insert((index - 1 >= 0) ? (index - 1) : 0, listViewItem);
            SaveTheEvent();
        }
    }

    private void button6_Click(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count != 0)
        {
            drty = true;
            ListViewItem listViewItem = listView3.SelectedItems[0];
            int index = listViewItem.Index;
            listView3.Items.Remove(listViewItem);
            listView3.Items.Insert((index + 1 > listView3.Items.Count) ? listView3.Items.Count : (index + 1), listViewItem);
            SaveTheEvent();
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count != 0)
        {
            drty = true;
            listView3.Items.Remove(listView3.SelectedItems[0]);
            SaveTheEvent();
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshBindView();
    }

    private void listView3_DoubleClick(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count == 0)
        {
            return;
        }
        ListViewItem listViewItem = listView3.SelectedItems[0];
        if (listViewItem.Text == "属性赋值")
        {
            EventSetProperty eventSetProperty = new((EventSetItem)listViewItem.Tag);
            if (eventSetProperty.ShowDialog() == DialogResult.OK)
            {
                drty = true;
                EventSetItem result = eventSetProperty.Result;
                result.EventName = comboBox1.SelectedText;
                listView3.SelectedItems[0].Text = result.OperationType;
                listView3.SelectedItems[0].Tag = result;
                listView3.SelectedItems[0].SubItems[1].Text = result.Condition;
                listView3.SelectedItems[0].SubItems[2].Text = result.ToObject.Key + "=[" + result.FromObject + "]";
                SaveTheEvent();
            }
        }
        else if (listViewItem.Text == "变量赋值")
        {
            EventSetVar eventSetVar = new((EventSetItem)listViewItem.Tag);
            if (eventSetVar.ShowDialog() == DialogResult.OK)
            {
                drty = true;
                EventSetItem result2 = eventSetVar.Result;
                result2.EventName = comboBox1.SelectedText;
                listView3.SelectedItems[0].Text = result2.OperationType;
                listView3.SelectedItems[0].Tag = result2;
                listView3.SelectedItems[0].SubItems[1].Text = result2.Condition;
                listView3.SelectedItems[0].SubItems[2].Text = "[" + result2.ToObject.Key + "]=[" + result2.FromObject + "]";
                SaveTheEvent();
            }
        }
        else if (listViewItem.Text == "方法调用")
        {
            EventSetFunction eventSetFunction = new((EventSetItem)listViewItem.Tag);
            if (eventSetFunction.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            drty = true;
            EventSetItem result3 = eventSetFunction.Result;
            result3.EventName = comboBox1.SelectedText;
            string text = "";
            foreach (KVPart<string, string> para in result3.Paras)
            {
                text = text + "[" + para.Key + "],";
            }
            if (text.Length != 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            string text2 = result3.ToObject.Key;
            if (text2.Length != 0)
            {
                text2 += "=";
            }
            listView3.SelectedItems[0].Text = result3.OperationType;
            listView3.SelectedItems[0].Tag = result3;
            listView3.SelectedItems[0].SubItems[1].Text = result3.Condition;
            listView3.SelectedItems[0].SubItems[2].Text = text2 + result3.FromObject + "(" + text + ")";
            SaveTheEvent();
        }
        else if (listViewItem.Text == "定义标签")
        {
            EventSetLabel eventSetLabel = new()
            {
                Result = (EventSetItem)listViewItem.Tag
            };
            if (eventSetLabel.ShowDialog() == DialogResult.OK)
            {
                drty = true;
                EventSetItem result4 = eventSetLabel.Result;
                result4.EventName = comboBox1.SelectedText;
                listView3.SelectedItems[0].Text = result4.OperationType;
                listView3.SelectedItems[0].Tag = result4;
                listView3.SelectedItems[0].SubItems[1].Text = result4.Condition;
                listView3.SelectedItems[0].SubItems[2].Text = result4.FromObject;
                SaveTheEvent();
            }
        }
        else if (listViewItem.Text == "跳转标签")
        {
            List<string> list = new();
            foreach (ListViewItem item in listView3.Items)
            {
                if (((EventSetItem)item.Tag).OperationType == "定义标签")
                {
                    list.Add(((EventSetItem)item.Tag).FromObject);
                }
            }
            EventSetGoto eventSetGoto = new(list)
            {
                Result = (EventSetItem)listViewItem.Tag
            };
            if (eventSetGoto.ShowDialog() == DialogResult.OK)
            {
                drty = true;
                EventSetItem result5 = eventSetGoto.Result;
                result5.EventName = comboBox1.SelectedText;
                listView3.SelectedItems[0].Text = result5.OperationType;
                listView3.SelectedItems[0].Tag = result5;
                listView3.SelectedItems[0].SubItems[1].Text = result5.Condition;
                listView3.SelectedItems[0].SubItems[2].Text = "goto->" + result5.FromObject;
                SaveTheEvent();
            }
        }
        else if (listViewItem.Text == "服务器逻辑")
        {
            EventSetServerLogic eventSetServerLogic = new()
            {
                Result = (EventSetItem)listViewItem.Tag
            };
            if (eventSetServerLogic.ShowDialog() == DialogResult.OK)
            {
                drty = true;
                EventSetItem result6 = eventSetServerLogic.Result;
                result6.EventName = comboBox1.SelectedText;
                listView3.SelectedItems[0].Text = result6.OperationType;
                listView3.SelectedItems[0].Tag = result6;
                listView3.SelectedItems[0].SubItems[1].Text = result6.Condition;
                listView3.SelectedItems[0].SubItems[2].Text = result6.FromObject;
                SaveTheEvent();
            }
        }
    }

    private void listView3_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView3.SelectedItems.Count == 0)
                {
                    return;
                }
                drty = true;
                listView3.Items.Remove(listView3.SelectedItems[0]);
            }
            if (e.Modifiers.CompareTo(Keys.Control) != 0)
            {
                return;
            }
            if (e.KeyCode == Keys.C)
            {
                try
                {
                    if (listView3.SelectedItems.Count == 0)
                    {
                        return;
                    }
                    List<EventSetItem> list = new();
                    foreach (ListViewItem selectedItem in listView3.SelectedItems)
                    {
                        if (selectedItem.Tag is EventSetItem)
                        {
                            list.Add(selectedItem.Tag as EventSetItem);
                        }
                    }
                    Clipboard.SetDataObject(list);
                }
                catch
                {
                }
            }
            if (e.KeyCode != Keys.V)
            {
                return;
            }
            try
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                List<EventSetItem> list2 = (List<EventSetItem>)dataObject.GetData(typeof(List<EventSetItem>));
                if (list2 == null || list2.Count == 0)
                {
                    return;
                }
                BinaryFormatter binaryFormatter = new();
                using (MemoryStream memoryStream = new())
                {
                    binaryFormatter.Serialize(memoryStream, list2);
                    using MemoryStream serializationStream = new(memoryStream.ToArray());
                    list2 = (List<EventSetItem>)binaryFormatter.UnsafeDeserialize(serializationStream, null);
                }
                foreach (EventSetItem item in list2)
                {
                    AddEventSetItem(item);
                }
            }
            catch
            {
            }
        }
        catch
        {
        }
    }

    private void EventBindForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (drty && MessageBox.Show("配置信息尚未保存,是否退出?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void button11_Click(object sender, EventArgs e)
    {
        EventSetLabel eventSetLabel = new();
        if (eventSetLabel.ShowDialog() == DialogResult.OK)
        {
            drty = true;
            EventSetItem result = eventSetLabel.Result;
            result.EventName = comboBox1.SelectedText;
            ListViewItem listViewItem = new()
            {
                Text = result.OperationType,
                Tag = result
            };
            listViewItem.SubItems.Add(result.Condition);
            listViewItem.SubItems.Add(result.FromObject);
            listView3.Items.Add(listViewItem);
            SaveTheEvent();
        }
    }

    private void button12_Click(object sender, EventArgs e)
    {
        List<string> list = new();
        foreach (ListViewItem item in listView3.Items)
        {
            if (((EventSetItem)item.Tag).OperationType == "定义标签")
            {
                list.Add(((EventSetItem)item.Tag).FromObject);
            }
        }
        EventSetGoto eventSetGoto = new(list);
        if (eventSetGoto.ShowDialog() == DialogResult.OK)
        {
            drty = true;
            EventSetItem result = eventSetGoto.Result;
            result.EventName = comboBox1.SelectedText;
            ListViewItem listViewItem2 = new()
            {
                Text = result.OperationType,
                Tag = result
            };
            listViewItem2.SubItems.Add(result.Condition);
            listViewItem2.SubItems.Add("goto->" + result.FromObject);
            listView3.Items.Add(listViewItem2);
            SaveTheEvent();
        }
    }

    private void button10_Click(object sender, EventArgs e)
    {
        EventSetServerLogic eventSetServerLogic = new();
        if (eventSetServerLogic.ShowDialog() == DialogResult.OK)
        {
            drty = true;
            EventSetItem result = eventSetServerLogic.Result;
            result.EventName = comboBox1.SelectedText;
            ListViewItem listViewItem = new()
            {
                Text = result.OperationType,
                Tag = result
            };
            listViewItem.SubItems.Add(result.Condition);
            listViewItem.SubItems.Add(result.FromObject);
            listView3.Items.Add(listViewItem);
            SaveTheEvent();
        }
    }

    private void listView3_SizeChanged(object sender, EventArgs e)
    {
        int num = listView3.Width;
        listView3.Columns[0].Width = 78;
        listView3.Columns[1].Width = 150;
        if (num - 78 - 150 > 0)
        {
            listView3.Columns[2].Width = num - 78 - 150;
        }
    }

    private void listView3_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void 配置控件常用属性PToolStripMenuItem_Click(object sender, EventArgs e)
    {
        object obj = ((!(selectST is CControl)) ? ((object)selectST) : ((object)(selectST as CControl)._c));
        Type type = obj.GetType();
        ControlInfoSetting controlInfoSetting = new(type.Name);
        controlInfoSetting.ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.配置控件常用属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.label1 = new System.Windows.Forms.Label();
        this.listView3 = new System.Windows.Forms.ListView();
        this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.button11 = new System.Windows.Forms.Button();
        this.button12 = new System.Windows.Forms.Button();
        this.button10 = new System.Windows.Forms.Button();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.contextMenuStrip1.SuspendLayout();
        base.SuspendLayout();
        this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox1.ContextMenuStrip = this.contextMenuStrip1;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(53, 12);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(540, 20);
        this.comboBox1.TabIndex = 0;
        this.toolTip1.SetToolTip(this.comboBox1, "显示当前选中控件（或页面）的所有事件，当触发事件后会执行该事件下所有的操作。");
        this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.配置控件常用属性PToolStripMenuItem });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(188, 26);
        this.配置控件常用属性PToolStripMenuItem.Name = "配置控件常用属性PToolStripMenuItem";
        this.配置控件常用属性PToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        this.配置控件常用属性PToolStripMenuItem.Text = "配置控件常用属性(&P)";
        this.配置控件常用属性PToolStripMenuItem.Click += new System.EventHandler(配置控件常用属性PToolStripMenuItem_Click);
        this.label1.AutoSize = true;
        this.label1.ContextMenuStrip = this.contextMenuStrip1;
        this.label1.Location = new System.Drawing.Point(12, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(35, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "事件:";
        this.listView3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.columnHeader5, this.columnHeader1, this.columnHeader6 });
        this.listView3.ContextMenuStrip = this.contextMenuStrip1;
        this.listView3.FullRowSelect = true;
        this.listView3.GridLines = true;
        this.listView3.HideSelection = false;
        this.listView3.Location = new System.Drawing.Point(14, 38);
        this.listView3.Name = "listView3";
        this.listView3.Size = new System.Drawing.Size(579, 335);
        this.listView3.TabIndex = 1;
        this.toolTip1.SetToolTip(this.listView3, "对应事件的所有操作，执行顺序由上到下，可通过右边的↑↓按钮改变顺序，双击编辑；\r\n支持Ctrl+C复制和Ctrl+V粘贴。");
        this.listView3.UseCompatibleStateImageBehavior = false;
        this.listView3.View = System.Windows.Forms.View.Details;
        this.listView3.SelectedIndexChanged += new System.EventHandler(listView3_SelectedIndexChanged);
        this.listView3.SizeChanged += new System.EventHandler(listView3_SizeChanged);
        this.listView3.DoubleClick += new System.EventHandler(listView3_DoubleClick);
        this.listView3.KeyDown += new System.Windows.Forms.KeyEventHandler(listView3_KeyDown);
        this.columnHeader5.Text = "操作类型";
        this.columnHeader5.Width = 120;
        this.columnHeader1.Text = "条件";
        this.columnHeader1.Width = 97;
        this.columnHeader6.Text = "操作表达式";
        this.columnHeader6.Width = 424;
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button1.ContextMenuStrip = this.contextMenuStrip1;
        this.button1.Location = new System.Drawing.Point(599, 79);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 3;
        this.button1.Text = "属性赋值";
        this.toolTip1.SetToolTip(this.button1, "当触发上面对应的事件后，\r\n执行属性赋值");
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.ContextMenuStrip = this.contextMenuStrip1;
        this.button2.Location = new System.Drawing.Point(599, 108);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 4;
        this.button2.Text = "方法调用";
        this.toolTip1.SetToolTip(this.button2, "当触发上面对应的事件后，\r\n执行对任意页面的任意控件的某一方法调用");
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button4.ContextMenuStrip = this.contextMenuStrip1;
        this.button4.Location = new System.Drawing.Point(599, 282);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 10;
        this.button4.Text = "删  除";
        this.toolTip1.SetToolTip(this.button4, "删除一条选中的操作");
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button5.ContextMenuStrip = this.contextMenuStrip1;
        this.button5.Location = new System.Drawing.Point(599, 224);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 8;
        this.button5.Text = "↑";
        this.toolTip1.SetToolTip(this.button5, "向上移动");
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button6.ContextMenuStrip = this.contextMenuStrip1;
        this.button6.Location = new System.Drawing.Point(599, 253);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(75, 23);
        this.button6.TabIndex = 9;
        this.button6.Text = "↓";
        this.toolTip1.SetToolTip(this.button6, "向下移动");
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        this.button7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button7.ContextMenuStrip = this.contextMenuStrip1;
        this.button7.Location = new System.Drawing.Point(599, 321);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(75, 23);
        this.button7.TabIndex = 11;
        this.button7.Text = "保  存";
        this.toolTip1.SetToolTip(this.button7, "保存");
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(button7_Click);
        this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button8.ContextMenuStrip = this.contextMenuStrip1;
        this.button8.Location = new System.Drawing.Point(599, 350);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 23);
        this.button8.TabIndex = 12;
        this.button8.Text = "关  闭";
        this.toolTip1.SetToolTip(this.button8, "退出不保存");
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(button8_Click);
        this.button9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button9.ContextMenuStrip = this.contextMenuStrip1;
        this.button9.Location = new System.Drawing.Point(599, 50);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(75, 23);
        this.button9.TabIndex = 2;
        this.button9.Text = "变量赋值";
        this.toolTip1.SetToolTip(this.button9, "当触发上面对应的事件后，\r\n执行变量赋值");
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        this.button11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button11.ContextMenuStrip = this.contextMenuStrip1;
        this.button11.Location = new System.Drawing.Point(599, 166);
        this.button11.Name = "button11";
        this.button11.Size = new System.Drawing.Size(75, 23);
        this.button11.TabIndex = 6;
        this.button11.Text = "定义标签";
        this.toolTip1.SetToolTip(this.button11, "定义一个标记点");
        this.button11.UseVisualStyleBackColor = true;
        this.button11.Click += new System.EventHandler(button11_Click);
        this.button12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button12.ContextMenuStrip = this.contextMenuStrip1;
        this.button12.Location = new System.Drawing.Point(599, 195);
        this.button12.Name = "button12";
        this.button12.Size = new System.Drawing.Size(75, 23);
        this.button12.TabIndex = 7;
        this.button12.Text = "跳转标签";
        this.toolTip1.SetToolTip(this.button12, "满足条件后跳转至任一定义过的标记点");
        this.button12.UseVisualStyleBackColor = true;
        this.button12.Click += new System.EventHandler(button12_Click);
        this.button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button10.ContextMenuStrip = this.contextMenuStrip1;
        this.button10.Location = new System.Drawing.Point(599, 137);
        this.button10.Name = "button10";
        this.button10.Size = new System.Drawing.Size(75, 23);
        this.button10.TabIndex = 5;
        this.button10.Text = "服务器逻辑";
        this.toolTip1.SetToolTip(this.button10, "使用服务器逻辑能有效的减少网络报文数量");
        this.button10.UseVisualStyleBackColor = true;
        this.button10.Click += new System.EventHandler(button10_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(690, 385);
        this.ContextMenuStrip = this.contextMenuStrip1;
        base.Controls.Add(this.button12);
        base.Controls.Add(this.button11);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button8);
        base.Controls.Add(this.button7);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button10);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.listView3);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventBindForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "事件绑定";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(EventBindForm_FormClosing);
        base.Load += new System.EventHandler(EventBindForm_Load);
        this.contextMenuStrip1.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
