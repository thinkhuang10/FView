using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetGoto : Form
{
	private List<string> usefulvar = new List<string>();

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
		SLChooseVar sLChooseVar = new SLChooseVar(usefulvar, tb_condition.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			tb_condition.Text = sLChooseVar.Result;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (!(tb_condition.Text == "") && !(cb_label.Text == ""))
		{
			ServerLogicItem serverLogicItem = new ServerLogicItem();
			serverLogicItem.LogicType = "跳转标签";
			serverLogicItem.DataDict = new Dictionary<string, object>
            {
                { "Name", cb_label.Text }
            };
			serverLogicItem.ConditionalExpression = tb_condition.Text;
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
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.cb_label = new System.Windows.Forms.ComboBox();
		this.button5 = new System.Windows.Forms.Button();
		this.tb_condition = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.button2.Location = new System.Drawing.Point(177, 80);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 4;
		this.button2.Text = "关闭";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button1.Location = new System.Drawing.Point(41, 80);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 3;
		this.button1.Text = "保存";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.cb_label.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cb_label.FormattingEnabled = true;
		this.cb_label.Location = new System.Drawing.Point(57, 37);
		this.cb_label.Name = "cb_label";
		this.cb_label.Size = new System.Drawing.Size(213, 20);
		this.cb_label.TabIndex = 2;
		this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button5.Location = new System.Drawing.Point(240, 10);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(30, 23);
		this.button5.TabIndex = 1;
		this.button5.Text = "...";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.tb_condition.Location = new System.Drawing.Point(57, 10);
		this.tb_condition.Name = "tb_condition";
		this.tb_condition.Size = new System.Drawing.Size(177, 21);
		this.tb_condition.TabIndex = 0;
		this.tb_condition.Text = "true";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(22, 13);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(29, 12);
		this.label3.TabIndex = 10;
		this.label3.Text = "条件";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(22, 40);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 11;
		this.label1.Text = "标签";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 112);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.cb_label);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.tb_condition);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetGoto";
		this.Text = "跳转标签";
		base.Load += new System.EventHandler(SLSetGoto_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
