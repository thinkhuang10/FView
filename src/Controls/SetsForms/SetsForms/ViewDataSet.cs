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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        ckb_colortrans = new System.Windows.Forms.CheckBox();
        lbl_offcolor = new System.Windows.Forms.Label();
        lbl_oncolor = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        lbl_backcolor = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        ckb_1 = new System.Windows.Forms.CheckBox();
        txt_count2 = new System.Windows.Forms.TextBox();
        txt_count1 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        cb_tran = new System.Windows.Forms.CheckBox();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var.Cursor = System.Windows.Forms.Cursors.IBeam;
        txt_var.Location = new System.Drawing.Point(36, 17);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(226, 15);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(24, 33);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 24;
        label1.Text = "整数位数：";
        groupBox1.Controls.Add(cb_tran);
        groupBox1.Controls.Add(ckb_colortrans);
        groupBox1.Controls.Add(lbl_offcolor);
        groupBox1.Controls.Add(lbl_oncolor);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(lbl_backcolor);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(ckb_1);
        groupBox1.Controls.Add(txt_count2);
        groupBox1.Controls.Add(txt_count1);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(34, 46);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(266, 180);
        groupBox1.TabIndex = 25;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
        ckb_colortrans.AutoSize = true;
        ckb_colortrans.Location = new System.Drawing.Point(140, 135);
        ckb_colortrans.Name = "ckb_colortrans";
        ckb_colortrans.Size = new System.Drawing.Size(60, 16);
        ckb_colortrans.TabIndex = 6;
        ckb_colortrans.Text = "透明色";
        ckb_colortrans.UseVisualStyleBackColor = true;
        ckb_colortrans.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
        lbl_offcolor.AutoSize = true;
        lbl_offcolor.BackColor = System.Drawing.Color.Green;
        lbl_offcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_offcolor.Location = new System.Drawing.Point(92, 101);
        lbl_offcolor.Name = "lbl_offcolor";
        lbl_offcolor.Size = new System.Drawing.Size(37, 14);
        lbl_offcolor.TabIndex = 8;
        lbl_offcolor.Tag = "        ";
        lbl_offcolor.Text = "     ";
        lbl_offcolor.Click += new System.EventHandler(label8_Click);
        lbl_oncolor.AutoSize = true;
        lbl_oncolor.BackColor = System.Drawing.Color.Lime;
        lbl_oncolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_oncolor.Location = new System.Drawing.Point(92, 67);
        lbl_oncolor.Name = "lbl_oncolor";
        lbl_oncolor.Size = new System.Drawing.Size(37, 14);
        lbl_oncolor.TabIndex = 7;
        lbl_oncolor.Tag = " ";
        lbl_oncolor.Text = "     ";
        lbl_oncolor.Click += new System.EventHandler(lbl_oncolor_Click);
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(24, 103);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 12);
        label6.TabIndex = 32;
        label6.Text = "灯灭颜色：";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(24, 68);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(65, 12);
        label5.TabIndex = 31;
        label5.Text = "灯亮颜色：";
        lbl_backcolor.AutoSize = true;
        lbl_backcolor.BackColor = System.Drawing.Color.Black;
        lbl_backcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_backcolor.Location = new System.Drawing.Point(92, 137);
        lbl_backcolor.Name = "lbl_backcolor";
        lbl_backcolor.Size = new System.Drawing.Size(37, 14);
        lbl_backcolor.TabIndex = 5;
        lbl_backcolor.Text = "     ";
        lbl_backcolor.Click += new System.EventHandler(lbl_backcolor_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(24, 139);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 29;
        label3.Text = "底板颜色：";
        ckb_1.AutoSize = true;
        ckb_1.Location = new System.Drawing.Point(140, 67);
        ckb_1.Name = "ckb_1";
        ckb_1.Size = new System.Drawing.Size(72, 16);
        ckb_1.TabIndex = 4;
        ckb_1.Text = "显示符号";
        ckb_1.UseVisualStyleBackColor = true;
        ckb_1.CheckedChanged += new System.EventHandler(ckb_1_CheckedChanged);
        txt_count2.Location = new System.Drawing.Point(207, 29);
        txt_count2.Name = "txt_count2";
        txt_count2.Size = new System.Drawing.Size(40, 21);
        txt_count2.TabIndex = 3;
        txt_count1.Location = new System.Drawing.Point(92, 30);
        txt_count1.Name = "txt_count1";
        txt_count1.Size = new System.Drawing.Size(40, 21);
        txt_count1.TabIndex = 2;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(138, 33);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 25;
        label2.Text = "小数位数：";
        btn_OK.Location = new System.Drawing.Point(145, 246);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 9;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(226, 246);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 10;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        cb_tran.AutoSize = true;
        cb_tran.Location = new System.Drawing.Point(140, 101);
        cb_tran.Name = "cb_tran";
        cb_tran.Size = new System.Drawing.Size(60, 16);
        cb_tran.TabIndex = 33;
        cb_tran.Text = "透明色";
        cb_tran.UseVisualStyleBackColor = true;
        cb_tran.CheckedChanged += new System.EventHandler(cb_tran_CheckedChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(330, 296);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(groupBox1);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ViewDataSet";
        Text = "数码管设置";
        base.Load += new System.EventHandler(ViewDataSet_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
