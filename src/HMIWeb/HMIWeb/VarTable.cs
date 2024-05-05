using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ShapeRuntime;

namespace HMIWeb;

public class VarTable : Form
{
    private readonly DataTable projectiostable = new();

    private readonly TreeNode ProjectIOTreeRoot;

    private string type;

    private readonly XmlDocument doc;

    private readonly HMIProjectFile dhp;

    private List<XmlNode> xnitems = new();

    private TreeNode DeviceIORootNode = new();

    public string tagvalue;

    public string value;

    private IContainer components;

    private TreeView treeView1;

    private DataGridView dataGridView1;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 添加ToolStripMenuItem;

    public string VarTableType
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
            if (treeView1.SelectedNode != null)
            {
                renewProjectIOs(treeView1.SelectedNode.Text);
            }
        }
    }

    public void SetType(string _type)
    {
    }

    public VarTable(HMIProjectFile _hpf, XmlDocument _doc)
    {
        InitializeComponent();
        doc = _doc;
        dhp = _hpf;
        InitLoad();
    }

    public VarTable(HMIProjectFile _hpf, XmlDocument _doc, string _type)
    {
        InitializeComponent();
        doc = _doc;
        dhp = _hpf;
        type = _type;
        InitLoad();
    }

    public VarTable(DataTable _projectiostable, TreeNode _ProjectIOTreeRoot, XmlDocument _doc, string _type)
    {
        InitializeComponent();
        projectiostable.Columns.Add("名称");
        projectiostable.Columns.Add("标签");
        projectiostable.Columns.Add("备注");
        projectiostable.Columns.Add("类型");
        projectiostable.Columns.Add("访问");
        projectiostable.Columns.Add("仿真");
        projectiostable.Columns.Add("范围上限");
        projectiostable.Columns.Add("范围下限");
        projectiostable.Columns.Add("周期(ms)");
        projectiostable.Columns.Add("延时(ms)");
        projectiostable.Columns.Add("id");
        projectiostable.Columns.Add("组");
        projectiostable.Columns.Add("历史");
        doc = _doc;
        ProjectIOTreeRoot = _ProjectIOTreeRoot;
        projectiostable = _projectiostable;
        type = _type;
        InitLoad();
    }

    private void InitLoad()
    {
        if (ProjectIOTreeRoot != null)
        {
            TreeNode treeNode = (TreeNode)ProjectIOTreeRoot.Clone();
            treeNode.Name = "ProjectIO";
            treeView1.Nodes.Add(treeNode);
            TreeNode treeNode2 = treeView1.Nodes.Add("设备变量", "设备变量", "G");
            XmlNodeList xmlNodeList = doc.SelectNodes("/DocumentRoot/Group");
            List<XmlNode> list = new();
            foreach (XmlNode item in xmlNodeList)
            {
                if (item.Attributes["ParentID"].Value != "-1")
                {
                    list.Add(item);
                }
            }
            DeviceIORootNode = treeNode2.Nodes.Add(xmlNodeList[0].Attributes["id"].Value, xmlNodeList[0].Attributes["Name"].Value, "G");
            TreeNode startNode = treeNode2.Nodes[0];
            List<XmlNode> list2 = new();
            while (list.Count != 0)
            {
                foreach (XmlNode item2 in list)
                {
                    TreeNode treeNode3 = findNode(item2.Attributes["ParentID"].Value, startNode);
                    if (treeNode3 != null)
                    {
                        treeNode3.Nodes.Add(item2.Attributes["id"].Value, item2.Attributes["Name"].Value, "G");
                        list2.Add(item2);
                    }
                }
                foreach (XmlNode item3 in list2)
                {
                    list.Remove(item3);
                }
            }
        }
        else
        {
            TreeNode treeNode4 = (TreeNode)dhp.ProjectIOTreeRoot.Clone();
            treeNode4.Name = "ProjectIO";
            treeView1.Nodes.Add(treeNode4);
            TreeNode treeNode5 = treeView1.Nodes.Add("设备变量", "设备变量", "G");
            XmlNodeList xmlNodeList2 = doc.SelectNodes("/DocumentRoot/Group");
            List<XmlNode> list3 = new();
            foreach (XmlNode item4 in xmlNodeList2)
            {
                if (item4.Attributes["ParentID"].Value != "-1")
                {
                    list3.Add(item4);
                }
            }
            DeviceIORootNode = treeNode5.Nodes.Add(xmlNodeList2[0].Attributes["id"].Value, xmlNodeList2[0].Attributes["Name"].Value, "G");
            TreeNode startNode2 = treeNode5.Nodes[0];
            List<XmlNode> list4 = new();
            while (list3.Count != 0)
            {
                foreach (XmlNode item5 in list3)
                {
                    TreeNode treeNode6 = findNode(item5.Attributes["ParentID"].Value, startNode2);
                    if (treeNode6 != null)
                    {
                        treeNode6.Nodes.Add(item5.Attributes["id"].Value, item5.Attributes["Name"].Value, "G");
                        list4.Add(item5);
                    }
                }
                foreach (XmlNode item6 in list4)
                {
                    list3.Remove(item6);
                }
            }
        }
        XmlNodeList xmlNodeList3 = doc.SelectNodes("/DocumentRoot/Item");
        xnitems = new List<XmlNode>();
        for (int i = 0; i < xmlNodeList3.Count; i++)
        {
            for (int j = ((i - 1 >= 0) ? (Convert.ToInt32(xmlNodeList3[i - 1].Attributes["id"].Value) + 1) : 0); j < Convert.ToInt32(xmlNodeList3[i].Attributes["id"].Value); j++)
            {
                xnitems.Add(null);
            }
            xnitems.Add(xmlNodeList3[i]);
        }
    }

    public void PorjectIOPushToTree(TreeNode tn, List<ProjectIO> ProjectIOs)
    {
        foreach (TreeNode node in tn.Nodes)
        {
            PorjectIOPushToTree(node, ProjectIOs);
        }
        foreach (ProjectIO ProjectIO in ProjectIOs)
        {
            if (!(ProjectIO.GroupName != tn.Text))
            {
                tn.Nodes.Add("theProjectIO", ProjectIO.name);
            }
        }
    }

    public TreeNode findNode(string id, TreeNode startNode)
    {
        if (startNode.Name == id)
        {
            return startNode;
        }
        foreach (TreeNode node in startNode.Nodes)
        {
            if (findNode(id, node) != null)
            {
                return findNode(id, node);
            }
        }
        return null;
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Name == "PageItem" || e.Node.Name == "PageShapeItem" || e.Node.Name == "Page")
        {
            return;
        }
        if (e.Node.Name == "ProjectIO")
        {
            renewProjectIOs(treeView1.SelectedNode.Text);
        }
        else if (e.Node.Name == "设备变量")
        {
            if (!doc.HasChildNodes)
            {
                return;
            }
            doc.SelectNodes("/DocumentRoot/Group[@id='" + e.Node.Name + "']/GroupItem");
            DataTable dataTable = new();
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("名称");
            dataTable.Columns.Add("注释");
            dataTable.Columns.Add("类型");
            dataTable.Columns.Add("默认值");
            foreach (XmlNode xnitem in xnitems)
            {
                if (xnitem == null)
                {
                    continue;
                }
                DataRow dataRow;
                if (xnitem.Attributes["GroupID"] == null)
                {
                    if (e.Node != DeviceIORootNode)
                    {
                        break;
                    }
                    foreach (XmlNode xnitem2 in xnitems)
                    {
                        if (xnitem2 != null)
                        {
                            dataRow = dataTable.NewRow();
                            dataRow[0] = xnitem2.Attributes["id"].Value;
                            dataRow[1] = xnitem2.Attributes["Name"].Value;
                            dataRow[2] = xnitem2.Attributes["Tag"].Value;
                            dataRow[3] = changenumtotype(new string[1] { xnitem2.Attributes["ValType"].Value })[0];
                            dataRow[4] = xnitem2.Attributes["DefaultValue"].Value;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    break;
                }
                dataRow = dataTable.NewRow();
                if (!(type == "history") || !(xnitem.Attributes["ReservDB"].Value != "1"))
                {
                    dataRow[0] = xnitem.Attributes["id"].Value;
                    dataRow[1] = xnitem.Attributes["Name"].Value;
                    dataRow[2] = xnitem.Attributes["Tag"].Value;
                    dataRow[3] = changenumtotype(new string[1] { xnitem.Attributes["ValType"].Value })[0];
                    dataRow[4] = xnitem.Attributes["DefaultValue"].Value;
                    dataTable.Rows.Add(dataRow);
                }
            }
            dataGridView1.DataSource = dataTable;
        }
        else
        {
            if (!doc.HasChildNodes)
            {
                return;
            }
            doc.SelectNodes("/DocumentRoot/Group[@id='" + e.Node.Name + "']/GroupItem");
            DataTable dataTable2 = new();
            dataTable2.Columns.Add("id");
            dataTable2.Columns.Add("名称");
            dataTable2.Columns.Add("注释");
            dataTable2.Columns.Add("类型");
            dataTable2.Columns.Add("默认值");
            foreach (XmlNode xnitem3 in xnitems)
            {
                if (xnitem3 == null)
                {
                    continue;
                }
                if (xnitem3.Attributes["GroupID"] == null)
                {
                    if (e.Node != DeviceIORootNode)
                    {
                        break;
                    }
                    foreach (XmlNode xnitem4 in xnitems)
                    {
                        if (xnitem4 != null)
                        {
                            DataRow dataRow2 = dataTable2.NewRow();
                            dataRow2[0] = xnitem4.Attributes["id"].Value;
                            dataRow2[1] = xnitem4.Attributes["Name"].Value;
                            dataRow2[2] = xnitem4.Attributes["Tag"].Value;
                            dataRow2[3] = changenumtotype(new string[1] { xnitem4.Attributes["ValType"].Value })[0];
                            dataRow2[4] = xnitem4.Attributes["DefaultValue"].Value;
                            dataTable2.Rows.Add(dataRow2);
                        }
                    }
                    break;
                }
                if (xnitem3.Attributes["GroupID"].Value == e.Node.Name)
                {
                    DataRow dataRow3 = dataTable2.NewRow();
                    if (!(type == "history") || !(xnitem3.Attributes["ReservDB"].Value != "1"))
                    {
                        dataRow3[0] = xnitem3.Attributes["id"].Value;
                        dataRow3[1] = xnitem3.Attributes["Name"].Value;
                        dataRow3[2] = xnitem3.Attributes["Tag"].Value;
                        dataRow3[3] = changenumtotype(new string[1] { xnitem3.Attributes["ValType"].Value })[0];
                        dataRow3[4] = xnitem3.Attributes["DefaultValue"].Value;
                        dataTable2.Rows.Add(dataRow3);
                    }
                }
            }
            dataGridView1.DataSource = dataTable2;
        }
    }

    public void renewProjectIOs(string _GroupName)
    {
        if (ProjectIOTreeRoot != null)
        {
            if (type == "history")
            {
                dataGridView1.DataSource = projectiostable.Select("组='" + _GroupName + "' and 历史='true'");
            }
            else
            {
                dataGridView1.DataSource = projectiostable.Select("组='" + _GroupName + "'");
            }
            return;
        }
        DataTable dataTable = new();
        dataTable.Columns.Add("名称");
        dataTable.Columns.Add("标签");
        dataTable.Columns.Add("备注");
        dataTable.Columns.Add("类型");
        dataTable.Columns.Add("访问");
        dataTable.Columns.Add("仿真");
        dataTable.Columns.Add("范围上限");
        dataTable.Columns.Add("范围下限");
        dataTable.Columns.Add("周期(ms)");
        dataTable.Columns.Add("延时(ms)");
        dataTable.Columns.Add("id");
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            if (!(projectIO.GroupName != _GroupName) && (!(type == "history") || projectIO.History))
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = projectIO.name;
                dataRow[1] = projectIO.tag;
                dataRow[2] = projectIO.description;
                dataRow[3] = changenumtotype(new string[1] { projectIO.type })[0];
                dataRow[4] = projectIO.access;
                dataRow[5] = projectIO.emluator;
                dataRow[6] = projectIO.max;
                dataRow[7] = projectIO.min;
                dataRow[8] = projectIO.T;
                dataRow[9] = projectIO.delay;
                dataRow[10] = projectIO.ID;
                dataTable.Rows.Add(dataRow);
            }
        }
        dataGridView1.DataSource = dataTable;
    }

    public string[] changetypetonum(string[] type)
    {
        string[] array = new string[type.Length];
        for (int i = 0; i < type.Length; i++)
        {
            switch (type[i])
            {
                case "Boolean":
                    array[i] = "0";
                    break;
                case "SByte":
                    array[i] = "1";
                    break;
                case "Byte":
                    array[i] = "2";
                    break;
                case "Int16":
                    array[i] = "3";
                    break;
                case "UInt16":
                    array[i] = "4";
                    break;
                case "Int32":
                    array[i] = "5";
                    break;
                case "UInt32":
                    array[i] = "6";
                    break;
                case "Single":
                    array[i] = "7";
                    break;
                case "Double":
                    array[i] = "8";
                    break;
                case "String":
                    array[i] = "9";
                    break;
                case "IPAddress":
                    array[i] = "10";
                    break;
                case "Enum":
                    array[i] = "11";
                    break;
                case "Object":
                    array[i] = "1024";
                    break;
                default:
                    array[i] = "1024";
                    break;
            }
        }
        return array;
    }

    public string[] changenumtotype(string[] num)
    {
        string[] array = new string[num.Length];
        for (int i = 0; i < num.Length; i++)
        {
            switch (num[i])
            {
                case "0":
                    array[i] = "Boolean";
                    break;
                case "1":
                    array[i] = "SByte";
                    break;
                case "2":
                    array[i] = "Byte";
                    break;
                case "3":
                    array[i] = "Int16";
                    break;
                case "4":
                    array[i] = "UInt16";
                    break;
                case "5":
                    array[i] = "Int32";
                    break;
                case "6":
                    array[i] = "UInt32";
                    break;
                case "7":
                    array[i] = "Single";
                    break;
                case "8":
                    array[i] = "Double";
                    break;
                case "9":
                    array[i] = "String";
                    break;
                case "10":
                    array[i] = "IPAddress";
                    break;
                case "11":
                    array[i] = "Enum";
                    break;
                case "1024":
                    array[i] = "Object";
                    break;
                default:
                    array[i] = "Object";
                    break;
            }
        }
        return array;
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (dataGridView1.SelectedCells.Count != 0)
        {
            if (treeView1.SelectedNode.Name == "ProjectIO")
            {
                base.Tag = new string[3]
                {
                    "Globle." + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["名称"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["名称"].ToString(),
                    "-" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["id"].ToString()
                };
                tagvalue = ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["标签"].ToString();
            }
            else
            {
                base.Tag = new string[3]
                {
                    "Globle.var" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["id"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["名称"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["id"].ToString()
                };
                tagvalue = ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedCells[0].RowIndex]["注释"].ToString();
            }
            base.DialogResult = DialogResult.OK;
            Close();
            value = ((string[])base.Tag)[1];
        }
    }

    private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0)
        {
            return;
        }
        if (treeView1.SelectedNode.Name == "ProjectIO")
        {
            tagvalue = "";
            value = "";
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                base.Tag = new string[3]
                {
                    "Globle." + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[i].Index]["名称"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[i].Index]["名称"].ToString(),
                    "-" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[i].Index]["id"].ToString()
                };
                value = value + "|" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[i].Index]["名称"].ToString();
                tagvalue = tagvalue + "|" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[i].Index]["标签"].ToString();
            }
            tagvalue = tagvalue.Substring(1);
            value = value.Substring(1);
        }
        else
        {
            tagvalue = "";
            value = "";
            for (int j = 0; j < dataGridView1.SelectedRows.Count; j++)
            {
                base.Tag = new string[3]
                {
                    "Globle.var" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[j].Index]["id"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[j].Index]["名称"].ToString(),
                    ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[j].Index]["id"].ToString()
                };
                value = value + "|" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[j].Index]["名称"].ToString();
                tagvalue = tagvalue + "|" + ((DataTable)dataGridView1.DataSource).Rows[dataGridView1.SelectedRows[j].Index]["注释"].ToString();
            }
            tagvalue = tagvalue.Substring(1);
            value = value.Substring(1);
        }
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && dataGridView1.SelectedRows.Count != 0)
        {
            contextMenuStrip1.Show((Control)sender, e.Location);
        }
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
        components = new System.ComponentModel.Container();
        treeView1 = new System.Windows.Forms.TreeView();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        contextMenuStrip1.SuspendLayout();
        base.SuspendLayout();
        treeView1.Dock = System.Windows.Forms.DockStyle.Left;
        treeView1.Location = new System.Drawing.Point(0, 0);
        treeView1.Name = "treeView1";
        treeView1.Size = new System.Drawing.Size(177, 456);
        treeView1.TabIndex = 0;
        treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToResizeRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        dataGridView1.Location = new System.Drawing.Point(177, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(575, 456);
        dataGridView1.TabIndex = 1;
        dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
        dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseClick);
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { 添加ToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
        添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
        添加ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
        添加ToolStripMenuItem.Text = "添加";
        添加ToolStripMenuItem.Click += new System.EventHandler(添加ToolStripMenuItem_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(752, 456);
        base.Controls.Add(dataGridView1);
        base.Controls.Add(treeView1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        base.Name = "VarTable";
        Text = "变量表";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        contextMenuStrip1.ResumeLayout(false);
        base.ResumeLayout(false);
    }
}
