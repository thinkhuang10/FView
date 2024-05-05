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
        label1 = new System.Windows.Forms.Label();
        lbl_bgcolor = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        lbl_timer = new System.Windows.Forms.Label();
        cbx_font = new System.Windows.Forms.ComboBox();
        label4 = new System.Windows.Forms.Label();
        btn_OK = new System.Windows.Forms.Button();
        btn_Cancel = new System.Windows.Forms.Button();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(23, 25);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 0;
        label1.Text = "边缘颜色：";
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.Color.Blue;
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(100, 23);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(73, 14);
        lbl_bgcolor.TabIndex = 0;
        lbl_bgcolor.Text = "           ";
        lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(23, 54);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 2;
        label2.Text = "表盘颜色：";
        lbl_timer.AutoSize = true;
        lbl_timer.BackColor = System.Drawing.Color.White;
        lbl_timer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_timer.Location = new System.Drawing.Point(100, 52);
        lbl_timer.Name = "lbl_timer";
        lbl_timer.Size = new System.Drawing.Size(73, 14);
        lbl_timer.TabIndex = 1;
        lbl_timer.Text = "           ";
        lbl_timer.Click += new System.EventHandler(lbl_timer_Click);
        cbx_font.FormattingEnabled = true;
        cbx_font.Items.AddRange(new object[2] { "阿拉伯数字", "罗马数字" });
        cbx_font.Location = new System.Drawing.Point(100, 80);
        cbx_font.Name = "cbx_font";
        cbx_font.Size = new System.Drawing.Size(99, 20);
        cbx_font.TabIndex = 2;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(23, 85);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 5;
        label4.Text = "时间字体：";
        btn_OK.Location = new System.Drawing.Point(116, 123);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(57, 23);
        btn_OK.TabIndex = 3;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(btn_OK_Click);
        btn_Cancel.Location = new System.Drawing.Point(179, 123);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(57, 23);
        btn_Cancel.TabIndex = 4;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(248, 170);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.Controls.Add(label4);
        base.Controls.Add(cbx_font);
        base.Controls.Add(lbl_timer);
        base.Controls.Add(label2);
        base.Controls.Add(lbl_bgcolor);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TimerSet";
        Text = "时钟设置";
        base.Load += new System.EventHandler(TimerSet_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
