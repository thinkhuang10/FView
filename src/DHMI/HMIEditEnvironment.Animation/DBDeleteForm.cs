using ShapeRuntime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment.Animation;

public class DBDeleteForm : Form
{
    public byte[] OtherData;

    public string ResultSQL = "";

    public string ResultTo = "";

    public bool Ansync;

    private List<string> ListboxContentstr = new();

    private DataTable DgDatatable = new();

    private readonly Dictionary<string, DataFile> dicds = new();

    private Label label1;

    private Label label2;

    private Label label3;

    private TreeView treeView1;

    private Button btn_addtable;

    private Button btn_removetable;

    private Panel panel1;

    private Label label5;

    private TextBox textBox1;

    private Label label7;

    private Button button8;

    private Button button9;

    private ListBox listBox1;

    private Button btn_setresult;

    private DataGridView dataGridView1;

    private ComboBox cbx_control;

    private ComboBox cbx_page;

    public Label label4;

    private CheckBox checkBox1;

    private DataGridViewCheckBoxColumn Column1;

    private DataGridViewComboBoxColumn Column5;

    private DataGridViewTextBoxColumn Column2;

    private DataGridViewComboBoxColumn Column6;

    private DataGridViewComboBoxColumn Column3;

    private DataGridViewTextBoxColumn col_page;

    private DataGridViewTextBoxColumn col_control;

    private DataGridViewTextBoxColumn Column4;

    private Button btn_alladd;

    private Button btn_allremove;

    public DBDeleteForm()
    {
        InitializeComponent();
        DgDatatable.Columns.Add("tbname");
        DgDatatable.Columns.Add("ck1");
        DgDatatable.Columns.Add("andor");
        DgDatatable.Columns.Add("varname");
        DgDatatable.Columns.Add("tiaojian");
        DgDatatable.Columns.Add("value1");
        DgDatatable.Columns.Add("value2");
        DgDatatable.Columns.Add("col_page");
        DgDatatable.Columns.Add("col_control");
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        DBSelectSerializeCopy dBSelectSerializeCopy = new()
        {
            ansync = Ansync,
            ListboxContentstr = ListboxContentstr,
            DgDatatable = DgDatatable
        };
        formatter.Serialize(memoryStream, dBSelectSerializeCopy);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public void DeSerialize(byte[] bt)
    {
        if (bt != null)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(bt);
            DBSelectSerializeCopy dBSelectSerializeCopy = (DBSelectSerializeCopy)formatter.Deserialize(stream);
            stream.Close();
            ListboxContentstr = dBSelectSerializeCopy.ListboxContentstr;
            DgDatatable = dBSelectSerializeCopy.DgDatatable;
            Ansync = dBSelectSerializeCopy.ansync;
        }
    }

