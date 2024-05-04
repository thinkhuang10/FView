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
                if (this.ckvarevent(txt_var1.Text))
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
        string value = this.viewevent();
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
        this.txt_var1 = new System.Windows.Forms.TextBox();
        this.btn_view = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.txt_change = new System.Windows.Forms.TextBox();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_cancel = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txt_maxval = new System.Windows.Forms.TextBox();
        this.txt_minval = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.txt_var1.Location = new System.Drawing.Point(25, 14);
        this.txt_var1.Name = "txt_var1";
        this.txt_var1.Size = new System.Drawing.Size(167, 21);
        this.txt_var1.TabIndex = 0;
        this.btn_view.Location = new System.Drawing.Point(198, 11);
        this.btn_view.Name = "btn_view";
        this.btn_view.Size = new System.Drawing.Size(71, 24);
        this.btn_view.TabIndex = 1;
        this.btn_view.Text = "....";
        this.btn_view.UseVisualStyleBackColor = true;
        this.btn_view.Click += new System.EventHandler(btn_view_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(35, 39);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 45;
        this.label1.Text = "变化量：";
        this.txt_change.Location = new System.Drawing.Point(109, 35);
        this.txt_change.Name = "txt_change";
        this.txt_change.Size = new System.Drawing.Size(73, 21);
        this.txt_change.TabIndex = 2;
        this.txt_change.Text = "1";
        this.btn_OK.Location = new System.Drawing.Point(118, 216);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(75, 23);
        this.btn_OK.TabIndex = 5;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_cancel.Location = new System.Drawing.Point(199, 216);
        this.btn_cancel.Name = "btn_cancel";
        this.btn_cancel.Size = new System.Drawing.Size(75, 23);
        this.btn_cancel.TabIndex = 6;
        this.btn_cancel.Text = "取消";
        this.btn_cancel.UseVisualStyleBackColor = true;
        this.btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(35, 75);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 48;
        this.label2.Text = "可控上限：";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(35, 111);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(65, 12);
        this.label3.TabIndex = 49;
        this.label3.Text = "可控下限：";
        this.txt_maxval.Location = new System.Drawing.Point(109, 70);
        this.txt_maxval.Name = "txt_maxval";
        this.txt_maxval.Size = new System.Drawing.Size(73, 21);
        this.txt_maxval.TabIndex = 3;
        this.txt_maxval.Text = "100";
        this.txt_minval.Location = new System.Drawing.Point(109, 105);
        this.txt_minval.Name = "txt_minval";
        this.txt_minval.Size = new System.Drawing.Size(73, 21);
        this.txt_minval.TabIndex = 4;
        this.txt_minval.Text = "0";
        this.txt_minval.TextChanged += new System.EventHandler(txt_minval_TextChanged);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.txt_change);
        this.groupBox1.Controls.Add(this.txt_minval);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txt_maxval);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Location = new System.Drawing.Point(27, 42);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(235, 155);
        this.groupBox1.TabIndex = 53;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "设置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(297, 257);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.btn_cancel);
        base.Controls.Add(this.txt_var1);
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
