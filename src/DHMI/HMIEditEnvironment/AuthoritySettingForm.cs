using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class AuthoritySettingForm : Form
{
	private Dictionary<string, CAuthorityInfo> dicAuthority = new Dictionary<string, CAuthorityInfo>();

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
		CAuthoritySeiallize cAuthoritySeiallize = new CAuthoritySeiallize();
		BinarySerialize<CAuthoritySeiallize> binarySerialize = new BinarySerialize<CAuthoritySeiallize>();
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
		CAuthorityInfo cAuthorityInfo = new CAuthorityInfo();
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
			CAuthoritySeiallize cAuthoritySeiallize = new CAuthoritySeiallize();
			cAuthoritySeiallize.dicAuthority = dicAuthority;
			cAuthoritySeiallize.bProjectStart = false;
			cAuthoritySeiallize.bProjectEnd = false;
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
			BinarySerialize<CAuthoritySeiallize> binarySerialize = new BinarySerialize<CAuthoritySeiallize>();
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.TreeNode treeNode = new System.Windows.Forms.TreeNode("操作工级");
		System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("班长级", 0, 0);
		System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("工程师级");
		System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("系统管理员级");
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.AuthoritySettingForm));
		this.btModify = new System.Windows.Forms.Button();
		this.btCancel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.treeView_User = new System.Windows.Forms.TreeView();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage_User = new System.Windows.Forms.TabPage();
		this.clbSafeArea = new System.Windows.Forms.CheckedListBox();
		this.label6 = new System.Windows.Forms.Label();
		this.btDel = new System.Windows.Forms.Button();
		this.btAdd = new System.Windows.Forms.Button();
		this.cbAuthorityLevel = new System.Windows.Forms.ComboBox();
		this.tbPasswordConfirm = new System.Windows.Forms.TextBox();
		this.tbPassword = new System.Windows.Forms.TextBox();
		this.tbUser = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.clbSystemAuthority = new System.Windows.Forms.CheckedListBox();
		this.label5 = new System.Windows.Forms.Label();
		this.btSure = new System.Windows.Forms.Button();
		this.tabControl1.SuspendLayout();
		this.tabPage_User.SuspendLayout();
		base.SuspendLayout();
		this.btModify.Location = new System.Drawing.Point(24, 183);
		this.btModify.Name = "btModify";
		this.btModify.Size = new System.Drawing.Size(70, 23);
		this.btModify.TabIndex = 0;
		this.btModify.Text = "修 改";
		this.btModify.UseVisualStyleBackColor = true;
		this.btModify.Click += new System.EventHandler(btModify_Click);
		this.btCancel.Location = new System.Drawing.Point(421, 264);
		this.btCancel.Name = "btCancel";
		this.btCancel.Size = new System.Drawing.Size(70, 23);
		this.btCancel.TabIndex = 1;
		this.btCancel.Text = "取 消";
		this.btCancel.UseVisualStyleBackColor = true;
		this.btCancel.Click += new System.EventHandler(btCancel_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 2;
		this.label1.Text = "用户列表";
		this.treeView_User.ImageIndex = 0;
		this.treeView_User.ImageList = this.imageList;
		this.treeView_User.Location = new System.Drawing.Point(12, 34);
		this.treeView_User.Name = "treeView_User";
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
		this.treeView_User.Nodes.AddRange(new System.Windows.Forms.TreeNode[4] { treeNode, treeNode2, treeNode3, treeNode4 });
		this.treeView_User.SelectedImageIndex = 0;
		this.treeView_User.Size = new System.Drawing.Size(158, 159);
		this.treeView_User.TabIndex = 3;
		this.treeView_User.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView_User_AfterSelect);
		this.treeView_User.MouseClick += new System.Windows.Forms.MouseEventHandler(treeView_User_MouseClick);
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.Transparent;
		this.tabControl1.Controls.Add(this.tabPage_User);
		this.tabControl1.Location = new System.Drawing.Point(176, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(339, 246);
		this.tabControl1.TabIndex = 4;
		this.tabPage_User.Controls.Add(this.clbSafeArea);
		this.tabPage_User.Controls.Add(this.label6);
		this.tabPage_User.Controls.Add(this.btDel);
		this.tabPage_User.Controls.Add(this.btAdd);
		this.tabPage_User.Controls.Add(this.cbAuthorityLevel);
		this.tabPage_User.Controls.Add(this.tbPasswordConfirm);
		this.tabPage_User.Controls.Add(this.tbPassword);
		this.tabPage_User.Controls.Add(this.tbUser);
		this.tabPage_User.Controls.Add(this.btModify);
		this.tabPage_User.Controls.Add(this.label7);
		this.tabPage_User.Controls.Add(this.label4);
		this.tabPage_User.Controls.Add(this.label3);
		this.tabPage_User.Controls.Add(this.label2);
		this.tabPage_User.Location = new System.Drawing.Point(4, 22);
		this.tabPage_User.Name = "tabPage_User";
		this.tabPage_User.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage_User.Size = new System.Drawing.Size(331, 220);
		this.tabPage_User.TabIndex = 0;
		this.tabPage_User.Text = "用户权限修改";
		this.tabPage_User.UseVisualStyleBackColor = true;
		this.clbSafeArea.CheckOnClick = true;
		this.clbSafeArea.FormattingEnabled = true;
		this.clbSafeArea.Items.AddRange(new object[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" });
		this.clbSafeArea.Location = new System.Drawing.Point(202, 37);
		this.clbSafeArea.Name = "clbSafeArea";
		this.clbSafeArea.Size = new System.Drawing.Size(109, 164);
		this.clbSafeArea.TabIndex = 11;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(207, 18);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 12);
		this.label6.TabIndex = 10;
		this.label6.Text = "安全区";
		this.btDel.Location = new System.Drawing.Point(100, 154);
		this.btDel.Name = "btDel";
		this.btDel.Size = new System.Drawing.Size(70, 23);
		this.btDel.TabIndex = 9;
		this.btDel.Text = "删 除";
		this.btDel.UseVisualStyleBackColor = true;
		this.btDel.Click += new System.EventHandler(btDel_Click);
		this.btAdd.Location = new System.Drawing.Point(24, 154);
		this.btAdd.Name = "btAdd";
		this.btAdd.Size = new System.Drawing.Size(70, 23);
		this.btAdd.TabIndex = 8;
		this.btAdd.Text = "添 加";
		this.btAdd.UseVisualStyleBackColor = true;
		this.btAdd.Click += new System.EventHandler(btAdd_Click);
		this.cbAuthorityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbAuthorityLevel.FormattingEnabled = true;
		this.cbAuthorityLevel.Items.AddRange(new object[4] { "操作工级", "班长级", "工程师级", "系统管理员级" });
		this.cbAuthorityLevel.Location = new System.Drawing.Point(77, 49);
		this.cbAuthorityLevel.Name = "cbAuthorityLevel";
		this.cbAuthorityLevel.Size = new System.Drawing.Size(99, 20);
		this.cbAuthorityLevel.TabIndex = 7;
		this.tbPasswordConfirm.Location = new System.Drawing.Point(76, 115);
		this.tbPasswordConfirm.Name = "tbPasswordConfirm";
		this.tbPasswordConfirm.PasswordChar = '*';
		this.tbPasswordConfirm.Size = new System.Drawing.Size(100, 21);
		this.tbPasswordConfirm.TabIndex = 6;
		this.tbPassword.Location = new System.Drawing.Point(76, 82);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.PasswordChar = '*';
		this.tbPassword.Size = new System.Drawing.Size(100, 21);
		this.tbPassword.TabIndex = 5;
		this.tbUser.Location = new System.Drawing.Point(76, 16);
		this.tbUser.Name = "tbUser";
		this.tbUser.Size = new System.Drawing.Size(100, 21);
		this.tbUser.TabIndex = 4;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(17, 119);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(53, 12);
		this.label7.TabIndex = 3;
		this.label7.Text = "核实口令";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 86);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(53, 12);
		this.label4.TabIndex = 2;
		this.label4.Text = "口    令";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(17, 53);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 1;
		this.label3.Text = "级    别";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "用 户 名";
		this.clbSystemAuthority.CheckOnClick = true;
		this.clbSystemAuthority.FormattingEnabled = true;
		this.clbSystemAuthority.Items.AddRange(new object[2] { "进入运行", "退出运行" });
		this.clbSystemAuthority.Location = new System.Drawing.Point(12, 218);
		this.clbSystemAuthority.Name = "clbSystemAuthority";
		this.clbSystemAuthority.Size = new System.Drawing.Size(158, 36);
		this.clbSystemAuthority.TabIndex = 13;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 201);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 12);
		this.label5.TabIndex = 12;
		this.label5.Text = "系统权限";
		this.btSure.Location = new System.Drawing.Point(345, 264);
		this.btSure.Name = "btSure";
		this.btSure.Size = new System.Drawing.Size(70, 23);
		this.btSure.TabIndex = 14;
		this.btSure.Text = "确 定";
		this.btSure.UseVisualStyleBackColor = true;
		this.btSure.Click += new System.EventHandler(btSure_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(524, 296);
		base.Controls.Add(this.btSure);
		base.Controls.Add(this.clbSystemAuthority);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.treeView_User);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btCancel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AuthoritySettingForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "用户管理";
		base.Load += new System.EventHandler(AuthoritySettingForm_Load);
		this.tabControl1.ResumeLayout(false);
		this.tabPage_User.ResumeLayout(false);
		this.tabPage_User.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
