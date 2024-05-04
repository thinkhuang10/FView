using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMIEditEnvironment.DBOperation;

public static class DBCtrl
{
    public static List<string> GetTables(string connstr, string typestr)
    {
        try
        {
            if (string.Compare(typestr, "MSSQLServer", ignoreCase: true) == 0)
            {
                return MSSQLServerGetTable(connstr);
            }
            if (string.Compare(typestr, "DataSet", ignoreCase: true) == 0)
            {
                return DataSetGetTable(connstr);
            }
            MessageBox.Show("未能正确解析数据库类型.");
            return new List<string>();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return new List<string>();
        }
    }

    private static List<string> DataSetGetTable(string connstr)
    {
        List<string> list = new();
        DataSet dataSet = new();
        dataSet.ReadXml(connstr);
        foreach (DataTable table in dataSet.Tables)
        {
            list.Add(table.TableName);
        }
        dataSet.Dispose();
        return list;
    }

    private static List<string> MSSQLServerGetTable(string connstr)
    {
        List<string> list = new();
        using SqlConnection sqlConnection = new(connstr);
        SqlCommand sqlCommand = new("select name from sysobjects where xtype = 'u' order by name", sqlConnection);
        sqlConnection.Open();
        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            while (sqlDataReader.Read())
            {
                list.Add(sqlDataReader.GetString(0));
            }
        }
        sqlConnection.Close();
        return list;
    }

    public static DataTable MSSQLServerGetColumns(string tablename, string connstr)
    {
        SqlConnection selectConnection = new(connstr);
        string selectCommandText = "select * from syscolumns where id=object_id('" + tablename + "')";
        SqlDataAdapter sqlDataAdapter = new(selectCommandText, selectConnection);
        DataTable dataTable = new();
        sqlDataAdapter.Fill(dataTable);
        return dataTable;
    }
}
