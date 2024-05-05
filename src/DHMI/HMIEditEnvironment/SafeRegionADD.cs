using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HMIEditEnvironment;

public class SafeRegionADD : XtraForm
{
	private IContainer components = null;

	private Label label1;

	private TextBox textBox1;

	private Button button1;

	private Button button2;

	private HelpProvider helpProvider1;

	public string str = "";

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
		this.helpProvider1 = new System.Windows.Forms.HelpProvider();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(20, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 14);
		this.label1.TabIndex = 3;
		this.label1.Text = "安全区名称";
		this.textBox1.Location = new System.Drawing.Point(100, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(116, 22);
		this.textBox1.TabIndex = 0;
		this.button1.Location = new System.Drawing.Point(27, 70);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 27);
		this.button1.TabIndex = 1;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(121, 70);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(87, 27);
		this.button2.TabIndex = 2;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.helpProvider1.HelpNamespace = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(237, 122);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "10.10.1.2添加安全区.htm");
		this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.MaximizeBox = false;
		base.Name = "SafeRegionADD";
		this.helpProvider1.SetShowHelp(this, true);
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "添加安全区";
		base.Load += new System.EventHandler(UserTypeADD_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public SafeRegionADD()
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
	}

	private void UserTypeADD_Load(object sender, EventArgs e)
	{
		base.AcceptButton = button1;
		base.CancelButton = button2;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "")
		{
			MessageBox.Show("请输入名称");
			return;
		}
		str = textBox1.Text;
		base.DialogResult = DialogResult.OK;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
