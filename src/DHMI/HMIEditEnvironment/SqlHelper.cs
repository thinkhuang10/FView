using System.Data;
using System.Data.SqlClient;

namespace HMIEditEnvironment;

internal class SqlHelper
{
    public void ExcuteNonSql(string sql, string connection)
    {
        SqlConnection sqlConnection = new(connection);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new(sql, sqlConnection);
        sqlCommand.ExecuteNonQuery();
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
    }

    public void ExcuteNonSql(string sql, string connection, params SqlParameter[] parameters)
    {
        SqlConnection sqlConnection = new(connection);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new(sql, sqlConnection);
        foreach (SqlParameter value in parameters)
        {
            sqlCommand.Parameters.Add(value);
        }
        sqlCommand.ExecuteNonQuery();
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
    }

    public DataSet ExcuteDataSetSql(string sql, string connection)
    {
        SqlConnection sqlConnection = new(connection);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand selectCommand = new(sql, sqlConnection);
        SqlDataAdapter sqlDataAdapter = new(selectCommand);
        DataSet dataSet = new();
        sqlDataAdapter.Fill(dataSet);
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
        return dataSet;
    }

    public DataSet ExcuteDataSetSql(string sql, string connection, params SqlParameter[] parameters)
    {
        SqlConnection sqlConnection = new(connection);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new(sql, sqlConnection);
        foreach (SqlParameter value in parameters)
        {
            sqlCommand.Parameters.Add(value);
        }
        SqlDataAdapter sqlDataAdapter = new(sqlCommand);
        DataSet dataSet = new();
        sqlDataAdapter.Fill(dataSet);
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
        return dataSet;
    }

    public void isDataBaseExist(string dataBase, string conncetion)
    {
        SqlConnection sqlConnection = new(conncetion);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new("select * from sys.databases where name='" + dataBase + "'", sqlConnection);
        object obj = sqlCommand.ExecuteScalar();
        if (obj == null)
        {
            SqlCommand sqlCommand2 = new("create database " + dataBase, sqlConnection);
            sqlCommand2.ExecuteNonQuery();
        }
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
    }

    public bool bDataTableExist(string dataTable, string conncetion)
    {
        SqlConnection sqlConnection = new(conncetion);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new("select * from sysobjects where type='U' and name='" + dataTable + "'", sqlConnection);
        object obj = sqlCommand.ExecuteScalar();
        if (obj == null)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            return false;
        }
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
        return true;
    }

    public bool isDataTableExist(string dataTable, string columns, string conncetion)
    {
        SqlConnection sqlConnection = new(conncetion);
        if (sqlConnection.State == ConnectionState.Closed)
        {
            sqlConnection.Open();
        }
        SqlCommand sqlCommand = new("select * from sysobjects where type='U' and name='" + dataTable + "'", sqlConnection);
        object obj = sqlCommand.ExecuteScalar();
        if (obj == null)
        {
            SqlCommand sqlCommand2 = new("create table " + dataTable + columns, sqlConnection);
            sqlCommand2.ExecuteNonQuery();
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            return false;
        }
        if (sqlConnection.State == ConnectionState.Open)
        {
            sqlConnection.Close();
        }
        return true;
    }
}
