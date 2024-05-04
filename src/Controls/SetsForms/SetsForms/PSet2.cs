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

    public event checkvarnamehandler ckvarevent;

    public PSet2()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        setcolor = lbl_color.BackColor;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void PSet2_Load(object sender, EventArgs e)
    {
        lbl_color.BackColor = setcolor;
    }

    private void lbl_color_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_color = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(25, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "管道颜色：";
        this.lbl_color.AutoSize = true;
        this.lbl_color.BackColor = System.Drawing.Color.FromArgb(192, 192, 0);
        this.lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color.Location = new System.Drawing.Point(96, 25);
        this.lbl_color.Name = "lbl_color";
        this.lbl_color.Size = new System.Drawing.Size(55, 14);
        this.lbl_color.TabIndex = 0;
        this.lbl_color.Text = "        ";
        this.lbl_color.Click += new System.EventHandler(lbl_color_Click);
        this.btn_OK.Location = new System.Drawing.Point(186, 20);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 1;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(293, 64);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.lbl_color);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "PSet2";
        this.Text = "管道配置";
        base.Load += new System.EventHandler(PSet2_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
