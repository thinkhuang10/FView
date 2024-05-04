using ShapeRuntime;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class projectIOEditForm : Form
{
    private readonly string GroupName;

    public ProjectIO pio = new();

    public bool nameChanged;

    private readonly string tempname = "";

    private GroupBox groupBox1;

    private TextBox textBox6;

    private TextBox textBox5;

    private TextBox textBox4;

    private TextBox textBox3;

    private TextBox textBox2;

    private TextBox textBox1;

    private ComboBox comboBox1;

    private Button button2;

    private Button button1;

    private Label label9;

    private Label label8;

    private Label label7;

    private Label label6;

    private Label label5;

    private Label label4;

    private Label label3;

    private Label label2;

    private Label label1;

    private ComboBox comboBox3;

    private ComboBox comboBox2;

    private TextBox textBox7;

    private Label label10;

    private Label label11;

    private Label label12;


    public projectIOEditForm(string _GroupName)
    {
        InitializeComponent();

        GroupName = _GroupName;
        if (comboBox3.SelectedItem == null)
        {
            comboBox3.SelectedItem = "无";
        }
        if (comboBox2.SelectedItem == null)
        {
            comboBox2.SelectedItem = "读写";
        }
        if (comboBox1.SelectedItem == null)
        {
            comboBox1.SelectedItem = "Object";
        }
    }

    public projectIOEditForm(ProjectIO _pio)
    {
        InitializeComponent();

        pio = _pio;
        label11.Text = pio.ID.ToString();
        textBox1.Text = pio.name;
        tempname = pio.name;
        textBox2.Text = pio.tag;
        textBox3.Text = pio.description;
        comboBox1.SelectedItem = changenumtotype(new string[1] { pio.type })[0];
        comboBox2.SelectedItem = pio.access;
        comboBox3.SelectedItem = pio.emluator;
        textBox4.Text = pio.max;
        textBox5.Text = pio.min;
        textBox6.Text = pio.T;
        textBox7.Text = pio.delay;
        GroupName = pio.GroupName;
    }

    public string[] changetypetonum(string[] type)
    {
        string[] array = new string[type.Length];
        for (int i = 0; i < type.Length; i++)
        {
            switch (type[i])
            {
                case "Boolean":
                    array[i] = "0";
                    break;
                case "Int32":
                    array[i] = "5";
                    break;
                case "Single":
                    array[i] = "7";
                    break;
                case "String":
                    array[i] = "9";
                    break;
                case "Object":
                    array[i] = "1024";
                    break;
                default:
                    array[i] = "1024";
                    break;
            }
        }
        return array;
    }

    public string[] changenumtotype(string[] num)
    {
        string[] array = new string[num.Length];
        for (int i = 0; i < num.Length; i++)
        {
            switch (num[i])
            {
                case "0":
                    array[i] = "Boolean";
                    break;
                case "5":
                    array[i] = "Int32";
                    break;
                case "7":
                    array[i] = "Single";
                    break;
                case "9":
                    array[i] = "String";
                    break;
                case "1024":
                    array[i] = "Object";
                    break;
                default:
                    array[i] = "Object";
                    break;
            }
        }
        return array;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            MessageBox.Show("变量名不能为空,请重新输入", "错误");
            return;
        }
        Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
        if (!regex.IsMatch(textBox1.Text))
        {
            MessageBox.Show("变量名不合法,请重新输入", "错误");
            return;
        }
        if (CheckScript.GetCSharpKeyStrs().Contains(textBox1.Text))
        {
            MessageBox.Show("名称中含有关键字可能会导致错误。");
            return;
        }
        foreach (ProjectIO projectIO in CEditEnvironmentGlobal.dhp.ProjectIOs)
        {
            if (!(projectIO.name == tempname) && projectIO.name == textBox1.Text)
            {
                MessageBox.Show("变量" + textBox1.Text + "已经定义,请重新输入", "错误");
                return;
            }
        }
        if (comboBox3.SelectedItem == null || comboBox2.SelectedItem == null || comboBox1.SelectedItem == null)
        {
            MessageBox.Show("输入信息有误,请重新输入");
            return;
        }
        if ((string)comboBox3.SelectedItem != "无")
        {
            try
            {
                Convert.ToDouble(textBox7.Text);
                if (Convert.ToDouble(textBox4.Text) < Convert.ToDouble(textBox5.Text))
                {
                    MessageBox.Show("范围下限大于范围上限,请重新输入.");
                    return;
                }
                if (Convert.ToDouble(textBox6.Text) < 0.0)
                {
                    MessageBox.Show("输入的周期值有误,请重新输入.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("输入信息有误,请重新输入");
                return;
            }
        }
        if (pio.name != textBox1.Text)
        {
            nameChanged = true;
        }
        else
        {
            nameChanged = false;
        }
        pio.name = textBox1.Text;
        pio.tag = textBox2.Text;
        pio.description = textBox3.Text;
        pio.type = ((comboBox1.SelectedItem == null) ? "1024" : changetypetonum(new string[1] { comboBox1.SelectedItem.ToString() })[0]);
        pio.access = ((comboBox2.SelectedItem == null) ? "" : comboBox2.SelectedItem.ToString());
        pio.emluator = ((comboBox3.SelectedItem == null) ? "" : comboBox3.SelectedItem.ToString());
        pio.max = textBox4.Text;
        pio.min = textBox5.Text;
        pio.T = textBox6.Text;
        pio.delay = textBox7.Text;
        pio.GroupName = GroupName;
        CEditEnvironmentGlobal.varFileNeedReLoad = true;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBox3.SelectedIndex == 0)
        {
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }
        else
        {
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox4.Text = "100";
            textBox5.Text = "0";
            textBox6.Text = "10000";
            textBox7.Text = "0";
            comboBox2.SelectedIndex = 0;
            comboBox2.Enabled = false;
            comboBox1.Text = "Object";
            comboBox1.Enabled = false;
        }
    }

    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label12 = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.textBox7 = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.comboBox3 = new System.Windows.Forms.ComboBox();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.textBox6 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.button2 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.label12);
        this.groupBox1.Controls.Add(this.label11);
        this.groupBox1.Controls.Add(this.textBox7);
        this.groupBox1.Controls.Add(this.label10);
        this.groupBox1.Controls.Add(this.comboBox3);
        this.groupBox1.Controls.Add(this.comboBox2);
        this.groupBox1.Controls.Add(this.textBox6);
        this.groupBox1.Controls.Add(this.textBox5);
        this.groupBox1.Controls.Add(this.textBox4);
        this.groupBox1.Controls.Add(this.textBox3);
        this.groupBox1.Controls.Add(this.textBox2);
        this.groupBox1.Controls.Add(this.textBox1);
        this.groupBox1.Controls.Add(this.comboBox1);
        this.groupBox1.Controls.Add(this.label9);
        this.groupBox1.Controls.Add(this.label8);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(387, 210);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "变量属性";
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(26, 27);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(23, 12);
        this.label12.TabIndex = 24;
        this.label12.Text = "ID:";
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(55, 27);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(0, 12);
        this.label11.TabIndex = 22;
        this.textBox7.Location = new System.Drawing.Point(272, 175);
        this.textBox7.Name = "textBox7";
        this.textBox7.Size = new System.Drawing.Size(100, 21);
        this.textBox7.TabIndex = 9;
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(206, 177);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(59, 12);
        this.label10.TabIndex = 20;
        this.label10.Text = "延时(ms):";
        this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox3.FormattingEnabled = true;
        this.comboBox3.Items.AddRange(new object[6] { "无", "递增", "递减", "正弦", "三角", "随机" });
        this.comboBox3.Location = new System.Drawing.Point(248, 54);
        this.comboBox3.Name = "comboBox3";
        this.comboBox3.Size = new System.Drawing.Size(124, 20);
        this.comboBox3.TabIndex = 5;
        this.comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Items.AddRange(new object[2] { "只读", "读写" });
        this.comboBox2.Location = new System.Drawing.Point(56, 175);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(121, 20);
        this.comboBox2.TabIndex = 4;
        this.textBox6.Location = new System.Drawing.Point(272, 145);
        this.textBox6.Name = "textBox6";
        this.textBox6.Size = new System.Drawing.Size(100, 21);
        this.textBox6.TabIndex = 8;
        this.textBox5.Location = new System.Drawing.Point(272, 115);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(100, 21);
        this.textBox5.TabIndex = 7;
        this.textBox4.Location = new System.Drawing.Point(272, 84);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(100, 21);
        this.textBox4.TabIndex = 6;
        this.textBox3.Location = new System.Drawing.Point(56, 115);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(100, 21);
        this.textBox3.TabIndex = 2;
        this.textBox2.Location = new System.Drawing.Point(56, 84);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(100, 21);
        this.textBox2.TabIndex = 1;
        this.textBox1.Location = new System.Drawing.Point(56, 54);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(100, 21);
        this.textBox1.TabIndex = 0;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Items.AddRange(new object[5] { "Object", "Boolean", "Int32", "Single", "String" });
        this.comboBox1.Location = new System.Drawing.Point(56, 145);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(121, 20);
        this.comboBox1.TabIndex = 3;
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(206, 147);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(59, 12);
        this.label9.TabIndex = 8;
        this.label9.Text = "周期(ms):";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(207, 117);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(59, 12);
        this.label8.TabIndex = 7;
        this.label8.Text = "范围下限:";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(207, 87);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(59, 12);
        this.label7.TabIndex = 6;
        this.label7.Text = "范围上限:";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(207, 57);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(35, 12);
        this.label6.TabIndex = 5;
        this.label6.Text = "仿真:";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(15, 177);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(35, 12);
        this.label5.TabIndex = 4;
        this.label5.Text = "访问:";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(15, 147);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(35, 12);
        this.label4.TabIndex = 3;
        this.label4.Text = "类型:";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(15, 117);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(35, 12);
        this.label3.TabIndex = 2;
        this.label3.Text = "备注:";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(15, 87);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(35, 12);
        this.label2.TabIndex = 1;
        this.label2.Text = "标签:";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(15, 57);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(35, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "名称:";
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(302, 230);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 11;
        this.button2.Text = "取 消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button1.Location = new System.Drawing.Point(221, 230);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 10;
        this.button1.Text = "确 定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(411, 260);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.button2);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "projectIOEditForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "变量定义";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
