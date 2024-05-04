using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Alert_Main;

public class RealTimeColumn : Form
{
	private MainControl mc;

	private IContainer components;

	private Button button2;

	private Button button1;

	private CheckBox ShangShangXian_8;

	private CheckBox MuBiao_12;

	private CheckBox ShangXian_7;

	private CheckBox ID_14;

	private CheckBox BaoJingLeiXing_4;

	private CheckBox WeiBaoJing_11;

	private CheckBox BianLiangLeiXing_6;

	private CheckBox BianHuaLv_13;

	private CheckBox BaoJingXinXi_3;

	private CheckBox XiaXiaXian_10;

	private CheckBox BaoJingShiJian_5;

	private CheckBox XiaXian_9;

	private CheckBox BaoJingZhuangTai_2;

	private CheckBox var_1;

	private CheckBox User_15;

	private CheckBox QueRenShiJian_16;

	private HelpProvider helpProvider;

	private CheckBox itemValue_17;

	public RealTimeColumn(MainControl mc)
	{
		InitializeComponent();
		this.mc = mc;
	}

	private void RealTimeColumn_Load(object sender, EventArgs e)
	{
		helpProvider.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		var_1.Checked = false;
		BaoJingZhuangTai_2.Checked = false;
		BaoJingXinXi_3.Checked = false;
		BaoJingLeiXing_4.Checked = false;
		BaoJingShiJian_5.Checked = false;
		BianLiangLeiXing_6.Checked = false;
		ShangXian_7.Checked = false;
		ShangShangXian_8.Checked = false;
		XiaXian_9.Checked = false;
		XiaXiaXian_10.Checked = false;
		WeiBaoJing_11.Checked = false;
		MuBiao_12.Checked = false;
		BianHuaLv_13.Checked = false;
		ID_14.Checked = false;
		User_15.Checked = false;
		QueRenShiJian_16.Checked = false;
		itemValue_17.Checked = false;
		if (mc.m_RealTime == null)
		{
			mc.m_RealTime = new List<string>();
		}
		using List<string>.Enumerator enumerator = mc.m_RealTime.GetEnumerator();
		while (enumerator.MoveNext())
		{
			switch (enumerator.Current)
			{
			case "变量":
				var_1.Checked = true;
				break;
			case "报警状态":
				BaoJingZhuangTai_2.Checked = true;
				break;
			case "报警信息":
				BaoJingXinXi_3.Checked = true;
				break;
			case "报警类型":
				BaoJingLeiXing_4.Checked = true;
				break;
			case "报警时间":
				BaoJingShiJian_5.Checked = true;
				break;
			case "变量类型":
				BianLiangLeiXing_6.Checked = true;
				break;
			case "变量报警上限":
				ShangXian_7.Checked = true;
				break;
			case "变量报警上上限":
				ShangShangXian_8.Checked = true;
				break;
			case "变量报警下限":
				XiaXian_9.Checked = true;
				break;
			case "变量报警下下限":
				XiaXiaXian_10.Checked = true;
				break;
			case "位报警":
				WeiBaoJing_11.Checked = true;
				break;
			case "目标报警":
				MuBiao_12.Checked = true;
				break;
			case "变化率报警":
				BianHuaLv_13.Checked = true;
				break;
			case "报警ID":
				ID_14.Checked = true;
				break;
			case "确认人":
				User_15.Checked = true;
				break;
			case "确认时间":
				QueRenShiJian_16.Checked = true;
				break;
			case "变量值":
				itemValue_17.Checked = true;
				break;
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (mc.m_RealTime == null)
		{
			mc.m_RealTime = new List<string>();
		}
		mc.m_RealTime.Clear();
		if (var_1.Checked)
		{
			mc.m_RealTime.Add("变量");
		}
		if (BaoJingZhuangTai_2.Checked)
		{
			mc.m_RealTime.Add("报警状态");
		}
		if (BaoJingXinXi_3.Checked)
		{
			mc.m_RealTime.Add("报警信息");
		}
		if (BaoJingLeiXing_4.Checked)
		{
			mc.m_RealTime.Add("报警类型");
		}
		if (BaoJingShiJian_5.Checked)
		{
			mc.m_RealTime.Add("报警时间");
		}
		if (BianLiangLeiXing_6.Checked)
		{
			mc.m_RealTime.Add("变量类型");
		}
		if (ShangXian_7.Checked)
		{
			mc.m_RealTime.Add("变量报警上限");
		}
		if (ShangShangXian_8.Checked)
		{
			mc.m_RealTime.Add("变量报警上上限");
		}
		if (XiaXian_9.Checked)
		{
			mc.m_RealTime.Add("变量报警下限");
		}
		if (XiaXiaXian_10.Checked)
		{
			mc.m_RealTime.Add("变量报警下下限");
		}
		if (WeiBaoJing_11.Checked)
		{
			mc.m_RealTime.Add("位报警");
		}
		if (MuBiao_12.Checked)
		{
			mc.m_RealTime.Add("目标报警");
		}
		if (BianHuaLv_13.Checked)
		{
			mc.m_RealTime.Add("变化率报警");
		}
		if (ID_14.Checked)
		{
			mc.m_RealTime.Add("报警ID");
		}
		if (User_15.Checked)
		{
			mc.m_RealTime.Add("确认人");
		}
		if (QueRenShiJian_16.Checked)
		{
			mc.m_RealTime.Add("确认时间");
		}
		if (itemValue_17.Checked)
		{
			mc.m_RealTime.Add("变量值");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Main.RealTimeColumn));
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.ShangShangXian_8 = new System.Windows.Forms.CheckBox();
		this.MuBiao_12 = new System.Windows.Forms.CheckBox();
		this.ShangXian_7 = new System.Windows.Forms.CheckBox();
		this.ID_14 = new System.Windows.Forms.CheckBox();
		this.BaoJingLeiXing_4 = new System.Windows.Forms.CheckBox();
		this.WeiBaoJing_11 = new System.Windows.Forms.CheckBox();
		this.BianLiangLeiXing_6 = new System.Windows.Forms.CheckBox();
		this.BianHuaLv_13 = new System.Windows.Forms.CheckBox();
		this.BaoJingXinXi_3 = new System.Windows.Forms.CheckBox();
		this.XiaXiaXian_10 = new System.Windows.Forms.CheckBox();
		this.BaoJingShiJian_5 = new System.Windows.Forms.CheckBox();
		this.XiaXian_9 = new System.Windows.Forms.CheckBox();
		this.BaoJingZhuangTai_2 = new System.Windows.Forms.CheckBox();
		this.var_1 = new System.Windows.Forms.CheckBox();
		this.User_15 = new System.Windows.Forms.CheckBox();
		this.QueRenShiJian_16 = new System.Windows.Forms.CheckBox();
		this.helpProvider = new System.Windows.Forms.HelpProvider();
		this.itemValue_17 = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.button2.Location = new System.Drawing.Point(153, 226);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 19;
		this.button2.Text = "取消";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button1.Location = new System.Drawing.Point(52, 226);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 18;
		this.button1.Text = "确认";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.ShangShangXian_8.AutoSize = true;
		this.ShangShangXian_8.Location = new System.Drawing.Point(150, 43);
		this.ShangShangXian_8.Name = "ShangShangXian_8";
		this.ShangShangXian_8.Size = new System.Drawing.Size(108, 16);
		this.ShangShangXian_8.TabIndex = 11;
		this.ShangShangXian_8.Text = "变量报警上上限";
		this.ShangShangXian_8.UseVisualStyleBackColor = true;
		this.MuBiao_12.AutoSize = true;
		this.MuBiao_12.Location = new System.Drawing.Point(150, 135);
		this.MuBiao_12.Name = "MuBiao_12";
		this.MuBiao_12.Size = new System.Drawing.Size(72, 16);
		this.MuBiao_12.TabIndex = 16;
		this.MuBiao_12.Text = "目标报警";
		this.MuBiao_12.UseVisualStyleBackColor = true;
		this.ShangXian_7.AutoSize = true;
		this.ShangXian_7.Location = new System.Drawing.Point(150, 21);
		this.ShangXian_7.Name = "ShangXian_7";
		this.ShangXian_7.Size = new System.Drawing.Size(96, 16);
		this.ShangXian_7.TabIndex = 10;
		this.ShangXian_7.Text = "变量报警上限";
		this.ShangXian_7.UseVisualStyleBackColor = true;
		this.ID_14.AutoSize = true;
		this.ID_14.Location = new System.Drawing.Point(23, 158);
		this.ID_14.Name = "ID_14";
		this.ID_14.Size = new System.Drawing.Size(60, 16);
		this.ID_14.TabIndex = 14;
		this.ID_14.Text = "报警ID";
		this.ID_14.UseVisualStyleBackColor = true;
		this.BaoJingLeiXing_4.AutoSize = true;
		this.BaoJingLeiXing_4.Location = new System.Drawing.Point(23, 90);
		this.BaoJingLeiXing_4.Name = "BaoJingLeiXing_4";
		this.BaoJingLeiXing_4.Size = new System.Drawing.Size(72, 16);
		this.BaoJingLeiXing_4.TabIndex = 15;
		this.BaoJingLeiXing_4.Text = "报警类型";
		this.BaoJingLeiXing_4.UseVisualStyleBackColor = true;
		this.WeiBaoJing_11.AutoSize = true;
		this.WeiBaoJing_11.Location = new System.Drawing.Point(150, 112);
		this.WeiBaoJing_11.Name = "WeiBaoJing_11";
		this.WeiBaoJing_11.Size = new System.Drawing.Size(60, 16);
		this.WeiBaoJing_11.TabIndex = 9;
		this.WeiBaoJing_11.Text = "位报警";
		this.WeiBaoJing_11.UseVisualStyleBackColor = true;
		this.BianLiangLeiXing_6.AutoSize = true;
		this.BianLiangLeiXing_6.Location = new System.Drawing.Point(23, 136);
		this.BianLiangLeiXing_6.Name = "BianLiangLeiXing_6";
		this.BianLiangLeiXing_6.Size = new System.Drawing.Size(72, 16);
		this.BianLiangLeiXing_6.TabIndex = 17;
		this.BianLiangLeiXing_6.Text = "变量类型";
		this.BianLiangLeiXing_6.UseVisualStyleBackColor = true;
		this.BianHuaLv_13.AutoSize = true;
		this.BianHuaLv_13.Location = new System.Drawing.Point(150, 158);
		this.BianHuaLv_13.Name = "BianHuaLv_13";
		this.BianHuaLv_13.Size = new System.Drawing.Size(84, 16);
		this.BianHuaLv_13.TabIndex = 4;
		this.BianHuaLv_13.Text = "变化率报警";
		this.BianHuaLv_13.UseVisualStyleBackColor = true;
		this.BaoJingXinXi_3.AutoSize = true;
		this.BaoJingXinXi_3.Location = new System.Drawing.Point(23, 67);
		this.BaoJingXinXi_3.Name = "BaoJingXinXi_3";
		this.BaoJingXinXi_3.Size = new System.Drawing.Size(72, 16);
		this.BaoJingXinXi_3.TabIndex = 6;
		this.BaoJingXinXi_3.Text = "报警信息";
		this.BaoJingXinXi_3.UseVisualStyleBackColor = true;
		this.XiaXiaXian_10.AutoSize = true;
		this.XiaXiaXian_10.Location = new System.Drawing.Point(150, 89);
		this.XiaXiaXian_10.Name = "XiaXiaXian_10";
		this.XiaXiaXian_10.Size = new System.Drawing.Size(108, 16);
		this.XiaXiaXian_10.TabIndex = 12;
		this.XiaXiaXian_10.Text = "变量报警下下限";
		this.XiaXiaXian_10.UseVisualStyleBackColor = true;
		this.BaoJingShiJian_5.AutoSize = true;
		this.BaoJingShiJian_5.Location = new System.Drawing.Point(23, 113);
		this.BaoJingShiJian_5.Name = "BaoJingShiJian_5";
		this.BaoJingShiJian_5.Size = new System.Drawing.Size(72, 16);
		this.BaoJingShiJian_5.TabIndex = 8;
		this.BaoJingShiJian_5.Text = "报警时间";
		this.BaoJingShiJian_5.UseVisualStyleBackColor = true;
		this.XiaXian_9.AutoSize = true;
		this.XiaXian_9.Location = new System.Drawing.Point(150, 66);
		this.XiaXian_9.Name = "XiaXian_9";
		this.XiaXian_9.Size = new System.Drawing.Size(96, 16);
		this.XiaXian_9.TabIndex = 7;
		this.XiaXian_9.Text = "变量报警下限";
		this.XiaXian_9.UseVisualStyleBackColor = true;
		this.BaoJingZhuangTai_2.AutoSize = true;
		this.BaoJingZhuangTai_2.Location = new System.Drawing.Point(23, 44);
		this.BaoJingZhuangTai_2.Name = "BaoJingZhuangTai_2";
		this.BaoJingZhuangTai_2.Size = new System.Drawing.Size(72, 16);
		this.BaoJingZhuangTai_2.TabIndex = 13;
		this.BaoJingZhuangTai_2.Text = "报警状态";
		this.BaoJingZhuangTai_2.UseVisualStyleBackColor = true;
		this.var_1.AutoSize = true;
		this.var_1.Location = new System.Drawing.Point(23, 21);
		this.var_1.Name = "var_1";
		this.var_1.Size = new System.Drawing.Size(48, 16);
		this.var_1.TabIndex = 5;
		this.var_1.Text = "变量";
		this.var_1.UseVisualStyleBackColor = true;
		this.User_15.AutoSize = true;
		this.User_15.Location = new System.Drawing.Point(23, 181);
		this.User_15.Name = "User_15";
		this.User_15.Size = new System.Drawing.Size(60, 16);
		this.User_15.TabIndex = 14;
		this.User_15.Text = "确认人";
		this.User_15.UseVisualStyleBackColor = true;
		this.QueRenShiJian_16.AutoSize = true;
		this.QueRenShiJian_16.Location = new System.Drawing.Point(150, 181);
		this.QueRenShiJian_16.Name = "QueRenShiJian_16";
		this.QueRenShiJian_16.Size = new System.Drawing.Size(72, 16);
		this.QueRenShiJian_16.TabIndex = 14;
		this.QueRenShiJian_16.Text = "确认时间";
		this.QueRenShiJian_16.UseVisualStyleBackColor = true;
		this.itemValue_17.AutoSize = true;
		this.itemValue_17.Location = new System.Drawing.Point(23, 203);
		this.itemValue_17.Name = "itemValue_17";
		this.itemValue_17.Size = new System.Drawing.Size(60, 16);
		this.itemValue_17.TabIndex = 14;
		this.itemValue_17.Text = "变量值";
		this.itemValue_17.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(281, 262);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.ShangShangXian_8);
		base.Controls.Add(this.MuBiao_12);
		base.Controls.Add(this.ShangXian_7);
		base.Controls.Add(this.QueRenShiJian_16);
		base.Controls.Add(this.itemValue_17);
		base.Controls.Add(this.User_15);
		base.Controls.Add(this.ID_14);
		base.Controls.Add(this.BaoJingLeiXing_4);
		base.Controls.Add(this.WeiBaoJing_11);
		base.Controls.Add(this.BianLiangLeiXing_6);
		base.Controls.Add(this.BianHuaLv_13);
		base.Controls.Add(this.BaoJingXinXi_3);
		base.Controls.Add(this.XiaXiaXian_10);
		base.Controls.Add(this.BaoJingShiJian_5);
		base.Controls.Add(this.XiaXian_9);
		base.Controls.Add(this.BaoJingZhuangTai_2);
		base.Controls.Add(this.var_1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		this.helpProvider.SetHelpKeyword(this, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "RealTimeColumn";
		this.helpProvider.SetShowHelp(this, true);
		this.Text = "实时报警显示列设置";
		base.Load += new System.EventHandler(RealTimeColumn_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
