using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet3 : Form
{
    public Color Bgcolor = Color.Gray;

    public Color Txtcolor = Color.Black;

    public Color Oncolor = Color.Green;

    public Color Offcolor = Color.Red;

    public string varname = "";

    public string txtstr = "";

    public bool opstflag;

    private ColorDialog colorDialog1;

    private Button btn_Cancel;

    private Button btn_OK;

    private TextBox txt_var;

    private Button btn_view;

    private GroupBox groupBox1;

    private Label lbl_bg;

    private Label label9;

    private Label lbl_off;

    private Label label7;

    private Label lbl_title;

    private Label label5;

    private TextBox txt_mark;

    private Label label2;

    private Label lbl_on;

    private Label label1;

    private CheckBox ckb_opst;

    private Label label3;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet3()
    {
        InitializeComponent();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (!string.IsNullOrEmpty(txt_var.Text))
                {
                    Bgcolor = lbl_bg.BackColor;
                    Txtcolor = lbl_title.BackColor;
                    Oncolor = lbl_on.BackColor;
                    Offcolor = lbl_off.BackColor;
                    varname = "[" + txt_var.Text + "]";
                    txtstr = txt_mark.Text;
                    if (ckb_opst.Checked)
                    {
                        opstflag = true;
                    }
                    else
                    {
                        opstflag = false;
                    }
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请将信息填写完整！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常!");
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void lbl_bg_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bg.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_title_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_title.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_on_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_on.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_off_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_off.BackColor = colorDialog1.Color;
        }
    }

    private void KGSet3_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            ckb_opst.Checked = opstflag;
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            txt_mark.Text = txtstr;
            lbl_bg.BackColor = Bgcolor;
            lbl_on.BackColor = Oncolor;
            lbl_off.BackColor = Bgcolor;
        }
    }

    private void txt_var_TextChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label3 = new System.Windows.Forms.Label();
        this.ckb_opst = new System.Windows.Forms.CheckBox();
        this.lbl_bg = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.lbl_off = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.lbl_title = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_mark = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_on = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(223, 176);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 9;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(139, 176);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 8;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.txt_var.Location = new System.Drawing.Point(12, 20);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(208, 21);
        this.txt_var.TabIndex = 0;
        this.txt_var.TextChanged += new System.EventHandler(txt_var_TextChanged);
        this.btn_view.Location = new System.Drawing.Point(226, 20);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.ckb_opst);
        this.groupBox1.Controls.Add(this.lbl_bg);
        this.groupBox1.Controls.Add(this.label9);
        this.groupBox1.Controls.Add(this.lbl_off);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.lbl_title);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_mark);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.lbl_on);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(12, 47);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(289, 114);
        this.groupBox1.TabIndex = 65;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "配置";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(137, 24);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 88;
        this.label3.Text = "是否取反：";
        this.ckb_opst.AutoSize = true;
        this.ckb_opst.Location = new System.Drawing.Point(209, 24);
        this.ckb_opst.Name = "ckb_opst";
        this.ckb_opst.Size = new System.Drawing.Size(48, 16);
        this.ckb_opst.TabIndex = 3;
        this.ckb_opst.Text = "取反";
        this.ckb_opst.UseVisualStyleBackColor = true;
        this.lbl_bg.AutoSize = true;
        this.lbl_bg.BackColor = System.Drawing.Color.Blue;
        this.lbl_bg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bg.Location = new System.Drawing.Point(81, 25);
        this.lbl_bg.Name = "lbl_bg";
        this.lbl_bg.Size = new System.Drawing.Size(37, 14);
        this.lbl_bg.TabIndex = 2;
        this.lbl_bg.Text = "     ";
        this.lbl_bg.Click += new System.EventHandler(lbl_bg_Click);
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(9, 25);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(65, 12);
        this.label9.TabIndex = 85;
        this.label9.Text = "面板颜色：";
        this.lbl_off.AutoSize = true;
        this.lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_off.Location = new System.Drawing.Point(207, 85);
        this.lbl_off.Name = "lbl_off";
        this.lbl_off.Size = new System.Drawing.Size(37, 14);
        this.lbl_off.TabIndex = 7;
        this.lbl_off.Text = "     ";
        this.lbl_off.Click += new System.EventHandler(lbl_off_Click);
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(137, 85);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 83;
        this.label7.Text = "关颜色：";
        this.lbl_title.AutoSize = true;
        this.lbl_title.BackColor = System.Drawing.Color.Blue;
        this.lbl_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_title.Location = new System.Drawing.Point(81, 56);
        this.lbl_title.Name = "lbl_title";
        this.lbl_title.Size = new System.Drawing.Size(37, 14);
        this.lbl_title.TabIndex = 4;
        this.lbl_title.Text = "     ";
        this.lbl_title.Click += new System.EventHandler(lbl_title_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(9, 56);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 81;
        this.label5.Text = "标题颜色：";
        this.txt_mark.Location = new System.Drawing.Point(209, 53);
        this.txt_mark.Name = "txt_mark";
        this.txt_mark.Size = new System.Drawing.Size(57, 21);
        this.txt_mark.TabIndex = 5;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(137, 56);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 79;
        this.label2.Text = "标题文本：";
        this.lbl_on.AutoSize = true;
        this.lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_on.Location = new System.Drawing.Point(81, 85);
        this.lbl_on.Name = "lbl_on";
        this.lbl_on.Size = new System.Drawing.Size(37, 14);
        this.lbl_on.TabIndex = 6;
        this.lbl_on.Text = "     ";
        this.lbl_on.Click += new System.EventHandler(lbl_on_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(9, 85);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 73;
        this.label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(321, 210);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet3";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet3_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
