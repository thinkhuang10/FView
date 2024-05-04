using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class StrBtnSET : Form
{
    public string strtoshow = "";

    public Color bgcolor = Color.White;

    public Color txtcolor = Color.Black;

    public Font selectedfont = new(new FontFamily("宋体"), 13f);

    private GroupBox groupBox1;

    private Button btn_OK;

    private Label lbl_txtcolor;

    private Label label2;

    private Label 颜色;

    private TextBox txt_str;

    private Button btn_cancel;

    private Label lbl_bgcolor;

    private ColorDialog colorDialog1;

    private TextBox txt_font;

    private Label label3;

    private FontDialog fontDialog1;

    private Button button1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public StrBtnSET()
    {
        InitializeComponent();
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        strtoshow = txt_str.Text;
        bgcolor = lbl_bgcolor.BackColor;
        txtcolor = lbl_txtcolor.BackColor;
        base.DialogResult = DialogResult.OK;
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void StrBtnSET_Load(object sender, EventArgs e)
    {
        lbl_bgcolor.BackColor = bgcolor;
        lbl_txtcolor.BackColor = txtcolor;
        txt_str.Text = strtoshow;
        txt_font.Text = selectedfont.FontFamily.Name.ToString() + "," + selectedfont.Size;
    }

    private void txt_font_Click(object sender, EventArgs e)
    {
        fontDialog1.Font = selectedfont;
        if (fontDialog1.ShowDialog() == DialogResult.OK)
        {
            selectedfont = fontDialog1.Font;
            txt_font.Text = selectedfont.FontFamily.Name.ToString() + "," + selectedfont.Size;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        fontDialog1.Font = selectedfont;
        if (fontDialog1.ShowDialog() == DialogResult.OK)
        {
            selectedfont = fontDialog1.Font;
            txt_font.Text = selectedfont.FontFamily.Name.ToString() + "," + selectedfont.Size;
        }
    }

    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.button1 = new System.Windows.Forms.Button();
        this.txt_font = new System.Windows.Forms.TextBox();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.颜色 = new System.Windows.Forms.Label();
        this.txt_str = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.fontDialog1 = new System.Windows.Forms.FontDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.button1);
        this.groupBox1.Controls.Add(this.txt_font);
        this.groupBox1.Controls.Add(this.lbl_bgcolor);
        this.groupBox1.Controls.Add(this.lbl_txtcolor);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.颜色);
        this.groupBox1.Controls.Add(this.txt_str);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(292, 143);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.button1.Location = new System.Drawing.Point(216, 16);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(52, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "选择";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.txt_font.Location = new System.Drawing.Point(95, 18);
        this.txt_font.Name = "txt_font";
        this.txt_font.ReadOnly = true;
        this.txt_font.Size = new System.Drawing.Size(114, 21);
        this.txt_font.TabIndex = 6;
        this.txt_font.Click += new System.EventHandler(txt_font_Click);
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.Color.White;
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(95, 114);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_bgcolor.TabIndex = 3;
        this.lbl_bgcolor.Text = "         ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.Color.Black;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(95, 86);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_txtcolor.TabIndex = 2;
        this.lbl_txtcolor.Text = "         ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(22, 114);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "背景颜色：";
        this.颜色.AutoSize = true;
        this.颜色.Location = new System.Drawing.Point(22, 86);
        this.颜色.Name = "颜色";
        this.颜色.Size = new System.Drawing.Size(65, 12);
        this.颜色.TabIndex = 2;
        this.颜色.Text = "文字颜色：";
        this.txt_str.Location = new System.Drawing.Point(95, 51);
        this.txt_str.Name = "txt_str";
        this.txt_str.Size = new System.Drawing.Size(173, 21);
        this.txt_str.TabIndex = 1;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(22, 21);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 0;
        this.label3.Text = "字体：";
        this.btn_OK.Location = new System.Drawing.Point(124, 167);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 4;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(205, 167);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 5;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(329, 205);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "StrBtnSET";
        this.Text = "按钮设置";
        base.Load += new System.EventHandler(StrBtnSET_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
