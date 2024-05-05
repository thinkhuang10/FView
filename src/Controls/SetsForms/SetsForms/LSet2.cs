using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class LSet2 : Form
{
    public string varname1 = "";

    public string varname2 = "";

    public string truetxt = "";

    public string falsetxt = "";

    public int spdtype = 1;

    public Color truecolor = Color.Green;

    public Color falsecolor = Color.Red;

    public Color txtcolor = Color.Blue;

    public int lighttype;

    private Button btn_Cancel;

    private GroupBox groupBox2;

    private Label label3;

    private Button btn_var2;

    private TextBox txt_var2;

    private ComboBox cbx_spd;

    private Label label1;

    private CheckBox ckb_light;

    private Button btn_OK;

    private GroupBox groupBox1;

    private Label lbl_txtcolor;

    private Label label7;

    private TextBox txt_falsetxt;

    private TextBox txt_truetxt;

    private Label label5;

    private Label label4;

    private Label lbl_fasle;

    private Label label2;

    private Label lbl_true;

    private Label label6;

    private TextBox txt_var;

    private Button btn_view;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public LSet2()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ckb_light.Checked && !ckvarevent(txt_var2.Text))
            {
                MessageBox.Show("控制闪烁变量错误！请选择合适的变量类型！");
            }
            else
            {
                varname2 = txt_var2.Text;
                if (ckb_light.Checked)
                {
                    lighttype = 1;
                }
                else
                {
                    lighttype = 0;
                }
                switch (cbx_spd.SelectedIndex)
                {
                    case -1:
                        spdtype = 1;
                        break;
                    case 0:
                        spdtype = 1;
                        break;
                    case 1:
                        spdtype = 2;
                        break;
                    case 2:
                        spdtype = 3;
                        break;
                }
            }
            if (ckvarevent(txt_var.Text))
            {
                if (!string.IsNullOrEmpty(txt_var.Text))
                {
                    truetxt = txt_truetxt.Text;
                    falsetxt = txt_falsetxt.Text;
                    varname1 = txt_var.Text;
                    truecolor = lbl_true.BackColor;
                    falsecolor = lbl_fasle.BackColor;
                    txtcolor = lbl_txtcolor.BackColor;
                }
                if (string.IsNullOrEmpty(varname1))
                {
                    MessageBox.Show("变量名不能为空！");
                    return;
                }
                Close();
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("控制报警变量错误！请选择合适的变量类型！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常！");
        }
    }

    private void ckb_light_CheckedChanged(object sender, EventArgs e)
    {
        if (ckb_light.Checked)
        {
            cbx_spd.Enabled = true;
            txt_var2.Enabled = true;
            btn_var2.Enabled = true;
        }
        else
        {
            cbx_spd.Enabled = false;
            txt_var2.Enabled = false;
            btn_var2.Enabled = false;
        }
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void LSet2_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname1))
        {
            txt_var.Text = varname1.Substring(1, varname1.Length - 2);
            txt_var2.Text = varname2.Substring(1, varname2.Length - 2);
            txt_truetxt.Text = truetxt;
            txt_falsetxt.Text = falsetxt;
            if (lighttype == 1)
            {
                ckb_light.Checked = true;
            }
            else
            {
                ckb_light.Checked = false;
            }
            lbl_true.BackColor = truecolor;
            lbl_fasle.BackColor = falsecolor;
            lbl_txtcolor.BackColor = txtcolor;
            switch (spdtype)
            {
                case 1:
                    cbx_spd.SelectedIndex = 0;
                    break;
                case 2:
                    cbx_spd.SelectedIndex = 1;
                    break;
                case 3:
                    cbx_spd.SelectedIndex = 2;
                    break;
            }
        }
    }

    private void lbl_true_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_true.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_fasle_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fasle.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_var2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void InitializeComponent()
    {
        btn_Cancel = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        label3 = new System.Windows.Forms.Label();
        btn_var2 = new System.Windows.Forms.Button();
        txt_var2 = new System.Windows.Forms.TextBox();
        cbx_spd = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        ckb_light = new System.Windows.Forms.CheckBox();
        btn_OK = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_txtcolor = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        txt_falsetxt = new System.Windows.Forms.TextBox();
        txt_truetxt = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        lbl_fasle = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        lbl_true = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(196, 288);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 12;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(btn_var2);
        groupBox2.Controls.Add(txt_var2);
        groupBox2.Controls.Add(cbx_spd);
        groupBox2.Controls.Add(label1);
        groupBox2.Controls.Add(ckb_light);
        groupBox2.Location = new System.Drawing.Point(12, 184);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(268, 89);
        groupBox2.TabIndex = 64;
        groupBox2.TabStop = false;
        groupBox2.Text = "闪烁";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(173, 62);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(71, 12);
        label3.TabIndex = 60;
        label3.Text = "不为0时闪烁";
        btn_var2.Location = new System.Drawing.Point(125, 53);
        btn_var2.Name = "btn_var2";
        btn_var2.Size = new System.Drawing.Size(24, 23);
        btn_var2.TabIndex = 10;
        btn_var2.Text = "....";
        btn_var2.UseVisualStyleBackColor = true;
        btn_var2.Click += new System.EventHandler(btn_var2_Click);
        txt_var2.Location = new System.Drawing.Point(17, 54);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(100, 21);
        txt_var2.TabIndex = 9;
        cbx_spd.FormattingEnabled = true;
        cbx_spd.Items.AddRange(new object[3] { "低速", "中速", "高速" });
        cbx_spd.Location = new System.Drawing.Point(169, 17);
        cbx_spd.Name = "cbx_spd";
        cbx_spd.Size = new System.Drawing.Size(75, 20);
        cbx_spd.TabIndex = 8;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(123, 21);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 12);
        label1.TabIndex = 1;
        label1.Text = "速度：";
        ckb_light.AutoSize = true;
        ckb_light.Location = new System.Drawing.Point(17, 21);
        ckb_light.Name = "ckb_light";
        ckb_light.Size = new System.Drawing.Size(72, 16);
        ckb_light.TabIndex = 7;
        ckb_light.Text = "允许闪烁";
        ckb_light.UseVisualStyleBackColor = true;
        ckb_light.CheckedChanged += new System.EventHandler(ckb_light_CheckedChanged);
        btn_OK.Location = new System.Drawing.Point(115, 288);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 11;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(txt_falsetxt);
        groupBox1.Controls.Add(txt_truetxt);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_fasle);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(lbl_true);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(txt_var);
        groupBox1.Controls.Add(btn_view);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(268, 166);
        groupBox1.TabIndex = 63;
        groupBox1.TabStop = false;
        groupBox1.Text = "一般";
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.Color.Blue;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(103, 137);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(73, 14);
        lbl_txtcolor.TabIndex = 6;
        lbl_txtcolor.Text = "           ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(15, 138);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 125;
        label7.Text = "文字颜色：";
        txt_falsetxt.Location = new System.Drawing.Point(103, 109);
        txt_falsetxt.Name = "txt_falsetxt";
        txt_falsetxt.Size = new System.Drawing.Size(150, 21);
        txt_falsetxt.TabIndex = 5;
        txt_truetxt.Location = new System.Drawing.Point(103, 81);
        txt_truetxt.Name = "txt_truetxt";
        txt_truetxt.Size = new System.Drawing.Size(150, 21);
        txt_truetxt.TabIndex = 4;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(15, 111);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(77, 12);
        label5.TabIndex = 122;
        label5.Text = "为假时文字：";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(15, 84);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(77, 12);
        label4.TabIndex = 121;
        label4.Text = "为真时文字：";
        lbl_fasle.AutoSize = true;
        lbl_fasle.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_fasle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fasle.Location = new System.Drawing.Point(197, 57);
        lbl_fasle.Name = "lbl_fasle";
        lbl_fasle.Size = new System.Drawing.Size(37, 14);
        lbl_fasle.TabIndex = 3;
        lbl_fasle.Text = "     ";
        lbl_fasle.Click += new System.EventHandler(lbl_fasle_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(138, 57);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 119;
        label2.Text = "为假时：";
        lbl_true.AutoSize = true;
        lbl_true.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_true.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_true.Location = new System.Drawing.Point(74, 57);
        lbl_true.Name = "lbl_true";
        lbl_true.Size = new System.Drawing.Size(37, 14);
        lbl_true.TabIndex = 2;
        lbl_true.Text = "     ";
        lbl_true.Click += new System.EventHandler(lbl_true_Click);
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(15, 57);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(53, 12);
        label6.TabIndex = 117;
        label6.Text = "为真时：";
        txt_var.Location = new System.Drawing.Point(17, 20);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(142, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(178, 20);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 322);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(groupBox2);
        base.Controls.Add(btn_OK);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "LSet2";
        Text = "报警灯设置";
        base.Load += new System.EventHandler(LSet2_Load);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
