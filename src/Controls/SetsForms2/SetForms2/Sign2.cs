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
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_fillcolor = new System.Windows.Forms.Label();
        lbl_linecolor = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        txt_linewidth = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_OK.Location = new System.Drawing.Point(56, 176);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 3;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(137, 176);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 4;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        groupBox1.Controls.Add(lbl_fillcolor);
        groupBox1.Controls.Add(lbl_linecolor);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(txt_linewidth);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new System.Drawing.Point(27, 17);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(165, 139);
        groupBox1.TabIndex = 70;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        lbl_fillcolor.AutoSize = true;
        lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor.Location = new System.Drawing.Point(85, 89);
        lbl_fillcolor.Name = "lbl_fillcolor";
        lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        lbl_fillcolor.TabIndex = 2;
        lbl_fillcolor.Text = "       ";
        lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        lbl_linecolor.AutoSize = true;
        lbl_linecolor.BackColor = System.Drawing.Color.Red;
        lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_linecolor.Location = new System.Drawing.Point(85, 60);
        lbl_linecolor.Name = "lbl_linecolor";
        lbl_linecolor.Size = new System.Drawing.Size(49, 14);
        lbl_linecolor.TabIndex = 1;
        lbl_linecolor.Text = "       ";
        lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(25, 89);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(53, 12);
        label5.TabIndex = 6;
        label5.Text = "填充色：";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(25, 60);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(41, 12);
        label4.TabIndex = 5;
        label4.Text = "线色：";
        txt_linewidth.Location = new System.Drawing.Point(83, 28);
        txt_linewidth.Name = "txt_linewidth";
        txt_linewidth.Size = new System.Drawing.Size(51, 21);
        txt_linewidth.TabIndex = 0;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(25, 31);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(41, 12);
        label2.TabIndex = 2;
        label2.Text = "线宽：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(224, 224);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox1);
        base.Name = "Sign2";
        Text = "注释";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
