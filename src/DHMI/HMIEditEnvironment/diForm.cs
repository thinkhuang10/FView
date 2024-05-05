using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class diForm : XtraForm
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

    public diForm(CGlobal _theglobal)
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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.button3 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(77, 129);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 6;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(Button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(231, 129);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 7;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.textBox1.Location = new System.Drawing.Point(103, 22);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(154, 22);
        this.textBox1.TabIndex = 0;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(42, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(55, 14);
        this.label1.TabIndex = 3;
        this.label1.Text = "变      量";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(238, 89);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(19, 14);
        this.label2.TabIndex = 4;
        this.label2.Text = "关";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(102, 89);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(19, 14);
        this.label3.TabIndex = 5;
        this.label3.Text = "开";
        this.textBox2.Location = new System.Drawing.Point(103, 55);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(154, 22);
        this.textBox2.TabIndex = 2;
        this.textBox2.Text = "数字量输入";
        this.textBox3.Location = new System.Drawing.Point(127, 86);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(87, 22);
        this.textBox3.TabIndex = 4;
        this.textBox3.Text = "打开";
        this.textBox4.Location = new System.Drawing.Point(263, 86);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(87, 22);
        this.textBox4.TabIndex = 5;
        this.textBox4.Text = "关闭";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(42, 59);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 9;
        this.label4.Text = "提示信息";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(44, 89);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(31, 14);
        this.label5.TabIndex = 10;
        this.label5.Text = "提示";
        this.button3.Location = new System.Drawing.Point(263, 19);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(394, 176);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.label5);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.textBox4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "diForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "数字量输入";
        base.Load += new System.EventHandler(diForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
