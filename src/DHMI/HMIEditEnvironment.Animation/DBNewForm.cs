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

namespace HMIEditEnvironment.Animation;

public class DBNewForm : Form
{
	public byte[] OtherData;

	public bool Ansync;

	public string resultSQL = string.Empty;

	private string sqlString = string.Empty;

	private string tableName = string.Empty;

	private DataTable DgDatatable = new();

	private List<string> _datatypes;

	private List<string> _datatypesreserved;

	private IContainer components;

	private Label label2;

	public Label label4;

	private Label label3;

	private Label label1;

	private TreeView treeView1;

	private DataGridView dataGridView1;

	private Panel panel1;

	private Button button1;

	private Button button2;

	private Label label5;

	private TextBox textBox1;

	private Button button_Generate;

	private CheckBox checkBox1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem 上移toolStripMenuItem;

	private ToolStripMenuItem 下移toolStripMenuItem2;

	private ToolStripMenuItem 删除列toolStripMenuItem3;

	private ToolStripMenuItem 刷新toolStripMenuItem4;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem 清除默认值toolStripMenuItem5;

	private ToolStripMenuItem 默认值0toolStripMenuItem6;

	private ToolStripMenuItem 默认值NULLtoolStripMenuItem7;

	private TextBox textBox2;

	private Label label6;

	private Button button3;

	private DataGridViewComboEditBoxColumn dataGridViewComboEditBoxColumn1;

	private DataGridViewTextBoxColumn ColumnName;

	private DataGridViewButtonColumn VarBinding;

	private DataGridViewComboEditBoxColumn DataType;

	private DataGridViewCheckBoxColumn ParmaryKey;

	private DataGridViewCheckBoxColumn IsNULL;

	private DataGridViewCheckBoxColumn AutoIncrement;

	public DBNewForm()
	{
		InitializeComponent();
		DgDatatable.Columns.Add("columnName");
		DgDatatable.Columns.Add("dataType");
		DgDatatable.Columns.Add("checkbox1");
		DgDatatable.Columns.Add("checkbox2");
		DgDatatable.Columns.Add("checkbox3");
	}

	public void DBNewForm_Load(object sender, EventArgs e)
	{
		_datatypes = new List<string>();
		_datatypesreserved = new List<string>();
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
		if (DBOperationGlobal.dbType.Contains("SqlClient"))
		{
			_datatypes.Add("bigint");
			_datatypes.Add("binary");
			_datatypes.Add("bit");
			_datatypes.Add("char");
			_datatypes.Add("date");
			_datatypes.Add("datetime");
			_datatypes.Add("decimal");
			_datatypes.Add("float");
			_datatypes.Add("image");
			_datatypes.Add("int");
			_datatypes.Add("money");
			_datatypes.Add("nchar");
			_datatypes.Add("nvarchar");
			_datatypes.Add("numeric");
			_datatypes.Add("ntext");
			_datatypes.Add("real");
			_datatypes.Add("smalldatetime");
			_datatypes.Add("text");
			_datatypes.Add("varbinary");
			_datatypes.Add("varchar");
		}
		else if (DBOperationGlobal.dbType.Contains("MySQL"))
		{
			_datatypes.Add("bigint");
			_datatypes.Add("bit");
			_datatypes.Add("char");
			_datatypes.Add("date");
			_datatypes.Add("datetime");
			_datatypes.Add("decimal");
			_datatypes.Add("float");
			_datatypes.Add("int");
			_datatypes.Add("numeric");
			_datatypes.Add("real");
			_datatypes.Add("text");
			_datatypes.Add("varbinary");
			_datatypes.Add("varchar");
		}
		foreach (string datatype in _datatypes)
		{
			switch (datatype)
			{
			case "char":
			case "nchar":
				_datatypesreserved.Add(datatype + "(10)");
				break;
			case "nvarchar":
			case "varchar":
			case "binary":
			case "varbinary":
				_datatypesreserved.Add(datatype + "(50)");
				break;
			case "decimal":
			case "numeric":
				_datatypesreserved.Add(datatype + "(18,0)");
				break;
			default:
				_datatypesreserved.Add(datatype);
				break;
			}
		}
		foreach (string item2 in _datatypesreserved)
		{
			DataType.Items.AddRange(item2);
		}
		try
		{
			DeSerialize(OtherData);
			checkBox1.Checked = Ansync;
			textBox2.Text = tableName;
			if (tableName.StartsWith("[") && tableName.EndsWith("]"))
			{
				textBox2.ReadOnly = true;
			}
			textBox1.Text = resultSQL;
			foreach (DataRow row in DgDatatable.Rows)
			{
				dataGridView1.Rows.Add(row["columnName"].ToString(), "", row["dataType"].ToString(), row["checkbox1"].ToString().Equals("") ? row["checkbox1"] : ((object)Convert.ToBoolean(row["checkbox1"].ToString())), row["checkbox2"].ToString().Equals("") ? row["checkbox2"] : ((object)Convert.ToBoolean(row["checkbox2"].ToString())), row["checkbox3"].ToString().Equals("") ? row["checkbox3"] : ((object)Convert.ToBoolean(row["checkbox3"].ToString())));
			}
		}
		catch
		{
		}
		dataGridView1.AllowUserToAddRows = true;
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		Ansync = checkBox1.Checked;
	}

