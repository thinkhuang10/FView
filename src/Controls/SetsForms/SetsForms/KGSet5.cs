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
        string value = viewevent();
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
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_linecolor = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        lbl_off = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        lbl_on = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(185, 142);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 6;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(104, 142);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 5;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        txt_var.Location = new System.Drawing.Point(16, 17);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(154, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(187, 15);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(lbl_linecolor);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(lbl_off);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(lbl_on);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(16, 44);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(246, 92);
        groupBox1.TabIndex = 119;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色设置";
        lbl_linecolor.AutoSize = true;
        lbl_linecolor.BackColor = System.Drawing.Color.Yellow;
        lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_linecolor.Location = new System.Drawing.Point(81, 55);
        lbl_linecolor.Name = "lbl_linecolor";
        lbl_linecolor.Size = new System.Drawing.Size(37, 14);
        lbl_linecolor.TabIndex = 4;
        lbl_linecolor.Text = "     ";
        lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click_1);
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(18, 55);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 12);
        label6.TabIndex = 117;
        label6.Text = "线路颜色：";
        lbl_off.AutoSize = true;
        lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_off.Location = new System.Drawing.Point(192, 26);
        lbl_off.Name = "lbl_off";
        lbl_off.Size = new System.Drawing.Size(37, 14);
        lbl_off.TabIndex = 3;
        lbl_off.Text = "     ";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(133, 26);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 115;
        label7.Text = "关颜色：";
        lbl_on.AutoSize = true;
        lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_on.Location = new System.Drawing.Point(81, 26);
        lbl_on.Name = "lbl_on";
        lbl_on.Size = new System.Drawing.Size(37, 14);
        lbl_on.TabIndex = 2;
        lbl_on.Text = "     ";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(18, 26);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 113;
        label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(295, 177);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet5";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet5_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
