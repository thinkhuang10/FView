using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class MsgForm : XtraForm
{
    private Label label1;

    private Panel panel1;

    public MsgForm()
    {
        InitializeComponent();
    }

    public void Say(string text)
    {
        try
        {
            if (CEditEnvironmentGlobal.MdiParent.dockPanel_输出栏.Visibility == DockVisibility.AutoHide && CEditEnvironmentGlobal.MdiParent.dockPanel_输出栏.DockManager.ActivePanel != CEditEnvironmentGlobal.MdiParent.dockPanel_输出栏)
            {
                CEditEnvironmentGlobal.MdiParent.dockPanel_输出栏.Show();
            }
            CEditEnvironmentGlobal.MdiParent.richTextBox1.AppendText(string.Concat(DateTime.Now, " (", DateTime.Now.Millisecond.ToString("D3"), ") --- ", text, "\n"));
            CEditEnvironmentGlobal.MdiParent.richTextBox1.Refresh();
            CEditEnvironmentGlobal.MdiParent.richTextBox1.ScrollToCaret();
        }
        catch
        {
        }
    }

    public void Clear()
    {
        CEditEnvironmentGlobal.MdiParent.richTextBox1.Clear();
    }

    public new void Show()
    {
    }

    private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
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
        panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(panel1_MouseDoubleClick);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.DarkGray;
        base.ClientSize = new System.Drawing.Size(270, 73);
        base.Controls.Add(panel1);
        DoubleBuffered = true;
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        base.Name = "MsgForm";
        base.ShowInTaskbar = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "InitForm";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        base.ResumeLayout(false);
    }
}
