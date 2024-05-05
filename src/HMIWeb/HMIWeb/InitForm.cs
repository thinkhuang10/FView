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
        label1 = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        panel1.SuspendLayout();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(25, 20);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(0, 12);
        label1.TabIndex = 0;
        panel1.BackColor = System.Drawing.Color.Gainsboro;
        panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        panel1.Controls.Add(label1);
        panel1.Location = new System.Drawing.Point(12, 9);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(248, 53);
        panel1.TabIndex = 1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.DarkGray;
        base.ClientSize = new System.Drawing.Size(270, 73);
        base.Controls.Add(panel1);
        DoubleBuffered = true;
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        base.Name = "InitForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "InitForm";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        base.ResumeLayout(false);
    }
}
