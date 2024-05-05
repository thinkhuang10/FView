using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace HMIEditEnvironment.Animation;

public class RulesForm : Form
{
    public List<string> vars = new();

    public DataTable RecordDatatable = new();

    public string Wherestr = "";

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

    private Button btn_OK;

    private Button btn_cancel;


    public RulesForm()
    {
        InitializeComponent();
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
        cbx_comparavartype.Items.Add("{权限管理字段}");
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            cbx_comparavartype.Items.Add("{[" + projectIO.name + "]}");
        }
        XmlNodeList xmlNodeList = CEditEnvironmentGlobal.xmldoc.SelectNodes("/DocumentRoot/Item");
        foreach (XmlNode item in xmlNodeList)
        {
            cbx_comparavartype.Items.Add("{[" + item.Attributes["Name"].Value + "]}");
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            foreach (CShape item2 in df.ListAllShowCShape)
            {
                if (item2 is CControl && (((CControl)item2)._c is CDataGridView || ((CControl)item2)._c is CButton || ((CControl)item2)._c is CDateTimePicker || ((CControl)item2)._c is CTextBox || ((CControl)item2)._c is CLabel || ((CControl)item2)._c is CComboBox || ((CControl)item2)._c is CListBox))
                {
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + "}");
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + ".Text}");
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
                else if (item2 is CControl && ((CControl)item2)._c is CCheckBox)
                {
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + "}");
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + ".Value}");
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
                else if (item2 is CString)
                {
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + "}");
                    cbx_comparavartype.Items.Add("{" + df.name + "." + item2.Name + ".Tag}");
                }
            }
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
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        cbx_var = new System.Windows.Forms.ComboBox();
        cbx_yunsuan = new System.Windows.Forms.ComboBox();
        cbx_comparavartype = new System.Windows.Forms.ComboBox();
        cbx_guanxi = new System.Windows.Forms.ComboBox();
        逻辑关系 = new System.Windows.Forms.Label();
        btn_add = new System.Windows.Forms.Button();
        btn_del = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(209, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "选择字段";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(10, 35);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 0;
        label2.Text = "匹配运算符";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(233, 35);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(29, 12);
        label4.TabIndex = 0;
        label4.Text = "参数";
        cbx_var.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        cbx_var.FormattingEnabled = true;
        cbx_var.Location = new System.Drawing.Point(270, 6);
        cbx_var.Name = "cbx_var";
        cbx_var.Size = new System.Drawing.Size(160, 20);
        cbx_var.TabIndex = 1;
        cbx_yunsuan.FormattingEnabled = true;
        cbx_yunsuan.Items.AddRange(new object[7] { "=", "<>", ">", "<", ">=", "<=", "like" });
        cbx_yunsuan.Location = new System.Drawing.Point(83, 32);
        cbx_yunsuan.Name = "cbx_yunsuan";
        cbx_yunsuan.Size = new System.Drawing.Size(120, 20);
        cbx_yunsuan.TabIndex = 2;
        cbx_comparavartype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        cbx_comparavartype.FormattingEnabled = true;
        cbx_comparavartype.Location = new System.Drawing.Point(270, 32);
        cbx_comparavartype.Name = "cbx_comparavartype";
        cbx_comparavartype.Size = new System.Drawing.Size(160, 20);
        cbx_comparavartype.TabIndex = 3;
        cbx_comparavartype.SelectedIndexChanged += new System.EventHandler(cbx_comparavartype_SelectedIndexChanged);
        cbx_guanxi.FormattingEnabled = true;
        cbx_guanxi.Items.AddRange(new object[2] { "并且", "或者" });
        cbx_guanxi.Location = new System.Drawing.Point(83, 6);
        cbx_guanxi.Name = "cbx_guanxi";
        cbx_guanxi.Size = new System.Drawing.Size(120, 20);
        cbx_guanxi.TabIndex = 0;
        逻辑关系.AutoSize = true;
        逻辑关系.Location = new System.Drawing.Point(22, 9);
        逻辑关系.Name = "逻辑关系";
        逻辑关系.Size = new System.Drawing.Size(53, 12);
        逻辑关系.TabIndex = 2;
        逻辑关系.Text = "逻辑关系";
        btn_add.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btn_add.Location = new System.Drawing.Point(448, 4);
        btn_add.Name = "btn_add";
        btn_add.Size = new System.Drawing.Size(75, 23);
        btn_add.TabIndex = 4;
        btn_add.Text = "添加";
        btn_add.UseVisualStyleBackColor = true;
        btn_add.Click += new System.EventHandler(btn_add_Click);
        btn_del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btn_del.Location = new System.Drawing.Point(448, 30);
        btn_del.Name = "btn_del";
        btn_del.Size = new System.Drawing.Size(75, 23);
        btn_del.TabIndex = 5;
        btn_del.Text = "删除";
        btn_del.UseVisualStyleBackColor = true;
        btn_del.Click += new System.EventHandler(btn_del_Click);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new System.Drawing.Point(12, 59);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(530, 238);
        dataGridView1.TabIndex = 6;
        btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        btn_OK.Location = new System.Drawing.Point(125, 303);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 7;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btn_cancel.Location = new System.Drawing.Point(355, 303);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 8;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(554, 338);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(dataGridView1);
        base.Controls.Add(btn_del);
        base.Controls.Add(btn_add);
        base.Controls.Add(cbx_guanxi);
        base.Controls.Add(逻辑关系);
        base.Controls.Add(cbx_comparavartype);
        base.Controls.Add(cbx_yunsuan);
        base.Controls.Add(cbx_var);
        base.Controls.Add(label4);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        base.Name = "RulesForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "条件配置";
        base.Load += new System.EventHandler(RulesForm_Load);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
