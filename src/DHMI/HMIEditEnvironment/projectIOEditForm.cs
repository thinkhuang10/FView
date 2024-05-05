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
        groupBox1 = new System.Windows.Forms.GroupBox();
        label12 = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        textBox7 = new System.Windows.Forms.TextBox();
        label10 = new System.Windows.Forms.Label();
        comboBox3 = new System.Windows.Forms.ComboBox();
        comboBox2 = new System.Windows.Forms.ComboBox();
        textBox6 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        textBox1 = new System.Windows.Forms.TextBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        label9 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        button2 = new System.Windows.Forms.Button();
        button1 = new System.Windows.Forms.Button();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(label12);
        groupBox1.Controls.Add(label11);
        groupBox1.Controls.Add(textBox7);
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(comboBox3);
        groupBox1.Controls.Add(comboBox2);
        groupBox1.Controls.Add(textBox6);
        groupBox1.Controls.Add(textBox5);
        groupBox1.Controls.Add(textBox4);
        groupBox1.Controls.Add(textBox3);
        groupBox1.Controls.Add(textBox2);
        groupBox1.Controls.Add(textBox1);
        groupBox1.Controls.Add(comboBox1);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(label8);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(387, 210);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "变量属性";
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(26, 27);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(23, 12);
        label12.TabIndex = 24;
        label12.Text = "ID:";
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(55, 27);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(0, 12);
        label11.TabIndex = 22;
        textBox7.Location = new System.Drawing.Point(272, 175);
        textBox7.Name = "textBox7";
        textBox7.Size = new System.Drawing.Size(100, 21);
        textBox7.TabIndex = 9;
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(206, 177);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(59, 12);
        label10.TabIndex = 20;
        label10.Text = "延时(ms):";
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Items.AddRange(new object[6] { "无", "递增", "递减", "正弦", "三角", "随机" });
        comboBox3.Location = new System.Drawing.Point(248, 54);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(124, 20);
        comboBox3.TabIndex = 5;
        comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Items.AddRange(new object[2] { "只读", "读写" });
        comboBox2.Location = new System.Drawing.Point(56, 175);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(121, 20);
        comboBox2.TabIndex = 4;
        textBox6.Location = new System.Drawing.Point(272, 145);
        textBox6.Name = "textBox6";
        textBox6.Size = new System.Drawing.Size(100, 21);
        textBox6.TabIndex = 8;
        textBox5.Location = new System.Drawing.Point(272, 115);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(100, 21);
        textBox5.TabIndex = 7;
        textBox4.Location = new System.Drawing.Point(272, 84);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(100, 21);
        textBox4.TabIndex = 6;
        textBox3.Location = new System.Drawing.Point(56, 115);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(100, 21);
        textBox3.TabIndex = 2;
        textBox2.Location = new System.Drawing.Point(56, 84);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(100, 21);
        textBox2.TabIndex = 1;
        textBox1.Location = new System.Drawing.Point(56, 54);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(100, 21);
        textBox1.TabIndex = 0;
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[5] { "Object", "Boolean", "Int32", "Single", "String" });
        comboBox1.Location = new System.Drawing.Point(56, 145);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(121, 20);
        comboBox1.TabIndex = 3;
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(206, 147);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(59, 12);
        label9.TabIndex = 8;
        label9.Text = "周期(ms):";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(207, 117);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(59, 12);
        label8.TabIndex = 7;
        label8.Text = "范围下限:";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(207, 87);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(59, 12);
        label7.TabIndex = 6;
        label7.Text = "范围上限:";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(207, 57);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(35, 12);
        label6.TabIndex = 5;
        label6.Text = "仿真:";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(15, 177);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(35, 12);
        label5.TabIndex = 4;
        label5.Text = "访问:";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(15, 147);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(35, 12);
        label4.TabIndex = 3;
        label4.Text = "类型:";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(15, 117);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(35, 12);
        label3.TabIndex = 2;
        label3.Text = "备注:";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(15, 87);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(35, 12);
        label2.TabIndex = 1;
        label2.Text = "标签:";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(15, 57);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(35, 12);
        label1.TabIndex = 0;
        label1.Text = "名称:";
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(302, 230);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 11;
        button2.Text = "取 消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button1.Location = new System.Drawing.Point(221, 230);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 10;
        button1.Text = "确 定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(411, 260);
        base.Controls.Add(groupBox1);
        base.Controls.Add(button1);
        base.Controls.Add(button2);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "projectIOEditForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "变量定义";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}
