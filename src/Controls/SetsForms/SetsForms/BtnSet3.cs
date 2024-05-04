using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BtnSet3 : Form
{
    public string varname;

    public string txt = "Check Box";

    public Color txtcolor = Color.Black;

    private TextBox txt_var;

    private Button btn_view;

    private TextBox txt_text;

    private Label label5;

    private GroupBox groupBox1;

    private Label lbl_txtcolor;

    private Label label1;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BtnSet3()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (txt_var.Text != "")
                {
                    varname = txt_var.Text;
                    txt = txt_text.Text;
                    txtcolor = lbl_txtcolor.BackColor;
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请将信息填写完整！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常！");
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

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void BtnSet3_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
        }
        txt_text.Text = txt;
        lbl_txtcolor.BackColor = txtcolor;
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_text = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(12, 19);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(203, 19);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_text.Location = new System.Drawing.Point(96, 24);
        this.txt_text.Name = "txt_text";
        this.txt_text.Size = new System.Drawing.Size(148, 21);
        this.txt_text.TabIndex = 2;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(25, 29);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 39;
        this.label5.Text = "显示文本：";
        this.groupBox1.Controls.Add(this.lbl_txtcolor);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_text);
        this.groupBox1.Location = new System.Drawing.Point(12, 48);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(266, 92);
        this.groupBox1.TabIndex = 41;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.Color.Black;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(96, 64);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(67, 14);
        this.lbl_txtcolor.TabIndex = 3;
        this.lbl_txtcolor.Text = "          ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(25, 64);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 41;
        this.label1.Text = "文字颜色：";
        this.btn_OK.Location = new System.Drawing.Point(121, 155);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 4;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(203, 154);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 5;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 191);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet3";
        this.Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet3_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
