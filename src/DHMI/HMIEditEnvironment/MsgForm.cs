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
            if (CEditEnvironmentGlobal.mdiparent.dockPanel_输出栏.Visibility == DockVisibility.AutoHide && CEditEnvironmentGlobal.mdiparent.dockPanel_输出栏.DockManager.ActivePanel != CEditEnvironmentGlobal.mdiparent.dockPanel_输出栏)
            {
                CEditEnvironmentGlobal.mdiparent.dockPanel_输出栏.Show();
            }
            CEditEnvironmentGlobal.mdiparent.richTextBox1.AppendText(string.Concat(DateTime.Now, " (", DateTime.Now.Millisecond.ToString("D3"), ") --- ", text, "\n"));
            CEditEnvironmentGlobal.mdiparent.richTextBox1.Refresh();
            CEditEnvironmentGlobal.mdiparent.richTextBox1.ScrollToCaret();
        }
        catch
        {
        }
    }

    public void Clear()
    {
        CEditEnvironmentGlobal.mdiparent.richTextBox1.Clear();
    }

    public new void Show()
    {
    }

    private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
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
        this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(panel1_MouseDoubleClick);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DarkGray;
        base.ClientSize = new System.Drawing.Size(270, 73);
        base.Controls.Add(this.panel1);
        this.DoubleBuffered = true;
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        base.Name = "MsgForm";
        base.ShowInTaskbar = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "InitForm";
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        base.ResumeLayout(false);
    }
}
