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
            if (ckvarevent(txt_var1.Text) && ckvarevent(txt_var2.Text))
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
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void btn_var2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
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
        groupBox1 = new System.Windows.Forms.GroupBox();
        rd_chuizhi = new System.Windows.Forms.RadioButton();
        rd_shuiping = new System.Windows.Forms.RadioButton();
        lbl_pipecolor = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        btn_var2 = new System.Windows.Forms.Button();
        btn_var1 = new System.Windows.Forms.Button();
        txt_var2 = new System.Windows.Forms.TextBox();
        txt_var1 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        groupBox3 = new System.Windows.Forms.GroupBox();
        rd_veryfast = new System.Windows.Forms.RadioButton();
        rd_veryslow = new System.Windows.Forms.RadioButton();
        rd_fast = new System.Windows.Forms.RadioButton();
        rd_nml = new System.Windows.Forms.RadioButton();
        rd_slow = new System.Windows.Forms.RadioButton();
        txt_divwth = new System.Windows.Forms.TextBox();
        txt_lwth = new System.Windows.Forms.TextBox();
        txt_lhigh = new System.Windows.Forms.TextBox();
        label9 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        lbl_lcolor = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(rd_chuizhi);
        groupBox1.Controls.Add(rd_shuiping);
        groupBox1.Controls.Add(lbl_pipecolor);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(btn_var2);
        groupBox1.Controls.Add(btn_var1);
        groupBox1.Controls.Add(txt_var2);
        groupBox1.Controls.Add(txt_var1);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(12, 13);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(319, 119);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "管道属性";
        rd_chuizhi.AutoSize = true;
        rd_chuizhi.Location = new System.Drawing.Point(239, 86);
        rd_chuizhi.Name = "rd_chuizhi";
        rd_chuizhi.Size = new System.Drawing.Size(47, 16);
        rd_chuizhi.TabIndex = 6;
        rd_chuizhi.TabStop = true;
        rd_chuizhi.Text = "垂直";
        rd_chuizhi.UseVisualStyleBackColor = true;
        rd_chuizhi.Visible = false;
        rd_shuiping.AutoSize = true;
        rd_shuiping.Checked = true;
        rd_shuiping.Location = new System.Drawing.Point(173, 86);
        rd_shuiping.Name = "rd_shuiping";
        rd_shuiping.Size = new System.Drawing.Size(47, 16);
        rd_shuiping.TabIndex = 5;
        rd_shuiping.TabStop = true;
        rd_shuiping.Text = "水平";
        rd_shuiping.UseVisualStyleBackColor = true;
        rd_shuiping.Visible = false;
        lbl_pipecolor.AutoSize = true;
        lbl_pipecolor.BackColor = System.Drawing.Color.Silver;
        lbl_pipecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_pipecolor.Location = new System.Drawing.Point(87, 86);
        lbl_pipecolor.Name = "lbl_pipecolor";
        lbl_pipecolor.Size = new System.Drawing.Size(43, 14);
        lbl_pipecolor.TabIndex = 4;
        lbl_pipecolor.Text = "      ";
        lbl_pipecolor.Click += new System.EventHandler(lbl_pipecolor_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(16, 86);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 6;
        label3.Text = "管道颜色：";
        btn_var2.Location = new System.Drawing.Point(225, 54);
        btn_var2.Name = "btn_var2";
        btn_var2.Size = new System.Drawing.Size(75, 23);
        btn_var2.TabIndex = 3;
        btn_var2.Text = "变量选择";
        btn_var2.UseVisualStyleBackColor = true;
        btn_var2.Click += new System.EventHandler(btn_var2_Click);
        btn_var1.Location = new System.Drawing.Point(225, 24);
        btn_var1.Name = "btn_var1";
        btn_var1.Size = new System.Drawing.Size(75, 23);
        btn_var1.TabIndex = 1;
        btn_var1.Text = "变量选择";
        btn_var1.UseVisualStyleBackColor = true;
        btn_var1.Click += new System.EventHandler(btn_var1_Click);
        txt_var2.Location = new System.Drawing.Point(87, 53);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(118, 21);
        txt_var2.TabIndex = 2;
        txt_var1.Location = new System.Drawing.Point(87, 26);
        txt_var1.Name = "txt_var1";
        txt_var1.Size = new System.Drawing.Size(119, 21);
        txt_var1.TabIndex = 0;
        txt_var1.TextChanged += new System.EventHandler(txt_var1_TextChanged);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(16, 56);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 1;
        label2.Text = "流动使能：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(16, 30);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "流动方向：";
        groupBox2.Controls.Add(groupBox3);
        groupBox2.Controls.Add(txt_divwth);
        groupBox2.Controls.Add(txt_lwth);
        groupBox2.Controls.Add(txt_lhigh);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(lbl_lcolor);
        groupBox2.Controls.Add(label5);
        groupBox2.Location = new System.Drawing.Point(12, 138);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(319, 128);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "流体属性";
        groupBox3.Controls.Add(rd_veryfast);
        groupBox3.Controls.Add(rd_veryslow);
        groupBox3.Controls.Add(rd_fast);
        groupBox3.Controls.Add(rd_nml);
        groupBox3.Controls.Add(rd_slow);
        groupBox3.Location = new System.Drawing.Point(6, 74);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(307, 46);
        groupBox3.TabIndex = 12;
        groupBox3.TabStop = false;
        groupBox3.Text = "流动速度";
        rd_veryfast.AutoSize = true;
        rd_veryfast.Location = new System.Drawing.Point(240, 21);
        rd_veryfast.Name = "rd_veryfast";
        rd_veryfast.Size = new System.Drawing.Size(59, 16);
        rd_veryfast.TabIndex = 15;
        rd_veryfast.TabStop = true;
        rd_veryfast.Text = "非常快";
        rd_veryfast.UseVisualStyleBackColor = true;
        rd_veryslow.AutoSize = true;
        rd_veryslow.Location = new System.Drawing.Point(12, 21);
        rd_veryslow.Name = "rd_veryslow";
        rd_veryslow.Size = new System.Drawing.Size(59, 16);
        rd_veryslow.TabIndex = 11;
        rd_veryslow.TabStop = true;
        rd_veryslow.Text = "非常慢";
        rd_veryslow.UseVisualStyleBackColor = true;
        rd_fast.AutoSize = true;
        rd_fast.Location = new System.Drawing.Point(183, 21);
        rd_fast.Name = "rd_fast";
        rd_fast.Size = new System.Drawing.Size(35, 16);
        rd_fast.TabIndex = 14;
        rd_fast.TabStop = true;
        rd_fast.Text = "快";
        rd_fast.UseVisualStyleBackColor = true;
        rd_nml.AutoSize = true;
        rd_nml.Location = new System.Drawing.Point(129, 21);
        rd_nml.Name = "rd_nml";
        rd_nml.Size = new System.Drawing.Size(35, 16);
        rd_nml.TabIndex = 13;
        rd_nml.TabStop = true;
        rd_nml.Text = "中";
        rd_nml.UseVisualStyleBackColor = true;
        rd_slow.AutoSize = true;
        rd_slow.Checked = true;
        rd_slow.Location = new System.Drawing.Point(74, 21);
        rd_slow.Name = "rd_slow";
        rd_slow.Size = new System.Drawing.Size(35, 16);
        rd_slow.TabIndex = 12;
        rd_slow.TabStop = true;
        rd_slow.Text = "慢";
        rd_slow.UseVisualStyleBackColor = true;
        txt_divwth.Location = new System.Drawing.Point(254, 47);
        txt_divwth.Name = "txt_divwth";
        txt_divwth.Size = new System.Drawing.Size(46, 21);
        txt_divwth.TabIndex = 10;
        txt_lwth.Location = new System.Drawing.Point(99, 45);
        txt_lwth.Name = "txt_lwth";
        txt_lwth.Size = new System.Drawing.Size(46, 21);
        txt_lwth.TabIndex = 9;
        txt_lhigh.Location = new System.Drawing.Point(254, 18);
        txt_lhigh.Name = "txt_lhigh";
        txt_lhigh.Size = new System.Drawing.Size(46, 21);
        txt_lhigh.TabIndex = 8;
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(171, 48);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(77, 12);
        label9.TabIndex = 4;
        label9.Text = "流动块间隔：";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(16, 48);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(77, 12);
        label8.TabIndex = 3;
        label8.Text = "流动块宽度：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(171, 21);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(77, 12);
        label7.TabIndex = 2;
        label7.Text = "流动块高度：";
        lbl_lcolor.AutoSize = true;
        lbl_lcolor.BackColor = System.Drawing.Color.Blue;
        lbl_lcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_lcolor.Location = new System.Drawing.Point(101, 21);
        lbl_lcolor.Name = "lbl_lcolor";
        lbl_lcolor.Size = new System.Drawing.Size(43, 14);
        lbl_lcolor.TabIndex = 7;
        lbl_lcolor.Text = "      ";
        lbl_lcolor.Click += new System.EventHandler(lbl_lcolor_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(16, 21);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(77, 12);
        label5.TabIndex = 0;
        label5.Text = "流动块颜色：";
        button1.Location = new System.Drawing.Point(158, 275);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 16;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(238, 275);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 17;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(343, 309);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "PSet1";
        Text = "管道设置";
        base.Load += new System.EventHandler(PSet1_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
