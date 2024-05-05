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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_offcolor = new System.Windows.Forms.Label();
        lbl_oncolor = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        rdbtn3 = new System.Windows.Forms.RadioButton();
        rdbtn2 = new System.Windows.Forms.RadioButton();
        rdbtn1 = new System.Windows.Forms.RadioButton();
        txt_text = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(12, 21);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(203, 21);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(lbl_offcolor);
        groupBox1.Controls.Add(lbl_oncolor);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(14, 50);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(266, 54);
        groupBox1.TabIndex = 37;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色设置";
        lbl_offcolor.AutoSize = true;
        lbl_offcolor.BackColor = System.Drawing.Color.Red;
        lbl_offcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_offcolor.Location = new System.Drawing.Point(189, 27);
        lbl_offcolor.Name = "lbl_offcolor";
        lbl_offcolor.Size = new System.Drawing.Size(49, 14);
        lbl_offcolor.TabIndex = 3;
        lbl_offcolor.Text = "       ";
        lbl_offcolor.Click += new System.EventHandler(lbl_offcolor_Click);
        lbl_oncolor.AutoSize = true;
        lbl_oncolor.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_oncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_oncolor.Location = new System.Drawing.Point(77, 27);
        lbl_oncolor.Name = "lbl_oncolor";
        lbl_oncolor.Size = new System.Drawing.Size(43, 14);
        lbl_oncolor.TabIndex = 2;
        lbl_oncolor.Text = "      ";
        lbl_oncolor.Click += new System.EventHandler(lbl_oncolor_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(129, 28);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 1;
        label2.Text = "关颜色：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(17, 28);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "开颜色：";
        groupBox2.Controls.Add(rdbtn3);
        groupBox2.Controls.Add(rdbtn2);
        groupBox2.Controls.Add(rdbtn1);
        groupBox2.Controls.Add(txt_text);
        groupBox2.Controls.Add(label5);
        groupBox2.Location = new System.Drawing.Point(14, 111);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(264, 95);
        groupBox2.TabIndex = 38;
        groupBox2.TabStop = false;
        groupBox2.Text = "文本和类型设置";
        rdbtn3.AutoSize = true;
        rdbtn3.Location = new System.Drawing.Point(160, 61);
        rdbtn3.Name = "rdbtn3";
        rdbtn3.Size = new System.Drawing.Size(47, 16);
        rdbtn3.TabIndex = 7;
        rdbtn3.TabStop = true;
        rdbtn3.Text = "切换";
        rdbtn3.UseVisualStyleBackColor = true;
        rdbtn2.AutoSize = true;
        rdbtn2.Location = new System.Drawing.Point(88, 61);
        rdbtn2.Name = "rdbtn2";
        rdbtn2.Size = new System.Drawing.Size(47, 16);
        rdbtn2.TabIndex = 6;
        rdbtn2.TabStop = true;
        rdbtn2.Text = "只关";
        rdbtn2.UseVisualStyleBackColor = true;
        rdbtn2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
        rdbtn1.AutoSize = true;
        rdbtn1.Location = new System.Drawing.Point(19, 61);
        rdbtn1.Name = "rdbtn1";
        rdbtn1.Size = new System.Drawing.Size(47, 16);
        rdbtn1.TabIndex = 5;
        rdbtn1.TabStop = true;
        rdbtn1.Text = "只开";
        rdbtn1.UseVisualStyleBackColor = true;
        txt_text.Location = new System.Drawing.Point(88, 23);
        txt_text.Name = "txt_text";
        txt_text.Size = new System.Drawing.Size(148, 21);
        txt_text.TabIndex = 4;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(17, 28);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 0;
        label5.Text = "显示文本：";
        btn_OK.Location = new System.Drawing.Point(121, 221);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 8;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(203, 220);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 9;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 258);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet2";
        Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet2_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
