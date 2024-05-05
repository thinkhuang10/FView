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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label3 = new System.Windows.Forms.Label();
        ckb_opst = new System.Windows.Forms.CheckBox();
        lbl_bg = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        lbl_off = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        lbl_title = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        txt_mark = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        lbl_on = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(223, 176);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 9;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(139, 176);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 8;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        txt_var.Location = new System.Drawing.Point(12, 20);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(208, 21);
        txt_var.TabIndex = 0;
        txt_var.TextChanged += new System.EventHandler(txt_var_TextChanged);
        btn_view.Location = new System.Drawing.Point(226, 20);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(ckb_opst);
        groupBox1.Controls.Add(lbl_bg);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(lbl_off);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(lbl_title);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_mark);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(lbl_on);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(12, 47);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(289, 114);
        groupBox1.TabIndex = 65;
        groupBox1.TabStop = false;
        groupBox1.Text = "配置";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(137, 24);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 88;
        label3.Text = "是否取反：";
        ckb_opst.AutoSize = true;
        ckb_opst.Location = new System.Drawing.Point(209, 24);
        ckb_opst.Name = "ckb_opst";
        ckb_opst.Size = new System.Drawing.Size(48, 16);
        ckb_opst.TabIndex = 3;
        ckb_opst.Text = "取反";
        ckb_opst.UseVisualStyleBackColor = true;
        lbl_bg.AutoSize = true;
        lbl_bg.BackColor = System.Drawing.Color.Blue;
        lbl_bg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bg.Location = new System.Drawing.Point(81, 25);
        lbl_bg.Name = "lbl_bg";
        lbl_bg.Size = new System.Drawing.Size(37, 14);
        lbl_bg.TabIndex = 2;
        lbl_bg.Text = "     ";
        lbl_bg.Click += new System.EventHandler(lbl_bg_Click);
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(9, 25);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(65, 12);
        label9.TabIndex = 85;
        label9.Text = "面板颜色：";
        lbl_off.AutoSize = true;
        lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_off.Location = new System.Drawing.Point(207, 85);
        lbl_off.Name = "lbl_off";
        lbl_off.Size = new System.Drawing.Size(37, 14);
        lbl_off.TabIndex = 7;
        lbl_off.Text = "     ";
        lbl_off.Click += new System.EventHandler(lbl_off_Click);
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(137, 85);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 83;
        label7.Text = "关颜色：";
        lbl_title.AutoSize = true;
        lbl_title.BackColor = System.Drawing.Color.Blue;
        lbl_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_title.Location = new System.Drawing.Point(81, 56);
        lbl_title.Name = "lbl_title";
        lbl_title.Size = new System.Drawing.Size(37, 14);
        lbl_title.TabIndex = 4;
        lbl_title.Text = "     ";
        lbl_title.Click += new System.EventHandler(lbl_title_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(9, 56);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 81;
        label5.Text = "标题颜色：";
        txt_mark.Location = new System.Drawing.Point(209, 53);
        txt_mark.Name = "txt_mark";
        txt_mark.Size = new System.Drawing.Size(57, 21);
        txt_mark.TabIndex = 5;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(137, 56);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 79;
        label2.Text = "标题文本：";
        lbl_on.AutoSize = true;
        lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_on.Location = new System.Drawing.Point(81, 85);
        lbl_on.Name = "lbl_on";
        lbl_on.Size = new System.Drawing.Size(37, 14);
        lbl_on.TabIndex = 6;
        lbl_on.Text = "     ";
        lbl_on.Click += new System.EventHandler(lbl_on_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(9, 85);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 73;
        label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(321, 210);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet3";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet3_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
