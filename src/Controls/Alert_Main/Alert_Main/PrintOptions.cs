using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Alert_Main;

public class PrintOptions : Form
{
	public string pname = "";

	private IContainer components;

	internal CheckBox chkFitToPageWidth;

	internal Label lblTitle;

	internal TextBox txtTitle;

	internal GroupBox gboxRowsToPrint;

	internal RadioButton rdoSelectedRows;

	internal RadioButton rdoAllRows;

	internal Label lblColumnsToPrint;

	protected Button btnOK;

	protected Button btnCancel;

	internal CheckedListBox chklst;

	private PrintDialog printDialog1;

	private HelpProvider helpProvider;

	public string PrintTitle => txtTitle.Text;

	public bool PrintAllRows => rdoAllRows.Checked;

	public bool FitToPageWidth => chkFitToPageWidth.Checked;

	public PrintOptions()
	{
		InitializeComponent();
	}

	public PrintOptions(List<string> availableFields)
	{
		InitializeComponent();
		foreach (string availableField in availableFields)
		{
			chklst.Items.Add(availableField, isChecked: true);
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (printDialog1.ShowDialog() == DialogResult.OK)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	public List<string> GetSelectedColumns()
	{
		List<string> list = new List<string>();
		foreach (object checkedItem in chklst.CheckedItems)
		{
			list.Add(checkedItem.ToString());
		}
		return list;
	}

	private void PrintOptions_Load(object sender, EventArgs e)
	{
		helpProvider.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Main.PrintOptions));
		this.chkFitToPageWidth = new System.Windows.Forms.CheckBox();
		this.lblTitle = new System.Windows.Forms.Label();
		this.txtTitle = new System.Windows.Forms.TextBox();
		this.gboxRowsToPrint = new System.Windows.Forms.GroupBox();
		this.rdoSelectedRows = new System.Windows.Forms.RadioButton();
		this.rdoAllRows = new System.Windows.Forms.RadioButton();
		this.lblColumnsToPrint = new System.Windows.Forms.Label();
		this.chklst = new System.Windows.Forms.CheckedListBox();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.printDialog1 = new System.Windows.Forms.PrintDialog();
		this.helpProvider = new System.Windows.Forms.HelpProvider();
		this.gboxRowsToPrint.SuspendLayout();
		base.SuspendLayout();
		this.chkFitToPageWidth.AutoSize = true;
		this.chkFitToPageWidth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkFitToPageWidth.Checked = true;
		this.chkFitToPageWidth.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkFitToPageWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkFitToPageWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkFitToPageWidth.Location = new System.Drawing.Point(203, 76);
		this.chkFitToPageWidth.Name = "chkFitToPageWidth";
		this.chkFitToPageWidth.Size = new System.Drawing.Size(84, 18);
		this.chkFitToPageWidth.TabIndex = 29;
		this.chkFitToPageWidth.Text = "适应页宽";
		this.chkFitToPageWidth.UseVisualStyleBackColor = true;
		this.lblTitle.AutoSize = true;
		this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTitle.Location = new System.Drawing.Point(200, 103);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(59, 13);
		this.lblTitle.TabIndex = 28;
		this.lblTitle.Text = "标题设置";
		this.txtTitle.AcceptsReturn = true;
		this.txtTitle.Location = new System.Drawing.Point(200, 118);
		this.txtTitle.Multiline = true;
		this.txtTitle.Name = "txtTitle";
		this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtTitle.Size = new System.Drawing.Size(176, 108);
		this.txtTitle.TabIndex = 27;
		this.gboxRowsToPrint.Controls.Add(this.rdoSelectedRows);
		this.gboxRowsToPrint.Controls.Add(this.rdoAllRows);
		this.gboxRowsToPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gboxRowsToPrint.Location = new System.Drawing.Point(200, 24);
		this.gboxRowsToPrint.Name = "gboxRowsToPrint";
		this.gboxRowsToPrint.Size = new System.Drawing.Size(173, 39);
		this.gboxRowsToPrint.TabIndex = 26;
		this.gboxRowsToPrint.TabStop = false;
		this.gboxRowsToPrint.Text = "打印行范围";
		this.rdoSelectedRows.AutoSize = true;
		this.rdoSelectedRows.Enabled = false;
		this.rdoSelectedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rdoSelectedRows.Location = new System.Drawing.Point(91, 18);
		this.rdoSelectedRows.Name = "rdoSelectedRows";
		this.rdoSelectedRows.Size = new System.Drawing.Size(64, 17);
		this.rdoSelectedRows.TabIndex = 1;
		this.rdoSelectedRows.Text = "选择行";
		this.rdoSelectedRows.UseVisualStyleBackColor = true;
		this.rdoAllRows.AutoSize = true;
		this.rdoAllRows.Checked = true;
		this.rdoAllRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rdoAllRows.Location = new System.Drawing.Point(9, 18);
		this.rdoAllRows.Name = "rdoAllRows";
		this.rdoAllRows.Size = new System.Drawing.Size(64, 17);
		this.rdoAllRows.TabIndex = 0;
		this.rdoAllRows.TabStop = true;
		this.rdoAllRows.Text = "全部行";
		this.rdoAllRows.UseVisualStyleBackColor = true;
		this.lblColumnsToPrint.AutoSize = true;
		this.lblColumnsToPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblColumnsToPrint.Location = new System.Drawing.Point(24, 12);
		this.lblColumnsToPrint.Name = "lblColumnsToPrint";
		this.lblColumnsToPrint.Size = new System.Drawing.Size(98, 13);
		this.lblColumnsToPrint.TabIndex = 25;
		this.lblColumnsToPrint.Text = "选择要打印的列";
		this.chklst.CheckOnClick = true;
		this.chklst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chklst.FormattingEnabled = true;
		this.chklst.Location = new System.Drawing.Point(24, 30);
		this.chklst.Name = "chklst";
		this.chklst.Size = new System.Drawing.Size(170, 214);
		this.chklst.TabIndex = 22;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
		this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnOK.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178);
		this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnOK.Image = (System.Drawing.Image)resources.GetObject("btnOK.Image");
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Location = new System.Drawing.Point(200, 232);
		this.btnOK.Name = "btnOK";
		this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnOK.Size = new System.Drawing.Size(56, 23);
		this.btnOK.TabIndex = 23;
		this.btnOK.Text = "打印";
		this.btnOK.UseVisualStyleBackColor = false;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178);
		this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnCancel.Image = (System.Drawing.Image)resources.GetObject("btnCancel.Image");
		this.btnCancel.Location = new System.Drawing.Point(316, 232);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnCancel.Size = new System.Drawing.Size(56, 23);
		this.btnCancel.TabIndex = 24;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.printDialog1.UseEXDialog = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(390, 283);
		base.Controls.Add(this.chkFitToPageWidth);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add(this.txtTitle);
		base.Controls.Add(this.gboxRowsToPrint);
		base.Controls.Add(this.lblColumnsToPrint);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.chklst);
		this.helpProvider.SetHelpKeyword(this, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "PrintOptions";
		this.helpProvider.SetShowHelp(this, true);
		this.Text = "打印设置";
		base.Load += new System.EventHandler(PrintOptions_Load);
		this.gboxRowsToPrint.ResumeLayout(false);
		this.gboxRowsToPrint.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
