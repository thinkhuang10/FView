using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class aiForm : Form
{
    private readonly CGlobal theglobal;

    private Label label1;

    private TextBox textBox1;

    private Label label2;

    private TextBox textBox2;

    private Button button1;

    private Button button2;

    private Button button3;

    private TextBox textBox3;

    private Label label3;

    private TextBox textBox4;

    private Label label4;

    public aiForm(CGlobal _theglobal)
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
                theglobal.SelectedShapeList[0].ai = false;
                theglobal.SelectedShapeList[0].aibianliang = "";
                Close();
                return;
            }
            if (Convert.ToDouble(textBox3.Text) > Convert.ToDouble(textBox4.Text))
            {
                MessageBox.Show("区间[" + textBox3.Text + "," + textBox4.Text + "]输入有误,请从新输入.");
                return;
            }

            theglobal.SelectedShapeList[0].aibianliang = textBox1.Text;
            theglobal.SelectedShapeList[0].aitishi = textBox2.Text;
            theglobal.SelectedShapeList[0].aimax = Convert.ToDouble(textBox4.Text);
            theglobal.SelectedShapeList[0].aimin = Convert.ToDouble(textBox3.Text);
            if (!theglobal.SelectedShapeList[0].ai && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].ai = true;
            }

            DialogResult = DialogResult.OK;
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

    private void aiForm_Load(object sender, EventArgs e)
    {
        textBox1.Text = theglobal.SelectedShapeList[0].aibianliang;
        textBox2.Text = theglobal.SelectedShapeList[0].aitishi;
        textBox3.Text = theglobal.SelectedShapeList[0].aimin.ToString();
        textBox4.Text = theglobal.SelectedShapeList[0].aimax.ToString();
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
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        textBox3 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        textBox4 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(23, 22);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "变    量";
        textBox1.Location = new System.Drawing.Point(88, 18);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(133, 21);
        textBox1.TabIndex = 0;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(23, 50);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 3;
        label2.Text = "提示信息";
        textBox2.Location = new System.Drawing.Point(88, 46);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(133, 21);
        textBox2.TabIndex = 2;
        textBox2.Text = "模拟量输入";
        button1.Location = new System.Drawing.Point(82, 111);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 6;
        button1.Text = "确认";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(184, 111);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 7;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Location = new System.Drawing.Point(242, 17);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        textBox3.Location = new System.Drawing.Point(86, 73);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(71, 21);
        textBox3.TabIndex = 4;
        textBox3.Text = "-9999";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(23, 77);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(53, 12);
        label3.TabIndex = 6;
        label3.Text = "最 小 值";
        textBox4.Location = new System.Drawing.Point(230, 73);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(71, 21);
        textBox4.TabIndex = 5;
        textBox4.Text = "9999";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(167, 77);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(53, 12);
        label4.TabIndex = 8;
        label4.Text = "最 大 值";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(338, 151);
        base.Controls.Add(textBox4);
        base.Controls.Add(label4);
        base.Controls.Add(textBox3);
        base.Controls.Add(label3);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(textBox2);
        base.Controls.Add(label2);
        base.Controls.Add(textBox1);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "aiForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "模拟量输入";
        base.Load += new System.EventHandler(aiForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
