using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class YouBiao : Form
{
    public string varname;

    public float maxval = 100f;

    public float minval;

    public int mainmark = 5;

    public int othermark = 2;

    public int ptcount = 2;

    public Color color1 = Color.Black;

    public Color colorvaluert = Color.Green;

    public float changestep = 1f;

    public bool btnflag;

    private TextBox txt_var;

    private Button btn_view;

    private GroupBox groupBox1;

    private Label label1;

    private TextBox txt_mark;

    private TextBox txt_mainmark;

    private TextBox txt_minval;

    private TextBox txt_maxval;

    private Label label4;

    private Label label3;

    private Label label2;

    private TextBox txt_ptcount;

    private Label label7;

    private Label lbl_color;

    private Label label5;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private TextBox txt_step;

    private Label label6;

    private Label lbl_fillcolor;

    private Label label9;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public YouBiao()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToSingle(txt_maxval.Text) < Convert.ToSingle(txt_minval.Text) || Convert.ToInt32(txt_mainmark.Text) <= 0 || Convert.ToInt32(txt_mark.Text) < 0 || Convert.ToInt32(txt_ptcount.Text) < 0)
            {
                MessageBox.Show("信息填写有误！");
            }
            else if (Convert.ToInt32(txt_ptcount.Text) > 5)
            {
                MessageBox.Show("小数位数不能大于5");
            }
            else if (txt_var.Text == "")
            {
                MessageBox.Show("请绑定变量！");
            }
            else if (this.ckvarevent(txt_var.Text))
            {
                varname = txt_var.Text;
                maxval = Convert.ToSingle(txt_maxval.Text);
                minval = Convert.ToSingle(txt_minval.Text);
                mainmark = Convert.ToInt32(txt_mainmark.Text);
                othermark = Convert.ToInt32(txt_mark.Text);
                ptcount = Convert.ToInt32(txt_ptcount.Text);
                changestep = Convert.ToInt32(txt_step.Text);
                color1 = lbl_color.BackColor;
                colorvaluert = lbl_fillcolor.BackColor;
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("变量名错误！");
            }
        }
        catch
        {
            MessageBox.Show("信息填写不完整或有误！");
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_color_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
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

    private void YouBiao_Load(object sender, EventArgs e)
    {
        if (varname != "")
        {
            txt_var.Text = varname;
        }
        txt_maxval.Text = maxval.ToString();
        txt_minval.Text = minval.ToString();
        txt_mainmark.Text = mainmark.ToString();
        txt_mark.Text = othermark.ToString();
        txt_ptcount.Text = ptcount.ToString();
        lbl_color.BackColor = color1;
        lbl_fillcolor.BackColor = colorvaluert;
        txt_step.Text = changestep.ToString();
        if (btnflag)
        {
            label6.Visible = true;
            txt_step.Visible = true;
        }
    }

    private void lbl_fillcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor.BackColor = colorDialog1.Color;
        }
    }

    private void txt_var_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_fillcolor = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.txt_step = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.txt_ptcount = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.lbl_color = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_mark = new System.Windows.Forms.TextBox();
        this.txt_mainmark = new System.Windows.Forms.TextBox();
        this.txt_minval = new System.Windows.Forms.TextBox();
        this.txt_maxval = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(12, 19);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(227, 21);
        this.txt_var.TabIndex = 0;
        this.txt_var.Click += new System.EventHandler(txt_var_Click);
        this.btn_view.Location = new System.Drawing.Point(245, 16);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.lbl_fillcolor);
        this.groupBox1.Controls.Add(this.label9);
        this.groupBox1.Controls.Add(this.txt_step);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.txt_ptcount);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.lbl_color);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_mark);
        this.groupBox1.Controls.Add(this.txt_mainmark);
        this.groupBox1.Controls.Add(this.txt_minval);
        this.groupBox1.Controls.Add(this.txt_maxval);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(12, 46);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(314, 155);
        this.groupBox1.TabIndex = 44;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.lbl_fillcolor.AutoSize = true;
        this.lbl_fillcolor.BackColor = System.Drawing.Color.Black;
        this.lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor.Location = new System.Drawing.Point(237, 119);
        this.lbl_fillcolor.Name = "lbl_fillcolor";
        this.lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_fillcolor.TabIndex = 9;
        this.lbl_fillcolor.Text = "       ";
        this.lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(164, 119);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(65, 12);
        this.label9.TabIndex = 14;
        this.label9.Text = "填充颜色：";
        this.txt_step.Location = new System.Drawing.Point(234, 85);
        this.txt_step.Name = "txt_step";
        this.txt_step.Size = new System.Drawing.Size(55, 21);
        this.txt_step.TabIndex = 7;
        this.txt_step.Visible = false;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(164, 88);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 12);
        this.label6.TabIndex = 12;
        this.label6.Text = "增减幅度：";
        this.label6.Visible = false;
        this.txt_ptcount.Location = new System.Drawing.Point(95, 85);
        this.txt_ptcount.Name = "txt_ptcount";
        this.txt_ptcount.Size = new System.Drawing.Size(54, 21);
        this.txt_ptcount.TabIndex = 6;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(21, 88);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(65, 12);
        this.label7.TabIndex = 10;
        this.label7.Text = "小数位数：";
        this.lbl_color.AutoSize = true;
        this.lbl_color.BackColor = System.Drawing.Color.Black;
        this.lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color.Location = new System.Drawing.Point(102, 119);
        this.lbl_color.Name = "lbl_color";
        this.lbl_color.Size = new System.Drawing.Size(49, 14);
        this.lbl_color.TabIndex = 8;
        this.lbl_color.Text = "       ";
        this.lbl_color.Click += new System.EventHandler(lbl_color_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(21, 121);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(77, 12);
        this.label5.TabIndex = 8;
        this.label5.Text = "刻度盘颜色：";
        this.txt_mark.Location = new System.Drawing.Point(234, 53);
        this.txt_mark.Name = "txt_mark";
        this.txt_mark.Size = new System.Drawing.Size(55, 21);
        this.txt_mark.TabIndex = 5;
        this.txt_mainmark.Location = new System.Drawing.Point(95, 53);
        this.txt_mainmark.Name = "txt_mainmark";
        this.txt_mainmark.Size = new System.Drawing.Size(54, 21);
        this.txt_mainmark.TabIndex = 4;
        this.txt_minval.Location = new System.Drawing.Point(234, 21);
        this.txt_minval.Name = "txt_minval";
        this.txt_minval.Size = new System.Drawing.Size(55, 21);
        this.txt_minval.TabIndex = 3;
        this.txt_maxval.Location = new System.Drawing.Point(94, 21);
        this.txt_maxval.Name = "txt_maxval";
        this.txt_maxval.Size = new System.Drawing.Size(55, 21);
        this.txt_maxval.TabIndex = 2;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(164, 57);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 3;
        this.label4.Text = "副刻度数：";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(21, 57);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 2;
        this.label3.Text = "主刻度数：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(164, 26);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "最小值：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(21, 24);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "最大值：";
        this.btn_OK.Location = new System.Drawing.Point(169, 221);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 10;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(251, 221);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 11;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(347, 255);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao";
        this.Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
