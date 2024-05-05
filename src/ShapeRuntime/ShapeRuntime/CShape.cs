using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CommonSnappableTypes;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

[Serializable]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Guid("A84314EE-2B42-434e-A48E-B6A79B9803D6")]
public class CShape : IDBAnimation, IEventBind, IPropertyBind, ISerializable
{
    public enum _BrushStyle
    {
        单色填充,
        线性渐变填充,
        渐变色填充,
        百分比填充,
        图案填充
    }

    public Dictionary<string, object> ShapeCfgFields = new();

    private bool enablePropertyBind;

    public DataTable propertyBindDT = new();

    public bool enableEventBind;

    public Dictionary<string, List<EventSetItem>> eventBindDict = new();

    public List<int> m_EventBindRegionList = new();

    public string DBOKScriptUser = "";

    public string DBErrScriptUser = "";

    public string DBOKScript = "";

    public string DBErrScript = "";

    [NonSerialized]
    public int dbResult = -1;

    public bool dbmultoperation;

    public List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimations = new();

    public bool newtable;

    public bool ansyncnewtable;

    public string newtableSQL = "";

    public byte[] newtableOtherData;

    public bool dbselect;

    public bool ansyncselect;

    public string dbselectSQL = "";

    public string dbselectTO = "";

    public byte[] dbselectOtherData;

    public bool dbinsert;

    public bool ansyncinsert;

    public string dbinsertSQL = "";

    public byte[] dbinsertOtherData;

    public bool dbupdate;

    public bool ansyncupdate;

    public string dbupdateSQL = "";

    public byte[] dbupdateOtherData;

    public bool dbdelete;

    public bool ansyncdelete;

    public string dbdeleteSQL = "";

    public byte[] dbdeleteOtherData;

    [NonSerialized]
    public object Tag;

    public HatchStyle hatchstyle;

    [NonSerialized]
    protected GraphicsPath swapgp;

    [NonSerialized]
    protected bool needRefreshShape;

    [NonSerialized]
    protected bool needRefreshBrush;

    public double DefaultAngle;

    public Point DefaultLocaion = default;

    public SizeF DefaultSize = default;

    public string[] ysdzshijianmingcheng = new string[0];

    public string[] ysdzshijianbiaodashi = new string[0];

    public string[] ysdzshijianLogic = new string[0];

    [OptionalField]
    public List<List<int>> UserRegionList;

    public string _sbsjldcLogic = "";

    public string _sbsjrdcLogic = "";

    public string _sbsjlcLogic = "";

    public string _sbsjrcLogic = "";

    public string _sbsjglLogic = "";

    public bool sbsj;

    public bool sbsjWhenOutThenDo;

    public string[] sbsjlcioios = new string[0];

    public string[] sbsjlciotjs = new string[0];

    public string[] sbsjlciozhis = new string[0];

    public string[] sbsjlcitemprogs = new string[0];

    public string[] sbsjlcitemtjs = new string[0];

    public string[] sbsjlcitemzhis = new string[0];

    public string[] sbsjlcitemfftjs = new string[0];

    public string[] sbsjlcitemffbdss = new string[0];

    public string[] sbsjlcpagevisibletrue = new string[0];

    public string[] sbsjlcpagevisiblefalse = new string[0];

    public string[] sbsjrcioios = new string[0];

    public string[] sbsjrciotjs = new string[0];

    public string[] sbsjrciozhis = new string[0];

    public string[] sbsjrcitemprogs = new string[0];

    public string[] sbsjrcitemtjs = new string[0];

    public string[] sbsjrcitemzhis = new string[0];

    public string[] sbsjrcitemfftjs = new string[0];

    public string[] sbsjrcitemffbdss = new string[0];

    public string[] sbsjrcpagevisibletrue = new string[0];

    public string[] sbsjrcpagevisiblefalse = new string[0];

    public string[] sbsjldcioios = new string[0];

    public string[] sbsjldciotjs = new string[0];

    public string[] sbsjldciozhis = new string[0];

    public string[] sbsjldcitemprogs = new string[0];

    public string[] sbsjldcitemtjs = new string[0];

    public string[] sbsjldcitemzhis = new string[0];

    public string[] sbsjldcitemfftjs = new string[0];

    public string[] sbsjldcitemffbdss = new string[0];

    public string[] sbsjldcpagevisibletrue = new string[0];

    public string[] sbsjldcpagevisiblefalse = new string[0];

    public string[] sbsjrdcioios = new string[0];

    public string[] sbsjrdciotjs = new string[0];

    public string[] sbsjrdciozhis = new string[0];

    public string[] sbsjrdcitemprogs = new string[0];

    public string[] sbsjrdcitemtjs = new string[0];

    public string[] sbsjrdcitemzhis = new string[0];

    public string[] sbsjrdcitemfftjs = new string[0];

    public string[] sbsjrdcitemffbdss = new string[0];

    public string[] sbsjrdcpagevisibletrue = new string[0];

    public string[] sbsjrdcpagevisiblefalse = new string[0];

    public string[] sbsjglbianliangs = new string[0];

    public string[] sbsjglxishus = new string[0];

    public bool txyc;

    public string txycbianliang = "";

    [OptionalField]
    public bool txycnotbianliang;

    public bool sptz;

    [NonSerialized]
    public bool sptzlock;

    public string sptzbianliang = "";

    public int sptzzhibianhuamin;

    public int sptzzhibianhuamax;

    public int sptzyidongmin;

    public int sptzyidongmax;

    public bool cztz;

    [NonSerialized]
    public bool cztzlock;

    public string cztzbianliang = "";

    public int cztzzhibianhuamin;

    public int cztzzhibianhuamax;

    public int cztzyidongmin;

    public int cztzyidongmax;

    [OptionalField]
    public string boolysbhbianliang = "";

    [OptionalField]
    public bool boolysbh;

    [OptionalField]
    public Color boolysbhtruecolor;

    [OptionalField]
    public Color boolysbhfalsecolor;

    public bool bxysbh;

    public string bxysbhbianliang = "";

    public float[] bxysbhmin;

    public float[] bxysbhmax;

    public Color[] bxysbhys;

    public bool[] bxysbhss;

    public bool tcs1ysbh;

    public string tcs1ysbhbianliang = "";

    public float[] tcs1ysbhmin;

    public float[] tcs1ysbhmax;

    public Color[] tcs1ysbhys;

    public bool[] tcs1ysbhss;

    public bool tcs2ysbh;

    public string tcs2ysbhbianliang = "";

    public float[] tcs2ysbhmin;

    public float[] tcs2ysbhmax;

    public Color[] tcs2ysbhys;

    public bool[] tcs2ysbhss;

    public bool czbfb;

    public string czbfbbianliang = "";

    public float czbfbzhimax;

    public float czbfbzhimin;

    public float czbfbbaifenbimax;

    public float czbfbbaifenbimin;

    public int czbfbcankao;

    public bool spbfb;

    public string spbfbbianliang = "";

    public float spbfbzhimax;

    public float spbfbzhimin;

    public float spbfbbaifenbimax;

    public float spbfbbaifenbimin;

    public int spbfbcankao;

    public bool spyd;

    public string spydbianliang = "";

    public float spydzhimin;

    public float spydzhimax;

    public float spydxiangsumin;

    public float spydxiangsumax;

    public bool czyd;

    public string czydbianliang = "";

    public float czydzhimin;

    public float czydzhimax;

    public float czydxiangsumin;

    public float czydxiangsumax;

    public bool mbxz;

    public string mbxzbianliang = "";

    public float mbxzzhimin;

    public float mbxzzhimax;

    public float mbxzjiaodumin;

    public float mbxzjiaodumax;

    public float mbxzzhongxinpianzhiright;

    public float mbxzzhongxinpianzhidown;

    public bool gdbh;

    public string gdbhbianliang = "";

    public float gdbhzhimax;

    public float gdbhzhimin;

    public float gdbhxiangsumax;

    public float gdbhxiangsumin;

    public int gdbhcankao;

    public bool kdbh;

    public string kdbhbianliang = "";

    public float kdbhzhimax;

    public float kdbhzhimin;

    public float kdbhxiangsumax;

    public float kdbhxiangsumin;

    public int kdbhcankao;

    public bool ai;

    public string aibianliang = "";

    public string aitishi = "模拟量输入";

    [OptionalField]
    public double aimax = 32767.0;

    [OptionalField]
    public double aimin = -32768.0;

    public bool di;

    public string dibianlaing = "";

    public string ditishion = "打开";

    public string ditishioff = "关闭";

    public string ditishi = "数字量输入";

    public bool zfcsr;

    public string zfcsrbianliang = "";

    public string zfcsrtishi = "字符串输入";

    public bool ao;

    [OptionalField]
    public int aojingdu;

    public string aobianliang = "";

    public bool doo;

    public string dobianlaing = "";

    public string dotishioff = "关闭";

    public string dotishion = "打开";

    public bool zfcsc;

    public string zfcscbianliang = "";

    public bool ymqh;

    public string[] ymqhxianshi;

    public string[] ymqhyincang;

    public PointF[] ImportantPoints;

    public string ShapeName;

    public Guid ShapeID;

    public float RotateAngel;

    public PointF RotateAtPoint;

    [NonSerialized]
    public bool NeedRefresh;

    public bool locked;

    public bool visible = true;

    [NonSerialized]
    public Matrix TranslateMatrix = new();

    [NonSerialized]
    public Pen _Pen = new(Color.Black);

    public DashStyle _DashStyle;

    [NonSerialized]
    public Brush _Brush = Brushes.Azure;

    public float fillAngel;

    public float FillBFB = 100f;

    public CShape[] Shapes;

    [NonSerialized]
    public bool IsClose = true;

    public Image[] _b;

    [NonSerialized]
    public bool WillDrawRectLine;

    public static long SumLayer;

    public long Layer;

    [NonSerialized]
    public static long MaxNo = 0L;

    [NonSerialized]
    public List<CShape> DelRegionShape = new();

    public PointF p00;

    public PointF p11;

    [NonSerialized]
    public List<PointF> delimportant00points = new();

    [NonSerialized]
    public List<PointF> delimportant11points = new();

    public string[] UserLogic = new string[30];

    public string[] Logic = new string[30];

    public float penwidth;

    public Color pencolor;

    public Color fillcolor1;

    public Color fillcolor2;

    public _BrushStyle bs;

    public CShape[] delregionshapetemp;

    public PointF[] delimportant00pointstemp;

    public PointF[] delimportant11pointstemp;

    [NonSerialized]
    private Image[] _bBackup;

    [NonSerialized]
    private bool brushInit = true;

    protected bool VMirror;

    protected bool HMirror;

    [OptionalField]
    public string groupName = "";

    [OptionalField]
    public int groupIndex;

    [Browsable(false)]
    private bool EnablePropertyBind
    {
        get
        {
            return enablePropertyBind;
        }
        set
        {
            enablePropertyBind = value;
        }
    }

    [Browsable(false)]
    private DataTable PropertyBindDT
    {
        get
        {
            return propertyBindDT;
        }
        set
        {
            propertyBindDT = value;
        }
    }

    [Browsable(false)]
    public bool EnableEventBind
    {
        get
        {
            return enableEventBind;
        }
        set
        {
            enableEventBind = value;
        }
    }

