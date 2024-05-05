using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class aoForm : Form
{
	private readonly CGlobal theglobal;

	private Button button1;

	private Button button2;

	private Label label1;

	private TextBox textBox1;

	private Button button3;

	private Label label2;

	private TextBox textBox2;

	private Label label3;

	private Label label4;

	private RadioGroup radioGroup1;

	private Label label5;

    public aoForm()
    {
        InitializeComponent();
    }

    public aoForm(CGlobal _theglobal)
	{
		InitializeComponent();
		theglobal = _theglobal;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "")
		{
			theglobal.SelectedShapeList[0].ao = false;
			Close();
			return;
		}
		try
		{
			int num = Convert.ToInt32(textBox2.Text);
			if (num < -1 || num > 15)
			{
				MessageBox.Show("小数保留位数填写错误.");
				return;
			}
			textBox2.Text = num.ToString();
		}
		catch (Exception)
		{
			MessageBox.Show("小数保留位数填写错误.");
			return;
		}
		try
		{
			theglobal.SelectedShapeList[0].aojingdu = Convert.ToInt32(textBox2.Text);
			theglobal.SelectedShapeList[0].aobianliang = textBox1.Text;
			if (!theglobal.SelectedShapeList[0].ao && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				theglobal.SelectedShapeList[0].ao = true;
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}
		catch (Exception)
		{
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void textBox1_Click(object sender, EventArgs e)
	{
	}

	private void aoForm_Load(object sender, EventArgs e)
	{
		try
		{
			textBox2.Text = theglobal.SelectedShapeList[0].aojingdu.ToString();
			textBox1.Text = theglobal.SelectedShapeList[0].aobianliang;
			if (theglobal.SelectedShapeList[0].aojingdu == -1)
			{
				radioGroup1.SelectedIndex = 1;
			}
			else
			{
				radioGroup1.SelectedIndex = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		string varTableEvent = CForDCCEControl.GetVarTableEvent("");
		if (varTableEvent != "")
		{
			int selectionStart = textBox1.SelectionStart;
			int selectionLength = textBox1.SelectionLength;
			string text = textBox1.Text.Remove(selectionStart, selectionLength);
			textBox1.Text = text.Insert(selectionStart, "[" + varTableEvent + "]");
		}
	}

	private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (radioGroup1.SelectedIndex == 1)
		{
			textBox2.Enabled = false;
			textBox2.Text = "-1";
		}
		else
		{
			textBox2.Enabled = true;
			textBox2.Text = "0";
		}
	}

	private void InitializeComponent()
	{
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		label1 = new System.Windows.Forms.Label();
		textBox1 = new System.Windows.Forms.TextBox();
		button3 = new System.Windows.Forms.Button();
		label2 = new System.Windows.Forms.Label();
		textBox2 = new System.Windows.Forms.TextBox();
		label3 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
		label5 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).BeginInit();
		base.SuspendLayout();
		button1.Location = new System.Drawing.Point(134, 115);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 2;
		button1.Text = "确定";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		button2.Location = new System.Drawing.Point(214, 115);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 3;
		button2.Text = "取消";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(14, 19);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(53, 12);
		label1.TabIndex = 4;
		label1.Text = "变    量";
		textBox1.HideSelection = false;
		textBox1.Location = new System.Drawing.Point(73, 15);
		textBox1.Name = "textBox1";
		textBox1.Size = new System.Drawing.Size(136, 21);
		textBox1.TabIndex = 1;
		button3.Location = new System.Drawing.Point(213, 14);
		button3.Name = "button3";
		button3.Size = new System.Drawing.Size(75, 23);
		button3.TabIndex = 0;
		button3.Text = "选择变量";
		button3.UseVisualStyleBackColor = true;
		button3.Click += new System.EventHandler(button3_Click);
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(14, 83);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(53, 12);
		label2.TabIndex = 6;
		label2.Text = "小数位数";
		textBox2.Location = new System.Drawing.Point(73, 79);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(76, 21);
		textBox2.TabIndex = 7;
		textBox2.Text = "0";
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(153, 83);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(17, 12);
		label3.TabIndex = 9;
		label3.Text = "位";
		label4.AutoSize = true;
		label4.ForeColor = System.Drawing.Color.Red;
		label4.Location = new System.Drawing.Point(177, 83);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(131, 12);
		label4.TabIndex = 10;
		label4.Text = "注:请输入0-15之间整数";
        this.radioGroup1.Location = new System.Drawing.Point(73, 45);
        this.radioGroup1.Name = "radioGroup1";
        this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[2]
        {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "十进制"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "十六进制")
        });
        radioGroup1.Size = new System.Drawing.Size(215, 24);
		radioGroup1.TabIndex = 11;
		radioGroup1.SelectedIndexChanged += new System.EventHandler(radioGroup1_SelectedIndexChanged);
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(14, 51);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(53, 12);
		label5.TabIndex = 4;
		label5.Text = "显示格式";
		base.AcceptButton = button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = button2;
		base.ClientSize = new System.Drawing.Size(308, 150);
		base.Controls.Add(radioGroup1);
		base.Controls.Add(label4);
		base.Controls.Add(label3);
		base.Controls.Add(textBox2);
		base.Controls.Add(label2);
		base.Controls.Add(button3);
		base.Controls.Add(textBox1);
		base.Controls.Add(label5);
		base.Controls.Add(label1);
		base.Controls.Add(button2);
		base.Controls.Add(button1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "aoForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "模拟量输出";
		base.Load += new System.EventHandler(aoForm_Load);
		((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
