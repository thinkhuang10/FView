using HMIEditEnvironment.ServerLogic;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventSetServerLogic : Form
{
    private EventSetItem result;

    private Button button1;

    private Button button2;

    private GroupBox groupBox1;

    private Button button5;

    private GroupBox groupBox2;

    private ListBox listBox2;

    private Button button3;

    private Button button4;

    private Button button6;

    private ListBox listBox1;

    private ListView listView1;

    private ColumnHeader columnHeader1;

    private ColumnHeader columnHeader2;

    private Button button7;

    private Button button8;

    private Button button9;

    private Button button10;

    private Button button11;

    private Button button12;

    private Button button13;

    private Button button14;

    private Button button15;

    private Button button16;

    private Button button17;

    private Button button18;

    private TableLayoutPanel tableLayoutPanel1;

    private Button button19;

    private Button button20;

    private TextBox textBox1;

    private Label label1;

    private TextBox textBox2;

    private Label label2;

    private Button button21;

    private CheckBox checkBox1;

    private ColumnHeader columnHeader3;

    public EventSetItem Result
    {
        get
        {
            return result;
        }
        set
        {
            result = value;
        }
    }

    public EventSetServerLogic()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent.Contains("|"))
        {
            string[] array = varTableEvent.Split('|');
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (!listBox1.Items.Contains("[" + text + "]"))
                {
                    listBox1.Items.Add("[" + text + "]");
                }
            }
        }
        else if (!listBox1.Items.Contains("[" + varTableEvent + "]") && varTableEvent != "")
        {
            listBox1.Items.Add("[" + varTableEvent + "]");
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent.Contains("|"))
        {
            string[] array = varTableEvent.Split('|');
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (!listBox2.Items.Contains("[" + text + "]"))
                {
                    listBox2.Items.Add("[" + text + "]");
                }
            }
        }
        else if (!listBox2.Items.Contains("[" + varTableEvent + "]") && varTableEvent != "")
        {
            listBox2.Items.Add("[" + varTableEvent + "]");
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        List<object> list = new();
        foreach (object selectedItem in listBox1.SelectedItems)
        {
            list.Add(selectedItem);
        }
        foreach (object item in list)
        {
            listBox1.Items.Remove(item);
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        List<object> list = new();
        foreach (object selectedItem in listBox2.SelectedItems)
        {
            list.Add(selectedItem);
        }
        foreach (object item in list)
        {
            listBox2.Items.Remove(item);
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        listBox2.Items.Clear();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        SLSetVar sLSetVar = new(canUseVars);
        if (sLSetVar.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetVar.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private void button19_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < listBox1.Items.Count; i++)
        {
            listBox1.SetSelected(i, !listBox1.GetSelected(i));
        }
    }

    private void button20_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < listBox2.Items.Count; i++)
        {
            listBox2.SetSelected(i, !listBox2.GetSelected(i));
        }
    }

    private void button12_Click(object sender, EventArgs e)
    {
        ServerLogicRequest serverLogicRequest = new()
        {
            InputDict = new Dictionary<string, object>()
        };
        foreach (object item in listBox1.Items)
        {
            if (!serverLogicRequest.InputDict.ContainsKey(item.ToString()))
            {
                serverLogicRequest.InputDict.Add(item.ToString(), null);
            }
        }
        serverLogicRequest.OutputList = new List<string>();
        foreach (object item2 in listBox2.Items)
        {
            if (!serverLogicRequest.OutputList.Contains(item2.ToString()))
            {
                serverLogicRequest.OutputList.Add(item2.ToString());
            }
        }
        serverLogicRequest.LogicItemList = new List<ServerLogicItem>();
        foreach (ListViewItem item3 in listView1.Items)
        {
            serverLogicRequest.LogicItemList.Add(item3.Tag as ServerLogicItem);
        }
        serverLogicRequest.Ansync = checkBox1.Checked;
        result = new EventSetItem
        {
            OperationType = "服务器逻辑",
            Condition = textBox2.Text,
            FromObject = textBox1.Text,
            Tag = serverLogicRequest
        };
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button13_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void EventSetServerLogic_Load(object sender, EventArgs e)
    {
        if (result == null)
        {
            return;
        }
        textBox2.Text = result.Condition;
        textBox1.Text = result.FromObject;
        ServerLogicRequest serverLogicRequest = result.Tag as ServerLogicRequest;
        checkBox1.Checked = serverLogicRequest.Ansync;
        foreach (string key in serverLogicRequest.InputDict.Keys)
        {
            listBox1.Items.Add(key);
        }
        foreach (string output in serverLogicRequest.OutputList)
        {
            listBox2.Items.Add(output);
        }
        foreach (ServerLogicItem logicItem in serverLogicRequest.LogicItemList)
        {
            AddServerLogicItem(logicItem);
        }
    }

    private void AddServerLogicItem(ServerLogicItem item)
    {
        ListViewItem listViewItem = new()
        {
            Text = item.LogicType,
            Tag = item
        };
        listViewItem.SubItems.Add(item.GetDesc());
        listViewItem.SubItems.Add(item.ConditionalExpression);
        listView1.Items.Add(listViewItem);
    }

    private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        ListViewItem itemAt = listView1.GetItemAt(e.X, e.Y);
        if (itemAt == null)
        {
            return;
        }
        List<string> canUseVars = GetCanUseVars();
        if (itemAt.Text == "变量赋值")
        {
            ServerLogicItem item = (ServerLogicItem)itemAt.Tag;
            SLSetVar sLSetVar = new(canUseVars, item);
            if (sLSetVar.ShowDialog() == DialogResult.OK)
            {
                item = sLSetVar.Result;
                itemAt.Text = item.LogicType;
                itemAt.Tag = item;
                itemAt.SubItems[2].Text = item.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item.GetDesc();
            }
        }
        else if (itemAt.Text == "查询数据")
        {
            ServerLogicItem item2 = (ServerLogicItem)itemAt.Tag;
            SLSetDBSelect sLSetDBSelect = new(canUseVars, item2);
            if (sLSetDBSelect.ShowDialog() == DialogResult.OK)
            {
                item2 = sLSetDBSelect.Result;
                itemAt.Text = item2.LogicType;
                itemAt.Tag = item2;
                itemAt.SubItems[2].Text = item2.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item2.GetDesc();
            }
        }
        else if (itemAt.Text == "添加数据")
        {
            ServerLogicItem item3 = (ServerLogicItem)itemAt.Tag;
            SLSetDBInsert sLSetDBInsert = new(canUseVars, item3);
            if (sLSetDBInsert.ShowDialog() == DialogResult.OK)
            {
                item3 = sLSetDBInsert.Result;
                itemAt.Text = item3.LogicType;
                itemAt.Tag = item3;
                itemAt.SubItems[1].Text = item3.GetDesc();
                itemAt.SubItems[2].Text = item3.ConditionalExpression.ToString();
            }
        }
        else if (itemAt.Text == "更新数据")
        {
            ServerLogicItem item4 = (ServerLogicItem)itemAt.Tag;
            SLSetDBUpdate sLSetDBUpdate = new(canUseVars, item4);
            if (sLSetDBUpdate.ShowDialog() == DialogResult.OK)
            {
                item4 = sLSetDBUpdate.Result;
                itemAt.Text = item4.LogicType;
                itemAt.Tag = item4;
                itemAt.SubItems[2].Text = item4.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item4.GetDesc();
            }
        }
        else if (itemAt.Text == "删除数据")
        {
            ServerLogicItem item5 = (ServerLogicItem)itemAt.Tag;
            SLSetDBDelete sLSetDBDelete = new(canUseVars, item5);
            if (sLSetDBDelete.ShowDialog() == DialogResult.OK)
            {
                item5 = sLSetDBDelete.Result;
                itemAt.Text = item5.LogicType;
                itemAt.Tag = item5;
                itemAt.SubItems[2].Text = item5.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item5.GetDesc();
            }
        }
        else if (itemAt.Text == "定义标签")
        {
            ServerLogicItem item6 = (ServerLogicItem)itemAt.Tag;
            SLSetLabel sLSetLabel = new(item6);
            if (sLSetLabel.ShowDialog() == DialogResult.OK)
            {
                item6 = sLSetLabel.Result;
                itemAt.Text = item6.LogicType;
                itemAt.Tag = item6;
                itemAt.SubItems[2].Text = item6.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item6.GetDesc();
            }
        }
        else
        {
            if (!(itemAt.Text == "跳转标签"))
            {
                return;
            }
            ServerLogicItem item7 = (ServerLogicItem)itemAt.Tag;
            List<string> list = new();
            foreach (ListViewItem item8 in listView1.Items)
            {
                if (((ServerLogicItem)item8.Tag).LogicType == "定义标签")
                {
                    list.Add(((ServerLogicItem)itemAt.Tag).DataDict["Name"].ToString());
                }
            }
            SLSetGoto sLSetGoto = new(list, canUseVars, item7);
            if (sLSetGoto.ShowDialog() == DialogResult.OK)
            {
                item7 = sLSetGoto.Result;
                itemAt.Text = item7.LogicType;
                itemAt.Tag = item7;
                itemAt.SubItems[2].Text = item7.ConditionalExpression.ToString();
                itemAt.SubItems[1].Text = item7.GetDesc();
            }
        }
    }

    private void button8_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        SLSetDBSelect sLSetDBSelect = new(canUseVars);
        if (sLSetDBSelect.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetDBSelect.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private List<string> GetCanUseVars()
    {
        List<string> list = new();
        foreach (object item in listBox1.Items)
        {
            if (!list.Contains(item.ToString()))
            {
                list.Add(item.ToString());
            }
        }
        foreach (object item2 in listBox2.Items)
        {
            if (!list.Contains(item2.ToString()))
            {
                list.Add(item2.ToString());
            }
        }
        return list;
    }

    private void button16_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count != 0)
        {
            ListViewItem listViewItem = listView1.SelectedItems[0];
            int index = listViewItem.Index;
            listView1.Items.Remove(listViewItem);
            listView1.Items.Insert((index - 1 >= 0) ? (index - 1) : 0, listViewItem);
        }
    }

    private void button17_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count != 0)
        {
            ListViewItem listViewItem = listView1.SelectedItems[0];
            int index = listViewItem.Index;
            listView1.Items.Remove(listViewItem);
            listView1.Items.Insert((index + 1 > listView1.Items.Count) ? listView1.Items.Count : (index + 1), listViewItem);
        }
    }

    private void button18_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count != 0)
        {
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }
    }

    private void listView1_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    return;
                }
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
            if (e.Modifiers.CompareTo(Keys.Control) != 0)
            {
                return;
            }
            if (e.KeyCode == Keys.C)
            {
                try
                {
                    if (listView1.SelectedItems.Count == 0)
                    {
                        return;
                    }
                    List<ServerLogicItem> list = new();
                    foreach (ListViewItem selectedItem in listView1.SelectedItems)
                    {
                        if (selectedItem.Tag is ServerLogicItem)
                        {
                            list.Add(selectedItem.Tag as ServerLogicItem);
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
                List<ServerLogicItem> list2 = (List<ServerLogicItem>)dataObject.GetData(typeof(List<ServerLogicItem>));
                if (list2 == null || list2.Count == 0)
                {
                    return;
                }
                BinaryFormatter binaryFormatter = new();
                using (MemoryStream memoryStream = new())
                {
                    binaryFormatter.Serialize(memoryStream, list2);
                    using MemoryStream serializationStream = new(memoryStream.ToArray());
                    list2 = (List<ServerLogicItem>)binaryFormatter.UnsafeDeserialize(serializationStream, null);
                }
                foreach (ServerLogicItem item in list2)
                {
                    AddServerLogicItem(item);
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

    private void button9_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        SLSetDBInsert sLSetDBInsert = new(canUseVars);
        if (sLSetDBInsert.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetDBInsert.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private void button10_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        SLSetDBUpdate sLSetDBUpdate = new(canUseVars);
        if (sLSetDBUpdate.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetDBUpdate.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private void button11_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        SLSetDBDelete sLSetDBDelete = new(canUseVars);
        if (sLSetDBDelete.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetDBDelete.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private void button14_Click(object sender, EventArgs e)
    {
        SLSetLabel sLSetLabel = new();
        if (sLSetLabel.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetLabel.Result;
            ListViewItem listViewItem = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem);
        }
    }

    private void button15_Click(object sender, EventArgs e)
    {
        List<string> canUseVars = GetCanUseVars();
        List<string> list = new();
        foreach (ListViewItem item in listView1.Items)
        {
            if (((ServerLogicItem)item.Tag).LogicType == "定义标签")
            {
                list.Add(((ServerLogicItem)item.Tag).DataDict["Name"].ToString());
            }
        }
        SLSetGoto sLSetGoto = new(list, canUseVars);
        if (sLSetGoto.ShowDialog() == DialogResult.OK)
        {
            ServerLogicItem serverLogicItem = sLSetGoto.Result;
            ListViewItem listViewItem2 = new()
            {
                Text = serverLogicItem.LogicType,
                Tag = serverLogicItem
            };
            listViewItem2.SubItems.Add(serverLogicItem.GetDesc());
            listViewItem2.SubItems.Add(serverLogicItem.ConditionalExpression);
            listView1.Items.Add(listViewItem2);
        }
    }

    private void button21_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent == "")
        {
            textBox2.Text = "true";
        }
        else
        {
            textBox2.Text = "[" + varTableEvent + "]";
        }
    }

    private void listView1_SizeChanged(object sender, EventArgs e)
    {
        int num = listView1.Width;
        listView1.Columns[0].Width = 78;
        listView1.Columns[2].Width = 150;
        if (num - 78 - 150 > 0)
        {
            listView1.Columns[1].Width = num - 78 - 150;
        }
    }

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.button19 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.listBox2 = new System.Windows.Forms.ListBox();
        this.button3 = new System.Windows.Forms.Button();
        this.button20 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.listView1 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.button7 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.button10 = new System.Windows.Forms.Button();
        this.button11 = new System.Windows.Forms.Button();
        this.button12 = new System.Windows.Forms.Button();
        this.button13 = new System.Windows.Forms.Button();
        this.button14 = new System.Windows.Forms.Button();
        this.button15 = new System.Windows.Forms.Button();
        this.button16 = new System.Windows.Forms.Button();
        this.button17 = new System.Windows.Forms.Button();
        this.button18 = new System.Windows.Forms.Button();
        this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.button21 = new System.Windows.Forms.Button();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.tableLayoutPanel1.SuspendLayout();
        base.SuspendLayout();
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button1.Location = new System.Drawing.Point(167, 20);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "添加";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.Location = new System.Drawing.Point(167, 49);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 1;
        this.button2.Text = "删除";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.groupBox1.Controls.Add(this.listBox1);
        this.groupBox1.Controls.Add(this.button1);
        this.groupBox1.Controls.Add(this.button19);
        this.groupBox1.Controls.Add(this.button5);
        this.groupBox1.Controls.Add(this.button2);
        this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBox1.Location = new System.Drawing.Point(3, 3);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(248, 215);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "输入变量";
        this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.HorizontalScrollbar = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new System.Drawing.Point(6, 20);
        this.listBox1.Name = "listBox1";
        this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBox1.Size = new System.Drawing.Size(155, 184);
        this.listBox1.TabIndex = 4;
        this.button19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button19.Location = new System.Drawing.Point(167, 78);
        this.button19.Name = "button19";
        this.button19.Size = new System.Drawing.Size(75, 23);
        this.button19.TabIndex = 2;
        this.button19.Text = "反选";
        this.button19.UseVisualStyleBackColor = true;
        this.button19.Click += new System.EventHandler(button19_Click);
        this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button5.Location = new System.Drawing.Point(167, 107);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 3;
        this.button5.Text = "清空";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.groupBox2.Controls.Add(this.listBox2);
        this.groupBox2.Controls.Add(this.button3);
        this.groupBox2.Controls.Add(this.button20);
        this.groupBox2.Controls.Add(this.button6);
        this.groupBox2.Controls.Add(this.button4);
        this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBox2.Location = new System.Drawing.Point(3, 224);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(248, 216);
        this.groupBox2.TabIndex = 1;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "输出变量";
        this.listBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox2.FormattingEnabled = true;
        this.listBox2.HorizontalScrollbar = true;
        this.listBox2.ItemHeight = 12;
        this.listBox2.Location = new System.Drawing.Point(6, 20);
        this.listBox2.Name = "listBox2";
        this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBox2.Size = new System.Drawing.Size(155, 160);
        this.listBox2.TabIndex = 4;
        this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button3.Location = new System.Drawing.Point(167, 20);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 0;
        this.button3.Text = "添加";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button20.Location = new System.Drawing.Point(167, 78);
        this.button20.Name = "button20";
        this.button20.Size = new System.Drawing.Size(75, 23);
        this.button20.TabIndex = 2;
        this.button20.Text = "反选";
        this.button20.UseVisualStyleBackColor = true;
        this.button20.Click += new System.EventHandler(button20_Click);
        this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button6.Location = new System.Drawing.Point(167, 107);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(75, 23);
        this.button6.TabIndex = 3;
        this.button6.Text = "清空";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button4.Location = new System.Drawing.Point(167, 49);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 1;
        this.button4.Text = "删除";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
        this.listView1.FullRowSelect = true;
        this.listView1.GridLines = true;
        this.listView1.HideSelection = false;
        this.listView1.Location = new System.Drawing.Point(272, 68);
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(456, 382);
        this.listView1.TabIndex = 16;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = System.Windows.Forms.View.Details;
        this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(listView1_MouseDoubleClick);
        this.listView1.SizeChanged += new System.EventHandler(listView1_SizeChanged);
        this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(listView1_KeyDown);
        this.columnHeader1.Text = "操作类型";
        this.columnHeader1.Width = 78;
        this.columnHeader2.DisplayIndex = 2;
        this.columnHeader2.Text = "操作表达式";
        this.columnHeader2.Width = 224;
        this.columnHeader3.DisplayIndex = 1;
        this.columnHeader3.Text = "条件";
        this.columnHeader3.Width = 149;
        this.button7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button7.Location = new System.Drawing.Point(739, 68);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(75, 23);
        this.button7.TabIndex = 4;
        this.button7.Text = "变量赋值";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(button7_Click);
        this.button8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button8.Location = new System.Drawing.Point(739, 97);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 23);
        this.button8.TabIndex = 5;
        this.button8.Text = "查询数据";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(button8_Click);
        this.button9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button9.Location = new System.Drawing.Point(739, 126);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(75, 23);
        this.button9.TabIndex = 6;
        this.button9.Text = "添加数据";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        this.button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button10.Location = new System.Drawing.Point(739, 155);
        this.button10.Name = "button10";
        this.button10.Size = new System.Drawing.Size(75, 23);
        this.button10.TabIndex = 7;
        this.button10.Text = "更新数据";
        this.button10.UseVisualStyleBackColor = true;
        this.button10.Click += new System.EventHandler(button10_Click);
        this.button11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button11.Location = new System.Drawing.Point(739, 184);
        this.button11.Name = "button11";
        this.button11.Size = new System.Drawing.Size(75, 23);
        this.button11.TabIndex = 8;
        this.button11.Text = "删除数据";
        this.button11.UseVisualStyleBackColor = true;
        this.button11.Click += new System.EventHandler(button11_Click);
        this.button12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button12.Location = new System.Drawing.Point(739, 400);
        this.button12.Name = "button12";
        this.button12.Size = new System.Drawing.Size(75, 23);
        this.button12.TabIndex = 14;
        this.button12.Text = "保存";
        this.button12.UseVisualStyleBackColor = true;
        this.button12.Click += new System.EventHandler(button12_Click);
        this.button13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button13.Location = new System.Drawing.Point(739, 429);
        this.button13.Name = "button13";
        this.button13.Size = new System.Drawing.Size(75, 23);
        this.button13.TabIndex = 15;
        this.button13.Text = "关闭";
        this.button13.UseVisualStyleBackColor = true;
        this.button13.Click += new System.EventHandler(button13_Click);
        this.button14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button14.Location = new System.Drawing.Point(739, 213);
        this.button14.Name = "button14";
        this.button14.Size = new System.Drawing.Size(75, 23);
        this.button14.TabIndex = 9;
        this.button14.Text = "定义标签";
        this.button14.UseVisualStyleBackColor = true;
        this.button14.Click += new System.EventHandler(button14_Click);
        this.button15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button15.Location = new System.Drawing.Point(739, 242);
        this.button15.Name = "button15";
        this.button15.Size = new System.Drawing.Size(75, 23);
        this.button15.TabIndex = 10;
        this.button15.Text = "跳转标签";
        this.button15.UseVisualStyleBackColor = true;
        this.button15.Click += new System.EventHandler(button15_Click);
        this.button16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button16.Location = new System.Drawing.Point(739, 271);
        this.button16.Name = "button16";
        this.button16.Size = new System.Drawing.Size(75, 23);
        this.button16.TabIndex = 11;
        this.button16.Text = "↑";
        this.button16.UseVisualStyleBackColor = true;
        this.button16.Click += new System.EventHandler(button16_Click);
        this.button17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button17.Location = new System.Drawing.Point(739, 300);
        this.button17.Name = "button17";
        this.button17.Size = new System.Drawing.Size(75, 23);
        this.button17.TabIndex = 12;
        this.button17.Text = "↓";
        this.button17.UseVisualStyleBackColor = true;
        this.button17.Click += new System.EventHandler(button17_Click);
        this.button18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button18.Location = new System.Drawing.Point(739, 329);
        this.button18.Name = "button18";
        this.button18.Size = new System.Drawing.Size(75, 23);
        this.button18.TabIndex = 13;
        this.button18.Text = "删除";
        this.button18.UseVisualStyleBackColor = true;
        this.button18.Click += new System.EventHandler(button18_Click);
        this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.tableLayoutPanel1.ColumnCount = 1;
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20f));
        this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
        this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
        this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 10);
        this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount = 2;
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
        this.tableLayoutPanel1.Size = new System.Drawing.Size(254, 443);
        this.tableLayoutPanel1.TabIndex = 17;
        this.textBox1.Location = new System.Drawing.Point(337, 39);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(392, 21);
        this.textBox1.TabIndex = 2;
        this.textBox1.Text = "服务器逻辑";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(272, 41);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 9;
        this.label1.Text = "备注信息:";
        this.textBox2.Location = new System.Drawing.Point(337, 13);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(440, 21);
        this.textBox2.TabIndex = 0;
        this.textBox2.Text = "true";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(272, 15);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(59, 12);
        this.label2.TabIndex = 9;
        this.label2.Text = "执行条件:";
        this.button21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button21.Location = new System.Drawing.Point(783, 12);
        this.button21.Name = "button21";
        this.button21.Size = new System.Drawing.Size(30, 23);
        this.button21.TabIndex = 1;
        this.button21.Text = "...";
        this.button21.UseVisualStyleBackColor = true;
        this.button21.Click += new System.EventHandler(button21_Click);
        this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(740, 42);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(72, 16);
        this.checkBox1.TabIndex = 3;
        this.checkBox1.Text = "异步操作";
        this.checkBox1.UseVisualStyleBackColor = true;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(825, 458);
        base.Controls.Add(this.checkBox1);
        base.Controls.Add(this.button21);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.tableLayoutPanel1);
        base.Controls.Add(this.button13);
        base.Controls.Add(this.button12);
        base.Controls.Add(this.button11);
        base.Controls.Add(this.button10);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button8);
        base.Controls.Add(this.button18);
        base.Controls.Add(this.button17);
        base.Controls.Add(this.button16);
        base.Controls.Add(this.button15);
        base.Controls.Add(this.button7);
        base.Controls.Add(this.listView1);
        base.Controls.Add(this.button14);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventSetServerLogic";
        base.ShowIcon = false;
        this.Text = "服务器逻辑";
        base.Load += new System.EventHandler(EventSetServerLogic_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.tableLayoutPanel1.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
