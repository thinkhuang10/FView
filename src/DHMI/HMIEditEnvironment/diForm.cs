using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class DIForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private TextBox textBox1;

    private Label label1;

    private Label label2;

    private Label label3;

    private TextBox textBox2;

    private TextBox textBox3;

    private TextBox textBox4;

    private Label label4;

    private Label label5;

    private Button button3;

    public DIForm(CGlobal _theglobal)
    {
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        try
        { 
            if (textBox1.Text == "")
            {
                theglobal.SelectedShapeList[0].di = false;
                theglobal.SelectedShapeList[0].dibianlaing = "";
                Close();
                return;
            }

            theglobal.SelectedShapeList[0].dibianlaing = textBox1.Text;
            theglobal.SelectedShapeList[0].ditishi = textBox2.Text;
            theglobal.SelectedShapeList[0].ditishion = textBox3.Text;
            theglobal.SelectedShapeList[0].ditishioff = textBox4.Text;
            if (!theglobal.SelectedShapeList[0].di && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].di = true;
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void diForm_Load(object sender, EventArgs e)
    {
        textBox1.Text = theglobal.SelectedShapeList[0].dibianlaing;
        textBox2.Text = theglobal.SelectedShapeList[0].ditishi;
        textBox3.Text = theglobal.SelectedShapeList[0].ditishion;
        textBox4.Text = theglobal.SelectedShapeList[0].ditishioff;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        IOForm iOForm = new()
        {
            Edit = false
        };
        if (iOForm.ShowDialog() == DialogResult.OK)
        {
            textBox1.Text = iOForm.io;
        }
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        button3 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(77, 129);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 6;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(231, 129);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 7;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.Location = new System.Drawing.Point(103, 22);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(154, 22);
        textBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(42, 25);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(55, 14);
        label1.TabIndex = 3;
        label1.Text = "变      量";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(238, 89);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(19, 14);
        label2.TabIndex = 4;
        label2.Text = "关";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(102, 89);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(19, 14);
        label3.TabIndex = 5;
        label3.Text = "开";
        textBox2.Location = new System.Drawing.Point(103, 55);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(154, 22);
        textBox2.TabIndex = 2;
        textBox2.Text = "数字量输入";
        textBox3.Location = new System.Drawing.Point(127, 86);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(87, 22);
        textBox3.TabIndex = 4;
        textBox3.Text = "打开";
        textBox4.Location = new System.Drawing.Point(263, 86);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(87, 22);
        textBox4.TabIndex = 5;
        textBox4.Text = "关闭";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(42, 59);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 9;
        label4.Text = "提示信息";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(44, 89);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(31, 14);
        label5.TabIndex = 10;
        label5.Text = "提示";
        button3.Location = new System.Drawing.Point(263, 19);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(394, 176);
        base.Controls.Add(button3);
        base.Controls.Add(label5);
        base.Controls.Add(label4);
        base.Controls.Add(textBox4);
        base.Controls.Add(textBox3);
        base.Controls.Add(textBox2);
        base.Controls.Add(label3);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "diForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "数字量输入";
        base.Load += new System.EventHandler(diForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
