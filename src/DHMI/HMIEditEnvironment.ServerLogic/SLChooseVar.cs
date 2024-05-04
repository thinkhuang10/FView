using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment.ServerLogic;

public class SLChooseVar : Form
{
	public string Result;

	private ListBox listBox1;

	public SLChooseVar(List<string> vars)
	{
		InitializeComponent();
		listBox1.Items.AddRange(vars.ToArray());
		if (listBox1.Items.Count != 0)
		{
			listBox1.SelectedItem = listBox1.Items[0];
		}
	}

	public SLChooseVar(List<string> vars, string _result)
	{
		InitializeComponent();
		listBox1.Items.AddRange(vars.ToArray());
		Result = _result;
		listBox1.SelectedItem = _result;
	}

	private void SLChooseVar_Load(object sender, EventArgs e)
	{
	}

	private void listBox1_DoubleClick(object sender, EventArgs e)
	{
		if (listBox1.SelectedItem != null)
		{
			Result = listBox1.SelectedItem.ToString();
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void InitializeComponent()
	{
		this.listBox1 = new System.Windows.Forms.ListBox();
		base.SuspendLayout();
		this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 12;
		this.listBox1.Location = new System.Drawing.Point(0, 0);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(189, 256);
		this.listBox1.TabIndex = 0;
		this.listBox1.DoubleClick += new System.EventHandler(listBox1_DoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(189, 256);
		base.Controls.Add(this.listBox1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "SLChooseVar";
		this.Text = "选择变量";
		base.Load += new System.EventHandler(SLChooseVar_Load);
		base.ResumeLayout(false);
	}
}
