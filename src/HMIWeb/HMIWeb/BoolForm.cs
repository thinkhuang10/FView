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
        this.button1 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(207, 56);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "取消";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.textBox1.Location = new System.Drawing.Point(65, 160);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(125, 21);
        this.textBox1.TabIndex = 1;
        this.textBox1.Visible = false;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 2;
        this.label1.Text = "提示：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(124, 146);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "label2";
        this.label2.Visible = false;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(241, 146);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 4;
        this.label3.Text = "label3";
        this.label3.Visible = false;
        this.button2.Location = new System.Drawing.Point(15, 56);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 5;
        this.button2.Text = "button2";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button3.Location = new System.Drawing.Point(96, 56);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 6;
        this.button3.Text = "button3";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(294, 100);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "BoolForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "开关量输入";
        base.Load += new System.EventHandler(BoolForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(BoolForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
