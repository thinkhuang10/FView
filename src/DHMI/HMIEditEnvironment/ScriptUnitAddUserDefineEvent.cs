using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ScriptUnitAddUserDefineEvent : XtraForm
{
    private readonly Dictionary<string, List<TreeNode>> CtrlEventNodeDic = new();

    public Dictionary<string, List<string>> ExistedCtrlEventDic = new();

    public string CtrlName;

    public string EventName;

    public TreeNode EventNode;

    private Label label1;

    private ListView ctrlLst;

    private Label label2;

    private ListView eventLst;

    private Button addEventBtn;

    private Button cancleBtn;

    private ColumnHeader columnHeader2;

    private ColumnHeader columnHeader1;

    private Label label3;

    private Label label4;

    public ScriptUnitAddUserDefineEvent()
    {
        InitializeComponent();
        Init();
        InitExistedCtrlEventDic();
    }

    private int ShapeNameCompare(CShape x, CShape y)
    {
        return string.Compare(x.Name, y.Name);
    }

    private int PageNameCompare(DataFile x, DataFile y)
    {
        return string.Compare(x.pageName, y.pageName);
    }

    private void ScriptUnitAddUserDefineEvent_Load(object sender, EventArgs e)
    {
        label3.Text = string.Empty;
        label4.Text = string.Empty;
        foreach (KeyValuePair<string, List<TreeNode>> item in CtrlEventNodeDic)
        {
            ListViewItem listViewItem = ctrlLst.Items.Add(item.Key);
            if (ExistedCtrlEventDic.ContainsKey(item.Key))
            {
                listViewItem.ForeColor = Color.Gray;
            }
        }
        ctrlLst.Select();
        if (ctrlLst.Items.Count > 0)
        {
            if (ScriptUnit.ShapeCtrl != null)
            {
                foreach (ListViewItem item2 in ctrlLst.Items)
                {
                    if (item2.Text == ScriptUnit.ShapeCtrl.Name)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }
            else
            {
                ctrlLst.Items[0].Selected = true;
            }
        }
        addEventBtn.Enabled = false;
    }

    private void Init()
    {
        CtrlEventNodeDic.Clear();
        ctrlLst.Items.Clear();
        eventLst.Items.Clear();
        List<DataFile> list = new(CEditEnvironmentGlobal.dfs);
        list.Sort(PageNameCompare);
        foreach (DataFile item in list)
        {
            List<CShape> list2 = new();
            if (item.pageName == ScriptUnit.ClickNode.Text)
            {
                list2 = item.ListAllShowCShape;
            }
            else
            {
                if (!(ScriptUnit.ClickNode.Parent.Text == item.pageName))
                {
                    continue;
                }
                foreach (CShape item2 in item.ListAllShowCShape)
                {
                    if (item2.Name == ScriptUnit.ClickNode.Text)
                    {
                        list2.Add(item2);
                    }
                }
            }
            list2.Sort(ShapeNameCompare);
            foreach (CShape item3 in list2)
            {
                List<TreeNode> list3 = new();
                if (item3 is not CControl)
                {
                    TreeNode treeNode = new("鼠标左键按下")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[1], item3)
                    };
                    ((TempTag)treeNode.Tag).WhenOutThenDo = new ObjectBoolean(item3.sbsjWhenOutThenDo);
                    object whenOutThenDo = ((TempTag)treeNode.Tag).WhenOutThenDo;
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标左键抬起")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[6], item3)
                    };
                    ((TempTag)treeNode.Tag).WhenOutThenDo = whenOutThenDo;
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标左键双击")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[2], item3)
                    };
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标右键按下")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[3], item3)
                    };
                    ((TempTag)treeNode.Tag).WhenOutThenDo = whenOutThenDo;
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标右键抬起")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[7], item3)
                    };
                    ((TempTag)treeNode.Tag).WhenOutThenDo = whenOutThenDo;
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标右键双击")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[4], item3)
                    };
                    list3.Add(treeNode);
                    treeNode = new TreeNode("鼠标滚轮滚动")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.UserLogic[5], item3)
                    };
                    list3.Add(treeNode);
                    treeNode = new TreeNode("数据库操作成功")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.DBOKScriptUser, item3)
                    };
                    list3.Add(treeNode);
                    treeNode = new TreeNode("数据库操作失败")
                    {
                        ImageKey = "l.png",
                        Tag = new TempTag(null, item3.DBErrScriptUser, item3)
                    };
                    list3.Add(treeNode);
                }
                else
                {
                    Type type = ((CControl)item3)._c.GetType();
                    EventInfo[] array = type.GetEvents();
                    for (int i = 0; i < array.Length; i++)
                    {
                        TreeNode treeNode = new(array[i].Name.ToString())
                        {
                            ImageKey = "l.png",
                            Tag = new TempTag(null, item3.ysdzshijianbiaodashi[i], item3)
                        };
                        list3.Add(treeNode);
                    }
                }
                CtrlEventNodeDic.Add(item3.Name, list3);
            }
        }
    }

    private void InitExistedCtrlEventDic()
    {
        ExistedCtrlEventDic.Clear();
        TreeNode treeNode = new();
        treeNode = ((!(ScriptUnit.ClickNode.Parent.Parent.Text == "页面相关")) ? ScriptUnit.ClickNode : ScriptUnit.ClickNode.Parent);
        foreach (TreeNode node in treeNode.Nodes)
        {
            List<string> list = new();
            foreach (TreeNode node2 in node.Nodes)
            {
                list.Add(node2.Text);
            }
            ExistedCtrlEventDic.Add(node.Text, list);
        }
    }

    private void ctrlLst_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        eventLst.Items.Clear();
        if (e.IsSelected)
        {
            foreach (TreeNode item in CtrlEventNodeDic[ctrlLst.SelectedItems[0].Text])
            {
                ListViewItem listViewItem = eventLst.Items.Add(item.Text);
                if (ExistedCtrlEventDic.ContainsKey(ctrlLst.SelectedItems[0].Text) && ExistedCtrlEventDic[ctrlLst.SelectedItems[0].Text].Contains(item.Text))
                {
                    listViewItem.ForeColor = Color.Gray;
                }
            }
        }
        addEventBtn.Enabled = false;
        label3.Text = string.Empty;
        label4.Text = string.Empty;
    }

    private void ctrlLst_MouseUp(object sender, MouseEventArgs e)
    {
        if (ctrlLst.FocusedItem != null)
        {
            ListViewItem itemAt = ctrlLst.GetItemAt(e.X, e.Y);
            if (itemAt == null)
            {
                ctrlLst.FocusedItem.Selected = true;
            }
        }
        else if (ctrlLst.Items.Count > 0)
        {
            ctrlLst.Items[0].Selected = true;
        }
        else
        {
            MessageBox.Show("请在页面上添加控件!");
        }
    }

    private void eventLst_MouseUp(object sender, MouseEventArgs e)
    {
        if (eventLst.SelectedItems.Count <= 0 && eventLst.FocusedItem != null)
        {
            ListViewItem itemAt = eventLst.GetItemAt(e.X, e.Y);
            if (itemAt == null)
            {
                eventLst.FocusedItem.Selected = true;
            }
        }
    }

    private void addEventBtn_Click(object sender, EventArgs e)
    {
        CtrlName = ctrlLst.SelectedItems[0].Text;
        EventName = eventLst.SelectedItems[0].Text;
        if (string.IsNullOrEmpty(CtrlName) || string.IsNullOrEmpty(EventName))
        {
            return;
        }
        foreach (TreeNode item in CtrlEventNodeDic[CtrlName])
        {
            if (item.Text == EventName)
            {
                EventNode = item;
                ((TempTag)EventNode.Tag).value = string.Empty;
                break;
            }
        }
        if (!ExistedCtrlEventDic.ContainsKey(CtrlName) || !ExistedCtrlEventDic[CtrlName].Contains(EventName))
        {
            base.DialogResult = DialogResult.OK;
        }
    }

    private void cancleBtn_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
    }

    private void ScriptUnitAddUserDefineEvent_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (base.DialogResult != DialogResult.Cancel && base.DialogResult != DialogResult.OK)
        {
            e.Cancel = true;
        }
    }

    private void eventLst_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        ListViewItem itemAt = eventLst.GetItemAt(e.X, e.Y);
        if (itemAt != null)
        {
            addEventBtn_Click(sender, e);
        }
    }

    private void eventLst_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (e.IsSelected)
        {
            if (e.Item.ForeColor == Color.Gray)
            {
                addEventBtn.Enabled = false;
            }
            else
            {
                addEventBtn.Enabled = true;
            }
        }
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        ctrlLst = new System.Windows.Forms.ListView();
        columnHeader2 = new System.Windows.Forms.ColumnHeader();
        label2 = new System.Windows.Forms.Label();
        eventLst = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        addEventBtn = new System.Windows.Forms.Button();
        cancleBtn = new System.Windows.Forms.Button();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        label1.Location = new System.Drawing.Point(8, 14);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(72, 14);
        label1.TabIndex = 0;
        label1.Text = "控件名称：";
        ctrlLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader2 });
        ctrlLst.Cursor = System.Windows.Forms.Cursors.Arrow;
        ctrlLst.FullRowSelect = true;
        ctrlLst.GridLines = true;
        ctrlLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        ctrlLst.HideSelection = false;
        ctrlLst.Location = new System.Drawing.Point(11, 44);
        ctrlLst.MultiSelect = false;
        ctrlLst.Name = "ctrlLst";
        ctrlLst.Size = new System.Drawing.Size(236, 321);
        ctrlLst.TabIndex = 1;
        ctrlLst.UseCompatibleStateImageBehavior = false;
        ctrlLst.View = System.Windows.Forms.View.Details;
        ctrlLst.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(ctrlLst_ItemSelectionChanged);
        ctrlLst.MouseUp += new System.Windows.Forms.MouseEventHandler(ctrlLst_MouseUp);
        columnHeader2.Width = 228;
        label2.AutoSize = true;
        label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        label2.Location = new System.Drawing.Point(278, 14);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(72, 14);
        label2.TabIndex = 2;
        label2.Text = "事件名称：";
        eventLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { columnHeader1 });
        eventLst.FullRowSelect = true;
        eventLst.GridLines = true;
        eventLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        eventLst.HideSelection = false;
        eventLst.Location = new System.Drawing.Point(281, 44);
        eventLst.MultiSelect = false;
        eventLst.Name = "eventLst";
        eventLst.Size = new System.Drawing.Size(236, 321);
        eventLst.TabIndex = 3;
        eventLst.UseCompatibleStateImageBehavior = false;
        eventLst.View = System.Windows.Forms.View.Details;
        eventLst.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(eventLst_ItemSelectionChanged);
        eventLst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(eventLst_MouseDoubleClick);
        eventLst.MouseUp += new System.Windows.Forms.MouseEventHandler(eventLst_MouseUp);
        columnHeader1.Width = 228;
        addEventBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
        addEventBtn.Font = new System.Drawing.Font("Tahoma", 9f);
        addEventBtn.Location = new System.Drawing.Point(361, 417);
        addEventBtn.Name = "addEventBtn";
        addEventBtn.Size = new System.Drawing.Size(75, 23);
        addEventBtn.TabIndex = 4;
        addEventBtn.Text = "添加";
        addEventBtn.UseVisualStyleBackColor = true;
        addEventBtn.Click += new System.EventHandler(addEventBtn_Click);
        cancleBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
        cancleBtn.Font = new System.Drawing.Font("Tahoma", 9f);
        cancleBtn.Location = new System.Drawing.Point(442, 417);
        cancleBtn.Name = "cancleBtn";
        cancleBtn.Size = new System.Drawing.Size(75, 23);
        cancleBtn.TabIndex = 5;
        cancleBtn.Text = "取消";
        cancleBtn.UseVisualStyleBackColor = true;
        cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        label3.AutoSize = true;
        label3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        label3.Location = new System.Drawing.Point(8, 381);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(75, 14);
        label3.TabIndex = 6;
        label3.Text = "EventName";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(8, 397);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(70, 14);
        label4.TabIndex = 7;
        label4.Text = "EventSignal";
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(529, 452);
        base.Controls.Add(label4);
        base.Controls.Add(label3);
        base.Controls.Add(cancleBtn);
        base.Controls.Add(addEventBtn);
        base.Controls.Add(eventLst);
        base.Controls.Add(label2);
        base.Controls.Add(ctrlLst);
        base.Controls.Add(label1);
        base.MaximizeBox = false;
        base.Name = "ScriptUnitAddUserDefineEvent";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "添加用户事件脚本";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitAddUserDefineEvent_FormClosing);
        base.Load += new System.EventHandler(ScriptUnitAddUserDefineEvent_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
