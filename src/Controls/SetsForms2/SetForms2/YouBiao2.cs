using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class YouBiao2 : Form
{
    public string varname;

    public float maxval = 100f;

    public float minval;

    public int mainmark = 5;

    public int othermark = 2;

    public int ptcount = 2;

    public Color colorvaluert1 = Color.Orange;

    public Color color1 = Color.Green;

    public string varname2;

    public float maxval2 = 100f;

    public float minval2;

    public int mainmark2 = 5;

    public int othermark2 = 2;

    public int ptcount2 = 2;

    public Color colorvaluert2 = Color.Orange;

    public Color color2 = Color.Green;

    private Button btn_OK;

    private Button btn_cancel;

    private GroupBox gbx;

    private TextBox txt_ptcount;

    private Label label7;

    private Label lbl_color;

    private Label label5;

    private TextBox txt_mark;

    private TextBox txt_mainmark;

    private TextBox txt_minval;

    private TextBox txt_maxval;

    private Label label4;

    private Label label3;

    private Label label2;

    private Label label1;

    private TextBox txt_var;

    private Button btn_view;

    private TextBox txt_var2;

    private Button button1;

    private TextBox txt_mark2;

    private TextBox txt_mainmark2;

    private Label label9;

    private Label label10;

    private TextBox txt_minval2;

    private TextBox txt_maxval2;

    private Label label6;

    private Label label8;

    private TextBox txt_ptcount2;

    private Label label13;

    private Label lbl_color2;

    private Label label12;

    private ColorDialog colorDialog1;

    private Label lbl_fillcolor2;

    private Label label14;

    private Label lbl_fillcolor;

    private Label label16;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public YouBiao2()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToSingle(txt_maxval.Text) < Convert.ToSingle(txt_minval.Text) || Convert.ToInt32(txt_mainmark.Text) <= 0 || Convert.ToInt32(txt_mark.Text) < 0 || Convert.ToInt32(txt_ptcount.Text) < 0 || Convert.ToSingle(txt_maxval2.Text) < Convert.ToSingle(txt_minval2.Text) || Convert.ToInt32(txt_mainmark2.Text) <= 0 || Convert.ToInt32(txt_mark2.Text) < 0 || Convert.ToInt32(txt_ptcount2.Text) < 0 || Convert.ToSingle(txt_maxval.Text) < Convert.ToSingle(txt_minval.Text) || Convert.ToSingle(txt_maxval2.Text) < Convert.ToSingle(txt_minval2.Text))
            {
                MessageBox.Show("信息填写有误！");
            }
            else if (string.IsNullOrEmpty(txt_var.Text) || string.IsNullOrEmpty(txt_var2.Text))
            {
                MessageBox.Show("变量填写不完整！");
            }
            else if (Convert.ToInt32(txt_ptcount.Text) > 10 || Convert.ToInt32(txt_ptcount2.Text) > 10)
            {
                MessageBox.Show("小数位数不能大于10！");
            }
            else if (ckvarevent(txt_var.Text) && ckvarevent(txt_var2.Text))
            {
                varname = txt_var.Text;
                maxval = Convert.ToSingle(txt_maxval.Text);
                minval = Convert.ToSingle(txt_minval.Text);
                mainmark = Convert.ToInt32(txt_mainmark.Text);
                othermark = Convert.ToInt32(txt_mark.Text);
                ptcount = Convert.ToInt32(txt_ptcount.Text);
                color1 = lbl_color.BackColor;
                colorvaluert1 = lbl_fillcolor.BackColor;
                varname2 = txt_var2.Text;
                maxval2 = Convert.ToSingle(txt_maxval2.Text);
                minval2 = Convert.ToSingle(txt_minval2.Text);
                mainmark2 = Convert.ToInt32(txt_mainmark2.Text);
                othermark2 = Convert.ToInt32(txt_mark2.Text);
                ptcount2 = Convert.ToInt32(txt_ptcount2.Text);
                color2 = lbl_color2.BackColor;
                colorvaluert2 = lbl_fillcolor2.BackColor;
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
            MessageBox.Show("信息填写有误或不完整！");
        }
    }

    private void lbl_color_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
        }
    }

    private void label11_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color2.BackColor = colorDialog1.Color;
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

    private void button1_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void YouBiao2_Load(object sender, EventArgs e)
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
        lbl_fillcolor.BackColor = colorvaluert1;
        if (varname2 != "")
        {
            txt_var2.Text = varname2;
        }
        txt_maxval2.Text = maxval2.ToString();
        txt_minval2.Text = minval2.ToString();
        txt_mainmark2.Text = mainmark2.ToString();
        txt_mark2.Text = othermark2.ToString();
        txt_ptcount2.Text = ptcount2.ToString();
        lbl_color2.BackColor = color2;
        lbl_fillcolor2.BackColor = colorvaluert2;
    }

    private void lbl_fillcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_fillcolor2_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor2.BackColor = colorDialog1.Color;
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

    private void txt_var2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void InitializeComponent()
    {
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        gbx = new System.Windows.Forms.GroupBox();
        lbl_fillcolor2 = new System.Windows.Forms.Label();
        label14 = new System.Windows.Forms.Label();
        lbl_fillcolor = new System.Windows.Forms.Label();
        label16 = new System.Windows.Forms.Label();
        txt_ptcount2 = new System.Windows.Forms.TextBox();
        label13 = new System.Windows.Forms.Label();
        lbl_color2 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        txt_mark2 = new System.Windows.Forms.TextBox();
        txt_mainmark2 = new System.Windows.Forms.TextBox();
        label9 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        txt_minval2 = new System.Windows.Forms.TextBox();
        txt_maxval2 = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        txt_var2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        gbx.SuspendLayout();
        base.SuspendLayout();
        btn_OK.Location = new System.Drawing.Point(177, 376);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 17;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(259, 376);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 18;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        gbx.Controls.Add(lbl_fillcolor2);
        gbx.Controls.Add(label14);
        gbx.Controls.Add(lbl_fillcolor);
        gbx.Controls.Add(label16);
        gbx.Controls.Add(txt_ptcount2);
        gbx.Controls.Add(label13);
        gbx.Controls.Add(lbl_color2);
        gbx.Controls.Add(label12);
        gbx.Controls.Add(txt_mark2);
        gbx.Controls.Add(txt_mainmark2);
        gbx.Controls.Add(label9);
        gbx.Controls.Add(label10);
        gbx.Controls.Add(txt_minval2);
        gbx.Controls.Add(txt_maxval2);
        gbx.Controls.Add(label6);
        gbx.Controls.Add(label8);
        gbx.Controls.Add(txt_ptcount);
        gbx.Controls.Add(label7);
        gbx.Controls.Add(lbl_color);
        gbx.Controls.Add(label5);
        gbx.Controls.Add(txt_mark);
        gbx.Controls.Add(txt_mainmark);
        gbx.Controls.Add(txt_minval);
        gbx.Controls.Add(txt_maxval);
        gbx.Controls.Add(label4);
        gbx.Controls.Add(label3);
        gbx.Controls.Add(label2);
        gbx.Controls.Add(label1);
        gbx.Location = new System.Drawing.Point(22, 80);
        gbx.Name = "gbx";
        gbx.Size = new System.Drawing.Size(314, 266);
        gbx.TabIndex = 62;
        gbx.TabStop = false;
        gbx.Text = "设置";
        lbl_fillcolor2.AutoSize = true;
        lbl_fillcolor2.BackColor = System.Drawing.Color.Lime;
        lbl_fillcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor2.Location = new System.Drawing.Point(237, 183);
        lbl_fillcolor2.Name = "lbl_fillcolor2";
        lbl_fillcolor2.Size = new System.Drawing.Size(43, 14);
        lbl_fillcolor2.TabIndex = 15;
        lbl_fillcolor2.Text = "      ";
        lbl_fillcolor2.Click += new System.EventHandler(lbl_fillcolor2_Click);
        label14.AutoSize = true;
        label14.Location = new System.Drawing.Point(164, 185);
        label14.Name = "label14";
        label14.Size = new System.Drawing.Size(71, 12);
        label14.TabIndex = 26;
        label14.Text = "填充颜色2：";
        lbl_fillcolor.AutoSize = true;
        lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor.Location = new System.Drawing.Point(98, 185);
        lbl_fillcolor.Name = "lbl_fillcolor";
        lbl_fillcolor.Size = new System.Drawing.Size(43, 14);
        lbl_fillcolor.TabIndex = 14;
        lbl_fillcolor.Text = "      ";
        lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        label16.AutoSize = true;
        label16.Location = new System.Drawing.Point(20, 185);
        label16.Name = "label16";
        label16.Size = new System.Drawing.Size(65, 12);
        label16.TabIndex = 24;
        label16.Text = "填充颜色：";
        txt_ptcount2.Location = new System.Drawing.Point(233, 145);
        txt_ptcount2.Name = "txt_ptcount2";
        txt_ptcount2.Size = new System.Drawing.Size(55, 21);
        txt_ptcount2.TabIndex = 13;
        label13.AutoSize = true;
        label13.Location = new System.Drawing.Point(164, 149);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(71, 12);
        label13.TabIndex = 22;
        label13.Text = "小数位数2：";
        lbl_color2.AutoSize = true;
        lbl_color2.BackColor = System.Drawing.Color.Lime;
        lbl_color2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color2.Location = new System.Drawing.Point(253, 247);
        lbl_color2.Name = "lbl_color2";
        lbl_color2.Size = new System.Drawing.Size(43, 14);
        lbl_color2.TabIndex = 19;
        lbl_color2.Text = "      ";
        lbl_color2.Visible = false;
        lbl_color2.Click += new System.EventHandler(label11_Click);
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(152, 249);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(83, 12);
        label12.TabIndex = 20;
        label12.Text = "刻度盘颜色2：";
        label12.Visible = false;
        txt_mark2.Location = new System.Drawing.Point(233, 113);
        txt_mark2.Name = "txt_mark2";
        txt_mark2.Size = new System.Drawing.Size(55, 21);
        txt_mark2.TabIndex = 11;
        txt_mainmark2.Location = new System.Drawing.Point(87, 113);
        txt_mainmark2.Name = "txt_mainmark2";
        txt_mainmark2.Size = new System.Drawing.Size(55, 21);
        txt_mainmark2.TabIndex = 10;
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(164, 116);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(71, 12);
        label9.TabIndex = 17;
        label9.Text = "副刻度数2：";
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(20, 116);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(71, 12);
        label10.TabIndex = 16;
        label10.Text = "主刻度数2：";
        txt_minval2.Location = new System.Drawing.Point(233, 49);
        txt_minval2.Name = "txt_minval2";
        txt_minval2.Size = new System.Drawing.Size(55, 21);
        txt_minval2.TabIndex = 7;
        txt_maxval2.Location = new System.Drawing.Point(87, 49);
        txt_maxval2.Name = "txt_maxval2";
        txt_maxval2.Size = new System.Drawing.Size(55, 21);
        txt_maxval2.TabIndex = 6;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(164, 52);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(59, 12);
        label6.TabIndex = 13;
        label6.Text = "最小值2：";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(20, 52);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(59, 12);
        label8.TabIndex = 12;
        label8.Text = "最大值2：";
        txt_ptcount.Location = new System.Drawing.Point(87, 145);
        txt_ptcount.Name = "txt_ptcount";
        txt_ptcount.Size = new System.Drawing.Size(55, 21);
        txt_ptcount.TabIndex = 12;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(20, 149);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 10;
        label7.Text = "小数位数：";
        lbl_color.AutoSize = true;
        lbl_color.BackColor = System.Drawing.Color.Lime;
        lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color.Location = new System.Drawing.Point(98, 213);
        lbl_color.Name = "lbl_color";
        lbl_color.Size = new System.Drawing.Size(43, 14);
        lbl_color.TabIndex = 16;
        lbl_color.Text = "      ";
        lbl_color.Click += new System.EventHandler(lbl_color_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(20, 215);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(77, 12);
        label5.TabIndex = 8;
        label5.Text = "刻度盘颜色：";
        txt_mark.Location = new System.Drawing.Point(233, 81);
        txt_mark.Name = "txt_mark";
        txt_mark.Size = new System.Drawing.Size(55, 21);
        txt_mark.TabIndex = 9;
        txt_mainmark.Location = new System.Drawing.Point(87, 81);
        txt_mainmark.Name = "txt_mainmark";
        txt_mainmark.Size = new System.Drawing.Size(55, 21);
        txt_mainmark.TabIndex = 8;
        txt_minval.Location = new System.Drawing.Point(233, 17);
        txt_minval.Name = "txt_minval";
        txt_minval.Size = new System.Drawing.Size(55, 21);
        txt_minval.TabIndex = 5;
        txt_maxval.Location = new System.Drawing.Point(87, 17);
        txt_maxval.Name = "txt_maxval";
        txt_maxval.Size = new System.Drawing.Size(55, 21);
        txt_maxval.TabIndex = 4;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(164, 84);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 3;
        label4.Text = "副刻度数：";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(20, 84);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 2;
        label3.Text = "主刻度数：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(164, 20);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 1;
        label2.Text = "最小值：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(20, 20);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "最大值：";
        txt_var.Location = new System.Drawing.Point(22, 20);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(227, 21);
        txt_var.TabIndex = 0;
        txt_var.Click += new System.EventHandler(txt_var_Click);
        btn_view.Location = new System.Drawing.Point(255, 18);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "变量1";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_var2.Location = new System.Drawing.Point(22, 50);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(227, 21);
        txt_var2.TabIndex = 2;
        txt_var2.Click += new System.EventHandler(txt_var2_Click);
        button1.Location = new System.Drawing.Point(255, 51);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 3;
        button1.Text = "变量2";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(346, 411);
        base.Controls.Add(txt_var2);
        base.Controls.Add(button1);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(gbx);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao2";
        Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao2_Load);
        gbx.ResumeLayout(false);
        gbx.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
