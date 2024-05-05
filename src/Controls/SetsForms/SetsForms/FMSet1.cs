using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class FMSet1 : Form
{
    public string varname;

    public Color oncolor = Color.Green;

    public Color offcolor = Color.Red;

    private Label lbl_off;

    private Label label11;

    private Button btn_Cancel;

    private Button btn_OK;

    private Label lbl_on;

    private TextBox txt_var;

    private Button btn_view;

    private Label label1;

    private GroupBox groupBox1;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public FMSet1()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ckvarevent(txt_var.Text))
            {
                if (txt_var.Text == "")
                {
                    MessageBox.Show("变量名不能为空！");
                    return;
                }
                varname = txt_var.Text;
                oncolor = lbl_on.BackColor;
                offcolor = lbl_off.BackColor;
                base.DialogResult = DialogResult.OK;
                Close();
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

    private void DJSet1_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(varname))
        {
            txt_var.Text = varname.Substring(1, varname.Length - 2);
            lbl_on.BackColor = oncolor;
            lbl_off.BackColor = offcolor;
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

    private void InitializeComponent()
    {
        lbl_off = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        lbl_on = new System.Windows.Forms.Label();
        txt_var = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        lbl_off.AutoSize = true;
        lbl_off.BackColor = System.Drawing.Color.Red;
        lbl_off.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_off.Location = new System.Drawing.Point(195, 28);
        lbl_off.Name = "lbl_off";
        lbl_off.Size = new System.Drawing.Size(37, 14);
        lbl_off.TabIndex = 3;
        lbl_off.Text = "     ";
        lbl_off.Click += new System.EventHandler(lbl_off_Click);
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(135, 28);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(53, 12);
        label11.TabIndex = 135;
        label11.Text = "关颜色：";
        btn_Cancel.Location = new System.Drawing.Point(197, 115);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(75, 23);
        btn_Cancel.TabIndex = 5;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(116, 115);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 4;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        lbl_on.AutoSize = true;
        lbl_on.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_on.Location = new System.Drawing.Point(82, 28);
        lbl_on.Name = "lbl_on";
        lbl_on.Size = new System.Drawing.Size(37, 14);
        lbl_on.TabIndex = 2;
        lbl_on.Text = "     ";
        lbl_on.Click += new System.EventHandler(lbl_on_Click);
        txt_var.Location = new System.Drawing.Point(22, 12);
        txt_var.Name = "txt_var";
        txt_var.Size = new System.Drawing.Size(154, 21);
        txt_var.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(197, 10);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(75, 23);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(20, 28);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 131;
        label1.Text = "开颜色：";
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(lbl_off);
        groupBox1.Controls.Add(lbl_on);
        groupBox1.Controls.Add(label11);
        groupBox1.Location = new System.Drawing.Point(22, 39);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(250, 61);
        groupBox1.TabIndex = 137;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色设置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(292, 149);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(txt_var);
        base.Controls.Add(btn_view);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "FMSet1";
        Text = "配置界面";
        base.Load += new System.EventHandler(DJSet1_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
