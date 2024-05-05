using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class zfcsrForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private Label label1;

    private Label label2;

    private TextBox textBox1;

    private TextBox textBox2;

    private Button button3;

    private PictureBox pictureBox1;


    public zfcsrForm(CGlobal _theglobal)
    {
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (textBox1.Text == "")
            {
                theglobal.SelectedShapeList[0].zfcsr = false;
                theglobal.SelectedShapeList[0].zfcsrbianliang = "";
                Close();
                return;
            }

            theglobal.SelectedShapeList[0].zfcsrbianliang = textBox1.Text;
            theglobal.SelectedShapeList[0].zfcsrtishi = textBox2.Text;
            if (!theglobal.SelectedShapeList[0].zfcsr && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].zfcsr = true;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ZfcsrForm_Load(object sender, EventArgs e)
    {
        textBox1.Text = theglobal.SelectedShapeList[0].zfcsrbianliang;
        textBox2.Text = theglobal.SelectedShapeList[0].zfcsrtishi;
    }

    private void Button3_Click(object sender, EventArgs e)
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
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(142, 97);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 3;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(235, 97);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 4;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(26, 26);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(47, 14);
        label1.TabIndex = 2;
        label1.Text = "变    量";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(26, 57);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(55, 14);
        label2.TabIndex = 3;
        label2.Text = "提示信息";
        textBox1.Location = new System.Drawing.Point(99, 22);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(117, 22);
        textBox1.TabIndex = 0;
        textBox2.Location = new System.Drawing.Point(99, 54);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(210, 22);
        textBox2.TabIndex = 2;
        textBox2.Text = "字符串输入";
        button3.Location = new System.Drawing.Point(222, 20);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "选择变量";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(Button3_Click);
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(17, 86);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(300, 1);
        pictureBox1.TabIndex = 8;
        pictureBox1.TabStop = false;
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(334, 136);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(button3);
        base.Controls.Add(textBox2);
        base.Controls.Add(textBox1);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "zfcsrForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "字符串输入";
        base.Load += new System.EventHandler(ZfcsrForm_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
