using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HMIEditEnvironment;

public class SafeRegionEdit : XtraForm
{
	public string str;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox textBox1;

	private TextBox textBox2;

	private Button button1;

	private Button button2;

	private HelpProvider helpProvider1;

	public SafeRegionEdit()
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
	}

	public SafeRegionEdit(string str)
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		textBox1.Text = str;
		textBox2.Text = str;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox2.Text == "")
		{
			MessageBox.Show("请输入名称");
			return;
		}
		str = textBox2.Text;
		base.DialogResult = DialogResult.OK;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void SafeRegionEdit_Load(object sender, EventArgs e)
	{
		base.AcceptButton = button1;
		base.CancelButton = button2;
	}

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
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(33, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 14);
		this.label1.TabIndex = 4;
		this.label1.Text = "旧名称";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(33, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(43, 14);
		this.label2.TabIndex = 1;
		this.label2.Text = "新名称";
		this.textBox1.Location = new System.Drawing.Point(87, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(116, 22);
		this.textBox1.TabIndex = 3;
		this.textBox2.Location = new System.Drawing.Point(87, 56);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(116, 22);
		this.textBox2.TabIndex = 0;
		this.button1.Location = new System.Drawing.Point(35, 93);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 27);
		this.button1.TabIndex = 1;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(129, 93);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(87, 27);
		this.button2.TabIndex = 2;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.helpProvider1.HelpNamespace = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(250, 146);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "10.10.1.3修改安全区.htm");
		this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.MaximizeBox = false;
		base.Name = "SafeRegionEdit";
		this.helpProvider1.SetShowHelp(this, true);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "编辑安全区";
		base.Load += new System.EventHandler(SafeRegionEdit_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
