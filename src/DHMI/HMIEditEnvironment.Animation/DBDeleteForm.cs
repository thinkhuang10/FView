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
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.cbx_control = new System.Windows.Forms.ComboBox();
        this.cbx_page = new System.Windows.Forms.ComboBox();
        this.treeView1 = new System.Windows.Forms.TreeView();
        this.btn_addtable = new System.Windows.Forms.Button();
        this.btn_removetable = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.col_page = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.col_control = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.btn_setresult = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.btn_alladd = new System.Windows.Forms.Button();
        this.btn_allremove = new System.Windows.Forms.Button();
        this.panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "当前操作:";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(78, 13);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(29, 12);
        this.label2.TabIndex = 0;
        this.label2.Text = "删除";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(146, 13);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(59, 12);
        this.label3.TabIndex = 0;
        this.label3.Text = "当前控件:";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(211, 13);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(71, 12);
        this.label4.TabIndex = 0;
        this.label4.Text = "ControlName";
        this.cbx_control.FormattingEnabled = true;
        this.cbx_control.Location = new System.Drawing.Point(213, 209);
        this.cbx_control.Name = "cbx_control";
        this.cbx_control.Size = new System.Drawing.Size(121, 20);
        this.cbx_control.TabIndex = 13;
        this.cbx_control.Visible = false;
        this.cbx_control.SelectedIndexChanged += new System.EventHandler(cbx_control_SelectedIndexChanged);
        this.cbx_page.FormattingEnabled = true;
        this.cbx_page.Location = new System.Drawing.Point(213, 183);
        this.cbx_page.Name = "cbx_page";
        this.cbx_page.Size = new System.Drawing.Size(121, 20);
        this.cbx_page.TabIndex = 12;
        this.cbx_page.Visible = false;
        this.cbx_page.SelectedIndexChanged += new System.EventHandler(cbx_page_SelectedIndexChanged);
        this.treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.treeView1.Location = new System.Drawing.Point(15, 40);
        this.treeView1.Name = "treeView1";
        this.treeView1.Size = new System.Drawing.Size(190, 446);
        this.treeView1.TabIndex = 0;
        this.treeView1.DoubleClick += new System.EventHandler(treeView1_DoubleClick);
        this.btn_addtable.Location = new System.Drawing.Point(213, 54);
        this.btn_addtable.Name = "btn_addtable";
        this.btn_addtable.Size = new System.Drawing.Size(75, 23);
        this.btn_addtable.TabIndex = 1;
        this.btn_addtable.Text = "添加表";
        this.btn_addtable.UseVisualStyleBackColor = true;
        this.btn_addtable.Click += new System.EventHandler(btnadd_Click);
        this.btn_removetable.Location = new System.Drawing.Point(213, 83);
        this.btn_removetable.Name = "btn_removetable";
        this.btn_removetable.Size = new System.Drawing.Size(75, 23);
        this.btn_removetable.TabIndex = 2;
        this.btn_removetable.Text = "移除表";
        this.btn_removetable.UseVisualStyleBackColor = true;
        this.btn_removetable.Click += new System.EventHandler(btn_removetable_Click);
        this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Controls.Add(this.checkBox1);
        this.panel1.Controls.Add(this.dataGridView1);
        this.panel1.Controls.Add(this.listBox1);
        this.panel1.Controls.Add(this.textBox1);
        this.panel1.Controls.Add(this.label7);
        this.panel1.Controls.Add(this.label5);
        this.panel1.Controls.Add(this.btn_setresult);
        this.panel1.Location = new System.Drawing.Point(294, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(627, 446);
        this.panel1.TabIndex = 3;
        this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(550, 12);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(72, 16);
        this.checkBox1.TabIndex = 6;
        this.checkBox1.Text = "异步操作";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(this.Column1, this.Column5, this.Column2, this.Column6, this.Column3, this.col_page, this.col_control, this.Column4);
        this.dataGridView1.Location = new System.Drawing.Point(5, 86);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.Size = new System.Drawing.Size(617, 245);
        this.dataGridView1.TabIndex = 1;
        this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
        this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        this.Column1.HeaderText = "";
        this.Column1.Name = "Column1";
        this.Column1.Width = 25;
        this.Column5.HeaderText = "逻辑";
        this.Column5.Name = "Column5";
        this.Column2.HeaderText = "字段";
        this.Column2.Name = "Column2";
        this.Column2.ReadOnly = true;
        this.Column2.Width = 80;
        this.Column6.HeaderText = "条件";
        this.Column6.Name = "Column6";
        this.Column3.HeaderText = "类型";
        this.Column3.Name = "Column3";
        this.Column3.Visible = false;
        this.col_page.HeaderText = "页面";
        this.col_page.Name = "col_page";
        this.col_page.ReadOnly = true;
        this.col_page.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.col_control.HeaderText = "控件";
        this.col_control.Name = "col_control";
        this.col_control.ReadOnly = true;
        this.col_control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.col_control.Width = 150;
        this.Column4.HeaderText = "值";
        this.Column4.Name = "Column4";
        this.Column4.Width = 45;
        this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new System.Drawing.Point(5, 28);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new System.Drawing.Size(617, 52);
        this.listBox1.TabIndex = 0;
        this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
        this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox1.Location = new System.Drawing.Point(62, 337);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBox1.Size = new System.Drawing.Size(560, 104);
        this.textBox1.TabIndex = 3;
        this.textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
        this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(3, 337);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 4;
        this.label7.Text = "命令预览";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(3, 13);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "已选表";
        this.btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btn_setresult.Location = new System.Drawing.Point(5, 357);
        this.btn_setresult.Name = "btn_setresult";
        this.btn_setresult.Size = new System.Drawing.Size(51, 23);
        this.btn_setresult.TabIndex = 2;
        this.btn_setresult.Text = "生成";
        this.btn_setresult.UseVisualStyleBackColor = true;
        this.btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
        this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button8.Location = new System.Drawing.Point(765, 464);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 23);
        this.button8.TabIndex = 4;
        this.button8.Text = "确定";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(button8_Click);
        this.button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button9.Location = new System.Drawing.Point(846, 464);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(75, 23);
        this.button9.TabIndex = 5;
        this.button9.Text = "取消";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        this.btn_alladd.Location = new System.Drawing.Point(213, 124);
        this.btn_alladd.Name = "btn_alladd";
        this.btn_alladd.Size = new System.Drawing.Size(75, 23);
        this.btn_alladd.TabIndex = 7;
        this.btn_alladd.Text = "全选表";
        this.btn_alladd.UseVisualStyleBackColor = true;
        this.btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
        this.btn_allremove.Location = new System.Drawing.Point(213, 162);
        this.btn_allremove.Name = "btn_allremove";
        this.btn_allremove.Size = new System.Drawing.Size(75, 23);
        this.btn_allremove.TabIndex = 8;
        this.btn_allremove.Text = "全移除";
        this.btn_allremove.UseVisualStyleBackColor = true;
        this.btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(933, 498);
        base.Controls.Add(this.btn_allremove);
        base.Controls.Add(this.btn_alladd);
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.btn_removetable);
        base.Controls.Add(this.btn_addtable);
        base.Controls.Add(this.treeView1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button8);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "DBDeleteForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "删除数据";
        base.Load += new System.EventHandler(DBSelectForm_Load);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