	private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && dataGridView1.SelectedRows.Count != 0)
		{
			contextMenuStrip1.Show((Control)sender, e.Location);
		}
	}

	private void button_Generate_Click(object sender, EventArgs e)
	{
		dataGridView1.EndEdit();
		if (textBox2.Text != "")
		{
			if (dataGridView1.Rows.Count <= 0)
			{
				return;
			}
			if (dataGridView1.Rows.Count == 1 && (dataGridView1.Rows[0].Cells[0].Value == null || dataGridView1.Rows[0].Cells[0].Value.ToString() == ""))
			{
				MessageBox.Show("生成失败，表不存在列", "提示");
				return;
			}
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				if (!item.IsNewRow)
				{
					if (item.Cells[0].Value == null || item.Cells[0].Value.ToString() == "")
					{
						MessageBox.Show("生成失败，列名不能为空或包含无效字符", "提示");
						return;
					}
					if (item.Cells[2].Value == null || item.Cells[2].Value.ToString() == "")
					{
						MessageBox.Show("生成失败，数据类型不能为空", "提示");
						return;
					}
				}
			}
			sqlString = textBox2.Text.Trim();
			if (sqlString.StartsWith("[") && sqlString.EndsWith("]"))
			{
				sqlString = "{" + sqlString + "}";
			}
			if (DBOperationGlobal.dbType.Contains("MySQL"))
			{
				sqlString = "`" + sqlString + "` " + GetSQL().Replace("IDENTITY(1,1)", "AUTO_INCREMENT");
			}
			else
			{
				sqlString = "[" + sqlString + "] " + GetSQL();
			}
			sqlString = "CREATE TABLE " + sqlString;
			textBox1.Text = sqlString;
		}
		else
		{
			MessageBox.Show("生成失败，表名不能为空", "提示");
		}
	}

	private string GetSQL()
	{
		string text = string.Empty;
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			if (!item.IsNewRow)
			{
				string text2 = item.Cells[0].Value.ToString();
				if (text2.StartsWith("[") && text2.EndsWith("]"))
				{
					text2 = "{" + text2 + "}";
				}
				text2 = ((!DBOperationGlobal.dbType.Contains("MySQL")) ? ("[" + text2 + "]") : ("`" + text2 + "`"));
				text2 = text2 + " " + item.Cells[2].Value.ToString();
				if ("True".Equals(Convert.ToString(item.Cells[3].Value)))
				{
					text2 += " PRIMARY KEY";
				}
				if ("".Equals(Convert.ToString(item.Cells[4].Value)) || "False".Equals(Convert.ToString(item.Cells[4].Value)))
				{
					text2 += " NOT NULL";
				}
				if ("True".Equals(Convert.ToString(item.Cells[5].Value)))
				{
					text2 += " IDENTITY(1,1)";
				}
				text = text + text2 + ",";
			}
		}
		return "(" + text.Trim(',') + ")";
	}

	public byte[] Serialize()
	{
		IFormatter formatter = new BinaryFormatter();
		MemoryStream memoryStream = new();
        DBNewSerializeCopy dBNewSerializeCopy = new()
        {
            ansync = Ansync,
            sqlString = sqlString,
            tableName = textBox2.Text,
            DgDatatable = DgDatatable
        };
        formatter.Serialize(memoryStream, dBNewSerializeCopy);
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
		DBNewSerializeCopy dBNewSerializeCopy = (DBNewSerializeCopy)formatter.Deserialize(stream);
		stream.Close();
		Ansync = dBNewSerializeCopy.ansync;
		tableName = dBNewSerializeCopy.tableName;
		sqlString = dBNewSerializeCopy.sqlString;
		if (dBNewSerializeCopy.DgDatatable.Columns.Count == DgDatatable.Columns.Count)
		{
			DgDatatable = dBNewSerializeCopy.DgDatatable;
			return;
		}
		foreach (DataRow row in dBNewSerializeCopy.DgDatatable.Rows)
		{
			DataRow dataRow2 = DgDatatable.NewRow();
			dataRow2["columnName"] = row["columnName"];
			dataRow2["dataType"] = row["dataType"];
			dataRow2["checkbox1"] = row["checkbox1"];
			dataRow2["checkbox2"] = row["checkbox2"];
			dataRow2["checkbox3"] = row["checkbox3"];
		}
	}

	private void 上移toolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void 下移toolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void 删除列toolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count == 0)
		{
			return;
		}
		foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
		{
			if (!selectedRow.IsNewRow)
			{
				dataGridView1.Rows.Remove(selectedRow);
			}
		}
	}

	private void 刷新toolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "" && MessageBox.Show("监测到未生成SQL语句,是否自动生成？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			button_Generate_Click(sender, e);
		}
		DgDatatable.Rows.Clear();
		tableName = textBox2.Text;
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			if (!item.IsNewRow)
			{
				DataRow dataRow = DgDatatable.NewRow();
				dataRow["columnName"] = item.Cells[0].Value.ToString();
				if (item.Cells[2].Value != null)
				{
					dataRow["dataType"] = item.Cells[2].Value.ToString();
				}
				else
				{
					dataRow["dataType"] = "";
				}
				if (item.Cells[3].Value == null || item.Cells[3].Value.ToString().Equals(""))
				{
					dataRow["checkbox1"] = item.Cells[3].Value;
				}
				else
				{
					dataRow["checkbox1"] = ((bool)item.Cells[3].Value).ToString();
				}
				if (item.Cells[4].Value == null || item.Cells[4].Value.ToString().Equals(""))
				{
					dataRow["checkbox2"] = item.Cells[4].Value;
				}
				else
				{
					dataRow["checkbox2"] = ((bool)item.Cells[4].Value).ToString();
				}
				if (item.Cells[5].Value == null || item.Cells[5].Value.ToString().Equals(""))
				{
					dataRow["checkbox3"] = item.Cells[5].Value;
				}
				else
				{
					dataRow["checkbox3"] = ((bool)item.Cells[5].Value).ToString();
				}
				DgDatatable.Rows.Add(dataRow);
			}
		}
		OtherData = Serialize();
		resultSQL = textBox1.Text;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void textBox1_DoubleClick(object sender, EventArgs e)
	{
		textBox1.ReadOnly = false;
	}

	private void textBox2_DoubleClick(object sender, EventArgs e)
	{
		textBox2.ReadOnly = false;
	}

	private void textBox1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.A && textBox1.Text != "")
		{
			textBox1.SelectAll();
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		string varTableEvent = CForDCCEControl.GetVarTableEvent("");
		if (varTableEvent != "")
		{
			textBox2.Text = "[" + varTableEvent + "]";
			textBox2.ReadOnly = true;
		}
	}

	private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (dataGridView1.NewRowIndex >= 0)
		{
			dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[1].Value = "..";
		}
	}

	private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex >= 0 && e.ColumnIndex == dataGridView1.Columns[1].Index)
		{
			string varTableEvent = CForDCCEControl.GetVarTableEvent("");
			if (varTableEvent != "")
			{
				dataGridView1.Rows.Add();
				dataGridView1.Rows[e.RowIndex].Cells[0].Value = "[" + varTableEvent + "]";
			}
		}
		int count = dataGridView1.Rows.Count;
		if (e.RowIndex < 0)
		{
			return;
		}
		if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].EditedFormattedValue))
		{
			if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
			{
				for (int i = 0; i < count - 1; i++)
				{
					if (i != e.RowIndex)
					{
						dataGridView1.Rows[i].Cells[3].Value = false;
					}
				}
				dataGridView1.Rows[e.RowIndex].Cells[4].Value = false;
				dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = true;
				dataGridView1.Rows[e.RowIndex].Cells[3].Value = true;
			}
		}
		else
		{
			dataGridView1.Rows[e.RowIndex].Cells[3].Value = false;
			if (!Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].EditedFormattedValue))
			{
				dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = false;
			}
		}
		if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[5].EditedFormattedValue))
		{
			for (int j = 0; j < count - 1; j++)
			{
				if (j != e.RowIndex)
				{
					dataGridView1.Rows[j].Cells[5].Value = false;
				}
			}
			dataGridView1.Rows[e.RowIndex].Cells[4].Value = false;
			dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = true;
			dataGridView1.Rows[e.RowIndex].Cells[5].Value = true;
		}
		else
		{
			dataGridView1.Rows[e.RowIndex].Cells[5].Value = false;
			if (!Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].EditedFormattedValue))
			{
				dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = false;
			}
		}
	}

	private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex >= 0 && e.ColumnIndex == dataGridView1.Columns[0].Index)
		{
			dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = false;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new();
		label2 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		treeView1 = new System.Windows.Forms.TreeView();
		dataGridView1 = new System.Windows.Forms.DataGridView();
		ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
		VarBinding = new System.Windows.Forms.DataGridViewButtonColumn();
		DataType = new HMIEditEnvironment.Animation.DataGridViewComboEditBoxColumn();
		ParmaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		IsNULL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		AutoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
		上移toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		下移toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		删除列toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		刷新toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		清除默认值toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
		默认值0toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
		默认值NULLtoolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
		panel1 = new System.Windows.Forms.Panel();
		button3 = new System.Windows.Forms.Button();
		textBox2 = new System.Windows.Forms.TextBox();
		label6 = new System.Windows.Forms.Label();
		checkBox1 = new System.Windows.Forms.CheckBox();
		button_Generate = new System.Windows.Forms.Button();
		textBox1 = new System.Windows.Forms.TextBox();
		label5 = new System.Windows.Forms.Label();
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		dataGridViewComboEditBoxColumn1 = new HMIEditEnvironment.Animation.DataGridViewComboEditBoxColumn();
		((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
		contextMenuStrip1.SuspendLayout();
		panel1.SuspendLayout();
		base.SuspendLayout();
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(78, 13);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(41, 12);
		label2.TabIndex = 1;
		label2.Text = "新建表";
		label4.AutoSize = true;
		label4.Location = new System.Drawing.Point(211, 13);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(71, 12);
		label4.TabIndex = 2;
		label4.Text = "ControlName";
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(146, 13);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(59, 12);
		label3.TabIndex = 3;
		label3.Text = "当前控件:";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(13, 13);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(59, 12);
		label1.TabIndex = 4;
		label1.Text = "当前操作:";
		treeView1.Location = new System.Drawing.Point(15, 40);
		treeView1.Name = "treeView1";
		treeView1.Size = new System.Drawing.Size(190, 470);
		treeView1.TabIndex = 5;
		dataGridView1.AllowUserToAddRows = false;
		dataGridView1.AllowUserToDeleteRows = false;
		dataGridView1.AllowUserToResizeRows = false;
		dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridView1.Columns.AddRange(ColumnName, VarBinding, DataType, ParmaryKey, IsNULL, AutoIncrement);
		dataGridView1.ContextMenuStrip = contextMenuStrip1;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
		dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		dataGridView1.Location = new System.Drawing.Point(12, 40);
		dataGridView1.MultiSelect = false;
		dataGridView1.Name = "dataGridView1";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		dataGridView1.RowTemplate.Height = 23;
		dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		dataGridView1.Size = new System.Drawing.Size(610, 241);
		dataGridView1.TabIndex = 6;
		dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
		dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseClick);
		ColumnName.FillWeight = 37.17461f;
		ColumnName.HeaderText = "列名";
		ColumnName.Name = "ColumnName";
		ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		ColumnName.Width = 150;
		VarBinding.HeaderText = "..";
		VarBinding.Name = "VarBinding";
		VarBinding.Text = "..";
		VarBinding.UseColumnTextForButtonValue = true;
		VarBinding.Width = 30;
		DataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
		DataType.DropDownWidth = 10;
		DataType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		DataType.HeaderText = "数据类型";
		DataType.MaxDropDownItems = 4;
		DataType.Name = "DataType";
		DataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		DataType.Sorted = true;
		DataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		ParmaryKey.FillWeight = 101.6741f;
		ParmaryKey.HeaderText = "主键";
		ParmaryKey.Name = "ParmaryKey";
		ParmaryKey.Width = 40;
		IsNULL.FillWeight = 286.8021f;
		IsNULL.HeaderText = "允许NULL值";
		IsNULL.Name = "IsNULL";
		IsNULL.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		IsNULL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		IsNULL.Width = 90;
		AutoIncrement.FillWeight = 37.17461f;
		AutoIncrement.HeaderText = "自增列";
		AutoIncrement.Name = "AutoIncrement";
		AutoIncrement.Width = 50;
		contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10] { 上移toolStripMenuItem, 下移toolStripMenuItem2, toolStripSeparator1, 删除列toolStripMenuItem3, toolStripSeparator2, 刷新toolStripMenuItem4, toolStripSeparator3, 清除默认值toolStripMenuItem5, 默认值0toolStripMenuItem6, 默认值NULLtoolStripMenuItem7 });
		contextMenuStrip1.Name = "contextMenuStrip1";
		contextMenuStrip1.Size = new System.Drawing.Size(144, 176);
		上移toolStripMenuItem.Name = "上移toolStripMenuItem";
		上移toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		上移toolStripMenuItem.Text = "上移";
		上移toolStripMenuItem.Visible = false;
		上移toolStripMenuItem.Click += new System.EventHandler(上移toolStripMenuItem_Click);
		下移toolStripMenuItem2.Name = "下移toolStripMenuItem2";
		下移toolStripMenuItem2.Size = new System.Drawing.Size(143, 22);
		下移toolStripMenuItem2.Text = "下移";
		下移toolStripMenuItem2.Visible = false;
		下移toolStripMenuItem2.Click += new System.EventHandler(下移toolStripMenuItem_Click);
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
		toolStripSeparator1.Visible = false;
		删除列toolStripMenuItem3.Name = "删除列toolStripMenuItem3";
		删除列toolStripMenuItem3.Size = new System.Drawing.Size(143, 22);
		删除列toolStripMenuItem3.Text = "删除列";
		删除列toolStripMenuItem3.Click += new System.EventHandler(删除列toolStripMenuItem_Click);
		toolStripSeparator2.Name = "toolStripSeparator2";
		toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
		toolStripSeparator2.Visible = false;
		刷新toolStripMenuItem4.Name = "刷新toolStripMenuItem4";
		刷新toolStripMenuItem4.Size = new System.Drawing.Size(143, 22);
		刷新toolStripMenuItem4.Text = "刷新";
		刷新toolStripMenuItem4.Visible = false;
		刷新toolStripMenuItem4.Click += new System.EventHandler(刷新toolStripMenuItem_Click);
		toolStripSeparator3.Name = "toolStripSeparator3";
		toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
		toolStripSeparator3.Visible = false;
		清除默认值toolStripMenuItem5.Name = "清除默认值toolStripMenuItem5";
		清除默认值toolStripMenuItem5.Size = new System.Drawing.Size(143, 22);
		清除默认值toolStripMenuItem5.Text = "清除默认值";
		清除默认值toolStripMenuItem5.Visible = false;
		默认值0toolStripMenuItem6.Name = "默认值0toolStripMenuItem6";
		默认值0toolStripMenuItem6.Size = new System.Drawing.Size(143, 22);
		默认值0toolStripMenuItem6.Text = "默认值0";
		默认值0toolStripMenuItem6.Visible = false;
		默认值NULLtoolStripMenuItem7.Name = "默认值NULLtoolStripMenuItem7";
		默认值NULLtoolStripMenuItem7.Size = new System.Drawing.Size(143, 22);
		默认值NULLtoolStripMenuItem7.Text = "默认值NULL";
		默认值NULLtoolStripMenuItem7.Visible = false;
		panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		panel1.Controls.Add(button3);
		panel1.Controls.Add(textBox2);
		panel1.Controls.Add(label6);
		panel1.Controls.Add(checkBox1);
		panel1.Controls.Add(button_Generate);
		panel1.Controls.Add(textBox1);
		panel1.Controls.Add(label5);
		panel1.Controls.Add(dataGridView1);
		panel1.Location = new System.Drawing.Point(228, 40);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(638, 432);
		panel1.TabIndex = 8;
		button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button3.Location = new System.Drawing.Point(164, 11);
		button3.Name = "button3";
		button3.Size = new System.Drawing.Size(30, 23);
		button3.TabIndex = 13;
		button3.Text = "...";
		button3.UseVisualStyleBackColor = true;
		button3.Click += new System.EventHandler(button3_Click);
		textBox2.Location = new System.Drawing.Point(58, 13);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(100, 21);
		textBox2.TabIndex = 12;
		textBox2.DoubleClick += new System.EventHandler(textBox2_DoubleClick);
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(10, 17);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(41, 12);
		label6.TabIndex = 11;
		label6.Text = "表名：";
		checkBox1.AutoSize = true;
		checkBox1.Location = new System.Drawing.Point(548, 15);
		checkBox1.Name = "checkBox1";
		checkBox1.Size = new System.Drawing.Size(72, 16);
		checkBox1.TabIndex = 10;
		checkBox1.Text = "异步操作";
		checkBox1.UseVisualStyleBackColor = true;
		checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		button_Generate.Location = new System.Drawing.Point(10, 324);
		button_Generate.Name = "button_Generate";
		button_Generate.Size = new System.Drawing.Size(50, 23);
		button_Generate.TabIndex = 9;
		button_Generate.Text = "生成";
		button_Generate.UseVisualStyleBackColor = true;
		button_Generate.Click += new System.EventHandler(button_Generate_Click);
		textBox1.Location = new System.Drawing.Point(67, 296);
		textBox1.Multiline = true;
		textBox1.Name = "textBox1";
		textBox1.ReadOnly = true;
		textBox1.Size = new System.Drawing.Size(553, 109);
		textBox1.TabIndex = 8;
		textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(8, 299);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(53, 12);
		label5.TabIndex = 7;
		label5.Text = "命令预览";
		button1.Location = new System.Drawing.Point(681, 487);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 9;
		button1.Text = "确定";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button2.Location = new System.Drawing.Point(774, 487);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 10;
		button2.Text = "取消";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		dataGridViewComboEditBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
		dataGridViewComboEditBoxColumn1.DropDownWidth = 10;
		dataGridViewComboEditBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		dataGridViewComboEditBoxColumn1.HeaderText = "数据类型";
		dataGridViewComboEditBoxColumn1.MaxDropDownItems = 4;
		dataGridViewComboEditBoxColumn1.Name = "dataGridViewComboEditBoxColumn1";
		dataGridViewComboEditBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		dataGridViewComboEditBoxColumn1.Sorted = true;
		dataGridViewComboEditBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(878, 522);
		base.Controls.Add(button2);
		base.Controls.Add(button1);
		base.Controls.Add(treeView1);
		base.Controls.Add(label2);
		base.Controls.Add(label4);
		base.Controls.Add(label3);
		base.Controls.Add(label1);
		base.Controls.Add(panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.Name = "DBNewForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "新建表";
		base.Load += new System.EventHandler(DBNewForm_Load);
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		contextMenuStrip1.ResumeLayout(false);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
