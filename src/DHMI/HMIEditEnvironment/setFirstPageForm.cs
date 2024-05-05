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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new(typeof(HMIEditEnvironment.setFirstPageForm));
		this.listView1 = new System.Windows.Forms.ListView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.listView1.LargeImageList = this.imageList1;
		this.listView1.Location = new System.Drawing.Point(12, 12);
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(539, 192);
		this.listView1.SmallImageList = this.imageList1;
		this.listView1.TabIndex = 0;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		//this.imageList1.Images.SetKeyName(0, "Generic_Document.png");
		this.button1.Location = new System.Drawing.Point(364, 212);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "确定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.button2.Location = new System.Drawing.Point(445, 212);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		base.AcceptButton = this.button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.button2;
		base.ClientSize = new System.Drawing.Size(563, 247);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.listView1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "setFirstPageForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "选择页面";
		base.Load += new System.EventHandler(openPageForm_Load);
		base.ResumeLayout(false);
	}
}
