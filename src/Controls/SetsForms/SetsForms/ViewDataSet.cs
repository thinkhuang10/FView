using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class ViewDataSet : Form
{
    public string varname;

    public int intcount = 4;

    public int dbcount = 2;

    public bool flag1;

    public bool colortranflag;

    public bool offcolortran;

    public Color bgcolor = Color.Black;

    public Color oncolor = Color.FromArgb(255, 128, 255, 0);

    public Color offcolor = Color.FromArgb(255, 43, 65, 22);

    private TextBox txt_var;

    private Button btn_view;

    private Label label1;

    private GroupBox groupBox1;

    private TextBox txt_count1;

    private Label label2;

    private CheckBox ckb_1;

    private TextBox txt_count2;

    private Label lbl_oncolor;

    private Label label6;

    private Label label5;

    private Label lbl_backcolor;

    private Label label3;

    private Label lbl_offcolor;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private CheckBox ckb_colortrans;

    private CheckBox cb_tran;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public ViewDataSet()
    {
        InitializeComponent();
    }

    private void label8_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_offcolor.BackColor = colorDialog1.Color;
        }
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (txt_var.Text != "" && Convert.ToInt32(txt_count1.Text) >= 0 && Convert.ToInt32(txt_count2.Text) >= 0)
                {
                    varname = txt_var.Text;
                    intcount = Convert.ToInt32(txt_count1.Text);
                    dbcount = Convert.ToInt32(txt_count2.Text);
                    if (ckb_1.Checked)
                    {
                        flag1 = true;
                    }
                    else
                    {
                        flag1 = false;
                    }
                    bgcolor = lbl_backcolor.BackColor;
                    oncolor = lbl_oncolor.BackColor;
                    offcolor = lbl_offcolor.BackColor;
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else if (Convert.ToInt32(txt_count2.Text) > 5)
                {
                    MessageBox.Show("小数位数不能大于5");
                }
                else
                {
                    MessageBox.Show("信息填写有误或不完整！");
                }
            }
            else
            {
                MessageBox.Show("变量名称错误！");
            }
        }
        catch
        {
            MessageBox.Show("信息填写有误！");
        }
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_view_Click(object sender, EventArgs e)
    {
        string value = this.viewevent();
        if (!string.IsNullOrEmpty(value))
        {
            txt_var.Text = value;
        }
    }

    private void lbl_backcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_backcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_oncolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_oncolor.BackColor = colorDialog1.Color;
        }
    }

    private void ckb_1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (!ckb_colortrans.Checked)
        {
            lbl_backcolor.Visible = true;
            if (bgcolor == Color.Transparent)
            {
                lbl_backcolor.BackColor = Color.Black;
            }
            else
            {
                lbl_backcolor.BackColor = bgcolor;
            }
            colortranflag = false;
        }
        else
        {
            lbl_backcolor.Visible = false;
            colortranflag = true;
        }
    }

    private void ViewDataSet_Load(object sender, EventArgs e)
    {
        txt_var.Text = varname;
        txt_count1.Text = intcount.ToString();
        txt_count2.Text = dbcount.ToString();
        lbl_backcolor.BackColor = bgcolor;
        lbl_oncolor.BackColor = oncolor;
        lbl_offcolor.BackColor = offcolor;
        if (flag1)
        {
            ckb_1.Checked = true;
        }
        else
        {
            ckb_1.Checked = false;
        }
        if (colortranflag)
        {
            ckb_colortrans.Checked = true;
        }
        else
        {
            ckb_colortrans.Checked = false;
        }
        if (offcolortran)
        {
            cb_tran.Checked = true;
        }
        else
        {
            cb_tran.Checked = false;
        }
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void cb_tran_CheckedChanged(object sender, EventArgs e)
    {
        if (!cb_tran.Checked)
        {
            lbl_offcolor.Visible = true;
            if (offcolor == Color.Transparent)
            {
                lbl_offcolor.BackColor = Color.Black;
            }
            else
            {
                lbl_offcolor.BackColor = offcolor;
            }
            offcolortran = false;
        }
        else
        {
            lbl_offcolor.Visible = false;
            offcolortran = true;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.ckb_colortrans = new System.Windows.Forms.CheckBox();
        this.lbl_offcolor = new System.Windows.Forms.Label();
        this.lbl_oncolor = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.lbl_backcolor = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.ckb_1 = new System.Windows.Forms.CheckBox();
        this.txt_count2 = new System.Windows.Forms.TextBox();
        this.txt_count1 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.cb_tran = new System.Windows.Forms.CheckBox();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Cursor = System.Windows.Forms.Cursors.IBeam;
        this.txt_var.Location = new System.Drawing.Point(36, 17);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(226, 15);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(24, 33);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 24;
        this.label1.Text = "整数位数：";
        this.groupBox1.Controls.Add(this.cb_tran);
        this.groupBox1.Controls.Add(this.ckb_colortrans);
        this.groupBox1.Controls.Add(this.lbl_offcolor);
        this.groupBox1.Controls.Add(this.lbl_oncolor);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.lbl_backcolor);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.ckb_1);
        this.groupBox1.Controls.Add(this.txt_count2);
        this.groupBox1.Controls.Add(this.txt_count1);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(34, 46);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(266, 180);
        this.groupBox1.TabIndex = 25;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
        this.ckb_colortrans.AutoSize = true;
        this.ckb_colortrans.Location = new System.Drawing.Point(140, 135);
        this.ckb_colortrans.Name = "ckb_colortrans";
        this.ckb_colortrans.Size = new System.Drawing.Size(60, 16);
        this.ckb_colortrans.TabIndex = 6;
        this.ckb_colortrans.Text = "透明色";
        this.ckb_colortrans.UseVisualStyleBackColor = true;
        this.ckb_colortrans.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        this.lbl_offcolor.AutoSize = true;
        this.lbl_offcolor.BackColor = System.Drawing.Color.Green;
        this.lbl_offcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_offcolor.Location = new System.Drawing.Point(92, 101);
        this.lbl_offcolor.Name = "lbl_offcolor";
        this.lbl_offcolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_offcolor.TabIndex = 8;
        this.lbl_offcolor.Tag = "        ";
        this.lbl_offcolor.Text = "     ";
        this.lbl_offcolor.Click += new System.EventHandler(label8_Click);
        this.lbl_oncolor.AutoSize = true;
        this.lbl_oncolor.BackColor = System.Drawing.Color.Lime;
        this.lbl_oncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_oncolor.Location = new System.Drawing.Point(92, 67);
        this.lbl_oncolor.Name = "lbl_oncolor";
        this.lbl_oncolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_oncolor.TabIndex = 7;
        this.lbl_oncolor.Tag = " ";
        this.lbl_oncolor.Text = "     ";
        this.lbl_oncolor.Click += new System.EventHandler(lbl_oncolor_Click);
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(24, 103);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(65, 12);
        this.label6.TabIndex = 32;
        this.label6.Text = "灯灭颜色：";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(24, 68);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(65, 12);
        this.label5.TabIndex = 31;
        this.label5.Text = "灯亮颜色：";
        this.lbl_backcolor.AutoSize = true;
        this.lbl_backcolor.BackColor = System.Drawing.Color.Black;
        this.lbl_backcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_backcolor.Location = new System.Drawing.Point(92, 137);
        this.lbl_backcolor.Name = "lbl_backcolor";
        this.lbl_backcolor.Size = new System.Drawing.Size(37, 14);
        this.lbl_backcolor.TabIndex = 5;
        this.lbl_backcolor.Text = "     ";
        this.lbl_backcolor.Click += new System.EventHandler(lbl_backcolor_Click);
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(24, 139);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 29;
        this.label3.Text = "底板颜色：";
        this.ckb_1.AutoSize = true;
        this.ckb_1.Location = new System.Drawing.Point(140, 67);
        this.ckb_1.Name = "ckb_1";
        this.ckb_1.Size = new System.Drawing.Size(72, 16);
        this.ckb_1.TabIndex = 4;
        this.ckb_1.Text = "显示符号";
        this.ckb_1.UseVisualStyleBackColor = true;
        this.ckb_1.CheckedChanged += new System.EventHandler(ckb_1_CheckedChanged);
        this.txt_count2.Location = new System.Drawing.Point(207, 29);
        this.txt_count2.Name = "txt_count2";
        this.txt_count2.Size = new System.Drawing.Size(40, 21);
        this.txt_count2.TabIndex = 3;
        this.txt_count1.Location = new System.Drawing.Point(92, 30);
        this.txt_count1.Name = "txt_count1";
        this.txt_count1.Size = new System.Drawing.Size(40, 21);
        this.txt_count1.TabIndex = 2;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(138, 33);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 25;
        this.label2.Text = "小数位数：";
        this.btn_OK.Location = new System.Drawing.Point(145, 246);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 9;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(226, 246);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 10;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.cb_tran.AutoSize = true;
        this.cb_tran.Location = new System.Drawing.Point(140, 101);
        this.cb_tran.Name = "cb_tran";
        this.cb_tran.Size = new System.Drawing.Size(60, 16);
        this.cb_tran.TabIndex = 33;
        this.cb_tran.Text = "透明色";
        this.cb_tran.UseVisualStyleBackColor = true;
        this.cb_tran.CheckedChanged += new System.EventHandler(cb_tran_CheckedChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(330, 296);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ViewDataSet";
        this.Text = "数码管设置";
        base.Load += new System.EventHandler(ViewDataSet_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
