using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class setFirstPageForm : Form
{
	private IContainer components;

	private ListView listView1;

	private Button button1;

	private Button button2;

	private ImageList imageList1;

	public setFirstPageForm()
	{
		InitializeComponent();
	}

	private void openPageForm_Load(object sender, EventArgs e)
	{
		foreach (DataFile df in CEditEnvironmentGlobal.dfs)
		{
			listView1.Items.Add(df.name, df.pageName, "Generic_Document.png");
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		foreach (ListViewItem selectedItem in listView1.SelectedItems)
		{
			foreach (DataFile df in CEditEnvironmentGlobal.dfs)
			{
				if (df.name == selectedItem.Name)
				{
					df.visable = true;
					break;
				}
			}
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void listView1_DoubleClick(object sender, EventArgs e)
	{
		if (listView1.SelectedItems.Count <= 0)
		{
			return;
		}
		foreach (ListViewItem selectedItem in listView1.SelectedItems)
		{
			foreach (DataFile df in CEditEnvironmentGlobal.dfs)
			{
				if (df.name == selectedItem.Name)
				{
					df.visable = true;
					break;
				}
			}
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
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
		components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.setFirstPageForm));
		listView1 = new System.Windows.Forms.ListView();
		imageList1 = new System.Windows.Forms.ImageList(components);
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		listView1.LargeImageList = imageList1;
		listView1.Location = new System.Drawing.Point(12, 12);
		listView1.Name = "listView1";
		listView1.Size = new System.Drawing.Size(539, 192);
		listView1.SmallImageList = imageList1;
		listView1.TabIndex = 0;
		listView1.UseCompatibleStateImageBehavior = false;
		listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
		imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		imageList1.TransparentColor = System.Drawing.Color.Transparent;
		//this.imageList1.Images.SetKeyName(0, "Generic_Document.png");
		button1.Location = new System.Drawing.Point(364, 212);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 1;
		button1.Text = "确定";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		button2.Location = new System.Drawing.Point(445, 212);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 2;
		button2.Text = "取消";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		base.AcceptButton = button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = button2;
		base.ClientSize = new System.Drawing.Size(563, 247);
		base.Controls.Add(button2);
		base.Controls.Add(button1);
		base.Controls.Add(listView1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "setFirstPageForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "选择页面";
		base.Load += new System.EventHandler(openPageForm_Load);
		base.ResumeLayout(false);
	}
}
