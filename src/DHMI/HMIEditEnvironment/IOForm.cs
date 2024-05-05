using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class IOForm : XtraForm
{
    private static readonly List<string> selectpath = new();

    private readonly List<DataFile> ldf = new();

    private int nowNo;

    private HMIProjectFile dhp;

    private int type;

    private TreeNode DeviceIORootTreeNode = new();

    private readonly XmlDocument tpdoc = new();

    private List<XmlNode> xnitems = new();

    private bool edit;

    public string io = "";

    private XmlDocument doc;

    public string iotype = "";

    private readonly List<ProjectIO> lcpio = new();

    private ProjectIO copypio;

    private readonly OpenFileDialog opfdlg = new();

    private readonly SaveFileDialog svfdlg = new();

    private readonly SaveFileDialog GetOutVarsInXML = new();

    private readonly OpenFileDialog AcquireVarsFromXML = new();

    private IContainer components;

    private TreeView treeView1;

    private DataGridView dataGridView1;

    private MenuStrip menuStrip1;

    private ToolStripMenuItem 变量操作ToolStripMenuItem;

    private ToolStripMenuItem 添加内部变量ToolStripMenuItem;

    private ToolStripMenuItem 修改内部变量ToolStripMenuItem;

    private ToolStripMenuItem 删除内部变量ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator1;

    private ToolStripMenuItem 复制ToolStripMenuItem;

    private ToolStripMenuItem 剪切ToolStripMenuItem;

    private ToolStripMenuItem 战列ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator2;

    private ToolStripMenuItem 引用统计ToolStripMenuItem;

    private ToolStripMenuItem 组操作ToolStripMenuItem;

    private ToolStripMenuItem 添加变量组ToolStripMenuItem;

    private ToolStripMenuItem 编辑变量组ToolStripMenuItem;

    private ToolStripMenuItem 删除变量组ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator3;

    private ToolStripMenuItem 导入变量ToolStripMenuItem;

    private Panel panel1;

    private Panel panel2;

    private ToolStripMenuItem 导出内部变量ToolStripMenuItem;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 添加内部变量ToolStripMenuItem1;

    private ToolStripMenuItem 编辑内部变量ToolStripMenuItem;

    private ToolStripMenuItem 删除内部变量ToolStripMenuItem1;

    private ToolStripSeparator toolStripSeparator4;

    private ToolStripMenuItem 复制ToolStripMenuItem1;

    private ToolStripMenuItem 剪切ToolStripMenuItem1;

    private ToolStripMenuItem 粘贴ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator5;

    private ToolStripMenuItem 导出选定变量ToolStripMenuItem1;

    private ToolStripMenuItem 导入内部变量ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator6;

    private ToolStripMenuItem 引用统计ToolStripMenuItem1;

    private ToolStripSeparator toolStripSeparator7;

    private ToolStripMenuItem 添加设备变量ToolStripMenuItem;

    private ToolStripMenuItem 变量同步ToolStripMenuItem;

    private ToolStripMenuItem 同步设备变量ToolStripMenuItem;

    private ToolStripMenuItem 批量添加OPC变量ToolStripMenuItem;

    private ToolStripMenuItem 导入XMLToolStripMenuItem;

    private ToolStripMenuItem 导出ToolStripMenuItem;

    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public bool Edit
    {
        get
        {
            return edit;
        }
        set
        {
            edit = value;
        }
    }

    public IOForm()
    {
        InitializeComponent();
    }

    private void LOAD()
    {
        doc = (XmlDocument)CEditEnvironmentGlobal.xmldoc.Clone();
        dhp = CEditEnvironmentGlobal.dhp;
        try
        {
            dataGridView1.Rows.Clear();
        }
        catch
        {
            dataGridView1.DataSource = new DataTable();
        }
        treeView1.Nodes.Clear();
        if (!edit && doc != null && doc.HasChildNodes)
        {
            XmlNodeList xmlNodeList = doc.SelectNodes("/DocumentRoot/Group");
            List<XmlNode> list = new();
            foreach (XmlNode item in xmlNodeList)
            {
                if (item.Attributes["ParentID"].Value != "-1")
                {
                    list.Add(item);
                }
            }
            DeviceIORootTreeNode = treeView1.Nodes.Add(xmlNodeList[0].Attributes["id"].Value, "设备变量", "G");
            TreeNode startNode = treeView1.Nodes[0];
            List<XmlNode> list2 = new();
            while (list.Count != 0)
            {
                foreach (XmlNode item2 in list)
                {
                    TreeNode treeNode = findNode(item2.Attributes["ParentID"].Value, startNode);
                    if (treeNode != null)
                    {
                        treeNode.Nodes.Add(item2.Attributes["id"].Value, item2.Attributes["Name"].Value, "G");
                        list2.Add(item2);
                    }
                }
                foreach (XmlNode item3 in list2)
                {
                    list.Remove(item3);
                }
            }
        }
        CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot = (TreeNode)CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Clone();
        CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Name = "ProjectIO";
        treeView1.Nodes.Add(CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot);
        treeView1.ExpandAll();
        XmlNodeList xmlNodeList2 = doc.SelectNodes("/DocumentRoot/Item");
        xnitems = new List<XmlNode>();
        for (int i = 0; i < xmlNodeList2.Count; i++)
        {
            for (int j = ((i - 1 >= 0) ? (Convert.ToInt32(xmlNodeList2[i - 1].Attributes["id"].Value) + 1) : 0); j < Convert.ToInt32(xmlNodeList2[i].Attributes["id"].Value); j++)
            {
                xnitems.Add(null);
            }
            xnitems.Add(xmlNodeList2[i]);
        }
        try
        {
            TreeNode treeNode2 = new();
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == selectpath[0])
                {
                    treeNode2 = node;
                    break;
                }
            }
            for (int k = 1; k <= selectpath.Count; k++)
            {
                foreach (TreeNode node2 in treeNode2.Nodes)
                {
                    if (node2.Text == selectpath[k])
                    {
                        treeNode2 = node2;
                        break;
                    }
                }
            }
            treeView1.SelectedNode = treeNode2;
        }
        catch
        {
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
        TreeNode treeNode5 = new("设备参数");
        treeNode5.Expand();
        if (CEditEnvironmentGlobal.dhp.DTPfiles == null)
        {
            CEditEnvironmentGlobal.dhp.DTPfiles = CEditEnvironmentGlobal.dhp.IOfiles.Replace("变量表.var", "设备拓扑.dtp");
        }
  
        if (treeNode5.Nodes.Count > 0)
        {
            treeView1.Nodes.Add(treeNode5);
        }
        CEditEnvironmentGlobal.varFileNeedReLoad = false;
    }

    public IOForm(HMIProjectFile _hpf, XmlDocument _doc, List<DataFile> _ldf, int _type, bool _edit)
    {
        InitializeComponent();
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
        else if (e.Node.Name == "设备参数子节点")
        {
            DataTable dataTable = new();
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("名称");
            dataTable.Columns.Add("类型");
            XmlNode xmlNode = tpdoc.SelectSingleNode("/DocumentRoot/DevInfo[@Name='" + e.Node.Text + "']");
            string value = xmlNode.Attributes["Path"].Value;
            FileInfo fileInfo = new(CEditEnvironmentGlobal.path + "\\" + CEditEnvironmentGlobal.dhp.DTPfiles);
            if (File.Exists(fileInfo.DirectoryName + "\\" + value))
            {
                XmlDocument xmlDocument = new();
                xmlDocument.Load(fileInfo.DirectoryName + "\\" + value);
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/DocumentRoot/Para");
                foreach (XmlNode item in xmlNodeList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = item.Attributes["ParaID"].Value;
                    dataRow[1] = e.Node.Text + item.Attributes["ParaName"].Value;
                    dataRow[2] = changenumtotype(new string[1] { item.Attributes["DataType"].Value })[0];
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
            dataTable2.Columns.Add("标签");
            dataTable2.Columns.Add("类型");
            dataTable2.Columns.Add("默认值");
            foreach (XmlNode xnitem in xnitems)
            {
                if (xnitem == null)
                {
                    continue;
                }
                if (xnitem.Attributes["GroupID"] == null)
                {
                    if (e.Node != DeviceIORootTreeNode)
                    {
                        break;
                    }
                    foreach (XmlNode xnitem2 in xnitems)
                    {
                        if (xnitem2 != null)
                        {
                            DataRow dataRow2 = dataTable2.NewRow();
                            dataRow2[0] = xnitem2.Attributes["id"].Value;
                            dataRow2[1] = xnitem2.Attributes["Name"].Value;
                            dataRow2[2] = xnitem2.Attributes["Tag"].Value;
                            dataRow2[3] = changenumtotype(new string[1] { xnitem2.Attributes["ValType"].Value })[0];
                            dataRow2[4] = xnitem2.Attributes["DefaultValue"].Value;
                            dataTable2.Rows.Add(dataRow2);
                        }
                    }
                    break;
                }
                if (xnitem.Attributes["GroupID"].Value == e.Node.Name)
                {
                    DataRow dataRow3 = dataTable2.NewRow();
                    dataRow3[0] = xnitem.Attributes["id"].Value;
                    dataRow3[1] = xnitem.Attributes["Name"].Value;
                    dataRow3[2] = xnitem.Attributes["Tag"].Value;
                    dataRow3[3] = changenumtotype(new string[1] { xnitem.Attributes["ValType"].Value })[0];
                    dataRow3[4] = xnitem.Attributes["DefaultValue"].Value;
                    dataTable2.Rows.Add(dataRow3);
                }
            }
            dataGridView1.DataSource = dataTable2;
        }
    }

    private void dataGridView1_DoubleClick(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedCells.Count == 0)
        {
            return;
        }
        if (edit)
        {
            if ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11)
            {
                return;
            }
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            ProjectIO projectIO = new(empty: true);
            foreach (ProjectIO projectIO2 in dhp.ProjectIOs)
            {
                if (projectIO2.name == dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString())
                {
                    projectIO = projectIO2;
                }
            }
            if (projectIO.name != null)
            {
                projectIOEditForm projectIOEditForm2 = new(projectIO);
                projectIOEditForm2.ShowDialog();
                renewProjectIOs(projectIO.GroupName);
                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            }
        }
        else
        {
            io = "[" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString() + "]";
            iotype = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["类型"].Value.ToString();
            if ((treeView1.SelectedNode != null && treeView1.SelectedNode.Name == "ProjectIO") || ((DataTable)dataGridView1.DataSource).Columns.Count == 11)
            {
                base.Tag = new string[3]
                {
                    "Globle." + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                    "-" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()
                };
            }
            else
            {
                base.Tag = new string[3]
                {
                    "Globle.var" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()
                };
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void treeView1_DoubleClick(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null && (treeView1.SelectedNode.Name == "PageItem" || treeView1.SelectedNode.Name == "PageShapeItem" || treeView1.SelectedNode.Name == "Page"))
        {
            if (treeView1.SelectedNode.Name == "PageItem")
            {
                io = treeView1.SelectedNode.Parent.Text + "." + treeView1.SelectedNode.Text;
                base.DialogResult = DialogResult.OK;
                Close();
            }
            if (treeView1.SelectedNode.Name == "PageShapeItem")
            {
                io = treeView1.SelectedNode.Parent.Parent.Text + "." + treeView1.SelectedNode.Parent.Text + "." + treeView1.SelectedNode.Text;
                base.DialogResult = DialogResult.OK;
                Close();
            }
        }
    }

    public void renewProjectIOs(string _GroupName)
    {
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
            if (!(projectIO.GroupName != _GroupName))
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

    private void 添加内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        string groupName = "内部变量";
        if (treeView1.SelectedNode != null && treeView1.SelectedNode.Name == "ProjectIO")
        {
            groupName = treeView1.SelectedNode.Text;
        }
        projectIOEditForm projectIOEditForm2 = new(groupName);
        if (projectIOEditForm2.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        ProjectIO pio = projectIOEditForm2.pio;
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            if (pio.name == projectIO.name)
            {
                MessageBox.Show("已经定义过名为" + pio.name + "的变量");
                return;
            }
        }
        dhp.ProjectIOs.Add(pio);
        renewProjectIOs(pio.GroupName);
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells["名称"].Value.ToString() == pio.name)
            {
                dataGridView1.CurrentCell = item.Cells[0];
            }
        }
    }

    private void 修改内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count <= 0 || ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11))
        {
            return;
        }
        ProjectIO projectIO = new();
        foreach (ProjectIO projectIO2 in dhp.ProjectIOs)
        {
            if (projectIO2.name == dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString())
            {
                projectIO = projectIO2;
            }
        }
        if (projectIO.name == null)
        {
            return;
        }
        projectIOEditForm projectIOEditForm2 = new(projectIO);
        if (projectIOEditForm2.ShowDialog() == DialogResult.OK && projectIOEditForm2.nameChanged)
        {
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
            }
        }
        renewProjectIOs(projectIO.GroupName);
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells["名称"].Value.ToString() == projectIO.name)
            {
                dataGridView1.CurrentCell = item.Cells[0];
            }
        }
    }

    private void 删除内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count <= 0 || ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11))
        {
            return;
        }
        yytjForm yytjForm2 = new(CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.dfs, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs);
        yytjForm2.FindInUseInitial();
        foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
        {
            ProjectIO projectIO = new();
            foreach (ProjectIO projectIO2 in dhp.ProjectIOs)
            {
                if (projectIO2.name == selectedRow.Cells["名称"].Value.ToString())
                {
                    projectIO = projectIO2;
                }
            }
            if (projectIO.name == null)
            {
                return;
            }
            DataRow dataRow = yytjForm2.FindInUseNew(projectIO.name);
            if (dataRow != null)
            {
                if (MessageBox.Show("变量" + projectIO.name + "在" + dataRow["画面"].ToString() + "处引用,是否强制删除?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dhp.ProjectIOs.Remove(projectIO);
                }
            }
            else
            {
                dhp.ProjectIOs.Remove(projectIO);
            }
        }
        renewProjectIOs(treeView1.SelectedNode.Text);
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
        }
    }

    private void 引用统计ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        yytjForm yytjForm2 = new(CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.dfs, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs);
        yytjForm2.Show();
    }

    private void 添加变量组ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ProjectIOGroupEditForm projectIOGroupEditForm = new();
        if (projectIOGroupEditForm.ShowDialog() == DialogResult.OK)
        {
            string str = projectIOGroupEditForm.str;
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Name == "ProjectIO")
            {
                treeView1.SelectedNode.Nodes.Add("ProjectIO", str);
            }
            else
            {
                CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Nodes.Add("ProjectIO", str);
            }
            CEditEnvironmentGlobal.varFileNeedReLoad = true;
        }
    }

    private void 编辑变量组ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode == null || treeView1.SelectedNode.Name != "ProjectIO" || treeView1.SelectedNode.Level == 0)
        {
            return;
        }
        string text = treeView1.SelectedNode.Text;
        ProjectIOGroupEditForm projectIOGroupEditForm = new(treeView1.SelectedNode.Text);
        if (projectIOGroupEditForm.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        string str = projectIOGroupEditForm.str;
        treeView1.SelectedNode.Text = str;
        ProjectIO[] array = CEditEnvironmentGlobal.dhp.ProjectIOs.ToArray();
        foreach (ProjectIO projectIO in array)
        {
            if (projectIO.GroupName == text)
            {
                projectIO.GroupName = str;
            }
        }
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
    }

    private void 删除变量组ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode == null || treeView1.SelectedNode.Name != "ProjectIO" || MessageBox.Show("删除变量组将同时删除其中的变量,是否执行操作?", "警告", MessageBoxButtons.YesNo) != DialogResult.Yes)
        {
            return;
        }
        RemoveVarGroup(treeView1.SelectedNode);
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
        }
    }

    private void RemoveVarGroup(TreeNode tn)
    {
        ProjectIO[] array = CEditEnvironmentGlobal.dhp.ProjectIOs.ToArray();
        foreach (ProjectIO projectIO in array)
        {
            if (projectIO.GroupName == tn.Text)
            {
                CEditEnvironmentGlobal.dhp.ProjectIOs.Remove(projectIO);
            }
        }
        foreach (TreeNode node in tn.Nodes)
        {
            RemoveVarGroup(node);
        }
        tn.Remove();
    }

    private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count <= 0 || ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11) || dataGridView1.SelectedCells.Count == 0)
        {
            return;
        }
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            if (projectIO.name == dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString())
            {
                copypio = projectIO.Copy();
                nowNo = 0;
                break;
            }
        }
    }

    private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count <= 0 || ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11))
        {
            return;
        }
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            if (!(projectIO.name == dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString()))
            {
                continue;
            }
            copypio = projectIO.Copy();
            nowNo = 0;
            CEditEnvironmentGlobal.dhp.ProjectIOs.Remove(projectIO);
            CEditEnvironmentGlobal.varFileNeedReLoad = true;
            renewProjectIOs(copypio.GroupName);
            {
                foreach (DataFile df in CEditEnvironmentGlobal.dfs)
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
                }
                break;
            }
        }
    }

    private void 战列ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (copypio == null || ((treeView1.SelectedNode == null || !(treeView1.SelectedNode.Name == "ProjectIO")) && ((DataTable)dataGridView1.DataSource).Columns.Count != 11))
        {
            return;
        }
        ProjectIO projectIO = copypio.Copy();
        if (treeView1.SelectedNode == null)
        {
            projectIO.GroupName = dhp.ProjectIOTreeRoot.Text;
        }
        else if (treeView1.SelectedNode.Name == "ProjectIO")
        {
            projectIO.GroupName = treeView1.SelectedNode.Text;
        }
        else
        {
            projectIO.GroupName = dhp.ProjectIOTreeRoot.Text;
        }
        Regex regex = new("(^[a-zA-Z_][a-zA-Z0-9_]*?)([0-9]*)$");
        nowNo = Convert.ToInt32((regex.Match(projectIO.name).Groups[2].Value == "") ? "0" : regex.Match(projectIO.name).Groups[2].Value);
        projectIO.name = regex.Match(projectIO.name).Groups[1].Value + ++nowNo;
        while (true)
        {
        IL_016c:
            foreach (ProjectIO projectIO2 in CEditEnvironmentGlobal.dhp.ProjectIOs)
            {
                if (projectIO2.name == projectIO.name)
                {
                    nowNo = Convert.ToInt32(regex.Match(projectIO.name).Groups[2].Value);
                    projectIO.name = regex.Match(projectIO.name).Groups[1].Value + ++nowNo;
                    goto IL_016c;
                }
            }
            break;
        }
        projectIO.ID = ++ProjectIO.StaticID;
        dhp.ProjectIOs.Add(projectIO);
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
        }
        renewProjectIOs(projectIO.GroupName);
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells["名称"].Value.ToString() == projectIO.name)
            {
                dataGridView1.CurrentCell = item.Cells[0];
            }
        }
    }

    private void 导入变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.path == "")
        {
            return;
        }
        try
        {
            opfdlg.Filter = "变量导出文件(*.vxp)|*.vxp|工程文件(*.dhp)|*.dhp";
            if (opfdlg.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new(opfdlg.FileName);
                if (fileInfo.Extension == ".dhp")
                {
                    Environment.CurrentDirectory = CEditEnvironmentGlobal.path;
                    HMIProjectFile hMIProjectFile = new();
                    hMIProjectFile = Operation.BinaryLoadProject(opfdlg.FileName);
                    foreach (ProjectIO projectIO2 in hMIProjectFile.ProjectIOs)
                    {
                        while (true)
                        {
                        IL_0094:
                            foreach (ProjectIO projectIO3 in CEditEnvironmentGlobal.dhp.ProjectIOs)
                            {
                                if (projectIO2.name == projectIO3.name)
                                {
                                    MessageBox.Show("导入变量 " + projectIO2.name + " 同已有变量冲突.");
                                    if (new renameProjectIOForm(projectIO2).ShowDialog() == DialogResult.OK)
                                    {
                                        goto IL_0094;
                                    }
                                    goto end_IL_0094;
                                }
                            }
                            projectIO2.ID = ++ProjectIO.StaticID;
                            projectIO2.GroupName = dhp.ProjectIOTreeRoot.Text;
                            dhp.ProjectIOs.Add(projectIO2);
                            break;

                        end_IL_0094:
                            break;
                        }
                    }
                    renewProjectIOs(dhp.ProjectIOTreeRoot.Text);
                }
                else if (fileInfo.Extension == ".vxp")
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(opfdlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    ProjectIO[] array = (ProjectIO[])formatter.Deserialize(stream);
                    stream.Close();
                    ProjectIO[] array2 = array;
                    foreach (ProjectIO projectIO in array2)
                    {
                        while (true)
                        {
                        IL_01d4:
                            foreach (ProjectIO projectIO4 in CEditEnvironmentGlobal.dhp.ProjectIOs)
                            {
                                if (projectIO.name == projectIO4.name)
                                {
                                    MessageBox.Show("导入变量 " + projectIO.name + " 同已有变量冲突.");
                                    if (new renameProjectIOForm(projectIO).ShowDialog() == DialogResult.OK)
                                    {
                                        goto IL_01d4;
                                    }
                                    goto end_IL_01d4;
                                }
                            }
                            projectIO.ID = ++ProjectIO.StaticID;
                            projectIO.GroupName = dhp.ProjectIOTreeRoot.Text;
                            dhp.ProjectIOs.Add(projectIO);
                            break;

                        end_IL_01d4:
                            break;
                        }
                    }
                    renewProjectIOs(dhp.ProjectIOTreeRoot.Text);
                }
            }
            CEditEnvironmentGlobal.varFileNeedReLoad = true;
        }
        catch (Exception)
        {
            MessageBox.Show("导入文件无法被识别,请检查导入文件");
        }
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void 导出内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!(CEditEnvironmentGlobal.path == ""))
        {
            svfdlg.Filter = "变量导出文件(*.vxp)|*.vxp";
            if (svfdlg.ShowDialog() == DialogResult.OK)
            {
                CEditEnvironmentGlobal.dhp.tprojectios = CEditEnvironmentGlobal.dhp.ProjectIOs.ToArray();
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(svfdlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, CEditEnvironmentGlobal.dhp.tprojectios);
                stream.Close();
            }
        }
    }

    private void 添加内部变量ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        添加内部变量ToolStripMenuItem_Click(sender, e);
    }

    private void 编辑内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        修改内部变量ToolStripMenuItem_Click(sender, e);
    }

    private void 删除内部变量ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        删除内部变量ToolStripMenuItem_Click(sender, e);
    }

    private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        复制ToolStripMenuItem_Click(sender, e);
    }

    private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        剪切ToolStripMenuItem_Click(sender, e);
    }

    private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        战列ToolStripMenuItem_Click(sender, e);
    }

    private void 引用统计ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        引用统计ToolStripMenuItem_Click(sender, e);
    }

    private void 导出内部变量ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.path == "" || ((DataTable)dataGridView1.DataSource).Columns.Count != 11)
        {
            return;
        }
        svfdlg.Filter = "变量导出文件(*.vxp)|*.vxp";
        if (svfdlg.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        List<ProjectIO> list = new();
        foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
        {
            foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
            {
                if (projectIO.name == selectedRow.Cells[0].Value.ToString())
                {
                    list.Add(projectIO);
                }
            }
        }
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(svfdlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, list.ToArray());
        stream.Close();
    }

    private void 导入内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        导入变量ToolStripMenuItem_Click(sender, e);
    }

    private void IOForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        CheckIOExists.IOTableOld = true;
        selectpath.Clear();
        if (treeView1.SelectedNode != null)
        {
            for (TreeNode selectedNode = treeView1.SelectedNode; selectedNode != null; selectedNode = selectedNode.Parent)
            {
                selectpath.Add(selectedNode.Text);
            }
        }
        int num = 0;
        string[] array = selectpath.ToArray();
        foreach (string value in array)
        {
            selectpath[selectpath.Count - num - 1] = value;
            num++;
        }
        if (edit)
        {
            e.Cancel = true;
            Hide();
        }
    }

    private void IOForm_Load(object sender, EventArgs e)
    {
        LOAD();
        try
        {
            if (type == 1)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
            if (type == 2)
            {
                treeView1.SelectedNode = treeView1.Nodes[1];
            }
        }
        catch (Exception)
        {
        }
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        if (base.Visible && CEditEnvironmentGlobal.varFileNeedReLoad)
        {
            LOAD();
        }
        base.OnVisibleChanged(e);
    }

    private void 同步设备变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LOAD();
    }

    private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
    {
    }

    private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        GetOutVarsInXML.Filter = "变量表（*.xml）|*.xml";
        if (GetOutVarsInXML.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        FileInfo fileInfo = new(GetOutVarsInXML.FileName);
        if (!fileInfo.Exists)
        {
            FileStream stream = new(GetOutVarsInXML.FileName, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new(stream);
            streamWriter.WriteLine("<?xml version='1.0' encoding='gb2312'?>");
            streamWriter.WriteLine("<XMLRoot>");
            streamWriter.WriteLine("</XMLRoot>");
            streamWriter.Close();
        }
        XmlDocument xmlDocument = new();
        xmlDocument.Load(GetOutVarsInXML.FileName);
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            XmlNode xmlNode = xmlDocument.SelectSingleNode("XMLRoot");
            XmlElement xmlElement = xmlDocument.CreateElement("Vars");
            xmlElement.SetAttribute("Name", projectIO.name);
            xmlElement.SetAttribute("Tag", projectIO.tag);
            xmlElement.SetAttribute("Description", projectIO.description);
            xmlElement.SetAttribute("VarsType", projectIO.type);
            xmlElement.SetAttribute("Access", projectIO.access);
            xmlElement.SetAttribute("Emluator", projectIO.emluator);
            xmlElement.SetAttribute("Max", projectIO.max);
            xmlElement.SetAttribute("Min", projectIO.min);
            xmlElement.SetAttribute("T", projectIO.T);
            xmlElement.SetAttribute("Delay", projectIO.delay);
            xmlElement.SetAttribute("ID", projectIO.ID.ToString());
            xmlElement.SetAttribute("GroupName", projectIO.GroupName);
            xmlElement.SetAttribute("History", projectIO.History.ToString());
            ((XmlElement)xmlNode).AppendChild(xmlElement);
            xmlDocument.Save(GetOutVarsInXML.FileName);
        }
    }

    private void 导入XMLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            AcquireVarsFromXML.Filter = "变量表(*.xml)|*.xml";
            if (AcquireVarsFromXML.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new(AcquireVarsFromXML.FileName);
                if (fileInfo.Extension == ".xml")
                {
                    List<ProjectIO> list = new();
                    XmlDocument xmlDocument = new();
                    xmlDocument.Load(AcquireVarsFromXML.FileName);
                    XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Vars");
                    foreach (XmlNode item in xmlNodeList)
                    {
                        ProjectIO projectIO = new()
                        {
                            name = ((XmlElement)item).GetAttribute("Name"),
                            tag = ((XmlElement)item).GetAttribute("Tag"),
                            description = ((XmlElement)item).GetAttribute("Description"),
                            type = ((XmlElement)item).GetAttribute("VarsType"),
                            access = ((XmlElement)item).GetAttribute("Access"),
                            emluator = ((XmlElement)item).GetAttribute("Emluator"),
                            max = ((XmlElement)item).GetAttribute("Max"),
                            min = ((XmlElement)item).GetAttribute("Min")
                        };
                        projectIO.emluator = ((XmlElement)item).GetAttribute("Emluator");
                        projectIO.T = ((XmlElement)item).GetAttribute("T");
                        projectIO.delay = ((XmlElement)item).GetAttribute("Delay");
                        projectIO.ID = Convert.ToInt32(((XmlElement)item).GetAttribute("ID"));
                        projectIO.GroupName = ((XmlElement)item).GetAttribute("GroupName");
                        if (((XmlElement)item).GetAttribute("History").ToString() == "True")
                        {
                            projectIO.History = true;
                        }
                        else if (((XmlElement)item).GetAttribute("History").ToString() == "False")
                        {
                            projectIO.History = false;
                        }
                        list.Add(projectIO);
                    }
                    switch (MessageBox.Show("是否覆盖原有变量（选择是：变量表的变量覆盖原有变量，选择否：如有重复使用原有变量）？", "提示", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            foreach (ProjectIO item2 in list)
                            {
                                foreach (ProjectIO projectIO2 in dhp.ProjectIOs)
                                {
                                    if (!(projectIO2.name == item2.name))
                                    {
                                        continue;
                                    }
                                    dhp.ProjectIOs.Remove(projectIO2);
                                    dhp.ProjectIOs.Add(item2);
                                    goto IL_0312;
                                }
                                dhp.ProjectIOs.Add(item2);
                            IL_0312:;
                            }
                            break;
                        case DialogResult.No:
                            foreach (ProjectIO item3 in list)
                            {
                                foreach (ProjectIO projectIO3 in dhp.ProjectIOs)
                                {
                                    if (projectIO3.name == item3.name)
                                    {
                                        break;
                                    }
                                }
                                dhp.ProjectIOs.Add(item3);
                            }
                            break;
                    }
                    renewProjectIOs(dhp.ProjectIOTreeRoot.Text);
                }
            }
            CEditEnvironmentGlobal.varFileNeedReLoad = true;
        }
        catch (Exception)
        {
            MessageBox.Show("导入文件无法被识别,请检查导入文件");
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
        添加内部变量ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        删除内部变量ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        编辑内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        批量添加OPC变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        添加设备变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        复制ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        剪切ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        导入内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        导出选定变量ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        引用统计ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        menuStrip1 = new System.Windows.Forms.MenuStrip();
        变量操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        添加内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        修改内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        删除内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        导入变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        导出内部变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        战列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        引用统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        导入XMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        组操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        添加变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        编辑变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        删除变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        变量同步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        同步设备变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        panel1 = new System.Windows.Forms.Panel();
        panel2 = new System.Windows.Forms.Panel();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        contextMenuStrip1.SuspendLayout();
        menuStrip1.SuspendLayout();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        base.SuspendLayout();
        treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
        treeView1.HideSelection = false;
        treeView1.Location = new System.Drawing.Point(0, 0);
        treeView1.Name = "treeView1";
        treeView1.Size = new System.Drawing.Size(293, 569);
        treeView1.TabIndex = 0;
        treeView1.DoubleClick += new System.EventHandler(treeView1_DoubleClick);
        treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToOrderColumns = true;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.BackgroundColor = System.Drawing.Color.White;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.ContextMenuStrip = contextMenuStrip1;
        dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        dataGridView1.Location = new System.Drawing.Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(703, 569);
        dataGridView1.TabIndex = 1;
        dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseClick);
        dataGridView1.DoubleClick += new System.EventHandler(dataGridView1_DoubleClick);
        dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
        {
            添加内部变量ToolStripMenuItem1, 删除内部变量ToolStripMenuItem1, 编辑内部变量ToolStripMenuItem, toolStripSeparator7, 批量添加OPC变量ToolStripMenuItem, 添加设备变量ToolStripMenuItem, toolStripSeparator4, 复制ToolStripMenuItem1, 剪切ToolStripMenuItem1, 粘贴ToolStripMenuItem,
            toolStripSeparator5, 导入内部变量ToolStripMenuItem, 导出选定变量ToolStripMenuItem1, toolStripSeparator6, 引用统计ToolStripMenuItem1
        });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(174, 292);
        添加内部变量ToolStripMenuItem1.Name = "添加内部变量ToolStripMenuItem1";
        添加内部变量ToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
        添加内部变量ToolStripMenuItem1.Text = "添加内部变量";
        添加内部变量ToolStripMenuItem1.Click += new System.EventHandler(添加内部变量ToolStripMenuItem1_Click);
        删除内部变量ToolStripMenuItem1.Name = "删除内部变量ToolStripMenuItem1";
        删除内部变量ToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
        删除内部变量ToolStripMenuItem1.Text = "删除内部变量";
        删除内部变量ToolStripMenuItem1.Click += new System.EventHandler(删除内部变量ToolStripMenuItem1_Click);
        编辑内部变量ToolStripMenuItem.Name = "编辑内部变量ToolStripMenuItem";
        编辑内部变量ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
        编辑内部变量ToolStripMenuItem.Text = "编辑内部变量";
        编辑内部变量ToolStripMenuItem.Click += new System.EventHandler(编辑内部变量ToolStripMenuItem_Click);
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new System.Drawing.Size(170, 6);
        toolStripSeparator7.Visible = false;
        添加设备变量ToolStripMenuItem.Name = "添加设备变量ToolStripMenuItem";
        添加设备变量ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
        添加设备变量ToolStripMenuItem.Text = "添加设备变量";
        添加设备变量ToolStripMenuItem.Visible = false;
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new System.Drawing.Size(170, 6);
        复制ToolStripMenuItem1.Name = "复制ToolStripMenuItem1";
        复制ToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
        复制ToolStripMenuItem1.Text = "复制";
        复制ToolStripMenuItem1.Click += new System.EventHandler(复制ToolStripMenuItem1_Click);
        剪切ToolStripMenuItem1.Name = "剪切ToolStripMenuItem1";
        剪切ToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
        剪切ToolStripMenuItem1.Text = "剪切";
        剪切ToolStripMenuItem1.Click += new System.EventHandler(剪切ToolStripMenuItem1_Click);
        粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
        粘贴ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
        粘贴ToolStripMenuItem.Text = "粘贴";
        粘贴ToolStripMenuItem.Click += new System.EventHandler(粘贴ToolStripMenuItem_Click);
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new System.Drawing.Size(170, 6);
        导入内部变量ToolStripMenuItem.Name = "导入内部变量ToolStripMenuItem";
        导入内部变量ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
        导入内部变量ToolStripMenuItem.Text = "导入内部变量";
        导入内部变量ToolStripMenuItem.Click += new System.EventHandler(导入内部变量ToolStripMenuItem_Click);
        导出选定变量ToolStripMenuItem1.Name = "导出选定变量ToolStripMenuItem1";
        导出选定变量ToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
        导出选定变量ToolStripMenuItem1.Text = "导出选定变量";
        导出选定变量ToolStripMenuItem1.Click += new System.EventHandler(导出内部变量ToolStripMenuItem1_Click);
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new System.Drawing.Size(170, 6);
        引用统计ToolStripMenuItem1.Name = "引用统计ToolStripMenuItem1";
        引用统计ToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
        引用统计ToolStripMenuItem1.Text = "引用统计";
        引用统计ToolStripMenuItem1.Click += new System.EventHandler(引用统计ToolStripMenuItem1_Click);
        menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { 变量操作ToolStripMenuItem, 组操作ToolStripMenuItem, 变量同步ToolStripMenuItem });
        menuStrip1.Location = new System.Drawing.Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
        menuStrip1.Size = new System.Drawing.Size(996, 25);
        menuStrip1.TabIndex = 5;
        menuStrip1.Text = "menuStrip1";
        变量操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
        {
            添加内部变量ToolStripMenuItem, 修改内部变量ToolStripMenuItem, 删除内部变量ToolStripMenuItem, toolStripSeparator3, 导入变量ToolStripMenuItem, 导出内部变量ToolStripMenuItem, toolStripSeparator1, 复制ToolStripMenuItem, 剪切ToolStripMenuItem, 战列ToolStripMenuItem,
            toolStripSeparator2, 引用统计ToolStripMenuItem, 导出ToolStripMenuItem, 导入XMLToolStripMenuItem
        });
        变量操作ToolStripMenuItem.Name = "变量操作ToolStripMenuItem";
        变量操作ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
        变量操作ToolStripMenuItem.Text = "变量操作";
        添加内部变量ToolStripMenuItem.Name = "添加内部变量ToolStripMenuItem";
        添加内部变量ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        添加内部变量ToolStripMenuItem.Text = "添加内部变量";
        添加内部变量ToolStripMenuItem.Click += new System.EventHandler(添加内部变量ToolStripMenuItem_Click);
        修改内部变量ToolStripMenuItem.Name = "修改内部变量ToolStripMenuItem";
        修改内部变量ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        修改内部变量ToolStripMenuItem.Text = "编辑内部变量";
        修改内部变量ToolStripMenuItem.Click += new System.EventHandler(修改内部变量ToolStripMenuItem_Click);
        删除内部变量ToolStripMenuItem.Name = "删除内部变量ToolStripMenuItem";
        删除内部变量ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
        删除内部变量ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        删除内部变量ToolStripMenuItem.Text = "删除内部变量";
        删除内部变量ToolStripMenuItem.Click += new System.EventHandler(删除内部变量ToolStripMenuItem_Click);
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new System.Drawing.Size(190, 6);
        导入变量ToolStripMenuItem.Name = "导入变量ToolStripMenuItem";
        导入变量ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        导入变量ToolStripMenuItem.Text = "导入内部变量";
        导入变量ToolStripMenuItem.Click += new System.EventHandler(导入变量ToolStripMenuItem_Click);
        导出内部变量ToolStripMenuItem.Name = "导出内部变量ToolStripMenuItem";
        导出内部变量ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        导出内部变量ToolStripMenuItem.Text = "导出内部变量";
        导出内部变量ToolStripMenuItem.Click += new System.EventHandler(导出内部变量ToolStripMenuItem_Click);
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
        复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
        复制ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
        复制ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        复制ToolStripMenuItem.Text = "复制";
        复制ToolStripMenuItem.Click += new System.EventHandler(复制ToolStripMenuItem_Click);
        剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
        剪切ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.X | System.Windows.Forms.Keys.Control;
        剪切ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        剪切ToolStripMenuItem.Text = "剪切";
        剪切ToolStripMenuItem.Click += new System.EventHandler(剪切ToolStripMenuItem_Click);
        战列ToolStripMenuItem.Name = "战列ToolStripMenuItem";
        战列ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control;
        战列ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        战列ToolStripMenuItem.Text = "粘贴";
        战列ToolStripMenuItem.Click += new System.EventHandler(战列ToolStripMenuItem_Click);
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
        引用统计ToolStripMenuItem.Name = "引用统计ToolStripMenuItem";
        引用统计ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        引用统计ToolStripMenuItem.Text = "引用统计";
        引用统计ToolStripMenuItem.Click += new System.EventHandler(引用统计ToolStripMenuItem_Click);
        导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
        导出ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        导出ToolStripMenuItem.Text = "导出XML变量表";
        导出ToolStripMenuItem.Click += new System.EventHandler(导出ToolStripMenuItem_Click);
        导入XMLToolStripMenuItem.Name = "导入XMLToolStripMenuItem";
        导入XMLToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
        导入XMLToolStripMenuItem.Text = "导入XML变量表";
        导入XMLToolStripMenuItem.Click += new System.EventHandler(导入XMLToolStripMenuItem_Click);
        组操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { 添加变量组ToolStripMenuItem, 编辑变量组ToolStripMenuItem, 删除变量组ToolStripMenuItem });
        组操作ToolStripMenuItem.Name = "组操作ToolStripMenuItem";
        组操作ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
        组操作ToolStripMenuItem.Text = "组操作";
        添加变量组ToolStripMenuItem.Name = "添加变量组ToolStripMenuItem";
        添加变量组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
        添加变量组ToolStripMenuItem.Text = "添加变量组";
        添加变量组ToolStripMenuItem.Click += new System.EventHandler(添加变量组ToolStripMenuItem_Click);
        编辑变量组ToolStripMenuItem.Name = "编辑变量组ToolStripMenuItem";
        编辑变量组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
        编辑变量组ToolStripMenuItem.Text = "编辑变量组";
        编辑变量组ToolStripMenuItem.Click += new System.EventHandler(编辑变量组ToolStripMenuItem_Click);
        删除变量组ToolStripMenuItem.Name = "删除变量组ToolStripMenuItem";
        删除变量组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
        删除变量组ToolStripMenuItem.Text = "删除变量组";
        删除变量组ToolStripMenuItem.Click += new System.EventHandler(删除变量组ToolStripMenuItem_Click);
        变量同步ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { 同步设备变量ToolStripMenuItem });
        变量同步ToolStripMenuItem.Name = "变量同步ToolStripMenuItem";
        变量同步ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
        变量同步ToolStripMenuItem.Text = "变量同步";
        同步设备变量ToolStripMenuItem.Name = "同步设备变量ToolStripMenuItem";
        同步设备变量ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
        同步设备变量ToolStripMenuItem.Text = "同步设备变量";
        同步设备变量ToolStripMenuItem.Click += new System.EventHandler(同步设备变量ToolStripMenuItem_Click);
        panel1.Controls.Add(dataGridView1);
        panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        panel1.Location = new System.Drawing.Point(293, 25);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(703, 569);
        panel1.TabIndex = 6;
        panel2.Controls.Add(treeView1);
        panel2.Dock = System.Windows.Forms.DockStyle.Left;
        panel2.Location = new System.Drawing.Point(0, 25);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(293, 569);
        panel2.TabIndex = 7;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(996, 594);
        base.Controls.Add(panel1);
        base.Controls.Add(panel2);
        base.Controls.Add(menuStrip1);
        base.MainMenuStrip = menuStrip1;
        base.Name = "IOForm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "变量管理";
        base.Load += new System.EventHandler(IOForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(IOForm_FormClosing);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        contextMenuStrip1.ResumeLayout(false);
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
