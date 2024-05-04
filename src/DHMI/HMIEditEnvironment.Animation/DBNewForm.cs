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

	private DataTable DgDatatable = new DataTable();

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
		MemoryStream memoryStream = new MemoryStream();
		DBNewSerializeCopy dBNewSerializeCopy = new DBNewSerializeCopy();
		dBNewSerializeCopy.ansync = Ansync;
		dBNewSerializeCopy.sqlString = sqlString;
		dBNewSerializeCopy.tableName = textBox2.Text;
		dBNewSerializeCopy.DgDatatable = DgDatatable;
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.VarBinding = new System.Windows.Forms.DataGridViewButtonColumn();
		this.DataType = new HMIEditEnvironment.Animation.DataGridViewComboEditBoxColumn();
		this.ParmaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.IsNULL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.AutoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.上移toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.下移toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.删除列toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.刷新toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.清除默认值toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
		this.默认值0toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
		this.默认值NULLtoolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button3 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.button_Generate = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.dataGridViewComboEditBoxColumn1 = new HMIEditEnvironment.Animation.DataGridViewComboEditBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(78, 13);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "新建表";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(211, 13);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(71, 12);
		this.label4.TabIndex = 2;
		this.label4.Text = "ControlName";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(146, 13);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 12);
		this.label3.TabIndex = 3;
		this.label3.Text = "当前控件:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 13);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "当前操作:";
		this.treeView1.Location = new System.Drawing.Point(15, 40);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(190, 470);
		this.treeView1.TabIndex = 5;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.ColumnName, this.VarBinding, this.DataType, this.ParmaryKey, this.IsNULL, this.AutoIncrement);
		this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dataGridView1.Location = new System.Drawing.Point(12, 40);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(610, 241);
		this.dataGridView1.TabIndex = 6;
		this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
		this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseClick);
		this.ColumnName.FillWeight = 37.17461f;
		this.ColumnName.HeaderText = "列名";
		this.ColumnName.Name = "ColumnName";
		this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ColumnName.Width = 150;
		this.VarBinding.HeaderText = "..";
		this.VarBinding.Name = "VarBinding";
		this.VarBinding.Text = "..";
		this.VarBinding.UseColumnTextForButtonValue = true;
		this.VarBinding.Width = 30;
		this.DataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
		this.DataType.DropDownWidth = 10;
		this.DataType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.DataType.HeaderText = "数据类型";
		this.DataType.MaxDropDownItems = 4;
		this.DataType.Name = "DataType";
		this.DataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.DataType.Sorted = true;
		this.DataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.ParmaryKey.FillWeight = 101.6741f;
		this.ParmaryKey.HeaderText = "主键";
		this.ParmaryKey.Name = "ParmaryKey";
		this.ParmaryKey.Width = 40;
		this.IsNULL.FillWeight = 286.8021f;
		this.IsNULL.HeaderText = "允许NULL值";
		this.IsNULL.Name = "IsNULL";
		this.IsNULL.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.IsNULL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.IsNULL.Width = 90;
		this.AutoIncrement.FillWeight = 37.17461f;
		this.AutoIncrement.HeaderText = "自增列";
		this.AutoIncrement.Name = "AutoIncrement";
		this.AutoIncrement.Width = 50;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.上移toolStripMenuItem, this.下移toolStripMenuItem2, this.toolStripSeparator1, this.删除列toolStripMenuItem3, this.toolStripSeparator2, this.刷新toolStripMenuItem4, this.toolStripSeparator3, this.清除默认值toolStripMenuItem5, this.默认值0toolStripMenuItem6, this.默认值NULLtoolStripMenuItem7 });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(144, 176);
		this.上移toolStripMenuItem.Name = "上移toolStripMenuItem";
		this.上移toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
		this.上移toolStripMenuItem.Text = "上移";
		this.上移toolStripMenuItem.Visible = false;
		this.上移toolStripMenuItem.Click += new System.EventHandler(上移toolStripMenuItem_Click);
		this.下移toolStripMenuItem2.Name = "下移toolStripMenuItem2";
		this.下移toolStripMenuItem2.Size = new System.Drawing.Size(143, 22);
		this.下移toolStripMenuItem2.Text = "下移";
		this.下移toolStripMenuItem2.Visible = false;
		this.下移toolStripMenuItem2.Click += new System.EventHandler(下移toolStripMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
		this.toolStripSeparator1.Visible = false;
		this.删除列toolStripMenuItem3.Name = "删除列toolStripMenuItem3";
		this.删除列toolStripMenuItem3.Size = new System.Drawing.Size(143, 22);
		this.删除列toolStripMenuItem3.Text = "删除列";
		this.删除列toolStripMenuItem3.Click += new System.EventHandler(删除列toolStripMenuItem_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
		this.toolStripSeparator2.Visible = false;
		this.刷新toolStripMenuItem4.Name = "刷新toolStripMenuItem4";
		this.刷新toolStripMenuItem4.Size = new System.Drawing.Size(143, 22);
		this.刷新toolStripMenuItem4.Text = "刷新";
		this.刷新toolStripMenuItem4.Visible = false;
		this.刷新toolStripMenuItem4.Click += new System.EventHandler(刷新toolStripMenuItem_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
		this.toolStripSeparator3.Visible = false;
		this.清除默认值toolStripMenuItem5.Name = "清除默认值toolStripMenuItem5";
		this.清除默认值toolStripMenuItem5.Size = new System.Drawing.Size(143, 22);
		this.清除默认值toolStripMenuItem5.Text = "清除默认值";
		this.清除默认值toolStripMenuItem5.Visible = false;
		this.默认值0toolStripMenuItem6.Name = "默认值0toolStripMenuItem6";
		this.默认值0toolStripMenuItem6.Size = new System.Drawing.Size(143, 22);
		this.默认值0toolStripMenuItem6.Text = "默认值0";
		this.默认值0toolStripMenuItem6.Visible = false;
		this.默认值NULLtoolStripMenuItem7.Name = "默认值NULLtoolStripMenuItem7";
		this.默认值NULLtoolStripMenuItem7.Size = new System.Drawing.Size(143, 22);
		this.默认值NULLtoolStripMenuItem7.Text = "默认值NULL";
		this.默认值NULLtoolStripMenuItem7.Visible = false;
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.button3);
		this.panel1.Controls.Add(this.textBox2);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.checkBox1);
		this.panel1.Controls.Add(this.button_Generate);
		this.panel1.Controls.Add(this.textBox1);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.dataGridView1);
		this.panel1.Location = new System.Drawing.Point(228, 40);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(638, 432);
		this.panel1.TabIndex = 8;
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Location = new System.Drawing.Point(164, 11);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(30, 23);
		this.button3.TabIndex = 13;
		this.button3.Text = "...";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.textBox2.Location = new System.Drawing.Point(58, 13);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(100, 21);
		this.textBox2.TabIndex = 12;
		this.textBox2.DoubleClick += new System.EventHandler(textBox2_DoubleClick);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(10, 17);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 12);
		this.label6.TabIndex = 11;
		this.label6.Text = "表名：";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(548, 15);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(72, 16);
		this.checkBox1.TabIndex = 10;
		this.checkBox1.Text = "异步操作";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.button_Generate.Location = new System.Drawing.Point(10, 324);
		this.button_Generate.Name = "button_Generate";
		this.button_Generate.Size = new System.Drawing.Size(50, 23);
		this.button_Generate.TabIndex = 9;
		this.button_Generate.Text = "生成";
		this.button_Generate.UseVisualStyleBackColor = true;
		this.button_Generate.Click += new System.EventHandler(button_Generate_Click);
		this.textBox1.Location = new System.Drawing.Point(67, 296);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(553, 109);
		this.textBox1.TabIndex = 8;
		this.textBox1.DoubleClick += new System.EventHandler(textBox1_DoubleClick);
		this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(8, 299);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 12);
		this.label5.TabIndex = 7;
		this.label5.Text = "命令预览";
		this.button1.Location = new System.Drawing.Point(681, 487);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 9;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(774, 487);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 10;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.dataGridViewComboEditBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
		this.dataGridViewComboEditBoxColumn1.DropDownWidth = 10;
		this.dataGridViewComboEditBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.dataGridViewComboEditBoxColumn1.HeaderText = "数据类型";
		this.dataGridViewComboEditBoxColumn1.MaxDropDownItems = 4;
		this.dataGridViewComboEditBoxColumn1.Name = "dataGridViewComboEditBoxColumn1";
		this.dataGridViewComboEditBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridViewComboEditBoxColumn1.Sorted = true;
		this.dataGridViewComboEditBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(878, 522);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.treeView1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.Name = "DBNewForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "新建表";
		base.Load += new System.EventHandler(DBNewForm_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
