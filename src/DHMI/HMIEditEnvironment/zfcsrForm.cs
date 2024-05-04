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
        if (textBox1.Text == "")
        {
            theglobal.SelectedShapeList[0].zfcsr = false;
            Close();
            return;
        }
        foreach (string iOItem in CheckIOExists.IOItemList)
        {
            if (!(iOItem == textBox1.Text))
            {
                continue;
            }
            goto IL_0084;
        }
        MessageBox.Show("输入的变量有误,请从新输入");
        return;
    IL_0084:
        try
        {
            theglobal.SelectedShapeList[0].zfcsrbianliang = textBox1.Text;
            theglobal.SelectedShapeList[0].zfcsrtishi = textBox2.Text;
            if (!theglobal.SelectedShapeList[0].zfcsr && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].zfcsr = true;
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception)
        {
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void textBox1_Click(object sender, EventArgs e)
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

    private void zfcsrForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox1.Text = theglobal.SelectedShapeList[0].zfcsrbianliang;
            textBox2.Text = theglobal.SelectedShapeList[0].zfcsrtishi;
        }
        catch (Exception)
        {
        }
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
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(142, 97);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 3;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(235, 97);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 4;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(26, 26);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(47, 14);
        this.label1.TabIndex = 2;
        this.label1.Text = "变    量";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(26, 57);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(55, 14);
        this.label2.TabIndex = 3;
        this.label2.Text = "提示信息";
        this.textBox1.Location = new System.Drawing.Point(99, 22);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(117, 22);
        this.textBox1.TabIndex = 0;
        this.textBox1.Click += new System.EventHandler(textBox1_Click);
        this.textBox2.Location = new System.Drawing.Point(99, 54);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(210, 22);
        this.textBox2.TabIndex = 2;
        this.textBox2.Text = "字符串输入";
        this.button3.Location = new System.Drawing.Point(222, 20);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "选择变量";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(17, 86);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(300, 1);
        this.pictureBox1.TabIndex = 8;
        this.pictureBox1.TabStop = false;
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(334, 136);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "zfcsrForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "字符串输入";
        base.Load += new System.EventHandler(zfcsrForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
