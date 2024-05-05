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
        string value = viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        string value = viewevent();
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
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        txt_var2 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1 = new System.Windows.Forms.GroupBox();
        txt_mid = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        lbl_mid = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        txt_title = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        lbl_titlecolor = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        txt_closed = new System.Windows.Forms.TextBox();
        txt_ontxt = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        lbl_off = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        lbl_on = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(191, 251);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 13;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(110, 252);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 12;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        txt_var.Location = new System.Drawing.Point(16, 18);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(169, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(194, 16);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "自动变量";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        txt_var2.Location = new System.Drawing.Point(16, 48);
        txt_var2.Name = "txt_var2";
        txt_var2.Size = new System.Drawing.Size(169, 21);
        txt_var2.TabIndex = 2;
        button2.Location = new System.Drawing.Point(194, 45);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 3;
        button2.Text = "手动变量";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        groupBox1.Controls.Add(txt_mid);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(lbl_mid);
        groupBox1.Controls.Add(label11);
        groupBox1.Controls.Add(txt_title);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_titlecolor);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(txt_closed);
        groupBox1.Controls.Add(txt_ontxt);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(lbl_off);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(lbl_on);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(16, 74);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(264, 168);
        groupBox1.TabIndex = 127;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        txt_mid.Location = new System.Drawing.Point(187, 59);
        txt_mid.Name = "txt_mid";
        txt_mid.Size = new System.Drawing.Size(66, 21);
        txt_mid.TabIndex = 7;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(120, 62);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(53, 12);
        label5.TabIndex = 145;
        label5.Text = "关文本：";
        lbl_mid.AutoSize = true;
        lbl_mid.BackColor = System.Drawing.Color.Blue;
        lbl_mid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_mid.Location = new System.Drawing.Point(77, 62);
        lbl_mid.Name = "lbl_mid";
        lbl_mid.Size = new System.Drawing.Size(37, 14);
        lbl_mid.TabIndex = 6;
        lbl_mid.Text = "     ";
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(6, 62);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(53, 12);
        label11.TabIndex = 143;
        label11.Text = "关颜色：";
        txt_title.Location = new System.Drawing.Point(187, 25);
        txt_title.Name = "txt_title";
        txt_title.Size = new System.Drawing.Size(66, 21);
        txt_title.TabIndex = 5;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(120, 28);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 141;
        label4.Text = "标题文本：";
        lbl_titlecolor.AutoSize = true;
        lbl_titlecolor.BackColor = System.Drawing.Color.Black;
        lbl_titlecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_titlecolor.Location = new System.Drawing.Point(77, 28);
        lbl_titlecolor.Name = "lbl_titlecolor";
        lbl_titlecolor.Size = new System.Drawing.Size(37, 14);
        lbl_titlecolor.TabIndex = 4;
        lbl_titlecolor.Text = "     ";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(6, 28);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 12);
        label6.TabIndex = 139;
        label6.Text = "标题颜色：";
        txt_closed.Location = new System.Drawing.Point(187, 127);
        txt_closed.Name = "txt_closed";
        txt_closed.Size = new System.Drawing.Size(66, 21);
        txt_closed.TabIndex = 11;
        txt_ontxt.Location = new System.Drawing.Point(187, 93);
        txt_ontxt.Name = "txt_ontxt";
        txt_ontxt.Size = new System.Drawing.Size(66, 21);
        txt_ontxt.TabIndex = 9;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(120, 130);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 136;
        label3.Text = "手动文本：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(120, 96);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 135;
        label2.Text = "自动文本：";
        lbl_off.AutoSize = true;
        lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_off.Location = new System.Drawing.Point(77, 130);
        lbl_off.Name = "lbl_off";
        lbl_off.Size = new System.Drawing.Size(37, 14);
        lbl_off.TabIndex = 10;
        lbl_off.Text = "     ";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(6, 130);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(65, 12);
        label7.TabIndex = 133;
        label7.Text = "手动颜色：";
        lbl_on.AutoSize = true;
        lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_on.Location = new System.Drawing.Point(77, 96);
        lbl_on.Name = "lbl_on";
        lbl_on.Size = new System.Drawing.Size(37, 14);
        lbl_on.TabIndex = 8;
        lbl_on.Text = "     ";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(6, 96);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 131;
        label1.Text = "自动颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 285);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var2);
        base.Controls.Add(button2);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet6";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet6_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
