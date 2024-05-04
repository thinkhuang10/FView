using ShapeRuntime;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class setForm : Form
{
    private readonly HMIProjectFile dhp;

    private Button button1;

    private Button button2;

    private Label label1;

    private TextBox textBox1;

    private Label label2;

    private Label label3;

    private TextBox textBox2;

    private TextBox tbVarRefreshTime;

    private TextBox textBox4;

    private Label label4;

    private TextBox textBox9;

    private TextBox textBox10;

    private Label label9;

    private Label label10;

    private Label label7;

    private TextBox tbPageRefreshTime;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private CheckBox checkBox4;

    private GroupBox groupBox4;

    private RadioButton radioButton2;

    private RadioButton radioButton1;

    private TextBox tbSleepTime;

    private Label label13;

    private void setForm_Load(object sender, EventArgs e)
    {
        textBox9.Text = dhp.ProjectSize.Width.ToString();
        textBox10.Text = dhp.ProjectSize.Height.ToString();
        textBox1.Text = dhp.projectname;
        textBox2.Text = dhp.creattime.ToString();
        tbVarRefreshTime.Text = dhp.refreshtime.ToString();
        tbSleepTime.Text = dhp.gDXP_SleepTime ?? "0";
        textBox4.Text = dhp.port;
        tbPageRefreshTime.Text = ((dhp.InvalidateTime.ToString() == "0") ? "100" : dhp.InvalidateTime.ToString());
        checkBox4.Checked = dhp.Compress;
        radioButton1.Checked = !dhp.dirtyCompile;
        radioButton2.Checked = dhp.dirtyCompile;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(textBox4.Text) > 65535 || Convert.ToInt32(textBox4.Text) < 1)
            {
                MessageBox.Show("端口号请输入1-65535之间整数。\n推荐使用80、81或1024-65535之间端口，IE浏览器默认使用80端口。", "提示");
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }
            if (Convert.ToInt32(tbVarRefreshTime.Text) < 10 || Convert.ToInt32(tbVarRefreshTime.Text) > 1000)
            {
                MessageBox.Show("变量刷新时间请输入10-1000之间整数。");
                tbVarRefreshTime.Focus();
                tbVarRefreshTime.SelectAll();
                return;
            }
            if (Convert.ToInt32(tbPageRefreshTime.Text) < 10 || Convert.ToInt32(tbPageRefreshTime.Text) > 1000)
            {
                MessageBox.Show("页面刷新时间请输入10-1000之间整数。");
                tbPageRefreshTime.Focus();
                tbPageRefreshTime.SelectAll();
                return;
            }
            string value = tbSleepTime.Text.ToString().Replace(" ", "");
            try
            {
                int num = Convert.ToInt32(value);
                if (num < 0 || num > 3600)
                {
                    MessageBox.Show("请在[延时启动时间]项中输入0~3600的纯数字！");
                    tbSleepTime.Focus();
                    tbSleepTime.SelectAll();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请在[延时启动时间]项中输入0~3600的纯数字！");
                tbSleepTime.Focus();
                tbSleepTime.SelectAll();
                return;
            }
            dhp.gDXP_SleepTime = tbSleepTime.Text.ToString().Replace(" ", "");
            dhp.ProjectSize.Width = Convert.ToInt32(textBox9.Text);
            dhp.ProjectSize.Height = Convert.ToInt32(textBox10.Text);
            dhp.projectname = textBox1.Text;
            dhp.creattime = Convert.ToDateTime(textBox2.Text);
            dhp.refreshtime = Convert.ToInt32(tbVarRefreshTime.Text);
            dhp.port = textBox4.Text;
            dhp.InvalidateTime = Convert.ToInt32(tbPageRefreshTime.Text);
            dhp.Compress = checkBox4.Checked;
            dhp.dirtyCompile = radioButton2.Checked;
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch
        {
            MessageBox.Show("工程设置出现异常，请核对输入信息！", "提示");
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    public setForm(HMIProjectFile dhp)
    {
        InitializeComponent();
        this.dhp = dhp;
    }

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.tbVarRefreshTime = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBox9 = new System.Windows.Forms.TextBox();
        this.textBox10 = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.tbPageRefreshTime = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.tbSleepTime = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.checkBox4 = new System.Windows.Forms.CheckBox();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton1 = new System.Windows.Forms.RadioButton();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox4.SuspendLayout();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(181, 338);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 16;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(262, 338);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 17;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(18, 28);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 12);
        this.label1.TabIndex = 2;
        this.label1.Text = "工程名称";
        this.textBox1.Location = new System.Drawing.Point(77, 25);
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.Size = new System.Drawing.Size(228, 21);
        this.textBox1.TabIndex = 0;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(18, 55);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(53, 12);
        this.label2.TabIndex = 4;
        this.label2.Text = "创建时间";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(16, 68);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(101, 12);
        this.label3.TabIndex = 5;
        this.label3.Text = "变量刷新时间(ms)";
        this.textBox2.Location = new System.Drawing.Point(77, 52);
        this.textBox2.Name = "textBox2";
        this.textBox2.ReadOnly = true;
        this.textBox2.Size = new System.Drawing.Size(228, 21);
        this.textBox2.TabIndex = 2;
        this.tbVarRefreshTime.Location = new System.Drawing.Point(18, 83);
        this.tbVarRefreshTime.Name = "tbVarRefreshTime";
        this.tbVarRefreshTime.Size = new System.Drawing.Size(131, 21);
        this.tbVarRefreshTime.TabIndex = 4;
        this.textBox4.Location = new System.Drawing.Point(172, 82);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(131, 21);
        this.textBox4.TabIndex = 6;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(172, 68);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(29, 12);
        this.label4.TabIndex = 9;
        this.label4.Text = "端口";
        this.textBox9.Location = new System.Drawing.Point(99, 86);
        this.textBox9.Name = "textBox9";
        this.textBox9.ReadOnly = true;
        this.textBox9.Size = new System.Drawing.Size(50, 21);
        this.textBox9.TabIndex = 1;
        this.textBox10.Location = new System.Drawing.Point(255, 86);
        this.textBox10.Name = "textBox10";
        this.textBox10.ReadOnly = true;
        this.textBox10.Size = new System.Drawing.Size(50, 21);
        this.textBox10.TabIndex = 3;
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(172, 89);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(77, 12);
        this.label9.TabIndex = 19;
        this.label9.Text = "工程高度(px)";
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(16, 89);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(77, 12);
        this.label10.TabIndex = 18;
        this.label10.Text = "工程宽度(px)";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(169, 30);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(101, 12);
        this.label7.TabIndex = 22;
        this.label7.Text = "页面刷新时间(ms)";
        this.tbPageRefreshTime.Location = new System.Drawing.Point(172, 44);
        this.tbPageRefreshTime.Name = "tbPageRefreshTime";
        this.tbPageRefreshTime.Size = new System.Drawing.Size(131, 21);
        this.tbPageRefreshTime.TabIndex = 5;
        this.groupBox1.Controls.Add(this.textBox9);
        this.groupBox1.Controls.Add(this.textBox10);
        this.groupBox1.Controls.Add(this.label9);
        this.groupBox1.Controls.Add(this.label10);
        this.groupBox1.Controls.Add(this.textBox2);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.textBox1);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(20, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(325, 124);
        this.groupBox1.TabIndex = 33;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "常规信息";
        this.groupBox2.Controls.Add(this.tbSleepTime);
        this.groupBox2.Controls.Add(this.label13);
        this.groupBox2.Controls.Add(this.checkBox4);
        this.groupBox2.Controls.Add(this.tbPageRefreshTime);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.tbVarRefreshTime);
        this.groupBox2.Controls.Add(this.label3);
        this.groupBox2.Controls.Add(this.textBox4);
        this.groupBox2.Controls.Add(this.label4);
        this.groupBox2.Location = new System.Drawing.Point(20, 142);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(325, 134);
        this.groupBox2.TabIndex = 34;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "工程参数";
        this.tbSleepTime.Location = new System.Drawing.Point(18, 45);
        this.tbSleepTime.Name = "tbSleepTime";
        this.tbSleepTime.Size = new System.Drawing.Size(131, 21);
        this.tbSleepTime.TabIndex = 24;
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(15, 30);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(95, 12);
        this.label13.TabIndex = 23;
        this.label13.Text = "延时启动时间(s)";
        this.checkBox4.AutoSize = true;
        this.checkBox4.Location = new System.Drawing.Point(17, 110);
        this.checkBox4.Name = "checkBox4";
        this.checkBox4.Size = new System.Drawing.Size(72, 16);
        this.checkBox4.TabIndex = 7;
        this.checkBox4.Text = "压缩资源";
        this.checkBox4.UseVisualStyleBackColor = true;
        this.groupBox4.Controls.Add(this.radioButton2);
        this.groupBox4.Controls.Add(this.radioButton1);
        this.groupBox4.Location = new System.Drawing.Point(20, 282);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(325, 50);
        this.groupBox4.TabIndex = 36;
        this.groupBox4.TabStop = false;
        this.groupBox4.Text = "编译方式";
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(108, 25);
        this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(71, 16);
        this.radioButton2.TabIndex = 11;
        this.radioButton2.Text = "增量编译";
        this.radioButton2.UseVisualStyleBackColor = true;
        this.radioButton1.AutoSize = true;
        this.radioButton1.Checked = true;
        this.radioButton1.Location = new System.Drawing.Point(19, 25);
        this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.radioButton1.Name = "radioButton1";
        this.radioButton1.Size = new System.Drawing.Size(71, 16);
        this.radioButton1.TabIndex = 10;
        this.radioButton1.TabStop = true;
        this.radioButton1.Text = "全部编译";
        this.radioButton1.UseVisualStyleBackColor = true;
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(365, 368);
        base.Controls.Add(this.groupBox4);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "setForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "工程设置";
        base.Load += new System.EventHandler(setForm_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox4.ResumeLayout(false);
        this.groupBox4.PerformLayout();
        base.ResumeLayout(false);
    }
}
