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

public class DBUpdateForm : Form
{
    public byte[] OtherData;

    public bool Ansync;

    public string ResultSQL = "";

    private string NowTable = "";

    private Dictionary<string, string[]> ResultStrings = new();

    private List<string> ListboxContentstr = new();

    private DataSet DgDatatables = new();

    private DataSet WhereDatatables = new();

    private readonly Dictionary<string, DataFile> dicds = new();

    private Label label1;

    private Label label2;

    private Label label3;

    private TreeView treeView1;

    private Button btn_addtable;

    private Button btn_removetable;

    private Panel panel1;

    private Label label5;

    private DataGridView dataGridView1;

    private TextBox textBox1;

    private Label label7;

    private Button button8;

    private Button button9;

    private Button btn_setresult;

    private Button btn_tiaojian;

    private ListBox listBox1;

    public Label label4;

    private ComboBox cbx_control;

    private ComboBox cbx_page;

    private CheckBox checkBox1;

    private DataGridViewCheckBoxColumn Column1;

    private DataGridViewTextBoxColumn Column2;

    private DataGridViewComboBoxColumn Column3;

    private DataGridViewTextBoxColumn col_page;

    private DataGridViewTextBoxColumn col_control;

    private DataGridViewTextBoxColumn Column4;

    private Button btn_allremove;

    private Button btn_alladd;

