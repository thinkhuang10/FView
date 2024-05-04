using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BSet2 : Form
{
    public int pt2 = 2;

    private string errorstr;

    public Color BqColor = Color.Blue;

    public float MinValue;

    public float MaxValue = 100f;

    public string VarName = "";

    public int MainMarkCount = 5;

    public Color TxtColor = Color.Blue;

    public Color Bgcolor = Color.White;

    public Color ValueColor = Color.Blue;

    public string Mark = "仪表";

    private int closeflag = 1;

    private Label lbl_txtcolor;

    private TextBox txt_var;

    private Label lbl_bgcolor;

    private Button btn_OK;

    private GroupBox groupBox2;

    private TextBox txt_minmark;

    private TextBox txt_maxmark;

    private Label label8;

    private Label label7;

    private Button btn_view;

    private TextBox txt_maincount;

    private Button btn_cancel;

    private Label label1;

    private Label label2;

    private Label label3;

    private Label label4;

    private ColorDialog colorDialog1;

    private Label lbl_ValueColor;

    private TextBox txt_varjd2;

    private Label label25;

    private GroupBox groupBox1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BSet2()
    {
        InitializeComponent();
    }

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BSet1_Load(object sender, EventArgs e)
    {
        txt_maxmark.Text = MaxValue.ToString();
        txt_minmark.Text = MinValue.ToString();
        txt_maincount.Text = MainMarkCount.ToString();
        txt_var.Text = VarName;
        if (VarName != "")
        {
            lbl_bgcolor.BackColor = Bgcolor;
            lbl_txtcolor.BackColor = TxtColor;
        }
    }

    private void btn_OK_Click_1(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                pt2 = Convert.ToInt32(txt_varjd2.Text);
                MaxValue = Convert.ToSingle(txt_maxmark.Text);
                MinValue = Convert.ToSingle(txt_minmark.Text);
                MainMarkCount = Convert.ToInt32(txt_maincount.Text);
                VarName = "[" + txt_var.Text + "]";
                Bgcolor = lbl_bgcolor.BackColor;
                TxtColor = lbl_txtcolor.BackColor;
                ValueColor = lbl_ValueColor.BackColor;
                checkinput();
                if (!string.IsNullOrEmpty(errorstr))
                {
                    MessageBox.Show(errorstr);
                    errorstr = "";
                    closeflag = 0;
                }
                else
                {
                    closeflag = 1;
                }
                if (closeflag == 1)
                {
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常，可能是您填写的文本有问题！");
        }
    }

    private void checkinput()
    {
        try
        {
            if (Convert.ToInt32(txt_varjd2.Text) > 10 || Convert.ToInt32(txt_varjd2.Text) < 0)
            {
                errorstr = "小数位数不能大于10或小于0";
            }
            if (txt_var.Text == "")
            {
                errorstr = "变量名不能为空！";
            }
            if (MaxValue < MinValue)
            {
                errorstr = "最大值不能小于最小值！";
            }
            if (MainMarkCount < 0)
            {
                errorstr = "刻度数不能小于零！";
            }
        }
        catch
        {
            MessageBox.Show("发生异常！");
        }
    }

    private void lbl_txtcolor_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_ValueColor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_ValueColor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_view_Click_1(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void btn_cancel_Click_1(object sender, EventArgs e)
    {
        Close();
    }

    private void BSet2_Load(object sender, EventArgs e)
    {
        txt_varjd2.Text = pt2.ToString();
        lbl_txtcolor.BackColor = TxtColor;
        txt_minmark.Text = MinValue.ToString();
        txt_maxmark.Text = MaxValue.ToString();
        if (!string.IsNullOrEmpty(VarName))
        {
            txt_var.Text = VarName.Substring(1, VarName.Length - 2);
        }
        txt_maincount.Text = MainMarkCount.ToString();
        lbl_ValueColor.BackColor = ValueColor;
        lbl_bgcolor.BackColor = Bgcolor;
    }

    private void InitializeComponent()
    {
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.txt_minmark = new System.Windows.Forms.TextBox();
        this.txt_maxmark = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_maincount = new System.Windows.Forms.TextBox();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.lbl_ValueColor = new System.Windows.Forms.Label();
        this.txt_varjd2 = new System.Windows.Forms.TextBox();
        this.label25 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(218, 18);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_txtcolor.TabIndex = 4;
        this.lbl_txtcolor.Text = "      ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click_1);
        this.txt_var.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txt_var.Location = new System.Drawing.Point(21, 12);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(218, 68);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(43, 14);
        this.lbl_bgcolor.TabIndex = 6;
        this.lbl_bgcolor.Text = "      ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click_1);
        this.btn_OK.Location = new System.Drawing.Point(130, 236);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 9;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click_1);
        this.groupBox2.Controls.Add(this.txt_minmark);
        this.groupBox2.Controls.Add(this.txt_maxmark);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Location = new System.Drawing.Point(21, 160);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(277, 58);
        this.groupBox2.TabIndex = 22;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "量程范围";
        this.txt_minmark.Location = new System.Drawing.Point(198, 24);
        this.txt_minmark.Name = "txt_minmark";
        this.txt_minmark.Size = new System.Drawing.Size(56, 21);
        this.txt_minmark.TabIndex = 8;
        this.txt_maxmark.Location = new System.Drawing.Point(71, 24);
        this.txt_maxmark.Name = "txt_maxmark";
        this.txt_maxmark.Size = new System.Drawing.Size(58, 21);
        this.txt_maxmark.TabIndex = 7;
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(139, 27);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(53, 12);
        this.label8.TabIndex = 1;
        this.label8.Text = "最小值：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(15, 27);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 0;
        this.label7.Text = "最大值：";
        this.btn_view.Location = new System.Drawing.Point(212, 12);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click_1);
        this.txt_maincount.Location = new System.Drawing.Point(82, 27);
        this.txt_maincount.Name = "txt_maincount";
        this.txt_maincount.Size = new System.Drawing.Size(45, 21);
        this.txt_maincount.TabIndex = 2;
        this.btn_cancel.Location = new System.Drawing.Point(223, 236);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 10;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click_1);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(139, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(77, 12);
        this.label1.TabIndex = 25;
        this.label1.Text = "标签文本色：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(139, 70);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(77, 12);
        this.label2.TabIndex = 26;
        this.label2.Text = "表盘填充色：";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(139, 45);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 27;
        this.label3.Text = "数据颜色：";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(13, 31);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 28;
        this.label4.Text = "主刻度数：";
        this.lbl_ValueColor.AutoSize = true;
        this.lbl_ValueColor.BackColor = System.Drawing.Color.FromArgb(192, 0, 192);
        this.lbl_ValueColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_ValueColor.Location = new System.Drawing.Point(218, 43);
        this.lbl_ValueColor.Name = "lbl_ValueColor";
        this.lbl_ValueColor.Size = new System.Drawing.Size(43, 14);
        this.lbl_ValueColor.TabIndex = 5;
        this.lbl_ValueColor.Text = "      ";
        this.lbl_ValueColor.Click += new System.EventHandler(lbl_ValueColor_Click);
        this.txt_varjd2.Location = new System.Drawing.Point(82, 56);
        this.txt_varjd2.Name = "txt_varjd2";
        this.txt_varjd2.Size = new System.Drawing.Size(45, 21);
        this.txt_varjd2.TabIndex = 3;
        this.label25.AutoSize = true;
        this.label25.Location = new System.Drawing.Point(13, 59);
        this.label25.Name = "label25";
        this.label25.Size = new System.Drawing.Size(65, 12);
        this.label25.TabIndex = 30;
        this.label25.Text = "小数位数：";
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txt_varjd2);
        this.groupBox1.Controls.Add(this.lbl_txtcolor);
        this.groupBox1.Controls.Add(this.label25);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.lbl_ValueColor);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.lbl_bgcolor);
        this.groupBox1.Controls.Add(this.txt_maincount);
        this.groupBox1.Location = new System.Drawing.Point(21, 41);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(277, 103);
        this.groupBox1.TabIndex = 32;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "仪表表盘";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(310, 273);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.btn_view);
        base.Controls.Add(this.btn_cancel);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet2";
        this.Text = "仪表";
        base.Load += new System.EventHandler(BSet2_Load);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
