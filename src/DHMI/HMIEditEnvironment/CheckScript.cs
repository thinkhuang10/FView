using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

internal class CheckScript
{
    public static List<string> ReplaceWord;

    public static string[] Keystr = new string[167]
    {
        "AddHandler", "AddressOf", "Alias", "And", "AndAlso", "As", "Boolean", "ByRef", "Byte", "ByVal",
        "Call", "Case", "Catch", "CBool", "CByte", "CChar", "CDate", "CDec", "CDbl", "Char",
        "CInt", "Class", "CLng", "CObj", "Const", "Continue", "CSByte", "CShort", "CSng", "CStr",
        "CType", "CUInt", "CULng", "CUShort", "Date", "Decimal", "Declare", "Default", "Delegate", "Dim",
        "DirectCast", "Do", "Double", "Each", "Else", "ElseIf", "End", "EndIf", "Enum", "Erase",
        "Error", "NeedRefresh", "Exit", "False", "Finally", "For", "Friend", "Function", "Get", "GetType",
        "GetXMLNamespace", "Global", "GoSub", "GoTo", "Handles", "If", "Implements", "Imports", "Imports", "In",
        "Inherits", "Integer", "Interface", " Is ", "IsNot", "Let", "Lib", "Like", "Long", "Loop",
        "Me", "Mod", "Module", "MustInherit", "MustOverride", "MyBase", "MyClass", "Namespace", "Narrowing", "New",
        "Next", "Not", "Nothing", "NotInheritable", "NotOverridable", "Object", "Of", "On", "Operator", "Option",
        "Optional", "Or", "OrElse", "Overloads", "Overridable", "Overrides", "ParamArray", "Partial", "Private", "Property",
        "Protected", "Public", "RaiseEvent", "ReadOnly", "ReDim", "REM", "RemoveHandler", "Resume", "Return", "SByte",
        "Select", "Set", "Shadows", "Shared", "Short", "Single", "Static", "Step", "Stop", "String",
        "Structure", "Sub", "SyncLock", "Then", "Throw", "To", "True", "Try", "TryCast", "TypeOf",
        "Variant", "Wend", "UInteger", "ULong", "UShort", "Using", "When", "While", "Widening", "With",
        "WithEvents", "WriteOnly", "Xor", "+", "-", "*", "/", "^", "&", "|",
        "(", ")", "=", "<", ">", "\r", "\n"
    };

    public static string CSharpKeystr = "abstract \r\n event \r\n new \r\n struct \r\n \r\nas \r\n explicit \r\n null \r\n switch \r\n \r\nbase \r\n extern \r\n object \r\n this \r\n \r\nbool \r\n false \r\n operator \r\n throw \r\n \r\nbreak \r\n finally \r\n out \r\n true \r\n \r\nbyte \r\n fixed \r\n override \r\n try \r\n \r\ncase \r\n float \r\n params \r\n typeof \r\n \r\ncatch \r\n for \r\n private \r\n uint \r\n \r\nchar \r\n foreach \r\n protected \r\n ulong \r\n \r\nchecked \r\n goto \r\n public \r\n unchecked \r\n \r\nclass \r\n if \r\n readonly \r\n unsafe \r\n \r\nconst \r\n implicit \r\n ref \r\n ushort \r\n \r\ncontinue \r\n in \r\n return \r\n using \r\n \r\ndecimal \r\n int \r\n sbyte \r\n virtual \r\n \r\ndefault \r\n interface \r\n sealed \r\n volatile \r\n \r\ndelegate \r\n internal \r\n short \r\n void \r\n \r\ndo \r\n is \r\n sizeof \r\n while \r\n \r\ndouble \r\n lock \r\n stackalloc \r\n  \r\n \r\nelse \r\n long \r\n static \r\n  \r\n \r\nenum \r\n namespace \r\n string \r\n \r\n";

