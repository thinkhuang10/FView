using CommonSnappableTypes;
using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment;

public class VarTable : XtraForm
{
    private int nowNo;

    private readonly HMIProjectFile dhp;

    private ProjectIO copypio;

    private readonly DataTable projectiostable = new();

    private readonly TreeNode ProjectIOTreeRoot;

    private readonly string type;

    private readonly XmlDocument doc;

    private readonly TreeNode DeviceIORootNode = new();

    private readonly XmlDocument tpdoc = new();

    private List<XmlNode> xnitems = new();

    public string tagvalue;

    public string value;

    private int _iIndex;

    private int _iFindIndex;

    private string _strText = "";

    private readonly OpenFileDialog opfdlg = new();

    private readonly SaveFileDialog svfdlg = new();

    private int _iCount;

    private int _iCount1;

    private readonly List<ProjectIO> ListFind = new();

    private IContainer components;

    private TreeView treeView1;

    private DataGridView dataGridView1;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 添加ToolStripMenuItem;

    private Button button1;

    private Label label1;

    private TextBox textBox1;

    private Panel panel1;

    private MenuStrip menuStrip1;

    private ToolStripMenuItem 变量操作ToolStripMenuItem;

    private ToolStripMenuItem 添加内部变量ToolStripMenuItem;

    private ToolStripMenuItem 修改内部变量ToolStripMenuItem;

    private ToolStripMenuItem 删除内部变量ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator3;

    private ToolStripMenuItem 导入变量ToolStripMenuItem;

    private ToolStripMenuItem 导出内部变量ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator1;

    private ToolStripMenuItem 复制ToolStripMenuItem;

    private ToolStripMenuItem 剪切ToolStripMenuItem;

    private ToolStripMenuItem 战列ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator2;

    private ToolStripMenuItem 组操作ToolStripMenuItem;

    private ToolStripMenuItem 添加变量组ToolStripMenuItem;

    private ToolStripMenuItem 编辑变量组ToolStripMenuItem;

    private ToolStripMenuItem 删除变量组ToolStripMenuItem;

    private Button button2;

    private ToolTip toolTip1;