    [Browsable(false)]
    public Dictionary<string, List<EventSetItem>> EventBindDict
    {
        get
        {
            return eventBindDict;
        }
        set
        {
            eventBindDict = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<int> EventBindRegionList
    {
        get
        {
            return m_EventBindRegionList;
        }
        set
        {
            m_EventBindRegionList = value;
        }
    }

    [Browsable(false)]
    public int DBResult
    {
        get
        {
            return dbResult;
        }
        set
        {
            dbResult = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Dbmultoperation
    {
        get
        {
            return dbmultoperation;
        }
        set
        {
            dbmultoperation = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimationList
    {
        get
        {
            return DBAnimations;
        }
        set
        {
            DBAnimations = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Newtable
    {
        get
        {
            return newtable;
        }
        set
        {
            newtable = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncnewtable
    {
        get
        {
            return ansyncnewtable;
        }
        set
        {
            ansyncnewtable = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string NewtableSQL
    {
        get
        {
            return newtableSQL;
        }
        set
        {
            newtableSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] NewtableOtherData
    {
        get
        {
            return newtableOtherData;
        }
        set
        {
            newtableOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Dbselect
    {
        get
        {
            return dbselect;
        }
        set
        {
            dbselect = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncselect
    {
        get
        {
            return ansyncselect;
        }
        set
        {
            ansyncselect = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbselectSQL
    {
        get
        {
            return dbselectSQL;
        }
        set
        {
            dbselectSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string DbselectTO
    {
        get
        {
            return dbselectTO;
        }
        set
        {
            dbselectTO = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public byte[] DbselectOtherData
    {
        get
        {
            return dbselectOtherData;
        }
        set
        {
            dbselectOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Dbinsert
    {
        get
        {
            return dbinsert;
        }
        set
        {
            dbinsert = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncinsert
    {
        get
        {
            return ansyncinsert;
        }
        set
        {
            ansyncinsert = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string DbinsertSQL
    {
        get
        {
            return dbinsertSQL;
        }
        set
        {
            dbinsertSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] DbinsertOtherData
    {
        get
        {
            return dbinsertOtherData;
        }
        set
        {
            dbinsertOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Dbupdate
    {
        get
        {
            return dbupdate;
        }
        set
        {
            dbupdate = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public bool Ansyncupdate
    {
        get
        {
            return ansyncupdate;
        }
        set
        {
            ansyncupdate = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbupdateSQL
    {
        get
        {
            return dbupdateSQL;
        }
        set
        {
            dbupdateSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] DbupdateOtherData
    {
        get
        {
            return dbupdateOtherData;
        }
        set
        {
            dbupdateOtherData = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Dbdelete
    {
        get
        {
            return dbdelete;
        }
        set
        {
            dbdelete = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public bool Ansyncdelete
    {
        get
        {
            return ansyncdelete;
        }
        set
        {
            ansyncdelete = value;
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string DbdeleteSQL
    {
        get
        {
            return dbdeleteSQL;
        }
        set
        {
            dbdeleteSQL = value;
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public byte[] DbdeleteOtherData
    {
        get
        {
            return dbdeleteOtherData;
        }
        set
        {
            dbdeleteOtherData = value;
        }
    }

    [Category("外观")]
    [DisplayName("纹理样式")]
    [Description("当选择纹理填充时设定纹理样式。")]
    [ReadOnly(false)]
    public virtual HatchStyle HatchStyle
    {
        get
        {
            return hatchstyle;
        }
        set
        {
            if (hatchstyle != value)
            {
                NeedRefreshShape = true;
            }
            hatchstyle = value;
        }
    }

    [Browsable(false)]
    public virtual bool NeedRefreshShape
    {
        get
        {
            return NeedRefresh;
        }
        set
        {
            NeedRefresh = value;
            if (value)
            {
                needRefreshShape = true;
                needRefreshBrush = true;
            }
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string sbsjldcLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (sbsjldcioios.Length != 0)
            {
                for (int i = 0; i < sbsjldcioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjldciotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjldcioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjldciozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjldcitemprogs.Length != 0)
            {
                for (int j = 0; j < sbsjldcitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjldcitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjldcitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjldcitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjldcitemffbdss.Length != 0)
            {
                for (int k = 0; k < sbsjldcitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjldcitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjldcitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string sbsjrdcLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (sbsjrdcioios.Length != 0)
            {
                for (int i = 0; i < sbsjrdcioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrdciotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrdcioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjrdciozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjrdcitemprogs.Length != 0)
            {
                for (int j = 0; j < sbsjrdcitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrdcitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrdcitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjrdcitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjrdcitemffbdss.Length != 0)
            {
                for (int k = 0; k < sbsjrdcitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrdcitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrdcitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string sbsjlcLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (sbsjlcioios.Length != 0)
            {
                for (int i = 0; i < sbsjlcioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjlciotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjlcioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjlciozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjlcitemprogs.Length != 0)
            {
                for (int j = 0; j < sbsjlcitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjlcitemtjs[j]);
                    stringBuilder.Append(" Then ");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjlcitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjlcitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjlcitemffbdss.Length != 0)
            {
                for (int k = 0; k < sbsjlcitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjlcitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjlcitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }

    [Browsable(false)]
    [DHMIHideProperty]
    public string sbsjrcLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (sbsjrcioios.Length != 0)
            {
                for (int i = 0; i < sbsjrcioios.Length; i++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrciotjs[i]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrcioios[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjrciozhis[i]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjrcitemprogs.Length != 0)
            {
                for (int j = 0; j < sbsjrcitemprogs.Length; j++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrcitemtjs[j]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrcitemprogs[j]);
                    stringBuilder.Append("=");
                    stringBuilder.Append(sbsjrcitemzhis[j]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            if (sbsjrcitemffbdss.Length != 0)
            {
                for (int k = 0; k < sbsjrcitemffbdss.Length; k++)
                {
                    stringBuilder.Append("If ");
                    stringBuilder.Append(sbsjrcitemfftjs[k]);
                    stringBuilder.Append(" Then");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(sbsjrcitemffbdss[k]);
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("End If");
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }

    [DHMIHideProperty]
    [Browsable(false)]
    public string sbsjglLogic
    {
        get
        {
            StringBuilder stringBuilder = new();
            if (sbsjglbianliangs.Length != 0)
            {
                for (int i = 0; i < sbsjglbianliangs.Length; i++)
                {
                    stringBuilder.Append(sbsjglbianliangs[i]);
                    stringBuilder.Append("=");
                    stringBuilder.Append("System.Mouse.Delta / 120*" + sbsjglxishus[i]);
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }
    }

    [DisplayName("填充方向")]
    [Category("外观")]
    [Description("设定图形填充方向。")]
    [ReadOnly(false)]
    public virtual float FillAngel
    {
        get
        {
            return fillAngel;
        }
        set
        {
            if (fillAngel != value)
            {
                NeedRefreshShape = true;
            }
            fillAngel = value;
        }
    }

    [Category("设计")]
    [Description("设定图形名称。")]
    [ReadOnly(false)]
    [DisplayName("名称")]
    [DHMICtrlProperty]
    public virtual string Name
    {
        get
        {
            return ShapeName;
        }
        set
        {
            if (value == "")
            {
                MessageBox.Show(ShapeName + "名称不能为空,请从新输入", "错误");
            }
            Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
            if (!regex.IsMatch(value))
            {
                MessageBox.Show(ShapeName + "变量名不合法,请从新输入", "错误");
            }
            ShapeName = value;
        }
    }

    [Category("布局")]
    [DisplayName("位置")]
    [ReadOnly(false)]
    [Description("设定图形位置。")]
    public virtual Point LocationShow
    {
        get
        {
            return new Point(Convert.ToInt32(Location.X), Convert.ToInt32(Location.Y));
        }
        set
        {
            Location = value;
        }
    }

    [Browsable(false)]
    public virtual PointF Location
    {
        get
        {
            if (ImportantPoints.Length >= 8)
            {
                return ImportantPoints[0];
            }
            return Point.Empty;
        }
        set
        {
            if (sptzlock)
            {
                value = new PointF(Location.X, value.Y);
            }
            if (cztzlock)
            {
                value = new PointF(value.X, Location.Y);
            }
            if (value != Location)
            {
                NeedRefreshShape = true;
                if (ImportantPoints.Length >= 8)
                {
                    EditLocationByPoint(ImportantPoints[0], value);
                }
            }
        }
    }

    [Browsable(false)]
    public virtual float X
    {
        get
        {
            return ImportantPoints[0].X;
        }
        set
        {
            if (value != X)
            {
                NeedRefreshShape = true;
            }
            Location = new PointF(value, Location.Y);
        }
    }

    [Browsable(false)]
    public virtual float Y
    {
        get
        {
            return ImportantPoints[0].Y;
        }
        set
        {
            if (value != Y)
            {
                NeedRefreshShape = true;
            }
            Location = new PointF(Location.X, value);
        }
    }

    [Category("布局")]
    [Description("获取图形中心位置。")]
    [DisplayName("中心")]
    [ReadOnly(true)]
    public virtual Point MidPoint => Point.Truncate(new PointF(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f));

    [Browsable(false)]
    public virtual PointF MidRotatePoint => new(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);

    [Category("布局")]
    [Description("设定图形大小。")]
    [DisplayName("大小")]
    [ReadOnly(false)]
    public virtual SizeF Size
    {
        get
        {
            PointF[] array = (PointF[])ImportantPoints.Clone();
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(array);
            return new SizeF(array[1].X - array[0].X, array[1].Y - array[0].Y);
        }
        set
        {
            if (value != Size)
            {
                NeedRefreshShape = true;
            }
            new SizeF(ImportantPoints[1].X - ImportantPoints[0].X, ImportantPoints[1].Y - ImportantPoints[0].Y);
            EditPoint(ImportantPoints[7], new PointF(ImportantPoints[6].X + (float)Math.Cos(Math.Atan2(ImportantPoints[7].Y - ImportantPoints[6].Y, ImportantPoints[7].X - ImportantPoints[6].X)) * value.Width, ImportantPoints[6].Y + (float)Math.Sin(Math.Atan2(ImportantPoints[7].Y - ImportantPoints[6].Y, ImportantPoints[7].X - ImportantPoints[6].X)) * value.Width), -1);
            EditPoint(ImportantPoints[5], new PointF(ImportantPoints[4].X + (float)Math.Cos(Math.Atan2(ImportantPoints[5].Y - ImportantPoints[4].Y, ImportantPoints[5].X - ImportantPoints[4].X)) * value.Height, ImportantPoints[4].Y + (float)Math.Sin(Math.Atan2(ImportantPoints[5].Y - ImportantPoints[4].Y, ImportantPoints[5].X - ImportantPoints[4].X)) * value.Height), -1);
        }
    }

    [DisplayName("宽度")]
    [DHMICtrlProperty]
    [Category("布局")]
    [Description("设定图形宽度。")]
    [ReadOnly(false)]
    [Browsable(false)]
    public virtual float Width
    {
        get
        {
            return Size.Width;
        }
        set
        {
            if (value != Width)
            {
                NeedRefreshShape = true;
            }
            Size = new SizeF(value, Size.Height);
        }
    }

    [ReadOnly(false)]
    [Browsable(false)]
    [DHMICtrlProperty]
    [DisplayName("高度")]
    [Category("布局")]
    [Description("设定图形高度。")]
    public virtual float Height
    {
        get
        {
            return Size.Height;
        }
        set
        {
            if (value != Height)
            {
                NeedRefreshShape = true;
            }
            Size = new SizeF(Size.Width, value);
        }
    }

    [ReadOnly(false)]
    [Description("设定图形填充样式。")]
    [DisplayName("填充1")]
    [Category("外观")]
    public virtual Color Color1
    {
        get
        {
            Color result = Color.Azure;
            if (_Brush is SolidBrush solidBrush)
            {
                result = solidBrush.Color;
            }
            else if (_Brush is LinearGradientBrush linearGradientBrush)
            {
                result = linearGradientBrush.LinearColors[0];
            }
            else if (_Brush is PathGradientBrush pathGradientBrush)
            {
                result = pathGradientBrush.CenterColor;
            }
            else if (_Brush is HatchBrush hatchBrush)
            {
                try
                {
                    result = hatchBrush.ForegroundColor;
                }
                catch (Exception)
                {
                    result = Color.Transparent;
                }
            }
            return result;
        }
        set
        {
            if (value != Color1)
            {
                NeedRefreshShape = true;
            }
            if (_Brush is SolidBrush)
            {
                _ = (SolidBrush)_Brush;
                _Brush = new SolidBrush(value);
            }
            else if (_Brush is LinearGradientBrush)
            {
                _ = (LinearGradientBrush)_Brush;
                LinearGradientBrush linearGradientBrush = new(new Rectangle(Convert.ToInt32(HowToDraw()[0].GetBounds().X), Convert.ToInt32(HowToDraw()[0].GetBounds().Y), Convert.ToInt32(HowToDraw()[0].GetBounds().Width), Convert.ToInt32(HowToDraw()[0].GetBounds().Height)), value, Color2, FillAngel, isAngleScaleable: true);
                if (BrushStyle == _BrushStyle.百分比填充)
                {
                    float[] factors = new float[4] { 1f, 1f, 0f, 0f };
                    float[] positions = new float[4]
                    {
                        0f,
                        FillBFB / 100f,
                        FillBFB / 100f + 0.01f,
                        1f
                    };
                    Blend blend = new()
                    {
                        Factors = factors,
                        Positions = positions
                    };
                    linearGradientBrush.Blend = blend;
                }
                _Brush = linearGradientBrush;
            }
            else if (_Brush is PathGradientBrush)
            {
                Color color = Color2;
                _Brush = new PathGradientBrush(HowToDraw()[0]);
                PathGradientBrush pathGradientBrush = (PathGradientBrush)_Brush;
                pathGradientBrush.CenterColor = value;
                Color[] array = (Color[])pathGradientBrush.SurroundColors.Clone();
                for (int i = 0; i < pathGradientBrush.SurroundColors.Length; i++)
                {
                    array[i] = color;
                }
                pathGradientBrush.SurroundColors = array;
            }
            else if (_Brush is HatchBrush)
            {
                _ = (HatchBrush)_Brush;
                _Brush = new HatchBrush(hatchstyle, value, Color2);
            }
        }
    }

    [Category("外观")]
    [ReadOnly(false)]
    [Description("设定图形填充样式。")]
    [DisplayName("填充2")]
    public virtual Color Color2
    {
        get
        {
            Color result = Color.Azure;
            if (_Brush is SolidBrush solidBrush)
            {
                result = solidBrush.Color;
            }
            else if (_Brush is LinearGradientBrush linearGradientBrush)
            {
                result = linearGradientBrush.LinearColors[1];
            }
            else if (_Brush is PathGradientBrush pathGradientBrush)
            {
                result = pathGradientBrush.SurroundColors[0];
            }
            else if (_Brush is HatchBrush hatchBrush)
            {
                try
                {
                    result = hatchBrush.BackgroundColor;
                }
                catch (Exception)
                {
                    result = Color.Transparent;
                }
            }
            return result;
        }
        set
        {
            if (value != Color2)
            {
                NeedRefreshShape = true;
            }
            if (_Brush is SolidBrush)
            {
                _ = (SolidBrush)_Brush;
                _Brush = new SolidBrush(value);
            }
            else if (_Brush is LinearGradientBrush)
            {
                _ = (LinearGradientBrush)_Brush;
                LinearGradientBrush linearGradientBrush = new(new Rectangle(Convert.ToInt32(HowToDraw()[0].GetBounds().X), Convert.ToInt32(HowToDraw()[0].GetBounds().Y), Convert.ToInt32(HowToDraw()[0].GetBounds().Width), Convert.ToInt32(HowToDraw()[0].GetBounds().Height)), Color1, value, FillAngel, isAngleScaleable: true);
                if (BrushStyle == _BrushStyle.百分比填充)
                {
                    float[] factors = new float[4] { 1f, 1f, 0f, 0f };
                    float[] positions = new float[4]
                    {
                        0f,
                        FillBFB / 100f,
                        FillBFB / 100f + 0.01f,
                        1f
                    };
                    Blend blend = new()
                    {
                        Factors = factors,
                        Positions = positions
                    };
                    linearGradientBrush.Blend = blend;
                }
                _Brush = linearGradientBrush;
            }
            else if (_Brush is PathGradientBrush)
            {
                Color color = Color1;
                _Brush = new PathGradientBrush(HowToDraw()[0]);
                PathGradientBrush pathGradientBrush = (PathGradientBrush)_Brush;
                pathGradientBrush.CenterColor = color;
                Color[] array = (Color[])pathGradientBrush.SurroundColors.Clone();
                for (int i = 0; i < pathGradientBrush.SurroundColors.Length; i++)
                {
                    array[i] = value;
                }
                pathGradientBrush.SurroundColors = array;
            }
            else if (_Brush is HatchBrush)
            {
                _Brush = new HatchBrush(hatchstyle, Color1, value);
            }
        }
    }

    [ReadOnly(false)]
    [Description("设定图形填充样式。")]
    [Category("外观")]
    [DisplayName("填充样式")]
    public virtual _BrushStyle BrushStyle
    {
        get
        {
            _BrushStyle result = _BrushStyle.单色填充;
            if (_Brush is SolidBrush)
            {
                _ = (SolidBrush)_Brush;
                result = _BrushStyle.单色填充;
            }
            else if (_Brush is LinearGradientBrush linearGradientBrush)
            {
                result = ((linearGradientBrush.Blend.Factors.Length == 1) ? _BrushStyle.线性渐变填充 : _BrushStyle.百分比填充);
            }
            else if (_Brush is PathGradientBrush)
            {
                _ = (PathGradientBrush)_Brush;
                result = _BrushStyle.渐变色填充;
            }
            else if (_Brush is HatchBrush)
            {
                result = _BrushStyle.图案填充;
            }
            return result;
        }
        set
        {
            if (value != BrushStyle)
            {
                NeedRefreshShape = true;
            }
            if (value == _BrushStyle.单色填充)
            {
                _Brush = new SolidBrush(Color1);
            }
            if (value == _BrushStyle.线性渐变填充)
            {
                _Brush = new LinearGradientBrush(ImportantPoints[0], ImportantPoints[1], Color1, Color2);
            }
            if (value == _BrushStyle.百分比填充)
            {
                LinearGradientBrush linearGradientBrush = new(ImportantPoints[0], ImportantPoints[1], Color1, Color2);
                float[] factors = new float[4] { 1f, 1f, 0f, 0f };
                float[] positions = new float[4]
                {
                    0f,
                    FillBFB / 100f,
                    FillBFB / 100f + 0.01f,
                    1f
                };
                Blend blend = new()
                {
                    Factors = factors,
                    Positions = positions
                };
                linearGradientBrush.Blend = blend;
                _Brush = linearGradientBrush;
            }
            switch (value)
            {
                case _BrushStyle.渐变色填充:
                    {
                        Color color = Color1;
                        Color color2 = Color2;
                        _Brush = new PathGradientBrush(HowToDraw()[0]);
                        PathGradientBrush pathGradientBrush = (PathGradientBrush)_Brush;
                        pathGradientBrush.CenterColor = color;
                        Color[] array = (Color[])pathGradientBrush.SurroundColors.Clone();
                        for (int i = 0; i < pathGradientBrush.SurroundColors.Length; i++)
                        {
                            array[i] = color2;
                        }
                        pathGradientBrush.SurroundColors = array;
                        break;
                    }
                case _BrushStyle.图案填充:
                    _Brush = new HatchBrush(hatchstyle, Color1, Color2);
                    break;
            }
        }
    }

    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("填充百分比")]
    [Category("外观")]
    [Description("设定渐变填充百分比。")]
    public virtual float BrushBFB
    {
        get
        {
            if (BrushStyle != _BrushStyle.百分比填充)
            {
                return -1f;
            }
            return FillBFB;
        }
        set
        {
            if (value != BrushBFB)
            {
                NeedRefreshShape = true;
            }
            if (BrushStyle == _BrushStyle.百分比填充)
            {
                LinearGradientBrush linearGradientBrush = (LinearGradientBrush)_Brush;
                float[] factors = new float[4] { 1f, 1f, 0f, 0f };
                float[] positions = new float[4]
                {
                    0f,
                    FillBFB / 100f,
                    FillBFB / 100f + 0.01f,
                    1f
                };
                Blend blend = new()
                {
                    Factors = factors,
                    Positions = positions
                };
                linearGradientBrush.Blend = blend;
                FillBFB = value;
            }
        }
    }

    [ReadOnly(false)]
    [DisplayName("画笔颜色")]
    [Description("设定图形边线样式。")]
    [Category("外观")]
    public virtual Color PenColor
    {
        get
        {
            return _Pen.Color;
        }
        set
        {
            if (value != PenColor)
            {
                NeedRefreshShape = true;
            }
            _Pen.Color = value;
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [Description("设定图形边线样式。")]
    [DisplayName("画笔样式")]
    public virtual DashStyle PenStyle
    {
        get
        {
            return _DashStyle;
        }
        set
        {
            if (value != PenStyle)
            {
                NeedRefreshShape = true;
            }
            _Pen.DashStyle = value;
            _DashStyle = value;
        }
    }

    [Description("设定图形边线样式。")]
    [Category("外观")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("画笔宽度")]
    public virtual float PenWidth
    {
        get
        {
            return _Pen.Width;
        }
        set
        {
            if (value != PenWidth)
            {
                NeedRefreshShape = true;
            }
            _Pen.Width = value;
        }
    }

    [Category("布局")]
    [ReadOnly(false)]
    [Description("设定图形旋转点。")]
    [DisplayName("旋转点")]
    public Point RotatePointShow
    {
        get
        {
            return new Point(Convert.ToInt32(RotatePoint.X), Convert.ToInt32(RotatePoint.Y));
        }
        set
        {
            RotatePoint = value;
        }
    }

    [Browsable(false)]
    public PointF RotatePoint
    {
        get
        {
            return RotateAtPoint;
        }
        set
        {
            EditPoint(RotateAtPoint, value, 55);
        }
    }

    [DisplayName("旋转角度")]
    [Description("设定图形旋转角度。")]
    [ReadOnly(false)]
    [Category("布局")]
    [DHMICtrlProperty]
    public float Angel
    {
        get
        {
            return RotateAngel;
        }
        set
        {
            if (value != Angel)
            {
                NeedRefreshShape = true;
            }
            EditPoint(new PointF(RotateAtPoint.X + (Height / 2f + 35f) * (float)Math.Cos((double)(RotateAngel - 90f) * Math.PI / 180.0), RotateAtPoint.Y + (Height / 2f + 35f) * (float)Math.Sin((double)(RotateAngel - 90f) * Math.PI / 180.0)), new PointF(RotateAtPoint.X + (Height / 2f + 35f) * (float)Math.Cos((double)(value - 90f) * Math.PI / 180.0), RotateAtPoint.Y + (Height / 2f + 35f) * (float)Math.Sin((double)(value - 90f) * Math.PI / 180.0)), 557);
        }
    }

    [DHMICtrlProperty]
    [Category("布局")]
    [ReadOnly(false)]
    [DisplayName("可见")]
    [Description("设定是否初始显示图形。")]
    public bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            if (value != Visible)
            {
                NeedRefreshShape = true;
            }
            visible = value;
        }
    }

    [Description("设定图形显示顺序。")]
    [Category("布局")]
    [DisplayName("图层")]
    [ReadOnly(false)]
    public long Lay
    {
        get
        {
            return Layer;
        }
        set
        {
            Layer = value;
        }
    }

    [DisplayName("旋转点横坐标")]
    [DHMICtrlProperty]
    [Category("布局")]
    [Description("设定旋转点横坐标。")]
    [ReadOnly(true)]
    [Browsable(false)]
    public float RotatePointX
    {
        get
        {
            return RotatePoint.X;
        }
        set
        {
            RotatePoint = new PointF(value, RotatePoint.Y);
        }
    }

    [DisplayName("旋转点纵坐标")]
    [DHMICtrlProperty]
    [Browsable(false)]
    [Category("布局")]
    [ReadOnly(false)]
    [Description("设定旋转点纵坐标。")]
    public float RotatePointY
    {
        get
        {
            return RotatePoint.Y;
        }
        set
        {
            RotatePoint = new PointF(RotatePoint.X, value);
        }
    }

    [ReadOnly(false)]
    [Category("外观")]
    [Description("设定图元透明度百分比。(0-100;0-完全不透明 100-完全透明)")]
    [DHMICtrlProperty]
    [DisplayName("透明度")]
    public virtual int Opacity
    {
        get
        {
            return 100 - Convert.ToInt32((float)Color1A / 255f * 100f);
        }
        set
        {
            if (value > 100)
            {
                value = 100;
            }
            else if (value < 0)
            {
                value = 0;
            }
            Color1A = Convert.ToInt32((float)(100 - value) / 100f * 255f);
            Color2A = Convert.ToInt32((float)(100 - value) / 100f * 255f);
            PenColorA = Convert.ToInt32((float)(100 - value) / 100f * 255f);
        }
    }

    [Description("设定填充色1透明度。")]
    [Category("外观")]
    [DHMICtrlProperty]
    [DisplayName("填充色1透明度")]
    [Browsable(false)]
    [ReadOnly(false)]
    public int Color1A
    {
        get
        {
            return Color1.A;
        }
        set
        {
            Color1 = Color.FromArgb(value, Color1.R, Color1.G, Color1.B);
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Browsable(false)]
    [DisplayName("填充色1红色值")]
    [Category("外观")]
    [Description("设定填充色1红色值。")]
    public int Color1R
    {
        get
        {
            return Color1.R;
        }
        set
        {
            Color1 = Color.FromArgb(Color1.A, value, Color1.G, Color1.B);
        }
    }

    [Browsable(false)]
    [Category("外观")]
    [DHMICtrlProperty]
    [DisplayName("填充色1绿色值")]
    [ReadOnly(false)]
    [Description("设定填充色1绿色值。")]
    public int Color1G
    {
        get
        {
            return Color1.G;
        }
        set
        {
            Color1 = Color.FromArgb(Color1.A, Color1.R, value, Color1.B);
        }
    }

    [Browsable(false)]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("填充色1蓝色值")]
    [Category("外观")]
    [Description("设定填充色1蓝色值。")]
    public int Color1B
    {
        get
        {
            return Color1.B;
        }
        set
        {
            Color1 = Color.FromArgb(Color1.A, Color1.R, Color1.G, value);
        }
    }

    [ReadOnly(false)]
    [DisplayName("填充色2透明度")]
    [DHMICtrlProperty]
    [Browsable(false)]
    [Category("外观")]
    [Description("设定填充色2透明度。")]
    public int Color2A
    {
        get
        {
            return Color2.A;
        }
        set
        {
            Color2 = Color.FromArgb(value, Color2.R, Color2.G, Color2.B);
        }
    }

    [Category("外观")]
    [DHMICtrlProperty]
    [Browsable(false)]
    [Description("设定填充色2红色值。")]
    [ReadOnly(false)]
    [DisplayName("填充色2红色值")]
    public int Color2R
    {
        get
        {
            return Color2.R;
        }
        set
        {
            Color2 = Color.FromArgb(Color2.A, value, Color2.G, Color2.B);
        }
    }

    [Description("设定填充色2绿色值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    [DisplayName("填充色2绿色值")]
    [Category("外观")]
    [Browsable(false)]
    public int Color2G
    {
        get
        {
            return Color2.G;
        }
        set
        {
            Color2 = Color.FromArgb(Color2.A, Color2.R, value, Color2.B);
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Browsable(false)]
    [DisplayName("填充色2蓝色值")]
    [Category("外观")]
    [Description("设定填充色2蓝色值。")]
    public int Color2B
    {
        get
        {
            return Color2.B;
        }
        set
        {
            Color2 = Color.FromArgb(Color2.A, Color2.R, Color2.G, value);
        }
    }

    [DHMICtrlProperty]
    [ReadOnly(false)]
    [Browsable(false)]
    [DisplayName("边线颜色透明度")]
    [Category("外观")]
    [Description("设定边线颜色透明度。")]
    public int PenColorA
    {
        get
        {
            return PenColor.A;
        }
        set
        {
            PenColor = Color.FromArgb(value, PenColor.R, PenColor.G, PenColor.B);
        }
    }

    [Browsable(false)]
    [DHMICtrlProperty]
    [DisplayName("边线颜色红色值")]
    [Category("外观")]
    [Description("设定边线颜色红色值。")]
    [ReadOnly(false)]
    public int PenColorR
    {
        get
        {
            return PenColor.R;
        }
        set
        {
            PenColor = Color.FromArgb(PenColor.A, value, PenColor.G, PenColor.B);
        }
    }

    [Category("外观")]
    [DisplayName("边线颜色绿色值")]
    [Browsable(false)]
    [Description("设定边线颜色绿色值。")]
    [ReadOnly(false)]
    [DHMICtrlProperty]
    public int PenColorG
    {
        get
        {
            return PenColor.G;
        }
        set
        {
            PenColor = Color.FromArgb(PenColor.A, PenColor.R, value, PenColor.B);
        }
    }

    [DHMICtrlProperty]
    [DisplayName("边线颜色蓝色值")]
    [Browsable(false)]
    [Category("外观")]
    [Description("设定边线颜色蓝色值。")]
    [ReadOnly(false)]
    public int PenColorB
    {
        get
        {
            return PenColor.B;
        }
        set
        {
            PenColor = Color.FromArgb(PenColor.A, PenColor.R, PenColor.G, value);
        }
    }

    public event EventHandler DBOperationOK;

    public event EventHandler DBOperationErr;

    public event EventHandler Click;

    protected CShape(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        FieldInfo[] fields = typeof(CShape).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        CShape obj = new();
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.DeclaringType == typeof(CShape))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(obj));
            }
        }
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Name == "SumLayer")
            {
                if (SumLayer <= (long)enumerator.Value)
                {
                    SumLayer = (long)enumerator.Value;
                }
                continue;
            }
            FieldInfo field = typeof(CShape).GetField(enumerator.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null && field.DeclaringType == typeof(CShape))
            {
                field.SetValue(this, enumerator.Value);
            }
        }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("CShape info");
        }
        info.AddValue("enablePropertyBind", enablePropertyBind);
        info.AddValue("propertyBindDT", propertyBindDT);
        info.AddValue("enableEventBind", enableEventBind);
        info.AddValue("eventBindDict", eventBindDict);
        info.AddValue("m_EventBindRegionList", m_EventBindRegionList);
        info.AddValue("DBOKScriptUser", DBOKScriptUser);
        info.AddValue("DBErrScriptUser", DBErrScriptUser);
        info.AddValue("DBOKScript", DBOKScript);
        info.AddValue("DBErrScript", DBErrScript);
        if (dbmultoperation)
        {
            info.AddValue("dbmultoperation", dbmultoperation);
            info.AddValue("DBAnimations", DBAnimations);
        }
        if (newtable)
        {
            info.AddValue("newtable", newtable);
            info.AddValue("newtableSQL", newtableSQL);
            info.AddValue("ansyncnewtable", ansyncnewtable);
            info.AddValue("newtableOtherData", newtableOtherData);
        }
        if (dbselect)
        {
            info.AddValue("dbselect", dbselect);
            info.AddValue("ansyncselect", ansyncselect);
            info.AddValue("dbselectSQL", dbselectSQL);
            info.AddValue("dbselectTO", dbselectTO);
            info.AddValue("dbselectOtherData", dbselectOtherData);
        }
        if (dbinsert)
        {
            info.AddValue("dbinsert", dbinsert);
            info.AddValue("ansyncinsert", ansyncinsert);
            info.AddValue("dbinsertSQL", dbinsertSQL);
            info.AddValue("dbinsertOtherData", dbinsertOtherData);
        }
        if (dbupdate)
        {
            info.AddValue("dbupdate", dbupdate);
            info.AddValue("ansyncupdate", ansyncupdate);
            info.AddValue("dbupdateSQL", dbupdateSQL);
            info.AddValue("dbupdateOtherData", dbupdateOtherData);
        }
        if (dbdelete)
        {
            info.AddValue("dbdelete", dbdelete);
            info.AddValue("ansyncdelete", ansyncdelete);
            info.AddValue("dbdeleteSQL", dbdeleteSQL);
            info.AddValue("dbdeleteOtherData", dbdeleteOtherData);
        }
        info.AddValue("hatchstyle", hatchstyle);
        info.AddValue("DefaultAngle", DefaultAngle);
        info.AddValue("DefaultLocaion", DefaultLocaion);
        info.AddValue("DefaultSize", DefaultSize);
        info.AddValue("ysdzshijianmingcheng", ysdzshijianmingcheng);
        info.AddValue("ysdzshijianbiaodashi", ysdzshijianbiaodashi);
        info.AddValue("ysdzshijianLogic", ysdzshijianLogic);
        if (sbsj)
        {
            info.AddValue("sbsj", sbsj);
            info.AddValue("_sbsjldcLogic", _sbsjldcLogic);
            info.AddValue("_sbsjrdcLogic", _sbsjrdcLogic);
            info.AddValue("_sbsjlcLogic", _sbsjlcLogic);
            info.AddValue("_sbsjrcLogic", _sbsjrcLogic);
            info.AddValue("_sbsjglLogic", _sbsjglLogic);
            info.AddValue("sbsjWhenOutThenDo", sbsjWhenOutThenDo);
            info.AddValue("sbsjlcioios", sbsjlcioios);
            info.AddValue("sbsjlciotjs", sbsjlciotjs);
            info.AddValue("sbsjlciozhis", sbsjlciozhis);
            info.AddValue("sbsjlcitemprogs", sbsjlcitemprogs);
            info.AddValue("sbsjlcitemtjs", sbsjlcitemtjs);
            info.AddValue("sbsjlcitemzhis", sbsjlcitemzhis);
            info.AddValue("sbsjlcitemfftjs", sbsjlcitemfftjs);
            info.AddValue("sbsjlcitemffbdss", sbsjlcitemffbdss);
            info.AddValue("sbsjlcpagevisibletrue", sbsjlcpagevisibletrue);
            info.AddValue("sbsjlcpagevisiblefalse", sbsjlcpagevisiblefalse);
            info.AddValue("sbsjrcioios", sbsjrcioios);
            info.AddValue("sbsjrciotjs", sbsjrciotjs);
            info.AddValue("sbsjrciozhis", sbsjrciozhis);
            info.AddValue("sbsjrcitemprogs", sbsjrcitemprogs);
            info.AddValue("sbsjrcitemtjs", sbsjrcitemtjs);
            info.AddValue("sbsjrcitemzhis", sbsjrcitemzhis);
            info.AddValue("sbsjrcitemfftjs", sbsjrcitemfftjs);
            info.AddValue("sbsjrcitemffbdss", sbsjrcitemffbdss);
            info.AddValue("sbsjrcpagevisibletrue", sbsjrcpagevisibletrue);
            info.AddValue("sbsjrcpagevisiblefalse", sbsjrcpagevisiblefalse);
            info.AddValue("sbsjldcioios", sbsjldcioios);
            info.AddValue("sbsjldciotjs", sbsjldciotjs);
            info.AddValue("sbsjldciozhis", sbsjldciozhis);
            info.AddValue("sbsjldcitemprogs", sbsjldcitemprogs);
            info.AddValue("sbsjldcitemtjs", sbsjldcitemtjs);
            info.AddValue("sbsjldcitemzhis", sbsjldcitemzhis);
            info.AddValue("sbsjldcitemfftjs", sbsjldcitemfftjs);
            info.AddValue("sbsjldcitemffbdss", sbsjldcitemffbdss);
            info.AddValue("sbsjldcpagevisibletrue", sbsjldcpagevisibletrue);
            info.AddValue("sbsjldcpagevisiblefalse", sbsjldcpagevisiblefalse);
            info.AddValue("sbsjrdcioios", sbsjrdcioios);
            info.AddValue("sbsjrdciotjs", sbsjrdciotjs);
            info.AddValue("sbsjrdciozhis", sbsjrdciozhis);
            info.AddValue("sbsjrdcitemprogs", sbsjrdcitemprogs);
            info.AddValue("sbsjrdcitemtjs", sbsjrdcitemtjs);
            info.AddValue("sbsjrdcitemzhis", sbsjrdcitemzhis);
            info.AddValue("sbsjrdcitemfftjs", sbsjrdcitemfftjs);
            info.AddValue("sbsjrdcitemffbdss", sbsjrdcitemffbdss);
            info.AddValue("sbsjrdcpagevisibletrue", sbsjrdcpagevisibletrue);
            info.AddValue("sbsjrdcpagevisiblefalse", sbsjrdcpagevisiblefalse);
            info.AddValue("sbsjglbianliangs", sbsjglbianliangs);
            info.AddValue("sbsjglxishus", sbsjglxishus);
        }
        if (txyc)
        {
            info.AddValue("txyc", txyc);
            info.AddValue("txycbianliang", txycbianliang);
            info.AddValue("txycnotbianliang", txycnotbianliang);
        }
        if (sptz)
        {
            info.AddValue("sptz", sptz);
            info.AddValue("sptzbianliang", sptzbianliang);
            info.AddValue("sptzzhibianhuamin", sptzzhibianhuamin);
            info.AddValue("sptzzhibianhuamax", sptzzhibianhuamax);
            info.AddValue("sptzyidongmin", sptzyidongmin);
            info.AddValue("sptzyidongmax", sptzyidongmax);
        }
        if (cztz)
        {
            info.AddValue("cztz", cztz);
            info.AddValue("cztzbianliang", cztzbianliang);
            info.AddValue("cztzzhibianhuamin", cztzzhibianhuamin);
            info.AddValue("cztzzhibianhuamax", cztzzhibianhuamax);
            info.AddValue("cztzyidongmin", cztzyidongmin);
            info.AddValue("cztzyidongmax", cztzyidongmax);
        }
        if (bxysbh)
        {
            info.AddValue("bxysbh", bxysbh);
            info.AddValue("bxysbhbianliang", bxysbhbianliang);
            info.AddValue("bxysbhmin", bxysbhmin);
            info.AddValue("bxysbhmax", bxysbhmax);
            info.AddValue("bxysbhys", bxysbhys);
            info.AddValue("bxysbhss", bxysbhss);
        }
        if (tcs1ysbh)
        {
            info.AddValue("tcs1ysbh", tcs1ysbh);
            info.AddValue("tcs1ysbhbianliang", tcs1ysbhbianliang);
            info.AddValue("tcs1ysbhmin", tcs1ysbhmin);
            info.AddValue("tcs1ysbhmax", tcs1ysbhmax);
            info.AddValue("tcs1ysbhys", tcs1ysbhys);
            info.AddValue("tcs1ysbhss", tcs1ysbhss);
        }
        if (tcs2ysbh)
        {
            info.AddValue("tcs2ysbh", tcs2ysbh);
            info.AddValue("tcs2ysbhbianliang", tcs2ysbhbianliang);
            info.AddValue("tcs2ysbhmin", tcs2ysbhmin);
            info.AddValue("tcs2ysbhmax", tcs2ysbhmax);
            info.AddValue("tcs2ysbhys", tcs2ysbhys);
            info.AddValue("tcs2ysbhss", tcs2ysbhss);
        }
        if (czbfb)
        {
            info.AddValue("czbfb", czbfb);
            info.AddValue("czbfbbianliang", czbfbbianliang);
            info.AddValue("czbfbzhimax", czbfbzhimax);
            info.AddValue("czbfbzhimin", czbfbzhimin);
            info.AddValue("czbfbbaifenbimax", czbfbbaifenbimax);
            info.AddValue("czbfbbaifenbimin", czbfbbaifenbimin);
            info.AddValue("czbfbcankao", czbfbcankao);
        }
        if (spbfb)
        {
            info.AddValue("spbfb", spbfb);
            info.AddValue("spbfbbianliang", spbfbbianliang);
            info.AddValue("spbfbzhimax", spbfbzhimax);
            info.AddValue("spbfbzhimin", spbfbzhimin);
            info.AddValue("spbfbbaifenbimax", spbfbbaifenbimax);
            info.AddValue("spbfbbaifenbimin", spbfbbaifenbimin);
            info.AddValue("spbfbcankao", spbfbcankao);
        }
        if (spyd)
        {
            info.AddValue("spyd", spyd);
            info.AddValue("spydbianliang", spydbianliang);
            info.AddValue("spydzhimin", spydzhimin);
            info.AddValue("spydzhimax", spydzhimax);
            info.AddValue("spydxiangsumin", spydxiangsumin);
            info.AddValue("spydxiangsumax", spydxiangsumax);
        }
        if (czyd)
        {
            info.AddValue("czyd", czyd);
            info.AddValue("czydbianliang", czydbianliang);
            info.AddValue("czydzhimin", czydzhimin);
            info.AddValue("czydzhimax", czydzhimax);
            info.AddValue("czydxiangsumin", czydxiangsumin);
            info.AddValue("czydxiangsumax", czydxiangsumax);
        }
        if (mbxz)
        {
            info.AddValue("mbxz", mbxz);
            info.AddValue("mbxzbianliang", mbxzbianliang);
            info.AddValue("mbxzzhimin", mbxzzhimin);
            info.AddValue("mbxzzhimax", mbxzzhimax);
            info.AddValue("mbxzjiaodumin", mbxzjiaodumin);
            info.AddValue("mbxzjiaodumax", mbxzjiaodumax);
            info.AddValue("mbxzzhongxinpianzhiright", mbxzzhongxinpianzhiright);
            info.AddValue("mbxzzhongxinpianzhidown", mbxzzhongxinpianzhidown);
        }
        if (gdbh)
        {
            info.AddValue("gdbh", gdbh);
            info.AddValue("gdbhbianliang", gdbhbianliang);
            info.AddValue("gdbhzhimax", gdbhzhimax);
            info.AddValue("gdbhzhimin", gdbhzhimin);
            info.AddValue("gdbhxiangsumax", gdbhxiangsumax);
            info.AddValue("gdbhxiangsumin", gdbhxiangsumin);
            info.AddValue("gdbhcankao", gdbhcankao);
        }
        if (kdbh)
        {
            info.AddValue("kdbh", kdbh);
            info.AddValue("kdbhbianliang", kdbhbianliang);
            info.AddValue("kdbhzhimax", kdbhzhimax);
            info.AddValue("kdbhzhimin", kdbhzhimin);
            info.AddValue("kdbhxiangsumax", kdbhxiangsumax);
            info.AddValue("kdbhxiangsumin", kdbhxiangsumin);
            info.AddValue("kdbhcankao", kdbhcankao);
        }
        if (ai)
        {
            info.AddValue("ai", ai);
            info.AddValue("aibianliang", aibianliang);
            info.AddValue("aitishi", aitishi);
            info.AddValue("aimax", aimax);
            info.AddValue("aimin", aimin);
        }
        if (di)
        {
            info.AddValue("di", di);
            info.AddValue("dibianlaing", dibianlaing);
            info.AddValue("ditishion", ditishion);
            info.AddValue("ditishioff", ditishioff);
            info.AddValue("ditishi", ditishi);
        }
        if (zfcsr)
        {
            info.AddValue("zfcsr", zfcsr);
            info.AddValue("zfcsrbianliang", zfcsrbianliang);
            info.AddValue("zfcsrtishi", zfcsrtishi);
        }
        if (ao)
        {
            info.AddValue("ao", ao);
            info.AddValue("aojingdu", aojingdu);
            info.AddValue("aobianliang", aobianliang);
        }
        if (doo)
        {
            info.AddValue("doo", doo);
            info.AddValue("dobianlaing", dobianlaing);
            info.AddValue("dotishioff", dotishioff);
            info.AddValue("dotishion", dotishion);
        }
        if (zfcsc)
        {
            info.AddValue("zfcsc", zfcsc);
            info.AddValue("zfcscbianliang", zfcscbianliang);
        }
        if (ymqh)
        {
            info.AddValue("ymqh", ymqh);
            info.AddValue("ymqhxianshi", ymqhxianshi);
            info.AddValue("ymqhyincang", ymqhyincang);
        }
        info.AddValue("ImportantPoints", ImportantPoints);
        info.AddValue("ShapeName", ShapeName);
        info.AddValue("ShapeID", ShapeID);
        info.AddValue("RotateAngel", RotateAngel);
        info.AddValue("RotateAtPoint", RotateAtPoint);
        info.AddValue("locked", locked);
        info.AddValue("visible", visible);
        info.AddValue("_DashStyle", _DashStyle);
        info.AddValue("fillAngel", fillAngel);
        info.AddValue("FillBFB", FillBFB);
        info.AddValue("Shapes", Shapes);
        info.AddValue("_b", _b);
        info.AddValue("SumLayer", SumLayer);
        info.AddValue("Layer", Layer);
        info.AddValue("p00", p00);
        info.AddValue("p11", p11);
        info.AddValue("UserLogic", UserLogic);
        info.AddValue("Logic", Logic);
        info.AddValue("penwidth", penwidth);
        info.AddValue("pencolor", pencolor);
        info.AddValue("fillcolor1", fillcolor1);
        info.AddValue("fillcolor2", fillcolor2);
        info.AddValue("bs", bs);
        info.AddValue("delregionshapetemp", delregionshapetemp);
        info.AddValue("delimportant00pointstemp", delimportant00pointstemp);
        info.AddValue("delimportant11pointstemp", delimportant11pointstemp);
        info.AddValue("VMirror", VMirror);
        info.AddValue("HMirror", HMirror);
        info.AddValue("groupName", groupName);
        info.AddValue("groupIndex", groupIndex);
    }

    public void FireClick()
    {
        if (this.Click != null)
        {
            this.Click(this, null);
        }
    }

    public void FireDBOperationOK()
    {
        if (this.DBOperationOK != null)
        {
            this.DBOperationOK(this, null);
        }
    }

    public void FireDBOperationErr()
    {
        if (this.DBOperationErr != null)
        {
            this.DBOperationErr(this, null);
        }
    }

    public CShape()
    {
        ShapeID = Guid.NewGuid();
        ImportantPoints = new PointF[0];
        RotateAtPoint = default;
        Shapes = new CShape[0];
        _b = new Bitmap[1];
        Layer = SumLayer++;
    }

    public virtual CShape Copy()
    {
        return new CShape();
    }

    public virtual CShape clone()
    {
        CShape cShape = Copy();
        cShape.Layer = SumLayer++;
        cShape.locked = false;
        return cShape;
    }

    public static long NewNo()
    {
        return MaxNo++;
    }

    public override string ToString()
    {
        return ShapeName;
    }

    public virtual void AfterLoadMe()
    {
        TranslateMatrix = new Matrix();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        _Brush = Brushes.Azure;
        _Pen = new Pen(pencolor)
        {
            DashStyle = _DashStyle
        };
        if (delregionshapetemp != null)
        {
            DelRegionShape = new List<CShape>(delregionshapetemp);
        }
        delimportant00points = new List<PointF>(delimportant00pointstemp);
        delimportant11points = new List<PointF>(delimportant11pointstemp);
        delregionshapetemp = null;
        delimportant00pointstemp = null;
        delimportant11pointstemp = null;
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            cShape.AfterLoadMe();
        }
        PenWidth = penwidth;
        BrushStyle = bs;
        Color1 = fillcolor1;
        Color2 = fillcolor2;
        if (aimax == aimin && aimin == 0.0)
        {
            aimax = 65535.0;
            aimin = 0.0;
        }
        NeedRefreshShape = true;
    }

    public virtual void AfterSaveMe()
    {
        _b = _bBackup;
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            cShape.AfterSaveMe();
        }
        foreach (CShape item in DelRegionShape)
        {
            item.AfterSaveMe();
        }
    }

    public virtual void BeforeSaveMe()
    {
        pencolor = _Pen.Color;
        penwidth = _Pen.Width;
        bs = BrushStyle;
        fillcolor1 = Color1;
        fillcolor2 = Color2;
        delregionshapetemp = DelRegionShape.ToArray();
        delimportant00pointstemp = delimportant00points.ToArray();
        delimportant11pointstemp = delimportant11points.ToArray();
        _bBackup = (Image[])_b.Clone();
        if (_b != null)
        {
            for (int i = 0; i < _b.Length; i++)
            {
                _b[i] = null;
            }
        }
        CShape[] shapes = Shapes;
        foreach (CShape cShape in shapes)
        {
            cShape.BeforeSaveMe();
        }
        foreach (CShape item in DelRegionShape)
        {
            item.BeforeSaveMe();
        }
        DefaultSize = Size;
        DefaultLocaion = new Point(Convert.ToInt32(Location.X), Convert.ToInt32(Location.Y));
        DefaultAngle = Angel;
    }

    public List<GraphicsPath> DelRegionByShapes(PointF _p00, float Width, float Height)
    {
        Matrix matrix = new();
        List<GraphicsPath> list = new();
        for (int i = 0; i < DelRegionShape.Count; i++)
        {
            matrix.Reset();
            if (Width > 0f)
            {
                if (Height > 0f)
                {
                    matrix.Translate(0f - delimportant00points[i].X, 0f - delimportant00points[i].Y, MatrixOrder.Append);
                    matrix.Scale((0f - Width) / (delimportant00points[i].X - delimportant11points[i].X), (0f - Height) / (delimportant00points[i].Y - delimportant11points[i].Y), MatrixOrder.Append);
                    matrix.Translate(_p00.X, _p00.Y, MatrixOrder.Append);
                }
                else
                {
                    matrix.Translate(0f - delimportant00points[i].X, 0f - delimportant00points[i].Y, MatrixOrder.Append);
                    matrix.Scale((0f - Width) / (delimportant00points[i].X - delimportant11points[i].X), (0f - Height) / (delimportant00points[i].Y - delimportant11points[i].Y), MatrixOrder.Append);
                    matrix.Translate(_p00.X, _p00.Y - Height, MatrixOrder.Append);
                }
            }
            else if (Height > 0f)
            {
                matrix.Translate(0f - delimportant00points[i].X, 0f - delimportant00points[i].Y, MatrixOrder.Append);
                matrix.Scale((0f - Width) / (delimportant00points[i].X - delimportant11points[i].X), (0f - Height) / (delimportant00points[i].Y - delimportant11points[i].Y), MatrixOrder.Append);
                matrix.Translate(_p00.X - Width, _p00.Y, MatrixOrder.Append);
            }
            else
            {
                matrix.Translate(0f - delimportant00points[i].X, 0f - delimportant00points[i].Y, MatrixOrder.Append);
                matrix.Scale((0f - Width) / (delimportant00points[i].X - delimportant11points[i].X), (0f - Height) / (delimportant00points[i].Y - delimportant11points[i].Y), MatrixOrder.Append);
                matrix.Translate(_p00.X - Width, _p00.Y - Height, MatrixOrder.Append);
            }
            GraphicsPath[] array = DelRegionShape[i].HowToDraw().ToArray();
            foreach (GraphicsPath addingPath in array)
            {
                GraphicsPath graphicsPath = new();
                graphicsPath.AddPath(addingPath, connect: false);
                graphicsPath.Transform(matrix);
                list.Add(graphicsPath);
            }
        }
        return list;
    }

    public virtual bool RegionUnionFromShape(CShape theRegionShape)
    {
        theRegionShape.BrushStyle = _BrushStyle.单色填充;
        theRegionShape.Color1 = Color.Transparent;
        delimportant00points.Add(ImportantPoints[0]);
        delimportant11points.Add(ImportantPoints[1]);
        DelRegionShape.Add(theRegionShape);
        return true;
    }

    public virtual List<Brush> HowToFill()
    {
        List<Brush> list = new()
        {
            _Brush
        };
        return list;
    }

    public virtual List<Pen> HowToDrawLine()
    {
        List<Pen> list = new()
        {
            _Pen
        };
        return list;
    }

    public virtual void RefreshBrush()
    {
        if (brushInit)
        {
            needRefreshBrush = true;
            brushInit = false;
        }
        if (!needRefreshBrush)
        {
            return;
        }
        if (_Brush is PathGradientBrush)
        {
            Color color = Color1;
            Color color2 = Color2;
            PathGradientBrush pathGradientBrush = new(HowToDraw()[0])
            {
                CenterColor = color
            };
            Color[] array = (Color[])pathGradientBrush.SurroundColors.Clone();
            for (int i = 0; i < pathGradientBrush.SurroundColors.Length; i++)
            {
                array[i] = color2;
            }
            pathGradientBrush.SurroundColors = array;
            _Brush.Dispose();
            _Brush = pathGradientBrush;
        }
        else if (_Brush is LinearGradientBrush)
        {
            GraphicsPath graphicsPath = HowToDraw()[0];
            PointF[] pts = (PointF[])ImportantPoints.Clone();
            Matrix matrix = TranslateMatrix.Clone();
            matrix.Invert();
            matrix.TransformPoints(pts);
            Color color3 = Color1;
            Color color4 = Color2;
            LinearGradientBrush linearGradientBrush = new(graphicsPath.GetBounds(), color3, color4, FillAngel, isAngleScaleable: true);
            if (BrushStyle == _BrushStyle.百分比填充)
            {
                float[] factors = new float[4] { 1f, 1f, 0f, 0f };
                float[] positions = new float[4]
                {
                    0f,
                    FillBFB / 100f,
                    FillBFB / 100f + 0.01f,
                    1f
                };
                Blend blend = new()
                {
                    Factors = factors,
                    Positions = positions
                };
                linearGradientBrush.Blend = blend;
            }
            _Brush.Dispose();
            _Brush = linearGradientBrush;
        }
        else if (_Brush is HatchBrush)
        {
            HatchBrush brush = new(hatchstyle, Color1, Color2);
            _Brush.Dispose();
            _Brush = brush;
        }
        needRefreshBrush = false;
    }

    public virtual bool Mirror(int v)
    {
        PointF pointF = new(ImportantPoints[0].X / 2f + ImportantPoints[1].X / 2f, ImportantPoints[0].Y / 2f + ImportantPoints[1].Y / 2f);
        if (v == 0)
        {
            VMirror = !VMirror;
            for (int i = 0; i < ImportantPoints.Length; i++)
            {
                ImportantPoints[i].X = pointF.X * 2f - ImportantPoints[i].X;
            }
            RotateAtPoint.X = pointF.X * 2f - RotateAtPoint.X;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        if (v == 1)
        {
            HMirror = !HMirror;
            for (int j = 0; j < ImportantPoints.Length; j++)
            {
                ImportantPoints[j].Y = pointF.Y * 2f - ImportantPoints[j].Y;
            }
            RotateAtPoint.Y = pointF.Y * 2f - RotateAtPoint.Y;
            RotateAngel = 0f - RotateAngel;
            TranslateMatrix.Reset();
            TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        }
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        PointF[] array = (PointF[])ImportantPoints.Clone();
        Matrix matrix = TranslateMatrix.Clone();
        matrix.Invert();
        matrix.TransformPoints(array);
        ref PointF reference = ref array[0];
        reference = new PointF(float.MaxValue, float.MaxValue);
        ref PointF reference2 = ref array[1];
        reference2 = new PointF(float.MinValue, float.MinValue);
        for (int k = 0; k < array.Length - 8; k++)
        {
            array[0].X = ((array[0].X < array[k + 8].X) ? array[0].X : array[k + 8].X);
            array[0].Y = ((array[0].Y < array[k + 8].Y) ? array[0].Y : array[k + 8].Y);
            array[1].X = ((array[1].X > array[k + 8].X) ? array[1].X : array[k + 8].X);
            array[1].Y = ((array[1].Y > array[k + 8].Y) ? array[1].Y : array[k + 8].Y);
        }
        ref PointF reference3 = ref array[2];
        reference3 = new PointF(array[1].X, array[0].Y);
        ref PointF reference4 = ref array[3];
        reference4 = new PointF(array[0].X, array[1].Y);
        ref PointF reference5 = ref array[4];
        reference5 = new PointF(array[0].X / 2f + array[1].X / 2f, array[0].Y);
        ref PointF reference6 = ref array[5];
        reference6 = new PointF(array[0].X / 2f + array[1].X / 2f, array[1].Y);
        ref PointF reference7 = ref array[6];
        reference7 = new PointF(array[0].X, array[0].Y / 2f + array[1].Y / 2f);
        ref PointF reference8 = ref array[7];
        reference8 = new PointF(array[1].X, array[0].Y / 2f + array[1].Y / 2f);
        matrix.Reset();
        matrix.RotateAt(RotateAngel, point);
        TranslateMatrix.TransformPoints(array);
        ImportantPoints = (PointF[])array.Clone();
        NeedRefreshShape = true;
        return true;
    }

    public virtual bool AddPoint(PointF NewPoint)
    {
        return false;
    }

    public virtual bool MouseOnMe(PointF ThePoint)
    {
        if (ImportantPoints.Length < 9)
        {
            return false;
        }
        bool result = false;
        foreach (GraphicsPath item in HowToDraw())
        {
            if (item.IsVisible(ThePoint) || item.IsVisible(new PointF(ThePoint.X - 5f, ThePoint.Y - 5f)) || item.IsVisible(new PointF(ThePoint.X, ThePoint.Y - 5f)) || item.IsVisible(new PointF(ThePoint.X + 5f, ThePoint.Y - 5f)) || item.IsVisible(new PointF(ThePoint.X - 5f, ThePoint.Y)) || item.IsVisible(new PointF(ThePoint.X, ThePoint.Y)) || item.IsVisible(new PointF(ThePoint.X + 5f, ThePoint.Y)) || item.IsVisible(new PointF(ThePoint.X - 5f, ThePoint.Y + 5f)) || item.IsVisible(new PointF(ThePoint.X, ThePoint.Y + 5f)) || item.IsVisible(new PointF(ThePoint.X + 5f, ThePoint.Y + 5f)))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    public virtual bool DrawMe(Graphics g)
    {
        try
        {
            RefreshBrush();
        }
        catch
        {
        }
        List<GraphicsPath> list = HowToDraw();
        if (list.Count > 0)
        {
            g.FillPath(_Brush, list[0]);
            g.DrawPath(_Pen, list[0]);
        }
        return true;
    }

    public virtual bool DrawMe(Graphics g, bool trueorfalse)
    {
        if (!visible)
        {
            return false;
        }
        RefreshBrush();
        List<GraphicsPath> list = HowToDraw();
        if (list.Count > 0)
        {
            g.FillPath(_Brush, list[0]);
            g.DrawPath(_Pen, list[0]);
            DrawRectLine(g, trueorfalse);
        }
        return true;
    }

    public virtual bool DrawRectLine(Graphics g, bool trueorfalse)
    {
        if (HowToDraw().Count == 0 || !trueorfalse)
        {
            return false;
        }
        Pen pen = new(Color.White, 5f);
        RectangleF bounds = HowToDraw()[0].GetBounds();
        g.DrawRectangle(pen, Rectangle.Ceiling(new RectangleF(bounds.Location - new SizeF(5f, 5f), new SizeF(bounds.Width + 10f, bounds.Height + 10f))));
        return true;
    }

    public virtual List<GraphicsPath> HowToDraw()
    {
        return new List<GraphicsPath>();
    }

    public virtual List<Bitmap> GetImages()
    {
        List<Bitmap> list = new();
        Image[] b = _b;
        for (int i = 0; i < b.Length; i++)
        {
            Bitmap item = (Bitmap)b[i];
            list.Add(item);
        }
        return list;
    }

    public virtual bool DrawSelect(Graphics g)
    {
        return false;
    }

    public virtual bool RotateTheAngel(float TheAngel)
    {
        RotateAngel += TheAngel;
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(ImportantPoints);
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        TranslateMatrix.TransformPoints(ImportantPoints);
        return true;
    }

    public virtual int EditPoint(PointF OldPoint, PointF NewPoint, int r)
    {
        return -1;
    }

    protected void PartEditPoint(ref int r, PointF[] temporpoints, PointF[] oldnewpoints, ref PointF drotateatnangel)
    {
        if (r == 0 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[0].X && oldnewpoints[0].X - 5f < temporpoints[0].X && oldnewpoints[0].Y + 5f > temporpoints[0].Y && oldnewpoints[0].Y - 5f < temporpoints[0].Y))
        {
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X + 2f > temporpoints[1].X)
                {
                    oldnewpoints[1].X += temporpoints[0].X - oldnewpoints[0].X;
                    oldnewpoints[0].X = temporpoints[0].X;
                }
                if (oldnewpoints[1].Y + 2f > temporpoints[1].Y)
                {
                    oldnewpoints[1].Y += temporpoints[0].Y - oldnewpoints[0].Y;
                    oldnewpoints[0].Y = temporpoints[0].Y;
                }
            }
            r = 0;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X + 2f > temporpoints[1].X)
                {
                    oldnewpoints[1].X = temporpoints[1].X - 2f;
                }
                for (int i = 8; i < temporpoints.Length; i++)
                {
                    temporpoints[i].X = temporpoints[i].X + (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (temporpoints[i].X - temporpoints[1].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[0].X += oldnewpoints[1].X - temporpoints[0].X;
            }
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].Y + 2f > temporpoints[1].Y)
                {
                    oldnewpoints[1].Y = temporpoints[1].Y - 2f;
                }
                for (int j = 2; j < temporpoints.Length; j++)
                {
                    temporpoints[j].Y = temporpoints[j].Y + (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (temporpoints[j].Y - temporpoints[1].Y);
                }
                drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
                temporpoints[0].Y += oldnewpoints[1].Y - temporpoints[0].Y;
            }
        }
        if (r == 11 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[1].X && oldnewpoints[0].X - 5f < temporpoints[1].X && oldnewpoints[0].Y + 5f > temporpoints[1].Y && oldnewpoints[0].Y - 5f < temporpoints[1].Y))
        {
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (!(oldnewpoints[1].X - 2f < temporpoints[0].X))
                {
                    oldnewpoints[1].X += temporpoints[1].X - oldnewpoints[0].X;
                    oldnewpoints[0].X = temporpoints[1].X;
                }
                if (!(oldnewpoints[1].Y - 2f < temporpoints[0].Y))
                {
                    oldnewpoints[1].Y += temporpoints[1].Y - oldnewpoints[0].Y;
                    oldnewpoints[0].Y = temporpoints[1].Y;
                }
            }
            r = 11;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X - 2f < temporpoints[0].X)
                {
                    oldnewpoints[1].X = temporpoints[0].X + 2f;
                }
                for (int k = 2; k < temporpoints.Length; k++)
                {
                    temporpoints[k].X = temporpoints[k].X + (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[1].X - temporpoints[0].X) * (temporpoints[k].X - temporpoints[0].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[1].X += oldnewpoints[1].X - temporpoints[1].X;
            }
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].Y - 2f < temporpoints[0].Y)
                {
                    oldnewpoints[1].Y = temporpoints[0].Y + 2f;
                }
                for (int l = 2; l < temporpoints.Length; l++)
                {
                    temporpoints[l].Y = temporpoints[l].Y + (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[1].Y - temporpoints[0].Y) * (temporpoints[l].Y - temporpoints[0].Y);
                }
                drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
                temporpoints[1].Y += oldnewpoints[1].Y - temporpoints[1].Y;
            }
        }
        if (r == 1 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[0].X && oldnewpoints[0].X - 5f < temporpoints[0].X && oldnewpoints[0].Y + 5f > temporpoints[1].Y && oldnewpoints[0].Y - 5f < temporpoints[1].Y))
        {
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (!(oldnewpoints[1].X + 2f > temporpoints[1].X))
                {
                    oldnewpoints[1].X += temporpoints[0].X - oldnewpoints[0].X;
                    oldnewpoints[0].X = temporpoints[0].X;
                }
                if (!(oldnewpoints[1].Y - 2f < temporpoints[0].Y))
                {
                    oldnewpoints[1].Y += temporpoints[1].Y - oldnewpoints[0].Y;
                    oldnewpoints[0].Y = temporpoints[1].Y;
                }
            }
            r = 1;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X + 2f > temporpoints[1].X)
                {
                    oldnewpoints[1].X = temporpoints[1].X - 2f;
                }
                for (int m = 2; m < temporpoints.Length; m++)
                {
                    temporpoints[m].X = temporpoints[m].X + (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (temporpoints[m].X - temporpoints[1].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[0].X += oldnewpoints[1].X - temporpoints[0].X;
            }
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].Y - 2f < temporpoints[0].Y)
                {
                    oldnewpoints[1].Y = temporpoints[0].Y + 2f;
                }
                for (int n = 2; n < temporpoints.Length; n++)
                {
                    temporpoints[n].Y = temporpoints[n].Y + (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[1].Y - temporpoints[0].Y) * (temporpoints[n].Y - temporpoints[0].Y);
                }
                drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
                temporpoints[1].Y += oldnewpoints[1].Y - temporpoints[1].Y;
            }
        }
        if (r == 10 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[1].X && oldnewpoints[0].X - 5f < temporpoints[1].X && oldnewpoints[0].Y + 5f > temporpoints[0].Y && oldnewpoints[0].Y - 5f < temporpoints[0].Y))
        {
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (!(oldnewpoints[1].X - 2f < temporpoints[0].X))
                {
                    oldnewpoints[1].X += temporpoints[1].X - oldnewpoints[0].X;
                    oldnewpoints[0].X = temporpoints[1].X;
                }
                if (oldnewpoints[1].Y + 2f > temporpoints[1].Y)
                {
                    oldnewpoints[1].Y += temporpoints[0].Y - oldnewpoints[0].Y;
                    oldnewpoints[0].Y = temporpoints[0].Y;
                }
            }
            r = 10;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X - 2f < temporpoints[0].X)
                {
                    oldnewpoints[1].X = temporpoints[0].X + 2f;
                }
                for (int num = 2; num < temporpoints.Length; num++)
                {
                    temporpoints[num].X = temporpoints[num].X + (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[1].X - temporpoints[0].X) * (temporpoints[num].X - temporpoints[0].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[1].X += oldnewpoints[1].X - temporpoints[1].X;
            }
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].Y + 2f > temporpoints[1].Y)
                {
                    oldnewpoints[1].Y = temporpoints[1].Y - 2f;
                }
                for (int num2 = 2; num2 < temporpoints.Length; num2++)
                {
                    temporpoints[num2].Y = temporpoints[num2].Y + (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (temporpoints[num2].Y - temporpoints[1].Y);
                }
                drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
                temporpoints[0].Y += oldnewpoints[1].Y - temporpoints[0].Y;
            }
        }
        if (r == 5 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[0].X && oldnewpoints[0].X - 5f < temporpoints[0].X && oldnewpoints[0].Y + 5f > temporpoints[0].Y / 2f + temporpoints[1].Y / 2f && oldnewpoints[0].Y - 5f < temporpoints[0].Y / 2f + temporpoints[1].Y / 2f))
        {
            if (oldnewpoints[0] != oldnewpoints[1] && !(oldnewpoints[1].X + 2f > temporpoints[1].X))
            {
                oldnewpoints[1].X += temporpoints[0].X - oldnewpoints[0].X;
                oldnewpoints[0].X = temporpoints[0].X;
            }
            r = 5;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X + 2f > temporpoints[1].X)
                {
                    oldnewpoints[1].X = temporpoints[1].X - 2f;
                }
                for (int num3 = 2; num3 < temporpoints.Length; num3++)
                {
                    temporpoints[num3].X = temporpoints[num3].X + (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (temporpoints[num3].X - temporpoints[1].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[0].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[0].X += oldnewpoints[1].X - temporpoints[0].X;
            }
        }
        if (r == 15 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[1].X && oldnewpoints[0].X - 5f < temporpoints[1].X && oldnewpoints[0].Y + 5f > temporpoints[0].Y / 2f + temporpoints[1].Y / 2f && oldnewpoints[0].Y - 5f < temporpoints[0].Y / 2f + temporpoints[1].Y / 2f))
        {
            if (oldnewpoints[0] != oldnewpoints[1] && !(oldnewpoints[1].X - 2f < temporpoints[0].X))
            {
                oldnewpoints[1].X += temporpoints[1].X - oldnewpoints[0].X;
                oldnewpoints[0].X = temporpoints[1].X;
            }
            r = 15;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].X - 2f < temporpoints[0].X)
                {
                    oldnewpoints[1].X = temporpoints[0].X + 2f;
                }
                for (int num4 = 2; num4 < temporpoints.Length; num4++)
                {
                    temporpoints[num4].X = temporpoints[num4].X + (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[1].X - temporpoints[0].X) * (temporpoints[num4].X - temporpoints[0].X);
                }
                drotateatnangel.X = (oldnewpoints[1].X - temporpoints[1].X) / (temporpoints[0].X - temporpoints[1].X) * (RotateAtPoint.X - temporpoints[1].X);
                temporpoints[1].X += oldnewpoints[1].X - temporpoints[1].X;
            }
        }
        if (r == 50 || (r == -1 && oldnewpoints[0].X + 5f > temporpoints[0].X / 2f + temporpoints[1].X / 2f && oldnewpoints[0].X - 5f < temporpoints[0].X / 2f + temporpoints[1].X / 2f && oldnewpoints[0].Y + 5f > temporpoints[0].Y && oldnewpoints[0].Y - 5f < temporpoints[0].Y))
        {
            if (oldnewpoints[0] != oldnewpoints[1] && !(oldnewpoints[1].Y + 2f > temporpoints[1].Y))
            {
                oldnewpoints[1].Y += temporpoints[0].Y - oldnewpoints[0].Y;
                oldnewpoints[0].Y = temporpoints[0].Y;
            }
            r = 50;
            if (oldnewpoints[0] != oldnewpoints[1])
            {
                if (oldnewpoints[1].Y + 2f > temporpoints[1].Y)
                {
                    oldnewpoints[1].Y = temporpoints[1].Y - 2f;
                }
                for (int num5 = 2; num5 < temporpoints.Length; num5++)
                {
                    temporpoints[num5].Y = temporpoints[num5].Y + (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (temporpoints[num5].Y - temporpoints[1].Y);
                }
                drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[0].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
                temporpoints[0].Y += oldnewpoints[1].Y - temporpoints[0].Y;
            }
        }
        if (r != 51 && (r != -1 || !(oldnewpoints[0].X + 5f > temporpoints[0].X / 2f + temporpoints[1].X / 2f) || !(oldnewpoints[0].X - 5f < temporpoints[0].X / 2f + temporpoints[1].X / 2f) || !(oldnewpoints[0].Y + 5f > temporpoints[1].Y) || !(oldnewpoints[0].Y - 5f < temporpoints[1].Y)))
        {
            return;
        }
        if (oldnewpoints[0] != oldnewpoints[1] && !(oldnewpoints[1].Y - 2f < temporpoints[0].Y))
        {
            oldnewpoints[1].Y += temporpoints[1].Y - oldnewpoints[0].Y;
            oldnewpoints[0].Y = temporpoints[1].Y;
        }
        r = 51;
        if (oldnewpoints[0] != oldnewpoints[1])
        {
            if (oldnewpoints[1].Y - 2f < temporpoints[0].Y)
            {
                oldnewpoints[1].Y = temporpoints[0].Y + 2f;
            }
            for (int num6 = 2; num6 < temporpoints.Length; num6++)
            {
                temporpoints[num6].Y = temporpoints[num6].Y + (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[1].Y - temporpoints[0].Y) * (temporpoints[num6].Y - temporpoints[0].Y);
            }
            drotateatnangel.Y = (oldnewpoints[1].Y - temporpoints[1].Y) / (temporpoints[0].Y - temporpoints[1].Y) * (RotateAtPoint.Y - temporpoints[1].Y);
            temporpoints[1].Y += oldnewpoints[1].Y - temporpoints[1].Y;
        }
    }

    public virtual bool EditLocation(PointF OldPoint, PointF NewPoint)
    {
        return false;
    }

    public virtual bool EditLocationByPoint(PointF OldPoint, PointF NewPoint)
    {
        if (locked && Operation.bEditEnvironment)
        {
            return false;
        }
        NeedRefreshShape = true;
        PointF[] array = (PointF[])ImportantPoints.Clone();
        PointF[] array2 = new PointF[2] { OldPoint, NewPoint };
        TranslateMatrix.Invert();
        TranslateMatrix.TransformPoints(array);
        TranslateMatrix.TransformPoints(array2);
        PointF point = new(RotateAtPoint.X, RotateAtPoint.Y);
        for (int i = 0; i < array.Length; i++)
        {
            array[i].X += array2[1].X - array2[0].X;
            array[i].Y += array2[1].Y - array2[0].Y;
        }
        RotateAtPoint.X += NewPoint.X - OldPoint.X;
        RotateAtPoint.Y += NewPoint.Y - OldPoint.Y;
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, point);
        TranslateMatrix.TransformPoints(array);
        ImportantPoints = (PointF[])array.Clone();
        TranslateMatrix.Reset();
        TranslateMatrix.RotateAt(RotateAngel, RotateAtPoint);
        return true;
    }

    public virtual List<PointF> ValuePoints()
    {
        List<PointF> list = new();
        for (int i = 0; i < ImportantPoints.Length; i++)
        {
            list.Add(ImportantPoints[i]);
        }
        return list;
    }

    protected virtual string MakeScriptForShape()
    {
        StringBuilder stringBuilder = new();
        if (eventBindDict != null)
        {
            foreach (string key in eventBindDict.Keys)
            {
                string text;
                if ((text = key) == null || !(text == "Click"))
                {
                    continue;
                }
                int num = 0;
                stringBuilder.AppendLine("function " + Name + "_event_onClick(){");
                stringBuilder.AppendLine("\tvar step=\"0\";");
                stringBuilder.AppendLine("\tlabelFinish:");
                stringBuilder.AppendLine("\twhile(true)");
                stringBuilder.AppendLine("\t{");
                stringBuilder.AppendLine("\t\tswitch(step) {");
                stringBuilder.AppendLine("\t\tcase \"0\":");
                Regex regex = new("\\[.*?\\]");
                Regex regex2 = new("(\\b\\w+)\\.(\\b\\w+)\\.(\\b\\w+)\\((.*)\\)");
                foreach (EventSetItem item in eventBindDict[key])
                {
                    string text2 = item.Condition;
                    if (text2 == null)
                    {
                        text2 = "true";
                    }
                    else
                    {
                        List<string[]> replaceFunction = new();
                        GetReplaceJSFunStr(regex2, text2, ref replaceFunction);
                        foreach (string[] item2 in replaceFunction)
                        {
                            text2 = text2.Replace(item2[0], "parent.GetPage(\"" + item2[1] + "\")(\"#" + item2[2] + "\").data(\"" + item2[3] + "\")(" + item2[4] + ")");
                        }
                        text2 = text2.Replace("System.", "parent.");
                        List<string> list = new();
                        foreach (Match item3 in regex.Matches(text2))
                        {
                            if (!list.Contains(item3.Value))
                            {
                                list.Add(item3.Value);
                                text2 = text2.Replace(item3.Value, "parent.VarOperation.GetValueByName(\"" + item3.Value + "\")");
                            }
                        }
                        list.Clear();
                    }
                    if (item.OperationType == "定义标签")
                    {
                        stringBuilder.AppendLine("\t\tcase \"" + item.FromObject + "\":");
                    }
                    else if (item.OperationType == "跳转标签")
                    {
                        stringBuilder.AppendLine("\t\tcase \"" + num++ + "\":");
                        stringBuilder.AppendLine("\t\t\tif(" + text2 + ")");
                        stringBuilder.AppendLine("\t\t\t{");
                        stringBuilder.AppendLine("\t\t\t\tstep=\"" + item.FromObject + "\";");
                        stringBuilder.AppendLine("\t\t\t\tbreak;");
                        stringBuilder.AppendLine("\t\t\t}");
                    }
                    if (item.OperationType == "变量赋值")
                    {
                        int num2 = ++num;
                        stringBuilder.AppendLine("\t\tcase \"" + num2 + "\":");
                        stringBuilder.AppendLine("\t\t\tif(" + text2 + ")");
                        stringBuilder.AppendLine("\t\t\t{");
                        string text3 = item.FromObject;
                        if (text3.Contains("eval") && text3.StartsWith("eval"))
                        {
                            stringBuilder.AppendLine(text3);
                        }
                        else
                        {
                            List<string[]> replaceFunction2 = new();
                            GetReplaceJSFunStr(regex2, text3, ref replaceFunction2);
                            foreach (string[] item4 in replaceFunction2)
                            {
                                text3 = text3.Replace(item4[0], "parent.GetPage(\"" + item4[1] + "\")(\"#" + item4[2] + "\").data(\"" + item4[3] + "\")(" + item4[4] + ")");
                            }
                            text3 = text3.Replace("System.", "parent.");
                            List<string> list2 = new();
                            foreach (Match item5 in regex.Matches(text3))
                            {
                                if (!list2.Contains(item5.Value))
                                {
                                    list2.Add(item5.Value);
                                    text3 = text3.Replace(item5.Value, "parent.VarOperation.GetValueByName(\"" + item5.Value + "\")");
                                }
                            }
                            list2.Clear();
                            if (item.ToObject.Key != "")
                            {
                                stringBuilder.AppendLine("\t\t\t\tparent.VarOperation.SetValueByName(\"[" + item.ToObject.Key + "]\"," + text3 + ")");
                            }
                            else
                            {
                                stringBuilder.AppendLine("\t\t\t\t" + text3);
                            }
                        }
                        stringBuilder.AppendLine("\t\t\t}");
                    }
                    else if (item.OperationType == "属性赋值")
                    {
                        int num3 = ++num;
                        stringBuilder.AppendLine("\t\tcase \"" + num3 + "\":");
                        stringBuilder.AppendLine("\t\t\tif(" + text2 + ")");
                        stringBuilder.AppendLine("\t\t\t{");
                        string text4 = item.FromObject;
                        List<string[]> replaceFunction3 = new();
                        GetReplaceJSFunStr(regex2, text4, ref replaceFunction3);
                        foreach (string[] item6 in replaceFunction3)
                        {
                            text4 = text4.Replace(item6[0], "parent.GetPage(\"" + item6[1] + "\")(\"#" + item6[2] + "\").data(\"" + item6[3] + "\")(" + item6[4] + ")");
                        }
                        text4 = text4.Replace("System.", "parent.");
                        List<string> list3 = new();
                        foreach (Match item7 in regex.Matches(text4))
                        {
                            if (!list3.Contains(item7.Value))
                            {
                                list3.Add(item7.Value);
                                text4 = text4.Replace(item7.Value, "parent.VarOperation.GetValueByName(\"" + item7.Value + "\")");
                            }
                        }
                        list3.Clear();
                        string[] array = item.ToObject.Key.Split('.');
                        stringBuilder.AppendLine("\t\t\t\tparent.GetPage(\"" + array[0] + "\")(\"#" + array[1] + "\").data(\"set_" + array[2] + "\")(" + text4 + ");");
                        stringBuilder.AppendLine("\t\t\t}");
                    }
                    else
                    {
                        if (!(item.OperationType == "方法调用"))
                        {
                            continue;
                        }
                        int num4 = ++num;
                        stringBuilder.AppendLine("\t\tcase \"" + num4 + "\":");
                        stringBuilder.AppendLine("\t\t\tif(" + text2 + ")");
                        stringBuilder.AppendLine("\t\t\t{");
                        stringBuilder.AppendLine("\t\t\t");
                        if (item.ToObject.Key == "")
                        {
                            string[] array2 = item.FromObject.Split('.');
                            StringBuilder stringBuilder2 = new();
                            foreach (KVPart<string, string> para in item.Paras)
                            {
                                string text5 = para.Key.Replace("System.", "parent.");
                                List<string> list4 = new();
                                foreach (Match item8 in regex.Matches(text5))
                                {
                                    if (!list4.Contains(item8.Value))
                                    {
                                        list4.Add(item8.Value);
                                        text5 = text5.Replace(item8.Value, "parent.VarOperation.GetValueByName(\"" + item8.Value + "\")");
                                    }
                                }
                                list4.Clear();
                                stringBuilder2.Append("," + text5);
                            }
                            if (stringBuilder2.Length > 0)
                            {
                                stringBuilder2.Remove(0, 1);
                            }
                            stringBuilder.AppendLine("parent.GetPage(\"" + array2[0] + "\")(\"#" + array2[1] + "\").data(\"" + array2[2] + "\")(" + stringBuilder2.ToString() + ");");
                        }
                        else
                        {
                            string[] array3 = item.FromObject.Split('.');
                            StringBuilder stringBuilder3 = new();
                            foreach (KVPart<string, string> para2 in item.Paras)
                            {
                                string text6 = para2.Key.Replace("System.", "parent.");
                                List<string> list5 = new();
                                foreach (Match item9 in regex.Matches(text6))
                                {
                                    if (!list5.Contains(item9.Value))
                                    {
                                        list5.Add(item9.Value);
                                        text6 = text6.Replace(item9.Value, "parent.VarOperation.GetValueByName(\"" + item9.Value + "\")");
                                    }
                                }
                                list5.Clear();
                                stringBuilder3.Append("," + text6);
                            }
                            if (stringBuilder3.Length > 0)
                            {
                                stringBuilder3.Remove(0, 1);
                            }
                            stringBuilder.AppendLine(string.Concat("\t\t\t\tparent.VarOperation.SetValueByName(\"[", item.ToObject, "]\",parent.GetPage(\"", array3[0], "\")(\"#", array3[1], "\").data(\"", array3[2], "\")(", stringBuilder3.ToString(), "));"));
                        }
                        stringBuilder.AppendLine("\t\t\t}");
                    }
                }
                stringBuilder.AppendLine("\t\t\tbreak labelFinish;");
                stringBuilder.AppendLine("\t\t}");
                stringBuilder.AppendLine("\t}");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("$(\"#" + ShapeName + "\").data(\"Click\"," + ShapeName + "_event_onClick)");
            }
        }
        return stringBuilder.ToString();
    }

    public static void GetReplaceJSFunStr(Regex regex2, string condition, ref List<string[]> replaceFunction)
    {
        foreach (Match item in regex2.Matches(condition))
        {
            replaceFunction.Add(new string[5]
            {
                item.Value,
                item.Groups[1].Value,
                item.Groups[2].Value,
                item.Groups[3].Value,
                item.Groups[4].Value
            });
            GetReplaceJSFunStr(regex2, item.Groups[4].Value, ref replaceFunction);
        }
    }
}
