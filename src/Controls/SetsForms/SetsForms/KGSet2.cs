using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class KGSet2 : Form
{
    public string varname;

    public Color setcolor;

    public Color setbgcolor;

    public int rightflag;

    public string mark;

    public bool opstflag;

    private Button btn_Cancel;

    private Button btn_OK;

    private Label lbl_setcolor;

    private ColorDialog colorDialog1;

    private Label label1;

    private TextBox txt_var;

    private Button btn_view;

    private Label label2;

    private TextBox txt_mark;

    private CheckBox ckb_opst;

    private GroupBox groupBox1;

    private Label lbl_setbgcolor;

    private Label label3;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public KGSet2()
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
                    setcolor = lbl_setcolor.BackColor;
                    setbgcolor = lbl_setbgcolor.BackColor;
                    mark = txt_mark.Text;
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("请将信息填写完整！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("出现异常！");
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

    private void lbl_setcolor_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_setcolor.BackColor = colorDialog1.Color;
        }
    }

    private void KGSet2_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            ckb_opst.Checked = opstflag;
            txt_var.Text = varname;
            lbl_setcolor.BackColor = setcolor;
            lbl_setbgcolor.BackColor = setbgcolor;
            txt_mark.Text = mark;
        }
    }

    private void btn_Cancel_Click_1(object sender, EventArgs e)
    {
        Close();
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
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.lbl_setcolor = new System.Windows.Forms.Label();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.label1 = new System.Windows.Forms.Label();
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.txt_mark = new System.Windows.Forms.TextBox();
        this.ckb_opst = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lbl_setbgcolor = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(230, 200);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_Cancel.TabIndex = 6;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click_1);
        this.btn_OK.Location = new System.Drawing.Point(136, 200);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 5;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.lbl_setcolor.AutoSize = true;
        this.lbl_setcolor.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        this.lbl_setcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_setcolor.Location = new System.Drawing.Point(90, 66);
        this.lbl_setcolor.Name = "lbl_setcolor";
        this.lbl_setcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_setcolor.TabIndex = 3;
        this.lbl_setcolor.Text = "         ";
        this.lbl_setcolor.Click += new System.EventHandler(lbl_setcolor_Click_1);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(21, 66);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 45;
        this.label1.Text = "开关颜色：";
        this.txt_var.Location = new System.Drawing.Point(31, 20);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(209, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(251, 20);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(54, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(21, 36);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 53;
        this.label2.Text = "标签文字：";
        this.txt_mark.Location = new System.Drawing.Point(89, 31);
        this.txt_mark.Name = "txt_mark";
        this.txt_mark.Size = new System.Drawing.Size(100, 21);
        this.txt_mark.TabIndex = 2;
        this.ckb_opst.AutoSize = true;
        this.ckb_opst.Location = new System.Drawing.Point(161, 65);
        this.ckb_opst.Name = "ckb_opst";
        this.ckb_opst.Size = new System.Drawing.Size(48, 16);
        this.ckb_opst.TabIndex = 4;
        this.ckb_opst.Text = "取反";
        this.ckb_opst.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add(this.lbl_setbgcolor);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.ckb_opst);
        this.groupBox1.Controls.Add(this.txt_mark);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.lbl_setcolor);
        this.groupBox1.Location = new System.Drawing.Point(31, 49);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(274, 145);
        this.groupBox1.TabIndex = 56;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "界面配置";
        this.lbl_setbgcolor.AutoSize = true;
        this.lbl_setbgcolor.BackColor = System.Drawing.Color.Silver;
        this.lbl_setbgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_setbgcolor.Location = new System.Drawing.Point(90, 94);
        this.lbl_setbgcolor.Name = "lbl_setbgcolor";
        this.lbl_setbgcolor.Size = new System.Drawing.Size(61, 14);
        this.lbl_setbgcolor.TabIndex = 55;
        this.lbl_setbgcolor.Text = "         ";
        this.lbl_setbgcolor.Click += new System.EventHandler(lbl_setbgcolor_Click);
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(21, 96);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 54;
        this.label3.Text = "背景颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(332, 242);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet2";
        this.Text = "开关设置";
        base.Load += new System.EventHandler(KGSet2_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