    public VarTable(HMIProjectFile _hpf, XmlDocument _doc, string _type)
    {
        InitializeComponent();

        doc = _doc;
        dhp = _hpf;
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

            List<XmlNode> list = new();
            TreeNode startNode = treeNode2.Nodes[0];
            List<XmlNode> list2 = new();
            while (list.Count != 0)
            {
                foreach (XmlNode item2 in list)
                {
                    TreeNode treeNode3 = FindNode(item2.Attributes["ParentID"].Value, startNode);
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

            xnitems = new List<XmlNode>();

            TreeNode treeNode8 = treeView1.Nodes.Add("PageRoot", "组态页面");
            treeNode8.Expand();
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                TreeNode treeNode9 = treeNode8.Nodes.Add("PageRoot." + df.name, df.pageName);
                foreach (CShape item8 in df.ListAllShowCShape)
                {
                    treeNode9.Nodes.Add("PageRoot." + df.name + "." + item8.Name, item8.Name).Tag = item8;
                }
            }
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

    public TreeNode FindNode(string id, TreeNode startNode)
    {
        if (startNode.Name == id)
            return startNode;

        foreach (TreeNode node in startNode.Nodes)
        {
            if (FindNode(id, node) != null)
                return FindNode(id, node);
        }

        return null;
    }

    private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Name == "PageItem" || e.Node.Name == "PageShapeItem" || e.Node.Name == "Page")
        {
            return;
        }
        if (e.Node.Name == "ProjectIO")
        {
            RenewProjectIOs(treeView1.SelectedNode.Text);
        }
        else if (e.Node.Name.StartsWith("PageRoot") && e.Node.Tag is CShape)
        {
            DataTable dataTable = new();
            dataTable.Columns.Add("名称");
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("显示名称");
            dataTable.Columns.Add("类型");
            dataTable.Columns.Add("说明");
            CShape cShape = e.Node.Tag as CShape;
            object obj = ((cShape is not CControl) ? ((object)cShape) : ((object)(cShape as CControl)._c));
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            int num = 0;
            PropertyInfo[] array = properties;
            foreach (PropertyInfo propertyInfo in array)
            {
                if (!(propertyInfo.PropertyType.Name == "Boolean") && !(propertyInfo.PropertyType.Name == "Single") && !(propertyInfo.PropertyType.Name == "Int32") && !(propertyInfo.PropertyType.Name == "String") && !(propertyInfo.PropertyType.Name == "Object"))
                {
                    continue;
                }
                try
                {
                    if (obj is AxHost)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        int num2 = ++num;
                        dataRow[1] = num2.ToString();
                        try
                        {
                            if (Attribute.IsDefined(propertyInfo, typeof(DisplayNameAttribute)))
                            {
                                dataRow[2] = ((DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute))).DisplayName;
                            }
                            if (dataRow[2] == null)
                            {
                                dataRow[2] = propertyInfo.Name;
                            }
                        }
                        catch
                        {
                            dataRow[2] = propertyInfo.Name;
                        }
                        dataRow[0] = propertyInfo.Name;
                        dataRow[3] = propertyInfo.PropertyType.Name;
                        try
                        {
                            if (Attribute.IsDefined(propertyInfo, typeof(DescriptionAttribute)))
                            {
                                dataRow[4] = ((DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute))).Description;
                            }
                        }
                        catch
                        {
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    else
                    {
                        if (Attribute.IsDefined(propertyInfo, typeof(DHMIHidePropertyAttribute)))
                        {
                            continue;
                        }
                        DataRow dataRow2 = dataTable.NewRow();
                        int num3 = ++num;
                        dataRow2[1] = num3.ToString();
                        try
                        {
                            if (Attribute.IsDefined(propertyInfo, typeof(DisplayNameAttribute)))
                            {
                                dataRow2[2] = ((DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute))).DisplayName;
                            }
                            if (dataRow2[2] == null)
                            {
                                dataRow2[2] = propertyInfo.Name;
                            }
                        }
                        catch
                        {
                            dataRow2[2] = propertyInfo.Name;
                        }
                        dataRow2[0] = propertyInfo.Name;
                        dataRow2[3] = propertyInfo.PropertyType.Name;
                        try
                        {
                            if (Attribute.IsDefined(propertyInfo, typeof(DescriptionAttribute)))
                            {
                                dataRow2[4] = ((DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute))).Description;
                            }
                        }
                        catch
                        {
                        }
                        dataTable.Rows.Add(dataRow2);
                        continue;
                    }
                }
                catch (Exception)
                {
                }
            }
            dataGridView1.DataSource = dataTable;
        }
        else if (e.Node.Name == "设备参数子节点")
        {
            DataTable dataTable2 = new();
            dataTable2.Columns.Add("id");
            dataTable2.Columns.Add("名称");
            dataTable2.Columns.Add("类型");
            XmlNode xmlNode = tpdoc.SelectSingleNode("/DocumentRoot/DevInfo[@Name='" + e.Node.Text + "']");
            string text = xmlNode.Attributes["Path"].Value;
            FileInfo fileInfo = new(CEditEnvironmentGlobal.HMIPath + "\\" + CEditEnvironmentGlobal.dhp.DTPfiles);
            if (File.Exists(fileInfo.DirectoryName + "\\" + text))
            {
                XmlDocument xmlDocument = new();
                xmlDocument.Load(fileInfo.DirectoryName + "\\" + text);
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/DocumentRoot/Para");
                foreach (XmlNode item in xmlNodeList)
                {
                    DataRow dataRow3 = dataTable2.NewRow();
                    dataRow3[0] = item.Attributes["ParaID"].Value;
                    dataRow3[1] = e.Node.Text + item.Attributes["ParaName"].Value;
                    dataRow3[2] = Changenumtotype(new string[1] { item.Attributes["DataType"].Value })[0];
                    dataTable2.Rows.Add(dataRow3);
                }
            }
            dataGridView1.DataSource = dataTable2;
        }
        else if (e.Node.Name == "设备变量")
        {
            if (!doc.HasChildNodes)
            {
                return;
            }
            doc.SelectNodes("/DocumentRoot/Group[@id='" + e.Node.Name + "']/GroupItem");
            DataTable dataTable3 = new();
            dataTable3.Columns.Add("名称");
            dataTable3.Columns.Add("id");
            dataTable3.Columns.Add("注释");
            dataTable3.Columns.Add("类型");
            dataTable3.Columns.Add("默认值");
            foreach (XmlNode xnitem in xnitems)
            {
                if (xnitem == null)
                {
                    continue;
                }
                DataRow dataRow4;
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
                            dataRow4 = dataTable3.NewRow();
                            dataRow4[0] = xnitem2.Attributes["Name"].Value;
                            dataRow4[1] = xnitem2.Attributes["id"].Value;
                            dataRow4[2] = xnitem2.Attributes["Tag"].Value;
                            dataRow4[3] = Changenumtotype(new string[1] { xnitem2.Attributes["ValType"].Value })[0];
                            dataRow4[4] = xnitem2.Attributes["DefaultValue"].Value;
                            dataTable3.Rows.Add(dataRow4);
                        }
                    }
                    break;
                }
                dataRow4 = dataTable3.NewRow();
                if (!(type == "history") || !(xnitem.Attributes["ReservDB"].Value != "1"))
                {
                    dataRow4[0] = xnitem.Attributes["Name"].Value;
                    dataRow4[1] = xnitem.Attributes["id"].Value;
                    dataRow4[2] = xnitem.Attributes["Tag"].Value;
                    dataRow4[3] = Changenumtotype(new string[1] { xnitem.Attributes["ValType"].Value })[0];
                    dataRow4[4] = xnitem.Attributes["DefaultValue"].Value;
                    dataTable3.Rows.Add(dataRow4);
                }
            }
            dataGridView1.DataSource = dataTable3;
        }
        else
        {
            if (!doc.HasChildNodes)
            {
                return;
            }
            doc.SelectNodes("/DocumentRoot/Group[@id='" + e.Node.Name + "']/GroupItem");
            DataTable dataTable4 = new();
            dataTable4.Columns.Add("名称");
            dataTable4.Columns.Add("id");
            dataTable4.Columns.Add("注释");
            dataTable4.Columns.Add("类型");
            dataTable4.Columns.Add("默认值");
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
                            DataRow dataRow5 = dataTable4.NewRow();
                            dataRow5[0] = xnitem4.Attributes["Name"].Value;
                            dataRow5[1] = xnitem4.Attributes["id"].Value;
                            dataRow5[2] = xnitem4.Attributes["Tag"].Value;
                            dataRow5[3] = Changenumtotype(new string[1] { xnitem4.Attributes["ValType"].Value })[0];
                            dataRow5[4] = xnitem4.Attributes["DefaultValue"].Value;
                            dataTable4.Rows.Add(dataRow5);
                        }
                    }
                    break;
                }
                if (xnitem3.Attributes["GroupID"].Value == e.Node.Name)
                {
                    DataRow dataRow5 = dataTable4.NewRow();
                    if (!(type == "history") || !(xnitem3.Attributes["ReservDB"].Value != "1"))
                    {
                        dataRow5[0] = xnitem3.Attributes["Name"].Value;
                        dataRow5[1] = xnitem3.Attributes["id"].Value;
                        dataRow5[2] = xnitem3.Attributes["Tag"].Value;
                        dataRow5[3] = Changenumtotype(new string[1] { xnitem3.Attributes["ValType"].Value })[0];
                        dataRow5[4] = xnitem3.Attributes["DefaultValue"].Value;
                        dataTable4.Rows.Add(dataRow5);
                    }
                }
            }
            dataGridView1.DataSource = dataTable4;
        }
    }

    private void ReNewDevVars(string DevGroupID)
    {
        DataTable dataTable = new();
        dataTable.Columns.Add("名称");
        dataTable.Columns.Add("id");
        dataTable.Columns.Add("注释");
        dataTable.Columns.Add("类型");
        dataTable.Columns.Add("默认值");
        foreach (XmlNode xnitem in xnitems)
        {
            if (xnitem == null)
            {
                continue;
            }
            _ = xnitem.Attributes["GroupID"];
            if (xnitem.Attributes["GroupID"].Value == DevGroupID)
            {
                DataRow dataRow = dataTable.NewRow();
                if (!(type == "history") || !(xnitem.Attributes["ReservDB"].Value != "1"))
                {
                    dataRow[0] = xnitem.Attributes["Name"].Value;
                    dataRow[1] = xnitem.Attributes["id"].Value;
                    dataRow[2] = xnitem.Attributes["Tag"].Value;
                    dataRow[3] = Changenumtotype(new string[1] { xnitem.Attributes["ValType"].Value })[0];
                    dataRow[4] = xnitem.Attributes["DefaultValue"].Value;
                    dataTable.Rows.Add(dataRow);
                }
            }
        }
        dataGridView1.DataSource = dataTable;
    }

    public void RenewProjectIOs(string _GroupName)
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
        dataTable.Columns.Add("id");
        dataTable.Columns.Add("标签");
        dataTable.Columns.Add("备注");
        dataTable.Columns.Add("类型");
        dataTable.Columns.Add("访问");
        dataTable.Columns.Add("仿真");
        dataTable.Columns.Add("范围上限");
        dataTable.Columns.Add("范围下限");
        dataTable.Columns.Add("周期(ms)");
        dataTable.Columns.Add("延时(ms)");
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            if (!(projectIO.GroupName != _GroupName) && (!(type == "history") || projectIO.History))
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = projectIO.name;
                dataRow[1] = projectIO.ID;
                dataRow[2] = projectIO.tag;
                dataRow[3] = projectIO.description;
                dataRow[4] = Changenumtotype(new string[1] { projectIO.type })[0];
                dataRow[5] = projectIO.access;
                dataRow[6] = projectIO.emluator;
                dataRow[7] = projectIO.max;
                dataRow[8] = projectIO.min;
                dataRow[9] = projectIO.T;
                dataRow[10] = projectIO.delay;
                dataTable.Rows.Add(dataRow);
            }
        }
        dataGridView1.DataSource = dataTable;
    }

    public string[] Changetypetonum(string[] type)
    {
        string[] array = new string[type.Length];
        for (int i = 0; i < type.Length; i++)
        {
            array[i] = type[i] switch
            {
                "Boolean" => "0",
                "SByte" => "1",
                "Byte" => "2",
                "Int16" => "3",
                "UInt16" => "4",
                "Int32" => "5",
                "UInt32" => "6",
                "Single" => "7",
                "Double" => "8",
                "String" => "9",
                "IPAddress" => "10",
                "Enum" => "11",
                "Object" => "1024",
                _ => "1024",
            };
        }
        return array;
    }

    public string[] Changenumtotype(string[] num)
    {
        string[] array = new string[num.Length];
        for (int i = 0; i < num.Length; i++)
        {
            array[i] = num[i] switch
            {
                "0" => "Boolean",
                "1" => "SByte",
                "2" => "Byte",
                "3" => "Int16",
                "4" => "UInt16",
                "5" => "Int32",
                "6" => "UInt32",
                "7" => "Single",
                "8" => "Double",
                "9" => "String",
                "10" => "IPAddress",
                "11" => "Enum",
                "1024" => "Object",
                _ => "Object",
            };
        }
        return array;
    }

    private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                if (treeView1.SelectedNode.Name == "ProjectIO")
                {
                    base.Tag = new string[3]
                    {
                        "Globle." + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                        "-" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()
                    };
                    tagvalue = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["标签"].Value.ToString();
                }
                else if (treeView1.SelectedNode.Name == "设备参数子节点")
                {
                    base.Tag = new string[3]
                    {
                        "Globle.var" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString(),
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["名称"].Value.ToString(),
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString()
                    };
                }
                else if (treeView1.SelectedNode.Name.StartsWith("PageRoot") && treeView1.SelectedNode.Tag is CShape)
                {
                    base.Tag = new string[2]
                    {
                        "",
                        treeView1.SelectedNode.Name.Substring(9) + "." + dataGridView1.Rows[e.RowIndex].Cells["名称"].Value.ToString()
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
                    tagvalue = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["注释"].Value.ToString();
                }
                base.DialogResult = DialogResult.OK;
                Close();
                value = ((string[])base.Tag)[1];
            }
        }
        catch
        {
        }
    }

    private void VarTable_Load(object sender, EventArgs e)
    {
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
                    "Globle." + dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells["名称"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells["名称"].Value.ToString(),
                    "-" + dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells["id"].Value.ToString()
                };
                value = value + "|" + dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells["名称"].Value.ToString();
                tagvalue = tagvalue + "|" + dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells["标签"].Value.ToString();
            }
            tagvalue = tagvalue.Substring(1);
            value = value.Substring(1);
        }
        else if (treeView1.SelectedNode.Name == "设备参数子节点")
        {
            tagvalue = "";
            value = "";
            for (int j = 0; j < dataGridView1.SelectedRows.Count; j++)
            {
                base.Tag = new string[3]
                {
                    "Globle.var" + dataGridView1.Rows[dataGridView1.SelectedRows[j].Index].Cells["id"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedRows[j].Index].Cells["名称"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedRows[j].Index].Cells["id"].Value.ToString()
                };
                value = value + "|" + dataGridView1.Rows[dataGridView1.SelectedRows[j].Index].Cells["名称"].Value.ToString();
            }
            value = value.Substring(1);
        }
        else if (treeView1.SelectedNode.Name.StartsWith("PageRoot") && treeView1.SelectedNode.Tag is CShape)
        {
            base.Tag = new string[2]
            {
                "",
                treeView1.SelectedNode.Name.Substring(9) + "." + dataGridView1.SelectedRows[0].Cells["名称"].Value.ToString()
            };
            value = ((string[])base.Tag)[1];
        }
        else
        {
            tagvalue = "";
            value = "";
            for (int k = 0; k < dataGridView1.SelectedRows.Count; k++)
            {
                base.Tag = new string[3]
                {
                    "Globle.var" + dataGridView1.Rows[dataGridView1.SelectedRows[k].Index].Cells["id"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedRows[k].Index].Cells["名称"].Value.ToString(),
                    dataGridView1.Rows[dataGridView1.SelectedRows[k].Index].Cells["id"].Value.ToString()
                };
                value = value + "|" + dataGridView1.Rows[dataGridView1.SelectedRows[k].Index].Cells["名称"].Value.ToString();
                tagvalue = tagvalue + "|" + dataGridView1.Rows[dataGridView1.SelectedRows[k].Index].Cells["注释"].Value.ToString();
            }
            tagvalue = tagvalue.Substring(1);
            value = value.Substring(1);
        }
        DialogResult = DialogResult.OK;
        Close();
    }

    private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && dataGridView1.SelectedRows.Count != 0)
        {
            contextMenuStrip1.Show((Control)sender, e.Location);
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
            return;

        if (_strText == "")
        {
            _strText = textBox1.Text;
            if (_iIndex == 0)
            {
                goto IL_0084;
            }
        }
        else if (!(_strText == textBox1.Text))
        {
            _iIndex = 0;
            _iFindIndex = 0;
            _strText = textBox1.Text;
            goto IL_0084;
        }
        goto IL_01c6;
    IL_01c6:
        if (_iFindIndex == 0)
        {
            _iIndex = 0;
            MessageBox.Show("找不到" + textBox1.Text + "!");
            return;
        }
        for (int i = _iIndex; i < dataGridView1.Rows.Count; i++)
        {
            if (dataGridView1.Rows[i].Cells[0].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                _iIndex = i + 1;
                _iFindIndex--;
                break;
            }
        }
        return;
    IL_0084:
        int num = 0;
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells[0].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
            {
                num++;
            }
        }
        _iFindIndex = num;
        if (num == 0)
        {
            MessageBox.Show("找不到" + textBox1.Text + "!");
        }
        if (num == 1)
        {
            foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
            {
                if (item2.Cells[0].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    item2.Selected = true;
                    dataGridView1.CurrentCell = item2.Cells[0];
                    break;
                }
            }
        }
        if (num <= 1)
        {
            return;
        }
        goto IL_01c6;
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
            return;

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
        RenewProjectIOs(pio.GroupName);
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells["名称"].Value.ToString() == pio.name)
            {
                dataGridView1.CurrentCell = item.Cells[0];
            }
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
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
        if (projectIOEditForm2.ShowDialog() == DialogResult.OK)
        {
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
            }
        }
        RenewProjectIOs(projectIO.GroupName);
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
        yytjForm yytjForm2 = new(CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.dfs, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs);
        DataRow dataRow = yytjForm2.FindInUse(projectIO.name);
        if (dataRow != null && MessageBox.Show("变量仍然被" + dataRow["画面"].ToString() + "引用,是否强制删除?", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            return;
        }
        dhp.ProjectIOs.Remove(projectIO);
        RenewProjectIOs(treeView1.SelectedNode.Text);
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
        }
    }

    private void 导入变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.HMIPath == "")
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
                    Environment.CurrentDirectory = CEditEnvironmentGlobal.HMIPath;
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
                    RenewProjectIOs(dhp.ProjectIOTreeRoot.Text);
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
                    RenewProjectIOs(dhp.ProjectIOTreeRoot.Text);
                }
            }
            CEditEnvironmentGlobal.varFileNeedReLoad = true;
        }
        catch (Exception)
        {
            MessageBox.Show("导入文件无法被识别,请检查导入文件");
        }
    }

    private void 导出内部变量ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!(CEditEnvironmentGlobal.HMIPath == ""))
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
            RenewProjectIOs(copypio.GroupName);
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
        RenewProjectIOs(projectIO.GroupName);
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if (item.Cells["名称"].Value.ToString() == projectIO.name)
            {
                dataGridView1.CurrentCell = item.Cells[0];
            }
        }
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

    private void Button2_Click(object sender, EventArgs e)
    {
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            if (projectIO.name == textBox1.Text)
            {
                _iCount++;
                ListFind.Add(projectIO);
            }
        }
        if (_iCount == 1)
        {
            RenewProjectIOs(ListFind[0].GroupName);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(textBox1.Text))
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                    _iCount = 0;
                    ListFind.Clear();
                    break;
                }
            }
        }
        string devGroupID = "";
        XmlNodeList xmlNodeList = doc.SelectNodes("//Item");
        if (xmlNodeList.Count != 0)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                if (((XmlElement)item).GetAttribute("Name").ToString() == textBox1.Text.ToString())
                {
                    _iCount1++;
                    devGroupID = ((XmlElement)item).GetAttribute("GroupID").ToString();
                }
            }
        }
        if (_iCount1 != 1)
        {
            return;
        }
        ReNewDevVars(devGroupID);
        for (int j = 0; j < dataGridView1.Rows.Count; j++)
        {
            if (dataGridView1.Rows[j].Cells[1].Value.ToString().Contains(textBox1.Text))
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[j].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[0];
                _iCount1 = 0;
                break;
            }
        }
    }

    private void VarTable_FormClosing(object sender, FormClosingEventArgs e)
    {
        CheckIOExists.IOTableOld = true;
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
        button1 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        panel1 = new System.Windows.Forms.Panel();
        button2 = new System.Windows.Forms.Button();
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
        组操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        添加变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        编辑变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        删除变量组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolTip1 = new System.Windows.Forms.ToolTip(components);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        contextMenuStrip1.SuspendLayout();
        panel1.SuspendLayout();
        menuStrip1.SuspendLayout();
        base.SuspendLayout();
        treeView1.Dock = System.Windows.Forms.DockStyle.Left;
        treeView1.Location = new System.Drawing.Point(0, 0);
        treeView1.Name = "treeView1";
        treeView1.Size = new System.Drawing.Size(217, 531);
        treeView1.TabIndex = 0;
        treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(TreeView1_AfterSelect);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToResizeRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new System.Drawing.Point(217, 65);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(785, 466);
        dataGridView1.TabIndex = 1;
        dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);
        dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(DataGridView1_MouseClick);
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { 添加ToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
        添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
        添加ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
        添加ToolStripMenuItem.Text = "添加";
        添加ToolStripMenuItem.Click += new System.EventHandler(添加ToolStripMenuItem_Click);
        button1.Location = new System.Drawing.Point(427, 37);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 2;
        button1.Text = "局部查询";
        toolTip1.SetToolTip(button1, "提供对当前组的变量查询，支持模糊查询");
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(223, 40);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(63, 14);
        label1.TabIndex = 3;
        label1.Text = "变 量 名：";
        textBox1.Location = new System.Drawing.Point(287, 37);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(132, 22);
        textBox1.TabIndex = 4;
        toolTip1.SetToolTip(textBox1, "请输入要查询的变量名");
        panel1.Controls.Add(button2);
        panel1.Location = new System.Drawing.Point(217, 27);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(773, 38);
        panel1.TabIndex = 5;
        button2.Location = new System.Drawing.Point(291, 10);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 0;
        button2.Text = "全局查询";
        toolTip1.SetToolTip(button2, "提供对所有变量的查询，不支持模糊查询");
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { 变量操作ToolStripMenuItem, 组操作ToolStripMenuItem });
        menuStrip1.Location = new System.Drawing.Point(217, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
        menuStrip1.Size = new System.Drawing.Size(785, 24);
        menuStrip1.TabIndex = 6;
        menuStrip1.Text = "menuStrip1";
        变量操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[11]
        {
            添加内部变量ToolStripMenuItem, 修改内部变量ToolStripMenuItem, 删除内部变量ToolStripMenuItem, toolStripSeparator3, 导入变量ToolStripMenuItem, 导出内部变量ToolStripMenuItem, toolStripSeparator1, 复制ToolStripMenuItem, 剪切ToolStripMenuItem, 战列ToolStripMenuItem,
            toolStripSeparator2
        });
        变量操作ToolStripMenuItem.Name = "变量操作ToolStripMenuItem";
        变量操作ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
        变量操作ToolStripMenuItem.Text = "变量操作";
        添加内部变量ToolStripMenuItem.Name = "添加内部变量ToolStripMenuItem";
        添加内部变量ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        添加内部变量ToolStripMenuItem.Text = "添加内部变量";
        添加内部变量ToolStripMenuItem.Click += new System.EventHandler(添加内部变量ToolStripMenuItem_Click);
        修改内部变量ToolStripMenuItem.Name = "修改内部变量ToolStripMenuItem";
        修改内部变量ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        修改内部变量ToolStripMenuItem.Text = "编辑内部变量";
        修改内部变量ToolStripMenuItem.Click += new System.EventHandler(修改内部变量ToolStripMenuItem_Click);
        删除内部变量ToolStripMenuItem.Name = "删除内部变量ToolStripMenuItem";
        删除内部变量ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
        删除内部变量ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        删除内部变量ToolStripMenuItem.Text = "删除内部变量";
        删除内部变量ToolStripMenuItem.Click += new System.EventHandler(删除内部变量ToolStripMenuItem_Click);
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
        导入变量ToolStripMenuItem.Name = "导入变量ToolStripMenuItem";
        导入变量ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        导入变量ToolStripMenuItem.Text = "导入内部变量";
        导入变量ToolStripMenuItem.Click += new System.EventHandler(导入变量ToolStripMenuItem_Click);
        导出内部变量ToolStripMenuItem.Name = "导出内部变量ToolStripMenuItem";
        导出内部变量ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        导出内部变量ToolStripMenuItem.Text = "导出内部变量";
        导出内部变量ToolStripMenuItem.Click += new System.EventHandler(导出内部变量ToolStripMenuItem_Click);
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
        复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
        复制ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
        复制ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        复制ToolStripMenuItem.Text = "复制";
        复制ToolStripMenuItem.Click += new System.EventHandler(复制ToolStripMenuItem_Click);
        剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
        剪切ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.X | System.Windows.Forms.Keys.Control;
        剪切ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        剪切ToolStripMenuItem.Text = "剪切";
        剪切ToolStripMenuItem.Click += new System.EventHandler(剪切ToolStripMenuItem_Click);
        战列ToolStripMenuItem.Name = "战列ToolStripMenuItem";
        战列ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control;
        战列ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
        战列ToolStripMenuItem.Text = "粘贴";
        战列ToolStripMenuItem.Click += new System.EventHandler(战列ToolStripMenuItem_Click);
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
        组操作ToolStripMenuItem.Checked = true;
        组操作ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
        组操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { 添加变量组ToolStripMenuItem, 编辑变量组ToolStripMenuItem, 删除变量组ToolStripMenuItem });
        组操作ToolStripMenuItem.Enabled = false;
        组操作ToolStripMenuItem.Name = "组操作ToolStripMenuItem";
        组操作ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
        组操作ToolStripMenuItem.Text = "组操作";
        添加变量组ToolStripMenuItem.Name = "添加变量组ToolStripMenuItem";
        添加变量组ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
        添加变量组ToolStripMenuItem.Text = "添加变量组";
        添加变量组ToolStripMenuItem.Click += new System.EventHandler(添加变量组ToolStripMenuItem_Click);
        编辑变量组ToolStripMenuItem.Name = "编辑变量组ToolStripMenuItem";
        编辑变量组ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
        编辑变量组ToolStripMenuItem.Text = "编辑变量组";
        编辑变量组ToolStripMenuItem.Click += new System.EventHandler(编辑变量组ToolStripMenuItem_Click);
        删除变量组ToolStripMenuItem.Name = "删除变量组ToolStripMenuItem";
        删除变量组ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
        删除变量组ToolStripMenuItem.Text = "删除变量组";
        删除变量组ToolStripMenuItem.Click += new System.EventHandler(删除变量组ToolStripMenuItem_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(1002, 531);
        base.Controls.Add(menuStrip1);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(dataGridView1);
        base.Controls.Add(button1);
        base.Controls.Add(panel1);
        base.Controls.Add(treeView1);
        base.Name = "VarTable";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "参数绑定";
        base.Load += new System.EventHandler(VarTable_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(VarTable_FormClosing);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        contextMenuStrip1.ResumeLayout(false);
        panel1.ResumeLayout(false);
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
