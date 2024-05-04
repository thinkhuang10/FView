using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class TimerSet : Form
{
    public Color bgcolor = Color.Blue;

    public Color timercolor = Color.White;

    public int fonttype = 1;

    private Label label1;

    private Label lbl_bgcolor;

    private Label label2;

    private Label lbl_timer;

    private ComboBox cbx_font;

    private Label label4;

    private Button btn_OK;

    private Button btn_Cancel;

    private ColorDialog colorDialog1;

    public event checkvarnamehandler ckvarevent;

    public TimerSet()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            bgcolor = lbl_bgcolor.BackColor;
            timercolor = lbl_timer.BackColor;
            if (cbx_font.SelectedIndex == 1)
            {
                fonttype = 0;
            }
            else
            {
                fonttype = 1;
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch
        {
            MessageBox.Show("出现异常！");
        }
    }

    private void TimerSet_Load(object sender, EventArgs e)
    {
        lbl_bgcolor.BackColor = bgcolor;
        lbl_timer.BackColor = timercolor;
        if (fonttype == 0)
        {
            cbx_font.SelectedIndex = 1;
        }
        else
        {
            cbx_font.SelectedIndex = 0;
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void lbl_timer_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_timer.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_timer = new System.Windows.Forms.Label();
        this.cbx_font = new System.Windows.Forms.ComboBox();
        this.label4 = new System.Windows.Forms.Label();
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(23, 25);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "边缘颜色：";
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.Color.Blue;
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(100, 23);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(73, 14);
        this.lbl_bgcolor.TabIndex = 0;
        this.lbl_bgcolor.Text = "           ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(23, 54);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "表盘颜色：";
        this.lbl_timer.AutoSize = true;
        this.lbl_timer.BackColor = System.Drawing.Color.White;
        this.lbl_timer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_timer.Location = new System.Drawing.Point(100, 52);
        this.lbl_timer.Name = "lbl_timer";
        this.lbl_timer.Size = new System.Drawing.Size(73, 14);
        this.lbl_timer.TabIndex = 1;
        this.lbl_timer.Text = "           ";
        this.lbl_timer.Click += new System.EventHandler(lbl_timer_Click);
        this.cbx_font.FormattingEnabled = true;
        this.cbx_font.Items.AddRange(new object[2] { "阿拉伯数字", "罗马数字" });
        this.cbx_font.Location = new System.Drawing.Point(100, 80);
        this.cbx_font.Name = "cbx_font";
        this.cbx_font.Size = new System.Drawing.Size(99, 20);
        this.cbx_font.TabIndex = 2;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(23, 85);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 5;
        this.label4.Text = "时间字体：";
        this.btn_OK.Location = new System.Drawing.Point(116, 123);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(57, 23);
        this.btn_OK.TabIndex = 3;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.btn_Cancel.Location = new System.Drawing.Point(179, 123);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(57, 23);
        this.btn_Cancel.TabIndex = 4;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(248, 170);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.cbx_font);
        base.Controls.Add(this.lbl_timer);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.lbl_bgcolor);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TimerSet";
        this.Text = "时钟设置";
        base.Load += new System.EventHandler(TimerSet_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
