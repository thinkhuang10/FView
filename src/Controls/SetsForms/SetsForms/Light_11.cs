using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class Light_11 : Form
{
    public string varname1;

    public string varname2;

    public int spdtype = 1;

    public Color truecolor = Color.Green;

    public Color falsecolor = Color.Red;

    public int lighttype;

    private TextBox txt_var;

    private Button btn_view;

    private GroupBox groupBox1;

    private Label lbl_true;

    private Label label6;

    private Label lbl_fasle;

    private Label label2;

    private GroupBox groupBox2;

    private TextBox txt_var2;

    private ComboBox cbx_spd;

    private Label label1;

    private CheckBox ckb_light;

    private Label label3;

    private Button btn_var2;

    private Button btn_Cancel;

    private Button btn_OK;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public Light_11()
    {
        InitializeComponent();
    }

    private void label6_Click(object sender, EventArgs e)
    {
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ckb_light.Checked && !this.ckvarevent(txt_var2.Text))
            {
                MessageBox.Show("控制闪烁变量错误！请选择合适的变量类型！");
            }
            else
            {
                varname2 = txt_var2.Text;
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
                if (ckb_light.Checked)
                {
                    lighttype = 1;
                }
                else
                {
                    lighttype = 0;
                }
            }
            if (this.ckvarevent(txt_var.Text))
            {
                if (!string.IsNullOrEmpty(txt_var.Text))
                {
                    varname1 = txt_var.Text;
                    truecolor = lbl_true.BackColor;
                    falsecolor = lbl_fasle.BackColor;
                    if (ckb_light.Checked)
                    {
                        lighttype = 1;
                    }
                    else
                    {
                        lighttype = 0;
                    }
                }
                if (string.IsNullOrEmpty(txt_var.Text) || (ckb_light.Checked && string.IsNullOrEmpty(txt_var2.Text)))
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
            MessageBox.Show("报警灯出现异常！");
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

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void btn_var2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Light_11_Load(object sender, EventArgs e)
    {
        cbx_spd.SelectedIndex = 0;
        if (!string.IsNullOrEmpty(varname1))
        {
            txt_var.Text = varname1.Substring(1, varname1.Length - 2);
            txt_var2.Text = varname2.Substring(1, varname2.Length - 2);
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

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_fasle = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_true = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.label3 = new System.Windows.Forms.Label();
        this.btn_var2 = new System.Windows.Forms.Button();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.cbx_spd = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.ckb_light = new System.Windows.Forms.CheckBox();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(17, 20);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(142, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(178, 20);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.lbl_fasle);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.lbl_true);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.txt_var);
        this.groupBox1.Controls.Add(this.btn_view);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(268, 96);
        this.groupBox1.TabIndex = 59;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "一般";
        this.lbl_fasle.AutoSize = true;
        this.lbl_fasle.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_fasle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fasle.Location = new System.Drawing.Point(197, 57);
        this.lbl_fasle.Name = "lbl_fasle";
        this.lbl_fasle.Size = new System.Drawing.Size(37, 14);
        this.lbl_fasle.TabIndex = 3;
        this.lbl_fasle.Text = "     ";
        this.lbl_fasle.Click += new System.EventHandler(lbl_fasle_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(138, 57);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 119;
        this.label2.Text = "为假时：";
        this.lbl_true.AutoSize = true;
        this.lbl_true.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_true.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_true.Location = new System.Drawing.Point(74, 57);
        this.lbl_true.Name = "lbl_true";
        this.lbl_true.Size = new System.Drawing.Size(37, 14);
        this.lbl_true.TabIndex = 2;
        this.lbl_true.Text = "     ";
        this.lbl_true.Click += new System.EventHandler(lbl_true_Click);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(15, 57);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(53, 12);
        this.label6.TabIndex = 117;
        this.label6.Text = "为真时：";
        this.label6.Click += new System.EventHandler(label6_Click);
        this.groupBox2.Controls.Add(this.label3);
        this.groupBox2.Controls.Add(this.btn_var2);
        this.groupBox2.Controls.Add(this.txt_var2);
        this.groupBox2.Controls.Add(this.cbx_spd);
        this.groupBox2.Controls.Add(this.label1);
        this.groupBox2.Controls.Add(this.ckb_light);
        this.groupBox2.Location = new System.Drawing.Point(12, 128);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(268, 89);
        this.groupBox2.TabIndex = 60;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "闪烁";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(173, 62);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(71, 12);
        this.label3.TabIndex = 60;
        this.label3.Text = "不为0时闪烁";
        this.btn_var2.Location = new System.Drawing.Point(125, 53);
        this.btn_var2.Name = "btn_var2";
        this.btn_var2.Size = new System.Drawing.Size(34, 23);
        this.btn_var2.TabIndex = 7;
        this.btn_var2.Text = "....";
        this.btn_var2.UseVisualStyleBackColor = true;
        this.btn_var2.Click += new System.EventHandler(btn_var2_Click);
        this.txt_var2.Location = new System.Drawing.Point(17, 54);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(100, 21);
        this.txt_var2.TabIndex = 6;
        this.cbx_spd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbx_spd.FormattingEnabled = true;
        this.cbx_spd.Items.AddRange(new object[3] { "低速", "中速", "高速" });
        this.cbx_spd.Location = new System.Drawing.Point(178, 15);
        this.cbx_spd.Name = "cbx_spd";
        this.cbx_spd.Size = new System.Drawing.Size(75, 20);
        this.cbx_spd.TabIndex = 5;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(123, 21);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "速度：";
        this.ckb_light.AutoSize = true;
        this.ckb_light.Location = new System.Drawing.Point(17, 21);
        this.ckb_light.Name = "ckb_light";
        this.ckb_light.Size = new System.Drawing.Size(72, 16);
        this.ckb_light.TabIndex = 4;
        this.ckb_light.Text = "允许闪烁";
        this.ckb_light.UseVisualStyleBackColor = true;
        this.ckb_light.CheckedChanged += new System.EventHandler(ckb_light_CheckedChanged);
        this.btn_Cancel.Location = new System.Drawing.Point(196, 232);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 9;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(115, 232);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 8;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 276);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "Light_11";
        this.Text = "警示灯";
        base.Load += new System.EventHandler(Light_11_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        base.ResumeLayout(false);
    }
}
