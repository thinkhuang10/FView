using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using ShapeRuntime;

namespace HMIRunForm.ServerLogic;

[Guid("D197914E-B845-4143-98AC-157934F7C46E")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ServerLogicWorker
{
    private RuntimeDBOperationDele DBOperation;

    private ScriptEngine script;

    private Dictionary<string, object> vars;

    public static ServerLogicWorker GetWorker(RuntimeDBOperationDele DBOperation)
    {
        ServerLogicWorker serverLogicWorker = new()
        {
            DBOperation = DBOperation
        };
        return serverLogicWorker;
    }

    public Dictionary<string, object> DoWork(Dictionary<string, object> _vars, List<ServerLogicItem> logics, List<string> opts)
    {
        script = new ScriptEngine(ScriptLanguage.VBscript);
        script.AddObject("System", this);
        vars = new Dictionary<string, object>(_vars);
        Dictionary<string, int> dictionary = new();
        foreach (ServerLogicItem logic in logics)
        {
            if (logic.LogicType == "定义标签")
            {
                dictionary.Add(logic.DataDict["Name"].ToString(), logics.IndexOf(logic));
            }
        }
        for (int i = 0; i < logics.Count; i++)
        {
            switch (logics[i].LogicType)
            {
                case "变量赋值":
                    SLSetValue(logics[i]);
                    break;
                case "查询数据":
                    SLSetDBSelect(logics[i]);
                    break;
                case "删除数据":
                    SLSetDBDelete(logics[i]);
                    break;
                case "更新数据":
                    SLSetDBUpdate(logics[i]);
                    break;
                case "添加数据":
                    SLSetDBInsert(logics[i]);
                    break;
                case "跳转标签":
                    if ((bool)EvalLogic(logics[i].ConditionalExpression))
                    {
                        i = dictionary[logics[i].DataDict["Name"].ToString()];
                    }
                    break;
            }
        }
        Dictionary<string, object> dictionary2 = new();
        foreach (string opt in opts)
        {
            if (vars.ContainsKey(opt))
            {
                dictionary2.Add(opt, vars[opt]);
            }
            else
            {
                dictionary2.Add(opt, null);
            }
        }
        return dictionary2;
    }

    public object GetValue(string name)
    {
        if (name.ToLower() == "[null]")
        {
            return null;
        }
        if (name.ToLower() == "[true]")
        {
            return true;
        }
        if (name.ToLower() == "[false]")
        {
            return false;
        }
        if (name.StartsWith("[\"") && name.EndsWith("\"]"))
        {
            return name.Substring(2, name.Length - 4);
        }
        float result;
        if (name.StartsWith("[") && name.EndsWith("]") && float.TryParse(name.Substring(1, name.Length - 2), out result))
        {
            if (!name.Contains("."))
            {
                long num = Convert.ToInt64(result);
                if (num <= 127 && num >= -128)
                {
                    return Convert.ToSByte(num);
                }
                if (num <= 32767 && num >= -32768)
                {
                    return Convert.ToInt16(num);
                }
                if (num <= int.MaxValue && num >= int.MinValue)
                {
                    return Convert.ToInt32(num);
                }
                return num;
            }
            return result;
        }
        if (vars.ContainsKey(name))
        {
            return vars[name];
        }
        return null;
    }

    public void SetValue(string name, object value)
    {
        if (vars.ContainsKey(name))
        {
            vars[name] = value;
        }
        else
        {
            vars.Add(name, value);
        }
    }

    private string ReplaceSQLValue(string cmd)
    {
        Regex regex = new("{.*?}");
        MatchCollection matchCollection = regex.Matches(cmd);
        foreach (Match item in matchCollection)
        {
            try
            {
                if (item.Value[1] == '[' && item.Value[item.Value.Length - 2] == ']')
                {
                    object value = GetValue(item.Value.Substring(1, item.Value.Length - 2));
                    string text = "";
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                    cmd = cmd.Replace(item.Value, text.Replace("'", "''"));
                }
            }
            catch
            {
            }
        }
        return cmd;
    }

    private static string LogicToScript(string str)
    {
        StringBuilder stringBuilder = new(str);
        Regex regex = new("\\[[^\\]]+\\]");
        MatchCollection matchCollection = regex.Matches(str);
        List<string> list = new();
        foreach (Match item in matchCollection)
        {
            if (!list.Contains(item.Value))
            {
                stringBuilder = stringBuilder.Replace(item.Value, "System.GetValue(\"" + item.Value + "\")");
                list.Add(item.Value);
            }
        }
        return stringBuilder.ToString();
    }

    private object EvalLogic(string logicStr)
    {
        return EvalScript(LogicToScript(logicStr));
    }

    private object EvalScript(string scriptStr)
    {
        return script.Eval(scriptStr, null);
    }

    private void SLSetValue(ServerLogicItem sli)
    {
        if ((bool)EvalLogic(sli.ConditionalExpression))
        {
            SetValue(sli.DataDict["Name"] as string, EvalLogic(sli.DataDict["Value"] as string));
        }
    }

    private void SLSetDBSelect(ServerLogicItem sli)
    {
        if (!(bool)EvalLogic(sli.ConditionalExpression))
        {
            return;
        }
        object obj = DBOperation("select", ReplaceSQLValue(sli.DataDict["ResultSQL"].ToString()));
        if (obj is DataSet)
        {
            string[] array = sli.DataDict["ResultTo"].ToString().Split(',');
            foreach (string name in array)
            {
                SetValue(name, (obj as DataSet).Tables[0]);
            }
        }
        else if (obj is Exception)
        {
            throw obj as Exception;
        }
    }

    private void SLSetDBInsert(ServerLogicItem sli)
    {
        if ((bool)EvalLogic(sli.ConditionalExpression))
        {
            object obj = DBOperation("insert", ReplaceSQLValue(sli.DataDict["ResultSQL"].ToString()));
            if (obj is int)
            {
                SetValue(sli.DataDict["ResultTo"].ToString(), obj);
            }
            else if (obj is Exception)
            {
                throw obj as Exception;
            }
        }
    }

    private void SLSetDBDelete(ServerLogicItem sli)
    {
        if ((bool)EvalLogic(sli.ConditionalExpression))
        {
            object obj = DBOperation("delete", ReplaceSQLValue(sli.DataDict["ResultSQL"].ToString()));
            if (obj is int)
            {
                SetValue(sli.DataDict["ResultTo"].ToString(), obj);
            }
            else if (obj is Exception)
            {
                throw obj as Exception;
            }
        }
    }

    private void SLSetDBUpdate(ServerLogicItem sli)
    {
        if ((bool)EvalLogic(sli.ConditionalExpression))
        {
            object obj = DBOperation("update", ReplaceSQLValue(sli.DataDict["ResultSQL"].ToString()));
            if (obj is int)
            {
                SetValue(sli.DataDict["ResultTo"].ToString(), obj);
            }
            else if (obj is Exception)
            {
                throw obj as Exception;
            }
        }
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
}
