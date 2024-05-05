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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(21, 68);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 1;
        button1.Text = "修改名称";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(124, 68);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 2;
        button2.Text = "忽略变量";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.Location = new System.Drawing.Point(67, 26);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(153, 22);
        textBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(18, 29);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(43, 14);
        label1.TabIndex = 3;
        label1.Text = "重命名";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(251, 123);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "renameProjectIOForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "内部变量冲突解决";
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
