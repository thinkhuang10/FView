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
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(23, 22);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "变    量";
        this.textBox1.Location = new System.Drawing.Point(88, 18);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(133, 21);
        this.textBox1.TabIndex = 0;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(23, 50);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "提示信息";
        this.textBox2.Location = new System.Drawing.Point(88, 46);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(133, 21);
        this.textBox2.TabIndex = 2;
        this.textBox2.Text = "模拟量输入";
        this.button1.Location = new System.Drawing.Point(82, 111);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 6;
        this.button1.Text = "确认";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(184, 111);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 7;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button3.Location = new System.Drawing.Point(242, 17);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.textBox3.Location = new System.Drawing.Point(86, 73);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(71, 21);
        this.textBox3.TabIndex = 4;
        this.textBox3.Text = "-9999";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(23, 77);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(53, 12);
        this.label3.TabIndex = 6;
        this.label3.Text = "最 小 值";
        this.textBox4.Location = new System.Drawing.Point(230, 73);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(71, 21);
        this.textBox4.TabIndex = 5;
        this.textBox4.Text = "9999";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(167, 77);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(53, 12);
        this.label4.TabIndex = 8;
        this.label4.Text = "最 大 值";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(338, 151);
        base.Controls.Add(this.textBox4);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "aiForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "模拟量输入";
        base.Load += new System.EventHandler(aiForm_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
