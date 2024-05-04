using System.Collections.Generic;

namespace HMIEditEnvironment;

public class CDBVarDefine
{
    public string strTableName;

    public List<structTableInfo> ltTableInfo = new();

    public string strSQLConnection;

    public string strSaveTime;
}
