using System;
using System.Drawing;

namespace Alert_Main;

[Serializable]
public class AlertInfor
{
	private int VarID;

	private string VarName = "";

	private string Var_Type = "";

	private double Alerthigh;

	private double AlertTop;

	private double AlertLow;

	private double AlertBottom;

	private string ByteAlertType = "";

	private double TargetAlert;

	private string ChangeAlert = "";

	private string AlertType = "";

	private string m_AlertID = "";

	private string AlertMessage = "";

	private Color AlttxtColor = Color.Red;

	private Color AltbackColor = Color.White;

	private Color NotetxtColor = Color.Blue;

	private Color NotebackColor = Color.White;

	private Color RetxtColor = Color.Green;

	private Color RebackColor = Color.White;

	public string Value_Str = "0";

	public int M_VarID
	{
		get
		{
			return VarID;
		}
		set
		{
			VarID = value;
		}
	}

	public string M_VarName
	{
		get
		{
			return VarName;
		}
		set
		{
			VarName = value;
		}
	}

	public string M_Var_Type
	{
		get
		{
			return Var_Type;
		}
		set
		{
			Var_Type = value;
		}
	}

	public double M_Alerthigh
	{
		get
		{
			return Alerthigh;
		}
		set
		{
			Alerthigh = value;
		}
	}

	public double M_AlertTop
	{
		get
		{
			return AlertTop;
		}
		set
		{
			AlertTop = value;
		}
	}

	public double M_AlertLow
	{
		get
		{
			return AlertLow;
		}
		set
		{
			AlertLow = value;
		}
	}

	public double M_AlertBottom
	{
		get
		{
			return AlertBottom;
		}
		set
		{
			AlertBottom = value;
		}
	}

	public string M_ByteAlertType
	{
		get
		{
			return ByteAlertType;
		}
		set
		{
			ByteAlertType = value;
		}
	}

	public double M_TargetAlert
	{
		get
		{
			return TargetAlert;
		}
		set
		{
			TargetAlert = value;
		}
	}

	public string M_ChangeAlert
	{
		get
		{
			return ChangeAlert;
		}
		set
		{
			ChangeAlert = value;
		}
	}

	public string M_AlertType
	{
		get
		{
			return AlertType;
		}
		set
		{
			AlertType = value;
		}
	}

	public string AlertID
	{
		get
		{
			return m_AlertID;
		}
		set
		{
			m_AlertID = value;
		}
	}

	public string M_AlertMessage
	{
		get
		{
			return AlertMessage;
		}
		set
		{
			AlertMessage = value;
		}
	}

	public Color M_AlttxtColor
	{
		get
		{
			return AlttxtColor;
		}
		set
		{
			AlttxtColor = value;
		}
	}

	public Color M_AltbackColor
	{
		get
		{
			return AltbackColor;
		}
		set
		{
			AltbackColor = value;
		}
	}

	public Color M_NotetxtColor
	{
		get
		{
			return NotetxtColor;
		}
		set
		{
			NotetxtColor = value;
		}
	}

	public Color M_NotebackColor
	{
		get
		{
			return NotebackColor;
		}
		set
		{
			NotebackColor = value;
		}
	}

	public Color M_RetxtColor
	{
		get
		{
			return RetxtColor;
		}
		set
		{
			RetxtColor = value;
		}
	}

	public Color M_RebackColor
	{
		get
		{
			return RebackColor;
		}
		set
		{
			RebackColor = value;
		}
	}

	public string M_Value_Str
	{
		get
		{
			return Value_Str;
		}
		set
		{
			Value_Str = value;
		}
	}
}
