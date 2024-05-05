using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ProjectIOGroupEditForm : XtraForm
{
    public string str = "";

    private Button button1;

    private Button button2;

    private Label label1;

    private TextBox textBox1;

    public ProjectIOGroupEditForm()
    {
        InitializeComponent();
    }

    public ProjectIOGroupEditForm(string _str)
    {
        InitializeComponent();
        textBox1.Text = _str;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            MessageBox.Show("输入名称为空,请从新输入.");
            return;
        }
        str = textBox1.Text;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(28, 80);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 1;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(150, 80);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 2;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(36, 29);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(31, 14);
        label1.TabIndex = 2;
        label1.Text = "名称";
        textBox1.Location = new System.Drawing.Point(77, 26);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(135, 22);
        textBox1.TabIndex = 0;
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(266, 134);
        base.Controls.Add(textBox1);
        base.Controls.Add(label1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ProjectIOGroupEditForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "组管理";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
