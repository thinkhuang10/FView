using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class PageOpenerForm : Form
{
	public class OpenPageEventArgs : EventArgs
	{
		public string PageName { get; set; }
	}

	private IContainer components;

	private ListView listView_Pages;

	private Button btnOK;

	private Button btnCancel;

	private ImageList imageList1;

	public event EventHandler<OpenPageEventArgs> OpenPageEvent;

	public PageOpenerForm()
	{
		InitializeComponent();
	}

	private void openPageForm_Load(object sender, EventArgs e)
	{
		foreach (DataFile df in CEditEnvironmentGlobal.dfs)
		{
			listView_Pages.Items.Add(df.name, df.pageName, "Generic_Document.png");
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (listView_Pages.SelectedItems.Count > 0 && OpenPageEvent != null)
		{
			OpenPageEvent(sender, new OpenPageEventArgs
			{
				PageName = listView_Pages.SelectedItems[0].Name
			});
		}
	}

	private void listView_Pages_DoubleClick(object sender, EventArgs e)
	{
		if (listView_Pages.SelectedItems.Count > 0 && OpenPageEvent != null)
		{
			OpenPageEvent(sender, new OpenPageEventArgs
			{
				PageName = listView_Pages.SelectedItems[0].Name
			});
		}
	}

	private void listView_Pages_SelectedIndexChanged(object sender, EventArgs e)
	{
		btnOK.Enabled = listView_Pages.SelectedItems.Count > 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.PageOpenerForm));
		listView_Pages = new System.Windows.Forms.ListView();
		imageList1 = new System.Windows.Forms.ImageList(components);
		btnOK = new System.Windows.Forms.Button();
		btnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		listView_Pages.HideSelection = false;
		listView_Pages.LargeImageList = imageList1;
		listView_Pages.Location = new System.Drawing.Point(12, 12);
		listView_Pages.MultiSelect = false;
		listView_Pages.Name = "listView_Pages";
		listView_Pages.Size = new System.Drawing.Size(539, 192);
		listView_Pages.SmallImageList = imageList1;
		listView_Pages.TabIndex = 0;
		listView_Pages.UseCompatibleStateImageBehavior = false;
		listView_Pages.SelectedIndexChanged += new System.EventHandler(listView_Pages_SelectedIndexChanged);
		listView_Pages.DoubleClick += new System.EventHandler(listView_Pages_DoubleClick);
		imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		imageList1.TransparentColor = System.Drawing.Color.Transparent;
		//this.imageList1.Images.SetKeyName(0, "Generic_Document.png");
		btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		btnOK.Enabled = false;
		btnOK.Location = new System.Drawing.Point(374, 212);
		btnOK.Name = "btnOK";
		btnOK.Size = new System.Drawing.Size(75, 23);
		btnOK.TabIndex = 1;
		btnOK.Text = "确 定";
		btnOK.UseVisualStyleBackColor = true;
		btnOK.Click += new System.EventHandler(btnOK_Click);
		btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		btnCancel.Location = new System.Drawing.Point(455, 212);
		btnCancel.Name = "btnCancel";
		btnCancel.Size = new System.Drawing.Size(75, 23);
		btnCancel.TabIndex = 2;
		btnCancel.Text = "取 消";
		btnCancel.UseVisualStyleBackColor = true;
		base.AcceptButton = btnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = btnCancel;
		base.ClientSize = new System.Drawing.Size(563, 247);
		base.Controls.Add(btnCancel);
		base.Controls.Add(btnOK);
		base.Controls.Add(listView_Pages);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "PageOpenerForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		Text = "选择页面";
		base.Load += new System.EventHandler(openPageForm_Load);
		base.ResumeLayout(false);
	}
}
