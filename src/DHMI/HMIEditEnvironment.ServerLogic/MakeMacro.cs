using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment.ServerLogic;

public class MakeMacro : Form
{
	private Button button1;

	private Button button2;

	private SplitContainer splitContainer1;

	private ListView listView1;

	private Button button12;

	private Button button11;

	private Button button6;

	private Button button5;

	private Button button8;

	private Button button7;

	private Button button4;

	private Button button9;

	private Button button10;

	private Button button3;

	private Button button13;

	private ListView listView3;

	private ColumnHeader columnHeader5;

	private ColumnHeader columnHeader1;

	private ColumnHeader columnHeader6;

	private ColumnHeader columnHeader2;

	private ColumnHeader columnHeader3;

	private Label label1;

	private Label label2;

	public MakeMacro()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new(typeof(HMIEditEnvironment.ServerLogic.MakeMacro));
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		splitContainer1 = new System.Windows.Forms.SplitContainer();
		listView1 = new System.Windows.Forms.ListView();
		columnHeader2 = new System.Windows.Forms.ColumnHeader();
		columnHeader3 = new System.Windows.Forms.ColumnHeader();
		label2 = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		button12 = new System.Windows.Forms.Button();
		button11 = new System.Windows.Forms.Button();
		button6 = new System.Windows.Forms.Button();
		button5 = new System.Windows.Forms.Button();
		button8 = new System.Windows.Forms.Button();
		button7 = new System.Windows.Forms.Button();
		button4 = new System.Windows.Forms.Button();
		button9 = new System.Windows.Forms.Button();
		button10 = new System.Windows.Forms.Button();
		button3 = new System.Windows.Forms.Button();
		button13 = new System.Windows.Forms.Button();
		listView3 = new System.Windows.Forms.ListView();
		columnHeader5 = new System.Windows.Forms.ColumnHeader();
		columnHeader1 = new System.Windows.Forms.ColumnHeader();
		columnHeader6 = new System.Windows.Forms.ColumnHeader();
		splitContainer1.Panel1.SuspendLayout();
		splitContainer1.Panel2.SuspendLayout();
		splitContainer1.SuspendLayout();
		base.SuspendLayout();
		button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		button1.Location = new System.Drawing.Point(16, 12);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(75, 23);
		button1.TabIndex = 1;
		button1.Text = "新建宏";
		button1.UseVisualStyleBackColor = true;
		button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		button2.Location = new System.Drawing.Point(106, 12);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(75, 23);
		button2.TabIndex = 2;
		button2.Text = "移除宏";
		button2.UseVisualStyleBackColor = true;
		splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		splitContainer1.Location = new System.Drawing.Point(0, 0);
		splitContainer1.Name = "splitContainer1";
		splitContainer1.Panel1.Controls.Add(listView1);
		splitContainer1.Panel1.Controls.Add(button2);
		splitContainer1.Panel1.Controls.Add(button1);
		splitContainer1.Panel2.Controls.Add(label2);
		splitContainer1.Panel2.Controls.Add(label1);
		splitContainer1.Panel2.Controls.Add(button12);
		splitContainer1.Panel2.Controls.Add(button11);
		splitContainer1.Panel2.Controls.Add(button6);
		splitContainer1.Panel2.Controls.Add(button5);
		splitContainer1.Panel2.Controls.Add(button8);
		splitContainer1.Panel2.Controls.Add(button7);
		splitContainer1.Panel2.Controls.Add(button4);
		splitContainer1.Panel2.Controls.Add(button9);
		splitContainer1.Panel2.Controls.Add(button10);
		splitContainer1.Panel2.Controls.Add(button3);
		splitContainer1.Panel2.Controls.Add(button13);
		splitContainer1.Panel2.Controls.Add(listView3);
		splitContainer1.Size = new System.Drawing.Size(979, 514);
		splitContainer1.SplitterDistance = 308;
		splitContainer1.TabIndex = 3;
		listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { columnHeader2, columnHeader3 });
		listView1.FullRowSelect = true;
		listView1.GridLines = true;
		listView1.HideSelection = false;
		listView1.Location = new System.Drawing.Point(3, 41);
		listView1.MultiSelect = false;
		listView1.Name = "listView1";
		listView1.Size = new System.Drawing.Size(302, 470);
		listView1.TabIndex = 3;
		listView1.UseCompatibleStateImageBehavior = false;
		listView1.View = System.Windows.Forms.View.Details;
		columnHeader2.Text = "宏名称";
		columnHeader2.Width = 102;
		columnHeader3.Text = "备注";
		columnHeader3.Width = 195;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(104, 17);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(17, 12);
		label2.TabIndex = 25;
		label2.Text = "无";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(12, 17);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(77, 12);
		label1.TabIndex = 25;
		label1.Text = "当前编辑宏：";
		button12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button12.Location = new System.Drawing.Point(583, 206);
		button12.Name = "button12";
		button12.Size = new System.Drawing.Size(72, 23);
		button12.TabIndex = 19;
		button12.Text = "跳转标签";
		button12.UseVisualStyleBackColor = true;
		button11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button11.Location = new System.Drawing.Point(583, 173);
		button11.Name = "button11";
		button11.Size = new System.Drawing.Size(72, 23);
		button11.TabIndex = 18;
		button11.Text = "定义标签";
		button11.UseVisualStyleBackColor = true;
		button6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button6.Location = new System.Drawing.Point(583, 272);
		button6.Name = "button6";
		button6.Size = new System.Drawing.Size(72, 23);
		button6.TabIndex = 21;
		button6.Text = "↓";
		button6.UseVisualStyleBackColor = true;
		button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button5.Location = new System.Drawing.Point(583, 239);
		button5.Name = "button5";
		button5.Size = new System.Drawing.Size(72, 23);
		button5.TabIndex = 20;
		button5.Text = "↑";
		button5.UseVisualStyleBackColor = true;
		button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button8.Location = new System.Drawing.Point(583, 477);
		button8.Name = "button8";
		button8.Size = new System.Drawing.Size(72, 23);
		button8.TabIndex = 24;
		button8.Text = "关  闭";
		button8.UseVisualStyleBackColor = true;
		button7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		button7.Location = new System.Drawing.Point(583, 448);
		button7.Name = "button7";
		button7.Size = new System.Drawing.Size(72, 23);
		button7.TabIndex = 23;
		button7.Text = "保  存";
		button7.UseVisualStyleBackColor = true;
		button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button4.Location = new System.Drawing.Point(583, 305);
		button4.Name = "button4";
		button4.Size = new System.Drawing.Size(72, 23);
		button4.TabIndex = 22;
		button4.Text = "删  除";
		button4.UseVisualStyleBackColor = true;
		button9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button9.Location = new System.Drawing.Point(583, 41);
		button9.Name = "button9";
		button9.Size = new System.Drawing.Size(72, 23);
		button9.TabIndex = 14;
		button9.Text = "变量赋值";
		button9.UseVisualStyleBackColor = true;
		button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button10.Location = new System.Drawing.Point(583, 140);
		button10.Name = "button10";
		button10.Size = new System.Drawing.Size(72, 23);
		button10.TabIndex = 17;
		button10.Text = "服务器逻辑";
		button10.UseVisualStyleBackColor = true;
		button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button3.Location = new System.Drawing.Point(583, 107);
		button3.Name = "button3";
		button3.Size = new System.Drawing.Size(72, 23);
		button3.TabIndex = 16;
		button3.Text = "方法调用";
		button3.UseVisualStyleBackColor = true;
		button13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		button13.Location = new System.Drawing.Point(583, 74);
		button13.Name = "button13";
		button13.Size = new System.Drawing.Size(72, 23);
		button13.TabIndex = 15;
		button13.Text = "属性赋值";
		button13.UseVisualStyleBackColor = true;
		listView3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { columnHeader5, columnHeader1, columnHeader6 });
		listView3.FullRowSelect = true;
		listView3.GridLines = true;
		listView3.HideSelection = false;
		listView3.Location = new System.Drawing.Point(3, 41);
		listView3.Name = "listView3";
		listView3.Size = new System.Drawing.Size(574, 469);
		listView3.TabIndex = 13;
		listView3.UseCompatibleStateImageBehavior = false;
		listView3.View = System.Windows.Forms.View.Details;
		columnHeader5.Text = "操作类型";
		columnHeader5.Width = 120;
		columnHeader1.Text = "条件";
		columnHeader1.Width = 97;
		columnHeader6.Text = "操作表达式";
		columnHeader6.Width = 424;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(979, 514);
		base.Controls.Add(splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "MakeMacro";
		Text = "宏定义";
		splitContainer1.Panel1.ResumeLayout(false);
		splitContainer1.Panel2.ResumeLayout(false);
		splitContainer1.Panel2.PerformLayout();
		splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
