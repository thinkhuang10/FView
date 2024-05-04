using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Alert_Main;

namespace Alert_Setting;

public class ControlSettings : Form
{
	private Color m_VS_TableHeadBackColor = Color.Silver;

	private Color m_VS_TableHeadTextColor = Color.Black;

	private Color m_VS_WindowBackColor = Color.White;

	private List<string> m_VS_RealTimeSelectedColumn = new List<string>();

	private int m_VS_SortType;

	private string m_VS_FirstColumnSort = "报警时间";

	private string m_VS_SecondColumnSort = "确认时间";

	private bool m_VS_IsPlaySound;

	private bool m_VS_IsMessage;

	private int m_vs_PlayTimes;

	private bool m_VS_IsStopSound;

	private bool m_VS_IsShowOperationBar;

	private bool m_VS_IsShowMessageBar;

	private string m_VS_AlertMessage;

	private Color m_VS_AlertTextColor;

	private Color m_VS_AlertBackColor;

	private Color m_VS_NoterizeTextColor;

	private Color m_VS_NoterizeBackColor;

	private Color m_VS_RecoverTextColor;

	private Color m_VS_RecoverBackColor;

	private List<string> m_VS_VarCanSelectedList = new List<string>();

	private List<string> m_VS_VarHasSelectedList = new List<string>();

	private Dictionary<string, int> m_VS_VarDictionary = new Dictionary<string, int>();

	private int m_VS_ShowType;

	private bool m_VS_UseCustomAlarmWav;

	private MainControl mc;

	private ComboBox cmb_Temp = new ComboBox();

	private Button bt_Temp = new Button();

	private Button bt_Temp1 = new Button();

	private Button bt_Temp2 = new Button();

	private Button bt_Temp3 = new Button();

	private Button bt_Temp4 = new Button();

	private Button bt_Temp5 = new Button();

	private Button bt_AlertMessage = new Button();

	private DataTable m_VS_AlertVarDt = new DataTable();

	private int temp_flag = 1;

	private int m_SetALLColor;

	private bool TestSound;

	private IContainer components;

	private Button Btn_down;

	private Button Btn_up;

	private Button Btn_tolt;

	private Button Btn_tort;

	private ListBox lbx_selected;

	private ListBox lbx_all;

	private Label label4;

	private Label label2;

	private GroupBox groupBox3;

	private Label label7;

	private RadioButton rd2;

	private RadioButton rd1;

	private Label label6;

	private ComboBox cbx_firstsort;

	private Button Btn_ok;

	private Button Btn_cancel;

	private ColorDialog colorDialog1;

	private GroupBox groupBox4;

	private CheckBox ckb_outputbar;

	private CheckBox ckb_opbar;

	private CheckBox ckb_stop;

	private CheckBox ckb_issound;

	private Button btn_alltolt;

	private Button btn_alltort;

	private TextBox textBox1;

	private Label label20;

	private Label label19;

	private DataGridView dataGridView1;

	private Label label1;

	private Label Lbl_bkclr;

	private Label label3;

	private Label Lbl_txtclr;

	private Label label5;

	private Label Lbl_wdclr;

	private GroupBox groupBox1;

	private GroupBox groupBox5;

	private GroupBox groupBox6;

	private Label label9;

	private CheckBox checkBox_AlertNor;

	private CheckBox checkBox_AlertALL;

	private Label label15;

	private Label label12;

	private Label label14;

	private Label label11;

	private Label label_ReCoverText;

	private Label label_NoteText;

	private Label label_AlertText;

	private Label label13;

	private Label label10;

	private TextBox textBox_AlertMessage;

	private Button button_AlertALLOK;

	private Label label_RecoverBack;

	private Label label_NoteBack;

	private Label label_AlertBack;

	private CheckBox checkBox_AlertMessage;

	private HelpProvider helpProvider;

	private Button button1;

	private Button button3;

	private Button button2;

	private RadioButton radio_history;

	private RadioButton radio_realtime;

	private GroupBox Group7;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Button button4;

	private Label label8;

	private Button button5;

	private Label label17;

	private Button button6;

	private ListBox listBox1;

	private Button button7;

	private ListBox listBox2;

	private Button button8;

	private Button button9;

	private Label label18;

	private ToolTip toolTip1;

	private RadioButton radioCustomWav;

	private RadioButton radioDefaultWav;

	private GroupBox groupBox2;

	public bool M_VS_IsMessage
	{
		get
		{
			return m_VS_IsMessage;
		}
		set
		{
			m_VS_IsMessage = value;
		}
	}

	public int M_VS_ShowType
	{
		get
		{
			return m_VS_ShowType;
		}
		set
		{
			m_VS_ShowType = value;
		}
	}

	public int M_VS_PlayTimes
	{
		get
		{
			return m_vs_PlayTimes;
		}
		set
		{
			m_vs_PlayTimes = value;
		}
	}

	public Color M_VS_TableHeadBackColor
	{
		get
		{
			return m_VS_TableHeadBackColor;
		}
		set
		{
			m_VS_TableHeadBackColor = value;
		}
	}

	public Color M_VS_TableHeadTextColor
	{
		get
		{
			return m_VS_TableHeadTextColor;
		}
		set
		{
			m_VS_TableHeadTextColor = value;
		}
	}

	public Color M_VS_WindowBackColor
	{
		get
		{
			return m_VS_WindowBackColor;
		}
		set
		{
			m_VS_WindowBackColor = value;
		}
	}

	public List<string> M_VS_RealTimeSelectedColumn
	{
		get
		{
			return m_VS_RealTimeSelectedColumn;
		}
		set
		{
			m_VS_RealTimeSelectedColumn = value;
		}
	}

	public int M_VS_SortType
	{
		get
		{
			return m_VS_SortType;
		}
		set
		{
			m_VS_SortType = value;
		}
	}

	public string M_VS_FirstColumnSort
	{
		get
		{
			return m_VS_FirstColumnSort;
		}
		set
		{
			m_VS_FirstColumnSort = value;
		}
	}

	public string M_VS_SecondColumnSort
	{
		get
		{
			return m_VS_SecondColumnSort;
		}
		set
		{
			m_VS_SecondColumnSort = value;
		}
	}

	public bool M_VS_IsPlaySound
	{
		get
		{
			return m_VS_IsPlaySound;
		}
		set
		{
			m_VS_IsPlaySound = value;
		}
	}

	public bool M_VS_IsStopSound
	{
		get
		{
			return m_VS_IsStopSound;
		}
		set
		{
			m_VS_IsStopSound = value;
		}
	}

	public bool M_VS_IsShowOperationBar
	{
		get
		{
			return m_VS_IsShowOperationBar;
		}
		set
		{
			m_VS_IsShowOperationBar = value;
		}
	}

	public bool M_VS_IsShowMessageBar
	{
		get
		{
			return m_VS_IsShowMessageBar;
		}
		set
		{
			m_VS_IsShowMessageBar = value;
		}
	}

	public string M_VS_AlertMessage
	{
		get
		{
			return m_VS_AlertMessage;
		}
		set
		{
			m_VS_AlertMessage = value;
		}
	}

	public Color M_VS_AlertTextColor
	{
		get
		{
			return m_VS_AlertTextColor;
		}
		set
		{
			m_VS_AlertTextColor = value;
		}
	}

	public Color M_VS_AlertBackColor
	{
		get
		{
			return m_VS_AlertBackColor;
		}
		set
		{
			m_VS_AlertBackColor = value;
		}
	}

	public Color M_VS_NoterizeTextColor
	{
		get
		{
			return m_VS_NoterizeTextColor;
		}
		set
		{
			m_VS_NoterizeTextColor = value;
		}
	}

	public Color M_VS_NoterizeBackColor
	{
		get
		{
			return m_VS_NoterizeBackColor;
		}
		set
		{
			m_VS_NoterizeBackColor = value;
		}
	}

	public Color M_VS_RecoverTextColor
	{
		get
		{
			return m_VS_RecoverTextColor;
		}
		set
		{
			m_VS_RecoverTextColor = value;
		}
	}

	public Color M_VS_RecoverBackColor
	{
		get
		{
			return m_VS_RecoverBackColor;
		}
		set
		{
			m_VS_RecoverBackColor = value;
		}
	}

	public List<string> M_VS_VarCanSelectedList
	{
		get
		{
			return m_VS_VarCanSelectedList;
		}
		set
		{
			m_VS_VarCanSelectedList = value;
		}
	}

	public List<string> M_VS_VarHasSelectedList
	{
		get
		{
			return m_VS_VarHasSelectedList;
		}
		set
		{
			m_VS_VarHasSelectedList = value;
		}
	}

	public Dictionary<string, int> M_VS_VarDictionary
	{
		get
		{
			return m_VS_VarDictionary;
		}
		set
		{
			m_VS_VarDictionary = value;
		}
	}

	public bool M_VS_UseCustomAlarmWav
	{
		get
		{
			return m_VS_UseCustomAlarmWav;
		}
		set
		{
			m_VS_UseCustomAlarmWav = value;
		}
	}

	public ControlSettings(MainControl mc)
	{
		InitializeComponent();
		this.mc = mc;
	}

