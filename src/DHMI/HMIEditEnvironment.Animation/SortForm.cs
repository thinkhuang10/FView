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
        label1 = new System.Windows.Forms.Label();
        cbx_var = new System.Windows.Forms.ComboBox();
        cbx_rules = new System.Windows.Forms.ComboBox();
        label2 = new System.Windows.Forms.Label();
        btn_add = new System.Windows.Forms.Button();
        btn_del = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(25, 19);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "选择字段";
        cbx_var.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        cbx_var.FormattingEnabled = true;
        cbx_var.Location = new System.Drawing.Point(27, 39);
        cbx_var.Name = "cbx_var";
        cbx_var.Size = new System.Drawing.Size(165, 20);
        cbx_var.TabIndex = 0;
        cbx_rules.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        cbx_rules.FormattingEnabled = true;
        cbx_rules.Items.AddRange(new object[2] { "升序", "降序" });
        cbx_rules.Location = new System.Drawing.Point(206, 39);
        cbx_rules.Name = "cbx_rules";
        cbx_rules.Size = new System.Drawing.Size(109, 20);
        cbx_rules.TabIndex = 1;
        label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(204, 19);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 2;
        label2.Text = "排序规则";
        btn_add.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btn_add.Location = new System.Drawing.Point(333, 37);
        btn_add.Name = "btn_add";
        btn_add.Size = new System.Drawing.Size(75, 23);
        btn_add.TabIndex = 2;
        btn_add.Text = "添加";
        btn_add.UseVisualStyleBackColor = true;
        btn_add.Click += new System.EventHandler(btn_add_Click);
        btn_del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btn_del.Location = new System.Drawing.Point(427, 37);
        btn_del.Name = "btn_del";
        btn_del.Size = new System.Drawing.Size(75, 23);
        btn_del.TabIndex = 3;
        btn_del.Text = "删除";
        btn_del.UseVisualStyleBackColor = true;
        btn_del.Click += new System.EventHandler(btn_del_Click);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new System.Drawing.Point(27, 67);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(475, 150);
        dataGridView1.TabIndex = 4;
        dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btn_OK.Location = new System.Drawing.Point(427, 223);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 5;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btn_cancel.Location = new System.Drawing.Point(333, 223);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 6;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Visible = false;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(534, 270);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(dataGridView1);
        base.Controls.Add(btn_del);
        base.Controls.Add(btn_add);
        base.Controls.Add(cbx_rules);
        base.Controls.Add(label2);
        base.Controls.Add(cbx_var);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "SortForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "排序配置";
        base.Load += new System.EventHandler(SortForm_Load);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
