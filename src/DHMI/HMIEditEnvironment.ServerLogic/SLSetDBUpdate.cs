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
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetDBUpdate : Form
{
	public List<int> SafeRegion = new();

	public byte[] OtherData;

	public bool Ansync;

	public string ResultSQL = "";

	private string NowTable = "";

	private Dictionary<string, string[]> ResultStrings = new();

	private List<string> ListboxContentstr = new();

	private DataSet DgDatatables = new();

	private DataSet WhereDatatables = new();

	private readonly List<string> CanUseVars;

	public ServerLogicItem result;

	private Label label1;

	private Label label2;

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

	private CheckBox checkBox1;

	private Button btn_allremove;

	private Button btn_alladd;

	private Button button1;

	private TextBox textBox2;

	private Label label6;

	private DataGridViewCheckBoxColumn Column1;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewComboBoxColumn Column3;

	private DataGridViewTextBoxColumn Column4;

	private Button button2;

	private TextBox textBox3;

	private Label label3;

	public ServerLogicItem Result
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

	public SLSetDBUpdate(List<string> canusevars)
	{
		InitializeComponent();
		CanUseVars = canusevars;
	}

	public SLSetDBUpdate(List<string> canusevars, ServerLogicItem item)
	{
		InitializeComponent();
		CanUseVars = canusevars;
		try
		{
			result = item;
			textBox2.Text = result.ConditionalExpression;
			ResultSQL = result.DataDict["ResultSQL"] as string;
			Ansync = (bool)result.DataDict["Ansync"];
			OtherData = result.DataDict["OtherData"] as byte[];
			textBox3.Text = result.DataDict["ResultTo"] as string;
		}
		catch
		{
		}
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
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		return array;
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
		Column3.Items.Add("{常量}");
		foreach (string canUseVar in CanUseVars)
		{
			Column3.Items.Add("{" + canUseVar + "}");
		}
		try
		{
			DeSerialize(OtherData);
			checkBox1.Checked = Ansync;
			if (ListboxContentstr.Count != 0)
			{
				listBox1.Items.Clear();
				foreach (string item2 in ListboxContentstr)
				{
					listBox1.Items.Add(item2);
				}
			}
			listBox1.SelectedItem = NowTable;
			if (DgDatatables.Tables.Contains(NowTable))
			{
				dataGridView1.Rows.Clear();
				foreach (DataRow row in DgDatatables.Tables[NowTable].Rows)
				{
					dataGridView1.Rows.Add(Convert.ToBoolean(row["ck1"]), row["varname"].ToString(), row["vartype"].ToString(), row["value"].ToString());
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
						if (item2.Cells["Column3"].Value.ToString() != "{常量}")
						{
							string text2 = text;
							text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column3"].Value.ToString() + "',";
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
			foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["ck1"] = item3.Cells[0].Value.ToString();
				dataRow["varname"] = item3.Cells[1].Value.ToString();
				dataRow["vartype"] = item3.Cells[2].Value.ToString();
				dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
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
							if (item2.Cells["Column3"].Value.ToString() != "{常量}")
							{
								string text2 = text;
								text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column3"].Value.ToString() + "',";
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
			foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["ck1"] = item3.Cells[0].Value.ToString();
				dataRow["varname"] = item3.Cells[1].Value.ToString();
				dataRow["vartype"] = item3.Cells[2].Value.ToString();
				dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
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
        result = new ServerLogicItem
        {
            LogicType = "更新数据",
            ConditionalExpression = textBox2.Text,
            DataDict = new Dictionary<string, object>
        {
            { "ResultSQL", ResultSQL },
            { "Ansync", Ansync },
            { "OtherData", OtherData },
            { "ResultTo", textBox3.Text }
        }
        };
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
							if (item2.Cells["Column3"].Value.ToString() != "{常量}")
							{
								string text2 = text;
								text = text2 + item2.Cells[1].Value.ToString() + "='" + item2.Cells["Column3"].Value.ToString() + "',";
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
				foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["ck1"] = item3.Cells[0].Value.ToString();
					dataRow["varname"] = item3.Cells[1].Value.ToString();
					dataRow["vartype"] = item3.Cells[2].Value.ToString();
					dataRow["value"] = ((item3.Cells["Column4"].Value == null) ? "" : item3.Cells["Column4"].Value.ToString());
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
					DgDatatables.Tables[NowTable].Rows.Add(dataRow2);
				}
			}
		}
		dataGridView1.Rows.Clear();
		if (listBox1.SelectedItem == null)
		{
			return;
		}
		if (DgDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
		{
			foreach (DataRow row in DgDatatables.Tables[listBox1.SelectedItem.ToString()].Rows)
			{
				dataGridView1.Rows.Add(Convert.ToBoolean(row["ck1"]), row["varname"].ToString(), row["vartype"].ToString(), row["value"].ToString());
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
			foreach (DataRow row2 in dataSet.Tables[0].Rows)
			{
				dataGridView1.Rows.Add(true, row2["COLUMN_NAME"], "{常量}", "");
			}
		}
		NowTable = listBox1.SelectedItem.ToString();
	}

	private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void btn_tiaojian_Click(object sender, EventArgs e)
	{
		SLDBRules sLDBRules = new(CanUseVars);
		if (listBox1.SelectedItem == null)
		{
			return;
		}
		if (WhereDatatables.Tables.Contains(listBox1.SelectedItem.ToString()))
		{
			sLDBRules.RecordDatatable.Columns.Clear();
			sLDBRules.RecordDatatable.Columns.Add("逻辑关系");
			sLDBRules.RecordDatatable.Columns.Add("字段");
			sLDBRules.RecordDatatable.Columns.Add("匹配运算符");
			sLDBRules.RecordDatatable.Columns.Add("值");
			foreach (DataRow row in WhereDatatables.Tables[listBox1.SelectedItem.ToString()].Rows)
			{
				DataRow dataRow2 = sLDBRules.RecordDatatable.NewRow();
				dataRow2["逻辑关系"] = row["逻辑关系"].ToString();
				dataRow2["字段"] = row["字段"].ToString();
				dataRow2["匹配运算符"] = row["匹配运算符"].ToString();
				dataRow2["值"] = row["值"].ToString();
				sLDBRules.RecordDatatable.Rows.Add(dataRow2);
			}
		}
		else if (sLDBRules.RecordDatatable.Columns.Count == 0)
		{
			sLDBRules.RecordDatatable.Columns.Add("逻辑关系");
			sLDBRules.RecordDatatable.Columns.Add("字段");
			sLDBRules.RecordDatatable.Columns.Add("匹配运算符");
			sLDBRules.RecordDatatable.Columns.Add("值");
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
			sLDBRules.vars.Add(row2["COLUMN_NAME"].ToString());
		}
		if (sLDBRules.ShowDialog() != DialogResult.OK || sLDBRules.RecordDatatable.Rows.Count <= 0)
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
		foreach (DataRow row3 in sLDBRules.RecordDatatable.Rows)
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
		if (sLDBRules.RecordDatatable.Rows.Count > 0)
		{
			text = ((!(sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")) ? (" where " + sLDBRules.RecordDatatable.Rows[0]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '%" + sLDBRules.RecordDatatable.Rows[0]["值"].ToString() + "%'") : (" where " + sLDBRules.RecordDatatable.Rows[0]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '" + sLDBRules.RecordDatatable.Rows[0]["值"].ToString() + "'"));
		}
		if (sLDBRules.RecordDatatable.Rows.Count > 1)
		{
			for (int i = 1; i < sLDBRules.RecordDatatable.Rows.Count; i++)
			{
				text = ((!(sLDBRules.RecordDatatable.Rows[i]["逻辑关系"].ToString() == "并且")) ? (text + " OR ") : (text + " AND "));
				if (sLDBRules.RecordDatatable.Rows[i]["匹配运算符"].ToString() != "like")
				{
					string text2 = text;
					text = text2 + sLDBRules.RecordDatatable.Rows[i]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '" + sLDBRules.RecordDatatable.Rows[i]["值"].ToString() + "'";
				}
				else
				{
					string text3 = text;
					text = text3 + sLDBRules.RecordDatatable.Rows[i]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '%" + sLDBRules.RecordDatatable.Rows[i]["值"].ToString() + "%'";
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
	}

	private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
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

	private void button1_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(CanUseVars, textBox2.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox2.Text = sLChooseVar.Result;
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(CanUseVars, textBox3.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox3.Text = sLChooseVar.Result;
		}
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
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		treeView1 = new System.Windows.Forms.TreeView();
		btn_addtable = new System.Windows.Forms.Button();
		btn_removetable = new System.Windows.Forms.Button();
		panel1 = new System.Windows.Forms.Panel();
		button2 = new System.Windows.Forms.Button();
		textBox3 = new System.Windows.Forms.TextBox();
		label3 = new System.Windows.Forms.Label();
		checkBox1 = new System.Windows.Forms.CheckBox();
		listBox1 = new System.Windows.Forms.ListBox();
		btn_tiaojian = new System.Windows.Forms.Button();
		textBox1 = new System.Windows.Forms.TextBox();
		label7 = new System.Windows.Forms.Label();
		dataGridView1 = new System.Windows.Forms.DataGridView();
		Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		label5 = new System.Windows.Forms.Label();
		btn_setresult = new System.Windows.Forms.Button();
		button8 = new System.Windows.Forms.Button();
		button9 = new System.Windows.Forms.Button();
		btn_allremove = new System.Windows.Forms.Button();
		btn_alladd = new System.Windows.Forms.Button();
		button1 = new System.Windows.Forms.Button();
		textBox2 = new System.Windows.Forms.TextBox();
		label6 = new System.Windows.Forms.Label();
		panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
		base.SuspendLayout();
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(13, 13);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(59, 12);
		label1.TabIndex = 0;
		label1.Text = "当前操作:";
		label1.Click += new System.EventHandler(label1_Click);
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(78, 13);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(29, 12);
		label2.TabIndex = 0;
		label2.Text = "更新";
		treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		treeView1.Location = new System.Drawing.Point(15, 40);
		treeView1.Name = "treeView1";
		treeView1.Size = new System.Drawing.Size(190, 356);
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
		panel1.Controls.Add(button2);
		panel1.Controls.Add(textBox3);
		panel1.Controls.Add(label3);
		panel1.Controls.Add(checkBox1);
		panel1.Controls.Add(listBox1);
		panel1.Controls.Add(btn_tiaojian);
		panel1.Controls.Add(textBox1);
		panel1.Controls.Add(label7);
		panel1.Controls.Add(dataGridView1);
		panel1.Controls.Add(label5);
		panel1.Controls.Add(btn_setresult);
		panel1.Location = new System.Drawing.Point(294, 40);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(457, 328);
		panel1.TabIndex = 3;
		button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button2.Location = new System.Drawing.Point(418, 210);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(31, 23);
		button2.TabIndex = 4;
		button2.Text = "...";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		textBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox3.Location = new System.Drawing.Point(62, 211);
		textBox3.Margin = new System.Windows.Forms.Padding(2);
		textBox3.Name = "textBox3";
		textBox3.Size = new System.Drawing.Size(352, 21);
		textBox3.TabIndex = 3;
		label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(3, 215);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(53, 12);
		label3.TabIndex = 17;
		label3.Text = "影响行数";
		checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		checkBox1.AutoSize = true;
		checkBox1.Location = new System.Drawing.Point(377, 9);
		checkBox1.Name = "checkBox1";
		checkBox1.Size = new System.Drawing.Size(72, 16);
		checkBox1.TabIndex = 5;
		checkBox1.Text = "异步操作";
		checkBox1.UseVisualStyleBackColor = true;
		checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listBox1.FormattingEnabled = true;
		listBox1.ItemHeight = 12;
		listBox1.Location = new System.Drawing.Point(5, 28);
		listBox1.Name = "listBox1";
		listBox1.Size = new System.Drawing.Size(366, 52);
		listBox1.TabIndex = 0;
		listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		btn_tiaojian.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		btn_tiaojian.Location = new System.Drawing.Point(377, 28);
		btn_tiaojian.Name = "btn_tiaojian";
		btn_tiaojian.Size = new System.Drawing.Size(75, 23);
		btn_tiaojian.TabIndex = 1;
		btn_tiaojian.Text = "条件设计";
		btn_tiaojian.UseVisualStyleBackColor = true;
		btn_tiaojian.Click += new System.EventHandler(btn_tiaojian_Click);
		textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox1.Location = new System.Drawing.Point(62, 239);
		textBox1.Multiline = true;
		textBox1.Name = "textBox1";
		textBox1.ReadOnly = true;
		textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		textBox1.Size = new System.Drawing.Size(390, 84);
		textBox1.TabIndex = 6;
		textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label7.AutoSize = true;
		label7.Location = new System.Drawing.Point(3, 239);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(53, 12);
		label7.TabIndex = 4;
		label7.Text = "命令预览";
		dataGridView1.AllowUserToAddRows = false;
		dataGridView1.AllowUserToDeleteRows = false;
		dataGridView1.AllowUserToResizeRows = false;
		dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4);
		dataGridView1.Location = new System.Drawing.Point(5, 86);
		dataGridView1.Name = "dataGridView1";
		dataGridView1.RowHeadersVisible = false;
		dataGridView1.RowTemplate.Height = 23;
		dataGridView1.Size = new System.Drawing.Size(447, 120);
		dataGridView1.TabIndex = 2;
		dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
		dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
		dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridView1_DataError);
		dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
		Column1.HeaderText = "";
		Column1.Name = "Column1";
		Column1.Width = 25;
		Column2.HeaderText = "字段";
		Column2.Name = "Column2";
		Column2.ReadOnly = true;
		Column2.Width = 160;
		Column3.HeaderText = "类型";
		Column3.Name = "Column3";
		Column3.Width = 175;
		Column4.HeaderText = "值";
		Column4.Name = "Column4";
		Column4.Width = 65;
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(3, 13);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(41, 12);
		label5.TabIndex = 0;
		label5.Text = "已选表";
		label5.Click += new System.EventHandler(label1_Click);
		btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		btn_setresult.Location = new System.Drawing.Point(5, 259);
		btn_setresult.Name = "btn_setresult";
		btn_setresult.Size = new System.Drawing.Size(51, 23);
		btn_setresult.TabIndex = 5;
		btn_setresult.Text = "生成";
		btn_setresult.UseVisualStyleBackColor = true;
		btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
		button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button8.Location = new System.Drawing.Point(588, 374);
		button8.Name = "button8";
		button8.Size = new System.Drawing.Size(75, 23);
		button8.TabIndex = 7;
		button8.Text = "确定";
		button8.UseVisualStyleBackColor = true;
		button8.Click += new System.EventHandler(button8_Click);
		button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button9.Location = new System.Drawing.Point(669, 374);
		button9.Name = "button9";
		button9.Size = new System.Drawing.Size(75, 23);
		button9.TabIndex = 8;
		button9.Text = "取消";
		button9.UseVisualStyleBackColor = true;
		button9.Click += new System.EventHandler(button9_Click);
		btn_allremove.Location = new System.Drawing.Point(213, 172);
		btn_allremove.Name = "btn_allremove";
		btn_allremove.Size = new System.Drawing.Size(75, 23);
		btn_allremove.TabIndex = 4;
		btn_allremove.Text = "全移除";
		btn_allremove.UseVisualStyleBackColor = true;
		btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
		btn_alladd.Location = new System.Drawing.Point(213, 143);
		btn_alladd.Name = "btn_alladd";
		btn_alladd.Size = new System.Drawing.Size(75, 23);
		btn_alladd.TabIndex = 3;
		btn_alladd.Text = "全选表";
		btn_alladd.UseVisualStyleBackColor = true;
		btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
		button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button1.Location = new System.Drawing.Point(721, 7);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(30, 23);
		button1.TabIndex = 6;
		button1.Text = "...";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox2.Location = new System.Drawing.Point(365, 10);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(350, 21);
		textBox2.TabIndex = 5;
		textBox2.Text = "true";
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(292, 13);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(71, 12);
		label6.TabIndex = 10;
		label6.Text = "条件表达式:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(763, 408);
		base.Controls.Add(button1);
		base.Controls.Add(textBox2);
		base.Controls.Add(label6);
		base.Controls.Add(btn_allremove);
		base.Controls.Add(btn_alladd);
		base.Controls.Add(panel1);
		base.Controls.Add(btn_removetable);
		base.Controls.Add(btn_addtable);
		base.Controls.Add(treeView1);
		base.Controls.Add(label2);
		base.Controls.Add(button9);
		base.Controls.Add(button8);
		base.Controls.Add(label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "SLSetDBUpdate";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "更新数据";
		base.Load += new System.EventHandler(DBSelectForm_Load);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
