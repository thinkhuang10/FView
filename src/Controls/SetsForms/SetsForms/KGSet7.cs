using System;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet7 : Form
{
    public string varname;

    public bool opstflag;

    private Button btn_Cancel;

    private Button btn_OK;

    private TextBox txt_var;

    private Button btn_view;

    private CheckBox ckb_opst;

    private GroupBox groupBox1;

    private Label label1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet7()
    {
        InitializeComponent();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (!string.IsNullOrEmpty(txt_var.Text))
                {
                    varname = txt_var.Text;
                    if (ckb_opst.Checked)
                    {
                        opstflag = true;
                    }
                    else
                    {
                        opstflag = false;
                    }
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请输入变量值！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常");
        }
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void KGSet7_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            ckb_opst.Checked = opstflag;
            txt_var.Text = varname.Substring(1, varname.Length - 2);
        }
    }

    private void ckb_opst_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.ckb_opst = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(172, 150);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 4;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(82, 150);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 3;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.txt_var.Location = new System.Drawing.Point(21, 14);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(136, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(163, 13);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(60, 21);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.ckb_opst.AutoSize = true;
        this.ckb_opst.Location = new System.Drawing.Point(104, 35);
        this.ckb_opst.Name = "ckb_opst";
        this.ckb_opst.Size = new System.Drawing.Size(48, 16);
        this.ckb_opst.TabIndex = 2;
        this.ckb_opst.Text = "取反";
        this.ckb_opst.UseVisualStyleBackColor = true;
        this.ckb_opst.CheckedChanged += new System.EventHandler(ckb_opst_CheckedChanged);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.ckb_opst);
        this.groupBox1.Location = new System.Drawing.Point(21, 50);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(202, 73);
        this.groupBox1.TabIndex = 51;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(28, 36);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 51;
        this.label1.Text = "是否取反:";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(266, 185);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet7";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet7_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
