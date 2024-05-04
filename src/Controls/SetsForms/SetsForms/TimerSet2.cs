using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetsForms;

public class TimerSet2 : Form
{
    public Color bgcolor = Color.Yellow;

    public Color timercolor = Color.Black;

    public Color fontcolor = Color.Green;

    private Button btn_Cancel;

    private Button btn_OK;

    private Label label4;

    private Label lbl_timer;

    private Label label2;

    private Label lbl_bgcolor;

    private Label label1;

    private Label lbl_font;

    private ColorDialog colorDialog1;

    private GroupBox groupBox1;

    public event checkvarnamehandler ckvarevent;

    public TimerSet2()
    {
        InitializeComponent();
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
        try
        {
            bgcolor = lbl_bgcolor.BackColor;
            timercolor = lbl_timer.BackColor;
            fontcolor = lbl_font.BackColor;
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch
        {
            MessageBox.Show("出现异常！");
        }
    }

    private void TimerSet2_Load(object sender, EventArgs e)
    {
        lbl_bgcolor.BackColor = bgcolor;
        lbl_timer.BackColor = timercolor;
        lbl_font.BackColor = fontcolor;
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

    private void lbl_font_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_font.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.btn_OK = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.lbl_timer = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.lbl_bgcolor = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.lbl_font = new System.Windows.Forms.Label();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.btn_Cancel.Location = new System.Drawing.Point(201, 187);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(73, 23);
        this.btn_Cancel.TabIndex = 15;
        this.btn_Cancel.Text = "取消";
        this.btn_Cancel.UseVisualStyleBackColor = true;
        this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
        this.btn_OK.Location = new System.Drawing.Point(122, 187);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(73, 23);
        this.btn_OK.TabIndex = 14;
        this.btn_OK.Text = "确定";
        this.btn_OK.UseVisualStyleBackColor = true;
        this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(23, 97);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 12);
        this.label4.TabIndex = 13;
        this.label4.Text = "时间颜色：";
        this.lbl_timer.AutoSize = true;
        this.lbl_timer.BackColor = System.Drawing.Color.Black;
        this.lbl_timer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_timer.Location = new System.Drawing.Point(108, 66);
        this.lbl_timer.Name = "lbl_timer";
        this.lbl_timer.Size = new System.Drawing.Size(103, 14);
        this.lbl_timer.TabIndex = 11;
        this.lbl_timer.Text = "                ";
        this.lbl_timer.Click += new System.EventHandler(lbl_timer_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(23, 66);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(65, 12);
        this.label2.TabIndex = 10;
        this.label2.Text = "表盘颜色：";
        this.lbl_bgcolor.AutoSize = true;
        this.lbl_bgcolor.BackColor = System.Drawing.Color.FromArgb(192, 192, 0);
        this.lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_bgcolor.Location = new System.Drawing.Point(108, 37);
        this.lbl_bgcolor.Name = "lbl_bgcolor";
        this.lbl_bgcolor.Size = new System.Drawing.Size(103, 14);
        this.lbl_bgcolor.TabIndex = 9;
        this.lbl_bgcolor.Text = "                ";
        this.lbl_bgcolor.Click += new System.EventHandler(lbl_bgcolor_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(23, 37);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(65, 12);
        this.label1.TabIndex = 8;
        this.label1.Text = "边缘颜色：";
        this.lbl_font.AutoSize = true;
        this.lbl_font.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        this.lbl_font.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.lbl_font.Location = new System.Drawing.Point(108, 97);
        this.lbl_font.Name = "lbl_font";
        this.lbl_font.Size = new System.Drawing.Size(103, 14);
        this.lbl_font.TabIndex = 16;
        this.lbl_font.Text = "                ";
        this.lbl_font.Click += new System.EventHandler(lbl_font_Click);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.lbl_font);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.lbl_timer);
        this.groupBox1.Controls.Add(this.lbl_bgcolor);
        this.groupBox1.Location = new System.Drawing.Point(17, 17);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(256, 138);
        this.groupBox1.TabIndex = 17;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "颜色配置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(296, 224);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.btn_Cancel);
        base.Controls.Add(this.btn_OK);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TimerSet2";
        this.Text = "时间设置";
        base.Load += new System.EventHandler(TimerSet2_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
