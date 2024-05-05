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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        listBox1 = new System.Windows.Forms.ListBox();
        button19 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        listBox2 = new System.Windows.Forms.ListBox();
        button3 = new System.Windows.Forms.Button();
        button20 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        listView1 = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        columnHeader2 = new System.Windows.Forms.ColumnHeader();
        columnHeader3 = new System.Windows.Forms.ColumnHeader();
        button7 = new System.Windows.Forms.Button();
        button8 = new System.Windows.Forms.Button();
        button9 = new System.Windows.Forms.Button();
        button10 = new System.Windows.Forms.Button();
        button11 = new System.Windows.Forms.Button();
        button12 = new System.Windows.Forms.Button();
        button13 = new System.Windows.Forms.Button();
        button14 = new System.Windows.Forms.Button();
        button15 = new System.Windows.Forms.Button();
        button16 = new System.Windows.Forms.Button();
        button17 = new System.Windows.Forms.Button();
        button18 = new System.Windows.Forms.Button();
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        button21 = new System.Windows.Forms.Button();
        checkBox1 = new System.Windows.Forms.CheckBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        base.SuspendLayout();
        button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button1.Location = new System.Drawing.Point(167, 20);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "添加";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button2.Location = new System.Drawing.Point(167, 49);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 1;
        button2.Text = "删除";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        groupBox1.Controls.Add(listBox1);
        groupBox1.Controls.Add(button1);
        groupBox1.Controls.Add(button19);
        groupBox1.Controls.Add(button5);
        groupBox1.Controls.Add(button2);
        groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox1.Location = new System.Drawing.Point(3, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(248, 215);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "输入变量";
        listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listBox1.FormattingEnabled = true;
        listBox1.HorizontalScrollbar = true;
        listBox1.ItemHeight = 12;
        listBox1.Location = new System.Drawing.Point(6, 20);
        listBox1.Name = "listBox1";
        listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        listBox1.Size = new System.Drawing.Size(155, 184);
        listBox1.TabIndex = 4;
        button19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button19.Location = new System.Drawing.Point(167, 78);
        button19.Name = "button19";
        button19.Size = new System.Drawing.Size(75, 23);
        button19.TabIndex = 2;
        button19.Text = "反选";
        button19.UseVisualStyleBackColor = true;
        button19.Click += new System.EventHandler(button19_Click);
        button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button5.Location = new System.Drawing.Point(167, 107);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(75, 23);
        button5.TabIndex = 3;
        button5.Text = "清空";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        groupBox2.Controls.Add(listBox2);
        groupBox2.Controls.Add(button3);
        groupBox2.Controls.Add(button20);
        groupBox2.Controls.Add(button6);
        groupBox2.Controls.Add(button4);
        groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox2.Location = new System.Drawing.Point(3, 224);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(248, 216);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "输出变量";
        listBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listBox2.FormattingEnabled = true;
        listBox2.HorizontalScrollbar = true;
        listBox2.ItemHeight = 12;
        listBox2.Location = new System.Drawing.Point(6, 20);
        listBox2.Name = "listBox2";
        listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        listBox2.Size = new System.Drawing.Size(155, 160);
        listBox2.TabIndex = 4;
        button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button3.Location = new System.Drawing.Point(167, 20);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 0;
        button3.Text = "添加";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button20.Location = new System.Drawing.Point(167, 78);
        button20.Name = "button20";
        button20.Size = new System.Drawing.Size(75, 23);
        button20.TabIndex = 2;
        button20.Text = "反选";
        button20.UseVisualStyleBackColor = true;
        button20.Click += new System.EventHandler(button20_Click);
        button6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button6.Location = new System.Drawing.Point(167, 107);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(75, 23);
        button6.TabIndex = 3;
        button6.Text = "清空";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(button6_Click);
        button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button4.Location = new System.Drawing.Point(167, 49);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 1;
        button4.Text = "删除";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { columnHeader1, columnHeader2, columnHeader3 });
        listView1.FullRowSelect = true;
        listView1.GridLines = true;
        listView1.HideSelection = false;
        listView1.Location = new System.Drawing.Point(272, 68);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(456, 382);
        listView1.TabIndex = 16;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(listView1_MouseDoubleClick);
        listView1.SizeChanged += new System.EventHandler(listView1_SizeChanged);
        listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(listView1_KeyDown);
        columnHeader1.Text = "操作类型";
        columnHeader1.Width = 78;
        columnHeader2.DisplayIndex = 2;
        columnHeader2.Text = "操作表达式";
        columnHeader2.Width = 224;
        columnHeader3.DisplayIndex = 1;
        columnHeader3.Text = "条件";
        columnHeader3.Width = 149;
        button7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button7.Location = new System.Drawing.Point(739, 68);
        button7.Name = "button7";
        button7.Size = new System.Drawing.Size(75, 23);
        button7.TabIndex = 4;
        button7.Text = "变量赋值";
        button7.UseVisualStyleBackColor = true;
        button7.Click += new System.EventHandler(button7_Click);
        button8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button8.Location = new System.Drawing.Point(739, 97);
        button8.Name = "button8";
        button8.Size = new System.Drawing.Size(75, 23);
        button8.TabIndex = 5;
        button8.Text = "查询数据";
        button8.UseVisualStyleBackColor = true;
        button8.Click += new System.EventHandler(button8_Click);
        button9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button9.Location = new System.Drawing.Point(739, 126);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(75, 23);
        button9.TabIndex = 6;
        button9.Text = "添加数据";
        button9.UseVisualStyleBackColor = true;
        button9.Click += new System.EventHandler(button9_Click);
        button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button10.Location = new System.Drawing.Point(739, 155);
        button10.Name = "button10";
        button10.Size = new System.Drawing.Size(75, 23);
        button10.TabIndex = 7;
        button10.Text = "更新数据";
        button10.UseVisualStyleBackColor = true;
        button10.Click += new System.EventHandler(button10_Click);
        button11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button11.Location = new System.Drawing.Point(739, 184);
        button11.Name = "button11";
        button11.Size = new System.Drawing.Size(75, 23);
        button11.TabIndex = 8;
        button11.Text = "删除数据";
        button11.UseVisualStyleBackColor = true;
        button11.Click += new System.EventHandler(button11_Click);
        button12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button12.Location = new System.Drawing.Point(739, 400);
        button12.Name = "button12";
        button12.Size = new System.Drawing.Size(75, 23);
        button12.TabIndex = 14;
        button12.Text = "保存";
        button12.UseVisualStyleBackColor = true;
        button12.Click += new System.EventHandler(button12_Click);
        button13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button13.Location = new System.Drawing.Point(739, 429);
        button13.Name = "button13";
        button13.Size = new System.Drawing.Size(75, 23);
        button13.TabIndex = 15;
        button13.Text = "关闭";
        button13.UseVisualStyleBackColor = true;
        button13.Click += new System.EventHandler(button13_Click);
        button14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button14.Location = new System.Drawing.Point(739, 213);
        button14.Name = "button14";
        button14.Size = new System.Drawing.Size(75, 23);
        button14.TabIndex = 9;
        button14.Text = "定义标签";
        button14.UseVisualStyleBackColor = true;
        button14.Click += new System.EventHandler(button14_Click);
        button15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button15.Location = new System.Drawing.Point(739, 242);
        button15.Name = "button15";
        button15.Size = new System.Drawing.Size(75, 23);
        button15.TabIndex = 10;
        button15.Text = "跳转标签";
        button15.UseVisualStyleBackColor = true;
        button15.Click += new System.EventHandler(button15_Click);
        button16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button16.Location = new System.Drawing.Point(739, 271);
        button16.Name = "button16";
        button16.Size = new System.Drawing.Size(75, 23);
        button16.TabIndex = 11;
        button16.Text = "↑";
        button16.UseVisualStyleBackColor = true;
        button16.Click += new System.EventHandler(button16_Click);
        button17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button17.Location = new System.Drawing.Point(739, 300);
        button17.Name = "button17";
        button17.Size = new System.Drawing.Size(75, 23);
        button17.TabIndex = 12;
        button17.Text = "↓";
        button17.UseVisualStyleBackColor = true;
        button17.Click += new System.EventHandler(button17_Click);
        button18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button18.Location = new System.Drawing.Point(739, 329);
        button18.Name = "button18";
        button18.Size = new System.Drawing.Size(75, 23);
        button18.TabIndex = 13;
        button18.Text = "删除";
        button18.UseVisualStyleBackColor = true;
        button18.Click += new System.EventHandler(button18_Click);
        tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20f));
        tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
        tableLayoutPanel1.Location = new System.Drawing.Point(12, 10);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
        tableLayoutPanel1.Size = new System.Drawing.Size(254, 443);
        tableLayoutPanel1.TabIndex = 17;
        textBox1.Location = new System.Drawing.Point(337, 39);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(392, 21);
        textBox1.TabIndex = 2;
        textBox1.Text = "服务器逻辑";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(272, 41);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 9;
        label1.Text = "备注信息:";
        textBox2.Location = new System.Drawing.Point(337, 13);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(440, 21);
        textBox2.TabIndex = 0;
        textBox2.Text = "true";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(272, 15);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(59, 12);
        label2.TabIndex = 9;
        label2.Text = "执行条件:";
        button21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button21.Location = new System.Drawing.Point(783, 12);
        button21.Name = "button21";
        button21.Size = new System.Drawing.Size(30, 23);
        button21.TabIndex = 1;
        button21.Text = "...";
        button21.UseVisualStyleBackColor = true;
        button21.Click += new System.EventHandler(button21_Click);
        checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(740, 42);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(72, 16);
        checkBox1.TabIndex = 3;
        checkBox1.Text = "异步操作";
        checkBox1.UseVisualStyleBackColor = true;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(825, 458);
        base.Controls.Add(checkBox1);
        base.Controls.Add(button21);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(textBox2);
        base.Controls.Add(textBox1);
        base.Controls.Add(tableLayoutPanel1);
        base.Controls.Add(button13);
        base.Controls.Add(button12);
        base.Controls.Add(button11);
        base.Controls.Add(button10);
        base.Controls.Add(button9);
        base.Controls.Add(button8);
        base.Controls.Add(button18);
        base.Controls.Add(button17);
        base.Controls.Add(button16);
        base.Controls.Add(button15);
        base.Controls.Add(button7);
        base.Controls.Add(listView1);
        base.Controls.Add(button14);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventSetServerLogic";
        base.ShowIcon = false;
        Text = "服务器逻辑";
        base.Load += new System.EventHandler(EventSetServerLogic_Load);
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
