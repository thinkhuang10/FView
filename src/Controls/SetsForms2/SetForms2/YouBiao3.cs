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
            else if (ckvarevent(txt_var1.Text) && ckvarevent(txt_var2.Text) && ckvarevent(txt_var3.Text) && ckvarevent(txt_var4.Text) && ckvarevent(txt_var5.Text))
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
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var3.Text = value;
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var4.Text = value;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var5.Text = value;
        }
    }

    private void txt_var1_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var1.Text = value;
        }
    }

    private void txt_var2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var2.Text = value;
        }
    }

    private void txt_var3_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var3.Text = value;
        }
    }

    private void txt_var4_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var4.Text = value;
        }
    }

    private void txt_var5_Click(object sender, EventArgs e)
    {
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var5.Text = value;
        }
    }

    private void InitializeComponent()
    {
        txt_var1 = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        txt_text5 = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        txt_var5 = new System.Windows.Forms.TextBox();
        button4 = new System.Windows.Forms.Button();
        txt_text4 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        txt_var4 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        txt_text3 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        txt_var3 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        txt_text2 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        txt_var2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        txt_text1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        lbl_txtcolor5 = new System.Windows.Forms.Label();
        lbl_txtcolor4 = new System.Windows.Forms.Label();
        lbl_txtcolor3 = new System.Windows.Forms.Label();
        lbl_txtcolor2 = new System.Windows.Forms.Label();
        lbl_txtcolor1 = new System.Windows.Forms.Label();
        label20 = new System.Windows.Forms.Label();
        label19 = new System.Windows.Forms.Label();
        label18 = new System.Windows.Forms.Label();
        label17 = new System.Windows.Forms.Label();
        label16 = new System.Windows.Forms.Label();
        lbl_bgcolor5 = new System.Windows.Forms.Label();
        lbl_bgcolor4 = new System.Windows.Forms.Label();
        lbl_bgcolor3 = new System.Windows.Forms.Label();
        lbl_bgcolor2 = new System.Windows.Forms.Label();
        lbl_bgcolor1 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox3 = new System.Windows.Forms.GroupBox();
        txt_step = new System.Windows.Forms.TextBox();
        label15 = new System.Windows.Forms.Label();
        txt_mark = new System.Windows.Forms.TextBox();
        txt_mainmark = new System.Windows.Forms.TextBox();
        label13 = new System.Windows.Forms.Label();
        label14 = new System.Windows.Forms.Label();
        txt_minval = new System.Windows.Forms.TextBox();
        txt_maxval = new System.Windows.Forms.TextBox();
        label12 = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        base.SuspendLayout();
        txt_var1.Location = new System.Drawing.Point(21, 33);
        txt_var1.Name = "txt_var1";
        txt_var1.Size = new System.Drawing.Size(131, 21);
        txt_var1.TabIndex = 0;
        txt_var1.Click += new System.EventHandler(txt_var1_Click);
        btn_view.Location = new System.Drawing.Point(158, 33);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "变量1";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        groupBox1.Controls.Add(txt_text5);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_var5);
        groupBox1.Controls.Add(button4);
        groupBox1.Controls.Add(txt_text4);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(txt_var4);
        groupBox1.Controls.Add(button3);
        groupBox1.Controls.Add(txt_text3);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(txt_var3);
        groupBox1.Controls.Add(button2);
        groupBox1.Controls.Add(txt_text2);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(txt_var2);
        groupBox1.Controls.Add(button1);
        groupBox1.Controls.Add(txt_text1);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txt_var1);
        groupBox1.Controls.Add(btn_view);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(447, 187);
        groupBox1.TabIndex = 64;
        groupBox1.TabStop = false;
        groupBox1.Text = "变量与文本";
        txt_text5.Location = new System.Drawing.Point(291, 143);
        txt_text5.Name = "txt_text5";
        txt_text5.Size = new System.Drawing.Size(128, 21);
        txt_text5.TabIndex = 14;
        txt_text5.TextChanged += new System.EventHandler(textBox8_TextChanged);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(244, 148);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(47, 12);
        label5.TabIndex = 80;
        label5.Text = "文本5：";
        txt_var5.Location = new System.Drawing.Point(21, 143);
        txt_var5.Name = "txt_var5";
        txt_var5.Size = new System.Drawing.Size(131, 21);
        txt_var5.TabIndex = 12;
        txt_var5.Click += new System.EventHandler(txt_var5_Click);
        button4.Location = new System.Drawing.Point(158, 143);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 13;
        button4.Text = "变量5";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        txt_text4.Location = new System.Drawing.Point(291, 116);
        txt_text4.Name = "txt_text4";
        txt_text4.Size = new System.Drawing.Size(128, 21);
        txt_text4.TabIndex = 11;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(244, 121);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(47, 12);
        label4.TabIndex = 76;
        label4.Text = "文本4：";
        txt_var4.Location = new System.Drawing.Point(21, 116);
        txt_var4.Name = "txt_var4";
        txt_var4.Size = new System.Drawing.Size(131, 21);
        txt_var4.TabIndex = 9;
        txt_var4.Click += new System.EventHandler(txt_var4_Click);
        button3.Location = new System.Drawing.Point(158, 116);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 10;
        button3.Text = "变量4";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        txt_text3.Location = new System.Drawing.Point(291, 89);
        txt_text3.Name = "txt_text3";
        txt_text3.Size = new System.Drawing.Size(128, 21);
        txt_text3.TabIndex = 8;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(244, 94);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(47, 12);
        label3.TabIndex = 72;
        label3.Text = "文本3：";
        txt_var3.Location = new System.Drawing.Point(21, 89);
        txt_var3.Name = "txt_var3";
        txt_var3.Size = new System.Drawing.Size(131, 21);
        txt_var3.TabIndex = 6;
        txt_var3.Click += new System.EventHandler(txt_var3_Click);
        button2.Location = new System.Drawing.Point(158, 89);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 7;
        button2.Text = "变量3";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        txt_text2.Location = new System.Drawing.Point(291, 62);
        txt_text2.Name = "txt_text2";
        txt_text2.Size = new System.Drawing.Size(128, 21);
        txt_text2.TabIndex = 5;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(244, 67);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(47, 12);
        label2.TabIndex = 68;
        label2.Text = "文本2：";
        txt_var2.Location = new System.Drawing.Point(21, 62);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(131, 21);
        txt_var2.TabIndex = 3;
        txt_var2.Click += new System.EventHandler(txt_var2_Click);
        button1.Location = new System.Drawing.Point(158, 62);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 4;
        button1.Text = "变量2";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        txt_text1.Location = new System.Drawing.Point(291, 33);
        txt_text1.Name = "txt_text1";
        txt_text1.Size = new System.Drawing.Size(128, 21);
        txt_text1.TabIndex = 2;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(244, 38);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(47, 12);
        label1.TabIndex = 64;
        label1.Text = "文本1：";
        groupBox2.Controls.Add(lbl_txtcolor5);
        groupBox2.Controls.Add(lbl_txtcolor4);
        groupBox2.Controls.Add(lbl_txtcolor3);
        groupBox2.Controls.Add(lbl_txtcolor2);
        groupBox2.Controls.Add(lbl_txtcolor1);
        groupBox2.Controls.Add(label20);
        groupBox2.Controls.Add(label19);
        groupBox2.Controls.Add(label18);
        groupBox2.Controls.Add(label17);
        groupBox2.Controls.Add(label16);
        groupBox2.Controls.Add(lbl_bgcolor5);
        groupBox2.Controls.Add(lbl_bgcolor4);
        groupBox2.Controls.Add(lbl_bgcolor3);
        groupBox2.Controls.Add(lbl_bgcolor2);
        groupBox2.Controls.Add(lbl_bgcolor1);
        groupBox2.Controls.Add(label10);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(label6);
        groupBox2.Location = new System.Drawing.Point(12, 205);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(447, 170);
        groupBox2.TabIndex = 65;
        groupBox2.TabStop = false;
        groupBox2.Text = "颜色设置";
        lbl_txtcolor5.AutoSize = true;
        lbl_txtcolor5.BackColor = System.Drawing.Color.Lime;
        lbl_txtcolor5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor5.Location = new System.Drawing.Point(300, 142);
        lbl_txtcolor5.Name = "lbl_txtcolor5";
        lbl_txtcolor5.Size = new System.Drawing.Size(91, 14);
        lbl_txtcolor5.TabIndex = 24;
        lbl_txtcolor5.Text = "              ";
        lbl_txtcolor5.Click += new System.EventHandler(lbl_txtcolor5_Click);
        lbl_txtcolor4.AutoSize = true;
        lbl_txtcolor4.BackColor = System.Drawing.Color.Lime;
        lbl_txtcolor4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor4.Location = new System.Drawing.Point(300, 113);
        lbl_txtcolor4.Name = "lbl_txtcolor4";
        lbl_txtcolor4.Size = new System.Drawing.Size(91, 14);
        lbl_txtcolor4.TabIndex = 22;
        lbl_txtcolor4.Text = "              ";
        lbl_txtcolor4.Click += new System.EventHandler(lbl_txtcolor4_Click);
        lbl_txtcolor3.AutoSize = true;
        lbl_txtcolor3.BackColor = System.Drawing.Color.Lime;
        lbl_txtcolor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor3.Location = new System.Drawing.Point(300, 84);
        lbl_txtcolor3.Name = "lbl_txtcolor3";
        lbl_txtcolor3.Size = new System.Drawing.Size(91, 14);
        lbl_txtcolor3.TabIndex = 20;
        lbl_txtcolor3.Text = "              ";
        lbl_txtcolor3.Click += new System.EventHandler(lbl_txtcolor3_Click);
        lbl_txtcolor2.AutoSize = true;
        lbl_txtcolor2.BackColor = System.Drawing.Color.Lime;
        lbl_txtcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor2.Location = new System.Drawing.Point(300, 55);
        lbl_txtcolor2.Name = "lbl_txtcolor2";
        lbl_txtcolor2.Size = new System.Drawing.Size(91, 14);
        lbl_txtcolor2.TabIndex = 18;
        lbl_txtcolor2.Text = "              ";
        lbl_txtcolor2.Click += new System.EventHandler(lbl_txtcolor2_Click);
        lbl_txtcolor1.AutoSize = true;
        lbl_txtcolor1.BackColor = System.Drawing.Color.Lime;
        lbl_txtcolor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor1.Location = new System.Drawing.Point(300, 26);
        lbl_txtcolor1.Name = "lbl_txtcolor1";
        lbl_txtcolor1.Size = new System.Drawing.Size(91, 14);
        lbl_txtcolor1.TabIndex = 16;
        lbl_txtcolor1.Text = "              ";
        lbl_txtcolor1.Click += new System.EventHandler(lbl_txtcolor1_Click);
        label20.AutoSize = true;
        label20.Location = new System.Drawing.Point(223, 142);
        label20.Name = "label20";
        label20.Size = new System.Drawing.Size(71, 12);
        label20.TabIndex = 14;
        label20.Text = "填充颜色5：";
        label19.AutoSize = true;
        label19.Location = new System.Drawing.Point(223, 113);
        label19.Name = "label19";
        label19.Size = new System.Drawing.Size(71, 12);
        label19.TabIndex = 13;
        label19.Text = "填充颜色4：";
        label18.AutoSize = true;
        label18.Location = new System.Drawing.Point(223, 84);
        label18.Name = "label18";
        label18.Size = new System.Drawing.Size(71, 12);
        label18.TabIndex = 12;
        label18.Text = "填充颜色3：";
        label17.AutoSize = true;
        label17.Location = new System.Drawing.Point(223, 55);
        label17.Name = "label17";
        label17.Size = new System.Drawing.Size(71, 12);
        label17.TabIndex = 11;
        label17.Text = "填充颜色2：";
        label16.AutoSize = true;
        label16.Location = new System.Drawing.Point(223, 26);
        label16.Name = "label16";
        label16.Size = new System.Drawing.Size(71, 12);
        label16.TabIndex = 10;
        label16.Text = "填充颜色1：";
        lbl_bgcolor5.AutoSize = true;
        lbl_bgcolor5.BackColor = System.Drawing.Color.Black;
        lbl_bgcolor5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor5.Location = new System.Drawing.Point(109, 142);
        lbl_bgcolor5.Name = "lbl_bgcolor5";
        lbl_bgcolor5.Size = new System.Drawing.Size(91, 14);
        lbl_bgcolor5.TabIndex = 23;
        lbl_bgcolor5.Text = "              ";
        lbl_bgcolor5.Click += new System.EventHandler(lbl_bgcolor5_Click);
        lbl_bgcolor4.AutoSize = true;
        lbl_bgcolor4.BackColor = System.Drawing.Color.Black;
        lbl_bgcolor4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor4.Location = new System.Drawing.Point(109, 113);
        lbl_bgcolor4.Name = "lbl_bgcolor4";
        lbl_bgcolor4.Size = new System.Drawing.Size(91, 14);
        lbl_bgcolor4.TabIndex = 21;
        lbl_bgcolor4.Text = "              ";
        lbl_bgcolor4.Click += new System.EventHandler(lbl_bgcolor4_Click);
        lbl_bgcolor3.AutoSize = true;
        lbl_bgcolor3.BackColor = System.Drawing.Color.Black;
        lbl_bgcolor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor3.Location = new System.Drawing.Point(109, 84);
        lbl_bgcolor3.Name = "lbl_bgcolor3";
        lbl_bgcolor3.Size = new System.Drawing.Size(91, 14);
        lbl_bgcolor3.TabIndex = 19;
        lbl_bgcolor3.Text = "              ";
        lbl_bgcolor3.Click += new System.EventHandler(lbl_bgcolor3_Click);
        lbl_bgcolor2.AutoSize = true;
        lbl_bgcolor2.BackColor = System.Drawing.Color.Black;
        lbl_bgcolor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor2.Location = new System.Drawing.Point(109, 55);
        lbl_bgcolor2.Name = "lbl_bgcolor2";
        lbl_bgcolor2.Size = new System.Drawing.Size(91, 14);
        lbl_bgcolor2.TabIndex = 17;
        lbl_bgcolor2.Text = "              ";
        lbl_bgcolor2.Click += new System.EventHandler(lbl_bgcolor2_Click);
        lbl_bgcolor1.AutoSize = true;
        lbl_bgcolor1.BackColor = System.Drawing.Color.Black;
        lbl_bgcolor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor1.Location = new System.Drawing.Point(109, 26);
        lbl_bgcolor1.Name = "lbl_bgcolor1";
        lbl_bgcolor1.Size = new System.Drawing.Size(91, 14);
        lbl_bgcolor1.TabIndex = 15;
        lbl_bgcolor1.Text = "              ";
        lbl_bgcolor1.Click += new System.EventHandler(lbl_bgcolor1_Click);
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(19, 142);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(83, 12);
        label10.TabIndex = 4;
        label10.Text = "填充背景色5：";
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(19, 113);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(83, 12);
        label9.TabIndex = 3;
        label9.Text = "填充背景色4：";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(19, 84);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(83, 12);
        label8.TabIndex = 2;
        label8.Text = "填充背景色3：";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(19, 55);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(83, 12);
        label7.TabIndex = 1;
        label7.Text = "填充背景色2：";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(19, 26);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(83, 12);
        label6.TabIndex = 0;
        label6.Text = "填充背景色1：";
        btn_OK.Location = new System.Drawing.Point(303, 536);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 30;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(384, 536);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 31;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        groupBox3.Controls.Add(txt_step);
        groupBox3.Controls.Add(label15);
        groupBox3.Controls.Add(txt_mark);
        groupBox3.Controls.Add(txt_mainmark);
        groupBox3.Controls.Add(label13);
        groupBox3.Controls.Add(label14);
        groupBox3.Controls.Add(txt_minval);
        groupBox3.Controls.Add(txt_maxval);
        groupBox3.Controls.Add(label12);
        groupBox3.Controls.Add(label11);
        groupBox3.Location = new System.Drawing.Point(12, 387);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(447, 119);
        groupBox3.TabIndex = 68;
        groupBox3.TabStop = false;
        groupBox3.Text = "其他";
        txt_step.Location = new System.Drawing.Point(78, 85);
        txt_step.Name = "txt_step";
        txt_step.Size = new System.Drawing.Size(120, 21);
        txt_step.TabIndex = 29;
        label15.AutoSize = true;
        label15.Location = new System.Drawing.Point(19, 90);
        label15.Name = "label15";
        label15.Size = new System.Drawing.Size(65, 12);
        label15.TabIndex = 8;
        label15.Text = "变化幅度：";
        txt_mark.Location = new System.Drawing.Point(302, 55);
        txt_mark.Name = "txt_mark";
        txt_mark.Size = new System.Drawing.Size(108, 21);
        txt_mark.TabIndex = 28;
        txt_mainmark.Location = new System.Drawing.Point(302, 22);
        txt_mainmark.Name = "txt_mainmark";
        txt_mainmark.Size = new System.Drawing.Size(109, 21);
        txt_mainmark.TabIndex = 26;
        label13.AutoSize = true;
        label13.Location = new System.Drawing.Point(232, 59);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(65, 12);
        label13.TabIndex = 5;
        label13.Text = "副刻度数：";
        label14.AutoSize = true;
        label14.Location = new System.Drawing.Point(232, 30);
        label14.Name = "label14";
        label14.Size = new System.Drawing.Size(65, 12);
        label14.TabIndex = 4;
        label14.Text = "主刻度数：";
        txt_minval.Location = new System.Drawing.Point(78, 55);
        txt_minval.Name = "txt_minval";
        txt_minval.Size = new System.Drawing.Size(120, 21);
        txt_minval.TabIndex = 27;
        txt_maxval.Location = new System.Drawing.Point(78, 23);
        txt_maxval.Name = "txt_maxval";
        txt_maxval.Size = new System.Drawing.Size(120, 21);
        txt_maxval.TabIndex = 25;
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(19, 60);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(53, 12);
        label12.TabIndex = 1;
        label12.Text = "最小值：";
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(19, 31);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(53, 12);
        label11.TabIndex = 0;
        label11.Text = "最大值：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(471, 571);
        base.Controls.Add(groupBox3);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "YouBiao3";
        Text = "YouBiao设置";
        base.Load += new System.EventHandler(YouBiao3_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
