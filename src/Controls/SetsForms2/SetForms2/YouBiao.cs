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
            else if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void InitializeComponent()
    {
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_fillcolor = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        txt_step = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        txt_ptcount = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        lbl_color = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        txt_mark = new System.Windows.Forms.TextBox();
        txt_mainmark = new System.Windows.Forms.TextBox();
        txt_minval = new System.Windows.Forms.TextBox();
        txt_maxval = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(12, 19);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(227, 21);
        txt_var.TabIndex = 0;
        txt_var.Click += new System.EventHandler(txt_var_Click);
        btn_view.Location = new System.Drawing.Point(245, 16);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(lbl_fillcolor);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(txt_step);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(txt_ptcount);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(lbl_color);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_mark);
        groupBox1.Controls.Add(txt_mainmark);
        groupBox1.Controls.Add(txt_minval);
        groupBox1.Controls.Add(txt_maxval);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(12, 46);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(314, 155);
        groupBox1.TabIndex = 44;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        lbl_fillcolor.AutoSize = true;
        lbl_fillcolor.BackColor = System.Drawing.Color.Black;
        lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor.Location = new System.Drawing.Point(237, 119);
        lbl_fillcolor.Name = "lbl_fillcolor";
        lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        lbl_fillcolor.TabIndex = 9;
        lbl_fillcolor.Text = "       ";
        lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(164, 119);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(65, 12);
        label9.TabIndex = 14;
        label9.Text = "填充颜色：";
        txt_step.Location = new System.Drawing.Point(234, 85);
        txt_step.Name = "txt_step";
        txt_step.Size = new System.Drawing.Size(55, 21);
        txt_step.TabIndex = 7;
        txt_step.Visible = false;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(164, 88);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 12);
        label6.TabIndex = 12;
        label6.Text = "增减幅度：";
        label6.Visible = false;
        txt_ptcount.Location = new System.Drawing.Point(95, 85);
        txt_ptcount.Name = "txt_ptcount";
        txt_ptcount.Size = new System.Drawing.Size(54, 21);
        txt_ptcount.TabIndex = 6;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(21, 88);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 10;
        label7.Text = "小数位数：";
        lbl_color.AutoSize = true;
        lbl_color.BackColor = System.Drawing.Color.Black;
        lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color.Location = new System.Drawing.Point(102, 119);
        lbl_color.Name = "lbl_color";
        lbl_color.Size = new System.Drawing.Size(49, 14);
        lbl_color.TabIndex = 8;
        lbl_color.Text = "       ";
        lbl_color.Click += new System.EventHandler(lbl_color_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(21, 121);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(77, 12);
        label5.TabIndex = 8;
        label5.Text = "刻度盘颜色：";
        txt_mark.Location = new System.Drawing.Point(234, 53);
        txt_mark.Name = "txt_mark";
        txt_mark.Size = new System.Drawing.Size(55, 21);
        txt_mark.TabIndex = 5;
        txt_mainmark.Location = new System.Drawing.Point(95, 53);
        txt_mainmark.Name = "txt_mainmark";
        txt_mainmark.Size = new System.Drawing.Size(54, 21);
        txt_mainmark.TabIndex = 4;
        txt_minval.Location = new System.Drawing.Point(234, 21);
        txt_minval.Name = "txt_minval";
        txt_minval.Size = new System.Drawing.Size(55, 21);
        txt_minval.TabIndex = 3;
        txt_maxval.Location = new System.Drawing.Point(94, 21);
        txt_maxval.Name = "txt_maxval";
        txt_maxval.Size = new System.Drawing.Size(55, 21);
        txt_maxval.TabIndex = 2;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(164, 57);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 3;
        label4.Text = "副刻度数：";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(21, 57);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 2;
        label3.Text = "主刻度数：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(164, 26);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 1;
        label2.Text = "最小值：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(21, 24);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "最大值：";
        btn_OK.Location = new System.Drawing.Point(169, 221);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 10;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(251, 221);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 11;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(347, 255);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao";
        Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
