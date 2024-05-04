using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class Sign2 : Form
{
    public int linewidth = 1;

    public Color linecolor = Color.Red;

    public Color fillcolor = Color.Green;

    private Button btn_OK;

    private Button btn_cancel;

    private GroupBox groupBox1;

    private Label lbl_fillcolor;

    private Label lbl_linecolor;

    private Label label5;

    private Label label4;

    private TextBox txt_linewidth;

    private Label label2;

    private ColorDialog colorDialog1;

    public event viewhandler viewevent;

    public Sign2()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            linewidth = Convert.ToInt32(txt_linewidth.Text);
            linecolor = lbl_linecolor.BackColor;
            fillcolor = lbl_fillcolor.BackColor;
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch
        {
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
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

    private void lbl_fillcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_fillcolor = new System.Windows.Forms.Label();
        this.lbl_linecolor = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.txt_linewidth = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_OK.Location = new System.Drawing.Point(56, 176);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 3;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(137, 176);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 4;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.groupBox1.Controls.Add(this.lbl_fillcolor);
        this.groupBox1.Controls.Add(this.lbl_linecolor);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.txt_linewidth);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Location = new System.Drawing.Point(27, 17);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(165, 139);
        this.groupBox1.TabIndex = 70;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.lbl_fillcolor.AutoSize = true;
        this.lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        this.lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor.Location = new System.Drawing.Point(85, 89);
        this.lbl_fillcolor.Name = "lbl_fillcolor";
        this.lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_fillcolor.TabIndex = 2;
        this.lbl_fillcolor.Text = "       ";
        this.lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        this.lbl_linecolor.AutoSize = true;
        this.lbl_linecolor.BackColor = System.Drawing.Color.Red;
        this.lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_linecolor.Location = new System.Drawing.Point(85, 60);
        this.lbl_linecolor.Name = "lbl_linecolor";
        this.lbl_linecolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_linecolor.TabIndex = 1;
        this.lbl_linecolor.Text = "       ";
        this.lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(25, 89);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(53, 12);
        this.label5.TabIndex = 6;
        this.label5.Text = "填充色：";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(25, 60);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(41, 12);
        this.label4.TabIndex = 5;
        this.label4.Text = "线色：";
        this.txt_linewidth.Location = new System.Drawing.Point(83, 28);
        this.txt_linewidth.Name = "txt_linewidth";
        this.txt_linewidth.Size = new System.Drawing.Size(51, 21);
        this.txt_linewidth.TabIndex = 0;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(25, 31);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "线宽：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(224, 224);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox1);
        base.Name = "Sign2";
        this.Text = "注释";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
