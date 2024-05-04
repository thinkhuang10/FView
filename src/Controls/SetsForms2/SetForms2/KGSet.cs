using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class KGSet : Form
{
    public string varname1;

    public string varname2;

    public string varname3;

    public string varname4;

    public string txt1;

    public string txt2;

    public string txt3;

    public string txt4;

    public Color blockcolor = Color.Red;

    public string ontxt = "ON";

    public string offtxt = "OFF";

    public bool opstflag;

    private TextBox txt_var;

    private Button btn_view;

    private TextBox txt_text1;

    private Label label1;

    private TextBox txt_var1;

    private TextBox txt_var2;

    private Label label2;

    private TextBox txt_text2;

    private Button button1;

    private TextBox txt_var3;

    private Label label3;

    private TextBox txt_text3;

    private Button button2;

    private TextBox txt_var4;

    private Label label4;

    private TextBox txt_text4;

    private Button button3;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private Label label6;

    private Label lbl_color1;

    private Label label5;

    private TextBox txt_offtxt;

    private Label label7;

    private TextBox txt_ontxt;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private CheckBox ckb_opst;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet()
    {
        InitializeComponent();
    }

    private void KGSet_Load(object sender, EventArgs e)
    {
        txt_var1.Text = varname1;
        txt_var2.Text = varname2;
        txt_var3.Text = varname3;
        txt_var4.Text = varname4;
        txt_text1.Text = txt1;
        txt_text2.Text = txt2;
        txt_text3.Text = txt3;
        txt_text4.Text = txt4;
        lbl_color1.BackColor = blockcolor;
        txt_ontxt.Text = ontxt;
        txt_offtxt.Text = offtxt;
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (!(txt_var1.Text != "") || !(txt_var2.Text != "") || !(txt_var3.Text != "") || !(txt_var4.Text != ""))
            {
                return;
            }
            if (this.ckvarevent(txt_var1.Text) && this.ckvarevent(txt_var2.Text) && this.ckvarevent(txt_var3.Text) && this.ckvarevent(txt_var4.Text))
            {
                if (ckb_opst.Checked)
                {
                    opstflag = true;
                }
                else
                {
                    opstflag = false;
                }
                varname1 = txt_var1.Text;
                varname2 = txt_var2.Text;
                varname3 = txt_var3.Text;
                varname4 = txt_var4.Text;
                txt1 = txt_text1.Text;
                txt2 = txt_text2.Text;
                txt3 = txt_text3.Text;
                txt4 = txt_text4.Text;
                blockcolor = lbl_color1.BackColor;
                ontxt = txt_ontxt.Text;
                offtxt = txt_offtxt.Text;
                base.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常！");
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
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

    private void lbl_color1_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color1.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_text1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.txt_var1 = new System.Windows.Forms.TextBox();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txt_text2 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.txt_var3 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.txt_text3 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.txt_var4 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.txt_text4 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.ckb_opst = new System.Windows.Forms.CheckBox();
        this.txt_offtxt = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.txt_ontxt = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.lbl_color1 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(12, -429);
        this.txt_var.Name = "txt_var";
        this.txt_var.ReadOnly = true;
        this.txt_var.Size = new System.Drawing.Size(165, 21);
        this.txt_var.TabIndex = 37;
        this.btn_view.Location = new System.Drawing.Point(152, 20);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_text1.Location = new System.Drawing.Point(300, 20);
        this.txt_text1.Name = "txt_text1";
        this.txt_text1.Size = new System.Drawing.Size(115, 21);
        this.txt_text1.TabIndex = 2;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(253, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 40;
        this.label1.Text = "文本：";
        this.txt_var1.Location = new System.Drawing.Point(20, 20);
        this.txt_var1.Name = "txt_var1";
        this.txt_var1.Size = new System.Drawing.Size(126, 21);
        this.txt_var1.TabIndex = 0;
        this.txt_var2.Location = new System.Drawing.Point(20, 48);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(126, 21);
        this.txt_var2.TabIndex = 3;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(253, 51);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 44;
        this.label2.Text = "文本：";
        this.txt_text2.Location = new System.Drawing.Point(300, 48);
        this.txt_text2.Name = "txt_text2";
        this.txt_text2.Size = new System.Drawing.Size(115, 21);
        this.txt_text2.TabIndex = 5;
        this.button1.Location = new System.Drawing.Point(152, 46);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 4;
        this.button1.Text = "....";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.txt_var3.Location = new System.Drawing.Point(20, 76);
        this.txt_var3.Name = "txt_var3";
        this.txt_var3.Size = new System.Drawing.Size(126, 21);
        this.txt_var3.TabIndex = 6;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(253, 79);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 48;
        this.label3.Text = "文本：";
        this.txt_text3.Location = new System.Drawing.Point(300, 76);
        this.txt_text3.Name = "txt_text3";
        this.txt_text3.Size = new System.Drawing.Size(115, 21);
        this.txt_text3.TabIndex = 8;
        this.button2.Location = new System.Drawing.Point(152, 74);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 7;
        this.button2.Text = "....";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.txt_var4.Location = new System.Drawing.Point(20, 104);
        this.txt_var4.Name = "txt_var4";
        this.txt_var4.Size = new System.Drawing.Size(126, 21);
        this.txt_var4.TabIndex = 9;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(253, 108);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(41, 12);
        this.label4.TabIndex = 52;
        this.label4.Text = "文本：";
        this.txt_text4.Location = new System.Drawing.Point(300, 105);
        this.txt_text4.Name = "txt_text4";
        this.txt_text4.Size = new System.Drawing.Size(115, 21);
        this.txt_text4.TabIndex = 11;
        this.button3.Location = new System.Drawing.Point(152, 103);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 10;
        this.button3.Text = "....";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.groupBox1.Controls.Add(this.txt_var1);
        this.groupBox1.Controls.Add(this.txt_var4);
        this.groupBox1.Controls.Add(this.btn_view);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.txt_text1);
        this.groupBox1.Controls.Add(this.txt_text4);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.button3);
        this.groupBox1.Controls.Add(this.button1);
        this.groupBox1.Controls.Add(this.txt_var3);
        this.groupBox1.Controls.Add(this.txt_text2);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txt_text3);
        this.groupBox1.Controls.Add(this.txt_var2);
        this.groupBox1.Controls.Add(this.button2);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(439, 145);
        this.groupBox1.TabIndex = 54;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "变量绑定";
        this.groupBox2.Controls.Add(this.ckb_opst);
        this.groupBox2.Controls.Add(this.txt_offtxt);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.txt_ontxt);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Controls.Add(this.lbl_color1);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Location = new System.Drawing.Point(12, 173);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(439, 118);
        this.groupBox2.TabIndex = 55;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "颜色和其它设置";
        this.ckb_opst.AutoSize = true;
        this.ckb_opst.Location = new System.Drawing.Point(224, 74);
        this.ckb_opst.Name = "ckb_opst";
        this.ckb_opst.Size = new System.Drawing.Size(48, 16);
        this.ckb_opst.TabIndex = 15;
        this.ckb_opst.Text = "取反";
        this.ckb_opst.UseVisualStyleBackColor = true;
        this.ckb_opst.Visible = false;
        this.txt_offtxt.Location = new System.Drawing.Point(123, 69);
        this.txt_offtxt.Name = "txt_offtxt";
        this.txt_offtxt.Size = new System.Drawing.Size(90, 21);
        this.txt_offtxt.TabIndex = 14;
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(40, 78);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(77, 12);
        this.label7.TabIndex = 4;
        this.label7.Text = "关时文本：  ";
        this.txt_ontxt.Location = new System.Drawing.Point(123, 39);
        this.txt_ontxt.Name = "txt_ontxt";
        this.txt_ontxt.Size = new System.Drawing.Size(90, 21);
        this.txt_ontxt.TabIndex = 13;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(40, 42);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(77, 12);
        this.label6.TabIndex = 2;
        this.label6.Text = "开时文本：  ";
        this.lbl_color1.AutoSize = true;
        this.lbl_color1.BackColor = System.Drawing.Color.Red;
        this.lbl_color1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color1.Location = new System.Drawing.Point(305, 39);
        this.lbl_color1.Name = "lbl_color1";
        this.lbl_color1.Size = new System.Drawing.Size(49, 14);
        this.lbl_color1.TabIndex = 12;
        this.lbl_color1.Text = "       ";
        this.lbl_color1.Click += new System.EventHandler(lbl_color1_Click);
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(222, 39);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(77, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "显示块颜色：";
        this.btn_OK.Location = new System.Drawing.Point(294, 307);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 16;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(376, 307);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 17;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(474, 357);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
