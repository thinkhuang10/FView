using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using CommonSnappableTypes;
using Microsoft.Win32;
using SevenZip.Compression;

namespace ShapeRuntime;

public class Operation
{
    public static Dictionary<string, ServerLogicRequest> ServerLogicDict = new();

    public static bool bEditEnvironment = false;

    public static LZMACoder coder = new();

    public static List<CIOItem> LIO;

    public static byte[] CompressStream(byte[] input)
    {
        byte[] result = null;
        try
        {
            using MemoryStream memoryStream = new(input);
            using (MemoryStream memoryStream2 = coder.compress(memoryStream))
            {
                result = memoryStream2.ToArray();
                memoryStream2.Flush();
                memoryStream2.Close();
                memoryStream2.Dispose();
            }
            memoryStream.Flush();
            memoryStream.Close();
            memoryStream.Dispose();
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static byte[] UncompressStream(byte[] input)
    {
        byte[] result = null;
        try
        {
            using MemoryStream memoryStream = new(input);
            using (MemoryStream memoryStream2 = coder.decompress(memoryStream))
            {
                result = memoryStream2.ToArray();
                memoryStream2.Flush();
                memoryStream2.Close();
                memoryStream2.Dispose();
            }
            memoryStream.Flush();
            memoryStream.Close();
            memoryStream.Dispose();
            return result;
        }
        catch (Exception)
        {
            return input;
        }
    }

    public static bool BinarySaveFile(string FileName, DataFile df)
    {
        try
        {
            FileInfo fileInfo = new(FileName);
            if (fileInfo.Exists)
            {
                fileInfo.CopyTo(fileInfo.FullName + ".bak", overwrite: true);
            }
            foreach (CShape item in df.ListAllShowCShape)
            {
                if (item is CControl)
                {
                    ((CControl)item).DllCopyTo(fileInfo.DirectoryName + "\\");
                }
                else if (item is CPixieControl)
                {
                    ((CPixieControl)item).DllCopyTo(fileInfo.DirectoryName + "\\");
                }
                else if (item is CGraphicsPath)
                {
                    ((CGraphicsPath)item).DllCopyTo(fileInfo.DirectoryName + "\\");
                }
            }
            foreach (CShape item2 in df.ListAllShowCShape)
            {
                if (item2 is CPixieControl)
                {
                    ((CPixieControl)item2).backupEvent();
                    item2.BeforeSaveMe();
                }
                else
                {
                    item2.BeforeSaveMe();
                }
            }
            if (df.pageName == null || df.pageName == "")
            {
                df.pageName = df.name;
            }
            Image pageimage = df.pageimage;
            df.pageimage = null;
            df.tls = df.ListAllShowCShape.ToArray();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, df);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
                df.pageimage = pageimage;
            }
            catch (Exception innerException2)
            {
                try
                {
                    if (File.Exists(fileInfo.FullName + ".bak"))
                    {
                        File.Copy(fileInfo.FullName + ".bak", fileInfo.FullName, overwrite: true);
                    }
                }
                catch (Exception innerException)
                {
                    throw new Exception("尝试恢复文件" + fileInfo.FullName + "失败.", innerException);
                }
                throw new Exception("序列化" + fileInfo.FullName + "失败.", innerException2);
            }
            foreach (CShape item3 in df.ListAllShowCShape)
            {
                item3.AfterSaveMe();
                if (item3 is CPixieControl)
                {
                    ((CPixieControl)item3).resumeevent();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
    }

    public static DataFile BinaryLoadFile(string FileName)
    {
        try
        {
            BinaryFormatter binaryFormatter = new();
            DataFile dataFile;
            using (Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                DateTime now = DateTime.Now;
                dataFile = (DataFile)binaryFormatter.UnsafeDeserialize(stream, null);
                Console.WriteLine((DateTime.Now - now).ToString() + "\t" + FileName);
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
            dataFile.ListAllShowCShape = new List<CShape>(dataFile.tls);
            if (dataFile.pageName == null || dataFile.pageName == "")
            {
                dataFile.pageName = dataFile.name;
            }
            if (dataFile.pageimage != null && dataFile.pageImageNamef == null)
            {
                dataFile.pageImageNamef = DHMIImageManage.SaveImage(dataFile.pageimage);
            }
            dataFile.pageimage = DHMIImageManage.LoadImage(dataFile.pageImageNamef);
            _ = dataFile.ListAllShowCShape;
            dataFile.sizef = dataFile.size;
            dataFile.locationf = dataFile.location;
            CShape[] array = dataFile.ListAllShowCShape.ToArray();
            foreach (CShape cShape in array)
            {
                try
                {
                    cShape.AfterLoadMe();
                    if (cShape is CControl && ((CControl)cShape)._c == null)
                    {
                        dataFile.ListAllShowCShape.Remove(cShape);
                    }
                    if (cShape.Layer > CShape.SumLayer)
                    {
                        CShape.SumLayer = cShape.Layer;
                    }
                    if (cShape.ImportantPoints.Length == 0)
                    {
                        dataFile.ListAllShowCShape.Remove(cShape);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + "ShapeRuntime=>Operation=>BinaryLoadFile=>Deserialize()");
                    dataFile.ListAllShowCShape.Remove(cShape);
                }
            }
            foreach (CShape item in dataFile.ListAllShowCShape)
            {
                if (item.ysdzshijianLogic == null)
                {
                    continue;
                }
                string[] array2 = new string[item.ysdzshijianLogic.Length];
                for (int j = 0; j < item.ysdzshijianLogic.Length; j++)
                {
                    if (item.ysdzshijianmingcheng != null && item.ysdzshijianmingcheng[j] != null)
                    {
                        array2[j] = item.ysdzshijianmingcheng[j];
                    }
                    else
                    {
                        array2[j] = "";
                    }
                }
                item.ysdzshijianmingcheng = array2;
            }
            return dataFile;
        }
        catch (Exception ex2)
        {
            Trace.WriteLine(ex2.Message + Environment.NewLine + ex2.Source + Environment.NewLine + "ShapeRuntime=>Operation=>BinaryLoadFile=>st.AfterLoadMe()");
            throw ex2;
        }
    }

    public static DataFile BinaryLoadFile(Stream stream)
    {
        _ = DateTime.Now;
        BinaryFormatter binaryFormatter = new();
        DataFile dataFile = (DataFile)binaryFormatter.UnsafeDeserialize(stream, null);
        stream.Close();
        dataFile.ListAllShowCShape = new List<CShape>(dataFile.tls);
        if (dataFile.pageName == null || dataFile.pageName == "")
        {
            dataFile.pageName = dataFile.name;
        }
        if (dataFile.pageimage != null && dataFile.pageImageNamef == null)
        {
            dataFile.pageImageNamef = DHMIImageManage.SaveImage(dataFile.pageimage);
        }
        dataFile.pageimage = DHMIImageManage.LoadImage(dataFile.pageImageNamef);
        _ = dataFile.ListAllShowCShape;
        dataFile.sizef = dataFile.size;
        dataFile.locationf = dataFile.location;
        CShape[] array = dataFile.ListAllShowCShape.ToArray();
        foreach (CShape cShape in array)
        {
            try
            {
                cShape.AfterLoadMe();
                if (cShape is CControl && ((CControl)cShape)._c == null)
                {
                    dataFile.ListAllShowCShape.Remove(cShape);
                }
                if (cShape.Layer > CShape.SumLayer)
                {
                    CShape.SumLayer = cShape.Layer;
                }
            }
            catch (Exception)
            {
                dataFile.ListAllShowCShape.Remove(cShape);
            }
        }
        return dataFile;
    }

    public static List<CShape> BinaryLoadShapes(string FileName)
    {
        try
        {
            BinaryFormatter binaryFormatter = new();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            DataFile dataFile = (DataFile)binaryFormatter.UnsafeDeserialize(stream, null);
            stream.Close();
            dataFile.ListAllShowCShape = new List<CShape>(dataFile.tls);
            List<CShape> listAllShowCShape = dataFile.ListAllShowCShape;
            for (int i = 0; i < dataFile.ListAllShowCShape.Count; i++)
            {
                CShape cShape = dataFile.ListAllShowCShape[i];
                cShape.AfterLoadMe();
                if (cShape is CControl && ((CControl)cShape)._c == null)
                {
                    dataFile.ListAllShowCShape.Remove(cShape);
                }
            }
            return listAllShowCShape;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void DrawShapes(List<CShape> LS, Graphics g)
    {
        foreach (CShape L in LS)
        {
            L.DrawMe(g, L.WillDrawRectLine);
        }
    }

    public static void AutoAddControlsToFormOrControl(List<CShape> LS, Form.ControlCollection CC)
    {
        List<Control> list = new();
        CShape[] array = LS.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape.GetType() != typeof(CControl))
            {
                continue;
            }
            list.Add(((CControl)cShape)._c);
            ((CControl)cShape)._c.Enabled = true;
            try
            {
                if (((CControl)cShape)._c is AxHost)
                {
                    ((ISupportInitialize)((CControl)cShape)._c).BeginInit();
                    CC.Add(((CControl)cShape)._c);
                    ((ISupportInitialize)((CControl)cShape)._c).EndInit();
                }
                else
                {
                    CC.Add(((CControl)cShape)._c);
                }
                ((CControl)cShape)._c.BringToFront();
            }
            catch (Exception)
            {
                CC.Remove(((CControl)cShape)._c);
                LS.Remove(cShape);
            }
        }
    }

    public static void AutoAddControlsToFormOrControl(List<CShape> LS, Control.ControlCollection CC)
    {
        List<Control> list = new();
        CShape[] array = LS.ToArray();
        foreach (CShape cShape in array)
        {
            if (cShape.GetType() != typeof(CControl))
            {
                continue;
            }
            list.Add(((CControl)cShape)._c);
            ((CControl)cShape)._c.Enabled = true;
            try
            {
                if (((CControl)cShape)._c is AxHost)
                {
                    ((ISupportInitialize)((CControl)cShape)._c).BeginInit();
                    CC.Add(((CControl)cShape)._c);
                    ((ISupportInitialize)((CControl)cShape)._c).EndInit();
                }
                else
                {
                    CC.Add(((CControl)cShape)._c);
                }
                ((CControl)cShape)._c.BringToFront();
            }
            catch (Exception)
            {
                CC.Remove(((CControl)cShape)._c);
                LS.Remove(cShape);
            }
        }
    }

    public static bool BinarySaveProject(string FileName, HMIProjectFile dhp)
    {
        try
        {
            FileInfo fileInfo = new(FileName);
            string text = fileInfo.DirectoryName + "\\HMI.dhp.xml";
            if (File.Exists(text))
            {
                File.Copy(text, text + ".bak", overwrite: true);
                File.Delete(text);
            }
            XmlSerializer xmlSerializer = new(typeof(HmiProject));
            StreamWriter streamWriter = new(text);
            xmlSerializer.Serialize(streamWriter, new HmiProject
            {
                PageGroup = dhp.PageGroup,
                PageFiles = dhp.PageFiles
            });
            streamWriter.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        try
        {
            DataTable dataTable = new("ProjectIO");
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Tag");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("ValType");
            foreach (ProjectIO projectIO in dhp.ProjectIOs)
            {
                if (projectIO.History)
                {
                    dataTable.Rows.Add(projectIO.ID, projectIO.name, projectIO.tag, projectIO.description, projectIO.type);
                }
            }
            FileInfo fileInfo2 = new(FileName);
            string text2 = fileInfo2.DirectoryName + "\\变量表.hvr";
            if (File.Exists(text2))
            {
                File.Copy(text2, text2 + ".bak", overwrite: true);
                File.Delete(text2);
            }
            while (true)
            {
                try
                {
                    dataTable.WriteXml(text2);
                }
                catch
                {
                    switch (MessageBox.Show("变量表文件写入失败，请关闭其他软件并重试，点击“取消”停止保存", "提示", MessageBoxButtons.RetryCancel))
                    {
                        case DialogResult.Retry:
                            if (File.Exists(text2))
                            {
                                File.Delete(text2);
                            }
                            continue;
                        case DialogResult.Cancel:
                            File.Copy(text2 + ".bak", text2, overwrite: true);
                            break;
                        case DialogResult.Abort:
                            break;
                    }
                }
                break;
            }
            if (dhp.devjiaoben == null)
            {
                dhp.devjiaoben = "";
            }
            if (dhp.devLogic == null)
            {
                dhp.devLogic = "";
            }
            dhp.NowProjectIOMaxID = ProjectIO.StaticID;
            dhp.NowShapeMaxID = CShape.MaxNo;
            dhp.SetupPath = AppDomain.CurrentDomain.BaseDirectory;
            dhp.EnvironmentPath = fileInfo2.DirectoryName;
            dhp.tprojectios = dhp.ProjectIOs.ToArray();
            dhp.tioalarms = dhp.IOAlarms.ToArray();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, dhp);
            stream.Close();
            stream.Dispose();
            return true;
        }
        catch (Exception ex2)
        {
            MessageBox.Show(ex2.Message);
            return false;
        }
    }

    public static HmiPageGroup LoadProjectGroups(string FileName)
    {
        HmiPageGroup hmiPageGroup = null;
        try
        {
            FileInfo fileInfo = new(FileName);
            string path = fileInfo.DirectoryName + "\\HMI.dhp.xml";
            if (File.Exists(path))
            {
                XmlSerializer xmlSerializer = new(typeof(HmiProject));
                StreamReader streamReader = new(path);
                hmiPageGroup = ((HmiProject)xmlSerializer.Deserialize(streamReader)).PageGroup;
                streamReader.Close();
            }
        }
        catch
        {
        }
        finally
        {
            FixHmiPageGroupParent(hmiPageGroup);
        }
        return hmiPageGroup;
    }

    private static void FixHmiPageGroupParent(HmiPageGroup pageGroup)
    {
        if (pageGroup == null)
        {
            return;
        }
        pageGroup.Children.ForEach(delegate (HmiAbstractPage item)
        {
            item.Parent = pageGroup;
            if (item is HmiPageGroup)
            {
                FixHmiPageGroupParent(item as HmiPageGroup);
            }
        });
    }

    public static HMIProjectFile BinaryLoadProject(string FileName)
    {
        FileInfo fileInfo = new(FileName);
        FileInfo[] files = fileInfo.Directory.GetFiles();
        foreach (FileInfo fileInfo2 in files)
        {
            if (!(fileInfo2.Extension.ToLower() == ".dll") && !(fileInfo2.Extension.ToLower() == ".ocx"))
            {
                continue;
            }
            if (fileInfo2.Name.Contains("ActiveX"))
            {
                try
                {
                    string name = fileInfo2.Name.Substring(fileInfo2.Name.IndexOf('.') + 1).Substring(0, 38);
                    RegistryKey classesRoot = Registry.ClassesRoot;
                    RegistryKey registryKey = classesRoot.OpenSubKey("CLSID");
                    RegistryKey registryKey2 = registryKey.OpenSubKey(name);
                    if (registryKey2 == null)
                    {
                        string text = fileInfo2.Name.Substring(fileInfo2.Name.Substring(0, fileInfo2.Name.LastIndexOf('.')).LastIndexOf('.') + 1);
                        File.Copy(fileInfo2.FullName, Environment.SystemDirectory + "\\" + text, overwrite: true);
                        Process.Start(Environment.SystemDirectory + "\\regsvr32.exe", "/s \"" + Environment.SystemDirectory + "\\" + text + "\"");
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    File.Copy(fileInfo2.FullName, AppDomain.CurrentDomain.BaseDirectory + "\\" + fileInfo2.Name, overwrite: false);
                }
                catch (Exception)
                {
                }
            }
        }
        BinaryFormatter binaryFormatter = new();
        HMIProjectFile hMIProjectFile;
        using (Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            hMIProjectFile = (HMIProjectFile)binaryFormatter.UnsafeDeserialize(stream, null);
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }
        _ = hMIProjectFile.ProjectBackColor;
        if (hMIProjectFile.ProjectBackColor == Color.Transparent)
        {
            hMIProjectFile.ProjectBackColor = SystemColors.Control;
        }
        ProjectIO.StaticID = hMIProjectFile.NowProjectIOMaxID;
        CShape.MaxNo = hMIProjectFile.NowShapeMaxID;
        CShape.SumLayer = 0L;
        hMIProjectFile.ProjectIOs = new List<ProjectIO>(hMIProjectFile.tprojectios);
        hMIProjectFile.IOAlarms = new List<CIOAlarm>(hMIProjectFile.tioalarms);
        if (hMIProjectFile.pages == null)
        {
            hMIProjectFile.pages = new Dictionary<string, string>();
            string[] pagefiles = hMIProjectFile.pagefiles;
            foreach (string value in pagefiles)
            {
                hMIProjectFile.pages.Add(Guid.NewGuid().ToString(), value);
            }
        }
        foreach (CIOAlarm iOAlarm in hMIProjectFile.IOAlarms)
        {
            if (iOAlarm.script == null)
            {
                iOAlarm.script = new string[12]
                {
                    "", "", "", "", "", "", "", "", "", "",
                    "", ""
                };
            }
            if (iOAlarm.boolAlarmScript == null)
            {
                iOAlarm.boolAlarmScript = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            }
            if (iOAlarm.script.Length != 12)
            {
                string[] array = new string[12];
                for (int k = 0; k < 6; k++)
                {
                    array[k] = iOAlarm.script[k];
                }
                iOAlarm.script = array;
            }
            if (iOAlarm.boolAlarmScript.Length != 10)
            {
                string[] array2 = new string[10];
                for (int l = 0; l < 5; l++)
                {
                    array2[l] = iOAlarm.boolAlarmScript[l];
                }
                iOAlarm.boolAlarmScript = array2;
            }
        }
        if (hMIProjectFile.exitpassword == null)
        {
            hMIProjectFile.exitpassword = "";
        }
        if (hMIProjectFile.dbCfgPara == null)
        {
            hMIProjectFile.dbCfgPara = new DBCfgPara("SqlClient Data Provider", "");
            if (hMIProjectFile.DBConnStr != null)
            {
                hMIProjectFile.dbCfgPara.DBConnStr = hMIProjectFile.DBConnStr;
            }
        }
        return hMIProjectFile;
    }

    public static HMIProjectFile BinaryLoadProject(Stream stream, string ipaddress, string port, string absoluteUrl)
    {
        IFormatter formatter = new BinaryFormatter();
        HMIProjectFile hMIProjectFile = (HMIProjectFile)formatter.Deserialize(stream);
        stream.Close();
        _ = hMIProjectFile.ProjectBackColor;
        if (hMIProjectFile.ProjectBackColor == Color.Transparent)
        {
            hMIProjectFile.ProjectBackColor = SystemColors.Control;
        }
        if (ipaddress != null && ipaddress != "")
        {
            hMIProjectFile.ipaddress = ipaddress;
        }
        if (port != null && port != "")
        {
            hMIProjectFile.port = port;
        }
        hMIProjectFile.ProjectIOs = new List<ProjectIO>(hMIProjectFile.tprojectios);
        hMIProjectFile.IOAlarms = new List<CIOAlarm>(hMIProjectFile.tioalarms);
        if (hMIProjectFile.InvalidateTime == 0)
        {
            hMIProjectFile.InvalidateTime = 100;
        }
        foreach (CIOAlarm iOAlarm in hMIProjectFile.IOAlarms)
        {
            if (iOAlarm.script == null)
            {
                iOAlarm.script = new string[12]
                {
                    "", "", "", "", "", "", "", "", "", "",
                    "", ""
                };
            }
            if (iOAlarm.boolAlarmScript == null)
            {
                iOAlarm.boolAlarmScript = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            }
            if (iOAlarm.script.Length != 12)
            {
                string[] array = new string[12];
                for (int i = 0; i < 6; i++)
                {
                    array[i] = iOAlarm.script[i];
                }
                iOAlarm.script = array;
            }
            if (iOAlarm.boolAlarmScript.Length != 10)
            {
                string[] array2 = new string[10];
                for (int j = 0; j < 5; j++)
                {
                    array2[j] = iOAlarm.boolAlarmScript[j];
                }
                iOAlarm.boolAlarmScript = array2;
            }
        }
        if (hMIProjectFile.exitpassword == null)
        {
            hMIProjectFile.exitpassword = "";
        }
        return hMIProjectFile;
    }

    public static DataTable GetIOTable(string FileName)
    {
        DataSet dataSet = new();
        dataSet.ReadXml(FileName);
        return dataSet.Tables["Item"];
    }

    public static DataTable GetIOTable(Stream stream)
    {
        DataSet dataSet = new();
        dataSet.ReadXml(stream);
        return dataSet.Tables["Item"];
    }

    public static bool ReplaceIOName(List<CShape> LS, CIOItem cioroot, List<ProjectIO> LPIO, List<ParaIO> LPARAIO)
    {
        List<CIOItem> list = new();
        ScanIO(list, cioroot);
        foreach (CShape L in LS)
        {
            if (L.ysdzshijianbiaodashi != null)
            {
                L.ysdzshijianLogic = (string[])L.ysdzshijianbiaodashi.Clone();
                foreach (CIOItem item in list)
                {
                    for (int i = 0; i < L.ysdzshijianLogic.Length; i++)
                    {
                        if (L.ysdzshijianLogic[i] != null)
                        {
                            L.ysdzshijianLogic[i] = L.ysdzshijianLogic[i].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                        }
                    }
                }
                foreach (ProjectIO item2 in LPIO)
                {
                    for (int j = 0; j < L.ysdzshijianLogic.Length; j++)
                    {
                        if (L.ysdzshijianLogic[j] != null)
                        {
                            L.ysdzshijianLogic[j] = L.ysdzshijianLogic[j].Replace("[" + item2.name + "]", "Globle." + item2.name);
                        }
                    }
                }
                foreach (ParaIO item3 in LPARAIO)
                {
                    for (int k = 0; k < L.ysdzshijianLogic.Length; k++)
                    {
                        if (L.ysdzshijianLogic[k] != null)
                        {
                            L.ysdzshijianLogic[k] = L.ysdzshijianLogic[k].Replace("[" + item3.name + "]", "Globle.ParaIO" + item3.ID);
                        }
                    }
                }
            }
            if (L.Logic != null)
            {
                L.Logic = (string[])L.UserLogic.Clone();
                foreach (CIOItem item4 in list)
                {
                    for (int l = 0; l < L.Logic.Length; l++)
                    {
                        if (L.Logic[l] != null)
                        {
                            L.Logic[l] = L.Logic[l].Replace("[" + item4.name + "]", "Globle.var" + item4.MsgID);
                        }
                    }
                }
                foreach (ProjectIO item5 in LPIO)
                {
                    for (int m = 0; m < L.Logic.Length; m++)
                    {
                        if (L.Logic[m] != null)
                        {
                            L.Logic[m] = L.Logic[m].Replace("[" + item5.name + "]", "Globle." + item5.name);
                        }
                    }
                }
                foreach (ParaIO item6 in LPARAIO)
                {
                    for (int n = 0; n < L.Logic.Length; n++)
                    {
                        if (L.Logic[n] != null)
                        {
                            L.Logic[n] = L.Logic[n].Replace("[" + item6.name + "]", "Globle.ParaIO" + item6.ID);
                        }
                    }
                }
            }
            foreach (CIOItem item7 in list)
            {
                if (L.DBOKScript != null && L.DBOKScript != "")
                {
                    L.DBOKScript = L.DBOKScript.Replace("[" + item7.name + "]", "Globle.var" + item7.MsgID);
                }
                if (L.DBErrScript != null && L.DBErrScript != "")
                {
                    L.DBErrScript = L.DBErrScript.Replace("[" + item7.name + "]", "Globle.var" + item7.MsgID);
                }
            }
            foreach (ProjectIO item8 in LPIO)
            {
                if (L.DBOKScript != null && L.DBOKScript != "")
                {
                    L.DBOKScript = L.DBOKScript.Replace("[" + item8.name + "]", "Globle." + item8.name);
                }
                if (L.DBErrScript != null && L.DBErrScript != "")
                {
                    L.DBErrScript = L.DBErrScript.Replace("[" + item8.name + "]", "Globle." + item8.name);
                }
            }
            foreach (ParaIO item9 in LPARAIO)
            {
                if (L.DBOKScript != null && L.DBOKScript != "")
                {
                    L.DBOKScript = L.DBOKScript.Replace("[" + item9.name + "]", "Globle.ParaIO" + item9.ID);
                }
                if (L.DBErrScript != null && L.DBErrScript != "")
                {
                    L.DBErrScript = L.DBErrScript.Replace("[" + item9.name + "]", "Globle.ParaIO" + item9.ID);
                }
            }
        }
        return true;
    }

    public static void ScanIO(List<CIOItem> LIO, CIOItem root)
    {
        foreach (CIOItem item in root.subitem)
        {
            if (item.IsLeaf)
            {
                LIO.Add(item);
            }
            ScanIO(LIO, item);
        }
    }

    public static void ReplaceIO(CIOAlarm ioa, CIOItem cioroot, List<ProjectIO> LPIO, List<ParaIO> LPARAIO)
    {
        for (int i = 0; i < ioa.boolAlarmScript.Length / 2; i++)
        {
            ioa.boolAlarmScript[i] = ioa.boolAlarmScript[i + 5];
        }
        for (int j = 0; j < ioa.script.Length / 2; j++)
        {
            ioa.script[j] = ioa.script[j + 6];
        }
        if (LIO == null)
        {
            LIO = new List<CIOItem>();
            ScanIO(LIO, cioroot);
        }
        foreach (CIOItem item in LIO)
        {
            if (ioa.name == "[" + item.name + "]")
            {
                ioa.MsgID = item.MsgID;
            }
            for (int k = 0; k < 6; k++)
            {
                if (ioa.script[k] != null)
                {
                    ioa.script[k] = ioa.script[k].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                }
            }
            for (int l = 0; l < 5; l++)
            {
                if (ioa.boolAlarmScript[l] != null)
                {
                    ioa.boolAlarmScript[l] = ioa.boolAlarmScript[l].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                }
            }
        }
        foreach (ProjectIO item2 in LPIO)
        {
            for (int m = 0; m < 6; m++)
            {
                if (ioa.script[m] != null)
                {
                    ioa.script[m] = ioa.script[m].Replace("[" + item2.name + "]", "Globle." + item2.name);
                }
            }
            for (int n = 0; n < 5; n++)
            {
                if (ioa.boolAlarmScript[n] != null)
                {
                    ioa.boolAlarmScript[n] = ioa.boolAlarmScript[n].Replace("[" + item2.name + "]", "Globle." + item2.name);
                }
            }
        }
        foreach (ParaIO item3 in LPARAIO)
        {
            for (int num = 0; num < 6; num++)
            {
                if (ioa.script[num] != null)
                {
                    ioa.script[num] = ioa.script[num].Replace("[" + item3.name + "]", "Globle.ParaIO" + item3.ID);
                }
            }
            for (int num2 = 0; num2 < 5; num2++)
            {
                if (ioa.boolAlarmScript[num2] != null)
                {
                    ioa.boolAlarmScript[num2] = ioa.boolAlarmScript[num2].Replace("[" + item3.name + "]", "Globle.ParaIO" + item3.ID);
                }
            }
        }
    }

    public static string ReplaceIO(string str, CIOItem cioroot, List<ProjectIO> LPIO, List<ParaIO> LPARAIO)
    {
        if (str == null)
        {
            return "";
        }
        if (str == "")
        {
            return "";
        }
        if (LIO == null)
        {
            LIO = new List<CIOItem>();
            ScanIO(LIO, cioroot);
        }
        StringBuilder stringBuilder = new(str);
        foreach (CIOItem item in LIO)
        {
            if (str != null)
            {
                stringBuilder = stringBuilder.Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
            }
        }
        foreach (ProjectIO item2 in LPIO)
        {
            if (str != null)
            {
                stringBuilder = stringBuilder.Replace("[" + item2.name + "]", "Globle." + item2.name);
            }
        }
        foreach (ParaIO item3 in LPARAIO)
        {
            if (str != null)
            {
                stringBuilder = stringBuilder.Replace("[" + item3.name + "]", "Globle.ParaIO" + item3.ID);
            }
        }
        return stringBuilder.ToString();
    }

    public static bool ReplaceIOName(List<CShape> LS, CIOItem cioroot, List<ProjectIO> LPIO)
    {
        List<CIOItem> list = new();
        ScanIO(list, cioroot);
        foreach (CShape L in LS)
        {
            if (L.ysdzshijianbiaodashi != null)
            {
                L.ysdzshijianLogic = (string[])L.ysdzshijianbiaodashi.Clone();
                foreach (CIOItem item in list)
                {
                    for (int i = 0; i < L.ysdzshijianLogic.Length; i++)
                    {
                        if (L.ysdzshijianLogic[i] != null)
                        {
                            L.ysdzshijianLogic[i] = L.ysdzshijianLogic[i].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                        }
                    }
                }
                foreach (ProjectIO item2 in LPIO)
                {
                    for (int j = 0; j < L.ysdzshijianLogic.Length; j++)
                    {
                        if (L.ysdzshijianLogic[j] != null)
                        {
                            L.ysdzshijianLogic[j] = L.ysdzshijianLogic[j].Replace("[" + item2.name + "]", "Globle." + item2.name);
                        }
                    }
                }
            }
            if (L.Logic != null)
            {
                L.Logic = (string[])L.UserLogic.Clone();
                foreach (CIOItem item3 in list)
                {
                    for (int k = 0; k < L.Logic.Length; k++)
                    {
                        if (L.Logic[k] != null)
                        {
                            L.Logic[k] = L.Logic[k].Replace("[" + item3.name + "]", "Globle.var" + item3.MsgID);
                        }
                    }
                }
                foreach (ProjectIO item4 in LPIO)
                {
                    for (int l = 0; l < L.Logic.Length; l++)
                    {
                        if (L.Logic[l] != null)
                        {
                            L.Logic[l] = L.Logic[l].Replace("[" + item4.name + "]", "Globle." + item4.name);
                        }
                    }
                }
            }
            foreach (CIOItem item5 in list)
            {
                if (L.DBOKScript != null && L.DBOKScript != "")
                {
                    L.DBOKScript = L.DBOKScript.Replace("[" + item5.name + "]", "Globle.var" + item5.MsgID);
                }
                if (L.DBErrScript != null && L.DBErrScript != "")
                {
                    L.DBErrScript = L.DBErrScript.Replace("[" + item5.name + "]", "Globle.var" + item5.MsgID);
                }
            }
            foreach (ProjectIO item6 in LPIO)
            {
                if (L.DBOKScript != null && L.DBOKScript != "")
                {
                    L.DBOKScript = L.DBOKScript.Replace("[" + item6.name + "]", "Globle." + item6.name);
                }
                if (L.DBErrScript != null && L.DBErrScript != "")
                {
                    L.DBErrScript = L.DBErrScript.Replace("[" + item6.name + "]", "Globle." + item6.name);
                }
            }
        }
        return true;
    }

    public static void ReplaceIO(CIOAlarm ioa, CIOItem cioroot, List<ProjectIO> LPIO)
    {
        if (LIO == null)
        {
            LIO = new List<CIOItem>();
            ScanIO(LIO, cioroot);
        }
        foreach (CIOItem item in LIO)
        {
            if (ioa.name == "[" + item.name + "]")
            {
                ioa.MsgID = item.MsgID;
            }
            for (int i = 0; i < 6; i++)
            {
                if (ioa.script[i + 6] != null)
                {
                    ioa.script[i] = ioa.script[i + 6].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                }
            }
            for (int j = 0; j < 5; j++)
            {
                if (ioa.boolAlarmScript[j + 5] != null)
                {
                    ioa.boolAlarmScript[j] = ioa.boolAlarmScript[j + 5].Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
                }
            }
        }
        foreach (ProjectIO item2 in LPIO)
        {
            for (int k = 0; k < 6; k++)
            {
                if (ioa.script[k + 6] != null)
                {
                    ioa.script[k] = ioa.script[k + 6].Replace("[" + item2.name + "]", "Globle." + item2.name);
                }
            }
            for (int l = 0; l < 5; l++)
            {
                if (ioa.boolAlarmScript[l + 5] != null)
                {
                    ioa.boolAlarmScript[l] = ioa.boolAlarmScript[l + 5].Replace("[" + item2.name + "]", "Globle." + item2.name);
                }
            }
        }
    }

    public static string ReplaceIO(string str, CIOItem cioroot, List<ProjectIO> LPIO)
    {
        if (str == null)
        {
            return "";
        }
        if (str == "")
        {
            return "";
        }
        if (LIO == null)
        {
            LIO = new List<CIOItem>();
            ScanIO(LIO, cioroot);
        }
        StringBuilder stringBuilder = new(str);
        foreach (CIOItem item in LIO)
        {
            if (str != null)
            {
                stringBuilder = stringBuilder.Replace("[" + item.name + "]", "Globle.var" + item.MsgID);
            }
        }
        foreach (ProjectIO item2 in LPIO)
        {
            if (str != null)
            {
                stringBuilder = stringBuilder.Replace("[" + item2.name + "]", "Globle." + item2.name);
            }
        }
        return stringBuilder.ToString();
    }
}
