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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIEditEnvironment.ServerLogic.MakeMacro));
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.listView1 = new System.Windows.Forms.ListView();
		this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button12 = new System.Windows.Forms.Button();
		this.button11 = new System.Windows.Forms.Button();
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button8 = new System.Windows.Forms.Button();
		this.button7 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.button9 = new System.Windows.Forms.Button();
		this.button10 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.button13 = new System.Windows.Forms.Button();
		this.listView3 = new System.Windows.Forms.ListView();
		this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Location = new System.Drawing.Point(16, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "新建宏";
		this.button1.UseVisualStyleBackColor = true;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.button2.Location = new System.Drawing.Point(106, 12);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "移除宏";
		this.button2.UseVisualStyleBackColor = true;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.listView1);
		this.splitContainer1.Panel1.Controls.Add(this.button2);
		this.splitContainer1.Panel1.Controls.Add(this.button1);
		this.splitContainer1.Panel2.Controls.Add(this.label2);
		this.splitContainer1.Panel2.Controls.Add(this.label1);
		this.splitContainer1.Panel2.Controls.Add(this.button12);
		this.splitContainer1.Panel2.Controls.Add(this.button11);
		this.splitContainer1.Panel2.Controls.Add(this.button6);
		this.splitContainer1.Panel2.Controls.Add(this.button5);
		this.splitContainer1.Panel2.Controls.Add(this.button8);
		this.splitContainer1.Panel2.Controls.Add(this.button7);
		this.splitContainer1.Panel2.Controls.Add(this.button4);
		this.splitContainer1.Panel2.Controls.Add(this.button9);
		this.splitContainer1.Panel2.Controls.Add(this.button10);
		this.splitContainer1.Panel2.Controls.Add(this.button3);
		this.splitContainer1.Panel2.Controls.Add(this.button13);
		this.splitContainer1.Panel2.Controls.Add(this.listView3);
		this.splitContainer1.Size = new System.Drawing.Size(979, 514);
		this.splitContainer1.SplitterDistance = 308;
		this.splitContainer1.TabIndex = 3;
		this.listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { this.columnHeader2, this.columnHeader3 });
		this.listView1.FullRowSelect = true;
		this.listView1.GridLines = true;
		this.listView1.HideSelection = false;
		this.listView1.Location = new System.Drawing.Point(3, 41);
		this.listView1.MultiSelect = false;
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(302, 470);
		this.listView1.TabIndex = 3;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.Details;
		this.columnHeader2.Text = "宏名称";
		this.columnHeader2.Width = 102;
		this.columnHeader3.Text = "备注";
		this.columnHeader3.Width = 195;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(104, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 25;
		this.label2.Text = "无";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 25;
		this.label1.Text = "当前编辑宏：";
		this.button12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button12.Location = new System.Drawing.Point(583, 206);
		this.button12.Name = "button12";
		this.button12.Size = new System.Drawing.Size(72, 23);
		this.button12.TabIndex = 19;
		this.button12.Text = "跳转标签";
		this.button12.UseVisualStyleBackColor = true;
		this.button11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button11.Location = new System.Drawing.Point(583, 173);
		this.button11.Name = "button11";
		this.button11.Size = new System.Drawing.Size(72, 23);
		this.button11.TabIndex = 18;
		this.button11.Text = "定义标签";
		this.button11.UseVisualStyleBackColor = true;
		this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button6.Location = new System.Drawing.Point(583, 272);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(72, 23);
		this.button6.TabIndex = 21;
		this.button6.Text = "↓";
		this.button6.UseVisualStyleBackColor = true;
		this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button5.Location = new System.Drawing.Point(583, 239);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(72, 23);
		this.button5.TabIndex = 20;
		this.button5.Text = "↑";
		this.button5.UseVisualStyleBackColor = true;
		this.button8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button8.Location = new System.Drawing.Point(583, 477);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(72, 23);
		this.button8.TabIndex = 24;
		this.button8.Text = "关  闭";
		this.button8.UseVisualStyleBackColor = true;
		this.button7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button7.Location = new System.Drawing.Point(583, 448);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(72, 23);
		this.button7.TabIndex = 23;
		this.button7.Text = "保  存";
		this.button7.UseVisualStyleBackColor = true;
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button4.Location = new System.Drawing.Point(583, 305);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(72, 23);
		this.button4.TabIndex = 22;
		this.button4.Text = "删  除";
		this.button4.UseVisualStyleBackColor = true;
		this.button9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button9.Location = new System.Drawing.Point(583, 41);
		this.button9.Name = "button9";
		this.button9.Size = new System.Drawing.Size(72, 23);
		this.button9.TabIndex = 14;
		this.button9.Text = "变量赋值";
		this.button9.UseVisualStyleBackColor = true;
		this.button10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button10.Location = new System.Drawing.Point(583, 140);
		this.button10.Name = "button10";
		this.button10.Size = new System.Drawing.Size(72, 23);
		this.button10.TabIndex = 17;
		this.button10.Text = "服务器逻辑";
		this.button10.UseVisualStyleBackColor = true;
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button3.Location = new System.Drawing.Point(583, 107);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(72, 23);
		this.button3.TabIndex = 16;
		this.button3.Text = "方法调用";
		this.button3.UseVisualStyleBackColor = true;
		this.button13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button13.Location = new System.Drawing.Point(583, 74);
		this.button13.Name = "button13";
		this.button13.Size = new System.Drawing.Size(72, 23);
		this.button13.TabIndex = 15;
		this.button13.Text = "属性赋值";
		this.button13.UseVisualStyleBackColor = true;
		this.listView3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.columnHeader5, this.columnHeader1, this.columnHeader6 });
		this.listView3.FullRowSelect = true;
		this.listView3.GridLines = true;
		this.listView3.HideSelection = false;
		this.listView3.Location = new System.Drawing.Point(3, 41);
		this.listView3.Name = "listView3";
		this.listView3.Size = new System.Drawing.Size(574, 469);
		this.listView3.TabIndex = 13;
		this.listView3.UseCompatibleStateImageBehavior = false;
		this.listView3.View = System.Windows.Forms.View.Details;
		this.columnHeader5.Text = "操作类型";
		this.columnHeader5.Width = 120;
		this.columnHeader1.Text = "条件";
		this.columnHeader1.Width = 97;
		this.columnHeader6.Text = "操作表达式";
		this.columnHeader6.Width = 424;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(979, 514);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "MakeMacro";
		this.Text = "宏定义";
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
