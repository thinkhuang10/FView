using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Alert_Main;

public class HistoryColumn : Form
{
	private MainControl mc;

	private IContainer components;

	private CheckBox alarmID_1;

	private CheckBox var_2;

	private CheckBox alarmTime_3;

	private CheckBox AlarmMsg_4;

	private CheckBox NoteTime_5;

	private CheckBox AlarmType_6;

	private CheckBox User_7;

	private CheckBox ShangXian_8;

	private CheckBox ShangShangXian_9;

	private CheckBox XiaXian_10;

	private CheckBox Mubiao_13;

	private CheckBox XiaXiaXian_11;

	private CheckBox BianHuaLv_14;

	private CheckBox Byte_12;

	private Button button1;

	private Button button2;

	private HelpProvider helpProvider;

	private CheckBox itemValue_15;

	public HistoryColumn(MainControl mc)
	{
		InitializeComponent();
		this.mc = mc;
	}

	private void HistoryColumn_Load(object sender, EventArgs e)
	{
		helpProvider.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		if (mc.m_HistoryColumns == null)
		{
			mc.m_HistoryColumns = new List<string>();
		}
		using List<string>.Enumerator enumerator = mc.m_HistoryColumns.GetEnumerator();
		while (enumerator.MoveNext())
		{
			switch (enumerator.Current)
			{
			case "报警ID":
				alarmID_1.Checked = true;
				break;
			case "变量":
				var_2.Checked = true;
				break;
			case "报警时间":
				alarmTime_3.Checked = true;
				break;
			case "报警信息":
				AlarmMsg_4.Checked = true;
				break;
			case "确认时间":
				NoteTime_5.Checked = true;
				break;
			case "报警类型":
				AlarmType_6.Checked = true;
				break;
			case "确认人":
				User_7.Checked = true;
				break;
			case "上限报警":
				ShangXian_8.Checked = true;
				break;
			case "上上限报警":
				ShangShangXian_9.Checked = true;
				break;
			case "下限报警":
				XiaXian_10.Checked = true;
				break;
			case "下下限报警":
				XiaXiaXian_11.Checked = true;
				break;
			case "位报警":
				Byte_12.Checked = true;
				break;
			case "目标报警":
				Mubiao_13.Checked = true;
				break;
			case "变化率报警":
				BianHuaLv_14.Checked = true;
				break;
			case "变量值":
				itemValue_15.Checked = true;
				break;
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		mc.m_HistoryColumns.Clear();
		if (alarmID_1.Checked)
		{
			mc.m_HistoryColumns.Add("报警ID");
		}
		if (var_2.Checked)
		{
			mc.m_HistoryColumns.Add("变量");
		}
		if (alarmTime_3.Checked)
		{
			mc.m_HistoryColumns.Add("报警时间");
		}
		if (AlarmMsg_4.Checked)
		{
			mc.m_HistoryColumns.Add("报警信息");
		}
		if (NoteTime_5.Checked)
		{
			mc.m_HistoryColumns.Add("确认时间");
		}
		if (AlarmType_6.Checked)
		{
			mc.m_HistoryColumns.Add("报警类型");
		}
		if (User_7.Checked)
		{
			mc.m_HistoryColumns.Add("确认人");
		}
		if (ShangXian_8.Checked)
		{
			mc.m_HistoryColumns.Add("上限报警");
		}
		if (ShangShangXian_9.Checked)
		{
			mc.m_HistoryColumns.Add("上上限报警");
		}
		if (XiaXian_10.Checked)
		{
			mc.m_HistoryColumns.Add("下限报警");
		}
		if (XiaXiaXian_11.Checked)
		{
			mc.m_HistoryColumns.Add("下下限报警");
		}
		if (Byte_12.Checked)
		{
			mc.m_HistoryColumns.Add("位报警");
		}
		if (Mubiao_13.Checked)
		{
			mc.m_HistoryColumns.Add("目标报警");
		}
		if (BianHuaLv_14.Checked)
		{
			mc.m_HistoryColumns.Add("变化率报警");
		}
		if (itemValue_15.Checked)
		{
			mc.m_HistoryColumns.Add("变量值");
		}
		base.DialogResult = DialogResult.OK;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Main.HistoryColumn));
		this.alarmID_1 = new System.Windows.Forms.CheckBox();
		this.var_2 = new System.Windows.Forms.CheckBox();
		this.alarmTime_3 = new System.Windows.Forms.CheckBox();
		this.AlarmMsg_4 = new System.Windows.Forms.CheckBox();
		this.NoteTime_5 = new System.Windows.Forms.CheckBox();
		this.AlarmType_6 = new System.Windows.Forms.CheckBox();
		this.User_7 = new System.Windows.Forms.CheckBox();
		this.ShangXian_8 = new System.Windows.Forms.CheckBox();
		this.ShangShangXian_9 = new System.Windows.Forms.CheckBox();
		this.XiaXian_10 = new System.Windows.Forms.CheckBox();
		this.Mubiao_13 = new System.Windows.Forms.CheckBox();
		this.XiaXiaXian_11 = new System.Windows.Forms.CheckBox();
		this.BianHuaLv_14 = new System.Windows.Forms.CheckBox();
		this.Byte_12 = new System.Windows.Forms.CheckBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.helpProvider = new System.Windows.Forms.HelpProvider();
		this.itemValue_15 = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.alarmID_1.AutoSize = true;
		this.alarmID_1.Location = new System.Drawing.Point(29, 12);
		this.alarmID_1.Name = "alarmID_1";
		this.alarmID_1.Size = new System.Drawing.Size(60, 16);
		this.alarmID_1.TabIndex = 0;
		this.alarmID_1.Text = "报警ID";
		this.alarmID_1.UseVisualStyleBackColor = true;
		this.var_2.AutoSize = true;
		this.var_2.Location = new System.Drawing.Point(29, 35);
		this.var_2.Name = "var_2";
		this.var_2.Size = new System.Drawing.Size(48, 16);
		this.var_2.TabIndex = 2;
		this.var_2.Text = "变量";
		this.var_2.UseVisualStyleBackColor = true;
		this.alarmTime_3.AutoSize = true;
		this.alarmTime_3.Location = new System.Drawing.Point(29, 58);
		this.alarmTime_3.Name = "alarmTime_3";
		this.alarmTime_3.Size = new System.Drawing.Size(72, 16);
		this.alarmTime_3.TabIndex = 4;
		this.alarmTime_3.Text = "报警时间";
		this.alarmTime_3.UseVisualStyleBackColor = true;
		this.AlarmMsg_4.AutoSize = true;
		this.AlarmMsg_4.Location = new System.Drawing.Point(29, 81);
		this.AlarmMsg_4.Name = "AlarmMsg_4";
		this.AlarmMsg_4.Size = new System.Drawing.Size(72, 16);
		this.AlarmMsg_4.TabIndex = 6;
		this.AlarmMsg_4.Text = "报警信息";
		this.AlarmMsg_4.UseVisualStyleBackColor = true;
		this.NoteTime_5.AutoSize = true;
		this.NoteTime_5.Location = new System.Drawing.Point(29, 104);
		this.NoteTime_5.Name = "NoteTime_5";
		this.NoteTime_5.Size = new System.Drawing.Size(72, 16);
		this.NoteTime_5.TabIndex = 8;
		this.NoteTime_5.Text = "确认时间";
		this.NoteTime_5.UseVisualStyleBackColor = true;
		this.AlarmType_6.AutoSize = true;
		this.AlarmType_6.Location = new System.Drawing.Point(29, 127);
		this.AlarmType_6.Name = "AlarmType_6";
		this.AlarmType_6.Size = new System.Drawing.Size(72, 16);
		this.AlarmType_6.TabIndex = 10;
		this.AlarmType_6.Text = "报警类型";
		this.AlarmType_6.UseVisualStyleBackColor = true;
		this.User_7.AutoSize = true;
		this.User_7.Location = new System.Drawing.Point(29, 150);
		this.User_7.Name = "User_7";
		this.User_7.Size = new System.Drawing.Size(60, 16);
		this.User_7.TabIndex = 12;
		this.User_7.Text = "确认人";
		this.User_7.UseVisualStyleBackColor = true;
		this.ShangXian_8.AutoSize = true;
		this.ShangXian_8.Location = new System.Drawing.Point(156, 12);
		this.ShangXian_8.Name = "ShangXian_8";
		this.ShangXian_8.Size = new System.Drawing.Size(72, 16);
		this.ShangXian_8.TabIndex = 1;
		this.ShangXian_8.Text = "上限报警";
		this.ShangXian_8.UseVisualStyleBackColor = true;
		this.ShangShangXian_9.AutoSize = true;
		this.ShangShangXian_9.Location = new System.Drawing.Point(156, 35);
		this.ShangShangXian_9.Name = "ShangShangXian_9";
		this.ShangShangXian_9.Size = new System.Drawing.Size(84, 16);
		this.ShangShangXian_9.TabIndex = 3;
		this.ShangShangXian_9.Text = "上上限报警";
		this.ShangShangXian_9.UseVisualStyleBackColor = true;
		this.XiaXian_10.AutoSize = true;
		this.XiaXian_10.Location = new System.Drawing.Point(156, 58);
		this.XiaXian_10.Name = "XiaXian_10";
		this.XiaXian_10.Size = new System.Drawing.Size(72, 16);
		this.XiaXian_10.TabIndex = 5;
		this.XiaXian_10.Text = "下限报警";
		this.XiaXian_10.UseVisualStyleBackColor = true;
		this.Mubiao_13.AutoSize = true;
		this.Mubiao_13.Location = new System.Drawing.Point(156, 127);
		this.Mubiao_13.Name = "Mubiao_13";
		this.Mubiao_13.Size = new System.Drawing.Size(72, 16);
		this.Mubiao_13.TabIndex = 11;
		this.Mubiao_13.Text = "目标报警";
		this.Mubiao_13.UseVisualStyleBackColor = true;
		this.XiaXiaXian_11.AutoSize = true;
		this.XiaXiaXian_11.Location = new System.Drawing.Point(156, 81);
		this.XiaXiaXian_11.Name = "XiaXiaXian_11";
		this.XiaXiaXian_11.Size = new System.Drawing.Size(84, 16);
		this.XiaXiaXian_11.TabIndex = 7;
		this.XiaXiaXian_11.Text = "下下限报警";
		this.XiaXiaXian_11.UseVisualStyleBackColor = true;
		this.BianHuaLv_14.AutoSize = true;
		this.BianHuaLv_14.Location = new System.Drawing.Point(156, 150);
		this.BianHuaLv_14.Name = "BianHuaLv_14";
		this.BianHuaLv_14.Size = new System.Drawing.Size(84, 16);
		this.BianHuaLv_14.TabIndex = 13;
		this.BianHuaLv_14.Text = "变化率报警";
		this.BianHuaLv_14.UseVisualStyleBackColor = true;
		this.Byte_12.AutoSize = true;
		this.Byte_12.Location = new System.Drawing.Point(156, 104);
		this.Byte_12.Name = "Byte_12";
		this.Byte_12.Size = new System.Drawing.Size(60, 16);
		this.Byte_12.TabIndex = 9;
		this.Byte_12.Text = "位报警";
		this.Byte_12.UseVisualStyleBackColor = true;
		this.button1.Location = new System.Drawing.Point(46, 203);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 14;
		this.button1.Text = "确认";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(147, 203);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 15;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.itemValue_15.AutoSize = true;
		this.itemValue_15.Location = new System.Drawing.Point(29, 172);
		this.itemValue_15.Name = "itemValue_15";
		this.itemValue_15.Size = new System.Drawing.Size(60, 16);
		this.itemValue_15.TabIndex = 16;
		this.itemValue_15.Text = "变量值";
		this.itemValue_15.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(269, 239);
		base.Controls.Add(this.itemValue_15);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.ShangXian_8);
		base.Controls.Add(this.Byte_12);
		base.Controls.Add(this.User_7);
		base.Controls.Add(this.BianHuaLv_14);
		base.Controls.Add(this.AlarmMsg_4);
		base.Controls.Add(this.XiaXiaXian_11);
		base.Controls.Add(this.AlarmType_6);
		base.Controls.Add(this.Mubiao_13);
		base.Controls.Add(this.alarmTime_3);
		base.Controls.Add(this.XiaXian_10);
		base.Controls.Add(this.NoteTime_5);
		base.Controls.Add(this.ShangShangXian_9);
		base.Controls.Add(this.var_2);
		base.Controls.Add(this.alarmID_1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider.SetHelpKeyword(this, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "HistoryColumn";
		this.helpProvider.SetShowHelp(this, true);
		this.Text = "历史报警显示列设置";
		base.Load += new System.EventHandler(HistoryColumn_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
