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
using HMIEditEnvironment.Animation;
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetDBSelect : Form
{
	public List<string> BindVars;

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

	private readonly List<string> CanUseVars;

	public ServerLogicItem result;

	private IContainer components;

	private Label label1;

	private Label label2;

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

	private Label label3;

	private TextBox textBox2;

	private Button button5;

	private GroupBox groupBox1;

	private Label label4;

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

	public SLSetDBSelect(List<string> canusevars)
	{
		InitializeComponent();

		DgDatatable.Columns.Add("ck1");
		DgDatatable.Columns.Add("tbname");
		DgDatatable.Columns.Add("varname");
		DgDatatable.Columns.Add("asname");
		DgDatatable.Columns.Add("ck2");
		DgDatatable.Columns.Add("jointo");
		CanUseVars = canusevars;
		comboBox1.Items.AddRange(canusevars.ToArray());
		if (comboBox1.Items.Count != 0)
		{
			comboBox1.SelectedIndex = 0;
		}
	}

	public SLSetDBSelect(List<string> canusevars, ServerLogicItem item)
	{
		InitializeComponent();

		DgDatatable.Columns.Add("ck1");
		DgDatatable.Columns.Add("tbname");
		DgDatatable.Columns.Add("varname");
		DgDatatable.Columns.Add("asname");
		DgDatatable.Columns.Add("ck2");
		DgDatatable.Columns.Add("jointo");
		CanUseVars = canusevars;
		comboBox1.Items.AddRange(canusevars.ToArray());
		if (comboBox1.Items.Count != 0)
		{
			comboBox1.SelectedIndex = 0;
		}
		result = item;
		textBox2.Text = result.ConditionalExpression;
		ResultSQL = result.DataDict["ResultSQL"] as string;
		ResultTo = result.DataDict["ResultTo"] as string;
		Ansync = (bool)result.DataDict["Ansync"];
		OtherData = result.DataDict["OtherData"] as byte[];
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
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		return array;
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
					ShowListBox2Count();
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
        SLDBRules sLDBRules = new(CanUseVars)
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
				sLDBRules.vars.Add(item.ToString() + "." + row["COLUMN_NAME"].ToString());
			}
		}
		if (sLDBRules.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (sLDBRules.RecordDatatable.Rows.Count > 0)
		{
			WhereDatatable = sLDBRules.RecordDatatable;
			if (sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")
			{
				Wherestr = " where " + sLDBRules.RecordDatatable.Rows[0]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '" + sLDBRules.RecordDatatable.Rows[0]["值"].ToString() + "'";
			}
			else
			{
				Wherestr = " where " + sLDBRules.RecordDatatable.Rows[0]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() + " '%" + sLDBRules.RecordDatatable.Rows[0]["值"].ToString() + "%'";
			}
			if (sLDBRules.RecordDatatable.Rows.Count <= 1)
			{
				return;
			}
			for (int i = 1; i < sLDBRules.RecordDatatable.Rows.Count; i++)
			{
				if (sLDBRules.RecordDatatable.Rows[i]["逻辑关系"].ToString() == "并且")
				{
					Wherestr += " AND ";
				}
				else
				{
					Wherestr += " OR ";
				}
				if (sLDBRules.RecordDatatable.Rows[0]["匹配运算符"].ToString() != "like")
				{
					string wherestr = Wherestr;
					Wherestr = wherestr + sLDBRules.RecordDatatable.Rows[i]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '" + sLDBRules.RecordDatatable.Rows[i]["值"].ToString() + "'";
				}
				else
				{
					string wherestr2 = Wherestr;
					Wherestr = wherestr2 + sLDBRules.RecordDatatable.Rows[i]["字段"].ToString() + " " + sLDBRules.RecordDatatable.Rows[i]["匹配运算符"].ToString() + " '%" + sLDBRules.RecordDatatable.Rows[i]["值"].ToString() + "%'";
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
        result = new ServerLogicItem
        {
            LogicType = "查询数据",
            ConditionalExpression = textBox2.Text,
            DataDict = new Dictionary<string, object>
        {
            { "ResultSQL", ResultSQL },
            { "ResultTo", ResultTo },
            { "Ansync", Ansync },
            { "OtherData", OtherData }
        }
        };
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
		if (comboBox1.Text != "{...}" && comboBox1.Text != "" && !listBox2.Items.Contains(comboBox1.Text))
		{
			listBox2.Items.Add(comboBox1.Text);
			ShowListBox2Count();
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
			ShowListBox2Count();
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

	private void ShowListBox2Count()
	{
		label4.Text = listBox2.Items.Count.ToString();
	}

	private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void treeView1_DoubleClick(object sender, EventArgs e)
	{
		btnadd_Click(sender, e);
	}

	private void button5_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(CanUseVars, textBox2.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox2.Text = sLChooseVar.Result;
		}
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
		components = new System.ComponentModel.Container();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		treeView1 = new System.Windows.Forms.TreeView();
		btn_addtable = new System.Windows.Forms.Button();
		btn_removetable = new System.Windows.Forms.Button();
		btn_alladd = new System.Windows.Forms.Button();
		btn_allremove = new System.Windows.Forms.Button();
		panel1 = new System.Windows.Forms.Panel();
		listBox2 = new System.Windows.Forms.ListBox();
		contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
		上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		groupBox1 = new System.Windows.Forms.GroupBox();
		label4 = new System.Windows.Forms.Label();
		listBox1 = new System.Windows.Forms.ListBox();
		textBox1 = new System.Windows.Forms.TextBox();
		comboBox1 = new System.Windows.Forms.ComboBox();
		label7 = new System.Windows.Forms.Label();
		label6 = new System.Windows.Forms.Label();
		dataGridView1 = new System.Windows.Forms.DataGridView();
		Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		label5 = new System.Windows.Forms.Label();
		btn_sort = new System.Windows.Forms.Button();
		checkBox1 = new System.Windows.Forms.CheckBox();
		btn_setresult = new System.Windows.Forms.Button();
		button6 = new System.Windows.Forms.Button();
		button4 = new System.Windows.Forms.Button();
		btn_tiaojian = new System.Windows.Forms.Button();
		button3 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		button12 = new System.Windows.Forms.Button();
		button10 = new System.Windows.Forms.Button();
		button11 = new System.Windows.Forms.Button();
		button7 = new System.Windows.Forms.Button();
		button1 = new System.Windows.Forms.Button();
		button8 = new System.Windows.Forms.Button();
		button9 = new System.Windows.Forms.Button();
		label3 = new System.Windows.Forms.Label();
		textBox2 = new System.Windows.Forms.TextBox();
		button5 = new System.Windows.Forms.Button();
		panel1.SuspendLayout();
		contextMenuStrip1.SuspendLayout();
		groupBox1.SuspendLayout();
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
		label2.Text = "查询";
		treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		treeView1.Location = new System.Drawing.Point(15, 40);
		treeView1.Name = "treeView1";
		treeView1.Size = new System.Drawing.Size(190, 504);
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
		btn_alladd.Location = new System.Drawing.Point(213, 127);
		btn_alladd.Name = "btn_alladd";
		btn_alladd.Size = new System.Drawing.Size(75, 23);
		btn_alladd.TabIndex = 3;
		btn_alladd.Text = "全选表";
		btn_alladd.UseVisualStyleBackColor = true;
		btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
		btn_allremove.Location = new System.Drawing.Point(213, 156);
		btn_allremove.Name = "btn_allremove";
		btn_allremove.Size = new System.Drawing.Size(75, 23);
		btn_allremove.TabIndex = 4;
		btn_allremove.Text = "全移除";
		btn_allremove.UseVisualStyleBackColor = true;
		btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
		panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		panel1.Controls.Add(listBox2);
		panel1.Controls.Add(groupBox1);
		panel1.Controls.Add(listBox1);
		panel1.Controls.Add(textBox1);
		panel1.Controls.Add(comboBox1);
		panel1.Controls.Add(label7);
		panel1.Controls.Add(label6);
		panel1.Controls.Add(dataGridView1);
		panel1.Controls.Add(label5);
		panel1.Controls.Add(btn_sort);
		panel1.Controls.Add(checkBox1);
		panel1.Controls.Add(btn_setresult);
		panel1.Controls.Add(button6);
		panel1.Controls.Add(button4);
		panel1.Controls.Add(btn_tiaojian);
		panel1.Controls.Add(button3);
		panel1.Controls.Add(button2);
		panel1.Controls.Add(button12);
		panel1.Controls.Add(button10);
		panel1.Controls.Add(button11);
		panel1.Controls.Add(button7);
		panel1.Controls.Add(button1);
		panel1.Location = new System.Drawing.Point(294, 40);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(645, 475);
		panel1.TabIndex = 3;
		listBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listBox2.ContextMenuStrip = contextMenuStrip1;
		listBox2.FormattingEnabled = true;
		listBox2.ItemHeight = 12;
		listBox2.Location = new System.Drawing.Point(5, 325);
		listBox2.Name = "listBox2";
		listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
		listBox2.Size = new System.Drawing.Size(518, 52);
		listBox2.TabIndex = 12;
		contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { 上移ToolStripMenuItem, 下移ToolStripMenuItem, toolStripSeparator1, 删除ToolStripMenuItem });
		contextMenuStrip1.Name = "contextMenuStrip1";
		contextMenuStrip1.ShowImageMargin = false;
		contextMenuStrip1.Size = new System.Drawing.Size(108, 76);
		上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
		上移ToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
		上移ToolStripMenuItem.Text = "上移(&U)";
		上移ToolStripMenuItem.Click += new System.EventHandler(上移ToolStripMenuItem_Click);
		下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
		下移ToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
		下移ToolStripMenuItem.Text = "下移(&D)";
		下移ToolStripMenuItem.Click += new System.EventHandler(下移ToolStripMenuItem_Click);
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
		删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
		删除ToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
		删除ToolStripMenuItem.Text = "删除";
		删除ToolStripMenuItem.Click += new System.EventHandler(删除ToolStripMenuItem_Click);
		groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		groupBox1.Controls.Add(label4);
		groupBox1.Location = new System.Drawing.Point(553, 320);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new System.Drawing.Size(84, 59);
		groupBox1.TabIndex = 10;
		groupBox1.TabStop = false;
		groupBox1.Text = "列表变量数";
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		label4.Location = new System.Drawing.Point(10, 23);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(20, 19);
		label4.TabIndex = 0;
		label4.Text = "0";
		label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listBox1.FormattingEnabled = true;
		listBox1.ItemHeight = 12;
		listBox1.Location = new System.Drawing.Point(5, 28);
		listBox1.Name = "listBox1";
		listBox1.Size = new System.Drawing.Size(554, 52);
		listBox1.TabIndex = 0;
		listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox1.Location = new System.Drawing.Point(62, 383);
		textBox1.Multiline = true;
		textBox1.Name = "textBox1";
		textBox1.ReadOnly = true;
		textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		textBox1.Size = new System.Drawing.Size(575, 87);
		textBox1.TabIndex = 16;
		textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		comboBox1.FormattingEnabled = true;
		comboBox1.Location = new System.Drawing.Point(3, 294);
		comboBox1.Name = "comboBox1";
		comboBox1.Size = new System.Drawing.Size(520, 20);
		comboBox1.TabIndex = 9;
		comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		comboBox1.DropDownClosed += new System.EventHandler(comboBox1_DropDownClosed);
		comboBox1.DropDown += new System.EventHandler(comboBox1_DropDown);
		label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label7.AutoSize = true;
		label7.Location = new System.Drawing.Point(3, 386);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(53, 12);
		label7.TabIndex = 4;
		label7.Text = "命令预览";
		label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(3, 276);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(95, 12);
		label6.TabIndex = 4;
		label6.Text = "应用查询结果到:";
		label6.Click += new System.EventHandler(label6_Click);
		dataGridView1.AllowUserToAddRows = false;
		dataGridView1.AllowUserToDeleteRows = false;
		dataGridView1.AllowUserToResizeRows = false;
		dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column5, Column4, Column6);
		dataGridView1.Location = new System.Drawing.Point(5, 86);
		dataGridView1.MultiSelect = false;
		dataGridView1.Name = "dataGridView1";
		dataGridView1.RowHeadersVisible = false;
		dataGridView1.RowTemplate.Height = 23;
		dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		dataGridView1.Size = new System.Drawing.Size(577, 187);
		dataGridView1.TabIndex = 3;
		dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridView1_DataError);
		dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		Column1.HeaderText = "";
		Column1.Name = "Column1";
		Column1.Width = 25;
		Column2.HeaderText = "所属表";
		Column2.Name = "Column2";
		Column2.ReadOnly = true;
		Column2.Width = 130;
		Column3.HeaderText = "字段";
		Column3.Name = "Column3";
		Column3.ReadOnly = true;
		Column3.Width = 145;
		Column5.HeaderText = "显示名称";
		Column5.Name = "Column5";
		Column4.HeaderText = "连接";
		Column4.Name = "Column4";
		Column4.Width = 50;
		Column6.HeaderText = "连接字段";
		Column6.Name = "Column6";
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(3, 13);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(41, 12);
		label5.TabIndex = 0;
		label5.Text = "已选表";
		label5.Click += new System.EventHandler(label1_Click);
		btn_sort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		btn_sort.Location = new System.Drawing.Point(565, 57);
		btn_sort.Name = "btn_sort";
		btn_sort.Size = new System.Drawing.Size(75, 23);
		btn_sort.TabIndex = 2;
		btn_sort.Text = "排序设计";
		btn_sort.UseVisualStyleBackColor = true;
		btn_sort.Click += new System.EventHandler(btn_sort_Click);
		checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		checkBox1.AutoSize = true;
		checkBox1.Location = new System.Drawing.Point(566, 9);
		checkBox1.Name = "checkBox1";
		checkBox1.Size = new System.Drawing.Size(72, 16);
		checkBox1.TabIndex = 4;
		checkBox1.Text = "异步操作";
		checkBox1.UseVisualStyleBackColor = true;
		checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		btn_setresult.Location = new System.Drawing.Point(5, 406);
		btn_setresult.Name = "btn_setresult";
		btn_setresult.Size = new System.Drawing.Size(51, 23);
		btn_setresult.TabIndex = 15;
		btn_setresult.Text = "生成";
		btn_setresult.UseVisualStyleBackColor = true;
		btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
		button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button6.Location = new System.Drawing.Point(588, 291);
		button6.Name = "button6";
		button6.Size = new System.Drawing.Size(53, 23);
		button6.TabIndex = 11;
		button6.Text = "删除";
		button6.UseVisualStyleBackColor = true;
		button6.Click += new System.EventHandler(button6_Click);
		button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button4.Location = new System.Drawing.Point(529, 291);
		button4.Name = "button4";
		button4.Size = new System.Drawing.Size(53, 23);
		button4.TabIndex = 10;
		button4.Text = "添加";
		button4.UseVisualStyleBackColor = true;
		button4.Click += new System.EventHandler(button4_Click);
		btn_tiaojian.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		btn_tiaojian.Location = new System.Drawing.Point(565, 28);
		btn_tiaojian.Name = "btn_tiaojian";
		btn_tiaojian.Size = new System.Drawing.Size(75, 23);
		btn_tiaojian.TabIndex = 1;
		btn_tiaojian.Text = "条件设计";
		btn_tiaojian.UseVisualStyleBackColor = true;
		btn_tiaojian.Click += new System.EventHandler(btntiaojian_Click);
		button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button3.Location = new System.Drawing.Point(588, 202);
		button3.Name = "button3";
		button3.Size = new System.Drawing.Size(52, 23);
		button3.TabIndex = 8;
		button3.Text = "反选";
		button3.UseVisualStyleBackColor = true;
		button3.Click += new System.EventHandler(button3_Click);
		button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button2.Location = new System.Drawing.Point(588, 173);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(52, 23);
		button2.TabIndex = 7;
		button2.Text = "全不选";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		button12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button12.Location = new System.Drawing.Point(529, 354);
		button12.Name = "button12";
		button12.Size = new System.Drawing.Size(22, 23);
		button12.TabIndex = 14;
		button12.Text = "↓";
		button12.UseVisualStyleBackColor = true;
		button12.Click += new System.EventHandler(下移ToolStripMenuItem_Click);
		button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button10.Location = new System.Drawing.Point(588, 115);
		button10.Name = "button10";
		button10.Size = new System.Drawing.Size(52, 23);
		button10.TabIndex = 5;
		button10.Text = "↓";
		button10.UseVisualStyleBackColor = true;
		button10.Click += new System.EventHandler(button10_Click);
		button11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button11.Location = new System.Drawing.Point(529, 325);
		button11.Name = "button11";
		button11.Size = new System.Drawing.Size(22, 23);
		button11.TabIndex = 13;
		button11.Text = "↑";
		button11.UseVisualStyleBackColor = true;
		button11.Click += new System.EventHandler(上移ToolStripMenuItem_Click);
		button7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button7.Location = new System.Drawing.Point(588, 86);
		button7.Name = "button7";
		button7.Size = new System.Drawing.Size(52, 23);
		button7.TabIndex = 4;
		button7.Text = "↑";
		button7.UseVisualStyleBackColor = true;
		button7.Click += new System.EventHandler(button7_Click);
		button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button1.Location = new System.Drawing.Point(588, 144);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(52, 23);
		button1.TabIndex = 6;
		button1.Text = "全选";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button8.Location = new System.Drawing.Point(783, 521);
		button8.Name = "button8";
		button8.Size = new System.Drawing.Size(75, 23);
		button8.TabIndex = 7;
		button8.Text = "确定";
		button8.UseVisualStyleBackColor = true;
		button8.Click += new System.EventHandler(button8_Click);
		button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button9.Location = new System.Drawing.Point(864, 521);
		button9.Name = "button9";
		button9.Size = new System.Drawing.Size(75, 23);
		button9.TabIndex = 8;
		button9.Text = "取消";
		button9.UseVisualStyleBackColor = true;
		button9.Click += new System.EventHandler(button9_Click);
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(292, 13);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(71, 12);
		label3.TabIndex = 4;
		label3.Text = "条件表达式:";
		textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox2.Location = new System.Drawing.Point(369, 10);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(535, 21);
		textBox2.TabIndex = 5;
		textBox2.Text = "true";
		button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button5.Location = new System.Drawing.Point(909, 8);
		button5.Name = "button5";
		button5.Size = new System.Drawing.Size(30, 23);
		button5.TabIndex = 6;
		button5.Text = "...";
		button5.UseVisualStyleBackColor = true;
		button5.Click += new System.EventHandler(button5_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(951, 556);
		base.Controls.Add(button5);
		base.Controls.Add(textBox2);
		base.Controls.Add(label3);
		base.Controls.Add(panel1);
		base.Controls.Add(btn_allremove);
		base.Controls.Add(btn_alladd);
		base.Controls.Add(btn_removetable);
		base.Controls.Add(btn_addtable);
		base.Controls.Add(treeView1);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Controls.Add(button9);
		base.Controls.Add(button8);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "SLSetDBSelect";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "查询数据";
		base.Load += new System.EventHandler(DBSelectForm_Load);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		contextMenuStrip1.ResumeLayout(false);
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
