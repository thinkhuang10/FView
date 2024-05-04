using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Guid("0D79CA3E-EA06-4f37-88F0-61C1502DC8DE")]
public class ClassForScript
{
    public string url = "";

    public int AlarmState;

    public object ExternClass;

    private DbConnType dbType;

    private string DBConnStr;

    private DataTable LastTable;

    public int DeviceID = -1;

    public int DeviceState = -1;

    public int KeyValue = -1;

    public MouseEventArgs Mouse = new(MouseButtons.Left, 0, 0, 0, 0);

    public static Dictionary<string, Dictionary<string, List<object>>> pdc = new();

    public static bool old = true;

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

    public object Eval(string str)
    {
        return null;
    }

    public object GetVarValue(string varName)
    {
        return 0;
    }

    public void SetVarValue(string varName, object value)
    {
    }

    public object DXPExecute(int lMethordType, object varValue, int lDeviceID, int lStartID, int lRWCount)
    {
        return null;
    }

    public string GetAppFullDir()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
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

    public string SetDbConnType(int i)
    {
        switch (i)
        {
            case 0:
                dbType = DbConnType.MS_SQL_Server;
                break;
            case 1:
                dbType = DbConnType.ODBC;
                break;
            default:
                dbType = DbConnType.MS_SQL_Server;
                break;
        }
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
        catch (Exception)
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
            for (int j = 1; j <= dataSet.Tables[0].Rows.Count; j++)
            {
                for (int k = 0; k < dataSet.Tables[0].Columns.Count; k++)
                {
                    array[j, k] = dataSet.Tables[0].Rows[j][k].ToString();
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
        catch (Exception)
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
        catch (Exception)
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
        catch (Exception)
        {
            return null;
        }
    }

    public void Exit()
    {
    }

    public bool SetPageVisible(string PageName, bool Visible)
    {
        try
        {
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool FullScreen()
    {
        try
        {
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ExecWait(string FileName, string args)
    {
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
        MakeGroup();
        if (pdc.Count == 0)
        {
            return null;
        }
        return pdc[pageName][groupName][groupIndex];
    }

    public bool PageSizeLocation(string PageName, float PageZoomX, float PageZoomY, int PageX, int PageY)
    {
        try
        {
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckAuthority(string userName, string safeRegion)
    {
        try
        {
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int GetPageY(string PageName)
    {
        return 0;
    }

    public int GetPageX(string PageName)
    {
        return 0;
    }

    public int GetPageWidth(string PageName)
    {
        return 0;
    }

    public int GetPageHeight(string PageName)
    {
        return 0;
    }

    public int AddRow(DataTable dt)
    {
        dt.Rows.Add();
        return dt.Rows.Count;
    }

    public int AddRowAt(DataTable dt, int index)
    {
        dt.Rows.InsertAt(dt.NewRow(), index);
        return dt.Rows.Count;
    }

    public int AddColumn(DataTable dt)
    {
        dt.Columns.Add();
        return dt.Columns.Count;
    }

    public bool SetColumnName(DataTable dt, int columnIndex, string name)
    {
        dt.Columns[columnIndex].ColumnName = name;
        return true;
    }

    public string GetColumnName(DataTable dt, int columnIndex)
    {
        return dt.Columns[columnIndex].ColumnName;
    }

    public int GetRowCount(DataTable dt)
    {
        return dt.Rows.Count;
    }

    public int GetColumnCount(DataTable dt)
    {
        return dt.Columns.Count;
    }

    public bool RemoveRowAt(DataTable dt, int i)
    {
        dt.Rows.RemoveAt(i);
        return true;
    }

    public bool RemoveColumnAt(DataTable dt, int i)
    {
        dt.Columns.RemoveAt(i);
        return true;
    }

    public object GetCellValue(DataTable dt, int i, int j)
    {
        return dt.Rows[i][j];
    }

    public bool SetCellValue(DataTable dt, int i, int j, object obj)
    {
        dt.Rows[i][j] = obj;
        return true;
    }

    public static void MakeGroup()
    {
        if (!old)
        {
            return;
        }
        pdc.Clear();
        pdc = new Dictionary<string, Dictionary<string, List<object>>>();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            Dictionary<string, List<object>> dictionary = new();
            foreach (CShape item in df.ListAllShowCShape)
            {
                if (item.groupName == null || item.groupName == "" || dictionary.ContainsKey(item.groupName))
                {
                    continue;
                }
                List<object> list = new();
                for (int i = 0; i < 1024; i++)
                {
                    list.Add(null);
                }
                for (int j = 0; j < df.ListAllShowCShape.Count; j++)
                {
                    if (df.ListAllShowCShape[j].groupName == item.groupName)
                    {
                        if (df.ListAllShowCShape[j] is CControl)
                        {
                            CControl cControl = new()
                            {
                                _dllfile = ((CControl)df.ListAllShowCShape[j])._dllfile,
                                type = ((CControl)df.ListAllShowCShape[j]).type
                            };
                            cControl.AddPoint(((CControl)df.ListAllShowCShape[j]).ImportantPoints[0]);
                            cControl.AddPoint(((CControl)df.ListAllShowCShape[j]).ImportantPoints[1]);
                            list[df.ListAllShowCShape[j].groupIndex] = cControl._c;
                        }
                        else
                        {
                            list[df.ListAllShowCShape[j].groupIndex] = df.ListAllShowCShape[j].Copy();
                        }
                    }
                }
                dictionary.Add(item.groupName, list);
            }
            pdc.Add(df.name, dictionary);
        }
        old = false;
    }
}
