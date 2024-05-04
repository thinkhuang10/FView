using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class YouBiao3 : Form
{
    public float maxval;

    public float minval;

    public int mainmark;

    public int othermark;

    public string varname1 = "";

    public string varname2 = "";

    public string varname3 = "";

    public string varname4 = "";

    public string varname5 = "";

    public string txt1 = "";

    public string txt2 = "";

    public string txt3 = "";

    public string txt4 = "";

    public string txt5 = "";

    public float changestep = 1f;

    public Color bgcolor1 = Color.Black;

    public Color bgcolor2 = Color.Black;

    public Color bgcolor3 = Color.Black;

    public Color bgcolor4 = Color.Black;

    public Color bgcolor5 = Color.Black;

    public Color txtcolor1 = Color.Green;

    public Color txtcolor2 = Color.Green;

    public Color txtcolor3 = Color.Green;

    public Color txtcolor4 = Color.Green;

    public Color txtcolor5 = Color.Green;

    private TextBox txt_var1;

    private Button btn_view;

    private GroupBox groupBox1;

    private TextBox txt_text1;

    private Label label1;

    private TextBox txt_text5;

    private Label label5;

    private TextBox txt_var5;

    private Button button4;

    private TextBox txt_text4;

    private Label label4;

    private TextBox txt_var4;

    private Button button3;

    private TextBox txt_text3;

    private Label label3;

    private TextBox txt_var3;

    private Button button2;

    private TextBox txt_text2;

    private Label label2;

    private TextBox txt_var2;

    private Button button1;

    private GroupBox groupBox2;

    private Label label7;

    private Label label6;

    private Label label8;

    private Label label9;

    private Label lbl_txtcolor5;

    private Label lbl_txtcolor4;

    private Label lbl_txtcolor3;

    private Label lbl_txtcolor2;

    private Label lbl_txtcolor1;

    private Label label20;

    private Label label19;

    private Label label18;

    private Label label17;

    private Label label16;

    private Label lbl_bgcolor5;

    private Label lbl_bgcolor4;

    private Label lbl_bgcolor3;

    private Label lbl_bgcolor2;

    private Label lbl_bgcolor1;

    private Label label10;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private GroupBox groupBox3;

    private TextBox txt_mark;

    private TextBox txt_mainmark;

    private Label label13;

    private Label label14;

    private TextBox txt_minval;

    private TextBox txt_maxval;

    private Label label12;

    private Label label11;

    private TextBox txt_step;

    private Label label15;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public YouBiao3()
    {
        InitializeComponent();
    }

    private void textBox8_TextChanged(object sender, EventArgs e)
    {
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToSingle(txt_maxval.Text) < Convert.ToSingle(txt_minval.Text) || Convert.ToInt32(txt_mainmark.Text) <= 0 || Convert.ToInt32(txt_mark.Text) < 0)
            {
                MessageBox.Show("信息填写有误！");
            }
            else if (this.ckvarevent(txt_var1.Text) && this.ckvarevent(txt_var2.Text) && this.ckvarevent(txt_var3.Text) && this.ckvarevent(txt_var4.Text) && this.ckvarevent(txt_var5.Text))
            {
                mainmark = Convert.ToInt32(txt_mainmark.Text);
                othermark = Convert.ToInt32(txt_mark.Text);
                maxval = Convert.ToSingle(txt_maxval.Text);
                minval = Convert.ToSingle(txt_minval.Text);
                varname1 = txt_var1.Text;
                varname2 = txt_var2.Text;
                varname3 = txt_var3.Text;
                varname4 = txt_var4.Text;
                varname5 = txt_var5.Text;
                txt1 = txt_text1.Text;
                txt2 = txt_text2.Text;
                txt3 = txt_text3.Text;
                txt4 = txt_text4.Text;
                txt5 = txt_text5.Text;
                bgcolor1 = lbl_bgcolor1.BackColor;
                bgcolor2 = lbl_bgcolor2.BackColor;
                bgcolor3 = lbl_bgcolor3.BackColor;
                bgcolor4 = lbl_bgcolor4.BackColor;
                bgcolor5 = lbl_bgcolor5.BackColor;
                txtcolor1 = lbl_txtcolor1.BackColor;
                txtcolor2 = lbl_txtcolor2.BackColor;
                txtcolor3 = lbl_txtcolor3.BackColor;
                txtcolor4 = lbl_txtcolor4.BackColor;
                txtcolor5 = lbl_txtcolor5.BackColor;
                changestep = Convert.ToSingle(txt_step.Text);
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("变量名错误！");
            }
        }
        catch
        {
            MessageBox.Show("信息填写不完整或有误！");
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_bgcolor1_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor1.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor2_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor2.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor3_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor3.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor4_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor4.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor5_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor5.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor1_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor1.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor2_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor2.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor3_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor3.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor4_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor4.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_txtcolor5_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor5.BackColor = colorDialog1.Color;
        }
    }

    private void YouBiao3_Load(object sender, EventArgs e)
    {
        txt_maxval.Text = maxval.ToString();
        txt_minval.Text = minval.ToString();
        txt_mainmark.Text = mainmark.ToString();
        txt_mark.Text = othermark.ToString();
        txt_var1.Text = varname1;
        txt_var2.Text = varname2;
        txt_var3.Text = varname3;
        txt_var4.Text = varname4;
        txt_var5.Text = varname5;
        txt_text1.Text = txt1;
        txt_text2.Text = txt2;
        txt_text3.Text = txt3;
        txt_text4.Text = txt4;
        txt_text5.Text = txt5;
        txt_step.Text = changestep.ToString();
        lbl_txtcolor1.BackColor = txtcolor1;
        lbl_txtcolor2.BackColor = txtcolor2;
        lbl_txtcolor3.BackColor = txtcolor3;
        lbl_txtcolor4.BackColor = txtcolor4;
        lbl_txtcolor5.BackColor = txtcolor5;
        lbl_bgcolor1.BackColor = bgcolor1;
        lbl_bgcolor2.BackColor = bgcolor2;
        lbl_bgcolor3.BackColor = bgcolor3;
        lbl_bgcolor4.BackColor = bgcolor4;
        lbl_bgcolor5.BackColor = bgcolor5;
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var3.Text = value;
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var4.Text = value;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var5.Text = value;
        }
    }

    private void txt_var1_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void txt_var2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void txt_var3_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var3.Text = value;
        }
    }

    private void txt_var4_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var4.Text = value;
        }
    }

    private void txt_var5_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var5.Text = value;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var1 = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.txt_text5 = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.txt_var5 = new System.Windows.Forms.TextBox();
        this.button4 = new System.Windows.Forms.Button();
        this.txt_text4 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.txt_var4 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.txt_text3 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.txt_var3 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.txt_text2 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.txt_text1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.lbl_txtcolor5 = new System.Windows.Forms.Label();
        this.lbl_txtcolor4 = new System.Windows.Forms.Label();
        this.lbl_txtcolor3 = new System.Windows.Forms.Label();
        this.lbl_txtcolor2 = new System.Windows.Forms.Label();
        this.lbl_txtcolor1 = new System.Windows.Forms.Label();
        this.label20 = new System.Windows.Forms.Label();
        this.label19 = new System.Windows.Forms.Label();
        this.label18 = new System.Windows.Forms.Label();
        this.label17 = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.lbl_bgcolor5 = new System.Windows.Forms.Label();
        this.lbl_bgcolor4 = new System.Windows.Forms.Label();
        this.lbl_bgcolor3 = new System.Windows.Forms.Label();
        this.lbl_bgcolor2 = new System.Windows.Forms.Label();
        this.lbl_bgcolor1 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.txt_step = new System.Windows.Forms.TextBox();
        this.label15 = new System.Windows.Forms.Label();
        this.txt_mark = new System.Windows.Forms.TextBox();
        this.txt_mainmark = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.label14 = new System.Windows.Forms.Label();
        this.txt_minval = new System.Windows.Forms.TextBox();
        this.txt_maxval = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        base.SuspendLayout();
        this.txt_var1.Location = new System.Drawing.Point(21, 33);
        this.txt_var1.Name = "txt_var1";
        this.txt_var1.Size = new System.Drawing.Size(131, 21);
        this.txt_var1.TabIndex = 0;
        this.txt_var1.Click += new System.EventHandler(txt_var1_Click);
        this.btn_view.Location = new System.Drawing.Point(158, 33);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "变量1";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.groupBox1.Controls.Add(this.txt_text5);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_var5);
        this.groupBox1.Controls.Add(this.button4);
        this.groupBox1.Controls.Add(this.txt_text4);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.txt_var4);
        this.groupBox1.Controls.Add(this.button3);
        this.groupBox1.Controls.Add(this.txt_text3);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.txt_var3);
        this.groupBox1.Controls.Add(this.button2);
        this.groupBox1.Controls.Add(this.txt_text2);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txt_var2);
        this.groupBox1.Controls.Add(this.button1);
        this.groupBox1.Controls.Add(this.txt_text1);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txt_var1);
        this.groupBox1.Controls.Add(this.btn_view);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(447, 187);
        this.groupBox1.TabIndex = 64;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "变量与文本";
        this.txt_text5.Location = new System.Drawing.Point(291, 143);
        this.txt_text5.Name = "txt_text5";
        this.txt_text5.Size = new System.Drawing.Size(128, 21);
        this.txt_text5.TabIndex = 14;
        this.txt_text5.TextChanged += new System.EventHandler(textBox8_TextChanged);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(244, 148);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(47, 12);
        this.label5.TabIndex = 80;
        this.label5.Text = "文本5：";
        this.txt_var5.Location = new System.Drawing.Point(21, 143);
        this.txt_var5.Name = "txt_var5";
        this.txt_var5.Size = new System.Drawing.Size(131, 21);
        this.txt_var5.TabIndex = 12;
        this.txt_var5.Click += new System.EventHandler(txt_var5_Click);
        this.button4.Location = new System.Drawing.Point(158, 143);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 13;
        this.button4.Text = "变量5";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.txt_text4.Location = new System.Drawing.Point(291, 116);
        this.txt_text4.Name = "txt_text4";
        this.txt_text4.Size = new System.Drawing.Size(128, 21);
        this.txt_text4.TabIndex = 11;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(244, 121);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(47, 12);
        this.label4.TabIndex = 76;
        this.label4.Text = "文本4：";
        this.txt_var4.Location = new System.Drawing.Point(21, 116);
        this.txt_var4.Name = "txt_var4";
        this.txt_var4.Size = new System.Drawing.Size(131, 21);
        this.txt_var4.TabIndex = 9;
        this.txt_var4.Click += new System.EventHandler(txt_var4_Click);
        this.button3.Location = new System.Drawing.Point(158, 116);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 10;
        this.button3.Text = "变量4";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.txt_text3.Location = new System.Drawing.Point(291, 89);
        this.txt_text3.Name = "txt_text3";
        this.txt_text3.Size = new System.Drawing.Size(128, 21);
        this.txt_text3.TabIndex = 8;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(244, 94);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(47, 12);
        this.label3.TabIndex = 72;
        this.label3.Text = "文本3：";
        this.txt_var3.Location = new System.Drawing.Point(21, 89);
        this.txt_var3.Name = "txt_var3";
        this.txt_var3.Size = new System.Drawing.Size(131, 21);
        this.txt_var3.TabIndex = 6;
        this.txt_var3.Click += new System.EventHandler(txt_var3_Click);
        this.button2.Location = new System.Drawing.Point(158, 89);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 7;
        this.button2.Text = "变量3";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.txt_text2.Location = new System.Drawing.Point(291, 62);
        this.txt_text2.Name = "txt_text2";
        this.txt_text2.Size = new System.Drawing.Size(128, 21);
        this.txt_text2.TabIndex = 5;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(244, 67);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(47, 12);
        this.label2.TabIndex = 68;
        this.label2.Text = "文本2：";
        this.txt_var2.Location = new System.Drawing.Point(21, 62);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(131, 21);
        this.txt_var2.TabIndex = 3;
        this.txt_var2.Click += new System.EventHandler(txt_var2_Click);
        this.button1.Location = new System.Drawing.Point(158, 62);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 4;
        this.button1.Text = "变量2";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.txt_text1.Location = new System.Drawing.Point(291, 33);
        this.txt_text1.Name = "txt_text1";
        this.txt_text1.Size = new System.Drawing.Size(128, 21);
        this.txt_text1.TabIndex = 2;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(244, 38);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(47, 12);
        this.label1.TabIndex = 64;
        this.label1.Text = "文本1：";
        this.groupBox2.Controls.Add(this.lbl_txtcolor5);
        this.groupBox2.Controls.Add(this.lbl_txtcolor4);
        this.groupBox2.Controls.Add(this.lbl_txtcolor3);
        this.groupBox2.Controls.Add(this.lbl_txtcolor2);
        this.groupBox2.Controls.Add(this.lbl_txtcolor1);
        this.groupBox2.Controls.Add(this.label20);
        this.groupBox2.Controls.Add(this.label19);
        this.groupBox2.Controls.Add(this.label18);
        this.groupBox2.Controls.Add(this.label17);
        this.groupBox2.Controls.Add(this.label16);
        this.groupBox2.Controls.Add(this.lbl_bgcolor5);
        this.groupBox2.Controls.Add(this.lbl_bgcolor4);
        this.groupBox2.Controls.Add(this.lbl_bgcolor3);
        this.groupBox2.Controls.Add(this.lbl_bgcolor2);
        this.groupBox2.Controls.Add(this.lbl_bgcolor1);
        this.groupBox2.Controls.Add(this.label10);
        this.groupBox2.Controls.Add(this.label9);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Location = new System.Drawing.Point(12, 205);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(447, 170);
        this.groupBox2.TabIndex = 65;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "颜色设置";
        this.lbl_txtcolor5.AutoSize = true;
        this.lbl_txtcolor5.BackColor = System.Drawing.Color.Lime;
        this.lbl_txtcolor5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor5.Location = new System.Drawing.Point(300, 142);
        this.lbl_txtcolor5.Name = "lbl_txtcolor5";
        this.lbl_txtcolor5.Size = new System.Drawing.Size(91, 14);
        this.lbl_txtcolor5.TabIndex = 24;
        this.lbl_txtcolor5.Text = "              ";
        this.lbl_txtcolor5.Click += new System.EventHandler(lbl_txtcolor5_Click);
        this.lbl_txtcolor4.AutoSize = true;
        this.lbl_txtcolor4.BackColor = System.Drawing.Color.Lime;
        this.lbl_txtcolor4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor4.Location = new System.Drawing.Point(300, 113);
        this.lbl_txtcolor4.Name = "lbl_txtcolor4";
        this.lbl_txtcolor4.Size = new System.Drawing.Size(91, 14);
        this.lbl_txtcolor4.TabIndex = 22;
        this.lbl_txtcolor4.Text = "              ";
        this.lbl_txtcolor4.Click += new System.EventHandler(lbl_txtcolor4_Click);
        this.lbl_txtcolor3.AutoSize = true;
        this.lbl_txtcolor3.BackColor = System.Drawing.Color.Lime;
        this.lbl_txtcolor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor3.Location = new System.Drawing.Point(300, 84);
        this.lbl_txtcolor3.Name = "lbl_txtcolor3";
        this.lbl_txtcolor3.Size = new System.Drawing.Size(91, 14);
        this.lbl_txtcolor3.TabIndex = 20;
        this.lbl_txtcolor3.Text = "              ";
        this.lbl_txtcolor3.Click += new System.EventHandler(lbl_txtcolor3_Click);
        this.lbl_txtcolor2.AutoSize = true;
        this.lbl_txtcolor2.BackColor = System.Drawing.Color.Lime;
        this.lbl_txtcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor2.Location = new System.Drawing.Point(300, 55);
        this.lbl_txtcolor2.Name = "lbl_txtcolor2";
        this.lbl_txtcolor2.Size = new System.Drawing.Size(91, 14);
        this.lbl_txtcolor2.TabIndex = 18;
        this.lbl_txtcolor2.Text = "              ";
        this.lbl_txtcolor2.Click += new System.EventHandler(lbl_txtcolor2_Click);
        this.lbl_txtcolor1.AutoSize = true;
        this.lbl_txtcolor1.BackColor = System.Drawing.Color.Lime;
        this.lbl_txtcolor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor1.Location = new System.Drawing.Point(300, 26);
        this.lbl_txtcolor1.Name = "lbl_txtcolor1";
        this.lbl_txtcolor1.Size = new System.Drawing.Size(91, 14);
        this.lbl_txtcolor1.TabIndex = 16;
        this.lbl_txtcolor1.Text = "              ";
        this.lbl_txtcolor1.Click += new System.EventHandler(lbl_txtcolor1_Click);
        this.label20.AutoSize = true;
        this.label20.Location = new System.Drawing.Point(223, 142);
        this.label20.Name = "label20";
        this.label20.Size = new System.Drawing.Size(71, 12);
        this.label20.TabIndex = 14;
        this.label20.Text = "填充颜色5：";
        this.label19.AutoSize = true;
        this.label19.Location = new System.Drawing.Point(223, 113);
        this.label19.Name = "label19";
        this.label19.Size = new System.Drawing.Size(71, 12);
        this.label19.TabIndex = 13;
        this.label19.Text = "填充颜色4：";
        this.label18.AutoSize = true;
        this.label18.Location = new System.Drawing.Point(223, 84);
        this.label18.Name = "label18";
        this.label18.Size = new System.Drawing.Size(71, 12);
        this.label18.TabIndex = 12;
        this.label18.Text = "填充颜色3：";
        this.label17.AutoSize = true;
        this.label17.Location = new System.Drawing.Point(223, 55);
        this.label17.Name = "label17";
        this.label17.Size = new System.Drawing.Size(71, 12);
        this.label17.TabIndex = 11;
        this.label17.Text = "填充颜色2：";
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(223, 26);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(71, 12);
        this.label16.TabIndex = 10;
        this.label16.Text = "填充颜色1：";
        this.lbl_bgcolor5.AutoSize = true;
        this.lbl_bgcolor5.BackColor = System.Drawing.Color.Black;
        this.lbl_bgcolor5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor5.Location = new System.Drawing.Point(109, 142);
        this.lbl_bgcolor5.Name = "lbl_bgcolor5";
        this.lbl_bgcolor5.Size = new System.Drawing.Size(91, 14);
        this.lbl_bgcolor5.TabIndex = 23;
        this.lbl_bgcolor5.Text = "              ";
        this.lbl_bgcolor5.Click += new System.EventHandler(lbl_bgcolor5_Click);
        this.lbl_bgcolor4.AutoSize = true;
        this.lbl_bgcolor4.BackColor = System.Drawing.Color.Black;
        this.lbl_bgcolor4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor4.Location = new System.Drawing.Point(109, 113);
        this.lbl_bgcolor4.Name = "lbl_bgcolor4";
        this.lbl_bgcolor4.Size = new System.Drawing.Size(91, 14);
        this.lbl_bgcolor4.TabIndex = 21;
        this.lbl_bgcolor4.Text = "              ";
        this.lbl_bgcolor4.Click += new System.EventHandler(lbl_bgcolor4_Click);
        this.lbl_bgcolor3.AutoSize = true;
        this.lbl_bgcolor3.BackColor = System.Drawing.Color.Black;
        this.lbl_bgcolor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor3.Location = new System.Drawing.Point(109, 84);
        this.lbl_bgcolor3.Name = "lbl_bgcolor3";
        this.lbl_bgcolor3.Size = new System.Drawing.Size(91, 14);
        this.lbl_bgcolor3.TabIndex = 19;
        this.lbl_bgcolor3.Text = "              ";
        this.lbl_bgcolor3.Click += new System.EventHandler(lbl_bgcolor3_Click);
        this.lbl_bgcolor2.AutoSize = true;
        this.lbl_bgcolor2.BackColor = System.Drawing.Color.Black;
        this.lbl_bgcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor2.Location = new System.Drawing.Point(109, 55);
        this.lbl_bgcolor2.Name = "lbl_bgcolor2";
        this.lbl_bgcolor2.Size = new System.Drawing.Size(91, 14);
        this.lbl_bgcolor2.TabIndex = 17;
        this.lbl_bgcolor2.Text = "              ";
        this.lbl_bgcolor2.Click += new System.EventHandler(lbl_bgcolor2_Click);
        this.lbl_bgcolor1.AutoSize = true;
        this.lbl_bgcolor1.BackColor = System.Drawing.Color.Black;
        this.lbl_bgcolor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor1.Location = new System.Drawing.Point(109, 26);
        this.lbl_bgcolor1.Name = "lbl_bgcolor1";
        this.lbl_bgcolor1.Size = new System.Drawing.Size(91, 14);
        this.lbl_bgcolor1.TabIndex = 15;
        this.lbl_bgcolor1.Text = "              ";
        this.lbl_bgcolor1.Click += new System.EventHandler(lbl_bgcolor1_Click);
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(19, 142);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(83, 12);
        this.label10.TabIndex = 4;
        this.label10.Text = "填充背景色5：";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(19, 113);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(83, 12);
        this.label9.TabIndex = 3;
        this.label9.Text = "填充背景色4：";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(19, 84);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(83, 12);
        this.label8.TabIndex = 2;
        this.label8.Text = "填充背景色3：";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(19, 55);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(83, 12);
        this.label7.TabIndex = 1;
        this.label7.Text = "填充背景色2：";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(19, 26);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(83, 12);
        this.label6.TabIndex = 0;
        this.label6.Text = "填充背景色1：";
        this.btn_OK.Location = new System.Drawing.Point(303, 536);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 30;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(384, 536);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 31;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.groupBox3.Controls.Add(this.txt_step);
        this.groupBox3.Controls.Add(this.label15);
        this.groupBox3.Controls.Add(this.txt_mark);
        this.groupBox3.Controls.Add(this.txt_mainmark);
        this.groupBox3.Controls.Add(this.label13);
        this.groupBox3.Controls.Add(this.label14);
        this.groupBox3.Controls.Add(this.txt_minval);
        this.groupBox3.Controls.Add(this.txt_maxval);
        this.groupBox3.Controls.Add(this.label12);
        this.groupBox3.Controls.Add(this.label11);
        this.groupBox3.Location = new System.Drawing.Point(12, 387);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(447, 119);
        this.groupBox3.TabIndex = 68;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "其他";
        this.txt_step.Location = new System.Drawing.Point(78, 85);
        this.txt_step.Name = "txt_step";
        this.txt_step.Size = new System.Drawing.Size(120, 21);
        this.txt_step.TabIndex = 29;
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(19, 90);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(65, 12);
        this.label15.TabIndex = 8;
        this.label15.Text = "变化幅度：";
        this.txt_mark.Location = new System.Drawing.Point(302, 55);
        this.txt_mark.Name = "txt_mark";
        this.txt_mark.Size = new System.Drawing.Size(108, 21);
        this.txt_mark.TabIndex = 28;
        this.txt_mainmark.Location = new System.Drawing.Point(302, 22);
        this.txt_mainmark.Name = "txt_mainmark";
        this.txt_mainmark.Size = new System.Drawing.Size(109, 21);
        this.txt_mainmark.TabIndex = 26;
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(232, 59);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(65, 12);
        this.label13.TabIndex = 5;
        this.label13.Text = "副刻度数：";
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(232, 30);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(65, 12);
        this.label14.TabIndex = 4;
        this.label14.Text = "主刻度数：";
        this.txt_minval.Location = new System.Drawing.Point(78, 55);
        this.txt_minval.Name = "txt_minval";
        this.txt_minval.Size = new System.Drawing.Size(120, 21);
        this.txt_minval.TabIndex = 27;
        this.txt_maxval.Location = new System.Drawing.Point(78, 23);
        this.txt_maxval.Name = "txt_maxval";
        this.txt_maxval.Size = new System.Drawing.Size(120, 21);
        this.txt_maxval.TabIndex = 25;
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(19, 60);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(53, 12);
        this.label12.TabIndex = 1;
        this.label12.Text = "最小值：";
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(19, 31);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(53, 12);
        this.label11.TabIndex = 0;
        this.label11.Text = "最大值：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(471, 571);
        base.Controls.Add(this.groupBox3);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao3";
        this.Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao3_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
