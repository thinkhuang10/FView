using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class BtnSet : Form
{
    public string Varname;

    public float ChangeStep = 1f;

    public Color CtrlColor = Color.Green;

    public float maxval = 100f;

    public float minval;

    private TextBox txt_var;

    private Button btn_view;

    private Button btn_OK;

    private Button btn_cancel;

    private ColorDialog colorDialog1;

    private GroupBox groupBox1;

    private TextBox txt_minval;

    private TextBox txt_maxval;

    private Label label3;

    private Label label4;

    private Label lbl_color;

    private Label label2;

    private Label label1;

    private TextBox txt_change;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BtnSet()
    {
        InitializeComponent();
    }

    private void BtnSet_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Varname))
        {
            txt_var.Text = Varname.Substring(1, Varname.Length - 2);
        }
        lbl_color.BackColor = CtrlColor;
        txt_change.Text = ChangeStep.ToString();
        txt_maxval.Text = maxval.ToString();
        txt_minval.Text = minval.ToString();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ckvarevent(txt_var.Text))
            {
                if (txt_var.Text != "" && txt_change.Text != "" && Convert.ToSingle(txt_maxval.Text) > Convert.ToSingle(txt_minval.Text))
                {
                    Varname = txt_var.Text;
                    ChangeStep = Convert.ToSingle(txt_change.Text);
                    CtrlColor = lbl_color.BackColor;
                    maxval = Convert.ToSingle(txt_maxval.Text);
                    minval = Convert.ToSingle(txt_minval.Text);
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("信息有误！");
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

    private void lbl_color_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_color_Click_1(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_color.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.txt_var = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.txt_minval = new System.Windows.Forms.TextBox();
        this.txt_maxval = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.lbl_color = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.txt_change = new System.Windows.Forms.TextBox();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var.Location = new System.Drawing.Point(15, 21);
        this.txt_var.Name = "txt_var";
        this.txt_var.Size = new System.Drawing.Size(184, 21);
        this.txt_var.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(206, 21);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(75, 23);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.btn_OK.Location = new System.Drawing.Point(126, 217);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 6;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(203, 217);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 7;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.groupBox1.Controls.Add(this.txt_minval);
        this.groupBox1.Controls.Add(this.txt_maxval);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.lbl_color);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txt_change);
        this.groupBox1.Location = new System.Drawing.Point(15, 60);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(263, 142);
        this.groupBox1.TabIndex = 56;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        this.txt_minval.Location = new System.Drawing.Point(110, 75);
        this.txt_minval.Name = "txt_minval";
        this.txt_minval.Size = new System.Drawing.Size(147, 21);
        this.txt_minval.TabIndex = 4;
        this.txt_minval.Text = "1";
        this.txt_maxval.Location = new System.Drawing.Point(110, 48);
        this.txt_maxval.Name = "txt_maxval";
        this.txt_maxval.Size = new System.Drawing.Size(147, 21);
        this.txt_maxval.TabIndex = 3;
        this.txt_maxval.Text = "100";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(6, 81);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(77, 12);
        this.label3.TabIndex = 61;
        this.label3.Text = "调节最小值：";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(6, 53);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(77, 12);
        this.label4.TabIndex = 60;
        this.label4.Text = "调节最大值：";
        this.lbl_color.AutoSize = true;
        this.lbl_color.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_color.Location = new System.Drawing.Point(110, 109);
        this.lbl_color.Name = "lbl_color";
        this.lbl_color.Size = new System.Drawing.Size(61, 14);
        this.lbl_color.TabIndex = 5;
        this.lbl_color.Text = "         ";
        this.lbl_color.Click += new System.EventHandler(lbl_color_Click_1);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(6, 109);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 58;
        this.label2.Text = "箭头颜色：";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 57;
        this.label1.Text = "变化量：";
        this.txt_change.Location = new System.Drawing.Point(110, 21);
        this.txt_change.Name = "txt_change";
        this.txt_change.Size = new System.Drawing.Size(147, 21);
        this.txt_change.TabIndex = 2;
        this.txt_change.Text = "1";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(295, 254);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.txt_var);
        base.Controls.Add(this.btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet";
        this.Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
