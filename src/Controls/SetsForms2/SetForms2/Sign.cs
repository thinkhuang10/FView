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
        label1 = new System.Windows.Forms.Label();
        txt_text = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        txt_linewidth = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        cbx_widthtype = new System.Windows.Forms.ComboBox();
        label6 = new System.Windows.Forms.Label();
        lbl_fillcolor = new System.Windows.Forms.Label();
        lbl_linecolor = new System.Windows.Forms.Label();
        lbl_txtcolor = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(20, 29);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 12);
        label1.TabIndex = 0;
        label1.Text = "文字：";
        txt_text.Location = new System.Drawing.Point(75, 25);
        txt_text.Name = "txt_text";
        txt_text.Size = new System.Drawing.Size(49, 21);
        txt_text.TabIndex = 0;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(149, 29);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(41, 12);
        label2.TabIndex = 2;
        label2.Text = "线宽：";
        txt_linewidth.Location = new System.Drawing.Point(196, 25);
        txt_linewidth.Name = "txt_linewidth";
        txt_linewidth.Size = new System.Drawing.Size(51, 21);
        txt_linewidth.TabIndex = 1;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(20, 58);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(41, 12);
        label3.TabIndex = 4;
        label3.Text = "字色：";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(149, 88);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(41, 12);
        label4.TabIndex = 5;
        label4.Text = "线色：";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(19, 87);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(53, 12);
        label5.TabIndex = 6;
        label5.Text = "填充色：";
        groupBox1.Controls.Add(cbx_widthtype);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(lbl_fillcolor);
        groupBox1.Controls.Add(lbl_linecolor);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_txtcolor);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_text);
        groupBox1.Controls.Add(txt_linewidth);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(268, 121);
        groupBox1.TabIndex = 7;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        cbx_widthtype.FormattingEnabled = true;
        cbx_widthtype.Items.AddRange(new object[2] { "正常", "宽" });
        cbx_widthtype.Location = new System.Drawing.Point(196, 58);
        cbx_widthtype.Name = "cbx_widthtype";
        cbx_widthtype.Size = new System.Drawing.Size(51, 20);
        cbx_widthtype.TabIndex = 3;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(149, 58);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(41, 12);
        label6.TabIndex = 10;
        label6.Text = "字宽：";
        lbl_fillcolor.AutoSize = true;
        lbl_fillcolor.BackColor = System.Drawing.Color.Lime;
        lbl_fillcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_fillcolor.Location = new System.Drawing.Point(75, 88);
        lbl_fillcolor.Name = "lbl_fillcolor";
        lbl_fillcolor.Size = new System.Drawing.Size(49, 14);
        lbl_fillcolor.TabIndex = 4;
        lbl_fillcolor.Text = "       ";
        lbl_fillcolor.Click += new System.EventHandler(lbl_fillcolor_Click);
        lbl_linecolor.AutoSize = true;
        lbl_linecolor.BackColor = System.Drawing.Color.Red;
        lbl_linecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_linecolor.Location = new System.Drawing.Point(196, 88);
        lbl_linecolor.Name = "lbl_linecolor";
        lbl_linecolor.Size = new System.Drawing.Size(49, 14);
        lbl_linecolor.TabIndex = 5;
        lbl_linecolor.Text = "       ";
        lbl_linecolor.Click += new System.EventHandler(lbl_linecolor_Click);
        lbl_txtcolor.AutoSize = true;
        lbl_txtcolor.BackColor = System.Drawing.Color.Blue;
        lbl_txtcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_txtcolor.Location = new System.Drawing.Point(75, 58);
        lbl_txtcolor.Name = "lbl_txtcolor";
        lbl_txtcolor.Size = new System.Drawing.Size(49, 14);
        lbl_txtcolor.TabIndex = 2;
        lbl_txtcolor.Text = "       ";
        lbl_txtcolor.Click += new System.EventHandler(lbl_txtcolor_Click);
        btn_OK.Location = new System.Drawing.Point(120, 139);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 6;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(201, 139);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 7;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 177);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox1);
        base.Name = "Sign";
        Text = "注释";
        base.Load += new System.EventHandler(注释_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
