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

public class SLSetDBInsert : Form
{
	public byte[] OtherData;

	public bool Ansync;

	public string ResultSQL = "";

	private string ResultString = "";

	private string HeadString = "";

	private string JoinString = "";

	private string NowTable = "";

	private Dictionary<string, string> ResultStrings = new();

	private List<string> ListboxContentstr = new();

	private DataSet DgDatatables = new();

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

	private ListBox listBox1;

	private Button btn_setresult;

	private Button btn_allremove;

	private Button btn_alladd;

	private CheckBox checkBox1;

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

	public SLSetDBInsert(List<string> canusevars)
	{
		InitializeComponent();
		CanUseVars = canusevars;
	}

	public SLSetDBInsert(List<string> canusevars, ServerLogicItem item)
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
        DBInsertSerializeCopy dBInsertSerializeCopy = new()
        {
            ansync = Ansync,
            ResultString = ResultString,
            HeadString = HeadString,
            JoinString = JoinString,
            ListboxContentstr = ListboxContentstr,
            DgDatatables = DgDatatables,
            ResultStrings = ResultStrings,
            NowTable = NowTable
        };
        formatter.Serialize(memoryStream, dBInsertSerializeCopy);
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
			DBInsertSerializeCopy dBInsertSerializeCopy = (DBInsertSerializeCopy)formatter.Deserialize(stream);
			stream.Close();
			Ansync = dBInsertSerializeCopy.ansync;
			ResultString = dBInsertSerializeCopy.ResultString;
			HeadString = dBInsertSerializeCopy.HeadString;
			JoinString = dBInsertSerializeCopy.JoinString;
			ResultStrings = dBInsertSerializeCopy.ResultStrings;
			NowTable = dBInsertSerializeCopy.NowTable;
			ListboxContentstr = dBInsertSerializeCopy.ListboxContentstr;
			DgDatatables = dBInsertSerializeCopy.DgDatatables;
		}
	}

	private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void DBInsertForm_Load(object sender, EventArgs e)
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
			try
			{
				listBox1.SelectedItem = NowTable;
			}
			catch
			{
			}
			if (DgDatatables.Tables.Contains(NowTable))
			{
				dataGridView1.Rows.Clear();
				foreach (DataRow row in DgDatatables.Tables[NowTable].Rows)
				{
					if (!Column3.Items.Contains(row["vartype"].ToString()))
					{
						Column3.Items.Add(row["vartype"].ToString());
					}
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
		string text = listBox1.SelectedItem.ToString();
		List<object> list = new();
		foreach (object selectedItem in listBox1.SelectedItems)
		{
			list.Add(selectedItem);
		}
		foreach (object item in list)
		{
			listBox1.Items.Remove(item);
		}
		if (listBox1.Items.Count == 0)
		{
			NowTable = "";
		}
		else
		{
			listBox1.SelectedIndex = 0;
			NowTable = listBox1.SelectedItem.ToString();
		}
		if (DgDatatables.Tables.Contains(text))
		{
			DgDatatables.Tables.Remove(text);
		}
		if (ResultStrings.ContainsKey(text))
		{
			ResultStrings.Remove(text);
		}
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

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (dataGridView1.Rows.Count != 0 && NowTable != "")
		{
			string text = "";
			text = "INSERT INTO " + NowTable + " (";
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				if ((bool)item.Cells[0].Value)
				{
					text = text + item.Cells[1].Value.ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			text += ") VALUES (";
			foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
			{
				if ((bool)item2.Cells[0].Value && (bool)item2.Cells[0].Value)
				{
					text = ((!(item2.Cells["Column3"].Value.ToString() != "{常量}")) ? (text + "'" + ((item2.Cells["Column4"].Value == null) ? "" : item2.Cells["Column4"].Value.ToString()) + "',") : (text + "'" + item2.Cells["Column3"].Value.ToString() + "',"));
				}
			}
			text = text.Substring(0, text.Length - 1);
			text += ")";
			if (!ResultStrings.ContainsKey(NowTable))
			{
				ResultStrings.Add(NowTable, text);
			}
			else
			{
				ResultStrings[NowTable] = text;
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
				if (!Column3.Items.Contains(row["vartype"].ToString()))
				{
					Column3.Items.Add(row["vartype"].ToString());
				}
				dataGridView1.Rows.Add(Convert.ToBoolean(row["ck1"]), row["varname"].ToString(), row["vartype"].ToString(), row["value"].ToString());
			}
		}
		else
		{
			if (DBOperationGlobal.conn.State != ConnectionState.Open)
			{
				DBOperationGlobal.conn.Open();
			}
			string text2 = listBox1.SelectedItem.ToString();
			DBOperationGlobal.command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME  = '" + text2 + "' AND ( TABLE_SCHEMA = '" + DBOperationGlobal.conn.Database + "' OR TABLE_CATALOG = '" + DBOperationGlobal.conn.Database + "' )";
			DataSet dataSet = new();
			DBOperationGlobal.adapter.Fill(dataSet);
			foreach (DataRow row2 in dataSet.Tables[0].Rows)
			{
				dataGridView1.Rows.Add(true, row2["COLUMN_NAME"], "{常量}", "");
			}
		}
		NowTable = listBox1.SelectedItem.ToString();
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
		if (dataGridView1.Rows.Count == 0)
		{
			return;
		}
		string text = "";
		text = "INSERT INTO " + NowTable + " (";
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			if ((bool)item.Cells[0].Value)
			{
				text = text + item.Cells[1].Value.ToString() + ",";
			}
		}
		text = text.Substring(0, text.Length - 1);
		text += ") VALUES (";
		foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
		{
			if ((bool)item2.Cells[0].Value && (bool)item2.Cells[0].Value)
			{
				text = ((!(item2.Cells["Column3"].Value.ToString() != "{常量}")) ? (text + "'" + ((item2.Cells["Column4"].Value == null) ? "" : item2.Cells["Column4"].Value.ToString()) + "',") : (text + "'" + item2.Cells["Column3"].Value.ToString() + "',"));
			}
		}
		text = text.Substring(0, text.Length - 1);
		text += ")";
		if (!ResultStrings.ContainsKey(NowTable))
		{
			ResultStrings.Add(NowTable, text);
		}
		else
		{
			ResultStrings[NowTable] = text;
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
		textBox1.Text = "";
		foreach (string value in ResultStrings.Values)
		{
			TextBox textBox = textBox1;
			textBox.Text = textBox.Text + value + Environment.NewLine;
		}
	}

	private void button8_Click(object sender, EventArgs e)
	{
		if (dataGridView1.Rows.Count != 0)
		{
			string text = "";
			text = "INSERT INTO " + NowTable + " (";
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				if ((bool)item.Cells[0].Value)
				{
					text = text + item.Cells[1].Value.ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			text += ") VALUES (";
			foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
			{
				if ((bool)item2.Cells[0].Value && (bool)item2.Cells[0].Value)
				{
					text = ((!(item2.Cells["Column3"].Value.ToString() != "{常量}")) ? (text + "'" + ((item2.Cells["Column4"].Value == null) ? "" : item2.Cells["Column4"].Value.ToString()) + "',") : (text + "'" + item2.Cells["Column3"].Value.ToString() + "',"));
				}
			}
			text = text.Substring(0, text.Length - 1);
			text += ")";
			if (!ResultStrings.ContainsKey(NowTable))
			{
				ResultStrings.Add(NowTable, text);
			}
			else
			{
				ResultStrings[NowTable] = text;
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
            LogicType = "添加数据",
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

	private void treeView1_DoubleClick(object sender, EventArgs e)
	{
		btnadd_Click(sender, e);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(CanUseVars, textBox2.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox2.Text = sLChooseVar.Result;
		}
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
	}

	private void label6_Click(object sender, EventArgs e)
	{
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
		label2.Text = "插入";
		treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		treeView1.Location = new System.Drawing.Point(15, 40);
		treeView1.Name = "treeView1";
		treeView1.Size = new System.Drawing.Size(190, 370);
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
		panel1.Controls.Add(textBox1);
		panel1.Controls.Add(label7);
		panel1.Controls.Add(dataGridView1);
		panel1.Controls.Add(label5);
		panel1.Controls.Add(btn_setresult);
		panel1.Location = new System.Drawing.Point(294, 40);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(476, 342);
		panel1.TabIndex = 3;
		button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button2.Location = new System.Drawing.Point(440, 231);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(31, 23);
		button2.TabIndex = 3;
		button2.Text = "...";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		textBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox3.Location = new System.Drawing.Point(62, 234);
		textBox3.Margin = new System.Windows.Forms.Padding(2);
		textBox3.Name = "textBox3";
		textBox3.Size = new System.Drawing.Size(374, 21);
		textBox3.TabIndex = 2;
		label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(3, 236);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(53, 12);
		label3.TabIndex = 14;
		label3.Text = "影响行数";
		checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		checkBox1.AutoSize = true;
		checkBox1.Location = new System.Drawing.Point(399, 9);
		checkBox1.Name = "checkBox1";
		checkBox1.Size = new System.Drawing.Size(72, 16);
		checkBox1.TabIndex = 7;
		checkBox1.Text = "异步操作";
		checkBox1.UseVisualStyleBackColor = true;
		checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listBox1.FormattingEnabled = true;
		listBox1.ItemHeight = 12;
		listBox1.Location = new System.Drawing.Point(5, 28);
		listBox1.Name = "listBox1";
		listBox1.Size = new System.Drawing.Size(466, 52);
		listBox1.TabIndex = 0;
		listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox1.Location = new System.Drawing.Point(62, 260);
		textBox1.Multiline = true;
		textBox1.Name = "textBox1";
		textBox1.ReadOnly = true;
		textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		textBox1.Size = new System.Drawing.Size(409, 77);
		textBox1.TabIndex = 5;
		textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		label7.AutoSize = true;
		label7.Location = new System.Drawing.Point(3, 259);
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
		dataGridView1.Size = new System.Drawing.Size(466, 143);
		dataGridView1.TabIndex = 1;
		dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
		dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
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
		Column4.Width = 55;
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(3, 13);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(41, 12);
		label5.TabIndex = 0;
		label5.Text = "已选表";
		label5.Click += new System.EventHandler(label1_Click);
		btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		btn_setresult.Location = new System.Drawing.Point(5, 279);
		btn_setresult.Name = "btn_setresult";
		btn_setresult.Size = new System.Drawing.Size(51, 23);
		btn_setresult.TabIndex = 4;
		btn_setresult.Text = "生成";
		btn_setresult.UseVisualStyleBackColor = true;
		btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
		button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button8.Location = new System.Drawing.Point(614, 388);
		button8.Name = "button8";
		button8.Size = new System.Drawing.Size(75, 23);
		button8.TabIndex = 7;
		button8.Text = "确定";
		button8.UseVisualStyleBackColor = true;
		button8.Click += new System.EventHandler(button8_Click);
		button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button9.Location = new System.Drawing.Point(695, 388);
		button9.Name = "button9";
		button9.Size = new System.Drawing.Size(75, 23);
		button9.TabIndex = 8;
		button9.Text = "取消";
		button9.UseVisualStyleBackColor = true;
		button9.Click += new System.EventHandler(button9_Click);
		btn_allremove.Location = new System.Drawing.Point(213, 156);
		btn_allremove.Name = "btn_allremove";
		btn_allremove.Size = new System.Drawing.Size(75, 23);
		btn_allremove.TabIndex = 4;
		btn_allremove.Text = "全移除";
		btn_allremove.UseVisualStyleBackColor = true;
		btn_allremove.Click += new System.EventHandler(btn_allremove_Click);
		btn_alladd.Location = new System.Drawing.Point(213, 127);
		btn_alladd.Name = "btn_alladd";
		btn_alladd.Size = new System.Drawing.Size(75, 23);
		btn_alladd.TabIndex = 3;
		btn_alladd.Text = "全选表";
		btn_alladd.UseVisualStyleBackColor = true;
		btn_alladd.Click += new System.EventHandler(btn_alladd_Click);
		button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button1.Location = new System.Drawing.Point(740, 7);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(30, 23);
		button1.TabIndex = 6;
		button1.Text = "...";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox2.Location = new System.Drawing.Point(365, 10);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(369, 21);
		textBox2.TabIndex = 5;
		textBox2.Text = "true";
		textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(292, 13);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(71, 12);
		label6.TabIndex = 7;
		label6.Text = "条件表达式:";
		label6.Click += new System.EventHandler(label6_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(782, 422);
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
		base.Name = "SLSetDBInsert";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "添加数据";
		base.Load += new System.EventHandler(DBInsertForm_Load);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
