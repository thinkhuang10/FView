using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class PSet2 : Form
{
    public Color setcolor = Color.Yellow;

    private Label label1;

    private Label lbl_color;

    private Button btn_OK;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler Ckvarevent;

    public PSet2()
    {
        InitializeComponent();
    }

    private void Btn_OK_Click(object sender, EventArgs e)
    {
        setcolor = lbl_color.BackColor;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void PSet2_Load(object sender, EventArgs e)
    {
        lbl_color.BackColor = setcolor;
    }

    private void Lbl_color_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        lbl_color = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(25, 25);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "管道颜色：";
        lbl_color.AutoSize = true;
        lbl_color.BackColor = System.Drawing.Color.FromArgb(192, 192, 0);
        lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color.Location = new System.Drawing.Point(96, 25);
        lbl_color.Name = "lbl_color";
        lbl_color.Size = new System.Drawing.Size(55, 14);
        lbl_color.TabIndex = 0;
        lbl_color.Text = "        ";
        lbl_color.Click += new System.EventHandler(Lbl_color_Click);
        btn_OK.Location = new System.Drawing.Point(186, 20);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 1;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(Btn_OK_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(293, 64);
        base.Controls.Add(btn_OK);
        base.Controls.Add(lbl_color);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "PSet2";
        Text = "管道配置";
        base.Load += new System.EventHandler(PSet2_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
