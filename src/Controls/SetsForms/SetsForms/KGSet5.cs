using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet5 : Form
{
    public string varname;

    public Color linecolor;

    public Color oncolor;

    public Color offcolor;

    private Button btn_Cancel;

    private Button btn_OK;

    private TextBox txt_var;

    private Button btn_view;

    private ColorDialog colorDialog1;

    private GroupBox groupBox1;

    private Label lbl_linecolor;

    private Label label6;

    private Label lbl_off;

    private Label label7;

    private Label lbl_on;

    private Label label1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet5()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txt_var.Text))
            {
                varname = txt_var.Text;
                linecolor = lbl_linecolor.BackColor;
                oncolor = lbl_on.BackColor;
                offcolor = lbl_off.BackColor;
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("请输入变量值！");
            }
        }
        catch
        {
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_linecolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_linecolor.BackColor = colorDialog1.Color;
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

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void KGSet5_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            lbl_on.BackColor = oncolor;
            lbl_off.BackColor = BackColor;
            lbl_linecolor.BackColor = linecolor;
        }
    }

    private void lbl_linecolor_Click_1(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_linecolor = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.lbl_off = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.lbl_on = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(185, 142);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 6;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(104, 142);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 5;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.txt_var.Location = new System.Drawing.Point(16, 17);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(154, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(187, 15);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.lbl_linecolor);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.lbl_off);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.lbl_on);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(16, 44);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(246, 92);
        this.groupBox1.TabIndex = 119;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色设置";
        this.lbl_linecolor.AutoSize = true;
        this.lbl_linecolor.BackColor = System.Drawing.Color.Yellow;
        this.lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_linecolor.Location = new System.Drawing.Point(81, 55);
        this.lbl_linecolor.Name = "lbl_linecolor";
        this.lbl_linecolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_linecolor.TabIndex = 4;
        this.lbl_linecolor.Text = "     ";
        this.lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click_1);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(18, 55);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 12);
        this.label6.TabIndex = 117;
        this.label6.Text = "线路颜色：";
        this.lbl_off.AutoSize = true;
        this.lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_off.Location = new System.Drawing.Point(192, 26);
        this.lbl_off.Name = "lbl_off";
        this.lbl_off.Size = new System.Drawing.Size(37, 14);
        this.lbl_off.TabIndex = 3;
        this.lbl_off.Text = "     ";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(133, 26);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 115;
        this.label7.Text = "关颜色：";
        this.lbl_on.AutoSize = true;
        this.lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_on.Location = new System.Drawing.Point(81, 26);
        this.lbl_on.Name = "lbl_on";
        this.lbl_on.Size = new System.Drawing.Size(37, 14);
        this.lbl_on.TabIndex = 2;
        this.lbl_on.Text = "     ";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(18, 26);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 113;
        this.label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(295, 177);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet5";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet5_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
