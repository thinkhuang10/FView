using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AxDCCECLIENTLib;

namespace HMIClient;

public class Client : UserControl
{
    public delegate void OnDeviceAlarm(object sender, DeviceAlarmEventArgs e);

    public delegate void OnVariableAlarm(object sender, VariableAlarmEventArgs e);

    private readonly byte[] recivebuffer = new byte[16384];

    private readonly ManualResetEvent _sendevent = new(initialState: false);

    private readonly ManualResetEvent _revcontrol = new(initialState: false);

    private readonly Queue<Query> _querys = new();

    private readonly BinaryFormatter bf = new();

    private readonly object clientlock = new();

    private readonly object clientlock1 = new();

    public Label MsgLabel;

    private readonly Dictionary<int, object> paraDict;

    private bool dataReady;

    private readonly int[] DeviceStatus = new int[256];

    private readonly DataTable RealtimeAlarm = new("RealtimeAlarm");

    private readonly DataTable HistoryAlarm = new("HistoryAlarm");

    private object[] vars;

    private string ip = "127.0.0.1";

    private int port;

    public AxDCCEClient axDCCEClient1;

    public bool DataReady => dataReady;

    public object[] Vars
    {
        get
        {
            return vars;
        }
        set
        {
            vars = value;
        }
    }

    public string ServerIp
    {
        get
        {
            return ip;
        }
        set
        {
            ip = value;
        }
    }

    public int Port
    {
        get
        {
            return port;
        }
        set
        {
            port = value;
        }
    }

    public event OnDeviceAlarm DeviceAlarmEvent;

    public event OnVariableAlarm VariableAlarmEvent;

    public event EventHandler VariableAlarmConfirm;

    public event EventHandler VariForAlarm;

    [DllImport("User32.dll")]
    private static extern int FindWindow([MarshalAs(UnmanagedType.LPStr)] string lpClassName, [MarshalAs(UnmanagedType.LPStr)] string lpWindowName);

    public Client(object[] vars, Dictionary<int, object> paradict)
    {
        InitializeComponent();
        HistoryAlarm.Columns.Add("报警ID", typeof(string));
        HistoryAlarm.Columns.Add("报警时间", typeof(string));
        HistoryAlarm.Columns.Add("报警变量", typeof(string));
        HistoryAlarm.Columns.Add("报警值", typeof(string));
        HistoryAlarm.Columns.Add("报警类型", typeof(string));
        HistoryAlarm.Columns.Add("确认时间", typeof(string));
        HistoryAlarm.Columns.Add("确认人", typeof(string));
        try
        {
            FileInfo fileInfo = new("c:\\HistoryAlert.xml");
            if (fileInfo.Exists)
            {
                XmlDocument xmlDocument = new();
                xmlDocument.Load("c:\\HistoryAlert.xml");
                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//AlertInfor");
                if (xmlNodeList.Count != 0)
                {
                    HistoryAlarm.Rows.Clear();
                    foreach (XmlNode item in xmlNodeList)
                    {
                        DataRow dataRow = HistoryAlarm.NewRow();
                        dataRow["报警ID"] = ((XmlElement)item).GetAttribute("ID");
                        dataRow["报警时间"] = ((XmlElement)item).GetAttribute("AlarmTime");
                        dataRow["报警变量"] = ((XmlElement)item).GetAttribute("AlarmVar");
                        dataRow["报警值"] = ((XmlElement)item).GetAttribute("VarValue");
                        dataRow["报警类型"] = ((XmlElement)item).GetAttribute("AlarmType");
                        dataRow["确认时间"] = ((XmlElement)item).GetAttribute("ConfirmTime");
                        dataRow["确认人"] = ((XmlElement)item).GetAttribute("ConfirmUser");
                        HistoryAlarm.Rows.InsertAt(dataRow, 0);
                    }
                }
            }
            else
            {
                FileStream stream = new("c:\\HistoryAlert.xml", FileMode.OpenOrCreate);
                StreamWriter streamWriter = new(stream);
                streamWriter.WriteLine("<?xml version='1.0' encoding='gb2312'?>");
                streamWriter.WriteLine("<XMLRoot>");
                streamWriter.WriteLine("</XMLRoot>");
                streamWriter.Close();
            }
        }
        catch
        {
        }
        RealtimeAlarm.Columns.Add("ID", typeof(string));
        RealtimeAlarm.Columns.Add("AlarmTime", typeof(string));
        RealtimeAlarm.Columns.Add("AlarmVar", typeof(string));
        RealtimeAlarm.Columns.Add("VarValue", typeof(string));
        RealtimeAlarm.Columns.Add("AlarmType", typeof(string));
        RealtimeAlarm.Columns.Add("ConfirmTime", typeof(string));
        RealtimeAlarm.Columns.Add("ConfirmUser", typeof(string));
        for (int i = 0; i < 256; i++)
        {
            DeviceStatus[i] = -2;
        }
        Vars = vars;
        paraDict = paradict;
        axDCCEClient1.FireOnBehavior += axDCCEClient1_FireOnBehavior;
        axDCCEClient1.FireOnDataReady += axDCCEClient1_FireOnDataReady;
        axDCCEClient1.DataInit(50);
    }

