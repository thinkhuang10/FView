using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class ServerLogicItem
{
    public string LogicType;

    public string ConditionalExpression;

    public Dictionary<string, object> DataDict;

    public string GetDesc()
    {
        if (LogicType == "变量赋值")
        {
            return DataDict["Name"].ToString() + "=" + DataDict["Value"].ToString();
        }
        if (LogicType == "查询数据")
        {
            return DataDict["ResultSQL"].ToString();
        }
        if (LogicType == "添加数据")
        {
            return DataDict["ResultSQL"].ToString();
        }
        if (LogicType == "更新数据")
        {
            return DataDict["ResultSQL"].ToString();
        }
        if (LogicType == "删除数据")
        {
            return DataDict["ResultSQL"].ToString();
        }
        if (LogicType == "定义标签")
        {
            return DataDict["Name"].ToString() + ":";
        }
        if (LogicType == "跳转标签")
        {
            return "goto->" + DataDict["Name"].ToString();
        }
        return null;
    }
}
