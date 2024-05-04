using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIRunForm;

public class ProjectStartForm : Form
{
	private CAuthoritySeiallize cas = new CAuthoritySeiallize();

	public EventHandler EventCloseForm;

	public static bool bProjectStartClose;

	public string strCurrentUser;

	private IContainer components;

	private Button btSure;

	private Button btCancel;

	private Label label1;

	private Label label2;

	private PictureBox pictureBox1;

	private ComboBox cbUser;

	private TextBox tbPassword;

	public ProjectStartForm(CAuthoritySeiallize cas)
	{
		InitializeComponent();
		this.cas = cas;
	}

	private void ProjectStartForm_Load(object sender, EventArgs e)
	{
		foreach (string key in cas.dicAuthority.Keys)
		{
			cbUser.Items.Add(key);
		}
	}

	private void btCancel_Click(object sender, EventArgs e)
	{
		bProjectStartClose = true;
		EventCloseForm(null, null);
	}

	private void btSure_Click(object sender, EventArgs e)
	{
		if (cbUser.Text.Trim() == "")
		{
			MessageBox.Show("用户名输入为空！", "提示");
			return;
		}
		if (tbPassword.Text.Trim() == "")
		{
			MessageBox.Show("密码输入为空！", "提示");
			return;
		}
		if (tbPassword.Text.Trim() != cas.dicAuthority[cbUser.Text.Trim()].srtPassword)
		{
			MessageBox.Show("密码输入不正确!", "提示");
			return;
		}
		strCurrentUser = cbUser.Text.Trim();
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIRunForm.ProjectStartForm));
		this.btSure = new System.Windows.Forms.Button();
		this.btCancel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.cbUser = new System.Windows.Forms.ComboBox();
		this.tbPassword = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.btSure.Location = new System.Drawing.Point(67, 100);
		this.btSure.Name = "btSure";
		this.btSure.Size = new System.Drawing.Size(75, 23);
		this.btSure.TabIndex = 0;
		this.btSure.Text = "确定";
		this.btSure.UseVisualStyleBackColor = true;
		this.btSure.Click += new System.EventHandler(btSure_Click);
		this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btCancel.Location = new System.Drawing.Point(148, 100);
		this.btCancel.Name = "btCancel";
		this.btCancel.Size = new System.Drawing.Size(75, 23);
		this.btCancel.TabIndex = 1;
		this.btCancel.Text = "取消";
		this.btCancel.UseVisualStyleBackColor = true;
		this.btCancel.Click += new System.EventHandler(btCancel_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(97, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 2;
		this.label1.Text = "用户名";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(97, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 3;
		this.label2.Text = "口  令";
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(12, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(72, 72);
		this.pictureBox1.TabIndex = 4;
		this.pictureBox1.TabStop = false;
		this.cbUser.FormattingEnabled = true;
		this.cbUser.Location = new System.Drawing.Point(144, 21);
		this.cbUser.Name = "cbUser";
		this.cbUser.Size = new System.Drawing.Size(121, 20);
		this.cbUser.TabIndex = 5;
		this.tbPassword.Location = new System.Drawing.Point(144, 55);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.PasswordChar = '*';
		this.tbPassword.Size = new System.Drawing.Size(121, 21);
		this.tbPassword.TabIndex = 6;
		base.AcceptButton = this.btSure;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btCancel;
		base.ClientSize = new System.Drawing.Size(284, 135);
		base.Controls.Add(this.tbPassword);
		base.Controls.Add(this.cbUser);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btCancel);
		base.Controls.Add(this.btSure);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ProjectStartForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "登录";
		base.TopMost = true;
		base.Load += new System.EventHandler(ProjectStartForm_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
