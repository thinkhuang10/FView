using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace HMIWeb;

public class Utils
{
    public static string GetLogicToScript(string str)
    {
        var stringBuilder = new StringBuilder(str);
        var regex = new Regex("\\[[^\\]]+\\]");
        var matchCollection = regex.Matches(str);
        var list = new List<string>();
        foreach (Match item in matchCollection)
        {
            var arrayVar = item.Value.Split('$');
            if (2 == arrayVar.Length)
            {
                arrayVar[1] = "[" + arrayVar[1];
                var suffix = "System.GetValue(\"" + arrayVar[1] + "\")";
                var temp = "System.GetValue(\"" + arrayVar[0] + "\"+" + suffix + "+\"" + "]\")";
                stringBuilder = stringBuilder.Replace(item.Value, temp);
            }
            else
            {
                if (!list.Contains(item.Value))
                {
                    stringBuilder = stringBuilder.Replace(item.Value, "System.GetValue(\"" + item.Value + "\")");
                    list.Add(item.Value);
                }
            }
        }
        return stringBuilder.ToString();
    }


    public static bool CheckZero(object target)
    {
        if (Convert.ToDouble(target) == 0.0)
            return false;

        return true;
    }
}