    public DBUpdateForm()
    {
        InitializeComponent();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    public byte[] Serialize()
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new();
        DBUpdateSerializeCopy dBUpdateSerializeCopy = new()
        {
            ansync = Ansync,
            ListboxContentstr = ListboxContentstr,
            DgDatatables = DgDatatables,
            ResultStrings = ResultStrings,
            NowTable = NowTable,
            WhereDatatables = WhereDatatables
        };
        formatter.Serialize(memoryStream, dBUpdateSerializeCopy);
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
            DBUpdateSerializeCopy dBUpdateSerializeCopy = (DBUpdateSerializeCopy)formatter.Deserialize(stream);
            stream.Close();
            Ansync = dBUpdateSerializeCopy.ansync;
            ResultStrings = dBUpdateSerializeCopy.ResultStrings;
            NowTable = dBUpdateSerializeCopy.NowTable;
            ListboxContentstr = dBUpdateSerializeCopy.ListboxContentstr;
            DgDatatables = dBUpdateSerializeCopy.DgDatatables;
            WhereDatatables = dBUpdateSerializeCopy.WhereDatatables;
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
        try
        {
            DeSerialize(OtherData);
            checkBox1.Checked = Ansync;
            if (ListboxContentstr.Count != 0)
            {
                listBox1.Items.Clear();
                foreach (string item4 in ListboxContentstr)
                {
                    listBox1.Items.Add(item4);
                }
            }
            listBox1.SelectedItem = NowTable;
            if (DgDatatables.Tables.Contains(NowTable))
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow row in DgDatatables.Tables[NowTable].Rows)
                {
                    dataGridView1.Rows.Add(Convert.ToBoolean(row["ck1"]), row["varname"].ToString(), row["vartype"].ToString(), row["page"].ToString(), row["control"].ToString(), row["value"].ToString());
                }
            }
            textBox1.Text = "";
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
        }
    }

    private void btn_removetable_Click(object sender, EventArgs e)
    {
        if (listBox1.SelectedItem == null)
        {
            return;
        }
        if (DgDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
        {
            DgDatatables.Tables.Remove(listBox1.SelectedItem.ToString());
        }
        if (WhereDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
        {
            WhereDatatables.Tables.Remove(listBox1.SelectedItem.ToString());
        }
        if (ResultStrings.ContainsKey(listBox1.SelectedItem.ToString()))
        {
            ResultStrings.Remove(listBox1.SelectedItem.ToString());
        }
        List<object> list = new();
        foreach (object selectedItem in listBox1.SelectedItems)
        {
            list.Add(selectedItem);
        }
        foreach (object item in list)
        {
            listBox1.Items.Remove(item);
            ResultStrings.Remove(item.ToString());
        }
        if (listBox1.Items.Count == 0)
        {
            NowTable = "";
        }
        else
        {
            listBox1.SelectedIndex = 0;
        }
    }

    private void btntiaojian_Click(object sender, EventArgs e)
    {
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
        textBox1.Text = "";
        if (dataGridView1.Rows.Count == 0)
        {
            return;
        }
        string text = "";
        if (dataGridView1.Rows.Count != 0)
        {
            bool flag = false;
            foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
            {
                if (Convert.ToBoolean(item.Cells[0].Value))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                text = text + "update " + NowTable.ToString();
                text += " set ";
                foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(item2.Cells[0].Value))
                    {
                        if (item2.Cells["col_page"].Value.ToString() != "{常量}")
                        {
                            string text2 = text;
                            text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["col_control"].Value.ToString() + "',";
                        }
                        else
                        {
                            string text3 = text;
                            text = text3 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column4"].Value.ToString() + "',";
                        }
                    }
                }
                text = text.Substring(0, text.Length - 1);
                if (!ResultStrings.ContainsKey(NowTable))
                {
                    ResultStrings.Add(NowTable, new string[2] { text, "" });
                }
                else
                {
                    ResultStrings[NowTable] = new string[2]
                    {
                        text,
                        ResultStrings[NowTable][1]
                    };
                }
            }
            else if (ResultStrings.ContainsKey(NowTable))
            {
                ResultStrings.Remove(NowTable);
            }
        }
        if (!DgDatatables.Tables.Contains(NowTable))
        {
            DataTable dataTable = new(NowTable);
            dataTable.Columns.Add("ck1");
            dataTable.Columns.Add("varname");
            dataTable.Columns.Add("vartype");
            dataTable.Columns.Add("value");
            dataTable.Columns.Add("page");
            dataTable.Columns.Add("control");
            foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ck1"] = item3.Cells[0].Value.ToString();
                dataRow["varname"] = item3.Cells[1].Value.ToString();
                dataRow["vartype"] = item3.Cells[2].Value.ToString();
                dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
                dataRow["page"] = item3.Cells["col_page"].Value.ToString();
                dataRow["control"] = item3.Cells["col_control"].Value.ToString();
                dataTable.Rows.Add(dataRow);
            }
            DgDatatables.Tables.Add(dataTable);
        }
        else
        {
            DgDatatables.Tables[NowTable].Rows.Clear();
            foreach (DataGridViewRow item4 in (IEnumerable)dataGridView1.Rows)
            {
                DataRow dataRow2 = DgDatatables.Tables[NowTable].NewRow();
                dataRow2["ck1"] = item4.Cells[0].Value.ToString();
                dataRow2["varname"] = item4.Cells[1].Value.ToString();
                dataRow2["vartype"] = item4.Cells[2].Value.ToString();
                dataRow2["value"] = ((item4.Cells["Column4"].Value == null) ? "" : item4.Cells["Column4"].Value.ToString());
                dataRow2["page"] = item4.Cells["col_page"].Value.ToString();
                dataRow2["control"] = item4.Cells["col_control"].Value.ToString();
                DgDatatables.Tables[NowTable].Rows.Add(dataRow2);
            }
        }
        foreach (string[] value in ResultStrings.Values)
        {
            TextBox textBox = textBox1;
            textBox.Text = textBox.Text + value[0] + value[1] + Environment.NewLine;
        }
    }

    private void button8_Click(object sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count != 0)
        {
            string text = "";
            if (dataGridView1.Rows.Count != 0)
            {
                bool flag = false;
                foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    text = text + "update " + NowTable.ToString();
                    text += " set ";
                    foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(item2.Cells[0].Value))
                        {
                            if (item2.Cells["col_page"].Value.ToString() != "{常量}")
                            {
                                string text2 = text;
                                text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["col_control"].Value.ToString() + "',";
                            }
                            else
                            {
                                string text3 = text;
                                text = text3 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column4"].Value.ToString() + "',";
                            }
                        }
                    }
                    text = text.Substring(0, text.Length - 1);
                    if (!ResultStrings.ContainsKey(NowTable))
                    {
                        ResultStrings.Add(NowTable, new string[2] { text, "" });
                    }
                    else
                    {
                        ResultStrings[NowTable] = new string[2]
                        {
                            text,
                            ResultStrings[NowTable][1]
                        };
                    }
                }
                else if (ResultStrings.ContainsKey(NowTable))
                {
                    ResultStrings.Remove(NowTable);
                }
            }
        }
        if (!DgDatatables.Tables.Contains(NowTable))
        {
            DataTable dataTable = new(NowTable);
            dataTable.Columns.Add("ck1");
            dataTable.Columns.Add("varname");
            dataTable.Columns.Add("vartype");
            dataTable.Columns.Add("value");
            dataTable.Columns.Add("page");
            dataTable.Columns.Add("control");
            foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ck1"] = item3.Cells[0].Value.ToString();
                dataRow["varname"] = item3.Cells[1].Value.ToString();
                dataRow["vartype"] = item3.Cells[2].Value.ToString();
                dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
                dataRow["page"] = item3.Cells["col_page"].Value.ToString();
                dataRow["control"] = item3.Cells["col_control"].Value.ToString();
                dataTable.Rows.Add(dataRow);
            }
            DgDatatables.Tables.Add(dataTable);
        }
        else
        {
            DgDatatables.Tables[NowTable].Rows.Clear();
            foreach (DataGridViewRow item4 in (IEnumerable)dataGridView1.Rows)
            {
                DataRow dataRow2 = DgDatatables.Tables[NowTable].NewRow();
                dataRow2["ck1"] = item4.Cells[0].Value.ToString();
                dataRow2["varname"] = item4.Cells[1].Value.ToString();
                dataRow2["vartype"] = item4.Cells[2].Value.ToString();
                dataRow2["value"] = ((item4.Cells["Column4"].Value == null) ? "" : item4.Cells["Column4"].Value.ToString());
                dataRow2["page"] = item4.Cells["col_page"].Value.ToString();
                dataRow2["control"] = item4.Cells["col_control"].Value.ToString();
                DgDatatables.Tables[NowTable].Rows.Add(dataRow2);
            }
        }
        ListboxContentstr.Clear();
        foreach (object item5 in listBox1.Items)
        {
            ListboxContentstr.Add(item5.ToString());
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

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count != 0 && NowTable != "")
        {
            string text = "";
            if (dataGridView1.Rows.Count != 0)
            {
                bool flag = false;
                foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    text = text + "update " + NowTable.ToString();
                    text += " set ";
                    foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(item2.Cells[0].Value))
                        {
                            if (item2.Cells["col_page"].Value.ToString() != "{常量}")
                            {
                                string text2 = text;
                                text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["col_control"].Value.ToString() + "',";
                            }
                            else
                            {
                                string text3 = text;
                                text = text3 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column4"].Value.ToString() + "',";
                            }
                        }
                    }
                    text = text.Substring(0, text.Length - 1);
                    if (!ResultStrings.ContainsKey(NowTable))
                    {
                        ResultStrings.Add(NowTable, new string[2] { text, "" });
                    }
                    else
                    {
                        ResultStrings[NowTable] = new string[2]
                        {
                            text,
                            ResultStrings[NowTable][1]
                        };
                    }
                }
                else if (ResultStrings.ContainsKey(NowTable))
                {
                    ResultStrings.Remove(NowTable);
                }
            }
            if (!DgDatatables.Tables.Contains(NowTable))
            {
                DataTable dataTable = new(NowTable);
                dataTable.Columns.Add("ck1");
                dataTable.Columns.Add("varname");
                dataTable.Columns.Add("vartype");
                dataTable.Columns.Add("value");
                dataTable.Columns.Add("page");
                dataTable.Columns.Add("control");
                foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ck1"] = item3.Cells[0].Value.ToString();
                    dataRow["varname"] = item3.Cells[1].Value.ToString();
                    dataRow["vartype"] = item3.Cells[2].Value.ToString();
                    dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
                    dataRow["page"] = item3.Cells["col_page"].Value.ToString();
                    dataRow["control"] = item3.Cells["col_control"].Value.ToString();
                    dataTable.Rows.Add(dataRow);
                }
                DgDatatables.Tables.Add(dataTable);
            }
            else
            {
                DgDatatables.Tables[NowTable].Rows.Clear();
                foreach (DataGridViewRow item4 in (IEnumerable)dataGridView1.Rows)
                {
                    DataRow dataRow2 = DgDatatables.Tables[NowTable].NewRow();
                    dataRow2["ck1"] = item4.Cells[0].Value.ToString();
                    dataRow2["varname"] = item4.Cells[1].Value.ToString();
                    dataRow2["vartype"] = item4.Cells[2].Value.ToString();
                    dataRow2["value"] = ((item4.Cells["Column4"].Value == null) ? "" : item4.Cells["Column4"].Value.ToString());
                    dataRow2["page"] = item4.Cells["col_page"].Value.ToString();
                    dataRow2["control"] = item4.Cells["col_control"].Value.ToString();
                    DgDatatables.Tables[NowTable].Rows.Add(dataRow2);
                }
            }
        }
        dataGridView1.Rows.Clear();
        if (listBox1.SelectedItem == null)
        {
            return;
        }
        if (DgDatatables.Tables.Contains(listBox1.SelectedItem.ToString()) && !DgDatatables.Tables[listBox1.SelectedItem.ToString()].Columns.Contains("page"))
        {
            DgDatatables.Tables[listBox1.SelectedItem.ToString()].Columns.Add("page");
            DgDatatables.Tables[listBox1.SelectedItem.ToString()].Columns.Add("control");
            foreach (DataRow row in DgDatatables.Tables[listBox1.SelectedItem.ToString()].Rows)
            {
                if (row["vartype"].ToString() != "{常量}")
                {
                    if (row["vartype"].ToString().IndexOf('.') != -1)
                    {
                        row["page"] = "{" + row["vartype"].ToString().Substring(1, row["vartype"].ToString().Length - 2).Substring(0, row["vartype"].ToString().Substring(1, row["vartype"].ToString().Length - 2).IndexOf('.')) + "}";
                        row["control"] = row["vartype"];
                    }
                    else
                    {
                        row["page"] = "{内部变量}";
                        row["control"] = row["vartype"];
                    }
                }
                else
                {
                    row["page"] = "{常量}";
                    row["control"] = "";
                }
            }
        }
        if (DgDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
        {
            foreach (DataRow row2 in DgDatatables.Tables[listBox1.SelectedItem.ToString()].Rows)
            {
                dataGridView1.Rows.Add(Convert.ToBoolean(row2["ck1"]), row2["varname"].ToString(), row2["vartype"].ToString(), row2["page"].ToString(), row2["control"].ToString(), row2["value"].ToString());
            }
        }
        else
        {
            string text4 = listBox1.SelectedItem.ToString();
            if (DBOperationGlobal.conn.State != ConnectionState.Open)
            {
                DBOperationGlobal.conn.Open();
            }
            DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text4 + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
            DataSet dataSet = new();
            DBOperationGlobal.adapter.Fill(dataSet);
            foreach (DataRow row3 in dataSet.Tables[0].Rows)
            {
                dataGridView1.Rows.Add(true, row3["COLUMN_NAME"], "{常量}", "", "", "");
            }
        }
        NowTable = listBox1.SelectedItem.ToString();
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void btn_tiaojian_Click(object sender, EventArgs e)
    {
        RulesForm rulesForm = new();
        if (listBox1.SelectedItem == null)
        {
            return;
        }
        if (WhereDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
        {
            rulesForm.RecordDatatable.Columns.Clear();
            rulesForm.RecordDatatable.Columns.Add("逻辑关系");
            rulesForm.RecordDatatable.Columns.Add("字段");
            rulesForm.RecordDatatable.Columns.Add("匹配运算符");
            rulesForm.RecordDatatable.Columns.Add("值");
            foreach (DataRow row in WhereDatatables.Tables[listBox1.SelectedItem.ToString()].Rows)
            {
                DataRow dataRow2 = rulesForm.RecordDatatable.NewRow();
                dataRow2["逻辑关系"] = row["逻辑关系"].ToString();
                dataRow2["字段"] = row["字段"].ToString();
                dataRow2["匹配运算符"] = row["匹配运算符"].ToString();
                dataRow2["值"] = row["值"].ToString();
                rulesForm.RecordDatatable.Rows.Add(dataRow2);
            }
        }
        else if (rulesForm.RecordDatatable.Columns.Count == 0)
        {
            rulesForm.RecordDatatable.Columns.Add("逻辑关系");
            rulesForm.RecordDatatable.Columns.Add("字段");
            rulesForm.RecordDatatable.Columns.Add("匹配运算符");
            rulesForm.RecordDatatable.Columns.Add("值");
        }
        if (DBOperationGlobal.conn.State != ConnectionState.Open)
        {
            DBOperationGlobal.conn.Open();
        }
        DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + listBox1.SelectedItem.ToString() + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
        DataSet dataSet = new();
        DBOperationGlobal.adapter.Fill(dataSet);
        foreach (DataRow row2 in dataSet.Tables[0].Rows)
        {
            rulesForm.vars.Add(row2["COLUMN_NAME"].ToString());
        }
        if (rulesForm.ShowDialog() != DialogResult.OK || rulesForm.RecordDatatable.Rows.Count <= 0)
        {
            return;
        }
        if (WhereDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
        {
            WhereDatatables.Tables.Remove(listBox1.SelectedItem.ToString());
        }
        DataTable dataTable = new()
        {
            TableName = listBox1.SelectedItem.ToString()
        };
        dataTable.Columns.Add("逻辑关系");
        dataTable.Columns.Add("字段");
        dataTable.Columns.Add("匹配运算符");
        dataTable.Columns.Add("值");
        foreach (DataRow row3 in rulesForm.RecordDatatable.Rows)
        {
            DataRow dataRow5 = dataTable.NewRow();
            dataRow5["逻辑关系"] = row3["逻辑关系"].ToString();
            dataRow5["字段"] = row3["字段"].ToString();
            dataRow5["匹配运算符"] = row3["匹配运算符"].ToString();
            dataRow5["值"] = row3["值"].ToString();
            dataTable.Rows.Add(dataRow5);
        }
        WhereDatatables.Tables.Add(dataTable);
        string text = "";
        if (rulesForm.RecordDatatable.Rows.Count > 0)
        {
            text = ((!(rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")) ? (" where " + rulesForm.RecordDatatable.Rows[0]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '%" + rulesForm.RecordDatatable.Rows[0]["值"].ToString() + "%'") : (" where " + rulesForm.RecordDatatable.Rows[0]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '" + rulesForm.RecordDatatable.Rows[0]["值"].ToString() + "'"));
        }
        if (rulesForm.RecordDatatable.Rows.Count > 1)
        {
            for (int i = 1; i < rulesForm.RecordDatatable.Rows.Count; i++)
            {
                text = ((!(rulesForm.RecordDatatable.Rows[i]["逻辑关系"].ToString() == "并且")) ? (text + " OR ") : (text + " AND "));
                if (rulesForm.RecordDatatable.Rows[i]["匹配运算符"].ToString() != "like")
                {
                    string text2 = text;
                    text = text2 + rulesForm.RecordDatatable.Rows[i]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '" + rulesForm.RecordDatatable.Rows[i]["值"].ToString() + "'";
                }
                else
                {
                    string text3 = text;
                    text = text3 + rulesForm.RecordDatatable.Rows[i]["字段"].ToString() + " " + rulesForm.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '%" + rulesForm.RecordDatatable.Rows[i]["值"].ToString() + "%'";
                }
            }
        }
        if (ResultStrings.ContainsKey(listBox1.SelectedItem.ToString()))
        {
            ResultStrings[listBox1.SelectedItem.ToString()][1] = text;
            return;
        }
        ResultStrings.Add(listBox1.SelectedItem.ToString(), new string[2] { "", text });
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        Ansync = checkBox1.Checked;
    }

    private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
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
        if (e.ColumnIndex == 3)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            cbx_page.Visible = true;
            cbx_page.Location = new Point(num7 + num5 + num3 + num, num8 + num6 + num4 + num2);
            cbx_page.Width = dataGridView1.Columns[3].Width;
            cbx_page.DroppedDown = true;
        }
        if (e.ColumnIndex != 4 || e.RowIndex == -1 || !(dataGridView1.Rows[e.RowIndex].Cells["col_page"].Value.ToString() != ""))
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
        cbx_control.Location = new Point(num7 + num5 + num3 + num, num8 + num6 + num4 + num2);
        cbx_control.Width = dataGridView1.Columns[4].Width;
        cbx_control.DroppedDown = true;
    }

    private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
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
            if (dataGridView1.SelectedCells.Count == 1 && dataGridView1.SelectedCells[0].ColumnIndex == 3)
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

    private void cbx_control_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dataGridView1.SelectedCells[0].Value = cbx_control.SelectedItem.ToString();
            cbx_control.Visible = false;
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

    private void btn_alladd_Click(object sender, EventArgs e)
    {
        if (treeView1.Nodes.Count == 0)
        {
            return;
        }
        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
        {
            if (!listBox1.Items.Contains(node.Text))
            {
                listBox1.Items.Add(node.Text);
            }
        }
    }

    private void btn_allremove_Click(object sender, EventArgs e)
    {
        foreach (object item in listBox1.Items)
        {
            if (DgDatatables.Tables.Contains(item.ToString()))
            {
                DgDatatables.Tables.Remove(item.ToString());
            }
            if (WhereDatatables.Tables.Contains(item.ToString()))
            {
                WhereDatatables.Tables.Remove(item.ToString());
            }
            if (ResultStrings.ContainsKey(item.ToString()))
            {
                ResultStrings.Remove(item.ToString());
            }
        }
        if (listBox1.Items.Count != 0)
        {
            listBox1.Items.Clear();
        }
        NowTable = "";
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.A && textBox1.Text != "")
        {
            textBox1.SelectAll();
        }
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.treeView1 = new System.Windows.Forms.TreeView();
        this.btn_addtable = new System.Windows.Forms.Button();
        this.btn_removetable = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.btn_tiaojian = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
        this.col_page = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.col_control = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.label5 = new System.Windows.Forms.Label();
        this.btn_setresult = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.cbx_control = new System.Windows.Forms.ComboBox();
        this.cbx_page = new System.Windows.Forms.ComboBox();
        this.btn_allremove = new System.Windows.Forms.Button();
        this.btn_alladd = new System.Windows.Forms.Button();
        this.panel1.SuspendLayout();
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
        this.label2.Text = "更新";
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
        this.treeView1.Size = new System.Drawing.Size(190, 417);
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
        this.panel1.Controls.Add(this.listBox1);
        this.panel1.Controls.Add(this.btn_tiaojian);
        this.panel1.Controls.Add(this.textBox1);
        this.panel1.Controls.Add(this.label7);
        this.panel1.Controls.Add(this.dataGridView1);
        this.panel1.Controls.Add(this.label5);
        this.panel1.Controls.Add(this.btn_setresult);
        this.panel1.Location = new System.Drawing.Point(294, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(457, 417);
        this.panel1.TabIndex = 5;
        this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(377, 9);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(72, 16);
        this.checkBox1.TabIndex = 5;
        this.checkBox1.Text = "异步操作";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new System.Drawing.Point(5, 28);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new System.Drawing.Size(366, 52);
        this.listBox1.TabIndex = 0;
        this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
        this.btn_tiaojian.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btn_tiaojian.Location = new System.Drawing.Point(377, 28);
        this.btn_tiaojian.Name = "btn_tiaojian";
        this.btn_tiaojian.Size = new System.Drawing.Size(75, 23);
        this.btn_tiaojian.TabIndex = 1;
        this.btn_tiaojian.Text = "条件设计";
        this.btn_tiaojian.UseVisualStyleBackColor = true;
        this.btn_tiaojian.Click += new System.EventHandler(btn_tiaojian_Click);
        this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox1.Location = new System.Drawing.Point(62, 315);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBox1.Size = new System.Drawing.Size(390, 97);
        this.textBox1.TabIndex = 4;
        this.textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
        this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
        this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(3, 315);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 4;
        this.label7.Text = "命令预览";
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(this.Column1, this.Column2, this.Column3, this.col_page, this.col_control, this.Column4);
        this.dataGridView1.Location = new System.Drawing.Point(5, 86);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.Size = new System.Drawing.Size(447, 223);
        this.dataGridView1.TabIndex = 2;
        this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
        this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
        this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridView1_DataError);
        this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
        this.Column1.HeaderText = "";
        this.Column1.Name = "Column1";
        this.Column1.Width = 25;
        this.Column2.HeaderText = "字段";
        this.Column2.Name = "Column2";
        this.Column2.ReadOnly = true;
        this.Column2.Width = 160;
        this.Column3.HeaderText = "类型";
        this.Column3.Name = "Column3";
        this.Column3.Visible = false;
        this.Column3.Width = 175;
        this.col_page.HeaderText = "页面";
        this.col_page.Name = "col_page";
        this.col_page.ReadOnly = true;
        this.col_page.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.col_control.HeaderText = "控件";
        this.col_control.Name = "col_control";
        this.col_control.ReadOnly = true;
        this.col_control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.Column4.HeaderText = "值";
        this.Column4.Name = "Column4";
        this.Column4.Width = 45;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(3, 13);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "已选表";
        this.label5.Click += new System.EventHandler(label1_Click);
        this.btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.btn_setresult.Location = new System.Drawing.Point(5, 335);
        this.btn_setresult.Name = "btn_setresult";
        this.btn_setresult.Size = new System.Drawing.Size(51, 23);
        this.btn_setresult.TabIndex = 3;
        this.btn_setresult.Text = "生成";
        this.btn_setresult.UseVisualStyleBackColor = true;
        this.btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
        this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button8.Location = new System.Drawing.Point(588, 435);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 23);
        this.button8.TabIndex = 6;
        this.button8.Text = "确定";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(button8_Click);
        this.button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button9.Location = new System.Drawing.Point(669, 435);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(75, 23);
        this.button9.TabIndex = 7;
        this.button9.Text = "取消";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        this.cbx_control.FormattingEnabled = true;
        this.cbx_control.Location = new System.Drawing.Point(211, 206);
        this.cbx_control.Name = "cbx_control";
        this.cbx_control.Size = new System.Drawing.Size(121, 20);
        this.cbx_control.TabIndex = 11;
        this.cbx_control.Visible = false;
        this.cbx_control.SelectedIndexChanged += new System.EventHandler(cbx_control_SelectedIndexChanged);
        this.cbx_page.FormattingEnabled = true;
        this.cbx_page.Location = new System.Drawing.Point(211, 180);
        this.cbx_page.Name = "cbx_page";
        this.cbx_page.Size = new System.Drawing.Size(121, 20);
        this.cbx_page.TabIndex = 10;
        this.cbx_page.Visible = false;
        this.cbx_page.SelectedIndexChanged += new System.EventHandler(cbx_page_SelectedIndexChanged);
        this.btn_allremove.Location = new System.Drawing.Point(213, 172);
        this.btn_allremove.Name = "btn_allremove";
        this.btn_allremove.Size = new System.Drawing.Size(75, 23);
        this.btn_allremove.TabIndex = 4;
        this.btn_allremove.Text = "全移除";
        this.btn_allremove.UseVisualStyleBackColor = true;
        this.btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
        this.btn_alladd.Location = new System.Drawing.Point(213, 143);
        this.btn_alladd.Name = "btn_alladd";
        this.btn_alladd.Size = new System.Drawing.Size(75, 23);
        this.btn_alladd.TabIndex = 3;
        this.btn_alladd.Text = "全选表";
        this.btn_alladd.UseVisualStyleBackColor = true;
        this.btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(763, 469);
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
        base.Name = "DBUpdateForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "更新数据";
        base.Load += new System.EventHandler(DBSelectForm_Load);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
