using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetGoto : Form
{
	private readonly List<string> usefulvar = new();

	private ServerLogicItem result;

	private Button button2;

	private Button button1;

	private ComboBox cb_label;

	private Button button5;

	private TextBox tb_condition;

	private Label label3;

	private Label label1;

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

	public SLSetGoto(List<string> items, List<string> usevar)
	{
		InitializeComponent();

		cb_label.Items.AddRange(items.ToArray());
		if (cb_label.Items.Count > 0)
		{
			cb_label.SelectedIndex = 0;
		}
		usefulvar = usevar;
	}

	public SLSetGoto(List<string> labels, List<string> vars, ServerLogicItem item)
	{
		InitializeComponent();

		cb_label.Items.AddRange(labels.ToArray());
		cb_label.Text = item.DataDict["Name"].ToString();
		tb_condition.Text = item.ConditionalExpression;
		usefulvar = vars;
	}

	private void SLSetGoto_Load(object sender, EventArgs e)
	{
		if (result != null)
		{
			cb_label.Text = result.DataDict["Name"].ToString();
			tb_condition.Text = result.ConditionalExpression;
		}
	}

	private void button5_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(usefulvar, tb_condition.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			tb_condition.Text = sLChooseVar.Result;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (!(tb_condition.Text == "") && !(cb_label.Text == ""))
		{
            ServerLogicItem serverLogicItem = new()
            {
                LogicType = "跳转标签",
                DataDict = new Dictionary<string, object>
            {
                { "Name", cb_label.Text }
            },
                ConditionalExpression = tb_condition.Text
            };
            result = serverLogicItem;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void InitializeComponent()
	{
		button2 = new System.Windows.Forms.Button();
		button1 = new System.Windows.Forms.Button();
		cb_label = new System.Windows.Forms.ComboBox();
		button5 = new System.Windows.Forms.Button();
		tb_condition = new System.Windows.Forms.TextBox();
		label3 = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		button2.Location = new System.Drawing.Point(177, 80);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 4;
		button2.Text = "关闭";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		button1.Location = new System.Drawing.Point(41, 80);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 3;
		button1.Text = "保存";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		cb_label.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		cb_label.FormattingEnabled = true;
		cb_label.Location = new System.Drawing.Point(57, 37);
		cb_label.Name = "cb_label";
		cb_label.Size = new System.Drawing.Size(213, 20);
		cb_label.TabIndex = 2;
		button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button5.Location = new System.Drawing.Point(240, 10);
		button5.Name = "button5";
		button5.Size = new System.Drawing.Size(30, 23);
		button5.TabIndex = 1;
		button5.Text = "...";
		button5.UseVisualStyleBackColor = true;
		button5.Click += new System.EventHandler(button5_Click);
		tb_condition.Location = new System.Drawing.Point(57, 10);
		tb_condition.Name = "tb_condition";
		tb_condition.Size = new System.Drawing.Size(177, 21);
		tb_condition.TabIndex = 0;
		tb_condition.Text = "true";
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(22, 13);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(29, 12);
		label3.TabIndex = 10;
		label3.Text = "条件";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(22, 40);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(29, 12);
		label1.TabIndex = 11;
		label1.Text = "标签";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 112);
		base.Controls.Add(button2);
		base.Controls.Add(button1);
		base.Controls.Add(cb_label);
		base.Controls.Add(button5);
		base.Controls.Add(tb_condition);
		base.Controls.Add(label3);
		base.Controls.Add(label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetGoto";
		Text = "跳转标签";
		base.Load += new System.EventHandler(SLSetGoto_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
