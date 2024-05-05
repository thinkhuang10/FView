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

    public TimerSet2()
    {
        InitializeComponent();
    }

    private void Btn_OK_Click(object sender, EventArgs e)
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

    private void Btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Lbl_bgcolor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_bgcolor.BackColor = colorDialog1.Color;
        }
    }

    private void Lbl_timer_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_timer.BackColor = colorDialog1.Color;
        }
    }

    private void Lbl_font_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            lbl_font.BackColor = colorDialog1.Color;
        }
    }

    private void InitializeComponent()
    {
        btn_Cancel = new System.Windows.Forms.Button();
        btn_OK = new System.Windows.Forms.Button();
        label4 = new System.Windows.Forms.Label();
        lbl_timer = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        lbl_bgcolor = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        lbl_font = new System.Windows.Forms.Label();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        btn_Cancel.Location = new System.Drawing.Point(201, 187);
        btn_Cancel.Name = "btn_Cancel";
        btn_Cancel.Size = new System.Drawing.Size(73, 23);
        btn_Cancel.TabIndex = 15;
        btn_Cancel.Text = "取消";
        btn_Cancel.UseVisualStyleBackColor = true;
        btn_Cancel.Click += new System.EventHandler(Btn_Cancel_Click);
        btn_OK.Location = new System.Drawing.Point(122, 187);
        btn_OK.Name = "btn_OK";
        btn_OK.Size = new System.Drawing.Size(73, 23);
        btn_OK.TabIndex = 14;
        btn_OK.Text = "确定";
        btn_OK.UseVisualStyleBackColor = true;
        btn_OK.Click += new System.EventHandler(Btn_OK_Click);
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(23, 97);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(65, 12);
        label4.TabIndex = 13;
        label4.Text = "时间颜色：";
        lbl_timer.AutoSize = true;
        lbl_timer.BackColor = System.Drawing.Color.Black;
        lbl_timer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_timer.Location = new System.Drawing.Point(108, 66);
        lbl_timer.Name = "lbl_timer";
        lbl_timer.Size = new System.Drawing.Size(103, 14);
        lbl_timer.TabIndex = 11;
        lbl_timer.Text = "                ";
        lbl_timer.Click += new System.EventHandler(Lbl_timer_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(23, 66);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(65, 12);
        label2.TabIndex = 10;
        label2.Text = "表盘颜色：";
        lbl_bgcolor.AutoSize = true;
        lbl_bgcolor.BackColor = System.Drawing.Color.FromArgb(192, 192, 0);
        lbl_bgcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_bgcolor.Location = new System.Drawing.Point(108, 37);
        lbl_bgcolor.Name = "lbl_bgcolor";
        lbl_bgcolor.Size = new System.Drawing.Size(103, 14);
        lbl_bgcolor.TabIndex = 9;
        lbl_bgcolor.Text = "                ";
        lbl_bgcolor.Click += new System.EventHandler(Lbl_bgcolor_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(23, 37);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 12);
        label1.TabIndex = 8;
        label1.Text = "边缘颜色：";
        lbl_font.AutoSize = true;
        lbl_font.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
        lbl_font.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        lbl_font.Location = new System.Drawing.Point(108, 97);
        lbl_font.Name = "lbl_font";
        lbl_font.Size = new System.Drawing.Size(103, 14);
        lbl_font.TabIndex = 16;
        lbl_font.Text = "                ";
        lbl_font.Click += new System.EventHandler(Lbl_font_Click);
        groupBox1.Controls.Add(label1);
        groupBox1.Controls.Add(lbl_font);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(lbl_timer);
        groupBox1.Controls.Add(lbl_bgcolor);
        groupBox1.Location = new System.Drawing.Point(17, 17);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(256, 138);
        groupBox1.TabIndex = 17;
        groupBox1.TabStop = false;
        groupBox1.Text = "颜色配置";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(296, 224);
        base.Controls.Add(groupBox1);
        base.Controls.Add(btn_Cancel);
        base.Controls.Add(btn_OK);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TimerSet2";
        Text = "时间设置";
        base.Load += new System.EventHandler(TimerSet2_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
