using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class AuthoritySettingForm : Form
{
	private Dictionary<string, CAuthorityInfo> dicAuthority = new();

	private IContainer components;

	private Button btModify;

	private Button btCancel;

	private Label label1;

	private TreeView treeView_User;

	private TabControl tabControl1;

	private TabPage tabPage_User;

	private Label label2;

	private Label label4;

	private Label label3;

	private TextBox tbPasswordConfirm;

	private TextBox tbPassword;

	private TextBox tbUser;

	private ComboBox cbAuthorityLevel;

	private Button btDel;

	private Button btAdd;

	private Label label6;

	private CheckedListBox clbSafeArea;

	private Label label7;

	private CheckedListBox clbSystemAuthority;

	private Label label5;

	private ImageList imageList;

	private Button btSure;

	private void AuthoritySettingForm_Load(object sender, EventArgs e)
	{
		cbAuthorityLevel.SelectedIndex = 0;
		CAuthoritySeiallize cAuthoritySeiallize = new();
		BinarySerialize<CAuthoritySeiallize> binarySerialize = new();
		cAuthoritySeiallize = binarySerialize.DeSerialize(CEditEnvironmentGlobal.path + "\\Authority.data");
		if (cAuthoritySeiallize == null)
		{
			return;
		}
		dicAuthority = cAuthoritySeiallize.dicAuthority;
		if (dicAuthority.Count == 0)
		{
			return;
		}
		foreach (TreeNode node in treeView_User.Nodes)
		{
			foreach (string key in dicAuthority.Keys)
			{
				if (node.Text == dicAuthority[key].strAuthority)
				{
					node.Nodes.Add(new TreeNode(key, 1, 1));
				}
			}
			node.ExpandAll();
		}
		if (cAuthoritySeiallize.bProjectStart)
		{
			clbSystemAuthority.SetItemChecked(0, value: true);
		}
		if (cAuthoritySeiallize.bProjectEnd)
		{
			clbSystemAuthority.SetItemChecked(1, value: true);
		}
	}

	private void btCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void treeView_User_MouseClick(object sender, MouseEventArgs e)
	{
	}

	public AuthoritySettingForm()
	{
		InitializeComponent();
	}

	private void btAdd_Click(object sender, EventArgs e)
	{
		if (tbUser.Text.Trim() == "")
		{
			MessageBox.Show("用户名不能为空!", "提示");
			return;
		}
		if (tbPassword.Text.Trim() == "")
		{
			MessageBox.Show("口令不能为空！", "提示");
			return;
		}
		if (tbPasswordConfirm.Text.Trim() != tbPassword.Text.Trim())
		{
			MessageBox.Show("核实口令输入不正确,请重新输入！", "提示");
			return;
		}
		if (dicAuthority.ContainsKey(tbUser.Text.Trim()))
		{
			MessageBox.Show("用户已经存在！", "提示");
			return;
		}
		foreach (TreeNode node in treeView_User.Nodes)
		{
			if (node.Text == cbAuthorityLevel.Text)
			{
				node.Nodes.Add(new TreeNode(tbUser.Text.Trim(), 1, 1));
			}
			else if (node.Text == cbAuthorityLevel.Text)
			{
				node.Nodes.Add(new TreeNode(tbUser.Text.Trim(), 1, 1));
			}
			else if (node.Text == cbAuthorityLevel.Text)
			{
				node.Nodes.Add(new TreeNode(tbUser.Text.Trim(), 1, 1));
			}
			else if (node.Text == cbAuthorityLevel.Text)
			{
				node.Nodes.Add(new TreeNode(tbUser.Text.Trim(), 1, 1));
			}
			node.ExpandAll();
		}
		CAuthorityInfo cAuthorityInfo = new();
		foreach (string checkedItem in clbSafeArea.CheckedItems)
		{
			cAuthorityInfo.ltSafeRegion.Add(checkedItem);
		}
		cAuthorityInfo.strName = tbUser.Name.Trim();
		cAuthorityInfo.srtPassword = tbPasswordConfirm.Text.Trim();
		cAuthorityInfo.strAuthority = cbAuthorityLevel.SelectedItem.ToString();
		dicAuthority.Add(tbUser.Text.Trim(), cAuthorityInfo);
	}

	private void btDel_Click(object sender, EventArgs e)
	{
		if (tbUser.Text.Trim() == "" || cbAuthorityLevel.Text == "" || tbPassword.Text == "" || tbPasswordConfirm.Text == "")
		{
			return;
		}
		dicAuthority.Remove(tbUser.Text);
		foreach (TreeNode node in treeView_User.Nodes)
		{
			foreach (TreeNode node2 in node.Nodes)
			{
				if (node2.Text == tbUser.Text.Trim())
				{
					node2.Nodes.Remove(node2);
				}
			}
		}
		treeView_User.Refresh();
	}

	private void btModify_Click(object sender, EventArgs e)
	{
		if (tbUser.Text.Trim() == "" || cbAuthorityLevel.Text == "" || tbPassword.Text == "" || tbPasswordConfirm.Text == "")
		{
			return;
		}
		if (tbPassword.Text != tbPasswordConfirm.Text)
		{
			MessageBox.Show("核实口令不正确！", "提示");
			return;
		}
		string key = tbUser.Text.Trim();
		if (!dicAuthority.ContainsKey(key))
		{
			return;
		}
		dicAuthority[key].ltSafeRegion.Clear();
		foreach (string checkedItem in clbSafeArea.CheckedItems)
		{
			dicAuthority[key].ltSafeRegion.Add(checkedItem);
		}
		dicAuthority[key].srtPassword = tbPasswordConfirm.Text.Trim();
		dicAuthority[key].strAuthority = cbAuthorityLevel.SelectedItem.ToString();
	}

	private void treeView_User_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (e.Node.Text == "操作工级")
		{
			cbAuthorityLevel.Text = "操作工级";
			tbUser.Text = "";
			tbPassword.Text = "";
			tbPasswordConfirm.Text = "";
			for (int i = 0; i < clbSafeArea.Items.Count; i++)
			{
				clbSafeArea.SetItemChecked(i, value: false);
			}
			return;
		}
		if (e.Node.Text == "班长级")
		{
			cbAuthorityLevel.Text = "班长级";
			tbUser.Text = "";
			tbPassword.Text = "";
			tbPasswordConfirm.Text = "";
			for (int j = 0; j < clbSafeArea.Items.Count; j++)
			{
				clbSafeArea.SetItemChecked(j, value: false);
			}
			return;
		}
		if (e.Node.Text == "工程师级")
		{
			cbAuthorityLevel.Text = "工程师级";
			tbUser.Text = "";
			tbPassword.Text = "";
			tbPasswordConfirm.Text = "";
			for (int k = 0; k < clbSafeArea.Items.Count; k++)
			{
				clbSafeArea.SetItemChecked(k, value: false);
			}
			return;
		}
		if (e.Node.Text == "系统管理员级")
		{
			cbAuthorityLevel.Text = "系统管理员级";
			tbUser.Text = "";
			tbPassword.Text = "";
			tbPasswordConfirm.Text = "";
			for (int l = 0; l < clbSafeArea.Items.Count; l++)
			{
				clbSafeArea.SetItemChecked(l, value: false);
			}
			return;
		}
		string key = e.Node.Text;
		tbUser.Text = key;
		cbAuthorityLevel.Text = dicAuthority[key].strAuthority;
		tbPassword.Text = dicAuthority[key].srtPassword;
		tbPasswordConfirm.Text = dicAuthority[key].srtPassword;
		for (int m = 0; m < clbSafeArea.Items.Count; m++)
		{
			clbSafeArea.SetItemChecked(m, value: false);
		}
		using List<string>.Enumerator enumerator = dicAuthority[key].ltSafeRegion.GetEnumerator();
		while (enumerator.MoveNext())
		{
			switch (enumerator.Current)
			{
			case "A":
				clbSafeArea.SetItemChecked(0, value: true);
				break;
			case "B":
				clbSafeArea.SetItemChecked(1, value: true);
				break;
			case "C":
				clbSafeArea.SetItemChecked(2, value: true);
				break;
			case "D":
				clbSafeArea.SetItemChecked(3, value: true);
				break;
			case "E":
				clbSafeArea.SetItemChecked(4, value: true);
				break;
			case "F":
				clbSafeArea.SetItemChecked(5, value: true);
				break;
			case "G":
				clbSafeArea.SetItemChecked(6, value: true);
				break;
			case "H":
				clbSafeArea.SetItemChecked(7, value: true);
				break;
			case "I":
				clbSafeArea.SetItemChecked(8, value: true);
				break;
			case "J":
				clbSafeArea.SetItemChecked(9, value: true);
				break;
			}
		}
	}

	private void btSure_Click(object sender, EventArgs e)
	{
		try
		{
            CAuthoritySeiallize cAuthoritySeiallize = new()
            {
                dicAuthority = dicAuthority,
                bProjectStart = false,
                bProjectEnd = false
            };
            foreach (string checkedItem in clbSystemAuthority.CheckedItems)
			{
				if (checkedItem == "进入运行")
				{
					cAuthoritySeiallize.bProjectStart = true;
				}
				else if (checkedItem == "退出运行")
				{
					cAuthoritySeiallize.bProjectEnd = true;
				}
			}
			BinarySerialize<CAuthoritySeiallize> binarySerialize = new();
			binarySerialize.Serialize(cAuthoritySeiallize, CEditEnvironmentGlobal.path + "\\Authority.data");
			Close();
		}
		catch
		{
			MessageBox.Show("用户权限设置确定按钮出现异常！", "提示");
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
		components = new System.ComponentModel.Container();
		System.Windows.Forms.TreeNode treeNode = new("操作工级");
		System.Windows.Forms.TreeNode treeNode2 = new("班长级", 0, 0);
		System.Windows.Forms.TreeNode treeNode3 = new("工程师级");
		System.Windows.Forms.TreeNode treeNode4 = new("系统管理员级");
		System.ComponentModel.ComponentResourceManager resources = new(typeof(HMIEditEnvironment.AuthoritySettingForm));
		btModify = new System.Windows.Forms.Button();
		btCancel = new System.Windows.Forms.Button();
		label1 = new System.Windows.Forms.Label();
		treeView_User = new System.Windows.Forms.TreeView();
		imageList = new System.Windows.Forms.ImageList(components);
		tabControl1 = new System.Windows.Forms.TabControl();
		tabPage_User = new System.Windows.Forms.TabPage();
		clbSafeArea = new System.Windows.Forms.CheckedListBox();
		label6 = new System.Windows.Forms.Label();
		btDel = new System.Windows.Forms.Button();
		btAdd = new System.Windows.Forms.Button();
		cbAuthorityLevel = new System.Windows.Forms.ComboBox();
		tbPasswordConfirm = new System.Windows.Forms.TextBox();
		tbPassword = new System.Windows.Forms.TextBox();
		tbUser = new System.Windows.Forms.TextBox();
		label7 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		clbSystemAuthority = new System.Windows.Forms.CheckedListBox();
		label5 = new System.Windows.Forms.Label();
		btSure = new System.Windows.Forms.Button();
		tabControl1.SuspendLayout();
		tabPage_User.SuspendLayout();
		base.SuspendLayout();
		btModify.Location = new System.Drawing.Point(24, 183);
		btModify.Name = "btModify";
		btModify.Size = new System.Drawing.Size(70, 23);
		btModify.TabIndex = 0;
		btModify.Text = "修 改";
		btModify.UseVisualStyleBackColor = true;
		btModify.Click += new System.EventHandler(btModify_Click);
		btCancel.Location = new System.Drawing.Point(421, 264);
		btCancel.Name = "btCancel";
		btCancel.Size = new System.Drawing.Size(70, 23);
		btCancel.TabIndex = 1;
		btCancel.Text = "取 消";
		btCancel.UseVisualStyleBackColor = true;
		btCancel.Click += new System.EventHandler(btCancel_Click);
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(12, 12);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(53, 12);
		label1.TabIndex = 2;
		label1.Text = "用户列表";
		treeView_User.ImageIndex = 0;
		treeView_User.ImageList = imageList;
		treeView_User.Location = new System.Drawing.Point(12, 34);
		treeView_User.Name = "treeView_User";
		treeNode.ImageIndex = 0;
		treeNode.Name = "OperateLevel";
		treeNode.SelectedImageKey = "folder.ico";
		treeNode.Text = "操作工级";
		treeNode2.ImageIndex = 0;
		treeNode2.Name = "MonitorLevel";
		treeNode2.SelectedImageIndex = 0;
		treeNode2.Text = "班长级";
		treeNode3.ImageIndex = 0;
		treeNode3.Name = "EngineerLevel";
		treeNode3.Text = "工程师级";
		treeNode4.ImageIndex = 0;
		treeNode4.Name = "SystemManageLevel";
		treeNode4.Text = "系统管理员级";
		treeView_User.Nodes.AddRange(new System.Windows.Forms.TreeNode[4] { treeNode, treeNode2, treeNode3, treeNode4 });
		treeView_User.SelectedImageIndex = 0;
		treeView_User.Size = new System.Drawing.Size(158, 159);
		treeView_User.TabIndex = 3;
		treeView_User.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView_User_AfterSelect);
		treeView_User.MouseClick += new System.Windows.Forms.MouseEventHandler(treeView_User_MouseClick);
		imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		imageList.TransparentColor = System.Drawing.Color.Transparent;
		tabControl1.Controls.Add(tabPage_User);
		tabControl1.Location = new System.Drawing.Point(176, 12);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new System.Drawing.Size(339, 246);
		tabControl1.TabIndex = 4;
		tabPage_User.Controls.Add(clbSafeArea);
		tabPage_User.Controls.Add(label6);
		tabPage_User.Controls.Add(btDel);
		tabPage_User.Controls.Add(btAdd);
		tabPage_User.Controls.Add(cbAuthorityLevel);
		tabPage_User.Controls.Add(tbPasswordConfirm);
		tabPage_User.Controls.Add(tbPassword);
		tabPage_User.Controls.Add(tbUser);
		tabPage_User.Controls.Add(btModify);
		tabPage_User.Controls.Add(label7);
		tabPage_User.Controls.Add(label4);
		tabPage_User.Controls.Add(label3);
		tabPage_User.Controls.Add(label2);
		tabPage_User.Location = new System.Drawing.Point(4, 22);
		tabPage_User.Name = "tabPage_User";
		tabPage_User.Padding = new System.Windows.Forms.Padding(3);
		tabPage_User.Size = new System.Drawing.Size(331, 220);
		tabPage_User.TabIndex = 0;
		tabPage_User.Text = "用户权限修改";
		tabPage_User.UseVisualStyleBackColor = true;
		clbSafeArea.CheckOnClick = true;
		clbSafeArea.FormattingEnabled = true;
		clbSafeArea.Items.AddRange(new object[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" });
		clbSafeArea.Location = new System.Drawing.Point(202, 37);
		clbSafeArea.Name = "clbSafeArea";
		clbSafeArea.Size = new System.Drawing.Size(109, 164);
		clbSafeArea.TabIndex = 11;
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(207, 18);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(41, 12);
		label6.TabIndex = 10;
		label6.Text = "安全区";
		btDel.Location = new System.Drawing.Point(100, 154);
		btDel.Name = "btDel";
		btDel.Size = new System.Drawing.Size(70, 23);
		btDel.TabIndex = 9;
		btDel.Text = "删 除";
		btDel.UseVisualStyleBackColor = true;
		btDel.Click += new System.EventHandler(btDel_Click);
		btAdd.Location = new System.Drawing.Point(24, 154);
		btAdd.Name = "btAdd";
		btAdd.Size = new System.Drawing.Size(70, 23);
		btAdd.TabIndex = 8;
		btAdd.Text = "添 加";
		btAdd.UseVisualStyleBackColor = true;
		btAdd.Click += new System.EventHandler(btAdd_Click);
		cbAuthorityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		cbAuthorityLevel.FormattingEnabled = true;
		cbAuthorityLevel.Items.AddRange(new object[4] { "操作工级", "班长级", "工程师级", "系统管理员级" });
		cbAuthorityLevel.Location = new System.Drawing.Point(77, 49);
		cbAuthorityLevel.Name = "cbAuthorityLevel";
		cbAuthorityLevel.Size = new System.Drawing.Size(99, 20);
		cbAuthorityLevel.TabIndex = 7;
		tbPasswordConfirm.Location = new System.Drawing.Point(76, 115);
		tbPasswordConfirm.Name = "tbPasswordConfirm";
		tbPasswordConfirm.PasswordChar = '*';
		tbPasswordConfirm.Size = new System.Drawing.Size(100, 21);
		tbPasswordConfirm.TabIndex = 6;
		tbPassword.Location = new System.Drawing.Point(76, 82);
		tbPassword.Name = "tbPassword";
		tbPassword.PasswordChar = '*';
		tbPassword.Size = new System.Drawing.Size(100, 21);
		tbPassword.TabIndex = 5;
		tbUser.Location = new System.Drawing.Point(76, 16);
		tbUser.Name = "tbUser";
		tbUser.Size = new System.Drawing.Size(100, 21);
		tbUser.TabIndex = 4;
		label7.AutoSize = true;
		label7.Location = new System.Drawing.Point(17, 119);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(53, 12);
		label7.TabIndex = 3;
		label7.Text = "核实口令";
		label4.AutoSize = true;
		label4.Location = new System.Drawing.Point(17, 86);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(53, 12);
		label4.TabIndex = 2;
		label4.Text = "口    令";
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(17, 53);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(53, 12);
		label3.TabIndex = 1;
		label3.Text = "级    别";
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(17, 20);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(53, 12);
		label2.TabIndex = 0;
		label2.Text = "用 户 名";
		clbSystemAuthority.CheckOnClick = true;
		clbSystemAuthority.FormattingEnabled = true;
		clbSystemAuthority.Items.AddRange(new object[2] { "进入运行", "退出运行" });
		clbSystemAuthority.Location = new System.Drawing.Point(12, 218);
		clbSystemAuthority.Name = "clbSystemAuthority";
		clbSystemAuthority.Size = new System.Drawing.Size(158, 36);
		clbSystemAuthority.TabIndex = 13;
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(12, 201);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(53, 12);
		label5.TabIndex = 12;
		label5.Text = "系统权限";
		btSure.Location = new System.Drawing.Point(345, 264);
		btSure.Name = "btSure";
		btSure.Size = new System.Drawing.Size(70, 23);
		btSure.TabIndex = 14;
		btSure.Text = "确 定";
		btSure.UseVisualStyleBackColor = true;
		btSure.Click += new System.EventHandler(btSure_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(524, 296);
		base.Controls.Add(btSure);
		base.Controls.Add(clbSystemAuthority);
		base.Controls.Add(tabControl1);
		base.Controls.Add(label5);
		base.Controls.Add(treeView_User);
		base.Controls.Add(label1);
		base.Controls.Add(btCancel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AuthoritySettingForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "用户管理";
		base.Load += new System.EventHandler(AuthoritySettingForm_Load);
		tabControl1.ResumeLayout(false);
		tabPage_User.ResumeLayout(false);
		tabPage_User.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
