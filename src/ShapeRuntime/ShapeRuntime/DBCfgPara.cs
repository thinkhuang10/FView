using System;

namespace ShapeRuntime;

[Serializable]
public class DBCfgPara
{
    public string DBType;

    public string DBConnStr;

    public DBCfgPara()
    {
    }

    public DBCfgPara(string dbtype, string dbconnstr)
    {
        DBType = dbtype;
        DBConnStr = dbconnstr;
    }
}
