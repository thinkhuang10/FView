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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.button5 = new System.Windows.Forms.Button();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 42);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "变量";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(13, 69);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "值";
		this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox2.Location = new System.Drawing.Point(48, 66);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(350, 21);
		this.textBox2.TabIndex = 3;
		this.button1.Location = new System.Drawing.Point(100, 101);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 5;
		this.button1.Text = "保  存";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button2.Location = new System.Drawing.Point(271, 101);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 6;
		this.button2.Text = "关  闭";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button4.Location = new System.Drawing.Point(404, 64);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(30, 23);
		this.button4.TabIndex = 4;
		this.button4.Text = "...";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(13, 15);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(29, 12);
		this.label3.TabIndex = 0;
		this.label3.Text = "条件";
		this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox3.Location = new System.Drawing.Point(48, 12);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(350, 21);
		this.textBox3.TabIndex = 0;
		this.textBox3.Text = "true";
		this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button5.Location = new System.Drawing.Point(404, 12);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(30, 23);
		this.button5.TabIndex = 1;
		this.button5.Text = "...";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(48, 40);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(386, 20);
		this.comboBox1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(446, 133);
		base.Controls.Add(this.comboBox1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.Name = "SLSetVar";
		this.Text = "变量赋值";
		base.Load += new System.EventHandler(EventSetVar_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
