using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet1 : Form
{
    public string varname = "";

    public Color setcolor = Color.Gray;

    public Color setbgcolor = Color.Gray;

    public bool oppsiteflag;

    private TextBox txt_var;

    private Button btn_view;

    private Label label1;

    private Label lbl_setcolor;

    private Button btn_OK;

    private Button btn_Cancel;

    private ColorDialog colorDialog1;

    private CheckBox ckb_getopst;

    private GroupBox groupBox1;

    private Label label2;

    private Label lbl_setbgcolor;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet1()
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
                    varname = txt_var.Text;
                    setcolor = lbl_setcolor.BackColor;
                    setbgcolor = lbl_setbgcolor.BackColor;
                    if (ckb_getopst.Checked)
                    {
                        oppsiteflag = true;
                    }
                    else
                    {
                        oppsiteflag = false;
                    }
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请输入变量值！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常");
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_setcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_setcolor.BackColor = colorDialog1.Color;
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

    private void KGSet1_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            ckb_getopst.Checked = oppsiteflag;
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            lbl_setcolor.BackColor = setcolor;
            lbl_setbgcolor.BackColor = setbgcolor;
        }
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void lbl_setbgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_setbgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_setcolor = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.ckb_getopst = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_setbgcolor = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(25, 16);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(200, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(231, 14);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(60, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(19, 57);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 35;
        this.label1.Text = "开关颜色：";
        this.label1.Click += new System.EventHandler(label1_Click);
        this.lbl_setcolor.AutoSize = true;
        this.lbl_setcolor.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_setcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_setcolor.Location = new System.Drawing.Point(90, 57);
        this.lbl_setcolor.Name = "lbl_setcolor";
        this.lbl_setcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_setcolor.TabIndex = 2;
        this.lbl_setcolor.Text = "         ";
        this.lbl_setcolor.Click += new System.EventHandler(lbl_setcolor_Click);
        this.btn_OK.Location = new System.Drawing.Point(136, 153);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 25);
        this.btn_OK.TabIndex = 4;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_Cancel.Location = new System.Drawing.Point(217, 153);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 25);
        this.btn_Cancel.TabIndex = 5;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.ckb_getopst.AutoSize = true;
        this.ckb_getopst.Location = new System.Drawing.Point(157, 57);
        this.ckb_getopst.Name = "ckb_getopst";
        this.ckb_getopst.Size = new System.Drawing.Size(48, 16);
        this.ckb_getopst.TabIndex = 3;
        this.ckb_getopst.Text = "取反";
        this.ckb_getopst.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add(this.lbl_setbgcolor);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.ckb_getopst);
        this.groupBox1.Controls.Add(this.lbl_setcolor);
        this.groupBox1.Location = new System.Drawing.Point(25, 49);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(266, 86);
        this.groupBox1.TabIndex = 44;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色配置";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(19, 29);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 37;
        this.label2.Text = "背景颜色：";
        this.lbl_setbgcolor.AutoSize = true;
        this.lbl_setbgcolor.BackColor = System.Drawing.Color.Gray;
        this.lbl_setbgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_setbgcolor.Location = new System.Drawing.Point(90, 28);
        this.lbl_setbgcolor.Name = "lbl_setbgcolor";
        this.lbl_setbgcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_setbgcolor.TabIndex = 38;
        this.lbl_setbgcolor.Text = "         ";
        this.lbl_setbgcolor.Click += new System.EventHandler(lbl_setbgcolor_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(319, 197);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet1";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet1_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
