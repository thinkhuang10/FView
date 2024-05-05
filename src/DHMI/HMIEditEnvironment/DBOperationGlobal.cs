using System;
using System.Data;
using System.Data.Common;

namespace HMIEditEnvironment;

internal static class DBOperationGlobal
{
    public static bool effect = true;

    public static string dbConnStr = "";

    public static string dbType = "";

    public static DbProviderFactory factory;

    public static DbConnection conn;

    public static DbCommand command;

    public static DbDataAdapter adapter;

    private static DataRow tagdr;

    public static void Refresh(string providerName, string dbConnString)
    {
        conn?.Dispose();
        command?.Dispose();
        adapter?.Dispose();
        DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
        tagdr = null;
        foreach (DataRow row in factoryClasses.Rows)
        {
            if (row[0].ToString() == providerName)
            {
                tagdr = row;
                dbType = providerName;
                break;
            }
        }
        if (tagdr == null)
        {
            throw new Exception("数据库连接错误:无法创建提供程序对数据源类的实现的实例");
        }
        factory = DbProviderFactories.GetFactory(tagdr);
        conn = factory.CreateConnection();
        conn.ConnectionString = dbConnString;
        dbConnStr = dbConnString;
        conn.Open();
        command = factory.CreateCommand();
        command.Connection = conn;
        adapter = factory.CreateDataAdapter();
        adapter.SelectCommand = command;
        effect = true;
    }
}
