using ShapeRuntime;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class SetForm : Form
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

    private void SetForm_Load(object sender, EventArgs e)
    {
        textBox9.Text = dhp.ProjectSize.Width.ToString();
        textBox10.Text = dhp.ProjectSize.Height.ToString();
        textBox1.Text = dhp.ProjectName;
        textBox2.Text = dhp.CreatTime.ToString();
        tbVarRefreshTime.Text = dhp.RefreshTime.ToString();
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
            dhp.ProjectName = textBox1.Text;
            dhp.CreatTime = Convert.ToDateTime(textBox2.Text);
            dhp.RefreshTime = Convert.ToInt32(tbVarRefreshTime.Text);
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

    private void Button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    public SetForm(HMIProjectFile dhp)
    {
        InitializeComponent();
        this.dhp = dhp;
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        tbVarRefreshTime = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        textBox9 = new System.Windows.Forms.TextBox();
        textBox10 = new System.Windows.Forms.TextBox();
        label9 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        tbPageRefreshTime = new System.Windows.Forms.TextBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox2 = new System.Windows.Forms.GroupBox();
        tbSleepTime = new System.Windows.Forms.TextBox();
        label13 = new System.Windows.Forms.Label();
        checkBox4 = new System.Windows.Forms.CheckBox();
        groupBox4 = new System.Windows.Forms.GroupBox();
        radioButton2 = new System.Windows.Forms.RadioButton();
        radioButton1 = new System.Windows.Forms.RadioButton();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox4.SuspendLayout();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(181, 338);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 16;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(262, 338);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 17;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(18, 28);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 2;
        label1.Text = "工程名称";
        textBox1.Location = new System.Drawing.Point(77, 25);
        textBox1.Name = "textBox1";
        textBox1.ReadOnly = true;
        textBox1.Size = new System.Drawing.Size(228, 21);
        textBox1.TabIndex = 0;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(18, 55);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(53, 12);
        label2.TabIndex = 4;
        label2.Text = "创建时间";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(16, 68);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(101, 12);
        label3.TabIndex = 5;
        label3.Text = "变量刷新时间(ms)";
        textBox2.Location = new System.Drawing.Point(77, 52);
        textBox2.Name = "textBox2";
        textBox2.ReadOnly = true;
        textBox2.Size = new System.Drawing.Size(228, 21);
        textBox2.TabIndex = 2;
        tbVarRefreshTime.Location = new System.Drawing.Point(18, 83);
        tbVarRefreshTime.Name = "tbVarRefreshTime";
        tbVarRefreshTime.Size = new System.Drawing.Size(131, 21);
        tbVarRefreshTime.TabIndex = 4;
        textBox4.Location = new System.Drawing.Point(172, 82);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(131, 21);
        textBox4.TabIndex = 6;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(172, 68);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(29, 12);
        label4.TabIndex = 9;
        label4.Text = "端口";
        textBox9.Location = new System.Drawing.Point(99, 86);
        textBox9.Name = "textBox9";
        textBox9.ReadOnly = true;
        textBox9.Size = new System.Drawing.Size(50, 21);
        textBox9.TabIndex = 1;
        textBox10.Location = new System.Drawing.Point(255, 86);
        textBox10.Name = "textBox10";
        textBox10.ReadOnly = true;
        textBox10.Size = new System.Drawing.Size(50, 21);
        textBox10.TabIndex = 3;
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(172, 89);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(77, 12);
        label9.TabIndex = 19;
        label9.Text = "工程高度(px)";
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(16, 89);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(77, 12);
        label10.TabIndex = 18;
        label10.Text = "工程宽度(px)";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(169, 30);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(101, 12);
        label7.TabIndex = 22;
        label7.Text = "页面刷新时间(ms)";
        tbPageRefreshTime.Location = new System.Drawing.Point(172, 44);
        tbPageRefreshTime.Name = "tbPageRefreshTime";
        tbPageRefreshTime.Size = new System.Drawing.Size(131, 21);
        tbPageRefreshTime.TabIndex = 5;
        groupBox1.Controls.Add(textBox9);
        groupBox1.Controls.Add(textBox10);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(textBox2);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(textBox1);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(20, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(325, 124);
        groupBox1.TabIndex = 33;
        groupBox1.TabStop = false;
        groupBox1.Text = "常规信息";
        groupBox2.Controls.Add(tbSleepTime);
        groupBox2.Controls.Add(label13);
        groupBox2.Controls.Add(checkBox4);
        groupBox2.Controls.Add(tbPageRefreshTime);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(tbVarRefreshTime);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(textBox4);
        groupBox2.Controls.Add(label4);
        groupBox2.Location = new System.Drawing.Point(20, 142);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(325, 134);
        groupBox2.TabIndex = 34;
        groupBox2.TabStop = false;
        groupBox2.Text = "工程参数";
        tbSleepTime.Location = new System.Drawing.Point(18, 45);
        tbSleepTime.Name = "tbSleepTime";
        tbSleepTime.Size = new System.Drawing.Size(131, 21);
        tbSleepTime.TabIndex = 24;
        label13.AutoSize = true;
        label13.Location = new System.Drawing.Point(15, 30);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(95, 12);
        label13.TabIndex = 23;
        label13.Text = "延时启动时间(s)";
        checkBox4.AutoSize = true;
        checkBox4.Location = new System.Drawing.Point(17, 110);
        checkBox4.Name = "checkBox4";
        checkBox4.Size = new System.Drawing.Size(72, 16);
        checkBox4.TabIndex = 7;
        checkBox4.Text = "压缩资源";
        checkBox4.UseVisualStyleBackColor = true;
        groupBox4.Controls.Add(radioButton2);
        groupBox4.Controls.Add(radioButton1);
        groupBox4.Location = new System.Drawing.Point(20, 282);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new System.Drawing.Size(325, 50);
        groupBox4.TabIndex = 36;
        groupBox4.TabStop = false;
        groupBox4.Text = "编译方式";
        radioButton2.AutoSize = true;
        radioButton2.Location = new System.Drawing.Point(108, 25);
        radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new System.Drawing.Size(71, 16);
        radioButton2.TabIndex = 11;
        radioButton2.Text = "增量编译";
        radioButton2.UseVisualStyleBackColor = true;
        radioButton1.AutoSize = true;
        radioButton1.Checked = true;
        radioButton1.Location = new System.Drawing.Point(19, 25);
        radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new System.Drawing.Size(71, 16);
        radioButton1.TabIndex = 10;
        radioButton1.TabStop = true;
        radioButton1.Text = "全部编译";
        radioButton1.UseVisualStyleBackColor = true;
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(365, 368);
        base.Controls.Add(groupBox4);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "setForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "工程设置";
        base.Load += new System.EventHandler(SetForm_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        base.ResumeLayout(false);
    }
}
