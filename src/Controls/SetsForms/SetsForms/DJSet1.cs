using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class DJSet1 : Form
{
    public string varname;

    public Color oncolor = Color.Green;

    public Color offcolor = Color.Red;

    private Label lbl_off;

    private Label label11;

    private Button btn_Cancel;

    private Button btn_OK;

    private Label lbl_on;

    private TextBox txt_var;

    private Button btn_view;

    private Label label1;

    private GroupBox groupBox1;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public DJSet1()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (txt_var.Text == "")
                {
                    MessageBox.Show("变量名不能为空！");
                    return;
                }
                varname = txt_var.Text;
                oncolor = lbl_on.BackColor;
                offcolor = lbl_off.BackColor;
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常！");
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
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

    private void DJSet1_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            lbl_on.BackColor = oncolor;
            lbl_off.BackColor = offcolor;
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

    private void InitializeComponent()
    {
        this.lbl_off = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.lbl_on = new System.Windows.Forms.Label();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.lbl_off.AutoSize = true;
        this.lbl_off.BackColor = System.Drawing.Color.Red;
        this.lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_off.Location = new System.Drawing.Point(193, 28);
        this.lbl_off.Name = "lbl_off";
        this.lbl_off.Size = new System.Drawing.Size(37, 14);
        this.lbl_off.TabIndex = 3;
        this.lbl_off.Text = "     ";
        this.lbl_off.Click += new System.EventHandler(lbl_off_Click);
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(133, 28);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(53, 12);
        this.label11.TabIndex = 135;
        this.label11.Text = "关颜色：";
        this.btn_Cancel.Location = new System.Drawing.Point(197, 115);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 5;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(116, 115);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 4;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.lbl_on.AutoSize = true;
        this.lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_on.Location = new System.Drawing.Point(77, 28);
        this.lbl_on.Name = "lbl_on";
        this.lbl_on.Size = new System.Drawing.Size(37, 14);
        this.lbl_on.TabIndex = 2;
        this.lbl_on.Text = "     ";
        this.lbl_on.Click += new System.EventHandler(lbl_on_Click);
        this.txt_var.Location = new System.Drawing.Point(22, 12);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(154, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(197, 10);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(17, 28);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 131;
        this.label1.Text = "开颜色：";
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.lbl_off);
        this.groupBox1.Controls.Add(this.lbl_on);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Location = new System.Drawing.Point(22, 39);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(250, 61);
        this.groupBox1.TabIndex = 137;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色设置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 149);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "DJSet1";
        this.Text = "配置界面";
        base.Load += new System.EventHandler(DJSet1_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
