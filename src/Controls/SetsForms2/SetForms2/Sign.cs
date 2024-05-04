using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetForms2;

public class Sign : Form
{
    public string txt = "注释";

    public int linewidth = 1;

    public Color txtcolor = Color.Blue;

    public Color linecolor = Color.Red;

    public Color fillcolor = Color.Green;

    public int widthtype;

    private Label label1;

    private TextBox txt_text;

    private Label label2;

    private TextBox txt_linewidth;

    private Label label3;

    private Label label4;

    private Label label5;

    private GroupBox groupBox1;

    private Label lbl_fillcolor;

    private Label lbl_linecolor;

    private Label lbl_txtcolor;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private ComboBox cbx_widthtype;

    private Label label6;

    public event viewhandler viewevent;

    public Sign()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            txt = txt_text.Text;
            linewidth = Convert.ToInt32(txt_linewidth.Text);
            txtcolor = lbl_txtcolor.BackColor;
            linecolor = lbl_linecolor.BackColor;
            fillcolor = lbl_fillcolor.BackColor;
            if (cbx_widthtype.SelectedIndex == 1)
            {
                widthtype = 1;
            }
            else
            {
                widthtype = 0;
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch
        {
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void 注释_Load(object sender, EventArgs e)
    {
        txt_text.Text = txt;
        txt_linewidth.Text = linewidth.ToString();
        lbl_txtcolor.BackColor = txtcolor;
        lbl_linecolor.BackColor = linecolor;
        lbl_fillcolor.BackColor = fillcolor;
        if (widthtype == 1)
        {
            cbx_widthtype.SelectedIndex = 1;
        }
        else
        {
            cbx_widthtype.SelectedIndex = 0;
        }
    }

    private void lbl_txtcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_txtcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_linecolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_linecolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_fillcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_fillcolor.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.txt_text = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txt_linewidth = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.cbx_widthtype = new System.Windows.Forms.ComboBox();
        this.label6 = new System.Windows.Forms.Label();
        this.lbl_fillcolor = new System.Windows.Forms.Label();
        this.lbl_linecolor = new System.Windows.Forms.Label();
        this.lbl_txtcolor = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(20, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(41, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "文字：";
        this.txt_text.Location = new System.Drawing.Point(75, 25);
        this.txt_text.Name = "txt_text";
        this.txt_text.Size = new System.Drawing.Size(49, 21);
        this.txt_text.TabIndex = 0;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(149, 29);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(41, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "线宽：";
        this.txt_linewidth.Location = new System.Drawing.Point(196, 25);
        this.txt_linewidth.Name = "txt_linewidth";
        this.txt_linewidth.Size = new System.Drawing.Size(51, 21);
        this.txt_linewidth.TabIndex = 1;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(20, 58);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(41, 12);
        this.label3.TabIndex = 4;
        this.label3.Text = "字色：";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(149, 88);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(41, 12);
        this.label4.TabIndex = 5;
        this.label4.Text = "线色：";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(19, 87);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(53, 12);
        this.label5.TabIndex = 6;
        this.label5.Text = "填充色：";
        this.groupBox1.Controls.Add(this.cbx_widthtype);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.lbl_fillcolor);
        this.groupBox1.Controls.Add(this.lbl_linecolor);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.lbl_txtcolor);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_text);
        this.groupBox1.Controls.Add(this.txt_linewidth);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(268, 121);
        this.groupBox1.TabIndex = 7;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.cbx_widthtype.FormattingEnabled = true;
        this.cbx_widthtype.Items.AddRange(new object[2] { "正常", "宽" });
        this.cbx_widthtype.Location = new System.Drawing.Point(196, 58);
        this.cbx_widthtype.Name = "cbx_widthtype";
        this.cbx_widthtype.Size = new System.Drawing.Size(51, 20);
        this.cbx_widthtype.TabIndex = 3;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(149, 58);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(41, 12);
        this.label6.TabIndex = 10;
        this.label6.Text = "字宽：";
        this.lbl_fillcolor.AutoSize = true;
        this.lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        this.lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_fillcolor.Location = new System.Drawing.Point(75, 88);
        this.lbl_fillcolor.Name = "lbl_fillcolor";
        this.lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_fillcolor.TabIndex = 4;
        this.lbl_fillcolor.Text = "       ";
        this.lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        this.lbl_linecolor.AutoSize = true;
        this.lbl_linecolor.BackColor = System.Drawing.Color.Red;
        this.lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_linecolor.Location = new System.Drawing.Point(196, 88);
        this.lbl_linecolor.Name = "lbl_linecolor";
        this.lbl_linecolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_linecolor.TabIndex = 5;
        this.lbl_linecolor.Text = "       ";
        this.lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click);
        this.lbl_txtcolor.AutoSize = true;
        this.lbl_txtcolor.BackColor = System.Drawing.Color.Blue;
        this.lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_txtcolor.Location = new System.Drawing.Point(75, 58);
        this.lbl_txtcolor.Name = "lbl_txtcolor";
        this.lbl_txtcolor.Size = new System.Drawing.Size(49, 14);
        this.lbl_txtcolor.TabIndex = 2;
        this.lbl_txtcolor.Text = "       ";
        this.lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        this.btn_OK.Location = new System.Drawing.Point(120, 139);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 6;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(201, 139);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 7;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 177);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox1);
        base.Name = "Sign";
        this.Text = "注释";
        base.Load += new System.EventHandler(注释_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
