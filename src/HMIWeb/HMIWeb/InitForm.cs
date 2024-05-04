using System.ComponentModel;
using System.Windows.Forms;

namespace HMIWeb;

public class InitForm : Form
{
    private Label label1;

    private Panel panel1;

    public InitForm()
    {
        InitializeComponent();
    }

    public void Say(string text)
    {
        label1.Text = text;
        Refresh();
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.panel1.SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(25, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(0, 12);
        this.label1.TabIndex = 0;
        this.panel1.BackColor = System.Drawing.Color.Gainsboro;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.panel1.Controls.Add(this.label1);
        this.panel1.Location = new System.Drawing.Point(12, 9);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(248, 53);
        this.panel1.TabIndex = 1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkGray;
        base.ClientSize = new System.Drawing.Size(270, 73);
        base.Controls.Add(this.panel1);
        this.DoubleBuffered = true;
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        base.Name = "InitForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "InitForm";
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        base.ResumeLayout(false);
    }
}