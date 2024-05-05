using ShapeRuntime;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HMIRunForm;

public class ProjectStartForm : Form
{
	private readonly CAuthoritySeiallize cas = new();

	public EventHandler EventCloseForm;

	public static bool bProjectStartClose;

	public string strCurrentUser;

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

	private void BtCancel_Click(object sender, EventArgs e)
	{
		bProjectStartClose = true;
		EventCloseForm(null, null);
	}

	private void BtSure_Click(object sender, EventArgs e)
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

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIRunForm.ProjectStartForm));
		btSure = new System.Windows.Forms.Button();
		btCancel = new System.Windows.Forms.Button();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		pictureBox1 = new System.Windows.Forms.PictureBox();
		cbUser = new System.Windows.Forms.ComboBox();
		tbPassword = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		base.SuspendLayout();
		btSure.Location = new System.Drawing.Point(67, 100);
		btSure.Name = "btSure";
		btSure.Size = new System.Drawing.Size(75, 23);
		btSure.TabIndex = 0;
		btSure.Text = "确定";
		btSure.UseVisualStyleBackColor = true;
		btSure.Click += new System.EventHandler(BtSure_Click);
		btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		btCancel.Location = new System.Drawing.Point(148, 100);
		btCancel.Name = "btCancel";
		btCancel.Size = new System.Drawing.Size(75, 23);
		btCancel.TabIndex = 1;
		btCancel.Text = "取消";
		btCancel.UseVisualStyleBackColor = true;
		btCancel.Click += new System.EventHandler(BtCancel_Click);
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(97, 25);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(41, 12);
		label1.TabIndex = 2;
		label1.Text = "用户名";
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(97, 59);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(41, 12);
		label2.TabIndex = 3;
		label2.Text = "口  令";
		pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new System.Drawing.Point(12, 12);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new System.Drawing.Size(72, 72);
		pictureBox1.TabIndex = 4;
		pictureBox1.TabStop = false;
		cbUser.FormattingEnabled = true;
		cbUser.Location = new System.Drawing.Point(144, 21);
		cbUser.Name = "cbUser";
		cbUser.Size = new System.Drawing.Size(121, 20);
		cbUser.TabIndex = 5;
		tbPassword.Location = new System.Drawing.Point(144, 55);
		tbPassword.Name = "tbPassword";
		tbPassword.PasswordChar = '*';
		tbPassword.Size = new System.Drawing.Size(121, 21);
		tbPassword.TabIndex = 6;
		base.AcceptButton = btSure;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = btCancel;
		base.ClientSize = new System.Drawing.Size(284, 135);
		base.Controls.Add(tbPassword);
		base.Controls.Add(cbUser);
		base.Controls.Add(pictureBox1);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Controls.Add(btCancel);
		base.Controls.Add(btSure);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ProjectStartForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "登录";
		base.TopMost = true;
		base.Load += new System.EventHandler(ProjectStartForm_Load);
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
