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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        txt_text = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_txtcolor = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(12, 19);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(203, 19);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_text.Location = new System.Drawing.Point(96, 24);
        txt_text.Name = "txt_text";
        txt_text.Size = new System.Drawing.Size(148, 21);
        txt_text.TabIndex = 2;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(25, 29);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 39;
        label5.Text = "显示文本：";
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_text);
        groupBox1.Location = new System.Drawing.Point(12, 48);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(266, 92);
        groupBox1.TabIndex = 41;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.Color.Black;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(96, 64);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(67, 14);
        lbl_txtcolor.TabIndex = 3;
        lbl_txtcolor.Text = "          ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(25, 64);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 41;
        label1.Text = "文字颜色：";
        btn_OK.Location = new System.Drawing.Point(121, 155);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 4;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(203, 154);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 5;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 191);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet3";
        Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet3_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
