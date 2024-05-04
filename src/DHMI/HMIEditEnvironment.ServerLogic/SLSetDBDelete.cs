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

public class SLSetDBDelete : Form
{
	public byte[] OtherData;

	public string ResultSQL = "";

	public string ResultTo = "";

	public bool Ansync;

	private List<string> ListboxContentstr = new List<string>();

	private DataTable DgDatatable = new DataTable();

	private List<string> CanUseVars;

	public ServerLogicItem result;

	private Label label1;

	private Label label2;

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

	private CheckBox checkBox1;

	private Button button1;

	private TextBox textBox2;

	private Label label6;

	private DataGridViewCheckBoxColumn Column1;

	private DataGridViewComboBoxColumn Column5;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewComboBoxColumn Column6;

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

	public SLSetDBDelete(List<string> canusevars)
	{
		InitializeComponent();

		DgDatatable.Columns.Add("tbname");
		DgDatatable.Columns.Add("ck1");
		DgDatatable.Columns.Add("andor");
		DgDatatable.Columns.Add("varname");
		DgDatatable.Columns.Add("tiaojian");
		DgDatatable.Columns.Add("value1");
		DgDatatable.Columns.Add("value2");
		CanUseVars = canusevars;
	}

	public SLSetDBDelete(List<string> canusevars, ServerLogicItem item)
	{
		InitializeComponent();

		DgDatatable.Columns.Add("tbname");
		DgDatatable.Columns.Add("ck1");
		DgDatatable.Columns.Add("andor");
		DgDatatable.Columns.Add("varname");
		DgDatatable.Columns.Add("tiaojian");
		DgDatatable.Columns.Add("value1");
		DgDatatable.Columns.Add("value2");
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
		MemoryStream memoryStream = new MemoryStream();
		DBSelectSerializeCopy dBSelectSerializeCopy = new DBSelectSerializeCopy();
		dBSelectSerializeCopy.ansync = Ansync;
		dBSelectSerializeCopy.ListboxContentstr = ListboxContentstr;
		dBSelectSerializeCopy.DgDatatable = DgDatatable;
		formatter.Serialize(memoryStream, dBSelectSerializeCopy);
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
			DBSelectSerializeCopy dBSelectSerializeCopy = (DBSelectSerializeCopy)formatter.Deserialize(stream);
			stream.Close();
			ListboxContentstr = dBSelectSerializeCopy.ListboxContentstr;
			DgDatatable = dBSelectSerializeCopy.DgDatatable;
			Ansync = dBSelectSerializeCopy.ansync;
		}
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
			List<string> list = new List<string>();
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
				foreach (string item2 in ListboxContentstr)
				{
					listBox1.Items.Add(item2);
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
		DataSet dataSet = new DataSet();
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
			DgDatatable.Rows.Add(dataRow2);
		}
	}

	private void btn_removetable_Click(object sender, EventArgs e)
	{
		List<object> list = new List<object>();
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
			try
			{
				dataGridView1.Rows.Clear();
				return;
			}
			catch
			{
				return;
			}
		}
		listBox1.SelectedIndex = 0;
	}

	private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listBox1.SelectedItem != null)
		{
			string text = listBox1.SelectedItem.ToString();
			DataRow[] array = DgDatatable.Select("tbname = '" + text + "'");
			dataGridView1.Rows.Clear();
			DataRow[] array2 = array;
			foreach (DataRow dataRow in array2)
			{
				dataGridView1.Rows.Add(Convert.ToBoolean(dataRow[1]), dataRow[2].ToString(), dataRow[3].ToString(), dataRow[4].ToString(), dataRow["value1"].ToString(), dataRow["value2"].ToString());
			}
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
				if (dataRow["value1"].ToString() != "{常量}")
				{
					if (dataRow["tiaojian"].ToString() != "like")
					{
						string text2 = text;
						text = text2 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '" + dataRow["value1"].ToString() + "'";
					}
					else
					{
						string text3 = text;
						text = text3 + " " + dataRow[2].ToString() + " " + dataRow[3].ToString() + " " + dataRow[4].ToString() + " '%" + dataRow["value1"].ToString() + "%'";
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
		result = new ServerLogicItem();
		result.LogicType = "删除数据";
		result.ConditionalExpression = textBox2.Text;
		result.DataDict = new Dictionary<string, object>
        {
            { "ResultSQL", ResultSQL },
            { "Ansync", Ansync },
            { "OtherData", OtherData },
            { "ResultTo", textBox3.Text }
        };
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
			DgDatatable.Rows.Add(dataRow);
		}
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
		SLChooseVar sLChooseVar = new SLChooseVar(CanUseVars, textBox2.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox2.Text = sLChooseVar.Result;
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new SLChooseVar(CanUseVars, textBox3.Text);
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

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
	}

	private void InitializeComponent()
	{
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.btn_addtable = new System.Windows.Forms.Button();
		this.btn_removetable = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button2 = new System.Windows.Forms.Button();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.btn_setresult = new System.Windows.Forms.Button();
		this.button8 = new System.Windows.Forms.Button();
		this.button9 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
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
		this.label2.Text = "删除";
		this.treeView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.treeView1.Location = new System.Drawing.Point(15, 40);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(190, 392);
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
		this.panel1.Controls.Add(this.button2);
		this.panel1.Controls.Add(this.textBox3);
		this.panel1.Controls.Add(this.checkBox1);
		this.panel1.Controls.Add(this.dataGridView1);
		this.panel1.Controls.Add(this.listBox1);
		this.panel1.Controls.Add(this.textBox1);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.btn_setresult);
		this.panel1.Location = new System.Drawing.Point(294, 40);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(527, 364);
		this.panel1.TabIndex = 3;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Location = new System.Drawing.Point(491, 248);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(30, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "...";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox3.Location = new System.Drawing.Point(62, 251);
		this.textBox3.Margin = new System.Windows.Forms.Padding(2);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(425, 21);
		this.textBox3.TabIndex = 1;
		this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(450, 12);
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
		this.dataGridView1.Columns.AddRange(this.Column1, this.Column5, this.Column2, this.Column6, this.Column3, this.Column4);
		this.dataGridView1.Location = new System.Drawing.Point(5, 86);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(517, 160);
		this.dataGridView1.TabIndex = 8;
		this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
		this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
		this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellEnter);
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
		this.Column3.Width = 125;
		this.Column4.HeaderText = "值";
		this.Column4.Name = "Column4";
		this.Column4.Width = 65;
		this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 12;
		this.listBox1.Location = new System.Drawing.Point(5, 28);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(517, 52);
		this.listBox1.TabIndex = 0;
		this.listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
		this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox1.Location = new System.Drawing.Point(62, 281);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.textBox1.Size = new System.Drawing.Size(460, 78);
		this.textBox1.TabIndex = 3;
		this.textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 254);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 4;
		this.label3.Text = "影响行数";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(3, 281);
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
		this.label5.Click += new System.EventHandler(label1_Click);
		this.btn_setresult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btn_setresult.Location = new System.Drawing.Point(5, 301);
		this.btn_setresult.Name = "btn_setresult";
		this.btn_setresult.Size = new System.Drawing.Size(51, 23);
		this.btn_setresult.TabIndex = 2;
		this.btn_setresult.Text = "生成";
		this.btn_setresult.UseVisualStyleBackColor = true;
		this.btn_setresult.Click += new System.EventHandler(btn_setresult_Click);
		this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button8.Location = new System.Drawing.Point(665, 410);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(75, 23);
		this.button8.TabIndex = 5;
		this.button8.Text = "确定";
		this.button8.UseVisualStyleBackColor = true;
		this.button8.Click += new System.EventHandler(button8_Click);
		this.button9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button9.Location = new System.Drawing.Point(746, 410);
		this.button9.Name = "button9";
		this.button9.Size = new System.Drawing.Size(75, 23);
		this.button9.TabIndex = 6;
		this.button9.Text = "取消";
		this.button9.UseVisualStyleBackColor = true;
		this.button9.Click += new System.EventHandler(button9_Click);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Location = new System.Drawing.Point(791, 7);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(30, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "...";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox2.Location = new System.Drawing.Point(365, 10);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(421, 21);
		this.textBox2.TabIndex = 3;
		this.textBox2.Text = "true";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(292, 13);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(71, 12);
		this.label6.TabIndex = 10;
		this.label6.Text = "条件表达式:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(833, 444);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btn_removetable);
		base.Controls.Add(this.btn_addtable);
		base.Controls.Add(this.treeView1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.button9);
		base.Controls.Add(this.button8);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "SLSetDBDelete";
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