	private void Btn_Ok_Click(object sender, EventArgs e)
	{
		int.TryParse(textBox1.Text, out m_vs_PlayTimes);
		m_VS_TableHeadBackColor = Lbl_bkclr.BackColor;
		m_VS_TableHeadTextColor = Lbl_txtclr.BackColor;
		m_VS_WindowBackColor = Lbl_wdclr.BackColor;
		m_VS_RealTimeSelectedColumn.Clear();
		foreach (object item in lbx_selected.Items)
		{
			m_VS_RealTimeSelectedColumn.Add(item.ToString());
		}
		mc.m_HistoryColumns.Clear();
		foreach (object item2 in listBox2.Items)
		{
			mc.m_HistoryColumns.Add(item2.ToString());
		}
		if (rd1.Checked)
		{
			m_VS_SortType = 0;
		}
		else
		{
			m_VS_SortType = 1;
		}
		if (radio_realtime.Checked)
		{
			m_VS_ShowType = 0;
		}
		else if (radio_history.Checked)
		{
			m_VS_ShowType = 1;
		}
		m_VS_FirstColumnSort = cbx_firstsort.Text;
		m_VS_IsPlaySound = ckb_issound.Checked;
		m_VS_IsStopSound = ckb_stop.Checked;
		m_VS_IsShowOperationBar = ckb_opbar.Checked;
		m_VS_IsShowMessageBar = ckb_outputbar.Checked;
		m_VS_UseCustomAlarmWav = radioCustomWav.Checked;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
		{
			if (!item3.IsNewRow)
			{
				if (item3.Cells[1].Value.ToString() == "报  警")
				{
					list.Add(item3.Cells[0].Value.ToString());
				}
				if (item3.Cells[1].Value.ToString() == "不报警")
				{
					list2.Add(item3.Cells[0].Value.ToString());
				}
				continue;
			}
			break;
		}
		foreach (string item4 in list)
		{
			bool flag = false;
			foreach (AlertInfor item5 in mc.M_VarAlertInforlst)
			{
				if (item5.M_VarName == item4 && item5.M_VarID == m_VS_VarDictionary[item4])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				AlertInfor alertInfor = new AlertInfor();
				alertInfor.M_VarID = m_VS_VarDictionary[item4];
				alertInfor.M_VarName = item4;
				alertInfor.M_AlertMessage = "";
				alertInfor.M_AlttxtColor = Color.Red;
				alertInfor.M_AltbackColor = Color.Red;
				alertInfor.M_NotetxtColor = Color.Red;
				alertInfor.M_NotebackColor = Color.Red;
				alertInfor.M_RetxtColor = Color.Red;
				alertInfor.M_RebackColor = Color.Red;
				mc.M_VarAlertInforlst.Add(alertInfor);
			}
		}
		AlertInfor[] array = mc.M_VarAlertInforlst.ToArray();
		foreach (AlertInfor _AI in array)
		{
			Predicate<string> match = (string str) => str == _AI.M_VarName;
			if (list2.Exists(match))
			{
				mc.M_VarAlertInforlst.Remove(_AI);
			}
		}
		List<AlertInfor> list3 = new List<AlertInfor>(mc.M_VarAlertInforlst.ToArray());
		mc.M_VarAlertInforlst.Clear();
		foreach (string key in m_VS_VarDictionary.Keys)
		{
			foreach (AlertInfor item6 in list3)
			{
				if (item6.M_VarName == key)
				{
					mc.M_VarAlertInforlst.Add(item6);
				}
			}
		}
		foreach (AlertInfor item7 in mc.M_VarAlertInforlst)
		{
			foreach (DataGridViewRow item8 in (IEnumerable)dataGridView1.Rows)
			{
				if (!item8.IsNewRow)
				{
					if (item8.Cells[0].Value.ToString() == item7.M_VarName)
					{
						item7.M_AlertMessage = item8.Cells[4].Value.ToString();
						item7.M_AlttxtColor = item8.Cells[5].Style.BackColor;
						item7.M_NotetxtColor = item8.Cells[6].Style.BackColor;
						item7.M_RetxtColor = item8.Cells[7].Style.BackColor;
						item7.M_AltbackColor = item8.Cells[8].Style.BackColor;
						item7.M_NotebackColor = item8.Cells[9].Style.BackColor;
						item7.M_RebackColor = item8.Cells[10].Style.BackColor;
					}
					continue;
				}
				break;
			}
		}
		base.DialogResult = DialogResult.OK;
	}

	private void Btn_Cancel_Click(object sender, EventArgs e)
	{
		switch (MessageBox.Show("是否将更改保存到配置中？", "提示", MessageBoxButtons.YesNo))
		{
		case DialogResult.Yes:
			Btn_Ok_Click(null, null);
			break;
		case DialogResult.No:
			Close();
			break;
		}
	}

	private void Btn_Tort_Click(object sender, EventArgs e)
	{
		if (lbx_all.SelectedItem != null)
		{
			lbx_selected.Items.Add(lbx_all.SelectedItem.ToString());
			lbx_all.Items.Remove(lbx_all.SelectedItem.ToString());
		}
	}

	private void Btn_Tolt_Click(object sender, EventArgs e)
	{
		if (lbx_selected.SelectedItem != null)
		{
			lbx_all.Items.Add(lbx_selected.SelectedItem.ToString());
			lbx_selected.Items.Remove(lbx_selected.SelectedItem.ToString());
		}
	}

	private void Btn_Up_Click(object sender, EventArgs e)
	{
		if (lbx_selected.SelectedItem != null && lbx_selected.SelectedIndex > 0)
		{
			int selectedIndex = lbx_selected.SelectedIndex;
			string item = lbx_selected.SelectedItem.ToString();
			lbx_selected.Items.Remove(lbx_selected.SelectedItem);
			lbx_selected.Items.Insert(selectedIndex - 1, item);
			lbx_selected.SelectedIndex = selectedIndex - 1;
		}
	}

	private void Btn_Down_Click(object sender, EventArgs e)
	{
		if (lbx_selected.SelectedItem != null && lbx_selected.SelectedIndex < lbx_selected.Items.Count - 1)
		{
			int selectedIndex = lbx_selected.SelectedIndex;
			string item = lbx_selected.SelectedItem.ToString();
			lbx_selected.Items.Remove(lbx_selected.SelectedItem);
			lbx_selected.Items.Insert(selectedIndex + 1, item);
			lbx_selected.SelectedIndex = selectedIndex + 1;
		}
	}

