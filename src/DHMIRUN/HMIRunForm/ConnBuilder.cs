using System.Data;
using System.Data.Common;

namespace HMIRunForm;

internal class ConnBuilder
{
    private readonly DataRow dr;

    private DbConnectionStringBuilder builder;

    private DbConnection conn;

    public DbConnectionStringBuilder Builder
    {
        get
        {
            if (builder == null)
            {
                builder = DbProviderFactories.GetFactory(dr).CreateConnectionStringBuilder();
            }
            return builder;
        }
    }

    public DbConnection Conn
    {
        get
        {
            if (conn == null)
            {
                conn = DbProviderFactories.GetFactory(dr).CreateConnection();
            }
            return conn;
        }
    }

    public ConnBuilder(DataRow dr)
    {
        this.dr = dr;
    }

    public override string ToString()
    {
        if (dr != null && dr.ItemArray.Length > 0)
        {
            return dr[0].ToString();
        }
        return base.ToString();
    }
}