    public static Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> MakeObjectDict()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> dictionary = new();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            Dictionary<string, Dictionary<string, List<string>>> dictionary2 = new();
            foreach (CShape item in df.ListAllShowCShape)
            {
                Dictionary<string, List<string>> dictionary3 = new();
                Type type = ((!(item is CControl)) ? item.GetType() : ((CControl)item)._c.GetType());
                MemberInfo[] members = type.GetMembers();
                foreach (MemberInfo memberInfo in members)
                {
                    if (!dictionary3.ContainsKey(memberInfo.Name))
                    {
                        dictionary3.Add(memberInfo.Name, null);
                    }
                }
                try
                {
                    dictionary2.Add(item.Name, dictionary3);
                }
                catch
                {
                    Exception ex = new("页面" + df.name + "存在多个名为" + item.Name + "的元素.");
                    throw ex;
                }
            }
            dictionary.Add(df.name, dictionary2);
        }
        return dictionary;
    }

    public static Dictionary<string, List<string>> MakeSystemApiDict()
    {
        Dictionary<string, List<string>> dictionary = new();
        Type typeFromHandle = typeof(ClassForScript);
        MemberInfo[] members = typeFromHandle.GetMembers();
        foreach (MemberInfo memberInfo in members)
        {
            dictionary.Add(memberInfo.Name, null);
        }
        return dictionary;
    }

    public static Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> MakeObjectItems()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> dictionary = new();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            Dictionary<string, Dictionary<string, List<string>>> dictionary2 = new();
            foreach (CShape item in df.ListAllShowCShape)
            {
                Dictionary<string, List<string>> dictionary3 = new();
                Type type = item.GetType();
                MemberInfo[] members = type.GetMembers();
                foreach (MemberInfo memberInfo in members)
                {
                    dictionary3.Add(memberInfo.Name, null);
                }
                dictionary2.Add(item.Name, dictionary3);
            }
            dictionary.Add(df.name, dictionary2);
        }
        return dictionary;
    }

    public static Dictionary<int, string> CheckScriptItems(string inputScript, Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> ObjectItems, Dictionary<string, List<string>> SystemAPIItems)
    {
        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> dictionary = new();
        Dictionary<string, List<string>> dictionary2 = new();
        Dictionary<int, string> dictionary3 = new();
        Regex regex = new("([a-zA-Z_][a-zA-Z0-9_]*)\\.([a-zA-Z_]*[a-zA-Z0-9_]*)\\.?([a-zA-Z_]*[a-zA-Z0-9_]*)");
        Regex regex2 = new("\".*?\"");
        List<string> list = new(inputScript.Split('\n'));
        for (int i = 0; i < list.Count; i++)
        {
            MatchCollection matchCollection = regex.Matches(regex2.Replace(list[i].Split('\'')[0], "\"\""));
            for (int j = 0; j < matchCollection.Count; j++)
            {
                if (matchCollection[j].Groups[1].Value == "")
                {
                    if (!dictionary3.ContainsKey(Convert.ToInt32(i)))
                    {
                        dictionary3.Add(i, list[i]);
                    }
                }
                else if (matchCollection[j].Groups[2].Value == "" && !dictionary3.ContainsKey(Convert.ToInt32(i)))
                {
                    dictionary3.Add(i, list[i]);
                }
                if (matchCollection[j].Groups[3].Value != "" && matchCollection[j].Groups[1].Value.ToLower() != "system" && matchCollection[j].Groups[1].Value.ToLower().Substring(0, 2) != "m_")
                {
                    if (dictionary.ContainsKey(matchCollection[j].Groups[1].Value))
                    {
                        if (dictionary[matchCollection[j].Groups[1].Value].ContainsKey(matchCollection[j].Groups[2].Value))
                        {
                            if (dictionary[matchCollection[j].Groups[1].Value][matchCollection[j].Groups[2].Value].ContainsKey(matchCollection[j].Groups[3].Value))
                            {
                                List<string> list2 = dictionary[matchCollection[j].Groups[1].Value][matchCollection[j].Groups[2].Value][matchCollection[j].Groups[3].Value];
                                list2.Add(i.ToString());
                                continue;
                            }
                            Dictionary<string, List<string>> dictionary4 = dictionary[matchCollection[j].Groups[1].Value][matchCollection[j].Groups[2].Value];
                            List<string> list3 = new()
                            {
                                i.ToString()
                            };
                            dictionary4.Add(matchCollection[j].Groups[3].Value, list3);
                        }
                        else
                        {
                            Dictionary<string, Dictionary<string, List<string>>> dictionary5 = dictionary[matchCollection[j].Groups[1].Value];
                            Dictionary<string, List<string>> dictionary6 = new();
                            List<string> list4 = new()
                            {
                                i.ToString()
                            };
                            dictionary6.Add(matchCollection[j].Groups[3].Value, list4);
                            dictionary5.Add(matchCollection[j].Groups[2].Value, dictionary6);
                        }
                    }
                    else
                    {
                        Dictionary<string, Dictionary<string, List<string>>> dictionary7 = new();
                        Dictionary<string, List<string>> dictionary8 = new();
                        List<string> list5 = new()
                        {
                            i.ToString()
                        };
                        dictionary8.Add(matchCollection[j].Groups[3].Value, list5);
                        dictionary7.Add(matchCollection[j].Groups[2].Value, dictionary8);
                        dictionary.Add(matchCollection[j].Groups[1].Value, dictionary7);
                    }
                }
                else if (matchCollection[j].Groups[2].Value != "" && matchCollection[j].Groups[1].Value.ToLower().Substring(0, 2) != "m_" && matchCollection[j].Groups[1].Value.ToLower() == "system")
                {
                    if (dictionary2.ContainsKey(matchCollection[j].Groups[2].Value))
                    {
                        List<string> list6 = dictionary2[matchCollection[j].Groups[2].Value];
                        list6.Add(i.ToString());
                    }
                    else
                    {
                        List<string> list7 = new()
                        {
                            i.ToString()
                        };
                        dictionary2.Add(matchCollection[j].Groups[2].Value, list7);
                    }
                }
            }
        }
        foreach (string key in dictionary.Keys)
        {
            if (!ObjectItems.ContainsKey(key))
            {
                foreach (string key2 in dictionary[key].Keys)
                {
                    foreach (string key3 in dictionary[key][key2].Keys)
                    {
                        foreach (string item in dictionary[key][key2][key3])
                        {
                            if (!dictionary3.ContainsKey(Convert.ToInt32(item)))
                            {
                                dictionary3.Add(Convert.ToInt32(item), list[Convert.ToInt32(item)]);
                            }
                        }
                    }
                }
                continue;
            }
            foreach (string key4 in dictionary[key].Keys)
            {
                if (!ObjectItems[key].ContainsKey(key4))
                {
                    foreach (string key5 in dictionary[key][key4].Keys)
                    {
                        foreach (string item2 in dictionary[key][key4][key5])
                        {
                            if (!dictionary3.ContainsKey(Convert.ToInt32(item2)))
                            {
                                dictionary3.Add(Convert.ToInt32(item2), list[Convert.ToInt32(item2)]);
                            }
                        }
                    }
                    continue;
                }
                foreach (string key6 in dictionary[key][key4].Keys)
                {
                    if (ObjectItems[key][key4].ContainsKey(key6))
                    {
                        continue;
                    }
                    foreach (string item3 in dictionary[key][key4][key6])
                    {
                        if (!dictionary3.ContainsKey(Convert.ToInt32(item3)))
                        {
                            dictionary3.Add(Convert.ToInt32(item3), list[Convert.ToInt32(item3)]);
                        }
                    }
                }
            }
        }
        foreach (string key7 in dictionary2.Keys)
        {
            if (SystemAPIItems.ContainsKey(key7))
            {
                continue;
            }
            foreach (string item4 in dictionary2[key7])
            {
                if (!dictionary3.ContainsKey(Convert.ToInt32(item4)))
                {
                    dictionary3.Add(Convert.ToInt32(item4), list[Convert.ToInt32(item4)]);
                }
            }
        }
        return dictionary3;
    }

    public static List<string> JustGetReplaceWord()
    {
        if (ReplaceWord != null)
        {
            return ReplaceWord;
        }
        List<string> list = new();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            list.Add(df.name + "_______");
        }
        ReplaceWord = list;
        return list;
    }

    public static List<string> AddObjectForCheck(ScriptEngine se)
    {
        List<string> list = new();
        Control control = new();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            foreach (CShape item in df.ListAllShowCShape)
            {
                if (item is CControl)
                {
                    CControl cControl = new()
                    {
                        _dllfile = ((CControl)item)._dllfile,
                        type = ((CControl)item).type
                    };
                    cControl.AddPoint(((CControl)item).ImportantPoints[0]);
                    cControl.AddPoint(((CControl)item).ImportantPoints[1]);
                    if (cControl._c is AxHost)
                    {
                        ((ISupportInitialize)cControl._c).BeginInit();
                        control.Controls.Add(cControl._c);
                        ((ISupportInitialize)cControl._c).EndInit();
                    }
                    se.AddObject(df.name + "_____" + item.Name, cControl._c);
                }
                else
                {
                    se.AddObject(df.name + "_____" + item.Name, item.Copy());
                }
                list.Add(df.name + "_______");
            }
        }
        ReplaceWord = list;
        return list;
    }

    public static string ReplaceName(string script)
    {
        string[] keystr = Keystr;
        foreach (string text in keystr)
        {
            if (text != null)
            {
                script = script.ToUpper().Replace(text.ToUpper(), " ");
            }
        }
        return script;
    }

    public static List<string> FindErrorThing(string script)
    {
        List<string> list = new();
        try
        {
            if (script == null || script == "")
            {
                return list;
            }
            ScriptEngine scriptEngine = new(ScriptLanguage.VBscript);
            scriptEngine.AddCode("If false Then \n\r" + script + "\n\r End If");
        }
        catch (Exception ex)
        {
            list.Add(ex.Message);
        }
        return list;
    }

    public static List<string> FindErrorThing(string script, ScriptEngine se)
    {
        List<string> list = new();
        try
        {
            se.AddCode("If false Then \n\r" + script + "\n\r End If");
        }
        catch (Exception ex)
        {
            list.Add(ex.Message);
        }
        return list;
    }

    public static List<string> CheckScriptByIO(string script)
    {
        string[] array = new string[14]
        {
            "+", "-", "*", "/", "^", "&", "|", "(", ")", "=",
            "<", ">", "\r", "\n"
        };
        string[] array2 = array;
        foreach (string text in array2)
        {
            if (text != null)
            {
                script = script.ToUpper().Replace(text.ToUpper(), " ");
            }
        }
        string[] array3 = script.Split(' ');
        for (int j = 0; j < array3.Length; j++)
        {
            if (array3[j] == "")
            {
                continue;
            }
            try
            {
                Convert.ToDouble(array3[j]);
                array3[j] = "";
            }
            catch (Exception)
            {
            }
            try
            {
                Convert.ToBoolean(array3[j]);
                array3[j] = "";
            }
            catch (Exception)
            {
            }
            foreach (string iOItem in CheckIOExists.IOItemList)
            {
                if (iOItem.ToUpper() == array3[j].ToUpper())
                {
                    array3[j] = "";
                }
            }
        }
        string text2 = "";
        string[] array4 = array3;
        foreach (string text3 in array4)
        {
            text2 += text3;
        }
        string[] array5 = ReplaceName(text2).Split(' ');
        List<string> list = new();
        string[] array6 = array5;
        foreach (string text4 in array6)
        {
            if (text4 != "" && text4.Contains("."))
            {
                list.Add(text4);
            }
        }
        return list;
    }

    public static string CheckScriptBySym(string script)
    {
        StringBuilder stringBuilder = new(script.ToUpper());
        foreach (string onlyIOItem in CheckIOExists.OnlyIOItemList)
        {
            stringBuilder = stringBuilder.Replace(onlyIOItem.ToUpper(), "");
        }
        script = stringBuilder.ToString();
        string[] array = script.Split('\n');
        Regex regex = new("\".*?\"");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = regex.Replace(array[i].Split('\'')[0], "\"\"");
            if (!array[i].Contains("[") && !array[i].Contains("]"))
            {
                array[i] = "";
            }
        }
        string text = "";
        string[] array2 = array;
        foreach (string text2 in array2)
        {
            text += text2;
        }
        if (text.Replace("\n", "").Length == 0)
        {
            return null;
        }
        return text.Replace("\n", "");
    }

    public static string CheckWords(string logic, ScriptEngine se)
    {
        if (logic == null || logic == "")
        {
            return null;
        }
        List<string> list = FindErrorThing(logic, se);
        if (list.Count != 0)
        {
            string text = "";
            {
                foreach (string item in list)
                {
                    text = text + " " + item;
                }
                return text;
            }
        }
        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> objectItems = MakeObjectDict();
        Dictionary<string, List<string>> systemAPIItems = MakeSystemApiDict();
        Dictionary<int, string> dictionary = CheckScriptItems(logic, objectItems, systemAPIItems);
        if (dictionary.Count != 0)
        {
            using Dictionary<int, string>.KeyCollection.Enumerator enumerator2 = dictionary.Keys.GetEnumerator();
            if (enumerator2.MoveNext())
            {
                int current2 = enumerator2.Current;
                return "行" + current2 + ":" + dictionary[current2].Trim();
            }
        }
        return CheckScriptBySym(logic);
    }

    public static string CheckWords(string logic)
    {
        if (logic == null || logic == "")
        {
            return null;
        }
        List<string> list = FindErrorThing(logic);
        if (list.Count != 0)
        {
            string text = "";
            {
                foreach (string item in list)
                {
                    text = text + " " + item;
                }
                return text;
            }
        }
        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> objectItems = MakeObjectDict();
        Dictionary<string, List<string>> systemAPIItems = MakeSystemApiDict();
        Dictionary<int, string> dictionary = CheckScriptItems(logic, objectItems, systemAPIItems);
        if (dictionary.Count != 0)
        {
            using Dictionary<int, string>.KeyCollection.Enumerator enumerator2 = dictionary.Keys.GetEnumerator();
            if (enumerator2.MoveNext())
            {
                int current2 = enumerator2.Current;
                return "行" + current2 + ":" + dictionary[current2].Trim();
            }
        }
        return CheckScriptBySym(logic);
    }

    public static List<string> GetCSharpKeyStrs()
    {
        return new List<string>(CSharpKeystr.Replace(" ", "").Replace("\r", "").Split('\n'));
    }
}
