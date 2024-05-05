using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class UserTypeADD : XtraForm
{
	private IContainer components = null;

	private Label label1;

	private TextBox textBox1;

	private Button button1;

	private Button button2;

	private CheckedListBox checkedListBox1;

	private Label label2;

	private HelpProvider helpProvider1;

	private List<SafeRegions> SafeRegions;

	public object[] obj = new object[2];

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
		this.label2 = new System.Windows.Forms.Label();
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(26, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(55, 14);
		this.label1.TabIndex = 4;
		this.label1.Text = "工种名称";
		this.textBox1.Location = new System.Drawing.Point(94, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(116, 22);
		this.textBox1.TabIndex = 0;
		this.button1.Location = new System.Drawing.Point(28, 398);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 27);
		this.button1.TabIndex = 2;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(122, 398);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(87, 27);
		this.button2.TabIndex = 3;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.checkedListBox1.FormattingEnabled = true;
		this.checkedListBox1.Location = new System.Drawing.Point(35, 77);
		this.checkedListBox1.Name = "checkedListBox1";
		this.checkedListBox1.Size = new System.Drawing.Size(165, 310);
		this.checkedListBox1.TabIndex = 1;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(33, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(71, 14);
		this.label2.TabIndex = 5;
		this.label2.Text = "默认安全区:";
		this.helpProvider1.HelpNamespace = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(237, 439);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.checkedListBox1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "10.10.1.5添加工种.htm");
		this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.MaximizeBox = false;
		base.Name = "UserTypeADD";
		this.helpProvider1.SetShowHelp(this, true);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "添加工种";
		base.Load += new System.EventHandler(UserTypeADD_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public UserTypeADD()
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
	}

	public UserTypeADD(List<SafeRegions> SafeRegions)
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		if (SafeRegions != null)
		{
			this.SafeRegions = new List<SafeRegions>(SafeRegions);
		}
		else
		{
			this.SafeRegions = new List<SafeRegions>();
		}
	}

	private void UserTypeADD_Load(object sender, EventArgs e)
	{
		base.AcceptButton = button1;
		base.CancelButton = button2;
		foreach (SafeRegions safeRegion in SafeRegions)
		{
			checkedListBox1.Items.Add(safeRegion);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "")
		{
			MessageBox.Show("请输入名称");
			return;
		}
		obj[0] = textBox1.Text;
		List<int> list = new List<int>();
		foreach (object checkedItem in checkedListBox1.CheckedItems)
		{
			list.Add(((SafeRegions)checkedItem).Id);
		}
		obj[1] = list;
		base.DialogResult = DialogResult.OK;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
