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
        string value = viewevent();
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
            if (ckvarevent(txt_var.Text))
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
        groupBox7 = new System.Windows.Forms.GroupBox();
        txt_othercount = new System.Windows.Forms.TextBox();
        txt_maincount = new System.Windows.Forms.TextBox();
        label27 = new System.Windows.Forms.Label();
        label26 = new System.Windows.Forms.Label();
        txt_varjd2 = new System.Windows.Forms.TextBox();
        label25 = new System.Windows.Forms.Label();
        lbl_txtcolor = new System.Windows.Forms.Label();
        label16 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txt_minmark = new System.Windows.Forms.TextBox();
        txt_maxmark = new System.Windows.Forms.TextBox();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label4 = new System.Windows.Forms.Label();
        txt_bq = new System.Windows.Forms.TextBox();
        lbl_bgcolor = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        lbl_markcolor = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        txt_varjd1 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        btn_view = new System.Windows.Forms.Button();
        txt_var = new System.Windows.Forms.TextBox();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        btn_cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        groupBox7.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        groupBox7.Controls.Add(txt_othercount);
        groupBox7.Controls.Add(txt_maincount);
        groupBox7.Controls.Add(label27);
        groupBox7.Controls.Add(label26);
        groupBox7.Location = new System.Drawing.Point(217, 161);
        groupBox7.Name = "groupBox7";
        groupBox7.Size = new System.Drawing.Size(200, 82);
        groupBox7.TabIndex = 14;
        groupBox7.TabStop = false;
        groupBox7.Text = "刻度标志";
        txt_othercount.Location = new System.Drawing.Point(100, 47);
        txt_othercount.Name = "txt_othercount";
        txt_othercount.Size = new System.Drawing.Size(81, 21);
        txt_othercount.TabIndex = 9;
        txt_maincount.Location = new System.Drawing.Point(100, 17);
        txt_maincount.Name = "txt_maincount";
        txt_maincount.Size = new System.Drawing.Size(81, 21);
        txt_maincount.TabIndex = 8;
        label27.AutoSize = true;
        label27.Location = new System.Drawing.Point(22, 52);
        label27.Name = "label27";
        label27.Size = new System.Drawing.Size(65, 12);
        label27.TabIndex = 3;
        label27.Text = "副刻度数：";
        label26.AutoSize = true;
        label26.Location = new System.Drawing.Point(22, 26);
        label26.Name = "label26";
        label26.Size = new System.Drawing.Size(65, 12);
        label26.TabIndex = 2;
        label26.Text = "主刻度数：";
        txt_varjd2.Location = new System.Drawing.Point(267, 22);
        txt_varjd2.Name = "txt_varjd2";
        txt_varjd2.Size = new System.Drawing.Size(100, 21);
        txt_varjd2.TabIndex = 4;
        txt_varjd2.Visible = false;
        txt_varjd2.TextChanged += new System.EventHandler(txt_varjd2_TextChanged);
        label25.AutoSize = true;
        label25.Location = new System.Drawing.Point(196, 31);
        label25.Name = "label25";
        label25.Size = new System.Drawing.Size(65, 12);
        label25.TabIndex = 3;
        label25.Text = "小数位数：";
        label25.Visible = false;
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.SystemColors.ActiveCaption;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(267, 62);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(37, 14);
        lbl_txtcolor.TabIndex = 5;
        lbl_txtcolor.Text = "     ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        label16.AutoSize = true;
        label16.Location = new System.Drawing.Point(196, 62);
        label16.Name = "label16";
        label16.Size = new System.Drawing.Size(65, 12);
        label16.TabIndex = 1;
        label16.Text = "刻度颜色：";
        groupBox2.Controls.Add(txt_minmark);
        groupBox2.Controls.Add(txt_maxmark);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Location = new System.Drawing.Point(10, 161);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(200, 82);
        groupBox2.TabIndex = 12;
        groupBox2.TabStop = false;
        groupBox2.Text = "量程范围";
        txt_minmark.Location = new System.Drawing.Point(93, 47);
        txt_minmark.Name = "txt_minmark";
        txt_minmark.Size = new System.Drawing.Size(88, 21);
        txt_minmark.TabIndex = 7;
        txt_maxmark.Location = new System.Drawing.Point(93, 19);
        txt_maxmark.Name = "txt_maxmark";
        txt_maxmark.Size = new System.Drawing.Size(88, 21);
        txt_maxmark.TabIndex = 6;
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(22, 52);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(65, 12);
        label8.TabIndex = 1;
        label8.Text = "最小刻度：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(22, 26);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 0;
        label7.Text = "最大刻度：";
        groupBox1.Controls.Add(label16);
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(txt_varjd2);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(label25);
        groupBox1.Controls.Add(txt_bq);
        groupBox1.Controls.Add(lbl_bgcolor);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(10, 47);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(407, 100);
        groupBox1.TabIndex = 11;
        groupBox1.TabStop = false;
        groupBox1.Text = "仪表表盘";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(22, 62);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 5;
        label4.Text = "表盘颜色：";
        txt_bq.Location = new System.Drawing.Point(90, 22);
        txt_bq.Name = "txt_bq";
        txt_bq.Size = new System.Drawing.Size(100, 21);
        txt_bq.TabIndex = 2;
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.SystemColors.ActiveBorder;
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(90, 62);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(37, 14);
        lbl_bgcolor.TabIndex = 3;
        lbl_bgcolor.Text = "     ";
        lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(19, 31);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "标签文本：";
        lbl_markcolor.AutoSize = true;
        lbl_markcolor.BackColor = System.Drawing.SystemColors.ControlText;
        lbl_markcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_markcolor.Location = new System.Drawing.Point(86, 302);
        lbl_markcolor.Name = "lbl_markcolor";
        lbl_markcolor.Size = new System.Drawing.Size(103, 14);
        lbl_markcolor.TabIndex = 7;
        lbl_markcolor.Text = "                ";
        lbl_markcolor.Visible = false;
        lbl_markcolor.Click += new System.EventHandler(lbl_markcolor_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(15, 302);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 6;
        label5.Text = "标签颜色：";
        label5.Visible = false;
        txt_varjd1.Location = new System.Drawing.Point(84, 270);
        txt_varjd1.Name = "txt_varjd1";
        txt_varjd1.Size = new System.Drawing.Size(103, 21);
        txt_varjd1.TabIndex = 4;
        txt_varjd1.Visible = false;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(15, 273);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 3;
        label3.Text = "小数位数：";
        label3.Visible = false;
        btn_view.Location = new System.Drawing.Point(319, 19);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_var.Location = new System.Drawing.Point(17, 21);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(285, 21);
        txt_var.TabIndex = 0;
        btn_cancel.Location = new System.Drawing.Point(340, 283);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 11;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        btn_OK.Location = new System.Drawing.Point(248, 283);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 10;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(434, 321);
        base.Controls.Add(lbl_markcolor);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(label5);
        base.Controls.Add(btn_OK);
        base.Controls.Add(groupBox7);
        base.Controls.Add(txt_varjd1);
        base.Controls.Add(label3);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_view);
        base.Controls.Add(txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.Name = "BSet1";
        Text = "仪表";
        base.Load += new System.EventHandler(BSet1_Load);
        groupBox7.ResumeLayout(false);
        groupBox7.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