	private void Lbl_Bkclr_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			Lbl_bkclr.BackColor = colorDialog1.Color;
		}
	}

	private void Lbl_Txtclr_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			Lbl_txtclr.BackColor = colorDialog1.Color;
		}
	}

	private void Lbl_Wdclr_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			Lbl_wdclr.BackColor = colorDialog1.Color;
		}
	}

	private void ViewSetting_Load(object sender, EventArgs e)
	{
		helpProvider.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
		Lbl_bkclr.BackColor = m_VS_TableHeadBackColor;
		Lbl_txtclr.BackColor = m_VS_TableHeadTextColor;
		Lbl_wdclr.BackColor = m_VS_WindowBackColor;
		AlertInfor alertInfor = new AlertInfor();
		m_VS_AlertMessage = alertInfor.M_AlertMessage;
		m_VS_AlertTextColor = alertInfor.M_AlttxtColor;
		m_VS_AlertBackColor = alertInfor.M_AltbackColor;
		m_VS_NoterizeTextColor = alertInfor.M_NotetxtColor;
		m_VS_NoterizeBackColor = alertInfor.M_NotebackColor;
		m_VS_RecoverTextColor = alertInfor.M_RetxtColor;
		m_VS_RecoverBackColor = alertInfor.M_RebackColor;
		if (m_VS_SortType == 0)
		{
			rd1.Checked = true;
		}
		else
		{
			rd2.Checked = true;
		}
		if (m_VS_ShowType == 0)
		{
			radio_realtime.Checked = true;
		}
		else if (m_VS_ShowType == 1)
		{
			radio_history.Checked = true;
		}
		ckb_issound.Checked = m_VS_IsPlaySound;
		ckb_stop.Checked = m_VS_IsStopSound;
		ckb_opbar.Checked = m_VS_IsShowOperationBar;
		ckb_outputbar.Checked = m_VS_IsShowMessageBar;
		textBox1.Text = m_vs_PlayTimes.ToString();
		radioDefaultWav.Checked = !m_VS_UseCustomAlarmWav;
		radioCustomWav.Checked = m_VS_UseCustomAlarmWav;
		lbx_all.Items.Add("变量");
		lbx_all.Items.Add("变量类型");
		lbx_all.Items.Add("变量报警上限");
		lbx_all.Items.Add("变量报警上上限");
		lbx_all.Items.Add("变量报警下限");
		lbx_all.Items.Add("变量报警下下限");
		lbx_all.Items.Add("位报警");
		lbx_all.Items.Add("目标报警");
		lbx_all.Items.Add("变化率报警");
		lbx_all.Items.Add("报警信息");
		lbx_all.Items.Add("报警ID");
		lbx_all.Items.Add("确认人");
		lbx_all.Items.Add("报警类型");
		lbx_all.Items.Add("报警时间");
		lbx_all.Items.Add("变量值");
		if (mc.m_RealTime != null)
		{
			foreach (string item in mc.m_RealTime)
			{
				if (lbx_all.Items.Contains(item))
				{
					lbx_all.Items.Remove(item);
					lbx_selected.Items.Add(item);
				}
			}
		}
		listBox1.Items.Add("报警ID");
		listBox1.Items.Add("变量");
		listBox1.Items.Add("报警时间");
		listBox1.Items.Add("报警信息");
		listBox1.Items.Add("确认时间");
		listBox1.Items.Add("报警类型");
		listBox1.Items.Add("确认人");
		listBox1.Items.Add("上限报警");
		listBox1.Items.Add("上上限报警");
		listBox1.Items.Add("下限报警");
		listBox1.Items.Add("下下限报警");
		listBox1.Items.Add("位报警");
		listBox1.Items.Add("目标报警");
		listBox1.Items.Add("变化率报警");
		listBox1.Items.Add("变量值");
		if (mc.m_HistoryColumns == null)
		{
			mc.m_HistoryColumns = new List<string>();
			mc.m_HistoryColumns.Add("报警ID");
			mc.m_HistoryColumns.Add("变量");
			mc.m_HistoryColumns.Add("报警时间");
			mc.m_HistoryColumns.Add("报警信息");
			mc.m_HistoryColumns.Add("确认时间");
			mc.m_HistoryColumns.Add("报警类型");
			mc.m_HistoryColumns.Add("确认人");
			mc.m_HistoryColumns.Add("上限报警");
			mc.m_HistoryColumns.Add("上上限报警");
			mc.m_HistoryColumns.Add("下限报警");
			mc.m_HistoryColumns.Add("下下限报警");
			mc.m_HistoryColumns.Add("位报警");
			mc.m_HistoryColumns.Add("目标报警");
			mc.m_HistoryColumns.Add("变化率报警");
			mc.m_HistoryColumns.Add("变量值");
		}
		if (mc.m_RealTime != null)
		{
			foreach (string historyColumn in mc.m_HistoryColumns)
			{
				if (listBox1.Items.Contains(historyColumn))
				{
					listBox1.Items.Remove(historyColumn);
					listBox2.Items.Add(historyColumn);
				}
			}
		}
		cbx_firstsort.Items.Add("报警时间");
		cbx_firstsort.Items.Add("确认时间");
		cbx_firstsort.Items.Add("报警状态");
		int num = 0;
		if (m_VS_FirstColumnSort == "")
		{
			cbx_firstsort.SelectedIndex = 1;
		}
		else
		{
			foreach (object item2 in cbx_firstsort.Items)
			{
				if (item2.ToString() == m_VS_FirstColumnSort)
				{
					cbx_firstsort.SelectedIndex = num;
					break;
				}
				num++;
			}
		}
		foreach (string item3 in m_VS_RealTimeSelectedColumn)
		{
			lbx_selected.Items.Add(item3);
			lbx_all.Items.Remove(item3);
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(mc.VarPath);
		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Item");
		foreach (XmlNode item4 in xmlNodeList)
		{
			_ = (XmlElement)item4.ChildNodes[1];
			m_VS_VarDictionary.Add(((XmlElement)item4).GetAttribute("Name").ToString(), Convert.ToInt32(((XmlElement)item4).GetAttribute("id")));
		}
		if (ckb_issound.Checked)
		{
			ckb_stop.Enabled = true;
			label19.Enabled = true;
			textBox1.Enabled = true;
			label20.Enabled = true;
		}
		else
		{
			ckb_stop.Enabled = false;
			label19.Enabled = false;
			textBox1.Enabled = false;
			label20.Enabled = false;
		}
		cmb_Temp.Items.Add("报  警");
		cmb_Temp.Items.Add("不报警");
		m_VS_AlertVarDt.Columns.Add("报警变量");
		m_VS_AlertVarDt.Columns.Add("是否报警");
		m_VS_AlertVarDt.Columns.Add("标签");
		m_VS_AlertVarDt.Columns.Add("备注");
		m_VS_AlertVarDt.Columns.Add("报警信息");
		m_VS_AlertVarDt.Columns.Add("报警文字颜色");
		m_VS_AlertVarDt.Columns.Add("确认文字颜色");
		m_VS_AlertVarDt.Columns.Add("恢复文字颜色");
		m_VS_AlertVarDt.Columns.Add("报警文字背景");
		m_VS_AlertVarDt.Columns.Add("确认文字背景");
		m_VS_AlertVarDt.Columns.Add("恢复文字背景");
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
		foreach (XmlNode item5 in xmlNodeList)
		{
			XmlElement xmlElement = (XmlElement)item5.ChildNodes[1];
			if (xmlElement.GetAttribute("LoloActive") == "1" || xmlElement.GetAttribute("LowActive") == "1" || xmlElement.GetAttribute("HighActive") == "1" || xmlElement.GetAttribute("HihiActive") == "1" || xmlElement.GetAttribute("AimActive") == "1" || xmlElement.GetAttribute("ShiftActive") == "1" || xmlElement.GetAttribute("AlarmType") == "1" || xmlElement.GetAttribute("AlarmType") == "2" || xmlElement.GetAttribute("AlarmType") == "3" || xmlElement.GetAttribute("AlarmType") == "4" || xmlElement.GetAttribute("AlarmType") == "5")
			{
				string key = ((XmlElement)item5).GetAttribute("Name").ToString();
				string value = ((XmlElement)item5).GetAttribute("Tag").ToString();
				string value2 = ((XmlElement)item5).GetAttribute("Description").ToString();
				dictionary.Add(key, value);
				dictionary2.Add(key, value2);
			}
		}
		m_VS_AlertVarDt.Clear();
		if (mc.M_VarAlertInforlst == null)
		{
			mc.M_VarAlertInforlst = new List<AlertInfor>();
		}
		if (mc.M_VarAlertInforlst.Count == 0)
		{
			foreach (KeyValuePair<string, string> item6 in dictionary)
			{
				DataRow dataRow = m_VS_AlertVarDt.NewRow();
				dataRow["报警变量"] = item6.Key;
				dataRow["是否报警"] = "不报警";
				dataRow["标签"] = item6.Value;
				dataRow["备注"] = dictionary2[item6.Key];
				dataRow["报警信息"] = "双击编辑";
				dataRow["报警文字颜色"] = "点击设置";
				dataRow["报警文字背景"] = "点击设置";
				dataRow["确认文字颜色"] = "点击设置";
				dataRow["确认文字背景"] = "点击设置";
				dataRow["恢复文字颜色"] = "点击设置";
				dataRow["恢复文字背景"] = "点击设置";
				m_VS_AlertVarDt.Rows.Add(dataRow);
			}
		}
		else
		{
			foreach (KeyValuePair<string, string> item7 in dictionary)
			{
				DataRow dataRow2 = m_VS_AlertVarDt.NewRow();
				dataRow2["报警变量"] = item7.Key;
				dataRow2["是否报警"] = "不报警";
				dataRow2["标签"] = item7.Value;
				dataRow2["备注"] = dictionary2[item7.Key];
				dataRow2["报警信息"] = "双击编辑";
				dataRow2["报警文字颜色"] = "点击设置";
				dataRow2["报警文字背景"] = "点击设置";
				dataRow2["确认文字颜色"] = "点击设置";
				dataRow2["确认文字背景"] = "点击设置";
				dataRow2["恢复文字颜色"] = "点击设置";
				dataRow2["恢复文字背景"] = "点击设置";
				m_VS_AlertVarDt.Rows.Add(dataRow2);
			}
		}
		dataGridView1.DataSource = m_VS_AlertVarDt;
		dataGridView1.AllowUserToAddRows = false;
		for (int i = 0; i < 10; i++)
		{
			dataGridView1.Columns[i].ReadOnly = true;
			dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
		}
		dataGridView1.Columns[4].ReadOnly = false;
		cmb_Temp.SelectedIndexChanged += cmb_Temp_SelectedIndexChanged;
		bt_Temp.Click += bt_Temp_Click;
		bt_Temp1.Click += bt_Temp1_Click;
		bt_Temp2.Click += bt_Temp2_Click;
		bt_Temp3.Click += bt_Temp3_Click;
		bt_Temp4.Click += bt_Temp4_Click;
		bt_Temp5.Click += bt_Temp5_Click;
		dataGridView1.Controls.Add(cmb_Temp);
		dataGridView1.Controls.Add(bt_Temp);
		dataGridView1.Controls.Add(bt_Temp1);
		dataGridView1.Controls.Add(bt_Temp2);
		dataGridView1.Controls.Add(bt_Temp3);
		dataGridView1.Controls.Add(bt_Temp4);
		dataGridView1.Controls.Add(bt_Temp5);
		cmb_Temp.Visible = false;
		bt_Temp.Visible = false;
		bt_Temp1.Visible = false;
		bt_Temp2.Visible = false;
		bt_Temp3.Visible = false;
		bt_Temp4.Visible = false;
		bt_Temp5.Visible = false;
	}

	private void btn_alltort_Click(object sender, EventArgs e)
	{
		foreach (object item in lbx_all.Items)
		{
			lbx_selected.Items.Add(item.ToString());
		}
		lbx_all.Items.Clear();
	}

	private void btn_alltolt_Click(object sender, EventArgs e)
	{
		foreach (object item in lbx_selected.Items)
		{
			lbx_all.Items.Add(item.ToString());
		}
		lbx_selected.Items.Clear();
	}

	private void ckb_issound_CheckedChanged(object sender, EventArgs e)
	{
		if (ckb_issound.Checked)
		{
			ckb_stop.Enabled = true;
			label19.Enabled = true;
			textBox1.Enabled = true;
			label20.Enabled = true;
		}
		else
		{
			ckb_stop.Enabled = false;
			label19.Enabled = false;
			textBox1.Enabled = false;
			label20.Enabled = false;
		}
	}

	private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
	{
		try
		{
			if (dataGridView1.CurrentCell == null)
			{
				return;
			}
			if (dataGridView1.CurrentCell.ColumnIndex == 1)
			{
				Rectangle cellDisplayRectangle = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				if (dataGridView1.CurrentCell.Value.ToString() == "不报警")
				{
					cmb_Temp.Text = "不报警";
				}
				else
				{
					cmb_Temp.Text = "报  警";
				}
				cmb_Temp.Left = cellDisplayRectangle.Left;
				cmb_Temp.Top = cellDisplayRectangle.Top;
				cmb_Temp.Width = cellDisplayRectangle.Width;
				cmb_Temp.Height = cellDisplayRectangle.Height;
				cmb_Temp.Visible = true;
				bt_Temp.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 5)
			{
				Rectangle cellDisplayRectangle2 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp.Left = cellDisplayRectangle2.Left;
				bt_Temp.Top = cellDisplayRectangle2.Top;
				bt_Temp.Width = cellDisplayRectangle2.Width;
				bt_Temp.Height = cellDisplayRectangle2.Height;
				bt_Temp.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 6)
			{
				Rectangle cellDisplayRectangle3 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp1.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp1.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp1.Left = cellDisplayRectangle3.Left;
				bt_Temp1.Top = cellDisplayRectangle3.Top;
				bt_Temp1.Width = cellDisplayRectangle3.Width;
				bt_Temp1.Height = cellDisplayRectangle3.Height;
				bt_Temp1.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 7)
			{
				Rectangle cellDisplayRectangle4 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp2.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp2.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp2.Left = cellDisplayRectangle4.Left;
				bt_Temp2.Top = cellDisplayRectangle4.Top;
				bt_Temp2.Width = cellDisplayRectangle4.Width;
				bt_Temp2.Height = cellDisplayRectangle4.Height;
				bt_Temp2.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 8)
			{
				Rectangle cellDisplayRectangle5 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp3.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp3.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp3.Left = cellDisplayRectangle5.Left;
				bt_Temp3.Top = cellDisplayRectangle5.Top;
				bt_Temp3.Width = cellDisplayRectangle5.Width;
				bt_Temp3.Height = cellDisplayRectangle5.Height;
				bt_Temp3.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 9)
			{
				Rectangle cellDisplayRectangle6 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp4.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp4.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp4.Left = cellDisplayRectangle6.Left;
				bt_Temp4.Top = cellDisplayRectangle6.Top;
				bt_Temp4.Width = cellDisplayRectangle6.Width;
				bt_Temp4.Height = cellDisplayRectangle6.Height;
				bt_Temp4.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp5.Visible = false;
			}
			else if (dataGridView1.CurrentCell.ColumnIndex == 10)
			{
				Rectangle cellDisplayRectangle7 = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, cutOverflow: false);
				bt_Temp5.Text = dataGridView1.CurrentCell.Value.ToString();
				bt_Temp5.BackColor = dataGridView1.CurrentCell.Style.BackColor;
				bt_Temp5.Left = cellDisplayRectangle7.Left;
				bt_Temp5.Top = cellDisplayRectangle7.Top;
				bt_Temp5.Width = cellDisplayRectangle7.Width;
				bt_Temp5.Height = cellDisplayRectangle7.Height;
				bt_Temp5.Visible = true;
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp1.Visible = false;
			}
			else
			{
				cmb_Temp.Visible = false;
				bt_Temp.Visible = false;
				bt_Temp1.Visible = false;
				bt_Temp2.Visible = false;
				bt_Temp3.Visible = false;
				bt_Temp4.Visible = false;
				bt_Temp5.Visible = false;
			}
		}
		catch (Exception)
		{
			MessageBox.Show("短信报警配置界面发生异常！");
		}
	}

	private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
		string s = (e.RowIndex + 1).ToString();
		Brush black = Brushes.Black;
		e.Graphics.DrawString(s, label3.Font, black, e.RowBounds.Location.X + 25 - 4, e.RowBounds.Location.Y + 4);
	}

	private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
	{
		cmb_Temp.Visible = false;
		bt_Temp.Visible = false;
		bt_Temp1.Visible = false;
		bt_Temp2.Visible = false;
		bt_Temp3.Visible = false;
		bt_Temp4.Visible = false;
		bt_Temp5.Visible = false;
	}

	private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		cmb_Temp.Visible = false;
		bt_Temp.Visible = false;
		bt_Temp1.Visible = false;
		bt_Temp2.Visible = false;
		bt_Temp3.Visible = false;
		bt_Temp4.Visible = false;
		bt_Temp5.Visible = false;
	}

	private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
	{
		if (e.RowIndex > dataGridView1.Rows.Count - 1)
		{
			return;
		}
		DataGridViewRow dataGridViewRow = (sender as DataGridView).Rows[e.RowIndex];
		if (((DataTable)dataGridView1.DataSource).Columns.Contains("是否报警"))
		{
			if (dataGridViewRow.Cells["是否报警"].Value.ToString() == "报  警")
			{
				dataGridViewRow.Cells["是否报警"].Style.BackColor = Color.OrangeRed;
			}
			if (dataGridViewRow.Cells["是否报警"].Value.ToString() == "不报警")
			{
				dataGridViewRow.Cells["是否报警"].Style.BackColor = Color.YellowGreen;
			}
		}
		if (temp_flag == 1)
		{
			temp_flag = 0;
			if (mc.M_VarAlertInforlst == null)
			{
				return;
			}
			if (mc.M_VarAlertInforlst.Count == 0)
			{
				foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
				{
					if (!item.IsNewRow)
					{
						item.Cells[5].Style.BackColor = Color.Red;
						item.Cells[6].Style.BackColor = Color.Blue;
						item.Cells[7].Style.BackColor = Color.Green;
						continue;
					}
					break;
				}
			}
			foreach (AlertInfor item2 in mc.M_VarAlertInforlst)
			{
				foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
				{
					if (!item3.IsNewRow)
					{
						if (item3.Cells[0].Value.ToString() == item2.M_VarName)
						{
							item3.Cells[1].Value = "报  警";
							item3.Cells[4].Value = item2.M_AlertMessage;
							item3.Cells[5].Style.BackColor = item2.M_AlttxtColor;
							item3.Cells[6].Style.BackColor = item2.M_NotetxtColor;
							item3.Cells[7].Style.BackColor = item2.M_RetxtColor;
							item3.Cells[8].Style.BackColor = item2.M_AltbackColor;
							item3.Cells[9].Style.BackColor = item2.M_NotebackColor;
							item3.Cells[10].Style.BackColor = item2.M_RebackColor;
						}
						continue;
					}
					break;
				}
			}
			foreach (DataGridViewRow item4 in (IEnumerable)dataGridView1.Rows)
			{
				if (!item4.IsNewRow)
				{
					if (item4.Cells[1].Value.ToString() == "不报警")
					{
						item4.Cells[5].Style.BackColor = Color.Red;
						item4.Cells[6].Style.BackColor = Color.Blue;
						item4.Cells[7].Style.BackColor = Color.Green;
					}
					continue;
				}
				break;
			}
		}
		if (m_SetALLColor != 1)
		{
			return;
		}
		m_SetALLColor = 0;
		foreach (DataGridViewRow item5 in (IEnumerable)dataGridView1.Rows)
		{
			if (!item5.IsNewRow)
			{
				item5.Cells[5].Style.BackColor = label_AlertText.BackColor;
				item5.Cells[6].Style.BackColor = label_NoteText.BackColor;
				item5.Cells[7].Style.BackColor = label_ReCoverText.BackColor;
				item5.Cells[8].Style.BackColor = label_AlertBack.BackColor;
				item5.Cells[9].Style.BackColor = label_NoteBack.BackColor;
				item5.Cells[10].Style.BackColor = label_RecoverBack.BackColor;
				continue;
			}
			break;
		}
	}

	private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (((ComboBox)sender).Text == "报  警")
		{
			dataGridView1.CurrentCell.Value = "报  警";
		}
		else
		{
			dataGridView1.CurrentCell.Value = "不报警";
		}
	}

	private void bt_Temp_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp.BackColor;
	}

	private void bt_Temp1_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp1.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp1.BackColor;
	}

	private void bt_Temp2_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp2.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp2.BackColor;
	}

	private void bt_Temp3_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp3.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp3.BackColor;
	}

	private void bt_Temp4_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp4.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp4.BackColor;
	}

	private void bt_Temp5_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			bt_Temp5.BackColor = colorDialog1.Color;
		}
		dataGridView1.CurrentCell.Style.BackColor = bt_Temp5.BackColor;
	}

	private void checkBox_AlertALL_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox_AlertALL.Checked)
		{
			checkBox_AlertNor.Checked = false;
			checkBox_AlertNor.Enabled = false;
		}
		else
		{
			checkBox_AlertNor.Enabled = true;
		}
	}

	private void checkBox_AlertNor_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox_AlertNor.Checked)
		{
			checkBox_AlertALL.Checked = false;
			checkBox_AlertALL.Enabled = false;
		}
		else
		{
			checkBox_AlertALL.Enabled = true;
		}
	}

	private void label_AlertText_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_AlertText.BackColor = colorDialog1.Color;
		}
	}

	private void label_NoteText_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_NoteText.BackColor = colorDialog1.Color;
		}
	}

	private void label_ReCoverText_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_ReCoverText.BackColor = colorDialog1.Color;
		}
	}

	private void label_AlertBack_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_AlertBack.BackColor = colorDialog1.Color;
		}
	}

	private void label_NoteBack_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_NoteBack.BackColor = colorDialog1.Color;
		}
	}

	private void label_RecoverBack_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			label_RecoverBack.BackColor = colorDialog1.Color;
		}
	}

	private void button_AlertALLOK_Click(object sender, EventArgs e)
	{
		if (checkBox_AlertALL.Checked)
		{
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				if (!item.IsNewRow)
				{
					item.Cells[1].Value = "报  警";
					continue;
				}
				break;
			}
		}
		if (checkBox_AlertNor.Checked)
		{
			foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
			{
				if (!item2.IsNewRow)
				{
					item2.Cells[1].Value = "不报警";
					continue;
				}
				break;
			}
		}
		if (textBox_AlertMessage.Text != "")
		{
			foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
			{
				if (!item3.IsNewRow)
				{
					item3.Cells[4].Value = textBox_AlertMessage.Text;
					continue;
				}
				break;
			}
		}
		if (checkBox_AlertMessage.Checked)
		{
			foreach (DataGridViewRow item4 in (IEnumerable)dataGridView1.Rows)
			{
				if (!item4.IsNewRow)
				{
					item4.Cells[4].Value = item4.Cells[2].Value;
					continue;
				}
				break;
			}
		}
		m_SetALLColor = 1;
	}

	private void lbx_all_DoubleClick(object sender, EventArgs e)
	{
		if (lbx_all.SelectedItem != null)
		{
			int num = 0;
			num = lbx_all.SelectedIndex;
			lbx_selected.Items.Add(lbx_all.SelectedItem.ToString());
			lbx_all.Items.Remove(lbx_all.SelectedItem.ToString());
			if (num < lbx_all.Items.Count)
			{
				lbx_all.SelectedIndex = num;
			}
		}
	}

	private void lbx_selected_DoubleClick(object sender, EventArgs e)
	{
		if (lbx_selected.SelectedItem != null)
		{
			int num = 0;
			num = lbx_selected.SelectedIndex;
			lbx_all.Items.Add(lbx_selected.SelectedItem.ToString());
			lbx_selected.Items.Remove(lbx_selected.SelectedItem.ToString());
			if (num < lbx_selected.Items.Count)
			{
				lbx_selected.SelectedIndex = num;
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (TestSound)
		{
			TestSound = false;
			mc.m_SoundPlayer.Stop();
			button1.Text = "测试报警声音";
			RadioButton radioButton = radioDefaultWav;
			bool enabled = (radioCustomWav.Enabled = true);
			radioButton.Enabled = enabled;
		}
		else
		{
			TestSound = true;
			mc.m_SoundPlayer.Play();
			button1.Text = "停止测试";
			RadioButton radioButton2 = radioDefaultWav;
			bool enabled2 = (radioCustomWav.Enabled = false);
			radioButton2.Enabled = enabled2;
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Stream stream;
		try
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
			saveFileDialog.FilterIndex = 0;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.CreatePrompt = true;
			saveFileDialog.Title = "历史报警导出excel";
			saveFileDialog.ShowDialog();
			stream = saveFileDialog.OpenFile();
		}
		catch (Exception)
		{
			return;
		}
		StreamWriter streamWriter = new StreamWriter(stream, Encoding.GetEncoding(0));
		string text = "";
		try
		{
			for (int i = 0; i < 5; i++)
			{
				if (i > 0)
				{
					text += "\t";
				}
				text += dataGridView1.Columns[i].HeaderText;
			}
			streamWriter.WriteLine(text);
			for (int j = 0; j < dataGridView1.Rows.Count; j++)
			{
				string text2 = "";
				for (int k = 0; k < 5; k++)
				{
					if (k > 0)
					{
						text2 += "\t";
					}
					text2 += dataGridView1.Rows[j].Cells[k].Value.ToString();
				}
				streamWriter.WriteLine(text2);
			}
			streamWriter.Close();
			stream.Close();
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.ToString());
		}
		finally
		{
			streamWriter.Close();
			stream.Close();
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
	}

	private void button5_Click(object sender, EventArgs e)
	{
		foreach (object item in listBox1.Items)
		{
			listBox2.Items.Add(item.ToString());
		}
		listBox1.Items.Clear();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		foreach (object item in listBox2.Items)
		{
			listBox1.Items.Add(item.ToString());
		}
		listBox2.Items.Clear();
	}

	private void button9_Click(object sender, EventArgs e)
	{
		if (listBox1.SelectedItem != null)
		{
			listBox2.Items.Add(listBox1.SelectedItem.ToString());
			listBox1.Items.Remove(listBox1.SelectedItem.ToString());
		}
	}

	private void button8_Click(object sender, EventArgs e)
	{
		if (listBox2.SelectedItem != null)
		{
			listBox1.Items.Add(listBox2.SelectedItem.ToString());
			listBox2.Items.Remove(listBox2.SelectedItem.ToString());
		}
	}

	private void button7_Click(object sender, EventArgs e)
	{
		if (listBox2.SelectedItem != null && listBox2.SelectedIndex > 0)
		{
			int selectedIndex = listBox2.SelectedIndex;
			string item = listBox2.SelectedItem.ToString();
			listBox2.Items.Remove(listBox2.SelectedItem);
			listBox2.Items.Insert(selectedIndex - 1, item);
			listBox2.SelectedIndex = selectedIndex - 1;
		}
	}

	private void button6_Click(object sender, EventArgs e)
	{
		if (listBox2.SelectedItem != null && listBox2.SelectedIndex < listBox2.Items.Count - 1)
		{
			int selectedIndex = listBox2.SelectedIndex;
			string item = listBox2.SelectedItem.ToString();
			listBox2.Items.Remove(listBox2.SelectedItem);
			listBox2.Items.Insert(selectedIndex + 1, item);
			listBox2.SelectedIndex = selectedIndex + 1;
		}
	}

	private void radioAlarmWav_Click(object sender, EventArgs e)
	{
		m_VS_UseCustomAlarmWav = false;
		if (radioCustomWav.Checked)
		{
			if (File.Exists(mc.ProFilePath + "\\Resources\\alarm.wav"))
			{
				m_VS_UseCustomAlarmWav = mc.m_SoundPlayer.SetSoundLocation(mc.ProFilePath + "\\Resources\\alarm.wav");
				return;
			}
			mc.m_SoundPlayer.SetSoundLocation(null);
			MessageBox.Show("请确保自定义的报警音命名为alarm.wav文件，并且放到\r\n工程的Resources目录下，然后您才可以勾选此项。", "提示");
			radioDefaultWav.Checked = true;
		}
		else
		{
			mc.m_SoundPlayer.SetSoundLocation(null);
		}
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert_Setting.ControlSettings));
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.ckb_outputbar = new System.Windows.Forms.CheckBox();
		this.ckb_opbar = new System.Windows.Forms.CheckBox();
		this.ckb_stop = new System.Windows.Forms.CheckBox();
		this.ckb_issound = new System.Windows.Forms.CheckBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.cbx_firstsort = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.rd2 = new System.Windows.Forms.RadioButton();
		this.rd1 = new System.Windows.Forms.RadioButton();
		this.label6 = new System.Windows.Forms.Label();
		this.btn_alltolt = new System.Windows.Forms.Button();
		this.btn_alltort = new System.Windows.Forms.Button();
		this.Btn_down = new System.Windows.Forms.Button();
		this.Btn_up = new System.Windows.Forms.Button();
		this.Btn_tolt = new System.Windows.Forms.Button();
		this.Btn_tort = new System.Windows.Forms.Button();
		this.lbx_selected = new System.Windows.Forms.ListBox();
		this.lbx_all = new System.Windows.Forms.ListBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Btn_ok = new System.Windows.Forms.Button();
		this.Btn_cancel = new System.Windows.Forms.Button();
		this.colorDialog1 = new System.Windows.Forms.ColorDialog();
		this.label1 = new System.Windows.Forms.Label();
		this.Lbl_bkclr = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.Lbl_txtclr = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.Lbl_wdclr = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.button3 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.checkBox_AlertMessage = new System.Windows.Forms.CheckBox();
		this.button_AlertALLOK = new System.Windows.Forms.Button();
		this.label15 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label_RecoverBack = new System.Windows.Forms.Label();
		this.label_ReCoverText = new System.Windows.Forms.Label();
		this.label_NoteBack = new System.Windows.Forms.Label();
		this.label_NoteText = new System.Windows.Forms.Label();
		this.label_AlertBack = new System.Windows.Forms.Label();
		this.label_AlertText = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.textBox_AlertMessage = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.checkBox_AlertNor = new System.Windows.Forms.CheckBox();
		this.checkBox_AlertALL = new System.Windows.Forms.CheckBox();
		this.helpProvider = new System.Windows.Forms.HelpProvider();
		this.radio_realtime = new System.Windows.Forms.RadioButton();
		this.radio_history = new System.Windows.Forms.RadioButton();
		this.Group7 = new System.Windows.Forms.GroupBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.button4 = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.button5 = new System.Windows.Forms.Button();
		this.label17 = new System.Windows.Forms.Label();
		this.button6 = new System.Windows.Forms.Button();
		this.listBox1 = new System.Windows.Forms.ListBox();
		this.button7 = new System.Windows.Forms.Button();
		this.listBox2 = new System.Windows.Forms.ListBox();
		this.button8 = new System.Windows.Forms.Button();
		this.button9 = new System.Windows.Forms.Button();
		this.label18 = new System.Windows.Forms.Label();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.radioDefaultWav = new System.Windows.Forms.RadioButton();
		this.radioCustomWav = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.Group7.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox4.Controls.Add(this.textBox1);
		this.groupBox4.Controls.Add(this.label20);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.ckb_outputbar);
		this.groupBox4.Controls.Add(this.ckb_opbar);
		this.groupBox4.Controls.Add(this.ckb_stop);
		this.groupBox4.Controls.Add(this.ckb_issound);
		this.groupBox4.Location = new System.Drawing.Point(13, 463);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(324, 116);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "报警";
		this.button1.Location = new System.Drawing.Point(6, 81);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(85, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "测试报警音";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox1.Location = new System.Drawing.Point(222, 27);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(55, 21);
		this.textBox1.TabIndex = 1;
		this.textBox1.Text = "0";
		this.toolTip1.SetToolTip(this.textBox1, "播放蜂鸣时，此处为次数，播放wav文件时，此处为播放时间（秒）");
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(283, 32);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(17, 12);
		this.label20.TabIndex = 2;
		this.label20.Text = "次";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(158, 31);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(65, 12);
		this.label19.TabIndex = 33;
		this.label19.Text = "播放次数：";
		this.ckb_outputbar.AutoSize = true;
		this.ckb_outputbar.Location = new System.Drawing.Point(160, 88);
		this.ckb_outputbar.Name = "ckb_outputbar";
		this.ckb_outputbar.Size = new System.Drawing.Size(84, 16);
		this.ckb_outputbar.TabIndex = 10;
		this.ckb_outputbar.Text = "辅助信息栏";
		this.ckb_outputbar.UseVisualStyleBackColor = true;
		this.ckb_opbar.AutoSize = true;
		this.ckb_opbar.Location = new System.Drawing.Point(16, 88);
		this.ckb_opbar.Name = "ckb_opbar";
		this.ckb_opbar.Size = new System.Drawing.Size(60, 16);
		this.ckb_opbar.TabIndex = 9;
		this.ckb_opbar.Text = "操作栏";
		this.ckb_opbar.UseVisualStyleBackColor = true;
		this.ckb_stop.AutoSize = true;
		this.ckb_stop.Location = new System.Drawing.Point(15, 59);
		this.ckb_stop.Name = "ckb_stop";
		this.ckb_stop.Size = new System.Drawing.Size(108, 16);
		this.ckb_stop.TabIndex = 3;
		this.ckb_stop.Text = "确认后停止播放";
		this.ckb_stop.UseVisualStyleBackColor = true;
		this.ckb_issound.AutoSize = true;
		this.ckb_issound.Location = new System.Drawing.Point(15, 31);
		this.ckb_issound.Name = "ckb_issound";
		this.ckb_issound.Size = new System.Drawing.Size(108, 16);
		this.ckb_issound.TabIndex = 0;
		this.ckb_issound.Text = "是否播放报警音";
		this.ckb_issound.UseVisualStyleBackColor = true;
		this.ckb_issound.CheckedChanged += new System.EventHandler(ckb_issound_CheckedChanged);
		this.groupBox3.Controls.Add(this.cbx_firstsort);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.rd2);
		this.groupBox3.Controls.Add(this.rd1);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Location = new System.Drawing.Point(12, 410);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(454, 45);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "排序";
		this.cbx_firstsort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbx_firstsort.FormattingEnabled = true;
		this.cbx_firstsort.Location = new System.Drawing.Point(322, 21);
		this.cbx_firstsort.Name = "cbx_firstsort";
		this.cbx_firstsort.Size = new System.Drawing.Size(123, 20);
		this.cbx_firstsort.TabIndex = 2;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(255, 26);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 12);
		this.label7.TabIndex = 3;
		this.label7.Text = "排序字段：";
		this.rd2.AutoSize = true;
		this.rd2.Location = new System.Drawing.Point(184, 24);
		this.rd2.Name = "rd2";
		this.rd2.Size = new System.Drawing.Size(47, 16);
		this.rd2.TabIndex = 1;
		this.rd2.TabStop = true;
		this.rd2.Text = "降序";
		this.rd2.UseVisualStyleBackColor = true;
		this.rd1.AutoSize = true;
		this.rd1.Location = new System.Drawing.Point(106, 24);
		this.rd1.Name = "rd1";
		this.rd1.Size = new System.Drawing.Size(47, 16);
		this.rd1.TabIndex = 0;
		this.rd1.TabStop = true;
		this.rd1.Text = "升序";
		this.rd1.UseVisualStyleBackColor = true;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(22, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 0;
		this.label6.Text = "排序方式：";
		this.btn_alltolt.Location = new System.Drawing.Point(129, 114);
		this.btn_alltolt.Name = "btn_alltolt";
		this.btn_alltolt.Size = new System.Drawing.Size(75, 23);
		this.btn_alltolt.TabIndex = 3;
		this.btn_alltolt.Text = "<<";
		this.btn_alltolt.UseVisualStyleBackColor = true;
		this.btn_alltolt.Click += new System.EventHandler(btn_alltolt_Click);
		this.btn_alltort.Location = new System.Drawing.Point(129, 24);
		this.btn_alltort.Name = "btn_alltort";
		this.btn_alltort.Size = new System.Drawing.Size(75, 23);
		this.btn_alltort.TabIndex = 0;
		this.btn_alltort.Text = ">>";
		this.btn_alltort.UseVisualStyleBackColor = true;
		this.btn_alltort.Click += new System.EventHandler(btn_alltort_Click);
		this.Btn_down.Location = new System.Drawing.Point(347, 115);
		this.Btn_down.Name = "Btn_down";
		this.Btn_down.Size = new System.Drawing.Size(75, 23);
		this.Btn_down.TabIndex = 5;
		this.Btn_down.Text = "下移";
		this.Btn_down.UseVisualStyleBackColor = true;
		this.Btn_down.Click += new System.EventHandler(Btn_Down_Click);
		this.Btn_up.Location = new System.Drawing.Point(347, 86);
		this.Btn_up.Name = "Btn_up";
		this.Btn_up.Size = new System.Drawing.Size(75, 23);
		this.Btn_up.TabIndex = 4;
		this.Btn_up.Text = "上移";
		this.Btn_up.UseVisualStyleBackColor = true;
		this.Btn_up.Click += new System.EventHandler(Btn_Up_Click);
		this.Btn_tolt.Location = new System.Drawing.Point(129, 84);
		this.Btn_tolt.Name = "Btn_tolt";
		this.Btn_tolt.Size = new System.Drawing.Size(75, 23);
		this.Btn_tolt.TabIndex = 2;
		this.Btn_tolt.Text = "<=";
		this.Btn_tolt.UseVisualStyleBackColor = true;
		this.Btn_tolt.Click += new System.EventHandler(Btn_Tolt_Click);
		this.Btn_tort.Location = new System.Drawing.Point(129, 54);
		this.Btn_tort.Name = "Btn_tort";
		this.Btn_tort.Size = new System.Drawing.Size(75, 23);
		this.Btn_tort.TabIndex = 1;
		this.Btn_tort.Text = "=>";
		this.Btn_tort.UseVisualStyleBackColor = true;
		this.Btn_tort.Click += new System.EventHandler(Btn_Tort_Click);
		this.lbx_selected.FormattingEnabled = true;
		this.lbx_selected.ItemHeight = 12;
		this.lbx_selected.Location = new System.Drawing.Point(220, 19);
		this.lbx_selected.Name = "lbx_selected";
		this.lbx_selected.Size = new System.Drawing.Size(109, 124);
		this.lbx_selected.TabIndex = 5;
		this.lbx_selected.DoubleClick += new System.EventHandler(lbx_selected_DoubleClick);
		this.lbx_all.FormattingEnabled = true;
		this.lbx_all.ItemHeight = 12;
		this.lbx_all.Location = new System.Drawing.Point(10, 19);
		this.lbx_all.Name = "lbx_all";
		this.lbx_all.Size = new System.Drawing.Size(109, 124);
		this.lbx_all.TabIndex = 0;
		this.lbx_all.DoubleClick += new System.EventHandler(lbx_all_DoubleClick);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(254, 4);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 1;
		this.label4.Text = "已选列";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(44, 4);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 0;
		this.label2.Text = "可选列";
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(7, 16);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(901, 248);
		this.dataGridView1.TabIndex = 16;
		this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
		this.dataGridView1.CurrentCellChanged += new System.EventHandler(dataGridView1_CurrentCellChanged);
		this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
		this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
		this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(dataGridView1_Scroll);
		this.Btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Btn_ok.Location = new System.Drawing.Point(740, 638);
		this.Btn_ok.Name = "Btn_ok";
		this.Btn_ok.Size = new System.Drawing.Size(82, 23);
		this.Btn_ok.TabIndex = 5;
		this.Btn_ok.Text = "确认";
		this.Btn_ok.UseVisualStyleBackColor = true;
		this.Btn_ok.Click += new System.EventHandler(Btn_Ok_Click);
		this.Btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Btn_cancel.Location = new System.Drawing.Point(849, 638);
		this.Btn_cancel.Name = "Btn_cancel";
		this.Btn_cancel.Size = new System.Drawing.Size(82, 23);
		this.Btn_cancel.TabIndex = 6;
		this.Btn_cancel.Text = "取消";
		this.Btn_cancel.UseVisualStyleBackColor = true;
		this.Btn_cancel.Click += new System.EventHandler(Btn_Cancel_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(19, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "表头背景色：";
		this.Lbl_bkclr.AutoSize = true;
		this.Lbl_bkclr.BackColor = System.Drawing.Color.Silver;
		this.Lbl_bkclr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Lbl_bkclr.Location = new System.Drawing.Point(99, 21);
		this.Lbl_bkclr.Name = "Lbl_bkclr";
		this.Lbl_bkclr.Size = new System.Drawing.Size(43, 14);
		this.Lbl_bkclr.TabIndex = 1;
		this.Lbl_bkclr.Text = "      ";
		this.Lbl_bkclr.Click += new System.EventHandler(Lbl_Bkclr_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(167, 21);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 12);
		this.label3.TabIndex = 2;
		this.label3.Text = "表头文字色：";
		this.Lbl_txtclr.AutoSize = true;
		this.Lbl_txtclr.BackColor = System.Drawing.Color.Black;
		this.Lbl_txtclr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Lbl_txtclr.Location = new System.Drawing.Point(245, 21);
		this.Lbl_txtclr.Name = "Lbl_txtclr";
		this.Lbl_txtclr.Size = new System.Drawing.Size(43, 14);
		this.Lbl_txtclr.TabIndex = 3;
		this.Lbl_txtclr.Text = "      ";
		this.Lbl_txtclr.Click += new System.EventHandler(Lbl_Txtclr_Click);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(310, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 12);
		this.label5.TabIndex = 4;
		this.label5.Text = "窗口背景色：";
		this.Lbl_wdclr.AutoSize = true;
		this.Lbl_wdclr.BackColor = System.Drawing.Color.White;
		this.Lbl_wdclr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Lbl_wdclr.Location = new System.Drawing.Point(388, 21);
		this.Lbl_wdclr.Name = "Lbl_wdclr";
		this.Lbl_wdclr.Size = new System.Drawing.Size(43, 14);
		this.Lbl_wdclr.TabIndex = 5;
		this.Lbl_wdclr.Text = "      ";
		this.Lbl_wdclr.Click += new System.EventHandler(Lbl_Wdclr_Click);
		this.groupBox1.Controls.Add(this.Lbl_wdclr);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.Lbl_txtclr);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.Lbl_bkclr);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(477, 410);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(455, 45);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "颜色";
		this.groupBox5.Controls.Add(this.dataGridView1);
		this.groupBox5.Location = new System.Drawing.Point(12, 1);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(919, 274);
		this.groupBox5.TabIndex = 17;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "报警变量配置";
		this.groupBox6.Controls.Add(this.button3);
		this.groupBox6.Controls.Add(this.button2);
		this.groupBox6.Controls.Add(this.checkBox_AlertMessage);
		this.groupBox6.Controls.Add(this.button_AlertALLOK);
		this.groupBox6.Controls.Add(this.label15);
		this.groupBox6.Controls.Add(this.label12);
		this.groupBox6.Controls.Add(this.label14);
		this.groupBox6.Controls.Add(this.label11);
		this.groupBox6.Controls.Add(this.label_RecoverBack);
		this.groupBox6.Controls.Add(this.label_ReCoverText);
		this.groupBox6.Controls.Add(this.label_NoteBack);
		this.groupBox6.Controls.Add(this.label_NoteText);
		this.groupBox6.Controls.Add(this.label_AlertBack);
		this.groupBox6.Controls.Add(this.label_AlertText);
		this.groupBox6.Controls.Add(this.label13);
		this.groupBox6.Controls.Add(this.label10);
		this.groupBox6.Controls.Add(this.textBox_AlertMessage);
		this.groupBox6.Controls.Add(this.label9);
		this.groupBox6.Controls.Add(this.checkBox_AlertNor);
		this.groupBox6.Controls.Add(this.checkBox_AlertALL);
		this.groupBox6.Location = new System.Drawing.Point(13, 281);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(918, 123);
		this.groupBox6.TabIndex = 0;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "报警变量统一配置";
		this.button3.Enabled = false;
		this.button3.Location = new System.Drawing.Point(125, 94);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 5;
		this.button3.Text = "导入Excel";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.button2.Enabled = false;
		this.button2.Location = new System.Drawing.Point(28, 94);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 4;
		this.button2.Text = "导出Excel";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.checkBox_AlertMessage.AutoSize = true;
		this.checkBox_AlertMessage.Location = new System.Drawing.Point(125, 20);
		this.checkBox_AlertMessage.Name = "checkBox_AlertMessage";
		this.checkBox_AlertMessage.Size = new System.Drawing.Size(216, 16);
		this.checkBox_AlertMessage.TabIndex = 1;
		this.checkBox_AlertMessage.Text = "将标签列复制到报警信息中（推荐）";
		this.checkBox_AlertMessage.UseVisualStyleBackColor = true;
		this.button_AlertALLOK.Location = new System.Drawing.Point(832, 94);
		this.button_AlertALLOK.Name = "button_AlertALLOK";
		this.button_AlertALLOK.Size = new System.Drawing.Size(75, 23);
		this.button_AlertALLOK.TabIndex = 6;
		this.button_AlertALLOK.Text = "应用";
		this.button_AlertALLOK.UseVisualStyleBackColor = true;
		this.button_AlertALLOK.Click += new System.EventHandler(button_AlertALLOK_Click);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(724, 52);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(77, 12);
		this.label15.TabIndex = 8;
		this.label15.Text = "报警文字背景";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(725, 18);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(77, 12);
		this.label12.TabIndex = 4;
		this.label12.Text = "恢复文字颜色";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(566, 52);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(77, 12);
		this.label14.TabIndex = 4;
		this.label14.Text = "确认文字背景";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(567, 18);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(77, 12);
		this.label11.TabIndex = 4;
		this.label11.Text = "确认文字颜色";
		this.label_RecoverBack.AutoSize = true;
		this.label_RecoverBack.BackColor = System.Drawing.Color.White;
		this.label_RecoverBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_RecoverBack.Location = new System.Drawing.Point(813, 50);
		this.label_RecoverBack.Name = "label_RecoverBack";
		this.label_RecoverBack.Size = new System.Drawing.Size(43, 14);
		this.label_RecoverBack.TabIndex = 1;
		this.label_RecoverBack.Text = "      ";
		this.label_RecoverBack.Click += new System.EventHandler(label_RecoverBack_Click);
		this.label_ReCoverText.AutoSize = true;
		this.label_ReCoverText.BackColor = System.Drawing.Color.Green;
		this.label_ReCoverText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_ReCoverText.Location = new System.Drawing.Point(814, 18);
		this.label_ReCoverText.Name = "label_ReCoverText";
		this.label_ReCoverText.Size = new System.Drawing.Size(43, 14);
		this.label_ReCoverText.TabIndex = 4;
		this.label_ReCoverText.Text = "      ";
		this.label_ReCoverText.Click += new System.EventHandler(label_ReCoverText_Click);
		this.label_NoteBack.AutoSize = true;
		this.label_NoteBack.BackColor = System.Drawing.Color.White;
		this.label_NoteBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_NoteBack.Location = new System.Drawing.Point(657, 50);
		this.label_NoteBack.Name = "label_NoteBack";
		this.label_NoteBack.Size = new System.Drawing.Size(43, 14);
		this.label_NoteBack.TabIndex = 9;
		this.label_NoteBack.Text = "      ";
		this.label_NoteBack.Click += new System.EventHandler(label_NoteBack_Click);
		this.label_NoteText.AutoSize = true;
		this.label_NoteText.BackColor = System.Drawing.Color.Blue;
		this.label_NoteText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_NoteText.Location = new System.Drawing.Point(658, 18);
		this.label_NoteText.Name = "label_NoteText";
		this.label_NoteText.Size = new System.Drawing.Size(43, 14);
		this.label_NoteText.TabIndex = 3;
		this.label_NoteText.Text = "      ";
		this.label_NoteText.Click += new System.EventHandler(label_NoteText_Click);
		this.label_AlertBack.AutoSize = true;
		this.label_AlertBack.BackColor = System.Drawing.Color.White;
		this.label_AlertBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_AlertBack.Location = new System.Drawing.Point(501, 50);
		this.label_AlertBack.Name = "label_AlertBack";
		this.label_AlertBack.Size = new System.Drawing.Size(43, 14);
		this.label_AlertBack.TabIndex = 7;
		this.label_AlertBack.Text = "      ";
		this.label_AlertBack.Click += new System.EventHandler(label_AlertBack_Click);
		this.label_AlertText.AutoSize = true;
		this.label_AlertText.BackColor = System.Drawing.Color.Red;
		this.label_AlertText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label_AlertText.Location = new System.Drawing.Point(502, 18);
		this.label_AlertText.Name = "label_AlertText";
		this.label_AlertText.Size = new System.Drawing.Size(43, 14);
		this.label_AlertText.TabIndex = 2;
		this.label_AlertText.Text = "      ";
		this.label_AlertText.Click += new System.EventHandler(label_AlertText_Click);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(408, 52);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(77, 12);
		this.label13.TabIndex = 4;
		this.label13.Text = "报警文字背景";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(409, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(77, 12);
		this.label10.TabIndex = 2;
		this.label10.Text = "报警文字颜色";
		this.textBox_AlertMessage.Location = new System.Drawing.Point(194, 51);
		this.textBox_AlertMessage.Multiline = true;
		this.textBox_AlertMessage.Name = "textBox_AlertMessage";
		this.textBox_AlertMessage.Size = new System.Drawing.Size(187, 18);
		this.textBox_AlertMessage.TabIndex = 3;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(123, 54);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(65, 12);
		this.label9.TabIndex = 2;
		this.label9.Text = "报警信息：";
		this.checkBox_AlertNor.AutoSize = true;
		this.checkBox_AlertNor.Location = new System.Drawing.Point(28, 50);
		this.checkBox_AlertNor.Name = "checkBox_AlertNor";
		this.checkBox_AlertNor.Size = new System.Drawing.Size(72, 16);
		this.checkBox_AlertNor.TabIndex = 2;
		this.checkBox_AlertNor.Text = "都不报警";
		this.checkBox_AlertNor.UseVisualStyleBackColor = true;
		this.checkBox_AlertNor.CheckedChanged += new System.EventHandler(checkBox_AlertNor_CheckedChanged);
		this.checkBox_AlertALL.AutoSize = true;
		this.checkBox_AlertALL.Location = new System.Drawing.Point(28, 21);
		this.checkBox_AlertALL.Name = "checkBox_AlertALL";
		this.checkBox_AlertALL.Size = new System.Drawing.Size(72, 16);
		this.checkBox_AlertALL.TabIndex = 0;
		this.checkBox_AlertALL.Text = "全部报警";
		this.checkBox_AlertALL.UseVisualStyleBackColor = true;
		this.checkBox_AlertALL.CheckedChanged += new System.EventHandler(checkBox_AlertALL_CheckedChanged);
		this.radio_realtime.AutoSize = true;
		this.radio_realtime.Location = new System.Drawing.Point(23, 18);
		this.radio_realtime.Name = "radio_realtime";
		this.radio_realtime.Size = new System.Drawing.Size(119, 16);
		this.radio_realtime.TabIndex = 34;
		this.radio_realtime.TabStop = true;
		this.radio_realtime.Text = "初始显示实时报警";
		this.radio_realtime.UseVisualStyleBackColor = true;
		this.radio_history.AutoSize = true;
		this.radio_history.Location = new System.Drawing.Point(222, 18);
		this.radio_history.Name = "radio_history";
		this.radio_history.Size = new System.Drawing.Size(119, 16);
		this.radio_history.TabIndex = 34;
		this.radio_history.TabStop = true;
		this.radio_history.Text = "初始显示历史报警";
		this.radio_history.UseVisualStyleBackColor = true;
		this.Group7.Controls.Add(this.radio_history);
		this.Group7.Controls.Add(this.radio_realtime);
		this.Group7.Location = new System.Drawing.Point(13, 585);
		this.Group7.Name = "Group7";
		this.Group7.Size = new System.Drawing.Size(453, 50);
		this.Group7.TabIndex = 35;
		this.Group7.TabStop = false;
		this.Group7.Text = "初始显示";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(477, 463);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(440, 172);
		this.tabControl1.TabIndex = 6;
		this.tabPage1.BackColor = System.Drawing.Color.Transparent;
		this.tabPage1.Controls.Add(this.btn_alltolt);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.btn_alltort);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.Btn_down);
		this.tabPage1.Controls.Add(this.lbx_all);
		this.tabPage1.Controls.Add(this.Btn_up);
		this.tabPage1.Controls.Add(this.lbx_selected);
		this.tabPage1.Controls.Add(this.Btn_tolt);
		this.tabPage1.Controls.Add(this.Btn_tort);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(432, 146);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "实时报警";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.BackColor = System.Drawing.Color.Transparent;
		this.tabPage2.Controls.Add(this.button4);
		this.tabPage2.Controls.Add(this.label8);
		this.tabPage2.Controls.Add(this.button5);
		this.tabPage2.Controls.Add(this.label17);
		this.tabPage2.Controls.Add(this.button6);
		this.tabPage2.Controls.Add(this.listBox1);
		this.tabPage2.Controls.Add(this.button7);
		this.tabPage2.Controls.Add(this.listBox2);
		this.tabPage2.Controls.Add(this.button8);
		this.tabPage2.Controls.Add(this.button9);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(432, 146);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "历史报警";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.button4.Location = new System.Drawing.Point(129, 114);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 12;
		this.button4.Text = "<<";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(44, 4);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(41, 12);
		this.label8.TabIndex = 7;
		this.label8.Text = "可选列";
		this.button5.Location = new System.Drawing.Point(129, 24);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(75, 23);
		this.button5.TabIndex = 8;
		this.button5.Text = ">>";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(254, 4);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(41, 12);
		this.label17.TabIndex = 10;
		this.label17.Text = "已选列";
		this.button6.Location = new System.Drawing.Point(347, 115);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(75, 23);
		this.button6.TabIndex = 14;
		this.button6.Text = "下移";
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 12;
		this.listBox1.Location = new System.Drawing.Point(10, 19);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(109, 124);
		this.listBox1.TabIndex = 6;
		this.listBox1.DoubleClick += new System.EventHandler(button9_Click);
		this.button7.Location = new System.Drawing.Point(347, 86);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(75, 23);
		this.button7.TabIndex = 13;
		this.button7.Text = "上移";
		this.button7.UseVisualStyleBackColor = true;
		this.button7.Click += new System.EventHandler(button7_Click);
		this.listBox2.FormattingEnabled = true;
		this.listBox2.ItemHeight = 12;
		this.listBox2.Location = new System.Drawing.Point(220, 19);
		this.listBox2.Name = "listBox2";
		this.listBox2.Size = new System.Drawing.Size(109, 124);
		this.listBox2.TabIndex = 15;
		this.listBox2.DoubleClick += new System.EventHandler(button8_Click);
		this.button8.Location = new System.Drawing.Point(129, 84);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(75, 23);
		this.button8.TabIndex = 11;
		this.button8.Text = "<=";
		this.button8.UseVisualStyleBackColor = true;
		this.button8.Click += new System.EventHandler(button8_Click);
		this.button9.Location = new System.Drawing.Point(129, 54);
		this.button9.Name = "button9";
		this.button9.Size = new System.Drawing.Size(75, 23);
		this.button9.TabIndex = 9;
		this.button9.Text = "=>";
		this.button9.UseVisualStyleBackColor = true;
		this.button9.Click += new System.EventHandler(button9_Click);
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(12, 643);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(71, 12);
		this.label18.TabIndex = 36;
		this.label18.Text = "2.3.0.04071";
		this.radioDefaultWav.AutoSize = true;
		this.radioDefaultWav.Checked = true;
		this.radioDefaultWav.Location = new System.Drawing.Point(6, 24);
		this.radioDefaultWav.Name = "radioDefaultWav";
		this.radioDefaultWav.Size = new System.Drawing.Size(83, 16);
		this.radioDefaultWav.TabIndex = 34;
		this.radioDefaultWav.TabStop = true;
		this.radioDefaultWav.Text = "默认报警音";
		this.toolTip1.SetToolTip(this.radioDefaultWav, "使用组件内嵌的报警音");
		this.radioDefaultWav.UseVisualStyleBackColor = true;
		this.radioDefaultWav.Click += new System.EventHandler(radioAlarmWav_Click);
		this.radioCustomWav.AutoSize = true;
		this.radioCustomWav.Location = new System.Drawing.Point(6, 49);
		this.radioCustomWav.Name = "radioCustomWav";
		this.radioCustomWav.Size = new System.Drawing.Size(83, 16);
		this.radioCustomWav.TabIndex = 35;
		this.radioCustomWav.Text = "自选报警音";
		this.toolTip1.SetToolTip(this.radioCustomWav, "使用工程定义的报警音");
		this.radioCustomWav.UseVisualStyleBackColor = true;
		this.radioCustomWav.Click += new System.EventHandler(radioAlarmWav_Click);
		this.groupBox2.Controls.Add(this.radioCustomWav);
		this.groupBox2.Controls.Add(this.button1);
		this.groupBox2.Controls.Add(this.radioDefaultWav);
		this.groupBox2.Location = new System.Drawing.Point(343, 463);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(123, 116);
		this.groupBox2.TabIndex = 36;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "报警音";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(929, 662);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.label18);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.Group7);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.Btn_cancel);
		base.Controls.Add(this.Btn_ok);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		this.helpProvider.SetHelpKeyword(this, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(945, 700);
		base.Name = "ControlSettings";
		this.helpProvider.SetShowHelp(this, true);
		this.Text = "属性";
		base.Load += new System.EventHandler(ViewSetting_Load);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.Group7.ResumeLayout(false);
		this.Group7.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
