using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace ShapeRuntime;

[Serializable]
public class HMIProjectFile
{
    [NonSerialized]
    public HmiPageGroup PageGroup;

    public bool dirtyCompile;

    public List<string> dirtyPage;

    public Dictionary<string, string> Pages;

    public List<string> startVisiblePages;

    public bool Compress;

    public DBCfgPara dbCfgPara = new("SqlClient Data Provider", "");

    public string DBConnSerAdd = "127.0.0.1";

    public string DBConnUid = "sa";

    public string DBConnPwd = "";

    public string DBConnDB = "";

    public string DBConnStr = "";

    [OptionalField]
    public bool RunEvnironment;

    [OptionalField]
    public bool autofull;

    [OptionalField]
    public bool exitneedpasswordLocal;

    [OptionalField]
    public bool exitneedpasswordNet;

    [OptionalField]
    public string exitpassword = "";

    public string key = "";

    [OptionalField]
    public string devjiaoben = "";

    [OptionalField]
    public string devLogic = "";

    [OptionalField]
    public bool userInDB;

    [OptionalField]
    public bool DefaultLoginDialog;

    [OptionalField]
    public bool CanAnonymous = true;

    [OptionalField]
    public string uifID = "";

    public HMIUser[] HMIUsers = new HMIUser[0];

    public UserType[] UserTypes = new UserType[0];

    public SafeRegions[] SafeRegions = new SafeRegions[0];

    [OptionalField]
    public List<GlobalScriptClass> GlobalScripts = new();

    [OptionalField]
    public Color ProjectBackColor = SystemColors.Control;

    [OptionalField]
    public string EnvironmentPath = "";

    [OptionalField]
    public string SetupPath = "";

    [OptionalField]
    public int InvalidateTime = 500;

    public Size ProjectSize = new(800, 600);

    public string password = "123";

    public string ProjectName = "NewProject";

    public DateTime CreatTime = DateTime.Now;

    public int RefreshTime = 50;

    public string IOfiles = "..\\变量表.var";

    [OptionalField]
    public string DTPfiles = "..\\设备拓扑.dtp";

    public string ProjectIOFiles = "";

    public string[] pagefiles;

    [OptionalField]
    public string ipaddress = "127.0.0.1";

    public string port = "80";

    public string databasename = "";

    public string databaseusername = "root";

    public string databasepwd = "";

    public short databaseport = 3306;

    public int NowProjectIOMaxID;

    public long NowShapeMaxID;

    public int LogicTime = 1000;

    public List<ParaIO> ParaIOs = new();

    [NonSerialized]
    public List<ProjectIO> ProjectIOs = new();

    public ProjectIO[] tprojectios;

    [NonSerialized]
    public List<CIOAlarm> IOAlarms = new();

    public CIOAlarm[] tioalarms;

    public TreeNode ProjectIOTreeRoot = new("内部变量");

    public string SrcGlobalLogic = "";

    public string DstGlobalLogic = "";

    public string gDXP_SleepTime = "0";

    public string _cxdzgbLogic = "";

    public string _cxdzyxLogic = "";

    public string _cxdzqdLogic = "";

    public string[] cxdzshijiaoben = new string[3] { "", "", "" };

    public string[] cxdzLogic = new string[3] { "", "", "" };

    public string[] cxdzqdioios = new string[0];

    public string[] cxdzqdiotjs = new string[0];

    public string[] cxdzqdiozhis = new string[0];

    public string[] cxdzqditemprogs = new string[0];

    public string[] cxdzqditemtjs = new string[0];

    public string[] cxdzqditemzhis = new string[0];

    public string[] cxdzqditemfftjs = new string[0];

    public string[] cxdzqditemffbdss = new string[0];

    public string[] cxdzyxioios = new string[0];

    public string[] cxdzyxiotjs = new string[0];

    public string[] cxdzyxiozhis = new string[0];

    public string[] cxdzyxitemprogs = new string[0];

    public string[] cxdzyxitemtjs = new string[0];

    public string[] cxdzyxitemzhis = new string[0];

    public string[] cxdzyxitemfftjs = new string[0];

    public string[] cxdzyxitemffbdss = new string[0];

    public string[] cxdzgbioios = new string[0];

    public string[] cxdzgbiotjs = new string[0];

    public string[] cxdzgbiozhis = new string[0];

    public string[] cxdzgbitemprogs = new string[0];

    public string[] cxdzgbitemtjs = new string[0];

    public string[] cxdzgbitemzhis = new string[0];

    public string[] cxdzgbitemfftjs = new string[0];

    public string[] cxdzgbitemffbdss = new string[0];

    public List<HmiPageFile> PageFiles
    {
        get
        {
            List<HmiPageFile> list = new();
            foreach (KeyValuePair<string, string> page in Pages)
            {
                list.Add(new HmiPageFile
                {
                    PageName = page.Key,
                    FileName = page.Value
                });
            }
            return list;
        }
    }

    public string cxdzgbLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (cxdzgbioios.Length != 0)
            {
                for (int i = 0; i < cxdzgbioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzgbiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzgbioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzgbiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzgbitemprogs.Length != 0)
            {
                for (int j = 0; j < cxdzgbitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzgbitemfftjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzgbitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzgbitemffbdss[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzgbitemffbdss.Length != 0)
            {
                for (int k = 0; k < cxdzgbitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzgbitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzgbitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + cxdzshijiaoben[2] != null)
            {
                return cxdzshijiaoben[2];
            }
            return "";
        }
    }

    public string cxdzyxLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (cxdzyxioios.Length != 0)
            {
                for (int i = 0; i < cxdzyxioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzyxiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzyxioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzyxiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzyxitemprogs.Length != 0)
            {
                for (int j = 0; j < cxdzyxitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzyxitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzyxitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzyxitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzyxitemffbdss.Length != 0)
            {
                for (int k = 0; k < cxdzyxitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzyxitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzyxitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + cxdzshijiaoben[1] != null)
            {
                return cxdzshijiaoben[1];
            }
            return "";
        }
    }

    public string cxdzqdLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (cxdzqdioios.Length != 0)
            {
                for (int i = 0; i < cxdzqdioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzqdiotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzqdioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzqdiozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzqditemprogs.Length != 0)
            {
                for (int j = 0; j < cxdzqditemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzqditemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzqditemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(cxdzqditemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (cxdzqditemffbdss.Length != 0)
            {
                for (int k = 0; k < cxdzqditemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(cxdzqditemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(cxdzqditemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (stringBuilder.ToString() + "\n" + cxdzshijiaoben[0] != null)
            {
                return cxdzshijiaoben[0];
            }
            return "";
        }
    }

    public void dirtyPageAdd(string name)
    {
        if (dirtyPage == null)
        {
            dirtyPage = new List<string>();
        }
        if (!dirtyPage.Contains(name))
        {
            dirtyPage.Add(name);
        }
    }
}
