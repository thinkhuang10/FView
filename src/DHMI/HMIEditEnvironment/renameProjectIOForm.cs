using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class renameProjectIOForm : XtraForm
{
    private readonly ProjectIO pio;

    private Button button1;

    private Button button2;

    private TextBox textBox1;

    private Label label1;

    public renameProjectIOForm(ProjectIO _pio)
    {
        InitializeComponent();
        pio = _pio;
        textBox1.Text = pio.name;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            MessageBox.Show("变量名不能为空,请重新输入", "错误");
            return;
        }
        Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
        if (!regex.IsMatch(textBox1.Text))
        {
            MessageBox.Show("变量名不合法,请重新输入", "错误");
            return;
        }
        if (CheckScript.GetCSharpKeyStrs().Contains(textBox1.Text))
        {
            MessageBox.Show("名称中含有关键字可能会导致错误。");
            return;
        }
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            if (projectIO.name == textBox1.Text)
            {
                MessageBox.Show("变量" + textBox1.Text + "已经定义,请重新输入", "错误");
                return;
            }
        }
        pio.name = textBox1.Text;
        base.DialogResult = DialogResult.OK;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
    }

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(21, 68);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 1;
        this.button1.Text = "修改名称";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(124, 68);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 2;
        this.button2.Text = "忽略变量";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.textBox1.Location = new System.Drawing.Point(67, 26);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(153, 22);
        this.textBox1.TabIndex = 0;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(18, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(43, 14);
        this.label1.TabIndex = 3;
        this.label1.Text = "重命名";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(251, 123);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "renameProjectIOForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "内部变量冲突解决";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
