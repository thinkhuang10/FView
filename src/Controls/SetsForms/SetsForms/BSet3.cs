using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BSet3 : Form
{
    public float MinValue;

    public float MaxValue = 100f;

    public string VarName = "";

    public int MainMarkCount = 5;

    public Color bqcolor = Color.Blue;

    public Color fillcolor = Color.Black;

    public string Mark = "仪表";

    private int closeflag = 1;

    private string errorstr;

    private Label label4;

    private Label label2;

    private TextBox txt_var;

    private Label lbl_fillcolor;

    private GroupBox groupBox2;

    private TextBox txt_minmark;

    private TextBox txt_maxmark;

    private Label label8;

    private Label label7;

    private Button btn_view;

    private TextBox txt_maincount;

    private Label lbl_bqcolor;

    private Label label5;

    private TextBox txt_bq;

    private Label label1;

    private GroupBox 标签设置;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BSet3()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                VarName = "[" + txt_var.Text + "]";
                MaxValue = Convert.ToSingle(txt_maxmark.Text);
                MinValue = Convert.ToSingle(txt_minmark.Text);
                MainMarkCount = Convert.ToInt32(txt_maincount.Text);
                bqcolor = lbl_bqcolor.BackColor;
                fillcolor = lbl_fillcolor.BackColor;
                Mark = txt_bq.Text;
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
        }
    }

    private void checkinput()
    {
        try
        {
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

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bqcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bqcolor.BackColor = colorDialog1.Color;
        }
    }

    private void BSet3_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(VarName))
        {
            txt_var.Text = VarName.Substring(1, VarName.Length - 2);
        }
        txt_maxmark.Text = MaxValue.ToString();
        txt_minmark.Text = MinValue.ToString();
        txt_maincount.Text = MainMarkCount.ToString();
        lbl_fillcolor.BackColor = fillcolor;
        lbl_bqcolor.BackColor = bqcolor;
        txt_bq.Text = Mark;
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        this.label4 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.lbl_fillcolor = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.txt_minmark = new System.Windows.Forms.TextBox();
        this.txt_maxmark = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_maincount = new System.Windows.Forms.TextBox();
        this.lbl_bqcolor = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_bq = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.标签设置 = new System.Windows.Forms.GroupBox();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox2.SuspendLayout();
        this.标签设置.SuspendLayout();
        base.SuspendLayout();
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(6, 57);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 35;
        this.label4.Text = "主刻度数：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(139, 57);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 34;
        this.label2.Text = "填充色：";
        this.txt_var.Location = new System.Drawing.Point(21, 17);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.lbl_fillcolor.AutoSize = true;
        this.lbl_fillcolor.BackColor = System.Drawing.Color.Black;
        this.lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor.Location = new System.Drawing.Point(198, 57);
        this.lbl_fillcolor.Name = "lbl_fillcolor";
        this.lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_fillcolor.TabIndex = 5;
        this.lbl_fillcolor.Text = "       ";
        this.lbl_fillcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.groupBox2.Controls.Add(this.txt_minmark);
        this.groupBox2.Controls.Add(this.txt_maxmark);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Location = new System.Drawing.Point(21, 149);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(266, 58);
        this.groupBox2.TabIndex = 33;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "量程范围";
        this.txt_minmark.Location = new System.Drawing.Point(191, 20);
        this.txt_minmark.Name = "txt_minmark";
        this.txt_minmark.Size = new System.Drawing.Size(56, 21);
        this.txt_minmark.TabIndex = 7;
        this.txt_maxmark.Location = new System.Drawing.Point(63, 20);
        this.txt_maxmark.Name = "txt_maxmark";
        this.txt_maxmark.Size = new System.Drawing.Size(58, 21);
        this.txt_maxmark.TabIndex = 6;
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(139, 25);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(53, 12);
        this.label8.TabIndex = 1;
        this.label8.Text = "最小值：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(15, 25);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 0;
        this.label7.Text = "最大值：";
        this.btn_view.Location = new System.Drawing.Point(212, 17);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_maincount.Location = new System.Drawing.Point(75, 54);
        this.txt_maincount.Name = "txt_maincount";
        this.txt_maincount.Size = new System.Drawing.Size(45, 21);
        this.txt_maincount.TabIndex = 4;
        this.lbl_bqcolor.AutoSize = true;
        this.lbl_bqcolor.BackColor = System.Drawing.SystemColors.ControlText;
        this.lbl_bqcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bqcolor.Location = new System.Drawing.Point(198, 27);
        this.lbl_bqcolor.Name = "lbl_bqcolor";
        this.lbl_bqcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_bqcolor.TabIndex = 3;
        this.lbl_bqcolor.Text = "       ";
        this.lbl_bqcolor.Click += new System.EventHandler(lbl_bqcolor_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(139, 27);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 38;
        this.label5.Text = "颜色：";
        this.txt_bq.Location = new System.Drawing.Point(75, 20);
        this.txt_bq.Name = "txt_bq";
        this.txt_bq.Size = new System.Drawing.Size(46, 21);
        this.txt_bq.TabIndex = 2;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 27);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 36;
        this.label1.Text = "标签文字：";
        this.标签设置.Controls.Add(this.label1);
        this.标签设置.Controls.Add(this.label5);
        this.标签设置.Controls.Add(this.lbl_bqcolor);
        this.标签设置.Controls.Add(this.label2);
        this.标签设置.Controls.Add(this.lbl_fillcolor);
        this.标签设置.Controls.Add(this.label4);
        this.标签设置.Controls.Add(this.txt_bq);
        this.标签设置.Controls.Add(this.txt_maincount);
        this.标签设置.Location = new System.Drawing.Point(21, 46);
        this.标签设置.Name = "标签设置";
        this.标签设置.Size = new System.Drawing.Size(266, 97);
        this.标签设置.TabIndex = 40;
        this.标签设置.TabStop = false;
        this.标签设置.Text = "表盘设置";
        this.btn_OK.Location = new System.Drawing.Point(130, 220);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 8;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(212, 219);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 9;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(310, 255);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.标签设置);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet3";
        this.Text = "仪表设置";
        base.Load += new System.EventHandler(BSet3_Load);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.标签设置.ResumeLayout(false);
        this.标签设置.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
