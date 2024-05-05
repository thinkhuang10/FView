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
		listBox1 = new System.Windows.Forms.ListBox();
		base.SuspendLayout();
		listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		listBox1.FormattingEnabled = true;
		listBox1.ItemHeight = 12;
		listBox1.Location = new System.Drawing.Point(0, 0);
		listBox1.Name = "listBox1";
		listBox1.Size = new System.Drawing.Size(189, 256);
		listBox1.TabIndex = 0;
		listBox1.DoubleClick += new System.EventHandler(listBox1_DoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(189, 256);
		base.Controls.Add(listBox1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "SLChooseVar";
		Text = "选择变量";
		base.Load += new System.EventHandler(SLChooseVar_Load);
		base.ResumeLayout(false);
	}
}
