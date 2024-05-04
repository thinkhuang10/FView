using System;
using System.Data;

namespace HMIEditEnvironment.Animation;

[Serializable]
public class DBNewSerializeCopy
{
    public bool ansync;

    public string tableName = "";

    public string sqlString = "";

    public DataTable DgDatatable = new();
}
