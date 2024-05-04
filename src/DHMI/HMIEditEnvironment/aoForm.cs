using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class aoForm : Form
{
	private CGlobal theglobal;

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
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button3 = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
		this.label5 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.radioGroup1.Properties).BeginInit();
		base.SuspendLayout();
		this.button1.Location = new System.Drawing.Point(134, 115);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.button2.Location = new System.Drawing.Point(214, 115);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 3;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(14, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "变    量";
		this.textBox1.HideSelection = false;
		this.textBox1.Location = new System.Drawing.Point(73, 15);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(136, 21);
		this.textBox1.TabIndex = 1;
		this.button3.Location = new System.Drawing.Point(213, 14);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 0;
		this.button3.Text = "选择变量";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(14, 83);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 6;
		this.label2.Text = "小数位数";
		this.textBox2.Location = new System.Drawing.Point(73, 79);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(76, 21);
		this.textBox2.TabIndex = 7;
		this.textBox2.Text = "0";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(153, 83);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(17, 12);
		this.label3.TabIndex = 9;
		this.label3.Text = "位";
		this.label4.AutoSize = true;
		this.label4.ForeColor = System.Drawing.Color.Red;
		this.label4.Location = new System.Drawing.Point(177, 83);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(131, 12);
		this.label4.TabIndex = 10;
		this.label4.Text = "注:请输入0-15之间整数";
		this.radioGroup1.Location = new System.Drawing.Point(73, 45);
		this.radioGroup1.Name = "radioGroup1";
		this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[2]
		{
			new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "十进制"),
			new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "十六进制")
		});
		this.radioGroup1.Size = new System.Drawing.Size(215, 24);
		this.radioGroup1.TabIndex = 11;
		this.radioGroup1.SelectedIndexChanged += new System.EventHandler(radioGroup1_SelectedIndexChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(14, 51);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 12);
		this.label5.TabIndex = 4;
		this.label5.Text = "显示格式";
		base.AcceptButton = this.button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.button2;
		base.ClientSize = new System.Drawing.Size(308, 150);
		base.Controls.Add(this.radioGroup1);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "aoForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "模拟量输出";
		base.Load += new System.EventHandler(aoForm_Load);
		((System.ComponentModel.ISupportInitialize)this.radioGroup1.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
