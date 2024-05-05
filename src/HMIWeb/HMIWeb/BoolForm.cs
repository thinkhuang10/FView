using System;
using System.Windows.Forms;

namespace HMIWeb;

public class BoolForm : Form
{
    public bool? value;

    private Button button1;

    private TextBox textBox1;

    private Label label1;

    private Label label2;

    private Label label3;

    private Button button2;

    private Button button3;

    public BoolForm(string tag1, string tag2, string tag3)
    {
        InitializeComponent();
        label1.Text = tag1;
        label2.Text = "1（开）:" + tag2;
        label3.Text = "0（关）:" + tag3;
        button2.Text = ((tag2 == "") ? "true" : tag2);
        button3.Text = ((tag3 == "") ? "false" : tag3);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        value = null;
        Close();
    }

    private void BoolForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (textBox1.Text == "")
        {
            value = null;
        }
    }

    private void BoolForm_Load(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
        textBox1.Text = "true";
        try
        {
            value = Convert.ToBoolean(textBox1.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            value = null;
        }
        Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        textBox1.Text = "false";
        try
        {
            value = Convert.ToBoolean(textBox1.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            value = null;
        }
        Close();
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(207, 56);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "取消";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        textBox1.Location = new System.Drawing.Point(65, 160);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(125, 21);
        textBox1.TabIndex = 1;
        textBox1.Visible = false;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(13, 13);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 12);
        label1.TabIndex = 2;
        label1.Text = "提示：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(124, 146);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(41, 12);
        label2.TabIndex = 3;
        label2.Text = "label2";
        label2.Visible = false;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(241, 146);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(41, 12);
        label3.TabIndex = 4;
        label3.Text = "label3";
        label3.Visible = false;
        button2.Location = new System.Drawing.Point(15, 56);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 5;
        button2.Text = "button2";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Location = new System.Drawing.Point(96, 56);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 6;
        button3.Text = "button3";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(294, 100);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(label3);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "BoolForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "开关量输入";
        base.Load += new System.EventHandler(BoolForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(BoolForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
