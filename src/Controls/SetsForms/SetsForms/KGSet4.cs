using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet4 : Form
{
    public string varname;

    public string titlestr;

    public string onstr;

    public string offstr;

    public Color titlecolor;

    public Color oncolor;

    public Color offcolor;

    public bool opstflag;

    private TextBox txt_var;

    private Button btn_view;

    private Button btn_Cancel;

    private Button btn_OK;

    private ColorDialog colorDialog1;

    private CheckBox ckb_opst;

    private GroupBox groupBox1;

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

    private Label label5;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet4()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (!string.IsNullOrEmpty(txt_var.Text))
                {
                    if (ckb_opst.Checked)
                    {
                        opstflag = true;
                    }
                    else
                    {
                        opstflag = false;
                    }
                    varname = txt_var.Text;
                    titlestr = txt_title.Text;
                    onstr = txt_ontxt.Text;
                    offstr = txt_closed.Text;
                    titlecolor = lbl_titlecolor.BackColor;
                    oncolor = lbl_on.BackColor;
                    offcolor = lbl_off.BackColor;
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请输入变量名！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
        }
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

    private void KGSet4_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            ckb_opst.Checked = opstflag;
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            txt_title.Text = titlestr;
            txt_ontxt.Text = onstr;
            txt_closed.Text = offstr;
            lbl_on.BackColor = oncolor;
            lbl_off.BackColor = offcolor;
            lbl_titlecolor.BackColor = titlecolor;
        }
    }

    private void lbl_titlecolor_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_titlecolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_on_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_on.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_off_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_off.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.ckb_opst = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label5 = new System.Windows.Forms.Label();
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
        this.txt_var.Location = new System.Drawing.Point(21, 14);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(154, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(192, 12);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.btn_Cancel.Location = new System.Drawing.Point(192, 226);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 10;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(112, 226);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 9;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.ckb_opst.AutoSize = true;
        this.ckb_opst.Location = new System.Drawing.Point(74, 129);
        this.ckb_opst.Name = "ckb_opst";
        this.ckb_opst.Size = new System.Drawing.Size(48, 16);
        this.ckb_opst.TabIndex = 8;
        this.ckb_opst.Text = "取反";
        this.ckb_opst.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.txt_title);
        this.groupBox1.Controls.Add(this.ckb_opst);
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
        this.groupBox1.Location = new System.Drawing.Point(22, 49);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(246, 160);
        this.groupBox1.TabIndex = 106;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "配置";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(3, 129);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 12;
        this.label5.Text = "是否取反：";
        this.txt_title.Location = new System.Drawing.Point(181, 20);
        this.txt_title.Name = "txt_title";
        this.txt_title.Size = new System.Drawing.Size(59, 21);
        this.txt_title.TabIndex = 3;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(118, 27);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 2;
        this.label4.Text = "标题文本：";
        this.lbl_titlecolor.AutoSize = true;
        this.lbl_titlecolor.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_titlecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_titlecolor.Location = new System.Drawing.Point(72, 27);
        this.lbl_titlecolor.Name = "lbl_titlecolor";
        this.lbl_titlecolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_titlecolor.TabIndex = 2;
        this.lbl_titlecolor.Text = "     ";
        this.lbl_titlecolor.Click += new System.EventHandler(lbl_titlecolor_Click_1);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(3, 27);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 12);
        this.label6.TabIndex = 0;
        this.label6.Text = "标题颜色：";
        this.txt_closed.Location = new System.Drawing.Point(181, 95);
        this.txt_closed.Name = "txt_closed";
        this.txt_closed.Size = new System.Drawing.Size(59, 21);
        this.txt_closed.TabIndex = 7;
        this.txt_ontxt.Location = new System.Drawing.Point(181, 58);
        this.txt_ontxt.Name = "txt_ontxt";
        this.txt_ontxt.Size = new System.Drawing.Size(59, 21);
        this.txt_ontxt.TabIndex = 5;
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(119, 95);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(53, 12);
        this.label3.TabIndex = 10;
        this.label3.Text = "关文本：";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(118, 61);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 6;
        this.label2.Text = "开文本：";
        this.lbl_off.AutoSize = true;
        this.lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_off.Location = new System.Drawing.Point(72, 95);
        this.lbl_off.Name = "lbl_off";
        this.lbl_off.Size = new System.Drawing.Size(37, 14);
        this.lbl_off.TabIndex = 6;
        this.lbl_off.Text = "     ";
        this.lbl_off.Click += new System.EventHandler(lbl_off_Click_1);
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(3, 95);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(53, 12);
        this.label7.TabIndex = 8;
        this.label7.Text = "关颜色：";
        this.lbl_on.AutoSize = true;
        this.lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_on.Location = new System.Drawing.Point(72, 61);
        this.lbl_on.Name = "lbl_on";
        this.lbl_on.Size = new System.Drawing.Size(37, 14);
        this.lbl_on.TabIndex = 4;
        this.lbl_on.Text = "     ";
        this.lbl_on.Click += new System.EventHandler(lbl_on_Click_1);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(3, 61);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 4;
        this.label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 264);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet4";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet4_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
