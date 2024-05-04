using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Alert_Main.Properties;
using Alert_Setting;
using CommonSnappableTypes;
using HMIClient;

namespace Alert_Main;

public class MainControl : UserControl, IDCCEControl
{
	private bool _isRuning;

	private Client client;

	private Color m_TableHeadBackColor = Color.Silver;

	private Color m_TableHeadTextColor = Color.Black;

	private Color m_WindowBackColor = Color.White;

	private List<string> m_SelectedColumnList = new List<string>();

	private int m_SortType;

	private string m_FirstColumnSort = "报警时间";

	private List<string> m_VarHasSelectedList = new List<string>();

	private DataTable m_RealTimeVarDt = new DataTable();

	private DataTable m_HisVarDt = new DataTable();

	private bool m_IsPlaySound = true;

	private bool m_IsStopSound = true;

	private bool m_IsShowOperationBar = true;

	private bool m_IsShowMessageBar = true;

	private bool m_DtpFlag;

	private bool m_UseCustomAlarmWav;

	private List<AlertInfor> m_VarAlertInforList = new List<AlertInfor>();

	private List<AlertInfor> m_TempVarAlertInforList = new List<AlertInfor>();

	private string ProName = "";

	public string ProFilePath = "";

	public List<string> m_RealTime = new List<string>();

	public List<string> m_HistoryColumns = new List<string>();

	public PlayAlarmSound m_SoundPlayer = new PlayAlarmSound();

	private int m_ShowType;

	private int iVarColumnWidth = 120;

	private int iAlarmTimeColumnWidth = 120;

	private int iAlarmInfoColumnWidth = 120;

	private int iVarValueColumnWidth = 120;

	private int iConfirmTimeColumnWidth = 120;

	private int iAlarmTypeColumnWidth = 120;

	private int iVarTypeColumnWidth = 120;

	private int iAlarmStatusColumnWidth = 120;

	private string m_Throw_VarName = "";

	private string m_Throw_AlertMsg = "";

	private string m_Throw_AlertTime = "";

	private string m_Throw_AlertType = "";

	private string m_Throw_VarType = "";

	private string m_Throw_AlertID = "";

	private string m_VarPath = "";

	public int m_AlertCount;

	private string m_getAlertTime = "";

	public int m_NoteCount;

	private int m_PlayTimes;

	private int m_RealTimeShowModel = 1;

	private IContainer components;

	private SplitContainer splitContainer1;

	private Button Btn_note;

	private ComboBox Cbx1;

	private Button Btn_slc;

	private Button Btn_output;

	private DataGridView Dg1;

	private Panel pn_history;

	private Label label2;

	private Label label1;

	private DateTimePicker dtp_end;

	private DateTimePicker dtp_start;

	private SplitContainer splitContainer2;

	private Label label3;

	private Label label12;

	private Label Lbl_msgnote;

	private Label label10;

	private Label Lbl_msgalt;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label Lbl_msgall;

	private Label label6;

	private Button Btn_allnote;

	private Panel pn_his;

	private Label label_result;

	private Label label13;

	private Label label_time;

	private Label label11;

	private Label label_connect;

	private Panel pan_AlertMsg;

	private Label Lbl_msgaltmsg;

	private Label label5;

	private Label Lbl_msgvar;

	private Label label4;

	private ContextMenuStrip HistoryMenuStrip;

	private ToolStripMenuItem 设置显示列ToolStripMenuItem;

	private ToolStripMenuItem 清除当前显示数据ToolStripMenuItem;

	private Button button1;

	private HelpProvider helpProvider;

	private Button button2;

	[Browsable(false)]
	public bool isRuning
	{
		get
		{
			return _isRuning;
		}
		set
		{
			_isRuning = value;
			if (_isRuning)
			{
				client = (Client)this.GetSystemItemEvent("HMIClient");
				client.DeviceAlarmEvent += client_DeviceAlarmEvent;
				client.VariForAlarm += client_VariForAlarm;
				client.VariableAlarmConfirm += client_VariableAlarmConfirm;
				m_RealTimeVarDt.Rows.Clear();
				Dg1.BackgroundColor = m_WindowBackColor;
				splitContainer1.BackColor = m_WindowBackColor;
				splitContainer2.Panel2.BackColor = m_WindowBackColor;
				Dg1.EnableHeadersVisualStyles = false;
				Dg1.ColumnHeadersDefaultCellStyle.BackColor = m_TableHeadBackColor;
				Dg1.ColumnHeadersDefaultCellStyle.ForeColor = m_TableHeadTextColor;
				Cbx1.Enabled = true;
				Btn_note.Enabled = true;
				Btn_allnote.Enabled = true;
				Btn_slc.Enabled = true;
				Btn_output.Enabled = true;
				if (m_IsShowOperationBar)
				{
					splitContainer1.Panel1Collapsed = false;
				}
				else
				{
					splitContainer1.Panel1Collapsed = true;
				}
				if (m_IsShowMessageBar)
				{
					splitContainer2.Panel2Collapsed = false;
				}
				else
				{
					splitContainer2.Panel2Collapsed = true;
				}
				if (m_ShowType == 0)
				{
					Cbx1.SelectedIndex = 0;
				}
				else if (m_ShowType == 1)
				{
					Cbx1.SelectedIndex = 1;
				}
				RefleshDataTable();
				{
					foreach (DataGridViewColumn column in Dg1.Columns)
					{
						column.MinimumWidth = 120;
						if (Dg1.Columns.Contains("变量"))
						{
							Dg1.Columns["变量"].Width = VarColumnWidth;
						}
						if (Dg1.Columns.Contains("报警时间"))
						{
							Dg1.Columns["报警时间"].Width = AlarmTimeColumnWidth;
						}
						if (Dg1.Columns.Contains("报警信息"))
						{
							Dg1.Columns["报警信息"].Width = AlarmInfoColumnWidth;
						}
						if (Dg1.Columns.Contains("变量值列宽"))
						{
							Dg1.Columns["变量值列宽"].Width = VarValueColumnWidth;
						}
						if (Dg1.Columns.Contains("确认时间"))
						{
							Dg1.Columns["确认时间"].Width = ConfirmTimeColumnWidth;
						}
						if (Dg1.Columns.Contains("报警类型"))
						{
							Dg1.Columns["报警类型"].Width = AlarmTypeColumnWidth;
						}
						if (Dg1.Columns.Contains("变量类型"))
						{
							Dg1.Columns["变量类型"].Width = VarTypeColumnWidth;
						}
						if (Dg1.Columns.Contains("报警状态"))
						{
							Dg1.Columns["报警状态"].Width = AlarmStatusColumnWidth;
						}
					}
					return;
				}
			}
			if (m_IsShowOperationBar)
			{
				splitContainer1.Panel1Collapsed = false;
			}
			else
			{
				splitContainer1.Panel1Collapsed = true;
			}
			if (m_IsShowMessageBar)
			{
				splitContainer2.Panel2Collapsed = false;
			}
			else
			{
				splitContainer2.Panel2Collapsed = true;
			}
		}
	}

	[Browsable(false)]
	public Color M_Tbbackcolor
	{
		get
		{
			return m_TableHeadBackColor;
		}
		set
		{
			m_TableHeadBackColor = value;
		}
	}

	[Browsable(false)]
	public Color M_Tbtxtcolor
	{
		get
		{
			return m_TableHeadTextColor;
		}
		set
		{
			m_TableHeadTextColor = value;
		}
	}

	[Browsable(false)]
	public Color M_Windowbackcolor
	{
		get
		{
			return m_WindowBackColor;
		}
		set
		{
			m_WindowBackColor = value;
		}
	}

	[Browsable(false)]
	public List<string> M_Selectedcollst
	{
		get
		{
			return m_SelectedColumnList;
		}
		set
		{
			m_SelectedColumnList = value;
		}
	}

	[Browsable(false)]
	public int M_Sorttype
	{
		get
		{
			return m_SortType;
		}
		set
		{
			m_SortType = value;
		}
	}

	[Browsable(false)]
	public string M_Firstcol
	{
		get
		{
			return m_FirstColumnSort;
		}
		set
		{
			m_FirstColumnSort = value;
		}
	}

	[Browsable(false)]
	public List<string> M_Varlst
	{
		get
		{
			return m_VarHasSelectedList;
		}
		set
		{
			m_VarHasSelectedList = value;
		}
	}

	[Browsable(false)]
	public DataTable M_RealTimeVarDt
	{
		get
		{
			return m_RealTimeVarDt;
		}
		set
		{
			m_RealTimeVarDt = value;
		}
	}

	[Browsable(false)]
	public DataTable M_HisVarDt
	{
		get
		{
			return m_HisVarDt;
		}
		set
		{
			m_HisVarDt = value;
		}
	}

	[Browsable(false)]
	public bool M_IsPlaySound
	{
		get
		{
			return m_IsPlaySound;
		}
		set
		{
			m_IsPlaySound = value;
		}
	}

	[Browsable(false)]
	public bool M_IsStopSound
	{
		get
		{
			return m_IsStopSound;
		}
		set
		{
			m_IsStopSound = value;
		}
	}

	[Browsable(false)]
	public bool M_IsShowOperationBar
	{
		get
		{
			return m_IsShowOperationBar;
		}
		set
		{
			m_IsShowOperationBar = value;
		}
	}

	[Browsable(false)]
	public bool M_IsShowMessageBar
	{
		get
		{
			return m_IsShowMessageBar;
		}
		set
		{
			m_IsShowMessageBar = value;
		}
	}

	[Browsable(false)]
	public List<AlertInfor> M_VarAlertInforlst
	{
		get
		{
			return m_VarAlertInforList;
		}
		set
		{
			m_VarAlertInforList = value;
		}
	}

