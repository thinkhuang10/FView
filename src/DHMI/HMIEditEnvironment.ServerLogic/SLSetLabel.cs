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

	private List<string> inusestr = new List<string>();

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
		Regex regex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$");
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
		ServerLogicItem serverLogicItem = new ServerLogicItem();
		serverLogicItem.LogicType = "定义标签";
		serverLogicItem.DataDict = new Dictionary<string, object>
        {
            { "Name", tb_LabelName.Text }
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
		this.label1 = new System.Windows.Forms.Label();
		this.tb_LabelName = new System.Windows.Forms.TextBox();
		this.btn_Save = new System.Windows.Forms.Button();
		this.btn_Close = new System.Windows.Forms.Button();

		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(24, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "标签名称";
		this.tb_LabelName.Location = new System.Drawing.Point(93, 13);
		this.tb_LabelName.Name = "tb_LabelName";
		this.tb_LabelName.Size = new System.Drawing.Size(133, 21);
		this.tb_LabelName.TabIndex = 0;
		this.tb_LabelName.TextChanged += new System.EventHandler(tb_LabelName_TextChanged);
		this.btn_Save.Location = new System.Drawing.Point(44, 61);
		this.btn_Save.Name = "btn_Save";
		this.btn_Save.Size = new System.Drawing.Size(75, 23);
		this.btn_Save.TabIndex = 1;
		this.btn_Save.Text = "保存";
		this.btn_Save.UseVisualStyleBackColor = true;
		this.btn_Save.Click += new System.EventHandler(btn_Save_Click);
		this.btn_Close.Location = new System.Drawing.Point(160, 61);
		this.btn_Close.Name = "btn_Close";
		this.btn_Close.Size = new System.Drawing.Size(75, 23);
		this.btn_Close.TabIndex = 2;
		this.btn_Close.Text = "关闭";
		this.btn_Close.UseVisualStyleBackColor = true;
		this.btn_Close.Click += new System.EventHandler(btn_Close_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 112);
		base.Controls.Add(this.btn_Close);
		base.Controls.Add(this.btn_Save);
		base.Controls.Add(this.tb_LabelName);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetLabel";
		this.Text = "定义标签";
		base.Load += new System.EventHandler(SLSetLabel_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
