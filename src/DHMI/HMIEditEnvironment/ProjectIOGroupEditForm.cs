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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(28, 80);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 1;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(150, 80);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 2;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(36, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(31, 14);
        this.label1.TabIndex = 2;
        this.label1.Text = "名称";
        this.textBox1.Location = new System.Drawing.Point(77, 26);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(135, 22);
        this.textBox1.TabIndex = 0;
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(266, 134);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ProjectIOGroupEditForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "组管理";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
