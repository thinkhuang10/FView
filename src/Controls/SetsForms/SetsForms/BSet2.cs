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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        lbl_txtcolor = new System.Windows.Forms.Label();
        txt_var = new System.Windows.Forms.TextBox();
        lbl_bgcolor = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txt_minmark = new System.Windows.Forms.TextBox();
        txt_maxmark = new System.Windows.Forms.TextBox();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        btn_view = new System.Windows.Forms.Button();
        txt_maincount = new System.Windows.Forms.TextBox();
        btn_cancel = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        lbl_ValueColor = new System.Windows.Forms.Label();
        txt_varjd2 = new System.Windows.Forms.TextBox();
        label25 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(218, 18);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(43, 14);
        lbl_txtcolor.TabIndex = 4;
        lbl_txtcolor.Text = "      ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click_1);
        txt_var.Cursor = System.Windows.Forms.Cursors.IBeam;
        txt_var.Location = new System.Drawing.Point(21, 12);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(218, 68);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(43, 14);
        lbl_bgcolor.TabIndex = 6;
        lbl_bgcolor.Text = "      ";
        lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click_1);
        btn_OK.Location = new System.Drawing.Point(130, 236);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 9;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click_1);
        groupBox2.Controls.Add(txt_minmark);
        groupBox2.Controls.Add(txt_maxmark);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Location = new System.Drawing.Point(21, 160);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(277, 58);
        groupBox2.TabIndex = 22;
        groupBox2.TabStop = false;
        groupBox2.Text = "量程范围";
        txt_minmark.Location = new System.Drawing.Point(198, 24);
        txt_minmark.Name = "txt_minmark";
        txt_minmark.Size = new System.Drawing.Size(56, 21);
        txt_minmark.TabIndex = 8;
        txt_maxmark.Location = new System.Drawing.Point(71, 24);
        txt_maxmark.Name = "txt_maxmark";
        txt_maxmark.Size = new System.Drawing.Size(58, 21);
        txt_maxmark.TabIndex = 7;
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(139, 27);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(53, 12);
        label8.TabIndex = 1;
        label8.Text = "最小值：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(15, 27);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 0;
        label7.Text = "最大值：";
        btn_view.Location = new System.Drawing.Point(212, 12);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click_1);
        txt_maincount.Location = new System.Drawing.Point(82, 27);
        txt_maincount.Name = "txt_maincount";
        txt_maincount.Size = new System.Drawing.Size(45, 21);
        txt_maincount.TabIndex = 2;
        btn_cancel.Location = new System.Drawing.Point(223, 236);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 10;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click_1);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(139, 20);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(77, 12);
        label1.TabIndex = 25;
        label1.Text = "标签文本色：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(139, 70);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(77, 12);
        label2.TabIndex = 26;
        label2.Text = "表盘填充色：";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(139, 45);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 27;
        label3.Text = "数据颜色：";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(13, 31);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 28;
        label4.Text = "主刻度数：";
        lbl_ValueColor.AutoSize = true;
        lbl_ValueColor.BackColor = System.Drawing.Color.FromArgb(192, 0, 192);
        lbl_ValueColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_ValueColor.Location = new System.Drawing.Point(218, 43);
        lbl_ValueColor.Name = "lbl_ValueColor";
        lbl_ValueColor.Size = new System.Drawing.Size(43, 14);
        lbl_ValueColor.TabIndex = 5;
        lbl_ValueColor.Text = "      ";
        lbl_ValueColor.Click += new System.EventHandler(lbl_ValueColor_Click);
        txt_varjd2.Location = new System.Drawing.Point(82, 56);
        txt_varjd2.Name = "txt_varjd2";
        txt_varjd2.Size = new System.Drawing.Size(45, 21);
        txt_varjd2.TabIndex = 3;
        label25.AutoSize = true;
        label25.Location = new System.Drawing.Point(13, 59);
        label25.Name = "label25";
        label25.Size = new System.Drawing.Size(65, 12);
        label25.TabIndex = 30;
        label25.Text = "小数位数：";
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txt_varjd2);
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(label25);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_ValueColor);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(lbl_bgcolor);
        groupBox1.Controls.Add(txt_maincount);
        groupBox1.Location = new System.Drawing.Point(21, 41);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(277, 103);
        groupBox1.TabIndex = 32;
        groupBox1.TabStop = false;
        groupBox1.Text = "仪表表盘";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(310, 273);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_OK);
        base.Controls.Add(groupBox2);
        base.Controls.Add(btn_view);
        base.Controls.Add(btn_cancel);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet2";
        Text = "仪表";
        base.Load += new System.EventHandler(BSet2_Load);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
