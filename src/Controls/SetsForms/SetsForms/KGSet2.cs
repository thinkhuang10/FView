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
        string value = viewevent();
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
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        lbl_setcolor = new System.Windows.Forms.Label();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        label1 = new System.Windows.Forms.Label();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        txt_mark = new System.Windows.Forms.TextBox();
        ckb_opst = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        lbl_setbgcolor = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(230, 200);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 6;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click_1);
        btn_OK.Location = new System.Drawing.Point(136, 200);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 5;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        lbl_setcolor.AutoSize = true;
        lbl_setcolor.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
        lbl_setcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_setcolor.Location = new System.Drawing.Point(90, 66);
        lbl_setcolor.Name = "lbl_setcolor";
        lbl_setcolor.Size = new System.Drawing.Size(61, 14);
        lbl_setcolor.TabIndex = 3;
        lbl_setcolor.Text = "         ";
        lbl_setcolor.Click += new System.EventHandler(lbl_setcolor_Click_1);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(21, 66);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 45;
        label1.Text = "开关颜色：";
        txt_var.Location = new System.Drawing.Point(31, 20);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(209, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(251, 20);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(54, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(21, 36);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 53;
        label2.Text = "标签文字：";
        txt_mark.Location = new System.Drawing.Point(89, 31);
        txt_mark.Name = "txt_mark";
        txt_mark.Size = new System.Drawing.Size(100, 21);
        txt_mark.TabIndex = 2;
        ckb_opst.AutoSize = true;
        ckb_opst.Location = new System.Drawing.Point(161, 65);
        ckb_opst.Name = "ckb_opst";
        ckb_opst.Size = new System.Drawing.Size(48, 16);
        ckb_opst.TabIndex = 4;
        ckb_opst.Text = "取反";
        ckb_opst.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(lbl_setbgcolor);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(ckb_opst);
        groupBox1.Controls.Add(txt_mark);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(lbl_setcolor);
        groupBox1.Location = new System.Drawing.Point(31, 49);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(274, 145);
        groupBox1.TabIndex = 56;
        groupBox1.TabStop = false;
        groupBox1.Text = "界面配置";
        lbl_setbgcolor.AutoSize = true;
        lbl_setbgcolor.BackColor = System.Drawing.Color.Silver;
        lbl_setbgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_setbgcolor.Location = new System.Drawing.Point(90, 94);
        lbl_setbgcolor.Name = "lbl_setbgcolor";
        lbl_setbgcolor.Size = new System.Drawing.Size(61, 14);
        lbl_setbgcolor.TabIndex = 55;
        lbl_setbgcolor.Text = "         ";
        lbl_setbgcolor.Click += new System.EventHandler(lbl_setbgcolor_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(21, 96);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 54;
        label3.Text = "背景颜色：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(332, 242);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "KGSet2";
        Text = "开关设置";
        base.Load += new System.EventHandler(KGSet2_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
