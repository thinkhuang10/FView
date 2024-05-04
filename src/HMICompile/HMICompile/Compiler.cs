using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using ShapeRuntime;

namespace HMICompile;

public class Compiler
{
    public delegate void MsgOpt(string str);

    private readonly HMIProjectFile dhp;

    private readonly List<VarTableItem> vartableitem = new();

    private readonly FileInfo fiProjectFile;

    private readonly List<string> comdllnames = new();

    private readonly List<string> crnames = new();

    private readonly Dictionary<string, List<CShape>> strshapeDictionary = new();

    private readonly Dictionary<string, DataFile> strfileDictionary = new();

    private readonly List<string> referenceAssemblyList = new();

    private string error;

    public string ProjectFile { get; set; }

    public string InputDir { get; set; }

    public string OutputDir { get; set; }

    public Compiler(string projectFile, string inputDir, string outputDir, HMIProjectFile _hpf, List<DataFile> _dfs)
    {
        InputDir = inputDir;
        OutputDir = outputDir;
        ProjectFile = projectFile;
        fiProjectFile = new FileInfo(ProjectFile);
        dhp = _hpf;

        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            VarTableItem item2 = new()
            {
                Name = "[" + projectIO.name + "]",
                ScriptName = projectIO.name,
                Type = int.Parse(projectIO.type)
            };
            vartableitem.Add(item2);
        }
        foreach (ParaIO paraIO in dhp.ParaIOs)
        {
            VarTableItem item3 = new()
            {
                Name = "[" + paraIO.name + "]",
                ScriptName = "ParaIO" + paraIO.ID,
                Type = int.Parse(paraIO.type)
            };
            vartableitem.Add(item3);
        }
        foreach (DataFile _df in _dfs)
        {
            strshapeDictionary.Add(_df.name, _df.ListAllShowCShape);
            strfileDictionary.Add(_df.name, _df);
        }
    }

    public Compiler(string projectFile, string inputDir, string outputDir)
    {
        InputDir = inputDir;
        OutputDir = outputDir;
        ProjectFile = projectFile;
        fiProjectFile = new FileInfo(ProjectFile);
        dhp = Operation.BinaryLoadProject(ProjectFile);
        DataTable iOTable = Operation.GetIOTable(fiProjectFile.Directory.FullName + "\\" + dhp.IOfiles);
        if (iOTable != null)
        {
            foreach (DataRow row in iOTable.Rows)
            {
                VarTableItem item = new()
                {
                    Id = int.Parse(row["id"].ToString()),
                    ScriptName = "var" + row["id"].ToString(),
                    Type = int.Parse(row["ValType"].ToString()),
                    Name = "[" + row["Name"].ToString() + "]"
                };
                vartableitem.Add(item);
            }
        }
        foreach (ProjectIO projectIO in dhp.ProjectIOs)
        {
            VarTableItem item2 = new()
            {
                Name = "[" + projectIO.name + "]",
                ScriptName = projectIO.name,
                Type = int.Parse(projectIO.type)
            };
            vartableitem.Add(item2);
        }
        foreach (ParaIO paraIO in dhp.ParaIOs)
        {
            VarTableItem item3 = new()
            {
                Name = "[" + paraIO.name + "]",
                ScriptName = "ParaIO" + paraIO.ID,
                Type = int.Parse(paraIO.type)
            };
            vartableitem.Add(item3);
        }
        foreach (string key in dhp.pages.Keys)
        {
            DataFile dataFile = Operation.BinaryLoadFile(fiProjectFile.DirectoryName + "\\" + dhp.pages[key]);
            strshapeDictionary.Add(key, dataFile.ListAllShowCShape);
            strfileDictionary.Add(key, dataFile);
        }
    }

    public void CreateScript()
    {
        string text = "    ";
        StringBuilder stringBuilder = new();
        foreach (string key in dhp.pages.Keys)
        {
            StringBuilder stringBuilder2 = new();
            new StringBuilder();
            StringBuilder stringBuilder3 = new();
            new StringBuilder();
            new StringBuilder();
            new StringBuilder();
            new StringBuilder();
            new StringBuilder();
            new StringBuilder();
            StringBuilder stringBuilder4 = new();
            StringBuilder stringBuilder5 = new();
            for (int i = 0; i < strshapeDictionary[key].Count; i++)
            {
                CShape cShape = strshapeDictionary[key][i];
                object obj = ((!(cShape is CControl)) ? ((object)cShape) : ((object)(cShape as CControl)._c));
                Type type = obj.GetType();
                EventInfo[] events = type.GetEvents();
                EventInfo[] array = events;
                foreach (EventInfo eventInfo in array)
                {
                    bool flag = false;
                    for (int k = 0; k < cShape.ysdzshijianmingcheng.Length; k++)
                    {
                        string text2 = cShape.ysdzshijianmingcheng[k];
                        string text3 = cShape.ysdzshijianLogic[k];
                        if (!(text2 == "") && text3 != null && text2.Split(' ')[1] == eventInfo.Name && text3 != null && text3.Trim() != "")
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (cShape.eventBindDict != null && cShape.eventBindDict.ContainsKey(eventInfo.Name) && cShape.eventBindDict[eventInfo.Name].Count != 0)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    stringBuilder4.Append(text + text + text);
                    stringBuilder4.Append("this.");
                    stringBuilder4.Append(strshapeDictionary[key][i].Name);
                    stringBuilder4.Append(".");
                    stringBuilder4.Append(eventInfo.Name);
                    stringBuilder4.Append("+= new ");
                    stringBuilder4.Append(eventInfo.EventHandlerType.ToString());
                    stringBuilder4.Append("(");
                    stringBuilder4.Append(strshapeDictionary[key][i].Name);
                    stringBuilder4.Append("_" + eventInfo.Name + ");");
                    stringBuilder4.Append(Environment.NewLine);
                    stringBuilder5.Append(text + text);
                    MethodInfo method = eventInfo.EventHandlerType.GetMethod("Invoke");
                    string fullName = method.ReturnType.FullName;
                    fullName = ((fullName == "System.Void") ? "void" : fullName);
                    stringBuilder5.Append("public " + fullName + " ");
                    stringBuilder5.Append(strshapeDictionary[key][i].Name);
                    stringBuilder5.Append("_" + eventInfo.Name);
                    string text4 = "(";
                    ParameterInfo[] parameters = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters();
                    ParameterInfo[] array2 = parameters;
                    foreach (ParameterInfo parameterInfo in array2)
                    {
                        string text5 = text4;
                        text4 = text5 + parameterInfo.ParameterType.FullName + " " + parameterInfo.Name + ",";
                    }
                    if (text4.EndsWith(","))
                    {
                        text4 = text4.Substring(0, text4.Length - 1);
                    }
                    text4 += ")";
                    stringBuilder5.Append(text4);
                    stringBuilder5.Append(Environment.NewLine);
                    stringBuilder5.Append(text + text);
                    stringBuilder5.Append("{");
                    stringBuilder5.Append(Environment.NewLine);
                    for (int m = 0; m < strshapeDictionary[key][i].ysdzshijianLogic.Length; m++)
                    {
                        if (strshapeDictionary[key][i].ysdzshijianLogic != null && strshapeDictionary[key][i].ysdzshijianLogic.Length != 0 && strshapeDictionary[key][i].ysdzshijianmingcheng != null && strshapeDictionary[key][i].ysdzshijianmingcheng[m] != null && strshapeDictionary[key][i].ysdzshijianmingcheng[m].Split(' ')[1] == eventInfo.Name && strshapeDictionary[key][i].ysdzshijianLogic[m] != null && strshapeDictionary[key][i].ysdzshijianLogic[m].Trim() != "")
                        {
                            stringBuilder5.Append(text + text + text);
                            stringBuilder5.Append("if (datafile.ListAllShowCShape[");
                            stringBuilder5.Append(i);
                            stringBuilder5.Append("].ysdzshijianLogic!=null)");
                            stringBuilder5.Append(Environment.NewLine);
                            stringBuilder5.Append(text + text + text + text);
                            stringBuilder5.Append("try{");
                            stringBuilder5.Append(Environment.NewLine);
                            stringBuilder5.Append(text + text + text + text);
                            stringBuilder5.Append("execute2(datafile.ListAllShowCShape[");
                            stringBuilder5.Append(i);
                            stringBuilder5.Append("],datafile.ListAllShowCShape[");
                            stringBuilder5.Append(i);
                            stringBuilder5.Append("].ysdzshijianLogic[");
                            stringBuilder5.Append(m);
                            stringBuilder5.Append("]");
                            stringBuilder5.Append(",datafile.name);");
                            stringBuilder5.Append(Environment.NewLine);
                            stringBuilder5.Append(text + text + text + text + "}");
                            stringBuilder5.Append(Environment.NewLine);
                            stringBuilder5.Append(text + text + text + text);
                            stringBuilder5.Append("catch{}");
                            stringBuilder5.Append(Environment.NewLine);
                        }
                    }
                    if (cShape.eventBindDict != null && cShape.eventBindDict.ContainsKey(eventInfo.Name))
                    {
                        List<EventSetItem> list = cShape.eventBindDict[eventInfo.Name];
                        foreach (EventSetItem item in list)
                        {
                            if (item.OperationType == "属性赋值")
                            {
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "try" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + "object tempobj=EvalEvent(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\");" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + "if(tempobj is IConvertible)" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + text + "SetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(tempobj, Type.GetType(\"" + item.ToObject.Value + "\")));" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + "else" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + text + "SetValueEvent(\"[" + item.ToObject.Key + "]\", tempobj);" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                            }
                            else if (item.OperationType == "变量赋值")
                            {
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "try" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "{" + Environment.NewLine);
                                if (item.ToObject.Key != "")
                                {
                                    stringBuilder5.Append(text + text + text + text + text + "object tempobj=EvalEvent(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\");" + Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + text + text + "if(tempobj is IConvertible)" + Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + text + text + text + "SetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(tempobj, Type.GetType(\"" + item.ToObject.Value + "\")));" + Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + text + text + "else" + Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + text + text + text + "SetValueEvent(\"[" + item.ToObject.Key + "]\",tempobj);" + Environment.NewLine);
                                }
                                else
                                {
                                    stringBuilder5.Append(text + text + text + text + text + "execute(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\",null);" + Environment.NewLine);
                                }
                                stringBuilder5.Append(text + text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                            }
                            else if (item.OperationType == "方法调用")
                            {
                                if (item.ToObject.Key == "")
                                {
                                    string text6 = item.FromObject.Substring(0, item.FromObject.LastIndexOf("."));
                                    string text7 = item.FromObject.Substring(item.FromObject.LastIndexOf(".") + 1);
                                    string text8 = "new object[]{";
                                    foreach (KVPart<string, string> para in item.Paras)
                                    {
                                        text8 = text8 + "EvalEvent(\"" + para.Key.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"),";
                                    }
                                    if (text8.EndsWith(","))
                                    {
                                        text8 = text8.Substring(0, text8.Length - 1);
                                    }
                                    text8 += "}";
                                    stringBuilder5.Append(Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + "try{if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"))){Object obj=GetValueEvent(\"[" + text6 + "]\");obj.GetType().GetMethod(\"" + text7 + "\").Invoke(obj," + text8 + ");}}");
                                    stringBuilder5.Append(Environment.NewLine);
                                    stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                                    continue;
                                }
                                string text9 = item.FromObject.Substring(0, item.FromObject.LastIndexOf("."));
                                string text10 = item.FromObject.Substring(item.FromObject.LastIndexOf(".") + 1);
                                string text11 = "new object[]{";
                                foreach (KVPart<string, string> para2 in item.Paras)
                                {
                                    text11 = text11 + "EvalEvent(\"" + para2.Key.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"),";
                                }
                                if (text11.EndsWith(","))
                                {
                                    text11 = text11.Substring(0, text11.Length - 1);
                                }
                                text11 += "}";
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "try{if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"))){Object obj=GetValueEvent(\"[" + text9 + "]\");object retobj=obj.GetType().GetMethod(\"" + text10 + "\").Invoke(obj," + text11 + ");if(retobj is IConvertible) SetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(retobj, Type.GetType(\"" + item.ToObject.Value + "\"))); else SetValueEvent(\"[" + item.ToObject.Key + "]\", retobj );}}");
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                            }
                            else if (item.OperationType == "定义标签")
                            {
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + item.FromObject + ":" + Environment.NewLine);
                                stringBuilder5.Append(text + text + ";" + Environment.NewLine);
                            }
                            else if (item.OperationType == "跳转标签")
                            {
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "try" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + "goto " + item.FromObject + ";" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                            }
                            else if (item.OperationType == "服务器逻辑")
                            {
                                stringBuilder5.Append(Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "try" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "{" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + text + "CallRuntimeExecute(\"服务器逻辑\",datafile.ListAllShowCShape[" + i.ToString() + "].eventBindDict[\"" + eventInfo.Name + "\"][" + list.IndexOf(item) + "].Tag);" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "}" + Environment.NewLine);
                                stringBuilder5.Append(text + text + text + "catch (System.Exception ex){MessageBox.Show(\"" + key + "." + cShape.Name + "." + item.EventName + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                            }
                        }
                    }
                    stringBuilder5.Append(Environment.NewLine);
                    if (method.ReturnType.FullName != "System.Void")
                    {
                        if (method.ReturnType.IsValueType)
                        {
                            stringBuilder5.Append(text + text + "return 0;");
                        }
                        else
                        {
                            stringBuilder5.Append(text + text + "return null;");
                        }
                    }
                    stringBuilder5.Append(Environment.NewLine);
                    stringBuilder5.Append(text + text + "}");
                    stringBuilder5.Append(Environment.NewLine);
                }
                if (strshapeDictionary[key][i] is CControl)
                {
                    string dllfile = ((CControl)strshapeDictionary[key][i])._dllfile;
                    string type2 = ((CControl)strshapeDictionary[key][i]).type;
                    if (dllfile != null && dllfile != "")
                    {
                        comdllnames.Add(dllfile);
                    }
                    stringBuilder2.Append(text + text);
                    stringBuilder2.Append("public ");
                    stringBuilder2.Append(type2 + " ");
                    stringBuilder2.Append(strshapeDictionary[key][i].Name);
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + "{");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text);
                    stringBuilder2.Append("get{");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text);
                    stringBuilder2.Append("if (datafile == null)");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text);
                    stringBuilder2.Append("{");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text + text);
                    stringBuilder2.Append("DoLoadPage(\"" + key + "\");");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text);
                    stringBuilder2.Append("}");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + text);
                    stringBuilder2.Append("return (" + type2 + ")(((CControl)datafile.ListAllShowCShape[");
                    stringBuilder2.Append(i);
                    stringBuilder2.Append("])._c);}");
                    stringBuilder2.Append(Environment.NewLine);
                    stringBuilder2.Append(text + text + "}");
                    stringBuilder2.Append(Environment.NewLine);
                    continue;
                }
                if (strshapeDictionary[key][i] is CGraphicsPath)
                {
                    CShape[] shapes = ((CGraphicsPath)strshapeDictionary[key][i]).Shapes;
                    foreach (CShape cShape2 in shapes)
                    {
                        if (cShape2 is CVectorGraph)
                        {
                            string type3 = ((CVectorGraph)cShape2).type;
                            crnames.Add(type3);
                        }
                    }
                }
                if (strshapeDictionary[key][i] is CVectorGraph)
                {
                    string type4 = ((CVectorGraph)strshapeDictionary[key][i]).type;
                    crnames.Add(type4);
                }
                else if (strshapeDictionary[key][i] is CControl && (strshapeDictionary[key][i] as CControl)._files.Length > 0)
                {
                    string dllfile2 = (strshapeDictionary[key][i] as CControl)._dllfile;
                    if (!referenceAssemblyList.Contains(dllfile2))
                    {
                        referenceAssemblyList.Add(dllfile2);
                    }
                }
                else if (strshapeDictionary[key][i] is CPixieControl && (strshapeDictionary[key][i] as CPixieControl)._files.Length > 0)
                {
                    string dllfile3 = (strshapeDictionary[key][i] as CPixieControl)._dllfile;
                    if (!referenceAssemblyList.Contains(dllfile3))
                    {
                        referenceAssemblyList.Add(dllfile3);
                    }
                }
                stringBuilder2.Append(text + text);
                stringBuilder2.Append("public " + strshapeDictionary[key][i].GetType().FullName + " ");
                stringBuilder2.Append(strshapeDictionary[key][i].Name);
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + "{");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("get{");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("if (datafile == null)");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("{");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text + text);
                stringBuilder2.Append("DoLoadPage(\"" + key + "\");");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("}");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append(" return (" + strshapeDictionary[key][i].GetType().FullName + ")datafile.ListAllShowCShape[");
                stringBuilder2.Append(i);
                stringBuilder2.Append("];}");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("set{");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("if (datafile == null)");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("{");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text + text);
                stringBuilder2.Append("DoLoadPage(\"" + key + "\");");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + text);
                stringBuilder2.Append("}");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append("datafile.ListAllShowCShape[");
                stringBuilder2.Append(i);
                stringBuilder2.Append("]=value;}");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder2.Append(text + text + "}");
                stringBuilder2.Append(Environment.NewLine);
                stringBuilder3.Append(text + text + text);
                stringBuilder3.Append(strshapeDictionary[key][i].UserLogic[0]);
                stringBuilder3.Append(Environment.NewLine);
            }
            stringBuilder.Append(text);
            stringBuilder.Append("[Guid(" + '"' + Guid.NewGuid().ToString() + '"' + ")]");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text);
            stringBuilder.Append("[ComVisible(true)]");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text);
            stringBuilder.Append("[ClassInterface(ClassInterfaceType.AutoDispatch)]");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text);
            stringBuilder.Append("public class ");
            stringBuilder.Append("Page_" + key);
            stringBuilder.Append(" : UserControl");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public ");
            stringBuilder.Append("Page_" + key);
            stringBuilder.Append("(DataFile df)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable , true);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("FocusButton.Size = new Size(0, 0);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("FocusButton.Location = new Point(0, 0);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("this.Controls.Add(FocusButton);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("datafile=df;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("timer=new System.Windows.Forms.Timer();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("timer.Interval=");
            stringBuilder.Append(strfileDictionary[key].LogicTime);
            stringBuilder.Append(";");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("timer.Tick += new EventHandler(");
            stringBuilder.Append("OnPageRuning);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("timer.Enabled=false;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("Button FocusButton = new Button();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void ");
            stringBuilder.Append("GetFocus");
            stringBuilder.Append("()");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("FocusButton.Focus();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(stringBuilder2);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public DataFile datafile;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public bool inLoad;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public System.Windows.Forms.Timer timer;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public bool TimerEnable");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("set{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "timer.Enabled=value;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "if(value)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + text);
            stringBuilder.Append("System.Windows.Forms.Timer tempTimer=new System.Windows.Forms.Timer();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + text);
            stringBuilder.Append("tempTimer.Interval=1;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + text);
            stringBuilder.Append("tempTimer.Tick += new EventHandler(OnPageFirstRun);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + text);
            stringBuilder.Append("tempTimer.Enabled=true;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(stringBuilder5);
            stringBuilder.Append(Environment.NewLine + text + text + "public void OnPageFirstRun(object sender,EventArgs e)");
            stringBuilder.Append(Environment.NewLine + text + text + "{");
            stringBuilder.Append(Environment.NewLine + text + text + text + "((System.Windows.Forms.Timer)sender).Enabled = false;");
            stringBuilder.Append(Environment.NewLine + text + text + text + "((System.Windows.Forms.Timer)sender).Dispose();");
            stringBuilder.Append(Environment.NewLine + text + text + text + "OnPageRuning(sender,null);");
            stringBuilder.Append(Environment.NewLine + text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void OnPageRuning(object sender,EventArgs e)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if (datafile == null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("DoLoadPage(\"" + key + "\");");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "try" + Environment.NewLine + text + text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + text);
            stringBuilder.Append("execute(datafile.");
            stringBuilder.Append("_pagedzyxLogic,datafile.name);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "catch(Exception ex)" + Environment.NewLine + text + text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "timer.Enabled=false;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "MessageBox.Show(ex.Message);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "}");
            StringBuilder stringBuilder6 = new();
            foreach (CShape item2 in strfileDictionary[key].ListAllShowCShape)
            {
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(text + text + text + "//" + item2.Name);
                if (item2.propertyBindDT == null)
                {
                    continue;
                }
                foreach (DataRow row in item2.propertyBindDT.Rows)
                {
                    stringBuilder6.Append(Environment.NewLine);
                    stringBuilder6.Append(text + text + "bool " + item2.Name + "_" + row["PropertyName"].ToString() + "_ErrShow=true;");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "try");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "{");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "object tempobj=GetValueEvent(\"[" + row["Bind"].ToString().Replace("\"", "\\\"") + "]\");" + Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "if(tempobj is IConvertible)" + Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + item2.Name + ".GetType().GetProperty(\"" + row["PropertyName"].ToString() + "\").SetValue(" + item2.Name + ",Convert.ChangeType(tempobj,Type.GetType(\"" + row["Type"].ToString() + "\")),null);" + Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "else" + Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + item2.Name + ".GetType().GetProperty(\"" + row["PropertyName"].ToString() + "\").SetValue(" + item2.Name + ",tempobj,null);");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "}");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "catch (System.Exception ex)");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "{");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "if(" + item2.Name + "_" + row["PropertyName"].ToString() + "_ErrShow)");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "{");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + item2.Name + "_" + row["PropertyName"].ToString() + "_ErrShow=false;");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + "if(this_OnPageRun_ErrShow)");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + "{");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + text + "this_OnPageRun_ErrShow=false;");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + text + "MessageBox.Show(\"" + key + "." + item2.Name + "." + row["PropertyName"].ToString() + "在赋值时产生错误.\" + Environment.NewLine + ex.Message);");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + text + "}");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + text + "}");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append(text + text + text + "}");
                }
            }
            stringBuilder6.Append(Environment.NewLine);
            stringBuilder6.Append(text + text + "bool this_OnPageRun_ErrShow=true;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(CreateEvendFireBody(strfileDictionary[key], "OnPageRun", key));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(stringBuilder6);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event CallRuntimeDele CallRuntime;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public object CallRuntimeExecute(string str,object obj)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if(CallRuntime!=null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("return CallRuntime(str,obj);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("return null;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event SetValue SetValueEvent;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void setvalue(string name,object v)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("SetValueEvent(name,v);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event GetValue GetValueEvent;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public object getvalue(string s)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("return GetValueEvent(s);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event GetValue EvalEvent;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public object eval(string s)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("return EvalEvent(s);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event Execute ExecuteEvent;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void execute(string s,string name)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if(s!=null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("ExecuteEvent(s,name);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event Execute2 ExecuteEvent2;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void execute2(CShape shape,string s,string name)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if(s!=null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("ExecuteEvent2(shape,s,name);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("private object onelock = new object();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public event LoadPageEventHandler LoadPage;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text);
            stringBuilder.Append("public void DoLoadPage(string str)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "while(!Monitor.TryEnter(onelock))");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "System.Windows.Forms.Application.DoEvents();");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "Thread.Sleep(0);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if (datafile!=null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("return;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("if(LoadPage!=null)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text);
            stringBuilder.Append("datafile = LoadPage(str);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(stringBuilder4);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + "Monitor.Exit(onelock);");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "private bool lastVisible = false;");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "protected override void OnVisibleChanged(EventArgs e)" + Environment.NewLine);
            stringBuilder.Append(text + text + "{" + Environment.NewLine);
            stringBuilder.Append(text + text + text + "if(Visible != lastVisible)" + Environment.NewLine);
            stringBuilder.Append(text + text + text + "{" + Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "lastVisible = Visible;" + Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "if (Visible)" + Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "{" + Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(CreateEvendFireBody(strfileDictionary[key], "OnPageShow", key));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "}" + Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "else" + Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "{" + Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(CreateEvendFireBody(strfileDictionary[key], "OnPageHide", key));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text + text + "}" + Environment.NewLine);
            stringBuilder.Append(text + text + text + "}" + Environment.NewLine);
            stringBuilder.Append(text + text + text + "base.OnVisibleChanged(e);" + Environment.NewLine);
            stringBuilder.Append(text + text + "}" + Environment.NewLine);
            stringBuilder.Append(text + "}");
            stringBuilder.Append(Environment.NewLine);
        }
        using StreamWriter streamWriter = new(InputDir + "Logic.cs");
        StringBuilder stringBuilder7 = new(Resource.Logic_cs);
        stringBuilder7.Replace("#[ClassBlocks]", stringBuilder.ToString()).Replace("#[GLOBLE_GUID]", Guid.NewGuid().ToString()).Replace("#[VAR_LIST]", GenerateVarBlock())
            .Replace("#[PROJECT_NAME]", fiProjectFile.Name);
        streamWriter.Write(stringBuilder7.ToString());
    }

    public void ZipResource(bool compress, bool dirtyCompile, List<string> dirtyList, MsgOpt msgCall)
    {
        using ResourceWriter resourceWriter = new(InputDir + "myResoures.resources");
        using (FileStream fileStream = new(ProjectFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            byte[] array = new byte[(int)fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            if (compress)
            {
                resourceWriter.AddResource(fiProjectFile.Name, Operation.CompressStream(array));
            }
            else
            {
                resourceWriter.AddResource(fiProjectFile.Name, array);
            }
        }

        //using (FileStream fileStream2 = new FileStream(fiProjectFile.Directory.FullName + "\\" + dhp.IOfiles, FileMode.Open, FileAccess.Read, FileShare.Read))
        //{
        //	byte[] array2 = new byte[fileStream2.Length];
        //	fileStream2.Read(array2, 0, array2.Length);
        //	if (compress)
        //	{
        //		resourceWriter.AddResource(dhp.IOfiles, Operation.CompressStream(array2));
        //	}
        //	else
        //	{
        //		resourceWriter.AddResource(dhp.IOfiles, array2);
        //	}
        //}

        foreach (string value in dhp.pages.Values)
        {
            if (!dirtyCompile || dirtyList.Contains(value.Replace(".hpg", "")) || !File.Exists(fiProjectFile.DirectoryName + "\\CompileTemp\\" + value))
            {
                if (!Directory.Exists(fiProjectFile.DirectoryName + "\\CompileTemp\\"))
                {
                    Directory.CreateDirectory(fiProjectFile.DirectoryName + "\\CompileTemp\\");
                }
                if (compress)
                {
                    msgCall("开始压缩" + value + ".");
                    File.WriteAllBytes(fiProjectFile.DirectoryName + "\\CompileTemp\\" + value, Operation.CompressStream(File.ReadAllBytes(fiProjectFile.DirectoryName + "\\" + value)));
                    msgCall("压缩" + value + "成功.");
                }
                else
                {
                    File.Copy(fiProjectFile.DirectoryName + "\\" + value, fiProjectFile.DirectoryName + "\\CompileTemp\\" + value, overwrite: true);
                }
            }
            msgCall("开始拷贝" + value + ".");
            File.Copy(fiProjectFile.DirectoryName + "\\CompileTemp\\" + value, OutputDir + value, overwrite: true);
            msgCall("拷贝" + value + "成功.");
        }
        resourceWriter.Generate();
    }

    public bool CreatCustomLogicDLL()
    {
        try
        {
            string path = OutputDir + "CustomLogic.dll";
            if (!File.Exists(path))
            {
                using (File.Create(path))
                {
                }
            }
            return true;
        }
        catch
        {
            MessageBox.Show("编译失败：请关闭杀毒软件重新编译！", "提示");
            return false;
        }
    }

    public bool DynamicCompile()
    {
        try
        {
            CSharpCodeProvider cSharpCodeProvider = new();
            CompilerParameters compilerParameters = new()
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                IncludeDebugInformation = false,
                CompilerOptions = "/optimize",
                OutputAssembly = OutputDir + "CustomLogic.dll"
            };
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "ShapeRuntime.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Data.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            //compilerParameters.ReferencedAssemblies.Add("VectorGraphProvider.dll");
            compilerParameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "CommonSnappableTypes.dll");
            foreach (string referenceAssembly in referenceAssemblyList)
            {
                if (referenceAssembly.Contains("\\") || referenceAssembly.Contains("/"))
                {
                    compilerParameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + referenceAssembly.Substring(referenceAssembly.LastIndexOfAny(new char[2] { '\\', '/' })));
                }
                else
                {
                    compilerParameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + referenceAssembly);
                }
            }
            foreach (string comdllname in comdllnames)
            {
                compilerParameters.ReferencedAssemblies.Add(comdllname);
            }
            compilerParameters.EmbeddedResources.Add(InputDir + "myResoures.resources");
            CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromFile(compilerParameters, InputDir + "Logic.cs");
            if (compilerResults.Errors.HasErrors)
            {
                StringBuilder stringBuilder = new();
                foreach (CompilerError error in compilerResults.Errors)
                {
                    stringBuilder.Append(error.ToString());
                    stringBuilder.Append(Environment.NewLine);
                }
                this.error = stringBuilder.ToString();
                MessageBox.Show(this.error.ToString());
                Console.WriteLine(this.error);
                Console.ReadLine();
                return false;
            }
            if (crnames.Count > 0)
            {
                CSharpCodeProvider cSharpCodeProvider2 = new();
                CompilerParameters compilerParameters2 = new()
                {
                    GenerateExecutable = false,
                    GenerateInMemory = false,
                    IncludeDebugInformation = false
                };
                //compilerParameters2.OutputAssembly = OutputDir + "VectorGraphProvider.dll";
                compilerParameters2.ReferencedAssemblies.Add("System.dll");
                compilerParameters2.ReferencedAssemblies.Add("System.Drawing.dll");
                compilerParameters2.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                compilerParameters2.ReferencedAssemblies.Add("System.Data.dll");
                compilerParameters2.ReferencedAssemblies.Add("System.Xml.dll");
                string[] array = new string[2 + crnames.Count];
                array[0] = ".\\ICLibrary\\SVG.cs";
                array[1] = ".\\ICLibrary\\VectorGraph.cs";
                for (int i = 0; i < crnames.Count; i++)
                {
                    string text = crnames[i].Split('.')[crnames[i].Split('.').Length - 1].Substring(4, 1);
                    array[i + 2] = ".\\ICLibrary\\" + text + "\\" + crnames[i].Split('.')[crnames[i].Split('.').Length - 1] + ".cs";
                }
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\VectorGraphProvider.dat"))
                //{
                //	StringBuilder stringBuilder2 = new StringBuilder();
                //	string[] array2 = array;
                //	foreach (string text2 in array2)
                //	{
                //		stringBuilder2.Append(" ");
                //		stringBuilder2.Append(text2.Substring(2));
                //	}
                //	ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //	processStartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\7z.exe";
                //	processStartInfo.Arguments = "x -aos -pDCCE VectorGraphProvider.dat" + stringBuilder2.ToString();
                //	processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //	Process.Start(processStartInfo).WaitForExit();
                //}
                CompilerResults compilerResults2 = cSharpCodeProvider2.CompileAssemblyFromFile(compilerParameters2, array);
                if (compilerResults2.Errors.HasErrors)
                {
                    StringBuilder stringBuilder3 = new();
                    foreach (CompilerError error2 in compilerResults2.Errors)
                    {
                        stringBuilder3.Append(error2.ToString());
                        stringBuilder3.Append(Environment.NewLine);
                    }
                    this.error = stringBuilder3.ToString();
                    MessageBox.Show(this.error.ToString());
                    Console.WriteLine(this.error);
                    Console.ReadLine();
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            this.error = ex.Message;
            MessageBox.Show(this.error);
            return false;
        }
        return true;
    }

    public void CreateCab()
    {
        List<string[]> list = new();
        FileInfo[] files = fiProjectFile.Directory.GetFiles();
        foreach (FileInfo fileInfo in files)
        {
            if (fileInfo.Name.Contains("ActiveX."))
            {
                string[] array = fileInfo.Name.Split('.');
                int num = array.Length;
                list.Add(new string[4]
                {
                    fileInfo.Name,
                    array[num - 4],
                    array[num - 3].Replace('_', ','),
                    array[num - 2]
                });
            }
        }
        if (list.Count <= 0)
        {
            return;
        }
        StringBuilder stringBuilder = new();
        StringBuilder stringBuilder2 = new();
        StringBuilder stringBuilder3 = new();
        foreach (string[] item in list)
        {
            stringBuilder.AppendLine(item[0] + "=" + item[0]);
            stringBuilder2.AppendLine("[" + item[0] + "]");
            stringBuilder2.AppendLine("file=thiscab");
            stringBuilder2.AppendLine("clsid=" + item[1]);
            stringBuilder2.AppendLine("FileVersion=" + item[2]);
            stringBuilder2.AppendLine("RegisterServer=yes");
            stringBuilder2.AppendLine();
            stringBuilder3.Append(item[0] + " ");
        }
        using (StreamWriter streamWriter = new(string.Concat(fiProjectFile.Directory, "\\main.inf")))
        {
            StringBuilder stringBuilder4 = new();
            stringBuilder4.AppendLine("[version]");
            stringBuilder4.AppendLine("signature=\"$CHINA$\"");
            stringBuilder4.AppendLine("AdvancedINF=1.0");
            stringBuilder4.AppendLine();
            stringBuilder4.AppendLine("[Add.Code]");
            stringBuilder4.Append(stringBuilder);
            stringBuilder4.AppendLine();
            stringBuilder4.Append(stringBuilder2);
            stringBuilder4.AppendLine();
            streamWriter.Write(stringBuilder4.ToString());
            stringBuilder3.Append("main.inf");
        }
        File.Copy(AppDomain.CurrentDomain.BaseDirectory + "CABARC.exe", fiProjectFile.Directory.FullName + "\\CABARC.exe", overwrite: true);
        string currentDirectory = Environment.CurrentDirectory;
        Environment.CurrentDirectory = fiProjectFile.Directory.FullName;
        Process process = new();
        ProcessStartInfo processStartInfo = new(string.Concat(fiProjectFile.Directory, "\\CABARC.exe"), "-s 6144 n \"" + OutputDir + "main.cab\" " + stringBuilder3)
        {
            WindowStyle = ProcessWindowStyle.Hidden
        };
        process.StartInfo = processStartInfo;
        process.Start();
        Environment.CurrentDirectory = currentDirectory;
    }

    public void CreateHTML()
    {
        using (StreamWriter streamWriter = new(OutputDir + dhp.projectname + ".html"))
        {
            StringBuilder stringBuilder = new();
            FileInfo[] files = fiProjectFile.Directory.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                if (fileInfo.Name.Contains("ActiveX."))
                {
                    string[] array = fileInfo.Name.Split('.');
                    int num = array.Length;
                    string[] array2 = new string[4]
                    {
                        fileInfo.Name,
                        array[num - 4],
                        array[num - 3].Replace('_', ','),
                        array[num - 2]
                    };
                    stringBuilder.AppendLine("    <object classid=\"clsid:" + array[1].Substring(1, array[1].Length - 2) + "\" codebase=\"main.cab\"></object>");
                }
            }
            StringBuilder stringBuilder2 = new();
            stringBuilder2.Append(Resource.project_html.Replace("#[EmbbedBlock]", stringBuilder.ToString()));
            streamWriter.Write(stringBuilder2.ToString());
        }
        using StreamWriter streamWriter2 = new(OutputDir + "index.html");
        StringBuilder stringBuilder3 = new();
        stringBuilder3.Append(Resource.index_html.Replace("#[ProjectWidth]", dhp.ProjectSize.Width + "px").Replace("#[ProjectHeight]", dhp.ProjectSize.Height + "px"));
        streamWriter2.Write(stringBuilder3.ToString());
    }

    private string GenerateVarBlock()
    {
        string text = "    ";
        StringBuilder stringBuilder = new();
        foreach (VarTableItem item in vartableitem)
        {
            string text2;
            string text3;
            switch (item.Type)
            {
                case 0:
                    text2 = "Convert.ToBoolean";
                    text3 = "bool";
                    break;
                case 1:
                    text2 = "Convert.ToByte";
                    text3 = "Byte";
                    break;
                case 2:
                    text2 = "Convert.ToInt16";
                    text3 = "Int16";
                    break;
                case 3:
                    text2 = "Convert.ToInt16";
                    text3 = "Int16";
                    break;
                case 4:
                    text2 = "Convert.ToInt32";
                    text3 = "Int32";
                    break;
                case 5:
                    text2 = "Convert.ToInt32";
                    text3 = "Int32";
                    break;
                case 6:
                    text2 = "Convert.ToSingle";
                    text3 = "Single";
                    break;
                case 7:
                    text2 = "Convert.ToSingle";
                    text3 = "Single";
                    break;
                case 8:
                    text2 = "Convert.ToDouble";
                    text3 = "double";
                    break;
                case 9:
                    text2 = "Convert.ToString";
                    text3 = "string";
                    break;
                case 10:
                    text2 = "Convert.ToInt32";
                    text3 = "Int32";
                    break;
                case 11:
                    text2 = "Convert.ToInt32";
                    text3 = "Int32";
                    break;
                case 1024:
                    text2 = "";
                    text3 = "Object";
                    break;
                default:
                    text2 = "";
                    text3 = "Object";
                    break;
            }
            stringBuilder.Append(text + text);
            stringBuilder.Append("public " + text3 + " " + item.ScriptName);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("get {return " + text2 + "(GetValueEvent(" + '"');
            stringBuilder.Append(item.Name);
            stringBuilder.Append('"' + "));}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + text);
            stringBuilder.Append("set {SetValueEvent(" + '"');
            stringBuilder.Append(item.Name);
            stringBuilder.Append('"' + ",value);}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(text + text + "}");
            stringBuilder.Append(Environment.NewLine);
        }
        return stringBuilder.ToString();
    }

    private StringBuilder CreateEvendFireBody(IEventBind data, string eventname, string info)
    {
        StringBuilder stringBuilder = new();
        if (data.EventBindDict != null && data.EventBindDict.ContainsKey(eventname))
        {
            List<EventSetItem> list = data.EventBindDict[eventname];
            foreach (EventSetItem item in list)
            {
                if (item.OperationType == "属性赋值")
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\ttry" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\tif(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\tobject tempobj=EvalEvent(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\");" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\tif(tempobj is IConvertible)" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\t\tSetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(tempobj, Type.GetType(\"" + item.ToObject.Value + "\")));" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\telse" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\t\tSetValueEvent(\"[" + item.ToObject.Key + "]\", tempobj);" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "." + eventname + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                }
                else if (item.OperationType == "变量赋值")
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\ttry" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\tif(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t{" + Environment.NewLine);
                    if (item.ToObject.Key != "")
                    {
                        stringBuilder.Append("\t\t\t\t\t\tobject tempobj=EvalEvent(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\");" + Environment.NewLine);
                        stringBuilder.Append("\t\t\t\t\t\tif(tempobj is IConvertible)" + Environment.NewLine);
                        stringBuilder.Append("\t\t\t\t\t\t\tSetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(tempobj, Type.GetType(\"" + item.ToObject.Value + "\")));" + Environment.NewLine);
                        stringBuilder.Append("\t\t\t\t\t\telse" + Environment.NewLine);
                        stringBuilder.Append("\t\t\t\t\t\t\tSetValueEvent(\"[" + item.ToObject.Key + "]\",tempobj);" + Environment.NewLine);
                    }
                    else
                    {
                        stringBuilder.Append("\t\t\t\t\t\texecute(\"" + item.FromObject.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\",null);" + Environment.NewLine);
                    }
                    stringBuilder.Append("\t\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "." + eventname + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                }
                else if (item.OperationType == "方法调用")
                {
                    if (item.ToObject.Key == "")
                    {
                        string text = item.FromObject.Substring(0, item.FromObject.LastIndexOf("."));
                        string text2 = item.FromObject.Substring(item.FromObject.LastIndexOf(".") + 1);
                        string text3 = "new object[]{";
                        foreach (KVPart<string, string> para in item.Paras)
                        {
                            text3 = text3 + "EvalEvent(\"" + para.Key.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"),";
                        }
                        if (text3.EndsWith(","))
                        {
                            text3 = text3.Substring(0, text3.Length - 1);
                        }
                        text3 += "}";
                        stringBuilder.Append(Environment.NewLine);
                        stringBuilder.Append("\t\t\t\ttry{if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"))){Object obj=GetValueEvent(\"[" + text + "]\");obj.GetType().GetMethod(\"" + text2 + "\").Invoke(obj," + text3 + ");}}");
                        stringBuilder.Append(Environment.NewLine);
                        stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "." + eventname + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                        continue;
                    }
                    string text4 = item.FromObject.Substring(0, item.FromObject.LastIndexOf("."));
                    string text5 = item.FromObject.Substring(item.FromObject.LastIndexOf(".") + 1);
                    string text6 = "new object[]{";
                    foreach (KVPart<string, string> para2 in item.Paras)
                    {
                        text6 = text6 + "EvalEvent(\"" + para2.Key.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"),";
                    }
                    if (text6.EndsWith(","))
                    {
                        text6 = text6.Substring(0, text6.Length - 1);
                    }
                    text6 += "}";
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\ttry{if(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"))){Object obj=GetValueEvent(\"[" + text4 + "]\");object retobj=obj.GetType().GetMethod(\"" + text5 + "\").Invoke(obj," + text6 + ");if(retobj is IConvertible) SetValueEvent(\"[" + item.ToObject.Key + "]\", Convert.ChangeType(retobj, Type.GetType(\"" + item.ToObject.Value + "\"))); else SetValueEvent(\"[" + item.ToObject.Key + "]\", retobj );}}");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "." + eventname + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                }
                else if (item.OperationType == "定义标签")
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t" + item.FromObject + ":" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t;" + Environment.NewLine);
                }
                else if (item.OperationType == "跳转标签")
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\ttry" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\tif(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\tgoto " + item.FromObject + ";" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "." + eventname + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                }
                else if (item.OperationType == "服务器逻辑")
                {
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("\t\t\t\ttry" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\tif(Convert.ToBoolean(EvalEvent(\"" + ((item.Condition == null) ? "true" : item.Condition).Replace("\\", "\\\\").Replace("\"", "\\\"") + "\")))" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t{" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t\tCallRuntimeExecute(\"服务器逻辑\",datafile.eventBindDict[\"" + eventname + "\"][" + list.IndexOf(item) + "].Tag);" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\t}" + Environment.NewLine);
                    stringBuilder.Append("\t\t\t\tcatch (System.Exception ex){MessageBox.Show(\"" + info + "事件在触发时产生错误.\" + Environment.NewLine + ex.Message);}");
                }
            }
        }
        return stringBuilder;
    }
}
