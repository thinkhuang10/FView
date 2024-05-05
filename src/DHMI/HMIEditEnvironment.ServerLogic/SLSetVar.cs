using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment.ServerLogic;

public class SLSetVar : Form
{
	private readonly List<string> serverVars = new();

	private ServerLogicItem result;

	private Label label1;

	private Label label2;

	private TextBox textBox2;

	private Button button1;

	private Button button2;

	private Button button4;

	private Label label3;

	private TextBox textBox3;

	private Button button5;

	private ComboBox comboBox1;

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

	public SLSetVar(List<string> _serverVars)
	{
		InitializeComponent();
		serverVars = _serverVars;
		comboBox1.Items.AddRange(_serverVars.ToArray());
	}

	public SLSetVar(List<string> _serverVars, ServerLogicItem item)
	{
		InitializeComponent();
		serverVars = _serverVars;
		comboBox1.Items.AddRange(_serverVars.ToArray());
		result = item;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		List<string> list = new(serverVars)
        {
            "System.AddRow([dataTable])",
            "System.AddRowAt([dataTable],[index])",
            "System.AddColumn([dataTable])",
            "System.SetColumnName([dataTable],[columnIndex],[name])",
            "System.GetColumnName([dataTable],[columnIndex])",
            "System.GetRowCount([dataTable])",
            "System.GetColumnCount([dataTable])",
            "System.RemoveRowAt([dataTable],[index])",
            "System.RemoveColumnAt([dataTable],[index])",
            "System.GetCellValue([dataTable],[i],[j])",
            "System.SetCellValue([dataTable],[i],[j],[value])"
        };
		SLChooseVar sLChooseVar = new(list);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox2.Text += sLChooseVar.Result;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (!(textBox2.Text == "") && comboBox1.SelectedItem != null)
		{
            result = new ServerLogicItem
            {
                LogicType = "变量赋值",
                ConditionalExpression = textBox3.Text
            };
            Dictionary<string, object> dictionary = new()
            {
                { "Name", comboBox1.SelectedItem }
            };
			if (textBox3.Text == "")
			{
				textBox3.Text = "true";
			}
			dictionary.Add("Value", textBox2.Text);
			result.DataDict = dictionary;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void EventSetVar_Load(object sender, EventArgs e)
	{
		try
		{
			if (result != null && result.LogicType == "变量赋值")
			{
				textBox3.Text = result.ConditionalExpression;
				if (result.DataDict.ContainsKey("Name"))
				{
					comboBox1.SelectedItem = result.DataDict["Name"];
				}
				if (result.DataDict.ContainsKey("Value"))
				{
					textBox2.Text = result.DataDict["Value"].ToString();
				}
			}
		}
		catch
		{
		}
	}

	private void button5_Click(object sender, EventArgs e)
	{
		SLChooseVar sLChooseVar = new(serverVars, textBox3.Text);
		if (sLChooseVar.ShowDialog() == DialogResult.OK)
		{
			textBox3.Text = sLChooseVar.Result;
		}
	}

	private void InitializeComponent()
	{
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		textBox2 = new System.Windows.Forms.TextBox();
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		button4 = new System.Windows.Forms.Button();
		label3 = new System.Windows.Forms.Label();
		textBox3 = new System.Windows.Forms.TextBox();
		button5 = new System.Windows.Forms.Button();
		comboBox1 = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(13, 42);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(29, 12);
		label1.TabIndex = 0;
		label1.Text = "变量";
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(13, 69);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(17, 12);
		label2.TabIndex = 0;
		label2.Text = "值";
		textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox2.Location = new System.Drawing.Point(48, 66);
		textBox2.Name = "textBox2";
		textBox2.Size = new System.Drawing.Size(350, 21);
		textBox2.TabIndex = 3;
		button1.Location = new System.Drawing.Point(100, 101);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 5;
		button1.Text = "保  存";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button2.Location = new System.Drawing.Point(271, 101);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 6;
		button2.Text = "关  闭";
		button2.UseVisualStyleBackColor = true;
		button2.Click += new System.EventHandler(button2_Click);
		button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button4.Location = new System.Drawing.Point(404, 64);
		button4.Name = "button4";
		button4.Size = new System.Drawing.Size(30, 23);
		button4.TabIndex = 4;
		button4.Text = "...";
		button4.UseVisualStyleBackColor = true;
		button4.Click += new System.EventHandler(button4_Click);
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(13, 15);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(29, 12);
		label3.TabIndex = 0;
		label3.Text = "条件";
		textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBox3.Location = new System.Drawing.Point(48, 12);
		textBox3.Name = "textBox3";
		textBox3.Size = new System.Drawing.Size(350, 21);
		textBox3.TabIndex = 0;
		textBox3.Text = "true";
		button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button5.Location = new System.Drawing.Point(404, 12);
		button5.Name = "button5";
		button5.Size = new System.Drawing.Size(30, 23);
		button5.TabIndex = 1;
		button5.Text = "...";
		button5.UseVisualStyleBackColor = true;
		button5.Click += new System.EventHandler(button5_Click);
		comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		comboBox1.FormattingEnabled = true;
		comboBox1.Location = new System.Drawing.Point(48, 40);
		comboBox1.Name = "comboBox1";
		comboBox1.Size = new System.Drawing.Size(386, 20);
		comboBox1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(446, 133);
		base.Controls.Add(comboBox1);
		base.Controls.Add(button2);
		base.Controls.Add(button4);
		base.Controls.Add(button5);
		base.Controls.Add(button1);
		base.Controls.Add(textBox2);
		base.Controls.Add(textBox3);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetVar";
		Text = "变量赋值";
		base.Load += new System.EventHandler(EventSetVar_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
