using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HMIEditEnvironment.Animation;

public class SortForm : Form
{
    public List<string> vars = new();

    public DataTable RecordDatatable = new();

    private Label label1;

    private ComboBox cbx_var;

    private ComboBox cbx_rules;

    private Label label2;

    private Button btn_add;

    private Button btn_del;

    private DataGridView dataGridView1;

    private Button btn_OK;

    private Button btn_cancel;

    public SortForm()
    {
        InitializeComponent();
    }

    private void btn_add_Click(object sender, EventArgs e)
    {
        DataRow dataRow = RecordDatatable.NewRow();
        dataRow["字段"] = cbx_var.SelectedItem.ToString();
        dataRow["排序规则"] = cbx_rules.SelectedItem.ToString();
        RecordDatatable.Rows.Add(dataRow);
    }

    private void SortForm_Load(object sender, EventArgs e)
    {
        foreach (string var in vars)
        {
            cbx_var.Items.Add(var);
        }
        if (cbx_var.Items.Count != 0)
        {
            cbx_var.SelectedIndex = 0;
        }
        if (cbx_rules.Items.Count != 0)
        {
            cbx_rules.SelectedIndex = 0;
        }
        dataGridView1.DataSource = RecordDatatable;
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

    private void btn_OK_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.OK;
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.cbx_var = new System.Windows.Forms.ComboBox();
        this.cbx_rules = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.btn_add = new System.Windows.Forms.Button();
        this.btn_del = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(25, 19);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "选择字段";
        this.cbx_var.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.cbx_var.FormattingEnabled = true;
        this.cbx_var.Location = new System.Drawing.Point(27, 39);
        this.cbx_var.Name = "cbx_var";
        this.cbx_var.Size = new System.Drawing.Size(165, 20);
        this.cbx_var.TabIndex = 0;
        this.cbx_rules.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.cbx_rules.FormattingEnabled = true;
        this.cbx_rules.Items.AddRange(new object[2] { "升序", "降序" });
        this.cbx_rules.Location = new System.Drawing.Point(206, 39);
        this.cbx_rules.Name = "cbx_rules";
        this.cbx_rules.Size = new System.Drawing.Size(109, 20);
        this.cbx_rules.TabIndex = 1;
        this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(204, 19);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "排序规则";
        this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btn_add.Location = new System.Drawing.Point(333, 37);
        this.btn_add.Name = "btn_add";
        this.btn_add.Size = new System.Drawing.Size(75, 23);
        this.btn_add.TabIndex = 2;
        this.btn_add.Text = "添加";
        this.btn_add.UseVisualStyleBackColor = true;
        this.btn_add.Click += new System.EventHandler(btn_add_Click);
        this.btn_del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.btn_del.Location = new System.Drawing.Point(427, 37);
        this.btn_del.Name = "btn_del";
        this.btn_del.Size = new System.Drawing.Size(75, 23);
        this.btn_del.TabIndex = 3;
        this.btn_del.Text = "删除";
        this.btn_del.UseVisualStyleBackColor = true;
        this.btn_del.Click += new System.EventHandler(btn_del_Click);
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(27, 67);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(475, 150);
        this.dataGridView1.TabIndex = 4;
        this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.btn_OK.Location = new System.Drawing.Point(427, 223);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 5;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.btn_cancel.Location = new System.Drawing.Point(333, 223);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 6;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Visible = false;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(534, 270);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.dataGridView1);
        base.Controls.Add(this.btn_del);
        base.Controls.Add(this.btn_add);
        base.Controls.Add(this.cbx_rules);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.cbx_var);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "SortForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "排序配置";
        base.Load += new System.EventHandler(SortForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