	[ReadOnly(false)]
	[DisplayName("变量列宽")]
	[Category("外观")]
	[Description("")]
	public int VarColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("变量列宽"))
			{
				Dg1.Columns["变量列宽"].Width = iVarColumnWidth;
			}
			return iVarColumnWidth;
		}
		set
		{
			iVarColumnWidth = value;
		}
	}

	[Description("")]
	[ReadOnly(false)]
	[DisplayName("报警时间列宽")]
	[Category("列宽设置")]
	public int AlarmTimeColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("报警时间"))
			{
				Dg1.Columns["报警时间"].Width = iAlarmTimeColumnWidth;
			}
			return iAlarmTimeColumnWidth;
		}
		set
		{
			iAlarmTimeColumnWidth = value;
		}
	}

	[DisplayName("报警信息列宽")]
	[Description("")]
	[ReadOnly(false)]
	[Category("列宽设置")]
	public int AlarmInfoColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("报警信息"))
			{
				Dg1.Columns["报警信息"].Width = iAlarmInfoColumnWidth;
			}
			return iAlarmInfoColumnWidth;
		}
		set
		{
			iAlarmInfoColumnWidth = value;
		}
	}

	[ReadOnly(false)]
	[Description("")]
	[DisplayName("变量值列宽")]
	[Category("列宽设置")]
	public int VarValueColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("变量值"))
			{
				Dg1.Columns["变量值"].Width = iVarValueColumnWidth;
			}
			return iVarValueColumnWidth;
		}
		set
		{
			iVarValueColumnWidth = value;
		}
	}

	[ReadOnly(false)]
	[DisplayName("确认时间列宽")]
	[Category("列宽设置")]
	[Description("")]
	public int ConfirmTimeColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("确认时间"))
			{
				Dg1.Columns["确认时间"].Width = iConfirmTimeColumnWidth;
			}
			return iConfirmTimeColumnWidth;
		}
		set
		{
			iConfirmTimeColumnWidth = value;
		}
	}

	[Category("列宽设置")]
	[DisplayName("报警类型列宽")]
	[ReadOnly(false)]
	[Description("")]
	public int AlarmTypeColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("报警类型"))
			{
				Dg1.Columns["报警类型"].Width = iAlarmTypeColumnWidth;
			}
			return iAlarmTypeColumnWidth;
		}
		set
		{
			iAlarmTypeColumnWidth = value;
		}
	}

	[DisplayName("变量类型列宽")]
	[ReadOnly(false)]
	[Category("列宽设置")]
	[Description("")]
	public int VarTypeColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("变量类型"))
			{
				Dg1.Columns["变量类型"].Width = iVarTypeColumnWidth;
			}
			return iVarTypeColumnWidth;
		}
		set
		{
			iVarTypeColumnWidth = value;
		}
	}

	[DisplayName("报警转台列宽")]
	[Category("列宽设置")]
	[Description("")]
	[ReadOnly(false)]
	public int AlarmStatusColumnWidth
	{
		get
		{
			if (Dg1.Columns.Contains("报警状态"))
			{
				Dg1.Columns["报警状态"].Width = iAlarmStatusColumnWidth;
			}
			return iAlarmStatusColumnWidth;
		}
		set
		{
			iAlarmStatusColumnWidth = value;
		}
	}

	[Description("设置或获取控件的背景色")]
	[ReadOnly(false)]
	[Category("外观")]
	[DisplayName("背景色")]
	public new Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	[DisplayName("光标")]
	[ReadOnly(false)]
	[Category("外观")]
	[Description("指针移动过该控件时显示的光标")]
	public new Cursor Cursor
	{
		get
		{
			return base.Cursor;
		}
		set
		{
			base.Cursor = value;
		}
	}

	[ReadOnly(false)]
	[Description("设置控件中字体的样式")]
	[DisplayName("字体设置")]
	[Category("外观")]
	public new Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
		}
	}

	[Category("外观")]
	[Description("设置或获取控件的文本色")]
	[ReadOnly(false)]
	[DisplayName("文本色")]
	public new Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	[Category("外观")]
	[Description("与控件关联的文本")]
	[ReadOnly(false)]
	[DisplayName("文本")]
	public new string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	[ReadOnly(false)]
	[Description("指示面板是否应具有边框")]
	[DisplayName("边框类型")]
	[Category("外观")]
	public new BorderStyle BorderStyle
	{
		get
		{
			return base.BorderStyle;
		}
		set
		{
			base.BorderStyle = value;
		}
	}

	[Browsable(false)]
	public new RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			base.RightToLeft = value;
		}
	}

	[Browsable(false)]
	public new bool UseWaitCursor
	{
		get
		{
			return base.UseWaitCursor;
		}
		set
		{
			base.UseWaitCursor = value;
		}
	}

	[Browsable(false)]
	public new Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	[Browsable(false)]
	public new ImageLayout BackgroundImageLayout
	{
		get
		{
			return base.BackgroundImageLayout;
		}
		set
		{
			base.BackgroundImageLayout = value;
		}
	}

	[Category("布局")]
	[ReadOnly(false)]
	[DisplayName("位置")]
	[Description("控件左上角相对于其容器左上角的坐标")]
	public new Point Location
	{
		get
		{
			return base.Location;
		}
		set
		{
			base.Location = value;
		}
	}

	[DisplayName("大小")]
	[Description("控件的大小（以像素为单位）")]
	[ReadOnly(false)]
	[Category("布局")]
	public new Size Size
	{
		get
		{
			return base.Size;
		}
		set
		{
			base.Size = value;
		}
	}

	[Browsable(false)]
	public new bool AutoSize
	{
		get
		{
			return base.AutoSize;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	[Browsable(false)]
	public new AutoSizeMode AutoSizeMode
	{
		get
		{
			return base.AutoSizeMode;
		}
		set
		{
			base.AutoSizeMode = value;
		}
	}

	[Browsable(false)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	[Browsable(false)]
	public new AnchorStyles Anchor
	{
		get
		{
			return base.Anchor;
		}
		set
		{
			base.Anchor = value;
		}
	}

	[Browsable(false)]
	public new DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
		}
	}

	[Browsable(false)]
	public new Padding Margin
	{
		get
		{
			return base.Margin;
		}
		set
		{
			base.Margin = value;
		}
	}

	[Browsable(false)]
	public new Size MaximumSize
	{
		get
		{
			return base.MaximumSize;
		}
		set
		{
			base.MaximumSize = value;
		}
	}

	[Browsable(false)]
	public new Size MinimumSize
	{
		get
		{
			return base.MinimumSize;
		}
		set
		{
			base.MinimumSize = value;
		}
	}

	[Browsable(false)]
	public new bool AutoScroll
	{
		get
		{
			return base.AutoScroll;
		}
		set
		{
			base.AutoScroll = value;
		}
	}

	[Browsable(false)]
	public new Size AutoScrollMargin
	{
		get
		{
			return base.AutoScrollMargin;
		}
		set
		{
			base.AutoScrollMargin = value;
		}
	}

	[Browsable(false)]
	public new Size AutoScrollMinSize
	{
		get
		{
			return base.AutoScrollMinSize;
		}
		set
		{
			base.AutoScrollMinSize = value;
		}
	}

	[Browsable(false)]
	public new bool CausesValidation
	{
		get
		{
			return base.CausesValidation;
		}
		set
		{
			base.CausesValidation = value;
		}
	}

	[Browsable(false)]
	public new string AccessibleName
	{
		get
		{
			return base.AccessibleName;
		}
		set
		{
			base.AccessibleName = value;
		}
	}

	[Browsable(false)]
	public new AccessibleRole AccessibleRole
	{
		get
		{
			return base.AccessibleRole;
		}
		set
		{
			base.AccessibleRole = value;
		}
	}

	[Browsable(false)]
	public new string AccessibleDescription
	{
		get
		{
			return base.AccessibleDescription;
		}
		set
		{
			base.AccessibleDescription = value;
		}
	}

	[Browsable(false)]
	public new object Tag
	{
		get
		{
			return base.Tag;
		}
		set
		{
			base.Tag = value;
		}
	}

	[Browsable(false)]
	public new ControlBindingsCollection DataBindings => base.DataBindings;

	[Description("确定该控件是可见还是隐藏")]
	[ReadOnly(false)]
	[DisplayName("可见性")]
	[Category("行为")]
	public new bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	[Browsable(false)]
	public new bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	[Browsable(false)]
	public new bool AllowDrop
	{
		get
		{
			return base.AllowDrop;
		}
		set
		{
			base.AllowDrop = value;
		}
	}

	[Browsable(false)]
	public new ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return base.ContextMenuStrip;
		}
		set
		{
			base.ContextMenuStrip = value;
		}
	}

	[Browsable(false)]
	public new int TabIndex
	{
		get
		{
			return base.TabIndex;
		}
		set
		{
			base.TabIndex = value;
		}
	}

	[Browsable(false)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			base.TabStop = value;
		}
	}

	[Browsable(false)]
	public new AutoValidate AutoValidate
	{
		get
		{
			return base.AutoValidate;
		}
		set
		{
			base.AutoValidate = value;
		}
	}

	[Browsable(false)]
	public new ImeMode ImeMode
	{
		get
		{
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	[Browsable(false)]
	public string Throw_VarName
	{
		get
		{
			return m_Throw_VarName;
		}
		set
		{
			m_Throw_VarName = value;
		}
	}

	[Browsable(false)]
	public string Throw_AlertMsg
	{
		get
		{
			return m_Throw_AlertMsg;
		}
		set
		{
			m_Throw_AlertMsg = value;
		}
	}

	[Browsable(false)]
	public string Throw_AlertTime
	{
		get
		{
			return m_Throw_AlertTime;
		}
		set
		{
			m_Throw_AlertTime = value;
		}
	}

	[Browsable(false)]
	public string Throw_AlertType
	{
		get
		{
			return m_Throw_AlertType;
		}
		set
		{
			m_Throw_AlertType = value;
		}
	}

	[Browsable(false)]
	public string Throw_VarType
	{
		get
		{
			return m_Throw_VarType;
		}
		set
		{
			m_Throw_VarType = value;
		}
	}

	[Browsable(false)]
	public string Throw_AlertID
	{
		get
		{
			return m_Throw_AlertID;
		}
		set
		{
			m_Throw_AlertID = value;
		}
	}

	[Browsable(false)]
	public string VarPath
	{
		get
		{
			return m_VarPath;
		}
		set
		{
			m_VarPath = value;
		}
	}

	[Browsable(false)]
	public int AlertCount
	{
		get
		{
			return m_AlertCount;
		}
		set
		{
			m_AlertCount = value;
		}
	}

	[Browsable(false)]
	public string GetAlertTime
	{
		get
		{
			return m_getAlertTime;
		}
		set
		{
			m_getAlertTime = value;
		}
	}

	[Browsable(false)]
	public int NoteCount
	{
		get
		{
			return m_NoteCount;
		}
		set
		{
			m_NoteCount = value;
		}
	}

	[Browsable(false)]
	public int PlayTimes
	{
		get
		{
			return m_PlayTimes;
		}
		set
		{
			m_PlayTimes = value;
		}
	}

	[Browsable(false)]
	public int RealTimeShowModel
	{
		get
		{
			return m_RealTimeShowModel;
		}
		set
		{
			m_RealTimeShowModel = value;
		}
	}

	public event EventHandler Alert;

	public event EventHandler Resume;

	public event GetValue GetValueEvent;

	public event SetValue SetValueEvent;

	public event GetDataBase GetDataBaseEvent;

	public event GetVarTable GetVarTableEvent;

	public event GetValue GetSystemItemEvent;

	public MainControl()
	{
		InitializeComponent();
		MainControl_Load(this, null);
	}

	private void client_VariableAlarmConfirm(object sender, EventArgs e)
	{
		VariableAlarmConfirmEventArgs variableAlarmConfirmEventArgs = (VariableAlarmConfirmEventArgs)e;
		if (Cbx1.SelectedIndex == 0)
		{
			Dg1.Focus();
			foreach (DataGridViewRow item in (IEnumerable)Dg1.Rows)
			{
				if (!(item.Cells["报警状态"].Value.ToString() == "报警") && !(item.Cells["报警状态"].Value.ToString() == "恢复"))
				{
					continue;
				}
				foreach (DataRow row in m_RealTimeVarDt.Rows)
				{
					if (row["报警ID"].ToString() == variableAlarmConfirmEventArgs.id && row["报警状态"].ToString() != "确认")
					{
						row["报警状态"] = "确认";
						if (m_RealTimeVarDt.Columns.Contains("确认时间"))
						{
							row["确认时间"] = variableAlarmConfirmEventArgs.confirmTime;
						}
						if (m_RealTimeVarDt.Columns.Contains("确认人"))
						{
							row["确认人"] = variableAlarmConfirmEventArgs.confirmUser;
						}
						Dg1.Invalidate();
						ShowAlertCount();
					}
				}
			}
			Dg1.DataSource = m_RealTimeVarDt;
			return;
		}
		foreach (DataRow row2 in m_RealTimeVarDt.Rows)
		{
			if (row2["报警ID"].ToString() == variableAlarmConfirmEventArgs.id && row2["报警状态"].ToString() != "确认")
			{
				row2["报警状态"] = "确认";
				if (m_RealTimeVarDt.Columns.Contains("确认时间"))
				{
					row2["确认时间"] = variableAlarmConfirmEventArgs.confirmTime;
				}
				if (m_RealTimeVarDt.Columns.Contains("确认人"))
				{
					row2["确认人"] = variableAlarmConfirmEventArgs.confirmUser;
				}
				Dg1.Invalidate();
			}
		}
	}

	private void client_VariForAlarm(object sender, EventArgs e)
	{
		if (base.InvokeRequired)
		{
			Invoke(new EventHandler(client_VariForAlarm), sender, e);
			return;
		}
		VarForAlarmEventArgs varForAlarmEventArgs = (VarForAlarmEventArgs)e;
		if (!isRuning)
		{
			return;
		}
		string text = "";
		foreach (AlertInfor varAlertInfor in m_VarAlertInforList)
		{
			if (varAlertInfor.M_VarID != varForAlarmEventArgs.AlarmVar)
			{
				continue;
			}
			_ = varAlertInfor.M_VarName;
			varAlertInfor.M_Value_Str = varForAlarmEventArgs.VarValue;
			if ((varForAlarmEventArgs.lAlarmType & 0xFF000000u) >> 24 == 0)
			{
				switch (varForAlarmEventArgs.lAlarmType & 0xFF)
				{
				case 0:
					text = "位报警";
					break;
				case 1:
					text = "下下限报警";
					break;
				case 2:
					text = "下限报警";
					break;
				case 3:
					text = "上限报警";
					break;
				case 4:
					text = "上上限报警";
					break;
				case 5:
					text = "目标报警";
					break;
				case 6:
					text = "变化率报警";
					break;
				}
				if (text == "位报警")
				{
					switch ((varForAlarmEventArgs.lAlarmType & 0xFF00) >> 8)
					{
					case 1:
						text = "开时报警";
						break;
					case 2:
						text = "关时报警";
						break;
					case 3:
						text = "开到关时报警";
						break;
					case 4:
						text = "关到开时报警";
						break;
					case 5:
						text = "变化就报警";
						break;
					}
				}
				varAlertInfor.M_AlertType = "报警";
				varAlertInfor.AlertID = varForAlarmEventArgs.id;
				ShowAlert(text, varAlertInfor);
				if (this.Alert != null)
				{
					this.Alert(this, null);
				}
				if (m_IsPlaySound)
				{
					m_SoundPlayer.PlayWithTimes(m_PlayTimes);
				}
				break;
			}
			switch (varForAlarmEventArgs.lAlarmType & 0xFF)
			{
			case 0:
				text = "位报警恢复";
				break;
			case 1:
				text = "下下限报警恢复";
				break;
			case 2:
				text = "下限报警恢复";
				break;
			case 3:
				text = "上限报警恢复";
				break;
			case 4:
				text = "上上限报警恢复";
				break;
			case 5:
				text = "目标限报警恢复";
				break;
			case 6:
				text = "变化率限报警恢复";
				break;
			}
			if (text == "位报警恢复")
			{
				switch ((varForAlarmEventArgs.lAlarmType & 0xFF00) >> 8)
				{
				case 1:
					text = "开报警恢复";
					break;
				case 2:
					text = "关报警恢复";
					break;
				case 3:
					text = "开到关报警恢复";
					break;
				case 4:
					text = "关到开报警恢复";
					break;
				case 5:
					text = "变化报警恢复";
					break;
				}
			}
			varAlertInfor.M_AlertType = "恢复";
			varAlertInfor.AlertID = varForAlarmEventArgs.id;
			if (m_RealTimeShowModel != 2)
			{
				ShowAlert(text, varAlertInfor);
			}
			if (this.Resume != null)
			{
				this.Resume(this, null);
			}
			ShowAlertCount();
			if (m_IsPlaySound)
			{
				m_SoundPlayer.PlayWithTimes(m_PlayTimes);
			}
			break;
		}
	}

	private void client_DeviceAlarmEvent(object sender, DeviceAlarmEventArgs e)
	{
	}

	public byte[] Serialize()
	{
		try
		{
			IFormatter formatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			SerializeCopy serializeCopy = new SerializeCopy();
			serializeCopy.M_SER_IsShowMessageBar = m_IsShowMessageBar;
			serializeCopy.M_SER_IsShowOperationBar = m_IsShowOperationBar;
			serializeCopy.M_SER_TableHeadBackColor = m_TableHeadBackColor;
			serializeCopy.M_SER_TableHeadTextColor = m_TableHeadTextColor;
			serializeCopy.M_SER_WindowBackColor = m_WindowBackColor;
			serializeCopy.M_SER_SelectedColumnList = m_SelectedColumnList;
			serializeCopy.M_SER_IsPlaySound = m_IsPlaySound;
			serializeCopy.M_SER_UseCustomAlarmWav = m_UseCustomAlarmWav;
			serializeCopy.M_SER_SortType = m_SortType;
			serializeCopy.M_SER_FirstColumnSort = m_FirstColumnSort;
			serializeCopy.M_SER_VarHasSelectecList = m_VarHasSelectedList;
			serializeCopy.M_SER_RealTimeVarDt = m_RealTimeVarDt;
			serializeCopy.M_SER_RealTime = m_RealTime;
			serializeCopy.M_SER_History = m_HistoryColumns;
			serializeCopy.M_SER_ShowType = m_ShowType;
			serializeCopy.M_SER_VarAlertInforlst = m_VarAlertInforList;
			serializeCopy.ProName = ProName;
			serializeCopy.ProFilePath = ProFilePath;
			formatter.Serialize(memoryStream, serializeCopy);
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			return result;
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警控件序列化错误！" + ex.ToString());
			return null;
		}
	}

	public void DeSerialize(byte[] bytes)
	{
		try
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream(bytes);
			SerializeCopy serializeCopy = (SerializeCopy)formatter.Deserialize(stream);
			stream.Close();
			m_IsShowMessageBar = serializeCopy.M_SER_IsShowMessageBar;
			m_IsShowOperationBar = serializeCopy.M_SER_IsShowOperationBar;
			m_TableHeadBackColor = serializeCopy.M_SER_TableHeadBackColor;
			m_TableHeadTextColor = serializeCopy.M_SER_TableHeadTextColor;
			m_WindowBackColor = serializeCopy.M_SER_WindowBackColor;
			m_SelectedColumnList = serializeCopy.M_SER_SelectedColumnList;
			m_SortType = serializeCopy.M_SER_SortType;
			m_FirstColumnSort = serializeCopy.M_SER_FirstColumnSort;
			m_VarHasSelectedList = serializeCopy.M_SER_VarHasSelectecList;
			m_RealTimeVarDt = serializeCopy.M_SER_RealTimeVarDt;
			m_RealTime = serializeCopy.M_SER_RealTime;
			m_HistoryColumns = serializeCopy.M_SER_History;
			m_ShowType = serializeCopy.M_SER_ShowType;
			ProFilePath = serializeCopy.ProFilePath;
			ProName = serializeCopy.ProName;
			m_IsPlaySound = serializeCopy.M_SER_IsPlaySound;
			m_VarAlertInforList = serializeCopy.M_SER_VarAlertInforlst;
			Dg1.BackgroundColor = m_WindowBackColor;
			splitContainer1.BackColor = m_WindowBackColor;
			Dg1.EnableHeadersVisualStyles = false;
			Dg1.ColumnHeadersDefaultCellStyle.BackColor = m_TableHeadBackColor;
			Dg1.ColumnHeadersDefaultCellStyle.ForeColor = m_TableHeadTextColor;
			m_UseCustomAlarmWav = serializeCopy.M_SER_UseCustomAlarmWav;
			if (!m_UseCustomAlarmWav)
			{
				return;
			}
			string text = "Resources\\alarm.wav";
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			if (baseDirectory.StartsWith("http"))
			{
				if (!m_SoundPlayer.SetSoundLocation(baseDirectory + text))
				{
					m_SoundPlayer.SetSoundLocation(null);
					m_UseCustomAlarmWav = false;
				}
				return;
			}
			string text2 = ProFilePath + "\\" + text;
			if (File.Exists(text2))
			{
				m_SoundPlayer.SetSoundLocation(text2);
				return;
			}
			if (File.Exists(text))
			{
				m_SoundPlayer.SetSoundLocation(text);
				return;
			}
			m_SoundPlayer.SetSoundLocation(null);
			m_UseCustomAlarmWav = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警控件反序列化错误！" + ex.ToString());
		}
	}

	private void ExtendAlertInforList()
	{
		XmlDocument xmlDocument = new XmlDocument();
		string text = (ProFilePath = this.GetVarTableEvent("ProjectPath").ToString());
		text = text.Substring(0, text.IndexOf("\\hmi"));
		ProName = text.Substring(text.LastIndexOf("\\"), text.Length - text.LastIndexOf("\\"));
		m_VarPath = text + "\\变量表.var";
		xmlDocument.Load(m_VarPath);
		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Item");
		foreach (AlertInfor varAlertInfor in m_VarAlertInforList)
		{
			foreach (XmlNode item in xmlNodeList)
			{
				if (varAlertInfor.M_VarName == ((XmlElement)item).GetAttribute("Name").ToString())
				{
					switch (((XmlElement)item).GetAttribute("ValType"))
					{
					case "0":
						varAlertInfor.M_Var_Type = "位变量(bit)";
						break;
					case "1":
						varAlertInfor.M_Var_Type = "有符号字节(char)";
						break;
					case "2":
						varAlertInfor.M_Var_Type = "无符号字节(byte)";
						break;
					case "3":
						varAlertInfor.M_Var_Type = "有符号字(short)";
						break;
					case "4":
						varAlertInfor.M_Var_Type = "无符号字(unsigned short)";
						break;
					case "5":
						varAlertInfor.M_Var_Type = "有符号双字(int32)";
						break;
					case "6":
						varAlertInfor.M_Var_Type = "无符号双字(ussigned int32)";
						break;
					case "7":
						varAlertInfor.M_Var_Type = "单精度浮点(float)";
						break;
					}
					varAlertInfor.M_Alerthigh = Convert.ToSingle(((XmlElement)item.ChildNodes[1]).GetAttribute("HighValue"));
					varAlertInfor.M_AlertTop = Convert.ToSingle(((XmlElement)item.ChildNodes[1]).GetAttribute("HihiValue"));
					varAlertInfor.M_AlertLow = Convert.ToSingle(((XmlElement)item.ChildNodes[1]).GetAttribute("LowValue"));
					varAlertInfor.M_AlertBottom = Convert.ToSingle(((XmlElement)item.ChildNodes[1]).GetAttribute("LoloValue"));
					switch (((XmlElement)item.ChildNodes[1]).GetAttribute("AlarmType").ToString())
					{
					case "1":
						varAlertInfor.M_ByteAlertType = "开时报警";
						break;
					case "2":
						varAlertInfor.M_ByteAlertType = "关时报警";
						break;
					case "3":
						varAlertInfor.M_ByteAlertType = "开到关时时报警";
						break;
					case "4":
						varAlertInfor.M_ByteAlertType = "关到开时时报警";
						break;
					case "5":
						varAlertInfor.M_ByteAlertType = "变化就报警";
						break;
					default:
						varAlertInfor.M_ByteAlertType = "关";
						break;
					}
					varAlertInfor.M_TargetAlert = Convert.ToSingle(((XmlElement)item.ChildNodes[1]).GetAttribute("AimValue"));
					if (((XmlElement)item.ChildNodes[1]).GetAttribute("ShiftActive").ToString() == "1")
					{
						varAlertInfor.M_ChangeAlert = "开";
					}
					else
					{
						varAlertInfor.M_ChangeAlert = "关";
					}
				}
			}
		}
	}

	public Bitmap GetLogo()
	{
		return Resources.alarm_img;
	}

	public static Image GetLogoStatic()
	{
		return Resources.alarm_img;
	}

	public void Stop()
	{
	}

	private void Btn_Output_Click(object sender, EventArgs e)
	{
		PrintDGV.Print_DataGridView(Dg1);
	}

	private void Btn_Note_Click(object sender, EventArgs e)
	{
		if (!isRuning || Dg1.SelectedRows.Count == 0)
		{
			return;
		}
		foreach (DataGridViewRow selectedRow in Dg1.SelectedRows)
		{
			foreach (DataRow row in m_RealTimeVarDt.Rows)
			{
				if (row["报警ID"].ToString() == selectedRow.Cells["报警ID"].Value.ToString() && row["报警状态"].ToString() != "确认")
				{
					client.ConfirmAlarm(row["报警ID"].ToString(), DateTime.Now.ToString(), "");
					if (m_RealTimeVarDt.Columns.Contains("报警状态"))
					{
						row["报警状态"] = "确认";
					}
					if (m_RealTimeVarDt.Columns.Contains("确认时间"))
					{
						row["确认时间"] = DateTime.Now.ToString();
					}
				}
			}
		}
		if (m_IsStopSound)
		{
			bool flag = false;
			foreach (DataRow row2 in m_RealTimeVarDt.Rows)
			{
				if (row2["报警状态"].ToString() == "报警")
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				m_SoundPlayer.Stop();
			}
		}
		ShowAlertCount();
	}

	private void Btn_Allnote_Click(object sender, EventArgs e)
	{
		if (!isRuning)
		{
			return;
		}
		foreach (DataGridViewRow item in (IEnumerable)Dg1.Rows)
		{
			if (!(item.Cells["报警状态"].Value.ToString() == "报警") && !(item.Cells["报警状态"].Value.ToString() == "恢复"))
			{
				continue;
			}
			foreach (DataRow row in m_RealTimeVarDt.Rows)
			{
				if (row["报警状态"].ToString() != "确认")
				{
					client.ConfirmAlarm(row["报警ID"].ToString(), DateTime.Now.ToString(), "");
					if (m_RealTimeVarDt.Columns.Contains("确认时间") && row["报警状态"].ToString() != "确认")
					{
						row["确认时间"] = DateTime.Now.ToString();
					}
					if (m_RealTimeVarDt.Columns.Contains("报警状态") && row["报警状态"].ToString() != "确认")
					{
						row["报警状态"] = "确认";
					}
				}
			}
		}
		if (m_IsStopSound)
		{
			m_SoundPlayer.Stop();
		}
		ShowAlertCount();
	}

	private void Btn_slc_Click(object sender, EventArgs e)
	{
		m_SoundPlayer.Stop();
	}

	private void Datagridview1_DoubleClick(object sender, EventArgs e)
	{
		try
		{
			if (!isRuning)
			{
				string text = (ProFilePath = this.GetVarTableEvent("ProjectPath").ToString());
				text = text.Substring(0, text.IndexOf("\\hmi"));
				ProName = text.Substring(text.LastIndexOf("\\"), text.Length - text.LastIndexOf("\\"));
				m_VarPath = text + "\\变量表.var";
				if (((MouseEventArgs)e).Button == MouseButtons.Right)
				{
					return;
				}
				ControlSettings controlSettings = new ControlSettings(this);
				controlSettings.M_VS_TableHeadBackColor = m_TableHeadBackColor;
				controlSettings.M_VS_TableHeadTextColor = m_TableHeadTextColor;
				controlSettings.M_VS_WindowBackColor = m_WindowBackColor;
				controlSettings.M_VS_SortType = m_SortType;
				controlSettings.M_VS_FirstColumnSort = m_FirstColumnSort;
				controlSettings.M_VS_IsPlaySound = m_IsPlaySound;
				controlSettings.M_VS_UseCustomAlarmWav = m_UseCustomAlarmWav;
				controlSettings.M_VS_PlayTimes = m_PlayTimes;
				controlSettings.M_VS_IsStopSound = m_IsStopSound;
				controlSettings.M_VS_ShowType = m_ShowType;
				controlSettings.M_VS_IsShowOperationBar = m_IsShowOperationBar;
				controlSettings.M_VS_IsShowMessageBar = m_IsShowMessageBar;
				for (int i = 0; i < Dg1.Columns.Count; i++)
				{
					if (Dg1.Columns[i].Visible)
					{
						controlSettings.M_VS_RealTimeSelectedColumn.Add(Dg1.Columns[i].Name);
					}
				}
				if (controlSettings.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				ExtendAlertInforList();
				m_TableHeadBackColor = controlSettings.M_VS_TableHeadBackColor;
				m_TableHeadTextColor = controlSettings.M_VS_TableHeadTextColor;
				m_WindowBackColor = controlSettings.M_VS_WindowBackColor;
				m_SortType = controlSettings.M_VS_SortType;
				m_FirstColumnSort = controlSettings.M_VS_FirstColumnSort;
				m_IsPlaySound = controlSettings.M_VS_IsPlaySound;
				m_PlayTimes = controlSettings.M_VS_PlayTimes;
				m_IsStopSound = controlSettings.M_VS_IsStopSound;
				m_UseCustomAlarmWav = controlSettings.M_VS_UseCustomAlarmWav;
				m_IsShowOperationBar = controlSettings.M_VS_IsShowOperationBar;
				m_IsShowMessageBar = controlSettings.M_VS_IsShowMessageBar;
				m_ShowType = controlSettings.M_VS_ShowType;
				Dg1.BackgroundColor = m_WindowBackColor;
				splitContainer1.BackColor = m_WindowBackColor;
				splitContainer2.Panel2.BackColor = m_WindowBackColor;
				Dg1.EnableHeadersVisualStyles = false;
				Dg1.ColumnHeadersDefaultCellStyle.BackColor = m_TableHeadBackColor;
				Dg1.ColumnHeadersDefaultCellStyle.ForeColor = m_TableHeadTextColor;
				Dg1.ColumnHeadersHeight = 18;
				if (m_IsShowOperationBar)
				{
					splitContainer1.Panel1Collapsed = false;
				}
				else
				{
					splitContainer1.Panel1Collapsed = true;
				}
				if (m_IsShowMessageBar)
				{
					splitContainer2.Panel2Collapsed = false;
				}
				else
				{
					splitContainer2.Panel2Collapsed = true;
				}
				m_RealTimeVarDt = new DataTable();
				m_RealTimeVarDt.Columns.Clear();
				m_RealTimeVarDt.Columns.Add("变量");
				m_RealTimeVarDt.Columns.Add("报警时间");
				m_RealTimeVarDt.Columns.Add("报警信息");
				m_RealTimeVarDt.Columns.Add("变量值");
				m_RealTimeVarDt.Columns.Add("确认时间");
				m_RealTimeVarDt.Columns.Add("报警类型");
				m_RealTimeVarDt.Columns.Add("变量类型");
				m_RealTimeVarDt.Columns.Add("变量报警上限");
				m_RealTimeVarDt.Columns.Add("变量报警上上限");
				m_RealTimeVarDt.Columns.Add("变量报警下限");
				m_RealTimeVarDt.Columns.Add("变量报警下下限");
				m_RealTimeVarDt.Columns.Add("位报警");
				m_RealTimeVarDt.Columns.Add("目标报警");
				m_RealTimeVarDt.Columns.Add("变化率报警");
				m_RealTimeVarDt.Columns.Add("报警ID");
				m_RealTimeVarDt.Columns.Add("确认人");
				m_RealTimeVarDt.Columns.Add("报警状态");
				m_RealTimeVarDt.Rows.Clear();
				foreach (AlertInfor varAlertInfor in m_VarAlertInforList)
				{
					DataRow dataRow = m_RealTimeVarDt.NewRow();
					dataRow["变量"] = varAlertInfor.M_VarName;
					if (dataRow != null)
					{
						m_RealTimeVarDt.Rows.Add(dataRow);
					}
				}
				Dg1.DataSource = m_RealTimeVarDt;
				for (int j = 0; j < Dg1.Columns.Count; j++)
				{
					Dg1.Columns[j].Visible = false;
				}
				if (m_RealTime != null)
				{
					m_RealTime.Clear();
					m_RealTime = controlSettings.M_VS_RealTimeSelectedColumn;
				}
				foreach (string item in controlSettings.M_VS_RealTimeSelectedColumn)
				{
					Dg1.Columns[item].Visible = true;
				}
				{
					foreach (DataGridViewColumn column in Dg1.Columns)
					{
						column.MinimumWidth = 120;
					}
					return;
				}
			}
			if (Cbx1.SelectedIndex != 0 || Dg1.SelectedRows.Count == 0)
			{
				return;
			}
			foreach (DataGridViewRow selectedRow in Dg1.SelectedRows)
			{
				foreach (DataRow row in m_RealTimeVarDt.Rows)
				{
					if (row["报警ID"].ToString() == selectedRow.Cells["报警ID"].Value.ToString() && row["报警状态"].ToString() != "确认")
					{
						client.ConfirmAlarm(row["报警ID"].ToString(), DateTime.Now.ToString(), "");
						if (m_RealTimeVarDt.Columns.Contains("报警状态"))
						{
							row["报警状态"] = "确认";
						}
						if (m_RealTimeVarDt.Columns.Contains("确认时间"))
						{
							row["确认时间"] = DateTime.Now.ToString();
						}
					}
				}
			}
			if (m_IsStopSound)
			{
				bool flag = false;
				foreach (DataRow row2 in m_RealTimeVarDt.Rows)
				{
					if (row2["报警状态"].ToString() == "报警")
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					m_SoundPlayer.Stop();
				}
			}
			ShowAlertCount();
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警控件双击表格错误！" + ex.ToString());
		}
	}

	public object FireGetVarTableEvent(string str)
	{
		if (this.GetVarTableEvent != null)
		{
			return this.GetVarTableEvent(str);
		}
		return null;
	}

	private void MainControl_Load(object sender, EventArgs e)
	{
		try
		{
			if (Thread.CurrentThread.CurrentCulture.Name != "zh-CN")
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("zh-CN");
			}
			helpProvider.HelpNamespace = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Doc\\DView使用说明.chm";
			Cbx1.SelectedIndex = 0;
			pn_history.Visible = false;
			m_DtpFlag = true;
			dtp_start.Value = DateTime.Now.AddDays(-1.0);
			dtp_end.Value = DateTime.Now.AddDays(1.0);
			m_DtpFlag = false;
			Cbx1.Enabled = false;
			Btn_note.Enabled = false;
			Btn_allnote.Enabled = false;
			Btn_slc.Enabled = false;
			Btn_output.Enabled = false;
			if (m_RealTimeVarDt.Columns.Count == 0)
			{
				m_RealTimeVarDt.Columns.Clear();
				m_RealTimeVarDt.Columns.Add("变量");
				m_RealTimeVarDt.Columns.Add("变量值");
				m_RealTimeVarDt.Columns.Add("报警时间");
				m_RealTimeVarDt.Columns.Add("报警信息");
				m_RealTimeVarDt.Columns.Add("确认时间");
				m_RealTimeVarDt.Columns.Add("报警类型");
				m_RealTimeVarDt.Columns.Add("变量类型");
				m_RealTimeVarDt.Columns.Add("变量报警上限");
				m_RealTimeVarDt.Columns.Add("变量报警上上限");
				m_RealTimeVarDt.Columns.Add("变量报警下限");
				m_RealTimeVarDt.Columns.Add("变量报警下下限");
				m_RealTimeVarDt.Columns.Add("位报警");
				m_RealTimeVarDt.Columns.Add("目标报警");
				m_RealTimeVarDt.Columns.Add("变化率报警");
				m_RealTimeVarDt.Columns.Add("报警ID");
				m_RealTimeVarDt.Columns.Add("确认人");
				m_RealTimeVarDt.Columns.Add("报警状态");
			}
			Dg1.DataSource = m_RealTimeVarDt;
			if (m_RealTime != null)
			{
				for (int i = 0; i < Dg1.Columns.Count; i++)
				{
					Dg1.Columns[i].Visible = false;
				}
				foreach (string item in m_RealTime)
				{
					Dg1.Columns[item].Visible = true;
				}
			}
			foreach (DataGridViewColumn column in Dg1.Columns)
			{
				column.MinimumWidth = 120;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警控件载入时发生错误！" + ex.ToString());
		}
	}

	private void RefleshDataTable()
	{
		if (!isRuning)
		{
			return;
		}
		if (Cbx1.SelectedIndex == 0)
		{
			Dg1.DataSource = m_RealTimeVarDt;
			pn_history.Visible = false;
			pn_his.Visible = false;
			label_connect.Visible = false;
			Btn_note.Visible = true;
			Btn_allnote.Visible = true;
			Btn_slc.Visible = true;
			for (int i = 0; i < Dg1.Columns.Count; i++)
			{
				Dg1.Columns[i].Visible = false;
			}
			{
				foreach (string item in m_RealTime)
				{
					Dg1.Columns[item].Visible = true;
				}
				return;
			}
		}
		if (Cbx1.SelectedIndex != 1)
		{
			return;
		}
		label_connect.Visible = false;
		pn_his.Visible = true;
		Btn_note.Visible = true;
		Btn_allnote.Visible = true;
		Btn_slc.Visible = true;
		ShowHistoryAlarm();
		pn_history.Visible = true;
		label_time.Text = dtp_start.Value.Date.ToString().Substring(0, dtp_start.Value.Date.ToString().IndexOf(' ')) + "至" + dtp_end.Value.Date.ToString().Substring(0, dtp_end.Value.Date.ToString().IndexOf(' '));
		label_result.Text = Dg1.Rows.Count + "条";
		for (int j = 0; j < Dg1.Columns.Count; j++)
		{
			Dg1.Columns[j].Visible = false;
		}
		if (m_HistoryColumns == null)
		{
			m_HistoryColumns = new List<string>();
			m_HistoryColumns.Add("报警ID");
			m_HistoryColumns.Add("变量");
			m_HistoryColumns.Add("报警时间");
			m_HistoryColumns.Add("报警信息");
			m_HistoryColumns.Add("变量值");
			m_HistoryColumns.Add("确认时间");
			m_HistoryColumns.Add("报警类型");
			m_HistoryColumns.Add("确认人");
			m_HistoryColumns.Add("上限报警");
			m_HistoryColumns.Add("上上限报警");
			m_HistoryColumns.Add("下限报警");
			m_HistoryColumns.Add("下下限报警");
			m_HistoryColumns.Add("位报警");
			m_HistoryColumns.Add("目标报警");
			m_HistoryColumns.Add("变化率报警");
		}
		foreach (string historyColumn in m_HistoryColumns)
		{
			try
			{
				Dg1.Columns[historyColumn].Visible = true;
			}
			catch (Exception)
			{
			}
		}
	}

	private void ShowAlert(string _alttype, AlertInfor _AlertInfo)
	{
		if (Cbx1.SelectedIndex == 0)
		{
			Dg1.DataSource = m_RealTimeVarDt;
		}
		DataRow dataRow = m_RealTimeVarDt.NewRow();
		if (m_RealTimeVarDt.Columns.Contains("变量"))
		{
			dataRow["变量"] = _AlertInfo.M_VarName;
			m_Throw_VarName = _AlertInfo.M_VarName;
		}
		if (m_RealTimeVarDt.Columns.Contains("报警类型"))
		{
			dataRow["报警类型"] = _alttype;
			m_Throw_AlertType = _alttype;
		}
		if (m_RealTimeVarDt.Columns.Contains("变量类型"))
		{
			dataRow["变量类型"] = _AlertInfo.M_Var_Type.ToString();
			m_Throw_VarType = _AlertInfo.M_Var_Type.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("变量报警上限"))
		{
			dataRow["变量报警上限"] = _AlertInfo.M_Alerthigh.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("变量报警上上限"))
		{
			dataRow["变量报警上上限"] = _AlertInfo.M_AlertTop.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("变量报警下限"))
		{
			dataRow["变量报警下限"] = _AlertInfo.M_AlertLow.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("变量报警下下限"))
		{
			dataRow["变量报警下下限"] = _AlertInfo.M_AlertBottom.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("位报警"))
		{
			dataRow["位报警"] = _AlertInfo.M_ByteAlertType.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("变化率报警"))
		{
			dataRow["变化率报警"] = _AlertInfo.M_ChangeAlert.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("目标报警"))
		{
			dataRow["目标报警"] = _AlertInfo.M_TargetAlert.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("报警状态"))
		{
			dataRow["报警状态"] = _AlertInfo.M_AlertType.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("报警时间"))
		{
			dataRow["报警时间"] = DateTime.Now.ToString();
			m_Throw_AlertTime = DateTime.Now.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("报警信息") && _AlertInfo.M_AlertMessage.ToString() != "双击编辑")
		{
			dataRow["报警信息"] = _AlertInfo.M_AlertMessage.ToString();
			m_Throw_AlertMsg = _AlertInfo.M_AlertMessage.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("报警ID"))
		{
			dataRow["报警ID"] = _AlertInfo.AlertID.ToString();
			m_Throw_AlertID = _AlertInfo.AlertID.ToString();
		}
		if (m_RealTimeVarDt.Columns.Contains("确认人"))
		{
			dataRow["确认人"] = "";
		}
		if (M_RealTimeVarDt.Columns.Contains("变量值"))
		{
			dataRow["变量值"] = _AlertInfo.M_Value_Str;
		}
		m_RealTimeVarDt.Rows.Add(dataRow);
		Lbl_msgvar.Text = _AlertInfo.M_VarName;
		if (_AlertInfo.M_AlertMessage != "双击编辑")
		{
			Lbl_msgaltmsg.Text = _AlertInfo.M_AlertMessage;
		}
		Lbl_msgall.Text = m_RealTimeVarDt.Rows.Count.ToString();
		ShowAlertCount();
		string text = "";
		text = ((m_SortType != 0) ? " DESC" : " ASC");
		m_RealTimeVarDt.DefaultView.Sort = m_FirstColumnSort + text;
		if (Cbx1.SelectedIndex != 0)
		{
			return;
		}
		for (int i = 0; i < Dg1.Columns.Count; i++)
		{
			Dg1.Columns[i].Visible = false;
		}
		foreach (string item in m_RealTime)
		{
			Dg1.Columns[item].Visible = true;
		}
	}

	private void ShowAlertCount()
	{
		int num = 0;
		foreach (DataRow row in m_RealTimeVarDt.Rows)
		{
			if (row["报警状态"].ToString() == "报警" || row["报警状态"].ToString() == "恢复")
			{
				num++;
			}
		}
		Lbl_msgalt.Text = num.ToString();
		m_AlertCount = num;
		int num2 = 0;
		foreach (DataRow row2 in m_RealTimeVarDt.Rows)
		{
			if (row2["报警状态"].ToString() == "确认")
			{
				num2++;
			}
		}
		Lbl_msgnote.Text = num2.ToString();
		m_NoteCount = num2;
	}

	private void Combobox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		RefleshDataTable();
	}

	private void Datagridview1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
	{
		if (e.RowIndex > Dg1.Rows.Count - 1)
		{
			return;
		}
		DataGridViewRow dataGridViewRow = (sender as DataGridView).Rows[e.RowIndex];
		string text = dataGridViewRow.Cells["变量"].Value.ToString();
		if (!((DataTable)Dg1.DataSource).Columns.Contains("报警状态"))
		{
			return;
		}
		if (dataGridViewRow.Cells["报警状态"].Value.ToString() == "报警")
		{
			foreach (AlertInfor varAlertInfor in m_VarAlertInforList)
			{
				if (varAlertInfor.M_VarName == text)
				{
					dataGridViewRow.DefaultCellStyle.ForeColor = varAlertInfor.M_AlttxtColor;
					dataGridViewRow.DefaultCellStyle.BackColor = varAlertInfor.M_AltbackColor;
					break;
				}
			}
		}
		if (dataGridViewRow.Cells["报警状态"].Value.ToString() == "确认")
		{
			foreach (AlertInfor varAlertInfor2 in m_VarAlertInforList)
			{
				if (varAlertInfor2.M_VarName == text)
				{
					dataGridViewRow.DefaultCellStyle.ForeColor = varAlertInfor2.M_NotetxtColor;
					dataGridViewRow.DefaultCellStyle.BackColor = varAlertInfor2.M_NotebackColor;
					break;
				}
			}
		}
		if (!(dataGridViewRow.Cells["报警状态"].Value.ToString() == "恢复"))
		{
			return;
		}
		foreach (AlertInfor varAlertInfor3 in m_VarAlertInforList)
		{
			if (varAlertInfor3.M_VarName == text)
			{
				dataGridViewRow.DefaultCellStyle.ForeColor = varAlertInfor3.M_RetxtColor;
				dataGridViewRow.DefaultCellStyle.BackColor = varAlertInfor3.M_RebackColor;
				break;
			}
		}
	}

	private void Datepicker_ValueChanged(object sender, EventArgs e)
	{
		if (!m_DtpFlag)
		{
			if (dtp_start.Value > dtp_end.Value)
			{
				MessageBox.Show("开始时间不能在结束时间之后！");
				return;
			}
			ShowHistoryAlarm();
			label_time.Text = dtp_start.Value.Date.ToString().Substring(0, dtp_start.Value.Date.ToString().IndexOf(' ')) + "至" + dtp_end.Value.Date.ToString().Substring(0, dtp_end.Value.Date.ToString().IndexOf(' '));
			label_result.Text = Dg1.Rows.Count + "条";
		}
	}

	private void ExportToExcel()
	{
	}

	private void SaveAs()
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
			for (int i = 0; i < Dg1.ColumnCount; i++)
			{
				if (i > 0)
				{
					text += "\t";
				}
				text += Dg1.Columns[i].HeaderText;
			}
			streamWriter.WriteLine(text);
			for (int j = 0; j < Dg1.Rows.Count; j++)
			{
				string text2 = "";
				for (int k = 0; k < Dg1.Columns.Count; k++)
				{
					if (k > 0)
					{
						text2 += "\t";
					}
					text2 += Dg1.Rows[j].Cells[k].Value.ToString();
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

	private void ShowHistoryAlarm()
	{
		try
		{
			DataTable dataTable = new DataTable();
			dataTable = client.GetHistoryAlert(dtp_start.Value.ToString(), dtp_end.Value.ToString());
			m_HisVarDt = new DataTable();
			m_HisVarDt.Columns.Add("变量");
			m_HisVarDt.Columns.Add("报警时间");
			m_HisVarDt.Columns.Add("报警信息");
			m_HisVarDt.Columns.Add("变量值");
			m_HisVarDt.Columns.Add("确认时间");
			m_HisVarDt.Columns.Add("报警类型");
			m_HisVarDt.Columns.Add("上限报警");
			m_HisVarDt.Columns.Add("上上限报警");
			m_HisVarDt.Columns.Add("下限报警");
			m_HisVarDt.Columns.Add("下下限报警");
			m_HisVarDt.Columns.Add("位报警");
			m_HisVarDt.Columns.Add("目标报警");
			m_HisVarDt.Columns.Add("变化率报警");
			m_HisVarDt.Columns.Add("报警ID");
			m_HisVarDt.Columns.Add("确认人");
			foreach (DataRow row in dataTable.Rows)
			{
				DataRow dataRow2 = m_HisVarDt.NewRow();
				string text = "";
				int num = int.Parse(row["报警类型"].ToString());
				if ((num & 0xFF000000u) >> 24 == 0)
				{
					switch (num & 0xFF)
					{
					case 0:
						text = "位报警";
						break;
					case 1:
						text = "下下限报警";
						break;
					case 2:
						text = "下限报警";
						break;
					case 3:
						text = "上限报警";
						break;
					case 4:
						text = "上上限报警";
						break;
					case 5:
						text = "目标报警";
						break;
					case 6:
						text = "变化率报警";
						break;
					}
					if (text == "位报警")
					{
						switch ((num & 0xFF00) >> 8)
						{
						case 1:
							text = "开时报警";
							break;
						case 2:
							text = "关时报警";
							break;
						case 3:
							text = "开到关时报警";
							break;
						case 4:
							text = "关到开时报警";
							break;
						case 5:
							text = "变化就报警";
							break;
						}
					}
				}
				else
				{
					switch (num & 0xFF)
					{
					case 0:
						text = "位报警恢复";
						break;
					case 1:
						text = "下下限报警恢复";
						break;
					case 2:
						text = "下限报警恢复";
						break;
					case 3:
						text = "上限报警恢复";
						break;
					case 4:
						text = "上上限报警恢复";
						break;
					case 5:
						text = "目标限报警恢复";
						break;
					case 6:
						text = "变化率限报警恢复";
						break;
					}
					if (text == "位报警恢复")
					{
						switch ((num & 0xFF00) >> 8)
						{
						case 1:
							text = "开报警恢复";
							break;
						case 2:
							text = "关报警恢复";
							break;
						case 3:
							text = "开到关报警恢复";
							break;
						case 4:
							text = "关到开报警恢复";
							break;
						case 5:
							text = "变化报警恢复";
							break;
						}
					}
				}
				bool flag = false;
				foreach (AlertInfor varAlertInfor in m_VarAlertInforList)
				{
					if (varAlertInfor.M_VarID != int.Parse(row["报警变量"].ToString()))
					{
						continue;
					}
					dataRow2["变量"] = varAlertInfor.M_VarName;
					if (m_HisVarDt.Columns.Contains("报警ID"))
					{
						dataRow2["报警ID"] = row["报警ID"].ToString();
					}
					if (m_HisVarDt.Columns.Contains("报警类型"))
					{
						dataRow2["报警类型"] = text;
					}
					if (m_HisVarDt.Columns.Contains("变量类型"))
					{
						dataRow2["变量类型"] = varAlertInfor.M_Var_Type.ToString();
					}
					if (m_HisVarDt.Columns.Contains("上限报警"))
					{
						dataRow2["上限报警"] = varAlertInfor.M_Alerthigh.ToString();
					}
					if (m_HisVarDt.Columns.Contains("上上限报警"))
					{
						dataRow2["上上限报警"] = varAlertInfor.M_AlertTop.ToString();
					}
					if (m_HisVarDt.Columns.Contains("下限报警"))
					{
						dataRow2["下限报警"] = varAlertInfor.M_AlertLow.ToString();
					}
					if (m_HisVarDt.Columns.Contains("下下限报警"))
					{
						dataRow2["下下限报警"] = varAlertInfor.M_AlertBottom.ToString();
					}
					if (m_HisVarDt.Columns.Contains("位报警"))
					{
						dataRow2["位报警"] = varAlertInfor.M_ByteAlertType.ToString();
					}
					if (m_HisVarDt.Columns.Contains("变化率报警"))
					{
						dataRow2["变化率报警"] = varAlertInfor.M_ChangeAlert.ToString();
					}
					if (m_HisVarDt.Columns.Contains("目标报警"))
					{
						dataRow2["目标报警"] = varAlertInfor.M_TargetAlert.ToString();
					}
					if (m_HisVarDt.Columns.Contains("报警状态"))
					{
						dataRow2["报警状态"] = varAlertInfor.M_AlertType.ToString();
					}
					if (m_HisVarDt.Columns.Contains("报警时间"))
					{
						dataRow2["报警时间"] = row["报警时间"].ToString();
					}
					if (m_HisVarDt.Columns.Contains("报警信息"))
					{
						if (varAlertInfor.M_AlertMessage.ToString() != "双击编辑")
						{
							dataRow2["报警信息"] = varAlertInfor.M_AlertMessage.ToString();
						}
						else
						{
							dataRow2["报警信息"] = "";
						}
					}
					if (m_HisVarDt.Columns.Contains("确认时间"))
					{
						dataRow2["确认时间"] = row["确认时间"].ToString();
					}
					if (m_HisVarDt.Columns.Contains("确认人"))
					{
						dataRow2["确认人"] = row["确认人"].ToString();
					}
					if (m_HisVarDt.Columns.Contains("变量值"))
					{
						dataRow2["变量值"] = row["报警值"].ToString();
					}
					flag = true;
					break;
				}
				if (flag)
				{
					m_HisVarDt.Rows.Add(dataRow2);
				}
			}
			m_HisVarDt.DefaultView.Sort = "报警时间 DESC";
			Dg1.DataSource = m_HisVarDt;
		}
		catch (NullReferenceException ex)
		{
			MessageBox.Show("报警控件配置信息不足，请先配置报警控件！" + ex.ToString());
		}
		catch (Exception ex2)
		{
			MessageBox.Show("报警控件ShowHistoryAlarm出错！" + ex2.ToString());
		}
	}

	private void 设置显示列ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (Cbx1.SelectedIndex == 0)
		{
			RealTimeColumn realTimeColumn = new RealTimeColumn(this);
			if (realTimeColumn.ShowDialog() == DialogResult.OK)
			{
				for (int i = 0; i < Dg1.Columns.Count; i++)
				{
					Dg1.Columns[i].Visible = false;
				}
				if (m_RealTime == null)
				{
					m_RealTime = new List<string>();
				}
				foreach (string item in m_RealTime)
				{
					Dg1.Columns[item].Visible = true;
				}
			}
		}
		if (Cbx1.SelectedIndex != 1)
		{
			return;
		}
		HistoryColumn historyColumn = new HistoryColumn(this);
		if (historyColumn.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		foreach (string historyColumn2 in m_HistoryColumns)
		{
			try
			{
				Dg1.Columns[historyColumn2].Visible = true;
			}
			catch (Exception)
			{
			}
		}
	}

	private void 清除当前显示数据ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!isRuning)
		{
			return;
		}
		try
		{
			if (Cbx1.SelectedIndex == 0)
			{
				button1_Click(sender, e);
			}
			_ = Cbx1.SelectedIndex;
			_ = 1;
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警控件清除数据错误！" + ex.ToString());
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		m_RealTimeVarDt.Rows.Clear();
		Dg1.DataSource = m_RealTimeVarDt;
		Lbl_msgall.Text = m_RealTimeVarDt.Rows.Count.ToString();
		ShowAlertCount();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		SaveAs();
	}

	private void MainControl_Load_1(object sender, EventArgs e)
	{
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
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.pn_history = new System.Windows.Forms.Panel();
		this.button2 = new System.Windows.Forms.Button();
		this.dtp_end = new System.Windows.Forms.DateTimePicker();
		this.dtp_start = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.label_connect = new System.Windows.Forms.Label();
		this.Btn_output = new System.Windows.Forms.Button();
		this.Btn_slc = new System.Windows.Forms.Button();
		this.Btn_note = new System.Windows.Forms.Button();
		this.Cbx1 = new System.Windows.Forms.ComboBox();
		this.Btn_allnote = new System.Windows.Forms.Button();
		this.pan_AlertMsg = new System.Windows.Forms.Panel();
		this.Lbl_msgaltmsg = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.Lbl_msgvar = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.Dg1 = new System.Windows.Forms.DataGridView();
		this.HistoryMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.设置显示列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.清除当前显示数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.pn_his = new System.Windows.Forms.Panel();
		this.label_result = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label_time = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.Lbl_msgnote = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.Lbl_msgalt = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.Lbl_msgall = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.helpProvider = new System.Windows.Forms.HelpProvider();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.pn_history.SuspendLayout();
		this.pan_AlertMsg.SuspendLayout();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Dg1).BeginInit();
		this.HistoryMenuStrip.SuspendLayout();
		this.pn_his.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.BackColor = System.Drawing.Color.White;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
		this.splitContainer1.Panel1.Controls.Add(this.pn_history);
		this.splitContainer1.Panel1.Controls.Add(this.button1);
		this.splitContainer1.Panel1.Controls.Add(this.label_connect);
		this.splitContainer1.Panel1.Controls.Add(this.Btn_output);
		this.splitContainer1.Panel1.Controls.Add(this.Btn_slc);
		this.splitContainer1.Panel1.Controls.Add(this.Btn_note);
		this.splitContainer1.Panel1.Controls.Add(this.Cbx1);
		this.splitContainer1.Panel1.Controls.Add(this.Btn_allnote);
		this.splitContainer1.Panel1.Controls.Add(this.pan_AlertMsg);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(729, 335);
		this.splitContainer1.SplitterDistance = 63;
		this.splitContainer1.TabIndex = 0;
		this.pn_history.Controls.Add(this.button2);
		this.pn_history.Controls.Add(this.dtp_end);
		this.pn_history.Controls.Add(this.dtp_start);
		this.pn_history.Controls.Add(this.label2);
		this.pn_history.Controls.Add(this.label1);
		this.pn_history.Location = new System.Drawing.Point(212, 2);
		this.pn_history.Name = "pn_history";
		this.pn_history.Size = new System.Drawing.Size(469, 30);
		this.pn_history.TabIndex = 2;
		this.button2.Location = new System.Drawing.Point(321, 4);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(76, 23);
		this.button2.TabIndex = 4;
		this.button2.Text = "导出Excel";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.dtp_end.Location = new System.Drawing.Point(195, 4);
		this.dtp_end.Name = "dtp_end";
		this.dtp_end.Size = new System.Drawing.Size(120, 21);
		this.dtp_end.TabIndex = 3;
		this.dtp_end.ValueChanged += new System.EventHandler(Datepicker_ValueChanged);
		this.dtp_start.Location = new System.Drawing.Point(39, 4);
		this.dtp_start.Name = "dtp_start";
		this.dtp_start.Size = new System.Drawing.Size(120, 21);
		this.dtp_start.TabIndex = 2;
		this.dtp_start.ValueChanged += new System.EventHandler(Datepicker_ValueChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(164, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "结束";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(4, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "开始";
		this.button1.Location = new System.Drawing.Point(416, 5);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(119, 23);
		this.button1.TabIndex = 13;
		this.button1.Text = "清除当前实时数据";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label_connect.AutoSize = true;
		this.label_connect.Location = new System.Drawing.Point(280, 11);
		this.label_connect.Name = "label_connect";
		this.label_connect.Size = new System.Drawing.Size(0, 12);
		this.label_connect.TabIndex = 7;
		this.Btn_output.Location = new System.Drawing.Point(133, 5);
		this.Btn_output.Name = "Btn_output";
		this.Btn_output.Size = new System.Drawing.Size(62, 23);
		this.Btn_output.TabIndex = 4;
		this.Btn_output.Text = "打印";
		this.Btn_output.UseVisualStyleBackColor = true;
		this.Btn_output.Click += new System.EventHandler(Btn_Output_Click);
		this.Btn_slc.Location = new System.Drawing.Point(348, 5);
		this.Btn_slc.Name = "Btn_slc";
		this.Btn_slc.Size = new System.Drawing.Size(62, 23);
		this.Btn_slc.TabIndex = 3;
		this.Btn_slc.Text = "消音";
		this.Btn_slc.UseVisualStyleBackColor = true;
		this.Btn_slc.Click += new System.EventHandler(Btn_slc_Click);
		this.Btn_note.Location = new System.Drawing.Point(212, 5);
		this.Btn_note.Name = "Btn_note";
		this.Btn_note.Size = new System.Drawing.Size(62, 23);
		this.Btn_note.TabIndex = 1;
		this.Btn_note.Text = "确认";
		this.Btn_note.UseVisualStyleBackColor = true;
		this.Btn_note.Click += new System.EventHandler(Btn_Note_Click);
		this.Cbx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbx1.FormattingEnabled = true;
		this.Cbx1.Items.AddRange(new object[2] { "实时报警", "历史报警" });
		this.Cbx1.Location = new System.Drawing.Point(10, 6);
		this.Cbx1.Name = "Cbx1";
		this.Cbx1.Size = new System.Drawing.Size(121, 20);
		this.Cbx1.TabIndex = 0;
		this.Cbx1.SelectedIndexChanged += new System.EventHandler(Combobox1_SelectedIndexChanged);
		this.Btn_allnote.Location = new System.Drawing.Point(280, 5);
		this.Btn_allnote.Name = "Btn_allnote";
		this.Btn_allnote.Size = new System.Drawing.Size(62, 23);
		this.Btn_allnote.TabIndex = 5;
		this.Btn_allnote.Text = "全确认";
		this.Btn_allnote.UseVisualStyleBackColor = true;
		this.Btn_allnote.Click += new System.EventHandler(Btn_Allnote_Click);
		this.pan_AlertMsg.Controls.Add(this.Lbl_msgaltmsg);
		this.pan_AlertMsg.Controls.Add(this.label5);
		this.pan_AlertMsg.Controls.Add(this.Lbl_msgvar);
		this.pan_AlertMsg.Controls.Add(this.label4);
		this.pan_AlertMsg.Location = new System.Drawing.Point(10, 34);
		this.pan_AlertMsg.Name = "pan_AlertMsg";
		this.pan_AlertMsg.Size = new System.Drawing.Size(716, 25);
		this.pan_AlertMsg.TabIndex = 12;
		this.Lbl_msgaltmsg.AutoSize = true;
		this.Lbl_msgaltmsg.Location = new System.Drawing.Point(317, 6);
		this.Lbl_msgaltmsg.Name = "Lbl_msgaltmsg";
		this.Lbl_msgaltmsg.Size = new System.Drawing.Size(17, 12);
		this.Lbl_msgaltmsg.TabIndex = 8;
		this.Lbl_msgaltmsg.Text = "无";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(241, 6);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(65, 12);
		this.label5.TabIndex = 7;
		this.label5.Text = "报警信息：";
		this.Lbl_msgvar.AutoSize = true;
		this.Lbl_msgvar.Location = new System.Drawing.Point(98, 5);
		this.Lbl_msgvar.Name = "Lbl_msgvar";
		this.Lbl_msgvar.Size = new System.Drawing.Size(17, 12);
		this.Lbl_msgvar.TabIndex = 6;
		this.Lbl_msgvar.Text = "无";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(9, 6);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(89, 12);
		this.label4.TabIndex = 5;
		this.label4.Text = "最新报警变量：";
		this.splitContainer2.BackColor = System.Drawing.Color.White;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.Dg1);
		this.splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
		this.splitContainer2.Panel2.Controls.Add(this.pn_his);
		this.splitContainer2.Panel2.Controls.Add(this.label12);
		this.splitContainer2.Panel2.Controls.Add(this.Lbl_msgnote);
		this.splitContainer2.Panel2.Controls.Add(this.label10);
		this.splitContainer2.Panel2.Controls.Add(this.Lbl_msgalt);
		this.splitContainer2.Panel2.Controls.Add(this.label9);
		this.splitContainer2.Panel2.Controls.Add(this.label8);
		this.splitContainer2.Panel2.Controls.Add(this.label7);
		this.splitContainer2.Panel2.Controls.Add(this.Lbl_msgall);
		this.splitContainer2.Panel2.Controls.Add(this.label6);
		this.splitContainer2.Size = new System.Drawing.Size(729, 268);
		this.splitContainer2.SplitterDistance = 239;
		this.splitContainer2.TabIndex = 3;
		this.Dg1.AllowUserToAddRows = false;
		this.Dg1.AllowUserToDeleteRows = false;
		this.Dg1.BackgroundColor = System.Drawing.Color.White;
		this.Dg1.ColumnHeadersHeight = 25;
		this.Dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.Dg1.ContextMenuStrip = this.HistoryMenuStrip;
		this.Dg1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Dg1.EnableHeadersVisualStyles = false;
		this.Dg1.Location = new System.Drawing.Point(0, 0);
		this.Dg1.MultiSelect = false;
		this.Dg1.Name = "Dg1";
		this.Dg1.ReadOnly = true;
		this.Dg1.RowHeadersVisible = false;
		this.Dg1.RowTemplate.Height = 23;
		this.Dg1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.Dg1.Size = new System.Drawing.Size(729, 239);
		this.Dg1.TabIndex = 1;
		this.Dg1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(Datagridview1_RowPrePaint);
		this.Dg1.DoubleClick += new System.EventHandler(Datagridview1_DoubleClick);
		this.helpProvider.SetHelpKeyword(this.HistoryMenuStrip, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this.HistoryMenuStrip, System.Windows.Forms.HelpNavigator.Topic);
		this.HistoryMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.设置显示列ToolStripMenuItem, this.清除当前显示数据ToolStripMenuItem });
		this.HistoryMenuStrip.Name = "HistoryMenuStrip";
		this.helpProvider.SetShowHelp(this.HistoryMenuStrip, true);
		this.HistoryMenuStrip.Size = new System.Drawing.Size(173, 48);
		this.设置显示列ToolStripMenuItem.Name = "设置显示列ToolStripMenuItem";
		this.设置显示列ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
		this.设置显示列ToolStripMenuItem.Text = "设置显示列";
		this.设置显示列ToolStripMenuItem.Click += new System.EventHandler(设置显示列ToolStripMenuItem_Click);
		this.清除当前显示数据ToolStripMenuItem.Name = "清除当前显示数据ToolStripMenuItem";
		this.清除当前显示数据ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
		this.清除当前显示数据ToolStripMenuItem.Text = "清除当前显示数据";
		this.清除当前显示数据ToolStripMenuItem.Click += new System.EventHandler(清除当前显示数据ToolStripMenuItem_Click);
		this.pn_his.Controls.Add(this.label_result);
		this.pn_his.Controls.Add(this.label13);
		this.pn_his.Controls.Add(this.label_time);
		this.pn_his.Controls.Add(this.label11);
		this.pn_his.Controls.Add(this.label3);
		this.pn_his.Location = new System.Drawing.Point(8, 1);
		this.pn_his.Name = "pn_his";
		this.pn_his.Size = new System.Drawing.Size(718, 24);
		this.pn_his.TabIndex = 14;
		this.label_result.AutoSize = true;
		this.label_result.Location = new System.Drawing.Point(542, 6);
		this.label_result.Name = "label_result";
		this.label_result.Size = new System.Drawing.Size(0, 12);
		this.label_result.TabIndex = 3;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(401, 6);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(65, 12);
		this.label13.TabIndex = 2;
		this.label13.Text = "结果条数：";
		this.label_time.AutoSize = true;
		this.label_time.Location = new System.Drawing.Point(85, 5);
		this.label_time.Name = "label_time";
		this.label_time.Size = new System.Drawing.Size(0, 12);
		this.label_time.TabIndex = 1;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(13, 6);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 12);
		this.label11.TabIndex = 0;
		this.label11.Text = "查询时间：";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(11, 6);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 0;
		this.label3.Text = "最新报警";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(547, 6);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(17, 12);
		this.label12.TabIndex = 13;
		this.label12.Text = "条";
		this.Lbl_msgnote.AutoSize = true;
		this.Lbl_msgnote.Location = new System.Drawing.Point(513, 6);
		this.Lbl_msgnote.Name = "Lbl_msgnote";
		this.Lbl_msgnote.Size = new System.Drawing.Size(11, 12);
		this.Lbl_msgnote.TabIndex = 12;
		this.Lbl_msgnote.Text = "0";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(433, 6);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(17, 12);
		this.label10.TabIndex = 11;
		this.label10.Text = "条";
		this.Lbl_msgalt.AutoSize = true;
		this.Lbl_msgalt.Location = new System.Drawing.Point(405, 6);
		this.Lbl_msgalt.Name = "Lbl_msgalt";
		this.Lbl_msgalt.Size = new System.Drawing.Size(11, 12);
		this.Lbl_msgalt.TabIndex = 10;
		this.Lbl_msgalt.Text = "0";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(463, 6);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(41, 12);
		this.label9.TabIndex = 9;
		this.label9.Text = "确认：";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(348, 6);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(41, 12);
		this.label8.TabIndex = 8;
		this.label8.Text = "报警：";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(314, 6);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(17, 12);
		this.label7.TabIndex = 7;
		this.label7.Text = "条";
		this.Lbl_msgall.AutoSize = true;
		this.Lbl_msgall.Location = new System.Drawing.Point(282, 6);
		this.Lbl_msgall.Name = "Lbl_msgall";
		this.Lbl_msgall.Size = new System.Drawing.Size(11, 12);
		this.Lbl_msgall.TabIndex = 6;
		this.Lbl_msgall.Text = "0";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(198, 6);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 5;
		this.label6.Text = "记录条数：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.splitContainer1);
		this.helpProvider.SetHelpKeyword(this, "14.2报警控件的使用.htm");
		this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
		base.Name = "MainControl";
		this.helpProvider.SetShowHelp(this, true);
		this.Size = new System.Drawing.Size(729, 335);
		base.Load += new System.EventHandler(MainControl_Load_1);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.pn_history.ResumeLayout(false);
		this.pn_history.PerformLayout();
		this.pan_AlertMsg.ResumeLayout(false);
		this.pan_AlertMsg.PerformLayout();
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.Panel2.PerformLayout();
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Dg1).EndInit();
		this.HistoryMenuStrip.ResumeLayout(false);
		this.pn_his.ResumeLayout(false);
		this.pn_his.PerformLayout();
		base.ResumeLayout(false);
	}
}
