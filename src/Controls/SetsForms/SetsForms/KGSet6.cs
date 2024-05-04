using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet6 : Form
{
    public string varname;

    public string varname2;

    public string titlestr = "仪表";

    public string onstr = "AUTO";

    public string offstr = "MAN";

    public string midstr = "OFF";

    public Color titlecolor = Color.Black;

    public Color oncolor = Color.Green;

    public Color offcolor = Color.Red;

    public Color midcolor = Color.Blue;

    public bool opstflag;

    private Button btn_Cancel;

    private Button btn_OK;

    private TextBox txt_var;

    private Button btn_view;

    private TextBox txt_var2;

    private Button button2;

    private ColorDialog colorDialog1;

    private GroupBox groupBox1;

    private TextBox txt_mid;

    private Label label5;

    private Label lbl_mid;

    private Label label11;

    private TextBox txt_title;

    private Label label4;

    private Label lbl_titlecolor;

    private Label label6;

    private TextBox txt_closed;

    private TextBox txt_ontxt;

    private Label label3;

    private Label label2;

    private Label lbl_off;

    private Label label7;

    private Label lbl_on;

    private Label label1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet6()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        varname = txt_var.Text;
        varname2 = txt_var2.Text;
        titlestr = txt_title.Text;
        onstr = txt_ontxt.Text;
        offstr = txt_closed.Text;
        midstr = txt_mid.Text;
        titlecolor = lbl_titlecolor.BackColor;
        oncolor = lbl_on.BackColor;
        offcolor = lbl_off.BackColor;
        midcolor = lbl_mid.BackColor;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_titlecolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_titlecolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_mid_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_mid.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_on_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_on.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_off_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_off.BackColor = colorDialog1.Color;
        }
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void KGSet6_Load(object sender, EventArgs e)
    {

    }

    private void InitializeComponent()
    {
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.txt_var2 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.txt_mid = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.lbl_mid = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.txt_title = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.lbl_titlecolor = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.txt_closed = new System.Windows.Forms.TextBox();
        this.txt_ontxt = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_off = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.lbl_on = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(191, 251);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 13;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(110, 252);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 12;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.txt_var.Location = new System.Drawing.Point(16, 18);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(169, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(194, 16);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "自动变量";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.txt_var2.Location = new System.Drawing.Point(16, 48);
        this.txt_var2.Name = "txt_var2";
        this.txt_var2.Size = new System.Drawing.Size(169, 21);
        this.txt_var2.TabIndex = 2;
        this.button2.Location = new System.Drawing.Point(194, 45);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 3;
        this.button2.Text = "手动变量";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.groupBox1.Controls.Add(this.txt_mid);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.lbl_mid);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Controls.Add(this.txt_title);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.lbl_titlecolor);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.txt_closed);
        this.groupBox1.Controls.Add(this.txt_ontxt);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.lbl_off);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.lbl_on);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(16, 74);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(264, 168);
        this.groupBox1.TabIndex = 127;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.txt_mid.Location = new System.Drawing.Point(187, 59);
        this.txt_mid.Name = "txt_mid";
        this.txt_mid.Size = new System.Drawing.Size(66, 21);
        this.txt_mid.TabIndex = 7;
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(120, 62);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(53, 12);
        this.label5.TabIndex = 145;
        this.label5.Text = "关文本：";
        this.lbl_mid.AutoSize = true;
        this.lbl_mid.BackColor = System.Drawing.Color.Blue;
        this.lbl_mid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_mid.Location = new System.Drawing.Point(77, 62);
        this.lbl_mid.Name = "lbl_mid";
        this.lbl_mid.Size = new System.Drawing.Size(37, 14);
        this.lbl_mid.TabIndex = 6;
        this.lbl_mid.Text = "     ";
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(6, 62);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(53, 12);
        this.label11.TabIndex = 143;
        this.label11.Text = "关颜色：";
        this.txt_title.Location = new System.Drawing.Point(187, 25);
        this.txt_title.Name = "txt_title";
        this.txt_title.Size = new System.Drawing.Size(66, 21);
        this.txt_title.TabIndex = 5;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(120, 28);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 141;
        this.label4.Text = "标题文本：";
        this.lbl_titlecolor.AutoSize = true;
        this.lbl_titlecolor.BackColor = System.Drawing.Color.Black;
        this.lbl_titlecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_titlecolor.Location = new System.Drawing.Point(77, 28);
        this.lbl_titlecolor.Name = "lbl_titlecolor";
        this.lbl_titlecolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_titlecolor.TabIndex = 4;
        this.lbl_titlecolor.Text = "     ";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(6, 28);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 12);
        this.label6.TabIndex = 139;
        this.label6.Text = "标题颜色：";
        this.txt_closed.Location = new System.Drawing.Point(187, 127);
        this.txt_closed.Name = "txt_closed";
        this.txt_closed.Size = new System.Drawing.Size(66, 21);
        this.txt_closed.TabIndex = 11;
        this.txt_ontxt.Location = new System.Drawing.Point(187, 93);
        this.txt_ontxt.Name = "txt_ontxt";
        this.txt_ontxt.Size = new System.Drawing.Size(66, 21);
        this.txt_ontxt.TabIndex = 9;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(120, 130);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 136;
        this.label3.Text = "手动文本：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(120, 96);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 135;
        this.label2.Text = "自动文本：";
        this.lbl_off.AutoSize = true;
        this.lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_off.Location = new System.Drawing.Point(77, 130);
        this.lbl_off.Name = "lbl_off";
        this.lbl_off.Size = new System.Drawing.Size(37, 14);
        this.lbl_off.TabIndex = 10;
        this.lbl_off.Text = "     ";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(6, 130);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(65, 12);
        this.label7.TabIndex = 133;
        this.label7.Text = "手动颜色：";
        this.lbl_on.AutoSize = true;
        this.lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_on.Location = new System.Drawing.Point(77, 96);
        this.lbl_on.Name = "lbl_on";
        this.lbl_on.Size = new System.Drawing.Size(37, 14);
        this.lbl_on.TabIndex = 8;
        this.lbl_on.Text = "     ";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 96);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 131;
        this.label1.Text = "自动颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 285);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var2);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet6";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet6_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
