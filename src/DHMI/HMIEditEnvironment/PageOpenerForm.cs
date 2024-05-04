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
		if (listView_Pages.SelectedItems.Count > 0 && this.OpenPageEvent != null)
		{
			this.OpenPageEvent(sender, new OpenPageEventArgs
			{
				PageName = listView_Pages.SelectedItems[0].Name
			});
		}
	}

	private void listView_Pages_DoubleClick(object sender, EventArgs e)
	{
		if (listView_Pages.SelectedItems.Count > 0 && this.OpenPageEvent != null)
		{
			this.OpenPageEvent(sender, new OpenPageEventArgs
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.PageOpenerForm));
		this.listView_Pages = new System.Windows.Forms.ListView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.listView_Pages.HideSelection = false;
		this.listView_Pages.LargeImageList = this.imageList1;
		this.listView_Pages.Location = new System.Drawing.Point(12, 12);
		this.listView_Pages.MultiSelect = false;
		this.listView_Pages.Name = "listView_Pages";
		this.listView_Pages.Size = new System.Drawing.Size(539, 192);
		this.listView_Pages.SmallImageList = this.imageList1;
		this.listView_Pages.TabIndex = 0;
		this.listView_Pages.UseCompatibleStateImageBehavior = false;
		this.listView_Pages.SelectedIndexChanged += new System.EventHandler(listView_Pages_SelectedIndexChanged);
		this.listView_Pages.DoubleClick += new System.EventHandler(listView_Pages_DoubleClick);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		//this.imageList1.Images.SetKeyName(0, "Generic_Document.png");
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Enabled = false;
		this.btnOK.Location = new System.Drawing.Point(374, 212);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "确 定";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(455, 212);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取 消";
		this.btnCancel.UseVisualStyleBackColor = true;
		base.AcceptButton = this.btnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(563, 247);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.listView_Pages);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "PageOpenerForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "选择页面";
		base.Load += new System.EventHandler(openPageForm_Load);
		base.ResumeLayout(false);
	}
}
