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
            else if (this.ckvarevent(txt_var.Text) && this.ckvarevent(txt_var2.Text))
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
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
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
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void txt_var2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void InitializeComponent()
    {
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.gbx = new System.Windows.Forms.GroupBox();
        this.lbl_fillcolor2 = new System.Windows.Forms.Label();
        this.label14 = new System.Windows.Forms.Label();
        this.lbl_fillcolor = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.txt_ptcount2 = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.lbl_color2 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.txt_mark2 = new System.Windows.Forms.TextBox();
        this.txt_mainmark2 = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.txt_minval2 = new System.Windows.Forms.TextBox();
        this.txt_maxval2 = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
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
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.gbx.SuspendLayout();
        base.SuspendLayout();
        this.btn_OK.Location = new System.Drawing.Point(177, 376);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 17;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(259, 376);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 18;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.gbx.Controls.Add(this.lbl_fillcolor2);
        this.gbx.Controls.Add(this.label14);
        this.gbx.Controls.Add(this.lbl_fillcolor);
        this.gbx.Controls.Add(this.label16);
        this.gbx.Controls.Add(this.txt_ptcount2);
        this.gbx.Controls.Add(this.label13);
        this.gbx.Controls.Add(this.lbl_color2);
        this.gbx.Controls.Add(this.label12);
        this.gbx.Controls.Add(this.txt_mark2);
        this.gbx.Controls.Add(this.txt_mainmark2);
        this.gbx.Controls.Add(this.label9);
        this.gbx.Controls.Add(this.label10);
        this.gbx.Controls.Add(this.txt_minval2);
        this.gbx.Controls.Add(this.txt_maxval2);
        this.gbx.Controls.Add(this.label6);
        this.gbx.Controls.Add(this.label8);
        this.gbx.Controls.Add(this.txt_ptcount);
        this.gbx.Controls.Add(this.label7);
        this.gbx.Controls.Add(this.lbl_color);
        this.gbx.Controls.Add(this.label5);
        this.gbx.Controls.Add(this.txt_mark);
        this.gbx.Controls.Add(this.txt_mainmark);
        this.gbx.Controls.Add(this.txt_minval);
        this.gbx.Controls.Add(this.txt_maxval);
        this.gbx.Controls.Add(this.label4);
        this.gbx.Controls.Add(this.label3);
        this.gbx.Controls.Add(this.label2);
        this.gbx.Controls.Add(this.label1);
        this.gbx.Location = new System.Drawing.Point(22, 80);
        this.gbx.Name = "gbx";
        this.gbx.Size = new System.Drawing.Size(314, 266);
        this.gbx.TabIndex = 62;
        this.gbx.TabStop = false;
        this.gbx.Text = "设置";
        this.lbl_fillcolor2.AutoSize = true;
        this.lbl_fillcolor2.BackColor = System.Drawing.Color.Lime;
        this.lbl_fillcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor2.Location = new System.Drawing.Point(237, 183);
        this.lbl_fillcolor2.Name = "lbl_fillcolor2";
        this.lbl_fillcolor2.Size = new System.Drawing.Size(43, 14);
        this.lbl_fillcolor2.TabIndex = 15;
        this.lbl_fillcolor2.Text = "      ";
        this.lbl_fillcolor2.Click += new System.EventHandler(lbl_fillcolor2_Click);
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(164, 185);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(71, 12);
        this.label14.TabIndex = 26;
        this.label14.Text = "填充颜色2：";
        this.lbl_fillcolor.AutoSize = true;
        this.lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        this.lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor.Location = new System.Drawing.Point(98, 185);
        this.lbl_fillcolor.Name = "lbl_fillcolor";
        this.lbl_fillcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_fillcolor.TabIndex = 14;
        this.lbl_fillcolor.Text = "      ";
        this.lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(20, 185);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(65, 12);
        this.label16.TabIndex = 24;
        this.label16.Text = "填充颜色：";
        this.txt_ptcount2.Location = new System.Drawing.Point(233, 145);
        this.txt_ptcount2.Name = "txt_ptcount2";
        this.txt_ptcount2.Size = new System.Drawing.Size(55, 21);
        this.txt_ptcount2.TabIndex = 13;
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(164, 149);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(71, 12);
        this.label13.TabIndex = 22;
        this.label13.Text = "小数位数2：";
        this.lbl_color2.AutoSize = true;
        this.lbl_color2.BackColor = System.Drawing.Color.Lime;
        this.lbl_color2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color2.Location = new System.Drawing.Point(253, 247);
        this.lbl_color2.Name = "lbl_color2";
        this.lbl_color2.Size = new System.Drawing.Size(43, 14);
        this.lbl_color2.TabIndex = 19;
        this.lbl_color2.Text = "      ";
        this.lbl_color2.Visible = false;
        this.lbl_color2.Click += new System.EventHandler(label11_Click);
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(152, 249);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(83, 12);
        this.label12.TabIndex = 20;
        this.label12.Text = "刻度盘颜色2：";
        this.label12.Visible = false;
        this.txt_mark2.Location = new System.Drawing.Point(233, 113);
        this.txt_mark2.Name = "txt_mark2";
        this.txt_mark2.Size = new System.Drawing.Size(55, 21);
        this.txt_mark2.TabIndex = 11;
        this.txt_mainmark2.Location = new System.Drawing.Point(87, 113);
        this.txt_mainmark2.Name = "txt_mainmark2";
        this.txt_mainmark2.Size = new System.Drawing.Size(55, 21);
        this.txt_mainmark2.TabIndex = 10;
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(164, 116);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(71, 12);
        this.label9.TabIndex = 17;
        this.label9.Text = "副刻度数2：";
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(20, 116);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(71, 12);
        this.label10.TabIndex = 16;
        this.label10.Text = "主刻度数2：";
        this.txt_minval2.Location = new System.Drawing.Point(233, 49);
        this.txt_minval2.Name = "txt_minval2";
        this.txt_minval2.Size = new System.Drawing.Size(55, 21);
        this.txt_minval2.TabIndex = 7;
        this.txt_maxval2.Location = new System.Drawing.Point(87, 49);
        this.txt_maxval2.Name = "txt_maxval2";
        this.txt_maxval2.Size = new System.Drawing.Size(55, 21);
        this.txt_maxval2.TabIndex = 6;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(164, 52);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(59, 12);
        this.label6.TabIndex = 13;
        this.label6.Text = "最小值2：";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(20, 52);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(59, 12);
        this.label8.TabIndex = 12;
        this.label8.Text = "最大值2：";
        this.txt_ptcount.Location = new System.Drawing.Point(87, 145);
        this.txt_ptcount.Name = "txt_ptcount";
        this.txt_ptcount.Size = new System.Drawing.Size(55, 21);
        this.txt_ptcount.TabIndex = 12;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(20, 149);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(65, 12);
        this.label7.TabIndex = 10;
        this.label7.Text = "小数位数：";
        this.lbl_color.AutoSize = true;
        this.lbl_color.BackColor = System.Drawing.Color.Lime;
        this.lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color.Location = new System.Drawing.Point(98, 213);
        this.lbl_color.Name = "lbl_color";
        this.lbl_color.Size = new System.Drawing.Size(43, 14);
        this.lbl_color.TabIndex = 16;
        this.lbl_color.Text = "      ";
        this.lbl_color.Click += new System.EventHandler(lbl_color_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(20, 215);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(77, 12);
        this.label5.TabIndex = 8;
        this.label5.Text = "刻度盘颜色：";
        this.txt_mark.Location = new System.Drawing.Point(233, 81);
        this.txt_mark.Name = "txt_mark";
        this.txt_mark.Size = new System.Drawing.Size(55, 21);
        this.txt_mark.TabIndex = 9;
        this.txt_mainmark.Location = new System.Drawing.Point(87, 81);
        this.txt_mainmark.Name = "txt_mainmark";
        this.txt_mainmark.Size = new System.Drawing.Size(55, 21);
        this.txt_mainmark.TabIndex = 8;
        this.txt_minval.Location = new System.Drawing.Point(233, 17);
        this.txt_minval.Name = "txt_minval";
        this.txt_minval.Size = new System.Drawing.Size(55, 21);
        this.txt_minval.TabIndex = 5;
        this.txt_maxval.Location = new System.Drawing.Point(87, 17);
        this.txt_maxval.Name = "txt_maxval";
        this.txt_maxval.Size = new System.Drawing.Size(55, 21);
        this.txt_maxval.TabIndex = 4;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(164, 84);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 3;
        this.label4.Text = "副刻度数：";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(20, 84);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 2;
        this.label3.Text = "主刻度数：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(164, 20);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "最小值：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(20, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "最大值：";
        this.txt_var.Location = new System.Drawing.Point(22, 20);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(227, 21);
        this.txt_var.TabIndex = 0;
        this.txt_var.Click += new System.EventHandler(txt_var_Click);
        this.btn_view.Location = new System.Drawing.Point(255, 18);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "变量1";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_var2.Location = new System.Drawing.Point(22, 50);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(227, 21);
        this.txt_var2.TabIndex = 2;
        this.txt_var2.Click += new System.EventHandler(txt_var2_Click);
        this.button1.Location = new System.Drawing.Point(255, 51);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 3;
        this.button1.Text = "变量2";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(346, 411);
        base.Controls.Add(this.txt_var2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.gbx);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao2";
        this.Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao2_Load);
        this.gbx.ResumeLayout(false);
        this.gbx.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
