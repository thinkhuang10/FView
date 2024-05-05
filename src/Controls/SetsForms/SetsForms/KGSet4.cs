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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        ckb_opst = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label5 = new System.Windows.Forms.Label();
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
        txt_var.Location = new System.Drawing.Point(21, 14);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(154, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(192, 12);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        btn_Cancel.Location = new System.Drawing.Point(192, 226);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 10;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(112, 226);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 9;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        ckb_opst.AutoSize = true;
        ckb_opst.Location = new System.Drawing.Point(74, 129);
        ckb_opst.Name = "ckb_opst";
        ckb_opst.Size = new System.Drawing.Size(48, 16);
        ckb_opst.TabIndex = 8;
        ckb_opst.Text = "取反";
        ckb_opst.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(txt_title);
        groupBox1.Controls.Add(ckb_opst);
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
        groupBox1.Location = new System.Drawing.Point(22, 49);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(246, 160);
        groupBox1.TabIndex = 106;
        groupBox1.TabStop = false;
        groupBox1.Text = "配置";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(3, 129);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 12;
        label5.Text = "是否取反：";
        txt_title.Location = new System.Drawing.Point(181, 20);
        txt_title.Name = "txt_title";
        txt_title.Size = new System.Drawing.Size(59, 21);
        txt_title.TabIndex = 3;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(118, 27);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 2;
        label4.Text = "标题文本：";
        lbl_titlecolor.AutoSize = true;
        lbl_titlecolor.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_titlecolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_titlecolor.Location = new System.Drawing.Point(72, 27);
        lbl_titlecolor.Name = "lbl_titlecolor";
        lbl_titlecolor.Size = new System.Drawing.Size(37, 14);
        lbl_titlecolor.TabIndex = 2;
        lbl_titlecolor.Text = "     ";
        lbl_titlecolor.Click += new System.EventHandler(lbl_titlecolor_Click_1);
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(3, 27);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 12);
        label6.TabIndex = 0;
        label6.Text = "标题颜色：";
        txt_closed.Location = new System.Drawing.Point(181, 95);
        txt_closed.Name = "txt_closed";
        txt_closed.Size = new System.Drawing.Size(59, 21);
        txt_closed.TabIndex = 7;
        txt_ontxt.Location = new System.Drawing.Point(181, 58);
        txt_ontxt.Name = "txt_ontxt";
        txt_ontxt.Size = new System.Drawing.Size(59, 21);
        txt_ontxt.TabIndex = 5;
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(119, 95);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(53, 12);
        label3.TabIndex = 10;
        label3.Text = "关文本：";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(118, 61);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 6;
        label2.Text = "开文本：";
        lbl_off.AutoSize = true;
        lbl_off.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_off.Location = new System.Drawing.Point(72, 95);
        lbl_off.Name = "lbl_off";
        lbl_off.Size = new System.Drawing.Size(37, 14);
        lbl_off.TabIndex = 6;
        lbl_off.Text = "     ";
        lbl_off.Click += new System.EventHandler(lbl_off_Click_1);
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(3, 95);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(53, 12);
        label7.TabIndex = 8;
        label7.Text = "关颜色：";
        lbl_on.AutoSize = true;
        lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_on.Location = new System.Drawing.Point(72, 61);
        lbl_on.Name = "lbl_on";
        lbl_on.Size = new System.Drawing.Size(37, 14);
        lbl_on.TabIndex = 4;
        lbl_on.Text = "     ";
        lbl_on.Click += new System.EventHandler(lbl_on_Click_1);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(3, 61);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 4;
        label1.Text = "开颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 264);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet4";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet4_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
