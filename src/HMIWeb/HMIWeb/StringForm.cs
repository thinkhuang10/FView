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
        this.button1 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
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
        this.button13 = new System.Windows.Forms.Button();
        this.button14 = new System.Windows.Forms.Button();
        this.button15 = new System.Windows.Forms.Button();
        this.button16 = new System.Windows.Forms.Button();
        this.button17 = new System.Windows.Forms.Button();
        this.button18 = new System.Windows.Forms.Button();
        this.button19 = new System.Windows.Forms.Button();
        this.button20 = new System.Windows.Forms.Button();
        this.button21 = new System.Windows.Forms.Button();
        this.button23 = new System.Windows.Forms.Button();
        this.button24 = new System.Windows.Forms.Button();
        this.button25 = new System.Windows.Forms.Button();
        this.button26 = new System.Windows.Forms.Button();
        this.button27 = new System.Windows.Forms.Button();
        this.button28 = new System.Windows.Forms.Button();
        this.button29 = new System.Windows.Forms.Button();
        this.button30 = new System.Windows.Forms.Button();
        this.button31 = new System.Windows.Forms.Button();
        this.button22 = new System.Windows.Forms.Button();
        this.button32 = new System.Windows.Forms.Button();
        this.button33 = new System.Windows.Forms.Button();
        this.button34 = new System.Windows.Forms.Button();
        this.button35 = new System.Windows.Forms.Button();
        this.button36 = new System.Windows.Forms.Button();
        this.button37 = new System.Windows.Forms.Button();
        this.button38 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(235, 36);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.textBox1.Location = new System.Drawing.Point(12, 38);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(217, 21);
        this.textBox1.TabIndex = 1;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 14);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(35, 12);
        this.label1.TabIndex = 2;
        this.label1.Text = "提示:";
        this.button2.Location = new System.Drawing.Point(24, 78);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(23, 23);
        this.button2.TabIndex = 3;
        this.button2.Text = "1";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(SoftKey_Click);
        this.button3.Location = new System.Drawing.Point(50, 78);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(23, 23);
        this.button3.TabIndex = 3;
        this.button3.Text = "2";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(SoftKey_Click);
        this.button4.Location = new System.Drawing.Point(76, 78);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(23, 23);
        this.button4.TabIndex = 3;
        this.button4.Text = "3";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(SoftKey_Click);
        this.button5.Location = new System.Drawing.Point(102, 78);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(23, 23);
        this.button5.TabIndex = 3;
        this.button5.Text = "4";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(SoftKey_Click);
        this.button6.Location = new System.Drawing.Point(128, 78);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(23, 23);
        this.button6.TabIndex = 3;
        this.button6.Text = "5";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(SoftKey_Click);
        this.button7.Location = new System.Drawing.Point(154, 78);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(23, 23);
        this.button7.TabIndex = 3;
        this.button7.Text = "6";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(SoftKey_Click);
        this.button8.Location = new System.Drawing.Point(180, 78);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(23, 23);
        this.button8.TabIndex = 3;
        this.button8.Text = "7";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(SoftKey_Click);
        this.button9.Location = new System.Drawing.Point(206, 78);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(23, 23);
        this.button9.TabIndex = 3;
        this.button9.Text = "8";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(SoftKey_Click);
        this.button10.Location = new System.Drawing.Point(232, 78);
        this.button10.Name = "button10";
        this.button10.Size = new System.Drawing.Size(23, 23);
        this.button10.TabIndex = 3;
        this.button10.Text = "9";
        this.button10.UseVisualStyleBackColor = true;
        this.button10.Click += new System.EventHandler(SoftKey_Click);
        this.button11.Location = new System.Drawing.Point(258, 78);
        this.button11.Name = "button11";
        this.button11.Size = new System.Drawing.Size(23, 23);
        this.button11.TabIndex = 3;
        this.button11.Text = "0";
        this.button11.UseVisualStyleBackColor = true;
        this.button11.Click += new System.EventHandler(SoftKey_Click);
        this.button12.Location = new System.Drawing.Point(24, 107);
        this.button12.Name = "button12";
        this.button12.Size = new System.Drawing.Size(23, 23);
        this.button12.TabIndex = 3;
        this.button12.Text = "q";
        this.button12.UseVisualStyleBackColor = true;
        this.button12.Click += new System.EventHandler(SoftKey_Click);
        this.button13.Location = new System.Drawing.Point(50, 107);
        this.button13.Name = "button13";
        this.button13.Size = new System.Drawing.Size(23, 23);
        this.button13.TabIndex = 3;
        this.button13.Text = "w";
        this.button13.UseVisualStyleBackColor = true;
        this.button13.Click += new System.EventHandler(SoftKey_Click);
        this.button14.Location = new System.Drawing.Point(76, 107);
        this.button14.Name = "button14";
        this.button14.Size = new System.Drawing.Size(23, 23);
        this.button14.TabIndex = 3;
        this.button14.Text = "e";
        this.button14.UseVisualStyleBackColor = true;
        this.button14.Click += new System.EventHandler(SoftKey_Click);
        this.button15.Location = new System.Drawing.Point(102, 107);
        this.button15.Name = "button15";
        this.button15.Size = new System.Drawing.Size(23, 23);
        this.button15.TabIndex = 3;
        this.button15.Text = "r";
        this.button15.UseVisualStyleBackColor = true;
        this.button15.Click += new System.EventHandler(SoftKey_Click);
        this.button16.Location = new System.Drawing.Point(128, 107);
        this.button16.Name = "button16";
        this.button16.Size = new System.Drawing.Size(23, 23);
        this.button16.TabIndex = 3;
        this.button16.Text = "t";
        this.button16.UseVisualStyleBackColor = true;
        this.button16.Click += new System.EventHandler(SoftKey_Click);
        this.button17.Location = new System.Drawing.Point(154, 107);
        this.button17.Name = "button17";
        this.button17.Size = new System.Drawing.Size(23, 23);
        this.button17.TabIndex = 3;
        this.button17.Text = "y";
        this.button17.UseVisualStyleBackColor = true;
        this.button17.Click += new System.EventHandler(SoftKey_Click);
        this.button18.Location = new System.Drawing.Point(180, 107);
        this.button18.Name = "button18";
        this.button18.Size = new System.Drawing.Size(23, 23);
        this.button18.TabIndex = 3;
        this.button18.Text = "ui";
        this.button18.UseVisualStyleBackColor = true;
        this.button18.Click += new System.EventHandler(SoftKey_Click);
        this.button19.Location = new System.Drawing.Point(206, 107);
        this.button19.Name = "button19";
        this.button19.Size = new System.Drawing.Size(23, 23);
        this.button19.TabIndex = 3;
        this.button19.Text = "i";
        this.button19.UseVisualStyleBackColor = true;
        this.button19.Click += new System.EventHandler(SoftKey_Click);
        this.button20.Location = new System.Drawing.Point(232, 107);
        this.button20.Name = "button20";
        this.button20.Size = new System.Drawing.Size(23, 23);
        this.button20.TabIndex = 3;
        this.button20.Text = "o";
        this.button20.UseVisualStyleBackColor = true;
        this.button20.Click += new System.EventHandler(SoftKey_Click);
        this.button21.Location = new System.Drawing.Point(258, 107);
        this.button21.Name = "button21";
        this.button21.Size = new System.Drawing.Size(23, 23);
        this.button21.TabIndex = 3;
        this.button21.Text = "p";
        this.button21.UseVisualStyleBackColor = true;
        this.button21.Click += new System.EventHandler(SoftKey_Click);
        this.button23.Location = new System.Drawing.Point(245, 136);
        this.button23.Name = "button23";
        this.button23.Size = new System.Drawing.Size(23, 23);
        this.button23.TabIndex = 9;
        this.button23.Text = "l";
        this.button23.UseVisualStyleBackColor = true;
        this.button23.Click += new System.EventHandler(SoftKey_Click);
        this.button24.Location = new System.Drawing.Point(219, 136);
        this.button24.Name = "button24";
        this.button24.Size = new System.Drawing.Size(23, 23);
        this.button24.TabIndex = 11;
        this.button24.Text = "k";
        this.button24.UseVisualStyleBackColor = true;
        this.button24.Click += new System.EventHandler(SoftKey_Click);
        this.button25.Location = new System.Drawing.Point(193, 136);
        this.button25.Name = "button25";
        this.button25.Size = new System.Drawing.Size(23, 23);
        this.button25.TabIndex = 13;
        this.button25.Text = "j";
        this.button25.UseVisualStyleBackColor = true;
        this.button25.Click += new System.EventHandler(SoftKey_Click);
        this.button26.Location = new System.Drawing.Point(167, 136);
        this.button26.Name = "button26";
        this.button26.Size = new System.Drawing.Size(23, 23);
        this.button26.TabIndex = 12;
        this.button26.Text = "h";
        this.button26.UseVisualStyleBackColor = true;
        this.button26.Click += new System.EventHandler(SoftKey_Click);
        this.button27.Location = new System.Drawing.Point(141, 136);
        this.button27.Name = "button27";
        this.button27.Size = new System.Drawing.Size(23, 23);
        this.button27.TabIndex = 5;
        this.button27.Text = "g";
        this.button27.UseVisualStyleBackColor = true;
        this.button27.Click += new System.EventHandler(SoftKey_Click);
        this.button28.Location = new System.Drawing.Point(115, 136);
        this.button28.Name = "button28";
        this.button28.Size = new System.Drawing.Size(23, 23);
        this.button28.TabIndex = 4;
        this.button28.Text = "f";
        this.button28.UseVisualStyleBackColor = true;
        this.button28.Click += new System.EventHandler(SoftKey_Click);
        this.button29.Location = new System.Drawing.Point(89, 136);
        this.button29.Name = "button29";
        this.button29.Size = new System.Drawing.Size(23, 23);
        this.button29.TabIndex = 6;
        this.button29.Text = "d";
        this.button29.UseVisualStyleBackColor = true;
        this.button29.Click += new System.EventHandler(SoftKey_Click);
        this.button30.Location = new System.Drawing.Point(63, 136);
        this.button30.Name = "button30";
        this.button30.Size = new System.Drawing.Size(23, 23);
        this.button30.TabIndex = 8;
        this.button30.Text = "s";
        this.button30.UseVisualStyleBackColor = true;
        this.button30.Click += new System.EventHandler(SoftKey_Click);
        this.button31.Location = new System.Drawing.Point(37, 136);
        this.button31.Name = "button31";
        this.button31.Size = new System.Drawing.Size(23, 23);
        this.button31.TabIndex = 7;
        this.button31.Text = "a";
        this.button31.UseVisualStyleBackColor = true;
        this.button31.Click += new System.EventHandler(SoftKey_Click);
        this.button22.Location = new System.Drawing.Point(50, 165);
        this.button22.Name = "button22";
        this.button22.Size = new System.Drawing.Size(23, 23);
        this.button22.TabIndex = 3;
        this.button22.Text = "z";
        this.button22.UseVisualStyleBackColor = true;
        this.button22.Click += new System.EventHandler(SoftKey_Click);
        this.button32.Location = new System.Drawing.Point(76, 165);
        this.button32.Name = "button32";
        this.button32.Size = new System.Drawing.Size(23, 23);
        this.button32.TabIndex = 3;
        this.button32.Text = "x";
        this.button32.UseVisualStyleBackColor = true;
        this.button32.Click += new System.EventHandler(SoftKey_Click);
        this.button33.Location = new System.Drawing.Point(102, 165);
        this.button33.Name = "button33";
        this.button33.Size = new System.Drawing.Size(23, 23);
        this.button33.TabIndex = 3;
        this.button33.Text = "c";
        this.button33.UseVisualStyleBackColor = true;
        this.button33.Click += new System.EventHandler(SoftKey_Click);
        this.button34.Location = new System.Drawing.Point(128, 165);
        this.button34.Name = "button34";
        this.button34.Size = new System.Drawing.Size(23, 23);
        this.button34.TabIndex = 3;
        this.button34.Text = "v";
        this.button34.UseVisualStyleBackColor = true;
        this.button34.Click += new System.EventHandler(SoftKey_Click);
        this.button35.Location = new System.Drawing.Point(154, 165);
        this.button35.Name = "button35";
        this.button35.Size = new System.Drawing.Size(23, 23);
        this.button35.TabIndex = 3;
        this.button35.Text = "b";
        this.button35.UseVisualStyleBackColor = true;
        this.button35.Click += new System.EventHandler(SoftKey_Click);
        this.button36.Location = new System.Drawing.Point(180, 165);
        this.button36.Name = "button36";
        this.button36.Size = new System.Drawing.Size(23, 23);
        this.button36.TabIndex = 3;
        this.button36.Text = "n";
        this.button36.UseVisualStyleBackColor = true;
        this.button36.Click += new System.EventHandler(SoftKey_Click);
        this.button37.Location = new System.Drawing.Point(206, 165);
        this.button37.Name = "button37";
        this.button37.Size = new System.Drawing.Size(23, 23);
        this.button37.TabIndex = 3;
        this.button37.Text = "m";
        this.button37.UseVisualStyleBackColor = true;
        this.button37.Click += new System.EventHandler(SoftKey_Click);
        this.button38.Location = new System.Drawing.Point(232, 165);
        this.button38.Name = "button38";
        this.button38.Size = new System.Drawing.Size(23, 23);
        this.button38.TabIndex = 9;
        this.button38.Text = ".";
        this.button38.UseVisualStyleBackColor = true;
        this.button38.Click += new System.EventHandler(SoftKey_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(324, 73);
        base.Controls.Add(this.button38);
        base.Controls.Add(this.button23);
        base.Controls.Add(this.button24);
        base.Controls.Add(this.button25);
        base.Controls.Add(this.button26);
        base.Controls.Add(this.button27);
        base.Controls.Add(this.button28);
        base.Controls.Add(this.button29);
        base.Controls.Add(this.button30);
        base.Controls.Add(this.button31);
        base.Controls.Add(this.button21);
        base.Controls.Add(this.button20);
        base.Controls.Add(this.button11);
        base.Controls.Add(this.button19);
        base.Controls.Add(this.button37);
        base.Controls.Add(this.button10);
        base.Controls.Add(this.button18);
        base.Controls.Add(this.button36);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.button17);
        base.Controls.Add(this.button35);
        base.Controls.Add(this.button8);
        base.Controls.Add(this.button16);
        base.Controls.Add(this.button34);
        base.Controls.Add(this.button7);
        base.Controls.Add(this.button15);
        base.Controls.Add(this.button33);
        base.Controls.Add(this.button6);
        base.Controls.Add(this.button14);
        base.Controls.Add(this.button32);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button13);
        base.Controls.Add(this.button22);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button12);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "StringForm";
        this.Text = "字符量输入";
        base.Load += new System.EventHandler(StringForm_Load);
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(StringForm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
