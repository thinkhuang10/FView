using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using ShapeRuntime;

namespace HMIEditEnvironment.Animation;

public class DBSelectForm : Form
{
    public byte[] OtherData;

    public string ResultSQL = "";

    public string ResultTo = "";

    public bool Ansync;

    private string ResultString = "";

    private string Wherestr = "";

    private string Orderstr = "";

    private List<string> ListboxContentstr = new();

    private DataTable DgDatatable = new();

    private DataTable WhereDatatable = new();

    private DataTable OrderDatatable = new();

    private readonly Dictionary<string, DataFile> dicds = new();

    private IContainer components;

    private Label label1;

    private Label label2;

    private Label label3;

    private TreeView treeView1;

    private Button btn_addtable;

    private Button btn_removetable;

    private Button btn_alladd;

    private Button btn_allremove;

    private Panel panel1;

    private Label label5;

    private ComboBox comboBox1;

    private Label label6;

    private DataGridView dataGridView1;

    private Button btn_sort;

    private Button btn_tiaojian;

    private TextBox textBox1;

    private Label label7;

    private Button button8;

    private Button button9;

    private ListBox listBox1;

    private Button btn_setresult;

    public Label label4;

    private Button button3;

    private Button button2;

    private Button button1;

    private ListBox listBox2;

    private Button button6;

    private Button button4;

    private Button button10;

    private Button button7;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 上移ToolStripMenuItem;

    private ToolStripMenuItem 下移ToolStripMenuItem;

    private ToolStripSeparator toolStripSeparator1;

    private ToolStripMenuItem 删除ToolStripMenuItem;

    private Button button12;

    private Button button11;

    private CheckBox checkBox1;

    private DataGridViewCheckBoxColumn Column1;

    private DataGridViewTextBoxColumn Column2;

    private DataGridViewTextBoxColumn Column3;

    private DataGridViewTextBoxColumn Column5;

    private DataGridViewCheckBoxColumn Column4;

    private DataGridViewComboBoxColumn Column6;

    private ComboBox comboBox2;

    private ToolTip toolTip1;

    public DBSelectForm()
    {
        InitializeComponent();

        DgDatatable.Columns.Add("ck1");
        DgDatatable.Columns.Add("tbname");
        DgDatatable.Columns.Add("varname");
        DgDatatable.Columns.Add("asname");
        DgDatatable.Columns.Add("ck2");
        DgDatatable.Columns.Add("jointo");
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        DBSelectSerializeCopy dBSelectSerializeCopy = new()
        {
            ansync = Ansync,
            ResultString = ResultString,
            Wherestr = Wherestr,
            Orderstr = Orderstr,
            ListboxContentstr = ListboxContentstr,
            DgDatatable = DgDatatable,
            WhereDatatable = WhereDatatable,
            OrderDatatable = OrderDatatable
        };
        formatter.Serialize(memoryStream, dBSelectSerializeCopy);
        byte[] result = memoryStream.ToArray();
        memoryStream.Close();
        return result;
    }

