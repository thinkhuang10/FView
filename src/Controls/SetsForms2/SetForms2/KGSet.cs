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
            if (ckvarevent(txt_var1.Text) && ckvarevent(txt_var2.Text) && ckvarevent(txt_var3.Text) && ckvarevent(txt_var4.Text))
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

    private void lbl_color1_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color1.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        txt_text1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        txt_var1 = new System.Windows.Forms.TextBox();
        txt_var2 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        txt_text2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        txt_var3 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        txt_text3 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        txt_var4 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        txt_text4 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox2 = new System.Windows.Forms.GroupBox();
        ckb_opst = new System.Windows.Forms.CheckBox();
        txt_offtxt = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        txt_ontxt = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        lbl_color1 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(12, -429);
        txt_var.Name = "txt_var";
        txt_var.ReadOnly = true;
        txt_var.Size = new System.Drawing.Size(165, 21);
        txt_var.TabIndex = 37;
        btn_view.Location = new System.Drawing.Point(152, 20);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_text1.Location = new System.Drawing.Point(300, 20);
        txt_text1.Name = "txt_text1";
        txt_text1.Size = new System.Drawing.Size(115, 21);
        txt_text1.TabIndex = 2;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(253, 25);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 12);
        label1.TabIndex = 40;
        label1.Text = "文本：";
        txt_var1.Location = new System.Drawing.Point(20, 20);
        txt_var1.Name = "txt_var1";
        txt_var1.Size = new System.Drawing.Size(126, 21);
        txt_var1.TabIndex = 0;
        txt_var2.Location = new System.Drawing.Point(20, 48);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(126, 21);
        txt_var2.TabIndex = 3;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(253, 51);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(41, 12);
        label2.TabIndex = 44;
        label2.Text = "文本：";
        txt_text2.Location = new System.Drawing.Point(300, 48);
        txt_text2.Name = "txt_text2";
        txt_text2.Size = new System.Drawing.Size(115, 21);
        txt_text2.TabIndex = 5;
        button1.Location = new System.Drawing.Point(152, 46);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 4;
        button1.Text = "....";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        txt_var3.Location = new System.Drawing.Point(20, 76);
        txt_var3.Name = "txt_var3";
        txt_var3.Size = new System.Drawing.Size(126, 21);
        txt_var3.TabIndex = 6;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(253, 79);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(41, 12);
        label3.TabIndex = 48;
        label3.Text = "文本：";
        txt_text3.Location = new System.Drawing.Point(300, 76);
        txt_text3.Name = "txt_text3";
        txt_text3.Size = new System.Drawing.Size(115, 21);
        txt_text3.TabIndex = 8;
        button2.Location = new System.Drawing.Point(152, 74);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 7;
        button2.Text = "....";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        txt_var4.Location = new System.Drawing.Point(20, 104);
        txt_var4.Name = "txt_var4";
        txt_var4.Size = new System.Drawing.Size(126, 21);
        txt_var4.TabIndex = 9;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(253, 108);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(41, 12);
        label4.TabIndex = 52;
        label4.Text = "文本：";
        txt_text4.Location = new System.Drawing.Point(300, 105);
        txt_text4.Name = "txt_text4";
        txt_text4.Size = new System.Drawing.Size(115, 21);
        txt_text4.TabIndex = 11;
        button3.Location = new System.Drawing.Point(152, 103);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 10;
        button3.Text = "....";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        groupBox1.Controls.Add(txt_var1);
        groupBox1.Controls.Add(txt_var4);
        groupBox1.Controls.Add(btn_view);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(txt_text1);
        groupBox1.Controls.Add(txt_text4);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(button3);
        groupBox1.Controls.Add(button1);
        groupBox1.Controls.Add(txt_var3);
        groupBox1.Controls.Add(txt_text2);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(txt_text3);
        groupBox1.Controls.Add(txt_var2);
        groupBox1.Controls.Add(button2);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(439, 145);
        groupBox1.TabIndex = 54;
        groupBox1.TabStop = false;
        groupBox1.Text = "变量绑定";
        groupBox2.Controls.Add(ckb_opst);
        groupBox2.Controls.Add(txt_offtxt);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(txt_ontxt);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(lbl_color1);
        groupBox2.Controls.Add(label5);
        groupBox2.Location = new System.Drawing.Point(12, 173);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(439, 118);
        groupBox2.TabIndex = 55;
        groupBox2.TabStop = false;
        groupBox2.Text = "颜色和其它设置";
        ckb_opst.AutoSize = true;
        ckb_opst.Location = new System.Drawing.Point(224, 74);
        ckb_opst.Name = "ckb_opst";
        ckb_opst.Size = new System.Drawing.Size(48, 16);
        ckb_opst.TabIndex = 15;
        ckb_opst.Text = "取反";
        ckb_opst.UseVisualStyleBackColor = true;
        ckb_opst.Visible = false;
        txt_offtxt.Location = new System.Drawing.Point(123, 69);
        txt_offtxt.Name = "txt_offtxt";
        txt_offtxt.Size = new System.Drawing.Size(90, 21);
        txt_offtxt.TabIndex = 14;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(40, 78);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(77, 12);
        label7.TabIndex = 4;
        label7.Text = "关时文本：  ";
        txt_ontxt.Location = new System.Drawing.Point(123, 39);
        txt_ontxt.Name = "txt_ontxt";
        txt_ontxt.Size = new System.Drawing.Size(90, 21);
        txt_ontxt.TabIndex = 13;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(40, 42);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(77, 12);
        label6.TabIndex = 2;
        label6.Text = "开时文本：  ";
        lbl_color1.AutoSize = true;
        lbl_color1.BackColor = System.Drawing.Color.Red;
        lbl_color1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color1.Location = new System.Drawing.Point(305, 39);
        lbl_color1.Name = "lbl_color1";
        lbl_color1.Size = new System.Drawing.Size(49, 14);
        lbl_color1.TabIndex = 12;
        lbl_color1.Text = "       ";
        lbl_color1.Click += new System.EventHandler(lbl_color1_Click);
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(222, 39);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(77, 12);
        label5.TabIndex = 0;
        label5.Text = "显示块颜色：";
        btn_OK.Location = new System.Drawing.Point(294, 307);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 16;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(376, 307);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 17;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(474, 357);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
