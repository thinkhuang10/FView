using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class PSet1 : Form
{
    public string varname1;

    public string varname2;

    public Color PipeColor = Color.LightGray;

    public Color LColor = Color.Blue;

    public int SPDFlag = 1;

    public int TypeFlag = 1;

    public float divwth = 10f;

    public float LHigh = 10f;

    public float LWth = 10f;

    private GroupBox groupBox1;

    private Button btn_var2;

    private Button btn_var1;

    private TextBox txt_var2;

    private TextBox txt_var1;

    private Label label2;

    private Label label1;

    private RadioButton rd_chuizhi;

    private RadioButton rd_shuiping;

    private Label lbl_pipecolor;

    private Label label3;

    private GroupBox groupBox2;

    private Label label9;

    private Label label8;

    private Label label7;

    private Label lbl_lcolor;

    private Label label5;

    private Button button1;

    private Button button2;

    private TextBox txt_divwth;

    private TextBox txt_lwth;

    private TextBox txt_lhigh;

    private GroupBox groupBox3;

    private RadioButton rd_veryfast;

    private RadioButton rd_veryslow;

    private RadioButton rd_fast;

    private RadioButton rd_nml;

    private RadioButton rd_slow;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public PSet1()
    {
        InitializeComponent();
    }

    private void PSet1_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname1))
        {
            txt_var1.Text = varname1.Substring(1, varname1.Length - 2);
            txt_var2.Text = varname2.Substring(1, varname2.Length - 2);
        }
        lbl_lcolor.BackColor = LColor;
        lbl_pipecolor.BackColor = PipeColor;
        switch (SPDFlag)
        {
            case 1:
                rd_veryslow.Checked = true;
                break;
            case 2:
                rd_slow.Checked = true;
                break;
            case 3:
                rd_nml.Checked = true;
                break;
            case 4:
                rd_fast.Checked = true;
                break;
            case 5:
                rd_veryfast.Checked = true;
                break;
        }
        if (TypeFlag == 1)
        {
            rd_shuiping.Checked = true;
        }
        else
        {
            rd_chuizhi.Checked = true;
        }
        txt_lhigh.Text = LHigh.ToString();
        txt_lwth.Text = LWth.ToString();
        txt_divwth.Text = divwth.ToString();
    }

    private void txt_var1_TextChanged(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var1.Text) && this.ckvarevent(txt_var2.Text))
            {
                if (string.IsNullOrEmpty(txt_divwth.Text) || string.IsNullOrEmpty(txt_lhigh.Text) || string.IsNullOrEmpty(txt_lwth.Text) || string.IsNullOrEmpty(txt_var2.Text) || string.IsNullOrEmpty(txt_var1.Text))
                {
                    MessageBox.Show("请将信息填写完整！");
                    return;
                }
                if (Convert.ToInt32(txt_divwth.Text) <= 0 || Convert.ToInt32(txt_lhigh.Text) <= 0 || Convert.ToInt32(txt_lwth.Text) <= 0)
                {
                    MessageBox.Show("宽度或长度必须大于零！");
                    return;
                }
                varname1 = "[" + txt_var1.Text + "]";
                varname2 = "[" + txt_var2.Text + "]";
                PipeColor = lbl_pipecolor.BackColor;
                LColor = lbl_lcolor.BackColor;
                if (rd_shuiping.Checked)
                {
                    TypeFlag = 1;
                }
                else
                {
                    TypeFlag = 2;
                }
                divwth = Convert.ToSingle(txt_divwth.Text);
                LHigh = Convert.ToSingle(txt_lhigh.Text);
                LWth = Convert.ToSingle(txt_lwth.Text);
                if (rd_veryslow.Checked)
                {
                    SPDFlag = 1;
                }
                else if (rd_slow.Checked)
                {
                    SPDFlag = 2;
                }
                else if (rd_nml.Checked)
                {
                    SPDFlag = 3;
                }
                else if (rd_fast.Checked)
                {
                    SPDFlag = 4;
                }
                else if (rd_veryfast.Checked)
                {
                    SPDFlag = 5;
                }
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("信息填写不正确！");
        }
    }

    private void btn_var1_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void btn_var2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_lcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_lcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_pipecolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_pipecolor.BackColor = colorDialog1.Color;
        }
    }
    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.rd_chuizhi = new System.Windows.Forms.RadioButton();
        this.rd_shuiping = new System.Windows.Forms.RadioButton();
        this.lbl_pipecolor = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.btn_var2 = new System.Windows.Forms.Button();
        this.btn_var1 = new System.Windows.Forms.Button();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.txt_var1 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.rd_veryfast = new System.Windows.Forms.RadioButton();
        this.rd_veryslow = new System.Windows.Forms.RadioButton();
        this.rd_fast = new System.Windows.Forms.RadioButton();
        this.rd_nml = new System.Windows.Forms.RadioButton();
        this.rd_slow = new System.Windows.Forms.RadioButton();
        this.txt_divwth = new System.Windows.Forms.TextBox();
        this.txt_lwth = new System.Windows.Forms.TextBox();
        this.txt_lhigh = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.lbl_lcolor = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.rd_chuizhi);
        this.groupBox1.Controls.Add(this.rd_shuiping);
        this.groupBox1.Controls.Add(this.lbl_pipecolor);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.btn_var2);
        this.groupBox1.Controls.Add(this.btn_var1);
        this.groupBox1.Controls.Add(this.txt_var2);
        this.groupBox1.Controls.Add(this.txt_var1);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(12, 13);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(319, 119);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "管道属性";
        this.rd_chuizhi.AutoSize = true;
        this.rd_chuizhi.Location = new System.Drawing.Point(239, 86);
        this.rd_chuizhi.Name = "rd_chuizhi";
        this.rd_chuizhi.Size = new System.Drawing.Size(47, 16);
        this.rd_chuizhi.TabIndex = 6;
        this.rd_chuizhi.TabStop = true;
        this.rd_chuizhi.Text = "垂直";
        this.rd_chuizhi.UseVisualStyleBackColor = true;
        this.rd_chuizhi.Visible = false;
        this.rd_shuiping.AutoSize = true;
        this.rd_shuiping.Checked = true;
        this.rd_shuiping.Location = new System.Drawing.Point(173, 86);
        this.rd_shuiping.Name = "rd_shuiping";
        this.rd_shuiping.Size = new System.Drawing.Size(47, 16);
        this.rd_shuiping.TabIndex = 5;
        this.rd_shuiping.TabStop = true;
        this.rd_shuiping.Text = "水平";
        this.rd_shuiping.UseVisualStyleBackColor = true;
        this.rd_shuiping.Visible = false;
        this.lbl_pipecolor.AutoSize = true;
        this.lbl_pipecolor.BackColor = System.Drawing.Color.Silver;
        this.lbl_pipecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_pipecolor.Location = new System.Drawing.Point(87, 86);
        this.lbl_pipecolor.Name = "lbl_pipecolor";
        this.lbl_pipecolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_pipecolor.TabIndex = 4;
        this.lbl_pipecolor.Text = "      ";
        this.lbl_pipecolor.Click += new System.EventHandler(lbl_pipecolor_Click);
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(16, 86);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 6;
        this.label3.Text = "管道颜色：";
        this.btn_var2.Location = new System.Drawing.Point(225, 54);
        this.btn_var2.Name = "btn_var2";
        this.btn_var2.Size = new System.Drawing.Size(75, 23);
        this.btn_var2.TabIndex = 3;
        this.btn_var2.Text = "变量选择";
        this.btn_var2.UseVisualStyleBackColor = true;
        this.btn_var2.Click += new System.EventHandler(btn_var2_Click);
        this.btn_var1.Location = new System.Drawing.Point(225, 24);
        this.btn_var1.Name = "btn_var1";
        this.btn_var1.Size = new System.Drawing.Size(75, 23);
        this.btn_var1.TabIndex = 1;
        this.btn_var1.Text = "变量选择";
        this.btn_var1.UseVisualStyleBackColor = true;
        this.btn_var1.Click += new System.EventHandler(btn_var1_Click);
        this.txt_var2.Location = new System.Drawing.Point(87, 53);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(118, 21);
        this.txt_var2.TabIndex = 2;
        this.txt_var1.Location = new System.Drawing.Point(87, 26);
        this.txt_var1.Name = "txt_var1";
        this.txt_var1.Size = new System.Drawing.Size(119, 21);
        this.txt_var1.TabIndex = 0;
        this.txt_var1.TextChanged += new System.EventHandler(txt_var1_TextChanged);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(16, 56);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "流动使能：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "流动方向：";
        this.groupBox2.Controls.Add(this.groupBox3);
        this.groupBox2.Controls.Add(this.txt_divwth);
        this.groupBox2.Controls.Add(this.txt_lwth);
        this.groupBox2.Controls.Add(this.txt_lhigh);
        this.groupBox2.Controls.Add(this.label9);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.lbl_lcolor);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Location = new System.Drawing.Point(12, 138);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(319, 128);
        this.groupBox2.TabIndex = 1;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "流体属性";
        this.groupBox3.Controls.Add(this.rd_veryfast);
        this.groupBox3.Controls.Add(this.rd_veryslow);
        this.groupBox3.Controls.Add(this.rd_fast);
        this.groupBox3.Controls.Add(this.rd_nml);
        this.groupBox3.Controls.Add(this.rd_slow);
        this.groupBox3.Location = new System.Drawing.Point(6, 74);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(307, 46);
        this.groupBox3.TabIndex = 12;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "流动速度";
        this.rd_veryfast.AutoSize = true;
        this.rd_veryfast.Location = new System.Drawing.Point(240, 21);
        this.rd_veryfast.Name = "rd_veryfast";
        this.rd_veryfast.Size = new System.Drawing.Size(59, 16);
        this.rd_veryfast.TabIndex = 15;
        this.rd_veryfast.TabStop = true;
        this.rd_veryfast.Text = "非常快";
        this.rd_veryfast.UseVisualStyleBackColor = true;
        this.rd_veryslow.AutoSize = true;
        this.rd_veryslow.Location = new System.Drawing.Point(12, 21);
        this.rd_veryslow.Name = "rd_veryslow";
        this.rd_veryslow.Size = new System.Drawing.Size(59, 16);
        this.rd_veryslow.TabIndex = 11;
        this.rd_veryslow.TabStop = true;
        this.rd_veryslow.Text = "非常慢";
        this.rd_veryslow.UseVisualStyleBackColor = true;
        this.rd_fast.AutoSize = true;
        this.rd_fast.Location = new System.Drawing.Point(183, 21);
        this.rd_fast.Name = "rd_fast";
        this.rd_fast.Size = new System.Drawing.Size(35, 16);
        this.rd_fast.TabIndex = 14;
        this.rd_fast.TabStop = true;
        this.rd_fast.Text = "快";
        this.rd_fast.UseVisualStyleBackColor = true;
        this.rd_nml.AutoSize = true;
        this.rd_nml.Location = new System.Drawing.Point(129, 21);
        this.rd_nml.Name = "rd_nml";
        this.rd_nml.Size = new System.Drawing.Size(35, 16);
        this.rd_nml.TabIndex = 13;
        this.rd_nml.TabStop = true;
        this.rd_nml.Text = "中";
        this.rd_nml.UseVisualStyleBackColor = true;
        this.rd_slow.AutoSize = true;
        this.rd_slow.Checked = true;
        this.rd_slow.Location = new System.Drawing.Point(74, 21);
        this.rd_slow.Name = "rd_slow";
        this.rd_slow.Size = new System.Drawing.Size(35, 16);
        this.rd_slow.TabIndex = 12;
        this.rd_slow.TabStop = true;
        this.rd_slow.Text = "慢";
        this.rd_slow.UseVisualStyleBackColor = true;
        this.txt_divwth.Location = new System.Drawing.Point(254, 47);
        this.txt_divwth.Name = "txt_divwth";
        this.txt_divwth.Size = new System.Drawing.Size(46, 21);
        this.txt_divwth.TabIndex = 10;
        this.txt_lwth.Location = new System.Drawing.Point(99, 45);
        this.txt_lwth.Name = "txt_lwth";
        this.txt_lwth.Size = new System.Drawing.Size(46, 21);
        this.txt_lwth.TabIndex = 9;
        this.txt_lhigh.Location = new System.Drawing.Point(254, 18);
        this.txt_lhigh.Name = "txt_lhigh";
        this.txt_lhigh.Size = new System.Drawing.Size(46, 21);
        this.txt_lhigh.TabIndex = 8;
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(171, 48);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(77, 12);
        this.label9.TabIndex = 4;
        this.label9.Text = "流动块间隔：";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(16, 48);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(77, 12);
        this.label8.TabIndex = 3;
        this.label8.Text = "流动块宽度：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(171, 21);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(77, 12);
        this.label7.TabIndex = 2;
        this.label7.Text = "流动块高度：";
        this.lbl_lcolor.AutoSize = true;
        this.lbl_lcolor.BackColor = System.Drawing.Color.Blue;
        this.lbl_lcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_lcolor.Location = new System.Drawing.Point(101, 21);
        this.lbl_lcolor.Name = "lbl_lcolor";
        this.lbl_lcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_lcolor.TabIndex = 7;
        this.lbl_lcolor.Text = "      ";
        this.lbl_lcolor.Click += new System.EventHandler(lbl_lcolor_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(16, 21);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(77, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "流动块颜色：";
        this.button1.Location = new System.Drawing.Point(158, 275);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 16;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Location = new System.Drawing.Point(238, 275);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 17;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(343, 309);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "PSet1";
        this.Text = "管道设置";
        base.Load += new System.EventHandler(PSet1_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
