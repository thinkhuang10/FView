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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        ckb_opst = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label1 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(172, 150);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 4;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(82, 150);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 3;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        txt_var.Location = new System.Drawing.Point(21, 14);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(136, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(163, 13);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(60, 21);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        ckb_opst.AutoSize = true;
        ckb_opst.Location = new System.Drawing.Point(104, 35);
        ckb_opst.Name = "ckb_opst";
        ckb_opst.Size = new System.Drawing.Size(48, 16);
        ckb_opst.TabIndex = 2;
        ckb_opst.Text = "取反";
        ckb_opst.UseVisualStyleBackColor = true;
        ckb_opst.CheckedChanged += new System.EventHandler(ckb_opst_CheckedChanged);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(ckb_opst);
        groupBox1.Location = new System.Drawing.Point(21, 50);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(202, 73);
        groupBox1.TabIndex = 51;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(28, 36);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 51;
        label1.Text = "是否取反:";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(266, 185);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet7";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet7_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
