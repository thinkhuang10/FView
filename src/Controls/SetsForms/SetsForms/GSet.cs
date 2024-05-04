using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class GSet : Form
{
    public string varname = "";

    public Color colortype = Color.Green;

    public Color setcolortype = Color.LightGray;

    public Color Bgcolor = Color.White;

    public float high = 100f;

    public float low;

    public int highpersnt = 100;

    public int lowpersnt;

    private int closeflag = 1;

    public string errorstr = "";

    public EventHandler OnShowPropertyDialog;

    private TextBox txt_var;

    private Button btn_select;

    private GroupBox groupBox1;

    private Label label1;

    private Label lbl_color3;

    private Label lbl_color2;

    private Label lbl_color1;

    private Label label3;

    private Label label2;

    private GroupBox groupBox2;

    private TextBox txt_minfill;

    private TextBox txt_maxfill;

    private Label label7;

    private Label label6;

    private TextBox txt_min;

    private TextBox txt_max;

    private Label label5;

    private Label label4;

    private Button btn_ok;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public GSet()
    {
        InitializeComponent();
    }

    private void btn_ok_Click(object sender, EventArgs e)
    {
        if (txt_var.Text == "")
        {
            MessageBox.Show("请输入变量名称！");
            return;
        }
        if (txt_max.Text == "")
        {
            MessageBox.Show("请输入最大值！");
            return;
        }
        if (txt_min.Text == "")
        {
            MessageBox.Show("请输入最小值！");
            return;
        }
        if (txt_maxfill.Text == "")
        {
            MessageBox.Show("请输入最大百分比！");
            return;
        }
        if (txt_minfill.Text == "")
        {
            MessageBox.Show("请输入最小百分比！");
            return;
        }
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                highpersnt = Convert.ToInt32(txt_maxfill.Text);
                lowpersnt = Convert.ToInt32(txt_minfill.Text);
                high = Convert.ToSingle(txt_max.Text);
                low = Convert.ToSingle(txt_min.Text);
                if (high < low || highpersnt < lowpersnt)
                {
                    MessageBox.Show("最大值或最大百分比不能小于最小值或最小百分比！");
                    closeflag = 0;
                }
                else if (highpersnt > 100 || lowpersnt < 0)
                {
                    MessageBox.Show("最大百分比不能大于100或最小百分比不能小于0！");
                    closeflag = 0;
                }
                else
                {
                    closeflag = 1;
                }
                varname = "[" + txt_var.Text + "]";
                setcolortype = lbl_color3.BackColor;
                colortype = lbl_color1.BackColor;
                Bgcolor = lbl_color2.BackColor;
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
            MessageBox.Show("出现异常，可能是您填写的文本类型有问题！");
        }
    }

    private void GSet_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname.ToString()))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
        }
        txt_max.Text = high.ToString();
        txt_min.Text = low.ToString();
        txt_maxfill.Text = highpersnt.ToString();
        txt_minfill.Text = lowpersnt.ToString();
        lbl_color1.BackColor = colortype;
        lbl_color2.BackColor = Bgcolor;
        lbl_color3.BackColor = setcolortype;
    }

    public new DialogResult ShowDialog()
    {
        return base.ShowDialog();
    }

    private void lbl_color1_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color1.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_color3_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color3.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_color2_Click(object sender, EventArgs e)
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

    private void btn_select_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_select = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_color3 = new System.Windows.Forms.Label();
        this.lbl_color2 = new System.Windows.Forms.Label();
        this.lbl_color1 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.txt_minfill = new System.Windows.Forms.TextBox();
        this.txt_maxfill = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.txt_min = new System.Windows.Forms.TextBox();
        this.txt_max = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.btn_ok = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(16, 12);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(190, 21);
        this.txt_var.TabIndex = 0;
        this.btn_select.Location = new System.Drawing.Point(227, 12);
        this.btn_select.Name = "btn_select";
        this.btn_select.Size = new System.Drawing.Size(75, 23);
        this.btn_select.TabIndex = 1;
        this.btn_select.Text = "...";
        this.btn_select.UseVisualStyleBackColor = true;
        this.btn_select.Click += new System.EventHandler(btn_select_Click);
        this.groupBox1.Controls.Add(this.lbl_color3);
        this.groupBox1.Controls.Add(this.lbl_color2);
        this.groupBox1.Controls.Add(this.lbl_color1);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(16, 41);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(286, 69);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色设置";
        this.lbl_color3.AutoSize = true;
        this.lbl_color3.BackColor = System.Drawing.Color.Silver;
        this.lbl_color3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color3.Location = new System.Drawing.Point(87, 43);
        this.lbl_color3.Name = "lbl_color3";
        this.lbl_color3.Size = new System.Drawing.Size(43, 14);
        this.lbl_color3.TabIndex = 4;
        this.lbl_color3.Text = "      ";
        this.lbl_color3.Click += new System.EventHandler(lbl_color3_Click);
        this.lbl_color2.AutoSize = true;
        this.lbl_color2.BackColor = System.Drawing.Color.White;
        this.lbl_color2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color2.Location = new System.Drawing.Point(226, 21);
        this.lbl_color2.Name = "lbl_color2";
        this.lbl_color2.Size = new System.Drawing.Size(43, 14);
        this.lbl_color2.TabIndex = 3;
        this.lbl_color2.Text = "      ";
        this.lbl_color2.Click += new System.EventHandler(lbl_color2_Click);
        this.lbl_color1.AutoSize = true;
        this.lbl_color1.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_color1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color1.Location = new System.Drawing.Point(87, 21);
        this.lbl_color1.Name = "lbl_color1";
        this.lbl_color1.Size = new System.Drawing.Size(43, 14);
        this.lbl_color1.TabIndex = 2;
        this.lbl_color1.Text = "      ";
        this.lbl_color1.Click += new System.EventHandler(lbl_color1_Click);
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(139, 21);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(83, 12);
        this.label3.TabIndex = 2;
        this.label3.Text = "填充背景颜色:";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(17, 43);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(59, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "罐体颜色:";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(17, 21);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "填充颜色:";
        this.groupBox2.Controls.Add(this.txt_minfill);
        this.groupBox2.Controls.Add(this.txt_maxfill);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Controls.Add(this.txt_min);
        this.groupBox2.Controls.Add(this.txt_max);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Controls.Add(this.label4);
        this.groupBox2.Location = new System.Drawing.Point(16, 116);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(286, 83);
        this.groupBox2.TabIndex = 3;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "填充设置";
        this.txt_minfill.Location = new System.Drawing.Point(219, 47);
        this.txt_minfill.Name = "txt_minfill";
        this.txt_minfill.Size = new System.Drawing.Size(48, 21);
        this.txt_minfill.TabIndex = 8;
        this.txt_maxfill.Location = new System.Drawing.Point(219, 21);
        this.txt_maxfill.Name = "txt_maxfill";
        this.txt_maxfill.Size = new System.Drawing.Size(48, 21);
        this.txt_maxfill.TabIndex = 6;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(136, 54);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(77, 12);
        this.label7.TabIndex = 5;
        this.label7.Text = "最小填充(%):";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(136, 24);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(77, 12);
        this.label6.TabIndex = 4;
        this.label6.Text = "最大填充(%):";
        this.txt_min.Location = new System.Drawing.Point(71, 47);
        this.txt_min.Name = "txt_min";
        this.txt_min.Size = new System.Drawing.Size(53, 21);
        this.txt_min.TabIndex = 7;
        this.txt_max.Location = new System.Drawing.Point(71, 20);
        this.txt_max.Name = "txt_max";
        this.txt_max.Size = new System.Drawing.Size(53, 21);
        this.txt_max.TabIndex = 5;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(20, 54);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(47, 12);
        this.label5.TabIndex = 1;
        this.label5.Text = "最小值:";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(20, 24);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(47, 12);
        this.label4.TabIndex = 0;
        this.label4.Text = "最大值:";
        this.btn_ok.Location = new System.Drawing.Point(154, 205);
        this.btn_ok.Name = "btn_ok";
        this.btn_ok.Size = new System.Drawing.Size(64, 23);
        this.btn_ok.TabIndex = 9;
        this.btn_ok.Text = "确定";
        this.btn_ok.UseVisualStyleBackColor = true;
        this.btn_ok.Click += new System.EventHandler(btn_ok_Click);
        this.btn_cancel.Location = new System.Drawing.Point(239, 205);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(63, 23);
        this.btn_cancel.TabIndex = 10;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(319, 238);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.btn_ok);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_select);
        base.Controls.Add(this.txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "GSet";
        this.Text = "罐向导";
        base.Load += new System.EventHandler(GSet_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
