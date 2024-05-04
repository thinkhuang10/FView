using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIRunForm;

public class ProjectEndForm : Form
{
	private CAuthoritySeiallize cas = new CAuthoritySeiallize();

	public static bool bProjectEndForm;

	private IContainer components;

	private TextBox tbPassword;

	private ComboBox cbUser;

	private PictureBox pictureBox1;

	private Label label2;

	private Label label1;

	private Button btCancel;

	private Button btSure;

	public ProjectEndForm(CAuthoritySeiallize cas)
	{
		InitializeComponent();
		this.cas = cas;
	}

	private void btCancel_Click(object sender, EventArgs e)
	{
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
		bProjectEndForm = true;
		Close();
	}

	private void ProjectEndForm_Load(object sender, EventArgs e)
	{
		foreach (string key in cas.dicAuthority.Keys)
		{
			cbUser.Items.Add(key);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIRunForm.ProjectEndForm));
		this.tbPassword = new System.Windows.Forms.TextBox();
		this.cbUser = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btCancel = new System.Windows.Forms.Button();
		this.btSure = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.tbPassword.Location = new System.Drawing.Point(146, 55);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.PasswordChar = '*';
		this.tbPassword.Size = new System.Drawing.Size(121, 21);
		this.tbPassword.TabIndex = 13;
		this.cbUser.FormattingEnabled = true;
		this.cbUser.Location = new System.Drawing.Point(146, 21);
		this.cbUser.Name = "cbUser";
		this.cbUser.Size = new System.Drawing.Size(121, 20);
		this.cbUser.TabIndex = 12;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(99, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 10;
		this.label2.Text = "口  令";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(99, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 9;
		this.label1.Text = "用户名";
		this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btCancel.Location = new System.Drawing.Point(148, 100);
		this.btCancel.Name = "btCancel";
		this.btCancel.Size = new System.Drawing.Size(75, 23);
		this.btCancel.TabIndex = 8;
		this.btCancel.Text = "取消";
		this.btCancel.UseVisualStyleBackColor = true;
		this.btCancel.Click += new System.EventHandler(btCancel_Click);
		this.btSure.Location = new System.Drawing.Point(67, 100);
		this.btSure.Name = "btSure";
		this.btSure.Size = new System.Drawing.Size(75, 23);
		this.btSure.TabIndex = 7;
		this.btSure.Text = "确定";
		this.btSure.UseVisualStyleBackColor = true;
		this.btSure.Click += new System.EventHandler(btSure_Click);
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(14, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(72, 72);
		this.pictureBox1.TabIndex = 11;
		this.pictureBox1.TabStop = false;
		base.AcceptButton = this.btCancel;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
		base.Name = "ProjectEndForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "退出";
		base.Load += new System.EventHandler(ProjectEndForm_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
