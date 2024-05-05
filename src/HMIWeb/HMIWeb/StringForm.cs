using System;
using System.Windows.Forms;

namespace HMIWeb;

public class StringForm : Form
{
    public string value;

    private Button button1;

    private TextBox textBox1;

    private Label label1;

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

    private Button button13;

    private Button button14;

    private Button button15;

    private Button button16;

    private Button button17;

    private Button button18;

    private Button button19;

    private Button button20;

    private Button button21;

    private Button button23;

    private Button button24;

    private Button button25;

    private Button button26;

    private Button button27;

    private Button button28;

    private Button button29;

    private Button button30;

    private Button button31;

    private Button button22;

    private Button button32;

    private Button button33;

    private Button button34;

    private Button button35;

    private Button button36;

    private Button button37;

    private Button button38;

    public StringForm(string tag)
    {
        InitializeComponent();
        label1.Text = tag;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        value = textBox1.Text;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void StringForm_Load(object sender, EventArgs e)
    {
        if (value != null)
        {
            textBox1.Text = value.ToString();
        }
        textBox1.SelectAll();
    }

    private void StringForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (textBox1.Text == "")
        {
            value = null;
        }
    }

    private void SoftKey_Click(object sender, EventArgs e)
    {
        textBox1.Text += ((Control)sender).Text;
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
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
        button13 = new System.Windows.Forms.Button();
        button14 = new System.Windows.Forms.Button();
        button15 = new System.Windows.Forms.Button();
        button16 = new System.Windows.Forms.Button();
        button17 = new System.Windows.Forms.Button();
        button18 = new System.Windows.Forms.Button();
        button19 = new System.Windows.Forms.Button();
        button20 = new System.Windows.Forms.Button();
        button21 = new System.Windows.Forms.Button();
        button23 = new System.Windows.Forms.Button();
        button24 = new System.Windows.Forms.Button();
        button25 = new System.Windows.Forms.Button();
        button26 = new System.Windows.Forms.Button();
        button27 = new System.Windows.Forms.Button();
        button28 = new System.Windows.Forms.Button();
        button29 = new System.Windows.Forms.Button();
        button30 = new System.Windows.Forms.Button();
        button31 = new System.Windows.Forms.Button();
        button22 = new System.Windows.Forms.Button();
        button32 = new System.Windows.Forms.Button();
        button33 = new System.Windows.Forms.Button();
        button34 = new System.Windows.Forms.Button();
        button35 = new System.Windows.Forms.Button();
        button36 = new System.Windows.Forms.Button();
        button37 = new System.Windows.Forms.Button();
        button38 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(235, 36);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        textBox1.Location = new System.Drawing.Point(12, 38);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(217, 21);
        textBox1.TabIndex = 1;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 14);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(35, 12);
        label1.TabIndex = 2;
        label1.Text = "提示:";
        button2.Location = new System.Drawing.Point(24, 78);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(23, 23);
        button2.TabIndex = 3;
        button2.Text = "1";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(SoftKey_Click);
        button3.Location = new System.Drawing.Point(50, 78);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(23, 23);
        button3.TabIndex = 3;
        button3.Text = "2";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(SoftKey_Click);
        button4.Location = new System.Drawing.Point(76, 78);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(23, 23);
        button4.TabIndex = 3;
        button4.Text = "3";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(SoftKey_Click);
        button5.Location = new System.Drawing.Point(102, 78);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(23, 23);
        button5.TabIndex = 3;
        button5.Text = "4";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(SoftKey_Click);
        button6.Location = new System.Drawing.Point(128, 78);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(23, 23);
        button6.TabIndex = 3;
        button6.Text = "5";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(SoftKey_Click);
        button7.Location = new System.Drawing.Point(154, 78);
        button7.Name = "button7";
        button7.Size = new System.Drawing.Size(23, 23);
        button7.TabIndex = 3;
        button7.Text = "6";
        button7.UseVisualStyleBackColor = true;
        button7.Click += new System.EventHandler(SoftKey_Click);
        button8.Location = new System.Drawing.Point(180, 78);
        button8.Name = "button8";
        button8.Size = new System.Drawing.Size(23, 23);
        button8.TabIndex = 3;
        button8.Text = "7";
        button8.UseVisualStyleBackColor = true;
        button8.Click += new System.EventHandler(SoftKey_Click);
        button9.Location = new System.Drawing.Point(206, 78);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(23, 23);
        button9.TabIndex = 3;
        button9.Text = "8";
        button9.UseVisualStyleBackColor = true;
        button9.Click += new System.EventHandler(SoftKey_Click);
        button10.Location = new System.Drawing.Point(232, 78);
        button10.Name = "button10";
        button10.Size = new System.Drawing.Size(23, 23);
        button10.TabIndex = 3;
        button10.Text = "9";
        button10.UseVisualStyleBackColor = true;
        button10.Click += new System.EventHandler(SoftKey_Click);
        button11.Location = new System.Drawing.Point(258, 78);
        button11.Name = "button11";
        button11.Size = new System.Drawing.Size(23, 23);
        button11.TabIndex = 3;
        button11.Text = "0";
        button11.UseVisualStyleBackColor = true;
        button11.Click += new System.EventHandler(SoftKey_Click);
        button12.Location = new System.Drawing.Point(24, 107);
        button12.Name = "button12";
        button12.Size = new System.Drawing.Size(23, 23);
        button12.TabIndex = 3;
        button12.Text = "q";
        button12.UseVisualStyleBackColor = true;
        button12.Click += new System.EventHandler(SoftKey_Click);
        button13.Location = new System.Drawing.Point(50, 107);
        button13.Name = "button13";
        button13.Size = new System.Drawing.Size(23, 23);
        button13.TabIndex = 3;
        button13.Text = "w";
        button13.UseVisualStyleBackColor = true;
        button13.Click += new System.EventHandler(SoftKey_Click);
        button14.Location = new System.Drawing.Point(76, 107);
        button14.Name = "button14";
        button14.Size = new System.Drawing.Size(23, 23);
        button14.TabIndex = 3;
        button14.Text = "e";
        button14.UseVisualStyleBackColor = true;
        button14.Click += new System.EventHandler(SoftKey_Click);
        button15.Location = new System.Drawing.Point(102, 107);
        button15.Name = "button15";
        button15.Size = new System.Drawing.Size(23, 23);
        button15.TabIndex = 3;
        button15.Text = "r";
        button15.UseVisualStyleBackColor = true;
        button15.Click += new System.EventHandler(SoftKey_Click);
        button16.Location = new System.Drawing.Point(128, 107);
        button16.Name = "button16";
        button16.Size = new System.Drawing.Size(23, 23);
        button16.TabIndex = 3;
        button16.Text = "t";
        button16.UseVisualStyleBackColor = true;
        button16.Click += new System.EventHandler(SoftKey_Click);
        button17.Location = new System.Drawing.Point(154, 107);
        button17.Name = "button17";
        button17.Size = new System.Drawing.Size(23, 23);
        button17.TabIndex = 3;
        button17.Text = "y";
        button17.UseVisualStyleBackColor = true;
        button17.Click += new System.EventHandler(SoftKey_Click);
        button18.Location = new System.Drawing.Point(180, 107);
        button18.Name = "button18";
        button18.Size = new System.Drawing.Size(23, 23);
        button18.TabIndex = 3;
        button18.Text = "ui";
        button18.UseVisualStyleBackColor = true;
        button18.Click += new System.EventHandler(SoftKey_Click);
        button19.Location = new System.Drawing.Point(206, 107);
        button19.Name = "button19";
        button19.Size = new System.Drawing.Size(23, 23);
        button19.TabIndex = 3;
        button19.Text = "i";
        button19.UseVisualStyleBackColor = true;
        button19.Click += new System.EventHandler(SoftKey_Click);
        button20.Location = new System.Drawing.Point(232, 107);
        button20.Name = "button20";
        button20.Size = new System.Drawing.Size(23, 23);
        button20.TabIndex = 3;
        button20.Text = "o";
        button20.UseVisualStyleBackColor = true;
        button20.Click += new System.EventHandler(SoftKey_Click);
        button21.Location = new System.Drawing.Point(258, 107);
        button21.Name = "button21";
        button21.Size = new System.Drawing.Size(23, 23);
        button21.TabIndex = 3;
        button21.Text = "p";
        button21.UseVisualStyleBackColor = true;
        button21.Click += new System.EventHandler(SoftKey_Click);
        button23.Location = new System.Drawing.Point(245, 136);
        button23.Name = "button23";
        button23.Size = new System.Drawing.Size(23, 23);
        button23.TabIndex = 9;
        button23.Text = "l";
        button23.UseVisualStyleBackColor = true;
        button23.Click += new System.EventHandler(SoftKey_Click);
        button24.Location = new System.Drawing.Point(219, 136);
        button24.Name = "button24";
        button24.Size = new System.Drawing.Size(23, 23);
        button24.TabIndex = 11;
        button24.Text = "k";
        button24.UseVisualStyleBackColor = true;
        button24.Click += new System.EventHandler(SoftKey_Click);
        button25.Location = new System.Drawing.Point(193, 136);
        button25.Name = "button25";
        button25.Size = new System.Drawing.Size(23, 23);
        button25.TabIndex = 13;
        button25.Text = "j";
        button25.UseVisualStyleBackColor = true;
        button25.Click += new System.EventHandler(SoftKey_Click);
        button26.Location = new System.Drawing.Point(167, 136);
        button26.Name = "button26";
        button26.Size = new System.Drawing.Size(23, 23);
        button26.TabIndex = 12;
        button26.Text = "h";
        button26.UseVisualStyleBackColor = true;
        button26.Click += new System.EventHandler(SoftKey_Click);
        button27.Location = new System.Drawing.Point(141, 136);
        button27.Name = "button27";
        button27.Size = new System.Drawing.Size(23, 23);
        button27.TabIndex = 5;
        button27.Text = "g";
        button27.UseVisualStyleBackColor = true;
        button27.Click += new System.EventHandler(SoftKey_Click);
        button28.Location = new System.Drawing.Point(115, 136);
        button28.Name = "button28";
        button28.Size = new System.Drawing.Size(23, 23);
        button28.TabIndex = 4;
        button28.Text = "f";
        button28.UseVisualStyleBackColor = true;
        button28.Click += new System.EventHandler(SoftKey_Click);
        button29.Location = new System.Drawing.Point(89, 136);
        button29.Name = "button29";
        button29.Size = new System.Drawing.Size(23, 23);
        button29.TabIndex = 6;
        button29.Text = "d";
        button29.UseVisualStyleBackColor = true;
        button29.Click += new System.EventHandler(SoftKey_Click);
        button30.Location = new System.Drawing.Point(63, 136);
        button30.Name = "button30";
        button30.Size = new System.Drawing.Size(23, 23);
        button30.TabIndex = 8;
        button30.Text = "s";
        button30.UseVisualStyleBackColor = true;
        button30.Click += new System.EventHandler(SoftKey_Click);
        button31.Location = new System.Drawing.Point(37, 136);
        button31.Name = "button31";
        button31.Size = new System.Drawing.Size(23, 23);
        button31.TabIndex = 7;
        button31.Text = "a";
        button31.UseVisualStyleBackColor = true;
        button31.Click += new System.EventHandler(SoftKey_Click);
        button22.Location = new System.Drawing.Point(50, 165);
        button22.Name = "button22";
        button22.Size = new System.Drawing.Size(23, 23);
        button22.TabIndex = 3;
        button22.Text = "z";
        button22.UseVisualStyleBackColor = true;
        button22.Click += new System.EventHandler(SoftKey_Click);
        button32.Location = new System.Drawing.Point(76, 165);
        button32.Name = "button32";
        button32.Size = new System.Drawing.Size(23, 23);
        button32.TabIndex = 3;
        button32.Text = "x";
        button32.UseVisualStyleBackColor = true;
        button32.Click += new System.EventHandler(SoftKey_Click);
        button33.Location = new System.Drawing.Point(102, 165);
        button33.Name = "button33";
        button33.Size = new System.Drawing.Size(23, 23);
        button33.TabIndex = 3;
        button33.Text = "c";
        button33.UseVisualStyleBackColor = true;
        button33.Click += new System.EventHandler(SoftKey_Click);
        button34.Location = new System.Drawing.Point(128, 165);
        button34.Name = "button34";
        button34.Size = new System.Drawing.Size(23, 23);
        button34.TabIndex = 3;
        button34.Text = "v";
        button34.UseVisualStyleBackColor = true;
        button34.Click += new System.EventHandler(SoftKey_Click);
        button35.Location = new System.Drawing.Point(154, 165);
        button35.Name = "button35";
        button35.Size = new System.Drawing.Size(23, 23);
        button35.TabIndex = 3;
        button35.Text = "b";
        button35.UseVisualStyleBackColor = true;
        button35.Click += new System.EventHandler(SoftKey_Click);
        button36.Location = new System.Drawing.Point(180, 165);
        button36.Name = "button36";
        button36.Size = new System.Drawing.Size(23, 23);
        button36.TabIndex = 3;
        button36.Text = "n";
        button36.UseVisualStyleBackColor = true;
        button36.Click += new System.EventHandler(SoftKey_Click);
        button37.Location = new System.Drawing.Point(206, 165);
        button37.Name = "button37";
        button37.Size = new System.Drawing.Size(23, 23);
        button37.TabIndex = 3;
        button37.Text = "m";
        button37.UseVisualStyleBackColor = true;
        button37.Click += new System.EventHandler(SoftKey_Click);
        button38.Location = new System.Drawing.Point(232, 165);
        button38.Name = "button38";
        button38.Size = new System.Drawing.Size(23, 23);
        button38.TabIndex = 9;
        button38.Text = ".";
        button38.UseVisualStyleBackColor = true;
        button38.Click += new System.EventHandler(SoftKey_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(324, 73);
        base.Controls.Add(button38);
        base.Controls.Add(button23);
        base.Controls.Add(button24);
        base.Controls.Add(button25);
        base.Controls.Add(button26);
        base.Controls.Add(button27);
        base.Controls.Add(button28);
        base.Controls.Add(button29);
        base.Controls.Add(button30);
        base.Controls.Add(button31);
        base.Controls.Add(button21);
        base.Controls.Add(button20);
        base.Controls.Add(button11);
        base.Controls.Add(button19);
        base.Controls.Add(button37);
        base.Controls.Add(button10);
        base.Controls.Add(button18);
        base.Controls.Add(button36);
        base.Controls.Add(button9);
        base.Controls.Add(button17);
        base.Controls.Add(button35);
        base.Controls.Add(button8);
        base.Controls.Add(button16);
        base.Controls.Add(button34);
        base.Controls.Add(button7);
        base.Controls.Add(button15);
        base.Controls.Add(button33);
        base.Controls.Add(button6);
        base.Controls.Add(button14);
        base.Controls.Add(button32);
        base.Controls.Add(button5);
        base.Controls.Add(button13);
        base.Controls.Add(button22);
        base.Controls.Add(button4);
        base.Controls.Add(button12);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "StringForm";
        Text = "字符量输入";
        base.Load += new System.EventHandler(StringForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(StringForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