    public void DeSerialize(byte[] bt)
    {
        if (bt == null)
        {
            return;
        }
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream(bt);
        DBSelectSerializeCopy dBSelectSerializeCopy = (DBSelectSerializeCopy)formatter.Deserialize(stream);
        stream.Close();
        Ansync = dBSelectSerializeCopy.ansync;
        ResultString = dBSelectSerializeCopy.ResultString;
        Wherestr = dBSelectSerializeCopy.Wherestr;
        Orderstr = dBSelectSerializeCopy.Orderstr;
        ListboxContentstr = dBSelectSerializeCopy.ListboxContentstr;
        if (dBSelectSerializeCopy.DgDatatable.Columns.Count == DgDatatable.Columns.Count)
        {
            DgDatatable = dBSelectSerializeCopy.DgDatatable;
        }
        else
        {
            foreach (DataRow row in dBSelectSerializeCopy.DgDatatable.Rows)
            {
                DataRow dataRow2 = DgDatatable.NewRow();
                dataRow2["ck1"] = row["ck1"];
                dataRow2["tbname"] = row["tbname"];
                dataRow2["varname"] = row["varname"];
                dataRow2["asname"] = "";
                dataRow2["ck2"] = row["ck2"];
                dataRow2["jointo"] = null;
            }
        }
        WhereDatatable = dBSelectSerializeCopy.WhereDatatable;
        OrderDatatable = dBSelectSerializeCopy.OrderDatatable;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
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
        if (WhereDatatable.Columns.Count == 0)
        {
            WhereDatatable.Columns.Add(new DataColumn("逻辑关系"));
            WhereDatatable.Columns.Add(new DataColumn("字段"));
            WhereDatatable.Columns.Add(new DataColumn("匹配运算符"));
            WhereDatatable.Columns.Add(new DataColumn("值"));
        }
        if (OrderDatatable.Columns.Count == 0)
        {
            OrderDatatable.Columns.Add(new DataColumn("字段"));
            OrderDatatable.Columns.Add(new DataColumn("排序规则"));
        }
        comboBox2.Items.Add("{...}");
        comboBox2.Items.Add("{内部变量}");
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            comboBox2.Items.Add("{" + df.name + "}");
            dicds.Add(df.name, df);
        }
        try
        {
            DeSerialize(OtherData);
            string[] array = ResultTo.Split(',');
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (text != "")
                {
                    listBox2.Items.Add(text);
                }
            }
            checkBox1.Checked = Ansync;
            if (ListboxContentstr.Count != 0)
            {
                foreach (string item2 in ListboxContentstr)
                {
                    listBox1.Items.Add(item2);
                }
            }
            Column6.Items.Clear();
            foreach (object item3 in listBox1.Items)
            {
                string text2 = item3.ToString();
                if (DBOperationGlobal.conn.State != ConnectionState.Open)
                {
                    DBOperationGlobal.conn.Open();
                }
                DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text2 + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
                DataSet dataSet = new();
                DBOperationGlobal.adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    if (!Column6.Items.Contains(item3.ToString() + "." + row["COLUMN_NAME"].ToString()))
                    {
                        Column6.Items.Add(item3.ToString() + "." + row["COLUMN_NAME"].ToString());
                    }
                }
            }
            foreach (DataRow row2 in DgDatatable.Rows)
            {
                if (!Column6.Items.Contains(row2["jointo"]))
                {
                    Column6.Items.Add(row2["jointo"]);
                }
                dataGridView1.Rows.Add(Convert.ToBoolean(row2["ck1"].ToString()), row2["tbname"].ToString(), row2["varname"].ToString(), row2["asname"].ToString(), Convert.ToBoolean(row2["ck2"].ToString()), row2["jointo"]);
            }
            textBox1.Text = ResultSQL;
        }
        catch
        {
        }
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text != "数据库" && !listBox1.Items.Contains(treeView1.SelectedNode.Text))
        {
            listBox1.Items.Add(treeView1.SelectedNode.Text);
            SetDatagridviewDuetoListbox();
        }
    }

    private void SetDatagridviewDuetoListbox()
    {
        dataGridView1.Rows.Clear();
        Column6.Items.Clear();
        foreach (object item in listBox1.Items)
        {
            string text = item.ToString();
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            DataSet dataSet = new();
            DBOperationGlobal.adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (!Column6.Items.Contains(item.ToString() + "." + row["COLUMN_NAME"].ToString()))
                {
                    Column6.Items.Add(item.ToString() + "." + row["COLUMN_NAME"].ToString());
                }
                dataGridView1.Rows.Add(true, item.ToString(), row["COLUMN_NAME"], "", false, null);
            }
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
        }
        SetDatagridviewDuetoListbox();
    }

    private void btn_alladd_Click(object sender, EventArgs e)
    {
        if (treeView1.Nodes.Count != 0)
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (!listBox1.Items.Contains(node.Text))
                {
                    listBox1.Items.Add(node.Text);
                }
            }
        }
        SetDatagridviewDuetoListbox();
    }

    private void btn_allremove_Click(object sender, EventArgs e)
    {
        if (listBox1.Items.Count != 0)
        {
            listBox1.Items.Clear();
        }
        SetDatagridviewDuetoListbox();
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void btntiaojian_Click(object sender, EventArgs e)
    {
        RulesForm rulesForm = new()
        {
            RecordDatatable = WhereDatatable
        };
        foreach (object item in listBox1.Items)
        {
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + item.ToString() + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            DataSet dataSet = new();
            DBOperationGlobal.adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rulesForm.vars.Add(item.ToString() + "." + row["COLUMN_NAME"].ToString());
            }
        }
        if (rulesForm.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        if (rulesForm.RecordDatatable.Rows.Count > 0)
        {
            WhereDatatable = rulesForm.RecordDatatable;
            if (rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")
            {
                Wherestr = " where " + rulesForm.RecordDatatable.Rows[0]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '" + rulesForm.RecordDatatable.Rows[0]["值"].ToString() + "'";
            }
            else
            {
                Wherestr = " where " + rulesForm.RecordDatatable.Rows[0]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '%" + rulesForm.RecordDatatable.Rows[0]["值"].ToString() + "%'";
            }
            if (rulesForm.RecordDatatable.Rows.Count <= 1)
            {
                return;
            }
            for (int i = 1; i < rulesForm.RecordDatatable.Rows.Count; i++)
            {
                if (rulesForm.RecordDatatable.Rows[i]["逻辑关系"].ToString() == "并且")
                {
                    Wherestr += " AND ";
                }
                else
                {
                    Wherestr += " OR ";
                }
                if (rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")
                {
                    string wherestr = Wherestr;
                    Wherestr = wherestr + rulesForm.RecordDatatable.Rows[i]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '" + rulesForm.RecordDatatable.Rows[i]["值"].ToString() + "'";
                }
                else
                {
                    string wherestr2 = Wherestr;
                    Wherestr = wherestr2 + rulesForm.RecordDatatable.Rows[i]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '%" + rulesForm.RecordDatatable.Rows[i]["值"].ToString() + "%'";
                }
            }
        }
        else
        {
            Wherestr = "";
        }
    }

    private void btn_sort_Click(object sender, EventArgs e)
    {
        SortForm sortForm = new()
        {
            RecordDatatable = OrderDatatable
        };
        foreach (object item in listBox1.Items)
        {
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + item.ToString() + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            DataSet dataSet = new();
            DBOperationGlobal.adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                sortForm.vars.Add(item.ToString() + "." + row["COLUMN_NAME"].ToString());
            }
        }
        if (sortForm.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        OrderDatatable = sortForm.RecordDatatable;
        if (sortForm.RecordDatatable.Rows.Count > 0)
        {
            Orderstr = " ORDER BY " + sortForm.RecordDatatable.Rows[0]["字段"].ToString();
            if (sortForm.RecordDatatable.Rows[0]["排序规则"].ToString() == "升序")
            {
                Orderstr += " ASC ";
            }
            else
            {
                Orderstr += " DESC ";
            }
            if (sortForm.RecordDatatable.Rows.Count <= 1)
            {
                return;
            }
            for (int i = 1; i < sortForm.RecordDatatable.Rows.Count; i++)
            {
                Orderstr = Orderstr + "," + sortForm.RecordDatatable.Rows[i]["字段"].ToString();
                if (sortForm.RecordDatatable.Rows[i]["排序规则"].ToString() == "升序")
                {
                    Orderstr += " ASC ";
                }
                else
                {
                    Orderstr += " DESC ";
                }
            }
        }
        else
        {
            Orderstr = "";
        }
    }

    private void comboBox1_DropDown(object sender, EventArgs e)
    {
    }

    private void textBox1_DoubleClick(object sender, EventArgs e)
    {
        textBox1.ReadOnly = false;
    }

    private void btn_setresult_Click(object sender, EventArgs e)
    {
        string text = "SELECT ";
        string text2 = " FROM ";
        string text3 = "";
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            if ((bool)item.Cells[0].Value)
            {
                string text4 = "";
                if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                {
                    text4 = " AS '" + item.Cells[3].Value.ToString() + "'";
                }
                string text5 = text;
                text = text5 + item.Cells[1].Value.ToString() + "." + item.Cells[2].Value.ToString() + text4 + ",";
            }
            if ((bool)item.Cells[4].Value && item.Cells[5].Value != null)
            {
                string text6 = text3;
                text3 = text6 + " LEFT JOIN " + item.Cells[5].Value.ToString().Split('.')[0] + " ON " + item.Cells[1].Value.ToString() + "." + item.Cells[2].Value.ToString() + "=" + item.Cells[5].Value.ToString();
            }
        }
        foreach (string item2 in listBox1.Items)
        {
            if (!text3.Contains("LEFT JOIN " + item2))
            {
                text2 = text2 + item2 + ",";
            }
        }
        if (text2.EndsWith(","))
        {
            text2 = text2.Substring(0, text2.Length - 1);
        }
        text = text.Substring(0, text.Length - 1);
        ResultString = text + text2 + text3 + Wherestr + Orderstr;
        textBox1.Text = ResultString;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "" && MessageBox.Show("监测到未生成SQL语句,是否自动生成？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            btn_setresult_Click(sender, e);
        }
        DgDatatable.Rows.Clear();
        ListboxContentstr.Clear();
        foreach (object item in listBox1.Items)
        {
            ListboxContentstr.Add(item.ToString());
        }
        foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
        {
            DataRow dataRow = DgDatatable.NewRow();
            dataRow["ck1"] = ((bool)item2.Cells[0].Value).ToString();
            dataRow["tbname"] = item2.Cells[1].Value.ToString();
            dataRow["varname"] = item2.Cells[2].Value.ToString();
            dataRow["asname"] = item2.Cells[3].Value.ToString();
            dataRow["ck2"] = ((bool)item2.Cells[4].Value).ToString();
            dataRow["jointo"] = item2.Cells[5].Value;
            DgDatatable.Rows.Add(dataRow);
        }
        OtherData = Serialize();
        ResultSQL = textBox1.Text;
        string text = "";
        foreach (object item3 in listBox2.Items)
        {
            text = text + item3.ToString() + ",";
        }
        if (text.Length != 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        ResultTo = text;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button9_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void comboBox1_DropDownClosed(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            item.Cells[0].Value = true;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            item.Cells[0].Value = false;
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
        {
            item.Cells[0].Value = !(bool)item.Cells[0].Value;
        }
    }

    private void label6_Click(object sender, EventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (!(comboBox1.Text != "{...}") || !(comboBox1.Text != "") || listBox2.Items.Contains(comboBox1.Text))
        {
            return;
        }
        if (comboBox1.Text == "{权限管理字段}")
        {
            listBox2.Items.Add("{权限管理字段}");
        }
        else if (comboBox1.Text.StartsWith("."))
        {
            if (comboBox2.Text.StartsWith("{"))
            {
                string text = comboBox2.Text.Replace("{", "").Replace("}", "");
                listBox2.Items.Add("{" + text + comboBox1.Text + "}");
            }
        }
        else
        {
            listBox2.Items.Add(comboBox1.Text);
        }
    }

    private void button6_Click(object sender, EventArgs e)
    {
        object[] array = new object[listBox2.SelectedItems.Count];
        listBox2.SelectedItems.CopyTo(array, 0);
        object[] array2 = array;
        foreach (object value in array2)
        {
            listBox2.Items.Remove(value);
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
        foreach (DataGridViewRow item in selectedRows)
        {
            int index = item.Index;
            if (index != 0)
            {
                dataGridView1.Rows.Remove(item);
                dataGridView1.Rows.Insert(index - 1, item);
                continue;
            }
            break;
        }
        dataGridView1.ClearSelection();
        foreach (DataGridViewRow item2 in selectedRows)
        {
            item2.Selected = true;
        }
    }

    private void button10_Click(object sender, EventArgs e)
    {
        DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
        foreach (DataGridViewRow item in selectedRows)
        {
            int index = item.Index;
            if (index != dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows.Remove(item);
                dataGridView1.Rows.Insert(index + 1, item);
                continue;
            }
            break;
        }
        dataGridView1.ClearSelection();
        foreach (DataGridViewRow item2 in selectedRows)
        {
            item2.Selected = true;
        }
    }

    private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Dictionary<int, object> dictionary = new();
        foreach (int selectedIndex in listBox2.SelectedIndices)
        {
            dictionary.Add(selectedIndex, listBox2.Items[selectedIndex]);
        }
        listBox2.SelectedItems.Clear();
        foreach (int key in dictionary.Keys)
        {
            if (key != 0)
            {
                listBox2.Items.Remove(dictionary[key]);
                listBox2.Items.Insert(key - 1, dictionary[key]);
                listBox2.SelectedItems.Add(dictionary[key]);
                continue;
            }
            break;
        }
    }

    private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Dictionary<int, object> dictionary = new();
        foreach (int selectedIndex in listBox2.SelectedIndices)
        {
            dictionary.Add(selectedIndex, listBox2.Items[selectedIndex]);
        }
        listBox2.SelectedItems.Clear();
        SortedList<int, int> sortedList = new();
        foreach (int key in dictionary.Keys)
        {
            if (key == listBox2.Items.Count - 1)
            {
                sortedList.Clear();
                break;
            }
            sortedList.Add(key, key);
        }
        for (int num2 = sortedList.Count - 1; num2 >= 0; num2--)
        {
            listBox2.Items.Remove(dictionary[sortedList.Keys[num2]]);
            listBox2.Items.Insert(sortedList.Keys[num2] + 1, dictionary[sortedList.Keys[num2]]);
            listBox2.SelectedItems.Add(dictionary[sortedList.Keys[num2]]);
        }
    }

    private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        button6_Click(sender, e);
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        Ansync = checkBox1.Checked;
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            comboBox1.Items.Clear();
            if (comboBox2.Text != "{内部变量}" && comboBox2.Text != "{...}")
            {
                DataFile dataFile = dicds[comboBox2.Text.Substring(1, comboBox2.Text.Length - 2)];
                List<string> list = new();
                foreach (CShape item in dataFile.ListAllShowCShape)
                {
                    if (item is CControl && (((CControl)item)._c is CDataGridView || ((CControl)item)._c is CButton || ((CControl)item)._c is CDateTimePicker || ((CControl)item)._c is CTextBox || ((CControl)item)._c is CLabel || ((CControl)item)._c is CComboBox || ((CControl)item)._c is CListBox))
                    {
                        list.Add("." + item.Name);
                        list.Add("." + item.Name + ".Tag");
                    }
                    else if (item is CControl && ((CControl)item)._c is CCheckBox)
                    {
                        list.Add("." + item.Name);
                        list.Add("." + item.Name + ".Value");
                        list.Add("." + item.Name + ".Tag");
                    }
                    else if (item is CString)
                    {
                        list.Add("." + item.Name);
                        list.Add("." + item.Name + ".Tag");
                    }
                    else if (item is CControl)
                    {
                        list.Add("." + item.Name);
                    }
                }
                list.Sort();
                {
                    foreach (string item2 in list)
                    {
                        comboBox1.Items.Add(item2);
                    }
                    return;
                }
            }
            comboBox1.Items.Add("{权限管理字段}");
            foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
            {
                comboBox1.Items.Add("{[" + projectIO.name + "]}");
            }
            XmlNodeList xmlNodeList = CEditEnvironmentGlobal.xmldoc.SelectNodes("/DocumentRoot/Item");
            foreach (XmlNode item3 in xmlNodeList)
            {
                comboBox1.Items.Add("{[" + item3.Attributes["Name"].Value + "]}");
            }
        }
        catch
        {
        }
    }

    private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
    }

    private void treeView1_DoubleClick(object sender, EventArgs e)
    {
        btnadd_Click(sender, e);
    }

    private void comboBox2_DropDownClosed(object sender, EventArgs e)
    {
        if (comboBox2.SelectedItem == null || !(comboBox2.SelectedItem.ToString() == "{...}"))
        {
            return;
        }
        DBVarForm dBVarForm = new();
        if (dBVarForm.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        string[] array = dBVarForm.toVarStr.Split(',');
        string[] array2 = array;
        foreach (string text in array2)
        {
            if (!listBox2.Items.Contains(text))
            {
                listBox2.Items.Add(text);
            }
        }
        comboBox2.Text = "";
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.A && textBox1.Text != "")
        {
            textBox1.SelectAll();
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
        this.components = new System.ComponentModel.Container();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.treeView1 = new System.Windows.Forms.TreeView();
        this.btn_addtable = new System.Windows.Forms.Button();
        this.btn_removetable = new System.Windows.Forms.Button();
        this.btn_alladd = new System.Windows.Forms.Button();
        this.btn_allremove = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.listBox2 = new System.Windows.Forms.ListBox();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.label5 = new System.Windows.Forms.Label();
        this.btn_sort = new System.Windows.Forms.Button();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.btn_setresult = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.btn_tiaojian = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button12 = new System.Windows.Forms.Button();
        this.button10 = new System.Windows.Forms.Button();
        this.button11 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.panel1.SuspendLayout();
        this.contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "当前操作:";
        this.label1.Click += new System.EventHandler(label1_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(78, 13);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(29, 12);
        this.label2.TabIndex = 0;
        this.label2.Text = "查询";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(146, 13);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(59, 12);
        this.label3.TabIndex = 0;
        this.label3.Text = "当前控件:";
        this.label3.Click += new System.EventHandler(label1_Click);
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(211, 13);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(71, 12);
        this.label4.TabIndex = 0;
        this.label4.Text = "ControlName";
        this.label4.Click += new System.EventHandler(label1_Click);
        this.treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.treeView1.Location = new System.Drawing.Point(15, 40);
        this.treeView1.Name = "treeView1";
        this.treeView1.Size = new System.Drawing.Size(190, 494);
        this.treeView1.TabIndex = 0;
        this.treeView1.DoubleClick += new System.EventHandler(treeView1_DoubleClick);
        this.btn_addtable.Location = new System.Drawing.Point(213, 54);
        this.btn_addtable.Name = "btn_addtable";
        this.btn_addtable.Size = new System.Drawing.Size(75, 23);
        this.btn_addtable.TabIndex = 1;
        this.btn_addtable.Text = "添加表";
        this.toolTip1.SetToolTip(this.btn_addtable, "添加选中的表");
        this.btn_addtable.UseVisualStyleBackColor = true;
        this.btn_addtable.Click += new System.EventHandler(btnadd_Click);
        this.btn_removetable.Location = new System.Drawing.Point(213, 83);
        this.btn_removetable.Name = "btn_removetable";
        this.btn_removetable.Size = new System.Drawing.Size(75, 23);
        this.btn_removetable.TabIndex = 2;
        this.btn_removetable.Text = "移除表";
        this.toolTip1.SetToolTip(this.btn_removetable, "移除选中的表");
        this.btn_removetable.UseVisualStyleBackColor = true;
        this.btn_removetable.Click += new System.EventHandler(btn_removetable_Click);
        this.btn_alladd.Location = new System.Drawing.Point(213, 127);
        this.btn_alladd.Name = "btn_alladd";
        this.btn_alladd.Size = new System.Drawing.Size(75, 23);
        this.btn_alladd.TabIndex = 3;
        this.btn_alladd.Text = "全选表";
        this.toolTip1.SetToolTip(this.btn_alladd, "将左面的表全选");
        this.btn_alladd.UseVisualStyleBackColor = true;
        this.btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
        this.btn_allremove.Location = new System.Drawing.Point(213, 156);
        this.btn_allremove.Name = "btn_allremove";
        this.btn_allremove.Size = new System.Drawing.Size(75, 23);
        this.btn_allremove.TabIndex = 4;
        this.btn_allremove.Text = "全移除";
        this.toolTip1.SetToolTip(this.btn_allremove, "将右边的表全移除");
        this.btn_allremove.UseVisualStyleBackColor = true;
        this.btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
        this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Controls.Add(this.comboBox2);
        this.panel1.Controls.Add(this.listBox2);
        this.panel1.Controls.Add(this.listBox1);
        this.panel1.Controls.Add(this.textBox1);
        this.panel1.Controls.Add(this.comboBox1);
        this.panel1.Controls.Add(this.label7);
        this.panel1.Controls.Add(this.label6);
        this.panel1.Controls.Add(this.dataGridView1);
        this.panel1.Controls.Add(this.label5);
        this.panel1.Controls.Add(this.btn_sort);
        this.panel1.Controls.Add(this.checkBox1);
        this.panel1.Controls.Add(this.btn_setresult);
        this.panel1.Controls.Add(this.button6);
        this.panel1.Controls.Add(this.button4);
        this.panel1.Controls.Add(this.btn_tiaojian);
        this.panel1.Controls.Add(this.button3);
        this.panel1.Controls.Add(this.button2);
        this.panel1.Controls.Add(this.button12);
        this.panel1.Controls.Add(this.button10);
        this.panel1.Controls.Add(this.button11);
        this.panel1.Controls.Add(this.button7);
        this.panel1.Controls.Add(this.button1);
        this.panel1.Location = new System.Drawing.Point(294, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(645, 494);
        this.panel1.TabIndex = 5;
        this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(5, 303);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(263, 20);
        this.comboBox2.TabIndex = 12;
        this.toolTip1.SetToolTip(this.comboBox2, "{..}——可以进行快速绑定\r\n{内部变量}——可以绑定内部变量\r\n{页面}——可以绑定页面元素");
        this.comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        this.comboBox2.DropDownClosed += new System.EventHandler(comboBox2_DropDownClosed);
        this.listBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox2.ContextMenuStrip = this.contextMenuStrip1;
        this.listBox2.FormattingEnabled = true;
        this.listBox2.ItemHeight = 12;
        this.listBox2.Location = new System.Drawing.Point(5, 333);
        this.listBox2.Name = "listBox2";
        this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBox2.Size = new System.Drawing.Size(604, 52);
        this.listBox2.TabIndex = 16;
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.上移ToolStripMenuItem, this.下移ToolStripMenuItem, this.toolStripSeparator1, this.删除ToolStripMenuItem });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.ShowImageMargin = false;
        this.contextMenuStrip1.Size = new System.Drawing.Size(93, 76);
        this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
        this.上移ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
        this.上移ToolStripMenuItem.Text = "上移(&U)";
        this.上移ToolStripMenuItem.Click += new System.EventHandler(上移ToolStripMenuItem_Click);
        this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
        this.下移ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
        this.下移ToolStripMenuItem.Text = "下移(&D)";
        this.下移ToolStripMenuItem.Click += new System.EventHandler(下移ToolStripMenuItem_Click);
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(89, 6);
        this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
        this.删除ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
        this.删除ToolStripMenuItem.Text = "删除";
        this.删除ToolStripMenuItem.Click += new System.EventHandler(删除ToolStripMenuItem_Click);
        this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new System.Drawing.Point(5, 28);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new System.Drawing.Size(554, 52);
        this.listBox1.TabIndex = 2;
        this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
        this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox1.Location = new System.Drawing.Point(62, 391);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBox1.Size = new System.Drawing.Size(575, 98);
        this.textBox1.TabIndex = 20;
        this.toolTip1.SetToolTip(this.textBox1, "生成的SQL语句，双击后可进行手动编辑");
        this.textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
        this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
        this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(274, 303);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(272, 20);
        this.comboBox1.TabIndex = 13;
        this.toolTip1.SetToolTip(this.comboBox1, "选择变量后请点击右面的添加按钮进行添加");
        this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        this.comboBox1.DropDownClosed += new System.EventHandler(comboBox1_DropDownClosed);
        this.comboBox1.DropDown += new System.EventHandler(comboBox1_DropDown);
        this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(3, 394);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 4;
        this.label7.Text = "命令预览";
        this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(3, 285);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(95, 12);
        this.label6.TabIndex = 4;
        this.label6.Text = "应用查询结果到:";
        this.label6.Click += new System.EventHandler(label6_Click);
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(this.Column1, this.Column2, this.Column3, this.Column5, this.Column4, this.Column6);
        this.dataGridView1.Location = new System.Drawing.Point(5, 86);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(577, 196);
        this.dataGridView1.TabIndex = 6;
        this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridView1_DataError);
        this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        this.Column1.HeaderText = "";
        this.Column1.Name = "Column1";
        this.Column1.Width = 25;
        this.Column2.HeaderText = "所属表";
        this.Column2.Name = "Column2";
        this.Column2.ReadOnly = true;
        this.Column2.Width = 130;
        this.Column3.HeaderText = "字段";
        this.Column3.Name = "Column3";
        this.Column3.ReadOnly = true;
        this.Column3.Width = 145;
        this.Column5.HeaderText = "显示名称";
        this.Column5.Name = "Column5";
        this.Column4.HeaderText = "连接";
        this.Column4.Name = "Column4";
        this.Column4.Width = 50;
        this.Column6.HeaderText = "连接字段";
        this.Column6.Name = "Column6";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(3, 13);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 1;
        this.label5.Text = "已选表";
        this.label5.Click += new System.EventHandler(label1_Click);
        this.btn_sort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btn_sort.Location = new System.Drawing.Point(565, 57);
        this.btn_sort.Name = "btn_sort";
        this.btn_sort.Size = new System.Drawing.Size(75, 23);
        this.btn_sort.TabIndex = 5;
        this.btn_sort.Text = "排序设计";
        this.toolTip1.SetToolTip(this.btn_sort, "可以根据某一字段进行排序");
        this.btn_sort.UseVisualStyleBackColor = true;
        this.btn_sort.Click += new System.EventHandler(btn_sort_Click);
        this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(565, 9);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(72, 16);
        this.checkBox1.TabIndex = 3;
        this.checkBox1.Text = "异步操作";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        this.btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btn_setresult.Location = new System.Drawing.Point(5, 414);
        this.btn_setresult.Name = "btn_setresult";
        this.btn_setresult.Size = new System.Drawing.Size(51, 23);
        this.btn_setresult.TabIndex = 19;
        this.btn_setresult.Text = "生成";
        this.toolTip1.SetToolTip(this.btn_setresult, "根据上面的配置自动生成SQL语句");
        this.btn_setresult.UseVisualStyleBackColor = true;
        this.btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
        this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button6.Location = new System.Drawing.Point(601, 300);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(39, 23);
        this.button6.TabIndex = 15;
        this.button6.Text = "删除";
        this.toolTip1.SetToolTip(this.button6, "删除");
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(button6_Click);
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button4.Location = new System.Drawing.Point(552, 300);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(39, 23);
        this.button4.TabIndex = 14;
        this.button4.Text = "添加";
        this.toolTip1.SetToolTip(this.button4, "添加");
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.btn_tiaojian.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btn_tiaojian.Location = new System.Drawing.Point(565, 28);
        this.btn_tiaojian.Name = "btn_tiaojian";
        this.btn_tiaojian.Size = new System.Drawing.Size(75, 23);
        this.btn_tiaojian.TabIndex = 4;
        this.btn_tiaojian.Text = "条件设计";
        this.toolTip1.SetToolTip(this.btn_tiaojian, "设计查询条件");
        this.btn_tiaojian.UseVisualStyleBackColor = true;
        this.btn_tiaojian.Click += new System.EventHandler(btntiaojian_Click);
        this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button3.Location = new System.Drawing.Point(588, 202);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(52, 23);
        this.button3.TabIndex = 11;
        this.button3.Text = "反选";
        this.toolTip1.SetToolTip(this.button3, "反选");
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.Location = new System.Drawing.Point(588, 173);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(52, 23);
        this.button2.TabIndex = 10;
        this.button2.Text = "全不选";
        this.toolTip1.SetToolTip(this.button2, "全不选");
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button12.Location = new System.Drawing.Point(615, 362);
        this.button12.Name = "button12";
        this.button12.Size = new System.Drawing.Size(22, 23);
        this.button12.TabIndex = 18;
        this.button12.Text = "↓";
        this.toolTip1.SetToolTip(this.button12, "向下移动");
        this.button12.UseVisualStyleBackColor = true;
        this.button12.Click += new System.EventHandler(下移ToolStripMenuItem_Click);
        this.button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button10.Location = new System.Drawing.Point(588, 115);
        this.button10.Name = "button10";
        this.button10.Size = new System.Drawing.Size(52, 23);
        this.button10.TabIndex = 8;
        this.button10.Text = "↓";
        this.toolTip1.SetToolTip(this.button10, "向下移动");
        this.button10.UseVisualStyleBackColor = true;
        this.button10.Click += new System.EventHandler(button10_Click);
        this.button11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button11.Location = new System.Drawing.Point(615, 333);
        this.button11.Name = "button11";
        this.button11.Size = new System.Drawing.Size(22, 23);
        this.button11.TabIndex = 17;
        this.button11.Text = "↑";
        this.toolTip1.SetToolTip(this.button11, "向上移动");
        this.button11.UseVisualStyleBackColor = true;
        this.button11.Click += new System.EventHandler(上移ToolStripMenuItem_Click);
        this.button7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button7.Location = new System.Drawing.Point(588, 86);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(52, 23);
        this.button7.TabIndex = 7;
        this.button7.Text = "↑";
        this.toolTip1.SetToolTip(this.button7, "向上移动");
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(button7_Click);
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button1.Location = new System.Drawing.Point(588, 144);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(52, 23);
        this.button1.TabIndex = 9;
        this.button1.Text = "全选";
        this.toolTip1.SetToolTip(this.button1, "全选");
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button8.Location = new System.Drawing.Point(783, 511);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 23);
        this.button8.TabIndex = 7;
        this.button8.Text = "确定";
        this.toolTip1.SetToolTip(this.button8, "保存");
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(button8_Click);
        this.button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button9.Location = new System.Drawing.Point(864, 511);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(75, 23);
        this.button9.TabIndex = 8;
        this.button9.Text = "取消";
        this.toolTip1.SetToolTip(this.button9, "退出不保存");
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(951, 546);
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.btn_allremove);
        base.Controls.Add(this.btn_alladd);
        base.Controls.Add(this.btn_removetable);
        base.Controls.Add(this.btn_addtable);
        base.Controls.Add(this.treeView1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button8);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "DBSelectForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "查询数据";
        base.Load += new System.EventHandler(DBSelectForm_Load);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
