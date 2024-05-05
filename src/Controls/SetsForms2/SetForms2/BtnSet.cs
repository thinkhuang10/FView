using System;
using System.Windows.Forms;

namespace SetForms2;

public class BtnSet : Form
{
    public string varname;

    public float changestep = 1f;

    public float maxval = 100f;

    public float minval;

    private TextBox txt_var1;

    private Button btn_view;

    private Label label1;

    private TextBox txt_change;

    private Button btn_OK;

    private Button btn_cancel;

    private Label label2;

    private Label label3;

    private TextBox txt_maxval;

    private TextBox txt_minval;

    private GroupBox groupBox1;

    public event checkvarnamehandler ckvarevent;

    public event viewhandler viewevent;

    public BtnSet()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_var1.Text != "" && txt_change.Text != "" && Convert.ToSingle(txt_maxval.Text) > Convert.ToSingle(txt_minval.Text))
            {
                if (ckvarevent(txt_var1.Text))
                {
                    varname = txt_var1.Text;
                    changestep = Convert.ToSingle(txt_change.Text);
                    maxval = Convert.ToSingle(txt_maxval.Text);
                    minval = Convert.ToSingle(txt_minval.Text);
                    base.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("变量名称错误！");
                }
            }
            else
            {
                MessageBox.Show("信息填写有误！");
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
            txt_var1.Text = value;
        }
    }

    private void BtnSet_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(varname))
            {
                txt_var1.Text = varname.Substring(1, varname.Length - 2);
            }
            txt_change.Text = changestep.ToString();
            txt_maxval.Text = maxval.ToString();
            txt_minval.Text = minval.ToString();
        }
        catch
        {
        }
    }

    private void txt_minval_TextChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
        txt_var1 = new System.Windows.Forms.TextBox();
        btn_view = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        txt_change = new System.Windows.Forms.TextBox();
        btn_OK = new System.Windows.Forms.Button();
        btn_cancel = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        txt_maxval = new System.Windows.Forms.TextBox();
        txt_minval = new System.Windows.Forms.TextBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        txt_var1.Location = new System.Drawing.Point(25, 14);
        txt_var1.Name = "txt_var1";
        txt_var1.Size = new System.Drawing.Size(167, 21);
        txt_var1.TabIndex = 0;
        btn_view.Location = new System.Drawing.Point(198, 11);
        btn_view.Name = "btn_view";
        btn_view.Size = new System.Drawing.Size(71, 24);
        btn_view.TabIndex = 1;
        btn_view.Text = "....";
        btn_view.UseVisualStyleBackColor = true;
        btn_view.Click += new System.EventHandler(btn_view_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(35, 39);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 45;
        label1.Text = "变化量：";
        txt_change.Location = new System.Drawing.Point(109, 35);
        txt_change.Name = "txt_change";
        txt_change.Size = new System.Drawing.Size(73, 21);
        txt_change.TabIndex = 2;
        txt_change.Text = "1";
        btn_OK.Location = new System.Drawing.Point(118, 216);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(75, 23);
        btn_OK.TabIndex = 5;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_cancel.Location = new System.Drawing.Point(199, 216);
        btn_cancel.Name = "btn_cancel";
        btn_cancel.Size = new System.Drawing.Size(75, 23);
        btn_cancel.TabIndex = 6;
        btn_cancel.Text = "取消";
        btn_cancel.UseVisualStyleBackColor = true;
        btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(35, 75);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 48;
        label2.Text = "可控上限：";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(35, 111);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 12);
        label3.TabIndex = 49;
        label3.Text = "可控下限：";
        txt_maxval.Location = new System.Drawing.Point(109, 70);
        txt_maxval.Name = "txt_maxval";
        txt_maxval.Size = new System.Drawing.Size(73, 21);
        txt_maxval.TabIndex = 3;
        txt_maxval.Text = "100";
        txt_minval.Location = new System.Drawing.Point(109, 105);
        txt_minval.Name = "txt_minval";
        txt_minval.Size = new System.Drawing.Size(73, 21);
        txt_minval.TabIndex = 4;
        txt_minval.Text = "0";
        txt_minval.TextChanged += new System.EventHandler(txt_minval_TextChanged);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(txt_change);
        groupBox1.Controls.Add(txt_minval);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(txt_maxval);
        groupBox1.Controls.Add(label3);
        groupBox1.Location = new System.Drawing.Point(27, 42);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(235, 155);
        groupBox1.TabIndex = 53;
        groupBox1.TabStop = false;
        groupBox1.Text = "设置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(297, 257);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_OK);
        base.Controls.Add(btn_cancel);
        base.Controls.Add(txt_var1);
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
