using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace Alert_Main;

[Serializable]
public class SerializeCopy
{
	public string ProFilePath = "";

	private string m_SER_SoundFileName = "";

	private Color m_SER_TableHeadBackColor = Color.Silver;

	private Color m_SER_TableHeadTextColor = Color.White;

	private Color Windowbackcolor = Color.White;

	private List<string> m_SER_SelectedColumnList = new List<string>();

	private int m_SER_SortType;

	private string m_SER_FirstColumnSort = "";

	private string m_SER_SecondColumnSort = "";

	private DataTable m_SER_RealTimeVarDt;

	private List<string> m_SER_VarHasSelectecList = new List<string>();

	private DataTable m_SER_HisVarDt;

	private List<AlertInfor> m_SER_VarAlertInforlst = new List<AlertInfor>();

	private DataTable m_SER_MsgDt;

	private DataTable m_SER_MsgVarDt;

	private bool m_SER_IsPlaySound;

	private int m_SER_SoundType;

	public string ProName;

	private bool m_SER_IsShowOperationBar;

	private bool m_SER_IsShowMessageBar;

	private bool m_SER_UseCustomAlarmWav;

	private List<string> m_SER_RealTime = new List<string>();

	private List<string> m_SER_History = new List<string>();

	private List<bool> m_SER_HisTory = new List<bool>();

	public byte[] m_SER_SoundFileData;

	private int m_SER_ShowType;

	public bool M_SER_IsPlaySound
	{
		get
		{
			return m_SER_IsPlaySound;
		}
		set
		{
			m_SER_IsPlaySound = value;
		}
	}

	public bool M_SER_IsShowOperationBar
	{
		get
		{
			return m_SER_IsShowOperationBar;
		}
		set
		{
			m_SER_IsShowOperationBar = value;
		}
	}

	public bool M_SER_IsShowMessageBar
	{
		get
		{
			return m_SER_IsShowMessageBar;
		}
		set
		{
			m_SER_IsShowMessageBar = value;
		}
	}

	public bool M_SER_UseCustomAlarmWav
	{
		get
		{
			return m_SER_UseCustomAlarmWav;
		}
		set
		{
			m_SER_UseCustomAlarmWav = value;
		}
	}

	public List<string> M_SER_RealTime
	{
		get
		{
			return m_SER_RealTime;
		}
		set
		{
			m_SER_RealTime = value;
		}
	}

	public List<string> M_SER_History
	{
		get
		{
			return m_SER_History;
		}
		set
		{
			m_SER_History = value;
		}
	}

	public List<bool> M_SER_HisTory
	{
		get
		{
			return m_SER_HisTory;
		}
		set
		{
			m_SER_HisTory = value;
		}
	}

	public string M_SER_SoundFileName
	{
		get
		{
			return m_SER_SoundFileName;
		}
		set
		{
			m_SER_SoundFileName = value;
		}
	}

	public byte[] M_SER_SoundFileData
	{
		get
		{
			return m_SER_SoundFileData;
		}
		set
		{
			m_SER_SoundFileData = value;
		}
	}

	public Color M_SER_TableHeadBackColor
	{
		get
		{
			return m_SER_TableHeadBackColor;
		}
		set
		{
			m_SER_TableHeadBackColor = value;
		}
	}

	public Color M_SER_TableHeadTextColor
	{
		get
		{
			return m_SER_TableHeadTextColor;
		}
		set
		{
			m_SER_TableHeadTextColor = value;
		}
	}

	public Color M_SER_WindowBackColor
	{
		get
		{
			return Windowbackcolor;
		}
		set
		{
			Windowbackcolor = value;
		}
	}

	public List<string> M_SER_SelectedColumnList
	{
		get
		{
			return m_SER_SelectedColumnList;
		}
		set
		{
			m_SER_SelectedColumnList = value;
		}
	}

	public int M_SER_SortType
	{
		get
		{
			return m_SER_SortType;
		}
		set
		{
			m_SER_SortType = value;
		}
	}

	public string M_SER_FirstColumnSort
	{
		get
		{
			return m_SER_FirstColumnSort;
		}
		set
		{
			m_SER_FirstColumnSort = value;
		}
	}

	public string M_SER_SecondColumnSort
	{
		get
		{
			return m_SER_SecondColumnSort;
		}
		set
		{
			m_SER_SecondColumnSort = value;
		}
	}

	public List<string> M_SER_VarHasSelectecList
	{
		get
		{
			return m_SER_VarHasSelectecList;
		}
		set
		{
			m_SER_VarHasSelectecList = value;
		}
	}

	public DataTable M_SER_RealTimeVarDt
	{
		get
		{
			return m_SER_RealTimeVarDt;
		}
		set
		{
			m_SER_RealTimeVarDt = value;
		}
	}

	public DataTable M_SER_HisVarDt
	{
		get
		{
			return m_SER_HisVarDt;
		}
		set
		{
			m_SER_HisVarDt = value;
		}
	}

	public List<AlertInfor> M_SER_VarAlertInforlst
	{
		get
		{
			return m_SER_VarAlertInforlst;
		}
		set
		{
			m_SER_VarAlertInforlst = value;
		}
	}

	public int M_SER_SoundType
	{
		get
		{
			return m_SER_SoundType;
		}
		set
		{
			m_SER_SoundType = value;
		}
	}

	public DataTable M_SER_MsgDt
	{
		get
		{
			return m_SER_MsgDt;
		}
		set
		{
			m_SER_MsgDt = value;
		}
	}

	public DataTable M_SER_MsgVarDt
	{
		get
		{
			return m_SER_MsgVarDt;
		}
		set
		{
			m_SER_MsgVarDt = value;
		}
	}

	public int M_SER_ShowType
	{
		get
		{
			return m_SER_ShowType;
		}
		set
		{
			m_SER_ShowType = value;
		}
	}
}
