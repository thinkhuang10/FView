using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment.ServerLogic;

public class SLDBRules : Form
{
	public List<string> vars = new List<string>();

	public DataTable RecordDatatable = new DataTable();

	public string Wherestr = "";

	private List<string> CanUseVars;

	private Label label1;

	private Label label2;

	private Label label4;

	private ComboBox cbx_var;

	private ComboBox cbx_yunsuan;

	private ComboBox cbx_comparavartype;

	private ComboBox cbx_guanxi;

	private Label 逻辑关系;

	private Button btn_add;

	private Button btn_del;

	private DataGridView dataGridView1;

	private Button btn_cancel;

	private Button btn_OK;

	public SLDBRules(List<string> canusevars)
	{
		InitializeComponent();
		CanUseVars = canusevars;
	}

	private void btn_del_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count != 0)
		{
			int count = dataGridView1.SelectedRows.Count;
			for (int i = 0; i < count; i++)
			{
				dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
			}
			dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void btn_add_Click(object sender, EventArgs e)
	{
		if (cbx_comparavartype.Text != "")
		{
			DataRow dataRow = RecordDatatable.NewRow();
			dataRow["逻辑关系"] = cbx_guanxi.Text.ToString();
			dataRow["字段"] = cbx_var.Text.ToString();
			dataRow["匹配运算符"] = cbx_yunsuan.Text.ToString();
			dataRow["值"] = cbx_comparavartype.Text;
			RecordDatatable.Rows.Add(dataRow);
		}
	}

	private void RulesForm_Load(object sender, EventArgs e)
	{
		foreach (string canUseVar in CanUseVars)
		{
			cbx_comparavartype.Items.Add("{" + canUseVar + "}");
		}
		foreach (string var in vars)
		{
			cbx_var.Items.Add(var);
		}
		if (cbx_var.Items.Count != 0)
		{
			cbx_var.SelectedIndex = 0;
		}
		dataGridView1.DataSource = RecordDatatable;
		cbx_guanxi.SelectedIndex = 0;
		cbx_yunsuan.SelectedIndex = 0;
	}

	private void btn_OK_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void btn_cancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cbx_comparavartype_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void InitializeComponent()
	{
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cbx_var = new System.Windows.Forms.ComboBox();
		this.cbx_yunsuan = new System.Windows.Forms.ComboBox();
		this.cbx_comparavartype = new System.Windows.Forms.ComboBox();
		this.cbx_guanxi = new System.Windows.Forms.ComboBox();
		this.逻辑关系 = new System.Windows.Forms.Label();
		this.btn_add = new System.Windows.Forms.Button();
		this.btn_del = new System.Windows.Forms.Button();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.btn_cancel = new System.Windows.Forms.Button();
		this.btn_OK = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(209, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "选择字段";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(10, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "匹配运算符";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(233, 35);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 12);
		this.label4.TabIndex = 0;
		this.label4.Text = "参数";
		this.cbx_var.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbx_var.FormattingEnabled = true;
		this.cbx_var.Location = new System.Drawing.Point(270, 6);
		this.cbx_var.Name = "cbx_var";
		this.cbx_var.Size = new System.Drawing.Size(160, 20);
		this.cbx_var.TabIndex = 1;
		this.cbx_yunsuan.FormattingEnabled = true;
		this.cbx_yunsuan.Items.AddRange(new object[7] { "=", "<>", ">", "<", ">=", "<=", "like" });
		this.cbx_yunsuan.Location = new System.Drawing.Point(83, 32);
		this.cbx_yunsuan.Name = "cbx_yunsuan";
		this.cbx_yunsuan.Size = new System.Drawing.Size(120, 20);
		this.cbx_yunsuan.TabIndex = 2;
		this.cbx_comparavartype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbx_comparavartype.FormattingEnabled = true;
		this.cbx_comparavartype.Location = new System.Drawing.Point(270, 32);
		this.cbx_comparavartype.Name = "cbx_comparavartype";
		this.cbx_comparavartype.Size = new System.Drawing.Size(160, 20);
		this.cbx_comparavartype.TabIndex = 3;
		this.cbx_comparavartype.SelectedIndexChanged += new System.EventHandler(cbx_comparavartype_SelectedIndexChanged);
		this.cbx_guanxi.FormattingEnabled = true;
		this.cbx_guanxi.Items.AddRange(new object[2] { "并且", "或者" });
		this.cbx_guanxi.Location = new System.Drawing.Point(83, 6);
		this.cbx_guanxi.Name = "cbx_guanxi";
		this.cbx_guanxi.Size = new System.Drawing.Size(120, 20);
		this.cbx_guanxi.TabIndex = 0;
		this.逻辑关系.AutoSize = true;
		this.逻辑关系.Location = new System.Drawing.Point(22, 9);
		this.逻辑关系.Name = "逻辑关系";
		this.逻辑关系.Size = new System.Drawing.Size(53, 12);
		this.逻辑关系.TabIndex = 2;
		this.逻辑关系.Text = "逻辑关系";
		this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_add.Location = new System.Drawing.Point(448, 4);
		this.btn_add.Name = "btn_add";
		this.btn_add.Size = new System.Drawing.Size(75, 23);
		this.btn_add.TabIndex = 4;
		this.btn_add.Text = "添加";
		this.btn_add.UseVisualStyleBackColor = true;
		this.btn_add.Click += new System.EventHandler(btn_add_Click);
		this.btn_del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_del.Location = new System.Drawing.Point(448, 30);
		this.btn_del.Name = "btn_del";
		this.btn_del.Size = new System.Drawing.Size(75, 23);
		this.btn_del.TabIndex = 5;
		this.btn_del.Text = "删除";
		this.btn_del.UseVisualStyleBackColor = true;
		this.btn_del.Click += new System.EventHandler(btn_del_Click);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(12, 59);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(530, 238);
		this.dataGridView1.TabIndex = 8;
		this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btn_cancel.Location = new System.Drawing.Point(355, 303);
		this.btn_cancel.Name = "btn_cancel";
		this.btn_cancel.Size = new System.Drawing.Size(75, 23);
		this.btn_cancel.TabIndex = 7;
		this.btn_cancel.Text = "取消";
		this.btn_cancel.UseVisualStyleBackColor = true;
		this.btn_cancel.Visible = false;
		this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
		this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btn_OK.Location = new System.Drawing.Point(467, 303);
		this.btn_OK.Name = "btn_OK";
		this.btn_OK.Size = new System.Drawing.Size(75, 23);
		this.btn_OK.TabIndex = 6;
		this.btn_OK.Text = "确定";
		this.btn_OK.UseVisualStyleBackColor = true;
		this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(554, 338);
		base.Controls.Add(this.btn_cancel);
		base.Controls.Add(this.btn_OK);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.btn_del);
		base.Controls.Add(this.btn_add);
		base.Controls.Add(this.cbx_guanxi);
		base.Controls.Add(this.逻辑关系);
		base.Controls.Add(this.cbx_comparavartype);
		base.Controls.Add(this.cbx_yunsuan);
		base.Controls.Add(this.cbx_var);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "SLDBRules";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "条件配置";
		base.Load += new System.EventHandler(RulesForm_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
