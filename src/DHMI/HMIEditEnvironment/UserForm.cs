using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HMIEditEnvironment.Properties;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class UserForm : XtraForm
{
	private List<HMIUser> HMIUsers;

	private List<UserType> UserTypes;

	private List<SafeRegions> SafeRegions;

	private HMIUser theuser;

	private TreeNode thenode;

	private bool leftType = false;

	private bool rightType = false;

	private IContainer components = null;

	private TreeView treeView1;

	private Label label1;

	private GroupBox groupBox1;

	private CheckedListBox checkedListBox1;

	private Label label7;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox textBox3;

	private TextBox textBox2;

	private TextBox textBox1;

	private CheckBox checkBox1;

	private Button button1;

	private System.Windows.Forms.ComboBox comboBox1;

	private Button button3;

	private Button button2;

	private Button button4;

	private Button button5;

	private Button button6;

	private Button button7;

	private PictureBox pictureBox1;

	private Panel panel1;

	private Button button8;

	private Button button9;

	private Button button10;

	private PictureBox pictureBox2;

	private TreeView treeView2;

	private HelpProvider helpProvider1;

	public UserForm()
	{
		InitializeComponent();
		helpProvider1.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void UserForm_Load(object sender, EventArgs e)
	{
		if (CEditEnvironmentGlobal.dhp.HMIUsers != null)
		{
			HMIUsers = new List<HMIUser>(CEditEnvironmentGlobal.dhp.HMIUsers);
		}
		else
		{
			HMIUsers = new List<HMIUser>();
		}
		if (CEditEnvironmentGlobal.dhp.UserTypes != null)
		{
			UserTypes = new List<UserType>(CEditEnvironmentGlobal.dhp.UserTypes);
		}
		else
		{
			UserTypes = new List<UserType>();
		}
		if (CEditEnvironmentGlobal.dhp.SafeRegions != null)
		{
			SafeRegions = new List<SafeRegions>(CEditEnvironmentGlobal.dhp.SafeRegions);
		}
		else
		{
			SafeRegions = new List<SafeRegions>();
		}
		if (SafeRegions.Count == 0)
		{
			SafeRegions safeRegions = new SafeRegions();
			safeRegions.Id = 0;
			safeRegions.RegionName = "A";
			SafeRegions.Add(safeRegions);
			safeRegions = new SafeRegions();
			safeRegions.Id = 1;
			safeRegions.RegionName = "B";
			SafeRegions.Add(safeRegions);
			safeRegions = new SafeRegions();
			safeRegions.Id = 2;
			safeRegions.RegionName = "C";
			SafeRegions.Add(safeRegions);
			safeRegions = new SafeRegions();
			safeRegions.Id = 3;
			safeRegions.RegionName = "D";
			SafeRegions.Add(safeRegions);
			safeRegions = new SafeRegions();
			safeRegions.Id = 4;
			safeRegions.RegionName = "E";
			SafeRegions.Add(safeRegions);
		}
		if (UserTypes.Count == 0)
		{
			UserType userType = new UserType();
			userType.Id = 0;
			userType.UserTypeName = "系统管理员级";
			userType.Regions.Add(0);
			userType.Regions.Add(1);
			userType.Regions.Add(2);
			userType.Regions.Add(3);
			userType.Regions.Add(4);
			UserTypes.Add(userType);
			userType = new UserType();
			userType.Id = 1;
			userType.Regions.Add(1);
			userType.Regions.Add(2);
			userType.Regions.Add(3);
			userType.Regions.Add(4);
			userType.UserTypeName = "工程师级";
			UserTypes.Add(userType);
			userType = new UserType();
			userType.Id = 2;
			userType.Regions.Add(2);
			userType.Regions.Add(3);
			userType.Regions.Add(4);
			userType.UserTypeName = "班长级级";
			UserTypes.Add(userType);
			userType = new UserType();
			userType.Id = 3;
			userType.Regions.Add(3);
			userType.Regions.Add(4);
			userType.UserTypeName = "操作员级";
			UserTypes.Add(userType);
		}
		ClassForUsers.MakeUserTypeTree(treeView1, comboBox1, UserTypes, HMIUsers);
		foreach (SafeRegions safeRegion in SafeRegions)
		{
			checkedListBox1.Items.Add(safeRegion);
		}
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		theuser = null;
		thenode = null;
		if (e.Node.Name == "Class")
		{
			thenode = e.Node;
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
			try
			{
				ClassForUsers.MakeUserTypeTree(new TreeView(), comboBox1, UserTypes, HMIUsers);
				foreach (object item in comboBox1.Items)
				{
					if (item.ToString() == e.Node.Text)
					{
						comboBox1.SelectedItem = item;
					}
				}
				return;
			}
			catch (Exception)
			{
				return;
			}
		}
		HMIUser hMIUser = new HMIUser();
		foreach (HMIUser hMIUser2 in HMIUsers)
		{
			if (hMIUser2.name == e.Node.Text)
			{
				hMIUser = hMIUser2;
				thenode = e.Node;
			}
		}
		if (hMIUser.name == "")
		{
			return;
		}
		textBox1.Text = hMIUser.name;
		textBox2.Text = "********";
		textBox3.Text = "********";
		foreach (object item2 in comboBox1.Items)
		{
			if (item2.ToString() == e.Node.Parent.Text)
			{
				comboBox1.SelectedItem = item2;
			}
		}
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			checkedListBox1.SetItemChecked(i, value: false);
		}
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			foreach (int region in hMIUser.Regions)
			{
				if (((SafeRegions)checkedListBox1.Items[i]).Id == region)
				{
					checkedListBox1.SetItemChecked(i, value: true);
					break;
				}
			}
		}
		theuser = hMIUser;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			HMIUser hMIUser = new HMIUser();
			if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex < 0)
			{
				MessageBox.Show("请填写完整信息");
				return;
			}
			foreach (HMIUser hMIUser2 in HMIUsers)
			{
				if (hMIUser2.name == textBox1.Text)
				{
					MessageBox.Show("该用户名已经被使用");
					return;
				}
			}
			hMIUser.name = textBox1.Text;
			if (textBox3.Text != textBox2.Text)
			{
				MessageBox.Show("两次输入的口令不同");
				return;
			}
			hMIUser.password = ClassForUsers.getMd5Hash(textBox2.Text);
			hMIUser.type = ((UserType)comboBox1.SelectedItem).Id;
			hMIUser.time = -1;
			foreach (object checkedItem in checkedListBox1.CheckedItems)
			{
				hMIUser.Regions.Add(((SafeRegions)checkedItem).Id);
			}
			HMIUsers.Add(hMIUser);
			ClassForUsers.MakeUserTypeTree(treeView1, new System.Windows.Forms.ComboBox(), UserTypes, HMIUsers);
			theuser = null;
			thenode = null;
		}
		catch (Exception)
		{
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		try
		{
			if (thenode == null || theuser == null)
			{
				return;
			}
			if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex < 0)
			{
				MessageBox.Show("请填写完整信息");
				return;
			}
			HMIUser hMIUser = theuser;
			foreach (HMIUser hMIUser2 in HMIUsers)
			{
				if (hMIUser.name == hMIUser2.name || !(hMIUser2.name == textBox1.Text))
				{
					continue;
				}
				MessageBox.Show("该用户名已经被使用");
				return;
			}
			if (textBox3.Text != textBox2.Text)
			{
				MessageBox.Show("两次输入的口令不同");
				return;
			}
			hMIUser.name = textBox1.Text;
			if (textBox2.Text != "********")
			{
				hMIUser.password = ClassForUsers.getMd5Hash(textBox2.Text);
			}
			hMIUser.type = ((UserType)comboBox1.SelectedItem).Id;
			hMIUser.time = -1;
			hMIUser.Regions.Clear();
			foreach (object checkedItem in checkedListBox1.CheckedItems)
			{
				hMIUser.Regions.Add(((SafeRegions)checkedItem).Id);
			}
			TreeNode treeNode = thenode;
			thenode = thenode.Parent.Nodes.Add(hMIUser.name, hMIUser.name, "Snap1.bmp", "Snap1.bmp");
			treeNode.Remove();
			theuser = hMIUser;
		}
		catch (Exception)
		{
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (thenode != null && theuser != null && !(thenode.Name == "Class"))
		{
			HMIUsers.Remove(theuser);
			thenode.Remove();
			thenode = null;
			theuser = null;
		}
	}

	private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		CEditEnvironmentGlobal.dhp.HMIUsers = HMIUsers.ToArray();
		CEditEnvironmentGlobal.dhp.UserTypes = UserTypes.ToArray();
		CEditEnvironmentGlobal.dhp.SafeRegions = SafeRegions.ToArray();
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			checkedListBox1.SetItemChecked(i, checkBox1.Checked);
		}
	}

	private void label3_Click(object sender, EventArgs e)
	{
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		UserType userType = (UserType)comboBox1.SelectedItem;
		if (userType == null)
		{
			return;
		}
		for (int i = 0; i < checkedListBox1.Items.Count; i++)
		{
			checkedListBox1.SetItemChecked(i, value: false);
			foreach (int region in userType.Regions)
			{
				if (((SafeRegions)checkedListBox1.Items[i]).Id == region)
				{
					checkedListBox1.SetItemChecked(i, value: true);
					break;
				}
			}
		}
	}

	private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private bool usertypeidinuse(int i)
	{
		return ClassForUsers.UserTypeInUseByID(i, UserTypes);
	}

	private void comboBox1_DropDown(object sender, EventArgs e)
	{
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
		if (leftType)
		{
			base.Width -= 110;
			base.Location = new Point(base.Location.X + 110, base.Location.Y);
			panel1.Location = new Point(panel1.Location.X - 110, panel1.Location.Y);
			pictureBox1.Image = global::HMIEditEnvironment_图标.FillLeft;
			leftType = false;
		}
		else
		{
			base.Width += 110;
			base.Location = new Point(base.Location.X - 110, base.Location.Y);
			panel1.Location = new Point(panel1.Location.X + 110, panel1.Location.Y);
			pictureBox1.Image = global::HMIEditEnvironment_图标.FillRight;
			leftType = true;
		}
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void button5_Click(object sender, EventArgs e)
	{
		UserTypeADD userTypeADD = new UserTypeADD(SafeRegions);
		if (userTypeADD.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		object[] obj = userTypeADD.obj;
		UserType userType = new UserType();
		if (ClassForUsers.FindUserType((string)obj[0], UserTypes) != null)
		{
			MessageBox.Show("因名称重复,添加失败");
			return;
		}
		int i;
		for (i = 0; usertypeidinuse(i); i++)
		{
		}
		userType.Id = i;
		userType.UserTypeName = (string)obj[0];
		userType.Regions = (List<int>)obj[1];
		if (thenode == null)
		{
			treeView1.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			UserTypes.Add(userType);
		}
		else if (thenode.Parent == null && MessageBox.Show("是否要添加根节点?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			treeView1.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			UserTypes.Add(userType);
		}
		else if (thenode.Name != "Class")
		{
			thenode.Parent.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			if (ClassForUsers.FindUserType(thenode.Parent.Text, UserTypes).SubUserTypes == null)
			{
				ClassForUsers.FindUserType(thenode.Parent.Text, UserTypes).SubUserTypes = new List<UserType> { userType };
			}
			else
			{
				ClassForUsers.FindUserType(thenode.Parent.Text, UserTypes).SubUserTypes.Add(userType);
			}
		}
		else
		{
			thenode.Nodes.Add("Class", userType.UserTypeName, "CreateMailRule.png", "CreateMailRule.png");
			if (ClassForUsers.FindUserType(thenode.Text, UserTypes).SubUserTypes == null)
			{
				ClassForUsers.FindUserType(thenode.Text, UserTypes).SubUserTypes = new List<UserType> { userType };
			}
			else
			{
				ClassForUsers.FindUserType(thenode.Text, UserTypes).SubUserTypes.Add(userType);
			}
		}
	}

	private void button6_Click(object sender, EventArgs e)
	{
		if (thenode == null || thenode.Name != "Class")
		{
			return;
		}
		UserType userType = ClassForUsers.FindUserType(thenode.Text, UserTypes);
		UserTypeEdit userTypeEdit = new UserTypeEdit(userType, SafeRegions);
		if (userTypeEdit.ShowDialog() == DialogResult.OK)
		{
			object[] obj = userTypeEdit.obj;
			UserType userType2 = ClassForUsers.FindUserType((string)obj[0], UserTypes);
			if (userType2 != null && userType2.UserTypeName != thenode.Text)
			{
				MessageBox.Show("因名称重复,修改失败");
				return;
			}
			userType.Regions = (List<int>)obj[1];
			userType.UserTypeName = (string)obj[0];
			thenode.Text = (string)obj[0];
		}
	}

	private void button7_Click(object sender, EventArgs e)
	{
		if (thenode == null || thenode.Name != "Class")
		{
			return;
		}
		UserType userType = ClassForUsers.FindUserType(thenode.Text, UserTypes);
		if (userType.Id == 0)
		{
			MessageBox.Show("系统管理员级不可被删除.");
		}
		else
		{
			if (MessageBox.Show("删除工种将同时删除用户，确定要删除?", "警告", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			thenode.Remove();
			HMIUser[] array = HMIUsers.ToArray();
			foreach (HMIUser hMIUser in array)
			{
				if (hMIUser.type == userType.Id)
				{
					HMIUsers.Remove(hMIUser);
				}
			}
			ClassForUsers.DelUserType(userType.UserTypeName, UserTypes);
			thenode = treeView1.SelectedNode;
		}
	}

	private void pictureBox2_Click(object sender, EventArgs e)
	{
		if (rightType)
		{
			base.Width -= 110;
			pictureBox2.Image = global::HMIEditEnvironment_图标.FillRight;
			rightType = false;
		}
		else
		{
			base.Width += 110;
			pictureBox2.Image = global::HMIEditEnvironment_图标.FillLeft;
			rightType = true;
		}
	}

	private bool saferegionidinuse(int i)
	{
		foreach (SafeRegions safeRegion in SafeRegions)
		{
			if (safeRegion.Id == i)
			{
				return true;
			}
		}
		return false;
	}

	private void button10_Click(object sender, EventArgs e)
	{
		SafeRegionADD safeRegionADD = new SafeRegionADD();
		if (safeRegionADD.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string str = safeRegionADD.str;
		SafeRegions safeRegions = new SafeRegions();
		foreach (SafeRegions safeRegion in SafeRegions)
		{
			if (safeRegion.RegionName == str)
			{
				MessageBox.Show("因名称重复,添加失败");
				return;
			}
		}
		int i;
		for (i = 0; saferegionidinuse(i); i++)
		{
		}
		safeRegions.Id = i;
		safeRegions.RegionName = str;
		SafeRegions.Add(safeRegions);
		checkedListBox1.Items.Clear();
		foreach (SafeRegions safeRegion2 in SafeRegions)
		{
			checkedListBox1.Items.Add(safeRegion2);
		}
	}

	private void button4_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button9_Click(object sender, EventArgs e)
	{
		SafeRegions safeRegions = (SafeRegions)checkedListBox1.SelectedItem;
		if (safeRegions == null)
		{
			return;
		}
		SafeRegionEdit safeRegionEdit = new SafeRegionEdit(safeRegions.RegionName);
		if (safeRegionEdit.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string str = safeRegionEdit.str;
		foreach (SafeRegions safeRegion in SafeRegions)
		{
			if (safeRegion.RegionName == safeRegions.RegionName || !(str == safeRegion.RegionName))
			{
				continue;
			}
			MessageBox.Show("因名称重复,修改失败");
			return;
		}
		safeRegions.RegionName = str;
		checkedListBox1.Refresh();
	}

	private void button8_Click(object sender, EventArgs e)
	{
		SafeRegions safeRegions = (SafeRegions)checkedListBox1.SelectedItem;
		if (safeRegions == null)
		{
			return;
		}
		if (safeRegions.Id < 5)
		{
			MessageBox.Show("默认安全区无法删除");
		}
		else
		{
			if (MessageBox.Show("删除安全区将导致需要该安全区的操作开放，确定要删除?", "警告", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			List<UserType> userTypeList = ClassForUsers.GetUserTypeList(UserTypes);
			foreach (UserType item in userTypeList)
			{
				item.Regions.Remove(safeRegions.Id);
			}
			foreach (HMIUser hMIUser in HMIUsers)
			{
				hMIUser.Regions.Remove(safeRegions.Id);
			}
			foreach (DataFile df in CEditEnvironmentGlobal.dfs)
			{
				foreach (CShape item2 in df.ListAllShowCShape)
				{
					foreach (List<int> userRegion in item2.UserRegionList)
					{
						userRegion.Remove(safeRegions.Id);
					}
				}
			}
			checkedListBox1.Items.Remove(safeRegions);
			SafeRegions.Remove(safeRegions);
		}
	}

	private void comboBox1_DropDownClosed(object sender, EventArgs e)
	{
	}

	private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
	}

	private void treeView2_VisibleChanged(object sender, EventArgs e)
	{
		if (!treeView2.Visible)
		{
		}
	}

	private void comboBox1_MouseDown(object sender, MouseEventArgs e)
	{
		ClassForUsers.MakeUserTypeTree(new TreeView(), comboBox1, UserTypes, HMIUsers);
		ClassForUsers.MakeUserTypeTree(treeView2, UserTypes, HMIUsers);
		treeView2.Visible = true;
		comboBox1.Visible = false;
		treeView2.Focus();
	}

	private void treeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		foreach (object item in comboBox1.Items)
		{
			if (((UserType)item).UserTypeName == e.Node.Text)
			{
				comboBox1.SelectedItem = item;
				treeView2.Visible = false;
				comboBox1.Visible = true;
				break;
			}
		}
	}

	private void treeView2_LostFocus(object sender, EventArgs e)
	{
		treeView2.Visible = false;
		comboBox1.Visible = true;
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(98, 50);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(198, 296);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户列表";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView2);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(303, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 334);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户信息";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(118, 78);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(164, 179);
            this.treeView2.TabIndex = 18;
            this.treeView2.Visible = false;
            this.treeView2.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseClick);
            this.treeView2.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseDoubleClick);
            this.treeView2.VisibleChanged += new System.EventHandler(this.treeView2_VisibleChanged);
            this.treeView2.LostFocus += new System.EventHandler(this.treeView2_LostFocus);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(147, 299);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 27);
            this.button4.TabIndex = 17;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(43, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 27);
            this.button3.TabIndex = 15;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 245);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 27);
            this.button2.TabIndex = 14;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(118, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(116, 22);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.comboBox1_DropDownClosed);
            this.comboBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseDown);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(118, 160);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(116, 22);
            this.textBox3.TabIndex = 10;
            this.textBox3.UseSystemPasswordChar = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(118, 119);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 22);
            this.textBox2.TabIndex = 9;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(116, 22);
            this.textBox1.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(348, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(50, 18);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(267, 41);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(165, 276);
            this.checkedListBox1.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(265, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "安全区";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "核实口令";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "口    令";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "工    种";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "用 户 名";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 58);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 27);
            this.button5.TabIndex = 4;
            this.button5.Text = "添加工种";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(3, 92);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 27);
            this.button6.TabIndex = 5;
            this.button6.Text = "修改工种";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(3, 126);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(87, 27);
            this.button7.TabIndex = 6;
            this.button7.Text = "删除工种";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(-91, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 372);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(762, 126);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(87, 27);
            this.button8.TabIndex = 10;
            this.button8.Text = "删除安全区";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(762, 92);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(87, 27);
            this.button9.TabIndex = 9;
            this.button9.Text = "修改安全区";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(762, 58);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(87, 27);
            this.button10.TabIndex = 8;
            this.button10.Text = "添加安全区";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HMIEditEnvironment_图标.FillLeft;
            this.pictureBox1.Location = new System.Drawing.Point(98, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HMIEditEnvironment_图标.FillRight;
            this.pictureBox2.Location = new System.Drawing.Point(411, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(19, 19);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 362);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.helpProvider1.SetHelpKeyword(this, "10.10.1.1编辑窗口用户管理界面.htm");
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.helpProvider1.SetShowHelp(this, true);
            this.Text = "用户管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

	}
}
