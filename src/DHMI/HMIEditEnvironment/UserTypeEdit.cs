using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class UserTypeEdit : XtraForm
{
	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox textBox1;

	private TextBox textBox2;

	private Button button1;

	private Button button2;

	private CheckedListBox checkedListBox1;

	private Label label3;

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
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
		this.label3 = new System.Windows.Forms.Label();
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(33, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 14);
		this.label1.TabIndex = 5;
		this.label1.Text = "旧名称";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(33, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(43, 14);
		this.label2.TabIndex = 6;
		this.label2.Text = "新名称";
		this.textBox1.Location = new System.Drawing.Point(87, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(116, 22);
		this.textBox1.TabIndex = 4;
		this.textBox2.Location = new System.Drawing.Point(87, 56);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(116, 22);
		this.textBox2.TabIndex = 0;
		this.button1.Location = new System.Drawing.Point(35, 418);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 27);
		this.button1.TabIndex = 2;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(129, 418);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(87, 27);
		this.button2.TabIndex = 3;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.checkedListBox1.FormattingEnabled = true;
		this.checkedListBox1.Location = new System.Drawing.Point(42, 114);
		this.checkedListBox1.Name = "checkedListBox1";
		this.checkedListBox1.Size = new System.Drawing.Size(165, 293);
		this.checkedListBox1.TabIndex = 1;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(40, 97);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(71, 14);
		this.label3.TabIndex = 7;
		this.label3.Text = "默认安全区:";
		this.helpProvider1.HelpNamespace = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(250, 468);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.checkedListBox1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "10.10.1.6修改工种.htm");
		this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.MaximizeBox = false;
		base.Name = "UserTypeEdit";
		this.helpProvider1.SetShowHelp(this, true);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "编辑工种";
		base.Load += new System.EventHandler(UserTypeEdit_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public UserTypeEdit()
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
	}

	public UserTypeEdit(UserType ut, List<SafeRegions> SafeRegions)
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		textBox1.Text = ut.UserTypeName;
		textBox2.Text = ut.UserTypeName;
		if (SafeRegions != null)
		{
			this.SafeRegions = new List<SafeRegions>(SafeRegions);
		}
		else
		{
			this.SafeRegions = new List<SafeRegions>();
		}
		foreach (SafeRegions SafeRegion in SafeRegions)
		{
			checkedListBox1.Items.Add(SafeRegion);
		}
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			foreach (int region in ut.Regions)
			{
				if (((SafeRegions)checkedListBox1.Items[i]).Id == region)
				{
					checkedListBox1.SetItemChecked(i, value: true);
					break;
				}
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox2.Text == "")
		{
			MessageBox.Show("请输入名称");
			return;
		}
		obj[0] = textBox2.Text;
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

	private void UserTypeEdit_Load(object sender, EventArgs e)
	{
		base.AcceptButton = button1;
		base.CancelButton = button2;
	}
}
