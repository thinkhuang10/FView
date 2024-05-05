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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        label4 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        txt_var = new System.Windows.Forms.TextBox();
        lbl_fillcolor = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txt_minmark = new System.Windows.Forms.TextBox();
        txt_maxmark = new System.Windows.Forms.TextBox();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        btn_view = new System.Windows.Forms.Button();
        txt_maincount = new System.Windows.Forms.TextBox();
        lbl_bqcolor = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        txt_bq = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        标签设置 = new System.Windows.Forms.GroupBox();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox2.SuspendLayout();
        标签设置.SuspendLayout();
        base.SuspendLayout();
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(6, 57);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 35;
        label4.Text = "主刻度数：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(139, 57);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 34;
        label2.Text = "填充色：";
        txt_var.Location = new System.Drawing.Point(21, 17);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        lbl_fillcolor.AutoSize = true;
        lbl_fillcolor.BackColor = System.Drawing.Color.Black;
        lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor.Location = new System.Drawing.Point(198, 57);
        lbl_fillcolor.Name = "lbl_fillcolor";
        lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        lbl_fillcolor.TabIndex = 5;
        lbl_fillcolor.Text = "       ";
        lbl_fillcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        groupBox2.Controls.Add(txt_minmark);
        groupBox2.Controls.Add(txt_maxmark);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Location = new System.Drawing.Point(21, 149);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(266, 58);
        groupBox2.TabIndex = 33;
        groupBox2.TabStop = false;
        groupBox2.Text = "量程范围";
        txt_minmark.Location = new System.Drawing.Point(191, 20);
        txt_minmark.Name = "txt_minmark";
        txt_minmark.Size = new System.Drawing.Size(56, 21);
        txt_minmark.TabIndex = 7;
        txt_maxmark.Location = new System.Drawing.Point(63, 20);
        txt_maxmark.Name = "txt_maxmark";
        txt_maxmark.Size = new System.Drawing.Size(58, 21);
        txt_maxmark.TabIndex = 6;
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(139, 25);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(53, 12);
        label8.TabIndex = 1;
        label8.Text = "最小值：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(15, 25);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 0;
        label7.Text = "最大值：";
        btn_view.Location = new System.Drawing.Point(212, 17);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_maincount.Location = new System.Drawing.Point(75, 54);
        txt_maincount.Name = "txt_maincount";
        txt_maincount.Size = new System.Drawing.Size(45, 21);
        txt_maincount.TabIndex = 4;
        lbl_bqcolor.AutoSize = true;
        lbl_bqcolor.BackColor = System.Drawing.SystemColors.ControlText;
        lbl_bqcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bqcolor.Location = new System.Drawing.Point(198, 27);
        lbl_bqcolor.Name = "lbl_bqcolor";
        lbl_bqcolor.Size = new System.Drawing.Size(49, 14);
        lbl_bqcolor.TabIndex = 3;
        lbl_bqcolor.Text = "       ";
        lbl_bqcolor.Click += new System.EventHandler(lbl_bqcolor_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(139, 27);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(41, 12);
        label5.TabIndex = 38;
        label5.Text = "颜色：";
        txt_bq.Location = new System.Drawing.Point(75, 20);
        txt_bq.Name = "txt_bq";
        txt_bq.Size = new System.Drawing.Size(46, 21);
        txt_bq.TabIndex = 2;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(6, 27);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 36;
        label1.Text = "标签文字：";
        标签设置.Controls.Add(label1);
        标签设置.Controls.Add(label5);
        标签设置.Controls.Add(lbl_bqcolor);
        标签设置.Controls.Add(label2);
        标签设置.Controls.Add(lbl_fillcolor);
        标签设置.Controls.Add(label4);
        标签设置.Controls.Add(txt_bq);
        标签设置.Controls.Add(txt_maincount);
        标签设置.Location = new System.Drawing.Point(21, 46);
        标签设置.Name = "标签设置";
        标签设置.Size = new System.Drawing.Size(266, 97);
        标签设置.TabIndex = 40;
        标签设置.TabStop = false;
        标签设置.Text = "表盘设置";
        btn_OK.Location = new System.Drawing.Point(130, 220);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 8;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(212, 219);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 9;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(310, 255);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(标签设置);
        base.Controls.Add(txt_var);
        base.Controls.Add(groupBox2);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BSet3";
        Text = "仪表设置";
        base.Load += new System.EventHandler(BSet3_Load);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        标签设置.ResumeLayout(false);
        标签设置.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
