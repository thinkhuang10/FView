using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BtnSet2 : Form
{
    public string varname;

    public string txt = "关";

    public Color oncolor = Color.Green;

    public Color offcolor = Color.Red;

    public int btntype = 3;

    private TextBox txt_var;

    private Button btn_view;

    private GroupBox groupBox1;

    private Label lbl_offcolor;

    private Label lbl_oncolor;

    private Label label2;

    private Label label1;

    private GroupBox groupBox2;

    private RadioButton rdbtn3;

    private RadioButton rdbtn2;

    private RadioButton rdbtn1;

    private TextBox txt_text;

    private Label label5;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BtnSet2()
    {
        InitializeComponent();
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (txt_var.Text != "" && txt_text.Text != "")
                {
                    varname = txt_var.Text;
                    txt = txt_text.Text;
                    oncolor = lbl_oncolor.BackColor;
                    offcolor = lbl_offcolor.BackColor;
                    if (rdbtn1.Checked)
                    {
                        btntype = 1;
                    }
                    else if (rdbtn2.Checked)
                    {
                        btntype = 2;
                    }
                    else
                    {
                        btntype = 3;
                    }
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
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void lbl_offcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_offcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_oncolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_oncolor.BackColor = colorDialog1.Color;
        }
    }

    private void BtnSet2_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
        }
        lbl_oncolor.BackColor = oncolor;
        lbl_offcolor.BackColor = offcolor;
        txt_text.Text = txt;
        if (btntype == 1)
        {
            rdbtn1.Checked = true;
        }
        else if (btntype == 2)
        {
            rdbtn2.Checked = true;
        }
        else
        {
            rdbtn3.Checked = true;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_offcolor = new System.Windows.Forms.Label();
        this.lbl_oncolor = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.rdbtn3 = new System.Windows.Forms.RadioButton();
        this.rdbtn2 = new System.Windows.Forms.RadioButton();
        this.rdbtn1 = new System.Windows.Forms.RadioButton();
        this.txt_text = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(12, 21);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(203, 21);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.lbl_offcolor);
        this.groupBox1.Controls.Add(this.lbl_oncolor);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(14, 50);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(266, 54);
        this.groupBox1.TabIndex = 37;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色设置";
        this.lbl_offcolor.AutoSize = true;
        this.lbl_offcolor.BackColor = System.Drawing.Color.Red;
        this.lbl_offcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_offcolor.Location = new System.Drawing.Point(189, 27);
        this.lbl_offcolor.Name = "lbl_offcolor";
        this.lbl_offcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_offcolor.TabIndex = 3;
        this.lbl_offcolor.Text = "       ";
        this.lbl_offcolor.Click += new System.EventHandler(lbl_offcolor_Click);
        this.lbl_oncolor.AutoSize = true;
        this.lbl_oncolor.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_oncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_oncolor.Location = new System.Drawing.Point(77, 27);
        this.lbl_oncolor.Name = "lbl_oncolor";
        this.lbl_oncolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_oncolor.TabIndex = 2;
        this.lbl_oncolor.Text = "      ";
        this.lbl_oncolor.Click += new System.EventHandler(lbl_oncolor_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(129, 28);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "关颜色：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(17, 28);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "开颜色：";
        this.groupBox2.Controls.Add(this.rdbtn3);
        this.groupBox2.Controls.Add(this.rdbtn2);
        this.groupBox2.Controls.Add(this.rdbtn1);
        this.groupBox2.Controls.Add(this.txt_text);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Location = new System.Drawing.Point(14, 111);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(264, 95);
        this.groupBox2.TabIndex = 38;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "文本和类型设置";
        this.rdbtn3.AutoSize = true;
        this.rdbtn3.Location = new System.Drawing.Point(160, 61);
        this.rdbtn3.Name = "rdbtn3";
        this.rdbtn3.Size = new System.Drawing.Size(47, 16);
        this.rdbtn3.TabIndex = 7;
        this.rdbtn3.TabStop = true;
        this.rdbtn3.Text = "切换";
        this.rdbtn3.UseVisualStyleBackColor = true;
        this.rdbtn2.AutoSize = true;
        this.rdbtn2.Location = new System.Drawing.Point(88, 61);
        this.rdbtn2.Name = "rdbtn2";
        this.rdbtn2.Size = new System.Drawing.Size(47, 16);
        this.rdbtn2.TabIndex = 6;
        this.rdbtn2.TabStop = true;
        this.rdbtn2.Text = "只关";
        this.rdbtn2.UseVisualStyleBackColor = true;
        this.rdbtn2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
        this.rdbtn1.AutoSize = true;
        this.rdbtn1.Location = new System.Drawing.Point(19, 61);
        this.rdbtn1.Name = "rdbtn1";
        this.rdbtn1.Size = new System.Drawing.Size(47, 16);
        this.rdbtn1.TabIndex = 5;
        this.rdbtn1.TabStop = true;
        this.rdbtn1.Text = "只开";
        this.rdbtn1.UseVisualStyleBackColor = true;
        this.txt_text.Location = new System.Drawing.Point(88, 23);
        this.txt_text.Name = "txt_text";
        this.txt_text.Size = new System.Drawing.Size(148, 21);
        this.txt_text.TabIndex = 4;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(17, 28);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "显示文本：";
        this.btn_OK.Location = new System.Drawing.Point(121, 221);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 8;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(203, 220);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 9;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 258);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet2";
        this.Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet2_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
