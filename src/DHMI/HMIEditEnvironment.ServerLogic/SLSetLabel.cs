using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetLabel : Form
{
	private ServerLogicItem result;

	private readonly List<string> inusestr = new();

	private Label label1;

	private TextBox tb_LabelName;

	private Button btn_Save;

	private Button btn_Close;

	public ServerLogicItem Result
	{
		get
		{
			return result;
		}
		set
		{
			result = value;
		}
	}

	public SLSetLabel()
	{
		InitializeComponent();
	}

	public SLSetLabel(ServerLogicItem item)
	{
		InitializeComponent();
		tb_LabelName.Text = item.DataDict["Name"].ToString();
	}

	private void btn_Save_Click(object sender, EventArgs e)
	{
		if (tb_LabelName.Text == "")
		{
			return;
		}
		Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
		if (!regex.IsMatch(tb_LabelName.Text))
		{
			MessageBox.Show("标签中仅可以由字母数字以及下划线组成，并且不能以数字开头。", "错误");
			return;
		}
		foreach (string item in inusestr)
		{
			if (tb_LabelName.Text.Trim() == item)
			{
				MessageBox.Show("标签名称已经存在，请修改标签名称。", "错误");
				return;
			}
		}
        ServerLogicItem serverLogicItem = new()
        {
            LogicType = "定义标签",
            DataDict = new Dictionary<string, object>
        {
            { "Name", tb_LabelName.Text }
        }
        };
        result = serverLogicItem;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btn_Close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void SLSetLabel_Load(object sender, EventArgs e)
	{
		if (result != null)
		{
			tb_LabelName.Text = result.LogicType;
			inusestr.Remove(result.DataDict["Name"].ToString());
		}
	}

	private void tb_LabelName_TextChanged(object sender, EventArgs e)
	{
	}

	private void InitializeComponent()
	{
		label1 = new System.Windows.Forms.Label();
		tb_LabelName = new System.Windows.Forms.TextBox();
		btn_Save = new System.Windows.Forms.Button();
		btn_Close = new System.Windows.Forms.Button();

		base.SuspendLayout();
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(24, 17);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(53, 12);
		label1.TabIndex = 0;
		label1.Text = "标签名称";
		tb_LabelName.Location = new System.Drawing.Point(93, 13);
		tb_LabelName.Name = "tb_LabelName";
		tb_LabelName.Size = new System.Drawing.Size(133, 21);
		tb_LabelName.TabIndex = 0;
		tb_LabelName.TextChanged += new System.EventHandler(tb_LabelName_TextChanged);
		btn_Save.Location = new System.Drawing.Point(44, 61);
		btn_Save.Name = "btn_Save";
		btn_Save.Size = new System.Drawing.Size(75, 23);
		btn_Save.TabIndex = 1;
		btn_Save.Text = "保存";
		btn_Save.UseVisualStyleBackColor = true;
		btn_Save.Click += new System.EventHandler(btn_Save_Click);
		btn_Close.Location = new System.Drawing.Point(160, 61);
		btn_Close.Name = "btn_Close";
		btn_Close.Size = new System.Drawing.Size(75, 23);
		btn_Close.TabIndex = 2;
		btn_Close.Text = "关闭";
		btn_Close.UseVisualStyleBackColor = true;
		btn_Close.Click += new System.EventHandler(btn_Close_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 112);
		base.Controls.Add(btn_Close);
		base.Controls.Add(btn_Save);
		base.Controls.Add(tb_LabelName);
		base.Controls.Add(label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetLabel";
		Text = "定义标签";
		base.Load += new System.EventHandler(SLSetLabel_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
