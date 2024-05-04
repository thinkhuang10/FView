using System;
using System.Windows.Forms;

namespace HMIWeb;

public class SingleForm : Form
{
    public float? value;

    private readonly double max;

    private readonly double min;

    private Button ok;

    private TextBox textBox1;

    private Label label1;

    private Button button1;

    private Button button2;

    private Button button3;

    private Button button4;

    private Button button5;

    private Button button6;

    private Button button7;

    private Button button8;

    private Button button9;

    private Button button10;

    private Button button11;

    private Button button12;

    private Button button14;

    private Button button15;

    public SingleForm(string tag, double maxvalue, double minvalue)
    {
        InitializeComponent();
        base.AcceptButton = ok;
        max = maxvalue;
        min = minvalue;
        label1.Text = tag;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            value = Convert.ToSingle(textBox1.Text);
            if ((double?)value < min || (double?)value > max)
            {
                MessageBox.Show("请输入[" + min + "," + max + "]之间数值.");
                value = null;
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            value = null;
        }
        Close();
    }

    private void SingleForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (textBox1.Text == "")
        {
            value = null;
        }
    }

    private void SoftKey_Click(object sender, EventArgs e)
    {
        int selectionStart = textBox1.SelectionStart;
        string text = textBox1.Text.Remove(selectionStart, textBox1.SelectionLength);
        text = text.Insert(selectionStart, ((Control)sender).Text);
        textBox1.Text = text;
        textBox1.SelectionStart = selectionStart + ((Control)sender).Text.Length;
    }

    private void SingleForm_Load(object sender, EventArgs e)
    {
        if (value.HasValue)
        {
            textBox1.Text = value.ToString();
        }
        textBox1.SelectAll();
    }

    private void button15_Click(object sender, EventArgs e)
    {
        textBox1.Text = "";
    }

    private void button14_Click(object sender, EventArgs e)
    {
        if (textBox1.SelectionLength == 0)
        {
            int selectionStart = textBox1.SelectionStart;
            if (selectionStart != 0)
            {
                string text = textBox1.Text.Remove(selectionStart - 1, 1);
                textBox1.Text = text;
                textBox1.SelectionStart = selectionStart - 1;
            }
        }
        else
        {
            int selectionStart2 = textBox1.SelectionStart;
            string text2 = textBox1.Text.Remove(selectionStart2, textBox1.SelectionLength);
            textBox1.Text = text2;
            textBox1.SelectionStart = selectionStart2;
        }
    }

    private void InitializeComponent()
    {
        this.ok = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.button10 = new System.Windows.Forms.Button();
        this.button11 = new System.Windows.Forms.Button();
        this.button12 = new System.Windows.Forms.Button();
        this.button14 = new System.Windows.Forms.Button();
        this.button15 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.ok.Location = new System.Drawing.Point(234, 13);
        this.ok.Name = "ok";
        this.ok.Size = new System.Drawing.Size(99, 45);
        this.ok.TabIndex = 1;
        this.ok.Text = "确定";
        this.ok.UseVisualStyleBackColor = true;
        this.ok.Click += new System.EventHandler(button1_Click);
        this.textBox1.Location = new System.Drawing.Point(16, 37);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(208, 21);
        this.textBox1.TabIndex = 0;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(14, 13);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 2;
        this.label1.Text = "提示";
        this.button1.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button1.Location = new System.Drawing.Point(16, 104);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(99, 69);
        this.button1.TabIndex = 3;
        this.button1.Text = "7";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(SoftKey_Click);
        this.button2.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button2.Location = new System.Drawing.Point(125, 104);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(99, 69);
        this.button2.TabIndex = 3;
        this.button2.Text = "8";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(SoftKey_Click);
        this.button3.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button3.Location = new System.Drawing.Point(234, 104);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(99, 69);
        this.button3.TabIndex = 3;
        this.button3.Text = "9";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(SoftKey_Click);
        this.button4.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button4.Location = new System.Drawing.Point(16, 180);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(99, 69);
        this.button4.TabIndex = 3;
        this.button4.Text = "4";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(SoftKey_Click);
        this.button5.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button5.Location = new System.Drawing.Point(125, 180);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(99, 69);
        this.button5.TabIndex = 3;
        this.button5.Text = "5";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(SoftKey_Click);
        this.button6.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button6.Location = new System.Drawing.Point(234, 180);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(99, 69);
        this.button6.TabIndex = 3;
        this.button6.Text = "6";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(SoftKey_Click);
        this.button7.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button7.Location = new System.Drawing.Point(16, 256);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(99, 69);
        this.button7.TabIndex = 3;
        this.button7.Text = "1";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(SoftKey_Click);
        this.button8.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button8.Location = new System.Drawing.Point(125, 256);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(99, 69);
        this.button8.TabIndex = 3;
        this.button8.Text = "2";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(SoftKey_Click);
        this.button9.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button9.Location = new System.Drawing.Point(234, 256);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(99, 69);
        this.button9.TabIndex = 3;
        this.button9.Text = "3";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(SoftKey_Click);
        this.button10.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button10.Location = new System.Drawing.Point(16, 332);
        this.button10.Name = "button10";
        this.button10.Size = new System.Drawing.Size(99, 69);
        this.button10.TabIndex = 3;
        this.button10.Text = "0";
        this.button10.UseVisualStyleBackColor = true;
        this.button10.Click += new System.EventHandler(SoftKey_Click);
        this.button11.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button11.Location = new System.Drawing.Point(125, 332);
        this.button11.Name = "button11";
        this.button11.Size = new System.Drawing.Size(99, 69);
        this.button11.TabIndex = 3;
        this.button11.Text = ".";
        this.button11.UseVisualStyleBackColor = true;
        this.button11.Click += new System.EventHandler(SoftKey_Click);
        this.button12.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button12.Location = new System.Drawing.Point(234, 332);
        this.button12.Name = "button12";
        this.button12.Size = new System.Drawing.Size(99, 69);
        this.button12.TabIndex = 3;
        this.button12.Text = "-";
        this.button12.UseVisualStyleBackColor = true;
        this.button12.Click += new System.EventHandler(SoftKey_Click);
        this.button14.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button14.Location = new System.Drawing.Point(177, 64);
        this.button14.Name = "button14";
        this.button14.Size = new System.Drawing.Size(156, 35);
        this.button14.TabIndex = 3;
        this.button14.Text = "删除";
        this.button14.UseVisualStyleBackColor = true;
        this.button14.Click += new System.EventHandler(button14_Click);
        this.button15.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        this.button15.Location = new System.Drawing.Point(16, 64);
        this.button15.Name = "button15";
        this.button15.Size = new System.Drawing.Size(156, 35);
        this.button15.TabIndex = 3;
        this.button15.Text = "清空";
        this.button15.UseVisualStyleBackColor = true;
        this.button15.Click += new System.EventHandler(button15_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(348, 411);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button12);
        base.Controls.Add(this.button11);
        base.Controls.Add(this.button8);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button10);
        base.Controls.Add(this.button7);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button15);
        base.Controls.Add(this.button14);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.ok);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "SingleForm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "模拟量输入";
        base.Load += new System.EventHandler(SingleForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SingleForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
