using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BSet1 : Form
{
    public int pt2 = 2;

    private string errorstr;

    public Color BqColor = Color.Black;

    public float MinValue;

    public float MaxValue = 100f;

    public string VarName = "";

    public int MainMarkCount = 5;

    public int OtherMarkCount = 1;

    public Color TxtColor = Color.Black;

    public Color Bgcolor = Color.LightGray;

    public Color MarkColor = Color.Black;

    public string Mark = "仪表";

    private int closeflag = 1;

    private GroupBox groupBox7;

    private TextBox txt_othercount;

    private TextBox txt_maincount;

    private Label label27;

    private Label label26;

    private TextBox txt_varjd2;

    private Label label25;

    private Label lbl_txtcolor;

    private Label label16;

    private GroupBox groupBox2;

    private TextBox txt_minmark;

    private TextBox txt_maxmark;

    private Label label8;

    private Label label7;

    private GroupBox groupBox1;

    private Label lbl_markcolor;

    private Label label5;

    private Label label4;

    private TextBox txt_varjd1;

    private Label label3;

    private TextBox txt_bq;

    private Label lbl_bgcolor;

    private Label label1;

    private Button btn_view;

    private TextBox txt_var;

    private ColorDialog colorDialog1;

    private Button btn_cancel;

    private Button btn_OK;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BSet1()
    {
        InitializeComponent();
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_markcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_markcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                pt2 = Convert.ToInt32(txt_varjd2.Text);
                MaxValue = Convert.ToSingle(txt_maxmark.Text);
                MinValue = Convert.ToSingle(txt_minmark.Text);
                MainMarkCount = Convert.ToInt32(txt_maincount.Text);
                OtherMarkCount = Convert.ToInt32(txt_othercount.Text);
                VarName = "[" + txt_var.Text + "]";
                Mark = txt_bq.Text;
                Bgcolor = lbl_bgcolor.BackColor;
                BqColor = lbl_markcolor.BackColor;
                TxtColor = lbl_txtcolor.BackColor;
                MarkColor = lbl_markcolor.BackColor;
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
            if (MainMarkCount < 0 || OtherMarkCount < 0)
            {
                errorstr = "刻度数不能小于零！";
            }
        }
        catch
        {
            MessageBox.Show("发生异常！");
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BSet1_Load(object sender, EventArgs e)
    {
        txt_varjd2.Text = pt2.ToString();
        txt_maxmark.Text = MaxValue.ToString();
        txt_minmark.Text = MinValue.ToString();
        txt_maincount.Text = MainMarkCount.ToString();
        txt_othercount.Text = OtherMarkCount.ToString();
        if (!string.IsNullOrEmpty(VarName))
        {
            txt_var.Text = VarName.Substring(1, VarName.Length - 2);
        }
        lbl_bgcolor.BackColor = Bgcolor;
        lbl_markcolor.BackColor = BqColor;
        lbl_txtcolor.BackColor = TxtColor;
        txt_bq.Text = Mark;
    }

    private void txt_varjd2_TextChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.groupBox7 = new System.Windows.Forms.GroupBox();
        this.txt_othercount = new System.Windows.Forms.TextBox();
        this.txt_maincount = new System.Windows.Forms.TextBox();
        this.label27 = new System.Windows.Forms.Label();
        this.label26 = new System.Windows.Forms.Label();
        this.txt_varjd2 = new System.Windows.Forms.TextBox();
        this.label25 = new System.Windows.Forms.Label();
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.txt_minmark = new System.Windows.Forms.TextBox();
        this.txt_maxmark = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label4 = new System.Windows.Forms.Label();
        this.txt_bq = new System.Windows.Forms.TextBox();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_markcolor = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_varjd1 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.groupBox7.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.groupBox7.Controls.Add(this.txt_othercount);
        this.groupBox7.Controls.Add(this.txt_maincount);
        this.groupBox7.Controls.Add(this.label27);
        this.groupBox7.Controls.Add(this.label26);
        this.groupBox7.Location = new System.Drawing.Point(217, 161);
        this.groupBox7.Name = "groupBox7";
        this.groupBox7.Size = new System.Drawing.Size(200, 82);
        this.groupBox7.TabIndex = 14;
        this.groupBox7.TabStop = false;
        this.groupBox7.Text = "刻度标志";
        this.txt_othercount.Location = new System.Drawing.Point(100, 47);
        this.txt_othercount.Name = "txt_othercount";
        this.txt_othercount.Size = new System.Drawing.Size(81, 21);
        this.txt_othercount.TabIndex = 9;
        this.txt_maincount.Location = new System.Drawing.Point(100, 17);
        this.txt_maincount.Name = "txt_maincount";
        this.txt_maincount.Size = new System.Drawing.Size(81, 21);
        this.txt_maincount.TabIndex = 8;
        this.label27.AutoSize = true;
        this.label27.Location = new System.Drawing.Point(22, 52);
        this.label27.Name = "label27";
        this.label27.Size = new System.Drawing.Size(65, 12);
        this.label27.TabIndex = 3;
        this.label27.Text = "副刻度数：";
        this.label26.AutoSize = true;
        this.label26.Location = new System.Drawing.Point(22, 26);
        this.label26.Name = "label26";
        this.label26.Size = new System.Drawing.Size(65, 12);
        this.label26.TabIndex = 2;
        this.label26.Text = "主刻度数：";
        this.txt_varjd2.Location = new System.Drawing.Point(267, 22);
        this.txt_varjd2.Name = "txt_varjd2";
        this.txt_varjd2.Size = new System.Drawing.Size(100, 21);
        this.txt_varjd2.TabIndex = 4;
        this.txt_varjd2.Visible = false;
        this.txt_varjd2.TextChanged += new System.EventHandler(txt_varjd2_TextChanged);
        this.label25.AutoSize = true;
        this.label25.Location = new System.Drawing.Point(196, 31);
        this.label25.Name = "label25";
        this.label25.Size = new System.Drawing.Size(65, 12);
        this.label25.TabIndex = 3;
        this.label25.Text = "小数位数：";
        this.label25.Visible = false;
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(267, 62);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_txtcolor.TabIndex = 5;
        this.lbl_txtcolor.Text = "     ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(196, 62);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(65, 12);
        this.label16.TabIndex = 1;
        this.label16.Text = "刻度颜色：";
        this.groupBox2.Controls.Add(this.txt_minmark);
        this.groupBox2.Controls.Add(this.txt_maxmark);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Location = new System.Drawing.Point(10, 161);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(200, 82);
        this.groupBox2.TabIndex = 12;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "量程范围";
        this.txt_minmark.Location = new System.Drawing.Point(93, 47);
        this.txt_minmark.Name = "txt_minmark";
        this.txt_minmark.Size = new System.Drawing.Size(88, 21);
        this.txt_minmark.TabIndex = 7;
        this.txt_maxmark.Location = new System.Drawing.Point(93, 19);
        this.txt_maxmark.Name = "txt_maxmark";
        this.txt_maxmark.Size = new System.Drawing.Size(88, 21);
        this.txt_maxmark.TabIndex = 6;
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(22, 52);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(65, 12);
        this.label8.TabIndex = 1;
        this.label8.Text = "最小刻度：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(22, 26);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(65, 12);
        this.label7.TabIndex = 0;
        this.label7.Text = "最大刻度：";
        this.groupBox1.Controls.Add(this.label16);
        this.groupBox1.Controls.Add(this.lbl_txtcolor);
        this.groupBox1.Controls.Add(this.txt_varjd2);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label25);
        this.groupBox1.Controls.Add(this.txt_bq);
        this.groupBox1.Controls.Add(this.lbl_bgcolor);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(10, 47);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(407, 100);
        this.groupBox1.TabIndex = 11;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "仪表表盘";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(22, 62);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 5;
        this.label4.Text = "表盘颜色：";
        this.txt_bq.Location = new System.Drawing.Point(90, 22);
        this.txt_bq.Name = "txt_bq";
        this.txt_bq.Size = new System.Drawing.Size(100, 21);
        this.txt_bq.TabIndex = 2;
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(90, 62);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_bgcolor.TabIndex = 3;
        this.lbl_bgcolor.Text = "     ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(19, 31);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "标签文本：";
        this.lbl_markcolor.AutoSize = true;
        this.lbl_markcolor.BackColor = System.Drawing.SystemColors.ControlText;
        this.lbl_markcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_markcolor.Location = new System.Drawing.Point(86, 302);
        this.lbl_markcolor.Name = "lbl_markcolor";
        this.lbl_markcolor.Size = new System.Drawing.Size(103, 14);
        this.lbl_markcolor.TabIndex = 7;
        this.lbl_markcolor.Text = "                ";
        this.lbl_markcolor.Visible = false;
        this.lbl_markcolor.Click += new System.EventHandler(lbl_markcolor_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(15, 302);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 6;
        this.label5.Text = "标签颜色：";
        this.label5.Visible = false;
        this.txt_varjd1.Location = new System.Drawing.Point(84, 270);
        this.txt_varjd1.Name = "txt_varjd1";
        this.txt_varjd1.Size = new System.Drawing.Size(103, 21);
        this.txt_varjd1.TabIndex = 4;
        this.txt_varjd1.Visible = false;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(15, 273);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 3;
        this.label3.Text = "小数位数：";
        this.label3.Visible = false;
        this.btn_view.Location = new System.Drawing.Point(319, 19);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_var.Location = new System.Drawing.Point(17, 21);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(285, 21);
        this.txt_var.TabIndex = 0;
        this.btn_cancel.Location = new System.Drawing.Point(340, 283);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 11;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(248, 283);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 10;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(434, 321);
        base.Controls.Add(this.lbl_markcolor);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.label5);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.groupBox7);
        base.Controls.Add(this.txt_varjd1);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_view);
        base.Controls.Add(this.txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.Name = "BSet1";
        this.Text = "仪表";
        base.Load += new System.EventHandler(BSet1_Load);
        this.groupBox7.ResumeLayout(false);
        this.groupBox7.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
