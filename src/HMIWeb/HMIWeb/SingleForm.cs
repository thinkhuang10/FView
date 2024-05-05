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
        ok = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button7 = new System.Windows.Forms.Button();
        button8 = new System.Windows.Forms.Button();
        button9 = new System.Windows.Forms.Button();
        button10 = new System.Windows.Forms.Button();
        button11 = new System.Windows.Forms.Button();
        button12 = new System.Windows.Forms.Button();
        button14 = new System.Windows.Forms.Button();
        button15 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        ok.Location = new System.Drawing.Point(234, 13);
        ok.Name = "ok";
        ok.Size = new System.Drawing.Size(99, 45);
        ok.TabIndex = 1;
        ok.Text = "确定";
        ok.UseVisualStyleBackColor = true;
        ok.Click += new System.EventHandler(button1_Click);
        textBox1.Location = new System.Drawing.Point(16, 37);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(208, 21);
        textBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(14, 13);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 2;
        label1.Text = "提示";
        button1.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button1.Location = new System.Drawing.Point(16, 104);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(99, 69);
        button1.TabIndex = 3;
        button1.Text = "7";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(SoftKey_Click);
        button2.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button2.Location = new System.Drawing.Point(125, 104);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(99, 69);
        button2.TabIndex = 3;
        button2.Text = "8";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(SoftKey_Click);
        button3.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button3.Location = new System.Drawing.Point(234, 104);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(99, 69);
        button3.TabIndex = 3;
        button3.Text = "9";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(SoftKey_Click);
        button4.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button4.Location = new System.Drawing.Point(16, 180);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(99, 69);
        button4.TabIndex = 3;
        button4.Text = "4";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(SoftKey_Click);
        button5.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button5.Location = new System.Drawing.Point(125, 180);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(99, 69);
        button5.TabIndex = 3;
        button5.Text = "5";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(SoftKey_Click);
        button6.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button6.Location = new System.Drawing.Point(234, 180);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(99, 69);
        button6.TabIndex = 3;
        button6.Text = "6";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(SoftKey_Click);
        button7.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button7.Location = new System.Drawing.Point(16, 256);
        button7.Name = "button7";
        button7.Size = new System.Drawing.Size(99, 69);
        button7.TabIndex = 3;
        button7.Text = "1";
        button7.UseVisualStyleBackColor = true;
        button7.Click += new System.EventHandler(SoftKey_Click);
        button8.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button8.Location = new System.Drawing.Point(125, 256);
        button8.Name = "button8";
        button8.Size = new System.Drawing.Size(99, 69);
        button8.TabIndex = 3;
        button8.Text = "2";
        button8.UseVisualStyleBackColor = true;
        button8.Click += new System.EventHandler(SoftKey_Click);
        button9.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button9.Location = new System.Drawing.Point(234, 256);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(99, 69);
        button9.TabIndex = 3;
        button9.Text = "3";
        button9.UseVisualStyleBackColor = true;
        button9.Click += new System.EventHandler(SoftKey_Click);
        button10.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button10.Location = new System.Drawing.Point(16, 332);
        button10.Name = "button10";
        button10.Size = new System.Drawing.Size(99, 69);
        button10.TabIndex = 3;
        button10.Text = "0";
        button10.UseVisualStyleBackColor = true;
        button10.Click += new System.EventHandler(SoftKey_Click);
        button11.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button11.Location = new System.Drawing.Point(125, 332);
        button11.Name = "button11";
        button11.Size = new System.Drawing.Size(99, 69);
        button11.TabIndex = 3;
        button11.Text = ".";
        button11.UseVisualStyleBackColor = true;
        button11.Click += new System.EventHandler(SoftKey_Click);
        button12.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button12.Location = new System.Drawing.Point(234, 332);
        button12.Name = "button12";
        button12.Size = new System.Drawing.Size(99, 69);
        button12.TabIndex = 3;
        button12.Text = "-";
        button12.UseVisualStyleBackColor = true;
        button12.Click += new System.EventHandler(SoftKey_Click);
        button14.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button14.Location = new System.Drawing.Point(177, 64);
        button14.Name = "button14";
        button14.Size = new System.Drawing.Size(156, 35);
        button14.TabIndex = 3;
        button14.Text = "删除";
        button14.UseVisualStyleBackColor = true;
        button14.Click += new System.EventHandler(button14_Click);
        button15.Font = new System.Drawing.Font("黑体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        button15.Location = new System.Drawing.Point(16, 64);
        button15.Name = "button15";
        button15.Size = new System.Drawing.Size(156, 35);
        button15.TabIndex = 3;
        button15.Text = "清空";
        button15.UseVisualStyleBackColor = true;
        button15.Click += new System.EventHandler(button15_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(348, 411);
        base.Controls.Add(button9);
        base.Controls.Add(button12);
        base.Controls.Add(button11);
        base.Controls.Add(button8);
        base.Controls.Add(button6);
        base.Controls.Add(button5);
        base.Controls.Add(button10);
        base.Controls.Add(button7);
        base.Controls.Add(button3);
        base.Controls.Add(button4);
        base.Controls.Add(button2);
        base.Controls.Add(button15);
        base.Controls.Add(button14);
        base.Controls.Add(button1);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(ok);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "SingleForm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "模拟量输入";
        base.Load += new System.EventHandler(SingleForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SingleForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
