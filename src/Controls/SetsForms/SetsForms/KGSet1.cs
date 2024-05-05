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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        lbl_setcolor = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_Cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        ckb_getopst = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        label2 = new System.Windows.Forms.Label();
        lbl_setbgcolor = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(25, 16);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(200, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(231, 14);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(60, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(19, 57);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 35;
        label1.Text = "开关颜色：";
        label1.Click += new System.EventHandler(label1_Click);
        lbl_setcolor.AutoSize = true;
        lbl_setcolor.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_setcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_setcolor.Location = new System.Drawing.Point(90, 57);
        lbl_setcolor.Name = "lbl_setcolor";
        lbl_setcolor.Size = new System.Drawing.Size(61, 14);
        lbl_setcolor.TabIndex = 2;
        lbl_setcolor.Text = "         ";
        lbl_setcolor.Click += new System.EventHandler(lbl_setcolor_Click);
        btn_OK.Location = new System.Drawing.Point(136, 153);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 25);
        btn_OK.TabIndex = 4;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_Cancel.Location = new System.Drawing.Point(217, 153);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 25);
        btn_Cancel.TabIndex = 5;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        ckb_getopst.AutoSize = true;
        ckb_getopst.Location = new System.Drawing.Point(157, 57);
        ckb_getopst.Name = "ckb_getopst";
        ckb_getopst.Size = new System.Drawing.Size(48, 16);
        ckb_getopst.TabIndex = 3;
        ckb_getopst.Text = "取反";
        ckb_getopst.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(lbl_setbgcolor);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(ckb_getopst);
        groupBox1.Controls.Add(lbl_setcolor);
        groupBox1.Location = new System.Drawing.Point(25, 49);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(266, 86);
        groupBox1.TabIndex = 44;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色配置";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(19, 29);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 37;
        label2.Text = "背景颜色：";
        lbl_setbgcolor.AutoSize = true;
        lbl_setbgcolor.BackColor = System.Drawing.Color.Gray;
        lbl_setbgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_setbgcolor.Location = new System.Drawing.Point(90, 28);
        lbl_setbgcolor.Name = "lbl_setbgcolor";
        lbl_setbgcolor.Size = new System.Drawing.Size(61, 14);
        lbl_setbgcolor.TabIndex = 38;
        lbl_setbgcolor.Text = "         ";
        lbl_setbgcolor.Click += new System.EventHandler(lbl_setbgcolor_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(319, 197);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet1";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet1_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