    private void axDCCEClient1_FireOnDataReady(object sender, _DDCCEClientEvents_FireOnDataReadyEvent e)
    {
        dataReady = true;
        axDCCEClient1.FireOnVariableAlarm += axDCCEClient1_FireOnVariableAlarm;
        axDCCEClient1.FireOnDeviceStatus += axDCCEClient1_FireOnDeviceStatus;
        axDCCEClient1.Execute(6, 0, 0, 0, 0);
    }

    private void axDCCEClient1_FireOnDeviceStatus(object sender, _DDCCEClientEvents_FireOnDeviceStatusEvent e)
    {
        if (DeviceStatus[e.lDeviceID] != e.lDeviceStatus)
        {
            DeviceStatus[e.lDeviceID] = e.lDeviceStatus;
            this.DeviceAlarmEvent(sender, new DeviceAlarmEventArgs(e.lDeviceID, e.lDeviceStatus));
        }
    }

    public DataTable GetHistoryAlert(string start, string end)
    {
        try
        {
            if (HistoryAlarm != null)
            {
                DataView defaultView = HistoryAlarm.DefaultView;
                string rowFilter = "(报警时间 <='" + end.ToString() + "'and 报警时间>='" + start.ToString() + "')or(确认时间<='" + end.ToString() + "'and 确认时间>='" + start.ToString() + "')";
                defaultView.RowFilter = rowFilter;
                return defaultView.ToTable();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public void ConfirmAlarm(string id, string confirmTime, string confirmUser)
    {
        try
        {
            foreach (DataRow row in RealtimeAlarm.Rows)
            {
                if (row["ID"].ToString() == id)
                {
                    row["ConfirmTime"] = confirmTime;
                    row["ConfirmUser"] = confirmUser;
                    RealtimeAlarm.Rows.Remove(row);
                    break;
                }
            }
            foreach (DataRow row2 in HistoryAlarm.Rows)
            {
                if (!(row2["报警ID"].ToString() == id))
                {
                    continue;
                }
                row2["确认时间"] = confirmTime;
                row2["确认人"] = confirmUser;
                try
                {
                    FileInfo fileInfo = new("c:\\HistoryAlert.xml");
                    if (!fileInfo.Exists)
                    {
                        FileStream stream = new("c:\\HistoryAlert.xml", FileMode.OpenOrCreate);
                        StreamWriter streamWriter = new(stream);
                        streamWriter.WriteLine("<?xml version='1.0' encoding='gb2312'?>");
                        streamWriter.WriteLine("<XMLRoot>");
                        streamWriter.WriteLine("</XMLRoot>");
                        streamWriter.Close();
                    }
                    XmlDocument xmlDocument = new();
                    xmlDocument.Load("c:\\HistoryAlert.xml");
                    XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//AlertInfor");
                    if (xmlNodeList.Count == 0)
                    {
                        break;
                    }
                    foreach (XmlNode item in xmlNodeList)
                    {
                        if (((XmlElement)item).GetAttribute("ID").ToString() == id)
                        {
                            ((XmlElement)item).SetAttribute("ConfirmTime", confirmTime);
                        }
                    }
                    xmlDocument.Save("c:\\HistoryAlert.xml");
                }
                catch
                {
                }
                break;
            }
            VariableAlarmConfirmEventArgs e = new(id, confirmTime, confirmUser);
            if (this.VariableAlarmConfirm != null)
            {
                this.VariableAlarmConfirm(this, e);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("HMIClient_confirmAlarm错误" + ex.ToString());
        }
    }

    private void axDCCEClient1_FireOnVariableAlarm(object sender, _DDCCEClientEvents_FireOnVariableAlarmEvent e)
    {
        try
        {
            DataRow dataRow = RealtimeAlarm.NewRow();
            dataRow["ID"] = Guid.NewGuid().ToString();
            dataRow["AlarmTime"] = DateTime.Now;
            dataRow["AlarmVar"] = e.lID;
            dataRow["VarValue"] = axDCCEClient1.Read(e.lID);
            dataRow["AlarmType"] = e.lAlarmType;
            RealtimeAlarm.Rows.Add(dataRow);
            DataRow dataRow2 = HistoryAlarm.NewRow();
            dataRow2["报警ID"] = dataRow["ID"];
            dataRow2["报警时间"] = dataRow["AlarmTime"];
            dataRow2["报警变量"] = dataRow["AlarmVar"];
            dataRow2["报警值"] = dataRow["VarValue"];
            dataRow2["报警类型"] = dataRow["AlarmType"];
            HistoryAlarm.Rows.Add(dataRow2);
            try
            {
                FileInfo fileInfo = new("c:\\HistoryAlert.xml");
                if (!fileInfo.Exists)
                {
                    FileStream stream = new("c:\\HistoryAlert.xml", FileMode.OpenOrCreate);
                    StreamWriter streamWriter = new(stream);
                    streamWriter.WriteLine("<?xml version='1.0' encoding='gb2312'?>");
                    streamWriter.WriteLine("<XMLRoot>");
                    streamWriter.WriteLine("</XMLRoot>");
                    streamWriter.Close();
                }
                XmlDocument xmlDocument = new();
                xmlDocument.Load("c:\\HistoryAlert.xml");
                XmlNode xmlNode = xmlDocument.SelectSingleNode("XMLRoot");
                XmlElement xmlElement = xmlDocument.CreateElement("AlertInfor");
                xmlElement.SetAttribute("ID", dataRow["ID"].ToString());
                xmlElement.SetAttribute("AlarmTime", dataRow["AlarmTime"].ToString());
                xmlElement.SetAttribute("AlarmVar", dataRow["AlarmVar"].ToString());
                xmlElement.SetAttribute("VarValue", dataRow["VarValue"].ToString());
                xmlElement.SetAttribute("AlarmType", dataRow["AlarmType"].ToString());
                ((XmlElement)xmlNode).AppendChild(xmlElement);
                while (xmlNode.ChildNodes.Count > 1000)
                {
                    xmlNode.RemoveChild(xmlNode.ChildNodes[0]);
                }
                xmlDocument.Save("c:\\HistoryAlert.xml");
            }
            catch
            {
            }
            VarForAlarmEventArgs e2 = new(dataRow["ID"].ToString(), (string)dataRow["AlarmTime"], e.lID, dataRow["VarValue"].ToString(), e.lAlarmType);
            if (this.VariForAlarm != null)
            {
                this.VariForAlarm(this, e2);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("HMIClient报警数据错误！" + ex.ToString());
        }
        finally
        {
            VariableAlarmEventArgs e3 = new(e.lAlarmType, e.lID, e.pvarValue);
            this.VariableAlarmEvent(this, e3);
        }
    }

    public void setvalue(int ID, object val)
    {
        axDCCEClient1.Write(ID, val);
    }

    private void axDCCEClient1_FireOnBehavior(object sender, _DDCCEClientEvents_FireOnBehaviorEvent e)
    {
        object[] array = (object[])e.varValue;
        Dictionary<int, object> dictionary = new();
        for (int i = 0; i < (array.Length - 1) / 2; i++)
        {
            dictionary.Add(Convert.ToInt32(array[i * 2 + 1]), array[i * 2 + 2]);
        }
    }

    public object getvalue(int ID)
    {
        return axDCCEClient1.Read(ID);
    }

    public void getvalues(int startID, int endID)
    {
        object[] array = (object[])axDCCEClient1.ReadEx(startID, endID);
        array.CopyTo(vars, startID);
    }

    public void getvalues(int[] IDs)
    {
        if (IDs.Length != 0)
        {
            object[] array = (object[])axDCCEClient1.ReadEx(IDs[0], IDs[IDs.Length - 1]);
            array.CopyTo(vars, IDs[0]);
        }
    }

    public object Execute(int lMethordType, object varValue, int lDeviceID, int lStartID, int lRWCount)
    {
        try
        {
            return axDCCEClient1.Execute(lMethordType, varValue, lDeviceID, lStartID, lRWCount);
        }
        catch
        {
            return null;
        }
    }

    public void Stop()
    {
    }

    public void Start()
    {
    }


    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new(typeof(HMIClient.Client));
        this.axDCCEClient1 = new AxDCCECLIENTLib.AxDCCEClient();
        ((System.ComponentModel.ISupportInitialize)this.axDCCEClient1).BeginInit();
        base.SuspendLayout();
        this.axDCCEClient1.Enabled = true;
        this.axDCCEClient1.Location = new System.Drawing.Point(24, 27);
        this.axDCCEClient1.Name = "axDCCEClient1";
        this.axDCCEClient1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axDCCEClient1.OcxState");
        this.axDCCEClient1.Size = new System.Drawing.Size(100, 50);
        this.axDCCEClient1.TabIndex = 0;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.axDCCEClient1);
        base.Name = "Client";
        ((System.ComponentModel.ISupportInitialize)this.axDCCEClient1).EndInit();
        base.ResumeLayout(false);
    }
}
