using CommonSnappableTypes;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using ShapeRuntime;
using ShapeRuntime.DBAnimation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace HMIWeb;

[Guid("0F48C10C-5118-40d5-8A43-F7CFD1336C4D")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class MainControl : UserControl, IMessageFilter
{
    public delegate Type GetTypeDele(string typeName);

    public delegate Assembly GetAssemblyDele(string assemblyName);

    private delegate void CallInformationLabelDelegate(string info, bool visible);

    private delegate void CallInitPageControlDele(string pagename, DataFile df);

    public delegate object CallRuntimeDBOperationDele(string type, string sqlcmd);

    public delegate Dictionary<string, object> CallRuntimeServerLogicDele(ServerLogicRequest slr);

    private delegate byte[] CallAnsyncUploadDataDele(string url, byte[] datas);

    private delegate void DBAnsyncOperationCompleteDelegate(object obj);

    public CAuthoritySeiallize cas;

    public string strCurrentUser;

    public HMIProjectFile dhp;

    public Type G;

    public Type Clienttype;

    public Dictionary<int, object> ParaDict = new();

    public object[] Varlist;

    private readonly List<DataFile> dflist = new();

    private readonly Dictionary<string, DataFile> SD = new();

    private readonly Dictionary<string, object> SO = new();

    private readonly System.Windows.Forms.Timer timer = new();

    private readonly System.Windows.Forms.Timer systimer = new();

    private readonly System.Windows.Forms.Timer comtimer = new();

    private readonly Dictionary<string, VarTableItem> DicIO = new();

    private readonly Dictionary<int, VarTableItem> DicMe = new();

    public Thread datathread;

    public Thread databasethread;

    public bool canwork = true;

    private readonly int MaxID;

    private object GlobleObj;

    private ScriptEngine m_ScriptEngine;

    private readonly XmlDocument xmldoc = new();

    private VarTable varForm;

    public bool isauthority = true;

    private bool needdraw;

    private readonly List<VarTableItem> ids = new();

    private readonly ManualResetEvent _event = new(initialState: false);

    public string installpath;

    public string projectpath;

    public string projectname;

    public MouseEventArgs Mouse = new(MouseButtons.Left, 0, 0, 0, 0);

    private float Mousedownx;

    private float Mousedowny;

    private new CShape Focus;

    private CShape Moving;

    private readonly float zoomX = 1f;

    private readonly float zoomY = 1f;

    private List<DataFile> dfs;

    private Point ControlMoveTempPoint;

    private bool ControlMoveTempMouseDown;

    public List<CShape> lastMouseDownShapeList = new();

    private bool m_VisibleChangeIn;

    public int AlarmState;

    private bool alreadyfirstEnterFocus;

    private bool inresize;

    public bool dead;

    private Size oldsize;

    private int LastPageHeight;

    private bool m_NotVisibleChange;

    public List<string> pageNeedResize = new();

    public string url = "";

    private string absoluteUrl = "";

    public string ipaddress = "127.0.0.1";

    public string port = "80";

    public List<string> PageNotResize = new();

    private readonly Dictionary<string, float> dic_zoomx = new();

    private readonly Dictionary<string, float> dic_zoomy = new();

    private readonly DateTime begintime = DateTime.Now;

    private readonly Dictionary<string, object> tempValueDict = new();

    private string GlobalScriptBackUp = "";

    private bool isForm;

    private readonly System.Windows.Forms.Timer timer2Delay = new();

    private int nDelayTick;

    private readonly InitForm init = new();

    private readonly Dictionary<string, DateTime> fileTimeDict = new();

    private bool bInitOK;

    private bool bOnInit = true;

    private DbConnType dbType;

    private string DBConnStr;

    private DataTable LastTable;

    public object ExternClass;

    public int DeviceID = -1;

    public int DeviceState = -1;

    public Dictionary<string, Dictionary<string, List<object>>> pdc = new();

    private readonly Dictionary<string, int> Dic_IsPageRunLogic = new();

    public bool bStartProject;

    public Dictionary<string, string> dicAuthority = new();

    private Label label1;

    public string RequestUrl
    {
        get
        {
            return url;
        }
        set
        {
            url = value;
        }
    }

    public event CallRuntimeDBOperationDele CallRuntimeDBOperation;

    public event CallRuntimeServerLogicDele CallRuntimeServerLogic;

    public event EventHandler InitOK;

    public event EventHandler FullScreenEvent;

    public void YMQH(CShape s)
    {
        if (!s.ymqh)
        {
            return;
        }
        if (s.ymqhxianshi != null)
        {
            for (int i = 0; i < s.ymqhxianshi.Length; i++)
            {
                SetPageVisible(s.ymqhxianshi[i], Visible: true);
            }
        }
        if (s.ymqhyincang != null)
        {
            for (int j = 0; j < s.ymqhyincang.Length; j++)
            {
                SetPageVisible(s.ymqhyincang[j], Visible: false);
            }
        }
    }

    public void YC(CShape s)
    {
        float num = 0f;
        try
        {
            num = Convert.ToSingle(Eval(s.txycbianliang));
        }
        catch
        {
        }
        if (s.txycnotbianliang)
        {
            if (num != 0f)
            {
                s.Visible = false;
            }
            if (num == 0f)
            {
                s.Visible = true;
            }
        }
        else
        {
            if (num == 0f)
            {
                s.Visible = false;
            }
            if (num != 0f)
            {
                s.Visible = true;
            }
        }
    }

    public void LD(CShape s)
    {
        if (s is CRectangle rectangle)
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(rectangle.ldbianliang));
            }
            catch
            {
            }
            if (num != 0f)
            {
                rectangle.flow();
            }
        }
    }

    public void ZFXS(CShape s)
    {
        if (s is not CString)
        {
            return;
        }
        string displayStr = "";
        try
        {
            object obj = Eval(s.zfcscbianliang);
            if (obj != null)
            {
                displayStr = obj.ToString();
            }
        }
        catch
        {
        }
        ((CString)s).DisplayStr = displayStr;
    }

    public void MNSC(CShape s)
    {
        if (s is not CString)
        {
            return;
        }
        try
        {
            double value = 0.0;
            try
            {
                value = Convert.ToDouble(Eval(s.aobianliang));
            }
            catch
            {
            }
            if (s.aojingdu != -1)
            {
                ((CString)s).DisplayStr = value.ToString("F" + s.aojingdu);
                return;
            }
            byte[] bytes = BitConverter.GetBytes(value);
            ((CString)s).DisplayStr = ((BitConverter.ToString(bytes, 3, 1) == "00") ? "" : BitConverter.ToString(bytes, 3, 1)) + ((BitConverter.ToString(bytes, 2, 1) == "00") ? "" : BitConverter.ToString(bytes, 2, 1)) + ((BitConverter.ToString(bytes, 1, 1) == "00") ? "" : BitConverter.ToString(bytes, 1, 1)) + BitConverter.ToString(bytes, 0, 1);
        }
        catch
        {
            ((CString)s).DisplayStr = "";
        }
    }

    public void KGSC(CShape s)
    {
        if (s is CString)
        {
            bool flag = false;
            try
            {
                flag = Convert.ToBoolean(Eval(s.dobianlaing));
            }
            catch
            {
            }
            if (flag && s.dotishion != "")
            {
                ((CString)s).DisplayStr = s.dotishion;
            }
            else if (!flag && s.dotishioff != "")
            {
                ((CString)s).DisplayStr = s.dotishioff;
            }
            else
            {
                ((CString)s).DisplayStr = flag.ToString();
            }
        }
    }

    public void SPYD(CShape s)
    {
        if (s.sptzlock || s.spydzhimax - s.spydzhimin == 0f)
        {
            return;
        }
        try
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(Eval(s.spydbianliang));
            }
            catch
            {
            }
            int num2 = num;
            if (num2 > (int)s.spydzhimax)
            {
                num2 = (int)s.spydzhimax;
            }
            else if (num2 < (int)s.spydzhimin)
            {
                num2 = (int)s.spydzhimin;
            }
            float num3 = s.spydxiangsumin + (float)s.DefaultLocaion.X + (s.spydxiangsumax - s.spydxiangsumin) * ((float)num2 - s.spydzhimin) / (s.spydzhimax - s.spydzhimin);
            s.EditLocationByPoint(s.Location, new PointF(num3, s.Location.Y));
        }
        catch
        {
        }
    }

    public void CZYD(CShape s)
    {
        if (s.cztzlock || s.czydzhimax - s.czydzhimin == 0f)
        {
            return;
        }
        try
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(Eval(s.czydbianliang));
            }
            catch
            {
            }
            int num2 = num;
            if (num2 > (int)s.czydzhimax)
            {
                num2 = (int)s.czydzhimax;
            }
            else if (num2 < (int)s.czydzhimin)
            {
                num2 = (int)s.czydzhimin;
            }
            float num3 = s.czydxiangsumin + (float)s.DefaultLocaion.Y + (s.czydxiangsumax - s.czydxiangsumin) * ((float)num2 - s.czydzhimin) / (s.czydzhimax - s.czydzhimin);
            s.EditLocationByPoint(s.Location, new PointF(s.Location.X, num3));
        }
        catch
        {
        }
    }

    public void SPTZmousemove(CShape shape, float origx, float curx, ref float tempx)
    {
        if (shape.sptz)
        {
            shape.sptzlock = true;
            if ((double)Math.Abs((shape.Location.X - ((float)shape.sptzyidongmax + origx)) / (shape.Location.X + ((float)shape.sptzyidongmax + origx))) > 1E-06 && shape.Location.X > (float)shape.sptzyidongmax + origx)
            {
                shape.EditLocationByPoint(shape.Location, new PointF((float)shape.sptzyidongmax + origx, shape.Location.Y));
                return;
            }
            if ((double)Math.Abs((shape.Location.X - ((float)shape.sptzyidongmin + origx)) / (shape.Location.X + ((float)shape.sptzyidongmin + origx))) > 1E-06 && shape.Location.X < (float)shape.sptzyidongmin + origx)
            {
                shape.EditLocationByPoint(shape.Location, new PointF((float)shape.sptzyidongmin + origx, shape.Location.Y));
                return;
            }
            shape.EditLocationByPoint(shape.Location, new PointF(shape.Location.X + (curx - tempx), shape.Location.Y));
            tempx = curx;
        }
    }

    public void SZTZmousemove(CShape shape, float origy, float cury, ref float tempy)
    {
        if (shape.cztz)
        {
            shape.cztzlock = true;
            if ((double)Math.Abs((shape.Location.Y - ((float)shape.cztzyidongmax + origy)) / (shape.Location.Y + ((float)shape.cztzyidongmax + origy))) > 1E-06 && shape.Location.Y < (float)shape.cztzyidongmax + origy)
            {
                shape.EditLocationByPoint(shape.Location, new PointF(shape.Location.X, (float)shape.cztzyidongmax + origy));
                return;
            }
            if ((double)Math.Abs((shape.Location.Y - ((float)shape.cztzyidongmin + origy)) / (shape.Location.Y + ((float)shape.cztzyidongmin + origy))) > 1E-06 && shape.Location.Y > (float)shape.cztzyidongmin + origy)
            {
                shape.EditLocationByPoint(shape.Location, new PointF(shape.Location.X, (float)shape.cztzyidongmin + origy));
                return;
            }
            shape.EditLocationByPoint(shape.Location, new PointF(shape.Location.X, shape.Location.Y + (cury - tempy)));
            tempy = cury;
        }
    }

    public void SPTZmouseup(CShape shape)
    {
        if (!shape.sptz || shape.sptzyidongmax - shape.sptzyidongmin == 0)
        {
            return;
        }
        shape.sptzlock = false;
        try
        {
            float num = (shape.Location.X - (float)shape.sptzyidongmin - (float)shape.DefaultLocaion.X) * (float)(shape.sptzzhibianhuamax - shape.sptzzhibianhuamin) / (float)(shape.sptzyidongmax - shape.sptzyidongmin) + (float)shape.sptzzhibianhuamin;
            if (num < (float)shape.sptzzhibianhuamin)
            {
                num = shape.sptzzhibianhuamin;
            }
            else if (num > (float)shape.sptzzhibianhuamax)
            {
                num = shape.sptzzhibianhuamax;
            }
            SetValue(shape.sptzbianliang, num);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void SZTZmouseup(CShape shape)
    {
        if (!shape.cztz || shape.cztzyidongmax - shape.cztzyidongmin == 0)
        {
            return;
        }
        shape.cztzlock = false;
        try
        {
            float num = (shape.Location.Y - (float)shape.cztzyidongmin - (float)shape.DefaultLocaion.Y) * (float)(shape.cztzzhibianhuamax - shape.cztzzhibianhuamin) / (float)(shape.cztzyidongmax - shape.cztzyidongmin) + (float)shape.cztzzhibianhuamin;
            if (num < (float)shape.cztzzhibianhuamin)
            {
                num = shape.cztzzhibianhuamin;
            }
            else if (num > (float)shape.cztzzhibianhuamax)
            {
                num = shape.cztzzhibianhuamax;
            }
            SetValue(shape.cztzbianliang, num);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void AIinput(CShape shape)
    {
        if (!shape.ai)
        {
            return;
        }
        try
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(shape.aibianliang));
            }
            catch
            {
            }
            float? singleValue = GetSingleValue(shape.aitishi, shape.aimax, shape.aimin, num);
            if (singleValue.HasValue)
            {
                SetValue(shape.aibianliang, singleValue);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void DIinput(CShape shape)
    {
        if (!shape.di)
        {
            return;
        }
        try
        {
            bool? boolValue = GetBoolValue(shape.ditishi, shape.ditishion, shape.ditishioff);
            if (boolValue.HasValue)
            {
                SetValue(shape.dibianlaing, boolValue);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void STRinput(CShape shape)
    {
        if (!shape.zfcsr)
        {
            return;
        }
        try
        {
            string oldvalue = "";
            try
            {
                object obj = Eval(shape.zfcsrbianliang);
                if (obj != null)
                {
                    oldvalue = obj.ToString();
                }
            }
            catch
            {
            }
            string stringValue = GetStringValue(shape.zfcsrtishi, oldvalue);
            if (stringValue != null)
            {
                SetValue(shape.zfcsrbianliang, stringValue);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    public void CIR(CShape s)
    {
        if (s.mbxzzhimax - s.mbxzzhimin != 0f)
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(s.mbxzbianliang));
            }
            catch
            {
            }
            if (num > s.mbxzzhimax)
            {
                num = s.mbxzzhimax;
            }
            if (num < s.mbxzzhimin)
            {
                num = s.mbxzzhimin;
            }
            double num2 = Math.Pow(Math.Pow(s.mbxzzhongxinpianzhiright, 2.0) + Math.Pow(s.mbxzzhongxinpianzhidown, 2.0), 0.5);
            s.RotatePoint = new PointF(Convert.ToSingle((double)s.MidRotatePoint.X + num2 * Math.Cos((double)(s.Angel / 180f) * Math.PI)), Convert.ToSingle((double)s.MidRotatePoint.Y + num2 * Math.Sin((double)(s.Angel / 180f) * Math.PI)));
            s.Angel = s.mbxzjiaodumin + (num - s.mbxzzhimin) / (s.mbxzzhimax - s.mbxzzhimin) * (s.mbxzjiaodumax - s.mbxzjiaodumin);
        }
    }

    public void SPBFB(CShape s)
    {
        if (s.spbfbzhimax - s.spbfbzhimin != 0f)
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(s.spbfbbianliang));
            }
            catch
            {
            }
            float num2 = num;
            if (num2 > s.spbfbzhimax)
            {
                num2 = s.spbfbzhimax;
            }
            if (num2 < s.spbfbzhimin)
            {
                num2 = s.spbfbzhimin;
            }
            if (s.spbfbcankao == 1)
            {
                s.BrushBFB = s.spbfbbaifenbimin + (num2 - s.spbfbzhimin) / (s.spbfbzhimax - s.spbfbzhimin) * (s.spbfbbaifenbimax - s.spbfbbaifenbimin);
            }
            if (s.spbfbcankao == 2)
            {
                s.BrushBFB = 100f - (s.spbfbbaifenbimin + (num2 - s.spbfbzhimin) / (s.spbfbzhimax - s.spbfbzhimin) * (s.spbfbbaifenbimax - s.spbfbbaifenbimin));
            }
        }
    }

    public void CZBFB(CShape s)
    {
        if (s.czbfbzhimax - s.czbfbzhimin != 0f)
        {
            s.FillAngel = 90f;
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(s.czbfbbianliang));
            }
            catch
            {
            }
            float num2 = num;
            if (num2 > s.czbfbzhimax)
            {
                num2 = s.czbfbzhimax;
            }
            if (num2 < s.czbfbzhimin)
            {
                num2 = s.czbfbzhimin;
            }
            if (s.czbfbcankao == 1)
            {
                s.BrushBFB = s.czbfbbaifenbimin + (num2 - s.czbfbzhimin) / (s.czbfbzhimax - s.czbfbzhimin) * (s.czbfbbaifenbimax - s.czbfbbaifenbimin);
            }
            if (s.czbfbcankao == 2)
            {
                s.BrushBFB = 100f - (s.czbfbbaifenbimin + (num2 - s.czbfbzhimin) / (s.czbfbzhimax - s.czbfbzhimin) * (s.czbfbbaifenbimax - s.czbfbbaifenbimin));
            }
        }
    }

    public void KDBH(CShape s)
    {
        if (s.kdbhzhimax - s.kdbhzhimin != 0f && s.kdbhxiangsumax - s.kdbhxiangsumin != 0f)
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(s.kdbhbianliang));
            }
            catch
            {
            }
            float num2 = num;
            if (num2 > s.kdbhzhimax)
            {
                num2 = s.kdbhzhimax;
            }
            if (num2 < s.kdbhzhimin)
            {
                num2 = s.kdbhzhimin;
            }
            if (s.kdbhcankao == 1)
            {
                s.Size = new SizeF(s.kdbhxiangsumin + s.DefaultSize.Width + (num2 - s.kdbhzhimin) / (s.kdbhzhimax - s.kdbhzhimin) * (s.kdbhxiangsumax - s.kdbhxiangsumin), s.Size.Height);
            }
            if (s.kdbhcankao == 2)
            {
                s.Size = new SizeF(s.kdbhxiangsumin + s.DefaultSize.Width + (num2 - s.kdbhzhimin) / (s.kdbhzhimax - s.kdbhzhimin) * (s.kdbhxiangsumax - s.kdbhxiangsumin), s.Size.Height);
                Matrix matrix = new();
                matrix.RotateAt(s.RotateAngel, s.RotateAtPoint);
                Point defaultLocaion = s.DefaultLocaion;
                PointF[] array = new PointF[1] { defaultLocaion };
                matrix.Invert();
                matrix.TransformPoints(array);
                ref PointF reference = ref array[0];
                reference = new PointF(Convert.ToSingle(array[0].X + (s.DefaultSize.Width - s.Size.Width)), Convert.ToSingle(array[0].Y + (s.DefaultSize.Height - s.Size.Height)));
                matrix.Invert();
                matrix.TransformPoints(array);
                s.Location = array[0];
            }
        }
    }

    public void GDBH(CShape s)
    {
        if (s.gdbhzhimax - s.gdbhzhimin != 0f && s.gdbhxiangsumax - s.gdbhxiangsumin != 0f)
        {
            float num = 0f;
            try
            {
                num = Convert.ToSingle(Eval(s.gdbhbianliang));
            }
            catch
            {
            }
            float num2 = num;
            if (num2 > s.gdbhzhimax)
            {
                num2 = s.gdbhzhimax;
            }
            if (num2 < s.gdbhzhimin)
            {
                num2 = s.gdbhzhimin;
            }
            if (s.gdbhcankao == 1)
            {
                s.Size = new SizeF(s.Size.Width, s.DefaultSize.Height + s.gdbhxiangsumin + (num2 - s.gdbhzhimin) / (s.gdbhzhimax - s.gdbhzhimin) * (s.gdbhxiangsumax - s.gdbhxiangsumin));
            }
            if (s.gdbhcankao == 2)
            {
                s.Size = new SizeF(s.Size.Width, s.DefaultSize.Height + s.gdbhxiangsumin + (num2 - s.gdbhzhimin) / (s.gdbhzhimax - s.gdbhzhimin) * (s.gdbhxiangsumax - s.gdbhxiangsumin));
                Matrix matrix = new();
                matrix.RotateAt(s.RotateAngel, s.RotateAtPoint);
                Point defaultLocaion = s.DefaultLocaion;
                PointF[] array = new PointF[1] { defaultLocaion };
                matrix.Invert();
                matrix.TransformPoints(array);
                ref PointF reference = ref array[0];
                reference = new PointF(Convert.ToSingle(array[0].X + (s.DefaultSize.Width - s.Size.Width)), Convert.ToSingle(array[0].Y + (s.DefaultSize.Height - s.Size.Height)));
                matrix.Invert();
                matrix.TransformPoints(array);
                s.Location = array[0];
            }
        }
    }

    public void BXYS(CShape s)
    {
        if (!s.bxysbh)
        {
            return;
        }
        float num = 0f;
        try
        {
            num = Convert.ToSingle(Eval(s.bxysbhbianliang));
        }
        catch
        {
        }
        for (int i = 0; i < s.bxysbhmin.Length; i++)
        {
            if (num > s.bxysbhmin[i] && num <= s.bxysbhmax[i])
            {
                _ = s.bxysbhys[i];
                if (s.bxysbhys[i] != s.PenColor)
                {
                    s.PenColor = s.bxysbhys[i];
                }
                else if (s.bxysbhss[i] && s.PenColor == s.bxysbhys[i])
                {
                    s.PenColor = Color.Transparent;
                }
            }
        }
    }

    public void TCS1(CShape s)
    {
        try
        {
            float num = Convert.ToSingle(Eval(s.tcs1ysbhbianliang));
            bool flag = false;
            for (int i = 0; i < s.tcs1ysbhmin.Length; i++)
            {
                if (num > s.tcs1ysbhmin[i] && num <= s.tcs1ysbhmax[i])
                {
                    flag = true;
                }
            }
            for (int j = 0; j < s.tcs1ysbhmin.Length; j++)
            {
                if (num > s.tcs1ysbhmin[j] && num <= s.tcs1ysbhmax[j])
                {
                    _ = s.tcs1ysbhys[j];
                    if (s.tcs1ysbhys[j] != s.Color1)
                    {
                        s.Color1 = s.tcs1ysbhys[j];
                        s.PenColorA = 255;
                    }
                    else if (s.tcs1ysbhss[j] && s.Color1 == s.tcs1ysbhys[j])
                    {
                        s.Color1 = Color.Transparent;
                        s.PenColorA = 0;
                    }
                }
                else if (!flag)
                {
                    s.Color1 = s.fillcolor1;
                    s.PenColorA = 255;
                }
            }
        }
        catch
        {
        }
    }

    public void TCS2(CShape s)
    {
        try
        {
            float num = Convert.ToSingle(Eval(s.tcs2ysbhbianliang));
            bool flag = false;
            for (int i = 0; i < s.tcs1ysbhmin.Length; i++)
            {
                if (num > s.tcs1ysbhmin[i] && num <= s.tcs1ysbhmax[i])
                {
                    flag = true;
                }
            }
            for (int j = 0; j < s.tcs2ysbhmin.Length; j++)
            {
                if (num > s.tcs2ysbhmin[j] && num <= s.tcs2ysbhmax[j])
                {
                    _ = s.tcs2ysbhys[j];
                    if (s.tcs2ysbhys[j] != s.Color2)
                    {
                        s.Color2 = s.tcs2ysbhys[j];
                        s.PenColorA = 255;
                    }
                    else if (s.tcs2ysbhss[j] && s.Color2 == s.tcs2ysbhys[j])
                    {
                        s.PenColorA = 0;
                        s.Color2 = Color.Transparent;
                    }
                }
                else if (!flag)
                {
                    s.Color1 = s.fillcolor1;
                    s.PenColorA = 255;
                }
            }
        }
        catch
        {
        }
    }

    public double MySin(int time, int cycle, double max, double min, double delay)
    {
        if (!Utils.CheckZero(cycle))
            return 0.0;

        return Math.Sin(Math.PI * 2.0 / (double)cycle * (((double)time - delay) % (double)cycle)) * (max - min) / 2.0 + (max + min) / 2.0;
    }

    public double Increase(int time, int cycle, double max, double min, double delay)
    {
        if (!Utils.CheckZero(cycle))
            return 0.0;

        double num = (max - min) / (double)cycle;
        return min + num * (((double)time - delay) % (double)cycle);
    }

    public double Degress(int time, int cycle, double max, double min, double delay)
    {
        if (!Utils.CheckZero(cycle))
            return 0.0;

        double num = (float)(min - max) / (float)cycle;
        return max + num * (((double)time - delay) % (double)cycle);
    }

    public double Triangle(int time, int cycle, double max, double min, double delay)
    {
        if (!Utils.CheckZero(cycle))
            return 0.0;

        double num = (float)(max - min) / (float)(cycle / 2);
        if (((double)time - delay) % (double)cycle <= (double)(cycle / 2))
        {
            return min + num * (((double)time - delay) % (double)cycle);
        }
        if (((double)time - delay) % (double)cycle > (double)(cycle / 2))
        {
            return max - num * (((double)time - delay) % (double)cycle - (double)(cycle / 2));
        }
        return 0.0;
    }

    public double Random(double max, double min)
    {
        Random random = new(Guid.NewGuid().GetHashCode());
        return random.NextDouble() * (max - min) + min;
    }

    private void DataWorkThread()
    {
        //while (canwork)
        //{
        //    _event.WaitOne();
        //    lock (ids)
        //    {
        //        for (int i = MaxID + 1; i < Varlist.Length; i++)
        //        {
        //            if (Varlist[i] == null)
        //            {
        //                Varlist[i] = 0;
        //            }
        //        }
        //        if (ids.Count != 0)
        //        {
        //            int num = int.MaxValue;
        //            for (int j = 0; j < ids.Count; j++)
        //            {
        //                if (ids[j].Id > MaxID)
        //                {
        //                    num = j;
        //                    break;
        //                }
        //            }
        //            if (num == int.MaxValue)
        //            {
        //                int id = ids[0].Id;
        //                int id2 = ids[ids.Count - 1].Id;
        //                client.getvalues(id, id2);
        //            }
        //            else
        //            {
        //                client.getvalues(ids[0].Id, ids[num - 1].Id);
        //            }
        //        }
        //    }
        //    _event.Reset();
        //}
    }

    public void close()
    {
        try
        {
            if (dead)
            {
                return;
            }
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Visible)
                {
                    SetPageVisible(control.Name, Visible: false);
                }
            }
            try
            {
                m_ScriptEngine.ExecuteStatement(dhp._cxdzgbLogic);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Line:" + m_ScriptEngine.Error.Line + " Columu:" + m_ScriptEngine.Error.Column);
            }
            canwork = false;
            if (datathread.IsAlive)
            {
                _event.Set();
            }
            timer.Stop();
            comtimer.Stop();
            systimer.Stop();
            //client.Stop();
        }
        catch (Exception ex2)
        {
            MessageBox.Show(ex2.Source + "\n" + ex2.Message + "\n" + ex2.StackTrace);
        }
        Application.RemoveMessageFilter(this);
    }

    public void ShutDown()
    {
        Process.GetCurrentProcess().Close();
    }

    public void BeginInit()
    {
        try
        {
            AfterLoad();
        }
        catch (Exception ex)
        {
            dead = true;
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
            isauthority = false;
        }
        if (dead)
        {
            Application.RemoveMessageFilter(this);
            Process.GetCurrentProcess().CloseMainWindow();
        }
        Refresh();
    }

    public void seturl(string u)
    {
        url = u;
        absoluteUrl = u.Substring(0, u.LastIndexOf('/') + 1);
    }

    public int GetPageX(string PageName)
    {
        try
        {
            int result = 0;
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Name == PageName)
                {
                    result = control.Location.X;
                }
            }
            return result;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int GetPageY(string PageName)
    {
        try
        {
            int result = 0;
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Name == PageName)
                {
                    result = control.Location.Y;
                }
            }
            return result;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int GetPageWidth(string PageName)
    {
        try
        {
            int result = 0;
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Name == PageName)
                {
                    result = control.Size.Width;
                }
            }
            return result;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int GetPageHeight(string PageName)
    {
        try
        {
            int result = 0;
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Name == PageName)
                {
                    result = control.Size.Height;
                }
            }
            return result;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public bool PageSizeLocation(string PageName, float PageZoomX, float PageZoomY, int PageX, int PageY)
    {
        try
        {
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                if (control.Name == PageName && SD.ContainsKey(control.Name))
                {
                    DataFile dataFile = SD[control.Name];
                    if (!PageNotResize.Contains(PageName))
                    {
                        PageNotResize.Add(PageName);
                    }
                    Convert.ToInt32(zoomX * (float)PageX);
                    Convert.ToInt32(zoomY * (float)PageY);
                    if (dic_zoomx.ContainsKey(PageName))
                    {
                        dic_zoomx.Remove(PageName);
                        dic_zoomx.Add(PageName, PageZoomX);
                    }
                    else
                    {
                        dic_zoomx.Add(PageName, PageZoomX);
                    }
                    if (dic_zoomy.ContainsKey(PageName))
                    {
                        dic_zoomy.Remove(PageName);
                        dic_zoomy.Add(PageName, PageZoomY);
                    }
                    else
                    {
                        dic_zoomy.Add(PageName, PageZoomY);
                    }
                    SD[control.Name].locationf.X = PageX;
                    SD[control.Name].locationf.Y = PageY;
                    control.Location = new Point(Convert.ToInt32(SD[control.Name].locationf.X * zoomX), Convert.ToInt32(SD[control.Name].locationf.Y * zoomY));
                    control.Size = new Size(Convert.ToInt32((float)dataFile.PageOldSize.Width * PageZoomX * zoomX), Convert.ToInt32((float)dataFile.PageOldSize.Height * PageZoomY * zoomY));
                }
            }
            if (PageZoomX == 0f)
            {
                return false;
            }
            DataFile[] array = dfs.ToArray();
            foreach (DataFile dataFile2 in array)
            {
                if (!(dataFile2.name == PageName))
                {
                    continue;
                }
                for (int j = 0; j < dataFile2.ListAllShowCShape.Count; j++)
                {
                    if (dataFile2.ListAllShowCShape[j] is CControl)
                    {
                        try
                        {
                            ((CControl)dataFile2.ListAllShowCShape[j]).Width = dataFile2.ListAllShowCShape[j].DefaultSize.Width * PageZoomX * zoomX;
                            ((CControl)dataFile2.ListAllShowCShape[j]).Height = dataFile2.ListAllShowCShape[j].DefaultSize.Height * PageZoomY * zoomY;
                            ((CControl)dataFile2.ListAllShowCShape[j]).X = Convert.ToInt32((float)dataFile2.ListAllShowCShape[j].DefaultLocaion.X * PageZoomX * zoomX);
                            ((CControl)dataFile2.ListAllShowCShape[j]).Y = Convert.ToInt32((float)dataFile2.ListAllShowCShape[j].DefaultLocaion.Y * PageZoomY * zoomY);
                            float num = ((CControl)dataFile2.ListAllShowCShape[j]).DefaultFontSize * ((PageZoomX * zoomX < PageZoomY * zoomY) ? (PageZoomX * zoomX) : (PageZoomY * zoomY));
                            num = ((num < 8.25f) ? 8.25f : num);
                            ((CControl)dataFile2.ListAllShowCShape[j])._c.Font = new Font(((CControl)dataFile2.ListAllShowCShape[j])._c.Font.Name, num);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            foreach (object value2 in SO.Values)
            {
                Control control2 = (Control)value2;
                if (control2.Name == PageName)
                {
                    control2.Invalidate();
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public MainControl()
    {
        Application.AddMessageFilter(this);
        Application.CurrentCulture = new CultureInfo("");
        Thread.CurrentThread.CurrentCulture = new CultureInfo("");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        Application.EnableVisualStyles();
        InitializeComponent();
        Control.CheckForIllegalCrossThreadCalls = false;
    }

    private void MainControl_Load(object sender, EventArgs e)
    {
        if (!alreadyfirstEnterFocus)
        {
            IntPtr hwnd = UnsafeNativeMethods.GetParent(new HandleRef(this, base.Handle));
            UnsafeNativeMethods.PostMessage(hwnd, 513, IntPtr.Zero, IntPtr.Zero);
            UnsafeNativeMethods.PostMessage(hwnd, 514, IntPtr.Zero, IntPtr.Zero);
            alreadyfirstEnterFocus = true;
        }
    }

    private void MainControl_Resize(object sender, EventArgs e)
    {
        if (inresize)
        {
            return;
        }
        inresize = true;
        if (base.Size.Width == 0 || base.Size.Height == 0)
        {
            m_NotVisibleChange = true;
            inresize = false;
            return;
        }
        DataFile[] array = dfs.ToArray();
        foreach (DataFile dataFile in array)
        {
            if (!dataFile.visable)
            {
                if (!pageNeedResize.Contains(dataFile.name))
                {
                    pageNeedResize.Add(dataFile.name);
                }
                continue;
            }
            if (pageNeedResize.Contains(dataFile.name))
            {
                pageNeedResize.Remove(dataFile.name);
            }
            if (PageNotResize.Contains(dataFile.name))
            {
                float num = dic_zoomx[dataFile.name];
                float num2 = dic_zoomy[dataFile.name];
                for (int j = 0; j < dataFile.ListAllShowCShape.Count; j++)
                {
                    if (dataFile.ListAllShowCShape[j] is CControl control)
                    {
                        try
                        {
                            control.Width = dataFile.ListAllShowCShape[j].DefaultSize.Width * zoomX * num;
                            control.Height = dataFile.ListAllShowCShape[j].DefaultSize.Height * zoomY * num2;
                            control.X = Convert.ToInt32((float)dataFile.ListAllShowCShape[j].DefaultLocaion.X * zoomX * num);
                            control.Y = Convert.ToInt32((float)dataFile.ListAllShowCShape[j].DefaultLocaion.Y * zoomY * num2);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                continue;
            }
            for (int k = 0; k < dataFile.ListAllShowCShape.Count; k++)
            {
                if (dataFile.ListAllShowCShape[k] is CControl)
                {
                    try
                    {
                        ((CControl)dataFile.ListAllShowCShape[k]).Width = dataFile.ListAllShowCShape[k].DefaultSize.Width * zoomX;
                        ((CControl)dataFile.ListAllShowCShape[k]).Height = dataFile.ListAllShowCShape[k].DefaultSize.Height * zoomY;
                        ((CControl)dataFile.ListAllShowCShape[k]).X = Convert.ToInt32((float)dataFile.ListAllShowCShape[k].DefaultLocaion.X * zoomX);
                        ((CControl)dataFile.ListAllShowCShape[k]).Y = Convert.ToInt32((float)dataFile.ListAllShowCShape[k].DefaultLocaion.Y * zoomY);
                        float num3 = ((CControl)dataFile.ListAllShowCShape[k]).DefaultFontSize * ((zoomX < zoomY) ? zoomX : zoomY);
                        num3 = ((num3 < 8.25f) ? 8.25f : num3);
                        ((CControl)dataFile.ListAllShowCShape[k])._c.Font = new Font(((CControl)dataFile.ListAllShowCShape[k])._c.Font.Name, num3);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        foreach (object value in SO.Values)
        {
            UserControl userControl = (UserControl)value;
            if (!SD.ContainsKey(userControl.Name))
            {
                continue;
            }
            if (!userControl.Visible)
            {
                if (!pageNeedResize.Contains(userControl.Name))
                {
                    pageNeedResize.Add(userControl.Name);
                }
                continue;
            }
            if (pageNeedResize.Contains(userControl.Name))
            {
                pageNeedResize.Remove(userControl.Name);
            }
            if (PageNotResize.Contains(userControl.Name))
            {
                float num4 = dic_zoomx[userControl.Name];
                float num5 = dic_zoomy[userControl.Name];
                userControl.Location = new Point(Convert.ToInt32(SD[userControl.Name].locationf.X * zoomX), Convert.ToInt32(SD[userControl.Name].locationf.Y * zoomY));
                userControl.Size = new Size(Convert.ToInt32(SD[userControl.Name].sizef.Width * zoomX * num4), Convert.ToInt32(SD[userControl.Name].sizef.Height * zoomY * num5));
            }
            else
            {
                userControl.Location = new Point(Convert.ToInt32(SD[userControl.Name].locationf.X * zoomX), Convert.ToInt32(SD[userControl.Name].locationf.Y * zoomY));
                userControl.Size = new Size(Convert.ToInt32(SD[userControl.Name].sizef.Width * zoomX), Convert.ToInt32(SD[userControl.Name].sizef.Height * zoomY));
            }
            userControl.Invalidate();
        }
        inresize = false;
    }

    private void MainControl_Paint(object sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        if (PageNotResize.Contains(((Control)sender).Name))
        {
            float num = dic_zoomx[((Control)sender).Name];
            float num2 = dic_zoomy[((Control)sender).Name];
            graphics.ScaleTransform(num * zoomX, num2 * zoomY);
        }
        else
        {
            graphics.ScaleTransform(zoomX, zoomY);
        }
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        foreach (CShape item in SD[((Control)sender).Name].ListAllShowCShape)
        {
            try
            {
                item.DrawMe(graphics, item.WillDrawRectLine);
            }
            catch (Exception)
            {
            }
        }
        graphics.ResetTransform();
        if (SD[((Control)sender).Name].IsWindow)
        {
            LinearGradientBrush brush = new(new Rectangle(0, 0, ((Control)sender).Width - 1, 22), SystemColors.GradientActiveCaption, SystemColors.ActiveCaption, 0f);
            graphics.FillRectangle(brush, new Rectangle(0, 0, ((Control)sender).Width - 1, 22));
            ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, ((Control)sender).Width - 1, 22), Border3DStyle.Bump);
            ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, ((Control)sender).Width - 1, ((Control)sender).Height - 1), Border3DStyle.Bump);
            graphics.DrawString(SD[((Control)sender).Name].pageName, SystemFonts.DefaultFont, Brushes.DarkGray, new PointF(3f, 3f));
            graphics.DrawString(SD[((Control)sender).Name].pageName, SystemFonts.DefaultFont, Brushes.Black, new PointF(4f, 4f));
            ControlPaint.DrawCaptionButton(graphics, new Rectangle(((Control)sender).Width - 23, 5, 12, 12), CaptionButton.Close, ButtonState.Normal);
        }
    }

    private void MainControl_Scroll(object sender, ScrollEventArgs e)
    {
        if (!((UserControl)sender).AutoScroll)
        {
            return;
        }
        foreach (object value in SO.Values)
        {
            UserControl userControl = (UserControl)value;
            if (userControl.Name == ((UserControl)sender).Name)
            {
                userControl.Invalidate();
            }
        }
    }

    private string UserControl1_GetVarTableEvent(string controlname)
    {
        if (varForm == null)
        {
            varForm = new VarTable(dhp, xmldoc, controlname);
        }
        if (varForm.VarTableType != controlname)
        {
            varForm.VarTableType = controlname;
        }
        varForm.ShowDialog();
        if (varForm.DialogResult == DialogResult.OK)
        {
            switch (controlname)
            {
                case "history":
                case "realtime":
                    {
                        string[] array2 = varForm.value.Split('|');
                        string[] array3 = varForm.tagvalue.Split('|');
                        string text2 = "";
                        for (int j = 0; j < array2.Length; j++)
                        {
                            string text3 = text2;
                            text2 = text3 + "|" + array2[j] + "|" + array3[j];
                        }
                        return text2.Substring(1);
                    }
                case "para":
                    {
                        string[] array = varForm.value.Split('|');
                        string text = "";
                        for (int i = 0; i < array.Length; i++)
                        {
                            text = text + "|" + array[i];
                        }
                        return text.Substring(1);
                    }
                default:
                    return varForm.value;
            }
        }
        if (controlname == "history" || controlname == "realtime")
        {
            return "|";
        }
        return "";
    }

    private void UserControl1_SetValueEvent(string name, object value)
    {
        SetValue(name, value);
    }

    private object UserControl1_GetValueEvent(string name)
    {
        return GetValue(name);
    }

    private void timer2Delay_Tick(object sender, EventArgs e)
    {
        if (nDelayTick <= 0)
        {
            timer2Delay.Stop();
            init.Close();
            try
            {
                m_ScriptEngine.ExecuteStatement(dhp._cxdzqdLogic);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Line:" + m_ScriptEngine.Error.Line + " Columu: " + m_ScriptEngine.Error.Column);
            }
            foreach (string startVisiblePage in dhp.startVisiblePages)
            {
                SetPageVisible(startVisiblePage, Visible: true);
            }
            if (this.InitOK != null)
            {
                this.InitOK(null, null);
            }
            bOnInit = false;
            bInitOK = true;
        }
        else
        {
            init.Say("数据将会加载" + nDelayTick + "s.请耐心等待！");
            init.Refresh();
            nDelayTick--;
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        try
        {
            DataFile[] array = dfs.ToArray();
            foreach (DataFile dataFile in array)
            {
                if (!((Control)SO[dataFile.name]).Visible)
                {
                    continue;
                }
                foreach (CShape item in dataFile.ListAllShowCShape)
                {
                    if (item.txyc)
                    {
                        YC(item);
                    }
                    if (item.kdbh)
                    {
                        KDBH(item);
                    }
                    if (item.gdbh)
                    {
                        GDBH(item);
                    }
                    if (item.spbfb)
                    {
                        SPBFB(item);
                    }
                    if (item.czbfb)
                    {
                        CZBFB(item);
                    }
                    if (item.mbxz)
                    {
                        CIR(item);
                    }
                    if (item.spyd)
                    {
                        SPYD(item);
                    }
                    if (item.czyd)
                    {
                        CZYD(item);
                    }
                    if (item.zfcsc)
                    {
                        ZFXS(item);
                    }
                    if (item.bxysbh)
                    {
                        BXYS(item);
                    }
                    if (item.tcs1ysbh)
                    {
                        TCS1(item);
                    }
                    if (item.tcs2ysbh)
                    {
                        TCS2(item);
                    }
                    if (item.ao)
                    {
                        MNSC(item);
                    }
                    if (item.doo)
                    {
                        KGSC(item);
                    }
                    if (item is CPixieControl control)
                    {
                        control.RefreshControl();
                    }
                    else if (item is CRectangle && ((CRectangle)item).ld)
                    {
                        LD(item);
                    }
                    if (item.NeedRefresh)
                    {
                        needdraw = true;
                        item.NeedRefresh = false;
                    }
                }
                if (needdraw)
                {
                    ((Control)SO[dataFile.name]).Invalidate();
                    needdraw = false;
                }
            }
        }
        catch (Exception ex)
        {
            timer.Enabled = false;
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
        }
    }

    private void comtimer_Tick(object sender, EventArgs e)
    {
        lock (ids)
        {
            ids.Clear();
            foreach (VarTableItem value in DicIO.Values)
            {
                if (value.Isalive)
                {
                    ids.Add(value);
                    if (Varlist[value.Id] != null)
                    {
                        value.Isalive = false;
                    }
                }
            }
        }
        _event.Set();
    }

    private void systimer_Tick(object sender, EventArgs e)
    {
        try
        {
            m_ScriptEngine.ExecuteStatement(dhp._cxdzyxLogic);
        }
        catch (Exception ex)
        {
            systimer.Enabled = false;
            MessageBox.Show(ex.Message + " Line:" + m_ScriptEngine.Error.Line + " Columu:" + m_ScriptEngine.Error.Column);
        }
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        sender.GetType().GetMethod("GetFocus")?.Invoke(sender, new object[0]);
        if (SD[((Control)sender).Name].IsWindow && e.Location.Y < 25)
        {
            Point controlMoveTempPoint = ((Control)sender).PointToScreen(e.Location);
            ControlMoveTempPoint = controlMoveTempPoint;
            ControlMoveTempMouseDown = true;
            return;
        }

        int num;
        int num2;
        if (PageNotResize.Contains(((Control)sender).Name))
        {
            float num3 = dic_zoomx[((Control)sender).Name];
            float num4 = dic_zoomy[((Control)sender).Name];
            num = (int)((float)e.X / (zoomX * num3));
            num2 = (int)((float)e.Y / (zoomY * num4));
        }
        else
        {
            num = (int)((float)e.X / zoomX);
            num2 = (int)((float)e.Y / zoomY);
        }
        Mouse = new MouseEventArgs(e.Button, e.Clicks, num, num2, e.Delta);
        Mousedownx = num;
        Mousedowny = num2;
        //object[] array = new object[2] { sender, e };
        Focus = null;
        List<CShape> listAllShowCShape = SD[((Control)sender).Name].ListAllShowCShape;
        for (int num5 = listAllShowCShape.Count - 1; num5 >= 0; num5--)
        {
            CShape cShape = listAllShowCShape[num5];
            if (cShape.Visible && cShape.MouseOnMe(new Point(num, num2)))
            {
                if (cShape is CPixieControl)
                {
                    MouseEventArgs e2 = new(e.Button, e.Clicks, Convert.ToInt32(Mousedownx), Convert.ToInt32(Mousedowny), e.Delta);
                    ((CPixieControl)cShape).OnMouseDown(sender, e2);
                }
                if (e.Button == MouseButtons.Left && (cShape.sbsj || cShape.ai || cShape.di || cShape.zfcsr || cShape.ymqh || cShape.sptz || cShape.cztz || cShape.newtable || cShape.dbselect || cShape.dbinsert || cShape.dbupdate || cShape.dbdelete || cShape.dbmultoperation))
                {
                    lastMouseDownShapeList.Add(cShape);
                    Focus = cShape;
                    if (cShape.sbsj)
                    {
                        lastMouseDownShapeList.Add(cShape);
                        try
                        {
                            ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape._sbsjlcLogic);
                            if (cShape.Logic[1] != null && cShape.Logic[1].Trim() != string.Empty)
                            {
                                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape.Logic[1]);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu: " + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                        }
                    }
                    AIinput(cShape);
                    DIinput(cShape);
                    STRinput(cShape);
                    if (cShape != null && cShape is not CControl)
                    {
                        CallDBAnimation(cShape);
                    }
                    YMQH(cShape);
                    break;
                }
                if (e.Button == MouseButtons.Right && cShape.sbsj)
                {
                    lastMouseDownShapeList.Add(cShape);
                    try
                    {
                        ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape._sbsjrcLogic);
                        if (cShape.Logic[3] != null && cShape.Logic[3].Trim() != string.Empty)
                        {
                            ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape.Logic[3]);
                        }
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show(ex2.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu: " + ((ScriptEngine)((Control)sender).Tag).Error.Line);
                    }
                    break;
                }
            }
        }
        ((Control)sender).Invalidate();
    }

    private void OnMouseDoubleClick(object sender, MouseEventArgs e)
    {
        Mouse = e;
        int num;
        int num2;
        if (PageNotResize.Contains(((Control)sender).Name))
        {
            float num3 = dic_zoomx[((Control)sender).Name];
            float num4 = dic_zoomy[((Control)sender).Name];
            num = (int)((float)e.X / (zoomX * num3));
            num2 = (int)((float)e.Y / (zoomY * num4));
        }
        else
        {
            num = (int)((float)e.X / zoomX);
            num2 = (int)((float)e.Y / zoomY);
        }
        List<CShape> listAllShowCShape = SD[((Control)sender).Name].ListAllShowCShape;
        foreach (CShape item in listAllShowCShape)
        {
            if (!item.sbsj || !item.MouseOnMe(new Point(num, num2)))
            {
                continue;
            }
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item._sbsjldcLogic);
                    if (item.Logic[2] != null && item.Logic[2].Trim() != string.Empty)
                    {
                        ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item.Logic[2]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                }
            }
            if (e.Button != MouseButtons.Right)
            {
                continue;
            }
            try
            {
                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item._sbsjrdcLogic);
                if (item.Logic[4] != null && item.Logic[4].Trim() != string.Empty)
                {
                    ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item.Logic[4]);
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
            }
        }
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        int num;
        int num2;
        if (PageNotResize.Contains(((Control)sender).Name))
        {
            float num3 = dic_zoomx[((Control)sender).Name];
            float num4 = dic_zoomy[((Control)sender).Name];
            num = (int)((float)e.X / (zoomX * num3));
            num2 = (int)((float)e.Y / (zoomY * num4));
        }
        else
        {
            num = (int)((float)e.X / zoomX);
            num2 = (int)((float)e.Y / zoomY);
        }
        Mouse = new MouseEventArgs(e.Button, e.Clicks, num, num2, e.Delta);
        if (SD[((Control)sender).Name].IsWindow)
        {
            if (e.X > ((Control)sender).Width - 23 && e.X <= ((Control)sender).Width - 11 && e.Y > 5 && e.Y < 17)
            {
                SetPageVisible(((Control)sender).Name, Visible: false);
            }
            ControlMoveTempMouseDown = false;
        }
        List<CShape> listAllShowCShape = SD[((Control)sender).Name].ListAllShowCShape;
        foreach (CShape item in listAllShowCShape)
        {
            if (!item.Visible)
            {
                continue;
            }
            if (item is CPixieControl control)
            {
                MouseEventArgs e2 = new(e.Button, e.Clicks, Convert.ToInt32((int)((float)e.X / zoomX)), Convert.ToInt32((int)((float)e.X / zoomY)), e.Delta);
                control.OnMouseUp(sender, e2);
            }
            if (item == Moving)
            {
                SPTZmouseup(item);
                SZTZmouseup(item);
            }
            if (!item.MouseOnMe(new Point(num, num2)))
            {
                continue;
            }
            if (item.sbsj)
            {
                lastMouseDownShapeList.Clear();
                if (e.Button == MouseButtons.Left)
                {
                    try
                    {
                        if (item.Logic[6] != null && item.Logic[6].Trim() != string.Empty)
                        {
                            ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item.Logic[6]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                    }
                }
                if (e.Button != MouseButtons.Right)
                {
                    break;
                }
                try
                {
                    if (item.Logic[7] != null && item.Logic[7].Trim() != string.Empty)
                    {
                        ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(item.Logic[7]);
                    }
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                }
                break;
            }
            try
            {
                item.FireClick();
            }
            catch
            {
            }
        }
        Moving = null;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (SD[((Control)sender).Name].IsWindow && ControlMoveTempMouseDown)
        {
            Point controlMoveTempPoint = ((Control)sender).PointToScreen(e.Location);
            ((Control)sender).Location = new Point(((Control)sender).Location.X + controlMoveTempPoint.X - ControlMoveTempPoint.X, ((Control)sender).Location.Y + controlMoveTempPoint.Y - ControlMoveTempPoint.Y);
            ControlMoveTempPoint = controlMoveTempPoint;
            ((Control)sender).Invalidate();
            return;
        }

        int num;
        int num2;
        if (PageNotResize.Contains(((Control)sender).Name))
        {
            float num3 = dic_zoomx[((Control)sender).Name];
            float num4 = dic_zoomy[((Control)sender).Name];
            num = (int)((float)e.X / (zoomX * num3));
            num2 = (int)((float)e.Y / (zoomY * num4));
        }
        else
        {
            num = (int)((float)e.X / zoomX);
            num2 = (int)((float)e.Y / zoomY);
        }
        Mouse = new MouseEventArgs(e.Button, e.Clicks, num, num2, e.Delta);
        List<CShape> listAllShowCShape = SD[((Control)sender).Name].ListAllShowCShape;
        for (int num5 = listAllShowCShape.Count - 1; num5 >= 0; num5--)
        {
            CShape cShape = listAllShowCShape[num5];
            if (cShape.Visible)
            {
                if (cShape is CPixieControl control)
                {
                    MouseEventArgs e2 = new(e.Button, e.Clicks, Convert.ToInt32((int)((float)e.X / zoomX)), Convert.ToInt32((int)((float)e.Y / zoomY)), e.Delta);
                    control.OnMouseMove(sender, e2);
                }
                if (e.Button == MouseButtons.Left && cShape == Focus && (cShape.sptz || cShape.cztz))
                {
                    SPTZmousemove(cShape, cShape.DefaultLocaion.X, num, ref Mousedownx);
                    SZTZmousemove(cShape, cShape.DefaultLocaion.Y, num2, ref Mousedowny);
                    Moving = cShape;
                    ((Control)sender).Invalidate();
                }
                if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && (cShape.ai || cShape.di || cShape.zfcsr || cShape.ymqh || cShape.sptz || cShape.cztz || cShape.sbsj || cShape.dbselect || cShape.dbinsert || cShape.dbupdate || cShape.dbdelete || cShape.dbmultoperation))
                {
                    if (cShape.MouseOnMe(new Point(num, num2)))
                    {
                        bool willDrawRectLine = cShape.WillDrawRectLine;
                        cShape.WillDrawRectLine = true;
                        for (int num6 = num5 - 1; num6 >= 0; num6--)
                        {
                            listAllShowCShape[num6].WillDrawRectLine = false;
                        }
                        if (willDrawRectLine != cShape.WillDrawRectLine)
                        {
                            ((Control)sender).Invalidate();
                        }
                        break;
                    }
                    if (cShape.WillDrawRectLine)
                    {
                        cShape.WillDrawRectLine = false;
                        ((Control)sender).Invalidate();
                    }
                }
            }
        }
        for (int num7 = listAllShowCShape.Count - 1; num7 >= 0; num7--)
        {
            CShape cShape2 = listAllShowCShape[num7];
            if (cShape2.sbsj && cShape2.sbsjWhenOutThenDo)
            {
                if (cShape2.MouseOnMe(new Point(num, num2)) && !lastMouseDownShapeList.Contains(cShape2))
                {
                    lastMouseDownShapeList.Add(cShape2);
                    if (e.Button == MouseButtons.Left)
                    {
                        Focus = cShape2;
                        try
                        {
                            ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2._sbsjlcLogic);
                            if (cShape2.Logic[1] != null && cShape2.Logic[1].Trim() != string.Empty)
                            {
                                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2.Logic[1]);
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu: " + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                            break;
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        try
                        {
                            ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2._sbsjrcLogic);
                            if (cShape2.Logic[3] != null && cShape2.Logic[3].Trim() != string.Empty)
                            {
                                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2.Logic[3]);
                            }
                            break;
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu: " + ((ScriptEngine)((Control)sender).Tag).Error.Line);
                            break;
                        }
                    }
                }
                else if (!cShape2.MouseOnMe(new Point(num, num2)) && lastMouseDownShapeList.Contains(cShape2))
                {
                    lastMouseDownShapeList.Remove(cShape2);
                    if (e.Button == MouseButtons.Left)
                    {
                        try
                        {
                            if (cShape2.Logic[6] != null && cShape2.Logic[6].Trim() != string.Empty)
                            {
                                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2.Logic[6]);
                            }
                        }
                        catch (Exception ex3)
                        {
                            MessageBox.Show(ex3.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        try
                        {
                            if (cShape2.Logic[7] != null && cShape2.Logic[7].Trim() != string.Empty)
                            {
                                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(cShape2.Logic[7]);
                            }
                        }
                        catch (Exception ex4)
                        {
                            MessageBox.Show(ex4.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
                        }
                    }
                }
            }
        }
    }

    private void OnVisibleChanged(object sender, EventArgs e)
    {
        if (!bInitOK && !bOnInit)
        {
            return;
        }
        if (m_NotVisibleChange)
        {
            m_NotVisibleChange = false;
            return;
        }
        try
        {
            if (((Control)sender).Visible && !SD.ContainsKey(((Control)sender).Name))
            {
                DelayLoadPage(((Control)sender).Name);
            }
            Type type = sender.GetType();
            object[] array = new object[2] { sender, e };
            DataFile dataFile = SD[((Control)sender).Name];
            if (((Control)sender).Visible && !dataFile.visable)
            {
                if (!m_VisibleChangeIn)
                {
                    m_VisibleChangeIn = true;
                    ResetScriptEngine((ScriptEngine)((Control)sender).Tag);
                    timer_Tick(null, null);
                    ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(dataFile._pagedzqdLogic);
                    m_VisibleChangeIn = false;
                }
                type.GetProperty("TimerEnable").SetValue(sender, true, null);
                dataFile.visable = true;
            }
            if (!((Control)sender).Visible && dataFile.visable)
            {
                type.GetProperty("TimerEnable").SetValue(sender, false, null);
                ((ScriptEngine)((Control)sender).Tag).ExecuteStatement(dataFile._pagedzgbLogic);
                dataFile.visable = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + " Line:" + ((ScriptEngine)((Control)sender).Tag).Error.Line + " Columu:" + ((ScriptEngine)((Control)sender).Tag).Error.Column);
        }
    }

    public float? GetSingleValue(string tag, double maxvalue, double minvalue, object oldvalue)
    {
        SingleForm singleForm = new(tag, maxvalue, minvalue);
        if (float.TryParse(oldvalue.ToString(), out var result))
        {
            singleForm.value = result;
        }
        singleForm.ShowDialog();
        return singleForm.value;
    }

    public bool? GetBoolValue(string tag1, string tag2, string tag3)
    {
        BoolForm boolForm = new(tag1, tag2, tag3);
        boolForm.ShowDialog();
        return boolForm.value;
    }

    public string GetStringValue(string tag, object oldvalue)
    {
        StringForm stringForm = new(tag)
        {
            value = oldvalue.ToString()
        };
        stringForm.ShowDialog();
        return stringForm.value;
    }

    public void Stoptimer()
    {
        timer.Enabled = false;
    }

    public void Starttimer()
    {
        timer.Enabled = true;
    }

    public void setvalue(int id, object value)
    {
        //client.setvalue(id, value);
    }

    private void ResetScriptEngine(ScriptEngine s)
    {
        s.Reset();
        s.AddObject("Globle", GlobleObj);
        s.AddObject("System", this);
        foreach (string key in SO.Keys)
        {
            s.AddObject(key, SO[key]);
        }
        s.ExecuteStatement(GlobalScriptBackUp);
    }

    public object Eval(string str)
    {
        var logicScript = Utils.GetLogicToScript(str);
        return m_ScriptEngine.Eval(logicScript, null);
    }

    public object GetValue(string str)
    {
        try
        {
            if (!DicIO.ContainsKey(str))
            {
                if (str.ToLower() == "[null]")
                    return null;

                if (str.ToLower() == "[true]")
                    return true;

                if (str.ToLower() == "[false]")
                    return false;

                if (str.StartsWith("[\"") && str.EndsWith("\"]"))
                    return str.Substring(2, str.Length - 4);

                if (str.StartsWith("[") && str.EndsWith("]") && float.TryParse(str.Substring(1, str.Length - 2), out float result))
                {
                    if (!str.Contains("."))
                    {
                        long num = Convert.ToInt64(result);
                        if (num <= 127 && num >= -128)
                        {
                            return Convert.ToSByte(num);
                        }
                        if (num <= 32767 && num >= -32768)
                        {
                            return Convert.ToInt16(num);
                        }
                        if (num <= int.MaxValue && num >= int.MinValue)
                        {
                            return Convert.ToInt32(num);
                        }
                        return num;
                    }
                    return result;
                }
                if (str.StartsWith("[TEMP"))
                {
                    if (tempValueDict.ContainsKey(str))
                        return tempValueDict[str];

                    return false;
                }
                if (str.Contains("."))
                {
                    string[] array = str.Substring(1, str.Length - 2).Split('.');
                    if (array.Length == 3)
                    {
                        object obj = SO[array[0]];
                        Type type = obj.GetType();
                        obj = type.GetProperty(array[1]).GetValue(obj, null);
                        type = obj.GetType();
                        return type.GetProperty(array[2]).GetValue(obj, null);
                    }
                    if (array.Length == 2)
                    {
                        object obj2 = SO[array[0]];
                        Type type2 = obj2.GetType();
                        return type2.GetProperty(array[1]).GetValue(obj2, null);
                    }
                    return null;
                }
                return null;
            }
            if (Varlist[DicIO[str].Id] == null)
            {
                DicIO[str].Isalive = true;
                return null;
            }
            if (DicIO[str].Id <= MaxID)
            {
                DicIO[str].Isalive = true;
                return Varlist[DicIO[str].Id];
            }
            if (DicIO[str].Emluator == "递增")
            {
                return Increase(Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMilliseconds - begintime.TimeOfDay.TotalMilliseconds), int.Parse(DicIO[str].Cycle), double.Parse(DicIO[str].Max), double.Parse(DicIO[str].Min), double.Parse(DicIO[str].Delay));
            }
            if (DicIO[str].Emluator == "递减")
            {
                return Degress(Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMilliseconds - begintime.TimeOfDay.TotalMilliseconds), int.Parse(DicIO[str].Cycle), double.Parse(DicIO[str].Max), double.Parse(DicIO[str].Min), double.Parse(DicIO[str].Delay));
            }
            if (DicIO[str].Emluator == "正弦")
            {
                return MySin(Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMilliseconds - begintime.TimeOfDay.TotalMilliseconds), int.Parse(DicIO[str].Cycle), double.Parse(DicIO[str].Max), double.Parse(DicIO[str].Min), double.Parse(DicIO[str].Delay));
            }
            if (DicIO[str].Emluator == "三角")
            {
                return Triangle(Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMilliseconds - begintime.TimeOfDay.TotalMilliseconds), int.Parse(DicIO[str].Cycle), double.Parse(DicIO[str].Max), double.Parse(DicIO[str].Min), double.Parse(DicIO[str].Delay));
            }
            if (DicIO[str].Emluator == "随机")
            {
                return Random(double.Parse(DicIO[str].Max), double.Parse(DicIO[str].Min));
            }
            return Varlist[DicIO[str].Id];
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static object ChangeType(object value, Type type)
    {
        if (value == null && type.IsGenericType)
        {
            return Activator.CreateInstance(type);
        }
        if (value == null)
        {
            return null;
        }
        if (type == value.GetType())
        {
            return value;
        }
        if (type.IsEnum)
        {
            if (value is string)
            {
                return Enum.Parse(type, value as string);
            }
            return Enum.ToObject(type, value);
        }
        if (!type.IsInterface && type.IsGenericType)
        {
            Type type2 = type.GetGenericArguments()[0];
            object obj = ChangeType(value, type2);
            return Activator.CreateInstance(type, obj);
        }
        if (value is string && type == typeof(Guid))
        {
            return new Guid(value as string);
        }
        if (value is not IConvertible)
        {
            return value;
        }
        return Convert.ChangeType(value, type);
    }

    public void SetValue(string str, object val)
    {
        try
        {
            var arrayVar = str.Split('$');
            if (2 == arrayVar.Length)
            {
                arrayVar[1] = "[" + arrayVar[1];
                var suffixVal = GetValue(arrayVar[1]);
                str = arrayVar[0] + suffixVal + "]";
            }

            if (!DicIO.ContainsKey(str))
            {
                if (str.StartsWith("[TEMP"))
                {
                    if (tempValueDict.ContainsKey(str))
                    {
                        tempValueDict[str] = val;
                    }
                    else
                    {
                        tempValueDict.Add(str, val);
                    }
                }
                else if (str.Contains("."))
                {
                    string[] array = str.Substring(1, str.Length - 2).Split('.');
                    object obj = SO[array[0]];
                    Type type = obj.GetType();
                    obj = type.GetProperty(array[1]).GetValue(obj, null);
                    type = obj.GetType();
                    PropertyInfo property = type.GetProperty(array[2]);
                    property.SetValue(obj, ChangeType(val, property.PropertyType), null);
                }
                return;
            }
            VarTableItem varTableItem = DicIO[str];
            switch (varTableItem.Type)
            {
                case 0:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToBoolean(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToBoolean(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToBoolean(Varlist[DicIO[str].Id]) != Convert.ToBoolean(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToBoolean(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 1:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToByte(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToByte(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToByte(Varlist[DicIO[str].Id]) != Convert.ToByte(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToByte(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 2:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToSByte(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToSByte(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToSByte(Varlist[DicIO[str].Id]) != Convert.ToSByte(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToSByte(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 3:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToInt16(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt16(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToInt16(Varlist[DicIO[str].Id]) != Convert.ToInt16(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt16(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 4:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToUInt16(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToUInt16(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToUInt16(Varlist[DicIO[str].Id]) != Convert.ToUInt16(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToUInt16(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 5:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToInt32(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToInt32(Varlist[DicIO[str].Id]) != Convert.ToInt32(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 6:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToUInt32(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToUInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToUInt32(Varlist[DicIO[str].Id]) != Convert.ToUInt32(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToUInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 7:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToSingle(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToSingle(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToSingle(Varlist[DicIO[str].Id]) != Convert.ToSingle(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToSingle(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 8:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToDouble(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToDouble(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToDouble(Varlist[DicIO[str].Id]) != Convert.ToDouble(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToDouble(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 9:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, val.ToString());
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = val.ToString();
                        DicIO[str].Invalidate = true;
                    }
                    else if (Varlist[DicIO[str].Id].ToString() != val.ToString())
                    {
                        Varlist[DicIO[str].Id] = val.ToString();
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 10:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToInt32(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToInt32(Varlist[DicIO[str].Id]) != Convert.ToInt32(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 11:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, Convert.ToInt32(val));
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else if (Convert.ToInt32(Varlist[DicIO[str].Id]) != Convert.ToInt32(val))
                    {
                        Varlist[DicIO[str].Id] = Convert.ToInt32(val);
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
                case 1024:
                    if (varTableItem.Id <= MaxID)
                    {
                        setvalue(varTableItem.Id, val);
                        GetValue(str);
                    }
                    else if (Varlist[DicIO[str].Id] == null)
                    {
                        Varlist[DicIO[str].Id] = val;
                        DicIO[str].Invalidate = true;
                    }
                    else if (Varlist[DicIO[str].Id] != val)
                    {
                        Varlist[DicIO[str].Id] = val;
                        DicIO[str].Invalidate = true;
                    }
                    else
                    {
                        DicIO[str].Invalidate = false;
                    }
                    break;
            }
        }
        catch
        {
        }
    }

    public void Execute(string statement, string name)
    {
        if (statement == null || statement == "" || statement.Trim() == "")
        {
            return;
        }
        try
        {
            if (name != null)
            {
                ((ScriptEngine)((Control)SO[name]).Tag).ExecuteStatement(statement);
                return;
            }
            m_ScriptEngine.ExecuteStatement(Utils.GetLogicToScript(statement));
        }
        catch (COMException ex)
        {
            throw new ErrorException(ex.Message, ((ScriptEngine)((Control)SO[name]).Tag).Error.Line, ((ScriptEngine)((Control)SO[name]).Tag).Error.Column);
        }
        catch (Exception ex2)
        {
            throw ex2;
        }
    }

    public void Execute2(CShape st, string statement, string name)
    {
        if (statement == null)
        {
            return;
        }
        try
        {
            ((ScriptEngine)((Control)SO[name]).Tag).ExecuteStatement(statement);
        }
        catch (COMException ex)
        {
            throw new ErrorException(ex.Message, ((ScriptEngine)((Control)SO[name]).Tag).Error.Line, ((ScriptEngine)((Control)SO[name]).Tag).Error.Column);
        }
        catch (Exception ex2)
        {
            throw ex2;
        }
    }

    public bool PreFilterMessage(ref Message m)
    {
        int msg = m.Msg;
        if (msg == 16)
        {
            dead = true;
        }
        return false;
    }

    public void AfterLoad()
    {
        if (base.Parent is Form)
        {
            isForm = true;
        }
        if (!isForm)
        {
            CallRuntimeDBOperation += OnCallRemoteDBOperation;
        }
        m_ScriptEngine = new ScriptEngine(ScriptLanguage.VBscript);
        init.Show();
        init.Say("初始化UI线程..");
        CControl.UIThreadControl = this;
        init.Say("读取工程文件..");
        Assembly assembly;
        if (isForm)
        {
            assembly = Assembly.Load("CustomLogic");
        }
        else
        {
            FileInfo fileInfo = new(Assembly.GetCallingAssembly().Location);
            string text = fileInfo.DirectoryName + "\\";
            byte[] datas = ((!File.Exists(text + "customlogic.dll")) ? Encoding.Unicode.GetBytes(DateTime.MinValue.ToString()) : Encoding.Unicode.GetBytes(File.GetLastWriteTime(text + "customlogic.dll").ToString()));
            byte[] array;
            using (new WebClient())
            {
                CallAnsyncUploadDataDele callAnsyncUploadDataDele = OnCallAnsyncUploadData;
                IAsyncResult asyncResult = callAnsyncUploadDataDele.BeginInvoke(absoluteUrl + "GetCustomLogic/?compress=deflate", datas, null, null);
                while (!asyncResult.IsCompleted)
                {
                    Application.DoEvents();
                    if (dead)
                    {
                        init.Hide();
                        return;
                    }
                    Thread.Sleep(0);
                }
                array = callAnsyncUploadDataDele.EndInvoke(asyncResult);
            }
            if (array.Length != 0)
            {
                array = DeflateCompress.Decompress(array);
                File.WriteAllBytes(text + "customlogic.dll", array);
            }
            else
            {
                array = File.ReadAllBytes(text + "customlogic.dll");
            }
            assembly = Assembly.Load(array);
        }
        G = assembly.GetType("LogicPage.Globle");
        init.Say("加载组态动态类型..");
        GlobleObj = Activator.CreateInstance(G);
        init.Say("初始化脚本引擎..");
        m_ScriptEngine.AddObject("Globle", GlobleObj);
        m_ScriptEngine.AddObject("System", this);
        ResourceManager resourceManager = (ResourceManager)G.GetField("rm").GetValue(null);
        timer.Tick += timer_Tick;
        comtimer.Tick += comtimer_Tick;
        datathread = new Thread(DataWorkThread)
        {
            IsBackground = true
        };
        string name = (string)G.GetField("projectname").GetValue(null);
        MemoryStream memoryStream = null;
        byte[] input = resourceManager.GetObject(name, new CultureInfo("")) as byte[];
        init.Say("解压工程文件..");
        memoryStream = new MemoryStream(Operation.UncompressStream(input));
        init.Say("加载工程文件..");
        dhp = Operation.BinaryLoadProject(memoryStream, ipaddress, port, absoluteUrl);
        GlobalScriptBackUp = dhp.DstGlobalLogic;
        if (dhp == null)
        {
            dead = true;
            bOnInit = false;
            if (!dead)
            {
                return;
            }
            try
            {
                if (base.Parent is Form)
                {
                    ((Form)base.Parent).Close();
                }
                return;
            }
            catch
            {
                return;
            }
        }
        memoryStream.Dispose();
        DHMIImageManage.projectname = dhp.projectname;
        DHMIImageManage.ipaddress = dhp.ipaddress;
        DHMIImageManage.projectpath = AppDomain.CurrentDomain.BaseDirectory;
        comtimer.Interval = dhp.refreshtime;
        if (dhp.SetupPath != null)
        {
            installpath = dhp.SetupPath;
        }
        if (dhp.EnvironmentPath != null)
        {
            projectpath = dhp.EnvironmentPath;
        }
        if (dhp.projectname != null)
        {
            projectname = dhp.projectname;
        }
        init.Say("初始化运行环境参数..");
        timer.Interval = ((dhp.InvalidateTime == 0) ? 100 : dhp.InvalidateTime);
        base.Name = dhp.projectname;
        BackColor = dhp.ProjectBackColor;
        systimer.Interval = ((dhp.LogicTime < 50) ? 50 : dhp.LogicTime);
        systimer.Tick += systimer_Tick;
        init.Say("加载变量信息..");
        //input = resourceManager.GetObject(dhp.IOfiles, new CultureInfo("")) as byte[];
        //byte[] buffer = ((!dhp.Compress) ? input : Operation.UncompressStream(input));
        //using (memoryStream = new MemoryStream(buffer))
        //{
        //	xmldoc.Load(memoryStream);
        //	memoryStream.Close();
        //	memoryStream.Dispose();
        //}
        //XmlNodeList xmlNodeList = xmldoc.SelectNodes("/DocumentRoot/Item");

        //foreach (XmlNode item in xmlNodeList)
        //{
        //	VarTableItem varTableItem = new VarTableItem();
        //	varTableItem.Id = int.Parse(item.Attributes["id"].Value);
        //	if (varTableItem.Id > MaxID)
        //	{
        //		MaxID = varTableItem.Id;
        //	}
        //	varTableItem.Name = "[" + item.Attributes["Name"].Value + "]";
        //	varTableItem.Type = int.Parse(item.Attributes["ValType"].Value);
        //	varTableItem.Id2 = int.Parse(item.Attributes["id"].Value);
        //	DicIO.Add(varTableItem.Name, varTableItem);
        //}
        int num = MaxID + 1;
        Varlist = new object[MaxID + 1 + dhp.ProjectIOs.Count];
        for (int i = 0; i < MaxID + 1; i++)
        {
            Varlist[i] = 0;
        }
        init.Say("加载内部变量信息..");
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            VarTableItem varTableItem2 = new()
            {
                Id = num,
                Name = "[" + projectIO.name + "]"
            };
            if (projectIO.type == null)
            {
                projectIO.type = "1024";
            }
            varTableItem2.Type = int.Parse(projectIO.type);
            varTableItem2.Emluator = projectIO.emluator;
            varTableItem2.Cycle = projectIO.T;
            varTableItem2.Max = projectIO.max;
            varTableItem2.Min = projectIO.min;
            varTableItem2.Delay = projectIO.delay;
            varTableItem2.Indatabase = projectIO.History;
            varTableItem2.Id2 = projectIO.ID * -1;
            if (!DicIO.ContainsKey(varTableItem2.Name))
            {
                DicIO.Add(varTableItem2.Name, varTableItem2);
            }
            varTableItem2.Isalive = false;
            DicMe.Add(varTableItem2.Id2, varTableItem2);
            Varlist[num] = projectIO.type switch
            {
                "0" => false,
                "1" => Convert.ToSByte(0),
                "2" => Convert.ToByte(0),
                "3" => Convert.ToInt16(0),
                "4" => Convert.ToUInt16(0),
                "5" => Convert.ToInt32(0),
                "6" => Convert.ToUInt16(0),
                "7" => Convert.ToSingle(0),
                "8" => Convert.ToDouble(0),
                "9" => "",
                "10" => Convert.ToInt32(0),
                "11" => Convert.ToInt32(0),
                "1024" => null,
                _ => null,
            };
            num++;
        }
        init.Say("构建通信组件..");
        //client = new Client(Varlist, ParaDict)
        //{
        //    MsgLabel = label1
        //};
        //if (dhp.ipaddress != null && dhp.ipaddress != "")
        //{
        //    client.ServerIp = dhp.ipaddress;
        //}
        //if (dhp.port != null && dhp.port != "")
        //{
        //    client.Port = int.Parse(dhp.port);
        //}
        init.Say("初始化页面..");
        dfs = new List<DataFile>();
        foreach (string key in dhp.pages.Keys)
        {
            Type type = assembly.GetType("LogicPage.Page_" + key);
            object[] args = new object[1];
            object obj2 = Activator.CreateInstance(type, args);
            EventInfo @event = type.GetEvent("LoadPage");
            Type eventHandlerType = @event.EventHandlerType;
            MethodInfo method = GetType().GetMethod("OnDelayLoadPage");
            Delegate handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(obj2, handler);
            ((Control)obj2).Name = key;
            ((Control)obj2).Visible = false;
            base.Controls.Add((Control)obj2);
            m_ScriptEngine.AddObject(key, obj2);
            SO.Add(key, (Control)obj2);
            ((Control)obj2).VisibleChanged += OnVisibleChanged;
        }
        m_ScriptEngine.ExecuteStatement(GlobalScriptBackUp);
        init.Say("初始化事件引擎..");
        //client.VariableAlarmEvent += client_VariableAlarmEvent;
        //client.DeviceAlarmEvent += client_DeviceAlarmEvent;
        base.Resize += MainControl_Resize;
        oldsize = dhp.ProjectSize;
        LastPageHeight = oldsize.Height;
        base.Size = dhp.ProjectSize;
        EventInfo event2 = G.GetEvent("SetValueEvent");
        Type eventHandlerType2 = event2.EventHandlerType;
        MethodInfo method2 = GetType().GetMethod("SetValue");
        Delegate handler2 = Delegate.CreateDelegate(eventHandlerType2, this, method2);
        event2.AddEventHandler(GlobleObj, handler2);
        event2 = G.GetEvent("GetValueEvent");
        eventHandlerType2 = event2.EventHandlerType;
        method2 = GetType().GetMethod("GetValue");
        handler2 = Delegate.CreateDelegate(eventHandlerType2, this, method2);
        event2.AddEventHandler(GlobleObj, handler2);
        event2 = G.GetEvent("ExecuteEvent");
        eventHandlerType2 = event2.EventHandlerType;
        method2 = GetType().GetMethod("Execute");
        handler2 = Delegate.CreateDelegate(eventHandlerType2, this, method2);
        event2.AddEventHandler(GlobleObj, handler2);

        init.Say("正在启动数据通讯..");
        //client.Start();
        datathread.Start();
        Thread.Sleep(1000);
        timer.Enabled = true;
        comtimer.Enabled = true;
        systimer.Enabled = false;
        init.Say("获取页面文件时间戳..");
        if (!isForm)
        {
            List<string> list = new();
            foreach (string key2 in SO.Keys)
            {
                list.Add(absoluteUrl.Substring(absoluteUrl.IndexOf('/', 7)) + key2 + ".hpg");
            }
            BinaryFormatter binaryFormatter = new();
            byte[] bytes;
            using (MemoryStream memoryStream3 = new())
            {
                binaryFormatter.Serialize(memoryStream3, list);
                bytes = memoryStream3.ToArray();
                memoryStream3.Flush();
                memoryStream3.Close();
                memoryStream3.Dispose();
            }
            bytes = DeflateCompress.Compress(bytes);
            byte[] bytes2;
            using (WebClient webClient2 = new())
            {
                bytes2 = webClient2.UploadData(absoluteUrl + "GetModifyTime/?compress=deflate", bytes);
            }
            bytes2 = DeflateCompress.Decompress(bytes2);
            Dictionary<string, DateTime> dictionary = new();
            using (MemoryStream memoryStream4 = new(bytes2))
            {
                BinaryFormatter binaryFormatter2 = new();
                dictionary = (Dictionary<string, DateTime>)binaryFormatter2.UnsafeDeserialize(memoryStream4, null);
                memoryStream4.Close();
                memoryStream4.Dispose();
            }
            foreach (string key3 in dictionary.Keys)
            {
                if (!fileTimeDict.ContainsKey(key3))
                {
                    fileTimeDict.Add(key3, dictionary[key3]);
                }
            }
        }
        init.Say("启动工程..");
        systimer.Enabled = true;
        try
        {
            nDelayTick = int.Parse(dhp.gDXP_SleepTime);
        }
        catch
        {
            nDelayTick = 0;
        }
        timer2Delay.Interval = 1000;
        timer2Delay.Tick += timer2Delay_Tick;
        timer2Delay.Start();
    }

    public void DelayLoadPage(string pagename)
    {
        Type type = SO[pagename].GetType();
        MethodInfo method = type.GetMethod("DoLoadPage");
        method.Invoke(SO[pagename], new object[1] { pagename });
    }

    private void CallInformationLabel(string info, bool visible)
    {
        label1.Text = info;
        label1.Location = Point.Empty;
        label1.Visible = visible;
        if (label1.Visible)
        {
            label1.BringToFront();
        }
        else
        {
            label1.SendToBack();
        }
        label1.Refresh();
    }

    private void SetInformationLabel(string info, bool visible)
    {
        if (label1.InvokeRequired)
        {
            label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), info, visible);
        }
        else
        {
            CallInformationLabel(info, visible);
        }
    }

    private byte[] ReadFileData(string filename)
    {
        try
        {
            if (isForm)
            {
                return File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\" + filename);
            }
            int num = 0;
            string path;
            byte[] array;
            while (true)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + dhp.projectname + "\\" + dhp.ipaddress + "\\" + filename;
                array = new byte[0];
                try
                {
                    string key = (absoluteUrl + filename).Substring((absoluteUrl + filename).IndexOf('/', 7));
                    if (File.Exists(path) && fileTimeDict.ContainsKey(key) && fileTimeDict[key] <= File.GetLastWriteTime(path))
                    {
                        return File.ReadAllBytes(path);
                    }
                    using WebClient webClient = new();
                    using (Stream stream = webClient.OpenRead(absoluteUrl + filename))
                    {
                        if (File.Exists(path) && File.GetLastWriteTime(path) >= Convert.ToDateTime(webClient.ResponseHeaders[HttpResponseHeader.LastModified]))
                        {
                            return File.ReadAllBytes(path);
                        }
                        if (label1.InvokeRequired)
                        {
                            label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "下载" + filename + "：0/" + webClient.ResponseHeaders[HttpResponseHeader.ContentLength].ToString(), true);
                        }
                        else
                        {
                            CallInformationLabel("下载" + filename + "：0/" + webClient.ResponseHeaders[HttpResponseHeader.ContentLength].ToString(), visible: true);
                        }
                        byte[] array2 = new byte[32768];
                        int num2 = 0;
                        while ((num2 = stream.Read(array2, 0, 32768)) != 0)
                        {
                            if (label1.InvokeRequired)
                            {
                                label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "下载" + filename + "：" + (array.Length + num2) + "/" + webClient.ResponseHeaders[HttpResponseHeader.ContentLength].ToString(), true);
                            }
                            else
                            {
                                CallInformationLabel("下载" + filename + "：" + (array.Length + num2) + "/" + webClient.ResponseHeaders[HttpResponseHeader.ContentLength].ToString(), visible: true);
                            }
                            byte[] array3 = new byte[array.Length + num2];
                            array.CopyTo(array3, 0);
                            Array.Copy(array2, 0, array3, array.Length, num2);
                            array = array3;
                        }
                        if (label1.InvokeRequired)
                        {
                            label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "", false);
                        }
                        else
                        {
                            CallInformationLabel("", visible: false);
                        }
                        stream.Close();
                        stream.Dispose();
                    }
                    webClient.Dispose();
                }
                catch (Exception ex)
                {
                    if (label1.InvokeRequired)
                    {
                        label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "下载" + filename + "发生异常：" + ex.Message, true);
                    }
                    else
                    {
                        CallInformationLabel("下载" + filename + "发生异常：" + ex.Message, visible: true);
                    }
                    if (num < 3)
                    {
                        num++;
                        continue;
                    }
                    return null;
                }
                break;
            }
            try
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + dhp.projectname + "\\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + dhp.projectname + "\\");
                }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + dhp.projectname + "\\" + ipaddress + "\\"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + dhp.projectname + "\\" + ipaddress + "\\");
                }
                File.WriteAllBytes(path, array);
            }
            catch
            {
            }
            return array;
        }
        catch
        {
            return null;
        }
    }

    public DataFile OnDelayLoadPage(string pagename)
    {
        try
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "正在加载" + pagename + ".", true);
            }
            else
            {
                CallInformationLabel("正在加载" + pagename + ".", visible: true);
            }
        }
        catch
        {
        }
        try
        {
            MemoryStream memoryStream = ((!dhp.Compress) ? new MemoryStream(ReadFileData(dhp.pages[pagename])) : new MemoryStream(Operation.UncompressStream(ReadFileData(dhp.pages[pagename]))));
            DataFile dataFile = Operation.BinaryLoadFile(memoryStream);
            memoryStream.Dispose();
            lock (SD)
            {
                if (SD.ContainsKey(pagename))
                {
                    return SD[pagename];
                }
                SD.Add(pagename, dataFile);
            }
            lock (dfs)
            {
                dfs.Add(dataFile);
            }
            if (base.InvokeRequired)
            {
                Invoke(new CallInitPageControlDele(CallInitPageControl), pagename, dataFile);
            }
            else
            {
                CallInitPageControl(pagename, dataFile);
            }
            Type type = SO[pagename].GetType();
            FieldInfo field = type.GetField("datafile");
            field.SetValue(SO[pagename], dataFile);
            EventInfo @event = type.GetEvent("SetValueEvent");
            Type eventHandlerType = @event.EventHandlerType;
            MethodInfo method = GetType().GetMethod("SetValue");
            Delegate handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            @event = type.GetEvent("GetValueEvent");
            eventHandlerType = @event.EventHandlerType;
            method = GetType().GetMethod("GetValue");
            handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            @event = type.GetEvent("EvalEvent");
            eventHandlerType = @event.EventHandlerType;
            method = GetType().GetMethod("Eval");
            handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            @event = type.GetEvent("ExecuteEvent2");
            eventHandlerType = @event.EventHandlerType;
            method = GetType().GetMethod("Execute2");
            handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            @event = type.GetEvent("ExecuteEvent");
            eventHandlerType = @event.EventHandlerType;
            method = GetType().GetMethod("Execute");
            handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            @event = type.GetEvent("CallRuntime");
            eventHandlerType = @event.EventHandlerType;
            method = GetType().GetMethod("OnCallRuntime");
            handler = Delegate.CreateDelegate(eventHandlerType, this, method);
            @event.AddEventHandler(SO[pagename], handler);
            ScriptEngine scriptEngine = new(ScriptLanguage.VBscript);
            ((Control)SO[pagename]).Tag = scriptEngine;
            scriptEngine.AddObject("Globle", GlobleObj);
            scriptEngine.AddObject("System", this);
            foreach (object value in SO.Values)
            {
                Control control = (Control)value;
                scriptEngine.AddObject(control.Name, control);
            }
            ((UserControl)SO[pagename]).MouseDown += OnMouseDown;
            ((UserControl)SO[pagename]).MouseMove += OnMouseMove;
            ((UserControl)SO[pagename]).MouseUp += OnMouseUp;
            ((UserControl)SO[pagename]).MouseDoubleClick += OnMouseDoubleClick;
            ((UserControl)SO[pagename]).Paint += MainControl_Paint;
            ((UserControl)SO[pagename]).Scroll += MainControl_Scroll;
            scriptEngine.ExecuteStatement(GlobalScriptBackUp);
            try
            {
                if (label1.InvokeRequired)
                {
                    label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "", false);
                }
                else
                {
                    CallInformationLabel("", visible: false);
                }
            }
            catch
            {
            }
            return dataFile;
        }
        catch (Exception ex)
        {
            try
            {
                if (label1.InvokeRequired)
                {
                    label1.BeginInvoke(new CallInformationLabelDelegate(CallInformationLabel), "正在加载" + pagename + ".", true);
                }
                else
                {
                    CallInformationLabel("加载" + pagename + "失败.", visible: true);
                }
            }
            catch
            {
            }
            throw ex;
        }
    }

    private void CallInitPageControl(string pagename, DataFile df)
    {
        Operation.AutoAddControlsToFormOrControl(df.ListAllShowCShape, ((Control)SO[pagename]).Controls);
        ((Control)SO[pagename]).Location = new Point(Convert.ToInt32(df.locationf.X * zoomX), Convert.ToInt32(df.locationf.Y * zoomY));
        ((Control)SO[pagename]).Size = new Size(Convert.ToInt32(df.sizef.Width * zoomX), Convert.ToInt32(df.sizef.Height * zoomY));
        ((Control)SO[pagename]).BackColor = df.color;
        ((Control)SO[pagename]).BackgroundImage = df.pageimage;
        ((Control)SO[pagename]).BackgroundImageLayout = df.pageImageLayout;
        ((Control)SO[pagename]).Name = df.name;
        ((Control)SO[pagename]).Visible = false;
        DataFile[] array = dfs.ToArray();
        foreach (DataFile dataFile in array)
        {
            if (!(dataFile.name == pagename))
            {
                continue;
            }
            for (int j = 0; j < dataFile.ListAllShowCShape.Count; j++)
            {
                if (dataFile.ListAllShowCShape[j] is CControl)
                {
                    try
                    {
                        ((CControl)dataFile.ListAllShowCShape[j]).Width = df.ListAllShowCShape[j].DefaultSize.Width * zoomX;
                        ((CControl)dataFile.ListAllShowCShape[j]).Height = df.ListAllShowCShape[j].DefaultSize.Height * zoomY;
                        ((CControl)dataFile.ListAllShowCShape[j]).X = Convert.ToInt32((float)df.ListAllShowCShape[j].DefaultLocaion.X * zoomX);
                        ((CControl)dataFile.ListAllShowCShape[j]).Y = Convert.ToInt32((float)df.ListAllShowCShape[j].DefaultLocaion.Y * zoomY);
                        float num = ((CControl)dataFile.ListAllShowCShape[j]).DefaultFontSize * ((zoomX < zoomY) ? zoomX : zoomY);
                        num = ((num < 8.25f) ? 8.25f : num);
                        ((CControl)dataFile.ListAllShowCShape[j])._c.Font = new Font(((CControl)dataFile.ListAllShowCShape[j])._c.Font.Name, num);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        pageNeedResize.Add(df.name);
        foreach (Control control in ((Control)SO[pagename]).Controls)
        {
            if (control is IDCCEControl)
            {
                ((IDCCEControl)control).GetValueEvent += UserControl1_GetValueEvent;
                ((IDCCEControl)control).SetValueEvent += UserControl1_SetValueEvent;
                //((IDCCEControl)control).GetDataBaseEvent += UserControl1_GetDataBaseEvent;
                ((IDCCEControl)control).GetVarTableEvent += UserControl1_GetVarTableEvent;
                ((IDCCEControl)control).GetSystemItemEvent += MainControl_GetSystemItemEvent;
                if (control is CButton)
                {
                    ((CButton)control).Click += UserDefButton_Clicked;
                }
                if (control is CDataGridView)
                {
                    ((CDataGridView)control).VisibleChanged += UserDefControl_VisibleChanged;
                }
                ((IDCCEControl)control).isRuning = true;
            }
        }
        foreach (CShape item in df.ListAllShowCShape)
        {
            if (item is CPixieControl)
            {
                ((CPixieControl)item).GetValueEvent += UserControl1_GetValueEvent;
                ((CPixieControl)item).SetValueEvent += UserControl1_SetValueEvent;
                ((CPixieControl)item).OnGetVarTable += UserControl1_GetVarTableEvent;
                ((CPixieControl)item).isRunning = true;
            }
            item.DBOperationOK += OnShapeDBOperationOK;
            item.DBOperationErr += OnShapeDBOperationErr;
        }
 
        df.visable = false;
    }

    private void OnShapeDBOperationErr(object sender, EventArgs e)
    {
        CShape cShape = (CShape)sender;
        if (cShape.DBErrScript == null || cShape.DBErrScript == "")
        {
            return;
        }
        try
        {
            m_ScriptEngine.ExecuteStatement(cShape.DBErrScript);
        }
        catch (COMException ex)
        {
            MessageBox.Show(ex.Message + m_ScriptEngine.Error.Line);
        }
        catch (Exception ex2)
        {
            MessageBox.Show(ex2.Message);
        }
    }

    private void OnShapeDBOperationOK(object sender, EventArgs e)
    {
        CShape cShape = (CShape)sender;
        if (cShape.DBOKScript == null || cShape.DBOKScript == "")
        {
            return;
        }
        try
        {
            m_ScriptEngine.ExecuteStatement(cShape.DBOKScript);
        }
        catch (COMException ex)
        {
            MessageBox.Show(ex.Message + m_ScriptEngine.Error.Line);
        }
        catch (Exception ex2)
        {
            MessageBox.Show(ex2.Message);
        }
    }

    private void CallDBAnimation(IDBAnimation shape)
    {
        try
        {
            if (shape == null)
            {
                return;
            }
            if (shape.Newtable)
            {
                ExecuteButtonNewtable(shape);
            }
            if (shape.Dbselect)
            {
                ExecuteButtonDBSelect(shape);
            }
            if (shape.Dbinsert)
            {
                ExecuteButtonDBInsert(shape);
            }
            if (shape.Dbupdate)
            {
                ExecuteButtonDBUpdate(shape);
            }
            if (shape.Dbdelete)
            {
                ExecuteButtonDBDelete(shape);
            }
            if (!shape.Dbmultoperation)
            {
                return;
            }
            foreach (DBAnimation dBAnimation in shape.DBAnimationList)
            {
                if (dBAnimation.Type == "插入数据")
                {
                    dBAnimation.Type = "添加数据";
                }
                switch (dBAnimation.Type)
                {
                    case "查询数据":
                        ExecuteButtonDBSelect(shape, dBAnimation as DBSelectAnimation);
                        break;
                    case "添加数据":
                        ExecuteButtonDBInsert(shape, dBAnimation as DBInsertAnimation);
                        break;
                    case "修改数据":
                        ExecuteButtonDBUpdate(shape, dBAnimation as DBUpdateAnimation);
                        break;
                    case "删除数据":
                        ExecuteButtonDBDelete(shape, dBAnimation as DBDeleteAnimation);
                        break;
                }
            }
        }
        catch
        {
            shape.FireDBOperationErr();
        }
    }

    private void UserDefButton_Clicked(object sender, EventArgs e)
    {
        if (sender is IDBAnimation)
        {
            CallDBAnimation(sender as IDBAnimation);
        }
    }

    private void UserDefControl_VisibleChanged(object sender, EventArgs e)
    {
        if (sender is IDBAnimation && sender is Control && (sender as Control).Visible)
        {
            CallDBAnimation(sender as IDBAnimation);
        }
    }

    private string ReplaceSQLValue(string cmd)
    {
        Regex regex = new("{.*?}");
        MatchCollection matchCollection = regex.Matches(cmd);
        foreach (Match item in matchCollection)
        {
            try
            {
                if (item.Value[1] == '[' && item.Value[2] != '[' && item.Value[item.Value.Length - 2] == ']')
                {
                    object value = GetValue(item.Value.Substring(1, item.Value.Length - 2));
                    string text = "";
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                    cmd = cmd.Replace(item.Value, text.Replace("'", "''"));
                }
                if (item.Value[1] == '[' && item.Value[2] == '[')
                {
                    object value2 = GetValue(item.Value.Substring(2, item.Value.Length - 4));
                    object obj = new();
                    string text2 = "";
                    if (value2 != null)
                    {
                        text2 = value2.ToString();
                        obj = GetValue(text2);
                        if (obj != null)
                        {
                            text2 = obj.ToString();
                            cmd = cmd.Replace(item.Value, text2.Replace("'", "''"));
                        }
                    }
                }
                else
                {
                    if (!item.Value.Contains("."))
                    {
                        continue;
                    }
                    string[] array = item.Value.Substring(1, item.Value.Length - 2).Split('.');
                    if (!SO.ContainsKey(array[0]))
                    {
                        continue;
                    }
                    if (!SD.ContainsKey(array[0]))
                    {
                        DelayLoadPage(array[0]);
                    }
                    DataFile dataFile = SD[array[0]];
                    foreach (CShape item2 in dataFile.ListAllShowCShape)
                    {
                        if (item2.Name != array[1])
                        {
                            continue;
                        }
                        if (item2 is CControl)
                        {
                            if (((CControl)item2)._c is CLabel || ((CControl)item2)._c is CTextBox || ((CControl)item2)._c is CButton)
                            {
                                cmd = ((array.Length != 3 || !(array[2] == "Tag")) ? cmd.Replace(item.Value, ((CControl)item2)._c.Text.Replace("'", "''")) : cmd.Replace(item.Value, ((CControl)item2)._c.Tag.ToString().Replace("'", "''")));
                            }
                            else if (((CControl)item2)._c is CComboBox)
                            {
                                cmd = ((array.Length == 3 && array[2] == "Tag") ? cmd.Replace(item.Value, ((CComboBox)((CControl)item2)._c).SelectedTag.ToString().Replace("'", "''")) : ((array.Length != 3 || !(array[2] == "Text")) ? cmd.Replace(item.Value, ((CComboBox)((CControl)item2)._c).SelectedItem.ToString().Replace("'", "''")) : cmd.Replace(item.Value, ((CComboBox)((CControl)item2)._c).Text.ToString().Replace("'", "''"))));
                            }
                            else if (((CControl)item2)._c is CListBox)
                            {
                                cmd = ((array.Length != 3 || !(array[2] == "Tag")) ? cmd.Replace(item.Value, ((CListBox)((CControl)item2)._c).SelectedItem.ToString().Replace("'", "''")) : cmd.Replace(item.Value, ((CListBox)((CControl)item2)._c).tags[((CListBox)((CControl)item2)._c).SelectedIndex].ToString().Replace("'", "''")));
                            }
                            else if (((CControl)item2)._c is CCheckBox)
                            {
                                cmd = ((array.Length == 3 && array[2] == "Tag") ? cmd.Replace(item.Value, ((CCheckBox)((CControl)item2)._c).Tag.ToString().Replace("'", "''")) : ((array.Length != 3 || !(array[2] == "Value")) ? cmd.Replace(item.Value, ((CCheckBox)((CControl)item2)._c).Checked.ToString()) : cmd.Replace(item.Value, ((CCheckBox)((CControl)item2)._c).Checked.ToString())));
                            }
                            else if (((CControl)item2)._c is CDateTimePicker)
                            {
                                cmd = ((array.Length != 3 || !(array[2] == "Tag")) ? cmd.Replace(item.Value, ((CDateTimePicker)((CControl)item2)._c).Value.ToString().Replace("'", "''")) : cmd.Replace(item.Value, ((CDateTimePicker)((CControl)item2)._c).Tag.ToString().Replace("'", "''")));
                            }
                        }
                        else if (item2 is CString)
                        {
                            cmd = ((array.Length != 3 || !(array[2] == "Tag")) ? cmd.Replace(item.Value, ((CString)item2).DisplayStr.Replace("'", "''")) : cmd.Replace(item.Value, ((CString)item2).Tag.ToString().Replace("'", "''")));
                        }
                        break;
                    }
                    continue;
                }
            }
            catch
            {
            }
        }
        return cmd;
    }

    private void FillDBResultTo(string[] tocmds, DataSet ds)
    {
        int num = -1;
        foreach (string text in tocmds)
        {
            num++;
            if (text.Contains("."))
            {
                string[] array = text.Substring(1, text.Length - 2).Split('.');
                if (!SO.ContainsKey(array[0]))
                {
                    continue;
                }
                if (!SD.ContainsKey(array[0]))
                {
                    DelayLoadPage(array[0]);
                }
                DataFile dataFile = SD[array[0]];
                foreach (CShape item in dataFile.ListAllShowCShape)
                {
                    if (item.Name != array[1])
                    {
                        continue;
                    }
                    if (item is CControl)
                    {
                        if (((CControl)item)._c is CDataGridView)
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                int count = ((CDataGridView)((CControl)item)._c).Rows.Count;
                                int count2 = ds.Tables[0].Rows.Count;
                                int num2 = 0;
                                num2 = ((count >= count2) ? count2 : count);
                                for (int j = 0; j < num2; j++)
                                {
                                    ((CDataGridView)((CControl)item)._c).Rows[j].Tag = ds.Tables[0].Rows[j][num];
                                }
                            }
                            else
                            {
                                ((CDataGridView)((CControl)item)._c).DataSource = ds.Tables[0];
                                ((CDataGridView)((CControl)item)._c).AutoResizeColumns();
                            }
                        }
                        else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0 && (((CControl)item)._c is CLabel || ((CControl)item)._c is CTextBox || ((CControl)item)._c is CButton))
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                ((CControl)item)._c.Tag = ds.Tables[0].Rows[0][num];
                            }
                            else
                            {
                                ((CControl)item)._c.Text = ds.Tables[0].Rows[0][num].ToString();
                            }
                        }
                        else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0 && ((CControl)item)._c is CListBox)
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                int count3 = ((CListBox)((CControl)item)._c).tags.Count;
                                int count4 = ds.Tables[0].Rows.Count;
                                int num3 = 0;
                                num3 = ((count3 >= count4) ? count4 : count3);
                                for (int k = 0; k < num3; k++)
                                {
                                    ((CListBox)((CControl)item)._c).tags[k] = ds.Tables[0].Rows[k][num];
                                }
                            }
                            else
                            {
                                ((CListBox)((CControl)item)._c).Items.Clear();
                                ((CListBox)((CControl)item)._c).tags.Clear();
                                for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                                {
                                    ((CListBox)((CControl)item)._c).Items.Add(ds.Tables[0].Rows[l][num]);
                                    ((CListBox)((CControl)item)._c).tags.Add(ds.Tables[0].Rows[l][num]);
                                }
                            }
                        }
                        else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0 && ((CControl)item)._c is CComboBox)
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                int count5 = ((CComboBox)((CControl)item)._c).Items.Count;
                                int count6 = ds.Tables[0].Rows.Count;
                                int num4 = 0;
                                num4 = ((count5 >= count6) ? count6 : count5);
                                for (int m = 0; m < num4; m++)
                                {
                                    ((CComboBox)((CControl)item)._c).tags[m] = ds.Tables[0].Rows[m][num];
                                }
                            }
                            else
                            {
                                ((CComboBox)((CControl)item)._c).Items.Clear();
                                ((CComboBox)((CControl)item)._c).tags.Clear();
                                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                                {
                                    ((CComboBox)((CControl)item)._c).Items.Add(ds.Tables[0].Rows[n][num]);
                                    ((CComboBox)((CControl)item)._c).tags.Add(ds.Tables[0].Rows[n][num]);
                                }
                            }
                        }
                        else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0 && ((CControl)item)._c is CCheckBox)
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                ((CControl)item)._c.Tag = ds.Tables[0].Rows[0][num];
                            }
                            else if (array.Length == 3 && array[2] == "Value")
                            {
                                try
                                {
                                    ((CCheckBox)((CControl)item)._c).Checked = Convert.ToBoolean(ds.Tables[0].Rows[0][num]);
                                }
                                catch
                                {
                                    ((CCheckBox)((CControl)item)._c).Text = Convert.ToString(ds.Tables[0].Rows[0][num]);
                                }
                            }
                            else
                            {
                                ((CCheckBox)((CControl)item)._c).Text = Convert.ToString(ds.Tables[0].Rows[0][num]);
                            }
                        }
                        else if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Count > 0 && ((CControl)item)._c is CDateTimePicker)
                        {
                            if (array.Length == 3 && array[2] == "Tag")
                            {
                                ((CDateTimePicker)((CControl)item)._c).Tag = ds.Tables[0].Rows[0][num];
                            }
                            else
                            {
                                ((CDateTimePicker)((CControl)item)._c).Value = Convert.ToDateTime(ds.Tables[0].Rows[0][num]);
                            }
                        }
                    }
                    else if (item is CString)
                    {
                        if (array.Length == 3 && array[2] == "Tag")
                        {
                            ((CString)item).Tag = ds.Tables[0].Rows[0][num];
                        }
                        else
                        {
                            ((CString)item).DisplayStr = ds.Tables[0].Rows[0][num].ToString();
                        }
                    }
                    break;
                }
            }
            else if (text[1] == '[' && text[text.Length - 2] == ']')
            {
                if (DicIO[text.Substring(1, text.Length - 2)].Type == 1024)
                {
                    SetValue(text.Substring(1, text.Length - 2), ds.Tables[0]);
                }
                else
                {
                    SetValue(text.Substring(1, text.Length - 2), ds.Tables[0].Rows[0][num]);
                }
            }
        }
    }

    private void DBAnsyncOperation(object obj)
    {
        Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
        IDBAnimation iDBAnimation = (IDBAnimation)dictionary["shape"];
        string text = (string)dictionary["operation"];
        try
        {
            switch (text)
            {
                case "Select":
                    {
                        string dbdeleteSQL = iDBAnimation.DbselectSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        object obj8 = CallDBOperation(dbdeleteSQL, "select");
                        SetInformationLabel("", visible: false);
                        if (obj8 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj8 as Exception).Message, visible: true);
                            throw obj8 as Exception;
                        }
                        if (obj8 is DataSet)
                        {
                            DataSet value2 = obj8 as DataSet;
                            dictionary.Add("ds", value2);
                            break;
                        }
                        throw new Exception();
                    }
                case "Update":
                    {
                        string dbdeleteSQL = iDBAnimation.DbupdateSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj9 = CallDBOperation(dbdeleteSQL, "update");
                        SetInformationLabel("", visible: false);
                        if (obj9 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj9 as Exception).Message, visible: true);
                            throw obj9 as Exception;
                        }
                        if (obj9 is not int dBResult6)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult6;
                        break;
                    }
                case "Insert":
                    {
                        string dbdeleteSQL = iDBAnimation.DbinsertSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj4 = CallDBOperation(dbdeleteSQL, "insert");
                        SetInformationLabel("", visible: false);
                        if (obj4 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj4 as Exception).Message, visible: true);
                            throw obj4 as Exception;
                        }
                        if (obj4 is not int dBResult2)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult2;
                        break;
                    }
                case "Delete":
                    {
                        string dbdeleteSQL = iDBAnimation.DbdeleteSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj5 = CallDBOperation(dbdeleteSQL, "delete");
                        SetInformationLabel("", visible: false);
                        if (obj5 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj5 as Exception).Message, visible: true);
                            throw obj5 as Exception;
                        }
                        if (obj5 is not int dBResult3)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult3;
                        break;
                    }
                case "DBSelectAnimation":
                    {
                        DBSelectAnimation dBSelectAnimation = (DBSelectAnimation)dictionary["anicfg"];
                        string dbdeleteSQL = dBSelectAnimation.dbselectSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj3 = CallDBOperation(dbdeleteSQL, "select");
                        SetInformationLabel("", visible: false);
                        if (obj3 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj3 as Exception).Message, visible: true);
                            throw obj3 as Exception;
                        }
                        if (obj3 is DataSet)
                        {
                            DataSet value = obj3 as DataSet;
                            dictionary.Add("ds", value);
                            break;
                        }
                        throw new Exception();
                    }
                case "DBUpdateAnimation":
                    {
                        DBUpdateAnimation dBUpdateAnimation = (DBUpdateAnimation)dictionary["anicfg"];
                        string dbdeleteSQL = dBUpdateAnimation.dbupdateSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj6 = CallDBOperation(dbdeleteSQL, "update");
                        SetInformationLabel("", visible: false);
                        if (obj6 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj6 as Exception).Message, visible: true);
                            throw obj6 as Exception;
                        }
                        if (obj6 is not int dBResult4)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult4;
                        break;
                    }
                case "DBInsertAnimation":
                    {
                        DBInsertAnimation dBInsertAnimation = (DBInsertAnimation)dictionary["anicfg"];
                        string dbdeleteSQL = dBInsertAnimation.dbinsertSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj7 = CallDBOperation(dbdeleteSQL, "insert");
                        SetInformationLabel("", visible: false);
                        if (obj7 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj7 as Exception).Message, visible: true);
                            throw obj7 as Exception;
                        }
                        if (obj7 is not int dBResult5)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult5;
                        break;
                    }
                case "DBDeleteAnimation":
                    {
                        DBDeleteAnimation dBDeleteAnimation = (DBDeleteAnimation)dictionary["anicfg"];
                        string dbdeleteSQL = dBDeleteAnimation.dbdeleteSQL;
                        dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
                        SetInformationLabel("正在操作数据库...", visible: true);
                        object obj2 = CallDBOperation(dbdeleteSQL, "delete");
                        SetInformationLabel("", visible: false);
                        if (obj2 is Exception)
                        {
                            SetInformationLabel("操作数据库错误:" + (obj2 as Exception).Message, visible: true);
                            throw obj2 as Exception;
                        }
                        if (obj2 is not int dBResult)
                        {
                            throw new Exception();
                        }
                        iDBAnimation.DBResult = dBResult;
                        break;
                    }
            }
        }
        catch
        {
            dictionary.Add("err", true);
        }
        Invoke(new DBAnsyncOperationCompleteDelegate(DBAnsyncOperationComplete), obj);
    }

    private void OnCallRemoteServerLogic(ServerLogicRequest req)
    {
        foreach (string item in new List<string>(req.InputDict.Keys))
        {
            object value = GetValue(item);
            if (value != null && Attribute.IsDefined(value.GetType(), typeof(SerializableAttribute)))
            {
                req.InputDict[item] = value;
            }
            else
            {
                req.InputDict[item] = null;
            }
        }
        byte[] bytes;
        using (MemoryStream memoryStream = new())
        {
            BinaryFormatter binaryFormatter = new();
            binaryFormatter.Serialize(memoryStream, req);
            bytes = memoryStream.ToArray();
            memoryStream.Flush();
            memoryStream.Close();
            memoryStream.Dispose();
        }
        bytes = DeflateCompress.Compress(bytes);
        WebClient webClient = new();
        if (req.Ansync)
        {
            webClient.UploadDataCompleted += ServerLogic_Completed;
            webClient.UploadDataAsync(new Uri(absoluteUrl + "ServerLogic/?compress=deflate"), bytes);
        }
        else
        {
            byte[] receiveBytes = webClient.UploadData(new Uri(absoluteUrl + "ServerLogic/?compress=deflate"), bytes);
            ServerLogicOK_SetValue(receiveBytes);
            webClient.Dispose();
        }
    }

    private void ServerLogic_Completed(object sender, UploadDataCompletedEventArgs e)
    {
        Invoke(new Action<byte[]>(ServerLogicOK_SetValue), e.Result);
        (sender as WebClient).Dispose();
    }

    private void ServerLogicOK_SetValue(byte[] receiveBytes)
    {
        receiveBytes = DeflateCompress.Decompress(receiveBytes);
        using MemoryStream serializationStream = new(receiveBytes);
        BinaryFormatter binaryFormatter = new();
        object obj = binaryFormatter.UnsafeDeserialize(serializationStream, null);
        if (obj is Exception)
        {
            SetInformationLabel("服务器逻辑执行错误:" + (obj as Exception).Message, visible: true);
            return;
        }
        Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
        foreach (string key in dictionary.Keys)
        {
            SetValue(key, dictionary[key]);
        }
    }

    private byte[] OnCallAnsyncUploadData(string url, byte[] datas)
    {
        using WebClient webClient = new();
        return webClient.UploadData(url, datas);
    }

    private object OnCallRemoteDBOperation(string type, string cmd)
    {
        byte[] bytes;
        using (WebClient webClient = new())
        {
            bytes = webClient.UploadData(absoluteUrl + "DBOperation/?type=" + type + "&compress=deflate", DeflateCompress.Compress(Encoding.Unicode.GetBytes(cmd)));
        }
        bytes = DeflateCompress.Decompress(bytes);
        BinaryFormatter binaryFormatter = new();
        using MemoryStream memoryStream = new(bytes);
        object result = binaryFormatter.UnsafeDeserialize(memoryStream, null);
        memoryStream.Flush();
        memoryStream.Close();
        memoryStream.Dispose();
        return result;
    }

    private object CallDBOperation(string cmd, string type)
    {
        if (this.CallRuntimeDBOperation != null)
        {
            return this.CallRuntimeDBOperation(type, cmd);
        }
        throw new Exception("CallDBOperation Err.");
    }

    private void CallServerLogic(ServerLogicRequest req)
    {
        if (isForm)
        {
            if (this.CallRuntimeServerLogic == null)
            {
                return;
            }
            try
            {
                foreach (string item in new List<string>(req.InputDict.Keys))
                {
                    object value = GetValue(item);
                    if (value != null && Attribute.IsDefined(value.GetType(), typeof(SerializableAttribute)))
                    {
                        req.InputDict[item] = value;
                    }
                    else
                    {
                        req.InputDict[item] = null;
                    }
                }
                Dictionary<string, object> dictionary = this.CallRuntimeServerLogic(req);
                foreach (string key in dictionary.Keys)
                {
                    SetValue(key, dictionary[key]);
                }
                return;
            }
            catch (Exception ex)
            {
                SetInformationLabel("服务器逻辑执行错误:" + ex.Message, visible: true);
                return;
            }
        }
        OnCallRemoteServerLogic(req);
    }

    public object OnCallRuntime(string str, object obj)
    {
        if (str == "服务器逻辑")
        {
            try
            {
                if (obj is ServerLogicRequest)
                {
                    CallServerLogic(obj as ServerLogicRequest);
                }
                return null;
            }
            catch (Exception ex)
            {
                SetInformationLabel("服务器逻辑执行错误:" + ex.Message, visible: true);
            }
        }
        return null;
    }

    private void DBAnsyncOperationComplete(object obj)
    {
        Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
        IDBAnimation iDBAnimation = (IDBAnimation)dictionary["shape"];
        string text = (string)dictionary["operation"];
        if (dictionary.ContainsKey("err") && (bool)dictionary["err"])
        {
            iDBAnimation.FireDBOperationErr();
            return;
        }
        try
        {
            switch (text)
            {
                case "Newtable":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "Select":
                    {
                        DataSet dataSet = (DataSet)dictionary["ds"];
                        string[] tocmds = iDBAnimation.DbselectTO.Split(',');
                        FillDBResultTo(tocmds, dataSet);
                        try
                        {
                            iDBAnimation.DBResult = dataSet.Tables[0].Rows.Count;
                        }
                        catch
                        {
                        }
                        iDBAnimation.FireDBOperationOK();
                        break;
                    }
                case "Update":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "Insert":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "Delete":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "NewtableAnimation":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "DBSelectAnimation":
                    {
                        DataSet dataSet = (DataSet)dictionary["ds"];
                        DBSelectAnimation dBSelectAnimation = (DBSelectAnimation)dictionary["anicfg"];
                        string[] tocmds = dBSelectAnimation.dbselectTO.Split(',');
                        FillDBResultTo(tocmds, dataSet);
                        try
                        {
                            iDBAnimation.DBResult = dataSet.Tables[0].Rows.Count;
                        }
                        catch
                        {
                        }
                        iDBAnimation.FireDBOperationOK();
                        break;
                    }
                case "DBUpdateAnimation":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "DBInsertAnimation":
                    iDBAnimation.FireDBOperationOK();
                    break;
                case "DBDeleteAnimation":
                    iDBAnimation.FireDBOperationOK();
                    break;
            }
        }
        catch
        {
            iDBAnimation.FireDBOperationErr();
        }
    }

    private void ExecuteButtonNewtable(IDBAnimation c)
    {
        try
        {
            if (c.Ansyncnewtable)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "Create" },
                    { "shape", c }
                };
                thread.Start(dictionary);
                return;
            }
            string newtableSQL = c.NewtableSQL;
            newtableSQL = ReplaceSQLValue(newtableSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(newtableSQL, "create");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is not int dBResult)
            {
                throw new Exception();
            }
            c.DBResult = dBResult;
            c.FireDBOperationOK();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBSelect(IDBAnimation c, DBSelectAnimation anicfg)
    {
        try
        {
            if (c.Ansyncselect)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "DBSelectAnimation" },
                    { "shape", c },
                    { "anicfg", anicfg }
                };
                thread.Start(dictionary);
                return;
            }
            string dbselectSQL = anicfg.dbselectSQL;
            dbselectSQL = ReplaceSQLValue(dbselectSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbselectSQL, "select");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is DataSet)
            {
                DataSet dataSet = obj as DataSet;
                string[] tocmds = anicfg.dbselectTO.Split(',');
                FillDBResultTo(tocmds, dataSet);
                try
                {
                    c.DBResult = dataSet.Tables[0].Rows.Count;
                }
                catch
                {
                }
                c.FireDBOperationOK();
                return;
            }
            throw new Exception();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBSelect(IDBAnimation c)
    {
        try
        {
            if (c.Ansyncselect)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "Select" },
                    { "shape", c }
                };
                thread.Start(dictionary);
                return;
            }
            string dbselectSQL = c.DbselectSQL;
            dbselectSQL = ReplaceSQLValue(dbselectSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbselectSQL, "select");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is DataSet)
            {
                DataSet dataSet = obj as DataSet;
                try
                {
                    c.DBResult = dataSet.Tables[0].Rows.Count;
                }
                catch
                {
                }
                string[] tocmds = c.DbselectTO.Split(',');
                FillDBResultTo(tocmds, dataSet);
                c.FireDBOperationOK();
                return;
            }
            throw new Exception();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBInsert(IDBAnimation c, DBInsertAnimation anicfg)
    {
        try
        {
            if (c.Ansyncselect)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "DBInsertAnimation" },
                    { "shape", c },
                    { "anicfg", anicfg }
                };
                thread.Start(dictionary);
                return;
            }
            string dbinsertSQL = anicfg.dbinsertSQL;
            dbinsertSQL = ReplaceSQLValue(dbinsertSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbinsertSQL, "insert");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is not int dBResult)
            {
                throw new Exception();
            }
            c.DBResult = dBResult;
            c.FireDBOperationOK();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBInsert(IDBAnimation c)
    {
        try
        {
            if (c.Ansyncinsert)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "Insert" },
                    { "shape", c }
                };
                thread.Start(dictionary);
                return;
            }
            string dbinsertSQL = c.DbinsertSQL;
            dbinsertSQL = ReplaceSQLValue(dbinsertSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbinsertSQL, "insert");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is not int dBResult)
            {
                throw new Exception();
            }
            c.DBResult = dBResult;
            c.FireDBOperationOK();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBUpdate(IDBAnimation c, DBUpdateAnimation anicfg)
    {
        try
        {
            if (c.Ansyncselect)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "DBUpdateAnimation" },
                    { "shape", c },
                    { "anicfg", anicfg }
                };
                thread.Start(dictionary);
                return;
            }
            string dbupdateSQL = anicfg.dbupdateSQL;
            dbupdateSQL = ReplaceSQLValue(dbupdateSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbupdateSQL, "update");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is not int dBResult)
            {
                throw new Exception();
            }
            c.DBResult = dBResult;
            c.FireDBOperationOK();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBUpdate(IDBAnimation c)
    {
        try
        {
            if (c.Ansyncupdate)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "Update" },
                    { "shape", c }
                };
                thread.Start(dictionary);
                return;
            }
            string dbupdateSQL = c.DbupdateSQL;
            dbupdateSQL = ReplaceSQLValue(dbupdateSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbupdateSQL, "update");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is not int dBResult)
            {
                throw new Exception("");
            }
            c.DBResult = dBResult;
            c.FireDBOperationOK();
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBDelete(IDBAnimation c, DBDeleteAnimation anicfg)
    {
        try
        {
            if (c.Ansyncselect)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "DBDeleteAnimation" },
                    { "shape", c },
                    { "anicfg", anicfg }
                };
                thread.Start(dictionary);
                return;
            }
            string dbdeleteSQL = anicfg.dbdeleteSQL;
            dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbdeleteSQL, "delete");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
            }
            else if (obj is int dBResult)
            {
                c.DBResult = dBResult;
                c.FireDBOperationOK();
            }
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private void ExecuteButtonDBDelete(IDBAnimation c)
    {
        try
        {
            if (c.Ansyncdelete)
            {
                Thread thread = new(DBAnsyncOperation);
                Dictionary<string, object> dictionary = new()
                {
                    { "operation", "Delete" },
                    { "shape", c }
                };
                thread.Start(dictionary);
                return;
            }
            string dbdeleteSQL = c.DbdeleteSQL;
            dbdeleteSQL = ReplaceSQLValue(dbdeleteSQL);
            SetInformationLabel("正在操作数据库...", visible: true);
            object obj = CallDBOperation(dbdeleteSQL, "delete");
            SetInformationLabel("", visible: false);
            if (obj is Exception)
            {
                SetInformationLabel("操作数据库错误:" + (obj as Exception).Message, visible: true);
                throw obj as Exception;
            }
            if (obj is int dBResult)
            {
                c.DBResult = dBResult;
                c.FireDBOperationOK();
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            c.FireDBOperationErr();
        }
    }

    private object MainControl_GetSystemItemEvent(string name)
    {
        switch (name)
        {
            case "IPAddress":
                return ipaddress;
            case "Port":
                return port;
            case "AbsoluteUrl":
                return absoluteUrl;
            case "ScriptEngine":
                {
                    ScriptEngine scriptEngine = new(ScriptLanguage.VBscript);
                    ResetScriptEngine(scriptEngine);
                    return scriptEngine;
                }
            case "MainControl":
                return this;
            default:
                return null;
        }
    }

    #region 脚本中的系统API

    public string NewGUID()
    {
        return Guid.NewGuid().ToString();
    }

    public object GetVarValue(string varName)
    {
        return GetValue(varName);
    }

    public void SetVarValue(string varName, object value)
    {
        SetValue(varName, value);
    }

    public int GetMouseLocationX()
    {
        return Mouse.X;
    }

    public int GetMouseLocationY()
    {
        return Mouse.Y;
    }

    public int GetMouseButton()
    {
        int num = 0;
        if ((Mouse.Button & MouseButtons.Left) == MouseButtons.Left)
        {
            num++;
        }
        if ((Mouse.Button & MouseButtons.Right) == MouseButtons.Right)
        {
            num += 2;
        }
        return num;
    }

    public string SetDbType(int i)
    {
        dbType = i switch
        {
            0 => DbConnType.MS_SQL_Server,
            1 => DbConnType.ODBC,
            _ => DbConnType.MS_SQL_Server,
        };
        return dbType.ToString();
    }

    public bool SetDBConn(string connstr)
    {
        DBConnStr = connstr;
        return true;
    }

    public int ExecuteSql(string SQLCommStr)
    {
        try
        {
            DbConnection dbConnection;
            DbCommand dbCommand;
            switch (dbType)
            {
                default:
                    dbConnection = new SqlConnection(DBConnStr);
                    dbCommand = new SqlCommand(SQLCommStr, (SqlConnection)dbConnection);
                    break;
                case DbConnType.ODBC:
                    dbConnection = new OdbcConnection(DBConnStr);
                    dbCommand = new OdbcCommand(SQLCommStr, (OdbcConnection)dbConnection);
                    break;
            }
            dbConnection.Open();
            int result = dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            return result;
        }
        catch
        {
            return -1;
        }
    }

    public object[,] ExecuteSqlReturnArray(string SQLCommStr)
    {
        try
        {
            SqlHelper sqlHelper = new();
            DataSet dataSet = sqlHelper.ExcuteDataSetSql(SQLCommStr, DBConnStr);
            object[,] array = new object[dataSet.Tables[0].Rows.Count + 1, dataSet.Tables[0].Columns.Count];
            for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
            {
                array[0, i] = dataSet.Tables[0].Columns[i].ColumnName;
            }
            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                for (int k = 0; k < dataSet.Tables[0].Columns.Count; k++)
                {
                    array[j + 1, k] = dataSet.Tables[0].Rows[j][k].ToString();
                }
            }
            return array;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public int DBSelect(string selectcommand)
    {
        try
        {
            DbDataAdapter dbDataAdapter;
            switch (dbType)
            {
                default:
                    {
                        DbConnection dbConnection = new SqlConnection(DBConnStr);
                        dbDataAdapter = new SqlDataAdapter(selectcommand, (SqlConnection)dbConnection);
                        break;
                    }
                case DbConnType.ODBC:
                    {
                        DbConnection dbConnection = new OdbcConnection(DBConnStr);
                        dbDataAdapter = new OdbcDataAdapter(selectcommand, (OdbcConnection)dbConnection);
                        break;
                    }
            }
            LastTable = new DataTable();
            return dbDataAdapter.Fill(LastTable);
        }
        catch
        {
            return -1;
        }
    }

    public object GetTableItemByIndex(int row, int column)
    {
        try
        {
            return LastTable.Rows[row][column];
        }
        catch
        {
            return null;
        }
    }

    public object GetTableItemByName(int row, string columnName)
    {
        try
        {
            return LastTable.Rows[row][columnName];
        }
        catch
        {
            return null;
        }
    }

    public string GetAppFullDir()
    {
        if (base.Parent is Form)
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        RegistryKey localMachine = Registry.LocalMachine;
        RegistryKey registryKey = localMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dhp.projectname);
        if (registryKey != null)
        {
            return (string)registryKey.GetValue("") + "\\Project\\Output\\";
        }
        return "http://" + dhp.ipaddress + ":" + dhp.port + "/";
    }

    public bool LoadExtern(string dllfile, string FullClassName)
    {
        try
        {
            if (dllfile.PadLeft(7) == "http://")
            {
                dllfile = dllfile.Replace("\\", "/");
            }
            Assembly assembly = Assembly.LoadFrom(dllfile);
            Type type = assembly.GetType(FullClassName);
            ExternClass = Activator.CreateInstance(type);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
            return false;
        }
    }

    public void Exit()
    {
        Process.GetCurrentProcess().CloseMainWindow();
    }

    public bool ExecWait(string FileName, string args)
    {
        try
        {
            Process.Start(FileName, args).WaitForExit();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public bool Exec(string FileName, string args)
    {
        try
        {
            Process.Start(FileName, args);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public object GetObject(string pageName, string groupName, int groupIndex)
    {
        if (pdc.Count == 0)
        {
            return null;
        }
        return pdc[pageName][groupName][groupIndex];
    }

    public bool SetPageVisible(string PageName, bool Visible)
    {
        try
        {
            if (!bStartProject)
            {
                bStartProject = true;
                foreach (string key in SO.Keys)
                {
                    DelayLoadPage(key);
                    dicAuthority.Add(key, SD[key].PageAuthority);
                }
            }
            if (cas != null && cas.bProjectStart)
            {
                if (!Visible)
                {
                    return true;
                }
                if (!cas.dicAuthority[strCurrentUser].ltSafeRegion.Contains(SD[PageName].PageAuthority) && SD[PageName].PageAuthority != "")
                {
                    MessageBox.Show("当前用户权限不够，不能够正确访问该页面！", "提示");
                    return true;
                }
            }
            if (Visible)
            {
                if (!SD.ContainsKey(PageName))
                {
                    DelayLoadPage(PageName);
                    if (!Dic_IsPageRunLogic.ContainsKey(PageName))
                    {
                        Dic_IsPageRunLogic.Add(PageName, 0);
                    }
                }
                ((Control)SO[PageName]).Visible = Visible;
                ((Control)SO[PageName]).BringToFront();
                if (pageNeedResize.Contains(PageName))
                {
                    DataFile[] array = dfs.ToArray();
                    foreach (DataFile dataFile in array)
                    {
                        if (!(dataFile.name == PageName))
                        {
                            continue;
                        }
                        for (int j = 0; j < dataFile.ListAllShowCShape.Count; j++)
                        {
                            if (dataFile.ListAllShowCShape[j] is CControl)
                            {
                                try
                                {
                                    ((CControl)dataFile.ListAllShowCShape[j]).Width = dataFile.ListAllShowCShape[j].DefaultSize.Width * zoomX;
                                    ((CControl)dataFile.ListAllShowCShape[j]).Height = dataFile.ListAllShowCShape[j].DefaultSize.Height * zoomY;
                                    ((CControl)dataFile.ListAllShowCShape[j]).X = Convert.ToInt32((float)dataFile.ListAllShowCShape[j].DefaultLocaion.X * zoomX);
                                    ((CControl)dataFile.ListAllShowCShape[j]).Y = Convert.ToInt32((float)dataFile.ListAllShowCShape[j].DefaultLocaion.Y * zoomY);
                                    float num = ((CControl)dataFile.ListAllShowCShape[j]).DefaultFontSize * ((zoomX < zoomY) ? zoomX : zoomY);
                                    num = ((num < 8.25f) ? 8.25f : num);
                                    ((CControl)dataFile.ListAllShowCShape[j])._c.Font = new Font(((CControl)dataFile.ListAllShowCShape[j])._c.Font.Name, num);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    foreach (object value in SO.Values)
                    {
                        UserControl userControl = (UserControl)value;
                        if (userControl.Name == PageName)
                        {
                            userControl.Location = new Point(Convert.ToInt32(SD[userControl.Name].locationf.X * zoomX), Convert.ToInt32(SD[userControl.Name].locationf.Y * zoomY));
                            userControl.Size = new Size(Convert.ToInt32(SD[userControl.Name].sizef.Width * zoomX), Convert.ToInt32(SD[userControl.Name].sizef.Height * zoomY));
                            userControl.Invalidate();
                        }
                    }
                    pageNeedResize.Remove(PageName);
                }
            }
            else
            {
                ((Control)SO[PageName]).Visible = Visible;
            }
            return true;
        }
        catch
        {
            MessageBox.Show("设置页面打开或关闭出现异常！", "提示");
            return false;
        }
    }

    public bool FullScreen()
    {
        try
        {
            if (this.FullScreenEvent != null)
            {
                EventArgs e = new();
                this.FullScreenEvent(this, e);
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    #endregion

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.Transparent;
        this.label1.Location = new System.Drawing.Point(0, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(0, 12);
        this.label1.TabIndex = 0;
        this.label1.Visible = false;
        base.Controls.Add(this.label1);
        base.Name = "MainControl";
        base.Size = new System.Drawing.Size(1024, 768);
        base.Load += new System.EventHandler(MainControl_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
