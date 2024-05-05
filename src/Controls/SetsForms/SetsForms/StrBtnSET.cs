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

    public StrBtnSET()
    {
        InitializeComponent();
    }

    private void Btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Btn_OK_Click(object sender, EventArgs e)
    {
        strtoshow = txt_str.Text;
        bgcolor = lbl_bgcolor.BackColor;
        txtcolor = lbl_txtcolor.BackColor;
        DialogResult = DialogResult.OK;
    }

    private void Lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void Lbl_bgcolor_Click(object sender, EventArgs e)
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

    private void Txt_font_Click(object sender, EventArgs e)
    {
        fontDialog1.Font = selectedfont;
        if (fontDialog1.ShowDialog() == DialogResult.OK)
        {
            selectedfont = fontDialog1.Font;
            txt_font.Text = selectedfont.FontFamily.Name.ToString() + "," + selectedfont.Size;
        }
    }

    private void Button1_Click(object sender, EventArgs e)
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
        groupBox1 = new System.Windows.Forms.GroupBox();
        button1 = new System.Windows.Forms.Button();
        txt_font = new System.Windows.Forms.TextBox();
        lbl_bgcolor = new System.Windows.Forms.Label();
        lbl_txtcolor = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        颜色 = new System.Windows.Forms.Label();
        txt_str = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        fontDialog1 = new System.Windows.Forms.FontDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(button1);
        groupBox1.Controls.Add(txt_font);
        groupBox1.Controls.Add(lbl_bgcolor);
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(颜色);
        groupBox1.Controls.Add(txt_str);
        groupBox1.Controls.Add(label3);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(292, 143);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        button1.Location = new System.Drawing.Point(216, 16);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(52, 23);
        button1.TabIndex = 0;
        button1.Text = "选择";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        txt_font.Location = new System.Drawing.Point(95, 18);
        txt_font.Name = "txt_font";
        txt_font.ReadOnly = true;
        txt_font.Size = new System.Drawing.Size(114, 21);
        txt_font.TabIndex = 6;
        txt_font.Click += new System.EventHandler(Txt_font_Click);
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.Color.White;
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(95, 114);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(61, 14);
        lbl_bgcolor.TabIndex = 3;
        lbl_bgcolor.Text = "         ";
        lbl_bgcolor.Click += new System.EventHandler(Lbl_bgcolor_Click);
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.Color.Black;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(95, 86);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(61, 14);
        lbl_txtcolor.TabIndex = 2;
        lbl_txtcolor.Text = "         ";
        lbl_txtcolor.Click += new System.EventHandler(Lbl_txtcolor_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(22, 114);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 3;
        label2.Text = "背景颜色：";
        颜色.AutoSize = true;
        颜色.Location = new System.Drawing.Point(22, 86);
        颜色.Name = "颜色";
        颜色.Size = new System.Drawing.Size(65, 12);
        颜色.TabIndex = 2;
        颜色.Text = "文字颜色：";
        txt_str.Location = new System.Drawing.Point(95, 51);
        txt_str.Name = "txt_str";
        txt_str.Size = new System.Drawing.Size(173, 21);
        txt_str.TabIndex = 1;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(22, 21);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(41, 12);
        label3.TabIndex = 0;
        label3.Text = "字体：";
        btn_OK.Location = new System.Drawing.Point(124, 167);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 4;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(Btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(205, 167);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 5;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(Btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(329, 205);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "StrBtnSET";
        Text = "按钮设置";
        base.Load += new System.EventHandler(StrBtnSET_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
