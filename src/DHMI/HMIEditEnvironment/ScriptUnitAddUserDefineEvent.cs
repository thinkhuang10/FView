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
        this.label1 = new System.Windows.Forms.Label();
        this.ctrlLst = new System.Windows.Forms.ListView();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.label2 = new System.Windows.Forms.Label();
        this.eventLst = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.addEventBtn = new System.Windows.Forms.Button();
        this.cancleBtn = new System.Windows.Forms.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        this.label1.Location = new System.Drawing.Point(8, 14);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(72, 14);
        this.label1.TabIndex = 0;
        this.label1.Text = "控件名称：";
        this.ctrlLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader2 });
        this.ctrlLst.Cursor = System.Windows.Forms.Cursors.Arrow;
        this.ctrlLst.FullRowSelect = true;
        this.ctrlLst.GridLines = true;
        this.ctrlLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        this.ctrlLst.HideSelection = false;
        this.ctrlLst.Location = new System.Drawing.Point(11, 44);
        this.ctrlLst.MultiSelect = false;
        this.ctrlLst.Name = "ctrlLst";
        this.ctrlLst.Size = new System.Drawing.Size(236, 321);
        this.ctrlLst.TabIndex = 1;
        this.ctrlLst.UseCompatibleStateImageBehavior = false;
        this.ctrlLst.View = System.Windows.Forms.View.Details;
        this.ctrlLst.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(ctrlLst_ItemSelectionChanged);
        this.ctrlLst.MouseUp += new System.Windows.Forms.MouseEventHandler(ctrlLst_MouseUp);
        this.columnHeader2.Width = 228;
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        this.label2.Location = new System.Drawing.Point(278, 14);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(72, 14);
        this.label2.TabIndex = 2;
        this.label2.Text = "事件名称：";
        this.eventLst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.columnHeader1 });
        this.eventLst.FullRowSelect = true;
        this.eventLst.GridLines = true;
        this.eventLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        this.eventLst.HideSelection = false;
        this.eventLst.Location = new System.Drawing.Point(281, 44);
        this.eventLst.MultiSelect = false;
        this.eventLst.Name = "eventLst";
        this.eventLst.Size = new System.Drawing.Size(236, 321);
        this.eventLst.TabIndex = 3;
        this.eventLst.UseCompatibleStateImageBehavior = false;
        this.eventLst.View = System.Windows.Forms.View.Details;
        this.eventLst.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(eventLst_ItemSelectionChanged);
        this.eventLst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(eventLst_MouseDoubleClick);
        this.eventLst.MouseUp += new System.Windows.Forms.MouseEventHandler(eventLst_MouseUp);
        this.columnHeader1.Width = 228;
        this.addEventBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.addEventBtn.Font = new System.Drawing.Font("Tahoma", 9f);
        this.addEventBtn.Location = new System.Drawing.Point(361, 417);
        this.addEventBtn.Name = "addEventBtn";
        this.addEventBtn.Size = new System.Drawing.Size(75, 23);
        this.addEventBtn.TabIndex = 4;
        this.addEventBtn.Text = "添加";
        this.addEventBtn.UseVisualStyleBackColor = true;
        this.addEventBtn.Click += new System.EventHandler(addEventBtn_Click);
        this.cancleBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.cancleBtn.Font = new System.Drawing.Font("Tahoma", 9f);
        this.cancleBtn.Location = new System.Drawing.Point(442, 417);
        this.cancleBtn.Name = "cancleBtn";
        this.cancleBtn.Size = new System.Drawing.Size(75, 23);
        this.cancleBtn.TabIndex = 5;
        this.cancleBtn.Text = "取消";
        this.cancleBtn.UseVisualStyleBackColor = true;
        this.cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
        this.label3.Location = new System.Drawing.Point(8, 381);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(75, 14);
        this.label3.TabIndex = 6;
        this.label3.Text = "EventName";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(8, 397);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(70, 14);
        this.label4.TabIndex = 7;
        this.label4.Text = "EventSignal";
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(529, 452);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.cancleBtn);
        base.Controls.Add(this.addEventBtn);
        base.Controls.Add(this.eventLst);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.ctrlLst);
        base.Controls.Add(this.label1);
        base.MaximizeBox = false;
        base.Name = "ScriptUnitAddUserDefineEvent";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "添加用户事件脚本";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitAddUserDefineEvent_FormClosing);
        base.Load += new System.EventHandler(ScriptUnitAddUserDefineEvent_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