    private void DBSelectForm_Load(object sender, EventArgs e)
    {
        try
        {
            treeView1.Nodes.Clear();
            TreeNode treeNode = treeView1.Nodes.Add("数据库");
            try
            {
                if (DBOperationGlobal.conn.State != ConnectionState.Open)
                {
                    DBOperationGlobal.conn.Open();
                }
            }
            catch
            {
                MessageBox.Show("连接数据库失败，请设置数据库连接！", "提示");
                DBOperationGlobal.effect = false;
                Close();
                return;
            }
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                cbx_page.Items.Add("{" + df.name + "}");
                dicds.Add(df.name, df);
            }
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            List<string> list = new();
            using (DbDataReader dbDataReader = DBOperationGlobal.command.ExecuteReader())
            {
                while (dbDataReader.Read())
                {
                    list.Add(dbDataReader.GetString(0));
                }
            }
            list.Sort();
            foreach (string item in list)
            {
                treeNode.Nodes.Add(item);
            }
            treeView1.ExpandAll();
        }
        catch
        {
            MessageBox.Show("连接数据库失败，请设置数据库连接！", "提示");
            Close();
            return;
        }
        cbx_page.Items.Add("{常量}");
        cbx_page.Items.Add("{内部变量}");
        Column3.Items.Add("{常量}");
        Column3.Items.Add("{权限管理字段}");
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            Column3.Items.Add("{[" + projectIO.name + "]}");
        }
        XmlNodeList xmlNodeList = CEditEnvironmentGlobal.xmldoc.SelectNodes("/DocumentRoot/Item");
        foreach (XmlNode item2 in xmlNodeList)
        {
            Column3.Items.Add("{[" + item2.Attributes["Name"].Value + "]}");
        }
        foreach (DataFile df2 in CEditEnvironmentGlobal.dfs)
        {
            foreach (CShape item3 in df2.ListAllShowCShape)
            {
                if (item3 is CControl && (((CControl)item3)._c is CDataGridView || ((CControl)item3)._c is CButton || ((CControl)item3)._c is CDateTimePicker || ((CControl)item3)._c is CTextBox || ((CControl)item3)._c is CLabel || ((CControl)item3)._c is CComboBox || ((CControl)item3)._c is CListBox))
                {
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + "}");
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + ".Tag}");
                }
                else if (item3 is CControl && ((CControl)item3)._c is CCheckBox)
                {
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + "}");
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + ".Value}");
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + ".Tag}");
                }
                else if (item3 is CString)
                {
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + "}");
                    Column3.Items.Add("{" + df2.name + "." + item3.Name + ".Tag}");
                }
            }
        }
        Column5.Items.Add("And");
        Column5.Items.Add("Or");
        Column6.Items.Add("=");
        Column6.Items.Add("<>");
        Column6.Items.Add(">");
        Column6.Items.Add("<");
        Column6.Items.Add(">=");
        Column6.Items.Add("<=");
        Column6.Items.Add("like");
        try
        {
            DeSerialize(OtherData);
            checkBox1.Checked = Ansync;
            if (ListboxContentstr.Count != 0)
            {
                foreach (string item4 in ListboxContentstr)
                {
                    listBox1.Items.Add(item4);
                }
            }
            textBox1.Text = ResultSQL;
        }
        catch
        {
        }
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Text != "数据库") || listBox1.Items.Contains(treeView1.SelectedNode.Text))
        {
            return;
        }
        listBox1.Items.Add(treeView1.SelectedNode.Text);
        string text = treeView1.SelectedNode.Text;
        if (DBOperationGlobal.conn.State != ConnectionState.Open)
        {
            DBOperationGlobal.conn.Open();
        }
        DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
        DataSet dataSet = new();
        DBOperationGlobal.adapter.Fill(dataSet);
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            DataRow dataRow2 = DgDatatable.NewRow();
            dataRow2["tbname"] = text;
            dataRow2["ck1"] = false.ToString();
            dataRow2["andor"] = "And".ToString();
            dataRow2["varname"] = row["COLUMN_NAME"].ToString();
            dataRow2["tiaojian"] = "=".ToString();
            dataRow2["value1"] = "{常量}";
            dataRow2["value2"] = "";
            dataRow2["col_page"] = "";
            dataRow2["col_control"] = "";
            DgDatatable.Rows.Add(dataRow2);
        }
    }

    private void btn_removetable_Click(object sender, EventArgs e)
    {
        List<object> list = new();
        foreach (object selectedItem in listBox1.SelectedItems)
        {
            list.Add(selectedItem);
        }
        foreach (object item in list)
        {
            listBox1.Items.Remove(item);
            string text = item.ToString();
            DataRow[] array = DgDatatable.Select("tbname='" + text + "'");
            DataRow[] array2 = array;
            foreach (DataRow row in array2)
            {
                DgDatatable.Rows.Remove(row);
            }
        }
        if (listBox1.Items.Count == 0)
        {
            dataGridView1.Rows.Clear();
        }
        else
        {
            listBox1.SelectedIndex = 0;
        }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox1.SelectedItem == null)
        {
            return;
        }
        string text = listBox1.SelectedItem.ToString();
        DataRow[] array = DgDatatable.Select("tbname = '" + text + "'");
        dataGridView1.Rows.Clear();
        if (!DgDatatable.Columns.Contains("col_page"))
        {
            DgDatatable.Columns.Add("col_page");
            DgDatatable.Columns.Add("col_control");
            foreach (DataRow row in DgDatatable.Rows)
            {
                if (row["value1"].ToString() != "{常量}")
                {
                    if (row["value1"].ToString().IndexOf('.') != -1)
                    {
                        row["col_page"] = "{" + row["value1"].ToString().Substring(1, row["value1"].ToString().Length - 2).Substring(0, row["value1"].ToString().Substring(1, row["value1"].ToString().Length - 2).IndexOf('.')) + "}";
                        row["col_control"] = row["value1"];
                    }
                    else
                    {
                        row["col_page"] = "{内部变量}";
                        row["col_control"] = row["value1"];
                    }
                }
                else
                {
                    row["col_page"] = "{常量}";
                    row["col_control"] = "";
                }
            }
        }
        DataRow[] array2 = array;
        foreach (DataRow dataRow2 in array2)
        {
            dataGridView1.Rows.Add(Convert.ToBoolean(dataRow2[1]), dataRow2[2].ToString(), dataRow2[3].ToString(), dataRow2[4].ToString(), dataRow2["value1"].ToString(), dataRow2["col_page"].ToString(), dataRow2["col_control"].ToString(), dataRow2["value2"].ToString());
        }
    }

    private void textBox1_DoubleClick(object sender, EventArgs e)
    {
        textBox1.ReadOnly = false;
    }

    private void btn_setresult_Click(object sender, EventArgs e)
    {
        string text = "";
        foreach (object item in listBox1.Items)
        {
            text = text + "delete from " + item.ToString();
            text += " where 1=1";
            DataRow[] array = DgDatatable.Select("tbname='" + item.ToString() + "'");
            DataRow[] array2 = array;
            foreach (DataRow dataRow in array2)
            {
                if (!Convert.ToBoolean(dataRow[1]) || !Convert.ToBoolean(dataRow[1]))
                {
                    continue;
                }
                if (dataRow["col_page"].ToString() != "{常量}")
                {
                    if (dataRow["tiaojian"].ToString() != "like")
                    {
                        string text2 = text;
                        text = text2 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '" + dataRow["col_control"].ToString() + "'";
                    }
                    else
                    {
                        string text3 = text;
                        text = text3 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '%" + dataRow["col_control"].ToString() + "%'";
                    }
                }
                else if (dataRow["tiaojian"].ToString() != "like")
                {
                    string text4 = text;
                    text = text4 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '" + dataRow["value2"].ToString() + "'";
                }
                else
                {
                    string text5 = text;
                    text = text5 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '%" + dataRow["value2"].ToString() + "%'";
                }
            }
            text += Environment.NewLine;
        }
        textBox1.Text = text;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        ListboxContentstr = new List<string>();
        foreach (object item in listBox1.Items)
        {
            ListboxContentstr.Add(item.ToString());
        }
        OtherData = Serialize();
        ResultSQL = textBox1.Text;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button9_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (listBox1.SelectedItem == null)
        {
            return;
        }
        string text = listBox1.SelectedItem.ToString();
        DataRow[] array = DgDatatable.Select("tbname='" + text + "'");
        DataRow[] array2 = array;
        foreach (DataRow row in array2)
        {
            DgDatatable.Rows.Remove(row);
        }
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            DataRow dataRow = DgDatatable.NewRow();
            dataRow["tbname"] = text.ToString();
            dataRow["ck1"] = ((bool)item.Cells[0].Value).ToString();
            dataRow["andor"] = ((item.Cells[1].Value != null) ? item.Cells[1].Value.ToString() : "");
            dataRow["varname"] = ((item.Cells[2].Value != null) ? item.Cells[2].Value.ToString() : "");
            dataRow["tiaojian"] = ((item.Cells[3].Value != null) ? item.Cells[3].Value.ToString() : "");
            dataRow["value1"] = ((item.Cells[4].Value != null) ? item.Cells[4].Value.ToString() : "");
            dataRow["value2"] = ((item.Cells["Column4"].Value == null) ? "" : item.Cells["Column4"].Value.ToString());
            dataRow["col_page"] = ((item.Cells["col_page"].Value != null) ? item.Cells["col_page"].Value.ToString() : "");
            dataRow["col_control"] = ((item.Cells["col_control"].Value != null) ? item.Cells["col_control"].Value.ToString() : "");
            DgDatatable.Rows.Add(dataRow);
        }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        Ansync = checkBox1.Checked;
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        int num = base.Location.X;
        int num2 = base.Location.Y;
        int num3 = panel1.Location.X;
        int num4 = panel1.Location.Y;
        int num5 = dataGridView1.Location.X;
        int num6 = dataGridView1.Location.Y;
        int num7 = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: false).X;
        int num8 = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: false).Y;
        if (e.ColumnIndex == 5)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            cbx_page.Visible = true;
            cbx_page.Location = new Point(num7 + num5 + num3 + num, num2 + num8 + num6 + num4);
            cbx_page.Width = dataGridView1.Columns[5].Width;
            cbx_page.DroppedDown = true;
        }
        if (e.ColumnIndex != 6 || e.RowIndex == -1 || !(dataGridView1.Rows[e.RowIndex].Cells["col_page"].Value.ToString() != ""))
        {
            return;
        }
        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString() != "" && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString() != "{常量}" && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString() != "{权限管理字段}" && cbx_page.Items.Contains(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
        {
            cbx_page.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
        }
        cbx_control.Visible = true;
        int num9 = cbx_control.Width;
        foreach (object item in cbx_control.Items)
        {
            if (TextRenderer.MeasureText(item.ToString(), cbx_control.Font).Width > num9)
            {
                num9 = TextRenderer.MeasureText(item.ToString(), cbx_control.Font).Width;
            }
        }
        cbx_control.DropDownWidth = num9;
        cbx_control.Location = new Point(num7 + num5 + num3 + num, num2 + num8 + num6 + num4);
        cbx_control.Width = dataGridView1.Columns[6].Width;
        cbx_control.DroppedDown = true;
    }

    private void cbx_control_SelectedIndexChanged(object sender, EventArgs e)
    {
        dataGridView1.SelectedCells[0].Value = cbx_control.SelectedItem.ToString();
        cbx_control.Visible = false;
    }

    private void cbx_page_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbx_control.Items.Clear();
            if (cbx_page.Text != "{内部变量}" && cbx_page.Text != "{常量}")
            {
                DataFile dataFile = dicds[cbx_page.Text.Substring(1, cbx_page.Text.Length - 2)];
                foreach (CShape item in dataFile.ListAllShowCShape)
                {
                    if (item is CControl && (((CControl)item)._c is CDataGridView || ((CControl)item)._c is CButton || ((CControl)item)._c is CDateTimePicker || ((CControl)item)._c is CTextBox || ((CControl)item)._c is CLabel || ((CControl)item)._c is CComboBox || ((CControl)item)._c is CListBox))
                    {
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + "}");
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + ".Tag}");
                    }
                    else if (item is CControl && ((CControl)item)._c is CCheckBox)
                    {
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + "}");
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + ".Value}");
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + ".Tag}");
                    }
                    else if (item is CString)
                    {
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + "}");
                        cbx_control.Items.Add("{" + dataFile.name + "." + item.Name + ".Tag}");
                    }
                }
            }
            else if (cbx_page.Text == "{常量}")
            {
                cbx_control.Items.Clear();
            }
            else
            {
                cbx_control.Items.Add("{权限管理字段}");
                foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
                {
                    cbx_control.Items.Add("{[" + projectIO.name + "]}");
                }
                XmlNodeList xmlNodeList = CEditEnvironmentGlobal.xmldoc.SelectNodes("/DocumentRoot/Item");
                foreach (XmlNode item2 in xmlNodeList)
                {
                    cbx_control.Items.Add("{[" + item2.Attributes["Name"].Value + "]}");
                }
            }
            if (dataGridView1.SelectedCells.Count == 1 && dataGridView1.SelectedCells[0].ColumnIndex == 5)
            {
                dataGridView1.SelectedCells[0].Value = cbx_page.SelectedItem.ToString();
                cbx_page.Visible = false;
                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[dataGridView1.SelectedCells[0].ColumnIndex + 1].Value = "";
            }
        }
        catch
        {
        }
    }

    private void treeView1_DoubleClick(object sender, EventArgs e)
    {
        btnadd_Click(sender, e);
    }

    private void btn_alladd_Click(object sender, EventArgs e)
    {
        if (treeView1.Nodes.Count == 0)
        {
            return;
        }
        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
        {
            if (listBox1.Items.Contains(node.Text))
            {
                continue;
            }
            listBox1.Items.Add(node.Text);
            string text = node.Text;
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            DataSet dataSet = new();
            DBOperationGlobal.adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                DataRow dataRow2 = DgDatatable.NewRow();
                dataRow2["tbname"] = text;
                dataRow2["ck1"] = false.ToString();
                dataRow2["andor"] = "And".ToString();
                dataRow2["varname"] = row["COLUMN_NAME"].ToString();
                dataRow2["tiaojian"] = "=".ToString();
                dataRow2["value1"] = "{常量}";
                dataRow2["value2"] = "";
                dataRow2["col_page"] = "";
                dataRow2["col_control"] = "";
                DgDatatable.Rows.Add(dataRow2);
            }
        }
    }

    private void btn_allremove_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        DgDatatable.Rows.Clear();
        dataGridView1.Rows.Clear();
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        cbx_control = new System.Windows.Forms.ComboBox();
        cbx_page = new System.Windows.Forms.ComboBox();
        treeView1 = new System.Windows.Forms.TreeView();
        btn_addtable = new System.Windows.Forms.Button();
        btn_removetable = new System.Windows.Forms.Button();
        panel1 = new System.Windows.Forms.Panel();
        checkBox1 = new System.Windows.Forms.CheckBox();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        col_page = new System.Windows.Forms.DataGridViewTextBoxColumn();
        col_control = new System.Windows.Forms.DataGridViewTextBoxColumn();
        Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        listBox1 = new System.Windows.Forms.ListBox();
        textBox1 = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        btn_setresult = new System.Windows.Forms.Button();
        button8 = new System.Windows.Forms.Button();
        button9 = new System.Windows.Forms.Button();
        btn_alladd = new System.Windows.Forms.Button();
        btn_allremove = new System.Windows.Forms.Button();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(13, 13);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 0;
        label1.Text = "当前操作:";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(78, 13);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(29, 12);
        label2.TabIndex = 0;
        label2.Text = "删除";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(146, 13);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(59, 12);
        label3.TabIndex = 0;
        label3.Text = "当前控件:";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(211, 13);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(71, 12);
        label4.TabIndex = 0;
        label4.Text = "ControlName";
        cbx_control.FormattingEnabled = true;
        cbx_control.Location = new System.Drawing.Point(213, 209);
        cbx_control.Name = "cbx_control";
        cbx_control.Size = new System.Drawing.Size(121, 20);
        cbx_control.TabIndex = 13;
        cbx_control.Visible = false;
        cbx_control.SelectedIndexChanged += new System.EventHandler(cbx_control_SelectedIndexChanged);
        cbx_page.FormattingEnabled = true;
        cbx_page.Location = new System.Drawing.Point(213, 183);
        cbx_page.Name = "cbx_page";
        cbx_page.Size = new System.Drawing.Size(121, 20);
        cbx_page.TabIndex = 12;
        cbx_page.Visible = false;
        cbx_page.SelectedIndexChanged += new System.EventHandler(cbx_page_SelectedIndexChanged);
        treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        treeView1.Location = new System.Drawing.Point(15, 40);
        treeView1.Name = "treeView1";
        treeView1.Size = new System.Drawing.Size(190, 446);
        treeView1.TabIndex = 0;
        treeView1.DoubleClick += new System.EventHandler(treeView1_DoubleClick);
        btn_addtable.Location = new System.Drawing.Point(213, 54);
        btn_addtable.Name = "btn_addtable";
        btn_addtable.Size = new System.Drawing.Size(75, 23);
        btn_addtable.TabIndex = 1;
        btn_addtable.Text = "添加表";
        btn_addtable.UseVisualStyleBackColor = true;
        btn_addtable.Click += new System.EventHandler(btnadd_Click);
        btn_removetable.Location = new System.Drawing.Point(213, 83);
        btn_removetable.Name = "btn_removetable";
        btn_removetable.Size = new System.Drawing.Size(75, 23);
        btn_removetable.TabIndex = 2;
        btn_removetable.Text = "移除表";
        btn_removetable.UseVisualStyleBackColor = true;
        btn_removetable.Click += new System.EventHandler(btn_removetable_Click);
        panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        panel1.Controls.Add(checkBox1);
        panel1.Controls.Add(dataGridView1);
        panel1.Controls.Add(listBox1);
        panel1.Controls.Add(textBox1);
        panel1.Controls.Add(label7);
        panel1.Controls.Add(label5);
        panel1.Controls.Add(btn_setresult);
        panel1.Location = new System.Drawing.Point(294, 12);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(627, 446);
        panel1.TabIndex = 3;
        checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(550, 12);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(72, 16);
        checkBox1.TabIndex = 6;
        checkBox1.Text = "异步操作";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToResizeRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(Column1, Column5, Column2, Column6, Column3, col_page, col_control, Column4);
        dataGridView1.Location = new System.Drawing.Point(5, 86);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.Size = new System.Drawing.Size(617, 245);
        dataGridView1.TabIndex = 1;
        dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
        dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        Column1.HeaderText = "";
        Column1.Name = "Column1";
        Column1.Width = 25;
        Column5.HeaderText = "逻辑";
        Column5.Name = "Column5";
        Column2.HeaderText = "字段";
        Column2.Name = "Column2";
        Column2.ReadOnly = true;
        Column2.Width = 80;
        Column6.HeaderText = "条件";
        Column6.Name = "Column6";
        Column3.HeaderText = "类型";
        Column3.Name = "Column3";
        Column3.Visible = false;
        col_page.HeaderText = "页面";
        col_page.Name = "col_page";
        col_page.ReadOnly = true;
        col_page.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        col_control.HeaderText = "控件";
        col_control.Name = "col_control";
        col_control.ReadOnly = true;
        col_control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        col_control.Width = 150;
        Column4.HeaderText = "值";
        Column4.Name = "Column4";
        Column4.Width = 45;
        listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 12;
        listBox1.Location = new System.Drawing.Point(5, 28);
        listBox1.Name = "listBox1";
        listBox1.Size = new System.Drawing.Size(617, 52);
        listBox1.TabIndex = 0;
        listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
        textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox1.Location = new System.Drawing.Point(62, 337);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.ReadOnly = true;
        textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        textBox1.Size = new System.Drawing.Size(560, 104);
        textBox1.TabIndex = 3;
        textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
        label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(3, 337);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 4;
        label7.Text = "命令预览";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(3, 13);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(41, 12);
        label5.TabIndex = 0;
        label5.Text = "已选表";
        btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        btn_setresult.Location = new System.Drawing.Point(5, 357);
        btn_setresult.Name = "btn_setresult";
        btn_setresult.Size = new System.Drawing.Size(51, 23);
        btn_setresult.TabIndex = 2;
        btn_setresult.Text = "生成";
        btn_setresult.UseVisualStyleBackColor = true;
        btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
        button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button8.Location = new System.Drawing.Point(765, 464);
        button8.Name = "button8";
        button8.Size = new System.Drawing.Size(75, 23);
        button8.TabIndex = 4;
        button8.Text = "确定";
        button8.UseVisualStyleBackColor = true;
        button8.Click += new System.EventHandler(button8_Click);
        button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button9.Location = new System.Drawing.Point(846, 464);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(75, 23);
        button9.TabIndex = 5;
        button9.Text = "取消";
        button9.UseVisualStyleBackColor = true;
        button9.Click += new System.EventHandler(button9_Click);
        btn_alladd.Location = new System.Drawing.Point(213, 124);
        btn_alladd.Name = "btn_alladd";
        btn_alladd.Size = new System.Drawing.Size(75, 23);
        btn_alladd.TabIndex = 7;
        btn_alladd.Text = "全选表";
        btn_alladd.UseVisualStyleBackColor = true;
        btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
        btn_allremove.Location = new System.Drawing.Point(213, 162);
        btn_allremove.Name = "btn_allremove";
        btn_allremove.Size = new System.Drawing.Size(75, 23);
        btn_allremove.TabIndex = 8;
        btn_allremove.Text = "全移除";
        btn_allremove.UseVisualStyleBackColor = true;
        btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(933, 498);
        base.Controls.Add(btn_allremove);
        base.Controls.Add(btn_alladd);
        base.Controls.Add(panel1);
        base.Controls.Add(btn_removetable);
        base.Controls.Add(btn_addtable);
        base.Controls.Add(treeView1);
        base.Controls.Add(label2);
        base.Controls.Add(label4);
        base.Controls.Add(label3);
        base.Controls.Add(button9);
        base.Controls.Add(button8);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "DBDeleteForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "删除数据";
        base.Load += new System.EventHandler(DBSelectForm_Load);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
