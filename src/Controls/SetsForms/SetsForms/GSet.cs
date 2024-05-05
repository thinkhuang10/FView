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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void InitializeComponent()
    {
        txt_var = new System.Windows.Forms.TextBox();
        btn_select = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_color3 = new System.Windows.Forms.Label();
        lbl_color2 = new System.Windows.Forms.Label();
        lbl_color1 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txt_minfill = new System.Windows.Forms.TextBox();
        txt_maxfill = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        txt_min = new System.Windows.Forms.TextBox();
        txt_max = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        btn_ok = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(16, 12);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(190, 21);
        txt_var.TabIndex = 0;
        btn_select.Location = new System.Drawing.Point(227, 12);
        btn_select.Name = "btn_select";
        btn_select.Size = new System.Drawing.Size(75, 23);
        btn_select.TabIndex = 1;
        btn_select.Text = "...";
        btn_select.UseVisualStyleBackColor = true;
        btn_select.Click += new System.EventHandler(btn_select_Click);
        groupBox1.Controls.Add(lbl_color3);
        groupBox1.Controls.Add(lbl_color2);
        groupBox1.Controls.Add(lbl_color1);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(16, 41);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(286, 69);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色设置";
        lbl_color3.AutoSize = true;
        lbl_color3.BackColor = System.Drawing.Color.Silver;
        lbl_color3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color3.Location = new System.Drawing.Point(87, 43);
        lbl_color3.Name = "lbl_color3";
        lbl_color3.Size = new System.Drawing.Size(43, 14);
        lbl_color3.TabIndex = 4;
        lbl_color3.Text = "      ";
        lbl_color3.Click += new System.EventHandler(lbl_color3_Click);
        lbl_color2.AutoSize = true;
        lbl_color2.BackColor = System.Drawing.Color.White;
        lbl_color2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color2.Location = new System.Drawing.Point(226, 21);
        lbl_color2.Name = "lbl_color2";
        lbl_color2.Size = new System.Drawing.Size(43, 14);
        lbl_color2.TabIndex = 3;
        lbl_color2.Text = "      ";
        lbl_color2.Click += new System.EventHandler(lbl_color2_Click);
        lbl_color1.AutoSize = true;
        lbl_color1.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_color1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color1.Location = new System.Drawing.Point(87, 21);
        lbl_color1.Name = "lbl_color1";
        lbl_color1.Size = new System.Drawing.Size(43, 14);
        lbl_color1.TabIndex = 2;
        lbl_color1.Text = "      ";
        lbl_color1.Click += new System.EventHandler(lbl_color1_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(139, 21);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(83, 12);
        label3.TabIndex = 2;
        label3.Text = "填充背景颜色:";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(17, 43);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(59, 12);
        label2.TabIndex = 1;
        label2.Text = "罐体颜色:";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(17, 21);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 0;
        label1.Text = "填充颜色:";
        groupBox2.Controls.Add(txt_minfill);
        groupBox2.Controls.Add(txt_maxfill);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(txt_min);
        groupBox2.Controls.Add(txt_max);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(label4);
        groupBox2.Location = new System.Drawing.Point(16, 116);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(286, 83);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "填充设置";
        txt_minfill.Location = new System.Drawing.Point(219, 47);
        txt_minfill.Name = "txt_minfill";
        txt_minfill.Size = new System.Drawing.Size(48, 21);
        txt_minfill.TabIndex = 8;
        txt_maxfill.Location = new System.Drawing.Point(219, 21);
        txt_maxfill.Name = "txt_maxfill";
        txt_maxfill.Size = new System.Drawing.Size(48, 21);
        txt_maxfill.TabIndex = 6;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(136, 54);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(77, 12);
        label7.TabIndex = 5;
        label7.Text = "最小填充(%):";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(136, 24);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(77, 12);
        label6.TabIndex = 4;
        label6.Text = "最大填充(%):";
        txt_min.Location = new System.Drawing.Point(71, 47);
        txt_min.Name = "txt_min";
        txt_min.Size = new System.Drawing.Size(53, 21);
        txt_min.TabIndex = 7;
        txt_max.Location = new System.Drawing.Point(71, 20);
        txt_max.Name = "txt_max";
        txt_max.Size = new System.Drawing.Size(53, 21);
        txt_max.TabIndex = 5;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(20, 54);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(47, 12);
        label5.TabIndex = 1;
        label5.Text = "最小值:";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(20, 24);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(47, 12);
        label4.TabIndex = 0;
        label4.Text = "最大值:";
        btn_ok.Location = new System.Drawing.Point(154, 205);
        btn_ok.Name = "btn_ok";
        btn_ok.Size = new System.Drawing.Size(64, 23);
        btn_ok.TabIndex = 9;
        btn_ok.Text = "确定";
        btn_ok.UseVisualStyleBackColor = true;
        btn_ok.Click += new System.EventHandler(btn_ok_Click);
        btn_cancel.Location = new System.Drawing.Point(239, 205);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(63, 23);
        btn_cancel.TabIndex = 10;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(319, 238);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(btn_ok);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_select);
        base.Controls.Add(txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "GSet";
        Text = "罐向导";
        base.Load += new System.EventHandler(GSet_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
