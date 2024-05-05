using ShapeRuntime;
using System;
using System.Windows.Forms;

namespace HMIRunForm;

public class ProjectEndForm : Form
{
	private readonly CAuthoritySeiallize cas = new();

	public static bool bProjectEndForm;

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

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new(typeof(HMIRunForm.ProjectEndForm));
		tbPassword = new System.Windows.Forms.TextBox();
		cbUser = new System.Windows.Forms.ComboBox();
		label2 = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		btCancel = new System.Windows.Forms.Button();
		btSure = new System.Windows.Forms.Button();
		pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		base.SuspendLayout();
		tbPassword.Location = new System.Drawing.Point(146, 55);
		tbPassword.Name = "tbPassword";
		tbPassword.PasswordChar = '*';
		tbPassword.Size = new System.Drawing.Size(121, 21);
		tbPassword.TabIndex = 13;
		cbUser.FormattingEnabled = true;
		cbUser.Location = new System.Drawing.Point(146, 21);
		cbUser.Name = "cbUser";
		cbUser.Size = new System.Drawing.Size(121, 20);
		cbUser.TabIndex = 12;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(99, 59);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(41, 12);
		label2.TabIndex = 10;
		label2.Text = "口  令";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(99, 25);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(41, 12);
		label1.TabIndex = 9;
		label1.Text = "用户名";
		btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		btCancel.Location = new System.Drawing.Point(148, 100);
		btCancel.Name = "btCancel";
		btCancel.Size = new System.Drawing.Size(75, 23);
		btCancel.TabIndex = 8;
		btCancel.Text = "取消";
		btCancel.UseVisualStyleBackColor = true;
		btSure.Location = new System.Drawing.Point(67, 100);
		btSure.Name = "btSure";
		btSure.Size = new System.Drawing.Size(75, 23);
		btSure.TabIndex = 7;
		btSure.Text = "确定";
		btSure.UseVisualStyleBackColor = true;
		btSure.Click += new System.EventHandler(BtSure_Click);
		pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new System.Drawing.Point(14, 12);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new System.Drawing.Size(72, 72);
		pictureBox1.TabIndex = 11;
		pictureBox1.TabStop = false;
		base.AcceptButton = btCancel;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
		base.Name = "ProjectEndForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "退出";
		base.Load += new System.EventHandler(ProjectEndForm_Load);
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
