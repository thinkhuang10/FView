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
            if (ckvarevent(txt_var.Text))
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
        string value = viewevent();
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
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1 = new System.Windows.Forms.GroupBox();
        txt_minval = new System.Windows.Forms.TextBox();
        txt_maxval = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        lbl_color = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        txt_change = new System.Windows.Forms.TextBox();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var.Location = new System.Drawing.Point(15, 21);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(184, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(206, 21);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        btn_OK.Location = new System.Drawing.Point(126, 217);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 6;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(203, 217);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 7;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        groupBox1.Controls.Add(txt_minval);
        groupBox1.Controls.Add(txt_maxval);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_color);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txt_change);
        groupBox1.Location = new System.Drawing.Point(15, 60);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(263, 142);
        groupBox1.TabIndex = 56;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        txt_minval.Location = new System.Drawing.Point(110, 75);
        txt_minval.Name = "txt_minval";
        txt_minval.Size = new System.Drawing.Size(147, 21);
        txt_minval.TabIndex = 4;
        txt_minval.Text = "1";
        txt_maxval.Location = new System.Drawing.Point(110, 48);
        txt_maxval.Name = "txt_maxval";
        txt_maxval.Size = new System.Drawing.Size(147, 21);
        txt_maxval.TabIndex = 3;
        txt_maxval.Text = "100";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(6, 81);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(77, 12);
        label3.TabIndex = 61;
        label3.Text = "调节最小值：";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(6, 53);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(77, 12);
        label4.TabIndex = 60;
        label4.Text = "调节最大值：";
        lbl_color.AutoSize = true;
        lbl_color.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_color.Location = new System.Drawing.Point(110, 109);
        lbl_color.Name = "lbl_color";
        lbl_color.Size = new System.Drawing.Size(61, 14);
        lbl_color.TabIndex = 5;
        lbl_color.Text = "         ";
        lbl_color.Click += new System.EventHandler(lbl_color_Click_1);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(6, 109);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 58;
        label2.Text = "箭头颜色：";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(6, 25);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 57;
        label1.Text = "变化量：";
        txt_change.Location = new System.Drawing.Point(110, 21);
        txt_change.Name = "txt_change";
        txt_change.Size = new System.Drawing.Size(147, 21);
        txt_change.TabIndex = 2;
        txt_change.Text = "1";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(295, 254);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "BtnSet";
        Text = "按钮设置";
        base.Load += new System.EventHandler(BtnSet_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
